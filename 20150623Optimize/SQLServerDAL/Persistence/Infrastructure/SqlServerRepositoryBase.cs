using System.Data.SqlClient;

namespace VAGastronomistMobileApp.SQLServerDAL.Persistence.Infrastructure
{
    public abstract class SqlServerRepositoryBase
    {
        //private SqlConnection _connection;

        protected SqlServerRepositoryBase(ISqlConnectionFactory connectionFactory)
        {
            ConnectionFactory = connectionFactory;
        }

        protected ISqlConnectionFactory ConnectionFactory
        {
            get;
            private set;
        }
        protected virtual SqlConnection Connection
        {
            get { return ConnectionFactory.Get(); }
        }
    }
}
