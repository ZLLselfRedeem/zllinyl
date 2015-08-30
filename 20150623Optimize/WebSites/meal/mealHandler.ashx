<%@ WebHandler Language="C#" Class="mealHandler" %>

using System;
using System.Web;
using VAGastronomistMobileApp.WebPageDll;
using VAGastronomistMobileApp.Model;

public class mealHandler : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        //context.Response.Write("Hello World");

        MealResult result = new MealResult();
        string response = "";//返回值，json字符串
        string mobile = context.Request["mobile"];//前端提交的客户手机号码
        if (string.IsNullOrEmpty(mobile))
        {
            result.error = -2;
            result.msg = "未登陆或未注册悠先点菜";
            response = SysJson.JsonSerializer(result);
            context.Response.Write(response);
        }
        else
        {
            string cookie = context.Request["cookie"];
            if (string.IsNullOrEmpty(cookie))
            {
                result.error = -1;
                result.msg = "参数传递不正确";
                response = SysJson.JsonSerializer(result);
                context.Response.Write(response);
            }
            else
            {
                string module = context.Request["m"] == null ? "" : Common.ToString(context.Request["m"]);

                switch (module)
                {
                    case "checkLogin"://检查是否登录
                        bool isLogin = MealOperate.CheckLogin(mobile, cookie);
                        if (!isLogin)
                        {
                            result.error = -2;
                            result.msg = "未登陆或未注册悠先点菜";
                            response = SysJson.JsonSerializer(result);
                        }
                        else
                        {
                            result.error = 0;
                            result.msg = "登陆成功";
                            response = SysJson.JsonSerializer(result);
                        }
                        break;
                    case "mealActivity"://年夜饭活动信息
                        int cityId = Common.ToInt32(context.Request["cityId"]);
                        int tagId = Common.ToInt32(context.Request["tagId"]);
                        int pageIndex = Common.ToInt32(context.Request["pageIndex"]);
                        int pageSize = Common.ToInt32(context.Request["pageSize"]);
                        if (cityId < 1 || tagId < 0 || pageIndex < 1 || pageSize < 1)
                        {
                            result.error = -1;
                            result.msg = "参数传递不正确";
                            response = SysJson.JsonSerializer(result);
                        }
                        else
                        {
                            response = MealOperate.GetMealActivityList(cityId, tagId, mobile, pageIndex, pageSize);
                        }
                        break;
                    case "mealList"://套餐信息
                        int mealId = Common.ToInt32(context.Request["mealId"]);
                        if (mealId > 0)
                        {
                            response = MealOperate.GetMealDetail(mealId);
                        }
                        else
                        {
                            result.error = -1;
                            result.msg = "参数传递不正确";
                            response = SysJson.JsonSerializer(result);
                        }
                        break;
                    //case "mealSchedule"://套餐详情信息
                    //    int mealScheduleId = Common.ToInt32(context.Request["mealScheduleId"]);
                    //    if (mealScheduleId > 0)
                    //    {
                    //        response = MealOperate.GetRemainCount(mealScheduleId);
                    //    }
                    //    else
                    //    {
                    //        result.error = -1;
                    //        result.msg = "参数传递不正确";
                    //        response = SysJson.JsonSerializer(result);
                    //    }
                    //    break;
                    case "order"://客户下单
                        int mealScheduleId = Common.ToInt32(context.Request["mealScheduleId"]);
                        if (mealScheduleId > 0)
                        {
                            response = MealOperate.Order(mobile, mealScheduleId);
                        }
                        else
                        {
                            result.error = -1;
                            result.msg = "参数传递不正确";
                            response = SysJson.JsonSerializer(result);
                        }
                        break;
                    default:
                        break;
                }
                context.Response.Write(response);
            }
        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}