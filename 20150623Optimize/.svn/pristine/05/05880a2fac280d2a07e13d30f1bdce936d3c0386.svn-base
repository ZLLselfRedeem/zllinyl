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

public partial class HomeNew_HomeTitleAdd : System.Web.UI.Page
{
    private MessageFirstTitleOperate mfto = new MessageFirstTitleOperate();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["cityID"] != null)
            {
                TextCityName.Text = Request.QueryString["cityName"];
                rblEnable.SelectedValue = "1";
                rblIsMaster.SelectedValue = "0";
                rblIsMerchant.SelectedValue = "0";
            }
            else
            {
                btnUpdate.Enabled = false;
            }
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("HomeTitle.aspx?cityID=" + Request.QueryString["cityID"]);
    }


    private MessageFirstTitle modelGenerator()
    {
        MessageFirstTitle title = new MessageFirstTitle()
        {
            Id = Common.ToInt32(Request.QueryString["id"]),
            CityID = Common.ToInt32(Request.QueryString["cityID"]),
            TitleName = TextBox_TitleName.Text,
            TitleIndex = Common.ToInt32(TextBox_TitleIndex.Text),
            Status = true,
            Enable = rblEnable.SelectedValue == "1" ? true : false,
            IsMaster = rblIsMaster.SelectedValue == "1" ? true : false,
            IsMerchant = rblIsMerchant.SelectedValue == "1" ? true : false,
            CreateTime = DateTime.Now,
            CreateBy = Common.ToInt32(((VAEmployeeLoginResponse)HttpContext.Current.Session["UserInfo"]).employeeID),
        };
        return title;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        MessageFirstTitle model = modelGenerator();
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

        int result = mfto.InsertMessageFirstTitle(model);
        if (result == 1)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('新建成功！');window.location.href='HomeTitle.aspx?cityID=" + model.CityID + "'</script>");
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('新建失败！');</script>");
        }
    }
}