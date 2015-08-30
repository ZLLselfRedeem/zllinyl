using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    /// <summary>
    /// 用户等级与折扣分类关系信息
    /// </summary>
    public class RankConnDiscount
    {
        /// <summary>
        /// 用户等级与折扣分类关系编号
        /// </summary>
        public int RankConnDisID { get; set; }
        /// <summary>
        /// 用户等级编号
        /// </summary>
        public int CustomerRankID { get; set; }
        /// <summary>
        /// 菜折扣分类编号
        /// </summary>
        public int DiscountTypeID { get; set; }
        /// <summary>
        /// 对应的具体折扣值
        /// </summary>
        public double DiscountValue { get; set; }
        /// <summary>
        /// 用户等级与折扣分类关系状态
        /// </summary>
        public int RankConnDisStatus { get; set; }
    }
}
