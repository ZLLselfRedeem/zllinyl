using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.WebPageDll;
using System.Web.Services;
using System.Data;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.WebPageDll.Syb;

public partial class CompanyPages_accountPayDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }
    /// <summary>
    /// 查看打款详情 add by wangc 20140417
    /// </summary>
    /// <param name="accountId"></param>
    /// <returns></returns>
    [WebMethod]
    public static string AccountDetail(long accountId)
    {
        CompanyApplyPaymentOperate operate = new CompanyApplyPaymentOperate();
        DataTable dt = operate.QueryBusinessPayByAccountId(accountId);
        if (dt.Rows.Count == 1)
        {
            return "{" + "\"accountInfo\":[" + Common.ConvertDateTableToJson(dt) + "]}";
        }
        else
        {
            return "暂无详情";
        }
    }
}
