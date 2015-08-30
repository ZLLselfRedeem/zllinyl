using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.DBUtility;
using System.Data.SqlClient;
using System.Data;
using VAGastronomistMobileApp.SQLServerDAL.Persistence.Infrastructure;

namespace VAGastronomistMobileApp.SQLServerDAL
{
    /// <summary>
    /// 红包活动
    /// 2014-7-29
    /// </summary>
    public class ActivityManager
    {
        /// <summary>
        /// 新增活动
        /// </summary>
        /// <param name="activity"></param>
        /// <returns></returns>
        public int InsertActivity(Activity activity)
        {
            const string strSql = @"insert Activity(name,beginTime,endTime,enabled,status,createTime,createdBy,expirationTimeRule,ruleValue,activityRule,activityType,redEnvelopeEffectiveBeginTime,redEnvelopeEffectiveEndTime) values
 (@name,@beginTime,@endTime,@enabled,@status,@createTime,@createdBy,@expirationTimeRule,@ruleValue,@activityRule,@activityType,@redEnvelopeEffectiveBeginTime,@redEnvelopeEffectiveEndTime) select @@identity";
            SqlParameter[] para = new SqlParameter[] { 
                new SqlParameter("@name", SqlDbType.NVarChar, 100) { Value = activity.name },
                new SqlParameter("@beginTime", SqlDbType.DateTime) { Value = activity.beginTime },
                new SqlParameter("@endTime", SqlDbType.DateTime) { Value = activity.endTime },
                new SqlParameter("@enabled", SqlDbType.Bit) { Value = 1 },
                new SqlParameter("@status", SqlDbType.Bit) { Value = 1 },
                new SqlParameter("@createTime", SqlDbType.DateTime) { Value = DateTime.Now },
                new SqlParameter("@createdBy", SqlDbType.Int) { Value = activity.createdBy },
                new SqlParameter("@expirationTimeRule", SqlDbType.Int) { Value = activity.expirationTimeRule },
                new SqlParameter("@ruleValue", SqlDbType.Int) { Value = activity.ruleValue },
                new SqlParameter("@activityRule", SqlDbType.NVarChar, 2000) { Value = activity.activityRule },
                new SqlParameter("@activityType", SqlDbType.Int) { Value = activity.activityType },
                new SqlParameter("@redEnvelopeEffectiveBeginTime", SqlDbType.DateTime) { Value = activity.redEnvelopeEffectiveBeginTime },
                new SqlParameter("@redEnvelopeEffectiveEndTime", SqlDbType.DateTime) { Value = activity.redEnvelopeEffectiveEndTime }
                };
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                object obj = SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql.ToString(), para);
                if (obj == null)
                {
                    return 0;
                }
                else
                {
                    return Convert.ToInt32(obj);
                }
            }
        }
        public int InsertActivityExt(Activity activity)
        {
            const string strSql = @"insert Activity(name,beginTime,endTime,enabled,status,createTime,createdBy,expirationTimeRule,ruleValue,activityRule,activityType) values
 (@name,@beginTime,@endTime,@enabled,@status,@createTime,@createdBy,@expirationTimeRule,@ruleValue,@activityRule,@activityType) select @@identity";
            SqlParameter[] para = new SqlParameter[] { 
                new SqlParameter("@name", SqlDbType.NVarChar, 100) { Value = activity.name },
                new SqlParameter("@beginTime", SqlDbType.DateTime) { Value = activity.beginTime },
                new SqlParameter("@endTime", SqlDbType.DateTime) { Value = activity.endTime },
                new SqlParameter("@enabled", SqlDbType.Bit) { Value = 1 },
                new SqlParameter("@status", SqlDbType.Bit) { Value = 1 },
                new SqlParameter("@createTime", SqlDbType.DateTime) { Value = DateTime.Now },
                new SqlParameter("@createdBy", SqlDbType.Int) { Value = activity.createdBy },
                new SqlParameter("@expirationTimeRule", SqlDbType.Int) { Value = activity.expirationTimeRule },
                new SqlParameter("@ruleValue", SqlDbType.Int) { Value = activity.ruleValue },
                new SqlParameter("@activityRule", SqlDbType.NVarChar, 2000) { Value = activity.activityRule },
                new SqlParameter("@activityType", SqlDbType.Int) { Value = activity.activityType }
                };
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                object obj = SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql.ToString(), para);
                if (obj == null)
                {
                    return 0;
                }
                else
                {
                    return Convert.ToInt32(obj);
                }
            }
        }

        /// <summary>
        /// 修改活动
        /// </summary>
        /// <param name="activity"></param>
        /// <returns></returns>
        public bool UpdateActivity(Activity activity)
        {
            const string strSql = @"UPDATE [VAGastronomistMobileApp].[dbo].[Activity] SET
 [name] = @name,[beginTime] = @beginTime,[endTime] = @endTime,[updateTime] = @updateTime,[updatedBy] = @updatedBy,
[expirationTimeRule] = @expirationTimeRule,[ruleValue] = @ruleValue,[activityRule] = @activityRule,[activityType] = @activityType,
[redEnvelopeEffectiveBeginTime] = @redEnvelopeEffectiveBeginTime,[redEnvelopeEffectiveEndTime] = @redEnvelopeEffectiveEndTime
 where activityId = @activityId";
            SqlParameter[] para = new SqlParameter[] { 
                new SqlParameter("@name", SqlDbType.NVarChar, 100) { Value = activity.name },
                new SqlParameter("@beginTime", SqlDbType.DateTime) { Value = activity.beginTime },
                new SqlParameter("@endTime", SqlDbType.DateTime) { Value = activity.endTime },
                new SqlParameter("@updateTime", SqlDbType.DateTime) { Value = DateTime.Now },
                new SqlParameter("@updatedBy", SqlDbType.Int) { Value = activity.createdBy },
                 new SqlParameter("@expirationTimeRule", SqlDbType.Int) { Value = activity.expirationTimeRule },
                new SqlParameter("@ruleValue", SqlDbType.Int) { Value = activity.ruleValue },
                new SqlParameter("@activityRule", SqlDbType.NVarChar, 2000) { Value = activity.activityRule },
                new SqlParameter("@activityId", SqlDbType.Int) { Value = activity.activityId },
                new SqlParameter("@activityType", SqlDbType.Int) { Value = activity.activityType },
                 new SqlParameter("@redEnvelopeEffectiveBeginTime", SqlDbType.DateTime) { Value = activity.redEnvelopeEffectiveBeginTime },
                new SqlParameter("@redEnvelopeEffectiveEndTime", SqlDbType.DateTime) { Value = activity.redEnvelopeEffectiveEndTime }
                };

            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int i = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), para);
                return i > 0;
            }
        }
        public bool UpdateActivityExt(Activity activity)
        {
            const string strSql = @"UPDATE [VAGastronomistMobileApp].[dbo].[Activity] SET
 [name] = @name,[beginTime] = @beginTime,[endTime] = @endTime,[updateTime] = @updateTime,[updatedBy] = @updatedBy,
[expirationTimeRule] = @expirationTimeRule,[ruleValue] = @ruleValue,[activityRule] = @activityRule,[activityType] = @activityType
 where activityId = @activityId";
            SqlParameter[] para = new SqlParameter[] { 
                new SqlParameter("@name", SqlDbType.NVarChar, 100) { Value = activity.name },
                new SqlParameter("@beginTime", SqlDbType.DateTime) { Value = activity.beginTime },
                new SqlParameter("@endTime", SqlDbType.DateTime) { Value = activity.endTime },
                new SqlParameter("@updateTime", SqlDbType.DateTime) { Value = DateTime.Now },
                new SqlParameter("@updatedBy", SqlDbType.Int) { Value = activity.createdBy },
                 new SqlParameter("@expirationTimeRule", SqlDbType.Int) { Value = activity.expirationTimeRule },
                new SqlParameter("@ruleValue", SqlDbType.Int) { Value = activity.ruleValue },
                new SqlParameter("@activityRule", SqlDbType.NVarChar, 2000) { Value = activity.activityRule },
                new SqlParameter("@activityId", SqlDbType.Int) { Value = activity.activityId },
                new SqlParameter("@activityType", SqlDbType.Int) { Value = activity.activityType }
                };

            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int i = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), para);
                return i > 0;
            }
        }

        /// <summary>
        /// 更改活动的启停状态
        /// </summary>
        /// <param name="activityId"></param>
        /// <param name="enabled"></param>
        /// <returns></returns>
        public bool EnableActivity(int activityId, int enabled)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Activity set enabled = @enabled");
            strSql.Append(" where activityId = @activityId");
            try
            {
                SqlParameter[] para = new SqlParameter[] { 
                new SqlParameter("@enabled", SqlDbType.Bit) { Value = enabled },
                new SqlParameter("@activityId", SqlDbType.Int) { Value = activityId }
                };
                using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
                {
                    int i = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), para);
                    if (i > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 删除指定活动
        /// </summary>
        /// <param name="activityId"></param>
        /// <returns></returns>
        public bool DeleteActivity(int activityId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Activity set status = 0");
            strSql.Append(" where activityId = @activityId");
            try
            {
                SqlParameter[] para = new SqlParameter[] { 
                new SqlParameter("@activityId", SqlDbType.Int) { Value = activityId }
                };
                using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
                {
                    int i = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), para);
                    if (i > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 根据活动Id查询相应信息
        /// </summary>
        /// <param name="activityId"></param>
        /// <returns></returns>
        public Activity QueryActivity(int activityId)
        {
            const string strSql = @"select activityId,name,beginTime,endTime,enabled,status,createTime,createdBy,expirationTimeRule,ruleValue,activityRule,activityType,redEnvelopeEffectiveBeginTime,redEnvelopeEffectiveEndTime
 from Activity where status = 1 and activityId = @activityId";
            Activity activity = null;
            try
            {
                SqlParameter[] para = new SqlParameter[] { 
                    new SqlParameter("@activityId", SqlDbType.Int) { Value = activityId }
                };
                using (SqlDataReader sdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), para))
                {
                    if (sdr.Read())
                    {
                        activity = SqlHelper.GetEntity<Activity>(sdr);
                    }
                }
                return activity;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 查询所有活动，分页
        /// </summary>
        /// <returns></returns>
        public List<Activity> QueryActivity(Page page, string name, out int cnt)
        {
            List<Activity> activities = new List<Activity>();
            StringBuilder strSql = new StringBuilder();
            StringBuilder strCnt = new StringBuilder();
            strCnt.Append("select count(1)  from Activity where status = 1 ");

            strSql.Append(" select activityId,name,beginTime,endTime,enabled,status,createTime,createdBy,expirationTimeRule,ruleValue,activityRule,activityType,redEnvelopeEffectiveBeginTime,redEnvelopeEffectiveEndTime from (");
            strSql.Append(" select row_number() OVER (ORDER BY activityId desc) rownum,");
            strSql.Append(" activityId,name,beginTime,endTime,enabled,status,createTime,createdBy,expirationTimeRule,ruleValue,activityRule,activityType,redEnvelopeEffectiveBeginTime,redEnvelopeEffectiveEndTime");
            strSql.Append(" from Activity where status = 1 ");
            if (!string.IsNullOrEmpty(name))
            {
                strSql.Append(" and name like @name");
                strCnt.Append(" and name like @name");
            }
            strSql.Append(") a");
            strSql.Append(" where a.rownum between @startIndex and @endIndex");

            try
            {
                List<SqlParameter> paraList = new List<SqlParameter>();
                if (!string.IsNullOrEmpty(name))
                {
                    paraList.Add(new SqlParameter("@name", SqlDbType.NVarChar, 100) { Value = "%" + name + "%" });
                }
                SqlParameter[] paraCnt = paraList.ToArray();

                object objCnt = SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strCnt.ToString(), paraCnt);
                if (objCnt == null)
                {
                    cnt = 0;
                }
                else
                {
                    cnt = Convert.ToInt32(objCnt);
                }

                paraList.Add(new SqlParameter("@startIndex", SqlDbType.Int) { Value = page.Skip + 1 });
                paraList.Add(new SqlParameter("@endIndex", SqlDbType.Int) { Value = page.Skip + page.PageSize });

                SqlParameter[] para = paraList.ToArray();
                using (SqlDataReader sdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), para))
                {
                    while (sdr.Read())
                    {
                        activities.Add(SqlHelper.GetEntity<Activity>(sdr));
                    }
                }
                return activities;
            }
            catch (Exception)
            {
                cnt = 0;
                return null;
            }
        }

        /// <summary>
        /// 查询所有活动
        /// </summary>
        /// <returns></returns>
        public List<Activity> QueryActivity(bool present = false)
        {
            List<Activity> activities = new List<Activity>();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select activityId,name,beginTime,endTime,enabled,status,createTime,createdBy,expirationTimeRule,ruleValue,activityRule,activityType from Activity where status = 1 and endTime>GETDATE()");
            if (present == true)
            {
                strSql.Append(" and activityType=" + (int)ActivityType.赠送红包);
            }
            else
            {
                strSql.Append(" and activityType!=" + (int)ActivityType.赠送红包);
            }
            strSql.Append(" order by activityId desc");
            using (SqlDataReader sdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString()))
            {
                while (sdr.Read())
                {
                    activities.Add(SqlHelper.GetEntity<Activity>(sdr));
                }
            }
            return activities;
        }

        public List<Activity> QueryAllActivity()
        {
            List<Activity> activities = new List<Activity>();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select activityId,name,beginTime,endTime,enabled,status,createTime,createdBy,expirationTimeRule,ruleValue,activityRule,activityType from Activity where status = 1");// and endTime>GETDATE()          
            strSql.Append(" order by activityId desc");
            using (SqlDataReader sdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString()))
            {
                while (sdr.Read())
                {
                    activities.Add(SqlHelper.GetEntity<Activity>(sdr));
                }
            }
            return activities;
        }

        public List<Activity> QueryAllActivityNew()
        {
            List<Activity> activities = new List<Activity>();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select activityId,name,beginTime,endTime,enabled,status,createTime,createdBy,expirationTimeRule,ruleValue,activityRule,activityType from Activity where status = 1 and endTime>GETDATE() and enabled=1");//          
            strSql.Append(" order by activityId desc");
            using (SqlDataReader sdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString()))
            {
                while (sdr.Read())
                {
                    activities.Add(SqlHelper.GetEntity<Activity>(sdr));
                }
            }
            return activities;
        }
    }
}
