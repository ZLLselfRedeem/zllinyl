using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using VAGastronomistMobileApp.DBUtility;

namespace VAGastronomistMobileApp.SQLServerDAL
{
    /// <summary>
    /// 微信点菜数据访问层
    /// add by wangc
    /// 20140317
    /// </summary>
    public class WechatOrderManager
    {
        /// <summary>
        /// 根据cookie查询门店微信点菜配置信息
        /// </summary>
        /// <param name="cookie"></param>
        /// <returns></returns>
        public DataTable SelectShopWechatOrderConfigByCookie(string cookie)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 id,cookie,ShopWechatOrderConfig.shopId,status,createdTime,wechatOrderUrl,cityID from ShopWechatOrderConfig ");
            strSql.Append(" left join ShopInfo on ShopInfo.shopID=ShopWechatOrderConfig.shopId");
            strSql.AppendFormat(" where cookie='{0}'", cookie);
            strSql.Append(" and status=1");
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }
        /// <summary>
        /// 微信ID查询用户信息
        /// </summary>
        /// <param name="wechatId"></param>
        /// <returns></returns>
        public DataTable SelectPartCustomerByWechatId(string wechatId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" SELECT TOP 1 CustomerID,mobilePhoneNumber,cookie FROM CustomerInfo ");
            strSql.AppendFormat(" where wechatId='{0}'", wechatId);
            strSql.Append(" and CustomerStatus=1");
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }
        /// <summary>
        /// 根据微信ID查询用户Cookie和最后登录设备信息
        /// </summary>
        /// <param name="wechatId"></param>
        /// <returns></returns>
        public DataTable SelectCustomerByWechatId(string wechatId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("  select DeviceInfo.uuid,MAX(CustomerConnDevice.updateTime),CustomerInfo.cookie");
            strSql.Append("  from DeviceInfo inner join CustomerConnDevice on DeviceInfo.deviceId=CustomerConnDevice.deviceId");
            strSql.Append("  inner join CustomerInfo on CustomerInfo.CustomerID =CustomerConnDevice.customerId");
            strSql.AppendFormat("  where CustomerInfo.wechatId='{0}'", wechatId);
            strSql.Append("  group by DeviceInfo.uuid,CustomerInfo.cookie");
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }
    }
}
