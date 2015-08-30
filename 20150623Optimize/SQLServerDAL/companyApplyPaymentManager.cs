using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.DBUtility;
using VAGastronomistMobileApp.SQLServerDAL.Persistence.Infrastructure;

namespace VAGastronomistMobileApp.SQLServerDAL
{
    public class companyApplyPaymentManager
    {
        ///// <summary>
        ///// （hyr）查询已对账且未结算完成的预点单信息
        ///// </summary>
        ///// <param name="companyId"></param>
        ///// <returns></returns>
        //public DataTable SelectPreOrderShopApprovedAndNotComplete(int companyId, int shopId, int hours)
        //{
        //    StringBuilder strSql = new StringBuilder();
        //    SqlParameter[] param ={
        //                              new SqlParameter("@companyId",companyId),
        //                              new SqlParameter("@shopId",shopId),
        //                              new SqlParameter("@hours",hours)
        //                          };
        //    strSql.Append("select A.[preOrder19dianId],MAX(C.queryTime) as queryTime,A.[companyId],B.[shopName],[prePaidSum],[viewallocCommission],[viewallocNeedsToPayToShop],[verificationCode]");
        //    strSql.Append(" from PreOrder19dian A inner join ShopInfo B on A.shopId=B.shopID");
        //    strSql.Append(" inner join PreOrderQueryInfo C on A.preorder19dianId = C.preOrder19dianId");
        //    strSql.Append(" where A.isShopVerified=" + (int)VAPreorderIsShopVerified.SHOPVERIFIED + " and C.isShopVerified=" + (int)VAPreorderIsShopVerified.SHOPVERIFIED + " and A.isPaid=" + (int)VAPreorderIsPaid.PAID);
        //    strSql.Append(" and isnull([isApplied],'0' )<>" + (int)VAPreOrderIsApplied.ISAPPLIED + " and isnull([viewallocTransactionCompleted],'')<>" + (int)VATransactionCompleted.COMPLETED + " and A.companyId=@companyId and Datediff(hh,C.queryTime,getDate())>=@hours");
        //    if (shopId != 0)
        //    {
        //        strSql.Append(" and A.shopId=@shopId ");
        //    }
        //    strSql.Append(" group by A.[preOrder19dianId],A.[companyId],B.[shopName],[prePaidSum],[viewallocCommission],[viewallocNeedsToPayToShop],[verificationCode]");
        //    DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), param);
        //    return ds.Tables[0];
        //}

        ///// <summary>
        ///// （hyr）查询对应申请记录的的预点单信息
        ///// </summary>
        ///// <param name="companyId"></param>
        ///// <returns></returns>
        //public DataTable SelectAppliedOrderInfo(long applyId)
        //{
        //    StringBuilder strSql = new StringBuilder();
        //    SqlParameter[] param ={
        //                              new SqlParameter("@applyId",applyId)

        //                          };
        //    strSql.Append("SELECT preOrder19dianId, verificationCode,B.prePaidSum,B.viewallocCommission,B.viewallocNeedsToPayToShop,mappingType,C.shopName");
        //    strSql.Append(" FROM PreOrder19dian A right join ApplyMapping B on A.preOrder19dianId=B.mappingId");
        //    strSql.Append(" inner join ShopInfo C on A.shopId = C.shopID");
        //    strSql.Append(" where B.applyId=@applyId ");
        //    DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), param);
        //    return ds.Tables[0];
        //}

        ///// <summary>
        ///// （wangcheng）客服（根据验证码）查询预点单
        ///// </summary>
        ///// <returns></returns>
        //public DataTable SelectByVerificationCode(string verificationcode)
        //{
        //    StringBuilder strSql = new StringBuilder();
        //    SqlParameter[] param ={
        //                              new SqlParameter("@verificationCode",verificationcode)

        //                          };
        //    strSql.Append("SELECT [preOrder19dianId],[verificationCode],[eCardNumber],[status],[isPaid],[isShopVerified],[preOrderTime],[preOrderServerSum],[prePayTime]");
        //    strSql.Append(" FROM PreOrder19dian ");
        //    strSql.Append(" where verificationCode=@verificationCode ");
        //    DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), param);
        //    return ds.Tables[0];
        //}

        /// <summary>
        /// 获取打款记录
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public DataTable QueryBusinessPay(int companyId, int shopId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat(@"select accountId,accountMoney,operTime,remark from MoneyMerchantAccountDetail where companyId='{0}' and shopId='{1}' and accountType={2} order by accountId desc"
                    , companyId, shopId, (int)AccountType.MERCHANT_CHECKOUT);
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];

        }

        /// <summary>
        /// 获取某一项打款记录 add by wanga 20140417
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        public DataTable QueryBusinessPayByAccountId(long accountId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat(@"select accountMoney,operTime,MoneyMerchantAccountDetail.remark ,EmployeeInfo.EmployeeFirstName operEmployeeName
                                 from MoneyMerchantAccountDetail inner join EmployeeInfo on EmployeeInfo.EmployeeID=MoneyMerchantAccountDetail.operUser
                                where accountId='{0}' "
                    , accountId);
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }
        /// <summary>
        /// 根据公司编号查询对应公司账户信息
        /// </summary>
        /// <returns></returns>
        public DataTable SelectAccountsInfo(int companyId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select [accountNum],[bankName],[remark]");
            strSql.Append(" from CompanyAccounts");
            strSql.AppendFormat(" where companyId = {0}", companyId);
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }
        /// <summary>
        /// （hyr）新建一条申请打款信息
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public long InsertApplyPayment(ApplyPaymentInfo apply)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                Object obj = null;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    SqlParameter[] parameters = null;
                    strSql.Append("insert into [ApplyPaymentInfo](");
                    strSql.Append("[companyId],[shopId],[prePaidSum],[viewallocCommission],[actualPaidSum],[applyDate],");
                    strSql.Append("[applyStatus],[accountNum])");
                    strSql.Append(" values (");
                    strSql.Append("@companyId,@shopId,@prePaidSum,@viewallocCommission,@actualPaidSum,@applyDate,@applyStatus,@accountNum)");
                    strSql.Append(" select @@identity");
                    parameters = new SqlParameter[]{
					new SqlParameter("@companyId", SqlDbType.Int,4),
					new SqlParameter("@shopId", SqlDbType.Int,4),
					new SqlParameter("@prePaidSum", SqlDbType.Float,8),
					new SqlParameter("@viewallocCommission", SqlDbType.Float,8),
					new SqlParameter("@actualPaidSum", SqlDbType.Float,8),
					new SqlParameter("@applyDate", SqlDbType.DateTime),
                    //new SqlParameter("@appFromTime", SqlDbType.DateTime),
                    //new SqlParameter("@appToTime", SqlDbType.DateTime),
					new SqlParameter("@applyStatus", SqlDbType.TinyInt,1),
                    //new SqlParameter("@checkPersonId", SqlDbType.Int,4),
                    new SqlParameter("@accountNum", SqlDbType.VarChar,40),
                    //new SqlParameter("@RemittanceNum", SqlDbType.VarChar,50)
                    };

                    parameters[0].Value = apply.companyId;
                    parameters[1].Value = apply.shopId;
                    parameters[2].Value = apply.prePaidSum;
                    parameters[3].Value = apply.viewallocCommission;
                    parameters[4].Value = apply.actualPaidSum;
                    parameters[5].Value = apply.applyDate;
                    //parameters[6].Value = apply.appFromTime;
                    //parameters[7].Value = apply.appToTime;
                    parameters[6].Value = apply.applyStatus;
                    //parameters[8].Value = apply.checkPersonId;
                    parameters[7].Value = apply.accountNum;
                    //parameters[9].Value = apply.RemittanceNum;

                    obj = SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql.ToString(), parameters);
                }
                catch (Exception)
                {
                    return 0;
                }
                if (obj == null)
                {
                    return 0;
                }
                else
                {
                    return Convert.ToInt64(obj);
                }
            }
        }

        ///// <summary>
        ///// （hyr）预点单信息批量插入映射表
        ///// </summary>
        ///// <returns></returns>
        //public bool InsertPreOrderCopy(DataTable dt)
        //{
        //    bool flag = false;
        //    using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
        //    {
        //        if (conn.State != ConnectionState.Open)
        //            conn.Open();
        //        //bulkcopy 复制至映射表中
        //        // SqlBulkCopy sqlbulkcopy = new SqlBulkCopy(SqlHelper.ConnectionStringLocalTransaction, SqlBulkCopyOptions.UseInternalTransaction);
        //        SqlBulkCopy sqlbulkcopy = new SqlBulkCopy(conn, SqlBulkCopyOptions.UseInternalTransaction, null);
        //        sqlbulkcopy.DestinationTableName = "ApplyMapping";
        //        sqlbulkcopy.BulkCopyTimeout = 30;
        //        sqlbulkcopy.ColumnMappings.Add("preOrder19dianId", "mappingId");
        //        sqlbulkcopy.ColumnMappings.Add("prePaidSum", "prePaidSum");
        //        sqlbulkcopy.ColumnMappings.Add("viewallocCommission", "viewallocCommission");
        //        sqlbulkcopy.ColumnMappings.Add("viewallocNeedsToPayToShop", "viewallocNeedsToPayToShop");
        //        sqlbulkcopy.ColumnMappings.Add("applyId", "applyId");
        //        sqlbulkcopy.ColumnMappings.Add("mappingType", "mappingType");
        //        try
        //        {
        //            sqlbulkcopy.WriteToServer(dt);
        //            flag = true;
        //        }
        //        catch (Exception)
        //        {
        //            flag = false;
        //        }
        //    }
        //    return flag;
        //}
        /// <summary>
        /// 根据公司编号查询已申请的回款信息(wangcheng添加店铺和公司名称)
        /// </summary>
        /// <returns></returns>
        public DataTable SelectAppliedInfo(int companyId, long receivedPayments)
        {
            StringBuilder strSql = new StringBuilder();
            // strSql.Append("select [identity_Id],[applyDate],[prePaidSum],[viewallocCommission],[actualPaidSum],[applyStatus],B.[shopName],C.[companyName]");(源代码，2013-7-19)
            strSql.Append("select [identity_Id],[applyDate],[prePaidSum],[viewallocCommission],[actualPaidSum],[applyStatus],C.[companyName]");
            strSql.Append(" from ApplyPaymentInfo A ");

            //strSql.Append(" inner join ShopInfo B on B.shopId=A.shopId");(源代码，2013-7-19)
            strSql.Append(" inner join CompanyInfo C on C.companyID=A.companyId");

            strSql.AppendFormat(" where A.companyId = {0} ", companyId);
            if (receivedPayments > 0)
            {
                strSql.AppendFormat(" and A.identity_Id = {0} ", receivedPayments);
            }
            strSql.Append(" order by [applyDate] desc");
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }
        /// <summary>
        /// 检查银行账户是否存在
        /// </summary>
        /// <returns></returns>
        public bool SelectAccountCheck(string acc)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                Object obj = null;
                try
                {

                    StringBuilder strSql = new StringBuilder();
                    SqlParameter[] param ={
                                      new SqlParameter("@accountNum",acc)
                                     
                                  };
                    strSql.Append("select count([accountNum])");
                    strSql.Append("from [CompanyAccounts]");
                    strSql.Append("where [accountNum]=@accountNum");
                    obj = SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql.ToString(), param);
                }
                catch (Exception)
                {
                    return false;
                }
                if (obj == null)
                {
                    return false;
                }
                else
                {
                    int i = Convert.ToInt16(obj);

                    if (i > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }

            }
        }

        /// <summary>
        /// 根据状态查询已申请的回款信息
        /// </summary>
        /// <returns></returns>
        public DataTable SelectAppliedInfoByStatus(int status, long serialNumber)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select [identity_Id],B.[companyName],[applyDate],[prePaidSum],[viewallocCommission],[actualPaidSum],[applyStatus],[accountNum]");
            // strSql.Append("select [identity_Id],B.[companyName],C.[shopName],[applyDate],[prePaidSum],[viewallocCommission],[actualPaidSum],[applyStatus],[accountNum]");(源代码 wangcheng 2013-7-19)
            if (status == (int)VAApplyStatus.PAID)
            {
                strSql.Append(",[checkPersonId],[remittanceNum]");
            }
            strSql.Append(" from ApplyPaymentInfo A inner join CompanyInfo B on A.companyId=B.companyId");
            // strSql.Append(" inner join ShopInfo C on A.shopId=C.shopID");
            strSql.AppendFormat(" where applyStatus = {0}", status);
            if (serialNumber > 0)
            {
                strSql.AppendFormat(" and identity_Id ={0}", serialNumber);
            }
            strSql.Append(" order by [applyDate] desc");
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }
        /// <summary>
        /// （hyr）未审核的回款申请更新其状态
        /// </summary>
        /// <returns></returns>
        public bool UpdateApplyStatus(int status, long applyId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update ApplyPaymentInfo set ");
            strSql.Append("applyStatus=@applyStatus");
            strSql.Append(" where identity_Id=@applyId and isnull(applyStatus,'0')=" + (int)VAApplyStatus.CHECKING);
            SqlParameter[] parameters = {
                        new SqlParameter("@applyId", applyId),
                        new SqlParameter("@applyStatus", status)
                        };

            int result = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters);
            if (result >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// （hyr）回款申请更新其金额
        /// </summary>
        /// <returns></returns>
        public bool UpdateApplyMoney(ApplyPaymentInfo apply)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update ApplyPaymentInfo set ");
            strSql.Append("prePaidSum=@prePaidSum,viewallocCommission=@viewallocCommission,actualPaidSum=@actualPaidSum");
            strSql.Append(" where identity_Id=@applyId");
            SqlParameter[] parameters = {
                        new SqlParameter("@applyId", apply.identity_Id),
                        new SqlParameter("@prePaidSum",apply.prePaidSum),
                        new SqlParameter("@viewallocCommission",apply.viewallocCommission),
                        new SqlParameter("@actualPaidSum",apply.actualPaidSum)
                        };

            int result = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters);
            if (result >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// （hyr）回款申请更新其汇款信息
        /// </summary>
        /// <returns></returns>
        public bool UpdatePaidInfo(ApplyPaymentInfo apply)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update ApplyPaymentInfo set ");
            strSql.Append("applyStatus=" + (int)VAApplyStatus.PAID + ",checkPersonId=@checkPersonId,RemittanceNum=@RemittanceNum");
            strSql.Append(" where identity_Id=@applyId and applyStatus=@applyStatus");
            SqlParameter[] parameters = {
                        new SqlParameter("@applyId", apply.identity_Id),
                        new SqlParameter("@applyStatus",apply.applyStatus),
                        new SqlParameter("@checkPersonId",apply.checkPersonId),
                        new SqlParameter("@RemittanceNum",apply.RemittanceNum)
                        };

            int result = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters);
            if (result >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// （hyr）更新申请对应预点单的付款、结算完成信息,返回更新行数viewallocPaidToShopSum=viewallocNeedsToPayToShop,
        /// </summary>
        /// <returns></returns>
        public int UpdateOrderPayInfo(long applyId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update PreOrder19dian set ");
            strSql.Append(" viewallocPaidToShopSum=viewallocNeedsToPayToShop,viewallocTransactionCompleted=" + (int)VATransactionCompleted.COMPLETED);
            strSql.Append(" where preOrder19dianId in");
            strSql.Append(" (select mappingId from ApplyMapping A inner join ApplyPaymentInfo B on A.applyId=B.identity_Id");
            strSql.Append(" where B.identity_Id=@applyId and A.mappingType=" + (int)VAMappingStatus.PREORDER + ")");
            strSql.Append(" and isnull(viewallocTransactionCompleted,'1')=" + (int)VATransactionCompleted.NOT_COMPLETED);
            SqlParameter[] parameters = {
                        new SqlParameter("@applyId", applyId)
                        };

            int result = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters);
            return result;
        }
        /// <summary>
        /// （hyr）查询申请对应预点单的数目
        /// </summary>
        /// <returns></returns>
        public int SelectMappingOrderNum(long applyId)
        {
            StringBuilder strSql = new StringBuilder();
            SqlParameter[] parameters ={
                                      new SqlParameter("@applyId",applyId)
                                      
                                  };
            strSql.Append("select count(*) from ApplyMapping ");
            strSql.Append(" where applyId=@applyId and mappingType = " + (int)VAMappingStatus.PREORDER);
            object obj = SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }

        }

        /// <summary>
        /// （hyr）客服生成投诉记录
        /// </summary>
        /// <returns></returns>
        public long InsertUserComplain(UserComplain comp)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                Object obj = null;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("insert into UserComplain(");
                    strSql.Append("complainType,complainDate,companyId,correspondId,verificationCode,correspondApplyId,complainStatus,eCardNumber,staffId,remark)");
                    strSql.Append(" values (");
                    strSql.Append("@complainType,@complainDate,@companyId,@correspondId,@verificationCode,@correspondApplyId,@complainStatus,@eCardNumber,@staffId,@remark)");
                    strSql.Append(" select @@identity");

                    SqlParameter[] parameters = {
					new SqlParameter("@complainType", SqlDbType.TinyInt,1),
                    new SqlParameter("@complainDate", SqlDbType.DateTime),
                    new SqlParameter("@companyId", SqlDbType.Int,4),
					new SqlParameter("@correspondId", SqlDbType.BigInt,8),
					new SqlParameter("@verificationCode", SqlDbType.NVarChar,50),
					new SqlParameter("@correspondApplyId", SqlDbType.BigInt,8),
					new SqlParameter("@complainStatus", SqlDbType.TinyInt,1),				
					new SqlParameter("@eCardNumber", SqlDbType.NVarChar,50),
					new SqlParameter("@staffId", SqlDbType.Int,4),
					new SqlParameter("@remark", SqlDbType.NVarChar,3000)};
                    parameters[0].Value = comp.complainType;
                    parameters[1].Value = comp.complainDate;
                    parameters[2].Value = comp.companyId;
                    parameters[3].Value = comp.correspondId;
                    parameters[4].Value = comp.verificationCode;
                    parameters[5].Value = comp.correspondApplyId;
                    parameters[6].Value = comp.complainStatus;
                    parameters[7].Value = comp.eCardNumber;
                    parameters[8].Value = comp.staffId;
                    parameters[9].Value = comp.remark;


                    obj = SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql.ToString(), parameters);
                }
                catch (Exception)
                {
                    return 0;
                }
                if (obj == null)
                {
                    return 0;
                }
                else
                {
                    return Convert.ToInt64(obj);
                }
            }
        }

        /// <summary>
        /// （hyr）客服设定投诉完成
        /// </summary>
        /// <returns></returns>
        public bool UpdateComplainFinish(string remark, long identity_Id, double reparation)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update UserComplain set");
            strSql.Append(" remark+=@remark,reparation=@reparation,complainStatus=" + (int)VAComplainStatus.COMPLAIN_COMPLETE);
            strSql.Append(" where identity_Id=@identity_Id");
            SqlParameter[] parameters = {
                        new SqlParameter("@remark",remark),
                        new SqlParameter("@reparation",reparation),
                        new SqlParameter("@identity_Id",identity_Id)
                      
                        };

            int result = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters);
            if (result >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// （hyr）根据状态查询投诉信息
        /// </summary>
        /// <returns></returns>
        public DataTable SelectComplainInfo(int complainStatus)
        {
            StringBuilder strSql = new StringBuilder();
            SqlParameter[] param ={
                                      new SqlParameter("@complainStatus",complainStatus)
                                      
                                  };
            strSql.Append("SELECT *");
            strSql.Append(" FROM UserComplain ");
            strSql.Append(" where complainStatus=@complainStatus order by complainDate desc");
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), param);
            return ds.Tables[0];
        }
        /// <summary>
        /// （hyr）查询投诉的预点单提交汇款申请的id
        /// </summary>
        /// <returns></returns>
        public long CheckOrderisApplid(long preOrderId)
        {
            StringBuilder strSql = new StringBuilder();
            SqlParameter[] parameters ={
                                      new SqlParameter("@preOrderId",preOrderId)                                  
                                  };
            strSql.Append("select applyId from ApplyMapping");
            strSql.Append(" where mappingId =@preOrderId");
            strSql.Append(" and mappingType=" + (int)VAMappingStatus.PREORDER + " order by applyId desc");
            object obj = SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt64(obj);
            }
        }
        /// <summary>
        /// （hyr）查询投诉的预点单所在的汇款申请状态
        /// </summary>
        /// <returns></returns>
        public int CheckOrderisStatus(long applyId)
        {
            StringBuilder strSql = new StringBuilder();
            SqlParameter[] parameters ={
                                      new SqlParameter("@applyId",applyId)                                  
                                  };
            strSql.Append("select applyStatus from ApplyPaymentInfo");
            strSql.Append(" where identity_Id =@applyId");

            object obj = SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// （hyr）根据预点单号查询正在申诉的投诉记录数量
        /// </summary>
        /// <returns></returns>
        public int SelectPreOrderComplainRecord(long preOrder19dianId)
        {
            StringBuilder strSql = new StringBuilder();
            SqlParameter[] parameters ={
                                      new SqlParameter("@preOrder19dianId",preOrder19dianId)                                  
                                  };
            strSql.Append("select count(*) from UserComplain");
            strSql.Append(" where correspondId=@preOrder19dianId and isnull(complainStatus,'')=" + (int)VAComplainStatus.COMPLAINING);
            strSql.Append(" where B.identity_Id=@identity_Id");
            object obj = SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }



        /// <summary>
        /// （hyr）映射表冻结/解冻资金
        /// </summary>
        /// <returns></returns>
        public bool InsertFrozenInfo(ApplyMapping mapping)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int result = 0;
                try
                {
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("insert into ApplyMapping(");
                    strSql.Append("applyId,mappingId,prePaidSum,viewallocCommission,viewallocNeedsToPayToShop,mappingType)");
                    strSql.Append(" values (");
                    strSql.Append("@applyId,@mappingId,@prePaidSum,@viewallocCommission,@viewallocNeedsToPayToShop,@mappingType)");
                    SqlParameter[] parameters = {
					new SqlParameter("@applyId", SqlDbType.BigInt,8),
					new SqlParameter("@mappingId", SqlDbType.BigInt,8),
					new SqlParameter("@prePaidSum", SqlDbType.Float,8),
					new SqlParameter("@viewallocCommission", SqlDbType.Float,8),
					new SqlParameter("@viewallocNeedsToPayToShop", SqlDbType.Float,8),
					new SqlParameter("@mappingType", SqlDbType.TinyInt,1)};
                    parameters[0].Value = mapping.applyId;
                    parameters[1].Value = mapping.mappingId;
                    parameters[2].Value = mapping.prePaidSum;
                    parameters[3].Value = mapping.viewallocCommission;
                    //资金数额
                    parameters[4].Value = mapping.viewallocNeedsToPayToShop;
                    //资金类型（冻结2，解冻3）
                    parameters[5].Value = mapping.mappingType;

                    result = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);
                }
                catch (Exception)
                {
                    return false;
                }
                if (result == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        /// <summary>
        /// 查询申诉表中需冻结的预点单信息
        /// </summary>
        /// <param name="preOrder19dianId"></param>
        /// <returns></returns>
        public DataTable SelectFreezeMoneyComplainID(int companyId)
        {
            //using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            //{
            StringBuilder strSql = new StringBuilder();
            SqlParameter[] param ={
                                    new SqlParameter("@companyId",companyId)
                                  };
            strSql.Append("select identity_Id as preOrder19dianId");
            strSql.Append(" from UserComplain");
            strSql.Append(" where companyId=@companyId and complainStatus =" + (int)VAComplainStatus.COMPLAINING + " and isFrozen =" + (int)VAFreezeMoneyStatus.NOT_FREEZED);
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), param);
            return ds.Tables[0];
            //}
        }

        /// <summary>
        /// 查询申诉表中需解冻的预点单信息
        /// </summary>
        /// <param name="preOrder19dianId"></param>
        /// <returns></returns>
        public DataTable SelectUnFreezeMoneyComplainID(int companyId)
        {
            StringBuilder strSql = new StringBuilder();
            SqlParameter[] param ={
                                    new SqlParameter("@companyId",companyId)
                                  };
            strSql.Append("select identity_Id as preOrder19dianId,reparation as viewallocNeedsToPayToShop from UserComplain");
            strSql.Append(" where complainStatus =" + (int)VAComplainStatus.COMPLAIN_COMPLETE + " and isFrozen =" + (int)VAFreezeMoneyStatus.FREEZED + " and isPaid =" + VAReparationStatus.ISPAID + " and companyId=@companyId");
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), param);
            return ds.Tables[0];
        }
        /// <summary>
        /// （wangcheng）客服（根据验证码,时间）查询预点单
        /// </summary>
        /// <returns></returns>
        public DataTable SelectPreOrderByVerificationCode(string verificationcode, string fromDate, string toDate)
        {
            StringBuilder strSql = new StringBuilder();
            SqlParameter[] param ={
                                      new SqlParameter("@verificationCode",verificationcode),
                                      new SqlParameter("@fromDate",fromDate),
                                      new SqlParameter("@toDate",toDate)
                                  };
            strSql.Append("SELECT [preOrder19dianId],[verificationCode],[eCardNumber],[prePayTime]");
            strSql.Append(" FROM PreOrder19dian ");
            strSql.Append(" where verificationCode=@verificationCode and preOrderTime between @fromDate and @toDate");
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), param);
            return ds.Tables[0];
        }
        /// <summary>
        /// （wangcheng）根据验证码获取shopid
        /// </summary>
        /// <returns></returns>
        public int GetShopId(string identifying)
        {
            StringBuilder strSql = new StringBuilder();
            SqlParameter[] parameters ={
                                      new SqlParameter("@identifying",identifying)                                  
                                  };
            strSql.Append("select shopId from PreOrder19dian");
            strSql.Append(" where verificationCode=@identifying");
            object obj = SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// （wangcheng）根据verificationCode获取预点单,ShopInfo和CompanyInfo等信息（表）
        /// </summary>
        /// <returns></returns>
        public DataTable GetShopInformationCompanyInfo(string verificationCode)
        {
            StringBuilder strSql = new StringBuilder();
            SqlParameter[] parameters ={
                                      new SqlParameter("@verificationCode",verificationCode)                                  
                                  };
            //公司表
            strSql.Append("select B.[companyName],B.[companyAddress],B.[companyTelePhone]");
            strSql.Append(",B.[contactPerson] as [contactCompanyPerson],B.[contactPhone] as [contactCompanyPhone] ");
            //门店表
            strSql.Append(",C.[shopName],C.[shopAddress],C.[shopTelephone],C.[contactPerson] as [contactShopPerson] ,C.[contactPhone] as [contactShopPhone]");
            //预点单表
            // strSql.Append(",A.[isShopQueried],A.[preOrderTime],A.[isPaid],A.[prePayTime],A.[preOrderServerSum],A.[customerUUID]");

            strSql.Append("from PreOrder19dian A inner join CompanyInfo B on A.companyId=B.companyId");
            strSql.Append(" inner join shopInfo C on A.shopId=C.shopId");
            strSql.Append(" where A.verificationCode=@verificationCode and B.companyId=C.companyId");
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters);
            return ds.Tables[0];
        }
        /// <summary>
        /// （wangcheng）根据couponId获取ShopInfo和CompanyInfo等信息（表）
        /// </summary>
        /// <returns></returns>
        public DataTable GetShopInformationCompanyInfoBycouponId(long couponId)
        {
            StringBuilder strSql = new StringBuilder();
            SqlParameter[] parameters ={
                                      new SqlParameter("@couponId",couponId)                                  
                                  };
            //公司表
            strSql.Append("select B.[companyID], B.[companyName],B.[companyAddress],B.[companyTelePhone]");
            strSql.Append(",B.[contactPerson] as [contactCompanyPerson],B.[contactPhone] as [contactCompanyPhone] ");
            //门店表
            strSql.Append(",C.[shopName],C.[shopAddress],C.[shopTelephone],C.[contactPerson] as [contactShopPerson] ,C.[contactPhone] as [contactShopPhone],D.[eCardNumber]");

            // strSql.Append(",D.[verificationCode]");

            strSql.Append("  from CouponConnShop A inner join ShopInfo C on A.shopID =C.shopID");

            strSql.Append("  inner join CompanyInfo B on A.companyId=B.companyID");

            strSql.Append("  inner join CouponUsed E on A.couponID=E.couponId");
            strSql.Append("  inner join CustomerInfo D on E.[customerId]=D.[CustomerID]");

            strSql.Append("  where E.couponId= @couponId");
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters);
            return ds.Tables[0];
        }
        /// <summary>
        /// （wangcheng）根据preOrder19dianId获取预点单,ShopInfo和CompanyInfo等信息（表）
        /// </summary>
        /// <returns></returns>
        public DataTable GetShopInformationCompanyInfoFrompreOrder19dianId(long preOrder19dianid)
        {
            StringBuilder strSql = new StringBuilder();
            SqlParameter[] parameters ={
                                      new SqlParameter("@preOrder19dianId",preOrder19dianid)                                  
                                  };
            //公司表
            strSql.Append("select B.[companyId],B.[companyName],B.[companyAddress],B.[companyTelePhone]");
            strSql.Append(",B.[contactPerson] as [contactCompanyPerson],B.[contactPhone] as [contactCompanyPhone] ");
            //门店表
            strSql.Append(",C.[shopName],C.[shopAddress],C.[shopTelephone],C.[contactPerson] as [contactShopPerson] ,C.[contactPhone] as [contactShopPhone]");
            //预点单表
            strSql.Append(",A.[isShopVerified],A.[preOrderTime],A.[isPaid],A.[prePayTime],");
            strSql.Append("A.[preOrderServerSum],A.[customerUUID],A.[verificationCode],A.[eCardNumber],A.[preOrder19dianId]");

            strSql.Append("from PreOrder19dian A inner join CompanyInfo B on A.companyId=B.companyId");
            strSql.Append(" inner join shopInfo C on A.shopId=C.shopId");
            strSql.Append(" where A.preOrder19dianId=@preOrder19dianid and B.companyId=C.companyId");
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            else
            {
                return ds.Tables[0];
            }
        }
        /// <summary>
        /// 优惠券验证码查询优惠券和用户信息
        /// </summary>
        /// <param name="couponverificationCode"></param>
        /// <returns></returns>
        public DataTable QueryByCouponVerificationCode(string verificationCode, string fromDate, string toDate)
        {
            StringBuilder strSql = new StringBuilder();
            SqlParameter[] param = { 
                                   new SqlParameter("@verificationCode",verificationCode),
                                    new SqlParameter("@fromDate",fromDate),
                                    new SqlParameter("@toDate",toDate)
                                   };
            strSql.Append("select A.[couponId],A.[verificationCode],A.[paid],B.[eCardNumber],A.[usedTime],A.[couponUsedId]");
            strSql.Append(" from CouponUsed A inner join CustomerInfo B on A.customerId=B.CustomerID");
            strSql.Append(" where A.verificationCode=@verificationCode and A.usedTime between @fromDate and @toDate");
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), param);
            return ds.Tables[0];
        }
        ///// <summary>
        ///// （hyr）更新申诉表冻结状态信息
        ///// </summary>
        ///// <returns></returns>
        //public bool UpdateFrozenInfo(long identity_Id)
        //{
        //    using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
        //    {
        //        StringBuilder strSql = new StringBuilder();
        //        SqlParameter[] parameters = {
        //                new SqlParameter("@identity_Id",identity_Id)
        //                };
        //        strSql.Append("update UserComplain set");
        //        strSql.Append(" isFrozen+=1");
        //        strSql.Append(" where identity_Id=@identity_Id");
        //        int result = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);
        //        if (result >= 1)
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //}

        /// <summary>
        /// （hyr）查询公司申请打款的小时数限制
        /// </summary>
        /// <returns></returns>
        public int SelectLimitHours(int companyId)
        {
            StringBuilder strSql = new StringBuilder();
            SqlParameter[] parameters ={
                                      new SqlParameter("@companyId",companyId)                                  
                                  };
            strSql.Append("select applyLimitHours from CompanyInfo");
            strSql.Append(" where companyId=@companyId ");
            object obj = SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters);
            if (obj == null || Convert.ToString(obj) == "")
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }

        /// <summary>
        /// (wangcheng)查询投诉备注
        /// </summary>
        /// <returns></returns>
        public String QueryRemarkFromidentityId(long identity_Id)
        {
            StringBuilder strSql = new StringBuilder();
            SqlParameter[] parameters ={
                                      new SqlParameter("@identity_Id",identity_Id)
                                  };
            strSql.Append("select [remark] from UserComplain");
            strSql.Append(" where identity_Id=@identity_Id ");
            object obj = SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters);
            return obj.ToString();
        }
        /// <summary>
        /// (wangcheng)更新投诉备注
        /// </summary>
        /// <returns></returns>
        public bool UpdateRemarkFromidentityId(long identity_Id, string remark)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                StringBuilder strSql = new StringBuilder();
                SqlParameter[] parameters ={
                                      new SqlParameter("@identity_Id",identity_Id),
                                       new SqlParameter("@remark",remark)
                                  };
                strSql.Append("update UserComplain set remark=@remark ");
                strSql.Append(" where identity_Id=@identity_Id ");
                int result = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);
                if (result >= 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        ///根据优惠券查询出couponId
        public long QueryCouponIdByCouponVerificationCode(string verificationCode)
        {
            StringBuilder strSql = new StringBuilder();
            SqlParameter[] parameters ={
                                      new SqlParameter("@verificationCode",verificationCode)
                                  };
            strSql.Append("Select couponId from CouponUsed where verificationCode=@verificationCode");
            object obj = SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters);
            long couid = Convert.ToInt64(obj);
            return couid;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="busniesspay"></param>
        /// <returns></returns>
        public int InsertBusinessPay(BusinessPay businesspay)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int result = 0;
                try
                {
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("insert into BusinessPay(");
                    strSql.Append("Btime,pay,type,companyId,shopId,paymentId)");
                    strSql.Append(" values (");
                    strSql.Append("@Btime,@pay,@type,@companyId,@shopId,@paymentId)");
                    SqlParameter[] parameters = {
					new SqlParameter("@Btime", SqlDbType.DateTime),
					new SqlParameter("@pay", SqlDbType.Float,8),
					new SqlParameter("@type", SqlDbType.Int),
					new SqlParameter("@companyId", SqlDbType.Int),
					new SqlParameter("@shopId", SqlDbType.Int),
                      new SqlParameter("@paymentId", SqlDbType.NVarChar)                          };
                    parameters[0].Value = businesspay.paytime;
                    parameters[1].Value = businesspay.pay;
                    parameters[2].Value = -1;
                    parameters[3].Value = businesspay.companyId;
                    parameters[4].Value = businesspay.shopId;
                    parameters[5].Value = businesspay.paymentId;
                    result = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);
                }
                catch (Exception)
                {
                    return 0;
                }
                return result;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="busniesspay"></param>
        /// <returns></returns>
        public int DeleteBusinessPay(int id)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                SqlTransaction tran = null;
                int result = 0;
                try
                {
                    conn.Open();
                    tran = conn.BeginTransaction();
                    StringBuilder strSql = new StringBuilder();

                    strSql.Append("delete from BusinessPay where id=@id;");

                    SqlParameter[] parameters = {					
					new SqlParameter("@id", SqlDbType.Int,4)};
                    parameters[0].Value = id;

                    result = SqlHelper.ExecuteNonQuery(tran, CommandType.Text, strSql.ToString(), parameters);

                    tran.Commit();
                }
                catch
                {
                    if (tran != null)
                    {
                        tran.Rollback();
                    }
                }
                if (result > 0)
                {
                    return result;
                }
                else
                {
                    return 0;
                }

            }

        }

        /// <summary>
        /// 查询是否该Id已经存在 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool IsThisBusinessPayId(string paymentId)
        {
            StringBuilder strSql = new StringBuilder();
            SqlParameter[] parameters ={
                                      new SqlParameter("@paymentId",paymentId)
                                  };
            strSql.Append("Select id from BusinessPay where paymentId=@paymentId");
            object obj = SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters);
            long iddatabase = Convert.ToInt64(obj);
            if (iddatabase > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

