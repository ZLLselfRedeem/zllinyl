using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.Model;

namespace VAGastronomistMobileApp.SQLServerDAL.Persistence
{
    public interface IOriginalRoadRefundInfoRepository
    {
        OriginalRoadRefundInfo GetOriginalRoadRefundInfoById(long id);

        void Update(OriginalRoadRefundInfo originalRoadRefundInfo);

        OriginalRoadRefundInfo GetOriginalRoadRefundInfoByOrderId(long orderId);
        
    }
}
