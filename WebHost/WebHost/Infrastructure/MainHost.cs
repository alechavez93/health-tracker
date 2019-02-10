using System;
using Microsoft.Owin.Hosting;
using System.Threading;
using System.Threading.Tasks;
using Unity;

namespace WebHost.Infrastructure
{
    public class MainHost
    {
        private IDisposable webApplication;
        private readonly string url = "http://localhost:8000";

        public async Task StartHost(string[] args, CancellationToken cancellationToken = default(CancellationToken))
        {
            // Unity container for dependency injection
            IUnityContainer container = new UnityContainer();
            AppDomain.CurrentDomain.SetData("unityContainer", container);
            CoreServicesStartup.InitializeUnityContainer(container);

            this.webApplication = WebApp.Start<Startup>(this.url);
        }

        public async Task Warmup(CancellationToken cancellationToken = default(CancellationToken))
        {

        }

        public async Task StopHost()
        {

        }
    }
}
