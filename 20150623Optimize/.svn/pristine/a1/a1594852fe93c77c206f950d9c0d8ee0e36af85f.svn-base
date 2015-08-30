using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using VAGastronomistMobileApp.DBUtility;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL.Persistence.Infrastructure;

namespace VAGastronomistMobileApp.SQLServerDAL.Persistence
{
    public class SqlServerDishI18NRepository : SqlServerRepositoryBase, IDishI18NRepository
    {
        public SqlServerDishI18NRepository(ISqlConnectionFactory connectionFactory)
            : base(connectionFactory)
        {
        }

        public IEnumerable<DishI18n> GetDishI18NsByDishIds(params int[] dishIds)
        {
            const string cmdText = @"SELECT [DishI18nID]
      ,[DishID]
      ,[LangID]
      ,[DishName]
      ,[DishDescShort]
      ,[DishDescDetail]
      ,[DishHistory]
      ,[DishI18nStatus]
      ,[dishQuanPin]
      ,[dishJianPin]
  FROM [dbo].[DishI18n] WHERE [DishI18nStatus]=1 AND [DishID] IN (
  select d.x.value('./id[1]','int') from @a.nodes('/*') as d(x))";

            var dishIdsXML = new StringBuilder();
            foreach (var dishId in dishIds)
            {
                dishIdsXML.AppendFormat("<row><id>{0}</id></row>", dishId);
            }

            var cmdParm = new SqlParameter("@a", SqlDbType.Xml) { Value = dishIdsXML.ToString() };

            List<DishI18n> list = new List<DishI18n>();
            using (var dr = SqlHelper.ExecuteReader(Connection, CommandType.Text, cmdText, cmdParm))
            {
                while (dr.Read())
                {
                    list.Add(SqlHelper.GetEntity<DishI18n>(dr));
                }
            }
            return list;
        }
    }
}