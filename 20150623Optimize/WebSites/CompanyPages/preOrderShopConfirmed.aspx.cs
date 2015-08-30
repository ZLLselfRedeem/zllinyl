using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using VAGastronomistMobileApp.WebPageDll;
using VAGastronomistMobileApp.Model;
using System.Data;
using System.Text;
using System.Transactions;
using VA.Cache;
using VAGastronomistMobileApp.SQLServerDAL;
using VA.CacheLogic.SybWeb;

public partial class CompanyPages_preOrderShopConfirmed : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }
    /// <summary>
    ///  财务审核和取消审核
    /// </summary>
    /// <param name="preOrder19dianId">点单Id</param>
    /// <param name="statusFlag">审核，取消审核（标记）</param>
    /// <returns></returns>
    [WebMethod]
    public static string ShopConfirmedOperate(Guid preOrder19dianId, int statusFlag)//int preOrder19dianId
    {
        if (HttpContext.Current.Session["MerchantsTreasureUserInfo"] == null)
        {
            return "-1000";
        }
        else
        {
            string returnResult = string.Empty;

            PreOrder19dianOperate preOrder19dianOperate = new PreOrder19dianOperate();
            if (preOrder19dianOperate.getPreOrder19dianCount(preOrder19dianId) > 1 && statusFlag != 1)
            {
                return "-9";//前端提示：当前单子已补差价，无法取消入座，请选择退款。  
            }

            long preOrder19dianId1 = 0;
            List<PreOrder19dianInfo> list = preOrder19dianOperate.GetPreOrder19dianByOrderId(preOrder19dianId);
            foreach (PreOrder19dianInfo p in list)
            {
                if (p.OrderType.Equals(OrderTypeEnum.Normal))
                {
                    preOrder19dianId1 = p.preOrder19dianId;
                }
            }

            SybMoneyMerchantOperate syb = new SybMoneyMerchantOperate();
            int result = syb.ConfrimPreOrder(preOrder19dianId1, statusFlag, PreOrderConfirmOperater.Cash);
            switch (result)
            {
                case 1:
                    returnResult = "1";//审核成功
                    break;
                case -1:
                    returnResult = "-1";//前端提示：当前点单已对账，无法取消审核
                    break;
                case -2:
                    returnResult = "-2";//前端提示：当前单子是未审核状态，无法取消审核
                    break;
                case -3:
                    returnResult = "-3";//前端提示：当前单子是已审核状态，无法审核
                    break;
                case -7:
                    returnResult = "-7";//前端提示：当前单子已部分退款或者全部退款，无法取消审核
                    break;
                case -8:
                    returnResult = "-8";//前端提示：当前点单已全部退款，无法入座
                    break;
                case 0:
                    returnResult = "0";
                    break;
            }
            return returnResult;
        }
    }
    /// <summary>
    /// 显示页面审核或者未审核或者全部的列表信息
    /// </summary>
    /// <param name="shopId">门店Id</param>
    /// <param name="PageSize">每页多少条</param>
    /// <param name="PageIndex">第几页</param>
    /// <param name="inputTextStr">查询文本框的值，初始化传“”</param>
    /// <param name="approvedStatus">已审核，未审核，全部</param>
    /// <param name="preOrderTimeStr">时间选择框，开始时间</param>
    /// <param name="preOrderTimeEnd">结束时间</param>
    /// <returns></returns>
    [WebMethod]
    public static string ShowPageConfirmedInfo(int PageSize, int PageIndex, int shopId, string inputTextStr, int approvedStatus, string preOrderTimeStr, string preOrderTimeEnd)
    {
        if (HttpContext.Current.Session["MerchantsTreasureUserInfo"] == null)
        {
            return "-1000";
        }

        DataTable dtOrder = MerchantInfoOperate.GetSybConfirmedOrder(shopId);

        if (dtOrder == null)
        {
            return "";
        }
        if (dtOrder.Rows.Count <= 0)
        {
            return "";
        }

        dtOrder = (from row in dtOrder.AsEnumerable()
                   where row.Field<int>("shopId") == shopId
                    && row.Field<DateTime>("prePayTime") >= Common.ToDateTime(preOrderTimeStr + " 00:00:00")//注意开闭时间区间
                    && row.Field<DateTime>("prePayTime") < Common.ToDateTime(preOrderTimeEnd + " 23:59:59")
                   orderby row.Field<DateTime>("prePayTime") descending
                   select row).CopyToDataTable();
        switch (approvedStatus)
        {
            case 1://已入座
                dtOrder = (from row in dtOrder.AsEnumerable()
                           where row.Field<int>("isSruhopConfirmed") == (int)VAPreOrderShopConfirmed.SHOPCONFIRMED
                           select row).CopyToDataTable();
                break;
            case 2://未入座
                dtOrder = (from row in dtOrder.AsEnumerable()
                           where row.Field<int>("isShopConfirmed") == (int)VAPreOrderShopConfirmed.NOT_SHOPCONFIRMED
                           select row).CopyToDataTable();
                break;
            default://全部
                break;
        }

        if (!string.IsNullOrEmpty(inputTextStr))//输入的流水号，手机号码不为空
        {
            dtOrder = (from row in dtOrder.AsEnumerable()
                       where row.Field<string>("mobilePhoneNumber").Contains(inputTextStr)
                       select row).CopyToDataTable();
        }

        double payTotalAmount = 0, refundAmount = 0, ocount = dtOrder.Rows.Count;
        string json = "";
        if (dtOrder.Rows.Count > 0)
        {
            Guid[] orderIds = new Guid[dtOrder.Rows.Count];

            for (int i = 0; i < dtOrder.Rows.Count; i++)
            {
                if (!dtOrder.Rows[i]["orderId"].ToString().Equals(string.Empty))
                {
                    orderIds[i] = (Guid)dtOrder.Rows[i]["orderId"];
                }
            }

            OrderOperate orderOperate = new OrderOperate();
            List<OrderPaidDetail> orderPaidDetails = orderOperate.GetOrderPaidList(orderIds);

            for (int i = 0; i < dtOrder.Rows.Count; i++)
            {
                foreach (OrderPaidDetail detail in orderPaidDetails)
                {
                    if (!dtOrder.Rows[i]["orderId"].ToString().Equals(string.Empty) && detail.id == (Guid)dtOrder.Rows[i]["orderId"])
                    {
                        dtOrder.Rows[i]["prePaidSum"] = detail.PreOrderServerSum - detail.VerifiedSaving;
                        dtOrder.Rows[i]["refundMoneySum"] = detail.refundMoneySum;
                        //dt_page.Rows[i]["preOrder19dianId"] = detail.id.ToString();
                    }
                }
            }
            payTotalAmount = Common.ToDouble(dtOrder.Compute("sum(prePaidSum)", "1=1"));
            refundAmount = Common.ToDouble(dtOrder.Compute("sum(refundMoneySum)", "1=1"));
            DataTable dt_page = Common.GetPageDataTable(dtOrder, (PageIndex - 1) * PageSize, PageIndex * PageSize);

           
            //DataTable dtclone = dt_page.Clone();

            //dtclone.Columns["preOrder19dianId"].DataType = typeof(string);

            //for (int i = 0; i < dt_page.Rows.Count; i++)
            //{
            //    DataRow dr = dtclone.NewRow();
            //    //C.mobilePhoneNumber,isnull(B.refundMoneySum,0) refundMoneySum,orderId
            //    dr["shopId"] = dt_page.Rows[i]["shopId"];
            //    dr["preOrder19dianId"] = dt_page.Rows[i]["preOrder19dianId"].ToString();
            //    dr["isShopConfirmed"] = dt_page.Rows[i]["isShopConfirmed"];
            //    dr["prePaidSum"] = dt_page.Rows[i]["prePaidSum"];
            //    dr["prePayTime"] = dt_page.Rows[i]["prePayTime"];
            //    dr["UserName"] = dt_page.Rows[i]["UserName"];
            //    dr["mobilePhoneNumber"] = dt_page.Rows[i]["mobilePhoneNumber"];
            //    dr["refundMoneySum"] = dt_page.Rows[i]["refundMoneySum"];
            //    dr["orderId"] = dt_page.Rows[i]["orderId"];
            //    dtclone.Rows.Add(dr);
            //}

            json = Common.ConvertDateTableToJson(dt_page);
            if (!String.IsNullOrEmpty(json))
            {
                json = json.TrimEnd('}');
                json += ",\"total\":[{\"totalAmount\":\"" + refundAmount + "\"},{\"ocount\":" + ocount + "},{\"payTotalAmount\":\"" + payTotalAmount + "\"}]}";
            }
        }
        return json;
    }
    /// <summary>
    /// 收银宝退款
    /// </summary>
    /// <param name="refundAccount">退款金额</param>
    /// <param name="refundDes">退款原因</param>
    /// <param name="preOrder19dianId">点单流水号</param>
    /// <returns></returns>
    [WebMethod]
    public static string OrderRefundOperate(string refundAccount, string refundDes, Guid preOrder19dianId)
    {
        if (HttpContext.Current.Session["MerchantsTreasureUserInfo"] == null)
        {
            return "-1000";
        }
        return SybMoneyCustomerOperate.OrderRefundOperate(Common.ToDouble(refundAccount), refundDes, preOrder19dianId);
    }
    /// <summary>
    /// 查询当前点单可退最大金额
    /// </summary>
    /// <param name="preOrder19dianId"></param>
    /// <returns></returns>
    [WebMethod]
    public static string QueryCanRefundAccount(Guid preOrder19dianId)
    {
        if (HttpContext.Current.Session["MerchantsTreasureUserInfo"] == null)
        {
            return "-1000";
        }
        //string转换为double类型，在转换为string类型
        double canRefundAccount = 0;
        CouponManager couponManager = new CouponManager();
        PreOrder19dianManager preOrder19dianManager = new PreOrder19dianManager();
        DataTable dtPreOrder = preOrder19dianManager.SybSelectPreOrder(preOrder19dianId);
        if (dtPreOrder != null && dtPreOrder.Rows.Count > 0)
        {
            canRefundAccount = Math.Round(Common.ToDouble(dtPreOrder.Rows[0]["prePaidSum"]) - Common.ToDouble(dtPreOrder.Rows[0]["refundMoneySum"]), 2);
        }
        return Common.ToString(canRefundAccount);
    }
}
