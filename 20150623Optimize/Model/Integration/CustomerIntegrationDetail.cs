using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    public class CustomerIntegrationDetail
    {
        /// <summary>
        /// ID
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public long CustomerID { get; set; }

        /// <summary>
        /// 规则ID
        /// </summary>
        public Guid RuleID { get; set; }

        /// <summary>
        /// 各类型表ID
        /// </summary>
        public Guid SubID { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 积分值
        /// </summary>
        public int Integration { get; set; }

        /// <summary>
        /// 当前积分值
        /// </summary>
        public int CurrentIntegration { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public int CreateUser { get; set; }
    }
}
