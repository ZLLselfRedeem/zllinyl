using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.WebPageDll;
using Web.Control;
using Web.Control.Enum;

public partial class CustomerServiceProcessing_uxianRechargeCheck : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtStartTime.Text = DateTime.Now.AddDays(-1).ToString("yyyy/MM/dd");
            txtEndTime.Text = DateTime.Now.ToString("yyyy/MM/dd");
            txtStrTime1.Text = DateTime.Now.AddDays(-1).ToString("yyyy/MM/dd");
            txtEndTime1.Text = DateTime.Now.ToString("yyyy/MM/dd");
            panelList.Visible = false;
            panelOperate.Visible = true;
            BindDDL();
            divRemark.Visible = false;
        }
    }

    private void DataBind()
    {
        string strTime = txtStartTime.Text + " 00:00:00";
        string endTime = txtEndTime.Text + " 23:59:59";
        RechargeLogOperate operate = new RechargeLogOperate();
        DataTable data = operate.QueryRechargeLog(strTime, endTime, (int)RechargeFlowStatus.审批申请);
        grRechargeApply.DataSource = data;
        grRechargeApply.DataBind();
        divRemark.Visible = true;
    }
    protected void btn_Search_Click(object sender, EventArgs e)
    {
        DataBind();
    }

    protected void chbCheckAll_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox cb = (CheckBox)sender;
        if (cb.Text == "(全选)")
        {
            foreach (GridViewRow gv in this.grRechargeApply.Rows)
            {
                CheckBox cd = (CheckBox)gv.FindControl("cbSelect");
                cd.Checked = cb.Checked;
                cb.Text = "(取消)";
            }
        }
        else
        {
            cb.Text = "(取消)";
            foreach (GridViewRow gv in this.grRechargeApply.Rows)
            {
                CheckBox cd = (CheckBox)gv.FindControl("cbSelect");
                cd.Checked = cb.Checked;
                cb.Text = "(全选)";
            }
        }
        BindRemark();
    }

    protected void cbSelect_CheckedChanged(object sender, EventArgs e)
    {
        BindRemark();
    }

    private void BindRemark()
    {
        int count = 0;
        double amount = 0;
        for (int i = 0; i < grRechargeApply.Rows.Count; i++)
        {
            CheckBox cb = (CheckBox)grRechargeApply.Rows[i].FindControl("cbSelect");
            if (cb.Checked == true)
            {
                count++;
                amount += Common.ToDouble(grRechargeApply.DataKeys[i].Values["amount"]);
            }
        }
        lbCount.Text = count.ToString();
        lbAmount.Text = amount.ToString();
    }

    protected string GetRechargeLogIds()
    {
        string ids = "";
        for (int i = 0; i < grRechargeApply.Rows.Count; i++)
        {
            CheckBox cb = (CheckBox)grRechargeApply.Rows[i].FindControl("cbSelect");
            if (cb.Checked == true)
            {
                ids += Common.ToInt32(grRechargeApply.DataKeys[i].Values["id"]) + ",";
            }
        }
        return ids.TrimEnd(','); ;
    }
    /// <summary>
    /// 同意审批操作
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAgree_Click(object sender, EventArgs e)
    {
        DoApplyOperate((int)RechargeFlowStatus.审批通过);
    }
    /// <summary>
    /// 拒绝审批操作
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnRefuse_Click(object sender, EventArgs e)
    {
        DoApplyOperate((int)RechargeFlowStatus.审批不通过);
    }

    protected void DoApplyOperate(int status)
    {
        string ids = GetRechargeLogIds();
        if (String.IsNullOrWhiteSpace(ids))
        {
            CommonPageOperate.AlterMsg(this, "请选择需要操作的数据");
            return;
        }
        using (TransactionScope ts = new TransactionScope())
        {
            RechargeLogOperate operate = new RechargeLogOperate();
            if (!operate.ModifyRechargeLogStatus(ids, status, SessionHelper.GetCurrectSessionEmployeeId()))
            {
                CommonPageOperate.AlterMsg(this, "审批操作失败"); return;
            }
            if (status == (int)RechargeFlowStatus.审批通过)
            {
                List<RechargeLogPartMolde> list = operate.QueryRechargeCustomerPhone(ids);

                CustomerOperate customerOper = new CustomerOperate();
                if (customerOper.CustomerRecharge(list))
                {
                    ts.Complete();
                    CommonPageOperate.AlterMsg(this, "审批操作成功");

                }
                else
                {
                    CommonPageOperate.AlterMsg(this, "审批操作失败");
                }
            }
            else
            {
                ts.Complete();
                CommonPageOperate.AlterMsg(this, "审批操作成功");
            }
        }
        DataBind();
    }

    protected void rbOperate_CheckedChanged(object sender, EventArgs e)
    {
        if (rbOperate.Checked)
        {
            panelList.Visible = false;
            panelOperate.Visible = true;
        }
        else
        {
            panelList.Visible = true;
            panelOperate.Visible = false;
        }
    }
    /// <summary>
    /// 查看列表
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button1_Click(object sender, EventArgs e)
    {
        DataBind1(0, 10);
    }
    protected void DataBind1(int str, int end)
    {
        string strTime = txtStrTime1.Text + " 00:00:00";
        string endTime = txtEndTime1.Text + " 23:59:59";
        RechargeLogOperate operate = new RechargeLogOperate();
        DataTable data = operate.QueryRechargeLog(strTime, endTime, Common.ToInt32(ddlStatus.SelectedValue));
        if (data.Rows.Count > 0)
        {
            ViewState["excelData"] = data;
            int tableCount = data.Rows.Count;
            AspNetPager1.RecordCount = tableCount;
            DataTable dt_page = Common.GetPageDataTable(data, str, end);
            grRechargeList.DataSource = dt_page;
            grRechargeList.DataBind();
            return;
        }
        grRechargeList.DataSource = data;
        grRechargeList.DataBind();
    }
    protected void BindDDL()
    {
        ddlStatus.DataSource = EnumHelper.EnumToList(typeof(RechargeFlowStatus));
        ddlStatus.DataTextField = "Text";
        ddlStatus.DataValueField = "Value";
        ddlStatus.DataBind();
        ddlStatus.Items.Add(new ListItem("所有", "0"));
        ddlStatus.SelectedValue = "0";
    }
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        DataBind1(AspNetPager1.StartRecordIndex - 1, AspNetPager1.EndRecordIndex);
    }
    /// <summary>
    /// 数据导出excel
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button2_Click(object sender, EventArgs e)
    {
        DataTable dt = ViewState["excelData"] as DataTable;
        if (dt == null || dt.Rows.Count <= 0)
        {
            CommonPageOperate.AlterMsg(this, "无任何数据");
            return;
        }
        dt.Columns.Remove("employeeId");//移除这一列
        dt.Columns["id"].ColumnName = "编号";//修改列数据
        dt.Columns["amount"].ColumnName = "申请金额";
        dt.Columns["operateTime"].ColumnName = "申请时间";
        dt.Columns["EmployeeFirstName"].ColumnName = "申请人";
        dt.Columns["remark"].ColumnName = "申请备注";
        dt.Columns["customerPhone"].ColumnName = "手机号码";
        dt.Columns["status"].ColumnName = "操作状态";
        dt.Columns["approvalEmployee"].ColumnName = "审核人";
        dt.Columns["approvalTime"].ColumnName = "审核时间";
        ExcelHelper.ExportExcel(dt, this, DateTime.Now.ToString());
    }
}