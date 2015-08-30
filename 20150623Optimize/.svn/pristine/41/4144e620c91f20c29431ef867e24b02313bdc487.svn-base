using System.Collections.Generic;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL.Persistence.Infrastructure;

namespace VAGastronomistMobileApp.SQLServerDAL.Persistence
{
    public interface IFoodDiaryDefaultConfigDishRepository
    {
        void Add(FoodDiaryDefaultConfigDish entity);
        /// <summary>
        /// 伪删除
        /// </summary>
        /// <param name="id"></param>
        void PseudoDelete(int id);
        /// <summary>
        /// 获取所有可用的FoodDiaryDefaultConfigDish列表
        /// </summary>
        /// <returns></returns>
        IEnumerable<FoodDiaryDefaultConfigDish> GetEnableFoodDiaryDefaultConfigDishes();
    }
}