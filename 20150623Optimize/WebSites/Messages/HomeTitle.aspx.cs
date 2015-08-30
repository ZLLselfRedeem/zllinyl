using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL;
using VAGastronomistMobileApp.WebPageDll;
using VAGastronomistMobileApp.WebPageDll.Messages;
using Web.Control.DDL;

public partial class HomeNew_CityManage : System.Web.UI.Page
{
    private MessageFirstTitleOperate mfto = new MessageFirstTitleOperate();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["cityID"] != null)
            {
                string cityID = Request.QueryString["cityID"];
                new CityDropDownList().BindCity(ddlCity, cityID);
            }
            else
            {
                new CityDropDownList().BindCity(ddlCity);
                ddlCity.Items.RemoveAt(ddlCity.Items.Count - 1);
            }
            BindCityList(Convert.ToInt32(ddlCity.SelectedValue));
        }
    }

    /// <summary>
    /// 根据城市ID获取一级栏目列表
    /// </summary>
    /// <param name="cityID"></param>
    private void BindCityList(int cityID)
    {
        DataTable dt = mfto.MessageFirstTitles(cityID);
        DataTable dtPage = new DataTable();

        if (dt.Rows.Count > 0)
        {
            int tableCount = dt.Rows.Count;
            AspNetPager1.RecordCount = tableCount;
            dtPage = Common.GetPageDataTable(dt, (AspNetPager1.CurrentPageIndex - 1) * AspNetPager1.PageSize, (AspNetPager1.CurrentPageIndex) * AspNetPager1.PageSize);
        }
        else
        {
            AspNetPager1.RecordCount = 0;
        }

        GridView_City.DataSource = dtPage;
        GridView_City.DataBind();
    }

    protected void GridView_City_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;//转换为按钮类型，获取其所在的行的索引
        int id = Common.ToInt32(GridView_City.DataKeys[index].Values["id"]);
        int cityID = Common.ToInt32(GridView_City.DataKeys[index].Values["cityID"]);
        string cityName = GridView_City.DataKeys[index].Values["cityName"].ToString();
        int Enable = Common.ToInt32(GridView_City.DataKeys[index].Values["Enable"]);
        int IsMaster = Common.ToInt32(GridView_City.DataKeys[index].Values["IsMaster"]);
        switch (e.CommandName.ToString())
        {
            case "edit":
                Response.Redirect("HomeTitleUpdate.aspx?id=" + id + "&cityID=" + ddlCity.SelectedValue + "&cityName=" + ddlCity.SelectedItem);
                break;
            case "delete":
                int i = mfto.UpdateMessageFirstTitleStatus(id,false);
                if (i >0)
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('删除成功！');</script>");
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('删除失败！');</script>");
                    return;
                }
                BindCityList(Convert.ToInt32(ddlCity.SelectedValue));
                break;
            case "clientUpdate":
                if (Enable == 0)
                {
                    if (IsMaster == 1)
                    {
                        long count = mfto.GetCountByCityID(id, cityID);
                        if (count > 1)
                        {
                            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('一个城市只能有一个主要标签！');</script>");
                            return;
                        }
                    }
                    mfto.UpdateMessageFirstTitleEnable(id,true);
                }
                else
                {
                     mfto.UpdateMessageFirstTitleEnable(id,false);
                }
                BindCityList(Convert.ToInt32(ddlCity.SelectedValue));
                break;
            default:
                break;
        }
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        AspNetPager1.CurrentPageIndex = 1;
        int cityID = Common.ToInt32(ddlCity.SelectedValue);
        BindCityList(cityID);
    }

    protected void Add_Click(object sender, EventArgs e)
    {
        if (ddlCity.SelectedValue == "0")
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('请选择要创建栏目的城市！');</script>");
        }
        else
        {
            Response.Redirect("HomeTitleAdd.aspx?cityID=" + ddlCity.SelectedValue + "&cityName=" + ddlCity.SelectedItem);
        }
    }

    protected void GridView_City_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

    }

    protected void GridView_City_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void GridView_City_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //DataRowView row = (DataRowView)e.Row.DataItem;
        //if(row == null)
        //{
        //    return;
        //}

        //int titleId = Convert.ToInt32(row["id"].ToString());
        //DataTable dtCity = null;
        //dtCity = TitleManager.CreateSubTitle(Common.ToInt32(ddlCity.SelectedValue),titleId);
        //var PlaceHolder1 = e.Row.FindControl("PlaceHolder1") as PlaceHolder;
        //foreach (DataRow dataRow in dtCity.Rows)
        //{
        //        LinkButton button1 = new LinkButton() { };
        //        button1.Text = Convert.ToString(dataRow["titleName"]);
        //        button1.PostBackUrl = "HomeAdvertDetail.aspx?cityID=" + ddlCity.SelectedValue 
        //            + "&firstTitleID=" + titleId + "&secondTitleID=" + Convert.ToString(dataRow["id"]);

        // }
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        int cityID = Common.ToInt32(ddlCity.SelectedValue);
        BindCityList(cityID);
    }

    //分页数目控制
    protected void Button_LargePageCount_Click(object sender, EventArgs e)
    {
        Button Button = (Button)sender;
        Label_LargePageCount.Text = "";
        switch (Button.CommandName)
        {
            case "button_10":
                Button_10.CssClass = "tabButtonBlueClick";
                Button_50.CssClass = "tabButtonBlueUnClick";
                Button_100.CssClass = "tabButtonBlueUnClick";
                Label_LargePageCount.Text = "10";
                break;
            case "button_50":
                Button_10.CssClass = "tabButtonBlueUnClick";
                Button_50.CssClass = "tabButtonBlueClick";
                Button_100.CssClass = "tabButtonBlueUnClick";
                Label_LargePageCount.Text = "50";
                break;
            case "button_100":
                Button_10.CssClass = "tabButtonBlueUnClick";
                Button_50.CssClass = "tabButtonBlueUnClick";
                Button_100.CssClass = "tabButtonBlueClick";
                Label_LargePageCount.Text = "100";
                break;
            default:
                break;
        }
        AspNetPager1.CurrentPageIndex = 0;
    }
}