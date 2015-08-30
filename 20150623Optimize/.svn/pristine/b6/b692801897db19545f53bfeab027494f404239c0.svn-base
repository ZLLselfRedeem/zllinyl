using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.SQLServerDAL;
using VAGastronomistMobileApp.WebPageDll;

public partial class CompanyPages_AccountTotalOutFile : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int shopId = Common.ToInt32(Request["shopId"]);
        int outType = Common.ToInt32(Request["outType"]);
        int CouponType = Common.ToInt32(Request["couponType"]);

        DateTime defaultDateTime = new DateTime(1970, 1, 1);
        DateTime startDate = Common.ToDateTime(Request["datetimestart"], defaultDateTime);
        DateTime endDate = Common.ToDateTime(Request["datetimeend"], defaultDateTime);
        endDate = endDate.AddDays(1);

        var list = MoneyMerchantAccountDetailManager.GetAccountTotal(shopId, startDate, endDate,CouponType);
        HttpResponse response = System.Web.HttpContext.Current.Response;
        response.Buffer = true;
        response.ClearContent();
        response.ClearHeaders();
        response.Charset = "GB2312";
        response.ContentEncoding = Encoding.GetEncoding("GB2312");

        if (outType == 1)
        {
            string excelName = "AccountTotal_" + DateTime.Now.ToString("yyyy/mm/dd_hh:mm:ss");
            response.ContentType = "application/ms-excel";//设置输出文件类型为excel文件。 
            response.AppendHeader("Content-Disposition", "attachment;filename=" + excelName + ".xls");

        }
        else
        {
            string textName = "AccountTotal_" + DateTime.Now.ToString("yyyy/mm/dd_hh:mm:ss");
            response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlPathEncode(textName) + ".txt");
        }
        //colHeaders = "预点单号" + "\t" + "手机号\t" + "时间" + "\t" + "金额" + "\t" + "余额" + "\t" + "操作类型" + "\r\n";
        StringBuilder strContextBuilder = new StringBuilder();
        strContextBuilder.AppendLine("日期\t已入座订单数\t已入座总金额");//头
        foreach (var moneyMerchantAccountSumResponse in list)
        {
            strContextBuilder.AppendFormat("{0:yyyy-MM-dd}\t{1}\t{2:F}", moneyMerchantAccountSumResponse.date, moneyMerchantAccountSumResponse.count, moneyMerchantAccountSumResponse.total);
            strContextBuilder.AppendLine();
        }
        response.Write(strContextBuilder.ToString());
        response.End();
        //return strContextBuilder.ToString();
    }
}