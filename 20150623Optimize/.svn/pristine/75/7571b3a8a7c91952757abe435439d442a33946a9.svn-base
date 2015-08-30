using System.Collections.Generic;
using System.Linq;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.WebPageDll.Services.Infrastructure;

namespace VAGastronomistMobileApp.WebPageDll.Services
{
    /// <summary>
    /// 美食日记缺省菜品配置接口
    /// </summary>
    public interface IFoodDiaryDefaultConfigDishService
    {
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="foodDiaryDefaultConfigDish"></param>
        void Add(FoodDiaryDefaultConfigDish foodDiaryDefaultConfigDish);

        /// <summary>
        /// 获取全部
        /// </summary>
        /// <returns></returns>
        IList<FoodDiaryDefaultConfigDish> GetAllList();

        /// <summary>
        /// 删除,假删除
        /// </summary>
        /// <param name="id"></param>
        void Delete(int id);
    }

    /// <summary>
    /// 美食日记缺省菜品配置业务逻辑
    /// </summary>
    public class FoodDiaryDefaultConfigDishService : BaseService, IFoodDiaryDefaultConfigDishService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="repositoryContext"></param>
        public FoodDiaryDefaultConfigDishService(IRepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public void Add(FoodDiaryDefaultConfigDish foodDiaryDefaultConfigDish)
        {
            RepositoryContext.GetFoodDiaryDefaultConfigDishRepository().Add(foodDiaryDefaultConfigDish);
        }

        public IList<FoodDiaryDefaultConfigDish> GetAllList()
        {
            var list = RepositoryContext.GetFoodDiaryDefaultConfigDishRepository().GetEnableFoodDiaryDefaultConfigDishes();
            return list.ToList();
        }

        public void Delete(int id)
        {
            RepositoryContext.GetFoodDiaryDefaultConfigDishRepository().PseudoDelete(id);
        }
    }
}
