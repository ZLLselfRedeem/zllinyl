using System.ServiceModel;
using Aliyun.OpenServices.OpenStorageService;
using Autofac;
using Autofac.Extras.AggregateService;
using Autofac.Extras.DynamicProxy2;
using Autofac.Integration.Wcf;
using Autofac.Integration.Web;
using System;
using System.Configuration;
using IDishMenuAsynUpdate;

namespace VAGastronomistMobileApp.WebPageDll.Services.Infrastructure
{
    /// <summary>
    /// 
    /// </summary>
    public class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            if (builder == null)
                throw new ArgumentNullException("builder");

            builder.RegisterAssemblyTypes(typeof(PreOrder19DianService).Assembly)
                .Where(t => t.Name.EndsWith("Service") && t.Namespace == typeof(PreOrder19DianService).Namespace)
                .AsImplementedInterfaces()
                .EnableInterfaceInterceptors()
                .InterceptedBy(typeof(TransactionInterceptor))
                .InstancePerHttpRequest();

            builder.RegisterAggregateService<IRepositoryContext>();
            builder.RegisterType<TransactionInterceptor>();

            builder.Register(p => new OssClient(ConfigurationManager.AppSettings["ossEndPoint"], ConfigurationManager.AppSettings["ossAccessID"], ConfigurationManager.AppSettings["ossAccessKey"]))
                .As<IOss>();

            string address = ConfigurationManager.AppSettings["MenuUpdateServiceAddress"];

            builder.Register(c => new ChannelFactory<IMenuService>(
               new NetTcpBinding(SecurityMode.None),
               new EndpointAddress(address)))
             .SingleInstance();

            builder.Register(c => c.Resolve<ChannelFactory<IMenuService>>().CreateChannel()).UseWcfSafeRelease();
        }
    }
}
