using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Autofac;
using Autofac.Integration.Web;

namespace VAGastronomistMobileApp.WebPageDll.Services.Infrastructure
{
    /// <summary>
    /// 
    /// </summary>
    public static class ServiceFactory
    {
        private static readonly IContainerProviderAccessor ContainerProviderAccessor;
        //private static IContainerProvider cp;
        static ServiceFactory()
        {
            ContainerProviderAccessor = (IContainerProviderAccessor)HttpContext.Current.ApplicationInstance;
        }

        /// <summary>
        /// 反转
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T Resolve<T>()
        {
            return ContainerProviderAccessor.ContainerProvider.RequestLifetime.Resolve<T>();
        }
    }
}
