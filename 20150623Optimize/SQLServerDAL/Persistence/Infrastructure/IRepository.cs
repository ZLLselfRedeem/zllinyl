using PagedList;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace VAGastronomistMobileApp.SQLServerDAL.Persistence.Infrastructure
{
    public interface IRepository<T>
       where T : class
    {
        void Add(T entity);

        void AddRange(IEnumerable<T> entities);
        void Update(T entity);
        void Update(Expression<Func<T, bool>> filterExpression, Expression<Func<T, T>> updateExpression);
        void Delete(T entity);
        void Delete(Expression<Func<T, bool>> where);
        T GetById(string id);
        T GetById(long id);
        T Get(Expression<Func<T, bool>> where);
        IEnumerable<T> GetAll();
        IEnumerable<T> GetMany(Expression<Func<T, bool>> where);
        IPagedList<T> GetPage<TOrder>(Page page, Expression<Func<T, bool>> where, Expression<Func<T, TOrder>> order);
    }
}
