using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.WebPageDll;
using System.Data;
using VAGastronomistMobileApp.Model;

public partial class AuthorizationManagement_employeeConnShop : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetEmployee(0, 10);
        }
    }
    /// <summary>
    /// 获取所有角色信息
    /// </summary>
    protected void GetEmployee(int str, int end)
    {
        EmployeeOperate EmployeeOperate = new EmployeeOperate();
        DataTable dt = EmployeeOperate.QueryEmployee();//总共的DataTable
        if (dt.Rows.Count > 0)
        {
            int tableCount = dt.Rows.Count;//桌子总数
            AspNetPager1.RecordCount = tableCount;
            DataTable dt_page = Common.GetPageDataTable(dt, str, end);//分页的DataTable
            GridView_Employee.DataSource = dt_page;
            GridView_Employee.DataBind();
        }
    }
    /// <summary>
    /// 角色选择权限
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GridView_Employee_SelectedIndexChanged(object sender, EventArgs e)
    {
        int EmployeeID = Common.ToInt32(GridView_Employee.DataKeys[GridView_Employee.SelectedIndex].Values["employeeID"]);
        Response.Redirect("employeeConnShopAdd.aspx?employeeID=" + EmployeeID + "");//跳转页面
    }
    /// <summary>
    /// 根据用户名和员工名模糊获取员工信息
    /// </summary>
    protected void GetEmployeeInfo(int str, int end)
    {
        string name = TextBox_Name.Text.Replace(" ", "").ToString();//姓名
        EmployeeOperate employeeOperate = new EmployeeOperate();
        DataTable dt = employeeOperate.QueryEmployeeByName(name);//总共的DataTable//wangcheng，加入参数
        if (dt.Rows.Count > 0)
        {
            int tableCount = dt.Rows.Count;//桌子总数
            AspNetPager1.RecordCount = tableCount;
            DataTable dt_page = Common.GetPageDataTable(dt, str, end);//分页的DataTable
            GridView_Employee.DataSource = dt_page;
            GridView_Employee.DataBind();
        }
    }
    protected void Button_QueryEmplooer_Click(object sender, EventArgs e)
    {
        GetEmployeeInfo(0, 10);
    }
    /// <summary>
    /// 分页显示
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        GetEmployee(AspNetPager1.StartRecordIndex - 1, AspNetPager1.EndRecordIndex);
    }
}