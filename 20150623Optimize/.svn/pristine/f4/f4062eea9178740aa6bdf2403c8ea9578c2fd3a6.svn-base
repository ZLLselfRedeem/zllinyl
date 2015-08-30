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
    public class SqlServerShopTagRepository : SqlServerRepositoryBase, IShopTagRepository
    {
        public SqlServerShopTagRepository(ISqlConnectionFactory connectionFactory)
            : base(connectionFactory)
        {
        }
        /// <summary>
        /// 查询当前城市所有一级商圈信息
        /// </summary>
        /// <param name="cityId"></param>
        /// <returns></returns>
        public List<ShopTag> GetFirstGradeShopTagByCityId(int cityId)
        {
            const string cmdText = @"SELECT TagId,Name,cast (replace(TagNode.GetAncestor(0).ToString(),'/','') as int) Flag ,A.ShopCount
                                      FROM ShopTag A inner join District B on A.TagId=B.Id
                                      where TagLevel=1 and Enable=1 and B.CityId=@CityId order by ShopCount desc";
            SqlParameter[] cmdParm = new[]
            {
                new SqlParameter("@CityId", cityId)
            };
            List<ShopTag> list = new List<ShopTag>();
            using (var dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, cmdText, cmdParm))
            {
                while (dr.Read())
                {
                    list.Add(SqlHelper.GetEntity<ShopTag>(dr));
                }
            }
            return list;
        }
        /// <summary>
        /// 查询当前城市所有二级商圈信息
        /// </summary>
        /// <param name="cityId"></param>
        /// <returns></returns>
        public List<ShopTag> GetSecondGradeShopTagByCityId(int cityId)
        {
            const string cmdText = @"SELECT distinct TagId,Name,cast(replace(A.[TagNode].GetAncestor(1).ToString(),'/','') as int) Flag  ,A.ShopCount
FROM ShopTag A inner join District B on A.TagId=B.Id
inner join ShopWithTag C on A.TagNode=C.TagNode
inner join ShopInfo D on D.shopID=C.ShopId
where TagLevel=2 and Enable=1 and B.CityId=@CityId and D.isHandle=1 order by ShopCount desc";
            SqlParameter[] cmdParm = new[]
            {
                new SqlParameter("@CityId", cityId)
            };
            List<ShopTag> list = new List<ShopTag>();
            using (var dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, cmdText, cmdParm))
            {
                while (dr.Read())
                {
                    list.Add(SqlHelper.GetEntity<ShopTag>(dr));
                }
            }
            return list;
        }
        /// <summary>
        /// 查询当前城市某个一级商圈所有二级商圈信息
        /// </summary>
        /// <param name="cityId"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        public List<ShopTag> GetSecondGradeShopTagByCityId(int cityId, int flag)
        {
            const string cmdText = @"SELECT TagId,Name,cast(replace([TagNode].GetAncestor(1).ToString(),'/','') as int) Flag  ,A.ShopCount
                                      FROM ShopTag A inner join District B on A.TagId=B.Id
                                      where TagLevel=2 and Enable=1 and B.CityId=@CityId 
                                      and  cast(replace([TagNode].GetAncestor(1).ToString(),'/','') as int)=@Flag
                                      order by ShopCount desc";
            SqlParameter[] cmdParm = new[]
            {
                new SqlParameter("@CityId", cityId),
                 new SqlParameter("@Flag", flag)
            };
            List<ShopTag> list = new List<ShopTag>();
            using (var dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, cmdText, cmdParm))
            {
                while (dr.Read())
                {
                    list.Add(SqlHelper.GetEntity<ShopTag>(dr));
                }
            }
            return list;
        }
        /// <summary>
        /// 查询当前商圈下所有门店
        /// </summary>
        /// <param name="tagId"></param>
        /// <returns></returns>
        public List<int> GetCurrectBusinessDistrictShopId(int tagId)
        {
            const string cmdText = @"select a.ShopId from dbo.ShopWithTag a
                                      inner join dbo.ShopTag b on a.TagNode.IsDescendantOf(b.TagNode)=1
                                      where b.TagId=@tagId and b.Enable=1";
            SqlParameter[] cmdParm = new[]
            {
                new SqlParameter("@tagId", tagId)
            };
            List<int> list = new List<int>();
            using (var dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, cmdText, cmdParm))
            {
                while (dr.Read())
                {
                    list.Add(dr[0] == DBNull.Value ? 0 : Convert.ToInt32(dr[0]));
                }
            }
            return list;
        }
        /// <summary>
        /// 查询当前门店下所有上线门店
        /// </summary>
        /// <param name="cityId"></param>
        /// <returns></returns>
        public List<ShopTagExt> GetCurrectBusinessDistrictHandleShopId(int cityId)
        {
            string cmdText = String.Format(@"select a.ShopId,b.TagId from dbo.ShopWithTag a
inner join dbo.ShopTag b on a.TagNode.IsDescendantOf(b.TagNode)=1
inner join ShopInfo c on c.shopID=a.ShopId
where b.Enable=1 and c.isHandle=1 and c.shopStatus=1 and c.cityID={0}", cityId);
            List<ShopTagExt> list = new List<ShopTagExt>();
            using (var dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, cmdText))
            {
                while (dr.Read())
                {
                    list.Add(
                        new ShopTagExt()
                        {
                            ShopId = dr[0] == DBNull.Value ? 0 : Convert.ToInt32(dr[0]),
                            TagId = dr[1] == DBNull.Value ? 0 : Convert.ToInt32(dr[1])
                        });
                }
            }
            return list;
        }
        #region 服务器端配置页面方法
        /// <summary>
        /// 新增商圈
        /// </summary>
        /// <param name="tagId">父商圈TagId</param>
        /// <param name="name">商圈名称</param>
        /// <returns></returns>
        public int AddShopTag(int tagId, string name)
        {
            using (SqlConnection connection = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                connection.Open();
                SqlCommand comm = new SqlCommand();
                comm.Connection = connection;
                comm.CommandType = CommandType.StoredProcedure;
                comm.CommandText = "AddShopTag";
                comm.Parameters.Add("@TagId", SqlDbType.Int).Value = tagId;
                comm.Parameters.Add("@Name", SqlDbType.NVarChar, 50).Value = name;
                object result = comm.ExecuteScalar();
                comm.Parameters.Clear();
                return Convert.ToInt32(result);
            }
        }

        /// <summary>
        /// 新增商圈归属城市
        /// </summary>
        /// <param name="tagId">商圈Id</param>
        /// <param name="cityId">城市Id</param>
        /// <returns></returns>
        public bool AddDistrict(int tagId, int cityId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into District(");
            strSql.Append("Id,CityId)");
            strSql.Append(" values (");
            strSql.Append("@Id,@CityId)");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4),
					new SqlParameter("@CityId", SqlDbType.Int,4)};
            parameters[0].Value = tagId;
            parameters[1].Value = cityId;
            int rows = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters);
            return rows > 0;
        }

        /// <summary>
        /// 新增商圈和门店关联关系
        /// </summary>
        /// <param name="tagId">当前商圈TagId</param>
        /// <param name="shopId">门店编号</param>
        /// <returns></returns>
        public int AddShopWithTag(int tagId, int shopId)
        {
            using (SqlConnection connection = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                connection.Open();
                SqlCommand comm = new SqlCommand();
                comm.Connection = connection;
                comm.CommandType = CommandType.StoredProcedure;
                comm.CommandText = "AddShopWithTag";
                comm.Parameters.Add("@TagId", SqlDbType.Int, 4).Value = tagId;
                comm.Parameters.Add("@ShopId", SqlDbType.Int, 4).Value = shopId;
                int result = comm.ExecuteNonQuery();
                comm.Parameters.Clear();
                return result;
            }
        }
        /// <summary>
        /// 更新商圈名称
        /// </summary>
        /// <param name="tagId"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool UpdateShopTagName(int tagId, string name)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int result = 0;
                conn.Open();
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update ShopTag set Name = @Name where TagId=@TagId;");
                SqlParameter[] parameters = {					
					new SqlParameter("@Name", SqlDbType.NVarChar,50),
                    new SqlParameter("@TagId", SqlDbType.Int,4)};
                parameters[0].Value = name;
                parameters[1].Value = tagId;
                result = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);
                return result > 0;
            }
        }
        /// <summary>
        /// 更新商圈绑定店铺的数量，包括二级及一级
        /// </summary>
        /// <param name="tagId">可以传N个二级商圈tagId，用英文逗号隔开</param>
        /// <param name="count"></param>
        /// <returns></returns>
        public bool UpdateShopTagCount(string tagId, int shopCount)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int result = 0;
                conn.Open();
                string strSql = "dbo.sp_updateShopCount";
                var tagIdXML = new StringBuilder();
                string[] tagIdArr = tagId.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var Id in tagIdArr)
                {
                    tagIdXML.AppendFormat("<row><id>{0}</id></row>", Id);
                }
                SqlParameter[] para = new SqlParameter[] { 
                new SqlParameter("@childrenTagId", SqlDbType.Xml) { Value = tagIdXML.ToString() },
                new SqlParameter("@shopCount", SqlDbType.Int) { Value = shopCount }
                };

                result = SqlHelper.ExecuteNonQuery(conn, CommandType.StoredProcedure, strSql.ToString(), para);
                return result > 0;
            }
        }
        /// <summary>
        /// 删除商圈
        /// </summary>
        /// <param name="tagIds"></param>
        /// <returns></returns>
        public bool DeleteShopTag(string tagIds)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int result = 0;
                conn.Open();
                StringBuilder strSql = new StringBuilder();
                strSql.AppendFormat("update ShopTag set Enable = 0  where TagId in {0};", tagIds);//软删除商圈信息表
                strSql.AppendFormat(@"delete from ShopWithTag 
where ShopWithTag.TagNode in(
select a.TagNode from  ShopWithTag a inner join ShopTag b on a.TagNode.IsDescendantOf(b.TagNode)=1
 where b.TagId in {0} ); ", tagIds);//真删除商圈和门店的关联表
                strSql.AppendFormat("delete District where Id in {0};", tagIds);//真删除城市和商圈关联表
                SqlParameter[] parameters = {					
                    new SqlParameter("@TagId", SqlDbType.NVarChar,500)};
                parameters[0].Value = tagIds;
                result = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);
                return result > 0;
            }
        }

        /// <summary>
        /// 根据一级商圈ID查询其所有二级商圈
        /// </summary>
        /// <param name="tagId"></param>
        /// <returns></returns>
        public List<ShopTag> GetSecondGradeShopTagByFirstGrade(int tagId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" DECLARE @level1 hierarchyid");
            strSql.AppendFormat(" SELECT @level1=TagNode FROM ShopTag WHERE TagId={0}", tagId);
            strSql.Append(" SELECT TagId,Name,0 Flag,ShopCount FROM ShopTag WHERE TagNode.IsDescendantOf(@level1)=1 and enable=1");
            strSql.AppendFormat(" and TagId!={0}", tagId);

            List<ShopTag> list = new List<ShopTag>();
            using (SqlDataReader sdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString()))
            {
                while (sdr.Read())
                {
                    list.Add(SqlHelper.GetEntity<ShopTag>(sdr));
                }
            }
            return list;
        }
        /// <summary>
        /// 根据shopId查询其所有的商圈标记
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public List<ShopTag> GetShopTagByShopId(int shopId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select tag.TagId,tag.Name,0 Flag ,tag.ShopCount from ShopWithTag shop");
            strSql.Append(" inner join ShopTag tag on shop.TagNode = tag.TagNode and shop.ShopId = @shopId");
            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@shopId", SqlDbType.Int) { Value = shopId }
            };
            List<ShopTag> list = new List<ShopTag>();
            using (SqlDataReader sdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), para))
            {
                while (sdr.Read())
                {
                    list.Add(SqlHelper.GetEntity<ShopTag>(sdr));
                }
            }
            return list;
        }

        /// <summary>
        /// 检查shop和商圈的对应关系是否存在，TRUE表示存在
        /// </summary>
        /// <param name="tagId"></param>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public bool CheckShopTagRelation(int tagId, int shopId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select tag.TagId,tag.Name from ShopWithTag shop");
            strSql.Append(" inner join ShopTag tag on shop.TagNode = tag.TagNode and shop.ShopId = @shopId and tag.TagId=@tagId");
            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@shopId", SqlDbType.Int) { Value = shopId },
            new SqlParameter("@tagId", SqlDbType.Int) { Value = tagId }
            };
            using (SqlDataReader sdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), para))
            {
                if (sdr.Read())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// 删除店铺和商圈的关系
        /// </summary>
        /// <param name="tagId"></param>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public int DeleteShopWithTag(string tagId, int shopId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("delete from ShopWithTag where TagNode in (select TagNode from ShopTag where TagId in ({0})) and ShopId={1}", tagId, shopId);
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int i = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString());
                return i;
            }
        }


        #endregion
    }
}
