using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.TheThirdPartyPaymentDll;
using System.Xml;
using System.Data;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL.Persistence;
using VAGastronomistMobileApp.WebPageDll.Services.Infrastructure;
using System.Transactions;
using LogDll;
using VAGastronomistMobileApp.SQLServerDAL;

namespace VAGastronomistMobileApp.WebPageDll
{
    /// <summary>
    /// 支付宝单笔交易查询接口
    /// </summary>
    public class AliSingleTradeQuery
    {
        /// <summary>
        /// 根据支付宝交易号查询订单交易信息（此处主要处理退款相关）
        /// </summary>
        /// <param name="originalId">原路退款流水号</param>
        public string SingleTradeQueryResquest(OriginalRoadRefundInfo originalRoadRefundInfo,IRepositoryContext repositoryContext)
        {
            string result = "";

            PreOrder19dianOperate preOrder19dianOperate = new PreOrder19dianOperate();
            AliRefundOperate aliRefundOperate = new AliRefundOperate();
            CustomerServiceOperateLogOperate customerOperate = new CustomerServiceOperateLogOperate();
            try
            {
                DataTable dtAliRefundInfo = aliRefundOperate.QueryAliRefundByOriginalId(originalRoadRefundInfo.id);
                if (dtAliRefundInfo != null && dtAliRefundInfo.Rows.Count > 0)
                {
                    string aliTradeNo = dtAliRefundInfo.Rows[0]["aliTradeNo"].ToString();//支付宝交易号
                    long preOrder19dianId = Common.ToInt64(dtAliRefundInfo.Rows[0]["connId"]);//点单流水号

                    #region 调用查询接口并处理接口
                    //把请求参数打包成数组
                    SortedDictionary<string, string> sParaTemp = new SortedDictionary<string, string>();
                    sParaTemp.Add("partner", Config.Partner);
                    sParaTemp.Add("_input_charset", Config.RefundInputCharset.ToLower());
                    sParaTemp.Add("service", "single_trade_query");
                    sParaTemp.Add("trade_no", aliTradeNo);
                    sParaTemp.Add("out_trade_no", "");

                    LogManager.WriteLog(LogFile.Trace, DateTime.Now.ToString() + "------------------------single_trade_query------------------------");
                    LogManager.WriteLog(LogFile.Trace, DateTime.Now.ToString() + "aliTradeNo:" + aliTradeNo);

                    //建立请求
                    string sHtmlText = Submit.BuildRequest(sParaTemp);

                    XmlDocument xmlDoc = new XmlDocument();

                    xmlDoc.LoadXml(sHtmlText);
                    string is_success = xmlDoc.SelectSingleNode("/alipay/is_success").InnerText;
                    if (is_success == "T")//查询成功
                    {
                        LogManager.WriteLog(LogFile.Trace, DateTime.Now.ToString() + "查询成功，以下是查询出的参数");

                        double refund_fee = Common.ToDouble(xmlDoc.SelectSingleNode("/alipay/response/trade/refund_fee").InnerText);//退款金额
                        string refund_status = xmlDoc.SelectSingleNode("/alipay/response/trade/refund_status").InnerText;//退款状态
                        double to_buyer_fee = Common.ToDouble(xmlDoc.SelectSingleNode("/alipay/response/trade/to_buyer_fee").InnerText);//累计的已经退款金额
                        double to_seller_fee = Common.ToDouble(xmlDoc.SelectSingleNode("/alipay/response/trade/to_seller_fee").InnerText);//累计的已打款给卖家的金额
                        string trade_no = xmlDoc.SelectSingleNode("/alipay/response/trade/trade_no").InnerText;//支付宝交易号

                        LogManager.WriteLog(LogFile.Trace, DateTime.Now.ToString() + "refund_fee:" + refund_fee);
                        LogManager.WriteLog(LogFile.Trace, DateTime.Now.ToString() + "refund_status:" + refund_status);
                        LogManager.WriteLog(LogFile.Trace, DateTime.Now.ToString() + "to_buyer_fee:" + to_buyer_fee);
                        LogManager.WriteLog(LogFile.Trace, DateTime.Now.ToString() + "to_seller_fee:" + to_seller_fee);
                        LogManager.WriteLog(LogFile.Trace, DateTime.Now.ToString() + "trade_no:" + trade_no);

                        #region 处理查询结果
                        PreOrder19dianManager preorder19dianMan = new PreOrder19dianManager();
                        ThirdPartyPaymentInfo thirdPartyPaymentInfo = preorder19dianMan.SelectPreorderPayAmount(preOrder19dianId);
                        double thirdPartySum = thirdPartyPaymentInfo.Amount;//该点单使用第三方支付的金额

                        //检查查询结果是否匹配
                        if (trade_no == aliTradeNo && to_seller_fee - thirdPartySum < 0.001)
                        {
                            LogManager.WriteLog(LogFile.Trace, DateTime.Now.ToString() + "查询出的结果与DB数据匹配");

                            switch (originalRoadRefundInfo.type)
                            {
                                case VAOriginalRefundType.PREORDER:
                                    #region
                                    //查询此单据申请的所有未完结并且收到异步通知的退款单据
                                    //数据按照退款金额降序，退款时间升序的方式排序
                                    DataTable dtAliRefund = aliRefundOperate.QueryAliRefundByTradeNo(aliTradeNo);
                                    if (dtAliRefund != null && dtAliRefund.Rows.Count > 0)
                                    {
                                        LogManager.WriteLog(LogFile.Trace, DateTime.Now.ToString() + "DB中找到所有符合条件的退款点单" + dtAliRefund.Rows.Count + "条");

                                        foreach (DataRow dr in dtAliRefund.Rows)
                                        {
                                            #region 检查每一笔点单并处理
                                            string batchNo = dr["batchNo"].ToString();//退款批次号
                                            double refundSum = Common.ToDouble(dr["refundSum"].ToString());//当次退款金额
                                            long original_id = Common.ToInt64(dr["originalId"].ToString());//原路退款ID

                                            DataTable dtOrder = preOrder19dianOperate.QueryPreOrderById(preOrder19dianId);
                                            if (dtOrder != null && dtOrder.Rows.Count > 0)
                                            {
                                                double prePaidSum = Common.ToDouble(dtOrder.Rows[0]["prePaidSum"]);//点单总金额
                                                double refundMoneySum = Common.ToDouble(dtOrder.Rows[0]["refundMoneySum"]);//申请退款总金额
                                                double refundMoneyClosedSum = Common.ToDouble(dtOrder.Rows[0]["refundMoneyClosedSum"]);//已完结的退款金额

                                                LogManager.WriteLog(LogFile.Trace, DateTime.Now.ToString() + "DB--refundSum:" + refundSum);
                                                LogManager.WriteLog(LogFile.Trace, DateTime.Now.ToString() + "DB--prePaidSum:" + prePaidSum);
                                                LogManager.WriteLog(LogFile.Trace, DateTime.Now.ToString() + "DB--refundMoneySum:" + refundMoneySum);
                                                LogManager.WriteLog(LogFile.Trace, DateTime.Now.ToString() + "DB--refundMoneyClosedSum:" + refundMoneyClosedSum);
                                                LogManager.WriteLog(LogFile.Trace, DateTime.Now.ToString() + "DB--thirdPartySum:" + thirdPartySum);

                                                LogManager.WriteLog(LogFile.Trace, DateTime.Now.ToString() + "待更新batchNo:" + batchNo);

                                                //判断是第三方支付还是 第三方+粮票
                                                if (prePaidSum - thirdPartySum < 0.001)//全是第三方
                                                {
                                                    if (refundSum + refundMoneyClosedSum - to_buyer_fee < 0.001)//此退款单的退款金额+已完结的退款金额 不超过 查询出的累计第三方退款金额
                                                    {
                                                        UpdateOrderStatus(preOrder19dianId, prePaidSum, refundMoneyClosedSum, batchNo, refundSum, original_id, repositoryContext);

                                                        result = "处理完毕";
                                                    }
                                                    else
                                                    {
                                                        //不做任何处理，检查下一条
                                                        result = "已处理完毕";
                                                    }
                                                }
                                                else//第三方+粮票
                                                {
                                                    double refundedFoodCoupon = 0;

                                                    if (refundMoneySum - thirdPartySum > 0.001)//第三方的钱退光了
                                                    {
                                                        refundedFoodCoupon = refundMoneySum - thirdPartySum;//已经退给粮票的钱
                                                    }
                                                    else
                                                    {
                                                        //refundedFoodCoupon 还是0
                                                    }

                                                    if (refundSum + refundMoneyClosedSum - refundedFoodCoupon - to_buyer_fee < 0.001)//此退款单的退款金额+已完结的退款金额-已退款的粮票金额 不超过 查询出的累计第三方退款金额
                                                    {
                                                        UpdateOrderStatus(preOrder19dianId, prePaidSum, refundMoneyClosedSum, batchNo, refundSum, original_id, repositoryContext);

                                                        result = "处理完毕";
                                                    }
                                                    else
                                                    {
                                                        //不做任何处理，检查下一条
                                                        result = "已处理完毕";
                                                    }
                                                }
                                                LogManager.WriteLog(LogFile.Trace, DateTime.Now.ToString() + "batchNo处理完毕:" + batchNo);
                                            }
                                            else
                                            {
                                                result = "未找到相应点单";
                                            }
                                            #endregion
                                        }
                                    }
                                    else
                                    {
                                        result = "此点单没有未完结的退款";
                                    }
                                    #endregion
                                    break;
                                case VAOriginalRefundType.REPEAT_PREORDER:
                                    DataTable dtAliRefundRepeat = aliRefundOperate.QueryAliRefundByTradeNo(aliTradeNo);
                                    if (dtAliRefundRepeat != null && dtAliRefundRepeat.Rows.Count > 0)
                                    {
                                        string batchNo = dtAliRefundRepeat.Rows[0]["batchNo"].ToString();//退款批次号
                                        UpdateRepeatOrderStatus(batchNo, originalRoadRefundInfo.id, originalRoadRefundInfo.tradeNo);
                                    }
                                    break;
                                default:
                                    break;
                            }                            
                        }
                        else
                        {
                            result = "查询结果不匹配（支付宝交易号，第三方支付金额）";
                        }
                        #endregion
                    }
                    else
                    {
                        result = "调用查询接口失败";
                    }
                    #endregion
                }
                else
                {
                    result = "支付宝退款记录中没有找到相应记录";
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            LogManager.WriteLog(LogFile.Trace, DateTime.Now.ToString() + "------------------------" + result + "------------------------");
            return result;
        }

        private static void UpdateOrderStatus(long preOrder19dianId, double prePaidSum, double refundMoneyClosedSum, string batchNo, double refundSum, long original_id, IRepositoryContext repositoryContext)
        {
            try
            {
                AliRefundOperate aliRefundOperate = new AliRefundOperate();
                CustomerServiceOperateLogOperate customerOperate = new CustomerServiceOperateLogOperate();
                //IPreOrder19DianInfoRepository preOrder19DianInfoRepository = ServiceFactory.Resolve<IPreOrder19DianInfoRepository>();
                IPreOrder19DianInfoRepository preOrder19DianInfoRepository = repositoryContext.GetPreOrder19DianInfoRepository();

                using (TransactionScope ts = new TransactionScope())
                {
                    //更新此单的支付宝退款记录
                    bool updateAliRefundOrder = aliRefundOperate.UpdateAliRefundStatus(batchNo);

                    LogManager.WriteLog(LogFile.Trace, DateTime.Now.ToString() + "updateAliRefundOrder结果:" + updateAliRefundOrder);

                    //更新此单的原路退款记录
                    bool updateOriginalRefund = customerOperate.UpdateOriginalRoadRefundApply(0, "支付宝自动打款", original_id, (int)VAOriginalRefundStatus.REMITTED);

                    LogManager.WriteLog(LogFile.Trace, DateTime.Now.ToString() + "updateOriginalRefund结果:" + updateOriginalRefund);

                    //如果当次退款完成后就是全额退款，则更新点单状态为已退款，反之不更新状态
                    if ((prePaidSum - Common.ToDouble(refundSum + refundMoneyClosedSum)) < 0.001)
                    {
                        preOrder19DianInfoRepository.UpdateOrderRefundStatusAndMoney(preOrder19dianId, Common.ToDouble(refundSum), (int)VAPreorderStatus.Refund);
                        LogManager.WriteLog(LogFile.Trace, DateTime.Now.ToString() + "UpdateOrderRefundStatusAndMoney");
                    }
                    else
                    {
                        preOrder19DianInfoRepository.UpdateOrderRefundMoney(preOrder19dianId, Common.ToDouble(refundSum));
                        LogManager.WriteLog(LogFile.Trace, DateTime.Now.ToString() + "UpdateOrderRefundMoney");
                    }
                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog(LogFile.Trace, DateTime.Now.ToString() + "Exception:" + ex.Message);
            }
        }

        private static void UpdateRepeatOrderStatus(string batchNo, long original_id, string outTradeNo)
        {
            try
            {
                AliRefundOperate aliRefundOperate = new AliRefundOperate();
                CustomerServiceOperateLogOperate customerOperate = new CustomerServiceOperateLogOperate();
                AlipayOperate alipayOperate = new AlipayOperate();

                using (TransactionScope ts = new TransactionScope())
                {
                    //更新此单的支付宝退款记录
                    bool updateAliRefundOrder = aliRefundOperate.UpdateAliRefundStatus(batchNo);

                    LogManager.WriteLog(LogFile.Trace, DateTime.Now.ToString() + "updateAliRefundOrder结果:" + updateAliRefundOrder);

                    //更新此单的原路退款记录
                    bool updateOriginalRefund = customerOperate.UpdateOriginalRoadRefundApply(0, "支付宝自动打款", original_id, (int)VAOriginalRefundStatus.REMITTED);

                    LogManager.WriteLog(LogFile.Trace, DateTime.Now.ToString() + "updateOriginalRefund结果:" + updateOriginalRefund);

                    //重复支付单据退款
                    bool updateStatus = alipayOperate.UpdateOrderStatus(outTradeNo, VAAlipayOrderStatus.REFUNDED);

                    LogManager.WriteLog(LogFile.Trace, DateTime.Now.ToString() + "UpdateRepeatOrderStatus结果:" + updateStatus);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog(LogFile.Trace, DateTime.Now.ToString() + "Exception:" + ex.Message);
            }
        }
    }
}
