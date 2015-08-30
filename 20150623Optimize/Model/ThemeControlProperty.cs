using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    /// <summary> 
    /// FileName: ThemeControlProperty.cs 
    /// CLRVersion: 4.0.30319.239 
    /// Author: TDQ 
    /// Corporation:杭州友络科技有限公司
    /// Description: 控件的属性
    /// DateTime: 2012/1/10 17:04:34 
    /// </summary>
   public class ThemeControlProperty
    {
        /// <summary>
        /// 属性编号
        /// </summary>
       public int TControlPropertyID { get; set; }
        /// <summary>
        /// 所属控件
        /// </summary>
       public int TControlID { get; set; }
       /// <summary>
       /// 控件属性id
       /// </summary>
       public string TPropertyID { get; set; }
        /// <summary>
        /// 控件属性值
        /// </summary>
       public string TPropertyValue { get; set; }
    }
}
