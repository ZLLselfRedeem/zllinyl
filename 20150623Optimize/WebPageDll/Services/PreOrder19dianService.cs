using Aliyun.OpenServices.OpenStorageService;
using System;
using System.Collections.Generic;
using System.Linq;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL.Persistence;
using VAGastronomistMobileApp.WebPageDll.Services.Infrastructure;

namespace VAGastronomistMobileApp.WebPageDll.Services
{
    /// <summary>
    /// 订单逻辑层接口
    /// </summary>
    public interface IPreOrder19DianService
    {
        /// <summary>
        /// 获取并记录美食日记
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="weatherService"></param>
        /// <param name="clientOss"></param>
        /// <param name="isFoodPlazaRequest"></param>
        /// <returns></returns>
        [Transaction]
        FoodDiary GetAndWriteFoodDiaryOrder(long orderId, IWeatherService weatherService, IOss clientOss, bool isFoodPlazaRequest = false);
    }

    /// <summary>
    /// 订单逻辑层
    /// </summary>
    public class PreOrder19DianService : BaseService, IPreOrder19DianService
    {
        /// <summary>
        /// 构造器
        /// </summary>
        public PreOrder19DianService(IRepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public FoodDiary GetAndWriteFoodDiaryOrder(long orderId, IWeatherService weatherService, IOss clientOss, bool isFoodPlazaRequest = false)
        {
            int i = 0;
            int sortIndex = 1;
            Random random = new Random();
            FoodDiary foodDiary = RepositoryContext.GetFoodDiaryRepository().GetFoodDiaryByOrderId(orderId);
            if (foodDiary == null)
            {
                var orderInfo = RepositoryContext.GetPreOrder19DianInfoRepository().GetById(orderId);
                if (orderInfo != null)
                {
                    //int foodDiaryDefaultContentCount =
                    //       int.Parse(ConfigurationManager.AppSettings["FoodDiaryDefaultContentCount"]);
                    //int foodDiaryDefaultContentIndex = random.Next(0, foodDiaryDefaultContentCount) + 1;

                    CustomerInfo customerInfo = RepositoryContext.GetCustomerInfoRepository().GetById(orderInfo.customerId);
                    ShopInfo shopInfo = RepositoryContext.GetShopInfoRepository().GetById(orderInfo.shopId);

                    FoodDiariesShareConfigOperate operate = new FoodDiariesShareConfigOperate();
                    List<FoodDiariesShareConfig> listConfig = operate.GetAllFoodDiariesShareConfig().Where(p => p.type == (int)FoodDiariesShareConfigType.美食日记分享页面顶部描述).ToList();
                    if (listConfig.Count == 0)
                    {
                        listConfig.Add(new FoodDiariesShareConfig() { foodDiariesShareInfo = "一路上有你，多吃点也可以！" });
                    }
                    int foodDiaryDefaultContentIndex = random.Next(0, listConfig.Count);//生成随机数

                    string weatherCode = "";
                    string cityName = "";
                    if (shopInfo != null)
                    {
                        City city = RepositoryContext.GetCityRepository().GetById(shopInfo.cityID);
                        if (city != null)
                        {
                            weatherCode = city.WeatherCityCode;
                            cityName = city.cityName.TrimEnd('市');
                        }
                    }
                    foodDiary = new FoodDiary()
                    {
                        Name = customerInfo != null ? customerInfo.UserName : "",
                        ShoppingDate = DateTime.Now,
                        CreateTime = DateTime.Now,
                        OrderId = orderId,
                        Shared = FoodDiaryShared.没有分享,
                        ShopName = shopInfo != null ? shopInfo.shopName : "",
                        Weather = weatherService.GetCityWeather(weatherCode, cityName),
                        //FoodDiaryDishes = dishes,
                        //Content = ConfigurationManager.AppSettings["FoodDiaryDefaultContent" + foodDiaryDefaultContentIndex]
                        Content = listConfig[foodDiaryDefaultContentIndex].foodDiariesShareInfo
                    };
                    RepositoryContext.GetFoodDiaryRepository().Add(foodDiary);
                    //下面的逻辑暂时用不上，影响效率，加参数值判断 wangc 2014/08/14
                    if (isFoodPlazaRequest)
                    {
                        return foodDiary;
                    }

                    var dishes = new List<FoodDiaryDish>();
                    var menuInfo = RepositoryContext.GetMenuInfoRepository().GetMenuInfoByShopId(orderInfo.shopId);
                    if (menuInfo != null)
                    {
                        string menuImagePath = menuInfo.menuImagePath;
                        List<int> dishIds = new List<int>();
                        if (!string.IsNullOrWhiteSpace(orderInfo.orderInJson))
                        {

                            var orders = JsonOperate.JsonDeserialize<List<PreOrderIn19dian>>(orderInfo.orderInJson);
                            dishIds = orders.Select(p => p.dishId).ToList();

                            var imagelist =
                                RepositoryContext.GetImageInfoRepository()
                                    .GetAssignScaleImageInfosByDishId(ImageScale.普通图片, dishIds.ToArray());

                            //dishes =
                            //    (from a in
                            //         RepositoryContext.GetDishI18NRepository().GetDishI18NsByDishIds(dishIds.ToArray())
                            //     select new FoodDiaryDish
                            //     {
                            //         DishId = a.DishID,
                            //         DishName = a.DishName,
                            //         ImagePath = (from b in imagelist
                            //                      where b.DishID == a.DishID
                            //                      orderby b.ImageID ascending 
                            //                      select WebConfig.ImagePath + menuImagePath + b.ImageName).FirstOrDefault(),
                            //         Source = FoodDiaryDishSource.订单,
                            //         Status = true,
                            //         FoodDiaryId = foodDiary.Id,
                            //         Sort = 3
                            //     }).ToList();

                            var query =
                                from a in
                                    RepositoryContext.GetDishI18NRepository().GetDishI18NsByDishIds(dishIds.ToArray())
                                select new FoodDiaryDish
                                {
                                    DishId = a.DishID,
                                    DishName = a.DishName,
                                    ImagePath = (from b in imagelist
                                                 where b.DishID == a.DishID
                                                 orderby b.ImageID ascending
                                                 select WebConfig.ImagePath + menuImagePath + b.ImageName).FirstOrDefault(),
                                    Source = FoodDiaryDishSource.订单,
                                    Status = true,
                                    FoodDiaryId = foodDiary.Id,
                                    Sort = sortIndex++
                                };

                            dishes = (from a in query
                                      where !string.IsNullOrEmpty(a.ImagePath)
                                      select a).ToList();
                        }

                        if (dishes.Count < 4 && shopInfo != null)
                        {
                            int count = 4 - dishes.Count;
                            IShopRevealImageRepository shopRevealImageRepository = RepositoryContext.GetRevealImageRepository();
                            var shopRevealImages = shopRevealImageRepository.GetShopRevealImages(shopInfo.shopID).ToList();

                            int index = 0;
                            if (shopRevealImages.Count > count)
                            {
                                index = random.Next(0, shopRevealImages.Count - count + 1);
                            }

                            dishes.AddRange(shopRevealImages.Skip(index).Take(count).Select(p => new FoodDiaryDish
                            {
                                DishId = 0,
                                DishName = "门店环境" + (++i),
                                ImagePath = WebConfig.ImagePath + shopInfo.shopImagePath + WebConfig.ShopImg + p.RevealImageName,
                                Source = FoodDiaryDishSource.门店环境,
                                Status = true,
                                FoodDiaryId = foodDiary.Id,
                                Sort = sortIndex++
                            }));
                        }
                    } RepositoryContext.GetFoodDiaryDishRepository().AddRange(dishes);

                }
                else
                {
                    return null;
                }
            }
            i = 0;
            sortIndex = 1;
            var fd = new FoodDiary
            {
                Name = foodDiary.Name,
                Content = foodDiary.Content,
                CreateTime = foodDiary.CreateTime,
                Id = foodDiary.Id,
                OrderId = foodDiary.OrderId,
                Shared = foodDiary.Shared,
                ShopName = foodDiary.ShopName,
                ShoppingDate = foodDiary.ShoppingDate,
                Weather = foodDiary.Weather,
                IsBig = foodDiary.IsBig,
                IsHideDishName = foodDiary.IsHideDishName,
                FoodDiaryDishes = (from a in foodDiary.FoodDiaryDishes != null && foodDiary.FoodDiaryDishes.Count > 0 ? foodDiary.FoodDiaryDishes : RepositoryContext.GetFoodDiaryDishRepository().GetFoodDiaryDishesByFoodDiaryId(foodDiary.Id)
                                   orderby a.Sort ascending
                                   select new FoodDiaryDish
                                   {
                                       DishId = a.DishId,
                                       DishName = string.IsNullOrEmpty(a.DishName) && a.Source == FoodDiaryDishSource.门店环境 ? "门店环境" + (++i) : a.DishName,
                                       FoodDiaryId = a.FoodDiaryId,
                                       Id = a.Id,
                                       Status = a.Status,
                                       ImagePath = WebConfig.CdnDomain + a.ImagePath,//+ "@600w_450h_50Q_1e_1c",
                                       Source = a.Source,
                                       Sort = sortIndex++
                                   }).ToList()
            };



            return fd;
        }
    }
}
