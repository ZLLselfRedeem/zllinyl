using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.WebPageDll;
using VAGastronomistMobileApp.Model;
using System.Data;

public partial class WeChatPlatManage_uxianCooperation : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            initData();
            GetCooperationinfo(0, 5);

        }
        if (Label_message.Text != "" || lbl_ID.Text == "")
            tbUpdate.Attributes.CssStyle.Add("display", "none");

        tbNew.Attributes.CssStyle.Add("display", "block");
    }

    private void initData()
    {
        WechatUxianCooperationOperate uco = new WechatUxianCooperationOperate();
        DataTable dt = uco.GetCooperationInfo();
        GridView_Info.DataSource = dt;
        GridView_Info.DataBind();
        if (dt.Rows.Count == 0)
            Label_message.Text = "暂无数据";
        else
            Label_message.Text = "";

    }

    private void GetCooperationinfo(int str, int end)
    {
        WechatUxianCooperationOperate uco = new WechatUxianCooperationOperate();
        //加载商户合作信息
        DataTable dtMerchantInfo = uco.GetMerchantSendInfo();
        AspNetPager1.RecordCount = dtMerchantInfo.Rows.Count;
        DataTable dt_page = Common.GetPageDataTable(dtMerchantInfo, str, end);//分页的DataTable

        GridView_CoopInfo.DataSource = dt_page;
        GridView_CoopInfo.DataBind();
        if (dtMerchantInfo.Rows.Count == 0)
            Label_MerchantNodata.Text = "暂无合作信息";
        else
            Label_MerchantNodata.Text = "";
    }
    //新增
    protected void Button_Save_Click(object sender, EventArgs e)
    {
        ClearInfo();

        if (!string.IsNullOrEmpty(txtContentNew.Text))
        {
            WechatUxianCooperationOperate uco = new WechatUxianCooperationOperate();
            WechatUxianCooperationInfo uxianCooperationInfo = new WechatUxianCooperationInfo();
            uxianCooperationInfo.msgContent = txtContentNew.Text;
            uxianCooperationInfo.operaterID = ((VAEmployeeLoginResponse)Session["UserInfo"]).employeeID;
            uxianCooperationInfo.pubDateTime = DateTime.Now.ToString("yyyyMMdd HH:mm:ss");
            if (uco.Insert(uxianCooperationInfo) > 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "js", "alert('新增成功!');", true);
                txtContentNew.Text = "";
                initData();
            }
            else
                ScriptManager.RegisterStartupScript(this, GetType(), "js", "alert('新增失败!');", true);
        }
    }

    private void ClearInfo()
    {
        lbl_ID.Text = "";
        txtContentUpdate.Text = "";
        tbUpdate.Attributes.CssStyle.Add("display", "none");
    }
    //更新
    protected void Button_Update_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(txtContentUpdate.Text))
        {
            WechatUxianCooperationOperate uco = new WechatUxianCooperationOperate();
            WechatUxianCooperationInfo uxianCooperationInfo = new WechatUxianCooperationInfo();
            uxianCooperationInfo.ID = Common.ToInt32(lbl_ID.Text);
            uxianCooperationInfo.msgContent = txtContentUpdate.Text;
            uxianCooperationInfo.pubDateTime = DateTime.Now.ToString("yyyyMMdd HH:mm:ss");
            uxianCooperationInfo.operaterID = ((VAEmployeeLoginResponse)Session["UserInfo"]).employeeID;
            if (uco.Update(uxianCooperationInfo) > 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "js", "alert('修改成功!');", true);
                initData();
            }
            else
                ScriptManager.RegisterStartupScript(this, GetType(), "js", "alert('修改失败!');", true);
        }
        ClearInfo();
    }
    //修改
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
        WechatUxianCooperationOperate uco = new WechatUxianCooperationOperate();
        if (uco.Delete(id))
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "js", "alert('删除成功!');", true);
            initData();
        }
        else
            ScriptManager.RegisterStartupScript(this, GetType(), "js", "alert('删除失败!');", true);

    }

    protected void GridView_CoopInfo_DataBound(object sender, EventArgs e)
    {
        for (int i = 0; i < GridView_CoopInfo.Rows.Count; i++)
        {
            CheckBox chk = GridView_CoopInfo.Rows[i].Cells[6].FindControl("chkChecked") as CheckBox;
            if ((GridView_CoopInfo.Rows[i].Cells[6].FindControl("lblStatus") as Label).Text == "0")
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
            WechatUxianCooperationOperate wco = new WechatUxianCooperationOperate();
            WechatMerchantSendInfo merchantSendInfo = new WechatMerchantSendInfo();
            merchantSendInfo.ID = Common.ToInt32(gvr.Cells[0].Text);
            merchantSendInfo.operaterID = ((VAEmployeeLoginResponse)Session["UserInfo"]).employeeID;
            merchantSendInfo.operateDateTime = DateTime.Now.ToString();
            merchantSendInfo.status = "1";

            wco.UpdateMerchantSendInf(merchantSendInfo);
        }
    }
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        GetCooperationinfo(AspNetPager1.StartRecordIndex - 1, AspNetPager1.EndRecordIndex);
    }
}