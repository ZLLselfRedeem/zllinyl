using System.Data.SqlClient;

namespace VAGastronomistMobileApp.SQLServerDAL.Persistence.Infrastructure
{
    public interface IDatabaseFactory<out T> 
    {
        T Get();
    }



    public interface ISqlConnectionFactory : IDatabaseFactory<SqlConnection>
    {
        SqlConnection Get(string connectionString);
    }
}
