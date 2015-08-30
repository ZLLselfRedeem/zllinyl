using System;
using System.Collections.Generic;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL.Persistence.Infrastructure;

namespace VAGastronomistMobileApp.SQLServerDAL.Persistence
{
    public interface IFoodDiaryRepository
    {
        void Add(FoodDiary entity);
        void Update(FoodDiary entity);

        FoodDiary GetById(long id);
        FoodDiary GetFoodDiaryByOrderId(long orderId);

        void IncrementHit(long id);

        /// <summary>
        /// 用于统计页面
        /// </summary>
        /// <param name="page"></param>
        /// <param name="mobilePhoneNumber"></param>
        /// <param name="isPaid"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        IEnumerable<FoodDiaryDetails> GetPageFoodDiaryDetailses(Page page, string mobilePhoneNumber, bool isPaid, DateTime startDate, DateTime endDate,out int count);
        /// <summary>
        /// 用于统计页面输出
        /// </summary>
        /// <param name="mobilePhoneNumber"></param>
        /// <param name="isPaid"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        IEnumerable<FoodDiaryDetails> GetManyFoodDiaryDetailses(string mobilePhoneNumber, bool isPaid,DateTime startDate, DateTime endDate);
    }
}