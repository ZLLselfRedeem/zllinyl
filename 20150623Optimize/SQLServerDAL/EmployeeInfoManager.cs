using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.DBUtility;
using VAGastronomistMobileApp.Model;

namespace VAGastronomistMobileApp.SQLServerDAL
{
    public class EmployeeInfoManager
    {
        public void UpdatePushToken(string cookie, string pushToken, VAServiceType appType)
        {
            StringBuilder strSqlBuilder = new StringBuilder(@"UPDATE [dbo].[EmployeeInfo] SET [pushToken]=@pushToken,[AppType]=@AppType where [cookie]=@cookie");
            SqlParameter[] cmdParms = new SqlParameter[]
            {
                new SqlParameter("@pushToken", pushToken),
                new SqlParameter("@cookie", cookie),
                new SqlParameter("@AppType", (int)appType)
            };

            SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text,
                strSqlBuilder.ToString(), cmdParms);
        }
    }
}
