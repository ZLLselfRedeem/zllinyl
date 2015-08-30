using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ViewAllocWebSite_CorpManage_adminLogin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["admin"] = null;
    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        if (txtName.Text.Trim() == "admin" && txtPassword.Text.Trim() == "viewalloc88776637")
        {
            Session["IsAuthorized"] = true;//配合CKFinder上传
            Session["admin"] = "admin";
            Page.Response.Redirect("manageIndex.aspx");
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "login", "<script>alert('用户名或密码错误！')</script>");
        }
    }
}