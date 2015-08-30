using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    /// <summary>
    /// 对于点单的操作，点菜，添加，减少，退菜，称重等等
    /// </summary>
    public class FoxhisInterfaceOrderManage
    {
        public string pc_id { get; set; }
        public string base_state { get; set; }//基站号，放1
        public string posno { get; set; }//IPAD编号
        public string pccodes { get; set; }//桌子区域编号
        public string tableno { get; set; }//桌子编号
        public int pluid { get; set; }//菜号
        public decimal number { get; set; }//称重数量
        public decimal order_number { get; set; }//点菜数量
        public string wdtno { get; set; }//每次发送指令的编号，不能重复
        public string remark_ttl { get; set; }//整单备注
        public string remark { get; set; }//单菜备注
        public string cook { get; set; }//制作方法
        public string empno { get; set; }//服务员
        public string shift { get; set; }//班别
        public string unit { get; set; }//规格名称
    }
}
