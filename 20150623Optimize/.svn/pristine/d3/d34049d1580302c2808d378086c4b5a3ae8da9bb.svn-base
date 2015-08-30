using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    /// <summary>
    /// 批量打款日志表
    /// </summary>
    public class StoresMoneyLog
    {
        /// <summary>
        /// 主键
        /// </summary>
        public Int32 Id {get;set;}
        /// <summary>
        /// 门店ID
        /// </summary>
        public Int32 ShopInfo_ShopID { get; set; }
        /// <summary>
        /// 平账单据ID
        /// </summary>
        public long MoneyMerchantAccountDetail_AccountId { get; set; }
        /// <summary>
        /// 批量打款主表ID
        /// </summary>
        public Int32 BatchMoneyApply_Id { get; set; }
        /// <summary>
        /// 批量打款明细表ID（批量操作时串起来保存用,隔开）
        /// </summary>
        public string BatchMoneyApplyDetail_Id { get; set; }
        /// <summary>
        /// 金额
        /// </summary>
        public double Money { get; set; }
        /// <summary>
        /// 操作类型说明（生成，提交，审核，撤回，删除等）
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime AddTime { get; set; }
        /// <summary>
        /// 操作人
        /// </summary>
        public string AddUser { get; set; }
        /// <summary>
        /// 操作人IP
        /// </summary>
        public string AddIP { get; set; }
    }
}
