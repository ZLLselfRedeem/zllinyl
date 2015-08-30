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
    public class IntegrationRuleManager
    {
        /// <summary>
        /// 插入记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Insert(IntegrationRule model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("INSERT INTO IntegrationRule");
            strSql.Append("(ID,Description,Integration,RuleTypeID");
            strSql.Append(" ,EventType,EventComplement,ConditionalMinValue ,ConditionalMaxValue");
            strSql.Append(" ,Status,CreateUser,CreateDate,UpdateUser,UpdateDate)");
            strSql.Append("VALUES(@ID,@Description,@Integration,@RuleTypeID");
            strSql.Append(" ,@EventType,@EventComplement,@ConditionalMinValue ,@ConditionalMaxValue");
            strSql.Append(" ,@Status,@CreateUser,@CreateDate,@UpdateUser,@UpdateDate)");

            SqlParameter[] parameter = new SqlParameter[]{
                new SqlParameter("@ID",SqlDbType.UniqueIdentifier){Value=Guid.NewGuid()},
                new SqlParameter("@Description",SqlDbType.NVarChar,100){Value=model.Description},
                new SqlParameter("@Integration",SqlDbType.Int){Value=model.Integration},
                new SqlParameter("@RuleTypeID",SqlDbType.UniqueIdentifier){Value=model.RuleTypeID},
                new SqlParameter("@EventType",SqlDbType.Int){Value=model.EventType},
                new SqlParameter("@EventComplement",SqlDbType.Int){Value=model.EventComplement},
                new SqlParameter("@ConditionalMinValue",SqlDbType.Int){Value=model.ConditionalMinValue},
                new SqlParameter("@ConditionalMaxValue",SqlDbType.Int){Value=model.ConditionalMaxValue},
                new SqlParameter("@Status",SqlDbType.Int){Value=model.Status},
                new SqlParameter("@CreateUser",SqlDbType.Int){Value=model.CreateUser},
                new SqlParameter("@CreateDate",SqlDbType.DateTime){Value=model.CreateDate},
                new SqlParameter("@UpdateUser",SqlDbType.Int){Value=model.UpdateUser},
                new SqlParameter("@UpdateDate",SqlDbType.DateTime){Value=model.UpdateDate}
            };
            object obj = Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameter));
            if (obj == null)
            {
                return 0;
            }
            return 1;
        }

        public int Update(IntegrationRule model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from IntegrationRule");
            strSql.Append(" where ID=@ID");

            SqlParameter[] parameter = new SqlParameter[]{
                new SqlParameter("ID",SqlDbType.UniqueIdentifier){Value=model.ID}
            };
            string strOld = string.Empty;
            string strNew = string.Empty;
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameter);
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    strOld += dt.Columns[i].ColumnName + "=" + dt.Rows[0][i].ToString() + ",";
                }
            }
            else
            {
                return 0;
            }

            StringBuilder strSqlUpdate = new StringBuilder();
            strSqlUpdate.Append("Update IntegrationRule Set ");
            strSqlUpdate.Append(" Description=@Description,Integration=@Integration,RuleTypeID=@RuleTypeID");
            strSqlUpdate.Append(",EventType=@EventType,EventComplement=@EventComplement");
            strSqlUpdate.Append(",ConditionalMinValue=@ConditionalMinValue,ConditionalMaxValue=@ConditionalMaxValue");
            strSqlUpdate.Append(",Status=@Status,UpdateUser=@UpdateUser,UpdateDate=@UpdateDate");
            strSqlUpdate.Append(" where ID=@ID");

            parameter = new SqlParameter[]{
                new SqlParameter("@ID",SqlDbType.UniqueIdentifier){Value=model.ID},
                new SqlParameter("@Description",SqlDbType.NVarChar,100){Value=model.Description},
                new SqlParameter("@Integration",SqlDbType.Int){Value=model.Integration},
                new SqlParameter("@RuleTypeID",SqlDbType.UniqueIdentifier){Value=model.RuleTypeID},
                new SqlParameter("@EventType",SqlDbType.Int){Value=model.EventType},
                new SqlParameter("@EventComplement",SqlDbType.Int){Value=model.EventComplement},
                new SqlParameter("@ConditionalMinValue",SqlDbType.Int){Value=model.ConditionalMinValue},
                new SqlParameter("@ConditionalMaxValue",SqlDbType.Int){Value=model.ConditionalMaxValue},
                new SqlParameter("@Status",SqlDbType.Int){Value=model.Status},
                new SqlParameter("@UpdateUser",SqlDbType.Int){Value=model.UpdateUser},
                new SqlParameter("@UpdateDate",SqlDbType.DateTime){Value=model.UpdateDate}
            };
            object obj = Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSqlUpdate.ToString(), parameter));
            if (obj == null)
            {
                return 0;
            }

            parameter = new SqlParameter[]{
                new SqlParameter("ID",SqlDbType.UniqueIdentifier){Value=model.ID}
            };
            ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameter);
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    strNew += dt.Columns[i].ColumnName + "=" + dt.Rows[0][i].ToString() + ",";
                }
            }

            strSql.Length = 0;
            strSql.Append(" Insert into IntegrationRuleLog ");
            strSql.Append(" (OperateUser,operateDate,OldData,NewData)");
            strSql.Append(" Values(@OperateUser,@operateDate,@OldData,@NewData)");
            parameter = new SqlParameter[]{
                new SqlParameter("@OperateUser",SqlDbType.Int){Value=model.UpdateUser},
                new SqlParameter("@operateDate",SqlDbType.DateTime){Value=model.UpdateDate},
                new SqlParameter("@OldData",SqlDbType.Text){Value=strOld},
                new SqlParameter("@NewData",SqlDbType.Text){Value=strNew}
             };
            SqlHelper.ExecuteScalar(SqlHelper.MobileAppLogConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameter);

            return 1;
        }

        /// <summary>
        /// 修改状态，删除为-1
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateStatus(IntegrationRule model)
        {
            StringBuilder strSqlUpdate = new StringBuilder();
            strSqlUpdate.Append("Update IntegrationRule Set ");
            strSqlUpdate.Append(" Status=@Status,UpdateUser=@UpdateUser,UpdateDate=@UpdateDate");
            strSqlUpdate.Append(" where ID=@ID");

            SqlParameter[] parameter = new SqlParameter[]{
                new SqlParameter("@ID",SqlDbType.UniqueIdentifier){Value=model.ID},
                new SqlParameter("@Status",SqlDbType.Int){Value=model.Status},
                new SqlParameter("@UpdateUser",SqlDbType.Int){Value=model.UpdateUser},
                new SqlParameter("@UpdateDate",SqlDbType.DateTime){Value=model.UpdateDate}
            };
            object obj = Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSqlUpdate.ToString(), parameter));
            if (obj == null)
            {
                return 0;
            }

            return 1;
        }

        public DataTable Integrations(Guid RuleTypeID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT a.ID,Description,Integration,b.Name RuleTypeName");
            strSql.Append(" ,EventType,EventComplement,ConditionalMinValue ,ConditionalMaxValue");
            strSql.Append(" ,a.Status,CreateUser,CreateDate,UpdateUser,UpdateDate");
            strSql.Append(" FROM IntegrationRule a");
            strSql.Append(" inner join RuleType b on a.RuleTypeID=b.ID");
            strSql.Append(" where a.Status>=0 ");

            List<SqlParameter> parameter = new List<SqlParameter>();
            if (RuleTypeID != new Guid())
            {
                strSql.Append(" and a.RuleTypeID=@RuleTypeID");
                parameter.Add(new SqlParameter("@RuleTypeID",SqlDbType.UniqueIdentifier){Value=RuleTypeID});
            }

            strSql.Append(" order by CreateDate desc ");

            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameter.ToArray());
            return ds.Tables[0];
        }

        public DataTable IntegrationDetail(Guid ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT ID,Description,Integration,RuleTypeID");
            strSql.Append(" ,EventType,EventComplement,ConditionalMinValue ,ConditionalMaxValue");
            strSql.Append(" ,Status,CreateUser,CreateDate,UpdateUser,UpdateDate");
            strSql.Append(" FROM IntegrationRule");
            strSql.Append(" where ID=@ID");

            SqlParameter[] parameter = new SqlParameter[]{
                new SqlParameter("@ID",SqlDbType.UniqueIdentifier){Value=ID}
            };

            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameter);
            return ds.Tables[0];
        }

        public DataTable Integrations(string strRule)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT ID,Description,Integration,RuleTypeID");
            strSql.Append(" ,EventType,EventComplement,ConditionalMinValue ,ConditionalMaxValue");
            strSql.Append(" ,Status,CreateUser,CreateDate,UpdateUser,UpdateDate");
            strSql.Append(" FROM IntegrationRule");
            strSql.Append(" where Status>=0 ");
            strSql.AppendFormat(" and ID in ({0})", strRule);

            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }

        public DataTable RuleTypes()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from RuleType where Status>0 ");

            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }

        public DataTable IntegrationRules(Guid RuleTypeID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from IntegrationRule where RuleTypeID=@RuleTypeID and Status>0 ");

            SqlParameter[] parameter = new SqlParameter[]{
                new SqlParameter("@RuleTypeID",SqlDbType.UniqueIdentifier){Value=RuleTypeID}
            };

            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameter);
            return ds.Tables[0];
        }

        public DataTable SumIntegration(int CityID, Guid RuleID, Guid RuleTypeID, DateTime BeginDate, DateTime EndDate)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select isnull(SUM(a.Integration),0) Integration from CustomerIntegrationDetail a ");
            strSql.Append(" inner join CustomerInfo b on a.CustomerID=b.CustomerID ");
            strSql.Append(" left join IntegrationRule c on c.ID=a.RuleID ");
            strSql.Append(" where a.CreateDate between @BeginDate and @EndDate");

            List<SqlParameter> para = new List<SqlParameter>();
           
            para.Add(new SqlParameter("@BeginDate", SqlDbType.DateTime) { Value = BeginDate });
            para.Add(new SqlParameter("@EndDate", SqlDbType.DateTime) { Value = EndDate });

            if (RuleTypeID == new Guid())
            {
                strSql.Append(" and a.RuleID=@RuleID ");
                para.Add(new SqlParameter("@RuleID", SqlDbType.UniqueIdentifier) { Value = new Guid() });
            }
            else
            {
                strSql.Append(" and c.RuleTypeID=@RuleTypeID ");
                para.Add(new SqlParameter("@RuleTypeID", SqlDbType.UniqueIdentifier) { Value = RuleTypeID });

                if (RuleID != new Guid())
                {
                    strSql.Append(" and a.RuleID=@RuleID ");
                    para.Add(new SqlParameter("@RuleID", SqlDbType.UniqueIdentifier) { Value = RuleID });
                }
            }
            if (CityID != 0)
            {
                strSql.Append(" and b.registerCityId=@registerCityId ");
                para.Add(new SqlParameter("registerCityId", SqlDbType.Int) { Value = CityID });
            }

            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), para.ToArray());
            return ds.Tables[0];
        }
    }
}
