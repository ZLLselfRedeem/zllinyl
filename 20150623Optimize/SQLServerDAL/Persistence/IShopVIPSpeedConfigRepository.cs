using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.Model;
using System.Data;

namespace VAGastronomistMobileApp.SQLServerDAL.Persistence
{
    public interface IShopVIPSpeedConfigRepository
    {
        ShopVIPSpeedConfig GetShopVipSpeedConfigByCityAndHour(int cityId, int hour);

        int[] GetCityForShopVipSpeedConfig();
        
        int InsertShopVipSpeedConfig(ShopVIPSpeedConfig config);

        bool DeleteShopVipSpeedConfig(int cityId);

        DataTable GetCityVipSpeed(int cityId = 0);
    }
}
