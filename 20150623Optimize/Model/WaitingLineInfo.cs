using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    /// <summary> 
    /// FileName: QueueType.cs 
    /// CLRVersion: 4.0.30319.239 
    /// Author: TDQ 
    /// Corporation:杭州友络科技有限公司
    /// Description: 
    /// DateTime: 2011/12/26 14:28:31 
    /// </summary>
   public class WaitingLineInfo
    {
        /// <summary>
        /// 队列id
        /// </summary>
       public int WaitingLineID { get; set; }
        /// <summary>
        /// 排队名称
        /// </summary>
       public string WaitingLineName { get; set; }
        /// <summary>
        /// 座位数下限
        /// </summary>
        public int SeatsMin { get; set; }
        /// <summary>
        /// 座位数上限
        /// </summary>
        public int SeatsMax { get; set; }
        /// <summary>
        /// 当前排号
        /// </summary>
        public int DisplayNumber { get; set; }
    }
}
