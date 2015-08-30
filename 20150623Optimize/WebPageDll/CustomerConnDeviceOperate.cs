using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.SQLServerDAL;

namespace VAGastronomistMobileApp.WebPageDll
{
    public class CustomerConnDeviceOperate
    {
        private readonly CustomerConnDeviceManager manager = new CustomerConnDeviceManager();

        /// <summary>
        /// 根据设备Id查询当日登陆过的用户
        /// </summary>
        /// <param name="deviceId"></param>
        /// <returns></returns>
        public List<long> SelectCustomerId(long deviceId)
        {
            return manager.SelectCustomerId(deviceId);
        }
    }
}
