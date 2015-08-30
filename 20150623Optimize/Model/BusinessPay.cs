using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    public class BusinessPay
    {
        public int companyId { get; set; }//公司ID
        public int shopId { get; set; }//店铺ID
        public double pay { get; set; }//提款金额
        public DateTime paytime { get; set; }//提款时间
        public long id { get; set; }
        public string paymentId { get; set; }//打款id
    }
}
