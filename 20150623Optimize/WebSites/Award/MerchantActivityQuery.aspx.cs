using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.WebPageDll;

public partial class Award_MerchantActivityQuery : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            GetMerchantActivity(0, 10);
        }
    }

    protected void GetMerchantActivity(int beginIndex,int endIndex)
    {
        // 城市
        string cityID = DropDownListCity.SelectedValue;
        // 门店名
        string shopName = txtShopName.Text.Trim();
        // 变更状态
        string changeStatus = drpChangeStatus.SelectedValue;
        // 发生变更时间 开始
        DateTime beginDateTimeValue = Convert.ToDateTime(beginTime.Text=="" ? null : beginTime.Text);
        // 发生变更时间 结束
        DateTime endDateTimeValue = Convert.ToDateTime(endTime.Text == "" ? null : endTime.Text);
        ShopConnRedEnvelopeOperate operate = new ShopConnRedEnvelopeOperate();
        ShopAwardOperate operateShopAward = new ShopAwardOperate();
        DataTable dt=operateShopAward.SelectBussinessActivity(cityID, shopName, changeStatus, beginDateTimeValue, endDateTimeValue,null);

        if (dt.Rows.Count > 0)
        {
            int tableCount = dt.Rows.Count;
            AspNetPager1.RecordCount = tableCount;
            DataTable dt_page = Common.GetPageDataTable(dt, beginIndex, endIndex);//分页的DataTable
            foreach(DataRow dr in dt_page.Rows)
            {
                // 抽奖功能
                if (Common.ToInt32(dr["awardCount"]) > 0)
                {
                    dr["awardCount"] = "已开通";
                }
                else
                {
                    dr["awardCount"] = "未开通";
                }
                // 免排队
                if (Common.ToInt32(dr["avoidQueueCount"])== 0)
                {
                    dr["avoidQueueCount"] = "未开通";
                }
                else
                {
                    // 查询免排队数量
                    int shopID = Common.ToInt32(dr["shopID"]);
                    var listShopAward=operateShopAward.SelectShopAwardList(shopID);
                    dr["avoidQueueCount"]=listShopAward.Find(s => s.AwardType == AwardType.AvoidQueue).Count;
                }
                // 赠菜
                if (Common.ToInt32(dr["presentDishCount"]) == 0)
                {
                    dr["presentDishCount"] = "未开通";
                }
                else
                {
                    int shopID = Common.ToInt32(dr["ShopID"]);
                    // 这里根据shopID查询店铺详细的菜品
                }
                // 返现限量
                if(Common.ToInt32(dr["shopRedCount"])==0)
                {
                    dr["shopRedCount"] = "未开通";
                }
                else
                {
                    // 查询返现限量
                    int shopID = Common.ToInt32(dr["shopID"]);
                    var objShopConnRedEnvelope=operate.SelectShopConnRedEnvelope(shopID);
                    if(objShopConnRedEnvelope.Id>0)
                    {
                        dr["shopRedCount"] = objShopConnRedEnvelope.RedEnvelopeConsumeCount;
                    }
                }
            }
            GridView_AwardTotal.DataSource = dt_page;
            GridView_AwardTotal.DataBind();
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

    /// <summary>
    /// 事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GridView_AwardTotal_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;//转换为按钮类型，获取其所在的行的索引
        string shopID = GridView_AwardTotal.DataKeys[index].Values["shopID"].ToString();
        string shopName = GridView_AwardTotal.DataKeys[index].Values["shopName"].ToString();
        string companyName = GridView_AwardTotal.DataKeys[index].Values["companyName"].ToString();
        switch (e.CommandName.ToString())
        {
            case "Select":
                Response.Redirect("ChangeHistory.aspx?shopID=" + shopID + "&shopName=" + shopName + "&companyName=" + companyName);
                break;
            case "Edit":
                Response.Redirect("EditMerchantActivity.aspx?shopID=" + shopID);
                break;
            default:
                break;
        }
    }
}