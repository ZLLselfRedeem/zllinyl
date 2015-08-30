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
    public interface IFoodDiaryRepository : IRepository<FoodDiary>
    {
        FoodDiary GetFoodDiaryByOrderId(long orderId);
    }

    public class EntityFrameworkFoodDiaryRepository : EntityFrameworkRepositoryBase<FoodDiary>, IFoodDiaryRepository
    {
        public EntityFrameworkFoodDiaryRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        public FoodDiary GetFoodDiaryByOrderId(long orderId)
        {
            return Get(p => p.OrderId == orderId);
        }
    }

    public class SqlServerFoodDiaryRepository : IFoodDiaryRepository
    {
        public void Add(FoodDiary entity)
        {
            const string cmdText = @"INSERT INTO [dbo].[FoodDiary]
           ([OrderId]
           ,[Name]
           ,[Content]
           ,[Weather]
           ,[ShopName]
           ,[ShoppingDate]
           ,[Shared]
           ,[CreateTime])
     VALUES
           (@OrderId
           ,@Name
           ,@Content
           ,@Weather
           ,@ShopName
           ,@ShoppingDate
           ,@Shared
           ,@CreateTime);SELECT @@IDENTITY;";

            SqlParameter[] cmdParms = new SqlParameter[]
            {
                new SqlParameter("@OrderId", entity.OrderId),
                new SqlParameter("@Name", entity.Name),
                new SqlParameter("@Content", entity.Content), 
                new SqlParameter("@Weather",entity.Weather), 
                new SqlParameter("@ShopName", entity.ShopName), 
                new SqlParameter("@ShoppingDate",entity.ShoppingDate), 
                new SqlParameter("@Shared",(byte)entity.Shared), 
                new SqlParameter("@CreateTime", entity.CreateTime)
            };

            object obj_value = SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, cmdText, cmdParms);
            entity.Id = Convert.ToInt64(obj_value);
        }

        public void AddRange(IEnumerable<FoodDiary> entities)
        {
            throw new NotImplementedException();
        }

        public void Update(FoodDiary entity)
        {
            const string cmdText = @"UPDATE [dbo].[FoodDiary]
   SET [OrderId] = @OrderId
      ,[Name] = @Name
      ,[Content] = @Content
      ,[Weather] = @Weather
      ,[ShopName] = @ShopName
      ,[ShoppingDate] = @ShoppingDate
      ,[Shared] = @Shared
      ,[CreateTime] = @CreateTime
 WHERE [Id] = @Id";

            SqlParameter[] cmdParms = new SqlParameter[]
            {
                new SqlParameter("@OrderId", entity.OrderId),
                new SqlParameter("@Name", entity.Name),
                new SqlParameter("@Content", entity.Content), 
                new SqlParameter("@Weather",entity.Weather), 
                new SqlParameter("@ShopName", entity.ShopName), 
                new SqlParameter("@ShoppingDate",entity.ShoppingDate), 
                new SqlParameter("@Shared",(byte)entity.Shared), 
                new SqlParameter("@CreateTime", entity.CreateTime),
                new SqlParameter("@Id",entity.Id), 
            };

            SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, cmdText, cmdParms);
        }

        public void Update(Expression<Func<FoodDiary, bool>> filterExpression, Expression<Func<FoodDiary, FoodDiary>> updateExpression)
        {
            throw new NotImplementedException();
        }

        public void Delete(FoodDiary entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Expression<Func<FoodDiary, bool>> @where)
        {
            throw new NotImplementedException();
        }

        public FoodDiary GetById(string id)
        {
            throw new NotImplementedException();
        }

        public FoodDiary GetById(long id)
        {
            const string cmdText = @"SELECT * FROM [dbo].[FoodDiary] WHERE [Id]=@Id";

            var cmdParm = new SqlParameter("@Id", id);
            FoodDiary foodDiary = null;
            using (var dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, cmdText, cmdParm))
            {
                if (dr.Read())
                    foodDiary = SqlHelper.GetEntity<FoodDiary>(dr);
            }
            return foodDiary;
        }

        public FoodDiary Get(Expression<Func<FoodDiary, bool>> @where)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<FoodDiary> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<FoodDiary> GetMany(Expression<Func<FoodDiary, bool>> @where)
        {
            throw new NotImplementedException();
        }

        public IPagedList<FoodDiary> GetPage<TOrder>(Page page, Expression<Func<FoodDiary, bool>> @where, Expression<Func<FoodDiary, TOrder>> order)
        {
            throw new NotImplementedException();
        }

        public FoodDiary GetFoodDiaryByOrderId(long orderId)
        {
            const string cmdText = @"SELECT * FROM [dbo].[FoodDiary] WHERE [OrderId]=@OrderId";

            var cmdParm = new SqlParameter("@OrderId", orderId);
            FoodDiary foodDiary = null;
            using (var dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, cmdText, cmdParm))
            {
                if (dr.Read())
                    foodDiary = SqlHelper.GetEntity<FoodDiary>(dr);
            }
            return foodDiary;
        }
    }

}
