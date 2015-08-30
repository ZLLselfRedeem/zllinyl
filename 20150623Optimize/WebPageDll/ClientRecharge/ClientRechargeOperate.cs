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
    /// 客户端充值活动管理:逻辑层
    /// 创建日期：2014-5-4
    /// </summary>
    public class ClientRechargeOperate
    {
        public readonly ClientRechargeManager rechargeManage = new ClientRechargeManager();

        /// <summary>
        /// 新增一个活动
        /// </summary>
        /// <param name="recharge"></param>
        /// <returns></returns>
        public int Insert(ClientRechargeInfo recharge)
        {
            return rechargeManage.Insert(recharge);
        }
        /// <summary>
        /// 新增客户充值记录
        /// </summary>
        /// <param name="recharge"></param>
        /// <returns></returns>
        public long Insert(CustomerRechargeInfo recharge)
        {
            return rechargeManage.Insert(recharge);
        }
        /// <summary>
        /// 开启或关闭充值活动
        /// </summary>
        /// <param name="id">活动编号</param>
        /// <param name="status">状态(1 / -1)</param>
        /// <returns></returns>
        public bool ClientRechargeOnOff(int id, int status)
        {
            return rechargeManage.ClientRechargeOnOff(id, status);
        }
        /// <summary>
        /// 更新充值活动
        /// </summary>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="externalSold">对外已售份数</param>
        /// <returns></returns>
        public bool UpdateRecharge(ClientRechargeInfo recharge)
        {
            return rechargeManage.UpdateRecharge(recharge);
        }
        /// <summary>
        /// 用户充值后，更改已售份数
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool UpdateRechargeSoldCount(int id)
        {
            return rechargeManage.UpdateRechargeSoldCount(id);
        }
        /// <summary>
        /// 客户端查询所有开启的活动
        /// </summary>
        /// <returns></returns>
        public DataTable ClientQueryRecharge()
        {
            return rechargeManage.ClientQueryRecharge();
        }
        /// <summary>
        /// 查询所有活动或指定活动
        /// </summary>
        /// <param name="name">充值活动名称</param>
        /// <returns></returns>
        public DataTable QueryRecharge(string name)
        {
            return rechargeManage.QueryRecharge(name);
        }
        /// <summary>
        /// 根据ID查询活动详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataTable QueryRecharge(int id)
        {
            return rechargeManage.QueryRecharge(id);
        }
        /// <summary>
        /// 充值成功后更新客户充值记录
        /// </summary>
        /// <param name="id">充值活动ID</param>
        /// <param name="payStauts">支付状态</param>
        /// <param name="payTime">支付时间</param>
        /// <param name="payMode">支付方式</param>
        /// <returns></returns>
        public bool UpdateCustomerRecharge(long id, int payStauts, DateTime payTime, int payMode)
        {
            return rechargeManage.UpdateCustomerRecharge(id, payStauts, payTime, payMode);
        }
        /// <summary>
        /// 根据充值记录ID查询用户充值详情
        /// </summary>
        /// <param name="id">用户充值记录ID</param>
        /// <returns></returns>
        public DataTable QueryCustomerRecharge(long id)
        {
            return rechargeManage.QueryCustomerRecharge(id);
        }
        /// <summary>
        /// 充值活动统计数据概览
        /// </summary>
        /// <param name="rechargeId"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public DataTable QueryRechargeStatistics(DateTime beginTime, DateTime endTime, int rechargeId)
        {
            return rechargeManage.QueryRechargeStatistics(beginTime, endTime, rechargeId);
        }
        /// <summary>
        /// 用户充值数据统计
        /// </summary>
        /// <param name="beginTime">充值时间起</param>
        /// <param name="endTime">充值时间止</param>
        /// <param name="rechargeId">充值活动ID</param>
        /// <returns></returns>
        public DataTable QueryCustomerRechargeStatistics(DateTime beginTime, DateTime endTime, int rechargeId)
        {
            return rechargeManage.QueryCustomerRechargeStatistics(beginTime, endTime, rechargeId);
        }
        /// <summary>
        /// 查询用户的充值及消费统计数据
        /// </summary>
        /// <returns></returns>
        public DataTable QueryCustomerConsumeAndRechargeStatistics()
        {
            return rechargeManage.QueryCustomerConsumeAndRechargeStatistics();
        }
        /// <summary>
        /// 查询用户的充值及消费详情
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public DataTable QueryCustomerConsumeAndRechargeDetail(long customerId)
        {
            return rechargeManage.QueryCustomerConsumeAndRechargeDetail(customerId);
        }
        /// <summary>
        /// 查询用户的充值及消费详情
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="rechargeId"></param>
        /// <param name="str"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public DataTable QueryCustomerConsumeAndRechargeDetail(long customerId, int rechargeId, DateTime str, DateTime end)
        {
            return rechargeManage.QueryCustomerConsumeAndRechargeDetail(customerId, rechargeId, str, end);
        }
        /// <summary>
        /// 根据用户充值记录ID查询当笔赠送金额
        /// </summary>
        /// <param name="id">客户充值记录ID</param>
        /// <returns></returns>
        public double QueryPresentMoney(long id)
        {
            return rechargeManage.QueryPresentMoney(id);
        }
        /// <summary>
        /// 是否有在有效时间范围内的活动(客户端闹钟是否显示小红点)
        /// add by wangc 20140512
        /// </summary>
        /// <param name="isShowRechargeActivities"></param>
        /// <returns></returns>
        public bool ServerIsHaveEffectiveActivities(bool isShowRechargeActivities)
        {
            if (isShowRechargeActivities == false)//显示活动总开关
            {
                return false;
            }
            else
            {
                bool result = false;
                DateTime nowTime = DateTime.Now;
                DataTable dtRecharge = ClientQueryRecharge();
                foreach (DataRow item in dtRecharge.Rows)
                {
                    if (nowTime > Common.ToDateTime(item["beginTime"]) && nowTime < Common.ToDateTime(item["endTime"]))//等于这种临界值，不考虑，c/s 通讯需要时间
                    {
                        result = true;
                        break;
                    }
                    else
                    {
                        continue;
                    }
                }
                return result;
            }
        }
    }
}
