using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.SQLServerDAL;
using System.Data;
using VAGastronomistMobileApp.Model;
using System.Threading;
using VAGastronomistMobileApp.WebPageDll.ThreadCallBacks;

namespace VAGastronomistMobileApp.WebPageDll
{
    /// <summary>
    /// 处理重复支付订单
    /// </summary>
    public class RepeatPayOperate
    {
        /// <summary>
        /// 根据客户电话号码查询其重复支付的点单
        /// </summary>
        /// <param name="customerPhoneNumber"></param>
        /// <returns></returns>
        public DataTable QueryRepeatedPay(string customerPhoneNumber)
        {
            WechatPayManager wechatPayManager = new WechatPayManager();
            return wechatPayManager.QueryRepeatedPay(customerPhoneNumber);
        }

        /// <summary>
        /// 重复支付订单申请退款
        /// </summary>
        /// <param name="connId">点单流水号</param>
        /// <param name="originalRoadRefund"></param>
        public void RepeatPayRefund(long connId, OriginalRoadRefundInfo originalRoadRefund)
        {
            PreOrder19dianOperate preOrder19dianOperate = new PreOrder19dianOperate();
            PreOrder19dianManager preOrder19dianManager = new PreOrder19dianManager();
            ThirdPartyPaymentInfo thirdPartyPaymentInfo = preOrder19dianManager.SelectRepeatPreorderPayAmount(Common.ToInt64(originalRoadRefund.tradeNo));//该点单使用第三方支付的金额

            long originalRoadRefundId = preOrder19dianOperate.InsertOriginalRoadRefund(originalRoadRefund);
            if (originalRoadRefundId > 0)
            {
                originalRoadRefund.id = originalRoadRefundId;
                ThreadPool.QueueUserWorkItem(
                    new RefundCallBack(connId, thirdPartyPaymentInfo,
                        (float)originalRoadRefund.refundAmount, originalRoadRefund).Refund);
            }
        }
    }
}
