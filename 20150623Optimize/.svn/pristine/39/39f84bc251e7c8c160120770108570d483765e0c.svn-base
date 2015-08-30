using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using VAGastronomistMobileApp.SQLServerDAL;
using VAGastronomistMobileApp.Model;
namespace VAGastronomistMobileApp.WebPageDll
{
    public class AccountOperate
    {
        AccountManager manager = new AccountManager();

        /// <summary>
        /// 作废
        // select preorder19dianId,Btime,pay,(select sum(pay) as payd from(select * from (select preorder19dianId,prePayTime as Btime,[prePaidSum] as pay 
        // from PreOrder19dian  where isPaid='1' and isApproved ='1')ABC
        //union
        //select type as preorder19dianId,Btime,-pay from BusniessPay)as D1 where D1.Btime<=AB.Btime) as payd from (select * from 
        //(select preorder19dianId,prePayTime as Btime,[prePaidSum] as pay 
        // from PreOrder19dian   where isPaid='1' and isApproved ='1')ABC
        //union
        //select type as preorder19dianId,Btime,-pay from BusniessPay)AB where 1=1 and (Btime >= '2013-07-23' and Btime<='2013-09-11') order by Btime
        ///// </summary>
        ///// <returns></returns>
        //public DataTable GetAccountDetail()
        //{
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append("select Btime,pay,(select sum(pay) as payd from(select * from (select prePayTime as Btime,[prePaidSum] as pay ");
        //    strSql.Append(" from PreOrderQueryInfo A,PreOrder19dian B  where A.preorder19dianId =B.preorder19dianId ");
        //    strSql.Append(" and A.isShopVerified='1' and isPaid='1' and isApproved ='1')ABC ");
        //    strSql.Append(" union");
        //    strSql.Append(" select Btime,-pay from BusinessPay)as D1 where D1.Btime<=AB.Btime) as payd from (select * from ");
        //    strSql.Append(" (select prePayTime as Btime,[prePaidSum] as pay ");
        //    strSql.Append(" from PreOrderQueryInfo A,PreOrder19dian B  where A.preorder19dianId =B.preorder19dianId");
        //    strSql.Append(" and A.isShopVerified='1' and isPaid='1' and isApproved ='1')ABC");
        //    strSql.Append(" union");
        //    strSql.Append(" select Btime,-pay from BusinessPay)AB order by Btime");
        //    return manager.GetAccountDetail(strSql.ToString());
        //}
        ///// <summary>
        ///// 计算累计收入
        ///// </summary>
        ///// <returns></returns>
        //public double GetAllcount(int companyid, int shopid)
        //{
        //    string startTime = Common.GetFieldValue("VASystemConfig", "configContent", "configName='PreOrder19dianTime'");
        //    if (startTime == "")
        //    {
        //        startTime = "1970-01-01 00:00:00";
        //    }
        //    return Common.ToDouble(manager.GetAllcount(companyid, shopid, startTime));
        //}
        ///// <summary>
        ///// 可提取余额
        ///// </summary>
        ///// <param name="companyid"></param>
        ///// <param name="shopid"></param>
        ///// <returns></returns>
        //public double GetcountBhaved(int companyid, int shopid)
        //{
        //    string startTime = Common.GetFieldValue("VASystemConfig", "configContent", "configName='PreOrder19dianTime'");
        //    if (startTime == "")
        //    {
        //        startTime = "1970-01-01 00:00:00";
        //    }
        //    double outall = Common.ToDouble(manager.Getgetoutall(companyid, shopid, startTime));
        //    return Common.ToDouble(manager.GetAllcount(companyid, shopid, startTime)) - outall;
        //}

        //public double GetCount(string datetimestart, string datetimeend, int companyid, int shopid)
        //{
        //    return Common.ToDouble(manager.GetCount(datetimestart, datetimeend, companyid, shopid)) - Common.ToDouble(manager.Getgetout(datetimestart, datetimeend, companyid, shopid));

 
        //}

    }
}
