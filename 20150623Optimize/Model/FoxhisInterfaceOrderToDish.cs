using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    /// <summary>
    /// 接口落单操作参数
    /// </summary>
   public class FoxhisInterfaceOrderToDish
    {
        public string pc_id { get; set; }
        public string wdtno { get; set; }//每次发送指令的编号，不能重复
        public string empno { get; set; }//服务员
        public string kitprn { get; set; }//是否发送厨房打印
    }
}