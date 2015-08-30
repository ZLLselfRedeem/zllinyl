using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL.Persistence.Infrastructure;

namespace VAGastronomistMobileApp.SQLServerDAL.Persistence
{
    public interface IDishPriceInfoRepository
    {
       
        int UpdatePrice(int dishPriceId, double price);

        DishPriceInfo GetById(long id);
    }
}