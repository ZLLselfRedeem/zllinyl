using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.WebPageDll;
using System.Data;
using VAGastronomistMobileApp.Model;
using System.Transactions;
using Web.Control.DDL;

public partial class CustomerServiceProcessing_batchMoneyApply : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            new CityDropDownList().BindCity(ddlCity);
        }
    }
    /// <summary>
    /// 生成打款记录编号
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btn_create_Click(object sender, EventArgs e)
    {
        if (String.IsNullOrEmpty(txt_amount.Text.Trim()))
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('条件输入不能为空')</script>");
            return;
        }
        double minAmount = Common.ToDouble(txt_amount.Text.Trim());
        if (minAmount <= 0)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('金额必须大于0')</script>");
            return;
        }
        using (TransactionScope scope = new TransactionScope())
        {
            int cityId = Common.ToInt32(ddlCity.SelectedValue);
            BatchMoneyOperate operate = new BatchMoneyOperate();
            DataTable dtBatchMoneyMerchantApply = operate.QueryBatchMoneyMerchantApply(minAmount, cityId);
            if (dtBatchMoneyMerchantApply.Rows.Count > 0)
            {
                double sumObject = Common.ToDouble(dtBatchMoneyMerchantApply.Compute("Sum(remainMoney)", "1=1"));//总打款金额
                int countObject = Common.ToInt32(dtBatchMoneyMerchantApply.Compute("count(shopID)", "1=1"));//总打款记录
                int employeeId = ((VAEmployeeLoginResponse)Session["UserInfo"]).employeeID;
                string employeeName = ((VAEmployeeLoginResponse)Session["UserInfo"]).userName;
                BatchMoneyApply batchMoneyApply = new BatchMoneyApply()
                {
                    advanceAmount = sumObject,
                    createdTime = DateTime.Now,
                    operateEmployee = employeeId,
                    advanceCount = countObject,
                    practicalAmount = 0,
                    practicalCount = 0,
                    remark = employeeName + "帐号登录生成" + ddlCity.SelectedItem.Text + "批量打款记录",
                    status = 1,//有效打款记录
                    cityId = cityId
                };
                int resultInsert = operate.AddBatchMoneyApply(batchMoneyApply);
                if (resultInsert > 0)
                {
                    //int flagCount = 0;
                    DataTable dtTemp = new DataTable();
                    dtTemp.Columns.Add(new DataColumn("batchMoneyApplyId", typeof(Int32)));
                    dtTemp.Columns.Add(new DataColumn("accountId", typeof(Int64)));
                    dtTemp.Columns.Add(new DataColumn("operateEmployee", typeof(Int32)));
                    dtTemp.Columns.Add(new DataColumn("companyId", typeof(Int32)));
                    dtTemp.Columns.Add(new DataColumn("shopId", typeof(Int32)));
                    dtTemp.Columns.Add(new DataColumn("accountNum", typeof(String)));
                    dtTemp.Columns.Add(new DataColumn("bankName", typeof(String)));
                    dtTemp.Columns.Add(new DataColumn("accountName", typeof(String)));
                    dtTemp.Columns.Add(new DataColumn("applyAmount", typeof(Double)));
                    dtTemp.Columns.Add(new DataColumn("serialNumberOrRemark", typeof(String)));
                    dtTemp.Columns.Add(new DataColumn("status", typeof(Int32)));
                    dtTemp.Columns.Add(new DataColumn("haveAdjustAmount", typeof(Double)));
                    dtTemp.Columns.Add(new DataColumn("cityId", typeof(Int32)));
                    //生成批量打款申请明细
                    foreach (DataRow dr in dtBatchMoneyMerchantApply.Rows)
                    {
                        DataRow drTemp = dtTemp.NewRow();
                        drTemp[0] = resultInsert;
                        drTemp[1] = 0;
                        drTemp[2] = 0;
                        drTemp[3] = Common.ToInt32(dr["companyID"]);
                        drTemp[4] = Common.ToInt32(dr["shopID"]);
                        drTemp[5] = Common.ToString(dr["accountNum"]);
                        drTemp[6] = Common.ToString(dr["bankName"]);
                        drTemp[7] = Common.ToString(dr["accountName"]);
                        drTemp[8] = Common.ToDouble(dr["remainMoney"]);
                        drTemp[9] = "";
                        drTemp[10] = 1;
                        drTemp[11] = 0;
                        drTemp[12] = Common.ToInt32(dr["cityId"]);
                        dtTemp.Rows.Add(drTemp);
                    }
                    bool flagResult = operate.BatchAddBatchMoneyApplyDetail(dtTemp);//批量插入数据库
                    if (flagResult)
                    {
                        scope.Complete();
                        Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('生成成功')</script>");
                        recordId.Text = " 记录" + resultInsert.ToString() + " ";
                        totleCount.Text = countObject.ToString();
                        totleAmount.Text = sumObject.ToString();
                        recordId.PostBackUrl = "~/CustomerServiceProcessing/batchMoneyDetail.aspx?batchMoneyApplyId=" + resultInsert;//跳转详情页面
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('批量打款明细添加失败')</script>");
                    }
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('新建批量打款申请失败')</script>");
                }
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('未找到符合当前条件的记录信息')</script>");
            }
        }
    }
}