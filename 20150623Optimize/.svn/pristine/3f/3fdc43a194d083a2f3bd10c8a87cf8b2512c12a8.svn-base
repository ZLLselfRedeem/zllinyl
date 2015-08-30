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
    public class SqlServerShopInfoRepository : SqlServerRepositoryBase, IShopInfoRepository
    {
        public SqlServerShopInfoRepository(ISqlConnectionFactory connectionFactory)
            : base(connectionFactory)
        {
        }


        public ShopInfo GetById(long id)
        {
            const string cmdText = @"SELECT * FROM [dbo].[ShopInfo] WHERE [shopID]=@shopID";
            SqlParameter cmdParm = new SqlParameter("@shopID", id);

            ShopInfo shopInfo = null;
            using (var dr = SqlHelper.ExecuteReader(Connection, CommandType.Text, cmdText, cmdParm))
            {
                if (dr.Read())
                {
                    shopInfo = SqlHelper.GetEntity<ShopInfo>(dr);
                }
            }
            return shopInfo;
        }

    }
}