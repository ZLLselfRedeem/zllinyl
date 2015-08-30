using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.Model;

namespace VAGastronomistMobileApp.SQLServerDAL.Persistence
{
    public interface ITenpayRefundOrderRepository
    {
        void Add(TenpayRefundOrder tenpayRefundOrder);

        TenpayRefundOrder GetTenpayRefundOrderByOutTradeNo(long orderId);

        TenpayRefundOrder GeTenpayRefundOrderByRefundIdP(string refundId);

        void Update(TenpayRefundOrder tenpayRefundOrder);

        IEnumerable<TenpayRefundOrder> GetProcessingenpayRefundOrders();
        [Obsolete]
        TenpayRefundOrder GetTenpayRefundOrderByOrder(long orderId);

        TenpayRefundOrder GeTenpayRefundOrderByOriginalRoadRefundInfo(long originalRoadRefundInfoId);
    }
}
