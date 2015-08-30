using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.DBUtility;
using System.Data.SqlClient;
using System.Data;
using VAGastronomistMobileApp.Model;

namespace VAGastronomistMobileApp.SQLServerDAL
{
    public class AwardConnPreOrderManager
    {
        public bool InsertAwardConnPreOrder(AwardConnPreOrder awardConnPreOrder)
        {
            string strSql = @"INSERT INTO [VAAward].[dbo].[AwardConnPreOrder]
           (Id,ShopId,PreOrder19dianId,OrderId,CustomerId,Type,AwardId,RedEnvelopeId,LotteryTime,ValidTime,Status)
     VALUES(@Id,@ShopId,@PreOrder19dianId,@OrderId,@CustomerId,@Type,@AwardId,@RedEnvelopeId,@LotteryTime,@ValidTime,@Status)";
            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@Id", SqlDbType.UniqueIdentifier) { Value = awardConnPreOrder.Id },
            new SqlParameter("@ShopId", SqlDbType.Int) { Value = awardConnPreOrder.ShopId },
            new SqlParameter("@PreOrder19dianId", SqlDbType.BigInt) { Value = awardConnPreOrder.PreOrder19dianId },
            new SqlParameter("@OrderId", SqlDbType.UniqueIdentifier) { Value = awardConnPreOrder.OrderId },
            new SqlParameter("@CustomerId", SqlDbType.BigInt) { Value = awardConnPreOrder.CustomerId },
            new SqlParameter("@Type", SqlDbType.Int) { Value = (int)awardConnPreOrder.Type },
            new SqlParameter("@AwardId", SqlDbType.UniqueIdentifier) { Value = awardConnPreOrder.AwardId },
            new SqlParameter("@RedEnvelopeId", SqlDbType.BigInt) { Value = awardConnPreOrder.redEnvelopeId },
            new SqlParameter("@LotteryTime", SqlDbType.DateTime) { Value = awardConnPreOrder.LotteryTime },
            new SqlParameter("@ValidTime", SqlDbType.DateTime) { Value = awardConnPreOrder.ValidTime },
            new SqlParameter("@Status", SqlDbType.Bit) { Value = awardConnPreOrder.Status }
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
        /// 获取商家对应的奖品列表
        /// </summary>
        /// <param name="shopID"></param>
        /// <returns></returns>
        public List<AwardConnPreOrder> GetAwardConnPreOrderList(int shopID, bool isAvoidQueue)
        {
            string strSql = @"select top 10 * from AwardConnPreOrder where ShopId=@ShopID order by LotteryTime desc";
            // 未开启免排队，需要过滤掉免排队的中奖数
            if (!isAvoidQueue)
            {
                strSql = @"select top 10 * from AwardConnPreOrder where ShopId=@ShopID and Type!=2 order by LotteryTime desc";
            }
            SqlParameter[] param ={
                                      new SqlParameter("@ShopID",shopID)
                                  };
            List<AwardConnPreOrder> awardConnPreOrderList = new List<AwardConnPreOrder>();
            using (SqlDataReader sdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), param))
            {
                while (sdr.Read())
                {
                    awardConnPreOrderList.Add(SqlHelper.GetEntity<AwardConnPreOrder>(sdr));
                }
            }
            return awardConnPreOrderList;
        }

        public AwardConnPreOrder GetAwardConnPreOrder(int shopID)
        {
            AwardConnPreOrder objAwardConnPreOrder = new AwardConnPreOrder();
            string strSql = @"select top 1 * from AwardConnPreOrder where ShopId=@ShopID order by Type desc";

            SqlParameter[] param ={
                                      new SqlParameter("@ShopID",shopID)
                                  };
            List<AwardConnPreOrder> awardConnPreOrderList = new List<AwardConnPreOrder>();
            using (SqlDataReader sdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), param))
            {
                if (sdr.Read())
                {
                    objAwardConnPreOrder = SqlHelper.GetEntity<AwardConnPreOrder>(sdr);
                }
            }
            return objAwardConnPreOrder;
        }

        /// <summary>
        /// 查询指定商户指定类型的奖品
        /// </summary>
        /// <param name="shopID"></param>
        /// <param name="awardType"></param>
        /// <returns></returns>
        public List<AwardConnPreOrder> GetAwardConnPreOrderList(int shopID, AwardType awardType)
        {
            const string strSql = "select * from AwardConnPreOrder where ShopId=@ShopID and AwardType=@awardType";
            SqlParameter[] param = new SqlParameter[]{
            new SqlParameter("@ShopID",SqlDbType.Int) { Value = shopID },
            new SqlParameter("@awardType",SqlDbType.Int) { Value = (int)awardType},
            };
            List<AwardConnPreOrder> awardConnPreOrderList = new List<AwardConnPreOrder>();
            using (SqlDataReader sdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), param))
            {
                while (sdr.Read())
                {
                    awardConnPreOrderList.Add(SqlHelper.GetEntity<AwardConnPreOrder>(sdr));
                }
            }
            return awardConnPreOrderList;
        }

        /// <summary>
        /// 根据用户退当日退款次数，检查其是否能抽奖
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public int CheckCusRefundCnt(long[] customerIds)
        {
            string strSql = @"select COUNT(1) from RefundLogData l inner join PreOrder19dian p
on l.preOrder19dianId = p.preOrder19dianId
and l.customerId in (select d.x.value('./id[1]','bigint') from @xml.nodes('/*') as d(x))
and refundTime>'" + DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00' and note in ('客户端原路退款','客户端退款到余额')";

            var xml = new StringBuilder();
            foreach (var t in customerIds)
            {
                xml.AppendFormat("<row><id>{0}</id></row>", t);
            }

            SqlParameter[] cmdParms = new[]
            {
                new SqlParameter("@xml", SqlDbType.Xml) { Value = xml.ToString() }
            };

            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                object obj = SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql, cmdParms);
                if (obj == null || Convert.ToInt32(obj) == 0)
                {
                    return 0;
                }
                else
                {
                    return Convert.ToInt32(obj);
                }
            }
        }

        /// <summary>
        /// 根据用户在门店的未消费订单个数，检查用户是否能抽奖
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public int CheckCusUnConfirmedOrderCntOfShop(long[] customerIds, int shopId)
        {
            const string strSql = @"select count(1) from PreOrder19dian
where customerId in (select d.x.value('./id[1]','bigint') from @xml.nodes('/*') as d(x))
and shopId=@shopId and isPaid=1 and ISNULL(isShopConfirmed,0)=0";

            var xml = new StringBuilder();
            foreach (var t in customerIds)
            {
                xml.AppendFormat("<row><id>{0}</id></row>", t);
            }
            List<SqlParameter> paraList = new List<SqlParameter>();

            paraList.Add(new SqlParameter("@xml", SqlDbType.Xml) { Value = xml.ToString() });
            paraList.Add(new SqlParameter("@shopId", SqlDbType.Int) { Value = shopId });

            SqlParameter[] para = paraList.ToArray();

            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                object obj = SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql, para);
                if (obj == null || Convert.ToInt32(obj) == 0)
                {
                    return 0;
                }
                else
                {
                    return Convert.ToInt32(obj);
                }
            }
        }

        /// <summary>
        /// 根据用户在平台的未消费订单个数，检查用户是否能继续支付
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public int CheckCusUnConfirmedOrderCntOfVA(long[] customerIds)
        {
            string strSql = @"select count(1) from PreOrder19dian
where customerId in (select d.x.value('./id[1]','bigint') from @xml.nodes('/*') as d(x))
and isPaid=1 and ISNULL(isShopConfirmed,0)=0 and status not in (" + (int)VAPreorderStatus.OriginalRefunding + "," + (int)VAPreorderStatus.Refund + ")";
            var xml = new StringBuilder();
            foreach (var t in customerIds)
            {
                xml.AppendFormat("<row><id>{0}</id></row>", t);
            }
            List<SqlParameter> paraList = new List<SqlParameter>();

            paraList.Add(new SqlParameter("@xml", SqlDbType.Xml) { Value = xml.ToString() });

            SqlParameter[] para = paraList.ToArray();

            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                object obj = SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql, para);
                if (obj == null || Convert.ToInt32(obj) == 0)
                {
                    return 0;
                }
                else
                {
                    return Convert.ToInt32(obj);
                }
            }
        }

        /// <summary>
        /// 获取商家对应的奖品统计
        /// </summary>
        /// <param name="shopID"></param>
        /// <returns></returns>
        public DataTable GetAwardTotalList(int shopID)
        {
            const string strSql = @"select 
	                                    case when Type=2 then '免排队' 
	                                    when Type=3 then '赠菜' 
	                                    when Type=4 then '红包'
	                                    when Type=5 then '第三方'
	                                    else '其它' end AwardName,
	                                    Convert(nvarchar(10),LotteryTime,120) getTime,COUNT(type) as Count from dbo.AwardConnPreOrder 
                                    where shopid=@shopID and LotteryTime<GETDATE() 
                                    group by Type,Convert(nvarchar(10),LotteryTime,120)
                                    order by Convert(nvarchar(10),LotteryTime,120) desc,Type asc ";
            SqlParameter[] param ={
                                      new SqlParameter("@ShopID",shopID)
                                  };
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), param);
            return ds.Tables[0];
        }

        /// <summary>
        /// 获取商家月奖品统计
        /// </summary>
        /// <param name="shopID"></param>
        /// <returns></returns>
        public DataTable GetAwardTotalMonthList(int shopID)
        {
            const string strSql = @"select 
	                                    case when Type=2 then '免排队' 
	                                    when Type=3 then '赠菜' 
	                                    when Type=4 then '红包'
	                                    when Type=5 then '第三方'
	                                    else '其它' end AwardName,COUNT(type) as Count from dbo.AwardConnPreOrder 
                                    where shopid=@ShopID and LotteryTime between Convert(varchar(7),GETDATE(),120)+'-01' and Convert(varchar(7),DATEADD(month,1,Getdate()),120)+'-01'
                                    group by Type
                                    order by Type ";
            SqlParameter[] param ={
                                      new SqlParameter("@ShopID",shopID)
                                  };
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), param);
            return ds.Tables[0];
        }

        /// <summary>
        /// 查询某奖项发了多少个
        /// </summary>
        /// <param name="awardId"></param>
        /// <returns></returns>
        public int SelectAwardCount(Guid awardId)
        {
            const string strSql = "select COUNT(1) from AwardConnPreOrder where AwardId=@awardId";
            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@awardId", SqlDbType.UniqueIdentifier) { Value = awardId }
            };

            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                object obj = SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql, para);
                if (obj == null)
                {
                    return 0;
                }
                else
                {
                    return Convert.ToInt32(obj);
                }
            }
        }

        /// <summary>
        /// 统计第三方奖品领取数量
        /// </summary>
        /// <returns></returns>
        public DataTable SelectThirdAwardConsume()
        {
            string strSql = @"select AwardId,COUNT(1) cnt from AwardConnPreOrder
                        where Type = " + (int)AwardType.PresentThirdParty;
            strSql += " and LotteryTime > '" + DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00'";
            strSql += " group by AwardId";
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql);
            return ds.Tables[0];
        }

        /// <summary>
        /// 统计菜品奖品发放数量
        /// </summary>
        /// <returns></returns>
        public DataTable SelectDishAwardConsume(Guid[] awardIds)
        {
            string strSql = "select AwardId,COUNT(1) cnt from AwardConnPreOrder where AwardId in (select d.x.value('./id[1]','uniqueidentifier') from @xml.nodes('/*') as d(x))";
            strSql += " and LotteryTime > '" + DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00'";
            strSql += " group by AwardId";

            var xml = new StringBuilder();
            foreach (var t in awardIds)
            {
                xml.AppendFormat("<row><id>{0}</id></row>", t);
            }

            SqlParameter[] cmdParms = new[]
            {
                new SqlParameter("@xml", SqlDbType.Xml) { Value = xml.ToString() }
            };

            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, cmdParms);
            return ds.Tables[0];
        }

        /// <summary>
        /// 根据点单Id查询中奖信息
        /// </summary>
        /// <param name="preOrder19dianId"></param>
        /// <returns></returns>
        public AwardConnPreOrder SelectAwardConnPreOrderByOrderId(long preOrder19dianId)
        {
            const string strSql = "select * from AwardConnPreOrder where PreOrder19dianId=@preOrder19dianId";
            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@preOrder19dianId", SqlDbType.BigInt) { Value = preOrder19dianId }
            };

            AwardConnPreOrder award = new AwardConnPreOrder();
            using (SqlDataReader sdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, para))
            {
                if (sdr.Read())
                {
                    award = SqlHelper.GetEntity<AwardConnPreOrder>(sdr);
                }
            }
            return award;
        }

        /// <summary>
        /// 查询中奖的订单
        /// </summary>
        /// <param name="preOrder19dianIds"></param>
        /// <returns></returns>
        public List<long> SelectAwardPreOrderByOrderIds(long[] preOrder19dianIds)
        {
            string strSql = "select PreOrder19dianId from AwardConnPreOrder where PreOrder19dianId in (select d.x.value('./id[1]','bigInt') from @xml.nodes('/*') as d(x)) and Type=" + (int)AwardType.AvoidQueue;
            var xml = new StringBuilder();
            foreach (var t in preOrder19dianIds)
            {
                xml.AppendFormat("<row><id>{0}</id></row>", (int)t);
            }

            SqlParameter[] para = new SqlParameter[]{
                new SqlParameter("@xml", SqlDbType.Xml) { Value = xml.ToString() }
            };
            List<long> orderIds = new List<long>();
            using (SqlDataReader sdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, para))
            {
                while (sdr.Read())
                {
                    orderIds.Add(Convert.ToInt64(sdr[0]));
                }
            }
            return orderIds;
        }

        /// <summary>
        /// 查询某用户当日支付的第三方账号
        /// </summary>
        /// <param name="mobilePhone"></param>
        /// <returns></returns>
        public List<string> selectPayAccountOfCustomer(string mobilePhone)
        {
            string strSql = @"select PayAccount from Preorder19DianLine where CustomerId=@mobilePhone
and ISNULL(PayAccount,'')!=''
and CreateTime>'" + DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00'";

            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@mobilePhone", SqlDbType.NVarChar, 20) { Value = mobilePhone }
            };

            List<string> mobiles = new List<string>();
            using (SqlDataReader sdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, para))
            {
                while (sdr.Read())
                {
                    mobiles.Add(sdr[0].ToString());
                }
            }
            return mobiles;
        }

        public int SelectRefundOrderCount(string[] payAccounts)
        {
            string strSql = @"select COUNT(1) from Preorder19DianLine where PayAccount in (select d.x.value('./id[1]','nchar(200)') from @xml.nodes('/*') as d(x))
and ISNULL(PayAccount,'')!=''
and CreateTime>'" + DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00'";

            var xml = new StringBuilder();
            foreach (var t in payAccounts)
            {
                xml.AppendFormat("<row><id>{0}</id></row>", t);
            }

            SqlParameter[] para = new SqlParameter[]{
                new SqlParameter("@xml", SqlDbType.Xml) { Value = xml.ToString() }
            };

            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                object obj = SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql, para);

                if (obj == null)
                {
                    return 0;
                }
                else
                {
                    return Convert.ToInt32(obj);
                }
            }
        }

        /// <summary>
        /// 根据用户ID获取当日支付的第三方支付信息
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public List<string> GetPayAccount(long customerId)
        {
            string strSql = @"select l.PayAccount from PreOrder19dian p inner join Preorder19DianLine l on p.preOrder19dianId=l.Preorder19DianId
where l.customerId = @customerId and CreateTime>'" + DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00'";
            strSql += "and p.isPaid=1 and ISNULL(p.isShopConfirmed,0)=0 and l.PayType in (" + (int)VAOrderUsedPayMode.ALIPAY + "," + (int)VAOrderUsedPayMode.WECHAT + ")";

            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@customerId", SqlDbType.BigInt) { Value = customerId }
            };
            List<string> payAccounts = new List<string>();
            using (SqlDataReader sdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, para))
            {
                while (sdr.Read())
                {
                    payAccounts.Add(sdr[0].ToString());
                }
            }
            return payAccounts;
        }

        /// <summary>
        /// 根据第三方支付信息查询用户在指定门店当日有几个已支付未入座订单
        /// </summary>
        /// <param name="payAccounts"></param>
        /// <returns></returns>
        public int GetUnConfirmedOrderCountOfShop(string[] payAccounts, int shopId)
        {
            string strSql = @"select count(1) from PreOrder19dian p inner join Preorder19DianLine l on p.preOrder19dianId=l.Preorder19DianId
where l.PayAccount in (select d.x.value('./id[1]','nchar(200)') from @xml.nodes('/*') as d(x))
and p.isPaid=1 and ISNULL(p.isShopConfirmed,0)=0 and p.shopId = @shopId
and l.PayType in (" + (int)VAOrderUsedPayMode.ALIPAY + "," + (int)VAOrderUsedPayMode.WECHAT + ") and CreateTime>'" + DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00'";
            var xml = new StringBuilder();
            foreach (var t in payAccounts)
            {
                xml.AppendFormat("<row><id>{0}</id></row>", t);
            }

            SqlParameter[] para = new SqlParameter[]{
                new SqlParameter("@xml", SqlDbType.Xml) { Value = xml.ToString() },
                new SqlParameter("@shopId", SqlDbType.Int) { Value = shopId }
            };

            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                object obj = SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql, para);

                if (obj == null)
                {
                    return 0;
                }
                else
                {
                    return Convert.ToInt32(obj);
                }
            }
        }

        /// <summary>
        /// 根据第三方支付信息查询当日有几个已支付未入座订单
        /// </summary>
        /// <param name="payAccounts"></param>
        /// <returns></returns>
        public int GetUnConfirmedOrderCountOfVA(string[] payAccounts)
        {
            string strSql = @"select count(1) from PreOrder19dian p inner join Preorder19DianLine l on p.preOrder19dianId=l.Preorder19DianId
where l.PayAccount in (select d.x.value('./id[1]','nchar(200)') from @xml.nodes('/*') as d(x))
and p.isPaid=1 and ISNULL(p.isShopConfirmed,0)=0 and status not in (" + (int)VAPreorderStatus.OriginalRefunding + "," + (int)VAPreorderStatus.Refund + ")";
            strSql += " and l.PayType in (" + (int)VAOrderUsedPayMode.ALIPAY + "," + (int)VAOrderUsedPayMode.WECHAT + ") and CreateTime>'" + DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00'";

            var xml = new StringBuilder();
            foreach (var t in payAccounts)
            {
                xml.AppendFormat("<row><id>{0}</id></row>", t);
            }

            SqlParameter[] para = new SqlParameter[]{
                new SqlParameter("@xml", SqlDbType.Xml) { Value = xml.ToString() }
            };

            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                object obj = SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql, para);

                if (obj == null)
                {
                    return 0;
                }
                else
                {
                    return Convert.ToInt32(obj);
                }
            }
        }
    }
}
