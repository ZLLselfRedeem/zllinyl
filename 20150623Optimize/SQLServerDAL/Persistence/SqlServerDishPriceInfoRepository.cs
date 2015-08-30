using System;
using System.Data;
using System.Data.SqlClient;
using VAGastronomistMobileApp.DBUtility;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL.Persistence.Infrastructure;

namespace VAGastronomistMobileApp.SQLServerDAL.Persistence
{
    public class SqlServerDishPriceInfoRepository : SqlServerRepositoryBase, IDishPriceInfoRepository
    {
        public SqlServerDishPriceInfoRepository(ISqlConnectionFactory connectionFactory)
            : base(connectionFactory)
        {
        }



        public DishPriceInfo GetById(long id)
        {
            const string cmdText = @"SELECT * FROM [dbo].[DishPriceInfo] WHERE [DishPriceID]=@DishPriceID";
            var cmdParm = new SqlParameter("@DishPriceID", id);

            DishPriceInfo dishPriceInfo = null;
            using (var dr = SqlHelper.ExecuteReader(Connection, CommandType.Text, cmdText, cmdParm))
            {
                if (dr.Read())
                {
                    dishPriceInfo = SqlHelper.GetEntity<DishPriceInfo>(dr);
                }
            }
            return dishPriceInfo;
        }

        public int UpdatePrice(int dishPriceId, double price)
        {
            const string cmdText = @"UPDATE [dbo].[DishPriceInfo]
   SET [DishPrice] = @DishPrice WHERE [DishPriceID]=@DishPriceID";

            SqlParameter[] cmdParms = new SqlParameter[]
            {
                new SqlParameter("@DishPrice",Math.Round(price,2)), 
                new SqlParameter("@DishPriceID", dishPriceId), 
            };

            return SqlHelper.ExecuteNonQuery(Connection, CommandType.Text, cmdText, cmdParms);
        }
    }
}