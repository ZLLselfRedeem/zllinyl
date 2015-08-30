using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.WebPageDll;
using System.Data;
using System.ComponentModel;
using System.Reflection;
using VAGastronomistMobileApp.Model;
using Web.Control.Enum;

public partial class CustomerServiceProcessing_customerServiceOperateLogList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //InitFunc();
            BindGridViewInfo(0, 10);
            BindDropDown();
        }
    }
    /// <summary>
    /// 绑定显示客服操作下拉列表信息
    /// </summary>
    private void BindDropDown()
    {
        DataTable DescriptionTextAndEnumValueDt = EnumHelper.EnumToDataTable(typeof(VACustomerServiceOperateType), "typeValue", "typeId");
        if (DescriptionTextAndEnumValueDt.Rows.Count > 0)
        {
            DropDownList_OperateType.DataSource = DescriptionTextAndEnumValueDt;
            DropDownList_OperateType.DataTextField = "typeValue";
            DropDownList_OperateType.DataValueField = "typeId";
            DropDownList_OperateType.DataBind();
            DropDownList_OperateType.Items.Add(new ListItem("所有操作", "0"));
            DropDownList_OperateType.SelectedValue = "0";//默认选中所有操作
        }
    }
    /// <summary>
    /// 初始化一些值
    /// </summary>
    private void InitFunc()
    {
        TextBox_preOrderId.Text = "";
        TextBox_operateName.Text = "";
        TextBox_startTime.Text = DateTime.Now.ToString();
        TextBox_endTime.Text = DateTime.Now.ToString();
    }
    protected void DropDownList_OperateType_SelectedIndexChanged(object sender, EventArgs e)
    {
        //InitFunc();
        CustomerServiceOperateLogOperate cso = new CustomerServiceOperateLogOperate();
        DataTable dtOperateLog = cso.QueryCustomerServiceOperateLog(Common.ToInt32(DropDownList_OperateType.SelectedValue));
        if (dtOperateLog.Rows.Count > 0)
        {
            AspNetPager1.Visible = true;
            AspNetPager1.RecordCount = dtOperateLog.Rows.Count;
            DataTable dt_page = Common.GetPageDataTable(dtOperateLog, 0, 10);
            GridView_customerServiceLog.DataSource = dt_page;
        }
        else
        {
            GridView_customerServiceLog.DataSource = dtOperateLog;
            AspNetPager1.Visible = false;
        }
        GridView_customerServiceLog.DataBind();
    }
    /// <summary>
    /// 绑定显示客服信息列表
    /// </summary>
    protected void BindGridViewInfo(int str, int end)
    {
        int preOrderId = Common.ToInt32(TextBox_preOrderId.Text.Trim());//点单流水号
        string operateName = TextBox_operateName.Text.Trim();//客服操作者姓名
        string strTime = TextBox_startTime.Text.Trim();//查询开始时间
        string endTime = TextBox_endTime.Text.Trim();//查询结束时间
        CustomerServiceOperateLogOperate cso = new CustomerServiceOperateLogOperate();
        DataTable dtOperateLog = cso.QueryCustomerServiceOperateLog(preOrderId, operateName, strTime, endTime);
        if (dtOperateLog.Rows.Count > 0)
        {
            AspNetPager1.Visible = true;
            AspNetPager1.RecordCount = dtOperateLog.Rows.Count;
            DataTable dt_page = Common.GetPageDataTable(dtOperateLog, str, end);
            GridView_customerServiceLog.DataSource = dt_page;
        }
        else
        {
            GridView_customerServiceLog.DataSource = dtOperateLog;
            AspNetPager1.Visible = false;
        }
        GridView_customerServiceLog.DataBind();
    }
    /// <summary>
    /// 根据文本框条件查询获得客服信息列表
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btn_Search_Click(object sender, EventArgs e)
    {
        BindGridViewInfo(0, 10);
    }
    /// <summary>
    /// 分页
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        BindGridViewInfo(AspNetPager1.StartRecordIndex - 1, AspNetPager1.EndRecordIndex);
    }
}