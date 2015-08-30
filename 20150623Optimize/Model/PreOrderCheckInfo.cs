using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
   public class PreOrderCheckInfo
    {
        /// <summary>
        /// 预点单审核记录编号
        /// </summary>
        public long id { get; set; }
        /// <summary>
        /// 预点单编号
        /// </summary>
        public long preOrder19dianId { get; set; }
        /// <summary>
        /// 审核人员id
        /// </summary>
        public int employeeId { get; set; }
        /// <summary>
        /// 审核人员名字
        /// </summary>
        public string employeeName { get; set; }
       /// <summary>
        /// 审核人员职位
        /// </summary>
        public string employeePosition { get; set; }
        /// <summary>
        /// 审核时间
        /// </summary>
        public DateTime checkTime { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int status { get; set; }
    }
}