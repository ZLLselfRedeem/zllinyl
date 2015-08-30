using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.WebPageDll;
using System.Data;
using System.Text;
public partial class WebUserControl_CheckUser : System.Web.UI.UserControl
{
    /// <summary>
    /// 是否判断url
    /// </summary>
    public int CheckUrl{ get; set; }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserInfo"] == null)
        {
            Response.Write("<script>window.parent.location.href='../Login.aspx';</script>");
        }
        else
        {
            if (CheckUrl != 1)
            {
                //从外面传过来的url
                string url = Request.Url.AbsolutePath.ToString().Substring(1);
                if (url.Substring(0, 8) == "WebSites" )
                {
                    url = url.Substring(9);
                }
                string userName = ((VAEmployeeLoginResponse)Session["UserInfo"]).userName;
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
                    Response.Write("<script>;window.parent.location.href='../Login.aspx';</script>");
                }
            }
        }
    }
}