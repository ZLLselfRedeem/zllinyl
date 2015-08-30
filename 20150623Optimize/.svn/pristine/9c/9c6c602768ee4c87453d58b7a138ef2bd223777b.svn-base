using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    /// <summary>
    /// 年夜饭套餐排期详情
    /// 2015-1-4
    /// </summary>
    public class MealSchedule
    {
        /// <summary>
        /// 套餐排期Id
        /// </summary>
        public int MealScheduleID { get; set; }
        /// <summary>
        /// 套餐Id
        /// </summary>
        public int MealID { get; set; }
        /// <summary>
        /// 套餐有效期起
        /// </summary>
        public DateTime ValidFrom { get; set; }
        /// <summary>
        /// 套餐有效期止
        /// </summary>
        public DateTime ValidTo { get; set; }
        /// <summary>
        /// 已售份数
        /// </summary>
        public int SoldCount { get; set; }
        /// <summary>
        /// 就餐时间
        /// </summary>
        public DateTime DinnerTime { get; set; }
        /// <summary>
        /// 是否有效
        /// </summary>
        public int IsActive { get; set; }
        /// <summary>
        /// 订餐类别
        /// </summary>
        public DinnerType DinnerType { get; set; }
        /// <summary>
        /// 套餐总份数
        /// </summary>
        public int TotalCount { get; set; }
        /// <summary>
        /// 就餐时间（对应农历）
        /// </summary>
        public string lunarCalendar { get; set; }
        /// <summary>
        /// 同一天的套餐是否已售罄
        /// </summary>
        public bool isSoldOut { get; set; }
    }
    public class MealScheduleQueryObject
    { 
        public int? MealID { get; set; } 

        public DateTime? DinnerTimeFrom { get; set; }
        public DateTime? DinnerTimeTo { get; set; }

        public DateTime? DinnerTime { get; set; }

        public int? DinnerType { get; set; }
    }

    public enum DinnerType
    {
        /// <summary>
        /// 中餐
        /// </summary>
        中餐 = 1,
        /// <summary>
        /// 晚餐
        /// </summary>
        晚餐 = 2
    }

    public class MealScheduleCount
    {
        public int mealId { get; set; }
        public string dinnerTime { get; set; }
        public int remainCount { get; set; }
    }
}
