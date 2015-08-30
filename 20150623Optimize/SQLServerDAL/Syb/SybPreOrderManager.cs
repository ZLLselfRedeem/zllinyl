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
    public class SybPreOrderManager
    {
        /// <summary>
        /// 收银宝查询所有未对账点单信息（入座列表）
        /// </summary>
        /// <returns></returns>
        public DataTable GetSybConfirmedOrder(int shopId)
        {
            //            const string strSql = @"select B.shopId,B.preOrder19dianId,B.isShopConfirmed,round((B.preOrderSum-isnull(B.verifiedSaving,0)),2) prePaidSum,B.prePayTime,isnull(C.UserName,'') UserName,C.mobilePhoneNumber,isnull(B.refundMoneySum,0) refundMoneySum,OrderId
            // from PreOrder19dian B  inner join CustomerInfo C on C.CustomerID=B.customerId
            // where B.shopId=@shopId and B.isPaid=1 
            // AND ((B.[isShopConfirmed]=1 AND ISNULL(B.[isApproved],0)=0) OR (ISNULL(B.[isShopConfirmed],0)=0 AND B.[status] NOT IN(105,104,107)))
            // and isnull(B.expireTime, DATEADD(DAY,1,GETDATE()))>GETDATE()
            // and C.mobilePhoneNumber!='' and B.orderType=1";
            //Round((b.preOrderSum - Isnull(b.verifiedSaving,0)),2) 
            const string strSql = @"SELECT b.shopId,
                                           b.preOrder19dianId,
                                           b.isShopConfirmed,
                                           b.prePaidSum+isnull(p.RealDeductibleAmount,0) prePaidSum,
                                           b.prePayTime,
                                           Isnull(c.UserName,'') UserName,
                                           c.mobilePhoneNumber,
                                           Isnull(b.refundmoneysum,0) refundMoneySum,
                                           OrderId,
                                           co.RequirementMoney,
                                           co.DeductibleAmount,
                                           co.MaxAmount
                                    FROM   preorder19dian b
                                           INNER JOIN customerinfo c
                                             ON c.customerid = b.customerid
                                           left JOIN CouponGetDetail p
		                                     on b.preOrder19dianId=p.PreOrder19DianId and p.UseTime is not null
	                                       left join Coupon co
	                                         on p.CouponId=co.CouponId
                                    WHERE  b.shopid = @shopId
                                           AND b.ispaid = 1
                                           AND ((b.[isShopConfirmed] = 1
                                                 AND Isnull(b.[isApproved],0) = 0)
                                                 OR (Isnull(b.[isShopConfirmed],0) = 0
                                                     AND b.[status] NOT IN (105,
                                                                            104,
                                                                            107)))
                                           AND Isnull(b.expiretime,Dateadd(DAY,1,Getdate())) > Getdate()
                                           AND c.mobilephonenumber != '' AND OrderType=1";
            //AND b.ordertype = 1";
            SqlParameter parameter = new SqlParameter("@shopId", shopId);
            DataSet dt = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, parameter);
            return dt.Tables[0];
        }

        /// <summary>
        /// 收银宝查询所有需要对账点单（对账列表）
        /// </summary>
        /// <param name="shopId"></param>
        /// <param name="inputTextStr"></param>
        /// <param name="approvedStatus"></param>
        /// <param name="preOrderTimeStr"></param>
        /// <param name="preOrderTimeEnd"></param>
        /// <returns></returns>
        public DataTable GetSybVerifiedOrder(int shopId, string inputTextStr, int approvedStatus, string preOrderTimeStr, string preOrderTimeEnd, int CouponType)
        {
            StringBuilder strBuilder = new StringBuilder();
            //            strBuilder.Append(@"select B.preOrder19dianId,round((B.preOrderSum-isnull(B.verifiedSaving,0)),2) prePaidSum,
            //isnull(B.isApproved,0) as isApproved,B.prePayTime,isnull(C.UserName,'') UserName,C.mobilePhoneNumber,
            //isnull(B.refundMoneySum,0) refundMoneySum,OrderId");
            //            strBuilder.Append(" from PreOrder19dian B inner join CustomerInfo C on C.CustomerID=B.customerId left join PreorderShopConfirmedInfo D on B.preOrder19dianId=D.preOrder19dianId");
            //Round((b.preordersum - Isnull(b.verifiedsaving,0)),
            //                                           2)
            strBuilder.AppendFormat(@"SELECT   b.preOrder19dianId,
                                                 b.prePaidSum+isnull(p.RealDeductibleAmount,0) prePaidSum,
                                                 Isnull(b.isapproved,0) AS isApproved,
                                                 b.prePayTime,
                                                 Isnull(c.username,'') UserName,
                                                 c.mobilePhoneNumber,
                                                 Isnull(b.refundmoneysum,0) refundMoneySum,
                                                 OrderId,
                                                 co.RequirementMoney,
                                                 co.DeductibleAmount,
                                                 co.MaxAmount,
                                                 case when b.status = 105 THEN 0 when b.status=107 THEN 0 when p.RequirementMoney is NULL then 0 when p.RequirementMoney = 0 then 0 
                                                 when FLOOR(isnull(p.CalculationAmount,0)-isnull(refundMoneySum,0))<=0 then 0
                                                 when FLOOR((isnull(p.CalculationAmount,0)-isnull(refundMoneySum,0))/p.RequirementMoney)*isnull(p.DeductibleAmount,0)>co.MaxAmount then co.MaxAmount 
                                                 when FLOOR((isnull(p.CalculationAmount,0)-isnull(refundMoneySum,0))/p.RequirementMoney)*isnull(p.DeductibleAmount,0)>p.RealDeductibleAmount then p.RealDeductibleAmount 
                                                 else FLOOR((isnull(p.CalculationAmount,0)-isnull(refundMoneySum,0))/p.RequirementMoney)*isnull(p.DeductibleAmount,0) end RealDeductibleAmount
                                        FROM     preorder19dian b
                                                 INNER JOIN customerinfo c
                                                   ON c.customerid = b.customerid
                                                 LEFT JOIN preordershopconfirmedinfo d
                                                   ON b.preorder19dianid = d.preorder19dianid
                                                 left JOIN CouponGetDetail p
		                                           on b.preOrder19dianId=p.PreOrder19DianId and p.UseTime is not null
	                                             left join Coupon co
	                                               on p.CouponId=co.CouponId ");
            strBuilder.AppendFormat(" where isPaid=1 and B.shopId={0} and B.isShopConfirmed=1  and B.OrderType=1", shopId);
            switch (approvedStatus)
            {
                case 1://已对账
                    strBuilder.Append(" and isApproved=1");
                    break;
                case 2://未对账
                    strBuilder.Append(" and isnull(isApproved,0)=0");
                    break;
                default: //approvedStatus=“全部”
                    break;
            }
            if (CouponType != 0)
            {
                strBuilder.AppendFormat(" and co.CouponType={0}", CouponType);//输入的流水号不为空
            }
            if (!string.IsNullOrEmpty(inputTextStr))
            {
                strBuilder.AppendFormat(" and c.mobilePhoneNumber like '%{0}%'", inputTextStr.Trim().ToString());//输入的流水号不为空
            }
            strBuilder.Append(" group by B.preOrder19dianId,B.status,B.invoiceTitle,B.preOrderSum,B.preOrderServerSum,B.prePaidSum,B.isApproved ,B.prePayTime,C.UserName,C.mobilePhoneNumber,B.refundMoneySum,B.verifiedSaving,OrderId,co.RequirementMoney,co.DeductibleAmount,co.MaxAmount,p.RealDeductibleAmount,p.RequirementMoney,p.DeductibleAmount,p.CalculationAmount");
            strBuilder.AppendFormat(" having MAX(D.shopConfirmedTime) between '{0}' and '{1}' order by MAX(D.shopConfirmedTime) desc", Convert.ToDateTime(preOrderTimeStr + " 00:00:00"), Convert.ToDateTime(preOrderTimeEnd + " 23:59:59"));

            DataSet dt = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strBuilder.ToString());
            return dt.Tables[0];
        }

        /// <summary>
        /// 查询门店是否有未处理完结的单子
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public static bool CheckSybShopIsHaveUntreatedOrder(int shopId)
        {
            const string strSql = @"select COUNT(preOrder19dianId) from PreOrder19dian
where shopId=@shopId and isPaid=1 and status not in(107,105,104)
 and ISNULL(isApproved,0)=0";
            SqlParameter parameter = new SqlParameter("@shopId", shopId);
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, parameter))
            {
                if (dr.Read())
                {
                    return Convert.ToInt32(dr[0]) > 0;
                }
            }
            return false;
        }
    }
}
