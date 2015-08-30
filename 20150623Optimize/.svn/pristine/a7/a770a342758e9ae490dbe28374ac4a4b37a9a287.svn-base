using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using PagedList;
using VAGastronomistMobileApp.DBUtility;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL.Persistence.Infrastructure;

namespace VAGastronomistMobileApp.SQLServerDAL.Persistence
{
    public interface IEmployeeInfoRepository : IRepository<EmployeeInfo>
    {
    }

    public class EntityFrameworkEmployeeInfoRepository : EntityFrameworkRepositoryBase<EmployeeInfo>, IEmployeeInfoRepository
    {
        public EntityFrameworkEmployeeInfoRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }

    public class SqlServerEmployeeInfoRepository : IEmployeeInfoRepository
    {
        public void Add(EmployeeInfo entity)
        {
            throw new NotImplementedException();
        }

        public void AddRange(IEnumerable<EmployeeInfo> entities)
        {
            throw new NotImplementedException();
        }

        public void Update(EmployeeInfo entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Expression<Func<EmployeeInfo, bool>> filterExpression, Expression<Func<EmployeeInfo, EmployeeInfo>> updateExpression)
        {
            throw new NotImplementedException();
        }

        public void Delete(EmployeeInfo entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Expression<Func<EmployeeInfo, bool>> @where)
        {
            throw new NotImplementedException();
        }

        public EmployeeInfo GetById(string id)
        {
            throw new NotImplementedException();
        }

        public EmployeeInfo GetById(long id)
        {
            const string cmdText = @"SELECT * FROM [dbo].[EmployeeInfo] WHERE [EmployeeID]=@EmployeeID";

            SqlParameter cmdParm = new SqlParameter("@EmployeeID", id);
            EmployeeInfo employeeInfo = new EmployeeInfo();
            using (var dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, cmdText, cmdParm))
            {
                if (dr.Read())
                {
                    employeeInfo = SqlHelper.GetEntity<EmployeeInfo>(dr);
                }
            }

            return employeeInfo;

        }

        public EmployeeInfo Get(Expression<Func<EmployeeInfo, bool>> @where)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<EmployeeInfo> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<EmployeeInfo> GetMany(Expression<Func<EmployeeInfo, bool>> @where)
        {
            throw new NotImplementedException();
        }

        public IPagedList<EmployeeInfo> GetPage<TOrder>(Page page, Expression<Func<EmployeeInfo, bool>> @where, Expression<Func<EmployeeInfo, TOrder>> order)
        {
            throw new NotImplementedException();
        }
    }
}
