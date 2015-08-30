using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    /// <summary>
    /// 菜分类多语言信息
    /// </summary>
    public class DishTypeI18n
    {
        /// <summary>
        /// 菜分类多语言编号
        /// </summary>
        public int DishTypeI18nID { get; set; }
        /// <summary>
        /// 菜分类名称
        /// </summary>
        public string DishTypeName { get; set; }
        /// <summary>
        /// 对应分类编号
        /// </summary>
        public int DishTypeID { get; set; }
        /// <summary>
        /// 对应语言编号
        /// </summary>
        public int LangID { get; set; }
        /// <summary>
        /// 状态
        /// 0：已删除，1：正常
        /// </summary>
        public int DishTypeI18nStatus { get; set; }
    }
}
