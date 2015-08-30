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
using VAGastronomistMobileApp.SQLServerDAL;
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
                int titleId = Common.ToInt32(Request.QueryString["id"]);
                string cityName = Common.ToString(Request.QueryString["cityName"]);
                BindTitleInfo(titleId, cityName);
            }
        }
    }

    protected void BindTitleInfo(int titleId, string cityName)
    {
        Title titleInfo = new Title();
        titleInfo = TitleManager.QueryTitle(titleId);
        TextBox_TitleName.Text = titleInfo.TitleName;
        TextBox_cityName.Text = cityName;
        TextOrder.Text = Common.ToString(titleInfo.TitleIndex);
        radListStatus.SelectedValue = Common.ToString(titleInfo.Status);
        if (titleInfo.Type == 2)
        {
            TextBox_TitleType.Text = "广告";
        }
        else
        {
            TextBox_TitleType.Text = "基本";
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("HomeTitle.aspx?cityID=" + Request.QueryString["cityID"]);
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        int result = 0;
        int id = Common.ToInt32(Request.QueryString["id"]);
        int cityID = Common.ToInt32(TitleManager.SelectTitleByID(id).Rows[0]["cityID"]);
        //string isClientShow = Common.ToString(new CityManager().SelectCity(cityID).Rows[0]["isClientShow"]);
        string titleName = TextBox_TitleName.Text;
        int titleIndex = Common.ToInt32(TextOrder.Text);
        int status = Common.ToInt32(radListStatus.SelectedValue);
        string uplineReuslt = TitleManager.Upline(cityID, id, titleIndex);
        
        if (status == 1)
        {
            if (!string.IsNullOrEmpty(uplineReuslt))
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('" + uplineReuslt + "');</script>");
            }
            else
            {
                result = TitleManager.TitleUpdate(id, titleName, titleIndex, status);
                if (result >= 1)
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('修改成功！');window.location.href='HomeTitle.aspx?cityID=" + Request.QueryString["cityID"] + "'</script>");
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('修改失败！');</script>");
                }
            }
        }
        else
        {
            int type = TitleManager.QueryTitle(id).Type;
            if (type == 1)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('非广告栏目不能下线！');</script>");
            }
            else
            {
                result = TitleManager.TitleUpdate(id, titleName, titleIndex, status);
                if (result >= 1)
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('修改成功！');window.location.href='HomeTitle.aspx?cityID=" + Request.QueryString["cityID"] + "'</script>");
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('修改失败！');</script>");
                }
            }
            //}
        }
    }


}