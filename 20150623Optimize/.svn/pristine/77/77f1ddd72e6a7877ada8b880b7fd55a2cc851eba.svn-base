using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL;
using System.Data;

namespace VAGastronomistMobileApp.WebPageDll
{
    /// <summary>
    /// add by wangc 20140326
    /// 用户充值日志
    /// </summary>
    public class RechargeLogOperate
    {
        readonly RechargeLogManager man = new RechargeLogManager();
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(RechargeLog model)
        {
            return man.Add(model) > 0;
        }
        public bool BatchInsertRechargeLog(DataTable dt)
        {
            return man.BatchInsertRechargeLog(dt);
        }
        /// <summary>
        /// 查询打款日志
        /// </summary>
        /// <param name="strTime"></param>
        /// <param name="endTime"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public DataTable QueryRechargeLog(string strTime, string endTime, int status)
        {
            return man.SelectRechargeLog(strTime, endTime, status);
        }
        /// <summary>
        /// 查询申请充值记录手机号码
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public List<RechargeLogPartMolde> QueryRechargeCustomerPhone(string ids)
        {
            return man.SelectRechargeCustomerPhone(ids);
        }
        /// <summary>
        /// 更新充值申请审批状态
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="status"></param>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public bool ModifyRechargeLogStatus(string ids, int status, int employeeId)
        {
            return man.UpdateRechargeLogStatus(ids, status,employeeId);
        }
    }
}
