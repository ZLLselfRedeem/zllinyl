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

public partial class CompanyPages_accountPreOrderDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }
    /// <summary>
    /// 显示固定页面信息
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    public static string CommonPageInfoShow(Guid preOrder19dianId)
    {
        return SybPreOrder.PreOrderDetail(preOrder19dianId);
    }
}
