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
    public class SqlServerFoodDiaryRepository : SqlServerRepositoryBase, IFoodDiaryRepository
    {
        public SqlServerFoodDiaryRepository(ISqlConnectionFactory connectionFactory)
            : base(connectionFactory)
        {
        }

        public void Add(FoodDiary entity)
        {
            const string cmdText = @"INSERT INTO [dbo].[FoodDiary]
           ([OrderId]
           ,[Name]
           ,[Content]
           ,[Weather]
           ,[ShopName]
           ,[ShoppingDate]
           ,[Shared]
           ,[CreateTime],[Hit],[IsBig],[IsHideDishName])
     VALUES
           (@OrderId
           ,@Name
           ,@Content
           ,@Weather
           ,@ShopName
           ,@ShoppingDate
           ,@Shared
           ,@CreateTime,0,0,0);SELECT @@IDENTITY;";

            SqlParameter[] cmdParms = new SqlParameter[]
            {
                new SqlParameter("@OrderId", entity.OrderId),
                new SqlParameter("@Name", entity.Name),
                new SqlParameter("@Content", entity.Content), 
                new SqlParameter("@Weather",entity.Weather), 
                new SqlParameter("@ShopName", entity.ShopName), 
                new SqlParameter("@ShoppingDate",entity.ShoppingDate), 
                new SqlParameter("@Shared",(byte)entity.Shared), 
                new SqlParameter("@CreateTime", entity.CreateTime)
            };

            object obj_value = SqlHelper.ExecuteScalar(Connection, CommandType.Text, cmdText, cmdParms);
            entity.Id = Convert.ToInt64(obj_value);
        }



        public void Update(FoodDiary entity)
        {
            const string cmdText = @"UPDATE [dbo].[FoodDiary]
   SET [OrderId] = @OrderId
      ,[Name] = @Name
      ,[Content] = @Content
      ,[Weather] = @Weather
      ,[ShopName] = @ShopName
      ,[ShoppingDate] = @ShoppingDate
      ,[Shared] = @Shared
      ,[CreateTime] = @CreateTime,[IsBig]=@IsBig,[IsHideDishName]=@IsHideDishName
 WHERE [Id] = @Id";

            SqlParameter[] cmdParms = new SqlParameter[]
            {
                new SqlParameter("@OrderId", entity.OrderId),
                new SqlParameter("@Name", entity.Name),
                new SqlParameter("@Content", entity.Content), 
                new SqlParameter("@Weather",entity.Weather), 
                new SqlParameter("@ShopName", entity.ShopName), 
                new SqlParameter("@ShoppingDate",entity.ShoppingDate), 
                new SqlParameter("@Shared",(byte)entity.Shared), 
                new SqlParameter("@CreateTime", entity.CreateTime),
                new SqlParameter("@Id",entity.Id), 
                new SqlParameter("@IsBig",entity.IsBig),
                new SqlParameter("@IsHideDishName",entity.IsHideDishName), 
            };

            SqlHelper.ExecuteNonQuery(Connection, CommandType.Text, cmdText, cmdParms);
        }



        public FoodDiary GetById(long id)
        {
            const string cmdText = @"SELECT * FROM [dbo].[FoodDiary] WHERE [Id]=@Id";

            var cmdParm = new SqlParameter("@Id", id);
            FoodDiary foodDiary = null;
            using (var dr = SqlHelper.ExecuteReader(Connection, CommandType.Text, cmdText, cmdParm))
            {
                if (dr.Read())
                    foodDiary = SqlHelper.GetEntity<FoodDiary>(dr);
            }
            return foodDiary;
        }



        public FoodDiary GetFoodDiaryByOrderId(long orderId)
        {
            const string cmdText = @"SELECT * FROM [dbo].[FoodDiary] WHERE [OrderId]=@OrderId";

            var cmdParm = new SqlParameter("@OrderId", orderId);
            FoodDiary foodDiary = null;
            using (var dr = SqlHelper.ExecuteReader(Connection, CommandType.Text, cmdText, cmdParm))
            {
                if (dr.Read())
                    foodDiary = SqlHelper.GetEntity<FoodDiary>(dr);
            }
            return foodDiary;
        }

        public void IncrementHit(long id)
        {
            const string cmdText = @"UPDATE [dbo].[FoodDiary]
                                   SET [Hit]= ISNULL([Hit],0)+1
                                 WHERE [Id] = @Id";

            var cmdParm = new SqlParameter("@Id", SqlDbType.BigInt) { Value = id };

            SqlHelper.ExecuteNonQuery(Connection, CommandType.Text, cmdText, cmdParm);
        }



        public IEnumerable<FoodDiaryDetails> GetPageFoodDiaryDetailses(Page page, string mobilePhoneNumber, bool isPaid, DateTime startDate,
            DateTime endDate, out int count)
        {
            StringBuilder stringBuilderCount = new StringBuilder(@"SELECT COUNT(1) from FoodDiary as A
inner join PreOrder19dian as B on A.OrderId=B.preOrder19dianId
inner join CustomerInfo as C on C.CustomerID=B.customerId
where A.Shared > 0 and isPaid=@isPaid ");

            StringBuilder stringBuilder = new StringBuilder(@"select t.Id,t.Content,t.CreateTime,t.Hit,t.isPaid,t.mobilePhoneNumber,t.Name,t.prePaidSum,t.prePayTime,
t.Shared,t.ShopName from (select row_number() OVER (ORDER BY A.CreateTime) rn, A.Id, B.prePayTime,A.CreateTime,C.mobilePhoneNumber ,A.Name,B.prePaidSum,B.isPaid,A.ShopName,A.shared,A.Content,ISNULL(A.Hit,0) AS Hit
from FoodDiary as A
inner join PreOrder19dian as B on A.OrderId=B.preOrder19dianId
inner join CustomerInfo as C on C.CustomerID=B.customerId
where A.Shared > 0 and isPaid=@isPaid ");
            if (!string.IsNullOrEmpty(mobilePhoneNumber))
            {
                stringBuilder.Append(" and C.mobilePhoneNumber=@mobilePhoneNumber ");
                stringBuilderCount.Append(@" and C.mobilePhoneNumber=@mobilePhoneNumber ");
            }
            stringBuilderCount.Append(@" and A.CreateTime >@startDate and A.CreateTime <@endDate");
            stringBuilder.Append(@" and A.CreateTime >@startDate and A.CreateTime <@endDate) as t
where t.rn between @startIndex and @endIndex");


            string cmdText = stringBuilder.ToString();
            string cmdTextCount = stringBuilderCount.ToString();
            List<SqlParameter> listCountParms = new List<SqlParameter>()
            {
                new SqlParameter("@isPaid", SqlDbType.TinyInt) {Value = isPaid ? 1 : 0},
                new SqlParameter("@startDate", SqlDbType.DateTime) {Value = startDate},
                new SqlParameter("@endDate", SqlDbType.DateTime) {Value = endDate},
            };
            if (!string.IsNullOrEmpty(mobilePhoneNumber))
            {
                listCountParms.Add(new SqlParameter("@mobilePhoneNumber", SqlDbType.NVarChar, 50) { Value = mobilePhoneNumber });
            }

            SqlParameter[] cmdCountParms = listCountParms.ToArray();
            listCountParms.AddRange(new SqlParameter[]{
                new SqlParameter("@startIndex", SqlDbType.Int) {Value = page.Skip+1},
                new SqlParameter("@endIndex", SqlDbType.Int) {Value = page.Skip+page.PageSize}
            });
            SqlParameter[] cmdParms = listCountParms.ToArray();

            object o = SqlHelper.ExecuteScalar(Connection, CommandType.Text, cmdTextCount, cmdCountParms);
            count = Convert.ToInt32(o);

            List<FoodDiaryDetails> list = new List<FoodDiaryDetails>();
            using (var dr = SqlHelper.ExecuteReader(Connection, CommandType.Text, cmdText, cmdParms))
            {
                while (dr.Read())
                {
                    list.Add(SqlHelper.GetEntity<FoodDiaryDetails>(dr));
                }
            }
            return list;
            //return new StaticPagedList<FoodDiaryDetails>(list, page.PageNumber, page.PageSize, count);
        }

        public IEnumerable<FoodDiaryDetails> GetManyFoodDiaryDetailses(string mobilePhoneNumber, bool isPaid, DateTime startDate, DateTime endDate)
        {

            List<SqlParameter> listCountParms = new List<SqlParameter>()
            {
                new SqlParameter("@isPaid", SqlDbType.TinyInt) {Value = isPaid ? 1 : 0},
                new SqlParameter("@startDate", SqlDbType.DateTime) {Value = startDate},
                new SqlParameter("@endDate", SqlDbType.DateTime) {Value = endDate},
            };

            StringBuilder stringBuilder = new StringBuilder(@"select A.Id, B.prePayTime,A.CreateTime,C.mobilePhoneNumber ,A.Name,B.prePaidSum,B.isPaid,A.ShopName,A.shared,A.Content,ISNULL(A.Hit,0) AS Hit
from FoodDiary as A
inner join PreOrder19dian as B on A.OrderId=B.preOrder19dianId
inner join CustomerInfo as C on C.CustomerID=B.customerId
where A.Shared > 0 and isPaid=@isPaid ");
            if (!string.IsNullOrEmpty(mobilePhoneNumber))
            {
                stringBuilder.Append(" and C.mobilePhoneNumber=@mobilePhoneNumber ");
                listCountParms.Add(new SqlParameter("@mobilePhoneNumber", SqlDbType.NVarChar, 50) { Value = mobilePhoneNumber });

            }
            stringBuilder.Append(@" and A.CreateTime >@startDate and A.CreateTime <@endDate ORDER BY A.CreateTime");


            string cmdText = stringBuilder.ToString();


            SqlParameter[] cmdParms = listCountParms.ToArray();

            using (var dr = SqlHelper.ExecuteReader(Connection, CommandType.Text, cmdText, cmdParms))
            {
                while (dr.Read())
                {
                    yield return SqlHelper.GetEntity<FoodDiaryDetails>(dr);
                }
            }


        }
    }
}