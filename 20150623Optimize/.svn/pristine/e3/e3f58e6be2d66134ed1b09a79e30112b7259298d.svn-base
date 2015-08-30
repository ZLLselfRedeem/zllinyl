using System;
using System.ServiceProcess;
using Autofac;

namespace TenpayRefundCheck
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        static void Main()
        {
#if ConsoleApplication
            using (var c = Bootstrapper.Start())
            {
                var cb = c.Resolve<ICallback>();
                cb.Container = c;
                cb.Run(null);
                Console.Read();
            }
#else
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] 
            { 
                new TenpayRefundCheckService() 
            };
            ServiceBase.Run(ServicesToRun);
#endif

        }
    }
}
