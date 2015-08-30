using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.WebPageDll;
using VAGastronomistMobileApp.Model;
using System.Data;

public partial class WeChatPlatManage_uxianProposal : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            initData();
            getProposalReceiveInfo(0, 5);
        }
        if (Label_message.Text != "" || lbl_ID.Text == "")
            tbUpdate.Attributes.CssStyle.Add("display", "none");

        tbNew.Attributes.CssStyle.Add("display", "block");
    }

    private void initData()
    {
        WechatUxianProposalOperate upo = new WechatUxianProposalOperate();
        DataTable dt = upo.GetUxianProposalInfo();
        GridView_Info.DataSource = dt;
        GridView_Info.DataBind();
        if (dt.Rows.Count == 0)
            Label_message.Text = "暂无数据";
        else
            Label_message.Text = "";

    }

    //获取微信平台用户发送的意见建议
    private void getProposalReceiveInfo(int str, int end)
    {
        WechatUxianProposalOperate upo = new WechatUxianProposalOperate();
        DataTable dt = upo.GetProposalReceiveInfo();
        AspNetPager1.RecordCount = dt.Rows.Count;
        DataTable dt_page = Common.GetPageDataTable(dt, str, end);
        GridView_ProposalInfo.DataSource = dt_page;
        GridView_ProposalInfo.DataBind();
        if (dt.Rows.Count == 0)
            Label_page.Text = "暂无意见建议";
        else
            Label_page.Text = "";
    }

    private void ClearInfo()
    {
        lbl_ID.Text = "";
        txtContentUpdate.Text = "";
        tbUpdate.Attributes.CssStyle.Add("display", "none");
    }

    //新增
    protected void Button_Save_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(txtContentNew.Text))
        {
            WechatUxianProposalOperate upo = new WechatUxianProposalOperate();
            WechatUxianProposalInfo uxianProposalInfo = new WechatUxianProposalInfo();
            uxianProposalInfo.msgContent = txtContentNew.Text;
            uxianProposalInfo.operaterID = ((VAEmployeeLoginResponse)Session["UserInfo"]).employeeID;
            uxianProposalInfo.pubDateTime = DateTime.Now.ToString("yyyyMMdd HH:mm:ss");
            if (upo.Insert(uxianProposalInfo) > 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "js", "alert('新增成功!');", true);
                txtContentNew.Text = "";
                initData();
            }
            else
                ScriptManager.RegisterStartupScript(this, GetType(), "js", "alert('新增失败!');", true);
        }
        ClearInfo();
    }

    protected void Button_Update_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(txtContentUpdate.Text))
        {
            WechatUxianProposalOperate upo = new WechatUxianProposalOperate();
            WechatUxianProposalInfo uxianProposalInfo = new WechatUxianProposalInfo();
            uxianProposalInfo.msgContent = txtContentUpdate.Text;
            uxianProposalInfo.operaterID = ((VAEmployeeLoginResponse)Session["UserInfo"]).employeeID;
            uxianProposalInfo.pubDateTime = DateTime.Now.ToString("yyyyMMdd HH:mm:ss");
            uxianProposalInfo.ID = Common.ToInt32(lbl_ID.Text);
            if (upo.Update(uxianProposalInfo) > 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "js", "alert('修改成功!');", true);
                initData();
            }
            else
                ScriptManager.RegisterStartupScript(this, GetType(), "js", "alert('修改失败!');", true);
        }
        ClearInfo();
    }

    protected void lbtnUpdate_Click(object sender, EventArgs e)
    {
        tbUpdate.Attributes.CssStyle.Add("display", "block");
        tbNew.Attributes.CssStyle.Add("display", "none");

        GridViewRow gvr = (GridViewRow)(sender as LinkButton).Parent.Parent;
        lbl_ID.Text = gvr.Cells[0].Text;
        txtContentUpdate.Text = gvr.Cells[1].Text;
    }

    protected void GridView_Info_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int id = Common.ToInt32(GridView_Info.Rows[e.RowIndex].Cells[0].Text);
        WechatUxianProposalOperate upo = new WechatUxianProposalOperate();
        if (upo.Delete(id))
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "js", "alert('删除成功!');", true);
            initData();
        }
        else
            ScriptManager.RegisterStartupScript(this, GetType(), "js", "alert('删除失败!');", true);
    }

    protected void GridView_ProposalInfo_DataBound(object sender, EventArgs e)
    {
        for (int i = 0; i < GridView_ProposalInfo.Rows.Count; i++)
        {
            CheckBox chk = GridView_ProposalInfo.Rows[i].Cells[7].FindControl("chkChecked") as CheckBox;
            if ((GridView_ProposalInfo.Rows[i].Cells[7].FindControl("lblStatus") as Label).Text == "0")
                chk.Checked = false;
            else
            {
                chk.Checked = true;
                chk.Enabled = false;
            }
        }
    }

    protected void chkChecked_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chk = (CheckBox)sender;
        GridViewRow gvr = chk.Parent.Parent as GridViewRow;
        if (chk.Checked)
        {
            WechatUxianProposalOperate wpo = new WechatUxianProposalOperate();
            WechatProposalReceiveInfo proposalReceiveInfo = new WechatProposalReceiveInfo();
            proposalReceiveInfo.ID = Common.ToInt32(gvr.Cells[0].Text);
            proposalReceiveInfo.operaterID = ((VAEmployeeLoginResponse)Session["UserInfo"]).employeeID;
            proposalReceiveInfo.operateDateTime = DateTime.Now.ToString();
            proposalReceiveInfo.status = "1";

            wpo.UpdateProposalReceiveInfo(proposalReceiveInfo);
        }
    }
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        getProposalReceiveInfo(AspNetPager1.StartRecordIndex - 1, AspNetPager1.EndRecordIndex);
    }
}