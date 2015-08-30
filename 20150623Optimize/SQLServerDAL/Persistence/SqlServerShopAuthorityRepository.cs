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
    public class SqlServerShopAuthorityRepository : SqlServerRepositoryBase, IShopAuthorityRepository
    {
        public SqlServerShopAuthorityRepository(ISqlConnectionFactory connectionFactory)
            : base(connectionFactory)
        {
        }

        public IEnumerable<ShopAuthority> GetShopAuthoritiesInViewAllocService(int shopId, int employeeId, params ShopAuthorityType[] types)
        {
            const string cmdText = @"SELECT a.* FROM [dbo].[ShopAuthority] a
                                    INNER JOIN [dbo].[EmployeeShopAuthority] b on a.[shopAuthorityId]=b.[shopAuthorityId]
                                    INNER JOIN [dbo].[EmployeeConnShop] c on c.[employeeShopID]=b.[employeeConnShopId]
                                    WHERE a.[shopAuthorityStatus]=1 AND c.[shopID]=@ShopId and c.[employeeID]=@EmployeeId
                                    AND b.[employeeShopAuthorityStatus]=1 and c.[status]=1
                                    AND a.[shopAuthorityType] IN (
                                    select d.x.value('./id[1]','int') from @xml.nodes('/*') as d(x)) ORDER BY [ShopAuthoritySequence]";

            var xml = new StringBuilder();
            foreach (var t in types)
            {
                xml.AppendFormat("<row><id>{0}</id></row>", (int)t);
            }

            SqlParameter[] cmdParms = new[]
            {
                new SqlParameter("@EmployeeID", SqlDbType.Int) {Value = employeeId},
                new SqlParameter("@ShopID", SqlDbType.Int) {Value = shopId},
                new SqlParameter("@xml", SqlDbType.Xml) {Value = xml.ToString()}
            };

            List<ShopAuthority> shopAuthorities = new List<ShopAuthority>();
            using (var dr = SqlHelper.ExecuteReader(Connection, CommandType.Text, cmdText, cmdParms))
            {

                while (dr.Read())
                {
                    shopAuthorities.Add((SqlHelper.GetEntity<ShopAuthority>(dr)));
                }
            }
            return shopAuthorities;
        }

        public IEnumerable<ShopAuthority> GetShopAuthoritiesWithViewAllocWorkerInViewAllocService(int employeeId, params ShopAuthorityType[] types)
        {
            const string cmdText = @"SELECT a.* FROM [dbo].[ShopAuthority] a
                                    INNER JOIN [dbo].[ViewAllocEmployeeAuthority] b ON a.[shopAuthorityId]=b.[shopAuthorityId]
                                    WHERE b.employeeId=@employeeId AND a.[shopAuthorityStatus]=1 AND b.[status]=1
                                    AND a.[shopAuthorityType] IN (
                                    select d.x.value('./id[1]','int') from @xml.nodes('/*') as d(x))
                                    ORDER BY a.[ShopAuthoritySequence]";
            var xml = new StringBuilder();
            foreach (var t in types)
            {
                xml.AppendFormat("<row><id>{0}</id></row>", (int)t);
            }
            SqlParameter[] cmdParms = new[]
            {
                new SqlParameter("@employeeId", SqlDbType.Int) { Value = employeeId },
                new SqlParameter("@xml", SqlDbType.Xml) { Value = xml.ToString() } 
            };

            List<ShopAuthority> shopAuthorities = new List<ShopAuthority>();
            using (var dr = SqlHelper.ExecuteReader(Connection, CommandType.Text, cmdText, cmdParms))
            {

                while (dr.Read())
                {
                    shopAuthorities.Add((SqlHelper.GetEntity<ShopAuthority>(dr)));
                }
            }
            return shopAuthorities;
        }

        public IEnumerable<ShopAuthority> GetShopAuthoritiesByTypeInViewAllocService(params ShopAuthorityType[] types)
        {
            const string cmdText = @"SELECT * FROM [dbo].[ShopAuthority] 
                                    WHERE [shopAuthorityStatus]=1 AND [isViewAllocWorkerEnable]=1 AND [shopAuthorityType] IN (
                                    select d.x.value('./id[1]','int') from @xml.nodes('/*') as d(x)) ORDER BY [ShopAuthoritySequence]";

            var xml = new StringBuilder();
            foreach (var t in types)
            {
                xml.AppendFormat("<row><id>{0}</id></row>", (int)t);
            }

            SqlParameter cmdParm = new SqlParameter("@xml", SqlDbType.Xml) { Value = xml.ToString() };

            List<ShopAuthority> shopAuthorities = new List<ShopAuthority>();
            using (var dr = SqlHelper.ExecuteReader(Connection, CommandType.Text, cmdText, cmdParm))
            {

                while (dr.Read())
                {
                    shopAuthorities.Add((SqlHelper.GetEntity<ShopAuthority>(dr)));
                }
            }
            return shopAuthorities;

        }

        public IEnumerable<ShopAuthority> GetShopAuthoritiesByType(bool? isSybShow = null, params ShopAuthorityType[] types)
        {
            List<SqlParameter> cmdParms = new List<SqlParameter>();
            StringBuilder stringBuilderText = new StringBuilder();
            stringBuilderText.AppendLine("SELECT * FROM [dbo].[ShopAuthority]");
            stringBuilderText.AppendLine("WHERE [shopAuthorityStatus]=1 AND [isViewAllocWorkerEnable]=0");
            stringBuilderText.AppendLine("AND [shopAuthorityType] IN (select d.x.value('./id[1]','int') from @xml.nodes('/*') as d(x))");
            if (isSybShow.HasValue)
            {
                stringBuilderText.AppendLine("AND [isSYBShow]=@isSYBShow");
                cmdParms.Add(new SqlParameter("@isSYBShow", SqlDbType.Bit) { Value = isSybShow.Value });
            }
            stringBuilderText.AppendLine("ORDER BY [ShopAuthoritySequence]");
            string cmdText = stringBuilderText.ToString();

            var xml = new StringBuilder();
            foreach (var t in types)
            {
                xml.AppendFormat("<row><id>{0}</id></row>", (int)t);
            }

            cmdParms.Add(new SqlParameter("@xml", SqlDbType.Xml) { Value = xml.ToString() });

            List<ShopAuthority> shopAuthorities = new List<ShopAuthority>();
            using (var dr = SqlHelper.ExecuteReader(Connection, CommandType.Text, cmdText, cmdParms.ToArray()))
            {

                while (dr.Read())
                {
                    shopAuthorities.Add((SqlHelper.GetEntity<ShopAuthority>(dr)));
                }
            }
            return shopAuthorities;
        }

        public void Add(ShopAuthority shopAuthority)
        {
            const string cmdText = @"INSERT INTO [dbo].[ShopAuthority] ([shopAuthorityName],[shopAuthorityDescription],[shopAuthorityStatus],[shopAuthorityType],[ShopAuthoritySequence],[isClientShow],[isSYBShow],[authorityCode],[isViewAllocWorkerEnable]) 
                                                                 VALUES(@shopAuthorityName,@shopAuthorityDescription,@shopAuthorityStatus,@shopAuthorityType,@ShopAuthoritySequence,@isClientShow,@isSYBShow,@authorityCode,@isViewAllocWorkerEnable)";

            SqlParameter[] cmdParms = new SqlParameter[]
            {
                new SqlParameter("@shopAuthorityName", SqlDbType.NVarChar,50){Value = shopAuthority.ShopAuthorityName},
                new SqlParameter("@shopAuthorityDescription", SqlDbType.NVarChar, 500){Value = shopAuthority.ShopAuthorityDescription},
                new SqlParameter("@shopAuthorityStatus", SqlDbType.Int){Value = shopAuthority.ShopAuthorityStatus},
                new SqlParameter("@shopAuthorityType", SqlDbType.SmallInt){Value = shopAuthority.ShopAuthorityType},
                new SqlParameter("@ShopAuthoritySequence",SqlDbType.Int){Value = shopAuthority.ShopAuthoritySequence},
                new SqlParameter("@isClientShow", SqlDbType.Bit){Value = shopAuthority.IsClientShow},
                new SqlParameter("@isSYBShow",SqlDbType.Bit){Value = shopAuthority.IsSYBShow},
                new SqlParameter("@authorityCode", SqlDbType.VarChar,5){Value = shopAuthority.AuthorityCode},
                new SqlParameter("@isViewAllocWorkerEnable", SqlDbType.Bit){Value = shopAuthority.IsViewAllocWorkerEnable} 
            };

            SqlHelper.ExecuteNonQuery(Connection, CommandType.Text, cmdText, cmdParms);
        }

        public ShopAuthority GetShopAuthorityByCode(string code)
        {
            const string cmdText = @"SELECT * FROM [dbo].[ShopAuthority] WHERE [authorityCode]=@authorityCode";
            SqlParameter cmdParm = new SqlParameter("@authorityCode", SqlDbType.VarChar, 5) { Value = code };

            ShopAuthority shopAuthority = null;
            using (var dr = SqlHelper.ExecuteReader(Connection, CommandType.Text, cmdText, cmdParm))
            {
                if (dr.Read())
                {
                    shopAuthority = SqlHelper.GetEntity<ShopAuthority>(dr);
                }
            }
            return shopAuthority;
        }
        
        public DataTable QueryUxianServiceAuthorityOld()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select shopAuthorityName from ShopAuthority where authorityCode in (1,2)");
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }

        public IEnumerable<ShopAuthority> GetAllShopAuthorities()
        {
            const string cmdText = @"SELECT * FROM [dbo].[ShopAuthority]";
            List<ShopAuthority> shopAuthorities = new List<ShopAuthority>();
            using (var dr = SqlHelper.ExecuteReader(Connection, CommandType.Text, cmdText))
            {

                while (dr.Read())
                {
                    shopAuthorities.Add((SqlHelper.GetEntity<ShopAuthority>(dr)));
                }
            }
            return shopAuthorities;
        }

        public void Delete(ShopAuthority shopAuthority)
        {
            const string cmdText = @"DELETE FROM [dbo].[ShopAuthority] WHERE [ShopAuthorityId]=@ShopAuthorityId";
            SqlParameter cmdParm = new SqlParameter("@ShopAuthorityId", SqlDbType.Int) { Value = shopAuthority.ShopAuthorityId };

            SqlHelper.ExecuteNonQuery(Connection, CommandType.Text, cmdText, cmdParm);
        }
    }
}
