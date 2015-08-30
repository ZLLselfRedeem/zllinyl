using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.WebPageDll;
using System.Data;
public partial class CompanyManage_CompanyManage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetCompany(0, 10);
        }
    }
    protected void GetUserAuthortiy()
    {
        if ((VAEmployeeLoginResponse)Session["UserInfo"] != null)
        {
            string userName = ((VAEmployeeLoginResponse)Session["UserInfo"]).userName;
            EmployeeOperate employeeOperate = new EmployeeOperate();
            DataTable dt = employeeOperate.QueryEmployeeAuthortiy(userName);
            bool commission = false;
            bool vip = false;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["AuthorityURL"].ToString() == "Commission")
                {
                    commission = true;
                }
                if (dt.Rows[i]["AuthorityURL"].ToString() == "PrePayPrivilege")
                {
                    vip = true;
                }
            }
            if (!commission)
            {
                GridView_Company.Columns[3].Visible = false;
            }
            if (!vip)
            {
                GridView_Company.Columns[4].Visible = false;
            }
        }
    }
    protected DataTable GetCompanyDataTable()
    {
        CompanyOperate companyOperate = new CompanyOperate();
        DataTable dt = companyOperate.QueryCompany();//总共的DataTable
        return dt;
    }
    /// <summary>
    /// 获取客户信息
    /// </summary>
    protected void GetCompany(int str, int end)
    {
        DataTable dt = GetCompanyDataTable();
        if (dt.Rows.Count > 0)
        {
            int tableCount = dt.Rows.Count;//桌子总数
            AspNetPager1.RecordCount = tableCount;
            DataTable dt_page = Common.GetPageDataTable(dt, str, end);//分页的DataTable
            GridView_Company.DataSource = dt_page;
        }
        else
        {
            GridView_Company.DataSource = dt;
        }
        GridView_Company.DataBind();
    }
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        GetCompany(AspNetPager1.StartRecordIndex - 1, AspNetPager1.EndRecordIndex);
    }
    /// <summary>
    /// 绑定列表事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GridView_Company_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;//转换为按钮类型，获取其所在的行的索引
        string companyID = GridView_Company.DataKeys[index].Values["companyID"].ToString();
        switch (e.CommandName.ToString())
        {
            case "Select":
                Response.Redirect("CompanyUpdate.aspx?companyID=" + companyID);
                break;
            case "Commission":
                Response.Redirect("CompanyCommissionAndFreeRefundHour.aspx?companyID=" + companyID);
                break;
            case "BankAccount":
                Response.Redirect("AccountsList.aspx?companyID=" + companyID + "&status=1");
                break;
            case "deleteCompany":
                CompanyOperate companyOperate = new CompanyOperate();
                bool i = companyOperate.RemoveCompany(Common.ToInt32(companyID));
                if (i == true)
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('删除成功！');</script>");
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('删除失败！');</script>");
                }
                GetCompany(AspNetPager1.StartRecordIndex - 1, AspNetPager1.EndRecordIndex);
                break;
            default:
                break;
        }
    }
    /// <summary>
    /// 根据公司名称查询公司信息（wangcheng）
    /// </summary>
    protected void Button_QueryCompany_Click(object sender, EventArgs e)
    {
        string compangName = TextBox_CompanyName.Text.Trim().ToString();
        DataTable dtcompany = new DataTable();
        if (!String.IsNullOrEmpty(compangName))
        {
            dtcompany = GetNewDataTable(compangName);
            GridView_Company.DataSource = Common.GetPageDataTable(dtcompany, 0, 10);
            AspNetPager1.Attributes.Add("style", "display:none");
        }
        else
        {
            dtcompany = GetCompanyDataTable();
            GridView_Company.DataSource = Common.GetPageDataTable(dtcompany, 0, 10); ;
            AspNetPager1.Attributes.Add("style", "display:block");
        }
        GridView_Company.DataBind();
    }

    /// <summary>
    ///模糊查询公司名称（wangcheng）
    /// </summary>
    protected DataTable GetNewDataTable(string condition)
    {
        DataTable dtcompany = GetCompanyDataTable();
        DataTable newdt = new DataTable();
        newdt = dtcompany.Clone();//浅拷贝，复制表的结构，不复制表的数据
        string likecompanyname = "companyName like '%" + condition + "%'";//匹配模糊查询
        DataRow[] dr = dtcompany.Select(likecompanyname);
        for (int i = 0; i < dr.Length; i++)
        {
            newdt.ImportRow((DataRow)dr[i]);
        }
        return newdt;//返回的查询结果
    }
}