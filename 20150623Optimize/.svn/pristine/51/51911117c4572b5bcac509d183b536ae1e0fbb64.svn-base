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
    /// <summary>
    /// 支付宝相关操作类
    /// </summary>
    public class AlipayOperate
    {
        private readonly AlipayManager alipayMan = new AlipayManager();
        /// <summary>
        /// 增加支付宝订单
        /// </summary>
        /// <param name="alipayOrder"></param>
        /// <returns></returns>
        public long AddAlipayOrder(AlipayOrderInfo alipayOrder)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                long alipayOrderId = alipayMan.InsertAlipayOrder(alipayOrder);
                if (alipayOrderId > 0)
                {
                    scope.Complete();
                    return alipayOrderId;
                }
                else
                {
                    return 0;
                }
            }
        }
        /// <summary>
        /// 修改支付宝订单
        /// </summary>
        /// <param name="alipayOrder"></param>
        /// <returns></returns>
        public bool ModifyAlipayOrder(AlipayOrderInfo alipayOrder)
        {
            if (alipayMan.UpdateAlipayOrder(alipayOrder))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 更新第三方订单的状态
        /// </summary>
        /// <param name="outTradeNo"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public bool UpdateOrderStatus(string outTradeNo, VAAlipayOrderStatus status)
        {
            return alipayMan.UpdateOrderStatus(outTradeNo, status);
        }
        /// <summary>
        /// 查询是否存在未支付的支付宝订单
        /// </summary>
        /// <param name="alipayOrderId"></param>
        /// <returns></returns>
        public DataTable QueryAlipayOrder(long alipayOrderId)
        {
            return alipayMan.SelectAlipayOrder(alipayOrderId);
        }

        /// <summary>
        /// 根据点单Id查询相应的支付宝订单信息
        /// </summary>
        /// <param name="connId">点单Id</param>
        /// <returns></returns>
        public DataTable QueryAlipayOrderByConnId(long connId)
        {
            return alipayMan.SelectAlipayOrderByConnId(connId);
        }

        /// <summary>
        /// 根据我方交易号查询支付订单
        /// </summary>
        /// <param name="alipayOrderId"></param>
        /// <returns></returns>
        public DataTable QueryAlipayOrderById(long alipayOrderId)
        {
            return alipayMan.SelectAlipayOrderById(alipayOrderId);
        }
    }
}
