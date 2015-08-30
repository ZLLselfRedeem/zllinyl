using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    /// <summary> 
    /// FileName: CountyInfo.cs 
    /// CLRVersion: 4.0.30319.269 
    /// Author: TDQ 
    /// Corporation:杭州友络科技有限公司
    /// Description: 
    /// DateTime: 2012-05-15 15:28:02 
    /// </summary>
   public class County
    {
        /// <summary>
        /// 县ID
        /// </summary>
       public int countyID { get; set; }
        /// <summary>
        /// 县名称
        /// </summary>
       public string countyName { get; set; }
        /// <summary>
        /// 市ID
        /// </summary>
        public int cityID { get; set; }
    }
}
