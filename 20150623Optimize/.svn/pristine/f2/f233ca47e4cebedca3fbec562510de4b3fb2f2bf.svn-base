using System.Data;
using System.Data.SqlClient;
using VAGastronomistMobileApp.DBUtility;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL.Persistence.Infrastructure;

namespace VAGastronomistMobileApp.SQLServerDAL.Persistence
{
    public class SqlServerShopVIPFlaseCountRepository : SqlServerRepositoryBase, IShopVIPFlaseCountRepository
    {
        public SqlServerShopVIPFlaseCountRepository(ISqlConnectionFactory connectionFactory)
            : base(connectionFactory)
        {
        }

        public void Add(ShopVIPFlaseCount shopVipFlaseCount)
        {
            const string cmdText = @"INSERT INTO [dbo].[ShopVIPFlaseCount]
           ([City]
           ,[Date]
           ,[Count]
           ,[Enable])
     VALUES
           (@City
           ,@Date
           ,@Count
           ,@Enable)";

            SqlParameter[] cmdParms = new SqlParameter[]
            {
                new SqlParameter("@City",SqlDbType.Int){Value = shopVipFlaseCount.City},
                new SqlParameter("@Date",SqlDbType.VarChar,6){Value = shopVipFlaseCount.Date},
                new SqlParameter("@Count",SqlDbType.Int){Value = shopVipFlaseCount.Count},
                new SqlParameter("@Enable",SqlDbType.Bit){Value = shopVipFlaseCount.Enable}, 
            };


            SqlHelper.ExecuteNonQuery(Connection, CommandType.Text, cmdText, cmdParms);
        }

        public void Update(ShopVIPFlaseCount shopVipFlaseCount)
        {
            const string cmdText = @"UPDATE [dbo].[ShopVIPFlaseCount]
   SET [City] = @City
      ,[Date] = @Date
      ,[Count] = @Count
      ,[Enable] = @Enable
 WHERE [Id]=@Id";

            SqlParameter[] cmdParms = new SqlParameter[]
            {
                new SqlParameter("@City",SqlDbType.Int){Value = shopVipFlaseCount.City},
                new SqlParameter("@Date",SqlDbType.VarChar,6){Value = shopVipFlaseCount.Date},
                new SqlParameter("@Count",SqlDbType.Int){Value = shopVipFlaseCount.Count},
                new SqlParameter("@Enable",SqlDbType.Bit){Value = shopVipFlaseCount.Enable},
                new SqlParameter("@Id",SqlDbType.Int){Value = shopVipFlaseCount.Id}, 
            };
            SqlHelper.ExecuteNonQuery(Connection, CommandType.Text, cmdText, cmdParms);
        }

        public ShopVIPFlaseCount GetByCityAndMonth(int cityId, string month)
        {
            const string cmdText = @"SELECT * FROM [dbo].[ShopVIPFlaseCount] WHERE [City] = @City AND [Date] = @Date";

            SqlParameter[] cmdParms = new SqlParameter[]
            {
                new SqlParameter("@City",SqlDbType.Int){Value = cityId},
                new SqlParameter("@Date",SqlDbType.VarChar,6){Value = month},
            };

            ShopVIPFlaseCount shopVipFlaseCount = null;
            using (var dr = SqlHelper.ExecuteReader(Connection, CommandType.Text, cmdText, cmdParms))
            {
                if (dr.Read())
                {
                    shopVipFlaseCount = SqlHelper.GetEntity<ShopVIPFlaseCount>(dr);
                }
            }

            return shopVipFlaseCount;
        }
    }
}
