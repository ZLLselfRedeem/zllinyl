using System;
using System.Collections.Generic;
using System.Linq;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL.Persistence;
using VAGastronomistMobileApp.WebPageDll.Services.Infrastructure;

namespace VAGastronomistMobileApp.WebPageDll.Services
{
    /// <summary>
    /// 美食日记菜单逻辑层接口
    /// </summary>
    public interface IFoodDiaryDishService
    {
        IList<FoodDiaryDish> GetFoodDiaryDishesByFoodDiaryId(long foodDiaryId);
    }

    /// <summary>
    /// 美食日记菜单逻辑层
    /// </summary>
    public class FoodDiaryDishService : BaseService, IFoodDiaryDishService
    {
        public FoodDiaryDishService(IRepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public IList<FoodDiaryDish> GetFoodDiaryDishesByFoodDiaryId(long foodDiaryId)
        {
            return RepositoryContext.GetFoodDiaryDishRepository().GetFoodDiaryDishesByFoodDiaryId(foodDiaryId).ToList();
        }
    }
}
