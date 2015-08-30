using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.DBUtility;
using System.Data;

namespace VAGastronomistMobileApp.SQLServerDAL
{
    /// <summary>
    /// 活动分享配置
    /// 2014-10-15
    /// </summary>
    public class ActivityShareManager
    {
        /// <summary>
        /// 新增分享信息
        /// </summary>
        /// <param name="activityShareInfo"></param>
        /// <returns></returns>
        public int InsertActivityShareInfo(ActivityShareInfo activityShareInfo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert dbo.ActivityShareInfo(activityId,type,remark,status) values");
            strSql.Append("(@activityId,@type,@remark,@status)");
            strSql.Append(" select @@identity");
            try
            {
                SqlParameter[] para = new SqlParameter[] { 
                new SqlParameter("@activityId", SqlDbType.Int) { Value = activityShareInfo.activityId },
                new SqlParameter("@type", SqlDbType.Int) { Value = activityShareInfo.type },
                new SqlParameter("@remark", SqlDbType.NVarChar, 500) { Value = activityShareInfo.remark },
                new SqlParameter("@status", SqlDbType.Bit) { Value = activityShareInfo.status }
                };
                using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
                {
                    conn.Open();
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
            catch (Exception)
            {
                return 0;
            }
        }

        /// <summary>
        /// 删除分享信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteActivityShareInfo(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update ActivityShareInfo set status = 0");
            strSql.Append(" where id = @id");
            try
            {
                SqlParameter[] para = new SqlParameter[] { 
                new SqlParameter("@id", SqlDbType.Int) { Value = id }
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
        /// 根据类别查询分享信息
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public List<ActivityShareInfo> SelectActivityShareInfo(ActivityShareInfoType type)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,activityId,type,remark,status");
            strSql.Append(" from dbo.ActivityShareInfo");
            strSql.Append(" where status=1 and activityId=0 and type=@type order by id desc");
            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@type", SqlDbType.Int) { Value = (int)type}
            };
            List<ActivityShareInfo> shareInfo = new List<ActivityShareInfo>();
            using (SqlDataReader sdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), para))
            {
                while (sdr.Read())
                {
                    shareInfo.Add(SqlHelper.GetEntity<ActivityShareInfo>(sdr));
                }
            }
            return shareInfo;
        }

        /// <summary>
        /// 根据活动Id查询分享信息
        /// </summary>
        /// <param name="activityId"></param>
        /// <returns></returns>
        public List<ActivityShareInfo> SelectActivityShareInfo(ActivityShareInfoType type, int activityId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,activityId,type,remark,status");
            strSql.Append(" from dbo.ActivityShareInfo");
            strSql.Append(" where status=1 and activityId=@activityId and type=@type order by id desc");
            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@activityId", SqlDbType.Int) { Value = activityId},
            new SqlParameter("@type", SqlDbType.Int) { Value = type}
            };
            List<ActivityShareInfo> shareInfo = new List<ActivityShareInfo>();
            using (SqlDataReader sdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), para))
            {
                while (sdr.Read())
                {
                    shareInfo.Add(SqlHelper.GetEntity<ActivityShareInfo>(sdr));
                }
            }
            return shareInfo;
        }
        /// <summary>
        /// 根据Id查询其详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActivityShareInfo SelectActivityShareInfoById(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,activityId,type,remark,status");
            strSql.Append(" from dbo.ActivityShareInfo");
            strSql.Append(" where status=1 and id=@id");
            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@id", SqlDbType.Int) { Value = id}
            };
            ActivityShareInfo shareInfo = new ActivityShareInfo();
            using (SqlDataReader sdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), para))
            {
                while (sdr.Read())
                {
                    shareInfo = SqlHelper.GetEntity<ActivityShareInfo>(sdr);
                }
            }
            return shareInfo;
        }

        public List<ActivityShareInfo> GetManyByActivity(int activityId)
        {
            const string strSql = "SELECT id,activityId,type,remark,status FROM [dbo].[ActivityShareInfo] WITH(NOLOCK) WHERE [activityId]=@activityId AND [status]=1";
            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@activityId",SqlDbType.Int) { Value = activityId }
            };
            List<ActivityShareInfo> list = new List<ActivityShareInfo>();
            using (SqlDataReader sdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, para))
            {
                while (sdr.Read())
                {
                    list.Add(sdr.GetEntity<ActivityShareInfo>());
                }
                return list;
            }
        }
    }
}
