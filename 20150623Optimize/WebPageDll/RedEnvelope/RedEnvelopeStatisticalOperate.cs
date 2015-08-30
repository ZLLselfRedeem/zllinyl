using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL;

namespace VAGastronomistMobileApp.WebPageDll
{
    /// <summary>
    /// 
    /// </summary>
    public class RedEnvelopeStatisticalOperate
    {
        RedEnvelopeStatisticalManager manager = new RedEnvelopeStatisticalManager();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name">活动名称</param>
        /// <param name="getTimeBegin">获取红包开始时间</param>
        /// <param name="getTimeEnd">获取红包结束时间</param>
        /// <param name="mobilePhoneNumber">电话号码</param>
        /// <param name="isReal">是否抓取真实抢红包数据</param>
        /// <param name="isUsed">是否已经使用</param>
        /// <returns></returns>
        public DataTable QueryData(string name, DateTime getTimeBegin, DateTime getTimeEnd, bool isReal = false, bool isUsed = false)
        {
            return manager.SelectData(name, getTimeBegin, getTimeEnd, isReal, isUsed);
        }

        /// <summary>
        /// 按照条件查询新用户红包信息
        /// </summary>
        /// <param name="name">活动名称</param>
        /// <param name="getTimeBegin">获取红包开始时间</param>
        /// <param name="getTimeEnd">获取红包结束时间</param>
        /// <param name="registerTimeBegin">注册时间起</param>
        /// <param name="registerTimeEnd">注册时间止</param>
        /// <param name="isUsed">是否已经使用</param>
        /// <returns></returns>
        public DataTable QueryNewCustomer(string name, DateTime getTimeBegin, DateTime getTimeEnd, DateTime registerTimeBegin, DateTime registerTimeEnd, bool isUsed = false)
        {
            return manager.SelectNewCustomer(name, getTimeBegin, getTimeEnd, registerTimeBegin, registerTimeEnd, isUsed);
        }

        /// <summary>
        /// 查询红包订单相关信息
        /// </summary>
        /// <param name="name">活动名称</param>
        /// <param name="getTimeBegin">获取红包时间起</param>
        /// <param name="getTimeEnd">获取红包时间止</param>
        /// <param name="registerTimeBegin">注册时间起</param>
        /// <param name="registerTimeEnd">注册时间止</param>
        /// <param name="payTimeBegin">支付时间起</param>
        /// <param name="payTimeEnd">支付时间止</param>
        /// <param name="cityId">城市ID</param>
        /// <param name="activityType">活动类别</param>
        /// <param name="isRegister">是否参考注册时间</param>
        /// <param name="isPay">是否参考支付时间</param>
        /// <param name="isAll">是否查询全部活动</param>
        /// <returns></returns>
        public DataTable QueryOrder(string name, DateTime getTimeBegin, DateTime getTimeEnd, DateTime registerTimeBegin, DateTime registerTimeEnd, DateTime payTimeBegin, DateTime payTimeEnd, int cityId, int activityType, bool isRegister = false, bool isAll = false)
        {
            return manager.SelectOrder(name, getTimeBegin, getTimeEnd, registerTimeBegin, registerTimeEnd, payTimeBegin, payTimeEnd, cityId, activityType, isRegister, isAll);
        }

        /// <summary>
        /// 查询最高纪录门店流水
        /// </summary>
        /// <param name="name">活动名称</param>
        /// <param name="getTimeBegin">获取红包时间起</param>
        /// <param name="getTimeEnd">获取红包时间止</param>
        /// <param name="registerTimeBegin">注册时间起</param>
        /// <param name="registerTimeEnd">注册时间止</param>
        /// <param name="payTimeBegin">支付时间起</param>
        /// <param name="payTimeEnd">支付时间止</param>
        /// <param name="cityId">城市ID</param>
        /// <param name="activityType">活动类别</param>
        /// <param name="isRegister">是否参考注册时间</param>
        /// <param name="isPay">是否参考支付时间</param>
        /// <param name="isAll">是否查询全部活动</param>
        /// <returns></returns>
        public DataTable QueryTopShop(string name, DateTime getTimeBegin, DateTime getTimeEnd, DateTime registerTimeBegin, DateTime registerTimeEnd, DateTime payTimeBegin, DateTime payTimeEnd, int cityId, int activityType, bool isRegister = false, bool isAll = false)
        {
            return manager.SelectTopShop(name, getTimeBegin, getTimeEnd, registerTimeBegin, registerTimeEnd, payTimeBegin, payTimeEnd, cityId, activityType, isRegister, isAll);
        }

        /// <summary>
        /// 查询指定时间段内的N次消费次数
        /// </summary>
        /// <param name="dayNum"></param>
        /// <param name="paidNum"></param>
        /// <param name="payTimeBegin"></param>
        /// <param name="payTimeEnd"></param>
        /// <returns></returns>
        public int QueryPrePaidCount(int dayNum, int paidNum, DateTime payTimeBegin, DateTime payTimeEnd)
        {
            return manager.SelectPrePaidCount(dayNum, paidNum, payTimeBegin, payTimeEnd);
        }
    }
}
