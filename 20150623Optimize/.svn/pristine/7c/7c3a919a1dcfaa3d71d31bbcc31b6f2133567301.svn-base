using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq.Expressions;
using PagedList;
using VAGastronomistMobileApp.DBUtility;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL.Persistence.Infrastructure;

namespace VAGastronomistMobileApp.SQLServerDAL.Persistence
{
    public class SqlServerEmployeeInfoRepository : SqlServerRepositoryBase, IEmployeeInfoRepository
    {
        public SqlServerEmployeeInfoRepository(ISqlConnectionFactory connectionFactory)
            : base(connectionFactory)
        {
        }

        public EmployeeInfo GetById(long id)
        {
            const string cmdText = @"SELECT * FROM [dbo].[EmployeeInfo] WHERE [EmployeeID]=@EmployeeID";

            SqlParameter cmdParm = new SqlParameter("@EmployeeID", id);
            EmployeeInfo employeeInfo = new EmployeeInfo();
            using (var dr = SqlHelper.ExecuteReader(Connection, CommandType.Text, cmdText, cmdParm))
            {
                if (dr.Read())
                {
                    employeeInfo = SqlHelper.GetEntity<EmployeeInfo>(dr);
                }
            }

            return employeeInfo;

        }
    }
}