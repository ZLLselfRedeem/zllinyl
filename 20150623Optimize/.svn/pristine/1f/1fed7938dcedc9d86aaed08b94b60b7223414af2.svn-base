using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.WebPageDll;
using VAGastronomistMobileApp.Model;
using System.Data;

public partial class ClientRecharge_ClientRechargeManage : System.Web.UI.Page
{
    ClientRechargeOperate recharge = new ClientRechargeOperate();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindRecharge("", 0, 0, 10);
        }
    }
    private void BindRecharge(string rechargeName, int status, int str, int end)
    {
        DataTable dtRecharge = recharge.QueryRecharge(rechargeName);
        if (dtRecharge.Rows.Count > 0)
        {
            DataView dvRecharge = dtRecharge.DefaultView;
            switch (status)
            {
                case 1:
                    dvRecharge.RowFilter = "status=1";
                    break;
                case -1:
                    dvRecharge.RowFilter = "status=-1";
                    break;
            }
            int cnt = dtRecharge.Rows.Count;
            AspNetPager1.RecordCount = cnt;
            DataTable dtPage = Common.GetPageDataTable(dvRecharge.ToTable(), str, end);
            
            if (dtPage.Rows.Count > 0)
            {
                this.gdvRechageList.DataSource = dtPage;
                this.gdvRechageList.DataBind();

                this.lbRechargeCount.Text = dvRecharge.Count.ToString();
                this.lbActualSoldCount.Text = dtPage.Compute("sum(actualSold)", "1=1").ToString(); 
            }
            else
            {
                this.gdvRechageList.DataSource = null;
                this.gdvRechageList.DataBind();
            }
        }
        else
        {
            this.gdvRechageList.DataSource = null;
            this.gdvRechageList.DataBind();
        }
    }

    protected void gdvRechageList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;//转换为按钮类型，获取其所在的行的索引
        int id = Common.ToInt32(gdvRechageList.DataKeys[index].Values["id"].ToString());
        int status = Common.ToInt32(gdvRechageList.DataKeys[index].Values["status"].ToString());
        if (e.CommandName == "SetStatus")
        {
            if (status == 1)
            {
                status = -1;
            }
            else
            {
                status = 1;
            }
            bool result = recharge.ClientRechargeOnOff(id, status);
            if (result)
            {
                if (!string.IsNullOrEmpty(txbRechageName.Text))
                {
                    BindRecharge(txbRechageName.Text, Common.ToInt32(rblStatus.SelectedValue), AspNetPager1.StartRecordIndex - 1, AspNetPager1.EndRecordIndex);
                }
                else
                {
                    BindRecharge(txbRechageName.Text, Common.ToInt32(rblStatus.SelectedValue), AspNetPager1.StartRecordIndex - 1, AspNetPager1.EndRecordIndex);
                }
            }
        }
    }

    protected void lnkbtnEdit_OnCommand(object sender, CommandEventArgs e)
    {
        int rechargeId = Common.ToInt32(e.CommandArgument);
        ViewState["id"] = rechargeId;

        switch (e.CommandName)
        {
            case "modify":
                BindRechargeDetail(rechargeId);
                this.divList.Attributes.Add("style", "display:none");
                this.divDetail.Attributes.Add("style", "display:''");
                break;
        }
    }
    //保存活动内容
    protected void btnSave_Click(object sender, EventArgs e)
    {
        bool update = false;
        int insert = 0;
        ClientRechargeInfo rechargeInfo = new ClientRechargeInfo();
        rechargeInfo.name = txbName.Text.Trim();
        rechargeInfo.rechargeCondition = Common.ToDouble(txbRechargeCondition.Text.Trim());
        rechargeInfo.present = Common.ToDouble(txbPresent.Text.Trim());
        rechargeInfo.beginTime = txbBeginTime.Text.Trim();
        rechargeInfo.endTime = txbEndTime.Text.Trim();
        rechargeInfo.externalSold = Common.ToInt32(txbExternalSold.Text.Trim());
        rechargeInfo.status = Common.ToInt32(lbStatus.Text.Trim());
        rechargeInfo.createTime = DateTime.Now;
        rechargeInfo.sequence = Common.ToInt32(txbSequence.Text.Trim());

        if (ViewState["id"] != null && ViewState["id"].ToString() != "")//修改
        {
            rechargeInfo.id = Common.ToInt32(ViewState["id"]);
            update = recharge.UpdateRecharge(rechargeInfo);
        }
        else
        {
            rechargeInfo.id = 0;
            insert = recharge.Insert(rechargeInfo);
        }
        if (insert > 0 || update)
        {
            this.divDetail.Attributes.Add("style", "display:none");
            this.divList.Attributes.Add("style", "display:''");
            if (ViewState["id"] != null && ViewState["id"].ToString() != "")//修改
            {
                BindRecharge(txbRechageName.Text.Trim(), Common.ToInt32(rblStatus.SelectedValue), AspNetPager1.StartRecordIndex - 1, AspNetPager1.EndRecordIndex);
            }
            else
            {
                BindRecharge("", Common.ToInt32(rblStatus.SelectedValue), 0, 10);
            }
            Clear();
            EnabledTextBox();
            Page.ClientScript.RegisterStartupScript(GetType(), "success", "<script>alert('保存成功！');</script>");
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "success", "<script>alert('保存失败！')</script>");
        }
    }
    //编辑页面取消按钮
    protected void btnCancle_Click(object sender, EventArgs e)
    {
        this.divDetail.Attributes.Add("style", "display:none");
        this.divList.Attributes.Add("style", "display:''");
        EnabledTextBox();
        Clear();
    }
    //新建充值活动
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        this.divList.Attributes.Add("style", "display:none");
        this.divDetail.Attributes.Add("style", "display:''");
    }
    private void Clear()
    {
        this.lbActualSold.Text = string.Empty;
        this.lbStatus.Text = string.Empty;
        this.lbId.Text = string.Empty;
        this.txbBeginTime.Text = string.Empty;
        this.txbEndTime.Text = string.Empty;
        this.txbExternalSold.Text = string.Empty;
        this.txbName.Text = string.Empty;
        this.txbPresent.Text = string.Empty;
        this.txbRechageName.Text = string.Empty;
        this.txbRechargeCondition.Text = string.Empty;
        this.txbSequence.Text = string.Empty;
        ViewState["id"] = null;
    }
    /// <summary>
    /// 分页操作
    /// </summary>
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        BindRecharge("", Common.ToInt32(rblStatus.SelectedValue), AspNetPager1.StartRecordIndex - 1, AspNetPager1.EndRecordIndex);
    }
    private void BindRechargeDetail(int rechargeId)
    {
        DataTable dt = recharge.QueryRecharge(rechargeId);
        if (dt.Rows.Count > 0)
        {
            this.lbId.Text = dt.Rows[0]["id"].ToString();
            this.txbName.Text = dt.Rows[0]["name"].ToString();
            this.txbRechargeCondition.Text = dt.Rows[0]["RechargeCondition"].ToString();
            this.txbPresent.Text = dt.Rows[0]["present"].ToString();
            this.txbBeginTime.Text = dt.Rows[0]["BeginTime"].ToString();
            this.txbEndTime.Text = dt.Rows[0]["EndTime"].ToString();
            this.txbExternalSold.Text = dt.Rows[0]["ExternalSold"].ToString();
            this.lbActualSold.Text = dt.Rows[0]["ActualSold"].ToString();
            this.txbSequence.Text = dt.Rows[0]["Sequence"].ToString();
            if (dt.Rows[0]["Status"].ToString() == "1")
            {
                this.lbStatus.Text = "已开启";
            }
            else
            {
                this.lbStatus.Text = "已关闭";
            }
            DisabledTextBox();
        }
    }

    private void EnabledTextBox()
    {
        this.txbName.Enabled = true;
        this.txbRechargeCondition.Enabled = true;
        this.txbPresent.Enabled = true;
    }
    private void DisabledTextBox()
    {
        this.txbName.Enabled = false;
        this.txbRechargeCondition.Enabled = false;
        this.txbPresent.Enabled = false;
    }
    protected void gdvRechageList_DataBound(object sender, EventArgs e)
    {
        int status = 0;
        Label labelStatus = null;
        for (int i = 0; i < gdvRechageList.Rows.Count; i++)
        {
            status = Common.ToInt32(gdvRechageList.DataKeys[i].Values["status"].ToString());
            labelStatus = (Label)gdvRechageList.Rows[i].FindControl("Label_status");
            if (status == 1)
            {
                labelStatus.Text = "已开启";
            }
            else
            {
                labelStatus.Text = "已关闭";
            }
        }
    }
    // 开启/关闭 数据切换
    protected void rblStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindRecharge(txbRechageName.Text.Trim(), Common.ToInt32(rblStatus.SelectedValue), AspNetPager1.StartRecordIndex - 1, AspNetPager1.EndRecordIndex);
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        BindRecharge(txbRechageName.Text.Trim(), Common.ToInt32(rblStatus.SelectedValue), 0, 10);
    }
}