using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using VAGastronomistMobileApp.DBUtility;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL.Persistence.Infrastructure;

namespace VAGastronomistMobileApp.SQLServerDAL.Persistence
{
    public class SqlServerMenuUpdateTaskRepository : SqlServerRepositoryBase, IMenuUpdateTaskRepository
    {
        public SqlServerMenuUpdateTaskRepository(ISqlConnectionFactory connectionFactory)
            : base(connectionFactory)
        {
        }

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
           ,@FailureCount); SELECT @@IDENTITY;";
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

            object obj_value = SqlHelper.ExecuteScalar(Connection, CommandType.Text, cmdTextTask,
                cmdParmsTask);
            entity.Id = Convert.ToInt64(obj_value);
            SqlParameter[] cmdParmsMenuUpdateTask = new SqlParameter[]
            {
                new SqlParameter("@Id", entity.Id), 
                new SqlParameter("@MenuId", entity.MenuId), 
                new SqlParameter("@EmployeeId", entity.EmployeeId), 
            };

            SqlHelper.ExecuteNonQuery(Connection, CommandType.Text,
                cmdTextMenuUpdateTask, cmdParmsMenuUpdateTask);

        }

        public void Update(MenuUpdateTask entity)
        {
            const string cmdText = @"UPDATE [dbo].[Task]
   SET [Status] = @Status
      ,[CreateTime] = @CreateTime
      ,[BeginTime] = @BeginTime
      ,[EndTime] = @EndTime
      ,[FailureCount] = @FailureCount
 WHERE [Id] = @Id;
UPDATE [dbo].[MenuUpdateTask]
   SET [MenuId] = @MenuId
      ,[EmployeeId] = @EmployeeId
 WHERE [Id] = @Id;";

            SqlParameter[] cmdParms = new SqlParameter[]
            {
                new SqlParameter("@Status", SqlDbType.TinyInt) {Value = entity.Status},
                new SqlParameter("@CreateTime", SqlDbType.DateTime) {Value = entity.CreateTime},
                new SqlParameter("@BeginTime", SqlDbType.DateTime) {Value = entity.BeginTime.HasValue?(object) entity.BeginTime.Value:DBNull.Value, IsNullable = true},
                new SqlParameter("@EndTime", SqlDbType.DateTime) {Value = entity.EndTime.HasValue?(object) entity.EndTime.Value:DBNull.Value, IsNullable = true},
                new SqlParameter("@FailureCount", SqlDbType.Int) {Value = entity.FailureCount},
                new SqlParameter("@Id", SqlDbType.BigInt) {Value = entity.Id},
                new SqlParameter("@MenuId", SqlDbType.Int) {Value = entity.MenuId},
                new SqlParameter("@EmployeeId", SqlDbType.Int) {Value = entity.EmployeeId},
            };

            SqlHelper.ExecuteNonQuery(Connection, CommandType.Text, cmdText, cmdParms);
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
            using (var dr = SqlHelper.ExecuteReader(Connection, CommandType.Text, cmdText, cmdParm))
            {
                if (dr.Read())
                {
                    menuUpdateTask = SqlHelper.GetEntity<MenuUpdateTask>(dr);
                }
            }
            return menuUpdateTask;
        }

        public IEnumerable<MenuUpdateTask> GetAllFailureTask()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<MenuUpdateTask> GetAllFailureTask(int failureCount)
        {
            const string cmdText = @"SELECT a.[Id]
      ,[Status]
      ,[CreateTime]
      ,[BeginTime]
      ,[EndTime]
      ,[FailureCount],b.EmployeeId,b.MenuId
  FROM [dbo].[Task] a
  INNER JOIN [dbo].[MenuUpdateTask] b on a.[Id]=b.[Id] WHERE a.[Status]=2 AND a.[FailureCount]<@FailureCount";

            SqlParameter cmdParm = new SqlParameter("@FailureCount", failureCount);

            List<MenuUpdateTask> list = new List<MenuUpdateTask>();
            using (var dr = SqlHelper.ExecuteReader(Connection, CommandType.Text, cmdText, cmdParm))
            {
                while (dr.Read())
                {
                    list.Add(SqlHelper.GetEntity<MenuUpdateTask>(dr));
                }
            }
            return list;
        }
    }
}