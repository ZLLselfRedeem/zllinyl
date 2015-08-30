using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    /// <summary> 
    /// FileName: FoxhisInterface.cs 
    /// CLRVersion: 4.0.30319.269 
    /// Author: TDQ 
    /// Corporation:杭州友络科技有限公司
    /// Description: 
    /// DateTime: 2012-06-04 15:14:44 
    /// </summary>
    public class FoxhisInterfaceInfo
    {
        public string menu { get; set; }//--对方单号
        public string id { get; set; }//
        public string pc_id { get; set; }//
        public string shift { get; set; }//
        public string empno { get; set; }//
        public string pccode { get; set; }//--入帐费用码
        public string paycode { get; set; }//--付款方式
        public string accnt { get; set; }//--帐号
        public string roomno { get; set; }//
        public string guestno { get; set; }//
        public string tableno { get; set; }//--桌号
        public int guests { get; set; }//--人数
        public decimal amount { get; set; }//--总金额
        public decimal srv { get; set; }//--服务费
        public decimal dsc { get; set; }//--折扣
        public decimal tax { get; set; }//--税
        public decimal food { get; set; }//--食品金额
        public decimal drink { get; set; }//-酒水金额
        public decimal cig { get; set; }//--香烟金额
        public decimal other1 { get; set; }//--其他金额1
        public decimal other2 { get; set; }//--其他金额2
        public decimal other3 { get; set; }//--其他金额3
        public string option { get; set; }//
        public string remark { get; set; }//
        public string vipno { get; set; }//
        public string cusno { get; set; }//
        public string saleid { get; set; }//
    }
}
