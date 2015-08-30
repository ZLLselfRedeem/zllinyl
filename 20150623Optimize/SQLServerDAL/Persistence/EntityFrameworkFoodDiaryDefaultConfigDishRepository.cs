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


    public interface IFoodDiaryDefaultConfigDishRepository : IRepository<FoodDiaryDefaultConfigDish>
    {
        /// <summary>
        /// 伪删除
        /// </summary>
        /// <param name="id"></param>
        void PseudoDelete(int id);
        /// <summary>
        /// 获取所有可用的FoodDiaryDefaultConfigDish列表
        /// </summary>
        /// <returns></returns>
        IEnumerable<FoodDiaryDefaultConfigDish> GetEnableFoodDiaryDefaultConfigDishes();
    }
    public class EntityFrameworkFoodDiaryDefaultConfigDishRepository : EntityFrameworkRepositoryBase<FoodDiaryDefaultConfigDish>, IFoodDiaryDefaultConfigDishRepository
    {
        public EntityFrameworkFoodDiaryDefaultConfigDishRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        public void PseudoDelete(int id)
        {
            Update(p => p.Id == id, p => new FoodDiaryDefaultConfigDish() { Status = false });
        }

        public IEnumerable<FoodDiaryDefaultConfigDish> GetEnableFoodDiaryDefaultConfigDishes()
        {
            return GetMany(p => p.Status);
        }
    }

    public class SqlServerFoodDiaryDefaultConfigDishRepository : IFoodDiaryDefaultConfigDishRepository
    {
        public void Add(FoodDiaryDefaultConfigDish entity)
        {
            const string cmdText = @"INSERT INTO [dbo].[FoodDiaryDefaultConfigDish]
           ([DishName]
           ,[ImageName]
           ,[Status])
     VALUES
           (@DishName
           ,@ImageName
           ,@Status)";

            SqlParameter[] cmdParms = new SqlParameter[]
            {
                new SqlParameter("@DishName", entity.DishName), 
                new SqlParameter("@ImageName", entity.ImageName),
                new SqlParameter("@Status", entity.Status), 
            };

            SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, cmdText, cmdParms);
        }

        public void AddRange(IEnumerable<FoodDiaryDefaultConfigDish> entities)
        {
            throw new NotImplementedException();
        }

        public void Update(FoodDiaryDefaultConfigDish entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Expression<Func<FoodDiaryDefaultConfigDish, bool>> filterExpression, Expression<Func<FoodDiaryDefaultConfigDish, FoodDiaryDefaultConfigDish>> updateExpression)
        {
            throw new NotImplementedException();
        }

        public void Delete(FoodDiaryDefaultConfigDish entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Expression<Func<FoodDiaryDefaultConfigDish, bool>> @where)
        {
            throw new NotImplementedException();
        }

        public FoodDiaryDefaultConfigDish GetById(string id)
        {
            throw new NotImplementedException();
        }

        public FoodDiaryDefaultConfigDish GetById(long id)
        {
            throw new NotImplementedException();
        }

        public FoodDiaryDefaultConfigDish Get(Expression<Func<FoodDiaryDefaultConfigDish, bool>> @where)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<FoodDiaryDefaultConfigDish> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<FoodDiaryDefaultConfigDish> GetMany(Expression<Func<FoodDiaryDefaultConfigDish, bool>> @where)
        {
            throw new NotImplementedException();
        }

        public IPagedList<FoodDiaryDefaultConfigDish> GetPage<TOrder>(Page page, Expression<Func<FoodDiaryDefaultConfigDish, bool>> @where, Expression<Func<FoodDiaryDefaultConfigDish, TOrder>> order)
        {
            throw new NotImplementedException();
        }

        public void PseudoDelete(int id)
        {
            const string cmdText = @"UPDATE [dbo].[FoodDiaryDefaultConfigDish] SET [Status] = 0 WHERE [Id]=@Id";
            var cmdParm = new SqlParameter("@Id", id);

            SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, cmdText, cmdParm);
        }

        public IEnumerable<FoodDiaryDefaultConfigDish> GetEnableFoodDiaryDefaultConfigDishes()
        {
            const string cmdText = @"SELECT * FROM [dbo].[FoodDiaryDefaultConfigDish] WHERE [Status]=1";
            List<FoodDiaryDefaultConfigDish> list = new List<FoodDiaryDefaultConfigDish>();
            using (var dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, cmdText, null))
            {
                while (dr.Read())
                {
                    list.Add(SqlHelper.GetEntity<FoodDiaryDefaultConfigDish>(dr));
                }
            }
            return list;
        }
    }
}
