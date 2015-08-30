using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    public class Money19dianDetail
    {
        /// <summary>
        ///  主键
        /// </summary>
        public long id { get; set; }
        /// <summary>
        /// 用户Id
        /// </summary>
        public long customerId { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string changeReason { get; set; }
        /// <summary>
        /// 金额
        /// </summary>
        public double changeValue { get; set; }
        /// <summary>
        /// 当前余额
        /// </summary>
        public double remainMoney { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime changeTime { get; set; }
        /// <summary>
        /// 流水号(11+用户Id)
        /// </summary>
        public string flowNumber { get; set; }
        /// <summary>
        /// 1:支付宝充值，2:银联充值，3：用户退款 4.商户退款 5.财务对账 6.商户结账
        /// </summary>
        public int accountType { get; set; }
        /// <summary>
        /// 来源关联ID(点单ID，回款ID等)
        /// </summary>
        public string accountTypeConnId { get; set; }
        /// <summary>
        /// 收支类型，1：收入，2：支出
        /// </summary>
        public int inoutcomeType { get; set; }
        /// <summary>
        /// 公司
        /// </summary>
        public int companyId { get; set; }
        /// <summary>
        /// 店铺
        /// </summary>
        public int shopId { get; set; }
    }
}