using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;
using Unity;

namespace WebHost.Infrastructure
{
    public class UnityResolver : IDependencyResolver
    {
        protected IUnityContainer container;

        public UnityResolver(IUnityContainer container)
        {
            this.container = container;
        }

        public object GetService(Type serviceType)
        {
            try
            {
                if (this.container.IsRegistered(serviceType))
                {
                    return this.container.Resolve(serviceType);
                }

                return null;
            }
            catch (ResolutionFailedException)
            {
                return null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return this.container.ResolveAll(serviceType);
            }
            catch (ResolutionFailedException)
            {
                return new List<object>();
            }
        }

        public IDependencyScope BeginScope()
        {
            return new UnityResolver(this.container.CreateChildContainer());
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.container.Dispose();
            }
        }
    }
}
