using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using VAGastronomistMobileApp.SQLServerDAL;
using VAGastronomistMobileApp.Model;
using System.Transactions;

namespace VAGastronomistMobileApp.WebPageDll
{
    /// <summary>
    /// 
    /// </summary>
    public class PointManageLogOperate
    {
        /// <summary>
        /// 积分商城：根据积分变动ID查询其对应的操作日志
        /// </summary>
        /// <param name="pointLogId"></param>
        /// <returns></returns>
        public DataTable QueryPointManageLog(int pointLogId)
        {
            PointManageLogManager _manager = new PointManageLogManager();
            return _manager.QueryPointManageLog(pointLogId);
        }

        /// <summary>
        /// 积分商城：新增一条操作积分兑换表的记录
        /// </summary>
        /// <param name="Log"></param>
        /// <returns></returns>
        public bool InsertPointManageLog(PointManageLog Log)
        {
            PointManageLogManager _manager = new PointManageLogManager();
            if (_manager.InsertPointManageLog(Log) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 更新兑换记录并新增操作日志
        /// </summary>
        /// <param name="point">积分兑换Model</param>
        /// <param name="log">操作日志Model</param>
        /// <returns></returns>
        public bool ModifyPointLog(EmployeePointLog point, PointManageLog log)
        {
            EmployeePointLogManager _ManagePoint = new EmployeePointLogManager();
            PointManageLogManager _ManageLog = new PointManageLogManager();

            using (TransactionScope ts = new TransactionScope())
            {
                bool update = _ManagePoint.UpdateExchangeLog(point);//更新兑换记录
                int i = _ManageLog.InsertPointManageLog(log);//新增操作日志
                //bool exchangeStatus = _ManagePoint.UpdateExchangeStatus(point.exchangeStatus, point.id);//更改兑换单的兑换状态
                if (update && i > 0)// && exchangeStatus
                {
                    ts.Complete();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
