using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.DBUtility;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL.Persistence.Infrastructure;

namespace VAGastronomistMobileApp.SQLServerDAL
{
    public class PackageManager
    {
        /// <summary>
        /// 获取套餐模板列表
        /// </summary>
        /// <param name="cityId">适用城市</param>
        /// <param name="Status">是否启用</param>
        /// <returns></returns>
        public DataTable Packages(int cityId, int Status)
        {
            StringBuilder strSql = new StringBuilder();
            List<SqlParameter> para = new List<SqlParameter>();
            strSql.Append("select a.*,b.cityName from Package a ");
            strSql.Append(" left join City b on ApplicableCity=cityID where 1=1 ");
            if (cityId != 0)
            {
                strSql.Append(" and ApplicableCity=@ApplicableCity ");
                para.Add(new SqlParameter("@ApplicableCity", SqlDbType.Int) { Value = cityId });
            }
            if (Status != -1)
            {
                strSql.Append(" and a.Status=@Status");
                para.Add(new SqlParameter("@Status", SqlDbType.Int) { Value = Status });
            }
            strSql.Append(" order by CreateDate desc");
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), para.ToArray());
            return ds.Tables[0];
        }

        /// <summary>
        /// 获取单条记录明细
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public DataTable PackageDetail(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            List<SqlParameter> para = new List<SqlParameter>();
            strSql.Append("select * from Package ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameter = new SqlParameter[]
            {
                new SqlParameter("@ID",SqlDbType.BigInt){Value=ID}
            };
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameter);
            return ds.Tables[0];
        }

        public int Insert(Package model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("INSERT INTO Package");
            strSql.Append("(Name,Description,PositionType,Distance,TimeRange,ISGuestUnitPrice,MinGuestUnitPrice");
            strSql.Append(",MaxGuestUnitPrice,ValuationType,Cost,LevelRequirements,EnableFilter,SendLnterval,Status");
            strSql.Append(",ApplicableCity,CreateUser,CreateDate,UpdateUser,UpdateDate)");
            strSql.Append("VALUES(@Name,@Description,@PositionType,@Distance,@TimeRange,@ISGuestUnitPrice,@MinGuestUnitPrice");
            strSql.Append(",@MaxGuestUnitPrice,@ValuationType,@Cost,@LevelRequirements,@EnableFilter,@SendLnterval,@Status");
            strSql.Append(",@ApplicableCity,@CreateUser,@CreateDate,@UpdateUser,@UpdateDate)");

            SqlParameter[] parameter = new SqlParameter[]{
                new SqlParameter("@Name",SqlDbType.VarChar,100){Value=model.Name},
                new SqlParameter("@Description",SqlDbType.NVarChar,200){Value=model.Description},
                new SqlParameter("@PositionType",SqlDbType.Int){Value=model.PositionType},
                new SqlParameter("@Distance",SqlDbType.Decimal){Value=model.Distance},
                new SqlParameter("@TimeRange",SqlDbType.Int){Value=model.TimeRange},
                new SqlParameter("@ISGuestUnitPrice",SqlDbType.Bit){Value=model.ISGuestUnitPrice},
                new SqlParameter("@MinGuestUnitPrice",SqlDbType.Decimal){Value=model.MinGuestUnitPrice},
                new SqlParameter("@MaxGuestUnitPrice",SqlDbType.Decimal){Value=model.MaxGuestUnitPrice},
                new SqlParameter("@ValuationType",SqlDbType.Int){Value=model.ValuationType},
                new SqlParameter("@Cost",SqlDbType.Decimal){Value=model.Cost},
                new SqlParameter("@LevelRequirements",SqlDbType.Int){Value=model.LevelRequirements},
                new SqlParameter("@EnableFilter",SqlDbType.Bit){Value=model.EnableFilter},
                new SqlParameter("@SendLnterval",SqlDbType.Decimal){Value=model.SendLnterval},
                new SqlParameter("@Status",SqlDbType.Bit){Value=model.Status},
                new SqlParameter("@ApplicableCity",SqlDbType.Int){Value=model.ApplicableCity},
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

        public int Update(Package model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from Package");
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

            strSql.Length = 0;
            strSql.Append("Update Package set ");
            strSql.Append("Name=@Name,Description=@Description,PositionType=@PositionType,Distance=@Distance,TimeRange=@TimeRange");
            strSql.Append(",ISGuestUnitPrice=@ISGuestUnitPrice,MinGuestUnitPrice=@MinGuestUnitPrice");
            strSql.Append(",MaxGuestUnitPrice=@MaxGuestUnitPrice,ValuationType=@ValuationType,Cost=@Cost");
            strSql.Append(",LevelRequirements=@LevelRequirements,EnableFilter=@EnableFilter,SendLnterval=@SendLnterval,Status=@Status");
            strSql.Append(",ApplicableCity=@ApplicableCity,UpdateUser=@UpdateUser,UpdateDate=@UpdateDate");
            strSql.Append(" where ID=@ID");

            parameter = new SqlParameter[]{
                new SqlParameter("@Name",SqlDbType.VarChar,100){Value=model.Name},
                new SqlParameter("@Description",SqlDbType.NVarChar,200){Value=model.Description},
                new SqlParameter("@PositionType",SqlDbType.Int){Value=model.PositionType},
                new SqlParameter("@Distance",SqlDbType.Decimal){Value=model.Distance},
                new SqlParameter("@TimeRange",SqlDbType.Int){Value=model.TimeRange},
                new SqlParameter("@ISGuestUnitPrice",SqlDbType.Bit){Value=model.ISGuestUnitPrice},
                new SqlParameter("@MinGuestUnitPrice",SqlDbType.Decimal){Value=model.MinGuestUnitPrice},
                new SqlParameter("@MaxGuestUnitPrice",SqlDbType.Decimal){Value=model.MaxGuestUnitPrice},
                new SqlParameter("@ValuationType",SqlDbType.Int){Value=model.ValuationType},
                new SqlParameter("@Cost",SqlDbType.Decimal){Value=model.Cost},
                new SqlParameter("@LevelRequirements",SqlDbType.Int){Value=model.LevelRequirements},
                new SqlParameter("@EnableFilter",SqlDbType.Bit){Value=model.EnableFilter},
                new SqlParameter("@SendLnterval",SqlDbType.Decimal){Value=model.SendLnterval},
                new SqlParameter("@Status",SqlDbType.Bit){Value=model.Status},
                new SqlParameter("@ApplicableCity",SqlDbType.Int){Value=model.ApplicableCity},
                new SqlParameter("@UpdateUser",SqlDbType.Int){Value=model.UpdateUser},
                new SqlParameter("@UpdateDate",SqlDbType.DateTime){Value=model.UpdateDate},
                new SqlParameter("@ID",SqlDbType.BigInt){Value=model.ID}
            };
            object obj = Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameter));
            if (obj == null)
            {
                return 0;
            }

            strSql.Length = 0;
            strSql.Append("select * from Package");
            strSql.Append(" where ID=@ID");

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
            strSql.Append(" Insert into PackageLog ");
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

        /// <summary>
        /// 改变套餐模板状态
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="Status"></param>
        /// <returns></returns>
        public int ChangeStatus(int ID,int Status)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("Update Package set Status=@Status where ID=@ID");
            SqlParameter[] parameter = new SqlParameter[]
            {
                new SqlParameter("Status",SqlDbType.Bit){Value=Status},
                new SqlParameter("ID",SqlDbType.BigInt){Value=ID}
            };
            object obj = Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameter));
            if (obj == null)
            {
                return 0;
            }
            return 1;
        }

     /// <summary>
     ///会员营销统计
     /// </summary>
     /// <param name="ShopName">门店关键字</param>
     /// <param name="BeginDate">开始时间</param>
     /// <param name="EndDate">结束时间</param>
     /// <param name="cityId">城市ID</param>
     /// <param name="PackageID">模板ID</param>
     /// <returns></returns>
        public DataTable PackageStatistics(string ShopName, DateTime BeginDate, DateTime EndDate, int cityId, int PackageID)
        {
            StringBuilder strSql = new StringBuilder();
            List<SqlParameter> para = new List<SqlParameter>();
            strSql.Append("select a.CouponId,b.shopName,a.CouponName,");
            strSql.Append(" case when c.ID=null then d.CreateDate else a.CreateTime end SendTime,");
            strSql.Append(" isnull(d.Name,a.CouponName) ActivityName,e.Name,c.Amount");
            strSql.Append(" from Coupon a");
            strSql.Append(" inner join shopInfo b on a.ShopId=b.shopID");
            strSql.Append(" left join PurchasePackages c on c.CouponId=a.CouponId");
            strSql.Append(" left join ActivityMessage d on a.CouponId=d.CouponID");
            strSql.Append(" left join Package e on c.PackageID=e.ID");
            strSql.Append(" where a.shopName like @shopName and (c.ID is not null or d.ID is not null)");
            strSql.Append(" and a.CreateTime between @BeginDate and @EndDate");

            para.Add(new SqlParameter("@shopName", SqlDbType.NVarChar) { Value = "%" + ShopName + "%" });
            para.Add(new SqlParameter("@BeginDate", SqlDbType.DateTime) { Value = BeginDate });
            para.Add(new SqlParameter("@EndDate", SqlDbType.DateTime) { Value = EndDate });

            if (cityId != 0)
            {
                strSql.Append(" and a.CityId=@CityId");
                para.Add(new SqlParameter("@CityId", SqlDbType.Int) { Value = cityId });
            }
            if (PackageID > 0)
            {
                strSql.Append(" and c.PackageID=@PackageID");
                para.Add(new SqlParameter("@PackageID", SqlDbType.Int) { Value = PackageID });
            }
            else if (PackageID == -1)//套餐取平台中配置的套餐，加一项“由平台发送”
            {
                strSql.Append(" and c.PackageID is null");
            }
            strSql.Append(" order by CouponId desc");
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), para.ToArray());
            return ds.Tables[0];
        }

         /// <summary>
        /// 获取券信息
        /// </summary>
        /// <param name="CouponId">券ID</param>
        /// <returns></returns>
        public DataTable PackageStatisticsView(int CouponId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select b.shopName,Case when c.ID=null then d.CreateDate else a.CreateTime end SendTime,");
            strSql.Append(" d.Name,a.RequirementMoney,a.DeductibleAmount,a.MaxAmount,c.Amount,");
            strSql.Append(" c.SendUsers,c.ClickCount,a.CouponName,a.SheetNumber,d.ValuationType,d.Cost");
            strSql.Append(" from coupon a");
            strSql.Append(" inner join ShopInfo b on a.ShopId=b.shopID");
            strSql.Append(" left join PurchasePackages c on a.CouponId=c.CouponId");
            strSql.Append(" left join Package d on c.PackageID=d.ID");
            strSql.Append(" left join ActivityMessage e on a.CouponId=e.CouponID");
            strSql.Append(" where a.CouponId=@CouponId");
            SqlParameter[] parameter = new SqlParameter[]
            {
                new SqlParameter("CouponId",SqlDbType.Int){Value=CouponId}
            };
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameter);
            return ds.Tables[0];
        }
    }
}
