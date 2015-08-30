using System.Data;
using System.Data.SqlClient;
using VAGastronomistMobileApp.DBUtility;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL.Persistence.Infrastructure;

namespace VAGastronomistMobileApp.SQLServerDAL.Persistence
{
    public interface ICityRepository : IRepository<City>
    {
    }

    public class EntityFrameworkCityRepository : EntityFrameworkRepositoryBase<City>, ICityRepository
    {
        public EntityFrameworkCityRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }

    public class SqlServerCityRepository : ICityRepository
    {
        public void Add(City entity)
        {
            throw new System.NotImplementedException();
        }

        public void AddRange(System.Collections.Generic.IEnumerable<City> entities)
        {
            throw new System.NotImplementedException();
        }

        public void Update(City entity)
        {
            throw new System.NotImplementedException();
        }

        public void Update(System.Linq.Expressions.Expression<System.Func<City, bool>> filterExpression, System.Linq.Expressions.Expression<System.Func<City, City>> updateExpression)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(City entity)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(System.Linq.Expressions.Expression<System.Func<City, bool>> where)
        {
            throw new System.NotImplementedException();
        }

        public City GetById(string id)
        {
            throw new System.NotImplementedException();
        }

        public City GetById(long id)
        {
            const string cmdText = @"SELECT * FROM [dbo].[City] WHERE [cityID]=@cityID";
            SqlParameter cmdParm = new SqlParameter("@cityID", id);

            City city = null;
            using (var dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, cmdText, cmdParm))
            {
                if (dr.Read())
                {
                    city = SqlHelper.GetEntity<City>(dr);
                }
            }
            return city;
        }

        public City Get(System.Linq.Expressions.Expression<System.Func<City, bool>> where)
        {
            throw new System.NotImplementedException();
        }

        public System.Collections.Generic.IEnumerable<City> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public System.Collections.Generic.IEnumerable<City> GetMany(System.Linq.Expressions.Expression<System.Func<City, bool>> where)
        {
            throw new System.NotImplementedException();
        }

        public PagedList.IPagedList<City> GetPage<TOrder>(Page page, System.Linq.Expressions.Expression<System.Func<City, bool>> where, System.Linq.Expressions.Expression<System.Func<City, TOrder>> order)
        {
            throw new System.NotImplementedException();
        }
    }
}
