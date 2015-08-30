using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    public class DishAward
    {
        public int dishId { set; get; }

        public string dishName { get; set; }
        public double unitPrice { get; set; }
        public int quantity { get; set; }
        public string dishPriceName { get; set; }
        public int dishPriceI18nId { get; set; }
        public string dishTypeName { get; set; }
        //wangcheng
        public string markName { get; set; }

        public bool vipDiscountable { get; set; }
        public int menuId { get; set; }
        //口味，配料
        public DishTasteAward dishTaste { get; set; }//口味
        public List<DishIngredientsAward> dishIngredients { get; set; }//配料
    }

    /// <summary>
    /// 口味
    /// </summary>
    public class DishTasteAward
    {
        public int tasteId { get; set; }//口味ID
        public string tasteName { get; set; }//口味名称
    }
    /// <summary>
    /// 配料
    /// </summary>
    public class DishIngredientsAward
    {
        public int ingredientsId { get; set; }//配料ID
        public string ingredientsName { get; set; }//配料名称
        public double ingredientsPrice { get; set; }//配料价格
        public int quantity { get; set; }//该项数量
        public string vipDiscountable { get; set; }
    }
}
