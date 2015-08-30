using System;
using System.Data.Common;
using System.Data.Entity.Infrastructure.Interception;

namespace VAGastronomistMobileApp.SQLServerDAL.Persistence.Infrastructure
{


    public class DbCommandInterceptor : IDbCommandInterceptor
    {
        public void NonQueryExecuting(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
            Log(command, interceptionContext);
        }

        public void NonQueryExecuted(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
            Log(command, interceptionContext);
        }

        public void ReaderExecuting(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
            Log(command, interceptionContext);
        }

        public void ReaderExecuted(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
            //Log(command, interceptionContext);
        }

        public void ScalarExecuting(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
            Log(command, interceptionContext);
        }

        public void ScalarExecuted(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
            //Log(command, interceptionContext);
        }

        private void Log(DbCommand command, DbCommandInterceptionContext interceptionContext)
        {
            if (!interceptionContext.IsAsync)
            {
                Console.WriteLine(command.CommandText);
            }
        }
    }
}
