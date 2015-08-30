using Autofac;
using log4net;
using System;
using System.Transactions;
using System.Web;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL.Persistence;
using VAGastronomistMobileApp.TheThirdPartyPaymentDll;
using VAGastronomistMobileApp.WebPageDll.Services.Infrastructure;
using VAGastronomistMobileApp.WebPageDll;
using System.Data;

namespace TenpayRefundCheck
{
    public class TimerCallback : ICallback
    {
        private readonly ILog _log;

        public TimerCallback(ILog log)
        {
            _log = log;
            //this._repositoryContext = repositoryContext;
        }

        public IContainer Container { get; set; }

        public void Run(object state)
        {
            try
            {
                if (_log.IsInfoEnabled)
                {
                    _log.Info("开始工作!");
                }
                #region 微信退款进度查询
                var refundOrders = Container.Resolve<ITenpayRefundOrderRepository>().GetProcessingenpayRefundOrders();
                foreach (var tenpayRefundOrder in refundOrders)
                {
                    try
                    {
                        using (var cscope = Container.BeginLifetimeScope())
                        {

                            var originalRoadRefundInfoRepository = cscope.Resolve<IOriginalRoadRefundInfoRepository>();
                            var tenpayRefundOrderRepository = cscope.Resolve<ITenpayRefundOrderRepository>();
                            var wechatPayOrderRepository = cscope.Resolve<IWechatPayOrderInfoRepository>();

                            var originalRoadRefundInfo =
                                originalRoadRefundInfoRepository.GetOriginalRoadRefundInfoById(
                                    tenpayRefundOrder.OriginalRoadRefundInfoId);

                            if (originalRoadRefundInfo != null)
                            {


                                int[] array = Call(tenpayRefundOrder.OutRefundId);

                                if (array.Length > 0)
                                {
                                    originalRoadRefundInfo.remittanceTime = new DateTime(1970, 1, 1);
                                    originalRoadRefundInfo.note = "";
                                    switch (array[0])
                                    {
                                        case 4:
                                        case 10:
                                            tenpayRefundOrder.Status = TenpayRefundStatus.退款成功;
                                            tenpayRefundOrder.ChangeStatusTime = DateTime.Now;
                                            originalRoadRefundInfo.status = 2;
                                            originalRoadRefundInfo.remittanceTime = DateTime.Now;
                                            originalRoadRefundInfo.note = "微信自动打款";
                                            break;
                                        case 3:
                                        case 5:
                                        case 6:
                                            tenpayRefundOrder.Status = TenpayRefundStatus.退款失败;
                                            tenpayRefundOrder.ChangeStatusTime = DateTime.Now;
                                            originalRoadRefundInfo.status = 4;
                                            break;
                                        case 8:
                                        case 9:
                                        case 11:
                                            tenpayRefundOrder.Status = TenpayRefundStatus.退款处理中;
                                            tenpayRefundOrder.ChangeStatusTime = DateTime.Now;
                                            //修改原路退款状态
                                            break;
                                        case 1:
                                        case 2:
                                            tenpayRefundOrder.Status = TenpayRefundStatus.未确定;
                                            tenpayRefundOrder.ChangeStatusTime = DateTime.Now;
                                            originalRoadRefundInfo.status = 4;
                                            break;
                                        case 7:
                                            tenpayRefundOrder.Status = TenpayRefundStatus.转入代发;
                                            tenpayRefundOrder.ChangeStatusTime = DateTime.Now;
                                            originalRoadRefundInfo.status = 4;
                                            break;
                                            ;
                                        default:
                                            tenpayRefundOrder.Status = TenpayRefundStatus.退款失败;
                                            tenpayRefundOrder.ChangeStatusTime = DateTime.Now;
                                            originalRoadRefundInfo.status = 4;
                                            break;
                                    }
                                    if (tenpayRefundOrder.Status != TenpayRefundStatus.退款处理中)
                                    {

                                        using (var scope = new TransactionScope())
                                        {
                                            tenpayRefundOrderRepository.Update(tenpayRefundOrder);
                                            originalRoadRefundInfoRepository.Update(originalRoadRefundInfo);
                                            if (tenpayRefundOrder.Status == TenpayRefundStatus.退款成功)
                                            {
                                                if (originalRoadRefundInfo.type == VAOriginalRefundType.PREORDER)
                                                {
                                                    IPreOrder19DianInfoRepository preOrder19DianInfoRepository = cscope.Resolve<IPreOrder19DianInfoRepository>();
                                                    var orderinfo = preOrder19DianInfoRepository.GetById(tenpayRefundOrder.PreOrder19dianId);

                                                    if (orderinfo != null)
                                                    {
                                                        double prepaidSum = Convert.ToDouble(orderinfo.prePaidSum);
                                                        double refundAmount = Convert.ToDouble(tenpayRefundOrder.RefundFee);
                                                        double refundMoneyClosedSum = Convert.ToDouble(orderinfo.refundMoneyClosedSum);

                                                        if ((prepaidSum - Convert.ToDouble(refundAmount + refundMoneyClosedSum)) < 0.001)
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
                                                    wechatPayOrderRepository.UpdateOrderStatus(originalRoadRefundInfo.tradeNo, VAAlipayOrderStatus.REFUNDED);        
                                                }
                                            }
                                            scope.Complete();
                                        }
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception exc)
                    {
                        if (_log.IsErrorEnabled)
                            _log.Error(exc.Message, exc);
                    }
                }
                #endregion
                #region 支付宝退款进度查询
                AliSingleTradeQuery aliSingleTradeQuery = new AliSingleTradeQuery();
                AliRefundOperate aliRefundOperate = new AliRefundOperate();

                DataTable dtAliRefundOrder = aliRefundOperate.QueryAllRefundOrder();
                if (dtAliRefundOrder != null)
                {
                    if (_log.IsInfoEnabled)
                    {
                        _log.Info(DateTime.Now.ToString() + "-----------------------------支付宝退款进度查询开始-----------------------------");
                    }
                    using (var cscope = Container.BeginLifetimeScope())
                    {
                        var repositoryContext = cscope.Resolve<IRepositoryContext>();
                        var originalRoadRefundInfoRepository = cscope.Resolve<IOriginalRoadRefundInfoRepository>();

                        foreach (DataRow dr in dtAliRefundOrder.Rows)
                        {
                            long originalId = Common.ToInt64(dr["originalId"].ToString());
                            long connId = Common.ToInt64(dr["connId"].ToString());
                            
                            var originalRoadRefundInfo =
                                originalRoadRefundInfoRepository.GetOriginalRoadRefundInfoById(originalId);

                            string result = aliSingleTradeQuery.SingleTradeQueryResquest(originalRoadRefundInfo, repositoryContext);

                            if (_log.IsInfoEnabled)
                            {
                                _log.Info(DateTime.Now.ToString() + "--originalId:" + originalId + "--connId:" + connId + "--result:" + result);
                            }
                        }
                    }
                    if (_log.IsInfoEnabled)
                    {
                        _log.Info(DateTime.Now.ToString() + "-----------------------------支付宝退款进度查询结束-----------------------------");
                    }
                }
                #endregion
                if (_log.IsInfoEnabled)
                {
                    _log.Info("结束工作!");
                }
            }
            catch (Exception exc)
            {
                if (_log.IsErrorEnabled)
                    _log.Error(exc.Message, exc);
            }
        }

        private int[] Call(string outRefundId)
        {
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
            reqHandler.setGateUrl("https://gw.tenpay.com/gateway/normalrefundquery.xml");


            reqHandler.setParameter("partner", WechatPayConfig.partnerId);
            reqHandler.setParameter("out_refund_no", outRefundId);
            string requestUrl = reqHandler.getRequestURL();
            httpClient.SetCertInfo(WechatPayConfig.CertInfo, WechatPayConfig.CertPasswd);
            //设置请求内容
            httpClient.SetReqContent(requestUrl);
            //设置超时
            httpClient.SetTimeOut(10);

            string rescontent = "";
            //后台调用
            if (httpClient.Call())
            {
                //获取结果
                rescontent = httpClient.GetResContent();

                resHandler.setKey(WechatPayConfig.partnerKey);
                //设置结果参数
                resHandler.setContent(rescontent);

                //判断签名及结果
                if (resHandler.isTenpaySign() && resHandler.getParameter("retcode") == "0")
                {
                    if (_log.IsInfoEnabled)
                    {
                        _log.Info(rescontent);
                    }

                    int refund_count = int.Parse(resHandler.getParameter("refund_count"));
                    int[] refund_state_array = new int[refund_count];
                    for (int i = 0; i < refund_count; i++)
                    {
                        int refund_state = int.Parse(resHandler.getParameter("refund_state_" + i));
                        refund_state_array[i] = refund_state;
                    }
                    return refund_state_array;
                }
            }
            return new int[0];
        }
    }
}
