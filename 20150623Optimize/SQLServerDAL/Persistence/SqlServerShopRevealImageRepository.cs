using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.DBUtility;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL.Persistence.Infrastructure;

namespace VAGastronomistMobileApp.SQLServerDAL.Persistence
{
    public class SqlServerShopRevealImageRepository : SqlServerRepositoryBase, IShopRevealImageRepository
    {
        public SqlServerShopRevealImageRepository(ISqlConnectionFactory connectionFactory)
            : base(connectionFactory)
        {
        }


        public IEnumerable<ShopRevealImage> GetShopRevealImages(int shopId)
        {
            const string cmdText = @"SELECT * FROM [dbo].[ShopRevealImage] WHERE [shopId]=@ShopId And [status]=1";

            SqlParameter cmdParm = new SqlParameter("@ShopId", SqlDbType.Int) { Value = shopId };
            List<ShopRevealImage> shopRevealImages = new List<ShopRevealImage>();
            using (var dr=SqlHelper.ExecuteReader(Connection, CommandType.Text, cmdText, cmdParm))
            {
                while (dr.Read())
                {
                    shopRevealImages.Add(SqlHelper.GetEntity<ShopRevealImage>(dr));
                }
            }

            return shopRevealImages;
        }
    }
}
