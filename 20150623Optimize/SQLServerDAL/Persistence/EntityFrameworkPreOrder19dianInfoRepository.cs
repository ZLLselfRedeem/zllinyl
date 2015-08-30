using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using VAGastronomistMobileApp.DBUtility;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL.Persistence.Infrastructure;

namespace VAGastronomistMobileApp.SQLServerDAL.Persistence
{
    public interface IPreOrder19DianInfoRepository : IRepository<PreOrder19dianInfo>
    {

    }

    public class EntityFrameworkPreOrder19DianInfoRepository : EntityFrameworkRepositoryBase<PreOrder19dianInfo>, IPreOrder19DianInfoRepository
    {
        public EntityFrameworkPreOrder19DianInfoRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }

    public class SqlServerPreOrder19DianInfoRepository : IPreOrder19DianInfoRepository
    {

        public void Add(PreOrder19dianInfo entity)
        {
            throw new System.NotImplementedException();
        }

        public void AddRange(System.Collections.Generic.IEnumerable<PreOrder19dianInfo> entities)
        {
            throw new System.NotImplementedException();
        }

        public void Update(PreOrder19dianInfo entity)
        {
            throw new System.NotImplementedException();
        }

        public void Update(System.Linq.Expressions.Expression<System.Func<PreOrder19dianInfo, bool>> filterExpression, System.Linq.Expressions.Expression<System.Func<PreOrder19dianInfo, PreOrder19dianInfo>> updateExpression)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(PreOrder19dianInfo entity)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(System.Linq.Expressions.Expression<System.Func<PreOrder19dianInfo, bool>> where)
        {
            throw new System.NotImplementedException();
        }

        public PreOrder19dianInfo GetById(string id)
        {
            throw new System.NotImplementedException();
        }

        public PreOrder19dianInfo GetById(long id)
        {
            const string cmdText = @"SELECT * FROM [dbo].[PreOrder19dian] WHERE [preOrder19dianId]=@preOrder19dianId";

            var cmdParm = new SqlParameter("@preOrder19dianId", id);

            PreOrder19dianInfo preOrder19DianInfo = null;
            using (var dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, cmdText, cmdParm))
            {
                if (dr.Read())
                    preOrder19DianInfo = SqlHelper.GetEntity<PreOrder19dianInfo>(dr);
            }
            return preOrder19DianInfo;
        }

        public PreOrder19dianInfo Get(System.Linq.Expressions.Expression<System.Func<PreOrder19dianInfo, bool>> where)
        {
            throw new System.NotImplementedException();
        }

        public System.Collections.Generic.IEnumerable<PreOrder19dianInfo> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public System.Collections.Generic.IEnumerable<PreOrder19dianInfo> GetMany(System.Linq.Expressions.Expression<System.Func<PreOrder19dianInfo, bool>> where)
        {
            throw new System.NotImplementedException();
        }

        public PagedList.IPagedList<PreOrder19dianInfo> GetPage<TOrder>(Page page, System.Linq.Expressions.Expression<System.Func<PreOrder19dianInfo, bool>> where, System.Linq.Expressions.Expression<System.Func<PreOrder19dianInfo, TOrder>> order)
        {
            throw new System.NotImplementedException();
        }
    }
}
