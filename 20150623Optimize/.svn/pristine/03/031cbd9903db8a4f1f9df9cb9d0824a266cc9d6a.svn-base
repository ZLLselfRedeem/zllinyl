using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.WebPageDll;

public partial class Award_MerchantActivityCount : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            GetMerchantActivity(0, 10);
        }
    }

    /// <summary>
    /// 查询
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        GetMerchantActivity(0, 10);
    }

    /// <summary>
    /// 分页
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        GetMerchantActivity(AspNetPager1.StartRecordIndex - 1, AspNetPager1.EndRecordIndex);
    }

    protected void GetMerchantActivity(int beginIndex, int endIndex)
    {
        // 城市
        string cityID = DropDownListCity.SelectedValue;
        // 门店名
        string shopName = txtShopName.Text.Trim();
        // 时间 开始
        DateTime beginDateTimeValue = Convert.ToDateTime(beginTime.Text == "" ? null : beginTime.Text);
        // 时间 结束
        DateTime endDateTimeValue = Convert.ToDateTime(endTime.Text == "" ? null : endTime.Text);

        ShopAwardOperate operateShopAward = new ShopAwardOperate();
        DataTable dt = operateShopAward.SelectBussinessActivityTotal(cityID, shopName,beginDateTimeValue, endDateTimeValue, null,1);
        // 商家数量
        int shopCount = 0; 
        if (dt.Rows.Count > 0)
        {
            shopCount = dt.Rows.Count;
            int tableCount = dt.Rows.Count;
            AspNetPager1.RecordCount = tableCount;
            DataTable dt_page = Common.GetPageDataTable(dt, beginIndex, endIndex);//分页的DataTable
            GridView_AwardTotal.DataSource = dt_page;
            GridView_AwardTotal.DataBind();
        }
        else
        {
            int tableCount = dt.Rows.Count;
            AspNetPager1.RecordCount = tableCount;
            GridView_AwardTotal.DataSource = dt;
            GridView_AwardTotal.DataBind();
        }

        // 统计总计
        dt = operateShopAward.SelectBussinessActivityTotal(cityID, shopName, beginDateTimeValue, endDateTimeValue, null, 2);
        if (dt.Rows.Count > 0)
        {
            DataRow dr = dt.Rows[0];
            var orderMoneySum = Common.ToDecimal(dr["orderMoneySum"]);
            var orderCountSum = Common.ToInt32(dr["orderCountSum"]);
            var avoidQueueCountSum = Common.ToInt32(dr["avoidQueueCountSum"]);
            var presentDishCountSum = Common.ToInt32(dr["presentDishCountSum"]);
            var redCountSum = Common.ToInt32(dr["redCountSum"]);
            var thirdCountSum = Common.ToInt32(dr["thirdCountSum"]);
            var noAwardCount = Common.ToInt32(dr["noAwardCount"]);
            lblAwardInfo.Text = "共{0}家门店，订单总金额{1}，订单总量{2}，中[免排队]{3}，中[赠菜]{4}，中[返现]{5}，中[第三方]{6}，未中奖{7}";
            lblAwardInfo.Text = string.Format(lblAwardInfo.Text, shopCount, orderMoneySum, orderCountSum, avoidQueueCountSum, presentDishCountSum, redCountSum, thirdCountSum, noAwardCount);
        }
    }
}