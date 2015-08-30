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
    public class SqlServerMenuInfoRepository : SqlServerRepositoryBase, IMenuInfoRepository
    {
        public SqlServerMenuInfoRepository(ISqlConnectionFactory connectionFactory)
            : base(connectionFactory)
        {
        }

        public MenuInfo GetById(long id)
        {
            const string cmdText = @"SELECT * FROM [dbo].[MenuInfo] WHERE [MenuID]=@MenuID";
            var cmdParm = new SqlParameter("@MenuID", id);

            MenuInfo menuInfo = null;
            using (var dr = SqlHelper.ExecuteReader(Connection, CommandType.Text, cmdText, cmdParm))
            {
                if (dr.Read())
                {
                    menuInfo = SqlHelper.GetEntity<MenuInfo>(dr);
                }
            }
            return menuInfo;
        }

        public MenuInfo GetMenuInfoByShopId(int shopId)
        {
            const string cmdText = @"select a.* from MenuInfo a 
inner join MenuConnShop b on a.MenuID=b.menuId
where b.shopId=@shopId and a.MenuStatus=1";
            var cmdParm = new SqlParameter("@shopId", shopId);

            MenuInfo menuInfo = null;
            using (var dr = SqlHelper.ExecuteReader(Connection, CommandType.Text, cmdText, cmdParm))
            {
                if (dr.Read())
                {
                    menuInfo = SqlHelper.GetEntity<MenuInfo>(dr);
                }
            }
            return menuInfo;
        }
    }
}