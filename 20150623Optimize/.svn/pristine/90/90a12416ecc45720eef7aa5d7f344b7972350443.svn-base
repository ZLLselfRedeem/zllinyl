using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.WebPageDll;
using System.Data;
using VAGastronomistMobileApp.Model;

public partial class WeChatPlatManage_FreeCase : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Button_day_Click(Button_1day, e);
            //ScriptManager.RegisterStartupScript(this, GetType(), "js", "alert('后台的');", true);
        }
        if (Label_message.Text != "" || lbl_ID.Text == "")
            tbUpdate.Attributes.CssStyle.Add("display", "none");

        tbNew.Attributes.CssStyle.Add("display", "block");
    }

    protected void Button_day_Click(object sender, EventArgs e)
    {
        clearData();

        string CommandName = "";
        try
        {
            Button Button = (Button)sender;
            CommandName = Button.CommandName;
        }
        catch
        {
            CommandName = sender.ToString();
        }
        switch (CommandName)
        {
            case "1"://
                Button_1day.CssClass = "tabButtonBlueClick";//今天
                Button_yesterday.CssClass = "tabButtonBlueUnClick";//昨天
                Button_7day.CssClass = "tabButtonBlueUnClick";//最近一周
                Button_14day.CssClass = "tabButtonBlueUnClick";//最近14天
                Button_30day.CssClass = "tabButtonBlueUnClick";//最近30天
                Button_self.CssClass = "tabButtonBlueUnClick";//自定义
                TextBox_registerTimeStr.Text = DateTime.Now.ToString("yyyy-MM-dd");//日期控件显示当前时间
                TextBox_registerTimeEnd.Text = DateTime.Now.ToString("yyyy-MM-dd");
                break;
            case "yesterday":
                Button_1day.CssClass = "tabButtonBlueUnClick";//今天
                Button_yesterday.CssClass = "tabButtonBlueClick";//昨天
                Button_7day.CssClass = "tabButtonBlueUnClick";//最近一周
                Button_14day.CssClass = "tabButtonBlueUnClick";//最近14天
                Button_30day.CssClass = "tabButtonBlueUnClick";//最近30天
                Button_self.CssClass = "tabButtonBlueUnClick";//自定义
                TextBox_registerTimeStr.Text = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
                TextBox_registerTimeEnd.Text = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
                break;
            case "7":
                Button_1day.CssClass = "tabButtonBlueUnClick";//今天
                Button_yesterday.CssClass = "tabButtonBlueUnClick";//昨天
                Button_7day.CssClass = "tabButtonBlueClick";//最近一周
                Button_14day.CssClass = "tabButtonBlueUnClick";//最近14天
                Button_30day.CssClass = "tabButtonBlueUnClick";//最近30天
                Button_self.CssClass = "tabButtonBlueUnClick";//自定义
                TextBox_registerTimeStr.Text = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
                TextBox_registerTimeEnd.Text = DateTime.Now.ToString("yyyy-MM-dd");
                break;
            case "14":
                Button_1day.CssClass = "tabButtonBlueUnClick";//今天
                Button_yesterday.CssClass = "tabButtonBlueUnClick";//昨天
                Button_7day.CssClass = "tabButtonBlueUnClick";//最近一周
                Button_14day.CssClass = "tabButtonBlueClick";//最近14天
                Button_30day.CssClass = "tabButtonBlueUnClick";//最近30天
                Button_self.CssClass = "tabButtonBlueUnClick";//自定义
                TextBox_registerTimeStr.Text = DateTime.Now.AddDays(-14).ToString("yyyy-MM-dd");
                TextBox_registerTimeEnd.Text = DateTime.Now.ToString("yyyy-MM-dd");
                break;
            case "30":
                Button_self.CssClass = "tabButtonBlueClick";//自定义
                Button_1day.CssClass = "tabButtonBlueUnClick";//今天
                Button_yesterday.CssClass = "tabButtonBlueUnClick";//昨天
                Button_7day.CssClass = "tabButtonBlueUnClick";//最近一周
                Button_14day.CssClass = "tabButtonBlueUnClick";//最近14天
                Button_30day.CssClass = "tabButtonBlueClick";//最近30天
                TextBox_registerTimeStr.Text = DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd");
                TextBox_registerTimeEnd.Text = DateTime.Now.ToString("yyyy-MM-dd");
                break;
            case "self":
                Button_self.CssClass = "tabButtonBlueClick";//自定义
                Button_1day.CssClass = "tabButtonBlueUnClick";//今天
                Button_yesterday.CssClass = "tabButtonBlueUnClick";//昨天
                Button_7day.CssClass = "tabButtonBlueUnClick";//最近一周
                Button_14day.CssClass = "tabButtonBlueUnClick";//最近14天
                Button_30day.CssClass = "tabButtonBlueUnClick";//最近30天
                break;
            default:
                break;
        }
        Hidden_Day.Value = CommandName;
        SelectFunction();
    }

    private void clearData()
    {
        lbl_ID.Text = "";
        txtContentUpdate.Text = "";
        validSet.Checked = false;

        tbUpdate.Attributes.CssStyle.Add("display", "none");
    }

    /// <summary>
    ///触发查询显示信息方法
    /// </summary>
    protected void SelectFunction()
    {
        if (TextBox_registerTimeStr.Text != "" && TextBox_registerTimeEnd.Text != "")
        {
            ShowGridView_MemberDetailInfo(0, 10);
        }
    }

    /// <summary>
    /// 绑定显示数据表信息
    /// </summary>
    protected void ShowGridView_MemberDetailInfo(int str, int end)
    {
        string strTime = TextBox_registerTimeStr.Text + " 00:00:00";
        string endTime = TextBox_registerTimeEnd.Text + " 23:59:59";

        WechatFreeCaseOperator fco = new WechatFreeCaseOperator();
        DataTable dt = fco.GetFreeCaseInfo(strTime, endTime);

        if (dt.Rows.Count > 0)
        {
            Label_message.Text = "";
            int tableCount = dt.Rows.Count;
            AspNetPager1.RecordCount = tableCount;
            DataTable dt_page = Common.GetPageDataTable(dt, str, end);//分页的DataTable
            GridView_FreeCase.DataSource = dt_page;
            GridView_FreeCase.DataBind();
        }
        else
        {
            Label_message.Text = "暂无符合条件的数据";
            GridView_FreeCase.DataSource = dt;//绑定显示的是空数据，目的为清空显示
            GridView_FreeCase.DataBind();
        }
    }

    protected void Button_Save_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(txtContentNew.Text))
        {
            WechatFreeCaseOperator fco = new WechatFreeCaseOperator();
            WechatFreeCaseInfo freeCaseInfo = new WechatFreeCaseInfo();
            freeCaseInfo.MsgContent = txtContentNew.Text;
            freeCaseInfo.OperaterID = ((VAEmployeeLoginResponse)Session["UserInfo"]).employeeID;
            freeCaseInfo.PubDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            freeCaseInfo.Status = "未过期";

            int iRet = fco.Insert(freeCaseInfo);
            if (iRet > 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "js", "alert('新增成功!');", true);
                txtContentNew.Text = "";
                SelectFunction();
            }
            else
                ScriptManager.RegisterStartupScript(this, GetType(), "js", "alert('新增失败!');", true);

            Button_day_Click(Hidden_Day.Value, e);
        }
    }

    protected void Button_Update_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(lbl_ID.Text) && !string.IsNullOrEmpty(txtContentUpdate.Text))
        {
            WechatFreeCaseOperator fco = new WechatFreeCaseOperator();
            WechatFreeCaseInfo freeCaseInfo = new WechatFreeCaseInfo();
            freeCaseInfo.MsgContent = txtContentUpdate.Text;
            freeCaseInfo.OperaterID = ((VAEmployeeLoginResponse)Session["UserInfo"]).employeeID;
            freeCaseInfo.PubDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            freeCaseInfo.Status = validSet.Checked == true ? "已过期" : "未过期";

            freeCaseInfo.ID = Common.ToInt32(lbl_ID.Text);

            int iRet = fco.Update(freeCaseInfo);
            if (iRet > 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "js", "alert('修改成功!');", true);
                SelectFunction();
            }
            else
                ScriptManager.RegisterStartupScript(this, GetType(), "js", "alert('修改失败!');", true);
        }
    }

    protected void lbtnUpdate_Click(object sender, EventArgs e)
    {
        tbUpdate.Attributes.CssStyle.Add("display", "block");
        tbNew.Attributes.CssStyle.Add("display", "none");
        //显示当前选 择行的数据
        GridViewRow gvr = (GridViewRow)((sender as LinkButton).Parent.Parent);
        lbl_ID.Text = gvr.Cells[0].Text;
        txtContentUpdate.Text = gvr.Cells[1].Text;
        validSet.Checked = gvr.Cells[4].Text == "已过期" ? true : false;

        //Button_day_Click(Hidden_Day.Value, e);
    }

    protected void GridView_FreeCase_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int id = Common.ToInt32(GridView_FreeCase.Rows[e.RowIndex].Cells[0].Text);
        WechatFreeCaseOperator fco = new WechatFreeCaseOperator();
        if (fco.Delete(id))
            SelectFunction();
        else
            ScriptManager.RegisterStartupScript(this, GetType(), "js", "alert('删除失败!');", true);

        Button_day_Click(Hidden_Day.Value, e);
    }
}