using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    /// <summary>
    /// 菜名与菜显示分类关系信息
    /// </summary>
    public class DishConnType
    {
        /// <summary>
        /// 菜名与菜显示分类关系编号
        /// </summary>
        public int DishConnTypeID { get; set; }
        /// <summary>
        /// 菜编号
        /// </summary>
        public int DishID { get; set; }
        /// <summary>
        /// 菜显示分类编号
        /// </summary>
        public int DishTypeID { get; set; }
        /// <summary>
        /// 菜名与菜显示分类关系状态
        /// -1：已删除，1：正常
        /// </summary>
        public int DishConnTypeStatus { get; set; }
    }
}
