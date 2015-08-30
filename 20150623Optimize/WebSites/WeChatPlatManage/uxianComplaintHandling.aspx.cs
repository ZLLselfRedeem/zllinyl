using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.WebPageDll;
using VAGastronomistMobileApp.Model;
using System.Data;

public partial class WeChatPlatManage_uxianComplaintHandling : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            initData();

            GetComplaintInfo(0, 5);
        }
        if (Label_message.Text != "" || lbl_ID.Text == "")
            tbUpdate.Attributes.CssStyle.Add("display", "none");

        tbNew.Attributes.CssStyle.Add("display", "block");
    }

    private void initData()
    {
        WechatUxianComplaintOperate uco = new WechatUxianComplaintOperate();
        DataTable dt = uco.GetUxianComplaintInfo();
        GridView_Info.DataSource = dt;
        GridView_Info.DataBind();
        if (dt.Rows.Count == 0)
            Label_message.Text = "暂无数据";
        else
            Label_message.Text = "";
    }

    //获取投诉信息
    private void GetComplaintInfo(int str, int end)
    {
        WechatUxianComplaintOperate uco = new WechatUxianComplaintOperate();
        DataTable dt = uco.GetComplaintReceiveInfo();
        AspNetPager1.RecordCount = dt.Rows.Count;
        DataTable dt_page = Common.GetPageDataTable(dt, str, end);
        GridView_Handling.DataSource = dt_page;
        GridView_Handling.DataBind();
        if (dt.Rows.Count == 0)
            Label_page.Text = "暂无投诉信息";
        else
            Label_page.Text = "";
    }

    private void ClearInfo()
    {
        lbl_ID.Text = "";
        txtContentUpdate.Text = "";
        tbUpdate.Attributes.CssStyle.Add("display", "none");
    }

    protected void Button_Save_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(txtContentNew.Text))
        {
            WechatUxianComplaintOperate uco = new WechatUxianComplaintOperate();
            WechatUxianComplaintInfo uxianComplaintInfo = new WechatUxianComplaintInfo();
            uxianComplaintInfo.msgContent = txtContentNew.Text;
            uxianComplaintInfo.operaterID = ((VAEmployeeLoginResponse)Session["UserInfo"]).employeeID;
            uxianComplaintInfo.pubDateTime = DateTime.Now.ToString("yyyyMMdd HH:mm:ss");

            if (uco.Insert(uxianComplaintInfo) > 0)
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
            WechatUxianComplaintOperate uco = new WechatUxianComplaintOperate();
            WechatUxianComplaintInfo uxianComplaintInfo = new WechatUxianComplaintInfo();
            uxianComplaintInfo.msgContent = txtContentUpdate.Text;
            uxianComplaintInfo.operaterID = ((VAEmployeeLoginResponse)Session["UserInfo"]).employeeID;
            uxianComplaintInfo.pubDateTime = DateTime.Now.ToString("yyyyMMdd HH:mm:ss");
            uxianComplaintInfo.ID = Common.ToInt32(lbl_ID.Text);
            if (uco.Update(uxianComplaintInfo) > 0)
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
        WechatUxianComplaintOperate uco = new WechatUxianComplaintOperate();
        if (uco.Delete(id))
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "js", "alert('删除成功!');", true);
            initData();
        }
        else
            ScriptManager.RegisterStartupScript(this, GetType(), "js", "alert('删除失败!');", true);
    }

    protected void GridView_Handling_DataBound(object sender, EventArgs e)
    {
        for (int i = 0; i < GridView_Handling.Rows.Count; i++)
        {
            CheckBox chk = GridView_Handling.Rows[i].Cells[7].FindControl("chkChecked") as CheckBox;
            if ((GridView_Handling.Rows[i].Cells[7].FindControl("lblStatus") as Label).Text == "0")
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
            WechatUxianComplaintOperate wco = new WechatUxianComplaintOperate();
            WechatComplaintReceiveInfo complaintReceiveInfo = new WechatComplaintReceiveInfo();
            complaintReceiveInfo.ID = Common.ToInt32(gvr.Cells[0].Text);
            complaintReceiveInfo.operaterID = ((VAEmployeeLoginResponse)Session["UserInfo"]).employeeID;
            complaintReceiveInfo.operateDateTime = DateTime.Now.ToString();
            complaintReceiveInfo.status = "1";
            wco.UpdateComplaintReceiveInfo(complaintReceiveInfo);
        }
    }
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        GetComplaintInfo(AspNetPager1.StartRecordIndex - 1, AspNetPager1.EndRecordIndex);
    }
}