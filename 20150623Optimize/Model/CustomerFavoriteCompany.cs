using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    /// <summary>
    /// 用户收藏公司信息
    /// </summary>
    public class CustomerFavoriteCompany
    {
        /// <summary>
        /// 编号
        /// </summary>
        public long id { get; set; }
        /// <summary>
        /// 用户编号
        /// </summary>
        public long customerId { get; set; }
        /// <summary>
        /// 公司编号
        /// </summary>
        public int companyId { get; set; }
        /// <summary>
        /// 收藏时间
        /// </summary>
        public DateTime collectTime { get; set; }
        /// <summary>
        /// 门店编号
        /// </summary>
        public int shopId { get; set; }
    }
}
