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
    /// <summary>
    /// 美食广场数据访问接口
    /// </summary>
    public interface IFoodPlazaManager
    {
        /// <summary>
        /// 查询当前城市所有美食广场分享的菜品编号
        /// </summary>
        /// <param name="cityId"></param>
        /// <returns></returns>
        string SelectAllDishsByCityId(int cityId);

        /// <summary>
        /// 查询当前城市当前时间段美食广场所有的数据
        /// </summary>
        /// <param name="cityId"></param>
        /// <param name="strTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        DataTable SelectFoodPlaza(int cityId, DateTime strTime, DateTime endTime);

        /// <summary>
        /// 分页获取美食广场数据（悠先点菜客户端接口调用）
        /// </summary>
        /// <param name="cityId"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="isHaveMore">是否还有下一页数据</param>
        /// <returns></returns>
        List<ClientFoodPlazaTemp> PagingFoodPlaza(int cityId, int pageSize, int pageIndex, out bool isHaveMore);

        /// <summary>
        /// 分页获取美食广场数据（服务器端配置页面调用）
        /// </summary>
        /// <param name="cityId"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="strTime"></param>
        /// <param name="endTime"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        List<FoodPlazaTempClild> PagingFoodPlaza(int cityId, int pageSize, int pageIndex, string strTime, string endTime, out int totalCount);

        /// <summary>
        /// 美食广场配置页面分页根据查询条件查询点单信息
        /// </summary>
        /// <param name="cityId"></param>
        /// <param name="shopId"></param>
        /// <param name="preOrderSum"></param>
        /// <param name="isPaid"></param>
        /// <param name="strTime"></param>
        /// <param name="endTime"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        List<FoodPlazaOrder> PagingFoodPlazaOrder(int cityId, int shopId, double preOrderSum, int isPaid,
            string strTime, string endTime,
            int pageSize, int pageIndex, out int totalCount);

        /// <summary>
        /// 新增美食广场记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        long InsertFoodPlaza(FoodPlaza model);

        /// <summary>
        /// 置顶当前美食广场记录
        /// </summary>
        /// <param name="foodPlazaId"></param>
        /// <param name="latestOperateEmployeeId"></param>
        /// <param name="isListTop"></param>
        /// <returns></returns>
        bool ListTopFoodPlaza(long foodPlazaId, int latestOperateEmployeeId, bool isListTop);

        /// <summary>
        ///  删除当前美食广场记录
        /// </summary>
        /// <param name="foodPlazaId"></param>
        /// <param name="latestOperateEmployeeId"></param>
        /// <returns></returns>
        bool DeleteFoodPlaza(long foodPlazaId, int latestOperateEmployeeId);

        /// <summary>
        /// 美食广场记录更新操作
        /// </summary>
        /// <param name="foodPlazaId"></param>
        /// <param name="latestOperateEmployeeId"></param>
        /// <returns></returns>
        bool UpdateLatestOperate(long foodPlazaId, int latestOperateEmployeeId);
    }

    /// <summary>
    /// 美食广场数据访问
    /// </summary>
    public class FoodPlazaManager : IFoodPlazaManager
    {
        /// <summary>
        /// 查询当前城市所有美食广场分享的菜品编号
        /// </summary>
        /// <param name="cityId"></param>
        /// <returns></returns>
        public string SelectAllDishsByCityId(int cityId)
        {
            string strSql = " select stuff((select ','+dishIds  from dbo.FoodPlaza where cityId=@cityId for xml path('')),1,1,'') as dishIdStr";
            SqlParameter[] parameters = {
					new SqlParameter("@cityId", SqlDbType.Int,4)
			};
            parameters[0].Value = cityId;
            var dishIdStr = "";
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, parameters))
            {
                if (dr.Read())
                {
                    dishIdStr = dr[0] == DBNull.Value ? "" : Convert.ToString(dr[0]);
                }
            }
            return dishIdStr;
        }

        /// <summary>
        /// 查询当前城市当前时间段美食广场所有的数据
        /// </summary>
        /// <param name="cityId"></param>
        /// <param name="strTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public DataTable SelectFoodPlaza(int cityId, DateTime strTime, DateTime endTime)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select foodPlazaId,preOrder19DianId,cityId,shopId,shopName,status,latestUpdateTime,isListTop,dishIds,customerName,customerId,personImgUrl,latestOperateEmployeeId ");
            strSql.Append(" FROM FoodPlaza ");
            strSql.Append(" Where cityId=@cityId and latestUpdateTime between @strTime and @endTime");
            SqlParameter[] parameters = {
					new SqlParameter("@cityId", SqlDbType.Int,4),
					new SqlParameter("@strTime", SqlDbType.DateTime),
                    new SqlParameter("@endTime", SqlDbType.DateTime)
                                         };
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters);
            return ds.Tables[0];
        }

        /// <summary>
        /// 分页获取美食广场数据（悠先点菜客户端接口调用）
        /// </summary>
        /// <param name="cityId"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="isHaveMore">是否还有下一页数据</param>
        /// <returns></returns>
        public List<ClientFoodPlazaTemp> PagingFoodPlaza(int cityId, int pageSize, int pageIndex, out bool isHaveMore)
        {
            PageQuery pageQuery = new PageQuery()
            {
                tableName = @"FoodPlaza A
inner join FoodDiary B on A.preOrder19DianId=B.OrderId
inner join CustomerInfo C on A.customerId=C.CustomerID",
                fields = @"A.shopId,A.dishIds,C.UserName as customerName,B.Content as content,C.Picture as picture,B.Id as foodDiaryId,C.RegisterDate as registerDate",
                orderField = "A.isListTop desc,A.latestUpdateTime desc",
                sqlWhere = "A.cityId=" + cityId + " and A.status=1 and C.CustomerStatus=1"
            };
            Paging paging = new Paging()
            {
                pageIndex = pageIndex,
                pageSize = pageSize,
                recordCount = 0,
                pageCount = 0
            };
            List<ClientFoodPlazaTemp> list = CommonManager.GetPageData<ClientFoodPlazaTemp>(SqlHelper.ConnectionStringLocalTransaction, pageQuery, paging);
            var resultPageNav = new PageNav() { pageIndex = pageIndex, pageSize = pageSize, totalCount = paging.recordCount };
            isHaveMore = resultPageNav.totalCount > pageSize * pageIndex;
            return list;
        }

        /// <summary>
        /// 分页获取美食广场数据（服务器端配置页面调用）
        /// </summary>
        /// <param name="cityId"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="strTime"></param>
        /// <param name="endTime"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public List<FoodPlazaTempClild> PagingFoodPlaza(int cityId, int pageSize, int pageIndex, string strTime, string endTime, out int totalCount)
        {
            StringBuilder strWhere = new StringBuilder();
            strWhere.AppendFormat(" latestUpdateTime between '{0}' and '{1}'", strTime, endTime);
            if (cityId > 0)
            {
                strWhere.AppendFormat("  and cityId={0} and status=1", cityId);
            }
            PageQuery pageQuery = new PageQuery()
            {
                tableName = " FoodPlaza inner join CustomerInfo on FoodPlaza.customerId=CustomerInfo.CustomerID",
                fields = @"foodPlazaId,preOrder19DianId,cityId,shopId,shopName,status,latestUpdateTime ,isListTop
      ,dishIds,CustomerInfo.username as customerName,FoodPlaza.customerId,CustomerInfo.Picture as personImgUrl,latestOperateEmployeeId ,orderAmount,registerDate",
                orderField = "isListTop desc,latestUpdateTime desc",
                sqlWhere = strWhere.ToString()
            };
            Paging paging = new Paging()
            {
                pageIndex = pageIndex,
                pageSize = pageSize,
                recordCount = 0,
                pageCount = 0
            };
            List<FoodPlazaTempClild> list = CommonManager.GetPageData<FoodPlazaTempClild>(SqlHelper.ConnectionStringLocalTransaction, pageQuery, paging);
            var resultPageNav = new PageNav() { pageIndex = pageIndex, pageSize = pageSize, totalCount = paging.recordCount };
            totalCount = resultPageNav.totalCount;
            return list;
        }

        /// <summary>
        /// 美食广场配置页面分页根据查询条件查询点单信息
        /// </summary>
        /// <param name="cityId"></param>
        /// <param name="shopId"></param>
        /// <param name="preOrderSum"></param>
        /// <param name="isPaid"></param>
        /// <param name="strTime"></param>
        /// <param name="endTime"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public List<FoodPlazaOrder> PagingFoodPlazaOrder(int cityId, int shopId, double preOrderSum, int isPaid, string strTime, string endTime,
            int pageSize, int pageIndex, out int totalCount)
        {
            StringBuilder strWhere = new StringBuilder();
            strWhere.Append("A.orderInJson!='' and A.preOrder19dianId not in (select D.preOrder19DianId from FoodPlaza D where D.status=1 )");//发布过后的数据，再次满足条件抓取的时候过滤掉
            if (cityId > 0)
            {
                strWhere.AppendFormat(" and B.cityID={0}", cityId);
            }
            if (shopId > 0)
            {
                strWhere.AppendFormat(" and A.shopId={0}", shopId);
            }
            strWhere.AppendFormat(" and A.preOrderSum>{0}", preOrderSum);
            if (isPaid == 1)
            {
                strWhere.AppendFormat(" and A.isPaid={0}", isPaid);
            }
            else
            {
                strWhere.AppendFormat(" and (A.isPaid is null or A.isPaid=0)", isPaid);
            }
            strWhere.AppendFormat(" and A.preOrderTime between '{0}' and '{1}'", strTime, endTime);
            //            strWhere.Append(@" group by A.preOrder19dianId ,A.orderInJson,A.shopId,B.cityId,
            //                           A.preOrderSum,B.shopName,A.customerId,C.UserName,C.Picture,C.RegisterDate,A.preOrderTime");
            PageQuery pageQuery = new PageQuery()
            {
                tableName = @"PreOrder19dian A
                              inner join ShopInfo B on A.shopId=B.shopID
                              inner join CustomerInfo C on A.customerId=C.CustomerID",
                fields = @"A.preOrder19dianId ,A.orderInJson,A.shopId,B.cityId,
                           A.preOrderSum,B.shopName,A.customerId,C.UserName as customerName,C.Picture as personImgUrl,C.RegisterDate as registerDate",
                orderField = "preOrderTime desc",
                sqlWhere = strWhere.ToString()
            };
            Paging paging = new Paging()
            {
                pageIndex = pageIndex,
                pageSize = pageSize,
                recordCount = 0,
                pageCount = 0
            };
            List<FoodPlazaOrder> list = CommonManager.GetPageData<FoodPlazaOrder>(SqlHelper.ConnectionStringLocalTransaction, pageQuery, paging);
            var resultPageNav = new PageNav() { pageIndex = pageIndex, pageSize = pageSize, totalCount = paging.recordCount };
            totalCount = resultPageNav.totalCount;
            return list;
        }

        /// <summary>
        /// 新增美食广场记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public long InsertFoodPlaza(FoodPlaza model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into FoodPlaza(");
            strSql.Append("preOrder19DianId,cityId,shopId,shopName,status,latestUpdateTime,isListTop,dishIds,customerId,latestOperateEmployeeId,orderAmount)");
            strSql.Append(" values (");
            strSql.Append("@preOrder19DianId,@cityId,@shopId,@shopName,@status,@latestUpdateTime,@isListTop,@dishIds,@customerId,@latestOperateEmployeeId,@orderAmount)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@preOrder19DianId", SqlDbType.BigInt,8),
					new SqlParameter("@cityId", SqlDbType.Int,4),
					new SqlParameter("@shopId", SqlDbType.Int,4),
					new SqlParameter("@shopName", SqlDbType.NVarChar,500),
					new SqlParameter("@status", SqlDbType.Bit,1),
					new SqlParameter("@latestUpdateTime", SqlDbType.DateTime),
					new SqlParameter("@isListTop", SqlDbType.Bit,1),
					new SqlParameter("@dishIds", SqlDbType.NVarChar,100),
					new SqlParameter("@customerId", SqlDbType.BigInt,8),
					new SqlParameter("@latestOperateEmployeeId", SqlDbType.Int,4),
                    new SqlParameter("@orderAmount", SqlDbType.Float)};
            parameters[0].Value = model.preOrder19DianId;
            parameters[1].Value = model.cityId;
            parameters[2].Value = model.shopId;
            parameters[3].Value = model.shopName;
            parameters[4].Value = model.status;
            parameters[5].Value = model.latestUpdateTime;
            parameters[6].Value = model.isListTop;
            parameters[7].Value = model.dishIds;
            parameters[8].Value = model.customerId;
            parameters[9].Value = model.latestOperateEmployeeId;
            parameters[10].Value = model.orderAmount;
            object obj = SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt64(obj);
            }
        }

        /// <summary>
        /// 置顶当前美食广场记录
        /// </summary>
        /// <param name="foodPlazaId"></param>
        /// <param name="latestOperateEmployeeId"></param>
        /// <param name="isListTop"></param>
        /// <returns></returns>
        public bool ListTopFoodPlaza(long foodPlazaId, int latestOperateEmployeeId, bool isListTop)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update FoodPlaza set ");
            strSql.Append("isListTop=@isListTop,");
            strSql.Append("latestUpdateTime=@latestUpdateTime,");
            strSql.Append("latestOperateEmployeeId=@latestOperateEmployeeId");
            strSql.Append(" where foodPlazaId=@foodPlazaId");
            SqlParameter[] parameters = {
					new SqlParameter("@isListTop", SqlDbType.Bit,1),
					new SqlParameter("@foodPlazaId", SqlDbType.BigInt,8),
                    new SqlParameter("@latestOperateEmployeeId", SqlDbType.Int,4),
                    new SqlParameter("@latestUpdateTime", SqlDbType.DateTime),};
            parameters[0].Value = isListTop;
            parameters[1].Value = foodPlazaId;
            parameters[2].Value = latestOperateEmployeeId;
            parameters[3].Value = DateTime.Now;
            int rows = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters);
            return rows > 0;
        }

        /// <summary>
        ///  删除当前美食广场记录
        /// </summary>
        /// <param name="foodPlazaId"></param>
        /// <param name="latestOperateEmployeeId"></param>
        /// <returns></returns>
        public bool DeleteFoodPlaza(long foodPlazaId, int latestOperateEmployeeId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update FoodPlaza set ");
            strSql.Append("status=@status");
            strSql.Append(" where foodPlazaId=@foodPlazaId");
            SqlParameter[] parameters = {
					new SqlParameter("@status", SqlDbType.Bit,1),
                    new SqlParameter("@latestUpdateTime", SqlDbType.DateTime),
					new SqlParameter("@latestOperateEmployeeId", SqlDbType.NChar,10),
					new SqlParameter("@foodPlazaId", SqlDbType.BigInt,8)};
            parameters[0].Value = false;
            parameters[1].Value = DateTime.Now;
            parameters[2].Value = latestOperateEmployeeId;
            parameters[3].Value = foodPlazaId;
            int rows = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters);
            return rows > 0;
        }

        /// <summary>
        /// 美食广场记录更新操作
        /// </summary>
        /// <param name="foodPlazaId"></param>
        /// <param name="latestOperateEmployeeId"></param>
        /// <returns></returns>
        public bool UpdateLatestOperate(long foodPlazaId, int latestOperateEmployeeId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update FoodPlaza set ");
            strSql.Append("latestUpdateTime=@latestUpdateTime,");
            strSql.Append("latestOperateEmployeeId=@latestOperateEmployeeId");
            strSql.Append(" where foodPlazaId=@foodPlazaId");
            SqlParameter[] parameters = {
					new SqlParameter("@latestUpdateTime", SqlDbType.DateTime),
					new SqlParameter("@latestOperateEmployeeId", SqlDbType.NChar,10),
					new SqlParameter("@foodPlazaId", SqlDbType.BigInt,8)};
            parameters[0].Value = DateTime.Now;
            parameters[1].Value = latestOperateEmployeeId;
            parameters[2].Value = foodPlazaId;
            int rows = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters);
            return rows > 0;
        }
    }
}
