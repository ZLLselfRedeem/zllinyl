using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using VAGastronomistMobileApp.WebPageDll;
using VAGastronomistMobileApp.Model;
using System.Transactions;
using Web.Control;

public partial class CustomerServiceProcessing_batchMoneyDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindGr(0, 10);
        }
    }
    void BindGr(int str, int end)
    {
        DataTable dt = GetGrData(false);
        if (dt.Rows.Count > 0)
        {
            int tableCount = dt.Rows.Count;
            AspNetPager1.RecordCount = tableCount;
            DataTable dt_page = Common.GetPageDataTable(dt, str, end);
            GridView_batchMoneyDetail.DataSource = dt_page;
        }
        GridView_batchMoneyDetail.DataBind();
        BatchMoneyOperate operate = new BatchMoneyOperate();
        long batchMoneyApplyDetailId = 0;
        for (int i = 0; i < GridView_batchMoneyDetail.Rows.Count; i++)
        {
            batchMoneyApplyDetailId = Common.ToInt32(GridView_batchMoneyDetail.DataKeys[i].Values["batchMoneyApplyDetailId"]);
            TextBox textBox = GridView_batchMoneyDetail.Rows[i].FindControl("txt_serialNumberOrRemark") as TextBox;
            textBox.Text = Common.ToString(GridView_batchMoneyDetail.DataKeys[i].Values["serialNumberOrRemark"].ToString());
            LinkButton linkBtn = GridView_batchMoneyDetail.Rows[i].FindControl("btn_cancle") as LinkButton;
            CheckBox cb = (CheckBox)GridView_batchMoneyDetail.Rows[i].FindControl("cbSelect");
            if (!operate.QueryBatchMoneyApplyDetailStatus(batchMoneyApplyDetailId))
            {
                linkBtn.CssClass = "";
                linkBtn.Enabled = false;
                cb.Enabled = false;
            }
            else
            {
                linkBtn.Attributes.Add("onclick", "return confirm('撤销后列表将不再显示该申请，确认撤销？');");
            }
        }
        lit_totleCount.Text = dt.Compute("count(batchMoneyApplyDetailId)", "1=1").ToString();
        lit_totleAmount.Text = dt.Compute("sum(applyAmount)", "1=1").ToString();
        lit_remitCount.Text = dt.Compute("count(batchMoneyApplyDetailId)", "status=2").ToString();
        lit_remitAmount.Text = dt.Compute("sum(applyAmount)", "status=2").ToString() == "" ? "0" : dt.Compute("sum(applyAmount)", "status=2").ToString();
        lit_adjustCount.Text = dt.Compute("count(batchMoneyApplyDetailId)", "status=2").ToString();
        lit_adjustAmount.Text = dt.Compute("sum(applyAmount)", "status=2").ToString() == "" ? "0" : dt.Compute("sum(applyAmount)", "status=2").ToString();
    }
    /// <summary>
    /// 查询批量
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btn_queryBatch_Click(object sender, EventArgs e)
    {
        hidden.Value = "1";
        BindGr(0, 10);
    }
    /// <summary>
    /// 绑定多个事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GridView_batchMoneyDetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;//转换为按钮类型，获取其所在的行的索引
        long batchMoneyApplyDetailId = Common.ToInt32(GridView_batchMoneyDetail.DataKeys[index].Values["batchMoneyApplyDetailId"]);
        int batchMoneyApplyId = Common.ToInt32(GridView_batchMoneyDetail.DataKeys[index].Values["batchMoneyApplyId"]);
        double applyAmount = Common.ToDouble(GridView_batchMoneyDetail.DataKeys[index].Values["applyAmount"]);//申请调整余额
        string shopName = Common.ToString(GridView_batchMoneyDetail.DataKeys[index].Values["shopName"]);
        int companyId = Common.ToInt32(GridView_batchMoneyDetail.DataKeys[index].Values["companyId"]);
        int shopId = Common.ToInt32(GridView_batchMoneyDetail.DataKeys[index].Values["shopId"]);
        long accountId_init = Common.ToInt64(GridView_batchMoneyDetail.DataKeys[index].Values["accountId"]);
        BatchMoneyOperate operate = new BatchMoneyOperate();
        using (TransactionScope scope = new TransactionScope())
        {
            BatchMoneyApplyDetail model = new BatchMoneyApplyDetail();
            model.batchMoneyApplyDetailId = batchMoneyApplyDetailId;
            int employeeId = ((VAEmployeeLoginResponse)Session["UserInfo"]).employeeID;
            model.operateEmployee = employeeId;//操作人
            model.batchMoneyApplyId = batchMoneyApplyId;
            model.applyAmount = applyAmount;
            TextBox textBox = GridView_batchMoneyDetail.Rows[index].FindControl("txt_serialNumberOrRemark") as TextBox;
            string txt_serialNumberOrRemark = textBox.Text.Trim();
            switch (e.CommandName)
            {
                case "Save":
                    bool flag1 = true;
                    bool flag2 = true;
                    VAEmployeeLoginResponse vAEmployeeLoginResponse = (VAEmployeeLoginResponse)Session["UserInfo"];
                    int employeeID = vAEmployeeLoginResponse.employeeID;
                    if (String.IsNullOrEmpty(txt_serialNumberOrRemark))
                    {
                        Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('请填写流水号或备注信息');</script>");
                        return;
                    }
                    model.serialNumberOrRemark = txt_serialNumberOrRemark;
                    model.status = BatchMoneyStatus.com;//表示已经完成
                    model.haveAdjustAmount = (-1) * applyAmount;//调整余额
                    if (operate.QueryBatchMoneyApplyDetailStatus(batchMoneyApplyDetailId))//第一次处理数据
                    {
                        if (Common.ToDouble(operate.QueryShopRemainMoney(shopId)) < applyAmount)
                        {
                            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('" + shopName + "申请调整的余额大于该门店可调整的余额，请联系财务总监');</script>");
                            textBox.Text = "";//滞空文本框
                            return;
                        }
                        BatchMoneyApply batch_model = new BatchMoneyApply();
                        batch_model.practicalCount = 1;
                        batch_model.practicalAmount = applyAmount;
                        batch_model.batchMoneyApplyId = batchMoneyApplyId;
                        long accountId = 0;
                        flag2 = SybMoneyMerchantOperate.MerchantCheckout(companyId, shopId, applyAmount, employeeID, txt_serialNumberOrRemark, ref accountId);//更新门店账户余额
                        model.accountId = accountId;
                        flag1 = operate.ModifyBatchMoneyApply(batch_model);
                    }
                    else//第二次处理数据
                    {
                        CompanyApplyPaymentOperate applyOperate = new CompanyApplyPaymentOperate();
                        flag1 = applyOperate.ModifyRemark(accountId_init, txt_serialNumberOrRemark);
                    }
                    if (operate.ModifySaveBatchMoneyApplyDetail(model) && flag1 && flag2)
                    {
                        BindGr(AspNetPager1.StartRecordIndex - 1, AspNetPager1.EndRecordIndex);
                        scope.Complete();
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('" + shopName + "的" + applyAmount.ToString() + "元打款记录失败');</script>");
                    }
                    break;
                case "Cancle":
                    model.status = BatchMoneyStatus.delete;
                    if (operate.QueryBatchMoneyApplyDetailStatus(batchMoneyApplyDetailId))
                    {
                        model.serialNumberOrRemark = txt_serialNumberOrRemark + ((VAEmployeeLoginResponse)Session["UserInfo"]).userName + "撤销";
                        if (operate.ModifyCancleBatchMoneyApplyDetail(model))
                        {
                            BindGr(AspNetPager1.StartRecordIndex - 1, AspNetPager1.EndRecordIndex);
                            scope.Complete();
                        }
                        else
                        {
                            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('撤销失败');</script>");
                        }
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('" + shopName + "的" + applyAmount.ToString() + "元打款已经记录，无法撤销，请联系财务主管。');</script>");
                    }
                    break;
                default:
                    //不做任何操作
                    break;
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
        BindGr(AspNetPager1.StartRecordIndex - 1, AspNetPager1.EndRecordIndex);
    }
    /// <summary>
    /// 根据流水号查询单笔打款记录
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btn_querySingle_Click(object sender, EventArgs e)
    {
        hidden.Value = "2";
        BindGr(0, 10);
    }
    /// <summary>
    /// 获取绑定列表的数据源
    /// </summary>
    /// <returns></returns>
    DataTable GetGrData(bool doExcel)
    {
        int typeFlag = Common.ToInt32(hidden.Value);
        int batchMoneyApplyId = 0;
        if (Common.ToInt32(txt_batchMoneyApplyId.Text.Trim()) > 0)
        {
            batchMoneyApplyId = Common.ToInt32(txt_batchMoneyApplyId.Text.Trim());
        }
        else if (Common.ToInt32(Request.QueryString["batchMoneyApplyId"]) > 0)
        {
            batchMoneyApplyId = Common.ToInt32(Request.QueryString["batchMoneyApplyId"]);
        }
        BatchMoneyOperate operate = new BatchMoneyOperate();
        DataTable dt = new DataTable();
        if (typeFlag == 1)
        {
            dt = operate.QueryBatchMoneyApplyDetailByBatchMoneyApplyId(batchMoneyApplyId, doExcel);
        }
        else
        {
            dt = operate.QueryBatchMoneyApplyDetailBySerialNumberOrRemark(txt_number.Text.Trim(), doExcel);
        }
        return dt;
    }
    /// <summary>
    /// 数据导出excel
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btn_exportExcel_Click(object sender, EventArgs e)
    {
        // ExcelHelper.ExportExcel(GetGrData(true), this, HttpUtility.UrlEncode("批量打款_" + DateTime.Now.ToString("yyyy/mm/dd_hh:mm:ss"), System.Text.Encoding.UTF8).ToString());
        CreateExcel(GetGrData(true));
    }
    protected void btn_batchCancle_Click(object sender, EventArgs e)
    {
        int flagCount = 0;
        for (int i = 0; i < GridView_batchMoneyDetail.Rows.Count; i++)
        {
            CheckBox cb = (CheckBox)GridView_batchMoneyDetail.Rows[i].FindControl("cbSelect");
            if (cb.Checked == true)
            {
                flagCount++;
            }
        }
        if (flagCount > 0)
        {
            BatchMoneyOperate operate = new BatchMoneyOperate();
            int employeeId = ((VAEmployeeLoginResponse)Session["UserInfo"]).employeeID;
            string operStr = ((VAEmployeeLoginResponse)Session["UserInfo"]).userName + "批量撤销";
            using (TransactionScope ts = new TransactionScope())
            {
                int count = 0;
                for (int i = 0; i < GridView_batchMoneyDetail.Rows.Count; i++)
                {
                    CheckBox cb = (CheckBox)GridView_batchMoneyDetail.Rows[i].FindControl("cbSelect");
                    if (cb.Checked == true)
                    {
                        BatchMoneyApplyDetail model = new BatchMoneyApplyDetail();
                        model.batchMoneyApplyDetailId = Common.ToInt64(GridView_batchMoneyDetail.DataKeys[i].Values["batchMoneyApplyDetailId"]);
                        model.operateEmployee = employeeId;//操作人
                        model.batchMoneyApplyId = Common.ToInt32(GridView_batchMoneyDetail.DataKeys[i].Values["batchMoneyApplyId"]);
                        model.applyAmount = Common.ToDouble(GridView_batchMoneyDetail.DataKeys[i].Values["applyAmount"]); ;
                        model.status = BatchMoneyStatus.delete;
                        model.serialNumberOrRemark = operStr;
                        if (operate.ModifyCancleBatchMoneyApplyDetail(model))
                        {
                            count++;
                        }
                    }
                }
                if (count == flagCount)
                {
                    BindGr(AspNetPager1.StartRecordIndex - 1, AspNetPager1.EndRecordIndex);
                    ts.Complete();
                }
                else
                {
                    CommonPageOperate.AlterMsg(this, "批量撤销失败");
                }
            }
        }
        else
        {
            CommonPageOperate.AlterMsg(this, "请先选择批量撤销项");
        }
    }


    void CreateExcel(DataTable dt)
    {
        string excelName = HttpUtility.UrlEncode("批量打款_" + DateTime.Now.ToString("yyyy/mm/dd_hh:mm:ss"), System.Text.Encoding.UTF8).ToString();
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
                else if (i == 4)
                {
                    ls_item += "'" + row[i].ToString().Trim() + "\t";
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