using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.WebPageDll;
using System.Data;

public partial class CompanyPages_merchantsTreasureUserCheck_MerchantsTreasureUserCheck : System.Web.UI.UserControl
{
    /// <summary>
    /// 根据登录商户宝url判断该用户是否合法，此处页面权限需要老后台添加，当用户登录成功，会保存Session["MerchantsTreasureUserInfo"]
    /// </summary>
    public int CheckUrl { get; set; }
    protected void Page_Load(object sender, EventArgs e)
    {
        //判断session
        if (Session["MerchantsTreasureUserInfo"] == null)
        {
            Response.Write("<script>window.parent.location.href='../CompanyPages/login.aspx';</script>");//跳转到商户宝登录首页
        }
        //判断权限
        else
        {
            if (CheckUrl != 1)
            {
                //从外面传过来的url
                string url = Request.Url.AbsolutePath.ToString().Substring(1);
                if (url.Substring(0, 8) == "WebSites")
                {
                    url = url.Substring(9);
                }
                string userName = ((VAEmployeeLoginResponse)Session["MerchantsTreasureUserInfo"]).userName;
                EmployeeOperate employeeOperate = new EmployeeOperate();
                DataTable dt = employeeOperate.QueryEmployeeAuthortiy(userName);
                bool haveQuanxian = false;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["AuthorityURL"].ToString() == url)
                    {
                        haveQuanxian = true;
                        break;
                    }
                }
                if (!haveQuanxian)
                {
                    Response.Write("<script>window.parent.location.href='../CompanyPages/login.aspx';</script>");//跳转到商户宝登录首页
                }
            }
        }
    }
}