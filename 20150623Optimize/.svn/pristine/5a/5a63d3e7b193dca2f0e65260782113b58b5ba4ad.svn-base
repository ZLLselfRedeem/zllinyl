using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.WebPageDll;
using System.Data;
using VAGastronomistMobileApp.Model;

public partial class AuthorizationManagement_employeeConnShopAdd : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetEmployeeName();
            GetEmployeeShop(0, 10);
        }
    }
    protected void GetEmployeeShop(int str, int end)
    {
        EmployeeConnShopOperate employeeConnShopOperate = new EmployeeConnShopOperate();
        DataTable dt = employeeConnShopOperate.QueryEmployeeConnShop(Common.ToInt32(Request.QueryString["employeeID"]));
        if (dt.Rows.Count > 0)
        {
            int tableCount = dt.Rows.Count;
            AspNetPager1.RecordCount = tableCount;
            DataTable dt_page = Common.GetPageDataTable(dt, str, end);//分页的DataTable
            GridView_List.DataSource = dt_page;
        }
        else
        {
            GridView_List.DataSource = dt;
        }
        GridView_List.DataBind();
    }
    void GetEmployeeName()
    {
        EmployeeOperate operate = new EmployeeOperate();
        EmployeeInfo info = operate.QueryEmployee(Common.ToInt32(Request.QueryString["employeeID"]));
        if (info != null)
        {
            lb_head.Text = "当前操作员工：姓名：" + info.EmployeeFirstName + "，手机号码：" + info.UserName;
        }
    }
    /// <summary>
    /// 分页显示信息
    /// </summary>
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        GetEmployeeShop(AspNetPager1.StartRecordIndex - 1, AspNetPager1.EndRecordIndex);
    }
    /// <summary>
    /// 删除员工门店关联权限
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GridView_List_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        ShopOperate shopOperate = new ShopOperate();
        int employeeShopID = Common.ToInt32(GridView_List.DataKeys[e.RowIndex].Values["employeeShopID"].ToString());
        EmployeeConnShopOperate operate = new EmployeeConnShopOperate();
        if (operate.RemoveEmployeeShopStatusByemployeeShopID(employeeShopID))
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "js", "<script>alert('删除成功！');</script>");
            GetEmployeeShop(0, 10);
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "js", "<script>alert('删除失败！');</script>");
        }
    }
    /// <summary>
    /// 增加权限
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btn_add_Click(object sender, EventArgs e)
    {
        int employeeID = Common.ToInt32(Request.QueryString["employeeID"]);
        if (Common.ToInt32(hidden.Value) > 0)
        {
            EmployeeConnShopOperate employeeShopOperate = new EmployeeConnShopOperate();
            EmployeeConnShop employeeShop = new EmployeeConnShop();
            CompanyOperate operate = new CompanyOperate();
            employeeShop.shopID = Common.ToInt32(hidden.Value);
            employeeShop.companyID = operate.GetCompanyId(Common.ToInt32(hidden.Value));
            employeeShop.employeeID = employeeID;
            employeeShop.status = 1;
            if (employeeShop.shopID > 0 && employeeShop.companyID > 0)
            {
                if (employeeShopOperate.AddEmployeeShop(employeeShop))
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "js", "<script>alert('添加成功！');</script>");
                    GetEmployeeShop(0, 10);
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "js", "<script>alert('添加失败！');</script>");
                }
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "js", "<script>alert('请输入搜索门店，选择门店！');</script>");
            }
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "js", "<script>alert('请输入搜索门店，选择门店！');</script>");
        }
    }
}