using Autofac;
using Autofac.Integration.Wcf;
using DishMenuAsynUpdate.Core;
using IDishMenuAsynUpdate;
using System;
using System.Diagnostics;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Timers;
using VAGastronomistMobileApp.WebPageDll.Services.Infrastructure;

namespace DishMenuAsynUpdate
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
             则可以通过将“system.serviceModel/serviceHostingEnvironment/multipleSiteBindingsEnabled”设置为 true，或指定“system.serviceModel/serviceHostingEnvironment/baseAddressPrefixFilters”来解决此问题。
             */

            using (IContainer container = DishMenuAsynUpdate.Core.Bootstrapper.Start())
            {
                using (ServiceHost host = new ServiceHost(typeof(MenuService)))
                {
                    //host.AddServiceEndpoint(typeof(IService1), new NetTcpBinding(), "net.tcp://127.0.0.1:6666/Service1");
                    host.AddDependencyInjectionBehavior<IMenuService>(container);
                    if (host.Description.Behaviors.Find<ServiceMetadataBehavior>() == null)
                    {
                        var behavior = new ServiceMetadataBehavior
                        {
                            HttpGetEnabled = true,
                            HttpGetUrl = new Uri("http://127.0.0.1:9999/MenuService/metadata")
                        };
                        host.Description.Behaviors.Add(behavior);
                    }
                    host.Opened += delegate
                    {
                        Console.WriteLine("MenuService已经启动，按任意键终止服务！");
                    };

                     host.Open();
                        Console.ReadLine();
                    //using (var timer = new Timer(1000 * 60 * 5))
                    //{
                    //    timer.Enabled = true;
                    //    timer.AutoReset = true;

                    //    timer.Elapsed += (sender, e) =>
                    //    {
                    //        Debug.Assert(container != null, "container != null");
                    //        // ReSharper disable AccessToDisposedClosure
                    //        using (var scope = container.BeginLifetimeScope())
                    //        // ReSharper restore AccessToDisposedClosure
                    //        {
                    //            var repositoryContext = scope.Resolve<IRepositoryContext>();
                    //            var menuUpdateTask = repositoryContext.GetMenuUpdateTaskRepository();
                    //            //menuUpdateTask.GetAllFailureTask(3);
                    //            //menuUpdateTask.
                    //        }
                    //    }
                    //        ;
                    //    timer.Start();

                    //   
                    //}

                }
                Environment.Exit(0);
            }
        }


    }
}
