﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.DBUtility;
using VAGastronomistMobileApp.Model;

namespace VAGastronomistMobileApp.SQLServerDAL
{
    public interface IShopVIPManager
    {
        long AddShopVIP(ShopVIP model);
        ShopVIP SelectShopVIP(long CustomerId, int ShopId);
        bool UpdateShopVIP(ShopVIP model);
        ZZB_ShopVipResponse GetZZBShopVipResponse(int shopId, int cityId);
        ZZB_ShopVIPFlaseCountModel GetShopVIPFlaseCountModel(int cityId);

        #region----------------------------------------
        double GetShopVipDiscount(int shopId);
        #endregion
    }
    public class ShopVIPManager : IShopVIPManager
    {
        public long AddShopVIP(ShopVIP model)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into ShopVIP(");
                strSql.Append("CustomerId,ShopId,CityId,PreOrderTotalQuantity,PreOrderTotalAmount,CreateTime,LastPreOrderTime)");
                strSql.Append(" values (");
                strSql.Append("@CustomerId,@ShopId,@CityId,@PreOrderTotalQuantity,@PreOrderTotalAmount,@CreateTime,@LastPreOrderTime)");
                strSql.Append(";select @@IDENTITY");
                SqlParameter[] parameters = {
					new SqlParameter("@CustomerId", SqlDbType.BigInt,8),
					new SqlParameter("@ShopId", SqlDbType.Int,4),
					new SqlParameter("@CityId", SqlDbType.Int,4),
					new SqlParameter("@PreOrderTotalQuantity", SqlDbType.Int,4),
					new SqlParameter("@PreOrderTotalAmount", SqlDbType.Decimal,9),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@LastPreOrderTime", SqlDbType.DateTime)};
                parameters[0].Value = model.CustomerId;
                parameters[1].Value = model.ShopId;
                parameters[2].Value = model.CityId;
                parameters[3].Value = model.PreOrderTotalQuantity;
                parameters[4].Value = model.PreOrderTotalAmount;
                parameters[5].Value = model.CreateTime;
                parameters[6].Value = model.LastPreOrderTime;
                object obj = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);
                return Convert.ToInt64(obj);
            }
        }
        public ShopVIP SelectShopVIP(long CustomerId, int ShopId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 Id,CustomerId,ShopId,CityId,PreOrderTotalQuantity,PreOrderTotalAmount,CreateTime,LastPreOrderTime from ShopVIP ");
            strSql.Append(" where  CustomerId=@CustomerId and ShopId=@ShopId ");
            SqlParameter[] parameters = {
					new SqlParameter("@CustomerId", SqlDbType.BigInt,8),
					new SqlParameter("@ShopId", SqlDbType.Int,4)	
                                        };
            parameters[0].Value = CustomerId;
            parameters[1].Value = ShopId;
            var model = new ShopVIP();
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters))
            {
                if (dr.Read())
                {
                    //数据库定义的都是不为空字段，其实没有判断的必要，纯属习惯
                    model.CityId = dr["CityId"] == DBNull.Value ? 0 : Convert.ToInt32(dr["CityId"]);
                    model.CreateTime = dr["CreateTime"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(dr["CreateTime"]);
                    model.CustomerId = dr["CustomerId"] == DBNull.Value ? 0 : Convert.ToInt64(dr["CustomerId"]);
                    model.Id = dr["Id"] == DBNull.Value ? 0 : Convert.ToInt32(dr["Id"]);
                    model.LastPreOrderTime = dr["LastPreOrderTime"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(dr["LastPreOrderTime"]);
                    model.PreOrderTotalAmount = dr["PreOrderTotalAmount"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["PreOrderTotalAmount"]);
                    model.PreOrderTotalQuantity = dr["PreOrderTotalQuantity"] == DBNull.Value ? 0 : Convert.ToInt32(dr["PreOrderTotalQuantity"]);
                    model.ShopId = dr["ShopId"] == DBNull.Value ? 0 : Convert.ToInt32(dr["ShopId"]);
                }
            }
            return model;
        }
        public bool UpdateShopVIP(ShopVIP model)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update ShopVIP set ");
                strSql.Append("PreOrderTotalQuantity=@PreOrderTotalQuantity,");
                strSql.Append("PreOrderTotalAmount=@PreOrderTotalAmount,");
                strSql.Append("LastPreOrderTime=@LastPreOrderTime");
                strSql.Append(" where Id=@Id");
                SqlParameter[] parameters = {
					new SqlParameter("@PreOrderTotalQuantity", SqlDbType.Int,4),
					new SqlParameter("@PreOrderTotalAmount", SqlDbType.Decimal,9),
					new SqlParameter("@LastPreOrderTime", SqlDbType.DateTime),
					new SqlParameter("@Id", SqlDbType.BigInt,8)
                                            };
                parameters[0].Value = model.PreOrderTotalQuantity;
                parameters[1].Value = model.PreOrderTotalAmount;
                parameters[2].Value = model.LastPreOrderTime;
                parameters[3].Value = model.Id;

                return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters) > 0;
            }
        }
        public ZZB_ShopVipResponse GetZZBShopVipResponse(int shopId, int cityId)
        {
            const string strSql = @"SELECT (select COUNT(CustomerId) from ShopVIP where ShopId=@shopId) as shopTotalCount,
(select top 1 COUNT(CustomerId) as currectCityTopShopTotalCount from ShopVIP B  WHERE B.CityId=@cityId group by B.ShopId ORDER BY currectCityTopShopTotalCount DESC) as currectCityTopShopTotalCount,
(SELECT City.cityName from City where City.cityID=@cityId) as currectCityName,
(select COUNT(CustomerId) from ShopVIP C where datediff(mm,C.CreateTime,getdate())=0 and ShopId=@shopId) as currectMonthShopIncreasedCount,
(SELECT MONTH(GETDATE())) as currectMonth,
(select top 1 COUNT(CustomerId) as currectMonthTopShopIncreasedCount from ShopVIP C where datediff(mm,C.CreateTime,getdate())=0 group by C.ShopId ORDER BY currectMonthTopShopIncreasedCount DESC) as currectMonthTopShopIncreasedCount";
            SqlParameter[] parameter ={
            new SqlParameter("@shopId",shopId),new SqlParameter("@cityId",cityId)
            };
            ZZB_ShopVipResponse model = new ZZB_ShopVipResponse();
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, parameter))
            {
                if (dr.Read())
                {
                    model.currectCityName = dr["currectCityName"] == DBNull.Value ? "" : Convert.ToString(dr["currectCityName"]);
                    model.currectCityTopShopTotalCount = dr["currectCityTopShopTotalCount"] == DBNull.Value ? 0 : Convert.ToInt32(dr["currectCityTopShopTotalCount"]);
                    model.currectMonth = dr["currectMonth"] == DBNull.Value ? 0 : Convert.ToInt32(dr["currectMonth"]);
                    model.currectMonthShopIncreasedCount = dr["currectMonthShopIncreasedCount"] == DBNull.Value ? 0 : Convert.ToInt32(dr["currectMonthShopIncreasedCount"]);
                    model.currectMonthTopShopIncreasedCount = dr["currectMonthTopShopIncreasedCount"] == DBNull.Value ? 0 : Convert.ToInt32(dr["currectMonthTopShopIncreasedCount"]);
                    model.shopTotalCount = dr["shopTotalCount"] == DBNull.Value ? 0 : Convert.ToInt32(dr["shopTotalCount"]);
                }
            }
            return model;
        }
        public ZZB_ShopVIPFlaseCountModel GetShopVIPFlaseCountModel(int cityId)
        {
            string month = DateTime.Now.Month < 10 ? "0" + DateTime.Now.Month.ToString() : DateTime.Now.Month.ToString();
            string dateStr = DateTime.Now.Year.ToString() + month;
            const string strSql = @"SELECT (SELECT sum([Count]) from ShopVIPFlaseCount where City=@cityId) as currectCityTopShopTotalCountFalse,
(SELECT sum([Count]) from ShopVIPFlaseCount where City=@cityId and ShopVIPFlaseCount.[Date]=@dateStr) as currectMonthTopShopIncreasedCountFalse";
            SqlParameter[] parameter = { new SqlParameter("@cityId", cityId), 
                                           new SqlParameter("@dateStr", dateStr) };
            ZZB_ShopVIPFlaseCountModel model = new ZZB_ShopVIPFlaseCountModel();
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameter))
            {
                if (dr.Read())
                {
                    model.currectCityTopShopTotalCountFalse = dr["currectCityTopShopTotalCountFalse"] == DBNull.Value ? 0 : Convert.ToInt32(dr["currectCityTopShopTotalCountFalse"]);
                    model.currectMonthTopShopIncreasedCountFalse = dr["currectMonthTopShopIncreasedCountFalse"] == DBNull.Value ? 0 : Convert.ToInt32(dr["currectMonthTopShopIncreasedCountFalse"]);
                }
            }
            return model;
        }

        #region ------------------------------------------------------------------------
        /// <summary>
        /// 查询店铺折扣
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public double GetShopVipDiscount(int shopId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select discount from ShopVIPInfo ");
            strSql.Append(" where ShopId=@ShopId  and platformVipId=1 and status=1 ");
            SqlParameter[] parameters = {
					new SqlParameter("@ShopId", SqlDbType.Int,4)	
                                        };
            parameters[0].Value = shopId;

            double discount = 0;
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters))
            {
                if (dr.Read())
                {
                    discount=Convert.ToDouble(dr[0])*10;
                    if(discount==10)
                    {
                        discount = 0;
                    }
                }
            }
            return discount;
        }
        #endregion
    }
}



