using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq.Expressions;
using System.Text;
using PagedList;
using VAGastronomistMobileApp.DBUtility;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL.Persistence.Infrastructure;

namespace VAGastronomistMobileApp.SQLServerDAL.Persistence
{
    public class SqlServerImageInfoRepository : SqlServerRepositoryBase, IImageInfoRepository
    {
        public SqlServerImageInfoRepository(ISqlConnectionFactory connectionFactory)
            : base(connectionFactory)
        {
        }

        public IEnumerable<ImageInfo> GetAssignScaleImageInfosByDishId(ImageScale scale, params int[] dishIds)
        {
            const string cmdText = @"SELECT [ImageID]
      ,[DishID]
      ,[ImageName]
      ,[ImageSequence]
      ,[ImageScale]
      ,[ImageStatus]
      ,[ImageXY]
  FROM [dbo].[ImageInfo] WHERE [ImageScale]=@ImageScale AND [DishID] IN  (
select d.x.value('./id[1]','int') from @a.nodes('/*') as d(x))";

            StringBuilder dishIdsXML = new StringBuilder();
            foreach (var dishId in dishIds)
            {
                dishIdsXML.AppendFormat("<row><id>{0}</id></row>", dishId);
            }

            SqlParameter[] cmdParms = new SqlParameter[]
            {
                new SqlParameter("@ImageScale", (int)scale), 
                new SqlParameter("@a",SqlDbType.Xml){Value= dishIdsXML.ToString()}
            };
            List<ImageInfo> list = new List<ImageInfo>();
            using (var dr = SqlHelper.ExecuteReader(Connection, CommandType.Text, cmdText, cmdParms))
            {
                while (dr.Read())
                {
                    list.Add(SqlHelper.GetEntity<ImageInfo>(dr));
                }
            }
            return list;

        }
        public List<DishImage> GetAssignScaleImageInfosByDishId(string dishIds, string path)
        {
            string cmdText = @"select B.menuImagePath+ImageName as url,A.dishId,C.MenuID
FROM ImageInfo A 
inner join DishInfo C on A.DishID=C.DishID 
inner join MenuInfo B on C.MenuID=B.MenuID
WHERE ImageStatus=1 AND ImageScale=0 
AND A.DishID IN " + dishIds;
            List<DishImage> list = new List<DishImage>();
            using (var dr = SqlHelper.ExecuteReader(Connection, CommandType.Text, cmdText))
            {
                while (dr.Read())
                {
                    list.Add(new DishImage()
                    {
                        url = dr["url"] == DBNull.Value ? "" : path + dr["url"].ToString(),
                        dishId = dr["dishId"] == DBNull.Value ? 0 : Convert.ToInt32(dr["dishId"]),
                        imageId = dr["MenuID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["MenuID"])
                    });
                }
            }
            return list;
        }

        public List<DishPraiseInfo> GetDishPraiseInfosByDishId(string dishIds)
        {
            string cmdText = string.Format(@"select DishID,dishPraiseNum from DishInfo where DishID in {0}", dishIds);
            List<DishPraiseInfo> list = new List<DishPraiseInfo>();
            using (var dr = SqlHelper.ExecuteReader(Connection, CommandType.Text, cmdText))
            {
                while (dr.Read())
                {
                    list.Add(new DishPraiseInfo()
                    {
                        dishId = dr["dishId"] == DBNull.Value ? 0 : Convert.ToInt32(dr["dishId"]),
                        orderDishPraiseNum = dr["dishPraiseNum"] == DBNull.Value ? 0 : Convert.ToInt32(dr["dishPraiseNum"])
                    });
                }
            }
            return list;
        }
    }
}