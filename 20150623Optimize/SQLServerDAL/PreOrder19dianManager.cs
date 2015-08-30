﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Threading;
using LogDll;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.DBUtility;

namespace VAGastronomistMobileApp.SQLServerDAL
{
    /// <summary> 
    /// 19点预点单数据库操作类
    /// </summary>
    public partial class PreOrder19dianManager
    {
        //public bool IsExists(long preOrder19DianId)
        //{
        //    string sql = "SELECT COUNT(0) FROM [PreOrder19dian] WHERE [preOrder19dianId] =@preOrder19dianId";
        //    long count;
        //    object o =
        //    SqlHelper.ExecuteScalar(sql, CommandType.Text, sql, new SqlParameter("@preOrder19dianId", SqlDbType.BigInt) { Value = preOrder19DianId });
        //    if (o != null && long.TryParse(o.ToString(), out count))
        //    {
        //        if (count > 0)
        //        {
        //            return true;
        //        }
        //    }
        //    return false;
        //}
        ///// <summary>
        ///// 新增预支付奖励信息
        ///// </summary>
        ///// <param name="prePayPrivilege"></param>
        ///// <returns></returns>
        //public int InsertPrePayPrivilege(PrePayPrivilege prePayPrivilege)
        //{
        //    using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
        //    {
        //        Object obj = null;
        //        try
        //        {
        //            conn.Open();
        //            StringBuilder strSql = new StringBuilder();
        //            SqlParameter[] parameters = null;
        //            strSql.Append("insert into PrePayPrivilege(");
        //            strSql.Append("shopId,prePayPrivilegeStr,prePayCashBackType,prePayCashBackValue,isValid,numberOfHoursAhead,supportPrePayCashBack,supportPrePayVIPEntrance,supportPrePayGift,vipEntranceMax,giftContent,prePayCashMin,timeType,dayCount,effectTime,useCountinTime,returnCouponInterval)");
        //            strSql.Append(" values (");
        //            strSql.Append("@shopId,@prePayPrivilegeStr,@prePayCashBackType,@prePayCashBackValue,@isValid,@numberOfHoursAhead,@supportPrePayCashBack,@supportPrePayVIPEntrance,@supportPrePayGift,@vipEntranceMax,@giftContent,@prePayCashMin,@timeType,@dayCount,@effectTime,@useCountinTime,@returnCouponInterval)");
        //            strSql.Append(" select @@identity");
        //            parameters = new SqlParameter[]{
        //                new SqlParameter("@shopId",SqlDbType.Int,4),
        //                new SqlParameter("@prePayPrivilegeStr",SqlDbType.NVarChar,200),
        //                new SqlParameter("@prePayCashBackType",SqlDbType.SmallInt,2),
        //                new SqlParameter("@prePayCashBackValue",SqlDbType.Float),
        //                new SqlParameter("@isValid",SqlDbType.Int,4),
        //                new SqlParameter("@numberOfHoursAhead",SqlDbType.Float),
        //                new SqlParameter("@supportPrePayCashBack",SqlDbType.SmallInt),
        //                new SqlParameter("@supportPrePayVIPEntrance",SqlDbType.SmallInt),
        //                new SqlParameter("@supportPrePayGift",SqlDbType.SmallInt),
        //                new SqlParameter("@vipEntranceMax",SqlDbType.Int),
        //                new SqlParameter("@giftContent",SqlDbType.NVarChar,50),
        //                new SqlParameter("@prePayCashMin",SqlDbType.Float),

        //                new SqlParameter("@timeType",SqlDbType.Int,4),
        //                new SqlParameter("@dayCount",SqlDbType.Float),
        //                new SqlParameter("@effectTime",SqlDbType.Float),
        //                new SqlParameter("@useCountinTime",SqlDbType.Int,4),
        //                new SqlParameter("@returnCouponInterval",SqlDbType.Float)
        //            };
        //            parameters[0].Value = prePayPrivilege.shopId;
        //            parameters[1].Value = prePayPrivilege.prePayPrivilegeStr;
        //            parameters[2].Value = prePayPrivilege.prePayCashBackType;
        //            parameters[3].Value = prePayPrivilege.prePayCashBackValue;
        //            parameters[4].Value = 1;
        //            parameters[5].Value = prePayPrivilege.numberOfHoursAhead;
        //            parameters[6].Value = prePayPrivilege.supportPrePayCashBack;
        //            parameters[7].Value = prePayPrivilege.supportPrePayVIPEntrance;
        //            parameters[8].Value = prePayPrivilege.supportPrePayGift;
        //            parameters[9].Value = prePayPrivilege.vipEntranceMax;
        //            parameters[10].Value = prePayPrivilege.giftContent;
        //            parameters[11].Value = prePayPrivilege.prePayCashMin;
        //            parameters[12].Value = prePayPrivilege.timeType;
        //            parameters[13].Value = prePayPrivilege.dayCount;
        //            parameters[14].Value = prePayPrivilege.effectTime;
        //            parameters[15].Value = prePayPrivilege.useCountinTime;
        //            parameters[16].Value = prePayPrivilege.returnCouponInterval;

        //            obj = SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql.ToString(), parameters);
        //        }
        //        catch (Exception)
        //        {
        //            return 0;
        //        }
        //        if (obj == null)
        //        {
        //            return 0;
        //        }
        //        else
        //        {
        //            return Convert.ToInt32(obj);
        //        }
        //    }
        //}

        //public bool UpdatePayCashBack(PreOrder19dianInfo preOrder19dian)
        //{
        //    using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
        //    {
        //        int result = 0;
        //        try
        //        {
        //            conn.Open();
        //            StringBuilder strSql = new StringBuilder();
        //            strSql.Append("update PreOrder19dian set ");
        //            strSql.Append("prePayCashBack=@prePayCashBack,");
        //            strSql.Append("cashbackReceived=@cashbackReceived");
        //            strSql.Append(" where preOrder19dianId=@preOrder19dianId ");
        //            SqlParameter[] parameters = {                   
        //            new SqlParameter("@prePayCashBack", SqlDbType.Float),
        //            new SqlParameter("@cashbackReceived", SqlDbType.SmallInt,2),
        //            new SqlParameter("@preOrder19dianId", SqlDbType.BigInt,8)};
        //            parameters[0].Value = preOrder19dian.prePayCashBack;
        //            parameters[1].Value = preOrder19dian.cashbackReceived;
        //            parameters[2].Value = preOrder19dian.preOrder19dianId;
        //            result = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);
        //        }
        //        catch (Exception)
        //        {
        //            return false;
        //        }
        //        if (result == 1)
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //}

        ///// <summary>
        ///// 更新点单信息
        ///// </summary>
        ///// <param name="preOrder19dian"></param>
        ///// <returns></returns>
        //        public bool UpdatePreOrder19dianOrderInfo(PreOrder19dianInfo preOrder19dian)
        //        {
        //            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
        //            {
        //                if (conn.State != ConnectionState.Open)
        //                {
        //                    conn.Open();
        //                }
        //                const string strSql = @"update PreOrder19dian set status=@status,isPaid=@isPaid,prePaidSum=@prePaidSum,prePayTime=@prePayTime,validFromDate=@validFromDate,
        //viewallocCommission=@viewallocCommission,transactionFee=@transactionFee,viewallocNeedsToPayToShop=@viewallocNeedsToPayToShop,verificationCode=@verificationCode,
        //refundDeadline=@refundDeadline,discount=@discount,numberOfHoursAhead=@numberOfHoursAhead,preOrderServerSum=@preOrderServerSum,verifiedSaving=@verifiedSaving
        // where preOrder19dianId=@preOrder19dianId";
        //                SqlParameter[] parameters = {                   
        //                    new SqlParameter("@status", SqlDbType.SmallInt,2),
        //                    new SqlParameter("@isPaid", SqlDbType.SmallInt,2),
        //                    new SqlParameter("@prePaidSum", SqlDbType.Float),
        //                    new SqlParameter("@prePayTime", SqlDbType.DateTime),
        //                    new SqlParameter("@validFromDate", SqlDbType.DateTime),
        //                    new SqlParameter("@viewallocCommission", SqlDbType.Float),
        //                    new SqlParameter("@transactionFee", SqlDbType.Float),
        //                    new SqlParameter("@viewallocNeedsToPayToShop", SqlDbType.Float),
        //                    new SqlParameter("@verificationCode", SqlDbType.NVarChar,50),
        //                    new SqlParameter("@refundDeadline", SqlDbType.DateTime),
        //                    new SqlParameter("@discount", SqlDbType.Float),
        //                    new SqlParameter("@numberOfHoursAhead", SqlDbType.Float),
        //                    new SqlParameter("@preOrder19dianId", SqlDbType.BigInt,8),
        //                    new SqlParameter("@preOrderServerSum", SqlDbType.Float),
        //                    new SqlParameter("@verifiedSaving", SqlDbType.Float)};
        //                parameters[0].Value = (int)preOrder19dian.status;
        //                parameters[1].Value = preOrder19dian.isPaid;
        //                parameters[2].Value = preOrder19dian.prePaidSum;
        //                parameters[3].Value = preOrder19dian.prePayTime;
        //                parameters[4].Value = preOrder19dian.validFromDate;
        //                parameters[5].Value = preOrder19dian.viewallocCommission;
        //                parameters[6].Value = preOrder19dian.transactionFee;
        //                parameters[7].Value = preOrder19dian.viewallocNeedsToPayToShop;
        //                parameters[8].Value = preOrder19dian.verificationCode;
        //                parameters[9].Value = preOrder19dian.refundDeadline;
        //                parameters[10].Value = preOrder19dian.discount;
        //                parameters[11].Value = preOrder19dian.numberOfHoursAhead;
        //                parameters[12].Value = preOrder19dian.preOrder19dianId;
        //                parameters[13].Value = preOrder19dian.preOrderServerSum;
        //                parameters[14].Value = preOrder19dian.verifiedSaving;
        //                return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters) > 0;
        //            }
        //        }
        ///// <summary>
        ///// 更新预点单折扣信息
        ///// </summary>
        ///// <param name="preOrder19dianId"></param>
        ///// <param name="discount"></param>
        ///// <returns></returns>
        //public bool UpdatePreOrderDiscount(long preOrder19dianId, double discount)
        //{
        //    using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
        //    {
        //        int result = 0;
        //        try
        //        {
        //            conn.Open();
        //            StringBuilder strSql = new StringBuilder();
        //            strSql.Append("update PreOrder19dian set ");
        //            strSql.Append("discount=@discount");
        //            strSql.Append(" where preOrder19dianId=@preOrder19dianId ");
        //            SqlParameter[] parameters = {                   
        //            new SqlParameter("@discount", SqlDbType.Float),
        //            new SqlParameter("@preOrder19dianId", SqlDbType.BigInt,8)};
        //            parameters[0].Value = discount;
        //            parameters[1].Value = preOrder19dianId;
        //            result = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);
        //        }
        //        catch (Exception)
        //        {
        //            return false;
        //        }
        //        if (result == 1)
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //}

        ///// <summary>
        ///// 修改已支付点单跟新暂存JSON表
        ///// </summary>
        ///// <param name="preOrder19dianId"></param>
        ///// <param name="orderInJson"></param>
        ///// <param name="clientCalculatedSum"></param>
        ///// <param name="sundryJson"></param>
        ///// <returns></returns>
        //public bool IndertPreOrder19dianUpdateJson(long preOrder19dianId, string orderInJson, double clientCalculatedSum, string sundryJson)
        //{
        //    using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
        //    {
        //        bool result = false;
        //        try
        //        {
        //            conn.Open();
        //            StringBuilder strSql = new StringBuilder();
        //            strSql.Append("delete from PreOrder19dianUpdate where preOrder19dianId=@preOrder19dianId");
        //            SqlParameter[] parameters = {
        //            new SqlParameter("@preOrder19dianId", SqlDbType.BigInt,8)};
        //            parameters[0].Value = preOrder19dianId;


        //            StringBuilder strSql2 = new StringBuilder();
        //            SqlParameter[] parameters2 = null;
        //            strSql2.Append("insert into PreOrder19dianUpdate(");
        //            strSql2.Append("preOrder19dianId,changeOrderInJson,changeSundryJson,preOrderSum,status)");
        //            strSql2.Append(" values (");
        //            strSql2.Append("@preOrder19dianId,@changeOrderInJson,@changeSundryJson,@preOrderSum,@status)");
        //            parameters2 = new SqlParameter[]{
        //                new SqlParameter("@preorder19dianId",SqlDbType.BigInt,8),
        //                new SqlParameter("@changeOrderInJson",SqlDbType.NVarChar),
        //                new SqlParameter("@changeSundryJson",SqlDbType.NVarChar),
        //                new SqlParameter("@preOrderSum",SqlDbType.Float),
        //                 new SqlParameter("@status",SqlDbType.Int)
        //            };
        //            parameters2[0].Value = preOrder19dianId;
        //            parameters2[1].Value = orderInJson;
        //            parameters2[2].Value = sundryJson;
        //            parameters2[3].Value = clientCalculatedSum;
        //            parameters2[4].Value = 1;
        //            int r1 = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);
        //            int r2 = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql2.ToString(), parameters2);
        //            if (r2 > 0)
        //            {
        //                result = true;
        //            }
        //        }
        //        catch (Exception)
        //        {
        //            return false;
        //        }
        //        if (result)
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //}

        ///// <summary>
        ///// 更新预点单信息
        ///// </summary>
        ///// <param name="preOrder19dianId"></param>
        ///// <param name="orderInJson"></param>
        ///// <returns></returns>
        //public bool UpdatePreOrder19dianOrderJson(long preOrder19dianId, string orderInJson, double clientCalculatedSum, string sundryJson)
        //{
        //    using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
        //    {
        //        int result = 0;
        //        try
        //        {
        //            conn.Open();
        //            StringBuilder strSql = new StringBuilder();
        //            strSql.Append("update PreOrder19dian set ");
        //            strSql.Append("orderInJson=@orderInJson,");
        //            strSql.Append("preOrderSum=@preOrderSum,");
        //            strSql.Append("sundryJson=@sundryJson");
        //            strSql.Append(" where preOrder19dianId=@preOrder19dianId ");
        //            SqlParameter[] parameters = {
        //            new SqlParameter("@orderInJson", SqlDbType.NVarChar),
        //            new SqlParameter("@preOrderSum", SqlDbType.Float),
        //             new SqlParameter("@sundryJson", SqlDbType.NVarChar),
        //            new SqlParameter("@preOrder19dianId", SqlDbType.BigInt,8)};

        //            parameters[0].Value = orderInJson;
        //            parameters[1].Value = clientCalculatedSum;
        //            parameters[2].Value = sundryJson;
        //            parameters[3].Value = preOrder19dianId;

        //            result = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);
        //        }
        //        catch (Exception)
        //        {
        //            return false;
        //        }
        //        if (result == 1)
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //}
        ///// <summary>
        ///// 更新预支付奖励信息
        ///// </summary>
        ///// <param name="prePayPrivilege"></param>
        ///// <returns></returns>
        //public bool UpdatePrePayPrivilege(PrePayPrivilege prePayPrivilege)
        //{
        //    using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
        //    {
        //        int result = 0;
        //        try
        //        {
        //            conn.Open();
        //            StringBuilder strSql = new StringBuilder();
        //            strSql.Append("update PrePayPrivilege set ");
        //            strSql.Append("prePayPrivilegeStr=@prePayPrivilegeStr,");
        //            strSql.Append("prePayCashBackType=@prePayCashBackType,");
        //            strSql.Append("prePayCashBackValue=@prePayCashBackValue,");
        //            strSql.Append("supportPrePayCashBack=@supportPrePayCashBack,");
        //            strSql.Append("supportPrePayVIPEntrance=@supportPrePayVIPEntrance,");
        //            strSql.Append("supportPrePayGift=@supportPrePayGift,");
        //            strSql.Append("vipEntranceMax=@vipEntranceMax,");
        //            strSql.Append("giftContent=@giftContent,");
        //            strSql.Append("numberOfHoursAhead=@numberOfHoursAhead,");
        //            strSql.Append("prePayCashMin=@prePayCashMin,");
        //            strSql.Append("timeType=@timeType,");
        //            strSql.Append("dayCount=@dayCount,");
        //            strSql.Append("effectTime=@effectTime,");
        //            strSql.Append("useCountinTime=@useCountinTime,");
        //            strSql.Append("returnCouponInterval=@returnCouponInterval");
        //            strSql.Append(" where id=@id ");
        //            SqlParameter[] parameters = {                   
        //            new SqlParameter("@prePayPrivilegeStr", SqlDbType.NVarChar,200),
        //            new SqlParameter("@prePayCashBackType", SqlDbType.SmallInt,2),
        //            new SqlParameter("@prePayCashBackValue", SqlDbType.Float),
        //            new SqlParameter("@supportPrePayCashBack", SqlDbType.SmallInt,2),
        //            new SqlParameter("@supportPrePayVIPEntrance", SqlDbType.SmallInt,2),
        //            new SqlParameter("@supportPrePayGift", SqlDbType.SmallInt,2),
        //            new SqlParameter("@vipEntranceMax", SqlDbType.Int,2),
        //            new SqlParameter("@giftContent", SqlDbType.NVarChar,50),
        //            new SqlParameter("@numberOfHoursAhead", SqlDbType.Float),
        //            new SqlParameter("@prePayCashMin", SqlDbType.Float),
        //            new SqlParameter("@id", SqlDbType.Int,4),
        //            new SqlParameter("@timeType", SqlDbType.Int,4),
        //            new SqlParameter("@dayCount", SqlDbType.Float),
        //            new SqlParameter("@effectTime", SqlDbType.Float),
        //            new SqlParameter("@useCountinTime", SqlDbType.Int,4),
        //            new SqlParameter("@returnCouponInterval",SqlDbType.Float)
        //                                        };

        //            parameters[0].Value = prePayPrivilege.prePayPrivilegeStr;
        //            parameters[1].Value = prePayPrivilege.prePayCashBackType;
        //            parameters[2].Value = prePayPrivilege.prePayCashBackValue;
        //            parameters[3].Value = prePayPrivilege.supportPrePayCashBack;
        //            parameters[4].Value = prePayPrivilege.supportPrePayVIPEntrance;
        //            parameters[5].Value = prePayPrivilege.supportPrePayGift;
        //            parameters[6].Value = prePayPrivilege.vipEntranceMax;
        //            parameters[7].Value = prePayPrivilege.giftContent;
        //            parameters[8].Value = prePayPrivilege.numberOfHoursAhead;
        //            parameters[9].Value = prePayPrivilege.prePayCashMin;
        //            parameters[10].Value = prePayPrivilege.id;
        //            parameters[11].Value = prePayPrivilege.timeType;
        //            parameters[12].Value = prePayPrivilege.dayCount;
        //            parameters[13].Value = prePayPrivilege.effectTime;
        //            parameters[14].Value = prePayPrivilege.useCountinTime;
        //            parameters[15].Value = prePayPrivilege.returnCouponInterval;
        //            result = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);
        //        }
        //        catch (Exception)
        //        {
        //            return false;
        //        }
        //        if (result == 1)
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //}
        ///// <summary>
        ///// 更新预支付奖励状态
        ///// </summary>
        ///// <param name="prePayPrivilege"></param>
        ///// <returns></returns>
        //public bool UpdatePrePayPrivilegeStatus(int prepayPrivilegeid, int isValid)
        //{
        //    using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
        //    {
        //        int result = 0;
        //        try
        //        {
        //            conn.Open();
        //            StringBuilder strSql = new StringBuilder();
        //            strSql.Append("update PrePayPrivilege set ");
        //            strSql.Append("isValid=@isValid");
        //            strSql.Append(" where id=@id ");
        //            SqlParameter[] parameters = {
        //            new SqlParameter("@isValid", SqlDbType.SmallInt,2),
        //            new SqlParameter("@id", SqlDbType.Int,4)};

        //            parameters[0].Value = isValid;
        //            parameters[1].Value = prepayPrivilegeid;

        //            result = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);
        //        }
        //        catch (Exception)
        //        {
        //            return false;
        //        }
        //        if (result == 1)
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //}

        ///// <summary>
        ///// new根据预点单编号修改该预点单是否由商家查询过
        ///// </summary>
        ///// <param name="preOrder19dianId"></param>
        ///// <returns></returns>
        //public bool UpdatePreOrderShopQueryFlag(long preOrder19dianId)
        //{
        //    using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
        //    {
        //        int result = 0;
        //        try
        //        {
        //            conn.Open();
        //            StringBuilder strSql = new StringBuilder();
        //            strSql.Append("update PreOrder19dian set ");
        //            strSql.Append("isShopQueried=@isShopQueried");
        //            strSql.Append(" where preOrder19dianId=@preOrder19dianId ");
        //            SqlParameter[] parameters = {                   
        //            new SqlParameter("@isShopQueried", SqlDbType.SmallInt,2),
        //            new SqlParameter("@preOrder19dianId", SqlDbType.BigInt,8)};

        //            parameters[0].Value = 1;
        //            parameters[1].Value = preOrder19dianId;

        //            result = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);
        //        }
        //        catch (Exception)
        //        {
        //            return false;
        //        }
        //        if (result == 1)
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //}
        ///// <summary>
        ///// new根据预点单编号修改该预点单是否由商家查询过
        ///// </summary>
        ///// <param name="preOrder19dianId"></param>
        ///// <returns></returns>
        //public bool UpdatePreOrderShopVerifyFlag(long preOrder19dianId, VAPreorderIsShopVerified verify, VAPreorderStatus status)
        //{
        //    using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
        //    {
        //        int result = 0;
        //        try
        //        {
        //            conn.Open();
        //            StringBuilder strSql = new StringBuilder();
        //            strSql.Append("update PreOrder19dian set ");
        //            strSql.Append("isShopQueried=@isShopQueried,");
        //            strSql.Append("isShopVerified=@isShopVerified,");
        //            strSql.Append("status=@status");
        //            strSql.Append(" where preOrder19dianId=@preOrder19dianId ");
        //            SqlParameter[] parameters = {                   
        //            new SqlParameter("@isShopQueried", SqlDbType.SmallInt,2),
        //            new SqlParameter("@isShopVerified", SqlDbType.SmallInt,2),
        //            new SqlParameter("@status", SqlDbType.SmallInt,2),
        //            new SqlParameter("@preOrder19dianId", SqlDbType.BigInt,8)};

        //            parameters[0].Value = 1;
        //            parameters[1].Value = verify;
        //            parameters[2].Value = status;
        //            parameters[3].Value = preOrder19dianId;

        //            result = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);
        //        }
        //        catch (Exception)
        //        {
        //            return false;
        //        }
        //        if (result == 1)
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //}
        ///// <summary>
        ///// new根据预点单编号修改该预点单的实际节省金额和服务器计算的总计
        ///// </summary>
        ///// <param name="preOrder19dianId"></param>
        ///// <returns></returns>
        //public bool UpdatePreOrderServerSumAndSaving(long preOrder19dianId, double preOrderServerSum, double verifiedSaving)
        //{
        //    using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
        //    {
        //        int result = 0;
        //        try
        //        {
        //            conn.Open();
        //            StringBuilder strSql = new StringBuilder();
        //            strSql.Append("update PreOrder19dian set ");
        //            strSql.Append("preOrderServerSum=@preOrderServerSum,");
        //            strSql.Append("verifiedSaving=@verifiedSaving");
        //            strSql.Append(" where preOrder19dianId=@preOrder19dianId ");
        //            SqlParameter[] parameters = {                   
        //            new SqlParameter("@preOrderServerSum", SqlDbType.Float),
        //            new SqlParameter("@verifiedSaving", SqlDbType.Float),
        //            new SqlParameter("@preOrder19dianId", SqlDbType.BigInt,8)};

        //            parameters[0].Value = preOrderServerSum;
        //            parameters[1].Value = verifiedSaving;
        //            parameters[2].Value = preOrder19dianId;

        //            result = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);
        //        }
        //        catch (Exception)
        //        {
        //            return false;
        //        }
        //        if (result == 1)
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //}
        ///// <summary>
        ///// new根据预点单编号修改该预点单是否分享过微博
        ///// </summary>
        ///// <param name="preOrder19dianId"></param>
        ///// <returns></returns>
        //public bool UpdatePreOrderWeiboShareFlag(long preOrder19dianId)
        //{
        //    using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
        //    {
        //        int result = 0;
        //        try
        //        {
        //            conn.Open();
        //            StringBuilder strSql = new StringBuilder();
        //            strSql.Append("update PreOrder19dian set ");
        //            strSql.Append("isWeiboShared=@isWeiboShared");
        //            strSql.Append(" where preOrder19dianId=@preOrder19dianId ");
        //            SqlParameter[] parameters = {                   
        //            new SqlParameter("@isWeiboShared", SqlDbType.SmallInt,2),
        //            new SqlParameter("@preOrder19dianId", SqlDbType.BigInt,8)};

        //            parameters[0].Value = 1;
        //            parameters[1].Value = preOrder19dianId;

        //            result = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);
        //        }
        //        catch (Exception)
        //        {
        //            return false;
        //        }
        //        if (result == 1)
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //}
        ///// <summary>
        ///// new根据预点单编号修改对应的预支付奖励编号
        ///// </summary>
        ///// <param name="preOrder19dianId"></param>
        ///// <returns></returns>
        //public bool UpdatePreOrderPrePayPrivilegeId(long preOrder19dianId, int prePayPrivilegeId)
        //{
        //    using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
        //    {
        //        int result = 0;
        //        try
        //        {
        //            conn.Open();
        //            StringBuilder strSql = new StringBuilder();
        //            strSql.Append("update PreOrder19dian set ");
        //            strSql.Append("prePayPrivilegeId=@prePayPrivilegeId");
        //            strSql.Append(" where preOrder19dianId=@preOrder19dianId ");
        //            SqlParameter[] parameters = {                   
        //            new SqlParameter("@prePayPrivilegeId", SqlDbType.Int,4),
        //            new SqlParameter("@preOrder19dianId", SqlDbType.BigInt,8)};

        //            parameters[0].Value = prePayPrivilegeId;
        //            parameters[1].Value = preOrder19dianId;

        //            result = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);
        //        }
        //        catch (Exception)
        //        {
        //            return false;
        //        }
        //        if (result == 1)
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //}
        ///// <summary>
        ///// new根据公司编号增加预支付点单计数（+1）
        ///// </summary>
        ///// <param name="companyId"></param>
        ///// <returns></returns>
        //public bool UpdateCompanyPrepayOrderCount(int companyId)
        //{
        //    using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
        //    {
        //        int result = 0;
        //        try
        //        {
        //            conn.Open();
        //            StringBuilder strSql = new StringBuilder();
        //            strSql.Append("update CompanyInfo set ");
        //            strSql.Append("prepayOrderCount = isnull(prepayOrderCount, 0) + @value");
        //            //保证更新后的余额大于零
        //            strSql.Append(" where companyID=@companyID");
        //            SqlParameter[] parameters = {                   
        //            new SqlParameter("@value", SqlDbType.Int,4),
        //            new SqlParameter("@companyID", SqlDbType.Int,4)};

        //            parameters[0].Value = 1;
        //            parameters[1].Value = companyId;

        //            result = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);
        //        }
        //        catch (Exception)
        //        {
        //            return false;
        //        }
        //        if (result == 1)
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //}
        ///// <summary>
        ///// new根据公司编号增加未支付点单计数（+1）
        ///// </summary>
        ///// <param name="companyId"></param>
        ///// <returns></returns>
        //public bool UpdateCompanyPreorderCount(int companyId)
        //{
        //    using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
        //    {
        //        int result = 0;
        //        try
        //        {
        //            conn.Open();
        //            StringBuilder strSql = new StringBuilder();
        //            strSql.Append("update CompanyInfo set ");
        //            strSql.Append("preorderCount = isnull(preorderCount, 0) + @value");
        //            //保证更新后的余额大于零
        //            strSql.Append(" where companyID=@companyID");
        //            SqlParameter[] parameters = {                   
        //            new SqlParameter("@value", SqlDbType.Int,4),
        //            new SqlParameter("@companyID", SqlDbType.Int,4)};

        //            parameters[0].Value = 1;
        //            parameters[1].Value = companyId;

        //            result = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);
        //        }
        //        catch (Exception)
        //        {
        //            return false;
        //        }
        //        if (result == 1)
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //}
        ///// <summary>
        ///// (hyr)根据id修改该预点单为已被申请回款,
        ///// </summary>
        ///// <param name="preOrder19dianId"></param>
        ///// <returns></returns>
        //public int UpdatePreOrderIsApplyFlag(long preOrder19dianId)
        //{
        //    using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
        //    {
        //        int result = 0;
        //        try
        //        {
        //            StringBuilder strSql = new StringBuilder();
        //            strSql.Append("update PreOrder19dian set ");
        //            strSql.Append("isApplied=@isApplied");
        //            strSql.Append(" where preOrder19dianId=@preOrder19dianId and isnull([isApplied],'0' )<>" + (int)VAPreOrderIsApplied.ISAPPLIED);
        //            SqlParameter[] parameters = {                   
        //            new SqlParameter("@isApplied", SqlDbType.TinyInt,1),
        //            new SqlParameter("@preOrder19dianId", SqlDbType.BigInt,8)};

        //            parameters[0].Value = (int)VAPreOrderIsApplied.ISAPPLIED;
        //            parameters[1].Value = preOrder19dianId;

        //            result = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);
        //        }
        //        catch (Exception)
        //        {
        //            return 0;
        //        }
        //        return result;
        //    }
        //}

        ///// <summary>
        ///// new根据预点单编号增加预点单被顶的计数（+1）
        ///// </summary>
        ///// <param name="preorderId"></param>
        ///// <returns></returns>
        //public bool UpdatePreorderSupportCount(long preorderId)
        //{
        //    using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
        //    {
        //        int result = 0;
        //        try
        //        {
        //            conn.Open();
        //            StringBuilder strSql = new StringBuilder();
        //            strSql.Append("update PreOrder19dian set ");
        //            strSql.Append("preorderSupportCount = isnull(preorderSupportCount, 0) + @value");
        //            //保证更新后的值小于99999999零
        //            strSql.Append(" where preOrder19dianId=@preOrder19dianId and (isnull(preorderSupportCount, 0) + @value) < 100000000");
        //            SqlParameter[] parameters = {                   
        //            new SqlParameter("@value", SqlDbType.Int,4),
        //            new SqlParameter("@preOrder19dianId", SqlDbType.BigInt)};

        //            parameters[0].Value = 1;
        //            parameters[1].Value = preorderId;

        //            result = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);
        //        }
        //        catch (Exception)
        //        {
        //            return false;
        //        }
        //        if (result == 1)
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //}
        ///// <summary>
        ///// 新增预点单查询信息
        ///// </summary>
        ///// <param name="preOrderQueryInfo"></param>
        ///// <returns></returns>
        //public long InsertPreOrderQueryInfo(PreOrderQueryInfo preOrderQueryInfo)
        //{
        //    using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
        //    {
        //        Object obj = null;
        //        try
        //        {
        //            conn.Open();
        //            StringBuilder strSql = new StringBuilder();
        //            SqlParameter[] parameters = null;
        //            strSql.Append("insert into PreOrderQueryInfo(");
        //            strSql.Append("preorder19dianId,shopId,queryTime,isShopVerified)");
        //            strSql.Append(" values (");
        //            strSql.Append("@preorder19dianId,@shopId,@queryTime,@isShopVerified)");
        //            strSql.Append(" select @@identity");
        //            parameters = new SqlParameter[]{
        //                new SqlParameter("@preorder19dianId",SqlDbType.BigInt,8),
        //                new SqlParameter("@shopId",SqlDbType.Int,4),
        //                new SqlParameter("@queryTime",SqlDbType.DateTime),
        //                new SqlParameter("@isShopVerified",SqlDbType.SmallInt,2)
        //            };
        //            parameters[0].Value = preOrderQueryInfo.preorder19dianId;
        //            parameters[1].Value = preOrderQueryInfo.shopId;
        //            parameters[2].Value = preOrderQueryInfo.queryTime;
        //            parameters[3].Value = preOrderQueryInfo.isShopVerified;

        //            obj = SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql.ToString(), parameters);
        //        }
        //        catch (Exception)
        //        {
        //            return 0;
        //        }
        //        if (obj == null)
        //        {
        //            return 0;
        //        }
        //        else
        //        {
        //            return Convert.ToInt64(obj);
        //        }
        //    }
        //}
        ///// <summary>
        ///// 更新预点单查询信息（有验证到取消验证）
        ///// </summary>
        ///// <param name="preOrderQuery"></param>
        ///// <returns></returns>
        //public bool UpdatePreOrderQueryInfo(PreOrderQueryInfo preOrderQuery)
        //{
        //    using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
        //    {
        //        int result = 0;
        //        try
        //        {
        //            conn.Open();
        //            StringBuilder strSql = new StringBuilder();
        //            strSql.Append("update PreOrderQueryInfo set ");
        //            strSql.Append("isShopVerified=@isShopVerified,");
        //            strSql.Append("queryTime=@queryTime");
        //            strSql.Append(" where preOrder19dianId=@preOrder19dianId and isShopVerified=1");
        //            SqlParameter[] parameters = {                   
        //            new SqlParameter("@isShopVerified", SqlDbType.SmallInt,2),
        //            new SqlParameter("@queryTime", SqlDbType.DateTime),
        //            new SqlParameter("@preOrder19dianId", SqlDbType.BigInt,8)};

        //            parameters[0].Value = preOrderQuery.isShopVerified;
        //            parameters[1].Value = preOrderQuery.queryTime;
        //            parameters[2].Value = preOrderQuery.preorder19dianId;

        //            result = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);
        //        }
        //        catch (Exception)
        //        {
        //            return false;
        //        }
        //        if (result == 1)
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //}

        ///// <summary>
        ///// 新增预点单微博分享信息
        ///// </summary>
        ///// <param name="preOrderWeiboShare"></param>
        ///// <returns></returns>
        //public int InsertPreOrderWeiboShare(PreOrderWeiboShare preOrderWeiboShare)
        //{
        //    using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
        //    {
        //        Object obj = null;
        //        try
        //        {
        //            conn.Open();
        //            StringBuilder strSql = new StringBuilder();
        //            SqlParameter[] parameters = null;
        //            strSql.Append("insert into PreOrderWeiboShare(");
        //            strSql.Append("preorder19dianId,openIdVendor,shareTime)");
        //            strSql.Append(" values (");
        //            strSql.Append("@preorder19dianId,@openIdVendor,@shareTime)");
        //            strSql.Append(" select @@identity");
        //            parameters = new SqlParameter[]{
        //                new SqlParameter("@preorder19dianId",SqlDbType.BigInt,8),
        //                new SqlParameter("@openIdVendor",SqlDbType.Int,4),
        //                new SqlParameter("@shareTime",SqlDbType.DateTime)
        //            };
        //            parameters[0].Value = preOrderWeiboShare.preorder19dianId;
        //            parameters[1].Value = preOrderWeiboShare.openIdVendor;
        //            parameters[2].Value = preOrderWeiboShare.shareTime;

        //            obj = SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql.ToString(), parameters);
        //        }
        //        catch (Exception)
        //        {
        //            return 0;
        //        }
        //        if (obj == null)
        //        {
        //            return 0;
        //        }
        //        else
        //        {
        //            return Convert.ToInt32(obj);
        //        }
        //    }
        //}
        ///// <summary>
        ///// 判断预点单是否分享过
        ///// </summary>
        ///// <param name="preorderId"></param>
        ///// <returns></returns>
        //public bool IsPreOrderShared(long preorderId)
        //{
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append("select [preOrder19dianId]");
        //    strSql.AppendFormat(" from PreOrder19dian where isWeiboShared = '1' and preOrder19dianId = '{0}'", preorderId);

        //    DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
        //    if (ds.Tables[0].Rows.Count == 1)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}
        ///// <summary>
        ///// 根据预点单编号查询预点单信息和对应的用户信息
        ///// </summary>
        ///// <param name="preorderId"></param>
        ///// <returns></returns>
        //public DataTable SelectPreOrderAndCustomer(long preorderId)
        //{
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append("select A.[orderInJson],A.[preOrderTime],B.CustomerFirstName,A.prePaidSum,A.preOrderServerSum,");
        //    strSql.Append("B.CustomerLastName,B.mobilePhoneNumber,B.UserName,");
        //    strSql.Append("B.eCardNumber,B.CustomerSex,B.isVIP,A.preorderSupportCount,");
        //    strSql.Append("C.shopLogo,C.shopImagePath");
        //    strSql.Append(" from CustomerInfo as B inner join");
        //    strSql.Append(" PreOrder19dian as A on B.eCardNumber = A.eCardNumber inner join");
        //    strSql.AppendFormat(" ShopInfo as C on C.shopID = A.shopId where A.preOrder19dianId = '{0}'", preorderId);
        //    DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());

        //    return ds.Tables[0];
        //}

        ///// <summary>
        ///// 根据点单ID查询已支付点单新菜单和杂项信息
        ///// </summary>
        ///// <param name="preorderId"></param>
        ///// <returns></returns>
        //public DataTable SelectPreOrderUpdate(long preorderId)
        //{
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append("select changeOrderInJson,changeSundryJson,preOrderSum from PreOrder19dianUpdate where  ");
        //    strSql.AppendFormat("preOrder19dianId ='" + preorderId + "'");
        //    DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
        //    return ds.Tables[0];
        //}
        ///// <summary>
        ///// 查询点单优惠劵关联（已支付修改暂存）
        ///// </summary>
        ///// <param name="preorderId"></param>
        ///// <returns></returns>
        //public DataTable SelectCustomerCouponPreOrderUpdate(long preorderId)
        //{
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append("select customerConnCouponID,customerID from CustomerCouponPreOrderUpdate where  ");
        //    strSql.AppendFormat("preOrderID ='" + preorderId + "'");
        //    DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
        //    return ds.Tables[0];
        //}
        ///// <summary>
        ///// 根据预点单编号查询预点单已顶次数
        ///// </summary>
        ///// <param name="preorderId"></param>
        ///// <returns></returns>
        //public int SelectPreOrderSuportCount(long preorderId)
        //{
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append("select A.preorderSupportCount from PreOrder19dian as A");
        //    strSql.AppendFormat(" where A.preOrder19dianId = '{0}'", preorderId);
        //    DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
        //    if (ds.Tables[0].Rows.Count == 1)
        //    {
        //        return Convert.ToInt32(ds.Tables[0].Rows[0]["preorderSupportCount"]);
        //    }
        //    else
        //    {
        //        return 0;
        //    }
        //}
        ///// <summary>
        ///// 根据预点单编号查询本周预点单（根据suportCount排序）
        ///// 20140620 xiaoyu 取消该功能
        ///// </summary>
        ///// <param name="preorderId"></param>
        ///// <returns></returns>
        //public DataTable SelectPreOrderSeqencingForCurrentWeek()
        //{
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append("select RANK() over(order by preorderSupportCount desc,");
        //    strSql.Append("preOrderTime asc) as sequence,preOrder19dianId");
        //    strSql.Append(" from PreOrder19dian as A where datediff(week , preOrderTime ,getdate()) = 0");
        //    DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
        //    return ds.Tables[0];
        //}
        ///// <summary>
        ///// 根据预点单编号查询本月预点单（根据suportCount排序）
        ///// 20140620 xiaoyu 取消该功能
        ///// </summary>
        ///// <param name="preorderId"></param>
        ///// <returns></returns>
        //public DataTable SelectPreOrderSeqencingForCurrentMonth()
        //{
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append("select RANK() over(order by preorderSupportCount desc,");
        //    strSql.Append("preOrderTime asc) as sequence,preOrder19dianId");
        //    strSql.Append(" from PreOrder19dian as A where datediff(mm, preOrderTime, GetDate()) = 0");
        //    DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
        //    return ds.Tables[0];
        //}

        //        /// <summary>
        //        ///  根据用户编号查询预点单信息和对应的公司门店信息
        //        /// </summary>
        //        /// <param name="customerID"></param>
        //        /// <param name="isWechatCustomer"></param>
        //        /// <returns></returns>
        //        public DataTable SelectPreOrderAndCompany(long customerID, bool isWechatCustomer)
        //        {
        //            StringBuilder strSql = new StringBuilder();
        //            strSql.AppendFormat(@" select A.preOrder19dianId,A.[orderInJson],A.[preOrderTime],A.[preOrderSum],A.[isShopConfirmed],
        //A.[prePayTime],A.companyId,A.status,A.shopId,isnull(A.isPaid,0) as isPaid,A.sundryJson,A.refundMoneySum,A.refundMoneyClosedSum,
        //A.[evaluationValue],A.preOrderServerSum,A.customerId,A.prePaidSum,A.snsMessageJson,A.snsShareImageUrl,A.verifiedSaving ,A.validFromDate,
        //C.shopName,invoiceTitle,discount,deskNumber,isSupportAccountsRound ,C.shopImagePath,C.shopLogo
        //                                   from CompanyInfo as B 
        //                                   inner join PreOrder19dian as A on B.companyID = A.companyId  
        //                                   inner join ShopInfo as C on C.shopID =A.shopId 
        //                                   where A.customerId ={0}  and A.status <> {1}", customerID, (int)VAPreorderStatus.Deleted);
        //            if (isWechatCustomer)
        //            {
        //                strSql.AppendFormat(" and A.isPaid <> {0}", (int)VAPreorderIsPaid.PAID);
        //            }
        //            strSql.Append(" order by preOrderTime desc");
        //            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
        //            return ds.Tables[0];
        //        }

        ///// <summary>
        ///// 根据公司编号查询对应的预支付奖励信息
        ///// </summary>
        ///// <param name="companyId"></param>
        ///// <returns></returns>
        //public DataTable SelectPrePayPrivilegeByCompany(int companyId)
        //{
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append("select A.[id],A.[companyId],A.[prePayPrivilegeStr],A.[prePayCashBackType],A.[prePayCashBackValue],A.[numberOfHoursAhead]");
        //    strSql.Append(" from PrePayPrivilege as A");
        //    strSql.AppendFormat(" where A.[companyId] = '{0}' and A.[isValid] = 1", companyId);

        //    DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());

        //    return ds.Tables[0];
        //}
        ///// <summary>
        ///// 根据店铺编号查询对应的预支付奖励信息
        ///// </summary>
        ///// <param name="shopId"></param>
        ///// <returns></returns>
        //public DataTable SelectPrePayPrivilegeByShop(int shopId)
        //{
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append("select A.[id],A.[shopId],A.[prePayPrivilegeStr],A.[prePayCashBackType],A.[prePayCashBackValue],A.[numberOfHoursAhead],");
        //    strSql.Append("A.[supportPrePayCashBack],A.[supportPrePayVIPEntrance],A.[supportPrePayGift],A.[isValid],A.[vipEntranceMax],A.[giftContent],A.[dayCount],A.[timeType],A.[prePayCashMin],A.[effectTime],A.[useCountinTime],A.returnCouponInterval");
        //    strSql.Append(" from PrePayPrivilege as A");
        //    strSql.AppendFormat(" where A.[shopId] = '{0}'", shopId);

        //    DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());

        //    return ds.Tables[0];
        //}
        ///// <summary>
        ///// 根据预支付奖励编号查询对应的信息
        ///// </summary>
        ///// <param name="privilegeId"></param>
        ///// <returns></returns>
        //public DataTable SelectPrePayPrivilege(int privilegeId)
        //{
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append("select A.[id],A.[shopId],A.[prePayPrivilegeStr],A.[prePayCashBackType],A.[prePayCashBackValue],A.[numberOfHoursAhead],");
        //    strSql.Append("A.[supportPrePayCashBack],A.[supportPrePayVIPEntrance],A.[supportPrePayGift],A.[vipEntranceMax],A.[giftContent],A.prePayCashMin,A.timeType,A.dayCount,A.effectTime,A.useCountinTime,A.returnCouponInterval from PrePayPrivilege as A");
        //    strSql.AppendFormat(" where A.[id] = '{0}'", privilegeId);

        //    DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());

        //    return ds.Tables[0];
        //}

        ///// <summary>
        ///// 根据门店Id查询免返送信息
        ///// </summary>
        ///// <returns></returns>
        //public DataTable SelectFreeToReturnToSend(int shopId)
        //{
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append("select description,descriptionType,shopId");
        //    strSql.Append(" from FreeToReturnToSend");
        //    strSql.AppendFormat(" where shopId = '{0}'", shopId);
        //    DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
        //    return ds.Tables[0];
        //}
        ///// <summary>
        ///// 在门店免返送信息表中中添加一条记录
        ///// </summary>
        ///// <returns></returns>
        //public bool InsertFreeToReturnToSendInfo(int shopId, int descriptionType, string description)
        //{
        //    StringBuilder strSql = new StringBuilder();
        //    SqlParameter[] param ={
        //                              new SqlParameter("@shopId",shopId),
        //                              new SqlParameter("@descriptionType",descriptionType),
        //                              new SqlParameter("@description",description)
        //                          };
        //    strSql.Append("insert into FreeToReturnToSend (description,descriptionType,shopId)");
        //    strSql.Append(" values (@description,@descriptionType,@shopId);");
        //    int resultInsertFreeToReturnToSendInfo = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), param);
        //    if (resultInsertFreeToReturnToSendInfo > 0)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}
        ///// <summary>
        ///// 修改门店免返送信息
        ///// </summary>
        ///// <returns></returns>
        //public bool UpdateFreeToReturnToSend(int shopId, int descriptionType, string description)
        //{
        //    StringBuilder strSql = new StringBuilder();
        //    SqlParameter[] param ={
        //                              new SqlParameter("@shopId",shopId),
        //                              new SqlParameter("@descriptionType",descriptionType),
        //                              new SqlParameter("@description",description)
        //                          };
        //    strSql.Append("update FreeToReturnToSend set description=@description");
        //    strSql.Append("  where shopId=@shopId and descriptionType=@descriptionType");
        //    int resultUpdateFreeToReturnToSend = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), param);
        //    if (resultUpdateFreeToReturnToSend > 0)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        ///// <summary>
        ///// 根据公司编号查询对应的预点单信息
        ///// </summary>
        ///// <param name="companyId"></param>
        ///// <returns></returns>
        //public DataTable SelectPreOrder19dian(int companyId, int shopId, DateTime timeFrom, DateTime timeTo, string queryCondition)
        //{
        //    StringBuilder strSql = new StringBuilder();
        //    SqlParameter[] param ={
        //                              new SqlParameter("@companyId",companyId),
        //                              new SqlParameter("@timeFrom",timeFrom),
        //                              new SqlParameter("@timeTo",timeTo)
        //                          };
        //    strSql.Append("select [preOrder19dianId],[eCardNumber],customerId,[companyId],[shopId],[orderInJson],[preOrderSum],[preOrderServerSum],[preOrderTime],isnull([isPaid],0) as isPaid,[prePaidSum],[prePayTime],[verificationCode],remoteOrder,invoiceTitle,viewallocPaidToShopSum from PreOrder19dian ");
        //    strSql.Append("  where companyId=@companyId and preOrderTime between @timeFrom and @timeTo ");
        //    switch (queryCondition)
        //    {
        //        case "y"://已支付
        //            strSql.AppendFormat(" and isPaid={0}", (int)VAPreorderIsPaid.PAID);
        //            break;
        //        case "n"://未支付
        //            strSql.AppendFormat(" and (isPaid={0} or isPaid is null)", (int)VAPreorderIsPaid.NOT_PAID);
        //            break;
        //        //case "v"://已验证
        //        //    strSql.AppendFormat(" and isShopVerified={0}", (int)VAPreorderIsShopVerified.SHOPVERIFIED);
        //        //    break;
        //        //case "zy"://支付并且已验证
        //        //    strSql.AppendFormat(" and isPaid={0}", (int)VAPreorderIsPaid.PAID);
        //        //    strSql.AppendFormat(" and isShopVerified={0}", (int)VAPreorderIsShopVerified.SHOPVERIFIED);
        //        //    break;
        //        default://查询的是全部的（已支付，未支付，已验证）
        //            break;
        //    }
        //    if (companyId != 0)
        //    {
        //        strSql.AppendFormat(" and companyId={0}", companyId);
        //    }
        //    if (shopId != 0)
        //    {
        //        strSql.AppendFormat(" and shopId={0}", shopId);
        //    }
        //    DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), param);
        //    return ds.Tables[0];
        //}

        ///// <summary>
        ///// （hyr）查询已对账且未结算完成的预点单信息
        ///// </summary>
        ///// <param name="companyId"></param>
        ///// <returns></returns>
        //public DataTable SelectPreOrderShopApprovedAndNotComplete(int companyId, int shopId)
        //{
        //    StringBuilder strSql = new StringBuilder();
        //    SqlParameter[] param ={
        //                              new SqlParameter("@companyId",companyId),
        //                              new SqlParameter("@shopId",shopId)
        //                          };
        //    strSql.Append("select [preOrder19dianId],A.[companyId],B.[shopName],[prePaidSum],[viewallocCommission],[viewallocNeedsToPayToShop],[verificationCode]");
        //    strSql.Append(" from PreOrder19dian A left join ShopInfo B on A.shopId=B.shopID");
        //    strSql.Append(" where isShopVerified=" + (int)VAPreorderIsShopVerified.SHOPVERIFIED + " and isPaid=" + (int)VAPreorderIsPaid.PAID + " and isApproved=" + (int)VAPreorderIsApproved.APPROVED + " and isnull([viewallocTransactionCompleted],'')<>1 and A.companyId=@companyId ");
        //    if (shopId != 0)
        //    {
        //        strSql.Append(" and A.shopId=@shopId ");
        //    }
        //    DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), param);
        //    return ds.Tables[0];
        //}
        //public List<PreOrder19dianInfo> SelectPreOrderByQuery(PreOrder19dianInfoQueryObject queryObject, int? pageIndxe, int pageSize)
        //{
        //    List<SqlParameter> parameters = new List<SqlParameter>();
        //    StringBuilder strSql = new StringBuilder();
        //    //strSql.Append(" SELECT ");
        //    strSql.Append(" [preOrder19dianId],");
        //    strSql.Append(" [eCardNumber],");
        //    strSql.Append(" [menuId],");
        //    strSql.Append(" [companyId],");
        //    strSql.Append(" [shopId],");
        //    strSql.Append(" [customerId],");
        //    strSql.Append(" [orderInJson],");
        //    strSql.Append(" [customerCookie],");
        //    strSql.Append(" [customerUUID],");
        //    strSql.Append(" [status],");
        //    strSql.Append(" [preOrderSum],");
        //    strSql.Append(" [preOrderServerSum],");
        //    strSql.Append(" [preOrderTime],");
        //    strSql.Append(" [isShopQueried],");
        //    strSql.Append(" [isWeiboShared],");
        //    strSql.Append(" [isShopVerified],");
        //    strSql.Append(" [isPaid],");
        //    strSql.Append(" [prePaidsum],");
        //    strSql.Append(" [prePayPrivilegeId],");
        //    strSql.Append(" [prePayPrivilegeStr],");
        //    strSql.Append(" [numberOfHoursAhead],");
        //    strSql.Append(" [prePayTime],");
        //    strSql.Append(" [prePayCashBack],");
        //    strSql.Append(" [validFromDate],");
        //    strSql.Append(" [viewallocCommission],");
        //    strSql.Append(" [transactionFee],");
        //    strSql.Append(" [viewallocNeedsToPayToShop],");
        //    strSql.Append(" [viewallocPaidToShopSum],");
        //    strSql.Append(" [viewallocTransactionCompleted],");
        //    strSql.Append(" [cashbackReceived],");
        //    strSql.Append(" [verificationCode],");
        //    strSql.Append(" [refundDeadline],");
        //    strSql.Append(" [isApproved],");
        //    strSql.Append(" [isApplied],");
        //    strSql.Append(" [preorderSupportCount],");
        //    strSql.Append(" [snsMessageJson],");
        //    strSql.Append(" [snsShareImageUrl],");
        //    strSql.Append(" [verifiedsaving],");
        //    strSql.Append(" [isShopConfirmed],");
        //    strSql.Append(" [invoiceTitle],");
        //    strSql.Append(" [remoteOrder],");
        //    strSql.Append(" [sundryJson],");
        //    strSql.Append(" [refundMoneySum],");
        //    strSql.Append(" [discount],");
        //    strSql.Append(" [refundMoneyClosedSum],");
        //    strSql.Append(" [evaluationValue],");
        //    strSql.Append(" [deskNumber],");
        //    strSql.Append(" [refundRedEnvelope],");
        //    strSql.Append(" [appType],");
        //    strSql.Append(" [appBuild], ");
        //    strSql.Append(" [IsEvaluation], ");
        //    strSql.Append(" [EvaluationValue], ");
        //    strSql.Append(" [EvaluationContent], ");
        //    strSql.Append(" [EvaluationTime], ");
        //    strSql.Append(" [EvaluationLevel] ");
        //    //strSql.Append("  FROM PreOrder19dian WHERE  1 =1");
        //    StringBuilder whereSql = new StringBuilder();
        //    if (queryObject.isEvaluation.HasValue)
        //    {
        //        whereSql.Append(" AND IsEvaluation = @IsEvaluation");
        //        parameters.Add(new SqlParameter("@IsEvaluation", queryObject.isEvaluation.Value));
        //    }
        //    if (queryObject.shopID.HasValue)
        //    {
        //        whereSql.Append(" AND shopID = @shopID");
        //        parameters.Add(new SqlParameter("@shopID", queryObject.shopID.Value));
        //    }
        //    //分页
        //    if (pageIndxe.HasValue)
        //    {
        //        strSql.Insert(0, "SELECT TOP (@pageSize) ");
        //        strSql.AppendFormat("  FROM (SELECT ROW_NUMBER() OVER (ORDER by EvaluationTime DESC) rownumber,* FROM PreOrder19dian WHERE 1=1 {0}" +
        //        ") preOrder WHERE  rownumber >@pageStart ", whereSql.ToString());
        //        parameters.Add(new SqlParameter("@pageSize", pageSize));
        //        parameters.Add(new SqlParameter("@pageStart", (pageIndxe - 1) * pageSize));
        //    }
        //    else
        //    {
        //        strSql.Insert(0, "SELECT ");
        //        strSql.Append("  FROM PreOrder19dian WHERE  1 =1 ");
        //        strSql.Append(whereSql.ToString());
        //    }
        //    // SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters.ToArray());
        //    List<PreOrder19dianInfo> list = new List<PreOrder19dianInfo>();
        //    using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters.ToArray()))
        //    {
        //        while (reader.Read())
        //        {
        //            list.Add(reader.GetEntity<PreOrder19dianInfo>());
        //        }
        //    }
        //    return list;

        //}

        //public int GetPreOrderCountByQuery(PreOrder19dianInfoQueryObject queryObject)
        //{
        //    List<SqlParameter> parameters = new List<SqlParameter>();
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append(" SELECT COUNT(0)  FROM PreOrder19dian WHERE  1 =1  ");
        //    if (queryObject.isEvaluation.HasValue)
        //    {
        //        strSql.Append(" AND IsEvaluation = @IsEvaluation");
        //        parameters.Add(new SqlParameter("@IsEvaluation", queryObject.isEvaluation.Value));
        //    }
        //    if (queryObject.shopID.HasValue)
        //    {
        //        strSql.Append(" AND shopID = @shopID");
        //        parameters.Add(new SqlParameter("@shopID", queryObject.shopID.Value));
        //    }
        //    object result = SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters.ToArray());
        //    return Convert.ToInt32(result);
        //}
        ///// <summary>
        ///// 查询点单的最新验证时间
        ///// </summary>
        ///// <param name="preOrder19dianId">点单流水号</param>
        ///// <returns></returns>
        //public DateTime GetPreOrderMaxQueryTime(long preOrder19dianId)
        //{
        //    DateTime maxQueryTime = Convert.ToDateTime("2013-01-01 00:00:00");
        //    //string strsql = @"select MAX(PreOrderQueryInfo.queryTime) from PreOrderQueryInfo  where PreOrderQueryInfo.isShopVerified=1 and PreOrderQueryInfo.preorder19dianId=" + preOrder19dianId + "";
        //    string strsql = @"select prePayTime from PreOrder19dian where preorder19dianId =@preOrder19dianId";//20140211 xiaoyu由于新程序取消验证，因此这里直接取支付时间
        //    SqlParameter[] para = new SqlParameter[] { 
        //    new SqlParameter("@preOrder19dianId", SqlDbType.BigInt) { Value = preOrder19dianId }
        //    };
        //    using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strsql, para))
        //    {
        //        if (dr.Read())
        //        {
        //            maxQueryTime = dr[0] != DBNull.Value ? Convert.ToDateTime(dr[0]) : Convert.ToDateTime("2013-01-01 00:00:00");
        //        }
        //    }
        //    return maxQueryTime;
        //}
        ///// <summary>
        ///// 查询当前所有未对账的订单
        ///// </summary>
        ///// <returns></returns>
        //public DataTable SelectAllPreOrder19dianId()
        //{
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append("select preOrder19dianId,shopId,eCardNumber,verificationCode");
        //    strSql.Append(" from PreOrder19dian ");
        //    strSql.Append("  where isShopConfirmed = 1 and (isApproved='" + (int)VAPreorderIsApproved.NOT_APPROVED + "' or isApproved is null)");
        //    DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
        //    return ds.Tables[0];
        //}
        ///// <summary>
        ///// 插入点单优惠劵信息
        ///// </summary>
        ///// <param name="customerID"></param>
        ///// <param name="customerConnCouponID"></param>
        ///// <param name="preOrderID"></param>
        ///// <returns></returns>
        //public bool InsertCustomerCouponPreOrder(long customerID, long customerConnCouponID, long preOrderID)
        //{
        //    using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
        //    {
        //        int obj = 0;
        //        try
        //        {
        //            conn.Open();
        //            StringBuilder strSql = new StringBuilder();
        //            SqlParameter[] parameters = null;
        //            strSql.Append("insert into CustomerCouponPreOrder(");
        //            strSql.Append("customerID,customerConnCouponID,preOrderID,status)");
        //            strSql.Append(" values (");
        //            strSql.Append("@customerID,@customerConnCouponID,@preOrderID,@status)");
        //            parameters = new SqlParameter[]{
        //                new SqlParameter("@customerID",SqlDbType.BigInt,8),
        //                new SqlParameter("@customerConnCouponID",SqlDbType.BigInt,8),
        //                new SqlParameter("@preOrderID",SqlDbType.BigInt,8),
        //                new SqlParameter("@status",SqlDbType.Int)
        //            };
        //            parameters[0].Value = customerID;
        //            parameters[1].Value = customerConnCouponID;
        //            parameters[2].Value = preOrderID;
        //            parameters[3].Value = 1;

        //            obj = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);
        //        }
        //        catch (Exception)
        //        {
        //            obj = 0;
        //        }
        //        if (obj > 0)
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //}
        ///// <summary>
        ///// new根据预点单编号修改该预点单的验证码
        ///// </summary>
        ///// <param name="preOrder19dianId"></param>
        ///// <returns></returns>
        //public bool UpdatePreOrderVerificationCode(long preOrder19dianId, string verificationCode)
        //{
        //    using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
        //    {
        //        int result = 0;
        //        try
        //        {
        //            conn.Open();
        //            StringBuilder strSql = new StringBuilder();
        //            strSql.Append("update PreOrder19dian set ");
        //            strSql.Append("verificationCode=@verificationCode");
        //            strSql.Append(" where preOrder19dianId=@preOrder19dianId ");
        //            SqlParameter[] parameters = {                   
        //            new SqlParameter("@verificationCode", SqlDbType.NVarChar,50),
        //            new SqlParameter("@preOrder19dianId", SqlDbType.BigInt,8)};

        //            parameters[0].Value = verificationCode;
        //            parameters[1].Value = preOrder19dianId;

        //            result = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);
        //        }
        //        catch (Exception)
        //        {
        //            return false;
        //        }
        //        if (result == 1)
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //}

        ///// <summary>
        ///// new根据预点单编号修改该预点单的sns信息
        ///// </summary>
        ///// <param name="preOrder19dianId"></param>
        ///// <returns></returns>
        //public bool UpdatePreOrderSNSMessageJson(long preOrder19dianId, string snsMessageJson)
        //{
        //    using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
        //    {
        //        int result = 0;
        //        try
        //        {
        //            conn.Open();
        //            StringBuilder strSql = new StringBuilder();
        //            strSql.Append("update PreOrder19dian set ");
        //            strSql.Append("snsMessageJson=@snsMessageJson");
        //            strSql.Append(" where preOrder19dianId=@preOrder19dianId ");
        //            SqlParameter[] parameters = {                   
        //            new SqlParameter("@snsMessageJson", SqlDbType.NVarChar),
        //            new SqlParameter("@preOrder19dianId", SqlDbType.BigInt,8)};

        //            parameters[0].Value = snsMessageJson;
        //            parameters[1].Value = preOrder19dianId;

        //            result = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);
        //        }
        //        catch (Exception)
        //        {
        //            return false;
        //        }
        //        if (result == 1)
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //}
        ///// <summary>
        ///// 新增预支付奖励关联优惠券表信息
        ///// </summary>
        //public int InsertPrePayPrivilegeConnCoupon(PrePayPrivilegeConnCouponInfo prePayPrivilegeConnCouponInfo)
        //{
        //    using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
        //    {
        //        Object obj = null;
        //        try
        //        {
        //            conn.Open();
        //            StringBuilder strSql = new StringBuilder();
        //            SqlParameter[] parameters = null;
        //            strSql.Append("insert into PrePayPrivilegeConnCoupon(");
        //            strSql.Append("prePayPrivilegeId,prePayCashMax,prePayCashMin,couponId,returnCouponRule)");
        //            strSql.Append(" values (");
        //            strSql.Append("@prePayPrivilegeId,@prePayCashMax,@prePayCashMin,@couponId,@returnCouponRule)");
        //            strSql.Append(" select @@identity");
        //            parameters = new SqlParameter[]{
        //                new SqlParameter("@prePayPrivilegeId",SqlDbType.Int,4),
        //                new SqlParameter("@prePayCashMax",SqlDbType.Float),
        //                new SqlParameter("@prePayCashMin",SqlDbType.Float),
        //                new SqlParameter("@couponId",SqlDbType.Int,4),
        //                new SqlParameter("@returnCouponRule",SqlDbType.SmallInt,2)
        //            };
        //            parameters[0].Value = prePayPrivilegeConnCouponInfo.prePayPrivilegeId;
        //            parameters[1].Value = prePayPrivilegeConnCouponInfo.prePayCashMax;
        //            parameters[2].Value = prePayPrivilegeConnCouponInfo.prePayCashMin;
        //            parameters[3].Value = prePayPrivilegeConnCouponInfo.couponId;
        //            parameters[4].Value = prePayPrivilegeConnCouponInfo.returnCouponRule;

        //            obj = SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql.ToString(), parameters);
        //        }
        //        catch (Exception)
        //        {
        //            return 0;
        //        }
        //        if (obj == null)
        //        {
        //            return 0;
        //        }
        //        else
        //        {
        //            return Convert.ToInt32(obj);
        //        }
        //    }
        //}

        ///// <summary>
        ///// 修改预支付奖励关联优惠券表信息
        ///// </summary>
        //public bool UpdatePrePayPrivilegeConnCoupon(PrePayPrivilegeConnCouponInfo prePayPrivilegeConnCouponInfo)
        //{
        //    using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
        //    {
        //        int result = 0;
        //        try
        //        {
        //            conn.Open();
        //            StringBuilder strSql = new StringBuilder();
        //            strSql.Append("update PrePayPrivilegeConnCoupon set ");
        //            strSql.Append("prePayCashMax=@invoiceTitle");
        //            strSql.Append("prePayCashMin=@invoiceTitle");
        //            strSql.Append("couponId=@invoiceTitle");
        //            strSql.Append("returnCouponRule=@invoiceTitle");
        //            strSql.Append(" where prePayPrivilegeId=@prePayPrivilegeId ");
        //            SqlParameter[] parameters = {
        //                new SqlParameter("@prePayPrivilegeId",SqlDbType.Int,4),
        //                new SqlParameter("@prePayCashMax",SqlDbType.Float),
        //                new SqlParameter("@prePayCashMin",SqlDbType.Float),
        //                new SqlParameter("@couponId",SqlDbType.Int,4),
        //                new SqlParameter("@returnCouponRule",SqlDbType.SmallInt,2)
        //            };
        //            parameters[0].Value = prePayPrivilegeConnCouponInfo.prePayPrivilegeId;
        //            parameters[1].Value = prePayPrivilegeConnCouponInfo.prePayCashMax;
        //            parameters[2].Value = prePayPrivilegeConnCouponInfo.prePayCashMin;
        //            parameters[3].Value = prePayPrivilegeConnCouponInfo.couponId;
        //            parameters[4].Value = prePayPrivilegeConnCouponInfo.returnCouponRule;
        //            result = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);
        //        }
        //        catch (Exception)
        //        {
        //            return false;
        //        }
        //        if (result == 1)
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //}
        ///// <summary>
        ///// 删除预支付奖励关联优惠券表信息
        ///// </summary>
        //public bool DeletePrePayPrivilegeConnCoupon(int prePayPrivilegeId)
        //{
        //    using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
        //    {
        //        int result = 0;
        //        try
        //        {
        //            conn.Open();
        //            StringBuilder strSql = new StringBuilder();
        //            strSql.Append("delete PrePayPrivilegeConnCoupon  where prePayPrivilegeId=@prePayPrivilegeId");
        //            SqlParameter[] parameters = {
        //            new SqlParameter("@prePayPrivilegeId",SqlDbType.Int,4)};
        //            parameters[0].Value = prePayPrivilegeId;
        //            result = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);
        //        }
        //        catch
        //        {
        //            result = 0;
        //        }
        //        if (result >= 1)
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //}
        ///// <summary>
        ///// 更新点单评价值
        ///// </summary>
        ///// <param name="preOrder19dianId">点单流水号</param>
        ///// <param name="value">评价分值</param>
        ///// <returns></returns>
        //public bool UpdatePreorderEvaluationvalue(long preOrder19dianId, int value)
        //{
        //    using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
        //    {
        //        if (conn.State != ConnectionState.Open)
        //        {
        //            conn.Open();
        //        }
        //        const string strSql = @"update PreOrder19dian set evaluationValue=@evaluationValue where preOrder19dianId=@preOrder19dianId";
        //        SqlParameter[] parameters = {
        //                                            new SqlParameter("@evaluationValue", SqlDbType.SmallInt),
        //                                           new SqlParameter("@preOrder19dianId", SqlDbType.BigInt,8)};
        //        parameters[0].Value = value;
        //        parameters[1].Value = preOrder19dianId;
        //        int result = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);
        //        return result > 0;
        //    }
        //}
        /*********************************************
         * Added By 林东宇 2014-12-08
         * for 更新指定点单的数据
         * *****************************************/
        ///// <summary>
        ///// 更新点单数据
        ///// </summary>
        ///// <param name="preOrder19dianInfo"></param>
        ///// <returns></returns>
        //public bool UpdatePreorder(PreOrder19dianInfo preOrder19dianInfo)
        //{
        //    using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
        //    {
        //        if (conn.State != ConnectionState.Open)
        //        {
        //            conn.Open();
        //        }
        //        string strSql = @"UPDATE PreOrder19dian SET "
        //            // + "eCardNumber=@eCardNumber,	"
        //            //// + "menuId=@menuId,	"
        //            // + "companyId=@companyId,	"
        //            // + "shopId=@shopId,	"
        //            // + "customerId=@customerId,	"
        //            // + "orderInJson=@orderInJson,	"
        //            // + "customerCookie=@customerCookie,	"
        //            // + "customerUUID=@customerUUID,	"
        //            // + "status=@status,	"
        //            // + "preOrderSum=@preOrderSum,	"
        //            // + "preOrderServerSum=@preOrderServerSum,	"
        //            // + "preOrderTime=@preOrderTime,	"
        //            //// + "isShopQueried=@isShopQueried,	"
        //            // + "isWeiboShared=@isWeiboShared,	"
        //            // + "isShopVerified=@isShopVerified,	"
        //            // + "isPaid=@isPaid,	"
        //            // + "prePaidSum=@prePaidSum,	"
        //            // + "prePayPrivilegeId=@prePayPrivilegeId,	"
        //            // + "prePayPrivilegeStr=@prePayPrivilegeStr,	"
        //            // + "numberOfHoursAhead=@numberOfHoursAhead	,"
        //            // + "prePayTime=@prePayTime,	"
        //            // + "prePayCashBack=@prePayCashBack,	"
        //            // + "validFromDate=@validFromDate,	"
        //            // + "viewallocCommission=@viewallocCommission,	"
        //            // + "transactionFee=@transactionFee,	"
        //            // + "viewallocNeedsToPayToShop=@viewallocNeedsToPayToShop,	"
        //            // + "viewallocPaidToShopSum=@viewallocPaidToShopSum,	"
        //            // + "viewallocTransactionCompleted=@viewallocTransactionCompleted,	"
        //            // + "cashbackReceived=@cashbackReceived,	"
        //            // + "verificationCode=@verificationCode,	"
        //            // + "refundDeadline=@refundDeadline,	"
        //            // + "isApproved=@isApproved,	"
        //            // + "isApplied=@isApplied,	"
        //            // + "preorderSupportCount=@preorderSupportCount,	"
        //            // + "snsMessageJson=@snsMessageJson,	"
        //            // + "snsShareImageUrl=@snsShareImageUrl,	"
        //            // + "verifiedSaving=@verifiedSaving,	"
        //            // + "isShopConfirmed=@isShopConfirmed,	"
        //            // + "invoiceTitle=@invoiceTitle,	"
        //            // + "remoteOrder=@remoteOrder,	"
        //            // + "sundryJson=@sundryJson,	"
        //            // + "refundMoneySum=@refundMoneySum,	"
        //            // + "discount=@discount,	"
        //            // + "refundMoneyClosedSum=@refundMoneyClosedSum,	"
        //                          + "evaluationValue=@evaluationValue,	"
        //            // + "deskNumber=@deskNumber,	"
        //            // + "refundRedEnvelope=@refundRedEnvelope,	"
        //            // + "appType=@appType,	"
        //            // + "appBuild=@appBuild,	"
        //                           + "evaluationContent=@evaluationContent,	"
        //                           + "isEvaluation =@isEvaluation,	"
        //                           + "evaluationTime=@evaluationTime,evaluationLevel = @evaluationLevel"
        //                           + " WHERE 	preOrder19dianId=@preOrder19dianId	";

        //        SqlParameter[] parameters = {
        //                                           // new SqlParameter("@eCardNumber",preOrder19dianInfo.eCardNumber),
        //                                           // //new SqlParameter("@menuId",preOrder19dianInfo.menuId),
        //                                           // new SqlParameter("@companyId",preOrder19dianInfo.companyId),
        //                                           // new SqlParameter("@shopId",preOrder19dianInfo.shopId),
        //                                           // new SqlParameter("@customerId",preOrder19dianInfo.customerId),
        //                                           // new SqlParameter("@orderInJson",preOrder19dianInfo.orderInJson),
        //                                           // new SqlParameter("@customerCookie",preOrder19dianInfo.customerCookie),
        //                                           // new SqlParameter("@customerUUID",preOrder19dianInfo.customerUUID),
        //                                           // new SqlParameter("@status",preOrder19dianInfo.status),
        //                                           // new SqlParameter("@preOrderSum",preOrder19dianInfo.preOrderSum),
        //                                           // new SqlParameter("@preOrderServerSum",preOrder19dianInfo.preOrderServerSum),
        //                                           // new SqlParameter("@preOrderTime",preOrder19dianInfo.preOrderTime),
        //                                           //// new SqlParameter("@isShopQueried",preOrder19dianInfo.isShopQueried),
        //                                           // new SqlParameter("@isWeiboShared",(byte)0),
        //                                           // new SqlParameter("@isShopVerified",0),
        //                                           // new SqlParameter("@isPaid",preOrder19dianInfo.isPaid),
        //                                           // new SqlParameter("@prePaidSum",preOrder19dianInfo.prePaidSum),
        //                                           // new SqlParameter("@prePayPrivilegeId",preOrder19dianInfo.prePayPrivilegeId),
        //                                           // new SqlParameter("@prePayPrivilegeStr",preOrder19dianInfo.prePayPrivilegeStr),
        //                                           // new SqlParameter("@numberOfHoursAhead",preOrder19dianInfo.numberOfHoursAhead),
        //                                           // new SqlParameter("@prePayTime",preOrder19dianInfo.prePayTime),
        //                                           // new SqlParameter("@prePayCashBack",preOrder19dianInfo.prePayCashBack),
        //                                           // new SqlParameter("@validFromDate",preOrder19dianInfo.validFromDate),
        //                                           // new SqlParameter("@viewallocCommission",preOrder19dianInfo.viewallocCommission),
        //                                           // new SqlParameter("@transactionFee",preOrder19dianInfo.transactionFee),
        //                                           // new SqlParameter("@viewallocNeedsToPayToShop",preOrder19dianInfo.viewallocNeedsToPayToShop),
        //                                           // new SqlParameter("@viewallocPaidToShopSum",preOrder19dianInfo.viewallocPaidToShopSum),
        //                                           // new SqlParameter("@viewallocTransactionCompleted",preOrder19dianInfo.viewallocTransactionCompleted),
        //                                           // new SqlParameter("@cashbackReceived",preOrder19dianInfo.cashbackReceived),
        //                                           // new SqlParameter("@verificationCode",preOrder19dianInfo.verificationCode),
        //                                           // new SqlParameter("@refundDeadline",preOrder19dianInfo.refundDeadline),
        //                                           // new SqlParameter("@isApproved",preOrder19dianInfo.isApproved),
        //                                           // new SqlParameter("@isApplied",preOrder19dianInfo.isApplied),
        //                                           // new SqlParameter("@preorderSupportCount",preOrder19dianInfo.preorderSupportCount),
        //                                           // new SqlParameter("@snsMessageJson",preOrder19dianInfo.snsMessageJson),
        //                                           // new SqlParameter("@snsShareImageUrl",preOrder19dianInfo.snsShareImageUrl),
        //                                           // new SqlParameter("@verifiedSaving",preOrder19dianInfo.verifiedSaving),
        //                                           // new SqlParameter("@isShopConfirmed",preOrder19dianInfo.isShopConfirmed),
        //                                           // new SqlParameter("@invoiceTitle",preOrder19dianInfo.invoiceTitle),
        //                                           // new SqlParameter("@remoteOrder",preOrder19dianInfo.remoteOrder),
        //                                           // new SqlParameter("@sundryJson",preOrder19dianInfo.sundryJson),
        //                                           // new SqlParameter("@refundMoneySum",preOrder19dianInfo.refundMoneySum),
        //                                           // new SqlParameter("@discount",preOrder19dianInfo.discount),
        //                                           // new SqlParameter("@refundMoneyClosedSum",preOrder19dianInfo.refundMoneyClosedSum),
        //                                            new SqlParameter("@evaluationValue",preOrder19dianInfo.evaluationValue),
        //                                           // new SqlParameter("@deskNumber",preOrder19dianInfo.deskNumber),
        //                                           // new SqlParameter("@refundRedEnvelope",preOrder19dianInfo.refundRedEnvelope),
        //                                           // new SqlParameter("@appType",preOrder19dianInfo.appType),
        //                                           // new SqlParameter("@appBuild",preOrder19dianInfo.appBuild) ,
        //                                            new SqlParameter("@evaluationContent",preOrder19dianInfo.evaluationContent) ,
        //                                            new SqlParameter("@isEvaluation",preOrder19dianInfo.isEvaluation),
        //                                            new SqlParameter("@evaluationTime",preOrder19dianInfo.evaluationTime),
        //                                            new SqlParameter("@evaluationLevel",preOrder19dianInfo.evaluationLevel),
        //                                            new SqlParameter("@preOrder19dianId",preOrder19dianInfo.preOrder19dianId)
        //                                    };

        //        int result = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);
        //        return result > 0;
        //    }
        //}
        /// <summary>
        /// 查询当前点单是否已后台打款
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool SelectOriginalRoadRefundLog(long id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" SELECT [id] FROM [VAGastronomistMobileApp].[dbo].[OriginalRoadRefundInfo]");
            strSql.AppendFormat(" where id='{0}' and status = 2 ", id);
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            if (ds.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 检查某订单是否已申请过原路退款
        /// </summary>
        /// <param name="preOrder19dianId"></param>
        /// <returns>True:申请过</returns>
        public bool IsOriginalRoadRefunded(long preOrder19dianId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("select id from OriginalRoadRefundInfo where connId={0}", preOrder19dianId);
            object obj = SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            if (obj == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }



        /// <summary>
        /// 新增预点单信息
        /// </summary>
        /// <param name="preOrder19dian"></param>
        /// <returns></returns>
        public long InsertPreOrder19dian(PreOrder19dianInfo preOrder19dian)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                const string strSql = @"insert into PreOrder19dian(companyId,shopId,orderInJson,preOrderTime,menuId,status,preOrderSum,customerId,customerUUID,
sundryJson,deskNumber,appType,appBuild,OrderId,OrderType,preOrderServerSum,prePaidSum)  values (
@companyId,@shopId,@orderInJson,@preOrderTime,@menuId,@status,@preOrderSum,@customerId,@customerUUID,@sundryJson,@deskNumber,@appType,@appBuild,@OrderId,@OrderType,@preOrderServerSum,@prePaidSum)
 select @@identity";
                SqlParameter[] parameters = new SqlParameter[]{                    
                        new SqlParameter("@companyId",SqlDbType.Int,4),
                        new SqlParameter("@shopId",SqlDbType.Int,4),
                        new SqlParameter("@orderInJson",SqlDbType.NVarChar),
                        new SqlParameter("@preOrderTime",SqlDbType.DateTime),
                        new SqlParameter("@menuId",SqlDbType.Int,4),
                        new SqlParameter("@status",SqlDbType.SmallInt,2),
                        new SqlParameter("@preOrderSum",SqlDbType.Float),
                        new SqlParameter("@customerId",SqlDbType.BigInt,8),
                        new SqlParameter("@customerUUID",SqlDbType.NVarChar,100),
                        new SqlParameter("@sundryJson",SqlDbType.NVarChar),
                        new SqlParameter("@deskNumber",SqlDbType.NVarChar,50),
                        new SqlParameter("@appType",SqlDbType.Int,4),
                        new SqlParameter("@appBuild",SqlDbType.NVarChar,100),
                        new SqlParameter("@OrderId",SqlDbType.UniqueIdentifier),
                        new SqlParameter("@OrderType",SqlDbType.SmallInt),
                        new SqlParameter("@preOrderServerSum",SqlDbType.Float),
                        new SqlParameter("@prePaidSum",SqlDbType.Float)
                    };
                parameters[0].Value = preOrder19dian.companyId;
                parameters[1].Value = preOrder19dian.shopId;
                parameters[2].Value = preOrder19dian.orderInJson;
                parameters[3].Value = preOrder19dian.preOrderTime;
                parameters[4].Value = preOrder19dian.menuId;
                parameters[5].Value = preOrder19dian.status;
                parameters[6].Value = preOrder19dian.preOrderSum;
                parameters[7].Value = preOrder19dian.customerId;
                parameters[8].Value = preOrder19dian.customerUUID;
                parameters[9].Value = preOrder19dian.sundryJson;
                parameters[10].Value = preOrder19dian.deskNumber;
                parameters[11].Value = preOrder19dian.appType;
                parameters[12].Value = preOrder19dian.appBuild;
                parameters[13].Value = preOrder19dian.OrderId;
                parameters[14].Value = preOrder19dian.OrderType;
                parameters[15].Value = preOrder19dian.preOrderServerSum.Value;
                parameters[16].Value = preOrder19dian.prePaidSum.Value;
                Object obj = SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql, parameters);
                return obj == null ? 0 : Convert.ToInt64(obj);
            }
        }

        /// <summary>
        /// 新增原路返回申请单信息
        /// </summary>
        /// <param name="originalRoadRefund"></param>
        /// <returns></returns>
        public long InsertOriginalRoadRefund(OriginalRoadRefundInfo originalRoadRefund)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                Object obj = null;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    SqlParameter[] parameters = null;
                    strSql.Append("insert into OriginalRoadRefundInfo(");
                    strSql.Append("type,connId,refundAmount,applicationTime,");
                    strSql.Append("customerMobilephone,status,customerUserName,employeeId,[RefundPayType])");
                    strSql.Append(" values (");
                    strSql.Append("@type,@connId,@refundAmount,@applicationTime,");
                    strSql.Append("@customerMobilephone,@status,@customerUserName,@employeeId,@RefundPayType)");
                    strSql.Append(" select @@identity");
                    parameters = new SqlParameter[]{
                        new SqlParameter("@type",SqlDbType.Int,4),
                        new SqlParameter("@connId",SqlDbType.BigInt,8),
                        new SqlParameter("@refundAmount",SqlDbType.Float),
                        new SqlParameter("@applicationTime",SqlDbType.DateTime),
                        new SqlParameter("@customerMobilephone",SqlDbType.NVarChar,50),
                        new SqlParameter("@status",SqlDbType.Int,4),
                        new SqlParameter("@customerUserName",SqlDbType.NVarChar,100),
                        new SqlParameter("@employeeId",SqlDbType.Int,4),
                        new SqlParameter("@RefundPayType", SqlDbType.Int), 
                    };
                    parameters[0].Value = originalRoadRefund.type;
                    parameters[1].Value = originalRoadRefund.connId;
                    parameters[2].Value = originalRoadRefund.refundAmount;
                    parameters[3].Value = System.DateTime.Now;
                    parameters[4].Value = originalRoadRefund.customerMobilephone;
                    parameters[5].Value = originalRoadRefund.status;
                    parameters[6].Value = originalRoadRefund.customerUserName;
                    parameters[7].Value = originalRoadRefund.employeeId;
                    parameters[8].Value = (int)originalRoadRefund.RefundPayType;
                    obj = SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql.ToString(), parameters);
                }
                catch (Exception exc)
                {
                    LogManager.WriteLog(LogFile.Error, DateTime.Now.ToString() + "msg:" + exc.ToString());
                    return 0;
                }
                if (obj == null)
                {
                    return 0;
                }
                else
                {
                    return Convert.ToInt64(obj);
                }
            }
        }

        /// <summary>
        /// 更新预点单信息
        /// </summary>
        /// <param name="preOrder19dian"></param>
        /// <returns></returns>
        public bool UpdatePreOrder19dian(PreOrder19dianInfo preOrder19dian)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"update PreOrder19dian set status=@status,isPaid=@isPaid,prePaidSum=@prePaidSum,prePayTime=@prePayTime,
 viewallocCommission=@viewallocCommission,transactionFee=@transactionFee,viewallocNeedsToPayToShop=@viewallocNeedsToPayToShop,
preOrderServerSum=@preOrderServerSum,discount=@discount,isShopConfirmed=@isShopConfirmed,
 verifiedSaving=@verifiedSaving,isEvaluation = @isEvaluation,preOrderSum = @preOrderSum");

            List<SqlParameter> paraList = new List<SqlParameter>();
            paraList.Add(new SqlParameter("@status", SqlDbType.SmallInt, 2) { Value = (int)preOrder19dian.status });
            paraList.Add(new SqlParameter("@isPaid", SqlDbType.SmallInt, 2) { Value = (int)preOrder19dian.isPaid });
            paraList.Add(new SqlParameter("@prePaidSum", SqlDbType.Float) { Value = SqlHelper.GetDbNullValue(preOrder19dian.prePaidSum) });
            paraList.Add(new SqlParameter("@prePayTime", SqlDbType.DateTime) { Value = SqlHelper.GetDbNullValue(preOrder19dian.prePayTime) });
            paraList.Add(new SqlParameter("@viewallocCommission", SqlDbType.Float) { Value = SqlHelper.GetDbNullValue(preOrder19dian.viewallocCommission) });
            paraList.Add(new SqlParameter("@transactionFee", SqlDbType.Float) { Value = SqlHelper.GetDbNullValue(preOrder19dian.transactionFee) });
            paraList.Add(new SqlParameter("@viewallocNeedsToPayToShop", SqlDbType.Float) { Value = SqlHelper.GetDbNullValue(preOrder19dian.viewallocNeedsToPayToShop) });
            paraList.Add(new SqlParameter("@preOrderServerSum", SqlDbType.Float) { Value = SqlHelper.GetDbNullValue(preOrder19dian.preOrderServerSum) });
            paraList.Add(new SqlParameter("@preOrderSum", SqlDbType.Float) { Value = preOrder19dian.preOrderSum });
            paraList.Add(new SqlParameter("@discount", SqlDbType.Float) { Value = SqlHelper.GetDbNullValue(preOrder19dian.discount) });
            paraList.Add(new SqlParameter("@verifiedSaving", SqlDbType.Float) { Value = SqlHelper.GetDbNullValue(preOrder19dian.verifiedSaving) });
            paraList.Add(new SqlParameter("@isEvaluation", SqlDbType.SmallInt) { Value = SqlHelper.GetDbNullValue(preOrder19dian.isEvaluation) });
            paraList.Add(new SqlParameter("@isShopConfirmed", SqlDbType.SmallInt) { Value = SqlHelper.GetDbNullValue(preOrder19dian.isShopConfirmed) });
            if (preOrder19dian.expireTime != Convert.ToDateTime("1970-1-1"))
            {
                strSql.Append(",expireTime=@expireTime");
                paraList.Add(new SqlParameter("@expireTime", SqlDbType.DateTime) { Value = preOrder19dian.expireTime });
            }
            strSql.Append(" where preOrder19dianId=@preOrder19dianId");
            paraList.Add(new SqlParameter("@preOrder19dianId", SqlDbType.BigInt, 8) { Value = preOrder19dian.preOrder19dianId });

            SqlParameter[] parameters = paraList.ToArray();

            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                //if (conn.State != ConnectionState.Open)
                //{
                //    conn.Open();
                //}
                int result = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);
                return result > 0;
            }
        }

        /// <summary>
        /// new根据店铺编号增加预支付点单计数（+1）
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public bool UpdateShopPrepayOrderCount(int shopId)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int result = 0;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("update ShopInfo set ");
                    strSql.Append("prepayOrderCount = isnull(prepayOrderCount, 0) + @value");
                    //保证更新后的余额大于零
                    strSql.Append(" where shopID=@shopID");
                    SqlParameter[] parameters = {                   
                    new SqlParameter("@value", SqlDbType.Int,4),
					new SqlParameter("@shopID", SqlDbType.Int,4)};

                    parameters[0].Value = 1;
                    parameters[1].Value = shopId;

                    result = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);
                }
                catch (Exception)
                {
                    return false;
                }
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
        /// new根据店铺编号增加未支付点单计数（+1）
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public bool UpdateShopPreorderCount(int shopId)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int result = 0;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("update ShopInfo set ");
                    strSql.Append("preorderCount = isnull(preorderCount, 0) + @value");
                    //保证更新后的余额大于零
                    strSql.Append(" where shopID=@shopID");
                    SqlParameter[] parameters = {                   
                    new SqlParameter("@value", SqlDbType.Int,4),
					new SqlParameter("@shopID", SqlDbType.Int,4)};

                    parameters[0].Value = 1;
                    parameters[1].Value = shopId;

                    result = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);
                }
                catch (Exception)
                {
                    return false;
                }
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
        /// new根据菜多语言编号增加菜销量计数
        /// </summary>
        /// <param name="dishPriceI18nId"></param>
        /// <returns></returns>
        public bool UpdateDishSalesCount(int dishPriceI18nId, int value)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int result = 0;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("update DishInfo set ");
                    strSql.Append("dishSalesIn19dian = isnull(dishSalesIn19dian, 0) + @value");
                    strSql.Append(" where DishID in (select DishID from DishPriceInfo inner join");
                    strSql.Append(" DishPriceI18n on DishPriceInfo.DishPriceID = DishPriceI18n.DishPriceID");
                    strSql.Append(" where DishPriceI18nID=@DishPriceI18nID)");
                    SqlParameter[] parameters = {                   
                    new SqlParameter("@value", SqlDbType.Int,4),
					new SqlParameter("@DishPriceI18nID", SqlDbType.Int,4)};

                    parameters[0].Value = value;
                    parameters[1].Value = dishPriceI18nId;

                    result = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);
                }
                catch (Exception)
                {
                    return false;
                }
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
        /// 查询POSLite版本号
        /// </summary>
        /// <returns></returns>
        public DataTable SelectPOSLiteVersion(int type = 0)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select [POSLiteVersion],[downloadURL],[POSLiteUpdatePackageName]");
            strSql.AppendFormat(" from VersionInfo where type='{0}'", type);

            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }

        /// <summary>
        /// 根据预点单编号查询预点单信息和对应的公司信息
        /// </summary>
        /// <param name="preorderId"></param>
        /// <returns></returns>
        public DataTable SelectPreOrderAndCompany(long preorderId)
        {
            const string strSql = @"select A.invoiceTitle,A.deskNumber,A.preOrder19dianId,A.orderInJson,A.preOrderTime,A.preOrderSum,B.companyId,
B.companyName,A.status,A.shopId,A.isShopConfirmed,A.prePayTime,isnull(A.discount,1) as discount,
A.isApproved,B.viewallocCommissionType,B.viewallocCommissionValue,B.freeRefundHour,
A.customerUUID,isnull(A.isPaid,0) as isPaid,A.sundryJson,A.refundMoneySum,A.refundMoneyClosedSum,
A.isEvaluation,A.preOrderServerSum,A.customerId,
A.prePaidSum,A.verifiedSaving,C.shopName,C.shopImagePath,C.shopLogo,C.CityID,
C.isSupportAccountsRound,C.preorderGiftDesc,C.preorderGiftTitle,C.preorderGiftValidTime,A.appType,A.appBuild,isnull(C.isHandle,0) isHandle,C.isSupportPayment 
from CompanyInfo as B inner join ShopInfo as C on B.companyId =C.companyId
inner join PreOrder19dian as A on A.shopId = C.shopId  where A.preOrder19dianId = @preorderId and A.status <> 104";
            SqlParameter parameter = new SqlParameter("@preorderId", SqlDbType.BigInt, 8) { Value = preorderId };
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, parameter);
            return ds.Tables[0];
        }

        /// <summary>
        /// 根据预点单编号查询预点单信息和对应的公司信息
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public DataTable GetOrderIdToPreOrderAndCompany(Guid orderId)
        {
            const string strSql = @"select A.invoiceTitle,A.deskNumber,A.preOrder19dianId,A.orderInJson,A.preOrderTime,A.preOrderSum,B.companyId,
B.companyName,o.status,A.shopId,A.isShopConfirmed,A.prePayTime,isnull(A.discount,1) as discount,
A.isApproved,B.viewallocCommissionType,B.viewallocCommissionValue,B.freeRefundHour,
A.customerUUID,isnull(A.isPaid,0) as isPaid,A.sundryJson,A.refundMoneySum,A.refundMoneyClosedSum,
A.isEvaluation,A.preOrderServerSum,o.preOrderServerSum prePaidSum,A.customerId,
A.prePaidSum,A.verifiedSaving,C.shopName,C.shopImagePath,C.shopLogo,C.CityID,
C.isSupportAccountsRound,C.preorderGiftDesc,C.preorderGiftTitle,C.preorderGiftValidTime,A.appType,A.appBuild,isnull(C.isHandle,0) isHandle,C.isSupportPayment,o.PreOrderServerSum 
from CompanyInfo as B inner join ShopInfo as C on B.companyId =C.companyId
inner join PreOrder19dian as A on A.shopId = C.shopId inner join [order] o on a.orderid=o.id
 where A.OrderId = @OrderId AND A.OrderType=1 and A.status <> 104";
            SqlParameter parameter = new SqlParameter("@OrderId", SqlDbType.UniqueIdentifier) { Value = orderId };
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, parameter);
            return ds.Tables[0];
        }

        /// <summary>
        /// 根据预点单编号查询预点单信息信息
        /// </summary>
        /// <param name="preorderId"></param>
        /// <returns></returns>
        public DataTable SelectPreOrderAndCompanyByPayMent(long preorderId)
        {
            //            const string strSql = @"  select A.orderInJson,A.sundryJson,B.viewallocCommissionType,B.viewallocCommissionValue,A.status,
            // B.freeRefundHour,A.preOrder19dianId,A.isPaid,A.preOrderSum,A.deskNumber
            // from CompanyInfo as B inner join PreOrder19dian as A on B.companyID = A.companyId 
            //  where A.preOrder19dianId = @preorderId and A.status <> 104";

            const string strSql = @"select A.orderInJson,A.sundryJson,2 viewallocCommissionType,s.viewallocCommissionValue,A.status,
 B.freeRefundHour,A.preOrder19dianId,A.isPaid,A.preOrderSum,A.deskNumber,A.OrderId,A.OrderType
 FROM CompanyInfo as B inner join PreOrder19dian as A on B.companyID = A.companyId 
 inner join ShopInfo s on B.companyID = s.companyID and A.shopId=s.shopID
 where A.preOrder19dianId = @preorderId and A.status <> 104";

            SqlParameter parameter = new SqlParameter("@preorderId", SqlDbType.BigInt, 8) { Value = preorderId };
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, parameter);
            return ds.Tables[0];
        }

        public DataTable SelectEvaluation(long preorderId)
        {
            const string strSql = @"select p.shopId,p.prePayTime,p.prePaidSum,s.shopName
  from PreOrder19dian p inner join ShopInfo s on p.shopId=s.shopID and p.preOrder19dianId=@preorderId";
            SqlParameter parameter = new SqlParameter("@preorderId", SqlDbType.BigInt) { Value = preorderId };
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, parameter);
            return ds.Tables[0];
        }

        /// <summary>
        /// 根据预点单编号查询预点单部分信息
        /// </summary>
        /// <param name="preorderId"></param>
        /// <returns></returns>
        public DataTable SelectPartPreOrderInfo(long preorderId)
        {
            const string strSql = @"select A.preOrder19dianId,A.orderInJson,isnull(A.isPaid,0) as isPaid,A.sundryJson, A.status,A.OrderId
 from PreOrder19dian as A  where A.preOrder19dianId = @preorderId and A.status <> 104";
            SqlParameter parameter = new SqlParameter("@preorderId", SqlDbType.BigInt, 8) { Value = preorderId };
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameter);
            return ds.Tables[0];
        }

        /// <summary>
        /// 根据预点单编号查询预点单部分信息
        /// </summary>
        /// <param name="preorderId"></param>
        /// <returns></returns>
        public DataTable SybSelectPreOrder(Guid orderId)
        {
            //            const string strSql = @"select A.preOrder19dianId,A.discount,A.[orderInJson],A.[sundryJson],A.[preOrderSum],A.[prePaidSum],round(A.[preOrderSum]-A.[verifiedSaving],2) [afterDiscountAmount],A.[isPaid],A.[prePayTime],A.refundMoneySum,A.invoiceTitle,A.verifiedSaving
            //                                               from PreOrder19dian A where A.preOrder19dianId =@preorderId";
            const string strSql = @"select A.[preOrderServerSum] preOrderSum,A.[prePaidSum],
                                        round(A.[preOrderServerSum]-A.[verifiedSaving],2) [afterDiscountAmount],
                                        A.[isPaid],A.[prePayTime],A.refundMoneySum,A.invoiceTitle,A.verifiedSaving
                                        from [Order] A 
                                        where A.id =@orderID";
            SqlParameter para = new SqlParameter("@orderID", SqlDbType.UniqueIdentifier) { Value = orderId };
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, para);
            return ds.Tables[0];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="isHavaMore"></param>
        /// <returns></returns>
        public DataTable SelectPagingPreOrderList(long customerID, int pageIndex, int pageSize, ref bool isHavaMore)
        {
            const string queryCountStr = @"select count(A.preOrder19dianId) OrderCount
from PreOrder19dian as A where A.customerId = @customerId  and A.status <> 104";
            SqlParameter[] parameter = new[]
            {
                new SqlParameter("@customerId",customerID)
            };
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, queryCountStr, parameter))
            {
                if (dr.Read())
                {
                    isHavaMore = Convert.ToInt32(SqlHelper.ConvertDbNullValue(dr[0])) > pageIndex * pageSize;
                }
            }

            const string sqlStr = @"select * from 
                                     (select  ROW_NUMBER() OVER(Order by A.preOrderTime DESC ) AS RowNumber ,
A.preOrder19dianId,A.orderInJson,A.preOrderTime,A.preOrderSum,A.isEvaluation,
A.isShopConfirmed,A.prePayTime,A.status,A.shopId,isnull(A.isPaid,0) 
as isPaid,A.sundryJson,A.refundMoneySum,A.refundMoneyClosedSum,
A.preOrderServerSum,A.customerId,A.prePaidSum,
A.verifiedSaving ,C.shopName,
A.invoiceTitle,A.discount,A.deskNumber,C.isSupportAccountsRound 
,C.shopImagePath,C.shopLogo,isnull(C.isHandle,0) isHandle,C.CityID
                                    from CompanyInfo as B 
                                    inner join PreOrder19dian as A on B.companyID = A.companyId  
                                    inner join ShopInfo as C on C.shopID =A.shopId 
                                    where A.customerId = @customerId  and A.status <> 104) as T 
                                    where RowNumber between @rowStar and @rowEnd ";
            parameter = new[]
            {
                new SqlParameter("@customerId",customerID),
                new SqlParameter("@rowStar",(pageIndex-1)*pageSize+1),
                new SqlParameter("@rowEnd",pageIndex*pageSize),
            };
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sqlStr, parameter);
            return ds.Tables[0];
        }

        /// <summary>
        /// 根据编号，查询预点单详情信息
        /// </summary>
        /// <param name="preOrder19dianId"></param>
        /// <returns></returns>
        public DataTable SelectPreOrderById(long preOrder19dianId)
        {
            const string strSql = @"select top 10 a.OrderId,a.[preOrder19dianId],[menuId],a.[shopId],sundryJson,discount,companyId,
                                    a.[customerId],[orderInJson],[customerUUID],a.[status],
                                    [preOrderSum],a.[preOrderServerSum],a.[preOrderTime],a.[isPaid],a.[prePaidSum],
                                    a.[prePayTime],[viewallocCommission],[transactionFee],[viewallocNeedsToPayToShop],
                                    [viewallocPaidToShopSum],[viewallocTransactionCompleted],isnull([isApproved],0) isApproved,isnull(a.[isShopConfirmed],0) isShopConfirmed,
                                    a.invoiceTitle,a.[refundMoneySum],d.RefundMoneySum RefundMoneySum1,mobilePhoneNumber,
                                    CustomerInfo.UserName,a.refundMoneyClosedSum,preOrderTotalAmount,preOrderTotalQuantity,currentPlatformVipGrade,appBuild,OrderId 
                                    from PreOrder19dian a
                                    inner join CustomerInfo on a.customerId=CustomerInfo.CustomerID 
                                    inner join VAGastronomistMobileApp0.dbo.Orders d on a.OrderId=d.Id
                                    where a.preOrder19dianId=@preOrder19dianId";
            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@preOrder19dianId",SqlDbType.BigInt) { Value = preOrder19dianId }
            };
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, para);
            return ds.Tables[0];
        }

        /// <summary>
        /// 根据orderId，查询预点单详情信息
        /// </summary>
        /// <param name="preOrder19dianId"></param>
        /// <returns></returns>
        public DataTable SelectPreOrderByOrderId(Guid orderId)
        {
            const string strSql = @"select [preOrder19dianId],[menuId],[shopId],sundryJson,discount,companyId,
                                        PreOrder19dian.[customerId],[orderInJson],[customerUUID],[status],
                                        [preOrderSum],[preOrderServerSum],[preOrderTime],[isPaid],[prePaidSum],
                                        [prePayTime],[viewallocCommission],[transactionFee],[viewallocNeedsToPayToShop],
                                        [viewallocPaidToShopSum],[viewallocTransactionCompleted],isnull([isApproved],0) isApproved,isnull([isShopConfirmed],0) isShopConfirmed,
                                        invoiceTitle,[refundMoneySum],b.mobilePhoneNumber,
                                        invoiceTitle,[refundMoneySum],mobilePhoneNumber,
                                        CustomerInfo.UserName,refundMoneyClosedSum,preOrderTotalAmount,preOrderTotalQuantity,currentPlatformVipGrade,appBuild from PreOrder19dian
                                        inner join CustomerInfo on PreOrder19dian.customerId=CustomerInfo.CustomerID where preOrder19dianId=@preOrder19dianId";
            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@orderId",SqlDbType.UniqueIdentifier) { Value = orderId }
            };
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, para);
            return ds.Tables[0];
        }

        /// <summary>
        /// 查询订单主表数据
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        public DataTable SelectOrderById(Guid orderID)
        {
            const string strSql = @"SELECT top 1 p.[preOrder19dianId],
                                           p.[menuId],
                                           o.[shopId],
                                           p.sundryjson,
                                           p.discount,
                                           p.companyid,
                                           p.[customerId],
                                           p.[orderInJson],
                                           p.[customerUUID],
                                           o.[status],
                                           o.[preOrderSum],
                                           p.[preOrderServerSum],
                                           o.[preOrderTime],
                                           o.[isPaid],
                                           o.[prePaidSum],
                                           o.[prePayTime],
                                           p.[viewallocCommission],
                                           p.[transactionFee],
                                           p.[viewallocNeedsToPayToShop],
                                           p.[viewallocPaidToShopSum],
                                           p.[viewallocTransactionCompleted],
                                           Isnull([isApproved],0) isapproved,
                                           Isnull(p.[isShopConfirmed],0) isshopconfirmed,
                                           o.invoicetitle,
                                           o.[refundMoneySum],
                                           c.mobilephonenumber,
                                           c.username,
                                           o.refundmoneyclosedsum,
                                           c.preordertotalamount,
                                           c.preordertotalquantity,
                                           c.currentplatformvipgrade,
                                           p.appbuild
                                    FROM   preorder19dian p
                                           INNER JOIN [Order] o on o.Id=p.orderId 
                                           INNER JOIN customerinfo c
                                             ON p.customerid = c.customerid
                                    wHERE  o.id=1 and p.orderType=@orderId";

            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@orderId",SqlDbType.UniqueIdentifier) { Value = orderID }
            };
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, para);
            return ds.Tables[0];
        }

        /*************************************
         * Added By 林东宇
         * For 获取点单详情
         * *************************************/
        /// <summary>
        /// 依据点单ID获取点单详情
        /// </summary>
        /// <param name="preOrder19dianId"></param>
        /// <returns></returns>
        public PreOrder19dianInfo GetPreOrderById(long preOrder19dianId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" SELECT ");
            strSql.Append(" [preOrder19dianId],");
            strSql.Append(" [menuId],");
            strSql.Append(" [companyId],");
            strSql.Append(" [shopId],");
            strSql.Append(" [customerId],");
            strSql.Append(" [orderInJson],");
            strSql.Append(" [customerUUID],");
            strSql.Append(" [status],");
            strSql.Append(" [preOrderSum],");
            strSql.Append(" [preOrderServerSum],");
            strSql.Append(" [preOrderTime],");
            strSql.Append(" [isPaid],");
            strSql.Append(" [prePaidsum],");
            strSql.Append(" [prePayTime],");
            strSql.Append(" [viewallocCommission],");
            strSql.Append(" [transactionFee],");
            strSql.Append(" [viewallocNeedsToPayToShop],");
            strSql.Append(" [viewallocPaidToShopSum],");
            strSql.Append(" [viewallocTransactionCompleted],");
            strSql.Append(" [isApproved],");
            strSql.Append(" [verifiedsaving],");
            strSql.Append(" [isShopConfirmed],");
            strSql.Append(" [invoiceTitle],");
            strSql.Append(" [sundryJson],");
            strSql.Append(" [refundMoneySum],");
            strSql.Append(" [discount],");
            strSql.Append(" [refundMoneyClosedSum],");
            strSql.Append(" [deskNumber],");
            strSql.Append(" [refundRedEnvelope],");
            strSql.Append(" [appType],");
            strSql.Append(" [appBuild],");
            strSql.Append(" [orderId]");
            strSql.Append("  FROM PreOrder19dian WHERE  [preOrder19dianId]=@preOrder19dianId");
            SqlParameter[] parameters = new SqlParameter[]
            {
               new SqlParameter("@preOrder19dianId",preOrder19dianId)
            };
            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters))
            {
                if (reader.Read())
                {
                    return reader.GetEntity<PreOrder19dianInfo>();
                }
            }
            return null;
        }

        /// <summary>
        /// 查询当前公司门店所有未对账的
        /// </summary>
        /// <param name="preOrderId"></param>
        /// <returns></returns>
        public DataTable SelectPreOrder19dianId(int shopId)
        {
            //const string strSql = "select [preOrder19dianId],shopId from PreOrder19dian  where shopId=@shopId and isShopConfirmed = 1 and ISNULL(isApproved,0)=0";//eCardNumber,verificationCode
            const string strSql = "select [orderId],PreOrder19dianId,shopId from PreOrder19dian  where shopId=@shopId and isShopConfirmed = 1 and ISNULL(isApproved,0)=0 and orderType=1";//找出正常点单
            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@shopId", SqlDbType.Int){ Value = shopId }
            };
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, para);
            return ds.Tables[0];
        }

        /// <summary>
        /// 查询当前所有未对账的订单的店铺ID
        /// </summary>
        /// <returns></returns>
        public DataTable SelectShopIdNotVerified()
        {
            const string strSql = "select distinct shopId from PreOrder19dian where OrderType=1 and isShopConfirmed = 1 and ISNULL(isApproved,0)=0";
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql);
            return ds.Tables[0];
        }

        /// <summary>
        /// 在PreOrderCheckInfo中添加一条记录
        /// </summary>
        /// <param name="preOrder19dianId"></param>
        /// <param name="approveTag">审核还是撤销审核</param>
        /// <param name="employeeId">审核人</param>
        /// <param name="employeeName">审核人姓名</param>
        /// <param name="employeePosition">审核人职位</param>
        /// <returns></returns>
        public bool InsertPreOrderCheckInfo(long preOrder19dianId, int approveTag, int employeeId, string employeeName, string employeePosition)
        {

            SqlParameter[] param ={
                                      new SqlParameter("@preOrder19dianId",preOrder19dianId),
                                      new SqlParameter("@employeeId",employeeId),
                                      new SqlParameter("@employeeName",employeeName),
                                      new SqlParameter("@employeePosition",employeePosition),
                                      new SqlParameter("@checkTime",DateTime.Now),
                                      new SqlParameter("@status",approveTag)
                                  };
            const string strSql = @"insert into PreOrderCheckInfo (preOrder19dianId,employeeId,employeeName,employeePosition,checkTime,status)
 values (@preOrder19dianId,@employeeId,@employeeName,@employeePosition,@checkTime,@status)";

            int resultInsetPreOrderCheckInfo = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, param);
            if (resultInsetPreOrderCheckInfo > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 2013-7-26 wangcheng在PreorderShopConfirmedInfo中添加一条记录
        /// </summary>
        /// <param name="preOrder19dianId"></param>
        /// <param name="approveTag">审核还是撤销审核</param>
        /// <param name="employeeId">审核人</param>
        /// <param name="employeeName">审核人姓名</param>
        /// <param name="employeePosition">审核人职位</param>
        /// <returns></returns>
        public bool InsertPreorderShopConfirmedInfo(long preOrder19dianId, int shopConfirmenStatus, int employeeId, string employeeName, string employeePosition)
        {
            StringBuilder strSql = new StringBuilder();
            SqlParameter[] param ={
                                      new SqlParameter("@preOrder19dianId",preOrder19dianId),
                                      new SqlParameter("@employeeId",employeeId),
                                      new SqlParameter("@employeeName",employeeName),
                                      new SqlParameter("@employeePosition",employeePosition),
                                      new SqlParameter("@shopConfirmedTime",DateTime.Now),
                                      new SqlParameter("@status",shopConfirmenStatus)
                                  };
            strSql.Append("insert into PreorderShopConfirmedInfo (preOrder19dianId,employeeId,employeeName,employeePosition,shopConfirmedTime,status)");
            strSql.Append(" values (@preOrder19dianId,@employeeId,@employeeName,@employeePosition,@shopConfirmedTime,@status);");
            int resultInsetPreOrderCheckInfo = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), param);
            if (resultInsetPreOrderCheckInfo > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 对账预点单
        /// </summary>
        /// <param name="preOrder19dianId"></param>
        /// <param name="approveTag"></param>
        /// <returns></returns>
        public bool ApprovePreOrder19dian(long preOrder19dianId, int approveTag)
        {
            SqlParameter[] param ={
                                      new SqlParameter("@preOrder19dianId",preOrder19dianId),
                                      new SqlParameter("@isApproved",approveTag)
                                  };
            //修改PreOrder19dian
            const string strSql = "update PreOrder19dian set isApproved=@isApproved where preOrder19dianId=@preOrder19dianId";
            int resultApprovePreOrder19dian = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, param);
            if (resultApprovePreOrder19dian > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 根据OrderId对账预点单
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="approveTag"></param>
        /// <returns></returns>
        public bool ApprovePreOrder19dianByOrderId(Guid orderId, int approveTag)
        {
            SqlParameter[] param ={
                                      new SqlParameter("@orderId",orderId),
                                      new SqlParameter("@isApproved",approveTag)
                                  };
            //修改PreOrder19dian
            const string strSql = "update PreOrder19dian set isApproved=@isApproved where orderId=@orderId";
            int resultApprovePreOrder19dian = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, param);
            if (resultApprovePreOrder19dian > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 2013-7-26 wangcheng
        /// 验证预点单，修改预点单中是否审核的字段信息
        /// </summary>
        /// <param name="preOrder19dianId"></param>
        /// <param name="shopConfirmenStatus"></param>
        /// <returns></returns>
        public bool ShopConfirmPreOrder19dian(long preOrder19dianId, int shopConfirmenStatus)
        {
            int status = 0;
            if (shopConfirmenStatus == 1)//审核操作
            {
                status = (int)VAPreorderStatus.Completed;
            }
            else//取消审核
            {
                status = (int)VAPreorderStatus.Prepaid;
            }
            StringBuilder strSql = new StringBuilder();
            SqlParameter[] param ={
                                      new SqlParameter("@preOrder19dianId",preOrder19dianId),
                                      new SqlParameter("@isShopConfirmed",shopConfirmenStatus),
                                      new SqlParameter("@status",status)
                                  };
            //修改PreOrder19dian
            strSql.Append("update PreOrder19dian set isShopConfirmed=@isShopConfirmed,");
            strSql.Append(" status=@status");
            if (shopConfirmenStatus == 0)//取消审核
            {
                strSql.Append(",deskNumber=''");
            }
            strSql.Append("  where preOrder19dianId=@preOrder19dianId");
            int resultApprovePreOrder19dian = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), param);
            if (resultApprovePreOrder19dian > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        ///2013-7-26 wangcheng
        /// 查询某个预点单的审核信息
        /// </summary>
        /// <param name="preOrder19dianId"></param>
        /// <returns></returns>
        public DataTable SelectPreorderShopConfirmedInfo(long preOrder19dianId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("  select [id],[preOrder19dianId],[employeeId],[employeeName],[employeePosition],[shopConfirmedTime],[status] from PreorderShopConfirmedInfo");
            strSql.Append(" where preOrder19dianId=@preOrder19dianId");
            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@preOrder19dianId", SqlDbType.BigInt ) {Value = preOrder19dianId}
            };
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), para);
            return ds.Tables[0];
        }

        /// <summary>
        /// 查询某个预点单的最新审核信息
        /// </summary>
        /// <param name="preOrder19dianId"></param>
        /// <returns></returns>
        public DataTable SelectNewPreorderShopConfirmedInfo(long preOrder19dianId)
        {
            const string strSql = @"select top 1 [id],[preOrder19dianId],[employeeId],[employeeName],[employeePosition],[shopConfirmedTime],[status] from PreorderShopConfirmedInfo
 where preOrder19dianId=@preOrder19dianId and status =1 order by shopConfirmedTime desc";
            SqlParameter[] parameter = new SqlParameter[] { new SqlParameter("@preOrder19dianId", preOrder19dianId) };
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameter);
            return ds.Tables[0];
        }

        /// <summary>
        ///20140227 wangcheng
        /// 查询某个预点单的取消审核信息
        /// </summary>
        /// <param name="preOrder19dianId"></param>
        /// <returns></returns>
        public DataTable SelectPreorderShopCancleConfirmedInfo(long preOrder19dianId)
        {
            const string strSql = @"select [id],[preOrder19dianId],[employeeId],[employeeName],[employeePosition],[shopConfirmedTime],[status] from PreorderShopConfirmedInfo 
                                        where preOrder19dianId=@preOrder19dianId and status =0 order by shopConfirmedTime desc";
            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@preOrder19dianId", SqlDbType.BigInt) { Value = preOrder19dianId }
            };
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, para);
            return ds.Tables[0];
        }

        /// <summary>
        /// 查询某个预点单的对账信息
        /// </summary>
        /// <param name="preOrder19dianId"></param>
        /// <returns></returns>
        public DataTable SelectPreOrderCheckInfo(long preOrder19dianId)
        {
            string strSql = String.Format(@"select [id],[preOrder19dianId],[employeeId],[employeeName],[employeePosition],[checkTime],[status] from PreOrderCheckInfo
 where preOrder19dianId= @preOrder19dianId");
            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@preOrder19dianId", SqlDbType.BigInt) { Value = preOrder19dianId }
            };
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), para);
            return ds.Tables[0];
        }

        /// <summary>
        /// （wangcheng）查询菜的标记信息
        /// </summary>
        /// <returns></returns>
        public DataTable SelectMarkNameByDishPriceI18nId(string stringSql)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select A.dishPriceI18nId, isnull(A.markName,'') markName,A.ScaleName,C.DishName,B.DishPrice from DishPriceI18n as A");
            strSql.Append(" inner join DishPriceInfo as B on A.DishPriceID=B.DishPriceID");
            strSql.Append(" inner join DishI18n as C on C.DishID=B.DishID");
            strSql.AppendFormat(" where A.dishPriceI18nId in {0} and A.DishPriceI18nStatus=1 and B.DishPriceStatus=1 and C.DishI18nStatus=1", stringSql);
            //strSql.Append(" and markName is not null and markName!=''");//过滤去掉markName为null或者为空的情况
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }

        /// <summary>
        /// new根据预点单编号修改该预点单OrderJson,Sundry信息
        /// </summary>
        /// <param name="preOrder19DianId"></param>
        /// <param name="orderJson"></param>
        /// <param name="sundryJson"></param>
        /// <param name="preOrderSum"></param>
        /// <returns></returns>
        public bool UpdatePreOrderAndSundryJson(long preOrder19DianId, string orderJson, string sundryJson, double preOrderSum)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                const string strSql = @"update PreOrder19dian set orderInJson=@orderJson,sundryJson=@sundryJson,preOrderSum=@preOrderSum where preOrder19dianId=@preOrder19dianId";
                SqlParameter[] parameters = {                   
                    new SqlParameter("@orderJson", SqlDbType.NVarChar),
                    new SqlParameter("@sundryJson", SqlDbType.NVarChar),
                    new SqlParameter("@preOrderSum", SqlDbType.Float),
					new SqlParameter("@preOrder19dianId", SqlDbType.BigInt,8)};

                parameters[0].Value = orderJson;
                parameters[1].Value = sundryJson;
                parameters[2].Value = preOrderSum;
                parameters[3].Value = preOrder19DianId;

                return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters) > 0;
            }
        }

        /// <summary>
        /// 修改点单桌号信息  add by wangc 20140319
        /// </summary>
        /// <param name="preOrder19dianId"></param>
        /// <param name="deskNumber"></param>
        /// <returns></returns>
        public bool UpdateDeskNumber(long preOrder19dianId, string deskNumber)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                const string strSql = @"update PreOrder19dian set deskNumber=@deskNumber where preOrder19dianId=@preOrder19dianId";
                SqlParameter[] parameters = {                   
                    new SqlParameter("@deskNumber", SqlDbType.NVarChar,50),
					new SqlParameter("@preOrder19dianId", SqlDbType.BigInt,8)};
                parameters[0].Value = string.IsNullOrEmpty(deskNumber) ? "" : deskNumber;
                parameters[1].Value = preOrder19dianId;
                return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql, parameters) == 1;
            }
        }

        /// <summary>
        /// new根据预点单编号删除相应信息
        /// </summary>
        /// <param name="preOrder19dianIds"></param>
        /// <param name="str"></param>
        /// <returns></returns>
        public bool DeletePreOrder19dian(List<long> preOrder19dianIds, string str)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                string strSql = String.Format(@"update PreOrder19dian set status=@status where preOrder19dianId in {0} and status != {1}", str, (int)VAPreorderStatus.Prepaid);
                SqlParameter[] parameters = { new SqlParameter("@status", SqlDbType.SmallInt, 2) };
                parameters[0].Value = VAPreorderStatus.Deleted;
                int result = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);
                return result > 0;
            }
        }

        /// <summary>
        /// 修改预点单发票抬头（wangcheng 2013/9/15）
        /// </summary>
        /// <param name="preOrder19dianId"></param>
        /// <param name="invoiceTitle"></param>
        /// <returns></returns>
        public bool UpdatePreOrder19dianInvoiceTitle(long preOrder19dianId, string invoiceTitle)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                const string strSql = @"update PreOrder19dian set invoiceTitle=@invoiceTitle where preOrder19dianId=@preOrder19dianId";
                SqlParameter[] parameters = {
                    new SqlParameter("@invoiceTitle", SqlDbType.NVarChar,50),
					new SqlParameter("@preOrder19dianId", SqlDbType.BigInt,8)};
                parameters[0].Value = invoiceTitle;
                parameters[1].Value = preOrder19dianId;
                int result = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);
                return result == 1;
            }
        }

        /// <summary>
        /// 退款修改预点单状态信息
        /// </summary>
        /// <param name="preOrder19dianId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public bool UpdatePreOrderRefundInfo(long preOrder19dianId, VAPreorderStatus status, double refundMoney)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int result = 0;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("update PreOrder19dian set ");
                    strSql.AppendFormat(" status = {0},", (int)status);
                    strSql.Append(" refundMoneySum=@refundMoneySum+isnull(refundMoneySum,0)");
                    strSql.Append(" where preOrder19dianId=@preOrder19dianId ");
                    SqlParameter[] parameters = {
                    new SqlParameter("@refundMoneySum", SqlDbType.Float),
					new SqlParameter("@preOrder19dianId", SqlDbType.BigInt,8)};
                    parameters[0].Value = refundMoney;
                    parameters[1].Value = preOrder19dianId;
                    result = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);
                }
                catch (Exception)
                {
                    return false;
                }
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
        /// 退款修改预点单退款金额
        /// </summary>
        /// <param name="preOrder19dianId">点单流水号</param>
        /// <param name="refundMoneySum">退款金额</param>
        /// <returns></returns>
        public bool UpdatePreOrderRefundMoneySum(long preOrder19dianId, double refundMoneySum)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int result = 0;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("update PreOrder19dian set ");
                    strSql.Append(" refundMoneySum=@refundMoneySum+isnull(refundMoneySum,0)");
                    strSql.Append(" where preOrder19dianId=@preOrder19dianId ");
                    SqlParameter[] parameters = {
                                                    new SqlParameter("@refundMoneySum", SqlDbType.Float),
					                               new SqlParameter("@preOrder19dianId", SqlDbType.BigInt,8)};
                    parameters[0].Value = refundMoneySum;
                    parameters[1].Value = preOrder19dianId;
                    result = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);
                }
                catch (Exception)
                {
                    return false;
                }
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
        /// 修改预点单已打款的退款金额
        /// </summary>
        /// <param name="preOrder19dianId">点单流水号</param>
        /// <param name="refundMoneyClosed">已打款金额</param>
        /// <returns></returns>
        public bool UpdatePreOrderRefundMoneyClosedSum(long preOrder19dianId, double refundMoneyClosed)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int result = 0;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("update PreOrder19dian set ");
                    strSql.Append(" refundMoneyClosedSum=@refundMoneyClosedSum+isnull(refundMoneyClosedSum,0)");
                    strSql.Append(" where preOrder19dianId=@preOrder19dianId ");
                    SqlParameter[] parameters = {
                                                    new SqlParameter("@refundMoneyClosedSum", SqlDbType.Float),
					                               new SqlParameter("@preOrder19dianId", SqlDbType.BigInt,8)};
                    parameters[0].Value = refundMoneyClosed;
                    parameters[1].Value = preOrder19dianId;
                    result = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);
                }
                catch (Exception)
                {
                    return false;
                }
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
        /// 更新点单的申请退款金额及已完成打款的金额
        /// </summary>
        /// <param name="preOrder19dianId"></param>
        /// <param name="status"></param>
        /// <param name="refundMoney"></param>
        /// <param name="refundMoneyClosed"></param>
        /// <returns></returns>
        public bool UpdatePreOrderRefundMoney(long preOrder19dianId, VAPreorderStatus status, double refundMoney, double refundMoneyClosed)
        {
            string strSql = string.Format(@"update PreOrder19dian set status ={0},
refundMoneySum=@refundMoneySum+isnull(refundMoneySum,0),
refundMoneyClosedSum=@refundMoneyClosedSum+isnull(refundMoneyClosedSum,0)
 where preOrder19dianId=@preOrder19dianId", (int)status);
            SqlParameter[] para = {
                    new SqlParameter("@refundMoneySum", SqlDbType.Float) { Value = refundMoney },
                    new SqlParameter("@refundMoneyClosedSum", SqlDbType.Float) { Value = refundMoneyClosed },
					new SqlParameter("@preOrder19dianId", SqlDbType.BigInt,8) { Value = preOrder19dianId}};
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
        /// 更新Order表退款金额
        /// </summary>
        /// <param name="orderID">订单ID</param>
        /// <param name="status">退款状态</param>
        /// <param name="refundMoney">退款金额</param>
        /// <param name="refundMoneyClosed">已退款金额</param>
        /// <returns></returns>
        public bool UpdateOrderRefundMoney(Guid orderID, VAPreorderStatus status, double refundMoney, double refundMoneyClosed)
        {
            string strSql = string.Format(@"update [Order] set status ={0},
refundMoneySum=@refundMoneySum+isnull(refundMoneySum,0),
refundMoneyClosedSum=@refundMoneyClosedSum+isnull(refundMoneyClosedSum,0)
 where Id=@orderID", (int)status);
            SqlParameter[] para = {
                    new SqlParameter("@refundMoneySum", SqlDbType.Float) { Value = refundMoney },
                    new SqlParameter("@refundMoneyClosedSum", SqlDbType.Float) { Value = refundMoneyClosed },
					new SqlParameter("@orderID", SqlDbType.UniqueIdentifier) { Value = orderID}};
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
        /// 发生红包退款（作废）时，更新点单已退红包金额，已打款退款金额（为了让refundMoneyClosedSum=refundMoneySum）
        /// </summary>
        /// <param name="preOrder19dianId">点单Id</param>
        /// <param name="refundRedEnvelope">已退红包金额</param>
        /// <param name="refundMoneyClosedSum">已打款退款金额</param>
        /// <returns></returns>
        public bool UpdatePreOrderRefundRedEnvelope(long preOrder19dianId, double refundRedEnvelope, double refundMoneyClosedSum)
        {
            int result = 0;
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update PreOrder19dian set ");
                strSql.Append(" refundRedEnvelope=@refundRedEnvelope+isnull(refundRedEnvelope,0),refundMoneyClosedSum=@refundMoneyClosedSum+isnull(refundMoneyClosedSum,0)");
                strSql.Append(" where preOrder19dianId=@preOrder19dianId ");
                SqlParameter[] parameters = {
                    new SqlParameter("@refundRedEnvelope", SqlDbType.Float),
                    new SqlParameter("@refundMoneyClosedSum", SqlDbType.Float),
					new SqlParameter("@preOrder19dianId", SqlDbType.BigInt,8)};
                parameters[0].Value = refundRedEnvelope;
                parameters[1].Value = refundMoneyClosedSum;
                parameters[2].Value = preOrder19dianId;

                using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
                {
                    result = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);
                }
                if (result == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        #region 发票抬头操作
        /// <summary>
        /// 查询当前用户常用发票抬头信息
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public DataTable SelectInvoiceInfo(int customerId)
        {
            const string strSql = @"select invoiceId,invoiceTitle from InvoiceInfo where customerId=@customerId";
            SqlParameter[] parameter = new SqlParameter[] { new SqlParameter("@customerId", customerId) };
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameter);
            return ds.Tables[0];
        }
        public DataTable SelectInvoiceInfo(int customerId, string invoiceTitle)
        {
            const string strSql = @"select invoiceId from InvoiceInfo where customerId=@customerId and invoiceTitle=@invoiceTitle";
            SqlParameter[] parameter = new SqlParameter[] { 
            new SqlParameter("@customerId",customerId),
            new SqlParameter("@invoiceTitle",invoiceTitle)};
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameter);
            return ds.Tables[0];
        }
        /// <summary>
        /// 删除指定发票抬头信息
        /// </summary>
        /// <param name="invoiceId"></param>
        public bool DeleteInvoiceInfo(long invoiceId)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                const string strSql = "delete from InvoiceInfo where invoiceId=@invoiceId;";
                SqlParameter[] parameters = { new SqlParameter("@invoiceId", SqlDbType.BigInt, 8) };
                parameters[0].Value = invoiceId;
                int result = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);
                return result > 0;
            }
        }
        /// <summary>
        /// 添加常用发票抬头信息
        /// </summary>
        /// <param name="invoiceTitle"></param>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public bool InsertInvoiceInfo(string invoiceTitle, int customerId)
        {
            const string strSql = @"insert into InvoiceInfo (invoiceTitle,customerId) values (@invoiceTitle,@customerId)";
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                SqlParameter[] param ={
                                      new SqlParameter("@invoiceTitle",SqlDbType.NVarChar,50){Value=invoiceTitle},
                                      new SqlParameter("@customerId",SqlDbType.Int,4){Value=customerId}
                                  };
                int result = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), param);
                return result > 0;
            }
        }
        #endregion

        /// <summary>
        /// 查询用户最新的未评价的点单信息
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public long SelectLatestPreorderNotEvaluated(long customerId)
        {
            const string strSql = @"select top 1 preOrder19dianId from PreOrder19dian
 where customerId=@customerId and IsEvaluation  = 0 and isShopConfirmed = 1 and status != 104
 and (isnull(refundMoneyClosedSum,'')='' or refundMoneyClosedSum != prePaidSum) order by prePayTime desc";
            SqlParameter[] parameter = new SqlParameter[] { new SqlParameter("@customerId", customerId) };
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameter))
            {
                if (dr.Read())
                {
                    return dr[0] == DBNull.Value ? 0 : Convert.ToInt64(dr[0]);
                }
            }
            return 0;
        }

        /// <summary>
        /// 查询点单使用第三方支付时支付的金额
        /// </summary>
        /// <param name="preorderId"></param>
        /// <returns></returns>
        public ThirdPartyPaymentInfo SelectPreorderPayAmount(long preorderId)
        {
            ThirdPartyPaymentInfo tppi = new ThirdPartyPaymentInfo() { Type = PayType.其他 };
            SqlParameter parameter = null;
            DataSet ds = null;
            try
            {
                string strSql = @" SELECT [totalFee],[alipayOrderId] FROM [AlipayOrderInfo] where connId =@connId and orderStatus = 2";
                parameter = new SqlParameter("@connId", SqlDbType.BigInt, 8) { Value = preorderId };
                ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, parameter);
                if (ds.Tables[0].Rows.Count == 1)
                {
                    tppi.Amount = Convert.ToDouble(ds.Tables[0].Rows[0]["totalFee"]);
                    tppi.Type = PayType.支付宝;
                    tppi.tradeNo = ds.Tables[0].Rows[0]["alipayOrderId"].ToString();
                }
                else
                {
                    string strSql2 = @" SELECT [totalFee],[outTradeNo] FROM [WechatPayOrderInfo] where connId =@connId and orderStatus = 2";
                    ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql2, parameter);
                    if (ds.Tables[0].Rows.Count == 1)
                    {
                        tppi.Amount = Convert.ToDouble(ds.Tables[0].Rows[0]["totalFee"]);
                        tppi.Type = PayType.微信支付;
                        tppi.tradeNo = ds.Tables[0].Rows[0]["outTradeNo"].ToString();
                    }
                    else
                    {
                        string strSql3 = @" SELECT [merchantOrderAmt],[UnionPayOrderInfoID] FROM [UnionPayOrderInfo] where connId =@connId and orderStatus = 2";
                        ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql3, parameter);
                        if (ds.Tables[0].Rows.Count == 1)
                        {
                            tppi.Amount = Convert.ToDouble(ds.Tables[0].Rows[0]["merchantOrderAmt"]) / 100;//银联存的是分
                            tppi.Type = PayType.银联支付;
                            tppi.tradeNo = ds.Tables[0].Rows[0]["UnionPayOrderInfoID"].ToString();
                        }
                    }
                }
            }
            catch (System.Exception)
            {

            }
            return tppi;
        }

        public ThirdPartyPaymentInfo SelectRepeatPreorderPayAmount(long tradeNo)
        {
            ThirdPartyPaymentInfo tppi = new ThirdPartyPaymentInfo() { Type = PayType.其他 };
            //double amount = 0;
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append(" SELECT [totalFee],[alipayOrderId] FROM [AlipayOrderInfo]");
                strSql.AppendFormat(" where orderStatus = 3 and alipayOrderId ={0}", tradeNo);
                DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
                if (ds.Tables[0].Rows.Count == 1)
                {
                    tppi.Amount = Convert.ToDouble(ds.Tables[0].Rows[0]["totalFee"]);
                    tppi.Type = PayType.支付宝;
                    tppi.tradeNo = ds.Tables[0].Rows[0]["alipayOrderId"].ToString();
                }
                else
                {
                    StringBuilder strSql2 = new StringBuilder();
                    strSql2.Append(" SELECT [totalFee],[outTradeNo] FROM [WechatPayOrderInfo]");
                    strSql2.AppendFormat(" where orderStatus = 3 and outTradeNo ={0}", tradeNo);
                    DataSet ds2 = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql2.ToString());
                    if (ds2.Tables[0].Rows.Count == 1)
                    {
                        tppi.Amount = Convert.ToDouble(ds2.Tables[0].Rows[0]["totalFee"]);
                        tppi.Type = PayType.微信支付;
                        tppi.tradeNo = ds2.Tables[0].Rows[0]["outTradeNo"].ToString();
                    }
                    else
                    {
                        StringBuilder strSql3 = new StringBuilder();
                        strSql3.Append(" SELECT [merchantOrderAmt],[UnionPayOrderInfoID] FROM [UnionPayOrderInfo]");
                        strSql3.AppendFormat(" where orderStatus = 3 and UnionPayOrderInfoID ={1}", tradeNo);
                        DataSet ds3 = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql3.ToString());
                        if (ds3.Tables[0].Rows.Count == 1)
                        {
                            tppi.Amount = Convert.ToDouble(ds3.Tables[0].Rows[0]["merchantOrderAmt"]) / 100;//银联存的是分
                            tppi.Type = PayType.银联支付;
                            tppi.tradeNo = ds3.Tables[0].Rows[0]["merchantOrderAmt"].ToString();
                        }
                    }
                }
            }
            catch (System.Exception)
            {

            }
            return tppi;
        }

        /// <summary>
        /// 根据客户Id找到其所有已经支付的点单
        /// 2014-4-11
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public DataTable SelectPaidOrderByCustomerId(long customerId, long orderId = 0)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select pp.preOrder19dianId,s.shopName,pp.prePaidSum,pp.refundMoneySum,pp.prePayTime,case pp.isShopConfirmed when 1 then '是' else '否' end isShopConfirmed,case pp.isApproved when 1 then '是' else '否' end isApproved,pp.status,");
            strSql.Append(" (select '支付宝' payType");
            strSql.Append(" from PreOrder19dian p inner join AlipayOrderInfo ali on p.preOrder19dianId = ali.connId");
            strSql.Append(" and p.preOrder19dianId = pp.preOrder19dianId  and orderStatus = 2");
            strSql.Append(" union");
            strSql.Append(" select '微信' payType");
            strSql.Append(" from PreOrder19dian p inner join WechatPayOrderInfo wx on p.preOrder19dianId = wx.connId");
            strSql.Append(" and p.preOrder19dianId = pp.preOrder19dianId  and orderStatus = 2");
            strSql.Append(" union");
            strSql.Append(" select '银联' payType");
            strSql.Append(" from PreOrder19dian p inner join UnionPayOrderInfo un on p.preOrder19dianId = un.connId");
            strSql.Append(" and p.preOrder19dianId = pp.preOrder19dianId  and orderStatus = 2) thirdPayType,");
            strSql.Append(" (select ali.totalFee");
            strSql.Append(" from PreOrder19dian p inner join AlipayOrderInfo ali on p.preOrder19dianId = ali.connId");
            strSql.Append(" and p.preOrder19dianId = pp.preOrder19dianId  and orderStatus = 2");
            strSql.Append(" union");
            strSql.Append(" select wx.totalFee");
            strSql.Append(" from PreOrder19dian p inner join WechatPayOrderInfo wx on p.preOrder19dianId = wx.connId");
            strSql.Append(" and p.preOrder19dianId = pp.preOrder19dianId  and orderStatus = 2");
            strSql.Append(" union");
            strSql.Append(" select (un.merchantOrderAmt/100) totalFee");
            strSql.Append(" from PreOrder19dian p inner join UnionPayOrderInfo un on p.preOrder19dianId = un.connId");
            strSql.Append(" and p.preOrder19dianId = pp.preOrder19dianId  and orderStatus = 2) thirdTotalFee");
            strSql.Append(" ,red.consumeRedEnvelopeAmount");
            strSql.Append(" from PreOrder19dian pp inner join ShopInfo s on pp.shopId = s.shopID");
            if (orderId == 0)
            {
                strSql.AppendFormat(" and pp.customerId = '{0}' ", customerId);
            }
            else if (orderId > 0)
            {
                strSql.AppendFormat(" and pp.preOrder19dianId = '{0}' ", orderId);
            }
            strSql.AppendFormat(" and pp.isPaid = '{0}'", (int)VAPreorderIsPaid.PAID);
            //2014-7-31 加入红包抵扣金额
            strSql.Append(" left join");
            strSql.Append(" (select preOrder19dianId,sum(currectUsedAmount) consumeRedEnvelopeAmount");
            strSql.Append(" FROM RedEnvelopeConnPreOrder group by preOrder19dianId) red");
            strSql.Append(" on pp.preOrder19dianId = red.preOrder19dianId");
            strSql.Append(" order by pp.preOrder19dianId desc,pp.shopId");

            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }

        /// <summary>
        /// 返回分页用户订单
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="startIndex"></param>
        /// <param name="endIndex"></param>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public Tuple<int, DataTable> GetPagePaidOrderByCustomerId(long customerId, int startIndex, int endIndex, long orderId = 0)
        {
            string fields = "p.preOrder19dianId,s.shopName,p.prePaidSum,p.refundMoneySum,p.prePayTime,case p.isShopConfirmed WHEN 1 THEN '是' ELSE '否' end isShopConfirmed,case p.isApproved WHEN 1 THEN '是' ELSE '否' end isApproved,p.status";
            string order = "p.preOrder19dianId DESC";
            string where = "p.isPaid=1 AND p.customerId=@customerId";
            if (orderId != 0)
                where += " AND p.preorder19dianid=" + orderId;
            SqlParameter[] parameter = new SqlParameter[] { new SqlParameter("@customerId", customerId) };

            string table = "ShopInfo s INNER JOIN PreOrder19dian p on s.shopID =p.shopId";

            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT ");
            strSql.Append(" * ");
            strSql.Append(" FROM (");
            strSql.Append(" SELECT ");
            strSql.AppendFormat(" {0} ,ROW_NUMBER() OVER(ORDER BY {1}) AS ROW_NUMBER", fields, order);
            strSql.AppendFormat(" FROM {0}", table);
            if (!string.IsNullOrEmpty(where))
                strSql.AppendFormat(" WHERE {0}", where);
            strSql.Append(" ) AS AA");
            strSql.Append(" WHERE ");
            strSql.AppendFormat(" AA.ROW_NUMBER BETWEEN {0} AND {1}", startIndex, endIndex);

            string strCountSql = "SELECT COUNT(0) FROM PreOrder19dian p WHERE " + where;

            int count = (int)SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strCountSql, parameter);

            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameter);

            return new Tuple<int, DataTable>(count, ds.Tables[0]);

            //StringBuilder sql = new StringBuilder();
            //sql.Append("SELECT TOP 10 p.preOrder19dianId,s.shopName,p.prePaidSum,p.refundMoneySum,p.prePayTime,");
            //sql.Append(" case p.isShopConfirmed WHEN 1 THEN '是' ELSE '否' end isShopConfirmed,");
            //sql.Append(" case p.isApproved WHEN 1 THEN '是' ELSE '否' end isApproved,p.status");
            //sql.Append(" FROM ShopInfo s INNER JOIN PreOrder19dian p on s.shopID =p.shopId");
            //sql.Append("WHERE p.isPaid=1 AND p.customerId=@customerId");
            //if (orderId != 0)
            //    sql.Append(" AND p.preorder19dianid=" + orderId);

            //SqlParameter[] parameter = new SqlParameter[] { new SqlParameter("@customerId", customerId) };

        }

        /// <summary>
        /// 悠先服务客户追溯查看某个用户支付点单列表（分页查询）
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public CustomerOrderList ZZBSelectCustomerOrder(long customerId, int pageSize, int pageIndex)
        {
            string sqlWhere = String.Format(" PreOrder19dian.customerId={0} and PreOrder19dian.isPaid=1", customerId);
            PageQuery pageQuery = new PageQuery()
            {
                tableName = " PreOrder19dian  inner join ShopInfo on PreOrder19dian.shopId=ShopInfo.shopID",
                fields = " PreOrder19dian.preOrder19dianId,PreOrder19dian.prePaidSum,ShopInfo.shopName,PreOrder19dian.OrderId",
                orderField = " PreOrder19dian.prePayTime desc",
                sqlWhere = sqlWhere
            };
            Paging paging = new Paging()
            {
                pageIndex = pageIndex,
                pageSize = pageSize,
                recordCount = 0,
                pageCount = 0
            };
            CustomerOrderList data = new CustomerOrderList();
            data.customerOrderList = CommonManager.GetPageData<CustomerOrder>(SqlHelper.ConnectionStringLocalTransaction, pageQuery, paging);//ref paging
            data.page = new PageNav() { pageIndex = pageIndex, pageSize = pageSize, totalCount = paging.recordCount };
            return data;
        }

        /// <summary>
        /// 修改点单使用app版本号和app类型
        /// </summary>
        /// <param name="preOrder19dianId"></param>
        /// <param name="appType"></param>
        /// <param name="appBuild"></param>
        /// <returns></returns>
        public bool UpdatePreorderAppInfo(long preOrder19dianId, int appType, string appBuild)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                const string strSql = "update PreOrder19dian set appType=@appType,appBuild=@appBuild where preOrder19dianId=@preOrder19dianId";
                SqlParameter[] parameters = {
                                                    new SqlParameter("@appType", SqlDbType.Int,4),
                                                    new SqlParameter("@appBuild", SqlDbType.NVarChar,100),
					                               new SqlParameter("@preOrder19dianId", SqlDbType.BigInt,8)};
                parameters[0].Value = appType;
                parameters[1].Value = appBuild;
                parameters[2].Value = preOrder19dianId;
                return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql, parameters) > 0;
            }
        }

        /// <summary>
        /// 查询某个订单最后一次退款时间
        /// </summary>
        /// <param name="preOrder19dianId"></param>
        /// <returns></returns>
        public string GetMaxRefundTime(long preOrder19dianId)
        {
            const string strSql = "select MAX(refundTime) refundTime from RefundLogData where preOrder19dianId=@preOrder19dianId";
            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@preOrder19dianId", SqlDbType.BigInt) { Value = preOrder19dianId }
            };
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                object obj = SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql, para);
                if (obj == null)
                {
                    return "";
                }
                else
                {
                    return obj.ToString();
                }
            }
        }

        /// <summary>
        /// 根据点单Id查询店铺名称
        /// </summary>
        /// <param name="preOrder19dianId"></param>
        /// <returns></returns>
        public string GetShopNameByOrderId(long preOrder19dianId)
        {
            const string strSql = "select s.shopName from ShopInfo s inner join PreOrder19dian p on s.shopID = p.shopId and p.preOrder19dianId=@preOrder19dianId";
            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@preOrder19dianId", SqlDbType.BigInt) { Value = preOrder19dianId }
            };
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                object obj = SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql, para);
                if (obj == null)
                {
                    return "";
                }
                else
                {
                    return obj.ToString();
                }
            }
        }

        /// <summary>
        /// 查询的预点单信息
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public DataTable SelectPreOrderShopVerified(int shopId, DateTime timeFrom, DateTime timeTo, int status, bool flag = false, int companyId = 0, bool allOrPart = false)
        {
            StringBuilder strSql = new StringBuilder();
            SqlParameter[] param ={
                                      new SqlParameter("@shopId",shopId),
                                      new SqlParameter("@timeFrom",timeFrom),
                                      new SqlParameter("@timeTo",timeTo)
                                  };
            strSql.Append("select [preOrder19dianId],[menuId],[companyId],[shopId],");
            strSql.Append(" PreOrder19dian.[customerId],[orderInJson],[customerUUID],[status],");
            strSql.Append(" [preOrderSum],[preOrderServerSum],[preOrderTime],");
            strSql.Append(" [isPaid],[prePaidSum],");
            strSql.Append(" [prePayTime],");
            strSql.Append(" [viewallocCommission],[transactionFee],[viewallocNeedsToPayToShop],");
            strSql.Append(" [viewallocPaidToShopSum],[viewallocTransactionCompleted],");
            strSql.Append(" [isApproved],[isShopConfirmed],");
            strSql.Append(" invoiceTitle,[refundMoneySum],");
            strSql.Append(" mobilePhoneNumber,");
            strSql.Append(" CustomerInfo.UserName from PreOrder19dian ");
            strSql.Append(" inner join CustomerInfo on PreOrder19dian.customerId=CustomerInfo.CustomerID");
            //strSql.Append(" where A.isShopVerified=" + (int)VAPreorderIsShopVerified.SHOPVERIFIED + "");
            strSql.Append(" where 1=1 ");
            if (allOrPart == false)//区别查询全部还是支付或者未支付
            {
                if (flag == true)
                {
                    strSql.Append(" and isPaid=" + (int)VAPreorderIsPaid.PAID + "");//已支付
                }
                else
                {
                    strSql.Append(" and (isPaid=" + (int)VAPreorderIsPaid.NOT_PAID + " or isPaid is null)");//未支付
                }
            }
            if (companyId != 0)
            {
                strSql.AppendFormat(" and companyId={0}", companyId);
            }
            if (status == 2 && shopId != 0)
            {
                strSql.Append("  and shopId=@shopId ");
            }
            strSql.AppendFormat(" and preOrderTime between  @timeFrom and  @timeTo");//过滤点单时间
            //strSql.Append("  group by [preOrder19dianId],isShopConfirmed,[companyId],[shopId],[orderInJson],customerId,");
            // strSql.Append(" [preOrderServerSum],[preOrderTime], [isShopVerified],isPaid,[prePaidSum],[prePayTime],viewallocPaidToShopSum,");
            //strSql.Append(" [verificationCode],isApproved,remoteOrder,invoiceTitle ");
            //strSql.AppendFormat(" having max(queryTime) between  @timeFrom and  @timeTo");//过滤验证时间
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), param);
            return ds.Tables[0];
        }

        /// <summary>
        /// 查询某客户未入座的订单信息
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="mapType">默认百度地图</param>
        /// <returns></returns>
        public List<PreOrder19dianRemindInfoDBModel> SelectUnConfirmPreOrder(string cookie, int mapType = 2)
        {
            string strSql = @" select p.preOrder19dianId,s.shopName,p.prePaidSum,p.prePayTime,c.longitude,c.latitude,preOrderServerSum,verifiedSaving
        from PreOrder19dian p 
        inner join customerInfo cus on p.customerId = cus.customerId
        inner join ShopInfo s on p.shopId=s.shopID
        inner join ShopCoordinate c on s.shopID =c.shopId 
        where cus.cookie=@cookie and p.isPaid=1 and ISNULL(p.refundMoneySum,0)=0
        and ISNULL(p.isShopConfirmed,0)=0 and c.mapId=" + mapType + "";
            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@cookie", SqlDbType.NVarChar, 100) { Value = cookie }
            };
            List<PreOrder19dianRemindInfoDBModel> preOrder19dianRemindInfoList = new List<PreOrder19dianRemindInfoDBModel>();
            using (SqlDataReader sdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, para))
            {
                while (sdr.Read())
                {
                    preOrder19dianRemindInfoList.Add(SqlHelper.GetEntity<PreOrder19dianRemindInfoDBModel>(sdr));
                }
            }
            return preOrder19dianRemindInfoList;
        }


        #region -------------------------------------------------------------------
        /// <summary>
        /// 取补差价
        /// </summary>
        /// <param name="orderId">定单id</param>
        /// <returns></returns>
        public decimal GetCompensatePrice(Guid orderId)
        {
            string sql = "SELECT PayDifferenceSum FROM [Order] WHERE  Id=@OrderId";
            SqlParameter[] parameter = new SqlParameter[] { new SqlParameter("@OrderId", SqlDbType.UniqueIdentifier) { Value = orderId } };
            var CountprePaidSum = SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sql, parameter);
            return Convert.ToDecimal(CountprePaidSum);
        }

        /// <summary>
        /// 根据订单ID查询PreOrder19dian表的数据 add by zhujinlei 2015/06/26
        /// </summary>
        /// <param name="orderId">订单ID</param>
        /// <returns></returns>
        public List<PreOrder19dianInfo> GetPreOrder19dianByOrderId(Guid orderId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" SELECT ");
            strSql.Append(" [preOrder19dianId],");
            strSql.Append(" [menuId],");
            strSql.Append(" [companyId],");
            strSql.Append(" [shopId],");
            strSql.Append(" [customerId],");
            strSql.Append(" [orderInJson],");
            strSql.Append(" [customerUUID],");
            strSql.Append(" [status],");
            strSql.Append(" [preOrderSum],");
            strSql.Append(" [preOrderServerSum],");
            strSql.Append(" [preOrderTime],");
            strSql.Append(" [isPaid],");
            strSql.Append(" [prePaidsum],");
            strSql.Append(" [prePayTime],");
            strSql.Append(" [viewallocCommission],");
            strSql.Append(" [transactionFee],");
            strSql.Append(" [viewallocNeedsToPayToShop],");
            strSql.Append(" [viewallocPaidToShopSum],");
            strSql.Append(" [viewallocTransactionCompleted],");
            strSql.Append(" [isApproved],");
            strSql.Append(" [verifiedsaving],");
            strSql.Append(" [isShopConfirmed],");
            strSql.Append(" [invoiceTitle],");
            strSql.Append(" [sundryJson],");
            strSql.Append(" [refundMoneySum],");
            strSql.Append(" [discount],");
            strSql.Append(" [refundMoneyClosedSum],");
            strSql.Append(" [deskNumber],");
            strSql.Append(" [refundRedEnvelope],");
            strSql.Append(" [appType],");
            strSql.Append(" [appBuild],");
            strSql.Append(" [OrderId],");
            strSql.Append(" [OrderType]");
            strSql.Append("  FROM PreOrder19dian WHERE  [OrderId]=@OrderId");
            SqlParameter[] parameters = new SqlParameter[]
            {
               new SqlParameter("@OrderId",orderId)
            };
            List<PreOrder19dianInfo> list = null;
            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters))
            {
                list = new List<PreOrder19dianInfo>();
                while (reader.Read())
                {
                    list.Add(reader.GetEntity<PreOrder19dianInfo>());
                }
            }
            return list;
        }

        /// <summary>
        /// 获取一张主订单下有多少张明细(正常订单，补差价订单)
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public long getPreOrder19dianCount(Guid orderId)
        {
            string sql = "SELECT Count(*) FROM PreOrder19dian WHERE isPaid=1 and OrderId=@OrderId";
            SqlParameter[] parameter = new SqlParameter[] { new SqlParameter("@OrderId", SqlDbType.UniqueIdentifier) { Value = orderId } };
            return (int)SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sql, parameter);
        }

        /// <summary>
        /// 由OrderId查询对应的所有订单
        /// </summary>
        /// <param name="preOrder19dianId"></param>
        /// <returns></returns>
        public DataTable SelectPreorder19dianByOrderId(Guid OrderId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("  select select * from PreOrder19dian");
            strSql.Append(" where OrderId=@OrderId");
            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@OrderId", SqlDbType.UniqueIdentifier ) {Value = OrderId}
            };
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), para);
            return ds.Tables[0];
        }
        public DataTable GetPreOrder19DianByPayTime(DateTime beginDateTime, DateTime endDateTime, int shopID)
        {
            const string strSql = @"select isnull(SUM(prePaidSum-isnull(refundMoneySum,0)),0) as prePaidSumTotal,Count(prePaidSum) as prePaidCount,Convert(nvarchar(10),preOrderTime,120) preOrderTime
                                    from PreOrder19dian where isPaid=1 and shopId=@shopID and prePaidSum>isnull(refundMoneySum,0)
                                    and preOrderTime between  @beginDateTime and @endDateTime
                                    group by Convert(nvarchar(10),preOrderTime,120)
                                    order by preOrderTime desc ";
            SqlParameter[] para = new SqlParameter[] { 
                new SqlParameter("@shopID", SqlDbType.Int) { Value = shopID },
                new SqlParameter("@beginDateTime", SqlDbType.DateTime) { Value = beginDateTime },
                new SqlParameter("@endDateTime", SqlDbType.DateTime) { Value = endDateTime }
            };
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), para);
            return ds.Tables[0];
        }

        /// <summary>
        /// 根据编号，查询预点单详情信息
        /// </summary>
        /// <param name="preOrder19dianId"></param>
        /// <returns></returns>
        public DataTable SelectPreOrderGroupByOrderId(long preOrder19dianId)
        {
            const string strSql = @"select min(a.[preOrder19dianId]) preOrder19dianId,max([menuId]) menuId,
                                    max(a.[shopId]) shopId,max(sundryJson) sundryJson,max(discount) discount,
                                    max(companyId) companyId,max(a.[customerId]) customerId,
                                    max([orderInJson]) orderInJson,max([customerUUID]) customerUUID,max(a.[status]) status,
                                    sum([preOrderSum]) preOrderSum,sum(a.[preOrderServerSum]) preOrderServerSum,
                                    min(a.[preOrderTime]) preOrderTime,max(a.[isPaid]) isPaid,sum(a.[prePaidSum]) prePaidSum,
                                    min(a.[prePayTime]) prePayTime,sum([viewallocCommission]) viewallocCommission,
                                    max(isnull([isApproved],0)) isApproved,max(isnull(a.[isShopConfirmed],0)) isShopConfirmed,
                                    sum(a.[refundMoneySum]) refundMoneySum,max(d.RefundMoneySum) RefundMoneySum1,
                                    max(mobilePhoneNumber) mobilePhoneNumber,max(b.UserName) UserName,
                                    sum(preOrderTotalAmount) preOrderTotalAmount,max(preOrderTotalQuantity) preOrderTotalQuantity,
                                    max(currentPlatformVipGrade) currentPlatformVipGrade,max(appBuild) appBuild,OrderId 
                                    from PreOrder19dian a
                                    inner join CustomerInfo b on a.customerId=b.CustomerID 
                                    inner join VAGastronomistMobileApp0.dbo.Orders d on a.OrderId=d.Id
                                    where a.OrderId=(select OrderId from PreOrder19dian d where d.preOrder19dianId=@preOrder19dianId)
                                    group by OrderId";
            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@preOrder19dianId",SqlDbType.BigInt) { Value = preOrder19dianId }
            };
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, para);
            return ds.Tables[0];
        }

        /// <summary>
        /// 对账预点单
        /// </summary>
        /// <param name="preOrder19dianId"></param>
        /// <param name="approveTag"></param>
        /// <returns></returns>
        public bool ApprovePreOrder19dianNew(long preOrder19dianId, int approveTag)
        {
            SqlParameter[] param ={
                                      new SqlParameter("@preOrder19dianId",preOrder19dianId),
                                      new SqlParameter("@isApproved",approveTag)
                                  };
            //修改PreOrder19dian
            const string strSql = "update PreOrder19dian set isApproved=@isApproved whereOrderId=(select OrderId from PreOrder19dian where preOrder19dianId=@preOrder19dianId)";
            int resultApprovePreOrder19dian = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, param);
            if (resultApprovePreOrder19dian > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 根据订单ID查询PreOrder19dian表的数据 add by lb
        /// </summary>
        /// <param name="orderId">订单ID</param>
        /// <returns></returns>
        public List<PreOrder19dianInfo> GetPreOrder19dianByOrderIdNew(Guid orderId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" SELECT ");
            strSql.Append(" [preOrder19dianId],");
            strSql.Append(" [menuId],");
            strSql.Append(" [companyId],");
            strSql.Append(" [shopId],");
            strSql.Append(" [customerId],");
            strSql.Append(" [orderInJson],");
            strSql.Append(" [customerUUID],");
            strSql.Append(" [status],");
            strSql.Append(" [preOrderSum],");
            strSql.Append(" [preOrderServerSum],");
            strSql.Append(" [preOrderTime],");
            strSql.Append(" [isPaid],");
            strSql.Append(" [prePaidsum],");
            strSql.Append(" [prePayTime],");
            strSql.Append(" [viewallocCommission],");
            strSql.Append(" [transactionFee],");
            strSql.Append(" [viewallocNeedsToPayToShop],");
            strSql.Append(" [viewallocPaidToShopSum],");
            strSql.Append(" [viewallocTransactionCompleted],");
            strSql.Append(" [isApproved],");
            strSql.Append(" [verifiedsaving],");
            strSql.Append(" [isShopConfirmed],");
            strSql.Append(" [invoiceTitle],");
            strSql.Append(" [sundryJson],");
            strSql.Append(" [refundMoneySum],");
            strSql.Append(" [discount],");
            strSql.Append(" [refundMoneyClosedSum],");
            strSql.Append(" [deskNumber],");
            strSql.Append(" [refundRedEnvelope],");
            strSql.Append(" [appType],");
            strSql.Append(" [appBuild],");
            strSql.Append(" [OrderId],");
            strSql.Append(" [OrderType]");
            strSql.Append("  FROM PreOrder19dian WHERE  [OrderId]=@OrderId and OrderType=1");
            SqlParameter[] parameters = new SqlParameter[]
            {
               new SqlParameter("@OrderId",orderId)
            };
            List<PreOrder19dianInfo> list = null;
            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters))
            {
                list = new List<PreOrder19dianInfo>();
                while (reader.Read())
                {
                    list.Add(reader.GetEntity<PreOrder19dianInfo>());
                }
            }
            return list;
        }

        #endregion

        /// <summary>
        /// 更新点单PreOrder19dian 的 OrderInJson
        /// </summary>
        /// <param name="orderJson"></param>
        /// <param name="preOrderId"></param>
        /// <returns></returns>
        public bool UpdatePreOrderOrderJson(string orderJson, long preOrderId)
        {
            const string strSql = @"update PreOrder19dian set orderInJson=@orderJson where preOrder19DianId=@preOrder19dianId";
            SqlParameter[] parameters = { new SqlParameter("@orderJson", orderJson), new SqlParameter("@preOrder19dianId", preOrderId) };
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql, parameters) > 0;
            }
        }

        /// <summary>
        /// 券使用的订单总金额
        /// </summary>
        /// <param name="CouponId"></param>
        /// <returns></returns>
        public DataTable GetPrePaidSumByCouponId(int CouponId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select Sum(prePaidSum+isnull(b.RealDeductibleAmount,0)-isnull(refundMoneySum,0)) prePaidSum,sum(b.RealDeductibleAmount) RealDeductibleAmount from PreOrder19dian a");
            strSql.Append(" inner join CouponGetDetail b on a.preOrder19dianId=b.PreOrder19DianId ");
            strSql.Append(" where isApproved=1 and isPaid=1 and prePaidSum>isnull(refundMoneySum,0)");
            strSql.Append(" and a.preOrder19dianId in (select preOrder19dianId from CouponGetDetail where CouponId=@CouponId)");

            SqlParameter[] param ={
                                      new SqlParameter("@CouponId",SqlDbType.Int){Value=CouponId}
                                  };
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), param);
            return ds.Tables[0];
        }

        /// <summary>
        /// 获取第三方支付的数量
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        public bool GetThirdPay(Guid orderID)
        {
            string strSql = @"select COUNT(*) from Preorder19DianLine where (PayType=2 or PayType=3 or PayType=4) and Preorder19DianId in 
                                            (select Preorder19DianId from PreOrder19dian where OrderId=@OrderId)";
            SqlParameter[] param ={
                                      new SqlParameter("@OrderId",SqlDbType.UniqueIdentifier){Value=orderID}
                                  };
            Object obj=SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), param);
            if(Convert.ToInt32(obj)>0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
