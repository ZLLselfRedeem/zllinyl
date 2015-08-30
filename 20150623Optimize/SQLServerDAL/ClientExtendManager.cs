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
    public class ClientExtendManager
    {
        /// <summary>
        /// 转换对象模型
        /// </summary>
        public List<VAIndexExt> GetClientIndexAllDataByCity(int cityId)
        {
            List<VAIndexExt> list = new List<VAIndexExt>();
            const string fields = @" S.prepayOrderCount,S.shopID,S.shopName,S.shopLogo,C.defaultMenuId,S.shopAddress,S.shopLevel,S.isSupportPayment,H.EvaluationCount,
S.shopImagePath,S.prePayVIPCount,S.orderDishDesc,S.shopRating,S.publicityPhotoPath,S.acpp,S.isSupportAccountsRound,S1.longitude,S1.latitude,M2.menuId ";
            const string tableName = @" CompanyInfo C with(nolock) inner join ShopInfo S with(nolock) on C.companyId=S.companyId inner join ShopCoordinate S1 with(nolock) on S.shopID = S1.shopId inner join MapInfo M1 with(nolock) on S1.mapId = M1.mapId
inner join MenuConnShop M2 with(nolock) on S.shopID=M2.shopId left join MenuInfo M3 with(nolock) on M2.menuId=M3.MenuID 
 left join ShopEvaluationDetail H on H.ShopId=S.shopID and H.EvaluationValue=1 ";
            string strWhere = String.Format(@" C.companyStatus=1 and S.isHandle=1 and S.shopStatus=1 and M3.MenuStatus=1 and S1.mapId = 2 and S.cityID ={0}", cityId);

            PageQuery pageQuery = new PageQuery()
            {
                tableName = tableName,
                fields = fields,
                orderField = "S.shopID desc",
                sqlWhere = strWhere.ToString()
            };
            Paging paging = new Paging()
            {
                pageIndex = 1,
                pageSize = 100000,
                recordCount = 0,
                pageCount = 0
            };
            SqlConnection connection = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction);
            if (connection.State != ConnectionState.Open) connection.Open();
            SqlCommand comm = new SqlCommand();
            comm.Connection = connection;
            comm.CommandType = CommandType.StoredProcedure;
            comm.CommandText = "CommonPageProc";
            comm.Parameters.Add("@TableName", SqlDbType.VarChar, 5000).Value = pageQuery.tableName;
            comm.Parameters.Add("@Fields", SqlDbType.VarChar, 5000).Value = pageQuery.fields;
            comm.Parameters.Add("@OrderField", SqlDbType.VarChar, 5000).Value = pageQuery.orderField;
            comm.Parameters.Add("@sqlWhere", SqlDbType.VarChar, 5000).Value = pageQuery.sqlWhere;
            comm.Parameters.Add("@pageSize", SqlDbType.Int, 16).Value = paging.pageSize;
            comm.Parameters.Add("@pageIndex", SqlDbType.Int, 16).Value = paging.pageIndex;
            SqlParameter paraTP = new SqlParameter("@TotalPage", SqlDbType.Int);
            paraTP.Direction = ParameterDirection.Output;
            paraTP.Value = paging.pageCount;
            comm.Parameters.Add(paraTP);
            SqlParameter param = new SqlParameter("@TotalRecord", SqlDbType.Int);
            param.Direction = ParameterDirection.ReturnValue;
            comm.Parameters.Add(param);
            using (IDataReader dr = comm.ExecuteReader(CommandBehavior.CloseConnection))
            {
                while (dr.Read())
                {
                    list.Add(new VAIndexExt()
                    {
                        prepayOrderCount = dr["prepayOrderCount"] == DBNull.Value ? 0 : Convert.ToInt32(dr["prepayOrderCount"]),
                        shopID = dr["shopID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["shopID"]),
                        shopName = dr["shopName"] == DBNull.Value ? "未知门店" : Convert.ToString(dr["shopName"]),
                        shopLogo = dr["shopLogo"] == DBNull.Value ? "" : Convert.ToString(dr["shopLogo"]),
                        defaultMenuId = dr["defaultMenuId"] == DBNull.Value ? 0 : Convert.ToInt32(dr["defaultMenuId"]),
                        shopImagePath = dr["shopImagePath"] == DBNull.Value ? "" : Convert.ToString(dr["shopImagePath"]),
                        prePayVIPCount = dr["prePayVIPCount"] == DBNull.Value ? 0 : Convert.ToInt32(dr["prePayVIPCount"]),
                        orderDishDesc = dr["orderDishDesc"] == DBNull.Value ? "" : Convert.ToString(dr["orderDishDesc"]),
                        shopRating = dr["shopRating"] == DBNull.Value ? 0 : Convert.ToDouble(dr["shopRating"]),
                        publicityPhotoPath = dr["publicityPhotoPath"] == DBNull.Value ? "" : Convert.ToString(dr["publicityPhotoPath"]),
                        acpp = dr["acpp"] == DBNull.Value ? 0 : Convert.ToDouble(dr["acpp"]),
                        isSupportAccountsRound = dr["isSupportAccountsRound"] == DBNull.Value ? false : Convert.ToBoolean(dr["isSupportAccountsRound"]),
                        longitude = dr["longitude"] == DBNull.Value ? 0 : Convert.ToDouble(dr["longitude"]),
                        latitude = dr["latitude"] == DBNull.Value ? 0 : Convert.ToDouble(dr["latitude"]),
                        menuId = dr["menuId"] == DBNull.Value ? 0 : Convert.ToInt32(dr["menuId"]),
                        shopAddress = dr["shopAddress"] == DBNull.Value ? "" : Convert.ToString(dr["shopAddress"]),
                        shopLevel = dr["shopLevel"] == DBNull.Value ? 0 : Convert.ToInt32(dr["shopLevel"]),
                        isSupportPayment = dr["isSupportPayment"] == DBNull.Value ? false : Convert.ToBoolean(dr["isSupportPayment"]),
                        goodEvaluationCount = dr["EvaluationCount"] == DBNull.Value ? 0 : Convert.ToInt32(dr["EvaluationCount"])
                    });
                }
            }
            paging.pageCount = comm.Parameters["@TotalPage"].Value == DBNull.Value ? 0 : Convert.ToInt32(comm.Parameters["@TotalPage"].Value);
            paging.recordCount = param.Value == DBNull.Value ? 0 : Convert.ToInt32(param.Value);
            comm.Parameters.Clear();
            return list;
        }

        //        public List<VAIndexExt> TransformData(int cityId, double userLongtitude, double userLatitude, int shopId,
        //         int pageIndex, int pageSize, long customerId, int dataType, string shopIds, int tagId, ref int dataCount, string searchKeyWord = "")
        //        {
        //            List<VAIndexExt> list = new List<VAIndexExt>();
        //            const string fields = @" S.prepayOrderCount,S.shopID,S.shopName,S.shopLogo,C.defaultMenuId,S.shopAddress,
        //S.shopImagePath,S.prePayVIPCount,S.orderDishDesc,S.shopRating,S.publicityPhotoPath,S.acpp,S.isSupportAccountsRound,S1.longitude,S1.latitude,M2.menuId ";
        //            string tableName = @" CompanyInfo C with(nolock) inner join ShopInfo S with(nolock) on C.companyId=S.companyId inner join ShopCoordinate S1 with(nolock) on S.shopID = S1.shopId inner join MapInfo M1 with(nolock) on S1.mapId = M1.mapId
        //inner join MenuConnShop M2 with(nolock) on S.shopID=M2.shopId left join MenuInfo M3 with(nolock) on M2.menuId=M3.MenuID ";
        //            switch (dataType)
        //            {
        //                case (int)VAIndexSorting.关注的店:
        //                    tableName += String.Format(@"inner join CustomerFavoriteCompany CC with(nolock) on CC.shopId=S.shopID
        // and S.shopID in (select isnull(CustomerFavoriteCompany.shopId,0) from CustomerFavoriteCompany
        //  where CustomerFavoriteCompany.customerId={0} group by CustomerFavoriteCompany.shopId)", customerId);//门店收藏表
        //                    break;
        //                case (int)VAIndexSorting.我看过的:
        //                    tableName += String.Format(@"inner join CustomerCheckedShop CC with(nolock) on CC.shopId=S.shopID and 
        // S.shopID in (select temp.shopId from (select distinct top 20 shopId,MAX(checkTime) checkTime 
        // from CustomerCheckedShop where customerId={0} and cityId={1} group by shopId order by MAX(checkTime) desc) temp)", customerId, cityId);//门店查看表
        //                    break;
        //                case (int)VAIndexSorting.我吃过的:
        //                    tableName += @" inner join PreOrder19dian A with(nolock) on A.shopId=S.shopID and A.isPaid=1 and A.customerId=" + customerId;//点单表
        //                    break;
        //                default:
        //                    break;
        //            }
        //            StringBuilder strWhere = new StringBuilder();
        //            strWhere.AppendFormat(" C.companyStatus=1 and S.isHandle=1 and S.shopStatus=1 and M3.MenuStatus=1 and S1.mapId = 2 and S.cityID ={0}", cityId);
        //            if (tagId > 1 && !String.IsNullOrWhiteSpace(shopIds))
        //            {
        //                strWhere.AppendFormat(" and S.shopID in " + shopIds);
        //            }
        //            if (shopId > 0)
        //            {
        //                strWhere.AppendFormat("and S.shopID = {0} ", shopId);
        //            }
        //            if (!string.IsNullOrEmpty(searchKeyWord.Trim()))
        //            {
        //                strWhere.AppendFormat("and S.shopName like '%{0}%' ", searchKeyWord);//客户端查询
        //            }
        //            string initOrderByStr = string.Empty;
        //            switch (dataType)
        //            {
        //                case (int)VAIndexSorting.我看过的:
        //                    strWhere.AppendFormat(@"group by S.prepayOrderCount,S.shopID,S.shopName,S.shopLogo,C.defaultMenuId,S.shopAddress,
        //S.shopImagePath,S.prePayVIPCount,S.orderDishDesc,S.shopRating,S.publicityPhotoPath,S.acpp,S.isSupportAccountsRound,S1.longitude,S1.latitude,M2.menuId ");
        //                    initOrderByStr = "MAX(CC.checkTime) desc";
        //                    break;
        //                case (int)VAIndexSorting.关注的店:
        //                    strWhere.AppendFormat(@"group by S.prepayOrderCount,S.shopID,S.shopName,S.shopLogo,C.defaultMenuId,S.shopAddress,
        //S.shopImagePath,S.prePayVIPCount,S.orderDishDesc,S.shopRating,S.publicityPhotoPath,S.acpp,S.isSupportAccountsRound,S1.longitude,S1.latitude,M2.menuId ");
        //                    initOrderByStr = "MAX(CC.collectTime) desc";
        //                    break;
        //                case (int)VAIndexSorting.我吃过的:
        //                    strWhere.AppendFormat(@"group by S.prepayOrderCount,S.shopID,S.shopName,S.shopLogo,C.defaultMenuId,S.shopAddress,
        //S.shopImagePath,S.prePayVIPCount,S.orderDishDesc,S.shopRating,S.publicityPhotoPath,S.acpp,S.isSupportAccountsRound,S1.longitude,S1.latitude,M2.menuId ");
        //                    initOrderByStr = " MAX(A.prePayTime) desc";
        //                    break;
        //                case 2://距离排序
        //                    initOrderByStr = " [dbo].[fnGetDistance](S1.latitude,S1.longitude," + userLatitude + "," + userLongtitude + ")";
        //                    break;
        //                default:
        //                case 3://销量排序
        //                    initOrderByStr = "S.acpp desc";//定位失败按照销量排序
        //                    break;
        //            }
        //            PageQuery pageQuery = new PageQuery()
        //            {
        //                tableName = tableName,
        //                fields = fields,
        //                orderField = initOrderByStr,//排序
        //                sqlWhere = strWhere.ToString()
        //            };
        //            if (!string.IsNullOrEmpty(searchKeyWord.Trim())) pageIndex = 1;//表示如果是查询的就默认返回第一页
        //            Paging paging = new Paging()
        //            {
        //                pageIndex = pageIndex,
        //                pageSize = pageSize,
        //                recordCount = 0,
        //                pageCount = 0
        //            };
        //            SqlConnection connection = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction);
        //            if (connection.State != ConnectionState.Open) connection.Open();
        //            SqlCommand comm = new SqlCommand();
        //            comm.Connection = connection;
        //            comm.CommandType = CommandType.StoredProcedure;
        //            comm.CommandText = "CommonPageProc";
        //            comm.Parameters.Add("@TableName", SqlDbType.VarChar, 5000).Value = pageQuery.tableName;
        //            comm.Parameters.Add("@Fields", SqlDbType.VarChar, 5000).Value = pageQuery.fields;
        //            comm.Parameters.Add("@OrderField", SqlDbType.VarChar, 5000).Value = pageQuery.orderField;
        //            comm.Parameters.Add("@sqlWhere", SqlDbType.VarChar, 5000).Value = pageQuery.sqlWhere;
        //            comm.Parameters.Add("@pageSize", SqlDbType.Int, 16).Value = paging.pageSize;
        //            comm.Parameters.Add("@pageIndex", SqlDbType.Int, 16).Value = paging.pageIndex;
        //            SqlParameter paraTP = new SqlParameter("@TotalPage", SqlDbType.Int);
        //            paraTP.Direction = ParameterDirection.Output;
        //            paraTP.Value = paging.pageCount;
        //            comm.Parameters.Add(paraTP);
        //            SqlParameter param = new SqlParameter("@TotalRecord", SqlDbType.Int);
        //            param.Direction = ParameterDirection.ReturnValue;
        //            comm.Parameters.Add(param);
        //            using (IDataReader dr = comm.ExecuteReader(CommandBehavior.CloseConnection))
        //            {
        //                while (dr.Read())
        //                {
        //                    list.Add(new VAIndexExt()
        //                    {
        //                        prepayOrderCount = dr["prepayOrderCount"] == DBNull.Value ? 0 : Convert.ToInt32(dr["prepayOrderCount"]),
        //                        shopID = dr["shopID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["shopID"]),
        //                        shopName = dr["shopName"] == DBNull.Value ? "未知门店" : Convert.ToString(dr["shopName"]),
        //                        shopLogo = dr["shopLogo"] == DBNull.Value ? "" : Convert.ToString(dr["shopLogo"]),
        //                        defaultMenuId = dr["defaultMenuId"] == DBNull.Value ? 0 : Convert.ToInt32(dr["defaultMenuId"]),
        //                        shopImagePath = dr["shopImagePath"] == DBNull.Value ? "" : Convert.ToString(dr["shopImagePath"]),
        //                        prePayVIPCount = dr["prePayVIPCount"] == DBNull.Value ? 0 : Convert.ToInt32(dr["prePayVIPCount"]),
        //                        orderDishDesc = dr["orderDishDesc"] == DBNull.Value ? "未知门店" : Convert.ToString(dr["orderDishDesc"]),
        //                        shopRating = dr["shopRating"] == DBNull.Value ? 0 : Convert.ToDouble(dr["shopRating"]),
        //                        publicityPhotoPath = dr["publicityPhotoPath"] == DBNull.Value ? "" : Convert.ToString(dr["publicityPhotoPath"]),
        //                        acpp = dr["acpp"] == DBNull.Value ? 0 : Convert.ToDouble(dr["acpp"]),
        //                        isSupportAccountsRound = dr["isSupportAccountsRound"] == DBNull.Value ? false : Convert.ToBoolean(dr["isSupportAccountsRound"]),
        //                        longitude = dr["longitude"] == DBNull.Value ? 0 : Convert.ToDouble(dr["longitude"]),
        //                        latitude = dr["latitude"] == DBNull.Value ? 0 : Convert.ToDouble(dr["latitude"]),
        //                        menuId = dr["menuId"] == DBNull.Value ? 0 : Convert.ToInt32(dr["menuId"]),
        //                        shopAddress = dr["shopAddress"] == DBNull.Value ? "" : Convert.ToString(dr["shopAddress"])
        //                    });
        //                }
        //            }
        //            paging.pageCount = comm.Parameters["@TotalPage"].Value == DBNull.Value ? 0 : Convert.ToInt32(comm.Parameters["@TotalPage"].Value);
        //            paging.recordCount = param.Value == DBNull.Value ? 0 : Convert.ToInt32(param.Value);
        //            dataCount = dataType == (int)VAIndexSorting.我看过的 ? 1 : paging.pageCount;////看过的店特殊处理只返回最新第一页20条数据
        //            comm.Parameters.Clear();
        //            return list;
        //        }

        /// <summary>
        /// 客户端接口查询用户实时信息
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public double GetCustomerCurrectInfo(long customerId)
        {
            const string strSql = @"select money19dianRemained from CustomerInfo where CustomerID=@customerId ";
            SqlParameter[] parameter = { new SqlParameter("@customerId", SqlDbType.BigInt, 8) { Value = customerId } };
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, parameter))
            {
                if (dr.Read())
                {
                    return dr[0] == DBNull.Value ? 0 : Convert.ToDouble(dr[0]);
                }
            }
            return 0;
        }

        #region 门店举报
        public long InsertShopReport(ShopReport model)
        {
            const string strSql = @"insert into [VAGastronomistMobileApp].[dbo].[ShopReport](
 [CustomId] ,[ReportTime] ,[ShopId] ,[ReportValue])
 values (@CustomId ,@ReportTime,@ShopId,@ReportValue)  select @@identity";
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                SqlParameter[] parameters = new SqlParameter[]{
					    new SqlParameter("@CustomId", SqlDbType.BigInt,8),
                        new SqlParameter("@ReportTime", SqlDbType.DateTime),
                        new SqlParameter("@ShopId", SqlDbType.Int,4),
                         new SqlParameter("@ReportValue", SqlDbType.Int,4)
                    };
                parameters[0].Value = model.CustomId;
                parameters[1].Value = model.ReportTime;
                parameters[2].Value = model.ShopId;
                parameters[3].Value = model.ReportValue;
                object obj = SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql.ToString(), parameters);
                return obj == null ? 0 : Convert.ToInt64(obj);
            }
        }
        #endregion

        /// <summary>
        /// 查询用户收藏，看过，吃过条件下门店编号
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="cityId"></param>
        /// <param name="dataType"></param>
        /// <returns></returns>
        public List<int> GetCustomerShop(long customerId, int cityId, int dataType)
        {
            var list = new List<int>();
            string strSql = "";
            switch (dataType)
            {
                case (int)VAIndexSorting.关注的店:
                    strSql = @"select isnull(CustomerFavoriteCompany.shopId,0) shopId from CustomerFavoriteCompany
left join ShopInfo on CustomerFavoriteCompany.shopId=ShopInfo.shopID
where CustomerFavoriteCompany.customerId=@customerId and cityID=@cityId order by collectTime desc";
                    break;
                case (int)VAIndexSorting.我看过的:
                    strSql = @"select distinct temp.shopId from (select distinct top 20 shopId,MAX(checkTime) checkTime 
         from CustomerCheckedShop where customerId=@customerId and cityId=@cityId group by shopId order by MAX(checkTime) desc) temp";
                    break;
                case (int)VAIndexSorting.我吃过的:
                    strSql = @"select PreOrder19dian.shopId
from PreOrder19dian inner join ShopInfo on PreOrder19dian.shopId=ShopInfo.shopID where customerId=@customerId
and isnull(isPaid,0)=1 and cityID=@cityId order by prePayTime desc";
                    break;
                case (int)VAIndexSorting.有券的店:
                    strSql = string.Format(@" SELECT ShopInfo.shopId
FROM ShopInfo INNER JOIN Coupon ON ShopInfo.shopId=Coupon.shopID WHERE ShopInfo.cityID=@cityId AND Coupon.StartDate <= '{0}' AND Coupon.EndDate >='{0}' AND Coupon.State = 1
and shopStatus=1 and isHandle=1  and IsGot=0  and SendCount<SheetNumber
 ORDER by Coupon.CreateTime DESC ", DateTime.Now);
                    break;
                default:
                    break;
            }
            if (String.IsNullOrWhiteSpace(strSql))
            {
                return list;
            }
            SqlParameter[] parameter = { new SqlParameter("@customerId", SqlDbType.BigInt, 8) { Value = customerId },
                                        new SqlParameter("@cityId", SqlDbType.Int, 4) { Value = cityId }};

            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, parameter))
            {
                while (dr.Read())
                {
                    list.Add(dr[0] == DBNull.Value ? 0 : Convert.ToInt32(dr[0]));
                }
            }
            return list;
        }
        /// <summary>
        /// 查询当前商圈下所有门店
        /// </summary>
        /// <param name="tagId"></param>
        /// <returns></returns>
        public List<int> GetBusinessDistrictShop(int tagId)
        {
            const string cmdText = @"select a.ShopId from dbo.ShopWithTag a
                                      inner join dbo.ShopTag b on a.TagNode.IsDescendantOf(b.TagNode)=1
                                      where b.TagId=@tagId and b.Enable=1";
            SqlParameter[] cmdParm = new[] { new SqlParameter("@tagId", tagId) };
            var list = new List<int>();
            using (var dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, cmdText, cmdParm))
            {
                while (dr.Read())
                {
                    list.Add(dr[0] == DBNull.Value ? 0 : Convert.ToInt32(dr[0]));
                }
            }
            return list;
        }
    }
}
