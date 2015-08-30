using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.WebPageDll;
using System.Data;
using VAGastronomistMobileApp.Model;
using System.Transactions;
public partial class AuthorizationManagement_AuthorityManage : System.Web.UI.Page
{
    DataTable allAuthority = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetAuthority(0, 10);
            GetDropdownlistAuthority();
        }
    }
    /// <summary>
    /// 获取所有权限
    /// </summary>
    protected void GetDropdownlistAuthority()
    {
        AuthorityOperate authorityOperate = new AuthorityOperate();
        DataTable dt = allAuthority;
        DataView dv = dt.DefaultView;
        dv.RowFilter = "AuthorityRank=0";
        dv.Sort = "AuthoritySequence";
        DropDownList_AuthorityRank.Items.Clear();
        DropDownList_AuthorityRank.DataSource = dt;
        DropDownList_AuthorityRank.DataTextField = "AuthorityName";
        DropDownList_AuthorityRank.DataValueField = "AuthorityID";
        DropDownList_AuthorityRank.DataBind();
        DropDownList_AuthorityRank.Items.Insert(0, new ListItem("", "0"));
    }
    /// <summary>
    /// 获取所有权限信息
    /// </summary>
    protected void GetAuthority(int str, int end)
    {
        AuthorityOperate AuthorityOperate = new AuthorityOperate();
        allAuthority = AuthorityOperate.QueryAuthority();//总共的DataTable
        DataView dv = allAuthority.DefaultView;
        dv.RowFilter = "AuthorityRank=0";
        dv.Sort = "AuthoritySequence";
        if (allAuthority.Rows.Count > 0)
        {
            int tableCount = dv.Count;//桌子总数
            AspNetPager1.RecordCount = tableCount;
            GridView1.DataSource = dv;
            GridView1.DataBind();
        }
    }
    /// <summary>
    /// 删除某行
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        AuthorityOperate AuthorityOperate = new AuthorityOperate();
        int AuthorityID = Common.ToInt32(GridView1.DataKeys[e.RowIndex].Values["AuthorityID"].ToString());
        bool i = AuthorityOperate.RemoveAuthority(AuthorityID);
        if (i == true)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "js", "<script>alert('删除成功！');</script>");
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "js", "<script>alert('删除失败！');</script>");
        }
        GetAuthority(AspNetPager1.StartRecordIndex - 1, AspNetPager1.EndRecordIndex);
    }
    /// <summary>
    /// 删除某行
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GridView_Son_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        AuthorityOperate AuthorityOperate = new AuthorityOperate();
        int AuthorityID = Common.ToInt32(GridView_Son.DataKeys[e.RowIndex].Values["AuthorityID"].ToString());
        bool i = AuthorityOperate.RemoveAuthority(AuthorityID);
        if (i == true)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "js", "<script>alert('删除成功！');</script>");
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "js", "<script>alert('删除失败！');</script>");
        }
        GetAuthority(AspNetPager1.StartRecordIndex - 1, AspNetPager1.EndRecordIndex);
    }
    /// <summary>
    /// 点击修改
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        TextBox_AuthorityDescription.Text = GridView1.DataKeys[GridView1.SelectedIndex].Values["AuthorityDescription"].ToString();
        TextBox_AuthorityName.Text = GridView1.DataKeys[GridView1.SelectedIndex].Values["AuthorityName"].ToString();
        TextBox_AuthorityURL.Text = GridView1.DataKeys[GridView1.SelectedIndex].Values["AuthorityURL"].ToString();
        DropDownList_AuthorityRank.Text = GridView1.DataKeys[GridView1.SelectedIndex].Values["AuthorityRank"].ToString();
        TextBox_AuthoritySequence.Text = GridView1.DataKeys[GridView1.SelectedIndex].Values["AuthoritySequence"].ToString();
        HiddenField_AuthorityID.Value = GridView1.DataKeys[GridView1.SelectedIndex].Values["AuthorityID"].ToString();
        DropDownList_AuthorityType.Text = GridView1.DataKeys[GridView1.SelectedIndex].Values["AuthorityType"].ToString();
        Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>ConfirmWindow('Panel_Authority');</script>");
    }
    /// <summary>
    /// 点击修改
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GridView_Son_SelectedIndexChanged(object sender, EventArgs e)
    {
        TextBox_AuthorityDescription.Text = GridView_Son.DataKeys[GridView_Son.SelectedIndex].Values["AuthorityDescription"].ToString();
        TextBox_AuthorityName.Text = GridView_Son.DataKeys[GridView_Son.SelectedIndex].Values["AuthorityName"].ToString();
        TextBox_AuthorityURL.Text = GridView_Son.DataKeys[GridView_Son.SelectedIndex].Values["AuthorityURL"].ToString();
        DropDownList_AuthorityRank.Text = GridView_Son.DataKeys[GridView_Son.SelectedIndex].Values["AuthorityRank"].ToString();
        TextBox_AuthoritySequence.Text = GridView_Son.DataKeys[GridView_Son.SelectedIndex].Values["AuthoritySequence"].ToString();
        HiddenField_AuthorityID.Value = GridView_Son.DataKeys[GridView_Son.SelectedIndex].Values["AuthorityID"].ToString();
        DropDownList_AuthorityType.Text = GridView_Son.DataKeys[GridView_Son.SelectedIndex].Values["AuthorityType"].ToString();
        Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>ConfirmWindow('Panel_Authority');</script>");
    }
    /// <summary>
    /// 翻页
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        GetAuthority(AspNetPager1.StartRecordIndex - 1, AspNetPager1.EndRecordIndex);
    }
    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button1_Click(object sender, EventArgs e)
    {
        AuthorityInfo authorityInfo = new AuthorityInfo();
        authorityInfo.AuthorityDescription = TextBox_AuthorityDescription.Text;
        authorityInfo.AuthorityName = TextBox_AuthorityName.Text;
        authorityInfo.AuthorityRank = Common.ToInt32(DropDownList_AuthorityRank.SelectedValue);
        authorityInfo.AuthorityStatus = 1;
        authorityInfo.AuthorityURL = TextBox_AuthorityURL.Text;
        authorityInfo.AuthorityID = Common.ToInt32(HiddenField_AuthorityID.Value);
        authorityInfo.AuthoritySequence = Common.ToInt32(TextBox_AuthoritySequence.Text);
        authorityInfo.AuthorityType = DropDownList_AuthorityType.SelectedValue;
        AuthorityOperate authorityOperate = new AuthorityOperate();
        bool i = authorityOperate.ModifyAuthority(authorityInfo);
        if (i == true)
        {
            GetAuthority(AspNetPager1.StartRecordIndex - 1, AspNetPager1.EndRecordIndex);
            int authorityID = Common.ToInt32(GridView1.DataKeys[GridView1.SelectedIndex].Values["AuthorityID"].ToString());
            GetSon(authorityID);
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('修改失败，原因可能是权限名称已经存在，请查证');</script>");
        }
    }

    protected void Button_ChangeAuthoritySequence_Click(object sender, EventArgs e)
    {
        using (TransactionScope scope = new TransactionScope())
        {
            int res = 0;
            AuthorityInfo authorityInfo = new AuthorityInfo();
            AuthorityOperate authorityOperate = new AuthorityOperate();
            bool suc = false;
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                TextBox textbox1 = GridView1.Rows[i].FindControl("TextBox1") as TextBox;
                authorityInfo.AuthorityDescription = GridView1.DataKeys[i].Values["AuthorityDescription"].ToString();
                authorityInfo.AuthorityName = GridView1.DataKeys[i].Values["AuthorityName"].ToString();
                authorityInfo.AuthorityRank = Common.ToInt32(GridView1.DataKeys[i].Values["AuthorityRank"].ToString());
                authorityInfo.AuthorityStatus = 1;
                authorityInfo.AuthorityURL = GridView1.DataKeys[i].Values["AuthorityURL"].ToString();
                authorityInfo.AuthorityID = Common.ToInt32(GridView1.DataKeys[i].Values["AuthorityID"].ToString());
                authorityInfo.AuthoritySequence = Common.ToInt32(textbox1.Text);
                authorityInfo.AuthorityType = GridView1.DataKeys[i].Values["AuthorityType"].ToString();
                suc = authorityOperate.ModifyAuthority(authorityInfo);
                if (suc == true)
                {
                    res++;
                }
            }
            if (res == GridView1.Rows.Count)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('排序修改成功');</script>");
                GetAuthority(0, 10);
                scope.Complete();
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('排序修改失败');</script>");
            }
        }
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "getSon")
        {
            int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;//转换为按钮类型，获取其所在的行的索引
            int authorityID = Common.ToInt32(GridView1.DataKeys[index].Values["AuthorityID"].ToString());
            GridView1.SelectedIndex = index;
            GetSon(authorityID);
        }
    }
    /// <summary>
    /// 获取子类
    /// </summary>
    /// <param name="authorityID"></param>
    protected void GetSon(int authorityID)
    {
        AuthorityOperate AuthorityOperate = new AuthorityOperate();
        allAuthority = AuthorityOperate.QueryAuthority();//总共的DataTable
        DataView dv = allAuthority.DefaultView;
        dv.RowFilter = "AuthorityRank=" + authorityID;
        dv.Sort = "AuthoritySequence";
        GridView_Son.DataSource = dv;
        GridView_Son.DataBind();
    }
}