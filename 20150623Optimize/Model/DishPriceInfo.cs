using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    /// <summary>
    /// 菜价格信息
    /// </summary>
    public class DishPriceInfo
    {
        public DishPriceInfo()
        {
            DishPrice = 0;
            DishID = 0;
            DishSoldout = false;
            DishPriceStatus = 0;
            DishNeedWeigh = false;
            vipDiscountable = false;
            backDiscountable = false;
            dishIngredientsMinAmount = 0;
            dishIngredientsMaxAmount = 0;
        }

        /// <summary>
        /// 菜价格编号
        /// </summary>
        public int DishPriceID { get; set; }
        /// <summary>
        /// 菜单价
        /// </summary>
        public double? DishPrice { get; set; }
        /// <summary>
        /// 对应菜名编号
        /// </summary>
        public int? DishID { get; set; }
        /// <summary>
        /// 菜是否售罄
        /// </summary>
        public bool? DishSoldout { get; set; }
        /// <summary>
        /// 菜价状态（是否删除）
        /// 0：已删除，1：正常
        /// </summary>
        public int? DishPriceStatus { get; set; }
        /// <summary>
        /// 菜是否需要称重
        /// </summary>
        public bool? DishNeedWeigh { get; set; }
        /// <summary>
        /// 菜是否能享受Vip折扣
        /// </summary>
        public bool? vipDiscountable { get; set; }
        /// <summary>
        /// 菜能否支持返送
        /// </summary>
        public bool? backDiscountable { get; set; }

        public int? dishIngredientsMinAmount { get; set; }

        public int? dishIngredientsMaxAmount { get; set; }

        public string sundryJson { set; get; }

        public virtual DishInfo DishInfo { set; get; }
    }
}
