using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.Model.Interface;
using VAGastronomistMobileApp.Model.QueryObject;
using VAGastronomistMobileApp.DBUtility;

namespace VAGastronomistMobileApp.SQLServerDAL
{
    public partial class Preorder19DianLineVManager 
    {
        public List<IPreorder19DianLineV> GetListByQueryWithColumns(Preorder19DianLineVQueryObject queryObject,
                                         params Preorder19DianLineVOrderColumn[] columns  )
        {
            StringBuilder sqlBuilder = new StringBuilder(" SELECT ");
            if (columns == null)
            {
                sqlBuilder.Append(" * ");
            }
            else
            {
                foreach (var c in columns)
                {
                    switch (c)
                    {
                        case Preorder19DianLineVOrderColumn.Preorder19DianLineId:
                            sqlBuilder.Append("Preorder19DianLineId,");
                            break;
                        case Preorder19DianLineVOrderColumn.Preorder19DianId:
                            sqlBuilder.Append("Preorder19DianId,");
                            break;
                        case Preorder19DianLineVOrderColumn.CustomerId:
                            sqlBuilder.Append("CustomerId,");
                            break;
                        case Preorder19DianLineVOrderColumn.PayType:
                            sqlBuilder.Append("PayType,");
                            break;
                        case Preorder19DianLineVOrderColumn.PayAccount:
                            sqlBuilder.Append("PayAccount,");
                            break;
                        case Preorder19DianLineVOrderColumn.Amount:
                            sqlBuilder.Append("Amount,");
                            break;
                        case Preorder19DianLineVOrderColumn.CreateTime:
                            sqlBuilder.Append("CreateTime,");
                            break;
                        case Preorder19DianLineVOrderColumn.Remark:
                            sqlBuilder.Append("Remark,");
                            break;
                        case Preorder19DianLineVOrderColumn.State:
                            sqlBuilder.Append("[State],");
                            break;
                        case Preorder19DianLineVOrderColumn.Uuid:
                            sqlBuilder.Append("Uuid,");
                            break;
                        case Preorder19DianLineVOrderColumn.RefundAmount:
                            sqlBuilder.Append("RefundAmount,");
                            break;
                        case Preorder19DianLineVOrderColumn.PreOrderTime:
                            sqlBuilder.Append("preOrderTime,");
                            break;
                        case Preorder19DianLineVOrderColumn.Status:
                            sqlBuilder.Append("[status],");
                            break;
                        case Preorder19DianLineVOrderColumn.CityID:
                            sqlBuilder.Append("cityID,");
                            break;
                        case Preorder19DianLineVOrderColumn.ShopID:
                            sqlBuilder.Append("ShopID,");
                            break;
                    }
                }
                sqlBuilder = sqlBuilder.Remove(sqlBuilder.Length - 1, 1);
            }

            sqlBuilder.Append(" FROM Preorder19DianLineV WHERE 1=1 "); 

            SqlParameter[] sqlParameters = null;
            StringBuilder whereSqlBuilder = new StringBuilder();
            GetWhereSqlBuilderAndSqlParameterList(queryObject, ref sqlParameters, whereSqlBuilder);
            string sql = whereSqlBuilder.Insert(0, sqlBuilder.ToString()).ToString();
            List<IPreorder19DianLineV> list = null;
            using (var reader = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sql, sqlParameters))
            {
                list = new List<IPreorder19DianLineV>();
                while (reader.Read())
                {
                    list.Add(reader.GetEntity<Preorder19DianLineV>());
                }
            }

            return list;
        }
    }
}
