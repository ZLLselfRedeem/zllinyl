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
    /// <summary>
    /// 宝箱配置
    /// 2014-7-29
    /// </summary>
    public class TreasureChestConfigManager
    {
        /// <summary>
        /// 新增宝箱
        /// </summary>
        /// <param name="treasureChest"></param>
        /// <returns></returns>
        public int InsertTreasureChest(TreasureChestConfig treasureChest)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert TreasureChestConfig(activityId,amount,count,min,max,quantity,remainQuantity,status,createTime,createdBy,amountRule,defaultAmountRange,defaultRateRange,newAmountRange,newRateRange,isPreventCheat) values");
            strSql.Append("(@activityId,@amount,@count,@min,@max,@quantity,@remainQuantity,@status,@createTime,@createdBy,@amountRule,@defaultAmountRange,@defaultRateRange,@newAmountRange,@newRateRange,@isPreventCheat)");
            strSql.Append(" select @@identity");
            try
            {
                SqlParameter[] para = new SqlParameter[] { 
                new SqlParameter("@activityId", SqlDbType.Int) { Value = treasureChest.activityId },
                new SqlParameter("@amount", SqlDbType.Float) { Value = treasureChest.amount },
                new SqlParameter("@count", SqlDbType.Int) { Value = treasureChest.count },
                new SqlParameter("@min", SqlDbType.Float) { Value = treasureChest.min },
                new SqlParameter("@max", SqlDbType.Float) { Value = treasureChest.max },
                new SqlParameter("@quantity", SqlDbType.Int) { Value = treasureChest.quantity },
                new SqlParameter("@remainQuantity", SqlDbType.Int) { Value = treasureChest.remainQuantity },
                new SqlParameter("@status", SqlDbType.Bit) { Value = 1 },
                new SqlParameter("@createTime", SqlDbType.DateTime) { Value = DateTime.Now },
                new SqlParameter("@createdBy", SqlDbType.Int) { Value = treasureChest.createdBy },
                new SqlParameter("@amountRule", SqlDbType.Int) { Value = treasureChest.amountRule },
                new SqlParameter("@defaultAmountRange", SqlDbType.NVarChar,100) { Value = treasureChest.defaultAmountRange },
                new SqlParameter("@defaultRateRange", SqlDbType.NVarChar,100) { Value = treasureChest.defaultRateRange },
                new SqlParameter("@newAmountRange", SqlDbType.NVarChar,100) { Value = treasureChest.newAmountRange },
                new SqlParameter("@newRateRange", SqlDbType.NVarChar,100) { Value = treasureChest.newRateRange },
                new SqlParameter("@isPreventCheat", SqlDbType.Bit) { Value = treasureChest.isPreventCheat }
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
        /// 修改宝箱配置
        /// </summary>
        /// <param name="treasureChest"></param>
        /// <returns></returns>
        public bool UpdateActivity(TreasureChestConfig treasureChest)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE [VAGastronomistMobileApp].[dbo].[TreasureChestConfig] SET");
            strSql.Append("[activityId] = @activityId,");
            strSql.Append("[amount] = @amount,");
            strSql.Append("[count] = @count,");
            strSql.Append("[min] = @min,");
            strSql.Append("[max] = @max,");
            strSql.Append("[quantity] = @quantity,");
            strSql.Append("[remainQuantity] = @remainQuantity,");
            strSql.Append("[updateTime] = @updateTime,");
            strSql.Append("[updatedBy] = @updatedBy,");
            strSql.Append("[amountRule] = @amountRule,");
            strSql.Append("[defaultAmountRange] = @defaultAmountRange,");
            strSql.Append("[defaultRateRange] = @defaultRateRange,");
            strSql.Append("[newAmountRange] = @newAmountRange,");
            strSql.Append("[newRateRange] = @newRateRange,");
            strSql.Append("[isPreventCheat] = @isPreventCheat");
            strSql.Append(" where treasureChestConfigId = @treasureChestConfigId");
            try
            {
                SqlParameter[] para = new SqlParameter[] { 
                new SqlParameter("@activityId", SqlDbType.Int) { Value = treasureChest.activityId },
                new SqlParameter("@amount", SqlDbType.Float) { Value = treasureChest.amount },
                new SqlParameter("@count", SqlDbType.Int) { Value = treasureChest.count },
                new SqlParameter("@min", SqlDbType.Float) { Value = treasureChest.min },
                new SqlParameter("@max", SqlDbType.Float) { Value = treasureChest.max },
                new SqlParameter("@quantity", SqlDbType.Int) { Value = treasureChest.quantity },
                new SqlParameter("@remainQuantity", SqlDbType.Int) { Value = treasureChest.remainQuantity },
                new SqlParameter("@updateTime", SqlDbType.DateTime) { Value = DateTime.Now },
                new SqlParameter("@updatedBy", SqlDbType.Int) { Value = treasureChest.createdBy },
                new SqlParameter("@amountRule", SqlDbType.Int) { Value = treasureChest.amountRule },
                new SqlParameter("@defaultAmountRange", SqlDbType.NVarChar,100) { Value = treasureChest.defaultAmountRange },
                new SqlParameter("@defaultRateRange", SqlDbType.NVarChar,100) { Value = treasureChest.defaultRateRange },
                new SqlParameter("@newAmountRange", SqlDbType.NVarChar,100) { Value = treasureChest.newAmountRange },
                new SqlParameter("@newRateRange", SqlDbType.NVarChar,100) { Value = treasureChest.newRateRange },
                new SqlParameter("@isPreventCheat", SqlDbType.Bit) { Value = treasureChest.isPreventCheat },
                new SqlParameter("@treasureChestConfigId", SqlDbType.Int) { Value = treasureChest.treasureChestConfigId }
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
        /// 删除指定宝箱
        /// </summary>
        /// <param name="treasureChestConfigId"></param>
        /// <returns></returns>
        public bool DeleteTreasureChest(int treasureChestConfigId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update TreasureChestConfig set status = 0");
            strSql.Append(" where treasureChestConfigId = @treasureChestConfigId");
            try
            {
                SqlParameter[] para = new SqlParameter[] { 
                new SqlParameter("@treasureChestConfigId", SqlDbType.Int) { Value = treasureChestConfigId }
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
        /// 根据宝箱Id查询相应宝箱
        /// </summary>
        /// <param name="treasureChestConfigId"></param>
        /// <returns></returns>
        public TreasureChestConfig QueryTreasureChest(int treasureChestConfigId)
        {
            TreasureChestConfig treasureChest = null;
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select treasureChestConfigId,activityId,amount,count,min,max,quantity,remainQuantity,status,amountRule,defaultAmountRange,defaultRateRange,newAmountRange,newRateRange,isPreventCheat");
            strSql.Append(" from TreasureChestConfig");
            strSql.Append(" where status = 1 and treasureChestConfigId = @treasureChestConfigId order by treasureChestConfigId desc");
            try
            {
                SqlParameter[] para = new SqlParameter[] { 
                    new SqlParameter("@treasureChestConfigId", SqlDbType.Int) { Value = treasureChestConfigId }
                };
                using (SqlDataReader sdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), para))
                {
                    if (sdr.Read())
                    {
                        treasureChest = SqlHelper.GetEntity<TreasureChestConfig>(sdr);
                    }
                }
                return treasureChest;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 查询所有宝箱配置
        /// </summary>
        /// <param name="page"></param>
        /// <param name="activityId"></param>
        /// <returns></returns>
        public List<TreasureChestConfig> QueryTreasureChest(SQLServerDAL.Persistence.Infrastructure.Page page, out int cnt, int activityId = 0)
        {
            List<TreasureChestConfig> treasureChests = new List<TreasureChestConfig>();
            StringBuilder strSql = new StringBuilder();
            StringBuilder strCnt = new StringBuilder();
            strCnt.Append(" select count(1) from TreasureChestConfig t inner join Activity act");
            strCnt.Append(" on t.activityId = act.activityId and t.status = 1 and act.status = 1");

            strSql.Append("select a.treasureChestConfigId,a.activityId,a.amount,a.count,a.min,a.max,a.quantity,a.amountRule,a.defaultAmountRange,a.defaultRateRange,a.newAmountRange,a.newRateRange,a.isPreventCheat,a.activityName from (");
            strSql.Append("select ROW_NUMBER() over (order by t.activityId desc) rownum,");
            strSql.Append(" t.treasureChestConfigId,t.activityId,t.amount,t.count,t.min,t.max,t.quantity,t.amountRule,t.defaultAmountRange,t.defaultRateRange,t.newAmountRange,t.newRateRange,t.isPreventCheat,act.name activityName");
            strSql.Append(" from TreasureChestConfig t inner join Activity act");
            strSql.Append(" on t.activityId = act.activityId and t.status = 1 and act.status = 1");
            if (activityId > 0)
            {
                strCnt.Append(" and t.activityId = @activityId");
                strSql.Append(" and t.activityId = @activityId");
            }
            strSql.Append(") a");
            strSql.Append(" where a.rownum between @startIndex and @endIndex");
            try
            {
                List<SqlParameter> paraList = new List<SqlParameter>();
                if (activityId > 0)
                {
                    paraList.Add(new SqlParameter("@activityId", SqlDbType.Int) { Value = activityId });
                }
                SqlParameter[] paraCnt = paraList.ToArray();
                paraList.Add(new SqlParameter("@startIndex", SqlDbType.Int) { Value = page.Skip + 1 });
                paraList.Add(new SqlParameter("@endIndex", SqlDbType.Int) { Value = page.Skip + page.PageSize });

                SqlParameter[] para = paraList.ToArray();

                object objCnt = SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strCnt.ToString(), para);
                if (objCnt == null)
                {
                    cnt = 0;
                }
                else
                {
                    cnt = Convert.ToInt32(objCnt);
                }
                using (SqlDataReader sdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), para))
                {
                    while (sdr.Read())
                    {
                        treasureChests.Add(SqlHelper.GetEntity<TreasureChestConfig>(sdr));
                    }
                    return treasureChests;
                }
            }
            catch (Exception)
            {
                cnt = 0;
                return null;
            }
        }

        /// <summary>
        /// 根据活动Id查询宝箱配置
        /// </summary>
        /// <param name="activityId"></param>
        /// <returns></returns>
        public TreasureChestConfig QueryConfigOfActivity(int activityId)
        {
            TreasureChestConfig Config = null;
            const string strSql = @"select treasureChestConfigId,activityId,amount,count,min,max,quantity,remainQuantity,status,amountRule,defaultAmountRange,defaultRateRange,newAmountRange,newRateRange,isPreventCheat
                                            from TreasureChestConfig
                                            where status = 1 and activityId = @activityId order by treasureChestConfigId desc";
             SqlParameter[] para = new SqlParameter[] { 
                    new SqlParameter("@activityId", SqlDbType.Int) { Value = activityId }
                };
                using (SqlDataReader sdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), para))
                {
                    if (sdr.Read())
                    {
                        Config = SqlHelper.GetEntity<TreasureChestConfig>(sdr);
                    }
                }
                return Config;
            }
    }
}
