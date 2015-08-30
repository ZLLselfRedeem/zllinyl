﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.WebPageDll;
using Web.Control;
using System.Web.UI.HtmlControls;
using Web.Control.DDL;
using VAGastronomistMobileApp.Model.HomeNew;
using VAGastronomistMobileApp.SQLServerDAL.HomeNew;
using VAGastronomistMobileApp.SQLServerDAL;

public partial class HomeNew_HomeAdvert : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            new CityDropDownList().BindCity(ddlCity,"87");

            // 从一级目录列表页面链接过来的
            if (!string.IsNullOrEmpty(Request.QueryString["cityID"]))
            {
                ddlCity.SelectedValue = Convert.ToString(Request.QueryString["cityID"]);
                BindTitle(ddlTitle, Convert.ToInt32(ddlCity.SelectedValue));
                ddlTitle.SelectedValue = Convert.ToString(Request.QueryString["firstTitleID"]);
                BindSubTitle(ddlSubTitle, Convert.ToInt32(string.IsNullOrEmpty(Request.QueryString["firstTitleID"]) ? "0":Request.QueryString["firstTitleID"]));
                ddlSubTitle.SelectedValue = Convert.ToString(Request.QueryString["secondTitleID"]);
            }
            else
            {
                // 默认选中杭州
                ddlCity.SelectedValue = "87";
                BindTitle(ddlTitle, Convert.ToInt32(ddlCity.SelectedValue));
                BindSubTitle(ddlSubTitle, Common.ToInt32(ddlTitle.SelectedValue));
            }
            AspNetPager1.RecordCount = 100;
            BindGridViewAdvert(0, 10);
        }
    }

    private void BindTitle(DropDownList ddl_Title, int cityID)
    {
        ddl_Title.Items.Clear();
        int NonAdTitleID;
        List<TitleViewModel> data = TitleManager.SelectHandleTitle(cityID, out NonAdTitleID);
        data = data.FindAll(t => t.titleID != NonAdTitleID).ToList();
        ddl_Title.DataSource = data;
        ddl_Title.DataTextField = "titleName";
        ddl_Title.DataValueField = "titleId";
        ddl_Title.DataBind();
        ddl_Title.Items.Insert(0, new ListItem("", "0"));
        //ddl_Title.SelectedValue = Convert.ToString(NonAdTitleID);
    }

    private void BindSubTitle(DropDownList ddl_SubTitle, int firstTitleID)
    {
        ddlSubTitle.Items.Clear();
        List<TitleViewModel> data = TitleManager.SelectHandleSubTitle(firstTitleID);
        ddl_SubTitle.DataSource = data;
        ddl_SubTitle.DataTextField = "titleName";
        ddl_SubTitle.DataValueField = "titleId";
        ddl_SubTitle.DataBind();
        ddl_SubTitle.Items.Insert(0, new ListItem("", "0"));
        //ddl_SubTitle.SelectedValue = "0";
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        BindGridViewAdvert(AspNetPager1.StartRecordIndex - 1, AspNetPager1.EndRecordIndex);
    }

    /// <summary>
    /// 全选
    /// </summary>
    /// <param name="checkFlag"></param>
    private void CheckAll(bool checkFlag)
    {
        for (int i = 0; i < GridView_Advert.Rows.Count; i++)
        {
            HtmlInputCheckBox ckbSelect = (HtmlInputCheckBox)GridView_Advert.Rows[i].FindControl("ckbSelect");

            ckbSelect.Checked = checkFlag;
        }
    }
    /// <summary>
    /// 城市下拉变更事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlCity_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindTitle(ddlTitle, Convert.ToInt32(ddlCity.SelectedValue));
        //if (!string.IsNullOrEmpty(ddlTitle.SelectedValue))
        //{
            BindSubTitle(ddlSubTitle, Convert.ToInt32(ddlTitle.SelectedValue=="" ? "0":ddlTitle.SelectedValue));
        //}
    }
    /// <summary>
    /// 一级目录下拉变更事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlTitle_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindSubTitle(ddlSubTitle, Convert.ToInt32(ddlTitle.SelectedValue));
    }

    /// <summary>
    /// 查询
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindGridViewAdvert(AspNetPager1.StartRecordIndex - 1, AspNetPager1.EndRecordIndex);
    }

    protected void BindGridViewAdvert(int startIndex, int endIndex)
    {
        int cityID = Convert.ToInt32(ddlCity.SelectedValue);
        int firstTitleID = Convert.ToInt32(string.IsNullOrEmpty(ddlTitle.SelectedValue) ? "0":ddlTitle.SelectedValue);
        int secondTitleID = Convert.ToInt32(string.IsNullOrEmpty(ddlSubTitle.SelectedValue) ? "0" : ddlSubTitle.SelectedValue);
        string shopName = txtShop.Text.Trim();
        AdvertManager advertManager = new AdvertManager();
        var listAdverts = advertManager.SelectAdvertByKey(cityID, firstTitleID, secondTitleID, shopName);
        
        AspNetPager1.RecordCount = listAdverts.Count > 0 ? listAdverts.Count : 10;
        lbCount.InnerText = "共计:" + listAdverts.Count + "条";
        if (!listAdverts.Count.ToString().Equals(hidTotalCount.Value))
        {
            startIndex = 0;
            endIndex = 10;
            AspNetPager1.CurrentPageIndex = 0;
        }
        hidTotalCount.Value = listAdverts.Count.ToString();
        DataTable dtAdvert = Common.ListToDataTable<AdvertShop>(listAdverts);
        string sortedField = "";
        if (ViewState["SortedField"] != null)
        {
            Dictionary<string, string> sorted = (Dictionary<string, string>)ViewState["SortedField"];
            foreach (KeyValuePair<string, string> kvp in sorted)
            {
                sortedField = kvp.Key + "  " + kvp.Value;
            }
            dtAdvert.DefaultView.Sort = sortedField;
        }
        dtAdvert = dtAdvert.DefaultView.ToTable();
        dtAdvert = Common.GetPageDataTable(dtAdvert, startIndex, endIndex);
        GridView_Advert.DataSource = dtAdvert;
        GridView_Advert.DataBind();
    }
    /// <summary>
    /// 预览
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnShow_Click(object sender, EventArgs e)
    {
        if (ddlCity.SelectedValue == "0")
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('请选择城市！');</script>");
            return;
        }
        if (ddlTitle.SelectedValue == "0")
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('请选择一级标题！');</script>");
            return;
        }
        if (ddlSubTitle.SelectedValue == "0")
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('请选择二级标题！');</script>");
            return;
        }
        Response.Redirect("HomeAdvertShow.aspx?cityID=" + ddlCity.SelectedValue + "&firstTitleID=" + ddlTitle.SelectedValue + "&secondTitleID=" + ddlSubTitle.SelectedValue);
    }
    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        AdvertManager advertManager = new AdvertManager();
        bool isDeleteOK = false;
        for (int i = 0; i < GridView_Advert.Rows.Count; i++)
        {
            HtmlInputCheckBox cbSelect = (HtmlInputCheckBox)GridView_Advert.Rows[i].FindControl("ckbSelect");
            string advertID = GridView_Advert.DataKeys[i].Values["id"].ToString();
            if (cbSelect.Checked)
            {
                isDeleteOK = advertManager.DeleteAdvert(Common.ToInt32(advertID));
            }
        }
        if (isDeleteOK)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('删除成功！');</script>");
            BindGridViewAdvert(AspNetPager1.StartRecordIndex - 1, AspNetPager1.EndRecordIndex);
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('删除失败！');</script>");
        }
    }
    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        int cityID = Convert.ToInt32(ddlCity.SelectedValue);
        string cityName = ddlCity.SelectedItem.Text;
        int firstTitleID = Convert.ToInt32(string.IsNullOrEmpty(ddlTitle.SelectedValue) ? "0":ddlTitle.SelectedValue);
        int secondTitleID = Convert.ToInt32(string.IsNullOrEmpty(ddlSubTitle.SelectedValue) ? "0":ddlSubTitle.SelectedValue);
        Response.Redirect("HomeAdvertDetail.aspx?cityID=" + cityID + "&firstTitleID=" + firstTitleID + "&secondTitleID=" + secondTitleID + "&cityName=" + cityName);
    }
    protected void GridView_Advert_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if(e.CommandName=="Sort")
        {
            return;
        }
        int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;//转换为按钮类型，获取其所在的行的索引
        string advertID = GridView_Advert.DataKeys[index].Values["id"].ToString();
        string status = GridView_Advert.DataKeys[index].Values["status"].ToString();
        int orderIndex = Common.ToInt32(GridView_Advert.DataKeys[index].Values["index"]);
        int cityID = Common.ToInt32(GridView_Advert.DataKeys[index].Values["cityID"]);
        string secondTitleID = GridView_Advert.DataKeys[index].Values["secondTitleID"].ToString();
        string firstTitleID = GridView_Advert.DataKeys[index].Values["firstTitleID"].ToString();
        switch (e.CommandName.ToString())
        {
            case "edit":
                Response.Redirect("HomeAdvertDetail.aspx?advertID=" + advertID + "&cityID=" + cityID + "&firstTitleID" + firstTitleID + "&secondTitleID" + secondTitleID);
                break;
            case "update":
                AdvertManager advertManager = new AdvertManager();
                string message = "";
                if (status == "1")
                {
                    status = "0";
                    message = "下线成功！";
                }
                else
                {
                    var subTitle = SubTitleManager.SelectSubTitleByID(Common.ToInt32(secondTitleID));
                    if (subTitle != null && subTitle.RuleType == 1)
                    {
                        if (advertManager.CheckHasAdvertIndex(orderIndex, secondTitleID, cityID))
                        {
                            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('广告位已被占用！');</script>");
                            return;
                        }
                    }
                    status = "1";
                    message = "上线成功！";
                }
                bool isUpdateok = advertManager.UpdateAdvertShopStatus(Common.ToInt32(advertID), Common.ToInt32(status));
                if (isUpdateok)
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('" + message + "');</script>");
                    BindGridViewAdvert(AspNetPager1.StartRecordIndex - 1, AspNetPager1.EndRecordIndex);
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('操作失败！');</script>");
                }
                break;
            case "delete":
                advertManager = new AdvertManager();
                bool isDeleteOK = advertManager.DeleteAdvert(Common.ToInt32(advertID));
                if (isDeleteOK)
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('删除成功！');</script>");
                    BindGridViewAdvert(AspNetPager1.StartRecordIndex - 1, AspNetPager1.EndRecordIndex);
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('删除失败！');</script>");
                }
                break;
            default:
                break;
        }
    }
    protected void GridView_Advert_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void GridView_Advert_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

    }
    protected void GridView_Advert_Sorting(object sender, GridViewSortEventArgs e)
    {
        Dictionary<string, string> sorted = new Dictionary<string, string>();
        if (ViewState["SortedField"] == null)
        {
            sorted.Add(e.SortExpression, "ASC");
            ViewState["SortedField"] = sorted;
        }
        else
        {
            sorted = (Dictionary<string, string>)ViewState["SortedField"];
            if (sorted.ContainsKey(e.SortExpression))
            {
                if (sorted[e.SortExpression] == "ASC")
                {
                    sorted[e.SortExpression] = "DESC";
                }
                else
                {
                    sorted[e.SortExpression] = "ASC";
                }
            }
            else
            {
                sorted.Clear();
                sorted.Add(e.SortExpression, "ASC");
                ViewState["SortedField"] = sorted;
            }
        }
        BindGridViewAdvert(AspNetPager1.StartRecordIndex - 1, AspNetPager1.EndRecordIndex);
    }

    protected void GridView_Advert_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            if (ViewState["SortedField"] != null)
            {
                Dictionary<string, string> order = (Dictionary<string, string>)ViewState["SortedField"];
                for (int i = 0; i < e.Row.Cells.Count; i++)
                {
                    if (e.Row.Cells[i].Controls.Count > 0)
                    {
                        LinkButton lb = e.Row.Cells[i].Controls[0] as LinkButton;
                        if (lb != null)
                        {
                            Literal li = new Literal();
                            if (order.ContainsKey(lb.CommandArgument))
                            {
                                if (order[lb.CommandArgument] == "ASC")
                                {
                                    li.Text = "▲";
                                }
                                else
                                {
                                    li.Text = "▼";
                                }
                            }
                            else
                            {
                                li.Text = "▲▼";

                            }
                            e.Row.Cells[i].Controls.Add(li);
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < e.Row.Cells.Count; i++)
                {
                    if (e.Row.Cells[i].Controls.Count > 0)
                    {
                        LinkButton lb = e.Row.Cells[i].Controls[0] as LinkButton;
                        if (lb != null)
                        {
                            Literal li = new Literal();
                            li.Text = "▲▼";
                            e.Row.Cells[i].Controls.Add(li);
                        }
                    }
                }
            }
        }
    }
}