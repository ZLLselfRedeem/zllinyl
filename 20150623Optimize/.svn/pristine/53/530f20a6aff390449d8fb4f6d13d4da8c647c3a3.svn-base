using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.Model;
using System.Data.SqlClient;

namespace VAGastronomistMobileApp.SQLServerDAL
{
    public class ImageTaskManager
    {
        public SqlTransaction Transaction { private set; get; }

        public ImageTaskManager(SqlTransaction tran)
        {
            this.Transaction = tran;
        }

        public int Insert(ImageTask itask)
        {
            string cmdText = "INSERT INTO [dbo].[ImageTask]([Name],[Extension],[Path],[UserId],[CreateTime],[FileName],[ShopId],[MenuId],[EqualProportion]) VALUES(@Name,@Extension,@Path, @UserId,@CreateTime,@FileName,@ShopId,@MenuId,@EqualProportion);SELECT @@IDENTITY";
            SqlParameter[] cmdParms = new SqlParameter[] { 
                new SqlParameter("@Name", itask.Name),
                new SqlParameter("@Extension", itask.Extension),
                new SqlParameter("@Path", itask.Path),
                new SqlParameter("@UserId", itask.UserId),
                new SqlParameter("@CreateTime", itask.CreateTime),
                new SqlParameter("@FileName", itask.FileName),
                new SqlParameter("@ShopId", itask.ShopId),
                new SqlParameter("@MenuId", itask.MenuId),
                new SqlParameter("@EqualProportion",itask.EqualProportion)
            };

            using (SqlCommand cmd = new SqlCommand(cmdText, this.Transaction.Connection, this.Transaction))
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddRange(cmdParms);

                object count = cmd.ExecuteScalar();
                return Convert.ToInt32(count);
            }
        }

        public ImageTask GetImageTaskById(int id)
        {
            string cmdText = "SELECT [Id],[Name],[Extension],[Path],[UserId],[CreateTime],[FileName],[ShopId],[MenuId],[EqualProportion] FROM [dbo].[ImageTask] where [Id]=@Id";
            SqlParameter[] cmdParms = new SqlParameter[] {
                new SqlParameter("@Id",id)
             };
            using (SqlCommand cmd = new SqlCommand(cmdText, this.Transaction.Connection, this.Transaction))
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddRange(cmdParms);
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        ImageTask itask = new ImageTask
                        {
                            Id = dr.GetInt32(0),
                            Name = dr.GetString(1),
                            Extension = dr.GetString(2),
                            Path = dr.GetString(3),
                            UserId = dr.GetInt32(4),
                            CreateTime = dr.GetDateTime(5),
                            FileName = dr.GetString(6),
                            ShopId = dr.GetInt32(7),
                            MenuId = dr.GetInt32(8),
                            EqualProportion = dr.GetBoolean(9)
                        };

                        return itask;
                    }
                }
            }

            return null;
        }

        public void DeleteById(int id)
        {
            string cmdText = "DELETE FROM [dbo].[ImageTask] WHERE [Id]=@Id";
            SqlParameter[] cmdParms = new SqlParameter[] {
                new SqlParameter("@Id",id)
             };
            using (SqlCommand cmd = new SqlCommand(cmdText, this.Transaction.Connection, this.Transaction))
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddRange(cmdParms);

                cmd.ExecuteNonQuery();
            }
        }

        public int GetCountByCompanyShop(int userId, int shopId)
        {
            string cmdText = "SELECT COUNT(*) FROM [dbo].[ImageTask] WHERE [UserId]=@UserId AND [ShopId]=@ShopId";
            SqlParameter[] cmdParms = new SqlParameter[] {
                new SqlParameter("@UserId",userId),
                new SqlParameter("@ShopId",shopId),
             };

            using (SqlCommand cmd = new SqlCommand(cmdText, this.Transaction.Connection, this.Transaction))
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddRange(cmdParms);

                object obj = cmd.ExecuteScalar();

                return Convert.ToInt32(obj);
            }
        }

        public List<ImageTask> GetListByCompanyShop(int userId, int shopId)
        {
            string cmdText = "SELECT [Id],[Name],[Extension],[Path],[UserId],[CreateTime],[FileName],[ShopId],[MenuId],[EqualProportion] FROM [dbo].[ImageTask] WHERE [UserId]=@UserId AND [ShopId]=@ShopId";
            SqlParameter[] cmdParms = new SqlParameter[] {
                new SqlParameter("@UserId",userId),
                new SqlParameter("@ShopId",shopId),
             };

            List<ImageTask> list = new List<ImageTask>();
            using (SqlCommand cmd = new SqlCommand(cmdText, this.Transaction.Connection, this.Transaction))
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddRange(cmdParms);
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        list.Add(new ImageTask
                        {
                            Id = dr.GetInt32(0),
                            Name = dr.GetString(1),
                            Extension = dr.GetString(2),
                            Path = dr.GetString(3),
                            UserId = dr.GetInt32(4),
                            CreateTime = dr.GetDateTime(5),
                            FileName = dr.GetString(6),
                            ShopId = dr.GetInt32(7),
                            MenuId = dr.GetInt32(8),
                            EqualProportion = dr.GetBoolean(9)
                        });
                    }

                    return list;
                }
            }
        }

        public void DeleteByCompanyShop(int userId, int shopId)
        {
            string cmdText = "DELETE FROM [dbo].[ImageTask] WHERE [UserId]=@UserId AND [ShopId]=@ShopId";
            SqlParameter[] cmdParms = new SqlParameter[] {
                new SqlParameter("@UserId",userId),
                new SqlParameter("@ShopId",shopId),
             };

            using (SqlCommand cmd = new SqlCommand(cmdText, this.Transaction.Connection, this.Transaction))
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddRange(cmdParms);

                cmd.ExecuteNonQuery();
            }
        }
    }
}
