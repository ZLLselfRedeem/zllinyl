using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.WebPageDll;
using System.Data;

public partial class CompanyPages_couponsActivitiesManage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // if (!IsPostBack)
        //{
        //{"PageSize":"10","PageIndex":"2","shopId":"89","companyId":"67","timerStatus":"1"}
        //ShopCouponInfo(10, 3, 89, 67, 1);
        // }
    }
    /// <summary>
    /// 显示优惠券列表信息
    /// </summary>
    /// <param name="PageSize">页面尺寸</param>
    /// <param name="PageIndex">页码</param>
    /// <param name="shopId">门店ID</param>
    /// <param name="companyId">公司ID</param>
    /// <param name="timerStatus">进行中→1；未开始→2；已结束→3</param>
    /// <returns></returns>
    [WebMethod]
    public static string ShopCouponInfo(int PageSize, int PageIndex, int shopId, int companyId, int timerStatus)
    {
        if (HttpContext.Current.Session["MerchantsTreasureUserInfo"] == null)
        {
            return "-1000";
        }
        //优惠券标题couponName，优惠券内容couponDesc，发放起止日期GrantingDate，已发放provided ， 已使用used ， 回收率recovery ， 总金额sumAmount
        int ocount = 0;
        string json = "";
        double totalAmount = 0;
        string tblName = "CouponInfo inner join CustomerConnCoupon on CouponInfo.couponID=CustomerConnCoupon.couponID inner join CouponConnShop  on CouponConnShop.couponID=CouponInfo.couponID left join CustomerCouponPreOrder on CustomerCouponPreOrder.customerConnCouponID=CustomerConnCoupon.customerConnCouponID left join PreOrder19dian on PreOrder19dian.preOrder19dianId=CustomerCouponPreOrder.preOrderID";
        string strGetFields = "CouponInfo.couponID, CouponInfo.couponName ,CouponInfo.couponDesc,CONVERT(varchar(100),CouponInfo.couponValidStartTime, 23)+'至'+CONVERT(varchar(100),CouponInfo.couponValidEndTime,23)+'止' as 'GrantingDate',count(CustomerConnCoupon.customerConnCouponID) as 'provided' ,COUNT(case when CustomerConnCoupon.status='2'then 1 end) as 'used' , SUBSTRING(convert(varchar,((COUNT( case when CustomerConnCoupon.status='2' then 1 end))/ (COUNT( CustomerConnCoupon.customerConnCouponID)*1.0))*100),1,4) as 'recovery', ISNULL( SUM( case  when CustomerConnCoupon.status='2' then PreOrder19dian.preOrderServerSum  end),0) as 'sumAmount'";
        string strWhere = " CouponConnShop.shopId='" + shopId + "' and CouponConnShop.companyId='" + companyId + "'";//过滤获得当前公司当前门店下的优惠券信息
        switch (timerStatus)
        {
            case 1://进行中
                strWhere += " and CouponInfo.couponValidStartTime<GETDATE() and GETDATE()<CouponInfo.couponValidEndTime";//当前系统时间大于优惠券开始时间小于优惠券结束时间
                break;
            case 2://未开始
                strWhere += " and CouponInfo.couponValidStartTime>GETDATE()";//优惠券开始时间大于系统当期时间
                break;
            case 3://已结束
                strWhere += " and  GETDATE()>CouponInfo.couponValidEndTime";//优惠券结束时间小于系统当前时间
                break;
        }
        strWhere += " group by CouponInfo.couponID,CouponInfo.couponName ,CouponInfo.couponDesc, CouponInfo.couponValidStartTime,CouponInfo.couponValidEndTime";
        DataTable dtJson = Common.GetDataTableFieldValue(tblName, strGetFields, strWhere);
        if (dtJson.Rows.Count > 0)
        {
            for (int i = 0; i < dtJson.Rows.Count; i++)
            {
                totalAmount += Common.ToDouble(dtJson.Rows[i]["sumAmount"]);
            }
            ocount = dtJson.Rows.Count;
            DataTable dt_page = Common.GetPageDataTable(dtJson, (PageIndex - 1) * PageSize, PageIndex * PageSize);
            json = Common.ConvertDateTableToJson(dt_page);
        }
        if (!String.IsNullOrEmpty(json))
        {
            json = json.TrimEnd('}');
            json += ",\"total\":[{\"totalAmount\":\"" + totalAmount.ToString() + "\"},{\"ocount\":" + ocount + "}]}";
        }
        else
        {
            return "";
        }
        return json;
    }
}