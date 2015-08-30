using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.WebPageDll;
using VAGastronomistMobileApp.Model;
using System.Collections;
using VAGastronomistMobileApp.SQLServerDAL.Persistence.Infrastructure;

public partial class RedEnvelope_ActivityManage : System.Web.UI.Page
{
    public const int pageSize = 20;
    ActivityOperate activityOperate = new ActivityOperate();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindActivityList("", 1, pageSize);
        }
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        string name = txbNameQuery.Text.Trim();
        BindActivityList(name, 1, pageSize);
    }

    /// <summary>
    /// 绑定所有活动列表
    /// </summary>
    private void BindActivityList(string name, int pageIndex, int pageSize)
    {
        int cnt = 0;
        IList<Activity> activities = activityOperate.QueryActivity(new VAGastronomistMobileApp.SQLServerDAL.Persistence.Infrastructure.Page(pageIndex, pageSize), name, out cnt);
        if (activities != null && activities.Count > 0)
        {
            AspNetPager1.PageSize = pageSize;
            AspNetPager1.RecordCount = cnt;
            this.gdvActivity.DataSource = activities;
        }
        else
        {
            this.gdvActivity.DataSource = null;
        }
        this.gdvActivity.DataBind();
    }

    protected void lnkbtn_OnCommand(object sender, CommandEventArgs e)
    {
        int activityId = Common.ToInt32(e.CommandArgument);
        ViewState["id"] = activityId;

        switch (e.CommandName)
        {
            case "enable":
                bool enable = activityOperate.EnableActivity(activityId);
                if (enable)
                {
                    BindActivityList("", 1, pageSize);
                }
                break;
            case "modify":
                BindActivityInfo(activityId);
                this.divList.Attributes.Add("style", "display:none");
                this.divDetail.Attributes.Add("style", "display:''");
                break;
            case "del":
                bool delete = activityOperate.DeleteActivity(activityId);
                if (delete)
                {
                    BindActivityList("", 1, pageSize);
                    CommonPageOperate.AlterMsg(this, "删除成功！");
                    Clear();
                }
                else
                {
                    CommonPageOperate.AlterMsg(this, "删除失败！");
                }
                break;
            case "share":
                Response.Redirect("ActivityShareConfig.aspx?activityId=" + activityId + "");
                break;
            default:
                break;
        }
    }


    /// <summary>
    /// 分页操作
    /// </summary>
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        BindActivityList("", AspNetPager1.CurrentPageIndex, pageSize);
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        this.divList.Attributes.Add("style", "display:none");
        this.divDetail.Attributes.Add("style", "display:''");
    }

    protected void gdvActivity_DataBound(object sender, EventArgs e)
    {
        Label lbEnable = new Label();
        string strEnable = "";
        string ruleValue = "";
        int rule = 0, type = 0;
        for (int i = 0; i < gdvActivity.Rows.Count; i++)
        {
            lbEnable = (Label)gdvActivity.Rows[i].FindControl("lbActivityTime");
            lbEnable.Text = "起" + gdvActivity.DataKeys[i].Values["beginTime"].ToString() + "至" + gdvActivity.DataKeys[i].Values["endTime"].ToString();

            lbEnable = (Label)gdvActivity.Rows[i].FindControl("lbEabled");
            strEnable = gdvActivity.DataKeys[i].Values[0].ToString();
            if (strEnable == "True")
            {
                lbEnable.Text = "已启用";
            }
            else
            {
                lbEnable.Text = "未启用";
            }

            Label lbRedEnvelopeTime = (Label)gdvActivity.Rows[i].FindControl("lbRedEnvelopeTime");
            
            rule = Common.ToInt32(gdvActivity.DataKeys[i].Values[1]);
            switch (rule)
            {
                case (int)ExpirationTimeRule.postpone:
                    ruleValue = gdvActivity.DataKeys[i].Values[2].ToString();
                    gdvActivity.Rows[i].Cells[5].Text = "顺延" + ruleValue + "日";
                    lbRedEnvelopeTime.Text = "";
                    break;
                case (int)ExpirationTimeRule.unify:
                    gdvActivity.Rows[i].Cells[5].Text = "统一时间点";
                    lbRedEnvelopeTime.Text = "起" + gdvActivity.DataKeys[i].Values["redEnvelopeEffectiveBeginTime"].ToString() + "至" + gdvActivity.DataKeys[i].Values["redEnvelopeEffectiveEndTime"].ToString();
                    break;
                default:
                    gdvActivity.Rows[i].Cells[5].Text = "";
                    break;
            }

            type = Common.ToInt32(gdvActivity.DataKeys[i].Values["activityType"]);
            switch (type)
            {
                case (int)ActivityType.大红包:
                    gdvActivity.Rows[i].Cells[4].Text = "大红包";
                    break;
                case (int)ActivityType.天天红包:
                    gdvActivity.Rows[i].Cells[4].Text = "天天红包";
                    break;
                case (int)ActivityType.节日免单红包:
                    gdvActivity.Rows[i].Cells[4].Text = "节日免单红包";
                    break;
                case (int)ActivityType.赠送红包:
                    gdvActivity.Rows[i].Cells[4].Text = "赠送红包";
                    break;
                case (int)ActivityType.抽奖红包:
                    gdvActivity.Rows[i].Cells[4].Text = "抽奖红包";
                    break;
                default:
                    gdvActivity.Rows[i].Cells[4].Text = "";
                    break;
            }
        }
    }

    #region 编辑模式
    private void BindActivityInfo(int id)
    {
        Activity activity = new Activity();
        activity = activityOperate.QueryActivity(id);
        txbName.Text = activity.name;
        txbBeginTime.Text = activity.beginTime.ToString();
        txbEndTime.Text = activity.endTime.ToString();
        if ((int)activity.expirationTimeRule == 1)
        {
            rb_1.Checked = true;
        }
        else
        {
            rb_2.Checked = true;
        }
        txbRuleValue.Text = activity.ruleValue.ToString();
        //if (!string.IsNullOrEmpty(activity.activityRule))
        //{
        //    txbActivityRule.Text = activity.activityRule.ToString();
        //}
        ddlActivityType.SelectedValue = ((int)activity.activityType).ToString();
        txtRedEnvelopeEffectiveBeginTime.Text = Common.ToDateTime(activity.redEnvelopeEffectiveBeginTime) <= DateTime.MinValue ? "" : activity.redEnvelopeEffectiveBeginTime.ToString();
        txtRedEnvelopeEffectiveEndTime.Text = Common.ToDateTime(activity.redEnvelopeEffectiveEndTime) <= DateTime.MinValue ? "" : activity.redEnvelopeEffectiveEndTime.ToString();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        bool update = false;
        int insert = 0;
        Activity activity = new Activity();
        if (rb_2.Checked)
        {
            activity.redEnvelopeEffectiveBeginTime = String.IsNullOrWhiteSpace(txtRedEnvelopeEffectiveBeginTime.Text) ? Common.ToDateTime(txbBeginTime.Text.Trim()) : Common.ToDateTime(txtRedEnvelopeEffectiveBeginTime.Text.Trim());
            activity.redEnvelopeEffectiveEndTime = String.IsNullOrWhiteSpace(txtRedEnvelopeEffectiveEndTime.Text) ? Common.ToDateTime(txbEndTime.Text.Trim()) : Common.ToDateTime(txtRedEnvelopeEffectiveEndTime.Text.Trim());
        }
        else
        {
            //activity.redEnvelopeEffectiveBeginTime = DateTime.MinValue;
            //activity.redEnvelopeEffectiveEndTime = DateTime.MinValue;
        }

        activity.name = txbName.Text.Trim();
        activity.beginTime = Common.ToDateTime(txbBeginTime.Text.Trim());
        activity.endTime = Common.ToDateTime(txbEndTime.Text.Trim());
        //activity.activityRule = txbActivityRule.Text.Trim();
        activity.activityRule = "";
        activity.expirationTimeRule = rb_1.Checked == true ? ExpirationTimeRule.postpone : ExpirationTimeRule.unify;
        if (activity.expirationTimeRule == ExpirationTimeRule.postpone)
        {
            activity.ruleValue = Common.ToInt32(txbRuleValue.Text.Trim());
        }
        else
        {
            activity.ruleValue = 0;
        }
        if ((VAEmployeeLoginResponse)Session["UserInfo"] != null)
        {
            activity.createdBy = ((VAEmployeeLoginResponse)Session["UserInfo"]).employeeID;
        }
        else
        {
            activity.createdBy = 0;
        }
        activity.activityType = (ActivityType)Common.ToInt32(ddlActivityType.SelectedValue);
        if (ViewState["id"] != null && ViewState["id"].ToString() != "")//修改
        {
            activity.activityId = Common.ToInt32(ViewState["id"]);
            update = rb_2.Checked ? activityOperate.UpdateActivity(activity) : activityOperate.UpdateActivityExt(activity);
        }
        else
        {
            activity.activityId = 0;
            insert = rb_2.Checked ? activityOperate.InsertActivity(activity) : activityOperate.InsertActivityExt(activity);
        }
        if (update || insert > 0)
        {
            this.divDetail.Attributes.Add("style", "display:none");
            this.divList.Attributes.Add("style", "display:''");
            if (!string.IsNullOrEmpty(txbNameQuery.Text.Trim()))//修改
            {
                BindActivityList(txbName.Text.Trim(), AspNetPager1.StartRecordIndex, AspNetPager1.RecordCount);
            }
            else
            {
                BindActivityList("", 1, pageSize);
            }
            Clear();
            CommonPageOperate.AlterMsg(this, "保存成功！");
        }
        else
        {
            CommonPageOperate.AlterMsg(this, "保存失败！");
        }
    }

    #endregion
    protected void btnCancle_Click(object sender, EventArgs e)
    {
        this.divDetail.Attributes.Add("style", "display:none");
        this.divList.Attributes.Add("style", "display:''");
        Clear();
    }

    private void Clear()
    {
        txbName.Text = "";
        txbBeginTime.Text = "";
        txbEndTime.Text = "";
        ViewState["id"] = null;
        ddlActivityType.SelectedValue = ((int)ActivityType.大红包).ToString();
        txtRedEnvelopeEffectiveBeginTime.Text = "";
        txtRedEnvelopeEffectiveEndTime.Text = "";
        txbRuleValue.Text = "";
        rb_1.Checked = false;
        rb_2.Checked = false;
    }
}