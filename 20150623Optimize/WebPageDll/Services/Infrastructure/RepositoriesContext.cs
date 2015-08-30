using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.SQLServerDAL.Persistence.Infrastructure;

namespace VAGastronomistMobileApp.WebPageDll.Services.Infrastructure
{
    /// <summary>
    /// 数据操作上下文实现
    /// </summary>
    public abstract class RepositoriesContext : IRepositoriesContext
    {
        private readonly IUnitOfWork _unitOfWork;
        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="unitOfWork"></param>
        public RepositoriesContext(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        /// <summary>
        /// 工作单元
        /// </summary>
        public IUnitOfWork UnitOfWork
        {
            get { return _unitOfWork; }
        }
    }
}
