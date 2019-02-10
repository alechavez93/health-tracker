using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace WebHost.Infrastructure
{
    /// <summary>
    /// Class in charge of running the application pool from the Command Line Interface (CLI)
    /// </summary>
    public class CliRunner
    {
        /// <summary>
        /// Runs an instance of the application from the Command Line Interface (CLI)
        /// </summary>
        /// <param name="args"></param>
        public void Run(string[] args)
        {
            MainHost host = null;
            CancellationTokenSource startupTokenSource = null;
            Task startupTask = null;
            CancellationTokenSource warmupTokenSource = null;
            Task warmupTask = null;
            
            try
            {
                host = new MainHost();
                warmupTokenSource = new CancellationTokenSource();
                startupTokenSource = new CancellationTokenSource();
                
                startupTask = host.StartHost(args, startupTokenSource.Token)
                    .ContinueWith(state1 =>
                    {
                        if (state1.IsFaulted && state1.Exception != null)
                        {
                            Console.Error.WriteLine($"Error occurred at Application Host: {state1.Exception.ToString()}");
                            EventLog.WriteEntry(typeof(CliRunner).Assembly.GetName().Name, state1.Exception.ToString(), EventLogEntryType.Error);
                            return;
                        }

                        Console.WriteLine("Running Application Host...");
                        warmupTask = host.Warmup(warmupTokenSource.Token)
                            .ContinueWith(state2 => Console.WriteLine("   ... self-tested ..."), warmupTokenSource.Token);
                    },
                    startupTokenSource.Token);

                EventLog.WriteEntry(typeof(CliRunner).Assembly.GetName().Name, "Application Host start executed.", EventLogEntryType.Information);
                Console.WriteLine("Starting Application Host. Press ENTER key to stop.");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred at Application Host: {ex}");
                EventLog.WriteEntry(typeof(CliRunner).Assembly.GetName().Name, ex.ToString(), EventLogEntryType.Error);
            }
            finally
            {
                FinalizeTask(startupTask, startupTokenSource);

                try
                {
                    host?.StopHost();
                }
                catch (Exception ex)
                {
                    EventLog.WriteEntry(typeof(CliRunner).Assembly.GetName().Name, ex.ToString(), EventLogEntryType.Error);
                    throw;
                }

                FinalizeTask(warmupTask, warmupTokenSource);

                EventLog.WriteEntry(typeof(CliRunner).Assembly.GetName().Name, @"Application host stopped.", EventLogEntryType.Information);
            }
        }

        /// <summary>
        /// Finalizes a task and its token source
        /// </summary>
        /// <param name="task">The task to finalize.</param>
        /// <param name="tokenSource">The token source.</param>
        public static void FinalizeTask(Task task, CancellationTokenSource tokenSource)
        {
            try
            {
                if (tokenSource != null && task != null)
                {
                    if (!task.IsCanceled && !task.IsCompleted)
                    {
                        tokenSource.Cancel();
                    }
                }
            }
            catch
            {
                // ignored
            }
            finally
            {
                DisposeWithoutException(tokenSource);
            }
        }

        /// <summary>
        /// Disposes an object without throwing an exception
        /// </summary>
        /// <param name="target">The disposable object.</param>
        public static void DisposeWithoutException(IDisposable target)
        {
            try
            {
                target?.Dispose();
            }
            catch
            {
                // ignored
            }
        }
    }
}
