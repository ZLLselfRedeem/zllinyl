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
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL;
using VAGastronomistMobileApp.WebPageDll;
using VAGastronomistMobileApp.WebPageDll.Messages;

public partial class HomeNew_HomeTitleUpdate : System.Web.UI.Page
{
    private MessageFirstTitleOperate mfto = new MessageFirstTitleOperate();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["id"] != null)
            {
                int ID = Common.ToInt32(Request.QueryString["id"]);
                string cityName = Common.ToString(Request.QueryString["cityName"]);
                BindTitleInfo(ID, cityName);
            }
            else
            {
                btnUpdate.Enabled = false;
            }
        }
    }

    protected void BindTitleInfo(int titleId, string cityName)
    {
        DataTable dt = mfto.MessageFirstTitleDetail(titleId);
        TextBox_TitleName.Text = dt.Rows[0]["TitleName"].ToString();
        TextBox_cityName.Text = cityName;
        TextOrder.Text = dt.Rows[0]["TitleIndex"].ToString();
        rblEnable.SelectedValue = dt.Rows[0]["Enable"].ToString() == "True" ? "1" : "0";
        rblIsMaster.SelectedValue = dt.Rows[0]["IsMaster"].ToString() == "True" ? "1" : "0";
        rblIsMerchant.SelectedValue = dt.Rows[0]["IsMerchant"].ToString() == "True" ? "1" : "0";
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("HomeTitle.aspx?cityID=" + Request.QueryString["cityID"]);
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        int id = Common.ToInt32(Request.QueryString["id"]);
        MessageFirstTitle model = new MessageFirstTitle();
        model.Id = id;
        model.TitleName = TextBox_TitleName.Text;
        model.TitleIndex = Common.ToInt32(TextOrder.Text);
        model.Enable = rblEnable.SelectedValue == "1" ? true : false;
        model.IsMaster = rblIsMaster.SelectedValue == "1" ? true : false;
        model.IsMerchant = rblIsMerchant.SelectedValue == "1" ? true : false;
        if (model.IsMaster)
        {
            int cityId = Common.ToInt32(Request.QueryString["cityId"]);
            long count = mfto.GetCountByCityID(model.Id, cityId);
            if (count > 0)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('一个城市只能有一个主要标签');</script>");
                return;
            }
        }

        if (model.IsMerchant)
        {
            int cityId = Common.ToInt32(Request.QueryString["cityId"]);
            long count = mfto.GetCountByCityIDIsMerchant(model.Id, cityId);
            if (count > 0)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('一个城市只能有一个商户标签');</script>");
                return;
            }
        }

        int Result =mfto.UpdateMessageFirstTitle(model);
        if (Result >= 1)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('修改成功！');window.location.href='HomeTitle.aspx?cityID=" + Request.QueryString["cityID"] + "'</script>");
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('修改失败！');</script>");
        }
    }

    
}