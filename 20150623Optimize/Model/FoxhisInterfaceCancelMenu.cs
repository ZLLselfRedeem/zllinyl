using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
   /// <summary>
   /// 西软接口消台输入的参数
   /// </summary>
   public class FoxhisInterfaceCancelMenu
   {
       public string pc_id { get; set; }//
       public string pccodes { get; set; }//--入帐费用码（中餐厅）
       public string tableno { get; set; }//--桌号
       public string empno { get; set; }//
       public string shift { get; set; }//班号
   }
}