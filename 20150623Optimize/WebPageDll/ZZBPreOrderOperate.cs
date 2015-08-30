﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Data;
using System.Web;
using Autofac;
using Autofac.Integration.Web;
using IDishMenuAsynUpdate;
using Microsoft.VisualBasic.CompilerServices;
using PagedList;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL;
using System.Transactions;
using System.Configuration;
using System.Threading;
using VA.AllNotifications;
using VAGastronomistMobileApp.SQLServerDAL.Persistence;
using VAGastronomistMobileApp.SQLServerDAL.Persistence.Infrastructure;
using VAGastronomistMobileApp.WebPageDll.Services;
using VAGastronomistMobileApp.WebPageDll.Services.Infrastructure;
using VAGastronomistMobileApp.WebPageDll.ThreadCallBacks;
using VA.Cache.HttpRuntime;
using VA.Cache.Distributed;
using VA.CacheLogic;
using VA.CacheLogic.ServiceClient;
using VAGastronomistMobileApp.Model.QueryObject;
using VAGastronomistMobileApp.Model.Interface;

namespace VAGastronomistMobileApp.WebPageDll
{
    /// <summary>
    /// 掌中宝操作点单（对账，审核，退款等）
    /// created by wangcheng
    /// 20131203
    /// </summary>
    public class ZZBPreOrderOperate
    {
        /// <summary>
        /// 掌中宝查询获取点单信息
        /// </summary>
        /// <param name="zzb_vaPreOrderListPequest"></param>
        /// <returns></returns>
        //public ZZB_VAPreOrderListResponse ZZBClientQueryPreOrderList(ZZB_VAPreOrderListRequest zzb_vaPreOrderListRequest)
        //{
        //    ZZB_VAPreOrderListResponse zzb_vaPreOrderListResponse = new ZZB_VAPreOrderListResponse();
        //    zzb_vaPreOrderListResponse.type = VAMessageType.ZZB_CLIENT_PREORDERLIST_RESPONSE;
        //    zzb_vaPreOrderListResponse.cookie = zzb_vaPreOrderListRequest.cookie;
        //    zzb_vaPreOrderListResponse.uuid = zzb_vaPreOrderListRequest.uuid;
        //    CheckCookieAndMsgForZZZ checkResult = Common.CheckCookieAndMsgForZZZ(zzb_vaPreOrderListRequest.cookie, (int)zzb_vaPreOrderListRequest.type, (int)VAMessageType.ZZB_CLIENT_PREORDERLIST_REQUEST, zzb_vaPreOrderListRequest.shopId);
        //    if (checkResult.result == VAResult.VA_OK)
        //    {
        //        DataTable dtResponse = new DataTable();
        //        ZZBPreOrderManager zzbPreOrderMan = new ZZBPreOrderManager();
        //        dtResponse = zzbPreOrderMan.ZZB_SelectPreOrderList(zzb_vaPreOrderListRequest.shopId);
        //        List<ZZB_VAPreOrderListModel> preOrderList = new List<ZZB_VAPreOrderListModel>();
        //        if (dtResponse.Rows.Count > 0)
        //        {
        //            for (int i = 0; i < dtResponse.Rows.Count; i++)
        //            {
        //                ZZB_VAPreOrderListModel preOrderListModel = new ZZB_VAPreOrderListModel();
        //                preOrderListModel.customerUserName = Common.ToString(dtResponse.Rows[i]["UserName"]);//用户昵称
        //                preOrderListModel.mobilePhoneNumber = Common.ToString(dtResponse.Rows[i]["number"]);//电话号码
        //                preOrderListModel.preOrder19dianId = Common.ToInt64(dtResponse.Rows[i]["preOrder19dianId"]);//订单流水号
        //                preOrderListModel.prePaidSum = Common.ToDouble(dtResponse.Rows[i]["prePaidSum"]);//支付金额
        //                preOrderListModel.prepaidTime = Common.ToSecondFrom1970(Common.ToDateTime(dtResponse.Rows[i]["prePayTime"]));//支付时间
        //                double refundMoneyClosedSum = Common.ToDouble(dtResponse.Rows[i]["refundMoneyClosedSum"]);
        //                double refundMoneySum = Common.ToDouble(dtResponse.Rows[i]["refundMoneySum"]);
        //                VAPreorderStatus status = (VAPreorderStatus)Common.ToInt32(dtResponse.Rows[i]["status"]);
        //                if (status == VAPreorderStatus.Refund)//已退款
        //                {
        //                    preOrderListModel.status = 3;//客户端全额退款
        //                }
        //                else if (status == VAPreorderStatus.OriginalRefunding)//退款中
        //                {
        //                    preOrderListModel.status = 4;//表示点单退款中
        //                }
        //                else
        //                {
        //                    if (Common.ToInt32(dtResponse.Rows[i]["isShopConfirmed"]) == 1)//已审核
        //                    {
        //                        if (preOrderListModel.prePaidSum - refundMoneyClosedSum < 0.01 && status == VAPreorderStatus.Deleted)
        //                        {
        //                            //点单已审核，并进行部分退款后全部退完，并已经完成打款，在悠先点菜的点单列表中删除该单，
        //                            //该单在悠先服务列表中显示为已审核（已使用），应该显示已退款
        //                            preOrderListModel.status = 3;
        //                        }
        //                        else if (refundMoneySum > 0.009)
        //                        {
        //                            preOrderListModel.status = 4;
        //                        }
        //                        else
        //                        {
        //                            preOrderListModel.status = 2;//点单已使用
        //                        }
        //                    }
        //                    else//未审核
        //                    {
        //                        preOrderListModel.status = 1;//点单未使用
        //                    }
        //                }
        //                preOrderList.Add(preOrderListModel);
        //            }
        //        }
        //        zzb_vaPreOrderListResponse.shoppingMallURL = WebConfig.ShoppingMallURL;
        //        zzb_vaPreOrderListResponse.result = VAResult.VA_OK;
        //        zzb_vaPreOrderListResponse.preOrderList = preOrderList;
        //    }
        //    else
        //    {
        //        zzb_vaPreOrderListResponse.result = checkResult.result;
        //    }
        //    #region
        //    //VAEmployeeLogin employeeLogin = new VAEmployeeLogin();
        //    //EmployeeOperate employeeLoginOpe = new EmployeeOperate();
        //    //employeeLogin.userName = zzb_vaPreOrderListPequest.userName;
        //    //employeeLogin.password = zzb_vaPreOrderListPequest.password;
        //    //VAEmployeeLoginResponse employeeLoginResponse = employeeLoginOpe.EmployeeLogin(employeeLogin);
        //    //if (employeeLoginResponse.result == VAResult.VA_OK)
        //    //{
        //    //    string clientQueryStartTime = zzb_vaPreOrderListPequest.queryTime + " 00:00:00";
        //    //    string clientQueryEndTime = zzb_vaPreOrderListPequest.queryTime + " 23:59:59";
        //    //    DataTable dtResponse = new DataTable();
        //    //    ZZBPreOrderManager zzbPreOrderMan = new ZZBPreOrderManager();
        //    //    switch (zzb_vaPreOrderListPequest.queryType)
        //    //    {
        //    //        case 1://已付款（条件：已支付，未验证，未审核，未对账）
        //    //            //此处根据点单时间排序，无验证时间，所以需呀单独查询
        //    //            dtResponse = zzbPreOrderMan.ZZB_SelectPreOrderList(clientQueryStartTime, clientQueryEndTime, zzb_vaPreOrderListPequest.shopId);
        //    //            break;
        //    //        //以下三种情况根据验证时间排序，同意查询
        //    //        case 2://待审核（条件：已支付，已验证，未审核，未对账）
        //    //            dtResponse = zzbPreOrderMan.ZZB_SelectPreOrderList(clientQueryStartTime, clientQueryEndTime, zzb_vaPreOrderListPequest.shopId, 2);
        //    //            break;
        //    //        case 3://待对账（条件：已支付，已验证，已审核，未对账）
        //    //            dtResponse = zzbPreOrderMan.ZZB_SelectPreOrderList(clientQueryStartTime, clientQueryEndTime, zzb_vaPreOrderListPequest.shopId, 3);
        //    //            break;
        //    //        case 4://已完成（条件：已支付，已验证，已审核，已对账）
        //    //            dtResponse = zzbPreOrderMan.ZZB_SelectPreOrderList(clientQueryStartTime, clientQueryEndTime, zzb_vaPreOrderListPequest.shopId, 4);
        //    //            break;
        //    //    }
        //    //    List<ZZB_VAPreOrderListModel> preOrderList = new List<ZZB_VAPreOrderListModel>();
        //    //    if (dtResponse.Rows.Count > 0)
        //    //    {
        //    //        for (int i = 0; i < dtResponse.Rows.Count; i++)
        //    //        {
        //    //            ZZB_VAPreOrderListModel preOrderListModel = new ZZB_VAPreOrderListModel();
        //    //            preOrderListModel.customerName = Common.ToString(dtResponse.Rows[i]["customerName"]);
        //    //            preOrderListModel.eCardNumber = Common.ToString(dtResponse.Rows[i]["eCardNumber"]);
        //    //            preOrderListModel.preOrder19dianId = Common.ToInt32(dtResponse.Rows[i]["preOrder19dianId"]);
        //    //            preOrderListModel.prePaidSum = Common.ToDouble(dtResponse.Rows[i]["prePaidSum"]);
        //    //            preOrderList.Add(preOrderListModel);
        //    //        }
        //    //    }
        //    //    zzb_vaPreOrderListResponse.result = VAResult.VA_OK;
        //    //    zzb_vaPreOrderListResponse.preOrderList = preOrderList;
        //    //}
        //    //else
        //    //{
        //    //    zzb_vaPreOrderListResponse.result = employeeLoginResponse.result;
        //    //}
        //    #endregion
        //    return zzb_vaPreOrderListResponse;
        //}
        /// <summary>
        /// 掌中宝查看点单详情
        /// </summary>
        /// <param name="zzb_vaPreOrderListDetailRequest"></param>
        /// <returns></returns>
        public ZZB_VAPreOrderListDetailResponse ZZBClientQueryPreOrderListDetail(ZZB_VAPreOrderListDetailRequest zzb_vaPreOrderListDetailRequest)
        {
            ZZB_VAPreOrderListDetailResponse zzb_vaPreOrderListDetailResponse = new ZZB_VAPreOrderListDetailResponse();
            zzb_vaPreOrderListDetailResponse.type = VAMessageType.ZZB_CLIENT_PREORDERLISTDETAIL_RESPONSE;
            zzb_vaPreOrderListDetailResponse.cookie = zzb_vaPreOrderListDetailRequest.cookie;
            zzb_vaPreOrderListDetailResponse.uuid = zzb_vaPreOrderListDetailRequest.uuid;
            CheckCookieAndMsgForZZZ checkResult = Common.CheckCookieAndMsgForZZZ(zzb_vaPreOrderListDetailRequest.cookie, (int)zzb_vaPreOrderListDetailRequest.type, (int)VAMessageType.ZZB_CLIENT_PREORDERLISTDETAIL_REQUEST, zzb_vaPreOrderListDetailRequest.shopId);
            var refundDetail = new List<RefundDetail>();
            if (checkResult.result == VAResult.VA_OK)
            {
                ZZBPreOrderManager zzbPreOrderMan = new ZZBPreOrderManager();
                DataTable dtDetail = null;

                // orderID不为空表示客户端20150626后的新版本(补差价)，传入了orderID add by zhujinlei 2015/06/30
                PreOrder19dianOperate preOperate = new PreOrder19dianOperate();
                double fillpostAmount = 0; // 补差价

                if (zzb_vaPreOrderListDetailRequest.orderId != Guid.Empty)
                {
                    // 这里获取的是order表的数据，部分数据订单流水号、桌号取的是第一单支付(正常点单)的订单值
                    dtDetail = zzbPreOrderMan.ZZB_SelectOrderListDetail(zzb_vaPreOrderListDetailRequest.orderId);

                    if (dtDetail.Rows.Count == 1)
                    {
                        fillpostAmount = Common.ToDouble(dtDetail.Rows[0]["PayDifferenceSum"]);
                        zzb_vaPreOrderListDetailRequest.preOrder19dianId = Common.ToInt64(dtDetail.Rows[0]["preOrder19dianId"]);
                    }
                }
                else
                {
                    dtDetail = zzbPreOrderMan.ZZB_SelectPreOrderListDetail(zzb_vaPreOrderListDetailRequest.preOrder19dianId);
                    fillpostAmount = 0;
                }
                // 返回订单ID
                zzb_vaPreOrderListDetailResponse.orderId = zzb_vaPreOrderListDetailRequest.orderId;

                if (dtDetail.Rows.Count == 1)
                {
                    zzb_vaPreOrderListDetailResponse.result = VAResult.VA_OK;
                    zzb_vaPreOrderListDetailResponse.preOrder19dianId = Common.ToInt64(dtDetail.Rows[0]["preOrder19dianId"]);//订单流水号
                    zzb_vaPreOrderListDetailResponse.prePaidTime = Common.ToSecondFrom1970(Common.ToDateTime(dtDetail.Rows[0]["prePayTime"]));//支付时间
                    zzb_vaPreOrderListDetailResponse.discount = Common.ToDouble(dtDetail.Rows[0]["discount"]);//支付时享受的折扣
                    zzb_vaPreOrderListDetailResponse.savedAmount = Common.ToDecimal(dtDetail.Rows[0]["savedAmount"]);//已省金额
                    zzb_vaPreOrderListDetailResponse.orginalPrice = Common.ToDecimal(dtDetail.Rows[0]["orginalPrice"]);//原价
                    zzb_vaPreOrderListDetailResponse.prePaidSum = Common.ToDecimal(dtDetail.Rows[0]["prePaidSum"]);//支付金额
                    zzb_vaPreOrderListDetailResponse.refundMoneySum = Common.ToDecimal(dtDetail.Rows[0]["refundMoneySum"]);//已退款金额                    
                    zzb_vaPreOrderListDetailResponse.deskNumber = Common.ToString(dtDetail.Rows[0]["deskNumber"]); // 桌号                 
                    zzb_vaPreOrderListDetailResponse.deskNumber = Common.ToString(dtDetail.Rows[0]["deskNumber"]);
                    zzb_vaPreOrderListDetailResponse.customerId = Common.ToInt64(dtDetail.Rows[0]["customerId"]);
                    zzb_vaPreOrderListDetailResponse.canRefundAccount = Common.ToDecimal(dtDetail.Rows[0]["canRefundAccount"]);//当前可退最大金额 
                    // 补差价 add by zhujinlei 2015/06/30 
                    //zzb_vaPreOrderListDetailResponse.fillpostAmount = zzbPreOrderMan.ZZB_SelectFillPostAmount(zzb_vaPreOrderListDetailRequest.orderId);
                    zzb_vaPreOrderListDetailResponse.fillpostAmount = fillpostAmount;
                    // 折扣价 add by zhujinlei 2015/07/06
                    zzb_vaPreOrderListDetailResponse.disCountMoney = Common.ToDouble(dtDetail.Rows[0]["verifiedSaving"]);

                    int employeeId = Common.ToInt32(checkResult.dtEmployee.Rows[0]["EmployeeID"]);
                    bool isViewAllocWorker = Common.ToBool(checkResult.dtEmployee.Rows[0]["isViewAllocWorker"]);
                    zzb_vaPreOrderListDetailResponse.isNotEvaluated = Common.ToInt32(dtDetail.Rows[0]["isEvaluation"]) == 0;

                    // 评价
                    if (!zzb_vaPreOrderListDetailResponse.isNotEvaluated)
                    {
                        var preorderEvaluation
                            = PreorderEvaluationOperate.GetFirstByQuery(new PreorderEvaluationQueryObject() { PreOrder19dianId = zzb_vaPreOrderListDetailResponse.preOrder19dianId });
                        zzb_vaPreOrderListDetailResponse.evaluationValue = preorderEvaluation.EvaluationLevel;
                        zzb_vaPreOrderListDetailResponse.evaluationTime = Common.ToSecondFrom1970(preorderEvaluation.EvaluationTime);
                        zzb_vaPreOrderListDetailResponse.evaluationContent = preorderEvaluation.EvaluationContent;
                    }
                    else
                    {
                        zzb_vaPreOrderListDetailResponse.evaluationContent = string.Empty;
                    }
                    #region 权限
                    var shopAuthorityService = ServiceFactory.Resolve<IShopAuthorityService>();
                    var rs = new string[] { ShopRole.客户信息.GetString(), ShopRole.店员退款.GetString() };
                    zzb_vaPreOrderListDetailResponse.roles =
                        (from a in
                             shopAuthorityService.GetShopAuthorities(zzb_vaPreOrderListDetailRequest.shopId, employeeId,
                                 isViewAllocWorker)
                         where rs.Contains(a.AuthorityCode)
                         orderby a.ShopAuthoritySequence
                         select
                             new ShopHaveAuthority
                             {
                                 authorityCode = a.AuthorityCode,
                                 authorityName = a.ShopAuthorityName,
                                 isClientShow = a.IsClientShow
                             }).ToList();
                    #endregion

                    string orderInJson = Common.ToString(dtDetail.Rows[0]["orderInJson"]);
                    VAPreorderStatus status = (VAPreorderStatus)Common.ToInt32(dtDetail.Rows[0]["status"]);
                    int isShopConfirmed = Common.ToInt32(dtDetail.Rows[0]["isShopConfirmed"]);
                    if (status == VAPreorderStatus.Refund)//已退款
                    {
                        zzb_vaPreOrderListDetailResponse.status = 3;//客户端全额退款
                    }
                    else if (status == VAPreorderStatus.OriginalRefunding)//退款中
                    {
                        zzb_vaPreOrderListDetailResponse.status = 4;//表示点单退款中
                    }
                    else
                    {
                        if (isShopConfirmed == 1)//已审核
                        {
                            zzb_vaPreOrderListDetailResponse.status = 2;//点单已使用
                        }
                        else//未审核
                        {
                            zzb_vaPreOrderListDetailResponse.status = 1;//点单未使用
                        }
                    }
                    string sundryJson = Common.ToString(dtDetail.Rows[0]["sundryJson"]);//杂项信息//直接支付接送信息为空
                    List<VASundryInfo> listSundryInfo = new List<VASundryInfo>();
                    if (!string.IsNullOrEmpty(sundryJson))
                    {
                        listSundryInfo = JsonOperate.JsonDeserialize<List<VASundryInfo>>(sundryJson);
                    }
                    List<SundryInfoResponse> listSundryInfoResponse = new List<SundryInfoResponse>();
                    PreOrder19dianOperate preOrder19dianOpe = new PreOrder19dianOperate();
                    VAEstimatedSavingAndOrginal estimatedSavingAndorginal = preOrder19dianOpe.CalcPreorderEstimatedSaving(orderInJson, zzb_vaPreOrderListDetailResponse.discount);
                    preOrder19dianOpe.CalcSundryCount(estimatedSavingAndorginal.orginalPriceSum, listSundryInfo, ref listSundryInfoResponse, zzb_vaPreOrderListDetailResponse.discount);//杂项折扣后价格
                    zzb_vaPreOrderListDetailResponse.sundryList = listSundryInfoResponse;
                    List<PreOrderIn19dian> listOrderInJson = new List<PreOrderIn19dian>();
                    if (!string.IsNullOrEmpty(orderInJson))
                    {
                        listOrderInJson = JsonOperate.JsonDeserialize<List<PreOrderIn19dian>>(orderInJson);
                        for (int i = 0; i < listOrderInJson.Count; i++)
                        {
                            if (listOrderInJson[i].dishIngredients == null)
                            {
                                listOrderInJson[i].dishIngredients = new List<VADishIngredients>();
                            }
                            else
                            {
                                for (int j = 0; j < listOrderInJson[i].dishIngredients.Count; j++)
                                {
                                    if (listOrderInJson[i].dishIngredients[j].vipDiscountable == "True")
                                    {//处理为折扣价
                                        listOrderInJson[i].dishIngredients[j].ingredientsPrice = Common.ToDouble(listOrderInJson[i].dishIngredients[j].ingredientsPrice * zzb_vaPreOrderListDetailResponse.discount);
                                    }
                                }
                            }
                            if (listOrderInJson[i].vipDiscountable)
                            {//处理为折后价
                                listOrderInJson[i].unitPrice = Common.ToDouble(zzb_vaPreOrderListDetailResponse.discount * listOrderInJson[i].unitPrice);
                            }
                        }
                        zzb_vaPreOrderListDetailResponse.orderInJson = JsonOperate.JsonSerializer<List<PreOrderIn19dian>>(listOrderInJson);//菜品信息
                    }
                    else//直接付款
                    {
                        zzb_vaPreOrderListDetailResponse.orderInJson = "";//菜品信息
                    }
                    if (isShopConfirmed == 1)
                    {
                        zzb_vaPreOrderListDetailResponse.isShopConfirmed = true;
                    }
                    else
                    {
                        zzb_vaPreOrderListDetailResponse.isShopConfirmed = false;
                    }
                    int isApproved = Common.ToInt32(dtDetail.Rows[0]["isApproved"]);
                    if (isApproved == 1)
                    {
                        zzb_vaPreOrderListDetailResponse.isApproved = true;//对账
                    }
                    else
                    {
                        zzb_vaPreOrderListDetailResponse.isApproved = false;//对账
                    }
                    zzb_vaPreOrderListDetailResponse.mobilePhoneNumber = Common.ToString(dtDetail.Rows[0]["mobilePhoneNumber"]);//手机号码
                    zzb_vaPreOrderListDetailResponse.customerUserName = Common.ToString(dtDetail.Rows[0]["UserName"]);//用户昵称
                    zzb_vaPreOrderListDetailResponse.invoiceTitle = Common.ToString(dtDetail.Rows[0]["invoiceTitle"]);
                    if (zzb_vaPreOrderListDetailResponse.refundMoneySum > 0)
                    {
                        // 老版本
                        if (zzb_vaPreOrderListDetailRequest.orderId == Guid.Empty)
                        {
                            refundDetail = new Money19dianDetailManager().SelectRefundDetailByOrderId(zzb_vaPreOrderListDetailResponse.preOrder19dianId);
                        }
                        else
                        {
                            refundDetail = new Money19dianDetailManager().SelectRefundDetailByOrderIdNew(zzb_vaPreOrderListDetailRequest.orderId);
                        }
                    }
                    var list = new List<OrderPreferentialDetail>();
                    double[] amoDoubles = new CouponOperate().GetCouponDeductibleAmount(zzb_vaPreOrderListDetailResponse.preOrder19dianId);
                    zzb_vaPreOrderListDetailResponse.deductionPrice = amoDoubles[2];
                    if (zzb_vaPreOrderListDetailResponse.discount != 1)
                    {
                        list.Add(new OrderPreferentialDetail()
                        {
                            name = "折扣",
                            description = zzb_vaPreOrderListDetailResponse.discount * 10 + "折"
                        });
                    }
                    if (zzb_vaPreOrderListDetailResponse.deductionPrice > 0)
                    {
                        list.Add(new OrderPreferentialDetail()
                        {
                            name = "折扣券",
                            description = "每满" + amoDoubles[1] + "元减" + amoDoubles[0] + "元，最多减" + amoDoubles[3]
                        });
                    }
                    zzb_vaPreOrderListDetailResponse.preferentialDetails = list;

                    // 老版本需要特殊处理,新版本preServerOrderSum已经包括折扣价和抵扣券的金额
                    if (zzb_vaPreOrderListDetailRequest.orderId == Guid.Empty)
                    {
                        // 原价=服务器算出的preServerOrderSum+折扣价+抵扣券金额
                        zzb_vaPreOrderListDetailResponse.orginalPrice = zzb_vaPreOrderListDetailResponse.orginalPrice + (decimal)zzb_vaPreOrderListDetailResponse.disCountMoney + (decimal)zzb_vaPreOrderListDetailResponse.deductionPrice;

                        zzb_vaPreOrderListDetailRequest.orderId = new Guid(Convert.ToString(dtDetail.Rows[0]["orderId"]));
                    }
                }
            }
            else
            {
                zzb_vaPreOrderListDetailResponse.result = checkResult.result;
            }
            zzb_vaPreOrderListDetailResponse.refundDetails = refundDetail;
            return zzb_vaPreOrderListDetailResponse;
        }

        /// <summary>
        /// 悠先服务补差价退款特殊处理 add by zhujinlei 2015/06/26
        /// 要求先退微信支付，再退粮票，最后退红包
        /// </summary>
        /// <param name="preorderRefundRequest">请求参数</param>
        /// <param name="list">订单的列表</param>
        /// <param name="preorderRefundResponse">返回对象</param>
        /// <param name="preorder19dianMan"></param>
        /// <param name="checkResult">验证结果</param>
        public void FillPostMoney(ZZB_VAPreOrderRefundRequest preorderRefundRequest, List<PreOrder19dianInfo> list, ZZB_VAPreOrderRefundResponse preorderRefundResponse,
            PreOrder19dianManager preorder19dianMan, CheckCookieAndMsgForZZZ checkResult)
        {
            // 首先获取订单表数据
            OrderManager objOrderManager = new OrderManager();
            var objOrder = objOrderManager.GetEntityById(preorderRefundRequest.orderId);
            // 已退款总额
            double refundMoneySumTotal = objOrder.RefundMoneySum;
            // 支付金额
            double prepaidSumTotal = objOrder.PrePaidSum;
            // 最大退款的额度
            double canRefundSumTotal = Common.ToDouble(prepaidSumTotal - refundMoneySumTotal);
            // 已结算的退款总额
            double refundMoneyClosedSumTotal = objOrder.RefundMoneyClosedSum;

            // 悠先服务申请的退款金额
            double refundsum = Common.ToDouble(preorderRefundRequest.refundAccount);
            // 最终需要修改order表里面的退款金额
            double finalRefundSum = refundsum;

            // 处理步骤

            //01 获取第三方支付的总金额
            // 第三方支付的总额
            double thirdPayAmount = 0;
            foreach (PreOrder19dianInfo obj in list)
            {
                //已支付，已审核，未对账
                if (Common.ToInt32(obj.isPaid) == 1 && Common.ToInt32(obj.isApproved) == 0 && Common.ToInt32(obj.isShopConfirmed) == 1)
                {
                    //该点单使用第三方支付的金额
                    ThirdPartyPaymentInfo thirdPartyPaymentInfo = preorder19dianMan.SelectPreorderPayAmount(obj.preOrder19dianId);
                    //额外收取的钱
                    double extendPay = preorder19dianMan.SelectExtendPay(obj.preOrder19dianId);
                    //不退还系统额外收取的钱
                    double payAmount = thirdPartyPaymentInfo.Amount - extendPay;
                    thirdPayAmount = thirdPayAmount + payAmount;

                    // 赋值支付类型，用于排序退款，先退微信再退支付宝
                    obj.OrderPayType = (int)thirdPartyPaymentInfo.Type;
                }
            }

            // 排序，按先退微信再退支付宝顺序排
            list = list.OrderBy(p => p.OrderPayType).ToList();


            // 判断是否可以退款(申请的退款金额要小于最大可退款金额）
            if ((canRefundSumTotal - refundsum) > -0.001)
            {
                // 用于标识整个退款流程是否正确结束的标识，有一个失败就不发送成功短信和推送消息
                bool isRefundOk = false;
                int shopId = 0;
                string customerMobilephone = string.Empty;
                string appBuildID = string.Empty;
                long finalPreOrder19dianId = 0;
                // 是否涉及第三方退款
                bool hasThirdRefundMoney = false;
                // 用户ID
                long customerId = 0;
                // 记录退款日志用
                List<MoneyRefundOrder> listMoneyRefundOrder = new List<MoneyRefundOrder>();
                MoneyRefundOrder objMoneyRefundOrder = null;

                // 如果申请的退款金额大于第三方支付的总金额，先把所有单子第三方支付的先退完，再依次处理粮票和红包
              
                //a、循环退每一个单子的第三方支付
                #region 第三方支付退款
                foreach (PreOrder19dianInfo obj in list)
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        //已支付，已审核，未对账
                        if (Common.ToInt32(obj.isPaid) == 1 && Common.ToInt32(obj.isApproved) == 0 && Common.ToInt32(obj.isShopConfirmed) == 1)
                        {
                            if (refundsum <= 0)
                            {
                                break;
                            }
                            shopId = obj.shopId;
                            int companyId = obj.companyId;
                            customerId = obj.customerId;
                            long preOrder19dianId = obj.preOrder19dianId;
                            // 记录正常点单的点单ID
                            if (obj.OrderType == OrderTypeEnum.Normal)
                            {
                                finalPreOrder19dianId = preOrder19dianId;
                            }
                            appBuildID = obj.appBuild;

                            // 已退款总额
                            double refundMoneySum = Convert.ToDouble(obj.refundMoneySum);
                            // 支付金额
                            double prepaidSum = Convert.ToDouble(obj.prePaidSum);
                            // 可以退款的额度
                            double canRefundSum = Common.ToDouble(prepaidSum - refundMoneySum);
                            // 已结算的退款总额
                            double refundMoneyClosedSum = Convert.ToDouble(obj.refundMoneyClosedSum);

                            //该点单使用第三方支付的金额
                            ThirdPartyPaymentInfo thirdPartyPaymentInfo = preorder19dianMan.SelectPreorderPayAmount(obj.preOrder19dianId);
                            //额外收取的钱
                            double extendPay = preorder19dianMan.SelectExtendPay(obj.preOrder19dianId);
                            //不退还系统额外收取的钱
                            double payAmount = thirdPartyPaymentInfo.Amount - extendPay;

                            // 第三方支付为零，不处理
                            if (payAmount <= 0)
                            {
                                continue;
                            }

                            // 已退款总额大于第三方支付的额度，需要处理下一条
                            if ((refundMoneySum - payAmount) > -0.001)
                            {
                                continue;
                            }
                            isRefundOk = false;
                            //hasThirdRefundMoney = true;
                            // 该单实际需要退还的第三方金额
                            double actualPayAmount = payAmount - refundMoneySum;
                            if (actualPayAmount > refundsum)
                                actualPayAmount = refundsum;

                            // 该单第三方退款完成后，还需要退还的申请退款额度
                            refundsum = refundsum - actualPayAmount;

                            //修改预点单状态
                            bool updatePreorder = false;
                            // 本单的实际退款金额等于最大可退款金额，表示全部退完了
                            if (Common.ToDecimal(actualPayAmount) == Common.ToDecimal(canRefundSum))
                            {
                                updatePreorder = preorder19dianMan.UpdatePreOrderRefundInfo(preOrder19dianId, VAPreorderStatus.OriginalRefunding, actualPayAmount);
                            }
                            else
                            {
                                updatePreorder = preorder19dianMan.UpdatePreOrderRefundMoneySum(preOrder19dianId, actualPayAmount);
                            }
                            bool flagUpdatePreOrderRefundMoneyClosedSum = preorder19dianMan.UpdatePreOrderRefundMoneyClosedSum(preOrder19dianId, actualPayAmount);

                            // 更新修改后的退款金额
                            obj.refundMoneySum = Common.ToDouble(obj.refundMoneySum) + actualPayAmount;
                            obj.refundMoneyClosedSum = Common.ToDouble(obj.refundMoneyClosedSum) + actualPayAmount;

                            // 添加退款日志相关备用
                            objMoneyRefundOrder = listMoneyRefundOrder.FirstOrDefault(m => m.preOrder19dianId == preOrder19dianId);
                            if (objMoneyRefundOrder != null)
                            {
                                objMoneyRefundOrder.moneyAmount = Common.ToDouble(objMoneyRefundOrder.moneyAmount) + actualPayAmount;
                            }
                            else
                            {
                                objMoneyRefundOrder = new MoneyRefundOrder();
                                objMoneyRefundOrder.moneyAmount = actualPayAmount;
                                objMoneyRefundOrder.preOrder19dianId = preOrder19dianId;
                                objMoneyRefundOrder.orderID = preorderRefundRequest.orderId;
                                listMoneyRefundOrder.Add(objMoneyRefundOrder);
                            }

                            CustomerManager customerMan = new CustomerManager();
                            DataTable dtCustomer = customerMan.SelectCustomer(customerId);

                            if (dtCustomer.Rows.Count == 1)
                            {
                                // 原路退款申请
                                bool flagOriginalRefundRecord = false; // 原路退款记录成功标识
                                customerMobilephone = Common.ToString(dtCustomer.Rows[0]["mobilePhoneNumber"]);
                                OriginalRoadRefundInfo originalRoadRefund = new OriginalRoadRefundInfo();
                                originalRoadRefund.refundAmount = actualPayAmount;
                                originalRoadRefund.type = VAOriginalRefundType.PREORDER;
                                originalRoadRefund.connId = preOrder19dianId;
                                originalRoadRefund.customerMobilephone = customerMobilephone;
                                originalRoadRefund.customerUserName = Common.ToString(dtCustomer.Rows[0]["UserName"]);
                                originalRoadRefund.status = (int)VAOriginalRefundStatus.REMITTING;
                                originalRoadRefund.employeeId = Common.ToInt32(checkResult.dtEmployee.Rows[0]["EmployeeID"]);
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
                                {
                                    //记录原路退款记录
                                    flagOriginalRefundRecord = true;
                                    originalRoadRefund.tradeNo = thirdPartyPaymentInfo.tradeNo;
                                    //这里加入退款流程
                                    ThreadPool.QueueUserWorkItem(
                                        new RefundCallBack(preOrder19dianId, thirdPartyPaymentInfo,
                                            (float)originalRoadRefund.refundAmount, originalRoadRefund)
                                            .Refund);
                                }
                                else
                                {
                                    flagOriginalRefundRecord = false;
                                }

                                //记录退款记录
                                bool refundData = false;
                                refundData = Common.InsertRefundData(customerId, actualPayAmount, preOrder19dianId, "悠先服务原路退款");

                                //// 用户在平台的VIP等级
                                //bool modifyVipInfo = false;
                                //double preOrderTotalAmount =
                                //    Common.ToDouble(dtCustomer.Rows[0]["preOrderTotalAmount"]);
                                //int preOrderTotalQuantity =
                                //    Common.ToInt32(dtCustomer.Rows[0]["preOrderTotalQuantity"]);
                                //int currentPlatformVipGrade =
                                //    Common.ToInt32(dtCustomer.Rows[0]["currentPlatformVipGrade"]);
                                //double cumulativeAmount = preOrderTotalAmount - actualPayAmount;
                                //if ((canRefundSum - actualPayAmount) < 0.001)
                                //{
                                //    modifyVipInfo = Common.ModifyUserPlatVip(customerId, preOrderTotalQuantity,
                                //        currentPlatformVipGrade, -actualPayAmount, cumulativeAmount, true);
                                //}
                                //else
                                //{
                                //    modifyVipInfo = Common.ModifyUserPlatVip(customerId, preOrderTotalQuantity,
                                //        currentPlatformVipGrade, -actualPayAmount, cumulativeAmount, false);
                                //}

                                // 修改用户在平台的消费金额
                                bool modifyVipInfo = customerMan.UpdateCustomerPartInfo(-actualPayAmount, 0, customerId);

                                //退款要更新支付明细表的退款金额及状态
                                Preorder19DianLineManager lineManager = new Preorder19DianLineManager();
                                Model.QueryObject.Preorder19DianLineQueryObject queryObject = new Model.QueryObject.Preorder19DianLineQueryObject()
                                {
                                    Preorder19DianId = preOrder19dianId
                                };
                                List<IPreorder19DianLine> orderLineList = lineManager.GetListByQuery(queryObject);
                                bool updateLine = false;
                                foreach (IPreorder19DianLine line in orderLineList)
                                {
                                    switch (line.PayType)
                                    {
                                        case (int)VAOrderUsedPayMode.ALIPAY:
                                        case (int)VAOrderUsedPayMode.WECHAT:
                                        case (int)VAOrderUsedPayMode.UNIONPAY:
                                            line.RefundAmount = line.RefundAmount + originalRoadRefund.refundAmount;//已经退的钱+本次退的第三方金额
                                            break;
                                        case (int)VAOrderUsedPayMode.BALANCE:
                                        case (int)VAOrderUsedPayMode.COUPON:
                                        case (int)VAOrderUsedPayMode.REDENVELOPE:
                                            continue;
                                    }
                                    updateLine = lineManager.Update(line);//更新每种支付类型的退款金额及状态
                                }

                                if (updatePreorder && flagOriginalRefundRecord && refundData && flagUpdatePreOrderRefundMoneyClosedSum && modifyVipInfo)
                                {
                                    scope.Complete();
                                    isRefundOk = true;
                                }
                                else
                                {
                                    preorderRefundResponse.result = VAResult.VA_PREORDER_REFUND_ERROR; //退款失败
                                    refundsum = 0;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            // 未支付，未审核，已对账 不需要处理
                            continue;
                        }
                    }
                }
                #endregion

                //b、第三方支付都退完了， 再循环退每一个单子的粮票
                #region 粮票退款
                foreach (PreOrder19dianInfo obj in list)
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        //已支付，已审核，未对账
                        if (Common.ToInt32(obj.isPaid) == 1 && Common.ToInt32(obj.isApproved) == 0 && Common.ToInt32(obj.isShopConfirmed) == 1)
                        {
                            if (refundsum <= 0)
                            {
                                break;
                            }

                            shopId = obj.shopId;
                            int companyId = obj.companyId;
                            customerId = obj.customerId;
                            long preOrder19dianId = obj.preOrder19dianId;
                            // 记录正常点单的点单ID
                            if (obj.OrderType == OrderTypeEnum.Normal)
                            {
                                finalPreOrder19dianId = preOrder19dianId;
                            }
                            appBuildID = obj.appBuild;

                            // 已退款总额
                            double refundMoneySum = Convert.ToDouble(obj.refundMoneySum);
                            // 支付金额
                            double prepaidSum = Convert.ToDouble(obj.prePaidSum);
                            // 可以退款的额度
                            double canRefundSum = Common.ToDouble(prepaidSum - refundMoneySum);
                            // 已结算的退款总额
                            double refundMoneyClosedSum = Convert.ToDouble(obj.refundMoneyClosedSum);

                            //该点单使用第三方支付的金额
                            ThirdPartyPaymentInfo thirdPartyPaymentInfo = preorder19dianMan.SelectPreorderPayAmount(obj.preOrder19dianId);
                            //额外收取的钱
                            double extendPay = preorder19dianMan.SelectExtendPay(obj.preOrder19dianId);
                            //不退还系统额外收取的钱
                            double payAmount = thirdPartyPaymentInfo.Amount - extendPay;

                            RedEnvelopeConnPreOrderOperate redEnvelopeOperate = new RedEnvelopeConnPreOrderOperate();
                            //当前点单使用红包抵扣的金额
                            double redEnvelopeConsumed = redEnvelopeOperate.GetPayOrderConsumeRedEnvelopeAmount(preOrder19dianId);
                            //该点单使用粮票支付的金额
                            double foodCoupon = prepaidSum - payAmount - redEnvelopeConsumed;

                            if (foodCoupon <= 0)
                            {
                                continue;
                            }

                            // 已退款总额大于第三方支付的额度和粮票，需要处理下一条
                            if ((refundMoneySum - payAmount - foodCoupon) > -0.001)
                            {
                                continue;
                            }
                            isRefundOk = false;

                            // 该单实际可以退还的粮票金额
                            double actualPayAmount = foodCoupon - (refundMoneySum - payAmount);
                            if (actualPayAmount > refundsum)
                                actualPayAmount = refundsum;
                            // 该单粮票退款完成后，还需要退还的申请退款额度
                            refundsum = refundsum - actualPayAmount;

                            // 如果申请退款的金额已经小于0了， 表示不需要把余下的粮票全部退完了， 只需要退实际要求退款的量即可
                            if (refundsum < 0)
                            {
                                actualPayAmount = refundsum + actualPayAmount;
                                refundsum = 0;
                            }


                            //修改预点单状态
                            bool updatePreorder = false;
                            // 本单的实际退款金额等于最大可退款金额，表示全部退完了
                            if (Common.ToDecimal(actualPayAmount) == Common.ToDecimal(canRefundSum))
                            {
                                updatePreorder = preorder19dianMan.UpdatePreOrderRefundInfo(preOrder19dianId, VAPreorderStatus.Refund, actualPayAmount);
                            }
                            else
                            {
                                updatePreorder = preorder19dianMan.UpdatePreOrderRefundMoneySum(preOrder19dianId, actualPayAmount);
                            }

                            //updatePreorder = preorder19dianMan.UpdatePreOrderRefundInfo(preOrder19dianId, VAPreorderStatus.Refund, actualPayAmount);

                            Money19dianDetail money19dianDetail = new Money19dianDetail
                            {
                                customerId = customerId,
                                changeReason = Common.GetEnumDescription(VA19dianMoneyChangeReason.MONEY19DIAN_REFUND_PREORDER).Replace("{0}", Common.ToString(preOrder19dianId)),
                                changeTime = System.DateTime.Now,
                                flowNumber = SybMoneyOperate.CreateCustomerFlowNumber(customerId), //流水号
                                accountType = (int)AccountType.USER_CANCEL_ORDER, //用户取消订单
                                accountTypeConnId = Common.ToString(preOrder19dianId),
                                inoutcomeType = (int)InoutcomeType.IN, //相对于用户退款就是收入
                                companyId = companyId,
                                shopId = shopId
                            };
                            money19dianDetail.changeValue = actualPayAmount;
                            // 修改用户余额（加上退款的粮票）
                            money19dianDetail.remainMoney = Money19dianDetailManager.GetCustomerRemainMoney(customerId) + actualPayAmount;
                            // 用户余额变更是否成功标识
                            bool flagUpdateCustomerMoneyRemain = true;
                            // 修改预订单打款金额是否成功标识
                            bool flagUpdatePreOrderRefundMoneyClosedSum = true;
                            flagUpdateCustomerMoneyRemain = SybMoneyCustomerOperate.AccountBalanceChanges(money19dianDetail);
                            flagUpdatePreOrderRefundMoneyClosedSum = preorder19dianMan.UpdatePreOrderRefundMoneyClosedSum(preOrder19dianId, money19dianDetail.changeValue);

                            // 更新修改后的退款金额
                            obj.refundMoneySum = Common.ToDouble(obj.refundMoneySum) + actualPayAmount;
                            obj.refundMoneyClosedSum = Common.ToDouble(obj.refundMoneyClosedSum) + actualPayAmount;

                            // 添加退款日志相关备用
                            objMoneyRefundOrder = listMoneyRefundOrder.FirstOrDefault(m => m.preOrder19dianId == preOrder19dianId);
                            if (objMoneyRefundOrder != null)
                            {
                                objMoneyRefundOrder.moneyAmount = Common.ToDouble(objMoneyRefundOrder.moneyAmount) + actualPayAmount;
                            }
                            else
                            {
                                objMoneyRefundOrder = new MoneyRefundOrder();
                                objMoneyRefundOrder.moneyAmount = actualPayAmount;
                                objMoneyRefundOrder.preOrder19dianId = preOrder19dianId;
                                objMoneyRefundOrder.orderID = preorderRefundRequest.orderId;
                                listMoneyRefundOrder.Add(objMoneyRefundOrder);
                            }

                            CustomerManager customerMan = new CustomerManager();
                            DataTable dtCustomer = customerMan.SelectCustomer(customerId);
                            customerMobilephone = Common.ToString(dtCustomer.Rows[0]["mobilePhoneNumber"]);

                            if (dtCustomer.Rows.Count == 1)
                            {
                                //记录退款记录
                                bool refundData = false;
                                refundData = Common.InsertRefundData(customerId, actualPayAmount, preOrder19dianId, "悠先服务粮票退款");

                                //// 用户在平台的VIP等级
                                //bool modifyVipInfo = false;
                                //double preOrderTotalAmount =
                                //    Common.ToDouble(dtCustomer.Rows[0]["preOrderTotalAmount"]);
                                //int preOrderTotalQuantity =
                                //    Common.ToInt32(dtCustomer.Rows[0]["preOrderTotalQuantity"]);
                                //int currentPlatformVipGrade =
                                //    Common.ToInt32(dtCustomer.Rows[0]["currentPlatformVipGrade"]);
                                //double cumulativeAmount = preOrderTotalAmount - actualPayAmount;
                                //if ((canRefundSum - actualPayAmount) < 0.001)
                                //{
                                //    modifyVipInfo = Common.ModifyUserPlatVip(customerId, preOrderTotalQuantity,
                                //        currentPlatformVipGrade, -actualPayAmount, cumulativeAmount, true);
                                //}
                                //else
                                //{
                                //    modifyVipInfo = Common.ModifyUserPlatVip(customerId, preOrderTotalQuantity,
                                //        currentPlatformVipGrade, -actualPayAmount, cumulativeAmount, false);
                                //}

                                // 修改用户在平台的消费金额
                                bool modifyVipInfo = customerMan.UpdateCustomerPartInfo(-actualPayAmount, 0, customerId);

                                //退款要更新支付明细表的退款金额及状态
                                Preorder19DianLineManager lineManager = new Preorder19DianLineManager();
                                Model.QueryObject.Preorder19DianLineQueryObject queryObject = new Model.QueryObject.Preorder19DianLineQueryObject()
                                {
                                    Preorder19DianId = preOrder19dianId
                                };
                                List<IPreorder19DianLine> orderLineList = lineManager.GetListByQuery(queryObject);
                                bool updateLine = false;
                                foreach (IPreorder19DianLine line in orderLineList)
                                {
                                    switch (line.PayType)
                                    {
                                        case (int)VAOrderUsedPayMode.ALIPAY:
                                        case (int)VAOrderUsedPayMode.WECHAT:
                                        case (int)VAOrderUsedPayMode.UNIONPAY:
                                            continue;
                                        case (int)VAOrderUsedPayMode.BALANCE:
                                            line.RefundAmount = line.RefundAmount + actualPayAmount;//已经退的钱+本次退的粮票金额
                                            break;
                                        case (int)VAOrderUsedPayMode.COUPON:
                                        case (int)VAOrderUsedPayMode.REDENVELOPE:
                                            continue;
                                    }
                                    updateLine = lineManager.Update(line);//更新每种支付类型的退款金额及状态
                                }

                                if (updatePreorder && refundData && flagUpdateCustomerMoneyRemain && flagUpdatePreOrderRefundMoneyClosedSum && modifyVipInfo)
                                {
                                    scope.Complete();
                                    isRefundOk = true;
                                }
                                else
                                {
                                    preorderRefundResponse.result = VAResult.VA_PREORDER_REFUND_ERROR; //退款失败
                                    refundsum = 0;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            // 未支付，未审核，已对账 不需要处理
                            continue;
                        }
                    }
                }
                #endregion

                //c、粮票都退完了，再循环退每一个单子的红包
                #region 红包退款
                foreach (PreOrder19dianInfo obj in list)
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        //已支付，已审核，未对账
                        if (Common.ToInt32(obj.isPaid) == 1 && Common.ToInt32(obj.isApproved) == 0 && Common.ToInt32(obj.isShopConfirmed) == 1)
                        {
                            if (refundsum <= 0)
                            {
                                break;
                            }

                            shopId = obj.shopId;
                            int companyId = obj.companyId;
                            customerId = obj.customerId;
                            long preOrder19dianId = obj.preOrder19dianId;
                            // 记录正常点单的点单ID
                            if (obj.OrderType == OrderTypeEnum.Normal)
                            {
                                finalPreOrder19dianId = preOrder19dianId;
                            }
                            appBuildID = obj.appBuild;

                            // 已退款总额
                            double refundMoneySum = Convert.ToDouble(obj.refundMoneySum);
                            // 支付金额
                            double prepaidSum = Convert.ToDouble(obj.prePaidSum);
                            // 可以退款的额度
                            double canRefundSum = Common.ToDouble(prepaidSum - refundMoneySum);
                            // 已结算的退款总额
                            double refundMoneyClosedSum = Convert.ToDouble(obj.refundMoneyClosedSum);

                            //该点单使用第三方支付的金额
                            ThirdPartyPaymentInfo thirdPartyPaymentInfo = preorder19dianMan.SelectPreorderPayAmount(obj.preOrder19dianId);
                            //额外收取的钱
                            double extendPay = preorder19dianMan.SelectExtendPay(obj.preOrder19dianId);
                            //不退还系统额外收取的钱
                            double payAmount = thirdPartyPaymentInfo.Amount - extendPay;

                            RedEnvelopeConnPreOrderOperate redEnvelopeOperate = new RedEnvelopeConnPreOrderOperate();
                            //当前点单使用红包抵扣的金额
                            double redEnvelopeConsumed = redEnvelopeOperate.GetPayOrderConsumeRedEnvelopeAmount(preOrder19dianId);
                            //该点单使用粮票支付的金额
                            double foodCoupon = prepaidSum - payAmount - redEnvelopeConsumed;

                            if (redEnvelopeConsumed <= 0)
                            {
                                continue;
                            }

                            // 已退款总额大于第三方支付的额度和粮票+红包，需要处理下一条
                            if ((refundMoneySum - payAmount - foodCoupon - redEnvelopeConsumed) > -0.001)
                            {
                                continue;
                            }
                            isRefundOk = false;

                            // 该单实际可以退还的红包金额
                            double actualPayAmount = redEnvelopeConsumed - (refundMoneySum - payAmount - foodCoupon);
                            if (actualPayAmount > refundsum)
                                actualPayAmount = refundsum;
                            // 该单第三方+粮票退款完成后，还需要退还的申请退款额度
                            refundsum = refundsum - actualPayAmount;

                            // 如果申请退款的金额已经小于0了， 表示不需要把余下的红包全部退完了， 只需要退实际要求退款的量即可
                            if (refundsum < 0)
                            {
                                actualPayAmount = refundsum + actualPayAmount;
                                refundsum = 0;
                            }

                            //修改预点单状态
                            bool updatePreorder = false;
                            // 本单的实际退款金额等于最大可退款金额，表示全部退完了
                            if (Common.ToDecimal(actualPayAmount) == Common.ToDecimal(canRefundSum))
                            {
                                updatePreorder = preorder19dianMan.UpdatePreOrderRefundInfo(preOrder19dianId, VAPreorderStatus.Refund, actualPayAmount);
                            }
                            else
                            {
                                updatePreorder = preorder19dianMan.UpdatePreOrderRefundMoneySum(preOrder19dianId, actualPayAmount);
                            }

                            // 红包退款
                            bool flagUpdateRefundRedEnvelope = false;
                            if (actualPayAmount > 0.001)
                            {
                                SybMoneyCustomerOperate.RedEnvelopePartialRefund(preOrder19dianId, customerId, actualPayAmount, ref flagUpdateRefundRedEnvelope);
                            }
                            else
                            {
                                flagUpdateRefundRedEnvelope = true;
                            }

                            // 更新修改后的退款金额
                            obj.refundMoneySum = Common.ToDouble(obj.refundMoneySum) + actualPayAmount;
                            obj.refundMoneyClosedSum = Common.ToDouble(obj.refundMoneyClosedSum) + actualPayAmount;
                            obj.refundRedEnvelope = Common.ToDouble(obj.refundRedEnvelope) + actualPayAmount;

                            // 添加退款日志相关备用
                            objMoneyRefundOrder = listMoneyRefundOrder.FirstOrDefault(m => m.preOrder19dianId == preOrder19dianId);
                            if (objMoneyRefundOrder != null)
                            {
                                objMoneyRefundOrder.moneyAmount = Common.ToDouble(objMoneyRefundOrder.moneyAmount) + actualPayAmount;
                            }
                            else
                            {
                                objMoneyRefundOrder = new MoneyRefundOrder();
                                objMoneyRefundOrder.moneyAmount = actualPayAmount;
                                objMoneyRefundOrder.preOrder19dianId = preOrder19dianId;
                                objMoneyRefundOrder.orderID = preorderRefundRequest.orderId;
                                listMoneyRefundOrder.Add(objMoneyRefundOrder);
                            }

                            CustomerManager customerMan = new CustomerManager();
                            DataTable dtCustomer = customerMan.SelectCustomer(customerId);
                            customerMobilephone = Common.ToString(dtCustomer.Rows[0]["mobilePhoneNumber"]);

                            if (dtCustomer.Rows.Count == 1)
                            {
                                //记录退款记录
                                bool refundData = false;
                                refundData = Common.InsertRefundData(customerId, actualPayAmount, preOrder19dianId, "悠先服务红包退款");

                                //// 用户在平台的VIP等级
                                //bool modifyVipInfo = false;
                                //double preOrderTotalAmount =
                                //    Common.ToDouble(dtCustomer.Rows[0]["preOrderTotalAmount"]);
                                //int preOrderTotalQuantity =
                                //    Common.ToInt32(dtCustomer.Rows[0]["preOrderTotalQuantity"]);
                                //int currentPlatformVipGrade =
                                //    Common.ToInt32(dtCustomer.Rows[0]["currentPlatformVipGrade"]);
                                //double cumulativeAmount = preOrderTotalAmount - actualPayAmount;
                                //if ((canRefundSum - actualPayAmount) < 0.001)
                                //{
                                //    modifyVipInfo = Common.ModifyUserPlatVip(customerId, preOrderTotalQuantity,
                                //        currentPlatformVipGrade, -actualPayAmount, cumulativeAmount, true);
                                //}
                                //else
                                //{
                                //    modifyVipInfo = Common.ModifyUserPlatVip(customerId, preOrderTotalQuantity,
                                //        currentPlatformVipGrade, -actualPayAmount, cumulativeAmount, false);
                                //}
                                
                                // 修改用户在平台的消费金额
                                bool modifyVipInfo =customerMan.UpdateCustomerPartInfo(-actualPayAmount, 0, customerId);

                                //退款要更新支付明细表的退款金额及状态
                                Preorder19DianLineManager lineManager = new Preorder19DianLineManager();
                                Model.QueryObject.Preorder19DianLineQueryObject queryObject = new Model.QueryObject.Preorder19DianLineQueryObject()
                                {
                                    Preorder19DianId = preOrder19dianId
                                };
                                List<IPreorder19DianLine> orderLineList = lineManager.GetListByQuery(queryObject);
                                bool updateLine = false;
                                foreach (IPreorder19DianLine line in orderLineList)
                                {
                                    switch (line.PayType)
                                    {
                                        case (int)VAOrderUsedPayMode.ALIPAY:
                                        case (int)VAOrderUsedPayMode.WECHAT:
                                        case (int)VAOrderUsedPayMode.UNIONPAY:
                                        case (int)VAOrderUsedPayMode.BALANCE:
                                        case (int)VAOrderUsedPayMode.COUPON:
                                            continue;
                                        case (int)VAOrderUsedPayMode.REDENVELOPE:
                                            line.RefundAmount = line.RefundAmount + actualPayAmount;//已经退的钱+本次退的红包金额
                                            break;
                                    }
                                    updateLine = lineManager.Update(line);//更新每种支付类型的退款金额及状态
                                }

                                if (flagUpdateRefundRedEnvelope && refundData && modifyVipInfo)
                                {
                                    scope.Complete();
                                    isRefundOk = true;
                                }
                                else
                                {
                                    preorderRefundResponse.result = VAResult.VA_PREORDER_REFUND_ERROR; //退款失败
                                    refundsum = 0;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            // 未支付，未审核，已对账 不需要处理
                            continue;
                        }
                    }
                }
                #endregion
                // 分步退款成功后， 统一发布短信和推送
                #region 推送
                if (isRefundOk)
                {
                    // 修改order表里面总退款和额度
                    // 退款的额度等于最大可退款额度，表示已退完
                    if (Common.ToDecimal(finalRefundSum) == Common.ToDecimal(canRefundSumTotal))
                    {
                        // 涉及第三方退款并且已退完， 把退款状态改成退款中
                        // 查找是否存在第三方支付
                        hasThirdRefundMoney=preorder19dianMan.GetThirdPay(preorderRefundRequest.orderId);
                        if (hasThirdRefundMoney)
                        {
                            preorder19dianMan.UpdateOrderRefundMoney(preorderRefundRequest.orderId, VAPreorderStatus.OriginalRefunding, finalRefundSum, finalRefundSum);
                        }
                        else
                        {
                            var status = (VAPreorderStatus)new OrderManager().GetEntityById(preorderRefundRequest.orderId).Status;
                            if (status != VAPreorderStatus.OriginalRefunding)
                                status = VAPreorderStatus.Refund;
                            if (status != VAPreorderStatus.Refund && new Preorder19DianLineManager().IsMoneyPaymentOfOrderId(preorderRefundRequest.orderId))
                                status = VAPreorderStatus.OriginalRefunding;

                            // order表里的退款状态改成 已退款
                            preorder19dianMan.UpdateOrderRefundMoney(preorderRefundRequest.orderId, status, finalRefundSum, finalRefundSum);
                        }

                        // 全额退款修改用户在平台的消费次数 -1
                        CustomerManager customerMan = new CustomerManager();
                        bool modifyVipInfo = customerMan.UpdateCustomerPartInfo(0, -1, customerId);
                    }
                    else
                    {
                        // 没有退完，退款状态依然为已付款
                        preorder19dianMan.UpdateOrderRefundMoney(preorderRefundRequest.orderId, VAPreorderStatus.Prepaid, finalRefundSum, finalRefundSum);
                    }

                    var dateTime = System.DateTime.Now;
                    foreach (var item in listMoneyRefundOrder)
                    {
                        MoneyRefundDetail moneyRefundDetail = new MoneyRefundDetail
                        {
                            preOrder19dianId = Common.ToInt64(item.preOrder19dianId),
                            refundMoney = Common.ToDecimal(item.moneyAmount),
                            remark = "悠先服务原路退款",
                            operUser = Common.ToString(checkResult.dtEmployee.Rows[0]["EmployeeID"]),
                            //不记录当前员工编号，后期无法查询当前的退款操作人
                            operTime = dateTime,
                            orderID = item.orderID
                        };
                        //退款日志
                        bool resultInsertMoneyRefundDetail = SybMoneyCustomerOperate.InsertMoneyRefundDetail(moneyRefundDetail);
                    }

                    ShopManager shopMan = new ShopManager();
                    DataTable dtShop = shopMan.SelectShop(shopId);
                    isRefundOk = true;
                    preorderRefundResponse.result = VAResult.VA_OK;
                    if (customerMobilephone.Length == 11)
                    {
                        string shopName = "";
                        if (dtShop.Rows.Count == 1)
                        {
                            shopName = Common.ToString(dtShop.Rows[0]["shopName"]);
                        }
                        string smsContent =
                            ConfigurationManager.AppSettings["sybRefundMessage"].Trim();
                        smsContent = smsContent.Replace("{0}", shopName);
                        smsContent = smsContent.Replace("{1}", Common.ToString(finalRefundSum));
                        Common.SendMessageBySms(customerMobilephone, smsContent);

                        CustomPushRecordOperate customPushRecordOperate = new CustomPushRecordOperate();
                        UniPushInfo uniPushInfo = new UniPushInfo()
                        {
                            customerPhone = customerMobilephone,
                            //preOrder19dianId = preOrder19dianId,
                            orderId = preorderRefundRequest.orderId,
                            shopName = shopName,
                            pushMessage = VAPushMessage.退款成功,
                            clientBuild = appBuildID
                        };
                        Thread refundThread = new Thread(customPushRecordOperate.UniPush);
                        refundThread.Start(uniPushInfo);
                    }
                }
                #endregion

            }
            else
            {
                // 申请的退款金额不能大于最大退款额度
                preorderRefundResponse.result = VAResult.VA_REFUND_NO_MONEY; //退款失败
            }

        }

        /// <summary>
        /// 掌中宝客户端退款
        /// </summary>
        /// <param name="preorderRefundRequest"></param>
        /// <returns></returns>
        public ZZB_VAPreOrderRefundResponse ZZBClientPreOrderRefund(ZZB_VAPreOrderRefundRequest preorderRefundRequest)
        {
            ZZB_VAPreOrderRefundResponse preorderRefundResponse = new ZZB_VAPreOrderRefundResponse();
            preorderRefundResponse.type = VAMessageType.ZZB_CLIENT_PREORDERREFUND_RESPONSE;
            CheckCookieAndMsgForZZZ checkResult = Common.CheckCookieAndMsgForZZZ(preorderRefundRequest.cookie,
                (int)preorderRefundRequest.type, (int)VAMessageType.ZZB_CLIENT_PREORDERREFUND_REQUEST, preorderRefundRequest.shopId);
            if (checkResult.result == VAResult.VA_OK)
            {
                int employeeId = Common.ToInt32(checkResult.dtEmployee.Rows[0]["EmployeeID"]);
                bool isViewAllocWorker = Common.ToBool(checkResult.dtEmployee.Rows[0]["isViewAllocWorker"]);
                if (
                    ServiceFactory.Resolve<IShopAuthorityService>()
                        .GetEmployeeHasShopAuthority(preorderRefundRequest.shopId, employeeId, isViewAllocWorker,
                            ShopRole.店员退款.GetString()))
                {
                    List<PreOrder19dianInfo> listPreOrder19dianInfo = new List<PreOrder19dianInfo>();
                    PreOrder19dianManager preorder19dianMan = new PreOrder19dianManager();
                    // 订单OrderID不为空表示新版本20150630后版本
                    if (preorderRefundRequest.orderId != Guid.Empty)
                    {
                        listPreOrder19dianInfo = preorder19dianMan.GetPreOrder19dianByOrderId(preorderRefundRequest.orderId);
                    }
                    else// 老版本
                    {
                        listPreOrder19dianInfo.Add(new PreOrder19dianInfo() { preOrder19dianId = preorderRefundRequest.preOrder19dianId });
                    }

                    if (preorderRefundRequest.orderId != Guid.Empty)
                    {
                        // 特殊处理补差价订单退款
                        FillPostMoney(preorderRefundRequest, listPreOrder19dianInfo, preorderRefundResponse, preorder19dianMan, checkResult);
                    }
                    else
                    {
                        long preOrder19dianId = listPreOrder19dianInfo.FirstOrDefault().preOrder19dianId;
                        if (preOrder19dianId > 0)
                        {
                            bool isThirdRefundMoney = false;

                            ThirdPartyPaymentInfo thirdPartyPaymentInfo =
                                preorder19dianMan.SelectPreorderPayAmount(preOrder19dianId); //该点单使用第三方支付的金额
                            double extendPay = preorder19dianMan.SelectExtendPay(preOrder19dianId);//额外收取的钱
                            double payAmount = thirdPartyPaymentInfo.Amount - extendPay;//不退还系统额外收取的钱

                            RedEnvelopeConnPreOrderOperate redEnvelopeOperate = new RedEnvelopeConnPreOrderOperate();
                            double redEnvelopeConsumed = redEnvelopeOperate.GetPayOrderConsumeRedEnvelopeAmount(preOrder19dianId);//当前点单使用红包抵扣的金额

                            using (TransactionScope scope = new TransactionScope())
                            {
                                PreOrder19dianOperate preOperate = new PreOrder19dianOperate();
                                DataTable dtPreOrderInfo = preOperate.QueryPreOrderById(preOrder19dianId);
                                // add by zhujinlei 获取orderID 2015/07/10
                                if (dtPreOrderInfo.Rows.Count > 0)
                                {
                                    string guidOrderID = string.IsNullOrEmpty(Convert.ToString(dtPreOrderInfo.Rows[0]["OrderId"])) ? Guid.Empty.ToString() : Convert.ToString(dtPreOrderInfo.Rows[0]["OrderId"]);
                                    preorderRefundRequest.orderId = new Guid(guidOrderID);
                                }

                                if (Common.ToInt32(dtPreOrderInfo.Rows[0]["isPaid"]) == 1
                                    && Common.ToInt32(dtPreOrderInfo.Rows[0]["isApproved"]) == 0
                                    && Common.ToInt32(dtPreOrderInfo.Rows[0]["isShopConfirmed"]) == 1) //已支付，已审核，未对账
                                {
                                    int shopId = Common.ToInt32(dtPreOrderInfo.Rows[0]["shopId"]);
                                    int companyId = Common.ToInt32(dtPreOrderInfo.Rows[0]["companyId"]);
                                    long customerId = Common.ToInt32(dtPreOrderInfo.Rows[0]["customerId"]);
                                    double refundsum = Common.ToDouble(preorderRefundRequest.refundAccount); //提交退款金额

                                    // 已退款总额
                                    double refundMoneySum = Common.ToDouble(dtPreOrderInfo.Rows[0]["refundMoneySum"]);
                                    // 支付金额
                                    double prepaidSum = Common.ToDouble(dtPreOrderInfo.Rows[0]["prePaidSum"]);
                                    // 可以退款的额度
                                    double canRefundSum = Common.ToDouble(prepaidSum - refundMoneySum);
                                    // 已结算的退款总额
                                    double refundMoneyClosedSum = Common.ToDouble(dtPreOrderInfo.Rows[0]["refundMoneyClosedSum"]);

                                    // 可退款金额大于申请的退款金额，可以退款
                                    if ((canRefundSum - refundsum) > -0.001)
                                    {
                                        //修改预点单状态
                                        bool updatePreorder = false;

                                        // 第三方支付大于等于1分就需要退款
                                        if (payAmount > 0.009)
                                        {
                                            //有支付 
                                            if ((payAmount - refundsum - refundMoneySum) < 0.001)
                                            {
                                                //原路的全部退完了
                                                if (payAmount - refundMoneySum < 0.001)
                                                {
                                                    //当次退款前原路已经全部退完了
                                                    if (refundsum + refundMoneySum - prepaidSum > -0.001)
                                                    {
                                                        // 已打款金额大于退款金额
                                                        if (refundMoneyClosedSum - refundMoneySum > -0.001)
                                                        {
                                                            updatePreorder = preorder19dianMan.UpdatePreOrderRefundInfo(preOrder19dianId, VAPreorderStatus.Refund, refundsum);
                                                        }
                                                        else
                                                        {
                                                            isThirdRefundMoney = true;
                                                            updatePreorder = preorder19dianMan.UpdatePreOrderRefundInfo(preOrder19dianId, VAPreorderStatus.OriginalRefunding, refundsum);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        updatePreorder = preorder19dianMan.UpdatePreOrderRefundMoneySum(preOrder19dianId, refundsum);
                                                    }
                                                }
                                                else
                                                {
                                                    if (refundsum + refundMoneySum - prepaidSum > -0.001)
                                                    {
                                                        isThirdRefundMoney = true;
                                                        // 第三方退款中，已经没有可退的了
                                                        updatePreorder = preorder19dianMan.UpdatePreOrderRefundInfo(preOrder19dianId, VAPreorderStatus.OriginalRefunding, refundsum);
                                                    }
                                                    else
                                                    {
                                                        // 第三方退款， 还可以继续退
                                                        updatePreorder = preorder19dianMan.UpdatePreOrderRefundMoneySum(preOrder19dianId, refundsum);
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                updatePreorder = preorder19dianMan.UpdatePreOrderRefundMoneySum(preOrder19dianId, refundsum);
                                            }
                                        }
                                        else // 没有涉及第三方支付
                                        {
                                            if (refundsum + refundMoneySum - prepaidSum > -0.001)
                                            {
                                                // 全部退完了
                                                updatePreorder = preorder19dianMan.UpdatePreOrderRefundInfo(preOrder19dianId, VAPreorderStatus.Refund, refundsum);
                                            }
                                            else
                                            {
                                                // 还可以继续退
                                                updatePreorder = preorder19dianMan.UpdatePreOrderRefundMoneySum(preOrder19dianId, refundsum);
                                            }
                                        }

                                        bool flagOriginalRefundRecord = false;
                                        bool flagUpdateCustomerMoneyRemain = true;
                                        bool flagUpdatePreOrderRefundMoneyClosedSum = true;
                                        CustomerManager customerMan = new CustomerManager();
                                        DataTable dtCustomer = customerMan.SelectCustomer(customerId);
                                        string customerMobilephone = Common.ToString(dtCustomer.Rows[0]["mobilePhoneNumber"]);
                                        //bool viewallocBalance = false;
                                        bool refundData = false;
                                        bool modifyVipInfo = false;
                                        bool resultInsertMoneyRefundDetail = false;
                                        //bool opintOperateFlag = false;

                                        double refundFoodCoupon = 0;//本次退到粮票的钱
                                        double refundRedEnvelope = 0;//本次退到红包的钱

                                        OriginalRoadRefundInfo originalRoadRefund = new OriginalRoadRefundInfo();

                                        if (dtCustomer.Rows.Count == 1)
                                        {
                                            MoneyRefundDetail moneyRefundDetail = new MoneyRefundDetail
                                            {
                                                preOrder19dianId = Common.ToInt64(preOrder19dianId),
                                                refundMoney = Common.ToDecimal(refundsum),
                                                remark = "悠先服务原路退款",
                                                operUser = Common.ToString(checkResult.dtEmployee.Rows[0]["EmployeeID"]),
                                                //不记录当前员工编号，后期无法查询当前的退款操作人
                                                operTime = System.DateTime.Now,
                                                orderID = preorderRefundRequest.orderId
                                            };
                                            resultInsertMoneyRefundDetail = SybMoneyCustomerOperate.InsertMoneyRefundDetail(moneyRefundDetail); //退款日志
                                            bool flagUpdateRefundRedEnvelope = false;

                                            double foodCoupon = prepaidSum - payAmount - redEnvelopeConsumed;//该点单使用粮票支付的金额
                                            double refundedFoodCoupon = 0;//已退到粮票的钱
                                            if (refundMoneySum - payAmount < -0.001)
                                            {
                                                refundedFoodCoupon = 0;//第三方还未退完，已退粮票必然为0
                                            }
                                            else
                                            {
                                                // 退款总款-第三方支付-粮票支付的金额
                                                if (refundMoneySum - payAmount - foodCoupon < -0.001)
                                                {
                                                    refundedFoodCoupon = refundMoneySum - payAmount;
                                                }
                                                else
                                                {
                                                    refundedFoodCoupon = foodCoupon;
                                                }
                                            }

                                            double moneyBackToRemain = Common.ToDouble((refundsum + refundMoneySum) - payAmount);
                                            if (moneyBackToRemain > 0)
                                            {
                                                if (moneyBackToRemain > refundsum)
                                                {
                                                    moneyBackToRemain = refundsum;
                                                }
                                                Money19dianDetail money19dianDetail = new Money19dianDetail
                                                {
                                                    customerId = customerId,
                                                    changeReason = Common.GetEnumDescription(VA19dianMoneyChangeReason.MONEY19DIAN_REFUND_PREORDER).Replace("{0}", Common.ToString(preOrder19dianId)),
                                                    changeTime = System.DateTime.Now,
                                                    flowNumber = SybMoneyOperate.CreateCustomerFlowNumber(customerId), //流水号
                                                    accountType = (int)AccountType.USER_CANCEL_ORDER, //用户取消订单
                                                    accountTypeConnId = Common.ToString(preOrder19dianId),
                                                    inoutcomeType = (int)InoutcomeType.IN, //相对于用户退款就是收入
                                                    companyId = companyId,
                                                    shopId = shopId
                                                };
                                                if (refundedFoodCoupon - foodCoupon < -0.001)//有要退到粮票的钱
                                                {
                                                    refundFoodCoupon = foodCoupon - refundedFoodCoupon;//本次退到粮票的钱
                                                    if (moneyBackToRemain - refundFoodCoupon < -0.001)
                                                    {
                                                        refundFoodCoupon = moneyBackToRemain;
                                                    }

                                                    // 红包
                                                    refundRedEnvelope = moneyBackToRemain - refundFoodCoupon;

                                                    money19dianDetail.changeValue = refundFoodCoupon;
                                                    // 修改用户余额（加上退款的粮票）
                                                    money19dianDetail.remainMoney = Money19dianDetailManager.GetCustomerRemainMoney(customerId) + refundFoodCoupon;

                                                    flagUpdateCustomerMoneyRemain = SybMoneyCustomerOperate.AccountBalanceChanges(money19dianDetail);
                                                    flagUpdatePreOrderRefundMoneyClosedSum = preorder19dianMan.UpdatePreOrderRefundMoneyClosedSum(preOrder19dianId, money19dianDetail.changeValue);
                                                    originalRoadRefund.refundAmount = Common.ToDouble(refundsum - moneyBackToRemain);
                                                }
                                                else // 没有要退到粮票的钱了
                                                {
                                                    money19dianDetail.changeValue = 0;
                                                    money19dianDetail.remainMoney = Money19dianDetailManager.GetCustomerRemainMoney(customerId) + 0;

                                                    originalRoadRefund.refundAmount = refundsum - moneyBackToRemain;
                                                    refundRedEnvelope = moneyBackToRemain;//红包作废不退还 
                                                }
                                                if (refundRedEnvelope > 0.001)
                                                {
                                                    //flagUpdateRefundRedEnvelope = preorder19dianMan.UpdatePreOrderRefundRedEnvelope(preOrder19dianId, refundRedEnvelope, refundRedEnvelope);
                                                    SybMoneyCustomerOperate.RedEnvelopePartialRefund(preOrder19dianId, customerId, refundRedEnvelope, ref flagUpdateRefundRedEnvelope);
                                                }
                                                else
                                                {
                                                    flagUpdateRefundRedEnvelope = true;
                                                }
                                            }
                                            else
                                            {
                                                originalRoadRefund.refundAmount = refundsum;
                                            }
                                            originalRoadRefund.type = VAOriginalRefundType.PREORDER;
                                            originalRoadRefund.connId = preOrder19dianId;
                                            originalRoadRefund.customerMobilephone = customerMobilephone;
                                            originalRoadRefund.customerUserName =
                                                Common.ToString(dtCustomer.Rows[0]["UserName"]);

                                            originalRoadRefund.status = (int)VAOriginalRefundStatus.REMITTING;
                                            originalRoadRefund.employeeId =
                                                Common.ToInt32(checkResult.dtEmployee.Rows[0]["EmployeeID"]);

                                            if (originalRoadRefund.refundAmount > 0.009)
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
                                                originalRoadRefund.id =
                                                    preorder19dianMan.InsertOriginalRoadRefund(originalRoadRefund);
                                                if (originalRoadRefund.id > 0)
                                                {
                                                    //记录原路退款记录
                                                    flagOriginalRefundRecord = true;
                                                    originalRoadRefund.tradeNo = thirdPartyPaymentInfo.tradeNo;
                                                    //这里加入退款流程
                                                    ThreadPool.QueueUserWorkItem(
                                                        new RefundCallBack(preOrder19dianId, thirdPartyPaymentInfo,
                                                            (float)originalRoadRefund.refundAmount, originalRoadRefund)
                                                            .Refund);
                                                }
                                                else
                                                {
                                                    flagOriginalRefundRecord = false;
                                                }
                                            }
                                            else
                                            {
                                                flagOriginalRefundRecord = true;
                                            }
                                            //公司
                                            //MoneyViewallocAccountDetail moneyViewallocAccountDetailModel = new MoneyViewallocAccountDetail
                                            //{
                                            //    accountMoney = -1 * (refundsum),
                                            //    remainMoney =
                                            //        Money19dianDetailManager.GetViewAllocRemainMoney() + (-1 * (refundsum)),
                                            //    //获得公司当前时间账户余额
                                            //    accountType = (int)AccountType.USER_CANCEL_ORDER, //用户取消订单
                                            //    accountTypeConnId = Common.ToString(preOrder19dianId),
                                            //    inoutcomeType = (int)InoutcomeType.OUT, //相对于公司退款就是支出
                                            //    operUser = "用户customerID：" + Common.ToString(customerId),
                                            //    operTime = DateTime.Now,
                                            //    outcomeCompanyId = companyId,
                                            //    outcomeShopId = shopId,
                                            //    remark = "悠先服务原路退款"
                                            //};
                                            //viewallocBalance = SybMoneyViewallocOperate.ViewAllocAccountBalanceChanges(moneyViewallocAccountDetailModel);
                                            refundData = Common.InsertRefundData(customerId, refundsum, preOrder19dianId, "悠先服务原路退款"); //记录退款记录
                                            double preOrderTotalAmount =
                                                Common.ToDouble(dtCustomer.Rows[0]["preOrderTotalAmount"]);
                                            int preOrderTotalQuantity =
                                                Common.ToInt32(dtCustomer.Rows[0]["preOrderTotalQuantity"]);
                                            int currentPlatformVipGrade =
                                                Common.ToInt32(dtCustomer.Rows[0]["currentPlatformVipGrade"]);
                                            double cumulativeAmount = preOrderTotalAmount - refundsum;
                                            if ((canRefundSum - refundsum) < 0.001)
                                            {
                                                modifyVipInfo = Common.ModifyUserPlatVip(customerId, preOrderTotalQuantity,
                                                    currentPlatformVipGrade, -refundsum, cumulativeAmount, true);
                                            }
                                            else
                                            {
                                                modifyVipInfo = Common.ModifyUserPlatVip(customerId, preOrderTotalQuantity,
                                                    currentPlatformVipGrade, -refundsum, cumulativeAmount, false);
                                            }
                                            //悠先服务，更新当前服务员积分信息
                                            //EmployeePointOperate pointOperate = new EmployeePointOperate();
                                            //opintOperateFlag = pointOperate.RefundOpdatePoint(dtPreOrderInfo,
                                            //    Common.ToInt32(checkResult.dtEmployee.Rows[0]["EmployeeID"]), refundsum);
                                        }

                                        //退款要更新支付明细表的退款金额及状态，Add at 2015-4-15
                                        Preorder19DianLineManager lineManager = new Preorder19DianLineManager();
                                        Model.QueryObject.Preorder19DianLineQueryObject queryObject = new Model.QueryObject.Preorder19DianLineQueryObject()
                                        {
                                            Preorder19DianId = preOrder19dianId
                                        };
                                        List<IPreorder19DianLine> orderLineList = lineManager.GetListByQuery(queryObject);
                                        bool updateLine = false;
                                        foreach (IPreorder19DianLine line in orderLineList)
                                        {
                                            switch (line.PayType)
                                            {
                                                case (int)VAOrderUsedPayMode.ALIPAY:
                                                case (int)VAOrderUsedPayMode.WECHAT:
                                                case (int)VAOrderUsedPayMode.UNIONPAY:
                                                    line.RefundAmount = line.RefundAmount + originalRoadRefund.refundAmount;//已经退的钱+本次退的第三方金额
                                                    break;
                                                case (int)VAOrderUsedPayMode.BALANCE:
                                                    line.RefundAmount = line.RefundAmount + refundFoodCoupon;//已经退的钱+本次退到粮票的钱
                                                    break;
                                                case (int)VAOrderUsedPayMode.COUPON:
                                                    line.RefundAmount = 0;//商户退款，抵扣券不返还
                                                    break;
                                                case (int)VAOrderUsedPayMode.REDENVELOPE:
                                                    line.RefundAmount = line.RefundAmount + refundRedEnvelope;//已经退的钱+本次退到红包的钱
                                                    break;
                                            }
                                            updateLine = lineManager.Update(line);//更新每种支付类型的退款金额及状态
                                        }
                                        if (updatePreorder && flagOriginalRefundRecord && refundData && modifyVipInfo && flagUpdatePreOrderRefundMoneyClosedSum)//&& viewallocBalance&& opintOperateFlag
                                        {
                                            ShopManager shopMan = new ShopManager();
                                            DataTable dtShop = shopMan.SelectShop(shopId);

                                            // 已全部退完，修改order表里的状态为已退款
                                            var orderEntity = new OrderManager().GetEntityById(preorderRefundRequest.orderId);
                                            if ((decimal)orderEntity.PrePaidSum == (decimal)(orderEntity.RefundMoneySum + refundsum))
                                            {
                                                var status = VAPreorderStatus.Refund;
                                                if (isThirdRefundMoney)
                                                    status = VAPreorderStatus.OriginalRefunding;
                                                // order表里的退款状态改成 已退款
                                                preorder19dianMan.UpdateOrderRefundMoney(preorderRefundRequest.orderId, status, refundsum, refundsum);
                                            }
                                            else
                                            {
                                                preorder19dianMan.UpdateOrderRefundMoney(preorderRefundRequest.orderId, VAPreorderStatus.Prepaid, refundsum, refundsum);
                                            }

                                            scope.Complete();
                                            preorderRefundResponse.result = VAResult.VA_OK;

                                            if (customerMobilephone.Length == 11)
                                            {
                                                string shopName = "";
                                                if (dtShop.Rows.Count == 1)
                                                {
                                                    shopName = Common.ToString(dtShop.Rows[0]["shopName"]);
                                                }
                                                string smsContent =
                                                    ConfigurationManager.AppSettings["sybRefundMessage"].Trim();
                                                smsContent = smsContent.Replace("{0}", shopName);
                                                smsContent = smsContent.Replace("{1}", Common.ToString(refundsum));
                                                Common.SendMessageBySms(customerMobilephone, smsContent);

                                                CustomPushRecordOperate customPushRecordOperate = new CustomPushRecordOperate();
                                                UniPushInfo uniPushInfo = new UniPushInfo()
                                                {
                                                    customerPhone = customerMobilephone,
                                                    preOrder19dianId = preOrder19dianId,
                                                    shopName = shopName,
                                                    pushMessage = VAPushMessage.退款成功,
                                                    clientBuild = dtPreOrderInfo.Rows[0]["appBuild"].ToString()
                                                };
                                                Thread refundThread = new Thread(customPushRecordOperate.UniPush);
                                                refundThread.Start(uniPushInfo);
                                            }
                                        }
                                        else
                                        {
                                            preorderRefundResponse.result = VAResult.VA_PREORDER_REFUND_ERROR; //退款失败
                                        }
                                    }
                                    else
                                    {
                                        preorderRefundResponse.result = VAResult.VA_REFUND_NO_MONEY; //退款失败
                                    }
                                }
                                else
                                {
                                    preorderRefundResponse.result = VAResult.VA_REFUND_NOTPAYORAPPROVED; //已对账或是未付款
                                }
                            }
                        }
                        else
                        {
                            preorderRefundResponse.result = VAResult.VA_FAILED_PREORDER_NOT_FOUND; //当前点单未找到
                        }
                    }
                }
                else
                {
                    preorderRefundResponse.result = VAResult.VA_NO_ACCESS_INTERFACE_AUTHORITY; //没有权限
                }
            }
            else
            {
                preorderRefundResponse.result = checkResult.result;
            }
            return preorderRefundResponse;
        }

        /// <summary>
        /// 掌中宝客户端退款备份 2015/06/26 add by zhujinlei
        /// </summary>
        /// <param name="preorderRefundRequest"></param>
        /// <returns></returns>
        public ZZB_VAPreOrderRefundResponse ZZBClientPreOrderRefundBak(ZZB_VAPreOrderRefundRequest preorderRefundRequest)
        {
            ZZB_VAPreOrderRefundResponse preorderRefundResponse = new ZZB_VAPreOrderRefundResponse();
            preorderRefundResponse.type = VAMessageType.ZZB_CLIENT_PREORDERREFUND_RESPONSE;
            CheckCookieAndMsgForZZZ checkResult = Common.CheckCookieAndMsgForZZZ(preorderRefundRequest.cookie,
                (int)preorderRefundRequest.type, (int)VAMessageType.ZZB_CLIENT_PREORDERREFUND_REQUEST, preorderRefundRequest.shopId);
            if (checkResult.result == VAResult.VA_OK)
            {
                int employeeId = Common.ToInt32(checkResult.dtEmployee.Rows[0]["EmployeeID"]);
                bool isViewAllocWorker = Common.ToBool(checkResult.dtEmployee.Rows[0]["isViewAllocWorker"]);
                if (
                    ServiceFactory.Resolve<IShopAuthorityService>()
                        .GetEmployeeHasShopAuthority(preorderRefundRequest.shopId, employeeId, isViewAllocWorker,
                            ShopRole.店员退款.GetString()))
                {
                    long preOrder19dianId = preorderRefundRequest.preOrder19dianId;
                    if (preOrder19dianId > 0)
                    {
                        PreOrder19dianManager preorder19dianMan = new PreOrder19dianManager();
                        ThirdPartyPaymentInfo thirdPartyPaymentInfo =
                            preorder19dianMan.SelectPreorderPayAmount(preOrder19dianId); //该点单使用第三方支付的金额
                        double extendPay = preorder19dianMan.SelectExtendPay(preOrder19dianId);//额外收取的钱
                        double payAmount = thirdPartyPaymentInfo.Amount - extendPay;//不退还系统额外收取的钱

                        RedEnvelopeConnPreOrderOperate redEnvelopeOperate = new RedEnvelopeConnPreOrderOperate();
                        double redEnvelopeConsumed = redEnvelopeOperate.GetPayOrderConsumeRedEnvelopeAmount(preOrder19dianId);//当前点单使用红包抵扣的金额

                        using (TransactionScope scope = new TransactionScope())
                        {
                            PreOrder19dianOperate preOperate = new PreOrder19dianOperate();
                            DataTable dtPreOrderInfo = preOperate.QueryPreOrderById(preOrder19dianId);
                            if (Common.ToInt32(dtPreOrderInfo.Rows[0]["isPaid"]) == 1
                                && Common.ToInt32(dtPreOrderInfo.Rows[0]["isApproved"]) == 0
                                && Common.ToInt32(dtPreOrderInfo.Rows[0]["isShopConfirmed"]) == 1) //已支付，已审核，未对账
                            {
                                int shopId = Common.ToInt32(dtPreOrderInfo.Rows[0]["shopId"]);
                                int companyId = Common.ToInt32(dtPreOrderInfo.Rows[0]["companyId"]);
                                long customerId = Common.ToInt32(dtPreOrderInfo.Rows[0]["customerId"]);
                                double refundsum = Common.ToDouble(preorderRefundRequest.refundAccount); //提交退款金额

                                // 这里实际支付和退款的金额要从order表里面取值
                                double refundMoneySum = Common.ToDouble(dtPreOrderInfo.Rows[0]["refundMoneySum"]);//已退款总额
                                double prepaidSum = Common.ToDouble(dtPreOrderInfo.Rows[0]["prePaidSum"]);
                                double canRefundSum = Common.ToDouble(prepaidSum - refundMoneySum);
                                double refundMoneyClosedSum =
                                    Common.ToDouble(dtPreOrderInfo.Rows[0]["refundMoneyClosedSum"]); //已结算的退款总额

                                // 可退款金额大于申请的退款金额，可以退款
                                if ((canRefundSum - refundsum) > -0.001)
                                {
                                    //修改预点单状态
                                    bool updatePreorder = false;

                                    // 第三方支付大于等于1分就需要退款
                                    if (payAmount > 0.009)
                                    {
                                        //有支付 
                                        if ((payAmount - refundsum - refundMoneySum) < 0.001)
                                        {
                                            //原路的全部退完了
                                            if (payAmount - refundMoneySum < 0.001)
                                            {
                                                //当次退款前原路已经全部退完了
                                                if (refundsum + refundMoneySum - prepaidSum > -0.001)
                                                {
                                                    // 已打款金额大于退款金额
                                                    if (refundMoneyClosedSum - refundMoneySum > -0.001)
                                                    {
                                                        updatePreorder = preorder19dianMan.UpdatePreOrderRefundInfo(preOrder19dianId, VAPreorderStatus.Refund, refundsum);
                                                    }
                                                    else
                                                    {
                                                        updatePreorder = preorder19dianMan.UpdatePreOrderRefundInfo(preOrder19dianId, VAPreorderStatus.OriginalRefunding, refundsum);
                                                    }
                                                }
                                                else
                                                {
                                                    updatePreorder = preorder19dianMan.UpdatePreOrderRefundMoneySum(preOrder19dianId, refundsum);
                                                }
                                            }
                                            else
                                            {
                                                if (refundsum + refundMoneySum - prepaidSum > -0.001)
                                                {
                                                    updatePreorder = preorder19dianMan.UpdatePreOrderRefundInfo(preOrder19dianId, VAPreorderStatus.OriginalRefunding, refundsum);
                                                }
                                                else
                                                {
                                                    updatePreorder = preorder19dianMan.UpdatePreOrderRefundMoneySum(preOrder19dianId, refundsum);
                                                }
                                            }
                                        }
                                        else
                                        {
                                            updatePreorder = preorder19dianMan.UpdatePreOrderRefundMoneySum(preOrder19dianId, refundsum);
                                        }
                                    }
                                    else
                                    {
                                        if (refundsum + refundMoneySum - prepaidSum > -0.001)
                                        {
                                            updatePreorder = preorder19dianMan.UpdatePreOrderRefundInfo(preOrder19dianId, VAPreorderStatus.Refund, refundsum);
                                        }
                                        else
                                        {
                                            updatePreorder = preorder19dianMan.UpdatePreOrderRefundMoneySum(preOrder19dianId, refundsum);
                                        }
                                    }
                                    bool flagOriginalRefundRecord = false;
                                    bool flagUpdateCustomerMoneyRemain = true;
                                    bool flagUpdatePreOrderRefundMoneyClosedSum = true;
                                    CustomerManager customerMan = new CustomerManager();
                                    DataTable dtCustomer = customerMan.SelectCustomer(customerId);
                                    string customerMobilephone = Common.ToString(dtCustomer.Rows[0]["mobilePhoneNumber"]);
                                    //bool viewallocBalance = false;
                                    bool refundData = false;
                                    bool modifyVipInfo = false;
                                    bool resultInsertMoneyRefundDetail = false;
                                    //bool opintOperateFlag = false;

                                    double refundFoodCoupon = 0;//本次退到粮票的钱
                                    double refundRedEnvelope = 0;//本次退到红包的钱

                                    OriginalRoadRefundInfo originalRoadRefund = new OriginalRoadRefundInfo();

                                    if (dtCustomer.Rows.Count == 1)
                                    {
                                        MoneyRefundDetail moneyRefundDetail = new MoneyRefundDetail
                                        {
                                            preOrder19dianId = Common.ToInt64(preOrder19dianId),
                                            refundMoney = Common.ToDecimal(refundsum),
                                            remark = "悠先服务原路退款",
                                            operUser = Common.ToString(checkResult.dtEmployee.Rows[0]["EmployeeID"]),
                                            //不记录当前员工编号，后期无法查询当前的退款操作人
                                            operTime = System.DateTime.Now
                                        };
                                        resultInsertMoneyRefundDetail = SybMoneyCustomerOperate.InsertMoneyRefundDetail(moneyRefundDetail); //退款日志
                                        bool flagUpdateRefundRedEnvelope = false;
                                        double foodCoupon = prepaidSum - payAmount - redEnvelopeConsumed;//该点单使用粮票支付的金额
                                        double refundedFoodCoupon = 0;//已退到粮票的钱
                                        if (refundMoneySum - payAmount < -0.001)
                                        {
                                            refundedFoodCoupon = 0;//第三方还未退完，已退粮票必然为0
                                        }
                                        else
                                        {
                                            if (refundMoneySum - payAmount - foodCoupon < -0.001)
                                            {
                                                refundedFoodCoupon = refundMoneySum - payAmount;
                                            }
                                            else
                                            {
                                                refundedFoodCoupon = foodCoupon;
                                            }
                                        }
                                        double moneyBackToRemain = Common.ToDouble((refundsum + refundMoneySum) - payAmount);
                                        if (moneyBackToRemain > 0)
                                        {
                                            if (moneyBackToRemain > refundsum)
                                            {
                                                moneyBackToRemain = refundsum;
                                            }
                                            Money19dianDetail money19dianDetail = new Money19dianDetail
                                            {
                                                customerId = customerId,
                                                changeReason = Common.GetEnumDescription(VA19dianMoneyChangeReason.MONEY19DIAN_REFUND_PREORDER).Replace("{0}", Common.ToString(preOrder19dianId)),
                                                changeTime = System.DateTime.Now,
                                                flowNumber = SybMoneyOperate.CreateCustomerFlowNumber(customerId), //流水号
                                                accountType = (int)AccountType.USER_CANCEL_ORDER, //用户取消订单
                                                accountTypeConnId = Common.ToString(preOrder19dianId),
                                                inoutcomeType = (int)InoutcomeType.IN, //相对于用户退款就是收入
                                                companyId = companyId,
                                                shopId = shopId
                                            };
                                            if (refundedFoodCoupon - foodCoupon < -0.001)//有要退到粮票的钱
                                            {
                                                refundFoodCoupon = foodCoupon - refundedFoodCoupon;//本次退到粮票的钱
                                                if (moneyBackToRemain - refundFoodCoupon < -0.001)
                                                {
                                                    refundFoodCoupon = moneyBackToRemain;
                                                }
                                                refundRedEnvelope = moneyBackToRemain - refundFoodCoupon;
                                                money19dianDetail.changeValue = refundFoodCoupon;
                                                money19dianDetail.remainMoney = Money19dianDetailManager.GetCustomerRemainMoney(customerId) + refundFoodCoupon;

                                                flagUpdateCustomerMoneyRemain = SybMoneyCustomerOperate.AccountBalanceChanges(money19dianDetail);
                                                flagUpdatePreOrderRefundMoneyClosedSum = preorder19dianMan.UpdatePreOrderRefundMoneyClosedSum(preOrder19dianId, money19dianDetail.changeValue);
                                                originalRoadRefund.refundAmount = Common.ToDouble(refundsum - moneyBackToRemain);
                                            }
                                            else
                                            {
                                                money19dianDetail.changeValue = 0;
                                                money19dianDetail.remainMoney = Money19dianDetailManager.GetCustomerRemainMoney(customerId) + 0;

                                                originalRoadRefund.refundAmount = refundsum - moneyBackToRemain;
                                                refundRedEnvelope = moneyBackToRemain;//红包作废不退还 
                                            }
                                            if (refundRedEnvelope > 0.001)
                                            {
                                                //flagUpdateRefundRedEnvelope = preorder19dianMan.UpdatePreOrderRefundRedEnvelope(preOrder19dianId, refundRedEnvelope, refundRedEnvelope);
                                                SybMoneyCustomerOperate.RedEnvelopePartialRefund(preOrder19dianId, customerId, refundRedEnvelope, ref flagUpdateRefundRedEnvelope);
                                            }
                                            else
                                            {
                                                flagUpdateRefundRedEnvelope = true;
                                            }
                                        }
                                        else
                                        {
                                            originalRoadRefund.refundAmount = refundsum;
                                        }
                                        originalRoadRefund.type = VAOriginalRefundType.PREORDER;
                                        originalRoadRefund.connId = preOrder19dianId;
                                        originalRoadRefund.customerMobilephone = customerMobilephone;
                                        originalRoadRefund.customerUserName =
                                            Common.ToString(dtCustomer.Rows[0]["UserName"]);

                                        originalRoadRefund.status = (int)VAOriginalRefundStatus.REMITTING;
                                        originalRoadRefund.employeeId =
                                            Common.ToInt32(checkResult.dtEmployee.Rows[0]["EmployeeID"]);
                                        if (originalRoadRefund.refundAmount > 0.009)
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
                                            originalRoadRefund.id =
                                                preorder19dianMan.InsertOriginalRoadRefund(originalRoadRefund);
                                            if (originalRoadRefund.id > 0)
                                            {
                                                //记录原路退款记录
                                                flagOriginalRefundRecord = true;
                                                originalRoadRefund.tradeNo = thirdPartyPaymentInfo.tradeNo;
                                                //这里加入退款流程
                                                ThreadPool.QueueUserWorkItem(
                                                    new RefundCallBack(preOrder19dianId, thirdPartyPaymentInfo,
                                                        (float)originalRoadRefund.refundAmount, originalRoadRefund)
                                                        .Refund);
                                            }
                                            else
                                            {
                                                flagOriginalRefundRecord = false;
                                            }
                                        }
                                        else
                                        {
                                            flagOriginalRefundRecord = true;
                                        }
                                        //公司
                                        //MoneyViewallocAccountDetail moneyViewallocAccountDetailModel = new MoneyViewallocAccountDetail
                                        //{
                                        //    accountMoney = -1 * (refundsum),
                                        //    remainMoney =
                                        //        Money19dianDetailManager.GetViewAllocRemainMoney() + (-1 * (refundsum)),
                                        //    //获得公司当前时间账户余额
                                        //    accountType = (int)AccountType.USER_CANCEL_ORDER, //用户取消订单
                                        //    accountTypeConnId = Common.ToString(preOrder19dianId),
                                        //    inoutcomeType = (int)InoutcomeType.OUT, //相对于公司退款就是支出
                                        //    operUser = "用户customerID：" + Common.ToString(customerId),
                                        //    operTime = DateTime.Now,
                                        //    outcomeCompanyId = companyId,
                                        //    outcomeShopId = shopId,
                                        //    remark = "悠先服务原路退款"
                                        //};
                                        //viewallocBalance = SybMoneyViewallocOperate.ViewAllocAccountBalanceChanges(moneyViewallocAccountDetailModel);
                                        refundData = Common.InsertRefundData(customerId, refundsum, preOrder19dianId, "悠先服务原路退款"); //记录退款记录
                                        double preOrderTotalAmount =
                                            Common.ToDouble(dtCustomer.Rows[0]["preOrderTotalAmount"]);
                                        int preOrderTotalQuantity =
                                            Common.ToInt32(dtCustomer.Rows[0]["preOrderTotalQuantity"]);
                                        int currentPlatformVipGrade =
                                            Common.ToInt32(dtCustomer.Rows[0]["currentPlatformVipGrade"]);
                                        double cumulativeAmount = preOrderTotalAmount - refundsum;
                                        if ((canRefundSum - refundsum) < 0.001)
                                        {
                                            modifyVipInfo = Common.ModifyUserPlatVip(customerId, preOrderTotalQuantity,
                                                currentPlatformVipGrade, -refundsum, cumulativeAmount, true);
                                        }
                                        else
                                        {
                                            modifyVipInfo = Common.ModifyUserPlatVip(customerId, preOrderTotalQuantity,
                                                currentPlatformVipGrade, -refundsum, cumulativeAmount, false);
                                        }
                                        //悠先服务，更新当前服务员积分信息
                                        //EmployeePointOperate pointOperate = new EmployeePointOperate();
                                        //opintOperateFlag = pointOperate.RefundOpdatePoint(dtPreOrderInfo,
                                        //    Common.ToInt32(checkResult.dtEmployee.Rows[0]["EmployeeID"]), refundsum);
                                    }

                                    //退款要更新支付明细表的退款金额及状态，Add at 2015-4-15
                                    Preorder19DianLineManager lineManager = new Preorder19DianLineManager();
                                    Model.QueryObject.Preorder19DianLineQueryObject queryObject = new Model.QueryObject.Preorder19DianLineQueryObject()
                                    {
                                        Preorder19DianId = preOrder19dianId
                                    };
                                    List<IPreorder19DianLine> orderLineList = lineManager.GetListByQuery(queryObject);
                                    bool updateLine = false;
                                    foreach (IPreorder19DianLine line in orderLineList)
                                    {
                                        switch (line.PayType)
                                        {
                                            case (int)VAOrderUsedPayMode.ALIPAY:
                                            case (int)VAOrderUsedPayMode.WECHAT:
                                            case (int)VAOrderUsedPayMode.UNIONPAY:
                                                line.RefundAmount = line.RefundAmount + originalRoadRefund.refundAmount;//已经退的钱+本次退的第三方金额
                                                break;
                                            case (int)VAOrderUsedPayMode.BALANCE:
                                                line.RefundAmount = line.RefundAmount + refundFoodCoupon;//已经退的钱+本次退到粮票的钱
                                                break;
                                            case (int)VAOrderUsedPayMode.COUPON:
                                                line.RefundAmount = 0;//商户退款，抵扣券不返还
                                                break;
                                            case (int)VAOrderUsedPayMode.REDENVELOPE:
                                                line.RefundAmount = line.RefundAmount + refundRedEnvelope;//已经退的钱+本次退到红包的钱
                                                break;
                                        }
                                        updateLine = lineManager.Update(line);//更新每种支付类型的退款金额及状态
                                    }


                                    //----------------------------------------------------------------------------------

                                    bool cancelAwardRedEnvelope = false;

                                    //本次退款是最后一次退款
                                    if (refundsum + refundMoneySum - prepaidSum > -0.001)
                                    {
                                        //全额退款时，中奖赠送的红包作废
                                        AwardConnPreOrderOperate awardOperate = new AwardConnPreOrderOperate();
                                        cancelAwardRedEnvelope = awardOperate.CancelAwardRedEnvelope(preOrder19dianId);
                                    }
                                    else
                                    {
                                        cancelAwardRedEnvelope = true;
                                    }

                                    //----------------------------------------------------------------------------------



                                    if (updatePreorder && flagOriginalRefundRecord && refundData && modifyVipInfo && flagUpdatePreOrderRefundMoneyClosedSum && cancelAwardRedEnvelope)//&& viewallocBalance&& opintOperateFlag
                                    {
                                        ShopManager shopMan = new ShopManager();
                                        DataTable dtShop = shopMan.SelectShop(shopId);
                                        scope.Complete();
                                        preorderRefundResponse.result = VAResult.VA_OK;
                                        if (customerMobilephone.Length == 11)
                                        {
                                            string shopName = "";
                                            if (dtShop.Rows.Count == 1)
                                            {
                                                shopName = Common.ToString(dtShop.Rows[0]["shopName"]);
                                            }
                                            string smsContent =
                                                ConfigurationManager.AppSettings["sybRefundMessage"].Trim();
                                            smsContent = smsContent.Replace("{0}", shopName);
                                            smsContent = smsContent.Replace("{1}", Common.ToString(refundsum));
                                            Common.SendMessageBySms(customerMobilephone, smsContent);

                                            CustomPushRecordOperate customPushRecordOperate = new CustomPushRecordOperate();
                                            UniPushInfo uniPushInfo = new UniPushInfo()
                                            {
                                                customerPhone = customerMobilephone,
                                                preOrder19dianId = preOrder19dianId,
                                                shopName = shopName,
                                                pushMessage = VAPushMessage.退款成功,
                                                clientBuild = dtPreOrderInfo.Rows[0]["appBuild"].ToString()
                                            };
                                            Thread refundThread = new Thread(customPushRecordOperate.UniPush);
                                            refundThread.Start(uniPushInfo);
                                        }
                                    }
                                    else
                                    {
                                        preorderRefundResponse.result = VAResult.VA_PREORDER_REFUND_ERROR; //退款失败
                                    }
                                }
                                else
                                {
                                    preorderRefundResponse.result = VAResult.VA_REFUND_NO_MONEY; //退款失败
                                }
                            }
                            else
                            {
                                preorderRefundResponse.result = VAResult.VA_REFUND_NOTPAYORAPPROVED; //已对账或是未付款
                            }
                        }
                    }
                    else
                    {
                        preorderRefundResponse.result = VAResult.VA_FAILED_PREORDER_NOT_FOUND; //当前点单未找到
                    }
                }
                else
                {
                    preorderRefundResponse.result = VAResult.VA_NO_ACCESS_INTERFACE_AUTHORITY; //没有权限
                }
            }
            else
            {
                preorderRefundResponse.result = checkResult.result;
            }
            return preorderRefundResponse;
        }
        /// <summary>
        /// 掌中宝客户端审核点单
        /// </summary>
        /// <param name="zzb_vaPreOrderConfrimRequest"></param>
        /// <returns></returns>
        public ZZB_VAPreOrderConfrimResponse ZZBClientPreOrderConfrim(ZZB_VAPreOrderConfrimRequest zzb_vaPreOrderConfrimRequest)
        {
            ZZB_VAPreOrderConfrimResponse zzb_vaPreOrderConfrimResponse = new ZZB_VAPreOrderConfrimResponse();
            zzb_vaPreOrderConfrimResponse.type = VAMessageType.ZZB_CLIENT_PREORDERCONFRIM_RESPONSE;
            zzb_vaPreOrderConfrimResponse.cookie = zzb_vaPreOrderConfrimRequest.cookie;
            zzb_vaPreOrderConfrimResponse.uuid = zzb_vaPreOrderConfrimRequest.uuid;
            CheckCookieAndMsgForZZZ checkResult = Common.CheckCookieAndMsgForZZZ(zzb_vaPreOrderConfrimRequest.cookie, (int)zzb_vaPreOrderConfrimRequest.type, (int)VAMessageType.ZZB_CLIENT_PREORDERCONFRIM_REQUEST, zzb_vaPreOrderConfrimRequest.shopId);
            if (checkResult.result == VAResult.VA_OK)
            {
                SybMoneyMerchantOperate syb = new SybMoneyMerchantOperate();
                int returnResult = syb.ConfrimPreOrder(zzb_vaPreOrderConfrimRequest.preOrder19dianId, 1, PreOrderConfirmOperater.Service, zzb_vaPreOrderConfrimRequest.employeeId);
                switch (returnResult)
                {
                    case -3:
                        zzb_vaPreOrderConfrimResponse.result = VAResult.VA_FAILED_CONFRIEM_ERROR_ORDER_ISCONFRIEM;//当前单子是已审核状态，无法审核
                        break;
                    case 0:
                        zzb_vaPreOrderConfrimResponse.result = VAResult.VA_FAILED_CONFRIEM_ERROR_ORDER_NOTFOUND;//未找到该订单
                        break;
                    case -8:
                        zzb_vaPreOrderConfrimResponse.result = VAResult.VA_FAILED_CONFRIEM_ERROR_ORDER_HAVE_REFUND;//入座失败，点单已退款或已申请原路打款
                        break;
                    case 1:
                        zzb_vaPreOrderConfrimResponse.result = VAResult.VA_OK;//入座成功
                        break;
                    default:
                        zzb_vaPreOrderConfrimResponse.result = VAResult.VA_FAILED_CONFRIEM;//入座失败
                        break;
                }
            }
            else
            {
                zzb_vaPreOrderConfrimResponse.result = checkResult.result;
            }
            return zzb_vaPreOrderConfrimResponse;
        }
        /// <summary>
        /// 掌中宝客户端手机认证
        /// 20140208 xiaoyu
        /// </summary>
        /// <param name="userRegisterRequest"></param>
        /// <returns></returns>
        public ZZB_VAUserRegisterResponse ZZBClientMobileVerify(ZZB_VAUserRegisterRequest userRegisterRequest)
        {
            ZZB_VAUserRegisterResponse userRegisterResponse = new ZZB_VAUserRegisterResponse();
            userRegisterResponse.type = VAMessageType.ZZB_CLIENT_USER_MOBILE_REGISTER_RESPONSE;
            List<VAEmployeeShop> employeeShop = new List<VAEmployeeShop>();
            userRegisterResponse.employeeShop = employeeShop;
            if (userRegisterRequest.type == VAMessageType.ZZB_CLIENT_USER_MOBILE_REGISTER_REQUEST)
            {
                if (!string.IsNullOrEmpty(userRegisterRequest.mobilePhoneNumber))
                {
                    AuthorityManager authorityMan = new AuthorityManager();
                    DataTable dtEmployee = authorityMan.SelectEmployeeByMobilephone(userRegisterRequest.mobilePhoneNumber);
                    if (dtEmployee.Rows.Count == 1)
                    {//号码已注册
                        DateTime verificationCodeTime = Common.ToDateTime(dtEmployee.Rows[0]["verificationCodeTime"]);
                        string verificationCode = Common.ToString(dtEmployee.Rows[0]["verificationCode"]);
                        if (string.IsNullOrEmpty(userRegisterRequest.verificationCode))
                        {//验证码为空则为发送验证码请求
                            if (userRegisterRequest.mobilePhoneNumber == "12345678901")
                            {//提供苹果审核时使用的测试帐号
                                userRegisterResponse.result = VAResult.VA_OK;
                                userRegisterResponse.cookie = userRegisterRequest.cookie;
                            }
                            else
                            {
                                // 验证码有效期内
                                if (((System.DateTime.Now - verificationCodeTime) < TimeSpan.FromSeconds(Common.smsValidTime)) && !string.IsNullOrEmpty(verificationCode))
                                {
                                    userRegisterResponse.result = SendVerificationCodeBySms(userRegisterRequest.mobilePhoneNumber, verificationCode);
                                }
                                else
                                {
                                    userRegisterResponse.result = SendVerificationCodeBySms(userRegisterRequest.mobilePhoneNumber, "");
                                }
                                if (userRegisterResponse.result != VAResult.VA_OK)
                                {
                                    userRegisterResponse.result = VAResult.VA_FAILED_SMS_NOT_SEND;
                                }
                                userRegisterResponse.cookie = userRegisterRequest.cookie;
                            }
                        }
                        else
                        {//验证码不为空则为手机登录请求
                            if (userRegisterRequest.mobilePhoneNumber == "12345678901")
                            {//提供苹果审核时使用的测试帐号
                                userRegisterResponse.employeeId = Common.ToInt32(dtEmployee.Rows[0]["EmployeeID"]);
                                userRegisterResponse.cookie = Common.ToString(dtEmployee.Rows[0]["cookie"]);
                                string employeeName = Common.ToString(dtEmployee.Rows[0]["EmployeeFirstName"]);
                                if (string.IsNullOrEmpty(employeeName))
                                {
                                    userRegisterResponse.isNewNumber = true;
                                }
                                EmployeeConnShopOperate employeeConnShopOpe = new EmployeeConnShopOperate();
                                userRegisterResponse.employeeShop = employeeConnShopOpe.QueryEmployeeShop(userRegisterResponse.employeeId, true);
                                userRegisterResponse.result = VAResult.VA_OK;
                            }
                            else
                            {
                                if ((System.DateTime.Now - verificationCodeTime) < TimeSpan.FromSeconds(Common.smsValidTime))
                                {
                                    if (string.Equals(userRegisterRequest.verificationCode, verificationCode))
                                    {
                                        userRegisterResponse.employeeId = Common.ToInt32(dtEmployee.Rows[0]["EmployeeID"]);
                                        userRegisterResponse.cookie = Common.ToString(dtEmployee.Rows[0]["cookie"]);
                                        string employeeName = Common.ToString(dtEmployee.Rows[0]["EmployeeFirstName"]);
                                        if (string.IsNullOrEmpty(employeeName))
                                        {
                                            userRegisterResponse.isNewNumber = true;
                                        }
                                        EmployeeConnShopOperate employeeConnShopOpe = new EmployeeConnShopOperate();
                                        userRegisterResponse.employeeShop = employeeConnShopOpe.QueryEmployeeShop(userRegisterResponse.employeeId, true);
                                        userRegisterResponse.result = VAResult.VA_OK;
                                    }
                                    else
                                    {
                                        userRegisterResponse.result = VAResult.VA_FAILED_VERIFICATIONCODE_WRONG;
                                    }
                                }
                                else
                                {
                                    userRegisterResponse.result = VAResult.VA_FAILED_VERIFICATIONCODE_OUTOFTIME;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(userRegisterRequest.verificationCode))
                        {//验证码为空则为发送验证码请求
                            EmployeeInfo employee = new EmployeeInfo();
                            employee.UserName = userRegisterRequest.mobilePhoneNumber;
                            employee.cookie = Common.ToString(Guid.NewGuid()) + Common.ToString(System.DateTime.Now.Ticks);
                            employee.EmployeeStatus = 1;
                            employee.EmployeePhone = userRegisterRequest.mobilePhoneNumber;
                            employee.EmployeeAge = 18;
                            employee.EmployeeFirstName = "";
                            //employee.EmployeeLastName = "";//2014-2-23 取消LastName
                            employee.EmployeeSequence = 10;
                            employee.EmployeeSex = 1;
                            Random randomNumber = new Random();
                            employee.Password = MD5Operate.getMd5Hash(Common.ToString(Common.randomStrAndNum(6)));
                            employee.position = "";
                            employee.defaultPage = "";
                            employee.isViewAllocWorker = false; //是否是友络工作人员
                            employee.registerTime = DateTime.Now;
                            employee.isSupportLoginBgSYS = false;//add by wangc 20140324，是否可进入后台
                            //if (authorityMan.InsertEmployee(employee) > 0)
                            //调用服务器内部存储过程，尽量避免重复注册
                            if (authorityMan.InsertEmployeeBySp(employee) == 1)
                            {
                                userRegisterResponse.result = SendVerificationCodeBySms(userRegisterRequest.mobilePhoneNumber, "");
                                userRegisterResponse.cookie = employee.cookie;
                            }
                            else
                            {
                                userRegisterResponse.result = VAResult.VA_FAILED_DB_ERROR;
                            }
                        }
                        else
                        {//未注册的手机号码不会出现验证验证码的过程
                            userRegisterResponse.result = VAResult.VA_FAILED_OTHER;
                        }
                    }
                }
                else
                {
                    userRegisterResponse.result = VAResult.VA_FAILED_PHONENUMBER_NULL;
                }
            }
            else
            {
                userRegisterResponse.result = VAResult.VA_FAILED_TYPE_ERROR;
            }
            return userRegisterResponse;
        }
        /// <summary>
        /// 悠先服务客户端用户信息修改
        /// 20140208 xiaoyu
        /// </summary>
        /// <param name="modifyUserInfoRequest"></param>
        /// <returns></returns>
        public ZZB_VAModifyUserInfoResponse ZZBClientModifyUserInfo(ZZB_VAModifyUserInfoRequest modifyUserInfoRequest)
        {
            ZZB_VAModifyUserInfoResponse modifyUserInfoResponse = new ZZB_VAModifyUserInfoResponse();
            modifyUserInfoResponse.type = VAMessageType.ZZB_CLIENT_MODIFY_USERINFO_RESPONSE;
            modifyUserInfoResponse.cookie = modifyUserInfoRequest.cookie;
            CheckCookieAndMsgForZZZ checkResult = Common.CheckCookieAndMsgForZZZ(modifyUserInfoRequest.cookie,
                (int)modifyUserInfoRequest.type, (int)VAMessageType.ZZB_CLIENT_MODIFY_USERINFO_REQUEST);
            if (checkResult.result == VAResult.VA_OK)
            {
                AuthorityManager authorityMan = new AuthorityManager();
                if (authorityMan.UpdateEmployeeNameAndNumber(modifyUserInfoRequest.name, modifyUserInfoRequest.employeeNumber, modifyUserInfoRequest.cookie))
                {
                    modifyUserInfoResponse.result = VAResult.VA_OK;
                }
                else
                {
                    modifyUserInfoResponse.result = VAResult.VA_FAILED_DB_ERROR;
                }
            }
            else
            {
                modifyUserInfoResponse.result = checkResult.result;
            }
            return modifyUserInfoResponse;
        }
        /// <summary>
        /// 悠先服务查询门店列表
        /// </summary>
        /// <param name="queryShopListRequest"></param>
        /// <returns></returns>
        public ZZB_VAQueryShopListResponse ZZBQueryShopList(ZZB_VAQueryShopListRequest queryShopListRequest)
        {
            ZZB_VAQueryShopListResponse queryShopListResponse = new ZZB_VAQueryShopListResponse();
            queryShopListResponse.type = VAMessageType.ZZB_CLIENT_QUERY_SHOPLIST_RESPONSE;
            queryShopListResponse.cookie = queryShopListRequest.cookie;
            queryShopListResponse.employeeShop = new List<VAEmployeeShop>();
            CheckCookieAndMsgForZZZ checkResult = Common.CheckCookieAndMsgForZZZ(queryShopListRequest.cookie, (int)queryShopListRequest.type, (int)VAMessageType.ZZB_CLIENT_QUERY_SHOPLIST_REQUEST);
            if (checkResult.result == VAResult.VA_OK)
            {
                queryShopListResponse.result = VAResult.VA_OK;
                int employeeId = Common.ToInt32(checkResult.dtEmployee.Rows[0]["EmployeeID"]);
                DataTable dtQueryOnlineCompamnyInfo = new RoleOperate().QuerySpecialAuthorityInfoByEmployeeID(employeeId, (int)VASpecialAuthority.CHECK_PREORDER_ONLINE_COMPANY);
                EmployeeConnShopOperate employeeConnShopOpe = new EmployeeConnShopOperate();
                if (dtQueryOnlineCompamnyInfo.Rows.Count > 0)
                {
                    queryShopListResponse.employeeShop = GetHandleShop();
                }
                else
                {
                    queryShopListResponse.employeeShop = GetEmployeeHandleShop(employeeId);
                }
            }
            else
            {
                queryShopListResponse.result = checkResult.result;
            }
            SystemConfigCacheLogic systemConfigCacheLogic = new SystemConfigCacheLogic();
            queryShopListResponse.servicePhone = systemConfigCacheLogic.GetVAServicePhone();
            return queryShopListResponse;
        }
        /// <summary>
        /// 发送验证码
        /// <para>bruke 修改</para>
        /// <para>用增语音验证码</para>
        /// </summary>
        /// <param name="mobilePhoneNumber"></param>
        /// <param name="verificationCode"></param>
        /// <returns></returns>
        public VAResult SendVerificationCodeBySms(string mobilePhoneNumber, string verificationCode, bool voice = false)
        {
            AuthorityManager authorityMan = new AuthorityManager();
            Random randomNumber = new Random();
            bool updateVerificationCodeTime = false;
            if (string.IsNullOrEmpty(verificationCode))
            {
                verificationCode = Common.ToString(randomNumber.Next(10000, 99999));
                updateVerificationCodeTime = true;
            }
            bool updateVerificationCodeSucess = authorityMan.UpdateEmployeeVerificationCodeByMobilephoneNumber(mobilePhoneNumber, verificationCode, updateVerificationCodeTime, voice);

            if (updateVerificationCodeSucess)
            {
                string smsContent = ConfigurationManager.AppSettings["SmsContent"].Trim();
                smsContent = smsContent.Replace("{0}", verificationCode);
                if (voice)
                {
                    string[] mobiles = new string[1];
                    mobiles[0] = mobilePhoneNumber;
                    if (Common.SendVoiceMessage(mobiles, verificationCode))
                    {
                        return VAResult.VA_OK;
                    }
                    else
                    {
                        return VAResult.VA_FAILED_SMS_NOT_SEND;
                    }
                }
                else
                {
                    if (Common.SendMessageBySms(mobilePhoneNumber, smsContent))
                    {
                        return VAResult.VA_OK;
                    }
                    else
                    {
                        return VAResult.VA_FAILED_SMS_NOT_SEND;
                    }
                }
            }
            else
            {
                return VAResult.VA_FAILED_DB_ERROR;
            }
        }

        /// <summary>
        /// 悠先服务查询版本更新信息
        /// </summary>
        /// <param name="zzb_vaPreOrderConfrimRequest"></param>
        /// <returns></returns>
        public ZZB_VAQueryUpdateInfoResponse QueryUpdateInfoResponse(ZZB_VAQueryUpdateInfoResqeust queryUpdateInfoResqeust)
        {
            ZZB_VAQueryUpdateInfoResponse queryUpdateInfoResponse = new ZZB_VAQueryUpdateInfoResponse();
            queryUpdateInfoResponse.type = VAMessageType.ZZB_CLIENT_QUERY_UPDATEINFO_RESPONSE;
            queryUpdateInfoResponse.cookie = queryUpdateInfoResqeust.cookie;
            queryUpdateInfoResponse.uuid = queryUpdateInfoResqeust.uuid;
            CheckCookieAndMsgForZZZ checkResult = Common.CheckCookieAndMsgForZZZ(queryUpdateInfoResqeust.cookie, (int)queryUpdateInfoResqeust.type, (int)VAMessageType.ZZB_CLIENT_QUERY_UPDATEINFO_REQUEST);
            if (checkResult.result == VAResult.VA_OK)
            {
                SystemConfigCacheLogic systemConfigCacheLogic = new SystemConfigCacheLogic();
                DataTable latestBuildInfo = systemConfigCacheLogic.GetUxianServiceLatestBuild(queryUpdateInfoResqeust.serviceType);
                if (latestBuildInfo.Rows.Count == 1)
                {
                    queryUpdateInfoResponse.result = VAResult.VA_OK;
                    queryUpdateInfoResponse.latestBuild = Common.ToString(latestBuildInfo.Rows[0]["latestBuild"]);
                    queryUpdateInfoResponse.latestUpdateDescription = Common.ToString(latestBuildInfo.Rows[0]["latestUpdateDescription"]);
                    queryUpdateInfoResponse.latestUpdateUrl = Common.ToString(latestBuildInfo.Rows[0]["latestUpdateUrl"]);
                    string oldBuildSupport = Common.ToString(latestBuildInfo.Rows[0]["oldBuildSupport"]);
                    string clientBuild = "";
                    if (!string.IsNullOrEmpty(queryUpdateInfoResqeust.clientBuild))
                    {
                        clientBuild = queryUpdateInfoResqeust.clientBuild;
                    }
                    if (string.Compare(clientBuild, oldBuildSupport) == -1)
                    {
                        queryUpdateInfoResponse.forceUpdate = true;
                    }
                    else
                    {
                        queryUpdateInfoResponse.forceUpdate = false;
                    }
                }
                else
                {
                    queryUpdateInfoResponse.latestBuild = "";
                    queryUpdateInfoResponse.latestUpdateDescription = "";
                    queryUpdateInfoResponse.latestUpdateUrl = "";
                    queryUpdateInfoResponse.forceUpdate = false;
                }
            }
            else
            {
                queryUpdateInfoResponse.result = checkResult.result;
            }
            return queryUpdateInfoResponse;
        }

        /// <summary>
        /// 从Cache中读取所有已上线门店
        /// </summary>
        /// <returns></returns>
        private List<VAEmployeeShop> GetHandleShop()
        {
            List<VAEmployeeShop> HandleShopCache = MemcachedHelper.GetMemcached<List<VAEmployeeShop>>("handleShop");
            if (HandleShopCache == null)
            {
                EmployeeConnShopOperate employeeConnShopOpe = new EmployeeConnShopOperate();
                HandleShopCache = employeeConnShopOpe.QueryHandleShop();
                if (HandleShopCache != null)
                {
                    MemcachedHelper.AddMemcached("handleShop", HandleShopCache, 30);//30秒
                }
            }
            return HandleShopCache;
        }
        /// <summary>
        /// 从Cache中读取服务员有权限的所有已上线门店
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        private List<VAEmployeeShop> GetEmployeeHandleShop(int employeeId)
        {
            List<VAEmployeeShop> employeeHandleShopCache = MemcachedHelper.GetMemcached<List<VAEmployeeShop>>("employeeHandleShop_" + employeeId);
            if (employeeHandleShopCache == null)
            {
                EmployeeConnShopOperate employeeConnShopOpe = new EmployeeConnShopOperate();
                employeeHandleShopCache = employeeConnShopOpe.QueryEmployeeShop(employeeId);
                if (employeeHandleShopCache != null)
                {
                    MemcachedHelper.AddMemcached("employeeHandleShop_" + employeeId, employeeHandleShopCache, 30);//30秒
                }
            }
            return employeeHandleShopCache;
        }

        /// <summary>
        /// 悠先服务客户端发送错误日志
        /// 2014-04-18 xiaoyu
        /// </summary>
        /// <param name="sendErrorMessageRequest"></param>
        /// <returns></returns>
        public ZZB_SendErrorMessageResponse ZZBSendErrorMessage(ZZB_SendErrorMessageRequest sendErrorMessageRequest)
        {
            ZZB_SendErrorMessageResponse sendErrorMessageResponse = new ZZB_SendErrorMessageResponse();
            sendErrorMessageResponse.type = VAMessageType.ZZB_CLIENT_SEND_ERRORMESSAGE_RESPONSE;
            sendErrorMessageResponse.cookie = sendErrorMessageRequest.cookie;
            sendErrorMessageResponse.uuid = sendErrorMessageRequest.uuid;
            CheckCookieAndMsgForZZZ checkResult = Common.CheckCookieAndMsgForZZZ(sendErrorMessageRequest.cookie, (int)sendErrorMessageRequest.type, (int)VAMessageType.ZZB_CLIENT_SEND_ERRORMESSAGE_REQUEST);
            if (checkResult.result == VAResult.VA_OK)
            {
                ClientErrorManager clientErrorMan = new ClientErrorManager();
                ClientErrorInfo clientErrorInfo = new ClientErrorInfo();
                clientErrorInfo.appType = sendErrorMessageRequest.appType;
                clientErrorInfo.errorMessage = sendErrorMessageRequest.errorInfo;
                clientErrorInfo.time = DateTime.Now;
                clientErrorInfo.clientBuild = sendErrorMessageRequest.clientBuild;
                clientErrorInfo.clientType = VAClientType.UXIANSERVICE;
                using (TransactionScope scope = new TransactionScope())
                {
                    if (clientErrorMan.InsertClientError(clientErrorInfo) > 0)
                    {
                        scope.Complete();
                        sendErrorMessageResponse.result = VAResult.VA_OK;
                    }
                    else
                    {
                        sendErrorMessageResponse.result = VAResult.VA_FAILED_DB_ERROR;
                    }
                }
            }
            else
            {
                sendErrorMessageResponse.result = checkResult.result;
            }
            if (sendErrorMessageResponse.result == VAResult.VA_OK)
            {
                VAEmailInfo emailInfo = new VAEmailInfo();
                emailInfo.subject = "悠先服务客户端出错日志";
                emailInfo.messageBody = sendErrorMessageRequest.errorInfo + "----------------------------------悠先服务，当前版本号：" + sendErrorMessageRequest.clientBuild;
                emailInfo.emailAddressTo = ConfigurationManager.AppSettings["ClientErrorEmailAddressTo"].Trim();
                Thread emailThread = new Thread(Common.SendNEmailFrom19dianService);
                emailThread.Start((object)emailInfo);
            }
            return sendErrorMessageResponse;
        }

        /// <summary>
        /// <para>悠先服务客户端发送语音短信</para>
        /// <para>bruke @ 20140421</para>
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ZZB_VAClientSendVoiceMessageResponse ClientSendVoiceMessage(ZZB_VAClientSendVoiceMessageRequest request)
        {
            ZZB_VAClientSendVoiceMessageResponse response = new ZZB_VAClientSendVoiceMessageResponse
            {
                type = VAMessageType.ZZB_CLIENT_SEND_VOICEMESSAGE_RESPONSE,
                cookie = request.cookie,
                uuid = request.uuid
            };
            //CheckCookieAndMsgForZZZ checkResult = Common.CheckCookieAndMsgForZZZ(request.cookie, (int)request.type, (int)VAMessageType.ZZB_CLIENT_SEND_VOICEMESSAGE_REQUEST);
            //if (checkResult.result == VAResult.VA_OK)
            //{
            if (request.type == VAMessageType.ZZB_CLIENT_SEND_VOICEMESSAGE_REQUEST)
            {
                if (!string.IsNullOrEmpty(request.mobilePhone))
                {
                    //手机号码不为空
                    AuthorityManager authorityMan = new AuthorityManager();
                    DataTable dtEmployee = authorityMan.SelectEmployeeByMobilephone(request.mobilePhone);
                    if (dtEmployee.Rows.Count == 1)
                    {
                        //找到员工
                        DateTime verificationCodeTime = Common.ToDateTime(dtEmployee.Rows[0]["verificationCodeTime"]);
                        string verificationCode = Common.ToString(dtEmployee.Rows[0]["verificationCode"]);
                        bool currentVCSentByVoice = Common.ToBool(dtEmployee.Rows[0]["isVCSendByVoice"]);
                        if (currentVCSentByVoice && (System.DateTime.Now - verificationCodeTime) < TimeSpan.FromSeconds(600))
                        {
                            response.result = VAResult.VA_FAILED_SMS_VOICE_TOO_OFTEN;
                        }
                        else
                        {
                            if (((System.DateTime.Now - verificationCodeTime) < TimeSpan.FromSeconds(Common.smsValidTime)) && !string.IsNullOrEmpty(verificationCode))
                            {
                                response.result = SendVerificationCodeBySms(request.mobilePhone, verificationCode, true);
                            }
                            else
                            {
                                response.result = SendVerificationCodeBySms(request.mobilePhone, "", true);
                            }
                        }
                    }
                    else
                    {
                        response.result = VAResult.VA_FAILED_OTHER;
                    }
                }
                else
                {
                    response.result = VAResult.VA_FAILED_OTHER;
                }
            }
            else
            {
                response.result = VAResult.VA_FAILED_TYPE_ERROR;
            }
            //}
            //else
            //{
            //    response.result = checkResult.result;
            //}
            return response;
        }

        /// <summary>
        /// 获取员工菜品管理权限
        /// </summary>
        /// <param name="dishManageRoleRequest"></param>
        /// <returns></returns>
        public ZZB_DishManageRoleResponse ZZBDishManageRole(ZZB_DishManageRoleRequest dishManageRoleRequest)
        {
            ZZB_DishManageRoleResponse response = new ZZB_DishManageRoleResponse
            {
                type = VAMessageType.ZZB_CLIENT_DISH_MANAGE_ROLE_RESPONSE,
                cookie = dishManageRoleRequest.cookie,
                uuid = dishManageRoleRequest.uuid
            };

            //[isViewAllocWorker]
            CheckCookieAndMsgForZZZ checkResult = Common.CheckCookieAndMsgForZZZ(dishManageRoleRequest.cookie, (int)dishManageRoleRequest.type, (int)VAMessageType.ZZB_CLIENT_DISH_MANAGE_ROLE_REQUEST, dishManageRoleRequest.shopId);
            if (checkResult.result == VAResult.VA_OK)
            {
                int employeeId = Common.ToInt32(checkResult.dtEmployee.Rows[0]["EmployeeID"]);
                bool isViewAllocWorker = Common.ToBool(checkResult.dtEmployee.Rows[0]["isViewAllocWorker"]);

                try
                {
                    var shopAuthorityService = ServiceFactory.Resolve<IShopAuthorityService>();
                    response.roles =
                        (from a in
                             shopAuthorityService.GetShopAuthorities(dishManageRoleRequest.shopId, employeeId,
                                 isViewAllocWorker)
                         orderby a.ShopAuthoritySequence
                         select
                             new ShopHaveAuthority
                             {
                                 authorityCode = a.AuthorityCode,
                                 authorityName = a.ShopAuthorityName,
                                 isClientShow = a.IsClientShow
                             }).ToList();
                    response.result = VAResult.VA_OK;
                }
                catch (Exception)
                {
                    response.result = VAResult.VA_FAILED_DB_ERROR;
                }

            }
            else
            {
                response.result = checkResult.result;
            }
            return response;
        }

        /// <summary>
        /// 获取员工菜品管理权限
        /// </summary>
        /// <param name="dishManageRoleRequest"></param>
        /// <returns></returns>
        public ZZB_DishManageRoleResponseOld ZZBDishManageRoleOld(ZZB_DishManageRoleRequest dishManageRoleRequest)
        {
            ZZB_DishManageRoleResponseOld response = new ZZB_DishManageRoleResponseOld
            {
                type = VAMessageType.ZZB_CLIENT_DISH_MANAGE_ROLE_RESPONSE,
                cookie = dishManageRoleRequest.cookie,
                uuid = dishManageRoleRequest.uuid
            };

            //[isViewAllocWorker]
            CheckCookieAndMsgForZZZ checkResult = Common.CheckCookieAndMsgForZZZ(dishManageRoleRequest.cookie, (int)dishManageRoleRequest.type, (int)VAMessageType.ZZB_CLIENT_DISH_MANAGE_ROLE_REQUEST, dishManageRoleRequest.shopId);
            if (checkResult.result == VAResult.VA_OK)
            {
                //try
                //{
                //    var shopAuthorityService = ServiceFactory.Resolve<IShopAuthorityService>();
                //    response.roles = shopAuthorityService.QueryUxianServiceAuthorityOld();                 
                //    response.result = VAResult.VA_OK;
                //}
                //catch (Exception)
                //{
                //    response.result = VAResult.VA_FAILED_DB_ERROR;
                //}

                int employeeId = Common.ToInt32(checkResult.dtEmployee.Rows[0]["EmployeeID"]);
                bool isViewAllocWorker = Common.ToBool(checkResult.dtEmployee.Rows[0]["isViewAllocWorker"]);

                try
                {
                    /*
                     new ShopHaveAuthority
                             {
                                 authorityCode = a.AuthorityCode,
                                 authorityName = a.ShopAuthorityName,
                                 isClientShow = a.IsClientShow
                             }
                     */
                    var shopAuthorityService = ServiceFactory.Resolve<IShopAuthorityService>();
                    response.roles =
                        (from a in
                             shopAuthorityService.GetShopAuthorities(dishManageRoleRequest.shopId, employeeId,
                                 isViewAllocWorker)
                         where a.AuthorityCode == "1"
                         || a.AuthorityCode == "2"
                         orderby a.ShopAuthoritySequence
                         select a.ShopAuthorityName
                             ).ToArray();
                    response.result = VAResult.VA_OK;
                }
                catch (Exception)
                {
                    response.result = VAResult.VA_FAILED_DB_ERROR;
                }
            }
            else
            {
                response.result = checkResult.result;
            }
            return response;
        }

        /// <summary>
        /// 菜品搜索
        /// </summary>
        /// <param name="dishSearchRequest"></param>
        /// <returns></returns>
        public ZZB_DishSearchResponse DishSearch(ZZB_DishSearchRequest dishSearchRequest)
        {
            ZZB_DishSearchResponse response = new ZZB_DishSearchResponse
            {
                type = (dishSearchRequest is ZZB_DishAllSearchRequest) ? VAMessageType.ZZB_CLIENT_DISH_ALL_SEARCH_RESPONSE : VAMessageType.ZZB_CLIENT_DISH_SEARCH_RESPONSE,
                cookie = dishSearchRequest.cookie,
                uuid = dishSearchRequest.uuid
            };
            string[] roles = new[] { ShopRole.调整价格.GetString(), ShopRole.菜品沽清.GetString() };
            CheckCookieAndMsgForZZZ checkResult = Common.CheckCookieAndMsgForZZZ(dishSearchRequest.cookie, (int)dishSearchRequest.type, (int)((dishSearchRequest is ZZB_DishAllSearchRequest) ? VAMessageType.ZZB_CLIENT_DISH_ALL_SEARCH_REQUEST : VAMessageType.ZZB_CLIENT_DISH_SEARCH_REQUEST), dishSearchRequest.shopId);
            if (checkResult.result == VAResult.VA_OK)
            {
                int employeeId = Common.ToInt32(checkResult.dtEmployee.Rows[0]["EmployeeID"]);
                bool isViewAllocWorker = Common.ToBool(checkResult.dtEmployee.Rows[0]["isViewAllocWorker"]);
                //AuthorityManager authorityMan = new AuthorityManager();
                if (ServiceFactory.Resolve<IShopAuthorityService>().GetEmployeeHasShopAuthority(dishSearchRequest.shopId, employeeId, isViewAllocWorker, roles))
                {
                    //dishSearchRequest.
                    DishManager dishManager = new DishManager();

                    int state = 0;
                    if (dishSearchRequest is ZZB_DishAllSearchRequest)
                    {
                        state = ((ZZB_DishAllSearchRequest)dishSearchRequest).state;
                    }
                    else
                    {
                        state = dishSearchRequest.sellOff ? 1 : 0;
                    }
                    IDishInfoRepository dishInfoRepository = ServiceFactory.Resolve<IDishInfoRepository>();
                    IPagedList<DishDetails> list = null;
                    if (state > 0)
                    {
                        if (
                            ServiceFactory.Resolve<IShopAuthorityService>()
                                .GetEmployeeHasShopAuthority(dishSearchRequest.shopId, employeeId, isViewAllocWorker,
                                    ShopRole.菜品沽清.GetString()))
                        {
                            //var list = dishManager.GetSellOffDishDetailsesForPage(dishSearchRequest.shopId,
                            //    dishSearchRequest.pageIndex, dishSearchRequest.pageSize);
                            if (state == 1)
                            {
                                list = dishInfoRepository.GetPageShopSellOffDishDetailses(
                                    new Page(dishSearchRequest.pageIndex, dishSearchRequest.pageSize),
                                    dishSearchRequest.shopId);
                            }
                            else
                            {
                                list = dishInfoRepository.GetPageShopSellOnDishDetailses(
                                    new Page(dishSearchRequest.pageIndex, dishSearchRequest.pageSize),
                                    dishSearchRequest.shopId);
                            }
                            response.dishs = list.Select(p => new ZZB_DishDetails
                            {
                                companyId = p.CompanyId,
                                shopId = p.ShopId,
                                menuId = p.MenuId,
                                dishId = p.DishId,
                                dishPriceId = p.DishPriceId,
                                price = p.DishPrice,
                                scaleName = p.ScaleName,
                                sellOff = p.SellOff,
                                dishName = p.DishName
                            }).ToList();
                            response.hasNextPage = list.HasNextPage;
                            response.result = VAResult.VA_OK;
                        }
                        else
                        {
                            response.result = VAResult.VA_NO_ACCESS_INTERFACE_AUTHORITY;
                        }
                    }
                    else
                    {
                        list = dishInfoRepository.GetPageShopAllDishDetailses(
                                    new Page(dishSearchRequest.pageIndex, dishSearchRequest.pageSize),
                                    dishSearchRequest.shopId, dishSearchRequest.key);
                        response.dishs = list.Select(p => new ZZB_DishDetails
                        {
                            companyId = p.CompanyId,
                            shopId = p.ShopId,
                            menuId = p.MenuId,
                            dishId = p.DishId,
                            dishPriceId = p.DishPriceId,
                            price = p.DishPrice,
                            scaleName = p.ScaleName,
                            sellOff = p.SellOff,
                            dishName = p.DishName
                        }).ToList();
                        response.hasNextPage = list.HasNextPage;
                        response.result = VAResult.VA_OK;
                    }
                }
                else
                {
                    response.result = VAResult.VA_NO_ACCESS_INTERFACE_AUTHORITY;
                }

            }
            else
            {
                response.result = checkResult.result;
            }
            return response;
        }

        /// <summary>
        /// 沽清/取消沽清
        /// </summary>
        /// <param name="dishSellOffRequest"></param>
        /// <returns></returns>
        public ZZB_DishSellOffResponse SellOff(ZZB_DishSellOffRequest dishSellOffRequest)
        {
            ZZB_DishSellOffResponse response = new ZZB_DishSellOffResponse
            {
                type = VAMessageType.ZZB_CLIENT_DISH_SELLOFF_RESPONSE,
                cookie = dishSellOffRequest.cookie,
                uuid = dishSellOffRequest.uuid
            };

            CheckCookieAndMsgForZZZ checkResult = Common.CheckCookieAndMsgForZZZ(dishSellOffRequest.cookie, (int)dishSellOffRequest.type, (int)VAMessageType.ZZB_CLIENT_DISH_SELLOFF_REQUEST, dishSellOffRequest.shopId);
            if (checkResult.result == VAResult.VA_OK)
            {
                int employeeId = Common.ToInt32(checkResult.dtEmployee.Rows[0]["EmployeeID"]);
                bool isViewAllocWorker = Common.ToBool(checkResult.dtEmployee.Rows[0]["isViewAllocWorker"]);
                //AuthorityManager authorityMan = new AuthorityManager();
                if (ServiceFactory.Resolve<IShopAuthorityService>().GetEmployeeHasShopAuthority(dishSellOffRequest.shopId, employeeId, isViewAllocWorker, ShopRole.菜品沽清.GetString()))
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        try
                        {
                            DishManager dishManager = new DishManager();
                            var currentSellOffInfo =
                                dishManager.GetCurrentSellOffInfoByDishPrice(dishSellOffRequest.companyId,
                                    dishSellOffRequest.shopId, dishSellOffRequest.menuId,
                                    dishSellOffRequest.dishId, dishSellOffRequest.dishPriceId);

                            EmployeeOperateLogInfo employeeOperateLogInfo = null;

                            if (dishSellOffRequest.sellOff)
                            {
                                //沽清
                                if (currentSellOffInfo == null)
                                {
                                    dishManager.InsertCurrentSellOffInfoTable(new CurrentSellOffInfo()
                                    {
                                        companyId = dishSellOffRequest.companyId,
                                        DishI18nID = dishSellOffRequest.dishId,
                                        shopId = dishSellOffRequest.shopId,
                                        menuId = dishSellOffRequest.menuId,
                                        status = 1,
                                        DishPriceI18nID = dishSellOffRequest.dishPriceId,
                                        expirationTime = Common.ToDateTime(DateTime.Now.ToString("yyyy/MM/dd") + " 23:59:59"),
                                        operateEmployeeId = employeeId
                                    });
                                }
                                else if (currentSellOffInfo.status <= 0)
                                {
                                    dishManager.UpdateCurrentSellOffInfoStatus(currentSellOffInfo.Id, 1);
                                }

                                var commonCurrentSellOffInfo =
                                    dishManager.GetCommonCurrentSellOffInfoByDishPrice(dishSellOffRequest.menuId,
                                        dishSellOffRequest.dishId, dishSellOffRequest.dishPriceId);

                                if (commonCurrentSellOffInfo == null)
                                {
                                    dishManager.InsertCommonCurrentSellOffInfoTable(new CommonCurrentSellOffInfo()
                                    {
                                        currentSellOffCount = 1,
                                        DishI18nID = dishSellOffRequest.dishId,
                                        menuId = dishSellOffRequest.menuId,
                                        DishPriceI18nID = dishSellOffRequest.dishPriceId,
                                        status = 1
                                    });
                                }
                                else
                                {
                                    dishManager.UpdateCurrentSellOffCount(dishSellOffRequest.dishId,
                                        dishSellOffRequest.dishPriceId);
                                }

                                employeeOperateLogInfo = new EmployeeOperateLogInfo();
                                employeeOperateLogInfo.employeeId = employeeId;
                                employeeOperateLogInfo.operateTime = DateTime.Now;
                                employeeOperateLogInfo.operateType = 2;
                                employeeOperateLogInfo.pageType = 82;
                                employeeOperateLogInfo.operateDes = string.Format("悠先服务沽清菜品{0}", dishSellOffRequest.dishPriceId);




                            }
                            else
                            {
                                //取消沽清
                                dishManager.DeleteCurrentSellOffInfo(dishSellOffRequest.dishPriceId,
                                    dishSellOffRequest.shopId, employeeId);

                                employeeOperateLogInfo = new EmployeeOperateLogInfo();
                                employeeOperateLogInfo.employeeId = employeeId;
                                employeeOperateLogInfo.operateTime = DateTime.Now;
                                employeeOperateLogInfo.operateType = 2;
                                employeeOperateLogInfo.pageType = 82;
                                employeeOperateLogInfo.operateDes = string.Format("悠先服务取消沽清菜品{0}", dishSellOffRequest.dishPriceId);

                            }

                            scope.Complete();
                            response.result = VAResult.VA_OK;

                            if (employeeOperateLogInfo != null)
                            {
                                //开启单独线程插入数据
                                ParameterizedThreadStart threadstart =
                                    new ParameterizedThreadStart(Common.InsertEmployeeOperateLog);
                                Thread thread = new Thread(threadstart);
                                thread.IsBackground = true;
                                thread.Start(employeeOperateLogInfo);
                            }
                        }
                        catch (Exception)
                        {
                            response.result = VAResult.VA_FAILED_DB_ERROR;
                        }
                    }
                }
                else
                {
                    response.result = VAResult.VA_NO_ACCESS_INTERFACE_AUTHORITY;
                }
            }
            else
            {
                response.result = checkResult.result;
            }

            return response;
        }

        /// <summary>
        /// 悠先服务客户端push token更新
        /// </summary>
        /// <param name="pushTokenUpdateRequest"></param>
        /// <returns></returns>
        public ZZB_PushTokenUpdateResponse PushTokenUpdate(ZZB_PushTokenUpdateRequest pushTokenUpdateRequest)
        {
            ZZB_PushTokenUpdateResponse response = new ZZB_PushTokenUpdateResponse()
            {
                type = VAMessageType.ZZB_CLIENT_PUSHTOKEN_UPDATE_RESPONSE,
                cookie = pushTokenUpdateRequest.cookie,
                uuid = pushTokenUpdateRequest.uuid
            };

            CheckCookieAndMsgForZZZ checkResult = Common.CheckCookieAndMsgForZZZ(pushTokenUpdateRequest.cookie, (int)pushTokenUpdateRequest.type, (int)VAMessageType.ZZB_CLIENT_PUSHTOKEN_UPDATE_REQUEST);
            if (checkResult.result == VAResult.VA_OK)
            {
                //int employeeId = Common.ToInt32(checkResult.dtEmployee.Rows[0]["EmployeeID"]);
                try
                {
                    new EmployeeInfoManager().UpdatePushToken(pushTokenUpdateRequest.cookie, pushTokenUpdateRequest.pushToken, pushTokenUpdateRequest.serviceType);
                    response.result = VAResult.VA_OK;
                }
                catch (Exception)
                {
                    response.result = VAResult.VA_FAILED_DB_ERROR;
                }
            }
            else
            {
                response.result = checkResult.result;
            }
            return response;
        }

        /// <summary>
        /// 菜品价格修改
        /// </summary>
        /// <param name="clientDishPriceModifyRequest"></param>
        /// <returns></returns>
        public ZZB_ClientDishPriceModifyResponse DishPriceModify(ZZB_ClientDishPriceModifyRequest clientDishPriceModifyRequest)
        {
            ZZB_ClientDishPriceModifyResponse dishPriceModifyResponse = new ZZB_ClientDishPriceModifyResponse
            {
                type = VAMessageType.ZZB_CLIENT_DISH_PRICE_MODIFY_RESPONSE,
                cookie = clientDishPriceModifyRequest.cookie,
                uuid = clientDishPriceModifyRequest.uuid
            };

            CheckCookieAndMsgForZZZ checkResult = Common.CheckCookieAndMsgForZZZ(clientDishPriceModifyRequest.cookie, (int)clientDishPriceModifyRequest.type, (int)VAMessageType.ZZB_CLIENT_DISH_PRICE_MODIFY_REQUEST, clientDishPriceModifyRequest.shopId);
            if (checkResult.result == VAResult.VA_OK)
            {
                int employeeId = Common.ToInt32(checkResult.dtEmployee.Rows[0]["EmployeeID"]);
                bool isViewAllocWorker = Common.ToBool(checkResult.dtEmployee.Rows[0]["isViewAllocWorker"]);
                if (ServiceFactory.Resolve<IShopAuthorityService>().GetEmployeeHasShopAuthority(clientDishPriceModifyRequest.shopId, employeeId, isViewAllocWorker, ShopRole.调整价格.GetString()))
                {
                    var dishPriceInfoService = ServiceFactory.Resolve<IDishPriceInfoService>();
                    Task task = null;
                    try
                    {
                        task = dishPriceInfoService.ModifyDishPriceAndReturnDishMenuUpdateTask(
                            clientDishPriceModifyRequest.dishPriceId, clientDishPriceModifyRequest.price, employeeId);
                        if (task != null)
                        {
                            IMenuService menuService = ServiceFactory.Resolve<IMenuService>();
                            menuService.Update(task.Id);
                            dishPriceModifyResponse.taskId = task.Id;
                            dishPriceModifyResponse.result = VAResult.VA_OK;
                        }
                        else
                        {
                            dishPriceModifyResponse.result = VAResult.VA_FAILED_DISH_NOT_FOUND;
                        }
                    }
                    catch (Exception exc)
                    {
                        dishPriceModifyResponse.result = VAResult.VA_FAILED_DB_ERROR;
                    }

                }
                else
                {
                    dishPriceModifyResponse.result = VAResult.VA_NO_ACCESS_INTERFACE_AUTHORITY;
                }
            }
            else
            {
                dishPriceModifyResponse.result = checkResult.result;
            }

            return dishPriceModifyResponse;
        }

        public ZZB_ClientTaskCheckStatusResponse CheckTaskStatus(ZZB_ClientTaskCheckStatusRequest clientTaskCheckStatusRequest)
        {
            ZZB_ClientTaskCheckStatusResponse clientTaskCheckStatusResponse = new ZZB_ClientTaskCheckStatusResponse
            {
                type = VAMessageType.ZZB_CLIENT_TASK_CHECK_STATUS_RESPONSE,
                cookie = clientTaskCheckStatusRequest.cookie,
                uuid = clientTaskCheckStatusRequest.uuid
            };

            CheckCookieAndMsgForZZZ checkResult = Common.CheckCookieAndMsgForZZZ(clientTaskCheckStatusRequest.cookie, (int)clientTaskCheckStatusRequest.type, (int)VAMessageType.ZZB_CLIENT_TASK_CHECK_STATUS_REQUEST);
            if (checkResult.result == VAResult.VA_OK)
            {
                var taskService = ServiceFactory.Resolve<ITaskService>(); ;
                var task = taskService.GetTaskById(clientTaskCheckStatusRequest.taskId);
                clientTaskCheckStatusResponse.status = task != null ? task.Status : TaskStatus.处理成功;
                clientTaskCheckStatusResponse.result = VAResult.VA_OK;
            }
            else
            {
                clientTaskCheckStatusResponse.result = checkResult.result;
            }

            return clientTaskCheckStatusResponse;
        }

        /*
         * 悠先服务客户追溯模块
        */
        /// <summary>
        /// 客户追溯用户信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ZZB_ClientRetrospectCustomerResponse ClientRetrospectCustomer(ZZB_ClientRetrospectCustomerRequest request)
        {
            ZZB_ClientRetrospectCustomerResponse response = new ZZB_ClientRetrospectCustomerResponse()
            {
                type = VAMessageType.ZZB_CLIENT_RETROSPECT_CUSTOMER_RESPONSE,
                cookie = request.cookie,
                uuid = request.uuid
            };

            CheckCookieAndMsgForZZZ checkResult = Common.CheckCookieAndMsgForZZZ(request.cookie, (int)request.type, (int)VAMessageType.ZZB_CLIENT_RETROSPECT_CUSTOMER_REQUEST);
            if (checkResult.result == VAResult.VA_OK)
            {
                int employeeId = Common.ToInt32(checkResult.dtEmployee.Rows[0]["EmployeeID"]);//当前使用悠先服务操作的员工
                bool isViewAllocWorker = Common.ToBool(checkResult.dtEmployee.Rows[0]["isViewAllocWorker"]);
                if (!ServiceFactory.Resolve<IShopAuthorityService>().GetEmployeeHasShopAuthority(0, employeeId, isViewAllocWorker, ShopRole.客户追溯.GetString()))
                {
                    response.result = VAResult.VA_NO_ACCESS_INTERFACE_AUTHORITY;//没有访问接口权限
                    return response;
                }
                if (Common.ToClearSpecialCharString(request.mobilePhoneNumber).Length != 11)
                {
                    response.result = VAResult.VA_FAILED_FIND_CUSTOMER;
                    return response;
                }
                CustomerOperate customerOper = new CustomerOperate();
                DataTable dtCustomer = customerOper.QueryCustomerInfoByPhone(Common.ToClearSpecialCharString(request.mobilePhoneNumber));//简单过滤，防sql注入
                if (dtCustomer.Rows.Count == 1)
                {
                    response.mobilePhoneNumber = Common.ToString(dtCustomer.Rows[0]["mobilePhoneNumber"]);
                    response.customerId = Common.ToInt64(dtCustomer.Rows[0]["CustomerID"]);
                    response.money19dianRemained = Common.ToDouble(dtCustomer.Rows[0]["money19dianRemained"]);
                    response.registerDate = Common.ToSecondFrom1970(Common.ToDateTime(dtCustomer.Rows[0]["RegisterDate"]));
                    response.userName = Common.ToString(dtCustomer.Rows[0]["UserName"]);

                    RedEnvelopeOperate redEnvelopeOperate = new RedEnvelopeOperate();
                    double[] redEnvelope = redEnvelopeOperate.QueryCustomerRedEnvelope(response.mobilePhoneNumber);
                    response.executedRedEnvelopeAmount = redEnvelope[0];
                    response.notExecutedRedEnvelopeAmount = redEnvelope[1];

                    response.result = VAResult.VA_OK;
                }
                else
                {
                    response.result = VAResult.VA_FAILED_FIND_CUSTOMER;
                }
            }
            else
            {
                response.result = checkResult.result;
            }
            return response;
        }
        /// <summary>
        /// 查看追溯到当前用户的历史点单列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ZZB_ClientRetrospectCustomerOrderResponse ClientRetrospectCustomerOrderList(ZZB_ClientRetrospectCustomerOrderRequest request)
        {
            ZZB_ClientRetrospectCustomerOrderResponse response = new ZZB_ClientRetrospectCustomerOrderResponse()
            {
                type = VAMessageType.ZZB_CLIENT_RETROSPECT_CUSTOMER_ORDER_RESPONSE,
                cookie = request.cookie,
                uuid = request.uuid
            };
            CheckCookieAndMsgForZZZ checkResult = Common.CheckCookieAndMsgForZZZ(request.cookie, (int)request.type, (int)VAMessageType.ZZB_CLIENT_RETROSPECT_CUSTOMER_ORDER_REQUEST);
            if (checkResult.result == VAResult.VA_OK)
            {
                int employeeId = Common.ToInt32(checkResult.dtEmployee.Rows[0]["EmployeeID"]);//当前使用悠先服务操作的员工
                bool isViewAllocWorker = Common.ToBool(checkResult.dtEmployee.Rows[0]["isViewAllocWorker"]);
                if (!ServiceFactory.Resolve<IShopAuthorityService>().GetEmployeeHasShopAuthority(0, employeeId, isViewAllocWorker, ShopRole.客户追溯.GetString()))
                {
                    response.result = VAResult.VA_NO_ACCESS_INTERFACE_AUTHORITY;//没有访问接口权限
                    response.customerOrderList = new List<CustomerOrder>();//防止ios客户端解析null
                    response.isHaveMore = false;
                    return response;
                }
                PreOrder19dianManager man = new PreOrder19dianManager();
                CustomerOrderList list = man.ZZBSelectCustomerOrder(request.customerId, request.pageSize, request.pageIndex);
                response.customerOrderList = list.customerOrderList;
                if ((request.pageIndex * request.pageSize) < list.page.totalCount)
                {
                    response.isHaveMore = true;
                }
                else
                {
                    response.isHaveMore = false;
                }
                response.result = VAResult.VA_OK;
            }
            else
            {
                response.customerOrderList = new List<CustomerOrder>();//防止ios客户端解析null
                response.isHaveMore = false;
                response.result = checkResult.result;
            }
            return response;
        }
        /// <summary>
        /// 查询追溯到用户某个历史点单的详情内容
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ZZB_ClientRetrospectCustomerOrderDetailResponse ClientRetrospectCustomerOrderDetail(ZZB_ClientRetrospectCustomerOrderDetailRequest request)
        {
            ZZB_ClientRetrospectCustomerOrderDetailResponse response = new ZZB_ClientRetrospectCustomerOrderDetailResponse();
            response.type = VAMessageType.ZZB_CLIENT_RETROSPECT_CUSTOMER_ORDERDETAIL_RESPONSE;
            response.cookie = request.cookie;
            response.uuid = request.uuid;
            CheckCookieAndMsgForZZZ checkResult = Common.CheckCookieAndMsgForZZZ(request.cookie, (int)request.type, (int)VAMessageType.ZZB_CLIENT_RETROSPECT_CUSTOMER_ORDERDETAIL_REQUEST);
            List<OrderPayMode> payModeList = new List<OrderPayMode>();
            if (checkResult.result == VAResult.VA_OK)
            {
                int employeeId = Common.ToInt32(checkResult.dtEmployee.Rows[0]["EmployeeID"]);//当前使用悠先服务操作的员工
                bool isViewAllocWorker = Common.ToBool(checkResult.dtEmployee.Rows[0]["isViewAllocWorker"]);
                if (!ServiceFactory.Resolve<IShopAuthorityService>().GetEmployeeHasShopAuthority(0, employeeId, isViewAllocWorker, ShopRole.客户追溯.GetString()))
                {
                    response.result = VAResult.VA_NO_ACCESS_INTERFACE_AUTHORITY;//没有访问接口权限
                    response.payModeList = payModeList;//避免ios客户端解析null
                    return response;
                }
                PreOrder19dianManager man = new PreOrder19dianManager();
                DataTable dtOrderInfo = man.SelectPaidOrderByCustomerId(0, request.preOrder19dianId);
                if (dtOrderInfo.Rows.Count == 1)
                {
                    response.preOrder19dianId = Common.ToInt64(dtOrderInfo.Rows[0]["preOrder19dianId"]);
                    response.shopName = Common.ToString(dtOrderInfo.Rows[0]["shopName"]);
                    response.prePaidSum = Common.ToDouble(dtOrderInfo.Rows[0]["prePaidSum"]);
                    response.prePayTime = Common.ToSecondFrom1970(Common.ToDateTime(dtOrderInfo.Rows[0]["prePayTime"]));
                    response.refundMoneySum = Common.ToDouble(dtOrderInfo.Rows[0]["refundMoneySum"]);//点单申请退款金额
                    response.isShopConfirmed = Common.ToString(dtOrderInfo.Rows[0]["isShopConfirmed"]);
                    response.isApproved = Common.ToString(dtOrderInfo.Rows[0]["isApproved"]);
                    ThirdPartyPaymentInfo thirdPartyPaymentInfo = man.SelectPreorderPayAmount(request.preOrder19dianId);
                    ZZBPreOrderOperate operate = new ZZBPreOrderOperate();
                    CouponManager couponManager = new CouponManager();
                    List<double> couponAmount = couponManager.GetAmountOfOrder(response.preOrder19dianId);//优惠券点单抵扣金额
                    if (couponAmount != null && couponAmount.Any())
                    {
                        response.deductibleAmount = couponAmount[2];
                    }
                    payModeList
                        = operate.GetOrderPayModeInfo(response.prePaidSum, thirdPartyPaymentInfo, true,
                                                      request.preOrder19dianId, Common.ToInt32(dtOrderInfo.Rows[0]["status"]));
                    response.result = VAResult.VA_OK;

                    //PreOrder19dianOperate orderOperate = new PreOrder19dianOperate();
                    //DataTable dtOrder = man.SelectOrder(response.preOrder19dianId);

                    //double originalPrice = 0;
                    //double discountPrice = 0;
                    //double discountSundryPrice = 0;

                    //string orderInJson = Common.ToString(dtOrder.Rows[0]["orderInJson"]);
                    //double currentDiscount = Common.ToDouble(dtOrder.Rows[0]["discount"]);
                    //string sundryJson = Common.ToString(dtOrder.Rows[0]["sundryJson"]);
                    //if (!String.IsNullOrEmpty(orderInJson))
                    //{
                    //    double deductionCoupon;//
                    //    discountPrice = orderOperate.CalcPreorederVIPPrice(orderInJson, currentDiscount, out deductionCoupon);//菜品折扣后价格
                    //}
                    //else
                    //{
                    //    discountPrice = originalPrice;//菜品折扣后价格
                    //}
                    //杂项
                    //if (!String.IsNullOrEmpty(Common.ToString(sundryJson)))
                    //{
                    //    List<VASundryInfo> sundryListRequest = JsonOperate.JsonDeserialize<List<VASundryInfo>>(Common.ToString(sundryJson));
                    //    List<SundryInfoResponse> sundryInfoList2 = new List<SundryInfoResponse>();
                    //    discountSundryPrice = Common.ToDouble(orderOperate.CalcSundryCount(originalPrice, sundryListRequest, ref sundryInfoList2, currentDiscount));//杂项折扣后价格
                    //}
                    //double serverUxianPriceSum = discountPrice + discountSundryPrice;//整体折扣后价格（悠先价）
                    //门店是否支持四舍五入
                    //if (Common.ToBool(Common.ToString(dtOrder.Rows[0]["isSupportAccountsRound"])))
                    //{
                    //    response.deductibleAmount = Common.ToDouble(Common.ToDouble(CommonPageOperate.YouLuoRound(serverUxianPriceSum, 0)) - response.prePaidSum);
                    //}
                    //else
                    //{
                    //    response.deductibleAmount = Common.ToDouble(Common.ToDouble(serverUxianPriceSum) - response.prePaidSum);
                    //}

                    if (new CouponOperate().IsCouponRefund(response.preOrder19dianId))
                    {
                        response.isPersonalFullRefund = true;
                    }
                }
                else
                {
                    response.result = VAResult.VA_FAILED_NOT_FOUND_ORDER;//未找到当前点单
                }
            }
            else
            {
                response.result = checkResult.result;
            }
            response.payModeList = payModeList;//避免ios客户端解析null
            return response;
        }
        /// <summary>
        /// 计算点单支付详细
        /// </summary>
        /// <param name="prePaidSum"></param>
        /// <param name="thirdPartyPaymentInfo"></param>
        /// <param name="isPaid"></param>
        /// <param name="preorderId"></param>
        /// <returns></returns>
        public List<OrderPayMode> GetOrderPayModeInfo(double prePaidSum, ThirdPartyPaymentInfo thirdPartyPaymentInfo, bool isPaid, long preorderId, int preorderStatus = 102, bool isLatestBuild = false)
        {
            var payModeList = new List<OrderPayMode>();

            if (!isPaid)//未支付，客户追溯查询的都是用户支付的订单，不会执行下面代码
            {
                payModeList.Add(new OrderPayMode()
                {
                    payAmount = Common.ToDecimal(prePaidSum),
                    payModeName = "订单金额",
                    orderUsedPayMode = (int)VAOrderUsedPayMode.NOT_PAY_ORDER
                });
                return payModeList;
            }
            #region
            double ExtendPay = 0;
            OrderPayMode thirdOrderPayMode = null;

            var preorder19dianLineList = Preorder19DianLineOperate.GetListByQuery(new Preorder19DianLineQueryObject() { Preorder19DianId = preorderId });

            VAPreorderStatus vaPreorderStatus = (VAPreorderStatus)preorderStatus;
            //已入座的单子作为已付款
            if (vaPreorderStatus == VAPreorderStatus.Completed)
            {
                preorderStatus = (int)VAPreorderStatus.Prepaid;
            }
            if (preorder19dianLineList != null && preorder19dianLineList.Count > 0)
            {
                var thirdPayMode = preorder19dianLineList.FirstOrDefault(p => (VAOrderUsedPayMode)p.PayType == VAOrderUsedPayMode.ALIPAY
                                                                         || (VAOrderUsedPayMode)p.PayType == VAOrderUsedPayMode.WECHAT);


                //第三方支付金额需减去额外支付的0.01
                if (thirdPayMode != null)
                {
                    //仅在用户退款的"退款中"、"已退款"状态中显示额外支付的0.01
                    if (vaPreorderStatus == VAPreorderStatus.Refund || vaPreorderStatus == VAPreorderStatus.OriginalRefunding)
                    {
                        if (thirdPayMode.RefundAmount > 0)
                        {
                            thirdOrderPayMode = new OrderPayMode()
                            {
                                status = preorderStatus,
                                refundAmount = Common.ToDecimal(thirdPayMode.RefundAmount),
                                payAmount = Common.ToDecimal(thirdPayMode.Amount),
                                orderUsedPayMode = thirdPayMode.PayType,
                                payModeName = Common.GetEnumDescription((VAOrderUsedPayMode)thirdPayMode.PayType),
                                sortOrder = 40
                            };
                            payModeList.Add(thirdOrderPayMode);
                        }
                    }
                    else
                    {
                        ExtendPay = PreOrder19dianOperate.GetExtendPayByPreOrder19DianId(preorderId);
                        thirdPayMode.Amount = thirdPayMode.Amount - ExtendPay;
                        if (thirdPayMode.Amount > 0)
                        {
                            thirdOrderPayMode = new OrderPayMode()
                            {
                                status = preorderStatus,
                                refundAmount = Common.ToDecimal(thirdPayMode.RefundAmount),
                                payAmount = Common.ToDecimal(thirdPayMode.Amount),
                                orderUsedPayMode = thirdPayMode.PayType,
                                payModeName = Common.GetEnumDescription((VAOrderUsedPayMode)thirdPayMode.PayType),
                                sortOrder = 40
                            };
                            if (thirdOrderPayMode.refundAmount > 0)
                            {
                                thirdOrderPayMode.status = (int)VAPreorderStatus.Refund;
                            }
                            payModeList.Add(thirdOrderPayMode);
                        }
                    }
                }
                //粮票、红包、抵扣券无“退款中”状态
                if (vaPreorderStatus == VAPreorderStatus.OriginalRefunding)
                {
                    preorderStatus = (int)VAPreorderStatus.Refund;
                }
                if (isLatestBuild)//2015-4-22起的版本，取支付明细表的数据，老版本不发抵扣券数据
                {
                    //抵扣券
                    var couponPayMode
                                = preorder19dianLineList.FirstOrDefault(p => (VAOrderUsedPayMode)p.PayType == VAOrderUsedPayMode.COUPON);
                    //抵扣券需要判断是否作废
                    if (couponPayMode != null)
                    {
                        if (vaPreorderStatus == VAPreorderStatus.Refund || vaPreorderStatus == VAPreorderStatus.OriginalRefunding)
                        {
                            couponPayMode.State = (int)VAPreorderStatus.Cancel;
                        }
                        var orderPayMode = new OrderPayMode()
                        {
                            status = couponPayMode.State,
                            refundAmount = (decimal)couponPayMode.RefundAmount,
                            payAmount = (decimal)couponPayMode.Amount,
                            orderUsedPayMode = couponPayMode.PayType,
                            payModeName = Common.GetEnumDescription((VAOrderUsedPayMode)couponPayMode.PayType),
                            sortOrder = 10
                        };
                        payModeList.Add(orderPayMode);
                    }
                }
                //红包
                var redEnvelopePayMode
                     = preorder19dianLineList.FirstOrDefault(p => (VAOrderUsedPayMode)p.PayType == VAOrderUsedPayMode.REDENVELOPE);
                if (redEnvelopePayMode != null)
                {
                    var redEnvelopeOrderPayMode = new OrderPayMode()
                    {
                        status = preorderStatus,
                        refundAmount = Common.ToDecimal(redEnvelopePayMode.RefundAmount),
                        payAmount = Common.ToDecimal(redEnvelopePayMode.Amount),
                        orderUsedPayMode = redEnvelopePayMode.PayType,
                        payModeName = Common.GetEnumDescription((VAOrderUsedPayMode)redEnvelopePayMode.PayType),
                        sortOrder = 20
                    };
                    if (redEnvelopeOrderPayMode.refundAmount > 0)
                    {
                        redEnvelopeOrderPayMode.status = (int)VAPreorderStatus.Refund;
                    }
                    payModeList.Add(redEnvelopeOrderPayMode);
                }
                //粮票
                var balancePayMode
                    = preorder19dianLineList.FirstOrDefault(p => (VAOrderUsedPayMode)p.PayType == VAOrderUsedPayMode.BALANCE);
                if (balancePayMode != null)
                {
                    var balanceOrderPayMode = new OrderPayMode()
                    {
                        status = preorderStatus,
                        refundAmount = Common.ToDecimal(balancePayMode.RefundAmount),
                        payAmount = Common.ToDecimal(balancePayMode.Amount),
                        orderUsedPayMode = balancePayMode.PayType,
                        payModeName = Common.GetEnumDescription((VAOrderUsedPayMode)balancePayMode.PayType),
                        sortOrder = 30
                    };
                    if (balanceOrderPayMode.refundAmount > 0)
                    {
                        balanceOrderPayMode.status = (int)VAPreorderStatus.Refund;
                    }
                    payModeList.Add(balanceOrderPayMode);
                }
            }
            #endregion

            return payModeList.OrderBy(p => p.sortOrder).ToList();
        }

        /// <summary>
        /// 计算点单支付详细
        /// </summary>
        /// <param name="prePaidSum"></param>
        /// <param name="thirdPartyPaymentInfo"></param>
        /// <param name="isPaid"></param>
        /// <param name="preorderId"></param>
        /// <returns></returns>
        public List<OrderPayMode> GetOrderPayModeInfos(double prePaidSum, bool isPaid, Guid orderId, VAPreorderStatus preorderStatus = VAPreorderStatus.Prepaid)
        {
            var payModeList = new List<OrderPayMode>();

            if (!isPaid)//未支付，客户追溯查询的都是用户支付的订单，不会执行下面代码
            {
                payModeList.Add(new OrderPayMode()
                {
                    payAmount = Common.ToDecimal(prePaidSum),
                    payModeName = "订单金额",
                    orderUsedPayMode = (int)VAOrderUsedPayMode.NOT_PAY_ORDER
                });
                return payModeList;
            }

            IList<Preorder19DianLine> preorder19dianLineList = new Preorder19DianLineManager().GetOrderToPrices(orderId).ToList();

            foreach (var item in preorder19dianLineList)
            {
                if (preorderStatus == VAPreorderStatus.Completed)
                    preorderStatus = VAPreorderStatus.Prepaid;

                int sortOrder = 0;
                VAOrderUsedPayMode payType = (VAOrderUsedPayMode)item.PayType;
                switch (payType)
                {
                    case VAOrderUsedPayMode.BALANCE:
                        sortOrder = 30;
                        break;
                    case VAOrderUsedPayMode.ALIPAY:
                        sortOrder = 50;
                        break;
                    case VAOrderUsedPayMode.WECHAT:
                        sortOrder = 40;
                        break;
                    case VAOrderUsedPayMode.REDENVELOPE:
                        sortOrder = 20;
                        break;
                    case VAOrderUsedPayMode.COUPON:
                        sortOrder = 10;
                        break;
                }

                VAPreorderStatus status = preorderStatus;
                if (payType == VAOrderUsedPayMode.BALANCE || payType == VAOrderUsedPayMode.REDENVELOPE)
                {
                    if (item.RefundAmount > 0)
                        status = VAPreorderStatus.Refund;
                }
                if (payType == VAOrderUsedPayMode.COUPON)
                {
                    if (preorderStatus == VAPreorderStatus.Refund || preorderStatus == VAPreorderStatus.OriginalRefunding)
                        status = VAPreorderStatus.Cancel;
                }
                if (payType == VAOrderUsedPayMode.ALIPAY || payType == VAOrderUsedPayMode.WECHAT)
                {
                    if (preorderStatus != VAPreorderStatus.Refund && preorderStatus != VAPreorderStatus.Overtime)
                    {
                        //Preorder19DianLine的状态没人用，当你是第三方支付，并且不是退款中状态，Line表有退款金额就显示order状态
                        if (item.RefundAmount > 0)
                        {
                            if (preorderStatus != VAPreorderStatus.Prepaid)
                                status = preorderStatus;
                            else
                                status = VAPreorderStatus.Refund;
                        }

                        //还回0.01
                        var extendPay = new PreOrder19dianManager().GetExtendPayByOrderId(orderId);
                        //item.Amount = item.Amount - extendPay;
                        //modify by dangtao at 20150731 for bug BUG #2081 红包+抵扣券，需要多付的第三方0.01元，在订单详情里不要显示。
                        if (extendPay == item.Amount && item.Amount != item.RefundAmount)//支付与额外付款相等,即为多付0.01，退款与支付相等，即用户自己退款
                        {
                            continue;
                        }
                    }
                }

                payModeList.Add(new OrderPayMode()
                {
                    status = (int)status,
                    payModeName = Common.GetEnumDescription((VAOrderUsedPayMode)item.PayType),
                    payAmount = (decimal)item.Amount,
                    orderUsedPayMode = item.PayType,
                    refundAmount = (decimal)item.RefundAmount,
                    sortOrder = sortOrder
                });
            }

            //if ((decimal)preorder19dianLineList.Where(p => p.PayType == (int)VAOrderUsedPayMode.REDENVELOPE).Sum(p => p.Amount) == (decimal)prePaidSum)
            //    payModeList.Remove(payModeList.Find(p => (p.orderUsedPayMode == (int)VAOrderUsedPayMode.WECHAT || p.orderUsedPayMode == (int)VAOrderUsedPayMode.ALIPAY) && p.payAmount <= 0.01M));

            return payModeList.OrderBy(p => p.sortOrder).ToList();
        }

        /// <summary>
        /// 调整追溯到用户的余额
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ZZB_ClientRetrospectCustomerChangeBalanceResponse ClientRetrospectCustomerChangeBalance(ZZB_ClientRetrospectCustomerChangeBalanceRequest request)
        {
            ZZB_ClientRetrospectCustomerChangeBalanceResponse response = new ZZB_ClientRetrospectCustomerChangeBalanceResponse();
            response.type = VAMessageType.ZZB_CLIENT_RETROSPECT_CUSTOMER_CHANGEBALANCE_RESPONSE;
            response.cookie = request.cookie;
            response.uuid = request.uuid;
            CheckCookieAndMsgForZZZ checkResult = Common.CheckCookieAndMsgForZZZ(request.cookie, (int)request.type, (int)VAMessageType.ZZB_CLIENT_RETROSPECT_CUSTOMER_CHANGEBALANCE_REQUEST);
            CustomerManager man = new CustomerManager();
            if (checkResult.result == VAResult.VA_OK)
            {
                int employeeId = Common.ToInt32(checkResult.dtEmployee.Rows[0]["EmployeeID"]);//当前使用悠先服务操作的员工
                bool isViewAllocWorker = Common.ToBool(checkResult.dtEmployee.Rows[0]["isViewAllocWorker"]);
                if (!ServiceFactory.Resolve<IShopAuthorityService>().GetEmployeeHasShopAuthority(0, employeeId, isViewAllocWorker, ShopRole.余额调整.GetString()) ||
                    !ServiceFactory.Resolve<IShopAuthorityService>().GetEmployeeHasShopAuthority(0, employeeId, isViewAllocWorker, ShopRole.客户追溯.GetString()))
                {
                    response.result = VAResult.VA_NO_ACCESS_INTERFACE_AUTHORITY;//没有访问接口权限
                    return response;
                }
                CustomerOperate customerOper = new CustomerOperate();
                using (TransactionScope scope = new TransactionScope())
                {
                    double changeAmount = Common.ToDouble(request.changeAmount);//处理小数
                    DataTable dtCustomer = man.SelectCustomer(request.customerId);
                    if (dtCustomer.Rows.Count == 1)
                    {
                        if (changeAmount > 99999)
                        {
                            response.result = VAResult.VA_FAILED_ADJUSTMENT_AMOUNT_OVERSIZE;//最大余额调整金额为99999
                            return response;
                        }
                        if (changeAmount + Common.ToDouble(dtCustomer.Rows[0]["money19dianRemained"]) < 0)
                        {
                            response.result = VAResult.VA_FAILED_ADJUSTMENT_AMOUNT_UNDERSIZE;//余额不能调整为0
                            return response;
                        }
                        string customerCookie = Common.ToString(dtCustomer.Rows[0]["cookie"]);
                        bool change = customerOper.CustomerRecharge("", changeAmount, customerCookie);
                        RechargeLog log = new RechargeLog()//日志
                        {
                            amount = changeAmount,
                            cookie = customerCookie,
                            customerPhone = Common.ToString(dtCustomer.Rows[0]["mobilePhoneNumber"]),
                            employeeId = employeeId,
                            remark = request.remark,
                            operateTime = DateTime.Now
                        };
                        RechargeLogOperate logOperate = new RechargeLogOperate();
                        bool addLog = logOperate.Add(log);
                        if (change && addLog)
                        {
                            scope.Complete();
                            response.result = VAResult.VA_OK;
                        }
                        else
                        {
                            response.result = VAResult.VA_FAILED_CHANGE_REMAINMONEY;//更新余额失败，db error
                        }
                    }
                    else
                    {
                        response.result = VAResult.VA_FAILED_FIND_CUSTOMER;//未找到用户信息
                    }
                }
                if (response.result == VAResult.VA_OK)//操作成功，事物外查询当前余额，返回客户端
                {
                    DataTable dtCustomerEnd = man.SelectCustomer(request.customerId);
                    if (dtCustomerEnd.Rows.Count == 1)
                    {
                        response.remainMoney = Common.ToDouble(dtCustomerEnd.Rows[0]["money19dianRemained"]);
                    }
                }
            }
            else
            {
                response.result = checkResult.result;
            }
            return response;
        }
        /*
         *悠先服务查看门店VIP数量web view
        */
        /// <summary>
        /// 悠先服务查看门店VIP数量web view
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ZZB_ClientCheckShopVipUsersInfoResponse ClientCheckShopVipUsersInfo(ZZB_ClientCheckShopVipUsersInfoRequest request)
        {
            ZZB_ClientCheckShopVipUsersInfoResponse response = new ZZB_ClientCheckShopVipUsersInfoResponse()
            {
                type = VAMessageType.ZZB_CLIENT_CHECK_SHOP_VIPUSERSINFO_RESPONSE,
                cookie = request.cookie,
                uuid = request.uuid
            };

            CheckCookieAndMsgForZZZ checkResult = Common.CheckCookieAndMsgForZZZ(request.cookie, (int)request.type, (int)VAMessageType.ZZB_CLIENT_CHECK_SHOP_VIPUSERSINFO_REQUEST);
            if (checkResult.result == VAResult.VA_OK)
            {
                int shopId = request.shopId;
                int employeeId = Common.ToInt32(checkResult.dtEmployee.Rows[0]["EmployeeID"]);//当前使用悠先服务操作的员工
                bool isViewAllocWorker = Common.ToBool(checkResult.dtEmployee.Rows[0]["isViewAllocWorker"]);
                if (!ServiceFactory.Resolve<IShopAuthorityService>().GetEmployeeHasShopAuthority(shopId, employeeId, isViewAllocWorker, ShopRole.门店会员.GetString()))
                {
                    response.result = VAResult.VA_NO_ACCESS_INTERFACE_AUTHORITY;//没有访问接口权限
                    return response;
                }
                ShopOperate shopOper = new ShopOperate();
                ShopInfo shopInfo = shopOper.QueryShop(shopId);
                if (shopInfo == null)
                {
                    response.result = VAResult.VA_FAILED_DB_ERROR;
                    return response;
                }
                int cityId = shopInfo.cityID;
                ShopVIPOperate operate = new ShopVIPOperate();
                ZZB_ShopVipResponse model = operate.GetZZBShopVipResponse(shopId, cityId);//真实当前门店会员信息
                ZZB_ShopVIPFlaseCountModel falseModel = operate.GetShopVIPFlaseCountModel(cityId);//虚假累计会员数量信息
                if (model != null)
                {
                    response.currectCityName = model.currectCityName.Replace("市", "");
                    response.currectMonth = model.currectMonth;
                    response.currectMonthShopIncreasedCount = model.currectMonthShopIncreasedCount;
                    response.shopTotalCount = model.shopTotalCount;
                    //注意新老版本兼容
                    if (Common.UXServiceCheckLatestBuild_March(request.serviceType, request.clientBuild))
                    {
                        response.shopVipWebUrl = WebConfig.ServerDomain + "AppPages/discountService/vipView.html?s={0}&e={1}";
                    }
                    else
                    {
                        response.shopVipWebUrl = WebConfig.ServerDomain + "ViewAllocVip/viewallocVip.aspx?shopId=" + shopId;
                    }
                    if (falseModel != null)
                    {
                        response.currectCityTopShopTotalCount = model.currectCityTopShopTotalCount > falseModel.currectCityTopShopTotalCountFalse
                                                         ? model.currectCityTopShopTotalCount : falseModel.currectCityTopShopTotalCountFalse;
                        response.currectMonthTopShopIncreasedCount = model.currectMonthTopShopIncreasedCount > falseModel.currectMonthTopShopIncreasedCountFalse
                                                                   ? model.currectMonthTopShopIncreasedCount : falseModel.currectMonthTopShopIncreasedCountFalse;
                    }
                    else
                    {
                        response.currectCityTopShopTotalCount = model.currectCityTopShopTotalCount;
                        response.currectMonthTopShopIncreasedCount = model.currectMonthTopShopIncreasedCount;
                    }
                    response.result = VAResult.VA_OK;
                }
                else
                {
                    response.result = VAResult.VA_FAILED_DB_ERROR;
                }
            }
            else
            {
                response.result = checkResult.result;
            }
            return response;
        }

        /// <summary>
        /// 权限判断
        /// </summary>
        /// <param name="roleCheckRequest"></param>
        /// <returns></returns>
        public ZZB_RoleCheckResponse RoleCheck(ZZB_RoleCheckRequest roleCheckRequest)
        {
            ZZB_RoleCheckResponse roleCheckResponse = new ZZB_RoleCheckResponse
            {
                type = VAMessageType.ZZB_CLIENT_ROLE_CHECK_RESPONSE,
                cookie = roleCheckRequest.cookie,
                uuid = roleCheckRequest.uuid
            };

            CheckCookieAndMsgForZZZ checkResult = Common.CheckCookieAndMsgForZZZ(roleCheckRequest.cookie, (int)roleCheckRequest.type, (int)VAMessageType.ZZB_CLIENT_ROLE_CHECK_REQUEST, roleCheckRequest.shopId);
            if (checkResult.result == VAResult.VA_OK)
            {
                int employeeId = Common.ToInt32(checkResult.dtEmployee.Rows[0]["EmployeeID"]);
                bool isViewAllocWorker = Common.ToBool(checkResult.dtEmployee.Rows[0]["isViewAllocWorker"]);
                if (ServiceFactory.Resolve<IShopAuthorityService>().GetEmployeeHasShopAuthority(roleCheckRequest.shopId, employeeId, isViewAllocWorker, roleCheckRequest.authorityCode))
                {
                    roleCheckResponse.url = "";
                    #region ------------------------------------------------------------
                    if (Common.ToInt32(roleCheckRequest.authorityCode) == Common.ToInt32(ShopRole.抽奖活动统计))
                    {
                        roleCheckResponse.url = WebConfig.ServerDomain + "Award/awardMobile/statistics/statistics.html?s={0}&e={1}";

                    }
                    if (Common.ToInt32(roleCheckRequest.authorityCode) == Common.ToInt32(ShopRole.门店会员))
                    {
                        roleCheckResponse.url = WebConfig.ServerDomain + "AppPages/discountService/vipView.html?s={0}&e={1}";
                    }
                    if (Common.ToInt32(roleCheckRequest.authorityCode) == Common.ToInt32(ShopRole.抽奖设置))
                    {
                        // 第一次设置时进入引导页
                        roleCheckResponse.url = WebConfig.ServerDomain + "Award/awardMobile/awardSet/awardSet.html?s={0}&e={1}";
                        // 第二次修改时进入修改页面（赠菜为空表示未进入过引导页面）
                        ShopAwardOperate operateShopAward = new ShopAwardOperate();
                        var listShopAward = operateShopAward.SelectShopAwardList(roleCheckRequest.shopId);
                        if (listShopAward.Count > 0)
                        {
                            //listShopAward = listShopAward.FindAll(s => s.AwardType == AwardType.PresentDish);
                            if (listShopAward.Count > 0)
                            {
                                roleCheckResponse.url = WebConfig.ServerDomain + "Award/awardMobile/awardSet/awardSee.html?s={0}&e={1}";
                            }
                        }
                    }
                    if (Common.ToInt32(roleCheckRequest.authorityCode) == Common.ToInt32(ShopRole.会员营销统计))
                    {
                        roleCheckResponse.url = WebConfig.UxianAppServerDomain + "GiftCertificate/StatisticsGiftCertificate?s={0}&e={1}";
                    }
                    if (Common.ToInt32(roleCheckRequest.authorityCode) == Common.ToInt32(ShopRole.会员营销))
                    {
                        roleCheckResponse.url = WebConfig.UxianAppServerDomain + "GiftCertificate/GiftCertificateList?s={0}&e={1}&c=" + roleCheckRequest.cityId + "&phoneNumber=" + checkResult.dtEmployee.Rows[0]["UserName"].ToString() + "";
                    }
                    if (Common.ToInt32(roleCheckRequest.authorityCode) == Common.ToInt32(ShopRole.抵扣券发布))
                    {
                        roleCheckResponse.url = WebConfig.ServerDomain + "AppPages/discountService/release.html?s={0}&e={1}";
                    }
                    if (Common.ToInt32(roleCheckRequest.authorityCode) == Common.ToInt32(ShopRole.抵扣券统计))
                    {
                        roleCheckResponse.url = WebConfig.ServerDomain + "AppPages/discountService/activeCount.html?s={0}";
                    }

                    roleCheckResponse.url = string.Format(roleCheckResponse.url, roleCheckRequest.shopId, employeeId);
                    #endregion
                    roleCheckResponse.result = VAResult.VA_OK;
                }
                else
                {
                    roleCheckResponse.result = VAResult.VA_NO_ACCESS_INTERFACE_AUTHORITY;
                }
            }
            else
            {
                roleCheckResponse.result = checkResult.result;
            }

            return roleCheckResponse;
        }

        /// <summary>
        /// 悠先服务-客户信息-消费记录(分页)回复
        /// </summary>
        /// <param name="customerDetailsRequest"></param>
        /// <returns></returns>
        public ZZB_CustomerPreOrdersResponse GetCustomerPreOrders(ZZB_CustomerDetailsRequest customerDetailsRequest)
        {
            ZZB_CustomerPreOrdersResponse customerPreOrdersResponse = new ZZB_CustomerPreOrdersResponse
            {
                type = VAMessageType.ZZB_CLINET_CUSTOMER_DETAILS_RESPONSE,
                cookie = customerDetailsRequest.cookie,
                uuid = customerDetailsRequest.uuid
            };

            CheckCookieAndMsgForZZZ checkResult = Common.CheckCookieAndMsgForZZZ(customerDetailsRequest.cookie, (int)customerDetailsRequest.type, (int)VAMessageType.ZZB_CLINET_CUSTOMER_DETAILS_REQUEST, customerDetailsRequest.shopId);
            if (checkResult.result == VAResult.VA_OK)
            {
                int employeeId = Common.ToInt32(checkResult.dtEmployee.Rows[0]["EmployeeID"]);
                bool isViewAllocWorker = Common.ToBool(checkResult.dtEmployee.Rows[0]["isViewAllocWorker"]);
                if (ServiceFactory.Resolve<IShopAuthorityService>().GetEmployeeHasShopAuthority(customerDetailsRequest.shopId, employeeId, isViewAllocWorker, ShopRole.客户信息.GetString()))
                {
                    IPreOrder19DianInfoRepository preOrder19DianInfoRepository =
                        ServiceFactory.Resolve<IPreOrder19DianInfoRepository>();

                    try
                    {
                        var orders = preOrder19DianInfoRepository.GetPageCustomerPayforOrders(
                            new Page(customerDetailsRequest.pageNubmer, 10), customerDetailsRequest.customerId);

                        customerPreOrdersResponse.preOrders = (from preOrderForShop in orders
                                                               select new PreOrderLog
                                                               {
                                                                   shopName = preOrderForShop.shopName,
                                                                   preOrderDate = preOrderForShop.preOrderTime.ToString("yyyy.MM.dd"),
                                                                   preOrderAmount = FormatAmount(preOrderForShop.prePaidSum),
                                                                   shopLogo =
                                                                       WebConfig.CdnDomain + WebConfig.ImagePath + preOrderForShop.shopImagePath +
                                                                       preOrderForShop.shopLogo,
                                                                   dishs = string.IsNullOrEmpty(preOrderForShop.orderInJson) ? new string[0] :
                                                                       JsonOperate.JsonDeserialize<List<PreOrderIn19dian>>(preOrderForShop.orderInJson)
                                                                           .Select(p => p.dishName)
                                                                           .ToArray()
                                                               }).ToList();

                        customerPreOrdersResponse.nextPage = orders.HasNextPage;
                        customerPreOrdersResponse.result = VAResult.VA_OK;
                    }
                    catch (Exception)
                    {
                        customerPreOrdersResponse.result = VAResult.VA_FAILED_DB_ERROR;
                    }

                }
                else
                {
                    customerPreOrdersResponse.result = VAResult.VA_NO_ACCESS_INTERFACE_AUTHORITY;
                }
            }
            else
            {
                customerPreOrdersResponse.result = checkResult.result;
            }

            return customerPreOrdersResponse;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="customerDetailsRequest"></param>
        /// <returns></returns>
        public ZZB_CustomerDetailsResponse GetCustomerDetails(ZZB_CustomerDetailsRequest customerDetailsRequest)
        {
            ZZB_CustomerDetailsResponse customerDetailsResponse = new ZZB_CustomerDetailsResponse
            {
                type = VAMessageType.ZZB_CLINET_CUSTOMER_DETAILS_RESPONSE,
                cookie = customerDetailsRequest.cookie,
                uuid = customerDetailsRequest.uuid
            };

            CheckCookieAndMsgForZZZ checkResult = Common.CheckCookieAndMsgForZZZ(customerDetailsRequest.cookie, (int)customerDetailsRequest.type, (int)VAMessageType.ZZB_CLINET_CUSTOMER_DETAILS_REQUEST, customerDetailsRequest.shopId);
            if (checkResult.result == VAResult.VA_OK)
            {
                int employeeId = Common.ToInt32(checkResult.dtEmployee.Rows[0]["EmployeeID"]);
                bool isViewAllocWorker = Common.ToBool(checkResult.dtEmployee.Rows[0]["isViewAllocWorker"]);
                if (ServiceFactory.Resolve<IShopAuthorityService>().GetEmployeeHasShopAuthority(customerDetailsRequest.shopId, employeeId, isViewAllocWorker, ShopRole.客户信息.GetString()))
                {
                    IPreOrder19DianInfoRepository preOrder19DianInfoRepository =
                        ServiceFactory.Resolve<IPreOrder19DianInfoRepository>();

                    ICustomerInfoRepository customerInfoRepository = ServiceFactory.Resolve<ICustomerInfoRepository>();

                    try
                    {
                        var customer = customerInfoRepository.GetById(customerDetailsRequest.customerId);

                        customerDetailsResponse.custtomerName = customer.UserName;
                        customerDetailsResponse.mobilePhoneNumber = customer.mobilePhoneNumber;
                        customerDetailsResponse.preOrderTotalAmount = customer.preOrderTotalAmount.HasValue ? Common.ToDouble(customer.preOrderTotalAmount.Value) : 0;
                        customerDetailsResponse.registerDate = customer.RegisterDate.HasValue ? customer.RegisterDate.Value.ToString("yyyy.MM.dd") : "1970.01.01";
                        customerDetailsResponse.sex = (short)(customer.CustomerSex.HasValue ? customer.CustomerSex.Value : 0);


                        var orders = preOrder19DianInfoRepository.GetPageCustomerPayforOrders(
                            new Page(1, 10), customerDetailsRequest.customerId);

                        customerDetailsResponse.preOrders = (from preOrderForShop in orders
                                                             select new PreOrderLog
                                                             {
                                                                 shopName = preOrderForShop.shopName,
                                                                 preOrderDate = preOrderForShop.preOrderTime.ToString("yyyy.MM.dd"),
                                                                 preOrderAmount = FormatAmount(preOrderForShop.prePaidSum),
                                                                 shopLogo =
                                                                     WebConfig.CdnDomain + WebConfig.ImagePath + preOrderForShop.shopImagePath +
                                                                     preOrderForShop.shopLogo,
                                                                 dishs = string.IsNullOrEmpty(preOrderForShop.orderInJson) ? new string[0] :
                                                                       JsonOperate.JsonDeserialize<List<PreOrderIn19dian>>(preOrderForShop.orderInJson)
                                                                           .Select(p => p.dishName)
                                                                           .ToArray()
                                                             }).ToList();

                        customerDetailsResponse.nextPage = orders.HasNextPage;
                        customerDetailsResponse.result = VAResult.VA_OK;
                    }
                    catch (Exception)
                    {
                        customerDetailsResponse.result = VAResult.VA_FAILED_DB_ERROR;
                    }

                }
                else
                {
                    customerDetailsResponse.result = VAResult.VA_NO_ACCESS_INTERFACE_AUTHORITY;
                }
            }
            else
            {
                customerDetailsResponse.result = checkResult.result;
            }

            return customerDetailsResponse;
        }

        private string FormatAmount(double amount)
        {
            if (amount < 300)
            {
                return "<￥300.00";
            }
            else if (amount <= 1000)
            {
                return ">￥300.00";
            }
            else
            {
                return ">￥1000.00";
            }
        }

        public ZZB_PreOrderList2Response ZZBClientQueryPreOrderList2(ZZB_VAPreOrderListRequest preOrderListRequest)
        {
            ZZB_PreOrderList2Response preOrderList2Response = new ZZB_PreOrderList2Response
            {
                type = VAMessageType.ZZB_CLIENT_PREORDERLIST2_RESPONSE,
                cookie = preOrderListRequest.cookie,
                uuid = preOrderListRequest.uuid
            };

            CheckCookieAndMsgForZZZ checkResult = Common.CheckCookieAndMsgForZZZ(preOrderListRequest.cookie, (int)preOrderListRequest.type, (int)VAMessageType.ZZB_CLIENT_PREORDERLIST2_REQUEST, preOrderListRequest.shopId);
            if (checkResult.result == VAResult.VA_OK)
            {
                try
                {
                    string path = WebConfig.CdnDomain + WebConfig.ImagePath + WebConfig.CustomerPicturePath;

                    var orders = PreOrderCacheLogic.GetVAServiceAllOrderList(new Page(1, Int32.MaxValue), preOrderListRequest.shopId);
                    preOrderList2Response.preOrderList = (from shopPreOrder in orders
                                                          //where shopPreOrder.shopId == preOrderListRequest.shopId//过滤只获取当前门店
                                                          select new PreOrderList2Model
                                                          {
                                                              customerUserName = shopPreOrder.UserName,
                                                              customerPicture = string.IsNullOrEmpty(shopPreOrder.Picture) ? "" : path + shopPreOrder.RegisterDate.ToString("yyyyMM/") + shopPreOrder.Picture,
                                                              deskNumber = shopPreOrder.deskNumber,
                                                              mobilePhoneNumber = shopPreOrder.mobilePhoneNumber,
                                                              hadInvoice = !string.IsNullOrEmpty(shopPreOrder.invoiceTitle),
                                                              hadRefund = shopPreOrder.refundMoneySum > 0,
                                                              hadShared = shopPreOrder.Shared > 0,
                                                              hadSeat = shopPreOrder.isShopConfirmed == 1,
                                                              preOrder19dianId = shopPreOrder.preOrder19dianId,
                                                              preOrderTotalQuantity = shopPreOrder.PreOrderTotalQuantity,
                                                              prepaidTime = shopPreOrder.prePayTime.ToString("yyyy.MM.dd"),
                                                              beforeDeductionPrice = shopPreOrder.beforeDeductionPrice,
                                                              afterDeductionPrice = shopPreOrder.afterDeductionPrice,
                                                              hadCoupon = shopPreOrder.hadCoupon > 0
                                                          }).ToList();

                    preOrderList2Response.result = VAResult.VA_OK;
                }
                catch (Exception)
                {
                    preOrderList2Response.result = VAResult.VA_FAILED_DB_ERROR;
                }
            }
            else
            {
                preOrderList2Response.result = checkResult.result;
            }

            return preOrderList2Response;
        }

        public ZZB_ModifyDeskNumberResponse ModifyDeskNumber(ZZB_ModifyDeskNumberRequest modifyDeskNumberRequest)
        {
            ZZB_ModifyDeskNumberResponse modifyDeskNumberResponse = new ZZB_ModifyDeskNumberResponse
            {
                type = VAMessageType.ZZB_CLINET_MODIFY_DESK_NUMBER_RESPONSE,
                cookie = modifyDeskNumberRequest.cookie,
                uuid = modifyDeskNumberRequest.uuid
            };

            CheckCookieAndMsgForZZZ checkResult = Common.CheckCookieAndMsgForZZZ(modifyDeskNumberRequest.cookie, (int)modifyDeskNumberRequest.type, (int)VAMessageType.ZZB_CLINET_MODIFY_DESK_NUMBER_REQUEST);
            if (checkResult.result == VAResult.VA_OK)
            {
                try
                {
                    IPreOrder19DianInfoRepository preOrder19DianInfoRepository =
                        ServiceFactory.Resolve<IPreOrder19DianInfoRepository>();
                    preOrder19DianInfoRepository.UpdateOrderDeskNumber(modifyDeskNumberRequest.preOrder19dianId, modifyDeskNumberRequest.deskNumber);


                    modifyDeskNumberResponse.result = VAResult.VA_OK;
                }
                catch (Exception)
                {
                    modifyDeskNumberResponse.result = VAResult.VA_FAILED_DB_ERROR;
                }
            }
            else
            {
                modifyDeskNumberResponse.result = checkResult.result;
            }

            return modifyDeskNumberResponse;
        }
        /// <summary>
        /// 客户端查询门店评价详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ZZBClientShopPreorderListResponse ZZBQueryShopEvaluationList(ZZBClientShopPreorderListRequest request)
        {
            ZZBClientShopPreorderListResponse response = new ZZBClientShopPreorderListResponse();
            response.type = VAMessageType.ZZB_CLIENT_SHOP_PREORDERLIST_RESPONSE;
            response.cookie = request.cookie;
            response.uuid = request.uuid;
            response.cityId = request.cityId;
            CheckCookieAndMsgForZZZ checkResult = Common.CheckCookieAndMsgForZZZ(request.cookie, (int)request.type, (int)VAMessageType.ZZB_CLIENT_SHOP_PREORDERLIST_REQUEST, request.shopId);
            if (checkResult.result == VAResult.VA_OK)
            {
                int employeeId = Common.ToInt32(checkResult.dtEmployee.Rows[0]["EmployeeID"]);
                bool isViewAllocWorker = Common.ToBool(checkResult.dtEmployee.Rows[0]["isViewAllocWorker"]);
                //AuthorityManager authorityMan = new AuthorityManager();
                if (!ServiceFactory.Resolve<IShopAuthorityService>().GetEmployeeHasShopAuthority(request.shopId, employeeId, isViewAllocWorker, ShopRole.用户评价信息.GetString()))
                {
                    response.result = VAResult.VA_NO_ACCESS_INTERFACE_AUTHORITY;
                    return response;
                }
                VA.CacheLogic.OrderClient.ShopInfoCacheLogic shopInfoCacheLogic = new VA.CacheLogic.OrderClient.ShopInfoCacheLogic();
                ShopInfo shopInfo = shopInfoCacheLogic.GetShopInfo(request.shopId);
                if (shopInfo != null)
                {
                    response.shopLevel = shopInfo.shopLevel;
                    response.hasSold = shopInfo.prepayOrderCount;
                    ShopEvaluationDetailManager shopEvaluationDetailManager = new ShopEvaluationDetailManager();
                    ShopEvaluationDetailQueryObject shopEvaluationDetailQueryObject = new Model.ShopEvaluationDetailQueryObject()
                    {
                        ShopId = request.shopId
                    };
                    List<ShopEvaluationDetail> shopEvaluationDetailList =
                        shopEvaluationDetailManager.GetShopEvaluationDetailByQuery(shopEvaluationDetailQueryObject);
                    if (shopEvaluationDetailList != null)
                    {
                        int shopScore = shopEvaluationDetailList.Sum(p => p.EvaluationValue * p.EvaluationCount);
                        response.shopScore = shopScore;
                        if (response.shopScore < 0)
                        {
                            response.shopScore = 0;
                        }
                        if (shopInfo.shopLevel <= 0)
                        {
                            shopInfo.shopLevel = 1;
                        }
                        //计算下一级别分数
                        //levelList :标志物升级对应的门店等级
                        int[] levelList = new int[] { 1, 2, 3, 4, 5, 6, 12, 18, 24, 30, 36, 72, 108, 144, 180 };
                        int nextLevel = levelList.Where(p => p > shopInfo.shopLevel).Min();
                        response.upgradeScore = 15 * nextLevel * nextLevel - 5 * nextLevel - response.shopScore;
                        double totalEvaluation = shopEvaluationDetailList.Sum(p => p.EvaluationCount);
                        var evaluationPercent = from e in shopEvaluationDetailList
                                                group e by e.EvaluationValue into g
                                                select new EvaluationPercent()
                                                {
                                                    evaluationValue = g.Key,
                                                    percent = Math.Round((g.Sum(p => p.EvaluationCount) / totalEvaluation), 4)
                                                };
                        if (evaluationPercent != null && evaluationPercent.Count() >= 0)
                        {
                            response.evaluationPercent = evaluationPercent.ToList();
                        }
                        if (evaluationPercent.Count() < 3)
                        {
                            for (int i = -1; i < 2; i++)
                            {
                                EvaluationPercent entity = evaluationPercent.FirstOrDefault(p => p.evaluationValue == i);
                                if (entity == null)
                                {
                                    entity = new EvaluationPercent() { evaluationValue = i, percent = 0 };
                                    response.evaluationPercent.Add(entity);
                                }
                            }
                        }
                    }
                    if (request.pageIndex > 0)
                    {
                        #region 加载店铺评价
                        PreorderEvaluationQueryObject queryObject = new PreorderEvaluationQueryObject() { ShopId = shopInfo.shopID };
                        long queryCount = PreorderEvaluationOperate.GetCountByQuery(queryObject);
                        if (queryCount > request.pageIndex * request.pageSize)
                        {
                            response.isMore = true;
                        }
                        else
                        {
                            response.isMore = false;
                        }
                        List<PreorderEvaluation> preOrder19dianInfoList
                            = PreorderEvaluationOperate.GetListByQuery(request.pageSize, request.pageIndex, queryObject);
                        if (preOrder19dianInfoList != null && preOrder19dianInfoList.Count > 0)
                        {
                            var returnEvaluationList = from e in preOrder19dianInfoList
                                                       select new evaluationDetail()
                                                       {
                                                           preOrder19dianId = e.PreOrder19dianId,
                                                           customId = e.CustomerId,
                                                           evaluationContent = e.EvaluationContent,
                                                           evaluationValue = e.EvaluationLevel,
                                                           evaluationTime = Common.ToSecondFrom1970(e.EvaluationTime)
                                                       };
                            response.evaluationList = returnEvaluationList.ToList();
                            CustomerManager customerManager = new CustomerManager();
                            DataTable dtCustomer = null;
                            foreach (var item in response.evaluationList)
                            {
                                item.mobilePhoneNumber = string.Empty;
                                item.customName = string.Empty;
                                dtCustomer = customerManager.SelectCustomer(item.customId);
                                if (dtCustomer != null && dtCustomer.Rows.Count > 0)
                                {
                                    string mobilePhoneNumber = dtCustomer.Rows[0]["mobilePhoneNumber"].ToString();
                                    item.customName = dtCustomer.Rows[0]["UserName"].ToString();
                                    if (mobilePhoneNumber.Length > 10)
                                    {
                                        item.mobilePhoneNumber = dtCustomer.Rows[0]["mobilePhoneNumber"].ToString();
                                    }
                                }
                            }

                        }

                        #endregion
                    }
                    response.helpUrl = WebConfig.ServerDomain + "AppPages/levelHelp.html";
                    response.result = VAResult.VA_OK;
                }
                else
                {
                    response.result = checkResult.result;
                }
            }
            return response;
        }

        public ZZB_PreOrderListResponse ZZBClientQueryPreOrderList(ZZB_VAPreOrderListRequest preOrderListRequest)
        {
            ZZB_PreOrderListResponse preOrderListResponse = new ZZB_PreOrderListResponse
            {
                type = VAMessageType.ZZB_CLIENT_PREORDERLIST_RESPONSE,
                cookie = preOrderListRequest.cookie,
                uuid = preOrderListRequest.uuid
            };

            CheckCookieAndMsgForZZZ checkResult = Common.CheckCookieAndMsgForZZZ(preOrderListRequest.cookie, (int)preOrderListRequest.type, (int)VAMessageType.ZZB_CLIENT_PREORDERLIST_REQUEST, preOrderListRequest.shopId);
            if (checkResult.result == VAResult.VA_OK)
            {
                try
                {
                    bool isNew = Common.UXServiceCheckLatestBuild_201507(preOrderListRequest.serviceType, preOrderListRequest.clientBuild);//判断是否是201507新版本

                    var orders = PreOrderCacheLogic.GetVAServiceOrderList(preOrderListRequest.shopId, isNew);//抓出所有未对账点单

                    if (orders != null && orders.Any())
                    {
                        if (isNew)//新版本要处理订单
                        {
                            SqlServerPreOrder19DianInfoManager preOrder19dianManager = new SqlServerPreOrder19DianInfoManager();

                            List<PreOrderListModel> preOrderListModelList = new List<PreOrderListModel>();
                            foreach (PreOrderList item in orders)
                            {
                                OrderPaidDetail orderPaidDetail = preOrder19dianManager.GetOrderPaid(item.orderId);
                                PreOrderListModel preOrderListModel = new PreOrderListModel();
                                preOrderListModel.afterDeductionPrice = Math.Round(orderPaidDetail.PreOrderServerSum - orderPaidDetail.VerifiedSaving, 2);
                                preOrderListModel.beforeDeductionPrice = Math.Round(orderPaidDetail.PrePaidSum + orderPaidDetail.VerifiedSaving, 2);
                                preOrderListModel.customerUserName = item.UserName;
                                preOrderListModel.hadSeat = item.isShopConfirmed == 1;
                                preOrderListModel.mobilePhoneNumber = item.mobilePhoneNumber;
                                preOrderListModel.orderId = item.orderId;
                                preOrderListModel.preOrder19dianId = item.preOrder19dianId; // add by zhujinlei 2015/07/07
                                preOrderListModel.prepaidTime = item.prePayTime.ToString("yyyy.MM.dd");

                                preOrderListModelList.Add(preOrderListModel);
                            }
                            preOrderListResponse.preOrderList = preOrderListModelList;
                        }
                        else
                        {
                            preOrderListResponse.preOrderList = (from shopPreOrder in orders
                                                                 select new PreOrderListModel
                                                                 {
                                                                     preOrder19dianId = shopPreOrder.preOrder19dianId,
                                                                     customerUserName = shopPreOrder.UserName,
                                                                     mobilePhoneNumber = shopPreOrder.mobilePhoneNumber,
                                                                     hadSeat = shopPreOrder.isShopConfirmed == 1,
                                                                     prepaidTime = shopPreOrder.prePayTime.ToString("yyyy.MM.dd"),
                                                                     afterDeductionPrice = shopPreOrder.afterDeductionPrice,
                                                                     beforeDeductionPrice = shopPreOrder.beforeDeductionPrice
                                                                 }).ToList();

                            //// 老版本的列表的显示的原价需要加上抵扣券的金额
                            //foreach (var objOrder in preOrderListResponse.preOrderList)
                            //{
                            //    double[] amoDoubles = new CouponOperate().GetCouponDeductibleAmount(objOrder.preOrder19dianId);
                            //    var deductionPrice = amoDoubles[2];
                            //    objOrder.afterDeductionPrice = objOrder.afterDeductionPrice + deductionPrice;
                            //}
                        }
                    }

                    preOrderListResponse.result = VAResult.VA_OK;
                }
                catch (Exception)
                {
                    preOrderListResponse.result = VAResult.VA_FAILED_DB_ERROR;
                }
            }
            else
            {
                preOrderListResponse.result = checkResult.result;
            }

            return preOrderListResponse;
        }
        /// <summary>
        /// 悠先服务查询指定点单的附加信息
        /// </summary>
        /// <param name="preOrderListAttachRequest"></param>
        /// <returns></returns>
        public ZZB_PreOrderListAttachResponse ZZBClientQueryPreOrderListAttach(ZZB_PreOrderListAttachRequest preOrderListAttachRequest)
        {
            ZZB_PreOrderListAttachResponse preOrderListAttachResponse = new ZZB_PreOrderListAttachResponse
            {
                type = VAMessageType.ZZB_CLIENT_PREORDERLIST_ATTACG_RESPONSE,
                cookie = preOrderListAttachRequest.cookie,
                uuid = preOrderListAttachRequest.uuid
            };

            CheckCookieAndMsgForZZZ checkResult = Common.CheckCookieAndMsgForZZZ(preOrderListAttachRequest.cookie, (int)preOrderListAttachRequest.type, (int)VAMessageType.ZZB_CLIENT_PREORDERLIST_ATTACG_REQUEST, preOrderListAttachRequest.shopId);
            if (checkResult.result == VAResult.VA_OK)
            {
                try
                {
                    string path = WebConfig.CdnDomain + WebConfig.ImagePath + WebConfig.CustomerPicturePath;

                    SqlServerPreOrder19DianInfoManager preOrder19DianInfoManager = new SqlServerPreOrder19DianInfoManager();

                    bool isNew = Common.UXServiceCheckLatestBuild_201507(preOrderListAttachRequest.serviceType, preOrderListAttachRequest.clientBuild);//判断是否是201507新版本


                    //------------------------------------------------------------------------------------------------------------
                    //AwardConnPreOrderOperate awardOperate = new AwardConnPreOrderOperate();
                    //List<long> awardOrderId = awardOperate.SelectAwardPreOrderByOrderIds(preOrderListAttachRequest.preOrder19dianId);

                    ////------------------------------------------------------------------------------------------------------------

                    var orders = new List<PreOrderListAttach>();
                    List<long> share = null;
                    List<long> coupon = null;

                    if (isNew)
                    {
                        orders = preOrder19DianInfoManager.GetUnApprovedShopOrdersAttachByOrderId(preOrderListAttachRequest.orderId);

                        long[] preOrder19dianIds = new long[orders.Count];
                        for (int i = 0; i < orders.Count; i++)
                        {
                            preOrder19dianIds[i] = orders[i].preOrder19dianId;
                        }
                        share = preOrder19DianInfoManager.GetPreOrderSharedInfo(preOrder19dianIds);
                        coupon = preOrder19DianInfoManager.GetPreOrderCouponInfo(preOrder19dianIds);
                    }
                    else
                    {
                        orders = preOrder19DianInfoManager.GetUnApprovedShopOrdersAttach(preOrderListAttachRequest.preOrder19dianId);

                        share = preOrder19DianInfoManager.GetPreOrderSharedInfo(preOrderListAttachRequest.preOrder19dianId);
                        coupon = preOrder19DianInfoManager.GetPreOrderCouponInfo(preOrderListAttachRequest.preOrder19dianId);
                    }

                    preOrderListAttachResponse.preOrderListAttachInfo = (from attach in orders
                                                                         select new PreOrderListAttachInfo
                                                                         {
                                                                             customerPicture = string.IsNullOrEmpty(attach.Picture) ? "" : path + attach.RegisterDate.ToString("yyyyMM/") + attach.Picture,
                                                                             deskNumber = attach.deskNumber,
                                                                             hadCoupon = coupon == null ? false : (coupon.Contains(attach.preOrder19dianId) ? true : false),
                                                                             hadShared = share == null ? false : (share.Contains(attach.preOrder19dianId) ? true : false),
                                                                             hadInvoice = string.IsNullOrEmpty(attach.invoiceTitle) ? false : true,
                                                                             hadRefund = attach.refundMoneySum > 0,
                                                                             preOrder19dianId = attach.preOrder19dianId,
                                                                             preOrderTotalQuantity = attach.PreOrderTotalQuantity,
                                                                             mobilePhoneNumber = attach.mobilePhoneNumber,
                                                                             hadSeat = attach.isShopConfirmed == 1,
                                                                             orderId = attach.orderId,

                                                                             //------------------------------------------------------------------------------------------------------------

                                                                             //avoidQueue = awardOrderId == null ? false : (awardOrderId.Contains(attach.preOrder19dianId) ? true : false)

                                                                             //------------------------------------------------------------------------------------------------------------
                                                                         }).ToList();

                    preOrderListAttachResponse.result = VAResult.VA_OK;
                }
                catch (Exception)
                {
                    preOrderListAttachResponse.result = VAResult.VA_FAILED_DB_ERROR;
                }
            }
            else
            {
                preOrderListAttachResponse.result = checkResult.result;
            }

            return preOrderListAttachResponse;
        }


        #region ----------------------------------------------
        /// <summary>
        /// 统计活动列表权限
        /// </summary>
        /// <param name="countingListRequest"></param>
        /// <returns></returns>
        public CountingListResponse SelectAwardTotalRole(CountingListRequest countingListRequest)
        {
            CountingListResponse response = new CountingListResponse
            {
                type = VAMessageType.COUNTING_LIST_RESPONSE,
                cookie = countingListRequest.cookie,
                uuid = countingListRequest.uuid
            };

            //[isViewAllocWorker]
            CheckCookieAndMsgForZZZ checkResult = Common.CheckCookieAndMsgForZZZ(countingListRequest.cookie, (int)countingListRequest.type, (int)VAMessageType.COUNTING_LIST_REQUEST, countingListRequest.shopId);
            if (checkResult.result == VAResult.VA_OK)
            {
                int employeeId = Common.ToInt32(checkResult.dtEmployee.Rows[0]["EmployeeID"]);
                bool isViewAllocWorker = Common.ToBool(checkResult.dtEmployee.Rows[0]["isViewAllocWorker"]);

                try
                {
                    var shopAuthorityService = ServiceFactory.Resolve<IShopAuthorityService>();
                    response.roles =
                        (from a in
                             shopAuthorityService.GetShopAwardTotalAuthorities(countingListRequest.shopId, employeeId,
                                 isViewAllocWorker)
                         orderby a.ShopAuthoritySequence
                         select
                             new ShopHaveAuthority
                             {
                                 authorityCode = a.AuthorityCode,
                                 authorityName = a.ShopAuthorityName,
                                 isClientShow = a.IsClientShow
                             }).ToList();
                    response.result = VAResult.VA_OK;
                }
                catch (Exception)
                {
                    response.result = VAResult.VA_FAILED_DB_ERROR;
                }

            }
            else
            {
                response.result = checkResult.result;
            }
            return response;
        }
        #endregion
    }
}
