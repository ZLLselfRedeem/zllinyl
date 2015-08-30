﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Transactions;
using System.Configuration;
using System.Threading;
using LogDll;
using Microsoft.VisualBasic.Logging;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL;
using VAGastronomistMobileApp.SQLServerDAL.Persistence;
using VAGastronomistMobileApp.TheThirdPartyPaymentDll;
using System.Web;
using System.Xml;
using System.IO;
using VA.AllNotifications;
using Autofac.Integration.Web;
using Castle.Components.DictionaryAdapter;
using VAGastronomistMobileApp.WebPageDll.Services;
//
//  Copyright 2011 View Alloc inc. All rights reserved.
//  Created by Jason Xiao on 2012-04-10.
//
using VAGastronomistMobileApp.WebPageDll.Services.Infrastructure;
using VAGastronomistMobileApp.WebPageDll.ThreadCallBacks;
using VAGastronomistMobileApp.Model.Interface;
using VA.CacheLogic.OrderClient;
using VA.CacheLogic;
using VAGastronomistMobileApp.Model.QueryObject;
using System.Collections;

namespace VAGastronomistMobileApp.WebPageDll
{
    public partial class PreOrder19dianOperate
    {
        private static Object lotteryLock = new Object();

        ///// <summary>
        ///// 判断优惠劵可用
        ///// </summary>
        ///// <param name="clientPreOrderRequest"></param>
        ///// <param name="originalPrice"></param>
        ///// <returns></returns>
        //private bool CouponIsValid(VAClientPreOrderAddRequest clientPreOrderRequest, double originalPrice)
        //{
        //    bool isValid = true;
        //    for (int i = 0; i < clientPreOrderRequest.couponList.Count; i++)
        //    {
        //        //clientPreOrderRequest.preorderId 预点单总价格 与 couponList 中优惠券 最低使用价格比较,如果 小于 优惠券的最低使用价格,不予使用
        //        long couponid = Common.ToInt64(Common.GetFieldValue("CustomerConnCoupon", "couponID", "customerConnCouponID='" +
        //            clientPreOrderRequest.couponList[i].ToString() + "'"));
        //        if (getCouponUseValidation(couponid, Common.ToDouble(originalPrice)))//需改进，preorderId无用
        //        { }
        //        else
        //        {
        //            isValid = false;
        //            //clientPreOrderRequest.result = VAResult.VA_PREORDER_AMOUNT_NOTENOUGH; //点单金额<优惠券要求金额
        //        }
        //    }
        //    return isValid;
        //}
        ///// <summary>
        ///// 重构JSON,从数据库重新获取价格,防止客户端恶意提交数据
        ///// </summary>
        ///// <param name="clientPreOrderRequest"></param>
        ///// <param name="clientPreOrderResponse"></param>
        //private static void RestructureJson(VAClientPreOrderAddRequest clientPreOrderRequest, VAClientPreOrderAddResponse clientPreOrderResponse)
        //{
        //    List<PreOrderIn19dian> listOrderInfo = JsonOperate.JsonDeserialize<List<PreOrderIn19dian>>(clientPreOrderRequest.orderInJson);
        //    for (int i = 0; i < listOrderInfo.Count; i++)
        //    {
        //        listOrderInfo[i].unitPrice = Common.ToDouble(Common.GetFieldValue("DishPriceI18n,DishPriceInfo", "DishPriceInfo.DishPrice", "DishPriceI18n.DishPriceID =DishPriceInfo.DishPriceID and DishPriceI18n.DishPriceI18nID='" + listOrderInfo[i].dishPriceI18nId + "'"));
        //        if (listOrderInfo[i].dishIngredients != null && listOrderInfo[i].dishIngredients.Count > 0)
        //        {
        //            for (int j = 0; j < listOrderInfo[i].dishIngredients.Count; j++)
        //            {
        //                listOrderInfo[i].dishIngredients[j].ingredientsPrice = Common.ToDouble(Common.GetFieldValue("DishIngredients", "ingredientsPrice", "ingredientsId ='" + listOrderInfo[i].dishIngredients[j].ingredientsId + "'"));
        //            }
        //        }
        //    }
        //    clientPreOrderResponse.orderInJson = JsonOperate.JsonSerializer<List<PreOrderIn19dian>>(listOrderInfo);
        //    clientPreOrderRequest.orderInJson = clientPreOrderResponse.orderInJson;
        //}

        ///// <summary>
        ///// 此方法需改进
        ///// </summary>
        ///// <param name="preorderID"></param>
        ///// <param name="couponID"></param>
        ///// <param name="clientCalculatedSum"></param>
        ///// <returns></returns>
        //private bool getCouponUseValidation(long couponID, double clientCalculatedSum)
        //{
        //    //计算当前预点单的总价格 与 该 优惠券最低使用价格进行比较 返回是否大于= 优惠券最低使用价格
        //    //DataTable dt = preorder19dianMan.SelectPreOrderById(preorderID);
        //    double preorderSum = 0, couponLowprice;
        //    //if (dt.Rows.Count > 0)
        //    // {
        //    preorderSum = clientCalculatedSum;
        //    //}
        //    CouponManager cm = new CouponManager();
        //    couponLowprice = cm.GetLowPaymentByCouponID(couponID);

        //    return preorderSum >= couponLowprice;
        //}

        //public List<VACouponStatic> QueryCurrentListAtCoupon(long preorderId)
        //{
        //    DataTable dtHistoryUse = new DataTable();
        //    CouponManager CM = new CouponManager();
        //    //dtcoupon = CM.QueryCurrentListAtCoupon(preorderId);
        //    dtHistoryUse = CM.QueryCurrentListAtCouponNew(preorderId);


        //    List<VACouponStatic> couponHistoryUse = new List<VACouponStatic>();
        //    for (int i = 0; i < dtHistoryUse.Rows.Count; i++)
        //    {
        //        VACouponStatic couponli = new VACouponStatic();
        //        couponli.customerConnCouponID = Common.ToInt64(dtHistoryUse.Rows[i]["customerConnCouponID"]);
        //        couponli.CouponId = Common.ToInt64(dtHistoryUse.Rows[i]["couponID"]);
        //        couponli.CouponName = Common.ToString(dtHistoryUse.Rows[i]["couponName"]);
        //        couponli.CouponValue = 1;
        //        couponli.couponUseEndTime = Common.ToSecondFrom1970(Common.ToDateTime(dtHistoryUse.Rows[i]["couponValidEndTime"]));
        //        couponli.couponUseTimesBysame = Common.ToInt32(dtHistoryUse.Rows[i]["canUseNumberOnesOrder"]);
        //        couponli.ishistoryUse = true;
        //        couponHistoryUse.Add(couponli);
        //    }
        //    return couponHistoryUse;
        //}

        //public List<VACouponStatic> QueryCurrentListAtCouponUpdate(long preorderId)
        //{
        //    DataTable dtcoupon = new DataTable();
        //    CouponManager CM = new CouponManager();
        //    dtcoupon = CM.QueryCurrentListAtCouponUpdate(preorderId);

        //    List<VACouponStatic> coupon = new List<VACouponStatic>();
        //    for (int i = 0; i < dtcoupon.Rows.Count; i++)
        //    {
        //        VACouponStatic VS = new VACouponStatic();
        //        VS.CouponId = Common.ToInt64(Common.GetFieldValue("CustomerConnCoupon", "couponID", "customerConnCouponID ='" + dtcoupon.Rows[i]["customerConnCouponID"].ToString() + "'"));
        //        VS.CouponName = Common.GetFieldValue("CouponInfo", "couponName", "couponID ='" + VS.CouponId + "'");
        //        VS.CouponValue = 2;
        //        VS.customerConnCouponID = Common.ToInt64(dtcoupon.Rows[i]["customerConnCouponID"]);
        //        coupon.Add(VS);
        //    }
        //    return coupon;
        //}

        //public bool QueryIsUseDiffCoupon(List<long> couponList)
        //{
        //    if (couponList.Count > 1)
        //    {
        //        #region 拼接字符串 alist

        //        string alist = CommonPageOperate.SplicingListStr<long>(couponList, "");
        //        #endregion
        //        CouponManager couponM = new CouponManager();
        //        DataTable dtDiffCoupon = couponM.QueryIsUseDiffCoupon(alist);
        //        if (dtDiffCoupon != null && dtDiffCoupon.Rows.Count > 1)
        //        {
        //            return false;
        //        }
        //        else
        //        {
        //            return true;
        //        }

        //    }
        //    else
        //    {
        //        return true;
        //    }
        //}
        ///// <summary>
        ///// 使用优惠卷
        ///// </summary>
        ///// <param name="preOrder19dian"></param>
        ///// <param name="couponList"></param>
        ///// <returns></returns>
        //public bool UseCouPon(long customerId, List<long> couponList, long preorderId)
        //{
        //    bool result = true;
        //    using (TransactionScope scope = new TransactionScope())
        //    {
        //        CouponManager CM = new CouponManager();

        //        bool deleteOldFlag = true;
        //        if (couponList.Count > 0)
        //        {
        //            #region 拼接字符串 alist
        //            string alist = CommonPageOperate.SplicingListStr<long>(couponList, "");
        //            #endregion
        //            DataTable dtExist = CM.QuerySelectPersonCoupon(alist);

        //            if ((dtExist.Rows.Count > 0))
        //            {
        //                if (!CM.DelCustomerCouponPreOrderbylist(alist))
        //                {
        //                    deleteOldFlag = false;
        //                }
        //                else
        //                {
        //                    CM.DelCustomerCouponPreOrder(preorderId);
        //                }
        //            }
        //            //add couponList
        //            if (deleteOldFlag)
        //            {
        //                for (int n = 0; n < couponList.Count; n++)
        //                {
        //                    if (!CM.InsertPersonCoupon(couponList[n], customerId, preorderId))
        //                    {
        //                        result = false;
        //                        break;
        //                    }


        //                }
        //            }
        //        }
        //        else
        //        {

        //            //按照预点单删除数据
        //            CM.DelCustomerCouponPreOrder(preorderId);

        //        }
        //        scope.Complete();

        //    }
        //    return result;
        //}


        ///// <summary>
        ///// 使用优惠卷
        ///// </summary>
        ///// <param name="preOrder19dian"></param>
        ///// <param name="couponList"></param>
        ///// <returns></returns>
        //public bool UseCouPonUpdate(long customerId, List<long> couponList, long preorderId)
        //{
        //    bool result = true;
        //    using (TransactionScope scope = new TransactionScope())
        //    {
        //        CouponManager CM = new CouponManager();

        //        bool deleteOldFlag = true;
        //        if (couponList.Count > 0)
        //        {
        //            #region 拼接字符串 alist
        //            string alist = CommonPageOperate.SplicingListStr<long>(couponList, "");
        //            #endregion
        //            DataTable dtExist = CM.QuerySelectPersonCouponUpdate(alist);

        //            if ((dtExist.Rows.Count > 0))
        //            {
        //                if (!CM.DelCustomerCouponPreOrderbylistUpdate(alist))
        //                {
        //                    deleteOldFlag = false;
        //                }
        //                else
        //                {
        //                    CM.DelCustomerCouponPreOrderUpdate(preorderId);
        //                }
        //            }
        //            //add couponList
        //            if (deleteOldFlag)
        //            {
        //                for (int n = 0; n < couponList.Count; n++)
        //                {
        //                    if (!CM.InsertPersonCouponUpdate(couponList[n], customerId, preorderId))
        //                    {
        //                        result = false;
        //                        break;
        //                    }
        //                }
        //            }
        //        }
        //        else
        //        {
        //            //按照预点单删除数据
        //            CM.DelCustomerCouponPreOrderUpdate(preorderId);
        //        }
        //        scope.Complete();
        //    }
        //    return result;
        //}
        ///// <summary>
        ///// 处理优惠劵
        ///// </summary>
        ///// <param name="clientPreorderPrepayWithCreditRequest"></param>
        ///// <param name="customerId"></param>
        //private void UseCouponUpdate(VAClientPreorderPrepayWithCreditRequest clientPreorderPrepayWithCreditRequest, long customerId)
        //{
        //    long preorderId = clientPreorderPrepayWithCreditRequest.preorderId;
        //    CouponManager CM = new CouponManager();
        //    //查询Old优惠劵
        //    DataTable dtoldCustomerCoupon = CM.QueryCustomerCouponAndCouponId(preorderId);
        //    if (dtoldCustomerCoupon != null && dtoldCustomerCoupon.Rows.Count > 0)
        //    {
        //        for (int j = 0; j < dtoldCustomerCoupon.Rows.Count; j++)
        //        {
        //            CM.UpdateCustomerCouponStatus(Common.ToInt64(dtoldCustomerCoupon.Rows[j]["customerConnCouponID"]), VACustomerCouponStatus.NOT_USED);
        //        }
        //        CM.UpdateCouponDownloadQuantity(Common.ToInt64(dtoldCustomerCoupon.Rows[0]["couponID"]), (-1) * dtoldCustomerCoupon.Rows.Count);
        //        CM.UpdateCouponCurrentQuantity(Common.ToInt64(dtoldCustomerCoupon.Rows[0]["couponID"]), 1 * dtoldCustomerCoupon.Rows.Count);
        //    }
        //    CM.DelCustomerCouponPreOrder(preorderId);//删除old点单对应优惠劵
        //    DataTable dtnewCustomerCoupon = CM.QueryCustomerCouponAndCouponIdUpdate(preorderId);
        //    if (dtnewCustomerCoupon != null && dtnewCustomerCoupon.Rows.Count > 0)
        //    {
        //        for (int i = 0; i < dtnewCustomerCoupon.Rows.Count; i++)
        //        {
        //            //插入使用的优惠卷
        //            preorder19dianMan.InsertCustomerCouponPreOrder(customerId, Common.ToInt64(dtnewCustomerCoupon.Rows[i]["customerConnCouponID"]), preorderId);
        //            CM.UpdateCustomerCouponStatus(Common.ToInt64(dtnewCustomerCoupon.Rows[i]["customerConnCouponID"]), VACustomerCouponStatus.USED);
        //        }
        //        CM.UpdateCouponDownloadQuantity(Common.ToInt64(dtnewCustomerCoupon.Rows[0]["couponID"]), 1 * dtnewCustomerCoupon.Rows.Count);
        //        CM.UpdateCouponCurrentQuantity(Common.ToInt64(dtnewCustomerCoupon.Rows[0]["couponID"]), (-1) * dtnewCustomerCoupon.Rows.Count);
        //    }
        //}
        ///// <summary>
        ///// 推送
        ///// </summary>
        ///// <param name="checkResult"></param>
        ///// <param name="companyName"></param>
        ///// <param name="receiveCouponCount"></param>
        ///// <param name="payBackName"></param>
        ///// <param name="customerConCouponId"></param>
        //private static void PushMessage(CheckCookieAndMsgtypeInfo checkResult, string companyName, int receiveCouponCount, string payBackName, long customerConCouponId)
        //{
        //    string pushToken = Common.ToString(checkResult.dtCustomer.Rows[0]["pushToken"]);
        //    if (!string.IsNullOrEmpty(pushToken))
        //    {
        //        NotificationRecord notificationRecord = new NotificationRecord();
        //        notificationRecord.addTime = System.DateTime.Now;
        //        notificationRecord.appType = Common.ToInt32(checkResult.dtCustomer.Rows[0]["appType"]);
        //        notificationRecord.isLocked = false;
        //        notificationRecord.isSent = false;
        //        notificationRecord.pushToken = pushToken;
        //        notificationRecord.sendCount = 0;
        //        string message = ConfigurationManager.AppSettings["payOrderBackCouponMessage"];
        //        message = message.Replace("{0}", companyName);
        //        message = message.Replace("{1}", payBackName);
        //        message = message.Replace("{2}", receiveCouponCount.ToString());
        //        notificationRecord.message = message;
        //        notificationRecord.customType = (int)VANotificationsCustomType.NOTIFICATIONS_CUSTOM_PAYBACK; ;
        //        notificationRecord.customValue = customerConCouponId.ToString();
        //        Thread encourageThread = new Thread(Common.AddNotificationRecord);
        //        encourageThread.Start((object)notificationRecord);
        //    }
        //}
        ///// <summary>
        ///// 除余额外支付方式
        ///// </summary>
        ///// <param name="clientPreorderPrepayWithCreditRequest"></param>
        ///// <param name="clientPreorderPrepayWithCreditResponse"></param>
        ///// <param name="customerId"></param>
        ///// <param name="companyName"></param>
        ///// <param name="totalFee"></param>
        //private void UseOtherPayWay(VAClientPreorderPrepayWithCreditRequest clientPreorderPrepayWithCreditRequest, VAClientPreorderPrepayWithCreditResponse clientPreorderPrepayWithCreditResponse, long customerId, string companyName, double totalFee)
        //{
        //    using (TransactionScope scope = new TransactionScope())
        //    {
        //        if (preorder19dianMan.UpdatePreOrderPrePayPrivilegeId(clientPreorderPrepayWithCreditRequest.preorderId, clientPreorderPrepayWithCreditRequest.selectedPolicyId))
        //        {
        //            int payMode = 0;
        //            if (clientPreorderPrepayWithCreditRequest.preorderPayMode > 0)
        //            {
        //                payMode = clientPreorderPrepayWithCreditRequest.preorderPayMode;
        //            }
        //            switch (payMode)
        //            {
        //                default:
        //                case (int)VAClientPayMode.ALI_PAY_PLUGIN:
        //                case (int)VAClientPayMode.ALI_PAY_WEB:

        //                    #region 支付宝\网页形式\默认

        //                    AlipayOperate alipayOpe = new AlipayOperate();
        //                    AlipayOrderInfo alipayOrder = new AlipayOrderInfo();
        //                    string callBackUrl = WebConfig.ServerDomain + Config.Call_back_url;
        //                    alipayOrder.orderCreatTime = System.DateTime.Now;
        //                    alipayOrder.orderStatus = VAAlipayOrderStatus.NOT_PAID;
        //                    alipayOrder.totalFee = totalFee;
        //                    string subject = Common.GetEnumDescription(VAPayOrderType.PAY_PREORDER) + companyName;
        //                    if (subject.Length > 128)
        //                    {
        //                        subject = subject.Substring(0, 128);
        //                    }
        //                    alipayOrder.subject = subject;
        //                    alipayOrder.conn19dianOrderType = VAPayOrderType.PAY_PREORDER;
        //                    alipayOrder.connId = clientPreorderPrepayWithCreditRequest.preorderId;
        //                    alipayOrder.customerId = customerId;
        //                    long outTradeNo = alipayOpe.AddAlipayOrder(alipayOrder);
        //                    if (outTradeNo > 0)
        //                    {
        //                        scope.Complete();
        //                        clientPreorderPrepayWithCreditResponse.result = VAResult.VA_FAILED_MONEYREMAINED_NOT_ENOUGH;
        //                        if (payMode == (int)VAClientPayMode.ALI_PAY_PLUGIN)
        //                        {
        //                            //组装待签名数据
        //                            string alipayOrderString = "partner=" + "\"" + Config.Partner + "\"";
        //                            alipayOrderString += "&";
        //                            alipayOrderString += "seller=" + "\"" + Config.Partner + "\"";
        //                            alipayOrderString += "&";
        //                            alipayOrderString += "out_trade_no=" + "\"" + outTradeNo + "\"";
        //                            alipayOrderString += "&";
        //                            alipayOrderString += "subject=" + "\"" + subject + "\"";
        //                            alipayOrderString += "&";
        //                            alipayOrderString += "body=" + "\"" + companyName + "点单" + "\"";
        //                            alipayOrderString += "&";
        //                            alipayOrderString += "total_fee=" + "\"" + totalFee + "\"";
        //                            alipayOrderString += "&";
        //                            string notifyURL = WebConfig.ServerDomain + Config.QuickNotify_url;
        //                            alipayOrderString += "notify_url=" + "\"" + notifyURL + "\"";
        //                            string orderSign = RSAFromPkcs8.sign(alipayOrderString, Config.PrivateKey, Config.Input_charset_UTF8);
        //                            alipayOrderString += "&";
        //                            alipayOrderString += "sign=" + "\"" + HttpUtility.UrlEncode(orderSign, Encoding.GetEncoding(Config.Input_charset_UTF8)) + "\"";
        //                            alipayOrderString += "&";
        //                            alipayOrderString += "sign_type=\"RSA\"";
        //                            clientPreorderPrepayWithCreditResponse.alipayOrder = HttpUtility.UrlEncode(alipayOrderString, Encoding.GetEncoding(Config.Input_charset_UTF8));
        //                        }
        //                        else if (payMode == (int)VAClientPayMode.ALI_PAY_WEB)
        //                        {
        //                            string urlToContinuePayment = WebConfig.ServerDomain + "alipaytrade.aspx?type=" + (int)VAPayOrderType.PAY_PREORDER
        //                     + "&value=" + clientPreorderPrepayWithCreditRequest.preorderId + "&cookie=" + customerId + "&totalFee=" + totalFee + "&outTradeNo=" + outTradeNo;
        //                            clientPreorderPrepayWithCreditResponse.urlToContinuePayment = urlToContinuePayment;
        //                        }
        //                        else
        //                        {
        //                            string alipayOrderString = "partner=" + "\"" + Config.Partner + "\"";
        //                            alipayOrderString += "&";
        //                            alipayOrderString += "seller=" + "\"" + Config.Partner + "\"";
        //                            alipayOrderString += "&";
        //                            alipayOrderString += "out_trade_no=" + "\"" + outTradeNo + "\"";
        //                            alipayOrderString += "&";
        //                            alipayOrderString += "subject=" + "\"" + subject + "\"";
        //                            alipayOrderString += "&";
        //                            alipayOrderString += "body=" + "\"" + companyName + "点单" + "\"";
        //                            alipayOrderString += "&";
        //                            alipayOrderString += "total_fee=" + "\"" + totalFee + "\"";
        //                            alipayOrderString += "&";
        //                            string notifyURL = WebConfig.ServerDomain + Config.QuickNotify_url;
        //                            alipayOrderString += "notify_url=" + "\"" + notifyURL + "\"";
        //                            string orderSign = RSAFromPkcs8.sign(alipayOrderString, Config.PrivateKey, Config.Input_charset_UTF8);
        //                            alipayOrderString += "&";
        //                            alipayOrderString += "sign=" + "\"" + HttpUtility.UrlEncode(orderSign, Encoding.GetEncoding(Config.Input_charset_UTF8)) + "\"";
        //                            alipayOrderString += "&";
        //                            alipayOrderString += "sign_type=\"RSA\"";
        //                            clientPreorderPrepayWithCreditResponse.alipayOrder = HttpUtility.UrlEncode(alipayOrderString, Encoding.GetEncoding(Config.Input_charset_UTF8));
        //                            string urlToContinuePayment = WebConfig.ServerDomain + "alipaytrade.aspx?type=" + (int)VAPayOrderType.PAY_PREORDER
        //                     + "&value=" + clientPreorderPrepayWithCreditRequest.preorderId + "&cookie=" + customerId + "&totalFee=" + totalFee + "&outTradeNo=" + outTradeNo;
        //                            clientPreorderPrepayWithCreditResponse.urlToContinuePayment = urlToContinuePayment;
        //                        }
        //                    }

        //                    #endregion

        //                    break;
        //                case (int)VAClientPayMode.UNION_PAY_PLUGIN:

        //                    #region 银联支付
        //                    UnionPayOperate operate = new UnionPayOperate();
        //                    UnionPayInfo orderInfo = new UnionPayInfo();
        //                    orderInfo.merchantOrderTime = System.DateTime.Now;
        //                    orderInfo.orderStatus = VAAlipayOrderStatus.NOT_PAID;
        //                    orderInfo.conn19dianOrderType = VAPayOrderType.PAY_PREORDER;
        //                    orderInfo.merchantOrderAmt = (int)(totalFee * 100);
        //                    orderInfo.customerId = customerId;


        //                    string subjectTwo = Common.GetEnumDescription(VAPayOrderType.PAY_PREORDER) + companyName;
        //                    if (subjectTwo.Length > 128)
        //                    {
        //                        subjectTwo = subjectTwo.Substring(0, 128);
        //                    }
        //                    orderInfo.merchantOrderDesc = subjectTwo;
        //                    orderInfo.connId = clientPreorderPrepayWithCreditRequest.preorderId;
        //                    long payOrderId = operate.AddUnionpayOrder(orderInfo);
        //                    if (payOrderId > 0)
        //                    {
        //                        scope.Complete();
        //                        clientPreorderPrepayWithCreditResponse.result = VAResult.VA_FAILED_MONEYREMAINED_NOT_ENOUGH;

        //                        try
        //                        {
        //                            ////订单号,  从数据库中获取@@identity 自增长列
        //                            orderInfo.merchantOrderId = UnionPayParameters.UNION_PAY_FRONT_CODE + payOrderId;
        //                            StringBuilder originalInfo = new StringBuilder();
        //                            originalInfo
        //                                .Append("merchantName=").Append(UnionPayParameters.merchantName)
        //                                .Append("&merchantId=").Append(UnionPayParameters.merchantId)
        //                                .Append("&merchantOrderId=").Append(orderInfo.merchantOrderId)
        //                                .Append("&merchantOrderTime=").Append(orderInfo.merchantOrderTime.ToString("yyyyMMddHHmmss"))
        //                                .Append("&merchantOrderAmt=").Append(orderInfo.merchantOrderAmt)
        //                                .Append("&merchantOrderDesc=").Append(orderInfo.merchantOrderDesc)
        //                                .Append("&transTimeout=").Append(UnionPayParameters.transTimeout);

        //                            string originalsign = originalInfo.ToString();
        //                            string xmlSign = UnionPaySignEncode.CreateSign(originalsign, UnionPayParameters.alias, UnionPayParameters.password, UnionPayParameters.PrivatePath);

        //                            string Submit = SubmitString(orderInfo, xmlSign);
        //                            String returnContent = UnionPaySender.Send(UnionPayParameters.UNION_PAY_SERVER, Submit);

        //                            string decodeStr = HttpUtility.UrlDecode(returnContent);

        //                            XmlDocument xml = new XmlDocument();
        //                            xml.LoadXml(decodeStr);
        //                            string merchantId = xml.SelectSingleNode("/upomp/merchantId").InnerText;
        //                            string merchantOrderIdNew = xml.SelectSingleNode("/upomp/merchantOrderId").InnerText;
        //                            string merchantOrderTime = xml.SelectSingleNode("/upomp/merchantOrderTime").InnerText;
        //                            string resCode = xml.SelectSingleNode("/upomp/respCode").InnerText;
        //                            string resDesc = xml.SelectSingleNode("/upomp/respDesc").InnerText;
        //                            if (resCode == "0000")
        //                            {
        //                                StringBuilder builder = new StringBuilder();
        //                                builder
        //                                    .Append("merchantId=").Append(merchantId)
        //                                    .Append("&merchantOrderId=").Append(merchantOrderIdNew)
        //                                    .Append("&merchantOrderTime=").Append(merchantOrderTime);

        //                                string threeElement = builder.ToString();
        //                                string threeElementSign = UnionPaySignEncode.CreateSign(threeElement, UnionPayParameters.alias, UnionPayParameters.password, UnionPayParameters.PrivatePath);
        //                                //生成xml格式字符串
        //                                String unionPayOrder = "<?xml version=" + "'1.0' "
        //                                     + "encoding=" + "'utf-8' " + "standalone='yes'" + "?>" + "<upomp  application=" + "'LanchPay.Req' " + "version=" + "'1.0.0'" + ">"

        //                                     + "<merchantId>"
        //                                     + merchantId
        //                                     + "</merchantId>"

        //                                     + "<merchantOrderId>"
        //                                     + merchantOrderIdNew
        //                                     + "</merchantOrderId>"

        //                                     + "<merchantOrderTime>"
        //                                     + merchantOrderTime
        //                                     + "</merchantOrderTime>"

        //                                     + "<sign>"
        //                                     + threeElementSign
        //                                     + "</sign>" + "</upomp>";

        //                                clientPreorderPrepayWithCreditResponse.unionpayOrder = unionPayOrder;
        //                            }
        //                            else
        //                            {
        //                                clientPreorderPrepayWithCreditResponse.result = VAResult.VA_FAILED_UNION_PAY_ORDER_SUMMIT_ERROR;
        //                            }
        //                        }
        //                        catch
        //                        {

        //                        }
        //                    }
        //                    else
        //                    {
        //                        clientPreorderPrepayWithCreditResponse.result = VAResult.VA_FAILED_DB_ERROR;
        //                    }
        //                    #endregion

        //                    break;
        //            }
        //        }
        //        else
        //        {
        //            clientPreorderPrepayWithCreditResponse.result = VAResult.VA_FAILED_DB_ERROR;
        //        }
        //    }
        //}
        ///// <summary>
        ///// 更新预点单服务器计算金额和Vip节省金额
        ///// </summary>
        ///// <param name="clientPreorderPrepayWithCreditRequest"></param>
        ///// <param name="clientPreorderPrepayWithCreditResponse"></param>
        ///// <param name="customerId"></param>
        ///// <param name="companyId"></param>
        ///// <param name="vipMan"></param>
        ///// <param name="dtCustomerVip"></param>
        ///// <param name="estimatedSavingAndOrginal"></param>
        ///// <param name="prePaidSum"></param>
        ///// <param name="scope"></param>
        //private void UpdateSumAndVip(VAClientPreorderPrepayWithCreditRequest clientPreorderPrepayWithCreditRequest,
        //    VAClientPreorderPrepayWithCreditResponse clientPreorderPrepayWithCreditResponse, long customerId,
        //    int companyId, VipManager vipMan, DataTable dtCustomerVip,
        //    VAEstimatedSavingAndOrginal estimatedSavingAndOrginal, double prePaidSum, TransactionScope scope)
        //{
        //    if (preorder19dianMan.UpdatePreOrderServerSumAndSaving(clientPreorderPrepayWithCreditRequest.preorderId,
        //                    prePaidSum, estimatedSavingAndOrginal.estimatedSaving))
        //    {
        //        //更新数据库个人预点单状态
        //        CouponManager CM = new CouponManager();
        //        DataTable dtCM = new DataTable();
        //        dtCM = CM.GetcouponListbypreorderId(clientPreorderPrepayWithCreditRequest.preorderId);
        //        for (int i = 0; i < dtCM.Rows.Count; i++)
        //        {
        //            CM.UpdatePersonCouponState(Common.ToInt64(dtCM.Rows[i]["customerConnCouponID"]));
        //        }
        //        //更新预点单服务器计算金额和Vip节省金额
        //        ModifyCustomerVipUp(customerId, companyId, vipMan, dtCustomerVip, prePaidSum);//更新用户Vip信息
        //        scope.Complete();
        //        clientPreorderPrepayWithCreditResponse.result = VAResult.VA_OK;
        //    }
        //    else
        //    {
        //        clientPreorderPrepayWithCreditResponse.result = VAResult.VA_FAILED_DB_ERROR;
        //    }
        //}
        ///// <summary>
        ///// 新增预支付奖励信息
        ///// </summary>
        ///// <param name="prePayPrivilege"></param>
        ///// <returns></returns>
        //public int AddPrePayPrivilege(PrePayPrivilege prePayPrivilege)
        //{
        //    using (TransactionScope scope = new TransactionScope())
        //    {
        //        int prePayPrivilegeId = preorder19dianMan.InsertPrePayPrivilege(prePayPrivilege);
        //        if (prePayPrivilegeId > 0)
        //        {
        //            ShopManager shopMan = new ShopManager();
        //            if (shopMan.UpdateShopPrepayPrevilegeCount(prePayPrivilege.shopId, prePayPrivilege.supportPrePayCashBack, prePayPrivilege.supportPrePayVIPEntrance, prePayPrivilege.supportPrePayGift))
        //            {
        //                CompanyManager companyMan = new CompanyManager();
        //                int companyId = companyMan.SelectCompanyIdByShopId(prePayPrivilege.shopId);
        //                if (companyMan.UpdateCompanyPrepayPrevilegeCount(companyId, prePayPrivilege.supportPrePayCashBack, prePayPrivilege.supportPrePayVIPEntrance, prePayPrivilege.supportPrePayGift))
        //                {
        //                    scope.Complete();
        //                    return prePayPrivilegeId;
        //                }
        //                else
        //                {
        //                    return 0;
        //                }

        //            }
        //            else
        //            {
        //                return 0;
        //            }
        //        }
        //        else
        //        {
        //            return 0;
        //        }
        //    }
        //}
        ///// <summary>
        ///// 修改预支付奖励信息
        ///// </summary>
        ///// <param name="prePayPrivilege"></param>
        ///// <returns></returns>
        //public bool ModifyPrePayPrivilege(PrePayPrivilege prePayPrivilege)
        //{
        //    DataTable dtPrepayPrivilege = preorder19dianMan.SelectPrePayPrivilege(prePayPrivilege.id);
        //    int prepayCashBack = 0;
        //    if (prePayPrivilege.supportPrePayCashBack > Common.ToInt32(dtPrepayPrivilege.Rows[0]["supportPrePayCashBack"]))
        //    {//1,0
        //        prepayCashBack = 1;
        //    }
        //    else if (prePayPrivilege.supportPrePayCashBack == Common.ToInt32(dtPrepayPrivilege.Rows[0]["supportPrePayCashBack"]))
        //    {//1,1;0,0
        //        prepayCashBack = 0;
        //    }
        //    else
        //    {//0,1
        //        prepayCashBack = -1;
        //    }
        //    int prepayVIP = 0;
        //    if (prePayPrivilege.supportPrePayVIPEntrance > Common.ToInt32(dtPrepayPrivilege.Rows[0]["supportPrePayVIPEntrance"]))
        //    {//1,0
        //        prepayVIP = 1;
        //    }
        //    else if (prePayPrivilege.supportPrePayVIPEntrance == Common.ToInt32(dtPrepayPrivilege.Rows[0]["supportPrePayVIPEntrance"]))
        //    {//1,1;0,0
        //        prepayVIP = 0;
        //    }
        //    else
        //    {//0,1
        //        prepayVIP = -1;
        //    }
        //    int prepayGift = 0;
        //    if (prePayPrivilege.supportPrePayGift > Common.ToInt32(dtPrepayPrivilege.Rows[0]["supportPrePayGift"]))
        //    {//1,0
        //        prepayGift = 1;
        //    }
        //    else if (prePayPrivilege.supportPrePayGift == Common.ToInt32(dtPrepayPrivilege.Rows[0]["supportPrePayGift"]))
        //    {//1,1;0,0
        //        prepayGift = 0;
        //    }
        //    else
        //    {//0,1
        //        prepayGift = -1;
        //    }
        //    using (TransactionScope scope = new TransactionScope())
        //    {
        //        if (preorder19dianMan.UpdatePrePayPrivilege(prePayPrivilege))
        //        {
        //            ShopManager shopMan = new ShopManager();
        //            if (shopMan.UpdateShopPrepayPrevilegeCount(prePayPrivilege.shopId, prepayCashBack, prepayVIP, prepayGift))
        //            {
        //                CompanyManager companyMan = new CompanyManager();
        //                int companyId = companyMan.SelectCompanyIdByShopId(prePayPrivilege.shopId);
        //                if (companyMan.UpdateCompanyPrepayPrevilegeCount(companyId, prepayCashBack, prepayVIP, prepayGift))
        //                {
        //                    scope.Complete();
        //                    return true;
        //                }
        //                else
        //                {
        //                    return false;
        //                }
        //            }
        //            else
        //            {
        //                return false;
        //            }
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //}
        ///// <summary>
        ///// 修改预支付奖励状态
        ///// </summary>
        ///// <param name="prepayPrivilegeId"></param>
        ///// <param name="isValid"></param>
        ///// <returns></returns>
        //public bool SetPrepayPrivilegeStatus(int prepayPrivilegeId, int isValid)
        //{
        //    DataTable dtPrepayPrivilege = preorder19dianMan.SelectPrePayPrivilege(prepayPrivilegeId);
        //    int prepayCashBack = 0;
        //    int prepayVIP = 0;
        //    int prepayGift = 0;
        //    int shopId = Common.ToInt32(dtPrepayPrivilege.Rows[0]["shopId"]);
        //    if (isValid == 1)
        //    {
        //        prepayCashBack = Common.ToInt32(dtPrepayPrivilege.Rows[0]["supportPrePayCashBack"]);
        //        prepayVIP = Common.ToInt32(dtPrepayPrivilege.Rows[0]["supportPrePayVIPEntrance"]);
        //        prepayGift = Common.ToInt32(dtPrepayPrivilege.Rows[0]["supportPrePayGift"]);
        //    }
        //    else
        //    {
        //        prepayCashBack = -Common.ToInt32(dtPrepayPrivilege.Rows[0]["supportPrePayCashBack"]);
        //        prepayVIP = -Common.ToInt32(dtPrepayPrivilege.Rows[0]["supportPrePayVIPEntrance"]);
        //        prepayGift = -Common.ToInt32(dtPrepayPrivilege.Rows[0]["supportPrePayGift"]);
        //    }
        //    using (TransactionScope scope = new TransactionScope())
        //    {
        //        if (preorder19dianMan.UpdatePrePayPrivilegeStatus(prepayPrivilegeId, isValid))
        //        {
        //            ShopManager shopMan = new ShopManager();
        //            if (shopMan.UpdateShopPrepayPrevilegeCount(shopId, prepayCashBack, prepayVIP, prepayGift))
        //            {
        //                CompanyManager companyMan = new CompanyManager();
        //                int companyId = companyMan.SelectCompanyIdByShopId(shopId);
        //                if (companyMan.UpdateCompanyPrepayPrevilegeCount(companyId, prepayCashBack, prepayVIP, prepayGift))
        //                {
        //                    scope.Complete();
        //                    return true;
        //                }
        //                else
        //                {
        //                    return false;
        //                }
        //            }
        //            else
        //            {
        //                return false;
        //            }
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //}

        //#region 门店免返送
        ///// <summary>
        ///// 根据门店Id查询免返送信息
        ///// </summary>
        ///// <returns></returns>
        //public DataTable QueryFreeToReturnToSend(int shopId)
        //{
        //    return preorder19dianMan.SelectFreeToReturnToSend(shopId);
        //}
        ///// <summary>
        ///// 插入门店免返送信息
        ///// </summary>
        ///// <returns></returns>
        //public bool InsertFreeToReturnToSendInfo(int shopId, int descriptionType, string description)
        //{
        //    bool insertResult = false;
        //    insertResult = preorder19dianMan.InsertFreeToReturnToSendInfo(shopId, descriptionType, description);
        //    return insertResult;
        //}
        ///// <summary>
        ///// 修改门店免返送信息
        ///// </summary>
        ///// <returns></returns>
        //public bool ModifyFreeToReturnToSend(int shopId, int descriptionType, string description)
        //{
        //    return preorder19dianMan.UpdateFreeToReturnToSend(shopId, descriptionType, description);
        //}
        //#endregion

        ///// <summary>
        ///// 根据公司id，查询公司预付奖励信息
        ///// </summary>
        ///// <param name="companyId"></param>
        ///// <returns></returns>
        //public DataTable QueryCompanyPrepayPrivilege(int companyId)
        //{
        //    DataTable dtPrePayPrivilege = preorder19dianMan.SelectPrePayPrivilegeByCompany(companyId);
        //    return dtPrePayPrivilege;
        //}
        ///// <summary>
        ///// 根据店铺id，查询店铺预付奖励信息
        ///// </summary>
        ///// <param name="shopId"></param>
        ///// <returns></returns>
        //public DataTable QueryShopPrepayPrivilege(int shopId)
        //{
        //    DataTable dtPrePayPrivilege = preorder19dianMan.SelectPrePayPrivilegeByShop(shopId);
        //    return dtPrePayPrivilege;
        //}
        ///// <summary>
        ///// 审核预点单，在PreOrderCheckInfo中添加一条记录，然后修改PreOrder19dian中isApproved字段状态
        ///// </summary>
        ///// <param name="preOrder19dianId"></param>
        ///// <param name="approveTag">审核还是撤销审核</param>
        ///// <param name="employeeId">审核人</param>
        ///// <param name="employeeName">审核人姓名</param>
        ///// <param name="employeePosition">审核人职位</param>
        ///// <returns></returns>
        //public bool ApprovePreOrder(long preOrder19dianId, int approveTag, int employeeId, string employeeName, string employeePosition)
        //{
        //    using (TransactionScope scope = new TransactionScope())
        //    {
        //        bool resultInsetPreOrderCheckInfo = preorder19dianMan.InsertPreOrderCheckInfo(preOrder19dianId, approveTag, employeeId, employeeName, employeePosition);
        //        bool resultApprovePreOrder19dian = preorder19dianMan.ApprovePreOrder19dian(preOrder19dianId, approveTag);
        //        if (resultInsetPreOrderCheckInfo && resultApprovePreOrder19dian)
        //        {
        //            scope.Complete();
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //}

        ///// <summary>
        ///// 查询当前所有未对账的订单
        ///// </summary>
        ///// <returns></returns>
        //public DataTable QueryAllPreOrder19dianId()
        //{
        //    return preorder19dianMan.SelectAllPreOrder19dianId();
        //}

        ///// <summary>
        ///// 更新某个预点单的申请信息
        ///// </summary>
        ///// <param name="preOrder19dianId"></param>
        ///// <returns></returns>
        //public int QueryUpdateIsApplied(long PreOrder19dianId)
        //{
        //    return preorder19dianMan.UpdatePreOrderIsApplyFlag(PreOrder19dianId);
        //}
        ///// <summary>
        ///// web端更新预点单被支持的次数
        ///// </summary>
        ///// <param name="preorderId"></param>
        ///// <returns></returns>
        //public int WebUpdatePreorderSupportCount(long preorderId)
        //{
        //    using (TransactionScope scope = new TransactionScope())
        //    {
        //        if (preorder19dianMan.UpdatePreorderSupportCount(preorderId))
        //        {
        //            scope.Complete();
        //        }
        //    }
        //    return preorder19dianMan.SelectPreOrderSuportCount(preorderId);
        //}
        ///// <summary>
        ///// 获取公司的微博名称
        ///// </summary>
        ///// <param name="shopWeiboName"></param>
        ///// <param name="companyWeiboName"></param>
        ///// <param name="backupName"></param>
        ///// <returns></returns>
        //private string GetWeiboName(string shopWeiboName, string companyWeiboName, string backupName)
        //{
        //    if (!string.IsNullOrEmpty(shopWeiboName))
        //    {
        //        return shopWeiboName;
        //    }
        //    else if (!string.IsNullOrEmpty(companyWeiboName))
        //    {
        //        return companyWeiboName;
        //    }
        //    else
        //    {//取默认值
        //        return backupName;
        //    }
        //}
        ///// <summary>
        ///// 支付时 返现条件根据DishPriceInfo.backDiscountable，去判断哪些菜是能够计算在内的
        ///// </summary>
        ///// <param name="orderInJson"></param>
        ///// <param name="discount"></param>
        ///// <param name="preorderId"></param>
        ///// <returns></returns>
        //public double CalcPayBack(string orderInJson, double discount, long preorderId)
        //{
        //    if (discount > 0 && discount <= 1)
        //    {
        //        double SumCount = 0;
        //        double preorderVipDiscountableSum = 0;
        //        List<PreOrderIn19dian> listOrderInfo = JsonOperate.JsonDeserialize<List<PreOrderIn19dian>>(orderInJson);
        //        DishManager dishMan = new DishManager();
        //        double sumCoupon = 0;
        //        foreach (PreOrderIn19dian preorder in listOrderInfo)
        //        {
        //            if (dishMan.IsDishPayBackable(preorder.dishPriceI18nId))
        //            {
        //                if (dishMan.IsDishScaleVipDiscountable(preorder.dishPriceI18nId))
        //                {
        //                    preorderVipDiscountableSum += preorder.quantity * preorder.unitPrice;
        //                }
        //                SumCount += preorder.quantity * preorder.unitPrice;
        //            }

        //        }
        //        sumCoupon = CaleCouponCount(preorderId, preorderVipDiscountableSum);
        //        double paybacksum = SumCount - sumCoupon;
        //        if (SumCount < sumCoupon)
        //        {
        //            paybacksum = 0;
        //        }
        //        if (discount == 1)//vip折扣
        //        {
        //            return paybacksum;
        //        }
        //        else
        //        {
        //            return paybacksum * discount;
        //        }
        //    }
        //    else
        //    {
        //        return 0;

        //    }

        //}

        ///// <summary>
        ///// 根据优惠卷用户ID查询菜ID
        ///// </summary>
        ///// <param name="customerConnCouponID"></param>
        ///// <returns></returns>
        //private DataTable QueryDishid(long customerConnCouponID)
        //{
        //    string couponid = Common.GetFieldValue("CustomerConnCoupon", "couponID", "customerConnCouponID ='" + customerConnCouponID + "'");
        //    DataTable dishid = Common.GetDataTableFieldValue("CouponImageAndDish", "dishPriceI18nID", "couponId='" + couponid + "'");
        //    return dishid;
        //}
        ///// <summary>
        ///// 优惠卷抵价
        ///// </summary>
        ///// <param name="preorderId"></param>
        ///// <returns></returns>
        //private double CaleCouponCount(long preorderId, double orginalPriceSum)//服务器里面计算点单的总金额
        //{
        //    List<long> couponList = new List<long>();
        //    DataTable dtcouponList = new DataTable();
        //    CouponManager CM = new CouponManager();
        //    dtcouponList = CM.GetcouponListbypreorderId(preorderId);
        //    for (int i = 0; i < dtcouponList.Rows.Count; i++)
        //    {
        //        couponList.Add(Common.ToInt64(dtcouponList.Rows[i]["customerConnCouponID"]));
        //    }
        //    if (couponList.Count > 0)
        //    {
        //        double count = 0;
        //        double discount = 1;
        //        double priceAfterDiscount = orginalPriceSum * discount;//初始化折扣一下，其实此时priceAfterDiscount==原价
        //        if ((Common.QueryIsCouponCanUse(couponList)) && (Common.QueryIsCouponUsedbyAnybody(couponList)))
        //        {
        //            //计算开始
        //            DataTable dtAllcount = new DataTable();
        //            dtAllcount = CM.GetCountCouponAll(preorderId);
        //            for (int i = 0; i < dtAllcount.Rows.Count; i++)
        //            {
        //                //count = count + Common.ToInt64(dtAllcount.Rows[i]["discountedAmount"]);
        //                //wangcheng 2013/9/15 moedify 
        //                switch (Common.ToInt32(dtAllcount.Rows[i]["couponType"]))
        //                {
        //                    //折扣券
        //                    case (int)VACouponType.DISCOUNT_GENERAL_CAMPAIGN_TYPE://通用折扣券
        //                    case (int)VACouponType.DISCOUNT_DISH_CAMPAIGN_TYPE://特定折扣券
        //                        discount = Common.ToDouble(dtAllcount.Rows[i]["discount"]) / 10;//折扣优惠券折扣力度
        //                        count = orginalPriceSum - priceAfterDiscount * discount;//优惠=原价-折后价*折扣
        //                        priceAfterDiscount = priceAfterDiscount * discount;//下一次折扣原价=此时折扣价
        //                        break;
        //                    //抵价券
        //                    case (int)VACouponType.DEDUCT_GENERAL_CAMPAIGN_TYPE://通用抵价券
        //                    case (int)VACouponType.DEDUCT_DISH_CAMPAIGN_TYPE://特定抵价券
        //                    case (int)VACouponType.MEAL_SPECIAL_CAMPAIGN_TYPE://套餐特价券
        //                        count = count + Common.ToInt64(dtAllcount.Rows[i]["discountedAmount"]);
        //                        break;
        //                    //其他券
        //                    case (int)VACouponType.OTHER_COUPON_TYPE:
        //                        break;//暂无填充
        //                }
        //            }
        //        }
        //        return count;//返回优惠了多少钱
        //    }
        //    else return 0;

        //}

        ///// <summary>
        ///// 新增预支付奖励关联优惠券表信息
        ///// </summary>
        //public int AddPrePayPrivilegeConnCoupon(PrePayPrivilegeConnCouponInfo prePayPrivilegeConnCouponInfo)
        //{
        //    return preorder19dianMan.InsertPrePayPrivilegeConnCoupon(prePayPrivilegeConnCouponInfo);
        //}
        ///// <summary>
        ///// 修改预支付奖励关联优惠券表信息
        ///// </summary>
        //public bool ModifyPrePayPrivilegeConnCoupon(PrePayPrivilegeConnCouponInfo prePayPrivilegeConnCouponInfo)
        //{
        //    return preorder19dianMan.UpdatePrePayPrivilegeConnCoupon(prePayPrivilegeConnCouponInfo);
        //}
        // <summary>
        // 删除预支付奖励关联优惠券表信息
        // </summary>
        //public bool DeletePrePayPrivilegeConnCoupon(int prePayPrivilegeId)
        //{
        //    DataTable dt = Common.GetDataTableFieldValue("PrePayPrivilegeConnCoupon", "id", " prePayPrivilegeId='" + prePayPrivilegeId + "'");
        //    if (dt.Rows.Count == 0)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return preorder19dianMan.DeletePrePayPrivilegeConnCoupon(prePayPrivilegeId);
        //    }
        //}
        ///// <summary>
        ///// 增加用户会员日志
        ///// </summary>
        ///// <param name="customerVipLog"></param>
        ///// <returns></returns>
        //public static bool AddCustomerVipLog(CustomerVipLog customerVipLog)
        //{
        //    VipManager vipMan = new VipManager();
        //    if (vipMan.InsertCustomerVipLog(customerVipLog))
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}



        ///// <summary>
        ///// 判断点单编号是否存在
        ///// </summary>
        ///// <param name="preOrder19dian"></param>
        ///// <returns></returns>
        //public bool IsExists(long preOrder19dian)
        //{
        //    return preorder19dianMan.IsExists(preOrder19dian);
        //}
        ///// <summary>
        ///// 客户端微博分享预点单20140313
        ///// </summary>
        ///// <param name="clientSharePreOrderRequest"></param>
        ///// <returns></returns>
        //public VAClientSharePreOrderResponse ClientSharePreOrder(VAClientSharePreOrderRequest clientSharePreOrderRequest)
        //{
        //    VAClientSharePreOrderResponse clientSharePreOrderResponse = new VAClientSharePreOrderResponse();
        //    clientSharePreOrderResponse.type = VAMessageType.CLIENT_PREORDER_SHARE_RESPONSE;
        //    clientSharePreOrderResponse.cookie = clientSharePreOrderRequest.cookie;
        //    clientSharePreOrderResponse.uuid = clientSharePreOrderRequest.uuid;
        //    clientSharePreOrderResponse.preorderId = clientSharePreOrderRequest.preorderId;
        //    CheckCookieAndMsgtypeInfo checkResult = Common.CheckCookieAndMsgtype(clientSharePreOrderRequest.cookie,
        //        clientSharePreOrderRequest.uuid, (int)clientSharePreOrderRequest.type, (int)VAMessageType.CLIENT_PREORDER_SHARE_REQUEST);
        //    if (checkResult.result == VAResult.VA_OK)
        //    {
        //        if (clientSharePreOrderRequest.openIdVendors.Count > 0)
        //        {
        //            using (TransactionScope scope = new TransactionScope())
        //            {
        //                if (preorder19dianMan.UpdatePreOrderWeiboShareFlag(clientSharePreOrderRequest.preorderId))
        //                {
        //                    DateTime currentTime = System.DateTime.Now;
        //                    //插入分享记录表
        //                    for (int i = 0; i < clientSharePreOrderRequest.openIdVendors.Count; i++)
        //                    {
        //                        PreOrderWeiboShare preOrderWeiboShare = new PreOrderWeiboShare();
        //                        preOrderWeiboShare.openIdVendor = clientSharePreOrderRequest.openIdVendors[i];
        //                        preOrderWeiboShare.preorder19dianId = clientSharePreOrderRequest.preorderId;
        //                        preOrderWeiboShare.shareTime = currentTime;
        //                        preorder19dianMan.InsertPreOrderWeiboShare(preOrderWeiboShare);
        //                    }
        //                    scope.Complete();
        //                    clientSharePreOrderResponse.result = VAResult.VA_OK;
        //                }
        //                else
        //                {
        //                    clientSharePreOrderResponse.result = VAResult.VA_FAILED_DB_ERROR;
        //                }
        //            }
        //        }
        //        else
        //        {
        //            clientSharePreOrderResponse.result = VAResult.VA_FAILED_NOTHING_SHARED;
        //        }
        //    }
        //    else
        //    {
        //        clientSharePreOrderResponse.result = checkResult.result;
        //    }
        //    return clientSharePreOrderResponse;
        //}
        ///// <summary>
        ///// Web端查询预点单（已分享过的）
        ///// </summary>
        ///// <param name="webPreOrderQueryRequest"></param>
        ///// <returns></returns>
        //public VAWebPreOrderQueryResponse WebQueryPreOrder(VAWebPreOrderQueryRequest webPreOrderQueryRequest)
        //{
        //    VAWebPreOrderQueryResponse webPreOrderQueryResponse = new VAWebPreOrderQueryResponse();
        //    //if (preOrder19dianMan.IsPreOrderShared(webPreOrderQueryRequest.preorderId))//客户端上传时会重复，导致某些单子没有分享状态，所以暂时不判断这个
        //    if (true)
        //    {
        //        DataTable dtPreOrder = preorder19dianMan.SelectPreOrderAndCustomer(webPreOrderQueryRequest.preorderId);
        //        if (dtPreOrder.Rows.Count == 1)
        //        {
        //            webPreOrderQueryResponse.result = VAResult.VA_OK;
        //            string shopImagePath = Common.ToString(dtPreOrder.Rows[0]["shopImagePath"]);
        //            string shopLogo = Common.ToString(dtPreOrder.Rows[0]["shopLogo"]);
        //            if (!string.IsNullOrEmpty(shopImagePath) && !string.IsNullOrEmpty(shopLogo))
        //            {
        //                webPreOrderQueryResponse.companyLogoURL = WebConfig.CdnDomain + WebConfig.ImagePath + shopImagePath + shopLogo;
        //            }
        //            else
        //            {
        //                webPreOrderQueryResponse.companyLogoURL = WebConfig.ServerDomain + "Images/uxian.png";//取系统默认图片
        //            }
        //            webPreOrderQueryResponse.seqencingForCurrentWeek = 0;
        //            webPreOrderQueryResponse.seqencingForCurrentMonth = 0;
        //            if (webPreOrderQueryResponse.seqencingForCurrentMonth > 0.99)
        //            {
        //                webPreOrderQueryResponse.seqencingForCurrentMonth = 0.99;
        //            }
        //            if (webPreOrderQueryResponse.seqencingForCurrentWeek > 0.99)
        //            {
        //                webPreOrderQueryResponse.seqencingForCurrentWeek = 0.99;
        //            }
        //            webPreOrderQueryResponse.orderInJson = Common.ToString(dtPreOrder.Rows[0]["orderInJson"]);
        //            webPreOrderQueryResponse.preOrderTime = Common.ToString(dtPreOrder.Rows[0]["preOrderTime"]);
        //            webPreOrderQueryResponse.currentTime = Common.ToString(System.DateTime.Now);
        //            webPreOrderQueryResponse.customerName = Common.ToString(dtPreOrder.Rows[0]["CustomerFirstName"]) + Common.ToString(dtPreOrder.Rows[0]["CustomerLastName"]);
        //            webPreOrderQueryResponse.customerPhoneNumber = Common.ToString(dtPreOrder.Rows[0]["mobilePhoneNumber"]);
        //            webPreOrderQueryResponse.customerUserName = Common.ToString(dtPreOrder.Rows[0]["UserName"]);
        //            webPreOrderQueryResponse.eCardNumber = Common.ToString(dtPreOrder.Rows[0]["eCardNumber"]);
        //            webPreOrderQueryResponse.preorderSupportCount = Common.ToInt32(dtPreOrder.Rows[0]["preorderSupportCount"]);
        //            webPreOrderQueryResponse.prePayAmount = Common.ToDouble(dtPreOrder.Rows[0]["prePaidSum"]);
        //            webPreOrderQueryResponse.preorderAmount = Common.ToDouble(dtPreOrder.Rows[0]["preOrderServerSum"]);
        //            switch (Common.ToInt32(dtPreOrder.Rows[0]["CustomerSex"]))
        //            {
        //                case 1:
        //                    webPreOrderQueryResponse.customerSex = "男";
        //                    break;
        //                case 2:
        //                    webPreOrderQueryResponse.customerSex = "女";
        //                    break;
        //                default:
        //                    webPreOrderQueryResponse.customerSex = "不详";
        //                    break;
        //            }
        //            switch (Common.ToBool(dtPreOrder.Rows[0]["isVIP"]))
        //            {
        //                case true:
        //                    webPreOrderQueryResponse.isVIP = "是";
        //                    break;
        //                default:
        //                    webPreOrderQueryResponse.isVIP = "否";
        //                    break;
        //            }
        //        }
        //        else
        //        {
        //            webPreOrderQueryResponse.result = VAResult.VA_FAILED_PREORDER_NULL;
        //        }
        //    }
        //    //else
        //    //{
        //    //    webPreOrderQueryResponse.result = VAResult.VA_FAILED_PREORDER_NOT_SHARED;
        //    //}
        //    return webPreOrderQueryResponse;
        //}
        ///// <summary>
        ///// 更新Vip信息（-）
        ///// </summary>
        ///// <param name="companyId"></param>
        ///// <param name="vipMan"></param>
        ///// <param name="dtCustomerVip"></param>
        ///// <param name="preOrderServerSum"></param>
        //private static void ModifyCustomerVipDown(long customerId, int companyId, VipManager vipMan, DataTable dtCustomerVip, double preOrderServerSum)
        //{
        //    long currentCompanyVipId = Common.ToInt64(dtCustomerVip.Rows[0]["companyVipId"]);
        //    long customerCompanyVipId = Common.ToInt64(dtCustomerVip.Rows[0]["customerCompanyVipId"]);
        //    int currentCompanyVipSequence = Common.ToInt32(dtCustomerVip.Rows[0]["sequence"]);
        //    DataTable dtCompanyVip = vipMan.SelectCompanyVip(companyId, currentCompanyVipSequence, false);
        //    if (dtCompanyVip.Rows.Count > 0)
        //    {
        //        if (Common.ToInt32(dtCompanyVip.Rows[0]["nextRequirement"]) > (Common.ToInt32(dtCustomerVip.Rows[0]["userCompletedOrderCount"]) - 1))
        //        {
        //            long preCompanyVipId = Common.ToInt64(dtCompanyVip.Rows[0]["id"]);
        //            vipMan.UpdateCustomerCompanyVip(false, preOrderServerSum, preCompanyVipId, customerCompanyVipId);
        //            CustomerVipLog customerVipLog = new CustomerVipLog();
        //            customerVipLog.companyId = companyId;
        //            customerVipLog.customerId = customerId;
        //            customerVipLog.companyVipId = preCompanyVipId;
        //            customerVipLog.time = System.DateTime.Now;
        //            customerVipLog.isLevelUp = false;
        //            AddCustomerVipLog(customerVipLog);
        //        }
        //        else
        //        {
        //            vipMan.UpdateCustomerCompanyVip(false, preOrderServerSum, currentCompanyVipId, customerCompanyVipId);
        //        }
        //    }
        //    else
        //    {
        //        vipMan.UpdateCustomerCompanyVip(false, preOrderServerSum, currentCompanyVipId, customerCompanyVipId);
        //    }
        //}
        ///// <summary>
        ///// 更新用户Vip信息（+）
        ///// </summary>
        ///// <param name="customerId"></param>
        ///// <param name="companyId"></param>
        ///// <param name="dtCustomerVip"></param>
        ///// <param name="preOrderServerSum"></param>
        //private static void ModifyCustomerVipUp(long customerId, int companyId, VipManager vipMan, DataTable dtCustomerVip, double preOrderServerSum)
        //{
        //    DateTime currentTime = System.DateTime.Now;
        //    if (dtCustomerVip.Rows.Count == 1)
        //    {
        //        long currentCompanyVipId = Common.ToInt64(dtCustomerVip.Rows[0]["companyVipId"]);
        //        long customerCompanyVipId = Common.ToInt64(dtCustomerVip.Rows[0]["customerCompanyVipId"]);
        //        int currentCompanyVipSequence = Common.ToInt32(dtCustomerVip.Rows[0]["sequence"]);
        //        int currentDayCount = Common.ToInt32(dtCustomerVip.Rows[0]["currentDayCount"]);
        //        DateTime lastOrderTime = Common.ToDateTime(dtCustomerVip.Rows[0]["lastOrderTime"]);
        //        if ((Common.ToInt32(dtCustomerVip.Rows[0]["userCompletedOrderCount"]) + 1) >= Common.ToInt32(dtCustomerVip.Rows[0]["nextRequirement"])
        //            && (DateTime.Compare(lastOrderTime.Date, currentTime.Date) != 0
        //            || (DateTime.Compare(lastOrderTime.Date, currentTime.Date) == 0 && currentDayCount < 2)))
        //        {
        //            DataTable dtCompanyVip = vipMan.SelectCompanyVip(companyId, currentCompanyVipSequence, true);
        //            if (dtCompanyVip.Rows.Count > 0)
        //            {
        //                long nextCompanyVipId = Common.ToInt64(dtCompanyVip.Rows[0]["id"]);
        //                vipMan.UpdateCustomerCompanyVip(true, preOrderServerSum, nextCompanyVipId, customerCompanyVipId);
        //                CustomerVipLog customerVipLog = new CustomerVipLog();
        //                customerVipLog.companyId = companyId;
        //                customerVipLog.customerId = customerId;
        //                customerVipLog.companyVipId = nextCompanyVipId;
        //                customerVipLog.time = currentTime;
        //                customerVipLog.isLevelUp = true;
        //                AddCustomerVipLog(customerVipLog);
        //            }
        //            else
        //            {
        //                vipMan.UpdateCustomerCompanyVip(true, preOrderServerSum, currentCompanyVipId, customerCompanyVipId);
        //            }
        //        }
        //        else
        //        {
        //            vipMan.UpdateCustomerCompanyVip(true, preOrderServerSum, currentCompanyVipId, customerCompanyVipId);
        //        }
        //    }
        //    else
        //    {
        //        DataTable dtCompanyVip = vipMan.SelectCompanyVip(companyId);
        //        for (int i = 0; i < dtCompanyVip.Rows.Count; i++)
        //        {
        //            if (1 < Common.ToInt32(dtCompanyVip.Rows[i]["nextRequirement"]))
        //            {
        //                CustomerVipInfo customerVip = new CustomerVipInfo();
        //                customerVip.companyId = companyId;
        //                customerVip.companyVipId = Common.ToInt64(dtCompanyVip.Rows[i]["id"]);
        //                customerVip.customerId = customerId;
        //                customerVip.userTotalOrderAmount = preOrderServerSum;
        //                customerVip.lastOrderTime = System.DateTime.Now;
        //                vipMan.InsertCustomerVip(customerVip);
        //                CustomerVipLog customerVipLog = new CustomerVipLog();
        //                customerVipLog.companyId = companyId;
        //                customerVipLog.customerId = customerId;
        //                customerVipLog.companyVipId = customerVip.companyVipId;
        //                customerVipLog.time = currentTime;
        //                customerVipLog.isLevelUp = true;
        //                AddCustomerVipLog(customerVipLog);
        //                break;
        //            }
        //        }
        //    }
        //}

        private readonly PreOrder19dianManager preorder19dianMan = new PreOrder19dianManager();
        /// <summary>
        /// 查询POSLite最新版本信息
        /// </summary>
        /// <returns></returns>
        public VALatestPOSLiteInfo QueryPOSLiteLatestInfo(int type = 0)
        {
            VALatestPOSLiteInfo latestPOSLiteInfo = new VALatestPOSLiteInfo();
            DataTable posliteVersion = preorder19dianMan.SelectPOSLiteVersion(type);
            if (posliteVersion.Rows.Count == 1)
            {
                latestPOSLiteInfo.POSLiteVersion = Common.ToString(posliteVersion.Rows[0]["POSLiteVersion"]);
                latestPOSLiteInfo.downloadURL = Common.ToString(posliteVersion.Rows[0]["downloadURL"]);
                latestPOSLiteInfo.POSLiteUpdatePackageName = Common.ToString(posliteVersion.Rows[0]["POSLiteUpdatePackageName"]);
            }
            return latestPOSLiteInfo;
        }

        /// <summary>
        /// 客户端查询预点单20140313
        /// </summary>
        /// <param name="clientPreOrderQueryRequest"></param>
        /// <returns></returns>
        public VAClientPreOrderQueryResponse ClientQueryPreOrder(VAClientPreOrderQueryRequest clientPreOrderQueryRequest)
        {
            VAClientPreOrderQueryResponse clientPreOrderQueryResponse = new VAClientPreOrderQueryResponse();
            clientPreOrderQueryResponse.type = VAMessageType.CLIENT_PREORDER_QUERY_RESPONSE;
            clientPreOrderQueryResponse.cookie = clientPreOrderQueryRequest.cookie;
            clientPreOrderQueryResponse.uuid = clientPreOrderQueryRequest.uuid;
            CheckCookieAndMsgtypeInfo checkResult = Common.CheckCookieAndMsgtype(clientPreOrderQueryRequest.cookie, clientPreOrderQueryRequest.uuid, (int)clientPreOrderQueryRequest.type, (int)VAMessageType.CLIENT_PREORDER_QUERY_REQUEST, false);
            if (checkResult.result == VAResult.VA_OK)
            {
                DataTable dtPreOrderAndCompany = null;
                if (Common.CheckLatestBuild_201507(clientPreOrderQueryRequest.appType, clientPreOrderQueryRequest.clientBuild))
                    dtPreOrderAndCompany = preorder19dianMan.GetOrderIdToPreOrderAndCompany(clientPreOrderQueryRequest.orderId);
                else
                    dtPreOrderAndCompany = preorder19dianMan.SelectPreOrderAndCompany(clientPreOrderQueryRequest.preorderId);

                if (dtPreOrderAndCompany.Rows.Count == 1)
                {
                    clientPreOrderQueryResponse.result = VAResult.VA_OK;
                    long customerId = Common.ToInt64(checkResult.dtCustomer.Rows[0]["CustomerID"]);
                    var mealOrderIds = new MealOperate().GetMealOrderIds(customerId);
                    double executedRedEnvelopeAmount = 0;
                    if (Common.CheckLatestBuild_August(clientPreOrderQueryRequest.appType, clientPreOrderQueryRequest.clientBuild))
                    {
                        executedRedEnvelopeAmount
                            = new RedEnvelopeOperate().QueryCustomerExcutedRedEnvelope(checkResult.dtCustomer.Rows[0]["mobilePhoneNumber"].ToString());
                    }
                    double money19DianRemained = Common.ToDouble(checkResult.dtCustomer.Rows[0]["money19dianRemained"]);
                    int currentPlatformVipGrade = Common.ToInt32(checkResult.dtCustomer.Rows[0]["currentPlatformVipGrade"]);

                    bool isNewBuild = Common.CheckLatestBuild_January(clientPreOrderQueryRequest.appType, clientPreOrderQueryRequest.clientBuild);
                    bool isNewBuildAnother = Common.CheckLatestBuild_February(clientPreOrderQueryRequest.appType, clientPreOrderQueryRequest.clientBuild);
                    bool expireRedEnvelopeBuild = Common.CheckExpireRedEnvelopeBuild_February(clientPreOrderQueryRequest.appType, clientPreOrderQueryRequest.clientBuild);

                    var orderDetailConfig = isNewBuild == true ? new ClientOrderDetailConfigOperate().GetClientOrderDetailConfig() : new List<ClientOrderDetailConfig>();
                    string orderDetailConfigDesc = "";
                    if (orderDetailConfig.Any())
                    {
                        orderDetailConfigDesc = orderDetailConfig[0].Description;
                    }
                    string mobilePhoneNumber = Common.ToString(checkResult.dtCustomer.Rows[0]["mobilePhoneNumber"]);
                    clientPreOrderQueryResponse.preorderDetail =
                        FillPreorderDetail(isNewBuild, isNewBuildAnother, expireRedEnvelopeBuild, orderDetailConfigDesc, mealOrderIds, dtPreOrderAndCompany.Rows[0], executedRedEnvelopeAmount, money19DianRemained, false, mobilePhoneNumber, currentPlatformVipGrade, clientPreOrderQueryRequest.appType, clientPreOrderQueryRequest.clientBuild);


                    if (Common.CheckLatestBuild_201507(clientPreOrderQueryRequest.appType, clientPreOrderQueryRequest.clientBuild))
                    {
                        List<OrderPayMode> payModes = new List<OrderPayMode>();//点单详细支付名称和金额   
                        payModes = new ZZBPreOrderOperate().GetOrderPayModeInfos(Common.ToDouble(dtPreOrderAndCompany.Rows[0]["prePaidSum"]), true, clientPreOrderQueryRequest.orderId, (VAPreorderStatus)dtPreOrderAndCompany.Rows[0]["status"]);
                        clientPreOrderQueryResponse.preorderDetail.payModeList = payModes;
                        clientPreOrderQueryResponse.preorderDetail.compensatePrice = Common.ToDouble(new PreOrder19dianManager().GetCompensatePrice(clientPreOrderQueryRequest.orderId));
                        clientPreOrderQueryResponse.preorderDetail.detailServerCalculatedSum += clientPreOrderQueryResponse.preorderDetail.compensatePrice;
                        clientPreOrderQueryResponse.preorderDetail.detailServerUxianPriceSum += clientPreOrderQueryResponse.preorderDetail.compensatePrice;
                        clientPreOrderQueryResponse.preorderDetail.isApproved = Convert.ToInt32(dtPreOrderAndCompany.Rows[0]["isApproved"] is DBNull ? "0" : dtPreOrderAndCompany.Rows[0]["isApproved"]);
                        clientPreOrderQueryResponse.preorderDetail.orderId = clientPreOrderQueryRequest.orderId;
                    }
                }
                else
                {
                    clientPreOrderQueryResponse.result = VAResult.VA_FAILED_PREORDER_NOT_FOUND;
                }
            }
            else
            {
                clientPreOrderQueryResponse.result = checkResult.result;
            }
            return clientPreOrderQueryResponse;
        }

        /// <summary>
        /// 客户端查询预点单列表
        /// </summary>
        /// <param name="clientPreorderListRequest"></param>
        /// <returns></returns>
        public VAClientPreorderListResponse ClientQueryPreOrderList(VAClientPreorderListRequest clientPreorderListRequest)
        {
            VAClientPreorderListResponse clientPreorderListResponse = new VAClientPreorderListResponse();
            clientPreorderListResponse.type = VAMessageType.CLIENT_PREORDER_LIST_RESPONSE;
            clientPreorderListResponse.cookie = clientPreorderListRequest.cookie;
            clientPreorderListResponse.uuid = clientPreorderListRequest.uuid;
            CheckCookieAndMsgtypeInfo checkResult = Common.CheckCookieAndMsgtype(clientPreorderListRequest.cookie, clientPreorderListRequest.uuid, (int)clientPreorderListRequest.type, (int)VAMessageType.CLIENT_PREORDER_LIST_REQUEST);
            if (checkResult.result == VAResult.VA_OK)
            {
                clientPreorderListResponse.result = VAResult.VA_OK;
                DataRow checkResult_dtCustomer = checkResult.dtCustomer.Rows[0];
                long customerID = Common.ToInt64(checkResult_dtCustomer["CustomerID"]);
                bool isHaveMore = false;
                //分页处理点单
                int pageIndex = clientPreorderListRequest.pageIndex <= 0 ? 1 : clientPreorderListRequest.pageIndex;
                int pageSize = clientPreorderListRequest.pageSize <= 0 ? 10000 : clientPreorderListRequest.pageSize;
                DataTable dtPreOrderAndCompany = preorder19dianMan.SelectPagingPreOrderList(customerID, pageIndex, pageSize, ref isHaveMore);
                List<VAPreorderDetail> preorderList = new List<VAPreorderDetail>();
                if (dtPreOrderAndCompany.Rows.Count > 0)
                {
                    int currentPlatformVipGrade = Common.ToInt32(checkResult_dtCustomer["currentPlatformVipGrade"]);//用户当前在平台VIP等级
                    //该情况，下面二个字段为参与计算
                    double executedRedEnvelopeAmount = Common.ToDouble(checkResult_dtCustomer["executedRedEnvelopeAmount"]);
                    double money19DianRemained = Common.ToDouble(checkResult_dtCustomer["money19dianRemained"]);
                    var mealOrderIds = new MealOperate().GetMealOrderIds(customerID);
                    bool isNewBuild = Common.CheckLatestBuild_January(clientPreorderListRequest.appType, clientPreorderListRequest.clientBuild);
                    //过多的版本兼容判断有点繁琐，待优化
                    bool isNewBuildAnother = Common.CheckLatestBuild_February(clientPreorderListRequest.appType, clientPreorderListRequest.clientBuild);
                    bool expireRedEnvelopeBuild = Common.CheckExpireRedEnvelopeBuild_February(clientPreorderListRequest.appType, clientPreorderListRequest.clientBuild);

                    string mobilePhoneNumber = Common.ToString(checkResult.dtCustomer.Rows[0]["mobilePhoneNumber"]);
                    foreach (DataRow item in dtPreOrderAndCompany.Rows)
                    {
                        var preorderDetail = FillPreorderDetail(isNewBuild, isNewBuildAnother, expireRedEnvelopeBuild, "", mealOrderIds, item, executedRedEnvelopeAmount, money19DianRemained, true, mobilePhoneNumber, currentPlatformVipGrade, clientPreorderListRequest.appType, clientPreorderListRequest.clientBuild);

                        preorderList.Add(preorderDetail);
                    }
                    clientPreorderListResponse.preorders = preorderList.OrderByDescending(p => p.preorderDate).ToList();//列表返回按照usedDateTime时间降序排列
                }
                else
                {
                    clientPreorderListResponse.preorders = preorderList;
                }
                clientPreorderListResponse.isHaveMore = isHaveMore;
            }
            else
            {
                clientPreorderListResponse.result = checkResult.result;
            }
            #region 查询预点单列表日志记录

            Thread thread = new Thread(Excuit);
            thread.IsBackground = true;
            thread.Start(VAInvokedAPIType.API_SELECT_PREORDER_LIST);

            #endregion
            return clientPreorderListResponse;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public void Excuit(object value)
        {
            StatisticalStatementOperate sso = new StatisticalStatementOperate();
            sso.InsertInvokedAPILog((int)value);
        }

        /// <summary>
        /// 金额改变,同时改变用户、公司账户余额
        /// </summary>
        /// <param name="preOrder19dianId"></param>
        /// <param name="customerId"></param>
        /// <param name="companyId"></param>
        /// <param name="shopId"></param>
        /// <param name="prePaidSum"></param>
        /// <param name="result1"></param>
        public static void ChangeMoney(long preOrder19dianId, long customerId, int companyId, int shopId, double prePaidSum, ref bool result1)//, ref bool result2
        {
            Money19dianDetail money19dianDetail = new Money19dianDetail
            {
                customerId = customerId,
                changeReason = Common.GetEnumDescription(VA19dianMoneyChangeReason.MONEY19DIAN_PREPAY_PREORDER).Replace("{0}", preOrder19dianId.ToString()),
                changeTime = System.DateTime.Now,
                changeValue = -prePaidSum,
                remainMoney = SybMoneyCustomerOperate.GetCustomerRemainMoney(customerId) - prePaidSum,//当前余额
                flowNumber = SybMoneyOperate.CreateCustomerFlowNumber(customerId),//流水号
                accountType = (int)AccountType.USER_CONSUME,//来源类型，用户消费
                accountTypeConnId = Common.ToString(preOrder19dianId),
                inoutcomeType = (int)InoutcomeType.OUT,//支出
                companyId = companyId,
                shopId = shopId
            };
            if (money19dianDetail.changeValue > 0)
            {
                money19dianDetail.inoutcomeType = (int)InoutcomeType.IN;
            }
            result1 = SybMoneyCustomerOperate.AccountBalanceChanges(money19dianDetail);
            //公司余额变更，相对于公司，余额加钱      2014-5-6 去掉了
            //MoneyViewallocAccountDetail moneyViewallocAccountDetailModel = new MoneyViewallocAccountDetail
            //{
            //    accountMoney = prePaidSum,
            //    remainMoney = SybMoneyViewallocOperate.GetViewAllocRemainMoney() + prePaidSum,//获得公司当前时间账户余额
            //    accountType = (int)AccountType.USER_CONSUME,
            //    accountTypeConnId = Common.ToString(clientPreorderPrepayWithCreditRequest.preorderId),
            //    inoutcomeType = (int)InoutcomeType.IN,//收入
            //    operUser = "用户ID" + Common.ToString(customerId),
            //    operTime = DateTime.Now,
            //    outcomeCompanyId = companyId,
            //    outcomeShopId = shopId,
            //    remark = "用户支付点单"
            //};
            //if (moneyViewallocAccountDetailModel.accountMoney < 0)
            //{
            //    moneyViewallocAccountDetailModel.inoutcomeType = (int)InoutcomeType.OUT;
            //}
            //result2 = SybMoneyViewallocOperate.ViewAllocAccountBalanceChanges(moneyViewallocAccountDetailModel);
        }
        /// <summary>
        /// 银联支付 生成报文
        /// </summary>
        public string SubmitString(UnionPayInfo orderInfo, string xmlSign)
        {
            #region xml格式
            //<merchantName>商户名称</ merchantName >
            //<merchantId>商户代码（15-24位数字）</merchantId>
            //<merchantOrderId>商户订单号(12-32位数字、字母)</merchantOrderId>
            //<merchantOrderTime>商户订单时间(YYYYMMDDHHMMSS)</merchantOrderTime>
            //<merchantOrderAmt>商户订单金额（12位整数, 单位为分）</merchantOrderAmt>
            //< merchantOrderDesc>描述而已 </ merchantOrderDesc>
            //< transTimeout>订单超时时间（默认为空，是24小时）</ transTimeout>
            //<sign>上方7个节点的签名值</sign>
            //< backEndUrl>商户服务器地址，用于接收交易结果通知</ backEndUrl>
            //< merchantPublicCert>公钥证书--Base64编码的结果</ merchantPublicCert>
            //</upomp> 
            #endregion
            StringBuilder sb = new StringBuilder();
            sb.Append("<?xml version=\"1.0\" encoding=\"UTF-8\" ?>");
            sb.Append("<upomp application=\"SubmitOrder.Req\" version=\"1.0.0\">");
            sb.AppendFormat("<merchantName>{0}</merchantName>", UnionPayParameters.merchantName);
            sb.AppendFormat("<merchantId>{0}</merchantId>", UnionPayParameters.merchantId);
            sb.AppendFormat("<merchantOrderId>{0}</merchantOrderId>", orderInfo.merchantOrderId);
            sb.AppendFormat("<merchantOrderTime>{0}</merchantOrderTime>", orderInfo.merchantOrderTime.ToString("yyyyMMddHHmmss"));
            sb.AppendFormat("<merchantOrderAmt>{0}</merchantOrderAmt>", orderInfo.merchantOrderAmt);
            sb.AppendFormat("<merchantOrderDesc>{0}</merchantOrderDesc>", orderInfo.merchantOrderDesc);
            sb.AppendFormat("<backEndUrl>{0}</backEndUrl>", UnionPayParameters.backEndUrl);
            sb.AppendFormat("<sign>{0}</sign>", xmlSign);
            sb.AppendFormat("<merchantPublicCert>{0}</merchantPublicCert></upomp>", UnionPayParameters.merchant_public_cer);

            return sb.ToString();
        }

        /// <summary>
        /// 根据预点单编号查询对应的信息
        /// </summary>
        /// <param name="preorderId"></param>
        /// <returns></returns>
        public DataTable QueryPreorder(long preorderId)
        {
            return preorder19dianMan.SelectPreOrderAndCompany(preorderId);
        }

        /// <summary>
        /// 设置支付预点单时需要的PreOrder19dianInfo对象
        /// </summary>
        /// <param name="dvPreorder"></param>
        /// <param name="dtPrepayPrivilege"></param>
        /// <param name="prePaidSum"></param>
        /// <param name="discount"></param>
        /// <returns></returns>
        public static PreOrder19dianInfo GetPreOrder19dianInfo(DataView dvPreorder, double prePaidSum, double discount = 0)
        {
            PreOrder19dianInfo preorder19dian = new PreOrder19dianInfo();
            DateTime currentTime = System.DateTime.Now;
            int commissionType = Common.ToInt32(dvPreorder[0]["viewallocCommissionType"]);
            switch (commissionType)
            {
                case (int)VACommissionType.Normal_Value:
                    preorder19dian.viewallocCommission = Common.ToDouble(dvPreorder[0]["viewallocCommissionValue"]);
                    break;
                case (int)VACommissionType.Proportion:
                    preorder19dian.viewallocCommission = Common.ToDouble11Digit(dvPreorder[0]["viewallocCommissionValue"]) * prePaidSum;
                    preorder19dian.viewallocCommission = Math.Round(preorder19dian.viewallocCommission.Value, 2);
                    break;
                default:
                    break;
            }

            double transactionProportion = new SystemConfigCacheLogic().GetVATransactionProportion();
            preorder19dian.transactionFee = Common.ToDouble(prePaidSum * transactionProportion);
            preorder19dian.viewallocNeedsToPayToShop = Common.ToDouble(prePaidSum - preorder19dian.transactionFee - preorder19dian.viewallocCommission);
            preorder19dian.status = VAPreorderStatus.Prepaid;
            preorder19dian.isPaid = 1;
            preorder19dian.prePaidSum = prePaidSum;
            preorder19dian.OrderType = (OrderTypeEnum)(int.Parse(dvPreorder[0]["OrderType"].ToString()));
            preorder19dian.prePayTime = currentTime;
            preorder19dian.preOrder19dianId = Common.ToInt64(dvPreorder[0]["preOrder19dianId"]);
            preorder19dian.preOrderSum = Common.ToInt64(dvPreorder[0]["preOrderSum"]);
            if (discount > 0)
            {
                preorder19dian.discount = discount;
            }
            else
            {
                preorder19dian.discount = Common.ToDouble(dvPreorder[0]["discount"]);
            }
            preorder19dian.OrderId = Guid.Parse(dvPreorder[0]["OrderId"].ToString()); ;
            return preorder19dian;
        }
        /// <summary>
        /// 填充点单详情PreorderDetail
        /// </summary>
        /// <param name="isNewBuild">是否为最新版本</param>
        /// <param name="isNewBuildAnother">是否为包含优惠券功能最新版本</param>
        /// <param name="orderDetailConfigDesc">订单注意事项</param>
        /// <param name="mealOrderIds">年夜饭点单编号数组</param>
        /// <param name="dtrowPreOrderAndCompany"></param>
        /// <param name="executedRedEnvelopeAmount"></param>
        /// <param name="money19dianRemained"></param>
        /// <param name="list"></param>
        /// <param name="mobilePhoneNumber"></param>
        /// <param name="currentPlatformVipGrade"></param>
        /// <returns></returns>
        private VAPreorderDetail FillPreorderDetail(bool isNewBuild, bool isNewBuildAnother, bool expireRedEnvelopeBuild, string orderDetailConfigDesc, List<long> mealOrderIds,
            DataRow dtrowPreOrderAndCompany, double executedRedEnvelopeAmount, double money19dianRemained, bool list, string mobilePhoneNumber, int currentPlatformVipGrade = 0,
            VAAppType appType = VAAppType.ANDROID, string clientBuild = "")
        {
            //初始化全局变量
            string orderInJson = Common.ToString(dtrowPreOrderAndCompany["orderInJson"]);
            int isShopConfirmed = Common.ToInt32(dtrowPreOrderAndCompany["isShopConfirmed"]);
            //int evaluationValue = Common.ToInt32(dtrowPreOrderAndCompany["evaluationValue"]);
            double refundMoneyClosedSumComPare = Common.ToDouble(dtrowPreOrderAndCompany["refundMoneyClosedSum"]);
            double prePaidSumCompare = Common.ToDouble(dtrowPreOrderAndCompany["prePaidSum"]);
            bool isPaid = Common.ToInt32(dtrowPreOrderAndCompany["isPaid"]) == 1;
            int cityId = Common.ToInt32(dtrowPreOrderAndCompany["CityID"]);
            VAPreorderDetail preorderDetail = new VAPreorderDetail();
            preorderDetail.shopIsHandle = Common.ToInt32(dtrowPreOrderAndCompany["isHandle"]) == (int)VAShopHandleStatus.SHOP_Pass;
            preorderDetail.preOrderExplain = orderDetailConfigDesc;//注意事项，描述文字
            preorderDetail.preorderId = Common.ToInt64(dtrowPreOrderAndCompany["preOrder19dianId"]);
            preorderDetail.preorderDate = Common.ToSecondFrom1970(Common.ToDateTime(dtrowPreOrderAndCompany["preOrderTime"]));
            preorderDetail.status = Common.ToInt32(dtrowPreOrderAndCompany["status"]);
            preorderDetail.restaurantId = Common.ToInt32(dtrowPreOrderAndCompany["shopId"]);

            preorderDetail.refundMoneySum = Common.ToDouble(dtrowPreOrderAndCompany["refundMoneySum"]);//总申请退款金额
            //True:未评价
            int isEvaluation = Common.ToInt32(dtrowPreOrderAndCompany["isEvaluation"]);
            //preorderDetail.isNotEvaluated = (isShopConfirmed == 1 && (isEvaluation == 0) && (refundMoneyClosedSumComPare == 0 || refundMoneyClosedSumComPare != prePaidSumCompare));
            preorderDetail.isNotEvaluated = isShopConfirmed == 1 && (isEvaluation == 0);
            preorderDetail.invoiceTitle = Common.ToString(dtrowPreOrderAndCompany["invoiceTitle"]);
            preorderDetail.deskNumber = Common.ToString(dtrowPreOrderAndCompany["deskNumber"]);
            if (isEvaluation == 1)
            {
                var preorderEvaluation =
                           PreorderEvaluationOperate.GetFirstByQuery(new Model.QueryObject.PreorderEvaluationQueryObject() { PreOrder19dianId = preorderDetail.preorderId });
                if (Common.CheckLatestBuild_December(appType, clientBuild))
                {
                    preorderDetail.evaluationValue = preorderEvaluation.EvaluationLevel + 7;
                }
                else
                {
                    int evaluationValue = preorderDetail.evaluationValue;
                    switch (evaluationValue)
                    {
                        case 8:
                            evaluationValue = 5;
                            break;
                        case 7:
                            evaluationValue = 3;
                            break;
                        case 6:
                            evaluationValue = 2;
                            break;
                        default:
                            break;
                    }
                    preorderDetail.evaluationValue = evaluationValue;
                }

                preorderDetail.evaluationContent = preorderEvaluation.EvaluationContent;
                preorderDetail.evaluationValue = preorderEvaluation.EvaluationValue;//当前点单评分
            }
            else
            {
                preorderDetail.evaluationContent = string.Empty;
                preorderDetail.evaluationValue = -1;//当前点单评分
            }
            preorderDetail.canBeDelete = false;//标记点单是否可删除
            preorderDetail.praisedCount = 0;//点赞个数，预留属性
            preorderDetail.snsMessageJson = Common.ToString(ConfigurationManager.AppSettings["foodDiaries"]);
            preorderDetail.foodDiariesUrl = WebConfig.ServerDomain + "AppPages/FoodDiaries.aspx?id=" + preorderDetail.preorderId;
            preorderDetail.foodDiariesAfterShareUrl = WebConfig.ServerDomain + "AppPages/FoodDiariesShow.aspx?id={0}";
            preorderDetail.shopLogoUrl = !String.IsNullOrWhiteSpace(Common.ToString(dtrowPreOrderAndCompany["shopImagePath"]))
                                            && !String.IsNullOrWhiteSpace(Common.ToString(dtrowPreOrderAndCompany["shopLogo"]))
                                            ? WebConfig.CdnDomain + WebConfig.ImagePath + Common.ToString(dtrowPreOrderAndCompany["shopImagePath"]) + Common.ToString(dtrowPreOrderAndCompany["shopLogo"])
                                            : "";
            preorderDetail.restaurantName = Common.ToString(dtrowPreOrderAndCompany["shopName"]);
            if (list && mealOrderIds.Any() && mealOrderIds.Contains(preorderDetail.preorderId))
            {
                preorderDetail.restaurantName = new MealManager().GetMealScheduleByOrderId(preorderDetail.preorderId);
            }

            preorderDetail.isShopConfirmed = isShopConfirmed == 1 ? true : false;
            preorderDetail.orderInJson = orderInJson;
            preorderDetail.vipPolicies = new List<VAVipPolicy>();// vipPolicies;//待处理
            preorderDetail.userPolicy = new VAVipPolicy(); //userPolicy;//待处理
            preorderDetail.verifiedSaving = Common.ToDouble(dtrowPreOrderAndCompany["verifiedSaving"]);
            preorderDetail.usedDateTime = isPaid == true
                                                 ? Common.ToSecondFrom1970(Common.ToDateTime(dtrowPreOrderAndCompany["prePayTime"]))
                                                 : Common.ToSecondFrom1970(Common.ToDateTime(dtrowPreOrderAndCompany["preOrderTime"]));

            #region 未支付，未使用，已使用，已退款，退款中
            if (isPaid)
            {
                preorderDetail.usedDateTime = Common.ToSecondFrom1970(Common.ToDateTime(dtrowPreOrderAndCompany["prePayTime"]));//支付时间
                switch (preorderDetail.status)
                {
                    case (int)VAPreorderStatus.Refund://已退款
                        preorderDetail.isUsed = 3;
                        DateTime maxTime = Common.ToDateTime(preorder19dianMan.GetMaxRefundTime(preorderDetail.preorderId));
                        preorderDetail.usedDateTime = Common.ToSecondFrom1970(maxTime);//最新退款时间
                        if (isNewBuildAnother)
                        {
                            if (new CouponOperate().IsCouponRefund(preorderDetail.preorderId))
                            {
                                preorderDetail.isPersonalFullRefund = true;
                            }
                        }
                        break;
                    case (int)VAPreorderStatus.OriginalRefunding://退款中
                        preorderDetail.isUsed = 4;
                        if (isNewBuildAnother)
                        {
                            if (new CouponOperate().IsCouponRefund(preorderDetail.preorderId))
                            {
                                preorderDetail.isPersonalFullRefund = true;
                            }
                        }
                        break;
                    default:
                        preorderDetail.isContantExpireRedEnvelope = expireRedEnvelopeBuild ? new RedEnvelopeConnPreOrderManager().CheckPaidOrderRedEnvelopeStatus(preorderDetail.preorderId) : false;
                        preorderDetail.isUsed = isShopConfirmed == 1 ? 2 : 1;
                        break;
                }
                if ((isShopConfirmed == 1 && refundMoneyClosedSumComPare == preorderDetail.refundMoneySum) ||
                    (refundMoneyClosedSumComPare == preorderDetail.refundMoneySum && refundMoneyClosedSumComPare > 0))
                {
                    preorderDetail.canBeDelete = true;
                }
            }
            else
            {
                preorderDetail.isUsed = 0;//未支付
                preorderDetail.usedDateTime = preorderDetail.preorderDate;//下单时间
                preorderDetail.canBeDelete = true;
            }
            #endregion

            #region 美食日记相关
            List<int> listShared = new List<int>();
            var foodDiaryService = ServiceFactory.Resolve<IFoodDiaryService>();
            FoodDiary model = foodDiaryService.GetFoodDiaryByOrder(preorderDetail.preorderId);
            if (model != null)
            {
                FoodDiaryShared fds = model.Shared;
                if ((FoodDiaryShared.新浪微博 & fds) != FoodDiaryShared.没有分享)
                {
                    listShared.Add((int)FoodDiaryShared.新浪微博);
                }
                if ((FoodDiaryShared.QQ空间 & fds) != FoodDiaryShared.没有分享)
                {
                    listShared.Add((int)FoodDiaryShared.QQ空间);
                }
                if ((FoodDiaryShared.微信朋友圈 & fds) != FoodDiaryShared.没有分享)
                {
                    listShared.Add((int)FoodDiaryShared.微信朋友圈);
                }
                if ((FoodDiaryShared.微信好友 & fds) != FoodDiaryShared.没有分享)
                {
                    listShared.Add((int)FoodDiaryShared.微信好友);
                }
                if ((FoodDiaryShared.QQ好友 & fds) != FoodDiaryShared.没有分享)
                {
                    listShared.Add((int)FoodDiaryShared.QQ好友);
                }
                preorderDetail.isHaveShared = (int)model.Shared > 0 ? true : false;
                preorderDetail.sharedDesc = (int)model.Shared > 0 ? (String.IsNullOrEmpty(model.Content) ? "" : model.Content) : "你还没有写美食日记哦~去写？";
            }
            else
            {
                listShared.Add(0);
                preorderDetail.isHaveShared = false;
                preorderDetail.sharedDesc = "你还没有写美食日记哦~去写？";
            }
            preorderDetail.haveSharedType = listShared;
            #endregion

            #region 计算当前点单折扣
            DataTable dtShopVipInfo = new ShopInfoCacheLogic().GetShopVipInfo(preorderDetail.restaurantId);//查询当前门店的VIP等级信息
            double currentDiscount = 1;
            if (isPaid)//点单已支付，返回支付时的折扣
            {
                currentDiscount = Common.ToDouble(dtrowPreOrderAndCompany["discount"]);
            }
            else
            {
                if (dtShopVipInfo.Rows.Count > 0)
                {
                    List<VAShopVipInfo> shopVipList = new List<VAShopVipInfo>();
                    VAShopVipInfo userVipInfo = new VAShopVipInfo();
                    ClientExtendOperate.GetUserVipInfo(currentPlatformVipGrade, dtShopVipInfo, userVipInfo, shopVipList);
                    currentDiscount = userVipInfo.discount;
                }
            }
            #endregion

            #region 计算菜品原价和悠先价
            double originalPrice = 0;
            double discountPrice = 0;
            double originalsundryPrice = 0;
            double discountSundryPrice = 0;
            //菜品
            if (!String.IsNullOrEmpty(orderInJson))
            {
                originalPrice = CalcPreorederOriginalPrice(orderInJson);//计算菜品的原价 
                double deductionCoupon;//
                discountPrice = CalcPreorederVIPPrice(orderInJson, currentDiscount, out deductionCoupon);//菜品折扣后价格
            }
            else
            {
                originalPrice = Common.ToDouble(dtrowPreOrderAndCompany["preOrderSum"]);//直接付款产生的订单
                discountPrice = originalPrice;//菜品折扣后价格
            }
            //杂项
            if (!String.IsNullOrEmpty(Common.ToString(dtrowPreOrderAndCompany["sundryJson"])))
            {
                List<VASundryInfo> sundryListRequest = JsonOperate.JsonDeserialize<List<VASundryInfo>>(Common.ToString(dtrowPreOrderAndCompany["sundryJson"]));
                List<SundryInfoResponse> sundryInfoList = new List<SundryInfoResponse>();
                originalsundryPrice = CalcSundryCount(originalPrice, sundryListRequest, ref sundryInfoList, 0);//计算杂项原价
                List<SundryInfoResponse> sundryInfoList2 = new List<SundryInfoResponse>();
                discountSundryPrice = Common.ToDouble(CalcSundryCount(originalPrice, sundryListRequest, ref sundryInfoList2, currentDiscount));//杂项折扣后价格
                if (!list)
                {
                    preorderDetail.sundryList = sundryInfoList;
                }
                else
                {
                    preorderDetail.sundryList = new List<SundryInfoResponse>();
                }
            }
            double serverCalculatedSum = originalPrice + originalsundryPrice;//整体折扣前价格（原价，小计）
            double serverUxianPriceSum = discountPrice + discountSundryPrice;//整体折扣后价格（悠先价）
            //门店是否支持四舍五入
            if (Common.ToBool(dtrowPreOrderAndCompany["isSupportAccountsRound"]))
            {
                preorderDetail.detailServerCalculatedSum = Common.ToDouble(CommonPageOperate.YouLuoRound(serverCalculatedSum, 0));
                preorderDetail.detailServerUxianPriceSum = Common.ToDouble(CommonPageOperate.YouLuoRound(serverUxianPriceSum, 0));
            }
            else
            {
                preorderDetail.detailServerCalculatedSum = Common.ToDouble(serverCalculatedSum);
                preorderDetail.detailServerUxianPriceSum = Common.ToDouble(serverUxianPriceSum);
            }
            List<OrderPayMode> payModeList = new List<OrderPayMode>();//点单详细支付名称和金额   
            List<OrderDishOtherInfo> orderDishOtherList = new List<OrderDishOtherInfo>();
            preorderDetail.complainUrl = "";
            CouponOperate couponOperate = new CouponOperate();
            preorderDetail.deductibleAmount = isNewBuildAnother ? Common.ToDouble(couponOperate.GetCouponDeductibleAmount(preorderDetail.preorderId)[0]) : 0;//优惠券点单抵扣金额
            preorderDetail.couponDetails = new List<OrderPaymentCouponDetail>();


            if (!list)
            {
                preorderDetail.isSupportPayment = Common.ToBool(dtrowPreOrderAndCompany["isSupportPayment"]);
                //prePaidSumCompare = preorderDetail.detailServerUxianPriceSum;//悠先价，当前点单需要支付的金额
                preorderDetail.rationBalance = money19dianRemained;
                preorderDetail.executedRedEnvelopeAmount = executedRedEnvelopeAmount;
                preorderDetail.stillNeedPaySum = Common.ToDouble((preorderDetail.detailServerUxianPriceSum - executedRedEnvelopeAmount - money19dianRemained)) > 0
                 ? Common.ToDouble((preorderDetail.detailServerUxianPriceSum - executedRedEnvelopeAmount - money19dianRemained)) : 0;
                preorderDetail.executedRedEnvelopeAmount = executedRedEnvelopeAmount;
                if (isPaid)
                {
                    //系统中不存在优惠券,则隐藏分享按钮
                    if (CouponOperate.CheckCityIsWhite(cityId))
                    {
                        if (CouponOperate.GetCountByQuery(new CouponQueryObject() { StartDateTo = DateTime.Now, EndDateFrom = DateTime.Now, State = 1, IsGot = false }) > 0)
                        {
                            bool flag = CouponOperate.CheckShopIsWhite(cityId, preorderDetail.restaurantId);
                            preorderDetail.isHaveShareCoupon = isNewBuildAnother ?
                                (flag ?
                                Common.ToDateTime(dtrowPreOrderAndCompany["prePayTime"]).AddDays(SystemConfigOperate.GetCouponValidDate()) < DateTime.Now
                                : false)
                                : false;//老版本不去db检索数据
                            preorderDetail.shareCouponCount = isNewBuildAnother ?
                                (flag ? new CouponCacheLogic().GetCouponCount() : 0)
                                : 0;
                        }
                        else
                        {
                            preorderDetail.isHaveShareCoupon = true;
                            preorderDetail.shareCouponCount = 0;
                        }
                    }
                    ThirdPartyPaymentInfo thirdPartyPaymentInfo = preorder19dianMan.SelectPreorderPayAmount(preorderDetail.preorderId);

                    bool isLatestBuild = Common.CheckLatestBuild_201504(appType, clientBuild);
                    payModeList = new ZZBPreOrderOperate().GetOrderPayModeInfo(prePaidSumCompare, thirdPartyPaymentInfo, true, preorderDetail.preorderId, preorderDetail.status, isLatestBuild);
                    preorderDetail.shareCouponUrl = string.Format(WebConfig.ServerDomain + "AppPages/discountCoupons/v3.html?id={0}", (preorderDetail.preorderId * 3).ToString("X"));
                }
                else
                {
                    preorderDetail.couponDetails = new CouponOperate().GetShopCouponDetails(mobilePhoneNumber, preorderDetail.restaurantId);
                }
                //string newOrderInJson = Common.ToString(dtrowPreOrderAndCompany["newOrderInJson"]);//点单扩展表存储点赞次数新json信息
                //新版本，点菜支付才返回最新信息
                if (isNewBuild == true)
                {
                    preorderDetail.complainUrl = String.Format(WebConfig.ComplaintURL, preorderDetail.preorderId.ToString());
                    string newOrderInJson = new PreOrder19dianManager().GetPreOrder19DianExtendOrderJson(preorderDetail.preorderId);
                    if (!String.IsNullOrWhiteSpace(newOrderInJson))
                    {
                        IImageInfoRepository repositoryContext = ServiceFactory.Resolve<IImageInfoRepository>();
                        IMenuInfoRepository menuRepositoryContext = ServiceFactory.Resolve<IMenuInfoRepository>();
                        var menuInfo = menuRepositoryContext.GetMenuInfoByShopId(preorderDetail.restaurantId);
                        var orders = JsonOperate.JsonDeserialize<List<PreOrderIn19dianOrderJson>>(newOrderInJson);
                        var dishIds = orders.Select(p => p.dishId).ToList();
                        var orderDishPraiseInfos = repositoryContext.GetDishPraiseInfosByDishId(CommonPageOperate.SplicingListStr(dishIds, ""));
                        //组装菜品图片URL
                        var imageList = repositoryContext.GetAssignScaleImageInfosByDishId(ImageScale.普通图片, dishIds.ToArray());
                        foreach (var dishItem in orders)
                        {
                            var image =  imageList.OrderByDescending(p => p.ImageStatus).FirstOrDefault(p => p.DishID == dishItem.dishId);
                            string imagePath = string.Empty;
                            if(image != null)
                            {
                               imagePath = WebConfig.CdnDomain + WebConfig.ImagePath + menuInfo.menuImagePath + image.ImageName;
                            } 
                            orderDishOtherList.Add(new OrderDishOtherInfo()
                            {
                                dishId = dishItem.dishId,
                                orderDishImageUrl = imagePath,
                                orderDishIsPraise = dishItem.isHavePraise,
                                orderDishPraiseNum = orderDishPraiseInfos.Single(p => p.dishId == dishItem.dishId).orderDishPraiseNum
                            });
                        }
                    }
                }
            }
            preorderDetail.orderDishOtherList = orderDishOtherList;
            preorderDetail.payModeList = payModeList;
            preorderDetail.currentDiscount = currentDiscount;

            #endregion

            #region 抵扣券分享文字及图片
            CouponCacheLogic couponCacheLogic = new CouponCacheLogic();
            preorderDetail.couponShareImage = WebConfig.CdnDomain + WebConfig.ImagePath + "15/Coupon/" + couponCacheLogic.GetCouponShareImage();
            preorderDetail.couponShareText = couponCacheLogic.GetCouponShareText();
            #endregion

            #region 剪刀手入座

            ShopManager shopManager = new ShopManager();

            preorderDetail.payee = shopManager.QueryShopAccountName(Common.ToInt32(dtrowPreOrderAndCompany["shopId"]));//收款人，取的门店设置的银行开户名
            DataTable dtConfirm = preorder19dianMan.SelectNewPreorderShopConfirmedInfo(preorderDetail.preorderId);
            if (dtConfirm != null && dtConfirm.Rows.Count > 0)
            {
                preorderDetail.lastConfirmTime = Common.ToSecondFrom1970(Common.ToDateTime(dtConfirm.Rows[0]["shopConfirmedTime"]));//最后一次入座时间
            }
            else
            {
                preorderDetail.lastConfirmTime = 0;
            }

            ClientConfirmOrderLogic clientConfirmOrderLogic = new ClientConfirmOrderLogic();
            string[] whiteCity = clientConfirmOrderLogic.GetConfirmOrderWhiteCitys();

            if (whiteCity == null || whiteCity.Length == 0)
            {
                preorderDetail.couldClientConfirmOrder = true;//所有城市开放
            }
            else//部分城市开放
            {
                if (((IList)whiteCity).Contains(cityId.ToString()))//白名单包含指定城市
                {
                    string[] whiteShop = clientConfirmOrderLogic.GetConfirmOrderWhiteShops(cityId);

                    if (whiteShop == null || whiteShop.Length == 0)
                    {
                        preorderDetail.couldClientConfirmOrder = true;//所有门店开放
                    }
                    else
                    {
                        if (((IList)whiteShop).Contains(preorderDetail.restaurantId.ToString()))//白名单包含指定门店
                        {
                            preorderDetail.couldClientConfirmOrder = true;//开放
                        }
                        else
                        {
                            preorderDetail.couldClientConfirmOrder = false;
                        }
                    }
                }
            }

            #endregion

            #region 中奖信息
            AwardInfo awardInfo = new AwardInfo();
            ViewAllocAwardOperate vaOperate = new ViewAllocAwardOperate();
            AwardConnPreOrderOperate awardOperate = new AwardConnPreOrderOperate();
            AwardConnPreOrder awardConnPreOrder = awardOperate.SelectAwardConnPreOrderByOrderId(preorderDetail.preorderId);
            if (awardConnPreOrder != null && awardConnPreOrder.PreOrder19dianId > 0)
            {
                awardInfo.awardType = (AwardType)awardConnPreOrder.Type;
                awardInfo.awardShowUrl = WebConfig.ServerDomain + "Award/awardMobile/lottery/lottery.html?p=" + preorderDetail.preorderId + "&s=" + awardConnPreOrder.ShopId;
                awardInfo.awardShowUrl = awardInfo.awardShowUrl + "&ww={0}&wh={1}";

                switch (awardConnPreOrder.Type)
                {
                    case AwardType.NotWin:

                        awardInfo.awardDesc = "";
                        awardInfo.awardShowUrl = "";

                        break;
                    case AwardType.AvoidQueue:

                        awardInfo.awardDesc = "【专享" + awardConnPreOrder.ValidTime.ToString("MM月dd日") + "免排队特权】";

                        break;
                    case AwardType.PresentDish:

                        ShopAwardOperate shopAwardOperate = new ShopAwardOperate();
                        ShopAward dishAward = shopAwardOperate.QueryAllShopAward(awardConnPreOrder.AwardId);
                        string dishName = shopAwardOperate.GetDishNameI18nID(dishAward.DishId);

                        awardInfo.awardDesc = "【门店赠菜 - " + dishName + "】";

                        break;
                    case AwardType.PresentRedEnvelope:

                        //获取用户信息
                        //string mobile = "";
                        //DataTable dtUser = CustomerOperate.QueryPartCustomerInfo(preorderDetail.preorderId);
                        //if (dtUser != null && dtUser.Rows.Count > 0)
                        //{
                        //mobile = dtUser.Rows[0]["mobilePhoneNumber"].ToString();

                        //查询用户红包
                        RedEnvelopeOperate redEnvelopeOperate = new RedEnvelopeOperate();
                        double redEnvelopeAmount = redEnvelopeOperate.SelectRedEnvelopeAmount(awardConnPreOrder.redEnvelopeId);

                        awardInfo.awardDesc = "【全场通用红包￥" + redEnvelopeAmount.ToString() + "元】";
                        //}
                        //else
                        //{
                        //    awardInfo.awardDesc = "";
                        //}

                        //【全场通用红包￥4.2】
                        break;
                    case AwardType.PresentThirdParty:

                        ViewAllocAward vaAward = vaOperate.SelectVAAward(awardConnPreOrder.AwardId);//平台奖品
                        awardInfo.awardDesc = "【" + vaAward.Name + "】";

                        break;
                }
            }
            else
            {
                awardInfo.awardType = (AwardType)(-1);//表示未抽奖 
                awardInfo.awardDesc = "";
                awardInfo.awardShowUrl = "";
            }
            preorderDetail.userAwardInfo = awardInfo;
            #endregion

            return preorderDetail;
        }

        /// <summary>
        /// 计算预点单的服务器总金额
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="customerId"></param>
        /// <param name="orderInJson"></param>
        /// <returns></returns>
        private string CalcPreorderServerSum(int companyId, long customerId, string orderInJson)
        {
            VipManager vipMan = new VipManager();
            DataTable dtCustomerCompanyVip = vipMan.SelectCustomerCompanyVip(customerId, companyId);

            double discount = 1;
            if (dtCustomerCompanyVip.Rows.Count == 1)
            {
                discount = Common.ToDouble(dtCustomerCompanyVip.Rows[0]["discount"]);
            }
            VAEstimatedSavingAndOrginal estimatedSavingAndOrginal = CalcPreorderEstimatedSaving(orderInJson, discount);
            string preOrderServerSum = Common.ToString(Common.ToDouble(estimatedSavingAndOrginal.orginalPriceSum - estimatedSavingAndOrginal.estimatedSaving));
            return preOrderServerSum;
        }

        /// <summary>
        /// 根据编号，查询预点单详情信息
        /// </summary>
        /// <param name="preOrderId"></param>
        /// <returns></returns>
        public DataTable QueryPreOrderById(long preOrder19dianId)
        {
            DataTable dtPreOrder19dian = preorder19dianMan.SelectPreOrderById(preOrder19dianId);
            for (int i = 0; i < dtPreOrder19dian.Rows.Count; i++)
            {
                if (Common.ToDouble(dtPreOrder19dian.Rows[i]["preOrderServerSum"]) > 0)
                {
                    continue;
                }
                else
                {
                    long customerId = Common.ToInt64(dtPreOrder19dian.Rows[i]["customerId"]);
                    int companyId = Common.ToInt32(dtPreOrder19dian.Rows[i]["companyId"]);
                    string orderInJson = Common.ToString(dtPreOrder19dian.Rows[i]["orderInJson"]);
                    string preOrderServerSum = CalcPreorderServerSum(companyId, customerId, orderInJson);
                    dtPreOrder19dian.Rows[i]["preOrderServerSum"] = preOrderServerSum;
                }
            }
            return dtPreOrder19dian;
        }

        /// <summary>
        /// 根据orderId查询正常点单的详情
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public DataTable QueryPreOrderByOrderId(Guid orderId)
        {
            DataTable dtPreOrder19dian = preorder19dianMan.SelectPreOrderByOrderId(orderId);
            for (int i = 0; i < dtPreOrder19dian.Rows.Count; i++)
            {
                if (Common.ToDouble(dtPreOrder19dian.Rows[i]["preOrderServerSum"]) > 0)
                {
                    continue;
                }
                else
                {
                    long customerId = Common.ToInt64(dtPreOrder19dian.Rows[i]["customerId"]);
                    int companyId = Common.ToInt32(dtPreOrder19dian.Rows[i]["companyId"]);
                    string orderInJson = Common.ToString(dtPreOrder19dian.Rows[i]["orderInJson"]);
                    string preOrderServerSum = CalcPreorderServerSum(companyId, customerId, orderInJson);
                    dtPreOrder19dian.Rows[i]["preOrderServerSum"] = preOrderServerSum;
                }
            }
            return dtPreOrder19dian;
        }


        public DataTable QueryOrderById(Guid orderId)
        {
            DataTable dtPreOrder19dian = preorder19dianMan.SelectOrderById(orderId);
            for (int i = 0; i < dtPreOrder19dian.Rows.Count; i++)
            {
                if (Common.ToDouble(dtPreOrder19dian.Rows[i]["preOrderServerSum"]) > 0)
                {
                    continue;
                }
                else
                {
                    long customerId = Common.ToInt64(dtPreOrder19dian.Rows[i]["customerId"]);
                    int companyId = Common.ToInt32(dtPreOrder19dian.Rows[i]["companyId"]);
                    string orderInJson = Common.ToString(dtPreOrder19dian.Rows[i]["orderInJson"]);
                    string preOrderServerSum = CalcPreorderServerSum(companyId, customerId, orderInJson);
                    dtPreOrder19dian.Rows[i]["preOrderServerSum"] = preOrderServerSum;
                }
            }
            return dtPreOrder19dian;
        }

        /// <summary>
        /// 2013-7-26 wangcheng
        /// 验证预点单，在PreorderShopConfirmedInfo中添加一条记录，然后修改PreOrder19dian中isShopConfirmed字段状态
        /// </summary>
        /// <param name="preOrder19dianId"></param>
        /// <param name="shopConfirmenStatus">验证还是撤销验证</param>
        /// <param name="employeeId">验证人</param>
        /// <param name="employeeName">验证人姓名</param>
        /// <param name="employeePosition">验证人职位</param>
        /// <returns></returns>
        public bool ShopConfrimedPreOrder(long preOrder19dianId, int shopConfirmenStatus, int employeeId, string employeeName, string employeePosition)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                bool resultInsetPreOrderShopConfirmedInfo = preorder19dianMan.InsertPreorderShopConfirmedInfo(preOrder19dianId, shopConfirmenStatus, employeeId, employeeName, employeePosition);
                bool resultShopConfrimPreOrder19dian = preorder19dianMan.ShopConfirmPreOrder19dian(preOrder19dianId, shopConfirmenStatus);
                OrderManager om = new OrderManager();
                bool resultShopConfrim = om.ShopConfrim(preOrder19dianId, shopConfirmenStatus);
                if (resultInsetPreOrderShopConfirmedInfo && resultShopConfrimPreOrder19dian && resultShopConfrim)
                {
                    scope.Complete();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        /// <summary>
        /// 查询当前公司门店所有未对账的preOrder19dianId
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public DataTable QueryPreOrder19dianId(int shopId)
        {
            return preorder19dianMan.SelectPreOrder19dianId(shopId);
        }

        /// <summary>
        /// 查询当前所有未对账的订单的店铺ID
        /// </summary>
        /// <returns></returns>
        public DataTable QueryShopIdNotVerified()
        {
            return preorder19dianMan.SelectShopIdNotVerified();
        }
        /// <summary>
        /// 查询某个预点单的对账信息
        /// </summary>
        /// <param name="preOrder19dianId"></param>
        /// <returns></returns>
        public DataTable QueryPreOrderCheckInfo(long preOrder19dianId)
        {
            return preorder19dianMan.SelectPreOrderCheckInfo(preOrder19dianId);
        }
        /// <summary>
        /// 根据预点单id查询该点单的详细信息
        /// </summary>
        /// <param name="preOrder19dianId"></param>
        /// <returns></returns>
        public DataTable QueryPreOrderInfoByPreOrder19dianId(long preOrder19dianId)
        {
            return preorder19dianMan.SelectPreOrderById(preOrder19dianId);
        }
        /// <summary>
        /// 2013-7-26 wangcheng
        /// 查询某个预点单的审核信息
        /// </summary>
        /// <param name="preOrder19dianId"></param>
        /// <returns></returns>
        public DataTable QueryPreorderShopConfirmedInfo(long preOrder19dianId)
        {
            return preorder19dianMan.SelectPreorderShopConfirmedInfo(preOrder19dianId);
        }

        /// <summary>
        /// 客户端删除预点单（状态不为结账的）20140313
        /// </summary>
        /// <param name="clientPreorderDeleteRequest"></param>
        /// <returns></returns>
        public VAClientPreorderDeleteResponse ClientRemovePreorder19dian(VAClientPreorderDeleteRequest clientPreorderDeleteRequest)
        {
            VAClientPreorderDeleteResponse clientPreorderDeleteResponse = new VAClientPreorderDeleteResponse();
            clientPreorderDeleteResponse.type = VAMessageType.CLIENT_PREORDER_DELETE_RESPONSE;
            clientPreorderDeleteResponse.cookie = clientPreorderDeleteRequest.cookie;
            clientPreorderDeleteResponse.uuid = clientPreorderDeleteRequest.uuid;
            CheckCookieAndMsgtypeInfo checkResult = Common.CheckCookieAndMsgtype(clientPreorderDeleteRequest.cookie,
                clientPreorderDeleteRequest.uuid, (int)clientPreorderDeleteRequest.type, (int)VAMessageType.CLIENT_PREORDER_DELETE_REQUEST);
            if (checkResult.result == VAResult.VA_OK)
            {
                if (clientPreorderDeleteRequest.preorderIds.Count > 0)
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        string orderIds = CommonPageOperate.SplicingListStr(clientPreorderDeleteRequest.preorderIds, "");
                        if (preorder19dianMan.DeletePreOrder19dian(clientPreorderDeleteRequest.preorderIds, orderIds))
                        {
                            OrderManager objOrderManager = new OrderManager();
                            //add by dangtao at 20150730 for BUG #2061 优先点菜：通过优先点菜老版本删除的订单，在新版本上仍可见
                            if (objOrderManager.UpdateOrderStatus(orderIds))
                            {
                                scope.Complete();
                                clientPreorderDeleteResponse.result = VAResult.VA_OK;
                            }
                        }
                        else
                        {
                            clientPreorderDeleteResponse.result = VAResult.VA_FAILED_DB_ERROR;
                        }
                    }
                }
                else
                {
                    clientPreorderDeleteResponse.result = VAResult.VA_FAILED_OTHER;
                }
            }
            return clientPreorderDeleteResponse;
        }

        /// <summary>
        /// 计算杂项
        /// </summary>
        /// <param name="orginalPriceSum"></param>
        /// <param name="sundryListRequest"></param>
        /// <param name="sundryList"></param>
        /// <param name="discount"></param>
        /// <returns></returns>
        public double CalcSundryCount(double orginalPriceSum, List<VASundryInfo> sundryListRequest, ref List<SundryInfoResponse> sundryList, double discount)
        {
            double sundryCount = 0;
            double sundryRatio = 0;
            int sundryRatioInt = -1;
            if (sundryListRequest != null)
            {
                for (int i = 0; i < sundryListRequest.Count; i++)
                {
                    SundryInfoResponse sundryinfo = new SundryInfoResponse();
                    switch (Common.ToInt32(sundryListRequest[i].sundryChargeMode))
                    {
                        default:
                        case 1://固定金额
                            {
                                if (Common.ToBool(sundryListRequest[i].vipDiscountable))
                                {
                                    sundryCount += Common.ToDouble(sundryListRequest[i].price) * discount;
                                }
                                else
                                {
                                    sundryCount += Common.ToDouble(sundryListRequest[i].price);
                                }
                                sundryinfo.sundryPrice = Common.ToDouble(sundryListRequest[i].price);
                                sundryinfo.quantity = 0;
                                break;
                            }
                        case 2://按比例
                            {
                                sundryRatioInt = i;
                                if (Common.ToBool(sundryListRequest[i].vipDiscountable))
                                {
                                    sundryRatio = Common.ToDouble(sundryListRequest[i].price) * discount;
                                }
                                else
                                {
                                    sundryRatio = Common.ToDouble(sundryListRequest[i].price);
                                }
                                sundryinfo.quantity = 0;
                                break;
                            }
                        case 3://按人次
                            {
                                if (Common.ToBool(sundryListRequest[i].vipDiscountable))
                                {
                                    sundryCount += Common.ToDouble(sundryListRequest[i].price) * Common.ToDouble(sundryListRequest[i].quantity) * discount;
                                }
                                else
                                {
                                    sundryCount += Common.ToDouble(sundryListRequest[i].price) * Common.ToDouble(sundryListRequest[i].quantity);
                                }
                                sundryinfo.sundryPrice = Common.ToDouble(sundryListRequest[i].price) * Common.ToDouble(sundryListRequest[i].quantity);
                                sundryinfo.quantity = Common.ToInt32(sundryListRequest[i].quantity);
                                break;
                            }
                    }
                    sundryinfo.sundryId = Common.ToInt32(sundryListRequest[i].sundryId);
                    sundryinfo.sundryName = Common.ToString(sundryListRequest[i].sundryName);
                    sundryinfo.price = sundryListRequest[i].price;//当前杂项单价
                    sundryinfo.sundryChargeMode = Common.ToInt32(sundryListRequest[i].sundryChargeMode);//当前杂项收费方式
                    sundryinfo.sundryStandard = Common.ToString(sundryListRequest[i].sundryStandard);//杂项规格
                    sundryList.Add(sundryinfo);
                }
                //单独算比例
                if (sundryList.Count > 0 && sundryRatioInt != -1)
                {
                    sundryList[sundryRatioInt].sundryPrice = (orginalPriceSum + sundryCount) * sundryRatio;
                }
                sundryList = sundryList.OrderByDescending(i => i.quantity).ToList();
                sundryCount += (orginalPriceSum + sundryCount) * sundryRatio;
            }
            return sundryCount;
        }
        /// <summary>
        /// 计算点单经过Vip折扣后节省的金额
        /// </summary>
        /// <param name="orderInJson"></param>
        /// <param name="discount"></param>
        /// <returns></returns>
        public VAEstimatedSavingAndOrginal CalcPreorderEstimatedSaving(string orderInJson, double discount, long preorderId = 0, int isHadPaied = 0)
        {
            VAEstimatedSavingAndOrginal estimatedSavingAndOrginal = new VAEstimatedSavingAndOrginal();
            try
            {
                if (discount > 0 && discount <= 1)
                {
                    DishManager dishMan = new DishManager();
                    List<PreOrderIn19dian> listOrderInfo = JsonOperate.JsonDeserialize<List<PreOrderIn19dian>>(orderInJson);
                    double currentprice = 0;

                    double sumall = 0;
                    if (preorderId != 0)
                    {
                        foreach (PreOrderIn19dian preorder in listOrderInfo)
                        {
                            if (dishMan.IsDishScaleVipDiscountable(preorder.dishPriceI18nId))
                            {
                                sumall += (preorder.quantity * preorder.unitPrice) * discount;
                            }
                            else
                            {
                                sumall += preorder.quantity * preorder.unitPrice;
                            }
                            //配料计算
                            double ingredientsPriceAllVIPdiscount = 0;
                            double ingredientsPriceAll = 0;
                            double ingredientsPriceSingle = 0;
                            double ingredientsPriceSinglePayBackdiscount = 0;
                            double ingredientsPriceSingleVIPdiscount = 0;
                            double notingredientsPriceSingleVIPdiscount = 0;
                            double notingredientsPriceSinglePayBackdiscount = 0;
                            for (int i = 0; i < preorder.quantity; i++)
                            {
                                CalcIngredients(discount, preorder.dishIngredients, out ingredientsPriceSingleVIPdiscount,
                                    out ingredientsPriceSinglePayBackdiscount, out ingredientsPriceSingle, out notingredientsPriceSingleVIPdiscount, out notingredientsPriceSinglePayBackdiscount);
                                ingredientsPriceAll += ingredientsPriceSingle;
                                ingredientsPriceAllVIPdiscount += ingredientsPriceSingleVIPdiscount + notingredientsPriceSingleVIPdiscount;
                            }
                            currentprice += preorder.quantity * preorder.unitPrice + ingredientsPriceAll;
                            sumall += ingredientsPriceAllVIPdiscount;
                        }
                    }
                    else
                    {
                        foreach (PreOrderIn19dian preorder in listOrderInfo)
                        {
                            if (dishMan.IsDishScaleVipDiscountable(preorder.dishPriceI18nId))
                            {
                                sumall += (preorder.quantity * preorder.unitPrice) * discount;
                            }
                            else
                            {
                                sumall += preorder.quantity * preorder.unitPrice;
                            }
                            //配料计算
                            double ingredientsPriceAllVIPdiscount = 0;
                            double ingredientsPriceAll = 0;
                            double ingredientsPriceSingle = 0;
                            double ingredientsPriceSinglePayBackdiscount = 0;
                            double ingredientsPriceSingleVIPdiscount = 0;
                            double notingredientsPriceSingleVIPdiscount = 0;
                            double notingredientsPriceSinglePayBackdiscount = 0;
                            for (int i = 0; i < preorder.quantity; i++)
                            {
                                CalcIngredients(discount, preorder.dishIngredients, out ingredientsPriceSingleVIPdiscount,
                                    out ingredientsPriceSinglePayBackdiscount, out ingredientsPriceSingle, out notingredientsPriceSingleVIPdiscount, out notingredientsPriceSinglePayBackdiscount);
                                ingredientsPriceAll += ingredientsPriceSingle;
                                ingredientsPriceAllVIPdiscount += ingredientsPriceSingleVIPdiscount + notingredientsPriceSingleVIPdiscount;
                            }
                            currentprice += preorder.quantity * preorder.unitPrice + ingredientsPriceAll;
                            sumall += ingredientsPriceAllVIPdiscount;
                        }
                    }
                    estimatedSavingAndOrginal.estimatedSaving = Common.ToDouble((currentprice - sumall));
                    estimatedSavingAndOrginal.orginalPriceSum = Common.ToDouble(currentprice);
                    if (estimatedSavingAndOrginal.estimatedSaving > estimatedSavingAndOrginal.orginalPriceSum)
                    {
                        //当节省价格超过原价时
                        estimatedSavingAndOrginal.estimatedSaving = estimatedSavingAndOrginal.orginalPriceSum;
                    }
                }
            }
            catch (System.Exception)
            {

            }
            return estimatedSavingAndOrginal;
        }
        /// <summary>
        /// 计算原价
        /// </summary>
        /// <param name="orderInJson"></param>
        /// <returns></returns>
        public double CalcPreorederOriginalPrice(string orderInJson)
        {
            List<PreOrderIn19dian> listOrderInfo = JsonOperate.JsonDeserialize<List<PreOrderIn19dian>>(orderInJson);
            if (listOrderInfo == null || !listOrderInfo.Any())
            {
                return 0;
            }
            double currentprice = 0;
            foreach (PreOrderIn19dian preorder in listOrderInfo)
            {
                double dishIngredientsSingle = 0;
                if (preorder.dishIngredients != null)
                {
                    for (int j = 0; j < preorder.dishIngredients.Count; j++)
                    {
                        dishIngredientsSingle += preorder.dishIngredients[j].quantity * preorder.dishIngredients[j].ingredientsPrice;
                    }
                }
                dishIngredientsSingle = dishIngredientsSingle * preorder.quantity;
                currentprice += preorder.quantity * preorder.unitPrice + dishIngredientsSingle;
            }
            return currentprice;
        }
        /// <summary>
        /// 计算VIP折扣价格
        /// </summary>
        /// <param name="orderInJson"></param>
        /// <param name="discount"></param>
        /// <param name="deductionCoupon">参与优惠券抵价的部分金额</param>
        /// <returns></returns>
        public double CalcPreorederVIPPrice(string orderInJson, double discount, out double deductionCoupon)
        {
            List<PreOrderIn19dian> listOrderInfo = JsonOperate.JsonDeserialize<List<PreOrderIn19dian>>(orderInJson);
            if (listOrderInfo == null || !listOrderInfo.Any())
            {
                deductionCoupon = 0;
                return 0;
            }
            double currentVipprice = 0;
            deductionCoupon = 0;
            DishManager dishMan = new DishManager();
            foreach (PreOrderIn19dian preorder in listOrderInfo)
            {
                double dishIngredientsSingle = 0;
                double dishDeductionCoupon = 0;
                if (preorder.dishIngredients != null)
                {
                    for (int j = 0; j < preorder.dishIngredients.Count; j++)
                    {
                        if (dishMan.IsIngredientsVipDiscountable(preorder.dishIngredients[j].ingredientsId))
                        {
                            double amount = preorder.dishIngredients[j].quantity * preorder.dishIngredients[j].ingredientsPrice * discount;
                            dishIngredientsSingle += amount;
                            dishDeductionCoupon += amount;
                        }
                        else
                        {
                            dishIngredientsSingle += preorder.dishIngredients[j].quantity * preorder.dishIngredients[j].ingredientsPrice;
                        }
                    }
                }
                dishIngredientsSingle = dishIngredientsSingle * preorder.quantity;
                dishDeductionCoupon = dishDeductionCoupon * preorder.quantity;

                if (dishMan.IsDishScaleVipDiscountable(preorder.dishPriceI18nId))
                {
                    double amount = preorder.quantity * preorder.unitPrice * discount;
                    currentVipprice += amount;
                    deductionCoupon += amount;
                }
                else
                {
                    currentVipprice += preorder.quantity * preorder.unitPrice;
                }
                currentVipprice += dishIngredientsSingle;
                deductionCoupon += dishDeductionCoupon;
            }
            return currentVipprice;
        }

        /// <summary>
        /// 计算配料价格
        /// </summary>
        /// <param name="discount">店铺折扣</param>
        /// <param name="dishIngredients"></param>
        /// <param name="ingredientsPriceSingleVIPdiscount">折扣配料价格</param>
        /// <param name="ingredientsPriceSinglePayBackdiscount">支持返送配料价格</param>
        /// <param name="ingredientsPriceSingle">配料原价</param>
        /// <param name="notingredientsPriceSingleVIPdiscount">不支持VIP折扣配料价格</param>
        /// <param name="notingredientsPriceSinglePayBackdiscount">不支持支持返送配料价格</param>
        private static void CalcIngredients(double discount, List<VADishIngredients> dishIngredients, out double ingredientsPriceSingleVIPdiscount, out double ingredientsPriceSinglePayBackdiscount,
            out double ingredientsPriceSingle, out double notingredientsPriceSingleVIPdiscount, out double notingredientsPriceSinglePayBackdiscount)
        {
            DishManager dishMan = new DishManager();
            //配料计算
            ingredientsPriceSingleVIPdiscount = 0;//支持VIP折扣配料价格
            ingredientsPriceSinglePayBackdiscount = 0;//支持返送配料价格
            ingredientsPriceSingle = 0;//配料原价
            notingredientsPriceSingleVIPdiscount = 0;//不支持VIP折扣配料价格
            notingredientsPriceSinglePayBackdiscount = 0;//不支持支持返送配料价格
            if (dishIngredients != null)
            {
                for (int i = 0; i < dishIngredients.Count; i++)
                {
                    if (dishMan.IsIngredientsVipDiscountable(dishIngredients[i].ingredientsId))
                    {
                        ingredientsPriceSingleVIPdiscount += dishIngredients[i].ingredientsPrice * dishIngredients[i].quantity * discount;
                    }
                    else
                    {
                        notingredientsPriceSingleVIPdiscount += dishIngredients[i].ingredientsPrice * dishIngredients[i].quantity;
                    }

                    //if (dishMan.IsIngredientsPayBackable(dishIngredients[i].ingredientsId))
                    //{
                    //    if (dishMan.IsIngredientsVipDiscountable(dishIngredients[i].ingredientsId))
                    //    {
                    //        ingredientsPriceSinglePayBackdiscount += dishIngredients[i].ingredientsPrice * dishIngredients[i].quantity * discount;
                    //    }
                    //    else
                    //    {
                    notingredientsPriceSinglePayBackdiscount += dishIngredients[i].ingredientsPrice * dishIngredients[i].quantity;
                    //  }
                    //}
                    ingredientsPriceSingle += dishIngredients[i].ingredientsPrice * dishIngredients[i].quantity;
                }
            }
        }

        /// <summary>
        /// 增加预点单发票抬头（wangcheng 2013/9/15）20140313
        /// </summary>
        /// <param name="vaPreOrderInvoiceTitleRequest"></param>
        /// <returns></returns>
        public VAPreOrderInvoiceTitleResponse ClientAddPreOrderInvoiceTitle(VAPreOrderInvoiceTitleRequest vaPreOrderInvoiceTitleRequest)
        {
            VAPreOrderInvoiceTitleResponse vaPreOrderInvoiceTitleResponse = new VAPreOrderInvoiceTitleResponse();
            vaPreOrderInvoiceTitleResponse.type = VAMessageType.CLIENT_PREORDER_ADD_INVOICETITLE_RESPONSE;
            vaPreOrderInvoiceTitleResponse.cookie = vaPreOrderInvoiceTitleRequest.cookie;
            vaPreOrderInvoiceTitleResponse.uuid = vaPreOrderInvoiceTitleRequest.uuid;
            CheckCookieAndMsgtypeInfo checkResult = Common.CheckCookieAndMsgtype(vaPreOrderInvoiceTitleRequest.cookie,
                vaPreOrderInvoiceTitleRequest.uuid, (int)vaPreOrderInvoiceTitleRequest.type, (int)VAMessageType.CLIENT_PREORDER_ADD_INVOICETITLE_REQUEST);
            if (checkResult.result == VAResult.VA_OK)
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    switch (vaPreOrderInvoiceTitleRequest.invoiceStatus)
                    {
                        case (int)VAInvoiceTitleOperate.INVOICETITLE_QUERY://通用发票抬头查询
                            #region 查询
                            DataTable dtCommonInvoiceTitle = preorder19dianMan.SelectInvoiceInfo(Common.ToInt32(checkResult.dtCustomer.Rows[0]["CustomerID"]));
                            List<InvoiceInfo> invoiceInfoList = new List<InvoiceInfo>();
                            if (dtCommonInvoiceTitle.Rows.Count > 0)
                            {
                                for (int i = 0; i < dtCommonInvoiceTitle.Rows.Count; i++)
                                {
                                    InvoiceInfo invoiceInfo = new InvoiceInfo();
                                    invoiceInfo.invoiceId = Common.ToInt64(dtCommonInvoiceTitle.Rows[i]["invoiceId"]);
                                    invoiceInfo.invoiceTitle = Common.ToString(dtCommonInvoiceTitle.Rows[i]["invoiceTitle"]);
                                    invoiceInfoList.Add(invoiceInfo);
                                }
                                scope.Complete();
                                vaPreOrderInvoiceTitleResponse.result = VAResult.VA_OK;
                                vaPreOrderInvoiceTitleResponse.invoiceInfo = invoiceInfoList;//返回常用发票抬头信息列表
                                vaPreOrderInvoiceTitleResponse.invoiceStatus = (int)VAInvoiceTitleOperate.INVOICETITLE_QUERY;
                            }
                            else
                            {
                                vaPreOrderInvoiceTitleResponse.result = VAResult.VA_INVOICETITLE_NOT_FOUND;// 常用发票抬头信息未找到
                            }
                            break;
                            #endregion
                        case (int)VAInvoiceTitleOperate.INVOICETITLE_DELETE://发票抬头删除
                            #region 删除
                            if (vaPreOrderInvoiceTitleRequest.invoiceIdList.Count > 0)
                            {
                                //执行删除操作
                                int count = 0;
                                for (int i = 0; i < vaPreOrderInvoiceTitleRequest.invoiceIdList.Count; i++)
                                {
                                    if (preorder19dianMan.DeleteInvoiceInfo(vaPreOrderInvoiceTitleRequest.invoiceIdList[i]))
                                    {
                                        count++;
                                    }
                                }
                                if (count == vaPreOrderInvoiceTitleRequest.invoiceIdList.Count)
                                {
                                    DataTable dtCommonInvoiceTitleNew = preorder19dianMan.SelectInvoiceInfo(Common.ToInt32(checkResult.dtCustomer.Rows[0]["CustomerID"]));
                                    List<InvoiceInfo> invoiceInfoListNew = new List<InvoiceInfo>();
                                    if (dtCommonInvoiceTitleNew.Rows.Count > 0)
                                    {
                                        for (int i = 0; i < dtCommonInvoiceTitleNew.Rows.Count; i++)
                                        {
                                            InvoiceInfo invoiceInfo = new InvoiceInfo();
                                            invoiceInfo.invoiceId = Common.ToInt64(dtCommonInvoiceTitleNew.Rows[i]["invoiceId"]);
                                            invoiceInfo.invoiceTitle = Common.ToString(dtCommonInvoiceTitleNew.Rows[i]["invoiceTitle"]);
                                            invoiceInfoListNew.Add(invoiceInfo);
                                        }
                                    }
                                    scope.Complete();
                                    vaPreOrderInvoiceTitleResponse.result = VAResult.VA_OK;
                                    vaPreOrderInvoiceTitleResponse.invoiceInfo = invoiceInfoListNew;//返回删除完毕常用发票抬头信息列表
                                    vaPreOrderInvoiceTitleResponse.invoiceStatus = (int)VAInvoiceTitleOperate.INVOICETITLE_DELETE;
                                }
                                else
                                {
                                    vaPreOrderInvoiceTitleResponse.result = VAResult.VA_DELETEINVOICETITLE_ERROR;//发票抬头删除失败    
                                }
                            }
                            else
                            {
                                vaPreOrderInvoiceTitleResponse.result = VAResult.VA_DELETEINVOICETITLE_ERROR;//发票抬头删除失败
                            }
                            break;
                            #endregion
                        case (int)VAInvoiceTitleOperate.INVOICETITLE_ADD://发票抬头添加
                            #region 添加
                            if (vaPreOrderInvoiceTitleRequest.preOrder19dianId > 0)
                            {
                                try
                                {
                                    vaPreOrderInvoiceTitleRequest.invoiceTitle = vaPreOrderInvoiceTitleRequest.invoiceTitle.Replace("\r", "").Replace("\n", "");//去掉客户端回车和换行
                                    //更新点单信息表中发票抬头信息，插入当前信息到常用发票抬头信息表
                                    if (preorder19dianMan.UpdatePreOrder19dianInvoiceTitle(vaPreOrderInvoiceTitleRequest.preOrder19dianId, vaPreOrderInvoiceTitleRequest.invoiceTitle))
                                    {
                                        // 修改order表中的invoiceTitle信息 add by zhujinlei 2015/07/13
                                        //1、根据preOrder19DianId获取orderID
                                        var objPreOrder19Dian = preorder19dianMan.GetPreOrderById(vaPreOrderInvoiceTitleRequest.preOrder19dianId);
                                        if (objPreOrder19Dian != null)
                                        {
                                            var orderID = objPreOrder19Dian.OrderId;
                                            //2、根据orderID修改order表的发票抬头信息
                                            OrderManager objOrderManager = new OrderManager();
                                            var objOrder = objOrderManager.GetEntityById(orderID);
                                            if (objOrder != null)
                                            {
                                                objOrder.InvoiceTitle = vaPreOrderInvoiceTitleRequest.invoiceTitle;
                                                objOrderManager.Update(objOrder);
                                            }
                                        }
                                        // add end

                                        //查询获得当前传递过来的发票抬头在数据库中是否已经有记录
                                        DataTable dt = preorder19dianMan.SelectInvoiceInfo(Common.ToInt32(checkResult.dtCustomer.Rows[0]["CustomerID"]), vaPreOrderInvoiceTitleRequest.invoiceTitle);
                                        if (dt.Rows.Count == 0)//表示新填写的记录
                                        {
                                            if (preorder19dianMan.InsertInvoiceInfo(vaPreOrderInvoiceTitleRequest.invoiceTitle, Common.ToInt32(checkResult.dtCustomer.Rows[0]["CustomerID"])))
                                            {
                                                scope.Complete();
                                                vaPreOrderInvoiceTitleResponse.result = VAResult.VA_OK;
                                                vaPreOrderInvoiceTitleResponse.invoiceStatus = (int)VAInvoiceTitleOperate.INVOICETITLE_ADD;
                                            }
                                            else
                                            {
                                                vaPreOrderInvoiceTitleResponse.result = VAResult.VA_ADDINVOICETITLE_ERROR;//添加发票抬头失败 
                                            }
                                        }
                                        else//表示下拉选择的发票抬头（旧抬头）
                                        {
                                            scope.Complete();
                                            vaPreOrderInvoiceTitleResponse.result = VAResult.VA_OK;
                                            vaPreOrderInvoiceTitleResponse.invoiceStatus = (int)VAInvoiceTitleOperate.INVOICETITLE_ADD;
                                        }
                                    }
                                    else
                                    {
                                        vaPreOrderInvoiceTitleResponse.result = VAResult.VA_ADDINVOICETITLE_ERROR;//添加发票抬头失败
                                    }
                                }
                                catch (Exception)
                                {
                                    vaPreOrderInvoiceTitleResponse.result = VAResult.VA_ADDINVOICETITLE_ERROR;//添加发票抬头失败
                                }
                            }
                            else
                            {
                                vaPreOrderInvoiceTitleResponse.result = VAResult.VA_FAILED_PREORDER_NOT_FOUND;//当前点单未找到
                            }
                            break;
                            #endregion
                        case (int)VAInvoiceTitleOperate.OTHER://其他操作
                            break;
                    }
                }
            }
            else
            {
                vaPreOrderInvoiceTitleResponse.result = checkResult.result;
            }
            return vaPreOrderInvoiceTitleResponse;
        }
        /// <summary>
        /// 用户退款到余额20140313
        /// </summary>
        /// <param name="VAPreOrderRefundRequest"></param>
        /// <returns></returns>
        public VAPreOrderRefundResponse ClientRefund(VAPreOrderRefundRequest VAPreOrderRefundRequest, bool flag = false)
        {
            //客户端退款条件：点单支付，未验证
            CheckCookieAndMsgtypeInfo checkResult = new CheckCookieAndMsgtypeInfo();
            VAPreOrderRefundResponse VAPreOrderRefundResponse = new VAPreOrderRefundResponse();
            VAPreOrderRefundResponse.type = VAMessageType.CLIENT_PREORDER_REFUND_RESPONSE;
            VAPreOrderRefundResponse.cookie = VAPreOrderRefundRequest.cookie;
            VAPreOrderRefundResponse.uuid = VAPreOrderRefundRequest.uuid;
            double preOrderTotalAmount = 0;
            int preOrderTotalQuantity = 0;
            int currentPlatformVipGrade = 0;
            if (flag == true)//客服操作
            {
                checkResult.result = VAResult.VA_OK;
            }
            else//客户端请求
            {
                checkResult = Common.CheckCookieAndMsgtype(VAPreOrderRefundRequest.cookie,
              VAPreOrderRefundRequest.uuid, (int)VAPreOrderRefundRequest.type, (int)VAMessageType.CLIENT_PREORDER_REFUND_REQUEST);
                if (checkResult.result == VAResult.VA_OK)
                {
                    preOrderTotalAmount = Common.ToDouble(checkResult.dtCustomer.Rows[0]["preOrderTotalAmount"]);
                    preOrderTotalQuantity = Common.ToInt32(checkResult.dtCustomer.Rows[0]["preOrderTotalQuantity"]);
                    currentPlatformVipGrade = Common.ToInt32(checkResult.dtCustomer.Rows[0]["currentPlatformVipGrade"]);
                }
            }
            if (checkResult.result == VAResult.VA_OK)
            {

                PreOrder19dianOperate preOperate = new PreOrder19dianOperate();

                // 添加OrderID到Order表的查询 补差价 add by zhujinlei 2015/06/26
                List<PreOrder19dianInfo> listPreOrder19dianInfo = new List<PreOrder19dianInfo>();
                // 不为空表示客户端20150626后的新版本，传入了orderID
                if (VAPreOrderRefundRequest.orderId != Guid.Empty)
                {
                    listPreOrder19dianInfo = preOperate.GetPreOrder19dianByOrderId(VAPreOrderRefundRequest.orderId);
                }
                else
                {
                    listPreOrder19dianInfo.Add(new PreOrder19dianInfo() { preOrder19dianId = VAPreOrderRefundRequest.preOrder19dianId });
                }


                //  退款全部成功的状态
                bool isSuccess = false;
                string shopName = string.Empty;
                foreach (PreOrder19dianInfo objPreOrder19dianInfo in listPreOrder19dianInfo)
                {
                    isSuccess = false;
                    long preOrder19dianId = objPreOrder19dianInfo.preOrder19dianId;
                    Guid orderId = preorder19dianMan.GetPreOrderById(preOrder19dianId).OrderId;

                    //long preOrder19dianId = VAPreOrderRefundRequest.preOrder19dianId;
                    if (preOrder19dianId > 0)
                    {
                        //----------------------------------------------------------------------------------

                        AwardConnPreOrderOperate awardOperate = new AwardConnPreOrderOperate();
                        AwardConnPreOrder awardOrder = awardOperate.SelectAwardConnPreOrderByOrderId(VAPreOrderRefundRequest.preOrder19dianId);

                        //点单中奖且是免排队
                        if (awardOrder != null && awardOrder.Type == AwardType.AvoidQueue && awardOrder.LotteryTime.ToString("yyyy-MM-dd") == DateTime.Now.ToString("yyyy-MM-dd"))
                        {
                            AwardCacheLogic awardLogic = new AwardCacheLogic();

                            //提示当日不能退款
                            VAPreOrderRefundResponse.message = awardLogic.GetAwardConfig("CannotRefund", "");
                            VAPreOrderRefundResponse.result = VAResult.VA_PREORDER_REFUND_ERROR;//退款失败
                            return VAPreOrderRefundResponse;

                        }

                        //----------------------------------------------------------------------------------

                        DataTable dtPreOrderInfo = preOperate.QueryPreOrderById(preOrder19dianId);
                        if (Common.ToInt32(dtPreOrderInfo.Rows[0]["isPaid"]) == 1
                            //&& Common.ToInt32(dtPreOrderInfo.Rows[0]["isShopVerified"]) == 0
                            && Common.ToInt32(dtPreOrderInfo.Rows[0]["isApproved"]) == 0
                            && Common.ToInt32(dtPreOrderInfo.Rows[0]["isShopConfirmed"]) == 0
                            && Common.ToInt32(dtPreOrderInfo.Rows[0]["status"]) == (int)VAPreorderStatus.Prepaid)//支付，未审核、为对账
                        {
                            int companyId = Common.ToInt32(dtPreOrderInfo.Rows[0]["companyId"]);
                            int shopId = Common.ToInt32(dtPreOrderInfo.Rows[0]["shopId"]);
                            long customerId = Common.ToInt32(dtPreOrderInfo.Rows[0]["customerId"]);
                            double refundsum = Common.ToDouble(dtPreOrderInfo.Rows[0]["prePaidSum"]);//该点单支付金额                            
                            shopName = preorder19dianMan.GetShopNameByOrderId(preOrder19dianId);


                            //当前点单使用红包抵扣的金额
                            RedEnvelopeManager redEnvelopeManager = new RedEnvelopeManager();
                            List<RedEnvelopeConnOrder3> connPreOrder = redEnvelopeManager.SelectRedEnvelopeConnPreOrder3(preOrder19dianId);
                            double redEnvelopeConsumed = connPreOrder.Sum(q => q.currectUsedAmount);

                            //修改预点单状态
                            using (TransactionScope scope = new TransactionScope())
                            {
                                bool flagUpdateRefundRedEnvelope = false;
                                if (redEnvelopeConsumed > 0.001)
                                {
                                    flagUpdateRefundRedEnvelope = ClientRefundRedEnvelope(preOrder19dianId, connPreOrder, redEnvelopeConsumed, customerId, flagUpdateRefundRedEnvelope);
                                }
                                else
                                {
                                    flagUpdateRefundRedEnvelope = true;
                                }
                                bool resultCoupon = true;
                                //返还用户优惠券
                                CouponGetDetailOperate couponGetDetailOperate = new CouponGetDetailOperate();
                                resultCoupon = couponGetDetailOperate.RefundCustomerCoupon(preOrder19dianId, shopName);

                                double extendPay = preorder19dianMan.SelectExtendPay(preOrder19dianId);//额外收取的钱

                                //用户
                                Money19dianDetail money19dianDetail = new Money19dianDetail
                                {
                                    customerId = customerId,
                                    changeReason = Common.GetEnumDescription(VA19dianMoneyChangeReason.MONEY19DIAN_REFUND_PREORDER).Replace("{0}", Common.ToString(preOrder19dianId)),
                                    changeTime = System.DateTime.Now,
                                    changeValue = refundsum - redEnvelopeConsumed + extendPay,//若用户申请退到余额，红包返还，要加上额外收取的钱
                                    remainMoney = Money19dianDetailManager.GetCustomerRemainMoney(customerId) + refundsum - redEnvelopeConsumed + extendPay,//若用户申请退到余额，红包返还，要加上额外收取的钱
                                    flowNumber = SybMoneyOperate.CreateCustomerFlowNumber(customerId),//流水号
                                    accountType = (int)AccountType.USER_CANCEL_ORDER,//用户取消订单
                                    accountTypeConnId = Common.ToString(preOrder19dianId),
                                    inoutcomeType = (int)InoutcomeType.IN,//相对于用户退款就是收入
                                    companyId = companyId,
                                    shopId = shopId
                                };

                                bool result1 = preorder19dianMan.UpdatePreOrderRefundMoney(preOrder19dianId, VAPreorderStatus.Refund, refundsum, money19dianDetail.changeValue);
                                // Order订单表是否退款成功
                                bool isOrderReturnMoney = true;
                                if (VAPreOrderRefundRequest.orderId != Guid.Empty)
                                {
                                    isOrderReturnMoney = preorder19dianMan.UpdateOrderRefundMoney(VAPreOrderRefundRequest.orderId, VAPreorderStatus.Prepaid, refundsum, money19dianDetail.changeValue);
                                }

                                bool result2 = SybMoneyCustomerOperate.AccountBalanceChanges(money19dianDetail);

                                bool result4 = Common.InsertRefundData(customerId, refundsum, preOrder19dianId, "客户端退款到余额");//记录退款记录

                                //整单退款，要更新支付明细表的退款金额及状态，Add at 2015-4-15
                                Preorder19DianLineManager lineManager = new Preorder19DianLineManager();
                                Model.QueryObject.Preorder19DianLineQueryObject queryObject = new Model.QueryObject.Preorder19DianLineQueryObject()
                                {
                                    Preorder19DianId = preOrder19dianId
                                };
                                List<IPreorder19DianLine> orderLineList = lineManager.GetListByQuery(queryObject);
                                bool updateLine = false;
                                foreach (IPreorder19DianLine line in orderLineList)
                                {
                                    line.RefundAmount = line.Amount;//整单退，所以退款金额==支付金额
                                    updateLine = lineManager.Update(line);//更新每种支付类型的退款金额
                                }

                                //----------------------------------------------------------------------------------

                                //全额退款时，中奖赠送的红包作废
                                bool cancelAwardRedEnvelope = awardOperate.CancelAwardRedEnvelope(preOrder19dianId);

                                //----------------------------------------------------------------------------------

                                if (result1 && result2 && result4 && flagUpdateRefundRedEnvelope && resultCoupon && updateLine && cancelAwardRedEnvelope)
                                {
                                    scope.Complete();
                                    isSuccess = true;
                                    VAPreOrderRefundResponse.result = VAResult.VA_OK;

                                    // 老版本发一次推送 preOrder19dianId不为空,OrderID为空 // 新版本涉及补差价， 推送方法提到循环外
                                    if (VAPreOrderRefundRequest.orderId == Guid.Empty)
                                    {
                                        VAPreOrderRefundRequest.orderId = orderId;
                                        isSuccess = true;
                                        CustomPushRecordOperate customPushRecordOperate = new CustomPushRecordOperate();
                                        UniPushInfo uniPushInfo = new UniPushInfo()
                                        {
                                            customerPhone = Common.ToString(checkResult.dtCustomer.Rows[0]["mobilePhoneNumber"]),
                                            preOrder19dianId = preOrder19dianId,
                                            shopName = shopName,
                                            pushMessage = VAPushMessage.退款成功,
                                            clientBuild = VAPreOrderRefundRequest.clientBuild
                                        };
                                        Thread refundThread = new Thread(customPushRecordOperate.UniPush);
                                        refundThread.Start(uniPushInfo);
                                    }
                                }
                                else
                                {
                                    VAPreOrderRefundResponse.result = VAResult.VA_PREORDER_REFUND_ERROR;//退款失败
                                }
                            }
                        }
                        else
                        {
                            VAPreOrderRefundResponse.result = VAResult.VA_REFUND_NOTPAYORCONFIRM;//已验证或是未付款
                        }
                    }
                    else
                    {
                        VAPreOrderRefundResponse.result = VAResult.VA_FAILED_PREORDER_NOT_FOUND;//当前点单未找到
                    }
                }
                // 全部退款成功才去修改Order表的状态 并且推送消息
                if (isSuccess)
                {
                    preorder19dianMan.UpdateOrderRefundMoney(VAPreOrderRefundRequest.orderId, VAPreorderStatus.Refund, 0, 0);

                    CustomPushRecordOperate customPushRecordOperate = new CustomPushRecordOperate();
                    UniPushInfo uniPushInfo = new UniPushInfo()
                    {
                        customerPhone = Common.ToString(checkResult.dtCustomer.Rows[0]["mobilePhoneNumber"]),
                        orderId = VAPreOrderRefundRequest.orderId,
                        shopName = shopName,
                        pushMessage = VAPushMessage.退款成功,
                        clientBuild = VAPreOrderRefundRequest.clientBuild
                    };
                    Thread refundThread = new Thread(customPushRecordOperate.UniPush);
                    refundThread.Start(uniPushInfo);
                }
            }
            else
            {
                VAPreOrderRefundResponse.result = checkResult.result;
            }
            return VAPreOrderRefundResponse;
        }



        /// <summary>
        /// 用户申请原路退款20140313
        /// </summary>
        /// <param name="VAPreOrderRefundRequest"></param>
        /// <returns></returns>
        public VAOriginalRefundResponse ClientOriginalRefund(VAOriginalRefundRequest originalRefundRequest)
        {
            //客户端退款条件：点单支付，未验证
            CheckCookieAndMsgtypeInfo checkResult = new CheckCookieAndMsgtypeInfo();
            VAOriginalRefundResponse originalRefundResponse = new VAOriginalRefundResponse();
            originalRefundResponse.type = VAMessageType.CLIENT_ORIGINAL_REFUNF_REPONSE;
            originalRefundResponse.cookie = originalRefundRequest.cookie;
            originalRefundResponse.uuid = originalRefundRequest.uuid;
            checkResult = Common.CheckCookieAndMsgtype(originalRefundRequest.cookie,
              originalRefundRequest.uuid, (int)originalRefundRequest.type, (int)VAMessageType.CLIENT_ORIGINAL_REFUNF_REQUEST);
            if (checkResult.result == VAResult.VA_OK)
            {
                PreOrder19dianOperate preOperate = new PreOrder19dianOperate();

                // 添加OrderID到Order表的查询 补差价 add by zhujinlei 2015/06/26
                List<PreOrder19dianInfo> listPreOrder19dianInfo = new List<PreOrder19dianInfo>();
                // 不为空表示客户端20150626后的新版本，传入了orderID
                if (originalRefundRequest.orderId != Guid.Empty)
                {
                    listPreOrder19dianInfo = preOperate.GetPreOrder19dianByOrderId(originalRefundRequest.orderId);
                }
                else
                {
                    listPreOrder19dianInfo.Add(new PreOrder19dianInfo() { preOrder19dianId = originalRefundRequest.preOrder19dianId });
                }

                //----------------------------------------------------------------------------------

                AwardConnPreOrderOperate awardOperate = new AwardConnPreOrderOperate();
                AwardConnPreOrder awardOrder = awardOperate.SelectAwardConnPreOrderByOrderId(originalRefundRequest.preOrder19dianId);

                //点单中奖且是免排队
                if (awardOrder != null && awardOrder.PreOrder19dianId > 0 && awardOrder.Type == AwardType.AvoidQueue && awardOrder.LotteryTime.ToString("yyyy-MM-dd") == DateTime.Now.ToString("yyyy-MM-dd"))
                {
                    AwardCacheLogic awardLogic = new AwardCacheLogic();

                    //提示当日不能退款
                    originalRefundResponse.message = awardLogic.GetAwardConfig("CannotRefund", "");
                    originalRefundResponse.result = VAResult.VA_PREORDER_REFUND_ERROR;//退款失败
                    return originalRefundResponse;
                }

                //----------------------------------------------------------------------------------



                double preOrderTotalAmount = Common.ToDouble(checkResult.dtCustomer.Rows[0]["preOrderTotalAmount"]);//订单消费总额
                int preOrderTotalQuantity = Common.ToInt32(checkResult.dtCustomer.Rows[0]["preOrderTotalQuantity"]);//订单消费次数
                int currentPlatformVipGrade = Common.ToInt32(checkResult.dtCustomer.Rows[0]["currentPlatformVipGrade"]);//会员等级

                //  退款全部成功后更改状态标识
                bool isSuccess = false;
                string shopName = string.Empty;
                double payAmounts = 0;
                foreach (PreOrder19dianInfo objPreOrder19dianInfo in listPreOrder19dianInfo)
                {
                    isSuccess = false;
                    long preOrder19dianId = objPreOrder19dianInfo.preOrder19dianId;
                    Guid orderId = preorder19dianMan.GetPreOrderById(preOrder19dianId).OrderId;


                    if (preOrder19dianId > 0)
                    {
                        ThirdPartyPaymentInfo thirdPartyPaymentInfo = preorder19dianMan.SelectPreorderPayAmount(preOrder19dianId);//该点单使用第三方支付的金额
                        double payAmount = thirdPartyPaymentInfo.Amount;
                        payAmounts += payAmount;
                        RedEnvelopeConnPreOrderOperate redEnvelopeOperate = new RedEnvelopeConnPreOrderOperate();
                        RedEnvelopeManager redEnvelopeManager = new RedEnvelopeManager();
                        List<RedEnvelopeConnOrder3> connPreOrder = redEnvelopeManager.SelectRedEnvelopeConnPreOrder3(preOrder19dianId);
                        double redEnvelopeConsumed = connPreOrder.Sum(q => q.currectUsedAmount);//当前点单使用红包抵扣的金额

                        DataTable dtPreOrderInfo = preOperate.QueryPreOrderById(preOrder19dianId);
                        if (Common.ToInt32(dtPreOrderInfo.Rows[0]["isPaid"]) == 1 //支付成功
                            //&& Common.ToInt32(dtPreOrderInfo.Rows[0]["isShopVerified"]) == 0 //验证2维码,不用了
                            && Common.ToInt32(dtPreOrderInfo.Rows[0]["isApproved"]) == 0  //服务员未审核
                            && Common.ToInt32(dtPreOrderInfo.Rows[0]["isShopConfirmed"]) == 0 //商家为对帐
                            && Common.ToInt32(dtPreOrderInfo.Rows[0]["status"]) == (int)VAPreorderStatus.Prepaid)//支付，未审核、为对账
                        {
                            int companyId = Common.ToInt32(dtPreOrderInfo.Rows[0]["companyId"]);
                            int shopId = Common.ToInt32(dtPreOrderInfo.Rows[0]["shopId"]);
                            long customerId = Common.ToInt32(dtPreOrderInfo.Rows[0]["customerId"]);
                            double prePaidSum = Common.ToDouble(dtPreOrderInfo.Rows[0]["prePaidSum"]);//该点单支付金额
                            shopName = preorder19dianMan.GetShopNameByOrderId(preOrder19dianId);

                            double cumulativeAmount = preOrderTotalAmount - prePaidSum;
                            //修改预点单状态

                            double extendPay = preorder19dianMan.SelectExtendPay(preOrder19dianId);//额外收取的钱

                            //double amountToMoneyRemain = Common.ToDouble(prePaidSum - (payAmount - extendPay) - redEnvelopeConsumed); //粮票 = 支付总额 - (第三方支付金额 - 额外收取的钱) - 红包抵扣金额
                            double amountToMoneyRemain = Common.ToDouble(prePaidSum - payAmount - redEnvelopeConsumed + extendPay); //粮票 = 支付总额 - 第三方支付金额 - 红包抵扣金额 + 额外收取的钱

                            bool flagUpdateCustomerMoneyRemain = true;
                            bool flagUpdatePreOrderRefundMoneyClosedSum = true;
                            bool flagUpdateRefundRedEnvelope = false;

                            using (TransactionScope scope = new TransactionScope())
                            {
                                if (amountToMoneyRemain > 0)
                                {//操作用户余额
                                    Money19dianDetail money19dianDetail = new Money19dianDetail
                                    {
                                        customerId = customerId,
                                        changeReason = Common.GetEnumDescription(VA19dianMoneyChangeReason.MONEY19DIAN_REFUND_PREORDER).Replace("{0}", Common.ToString(preOrder19dianId)),
                                        changeTime = System.DateTime.Now,
                                        changeValue = amountToMoneyRemain,
                                        remainMoney = Money19dianDetailManager.GetCustomerRemainMoney(customerId) + amountToMoneyRemain,//
                                        flowNumber = SybMoneyOperate.CreateCustomerFlowNumber(customerId),//流水号
                                        accountType = (int)AccountType.USER_CANCEL_ORDER,//用户取消订单
                                        accountTypeConnId = Common.ToString(preOrder19dianId),
                                        inoutcomeType = (int)InoutcomeType.IN,//相对于用户退款就是收入
                                        companyId = companyId,
                                        shopId = shopId
                                    };
                                    flagUpdateCustomerMoneyRemain = SybMoneyCustomerOperate.AccountBalanceChanges(money19dianDetail);
                                    flagUpdatePreOrderRefundMoneyClosedSum = preorder19dianMan.UpdatePreOrderRefundMoneyClosedSum(preOrder19dianId, money19dianDetail.changeValue);
                                }
                                if (redEnvelopeConsumed > 0.001)
                                {
                                    flagUpdateRefundRedEnvelope = ClientRefundRedEnvelope(preOrder19dianId, connPreOrder, redEnvelopeConsumed, customerId, flagUpdateRefundRedEnvelope);
                                }
                                else
                                {
                                    flagUpdateRefundRedEnvelope = true;
                                }
                                bool resultCoupon = true;
                                //返还用户优惠券
                                CouponGetDetailOperate couponGetDetailOperate = new CouponGetDetailOperate();
                                resultCoupon = couponGetDetailOperate.RefundCustomerCoupon(preOrder19dianId, shopName);

                                OriginalRoadRefundInfo originalRoadRefund = new OriginalRoadRefundInfo();
                                originalRoadRefund.type = VAOriginalRefundType.PREORDER;
                                originalRoadRefund.connId = preOrder19dianId;
                                originalRoadRefund.customerMobilephone = Common.ToString(checkResult.dtCustomer.Rows[0]["mobilePhoneNumber"]);
                                originalRoadRefund.customerUserName = Common.ToString(checkResult.dtCustomer.Rows[0]["UserName"]);
                                originalRoadRefund.refundAmount = payAmount;
                                originalRoadRefund.status = (int)VAOriginalRefundStatus.REMITTING;
                                originalRoadRefund.employeeId = originalRefundRequest.employeeId;
                                bool flagOriginalRefundRecord = false;
                                bool updatePreorderStatus = false;
                                if (payAmount > 0.009)
                                {
                                    switch (thirdPartyPaymentInfo.Type)
                                    {
                                        case PayType.微信支付:
                                            originalRoadRefund.RefundPayType = RefundPayType.微信;
                                            break;
                                        case PayType.支付宝:
                                            originalRoadRefund.RefundPayType = RefundPayType.支付宝;
                                            break;
                                        default:
                                            break;
                                    }

                                    originalRoadRefund.id = preorder19dianMan.InsertOriginalRoadRefund(originalRoadRefund);
                                    if (originalRoadRefund.id > 0)
                                    {//记录原路退款记录
                                        flagOriginalRefundRecord = true;
                                        originalRoadRefund.tradeNo = thirdPartyPaymentInfo.tradeNo;
                                        ThreadPool.QueueUserWorkItem(
                                            new RefundCallBack(preOrder19dianId, thirdPartyPaymentInfo,
                                                (float)originalRoadRefund.refundAmount, originalRoadRefund).Refund);
                                    }
                                    else
                                    {
                                        flagOriginalRefundRecord = false;
                                    }
                                    updatePreorderStatus = preorder19dianMan.UpdatePreOrderRefundInfo(preOrder19dianId, VAPreorderStatus.OriginalRefunding, prePaidSum);
                                }
                                else
                                {
                                    flagOriginalRefundRecord = true;
                                    updatePreorderStatus = preorder19dianMan.UpdatePreOrderRefundInfo(preOrder19dianId, VAPreorderStatus.Refund, prePaidSum);
                                }
                                bool refundData = Common.InsertRefundData(customerId, prePaidSum, preOrder19dianId, "客户端原路退款");//记录退款记录                            
                                bool result7 = MealScheduleManager.MinusSoldOutCount(preOrder19dianId);

                                //整单退款，要更新支付明细表的退款金额及状态，Add at 2015-4-15
                                Preorder19DianLineManager lineManager = new Preorder19DianLineManager();
                                Model.QueryObject.Preorder19DianLineQueryObject queryObject = new Model.QueryObject.Preorder19DianLineQueryObject()
                                {
                                    Preorder19DianId = preOrder19dianId
                                };
                                List<IPreorder19DianLine> orderLineList = lineManager.GetListByQuery(queryObject);
                                bool updateLine = false;
                                foreach (IPreorder19DianLine line in orderLineList)
                                {
                                    line.RefundAmount = line.Amount;//整单退，所以退款金额==支付金额
                                    updateLine = lineManager.Update(line);//更新每种支付类型的退款金额
                                }
                                // 修改订单表order表的退款金额 add by zhujinlei 2015/06/26
                                bool isPreOrderReturnMoney = true;
                                if (originalRefundRequest.orderId != Guid.Empty)
                                {
                                    isPreOrderReturnMoney = preorder19dianMan.UpdateOrderRefundMoney(originalRefundRequest.orderId, VAPreorderStatus.Prepaid, prePaidSum, prePaidSum);
                                }

                                //----------------------------------------------------------------------------------

                                //全额退款时，中奖赠送的红包作废

                                bool cancelAwardRedEnvelope = awardOperate.CancelAwardRedEnvelope(originalRefundRequest.preOrder19dianId);

                                //----------------------------------------------------------------------------------


                                if (updatePreorderStatus && flagOriginalRefundRecord && refundData && flagUpdateCustomerMoneyRemain && flagUpdatePreOrderRefundMoneyClosedSum && flagUpdateRefundRedEnvelope && result7 && resultCoupon && updateLine && cancelAwardRedEnvelope)//&& viewallocBalance
                                {
                                    scope.Complete();
                                    originalRefundResponse.result = VAResult.VA_OK;
                                    originalRefundResponse.moneyBackToMoneyRemain = amountToMoneyRemain.ToString();
                                    originalRefundResponse.moneyOriginalRefund = payAmount.ToString();

                                    // 老版本这里发送一次推送，新版本补差价可能存在多次循环固提到循环外
                                    if (originalRefundRequest.orderId == Guid.Empty)
                                    {
                                        isSuccess = true;
                                        originalRefundRequest.orderId = orderId;
                                        CustomPushRecordOperate customPushRecordOperate = new CustomPushRecordOperate();

                                        UniPushInfo uniPushInfo = new UniPushInfo()
                                        {
                                            customerPhone = Common.ToString(checkResult.dtCustomer.Rows[0]["mobilePhoneNumber"]),
                                            preOrder19dianId = preOrder19dianId,
                                            shopName = shopName,
                                            pushMessage = VAPushMessage.退款成功,
                                            clientBuild = originalRefundRequest.clientBuild
                                        };
                                        Thread refundThread = new Thread(customPushRecordOperate.UniPush);
                                        refundThread.Start(uniPushInfo);
                                    }
                                }
                                else
                                {
                                    originalRefundResponse.result = VAResult.VA_PREORDER_REFUND_ERROR;//退款失败
                                }
                            }
                        }
                        else
                        {
                            originalRefundResponse.result = VAResult.VA_REFUND_NOTPAYORCONFIRM;//已验证或是未付款
                        }
                    }
                    else
                    {
                        originalRefundResponse.result = VAResult.VA_FAILED_PREORDER_NOT_FOUND;//当前点单未找到
                    }
                }
                // 全部退款成功才去修改Order表的状态
                if (isSuccess)
                {
                    VAPreorderStatus status = VAPreorderStatus.Refund;
                    if (payAmounts != 0)
                        status = VAPreorderStatus.OriginalRefunding;
                    preorder19dianMan.UpdateOrderRefundMoney(originalRefundRequest.orderId, status, 0, 0);

                    CustomPushRecordOperate customPushRecordOperate = new CustomPushRecordOperate();

                    UniPushInfo uniPushInfo = new UniPushInfo()
                    {
                        customerPhone = Common.ToString(checkResult.dtCustomer.Rows[0]["mobilePhoneNumber"]),
                        orderId = originalRefundRequest.orderId,
                        shopName = shopName,
                        pushMessage = VAPushMessage.退款成功,
                        clientBuild = originalRefundRequest.clientBuild
                    };
                    Thread refundThread = new Thread(customPushRecordOperate.UniPush);
                    refundThread.Start(uniPushInfo);
                }
            }
            else
            {
                originalRefundResponse.result = checkResult.result;
            }
            return originalRefundResponse;
        }









        private static object obj = new object();
        public static void WriteLog(string str)
        {
            try
            {
                //文件路径
                string fname = string.Format("{0}MyLog\\{1}.txt", AppDomain.CurrentDomain.BaseDirectory, DateTime.Now.ToString("yyyy-MM-dd"));

                lock (obj)
                {
                    //定义文件信息对象
                    FileInfo finfo = new FileInfo(fname);

                    if (!finfo.Exists)
                    {
                        // 如果目录不存在，则创建目录
                        string filePath = string.Format("{0}MyLog", AppDomain.CurrentDomain.BaseDirectory);
                        if (!Directory.Exists(filePath))
                        {
                            Directory.CreateDirectory(filePath);
                        }

                        // 创建日志文件
                        using (StreamWriter sw = File.CreateText(fname))
                        {
                            finfo = new FileInfo(fname);
                        }
                    }


                    //创建只写文件流
                    using (FileStream fs = finfo.OpenWrite())
                    {

                        //根据上面创建的文件流创建写数据流
                        StreamWriter w = new StreamWriter(fs);
                        //设置写数据流的起始位置为文件流的末尾
                        w.BaseStream.Seek(0, SeekOrigin.End);
                        w.WriteLine(str.ToString());
                        //清空缓冲区内容，并把缓冲区内容写入基础流
                        w.Flush();
                        //关闭写数据流
                        w.Close();
                        w.Dispose();

                        fs.Close();
                        fs.Dispose();

                    }
                }
            }
            catch (Exception eMsg)
            {
                string strRe = eMsg.ToString();
            }
        }










        private bool ClientRefundRedEnvelope(long preOrder19dianId, List<RedEnvelopeConnOrder3> connPreOrder, double redEnvelopeConsumed, long customerId, bool flagUpdateRefundRedEnvelope)
        {
            if (connPreOrder != null && connPreOrder.Count > 0)
            {
                RedEnvelopeManager redEnvelopeManager = new RedEnvelopeManager();
                using (TransactionScope ts = new TransactionScope())
                {
                    //1. 更新点单表退款相关金额                                   
                    bool a = preorder19dianMan.UpdatePreOrderRefundRedEnvelope(preOrder19dianId, redEnvelopeConsumed, redEnvelopeConsumed);

                    //2. 更新红包Detail表，支付记录应该加上的钱
                    //bool b = true;
                    //b = redEnvelopeManager.updateRedEnvelopePaidAmount(preOrder19dianId, redEnvelopeConsumed);
                    //if (b)
                    //{
                    //    b = redEnvelopeManager.UpdateCustomerRedEnvelope2(customerId, redEnvelopeConsumed);
                    //}

                    //3.红包表应该更改状态的 RedEnvelopeId 
                    bool c = true;

                    foreach (RedEnvelopeConnOrder3 order in connPreOrder)
                    {
                        c = redEnvelopeManager.UpdateRedEnvelopeStatus(order.redEnvelopeId, order.currectUsedAmount);
                        if (!c)
                        {
                            break;
                        }
                    }
                    if (a && c)
                    {
                        ts.Complete();
                        flagUpdateRefundRedEnvelope = true;
                    }
                    else
                    {
                        flagUpdateRefundRedEnvelope = false;
                    }
                }
            }
            else
            {
                flagUpdateRefundRedEnvelope = false;
            }
            return flagUpdateRefundRedEnvelope;
        }


        /// <summary>
        /// 获取账户明细数据20140313
        /// </summary>
        /// <param name="accountDetailRequest"></param>
        /// <returns></returns>
        public VAAccountDetailResponse QueryAccountDetail(VAAccountDetailRequest accountDetailRequest)
        {
            VAAccountDetailResponse accountDetailResponse = new VAAccountDetailResponse();
            accountDetailResponse.type = VAMessageType.ACCOUNTDETAIL_RESPONSE;
            accountDetailResponse.cookie = accountDetailRequest.cookie;
            accountDetailResponse.uuid = accountDetailRequest.uuid;
            CheckCookieAndMsgtypeInfo checkResult = Common.CheckCookieAndMsgtype(accountDetailRequest.cookie,
                accountDetailRequest.uuid, (int)accountDetailRequest.type, (int)VAMessageType.ACCOUNTDETAIL_REQUEST);
            if (checkResult.result == VAResult.VA_OK)
            {
                long customerId = Common.ToInt64(checkResult.dtCustomer.Rows[0]["CustomerID"]);
                DataTable dtAccount = Common.GetDataTableFieldValueOrderby("Money19dianDetail", "changeReason,changeValue,changeTime,accountType,companyId,shopId",
                    "customerId ='" + customerId + "'", "changeTime desc");

                List<VAAccountDetail> accountDetailList = new List<VAAccountDetail>();
                for (int i = 0; i < dtAccount.Rows.Count; i++)
                {
                    VAAccountDetail accountDetail = new VAAccountDetail();
                    accountDetail.accountTime = Common.ToString(Common.ToDateTime(dtAccount.Rows[i]["changeTime"]).ToString("yyyy-MM-dd HH:mm"));
                    accountDetail.accountPrice = Common.ToString(dtAccount.Rows[i]["changeValue"]);
                    switch (Common.ToInt32(dtAccount.Rows[i]["accountType"]))
                    {
                        case (int)AccountType.ALIPAY:
                            {
                                accountDetail.accountName = "充值-支付宝账户码";
                                accountDetail.accountStatus = "交易成功";
                            } break;
                        case (int)AccountType.UNIONPAY:
                            {
                                accountDetail.accountName = "充值-银联账户码";
                                accountDetail.accountStatus = "交易成功";
                            } break;
                        case (int)AccountType.USER_CANCEL_ORDER://取消订单
                            {
                                int companyId = Common.ToInt32(dtAccount.Rows[i]["companyId"]);
                                int shopId = Common.ToInt32(dtAccount.Rows[i]["shopId"]);
                                string companyName = "";
                                string shopName = "";
                                if (companyId != 0)
                                {
                                    companyName = Common.GetFieldValue("CompanyInfo", "companyName", "companyID ='" + companyId + "'");
                                }
                                if (shopId != 0)
                                {
                                    shopName = Common.GetFieldValue("ShopInfo", "shopName", "shopID ='" + shopId + "'");
                                }
                                accountDetail.accountName = "取消订单-" + companyName + "(" + shopName + ")";
                                accountDetail.accountStatus = "取消订单成功";
                            } break;
                        case (int)AccountType.ORDER_OUTCOME://收银宝退款
                            {
                                int companyId = Common.ToInt32(dtAccount.Rows[i]["companyId"]);
                                int shopId = Common.ToInt32(dtAccount.Rows[i]["shopId"]);
                                string companyName = "";
                                string shopName = "";
                                if (companyId != 0)
                                {
                                    companyName = Common.GetFieldValue("CompanyInfo", "companyName", "companyID ='" + companyId + "'");
                                }
                                if (shopId != 0)
                                {
                                    shopName = Common.GetFieldValue("ShopInfo", "shopName", "shopID ='" + shopId + "'");
                                }
                                accountDetail.accountName = "商户退款-" + companyName + "(" + shopName + ")";
                                accountDetail.accountStatus = "退款成功";
                            } break;
                        case (int)AccountType.USER_CONSUME:
                            {
                                int companyId = Common.ToInt32(dtAccount.Rows[i]["companyId"]);
                                int shopId = Common.ToInt32(dtAccount.Rows[i]["shopId"]);
                                string companyName = "";
                                string shopName = "";
                                if (companyId != 0)
                                {
                                    companyName = Common.GetFieldValue("CompanyInfo", "companyName", "companyID ='" + companyId + "'");
                                }
                                if (shopId != 0)
                                {
                                    shopName = Common.GetFieldValue("ShopInfo", "shopName", "shopID ='" + shopId + "'");
                                }
                                accountDetail.accountName = "消费-" + companyName + "(" + shopName + ")";
                                accountDetail.accountStatus = "交易成功";

                            } break;
                        case (int)AccountType.BUY_COUPON:
                            {
                                accountDetail.accountName = "购买优惠劵";
                                accountDetail.accountStatus = "交易成功";

                            } break;
                        default:
                            {
                                accountDetail.accountStatus = "交易成功";
                                if (Common.ToDouble(dtAccount.Rows[i]["changeValue"]) > 0)
                                {
                                    accountDetail.accountName = "充值";
                                }
                                else
                                {
                                    accountDetail.accountName = "消费";
                                }
                            } break;

                    }
                    accountDetailList.Add(accountDetail);
                }
                accountDetailResponse.accountDetailList = accountDetailList;
                accountDetailResponse.result = VAResult.VA_OK;
            }
            else
            {
                accountDetailResponse.result = checkResult.result;
            }
            return accountDetailResponse;
        }

        /// <summary>
        /// 新增原路返回申请单信息
        /// </summary>
        /// <param name="originalRoadRefund"></param>
        /// <returns></returns>
        public long InsertOriginalRoadRefund(OriginalRoadRefundInfo originalRoadRefund)
        {
            return preorder19dianMan.InsertOriginalRoadRefund(originalRoadRefund);
        }

        public DataTable SybSelectPreOrder(Guid preorderId)
        {
            return preorder19dianMan.SybSelectPreOrder(preorderId);
        }

        /// <summary>
        /// 查询订单表数据
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public DataTable SybSelectOrder(Guid orderId)
        {
            return preorder19dianMan.SybSelectOrder(orderId);
        }

        public PreOrder19dianInfo GetPreOrder19dianById(long preOrder19dian)
        {
            return preorder19dianMan.GetPreOrderById(preOrder19dian);
        }

        public List<PreOrder19dianInfo> GetPreOrder19dianByOrderId(Guid orderId)
        {
            return preorder19dianMan.GetPreOrder19dianByOrderId(orderId);
        }

        /// <summary>
        /// 根据客户Id找到其所有已经支付的点单
        /// 2014-4-11
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public DataTable QueryPaidOrderByCustomerId(long customerId)
        {
            return preorder19dianMan.SelectPaidOrderByCustomerId(customerId);
        }

        /// <summary>
        /// 返回分页用户订单
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="startIndex"></param>
        /// <param name="endIndex"></param>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public Tuple<int, DataTable> GetPagePaidOrderByCustomerId(long customerId, int startIndex, int endIndex, long orderId = 0)
        {
            return preorder19dianMan.GetPagePaidOrderByCustomerId(customerId, startIndex, endIndex, orderId);
        }


        /// <summary>
        /// 检查某订单是否已申请过原路退款
        /// </summary>
        /// <param name="preOrder19dianId"></param>
        /// <returns>True:申请过</returns>
        public bool IsOriginalRoadRefunded(long preOrder19dianId)
        {
            return preorder19dianMan.IsOriginalRoadRefunded(preOrder19dianId);
        }

        /// <summary>
        /// 根据门店查询已经验证过的预点单信息
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public DataTable QueryPreOrderShopVerified(int shopId, DateTime timeFrom, DateTime timeTo, int status)
        {
            DataTable dtPreOrderShopVerified = preorder19dianMan.SelectPreOrderShopVerified(shopId, timeFrom, timeTo, status, true);
            return dtPreOrderShopVerified;
        }
        /// <summary>
        /// 根据公司id，查询公司预点单信息
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="shopId"></param>
        /// <param name="timeFrom"></param>
        /// <param name="timeTo"></param>
        /// <param name="queryCondition"></param>
        /// <returns></returns>
        public DataTable QueryPreOrder19dian(int companyId, int shopId, DateTime timeFrom, DateTime timeTo, string queryCondition)
        {
            DataTable dtPreOrder19dian = new DataTable();
            switch (queryCondition)
            {
                //case "zy":
                // dtPreOrder19dian = preorder19dianMan.SelectPreOrderShopVerified(shopId, timeFrom, timeTo, 2, true, companyId);
                // break;
                case "n":
                    dtPreOrder19dian = preorder19dianMan.SelectPreOrderShopVerified(shopId, timeFrom, timeTo, 2, false, companyId);
                    break;
                case "y":
                    dtPreOrder19dian = preorder19dianMan.SelectPreOrderShopVerified(shopId, timeFrom, timeTo, 2, true, companyId);
                    break;
                default:
                    dtPreOrder19dian = preorder19dianMan.SelectPreOrderShopVerified(shopId, timeFrom, timeTo, 2, true, companyId, true);
                    break;
            }
            #region 服务器再次计算菜价格
            for (int i = 0; i < dtPreOrder19dian.Rows.Count; i++)
            {
                if (Common.ToDouble(dtPreOrder19dian.Rows[i]["preOrderServerSum"]) > 0)
                {
                    continue;
                }
                else
                {
                    long customerId = Common.ToInt64(dtPreOrder19dian.Rows[i]["customerId"]);
                    string orderInJson = Common.ToString(dtPreOrder19dian.Rows[i]["orderInJson"]);
                    string preOrderServerSum = CalcPreorderServerSum(companyId, customerId, orderInJson);
                    dtPreOrder19dian.Rows[i]["preOrderServerSum"] = preOrderServerSum;
                }
            }
            #endregion
            return dtPreOrder19dian;
        }

        public VAClientPreOrderConfirmResponse ClientPreOrderConfirm(VAClientPreOrderConfirmRequest preOrderConfirmRequest)
        {
            VAClientPreOrderConfirmResponse clientPreOrderConfirmResponse = new VAClientPreOrderConfirmResponse();
            clientPreOrderConfirmResponse.type = VAMessageType.CLIENT_PREORDER_CONFIRM_RESPONSE;
            clientPreOrderConfirmResponse.cookie = preOrderConfirmRequest.cookie;
            clientPreOrderConfirmResponse.uuid = preOrderConfirmRequest.uuid;
            CheckCookieAndMsgtypeInfo checkResult = Common.CheckCookieAndMsgtype(preOrderConfirmRequest.cookie, preOrderConfirmRequest.uuid, (int)preOrderConfirmRequest.type, (int)VAMessageType.CLIENT_PREORDER_CONFIRM_REQUEST, false);
            if (checkResult.result == VAResult.VA_OK)
            {

                SybMoneyMerchantOperate syb = new SybMoneyMerchantOperate();
                int returnResult = syb.ConfrimPreOrder(preOrderConfirmRequest.preOrder19dianId, 1, PreOrderConfirmOperater.Client, 29);
                switch (returnResult)
                {
                    case -3:
                        clientPreOrderConfirmResponse.result = VAResult.VA_FAILED_CONFRIEM_ERROR_ORDER_ISCONFRIEM;//当前单子是已审核状态，无法审核
                        break;
                    case 0:
                        clientPreOrderConfirmResponse.result = VAResult.VA_FAILED_CONFRIEM_ERROR_ORDER_NOTFOUND;//未找到该订单
                        break;
                    case -8:
                        clientPreOrderConfirmResponse.result = VAResult.VA_FAILED_CONFRIEM_ERROR_ORDER_HAVE_REFUND;//入座失败，点单已退款或已申请原路打款
                        break;
                    case 1:
                        clientPreOrderConfirmResponse.result = VAResult.VA_OK;//入座成功
                        break;
                    default:
                        clientPreOrderConfirmResponse.result = VAResult.VA_FAILED_CONFRIEM;//入座失败
                        break;
                }
            }
            else
            {
                clientPreOrderConfirmResponse.result = checkResult.result;
            }
            return clientPreOrderConfirmResponse;
        }
        /// <summary>
        /// 客户端查询某用户未入座的所有订单信息
        /// </summary>
        /// <param name="unConfirmPreOrderRequest"></param>
        /// <returns></returns>
        public VAClientUnConfirmPreOrderResponse ClientQueryUnConfirmPreOrder(VAClientUnConfirmPreOrderRequest unConfirmPreOrderRequest)
        {
            VAClientUnConfirmPreOrderResponse clientUnConfirmPreOrderResponse = new VAClientUnConfirmPreOrderResponse();
            clientUnConfirmPreOrderResponse.type = VAMessageType.CLIENT_UNCONFIRM_PREORDER_REMIND_RESPONSE;
            clientUnConfirmPreOrderResponse.cookie = unConfirmPreOrderRequest.cookie;
            clientUnConfirmPreOrderResponse.uuid = unConfirmPreOrderRequest.uuid;
            CheckCookieAndMsgtypeInfo checkResult = Common.CheckCookieAndMsgtype(unConfirmPreOrderRequest.cookie, unConfirmPreOrderRequest.uuid, (int)unConfirmPreOrderRequest.type, (int)VAMessageType.CLIENT_UNCONFIRM_PREORDER_REMIND_REQUEST, false);
            if (checkResult.result == VAResult.VA_OK)
            {
                SystemConfigCacheLogic systemCache = new SystemConfigCacheLogic();
                bool enableClientConfirm = Common.ToBool(systemCache.GetSystemConfig("enableClientConfirm", "false"));
                if (!enableClientConfirm)
                {
                    clientUnConfirmPreOrderResponse.result = checkResult.result;
                }
                else
                {

                    PreOrder19dianManager preOrder19dianManager = new PreOrder19dianManager();
                    List<PreOrder19dianRemindInfo> preOrder19dianRemindInfo = new List<PreOrder19dianRemindInfo>();
                    List<PreOrder19dianRemindInfoDBModel> preOrder19dianRemindInfoDBModel = preOrder19dianManager.SelectUnConfirmPreOrder(unConfirmPreOrderRequest.cookie);

                    if (preOrder19dianRemindInfoDBModel != null && preOrder19dianRemindInfoDBModel.Any())
                    {
                        List<long> PreOrder19dianIds = new List<long>();
                        foreach (PreOrder19dianRemindInfoDBModel item in preOrder19dianRemindInfoDBModel)
                        {
                            PreOrder19dianIds.Add(item.preOrder19dianId);
                        }

                        CouponOperate couponOperate = new CouponOperate();
                        List<OrderCoupon> orderCoupon = couponOperate.SelectOrderDeductibleAmount(PreOrder19dianIds);

                        RedEnvelopeConnPreOrderOperate redEnvelopeConnPreOrderOperate = new RedEnvelopeConnPreOrderOperate();
                        List<long> expireOrderId = redEnvelopeConnPreOrderOperate.QueryExpirePreOrder(unConfirmPreOrderRequest.cookie);

                        foreach (PreOrder19dianRemindInfoDBModel item in preOrder19dianRemindInfoDBModel)
                        {
                            if (expireOrderId != null && expireOrderId.Any())
                            {
                                if (!expireOrderId.Contains(item.preOrder19dianId))
                                {
                                    PreOrder19dianRemindInfo remind = new PreOrder19dianRemindInfo()
                                    {
                                        preOrder19dianId = item.preOrder19dianId,
                                        shopName = item.shopName,
                                        prePaidSum = Common.ToDouble(item.prePaidSum),
                                        prePayTime = Common.ToSecondFrom1970(item.prePayTime),
                                        longitude = item.longitude,
                                        latitude = item.latitude
                                    };

                                    if (orderCoupon != null && orderCoupon.Any())
                                    {
                                        foreach (OrderCoupon orderCouponItem in orderCoupon)
                                        {
                                            if (orderCouponItem.preOrder19dianId == item.preOrder19dianId)
                                            {
                                                remind.prePaidSum = Common.ToDouble(item.prePaidSum + orderCouponItem.RealDeductibleAmount);
                                                break;
                                            }
                                        }
                                    }

                                    preOrder19dianRemindInfo.Add(remind);
                                }
                            }
                            else
                            {
                                PreOrder19dianRemindInfo remind = new PreOrder19dianRemindInfo()
                                {
                                    preOrder19dianId = item.preOrder19dianId,
                                    shopName = item.shopName,
                                    prePaidSum = Common.ToDouble(item.prePaidSum),
                                    prePayTime = Common.ToSecondFrom1970(item.prePayTime),
                                    longitude = item.longitude,
                                    latitude = item.latitude
                                };

                                if (orderCoupon != null && orderCoupon.Any())
                                {
                                    foreach (OrderCoupon orderCouponItem in orderCoupon)
                                    {
                                        if (orderCouponItem.preOrder19dianId == item.preOrder19dianId)
                                        {
                                            remind.prePaidSum = Common.ToDouble(item.prePaidSum + orderCouponItem.RealDeductibleAmount);
                                            break;
                                        }
                                    }
                                }

                                preOrder19dianRemindInfo.Add(remind);
                            }
                        }
                    }
                    clientUnConfirmPreOrderResponse.preOrder19dianRemindInfo = preOrder19dianRemindInfo;

                    SystemConfigCacheLogic systemConfigCacheLogic = new SystemConfigCacheLogic();
                    string range = systemConfigCacheLogic.GetSystemConfig("unConfirmPreOrderRemindRange", "500");//默认距离设置500米
                    clientUnConfirmPreOrderResponse.range = Common.ToDouble(range);
                    clientUnConfirmPreOrderResponse.result = VAResult.VA_OK;
                }
            }
            else
            {
                clientUnConfirmPreOrderResponse.result = checkResult.result;
            }
            return clientUnConfirmPreOrderResponse;
        }

        public DataTable GetFinancial(int shopid, string beginDT, string endDT, int status, int cityid)
        {
            PreOrder19dianManager pm = new PreOrder19dianManager();
            return pm.GetFinancial(shopid, beginDT, endDT, status, cityid);
        }

        /// <summary>
        /// 获取一张主订单下有多少张明细(正常订单，补差价订单)
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public long getPreOrder19dianCount(Guid orderId)
        {
            PreOrder19dianManager pm = new PreOrder19dianManager();
            return pm.getPreOrder19dianCount(orderId);
        }

        /// <summary>
        /// 由OrderId查询对应的所有订单
        /// </summary>
        /// <param name="beginDateTime"></param>
        /// <param name="endDateTime"></param>
        /// <param name="shopID">商家ID</param>
        /// <returns></returns>
        public DataTable SelectPreorder19dianByOrderId(Guid OrderId)
        {
            PreOrder19dianManager pm = new PreOrder19dianManager();
            return pm.SelectPreorder19dianByOrderId(OrderId);
        }
        public DataTable GetPreOrder19DianByPayTime(DateTime beginDateTime, DateTime endDateTime, int shopID)
        {
            PreOrder19dianManager pm = new PreOrder19dianManager();
            return pm.GetPreOrder19DianByPayTime(beginDateTime, endDateTime, shopID);
        }

        /// <summary>
        /// 根据编号，查询预点单详情信息
        /// </summary>
        /// <param name="preOrderId"></param>
        /// <returns></returns>
        public DataTable QueryPreOrderGroupByOrderID(long preOrder19dianId)
        {
            DataTable dtPreOrder19dian = preorder19dianMan.SelectPreOrderGroupByOrderId(preOrder19dianId);
            for (int i = 0; i < dtPreOrder19dian.Rows.Count; i++)
            {
                if (Common.ToDouble(dtPreOrder19dian.Rows[i]["preOrderServerSum"]) > 0)
                {
                    continue;
                }
                else
                {
                    long customerId = Common.ToInt64(dtPreOrder19dian.Rows[i]["customerId"]);
                    int companyId = Common.ToInt32(dtPreOrder19dian.Rows[i]["companyId"]);
                    string orderInJson = Common.ToString(dtPreOrder19dian.Rows[i]["orderInJson"]);
                    string preOrderServerSum = CalcPreorderServerSum(companyId, customerId, orderInJson);
                    dtPreOrder19dian.Rows[i]["preOrderServerSum"] = preOrderServerSum;
                }
            }
            return dtPreOrder19dian;
        }

        public List<PreOrder19dianInfo> GetPreOrder19dianByOrderIdNew(Guid orderId)
        {
            return preorder19dianMan.GetPreOrder19dianByOrderIdNew(orderId);
        }

        /// <summary>
        /// 更新点单PreOrder19dian 的 OrderInJson
        /// </summary>
        /// <param name="orderJson"></param>
        /// <param name="preOrderId"></param>
        /// <returns></returns>
        public bool UpdatePreOrderOrderJson(string orderJson, long preOrderId)
        {
            PreOrder19dianManager pm = new PreOrder19dianManager();
            return pm.UpdatePreOrderOrderJson(orderJson, preOrderId);
        }

        /// <summary>
        /// 券使用的订单总金额
        /// </summary>
        /// <param name="CouponId"></param>
        /// <returns></returns>
        public DataTable GetPrePaidSumByCouponId(int CouponId)
        {
            PreOrder19dianManager pm = new PreOrder19dianManager();
            return pm.GetPrePaidSumByCouponId(CouponId);
        }
    }
}
