using System;
using System.Data;

namespace VAGastronomistMobileApp.SQLServerDAL.Persistence.Infrastructure
{
    public interface IUnitOfWork : IDisposable
    {
        void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.Unspecified);
        void Commit();
        void Rollback();
        void Close();
    }
}
