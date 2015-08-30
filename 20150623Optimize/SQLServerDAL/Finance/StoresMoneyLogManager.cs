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
    /// 批量打款日记数据处理
    /// </summary>
    public class StoresMoneyLogManager
    {
        /// <summary>
        /// 操作明细日志
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int insertDetailLog(StoresMoneyLog model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Tb_StoresMoneyLog(");
            strSql.Append("BatchMoneyApplyDetail_Id,[Content],AddTime,AddUser,AddIP)");
            strSql.Append(" values (");
            strSql.Append("@BatchMoneyApplyDetail_Id,@Content,@AddTime,@AddUser,@AddIP)");
            strSql.Append(" select @@identity");
            SqlParameter[] parameters = {
					new SqlParameter("@BatchMoneyApplyDetail_Id", SqlDbType.NVarChar),
					new SqlParameter("@Content", SqlDbType.NVarChar),
					new SqlParameter("@AddTime", SqlDbType.DateTime),
					new SqlParameter("@AddUser", SqlDbType.NVarChar),
					new SqlParameter("@AddIP", SqlDbType.NVarChar)
            };
            parameters[0].Value = model.BatchMoneyApplyDetail_Id;
            parameters[1].Value = model.Content;
            parameters[2].Value = model.AddTime;
            parameters[3].Value = model.AddUser;
            parameters[4].Value = model.AddIP;
            int rows = Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.MobileAppLogConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters));
            return rows;
        }

        /// <summary>
        /// 操作批量打款头表日志
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int insertLog(StoresMoneyLog model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Tb_StoresMoneyLog(");
            strSql.Append("ShopInfo_ShopID,MoneyMerchantAccountDetail_AccountId,BatchMoneyApply_Id,BatchMoneyApplyDetail_Id,Money,");
            strSql.Append("[Content],AddTime,AddUser,AddIP)");
            strSql.Append(" values (");
            strSql.Append("@ShopInfo_ShopID,@MoneyMerchantAccountDetail_AccountId,@BatchMoneyApply_Id,@BatchMoneyApplyDetail_Id,@Money,");
            strSql.Append("@Content,@AddTime,@AddUser,@AddIP)");
            strSql.Append(" select @@identity");
            SqlParameter[] parameters = {
                    new SqlParameter("@ShopInfo_ShopID", SqlDbType.Int),
                    new SqlParameter("@MoneyMerchantAccountDetail_AccountId", SqlDbType.BigInt),
                    new SqlParameter("@BatchMoneyApply_Id", SqlDbType.Int),
                    new SqlParameter("@BatchMoneyApplyDetail_Id", SqlDbType.NVarChar, 1000),
					new SqlParameter("@Money", SqlDbType.Money),
					new SqlParameter("@Content", SqlDbType.NVarChar, 255),
					new SqlParameter("@AddTime", SqlDbType.DateTime),
					new SqlParameter("@AddUser", SqlDbType.NVarChar, 50),
					new SqlParameter("@AddIP", SqlDbType.NVarChar, 39)
            };
            parameters[0].Value = model.ShopInfo_ShopID;
            parameters[1].Value = model.MoneyMerchantAccountDetail_AccountId;
            parameters[2].Value = model.BatchMoneyApply_Id;
            parameters[3].Value = model.BatchMoneyApplyDetail_Id;
            parameters[4].Value = model.Money;
            parameters[5].Value = model.Content;
            parameters[6].Value = model.AddTime;
            parameters[7].Value = model.AddUser;
            parameters[8].Value = model.AddIP;
            int rows = Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.MobileAppLogConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters));
            return rows;
        }
    }
}
