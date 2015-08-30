﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.DBUtility;
using System.Data.SqlClient;
using System.Data;
using VAGastronomistMobileApp.WebPageDll;
using System.Transactions;
namespace VAGastronomistMobileApp.SQLServerDAL
{
    /// <summary> 
    /// FileName: CityManager.cs 
    /// CLRVersion: 4.0.30319.269 
    /// Author: TDQ 
    /// Corporation:杭州友络科技有限公司
    /// Description: 
    /// DateTime: 2012-05-15 15:32:09 
    /// </summary>
    public class CityManager
    {
        /// <summary>
        /// 新增市信息
        /// </summary>
        /// <param name="City"></param>
        /// <returns></returns>
        public int InsertCity(City city)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                Object obj = null;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    SqlParameter[] parameters = null;
                    strSql.Append("insert into City(");
                    strSql.Append("cityName,zipCode,provinceID)");
                    strSql.Append(" values (");
                    strSql.Append("@cityName,@zipCode,@provinceID)");
                    strSql.Append(" select @@identity");
                    parameters = new SqlParameter[]{
					    new SqlParameter("@cityName", SqlDbType.VarChar,50),
                        new SqlParameter("@zipCode", SqlDbType.VarChar,50),
                        new SqlParameter("@provinceID", SqlDbType.Int,4),
                        new SqlParameter("@status",SqlDbType.Int,4)
                    };
                    parameters[0].Value = city.cityName;
                    parameters[1].Value = city.zipCode;
                    parameters[2].Value = city.provinceID;
                    parameters[3].Value = (int)VACityStatus.WEI_KAI_TONG;

                    //1、插入市信息表信息
                    obj = SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql.ToString(), parameters);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
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
        /// 设置城市入驻或退出
        /// </summary>
        /// <param name="status"></param>
        /// <param name="cityID"></param>
        public bool SetCityStatus(int status, int cityID)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int result = 0;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("Update City set status=@status where cityID=@cityID;");
                    SqlParameter[] parameters = {					
					    new SqlParameter("@cityID", SqlDbType.Int,4),
                        new SqlParameter("@status", SqlDbType.Int,4)};
                    parameters[0].Value = cityID;
                    parameters[1].Value = status;
                    result = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);
                }
                catch
                {
                    return false;
                }
                if (result >= 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool SetClientStatus(int isClientShow, int cityID)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int result = 0;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("Update City set isClientShow=@isClientShow where cityID=@cityID;");
                    SqlParameter[] parameters = {					
					new SqlParameter("@cityID", SqlDbType.Int,4),
                                                new SqlParameter("@isClientShow", SqlDbType.Int,4)};
                    parameters[0].Value = cityID;
                    parameters[1].Value = isClientShow;
                    result = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);
                }
                catch
                {
                    return false;
                }
                if (result >= 1)
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
        /// 删除市信息
        /// </summary>
        /// <param name="CityID"></param>
        public bool DeleteCity(int cityID)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int result = 0;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("delete from  City where cityID=@cityID;");
                    SqlParameter[] parameters = {					
					new SqlParameter("@cityID", SqlDbType.Int,4)};
                    parameters[0].Value = cityID;
                    result = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);
                }
                catch
                {

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
        /// 修改市信息
        /// </summary>
        /// <param name="City"></param>
        public bool UpdateCity(City city)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update City set ");
            strSql.Append("cityName=@cityName");
            strSql.Append("zipCode=@zipCode");
            strSql.Append("provinceID=@provinceID");
            strSql.Append(" where cityID=@cityID ");
            SqlParameter[] parameters = {
                        new SqlParameter("@cityID", SqlDbType.Int,4),
                        new SqlParameter("@cityName", SqlDbType.VarChar,50),
                        new SqlParameter("@zipCode", SqlDbType.VarChar,50),
					    new SqlParameter("@provinceID", SqlDbType.Int,4)};
            parameters[0].Value = city.cityID;
            parameters[1].Value = city.cityName;
            parameters[2].Value = city.zipCode;
            parameters[3].Value = city.provinceID;
            int result = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters);
            if (result >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 根据城市编号更新请求开通次数
        /// </summary>
        /// <param name="cityId"></param>
        /// <param name="requestCount"></param>
        /// <returns></returns>
        public bool UpdateCityRequestCount(int cityId, int requestCount)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int result = 0;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    SqlParameter[] parameters = null;
                    strSql.Append("update City set ");
                    strSql.Append("requestCount=@requestCount");
                    strSql.Append(" where cityID=@cityID");
                    parameters = new SqlParameter[]{
                    new SqlParameter("@requestCount", SqlDbType.Int,4),
                    new SqlParameter("@cityID", SqlDbType.Int,4)};
                    parameters[0].Value = requestCount;
                    parameters[1].Value = cityId;

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
        /// 查询所有城市信息
        /// </summary>
        /// <returns></returns>
        public DataTable SelectCity()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select [cityID],[cityName],[zipCode],[provinceID],[requestCount],[cityCenterLongitude],[cityCenterLatitude],[restaurantCount],[status]");
            strSql.Append(" from City where status > 0");
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }

        /// <summary>
        /// 查询城市列表信息 -首页改版城市管理用
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public DataTable SelectCity(string type)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ROW_NUMBER()over(Order by isClientShow desc,status desc) as [rowID],[cityID],[cityName],[provinceName],[status],isClientShow");
            strSql.Append(" from City city");
            strSql.Append(" INNER JOIN Province province");
            strSql.Append(" On city.provinceID = province.provinceID Order by status desc,isClientShow desc");
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }

        /// <summary>
        /// 根据城市编号查询对应城市信息
        /// </summary>
        /// <returns></returns>
        public DataTable SelectCity(int cityId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select [cityID],[cityName],[zipCode],[provinceID],[requestCount],[cityCenterLongitude],[cityCenterLatitude],[restaurantCount],[status],[WeatherCityCode],[isClientShow]");
            strSql.AppendFormat(" from City where cityID = '{0}'", cityId);
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }
        /// <summary>
        /// 根据省份编号查询对应城市信息20140313
        /// </summary>
        /// <returns></returns>
        public DataTable SelectProvinceCity(int provinceId)
        {
            const string strSql = @"select [cityID],[cityName],[zipCode],[provinceID],[requestCount],[cityCenterLongitude],[cityCenterLatitude],[restaurantCount],[status],isClientShow
                                        from City where provinceID =@provinceID and status > 0";
            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@provinceID",SqlDbType.Int) { Value = provinceId }
            };
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, para);
            return ds.Tables[0];
        }
        /// <summary>
        /// 更新城市中心经纬度
        /// </summary>
        /// <param name="cityId"></param>
        /// <param name="longitude"></param>
        /// <param name="latitude"></param>
        /// <returns></returns>
        public bool UpdateCityCoordinate(int cityId, double longitude, double latitude)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int result = 0;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("update City set ");
                    strSql.Append("cityCenterLongitude=@cityCenterLongitude,");
                    strSql.Append("cityCenterLatitude=@cityCenterLatitude");
                    strSql.Append(" where cityID=@cityID ");
                    SqlParameter[] parameters = {
                        new SqlParameter("@cityCenterLongitude", SqlDbType.Float),
                        new SqlParameter("@cityCenterLatitude", SqlDbType.Float),
					    new SqlParameter("@cityID", SqlDbType.Int,4)};
                    parameters[0].Value = longitude;
                    parameters[1].Value = latitude;
                    parameters[2].Value = cityId;

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
        /// 查询省市县名称
        /// </summary>
        /// <param name="countyID"></param>
        /// <returns></returns>
        public SybShopHandeleDetail SelectCountyCityProvinceName(int countyID)
        {
            string str = @"SELECT top 1 County.countyName,Province.provinceName,City.cityName
from County INNER JOIN City on County.cityID=City.cityID
INNER JOIN Province on Province.provinceID=City.provinceID
where countyID=" + countyID + "";
            dynamic data = new
            {
                countyName = "",
                cityName = "",
                provinceName = ""
            };
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, str))
            {
                if (dr.Read())
                {
                    data = new SybShopHandeleDetail
                    {
                        countyName = dr["countyName"] == DBNull.Value ? "" : Convert.ToString(dr["countyName"]),
                        cityName = dr["cityName"] == DBNull.Value ? "" : Convert.ToString(dr["cityName"]),
                        provinceName = dr["provinceName"] == DBNull.Value ? "" : Convert.ToString(dr["provinceName"])
                    };
                }
            }
            return data;
        }

        /// <summary>
        /// 查询上线城市列表
        /// </summary>
        /// <returns></returns>
        public List<CityViewModel> SelectHandleCity()
        {
            string sqlStr = @"select cityID,cityName  from City where status =2";
            var list = new List<CityViewModel>();
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sqlStr))
            {
                while (dr.Read())
                {
                    list.Add(new CityViewModel()
                    {
                        cityID = Convert.ToInt32(SqlHelper.ConvertDbNullValue(dr["cityID"])),
                        cityName = SqlHelper.ConvertDbNullValue(dr["cityName"])
                    });
                }
            }
            return list;
        }
        /// <summary>
        /// 查询门店名称和所在城市
        /// </summary>
        /// <param name="shopID"></param>
        /// <returns></returns>
        public CityExt SelectCityNameAndShopName(int shopID)
        {
            string sqlStr = @"select shopname,cityName,case when a.ishandle=1 then '是' else '否' end ishandle,isnull(a.accountManager,0) accountManager from shopinfo a inner join city b on a.cityid=b.cityid  where a.shopid=@shopid";
            SqlParameter[] parameter = new SqlParameter[] { new SqlParameter("@shopid", SqlDbType.Int, 4) };
            parameter[0].Value = shopID;
            CityExt model = new CityExt();
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sqlStr, parameter))
            {
                while (dr.Read())
                {
                    model.shopName = Convert.ToString(SqlHelper.ConvertDbNullValue(dr["shopname"]));
                    model.cityName = Convert.ToString(SqlHelper.ConvertDbNullValue(dr["cityName"]));
                    model.isHandle = Convert.ToString(SqlHelper.ConvertDbNullValue(dr["ishandle"]));
                    model.accountManager = Convert.ToInt32(SqlHelper.ConvertDbNullValue(dr["accountManager"]));
                }
            }
            return model;
        }

        public City GetCityById(int cityId)
        {
            string sql = "SELECT * FROM City WHERE CityId = @cityId";
            SqlParameter parameter = new SqlParameter("@cityId", SqlDbType.Int, 4) { Value = cityId };
            City city = null;
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sql, parameter))
            {
                if (dr.Read())
                {
                    city = dr.GetEntity<City>();
                }
            }
            return city;
        }

        /// <summary>
        /// 增加一级栏目中的全部栏目（非广告类）
        /// </summary>
        public bool AddNomalTitle(int cityID, int createBy)
        {
            using (TransactionScope ts= new TransactionScope())
            {
                using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
                {
                    int titleID = 0;
                    bool result = false;
                    try
                    {
                        StringBuilder strSql = new StringBuilder();
                        strSql.Append("Insert into HomeFirstTitle ");
                        strSql.Append("values(@cityID,@titleName,@titleIndex,@type,@status,@createTime,@createBy,@isDelete);");
                        strSql.Append(" select @@Identity");
                        SqlParameter[] param = new SqlParameter[] { 
                        new SqlParameter("@cityID",SqlDbType.Int,4),
                        new SqlParameter("@titleName", SqlDbType.NVarChar),
                        new SqlParameter("@titleIndex",SqlDbType.Int, 4),
                        new SqlParameter("@type",SqlDbType.Int, 4),
                        new SqlParameter("@status", SqlDbType.Int,4),
                        new SqlParameter("@createTime",SqlDbType.DateTime),
                        new SqlParameter("@createBy",SqlDbType.Int, 4),
                        new SqlParameter("@isDelete",SqlDbType.Bit)
                        };

                        param[0].Value = cityID;
                        param[1].Value = "全部";
                        param[2].Value = 1;
                        param[3].Value = 1;
                        param[4].Value = 1;
                        param[5].Value = DateTime.Now;
                        param[6].Value = createBy;
                        param[7].Value = "false";
                        titleID = Convert.ToInt32(SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql.ToString(), param));
                    }
                    catch (Exception)
                    {
                        return false;
                    }

                    if (titleID != 0)
                    {
                        result = AddNormalSubTitle(titleID, createBy);
                        if (result)
                        {
                            ts.Complete();
                        }
                        return result;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }

        /// <summary>
        /// 增加全部栏目下的全城和附近二级栏目
        /// </summary>
        public bool AddNormalSubTitle(int titleID, int createBy)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int result = 0;
                try
                {
                    StringBuilder str = new StringBuilder();
                    str.Append("Insert into HomeSecondTitle");
                    str.AppendFormat(" values({0},'全城',1,1,1,'{1}',{2},'false'),", titleID, DateTime.Now, createBy);
                    str.AppendFormat(" ({0},'附近',2,2,1,'{1}',{2},'false')", titleID, DateTime.Now, createBy);
                    result = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, str.ToString());
                }
                catch (Exception)
                {
                    return false;
                }
                if (result >= 1)
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
        /// 使用该方法判断城市曾经是否入驻过
        /// </summary>
        /// <param name="cityID"></param>
        /// <returns></returns>
        public bool CityOnceOpened(int cityID)
        {
            string strSql = string.Format(@"select cityID from HomeFirstTitle where type=1 and isDelete=0 and cityID='{0}'", cityID);
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            else
                return false;
        }

        public bool DownlineAll(int cityID)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int result =0;
                try
                {
                    StringBuilder str = new StringBuilder();
                    str.AppendFormat("update HomeFirstTitle set status=0 where cityID={0} and type=2", cityID);
                    str.AppendFormat("update AdvertShop set status=0 where cityID={0};", cityID);
                    result = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, str.ToString());
                }
                catch (Exception)
                {
                    return false;
                }
                if (result >= 1)
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
}
