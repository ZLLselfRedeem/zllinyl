using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.WebPageDll;
using System.Web.Services;
using VAGastronomistMobileApp.Model;
using System.Data;
using System.Text;
using VAGastronomistMobileApp.DBUtility;

public partial class CompanyPages_couponUsedDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        #region 注释
        //if (!IsPostBack)
        //{
        // ShowCouponReportData("2013/9/1", "2013/9/2");
        //ShowCouponReportData("2013/1/1", "2013/9/30");
        // couponId = 8;
        // {"PageSize":"10","PageIndex":"1","shopId":"89","companyId":"67"}
        // ShopCouponUsedDetail(10, 1, 89, 67);
        //if (!String.IsNullOrEmpty(Request.QueryString["couponId"]))//
        //{
        //    couponId = Common.ToInt32(Request.QueryString["couponId"]);
        //    if (!String.IsNullOrEmpty(Request.QueryString["Encouragetype"]))
        //    {
        //        //注释：encouragetype=1，不需要展示折线图
        //        //encouragetype=2或者3，调用方法ShowCouponReportData展示折线图
        //        encouragetype = Common.ToInt32(Request.QueryString["Encouragetype"]);//???????????
        //    }
        //    else
        //    {
        //        encouragetype = 0;
        //    }
        //}
        // } 
        #endregion
    }
    /// <summary>
    /// 展示当前优惠券活动下（couponId下）所有使用该优惠券点单列表信息
    /// </summary>
    /// <param name="PageSize">页面尺寸</param>
    /// <param name="PageIndex">页面</param>
    /// <param name="shopId">门店ID</param>
    /// <param name="companyId">公司ID</param>
    /// <returns></returns>
    [WebMethod]
    public static string ShopCouponUsedDetail(int PageSize, int PageIndex, int shopId, int companyId, int couponId)
    {
        if (HttpContext.Current.Session["MerchantsTreasureUserInfo"] == null)
        {
            return "-1000";
        }
        PaginationPager pg = new PaginationPager();
        pg.tblName = "PreOrder19dian inner join CustomerCouponPreOrder on CustomerCouponPreOrder.preOrderID=preOrder19dian.preOrder19dianId inner join CustomerConnCoupon on CustomerConnCoupon.customerConnCouponID=CustomerCouponPreOrder.customerConnCouponID ";
        pg.PageSize = PageSize;
        pg.PageIndex = PageIndex;
        pg.strGetFields = " PreOrder19dian.preOrder19dianId,PreOrder19dian.preOrderServerSum,CustomerConnCoupon.useTime";
        pg.strWhere = " CustomerConnCoupon.couponID='" + couponId + "' and CustomerConnCoupon.status='2' and PreOrder19dian.shopId='" + shopId + "' and preOrder19dian.companyId='" + companyId + "'";//已使用优惠券
        pg.strWhere += " group by  PreOrder19dian.preOrder19dianId,PreOrder19dian.preOrderServerSum,CustomerConnCoupon.useTime";
        pg.OrderType = 0;
        pg.OrderfldName = "PreOrder19dian.preOrder19dianId";
        pg.realOrderfldName = "preOrder19dianId";
        string totalAmount = Common.ToString(Common.GetFieldValue(" (select " + pg.strGetFields + " from " + pg.tblName + " where " + pg.strWhere + " )tmp", " SUM(tmp.preOrderServerSum) ", ""));
        string json = Common.ConvertDateTableToJson(Common.DbPager(pg));
        json = json.TrimEnd('}');
        json += ",\"total\":[{\"totalAmount\":\"" + totalAmount + "\"},{\"ocount\":" + pg.doCount + "}]";
        json += ",\"CouponNameAndStatus\":[{\"couponName\":\"" + GetCouponName(couponId) + "\"}]}";
        return json;
    }
    /// <summary>
    /// 报表数据统计
    /// </summary>
    /// <param name="strTime">开始时间</param>
    /// <param name="endTime">结束时间</param>
    /// <returns></returns>
    [WebMethod]
    public static string ShowCouponReportData(string strTime, string endTime, int couponId)
    {
        if (HttpContext.Current.Session["MerchantsTreasureUserInfo"] == null)
        {
            return "-1000";
        }
        string str1 = Common.ConvertDateTableToJson(QueryCouponUsed(strTime, endTime, couponId)) == null ? "{ \"TableJson\":[ ]}" : Common.ConvertDateTableToJson(QueryCouponUsed(strTime, endTime, couponId));
        string str2 = Common.ConvertDateTableToJson(QueryCouponGrant(strTime, endTime)) == null ? "{ \"TableJson\":[ ]}" : Common.ConvertDateTableToJson(QueryCouponGrant(strTime, endTime));
        string sign = "{" + "\"couponUsedInfo\":[" + str1 + "],\"couponGrantInfo\":[" + str2 + "]}";
        return sign;
    }

    /// <summary>
    /// 根据couponId获取couponName
    /// </summary>
    public static string GetCouponName(int couponId)
    {
        string couponName = Common.ToString(Common.GetFieldValue("CouponInfo", "couponName", "couponID='" + couponId + "'"));
        return couponName;
    }
    //优惠券使用情况（时间，数量）
    public static DataTable QueryCouponUsed(string strTime, string endTime, int couponId)
    {
        strTime = strTime + " 00:00:00";
        endTime = endTime + " 23:59:59";
        StringBuilder strSqlCouponUsed = new StringBuilder();
        strSqlCouponUsed.Append(" select CONVERT(varchar(10),CustomerConnCoupon.useTime, 120) 'time',");//使用时间
        strSqlCouponUsed.Append(" COUNT(customerConnCouponID) 'count'");//使用数量
        strSqlCouponUsed.Append(" from CustomerConnCoupon");
        strSqlCouponUsed.Append(" where CustomerConnCoupon.useTime between '" + strTime + "' and '" + endTime + "'");
        strSqlCouponUsed.Append(" and CustomerConnCoupon.couponID='" + couponId + "' and CustomerConnCoupon.useTime is not null");//直接过滤掉没有使用时间的垃圾数据
        strSqlCouponUsed.Append(" and CustomerConnCoupon.status='2'");//已使用
        strSqlCouponUsed.Append(" group by CONVERT(varchar(10),CustomerConnCoupon.useTime, 120)");
        strSqlCouponUsed.Append(" order by CONVERT(varchar(10),CustomerConnCoupon.useTime, 120)");
        DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSqlCouponUsed.ToString());
        return ds.Tables[0];
    }
    //优惠券发放情况（时间，数量）
    public static DataTable QueryCouponGrant(string strTime, string endTime)
    {
        strTime = strTime + " 00:00:00";
        endTime = endTime + " 23:59:59";
        StringBuilder strSqlCouponGrant = new StringBuilder();
        strSqlCouponGrant.Append(" select CONVERT(varchar(10),CustomerConnCoupon.downloadTime, 120) 'time',");//发放时间
        strSqlCouponGrant.Append(" COUNT(customerConnCouponID) 'count'");//发放数量
        strSqlCouponGrant.Append(" from CustomerConnCoupon");
        strSqlCouponGrant.Append(" where CustomerConnCoupon.downloadTime between '" + strTime + "' and '" + endTime + "'");
        strSqlCouponGrant.Append(" group by CONVERT(varchar(10),CustomerConnCoupon.downloadTime, 120)");
        strSqlCouponGrant.Append(" order by CONVERT(varchar(10),CustomerConnCoupon.downloadTime, 120)");
        DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSqlCouponGrant.ToString());
        return ds.Tables[0];
    }
}