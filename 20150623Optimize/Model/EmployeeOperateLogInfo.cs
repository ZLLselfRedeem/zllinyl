using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    /// <summary>
    /// 员工操作日志记录
    /// </summary>
    public class EmployeeOperateLogInfo
    {
        /// <summary>
        /// 员工编号
        /// </summary>
        public int employeeId { get; set; }
        /// <summary>
        /// 员工姓名
        /// </summary>
        public string employeeName { get; set; }
        /// <summary>
        /// 页面类型
        /// </summary>
        public int pageType { get; set; }
        /// <summary>
        /// 操作类型
        /// </summary>
        public int operateType { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime operateTime { get; set; }
        /// <summary>
        /// 操作描述
        /// </summary>
        public string operateDes { get; set; }
    }
}
