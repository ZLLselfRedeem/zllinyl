using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VAGastronomistMobileApp.WebPageDll.Services.Infrastructure
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class BaseService : IService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="repositoryContext"></param>
        protected BaseService(IRepositoryContext repositoryContext)
        {
            RepositoryContext = repositoryContext;
        }

        /// <summary>
        /// 
        /// </summary>
        public IRepositoryContext RepositoryContext
        {
            get;
            private set;
        }
    }
}
