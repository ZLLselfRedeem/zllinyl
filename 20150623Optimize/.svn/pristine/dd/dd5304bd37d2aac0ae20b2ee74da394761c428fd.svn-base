using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Aliyun.OpenServices.OpenStorageService;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL.Persistence;
using VAGastronomistMobileApp.WebPageDll.Services.Infrastructure;

namespace VAGastronomistMobileApp.WebPageDll.Services
{
    /// <summary>
    /// 美食日记逻辑层接口
    /// </summary>
    public interface IFoodDiaryService
    {
        /// <summary>
        /// 根据订单号获取美食日志
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        FoodDiary GetFoodDiaryByOrder(long orderId);

        /// <summary>
        /// 根据id获取美食日志
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        FoodDiary GetFoodDiaryById(long id);

        /// <summary>
        /// 分享更新
        /// </summary>
        /// <param name="foodDiaryId"></param>
        /// <param name="content"></param>
        /// <param name="shared"></param>
        /// <param name="foodDiaryDishes"></param>
        void Update(long foodDiaryId, string content, bool isBig, bool isHideDishName, FoodDiaryShared shared, IEnumerable<FoodDiaryDish> foodDiaryDishes);


    }

    /// <summary>
    /// 美食日记逻辑层
    /// </summary>
    public class FoodDiaryService : BaseService, IFoodDiaryService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="repositoryContext"></param>
        public FoodDiaryService(IRepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }


        public FoodDiary GetFoodDiaryByOrder(long orderId)
        {
            return RepositoryContext.GetFoodDiaryRepository().GetFoodDiaryByOrderId(orderId);
        }

        public FoodDiary GetFoodDiaryById(long id)
        {
            return RepositoryContext.GetFoodDiaryRepository().GetById(id);
        }

        public void Update(long foodDiaryId, string content, bool isBig, bool isHideDishName, FoodDiaryShared shared, IEnumerable<FoodDiaryDish> foodDiaryDishes)
        {
            var foodDiary = RepositoryContext.GetFoodDiaryRepository().GetById(foodDiaryId);
            if (foodDiary != null)
            {
                foodDiary.Content = content;
                foodDiary.Shared |= shared;
                foodDiary.IsBig = isBig;
                foodDiary.IsHideDishName = isHideDishName;
                RepositoryContext.GetFoodDiaryRepository().Update(foodDiary);



                foreach (var fdd in foodDiaryDishes)
                {
                    RepositoryContext.GetFoodDiaryDishRepository().UpdateSortAndStatus(fdd.Id, fdd.Sort, fdd.Status);
                }
            }


        }
    }
}
