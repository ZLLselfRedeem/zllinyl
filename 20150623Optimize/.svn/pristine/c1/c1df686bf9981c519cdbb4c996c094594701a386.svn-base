using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.DBUtility;
using System.Data.SqlClient;
using System.Data;
namespace VAGastronomistMobileApp.SQLServerDAL
{
    /// <summary> 
    /// FileName: CountyManager.cs 
    /// CLRVersion: 4.0.30319.269 
    /// Author: TDQ 
    /// Corporation:杭州友络科技有限公司
    /// Description: 
    /// DateTime: 2012-05-15 15:40:54 
    /// </summary>
    public class CountyManager
    {
        /// <summary>
        /// 新增市信息
        /// </summary>
        /// <param name="County"></param>
        /// <returns></returns>
        public int InsertCounty(County county)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                Object obj = null;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    SqlParameter[] parameters = null;
                    strSql.Append("insert into County(");
                    strSql.Append("countyName,cityID)");
                    strSql.Append(" values (");
                    strSql.Append("@countyName,@cityID)");
                    strSql.Append(" select @@identity");
                    parameters = new SqlParameter[]{
					    new SqlParameter("@countyName", SqlDbType.VarChar,50),
                        new SqlParameter("@cityID", SqlDbType.Int,4)
                    };
                    parameters[0].Value = county.countyName;
                    parameters[1].Value = county.cityID;
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
        /// 删除市信息
        /// </summary>
        /// <param name="CountyID"></param>
        public bool DeleteCounty(int countyID)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int result = 0;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("delete from  County where countyID=@countyID;");
                    SqlParameter[] parameters = {					
					new SqlParameter("@CountyID", SqlDbType.Int,4)};
                    parameters[0].Value = countyID;
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
        /// <param name="County"></param>
        public bool UpdateCounty(County county)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update County set ");
            strSql.Append("countyName=@countyName");
            strSql.Append("cityID=@cityID");
            strSql.Append(" where countyID=@countyID ");
            SqlParameter[] parameters = {
                        new SqlParameter("@countyID", SqlDbType.Int,4),
                        new SqlParameter("@countyName", SqlDbType.VarChar,50),
					    new SqlParameter("@cityID", SqlDbType.Int,4)};
            parameters[0].Value = county.countyID;
            parameters[1].Value = county.countyName;
            parameters[2].Value = county.cityID;
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
        /// 查询所有市信息
        /// </summary>
        /// <returns></returns>
        public DataTable SelectCounty()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select [countyID],[countyName],[cityID],[countyCenterLongitude],[countyCenterLatitude]");
            strSql.Append(" from County");
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }
    }
}
