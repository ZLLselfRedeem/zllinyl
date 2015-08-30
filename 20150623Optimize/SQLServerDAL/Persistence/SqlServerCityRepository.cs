using System.Data;
using System.Data.SqlClient;
using VAGastronomistMobileApp.DBUtility;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL.Persistence.Infrastructure;

namespace VAGastronomistMobileApp.SQLServerDAL.Persistence
{
    public class SqlServerCityRepository : SqlServerRepositoryBase, ICityRepository
    {
        public SqlServerCityRepository(ISqlConnectionFactory connectionFactory)
            : base(connectionFactory)
        {
        }

        public City GetById(long id)
        {
            const string cmdText = @"SELECT * FROM [dbo].[City] WHERE [cityID]=@cityID";
            SqlParameter cmdParm = new SqlParameter("@cityID", id);

            City city = null;
            using (var dr = SqlHelper.ExecuteReader(Connection, CommandType.Text, cmdText, cmdParm))
            {
                if (dr.Read())
                {
                    city = SqlHelper.GetEntity<City>(dr);
                }
            }
            return city;
        }
    }
}