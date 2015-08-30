using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using PagedList;
using VAGastronomistMobileApp.DBUtility;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL.Persistence.Infrastructure;

namespace VAGastronomistMobileApp.SQLServerDAL.Persistence
{
    public class SqlServerCustomerInfoRepository : SqlServerRepositoryBase, ICustomerInfoRepository
    {
        public SqlServerCustomerInfoRepository(ISqlConnectionFactory connectionFactory)
            : base(connectionFactory)
        {
        }

        public CustomerInfo GetById(long id)
        {
            const string cmdText = @"SELECT * FROM [dbo].[CustomerInfo] WHERE [CustomerID]=@CustomerID";
            var cmdParm = new SqlParameter("@CustomerID", id);

            CustomerInfo customerInfo = null;
            using (var dr = SqlHelper.ExecuteReader(Connection, CommandType.Text, cmdText, cmdParm))
            {
                if (dr.Read())
                {
                    customerInfo = dr.GetEntity<CustomerInfo>();
                }
            }
            return customerInfo;
        }

        public IPagedList<CustomerInfo> GetPage(Page page)
        {
            const string cmdTextCount = @"SELECT COUNT(1) FROM dbo.CustomerInfo";
            const string cmdText = @"SELECT * FROM (SELECT ROW_NUMBER() OVER(ORDER BY CustomerID) as RowNum,* from [CustomerInfo]) AS t
WHERE t.RowNum BETWEEN @BeginIndex AND @EndIndex";

            SqlParameter[] cmdParms = new SqlParameter[]
			{
				new SqlParameter("@BeginIndex", SqlDbType.Int) {Value = page.Skip + 1},
				new SqlParameter("@EndIndex", SqlDbType.Int) {Value = page.Skip + page.PageSize},
			};

            object o = SqlHelper.ExecuteScalar(Connection, CommandType.Text, cmdTextCount);
            int count = Convert.ToInt32(o);

            List<CustomerInfo> list = new List<CustomerInfo>();
            using (var dr = SqlHelper.ExecuteReader(Connection, CommandType.Text, cmdText, cmdParms))
            {
                while (dr.Read())
                {
                    list.Add(dr.GetEntity<CustomerInfo>());
                }
            }

            return new StaticPagedList<CustomerInfo>(list, page.PageNumber, page.PageSize, count);
        }

        public void UpdateCustomerPicture(long id, string picture)
        {
            const string cmdText = @"UPDATE dbo.CustomerInfo SET Picture=@Picture, IsUpdatePicture=1 WHERE CustomerID=@Id";

            SqlParameter[] cmdParms = new SqlParameter[]
            {
                new SqlParameter("@Picture", SqlDbType.NVarChar,256){Value = picture},
                new SqlParameter("@Id", SqlDbType.BigInt){Value = id}, 
            };

            SqlHelper.ExecuteNonQuery(Connection, CommandType.Text, cmdText, cmdParms);
        }

        public CustomerInfo GetByCookie(string cookie)
        {
            const string cmdText = "SELECT * FROM dbo.CustomerInfo WITH(NOLOCK) WHERE [cookie]=@cookie";
            SqlParameter cmdParm = new SqlParameter("@cookie", SqlDbType.NVarChar, 100) { Value = string.IsNullOrEmpty(cookie) ? "" : cookie };

            CustomerInfo customerInfo = null;
            using (var dr = SqlHelper.ExecuteReader(Connection, CommandType.Text, cmdText, cmdParm))
            {
                if (dr.Read())
                {
                    customerInfo = dr.GetEntity<CustomerInfo>();
                }
            }
            return customerInfo;
        }

        public CustomerInfo GetByMobilePhoneNumber(string mobilePhone)
        {
            const string cmdText = "SELECT * FROM dbo.CustomerInfo WHERE [mobilePhoneNumber]=@mobilePhoneNumber";
            SqlParameter cmdParm = new SqlParameter("@mobilePhoneNumber", SqlDbType.NVarChar, 100) { Value = string.IsNullOrEmpty(mobilePhone) ? "" : mobilePhone };

            CustomerInfo customerInfo = null;
            using (var dr = SqlHelper.ExecuteReader(Connection, CommandType.Text, cmdText, cmdParm))
            {
                if (dr.Read())
                {
                    customerInfo = dr.GetEntity<CustomerInfo>();
                }
            }
            return customerInfo;
        }
    }
}
