using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.WebPageDll;
using VAGastronomistMobileApp.Model;
using System.Data;
public partial class AuthorizationManagement_AuthorityAdd : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetAuthority();
        }
    }

    /// <summary>
    /// 获取所有权限
    /// </summary>
    protected void GetAuthority()
    {
        AuthorityOperate authorityOperate = new AuthorityOperate();
        DataTable dt= authorityOperate.QueryAuthority();
        DataView dv = dt.DefaultView;
        dv.RowFilter = "AuthorityRank=0";
        DropDownList_AuthorityRank.Items.Clear();
        DropDownList_AuthorityRank.DataSource = dt;
        DropDownList_AuthorityRank.DataTextField = "AuthorityName";
        DropDownList_AuthorityRank.DataValueField = "AuthorityID";
        DropDownList_AuthorityRank.DataBind();
        DropDownList_AuthorityRank.Items.Insert(0, new ListItem("","0"));
    }


    protected void Button1_Click(object sender, EventArgs e)
    {
        AuthorityInfo authorityInfo = new AuthorityInfo();
        authorityInfo.AuthorityDescription = TextBox_AuthorityDescription.Text;
        authorityInfo.AuthorityName = TextBox_AuthorityName.Text;
        authorityInfo.AuthorityRank = Common.ToInt32(DropDownList_AuthorityRank.SelectedValue);
        authorityInfo.AuthorityStatus = 1;
        authorityInfo.AuthorityURL = TextBox_AuthorityURL.Text;
        authorityInfo.AuthoritySequence =Common.ToInt32(TextBox_AuthoritySequence.Text);
        authorityInfo.AuthorityType = DropDownList_AuthorityType.SelectedValue;
        AuthorityOperate authorityOperate = new AuthorityOperate();
        bool i = authorityOperate.AddAuthority(authorityInfo);
        if (i == true)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('添加成功')</script>");
            GetAuthority();
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('添加失败')</script>");
        }
    }
}