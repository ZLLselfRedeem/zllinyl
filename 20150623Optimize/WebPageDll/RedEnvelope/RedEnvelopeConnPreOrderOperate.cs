using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.SQLServerDAL;
using System.Data;

namespace VAGastronomistMobileApp.WebPageDll
{
    /// <summary>
    /// 红包与点单关联关系业务逻辑
    /// </summary>
    public class RedEnvelopeConnPreOrderOperate
    {
        private readonly IRedEnvelopeConnPreOrderManager manager;

        public RedEnvelopeConnPreOrderOperate()
        {
            manager = new RedEnvelopeConnPreOrderManager();
        }
        /// <summary>
        /// 查询当前点单使用红包总金额
        /// </summary>
        /// <param name="preOrder19DianId"></param>
        /// <returns></returns>
        public double GetPayOrderConsumeRedEnvelopeAmount(long preOrder19DianId)
        {
            return manager.GetPayOrderConsumeRedEnvelopeAmount(preOrder19DianId);
        }

        /// <summary>
        /// 根据红包Id查看其使用情况（支付过哪些点单）
        /// </summary>
        /// <param name="redEnvelopeId"></param>
        /// <returns></returns>
        public DataTable QueryRedEnvelopeConnPreOrder(long redEnvelopeId)
        {
            return manager.SelectRedEnvelopeConnPreOrder(redEnvelopeId);
        }

        /// <summary>
        /// 批量插入点单使用红包关联
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public bool BatchInsertRedEnvelopeConnPreOrder(DataTable dt)
        {
            return manager.BatchInsertRedEnvelopeConnPreOrder(dt);
        }

        /// <summary>
        /// 根据用户cookie查询红包过期导致订单过期的订单号
        /// </summary>
        /// <param name="cookie"></param>
        /// <returns></returns>
        public List<long> QueryExpirePreOrder(string cookie)
        {
            return manager.QueryExpirePreOrder(cookie);
        }
    }
}
