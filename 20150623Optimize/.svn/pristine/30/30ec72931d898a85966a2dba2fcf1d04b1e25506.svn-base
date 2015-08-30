using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    public class ThirdPartyPaymentInfo
    {
        public PayType Type { set; get; }
        public double Amount { set; get; }
        /// <summary>
        /// 我方生成的第三方支付交易号
        /// </summary>
        public string tradeNo { get; set; }
    }

    public enum PayType
    {
        微信支付 = 1,
        支付宝 = 2,
        银联支付 = 3,
        其他 = 0
    }
}
