using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.DBUtility;
using System.Data;

namespace VAGastronomistMobileApp.SQLServerDAL
{
    /// <summary>
    /// 勋章信息操作
    /// </summary>
    public class MedalManager
    {
        /// <summary>
        /// new新增勋章图片信息
        /// </summary>
        /// <param name="customEncourage"></param>
        /// <returns></returns>
        public long InsertMedalImageInfoTable(MedalImageInfo medalImageInfo)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                Object obj = null;
                try
                {
                    conn.Open();

                    StringBuilder strSql = new StringBuilder();
                    SqlParameter[] parameters = null;
                    strSql.Append("insert into [MedalImageInfo](");
                    strSql.Append("[medalId],[medalURL],[medalScale])");
                    strSql.Append(" values (");
                    strSql.Append("@medalId,@medalURL,@medalScale)");
                    strSql.Append(" select @@identity");

                    parameters = new SqlParameter[]{
					        new SqlParameter("@medalId", SqlDbType.Int,4),
                            new SqlParameter("@medalURL",SqlDbType.NVarChar,200),
                            new SqlParameter("@medalScale",SqlDbType.Int,4)};

                    parameters[0].Value = medalImageInfo.medalId;
                    parameters[1].Value = medalImageInfo.medalURL;
                    parameters[2].Value = medalImageInfo.medalScale;
                    obj = SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql.ToString(), parameters);
                }
                catch (Exception)
                {
                    return 0;
                }
                if (obj == null)
                {
                    return 0;
                }
                else
                {
                    return Convert.ToInt64(obj);
                }
            }
        }
        /// <summary>
        /// new新增勋章信息
        /// </summary>
        /// <param name="customEncourage"></param>
        /// <returns></returns>
        public long InsertMedalConnShopCompanyTable(MedalConnShopCompany medalConnShopCompany)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                Object obj = null;
                try
                {
                    conn.Open();

                    StringBuilder strSql = new StringBuilder();
                    SqlParameter[] parameters = null;
                    strSql.Append("insert into [MedalConnShopCompany](");
                    strSql.Append("[medalType],[medalName],[companyOrShopId],[medalDescription])");
                    strSql.Append(" values (");
                    strSql.Append("@medalType,@medalName,@companyOrShopId,@medalDescription)");
                    strSql.Append(" select @@identity");

                    parameters = new SqlParameter[]{
					        new SqlParameter("@medalType", SqlDbType.Int,4),
                            new SqlParameter("@medalName",SqlDbType.NVarChar,100),
                            new SqlParameter("@companyOrShopId",SqlDbType.Int),
                            new SqlParameter("@medalDescription",SqlDbType.NVarChar,200)};

                    parameters[0].Value = medalConnShopCompany.medalType;
                    parameters[1].Value = medalConnShopCompany.medalName;
                    parameters[2].Value = medalConnShopCompany.companyOrShopId;
                    parameters[3].Value = medalConnShopCompany.medalDescription;
                    obj = SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql.ToString(), parameters);
                }
                catch (Exception)
                {
                    return 0;
                }
                if (obj == null)
                {
                    return 0;
                }
                else
                {
                    return Convert.ToInt64(obj);
                }
            }
        }
        /// <summary>
        /// 查询勋章信息
        /// </summary>
        /// <param name="customEncourageId"></param>
        /// <returns></returns>
        public DataTable SelectMedalInfoTable(int companyOrShopId, int medalType, int medalScale)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select MedalConnShopCompany.[medalType],MedalConnShopCompany.[id],MedalConnShopCompany.[companyOrShopId],MedalConnShopCompany.[medalName],MedalImageInfo .[medalURL],MedalConnShopCompany.[medalDescription],MedalImageInfo .[medalScale]");
            strSql.Append(" from MedalImageInfo inner join MedalConnShopCompany on  MedalConnShopCompany.id=MedalImageInfo.medalId");
            strSql.AppendFormat(" where MedalConnShopCompany.[companyOrShopId] = {0} and MedalConnShopCompany.[medalType]={1} and MedalImageInfo.[medalScale]={2}", companyOrShopId, medalType, medalScale);
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }

        /// <summary>
        /// 查询勋章信息根据勋章Id
        /// </summary>
        /// <param name="customEncourageId"></param>
        /// <returns></returns>
        public DataTable SelectMedalInfoTableById(long id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select [medalType],[id],[companyOrShopId],[medalName],[medalDescription]");
            strSql.Append(" from MedalConnShopCompany");
            strSql.AppendFormat(" where id='{0}'", id);
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }
        /// <summary>
        /// 修改勋章的描述信息
        /// </summary>
        /// <param name="companyVip"></param>
        /// <returns></returns>
        public bool UpdateMedalNameAndDescription(string medalName, string medalDescription, long id)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int result = 0;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("update MedalConnShopCompany set ");
                    strSql.Append(" medalName=@medalName,");
                    strSql.Append(" medalDescription=@medalDescription");
                    strSql.Append(" where id=@id ");
                    SqlParameter[] parameters = {                   
                    new SqlParameter("@medalName", SqlDbType.NVarChar,100),
                    new SqlParameter("@medalDescription", SqlDbType.NVarChar,200),
					new SqlParameter("@id", SqlDbType.BigInt,8)};
                    parameters[0].Value = medalName;
                    parameters[1].Value = medalDescription;
                    parameters[2].Value = id;

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
        /// 删除勋章信息
        /// </summary>
        /// <param name="imageID"></param>
        public bool DeleteMedalInfoTable(long medalId, int companyOrShopId)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int result = 0;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("delete from MedalConnShopCompany where id=@medalId and companyOrShopId=@companyOrShopId");
                    strSql.Append("  delete from MedalImageInfo  where medalId=@medalId");
                    SqlParameter[] parameters = {					
					new SqlParameter("@medalId", SqlDbType.Int),
                    new SqlParameter("@companyOrShopId", SqlDbType.Int)};
                    parameters[0].Value = medalId;
                    parameters[1].Value = companyOrShopId;
                    result = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);
                }
                catch (Exception)
                {
                    return false;
                }
                if (result > 0)//result==3(正常)
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
        /// 查询勋章图片信息 add by wangc 20140419
        /// </summary>
        /// <param name="customEncourageId"></param>
        /// <returns></returns>
        public DataTable SelectMedalInfoByMedalId(long medalId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select [id],[medalId] ,[medalScale] ,[medalURL]");
            strSql.Append(" from [MedalImageInfo]");
            strSql.AppendFormat(" where medalId='{0}'", medalId);
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }
        /// <summary>
        /// 查询门店所有勋章路径
        /// </summary>
        /// <param name="shopId"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public List<string> SelectShopMedalUrl(int shopId, string path)
        {
            const string strSql = @"select B.medalURL from MedalConnShopCompany A inner join MedalImageInfo B on A.id=B.medalId  where A.medalType=2
 and B.medalScale=1 and A.companyOrShopId=@companyOrShopId";
            List<string> list = new List<string>();
            SqlParameter parameter = new SqlParameter("@companyOrShopId", SqlDbType.Int, 4) { Value = shopId };
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, parameter))
            {
                while (dr.Read())
                {
                    list.Add(dr["medalURL"] == DBNull.Value ? "" : path + Convert.ToString(dr["medalURL"]));
                }
            }
            return list;
        }
    }
}
