using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.DBUtility;

namespace VAGastronomistMobileApp.SQLServerDAL.Persistence.Infrastructure
{
    public class DatabaseFactory : Disposable, IDatabaseFactory
    {
        private ViewAllocContext _dataContext;
        public ViewAllocContext Get()
        {
            return _dataContext ?? (_dataContext = new ViewAllocContext(SqlHelper.ConnectionStringLocalTransaction));
        }
        protected override void DisposeCore()
        {
            if (_dataContext != null)
                _dataContext.Dispose();
        }
    }
}
