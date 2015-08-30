using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Interception;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Expressions;
using System.Text;
using EntityFramework.Audit;
using EntityFramework.Extensions;
using PagedList;
using VAGastronomistMobileApp.DBUtility;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL.Persistence.Infrastructure;

namespace VAGastronomistMobileApp.SQLServerDAL.Persistence
{
    public interface IDishInfoRepository : IRepository<DishInfo>
    {
        /// <summary>
        /// 随机获取菜单下菜品
        /// </summary>
        /// <param name="menuId"></param>
        /// <returns></returns>
        IEnumerable<Dish> GetRandomDishInfosByMenu(int top, int menuId, params int[] dishIds);
    }

    public class EntityFrameworkDishInfoRepository : EntityFrameworkRepositoryBase<DishInfo>, IDishInfoRepository
    {
        public EntityFrameworkDishInfoRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }


        public IEnumerable<Dish> GetRandomDishInfosByMenu(int top, int menuId, params int[] dishIds)
        {


            var query = from a in DataContext.DishInfos.AsNoTracking()
                        join b in DataContext.ImageInfos.AsNoTracking() on a.DishID equals b.DishID
                        join c in DataContext.DishI18Ns.AsNoTracking() on a.DishID equals c.DishID
                        where a.MenuID == menuId && a.DishStatus == 1 && b.ImageScale == ImageScale.普通图片 && b.ImageStatus == 1 && c.DishI18nStatus == 1 && dishIds.Contains(a.DishID)
                        orderby a.DishDisplaySequence
                        select new Dish { DishId = a.DishID, DishName = c.DishName, Image0 = b.ImageName };

            int count = query.Count();
            if (count > 0)
            {
                if (count > top)
                {
                    Random random = new Random();
                    int index = random.Next(0, count - top + 1);
                    return query.Skip(index).Take(top);
                }
                else
                {
                    return query;
                }
            }
            else
            {
                return new List<Dish>();
            }





        }
    }

    public class SqlServerDishInfoRepository : IDishInfoRepository
    {
        public void Add(DishInfo entity)
        {
            throw new NotImplementedException();
        }

        public void AddRange(IEnumerable<DishInfo> entities)
        {
            throw new NotImplementedException();
        }

        public void Update(DishInfo entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Expression<Func<DishInfo, bool>> filterExpression, Expression<Func<DishInfo, DishInfo>> updateExpression)
        {
            throw new NotImplementedException();
        }

        public void Delete(DishInfo entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Expression<Func<DishInfo, bool>> @where)
        {
            throw new NotImplementedException();
        }

        public DishInfo GetById(string id)
        {
            throw new NotImplementedException();
        }

        public DishInfo GetById(long id)
        {
            const string cmdText = @"SELECT * FROM [dbo].[DishInfo] WHERE [DishID]=@DishID";
            SqlParameter cmdParm = new SqlParameter("@DishID", id);
            DishInfo dishInfo = null;
            using (var dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, cmdText, cmdParm))
            {
                if (dr.Read())
                {
                    dishInfo = SqlHelper.GetEntity<DishInfo>(dr);
                }
            }
            return dishInfo;
        }

        public DishInfo Get(Expression<Func<DishInfo, bool>> @where)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DishInfo> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DishInfo> GetMany(Expression<Func<DishInfo, bool>> @where)
        {
            throw new NotImplementedException();
        }

        public IPagedList<DishInfo> GetPage<TOrder>(Page page, Expression<Func<DishInfo, bool>> @where, Expression<Func<DishInfo, TOrder>> order)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Dish> GetRandomDishInfosByMenu(int top, int menuId, params int[] dishIds)
        {
            var dishIdsXML = new StringBuilder();
            foreach (var dishId in dishIds)
            {
                dishIdsXML.AppendFormat("<row><id>{0}</id></row>", dishId);
            }

            const string cmdTextCount = @"select COUNT(1) from [dbo].[DishInfo] a
INNER JOIN [dbo].[ImageInfo] b on a.DishID=b.DishID
INNER JOIN [dbo].[DishI18n] c on a.DishID=c.DishID
WHERE c.DishI18nStatus=1 AND b.ImageStatus=1 AND b.ImageScale=0 AND a.DishStatus=1 AND a.MenuID=@MenuId AND a.[DishID] NOT IN  (
select d.x.value('./id[1]','int') from @a.nodes('/*') as d(x))";

            SqlParameter[] cmdParmsCount = new SqlParameter[]
            {
                new SqlParameter("@MenuId", menuId),
                new SqlParameter("@a", SqlDbType.Xml) { Value = dishIdsXML.ToString() }
            };
            object obj_count = SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, cmdTextCount, cmdParmsCount);

            int count = Convert.ToInt32(obj_count);
            Random random = new Random();
            int index = 1;
            if (count > top)
            {
                index = random.Next(0, count - top + 1) + 1;
            }

            const string cmdText = @"SELECT t.DishID,t.DishName,t.ImageName as [Image0] FROM (
select a.DishID, c.DishName,b.ImageName,ROW_NUMBER() OVER(ORDER BY a.DishDisplaySequence) as RowNum from [dbo].[DishInfo] a
INNER JOIN [dbo].[ImageInfo] b on a.DishID=b.DishID
INNER JOIN [dbo].[DishI18n] c on a.DishID=c.DishID
WHERE c.DishI18nStatus=1 AND b.ImageStatus=1 AND b.ImageScale=0 AND a.DishStatus=1 AND a.MenuID=@MenuId AND a.[DishID] NOT IN  (
select d.x.value('./id[1]','int') from @a.nodes('/*') as d(x))
) AS t
WHERE t.RowNum BETWEEN @StartIndex AND @EndIndex";

            SqlParameter[] cmdParms = new SqlParameter[]
            {
               new SqlParameter("@MenuId", menuId),
                new SqlParameter("@a", SqlDbType.Xml) { Value = dishIdsXML.ToString() },
                new SqlParameter("@StartIndex", index),
                new SqlParameter("@EndIndex", index + top-1),
            };

            List<Dish> list = new List<Dish>();
            using (var dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, cmdText, cmdParms))
            {
                while (dr.Read())
                {
                    list.Add(SqlHelper.GetEntity<Dish>(dr));
                }
            }
            return list;
        }
    }



}
