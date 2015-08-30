using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    /// <summary>
    /// 2011年12月21日
    /// tdq
    /// 排队信息
    /// </summary>
  public class QueueInfo
    {
        /// <summary>
        /// 排队编号
        /// </summary>
      public int CallNumberID { get; set; }
        /// <summary>
        /// 排队桌号
        /// </summary>
        public int TableID { get; set; }
        /// <summary>
        /// 排队密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 排队人电话
        /// </summary>
        public string QueuePhone { get; set; }
        /// <summary>
        /// 排队时间
        /// </summary>
        public DateTime CreationTime { get; set; }
        /// <summary>
        /// 过号时间
        /// </summary>
        public DateTime CallTime { get; set; }
        /// 排队人数
        /// </summary>
        public int PeopleNumber { get; set; }
        /// <summary>
        /// 排队单状态
        /// </summary>
        public int QueueStatus { get; set; }
        /// <summary>
        /// 排队对应点单编号
        /// </summary>
        public int OrderID { get; set; }
        /// <summary>
        /// 显示号码（A23）
        /// </summary>
        public string DisplayName { get; set; }
        /// <summary>
        /// 对应的排队队列号
        /// </summary>
        public int WaitingListID { get; set; }
        /// <summary>
        /// 是否已经不在当前时段
        /// </summary>
        public bool IsActive { get; set; }
    }
}
