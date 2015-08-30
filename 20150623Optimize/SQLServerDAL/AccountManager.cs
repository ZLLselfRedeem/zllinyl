using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.DBUtility;

namespace VAGastronomistMobileApp.SQLServerDAL
{
    public class AccountManager
    {
        public DataTable GetAccountDetail(string sql)
        {
            DataSet dt = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sql.ToString());
            return dt.Tables[0];
        }
        ///// <summary>
        ///// 计算累计收入
        ///// </summary>
        ///// <returns></returns>
        //public string GetAllcount(int companyid, int shopid, string startTime)
        //{
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append(" select SUM(prePaidSum) as payd from PreOrder19dian,(select MAX(queryTime) as queryTime,preorder19dianId,shopId,isShopVerified  from PreOrderQueryInfo where PreOrderQueryInfo.isShopVerified =1 group by preorder19dianId,shopId,isShopVerified)tmp  ");
        //    strSql.AppendFormat(" where tmp.preorder19dianId =PreOrder19dian.preOrder19dianId and isPaid='1' and isApproved ='1' and companyId ={0} and PreOrder19dian.shopId={1} and queryTime>='{2}'", companyid, shopid, startTime);
        //    DataSet dt = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
        //    if (dt.Tables[0] != null && dt.Tables[0].Rows.Count > 0)
        //    {
        //        return dt.Tables[0].Rows[0]["payd"].ToString();
        //    }
        //    else
        //    {
        //        return "0";
        //    }

        //}
        //public string GetCount(string datetimestart, string datetimeend, int companyid, int shopid)
        //{
        //    try
        //    {
        //        StringBuilder strSql = new StringBuilder();
        //        strSql.Append(" select SUM(prePaidSum) as payd from PreOrder19dian,(select MAX(queryTime) as queryTime,preorder19dianId,shopId,isShopVerified  from PreOrderQueryInfo where PreOrderQueryInfo.isShopVerified =1 group by preorder19dianId,shopId,isShopVerified)tmp  where tmp.preorder19dianId =PreOrder19dian.preOrder19dianId and isPaid='1' and isApproved ='1'");
        //        strSql.AppendFormat(" and companyId ={0} and  PreOrder19dian.shopId={1} and queryTime> ='{2}' and queryTime<='{3}'", companyid, shopid, datetimestart, datetimeend);
        //        DataSet dt = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
        //        if (dt.Tables[0] != null && dt.Tables[0].Rows.Count > 0)
        //        {
        //            return dt.Tables[0].Rows[0]["payd"].ToString();
        //        }
        //        else
        //        {
        //            return "0";
        //        }
        //    }
        //    catch
        //    {
        //        return "0";
        //    }
        //}

        //public string Getgetout(string datetimestart, string datetimeend, int companyid, int shopid)
        //{
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.AppendFormat(" select SUM(pay)as payd from BusinessPay where companyId={0} and shopId={1} and Btime>='{2}' and Btime<='{3}'", companyid, shopid, datetimestart, datetimeend);
        //    DataSet dt = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
        //    if (dt.Tables[0] != null && dt.Tables[0].Rows.Count > 0)
        //    {
        //        return dt.Tables[0].Rows[0]["payd"].ToString();
        //    }
        //    else
        //    {
        //        return "0";
        //    }

        //}

        //public string Getgetoutall(int companyid, int shopid, string startTime)
        //{
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.AppendFormat(" select SUM(pay)as payd from BusinessPay where companyId={0} and shopId={1} and Btime>='{2}'", companyid, shopid, startTime);
        //    DataSet dt = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
        //    if (dt.Tables[0] != null && dt.Tables[0].Rows.Count > 0)
        //    {
        //        return dt.Tables[0].Rows[0]["payd"].ToString();
        //    }
        //    else
        //    {
        //        return "0";
        //    }

        //}
    }
}
