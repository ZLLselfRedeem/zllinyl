using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using VAGastronomistMobileApp.DBUtility;
using VAGastronomistMobileApp.Model;
using System.Data.SqlClient;

namespace VAGastronomistMobileApp.SQLServerDAL
{
    /// <summary>
    /// 批量打款数据访问层
    /// created by wangc
    /// </summary>
    public class BatchMoneyManager
    {
        /// <summary>
        /// 查询符合生成打款申请条件的商家信息
        /// </summary>
        /// <param name="minAmount"></param>
        /// <param name="cityId"></param>
        /// <returns></returns>
        public DataTable SelectBatchMoneyMerchantApply(double minAmount, int cityId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select A.shopName,A.shopID,B.companyName,C.accountNum,C.bankName,C.accountName,A.remainMoney,A.companyID,A.cityId");
            strSql.Append(" from ShopInfo A inner join CompanyInfo B on B.companyID=A.companyID");
            strSql.Append(" inner join CompanyAccounts C on C.identity_Id=A.bankAccount");
            strSql.AppendFormat(" where A.remainMoney>{0}", minAmount);
            if (cityId > 0)
            {
                strSql.AppendFormat(" and A.cityId={0}", cityId);
            }
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }

        /// <summary>
        /// 查询符合生成打款申请条件的商家信息(新)
        /// </summary>
        /// <param name="minAmount"></param>
        /// <param name="cityId"></param>
        /// <returns></returns>
        public DataTable SelectBatchMoneyMerchantApplyNew(double minAmount, int cityId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select A.shopName,A.shopID,B.companyName,C.accountNum,C.bankName,C.accountName,C.payeeBankName,A.remainMoney,A.companyID,A.cityId,");
            strSql.Append(" remainRedEnvelopeAmount,remainFoodCouponAmount,remainAlipayAmount,remainWechatPayAmount,remainCommissionAmount,amountFrozen");
            strSql.Append(" from ShopInfo A inner join CompanyInfo B on B.companyID=A.companyID");
            strSql.Append(" left join CompanyAccounts C on C.identity_Id=A.bankAccount and C.status=1");
            strSql.AppendFormat(" where Cast(A.remainMoney-isnull(amountFrozen,0) as numeric(18,2))>{0} ", minAmount);
            strSql.Append(" and a.shopID not in(select d.shopId from BatchMoneyApplyDetail d where d.status=5)");
            if (cityId > 0)
            {
                strSql.AppendFormat(" and A.cityId={0}", cityId);
            }
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }

        /// <summary>
        /// 查询批量打款申请
        /// </summary>
        /// <param name="strTime"></param>
        /// <param name="endTime"></param>
        /// <param name="cityId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public DataTable SelectBatchMoneyApply(string strTime, string endTime, int cityId, string status)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT batchMoneyApplyId,createdTime,advanceCount,practicalCount,advanceAmount,practicalAmount,operateEmployee,status,remark");
            strSql.Append("  FROM BatchMoneyApply ");
            strSql.AppendFormat(" where status=1 and advanceCount>0 and createdTime between '{0}' and '{1}'", strTime, endTime);
            if (cityId > 0)
            {
                strSql.AppendFormat(" and cityId={0}", cityId);
            }
            switch (status)
            {
                case "yes":
                    strSql.Append(" and advanceCount=practicalCount");
                    break;
                case "not":
                    strSql.Append(" and advanceCount>isnull(practicalCount,0)");
                    break;
                case "all":
                default:
                    break;
            }
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }
        /// <summary>
        /// 根据新建批量打款申请ID查询申请明细
        /// </summary>
        /// <param name="batchMoneyApplyId"></param>
        /// <param name="doExcel"></param>
        /// <returns></returns>
        public DataTable SelectBatchMoneyApplyDetailByBatchMoneyApplyId(int batchMoneyApplyId, bool doExcel)
        {
            StringBuilder strSql = new StringBuilder();
            if (doExcel)
            {
                strSql.Append("select C.companyName '公司名',B.shopName '门店名',");
                strSql.Append(" bankName '开户银行',accountName '开户名',accountNum '帐号',applyAmount '申请打款金额',isnull(A.haveAdjustAmount,0) '余额调整',serialNumberOrRemark '流水号或备注'");
                strSql.Append(" from BatchMoneyApplyDetail A");
                strSql.Append(" inner join ShopInfo B on A.shopId=B.shopID");
                strSql.Append(" inner join CompanyInfo C on C.companyID=A.companyId");
                strSql.Append(" where A.batchMoneyApplyId=@batchMoneyApplyId ");
                strSql.Append(" and A.status>=1");
            }
            else
            {
                strSql.Append("select batchMoneyApplyDetailId,batchMoneyApplyId,A.companyId,A.shopId,isnull(A.haveAdjustAmount,0) haveAdjustAmount,");
                strSql.Append(" accountNum,bankName,accountName,applyAmount,serialNumberOrRemark,status,B.shopName,C.companyName,isnull(A.accountId,0) accountId");
                strSql.Append(" from BatchMoneyApplyDetail A");
                strSql.Append(" inner join ShopInfo B on A.shopId=B.shopID");
                strSql.Append(" inner join CompanyInfo C on C.companyID=A.companyId");
                strSql.Append(" where A.batchMoneyApplyId=@batchMoneyApplyId ");
                strSql.Append(" and A.status>=1");

            }
            SqlParameter[] parameters = {
					new SqlParameter("@batchMoneyApplyId", SqlDbType.Int,4)
			};
            parameters[0].Value = batchMoneyApplyId;
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters);
            return ds.Tables[0];
        }
        /// <summary>
        /// 根据流水号查询申请明细
        /// </summary>
        /// <param name="serialNumberOrRemark"></param>
        /// <param name="doExcel"></param>
        /// <returns></returns>
        public DataTable SelectBatchMoneyApplyDetailBySerialNumberOrRemark(string serialNumberOrRemark, bool doExcel)
        {
            StringBuilder strSql = new StringBuilder();
            if (doExcel)
            {
                strSql.Append("select C.companyName '公司名',B.shopName '门店名',");
                strSql.Append(" bankName '开户银行',accountName '开户名',accountNum '帐号',applyAmount '申请打款金额',isnull(A.haveAdjustAmount,0) '余额调整',serialNumberOrRemark '流水号或备注'");
                strSql.Append(" from BatchMoneyApplyDetail A");
                strSql.Append(" inner join ShopInfo B on A.shopId=B.shopID");
                strSql.Append(" inner join CompanyInfo C on C.companyID=A.companyId");
                strSql.AppendFormat(" where A.status>0 and A.serialNumberOrRemark like  '%{0}%'", serialNumberOrRemark);
            }
            else
            {
                strSql.Append("select batchMoneyApplyDetailId,batchMoneyApplyId,A.companyId,A.shopId,A.haveAdjustAmount,isnull(A.accountId,0) accountId,");
                strSql.Append(" accountNum,bankName,accountName,applyAmount,serialNumberOrRemark,status,B.shopName,C.companyName");
                strSql.Append(" from BatchMoneyApplyDetail A");
                strSql.Append(" inner join ShopInfo B on A.shopId=B.shopID");
                strSql.Append(" inner join CompanyInfo C on C.companyID=A.companyId");
                strSql.AppendFormat(" where A.status>0 and A.serialNumberOrRemark like  '%{0}%'", serialNumberOrRemark);
            }
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }
        /// <summary>
        /// 查询某条批量申请打款申请明细记录
        /// </summary>
        public DataTable SelectBatchMoneyApplyDetailByBatchMoneyApplyDetailId(long batchMoneyApplyDetailId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 batchMoneyApplyDetailId,batchMoneyApplyId,companyId,shopId,accountNum,bankName,accountName,applyAmount,serialNumberOrRemark,status,haveAdjustAmount from BatchMoneyApplyDetail ");
            strSql.Append(" where batchMoneyApplyDetailId=@batchMoneyApplyDetailId");
            SqlParameter[] parameters = {
					new SqlParameter("@batchMoneyApplyDetailId", SqlDbType.BigInt,8)
			};
            parameters[0].Value = batchMoneyApplyDetailId;
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters);
            return ds.Tables[0];
        }

        /// <summary>
        /// 查询门店的可打款余额
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public double SelectShopRemainMoney(int shopId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 isnull(remainMoney,0) remainMoney from ShopInfo ");
            strSql.Append(" where shopID=@shopID");
            SqlParameter[] parameters = {
					new SqlParameter("@shopID", SqlDbType.Int,4)
			};
            parameters[0].Value = shopId;
            double result = 0;
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters))
            {

                if (dr.Read())
                {
                    result = dr[0] == DBNull.Value ? 0 : Convert.ToDouble(dr[0]);
                }
            }
            return result;
        }
        /// <summary>
        /// 生成一条批量打款申请记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int InsertBatchMoneyApply(BatchMoneyApply model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into BatchMoneyApply(");
            strSql.Append("createdTime,advanceCount,practicalCount,advanceAmount,practicalAmount,operateEmployee,status,remark,cityId)");
            strSql.Append(" values (");
            strSql.Append("@createdTime,@advanceCount,@practicalCount,@advanceAmount,@practicalAmount,@operateEmployee,@status,@remark,@cityId)");
            strSql.Append(" select @@identity");
            SqlParameter[] parameters = {
					new SqlParameter("@createdTime", SqlDbType.DateTime),
					new SqlParameter("@advanceCount", SqlDbType.Int,4),
					new SqlParameter("@practicalCount", SqlDbType.Int,4),
					new SqlParameter("@advanceAmount", SqlDbType.Float,8),
					new SqlParameter("@practicalAmount", SqlDbType.Float,8),
					new SqlParameter("@operateEmployee", SqlDbType.Int,4),
					new SqlParameter("@status", SqlDbType.TinyInt,1),
					new SqlParameter("@remark", SqlDbType.NVarChar,500),
                    new SqlParameter("@cityId", SqlDbType.Int,4)};
            parameters[0].Value = model.createdTime;
            parameters[1].Value = model.advanceCount;
            parameters[2].Value = model.practicalCount;
            parameters[3].Value = model.advanceAmount;
            parameters[4].Value = model.practicalAmount;
            parameters[5].Value = model.operateEmployee;
            parameters[6].Value = model.status;
            parameters[7].Value = model.remark;
            parameters[8].Value = model.cityId;
            int rows = Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters));
            return rows;
        }
        /// <summary>
        /// 生成一条详细批量打款申请记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public long InsertBatchMoneyApplyDetail(BatchMoneyApplyDetail model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into BatchMoneyApplyDetail(");
            strSql.Append("batchMoneyApplyId,companyId,shopId,accountNum,bankName,accountName,applyAmount,serialNumberOrRemark,status)");
            strSql.Append(" values (");
            strSql.Append("@batchMoneyApplyId,@companyId,@shopId,@accountNum,@bankName,@accountName,@applyAmount,@serialNumberOrRemark,@status)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@batchMoneyApplyId", SqlDbType.Int,4),
					new SqlParameter("@companyId", SqlDbType.Int,4),
					new SqlParameter("@shopId", SqlDbType.Int,4),
					new SqlParameter("@accountNum", SqlDbType.VarChar,40),
					new SqlParameter("@bankName", SqlDbType.NVarChar,100),
					new SqlParameter("@accountName", SqlDbType.NVarChar,100),
					new SqlParameter("@applyAmount", SqlDbType.Float,8),
					new SqlParameter("@serialNumberOrRemark", SqlDbType.NVarChar,500),
					new SqlParameter("@status", SqlDbType.Int,4)};
            parameters[0].Value = model.batchMoneyApplyId;
            parameters[1].Value = model.companyId;
            parameters[2].Value = model.shopId;
            parameters[3].Value = model.accountNum;
            parameters[4].Value = model.bankName;
            parameters[5].Value = model.accountName;
            parameters[6].Value = model.applyAmount;
            parameters[7].Value = model.serialNumberOrRemark;
            parameters[8].Value = model.status;
            int rows = Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters));
            return rows;
        }
        /// <summary>
        /// 批量插入批量打款记录
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public bool BatchInsertBatchMoneyApplyDetail(DataTable dt)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
                {
                    conn.Open();
                    SqlTransaction sqlbulkTransaction = conn.BeginTransaction();
                    SqlBulkCopy sqlbulkcopy = new SqlBulkCopy(conn, SqlBulkCopyOptions.UseInternalTransaction, null);
                    sqlbulkcopy.DestinationTableName = "BatchMoneyApplyDetail";//鏁版嵁搴撲腑鐨勮〃鍚?
                    sqlbulkcopy.BulkCopyTimeout = 30;
                    sqlbulkcopy.ColumnMappings.Add("batchMoneyApplyId", "batchMoneyApplyId");
                    sqlbulkcopy.ColumnMappings.Add("accountId", "accountId");
                    sqlbulkcopy.ColumnMappings.Add("operateEmployee", "operateEmployee");
                    sqlbulkcopy.ColumnMappings.Add("companyId", "companyId");
                    sqlbulkcopy.ColumnMappings.Add("shopId", "shopId");
                    sqlbulkcopy.ColumnMappings.Add("accountNum", "accountNum");
                    sqlbulkcopy.ColumnMappings.Add("bankName", "bankName");
                    sqlbulkcopy.ColumnMappings.Add("accountName", "accountName");
                    sqlbulkcopy.ColumnMappings.Add("applyAmount", "applyAmount");
                    sqlbulkcopy.ColumnMappings.Add("serialNumberOrRemark", "serialNumberOrRemark");
                    sqlbulkcopy.ColumnMappings.Add("status", "status");
                    sqlbulkcopy.ColumnMappings.Add("haveAdjustAmount", "haveAdjustAmount");
                    sqlbulkcopy.ColumnMappings.Add("cityId", "cityId");
                    sqlbulkcopy.WriteToServer(dt);
                    sqlbulkTransaction.Commit();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 批量插入批量打款记录(新)
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public bool BatchInsertBatchMoneyApplyDetailNew(DataTable dt)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
                {
                    conn.Open();
                    SqlTransaction sqlbulkTransaction = conn.BeginTransaction();
                    SqlBulkCopy sqlbulkcopy = new SqlBulkCopy(conn, SqlBulkCopyOptions.UseInternalTransaction, null);
                    sqlbulkcopy.DestinationTableName = "BatchMoneyApplyDetail";//数据库中的表名
                    sqlbulkcopy.BulkCopyTimeout = 30;
                    sqlbulkcopy.ColumnMappings.Add("batchMoneyApplyId", "batchMoneyApplyId");
                    sqlbulkcopy.ColumnMappings.Add("accountId", "accountId");
                    sqlbulkcopy.ColumnMappings.Add("operateEmployee", "operateEmployee");
                    sqlbulkcopy.ColumnMappings.Add("companyId", "companyId");
                    sqlbulkcopy.ColumnMappings.Add("shopId", "shopId");
                    sqlbulkcopy.ColumnMappings.Add("accountNum", "accountNum");
                    sqlbulkcopy.ColumnMappings.Add("bankName", "bankName");
                    sqlbulkcopy.ColumnMappings.Add("accountName", "accountName");
                    sqlbulkcopy.ColumnMappings.Add("applyAmount", "applyAmount");
                    sqlbulkcopy.ColumnMappings.Add("serialNumberOrRemark", "serialNumberOrRemark");
                    sqlbulkcopy.ColumnMappings.Add("status", "status");
                    sqlbulkcopy.ColumnMappings.Add("haveAdjustAmount", "haveAdjustAmount");
                    sqlbulkcopy.ColumnMappings.Add("cityId", "cityId");
                    //sqlbulkcopy.ColumnMappings.Add("redEnvelopeAmount", "redEnvelopeAmount");
                    //sqlbulkcopy.ColumnMappings.Add("foodCouponAmount", "foodCouponAmount");
                    //sqlbulkcopy.ColumnMappings.Add("alipayAmount", "alipayAmount");
                    //sqlbulkcopy.ColumnMappings.Add("wechatPayAmount", "wechatPayAmount");
                    //sqlbulkcopy.ColumnMappings.Add("commissionAmount", "commissionAmount");
                    sqlbulkcopy.ColumnMappings.Add("batchMoneyApplyDetailCode", "batchMoneyApplyDetailCode");
                    sqlbulkcopy.ColumnMappings.Add("payeeBankName", "payeeBankName");
                    sqlbulkcopy.WriteToServer(dt);
                    sqlbulkTransaction.Commit();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// 保存一条申请批量打款明细记录
        /// </summary>
        public bool UpdateSaveBatchMoneyApplyDetail(BatchMoneyApplyDetail model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update BatchMoneyApplyDetail set ");
            strSql.Append("serialNumberOrRemark=@serialNumberOrRemark,");
            strSql.Append("status=@status,");
            strSql.Append("operateEmployee=@operateEmployee,");
            strSql.Append("accountId=@accountId,");
            strSql.Append("haveAdjustAmount=@haveAdjustAmount");
            strSql.Append(" where batchMoneyApplyDetailId=@batchMoneyApplyDetailId");
            SqlParameter[] parameters = {
					new SqlParameter("@serialNumberOrRemark", SqlDbType.NVarChar,500),
					new SqlParameter("@status", SqlDbType.Int,4),
                    new SqlParameter("@operateEmployee", SqlDbType.Int,4),
                    new SqlParameter("@accountId", SqlDbType.Int,4),
					new SqlParameter("@haveAdjustAmount", SqlDbType.Float,8),
					new SqlParameter("@batchMoneyApplyDetailId", SqlDbType.BigInt,8)};
            parameters[0].Value = model.serialNumberOrRemark;
            parameters[1].Value = model.status;
            parameters[2].Value = model.operateEmployee;
            parameters[3].Value = model.accountId;
            parameters[4].Value = model.haveAdjustAmount;
            parameters[5].Value = model.batchMoneyApplyDetailId;
            int rows = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 撤销一条申请批量打款明细记录（置状态，伪删除操作）
        /// </summary>
        public bool UpdateCancleBatchMoneyApplyDetail(BatchMoneyApplyDetail model)
        {
            const string strSql = @"update BatchMoneyApplyDetail set status=@status,operateEmployee=@operateEmployee,serialNumberOrRemark=@serialNumberOrRemark
 where batchMoneyApplyDetailId=@batchMoneyApplyDetailId";
            SqlParameter[] parameters = {
					new SqlParameter("@status", SqlDbType.Int,4),
                    new SqlParameter("@operateEmployee", SqlDbType.Int,4),
                    new SqlParameter("@serialNumberOrRemark", SqlDbType.NVarChar,500),
					new SqlParameter("@batchMoneyApplyDetailId", SqlDbType.BigInt,8)};
            parameters[0].Value = model.status;
            parameters[1].Value = model.operateEmployee;
            parameters[2].Value = model.serialNumberOrRemark;
            parameters[3].Value = model.batchMoneyApplyDetailId;
            int rows = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters);
            return rows > 0;
        }

        /// <summary>
        /// 申请预打款记录-1,预打款金额-amount
        /// </summary>
        /// <param name="batchMoneyApplyId"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public bool SubtractBatchMoneyApplyAdvanceCountAndAmount(int batchMoneyApplyId, double amount)
        {
            const string strSql = @"update [dbo].[BatchMoneyApply] set [advanceCount]=[advanceCount]-1,[advanceAmount]=[advanceAmount]-@amount where [batchMoneyApplyId]=@batchMoneyApplyId
 and advanceCount>0 and advanceAmount>-0.01";
            SqlParameter[] cmdParms = new SqlParameter[]
            {
                new SqlParameter("@batchMoneyApplyId", SqlDbType.Int) { Value = batchMoneyApplyId },
                new SqlParameter("@amount",SqlDbType.Float){Value =amount }, 
            };
            int rows = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, cmdParms);
            return rows > 0;
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool UpdateBatchMoneyApply(BatchMoneyApply model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update BatchMoneyApply set ");
            strSql.Append("practicalCount=isnull(practicalCount,0)+@practicalCount,");
            strSql.Append("practicalAmount=isnull(practicalAmount,0)+@practicalAmount");
            strSql.Append(" where batchMoneyApplyId=@batchMoneyApplyId");
            SqlParameter[] parameters = {
					new SqlParameter("@practicalCount", SqlDbType.Int,4),
					new SqlParameter("@practicalAmount", SqlDbType.Float,8),
					new SqlParameter("@batchMoneyApplyId", SqlDbType.Int,4)}; ;
            parameters[0].Value = model.practicalCount;
            parameters[1].Value = model.practicalAmount;
            parameters[2].Value = model.batchMoneyApplyId;
            int rows = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 查询某个商家的信息
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public DataTable SelectBatchMoneyMerchantApplyByShop(int shopId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select A.shopName,A.shopID,B.companyName,C.accountNum,C.bankName,");
            strSql.Append(" C.PayeeBankName,C.accountName,A.remainMoney,A.companyID,A.cityId,amountFrozen ");
            strSql.Append(" from ShopInfo A inner join CompanyInfo B on B.companyID=A.companyID");
            strSql.Append(" left join CompanyAccounts C on C.identity_Id=A.bankAccount and C.status=1");
            strSql.AppendFormat(" where A.shopId={0}", shopId);
            strSql.AppendFormat(" and Cast(A.remainMoney-isnull(amountFrozen,0) as numeric(18,2))>0  and a.shopID not in(select d.shopId from BatchMoneyApplyDetail d where d.shopID={0} and d.status=5)", shopId);
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }

        /// <summary>
        /// 查询某门店申请打款申请明细记录(最新一条)
        /// </summary>
        public DataTable SelectBatchMoneyApplyDetailByShop(int shopId, int status, int cityId, int isFirst, string shopName)
        {
            StringBuilder strSql = new StringBuilder();
            //strSql.Append("select a.batchMoneyApplyId,batchMoneyApplyDetailId,a.companyId,shopName,companyName,a.bankName,");
            //strSql.Append(" bankAccount,a.accountName,a.accountNum,a.PayeeBankName,b.createdTime,a.financePlayMoneyTime,");
            //strSql.Append(" a.applyAmount,a.RedEnvelopeAmount,a.FoodCouponAmount,");
            //strSql.Append(" a.AlipayAmount,a.WechatPayAmount,a.CommissionAmount,");
            //strSql.Append(" d.viewallocCommissionType,d.viewallocCommissionValue,");
            //strSql.Append(" serialNumberOrRemark,convert(varchar,a.status) status,c.shopID,batchMoneyApplyDetailCode, ");
            //strSql.Append(" a.applyAmount+a.CommissionAmount as volume,");
            //strSql.Append(" cast(a.commissionAmount/(a.applyAmount+CommissionAmount) as numeric(18, 2)) CommissionValue,convert(varchar,isFirst) isFirst");
            //strSql.Append(" from BatchMoneyApplyDetail a");
            //strSql.Append(" inner join BatchMoneyApply b on a.batchMoneyApplyId=b.batchMoneyApplyId");
            //strSql.Append(" inner join ShopInfo c on a.shopId=c.shopID");
            //strSql.Append(" inner join CompanyInfo d on a.companyId=d.companyID");
            //strSql.Append(" left join CompanyAccounts e on c.bankAccount=e.identity_Id");
            //strSql.Append(" where a.status>=5");

            strSql.Append("select a.companyId,c.shopID,a.batchMoneyApplyId,batchMoneyApplyDetailId,a.companyId,shopName,companyName,a.bankName,");
            strSql.Append(" bankAccount,a.accountName,a.accountNum,a.PayeeBankName,b.createdTime,a.financePlayMoneyTime,");
            strSql.Append(" a.applyAmount,serialNumberOrRemark,convert(varchar,a.status) status,c.shopID,batchMoneyApplyDetailCode,");
            strSql.Append(" convert(varchar,isFirst) isFirst,c.viewallocCommissionValue");
            strSql.Append(" from BatchMoneyApplyDetail a");
            strSql.Append(" inner join BatchMoneyApply b on a.batchMoneyApplyId=b.batchMoneyApplyId");
            strSql.Append(" inner join ShopInfo c on a.shopId=c.shopID");
            strSql.Append(" inner join CompanyInfo d on a.companyId=d.companyID");
            strSql.Append(" left join CompanyAccounts e on c.bankAccount=e.identity_Id");
            strSql.Append(" where a.status>=5");
            if (shopId > 0)
            {
                strSql.AppendFormat(" and a.shopId={0}", shopId);
            }
            if (status > 0)
            {
                strSql.AppendFormat(" and a.status={0}", status);
            }
            if (cityId > 0)
            {
                strSql.AppendFormat(" and a.cityId={0}", cityId);
            }
            if (isFirst < 2)
            {
                strSql.AppendFormat(" and isFirst={0}", isFirst);
            }
            if (shopName.Trim().Length > 0)
            {
                strSql.AppendFormat(" and shopName like '%{0}%'", shopName);
            }

            strSql.Append(" order by a.batchMoneyApplyDetailId desc");

            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }

        /// <summary>
        /// 修改打款明细状态
        /// </summary>
        public bool UpdateBatchMoneyApplyDetailStatus(BatchMoneyApplyDetail model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update BatchMoneyApplyDetail set ");
            strSql.Append("status=@status");

            List<SqlParameter> parameters1 = new List<SqlParameter>();
            parameters1.Add(new SqlParameter("@status", SqlDbType.Int, 4));
            parameters1.Add(new SqlParameter("@batchMoneyApplyDetailId", SqlDbType.Int));

            if (model.serialNumberOrRemark != null && model.serialNumberOrRemark != string.Empty)
            {
                strSql.Append(",serialNumberOrRemark=@serialNumberOrRemark");
                parameters1.Add(new SqlParameter("@serialNumberOrRemark", SqlDbType.NVarChar));
            }
            strSql.Append(" where batchMoneyApplyDetailId=@batchMoneyApplyDetailId");

            SqlParameter[] parameters = parameters1.ToArray();
            parameters[0].Value = model.status;
            parameters[1].Value = model.batchMoneyApplyDetailId;
            if (model.serialNumberOrRemark != null && model.serialNumberOrRemark != string.Empty)
            {
                parameters[2].Value = model.serialNumberOrRemark;
            }
            int rows = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 修改打款明细状态-打款+时间
        /// </summary>
        public bool UpdateBatchMoneyApplyDetailStatusPay(BatchMoneyApplyDetail model)
        {
            model.financePlayMoneyTime = DateTime.Now;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update BatchMoneyApplyDetail set ");
            strSql.Append("status=@status,");
            strSql.Append("financePlayMoneyTime=@financePlayMoneyTime");
            strSql.Append(" where batchMoneyApplyDetailId=@batchMoneyApplyDetailId");
            SqlParameter[] parameters = {
					new SqlParameter("@status", SqlDbType.Int,4),
					new SqlParameter("@financePlayMoneyTime", SqlDbType.DateTime),
                    new SqlParameter("@batchMoneyApplyDetailId", SqlDbType.Int)};
            parameters[0].Value = model.status;
            parameters[1].Value = model.financePlayMoneyTime;
            parameters[2].Value = model.batchMoneyApplyDetailId;
            
            int rows = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 查询某条批量申请打款申请明细记录
        /// </summary>
        public DataTable SelectBatchMoneyApplyDetailByBatchMoneyApplyDetailIdNew(long batchMoneyApplyDetailId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a.batchMoneyApplyDetailId,batchMoneyApplyDetailCode,");
            strSql.Append(" c.companyName,b.shopName,d.bankName,a.accountName,a.PayeeBankName,bankAccount,a.accountNum,");
            strSql.Append(" e.createdTime,a.financePlayMoneyTime,");
            strSql.Append(" applyAmount,redEnvelopeAmount,foodCouponAmount,wechatPayAmount,");
            strSql.Append(" alipayAmount,viewallocCommissionType,b.viewallocCommissionValue,");
            strSql.Append(" a.commissionAmount,a.status,serialNumberOrRemark,");
            strSql.Append(" b.remainMoney,isnull(b.amountFrozen,0) amountFrozen,");
            strSql.Append(" a.applyAmount+a.CommissionAmount as volume,");
            strSql.Append(" remainRedEnvelopeAmount,remainFoodCouponAmount,");
            strSql.Append(" remainAlipayAmount,remainWechatPayAmount,remainCommissionAmount,isFirst");
            strSql.Append(" from BatchMoneyApplyDetail a");
            strSql.Append(" inner join ShopInfo b on a.shopId=b.shopID");
            strSql.Append(" inner join CompanyInfo c on a.companyId=c.companyID");
            strSql.Append(" left join CompanyAccounts d on b.bankAccount=d.identity_Id");
            strSql.Append(" inner join BatchMoneyApply e on a.batchMoneyApplyId=e.batchMoneyApplyId");
            strSql.AppendFormat(" where a.batchMoneyApplyDetailId={0}",batchMoneyApplyDetailId);
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }

        /// <summary>
        /// 更新批量中的修改信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateSaveBatchMoneyApplyDetailFinance(BatchMoneyApplyDetail model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update BatchMoneyApplyDetail set ");
            strSql.Append("applyAmount=@applyAmount,");
            strSql.Append("redEnvelopeAmount=@redEnvelopeAmount,");
            strSql.Append("foodCouponAmount=@foodCouponAmount,");
            strSql.Append("wechatPayAmount=@wechatPayAmount,");
            strSql.Append("alipayAmount=@alipayAmount,");
            strSql.Append("commissionAmount=@commissionAmount");
            strSql.Append(" where batchMoneyApplyDetailId=@batchMoneyApplyDetailId");
            SqlParameter[] parameters = {
					new SqlParameter("@applyAmount", SqlDbType.Float),
                    new SqlParameter("@redEnvelopeAmount", SqlDbType.Float),
                    new SqlParameter("@foodCouponAmount", SqlDbType.Float),
                    new SqlParameter("@wechatPayAmount", SqlDbType.Float),
                    new SqlParameter("@alipayAmount", SqlDbType.Float),
                    new SqlParameter("@commissionAmount", SqlDbType.Float),
					new SqlParameter("@batchMoneyApplyDetailId", SqlDbType.Int)};
            parameters[0].Value = model.applyAmount;
            parameters[1].Value = model.redEnvelopeAmount;
            parameters[2].Value = model.foodCouponAmount;
            parameters[3].Value = model.wechatPayAmount;
            parameters[4].Value = model.alipayAmount;
            parameters[5].Value = model.commissionAmount;
            parameters[6].Value = model.batchMoneyApplyDetailId;
            int rows = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 查询某门店申请打款申请明细记录(最新一条)
        /// </summary>
        public DataTable SelectBatchMoneyApplyDetailByManager(string batchMoneyApplyDetailCode, int shopID, int status,string OperateBeginTime,string OperateEndTime,int cityid,int isFirst)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a.batchMoneyApplyId,batchMoneyApplyDetailId,shopName,a.companyId,companyName,a.bankName,");
            strSql.Append(" bankAccount,a.accountName,a.accountNum,a.PayeeBankName,b.createdTime,a.financePlayMoneyTime,");
            strSql.Append(" e.bankName newbankName,e.accountName newaccountName,e.accountNum newaccountNum,e.PayeeBankName newPayeeBankName,");
            strSql.Append(" a.applyAmount,serialNumberOrRemark,convert(varchar,a.status) status,c.shopID,batchMoneyApplyDetailCode, ");//,convert(varchar,playMoneyFlag) playMoneyFlag
            strSql.Append(" a.applyAmount+a.CommissionAmount as volume,c.viewallocCommissionValue,");
            strSql.Append(" cast(a.commissionAmount/(a.applyAmount+CommissionAmount) as numeric(18, 2)) CommissionValue,convert(varchar,isFirst) isFirst");
            strSql.Append(" from BatchMoneyApplyDetail a");
            strSql.Append(" inner join BatchMoneyApply b on a.batchMoneyApplyId=b.batchMoneyApplyId");
            strSql.Append(" inner join ShopInfo c on a.shopId=c.shopID");
            strSql.Append(" inner join CompanyInfo d on a.companyId=d.companyID");
            strSql.Append(" left join CompanyAccounts e on c.bankAccount=e.identity_Id");
            strSql.Append(" where a.status>=5");
            if (batchMoneyApplyDetailCode.Length > 0)
            {
                strSql.AppendFormat(" and a.batchMoneyApplyDetailCode like '{0}'", "%" + batchMoneyApplyDetailCode + "%");
            }
            if (status > 0)
            {
                strSql.AppendFormat(" and a.status={0}", status);
            }
            if (shopID > 0)
            {
                strSql.AppendFormat(" and c.shopID={0}", shopID);
            }
            if (!string.IsNullOrEmpty(OperateBeginTime))
            {
                strSql.AppendFormat(" and createdTime>='{0}'", OperateBeginTime);
            }
            if (!string.IsNullOrEmpty(OperateEndTime))
            {
                strSql.AppendFormat(" and createdTime<='{0}'", OperateEndTime);
            }
            if (cityid > 0)
            {
                strSql.AppendFormat(" and a.cityid='{0}'", cityid);
            }
            if (isFirst < 2)
            {
                strSql.AppendFormat(" and isFirst={0}", isFirst);
            }

            strSql.Append(" order by a.batchMoneyApplyDetailId desc");

            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }

        public DataTable QueryBatchMoneyMerchantApplyByWithdrawType(int cityid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select A.shopName,A.shopID,B.companyName,C.accountNum,C.bankName,C.accountName,C.payeeBankName,A.remainMoney,A.companyID,A.cityId,");
            strSql.Append(" remainRedEnvelopeAmount,remainFoodCouponAmount,remainAlipayAmount,remainWechatPayAmount,");
            strSql.Append(" remainCommissionAmount,amountFrozen,withdrawType");
            strSql.Append(" from ShopInfo A inner join CompanyInfo B on B.companyID=A.companyID");
            strSql.Append(" left join CompanyAccounts C on C.identity_Id=A.bankAccount and C.status=1");
            strSql.Append(" where Cast(A.remainMoney-isnull(amountFrozen,0) as numeric(18,2))>0 ");
            strSql.Append(" and a.shopID not in(select d.shopId from BatchMoneyApplyDetail d where d.status=5)");
            strSql.Append(" and A.withdrawType is not null");

            int week = (int)Math.Pow(2, (int)DateTime.Now.DayOfWeek-1);
            strSql.Append(" and A.withdrawType&" + week + ">0");
            if (cityid > 0)
            {
                strSql.AppendFormat(" and A.cityId={0}", cityid);
            }
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }
    }
}
