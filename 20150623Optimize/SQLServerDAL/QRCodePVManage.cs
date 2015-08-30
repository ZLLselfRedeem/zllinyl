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
    /// 下载页面访问量统计 数据访问层
    /// 20140117 jinyanni
    /// </summary>
    public class QRCodePVManage
    {
        /// <summary>
        /// 插入通过扫二维码访问下载页面的记录
        /// </summary>
        /// <param name="qrCodePageView"></param>
        /// <returns></returns>
        public bool InsertQRCodePV(QRCodePageView qrCodePageView)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into QRCodePageView");
                strSql.Append("(typeId,shopId,visitTime)");
                strSql.Append("values(@typeId,@shopId,@visitTime);");

                using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
                {
                    conn.Open();
  
                    SqlParameter[] para = new SqlParameter[] { 
                        new SqlParameter("@typeId",SqlDbType.Int),
                        new SqlParameter("@shopId",SqlDbType.Int),
                        new SqlParameter("@visitTime",SqlDbType.DateTime)
                    };

                    para[0].Value = qrCodePageView.typeId;
                    para[1].Value = qrCodePageView.shopId;
                    para[2].Value = qrCodePageView.visitTime;

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
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 查询指定条件下的“下载页面”访问记录
        /// </summary>
        /// <param name="typeId">类别ID</param>
        /// <param name="companyId">公司ID</param>
        /// <param name="shopId">店铺ID</param>
        /// <param name="timeFrom">起始时间</param>
        /// <param name="timeTo">结束时间</param>
        /// <returns>符合条件的集合</returns>
        public DataTable QueryQRCodePV(int typeId, int companyId, int shopId, DateTime timeFrom, DateTime timeTo)
        {
            DataTable dt = new DataTable();
            try
            {
                StringBuilder strSql = new StringBuilder();
                string begin = "select pvId,typeId,name,companyId,companyName,shopId,shopName,visitTime from (";

                if (typeId < 3)
                {
                    strSql.Append(" select Q.pvId,Q.typeId,T.name,C.companyId,C.companyName,S.shopId,S.shopName,Q.visitTime");
                    strSql.Append(" from QRCodeType T inner join QRCodePageView Q");
                    strSql.Append(" on T.id = Q.typeId and T.status = 1 and Q.shopId!=0 inner join ShopInfo S");
                    strSql.Append(" on Q.shopId = S.shopID and S.isHandle = 1 and S.shopStatus = 1");
                    strSql.Append(" inner join CompanyInfo C on S.companyID = C.companyID and C.companyStatus = 1");

                    if (typeId != 0 && typeId != 3)//3：官网
                    {
                        strSql.AppendFormat(" and T.Id='{0}'", typeId);
                    }
                    if (companyId != 0)
                    {
                        strSql.AppendFormat(" and C.companyID='{0}'", companyId);
                    }
                    if (shopId != 0)
                    {
                        strSql.AppendFormat(" and S.shopId='{0}'", shopId);
                    }
                }
                if (typeId == 0 && companyId == 0 && shopId == 0)
                {
                    strSql.Append(" union");
                }
                if ((companyId == 0 && shopId == 0 && typeId == 0) || (companyId == 0 && shopId == 0 && typeId >= 3))
                {
                    strSql.Append(" select Q.pvId,Q.typeId,T.name,null companyId,null companyName,null shopId,null shopName,Q.visitTime");
                    strSql.Append(" from QRCodeType T inner join QRCodePageView Q");
                    strSql.Append(" on T.id = Q.typeId and T.status = 1");
                    if (typeId == 0)
                    {
                        strSql.Append("  and T.Id>=3");
                    }
                    if (typeId >= 3)
                    {
                        strSql.AppendFormat("  and T.Id='{0}'", typeId);
                    }
                }
                string end = ") A where A.visitTime between @timeFrom and @timeTo order by typeId asc,companyId asc,shopId asc,visitTime desc";

                if (strSql.ToString() != "")
                {
                    end = begin + strSql.ToString() + end;

                    SqlParameter[] para = new SqlParameter[] { 
                    new SqlParameter("@timeFrom", timeFrom),
                    new SqlParameter("@timeTo", timeTo)
                    };

                    DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, end, para);
                    return ds.Tables[0];
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
