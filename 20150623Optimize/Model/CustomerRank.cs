using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    /// <summary>
    /// 用户等级信息
    /// </summary>
    public class CustomerRank
    {
        /// <summary>
        /// 用户等级编号
        /// </summary>
        public int CustomerRankID { get; set; }
        /// <summary>
        /// 用户等级名称
        /// </summary>
        public string CustomerRankName { get; set; }
        /// <summary>
        /// 用户等级显示顺序
        /// </summary>
        public int CustomerRankSequence { get; set; }
        /// <summary>
        /// 用户等级状态
        /// -1:已删除，1：正常
        /// </summary>
        public int CustomerRankStatus { get; set; }
    }
}
