using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.SQLServerDAL;
using VAGastronomistMobileApp.Model;
using System.Data;

namespace VAGastronomistMobileApp.WebPageDll
{
    /// <summary>
    /// 客服操作业务逻辑
    /// </summary>

    public class CustomerServiceOperateLogOperate
    {
        protected CustomerServiceOperateLogManager customerServiceOperateLogManager = new CustomerServiceOperateLogManager();
        /// <summary>
        /// 增加客服日志信息
        /// </summary>
        /// <param name="customerServiceOperateLogInfo"></param>
        /// <returns></returns>
        public int InsertCustomerServiceOperateLog(CustomerServiceOperateLogInfo customerServiceOperateLogInfo)
        {
            return customerServiceOperateLogManager.InsertCustomerServiceOperateLogInfo(customerServiceOperateLogInfo);
        }
        /// <summary>
        /// 查询客服操作日志
        /// </summary>
        /// <returns></returns>
        public DataTable QueryCustomerServiceOperateLog(int preOrderId, string operateName, string strTime, string endTime)
        {
            return customerServiceOperateLogManager.SelectCustomerServiceOperateLog(preOrderId, operateName, strTime, endTime);
        }
        /// <summary>
        /// 查询客服操作日志(overload)
        /// </summary>
        /// <returns></returns>
        public DataTable QueryCustomerServiceOperateLog(int type)
        {
            return customerServiceOperateLogManager.SelectCustomerServiceOperateLog(type);
        }
        /// <summary>
        /// 查询所有原路返回申请信息（wangcheng）
        /// </summary>
        /// <param name="connId"></param>
        /// <param name="customerMobilephone"></param>
        /// <param name="strTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public DataTable QueryOriginalRoadRefundApply(long connId, string customerMobilephone, string strTime, string endTime, int status, int type)
        {
            return customerServiceOperateLogManager.SelectOriginalRoadRefundApply(connId, customerMobilephone, strTime, endTime, status, type);
        }
        /// <summary>
        /// 更新原路退款日志表信息（wangcheng）
        /// </summary>
        /// <param name="remitEmployee"></param>
        /// <param name="note"></param>
        /// <returns></returns>
        public bool UpdateOriginalRoadRefundApply(int remitEmployee, string note, long id, int status)
        {
            return customerServiceOperateLogManager.UpdateOriginalRoadRefundApply(remitEmployee, note, id, status);
        }
    }
}
