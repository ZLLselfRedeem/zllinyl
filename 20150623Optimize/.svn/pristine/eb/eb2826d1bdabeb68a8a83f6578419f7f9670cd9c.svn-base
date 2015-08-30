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
    public class ActivityMessageManager
    {
       /// <summary>
        /// 获取活动消息明细
       /// </summary>
       /// <param name="ID"></param>
       /// <returns></returns>
        public DataTable ActivityMessageDetail(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a.ID,a.Name,a.CityID,b.cityName,c.TitleName,a.MsgType,c.Id MessageFirstTitleID,");
            strSql.Append(" a.AdvertisementURL,d.shopName,a.CouponID,a.ActivityID,");
            strSql.Append(" a.ActivityLogo,a.AdvertisementAddress,ActivityExplain");
            strSql.Append(" from activityMessage a");
            strSql.Append(" inner join city b on a.CityID=b.cityID");
            strSql.Append(" inner join MessageFirstTitle c on a.MessageFirstTitleID=c.Id");
            strSql.Append(" left join ShopInfo d on a.ShopID=d.shopID");
            strSql.Append(" where a.ID=@ID");

            SqlParameter[] parameter = new SqlParameter[]{
                new SqlParameter("ID",SqlDbType.Int){Value=ID}
            };

            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameter);
            return ds.Tables[0];
        }

       /// <summary>
        /// 获取活动消息列表
       /// </summary>
       /// <param name="model">活动消息实体</param>
       /// <param name="BeginDate">开始时间</param>
       /// <param name="EndDate">结束时间</param>
       /// <returns></returns>
        public DataTable ActivityMessages(ActivityMessage model, DateTime BeginDate, DateTime EndDate)
        {
            StringBuilder strSql = new StringBuilder();
            List<SqlParameter> para = new List<SqlParameter>();
            strSql.Append("select a.ID,a.Name,b.cityName,c.TitleName,a.CreateDate,a.MsgType from activityMessage a");
            strSql.Append(" left join City b on b.cityID=a.CityID");
            strSql.Append(" left join MessageFirstTitle c on a.MessageFirstTitleID=c.Id");
            strSql.Append(" where CreateDate between @BeginDate and @EndDate");
            para.Add(new SqlParameter("@BeginDate", SqlDbType.DateTime) { Value = BeginDate });
            para.Add(new SqlParameter("@EndDate", SqlDbType.DateTime) { Value = EndDate });
            if (!model.Name.Trim().Equals(string.Empty))
            {
                strSql.Append(" and Name like @Name ");
                para.Add(new SqlParameter("@Name", SqlDbType.NVarChar) { Value = "%" + model.Name + "%" });
            }
            if(model.MsgType!=0)
            {
                strSql.Append(" and MsgType=@MsgType");
                para.Add(new SqlParameter("@MsgType", SqlDbType.Int) { Value =  model.MsgType });
            }
            if(model.CityID!=0)
            {
                strSql.Append(" and a.CityID=@CityID ");
                para.Add(new SqlParameter("@CityID", SqlDbType.Int) { Value = model.CityID });
            }

            if (model.MessageFirstTitleID != 0)
            {
                strSql.Append(" and MessageFirstTitleID=@MessageFirstTitleID ");
                para.Add(new SqlParameter("@MessageFirstTitleID", SqlDbType.Int) { Value = model.MessageFirstTitleID });
            }
            strSql.Append(" order by a.ID desc ");

            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), para.ToArray());
            return ds.Tables[0];
        }
        
        /// <summary>
        /// 插入一条活动消息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Insert(ActivityMessage model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("INSERT INTO ActivityMessage");
            strSql.Append("(Name,CityID,MessageFirstTitleID,MsgType");
            strSql.Append(",ActivityLogo,ActivityExplain,ShopID,AdvertisementAddress");
            strSql.Append(",AdvertisementURL,ActivityID,CouponID,");
            strSql.Append("CreateUser,CreateDate,UpdateUser,UpdateDate)");
            strSql.Append("VALUES(@Name,@CityID,@MessageFirstTitleID,@MsgType");
            strSql.Append(",@ActivityLogo,@ActivityExplain,@ShopID,@AdvertisementAddress");
            strSql.Append(",@AdvertisementURL,@ActivityID,@CouponID");
            strSql.Append(",@CreateUser,@CreateDate,@UpdateUser,@UpdateDate)");

            SqlParameter[] parameter = new SqlParameter[]{
                new SqlParameter("@Name",SqlDbType.NVarChar,200){Value=model.Name},
                new SqlParameter("@CityID",SqlDbType.Int){Value=model.CityID},
                new SqlParameter("@MessageFirstTitleID",SqlDbType.Int){Value=model.MessageFirstTitleID},
                new SqlParameter("@MsgType",SqlDbType.Int){Value=model.MsgType},
                new SqlParameter("@ActivityLogo",SqlDbType.NVarChar,200){Value=model.ActivityLogo},
                new SqlParameter("@ActivityExplain",SqlDbType.NVarChar,800){Value=model.ActivityExplain},
                new SqlParameter("@ShopID",SqlDbType.Int){Value=model.ShopID},
                new SqlParameter("@AdvertisementAddress",SqlDbType.NVarChar,200){Value=model.AdvertisementAddress},
                new SqlParameter("@AdvertisementURL",SqlDbType.NVarChar,200){Value=model.AdvertisementURL},
                new SqlParameter("@ActivityID",SqlDbType.Int){Value=model.ActivityID},
                new SqlParameter("@CouponID",SqlDbType.Int){Value=model.CouponID},
                new SqlParameter("@CreateUser",SqlDbType.Int){Value=model.CreateUser},
                new SqlParameter("@CreateDate",SqlDbType.DateTime){Value=model.CreateDate},
                new SqlParameter("@UpdateUser",SqlDbType.Int){Value=model.UpdateUser},
                new SqlParameter("@UpdateDate",SqlDbType.DateTime){Value=model.UpdateDate}
            };
            object obj = Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameter));
            if (obj == null)
            {
                return 0;
            }
            return 1;
        }

        /// <summary>
        /// 修改一条活动消息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Update(ActivityMessage model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from activityMessage");
            strSql.Append(" where ID=@ID");

            SqlParameter[] parameter = new SqlParameter[]{
                new SqlParameter("ID",SqlDbType.Int){Value=model.ID}
            };
            string strOld = string.Empty;
            string strNew = string.Empty;
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameter);
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    strOld += dt.Columns[i].ColumnName + "=" + dt.Rows[0][i].ToString() + ",";
                } 
            }
            else
            {
                return 0;
            }

            StringBuilder strSqlUpdate = new StringBuilder();
            strSqlUpdate.Append("Update ActivityMessage Set ");
            strSqlUpdate.Append(" Name=@Name,CityID=@CityID,MessageFirstTitleID=@MessageFirstTitleID,MsgType=@MsgType");
            strSqlUpdate.Append(",ActivityLogo=@ActivityLogo,ActivityExplain=@ActivityExplain");
            strSqlUpdate.Append(",ShopID=@ShopID,AdvertisementAddress=@AdvertisementAddress");
            strSqlUpdate.Append(",AdvertisementURL=@AdvertisementURL,ActivityID=@ActivityID,CouponID=@CouponID");
            strSqlUpdate.Append(",UpdateUser=@UpdateUser,UpdateDate=@UpdateDate");
            strSqlUpdate.Append(" where ID=@ID");

            parameter = new SqlParameter[]{
                new SqlParameter("@Name",SqlDbType.NVarChar,200){Value=model.Name},
                new SqlParameter("@CityID",SqlDbType.Int){Value=model.CityID},
                new SqlParameter("@MessageFirstTitleID",SqlDbType.Int){Value=model.MessageFirstTitleID},
                new SqlParameter("@MsgType",SqlDbType.Int){Value=model.MsgType},
                new SqlParameter("@ActivityLogo",SqlDbType.NVarChar,200){Value=model.ActivityLogo},
                new SqlParameter("@ActivityExplain",SqlDbType.NVarChar,800){Value=model.ActivityExplain},
                new SqlParameter("@ShopID",SqlDbType.Int){Value=model.ShopID},
                new SqlParameter("@AdvertisementAddress",SqlDbType.NVarChar,200){Value=model.AdvertisementAddress},
                new SqlParameter("@AdvertisementURL",SqlDbType.NVarChar,200){Value=model.AdvertisementURL},
                new SqlParameter("@ActivityID",SqlDbType.Int){Value=model.ActivityID},
                new SqlParameter("@CouponID",SqlDbType.Int){Value=model.CouponID},
                new SqlParameter("@UpdateUser",SqlDbType.Int){Value=model.UpdateUser},
                new SqlParameter("@UpdateDate",SqlDbType.DateTime){Value=model.UpdateDate},
                 new SqlParameter("@ID",SqlDbType.Int){Value=model.ID}
            };
            object obj = Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSqlUpdate.ToString(), parameter));
            if (obj == null)
            {
                return 0;
            }

            parameter = new SqlParameter[]{
                new SqlParameter("ID",SqlDbType.Int){Value=model.ID}
            };
            ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameter);
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    strNew += dt.Columns[i].ColumnName + "=" + dt.Rows[0][i].ToString() + ",";
                }
            }

            strSql.Length = 0;
            strSql.Append(" Insert into ActivityMessageLog ");
            strSql.Append(" (OperateUser,operateDate,OldData,NewData)");
            strSql.Append(" Values(@OperateUser,@operateDate,@OldData,@NewData)");
            parameter = new SqlParameter[]{
                new SqlParameter("@OperateUser",SqlDbType.Int){Value=model.UpdateUser},
                new SqlParameter("@operateDate",SqlDbType.DateTime){Value=model.UpdateDate},
                new SqlParameter("@OldData",SqlDbType.Text){Value=strOld},
                new SqlParameter("@NewData",SqlDbType.Text){Value=strNew}
             };
            SqlHelper.ExecuteScalar(SqlHelper.MobileAppLogConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameter);

            return 1;
        }
    }
}
