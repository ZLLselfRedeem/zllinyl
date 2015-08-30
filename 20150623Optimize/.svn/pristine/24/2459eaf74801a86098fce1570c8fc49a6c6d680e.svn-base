using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.WebPageDll;
public partial class WebUserControl_HeadControl : System.Web.UI.UserControl
{
    public string headName { get; set; }
    public string navigationImage { get; set; }
    public string navigationUrl { get; set; }
    public string navigationText { get; set; }


    protected void Page_Load(object sender, EventArgs e)
    {
        Label_HeadName.Text = headName;
        Label_HeadName2.Text = headName;
        Image_NavigationImage.ImageUrl = navigationImage;
        HyperLink_NavigationUrl.NavigateUrl = navigationUrl;
        HyperLink_NavigationUrl.Text = navigationText;
    }
    //protected void Button_Out_Click(object sender, EventArgs e)
    //{
    //    if (Session["UserInfo"] != null)
    //    {
    //        VAEmployeeLogout vAEmployeeLogout = new VAEmployeeLogout();
    //        VAEmployeeLoginResponse vAEmployeeLoginResponse = ((VAEmployeeLoginResponse)Session["UserInfo"]);
    //        vAEmployeeLogout.statusGUID = vAEmployeeLoginResponse.statusGUID;
    //        vAEmployeeLogout.userName = vAEmployeeLoginResponse.userName;
    //        //注销操作
    //        EmployeeOperate employeeOperate = new EmployeeOperate();
    //        employeeOperate.EmployeeLogout(vAEmployeeLogout);
    //        Session.Clear();
    //        Page.ClientScript.RegisterStartupScript(GetType(), "js", "<script>window.open('../Login.aspx',target='_top')</script>");
    //    }
    //    else
    //    {
    //        Page.ClientScript.RegisterStartupScript(GetType(), "js", "<script>window.open('../Login.aspx',target='_top')</script>");
    //    }
    //}
}