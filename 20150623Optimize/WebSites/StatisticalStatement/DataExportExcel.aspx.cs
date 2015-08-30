using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using VAGastronomistMobileApp.WebPageDll;
using System.Collections;
using System.Text;
using Web.Control;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL;
using Web.Control.DDL;

public partial class StatisticalStatement_DataExportExcel : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            new CityDropDownList().BindCity(ddlCity);
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
        DataTable dt = statisticalStatementOperate.QueryPreOrder(Common.ToInt32(ddlCity.SelectedValue));
        DataView dv = dt.DefaultView;
        var filter = new StringBuilder("1=1");
        #region DataView过滤DataTable
        if (!String.IsNullOrWhiteSpace(TextBox_Number.Text))//过滤手机号码
        {
            filter.AppendFormat(" and 手机号码='{0}'", TextBox_Number.Text.Trim());
        }
        if (!String.IsNullOrWhiteSpace(TextBox_orderStartTime.Text) && !String.IsNullOrWhiteSpace(TextBox_orderEndTime.Text))//过滤下单时间
        {
            filter.AppendFormat(" and 下单时间 >='{0}' and 下单时间 <='{1}'", Common.ToDateTime(TextBox_orderStartTime.Text + " 00:00:00"), Common.ToDateTime(TextBox_orderEndTime.Text + " 23:59:59"));
        }
        if (!String.IsNullOrWhiteSpace(TextBox_preOrderTimeStr.Text) && !String.IsNullOrWhiteSpace(TextBox_preOrderTimeEnd.Text))//过滤支付时间
        {
            filter.AppendFormat(" and 支付时间 >='{0}' and 支付时间 <='{1}'", Common.ToDateTime(TextBox_preOrderTimeStr.Text + " 00:00:00"), Common.ToDateTime(TextBox_preOrderTimeEnd.Text + " 23:59:59"));
        }
        if (!String.IsNullOrWhiteSpace(TextBox_verificationStartTime.Text) && !String.IsNullOrWhiteSpace(TextBox_verificationEndTime.Text))//过滤入座时间
        {
            filter.AppendFormat(" and 入座时间 >='{0}'  and 入座时间 <='{1}'", Common.ToDateTime(TextBox_verificationStartTime.Text + " 00:00:00"), Common.ToDateTime(TextBox_verificationEndTime.Text + " 23:59:59"));
        }
        if (Common.ToInt32(DropDownList_Company.SelectedValue) != 0)
        {
            if (Common.ToInt32(DropDownList_Shop.SelectedValue) != 0)//过滤公司和门店
            {
                filter.AppendFormat(" and 品牌名 ='{0}' and 店铺名 ='{1}'", Common.ToString(DropDownList_Company.SelectedItem.Text), Common.ToString(DropDownList_Shop.SelectedItem.Text));
            }
            else//过滤公司
            {
                filter.AppendFormat(" and 品牌名 ='{0}'", Common.ToString(DropDownList_Company.SelectedItem.Text));
            }
        }
        if (!String.IsNullOrWhiteSpace(TextBox_paymentMin.Text.Trim()) && !String.IsNullOrWhiteSpace(TextBox_paymentMax.Text.Trim()))//过滤支付金额范围
        {
            filter.AppendFormat(" and 支付金额 >= {0} and 支付金额 <={1}", Common.ToDouble(TextBox_paymentMin.Text.Trim()), Common.ToDouble(TextBox_paymentMax.Text.Trim()));
        }
        if (!String.IsNullOrWhiteSpace(TextBox_orderMin.Text.Trim()) && !String.IsNullOrWhiteSpace(TextBox_orderMax.Text.Trim()))//过滤点单金额范围
        {
            filter.AppendFormat(" and 点单金额 >= {0} and 点单金额 <={1}", Common.ToDouble(TextBox_paymentMin.Text.Trim()), Common.ToDouble(TextBox_paymentMax.Text.Trim()));
        }
        dv.RowFilter = filter.ToString();
        #endregion
        if (dv.Count > 0)
        {
            DataTable dvToDataTable = dv.ToTable();
            dvToDataTable.Columns.Add("(退)粮票", typeof(string));
            dvToDataTable.Columns.Add("(退)支付宝", typeof(string));
            dvToDataTable.Columns.Add("(退)微信", typeof(string));
            dvToDataTable.Columns.Add("(退)其他", typeof(string));
            PreOrder19dianManager manager = new PreOrder19dianManager();
            ThirdPartyPaymentInfo thirdPartyPaymentInfo = new ThirdPartyPaymentInfo();
            List<OrderPayMode> payModeList = new List<OrderPayMode>();
            foreach (DataRow dr in dvToDataTable.Rows)
            {
                dr["(退)粮票"] = "0";
                dr["(退)支付宝"] = "0";
                dr["(退)微信"] = "0";
                dr["(退)其他"] = "0";
                if (Common.ToDouble(dr["支付金额"]) > 0 && Common.ToDouble(dr["退款金额"]) > 0)
                {
                    thirdPartyPaymentInfo = manager.SelectPreorderPayAmount(Common.ToInt64(dr["订单编号"]));
                    payModeList = new ZZBPreOrderOperate().GetOrderPayModeInfo(Common.ToDouble(dr["支付金额"]), thirdPartyPaymentInfo, true, Common.ToInt64(dr["订单编号"]));
                    double 粮票支付金额 = 0;
                    double 支付宝支付金额 = 0;
                    double 微信支付金额 = 0;
                    double 其他支付金额 = 0;
                    foreach (var item in payModeList)
                    {
                        switch (item.orderUsedPayMode)
                        {
                            case (int)VAOrderUsedPayMode.BALANCE:
                                粮票支付金额 = (double)item.payAmount;
                                break;
                            case (int)VAOrderUsedPayMode.ALIPAY:
                                支付宝支付金额 = (double)item.payAmount;
                                break;
                            case (int)VAOrderUsedPayMode.WECHAT:
                                微信支付金额 = (double)item.payAmount;
                                break;
                            case (int)VAOrderUsedPayMode.REDENVELOPE:
                            default:
                                其他支付金额 = (double)item.payAmount;
                                break;
                        }
                    }
                    if (支付宝支付金额 + 微信支付金额 <= 0)
                    {
                        if (粮票支付金额 > 0)
                        {
                            if (粮票支付金额 > Common.ToDouble(dr["退款金额"]))
                            {
                                dr["(退)粮票"] = Common.ToDouble(dr["退款金额"]).ToString();
                            }
                            else
                            {
                                dr["(退)粮票"] = 粮票支付金额.ToString();
                                dr["(退)其他"] = Common.ToDouble(Common.ToDouble(dr["退款金额"]) - 粮票支付金额).ToString();
                            }
                        }
                        else
                        {
                            dr["(退)其他"] = Common.ToDouble(dr["退款金额"]).ToString();
                        }
                    }
                    else
                    {
                        if (支付宝支付金额 > 0)
                        {
                            if (Common.ToDouble(dr["退款金额"]) > 支付宝支付金额)
                            {
                                dr["(退)支付宝"] = 支付宝支付金额.ToString();
                                if (粮票支付金额 > 0)
                                {
                                    if (粮票支付金额 + 支付宝支付金额 > Common.ToDouble(dr["退款金额"]))
                                    {
                                        dr["(退)粮票"] = Common.ToDouble(Common.ToDouble(dr["退款金额"]) - 支付宝支付金额).ToString();
                                    }
                                    else
                                    {
                                        dr["(退)粮票"] = 粮票支付金额.ToString();
                                        dr["(退)其他"] = Common.ToDouble(Common.ToDouble(dr["退款金额"]) - 粮票支付金额 - 支付宝支付金额).ToString();
                                    }
                                }
                                else
                                {
                                    dr["(退)其他"] = Common.ToDouble(Common.ToDouble(dr["退款金额"]) - 支付宝支付金额).ToString();
                                }
                            }
                            else
                            {
                                dr["(退)支付宝"] = Common.ToDouble(dr["退款金额"]).ToString();
                            }
                        }
                        if (微信支付金额 > 0)
                        {
                            if (Common.ToDouble(dr["退款金额"]) > 微信支付金额)
                            {
                                dr["(退)微信"] = 微信支付金额.ToString();
                                if (粮票支付金额 > 0)
                                {
                                    if (粮票支付金额 + 微信支付金额 > Common.ToDouble(dr["退款金额"]))
                                    {
                                        dr["(退)粮票"] = Common.ToDouble(Common.ToDouble(dr["退款金额"]) - 微信支付金额).ToString();
                                    }
                                    else
                                    {
                                        dr["(退)粮票"] = 粮票支付金额.ToString();
                                        dr["(退)其他"] = Common.ToDouble(Common.ToDouble(dr["退款金额"]) - 粮票支付金额 - 微信支付金额).ToString();
                                    }
                                }
                                else
                                {
                                    dr["(退)其他"] = Common.ToDouble(Common.ToDouble(dr["退款金额"]) - 微信支付金额).ToString();
                                }
                            }
                            else
                            {
                                dr["(退)微信"] = Common.ToDouble(dr["退款金额"]).ToString();
                            }
                        }
                    }
                }
            }
            ExcelHelper.ExportExcel(dvToDataTable, this, "PreOrderStatement_" + DateTime.Now.ToString("yyyy/mm/dd_hh:mm:ss"));
        }
        else
        {
            CommonPageOperate.AlterMsg(this, "无任何数据");
        }
    }
    /// <summary>
    /// 获取所有上线公司
    /// </summary>
    protected void QueryCompany()
    {
        if (Session["UserInfo"] == null)
        {
            return;
        }
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
        if (DropDownList_Company.Items.Count <= 0)
        {
            return;
        }
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
}