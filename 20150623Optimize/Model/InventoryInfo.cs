using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    /// <summary>
    /// 库存物品信息
    /// </summary>
    public class InventoryInfo
    {
        /// <summary>
        /// 库存物品编号
        /// </summary>
        public int inventoryID { get; set; }
        /// <summary>
        /// 库存物品名称
        /// </summary>
        public string inventoryName { get; set; }
        /// <summary>
        /// 库存物品数量
        /// </summary>
        public double inventoryNumber { get; set; }
        /// <summary>
        /// 库存物品单位
        /// </summary>
        public int inventoryUnit { get; set; }
        /// <summary>
        /// 库存物品分类编号
        /// </summary>
        public int inventoryTypeID { get; set; }
        /// <summary>
        /// 库存物品状态
        /// </summary>
        public int inventoryStatus { get; set; }
        /// <summary>
        /// 原材料ID
        /// </summary>
        public int materialID { get; set; }
    }
}
