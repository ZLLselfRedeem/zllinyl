using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using PagedList;
using VAGastronomistMobileApp.DBUtility;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL.Persistence.Infrastructure;
using System.Text;

namespace VAGastronomistMobileApp.SQLServerDAL.Persistence
{
    public class SqlServerPreOrder19DianInfoRepository : SqlServerRepositoryBase, IPreOrder19DianInfoRepository
    {
        public SqlServerPreOrder19DianInfoRepository(ISqlConnectionFactory connectionFactory)
            : base(connectionFactory)
        {
        }

        public PreOrder19dianInfo GetById(long id)
        {
            const string cmdText = @"SELECT customerId,shopId,orderInJson FROM [dbo].[PreOrder19dian] WHERE [preOrder19dianId]=@preOrder19dianId";

            var cmdParm = new SqlParameter("@preOrder19dianId", id);

            PreOrder19dianInfo preOrder19DianInfo = null;
            using (var dr = SqlHelper.ExecuteReader(Connection, CommandType.Text, cmdText, cmdParm))
            {
                if (dr.Read())
                    preOrder19DianInfo = SqlHelper.GetEntity<PreOrder19dianInfo>(dr);
            }
            return preOrder19DianInfo;
        }

        public void UpdateOrderRefundStatusAndMoney(long orderId, double money, int refundStatus)
        {
            const string cmdText = @"update [dbo].[PreOrder19dian] set [status]=@status, refundMoneyClosedSum=
case when isnull(refundMoneyClosedSum,0)+@refundMoneySum >=isnull(refundMoneySum,0) then isnull(refundMoneySum,0) else isnull(refundMoneyClosedSum,0)+@refundMoneySum end
where preOrder19dianId=@preOrder19dianId";

            SqlParameter[] cmdParms = new SqlParameter[]
			{
				new SqlParameter("@status", SqlDbType.Int){Value = refundStatus}, 
				new SqlParameter("@refundMoneySum", SqlDbType.Float){Value = money},
				new SqlParameter("@preOrder19dianId", SqlDbType.BigInt){Value = orderId}, 
			};

            SqlHelper.ExecuteNonQuery(Connection, CommandType.Text, cmdText, cmdParms);
        }

        public void UpdateOrderRefundMoney(long orderId, double money)
        {
            const string cmdText = @"update [dbo].[PreOrder19dian] set refundMoneyClosedSum=
case when isnull(refundMoneyClosedSum,0)+@refundMoneySum >=isnull(refundMoneySum,0) then isnull(refundMoneySum,0) else isnull(refundMoneyClosedSum,0)+@refundMoneySum end
where preOrder19dianId=@preOrder19dianId";

            SqlParameter[] cmdParms = new SqlParameter[]
			{
				new SqlParameter("@refundMoneySum", SqlDbType.Float){Value = money},
				new SqlParameter("@preOrder19dianId", SqlDbType.BigInt){Value = orderId}, 
			};

            SqlHelper.ExecuteNonQuery(Connection, CommandType.Text, cmdText, cmdParms);
        }



        public IPagedList<CustomerPreOrder> GetPageCustomerPayforOrders(Page page, long customerId)
        {
            const string cmdTextCount = @"SELECT COUNT(1) FROM [dbo].[PreOrder19dian] a
										INNER JOIN [dbo].[ShopInfo] b on a.shopId= b.shopId
										WHERE a.[customerId]=@customerId AND a.[isPaid]=1";
            const string cmdText = @"SELECT t.preOrderTime,t.prePaidSum,t.orderInJson,t.shopName,t.shopLogo,t.shopImagePath FROM(
SELECT ROW_NUMBER() OVER(ORDER BY a.preOrderTime DESC) as RowNum, a.preOrderTime,a.prePaidSum,a.orderInJson,b.shopName,b.shopLogo,b.shopImagePath 
FROM [dbo].[PreOrder19dian] a
INNER JOIN [dbo].[ShopInfo] b on a.shopId= b.shopId
WHERE a.[customerId]=@customerId AND a.[isPaid]=1) AS t
WHERE t.RowNum BETWEEN @BeginIndex AND @EndIndex ";

            SqlParameter cmdParmCount = new SqlParameter("@customerId", SqlDbType.BigInt) { Value = customerId };
            SqlParameter[] cmdParms = new SqlParameter[]
			{
				cmdParmCount, new SqlParameter("@BeginIndex", SqlDbType.Int) {Value = page.Skip + 1},
				new SqlParameter("@EndIndex", SqlDbType.Int) {Value = page.Skip + page.PageSize},
			};

            object o = SqlHelper.ExecuteScalar(Connection, CommandType.Text, cmdTextCount, cmdParmCount);
            int count = Convert.ToInt32(o);

            List<CustomerPreOrder> list = new List<CustomerPreOrder>();
            using (var dr = SqlHelper.ExecuteReader(Connection, CommandType.Text, cmdText, cmdParms))
            {
                while (dr.Read())
                {
                    list.Add(SqlHelper.GetEntity<CustomerPreOrder>(dr));
                }
            }

            return new StaticPagedList<CustomerPreOrder>(list, page.PageNumber, page.PageSize, count);
        }

        public IPagedList<ShopPreOrder> GetPageNoApprovedShopOrders(Page page, int shopId)
        {
            const string cmdTextCount = @"SELECT COUNT(1)
											  FROM [dbo].[PreOrder19dian] a
											  INNER JOIN [dbo].[CustomerInfo] b ON a.[customerId]=b.[customerId]  
											WHERE  a.[isPaid]=1 AND ((a.[isShopConfirmed]=1 AND ISNULL(a.[isApproved],0)=0) OR (ISNULL(a.[isShopConfirmed],0)=0 AND a.[status] NOT IN(105,104,107))) 
										AND a.[shopId]=@shopId";

            // ,(SELECT COUNT(1) FROM [dbo].[PreOrder19dian] WHERE [customerId]=a.customerId AND [isPaid]=1) AS [PreOrderTotalQuantity]
            const string cmdText = @"SELECT [preOrder19dianId],[shopId],[customerId],[prePayTime],[isApproved],[refundMoneySum]      
	  ,[deskNumber],[invoiceTitle],[isShopConfirmed],[UserName],[mobilePhoneNumber]
	  ,[PreOrderTotalQuantity],[Picture],[RegisterDate],[Shared]
FROM (
SELECT ROW_NUMBER() OVER(ORDER BY a.prePayTime DESC) as RowNum
	  ,a.[preOrder19dianId]   
	  ,a.[shopId]   
	  ,a.[customerId]     
	  ,a.[prePayTime]         
	  ,a.[isApproved]      
	  ,a.[refundMoneySum]      
	  ,a.[deskNumber]
	  ,a.[invoiceTitle]
	  ,a.[isShopConfirmed]
	  ,b.[UserName]
	  ,b.[mobilePhoneNumber]	  
	  ,b.[Picture],b.[RegisterDate]
     ,case when a.[isShopConfirmed]= 1 then case when isnull(b.PreOrderTotalQuantity,0)<=0
      then 1 else isnull(b.PreOrderTotalQuantity,0) end
      else isnull(b.PreOrderTotalQuantity,0)+1 end as [PreOrderTotalQuantity]
      ,(SELECT COUNT(1) FROM [dbo].[FoodDiary] WHERE [OrderId]=a.[preOrder19dianId] AND [Shared]>0) AS [Shared]
  FROM [dbo].[PreOrder19dian] a
  INNER JOIN [dbo].[CustomerInfo] b ON a.[customerId]=b.[customerId]  
WHERE  a.[isPaid]=1 AND ((a.[isShopConfirmed]=1 AND ISNULL(a.[isApproved],0)=0) OR (ISNULL(a.[isShopConfirmed],0)=0 AND a.[status] NOT IN(105,104,107))) 
and a.[shopId]=@shopId) AS t WHERE t.RowNum BETWEEN @BeginIndex AND @EndIndex ORDER BY RowNum ASC";

            SqlParameter cmdParmCount = new SqlParameter("@shopId", SqlDbType.Int) { Value = shopId };
            SqlParameter[] cmdParms = new SqlParameter[]
			{
				cmdParmCount, new SqlParameter("@BeginIndex", SqlDbType.Int) {Value = page.Skip + 1},
				new SqlParameter("@EndIndex", SqlDbType.Int) {Value = page.Skip + page.PageSize},
			};

            object o = SqlHelper.ExecuteScalar(Connection, CommandType.Text, cmdTextCount, cmdParmCount);
            int count = Convert.ToInt32(o);

            List<ShopPreOrder> list = new List<ShopPreOrder>();
            using (var dr = SqlHelper.ExecuteReader(Connection, CommandType.Text, cmdText, cmdParms))
            {
                while (dr.Read())
                {
                    list.Add(SqlHelper.GetEntity<ShopPreOrder>(dr));
                }
            }

            return new StaticPagedList<ShopPreOrder>(list, page.PageNumber, page.PageSize, count);
        }

        public void UpdateOrderDeskNumber(long orderId, string deskNumber)
        {
            const string cmdText = @"UPDATE [dbo].[PreOrder19dian] SET [deskNumber]=@deskNumber WHERE [preOrder19dianId]=@preOrder19dianId";
            SqlParameter[] cmdParms = new SqlParameter[]
			{
				new SqlParameter("@deskNumber", SqlDbType.NVarChar,50){Value = deskNumber},
				new SqlParameter("@preOrder19dianId", SqlDbType.BigInt){Value = orderId}, 
			};

            SqlHelper.ExecuteNonQuery(Connection, CommandType.Text, cmdText, cmdParms);
        }
    }
    public class SqlServerPreOrder19DianInfoManager
    {
        public List<ShopPreOrder> GetPageNoApprovedShopOrders(Page page, int shopId)
        {
            const string cmdText = @"SELECT [preOrder19dianId],[shopId],[customerId],[prePayTime],[isApproved],[refundMoneySum]      
	  ,[deskNumber],[invoiceTitle],[isShopConfirmed],[UserName],[mobilePhoneNumber]
	  ,[PreOrderTotalQuantity],[Picture],[RegisterDate],[Shared],beforeDeductionPrice,afterDeductionPrice,hadCoupon
FROM (
SELECT ROW_NUMBER() OVER(ORDER BY a.prePayTime DESC) as RowNum
	  ,a.[preOrder19dianId]   
	  ,a.[shopId]   
	  ,a.[customerId]     
	  ,a.[prePayTime]         
	  ,a.[isApproved]      
	  ,a.[refundMoneySum]      
	  ,a.[deskNumber]
	  ,a.[invoiceTitle]
	  ,a.[isShopConfirmed]
	  ,b.[UserName]
	  ,b.[mobilePhoneNumber]	  
	  ,b.[Picture],b.[RegisterDate],(a.preOrderSum-isnull(a.verifiedSaving,0)) afterDeductionPrice ,ISNULL(c.DeductibleAmount ,0)+a.prePaidSum beforeDeductionPrice 
     ,case when a.[isShopConfirmed]= 1 then case when isnull(b.PreOrderTotalQuantity,0)<=0
      then 1 else isnull(b.PreOrderTotalQuantity,0) end
      else isnull(b.PreOrderTotalQuantity,0)+1 end as [PreOrderTotalQuantity]
      ,(SELECT COUNT(1) FROM [dbo].[FoodDiary] WHERE [OrderId]=a.[preOrder19dianId] AND [Shared]>0) AS [Shared]
      ,(SELECT COUNT(1) FROM [dbo].CouponUseRecord cr WHERE preOrder19dianId=a.[preOrder19dianId]) AS [hadCoupon]
FROM [dbo].[PreOrder19dian] a
  INNER JOIN [dbo].[CustomerInfo] b ON a.[customerId]=b.[customerId]  
  left join CouponGetDetail c on c.PreOrder19DianId=a.preOrder19dianId
WHERE a.[shopId]=@shopId and a.[isPaid]=1
and isnull(a.expireTime, DATEADD(DAY,1,GETDATE()))>GETDATE()
AND ((a.[isShopConfirmed]=1 AND ISNULL(a.[isApproved],0)=0) OR (ISNULL(a.[isShopConfirmed],0)=0 AND a.[status] NOT IN(105,104,107))))
 AS t WHERE t.RowNum BETWEEN @BeginIndex AND @EndIndex ORDER BY RowNum ASC";

            SqlParameter[] cmdParms = new SqlParameter[]
            {
                new SqlParameter("@shopId", SqlDbType.Int) {Value = shopId},
                new SqlParameter("@BeginIndex", SqlDbType.Int) {Value = page.Skip + 1},
                new SqlParameter("@EndIndex", SqlDbType.Int) {Value = page.Skip + page.PageSize},
            };

            List<ShopPreOrder> list = new List<ShopPreOrder>();
            using (var dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, cmdText, cmdParms))
            {
                while (dr.Read())
                {
                    list.Add(SqlHelper.GetEntity<ShopPreOrder>(dr));
                }
            }
            return list;
        }
        /// <summary>
        /// 查询所有未对账的订单
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public List<PreOrderList> GetUnApprovedShopOrders(int shopId, bool isNew)
        {
            string strSql = @"select B.preOrder19dianId,B.isShopConfirmed,B.prePayTime,isnull(C.UserName,'') UserName,C.mobilePhoneNumber,isnull(B.refundMoneySum,0) refundMoneySum,	  
round((B.preOrderSum-B.verifiedSaving),2) afterDeductionPrice,
round((B.prePaidSum+B.verifiedSaving),2) beforeDeductionPrice,orderId
 from PreOrder19dian B  inner join CustomerInfo C on C.CustomerID=B.customerId
 left join CouponGetDetail co on B.preOrder19dianId=co.PreOrder19DianId
 where B.shopId=@shopId and B.isPaid=1 ";

            if (isNew)
            {
                strSql += " and B.orderType=1";
            }
            strSql += @" AND ((B.[isShopConfirmed]=1 AND ISNULL(B.[isApproved],0)=0) OR (ISNULL(B.[isShopConfirmed],0)=0 AND B.[status] NOT IN(105,104,107)))
 and isnull(B.expireTime, DATEADD(DAY,1,GETDATE()))>GETDATE()
 and C.mobilePhoneNumber!='' 
 order by B.prePayTime desc";

            SqlParameter[] para = new SqlParameter[] { 
        new SqlParameter("@shopId", SqlDbType.Int) { Value =shopId  }};

            List<PreOrderList> list = new List<PreOrderList>();
            using (SqlDataReader sdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, para))
            {
                while (sdr.Read())
                {
                    list.Add(SqlHelper.GetEntity<PreOrderList>(sdr));
                }
            }
            return list;
        }

        public List<PreOrderListAttach> GetUnApprovedShopOrdersAttach(long[] preOrder19dianId)
        {
            string strSql = @"select p.preOrder19dianId,p.invoiceTitle,p.refundMoneySum,p.deskNumber,p.isShopConfirmed,c.Picture,c.RegisterDate,c.mobilePhoneNumber,
case when p.[isShopConfirmed]= 1 then case when isnull(c.PreOrderTotalQuantity,0)<=0
      then 1 else isnull(c.PreOrderTotalQuantity,0) end
      else isnull(c.PreOrderTotalQuantity,0)+1 end as [PreOrderTotalQuantity]       
 from PreOrder19dian p inner join CustomerInfo c on p.customerId = c.CustomerID
where preOrder19dianId in (select d.x.value('./id[1]','bigInt') from @xml.nodes('/*') as d(x))";
          
            var xml = new StringBuilder();
            foreach (var t in preOrder19dianId)
            {
                xml.AppendFormat("<row><id>{0}</id></row>", (int)t);
            }

            SqlParameter[] para = new SqlParameter[]{
                new SqlParameter("@xml", SqlDbType.Xml) { Value = xml.ToString() }
            };
            List<PreOrderListAttach> preOrderListAttach = new List<PreOrderListAttach>();
            using (SqlDataReader sdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, para))
            {
                while (sdr.Read())
                {
                    preOrderListAttach.Add(SqlHelper.GetEntity<PreOrderListAttach>(sdr));
                }
            }
            return preOrderListAttach;
        }

        public List<PreOrderListAttach> GetUnApprovedShopOrdersAttachByOrderId(Guid[] orderIds)
        {
            string strSql = @"select p.preOrder19dianId,p.invoiceTitle,p.refundMoneySum,p.deskNumber,p.isShopConfirmed,c.Picture,c.RegisterDate,c.mobilePhoneNumber,
case when p.[isShopConfirmed]= 1 then case when isnull(c.PreOrderTotalQuantity,0)<=0
      then 1 else isnull(c.PreOrderTotalQuantity,0) end
      else isnull(c.PreOrderTotalQuantity,0)+1 end as [PreOrderTotalQuantity],p.OrderId       
 from PreOrder19dian p inner join CustomerInfo c on p.customerId = c.CustomerID
where orderId in (select d.x.value('./id[1]','uniqueidentifier') from @xml.nodes('/*') as d(x)) and orderType=1";

            var xml = new StringBuilder();
            foreach (var t in orderIds)
            {
                xml.AppendFormat("<row><id>{0}</id></row>", (Guid)t);
            }

            SqlParameter[] para = new SqlParameter[]{
                new SqlParameter("@xml", SqlDbType.Xml) { Value = xml.ToString() }
            };
            List<PreOrderListAttach> preOrderListAttach = new List<PreOrderListAttach>();
            using (SqlDataReader sdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, para))
            {
                while (sdr.Read())
                {
                    preOrderListAttach.Add(SqlHelper.GetEntity<PreOrderListAttach>(sdr));
                }
            }
            return preOrderListAttach;
        }

        /// <summary>
        /// 查询指定点单的美食日记分享信息
        /// </summary>
        /// <param name="preOrder19dianId"></param>
        /// <returns></returns>
        public List<long> GetPreOrderSharedInfo(long[] preOrder19dianId)
        {
            const string strSql = "SELECT [OrderId] FROM [dbo].[FoodDiary] WHERE [OrderId] in (select d.x.value('./id[1]','bigInt') from @xml.nodes('/*') as d(x)) AND [Shared]>0";
            var xml = new StringBuilder();
            foreach (var t in preOrder19dianId)
            {
                xml.AppendFormat("<row><id>{0}</id></row>", (int)t);
            }

            SqlParameter[] para = new SqlParameter[]{
                new SqlParameter("@xml", SqlDbType.Xml) { Value = xml.ToString() }
            };
            List<long> orderId = new List<long>();
            using (SqlDataReader sdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, para))
            {
                while (sdr.Read())
                {
                    orderId.Add(Convert.ToInt64(sdr[0]));
                }
            }
            return orderId;
        }
        /// <summary>
        /// 查询指定点单的优惠券信息
        /// </summary>
        /// <param name="preOrder19dianId"></param>
        /// <returns></returns>
        public List<long> GetPreOrderCouponInfo(long[] preOrder19dianId)
        {
            const string strSql = "SELECT preOrder19dianId FROM [dbo].CouponUseRecord cr WHERE preOrder19dianId in (select d.x.value('./id[1]','bigInt') from @xml.nodes('/*') as d(x))";
            var xml = new StringBuilder();
            foreach (var t in preOrder19dianId)
            {
                xml.AppendFormat("<row><id>{0}</id></row>", (int)t);
            }

            SqlParameter[] para = new SqlParameter[]{
                new SqlParameter("@xml", SqlDbType.Xml) { Value = xml.ToString() }
            };
            List<long> orderId = new List<long>();
            using (SqlDataReader sdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, para))
            {
                while (sdr.Read())
                {
                    orderId.Add(Convert.ToInt64(sdr[0]));
                }
            }
            return orderId;
        }

        /// <summary>
        /// 查询某个订单的支付总额详情
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public OrderPaidDetail GetOrderPaid(Guid orderId)
        {
            const string strSql = "select id,PreOrderServerSum,PrePaidSum,VerifiedSaving,refundMoneySum from [Order] where Id=@orderId";
            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@orderId", SqlDbType.UniqueIdentifier) { Value= orderId }
            };
            OrderPaidDetail orderPaidDetail = new OrderPaidDetail();
            using (SqlDataReader sdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, para))
            {
                if (sdr.Read())
                {
                    orderPaidDetail = SqlHelper.GetEntity<OrderPaidDetail>(sdr);
                }
            }
            return orderPaidDetail;
        }        
    }
}