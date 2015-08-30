using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.Model;

namespace VAGastronomistMobileApp.SQLServerDAL.Persistence
{
    public interface IWechatPayOrderInfoRepository
    {
        WechatPayOrderInfo GetWechatPayOrderInfoByOrderId(long orderId);

        WechatPayOrderInfo GetWechatPayOrderInfoByOutTradeNo(long outTradeNo);

        bool UpdateOrderStatus(string outTradeNo, VAAlipayOrderStatus status);
    }
}
