using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL;

namespace VAGastronomistMobileApp.WebPageDll
{
    public class ShopEvaluationDetailOperate
    {
        public ShopEvaluationDetail GetShopEvaluationDetailByID(int shopEvaluationDetailID)
        {
            ShopEvaluationDetailManager shopEvaluationDetailManager = new ShopEvaluationDetailManager();

            return shopEvaluationDetailManager.GetShopEvaluationDetailByID(shopEvaluationDetailID);;
        }

        public bool AddShopEvaluationDetail(ShopEvaluationDetail shopEvaluationDetail)
        {
            ShopEvaluationDetailManager shopEvaluationDetailManager = new ShopEvaluationDetailManager();

            return shopEvaluationDetailManager.AddShopEvaluationDetail(shopEvaluationDetail); ;
        }
        public bool UpdateShopEvaluationDetail(ShopEvaluationDetail shopEvaluationDetail)
        {
            ShopEvaluationDetailManager shopEvaluationDetailManager = new ShopEvaluationDetailManager();

            return shopEvaluationDetailManager.UpdateShopEvaluationDetail(shopEvaluationDetail); ;
        }
        public  List<ShopEvaluationDetail> GetShopEvaluationDetailByQuery(ShopEvaluationDetailQueryObject queryObject)
        {
            ShopEvaluationDetailManager shopEvaluationDetailManager = new ShopEvaluationDetailManager();

            return shopEvaluationDetailManager.GetShopEvaluationDetailByQuery(queryObject); 
        }
    }
}
