using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.WebPageDll;

public partial class Advertisement_AdvertisementManagement : System.Web.UI.Page
{
    AdvertisementOperate oprate = new AdvertisementOperate();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.BindAdInfo(0, 10);
        }
    }
    protected void GridViewAd_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        DataRowView dr = e.Row.DataItem as DataRowView;
        if (dr == null)
        {
            return;
        }
        Label LabelClassify = e.Row.FindControl("LabelClassify") as Label;
        Label LabelType = e.Row.FindControl("LabelType") as Label;
        if (LabelClassify == null || LabelType == null)
        {
            return;
        }
        if (dr["advertisementClassify"].ToString() == "1")
        {
            LabelClassify.Text = "首页广告";
        }
        else
        {
            LabelClassify.Text = "美食广场广告";
        }

        if (dr["advertisementType"].ToString() == "1")
        {
            LabelType.Text = "门店广告";
        }
        if (dr["advertisementType"].ToString() == "3")
        {
            LabelType.Text = "网页广告";
        }
        if (dr["advertisementType"].ToString() == "4")
        {
            LabelType.Text = "红包广告";
        }
        if (dr["advertisementType"].ToString() == "5")
        {
            LabelType.Text = "专题广告";
        }
        LinkButton LinkButtonEdit = e.Row.FindControl("LinkButtonEdit") as LinkButton;
        if (LinkButtonEdit != null)
        {
            LinkButtonEdit.PostBackUrl = @"~/Advertisement/AdvertisementAdd.aspx?ID=" + dr["id"].ToString();
        }
    }

    /// <summary>
    /// 分页显示
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        BindAdInfo(AspNetPager1.StartRecordIndex - 1, AspNetPager1.EndRecordIndex); 
    }  

    /***************************
     * Added by 林东宇 at 2014-11-20  
     * 
     * ******************************/
    /// <summary>
    /// 绑定广告数据
    /// </summary>
    private void BindAdInfo(int start, int end)
    {
        
        AdvertisementInfoQueryObject queryObject = new AdvertisementInfoQueryObject()
        {
            name = this.TextBoxName.Text.Trim(),
            status = 1 
        };
        if (!string.IsNullOrEmpty(this.DropDownList_ADType.SelectedValue))
        {
            queryObject.advertisementType = Common.ToInt32(this.DropDownList_ADType.SelectedValue);
            if (this.DropDownList_adClassify.SelectedValue == "2")
            {
                queryObject.advertisementType = Common.ToInt32("2" + this.DropDownList_ADType.SelectedValue);
            }
        }
        if (!string.IsNullOrEmpty(this.DropDownList_adClassify.SelectedValue))
        {
            queryObject.advertisementClassify = Common.ToInt32(this.DropDownList_adClassify.SelectedValue); 
        }
        DataTable dt = oprate.QueryAdvertisement(queryObject);
        this.AspNetPager1.RecordCount = dt.Rows.Count;
        DataTable dt_page = Common.GetPageDataTable(dt, AspNetPager1.StartRecordIndex - 1, AspNetPager1.EndRecordIndex);
        this.GridViewAd.DataSource = dt_page;
        this.GridViewAd.DataBind();
    }
    protected void Button_search_Click(object sender, EventArgs e)
    {
        BindAdInfo(0, 10);
    }
}