using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.WebPageDll;
public partial class AuthorizationManagement_RoleAdd : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        RoleInfo roleInfo = new RoleInfo();
        roleInfo.RoleDescription = TextBox_RoleDescription.Text;
        roleInfo.RoleName = TextBox_RoleName.Text;
        roleInfo.RoleStatus = 1;

        RoleOperate roleOperate = new RoleOperate();
        bool i = roleOperate.AddRole(roleInfo);
        if (i == true)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "js", "<script>alert('添加成功！');</script>");
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "js", "<script>alert('添加失败！');</script>");
        }
    }
}