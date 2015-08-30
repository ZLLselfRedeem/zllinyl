using Aliyun.OpenServices.OpenStorageService;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using VA.Cache.Distributed;
using VA.Cache.HttpRuntime;
using VA.CacheLogic.OrderClient;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL;
using VAGastronomistMobileApp.SQLServerDAL.Persistence;
using VAGastronomistMobileApp.WebPageDll.Services;
using VAGastronomistMobileApp.WebPageDll.Services.Infrastructure;

namespace VAGastronomistMobileApp.WebPageDll
{
    /// <summary>
    /// 美食广场业务逻辑
    /// </summary>
    public class FoodPlazaOperate
    {
        private readonly IFoodPlazaManager manager;
        /// <summary>
        /// 构造器
        /// </summary>
        public FoodPlazaOperate()
        {
            manager = new FoodPlazaManager();
        }

        /// <summary>
        /// 查询当前城市所有美食广场分享的菜品编号
        /// </summary>
        /// <param name="cityId"></param>
        /// <returns></returns>
        public string SelectAllDishsByCityId(int cityId)
        {
            return manager.SelectAllDishsByCityId(cityId);
        }

        /// <summary>
        /// 查询当前城市当前时间段美食广场所有的数据
        /// </summary>
        /// <param name="cityId"></param>
        /// <param name="strTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public DataTable SelectFoodPlaza(int cityId, DateTime strTime, DateTime endTime)
        {
            return manager.SelectFoodPlaza(cityId, strTime, endTime);
        }

        /// <summary>
        /// 分页获取美食广场数据（悠先点菜客户端接口调用）
        /// </summary>
        /// <param name="foodPlazaList"></param>
        /// <param name="cityId"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="initPath1"></param>
        /// <param name="isHaveMore"></param>
        /// <returns></returns>
        public List<ClientFoodPlaza> PagingFoodPlaza(List<ClientFoodPlaza> foodPlazaList, int cityId, int pageSize, int pageIndex, string initPath1, out bool isHaveMore)
        {
            List<ClientFoodPlazaTemp> tempData = manager.PagingFoodPlaza(cityId, pageSize, pageIndex, out isHaveMore);
            if (tempData.Any())
            {
                string initPath = initPath1 + WebConfig.CustomerPicturePath;
                string url = WebConfig.ServerDomain + "AppPages/FoodDiariesShow.aspx?id={0}&app=true";
                IImageInfoRepository repositoryContext = ServiceFactory.Resolve<IImageInfoRepository>();

                string ids = CommonPageOperate.SplicingListStr(tempData, "dishIds");
                List<DishImage> list = repositoryContext.GetAssignScaleImageInfosByDishId(ids, initPath1);//一次查询

                foreach (var tempDataItem in tempData)
                {
                    ClientFoodPlaza clientFoodPlaza = new ClientFoodPlaza();
                    clientFoodPlaza.customerName = tempDataItem.customerName;
                    clientFoodPlaza.foodDiaryUrl = String.Format(url, tempDataItem.foodDiaryId);
                    //用户图片URL
                    clientFoodPlaza.personImgUrl = string.IsNullOrEmpty(tempDataItem.picture) ? "" : initPath + tempDataItem.registerDate.ToString("yyyyMM/") + tempDataItem.picture;
                    //预留字段
                    clientFoodPlaza.praisedCount = 0;
                    clientFoodPlaza.shareContent = tempDataItem.content;

                    //组装菜品图片URL
                    //clientFoodPlaza.dishUrlList = new List<string>();
                    //foreach (var item in strDishId)
                    //{
                    //    clientFoodPlaza.dishUrlList.Add(
                    //        (from b in list
                    //         where b.dishId == Common.ToInt32(item)
                    //         orderby b.imageId ascending
                    //         select b.url).FirstOrDefault()
                    //    );
                    //}
                    string[] strDishId = tempDataItem.dishIds.Split(',');
                    clientFoodPlaza.dishUrlList = (from a in list
                                                   join b in strDishId on a.dishId.ToString() equals b
                                                   select a.url).ToList();
                    foodPlazaList.Add(clientFoodPlaza);
                }
            }
            return foodPlazaList;
        }

        /// <summary>
        /// 分页获取美食广场数据（服务器端配置页面调用）
        /// </summary>
        /// <param name="cityId"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="strTime"></param>
        /// <param name="endTime"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public List<FoodPlazaConfigPage> PagingFoodPlaza(int cityId, int pageSize, int pageIndex, string strTime, string endTime, out int totalCount)
        {
            List<FoodPlazaTempClild> initData = manager.PagingFoodPlaza(cityId, pageSize, pageIndex, strTime, endTime, out  totalCount);
            List<FoodPlazaConfigPage> resultList = new List<FoodPlazaConfigPage>();
            if (initData.Any())
            {
                IMenuInfoRepository menuRepositoryContext = ServiceFactory.Resolve<IMenuInfoRepository>();
                IImageInfoRepository repositoryContext = ServiceFactory.Resolve<IImageInfoRepository>();
                string path = WebConfig.CdnDomain + WebConfig.ImagePath + WebConfig.CustomerPicturePath;
                string dishPath = WebConfig.CdnDomain + WebConfig.ImagePath;
                foreach (var item in initData)
                {
                    var menuInfo = menuRepositoryContext.GetMenuInfoByShopId(item.shopId);
                    //组装菜品图片URL
                    string[] dishsStrClild = item.dishIds.Split(',');
                    int[] intDishIds = new int[dishsStrClild.Length];
                    for (int i = 0; i < dishsStrClild.Length; i++)//将全部的数字存到数组里。
                    {
                        intDishIds[i] = Common.ToInt32(dishsStrClild[i]);
                    }
                    var imagelist = repositoryContext.GetAssignScaleImageInfosByDishId(ImageScale.普通图片, intDishIds.ToArray());
                    var dishImgs = new List<FoodPlazaDish>();
                    foreach (var dishItem in intDishIds)
                    {
                        dishImgs.Add(new FoodPlazaDish()
                        {
                            dishId = dishItem,
                            dishImg = (from b in imagelist
                                       where b.DishID == dishItem
                                       orderby b.ImageID ascending
                                       select dishPath + menuInfo.menuImagePath + b.ImageName).FirstOrDefault()
                        });
                    }
                    item.personImgUrl = string.IsNullOrEmpty(item.personImgUrl) ? "" : path + item.registerDate.ToString("yyyyMM/") + item.personImgUrl;
                    //组装一条订单数据
                    resultList.Add(new FoodPlazaConfigPage()
                    {
                        foodPlazaId = item.foodPlazaId,
                        preOrder19dianId = item.preOrder19DianId,
                        shopName = item.shopName,
                        personImgUrl = item.personImgUrl,
                        preOrderSum = item.orderAmount,
                        customerId = item.customerId,
                        dishImgs = dishImgs,
                        shopId = item.shopId,
                        customerName = item.customerName,
                        cityId = item.cityId,
                        isListTop = item.isListTop
                    });
                }
            }
            return resultList;
        }

        /// <summary>
        /// 美食广场配置页面分页根据查询条件查询点单信息
        /// </summary>
        /// <param name="cityId"></param>
        /// <param name="shopId"></param>
        /// <param name="preOrderSum"></param>
        /// <param name="isPaid"></param>
        /// <param name="strTime"></param>
        /// <param name="endTime"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public List<FoodPlazaConfigPage> PagingFoodPlazaOrder(int cityId, int shopId, double preOrderSum,
            int isPaid, string strTime, string endTime, int pageSize, int pageIndex, out int totalCount)
        {
            List<FoodPlazaOrder> initData = manager.PagingFoodPlazaOrder(cityId, shopId, preOrderSum, isPaid, strTime, endTime,
             pageSize, pageIndex, out totalCount);
            List<FoodPlazaConfigPage> resultList = new List<FoodPlazaConfigPage>();
            PackageData(initData, resultList);
            return resultList;
        }

        /// <summary>
        /// 组装服务器端页面调用数据
        /// </summary>
        /// <param name="initData"></param>
        /// <param name="resultList"></param>
        private void PackageData(List<FoodPlazaOrder> initData, List<FoodPlazaConfigPage> resultList)
        {
            if (initData.Any())
            {
                IMenuInfoRepository menuRepositoryContext = ServiceFactory.Resolve<IMenuInfoRepository>();
                IImageInfoRepository repositoryContext = ServiceFactory.Resolve<IImageInfoRepository>();
                foreach (var item in initData)
                {
                    var menuInfo = menuRepositoryContext.GetMenuInfoByShopId(item.shopId);
                    var orders = JsonOperate.JsonDeserialize<List<PreOrderIn19dian>>(item.orderInJson);
                    var dishIds = orders.Select(p => p.dishId).ToList();
                    //组装菜品图片URL
                    var imagelist = repositoryContext.GetAssignScaleImageInfosByDishId(ImageScale.普通图片, dishIds.ToArray());
                    var dishImgs = new List<FoodPlazaDish>();
                    foreach (var dishItem in dishIds)
                    {
                        string imagePath = (from b in imagelist
                                            where b.DishID == dishItem
                                            orderby b.ImageID ascending
                                            select
                                             WebConfig.CdnDomain + WebConfig.ImagePath + menuInfo.menuImagePath + b.ImageName).FirstOrDefault();
                        dishImgs.Add(new FoodPlazaDish()
                        {
                            dishId = dishItem,
                            dishImg = Common.ToString(imagePath)
                        });
                    }
                    //组装用户头像URL
                    item.personImgUrl = string.IsNullOrEmpty(item.personImgUrl)
                           ? ""
                           : WebConfig.CdnDomain + WebConfig.ImagePath + WebConfig.CustomerPicturePath +
                             item.registerDate.ToString("yyyyMM/") + item.personImgUrl;
                    //组装一条订单数据
                    resultList.Add(new FoodPlazaConfigPage()
                    {
                        foodPlazaId = item.foodPlazaId,
                        preOrder19dianId = item.preOrder19dianId,
                        shopName = item.shopName,
                        personImgUrl = item.personImgUrl,
                        preOrderSum = item.preOrderSum,
                        customerId = item.customerId,
                        dishImgs = dishImgs,
                        shopId = item.shopId,
                        customerName = item.customerName,
                        cityId = item.cityId
                    });
                }
            }
        }

        /// <summary>
        /// 新增美食广场记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public long InsertFoodPlaza(FoodPlaza model)
        {
            //using (TransactionScope scope = new TransactionScope())
            //{
            IWeatherService service = ServiceFactory.Resolve<IWeatherService>();
            IOss clientOss = ServiceFactory.Resolve<IOss>();
            IPreOrder19DianService orderService = ServiceFactory.Resolve<IPreOrder19DianService>();
            FoodDiary foodDiary = orderService.GetAndWriteFoodDiaryOrder(model.preOrder19DianId, service, clientOss, false);
            if (foodDiary != null && manager.InsertFoodPlaza(model) > 0)
            {
                // scope.Complete();
                return 1;
            }
            else
            {
                return 0;
            }
            // }
        }

        /// <summary>
        /// 置顶当前美食广场记录
        /// </summary>
        /// <param name="foodPlazaId"></param>
        /// <param name="latestOperateEmployeeId"></param>
        /// <param name="isListTop"></param>
        /// <returns></returns>
        public bool ListTopFoodPlaza(long foodPlazaId, int latestOperateEmployeeId, bool isListTop)
        {
            return manager.ListTopFoodPlaza(foodPlazaId, latestOperateEmployeeId, isListTop);
        }

        /// <summary>
        ///  删除当前美食广场记录
        /// </summary>
        /// <param name="foodPlazaId"></param>
        /// <param name="latestOperateEmployeeId"></param>
        /// <returns></returns>
        public bool DeleteFoodPlaza(long foodPlazaId, int latestOperateEmployeeId)
        {
            return manager.DeleteFoodPlaza(foodPlazaId, latestOperateEmployeeId);
        }

        /// <summary>
        /// 美食广场记录更新操作
        /// </summary>
        /// <param name="foodPlazaId"></param>
        /// <param name="latestOperateEmployeeId"></param>
        /// <returns></returns>
        public bool UpdateLatestOperate(long foodPlazaId, int latestOperateEmployeeId)
        {
            return manager.UpdateLatestOperate(foodPlazaId, latestOperateEmployeeId);
        }
        /// <summary>
        /// （悠先点菜）客户端美食广场接口
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ClientCheckFoodPlazaReponse ClientCheckFoodPlaza(ClientCheckFoodPlazaRequest request)
        {
            ClientCheckFoodPlazaReponse response = new ClientCheckFoodPlazaReponse()
            {
                type = VAMessageType.CLIENT_CHECK_FOODPLAZA_REPONSE,
                cookie = request.cookie,
                uuid = request.uuid
            };
            CheckCookieAndMsgtypeInfo checkResult = Common.CheckCookieAndMsgtype(request.cookie, request.uuid, (int)request.type, (int)VAMessageType.CLIENT_CHECK_FOODPLAZA_REQUEST);

            List<VABrandBannerList> vaAd = new List<VABrandBannerList>();
            List<ClientFoodPlaza> foodPlazaList = new List<ClientFoodPlaza>();
            bool isHaveMore = false;
            if (checkResult.result == VAResult.VA_OK)
            {
                response.result = VAResult.VA_OK;
                string initPath1 = WebConfig.CdnDomain + WebConfig.ImagePath;
                if (request.pageIndex == 1)
                {
                    List<ClientFoodPlaza> cache = MemcachedHelper.GetMemcached<List<ClientFoodPlaza>>("ClientCheckFoodPlazaReponse_" + request.cityId);//缓存美食广场第一页数据
                    if (cache == null)
                    {
                        response.foodPlazaList = PagingFoodPlaza(foodPlazaList, request.cityId, request.pageSize, request.pageIndex, initPath1, out  isHaveMore);
                        MemcachedHelper.AddMemcached("ClientCheckFoodPlazaReponse_" + request.cityId, response.foodPlazaList, 3600);
                    }
                    else
                    {
                        foodPlazaList = cache;
                        if (foodPlazaList.Count < request.pageSize)
                        {
                            isHaveMore = false;
                        }
                        else
                        {
                            isHaveMore = true;
                        }
                    }
                    DataTable dtCityCompanyBanner = new BannerCacheLogic().GetFoodPlazaBannerByCityId(request.cityId);
                    if (dtCityCompanyBanner.Rows.Count > 0)
                    {
                        vaAd = new ClientIndexListOperate().FillBrandBannerList(dtCityCompanyBanner, WebConfig.CdnDomain + WebConfig.ImagePath);
                        string str = CommonPageOperate.SplicingAlphabeticStr(dtCityCompanyBanner, "advertisementConnAdColumnId", false);//更新广告滚动次数
                        AdvertisementManager advertisementMan = new AdvertisementManager();
                        advertisementMan.UpdateAdvertisementDisplayCount(str, 1);
                    }
                }
                else
                {
                    response.foodPlazaList = PagingFoodPlaza(foodPlazaList, request.cityId, request.pageSize, request.pageIndex, initPath1, out  isHaveMore);
                }
            }
            else
            {
                response.result = checkResult.result;
            }
            response.vaAd = Common.GetRandomList<VABrandBannerList>(vaAd);
            response.foodPlazaList = foodPlazaList;
            response.isHaveMore = isHaveMore;
            return response;
        }
    }
}
