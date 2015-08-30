using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.Model;
using System.Data;
using System.Data.SqlClient;
using VAGastronomistMobileApp.DBUtility;
namespace VAGastronomistMobileApp.SQLServerDAL
{
    public class CompanyEncourageManager
    {
        ///// <summary>
        ///// （wangcheng）公司新增加自定义奖励活动，无推送
        ///// </summary>
        ///// <param name="customEncourage"></param>
        ///// <returns></returns>
        //public long InsertCompanyEncourage(CompanyEncourage companyEncourage)
        //{
        //    using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
        //    {
        //        Object obj = null;
        //        try
        //        {
        //            conn.Open();

        //            StringBuilder strSql = new StringBuilder();
        //            SqlParameter[] parameters = null;
        //            strSql.Append("insert into CompanyEncourage(");
        //            strSql.Append("type,value,reason,description,");
        //            strSql.Append("createTime,creater,status,companyId)");
        //            strSql.Append(" values (");
        //            strSql.Append("@type,@value,@reason,@description,");
        //            strSql.Append("@createTime,@creater,@status,@companyId)");
        //            strSql.Append(" select @@identity");

        //            parameters = new SqlParameter[]{
        //                    new SqlParameter("@type", SqlDbType.Int,4),
        //                    new SqlParameter("@value",SqlDbType.NVarChar,50),
        //                    new SqlParameter("@reason",SqlDbType.NVarChar,50),
        //                    new SqlParameter("@description",SqlDbType.NVarChar,500),
        //                    new SqlParameter("@createTime", SqlDbType.DateTime),
        //                    new SqlParameter("@creater",SqlDbType.Int,4),
        //                    new SqlParameter("@status",SqlDbType.Int,4),
        //            new SqlParameter("@companyId",SqlDbType.Int,4)};

        //            parameters[0].Value = companyEncourage.type;
        //            parameters[1].Value = companyEncourage.value;
        //            parameters[2].Value = companyEncourage.reason;
        //            parameters[3].Value = companyEncourage.description;
        //            parameters[4].Value = companyEncourage.createTime;
        //            parameters[5].Value = companyEncourage.creater;
        //            parameters[6].Value = companyEncourage.status;
        //            parameters[7].Value = companyEncourage.companyId;

        //            obj = SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql.ToString(), parameters);

        //        }
        //        catch (Exception)
        //        {
        //            return 0;
        //        }
        //        if (obj == null)
        //        {
        //            return 0;
        //        }
        //        else
        //        {
        //            return Convert.ToInt64(obj);
        //        }
        //    }

        //}

        ///// <summary>
        ///// 根据公司集合查询该公司下所有消费过的客户id
        ///// </summary>
        ///// <param name="customEncourage"></param>
        ///// <returns></returns>
        //public DataTable SelectAllCustomerByCompanyId(string companyIDstr)
        //{
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append("SELECT Distinct(customerId) from PreOrder19dian as A");
        //    strSql.Append(" where A.companyId in (" + companyIDstr + ")");
        //    strSql.Append(" union ");
        //    strSql.Append("select Distinct(customerId) from CustomerConnCoupon as C");
        //    strSql.Append(" inner join dbo.CouponInfo as D on C.couponID = D.couponID");
        //    strSql.Append(" where D.companyId in (" + companyIDstr + ")");
        //    DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
        //    return ds.Tables[0];
        //}

        ///// <summary>
        ///// (wangcheng)根据自定义奖励活动编号companyEncourageId查询对应的信息
        ///// </summary>
        ///// <param name="customEncourageId"></param>
        ///// <returns></returns>
        //public DataTable SelectCompanyEncourageBycompanyEncourageId(long companyEncourageId)
        //{
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append("select [id],[type],[value],[reason],[description],[createTime],[creater],[status]");
        //    strSql.Append(" from CompanyEncourage");
        //    strSql.AppendFormat(" where status > '0' and [id] = {0}", companyEncourageId);

        //    DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());

        //    return ds.Tables[0];
        //}

        ///// <summary>
        ///// (wangcheng) 根据公司查询该公司上次创建活动的时间和当前的id
        ///// </summary>
        ///// <param name="customEncourage"></param>
        ///// <returns></returns>
        //public string SelectTimeLastEncourageByCompangId(int companyId)
        //{
        //    StringBuilder strSql = new StringBuilder();
        //    SqlParameter[] parameters ={
        //                              new SqlParameter("@companyId",companyId)                                  
        //                          };
        //    strSql.Append("SELECT top 1 createTime");
        //    strSql.Append(" FROM CompanyEncourage");
        //    strSql.Append(" where companyId=@companyId order by createTime desc");
        //    object obj = SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters);
        //    if (obj == null)
        //    {
        //        return "";
        //    }
        //    else
        //    {
        //        return obj.ToString();
        //    }
        //}

        //#region 优化版，批量插入CompanyEncourageConnCustomerCoupon表（wangcheng）
        ///// <summary>
        ///// (wangcheng) 插入公司活动和领用活动关系表
        ///// </summary>
        ///// <param name="customEncourage"></param>
        ///// <returns></returns>
        //public bool BulkInsertCompanyEncourageConnCustomerCoupon(List<long> customerConnCouponIdList, long companyEncourageId, int status)
        //{
        //    bool result = false;
        //    using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
        //    {
        //        if (conn.State != ConnectionState.Open)
        //            conn.Open();
        //        DateTime beginTime = DateTime.Now;
        //        DataTable dt = new DataTable();
        //        dt.Columns.Add("customerConnCouponId", typeof(long));
        //        dt.Columns.Add("companyEncourageId", typeof(long));
        //        dt.Columns.Add("status", typeof(int));
        //        foreach (var item in customerConnCouponIdList)
        //        {
        //            DataRow r = dt.NewRow();
        //            r["customerConnCouponId"] = item;
        //            r["companyEncourageId"] = companyEncourageId;
        //            r["status"] = status;
        //            dt.Rows.Add(r);
        //        }
        //        DataTable _dt = dt;
        //        SqlBulkCopy bulk = new SqlBulkCopy(conn, SqlBulkCopyOptions.UseInternalTransaction, null);
        //        bulk.BulkCopyTimeout = 30;
        //        bulk.DestinationTableName = "CompanyEncourageConnCustomerCoupon";//table名称
        //        bulk.ColumnMappings.Add("customerConnCouponId", "customerConnCouponId");
        //        bulk.ColumnMappings.Add("companyEncourageId", "companyEncourageId");
        //        bulk.ColumnMappings.Add("status", "status");
        //        try
        //        {
        //            bulk.WriteToServer(dt);
        //            result = true;
        //        }
        //        catch (Exception)
        //        {
        //            result = false;
        //        }
        //    }
        //    return result;
        //}
        //#endregion

        //#region 单条插入CompanyEncourageConnCustomerCoupon表
        ///// <summary>
        ///// (wangcheng) 插入公司活动和领用活动关系表
        ///// </summary>
        ///// <param name="customEncourage"></param>
        ///// <returns></returns>
        //public long InsertCompanyEncourageConnCoupon(long customerConnCouponID, long companyEncourageId, int companyEncourageStatus)
        //{
        //    using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
        //    {
        //        Object obj = null;
        //        try
        //        {
        //            conn.Open();
        //            StringBuilder strSql = new StringBuilder();
        //            SqlParameter[] parameters ={
        //                              new SqlParameter("@customerConnCouponId",customerConnCouponID),
        //                               new SqlParameter("@companyEncourageId",companyEncourageId)  ,
        //                                new SqlParameter("@status",companyEncourageStatus)   
        //                          };
        //            strSql.Append("insert into CompanyEncourageConnCustomerCoupon(");
        //            strSql.Append(" customerConnCouponId,companyEncourageId,status)");
        //            strSql.Append("  values (");
        //            strSql.Append(" @customerConnCouponId,@companyEncourageId,@status)");
        //            strSql.Append(" select @@identity");
        //            obj = SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql.ToString(), parameters);

        //        }
        //        catch (Exception)
        //        {
        //            return 0;
        //        }
        //        if (obj == null)
        //        {
        //            return 0;
        //        }
        //        else
        //        {
        //            return Convert.ToInt64(obj);
        //        }
        //    }
        //}
        //#endregion

        ///// <summary>
        ///// (wangcheng) 修改下载活动优惠券后的客户和优惠券之间关系表的信息
        ///// </summary>
        ///// <param name="customEncourage"></param>
        ///// <returns></returns>
        //public bool UpdateCouponDownloadInfo(string verificationCode, DateTime downVerifyRewardTime, int newstatus, long customerConnCouponID)
        //{
        //    int result = 0;
        //    StringBuilder strSql = new StringBuilder();
        //    SqlParameter[] parameters ={
        //                              new SqlParameter("@verificationCode",verificationCode),
        //                              new SqlParameter("@downloadTime",downVerifyRewardTime),
        //                              new SqlParameter("@status",newstatus) ,
        //                              new SqlParameter("@customerConnCouponID",customerConnCouponID)
        //                          };
        //    strSql.Append("UPDATE CustomerConnCoupon SET verificationCode =@verificationCode,downloadTime=@downloadTime,status=@status");
        //    strSql.Append(" where customerConnCouponID=@customerConnCouponID");
        //    result = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters);
        //    if (result > 0)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}


        ///// <summary>
        ///// (wangcheng) 根据状态判断当前用户是否下载过此优惠券
        ///// </summary>
        ///// <param name="customEncourage"></param>
        ///// <returns></returns>
        //public bool UpdateCompanyEncourageConnCustomerCouponStattus(long customerConnCouponID, int companyEncourageStatus)
        //{
        //    int result = 0;
        //    StringBuilder strSql = new StringBuilder();
        //    SqlParameter[] parameters ={
        //                              new SqlParameter("@customerConnCouponId",customerConnCouponID),
        //                              new SqlParameter("@status",companyEncourageStatus)
        //                          };
        //    strSql.Append("UPDATE CompanyEncourageConnCustomerCoupon SET status=@status");
        //    strSql.Append(" where customerConnCouponId=@customerConnCouponId");
        //    result = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters);
        //    if (result > 0)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        ///// <summary>
        ///// (wangcheng) 根据customerConnCouponID查询对应的couponId和当前的状态
        ///// </summary>
        ///// <param name="customEncourage"></param>
        ///// <returns></returns>
        //public DataTable SelectCouponIdAndStatusByCustomerConnCouponID(long customerConnCouponID)
        //{
        //    StringBuilder strSql = new StringBuilder();
        //    SqlParameter[] parameters ={
        //                              new SqlParameter("@customerConnCouponID",customerConnCouponID)
        //                          };
        //    strSql.Append("SELECT couponID,status");
        //    strSql.Append(" FROM CustomerConnCoupon");
        //    strSql.AppendFormat(" where customerConnCouponID=@customerConnCouponID");
        //    DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters);
        //    if (ds == null)
        //    {
        //        return null;
        //    }
        //    else
        //    {
        //        return ds.Tables[0];
        //    }
        //}

        ///// <summary>
        ///// (wangcheng) 根据customerID查询出对应未查看的活动描述信息description
        ///// 只查出来了前20条，按创建时间降序排列
        ///// </summary>
        ///// <param name="customEncourage"></param>
        ///// <returns></returns>
        //public DataTable SelectCompanyEncourageDescripitionByCustomerID(long customerID)
        //{
        //    StringBuilder strSql = new StringBuilder();
        //    //获得查询结果
        //    SqlParameter[] parameters ={
        //                              new SqlParameter("@customerID",customerID),
        //                              new SqlParameter("@status",(int)VACustomerCouponStatus.NOT_DOWNLOAD)//表示已查看状态
        //                          };
        //    strSql.Append(" select top 20 A.[description],A.createTime,A.id from CompanyEncourage A");
        //    strSql.Append(" inner join CompanyEncourageConnCustomerCoupon B on A.id=B.companyEncourageId");
        //    strSql.Append(" inner join CustomerConnCoupon C on B.customerConnCouponId=C.customerConnCouponID");
        //    strSql.Append(" where C.customerID=@customerID and C.status =@status order by createTime desc");
        //    //紧接着修改状态

        //    DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters);
        //    if (ds == null)
        //    {
        //        return null;
        //    }
        //    else
        //    {
        //        return ds.Tables[0];
        //    }
        //}
        ///// <summary>
        ///// (wangcheng) 修改当前customerID所有的活动优惠券的状态，由未查看（未下载）改成已查看
        ///// </summary>
        ///// <param name="customEncourage"></param>
        ///// <returns></returns>
        //public bool UpdateCompanyEncourageCouponStatusByCustomerID(long customerID)
        //{
        //    int result = 0;
        //    StringBuilder strSql = new StringBuilder();
        //    SqlParameter[] parameters ={
        //                              new SqlParameter("@customerID",customerID),
        //                              new SqlParameter("@status",(int)VACustomerCouponStatus.CHECKED),//表示已查看状态
        //                              new SqlParameter("@oldstatus",(int)VACustomerCouponStatus.NOT_DOWNLOAD)//表示已查看状态
        //                          };
        //    strSql.Append("UPDATE CustomerConnCoupon SET status=@status");
        //    strSql.Append(" where customerID=@customerID and status=@oldstatus");//过滤出来那些状态为未查看的行
        //    result = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters);
        //    if (result > 0)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        ///// <summary>
        ///// (wangcheng) 修改当前customerID对应的CompanyEncourageConnCustomerCoupon中的状态，由已发送改成已查看
        ///// </summary>
        ///// <param name="customEncourage"></param>
        ///// <returns></returns>
        //public bool UpdateCompanyEncourageConnCustomerCouponStatus(long customerConnCouponID)
        //{
        //    int result = 0;
        //    StringBuilder strSql = new StringBuilder();
        //    SqlParameter[] parameters ={
        //                              new SqlParameter("@customerConnCouponID",customerConnCouponID),
        //                              new SqlParameter("@status",(int)VACompanyEncourageConnCouponStatus.CHECKED),//表示已查看状态
        //                              new SqlParameter("@oldstatus",(int)VACompanyEncourageConnCouponStatus.SEND)//表示已发送状态
        //                          };
        //    strSql.Append("UPDATE CompanyEncourageConnCustomerCoupon SET status=@status");
        //    strSql.Append(" where customerConnCouponID=@customerConnCouponID and status=@oldstatus");//过滤出来那些状态为未查看的行
        //    result = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters);
        //    if (result > 0)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        ///// <summary>
        ///// (wangcheng) 根据customerID查询出对应所有的customerConnCouponID
        ///// </summary>
        ///// <param name="customEncourage"></param>
        ///// <returns></returns>
        //public DataTable SelectCustomerConnCouponIDByCustomerID(long customerID)
        //{
        //    StringBuilder strSql = new StringBuilder();
        //    SqlParameter[] parameters ={
        //                              new SqlParameter("@customerID",customerID),
        //                              new SqlParameter("@status",(int)VACustomerCouponStatus.CHECKED)
        //                          };
        //    strSql.Append("SELECT customerConnCouponID");
        //    strSql.Append(" FROM CustomerConnCoupon");
        //    strSql.Append(" where customerID=@customerID and status=@status");
        //    DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters);
        //    if (ds == null)
        //    {
        //        return null;
        //    }
        //    else
        //    {
        //        return ds.Tables[0];
        //    }
        //}
    }
}

