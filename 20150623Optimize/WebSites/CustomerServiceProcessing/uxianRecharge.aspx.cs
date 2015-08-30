using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using VAGastronomistMobileApp.WebPageDll;
using System.Data;
using System.Transactions;
using VAGastronomistMobileApp.Model;
using Web.Control;
using System.Web.UI.HtmlControls;

public partial class CustomerServiceProcessing_uxianRecharge : System.Web.UI.Page
{
    /// <summary>
    /// add by wangc 20140326
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            panel_add.Visible = true;
            panel_list.Visible = false;
        }
    }
    /// <summary>
    /// 添加需要充值的用户
    /// </summary>
    /// <param name="phone"></param>
    /// <returns></returns>
    [WebMethod]
    public static string GetCustomerInfo(string phone)
    {
        CustomerOperate operate = new CustomerOperate();
        DataTable dt = operate.QueryCustomerByMobilephone(phone.Trim());
        if (dt.Rows.Count == 1)
        {
            Model model = new Model()
            {
                customerPhone = Common.ToString(dt.Rows[0]["mobilePhoneNumber"]),
                customerName = Common.ToString(dt.Rows[0]["UserName"])
            };
            return JsonOperate.JsonSerializer<Model>(model);
        }
        else
        {
            return "";
        }
    }
    public class Model
    {
        public string customerPhone { get; set; } //用户手机
        public string customerName { get; set; } //用户昵称
    }
    public class CustomerRechargeViewModel : Model
    {
        public double amount { get; set; }
        public string remark { get; set; }
    }
    /// <summary>
    /// 申请操作
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    [WebMethod]
    public static string RechargeOperate(string data)
    {
        if (String.IsNullOrWhiteSpace(data))
        {
            return "提交数据有误";
        }
        var jsonModelList = JsonOperate.JsonDeserialize<List<CustomerRechargeViewModel>>(data);
        if (jsonModelList == null)
        {
            return "提交数据有误";
        }
        int employeeID = SessionHelper.GetCurrectSessionEmployeeId();
        DataTable dtTemp = new DataTable();
        dtTemp.Columns.Add(new DataColumn("employeeId", typeof(Int32)));
        dtTemp.Columns.Add(new DataColumn("operateTime", typeof(DateTime)));
        dtTemp.Columns.Add(new DataColumn("remark", typeof(String)));
        dtTemp.Columns.Add(new DataColumn("customerPhone", typeof(String)));
        dtTemp.Columns.Add(new DataColumn("amount", typeof(Double)));
        dtTemp.Columns.Add(new DataColumn("cookie", typeof(String)));
        dtTemp.Columns.Add(new DataColumn("status", typeof(Int16)));
        foreach (var item in jsonModelList)
        {
            DataRow drTemp = dtTemp.NewRow();
            drTemp[0] = employeeID;
            drTemp[1] = DateTime.Now;
            drTemp[2] = item.remark;
            drTemp[3] = item.customerPhone;
            drTemp[4] = item.amount;
            drTemp[5] = "";
            drTemp[6] = (int)RechargeFlowStatus.审批申请;
            dtTemp.Rows.Add(drTemp);
        }
        using (TransactionScope scope = new TransactionScope())
        {
            if (new RechargeLogOperate().BatchInsertRechargeLog(dtTemp) == true)
            {
                scope.Complete();
                return "充值申请提交成功";
            }
            else
            {
                return "充值申请提交失败";
            }
        }
    }
    /// <summary>
    /// 分页
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        BindGridViewInfo(AspNetPager1.StartRecordIndex - 1, AspNetPager1.EndRecordIndex);
    }
    /// <summary>
    /// 绑定显示客服信息列表
    /// </summary>
    protected void BindGridViewInfo(int str, int end)
    {
        panel_add.Visible = false;
        panel_list.Visible = true;
        string strTime = "";
        string endTime = "";
        if (TextBox_startTime.Text.Trim() != "" && TextBox_endTime.Text.Trim() != "")
        {
            strTime = Common.ToDateTime(TextBox_startTime.Text.Trim()).ToString("yyyy/mm/dd") + " 00:00:00";//查询开始时间
            endTime = Common.ToDateTime(TextBox_endTime.Text.Trim()).ToString("yyyy/mm/dd") + " 23:59:59";//查询结束时间
        }
        RechargeLogOperate logOper = new RechargeLogOperate();
        DataTable dtOperateLog = logOper.QueryRechargeLog(strTime, endTime, 0);
        if (dtOperateLog.Rows.Count > 0)
        {
            AspNetPager1.Visible = true;
            AspNetPager1.RecordCount = dtOperateLog.Rows.Count;
            DataTable dt_page = Common.GetPageDataTable(dtOperateLog, str, end);
            GridView_List.DataSource = dt_page;
        }
        else
        {
            GridView_List.DataSource = dtOperateLog;
            AspNetPager1.Visible = false;
        }
        GridView_List.DataBind();
    }
    /// <summary>
    /// 切换radiobutton
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void rb_query_CheckedChanged(object sender, EventArgs e)
    {
        if (rb_add.Checked)
        {
            panel_add.Visible = true;
            panel_list.Visible = false;
        }
        else
        {
            panel_add.Visible = false;
            panel_list.Visible = true;
            BindGridViewInfo(0, 10);
        }
    }
    /// <summary>
    /// 查询
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btn_Search_Click(object sender, EventArgs e)
    {
        BindGridViewInfo(0, 10);
    }

    /// <summary>
    /// 导入数据
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnExportData_Click(object sender, EventArgs e)
    {
        try
        {
            string errorNumber = "";
            ImportResult excelDate = ExcelHelper.ImportExcel(fileUploadPhone);
            DataTable dt = excelDate.dtPhone;
            if (dt != null && dt.Rows.Count > 0)
            {
                string phones = CommonPageOperate.SplicingAlphabeticStr(dt, "手机号码", true);
                List<string[]> list = new CustomerOperate().QueryCustomerNameAndPhoneByMobilephone(phones);
                List<string> list1 = (from q in list select q[1]).ToList();
                dt.Columns.Add("昵称", Type.GetType("System.String"));
                dt.Columns.Add("状态", Type.GetType("System.Int32"));
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (!list1.Contains(dt.Rows[i]["手机号码"].ToString()))
                    {
                        errorNumber += dt.Rows[i]["手机号码"].ToString() + ",";
                        dt.Rows[i]["状态"] = -1;
                        //dt.Rows.Remove(dt.Rows[i]);//会导致循环次数减少
                    }
                    else
                    {
                        dt.Rows[i]["状态"] = 1;
                        dt.Rows[i]["昵称"] = (from q in list
                                            where q[1].ToString() == dt.Rows[i]["手机号码"].ToString()
                                            select q[0]).ToList()[0].ToString();
                    }
                }

                DataView dtNew = dt.DefaultView;
                dtNew.RowFilter = "状态=1";
                dt = dtNew.ToTable();

                string strHtml = string.Empty;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    HtmlTableRow r1 = new HtmlTableRow();
                    HtmlTableCell c1 = new HtmlTableCell();
                    HtmlAnchor a = new HtmlAnchor();
                    a.InnerHtml = String.Format("{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td>", (i + 1).ToString(), Common.ToString(dt.Rows[i]["昵称"]), Common.ToString(dt.Rows[i]["手机号码"]), Common.ToString(dt.Rows[i]["金额"]), Common.ToString(dt.Rows[i]["备注"]));
                    c1.Controls.Add(a);
                    r1.Cells.Add(c1);
                    this.rechergeObject.Rows.Add(r1);
                }
                if (!String.IsNullOrWhiteSpace(errorNumber.TrimEnd(',')))
                {
                    CommonPageOperate.AlterMsg(this, errorNumber + "手机号码导入失败");
                }
                lbTotalAmount.Text = dt.Compute("sum(金额)", "1=1").ToString();
            }
        }
        catch
        {
            CommonPageOperate.AlterMsg(this, "excel数据有误");
        }
    }
}