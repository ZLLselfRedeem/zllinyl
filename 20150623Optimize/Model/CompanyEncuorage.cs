using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    /// <summary>
    /// wangcheng(公司投放优惠券奖励)
    /// </summary>
    public class CompanyEncourage
    {
        /// <summary>
        /// 编号
        /// </summary>
        public long id { get; set; }
        /// <summary>
        /// 奖励类型
        /// </summary>
        public VACustomEncourageType type { get; set; }
        /// <summary>
        /// 奖励额度
        /// </summary>
        public string value { get; set; }
        /// <summary>
        /// 奖励原因
        /// 此项对应Money19dianDetail中的changeReason
        /// </summary>
        public string reason { get; set; }
        /// <summary>
        /// 奖励描述
        /// </summary>
        public string description { get; set; }
        /// <summary>
        /// 奖励创建时间
        /// </summary>
        public DateTime createTime { get; set; }
        /// <summary>
        /// 奖励创建人编号
        /// 对应EmployeeInfo中的EmployeeID
        /// </summary>
        public int creater { get; set; }
        /// <summary>
        /// 该活动所属公司
        /// </summary>
        public int companyId { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int status { get; set; }
    }
}
