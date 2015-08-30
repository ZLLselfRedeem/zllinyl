using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using PagedList;
using VAGastronomistMobileApp.DBUtility;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL.Persistence.Infrastructure;

namespace VAGastronomistMobileApp.SQLServerDAL.Persistence
{
    public interface IDishI18NRepository : IRepository<DishI18n>
    {
        /// <summary>
        /// 获取指定ID的菜品列表
        /// </summary>
        /// <param name="dishIds"></param>
        /// <returns></returns>
        IEnumerable<DishI18n> GetDishI18NsByDishIds(params int[] dishIds);
    }

    public class EntityFrameworkDishI18NRepository : EntityFrameworkRepositoryBase<DishI18n>, IDishI18NRepository
    {
        public EntityFrameworkDishI18NRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        public IEnumerable<DishI18n> GetDishI18NsByDishIds(params int[] dishIds)
        {
            return GetMany(p => dishIds.Contains(p.DishID) && p.DishI18nStatus == 1);
        }
    }

    public class SqlServerDishI18NRepository : IDishI18NRepository
    {
        public void Add(DishI18n entity)
        {
            throw new NotImplementedException();
        }

        public void AddRange(IEnumerable<DishI18n> entities)
        {
            throw new NotImplementedException();
        }

        public void Update(DishI18n entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Expression<Func<DishI18n, bool>> filterExpression, Expression<Func<DishI18n, DishI18n>> updateExpression)
        {
            throw new NotImplementedException();
        }

        public void Delete(DishI18n entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Expression<Func<DishI18n, bool>> @where)
        {
            throw new NotImplementedException();
        }

        public DishI18n GetById(string id)
        {
            throw new NotImplementedException();
        }

        public DishI18n GetById(long id)
        {
            throw new NotImplementedException();
        }

        public DishI18n Get(Expression<Func<DishI18n, bool>> @where)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DishI18n> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DishI18n> GetMany(Expression<Func<DishI18n, bool>> @where)
        {
            throw new NotImplementedException();
        }

        public IPagedList<DishI18n> GetPage<TOrder>(Page page, Expression<Func<DishI18n, bool>> @where, Expression<Func<DishI18n, TOrder>> order)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DishI18n> GetDishI18NsByDishIds(params int[] dishIds)
        {
            const string cmdText = @"SELECT [DishI18nID]
      ,[DishID]
      ,[LangID]
      ,[DishName]
      ,[DishDescShort]
      ,[DishDescDetail]
      ,[DishHistory]
      ,[DishI18nStatus]
      ,[dishQuanPin]
      ,[dishJianPin]
  FROM [dbo].[DishI18n] WHERE [DishI18nStatus]=1 AND [DishID] IN (
  select d.x.value('./id[1]','int') from @a.nodes('/*') as d(x))";

            var dishIdsXML = new StringBuilder();
            foreach (var dishId in dishIds)
            {
                dishIdsXML.AppendFormat("<row><id>{0}</id></row>", dishId);
            }

            var cmdParm = new SqlParameter("@a", SqlDbType.Xml) { Value = dishIdsXML.ToString() };

            List<DishI18n> list = new List<DishI18n>();
            using (var dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, cmdText, cmdParm))
            {
                while (dr.Read())
                {
                    list.Add(SqlHelper.GetEntity<DishI18n>(dr));
                }
            }
            return list;
        }
    }
}
