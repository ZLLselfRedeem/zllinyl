using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL;

namespace VAGastronomistMobileApp.WebPageDll
{
    public partial class ShopHandleLogOperate
    {
        /// <summary>
        /// 新增门店审核日志记录
        /// </summary>
        /// <param name="handleStatus"></param>
        /// <param name="shopId"></param>
        /// <param name="shopName"></param>
        /// <param name="cityId"></param>
        /// <param name="employeeId"></param>
        public static void InsertShopHandleLog(int handleStatus, int shopId, string shopName, int cityId, int employeeId)
        {
            string employeeName = Common.GetFieldValue("EmployeeInfo", "EmployeeFirstName+EmployeeLastName employeeName", "EmployeeID='" + employeeId + "'");

            var handleLog = new ShopHandleLog()
            {
                EmployeeId = employeeId,
                EmployeeName = employeeName,
                HandleStatus = handleStatus,
                ShopId = shopId,
                ShopName = shopName,
                CityId = cityId,
                OperateTime = DateTime.Now,
                HandleDesc = string.Empty
            };
            ParameterizedThreadStart threadstart = new ParameterizedThreadStart(_InsertShopHandleLog);
            Thread thread = new Thread(threadstart);
            thread.IsBackground = true;
            thread.Start(handleLog);
        }
        public static void _InsertShopHandleLog(object shopHandleLog)
        {
            new ShopHandleLogManager().InsertShopHandleLog((ShopHandleLog)shopHandleLog);
        }
    }
}
