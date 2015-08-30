using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    /// <summary>
    /// 西软接口开台输入的参数
    /// </summary>
    public class FoxhisInterfaceInsertMenu
    {
        public string pc_id { get; set; }//
        public string pccodes { get; set; }//--入帐费用码（中餐厅）
        public string tableno { get; set; }//--桌号
        public int gstno { get; set; }//--人数
        public string waiter { get; set; }//
        public string empno { get; set; }//
        public string shift { get; set; }//班号
    }
}
