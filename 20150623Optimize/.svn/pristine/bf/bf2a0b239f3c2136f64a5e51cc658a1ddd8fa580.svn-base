using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.WebPageDll;
using System.Data;

public partial class PointsManage_waiterManage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //page初始化
            txt_EmployeePhone.Text = "";
            BindGridView_EmployeeData(0, 10);
        }
    }

    protected void btn_QueryEmployee_Click(object sender, EventArgs e)
    {
        if (Common.ToClearSpecialCharString(txt_EmployeePhone.Text.Trim()) != "")
        {
            BindGridView_EmployeeData(0, 10);
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "js", "<script>alert('服务员手机号码有误');</script>");
        }
    }
    /// <summary>
    /// 绑定服务员列表信息
    /// </summary>
    void BindGridView_EmployeeData(int str, int end)
    {
        EmployeePointOperate emplpyeePointOper = new EmployeePointOperate();
        string phoneNum = Common.ToClearSpecialCharString(txt_EmployeePhone.Text.Trim());//搜索文本框内容
        DataTable dtEmployeeInfo = emplpyeePointOper.QueryWaiter(phoneNum);//模糊匹配手机号码
        if (dtEmployeeInfo.Rows.Count > 0)
        {
            int tableCount = dtEmployeeInfo.Rows.Count;
            AspNetPager_EmployeeInfo.RecordCount = tableCount;
            DataTable dt_page = Common.GetPageDataTable(dtEmployeeInfo, str, end);//分页的DataTable
            GridView_Employee.DataSource = dt_page;
        }
        GridView_Employee.DataBind();
    }
    /// <summary>
    /// 分页显示
    /// </summary>
    protected void AspNetPager_EmployeeInfo_PageChanged(object sender, EventArgs e)
    {
        BindGridView_EmployeeData(AspNetPager_EmployeeInfo.StartRecordIndex - 1, AspNetPager_EmployeeInfo.EndRecordIndex);
    }
    /// <summary>
    /// 点击查看详情事件
    /// </summary>
    protected void GridView_Employee_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        Panel_Detail.Visible = true;
        Panel_GridView.Visible = false;
        int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;//转换为按钮类型，获取其所在的行的索引
        string employeeId = Common.ToString(GridView_Employee.DataKeys[index].Values["EmployeeID"].ToString());
        mainFrame.Attributes.Add("src", "waiterQuery.aspx?employeeID=" + employeeId);
    }
    /// <summary>
    /// 返回
    /// </summary>
    protected void Button_back_Click(object sender, EventArgs e)
    {
        Panel_Detail.Visible = false;
        Panel_GridView.Visible = true;
    }
}