using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Transactions;
using VAGastronomistMobileApp.DBUtility;
using VAGastronomistMobileApp.Model;

namespace VAGastronomistMobileApp.SQLServerDAL
{
    public class CustomerIntegrationDetailManager
    {
        public int Insert(CustomerIntegrationDetail model, Guid CustomerIntegrationID)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("INSERT INTO CustomerIntegrationDetail");
                strSql.Append("(ID,CustomerID,Description,RuleID,SubID");
                strSql.Append(" ,Integration,CurrentIntegration,CreateDate,CreateUser)");
                strSql.Append("VALUES(@ID,@CustomerID,@Description,@RuleID,@SubID");
                strSql.Append(" ,@Integration,@CurrentIntegration,@CreateDate,@CreateUser)");

                SqlParameter[] parameter = new SqlParameter[]{
                    new SqlParameter("@ID",SqlDbType.UniqueIdentifier){Value=Guid.NewGuid()},
                    new SqlParameter("@CustomerID",SqlDbType.BigInt){Value=model.CustomerID},
                    new SqlParameter("@Description",SqlDbType.NVarChar,100){Value=model.Description},
                    new SqlParameter("@RuleID",SqlDbType.UniqueIdentifier){Value=model.RuleID},
                    new SqlParameter("@SubID",SqlDbType.UniqueIdentifier){Value=model.SubID},
                    new SqlParameter("@Integration",SqlDbType.Int){Value=model.Integration},
                    new SqlParameter("@CurrentIntegration",SqlDbType.Int){Value=model.CurrentIntegration},
                    new SqlParameter("@CreateDate",SqlDbType.DateTime){Value=model.CreateDate},
                    new SqlParameter("@CreateUser",SqlDbType.Int){Value=model.CreateUser}
                };
                object obj = Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameter));
                if (obj == null)
                {
                    return 0;
                }

                if (CustomerIntegrationID == new Guid())
                {
                    strSql = new StringBuilder();
                    strSql.Append("INSERT INTO CustomerIntegration");
                    strSql.Append("(ID,CustomerID,Integration,UpdateDate)");
                    strSql.Append("VALUES(@ID,@CustomerID,@Integration,@UpdateDate)");

                    parameter = new SqlParameter[]{
                    new SqlParameter("@ID",SqlDbType.UniqueIdentifier){Value=Guid.NewGuid()},
                    new SqlParameter("@CustomerID",SqlDbType.BigInt){Value=model.CustomerID},
                    new SqlParameter("@Integration",SqlDbType.Int){Value=model.Integration},
                    new SqlParameter("@UpdateDate",SqlDbType.DateTime){Value=model.CreateDate}
                };
                    obj = Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameter));
                    if (obj == null)
                    {
                        return 0;
                    }
                }
                else
                {
                    strSql = new StringBuilder();
                    strSql.Append("Update CustomerIntegration");
                    strSql.Append(" set Integration=Integration+@Integration,UpdateDate=@UpdateDate");
                    strSql.Append(" where ID=@ID");

                    parameter = new SqlParameter[]{
                    new SqlParameter("@ID",SqlDbType.UniqueIdentifier){Value=CustomerIntegrationID},
                    new SqlParameter("@Integration",SqlDbType.Int){Value=model.Integration},
                    new SqlParameter("@UpdateDate",SqlDbType.DateTime){Value=model.CreateDate}
                };
                    obj = Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameter));
                    if (obj == null)
                    {
                        return 0;
                    }
                }
                scope.Complete();
                return 1;
            }
        }

        public int CountDetail(int CityID, DateTime BeginDate, DateTime EndDate, long CustomerID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select Count(*) counts");
            strSql.Append(" from CustomerIntegrationDetail where CreateDate>=@BeginDate and CreateDate<=@EndDate and CustomerID=@CustomerID ");

            List<SqlParameter> parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("@BeginDate", SqlDbType.DateTime) { Value = BeginDate });
            parameter.Add(new SqlParameter("@EndDate", SqlDbType.DateTime) { Value = EndDate });
            parameter.Add(new SqlParameter("@CustomerID", SqlDbType.BigInt) { Value = CustomerID });
            //if (CityID != 0)
            //{
            //    strSql.Append(" and CityID=@CityID");
            //    parameter.Add(new SqlParameter("RuleType", SqlDbType.Int) { Value = CityID });
            //}

            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameter.ToArray());
            return Convert.ToInt32(ds.Tables[0].Rows[0][0]);
        }


        public DataTable CustomerIntegrationDetails(int CityID, DateTime BeginDate, DateTime EndDate, long CustomerID, int str, int end)
        {
            StringBuilder strSql = new StringBuilder();
           
            strSql.Append("select * from (");
            strSql.Append(" select ROW_NUMBER() OVER(ORDER BY CreateDate DESC) AS RowNo,");
            strSql.Append(" CreateDate,RuleID,Description,Integration,CurrentIntegration,CreateUser");
            strSql.Append(" from CustomerIntegrationDetail where CreateDate>=@BeginDate and CreateDate<=@EndDate and CustomerID=@CustomerID) a");
            strSql.AppendFormat(" where a.RowNo between {0} and {1}", str, end);

            List<SqlParameter> parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("@BeginDate", SqlDbType.DateTime) { Value = BeginDate });
            parameter.Add(new SqlParameter("@EndDate", SqlDbType.DateTime) { Value = EndDate });
            parameter.Add(new SqlParameter("@CustomerID", SqlDbType.BigInt) { Value = CustomerID });
            //if (CityID != 0)
            //{
            //    strSql.Append(" and CityID=@CityID");
            //    parameter.Add(new SqlParameter("RuleType", SqlDbType.Int) { Value = CityID });
            //}

            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameter.ToArray());
            return ds.Tables[0];
        }

        public DataTable Rules()
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("select * from dbo.RuleType ");

            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }

        /// <summary>
        /// 用户积分总表
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <returns></returns>
        public DataTable CustomerIntegration(long CustomerID)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("select * from  CustomerIntegration where CustomerID=@CustomerID");

            List<SqlParameter> parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("@CustomerID", SqlDbType.BigInt) { Value = CustomerID });

            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameter.ToArray());
            return ds.Tables[0];
        }
    }
}
