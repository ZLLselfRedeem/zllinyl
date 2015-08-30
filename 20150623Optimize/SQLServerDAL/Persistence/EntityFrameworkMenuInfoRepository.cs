using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq.Expressions;
using PagedList;
using VAGastronomistMobileApp.DBUtility;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL.Persistence.Infrastructure;

namespace VAGastronomistMobileApp.SQLServerDAL.Persistence
{
    public interface IMenuInfoRepository : IRepository<MenuInfo>
    {
    }

    public class EntityFrameworkMenuInfoRepository : EntityFrameworkRepositoryBase<MenuInfo>, IMenuInfoRepository
    {
        public EntityFrameworkMenuInfoRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }

    public class SqlServerMenuInfoRepository : IMenuInfoRepository
    {
        public void Add(MenuInfo entity)
        {
            throw new NotImplementedException();
        }

        public void AddRange(IEnumerable<MenuInfo> entities)
        {
            throw new NotImplementedException();
        }

        public void Update(MenuInfo entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Expression<Func<MenuInfo, bool>> filterExpression, Expression<Func<MenuInfo, MenuInfo>> updateExpression)
        {
            throw new NotImplementedException();
        }

        public void Delete(MenuInfo entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Expression<Func<MenuInfo, bool>> @where)
        {
            throw new NotImplementedException();
        }

        public MenuInfo GetById(string id)
        {
            throw new NotImplementedException();
        }

        public MenuInfo GetById(long id)
        {
            const string cmdText = @"SELECT * FROM [dbo].[MenuInfo] WHERE [MenuID]=@MenuID";
            var cmdParm = new SqlParameter("@MenuID", id);

            MenuInfo menuInfo = null;
            using (var dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, cmdText, cmdParm))
            {
                if (dr.Read())
                {
                    menuInfo = SqlHelper.GetEntity<MenuInfo>(dr);
                }
            }
            return menuInfo;
        }

        public MenuInfo Get(Expression<Func<MenuInfo, bool>> @where)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<MenuInfo> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<MenuInfo> GetMany(Expression<Func<MenuInfo, bool>> @where)
        {
            throw new NotImplementedException();
        }

        public IPagedList<MenuInfo> GetPage<TOrder>(Page page, Expression<Func<MenuInfo, bool>> @where, Expression<Func<MenuInfo, TOrder>> order)
        {
            throw new NotImplementedException();
        }
    }
}
