using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.WebPageDll;

public partial class AppPages_RedEnvelope_list : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //test
        //Response.Write(GetWebRedEnvelopeDetail("18507385766", 1, 10, "present"));
        //Response.Write(GetWebRedEnvelopeDetail("18507385766", 1, 10, "history"));
        //Response.Write("-----------------------------------------------------------------------------------------------------------------------------------------------------------");
        //Response.Write(GetCustomerRedEnvelope("15990111524"));        
        //GetWebRedEnvelopeDetail("18507385766", 1, 10, "present");
        //GetCustomerRedEnvelope("18507385766");
    }

    /// <summary>
    /// 红包领用详细记录
    /// </summary>
    /// <param name="pageIndex"></param>
    /// <param name="pageSize"></param>
    /// <param name="phoneNum"></param>
    /// <returns></returns>
    [WebMethod]
    public static string GetWebRedEnvelopeDetail(string mobilePhoneNumber, int pageIndex, int pageSize, string type)
    {
        RedEnvelopeDetailOperate redEnvelopeDetailOperate = new RedEnvelopeDetailOperate();
        WebRedEnvelope queryData = redEnvelopeDetailOperate.GetWebRedEnvelopeDetail(pageIndex, pageSize, mobilePhoneNumber, type);
        string a = SysJson.JsonSerializer(queryData);
        return a;
    }

    /// <summary>
    /// 查询用户红包余额
    /// </summary>
    /// <param name="mobilePhoneNumber"></param>
    /// <returns></returns>
    [WebMethod]
    public static string GetCustomerRedEnvelope(string mobilePhoneNumber)
    {
        RedEnvelopeOperate redEnvelopeOperate = new RedEnvelopeOperate();
        double[] list = redEnvelopeOperate.QueryCustomerRedEnvelope(mobilePhoneNumber);
        int expireCount = redEnvelopeOperate.QueryExpireCount(mobilePhoneNumber);
        return "{\"expireCount\":" + expireCount + ",\"executedRedEnvelopeAmount\":" + Common.ToDouble(list[0]) + ",\"notExecutedRedEnvelopeAmount\":" + Common.ToDouble(list[1]) + "}";
    }
}