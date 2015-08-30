using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq.Expressions;
using PagedList;
using VAGastronomistMobileApp.DBUtility;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL.Persistence.Infrastructure;

namespace VAGastronomistMobileApp.SQLServerDAL.Persistence
{
    public interface IMenuUpdateTaskRepository : IRepository<MenuUpdateTask> { }

    public class EntityFrameworkMenuUpdateTaskRepository : EntityFrameworkRepositoryBase<MenuUpdateTask>, IMenuUpdateTaskRepository
    {
        public EntityFrameworkMenuUpdateTaskRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }

    public class SqlServerMenuUpdateTaskRepository : IMenuUpdateTaskRepository
    {
        public void Add(MenuUpdateTask entity)
        {
            const string cmdTextTask = @"INSERT INTO [dbo].[Task]
           ([Status]
           ,[CreateTime]
           ,[BeginTime]
           ,[EndTime]
           ,[FailureCount])
     VALUES
           (@Status
           ,@CreateTime
           ,@BeginTime
           ,@EndTime
           ,@FailureCount); SELECT @@IDENTITY ";
            const string cmdTextMenuUpdateTask = @"INSERT INTO [dbo].[MenuUpdateTask]
           ([Id]
           ,[MenuId]
           ,[EmployeeId])
     VALUES
           (@Id
           ,@MenuId
           ,@EmployeeId)";


            SqlParameter[] cmdParmsTask = new SqlParameter[]
            {
                new SqlParameter("@Status", (byte) entity.Status),
                new SqlParameter("@CreateTime", entity.CreateTime),
                new SqlParameter("@BeginTime", SqlDbType.DateTime) {Value = entity.BeginTime.HasValue?(object) entity.BeginTime.Value:DBNull.Value},
                new SqlParameter("@EndTime", SqlDbType.DateTime) {Value =entity.EndTime.HasValue?(object) entity.EndTime.Value:DBNull.Value},
                new SqlParameter("@FailureCount", entity.FailureCount),
            };

            object obj_value = SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, cmdTextTask,
                cmdParmsTask);
            entity.Id = Convert.ToInt64(obj_value);
            SqlParameter[] cmdParmsMenuUpdateTask = new SqlParameter[]
            {
                new SqlParameter("@Id", entity.Id), 
                new SqlParameter("@MenuId", entity.MenuId), 
                new SqlParameter("@EmployeeId", entity.EmployeeId), 
            };

            SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text,
                cmdTextMenuUpdateTask, cmdParmsMenuUpdateTask);

        }

        public void AddRange(IEnumerable<MenuUpdateTask> entities)
        {
            throw new NotImplementedException();
        }

        public void Update(MenuUpdateTask entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Expression<Func<MenuUpdateTask, bool>> filterExpression, Expression<Func<MenuUpdateTask, MenuUpdateTask>> updateExpression)
        {
            throw new NotImplementedException();
        }

        public void Delete(MenuUpdateTask entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Expression<Func<MenuUpdateTask, bool>> @where)
        {
            throw new NotImplementedException();
        }

        public MenuUpdateTask GetById(string id)
        {
            throw new NotImplementedException();
        }

        public MenuUpdateTask GetById(long id)
        {
            const string cmdText = @"SELECT a.[Id]
      ,[Status]
      ,[CreateTime]
      ,[BeginTime]
      ,[EndTime]
      ,[FailureCount],b.EmployeeId,b.MenuId
  FROM [dbo].[Task] a
  INNER JOIN [dbo].[MenuUpdateTask] b on a.[Id]=b.[Id] WHERE a.[Id]=@Id";

            SqlParameter cmdParm = new SqlParameter("@Id", id);

            MenuUpdateTask menuUpdateTask = null;
            using (var dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, cmdText, cmdParm))
            {
                if (dr.Read())
                {
                    menuUpdateTask = SqlHelper.GetEntity<MenuUpdateTask>(dr);
                }
            }
            return menuUpdateTask;
        }

        public MenuUpdateTask Get(Expression<Func<MenuUpdateTask, bool>> @where)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<MenuUpdateTask> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<MenuUpdateTask> GetMany(Expression<Func<MenuUpdateTask, bool>> @where)
        {
            throw new NotImplementedException();
        }

        public IPagedList<MenuUpdateTask> GetPage<TOrder>(Page page, Expression<Func<MenuUpdateTask, bool>> @where, Expression<Func<MenuUpdateTask, TOrder>> order)
        {
            throw new NotImplementedException();
        }
    }
}
