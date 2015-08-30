using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.Model;
using System.Data;
using System.Data.SqlClient;
using VAGastronomistMobileApp.DBUtility;

namespace VAGastronomistMobileApp.SQLServerDAL
{
    /// <summary>
    /// 自定义奖励数据库操作类
    /// </summary>
    public class CustomEncourageManager
    {
        /// <summary>
        /// new新增自定义奖励活动
        /// </summary>
        /// <param name="customEncourage"></param>
        /// <returns></returns>
        public long InsertCustomEncourage(CustomEncourage customEncourage)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                Object obj = null;
                try
                {
                    conn.Open();

                    StringBuilder strSql = new StringBuilder();
                    SqlParameter[] parameters = null;
                    strSql.Append("insert into CustomEncourage(");
                    strSql.Append("type,value,reason,description,");
                    strSql.Append("notificationMessage,createTime,creater,status,companyId,timeType,dayCount)");
                    strSql.Append(" values (");
                    strSql.Append("@type,@value,@reason,@description,");
                    strSql.Append("@notificationMessage,@createTime,@creater,@status,@companyId,@timeType,@dayCount)");
                    strSql.Append(" select @@identity");

                    parameters = new SqlParameter[]{
					        new SqlParameter("@type", SqlDbType.Int,4),
                            new SqlParameter("@value",SqlDbType.NVarChar,50),
                            new SqlParameter("@reason",SqlDbType.NVarChar,50),
                            new SqlParameter("@description",SqlDbType.NVarChar,500),
                            new SqlParameter("@notificationMessage",SqlDbType.NVarChar,100),
                            new SqlParameter("@createTime", SqlDbType.DateTime),
                            new SqlParameter("@creater",SqlDbType.Int,4),
                            new SqlParameter("@status",SqlDbType.Int,4),
                    new SqlParameter("@companyId",SqlDbType.Int,4),

                    new SqlParameter("@timeType",SqlDbType.Int,4),
                    new SqlParameter("@dayCount",SqlDbType.Float)};

                    parameters[0].Value = customEncourage.type;
                    parameters[1].Value = customEncourage.value;
                    parameters[2].Value = customEncourage.reason;
                    parameters[3].Value = customEncourage.description;
                    parameters[4].Value = customEncourage.notificationMessage;
                    parameters[5].Value = customEncourage.createTime;
                    parameters[6].Value = customEncourage.creater;
                    parameters[7].Value = customEncourage.status;
                    parameters[8].Value = customEncourage.companyId;

                    parameters[9].Value = customEncourage.timeType;
                    parameters[10].Value = customEncourage.dayCount;

                    obj = SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql.ToString(), parameters);

                }
                catch (Exception)
                {
                    return 0;
                }
                if (obj == null)
                {
                    return 0;
                }
                else
                {
                    return Convert.ToInt64(obj);
                }
            }

        }
        /// <summary>
        /// new根据自定义奖励活动编号查询对应的信息
        /// </summary>
        /// <param name="customEncourageId"></param>
        /// <returns></returns>
        public DataTable SelectCustomEncourage(long customEncourageId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select [id],[type],[value],[reason],[description],[notificationMessage],");
            strSql.Append("[createTime],[creater],[status],[timeType],[dayCount]");
            strSql.Append(" from CustomEncourage");
            strSql.AppendFormat(" where status > '0' and [id] = {0}", customEncourageId);

            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());

            return ds.Tables[0];
        }
        /// <summary>
        /// (hyr) 根据公司集合查询该公司下所有消费过的客户id
        /// </summary>
        /// <param name="customEncourage"></param>
        /// <returns></returns>
        public DataTable SelectAllCustomer(string companyIDstr)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT Distinct(customerId) from PreOrder19dian as A");
            strSql.Append(" where A.companyId in (" + companyIDstr + ")");
            strSql.Append(" union ");
            strSql.Append("select Distinct(customerId) from CustomerConnCoupon as C");
            strSql.Append(" inner join dbo.CouponInfo as D on C.couponID = D.couponID");
            strSql.Append(" where D.companyId in (" + companyIDstr + ")");
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }
        /// <summary>
        /// (hyr) 根据公司查询该公司上次创建活动的时间
        /// </summary>
        /// <param name="customEncourage"></param>
        /// <returns></returns>
        public string SelectTimeLastEncouraged(int companyId)
        {
            StringBuilder strSql = new StringBuilder();
            SqlParameter[] parameters ={
                                      new SqlParameter("@companyId",companyId)                                  
                                  };
            strSql.Append("SELECT top 1 createTime");
            strSql.Append(" FROM CustomEncourage");
            strSql.Append(" where companyId=@companyId order by createTime desc");
            object obj = SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters);
            if (obj == null)
            {
                return "";
            }
            else
            {
                return obj.ToString();
            }
        }
        /// <summary>
        /// new优惠券领取活动
        /// </summary>
        /// <param name="customEncourage"></param>
        /// <returns></returns>
        public int InsertCouponsReceiveActivitiesInfo(CouponsReceiveActivitiesInfo couponsReceiveActivitiesInfo)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                Object obj = null;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    SqlParameter[] parameters = null;
                    strSql.Append("insert into CouponsReceiveActivities(");
                    strSql.Append("couponsReceiveActivitiesName,companyId,shopId,couponId,");
                    strSql.Append("activitiesValidStartTime,activitiesValidEndTime,couponsReceiveActivitiesDes,couponValidDayCount,status,timeType,couponsReceiveActivitiesCreateTime)");
                    strSql.Append(" values (");
                    strSql.Append("@couponsReceiveActivitiesName,@companyId,@shopId,@couponId,");
                    strSql.Append("@activitiesValidStartTime,@activitiesValidEndTime,@couponsReceiveActivitiesDes,@couponValidDayCount,@status,@timeType,@couponsReceiveActivitiesCreateTime)");
                    strSql.Append(" select @@identity");
                    parameters = new SqlParameter[]{
					        new SqlParameter("@couponsReceiveActivitiesName", SqlDbType.NVarChar,100),
                            new SqlParameter("@companyId",SqlDbType.Int,4),
                            new SqlParameter("@shopId",SqlDbType.Int,4),
                            new SqlParameter("@couponId",SqlDbType.Int,4),
                            new SqlParameter("@activitiesValidStartTime",SqlDbType.DateTime),
                            new SqlParameter("@activitiesValidEndTime", SqlDbType.DateTime),
                            new SqlParameter("@couponsReceiveActivitiesDes",SqlDbType.NVarChar,300),
                            new SqlParameter("@couponValidDayCount",SqlDbType.Float),
                            new SqlParameter("@status",SqlDbType.Int,4),
                    new SqlParameter("@timeType",SqlDbType.Int,4),
                    new SqlParameter("@couponsReceiveActivitiesCreateTime",SqlDbType.DateTime)
                    };

                    parameters[0].Value = couponsReceiveActivitiesInfo.couponsReceiveActivitiesName;
                    parameters[1].Value = couponsReceiveActivitiesInfo.companyId;
                    parameters[2].Value = couponsReceiveActivitiesInfo.shopId;
                    parameters[3].Value = couponsReceiveActivitiesInfo.couponId;
                    parameters[4].Value = couponsReceiveActivitiesInfo.activitiesValidStartTime;
                    parameters[5].Value = couponsReceiveActivitiesInfo.activitiesValidEndTime;
                    parameters[6].Value = couponsReceiveActivitiesInfo.couponsReceiveActivitiesDes;
                    parameters[7].Value = couponsReceiveActivitiesInfo.couponValidDayCount;
                    parameters[8].Value = couponsReceiveActivitiesInfo.status;
                    parameters[9].Value = couponsReceiveActivitiesInfo.timeType;
                    parameters[10].Value = couponsReceiveActivitiesInfo.couponsReceiveActivitiesCreateTime;
                    obj = SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql.ToString(), parameters);
                }
                catch (Exception)
                {
                    return 0;
                }
                if (obj == null)
                {
                    return 0;
                }
                else
                {
                    return 1;
                }
            }
        }
        /// <summary>
        /// 获取领取优惠卷活动
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public DataTable GetActivityDt(int cityId, int companyId, int shopId = 0)
        {
            string swhere = "";
            if (companyId != 0)
            {
                swhere += " and CouponConnShop.companyId ='" + companyId + "'";
                if (shopId != 0)
                {
                    swhere += " and CouponConnShop.shopID ='" + shopId + "'";
                }
            }
            StringBuilder strSql = new StringBuilder();
            // strSql.Append("select * from  CouponsReceiveActivities where '" + System.DateTime.Now + "' >=activitiesValidStartTime and '" + System.DateTime.Now + "' <=activitiesValidEndTime" + swhere);
            //发布优惠券领取活动本身是没有关联门店的，发布优惠券关联门店，此处是通过发布活动的优惠券关联发布优惠券选择的门店进行查询
            //modify by wangcheng
            strSql.Append(" select distinct CouponsReceiveActivities.id, CouponsReceiveActivities.activitiesValidEndTime,CouponsReceiveActivities.activitiesValidStartTime,");
            strSql.Append(" CouponsReceiveActivities.companyId,CouponsReceiveActivities.couponId,CouponsReceiveActivities.couponsReceiveActivitiesCreateTime,");
            strSql.Append(" CouponsReceiveActivities.couponsReceiveActivitiesDes,CouponsReceiveActivities.couponsReceiveActivitiesName,");
            strSql.Append(" CouponsReceiveActivities.couponValidDayCount,CouponsReceiveActivities.status,CouponsReceiveActivities.timeType");
            strSql.Append(" from  CouponsReceiveActivities");
            strSql.Append(" inner join CouponInfo on CouponInfo.couponID=CouponsReceiveActivities.couponId");
            strSql.Append(" inner join CouponConnShop on CouponConnShop.companyId=CouponsReceiveActivities.companyId");
            strSql.Append(" inner join ShopInfo on ShopInfo.shopID=CouponConnShop.shopID");
            strSql.Append(" and CouponsReceiveActivities.couponId=CouponConnShop.couponID");
            strSql.Append(" where");
            strSql.Append("  GETDATE() between activitiesValidStartTime and activitiesValidEndTime");
            strSql.AppendFormat(" and ShopInfo.cityID='{0}'", cityId);
            strSql.Append(swhere);
            strSql.Append("and CouponsReceiveActivities.status='1' order by couponsReceiveActivitiesCreateTime desc");
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }
        /// <summary>
        /// 根据自定义优惠券领取活动Id查询该活动信息
        /// </summary>
        /// <returns></returns>
        public DataTable SelectCouponsReceiveActivities(long id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT couponsReceiveActivitiesName,companyId,shopId,couponId,activitiesValidStartTime,activitiesValidEndTime,");
            strSql.Append(" couponsReceiveActivitiesDes,couponValidDayCount,status,timeType from CouponsReceiveActivities ");
            strSql.Append(" where ");
            strSql.AppendFormat(" id={0}", id);
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }
        /// <summary>
        /// 根据自定义优惠券领取活动
        /// </summary>
        /// <returns></returns>
        public DataTable SelectCouponsReceiveActivities()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT couponsReceiveActivitiesName,CouponInfo.couponName,CouponsReceiveActivities.companyId,CouponsReceiveActivities.id,CompanyInfo.companyName,");
            strSql.Append(" shopId,CouponsReceiveActivities.couponId,activitiesValidStartTime,activitiesValidEndTime,couponsReceiveActivitiesDes,");
            strSql.Append(" couponValidDayCount,CouponsReceiveActivities.status,(case when timeType=1 then '相对时间' else '绝对时间' end) timeType ,couponsReceiveActivitiesCreateTime");
            strSql.Append(" from CouponsReceiveActivities ");
            strSql.Append(" inner join CouponInfo on CouponInfo.couponId=CouponsReceiveActivities.couponId");
            strSql.Append(" inner join CompanyInfo on CompanyInfo.companyID=CouponsReceiveActivities.companyId");
            strSql.Append(" order by couponsReceiveActivitiesCreateTime desc");
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }

        public int SelectCustomerConnCouponCountByCustomerId(long customerID, long activitiesId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Count(*)");
            strSql.Append(" from CustomerConnCoupon");
            strSql.AppendFormat(" where customerID={0}", customerID);
            strSql.AppendFormat(" and EncourageID={0}", activitiesId);
            strSql.AppendFormat(" and Encouragetype={0}", (int)VAEncouragetype.FROM_COUPONSRECEIVEACTIVITIES);
            strSql.AppendFormat(" status={0}", VACustomerCouponStatus.NOT_USED);
            object obj = SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            if (obj == null)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }
    }
}
