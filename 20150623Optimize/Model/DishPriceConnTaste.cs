using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    /// <summary>
    /// 功能描述:规格对应口味
    /// 创建标识:罗国华 20131104
    /// </summary>
    public class DishPriceConnTaste
    {
        /// <summary>
        /// 规格Id
        /// </summary>
        public int dishPriceId { get; set; }
        /// <summary>
        /// 口味Id
        /// </summary>
        public int tasteId { get; set; }
    }
}