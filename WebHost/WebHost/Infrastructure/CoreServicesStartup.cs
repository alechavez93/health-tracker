using Unity;
using Unity.Lifetime;
using WebHost.Application;
using WebHost.Controllers;
using WebHost.DataAccess;
using WebHost.DataAccess.SqlServerDbStore;

namespace WebHost.Infrastructure
{
    public class CoreServicesStartup
    {
        /// <summary>
        /// Registers all the dependencies in the unity container for injection 
        /// </summary>
        /// <param name="container">The unity container.</param>
        public static void InitializeUnityContainer(IUnityContainer container)
        {
            // User layers
            container.RegisterType<IUserRepository, UserSqlServerRepository>();
            container.RegisterType<IUserService, UserService>();
            container.RegisterType<UserController>();
        }
    }
}
