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
    public interface IShopInfoRepository : IRepository<ShopInfo>
    {
    }

    public class EntityFrameworkShopInfoRepository : EntityFrameworkRepositoryBase<ShopInfo>, IShopInfoRepository
    {
        public EntityFrameworkShopInfoRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }

    public class SqlServerShopInfoRepository : IShopInfoRepository
    {
        public void Add(ShopInfo entity)
        {
            throw new NotImplementedException();
        }

        public void AddRange(IEnumerable<ShopInfo> entities)
        {
            throw new NotImplementedException();
        }

        public void Update(ShopInfo entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Expression<Func<ShopInfo, bool>> filterExpression, Expression<Func<ShopInfo, ShopInfo>> updateExpression)
        {
            throw new NotImplementedException();
        }

        public void Delete(ShopInfo entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Expression<Func<ShopInfo, bool>> @where)
        {
            throw new NotImplementedException();
        }

        public ShopInfo GetById(string id)
        {
            throw new NotImplementedException();
        }

        public ShopInfo GetById(long id)
        {
            const string cmdText = @"SELECT * FROM [dbo].[ShopInfo] WHERE [shopID]=@shopID";
            SqlParameter cmdParm = new SqlParameter("@shopID", id);

            ShopInfo shopInfo = null;
            using (var dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, cmdText, cmdParm))
            {
                if (dr.Read())
                {
                    shopInfo = SqlHelper.GetEntity<ShopInfo>(dr);
                }
            }
            return shopInfo;
        }

        public ShopInfo Get(Expression<Func<ShopInfo, bool>> @where)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ShopInfo> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ShopInfo> GetMany(Expression<Func<ShopInfo, bool>> @where)
        {
            throw new NotImplementedException();
        }

        public IPagedList<ShopInfo> GetPage<TOrder>(Page page, Expression<Func<ShopInfo, bool>> @where, Expression<Func<ShopInfo, TOrder>> order)
        {
            throw new NotImplementedException();
        }
    }
}
