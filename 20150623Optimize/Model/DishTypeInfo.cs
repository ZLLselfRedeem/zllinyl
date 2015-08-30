using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    /// <summary>
    /// 菜分类信息
    /// </summary>
    public class DishTypeInfo
    {
        /// <summary>
        /// 菜分类编号
        /// </summary>
        public int DishTypeID { get; set; }
        /// <summary>
        /// 分类显示顺序
        /// </summary>
        public int DishTypeSequence { get; set; }
        /// <summary>
        /// 所属菜单编号
        /// </summary>
        public int MenuID { get; set; }
        /// <summary>
        /// 分类状态
        /// 0：已删除，1：正常
        /// </summary>
        public int DishTypeStatus { get; set; }

    }
}
