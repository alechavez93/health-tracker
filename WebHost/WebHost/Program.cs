using WebHost.Infrastructure;

namespace WebHost
{
    class Program
    {
        public static void Main(string[] args)
        {
            var runner = new CliRunner();
            runner.Run(args);
        }
    }
}
