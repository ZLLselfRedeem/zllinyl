using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    /// <summary>
    /// 打款记录表
    /// linb 2015-5-5
    /// </summary>
    public class BankMoneyRecord
    {
        /// <summary>
        ///  主键
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        ///  打款详情id
        /// </summary>
        public int BatchMoneyApplyDetail_Id { get; set; }

        /// <summary>
        ///  浦发银行电子凭证号
        /// </summary>
        public string ElecChequeNo { get; set; }

        /// <summary>
        ///  付款帐户
        /// </summary>
        public string AcctNo { get; set; }

        /// <summary>
        ///  付款人姓名
        /// </summary>
        public string AcctName { get; set; }

        /// <summary>
        ///  收款人帐户
        /// </summary>
        public string PayeeAcctNo { get; set; }

        /// <summary>
        ///  收款人姓名
        /// </summary>
        public string PayeeName { get; set; }

        /// <summary>
        ///  支付金额
        /// </summary>
        public double Amount { get; set; }

        /// <summary>
        ///  收款行名称
        /// </summary>
        public string PayeeBankName { get; set; }

        /// <summary>
        ///  收款行地址
        /// </summary>
        public string PayeeAddress { get; set; }

        /// <summary>
        ///  本行它行标志(0本行,1他行)
        /// </summary>
        public int SysFlag { get; set; }

        /// <summary>
        ///  同城异地标志(0同城,1异地)
        /// </summary>
        public int RemitLocation { get; set; }

        /// <summary>
        ///  交易号
        /// </summary>
        public string AcceptNo { get; set; }

        /// <summary>
        ///  柜员流水号
        /// </summary>
        public string SeqNo { get; set; }

        /// <summary>
        ///  交易状态
        /// </summary>
        public int TransStatus { get; set; }

        /// <summary>
        ///  数据状态
        /// </summary>
        public int DataStatus { get; set; }

        /// <summary>
        ///  添加时间
        /// </summary>
        public DateTime AddTime { get; set; }

        /// <summary>
        ///  修改时间
        /// </summary>
        public DateTime ModifyTime { get; set; }

        /// <summary>
        ///  修改人
        /// </summary>
        public string ModifyUser { get; set; }

        /// <summary>
        ///  修改IP
        /// </summary>
        public string ModifyIP { get; set; }
    }
}
