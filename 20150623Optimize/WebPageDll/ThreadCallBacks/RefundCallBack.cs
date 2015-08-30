using Autofac;
using Autofac.Integration.Web;
using LogDll;
using System;
using System.Transactions;
using System.Web;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL.Persistence;
using VAGastronomistMobileApp.TheThirdPartyPaymentDll;
using VAGastronomistMobileApp.WebPageDll.Services.Infrastructure;
using System.Data;

namespace VAGastronomistMobileApp.WebPageDll.ThreadCallBacks
{
    public class RefundCallBack
    {
        private long preOrder19dianId;
        private ThirdPartyPaymentInfo thirdPartyPaymentInfo;
        private float payAmount;
        private OriginalRoadRefundInfo originalRoadRefund;
        //private string url;
        public RefundCallBack(long preOrder19DianId, ThirdPartyPaymentInfo thirdPartyPaymentInfo, float payAmount, OriginalRoadRefundInfo originalRoadRefund)
        {
            preOrder19dianId = preOrder19DianId;
            this.thirdPartyPaymentInfo = thirdPartyPaymentInfo;
            this.payAmount = payAmount;
            this.originalRoadRefund = originalRoadRefund;
        }

        //public RefundCallBack(long preOrder19DianId, ThirdPartyPaymentInfo thirdPartyPaymentInfo, float payAmount, OriginalRoadRefundInfo originalRoadRefund, string url)
        //    : this(preOrder19DianId, thirdPartyPaymentInfo, payAmount, originalRoadRefund)
        //{
        //    this.url = url;
        //}

        public void Refund(object state)
        {
            try
            {
                //这里加入退款流程
                switch (thirdPartyPaymentInfo.Type)
                {
                    case PayType.微信支付:
                        WechatRefund();
                        break;
                    case PayType.支付宝:
                        AliRefund();
                        break;
                    default:
                        break;
                }
            }
            catch (Exception exc)
            {
                LogManager.WriteLog(LogFile.Error, DateTime.Now.ToString() + "msg:" + exc.ToString());
            }
        }

        private void WechatRefund()
        {
            using (var cscope = Bootstrapper.Container.BeginLifetimeScope(WebLifetime.Request))
            {
                IWechatPayOrderInfoRepository wechatPayOrderinfoRepository = cscope.Resolve<IWechatPayOrderInfoRepository>();
                //var wechatPayOrderInfo =
                //    wechatPayOrderinfoRepository.GetWechatPayOrderInfoByOrderId(preOrder19dianId);
                var wechatPayOrderInfo = wechatPayOrderinfoRepository.GetWechatPayOrderInfoByOutTradeNo(Common.ToInt64(originalRoadRefund.tradeNo));
                if (wechatPayOrderInfo != null)
                {
                    var tenpayRefundOrderRepository = cscope.Resolve<ITenpayRefundOrderRepository>();
                    IOriginalRoadRefundInfoRepository originalRoadRefundInfoRepository = cscope.Resolve<IOriginalRoadRefundInfoRepository>();
                    TenpayRefundOrder tenpayRefundOrder =
                        tenpayRefundOrderRepository.GeTenpayRefundOrderByOriginalRoadRefundInfo(
                            originalRoadRefund.id);

                    bool isInsert = false;
                    if (tenpayRefundOrder == null)
                    {
                        tenpayRefundOrder = new TenpayRefundOrder()
                        {
                            PreOrder19dianId = preOrder19dianId,
                            CretaeTime = DateTime.Now,
                            OpUserId = WechatPayConfig.opUserId,
                            OutTradeNo = wechatPayOrderInfo.outTradeNo,
                            OutRefundId = Guid.NewGuid().ToString("N"),
                            RefundFee = payAmount,
                            WechatPrePayId = wechatPayOrderInfo.wechatPrePayId,
                            OriginalRoadRefundInfoId = originalRoadRefund.id
                        };
                        isInsert = true;
                    }

                    //创建请求对象
                    RequestHandler reqHandler = new RequestHandler(HttpContext.Current);

                    //通信对象
                    TenpayHttpClient httpClient = new TenpayHttpClient();

                    //应答对象
                    ClientResponseHandler resHandler = new ClientResponseHandler();

                    //-----------------------------
                    //设置请求参数
                    //-----------------------------
                    reqHandler.init();
                    reqHandler.setKey(WechatPayConfig.partnerKey);
                    reqHandler.setGateUrl("https://mch.tenpay.com/refundapi/gateway/refund.xml");

                    reqHandler.setParameter("partner", WechatPayConfig.partnerId);
                    //out_trade_no和transaction_id至少一个必填，同时存在时transaction_id悠先
                    reqHandler.setParameter("out_trade_no",
                        WebConfig.WechatPrefix + tenpayRefundOrder.OutTradeNo); //待查加
                    //reqHandler.setParameter("transaction_id", tenpayRefundOrder.WechatPrePayId);//<28
                    reqHandler.setParameter("out_refund_no", tenpayRefundOrder.OutRefundId); //待组合
                    reqHandler.setParameter("total_fee",
                        (Common.ToInt32(thirdPartyPaymentInfo.Amount * 100)).ToString());
                    reqHandler.setParameter("refund_fee",
                        (Common.ToInt32(tenpayRefundOrder.RefundFee * 100)).ToString());
                    //reqHandler.setParameter("refund_fee", "1");
                    reqHandler.setParameter("op_user_id", WechatPayConfig.opUserId);
                    reqHandler.setParameter("op_user_passwd",
                        MD5Util.GetMD5(WechatPayConfig.opUserPasswd, "GBK"));
                    reqHandler.setParameter("service_version", "1.1");

                    //reqHandler.setParameter("notify_url", "http://112.124.0.220:8080/clientRefundCallback.aspx");

                    string requestUrl = reqHandler.getRequestURL();
                    LogManager.WriteLog(LogFile.Trace, DateTime.Now.ToString() + "msg:" + requestUrl);

                    httpClient.SetCertInfo(WechatPayConfig.CertInfo, WechatPayConfig.CertPasswd);
                    //设置请求内容
                    httpClient.SetReqContent(requestUrl);
                    //设置超时
                    httpClient.SetTimeOut(10);

                    string rescontent = "";
                    if (httpClient.Call())
                    {
                        //获取结果
                        rescontent = httpClient.GetResContent();

                        LogManager.WriteLog(LogFile.Trace, DateTime.Now.ToString() + "msg:" + rescontent);

                        resHandler.setKey(WechatPayConfig.partnerKey);
                        //设置结果参数
                        resHandler.setContent(rescontent);

                        //判断签名及结果
                        if (resHandler.isTenpaySign() &&
                            resHandler.getParameter("retcode") == "0")
                        {
                            tenpayRefundOrder.OutRefundId =
                                resHandler.getParameter("out_refund_no");
                            TenpayRefundChannel tenpayRefundChannel;
                            TenpayRefundChannel.TryParse(
                                resHandler.getParameter("refund_channel"),
                                out tenpayRefundChannel);
                            tenpayRefundOrder.RefundChannel = tenpayRefundChannel;
                            tenpayRefundOrder.ReccvUserName = resHandler.getParameter("reccv_user_name");
                            tenpayRefundOrder.RecvUserId = resHandler.getParameter("recv_user_id");
                            tenpayRefundOrder.RefundId = resHandler.getParameter("refund_id");
                            int status = 0;
                            if (int.TryParse(resHandler.getParameter("refund_status"),
                                out status))
                            {
                                originalRoadRefund.remittanceTime = new DateTime(1970, 1, 1);
                                originalRoadRefund.note = "";
                                switch (status)
                                {
                                    case 4:
                                    case 10:
                                        tenpayRefundOrder.Status = TenpayRefundStatus.退款成功;
                                        originalRoadRefund.status = (int)VAOriginalRefundStatus.REMITTED;
                                        originalRoadRefund.remittanceTime = DateTime.Now;
                                        originalRoadRefund.note = "微信自动打款";
                                        //修改原路退款状态
                                        break;
                                    case 3:
                                    case 5:
                                    case 6:
                                        tenpayRefundOrder.Status = TenpayRefundStatus.退款失败;
                                        break;
                                    case 8:
                                    case 9:
                                    case 11:
                                        tenpayRefundOrder.Status = TenpayRefundStatus.退款处理中;
                                        originalRoadRefund.status = (int)VAOriginalRefundStatus.Processing;
                                        //修改原路退款状态
                                        break;
                                    case 1:
                                    case 2:
                                        tenpayRefundOrder.Status = TenpayRefundStatus.未确定;
                                        break;
                                    case 7:
                                        tenpayRefundOrder.Status = TenpayRefundStatus.转入代发;
                                        break;
                                        ;
                                    default:
                                        tenpayRefundOrder.Status = TenpayRefundStatus.退款失败;
                                        break;
                                }
                            }
                            else
                            {
                                LogManager.WriteLog(LogFile.Error, DateTime.Now.ToString() + "msg:" + "微信退款失败");
                                tenpayRefundOrder.Status = TenpayRefundStatus.退款失败;
                            }


                        }
                        else
                        {
                            LogManager.WriteLog(LogFile.Error, DateTime.Now.ToString() + "msg:" + "未签名");
                            tenpayRefundOrder.Status = TenpayRefundStatus.退款失败;
                        }

                    }
                    else
                    {
                        LogManager.WriteLog(LogFile.Error,
                            DateTime.Now.ToString() + "msg:" + "网络访问失败" + httpClient.GetErrInfo());
                        tenpayRefundOrder.Status = TenpayRefundStatus.退款失败;
                    }
                    using (var scope = new TransactionScope())
                    {
                        if (tenpayRefundOrder.Status == TenpayRefundStatus.退款成功 ||
                            tenpayRefundOrder.Status == TenpayRefundStatus.退款处理中)
                        {
                            originalRoadRefundInfoRepository.Update(originalRoadRefund);

                            if (originalRoadRefund.type == VAOriginalRefundType.REPEAT_PREORDER)
                            {
                                wechatPayOrderinfoRepository.UpdateOrderStatus(originalRoadRefund.tradeNo, VAAlipayOrderStatus.REPEAT_REFUNDING);                               
                            }
                        }

                        if (tenpayRefundOrder.Status == TenpayRefundStatus.退款成功)
                        {
                            if (originalRoadRefund.type == VAOriginalRefundType.PREORDER)
                            {
                                IPreOrder19DianInfoRepository preOrder19DianInfoRepository = cscope.Resolve<IPreOrder19DianInfoRepository>();
                                var orderinfo = preOrder19DianInfoRepository.GetById(tenpayRefundOrder.PreOrder19dianId);

                                if (orderinfo != null)
                                {
                                    double prepaidSum = Common.ToDouble(orderinfo.prePaidSum);
                                    double refundAmount = Common.ToDouble(tenpayRefundOrder.RefundFee);
                                    double refundMoneyClosedSum = Common.ToDouble(orderinfo.refundMoneyClosedSum);

                                    if ((prepaidSum - Common.ToDouble(refundAmount + refundMoneyClosedSum)) < 0.01)
                                    {
                                        preOrder19DianInfoRepository.UpdateOrderRefundStatusAndMoney(tenpayRefundOrder.PreOrder19dianId, tenpayRefundOrder.RefundFee, (int)VAPreorderStatus.Refund);
                                    }
                                    else
                                    {
                                        preOrder19DianInfoRepository.UpdateOrderRefundMoney(tenpayRefundOrder.PreOrder19dianId, tenpayRefundOrder.RefundFee);
                                    }
                                }
                            }
                            else
                            {
                                wechatPayOrderinfoRepository.UpdateOrderStatus(originalRoadRefund.tradeNo, VAAlipayOrderStatus.REFUNDED);                               
                            }
                        }

                        if (isInsert)
                        {
                            tenpayRefundOrderRepository.Add(tenpayRefundOrder);
                        }
                        else
                        {
                            tenpayRefundOrder.ChangeStatusTime = DateTime.Now;
                            tenpayRefundOrderRepository.Update(tenpayRefundOrder);
                        }
                        scope.Complete();
                    }
                }
            }
        }

        private void AliRefund()
        {

            AliRefundOperate aliRefundOperate = new AliRefundOperate();
            AlipayOperate alipayOperate = new AlipayOperate();
            DataTable dtAliPay = new DataTable();
            //dtAliPay = alipayOperate.QueryAlipayOrderByConnId(preOrder19dianId); 
            dtAliPay = alipayOperate.QueryAlipayOrderById(Common.ToInt64(originalRoadRefund.tradeNo));
            if (dtAliPay != null && dtAliPay.Rows.Count > 0)
            {
                AliRefundOrderInfo aliRefundOrderInfo = new AliRefundOrderInfo();
                aliRefundOrderInfo.customerId = Common.ToInt64(dtAliPay.Rows[0]["customerId"]);
                //aliRefundOrderInfo.batchNo = DateTime.Now.ToString("yyyyMMddHHmmssfff") + aliRefundOrderInfo.customerId + Common.randomStrAndNum(4) + WebConfig.WechatPrefix;
                aliRefundOrderInfo.batchNo = DateTime.Now.ToString("yyyyMMdd") + aliRefundOrderInfo.customerId + Common.randomStrAndNum(4) + WebConfig.WechatPrefix;
                aliRefundOrderInfo.refundDate = DateTime.Now;
                aliRefundOrderInfo.batchNum = 1;
                aliRefundOrderInfo.refundSum = payAmount;
                aliRefundOrderInfo.refundReason = "协商退款";
                aliRefundOrderInfo.refundStatus = (int)VAAliRefundStatus.REFUNDING;
                aliRefundOrderInfo.aliTradeNo = dtAliPay.Rows[0]["aliTradeNo"].ToString();
                aliRefundOrderInfo.connId = originalRoadRefund.connId;
                aliRefundOrderInfo.originalId = originalRoadRefund.id;

                long refundId = aliRefundOperate.InsertAliRefundOrder(aliRefundOrderInfo);
                if (refundId > 0)//成功将此退款记录新增到我方DB
                {
                    bool request = aliRefundOperate.AliRefundRequest(aliRefundOrderInfo);
                    if (request)
                    {
                        CustomerServiceOperateLogOperate customerServiceOperateLogOperate = new CustomerServiceOperateLogOperate();
                        bool updateOriginalStatus = customerServiceOperateLogOperate.UpdateOriginalRoadRefundApply(0, "", originalRoadRefund.id, (int)VAOriginalRefundStatus.Processing);
                       
                        LogManager.WriteLog(LogFile.Trace, DateTime.Now.ToString() + "支付宝退款申请成功refundId：" + refundId);
                        LogManager.WriteLog(LogFile.Trace, DateTime.Now.ToString() + "原路退款单据状态修改结果：" + updateOriginalStatus);

                        if (originalRoadRefund.type == VAOriginalRefundType.REPEAT_PREORDER)
                        {
                            bool updatePayOrderStatus = alipayOperate.UpdateOrderStatus(originalRoadRefund.tradeNo, VAAlipayOrderStatus.REPEAT_REFUNDING);
                            LogManager.WriteLog(LogFile.Trace, DateTime.Now.ToString() + "支付订单状态修改结果：" + updatePayOrderStatus);
                        }
                    }
                    else
                    {
                        LogManager.WriteLog(LogFile.Trace, DateTime.Now.ToString() + "支付宝退款申请失败refundId：" + refundId);
                    }
                }
                else
                {
                    LogManager.WriteLog(LogFile.Trace, DateTime.Now.ToString() + "支付宝退款新增退款记录失败");
                }
            }
        }

    }
}
