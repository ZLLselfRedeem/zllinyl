using Autofac;
using VAGastronomistMobileApp.SQLServerDAL.Persistence.Infrastructure;

namespace VAGastronomistMobileApp.WebPageDll.Services.Infrastructure
{
    public class Bootstrapper
    {
        static readonly IContainer _container;

        static Bootstrapper()
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule<EntityFrameworkModule>();
            builder.RegisterModule<ServiceModule>();

            _container = builder.Build();
        }

        public static IContainer Container
        {
            get { return _container; }
        }
    }
}
