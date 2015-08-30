using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.WebPageDll;
using System.Data;
public partial class AuthorizationManagement_EmployeeShop : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetAllShop();
            GetEmployee(0, 10);
            this.TreeView_Shop.Attributes.Add("onclick", "CheckEvent()");
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
    /// 获取所有权限
    /// </summary>
    protected void GetAllShop()
    {
        TreeView_Shop.Nodes.Clear();
        CompanyOperate companyOperate = new CompanyOperate();
        DataTable dtCompany = companyOperate.QueryCompany();
        for (int i = 0; i < dtCompany.Rows.Count; i++)
        {
            TreeNode NodeDept = new TreeNode();
            NodeDept.Text = dtCompany.Rows[i]["companyName"].ToString();
            NodeDept.Value = dtCompany.Rows[i]["companyID"].ToString();
            int companyID = Common.ToInt32(dtCompany.Rows[i]["companyID"].ToString());
            //获取子的
            ShopOperate shopOperate = new ShopOperate();
            DataTable dtShop = shopOperate.QueryCompanyShop(companyID);
            for (int j = 0; j < dtShop.Rows.Count; j++)
            {
                TreeNode NodeEmployee = new TreeNode();
                NodeEmployee.Text = dtShop.Rows[j]["ShopName"].ToString();
                NodeEmployee.Value = dtShop.Rows[j]["ShopID"].ToString();
                NodeDept.ChildNodes.Add(NodeEmployee);
            }
            TreeView_Shop.Nodes.Add(NodeDept);
        }
        TreeView_Shop.CollapseAll();
    }




    /// <summary>
    /// 角色选择权限
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GridView_Employee_SelectedIndexChanged(object sender, EventArgs e)
    {
        int EmployeeID = Common.ToInt32(GridView_Employee.DataKeys[GridView_Employee.SelectedIndex].Values["employeeID"]);
        GetEmployeeShop(EmployeeID);
    }


    /// <summary>
    /// 获取角色权限
    /// </summary>
    protected void GetEmployeeShop(int employeeID)
    {
        EmployeeConnShopOperate employeeConnShopOperate = new EmployeeConnShopOperate();
        List<VAEmployeeShop> list_VAEmployeeShop = employeeConnShopOperate.QueryEmployeeShop(employeeID);
        if (list_VAEmployeeShop != null)
        {
            Check(TreeView_Shop.Nodes, list_VAEmployeeShop);
        }
        else
        {
            GetAllShop();
        }
    }

    /// <summary>
    /// 遍历treeview,查找用户已经选择的权限
    /// </summary>
    /// <param name="tvtemp"></param>
    /// <param name="list_Employee_menu"></param>
    protected void Check(TreeNodeCollection tvtemp, List<VAEmployeeShop> list_VAEmployeeShop)
    {
        //先取消历史记录
        foreach (TreeNode node in TreeView_Shop.Nodes)
        {
            node.Checked = false;
        }
        //重新查询选择
        foreach (TreeNode temp in tvtemp)
        {
            if (temp.ChildNodes.Count != 0)
            {
                for (int i = 0; i < temp.ChildNodes.Count; i++)
                {
                    foreach (VAEmployeeShop vAEmployeeShop in list_VAEmployeeShop)
                    {
                        if (vAEmployeeShop.shopID.ToString() == temp.ChildNodes[i].Value)
                        {
                            temp.Checked = true;
                            temp.ChildNodes[i].Checked = true;
                        }
                    }
                }
            }
        }
    }



    /// <summary>
    /// 遍历treeview,保存checkbox
    /// </summary>
    /// <param name="tvtemp"></param>
    protected void SaveEmployeeShop(TreeNodeCollection tvtemp, DataTable dt)
    {
        EmployeeConnShopOperate employeeShopOperate = new EmployeeConnShopOperate();
        EmployeeConnShop employeeShop = new EmployeeConnShop();
        foreach (TreeNode temp in tvtemp)
        {
            if (temp.Checked == true)
            {
                if (temp.ChildNodes.Count != 0)
                {
                    for (int i = 0; i < temp.ChildNodes.Count; i++)
                    {
                        if (temp.ChildNodes[i].Checked == true)
                        {
                            employeeShop.shopID = Common.ToInt32(temp.ChildNodes[i].Value);
                            employeeShop.companyID = Common.ToInt32(temp.Value);
                            employeeShop.employeeID = Common.ToInt32(GridView_Employee.DataKeys[GridView_Employee.SelectedIndex].Values["employeeID"]);
                            employeeShop.status = 1;
                            //if (employeeShopOperate.SelectEmployeeShop(employeeShop.employeeID, employeeShop.shopID))//如果存在-1数据，则直接置status为1
                            //{
                            //    employeeShopOperate.UpdateEmployeeShop(employeeShop.employeeID, employeeShop.shopID, 1);
                            //}
                            //else//反之新增一条
                            //{
                            for (int j = 0; j < dt.Rows.Count; j++)
                            {
                                if (employeeShop.shopID == Common.ToInt32(dt.Rows[j]["shopID"]))
                                {
                                    // employeeShop.isSupportEnterSyb = true;
                                    break;
                                }
                                else
                                {
                                    continue;
                                }
                            }
                            employeeShopOperate.AddEmployeeShop(employeeShop);
                            //}
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// 添加
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button_Confirm_Click(object sender, EventArgs e)
    {
        if (GridView_Employee.SelectedIndex == -1)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('请选择用户！');</script>");
        }
        else
        {
            EmployeeConnShopOperate employeeShopOperate = new EmployeeConnShopOperate();
            int employeeID = Common.ToInt32(GridView_Employee.DataKeys[GridView_Employee.SelectedIndex].Values["employeeID"]);
            AuthorityOperate oper = new AuthorityOperate();
            DataTable dt = oper.QueryEmployeeConnShopIsSupportEnterSyb(employeeID);
            employeeShopOperate.UpdateEmployeeShop(employeeID);
            SaveEmployeeShop(TreeView_Shop.Nodes, dt);
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('门店选择成功！');</script>");
        }
    }
    /// <summary>
    /// 根据用户名和员工名模糊获取员工信息
    /// </summary>
    protected void GetEmployeeInfo(int str, int end)
    {
        string name = TextBox_Name.Text.Replace(" ", "").ToString();//用户名或姓名
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