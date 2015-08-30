using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    /// <summary> 
    /// FileName: UnitInfo.cs 
    /// CLRVersion: 4.0.30319.269 
    /// Author: TDQ 
    /// Corporation:杭州友络科技有限公司
    /// Description: 计量单位表
    /// DateTime: 2011-04-07 10:27:49 
    /// </summary>
   public class UnitInfo
    {
        /// <summary>
        /// 计量单位编号
        /// </summary>
        public int unitID { get; set; }
        /// <summary>
        /// 计量单位名称
        /// </summary>
        public string unitName { get; set; }
        /// <summary>
        /// 计量单位状态
        /// </summary>
        public int unitStatus { get; set; }
    }
}
