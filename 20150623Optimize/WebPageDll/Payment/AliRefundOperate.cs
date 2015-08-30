using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.SQLServerDAL;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.TheThirdPartyPaymentDll;
using System.Xml;
using System.Data;
using LogDll;

namespace VAGastronomistMobileApp.WebPageDll
{
    /// <summary>
    /// 支付宝退款业务逻辑层
    /// 创建日期：2014-6-20
    /// </summary>
    public class AliRefundOperate
    {
        private readonly AliRefundManager aliRefundManager = new AliRefundManager();

        /// <summary>
        /// 新增支付宝退款记录
        /// </summary>
        /// <param name="refundOrder"></param>
        /// <returns></returns>
        public long InsertAliRefundOrder(AliRefundOrderInfo refundOrder)
        {
            return aliRefundManager.InsertAliRefundOrder(refundOrder);
        }

        /// <summary>
        /// 收到异步通知时，更新支付宝退款记录
        /// </summary>
        /// <param name="refundOrder"></param>
        /// <returns></returns>
        public bool UpdateAliRefundOrder(AliRefundOrderInfo refundOrder)
        {
            return aliRefundManager.UpdateAliRefundOrder(refundOrder);
        }

        /// <summary>
        /// 更新退款单状态为退款成功
        /// </summary>
        /// <param name="batchNo"></param>
        /// <returns></returns>
        public bool UpdateAliRefundStatus(string batchNo)
        {
            return aliRefundManager.UpdateAliRefundStatus(batchNo);
        }

        /// <summary>
        /// 根据batchNo查询还未收到通知的退款单据
        /// </summary>
        /// <param name="batchNo"></param>
        /// <returns></returns>
        public DataTable QueryAliRefund(string batchNo)
        {
            return aliRefundManager.QueryAliRefund(batchNo);
        }

        /// <summary>
        /// 根据支付宝交易号查询所有处于退款中的退款单据
        /// </summary>
        /// <param name="tradeNo"></param>
        /// <returns></returns>
        public DataTable QueryAliRefundByTradeNo(string tradeNo)
        {
            return aliRefundManager.QueryAliRefundByTradeNo(tradeNo);
        }

        /// <summary>
        /// 根据原路退款流水号查询支付宝退款记录
        /// </summary>
        /// <param name="originalId"></param>
        /// <returns></returns>
        public DataTable QueryAliRefundByOriginalId(long originalId)
        {
            return aliRefundManager.QueryAliRefundByOriginalId(originalId);
        }

          /// <summary>
        /// 根据原路退款流水号查询改订单我方交易号
        /// </summary>
        /// <param name="originalId"></param>
        /// <returns></returns>
        public string QueryTradeNo(long originalId)
        {
            return aliRefundManager.QueryTradeNo(originalId);
        }

        /// <summary>
        /// 查询所有支付宝退款单据（已通知，未打款）
        /// </summary>
        /// <returns></returns>
        public DataTable QueryAllRefundOrder()
        {
            return aliRefundManager.QueryAllRefundOrder();
        }

        /// <summary>
        /// 提交支付宝退款申请
        /// </summary>
        /// <param name="refundOrder"></param>
        /// <returns></returns>
        public bool AliRefundRequest(AliRefundOrderInfo refundOrder)
        {
            //单笔数据集，格式：原付款支付宝交易号^退款总金额^退款理由
            string detail_data = refundOrder.aliTradeNo + "^" + refundOrder.refundSum + "^" + refundOrder.refundReason;

            //把请求参数打包成数组
            SortedDictionary<string, string> sParaTemp = new SortedDictionary<string, string>();
            sParaTemp.Add("partner", Config.Partner);
            sParaTemp.Add("_input_charset", Config.RefundInputCharset.ToLower());
            sParaTemp.Add("service", "refund_fastpay_by_platform_nopwd");
            sParaTemp.Add("notify_url", Config.RefundNotifyUrl);
            sParaTemp.Add("batch_no", refundOrder.batchNo);
            sParaTemp.Add("refund_date", refundOrder.refundDate.ToString("yyyy-MM-dd HH:mm:ss"));
            sParaTemp.Add("batch_num", refundOrder.batchNum.ToString());
            sParaTemp.Add("detail_data", detail_data);

            //LogManager.WriteLog(LogFile.Trace, DateTime.Now.ToString() + Config.RefundNotifyUrl);

            //建立请求
            string sHtmlText = Submit.BuildRequest(sParaTemp);

            XmlDocument xmlDoc = new XmlDocument();
            try
            {
                xmlDoc.LoadXml(sHtmlText);
                string strXmlResponse = xmlDoc.SelectSingleNode("/alipay").InnerText;
                if (strXmlResponse.Equals("T"))//申请请求成功
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
