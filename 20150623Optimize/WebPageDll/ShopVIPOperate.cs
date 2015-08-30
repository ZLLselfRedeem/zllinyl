using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL;

namespace VAGastronomistMobileApp.WebPageDll
{
    /// <summary>
    /// 门店VIP用户模型
    /// </summary>
    public class ShopVIPOperate
    {
        private readonly IShopVIPManager shopVIPManager;
        public ShopVIPOperate()
        {
            shopVIPManager = new ShopVIPManager();
        }
        /// <summary>
        /// 记录当前门店VIP用户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool OperateShopVIP(ShopVIP model)
        {
            if (model == null)
            {
                return false;
            }
            if (model.CustomerId <= 0 || model.ShopId <= 0)
            {
                return false;
            }
            ShopOperate shopOper = new ShopOperate();
            ShopInfo shopInfo = shopOper.QueryShop(model.ShopId);
            if (shopInfo == null)
            {
                return false;
            }
            model.CityId = shopInfo.cityID;
            ShopVIP dbModel = shopVIPManager.SelectShopVIP(model.CustomerId, model.ShopId);
            if (dbModel == null || dbModel.Id <= 0)//Insert
            {
                return shopVIPManager.AddShopVIP(model) > 0;
            }
            else//Update
            {
                model.Id = dbModel.Id;
                model.PreOrderTotalQuantity = dbModel.PreOrderTotalQuantity + 1;
                model.PreOrderTotalAmount = dbModel.PreOrderTotalAmount + model.PreOrderTotalAmount;
                return shopVIPManager.UpdateShopVIP(model);
            }
        }
        /// <summary>
        /// 查询门店VIP接口返回信息
        /// </summary>
        /// <param name="shopId"></param>
        /// <param name="cityId"></param>
        /// <returns></returns>
        public ZZB_ShopVipResponse GetZZBShopVipResponse(int shopId, int cityId)
        {
            return shopVIPManager.GetZZBShopVipResponse(shopId, cityId);
        }
        /// <summary>
        /// 查询虚假城市累计会员数量（api调用）
        /// </summary>
        /// <param name="cityId"></param>
        /// <returns></returns>
        public ZZB_ShopVIPFlaseCountModel GetShopVIPFlaseCountModel(int cityId)
        {
            return shopVIPManager.GetShopVIPFlaseCountModel(cityId);
        }

        #region -----------------------------------------------------------------
        public double GetShopVipDiscount(int shopId)
        {
            return shopVIPManager.GetShopVipDiscount(shopId);
        }
        #endregion
    }
}
