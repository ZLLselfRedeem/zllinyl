using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    /// <summary>
    /// 餐桌预定信息
    /// </summary>
    public class ReserveInfo
    {
        /// <summary>
        /// 预定编号
        /// </summary>
        public int ReserveID { get; set; }
        /// <summary>
        /// 预定桌号
        /// </summary>
        public int TableID { get; set; }
        /// <summary>
        /// 预定人姓名
        /// </summary>
        public string ReserveName { get; set; }
        /// <summary>
        /// 预定人电话
        /// </summary>
        public string ReservePhone { get; set; }
        /// <summary>
        /// 预定时间
        /// </summary>
        public DateTime ReserveDate { get; set; }
        /// <summary>
        /// 就餐时段编号（早餐，午餐，晚餐，夜宵等的编号）
        /// </summary>
        public int DinnerTimeID { get; set; }
        /// <summary>
        /// 预定人数
        /// </summary>
        public int PeopleNumber { get; set; }
        /// <summary>
        /// 预定单状态
        /// -1:已删除，1：正常，其他？
        /// </summary>
        public int ReserveStatus { get; set; }
        /// <summary>
        /// 预定对应点单编号
        /// </summary>
        public int OrderID { get; set; }
        /// <summary>
        /// 预定对应的员工用户名
        /// </summary>
        public string EmployeeUserName { get; set; }
        /// <summary>
        /// 预定开始时间
        /// </summary>
        public DateTime ReserveStartTime { get; set; }
        /// <summary>
        /// 预定结束时间
        /// </summary>
        public DateTime ReserveEndTime { get; set; }

    }
}
