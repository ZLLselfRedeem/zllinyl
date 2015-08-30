using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.Model;
using System.Data.SqlClient;
using System.Data;
using VAGastronomistMobileApp.DBUtility;

namespace VAGastronomistMobileApp.SQLServerDAL
{
    public class ShopAwardManager
    {
        public bool InsertShopAward(ShopAward shopAward)
        {
            const string strSql = @"Insert into ShopAward(Id,ShopId,AwardType,DishId,DishPriceId,Name,Count,SubsidyAmount,Probability,Enable,Status,CreateTime,CreatedBy)
            Values(@Id,@ShopId,@AwardType,@DishId,@DishPriceId,@Name,@Count,
            @SubsidyAmount,@Probability,@Enable,@Status,@CreateTime,@CreatedBy)";

            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@Id", SqlDbType.UniqueIdentifier){ Value = shopAward.Id },
            new SqlParameter("@ShopId", SqlDbType.Int){ Value = shopAward.ShopId },
            new SqlParameter("@AwardType", SqlDbType.Int){ Value = shopAward.AwardType },
            new SqlParameter("@DishId", SqlDbType.Int){ Value = shopAward.DishId },
            new SqlParameter("@DishPriceId", SqlDbType.Int){ Value = shopAward.DishPriceId },
            new SqlParameter("@Name", SqlDbType.NVarChar,30){ Value = shopAward.Name },
            new SqlParameter("@Count", SqlDbType.Int){ Value = shopAward.Count },
            new SqlParameter("@SubsidyAmount", SqlDbType.Money){ Value = shopAward.SubsidyAmount },
            new SqlParameter("@Probability", SqlDbType.Int){ Value = shopAward.Probability },
            new SqlParameter("@Enable", SqlDbType.Bit){ Value = shopAward.Enable },
            new SqlParameter("@Status", SqlDbType.Bit){ Value = shopAward.Status },
            new SqlParameter("@CreateTime", SqlDbType.DateTime){ Value = shopAward.CreateTime },
            new SqlParameter("@CreatedBy", SqlDbType.NVarChar, 30){ Value = shopAward.CreatedBy }
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

        public bool UpdateShopAward(ShopAward shopAward)
        {
            const string strSql = @"update shopAward set 
            ShopId=@shopId,AwardType=@AwardType,DishId=@DishId,DishPriceId=@DishPriceId,Name=@Name,
            Count=@Count,SubsidyAmount=@SubsidyAmount,Probability=@Probability,Enable=@Enable,Status=@Status,
            LastUpdateTime=@LastUpdateTime,LastUpdatedBy=@LastUpdatedBy 
            where Id=@Id";

            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@ShopId", SqlDbType.Int){ Value = shopAward.ShopId },
            new SqlParameter("@AwardType", SqlDbType.Int){ Value = shopAward.AwardType },
            new SqlParameter("@DishId", SqlDbType.Int){ Value = shopAward.DishId },
            new SqlParameter("@DishPriceId",SqlDbType.Int){Value=shopAward.DishPriceId},
            new SqlParameter("@Name", SqlDbType.NVarChar,30){ Value = shopAward.Name },
            new SqlParameter("@Count", SqlDbType.Int){ Value = shopAward.Count },
            new SqlParameter("@SubsidyAmount", SqlDbType.Money){ Value = shopAward.SubsidyAmount },
            new SqlParameter("@Probability", SqlDbType.Int){ Value = shopAward.Probability },
            new SqlParameter("@Enable", SqlDbType.Bit){ Value = shopAward.Enable },
            new SqlParameter("@Status", SqlDbType.Bit){ Value = shopAward.Status },
            new SqlParameter("@LastUpdateTime", SqlDbType.DateTime){ Value = shopAward.LastUpdateTime },
            new SqlParameter("@LastUpdatedBy", SqlDbType.NVarChar, 30){ Value = shopAward.LastUpdatedBy },            
            new SqlParameter("@Id", SqlDbType.UniqueIdentifier){ Value = shopAward.Id }
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
        /// 停用某个奖品
        /// </summary>
        /// <param name="awardId"></param>
        /// <returns></returns>
        public bool DisabledShopAward(Guid awardId)
        {
            const string strSql = "update ShopAward set Enable=0 where Id=@awardId";
            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@awardId", SqlDbType.UniqueIdentifier) { Value = awardId }
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
        public List<ShopAward> SelectShopAwardList(int shopID,int type)
        {
            string strSql = "select * from ShopAward where ShopId=@ShopID and Enable=1 and Status=1  order by AwardType";
            if(type==2)
            {
                strSql = "select * from ShopAward where ShopId=@ShopID and Status=1  order by AwardType";
            }
            SqlParameter[] param ={
                                      new SqlParameter("@ShopID",shopID)
                                  };
            List<ShopAward> shopAwardList = new List<ShopAward>();
            using (SqlDataReader sdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), param))
            {
                while (sdr.Read())
                {
                    shopAwardList.Add(SqlHelper.GetEntity<ShopAward>(sdr));
                }
            }
            return shopAwardList;
        }

        /// <summary>
        /// 查询指定门店指定类别的奖品信息
        /// </summary>
        /// <param name="shopId"></param>
        /// <param name="awardType"></param>
        /// <returns></returns>
        public List<ShopAward> SelectShopAwardList(int shopId, AwardType awardType)
        {
            const string strSql = "select * from ShopAward where ShopId=@shopId and AwardType =@awardType and status=1 and Enable=1";
            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@shopId", SqlDbType.Int) { Value = shopId },
            new SqlParameter("@awardType", SqlDbType.Int) { Value = (int)awardType }
            };
            List<ShopAward> shopAwards = new List<ShopAward>();
            using (SqlDataReader sdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, para))
            {
                while (sdr.Read())
                {
                    shopAwards.Add(SqlHelper.GetEntity<ShopAward>(sdr));
                }
            }
            return shopAwards;
        }

        /// <summary>
        /// 查询门店里某个具体的奖品
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ShopAward QueryShopAward(Guid Id)
        {
            const string strSql = "select * from ShopAward where Status=1 and Id=@Id";
            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@Id", SqlDbType.UniqueIdentifier) { Value = Id }
            };
            ShopAward shopAward = new ShopAward();
            using (SqlDataReader sdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, para))
            {
                if (sdr.Read())
                {
                    shopAward = SqlHelper.GetEntity<ShopAward>(sdr);
                }
            }
            return shopAward;
        }

        public ShopAward QueryAllShopAward(Guid Id)
        {
            const string strSql = "select * from ShopAward where Id=@Id";
            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@Id", SqlDbType.UniqueIdentifier) { Value = Id }
            };
            ShopAward shopAward = new ShopAward();
            using (SqlDataReader sdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, para))
            {
                if (sdr.Read())
                {
                    shopAward = SqlHelper.GetEntity<ShopAward>(sdr);
                }
            }
            return shopAward;
        }

        public List<ShopAward> SelectShopAwardList(string awardIDS)
        {
            string strSql = "select * from ShopAward where Id in ({0}) and Enable=1 and Status=1";
            strSql = string.Format(strSql, awardIDS);
            List<ShopAward> shopAwardList = new List<ShopAward>();
            if (awardIDS == "'")
            {
                return shopAwardList;
            }
            using (SqlDataReader sdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), null))
            {
                while (sdr.Read())
                {
                    shopAwardList.Add(SqlHelper.GetEntity<ShopAward>(sdr));
                }
            }
            return shopAwardList;
        }

        /// <summary>
        /// 根据菜品ID获取菜品名称
        /// </summary>
        /// <param name="dishID"></param>
        /// <returns></returns>
        public string GetDishNameI18nID(int dishID)
        {
            string val = "";
            string strsql = @"select top 1 DishName from DishI18n where DishID=@DishID";
            SqlParameter[] parm = new[] {
                   new SqlParameter("@DishID",dishID )
                    };
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strsql, parm))
            {
                if (dr.Read())
                {
                    val = dr.GetString(0);
                }
            }
            return val;
        }

        /// <summary>
        /// 获取DISHID
        /// </summary>
        /// <param name="dishI18NID"></param>
        /// <returns></returns>
        public int GetDishID(int dishI18NID)
        {
            int val = 0;
            string strsql = @"select DishID from DishI18n where DishI18nID=@dishI18NID";
            SqlParameter[] parm = new[] {
                   new SqlParameter("@dishI18NID",dishI18NID )
                    };
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strsql, parm))
            {
                if (dr.Read())
                {
                    val = dr.GetInt32(0);
                }
            }
            return val;
        }

        /// <summary>
        /// 开通或者关闭商家的抽奖功能
        /// </summary>
        /// <param name="shopID">商家ID</param>
        /// <returns></returns>
        public bool UpdateShopAwardEnable(int shopID, int enable, int type = 0)
        {
            string strSql = @"update shopAward set Enable=@Enable where ShopID=@ShopID and enable=1";
            if (enable == 0)
            {
                strSql = @"update shopAward set Enable=@Enable where ShopID=@ShopID";
            }
            if (type == 3)
            {
                strSql += " and AwardType=3 ";
            }
            SqlParameter[] para = new SqlParameter[] { 
                new SqlParameter("@Enable",SqlDbType.Bit){Value=enable},
                new SqlParameter("@ShopId", SqlDbType.Int){ Value = shopID }
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

        public bool UpdateShopAwardEnable(int shopID)
        {
            string strSql = @"update shopAward set Enable=1 where ShopID=@ShopID  and AwardType=3 ";
            SqlParameter[] para = new SqlParameter[] { 
                new SqlParameter("@ShopId", SqlDbType.Int){ Value = shopID }
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
        /// 获取菜品 图片地址
        /// </summary>
        /// <param name="shopID"></param>
        /// <param name="dishID"></param>
        /// <returns></returns>
        public string GetDishPicUrl(int shopID, int dishID)
        {
            string val = "";
            string strsql = @"select m.menuImagePath+img.ImageName dishPicPath
                                from ImageInfo img
                                inner join DishInfo d on img.DishID=d.DishID
                                inner join MenuInfo m on d.MenuID = m.MenuID
                                inner join MenuConnShop conn on m.MenuID=conn.menuId and conn.shopId=@shopID
                                and img.ImageScale=0 and ImageStatus=1
                                and d.DishID=@dishID";
            SqlParameter[] parm = new[] {
                    new SqlParameter("@shopID",shopID ),
                   new SqlParameter("@dishID",dishID )
                    };
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strsql, parm))
            {
                if (dr.Read())
                {
                    val = dr.GetString(0);
                }
            }
            return val;
        }

        /// <summary>
        /// 商户活动查询 
        /// </summary>
        /// <param name="cityID"></param>
        /// <param name="shopName"></param>
        /// <param name="changeStatus"></param>
        /// <param name="beginDateTimeValue"></param>
        /// <param name="endDateTimeValue"></param>
        /// <returns></returns>
        public DataTable SelectBussinessActivity(string cityID, string shopName, string changeStatus, DateTime beginDateTimeValue, DateTime endDateTimeValue, string shopID)
        {
            string strSql = @"select 
			                           a.ShopID,
			                           c.companyName,
			                           s.shopName,
			                           (select Convert(varchar(5),isnull(Count(*),0)) awardCount from ShopAward where ShopId=a.ShopId and Enable=1 and Status=1) awardCount,
			                           (select Convert(varchar(5),ISNULL(count(*),0)) avoidQueueCount from ShopAward where ShopId=a.ShopID and Enable=1 and AwardType=2 and Status=1) avoidQueueCount,
			                           (select Convert(varchar(5),ISNULL(count(*),0)) presentDishCount from ShopAward where ShopId=a.ShopID and Enable=1 and AwardType=3 and Status=1) presentDishCount,
			                           (select Convert(varchar(5),ISNULL(count(*),0)) shopRedCount from ShopConnRedEnvelope where ShopId=a.ShopID and Status=1) shopRedCount
			                           from ShopAward a
                        left join VAGastronomistMobileApp.dbo.ShopInfo s on a.ShopId=s.shopID
                        left join VAGastronomistMobileApp.dbo.CompanyInfo c on s.companyID=c.companyID
                        left join ShopAwardVersion v on a.ShopId=v.ShopId
                        where 1=1 ";

            // 查询城市下的门店
            if (!string.IsNullOrEmpty(cityID))
            {
                strSql += string.Format(" and  s.cityID={0}", cityID);
            }
            // 查询门店的shopID
            if (!string.IsNullOrEmpty(shopName))
            {
                strSql += string.Format(" and  s.shopName like '%{0}%'", shopName);
            }
            // 变更状态
            if (!string.IsNullOrEmpty(changeStatus))
            {
                // 发生变更
                if (changeStatus == "1")
                {
                    strSql += " and v.ShopId is not null";
                }
                else
                {
                    strSql += " and v.ShopId is null";
                }
            }
            // 变更时间
            if (beginDateTimeValue != null && beginDateTimeValue != System.DateTime.MinValue)
            {
                strSql += string.Format(" and CreateTime >='{0}'", beginDateTimeValue);
            }
            if (endDateTimeValue != null && endDateTimeValue != System.DateTime.MinValue)
            {
                strSql += string.Format(" and CreateTime <='{0}'", endDateTimeValue);
            }

            // 商家shopID
            if (!string.IsNullOrEmpty(shopID))
            {
                strSql += string.Format(" and a.ShopId={0}", shopID);
            }
            strSql += " group by a.ShopId,c.companyName,s.shopName ";

            DataTable dt = new DataTable();
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, null);
            dt = ds.Tables[0];
            return dt;
        }

        /// <summary>
        /// 统计近三天的商户活动统计
        /// </summary>
        /// <param name="cityID"></param>
        /// <param name="shopName"></param>
        /// <param name="beginDateTimeValue"></param>
        /// <param name="endDateTimeValue"></param>
        /// <param name="shopID"></param>
        /// <returns></returns>
        public DataTable SelectBussinessActivityTotal(string cityID, string shopName, DateTime beginDateTimeValue, DateTime endDateTimeValue, string shopID, int type)
        {
            string strSql = @"SELECT c.companyname,
                                       s.shopname,
                                       a.*,
                                       (ordercount - avoidqueuecount - presentdishcount - redcount - thirdcount) noawardcount
                                FROM   (SELECT   a.shopid,
                                                 Isnull(Sum(a.prepaidsum - Isnull(a.refundmoneysum,0)),
                                                        0) ordermoney,
                                                 Count(a.shopid) AS ordercount,
                                                 (SELECT Isnull(Count(shopid),0) avoidqueuecount
                                                  FROM   vaaward.dbo.awardconnpreorder
                                                  WHERE  shopid = a.shopid
                                                         AND TYPE = 2
                                                         AND status = 1) avoidqueuecount,
                                                 (SELECT Isnull(Count(shopid),0) avoidqueuecount
                                                  FROM   vaaward.dbo.awardconnpreorder
                                                  WHERE  shopid = a.shopid
                                                         AND TYPE = 3
                                                         AND status = 1) presentdishcount,
                                                 (SELECT Isnull(Count(shopid),0) avoidqueuecount
                                                  FROM   vaaward.dbo.awardconnpreorder
                                                  WHERE  shopid = a.shopid
                                                         AND TYPE = 4
                                                         AND status = 1) redcount,
                                                 (SELECT Isnull(Count(shopid),0) avoidqueuecount
                                                  FROM   vaaward.dbo.awardconnpreorder
                                                  WHERE  shopid = a.shopid
                                                         AND TYPE = 5
                                                         AND status = 1) thirdcount
                                        FROM     preorder19dian a
                                        left join vaaward.dbo.awardconnpreorder b on a.preOrder19dianId=b.PreOrder19dianId
                                        WHERE    a.prepaidsum > Isnull(a.refundmoneysum,0)
                                                 AND a.ispaid = 1 ";
            if (type == 2)
            {
                strSql = @"SELECT Sum(ordermoney) ordermoneysum,
                                   Sum(ordercount) ordercountsum,
                                   Sum(avoidqueuecount) avoidqueuecountsum,
                                   Sum(presentdishcount) presentdishcountsum,
                                   Sum(redcount) redcountsum,
                                   Sum(thirdcount) thirdcountsum,
                                   Sum((ordercount - avoidqueuecount - presentdishcount - redcount - thirdcount)) noawardcount
                            FROM   (SELECT   a.shopid,
                                             Isnull(Sum(a.prepaidsum - Isnull(a.refundmoneysum,0)),
                                                    0) ordermoney,
                                             Count(a.shopid) AS ordercount,
                                             (SELECT Isnull(Count(shopid),0) avoidqueuecount
                                              FROM   vaaward.dbo.awardconnpreorder
                                              WHERE  shopid = a.shopid
                                                     AND TYPE = 2
                                                     AND status = 1) avoidqueuecount,
                                             (SELECT Isnull(Count(shopid),0) avoidqueuecount
                                              FROM   vaaward.dbo.awardconnpreorder
                                              WHERE  shopid = a.shopid
                                                     AND TYPE = 3
                                                     AND status = 1) presentdishcount,
                                             (SELECT Isnull(Count(shopid),0) avoidqueuecount
                                              FROM   vaaward.dbo.awardconnpreorder
                                              WHERE  shopid = a.shopid
                                                     AND TYPE = 4
                                                     AND status = 1) redcount,
                                             (SELECT Isnull(Count(shopid),0) avoidqueuecount
                                              FROM   vaaward.dbo.awardconnpreorder
                                              WHERE  shopid = a.shopid
                                                     AND TYPE = 5
                                                     AND status = 1) thirdcount
                                    FROM     preorder19dian a
                                    left join vaaward.dbo.awardconnpreorder b on a.preOrder19dianId=b.PreOrder19dianId
                                    WHERE    a.prepaidsum > Isnull(a.refundmoneysum,0)
                                             AND a.ispaid = 1 ";
            }

            // 变更时间
            if (beginDateTimeValue != null && beginDateTimeValue != System.DateTime.MinValue)
            {
                strSql += string.Format(" and b.LotteryTime >='{0}'", beginDateTimeValue);
            }
            if (endDateTimeValue != null && endDateTimeValue != System.DateTime.MinValue)
            {
                strSql += string.Format(" and b.LotteryTime <='{0}'", endDateTimeValue);
            }

            strSql += @"  GROUP BY a.shopid) a
                                       LEFT JOIN shopinfo s
                                         ON a.shopid = s.shopid
                                       LEFT JOIN companyinfo c
                                         ON s.companyid = c.companyid  where 1=1 ";

            // 查询城市下的门店
            if (!string.IsNullOrEmpty(cityID))
            {
                strSql += string.Format(" and  s.cityID={0}", cityID);
            }
            // 查询门店的shopID
            if (!string.IsNullOrEmpty(shopName))
            {
                strSql += string.Format(" and  s.shopName like '%{0}%'", shopName);
            }
            

            // 商家shopID
            if (!string.IsNullOrEmpty(shopID))
            {
                strSql += string.Format(" and a.ShopId={0}", shopID);
            }
            
            DataTable dt = new DataTable();
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, null);
            dt = ds.Tables[0];
            return dt;
        }

        /// <summary>
        /// 删除店铺奖品
        /// </summary>
        /// <param name="awardID"></param>
        /// <returns></returns>
        public bool DeleteShopAward(Guid awardID)
        {
            //const string strSql = @"delete ShopAward where Id=@Id";
            const string strSql = @"update ShopAward set Status=0 where Id=@Id ";
            SqlParameter[] para = new SqlParameter[] { 
                new SqlParameter("@Id",SqlDbType.UniqueIdentifier){Value=awardID}
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
        /// 获取商家对应的奖品类别
        /// </summary>
        /// <param name="shopID"></param>
        /// <returns></returns>
        public bool SelectShopAwardType(int shopID, AwardType awardType)
        {
            const string strSql = "select 1 from ShopAward where ShopId=@ShopID and awardType=@awardType and Enable=1 and Status=1";
            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@ShopID", SqlDbType.Int) { Value = shopID },
            new SqlParameter("@awardType", SqlDbType.Int) { Value = (int)awardType }
            };

            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                object obj = SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), para);
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
    }
}
