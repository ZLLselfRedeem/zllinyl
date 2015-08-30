using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Transactions;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL;

namespace VAGastronomistMobileApp.WebPageDll
{
    public class CompanyApplyPaymentOperate
    {
        ///// <summary>
        ///// （hyr）查询已对账且未结算完成的预点单信息
        ///// </summary>
        ///// <param name="companyId"></param>
        ///// <returns></returns>
        //public DataTable QueryPreOrderApprovedAndNotComplete(int companyId, int shopId, int hours)
        //{
        //    companyApplyPaymentManager payManager = new companyApplyPaymentManager();
        //    DataTable dtPreOrderAppNotComplete = payManager.SelectPreOrderShopApprovedAndNotComplete(companyId, shopId, hours);
        //    return dtPreOrderAppNotComplete;
        //}
        ///// <summary>
        ///// 根据公司编号查询对应公司银行账户信息
        ///// </summary>
        ///// <returns></returns>
        //public DataTable QueryCompanyInfo(int companyId)
        //{
        //    companyApplyPaymentManager payManager = new companyApplyPaymentManager();
        //    DataTable dtPayAccount = payManager.SelectAccountsInfo(companyId);
        //    return dtPayAccount;
        //}
        ///// <summary>
        ///// （hyr）生成申请记录&相关预点单批量插入映射表&修改预点单申请状态
        ///// </summary>
        ///// <returns></returns>
        //public long AddApplyPaymentInfo(Model.ApplyPaymentInfo apply)
        //{
        //    companyApplyPaymentManager payManager = new companyApplyPaymentManager();
        //    return payManager.InsertApplyPayment(apply);
        //}
        ///// <summary>
        ///// （hyr）生成申请记录&相关预点单批量插入映射表&修改预点单申请状态&需要冻结/解冻资金操作
        ///// </summary>
        ///// <returns></returns>
        //public bool GenerateApplyPayment(DataTable dtPreOrder, ApplyPaymentInfo apply)
        //{
        //    bool flag = false;
        //    using (TransactionScope scope = new TransactionScope())
        //    {
        //        if (dtPreOrder.Rows.Count > 0)
        //        {
        //            companyApplyPaymentManager payManager = new companyApplyPaymentManager();
        //            //返回生成的申请id（0为生成失败）
        //            long applyId = AddApplyPaymentInfo(apply);
        //            if (applyId > 0)
        //            {
        //                //获取预点单是否成功插入映射表
        //                bool mappingFlag = false;

        //                #region 需获取回款申请id
        //                DataColumn dcApplyId = new DataColumn("applyId", typeof(Int64));
        //                dcApplyId.DefaultValue = applyId;
        //                dtPreOrder.Columns.Add(dcApplyId);
        //                #endregion
        //                DataColumn dcMapping = new DataColumn("mappingType", typeof(byte));
        //                dcMapping.DefaultValue = (int)VAMappingStatus.PREORDER;
        //                dtPreOrder.Columns.Add(dcMapping);
        //                //读取冻结/解冻信息
        //                DataTable dtFrozenMoney = new DataTable();
        //                dtFrozenMoney = GenerateComplainMoneyToMapping(apply.companyId);

        //                //获取是否成功修改预点单的isApplied信息
        //                PreOrder19dianManager orderManager = new PreOrder19dianManager();
        //                int preOrderCheck = 0;
        //                int dtCount = dtPreOrder.Rows.Count;
        //                for (int i = 0; i < dtCount; i++)
        //                {
        //                    preOrderCheck += orderManager.UpdatePreOrderIsApplyFlag(Common.ToInt64(dtPreOrder.Rows[i]["preOrder19dianId"]));
        //                }
        //                //冻结信息与预点单合并
        //                bool complainCheck = true;
        //                if (dtFrozenMoney.Rows.Count > 0)
        //                {

        //                    dtPreOrder.Merge(dtFrozenMoney);
        //                    //更新申诉表信息冻结状态

        //                    for (int i = 0; i < dtFrozenMoney.Rows.Count; i++)
        //                    {
        //                        complainCheck = payManager.UpdateFrozenInfo(Common.ToInt64(dtFrozenMoney.Rows[i]["preOrder19dianId"]));
        //                        if (complainCheck == false)
        //                        {
        //                            break;
        //                        }
        //                    }
        //                }
        //                //预点单插入映射表
        //                mappingFlag = payManager.InsertPreOrderCopy(dtPreOrder);
        //                if (mappingFlag && applyId > 0 && preOrderCheck == dtCount && complainCheck)
        //                {
        //                    scope.Complete();
        //                    flag = true;
        //                }
        //            }
        //        }


        //    }


        //    return flag;
        //}

        ///// <summary>
        ///// （hyr）冻结/解冻资金操作
        ///// </summary>
        ///// <returns></returns>
        //public DataTable GenerateComplainMoneyToMapping(int companyId)
        //{
        //    companyApplyPaymentManager payManager = new companyApplyPaymentManager();
        //    SystemConfigManager systemConfigMan = new SystemConfigManager();
        //    //读取冻结资金金额
        //    DataTable dtSystem = systemConfigMan.SelectSystemConfig();
        //    DataView dvSystem = dtSystem.DefaultView;
        //    dvSystem.RowFilter = "configName = 'frozenValue'";
        //    double frozenValue = Common.ToInt64(dvSystem[0]["configContent"]);

        //    //首先查出申诉表中需冻结的预点单信息
        //    DataTable dtFreeze = new DataTable();
        //    dtFreeze = payManager.SelectFreezeMoneyComplainID(companyId);

        //    DataColumn freeze = new DataColumn("viewallocNeedsToPayToShop", typeof(Double));
        //    freeze.DefaultValue = -frozenValue;
        //    dtFreeze.Columns.Add(freeze);
        //    DataColumn unfreeze = new DataColumn("mappingType", typeof(byte));
        //    unfreeze.DefaultValue = VAMappingStatus.FREEZED;
        //    dtFreeze.Columns.Add(unfreeze);

        //    //再查出申诉表中处理完成
        //    DataTable dtUnFreeze = new DataTable();
        //    dtUnFreeze = payManager.SelectUnFreezeMoneyComplainID(companyId);

        //    for (int i = 0; i < dtUnFreeze.Rows.Count; i++)
        //    {
        //        dtUnFreeze.Rows[i]["viewallocNeedsToPayToShop"] = -Common.ToInt64(dtUnFreeze.Rows[i]["viewallocNeedsToPayToShop"]) + frozenValue;
        //    }

        //    DataColumn unfreezeOne = new DataColumn("mappingType", typeof(byte));
        //    unfreezeOne.DefaultValue = VAMappingStatus.UNFREEZED;
        //    dtUnFreeze.Columns.Add(unfreezeOne);

        //    try
        //    {
        //        dtFreeze.Merge(dtUnFreeze);
        //    }
        //    catch (Exception)
        //    {
        //        return null;
        //    }

        //    return dtFreeze;

        //}


        ///// <summary>
        ///// 根据公司编号查询已申请的回款信息
        ///// </summary>
        ///// <returns></returns>
        //public DataTable QueryAppliedList(int companyId, long receivedPayments)
        //{
        //    companyApplyPaymentManager payManager = new companyApplyPaymentManager();
        //    return payManager.SelectAppliedInfo(companyId, receivedPayments);
        //}
        ///// <summary>
        ///// 检查银行账户是否存在
        ///// </summary>
        ///// <returns></returns>
        //public bool QueryAccountCheck(string acc)
        //{
        //    companyApplyPaymentManager payManager = new companyApplyPaymentManager();
        //    return payManager.SelectAccountCheck(acc);
        //}
        ///// <summary>
        ///// （hyr）查询对应申请记录的的预点单信息
        ///// </summary>
        ///// <param name="companyId"></param>
        ///// <returns></returns>
        //public DataTable QueryAppliedOrder(long applyId)
        //{
        //    companyApplyPaymentManager payManager = new companyApplyPaymentManager();
        //    return payManager.SelectAppliedOrderInfo(applyId);
        //}
        ///// <summary>
        ///// 根据状态查询已申请的回款信息
        ///// </summary>
        ///// <returns></returns>
        //public DataTable QueryAppliedListByStatus(int status, long serialNumber)
        //{
        //    companyApplyPaymentManager payManager = new companyApplyPaymentManager();
        //    return payManager.SelectAppliedInfoByStatus(status, serialNumber);
        //}
        ///// <summary>
        ///// （hyr）未审核的回款申请更新其状态
        ///// </summary>
        ///// <returns></returns>
        //public bool QuerySetApplyStatus(int status, long applyId)
        //{
        //    companyApplyPaymentManager payManager = new companyApplyPaymentManager();
        //    return payManager.UpdateApplyStatus(status, applyId);
        //}
        ///// <summary>
        ///// （hyr）回款申请更新其金额
        ///// </summary>
        ///// <returns></returns>
        //public bool QueryUpdateApplyMoney(ApplyPaymentInfo apply)
        //{
        //    companyApplyPaymentManager payManager = new companyApplyPaymentManager();
        //    return payManager.UpdateApplyMoney(apply);
        //}
        ///// <summary>
        ///// （hyr）汇款确认并修改预点单状态
        ///// </summary>
        ///// <returns></returns>
        //public bool PaidConfirm(ApplyPaymentInfo apply)
        //{
        //    bool flag = false;
        //    using (TransactionScope scope = new TransactionScope())
        //    {
        //        companyApplyPaymentManager payManager = new companyApplyPaymentManager();


        //        int orderNum = payManager.SelectMappingOrderNum(apply.identity_Id);
        //        int orderChanged = payManager.UpdateOrderPayInfo(apply.identity_Id);
        //        bool ispaid = payManager.UpdatePaidInfo(apply);

        //        if (ispaid && orderNum == orderChanged)
        //        {
        //            scope.Complete();
        //            flag = true;
        //        }
        //        scope.Dispose();
        //    }
        //    return flag;
        //}
        ///// <summary>
        ///// (wangcheng)根据验证码查询预点单电子账号，支付时间，优惠券
        ///// </summary>
        ///// <returns></returns>
        //public DataTable QueryPreOrderFromVerificationCode(string verificationCode, string fromDate, string toDate)
        //{
        //    companyApplyPaymentManager payManager = new companyApplyPaymentManager();
        //    return payManager.SelectPreOrderByVerificationCode(verificationCode, fromDate, toDate);
        //}
        ///// <summary>
        ///// (wangcheng)根据验证码查询预点单信息
        ///// </summary>
        ///// <returns></returns>
        //public DataTable QueryFromVerificationCode(string verificationCode)
        //{
        //    companyApplyPaymentManager payManager = new companyApplyPaymentManager();
        //    return payManager.SelectByVerificationCode(verificationCode);
        //}
        ///// <summary>
        ///// (wangcheng)根据验证码信息查询出对应的 shop和company信息
        ///// </summary>
        ///// <returns></returns>
        //public DataTable QueryShopInforAndCompanyInforFromVerificationCode(string verificationCode)
        //{
        //    companyApplyPaymentManager payManager = new companyApplyPaymentManager();
        //    return payManager.GetShopInformationCompanyInfo(verificationCode);
        //}
        ///// <summary>
        ///// (wangcheng)根据预点单号查询出对应的唯一shop和company信息
        ///// </summary>
        ///// <returns></returns>
        //public DataTable QueryShopInforAndCompanyInforFromPreOrder19dianid(long preOrder19dianid)
        //{
        //    companyApplyPaymentManager payManager = new companyApplyPaymentManager();
        //    return payManager.GetShopInformationCompanyInfoFrompreOrder19dianId(preOrder19dianid);
        //}
        ///// <summary>
        ///// (wangcheng)根据优惠券验证码查询出对应用户信息
        ///// </summary>
        ///// <returns></returns>
        //public DataTable QueryInfoByCouponVerificationCode(string verificationCode, string fromDate, string toDate)
        //{
        //    companyApplyPaymentManager payManager = new companyApplyPaymentManager();
        //    return payManager.QueryByCouponVerificationCode(verificationCode, fromDate, toDate);

        //}
        ///// <summary>
        ///// (wangcheng)根据优惠券couponId查询出对应shop信息和company信息
        ///// </summary>
        ///// <returns></returns>
        //public DataTable QueryInfoBycompanyID(long couponId)
        //{
        //    companyApplyPaymentManager payManager = new companyApplyPaymentManager();
        //    return payManager.GetShopInformationCompanyInfoBycouponId(couponId);
        //}

        ///// <summary>
        ///// （hyr）生成申诉记录&返回自增id
        ///// </summary>
        ///// <returns></returns>
        //public bool AddComplainInfo(Model.UserComplain comp)
        //{
        //    bool flag = false;
        //    companyApplyPaymentManager payManager = new companyApplyPaymentManager();
        //    SystemConfigManager systemConfigMan = new SystemConfigManager();
        //    //读取冻结资金金额
        //    DataTable dtSystem = systemConfigMan.SelectSystemConfig();
        //    DataView dvSystem = dtSystem.DefaultView;
        //    dvSystem.RowFilter = "configName = 'frozenValue'";
        //    double frozenValue = Common.ToInt64(dvSystem[0]["configContent"]);
        //    using (TransactionScope scope = new TransactionScope())
        //    {

        //        long complainId = payManager.InsertUserComplain(comp);
        //        if (complainId > 0)
        //        {
        //            //判别优惠券、预点单
        //            if (comp.complainType == 2)
        //            {
        //                scope.Complete();
        //                flag = true;

        //            }
        //            else
        //            {

        //                //需要该correspondId对应的申请信息id
        //                long applyId = payManager.CheckOrderisApplid(comp.correspondId);
        //                bool check = true;
        //                if (applyId > 0)
        //                {
        //                    //如果申请id存在 判别状态是否为（审核中）
        //                    int status = payManager.CheckOrderisStatus(applyId);
        //                    if (status == (int)VAApplyStatus.CHECKING)
        //                    {
        //                        //加入对应冻结款信息
        //                        ApplyMapping apply = new ApplyMapping();
        //                        apply.applyId = applyId;
        //                        // apply.mappingId=1;
        //                        apply.viewallocNeedsToPayToShop = -frozenValue;
        //                        apply.mappingType = (int)VAMappingStatus.FREEZED;
        //                        check = payManager.InsertFrozenInfo(apply);
        //                    }

        //                }
        //                if (check)
        //                {
        //                    scope.Complete();
        //                    flag = true;
        //                }

        //            }
        //        }
        //    }

        //    return flag;
        //}

        ///// <summary>
        ///// （hyr）查询公司申请打款的小时数限制
        ///// </summary>
        ///// <returns></returns>
        //public int QueryLimitHours(int companyId)
        //{
        //    companyApplyPaymentManager payManager = new companyApplyPaymentManager();
        //    return payManager.SelectLimitHours(companyId);
        //}
        ///// <summary>
        ///// （hyr）查询金额限制
        ///// </summary>
        ///// <returns></returns>
        //public double QueryMoneyLimit()
        //{
        //    SystemConfigManager systemConfigMan = new SystemConfigManager();
        //    //读取金额限制
        //    DataTable dtSystem = systemConfigMan.SelectSystemConfig();
        //    DataView dvSystem = dtSystem.DefaultView;
        //    dvSystem.RowFilter = "configName = 'applyMoneyLimit'";
        //    return Common.ToInt64(dvSystem[0]["configContent"]);
        //}
        ///// <summary>
        ///// （hyr）客服设定投诉完成
        ///// </summary>
        ///// <returns></returns>
        //public bool QueryUpdateComplainEnd(string remark, long identity_Id, double reparation)
        //{
        //    companyApplyPaymentManager payManager = new companyApplyPaymentManager();
        //    return payManager.UpdateComplainFinish(remark, identity_Id, reparation);
        //}
        ///// <summary>
        ///// （wangcheng）查看申诉备注
        ///// </summary>
        ///// <returns></returns>
        //public string QueryComplainRemark(long identity_Id)
        //{
        //    companyApplyPaymentManager payManager = new companyApplyPaymentManager();
        //    return payManager.QueryRemarkFromidentityId(identity_Id);
        //}
        ///// <summary>
        ///// （wangcheng）更新申诉备注
        ///// </summary>
        ///// <returns></returns>
        //public bool UpdateComplainRemark(long identity_Id, string remark)
        //{
        //    companyApplyPaymentManager payManager = new companyApplyPaymentManager();
        //    return payManager.UpdateRemarkFromidentityId(identity_Id, remark);
        //}
        ///// <summary>
        ///// （wangcheng）获得couponid
        ///// </summary>
        ///// <returns></returns>
        //public long QueryCouponId(string verificationCode)
        //{
        //    companyApplyPaymentManager payManager = new companyApplyPaymentManager();
        //    return payManager.QueryCouponIdByCouponVerificationCode(verificationCode);
        //}

        //public int InsertBusinessPay(BusinessPay businesspay)
        //{
        //    companyApplyPaymentManager payManager = new companyApplyPaymentManager();
        //    return payManager.InsertBusinessPay(businesspay);
        //}

        //public int DeleteBusinessPay(int id)
        //{
        //    companyApplyPaymentManager payManager = new companyApplyPaymentManager();
        //    return payManager.DeleteBusinessPay(id);
        //}
        //public bool IsThisBusinessPayId(string paymentId)
        //{
        //    companyApplyPaymentManager payManager = new companyApplyPaymentManager();
        //    return payManager.IsThisBusinessPayId(paymentId);
        //}

        /// <summary>
        /// 获取某一项打款记录 add by wanga 20140417
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        public DataTable QueryBusinessPayByAccountId(long accountId)
        {
            companyApplyPaymentManager payManager = new companyApplyPaymentManager();
            return payManager.QueryBusinessPayByAccountId(accountId);
        }

        /// <summary>
        /// 更新备注 20140416 add by wangc
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        public bool ModifyRemark(long accountId, string remark)
        {
            MoneyMerchantAccountDetailManager manager = new MoneyMerchantAccountDetailManager();
            return manager.UpdateRemark(accountId, remark);
        }
        public DataTable QueryBusinessPay(int companyId, int shopId)
        {
            companyApplyPaymentManager payManager = new companyApplyPaymentManager();
            DataTable dtBusinessPay = payManager.QueryBusinessPay(companyId, shopId);
            return dtBusinessPay;
        }
    }
}
