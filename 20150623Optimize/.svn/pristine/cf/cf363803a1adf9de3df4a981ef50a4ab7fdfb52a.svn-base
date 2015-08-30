using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL;

namespace VAGastronomistMobileApp.WebPageDll
{
    public class ShopAwardOperate
    {
        private readonly ShopAwardManager shopAwardManager = new ShopAwardManager();
        
        /// <summary>
        /// 获取商家对应的奖品列表
        /// </summary>
        /// <param name="shopID"></param>
        /// <returns></returns>
        public List<ShopAward> SelectShopAwardList(int shopID,int type=1)
        {
            return shopAwardManager.SelectShopAwardList(shopID,type);
        }

        /// <summary>
        /// 查询指定门店指定类别的奖品信息
        /// </summary>
        /// <param name="shopId"></param>
        /// <param name="awardType"></param>
        /// <returns></returns>
        public List<ShopAward> SelectShopAwardList(int shopId, AwardType awardType)
        {
            return shopAwardManager.SelectShopAwardList(shopId, awardType);
        }

        /// <summary>
        /// 查询门店里某个具体的奖品
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ShopAward QueryShopAward(Guid Id)
        {
            return shopAwardManager.QueryShopAward(Id);
        }

        public ShopAward QueryAllShopAward(Guid Id)
        {
            return shopAwardManager.QueryAllShopAward(Id); 
        }

        public List<ShopAward> SelectShopAwardList(string awardIDS)
        {
            return shopAwardManager.SelectShopAwardList(awardIDS);
        }

        public bool InsertShopAward(ShopAward shopAward)
        {
            return shopAwardManager.InsertShopAward(shopAward);
        }

        /// <summary>
        /// 修改奖品
        /// </summary>
        /// <param name="shopAward"></param>
        /// <returns></returns>
        public bool UpdateShopAward(ShopAward shopAward)
        {
            ShopAwardManager shopAwardManager = new ShopAwardManager();
            return shopAwardManager.UpdateShopAward(shopAward);
        }

        public bool UpdateShopAwardOfDish(ShopAward shopAward)
        {
            ShopAwardManager shopAwardManager = new ShopAwardManager();
            using (TransactionScope ts = new TransactionScope())
            {
                //停用原有菜品
                bool disabled = shopAwardManager.DisabledShopAward(shopAward.Id);
                //新增菜品

                shopAward.Id = Guid.NewGuid(); 
                bool insert = shopAwardManager.InsertShopAward(shopAward);

                if (disabled && insert)
                {
                    ts.Complete();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// 根据菜品ID获取菜品名称
        /// </summary>
        /// <param name="dishID"></param>
        /// <returns></returns>
        public  string GetDishNameI18nID(int dishID)
        {
            return shopAwardManager.GetDishNameI18nID(dishID);
        }

        /// <summary>
        /// 获取DISHID
        /// </summary>
        /// <param name="dishI18NID"></param>
        /// <returns></returns>
        public int GetDishID(int dishI18NID)
        {
            return shopAwardManager.GetDishID(dishI18NID);
        }

        /// <summary>
        ///获取菜品图片地址
        /// </summary>
        /// <param name="shopID"></param>
        /// <param name="preOrder19DianID"></param>
        /// <returns></returns>
        public string GetDishPicUrl(int shopID, int dishID)
        {
            return shopAwardManager.GetDishPicUrl(shopID, dishID);
        }

        /// <summary>
        /// 开通或者关闭商家的抽奖功能
        /// </summary>
        /// <param name="shopID">商家ID</param>
        /// <returns></returns>
        public bool UpdateShopAwardEnable(int shopID,int enable,int type=0)
        {
            ShopAwardManager shopAwardManager = new ShopAwardManager();
            return shopAwardManager.UpdateShopAwardEnable(shopID, enable,type);
        }

        public bool UpdateShopAwardEnable(int shopID)
        {
            ShopAwardManager shopAwardManager = new ShopAwardManager();
            return shopAwardManager.UpdateShopAwardEnable(shopID);
        }

        /// <summary>
        /// 商户活动查询 
        /// </summary>
        /// <param name="cityID"></param>
        /// <param name="shopName"></param>
        /// <param name="changeStatus"></param>
        /// <param name="beginDateTimeValue"></param>
        /// <param name="endDateTimeValue"></param>
        /// <returns></returns>
        public DataTable SelectBussinessActivity(string cityID, string shopName, string changeStatus, DateTime beginDateTimeValue, DateTime endDateTimeValue, string shopID)
        {
            return shopAwardManager.SelectBussinessActivity(cityID, shopName, changeStatus, beginDateTimeValue, endDateTimeValue,shopID);
        }

        public DataTable SelectBussinessActivityTotal(string cityID,string shopName,DateTime beginDateTimeValue,DateTime endDateTimeValue,string shopID,int type)
        {
            return shopAwardManager.SelectBussinessActivityTotal(cityID, shopName,beginDateTimeValue,endDateTimeValue,shopID,type);
        }

        /// <summary>
        /// 删除店铺奖品
        /// </summary>
        /// <param name="awardID"></param>
        /// <returns></returns>
        public bool DeleteShopAward(Guid awardID)
        {
            return shopAwardManager.DeleteShopAward(awardID);
        }

        /// <summary>
        /// 获取商家对应的奖品类别
        /// </summary>
        /// <param name="shopID"></param>
        /// <returns></returns>
        public bool SelectShopAwardType(int shopID, AwardType awardType)
        {
            return shopAwardManager.SelectShopAwardType(shopID, awardType);
        }
    }
}
