using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using VAGastronomistMobileApp.DBUtility;
using System.Data;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL.Persistence.Infrastructure;

namespace VAGastronomistMobileApp.SQLServerDAL
{
    public class SybMoneyMerchantManager
    {
        /// <summary>
        /// 功能描述：更改商户账号余额
        /// </summary>
        /// <param name="shopID"></param>
        /// <param name="remainMoney"></param>
        /// <returns></returns>
        public static bool AddRemainMoney(int shopID, double remainMoney)
        {
            string strsql = @"update ShopInfo set remainMoney=isnull(remainMoney,0)+@remainMoney where shopID=@shopID";
            var parm = new[] { new SqlParameter("@shopID", shopID)
                , new SqlParameter("@remainMoney",remainMoney) };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strsql, parm) == 1;
        }
        /// <summary>
        /// 功能描述：更改商户账号累计金额
        /// </summary>
        /// <param name="shopID"></param>
        /// <param name="paySum"></param>
        /// <returns></returns>
        public static bool UpdateShopInfoAboutMoney(ShopMoney shopMoney, bool updateOrderCount = false)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update ShopInfo set remainMoney=isnull(remainMoney,0)+@remainMoney,totalMoney=isnull(totalMoney,0)+@paySum");
            //先注释部分代码，银企直联项目中开启 2015-6-24 砍掉此部分
            //strSql.Append(" ,remainRedEnvelopeAmount=isnull(remainRedEnvelopeAmount,0)+@remainRedEnvelopeAmount,");
            //strSql.Append(" remainFoodCouponAmount=isnull(remainFoodCouponAmount,0)+@remainFoodCouponAmount,");
            //strSql.Append(" remainAlipayAmount=isnull(remainAlipayAmount,0)+@remainAlipayAmount,");
            //strSql.Append(" remainWechatPayAmount=isnull(remainWechatPayAmount,0)+@remainWechatPayAmount,");
            //strSql.Append(" remainCommissionAmount=isnull(remainCommissionAmount,0)+@remainCommissionAmount");
            if (updateOrderCount)
            {
                strSql.Append(",prepayOrderCount = isnull(prepayOrderCount, 0) + 1 ");
            }
            strSql.Append(" where shopID=@shopID");

            List<SqlParameter> paraList = new List<SqlParameter>();
            paraList.Add(new SqlParameter("@remainMoney", SqlDbType.Float) { Value = shopMoney.remainMoney });
            paraList.Add(new SqlParameter("@paySum", SqlDbType.Float) { Value = shopMoney.paySum });
            //paraList.Add(new SqlParameter("@remainRedEnvelopeAmount", SqlDbType.Float) { Value = shopMoney.remainRedEnvelopeAmount });
            //paraList.Add(new SqlParameter("@remainFoodCouponAmount", SqlDbType.Float) { Value = shopMoney.remainFoodCouponAmount });
            //paraList.Add(new SqlParameter("@remainAlipayAmount", SqlDbType.Float) { Value = shopMoney.remainAlipayAmount });
            //paraList.Add(new SqlParameter("@remainWechatPayAmount", SqlDbType.Float) { Value = shopMoney.remainWechatPayAmount });
            //paraList.Add(new SqlParameter("@remainCommissionAmount", SqlDbType.Float) { Value = shopMoney.remainCommissionAmount });
            paraList.Add(new SqlParameter("@shopID", SqlDbType.Float) { Value = shopMoney.shopId });
            SqlParameter[] para = paraList.ToArray();
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
        /// 功能说明：获取商铺余额
        /// 
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public double GetShopInfoRemainMoney(int shopId)
        {
            SqlParameter[] param ={
                                      new SqlParameter("@shopID",shopId)
                                  };
            const string strSql = "SELECT remainMoney FROM ShopInfo where shopID=@shopID";
            string s = SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), param).ToString();
            double resultGetShopInfoRemainMoney = 0;
            if (s != "")
            {
                resultGetShopInfoRemainMoney = double.Parse(s);
            }
            return resultGetShopInfoRemainMoney;
        }

        /// <summary>
        /// 功能说明：获取商铺累计金额
        /// 
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public double GetShopInfoTotalMoney(int shopId)
        {
            StringBuilder strSql = new StringBuilder();
            SqlParameter[] param ={
                                      new SqlParameter("@shopID",shopId)
                                  };
            strSql.Append(@"SELECT totalMoney  FROM ShopInfo where shopID=@shopID");
            string s = SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), param).ToString();
            double resultGetShopInfoRemainMoney = 0;
            if (s != "")
            {
                resultGetShopInfoRemainMoney = double.Parse(s);
            }
            return resultGetShopInfoRemainMoney;
        }

        /// <summary>
        /// 功能说明：向商户流水中插入记录
        /// 
        /// </summary>
        /// <param name="shopId"></param>
        /// <param name="companyId"></param>
        /// <param name="flowNumber"></param>
        /// <param name="accountMoney"></param>
        /// <param name="remainMoney"></param>
        /// <param name="accountType"></param>
        /// <param name="accountTypeConnId"></param>
        /// <param name="inoutcomeType"></param>
        /// <param name="operUser"></param>
        /// <returns></returns>
        public bool InsertMoneyMerchantAccountDetail(int shopId, int companyId, string flowNumber, double accountMoney, double remainMoney, int accountType, long accountTypeConnId, int inoutcomeType, string operUser)
        {
            DateTime operTime = DateTime.Now;
            SqlParameter[] param ={
                                      new SqlParameter("@shopID",shopId),
                                      new SqlParameter("@companyId",companyId),
                                      new SqlParameter("@flowNumber",flowNumber),
                                      new SqlParameter("@accountMoney",accountMoney),
                                      new SqlParameter("@remainMoney",remainMoney),
                                      new SqlParameter("@accountType",accountType),
                                      new SqlParameter("@accountTypeConnId",accountTypeConnId),
                                      new SqlParameter("@inoutcomeType",inoutcomeType),
                                      new SqlParameter("@operUser",operUser),
                                      new SqlParameter("@operTime",operTime)
                                  };
            const string strSql = @"insert into MoneyMerchantAccountDetail
(flowNumber,accountMoney,remainMoney,accountType,accountTypeConnId,inoutcomeType,operUser,operTime,companyId,shopId)
values(@flowNumber,@accountMoney,@remainMoney,@accountType,@accountTypeConnId,@inoutcomeType,@operUser,@operTime,@companyId,@shopId)";

            int resultInsertMoneyMerchantAccountDetail = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, param);
            if (resultInsertMoneyMerchantAccountDetail > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool InsertMoneyMerchantAccountDetail(MoneyMerchantAccountDetail moneyMerchantAccountDetail)
        {
            const string strSql = @"insert into MoneyMerchantAccountDetail
(flowNumber,accountMoney,remainMoney,accountType,accountTypeConnId,inoutcomeType,operUser,operTime,companyId,shopId)
values(@flowNumber,@accountMoney,@remainMoney,@accountType,@accountTypeConnId,@inoutcomeType,@operUser,@operTime,@companyId,@shopId)";
            SqlParameter[] param ={
                                      new SqlParameter("@flowNumber", SqlDbType.NVarChar, 50) { Value = moneyMerchantAccountDetail.flowNumber },
                                      new SqlParameter("@accountMoney", SqlDbType.Float) { Value = moneyMerchantAccountDetail.accountMoney},
                                      new SqlParameter("@remainMoney",SqlDbType.Float) { Value = moneyMerchantAccountDetail.remainMoney},
                                      new SqlParameter("@accountType",SqlDbType.Int) { Value = moneyMerchantAccountDetail.accountType},
                                      new SqlParameter("@accountTypeConnId",SqlDbType.NVarChar,100) { Value = moneyMerchantAccountDetail.accountTypeConnId},
                                      new SqlParameter("@inoutcomeType",SqlDbType.Int) { Value = moneyMerchantAccountDetail.inoutcomeType},
                                      new SqlParameter("@operUser",SqlDbType.NVarChar,100) { Value = moneyMerchantAccountDetail.operUser},
                                      new SqlParameter("@operTime",SqlDbType.DateTime) { Value = DateTime.Now},
                                      new SqlParameter("@shopID",SqlDbType.Int) { Value = moneyMerchantAccountDetail.shopId },
                                      new SqlParameter("@companyId",SqlDbType.Int) { Value = moneyMerchantAccountDetail.companyId }
                                  };
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int resultInsertMoneyMerchantAccountDetail = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql, param);
                if (resultInsertMoneyMerchantAccountDetail > 0)
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
        /// 功能说明：获取商铺一段时间内的余额
        /// 
        /// </summary>
        /// <param name="datetimestart"></param>
        /// <param name="datetimeend"></param>
        /// <param name="shopId"></param>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public double GetAccountMoneySumForTime(string datetimestart, string datetimeend, int shopId, int companyId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select SUM(accountMoney) as sum from MoneyMerchantAccountDetail ");
            strSql.AppendFormat("where shopId={0} and companyId={1} and operTime>'{2}' and operTime<'{3}'", shopId, companyId, datetimestart, datetimeend);
            DataSet dt = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            double resultGetAccountMoneySumForTime = 0;
            if (dt.Tables[0] != null && dt.Tables[0].Rows.Count > 0)
            {
                if (dt.Tables[0].Rows[0]["sum"].ToString() == "") return resultGetAccountMoneySumForTime;
                resultGetAccountMoneySumForTime = double.Parse(dt.Tables[0].Rows[0]["sum"].ToString());
            }
            return resultGetAccountMoneySumForTime;
        }


        /// <summary>
        /// 功能说明：获取商铺一段时间内的净余额
        /// </summary>
        /// <param name="datetimestart"></param>
        /// <param name="datetimeend"></param>
        /// <param name="shopId"></param>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public double GetAccountMoneyCleanSumForTime(string datetimestart, string datetimeend, int shopId, int companyId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select SUM(accountMoney) as sum from MoneyMerchantAccountDetail ");
            strSql.AppendFormat("where shopId={0} and companyId={1} and operTime>'{2}' and operTime<'{3}'", shopId, companyId, datetimestart, datetimeend);
            DataSet dt = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            double resultGetAccountMoneyCleanSumForTime = 0;
            if (dt.Tables[0] != null && dt.Tables[0].Rows.Count > 0)
            {
                if (dt.Tables[0].Rows[0]["sum"].ToString() == "") return resultGetAccountMoneyCleanSumForTime;
                resultGetAccountMoneyCleanSumForTime = double.Parse(dt.Tables[0].Rows[0]["sum"].ToString());
            }
            return resultGetAccountMoneyCleanSumForTime;
        }

        public double GetAccountMoneyGetConditionSum(string datetimestart, string datetimeend, int shopId, int companyId, int type, double paystart, double payend, string mainkey)
        {
            StringBuilder strSqlField = new StringBuilder();
            strSqlField.Append("SELECT  SUM(accountMoney) as sum FROM [MoneyMerchantAccountDetail] ");
            strSqlField.AppendFormat("where shopId={0}", shopId);
            if (datetimestart != "" && datetimeend != "")
            {
                try
                {
                    strSqlField.AppendFormat("  and operTime >='{0}' and operTime<='{1}'", datetimestart, datetimeend);
                }
                catch
                {
                    strSqlField.AppendFormat("  and operTime >='" + "1970-01-01 00:00:00" + "'and operTime<='" + DateTime.Now + "'");
                }
            }
            //金额
            if (paystart != int.MinValue)
                strSqlField.AppendFormat(" and accountMoney>=" + paystart);
            if (payend != int.MaxValue)
                strSqlField.AppendFormat(" and accountMoney<=" + payend);
            switch (type)
            {
                case 13: strSqlField.Append(" and (accountType = 13) "); break;
                case 4: strSqlField.Append(" and (accountType = 4) "); break;
                case 5: strSqlField.Append(" and (accountType = 5) "); break;
                case 6: strSqlField.Append(" and (accountType = 6) "); break;
            }
            if (mainkey != "")
            {
                strSqlField.AppendFormat(" and accountTypeConnId = '{0}'", mainkey);
            }

            DataSet dt = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSqlField.ToString());
            double resultGetAccountMoneyGetConditionSum = 0;
            if (dt.Tables[0] != null && dt.Tables[0].Rows.Count > 0)
            {
                if (dt.Tables[0].Rows[0]["sum"].ToString() == "") return resultGetAccountMoneyGetConditionSum;
                resultGetAccountMoneyGetConditionSum = double.Parse(dt.Tables[0].Rows[0]["sum"].ToString());
            }
            return resultGetAccountMoneyGetConditionSum;
        }

        /// <summary>
        /// 用按orderId统计账户明细
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public DataTable getAccountDetailByOrderID(string preOrder19dianIds,int shopId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select OrderId,accountType,sum(accountMoney) accountMoney,");
            strSql.Append("case accountType when 4 then min(remainMoney) else MAX(remainMoney) end remainMoney,MIN(preOrder19dianId) preOrder19dianId");
            strSql.Append(" from MoneyMerchantAccountDetail a");
            strSql.Append(" inner join PreOrder19dian b on preOrder19dianId=accountTypeConnId");
            strSql.Append(" where accountMoney>=-2147483648");
            strSql.Append(" and (accountType IN (4, 5, 6, 13)) AND (a.status = 3)");
            strSql.AppendFormat(" and b.shopId={0}",shopId);
            if (preOrder19dianIds.Length > 0)
            {
                strSql.AppendFormat(" and orderId in (select OrderId from PreOrder19dian where preOrder19dianId in ({0}))", preOrder19dianIds);
            }
            strSql.Append(" group by OrderId,accountType");
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }

        /// <summary>
        /// 按orderId查出要显示的主单ID，只退补差价单子时用
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public DataTable getAccountDetailByAccountType(string preOrder19dianIds)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select OrderId,preOrder19dianId from PreOrder19dian");
            strSql.Append(" where OrderType=1");
            strSql.AppendFormat(" and orderId in (select OrderId from PreOrder19dian where preOrder19dianId in ({0}))", preOrder19dianIds);
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }

    }
}
