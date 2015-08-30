using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.DBUtility;
using System.Data.SqlClient;
using System.Data;
//
//  Copyright 2011 View Alloc inc. All rights reserved.
//  Created by TDQ on 2012-04-10.
//
namespace VAGastronomistMobileApp.SQLServerDAL
{
    /// <summary> 
    /// FileName: ProvinceManageer.cs 
    /// CLRVersion: 4.0.30319.269 
    /// Author: TDQ 
    /// Corporation:杭州友络科技有限公司
    /// Description: 
    /// DateTime: 2012-05-15 15:31:35 
    /// </summary>
    public class ProvinceManager
    {
        /// <summary>
        /// 新增省信息
        /// </summary>
        /// <param name="Province"></param>
        /// <returns></returns>
        public int InsertProvince(Province province)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                Object obj = null;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    SqlParameter[] parameters = null;
                    strSql.Append("insert into Province(");
                    strSql.Append("provinceName)");
                    strSql.Append(" values (");
                    strSql.Append("@provinceName)");
                    strSql.Append(" select @@identity");
                    parameters = new SqlParameter[]{
					    new SqlParameter("@provinceName", SqlDbType.VarChar,50)};
                    parameters[0].Value = province.provinceName;
                    //1、插入省信息表信息
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
        /// 删除省信息
        /// </summary>
        /// <param name="ProvinceID"></param>
        public bool DeleteProvince(int provinceID)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int result = 0;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("delete from  Province where provinceID=@provinceID;");
                    SqlParameter[] parameters = {					
					new SqlParameter("@provinceID", SqlDbType.Int,4)};
                    parameters[0].Value = provinceID;
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
        /// 修改省信息
        /// </summary>
        /// <param name="Province"></param>
        public bool UpdateProvince(Province province)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Province set ");
            strSql.Append("provinceName=@provinceName");
            strSql.Append(" where provinceID=@provinceID ");
            SqlParameter[] parameters = {
                        new SqlParameter("@provinceID", SqlDbType.Int,4),
					    new SqlParameter("@provinceName", SqlDbType.VarChar,50)};
            parameters[0].Value = province.provinceID;
            parameters[1].Value = province.provinceName;
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
        /// 查询所有省信息20140313
        /// </summary>
        /// <returns></returns>
        public DataTable SelectProvince()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select [provinceID],[provinceName]");
            strSql.Append(" from Province");
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }

        /// <summary>
        /// 查询所有上线省份
        /// </summary>
        /// <returns></returns>
        public List<Province> SelectOnLineProvince()
        {
            int flag = (int)VACityStatus.YI_KAI_TONG;
            string strSql = @"select distinct p.[provinceID],[provinceName]
 from Province p
 inner join City c on c.provinceID=p.provinceID
 where c.status=" + flag + " and c.isClientShow=1";
            var list = new List<Province>();
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql))
            {
                while (dr.Read())
                {
                    list.Add(new Province()
                    {
                        provinceID = dr["provinceID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["provinceID"]),
                        provinceName = dr["provinceName"] == DBNull.Value ? "未知城市" : Convert.ToString(dr["provinceName"])
                    });
                }
            }
            return list;
        }
    }
}
