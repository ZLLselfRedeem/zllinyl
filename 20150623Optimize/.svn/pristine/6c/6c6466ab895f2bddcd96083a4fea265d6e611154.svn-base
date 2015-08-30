using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL;

namespace VAGastronomistMobileApp.WebPageDll
{
    public class WechatCustomerOperate
    {
        private WechatCustomerManager customerMan;

        public WechatCustomerOperate()
        {
            if (customerMan == null)
                customerMan = new WechatCustomerManager();
        }
        
        /// <summary>
        /// Add WechatCustomerInfo
        /// </summary>
        /// <param name="freeCaseInfo"></param>
        /// <returns></returns>
        public int Insert(WechatCustomerInfo customerInfo)
        {
            return customerMan.Insert(customerInfo);
        }
    }
}
