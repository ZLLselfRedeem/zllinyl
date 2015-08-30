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
    public interface IImageInfoRepository : IRepository<ImageInfo>
    {
        /// <summary>
        /// 获取指定规格菜品图片
        /// </summary>
        /// <param name="scale"></param>
        /// <param name="dishIds"></param>
        /// <returns></returns>
        IEnumerable<ImageInfo> GetAssignScaleImageInfosByDishId(ImageScale scale, params int[] dishIds);
    }

    public class EntityFrameworkImageInfoRepository : EntityFrameworkRepositoryBase<ImageInfo>, IImageInfoRepository
    {
        public EntityFrameworkImageInfoRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        public IEnumerable<ImageInfo> GetAssignScaleImageInfosByDishId(ImageScale scale, params int[] dishIds)
        {
            return GetMany(p => dishIds.Contains(p.DishID) && p.ImageScale == scale && p.ImageStatus == 1);
        }
    }

    public class SqlServerImageInfoRepository : IImageInfoRepository
    {
        public void Add(ImageInfo entity)
        {
            throw new NotImplementedException();
        }

        public void AddRange(IEnumerable<ImageInfo> entities)
        {
            throw new NotImplementedException();
        }

        public void Update(ImageInfo entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Expression<Func<ImageInfo, bool>> filterExpression, Expression<Func<ImageInfo, ImageInfo>> updateExpression)
        {
            throw new NotImplementedException();
        }

        public void Delete(ImageInfo entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Expression<Func<ImageInfo, bool>> @where)
        {
            throw new NotImplementedException();
        }

        public ImageInfo GetById(string id)
        {
            throw new NotImplementedException();
        }

        public ImageInfo GetById(long id)
        {
            throw new NotImplementedException();
        }

        public ImageInfo Get(Expression<Func<ImageInfo, bool>> @where)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ImageInfo> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ImageInfo> GetMany(Expression<Func<ImageInfo, bool>> @where)
        {
            throw new NotImplementedException();
        }

        public IPagedList<ImageInfo> GetPage<TOrder>(Page page, Expression<Func<ImageInfo, bool>> @where, Expression<Func<ImageInfo, TOrder>> order)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ImageInfo> GetAssignScaleImageInfosByDishId(ImageScale scale, params int[] dishIds)
        {
            const string cmdText = @"SELECT [ImageID]
      ,[DishID]
      ,[ImageName]
      ,[ImageSequence]
      ,[ImageScale]
      ,[ImageStatus]
      ,[ImageXY]
  FROM [dbo].[ImageInfo] WHERE [ImageStatus]=1 AND [ImageScale]=@ImageScale AND [DishID] IN  (
select d.x.value('./id[1]','int') from @a.nodes('/*') as d(x))";

            StringBuilder dishIdsXML = new StringBuilder();
            foreach (var dishId in dishIds)
            {
                dishIdsXML.AppendFormat("<row><id>{0}</id></row>", dishId);
            }

            SqlParameter[] cmdParms = new SqlParameter[]
            {
                new SqlParameter("@ImageScale", (int)scale), 
                new SqlParameter("@a",SqlDbType.Xml){Value= dishIdsXML.ToString()}
            };
            List<ImageInfo> list = new List<ImageInfo>();
            using (var dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, cmdText, cmdParms))
            {
                while (dr.Read())
                {
                    list.Add(SqlHelper.GetEntity<ImageInfo>(dr));
                }
            }
            return list;

        }
    }
}
