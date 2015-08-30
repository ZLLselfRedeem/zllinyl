using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL;

namespace VAGastronomistMobileApp.WebPageDll
{
    public class DishAwardOperate
    {
        private readonly DishAwardManager manager = new DishAwardManager();

        /// <summary>
        /// 菜主要信息
        /// </summary>
        /// <param name="DishID"></param>
        /// <param name="DishPriceID"></param>
        /// <returns></returns>
        public PreOrderIn19dian SelectDishInfo(int DishID, int DishPriceID)
        {
            PreOrderIn19dian dish = new PreOrderIn19dian();
            DishAward dishAward = manager.SelectDishInfo(DishID, DishPriceID);
            if (dishAward != null && dishAward.menuId > 0)
            {
                DishTasteAward dishTasteAward = manager.SelectDishTaste(dishAward.menuId, DishPriceID);
                VADishTaste dishTaste = new VADishTaste();
                if (dishTasteAward != null)
                {
                    dishTaste.tasteId = dishTasteAward.tasteId;
                    dishTaste.tasteName = dishTasteAward.tasteName;
                }
                else
                {
                    dishTaste.tasteId = 0;
                    dishTaste.tasteName = ""; 
                }

                List<DishIngredientsAward> ingredientsAward = manager.SelectIngredients(dishAward.menuId, DishPriceID);
                List<VADishIngredients> ingredients = new List<VADishIngredients>();
                if (ingredientsAward != null && ingredientsAward.Any())
                {
                    foreach (DishIngredientsAward item in ingredientsAward)
                    {
                        VADishIngredients ingredient = new VADishIngredients()
                        {
                            ingredientsId = item.ingredientsId,
                            ingredientsName = item.ingredientsName,
                            ingredientsPrice = item.ingredientsPrice,
                            vipDiscountable = item.vipDiscountable
                        };
                        ingredients.Add(ingredient);
                    }
                }
                dish.dishId = DishID;
                dish.dishIngredients = ingredients;
                dish.dishName = "【赠菜】" + dishAward.dishName;
                dish.dishPriceI18nId = dishAward.dishPriceI18nId;
                dish.dishPriceName = dishAward.dishPriceName;
                dish.dishTaste = dishTaste;
                dish.dishTypeName = manager.SelectDishTypeName(dishAward.menuId);
                dish.markName = dishAward.markName;
                dish.quantity = 1;
                dish.unitPrice = 0;
                dish.vipDiscountable = dishAward.vipDiscountable;
            }
            return dish;
        }


    }
}
