using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    /// <summary>
    /// 菜名多语言信息
    /// </summary>
    public class DishI18n
    {
        /// <summary>
        /// 菜多语言编号
        /// </summary>
        public int DishI18nID { get; set; }
        /// <summary>
        /// 对应的菜编号
        /// </summary>
        public int DishID { get; set; }
        /// <summary>
        /// 对应的语言编号
        /// </summary>
        public int LangID { get; set; }
        /// <summary>
        /// 菜的名称
        /// </summary>
        public string DishName { get; set; }
        /// <summary>
        /// 菜的简单描述
        /// </summary>
        public string DishDescShort { get; set; }
        /// <summary>
        /// 菜的详细描述
        /// </summary>
        public string DishDescDetail { get; set; }
        /// <summary>
        /// 菜的历史
        /// </summary>
        public string DishHistory { get; set; }
        /// <summary>
        /// 菜多语言状态
        /// 0：已删除，1：正常
        /// </summary>
        public int DishI18nStatus { get; set; }
        /// <summary>
        /// 菜的全拼
        /// </summary>
        public string dishQuanPin { get; set; }
        /// <summary>
        /// 菜的首字母缩写
        /// </summary>
        public string dishJianPin { get; set; }
    }

    public class DishPraiseModel
    {
        public int DishID { get; set; }
        public int Count { get; set; }
    }
}
