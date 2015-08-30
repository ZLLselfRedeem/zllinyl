using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    /// <summary>
    /// 门店奖品设置
    /// </summary>
    public class ShopAward
    {
        /// <summary>
        /// 奖品ID
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 门店ID
        /// </summary>
        public int ShopId { get; set; }
        /// <summary>
        /// 奖品类别（2.免排队；3.菜；）
        /// </summary>
        public AwardType AwardType { get; set; }

        /// <summary>
        /// 菜品ID
        /// </summary>
        public int DishId { get; set; }

        /// <summary>
        /// 菜品价格ID
        /// </summary>
        public int DishPriceId { get; set; }

        /// <summary>
        /// 第三方奖品名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 数量（免排队，菜）
        /// </summary>
        public int Count { get; set; }
        /// <summary>
        /// 补贴金额
        /// </summary>
        public decimal SubsidyAmount { get; set; }
        /// <summary>
        /// 概率（备用）
        /// </summary>
        public int Probability { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool Enable { get; set; }
        /// <summary>
        /// 数据是否有效
        /// </summary>
        public bool Status { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 创建者
        /// </summary>
        public string CreatedBy { get; set; }
        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime LastUpdateTime { get; set; }
        /// <summary>
        /// 最后修改者
        /// </summary>
        public string LastUpdatedBy { get; set; }

    }

    /// <summary>
    /// 奖品对应的概率占比
    /// </summary>
    public class AwardRate
    {
        public AwardType awardType { get; set; }
        /// <summary>
        /// 中奖概率
        /// </summary>
        public int rate { get; set; }
    }
}
