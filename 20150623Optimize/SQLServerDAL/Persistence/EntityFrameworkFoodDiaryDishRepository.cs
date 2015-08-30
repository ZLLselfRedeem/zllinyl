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
    public interface IFoodDiaryDishRepository : IRepository<FoodDiaryDish>
    {
        void UpdateSortAndStatus(long id, int sort, bool status);

        IEnumerable<FoodDiaryDish> GetFoodDiaryDishesByFoodDiaryId(long foodDiaryId);
    }

    public class EntityFrameworkFoodDiaryDishRepository : EntityFrameworkRepositoryBase<FoodDiaryDish>, IFoodDiaryDishRepository
    {
        public EntityFrameworkFoodDiaryDishRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        public void UpdateSortAndStatus(long id, int sort, bool status)
        {
            Update(p => p.Id == id, dish => new FoodDiaryDish()
            {
                Sort = sort,
                Status = status
            });
        }

        public IEnumerable<FoodDiaryDish> GetFoodDiaryDishesByFoodDiaryId(long foodDiaryId)
        {
            return GetMany(p => p.FoodDiaryId == foodDiaryId);
        }
    }

    public class SqlServerFoodDiaryDishRepository : IFoodDiaryDishRepository
    {
        public void Add(FoodDiaryDish entity)
        {
            const string cmdText = @"INSERT INTO [dbo].[FoodDiaryDish]
           ([FoodDiaryId]
           ,[DishId]
           ,[DishName]
           ,[ImagePath]
           ,[Source]
           ,[Sort]
           ,[Status])
     VALUES
           (@FoodDiaryId
           ,@DishId
           ,@DishName
           ,@ImagePath
           ,@Source
           ,@Sort
           ,@Status);SELECT @@IDENTITY;";

            SqlParameter[] cmdParms = new SqlParameter[]
            {
                new SqlParameter("@FoodDiaryId", entity.FoodDiaryId),
                new SqlParameter("@DishId", entity.DishId), 
                new SqlParameter("@DishName", entity.DishName), 
                new SqlParameter("@ImagePath", entity.ImagePath), 
                new SqlParameter("@Source", entity.Source),
                new SqlParameter("@Sort", entity.Sort), 
                new SqlParameter("@Status", entity.Status), 
            };

            object obj_value = SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, cmdText, cmdParms);
            entity.Id = Convert.ToInt64(obj_value);
        }

        public void AddRange(IEnumerable<FoodDiaryDish> entities)
        {
            foreach (var foodDiaryDish in entities)
            {
                Add(foodDiaryDish);
            }
        }

        public void Update(FoodDiaryDish entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Expression<Func<FoodDiaryDish, bool>> filterExpression, Expression<Func<FoodDiaryDish, FoodDiaryDish>> updateExpression)
        {
            throw new NotImplementedException();
        }

        public void Delete(FoodDiaryDish entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Expression<Func<FoodDiaryDish, bool>> @where)
        {
            throw new NotImplementedException();
        }

        public FoodDiaryDish GetById(string id)
        {
            throw new NotImplementedException();
        }

        public FoodDiaryDish GetById(long id)
        {
            throw new NotImplementedException();
        }

        public FoodDiaryDish Get(Expression<Func<FoodDiaryDish, bool>> @where)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<FoodDiaryDish> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<FoodDiaryDish> GetMany(Expression<Func<FoodDiaryDish, bool>> @where)
        {
            throw new NotImplementedException();
        }

        public IPagedList<FoodDiaryDish> GetPage<TOrder>(Page page, Expression<Func<FoodDiaryDish, bool>> @where, Expression<Func<FoodDiaryDish, TOrder>> order)
        {
            throw new NotImplementedException();
        }

        public void UpdateSortAndStatus(long id, int sort, bool status)
        {
            const string cmdText = @"UPDATE [dbo].[FoodDiaryDish]
   SET[Sort] = @Sort
      ,[Status] = @Status
 WHERE [Id]=@Id";

            SqlParameter[] cmdParms = new SqlParameter[]
            {
                new SqlParameter("@Sort", sort),
                new SqlParameter("@Status", status),
                new SqlParameter("@Id", id), 
            };

            SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, cmdText, cmdParms);
        }

        public IEnumerable<FoodDiaryDish> GetFoodDiaryDishesByFoodDiaryId(long foodDiaryId)
        {
            const string cmdText = @"SELECT * FROM [dbo].[FoodDiaryDish] WHERE [FoodDiaryId]=@FoodDiaryId";
            SqlParameter cmdParm = new SqlParameter("@FoodDiaryId", SqlDbType.BigInt) { Value = foodDiaryId };

            List<FoodDiaryDish> list = new List<FoodDiaryDish>();
            using (var dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, cmdText, cmdParm))
            {
                while (dr.Read())
                {
                    list.Add(SqlHelper.GetEntity<FoodDiaryDish>(dr));
                }
            }

            return list;
        }
    }
}
