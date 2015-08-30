using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    /// <summary>
    /// 积分商城：兑换记录操作日志
    /// 2014-2-26 jinyanni
    /// </summary>
    public class PointManageLog
    {
        /// <summary>
        /// 操作日志ID
        /// </summary>
        public long id { get; set; }
        /// <summary>
        /// 服务员积分变动ID
        /// </summary>
        public long pointLogId { get; set; }
        /// <summary>
        /// 操作内容
        /// </summary>
        public string remark { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime createTime { get; set; }
        /// <summary>
        /// 创建者
        /// </summary>
        public int createdBy { get; set; }
        /// <summary>
        /// 数据是否有效标识
        /// </summary>
        public int status { get; set; }
    }
}
