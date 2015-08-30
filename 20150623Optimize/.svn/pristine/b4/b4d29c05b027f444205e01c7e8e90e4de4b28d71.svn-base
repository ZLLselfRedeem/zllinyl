using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.DBUtility;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL.Persistence.Infrastructure;

namespace VAGastronomistMobileApp.SQLServerDAL
{
    /// <summary>
    /// 打款记录数据处理层
    /// Add at 2015-5-5
    /// </summary>
    public class BankMoneyRecordManager
    {
        /// <summary>
        /// 打款记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int insertRecord(BankMoneyRecord model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into dbo.Tb_BankMoneyRecord(");
            strSql.Append("Id,BatchMoneyApplyDetail_Id,ElecChequeNo,AcctNo,AcctName,");
            strSql.Append("PayeeAcctNo,PayeeName,Amount,PayeeBankName,PayeeAddress,SysFlag,");
            strSql.Append("RemitLocation,DataStatus,AddTime,ModifyTime,ModifyUser,ModifyIP)");
            strSql.Append(" values (");
            strSql.Append("@Id,@BatchMoneyApplyDetail_Id,@ElecChequeNo,@AcctNo,@AcctName,");
            strSql.Append("@PayeeAcctNo,@PayeeName,@Amount,@PayeeBankName,@PayeeAddress,@SysFlag,");
            strSql.Append("@RemitLocation,@DataStatus,@AddTime,@ModifyTime,@ModifyUser,@ModifyIP)");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.UniqueIdentifier),
					new SqlParameter("@BatchMoneyApplyDetail_Id", SqlDbType.BigInt),
					new SqlParameter("@ElecChequeNo", SqlDbType.VarChar),
					new SqlParameter("@AcctNo", SqlDbType.VarChar),
                    new SqlParameter("@AcctName", SqlDbType.VarChar),
                    new SqlParameter("@PayeeAcctNo", SqlDbType.VarChar),
                    new SqlParameter("@PayeeName", SqlDbType.VarChar),
                    new SqlParameter("@Amount", SqlDbType.Money),
                    new SqlParameter("@PayeeBankName", SqlDbType.VarChar),
                    new SqlParameter("@PayeeAddress", SqlDbType.VarChar),
                    new SqlParameter("@SysFlag", SqlDbType.Int),
                    new SqlParameter("@RemitLocation", SqlDbType.Int),
                    //new SqlParameter("@AcceptNo", SqlDbType.VarChar),
                    //new SqlParameter("@SeqNo", SqlDbType.VarChar),
                    //new SqlParameter("@TransStatus", SqlDbType.Int),
                    new SqlParameter("@DataStatus", SqlDbType.Int),
                    new SqlParameter("@AddTime", SqlDbType.DateTime),
                    new SqlParameter("@ModifyTime", SqlDbType.DateTime),
                    new SqlParameter("@ModifyUser", SqlDbType.VarChar),
					new SqlParameter("@ModifyIP", SqlDbType.NVarChar)
            };
            parameters[0].Value = model.Id;
            parameters[1].Value = model.BatchMoneyApplyDetail_Id;
            parameters[2].Value = model.ElecChequeNo;
            parameters[3].Value = model.AcctNo;
            parameters[4].Value = model.AcctName;
            parameters[5].Value = model.PayeeAcctNo;
            parameters[6].Value = model.PayeeName;
            parameters[7].Value = model.Amount;
            parameters[8].Value = model.PayeeBankName;
            parameters[9].Value = model.PayeeAddress;
            parameters[10].Value = model.SysFlag;
            parameters[11].Value = model.RemitLocation;
            parameters[12].Value = model.DataStatus;
            parameters[13].Value = model.AddTime;
            parameters[14].Value = model.ModifyTime;
            parameters[15].Value = model.ModifyUser;
            parameters[16].Value = model.ModifyIP;

            object obj = Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters));
            if (obj == null)
            {
                return 0;
            }
            return 1;
        }
    }
}
