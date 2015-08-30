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
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL;
using VAGastronomistMobileApp.SQLServerDAL.Persistence;
using VAGastronomistMobileApp.WebPageDll;
using VAGastronomistMobileApp.WebPageDll.Services;
using VAGastronomistMobileApp.WebPageDll.Services.Infrastructure;

public partial class AppPages_FoodDiaries : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    [WebMethod]
    public static string GetJson(int orderId)
    {
        try
        {



            var preOrder19DianService = ServiceFactory.Resolve<IPreOrder19DianService>();
            var weatherService = ServiceFactory.Resolve<IWeatherService>();
            var ossClient = ServiceFactory.Resolve<IOss>();
            var foodDiary = preOrder19DianService.GetAndWriteFoodDiaryOrder(orderId, weatherService, ossClient);
            if (foodDiary != null)
            {
                
                JsonConverter dateJsonConverter = new IsoDateTimeConverter() { DateTimeFormat = "yyyy.MM.dd", Culture = CultureInfo.GetCultureInfo(8) };
                return JsonConvert.SerializeObject(foodDiary, dateJsonConverter);
            }
            else
                return "";
        }
        catch (Exception exc)
        {

            return exc.ToString();
        }
    }

    [WebMethod]
    public static string GetContent(long id)
    {
        string content = "";
        try
        {
            var foodDiaryRepository = ServiceFactory.Resolve<IFoodDiaryRepository>();
            var foodDiary = foodDiaryRepository.GetById(id);

            if (foodDiary != null)
            {
                Random random = new Random();
                FoodDiariesShareConfigOperate operate = new FoodDiariesShareConfigOperate();
                List<FoodDiariesShareConfig> listConfig = operate.GetAllFoodDiariesShareConfig().Where(p => p.type == (int)FoodDiariesShareConfigType.美食日记分享页面顶部描述).ToList();
                if (listConfig.Count > 0)
                {
                    do
                    {
                        int index = random.Next(0, listConfig.Count);
                        content = listConfig[index].foodDiariesShareInfo;
                        listConfig.RemoveAt(index);

                    } while (foodDiary.Content == content && listConfig.Count > 0);
                }

                if (string.IsNullOrEmpty(content))
                {
                    content = foodDiary.Content;
                }
                else
                {
                    foodDiary.Content = content;
                    //foodDiaryRepository.Update(foodDiary);
                }
            }
        }
        catch (Exception)
        {
        }

        return content;
    }
}