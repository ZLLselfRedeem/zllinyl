using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.WebPageDll;

public partial class RedEnvelope_redRedEnvelopeListDetail : System.Web.UI.Page
{
    private readonly ActivityOperate activityOperate;
    private readonly TreasureChestOperate treasureChestOperate;
    private readonly RedEnvelopeOperate redEnvelopeOperate;
    /// <summary>
    /// 构造函数生成实例
    /// </summary>
    public RedEnvelope_redRedEnvelopeListDetail()
    {
        activityOperate = new ActivityOperate();
        treasureChestOperate = new TreasureChestOperate();
        redEnvelopeOperate = new RedEnvelopeOperate();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindActivityList(0, 10);
        }
    }
    /// <summary>
    /// 绑定下拉列表数据
    /// </summary>
    private void BindDropDownList()
    {
        List<Activity> activities = activityOperate.QueryActivity();
        ddlActivityName.Items.Add(new ListItem("==请选择==", "NA"));
        ddlActivityName.DataTextField = "name";
        ddlActivityName.DataValueField = "activityId";
        ddlActivityName.DataSource = activities;
        ddlActivityName.DataBind();
    }
    /// <summary>
    /// 搜索按钮事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btn_Search_Click(object sender, EventArgs e)
    {
        BindActivityList(0, 10);
    }
    /// <summary>
    /// 绑定活动列表数据
    /// </summary>
    /// <param name="pageIndex"></param>
    /// <param name="pageSize"></param>
    private void BindActivityList(int pageIndex, int pageSize)
    {
        panel_ActivityList.Visible = true;
        panel_TreasureChestList.Visible = false;
        panel_RedEnvelopeList.Visible = false;
        panel_OrderDetail.Visible = false;
        BindDropDownList();
        int cnt = 0;
        IList<Activity> activities = activityOperate.QueryActivity(new VAGastronomistMobileApp.SQLServerDAL.Persistence.Infrastructure.Page(pageIndex, pageSize), "", out cnt);
        if (activities.Count > 0)
        {
            AspNetPager_Activity.RecordCount = cnt;
            this.gdvActivity.DataSource = activities;
        }
        else
        {
            this.gdvActivity.DataSource = null;
        }
        this.gdvActivity.DataBind();
        btn_Back.Visible = false;
    }
    /// <summary>
    /// 查看宝箱按钮事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gdvActivity_SelectedIndexChanged(object sender, EventArgs e)
    {
        panel_ActivityList.Visible = false;
        panel_TreasureChestList.Visible = true;
        panel_RedEnvelopeList.Visible = false;
        panel_OrderDetail.Visible = false;
        ViewState["activityId"] = gdvActivity.DataKeys[gdvActivity.SelectedIndex].Values["activityId"].ToString();//记录当前选中的红包活动编号
        BindTreasureChestList(1, 10);
    }
    /// <summary>
    /// 绑定宝箱列表数据
    /// </summary>
    /// <param name="pageIndex"></param>
    /// <param name="pageSize"></param>
    private void BindTreasureChestList(int pageIndex, int pageSize)
    {
        panel_ActivityList.Visible = false;
        panel_TreasureChestList.Visible = true;
        panel_RedEnvelopeList.Visible = false;
        panel_OrderDetail.Visible = false;
        IList<TreasureChest> treasureChest = treasureChestOperate.QueryTreasureChest(new VAGastronomistMobileApp.SQLServerDAL.Persistence.Infrastructure.Page(pageIndex, pageSize), (int)ViewState["activityId"]);
        if (treasureChest.Count > 0)
        {
            AspNetPager_TreasureChest.RecordCount = treasureChest.Count;
            this.gdvTreasureChest.DataSource = treasureChest;
        }
        else
        {
            this.gdvTreasureChest.DataSource = null;
        }
        this.gdvTreasureChest.DataBind();
        btn_Back.Visible = true;
    }
    /// <summary>
    /// 选择查看红包事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gdvTreasureChest_SelectedIndexChanged(object sender, EventArgs e)
    {
        panel_ActivityList.Visible = false;
        panel_TreasureChestList.Visible = false;
        panel_RedEnvelopeList.Visible = true;
        panel_OrderDetail.Visible = false;
        ViewState["treasureChestId"] = gdvTreasureChest.DataKeys[gdvTreasureChest.SelectedIndex].Values["treasureChestId"].ToString();
        //BindRedEnvelopeList(1, 10);
    }
    /// <summary>
    /// 绑定红包列表数据
    /// </summary>
    /// <param name="pageIndex"></param>
    /// <param name="pageSize"></param>
    //private void BindRedEnvelopeList(int pageIndex, int pageSize)
    //{
    //    panel_ActivityList.Visible = false;
    //    panel_TreasureChestList.Visible = false;
    //    panel_RedEnvelopeList.Visible = true;
    //    panel_OrderDetail.Visible = false;
    //    IList<RedEnvelopeViewModel> treasureChest = redEnvelopeOperate.QueryRedEnvelopeViewModel((long)ViewState["treasureChestId"]);
    //    if (treasureChest.Count > 0)
    //    {
    //        this.gdvRedEnvelope.DataSource = treasureChest;
    //    }
    //    else
    //    {
    //        this.gdvRedEnvelope.DataSource = null;
    //    }
    //    this.gdvRedEnvelope.DataBind();
    //    if (gdvRedEnvelope.Rows.Count > 0)
    //    {
    //        int count = gdvRedEnvelope.Rows.Count;
    //        for (int i = 0; i < count; i++)
    //        {
    //            Label lnkbtnSelectOrder = gdvRedEnvelope.Rows[i].FindControl("lnkbtnSelectOrder") as Label;
    //            if (gdvRedEnvelope.DataKeys[i].Values["stateType"].ToString() != "已使用")
    //            {
    //                lnkbtnSelectOrder.Enabled = false;
    //            }
    //        }
    //    }
    //    btn_Back.Visible = true;
    //}
    /// <summary>
    /// 选择查看点单事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gdvRedEnvelope_SelectedIndexChanged(object sender, EventArgs e)
    {
        panel_ActivityList.Visible = false;
        panel_TreasureChestList.Visible = false;
        panel_RedEnvelopeList.Visible = true;
        panel_OrderDetail.Visible = true;
        ViewState["redEnvelopeId"] = gdvRedEnvelope.DataKeys[gdvRedEnvelope.SelectedIndex].Values["redEnvelopeId"].ToString();
        BindOrderDetailList();
    }
    /// <summary>
    /// 绑定点单列表数据
    /// </summary>
    private void BindOrderDetailList()
    {
        panel_ActivityList.Visible = false;
        panel_TreasureChestList.Visible = false;
        panel_RedEnvelopeList.Visible = true;
        panel_OrderDetail.Visible = true;
        DataTable dataTable = redEnvelopeOperate.QueryRedEnvelopeUsedOrder((long)ViewState["treasureChestId"]);
        this.gdvOrderDetail.DataSource = dataTable;
        this.gdvOrderDetail.DataBind();
        btn_Back.Visible = true;
    }
    /// <summary>
    /// 返回按钮事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btn_Back_Click(object sender, EventArgs e)
    {
        if (panel_OrderDetail.Visible == true)//点单详情显示，返回操作
        {
            panel_OrderDetail.Visible = false;
            panel_RedEnvelopeList.Visible = true;
            panel_ActivityList.Visible = false;
            panel_TreasureChestList.Visible = false;
            btn_Back.Visible = true;
        }
        if (panel_RedEnvelopeList.Visible == true)//红包列表显示，返回操作
        {
            panel_RedEnvelopeList.Visible = false;
            panel_OrderDetail.Visible = false;
            panel_ActivityList.Visible = false;
            panel_TreasureChestList.Visible = true;
            btn_Back.Visible = true;
        }
        if (panel_TreasureChestList.Visible == true)//宝箱列表显示，返回操作
        {
            panel_ActivityList.Visible = true;
            panel_RedEnvelopeList.Visible = false;
            panel_OrderDetail.Visible = false;
            panel_TreasureChestList.Visible = false;
            btn_Back.Visible = true;
        }
        if (panel_ActivityList.Visible == true)//活动列表显示，隐藏返回
        {
            panel_RedEnvelopeList.Visible = false;
            panel_ActivityList.Visible = true;
            panel_TreasureChestList.Visible = false;
            panel_OrderDetail.Visible = false;
            btn_Back.Visible = false;
        }
    }
}