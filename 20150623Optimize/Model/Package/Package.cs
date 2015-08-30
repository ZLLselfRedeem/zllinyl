using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    public class Package
    {
        /// <summary>
        /// ID
        /// </summary>
        public long ID { get; set; }

        /// <summary>
        /// 套餐名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 位置选择
        /// </summary>
        public int PositionType { get; set; }

        /// <summary>
        /// 距离
        /// </summary>
        public decimal Distance { get; set; }

        /// <summary>
        /// 时间范围
        /// </summary>
        public int TimeRange { get; set; }

        /// <summary>
        /// 消费选择
        /// </summary>
        //public int ConsumptionChoice { get; set; }

        /// <summary>
        /// 客单价要求
        /// </summary>
        public bool ISGuestUnitPrice { get; set; }

        /// <summary>
        /// 最低客单价
        /// </summary>
        public decimal MinGuestUnitPrice { get; set; }

        /// <summary>
        /// 最高客单价
        /// </summary>
        public decimal MaxGuestUnitPrice { get; set; }

        /// <summary>
        /// 计价类型
        /// </summary>
        public int ValuationType { get; set; }

        /// <summary>
        /// 费用
        /// </summary>
        public decimal Cost { get; set; }

        /// <summary>
        /// 购买要求
        /// </summary>
        public int LevelRequirements { get; set; }

        /// <summary>
        /// 发送间隔
        /// </summary>
        public decimal SendLnterval { get; set; }

        /// <summary>
        /// 套餐状态
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// 适用城市
        /// </summary>
        public int ApplicableCity { get; set; }

        /// <summary>
        /// 允许商户筛选点过的菜
        /// </summary>
        public bool EnableFilter { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public int CreateUser{get;set;}

        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 修改人
        /// </summary>
        public int UpdateUser { get; set; }

        /// <summary>
        /// 修改日期
        /// </summary>
        public DateTime UpdateDate { get; set; }

    }
}
