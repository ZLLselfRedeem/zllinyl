﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.WebPageDll;
using System.Data;
using VAGastronomistMobileApp.Model;

public partial class PreOrder19dianManage_PreOrderShopConfirmed : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Label_Error.Text = "";
        if (!IsPostBack)
        {
            GetCompanyInfo();
            BindShopDropDownList();
            Button_Time_Click(Button_1day, null);
            Button_Approved_Click(Button_UnApproved, null);
        }
    }
    /// <summary>
    /// 获取公司信息
    /// </summary>
    protected void GetCompanyInfo()
    {
        if (Session["UserInfo"] != null)
        {
            VAEmployeeLoginResponse vAEmployeeLoginResponse = (VAEmployeeLoginResponse)Session["UserInfo"];
            int employeeID = vAEmployeeLoginResponse.employeeID;
            EmployeeConnShopOperate employeeConnShopOperate = new EmployeeConnShopOperate();
            List<VAEmployeeCompany> employeeCompany = employeeConnShopOperate.QueryEmployeeCompany(employeeID);
            DropDownList_Company.DataSource = employeeCompany;
            DropDownList_Company.DataTextField = "companyName";
            DropDownList_Company.DataValueField = "companyID";
            DropDownList_Company.DataBind();
        }
    }
    /// <summary>
    /// 获取门店信息
    /// </summary>
    protected void BindShopDropDownList()
    {
        if (DropDownList_Company.Items.Count == 0)
        {
            return;
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
        //初始化
        int status = 0;
        int shopId = Common.ToInt32(DropDownList_Shop.SelectedValue);
        DateTime timeFrom, timeTo;
        string strFilter = "";
        if (Label_Approved.Text == "y")
        {
            strFilter += " isShopConfirmed=" + (int)VAPreOrderShopConfirmed.SHOPCONFIRMED;
        }
        else if (Label_Approved.Text == "n")
        {
            strFilter += " (isShopConfirmed=" + (int)VAPreOrderShopConfirmed.NOT_SHOPCONFIRMED + " or isShopConfirmed is null)";
        }
        PreOrder19dianOperate preOrder19dianOperate = new PreOrder19dianOperate();
        DataTable dtPreOrder19dian = new DataTable();
        DataView dv = new DataView();
        string mobilePhoneNumber = Common.ToClearSpecialCharString(TextBox_preOrder19dianId.Text.Trim());//手机号码或尾号
        //case1：输入的流水号不为空
        if (!string.IsNullOrEmpty(mobilePhoneNumber))
        {
            timeFrom = Common.ToDateTime("1999-01-01 00:00:00");
            timeTo = Common.ToDateTime("9999-12-31 23:59:59");
            status = 1;
            dtPreOrder19dian = preOrder19dianOperate.QueryPreOrderShopVerified(shopId, timeFrom, timeTo, status);
            dv = dtPreOrder19dian.DefaultView;
            dv.RowFilter = "mobilePhoneNumber like '%" + mobilePhoneNumber + "%' and" + strFilter;//模糊匹配手机号码
            if (dv.Count > 0)
            {
                DropDownList_Company.SelectedValue = dv[0]["companyId"].ToString();
                BindShopDropDownList();
                DropDownList_Shop.SelectedValue = dv[0]["shopId"].ToString();
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('输入有误，流水号不存在');</script>");
            }
        }
        // case2：未输入流水号查询
        else
        {
            timeFrom = Common.ToDateTime(TextBox_preOrderTimeStr.Text + " 00:00:00");
            timeTo = Common.ToDateTime(TextBox_preOrderTimeEnd.Text + " 23:59:59");
            status = 2;
            dtPreOrder19dian = preOrder19dianOperate.QueryPreOrderShopVerified(shopId, timeFrom, timeTo, status);//根据门店，时间和筛选状态过滤数据
            dv = dtPreOrder19dian.DefaultView;
            dv.RowFilter = strFilter;
        }
        dv.Sort = " preOrderTime desc";
        AspNetPager1.RecordCount = dv.Count;
        if (dv.Count > 0)
        {
            DataTable dt_PreOrders = dv.ToTable();
            double preOrderServerSum = Common.ToDouble(dt_PreOrders.Compute("sum(preOrderServerSum)", "1=1"));
            double prePaidSum = Common.ToDouble(dt_PreOrders.Compute("sum(prePaidSum)", "1=1"));
            Label_preOrderServerSumSum.Text = (preOrderServerSum).ToString();
            Label_prePaidSumSum.Text = (prePaidSum).ToString();
            Label_OrderCount.Text = dv.Count.ToString();
            DataTable dt_page = Common.GetPageDataTable(dt_PreOrders, str, end);
            GridView_PreOrderShopVerified.DataSource = dt_page;
            PanelPage.Visible = true;
        }
        else
        {
            GridView_PreOrderShopVerified.DataSource = null;
            PanelPage.Visible = false;
        }
        GridView_PreOrderShopVerified.DataBind();
        GridView_PreOrderShopVerified.SelectedIndex = -1;
        Button_back.Visible = false;
        Panel_PreOrder19dian.Visible = true;
        Panel_Detail.Visible = false;
    }
    /// <summary>
    /// 选择分页，刷新列表
    /// </summary>
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        GetPreOrder19dian(AspNetPager1.StartRecordIndex - 1, AspNetPager1.EndRecordIndex);
    }
    /// <summary>
    /// 选择公司，刷新列表
    /// </summary>
    protected void DropDownList_Company_SelectedIndexChanged(object sender, EventArgs e)
    {
        TextBox_preOrder19dianId.Text = "";
        BindShopDropDownList();
        GetPreOrder19dian(0, 10);
    }
    /// <summary>
    /// 入座，标记按钮
    /// </summary>
    protected void Button_Approved_Click(object sender, EventArgs e)
    {
        TextBox_preOrder19dianId.Text = "";
        Button Button = (Button)sender;
        if (Button.CommandName == "y")
        {
            Button_Approved.CssClass = "tabButtonBlueClick";
            Button_UnApproved.CssClass = "tabButtonBlueUnClick";
            Button_ShopConfirmed.CommandName = ((int)VAPreOrderShopConfirmed.NOT_SHOPCONFIRMED).ToString();
            Button_ShopConfirmed.Text = "取消入座";
        }
        if (Button.CommandName == "n")
        {
            Button_Approved.CssClass = "tabButtonBlueUnClick";
            Button_UnApproved.CssClass = "tabButtonBlueClick";
            Button_ShopConfirmed.CommandName = ((int)VAPreOrderShopConfirmed.SHOPCONFIRMED).ToString();
            Button_ShopConfirmed.Text = "审     核";
        }
        Label_Approved.Text = Button.CommandName;
        GetPreOrder19dian(0, 10);
        Button_back_Click(null, null);
    }
    /// <summary>
    /// 选择查询周期
    /// </summary>
    protected void Button_Time_Click(object sender, EventArgs e)
    {
        TextBox_preOrder19dianId.Text = "";
        Button Button = (Button)sender;
        switch (Button.CommandName)
        {
            case "1":
                {
                    Button_1day.CssClass = "tabButtonBlueClick";
                    Button_7day.CssClass = "tabButtonBlueUnClick";
                    Button_30day.CssClass = "tabButtonBlueUnClick";
                    Button_yesterday.CssClass = "tabButtonBlueUnClick";//
                    TextBox_preOrderTimeStr.Text = DateTime.Now.ToString("yyyy-MM-dd");
                    TextBox_preOrderTimeEnd.Text = DateTime.Now.ToString("yyyy-MM-dd");
                }
                break;
            case "yesterday":
                {
                    Button_1day.CssClass = "tabButtonBlueUnClick";
                    Button_7day.CssClass = "tabButtonBlueUnClick";
                    Button_30day.CssClass = "tabButtonBlueUnClick";
                    Button_yesterday.CssClass = "tabButtonBlueClick";//
                    TextBox_preOrderTimeStr.Text = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
                    TextBox_preOrderTimeEnd.Text = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
                }
                break;
            case "7":
                {
                    Button_1day.CssClass = "tabButtonBlueUnClick";
                    Button_7day.CssClass = "tabButtonBlueClick";
                    Button_30day.CssClass = "tabButtonBlueUnClick";
                    Button_yesterday.CssClass = "tabButtonBlueUnClick";//
                    TextBox_preOrderTimeStr.Text = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
                    TextBox_preOrderTimeEnd.Text = DateTime.Now.ToString("yyyy-MM-dd");
                }
                break;
            case "30":
                {
                    Button_1day.CssClass = "tabButtonBlueUnClick";
                    Button_7day.CssClass = "tabButtonBlueUnClick";
                    Button_30day.CssClass = "tabButtonBlueClick";
                    Button_yesterday.CssClass = "tabButtonBlueUnClick";//
                    TextBox_preOrderTimeStr.Text = DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd");
                    TextBox_preOrderTimeEnd.Text = DateTime.Now.ToString("yyyy-MM-dd");
                }
                break;
            default:
                break;
        }
        GetPreOrder19dian(0, 10);
    }
    /// <summary>
    /// 返回按钮
    /// </summary>
    protected void Button_back_Click(object sender, EventArgs e)
    {
        Button_back.Visible = false;
        Panel_PreOrder19dian.Visible = true;
        Panel_Detail.Visible = false;
        GetPreOrder19dian(AspNetPager1.StartRecordIndex - 1, AspNetPager1.EndRecordIndex);
        GridView_PreOrderShopVerified.SelectedIndex = -1;
    }
    /// <summary>
    /// 选择门店，刷新列表
    /// </summary>
    protected void DropDownList_Shop_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetPreOrder19dian(0, 10);
    }
    /// <summary>
    /// 入座点单
    /// </summary>
    protected void Button_Approve_Click(object sender, EventArgs e)
    {
        int preOrder19dianId = Common.ToInt32(GridView_PreOrderShopVerified.DataKeys[GridView_PreOrderShopVerified.SelectedIndex].Values["preOrder19dianId"].ToString());
        ConfrimOperate(preOrder19dianId);
    }
    /// <summary>
    /// 入座点单操作
    /// </summary>
    /// <param name="preOrder19dianId"></param>
    protected void ConfrimOperate(int preOrder19dianId)
    {
        SybMoneyMerchantOperate syb = new SybMoneyMerchantOperate();
        int result = syb.ConfrimPreOrder(preOrder19dianId, Common.ToInt32(Button_ShopConfirmed.CommandName), PreOrderConfirmOperater.Cash, ((VAEmployeeLoginResponse)Session["UserInfo"]).employeeID);
        switch (result)
        {
            case 1:
                //入座成功
                Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('操作成功！');</script>");
                Panel_PreOrder19dian.Visible = true;
                Panel_Detail.Visible = false;
                GetPreOrder19dian(0, 10);//重新绑定数据，刷新GridView页面信息
                break;
            case -1:
                //前端提示：当前点单已对账，无法取消入座
                Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('当前点单已对账，无法取消入座！');</script>");
                break;
            case -2:
                //前端提示：当前单子是未入座状态，无法取消入座
                Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('当前单子是未入座状态，无法取消入座！');</script>");
                break;
            case -3:
                //前端提示：当前单子是已入座状态，无法入座
                Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('当前单子是已入座状态，无法入座！');</script>");
                break;
            default:
                break;
        }
    }
    protected void Button_preOrder19dianId_Click(object sender, EventArgs e)
    {
        if (Common.ToInt32(TextBox_preOrder19dianId.Text.Trim().ToString()) == 0)//表示文本框输入非法，或者就是输入0
        {
            Label_Error.Attributes.Add("style", "color:Red");
            Label_Error.Text = "请输入手机号码或尾号";
        }
        else
        {
            SqlInject myCheck = new SqlInject(this.Request);
            string error = myCheck.CheckTextBoxInject(TextBox_preOrder19dianId.Text);
            if (error == "")
            {
                GetPreOrder19dian(0, 10);
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('请输入手机号码或尾号！');</script>");
            }
        }
    }
    /// <summary>
    /// GridView RowCommand
    /// </summary>
    protected void GridView_PreOrderShopVerified_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;//转换为按钮类型，获取其所在的行的索引
        int preOrder19dianId = Common.ToInt32(GridView_PreOrderShopVerified.DataKeys[index].Values["preOrder19dianId"].ToString());
        if (e.CommandName.ToString() == "Verified")
        {
            if (Label_Approved.Text == "n")//表示是未入座的信息
            {
                ConfrimOperate(preOrder19dianId);
            }
        }
        else if (e.CommandName.ToString() == "Select")
        {
            Button_back.Visible = true;
            Panel_PreOrder19dian.Visible = false;
            Panel_Detail.Visible = true;
            mainFrame.Attributes.Add("src", "PreOrderDetail.aspx?preOrder19dianId=" + preOrder19dianId);//status区别是入座还是入座显示
        }
    }
    /// <summary>
    /// 隐藏对账按钮显示问题
    /// </summary>
    protected void GridView_PreOrderShopVerified_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (Label_Approved.Text == "y")//表示是选择的是已入座的信息
        {
            GridView_PreOrderShopVerified.Columns[8].Visible = false;
        }
        else if (Label_Approved.Text == "n")//表示是选择的是已入座的信息
        {
            GridView_PreOrderShopVerified.Columns[8].Visible = true;
        }
    }
    /// <summary>
    /// 选择时间，刷新列表
    /// </summary>
    protected void TextBox_preOrderTimeStr_TextChanged(object sender, EventArgs e)
    {
        TextBox_preOrder19dianId.Text = "";
        GetPreOrder19dian(0, 10);
    }
}