using System;

namespace VAGastronomistMobileApp.Model
{
    public class AccountDetailsQueryItemEntity
    {
        /// <summary>
        /// 凭证号
        /// </summary>
        public string VoucherNo { set; get; }

        /// <summary>
        /// 交易流水号
        /// </summary>
        public string SeqNo { set; get; }

        /// <summary>
        /// 交易金额
        /// </summary>
        public decimal TxAmount { set; get; }

        /// <summary>
        /// 帐户金额
        /// </summary>
        public decimal Balance { set; get; }

        /// <summary>
        /// 借贷标志 0借，1贷
        /// </summary>
        public int TranFlag { set; get; }

        /// <summary>
        /// 交易日期
        /// </summary>
        public string TransDate { set; get; }

        /// <summary>
        /// 交易时间
        /// </summary>
        public string TransTime { set; get; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Note { set; get; }

        /// <summary>
        /// 摘要代码
        /// </summary>
        public string Remark { set; get; }

        /// <summary>
        /// 对方行号
        /// </summary>
        public string PayeeBankNo { set; get; }

        /// <summary>
        /// 对方行名
        /// </summary>
        public string PayeeBankName { set; get; }

        /// <summary>
        /// 对方帐户
        /// </summary>
        public string PayeeAcctNo { set; get; }

        /// <summary>
        /// 对方户名
        /// </summary>
        public string PayeeName { set;get;}

        /// <summary>
        /// 交易代码
        /// </summary>
        public string TransCode { set; get; }

        /// <summary>
        /// 分行id
        /// </summary>
        public string BranchId { set; get; }

        /// <summary>
        /// 客户账户
        /// </summary>
        public string CustomerAcctNo { set; get; }

        /// <summary>
        /// 对方帐户类型
        /// </summary>
        public string PayeeAcctType { set; get; }

        /// <summary>
        /// 交易柜员
        /// </summary>
        public string TransCounter { set; get; }

        /// <summary>
        /// 授权柜员
        /// </summary>
        public string AuthCounter { set; get; }

        /// <summary>
        /// 备用域10位
        /// </summary>
        public string OtherChar10 { set; get; }

        /// <summary>
        /// 备用域40位
        /// </summary>
        public string OtherChar40 { set; get; }

        /// <summary>
        /// 传票序号
        /// </summary>
        public string SeqNum { set; get; }

        /// <summary>
        /// 冲补标志
        /// </summary>
        public string RevFlag { set; get; }
    }
}
