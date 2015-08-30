using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.SQLServerDAL;

namespace VAGastronomistMobileApp.WebPageDll
{
   public class DeviceOperate
    {
       private readonly DeviceManager manager = new DeviceManager();

        /// <summary>
       /// 根据UUID查询设备号
       /// </summary>
       /// <param name="uuid"></param>
       /// <returns></returns>
       public long SelectDeviceIdByUUID(string uuid)
       {
           return manager.SelectDeviceIdByUUID(uuid);
       }
    }
}
