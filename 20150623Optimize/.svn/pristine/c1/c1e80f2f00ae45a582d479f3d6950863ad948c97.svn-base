using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.WebPageDll;
using VAGastronomistMobileApp.Model;
using System.Configuration;

public partial class RedEnvelope_TreasureChestConfig : System.Web.UI.Page
{
    public const int pageSize = 10;
    TreasureChestConfigOperate treasureChestConfigOperate = new TreasureChestConfigOperate();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindDropDownList(ddlActivityNameQuery);
            BindTreasureChestList(0, 1, pageSize);
            PageInitial();
        }
        hiddenMaxRedEnvelope.Value = ConfigurationManager.AppSettings["maxRedEnvelope"].ToString();
    }
    private void PageInitial()
    {
        if (rblAmout.SelectedValue == "1")
        {
            divMinMax.Attributes.Add("style", "display:''");
            divRange.Attributes.Add("style", "display:none");
            txbDefaultAmountRange.Text = "";
            txbDefaultRateRange.Text = "";
            txbNewAmountRange.Text = "";
            txbNewRateRange.Text = "";
        }
        else
        {
            divMinMax.Attributes.Add("style", "display:none");
            divRange.Attributes.Add("style", "display:''");
            txbMin.Text = "";
            txbMax.Text = "";
        }
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        int activityId = Common.ToInt32(ddlActivityNameQuery.SelectedItem.Value);
        BindTreasureChestList(activityId, 1, pageSize);
    }

    /// <summary>
    /// 绑定所有活动列表
    /// </summary>
    private void BindTreasureChestList(int activityId, int pageIndex, int pageSize)
    {
        int cnt = 0;
        IList<TreasureChestConfig> treasureChest = treasureChestConfigOperate.QueryTreasureChest(new VAGastronomistMobileApp.SQLServerDAL.Persistence.Infrastructure.Page(pageIndex, pageSize), out cnt, activityId);
        if (treasureChest != null && treasureChest.Count > 0)
        {
            AspNetPager1.PageSize = pageSize;
            AspNetPager1.RecordCount = cnt;//总数
            this.gdvTreasureChest.DataSource = treasureChest;
        }
        else
        {
            this.gdvTreasureChest.DataSource = null;
        }
        this.gdvTreasureChest.DataBind();
    }

    protected void lnkbtn_OnCommand(object sender, CommandEventArgs e)
    {
        int treasureChestConfigId = Common.ToInt32(e.CommandArgument);
        ViewState["id"] = treasureChestConfigId;

        switch (e.CommandName)
        {
            case "modify":
                BindTreasureChestInfo(treasureChestConfigId);
                this.divList.Attributes.Add("style", "display:none");
                this.divDetail.Attributes.Add("style", "display:''");
                break;
            case "del":
                bool delete = treasureChestConfigOperate.DeleteTreasureChest(treasureChestConfigId);
                if (delete)
                {
                    BindTreasureChestList(0, 1, pageSize);
                    CommonPageOperate.AlterMsg(this, "删除成功！");
                    Clear();
                }
                else
                {
                    CommonPageOperate.AlterMsg(this, "删除失败！");
                }
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
        BindTreasureChestList(0, AspNetPager1.CurrentPageIndex, pageSize);
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        this.divList.Attributes.Add("style", "display:none");
        this.divDetail.Attributes.Add("style", "display:''");
        this.divMinMax.Attributes.Add("style", "display:''");
        this.divRange.Attributes.Add("style", "display:none");
        BindDropDownList(ddlActivityName);
    }

    private void BindDropDownList(DropDownList ddl)
    {
        ddl.Items.Clear();
        ActivityOperate activityOperate = new ActivityOperate();
        List<Activity> activities = activityOperate.QueryAllActivity();
        ddl.Items.Add(new ListItem("==请选择==", "NA"));
        if (activities.Count > 0)
        {
            foreach (Activity activity in activities)
            {
                ddl.Items.Add(new ListItem(activity.name, activity.activityId.ToString()));
            }
        }
    }
    #region 编辑模式
    private void BindTreasureChestInfo(int id)
    {
        TreasureChestConfig treasureChest = new TreasureChestConfig();
        treasureChest = treasureChestConfigOperate.QueryTreasureChest(id);
        txbAmount.Text = treasureChest.amount.ToString();
        //txbCount.Text = treasureChest.count.ToString();
        txbMin.Text = treasureChest.min == 0 ? "" : treasureChest.min.ToString();
        txbMax.Text = treasureChest.max == 0 ? "" : treasureChest.max.ToString();
        //txbQuantity.Text = treasureChest.quantity.ToString();
        BindDropDownList(ddlActivityName);
        ddlActivityName.SelectedValue = treasureChest.activityId.ToString();
        rblAmout.SelectedValue = treasureChest.amountRule == 0 ? "1" : treasureChest.amountRule.ToString();
        txbDefaultAmountRange.Text = treasureChest.defaultAmountRange == null ? "" : treasureChest.defaultAmountRange.ToString();
        txbDefaultRateRange.Text = treasureChest.defaultRateRange == null ? "" : treasureChest.defaultRateRange.ToString();
        txbNewAmountRange.Text = treasureChest.newAmountRange == null ? "" : treasureChest.newAmountRange.ToString();
        txbNewRateRange.Text = treasureChest.newRateRange == null ? "" : treasureChest.newRateRange.ToString();
        ckbIsPreventCheat.Checked = treasureChest.isPreventCheat;
        PageInitial();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        bool update = false;
        int insert = 0;
        TreasureChestConfig treasureChest = new TreasureChestConfig();
        treasureChest.activityId = Common.ToInt32(ddlActivityName.SelectedValue);
        treasureChest.activityName = ddlActivityName.SelectedItem.Text.Trim();
        treasureChest.amount = Common.ToDouble(txbAmount.Text.Trim());
        //treasureChest.count = Common.ToInt32(txbCount.Text.Trim());
        treasureChest.min = Common.ToDouble(txbMin.Text.Trim());
        treasureChest.max = Common.ToDouble(txbMax.Text.Trim());
        //treasureChest.quantity = Common.ToInt32(txbQuantity.Text.Trim());
        //treasureChest.remainQuantity = Common.ToInt32(txbQuantity.Text.Trim());
        treasureChest.createdBy = Web.Control.SessionHelper.GetCurrectSessionEmployeeId();
        treasureChest.createTime = DateTime.Now;
        treasureChest.updatedBy = Web.Control.SessionHelper.GetCurrectSessionEmployeeId();
        treasureChest.updateTime = DateTime.Now;
        treasureChest.amountRule = Common.ToInt32(rblAmout.SelectedValue);
        treasureChest.defaultAmountRange = txbDefaultAmountRange.Text.Trim(new char[] { ',' });
        treasureChest.defaultRateRange = txbDefaultRateRange.Text.Trim(new char[] { ',' });
        treasureChest.newAmountRange = txbNewAmountRange.Text.Trim(new char[] { ',' });
        treasureChest.newRateRange = txbNewRateRange.Text.Trim(new char[] { ',' });
        treasureChest.isPreventCheat = ckbIsPreventCheat.Checked;
        string err = "";
        if (treasureChest.max - treasureChest.min < -0.001)
        {
            err += "红包最小值不能超过红包最大值\r\n";
        }
        if (Common.ToDouble(hiddenMaxRedEnvelope.Value) - treasureChest.min < -0.001)
        {
            err += "红包最小值不能超过个人红包上限" + hiddenMaxRedEnvelope.Value + "元\\r\\n";
        }
        if (Common.ToDouble(hiddenMaxRedEnvelope.Value) - treasureChest.max < -0.001)
        {
            err += "红包最大值不能超过个人红包上限" + hiddenMaxRedEnvelope.Value + "元\\r\\n";
        }
        //if (treasureChest.min * treasureChest.count - treasureChest.amount > 0.001)
        //{
        //    err += treasureChest.count + "个人都拿最小值时，红包都不够分，请改小一些\\r\\n";
        //}
        //if (treasureChest.max * treasureChest.count - treasureChest.amount < -0.001)
        //{
        //    err += treasureChest.count + "个人都拿最大值时，红包都分不完，请改大一些\\r\\n";
        //}
        try
        {
            int destination;
            string[] source;
            source = treasureChest.defaultAmountRange.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string a in source)
            {
                destination = Convert.ToInt32(a);
            }
            source = treasureChest.defaultRateRange.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string a in source)
            {
                destination = Convert.ToInt32(a);
            }
            source = treasureChest.newAmountRange.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string a in source)
            {
                destination = Convert.ToInt32(a);
            }
            source = treasureChest.newRateRange.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string a in source)
            {
                destination = Convert.ToInt32(a);
            }
        }
        catch (Exception)
        {
            err += "区间值格式填写不正确，请检查\\r\\n";
            CommonPageOperate.AlterMsg(this, err);
        }
        if (err.Length > 0)
        {
            CommonPageOperate.AlterMsg(this, err);
            return;
        }
        ////用户能领取红包的累积最大额
        //double maxRedEnvelope = Common.ToDouble(ConfigurationManager.AppSettings["maxRedEnvelope"]);
        ////宝箱中每个红包金额的范围，暂时这样计算
        //if (treasureChest.amount > maxRedEnvelope)
        //{
        //    treasureChest.min = Common.ToDouble((treasureChest.amount - maxRedEnvelope) / (treasureChest.count - 1));
        //}
        //else
        //{
        //    treasureChest.min = Common.ToDouble(treasureChest.amount / (treasureChest.count) * 0.5);
        //}
        //treasureChest.max = Common.ToDouble((treasureChest.amount - treasureChest.min) / (treasureChest.count - 1));

        if (ViewState["id"] != null && ViewState["id"].ToString() != "")//修改
        {
            treasureChest.treasureChestConfigId = Common.ToInt32(ViewState["id"]);
            update = treasureChestConfigOperate.UpdateActivity(treasureChest);
        }
        else
        {
            treasureChest.treasureChestConfigId = 0;
            insert = treasureChestConfigOperate.InsertTreasureChest(treasureChest);
        }
        if (update || insert > 0)
        {
            this.divDetail.Attributes.Add("style", "display:none");
            this.divList.Attributes.Add("style", "display:''");
            if (!string.IsNullOrEmpty(txbAmount.Text.Trim()))//修改
            {
                BindTreasureChestList(Common.ToInt32(ddlActivityNameQuery.SelectedValue), AspNetPager1.CurrentPageIndex, pageSize);
            }
            else
            {
                BindTreasureChestList(0, 1, pageSize);
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
        txbAmount.Text = "";
        //txbCount.Text = "";
        txbMax.Text = "";
        txbMin.Text = "";
        //  txbQuantity.Text = "";
        ViewState["id"] = null;

        txbDefaultAmountRange.Text = "";
        txbDefaultRateRange.Text = "";
        txbNewAmountRange.Text = "";
        txbNewRateRange.Text = "";
        rblAmout.SelectedValue = "1";
        ckbIsPreventCheat.Checked = false;
    }
    protected void rblAmout_SelectedIndexChanged(object sender, EventArgs e)
    {
        PageInitial();
    }
    protected void gdvTreasureChest_DataBound(object sender, EventArgs e)
    {
        for (int i = 0; i < gdvTreasureChest.Rows.Count; i++)
        {
            if (gdvTreasureChest.Rows[i].Cells[3].Text == "2")
            {
                gdvTreasureChest.Rows[i].Cells[3].Text = "概率取值";
            }
            else
            {
                gdvTreasureChest.Rows[i].Cells[3].Text = "最小值/最大值";
            }
            if (gdvTreasureChest.Rows[i].Cells[4].Text == "0")
            {
                gdvTreasureChest.Rows[i].Cells[4].Text = "";
            }
            if (gdvTreasureChest.Rows[i].Cells[5].Text == "0")
            {
                gdvTreasureChest.Rows[i].Cells[5].Text = "";
            }
        }
    }
}