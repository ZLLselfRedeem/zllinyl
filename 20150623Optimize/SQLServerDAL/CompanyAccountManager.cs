using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.Model;
using System.Data.SqlClient;
using VAGastronomistMobileApp.DBUtility;
using System.Data;

namespace VAGastronomistMobileApp.SQLServerDAL
{
    public class CompanyAccountManager
    {
        /// <summary>
        /// 新增一条账户记录
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool InsertNewAccountInfo(CompanyAccountInfo account)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into CompanyAccounts(");
                strSql.Append("companyId,accountNum,bankName,payeeBankName,remark,accountName,status)");
                strSql.Append(" values (");
                strSql.Append("@companyId,@accountNum,@bankName,@payeeBankName,@remark,@accountName,@status)");
                SqlParameter[] parameters = {
					new SqlParameter("@companyId", SqlDbType.Int,4),
					new SqlParameter("@accountNum", SqlDbType.VarChar,40),
					new SqlParameter("@bankName", SqlDbType.NVarChar,100),
					new SqlParameter("@payeeBankName", SqlDbType.NVarChar,100),
					new SqlParameter("@remark", SqlDbType.NVarChar,200),
                    new SqlParameter("@accountName",SqlDbType.NVarChar,50),
                    new SqlParameter("@status",SqlDbType.Int,4)};
                parameters[0].Value = account.companyId;
                parameters[1].Value = account.accountNum;
                parameters[2].Value = account.bankName;
                parameters[3].Value = account.payeeBankName;
                parameters[4].Value = account.remark;
                parameters[5].Value = account.accountName;
                parameters[6].Value = account.status;

                int result = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);

                if (result > 0)
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
        /// 通过公司查询账户信息
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public DataTable SelectAccountByCompanyId(int companyId)
        {
            StringBuilder strSql = new StringBuilder();
            SqlParameter[] param ={
                                      new SqlParameter("@companyId",companyId)     
                                  };
            strSql.Append("SELECT A.[identity_Id],B.[companyName],A.[accountNum],A.[bankName],A.[remark],A.accountName");
            strSql.Append(" from [companyAccounts] A inner join CompanyInfo B on A.CompanyId=B.companyId");
            strSql.Append("  where A.companyId =@companyId and status=1");
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), param);
            return ds.Tables[0];
        }
        /// <summary>
        /// 通过公司查询账户信息 add by wangc 20140516
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <returns></returns>
        public List<CompanyAccountInfo> SelectAccountListByCompanyId(int companyId)
        {
            StringBuilder strSql = new StringBuilder();
            SqlParameter[] param ={
                                      new SqlParameter("@companyId",companyId)     
                                  };
            strSql.Append("SELECT A.[identity_Id],B.[companyName],A.[accountNum],A.[bankName],A.[remark],A.accountName,A.payeeBankName");
            strSql.Append(" from [companyAccounts] A inner join CompanyInfo B on A.CompanyId=B.companyId");
            strSql.Append("  where A.companyId =@companyId and A.status=1 order by A.[identity_Id] desc");
            List<CompanyAccountInfo> list = new List<CompanyAccountInfo>();
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), param))
            {
                while (dr.Read())
                {
                    list.Add(new CompanyAccountInfo()
                    {
                        accountName = dr["accountName"] == DBNull.Value ? "" : Convert.ToString(dr["accountName"]),
                        accountNum = dr["accountNum"] == DBNull.Value ? "" : Convert.ToString(dr["accountNum"]),
                        bankName = dr["bankName"] == DBNull.Value ? "" : Convert.ToString(dr["bankName"]),
                        companyId = companyId,
                        identity_Id = dr["identity_Id"] == DBNull.Value ? 0 : Convert.ToInt32(dr["identity_Id"]),
                        payeeBankName = dr["payeeBankName"] == DBNull.Value ? "" : Convert.ToString(dr["payeeBankName"]),
                        remark = dr["remark"] == DBNull.Value ? "" : Convert.ToString(dr["remark"]),
                        status = 1//上面过滤条件了，肯定为1，不需要判断了
                    });
                }
            }
            return list;
        }
        /// <summary>
        /// 通过公司查询账户信息
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public DataTable SelectAccountNameAndAccountNumByCompanyId(int companyId)
        {
            StringBuilder strSql = new StringBuilder();
            SqlParameter[] param ={
                                      new SqlParameter("@companyId",companyId)     
                                  };
            strSql.Append("SELECT A.[identity_Id],A.[bankName]+A.[accountNum] as accountStr");
            strSql.Append(" from [companyAccounts] A ");
            strSql.Append("  where A.companyId =@companyId and status=1");
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), param);
            return ds.Tables[0];
        }
        /// <summary>
        /// 通过帐号查询账户信息
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public DataTable SelectAccountByAccountNum(string accountNum, int companyId)
        {
            StringBuilder strSql = new StringBuilder();
            SqlParameter[] param ={
                                      new SqlParameter("@accountNum",accountNum),
                                      new SqlParameter("@companyId",companyId)
                                  };
            strSql.Append("SELECT [accountNum],[bankName],[remark]");
            strSql.Append(" from [companyAccounts]");
            strSql.Append("  where accountNum =@accountNum and companyId=@companyId and status=1");
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), param);
            return ds.Tables[0];
        }
        /// <summary>
        /// 通过账户id查询账户信息
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public DataTable SelectAccountById(int accountId)
        {
            StringBuilder strSql = new StringBuilder();
            SqlParameter[] param ={
                                      new SqlParameter("@identity_Id",accountId)     
                                  };
            strSql.Append("SELECT [accountNum],[bankName],payeeBankName,[remark],accountName,companyId");
            strSql.Append(" from [companyAccounts]");
            strSql.Append("  where identity_Id =@identity_Id and status=1");
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), param);
            return ds.Tables[0];
        }
        /// <summary>
        /// 更新一条账户记录
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool UpdateAccountInfo(long identity_Id, string accountNum, string bankName, string remark, string accountName)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                StringBuilder strSql = new StringBuilder();
                SqlParameter[] parameters = {
                        new SqlParameter("@identity_Id",identity_Id),
                        new SqlParameter("@accountNum",accountNum),
                        new SqlParameter("@bankName",bankName),
                        new SqlParameter("@remark",remark),
                        new SqlParameter("@accountName",accountName)
                        };
                strSql.Append("update CompanyAccounts set");
                strSql.Append(" accountNum=@accountNum,bankName=@bankName,remark=@remark,accountName=@accountName");
                strSql.Append(" where identity_Id=@identity_Id");
                int result = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);
                if (result >= 1)
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
        /// 更新一条账户记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateAccountInfo(CompanyAccountInfo model,DataTable dt)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int Flag = 0;
                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow dr=dt.Rows[0];
                    //四个基本信息是否做过修改
                    if (model.accountName.Equals(dr["accountName"].ToString()) && model.accountNum.Equals(dr["accountNum"].ToString())
                        && model.bankName.Equals(dr["bankName"].ToString()) && model.payeeBankName.Equals(dr["payeeBankName"].ToString()))
                    {
                        Flag = 0;
                    }
                    else
                    {
                        Flag = 1;
                    }
                }
                StringBuilder strSql = new StringBuilder();
                SqlParameter[] parameters = {
                        new SqlParameter("@identity_Id",model.identity_Id),
                        new SqlParameter("@accountNum",model.accountNum),
                        new SqlParameter("@bankName",model.bankName),
                        new SqlParameter("@payeeBankName",model.payeeBankName),
                        new SqlParameter("@remark",model.remark),
                        new SqlParameter("@accountName",model.accountName)
                        };
                strSql.Append("update CompanyAccounts set");
                strSql.Append(" accountNum=@accountNum,bankName=@bankName,payeeBankName=@payeeBankName,remark=@remark,accountName=@accountName");
                if (Flag == 1)
                {//重置状态为首次打款
                    strSql.Append(",isFirst=1");
                }
                strSql.Append(" where identity_Id=@identity_Id");
                int result = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);
                if (result >= 1)
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
        /// 删除账户记录
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool DeleteAccountInfo(long identity_Id)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                StringBuilder strSql = new StringBuilder();
                SqlParameter[] parameters = {
                        new SqlParameter("@identity_Id",identity_Id)
                        };
                //strSql.Append("delete CompanyAccounts ");
                //strSql.Append(" where identity_Id=@identity_Id");
                strSql.Append("update CompanyAccounts set status=-1 ");
                strSql.Append(" where identity_Id=@identity_Id");
                int result = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);
                if (result == 1)
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
        /// 查询门店银行帐户信息
        /// </summary>
        /// <param name="shopid"></param>
        /// <returns></returns>
        public CompanyAccountInfo GetAccountInfo(int shopid)
        {
            CompanyAccountInfo model = new CompanyAccountInfo();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a.accountName,a.accountNum,a.bankName,a.payeeBankName,a.remark,isnull(a.identity_Id,0) identity_Id,b.companyId");
            strSql.Append(" from ShopInfo b");
            strSql.Append(" left join CompanyAccounts a on b.bankAccount=a.identity_Id");
            strSql.Append(" where b.shopID=@shopID");
            SqlParameter[] parameters = new SqlParameter[]{
					    new SqlParameter("@shopID", SqlDbType.Int,4)
                    };
            parameters[0].Value = shopid;
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                model = new CompanyAccountInfo()
                {
                    accountName = ds.Tables[0].Rows[0]["accountName"].ToString(),
                    accountNum = ds.Tables[0].Rows[0]["accountNum"].ToString(),
                    bankName = ds.Tables[0].Rows[0]["bankName"].ToString(),
                    payeeBankName = ds.Tables[0].Rows[0]["payeeBankName"].ToString(),
                    remark = ds.Tables[0].Rows[0]["remark"].ToString(),
                    companyId=Convert.ToInt32(ds.Tables[0].Rows[0]["companyId"].ToString()),
                    identity_Id = Convert.ToInt32(ds.Tables[0].Rows[0]["identity_Id"].ToString())
                };
            }
            return model;
        }

        /// <summary>
        /// 新增一条账户记录，同时更新门店中的银行帐号信息ID
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public int InsertAccountInfo(CompanyAccountInfo account)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into CompanyAccounts(");
                strSql.Append("companyId,accountNum,bankName,payeeBankName,remark,accountName,status)");
                strSql.Append(" values (");
                strSql.Append("@companyId,@accountNum,@bankName,@payeeBankName,@remark,@accountName,@status)");
                strSql.Append(";select @@IDENTITY");
                SqlParameter[] parameters = {
					new SqlParameter("@companyId", SqlDbType.Int,4),
					new SqlParameter("@accountNum", SqlDbType.VarChar,40),
					new SqlParameter("@bankName", SqlDbType.NVarChar,100),
					new SqlParameter("@payeeBankName", SqlDbType.NVarChar,100),
					new SqlParameter("@remark", SqlDbType.NVarChar,200),
                    new SqlParameter("@accountName",SqlDbType.NVarChar,50),
                    new SqlParameter("@status",SqlDbType.Int,4)};
                parameters[0].Value = account.companyId;
                parameters[1].Value = account.accountNum;
                parameters[2].Value = account.bankName;
                parameters[3].Value = account.payeeBankName;
                parameters[4].Value = account.remark;
                parameters[5].Value = account.accountName;
                parameters[6].Value = 1;

                int result = Convert.ToInt32(SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql.ToString(), parameters));

                return result;
            }
        }
    }
}
