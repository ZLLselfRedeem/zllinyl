using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.DBUtility;
using VAGastronomistMobileApp.Model;

namespace VAGastronomistMobileApp.SQLServerDAL
{
    public class MessageFirstTitleManager
    {
        /// <summary>
        /// 用于填充标签下拉列表
        /// </summary>
        /// <param name="CityID"></param>
        /// <returns></returns>
        public List<MessageFirstTitleViewModel> GetHandleByCity(int CityID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Case when IsMaster=1 then 0 else Id end Id,TitleName,IsMaster from MessageFirstTitle ");
            strSql.Append(" where CityID=@CityID and status=1 and Enable=1 order by TitleIndex ");

            SqlParameter[] parameter = new SqlParameter[]{
                new SqlParameter("CityID",SqlDbType.Int){Value=CityID}
            };

            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameter);

            List<MessageFirstTitleViewModel> list = new List<MessageFirstTitleViewModel>();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                MessageFirstTitleViewModel viewModel = new MessageFirstTitleViewModel();
                viewModel.Id = Convert.ToInt32(ds.Tables[0].Rows[i]["Id"]);
                viewModel.TitleName = ds.Tables[0].Rows[i]["TitleName"].ToString();
                viewModel.IsMaster = Convert.ToBoolean(ds.Tables[0].Rows[i]["IsMaster"]);
                list.Add(viewModel);
            }

            return list;
        }

        /// <summary>
        /// 获取标签列表
        /// </summary>
        /// <param name="CityID">城市ID</param>
        /// <returns></returns>
        public DataTable MessageFirstTitles(int CityID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a.Id,a.TitleName,b.cityID,b.cityName,a.TitleIndex,a.Enable,a.IsMaster,a.status,a.IsMerchant");
            strSql.Append(" from MessageFirstTitle a");
            strSql.Append(" inner join City b on a.CityID=b.cityID");
            strSql.Append(" where a.cityID=@cityID and a.status=1 order by TitleIndex");

            SqlParameter[] parameter = new SqlParameter[]{
                new SqlParameter("CityID",SqlDbType.Int){Value=CityID}
            };

            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameter);

            return ds.Tables[0];
        }

        /// <summary>
        /// 获取某条标签记录
        /// </summary>
        /// <param name="CityID">城市ID</param>
        /// <returns></returns>
        public DataTable MessageFirstTitleDetail(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from MessageFirstTitle ");
            strSql.Append(" where ID=@ID and status=1");

            SqlParameter[] parameter = new SqlParameter[]{
                new SqlParameter("ID",SqlDbType.Int){Value=ID}
            };

            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameter);

            return ds.Tables[0];
        }

        /// <summary>
        /// 是否已有主要标签存在
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="CityID"></param>
        /// <returns></returns>
        public long GetCountByCityID(int Id, int CityID)
        {
            string sql = @"select Count(*) from MessageFirstTitle where Id<>@Id and CityId=@CityId and Status=1 and Enable=1 and IsMaster=1";
           
            object retutnValue;

            SqlParameter[] sqlParameters = new SqlParameter[]{
                new SqlParameter("@Id",SqlDbType.Int){Value=Id},
                new SqlParameter("@CityId",SqlDbType.Int){Value=CityID}
            };

            retutnValue = SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sql, sqlParameters);
            long count;
            if (long.TryParse(retutnValue.ToString(), out count))
            {
                return count;
            }
            return 0;
        }

        /// <summary>
        /// 是否商户在该城市中已存在
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="CityID"></param>
        /// <returns></returns>
        public long GetCountByCityIDIsMerchant(int Id, int CityID)
        {
            string sql = @"select Count(*) from MessageFirstTitle where Id<>@Id and CityId=@CityId and Status=1 and Enable=1 and IsMerchant=1 ";
           
            object retutnValue;

            SqlParameter[] sqlParameters = new SqlParameter[]{
                new SqlParameter("@Id",SqlDbType.Int){Value=Id},
                new SqlParameter("@CityId",SqlDbType.Int){Value=CityID}
            };

            retutnValue = SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sql, sqlParameters);
            long count;
            if (long.TryParse(retutnValue.ToString(), out count))
            {
                return count;
            }
            return 0;
        }

        /// <summary>
        /// 添加标签记录
        /// </summary>
        /// <param name="model">对象实体</param>
        /// <returns></returns>
        public int InsertMessageFirstTitle(MessageFirstTitle model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("INSERT INTO MessageFirstTitle");
            strSql.Append("(CityID,TitleName,TitleIndex,Enable,Status,CreateTime,CreateBy,IsMaster,IsMerchant)");
            strSql.Append("VALUES(@CityID,@TitleName,@TitleIndex,@Enable,@Status,@CreateTime,@CreateBy,@IsMaster,@IsMerchant)");

            SqlParameter[] parameter = new SqlParameter[]{
                new SqlParameter("@CityID",SqlDbType.Int){Value=model.CityID},
                new SqlParameter("@TitleName",SqlDbType.NVarChar,200){Value=model.TitleName},
                new SqlParameter("@TitleIndex",SqlDbType.Int){Value=model.TitleIndex},
                new SqlParameter("@Enable",SqlDbType.Bit){Value=model.Enable},
                new SqlParameter("@Status",SqlDbType.Int){Value=model.Status},
                new SqlParameter("@CreateTime",SqlDbType.DateTime){Value=model.CreateTime},
                new SqlParameter("@CreateBy",SqlDbType.Int){Value=model.CreateBy},
                new SqlParameter("@IsMaster",SqlDbType.Bit){Value=model.IsMaster},
                new SqlParameter("@IsMerchant",SqlDbType.Bit){Value=model.IsMerchant}
            };
            object obj = Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameter));
            if (obj == null)
            {
                return 0;
            }
            return 1;
        }

        /// <summary>
        /// 更新标签信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateMessageFirstTitle(MessageFirstTitle model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("Update MessageFirstTitle Set");
            strSql.Append(" TitleName=@TitleName,TitleIndex=@TitleIndex,Enable=@Enable,IsMaster=@IsMaster,IsMerchant=@IsMerchant");
            strSql.Append(" where ID=@ID");

            SqlParameter[] parameter = new SqlParameter[]{
                new SqlParameter("@ID",SqlDbType.Int){Value=model.Id},
                new SqlParameter("@TitleName",SqlDbType.NVarChar,50){Value=model.TitleName},
                new SqlParameter("@TitleIndex",SqlDbType.Int){Value=model.TitleIndex},
                new SqlParameter("@Enable",SqlDbType.Bit){Value=model.Enable},
                new SqlParameter("@IsMaster",SqlDbType.Bit){Value=model.IsMaster},
                new SqlParameter("@IsMerchant",SqlDbType.Bit){Value=model.IsMerchant}
            };
            object obj = Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameter));
            if (obj == null)
            {
                return 0;
            }
            return 1;
        }

        /// <summary>
        /// 更新状态，删除或恢复
        /// </summary>
        /// <param name="ID">ID</param>
        /// <param name="Status">状态</param>
        /// <returns></returns>
        public int UpdateMessageFirstTitleStatus(int ID,bool Status)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("Update MessageFirstTitle Set Status=@Status");
            strSql.Append(" where ID=@ID");

            SqlParameter[] parameter = new SqlParameter[]{
                new SqlParameter("@ID",SqlDbType.Int){Value=ID},
                new SqlParameter("@Status",SqlDbType.Bit){Value=Status}
            };
            object obj = Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameter));
            if (obj == null)
            {
                return 0;
            }
            return 1;
        }

        /// <summary>
        /// 更新是否启用
        /// </summary>
        /// <param name="ID">ID</param>
        /// <param name="Enable">是否启用</param>
        /// <returns></returns>
        public int UpdateMessageFirstTitleEnable(int ID,bool Enable)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("Update MessageFirstTitle Set Enable=@Enable");
            strSql.Append(" where ID=@ID");

            SqlParameter[] parameter = new SqlParameter[]{
                new SqlParameter("@ID",SqlDbType.Int){Value=ID},
                new SqlParameter("@Enable",SqlDbType.Bit){Value=Enable}
            };
            object obj = Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameter));
            if (obj == null)
            {
                return 0;
            }
            return 1;
        }
    }
}
