using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using VAGastronomistMobileApp.DBUtility;
using VAGastronomistMobileApp.Model;
using System.Data.SqlClient;

namespace VAGastronomistMobileApp.SQLServerDAL
{
    /// <summary>
    /// 二维码相关操作 数据访问层
    /// 20140115 jinyanni
    /// </summary>
    public partial class QRCodeManage
    {
        /// <summary>
        /// 查询店铺二维码类型
        /// </summary>
        /// <returns></returns>
        public DataTable QueryQRCodeShopType()
        {
            StringBuilder strSql = new StringBuilder();
            try
            {
                strSql.Append("select id,name");
                strSql.Append(" from QRCodeType");
                strSql.Append(" where status = 1 and remark = 'shop' order by id;");

                DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
                return ds.Tables[0];
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 查询所有二维码类型
        /// </summary>
        /// <returns></returns>
        public DataTable QueryQRCodeType()
        {
            StringBuilder strSql = new StringBuilder();
            try
            {
                strSql.Append("select id,name");
                strSql.Append(" from QRCodeType");
                strSql.Append(" where status = 1 order by id;");

                DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
                return ds.Tables[0];
            }
            catch (Exception)
            {
                return null;
            }
        } 

        /// <summary>
        /// 根据shopId查询对应的二维码
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public List<QRCodeConnShop> QueryQRByShopId(int shopId)
        {
            List<QRCodeConnShop> QRCodeConnShops = new List<QRCodeConnShop>();
            StringBuilder strSql = new StringBuilder();
            try
            {
                strSql.Append("select typeId,QRCodeImage,imageName");
                strSql.Append(" from QRCodeConnShop");
                strSql.Append(" where shopId=@shopId and status=1;");

                SqlParameter[] para = new SqlParameter[] { 
                    new SqlParameter("@shopId",SqlDbType.Int)
                };

                para[0].Value = shopId;

                using (SqlDataReader odr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), para))
                {
                    while (odr.Read())
                    {
                        QRCodeConnShop qrCodeConnShop = new QRCodeConnShop();
                        qrCodeConnShop.typeId = odr.GetInt32(0);
                        qrCodeConnShop.QRCodeImage = odr.GetString(1);
                        qrCodeConnShop.imageName = odr.GetString(2);

                        QRCodeConnShops.Add(qrCodeConnShop);
                    }
                }
                return QRCodeConnShops;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 保存店铺的二维码信息
        /// </summary>
        /// <param name="QRCodeConnShop"></param>
        /// <returns></returns>
        public bool InsertQRCodeConnShop(QRCodeConnShop QRCodeConnShop)
        {
            object obj = null;
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into QRCodeConnShop(");
                strSql.Append("shopId,typeId,QRCodeImage,status,imageName)");
                strSql.Append("values(@shopId,@typeId,@QRCodeImage,@status,@imageName)");
                strSql.Append(" select @@identity");

                using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
                {
                    conn.Open();

                    SqlParameter[] para = new SqlParameter[] { 
                    new SqlParameter("@shopId",SqlDbType.BigInt),
                    new SqlParameter("@typeId",SqlDbType.Int),
                    new SqlParameter("@QRCodeImage",SqlDbType.NVarChar,500),
                    new SqlParameter("@status",SqlDbType.Int),
                    new SqlParameter("@imageName",SqlDbType.NVarChar,100)
                    };

                    para[0].Value = QRCodeConnShop.shopId;
                    para[1].Value = QRCodeConnShop.typeId;
                    para[2].Value = QRCodeConnShop.QRCodeImage;
                    para[3].Value = QRCodeConnShop.status;
                    para[4].Value = QRCodeConnShop.imageName;

                    obj = SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql.ToString(), para);
                }
                if (obj == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception)
            {
                return true;
            }
        }

        /// <summary>
        /// 修改店铺的二维码信息
        /// </summary>
        /// <param name="qrCodeConnShop"></param>
        /// <returns></returns>
        public bool UpdateQRConnShop(QRCodeConnShop qrCodeConnShop)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update QRCodeConnShop");
                strSql.Append(" set QRCodeImage=@QRCodeImage,imageName=@imageName");
                strSql.Append(" where shopId=@shopId and typeId=@typeId ");

                using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
                {
                    conn.Open();

                    SqlParameter[] para = new SqlParameter[] { 
                        new SqlParameter("",SqlDbType.NVarChar,500),
                        new SqlParameter("",SqlDbType.Int),
                        new SqlParameter("",SqlDbType.Int)
                    };

                    para[0].Value = qrCodeConnShop.QRCodeImage;
                    para[1].Value = qrCodeConnShop.shopId;
                    para[2].Value = qrCodeConnShop.typeId;

                    int result = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), para);

                    if (result > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 删除店铺的二维码信息
        /// </summary>
        /// <param name="qrCodeConnShop"></param>
        /// <returns></returns>
        public bool DeleteQRConnShop(QRCodeConnShop qrCodeConnShop)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("delete from QRCodeConnShop");
                strSql.Append(" where shopId=@shopId and typeId=@typeId ");

                using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
                {
                    conn.Open();

                    SqlParameter[] para = new SqlParameter[] { 
                        new SqlParameter("@shopId",SqlDbType.Int),
                        new SqlParameter("@typeId",SqlDbType.Int)
                    };

                    para[0].Value = qrCodeConnShop.shopId;
                    para[1].Value = qrCodeConnShop.typeId;

                    int result = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), para);

                    if (result >= 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
