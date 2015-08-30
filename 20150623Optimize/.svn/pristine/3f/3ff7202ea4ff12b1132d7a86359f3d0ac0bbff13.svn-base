using System.Data;
using System.Data.SqlClient;
using System.Text;
using VAGastronomistMobileApp.DBUtility;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL.Persistence.Infrastructure;

namespace VAGastronomistMobileApp.SQLServerDAL.Persistence
{
    public class SqlServerEmployeeOperateLogRepository : SqlServerRepositoryBase, IEmployeeOperateLogRepository
    {
        public SqlServerEmployeeOperateLogRepository(ISqlConnectionFactory connectionFactory)
            : base(connectionFactory)
        {
        }

        protected override SqlConnection Connection
        {
            get { return ConnectionFactory.Get(SqlHelper.MobileAppLogConnectionStringLocalTransaction); }
        }

        public void Add(EmployeeOperateLogInfo employeeOperateLogInfo)
        {
            StringBuilder strSql = new StringBuilder();
            SqlParameter[] parameters = null;
            strSql.Append("insert into EmployeeOperateLog(");
            strSql.Append("employeeId,employeeName,pageType,operateType,operateTime,operateDes)");
            strSql.Append(" values (");
            strSql.Append("@employeeId,@employeeName,@pageType,@operateType,@operateTime,@operateDes)");
            strSql.Append(" select @@identity");
            parameters = new SqlParameter[]{
                new SqlParameter("@employeeId",SqlDbType.Int,4),
                new SqlParameter("@employeeName",SqlDbType.NVarChar,100),
                new SqlParameter("@pageType",SqlDbType.Int,4),
                new SqlParameter("@operateType",SqlDbType.Int,4),
                new SqlParameter("@operateTime",SqlDbType.DateTime),
                new SqlParameter("@operateDes",SqlDbType.NVarChar,1000)
            };
            parameters[0].Value = employeeOperateLogInfo.employeeId;
            parameters[1].Value = employeeOperateLogInfo.employeeName;
            parameters[2].Value = employeeOperateLogInfo.pageType;
            parameters[3].Value = employeeOperateLogInfo.operateType;
            parameters[4].Value = employeeOperateLogInfo.operateTime;
            parameters[5].Value = employeeOperateLogInfo.operateDes;
            SqlHelper.ExecuteScalar(Connection, CommandType.Text, strSql.ToString(), parameters);
        }
    }
}