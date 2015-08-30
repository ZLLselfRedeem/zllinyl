using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    /// <summary>
    /// 店铺管理员与店铺关系信息
    /// </summary>
    public class EmployeeConnShop
    {
        /// <summary>
        /// 店铺管理员与店铺关系编号
        /// </summary>
        public int employeeShopID { get; set; }
        /// <summary>
        /// 店铺管理员编号
        /// </summary>
        public int employeeID { get; set; }
        /// <summary>
        /// 店铺编号
        /// </summary>
        public int shopID { get; set; }
        /// <summary>
        /// 公司编号
        /// </summary>
        public int companyID { get; set; }
        /// <summary>
        /// 数据是否有效标志位（1，-1）
        /// </summary>
        public int status { get; set; }
        /// <summary>
        /// 服务员在该门店开始时间（wangcheng 20140222）
        /// </summary>
        public DateTime serviceStartTime { get; set; }
        /// <summary>
        /// 服务在该门店结束时间（wangcheng 20140222）
        /// </summary>
        public DateTime serviceEndTime { get; set; }
    }
}
