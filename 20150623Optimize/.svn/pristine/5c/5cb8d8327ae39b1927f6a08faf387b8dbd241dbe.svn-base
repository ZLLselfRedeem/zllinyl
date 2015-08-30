using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.DBUtility;

namespace VAGastronomistMobileApp.SQLServerDAL
{
    /// <summary>
    /// 菜备注数据库操作类
    /// </summary>
    public class DishOptionManager
    {
        /// <summary>
        /// 查询所有备注分类信息
        /// </summary>
        /// <returns></returns>
        public DataTable SelectDishOptionType()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select [id],[optionName],[internalName]");
            strSql.Append(" FROM DishOptionType");

            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());

            return ds.Tables[0];
        }
        /// <summary>
        /// 根据备注分类编号查询对应的备注详情
        /// </summary>
        /// <returns></returns>
        public DataTable SelectDishOptionDetail(int optionTypeId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select [id],[optionTypeId],[optionDetailName]");
            strSql.Append(" FROM DishOptionDetail");
            strSql.AppendFormat(" where optionTypeId = {0}", optionTypeId);

            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());

            return ds.Tables[0];
        }
    }
}
