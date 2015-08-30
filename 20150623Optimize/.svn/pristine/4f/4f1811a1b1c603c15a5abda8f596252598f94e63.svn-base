using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL;

namespace VAGastronomistMobileApp.WebPageDll
{
    /// <summary>
    /// 批量打款日记业务逻辑
    /// </summary>
    public class StoresMoneyLogOperate
    {
        readonly StoresMoneyLogManager manager = new StoresMoneyLogManager();
        /// <summary>
        /// 生成一条批量打款主表操作日志
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddStoresMoneyLog(StoresMoneyLog model)
        {
            return manager.insertLog(model);
        }

        /// <summary>
        /// 生成一条批量打款明细操作日志
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddStoresMoneyDetailLog(StoresMoneyLog model)
        {
            return manager.insertDetailLog(model);
        }
    }
}
