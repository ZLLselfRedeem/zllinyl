using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Threading;
using VAGastronomistMobileApp.DBUtility;

namespace VAGastronomistMobileApp.SQLServerDAL.Persistence.Infrastructure
{
    public class SqlConnectionFactory : Disposable, ISqlConnectionFactory
    {
        //private SqlConnection _connection;
        readonly Dictionary<string, SqlConnection> _dictionary = new Dictionary<string, SqlConnection>();
        ReaderWriterLockSlim rwLock = new ReaderWriterLockSlim();
        public SqlConnection Get()
        {
            return Get(SqlHelper.ConnectionStringLocalTransaction);
        }

        public SqlConnection Get(string connectionString)
        {
            SqlConnection connection = null;
            //rwLock.EnterReadLock();
            _dictionary.TryGetValue(connectionString, out connection);
            //rwLock.ExitReadLock();

            if (connection == null)
            {
                //rwLock.EnterWriteLock();
                connection = new SqlConnection(connectionString);
                _dictionary.Add(connectionString, connection);
                //rwLock.ExitWriteLock();
            }

            return connection;
        }

        protected override void DisposeCore()
        {
            if (_dictionary.Count > 0)
            {
                foreach (var kv in _dictionary)
                {
                    var conn = kv.Value;
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                    conn.Dispose();
                    //File.AppendAllText(@"c:\log.txt", "我来了");
                }
                _dictionary.Clear();

            }

        }
    }
}
