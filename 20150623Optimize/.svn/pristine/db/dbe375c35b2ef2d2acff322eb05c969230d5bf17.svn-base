using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using VAGastronomistMobileApp.WebPageDll;
using System.Collections;

public partial class OtherStatisticalStatement_DataExportExcel : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            QueryCompany();
            QueryShop();
        }
    }
    /// <summary>
    /// 导出excel操作
    /// </summary>
    protected void Button_operate_Click(object sender, EventArgs e)
    {
        StatisticalStatementOperate statisticalStatementOperate = new StatisticalStatementOperate();
        DataTable dt = statisticalStatementOperate.QueryPreOrder(0);
        DataView dv = dt.DefaultView;
        string filter = "1=1";
        #region DataView过滤DataTable
        if (TextBox_Number.Text != "")//过滤eVIP卡号
        {
            filter += " and 手机号码='" + Common.ToInt32(TextBox_Number.Text.Trim()) + "'";
        }
        //if (TextBox_orderStartTime.Text != "" && TextBox_orderEndTime.Text != "")//过滤下单时间
        //{
        //    DateTime staTime = Common.ToDateTime(TextBox_orderStartTime.Text + " 00:00:00");
        //    DateTime endTime = Common.ToDateTime(TextBox_orderEndTime.Text + " 23:59:59");
        //    filter += " and 下单时间 >='" + staTime + "'";
        //    filter += " and 下单时间 <='" + endTime + "'";
        //}
        if (TextBox_preOrderTimeStr.Text != "" && TextBox_preOrderTimeEnd.Text != "")//过滤支付时间
        {
            DateTime staTime = Common.ToDateTime(TextBox_preOrderTimeStr.Text + " 00:00:00");
            DateTime endTime = Common.ToDateTime(TextBox_preOrderTimeEnd.Text + " 23:59:59");
            filter += " and 支付时间 >='" + staTime + "'";
            filter += " and 支付时间 <='" + endTime + "'";
        }
        if (TextBox_verificationStartTime.Text != "" && TextBox_verificationEndTime.Text != "")//过滤验证时间
        {
            DateTime staTime = Common.ToDateTime(TextBox_verificationStartTime.Text + " 00:00:00");
            DateTime endTime = Common.ToDateTime(TextBox_verificationEndTime.Text + " 23:59:59");
            filter += " and 入座时间 >='" + staTime + "'";
            filter += " and 入座时间 <='" + endTime + "'";
        }
        if (Common.ToInt32(DropDownList_Company.SelectedValue) != 0)
        {
            if (Common.ToInt32(DropDownList_Shop.SelectedValue) != 0)//过滤公司和门店
            {
                filter += " and 品牌名 ='" + Common.ToString(DropDownList_Company.SelectedItem.Text) + "'";
                filter += " and 店铺名 <='" + Common.ToString(DropDownList_Shop.SelectedItem.Text) + "'";
            }
            else//过滤公司
            {
                filter += " and 品牌名 ='" + Common.ToString(DropDownList_Company.SelectedItem.Text) + "'";
            }
        }
        if (TextBox_paymentMin.Text.Trim() != "" && TextBox_paymentMax.Text.Trim() != "")//过滤支付金额范围
        {
            filter += "支付金额 >= '" + Common.ToDouble(TextBox_paymentMin.Text.Trim()) + "'" + " and 支付金额 <='" + Common.ToDouble(TextBox_paymentMax.Text.Trim()) + "'";
        }
        //if (TextBox_orderMin.Text.Trim() != "" && TextBox_orderMax.Text.Trim() != "")//过滤点单金额范围
        //{
        //    filter += "点单金额 >= '" + Common.ToDouble(TextBox_paymentMin.Text.Trim()) + "'" + " and 点单金额 <='" + Common.ToDouble(TextBox_paymentMax.Text.Trim()) + "'";
        //}
        dv.RowFilter = filter;
        #endregion
        if (dv.Count > 0)
        {
            DataTable dvToDataTable = dv.ToTable();
            CreateExcel(dvToDataTable);
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('无数据');</script>");
        }
    }
    /// <summary>
    /// 获取所有上线公司
    /// </summary>
    protected void QueryCompany()
    {
        EmployeeConnShopOperate employeeConnShopOperate = new EmployeeConnShopOperate();
        List<VAEmployeeCompany> employeeCompany = new List<VAEmployeeCompany>();
        VAEmployeeLoginResponse vAEmployeeLoginResponse = (VAEmployeeLoginResponse)Session["UserInfo"];
        int employeeID = vAEmployeeLoginResponse.employeeID;
        employeeCompany = employeeConnShopOperate.QueryEmployeeCompany(employeeID);//查询所有的上线的门店
        DropDownList_Company.DataSource = employeeCompany;
        DropDownList_Company.DataTextField = "CompanyName";
        DropDownList_Company.DataValueField = "CompanyID";
        DropDownList_Company.DataBind();
        DropDownList_Company.Items.Add(new ListItem("所有公司", "0"));
        DropDownList_Company.SelectedValue = "0";//默认选择所有公司
    }
    /// <summary>
    /// 获取门店信息
    /// </summary>
    protected void QueryShop()
    {
        EmployeeConnShopOperate employeeConnShopOperate = new EmployeeConnShopOperate();
        List<VAEmployeeShop> employeeShop = new List<VAEmployeeShop>();
        VAEmployeeLoginResponse vAEmployeeLoginResponse = (VAEmployeeLoginResponse)Session["UserInfo"];
        int employeeID = vAEmployeeLoginResponse.employeeID;
        int companyID = Common.ToInt32(DropDownList_Company.SelectedValue);
        employeeShop = employeeConnShopOperate.QueryEmployeeShopByCompanyAndEmplyee(employeeID, companyID);
        DropDownList_Shop.DataSource = employeeShop;
        DropDownList_Shop.DataTextField = "shopName";
        DropDownList_Shop.DataValueField = "shopID";
        DropDownList_Shop.DataBind();
        DropDownList_Shop.Items.Add(new ListItem("所有门店", "0"));
        DropDownList_Shop.SelectedValue = "0";//选中所有门店
    }
    protected void DropDownList_Company_SelectedIndexChanged(object sender, EventArgs e)
    {
        QueryShop();
    }
    /// <summary>
    /// 由DataTable导出Excel
    /// </summary>
    /// <param name="dt"></param>
    /// <param name="fileName"></param>
    private void CreateExcel(DataTable dt)
    {
        string excelName = "PreOrderStatement_" + DateTime.Now.ToString("yyyy/mm/dd_hh:mm:ss");
        HttpResponse resp;
        resp = Page.Response;
        resp.Buffer = true;
        resp.ClearContent();
        resp.ClearHeaders();
        resp.Charset = "GB2312";
        resp.AppendHeader("Content-Disposition", "attachment;filename=" + excelName + ".xls");
        resp.ContentEncoding = System.Text.Encoding.Default;//设置输出流为简体中文   
        resp.ContentType = "application/ms-excel";//设置输出文件类型为excel文件。 
        string colHeaders = "", ls_item = "";
        DataRow[] myRow = dt.Select();
        int i = 0;
        int cl = dt.Columns.Count;
        for (i = 0; i < cl; i++)
        {
            if (i == (cl - 1))//最后一列，加n
            {
                colHeaders += dt.Columns[i].Caption.ToString().Trim() + "\n";
            }
            else
            {
                colHeaders += dt.Columns[i].Caption.ToString().Trim() + "\t";
            }
        }
        resp.Write(colHeaders);
        foreach (DataRow row in myRow)
        {
            for (i = 0; i < cl; i++)
            {
                if (i == (cl - 1))//最后一列，加n
                {
                    ls_item += row[i].ToString().Trim() + "\n";
                }
                else
                {
                    ls_item += row[i].ToString().Trim() + "\t";
                }
            }
            resp.Write(ls_item);
            ls_item = "";
        }
        resp.End();
    }
}