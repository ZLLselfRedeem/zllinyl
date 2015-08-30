using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    /// <summary>
    /// 菜品基本信息
    /// </summary>
    public class DishInfo
    {
        public DishInfo()
        {
            DiscountTypeID = 0;
            MenuID = 0;
            DishDisplaySequence = 0;
            SendToKitchen = false;
            IsActive = false;
            DishStatus = 0;
            DishTotalQuantity = 0;
        }

        /// <summary>
        /// 菜编号
        /// </summary>
        public int DishID { get; set; }
        /// <summary>
        /// 所属折扣分类编号
        /// </summary>
        public int? DiscountTypeID { get; set; }
        /// <summary>
        /// 所属菜单编号
        /// </summary>
        public int? MenuID { get; set; }
        /// <summary>
        /// 菜的显示顺序
        /// </summary>
        public int? DishDisplaySequence { get; set; }
        /// <summary>
        /// 该菜是否需要发送到厨房
        /// </summary>
        public bool? SendToKitchen { get; set; }
        /// <summary>
        /// 是否显示该菜
        /// </summary>
        public bool? IsActive { get; set; }
        /// <summary>
        /// 菜状态（是否删除）
        /// -1：已删除，1：正常
        /// </summary>
        public int? DishStatus { get; set; }
        /// <summary>
        /// 菜总销量
        /// </summary>
        public double? DishTotalQuantity { get; set; }
        /// <summary>
        /// 当前菜打印机名称
        /// </summary>
        public string cookPrinterName { get; set; }

        //public virtual DishI18n DishI18N { set; get; }

        //public virtual ICollection<ImageInfo> ImageInfos { set; get; }
    }
}
