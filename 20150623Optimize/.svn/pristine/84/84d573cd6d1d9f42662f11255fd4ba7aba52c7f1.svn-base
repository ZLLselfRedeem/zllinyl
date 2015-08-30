using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.WebPageDll;
using System.Data;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserInfo"] == null)
        {
            Response.Redirect("~/Login.aspx");
            return;
        }
        //2013-7-26 wangcheng
        if (!IsPostBack)
        {
            //查询当前的员工的信息
            string userName = ((VAEmployeeLoginResponse)Session["UserInfo"]).userName;//获取当前的登录信息
            EmployeeOperate employeeOperate = new EmployeeOperate();
            DataTable dt = employeeOperate.QueryEmployeeAuthortiy(userName);//查询当前用户所有的权限页面
            if (dt.Rows.Count > 0)
            {
                DataRow[] dtSelect = dt.Select("AuthorityURL is not null and AuthorityURL <> '' and AuthorityType='page'");
                if (dtSelect.Length <= 0)
                {
                    CommonPageOperate.AlterMsg(this.Page, "当前帐号没有分配任何查看页面的权限，请联系管理员");
                    Response.Redirect("~/Login.aspx");
                    return;
                }
                string firstAuthortiyPage = Common.ToString(dtSelect[0]["AuthorityURL"]);
                if (String.IsNullOrWhiteSpace(firstAuthortiyPage))//当前用户权限下没有任何可用页面
                {
                    CommonPageOperate.AlterMsg(this.Page, "当前帐号没有分配任何查看页面的权限，请联系管理员");
                    Response.Redirect("~/Login.aspx");
                    return;
                }
                string defaultPage = employeeOperate.QueryEmployeeDefaultPage(userName);//当前帐号配置的默认页面
                if (!String.IsNullOrWhiteSpace(defaultPage))
                {
                    if (dtSelect.Any(p => p.Field<string>("AuthorityURL") == defaultPage))//表示默认页面在当前帐号的权限页面中
                    {
                        mainFrame.Attributes.Add("src", defaultPage);//读取跳转到设置的页面，配置页面可能无效
                    }
                    else
                    {
                        mainFrame.Attributes.Add("src", firstAuthortiyPage);//读取跳转到设置的页面
                    }
                }
                else
                {
                    mainFrame.Attributes.Add("src", firstAuthortiyPage);//读取跳转到设置的页面
                }
            }
            else
            {
                //表示没有分配任何权限页面，视为登录失败
                CommonPageOperate.AlterMsg(this.Page, "当前帐号没有分配任何查看页面的权限，请联系管理员");
                Response.Redirect("~/Login.aspx");
            }
        }
    }
}