using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.DBUtility;
using VAGastronomistMobileApp.Model;

namespace VAGastronomistMobileApp.SQLServerDAL
{
    public class DemoManager
    {
        /// <param name="table">准备更新的DataTable新数据。</param>
        /// <param name="columnsName">对应要更新的列的列名集合。</param>
        /// <param name="selectLimitWhere">需要在sql的where条件中限定的条件字符串，可为空。table数据集必须是selectLimitWhere条件限定查询子集。</param>
        /// <param name="onceUpdateNumber">批处理一次执行的行数。</param>
        /// <returns>返回更新的行数</returns>
        public int BatchUpdate(DataTable table, List<DemoModel> columnModels, string selectLimitWhere, int onceUpdateNumber)
        {
            if (String.IsNullOrWhiteSpace(table.TableName)) return 0;
            if (!columnModels.Any()) return 0;
            int result = 0;
            using (SqlConnection sqlconn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                if (sqlconn.State != ConnectionState.Open) sqlconn.Open();
                //使用加强读写锁事务   
                SqlTransaction tran = sqlconn.BeginTransaction(IsolationLevel.ReadCommitted);
                try
                {
                    table.AcceptChanges();//设置，否则SetModified() 方法异常
                    foreach (DataRow dr in table.Rows)
                    {
                        //所有行设为修改状态   
                        dr.SetModified();
                    }

                    string columnsUpdateSql = "";
                    string columsSelectSql = "";
                    string columsWhereSql = "";
                    SqlParameter[] paras = new SqlParameter[columnModels.Count];

                    //需要更新的列设置参数是,参数名为"@+列名"
                    for (int i = 0; i < columnModels.Count; i++)
                    {
                        columsSelectSql += columnModels[i].ColumName + ",";
                        if (columnModels[i].IsWhere == true)
                        {
                            columsWhereSql += ("[" + columnModels[i].ColumName + "]=@" + columnModels[i].ColumName + ",");
                        }
                        else
                        {
                            columnsUpdateSql += ("[" + columnModels[i].ColumName + "]=@" + columnModels[i].ColumName + ",");
                        }
                        paras[i] = new SqlParameter("@" + columnModels[i].ColumName, columnModels[i].SqlDbType, columnModels[i].Size, columnModels[i].ColumName);
                    }

                    columsSelectSql = columsSelectSql.TrimEnd(',');
                    columnsUpdateSql = columnsUpdateSql.TrimEnd(',');
                    columsWhereSql = columsWhereSql.TrimEnd(',');
                    //为Adapter定位目标表   
                    SqlCommand cmd = String.IsNullOrWhiteSpace(selectLimitWhere) ?
                        new SqlCommand(string.Format("select {0} from {1}", columsSelectSql, table.TableName), sqlconn, tran) :
                        new SqlCommand(string.Format("select {0} from {1} where {2}", columsSelectSql, table.TableName, selectLimitWhere), sqlconn, tran);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    SqlCommandBuilder sqlCmdBuilder = new SqlCommandBuilder(da);
                    da.AcceptChangesDuringUpdate = false;

                    SqlCommand updateCmd = new SqlCommand(string.Format(" UPDATE [{0}] SET {1} WHERE {2} ", table.TableName, columnsUpdateSql, columsWhereSql));
                    //不修改源DataTable   
                    updateCmd.UpdatedRowSource = UpdateRowSource.None;
                    da.UpdateCommand = updateCmd;
                    da.UpdateCommand.Parameters.AddRange(paras);
                    da.UpdateBatchSize = onceUpdateNumber;
                    result = da.Update(table);
                    table.AcceptChanges();
                    tran.Commit();
                }
                catch
                {
                    tran.Rollback();
                }
                finally
                {
                    sqlconn.Dispose();
                    sqlconn.Close();
                }
            }
            return result;
        }
        public int Update(DataTable dt)
        {
            int res = 0;
            using (SqlConnection sqlconn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                if (sqlconn.State != ConnectionState.Open)
                {
                    sqlconn.Open();
                }
                SqlTransaction tran = sqlconn.BeginTransaction(IsolationLevel.ReadCommitted);
                try
                {
                    dt.AcceptChanges();
                    foreach (DataRow dr in dt.Rows)
                    {
                        dr.SetModified();
                    }
                    //为Adapter定位目标表 
                    SqlCommand cmd = new SqlCommand("select * from " + dt.TableName + " where 1=1", sqlconn, tran);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    SqlCommandBuilder sqlCmdBuilder = new SqlCommandBuilder(da);
                    da.AcceptChangesDuringUpdate = false;
                    SqlCommand updatecmd = new SqlCommand("UPDATE " + dt.TableName + " SET [couponName] = @couponName where couponID=@couponID");
                    //不修改源DataTable   
                    updatecmd.UpdatedRowSource = UpdateRowSource.None;
                    da.UpdateCommand = updatecmd;
                    da.UpdateCommand.Parameters.Add("@couponName", SqlDbType.NVarChar, 500, "couponName");
                    da.UpdateCommand.Parameters.Add("@couponID", SqlDbType.BigInt, 8, "couponID");
                    da.UpdateBatchSize = 10;
                    res = da.Update(dt);
                    dt.AcceptChanges();
                    tran.Commit();
                    sqlconn.Close();
                }
                catch
                {
                    tran.Rollback();
                    return -1;
                }
            }
            return res;
        }
        public DataTable GetInitData()
        {
            const string strSql = @"SELECT TOP 100 [couponID]
      ,[couponName]
      ,[couponDesc]
      ,[couponDisplayStartTime]
      ,[couponDisplayEndTime]
      ,[couponValidStartTime]
      ,[couponValidEndTime]
      ,[couponCreatTime]
      ,[couponRefreshTime]
      ,[couponStatus]
      ,[couponType]
      ,[originaQuantity]
      ,[currentQuantity]
      ,[originalPrice]
      ,[currentPrice]
      ,[companyId]
      ,[couponRequirementType]
      ,[isVIPOnly]
      ,[couponDownloadPrice]

  FROM [VAGastronomistMobileApp].[dbo].[CouponInfo]";
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }
    }
}
