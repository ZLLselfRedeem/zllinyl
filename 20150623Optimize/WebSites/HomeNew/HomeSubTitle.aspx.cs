﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.Model.HomeNew;
using VAGastronomistMobileApp.SQLServerDAL.HomeNew;
using VAGastronomistMobileApp.WebPageDll;
using Web.Control.DDL;

public partial class HomeNew_HomeSubTitle : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["cityID"] != null)
            {
                new CityDropDownList().BindCity(ddlCity, Request.QueryString["cityID"]);
                BindTitle(ddlTitle, Request.QueryString["titleID"], Common.ToInt32(ddlCity.SelectedValue));
            }
            else
            {
                new CityDropDownList().BindCity(ddlCity,"87");
                BindTitle(ddlTitle, "0", Common.ToInt32(ddlCity.SelectedValue));
            }

            BindTitleList(Common.ToInt32(ddlCity.SelectedValue), Common.ToInt32(ddlTitle.SelectedValue));
        }
    }

    protected List<TitleViewModel> GetHandleTitle(int cityID, out int NonAdTitleID)
    {
        return TitleManager.SelectHandleTitle(cityID, out NonAdTitleID);
    }

    /// <summary>
    /// 绑定一级下拉菜单
    /// </summary>
    /// <param name="ddl_Title"></param>
    /// <param name="titleID"></param>
    /// <param name="cityID"></param>
    public void BindTitle(DropDownList ddl_Title, string titleID, int cityID)
    {
        ddl_Title.Items.Clear();
        int NonAdTitleID;
        List<TitleViewModel> data = GetHandleTitle(cityID, out NonAdTitleID);
        ddl_Title.DataSource = data;
        ddl_Title.DataTextField = "titleName";
        ddl_Title.DataValueField = "titleId";
        ddl_Title.DataBind();
        if (titleID != "0")
        {
            ddl_Title.SelectedValue = titleID;
        }
        else
        {
            ddl_Title.SelectedValue = Convert.ToString(NonAdTitleID);
        }
    }

    protected void BindTitleList(int cityID, int titleID)
    {
        DataTable dt = null;
        dt = TitleManager.SelectSubTitle(cityID, titleID);
        GridView_City.DataSource = dt.DefaultView;
        GridView_City.DataBind();
    }

    protected void GridView_City_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;//转换为按钮类型，获取其所在的行的索引
        int id = Common.ToInt32(GridView_City.DataKeys[index].Values["id"]);
        //int status = Common.ToInt32(GridView_City.DataKeys[index].Values["status"]);
        int firstTitleID = SubTitleManager.SelectSubTitleByID(id).FirstTitleID;
        string ruleType = Common.ToString(GridView_City.DataKeys[index].Values["type"]);
        int type = TitleManager.QueryTitle(firstTitleID).Type;
        switch (e.CommandName.ToString())
        {
            case "edit":
                Response.Redirect("HomeSubTitleUpdate.aspx?id=" + id + "&cityID=" + ddlCity.SelectedValue + "&cityName=" + ddlCity.SelectedItem + "&titleID=" + ddlTitle.SelectedValue + "&titleName=" + ddlTitle.SelectedItem + "&type=" + ruleType);
                break;
            case "delete":
                if (type == 1)
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('删除失败，非广告类二级栏目不能删除');</script>");
                }
                else
                {
                    if (SubTitleManager.NumLessThanTwo(firstTitleID))
                    {
                        Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('删除失败，二级栏目不能少于两个！');</script>");
                    }
                    else
                    {
                        bool i = TitleManager.RemoveSubTitle(Common.ToInt32(id));
                        if (i == true)
                        {
                            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('删除成功！');</script>");
                        }
                        else
                        {
                            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('删除失败！');</script>");
                        }
                        BindTitleList(Convert.ToInt32(ddlCity.SelectedValue), Convert.ToInt32(ddlTitle.SelectedValue));
                    }
                }
                break;
            //case "clientUpdate":
            //    int result = 0;
            //    if (status == 1)
            //    {
            //        result = SubTitleManager.ClientUpdate(id, 0);
            //        if (result >= 1)
            //        {
            //            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('栏目下线成功！');</script>");
            //        }
            //        else
            //        {
            //            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('栏目下线失败！');</script>");
            //        }
            //    }
            //    else
            //    {
            //        if (TitleManager.SubIndexClash(Common.ToInt32(Request.QueryString["titleIndex"]), Common.ToInt32(Request.QueryString["firstTitleIndex"])))
            //        {
            //            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('新建失败,二级栏目排序冲突！');</script>");
            //        }
            //        else
            //        {
            //            result = SubTitleManager.ClientUpdate(id, 1);
            //            if (result >= 1)
            //            {
            //                Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('栏目上线成功！');</script>");
            //            }
            //            else
            //            {
            //                Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('栏目上线失败！');</script>");
            //            }
            //        }
            //    }
            //    BindTitleList(Convert.ToInt32(ddlCity.SelectedValue), Convert.ToInt32(ddlTitle.SelectedValue));
            //    break;
            default:
                break;
        }
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        BindTitleList(Convert.ToInt32(ddlCity.SelectedValue), Convert.ToInt32(ddlTitle.SelectedValue));
    }

    protected void Add_Click(object sender, EventArgs e)
    {
        //int NonAdTitleID;
        //GetHandleTitle(Common.ToInt32(ddlCity.SelectedValue), out NonAdTitleID);
        //if (ddlTitle.SelectedValue == Convert.ToString(NonAdTitleID))
        //{
        //    Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('非广告一级栏目下不能新建二级栏目！');</script>");
        //}
        //else
        //{
        Response.Redirect("HomeSubTitleAdd.aspx?cityID=" + ddlCity.SelectedValue + "&cityName=" + ddlCity.SelectedItem + "&titleID=" + ddlTitle.SelectedValue + "&titleName=" + ddlTitle.SelectedItem);

    }

    public string GetNumConfirm(string firstTitleID)
    {
        int firstID = Common.ToInt32(firstTitleID);
        if (SubTitleManager.NumLessThanTwo(firstID) || TitleManager.QueryTitle(firstID).Type == 1)
        {
            return "";
        }
        else
        {
            return "return confirm('你确定删除吗？')";
        }
    }

    protected void ddlCity_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindTitle(ddlTitle, "0", Common.ToInt32(ddlCity.SelectedValue));
    }

    protected void GridView_City_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
    }

    protected void GridView_City_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
    }

    protected void GridView_City_RowEditing(object sender, GridViewEditEventArgs e)
    {
    }
}