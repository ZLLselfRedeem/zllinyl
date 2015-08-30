using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.DBUtility;
using System.Data.SqlClient;
using System.Data;

namespace VAGastronomistMobileApp.SQLServerDAL
{
    public class MealScheduleManager
    {
        public bool AddEntity(MealSchedule entity)
        {
            StringBuilder sqlBuilder = new StringBuilder();

            sqlBuilder.Append(" INSERT INTO  [dbo].[MealSchedule] ");
            sqlBuilder.Append(" ([MealID] ");
            sqlBuilder.Append(" ,[ValidFrom] ");
            sqlBuilder.Append("  ,[ValidTo] ");
            sqlBuilder.Append("  ,[SoldCount] ");
            sqlBuilder.Append(" ,[DinnerTime] ");
            sqlBuilder.Append(" ,[IsActive] ");
            sqlBuilder.Append(" ,[DinnerType] ");
            sqlBuilder.Append(" ,[TotalCount]) ");
            sqlBuilder.Append(" VALUES ");
            sqlBuilder.Append(" (@MealID ");
            sqlBuilder.Append(" ,@ValidFrom ");
            sqlBuilder.Append(" ,@ValidTo ");
            sqlBuilder.Append(" ,@SoldCount ");
            sqlBuilder.Append(" ,@DinnerTime ");
            sqlBuilder.Append(" ,@IsActive ");
            sqlBuilder.Append(" ,@DinnerType ");
            sqlBuilder.Append(" ,@TotalCount )");
            sqlBuilder.Append(" SELECT @@identity ");
            SqlParameter[] parameters = new SqlParameter[]
            {
             new SqlParameter("@MealID",entity.MealID),
           new SqlParameter("@ValidFrom",entity.ValidFrom),
           new SqlParameter("@ValidTo",entity.ValidTo),
           new SqlParameter("@SoldCount",entity.SoldCount),
           new SqlParameter("@DinnerTime",entity.DinnerTime),
           new SqlParameter("@IsActive",entity.IsActive),
           new SqlParameter("@DinnerType",(int)entity.DinnerType),
           new SqlParameter("@TotalCount",entity.TotalCount),
            };
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                var returnValue = SqlHelper.ExecuteScalar(conn, CommandType.Text, sqlBuilder.ToString(), parameters);
                int mealID;
                if (int.TryParse(returnValue.ToString(), out mealID))
                {
                    entity.MealID = mealID;
                    return true;
                } 
            }
            return false;
        }

        public bool UpdateEntity(MealSchedule entity)
        {
            StringBuilder sqlBuilder = new StringBuilder();

          sqlBuilder.Append("  UPDATE [MealSchedule]");
   sqlBuilder.Append(" SET [MealID] = @MealID");
      sqlBuilder.Append(" ,[ValidFrom] = @ValidFrom");
      sqlBuilder.Append(" ,[ValidTo] = @ValidTo");
     sqlBuilder.Append("  ,[SoldCount] = @SoldCount");
     sqlBuilder.Append("  ,[DinnerTime] = @DinnerTime");
      sqlBuilder.Append(" ,[IsActive] = @IsActive");
      sqlBuilder.Append(" ,[DinnerType] = @DinnerType");
      sqlBuilder.Append(" ,[TotalCount] = @TotalCount");
sqlBuilder.Append("  WHERE  MealScheduleID = @MealScheduleID");
            SqlParameter[] parameters = new SqlParameter[]
            {
             new SqlParameter("@MealID",entity.MealID),
           new SqlParameter("@ValidFrom",entity.ValidFrom),
           new SqlParameter("@ValidTo",entity.ValidTo),
           new SqlParameter("@SoldCount",entity.SoldCount),
           new SqlParameter("@DinnerTime",entity.DinnerTime),
           new SqlParameter("@IsActive",entity.IsActive),
           new SqlParameter("@DinnerType",(int)entity.DinnerType),
           new SqlParameter("@TotalCount",entity.TotalCount),
           new SqlParameter("@MealScheduleID",entity.MealScheduleID)
            };
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                var returnValue = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, sqlBuilder.ToString(), parameters);
                return returnValue > 0;
            } 
        }
        public MealSchedule GetEntityByID(int mealScheduleID)
        {
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.Append(" SELECT [MealScheduleID] ");
            sqlBuilder.Append(" ,[MealID] ");
            sqlBuilder.Append(" ,[ValidFrom] ");
            sqlBuilder.Append(" ,[ValidTo] "); 
            sqlBuilder.Append(" ,[DinnerTime] ");
            sqlBuilder.Append(" ,[SoldCount] ");
            sqlBuilder.Append(" ,[TotalCount] ");
            sqlBuilder.Append(" ,[IsActive] ");
            sqlBuilder.Append(" ,[DinnerType] ");
            sqlBuilder.Append(" FROM  [MealSchedule] ");
            sqlBuilder.Append(" WHERE [MealScheduleID] = @MealScheduleID ");
            SqlParameter[] parameters = {					
					new SqlParameter("@MealScheduleID", mealScheduleID)};
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                var reader = SqlHelper.ExecuteReader(conn, CommandType.Text, sqlBuilder.ToString(), parameters);
                if (reader.Read())
                {
                    return reader.GetEntity<MealSchedule>();
                }
            }
            return null;
        }

        public int GetCountByQuery(MealScheduleQueryObject queryObject)
        {
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.Append(" SELECT COUNT(0) FROM  [MealSchedule]  ");
            sqlBuilder.Append(" WHERE 1=1 ");
            List<SqlParameter> parameters = new List<SqlParameter>();
            if (queryObject.MealID.HasValue)
            {
                sqlBuilder.Append(" AND MealID = @MealID");
                parameters.Add(new SqlParameter("@MealID", queryObject.MealID.Value));
            }
            if (queryObject.DinnerType.HasValue)
            {
                sqlBuilder.Append(" AND DinnerType = @DinnerType");
                parameters.Add(new SqlParameter("@DinnerType", queryObject.DinnerType.Value));
            }
            if (queryObject.DinnerTime.HasValue)
            {
                sqlBuilder.Append(" AND DinnerTime = @DinnerTime");
                parameters.Add(new SqlParameter("@DinnerTime", queryObject.DinnerTime.Value));
            }
            if (queryObject.DinnerTimeFrom.HasValue)
            {
                sqlBuilder.Append(" AND DinnerTime >= @DinnerTimeFrom");
                parameters.Add(new SqlParameter("@DinnerTimeFrom", queryObject.DinnerTimeFrom.Value));
            }
            if (queryObject.DinnerTimeFrom.HasValue)
            {
                sqlBuilder.Append(" AND DinnerTime <= @DinnerTimeTo");
                parameters.Add(new SqlParameter("@DinnerTimeTo", queryObject.DinnerTimeTo.Value));
            }
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                var returnValue = SqlHelper.ExecuteScalar(conn, CommandType.Text, sqlBuilder.ToString(), parameters.ToArray());
                int count;
                if (int.TryParse(returnValue.ToString(), out count))
                {
                    return count;
                }
                return 0;
            }
        }

        public List<MealSchedule> GetListByQuery(MealScheduleQueryObject queryObject, int pageIndex, int pageSize)
        {
            StringBuilder sqlBuilder = new StringBuilder("SELECT * FROM (");
            sqlBuilder.Append(" SELECT ROW_NUMBER() OVER(ORDER BY DinnerTime,DinnerType) NUM, [MealScheduleID] ");
            sqlBuilder.Append(" ,[MealID] ");
            sqlBuilder.Append(" ,[ValidFrom] ");
            sqlBuilder.Append(" ,[ValidTo] "); 
            sqlBuilder.Append(" ,[DinnerTime] ");
            sqlBuilder.Append(" ,[SoldCount] ");
            sqlBuilder.Append(" ,[TotalCount] ");
            sqlBuilder.Append(" ,[IsActive] ");
            sqlBuilder.Append(" ,[DinnerType] ");
            sqlBuilder.Append(" FROM [VAGastronomistMobileApp].[dbo].[MealSchedule] WHERE 1=1 ");
            List<SqlParameter> parameters = new List<SqlParameter>();
            if (queryObject.MealID.HasValue)
            {
                sqlBuilder.Append(" AND MealID = @MealID");
                parameters.Add(new SqlParameter("@MealID", queryObject.MealID.Value));
            }
            if (queryObject.DinnerType.HasValue)
            {
                sqlBuilder.Append(" AND DinnerType = @DinnerType");
                parameters.Add(new SqlParameter("@DinnerType", queryObject.DinnerType.Value));
            }
            if (queryObject.DinnerTime.HasValue)
            {
                sqlBuilder.Append(" AND DinnerTime = @DinnerTime");
                parameters.Add(new SqlParameter("@DinnerTime", queryObject.DinnerTime.Value));
            }
            if (queryObject.DinnerTimeFrom.HasValue)
            {
                sqlBuilder.Append(" AND DinnerTime >= @DinnerTimeFrom");
                parameters.Add(new SqlParameter("@DinnerTimeFrom", queryObject.DinnerTimeFrom.Value));
            }
            if (queryObject.DinnerTimeFrom.HasValue)
            {
                sqlBuilder.Append(" AND DinnerTime <= @DinnerTimeTo");
                parameters.Add(new SqlParameter("@DinnerTimeTo", queryObject.DinnerTimeTo.Value));
            }
            sqlBuilder.Append(" ) T WHERE T.NUM > @PageStart AND T.NUM <= @PageEnd");
            parameters.Add(new SqlParameter("@PageStart", (pageIndex - 1) * pageSize));
            parameters.Add(new SqlParameter("@PageEnd", pageIndex * pageSize));
            List<MealSchedule> list = null;
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                list = new List<MealSchedule>();
                var reader = SqlHelper.ExecuteReader(conn, CommandType.Text, sqlBuilder.ToString(), parameters.ToArray());
                while (reader.Read())
                {
                    list.Add(reader.GetEntity<MealSchedule>());
                }
            }
            return list;
        }

        public List<MealSchedule> GetListByQuery(MealScheduleQueryObject queryObject)
        {
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.Append(" SELECT ROW_NUMBER() OVER(ORDER BY DinnerTime,DinnerType) NUM, [MealScheduleID] ");
            sqlBuilder.Append(" ,[MealID] ");
            sqlBuilder.Append(" ,[ValidFrom] ");
            sqlBuilder.Append(" ,[ValidTo] "); 
            sqlBuilder.Append(" ,[DinnerTime] ");
            sqlBuilder.Append(" ,[SoldCount] ");
            sqlBuilder.Append(" ,[TotalCount] ");
            sqlBuilder.Append(" ,[IsActive] ");
            sqlBuilder.Append(" ,[DinnerType] ");
            sqlBuilder.Append(" FROM [VAGastronomistMobileApp].[dbo].[MealSchedule] WHERE 1=1 ");
            List<SqlParameter> parameters = new List<SqlParameter>();
            if (queryObject.MealID.HasValue)
            {
                sqlBuilder.Append(" AND MealID = @MealID");
                parameters.Add(new SqlParameter("@MealID", queryObject.MealID.Value));
            }
            if (queryObject.DinnerTimeFrom.HasValue)
            {
                sqlBuilder.Append(" AND DinnerTime >= @DinnerTimeFrom");
                parameters.Add(new SqlParameter("@DinnerTimeFrom", queryObject.DinnerTimeFrom.Value));
            }
            if (queryObject.DinnerTimeFrom.HasValue)
            {
                sqlBuilder.Append(" AND DinnerTime <= @DinnerTimeTo");
                parameters.Add(new SqlParameter("@DinnerTimeTo", queryObject.DinnerTimeTo.Value));
            }
            List<MealSchedule> list = null;
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                list = new List<MealSchedule>();
                var reader = SqlHelper.ExecuteReader(conn, CommandType.Text, sqlBuilder.ToString(), parameters.ToArray());
                while (reader.Read())
                {
                    list.Add(reader.GetEntity<MealSchedule>());
                }
            }
            return list;
        }

        /// <summary>
        /// 根据MealID查询套餐排期详情
        /// </summary>
        /// <param name="mealId"></param>
        /// <returns></returns>
        public static List<MealSchedule> SelectMealSchedule(int mealId)
        {
            const string strSql = "select d.MealScheduleID,d.MealID,d.ValidFrom,d.DinnerTime,d.ValidTo,d.DinnerType,(d.TotalCount-d.SoldCount) TotalCount from MealSchedule d where d.MealID=@mealId and d.IsActive=1";
            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@mealId", SqlDbType.Int) { Value = mealId }
            };
            List<MealSchedule> mealScheduleList = new List<MealSchedule>();
            using (SqlDataReader sdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, para))
            {
                while (sdr.Read())
                {
                    mealScheduleList.Add(SqlHelper.GetEntity<MealSchedule>(sdr));
                }
            }
            return mealScheduleList;
        }

        public static List<MealScheduleCount> SelectMealScheduleCount(int shopId)
        {
            string strSql = @"select d.MealID,CONVERT(char(8),d.DinnerTime,112) DinnerTime,sum(d.TotalCount-d.SoldCount) remainCount 
from MealSchedule d inner join Meal m on d.MealID=m.MealID where d.IsActive=1
and m.ShopID=@shopId group by d.MealID,CONVERT(char(8),d.DinnerTime,112)";
            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@shopId", SqlDbType.Int) { Value = shopId }
            };
            List<MealScheduleCount> schedule = new List<MealScheduleCount>();
            using (SqlDataReader sdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, para))
            {
                while (sdr.Read())
                {
                    schedule.Add(SqlHelper.GetEntity<MealScheduleCount>(sdr));
                }
            }
            return schedule;
        }

        /// <summary>
        /// 根据套餐排期Id查询剩余份数
        /// </summary>
        /// <param name="mealId"></param>
        /// <returns></returns>
        public static string SelectRemain(int MealScheduleID)
        {
            const string strSql = @"select (d.TotalCount-d.SoldCount) remainCount from MealSchedule d where d.MealScheduleID=@MealScheduleID";
            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@MealScheduleID", SqlDbType.Int) { Value = MealScheduleID }
            };
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                object obj = SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql, para);
                if (obj == null)
                {
                    return "";
                }
                else
                {
                    return obj.ToString();
                }
            }
        }
        /// <summary>
        /// 查询年夜饭套餐就餐时间和中晚餐类别
        /// </summary>
        /// <param name="preOrderId"></param>
        /// <returns></returns>
        public static Tuple<string, string> GetDinnerTime(long preOrderId)
        {
            const string strSql = @"select C.DinnerTime,B.Price from MealConnPreOrder A
 left join MealSchedule C on C.MealScheduleID=A.MealScheduleID
 left join Meal B on C.MealID=B.MealID
 where A.preOrder19dianId=@preOrder19dianId";
            SqlParameter parameter = new SqlParameter("@preOrder19dianId", preOrderId);
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, parameter))
            {
                if (dr.Read())
                {
                    string timeStr = Convert.ToDateTime(dr[0]).ToString("yyyy年MM月dd日HH:mm");
                    string price = dr[1].ToString();
                    return Tuple.Create<string, string>(timeStr, price);
                }
            }
            return Tuple.Create<string, string>("", "");
        }

        public static bool AddSoldOutCount(int mealScheduleId)
        {
            const string strSql = "update MealSchedule set SoldCount=ISNULL(SoldCount,0)+1 where MealScheduleID=@mealScheduleId and TotalCount-ISNULL(SoldCount,0)>=1";
            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@mealScheduleId", SqlDbType.Int) { Value = mealScheduleId }
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
        /// <summary>
        /// 用户退款需要把套餐份数还回去
        /// </summary>
        /// <param name="preOrder19dianId"></param>
        /// <returns></returns>
        public static bool MinusSoldOutCount(long preOrder19dianId)
        {
            const string strSql = @"update MealSchedule set SoldCount=ISNULL(SoldCount,0)-1 where MealScheduleID = 
                (select MealScheduleID from MealConnPreOrder where preOrder19dianId=@preOrder19dianId) and SoldCount>0;";
            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@preOrder19dianId", SqlDbType.BigInt) { Value = preOrder19dianId }
            };
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int i = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), para);
                if (i >= 0)
                {
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
