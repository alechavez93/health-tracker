using System;
using Owin;
using System.Web.Http;
using Microsoft.Owin.Cors;
using Unity;

namespace WebHost.Infrastructure
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            var config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();

            var container = (IUnityContainer) AppDomain.CurrentDomain.GetData("unityContainer");
            config.DependencyResolver = new UnityResolver(container);

            appBuilder.UseWebApi(config);
//            config.EnsureInitialized();
        }
    }
}
