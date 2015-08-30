using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    /// <summary> 
    /// FileName: ThemeControl.cs 
    /// CLRVersion: 4.0.30319.239 
    /// Author: TDQ 
    /// Corporation:杭州友络科技有限公司
    /// Description: 控制ipad端控件样式
    /// DateTime: 2012/1/10 16:55:25 
    /// </summary>
   public class ThemeControl
    {
        /// <summary>
        /// 控件编号
        /// </summary>
       public int TControlID { get; set; }
       /// <summary>
       /// 所属页面
       /// </summary>
       public int TPageID { get; set; }
        /// <summary>
        /// 控件显示名称
        /// </summary>
       public string TControlDisplayName { get; set; }
        /// <summary>
        /// 控件名称
        /// </summary>
       public string TControlName { get; set; }
       /// <summary>
       /// 控件类型
       /// </summary>
       public string TControlType { get; set; }
       /// <summary>
       /// 控件父容器
       /// </summary>
       public int TParentControlID { get; set; }
    }
}
