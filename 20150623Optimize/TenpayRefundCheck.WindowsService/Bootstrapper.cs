using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Autofac;
using Autofac.Extras.AggregateService;
using LogDll;
using VAGastronomistMobileApp.SQLServerDAL.Persistence;
using VAGastronomistMobileApp.SQLServerDAL.Persistence.Infrastructure;
using VAGastronomistMobileApp.WebPageDll.Services.Infrastructure;

namespace TenpayRefundCheck
{
    public class Bootstrapper
    {
        public static IContainer Start()
        {
            string fileFullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log4net.config");
            var fileInfo = new FileInfo(fileFullPath);

            Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Log\\LogError"));
            Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Log\\LogInfo"));

            log4net.Config.XmlConfigurator.Configure(fileInfo);

            var builder = new ContainerBuilder();
            builder.RegisterAssemblyTypes(typeof(SqlServerFoodDiaryRepository).Assembly)
                .Where(t => t.Name.EndsWith("Repository") && t.Name.StartsWith("SqlServer"))
                .AsImplementedInterfaces();

            builder.RegisterType<SqlServerUnitOfWork>().As<IUnitOfWork>();

            builder.RegisterType<SqlConnectionFactory>().As<ISqlConnectionFactory>();
           

            builder.RegisterAggregateService<IRepositoryContext>();
            builder.RegisterType<TransactionInterceptor>();

            builder.RegisterModule<LoggingModule>();

            builder.RegisterType<TimerCallback>().As<ICallback>();

            return builder.Build();
        }
    }
}
