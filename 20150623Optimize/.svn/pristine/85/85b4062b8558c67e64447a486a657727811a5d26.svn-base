
using System.Data;
using System.Data.Entity;

namespace VAGastronomistMobileApp.SQLServerDAL.Persistence.Infrastructure
{
    public class UnitOfWork : Disposable, IUnitOfWork
    {
        private readonly IDatabaseFactory _databaseFactory;
        private ViewAllocContext _dataContext;
        private DbContextTransaction _currentTransaction;

        public UnitOfWork(IDatabaseFactory databaseFactory)
        {
            this._databaseFactory = databaseFactory;
        }

        protected ViewAllocContext DataContext
        {
            get { return _dataContext ?? (_dataContext = _databaseFactory.Get()); }
        }


        public void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.Unspecified)
        {
            _currentTransaction = DataContext.Database.BeginTransaction(isolationLevel);
        }

        public void Commit()
        {
            _currentTransaction.Commit();
        }
        public void Rollback()
        {
            _currentTransaction.Rollback();
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
