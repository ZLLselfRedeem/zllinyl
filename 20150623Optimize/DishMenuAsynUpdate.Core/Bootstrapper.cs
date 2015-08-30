using System;
using System.IO;
using Autofac;
using Autofac.Extras.AggregateService;
using Autofac.Extras.DynamicProxy2;
using IDishMenuAsynUpdate;
using VAGastronomistMobileApp.SQLServerDAL.Persistence;
using VAGastronomistMobileApp.SQLServerDAL.Persistence.Infrastructure;
using VAGastronomistMobileApp.WebPageDll.Services.Infrastructure;

namespace DishMenuAsynUpdate.Core
{
    public class Bootstrapper
    {
        public static IContainer Start()
        {
            string fileFullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log4net.config");
            var fileInfo = new System.IO.FileInfo(fileFullPath);

            log4net.Config.XmlConfigurator.Configure(fileInfo);

            var builder = new ContainerBuilder();
            builder.RegisterAssemblyTypes(typeof(SqlServerFoodDiaryRepository).Assembly)
                .Where(t => t.Name.EndsWith("Repository") && t.Name.StartsWith("SqlServer"))
                .AsImplementedInterfaces();

            builder.RegisterType<SqlServerUnitOfWork>().As<IUnitOfWork>();

            builder.RegisterType<SqlConnectionFactory>().As<ISqlConnectionFactory>();

            builder.RegisterType<MenuService>()
                .As<IMenuService>()
                .InterceptedBy(typeof(TransactionInterceptor));

            builder.RegisterAggregateService<IRepositoryContext>();
            builder.RegisterType<TransactionInterceptor>();

            builder.RegisterModule<LoggingModule>();

            return builder.Build();
        }
    }
}
