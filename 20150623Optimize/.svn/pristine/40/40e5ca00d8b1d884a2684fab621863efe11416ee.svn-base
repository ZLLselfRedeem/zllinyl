using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL.Persistence;
using VAGastronomistMobileApp.WebPageDll.Services.Infrastructure;

public partial class OtherStatisticalStatement_FoodDiaryDetailsOutFile : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Buffer = true;
        Response.ClearContent();
        Response.ClearHeaders();
        Response.Charset = "GB2312";
        Response.ContentEncoding = Encoding.GetEncoding("GB2312");

        DateTime startDate;
        DateTime endDate;
        string mobilePhoneNumber;
        bool isPaid;
        int outType;

        if (!DateTime.TryParse(Request.QueryString["startDate"], out startDate))
        {
            startDate = DateTime.Now.Date;
        }

        if (!DateTime.TryParse(Request.QueryString["endDate"], out endDate))
        {
            endDate = DateTime.Now.AddDays(1).Date;
        }
        else
        {
            endDate = endDate.AddDays(1);
        }

        mobilePhoneNumber = Request.QueryString["mobilePhoneNumber"];

        if (!bool.TryParse(Request.QueryString["isPaid"], out isPaid))
        {
            isPaid = true;
        }

        if (!int.TryParse(Request.QueryString["outType"], out outType))
        {
            outType = 1;
        }

        IFoodDiaryRepository foodDiaryRepository = ServiceFactory.Resolve<IFoodDiaryRepository>();
        var foodDiaryDetailses = foodDiaryRepository.GetManyFoodDiaryDetailses(mobilePhoneNumber, isPaid, startDate, endDate);
        if (outType == 1)
        {
            string excelName = "fooddiary" + DateTime.Now.ToString("yyyyMMddhhmmss");
            Response.ContentType = "application/ms-excel";//设置输出文件类型为excel文件。 
            Response.AppendHeader("Content-Disposition", "attachment;filename=" + excelName + ".xls");
        }


        StringBuilder strContextBuilder = new StringBuilder();
        strContextBuilder.AppendLine("昵称\t手机号码\t支付金额\t支付时间\t门店名称\t新浪微博\tQQ好友\tQQ空间\t微信好友\t微信朋友圈\t分享时间\t点击量\t内容");//头
        foreach (FoodDiaryDetails foodDiaryDetails in foodDiaryDetailses)
        {
            strContextBuilder.AppendFormat("{0}\t", foodDiaryDetails.Name);
            strContextBuilder.AppendFormat("{0}\t", foodDiaryDetails.MobilePhoneNumber);
            strContextBuilder.AppendFormat("{0:F}\t", foodDiaryDetails.PrePaidSum);
            strContextBuilder.AppendFormat("{0:G}\t", foodDiaryDetails.PrePayTime);
            strContextBuilder.AppendFormat("{0}\t", foodDiaryDetails.ShopName);
            strContextBuilder.AppendFormat("{0}\t", (foodDiaryDetails.Shared & FoodDiaryShared.新浪微博) != FoodDiaryShared.没有分享 ? 1 : 0);
            strContextBuilder.AppendFormat("{0}\t", (foodDiaryDetails.Shared & FoodDiaryShared.QQ好友) != FoodDiaryShared.没有分享 ? 1 : 0);
            strContextBuilder.AppendFormat("{0}\t", (foodDiaryDetails.Shared & FoodDiaryShared.QQ空间) != FoodDiaryShared.没有分享 ? 1 : 0);
            strContextBuilder.AppendFormat("{0}\t", (foodDiaryDetails.Shared & FoodDiaryShared.微信好友) != FoodDiaryShared.没有分享 ? 1 : 0);
            strContextBuilder.AppendFormat("{0}\t", (foodDiaryDetails.Shared & FoodDiaryShared.微信朋友圈) != FoodDiaryShared.没有分享 ? 1 : 0);
            strContextBuilder.AppendFormat("{0:G}\t", foodDiaryDetails.CreateTime);
            strContextBuilder.AppendFormat("{0}\t", foodDiaryDetails.Hit);
            strContextBuilder.AppendFormat("{0}", foodDiaryDetails.Content);
            strContextBuilder.AppendLine();
        }
        Response.Write(strContextBuilder.ToString());
        Response.End();


    }
}