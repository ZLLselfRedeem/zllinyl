using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.DBUtility;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL.Persistence.Infrastructure;

namespace VAGastronomistMobileApp.SQLServerDAL.Persistence
{
    internal class SqlServerViewAllocEmployeeAuthorityRepository : SqlServerRepositoryBase,
        IViewAllocEmployeeAuthorityRepository
    {
        public SqlServerViewAllocEmployeeAuthorityRepository(ISqlConnectionFactory connectionFactory)
            : base(connectionFactory)
        {
        }

        public void Add(ViewAllocEmployeeAuthority viewAllocEmployeeAuthority)
        {
            const string cmdText =
                @"INSERT INTO [dbo].[ViewAllocEmployeeAuthority] ([employeeId],[shopAuthorityId],[status]) 
                                    VALUES (@employeeId,@shopAuthorityId,1)";
            SqlParameter[] cmdParms = new[]
            {
                new SqlParameter("@employeeId", SqlDbType.Int) {Value = viewAllocEmployeeAuthority.EmployeeId},
                new SqlParameter("@shopAuthorityId", SqlDbType.Int) {Value = viewAllocEmployeeAuthority.ShopAuthorityId}
            };

            SqlHelper.ExecuteNonQuery(Connection, CommandType.Text, cmdText, cmdParms);
        }

        public IEnumerable<ViewAllocEmployeeAuthority> GetViewAllocEmployeeAuthorityByEmployeeAndShopAuthority(
            int employeeId, int shopAuthorityId)
        {
            const string cmdText =
                @"SELECT * FROM [dbo].[ViewAllocEmployeeAuthority] WHERE [employeeId]=@employeeId  AND [shopAuthorityId]=@shopAuthorityId";
            SqlParameter[] cmdParms = new[]
            {
                new SqlParameter("@employeeId", SqlDbType.Int) {Value = employeeId},
                new SqlParameter("@shopAuthorityId", SqlDbType.Int) {Value = shopAuthorityId}
            };
            List<ViewAllocEmployeeAuthority> viewAllocEmployeeAuthority = new List<ViewAllocEmployeeAuthority>();
            using (var dr = SqlHelper.ExecuteReader(Connection, CommandType.Text, cmdText, cmdParms))
            {
                while (dr.Read())
                {
                    viewAllocEmployeeAuthority.Add(SqlHelper.GetEntity<ViewAllocEmployeeAuthority>(dr));
                }

            }
            return viewAllocEmployeeAuthority;
        }

        public void Update(ViewAllocEmployeeAuthority viewAllocEmployeeAuthority)
        {
            const String cmdText =
                @"UPDATE [dbo].[ViewAllocEmployeeAuthority] SET [employeeId]=@employeeId,[shopAuthorityId]=@shopAuthorityId,[status]=@status WHERE [Id]=@Id";

            SqlParameter[] cmdParms = new[]
            {
                new SqlParameter("@employeeId", SqlDbType.Int) {Value = viewAllocEmployeeAuthority.EmployeeId},
                new SqlParameter("@shopAuthorityId", SqlDbType.Int) {Value = viewAllocEmployeeAuthority.ShopAuthorityId},
                new SqlParameter("@status", SqlDbType.Bit) {Value = viewAllocEmployeeAuthority.Status},
                new SqlParameter("@Id", SqlDbType.Int) {Value = viewAllocEmployeeAuthority.Id},
            };

            SqlHelper.ExecuteNonQuery(Connection, CommandType.Text, cmdText, cmdParms);
        }

        public void Delete(ViewAllocEmployeeAuthority viewAllocEmployeeAuthority)
        {
            const string cmdText = "DELETE [dbo].[ViewAllocEmployeeAuthority] WHERE [Id]=@Id";
            SqlParameter cmdParm = new SqlParameter("@Id", SqlDbType.Int) { Value = viewAllocEmployeeAuthority.Id };

            SqlHelper.ExecuteNonQuery(Connection, CommandType.Text, cmdText, cmdParm);
        }
    }
}
