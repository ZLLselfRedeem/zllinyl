using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.Model;

namespace VAGastronomistMobileApp.SQLServerDAL.Persistence
{
    public interface IShopRevealImageRepository
    {
        IEnumerable<ShopRevealImage> GetShopRevealImages(int shopId);
    }
}
