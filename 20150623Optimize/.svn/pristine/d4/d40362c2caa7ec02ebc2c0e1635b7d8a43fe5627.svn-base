using System;
using System.Data;
using VAGastronomistMobileApp.WebPageDll;
using Web.Control;
using Web.Control.DDL;

public partial class FinancialReporting_orderStatusDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtTimeStart.Text = DateTime.Now.AddDays(-1).ToString("yyyy/MM/dd");
            txtTimeEnd.Text = DateTime.Now.ToString("yyyy/MM/dd");
            new CityDropDownList().BindCity(ddlCity);
            new CompanyDropDownList().BindCompany(ddlCompany, Common.ToInt32(ddlCity.SelectedValue));
            new ShopDropDownList().BindShop(ddlShop, Common.ToInt32(ddlCompany.SelectedValue));
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindGridViewData(0, 10);
    }
    private void BindGridViewData(int str, int end)
    {
        if (string.IsNullOrWhiteSpace(txtTimeStart.Text) || string.IsNullOrWhiteSpace(txtTimeEnd.Text))
        {
            CommonPageOperate.AlterMsg(this, "请选择查询时间");
            return;
        }
        dataPager.Visible = true;
        int cityId = Common.ToInt32(ddlCity.SelectedValue);
        int companyId = Common.ToInt32(ddlCompany.SelectedValue);
        int shopId = Common.ToInt32(ddlShop.SelectedValue);
        string startTime = txtTimeStart.Text + " 00:00:00";
        string endTime = txtTimeEnd.Text + " 23:59:59";
        DataTable dt = new StatisticalStatementOperate().GetOrderStatusDetail(startTime, endTime, cityId, companyId, shopId);
        if (dt.Rows.Count > 0)
        {
            ViewState["excelData"] = dt;
            int tableCount = dt.Rows.Count;
            dataPager.RecordCount = tableCount;
            DataTable dt_page = Common.GetPageDataTable(dt, str, end);
            dt_page = CalculateDatatable(dt_page);
            gvListContent.DataSource = dt_page;
            gvListContent.DataBind();
            return;
        }
        gvListContent.DataSource = dt;
        gvListContent.DataBind();
        CommonPageOperate.AlterMsg(this, "暂无数据");
        dataPager.Visible = false;
    }
    protected DataTable CalculateDatatable(DataTable dtData)
    {
        foreach (DataRow item in dtData.Rows)
        {
            if (Common.ToString(item["ConfrimStatus"]) == "未入座")
            {
                item["ConfrimBalance"] = 0;
                item["ConfrimWechat"] = 0;
                item["ConfrimAli"] = 0;
                item["ConfrimAther"] = 0;
            }
            else
            {
                //以下变量都可能为0
                double OrderBalance = Common.ToDouble(item["OrderBalance"]);//粮票支付
                double OrderWechat = Common.ToDouble(item["OrderWechat"]);//微信支付
                double OrderAli = Common.ToDouble(item["OrderAli"]);//阿里支付
                double OrderOther = Common.ToDouble(item["OrderOther"]);//其他支付（红包支付）
                double RefundAmount = Common.ToDouble(item["RefundAmount"]);//总退款金额
                if (RefundAmount <= 0)//无退款
                {
                    item["ConfrimBalance"] = OrderBalance;
                    item["ConfrimWechat"] = OrderWechat;
                    item["ConfrimAli"] = OrderAli;
                    item["ConfrimAther"] = OrderOther;
                }
                else
                {
                    if (OrderWechat + OrderAli <= 0)//无第三方支付
                    {
                        item["ConfrimWechat"] = OrderWechat;//0
                        item["ConfrimAli"] = OrderAli;//0
                        if (OrderBalance >= RefundAmount)
                        {
                            item["ConfrimBalance"] = OrderBalance - RefundAmount;
                            item["ConfrimAther"] = OrderOther;
                        }
                        else
                        {
                            item["ConfrimBalance"] = 0;
                            item["ConfrimAther"] = OrderOther - (RefundAmount - OrderBalance);
                        }
                    }
                    else//存在第三方支付
                    {
                        if (OrderWechat > 0)
                        {
                            if (OrderWechat > RefundAmount)
                            {
                                item["ConfrimBalance"] = OrderBalance;
                                item["ConfrimWechat"] = OrderWechat - RefundAmount;
                                item["ConfrimAli"] = OrderAli;
                                item["ConfrimAther"] = OrderOther;
                            }
                            else
                            {
                                if (OrderWechat + OrderBalance >= RefundAmount)
                                {
                                    item["ConfrimBalance"] = OrderBalance - (RefundAmount - OrderWechat);
                                    item["ConfrimWechat"] = 0;
                                    item["ConfrimAli"] = OrderAli;//0
                                    item["ConfrimAther"] = OrderOther;
                                }
                                else
                                {
                                    item["ConfrimBalance"] = 0;
                                    item["ConfrimWechat"] = 0;
                                    item["ConfrimAli"] = OrderAli;//0
                                    item["ConfrimAther"] = OrderOther - (RefundAmount - OrderBalance - OrderWechat);
                                }
                            }
                        }
                        if (OrderAli > 0)
                        {
                            if (OrderAli > RefundAmount)
                            {
                                item["ConfrimBalance"] = OrderBalance;
                                item["ConfrimWechat"] = OrderWechat;
                                item["ConfrimAli"] = OrderAli - RefundAmount;
                                item["ConfrimAther"] = OrderOther;
                            }
                            else
                            {
                                if (OrderAli + OrderBalance >= RefundAmount)
                                {
                                    item["ConfrimBalance"] = OrderBalance - (RefundAmount - OrderAli);
                                    item["ConfrimWechat"] = OrderWechat;//0
                                    item["ConfrimAli"] = 0;
                                    item["ConfrimAther"] = OrderOther;
                                }
                                else
                                {
                                    item["ConfrimBalance"] = 0;
                                    item["ConfrimWechat"] = OrderWechat;//0
                                    item["ConfrimAli"] = 0;
                                    item["ConfrimAther"] = OrderOther - (RefundAmount - OrderBalance - OrderAli);
                                }
                            }
                        }
                    }
                }
            }
        }
        return dtData;
    }
    protected void dataPager_PageChanged(object sender, EventArgs e)
    {
        BindGridViewData(dataPager.StartRecordIndex - 1, dataPager.EndRecordIndex);
    }
    /// <summary>
    /// 数据导出excel
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        if (ViewState["excelData"] != null)
        {
            DataTable dtData = ViewState["excelData"] as DataTable;
            dtData = CalculateDatatable(dtData);
            //修改列名称
            dtData.Columns["OrderPhone"].ColumnName = "订单手机";
            dtData.Columns["OrderNumber"].ColumnName = "订单号";
            dtData.Columns["OrderTime"].ColumnName = "订单时间";
            dtData.Columns["OrderShop"].ColumnName = "订单门店";
            dtData.Columns["OrderAmount"].ColumnName = "订单金额";
            dtData.Columns["OrderBalance"].ColumnName = "订单粮票";
            dtData.Columns["OrderWechat"].ColumnName = "订单微信";
            dtData.Columns["OrderAli"].ColumnName = "订单支付宝";
            dtData.Columns["OrderOther"].ColumnName = "订单其他";
            dtData.Columns["ConfrimStatus"].ColumnName = "入座状态";
            dtData.Columns["ConfrimTime"].ColumnName = "入座时间";
            dtData.Columns["ConfrimAmount"].ColumnName = "入座金额";
            dtData.Columns["ConfrimBalance"].ColumnName = "入座粮票";
            dtData.Columns["ConfrimWechat"].ColumnName = "入座微信";
            dtData.Columns["ConfrimAli"].ColumnName = "入座支付宝";
            dtData.Columns["ConfrimAther"].ColumnName = "入座其他";
            dtData.Columns["ApproveStatus"].ColumnName = "结算状态";
            dtData.Columns["ApproveTime"].ColumnName = "结算日期";
            dtData.Columns["ApproveAmount"].ColumnName = "结算金额";
            dtData.Columns["RefundAmount"].ColumnName = "总退款金额";
            if (dtData.Rows.Count > 0)
            {
                ExcelHelper.ExportExcel(dtData, this, "Dingdangxiangqin" + DateTime.Now);
            }
            else
            {
                CommonPageOperate.AlterMsg(this, "未找到任何符合条件的数据");
                return;
            }
        }
        else
        {
            CommonPageOperate.AlterMsg(this, "请先查询需要导出的数据");
            return;
        }
    }
    protected void ddlCity_SelectedIndexChanged(object sender, EventArgs e)
    {
        new CompanyDropDownList().BindCompany(ddlCompany, Common.ToInt32(ddlCity.SelectedValue));
        new ShopDropDownList().BindShop(ddlShop, Common.ToInt32(ddlCompany.SelectedValue));
    }
    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        new ShopDropDownList().BindShop(ddlShop, Common.ToInt32(ddlCompany.SelectedValue));
    }
}