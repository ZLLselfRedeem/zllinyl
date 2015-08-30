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
    /// 平账管理数据处理层
    /// Add at 2015-4-10
    /// </summary>
    public class BalanceAccountManager
    {
        /// <summary>
        /// 分页查询某门店对应的平账记录
        /// </summary>
        /// <param name="page"></param>
        /// <param name="shopId"></param>
        /// <param name="cnt"></param>
        /// <returns></returns>
        public List<BalanceAccountDetail> QueryBusinessPay(Page page, int shopId, int status, long accountId, DateTime? beginTime, DateTime? endTime, int cityid, out int cnt)
        {
            string strCnt = @"select count(1) from MoneyMerchantAccountDetail where accountType=" + (int)AccountType.MERCHANT_CHECKOUT + "";
            string strSql = @"select accountId,flowNumber,companyName,shopName,operTime,confirmTime,accountMoney,remark,accountType,status 
from (select row_number() OVER (ORDER BY accountId desc) rownum,
accountId,flowNumber,companyName,shopName,operTime,confirmTime,accountMoney,remark,accountType,d.status 
from MoneyMerchantAccountDetail d
inner join CompanyInfo c on d.companyId = c.companyId
inner join ShopInfo s on d.shopId = s.shopId
where 1=1";
            if (shopId > 0)
            {
                strSql += " and d.shopId=@shopId";
                strCnt += " and shopId=@shopId";
            }
            if (status > 0)
            {
                strSql += " and d.status=@status";
                strCnt += " and status=@status";
            }
            if (accountId > 0)
            {
                strSql += " and d.accountId=@accountId";
                strCnt += " and accountId=@accountId";
            }
            if (beginTime != null)
            {
                strSql += " and d.operTime>=@beginTime";
                strCnt += " and operTime>=@beginTime";
            }
            if (endTime != null)
            {
                strSql += " and d.operTime<=@endTime";
                strCnt += " and operTime<=@endTime";
            }

            if (cityid > 0)
            {
                strSql += " and s.cityid=@cityid";
            }
            strSql += " and accountType=" + (int)AccountType.MERCHANT_CHECKOUT + ") a where a.rownum between @startIndex and @endIndex";

            List<SqlParameter> paraList = new List<SqlParameter>();

            if (shopId > 0)
            {
                paraList.Add(new SqlParameter("@shopId", SqlDbType.Int) { Value = shopId });
            }
            if (status > 0)
            {
                paraList.Add(new SqlParameter("@status", SqlDbType.Int) { Value = status });
            }
            if (accountId > 0)
            {
                paraList.Add(new SqlParameter("@accountId", SqlDbType.BigInt) { Value = accountId });
            }
            if (beginTime != null)
            {
                paraList.Add(new SqlParameter("@beginTime", SqlDbType.DateTime) { Value = beginTime });
            }
            if (endTime != null)
            {
                paraList.Add(new SqlParameter("@endTime", SqlDbType.DateTime) { Value = endTime });
            }
            if (cityid > 0)
            {
                paraList.Add(new SqlParameter("@cityid", SqlDbType.Int) { Value = cityid });
            }

            SqlParameter[] paraCnt = paraList.ToArray();

            object objCnt = SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strCnt.ToString(), paraCnt);
            if (objCnt == null)
            {
                cnt = 0;
            }
            else
            {
                cnt = Convert.ToInt32(objCnt);
            }

            paraList.Add(new SqlParameter("@startIndex", SqlDbType.Int) { Value = page.Skip + 1 });
            paraList.Add(new SqlParameter("@endIndex", SqlDbType.Int) { Value = page.Skip + page.PageSize });

            SqlParameter[] para = paraList.ToArray();
            List<BalanceAccountDetail> accountList = new List<BalanceAccountDetail>();
            using (SqlDataReader sdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), para))
            {
                while (sdr.Read())
                {
                    accountList.Add(SqlHelper.GetEntity<BalanceAccountDetail>(sdr));
                }
            }
            return accountList;
        }

        /// <summary>
        /// 查询某门店对应的平账记录
        /// </summary>
        /// <param name="shopId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public List<BalanceAccountDetail> QueryBusinessPay(int shopId, int status, long? accountId, DateTime? beginTime, DateTime? endTime)
        {
            string strSql = @"select accountId,flowNumber,companyName,shopName,operTime,confirmTime,accountMoney,remark,accountType,d.status 
from MoneyMerchantAccountDetail d
inner join CompanyInfo c on d.companyId = c.companyId
inner join ShopInfo s on d.shopId = s.shopId
where accountType=" + (int)AccountType.MERCHANT_CHECKOUT + "";
            if (shopId > 0)
            {
                strSql += " and d.shopId=@shopId";
            }
            if (status > 0)
            {
                strSql += " and d.status=@status";
            } 
            if (accountId > 0)
            {
                strSql += " and d.accountId=@accountId";
            }
            if (beginTime != null)
            {
                strSql += " and d.operTime>=@beginTime";
            }
            if (endTime != null)
            {
                strSql += " and d.operTime<=@endTime";
            }
            List<SqlParameter> paraList = new List<SqlParameter>() { };
            if (shopId > 0)
            {
                paraList.Add(new SqlParameter("@shopId", SqlDbType.Int) { Value = shopId });
            }
            if (status > 0)
            {
                paraList.Add(new SqlParameter("@status", SqlDbType.Int) { Value = status });
            }
            if (accountId > 0)
            {
                paraList.Add(new SqlParameter("@accountId", SqlDbType.BigInt) { Value = accountId });
            }
            if (beginTime != null)
            {
                paraList.Add(new SqlParameter("@beginTime", SqlDbType.DateTime) { Value = beginTime });
            }
            if (endTime != null)
            {
                paraList.Add(new SqlParameter("@endTime", SqlDbType.DateTime) { Value = endTime });
            }

            SqlParameter[] para = paraList.ToArray();

            List<BalanceAccountDetail> detail = new List<BalanceAccountDetail>();
            using (SqlDataReader sdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, para))
            {
                while (sdr.Read())
                {
                    detail.Add(SqlHelper.GetEntity<BalanceAccountDetail>(sdr));
                }
            }
            return detail;
        }

        /// <summary>
        /// 查询某门店的未结款金额及冻结金额
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public BalanceAccountShopMoney QueryShopMoney(int shopId)
        {
            const string strSql = "SELECT remainMoney,AmountFrozen FROM ShopInfo where shopID=@shopID";

            SqlParameter[] param ={
                                      new SqlParameter("@shopID",shopId)
                                  };
            BalanceAccountShopMoney shopMoney = new BalanceAccountShopMoney();
            using (SqlDataReader sdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, param))
            {
                if (sdr.Read())
                {
                    shopMoney = SqlHelper.GetEntity<BalanceAccountShopMoney>(sdr);
                }
            }
            return shopMoney;
        }

        /// <summary>
        /// 更新门店的未结款金额及冻结金额
        /// </summary>
        /// <param name="shopMoney"></param>
        /// <returns></returns>
        public bool UpdateShopMoney(BalanceAccountShopMoney shopMoney)
        {
            const string strSql = @"update ShopInfo set 
remainMoney=isnull(remainMoney,0)+@remainMoney,
amountFrozen=isnull(amountFrozen,0)+@amountFrozen
where shopID=@shopID
            and cast(isnull(remainMoney,0)+@remainMoney as numeric(18,2))>=0 and cast(isnull(amountFrozen,0)+@amountFrozen as numeric(18,2))>=0";

//           
//            , 
//remainAlipayAmount=isnull(remainAlipayAmount,0)+@remainAlipayAmount,
//remainWechatPayAmount=isnull(remainWechatPayAmount,0)+@remainWechatPayAmount,
//remainRedEnvelopeAmount=isnull(remainRedEnvelopeAmount,0)+@remainRedEnvelopeAmount,
//remainFoodCouponAmount=isnull(remainFoodCouponAmount,0)+@remainFoodCouponAmount,
//remainCommissionAmount=isnull(remainCommissionAmount,0)+@remainCommissionAmount

            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@remainMoney", SqlDbType.Float) { Value = shopMoney.remainMoney },
            new SqlParameter("@amountFrozen", SqlDbType.Float) { Value = shopMoney.amountFrozen },
            //new SqlParameter("@remainAlipayAmount", SqlDbType.Float) { Value = shopMoney.payAlipayAmount },
            //new SqlParameter("@remainWechatPayAmount", SqlDbType.Float) { Value = shopMoney.payWechatPayAmount },
            //new SqlParameter("@remainRedEnvelopeAmount", SqlDbType.Float) { Value = shopMoney.payRedEnvelopeAmount },
            //new SqlParameter("@remainFoodCouponAmount", SqlDbType.Float) { Value = shopMoney.payFoodCouponAmount },
            //new SqlParameter("@remainCommissionAmount", SqlDbType.Float) { Value = shopMoney.payCommissionAmount },
            new SqlParameter("@shopID", SqlDbType.Int) { Value = shopMoney.shopId }
            };
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int i = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql, para);
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
        /// 更新平账单据状态
        /// </summary>
        /// <param name="accountId">单号</param>
        /// <param name="status">状态</param>
        /// <returns></returns>
        public bool UpdateMoneyMerchantAccountDetailStatus(long accountId, BalanceAccountStatus status)
        {
            const string strSql = "update dbo.MoneyMerchantAccountDetail set status=@status where accountId=@accountId";
            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@accountId", SqlDbType.BigInt) { Value = accountId },
            new SqlParameter("@status", SqlDbType.BigInt) { Value = (int)status },
            };
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int i = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql, para);
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

        public bool CheckAuthority(int employeeId, string roleName)
        {
            const string strSql = @"select 1 from EmployeeRole er inner join RoleInfo r on er.RoleID=r.RoleID
and r.RoleName =@roleName and er.EmployeeID =@employeeId and EmployeeRoleStatus = 1";
            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@roleName", SqlDbType.NVarChar, 30) { Value = roleName },
            new SqlParameter("@employeeId", SqlDbType.Int) { Value = employeeId },
            };
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                object obj = SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql, para);
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
        /// 检查门店是否有申请未处理的打款单
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public bool CheckHaveUndoApply(int shopId)
        {
            string strSql = "select 1 from BatchMoneyApplyDetail where shopId=@shopId and status=" + (int)BatchMoneyStatus.wait_for_check + "";
            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@shopId", SqlDbType.Int) { Value = shopId }
            };
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                object obj = SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql, para);
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
        /// 查询某个门店各种余额
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public BalanceAccountShopRemainMoney QueryShopRemainMoney(int shopId)
        {
            const string strSql = @"select 
round(remainMoney,2) remainMoney
,round(remainAlipayAmount,2) remainAlipayAmount,
round(remainFoodCouponAmount,2) remainFoodCouponAmount,
round(remainRedEnvelopeAmount,2) remainRedEnvelopeAmount,
round(remainWechatPayAmount,2) remainWechatPayAmount,
round(remainCommissionAmount,2) remainCommissionAmount
from ShopInfo where shopID=@shopId";
            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@shopId", SqlDbType.Int) { Value = shopId }
            };
            BalanceAccountShopRemainMoney shopRemainMoney = new BalanceAccountShopRemainMoney();
            using (SqlDataReader sdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, para))
            {
                if (sdr.Read())
                {
                    shopRemainMoney = SqlHelper.GetEntity<BalanceAccountShopRemainMoney>(sdr);
                }
            }
            return shopRemainMoney;
        }
    }
}
