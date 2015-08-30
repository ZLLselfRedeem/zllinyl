using System.Transactions;
using IsolationLevel = System.Data.IsolationLevel;

namespace VAGastronomistMobileApp.SQLServerDAL.Persistence.Infrastructure
{
    public class SqlServerUnitOfWork : Disposable, IUnitOfWork
    {
        private TransactionScope _currentTransaction;
        public SqlServerUnitOfWork()
        {

        }

        public void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.Unspecified)
        {
            _currentTransaction = new TransactionScope();
        }

        public void Commit()
        {
            _currentTransaction.Complete();
        }

        public void Rollback()
        {

        }

        public void Close()
        {
            Dispose();
        }

        protected override void DisposeCore()
        {
            if (_currentTransaction != null)
                _currentTransaction.Dispose();
        }
    }
}
