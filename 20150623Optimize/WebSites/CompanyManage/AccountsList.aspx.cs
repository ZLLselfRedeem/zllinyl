using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.WebPageDll;
using System.Data;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL;
using System.Configuration;

public partial class CompanyManage_AccountsList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["companyID"] != null)
            {
                SetCompanyInfo();
                GridBindData();
            }
        }
    }
    protected void SetCompanyInfo()
    {
        if (Session["UserInfo"] != null)
        {
            VAEmployeeLoginResponse vAEmployeeLoginResponse = (VAEmployeeLoginResponse)Session["UserInfo"];
            int employeeID = vAEmployeeLoginResponse.employeeID;
            EmployeeConnShopOperate employeeConnShopOperate = new EmployeeConnShopOperate();
            List<VAEmployeeCompany> employeeCompany = new List<VAEmployeeCompany>();
            employeeCompany.AddRange(employeeConnShopOperate.QueryEmployeeCompany(employeeID));
            DropDownList_Companys.DataSource = employeeCompany;
            DropDownList_Companys.DataValueField = "companyID";
            DropDownList_Companys.DataTextField = "companyName";
            DropDownList_Companys.DataBind();
            DropDownList_Companys.SelectedValue = Common.ToString(Request.QueryString["companyID"]);//直接显示选中的门店
        }
    }
    ///// <summary>
    ///// 绑定显示银行下拉列表
    ///// </summary>
    //void SetBankNameInfo(string selectBank)
    //{
    //    DropDownList_BankName.Items.Clear();
    //    string bankName = ConfigurationManager.AppSettings["bankName"].ToString();
    //    if (bankName != "")
    //    {
    //        string[] strName = bankName.Split(',');
    //        for (int i = 0; i < strName.Length; i++)
    //        {
    //            DropDownList_BankName.Items.Add(strName[i]);
    //        }
    //    }
    //    if (!String.IsNullOrEmpty(selectBank))
    //    {
    //        DropDownList_BankName.SelectedValue = selectBank;
    //    }
    //}
    /// <summary>
    /// 绑定列表数据
    /// </summary>
    public void GridBindData()
    {
        int companyId = Common.ToInt32(Request.QueryString["companyID"]);
        CompanyAccountOprate oprate = new CompanyAccountOprate();
        DataTable dt_Accounts = oprate.QueryAccountByCompanyId(companyId);
        GridView_CompanyAccounts.DataSource = dt_Accounts;
        GridView_CompanyAccounts.DataBind();
        panel_list.Visible = true;
        panel_operate.Visible = false;
    }
    /// <summary>
    /// 添加按钮事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button_Add_Click(object sender, EventArgs e)
    {
        // SetBankNameInfo("");
        Button_Add.Visible = false;
        panel_list.Visible = false;
        panel_operate.Visible = true;
        btn_ok.CommandName = "add";
        TextBox_AccountNum.Text = "";
        TextBox_Remark.Text = "";
        TextBox__BankName.Text = "";
        tb_accountName.Text = "";
    }
    /// <summary>
    /// 删除对应银行帐号
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GridView_CompanyAccounts_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        CompanyAccountManager companyAccountManager = new CompanyAccountManager();
        int identity_Id = Common.ToInt32(GridView_CompanyAccounts.DataKeys[e.RowIndex].Values["identity_Id"].ToString());
        bool i = companyAccountManager.DeleteAccountInfo(identity_Id);
        if (i == true)
        {
            Common.RecordEmployeeOperateLog((int)VAEmployeeOperateLogOperatePageType.COMPANY_ACCOUNTS, (int)VAEmployeeOperateLogOperateType.DELETE_OPERATE, "公司名称："
                + DropDownList_Companys.SelectedItem.Text + "，删除银行帐号：" + GridView_CompanyAccounts.DataKeys[e.RowIndex].Values["accountNum"].ToString());
            GridBindData();
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "js", "<script>alert('删除失败');</script>");
        }
    }
    /// <summary>
    /// 列表修改事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GridView_CompanyAccounts_SelectedIndexChanged(object sender, EventArgs e)
    {
        panel_list.Visible = false;
        panel_operate.Visible = true;
        Button_Add.Visible = false;
        btn_ok.CommandName = "update";
        //赋值
        TextBox_AccountNum.Text = GridView_CompanyAccounts.DataKeys[GridView_CompanyAccounts.SelectedIndex].Values["accountNum"].ToString();
        TextBox_Remark.Text = GridView_CompanyAccounts.DataKeys[GridView_CompanyAccounts.SelectedIndex].Values["remark"].ToString();
        btn_back.CommandName = GridView_CompanyAccounts.DataKeys[GridView_CompanyAccounts.SelectedIndex].Values["identity_Id"].ToString();
        tb_accountName.Text = GridView_CompanyAccounts.DataKeys[GridView_CompanyAccounts.SelectedIndex].Values["accountName"].ToString();
        TextBox__BankName.Text = GridView_CompanyAccounts.DataKeys[GridView_CompanyAccounts.SelectedIndex].Values["bankName"].ToString();
        // SetBankNameInfo(selectBank);
    }
    /// <summary>
    /// 返回事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btn_back_Click(object sender, EventArgs e)
    {
        panel_list.Visible = true;
        panel_operate.Visible = false;
        Button_Add.Visible = true;
    }
    /// <summary>
    /// 确定事件（添加，修改）
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btn_ok_Click(object sender, EventArgs e)
    {
        if (String.IsNullOrEmpty(TextBox_AccountNum.Text.Trim()) || String.IsNullOrEmpty(TextBox_NumCheck.Text.Trim()) || String.IsNullOrEmpty(tb_accountName.Text.Trim()) || String.IsNullOrEmpty(TextBox__BankName.Text.Trim()))
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('页面信息填写有误！');</script>");
            return;
        }
        if (TextBox_AccountNum.Text.Trim() != TextBox_NumCheck.Text.Trim())
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('帐号核对号码有误');</script>");
            return;
        }
        CompanyAccountOprate oprate = new CompanyAccountOprate();
        if (btn_ok.CommandName == "add")  //新增
        {
            CompanyAccountInfo account = new CompanyAccountInfo();
            account.companyId = Common.ToInt32(Request.QueryString["companyID"]);
            account.accountNum = TextBox_AccountNum.Text;
            account.bankName = TextBox__BankName.Text.Trim();//DropDownList_BankName.SelectedItem.ToString();//选择银行文本内容
            account.remark = TextBox_Remark.Text;
            account.accountName = tb_accountName.Text.Trim();
            account.status = 1;
            int flag = oprate.QueryNewAccount(account);
            switch (flag)
            {
                case 1:
                    Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('添加成功！');</script>");
                    TextBox_AccountNum.Text = "";
                    TextBox_NumCheck.Text = "";
                    TextBox_Remark.Text = "";
                    Common.RecordEmployeeOperateLog((int)VAEmployeeOperateLogOperatePageType.COMPANY_ACCOUNTS, (int)VAEmployeeOperateLogOperateType.ADD_OPERATE, "公司名称："
                                                     + Request.QueryString["companyID"].ToString() + "，银行帐号：" + TextBox_AccountNum.Text.Trim()
                                                     + "，银行名称：" + TextBox__BankName.Text.Trim() + "，备注：" + TextBox_Remark.Text.Trim());
                    GridBindData();
                    Button_Add.Visible = true;
                    break;
                case 2:
                    Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('添加失败！');</script>");
                    break;
                case 0:
                    Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('该帐号已存在，请勿添加重复的帐号！');</script>");
                    break;
                default:
                    break;
            }
        }
        else if (btn_ok.CommandName == "update")//修改
        {
            int accountId = Common.ToInt32(btn_back.CommandName);
            string accountNum = TextBox_AccountNum.Text.Trim();
            string bankName = TextBox__BankName.Text.Trim();//DropDownList_BankName.SelectedItem.ToString();
            string remark = TextBox_Remark.Text.Trim();
            string accountName = tb_accountName.Text.Trim();
            bool boo = oprate.QueryUpdateAccount(accountId, accountNum, bankName, remark, accountName);
            if (boo)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('更新成功！');</script>");
                Common.RecordEmployeeOperateLog((int)VAEmployeeOperateLogOperatePageType.COMPANY_ACCOUNTS, (int)VAEmployeeOperateLogOperateType.UPDATE_OPERATE, "修改银行帐号："
                    + TextBox_AccountNum.Text.Trim() + "，银行名称：" + TextBox__BankName.Text.Trim() + "，备注：" + TextBox_Remark.Text.Trim());
                GridBindData();
                Button_Add.Visible = true;
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('更新失败！');</script>");
            }
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('页面发生错误！');</script>");
        }
    }
}