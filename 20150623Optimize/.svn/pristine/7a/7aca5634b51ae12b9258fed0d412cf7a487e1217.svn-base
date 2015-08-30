using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.WebPageDll;
//定义委托
public delegate void DropDownListCompanySelectedIndexChangedEventHandler(object sender, EventArgs e); 
public partial class WebUserControl_DropDownListCompany : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        QueryCompany();
    }

    public int companyId { get; set; }
    //定义事件
    public event DropDownListCompanySelectedIndexChangedEventHandler DropDownListCompanySelectedIndexChanged;
    protected void DropDownList_Company_SelectedIndexChanged(object sender, EventArgs e)
    {
        companyId =Common.ToInt32(DropDownList_Company.SelectedValue);
        if (DropDownListCompanySelectedIndexChanged != null)
            DropDownListCompanySelectedIndexChanged(sender, e);
        
    }
    /// <summary>
    /// 获取所有公司
    /// </summary>
    protected void QueryCompany()
    {
        if (Session["UserInfo"] != null)
        {
            EmployeeConnShopOperate employeeConnShopOperate = new EmployeeConnShopOperate();
            List<VAEmployeeCompany> employeeCompany = new List<VAEmployeeCompany>();
            VAEmployeeLoginResponse vAEmployeeLoginResponse = (VAEmployeeLoginResponse)Session["UserInfo"];
            int employeeID = vAEmployeeLoginResponse.employeeID;
            employeeCompany = employeeConnShopOperate.QueryEmployeeCompany(employeeID);
            DropDownList_Company.DataSource = employeeCompany;
            DropDownList_Company.DataTextField = "CompanyName";
            DropDownList_Company.DataValueField = "CompanyID";
            DropDownList_Company.DataBind();
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "js", "<script>window.open('../Login.aspx',target='_top')</script>");
        }
    }
}