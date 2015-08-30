using System;
using System.Data;
using System.Data.SqlClient;
using VAGastronomistMobileApp.DBUtility;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL.Persistence.Infrastructure;

namespace VAGastronomistMobileApp.SQLServerDAL.Persistence
{
    public class SqlServerShopVIPSpeedNextTimeRepository : SqlServerRepositoryBase, IShopVIPSpeedNextTimeRepository
    {
        public SqlServerShopVIPSpeedNextTimeRepository(ISqlConnectionFactory connectionFactory)
            : base(connectionFactory)
        {
        }

        public ShopVIPSpeedNextTime GetByCity(int cityId)
        {
            const string cmdText = @"SELECT * FROM [dbo].[ShopVIPSpeedNextTime] WHERE [City]=@City";
            SqlParameter cmdParm = new SqlParameter("@City", SqlDbType.Int) { Value = cityId };

            ShopVIPSpeedNextTime shopVipSpeedNextTime = null;
            using (var dr = SqlHelper.ExecuteReader(Connection, CommandType.Text, cmdText, cmdParm))
            {
                if (dr.Read())
                {
                    shopVipSpeedNextTime = SqlHelper.GetEntity<ShopVIPSpeedNextTime>(dr);
                }
            }
            return shopVipSpeedNextTime;
        }

        public void Add(ShopVIPSpeedNextTime shopVipSpeedNextTime)
        {
            const string cmdText = @"INSERT INTO [dbo].[ShopVIPSpeedNextTime]
           ([City]
           ,[NextTime])
     VALUES
           (@City
           ,@NextTime)";

            SqlParameter[] cmdParms = new SqlParameter[]
            {
                new SqlParameter("@City", SqlDbType.Int){Value = shopVipSpeedNextTime.City},
                new SqlParameter("@NextTime",SqlDbType.DateTime){Value = shopVipSpeedNextTime.NextTime??DateTime.Now}, 
            };

            SqlHelper.ExecuteNonQuery(Connection, CommandType.Text, cmdText, cmdParms);
        }

        public void Update(ShopVIPSpeedNextTime shopVipSpeedNextTime)
        {
            const string cmdText = @"UPDATE [dbo].[ShopVIPSpeedNextTime]
   SET [City] = @City
      ,[NextTime] = @NextTime
 WHERE [Id] = @Id";

            SqlParameter[] cmdParms = new SqlParameter[]
            {
                new SqlParameter("@City", SqlDbType.Int){Value = shopVipSpeedNextTime.City},
                new SqlParameter("@NextTime",SqlDbType.DateTime){Value = shopVipSpeedNextTime.NextTime??DateTime.Now}, 
                new SqlParameter("@Id",SqlDbType.Int){Value = shopVipSpeedNextTime.Id}, 
            };

            SqlHelper.ExecuteNonQuery(Connection, CommandType.Text, cmdText, cmdParms);
        }
    }
}