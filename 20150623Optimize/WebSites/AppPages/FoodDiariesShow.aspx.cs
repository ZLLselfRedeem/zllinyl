using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Aliyun.OpenServices.OpenStorageService;
using Autofac;
using Autofac.Integration.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using VA.AllNotifications;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL.Persistence;
using VAGastronomistMobileApp.WebPageDll.Services;
using VAGastronomistMobileApp.WebPageDll.Services.Infrastructure;
using VAGastronomistMobileApp.WebPageDll;

public partial class AppPages_FoodDiariesShow : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    [WebMethod]
    public static string GetJson(string id)
    {
        long longId = 0;
        if (long.TryParse(id, out longId))
        {

            var foodDiaryService = ServiceFactory.Resolve<IFoodDiaryRepository>();
            var foodDiaryDishService = ServiceFactory.Resolve<IFoodDiaryDishRepository>();

            var foodDiary = foodDiaryService.GetById(longId);
            if (foodDiary != null)
            {
                foodDiary.FoodDiaryDishes =
                    (from a in foodDiaryDishService.GetFoodDiaryDishesByFoodDiaryId(foodDiary.Id)
                     where a.Status == true
                     orderby a.Sort ascending
                     select new FoodDiaryDish
                     {
                         DishId = a.DishId,
                         DishName = a.DishName,
                         FoodDiaryId = a.FoodDiaryId,
                         Id = a.Id,
                         Status = a.Status,
                         ImagePath = WebConfig.CdnDomain + a.ImagePath, //+ "@600w_450h_50Q_1e_1c",
                         Source = a.Source,
                         Sort = a.Sort
                     }).ToList();


                foodDiaryService.IncrementHit(foodDiary.Id);
                JsonConverter dateJsonConverter = new IsoDateTimeConverter() { DateTimeFormat = "yyyy.MM.dd", Culture = CultureInfo.GetCultureInfo(8) };
                return Newtonsoft.Json.JsonConvert.SerializeObject(foodDiary, dateJsonConverter);
                //return VA.AllNotifications.JsonOperate.JsonSerializer(foodDiary);
            }

        }

        return "";
    }
    [WebMethod]
    public static string GetFooderHtml(string pageType)
    {
        FoodDiariesShareConfigOperate operate = new FoodDiariesShareConfigOperate();
        List<FoodDiariesShareConfig> list = new List<FoodDiariesShareConfig>();
        if (pageType == "app")
        {
            list = operate.GetAllFoodDiariesShareConfig().Where(p => p.type == (int)FoodDiariesShareConfigType.美食日记分享页面底部描述_app).ToList();
        }
        else
        {
            list = operate.GetAllFoodDiariesShareConfig().Where(p => p.type == (int)FoodDiariesShareConfigType.美食日记分享页面底部描述_pc).ToList();
        }
        if (list.Count == 0 || String.IsNullOrWhiteSpace(list[0].foodDiariesShareInfo))
        {
            //没有任何配置，默认信息
            return "<div class='title'><h3>手机点菜 就用悠先</h3></div><div class='text'><a href='../d.aspx' rel='external' data-ajax='false'>菜的美图好惊艳，先点先付高效率，首次注册减10块，百家门店慢慢挑。<span class='txtColor'>手机点菜 就用悠先~</span></a></div>";
        }
        else
        {
            Common common = new Common();
            return common.HtmlDiscode(list[0].foodDiariesShareInfo).Replace("<br/>", "");
        }

    }
}