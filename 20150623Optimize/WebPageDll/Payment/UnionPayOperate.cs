using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Configuration;
using System.Transactions;
using System.Threading;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL;

namespace VAGastronomistMobileApp.WebPageDll
{
    public class UnionPayOperate
    {
        private readonly UnionPayManager payMan = new UnionPayManager();

        /// <summary>
        /// 增加银联订单
        /// </summary>
        /// <param name="alipayOrder"></param>
        /// <returns></returns>
        public long AddUnionpayOrder(UnionPayInfo payOrder)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                long payOrderId = payMan.InsertUnionpayOrder(payOrder);
                if (payOrderId > 0)
                {
                    scope.Complete();
                    return payOrderId;
                }
                else
                {
                    return 0;
                }
            }
        }

        /// <summary>
        /// 修改订单状态
        /// </summary>
        public bool ModifyUnionpayOrderStatus(int orderStatus, DateTime orderPayTime, long unionPayOrderId, string cupsQid)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                if (payMan.UpdateUnionpayOrderStatus(orderStatus, orderPayTime,unionPayOrderId,cupsQid))
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
        /// 查询是否存在未支付的银联订单
        /// </summary>
        /// <param name="unionPayOrderId"></param>
        /// <returns></returns>
        public DataTable QueryUnionPayOrder(long unionPayOrderId)
        {
            return payMan.SelectunionPayOrder(unionPayOrderId);
        }

    }
}
