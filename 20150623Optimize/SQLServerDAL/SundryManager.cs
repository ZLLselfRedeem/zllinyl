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
    /// 店铺杂项管理（wangcheng）
    /// </summary>
    public class SundryManager
    {
        /// <summary>
        /// （wangcheng）插入店铺杂项信息
        /// </summary>
        public long InsertSundryInfoReturnidentity(SundryInfo sundryInfo)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                Object obj = null;
                try
                {
                    conn.Open();

                    StringBuilder strSql = new StringBuilder();
                    SqlParameter[] parameters = null;
                    strSql.Append("insert into SundryInfo(");
                    strSql.Append("shopId,sundryName,sundryStandard,sundryChargeMode,");
                    strSql.Append("supportChangeQuantity,price,status,description,vipDiscountable,backDiscountable,required)");
                    strSql.Append(" values (");
                    strSql.Append("@shopId,@sundryName,@sundryStandard,@sundryChargeMode,");
                    strSql.Append("@supportChangeQuantity,@price,@status,@description,@vipDiscountable,@backDiscountable,@required)");
                    strSql.Append(" select @@identity");//查询返回主键

                    parameters = new SqlParameter[]{
					        new SqlParameter("@shopId", SqlDbType.Int,4),
                            new SqlParameter("@sundryName",SqlDbType.NVarChar,20),
                            new SqlParameter("@sundryStandard",SqlDbType.NVarChar,10),
                            new SqlParameter("@sundryChargeMode",SqlDbType.Int),
                            new SqlParameter("@supportChangeQuantity", SqlDbType.Bit,5),
                            new SqlParameter("@price",SqlDbType.Float),
                            new SqlParameter("@status",SqlDbType.Int,4),
                     new SqlParameter("@description",SqlDbType.NVarChar,300),
                    new SqlParameter("@vipDiscountable",SqlDbType.Bit),
                            new SqlParameter("@backDiscountable",SqlDbType.Bit),
                     new SqlParameter("@required",SqlDbType.Bit)};

                    parameters[0].Value = sundryInfo.shopId;
                    parameters[1].Value = sundryInfo.sundryName;
                    parameters[2].Value = sundryInfo.sundryStandard;
                    parameters[3].Value = sundryInfo.sundryChargeMode;
                    parameters[4].Value = sundryInfo.supportChangeQuantity;
                    parameters[5].Value = sundryInfo.price;
                    parameters[6].Value = sundryInfo.status;
                    parameters[7].Value = sundryInfo.description;
                    parameters[8].Value = sundryInfo.vipDiscountable;
                    parameters[9].Value = sundryInfo.backDiscountable;
                    parameters[10].Value = sundryInfo.required;

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
        /// （wangcheng）查询添加的杂货信息
        /// </summary>
        public DataTable SelectSundryInfoByShopId(int shopId)
        {
            StringBuilder strSql = new StringBuilder();
            SqlParameter[] parameters ={
                                      new SqlParameter("@shopId",shopId)                                  
                                  };
            strSql.Append("SELECT [sundryId],[shopId],[sundryName],[sundryStandard],[sundryChargeMode],[supportChangeQuantity],[status],[price],[description]");
            strSql.Append(" ,required");
            strSql.Append(" FROM SundryInfo");
            strSql.Append(" where shopId=@shopId");
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters);
            if (ds.Tables[0] == null)
            {
                return null;
            }
            return ds.Tables[0];
        }

        /// <summary>
        /// （wangcheng）查询默认餐厅杂项信息
        /// </summary>
        public DataTable SelectDefaultSundryInfo()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT [sundryId],[shopId],[sundryName],[sundryStandard],[sundryChargeMode],[supportChangeQuantity],[status],[price],[description]");
            strSql.Append(" FROM DefauleSundryInfo");
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            if (ds.Tables[0] == null)
            {
                return null;
            }
            return ds.Tables[0];
        }

        /// <summary>
        /// （wangcheng）根据sundryId查询杂项信息
        /// </summary>
        public DataTable SelectSundryInfoBySundryId(long sundryId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT [sundryId],[shopId],[sundryName],[sundryStandard],[sundryChargeMode],[supportChangeQuantity],[status],[price],[description],vipDiscountable,backDiscountable,required");
            strSql.Append(" FROM SundryInfo");
            strSql.AppendFormat("  where [sundryId]={0}", sundryId);
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            if (ds.Tables[0] == null)
            {
                return null;
            }
            return ds.Tables[0];
        }

        /// <summary>
        /// （wangcheng）根据sundryId查询杂项信息
        /// </summary>
        public DataTable SelectSundryInfoBySundryId(string sundryIdStr)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT [sundryId],[shopId],[sundryName],[sundryStandard],[sundryChargeMode],[supportChangeQuantity],[status],[price],[description],vipDiscountable,backDiscountable,required");
            strSql.Append(" FROM SundryInfo");
            strSql.AppendFormat("  where [sundryId] in {0} and status=1", sundryIdStr);
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            if (ds.Tables[0] == null)
            {
                return null;
            }
            return ds.Tables[0];
        }
        /// <summary>
        /// 更新店铺杂项是否开启
        /// </summary>
        /// <param name="adverttisementConnAdColumnId">(1,2,3)</param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool UpdateSundryStatusBySundryId(long sundryId, int status)
        {
            int result = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [SundryInfo] set ");
            if (status == 1)
            {
                strSql.Append("status =" + (int)VASundry.CLOSED + "");
            }
            else
            {
                strSql.Append("status =" + (int)VASundry.OPENED + "");
            }
            strSql.AppendFormat(" where sundryId={0}", sundryId);
            result = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
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
        /// 更新店铺杂项信息（重载不修改状态）
        /// </summary>
        public long UpdateShopSundayInfo(long sundryId, SundryInfo sundryInfo)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int result = 0;
                conn.Open();
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update [SundryInfo] set ");
                strSql.Append(" shopId =@shopId,");
                strSql.Append(" sundryName =@sundryName,");
                strSql.Append(" sundryStandard =@sundryStandard,");
                strSql.Append(" sundryChargeMode =@sundryChargeMode,");
                strSql.Append(" supportChangeQuantity =@supportChangeQuantity,");
                strSql.Append(" price =@price,");
                strSql.Append(" description =@description,");
                strSql.Append(" vipDiscountable =@vipDiscountable,");
                strSql.Append(" required =@required,");
                strSql.Append(" backDiscountable =@backDiscountable");
                strSql.AppendFormat(" where [sundryId]={0}", sundryId);//根据sundryId修改对应的信息
                strSql.Append(" select @@identity");//查询返回主键
                SqlParameter[] parameters = new SqlParameter[]{
					        new SqlParameter("@shopId", SqlDbType.Int,4),
                            new SqlParameter("@sundryName",SqlDbType.NVarChar,20),
                            new SqlParameter("@sundryStandard",SqlDbType.NVarChar,10),
                            new SqlParameter("@sundryChargeMode",SqlDbType.Int),
                            new SqlParameter("@supportChangeQuantity", SqlDbType.Bit,5),
                            new SqlParameter("@price",SqlDbType.Float),
                             new SqlParameter("@description",SqlDbType.NVarChar,300),
                     new SqlParameter("@vipDiscountable",SqlDbType.Bit),
                     new SqlParameter("@required",SqlDbType.Bit),
                            new SqlParameter("@backDiscountable",SqlDbType.Bit)
                    };
                parameters[0].Value = sundryInfo.shopId;
                parameters[1].Value = sundryInfo.sundryName;
                parameters[2].Value = sundryInfo.sundryStandard;
                parameters[3].Value = sundryInfo.sundryChargeMode;
                parameters[4].Value = sundryInfo.supportChangeQuantity;
                parameters[5].Value = sundryInfo.price;
                parameters[6].Value = sundryInfo.description;
                parameters[7].Value = sundryInfo.vipDiscountable;
                parameters[8].Value = sundryInfo.required;
                parameters[9].Value = sundryInfo.backDiscountable;
                result = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);
                return result;
            }
        }
        public long UpdateShopSundayInfo(SundryInfo sundryInfo, long sundryId)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int result = 0;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("update [SundryInfo] set ");
                    strSql.Append(" shopId =@shopId,");
                    strSql.Append(" sundryName =@sundryName,");
                    strSql.Append(" sundryStandard =@sundryStandard,");
                    strSql.Append(" sundryChargeMode =@sundryChargeMode,");
                    strSql.Append(" supportChangeQuantity =@supportChangeQuantity,");
                    strSql.Append(" price =@price,");
                    strSql.Append(" status =@status,");
                    strSql.Append(" description =@description,");
                    strSql.Append(" vipDiscountable =@vipDiscountable,");
                    strSql.Append(" required =@required,");
                    strSql.Append(" backDiscountable =@backDiscountable");
                    strSql.AppendFormat(" where [sundryId]={0}", sundryId);//根据sundryId修改对应的信息
                    strSql.Append(" select @@identity");//查询返回主键
                    SqlParameter[] parameters = new SqlParameter[]{
					        new SqlParameter("@shopId", SqlDbType.Int,4),
                            new SqlParameter("@sundryName",SqlDbType.NVarChar,20),
                            new SqlParameter("@sundryStandard",SqlDbType.NVarChar,10),
                            new SqlParameter("@sundryChargeMode",SqlDbType.Int),
                            new SqlParameter("@supportChangeQuantity", SqlDbType.Bit,5),
                            new SqlParameter("@price",SqlDbType.Float),
                            new SqlParameter("@status",SqlDbType.Int,4),
                             new SqlParameter("@description",SqlDbType.NVarChar,300),
                     new SqlParameter("@vipDiscountable",SqlDbType.Bit),
                     new SqlParameter("@required",SqlDbType.Bit),
                            new SqlParameter("@backDiscountable",SqlDbType.Bit)
                    };

                    parameters[0].Value = sundryInfo.shopId;
                    parameters[1].Value = sundryInfo.sundryName;
                    parameters[2].Value = sundryInfo.sundryStandard;
                    parameters[3].Value = sundryInfo.sundryChargeMode;
                    parameters[4].Value = sundryInfo.supportChangeQuantity;
                    parameters[5].Value = sundryInfo.price;
                    parameters[6].Value = sundryInfo.status;
                    parameters[7].Value = sundryInfo.description;

                    parameters[8].Value = sundryInfo.vipDiscountable;
                    parameters[9].Value = sundryInfo.required;
                    parameters[10].Value = sundryInfo.backDiscountable;
                    result = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);
                }
                catch (System.Exception)
                {
                    return result = 0;
                }
                if (result >= 1)
                {
                    return result;
                }
                else
                {
                    return result = 0;
                }
            }
        }
        /// <summary>
        /// 检测杂项名称是否重复
        /// </summary>
        /// <param name="sundryName"></param>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public bool SelectSundryInfoBySundryName(string sundryName, int shopId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT [sundryId]");
            strSql.Append(" FROM SundryInfo");
            strSql.AppendFormat("  where [sundryName] = '{0}' and shopId={1}", sundryName, shopId);
            using (IDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString()))
            {
                if (dr.Read())
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
        /// 查询门店杂项信息
        /// </summary>
        /// <param name="shopId">门店编号</param>
        /// <returns></returns>
        public DataTable SelectSundryInfoByShop(int shopId)
        {
            const string strSql = @"SELECT sundryId,sundryName,sundryChargeMode,sundryStandard,supportChangeQuantity,price,description,vipDiscountable,backDiscountable,required
  FROM SundryInfo where shopId =@shopId and status=1";
            SqlParameter parameter = new SqlParameter("@shopId", SqlDbType.Int, 4) { Value = shopId };
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameter);
            return ds.Tables[0];
        }
    }
}
