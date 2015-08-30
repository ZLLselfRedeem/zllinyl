using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.Model;
using System.Data.SqlClient;
using System.Data;
using VAGastronomistMobileApp.DBUtility;

namespace VAGastronomistMobileApp.SQLServerDAL
{
    public class AwardConfigManager
    {
        /// <summary>
        /// 获取商家对应的奖品列表
        /// </summary>
        /// <param name="shopID"></param>
        /// <returns></returns>
        public AwardConfig GetAwardConfig(string configName)
        {
            const string strSql = "select * from AwardConfig where ConfigName=@configName";
            SqlParameter[] param ={
                                      new SqlParameter("@configName",configName)
                                  };
            AwardConfig objAwardConfig = new AwardConfig();
            using (SqlDataReader sdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), param))
            {
                if (sdr.Read())
                {
                    objAwardConfig = SqlHelper.GetEntity<AwardConfig>(sdr);
                }
            }
            return objAwardConfig;
        }
    }
}
