using System.Data;
using System.Data.SqlClient;
using System.Linq;
using EntityFramework.Extensions;
using VAGastronomistMobileApp.DBUtility;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL.Persistence.Infrastructure;

namespace VAGastronomistMobileApp.SQLServerDAL.Persistence
{
    public interface IDishPriceInfoRepository : IRepository<DishPriceInfo>
    {
        int UpdatePrice(int dishPriceId, double price);
    }

    public class EntityFrameworkDishPriceInfoRepository : EntityFrameworkRepositoryBase<DishPriceInfo>, IDishPriceInfoRepository
    {
        public EntityFrameworkDishPriceInfoRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        public int UpdatePrice(int dishPriceId, double price)
        {
            var query = DataContext.DishPriceInfos.Where(p => p.DishPriceID == dishPriceId);
            return query.Update(dpi => new DishPriceInfo() { DishPrice = price });
        }
    }

    public class SqlServerDishPriceInfoRepository : IDishPriceInfoRepository
    {

        public void Add(DishPriceInfo entity)
        {
            throw new System.NotImplementedException();
        }

        public void AddRange(System.Collections.Generic.IEnumerable<DishPriceInfo> entities)
        {
            throw new System.NotImplementedException();
        }

        public void Update(DishPriceInfo entity)
        {
            const string cmdText = @"USE [VAGastronomistMobileApp]
GO

UPDATE [dbo].[DishPriceInfo]
   SET [DishPrice] = @DishPrice, float,>
      ,[DishID] = @DishID, int,>
      ,[DishSoldout] = @DishSoldout, bit,>
      ,[DishPriceStatus] = @DishPriceStatus, int,>
      ,[DishNeedWeigh] = @DishNeedWeigh, bit,>
      ,[vipDiscountable] = @vipDiscountable, bit,>
      ,[backDiscountable] = <backDiscountable, bit,>
      ,[dishIngredientsMinAmount] = <dishIngredientsMinAmount, int,>
      ,[dishIngredientsMaxAmount] = <dishIngredientsMaxAmount, int,>
      ,[sundryJson] = <sundryJson, nvarchar(max),>
 WHERE ";
        }

        public void Update(System.Linq.Expressions.Expression<System.Func<DishPriceInfo, bool>> filterExpression, System.Linq.Expressions.Expression<System.Func<DishPriceInfo, DishPriceInfo>> updateExpression)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(DishPriceInfo entity)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(System.Linq.Expressions.Expression<System.Func<DishPriceInfo, bool>> where)
        {
            throw new System.NotImplementedException();
        }

        public DishPriceInfo GetById(string id)
        {
            throw new System.NotImplementedException();

        }

        public DishPriceInfo GetById(long id)
        {
            const string cmdText = @"SELECT * FROM [dbo].[DishPriceInfo] WHERE [DishPriceID]=@DishPriceID";
            var cmdParm = new SqlParameter("@DishPriceID", id);

            DishPriceInfo dishPriceInfo = null;
            using (var dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, cmdText, cmdParm))
            {
                if (dr.Read())
                {
                    dishPriceInfo = SqlHelper.GetEntity<DishPriceInfo>(dr);
                }
            }
            return dishPriceInfo;
        }

        public DishPriceInfo Get(System.Linq.Expressions.Expression<System.Func<DishPriceInfo, bool>> where)
        {
            throw new System.NotImplementedException();
        }

        public System.Collections.Generic.IEnumerable<DishPriceInfo> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public System.Collections.Generic.IEnumerable<DishPriceInfo> GetMany(System.Linq.Expressions.Expression<System.Func<DishPriceInfo, bool>> where)
        {
            throw new System.NotImplementedException();
        }

        public PagedList.IPagedList<DishPriceInfo> GetPage<TOrder>(Page page, System.Linq.Expressions.Expression<System.Func<DishPriceInfo, bool>> where, System.Linq.Expressions.Expression<System.Func<DishPriceInfo, TOrder>> order)
        {
            throw new System.NotImplementedException();
        }

        public int UpdatePrice(int dishPriceId, double price)
        {
            const string cmdText = @"UPDATE [dbo].[DishPriceInfo]
   SET [DishPrice] = @DishPrice WHERE [DishPriceID]=@DishPriceID";

            SqlParameter[] cmdParms = new SqlParameter[]
            {
                new SqlParameter("@DishPrice",price), 
                new SqlParameter("@DishPriceID", dishPriceId), 
            };

            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, cmdText, cmdParms);
        }
    }
}
