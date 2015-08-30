using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.WebPageDll;
using System.Data;
using VAGastronomistMobileApp;
using VAGastronomistMobileApp.Model;

public partial class Advertisement_AdMatchBanner : System.Web.UI.Page
{
    AdvertisementOperate oprate = new AdvertisementOperate();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Panel_update.Visible = true;
            Panel_Main.Visible = false;
            this.TextBoxTimeFrom.Text = Common.ToString(DateTime.Now.ToShortDateString());
            this.TextBoxTimeTo.Text = Common.ToString(DateTime.Now.AddDays(7).ToShortDateString());
            this.BindBannerInfo();
            SetAdBanner();
        }
    }

    /// <summary>
    /// 切换广告分类
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void DropDownList_ADType_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetAdBanner();
    } 
    /// <summary>
    /// 保存表单
    /// </summary>
    protected void MatchAdBanner()
    {
        //if (DropDownList_AdvertisementName.SelectedValue != "0" && DropDownList_Banners.SelectedValue != "0")
        //{

        //}
        //else
        //{
        //    Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('请选择广告名称和广告栏位！');</script>");
        //}
    }
    #region 广告列表维护（wancgheng）
    /****************************************
     * Modfied by 林东宇 2014-11-12
     * 增加广告分类、广告类型、广告名称为搜索条件
     * **************************************/
    /// <summary>
    /// 绑定广告数据
    /// </summary>
    /// <param name="str"></param>
    /// <param name="end"></param>
    /// <param name="selectCityId"></param>
    protected void BindGridViewInfo(int str, int end)
    {

        AdvertisementInfoQueryObject queryObject = new AdvertisementInfoQueryObject();
        if (!string.IsNullOrEmpty(this.DropDownListCity.SelectedValue))
        {
            queryObject.cityID = Common.ToInt32(this.DropDownListCity.SelectedValue);
        }
        if (!string.IsNullOrEmpty(this.DropDownList_adClassify.SelectedValue))
        {
            queryObject.advertisementClassify = Common.ToInt32(this.DropDownList_adClassify.SelectedValue);
        }
        if (!string.IsNullOrEmpty(this.DropDownList_ADType.SelectedValue))
        {
            queryObject.advertisementType = Common.ToInt32(this.DropDownList_ADType.SelectedValue);
        }
        DataTable dt = oprate.QueryAdvertisementBannerDetailInfo(queryObject);//获得所有广告信息
        if (dt.Rows.Count > 0)
        {
            int tableCount = dt.Rows.Count;
            AspNetPager1.RecordCount = tableCount;
            DataTable dt_page = Common.GetPageDataTable(dt, str, end);
            GridView_AdList.DataSource = dt_page;
        }
        else
        {
            GridView_AdList.DataSource = dt;
        }
        GridView_AdList.DataBind();
    }
    protected void GridView_AdList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label LabelName = e.Row.FindControl("LabelName") as Label;
            Label LabelIntervalTime = e.Row.FindControl("LabelIntervalTime") as Label;
            if (LabelName != null && LabelIntervalTime != null)
            {
                DataRowView dr = e.Row.DataItem as DataRowView;
                if (dr != null)
                {
                    AdvertisementInfoQueryObject queryObject = new AdvertisementInfoQueryObject();
                    queryObject.status = 1;
                    queryObject.cityID = Common.ToInt32(this.DropDownListCity.SelectedValue);
                    queryObject.advertisementColumnId = Common.ToInt32(this.DropDownList_Banners.SelectedValue);
                    queryObject.advertisementClassify = Common.ToInt32(this.DropDownList_adClassify.SelectedValue);
                    DateTime intervalStart = Common.ToDateTime(dr["IntervalTime"]);
                    LabelIntervalTime.Text = intervalStart.ToString("yyyy-MM-dd tt");
                    queryObject.IntervalStart = intervalStart;
                    queryObject.IntervalEnd = intervalStart.AddHours(12);
                    DataTable dt = oprate.QueryAdvertisementBannerDetailInfo(queryObject);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        LabelName.Text = dt.Rows[0]["name"].ToString();
                        LinkButton LinkButtonStop = e.Row.FindControl("LinkButtonStop") as LinkButton;
                        if (LinkButtonStop != null)
                        {
                            LinkButtonStop.Visible = true;
                            LinkButtonStop.CommandArgument = dt.Rows[0]["id"].ToString();
                        }
                        HiddenField HiddenFieldAdID = e.Row.FindControl("HiddenFieldAdID") as HiddenField;
                        if (HiddenFieldAdID != null)
                        {
                            HiddenFieldAdID.Value = dt.Rows[0]["ID"].ToString();
                        }
                    }
                    else
                    {
                        CheckBox CheckBoxSelect = e.Row.FindControl("CheckBoxSelect") as CheckBox;
                        if (CheckBoxSelect != null)
                        {
                            CheckBoxSelect.Visible = true;
                        }
                    }
                }
            }
        }
    }
    protected void GridView_AdList_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        int id = Common.ToInt32(e.CommandArgument);
        bool result = false; 
        if (e.CommandName.ToString() == "stop")
        {
            result = oprate.ModifyAdvertisementConnAdColumnStatus(id, 0); 
        }
      
        if (result)
        {
            this.BindBannerInfo();
        }

    }
    protected void Button_cancel_Click(object sender, EventArgs e)
    {
        //返回操作，直接隐藏当前修改窗体
        Panel_Window.Visible = false;
    }


    protected void Button_Click(object sender, EventArgs e)
    {
        //显示列表信息
        Panel_Main.Visible = false;
        Panel_update.Visible = true;
    }
    #endregion

    /// <summary>
    /// 分页显示
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        BindAdInfo(AspNetPager1.StartRecordIndex - 1, AspNetPager1.EndRecordIndex);
        Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>ConfirmWindow('Panel_Window');</script>"); 
    } 

    private void SetAdBanner()
    {
        int adType = 0;
        if (this.DropDownList_adClassify.SelectedValue == "1")
        {
            adType = Common.ToInt32(this.DropDownList_ADType.SelectedValue);
        }
        if (this.DropDownList_adClassify.SelectedValue == "2")
        {
             adType = Common.ToInt32(this.DropDownList_adClassify.SelectedValue + this.DropDownList_ADType.SelectedValue);
        }
        DataTable dt  = oprate.QueryAdvertisementBanner(adType);
        if (dt.Rows.Count > 0)
        {
            DropDownList_Banners.DataSource = dt; 
            DropDownList_Banners.DataBind();
            this.DropDownList_Banners.SelectedIndex = 0;
        }
    }
    protected void Button_search_Click(object sender, EventArgs e)
    {
        BindBannerInfo();
    }
    //protected void DropDownList_adClassify_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    switch (this.DropDownList_adClassify.SelectedValue)
    //    {
    //        case "1":
    //            DropDownList_ADType.Attributes.Add("style", "display:''");
    //            DropDownList_ADTypeFoodPlaza.Attributes.Add("style", "display:none");
    //            break;
    //        case "2":
    //            DropDownList_ADType.Attributes.Add("style", "display:none");
    //            DropDownList_ADTypeFoodPlaza.Attributes.Add("style", "display:''");
    //            break;
    //        default:
    //            break;
    //    }
    //    DropDownList_ADType.SelectedIndex = 0;
    //    DropDownList_ADTypeFoodPlaza.SelectedIndex = 0;
    //}

    protected void ButtonSelect_Click(object sender, EventArgs e)
    {
        Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>ConfirmWindow('Panel_Window');</script>");
        this.TextBoxName.Text = string.Empty;
        this.BindAdInfo(0, 10);
    }
    /***************************
     * Added by 林东宇 at 2014-11-20  
     * 
     * ******************************/
    /// <summary>
    /// 绑定广告排期数据
    /// </summary>
    private void BindBannerInfo()
    {
        if (string.IsNullOrEmpty(this.TextBoxTimeFrom.Text))
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('广告排期起始时间不能为空!');</script>");
            this.TextBoxTimeFrom.Focus();
            return;
        }
        if (string.IsNullOrEmpty(this.TextBoxTimeTo.Text))
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('广告排期截至时间不能为空!');</script>");
            this.TextBoxTimeTo.Focus();
            return;
        } 
        DateTime beginDate = DateTime.Parse(this.TextBoxTimeFrom.Text);
        DateTime endDate = DateTime.Parse(this.TextBoxTimeTo.Text).AddDays(0.99999);
        if((endDate - beginDate).Days>30)
        {
            CommonPageOperate.AlterMsg(this,"排期时间最长跨度为30天！");
            return;
        }
        if (beginDate >= endDate)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('广告排期起始时间不能大于截至时间!');</script>");
            this.TextBoxTimeFrom.Focus();
            return;
        }
        int IntervalHour;
        //全部时段
        if (string.IsNullOrEmpty(this.DropDownListInterval.SelectedValue))
        {
            IntervalHour = 12;
        }
        // 其他
        else
        {
            IntervalHour = 24;
        }
        //下午时段
        if (this.DropDownListInterval.SelectedValue == "2")
        {
            beginDate = beginDate.AddHours(12);
        }

        DateTime currentDate = beginDate;
        DataTable dt = new DataTable();
        dt.Columns.Add("IntervalTime");
        while (currentDate <= endDate)
        {
            DataRow dr = dt.NewRow();
            dr["IntervalTime"] = currentDate;
            currentDate = currentDate.AddHours(IntervalHour);  
            dt.Rows.Add(dr);
        }
        this.GridView_AdList.DataSource = dt;
        this.GridView_AdList.DataBind();
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
        if (string.IsNullOrEmpty(this.TextBoxTimeFrom.Text))
        {

        }
        AdvertisementInfoQueryObject queryObject = new AdvertisementInfoQueryObject()
        {
            name = this.TextBoxName.Text.Trim(),
            status = 1,
            advertisementType = Common.ToInt32(this.DropDownList_ADType.SelectedValue),
            advertisementClassify = Common.ToInt32(this.DropDownList_adClassify.SelectedValue),
        };
        queryObject.advertisementType = Common.ToInt32( this.DropDownList_ADType.SelectedValue);

        DataTable dt = oprate.QueryAdvertisement(queryObject);
        this.AspNetPager1.RecordCount = dt.Rows.Count;
        DataTable dt_page = Common.GetPageDataTable(dt, AspNetPager1.StartRecordIndex - 1, AspNetPager1.EndRecordIndex);
        this.GridViewAd.DataSource = dt_page;
        this.GridViewAd.DataBind();
    }
    protected void ButtonAdSearch_Click(object sender, EventArgs e)
    {
        Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>ConfirmWindow('Panel_Window');</script>"); 
        this.BindAdInfo(0, 10);
    }
    protected void GridViewAd_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "select")
        {
            int successCount = 0;
            int id = Common.ToInt32(e.CommandArgument);
            AdvertisementInfo advertisementInfo = oprate.GetAdvertisementByID(id);
            foreach (GridViewRow row in this.GridView_AdList.Rows)
            {
                CheckBox checkBoxSelect = row.FindControl("checkBoxSelect") as CheckBox;
                if (checkBoxSelect == null)
                {
                    continue;
                }
                if (checkBoxSelect.Checked)
                {
                    DateTime IntervalTimeBegin = Common.ToDateTime(this.GridView_AdList.DataKeys[row.RowIndex]["IntervalTime"]);
                    AdvertisementInfoQueryObject queryObject = new AdvertisementInfoQueryObject();
                    queryObject.status = 1;
                    queryObject.cityID = Common.ToInt32(this.DropDownListCity.SelectedValue);
                    queryObject.advertisementColumnId = Common.ToInt32(this.DropDownList_Banners.SelectedValue);
                    queryObject.advertisementClassify = Common.ToInt32(this.DropDownList_adClassify.SelectedValue);
                    DateTime intervalStart = IntervalTimeBegin;
                    queryObject.IntervalStart = intervalStart;
                    queryObject.IntervalEnd = intervalStart.AddHours(12);
                    DataTable dt = oprate.QueryAdvertisementBannerDetailInfo(queryObject);
                    if(dt!=null  && dt.Rows.Count >0)        
                    {
                        continue;
                    }
                    AdvertisementConnAdColumnInfo adColumn = new AdvertisementConnAdColumnInfo();
                    adColumn.name = advertisementInfo.name;
                    adColumn.advertisementColumnId = Common.ToInt32(this.DropDownList_Banners.SelectedValue);
                    adColumn.advertisementId = advertisementInfo.id;
                    adColumn.cityId = Common.ToInt32(this.DropDownListCity.SelectedValue);

                    adColumn.displayStartTime = IntervalTimeBegin;
                    adColumn.displayEndTime = IntervalTimeBegin.AddHours(12);
                    adColumn.status = 1;

                    bool boo = oprate.AddAdvertisementConnToBanner(adColumn);
                    if (boo)
                    {
                        successCount++;
                    }
                    else
                    {
                        string msg =  IntervalTimeBegin.ToString("yyyy-MM-dd tt") + "时段广告设置失败!";
                        Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('" + msg + "');</script>");
                        break;
                    }
                }
            }
            if (successCount > 0)
            {
                string msg = "成功设置" + successCount.ToString() + "条广告!";
                Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('" + msg + "');</script>");
                this.BindBannerInfo();
                Panel_Main.Visible = false;
                Panel_update.Visible = true;
            }
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
    }
    protected void DropDownList_adClassify_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.SetAdBanner();
    }
}