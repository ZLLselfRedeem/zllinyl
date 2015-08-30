using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using VAGastronomistMobileApp.WebPageDll;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL;
using System.Text;
using System.Web.Services;
using VA.CacheLogic.OrderClient;
using VAGastronomistMobileApp.Model.QueryObject;
using VAGastronomistMobileApp.Model.Adjunction;
using VAGastronomistMobileApp.WebPageDll.Adjunction;

public partial class shopShow : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //GetShopInfo(151, 1, 2, 760354);
    }

    [WebMethod]
    public static string GetShopInfo(int shopID, int pageIndex, int pageSize, long customerId)
    {
        var shopResponse = new VAClientShopDetailResponse();
        ShopInfoCacheLogic shopInfoCacheLogic = new ShopInfoCacheLogic();
        ShopInfo shopInfo = shopInfoCacheLogic.GetShopInfo(shopID);
        MenuOperate menuOperate = new MenuOperate();
        if (shopInfo != null)
        {
            ShopManager shopmanager = new ShopManager();
            CustomerManager customerManager = new CustomerManager();
            #region 加载店铺信息
            string imagePath = WebConfig.CdnDomain + WebConfig.ImagePath;
            string shopImagePath = imagePath + shopInfo.shopImagePath + WebConfig.ShopImg;
            //VAAppType appType = (VAAppType)checkResult.dtCustomer.Rows[0]["appType"];
            //long CustomerID = Common.ToInt64(checkResult.dtCustomer.Rows[0]["CustomerID"]);
            DataTable dtshopImage = shopmanager.QueryImageURL(shopID);
            List<string> imglist = new List<string>();
            if (dtshopImage.Rows.Count > 0)
            {
                for (int i = 0; i < dtshopImage.Rows.Count; i++)
                {
                    imglist.Add(shopImagePath + Common.ToString(dtshopImage.Rows[i]["revealImageName"]));
                }
            }
            shopResponse.shopImage = imglist;//门店图片地址
            shopResponse.shopId = shopInfo.shopID;
            if (!string.IsNullOrEmpty(shopInfo.publicityPhotoPath))
            {
                shopResponse.publicityPhotoPath = imagePath + shopInfo.publicityPhotoPath;
            }
            else
            {
                shopResponse.publicityPhotoPath = "";
            }
            shopResponse.shopName = shopInfo.shopName;
            shopResponse.shopRating = shopInfo.shopRating.HasValue ? shopInfo.shopRating.Value : 0;
            shopResponse.shopAddress = shopInfo.shopAddress;
            shopResponse.shopTelephone = shopInfo.shopTelephone;
            shopResponse.openTimes = shopInfo.openTimes;
            if (string.IsNullOrEmpty(shopInfo.shopLogo))//门店LOGO
            {
                shopResponse.shopLogoPath = "";
            }
            else
            {
                shopResponse.shopLogoPath = imagePath + shopInfo.shopImagePath + shopInfo.shopLogo;
            }
            shopResponse.userShareUrl = WebConfig.ServerDomain + "shopShow.aspx?shopId=" + shopID;//用户分享门店信息
            string userShareMessage = string.Empty;
            userShareMessage = WebConfig.ShopShare;
            userShareMessage = userShareMessage.Replace("{0}", shopInfo.shopName);
            shopResponse.userShareMessage = userShareMessage;
            //int currentPlatformVipGrade = Common.ToInt32(checkResult.dtCustomer.Rows[0]["currentPlatformVipGrade"]);//当前用户VIP等级 
            ShopCoordinate shopCoordinateBaidu = shopInfoCacheLogic.GetShopCoordinate(shopID);//百度经纬度
            shopResponse.latitude = shopCoordinateBaidu.latitude;
            shopResponse.longitude = shopCoordinateBaidu.longitude;
            DataTable dtShopVipInfo = shopmanager.SelectShopVipInfo(shopInfo.shopID);//查询当前门店的VIP等级信息
            //DataTable dtShopVipInfo = menuOperate.GetShopVipInfo(shopInfo.shopID);

            shopResponse.currectDiscount = 1;
            //List<int> shopIdList = customerManager.SelectCustomerFavoriteShop(CustomerID);
            //if (shopIdList.Contains(shopInfo.shopID))
            //{
            //    shopResponse.isFavorites = true;
            //}
            //else
            //{
            shopResponse.isFavorites = false;
            //}
            shopResponse.shopLevel = shopInfo.shopLevel;
            shopResponse.prepayOrderCount = (int)shopInfo.prepayOrderCount;
            ShopEvaluationDetailManager shopEvaluationDetailManager = new ShopEvaluationDetailManager();
            ShopEvaluationDetailQueryObject shopEvaluationDetailQueryObject = new VAGastronomistMobileApp.Model.ShopEvaluationDetailQueryObject()
            {
                ShopId = shopID
            };
            List<ShopEvaluationDetail> shopEvaluationDetailList =
                shopEvaluationDetailManager.GetShopEvaluationDetailByQuery(shopEvaluationDetailQueryObject);
            shopResponse.goodEvaluationCount = shopEvaluationDetailList.Where(p => p.EvaluationValue == 1).Sum(p => p.EvaluationCount);
            if (shopEvaluationDetailList != null && shopEvaluationDetailList.Count > 0)
            {
                double totalEvaluation = shopEvaluationDetailList.Sum(p => p.EvaluationCount);
                var evaluationPercent = from e in shopEvaluationDetailList
                                        group e by e.EvaluationValue into g
                                        select new EvaluationPercent()
                                        {
                                            evaluationValue = g.Key,
                                            percent = Math.Round((g.Sum(p => p.EvaluationCount) / totalEvaluation), 4)
                                        };
                if (evaluationPercent != null && evaluationPercent.Count() >= 0)
                {
                    shopResponse.evaluationPercent = evaluationPercent.ToList();
                    if (evaluationPercent.Count() < 3)
                    {
                        for (int i = -1; i < 2; i++)
                        {
                            EvaluationPercent entity = evaluationPercent.FirstOrDefault(p => p.evaluationValue == i);
                            if (entity == null)
                            {
                                entity = new EvaluationPercent() { evaluationValue = i, percent = 0 };
                                shopResponse.evaluationPercent.Add(entity);
                            }
                        }
                    }
                }
            }

            #endregion
            if (pageIndex > 0)
            {
                #region 加载店铺评价
                PreorderEvaluationQueryObject queryObject = new PreorderEvaluationQueryObject() { ShopId = shopInfo.shopID };
                long queryCount = PreorderEvaluationOperate.GetCountByQuery(queryObject);
                if (queryCount > pageIndex * pageSize)
                {
                    shopResponse.isMore = true;
                }
                else
                {
                    shopResponse.isMore = false;
                }
                List<PreorderEvaluation> preOrder19dianInfoList
                    = PreorderEvaluationOperate.GetListByQuery(pageSize, pageIndex, queryObject);
                if (preOrder19dianInfoList != null && preOrder19dianInfoList.Count > 0)
                {
                    var returnEvaluationList = from e in preOrder19dianInfoList
                                               select new EvaluationInfo()
                                               {
                                                   customId = (int)e.CustomerId,
                                                   evaluationContent = e.EvaluationContent,
                                                   evaluationValue = e.EvaluationLevel,
                                                   evaluationDate = Common.ToSecondFrom1970(e.EvaluationTime)
                                               };
                    shopResponse.evaluationList = returnEvaluationList.ToList();
                    foreach (var item in shopResponse.evaluationList)
                    {
                        item.mobilePhoneNumber = string.Empty;
                        DataTable dtCustomer = customerManager.SelectCustomer(item.customId);
                        if (dtCustomer != null && dtCustomer.Rows.Count > 0)
                        {
                            string mobilePhoneNumber = dtCustomer.Rows[0]["mobilePhoneNumber"].ToString();
                            if (mobilePhoneNumber.Length > 10)
                            {
                                item.mobilePhoneNumber = dtCustomer.Rows[0]["mobilePhoneNumber"].ToString().Remove(3, 6).Insert(3, "******");
                            }
                        }
                    }

                }

                #endregion
            }

            shopResponse.result = VAResult.VA_OK;
        }
        return JsonOperate.JsonSerializer<VAClientShopDetailResponse>(shopResponse);
    }
}