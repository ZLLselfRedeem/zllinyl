using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    /// <summary>
    /// add by wangc 20140326
    /// 用户充值日志
    /// </summary>
    public class RechargeLog
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long id { get; set; }
        /// <summary>
        /// 操作员工编号
        /// </summary>
        public int employeeId { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime operateTime { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string remark { get; set; }
        /// <summary>
        /// 用户手机号码
        /// </summary>
        public string customerPhone { get; set; }
        /// <summary>
        /// 充值金额
        /// </summary>
        public double amount { get; set; }
        /// <summary>
        /// cookie
        /// </summary>
        public string cookie { get; set; }
        /// <summary>
        /// 充值操作流水状态
        /// </summary>
        public int status { get; set; }
        /// <summary>
        /// 审批时间
        /// </summary>
        public DateTime approvalTime { get; set; }
        /// <summary>
        /// 审批人Id
        /// </summary>
        public int approvalEmployeeId { get; set; }
    }
    public enum RechargeFlowStatus
    {
        审批申请 = 1,
        审批通过 = 2,
        审批不通过 = -1,
    }

    public class RechargeLogPartMolde
    {
        /// <summary>
        /// 用户手机号码
        /// </summary>
        public string customerPhone { get; set; }
        /// <summary>
        /// 充值金额
        /// </summary>
        public double amount { get; set; }
    }
}
