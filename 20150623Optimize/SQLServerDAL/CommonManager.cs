using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LogDll;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.DBUtility;
using System.Data.SqlClient;
using System.Data;

using System.Reflection;
using System.ComponentModel;

namespace VAGastronomistMobileApp.SQLServerDAL
{
    public class CommonManager
    {
        /// <summary>
        /// 获取值
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static object GetFieldValue(string sql)
        {
            return SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sql.ToString());
        }

        public static DataTable GetDataTableFieldValue(string sql)
        {
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sql.ToString());
            return ds.Tables[0];
        }

        /// <summary>
        /// 分页方法
        /// </summary>
        /// <param name="pg"></param>
        /// <returns></returns>
        public static DataTable DbPager(PaginationPager pg)
        {
            DataTable dtnull = new DataTable();
            try
            {
                SqlConnection con = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction);
                SqlDataAdapter da = new SqlDataAdapter("SqlPager", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.Add(new SqlParameter("@tblName", pg.tblName));
                da.SelectCommand.Parameters.Add(new SqlParameter("@strGetFields", pg.strGetFields));
                da.SelectCommand.Parameters.Add(new SqlParameter("@OrderfldName", pg.OrderfldName));
                da.SelectCommand.Parameters.Add(new SqlParameter("@realOrderfldName", pg.realOrderfldName));
                da.SelectCommand.Parameters.Add(new SqlParameter("@PageSize", pg.PageSize));
                da.SelectCommand.Parameters.Add(new SqlParameter("@PageIndex", pg.PageIndex));
                da.SelectCommand.Parameters.Add("@doCount", SqlDbType.Int);
                da.SelectCommand.Parameters["@doCount"].Direction = ParameterDirection.Output;
                da.SelectCommand.Parameters.Add(new SqlParameter("@OrderType", pg.OrderType));
                da.SelectCommand.Parameters.Add(new SqlParameter("@strWhere", pg.strWhere));
                DataSet ds = new DataSet();
                da.Fill(ds);
                // pg.doCount = Convert.ToInt32(da.SelectCommand.Parameters["@doCount"].Value == DBNull.Value ? 0 : da.SelectCommand.Parameters["@doCount"].Value);
                pg.doCount = GetPaginationPagerTableCount(pg);
                return ds.Tables[0];
            }
            catch (Exception)
            {
                return dtnull;
            }
        }

        /// <summary>   
        /// 根据需要得实体类信息   
        /// </summary>   
        /// <typeparam name="T">需要一个对象有一个无参数的实例化方法</typeparam>   
        /// <param name="dr">table数据源</param>   
        /// <returns>返回整理好了集合</returns>           
        public static List<T> GetEntityList<T>(IDataReader dr) where T : new()
        {
            List<T> entityList = new List<T>();

            int fieldCount = -1;

            while (dr.Read())
            {
                if (-1 == fieldCount)
                    fieldCount = dr.FieldCount;

                // 得到实体类对象   
                T t = (T)Activator.CreateInstance(typeof(T));

                for (int i = 0; i < fieldCount; i++)
                {
                    PropertyInfo prop = t.GetType().GetProperty(dr.GetName(i),
                        BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public);

                    if (null != prop)
                    {
                        // 为了能用在默认为null的值上   
                        // 如 DateTime? tt = null;   
                        if (null == dr[i] || Convert.IsDBNull(dr[i]))
                            prop.SetValue(t, null, null);
                        else
                            prop.SetValue(t, dr[i], null);
                    }
                }

                entityList.Add(t);
            }
            dr.Close();
            return entityList;
        }

        /// <summary>
        /// 执行存储过程，获取分页数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connectionString"></param>
        /// <param name="pageQuery">查询内容</param>
        /// <param name="paging">分页</param>
        /// <returns></returns>
        public static List<T> GetPageData<T>(string connectionString, PageQuery pageQuery, Paging paging) where T : new()
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();

                SqlCommand comm = new SqlCommand();
                comm.Connection = connection;
                comm.CommandType = CommandType.StoredProcedure;
                comm.CommandText = "CommonPageProc";
                comm.Parameters.Add("@TableName", SqlDbType.VarChar).Value = pageQuery.tableName;
                comm.Parameters.Add("@Fields", SqlDbType.VarChar).Value = pageQuery.fields;
                comm.Parameters.Add("@OrderField", SqlDbType.VarChar).Value = pageQuery.orderField;
                comm.Parameters.Add("@sqlWhere", SqlDbType.VarChar).Value = pageQuery.sqlWhere;
                comm.Parameters.Add("@pageSize", SqlDbType.Int).Value = paging.pageSize;
                comm.Parameters.Add("@pageIndex", SqlDbType.Int).Value = paging.pageIndex;
                SqlParameter paraTP = new SqlParameter("@TotalPage", SqlDbType.Int);
                paraTP.Direction = ParameterDirection.Output;
                paraTP.Value = paging.pageCount;
                comm.Parameters.Add(paraTP);
                SqlParameter param = new SqlParameter("@TotalRecord", SqlDbType.Int);
                param.Direction = ParameterDirection.ReturnValue;
                comm.Parameters.Add(param);
                List<T> ent = null;
                using (IDataReader dr = comm.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    ent = GetEntityList<T>(dr);
                }
                paging.pageCount = Convert.ToInt32(comm.Parameters["@TotalPage"].Value);
                paging.recordCount = Convert.ToInt32(param.Value);
                comm.Parameters.Clear();
                return ent;
            }
        }
        /// <summary>
        /// 针对客户端首页软加载列表调用存贮过程（异常在逻辑层处理created by wangcheng）
        /// </summary>
        /// <param name="connectionString">链接数据库字符串</param>
        /// <param name="pageQuery">查询内容</param>
        /// <param name="paging">分页</param>
        /// <returns></returns>
        public static IDataReader GetPageDataReader(string connectionString, PageQuery pageQuery, Paging paging)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            if (connection.State != ConnectionState.Open) connection.Open();
            SqlCommand comm = new SqlCommand();
            comm.Connection = connection;
            comm.CommandType = CommandType.StoredProcedure;
            comm.CommandText = "CommonPageProc";
            comm.Parameters.Add("@TableName", SqlDbType.VarChar).Value = pageQuery.tableName;
            comm.Parameters.Add("@Fields", SqlDbType.VarChar).Value = pageQuery.fields;
            comm.Parameters.Add("@OrderField", SqlDbType.VarChar).Value = pageQuery.orderField;
            comm.Parameters.Add("@sqlWhere", SqlDbType.VarChar).Value = pageQuery.sqlWhere;
            comm.Parameters.Add("@pageSize", SqlDbType.Int).Value = paging.pageSize;
            comm.Parameters.Add("@pageIndex", SqlDbType.Int).Value = paging.pageIndex;
            SqlParameter paraTP = new SqlParameter("@TotalPage", SqlDbType.Int);
            paraTP.Direction = ParameterDirection.Output;
            paraTP.Value = paging.pageCount;
            comm.Parameters.Add(paraTP);
            SqlParameter param = new SqlParameter("@TotalRecord", SqlDbType.Int);
            param.Direction = ParameterDirection.ReturnValue;
            comm.Parameters.Add(param);
            IDataReader dr = comm.ExecuteReader(CommandBehavior.CloseConnection);
            paging.pageCount = Convert.ToInt32(comm.Parameters["@TotalPage"].Value);
            paging.recordCount = Convert.ToInt32(param.Value);
            comm.Parameters.Clear();
            return dr;
        }

        /// <summary>
        /// 求返回的数据行数，加入存储过程
        /// </summary>
        public static int GetPaginationPagerTableCount(PaginationPager pg)
        {
            StringBuilder strSql = new StringBuilder();
            if (pg.strWhere.Contains("group") && pg.strWhere.Contains("by"))//含有group by排序
            {
                strSql.Append("select count(c) as dataCount from (select count(*) c ");
            }
            else
            {
                strSql.Append("select sum(c) as dataCount  from (select count(*) c ");
            }
            strSql.Append(" from " + pg.tblName + " ");
            strSql.Append(" where " + pg.strWhere + ")tmp ");
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return Convert.ToInt32(ds.Tables[0].Rows[0]["dataCount"]);
        }

        /// <summary>
        /// 修改状态
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static int UpdateStatus(string sql)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                SqlTransaction tran = null;
                Object obj = null;
                try
                {
                    conn.Open();
                    tran = conn.BeginTransaction();
                    obj = SqlHelper.ExecuteNonQuery(tran, CommandType.Text, sql.ToString());
                    tran.Commit();

                }
                catch
                {
                    if (tran != null)
                    {
                        tran.Rollback();
                    }
                }

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
        /// 插入退款日志
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="refundsum"></param>
        /// <param name="preOrder19dianId"></param>
        /// <returns></returns>
        public static bool InsertRefundData(long customerId, double refundsum, long preOrder19dianId, string note)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                Object obj = null;
                try
                {
                    string strSql = "insert into RefundLogData (customerId,refundSum,preOrder19dianId,refundTime,note) values (@customerId,@refundSum,@preOrder19dianId,@refundTime,@note)";
                    SqlParameter[] para = new SqlParameter[] { 
                    new SqlParameter("@customerId", SqlDbType.BigInt) { Value = customerId },
                    new SqlParameter("@refundSum", SqlDbType.Float) { Value = refundsum },
                    new SqlParameter("@preOrder19dianId", SqlDbType.BigInt) { Value = preOrder19dianId },
                    new SqlParameter("@refundTime", SqlDbType.DateTime) { Value = DateTime.Now },
                    new SqlParameter("@note", SqlDbType.NVarChar, 50) { Value = note }
                    };
                    obj = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql, para);
                }
                catch (Exception)
                {
                    return false;
                }
                if (obj == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        /// <summary>
        /// 插入员工日志操作
        /// </summary>
        /// <returns></returns>
        public static long InsertEmployeeOperateLogInfo(EmployeeOperateLogInfo employeeOperateLogInfo)
        {
            //此处调用的是VAGastronomistMobileAppLog数据库
            using (SqlConnection conn = new SqlConnection(SqlHelper.MobileAppLogConnectionStringLocalTransaction))
            {
                Object obj = null;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    SqlParameter[] parameters = null;
                    strSql.Append("insert into EmployeeOperateLog(");
                    strSql.Append("employeeId,employeeName,pageType,operateType,operateTime,operateDes)");
                    strSql.Append(" values (");
                    strSql.Append("@employeeId,@employeeName,@pageType,@operateType,@operateTime,@operateDes)");
                    strSql.Append(" select @@identity");
                    parameters = new SqlParameter[]{
                        new SqlParameter("@employeeId",SqlDbType.Int,4),
                        new SqlParameter("@employeeName",SqlDbType.NVarChar,100),
                        new SqlParameter("@pageType",SqlDbType.Int,4),
                        new SqlParameter("@operateType",SqlDbType.Int,4),
                        new SqlParameter("@operateTime",SqlDbType.DateTime),
                          new SqlParameter("@operateDes",SqlDbType.NVarChar,1000)
                    };
                    parameters[0].Value = employeeOperateLogInfo.employeeId;
                    parameters[1].Value = string.IsNullOrEmpty(employeeOperateLogInfo.employeeName)
                        ? ""
                        : employeeOperateLogInfo.employeeName;
                    parameters[2].Value = employeeOperateLogInfo.pageType;
                    parameters[3].Value = employeeOperateLogInfo.operateType;
                    parameters[4].Value = employeeOperateLogInfo.operateTime;
                    parameters[5].Value = employeeOperateLogInfo.operateDes;
                    obj = SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql.ToString(), parameters);
                    //LogDll.LogManager.WriteLog(LogFile.Error, "LOG成功!");
                }
                catch (Exception exc)
                {
                    LogDll.LogManager.WriteLog(LogFile.Error, exc.ToString());
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
        /// 批量插入
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public bool BatchInsert(DataTable dt)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
                {
                    conn.Open();
                    SqlTransaction sqlbulkTransaction = conn.BeginTransaction();
                    SqlBulkCopy sqlbulkcopy = new SqlBulkCopy(conn, SqlBulkCopyOptions.Default, sqlbulkTransaction);
                    sqlbulkcopy.DestinationTableName = dt.TableName;//数据库中的表名
                    sqlbulkcopy.BulkCopyTimeout = 30;
                    foreach (DataColumn dc in dt.Columns)
                    {
                        sqlbulkcopy.ColumnMappings.Add(dc.ColumnName, dc.ColumnName);
                    }
                    sqlbulkcopy.WriteToServer(dt);
                    sqlbulkTransaction.Commit();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
