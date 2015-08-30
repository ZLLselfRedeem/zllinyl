using System.Collections.Generic;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL.Persistence.Infrastructure;

namespace VAGastronomistMobileApp.SQLServerDAL.Persistence
{
    public interface IFoodDiaryDishRepository
    {
        void Add(FoodDiaryDish entity);
        void UpdateSortAndStatus(long id, int sort, bool status);

        IEnumerable<FoodDiaryDish> GetFoodDiaryDishesByFoodDiaryId(long foodDiaryId);

        void AddRange(List<FoodDiaryDish> dishes);
    }
}