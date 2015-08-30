﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.SQLServerDAL;
using VAGastronomistMobileApp.SQLServerDAL.HomeNew;
using VAGastronomistMobileApp.WebPageDll;
using Web.Control.DDL;

public partial class HomeNew_CityManage : System.Web.UI.Page
{
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
                new CityDropDownList().BindCity(ddlCity,"87");
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
        DataTable dtCity = null;
        dtCity = TitleManager.SelectTitle(cityID);
        GridView_City.DataSource = dtCity;
        GridView_City.DataBind();
        DataTable dtOfCity = null;
        dtOfCity = TitleManager.SelectAllSubTitle(Common.ToInt32(ddlCity.SelectedValue));
        for (int i = 0; i < this.GridView_City.Rows.Count; i++)
        {
            int titleId = Common.ToInt32(GridView_City.DataKeys[i].Values["id"].ToString());
            int type = TitleManager.QueryTitle(titleId).Type;
            Label lbURL = (Label)GridView_City.Rows[i].FindControl("lbURL");
            DataView dv = dtOfCity.AsDataView();
            dv.RowFilter = "id=" + titleId;
            if (type == 2)
            {
                for (int j = 0; j < dv.Count; j++)
                {
                    if (Common.ToInt32(dv[j]["status"]) == 1)
                    {
                        lbURL.Text += "<a href='HomeAdvert.aspx?secondTitleid=" + dv[j]["subid"].ToString() + "&firstTitleID=" + titleId + "&cityID=" + ddlCity.SelectedValue + "'> &nbsp;&nbsp;&nbsp;&nbsp;<font color=blue>" + dv[j]["subtitleName"].ToString() + "</font></a>";
                    }
                    else
                    {
                        lbURL.Text += "<a href='HomeAdvert.aspx?secondTitleid=" + dv[j]["subid"].ToString() + "&firstTitleID=" + titleId + "&cityID=" + ddlCity.SelectedValue + "'> &nbsp;&nbsp;&nbsp;&nbsp;<font color=gray>" + dv[j]["subtitleName"].ToString() + "</font></a>";
                    }
                }
            }
            else
            {
                for (int j = 0; j < dv.Count; j++)
                {
                    lbURL.Text += "<a> &nbsp;&nbsp;&nbsp;&nbsp;<font color=gray>" + dv[j]["subtitleName"].ToString() + "</font></a>";
                }
            }
        }
    }

    protected void GridView_City_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;//转换为按钮类型，获取其所在的行的索引
        int id = Common.ToInt32(GridView_City.DataKeys[index].Values["id"]);
        int cityID = Common.ToInt32(GridView_City.DataKeys[index].Values["cityID"]);
        string cityName = GridView_City.DataKeys[index].Values["cityName"].ToString();
        int status = Common.ToInt32(GridView_City.DataKeys[index].Values["status"]);
        int titleIndex = Common.ToInt32(GridView_City.DataKeys[index].Values["titleIndex"]);
        int type = TitleManager.QueryTitle(id).Type;
        switch (e.CommandName.ToString())
        {
            case "edit":
                Response.Redirect("HomeTitleUpdate.aspx?id=" + id + "&cityID=" + ddlCity.SelectedValue + "&cityName=" + ddlCity.SelectedItem);
                break;
            case "delete":
                type = TitleManager.QueryTitle(id).Type;
                if (type == 1)
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('非广告类型一级栏目不能删除！');</script>");
                }
                else
                {
                    bool i = TitleManager.RemoveTitle(id);
                    if (i == true)
                    {
                        Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('删除成功！');</script>");
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('删除失败！');</script>");
                    }
                    BindCityList(Convert.ToInt32(ddlCity.SelectedValue));
                }
                break;
            case "clientUpdate":
                int updateResult = 0;
                if (status == 0)
                {
                    string uplineReuslt = TitleManager.Upline(cityID, id, titleIndex);
                    if (!string.IsNullOrEmpty(uplineReuslt))
                    {
                        Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('" + uplineReuslt + "');</script>");
                    }
                    else
                    {
                        //if (TitleManager.IndexClash(titleIndex, cityID))
                        //{
                        //    Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('栏目上线失败，一级栏目排序冲突');</script>");
                        //}
                        //else
                        //{
                        updateResult = TitleManager.ClientUpdate(id, 1);
                        if (updateResult >= 1)
                        {
                            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('栏目上线成功！');</script>");
                        }
                        else
                        {
                            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('栏目上线失败！');</script>");
                        }
                        //}
                    }
                }
                else
                {
                    if (type == 1)
                    {
                        Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('非广告栏目不能下线！');</script>");
                    }
                    else
                    {
                        updateResult = TitleManager.ClientUpdate(id, 0);

                        if (updateResult >= 1)
                        {
                            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('栏目下线成功！');</script>");
                        }
                        else
                        {
                            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('栏目下线失败！');</script>");
                        }
                    }
                }
                BindCityList(Convert.ToInt32(ddlCity.SelectedValue));
                break;
            default:
                break;
        }
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        int cityID = Common.ToInt32(ddlCity.SelectedValue);
        BindCityList(cityID);
    }

    protected void Add_Click(object sender, EventArgs e)
    {
        //if (ddlCity.SelectedValue == "0")
        //{
        //    Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('请选择要创建栏目的城市！');</script>");
        //}
        //else
        //{
        Response.Redirect("HomeTitleAdd.aspx?cityID=" + ddlCity.SelectedValue + "&cityName=" + ddlCity.SelectedItem);
        //}
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

    protected string GetDeleteConfirm(string type,string titleName)
    {
        string reStr = string.Empty;
        if (type=="广告")
        {
            reStr = string.Format("return confirm('你确定删除栏目{0}吗？')", titleName);
        }
        return reStr;
    }
    protected string GetUplineConfirm(int status, string type)
    {
        string reStr = string.Empty;
        if (type=="广告")
        {
            if (status == 1)
            {
                reStr = "return confirm('确定栏目下线吗？')";
            }
            else
            {
                reStr = "return confirm('确定栏目上线吗？')";
            }
        }
        return reStr;
    }
}