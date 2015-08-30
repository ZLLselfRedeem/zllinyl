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
using Web.Control.DDL;

public partial class HomeNew_HomeTitleAdd : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["cityID"] != null)
            {
                string cityID  = Request.QueryString["cityID"];
                new CityDropDownList().BindCity(TextCityName, cityID);
                TextBox_TitleType.Text = "广告";
                radListStatus.SelectedValue = "0";
            }
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("HomeTitle.aspx?cityID=" + Request.QueryString["cityID"]);
    }

    private Title TitleGenerator()
    {
        Title title = new Title()
        {
            //ID = Common.ToInt32(Request.QueryString["id"]),
            CityID = Common.ToInt32(TextCityName.SelectedValue),
            TitleName = TextBox_TitleName.Text,
            TitleIndex = Common.ToInt32(TextBox_TitleIndex.Text),
            Type = 2,
            Status = Common.ToInt32(radListStatus.SelectedValue),
            CreateTime = DateTime.Now,
            CreateBy = Common.ToInt32(((VAEmployeeLoginResponse)HttpContext.Current.Session["UserInfo"]).employeeID),
            IsDelete = false
        };
        return title;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        int result = 0;
        Title title = TitleGenerator();
        //string isClientShow = Common.ToString(new CityManager().SelectCity(title.CityID).Rows[0]["isClientShow"]);

        // string uplineReuslt = TitleManager.Upline(title.CityID, title.ID, title.TitleIndex);
        if (title.Status == 1 /*&& !string.IsNullOrEmpty(uplineReuslt)*/)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('新建失败，新建一级栏目时不能直接上线，请选择客户端下线！');</script>");
        }
        else
        {
            //if (title.Status == 1 && TitleManager.IndexClash(title.TitleIndex, title.CityID))
            //{
            //    Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('新建失败,一级栏目排序冲突！');</script>");
            //}
            //else
            //{
            result = TitleManager.Insert(title);
            if (result == 1)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('新建成功！');window.location.href='HomeTitle.aspx?cityID=" + title.CityID + "'</script>");
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('新建失败！');</script>");
            }
            //}
        }
    }
}