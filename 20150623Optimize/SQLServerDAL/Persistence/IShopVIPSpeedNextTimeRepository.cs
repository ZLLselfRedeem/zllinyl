using System.Collections.Generic;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.Model;

namespace VAGastronomistMobileApp.SQLServerDAL.Persistence
{
    public interface IShopVIPSpeedNextTimeRepository
    {
        ShopVIPSpeedNextTime GetByCity(int cityId);

        void Add(ShopVIPSpeedNextTime shopVipSpeedNextTime);

        void Update(ShopVIPSpeedNextTime shopVipSpeedNextTime);
    }
}
