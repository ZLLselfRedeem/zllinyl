using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.WebPageDll;
using VAGastronomistMobileApp.Model;
using System.Data;

public partial class AuthorizationManagement_EmployeeRoleManage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetEmployee(0, 10);
            GetRole();
        }
    }

    /// <summary>
    /// 获取所有角色信息
    /// </summary>
    protected void GetRole()
    {
        RoleOperate RoleOperate = new RoleOperate();
        DataTable dt = RoleOperate.QueryRole();//总共的DataTable
        if (dt.Rows.Count > 0)
        {
            GridView_Role.DataSource = dt;
            GridView_Role.DataBind();
        }
    }

    /// <summary>
    /// 获取员工角色信息
    /// </summary>
    protected void GetEmployeeRole()
    {
        EmployeeOperate employeeOperate = new EmployeeOperate();
        int employeeID = Common.ToInt32(GridView1.DataKeys[GridView1.SelectedIndex].Values["EmployeeID"]);
        GridView_EmployeeRole.DataSource = employeeOperate.QueryEmployeeRole(employeeID);
        GridView_EmployeeRole.DataBind();
    }
    /// <summary>
    /// 点击角色管理
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        Panel1.Visible = true;
        GetEmployeeRole();
    }

    /// <summary>
    /// 用户表翻页
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        GetEmployee(AspNetPager1.StartRecordIndex - 1, AspNetPager1.EndRecordIndex);
    }

    /// <summary>
    /// 给用户选择角色
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GridView_Role_SelectedIndexChanged(object sender, EventArgs e)
    {
        EmployeeRole employeeRole = new EmployeeRole();
        employeeRole.EmployeeID = Common.ToInt32(GridView1.DataKeys[GridView1.SelectedIndex].Values["EmployeeID"]);
        employeeRole.EmployeeRoleStatus = 1;
        employeeRole.RoleID = Common.ToInt32(GridView_Role.DataKeys[GridView_Role.SelectedIndex].Values["RoleID"]);

        EmployeeRoleOperate employeeRoleOperate = new EmployeeRoleOperate();
        bool i = employeeRoleOperate.AddEmployeeRole(employeeRole);
        if (i == true)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "js", "<script>alert('添加成功！');</script>");
            GetEmployeeRole();
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "js", "<script>alert('添加失败！');</script>");
        }
    }

    /// <summary>
    /// 删除用户角色
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GridView_EmployeeRole_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

        EmployeeRoleOperate employeeRoleOperate = new EmployeeRoleOperate();
        int employeeRoleID = Common.ToInt32(GridView_EmployeeRole.DataKeys[e.RowIndex].Values["EmployeeRoleID"]);
        //删除指定的employeeId的角色时，同时应该删除
        bool i = employeeRoleOperate.RemoveEmployeeRole(employeeRoleID);
        if (i == true)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "js", "<script>alert('删除成功！');</script>");
            GetEmployeeRole();
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "js", "<script>alert('删除失败！');</script>");
        }
    }
    /// <summary>
    /// 过滤员工信息
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button_QueryEmplooer_Click(object sender, EventArgs e)
    {
        GetEmployee(0, 10);
    }
    /// <summary>
    /// 获取员工信息
    /// </summary>
    protected void GetEmployee(int str, int end)
    {
        string name = TextBox_Name.Text.Replace(" ", "").ToString();//用户名或姓名
        EmployeeOperate employeeOperate = new EmployeeOperate();
        DataTable dt = employeeOperate.QueryEmployeeByName(name);//总共的DataTable//wangcheng，加入参数
        if (dt.Rows.Count > 0)
        {
            int tableCount = dt.Rows.Count;//桌子总数
            AspNetPager1.RecordCount = tableCount;

            DataTable dt_page = Common.GetPageDataTable(dt, str, end);//分页的DataTable
            GridView1.DataSource = dt_page;
        }
        GridView1.DataBind();
    }
}