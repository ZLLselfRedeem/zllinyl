using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.WebPageDll;
using System.Data;
using VAGastronomistMobileApp.WebPageDll.Services;
using VAGastronomistMobileApp.WebPageDll.Services.Infrastructure;

public partial class AuthorizationManagement_EmployeeManage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetEmployee(0, 10);
        }
    }

    /// <summary>
    /// 获取员工信息
    /// </summary>
    protected void GetEmployee(int str, int end)
    {
        string name = TextBox_Name.Text.Trim().Replace(" ", "").ToString();//姓名
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

    /// <summary>
    /// 删除某行
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        //EmployeeOperate employeeOperate = new EmployeeOperate();
        //int employeeID = Common.ToInt32(GridView1.DataKeys[e.RowIndex].Values["EmployeeID"].ToString());
        //bool i = employeeOperate.RemoveEmployee(employeeID);
        //if (i == true)
        //{
        //    Page.ClientScript.RegisterStartupScript(GetType(), "js", "<script>alert('删除成功！');</script>");
        //    TextBox_Name.Text = "";
        //}
        //else
        //{
        //    Page.ClientScript.RegisterStartupScript(GetType(), "js", "<script>alert('删除失败！');</script>");
        //}
        //GetEmployee(AspNetPager1.StartRecordIndex - 1, AspNetPager1.EndRecordIndex);
    }

    /// <summary>
    /// 点击修改
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

        string employeeID = GridView1.DataKeys[GridView1.SelectedIndex].Values["EmployeeID"].ToString();
        Response.Redirect("EmployeeUpdate.aspx?EmployeeID=" + employeeID);

    }
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        GetEmployee(AspNetPager1.StartRecordIndex - 1, AspNetPager1.EndRecordIndex);
    }
    /// <summary>
    /// 根据员工姓名和用户名查询员工信息（wangcheng 2013/7/5）
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button_QueryEmplooer_Click(object sender, EventArgs e)
    {
        GetEmployee(0, 10);
    }
    /// <summary>
    /// 绑定格式
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        //  e.Row.Attributes.Add("onclick", "selectChange(this)");
        //        if (e.Row.RowType == DataControlRowType.DataRow)
        //        {
        //            e.Row.Attributes.Add("onclick", "RowClick(this)");

        //            CheckBox cb1 = e.Row.FindControl("CheckBox1") as CheckBox;
        //            string id = GridView1.DataKeys[e.Row.RowIndex].Value.ToString();


        //            if (cb1 != null)
        //            {
        //                 cb1.Attributes.Add("onclick", "changeColor('" + cb1.ClientID + "',1," + id + ");");
        //            }
        //            //if (string.IsNullOrEmpty(hf.Value))
        //            //{
        //            //    if (Array.IndexOf(hf.Value.Split('|'), id + "," + cb1.ClientID) > -1)
        //            //    {
        //            //        if (cb1.Checked == true)
        //            //        {
        //                         //e.Row.BackColor = System.Drawing.Color.Red;
        //            //        }   
        //            //    }
        //            //}
        //        }
        //    }
    }

    protected void GridView1_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            string employeeID = e.CommandArgument.ToString();
            Response.Redirect("EmployeeUpdate.aspx?EmployeeID=" + employeeID);
        }
        else if (e.CommandName == "Role")
        {
            try
            {
                int employeeId = int.Parse(e.CommandArgument.ToString());
                hiddenEmployee.Value = e.CommandArgument.ToString();
                BindShopAuthority(employeeId);
                Page.ClientScript.RegisterStartupScript(GetType(), "message",
                    "<script language='javascript' defer>ConfirmWindow('Panel_Operate');</script>");
            }
            catch (ArgumentException exc)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "message",
                    "<script language='javascript' defer>alert('" + exc.Message +
                    "');window.location='EmployeeManage.aspx';</script>");
                ;
            }
        }
        else if (e.CommandName == "delete")
        {
            EmployeeOperate employeeOperate = new EmployeeOperate();
            int employeeID = int.Parse(e.CommandArgument.ToString());
            bool i = employeeOperate.RemoveEmployee(employeeID);
            if (i == true)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "js", "<script>alert('删除成功！');</script>");
                TextBox_Name.Text = "";
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "js", "<script>alert('删除失败！');</script>");
            }
            GetEmployee(AspNetPager1.StartRecordIndex - 1, AspNetPager1.EndRecordIndex);
        }
    }

    private void BindShopAuthority(int employeeID)
    {
        IShopAuthorityService shopAuthorityService = ServiceFactory.Resolve<IShopAuthorityService>();

        var list = shopAuthorityService.GetWaiterRoleInfos(0, employeeID);
        GridView2.DataSource = list;
        GridView2.DataBind();
    }

    protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
    {
        var roleId = (int)GridView2.DataKeys[GridView2.SelectedIndex].Values["roleId"];
        int employeeID = int.Parse(hiddenEmployee.Value);

        IViewAllocEmployeeAuthorityService viewAllocEmployeeAuthorityService = ServiceFactory.Resolve<IViewAllocEmployeeAuthorityService>();
        IList<ViewAllocEmployeeAuthority> vaea =
            viewAllocEmployeeAuthorityService.GetViewAllocEmployeeAuthorityByEmployeeAndShopAuthority(employeeID, roleId);
        if (vaea.Count > 0)
        {
            foreach (var v in vaea)
            {
                viewAllocEmployeeAuthorityService.Delete(v);
            }
        }
        else
        {
            ViewAllocEmployeeAuthority vv = new ViewAllocEmployeeAuthority()
            {
                EmployeeId = employeeID,
                ShopAuthorityId = roleId,
                Status = true
            };
            viewAllocEmployeeAuthorityService.Add(vv);
        }
        BindShopAuthority(employeeID);
        Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>ConfirmWindow('Panel_Operate');</script>");
    }


}


