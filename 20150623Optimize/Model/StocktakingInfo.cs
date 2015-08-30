using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    /// <summary> 
    /// FileName: StockTaking.cs 
    /// CLRVersion: 4.0.30319.269 
    /// Author: TDQ 
    /// Corporation:杭州友络科技有限公司
    /// Description: 物资盘存表
    /// DateTime: 2010-12-10 09:55:36 
    /// </summary>
   public class StocktakingInfo
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 仓库编号
        /// </summary>
        public int wareHouseID { get; set; }
        /// <summary>
        /// 原材料ID
        /// </summary>
        public int materialID { get; set; }
        /// <summary>
        /// 盘存时间
        /// </summary>
        public DateTime stockTakingTime { get; set; }
        /// <summary>
        /// 库存数量
        /// </summary>
        public double quantity { get; set; }
        /// <summary>
        /// 盘存数量
        /// </summary>
        public double stockTakingQuantity { get; set; }
        /// <summary>
        /// 盘存人
        /// </summary>
        public string stockTakingManager { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string remark { get; set; }
    }
}
