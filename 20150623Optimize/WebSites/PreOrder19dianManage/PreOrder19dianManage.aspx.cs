using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.WebPageDll;
using System.Data;
using VAGastronomistMobileApp.Model;
public partial class PreOrder19dianManage_PreOrder19dianManage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Label_Error.Text = "";
        if (!IsPostBack)
        {
            GetCompanyInfo();
            Button_Time_Click(Button_1day, null);
            Button_Paid_Click(Button_Paid, null);
            QueryShop();
        }
    }
    /// <summary>
    /// 获取公司信息
    /// </summary>
    protected void GetCompanyInfo()
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
    }
    protected void QueryShop()
    {
        if (DropDownList_Company.Items.Count == 0)
        {
            return;//没有公司信息，直接不显示门店信息
        }
        else
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
            GetPreOrder19dian(0, 10);
        }
    }
    /// <summary>
    /// 获取点单信息
    /// </summary>
    protected void GetPreOrder19dian(int str, int end)
    {
        if (DropDownList_Company.Items.Count == 0 || DropDownList_Shop.Items.Count == 0)
        {
            return;//没有公司或者门店或者加载列表失败，直接返回
        }
        int companyId = Common.ToInt32(DropDownList_Company.SelectedValue);
        int shopId = Common.ToInt32(DropDownList_Shop.SelectedValue);
        PreOrder19dianOperate preOrder19dianOperate = new PreOrder19dianOperate();
        string preOrder19dianId = TextBox_preOrder19dianId.Text.Trim();
        DataTable dtPreOrder19dian = new DataTable();
        DataView dv = new DataView();
        if (Common.ToInt32(DropDownList_SelectWay.SelectedValue) == 1)//操作的是点单编号
        {
            if (!string.IsNullOrEmpty(preOrder19dianId))//此时表示点单编号不为空
            {
                //触发下面代码：文本框输入流水号，点击查询按钮操作
                switch (Label_Paid.Text)//根据点单编号查询点单信息（不考虑时间）
                {
                    case "y"://已支付eCardNumber  verificationCode
                        dtPreOrder19dian = preOrder19dianOperate.QueryPreOrderById(Common.ToInt32(preOrder19dianId));
                        dv = dtPreOrder19dian.DefaultView;
                        dv.RowFilter = "isPaid=" + (int)VAPreorderIsPaid.PAID;
                        break;
                    case "n"://未支付
                        dtPreOrder19dian = preOrder19dianOperate.QueryPreOrderById(Common.ToInt32(preOrder19dianId));
                        dv = dtPreOrder19dian.DefaultView;
                        dv.RowFilter = "isPaid=" + (int)VAPreorderIsPaid.NOT_PAID + "or isPaid is null";
                        break;
                    case "a"://全部
                        dtPreOrder19dian = preOrder19dianOperate.QueryPreOrderById(Common.ToInt32(preOrder19dianId));
                        dv = dtPreOrder19dian.DefaultView;
                        dv.RowFilter = "1=1";
                        break;
                    default:
                        break;
                }
                if (dv.Count > 0)
                {
                    try
                    {
                        DropDownList_Company.SelectedValue = dv[0]["companyId"].ToString();
                        DropDownList_Shop.SelectedValue = dv[0]["shopId"].ToString();
                    }
                    catch { }
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('输入有误，查询信息不存在');</script>");
                }
            }
            else
            {
                //触发下面代码：点击条件查询按钮操作
                DateTime timeFrom = Common.ToDateTime(TextBox_preOrderTimeStr.Text + " 00:00:00");
                DateTime timeTo = Common.ToDateTime(TextBox_preOrderTimeEnd.Text + " 23:59:59");
                string queryCondition = Label_Paid.Text;//查询条件（已支付，未支付，全部）
                dtPreOrder19dian = preOrder19dianOperate.QueryPreOrder19dian(companyId, shopId, timeFrom, timeTo, queryCondition);//可能返回验证时间可能为返回验证时间
                dv = dtPreOrder19dian.DefaultView;
            }
        }
        else if (Common.ToInt32(DropDownList_SelectWay.SelectedValue) == 3)//操作手机号码或尾号
        {
            if (!string.IsNullOrEmpty(preOrder19dianId))//此时表示点单编号不为空
            {
                string mobilePhoneNumber = Common.ToClearSpecialCharString(preOrder19dianId);
                DateTime timeFrom = Common.ToDateTime("1999-01-01 00:00:00");
                DateTime timeTo = Common.ToDateTime("9999-12-31 23:59:59");
                dtPreOrder19dian = preOrder19dianOperate.QueryPreOrderShopVerified(shopId, timeFrom, timeTo, 1);
                dv = dtPreOrder19dian.DefaultView;
                switch (Label_Paid.Text)//根据点单编号查询点单信息（不考虑时间）
                {
                    case "y"://已支付eCardNumber  verificationCode
                        dv.RowFilter = "mobilePhoneNumber like '%" + mobilePhoneNumber + "%' and isPaid=" + (int)VAPreorderIsPaid.PAID;//模糊匹配手机号码
                        break;
                    case "n"://未支付
                        dv.RowFilter = "mobilePhoneNumber like '%" + mobilePhoneNumber + "%' and ( isPaid=" + (int)VAPreorderIsPaid.NOT_PAID + "or isPaid is null)";//模糊匹配手机号码
                        break;
                    case "a"://未支付
                        dv.RowFilter = "mobilePhoneNumber like '%" + mobilePhoneNumber + "%'";//模糊匹配手机号码
                        break;
                }
                if (dv.Count > 0)
                {
                    try
                    {
                        DropDownList_Company.SelectedValue = dv[0]["companyId"].ToString();
                        DropDownList_Shop.SelectedValue = dv[0]["shopId"].ToString();
                    }
                    catch { }
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('输入有误，查询信息不存在');</script>");
                }
            }
            else
            {
                //触发下面代码：点击条件查询按钮操作
                DateTime timeFrom = Common.ToDateTime(TextBox_preOrderTimeStr.Text + " 00:00:00");
                DateTime timeTo = Common.ToDateTime(TextBox_preOrderTimeEnd.Text + " 23:59:59");
                string queryCondition = Label_Paid.Text;//查询条件（已支付，未支付）
                dtPreOrder19dian = preOrder19dianOperate.QueryPreOrder19dian(companyId, shopId, timeFrom, timeTo, queryCondition);//可能返回验证时间可能为返回验证时间
                dv = dtPreOrder19dian.DefaultView;
            }
        }
        if (dv != null && dv.Count > 0)
        {
            dv.Sort = " preOrderTime desc";
        }
        AspNetPager1.RecordCount = dv.Count;
        if (dv.Count > 0)
        {
            DataTable dt_PreOrders = dv.ToTable();
            double preOrderServerSum = Common.ToDouble(dt_PreOrders.Compute("sum(preOrderServerSum)", "1=1"));
            double prePaidSum = Common.ToDouble(dt_PreOrders.Compute("sum(prePaidSum)", "1=1"));
            Label_preOrderServerSumSum.Text = preOrderServerSum.ToString();
            Label_prePaidSumSum.Text = prePaidSum.ToString();
            Label_OrderCount.Text = dv.Count.ToString();
            DataTable dt_page = Common.GetPageDataTable(dt_PreOrders, str, end);
            GridView_PreOrder19dian.DataSource = dt_page;
            PanelPage.Visible = true;
        }
        else
        {
            GridView_PreOrder19dian.DataSource = null;
            PanelPage.Visible = false;
        }
        GridView_PreOrder19dian.DataBind();
        GridView_PreOrder19dian.SelectedIndex = -1;
        Button_back.Visible = false;
        Panel_PreOrder19dian.Visible = true;
        Panel_Detail.Visible = false;
    }
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        GetPreOrder19dian(AspNetPager1.StartRecordIndex - 1, AspNetPager1.EndRecordIndex);
    }
    protected void DropDownList_Company_SelectedIndexChanged(object sender, EventArgs e)
    {
        QueryShop();
        TextBox_preOrder19dianId.Text = "";
    }
    protected void GridView_PreOrder19dian_SelectedIndexChanged(object sender, EventArgs e)
    {
        Button_back.Visible = true;
        Panel_PreOrder19dian.Visible = false;
        Panel_Detail.Visible = true;
        VAEmployeeLoginResponse vAEmployeeLoginResponse = (VAEmployeeLoginResponse)Session["UserInfo"];
        int employeeID = vAEmployeeLoginResponse.employeeID;
        string preOrder19dianId = GridView_PreOrder19dian.DataKeys[GridView_PreOrder19dian.SelectedIndex].Values["preOrder19dianId"].ToString();
        mainFrame.Attributes.Add("src", "PreOrderDetail.aspx?preOrder19dianId=" + preOrder19dianId + "&status=1&employeeID=" + employeeID);
    }
    /// <summary>
    /// 条件状态
    /// </summary>
    protected void Button_Paid_Click(object sender, EventArgs e)
    {
        TextBox_preOrder19dianId.Text = "";
        Button Button = (Button)sender;
        switch (Button.CommandName)
        {
            case "y":
                Button_Paid.CssClass = "tabButtonBlueClick";
                Button_UnPaid.CssClass = "tabButtonBlueUnClick";
                Button_All.CssClass = "tabButtonBlueUnClick";
                break;
            case "n":
                Button_Paid.CssClass = "tabButtonBlueUnClick";
                Button_UnPaid.CssClass = "tabButtonBlueClick";
                Button_All.CssClass = "tabButtonBlueUnClick";
                break;
            case "a":
                Button_Paid.CssClass = "tabButtonBlueUnClick";
                Button_UnPaid.CssClass = "tabButtonBlueUnClick";
                Button_All.CssClass = "tabButtonBlueClick";
                break;
            case "v":
                Button_Paid.CssClass = "tabButtonBlueUnClick";
                Button_UnPaid.CssClass = "tabButtonBlueUnClick";
                Button_All.CssClass = "tabButtonBlueUnClick";
                break;
            case "zy"://支付并且验证过
                Button_Paid.CssClass = "tabButtonBlueUnClick";
                Button_UnPaid.CssClass = "tabButtonBlueUnClick";
                Button_All.CssClass = "tabButtonBlueUnClick"; ;
                break;
            default:
                break;
        }
        Label_Paid.Text = Button.CommandName;
        GetPreOrder19dian(0, 10);
        Button_back.Visible = false;
        Panel_PreOrder19dian.Visible = true;
        Panel_Detail.Visible = false;
    }
    /// <summary>
    /// 查询日期段
    /// </summary>
    protected void Button_Time_Click(object sender, EventArgs e)
    {
        TextBox_preOrder19dianId.Text = "";
        Button Button = (Button)sender;
        switch (Button.CommandName)
        {
            case "1":
                Button_1day.CssClass = "tabButtonBlueClick";
                Button_7day.CssClass = "tabButtonBlueUnClick";
                Button_30day.CssClass = "tabButtonBlueUnClick";
                Button_yesterday.CssClass = "tabButtonBlueUnClick";//
                TextBox_preOrderTimeStr.Text = DateTime.Now.ToString("yyyy-MM-dd");
                TextBox_preOrderTimeEnd.Text = DateTime.Now.ToString("yyyy-MM-dd");
                break;
            case "yesterday":
                Button_1day.CssClass = "tabButtonBlueUnClick";
                Button_7day.CssClass = "tabButtonBlueUnClick";
                Button_30day.CssClass = "tabButtonBlueUnClick";
                Button_yesterday.CssClass = "tabButtonBlueClick";//
                TextBox_preOrderTimeStr.Text = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
                TextBox_preOrderTimeEnd.Text = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
                break;
            case "7":
                Button_1day.CssClass = "tabButtonBlueUnClick";
                Button_7day.CssClass = "tabButtonBlueClick";
                Button_30day.CssClass = "tabButtonBlueUnClick";
                Button_yesterday.CssClass = "tabButtonBlueUnClick";//
                TextBox_preOrderTimeStr.Text = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
                TextBox_preOrderTimeEnd.Text = DateTime.Now.ToString("yyyy-MM-dd");
                break;
            case "30":
                Button_1day.CssClass = "tabButtonBlueUnClick";
                Button_7day.CssClass = "tabButtonBlueUnClick";
                Button_30day.CssClass = "tabButtonBlueClick";
                Button_yesterday.CssClass = "tabButtonBlueUnClick";//
                TextBox_preOrderTimeStr.Text = DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd");
                TextBox_preOrderTimeEnd.Text = DateTime.Now.ToString("yyyy-MM-dd");
                break;
            default:
                break;
        }
        GetPreOrder19dian(0, 10);
    }
    /// <summary>
    /// 返回
    /// </summary>
    protected void Button_back_Click(object sender, EventArgs e)
    {
        Button_back.Visible = false;
        Panel_PreOrder19dian.Visible = true;
        Panel_Detail.Visible = false;
        GridView_PreOrder19dian.SelectedIndex = -1;
    }
    /// <summary>
    /// 按照流水号查询（合法校验）
    /// </summary>
    protected void Button_preOrder19dianId_Click(object sender, EventArgs e)
    {
        if (Common.ToInt32(DropDownList_SelectWay.SelectedValue) == 1)
            if (Common.ToInt32(TextBox_preOrder19dianId.Text.Trim().ToString()) == 0)//表示文本框输入非法，或者就是输入0
                ErrorPrompt();
            else
                ResultFunction();
        else if (Common.ToInt32(DropDownList_SelectWay.SelectedValue) == 3)
        {
            if (Common.ToInt64(TextBox_preOrder19dianId.Text.Trim().ToString()) == 0)//表示文本框输入非法，或者就是输入0
                Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('请输入手机号码或尾号！');</script>");
            else
                ResultFunction();
        }
    }
    //提示错误信息
    protected void ErrorPrompt()
    {
        Label_Error.Attributes.Add("style", "color:Red");
        Label_Error.Text = "请填写合法eVIP卡号或者验证码";
    }
    // 执行GetPreOrder19dian方法
    protected void ResultFunction()
    {
        SqlInject myCheck = new SqlInject(this.Request);
        string error = myCheck.CheckTextBoxInject(TextBox_preOrder19dianId.Text);
        if (error == "")
        {
            GetPreOrder19dian(0, 10);
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('请不要输入非法字符！');</script>");
        }
    }
    protected void DropDownList_SelectWay_SelectedIndexChanged(object sender, EventArgs e)
    {
        TextBox_preOrder19dianId.Visible = true;
        if (Common.ToInt32(DropDownList_SelectWay.SelectedValue) == 1)//表示选中的是点单流水号
        {
            TextBox_preOrder19dianId.Text = "点单号";
        }
        else if (Common.ToInt32(DropDownList_SelectWay.SelectedValue) == 3)
        {
            TextBox_preOrder19dianId.Text = "手机号码或尾号";
        }
    }
    protected void TextBox_preOrderTimeStr_TextChanged(object sender, EventArgs e)
    {
        TextBox_preOrder19dianId.Text = "";
        GetPreOrder19dian(0, 10);
    }
    protected void DropDownList_Shop_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetPreOrder19dian(0, 10);
    }
}
