using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    /// <summary>
    /// 挂账人信息
    /// </summary>
    public class CityLedgerCustomerInfo
    {
        /// <summary>
        /// 挂账人编号
        /// </summary>
        public int cityLedgerCustomerID { get; set; }
        /// <summary>
        /// 挂账单位编号
        /// </summary>
        public int cityLedgerCompanyID { get; set; }
        /// <summary>
        /// 挂账人名称
        /// </summary>
        public string cityLedgerCustomerName { get; set; }
        /// <summary>
        /// 该挂账人允许挂账金额
        /// </summary>
        public double allowAmount { get; set; }
        /// <summary>
        /// 该挂账人已挂账金额
        /// </summary>
        public double currentAmount { get; set; }
        /// <summary>
        /// 该挂账人创建时间
        /// </summary>
        public DateTime creatTime { get; set; }
        /// <summary>
        /// 该挂账人结算时间
        /// </summary>
        public DateTime lastPaidTime { get; set; }
        /// <summary>
        /// 挂账人电话号码
        /// </summary>
        public string phone { get; set; }
        /// <summary>
        /// 挂账人备注
        /// </summary>
        public string note { get; set; }
        /// <summary>
        /// 挂账人状态
        /// </summary>
        public int status { get; set; }
    }
}
