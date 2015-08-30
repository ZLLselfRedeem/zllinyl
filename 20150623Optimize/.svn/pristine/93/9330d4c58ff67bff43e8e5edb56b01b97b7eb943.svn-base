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

public partial class CompanyPages_preOrderShopVerified : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }
    /// <summary>
    ///对账操作（暂时收银宝没有取消对账操作）
    /// </summary>
    /// <param name="preOrder19dianIdStr">点单流水号</param>
    /// <param name="statusFlag">对账，未对账</param>
    /// <param name="shopId"></param>
    /// <returns></returns>
    [WebMethod]
    public static string ShopVerifiedOperate(string preOrder19dianIdStr, int statusFlag, int shopId)
    {
        if (HttpContext.Current.Session["MerchantsTreasureUserInfo"] == null)
        {
            return "-1000";
        }
        #region 调试数据
        /// preOrder19dianIdStr 财务对账当前点单 "1234"
        /// preOrder19dianIdStr 财务批量对账 "1234，12345，2563，5652...1224,1255"
        /// preOrder19dianIdStr 财务全部对账 "allPreorderVerified"
        #endregion
        #region 只能操作对账不能操作取消对账
        if (preOrder19dianIdStr.IndexOf("-") != -1)
        {
            Guid orderId = new Guid(preOrder19dianIdStr);
            preOrder19dianIdStr = string.Empty;
            PreOrder19dianOperate poo = new PreOrder19dianOperate();
            List<PreOrder19dianInfo> list = poo.GetPreOrder19dianByOrderIdNew(orderId);
            PreOrder19dianInfo info = list[0];
            preOrder19dianIdStr = info.preOrder19dianId.ToString();
            //foreach (PreOrder19dianInfo info in list)
            //{
            //    //preOrder19dianIdStr += info.preOrder19dianId + ",";
            //    if ((int)info.OrderType == OrderType.Normal)
            //    {
            //        preOrder19dianIdStr = info.preOrder19dianId.ToString();
            //        break;
            //    }
            //}
            //if (preOrder19dianIdStr.Length > 0)
            //{
            //    preOrder19dianIdStr = preOrder19dianIdStr.Substring(0, preOrder19dianIdStr.Length - 1);
            //}
        }
        if (statusFlag == 1)//只能操作对账不能操作取消对账
        {
            int employeeID = ((VAEmployeeLoginResponse)HttpContext.Current.Session["MerchantsTreasureUserInfo"]).employeeID;
            string result = "0";
            SybMoneyMerchantOperate smmo = new SybMoneyMerchantOperate();
            DataTable dtpreOrder19dian = new DataTable();
            PreOrder19dianOperate preOrder19dianOperate = new PreOrder19dianOperate();
            if (preOrder19dianIdStr.Trim() != "")
            {
                # region 批量对账
                if (preOrder19dianIdStr.Contains(","))//批量对账
                {
                    try
                    {
                        string[] intPreOrder19dianId = preOrder19dianIdStr.Split(new char[1] { ',' });
                        if (intPreOrder19dianId.Length > 0)
                        {
                            int count = 0;
                            for (int i = 0; i < intPreOrder19dianId.Length; i++)
                            {
                                long preOrder19dianId = Common.ToInt64(intPreOrder19dianId[i]);
                                smmo.ApproveMoneyMerchantNew(preOrder19dianId, employeeID);

                                //Guid orderId = Guid.Parse(intPreOrder19dianId[i]);
                                //smmo.ApproveMoneyMerchantByOrderId(orderId, employeeID);
                            }
                            if (count == intPreOrder19dianId.Length)
                            {
                                result = "1";
                            }
                            else
                            {
                                return "0";
                            }
                        }
                    }
                    catch (Exception)
                    {
                        result = "0";
                    }
                }
                #endregion
                #region 全部对账
                if (preOrder19dianIdStr.Trim() == "allPreorderVerified")
                {
                    DataTable dt = preOrder19dianOperate.QueryPreOrder19dianId(shopId);//当前公司当前门店所有未对账的单子
                    if (dt.Rows.Count > 0)
                    {
                        int count = 0;
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            long preOrder19dianId = Common.ToInt64(dt.Rows[i]["preOrder19dianId"]);
                            smmo.ApproveMoneyMerchantNew(preOrder19dianId, employeeID);

                            //Guid orderId = Guid.Parse(dt.Rows[i]["orderId"].ToString());
                            //smmo.ApproveMoneyMerchantByOrderId(orderId, employeeID);

                            count++;
                        }
                        if (count == dt.Rows.Count)
                        {
                            result = "1";
                        }
                        else
                        {
                            return "0";
                        }
                    }
                    else
                    {
                        return "0";
                    }
                }
                #endregion
                # region 单个对账
                if (preOrder19dianIdStr.Trim() != "allPreorderVerified" && !preOrder19dianIdStr.Contains(","))//表示对账的只有一项
                {
                    long preOrder19dianId = Common.ToInt64(preOrder19dianIdStr);
                    smmo.ApproveMoneyMerchantNew(preOrder19dianId, employeeID);
                    
                    //Guid orderId = Guid.Parse(preOrder19dianIdStr);
                    //smmo.ApproveMoneyMerchantByOrderId(orderId, employeeID);
                }
                #endregion
            }
            return result;//
        }
        #endregion
        else
        {
            return "0";
        }
    }

    /// <summary>
    /// 显示页面对账或者未对账或者全部的列表信息
    /// </summary>
    /// <param name="shopId">门店Id</param>
    /// <param name="PageSize">每页多少条</param>
    /// <param name="PageIndex">第几页</param>
    /// <param name="inputTextStr">查询文本框的值，初始化传“”</param>
    /// <param name="approvedStatus">已对账，未对账，全部</param>
    /// <param name="preOrderTimeStr">时间选择框，开始时间</param>
    /// <param name="preOrderTimeEnd">结束时间</param>
    /// <returns></returns>
    [WebMethod]
    public static string ShowPageVerifiedInfo(int PageSize, int PageIndex, int shopId, string inputTextStr, int approvedStatus, string preOrderTimeStr, string preOrderTimeEnd,int couponType)
    {
        if (HttpContext.Current.Session["MerchantsTreasureUserInfo"] == null)
        {
            return "-1000";
        }
        StringBuilder strBuilder = new StringBuilder();
        double refundAmount = 0, payTotalAmount = 0;
        DataTable dTable = MerchantInfoOperate.GetSybVerifiedOrder(shopId, inputTextStr, approvedStatus, preOrderTimeStr, preOrderTimeEnd, couponType);
        if (dTable == null || dTable.Rows.Count <= 0)
        {
            return "";
        }

        Guid[] orderIds = new Guid[dTable.Rows.Count];
        for (int i = 0; i < dTable.Rows.Count; i++)
        {
            orderIds[i] =(Guid)dTable.Rows[i]["orderId"];//找出所有总订单号
        }
        OrderOperate orderOperate = new OrderOperate();
        List<OrderPaidDetail> orderPaidDetails = orderOperate.GetOrderPaidList(orderIds);
        if (orderPaidDetails != null && orderPaidDetails.Any())
        {
            foreach (OrderPaidDetail item in orderPaidDetails)
            {
                for (int i = 0; i < dTable.Rows.Count; i++)
                {
                    if (item.id.Equals((Guid)dTable.Rows[i]["orderId"]))//找到匹配项
                    {
                        dTable.Rows[i]["prePaidSum"] = Math.Round(item.PreOrderServerSum - item.VerifiedSaving, 2);
                        dTable.Rows[i]["refundMoneySum"] = item.refundMoneySum;
                        //dTable.Rows[i]["preOrder19dianId"] = item.id.ToString();
                        break;
                    }
                }
            }
        }
        
        int ocount = dTable.Rows.Count;
        string json = "";
        if (ocount > 0)
        {
            payTotalAmount = Common.ToDouble(dTable.Compute("sum(prePaidSum)", "1=1"));
            refundAmount = Common.ToDouble(dTable.Compute("sum(refundMoneySum)", "1=1"));

            DataTable dt_page = Common.GetPageDataTable(dTable, (PageIndex - 1) * PageSize, PageIndex * PageSize);
            json = Common.ConvertDateTableToJson(dt_page);
        }
        if (json != null && json != "")
        {
            json = json.TrimEnd('}');
            json += ",\"total\":[{\"totalAmount\":\"" + refundAmount + "\"},{\"ocount\":" + ocount + "},{\"payTotalAmount\":\"" + payTotalAmount + "\"}]}";
        }
        return json;
    }
}