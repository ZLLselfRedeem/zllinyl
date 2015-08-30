using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VA.CacheLogic.Dish;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL;

namespace VAGastronomistMobileApp.WebPageDll
{
    public class ClientOrderDetailConfigOperate
    {
        public List<ClientOrderDetailConfig> GetClientOrderDetailConfig()
        {
            return new OrderConfigLogic().GetClientOrderDetailConfigOfCache();
        }
    }
}
