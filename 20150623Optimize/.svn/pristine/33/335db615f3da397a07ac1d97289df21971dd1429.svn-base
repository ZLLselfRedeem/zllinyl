using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Aliyun.OpenServices.OpenStorageService;
using Autofac;
using Autofac.Integration.Web;
using VA.AllNotifications;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.WebPageDll.Services;
using VAGastronomistMobileApp.WebPageDll.Services.Infrastructure;

public partial class AppPages_redirect : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    [WebMethod]
    public static string GetJson(long id)
    {

        var foodDiaryService = ServiceFactory.Resolve<IFoodDiaryService>();
        var foodDiary = foodDiaryService.GetFoodDiaryById(id);
        if (foodDiary != null)
        {
            FoodDiary fd = new FoodDiary
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
                FoodDiaryDishes = (from a in foodDiary.FoodDiaryDishes
                                   orderby a.Sort descending
                                   select new FoodDiaryDish
                                   {
                                       DishId = a.DishId,
                                       DishName = a.DishName,
                                       FoodDiaryId = a.FoodDiaryId,
                                       Id = a.Id,
                                       Status = a.Status,
                                       ImagePath = a.ImagePath,
                                       Source = a.Source,
                                       Sort = a.Sort
                                   }).ToList()
            };

            foodDiary = fd;
            return JsonOperate.JsonSerializer(foodDiary);
        }
        else
            return "";
    }
}