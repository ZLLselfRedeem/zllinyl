using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VAGastronomistMobileApp.DBUtility;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.Model.Interface;
using VAGastronomistMobileApp.Model.QueryObject;

namespace VAGastronomistMobileApp.SQLServerDAL
{
    public partial class Preorder19DianLineManager
    {
        public bool AddList(List<Preorder19DianLine> list)
        {
            if (list != null && list.Count > 0)
            {
                DataTable insertTable = list.GetTableFromList();
                //using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(SqlHelper.ConnectionStringLocalTransaction))
                //{
                //    sqlBulkCopy.DestinationTableName = "Preorder19DianLine";
                //    foreach (DataColumn column in insertTable.Columns)
                //    {
                //        sqlBulkCopy.ColumnMappings.Add(column.ColumnName.Trim(), column.ColumnName.Trim());
                //    }
                //    sqlBulkCopy.WriteToServer(insertTable);
                //    return true;
                //}
                foreach (var item in list)
                {
                    this.Add(item);
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 查询某点单的第三方支付金额
        /// </summary>
        /// <param name="preOrder19dianId"></param>
        /// <returns></returns>
        public double SelectThirdPayAmountOfOrder(long preOrder19dianId)
        {
            string strSql = @"select ISNULL(SUM(Amount),0) from Preorder19DianLine where Preorder19DianId=@preOrder19dianId
and PayType in (" + (int)VAOrderUsedPayMode.ALIPAY + "," + (int)VAOrderUsedPayMode.WECHAT + "," + (int)VAOrderUsedPayMode.BALANCE + ")";
            
            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@preOrder19dianId", SqlDbType.BigInt) { Value = preOrder19dianId }
            };
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                object obj = SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql, para);
                if (obj == null)
                {
                    return 0;
                }
                else
                {
                    return Convert.ToDouble(obj);
                }
            }
        }
    }
}
