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
    public class SqlServerFoodDiaryDefaultConfigDishRepository : SqlServerRepositoryBase, IFoodDiaryDefaultConfigDishRepository
    {
        public SqlServerFoodDiaryDefaultConfigDishRepository(ISqlConnectionFactory connectionFactory)
            : base(connectionFactory)
        {
        }

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

            SqlHelper.ExecuteNonQuery(Connection, CommandType.Text, cmdText, cmdParms);
        }


        public void PseudoDelete(int id)
        {
            const string cmdText = @"UPDATE [dbo].[FoodDiaryDefaultConfigDish] SET [Status] = 0 WHERE [Id]=@Id";
            var cmdParm = new SqlParameter("@Id", id);

            SqlHelper.ExecuteNonQuery(Connection, CommandType.Text, cmdText, cmdParm);
        }

        public IEnumerable<FoodDiaryDefaultConfigDish> GetEnableFoodDiaryDefaultConfigDishes()
        {
            const string cmdText = @"SELECT * FROM [dbo].[FoodDiaryDefaultConfigDish] WHERE [Status]=1";
            List<FoodDiaryDefaultConfigDish> list = new List<FoodDiaryDefaultConfigDish>();
            using (var dr = SqlHelper.ExecuteReader(Connection, CommandType.Text, cmdText, null))
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