using System.Data.Entity.Core.Objects;
using EntityFramework.Caching;
using EntityFramework.Extensions;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace VAGastronomistMobileApp.SQLServerDAL.Persistence.Infrastructure
{
    public abstract class EntityFrameworkRepositoryBase<T> : IRepository<T>
       where T : class
    {
        private ViewAllocContext _dataContext;
        private readonly DbSet<T> _dbset;

        protected EntityFrameworkRepositoryBase(IDatabaseFactory databaseFactory)
        {
            DatabaseFactory = databaseFactory;
            _dbset = DataContext.Set<T>();
        }

        protected IDatabaseFactory DatabaseFactory
        {
            get;
            private set;
        }

        protected ViewAllocContext DataContext
        {
            get { return _dataContext ?? (_dataContext = DatabaseFactory.Get()); }
        }

        public virtual void Add(T entity)
        {
            _dbset.Add(entity);
            _dataContext.SaveChanges();
        }

        public void AddRange(IEnumerable<T> entities)
        {
            _dbset.AddRange(entities);
            _dataContext.SaveChanges();
        }

        public virtual void Update(T entity)
        {
            _dbset.Attach(entity);
            _dataContext.Entry(entity).State = EntityState.Modified;
            _dataContext.SaveChanges();


        }

        public virtual void Update(Expression<Func<T, bool>> filterExpression, Expression<Func<T, T>> updateExpression)
        {
            _dbset.Update(filterExpression, updateExpression);
        }


        public virtual void Delete(T entity)
        {
            _dbset.Remove(entity);
            _dataContext.SaveChanges();
        }
        public virtual void Delete(Expression<Func<T, bool>> where)
        {
            _dbset.Where(where).Delete();
            _dataContext.SaveChanges();
        }

        public virtual T GetById(string id)
        {
            return _dbset.Find(id);
        }

        public T GetById(long id)
        {
            return _dbset.Find(id);
        }

        public T Get(Expression<Func<T, bool>> @where)
        {
            return _dbset.AsNoTracking<T>().Where(@where).FirstOrDefault();
        }

        public virtual IEnumerable<T> GetAll()
        {
            return _dbset.AsNoTracking<T>();
        }

        public IEnumerable<T> GetMany(Expression<Func<T, bool>> @where)
        {
            return _dbset.AsNoTracking<T>().Where(where);
        }

        public IPagedList<T> GetPage<TOrder>(Page page, Expression<Func<T, bool>> @where, Expression<Func<T, TOrder>> order)
        {
            var results = _dbset.AsNoTracking<T>().OrderBy(order).Where(where).GetPage(page);
            var total = _dbset.Count(where);
            return new StaticPagedList<T>(results, page.PageNumber, page.PageSize, total);
        }

    }
}