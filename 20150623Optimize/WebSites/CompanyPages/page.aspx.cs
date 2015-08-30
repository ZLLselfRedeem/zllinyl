using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using VAGastronomistMobileApp.WebPageDll;
using VAGastronomistMobileApp.Model;
using System.Data;
using System.Text;
using System.Transactions;
using VAGastronomistMobileApp.SQLServerDAL;

public partial class CompanyPages_page : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //判断session
        if (Session["MerchantsTreasureUserInfo"] == null)
        {
            Response.Write("<script>window.parent.location.href='../CompanyPages/login.aspx';</script>");//跳转到商户宝登录首页
        }
    }
}
