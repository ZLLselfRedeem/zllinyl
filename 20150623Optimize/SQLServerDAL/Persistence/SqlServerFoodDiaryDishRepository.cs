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
    public class SqlServerFoodDiaryDishRepository : SqlServerRepositoryBase, IFoodDiaryDishRepository
    {
        public SqlServerFoodDiaryDishRepository(ISqlConnectionFactory connectionFactory)
            : base(connectionFactory)
        {
        }

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

            object obj_value = SqlHelper.ExecuteScalar(Connection, CommandType.Text, cmdText, cmdParms);
            entity.Id = Convert.ToInt64(obj_value);
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

            SqlHelper.ExecuteNonQuery(Connection, CommandType.Text, cmdText, cmdParms);
        }

        public IEnumerable<FoodDiaryDish> GetFoodDiaryDishesByFoodDiaryId(long foodDiaryId)
        {
            const string cmdText = @"SELECT * FROM [dbo].[FoodDiaryDish] WHERE [FoodDiaryId]=@FoodDiaryId";
            SqlParameter cmdParm = new SqlParameter("@FoodDiaryId", SqlDbType.BigInt) { Value = foodDiaryId };

            List<FoodDiaryDish> list = new List<FoodDiaryDish>();
            using (var dr = SqlHelper.ExecuteReader(Connection, CommandType.Text, cmdText, cmdParm))
            {
                while (dr.Read())
                {
                    list.Add(SqlHelper.GetEntity<FoodDiaryDish>(dr));
                }
            }

            return list;
        }

        public void AddRange(List<FoodDiaryDish> dishes)
        {
            foreach (var foodDiaryDish in dishes)
            {
                Add(foodDiaryDish);
            }
        }
    }
}