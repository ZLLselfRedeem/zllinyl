using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.DBUtility;
using VAGastronomistMobileApp.Model.HomeNew;
using VAGastronomistMobileApp.SQLServerDAL.HomeNew;
using VAGastronomistMobileApp.WebPageDll;

public partial class HomeNew_HomeTitleUpdate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["id"] != null)
            {
                //int firstType = GetTitleStatus(Common.ToInt32(Request.QueryString["titleID"]));
                //int type = Common.ToInt32(Request.QueryString["type"]);
                GetSubtitleInfo();
            }
        }
    }

    private int GetTitleStatus(int firstTitleID)
    {
        Title title = TitleManager.QueryTitle(firstTitleID);
        return title.Type;
    }

    private void GetSubtitleInfo()
    {
        int subtitleID = Common.ToInt32(Request.QueryString["id"]);
        DataTable subTitle = TitleManager.SelectSubTitle(subtitleID);
        TextSubTitleName.Text = Common.ToString(subTitle.Rows[0]["titleName"]);
        TextCityName.Text = Common.ToString(subTitle.Rows[0]["cityName"]);
        TextTitleName.Text = Common.ToString(subTitle.Rows[0]["firstName"]);
        TextTitleIndex.Text = Common.ToString(subTitle.Rows[0]["titleIndex"]);
        ddlRule.Text = Convert.ToString(Request.QueryString["type"]);
        //if (firstType == 1)
        //{
        //    if (type == 1)
        //    {
        //        ddlRule.Text = "智能排序";
        //    }
        //    else
        //    {
        //        ddlRule.Text = "由近及远";
        //    }
        //}
        //else
        //{
        //    if (type == 1)
        //    {
        //        ddlRule.Text = "手动排序";
        //    }
        //    else
        //    {
        //        ddlRule.Text = "由近及远";
        //    }
        //}
        //radListStatus.SelectedValue = Common.ToString(subTitle.Rows[0]["status"]);
    }

    //public void BindRule(DropDownList ddlRule)
    //{
    //    var cityOperate = new CityOperate();
    //    ddlRule.DataTextField = "ruleName";
    //    ddlRule.DataValueField = "ruleId";
    //    ddlRule.DataBind();
    //    ddlRule.Items.Add(new ListItem("手动排序", "1"));
    //    ddlRule.Items.Add(new ListItem("由近到远", "2"));
    //}

    //public void BindNonAdRule(DropDownList ddlRule)
    //{
    //    var cityOperate = new CityOperate();
    //    ddlRule.DataTextField = "ruleName";
    //    ddlRule.DataValueField = "ruleId";
    //    ddlRule.DataBind();
    //    ddlRule.Items.Add(new ListItem("智能排序", "1"));
    //    ddlRule.Items.Add(new ListItem("由近到远", "2"));
    //}

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("HomeSubTitle.aspx?cityID=" + Request.QueryString["cityID"] + "&titleID=" + Request.QueryString["titleID"]);
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        int result = 0;
        int subtitleID = Common.ToInt32(Request.QueryString["id"]);
        SubTitle subTitle = SubTitleManager.SelectSubTitleByID(subtitleID);
        int firstTitleID = subTitle.FirstTitleID;
        string subtitleName = TextSubTitleName.Text;
        int subTitleIndex = Common.ToInt32(TextTitleIndex.Text);
        //string status = radListStatus.SelectedValue;
        //string firstStatus = Common.ToString(TitleManager.SelectTitleByID(firstTitleID).Rows[0]["status"]);

        //if (firstStatus == "0" && status == "1")
        //{
        //    Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('请先设置一级栏目上线，才能设置二级栏目上线！');</script>");
        //}
        //else
        //{
        if (TitleManager.SubIndexClash(subTitleIndex, firstTitleID, subtitleID))
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('修改失败,二级栏目排序冲突！');</script>");
        }
        else
        {
            result = SubTitleManager.Update(subtitleID, subtitleName, subTitleIndex);
            if (result == 1)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('修改成功！');window.location.href='HomeSubTitle.aspx?cityID=" + Request.QueryString["cityID"] + "&titleID=" + Request.QueryString["titleID"] + "'</script>");
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('修改失败！');</script>");
            }
        }
        //}
    }
}