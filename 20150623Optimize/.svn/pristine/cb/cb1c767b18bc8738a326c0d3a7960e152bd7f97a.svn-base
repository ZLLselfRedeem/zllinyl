using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.WebPageDll;
using VAGastronomistMobileApp.Model;
using System.Data;

public partial class WeChatPlatManage_uxianQandA : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            initData(0, 5);
        }
        if (Label_message.Text != "" || lbl_ID.Text == "")
            tbUpdate.Attributes.CssStyle.Add("display", "none");

        tbNew.Attributes.CssStyle.Add("display", "block");
    }

    private void initData(int str, int end)
    {
        WechatUxianQandAOperate uqa = new WechatUxianQandAOperate();
        DataTable dt = uqa.GetUxianQandAInfo();
        AspNetPager1.RecordCount = dt.Rows.Count;
        DataTable dt_page = Common.GetPageDataTable(dt, str, end);//分页的DataTable
        GridView_QandA.DataSource = dt_page;
        GridView_QandA.DataBind();
        if (dt.Rows.Count == 0)
            Label_message.Text = "暂无数据";
        else
            Label_message.Text = "";
    }

    protected void Button_Save_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(txtQuestionNew.Text) && !string.IsNullOrEmpty(txtAnswerNew.Text))
        {
            WechatUxianQandAOperate uqa = new WechatUxianQandAOperate();
            WechatUxianQandAInfo uxianQandAInfo = new WechatUxianQandAInfo();
            uxianQandAInfo.question = txtQuestionNew.Text;
            uxianQandAInfo.answer = txtAnswerNew.Text;
            uxianQandAInfo.pubDateTime = DateTime.Now.ToString("yyyyMMdd HH:mm:ss");
            uxianQandAInfo.operaterID = ((VAEmployeeLoginResponse)Session["UserInfo"]).employeeID;
            if (uqa.Insert(uxianQandAInfo) > 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "js", "alert('新增成功!');", true);
                txtQuestionNew.Text = "";
                txtAnswerNew.Text = "";
                initData(0, 5);
            }
            else
                ScriptManager.RegisterStartupScript(this, GetType(), "js", "alert('新增失败!');", true);
        }

        ClearInfo();
    }

    private void ClearInfo()
    {
        lbl_ID.Text = "";
        txtQuestionUpdate.Text = "";
        txtAnswerUpdate.Text = "";
        tbUpdate.Attributes.CssStyle.Add("display", "none");
    }

    protected void Button_Update_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(txtQuestionUpdate.Text) && !string.IsNullOrEmpty(txtAnswerUpdate.Text))
        {
            WechatUxianQandAOperate uqa = new WechatUxianQandAOperate();
            WechatUxianQandAInfo uxianQandAInfo = new WechatUxianQandAInfo();
            uxianQandAInfo.question = txtQuestionUpdate.Text;
            uxianQandAInfo.answer = txtAnswerUpdate.Text;
            uxianQandAInfo.pubDateTime = DateTime.Now.ToString("yyyyMMdd HH:mm:ss");
            uxianQandAInfo.operaterID = ((VAEmployeeLoginResponse)Session["UserInfo"]).employeeID;
            uxianQandAInfo.ID = Common.ToInt32(lbl_ID.Text);
            if (uqa.Update(uxianQandAInfo) > 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "js", "alert('修改成功!');", true);
                initData(0, 5);
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
        txtQuestionUpdate.Text = gvr.Cells[1].Text;
        txtAnswerUpdate.Text = gvr.Cells[2].Text;
    }

    protected void GridView_QandA_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int id = Common.ToInt32(GridView_QandA.Rows[e.RowIndex].Cells[0].Text);
        WechatUxianQandAOperate uqa = new WechatUxianQandAOperate();
        if (uqa.Delete(id))
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "js", "alert('删除成功!');", true);
            initData(0, 5);
        }
        else
            ScriptManager.RegisterStartupScript(this, GetType(), "js", "alert('删除失败!');", true);
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        GridView_QandA.SelectedIndex = -1;

        initData(AspNetPager1.StartRecordIndex - 1, AspNetPager1.EndRecordIndex);

        tbUpdate.Attributes.CssStyle.Add("display", "none");
        tbNew.Attributes.CssStyle.Add("display", "block");
        txtAnswerUpdate.Text = "";
        txtQuestionUpdate.Text = "";
    }
}