using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using VAGastronomistMobileApp.SQLServerDAL;
using VAGastronomistMobileApp.Model;

namespace VAGastronomistMobileApp.WebPageDll
{
    /// <summary>
    /// 批量打款业务逻辑层
    /// created by wangc
    /// </summary>
    public class BatchMoneyOperate
    {
        readonly BatchMoneyManager manager = new BatchMoneyManager();
        /// <summary>
        /// 查询符合生成打款申请条件的商家信息
        /// </summary>
        /// <param name="minAmount"></param>
        /// <param name="cityId"></param>
        /// <returns></returns>
        public DataTable QueryBatchMoneyMerchantApply(double minAmount, int cityId)
        {
            return manager.SelectBatchMoneyMerchantApply(minAmount, cityId);
        }
        /// <summary>
        /// 查询符合生成打款申请条件的商家信息
        /// </summary>
        /// <param name="minAmount"></param>
        /// <param name="cityId"></param>
        /// <returns></returns>
        public DataTable QueryBatchMoneyMerchantApplyNew(double minAmount, int cityId)
        {
            return manager.SelectBatchMoneyMerchantApplyNew(minAmount, cityId);
        }
        /// <summary>
        /// 查询批量打款申请
        /// </summary>
        /// <param name="strTime"></param>
        /// <param name="endTime"></param>
        /// <param name="cityId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public DataTable QueryBatchMoneyApply(string strTime, string endTime, int cityId, string status)
        {
            return manager.SelectBatchMoneyApply(strTime, endTime, cityId, status);
        }
        /// <summary>
        /// 根据新建批量打款申请ID查询申请明细
        /// </summary>
        /// <param name="batchMoneyApplyId"></param>
        /// <param name="doExcel"></param>
        /// <returns></returns>
        public DataTable QueryBatchMoneyApplyDetailByBatchMoneyApplyId(int batchMoneyApplyId, bool doExcel)
        {
            return manager.SelectBatchMoneyApplyDetailByBatchMoneyApplyId(batchMoneyApplyId, doExcel);
        }
        /// <summary>
        ///  根据流水号查询申请明细
        /// </summary>
        /// <param name="serialNumberOrRemark"></param>
        /// <param name="doExcel"></param>
        /// <returns></returns>
        public DataTable QueryBatchMoneyApplyDetailBySerialNumberOrRemark(string serialNumberOrRemark, bool doExcel)
        {
            return manager.SelectBatchMoneyApplyDetailBySerialNumberOrRemark(serialNumberOrRemark, doExcel);
        }
        /// <summary>
        /// 查询某条批量申请打款申请明细记录
        /// </summary>
        public DataTable QueryBatchMoneyApplyDetailByBatchMoneyApplyDetailId(long batchMoneyApplyDetailId)
        {
            return manager.SelectBatchMoneyApplyDetailByBatchMoneyApplyDetailId(batchMoneyApplyDetailId);
        }
        /// <summary>
        /// 查询批量打款申请明细某条打款申请状态
        /// </summary>
        /// <param name="batchMoneyApplyDetailId"></param>
        /// <returns></returns>
        public bool QueryBatchMoneyApplyDetailStatus(long batchMoneyApplyDetailId)
        {
            DataTable dt = QueryBatchMoneyApplyDetailByBatchMoneyApplyDetailId(batchMoneyApplyDetailId);
            if (dt.Rows.Count > 0)
            {
                if (Common.ToInt32(dt.Rows[0]["status"]) != 1)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 查询门店的可打款余额
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public double QueryShopRemainMoney(int shopId)
        {
            return manager.SelectShopRemainMoney(shopId);
        }
        /// <summary>
        /// 生成一条批量打款申请记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddBatchMoneyApply(BatchMoneyApply model)
        {
            return manager.InsertBatchMoneyApply(model);
        }
        /// <summary>
        /// 生成一条详细批量打款申请记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public long AddBatchMoneyApplyDetail(BatchMoneyApplyDetail model)
        {
            return manager.InsertBatchMoneyApplyDetail(model);
        }
        /// <summary>
        /// 批量插入批量打款记录
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public bool BatchAddBatchMoneyApplyDetail(DataTable dt)
        {
            return manager.BatchInsertBatchMoneyApplyDetail(dt);
        }

        /// <summary>
        /// 批量插入批量打款记录(新)
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public bool BatchInsertBatchMoneyApplyDetailNew(DataTable dt)
        {
            return manager.BatchInsertBatchMoneyApplyDetailNew(dt);
        }
        /// <summary>
        /// 保存一条申请批量打款明细记录
        /// </summary>
        public bool ModifySaveBatchMoneyApplyDetail(BatchMoneyApplyDetail model)
        {
            return manager.UpdateSaveBatchMoneyApplyDetail(model);
        }
        /// <summary>
        /// 撤销一条申请批量打款明细记录（置状态，伪删除操作）
        /// </summary>
        public bool ModifyCancleBatchMoneyApplyDetail(BatchMoneyApplyDetail model)
        {
            //model.batchMoneyApplyId
            return manager.UpdateCancleBatchMoneyApplyDetail(model) && manager.SubtractBatchMoneyApplyAdvanceCountAndAmount(model.batchMoneyApplyId, model.applyAmount);

        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool ModifyBatchMoneyApply(BatchMoneyApply model)
        {
            return manager.UpdateBatchMoneyApply(model);
        }

        /// <summary>
        /// 查询符合生成打款申请条件的商家信息
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public DataTable QueryBatchMoneyMerchantApplyByShop(int shopId)
        {
            return manager.SelectBatchMoneyMerchantApplyByShop(shopId);
        }

        /// <summary>
        /// 查询符合生成打款申请条件的商家信息
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public DataTable SelectBatchMoneyApplyDetailByShop(int shopId, int status, int cityId, int isFirst, string shopName)
        {
            return manager.SelectBatchMoneyApplyDetailByShop(shopId, status, cityId, isFirst, shopName);
        }

        /// <summary>
        /// 保存一条申请批量打款明细记录
        /// </summary>
        public bool ModifyBatchMoneyApplyDetailStatus(BatchMoneyApplyDetail model, string employeeID)
        {
            if (model.status == BatchMoneyStatus.wait_for_confirm || model.status == BatchMoneyStatus.check_rejected || model.status == BatchMoneyStatus.bank_pay_failure)
            {
                string flowNumber = SybMoneyOperate.CreateMerchantFlowNumber(model.shopId);
                double remainMoney = new SybMoneyMerchantManager().GetShopInfoRemainMoney(model.shopId);
                double applyAmountFlg = 0;
                if (model.status == BatchMoneyStatus.wait_for_confirm)
                {
                    applyAmountFlg = model.applyAmount * -1;
                }
                else
                {
                    applyAmountFlg = model.applyAmount;
                }

                string remark=string.Empty;
                if(model.status==BatchMoneyStatus.wait_for_confirm)
                {
                    remark="行政申请提交至出纳"+model.batchMoneyApplyDetailId;
                }
                else if (model.status == BatchMoneyStatus.check_rejected)
                {
                    BalanceAccountOperate bao = new BalanceAccountOperate();
                    if (bao.IsHaveCheckAuthority(Common.ToInt32(employeeID)))
                    {
                        remark = "申请被出纳撤回" + model.batchMoneyApplyDetailId;
                    }
                    else if (bao.IsHaveConfirmAuthority(Common.ToInt32(employeeID)))
                    {
                        remark = "申请被财务撤回" + model.batchMoneyApplyDetailId;
                    }
                    else
                    {
                        remark = "申请被管理员撤回" + model.batchMoneyApplyDetailId;
                    }
                }
                else if (model.status == BatchMoneyStatus.bank_pay_failure)
                {
                    remark = "银行打款失败" + model.batchMoneyApplyDetailId;
                }

                MoneyMerchantAccountDetail data = new MoneyMerchantAccountDetail()
                {
                    companyId = model.companyId,
                    shopId = model.shopId,
                    accountMoney = Common.ToDouble(applyAmountFlg),
                    remainMoney = Common.ToDouble(remainMoney + applyAmountFlg),
                    flowNumber = flowNumber,
                    accountType = (int)AccountType.MERCHANT_CHECKOUT,
                    inoutcomeType = applyAmountFlg > 0 ? (int)InoutcomeType.IN : (int)InoutcomeType.OUT,
                    accountTypeConnId = "",
                    operUser = employeeID,
                    operTime = DateTime.Now,
                    confirmTime = DateTime.Now,
                    remark = remark,
                    status = (int)BalanceAccountStatus.confirmed
                };

                long accountId = MoneyMerchantAccountDetailManager.InsertBalanceAccount(data);
            }
            return manager.UpdateBatchMoneyApplyDetailStatus(model);
        }

        /// <summary>
        /// 保存一条申请批量打款明细记录-财务打款
        /// </summary>
        public bool ModifyBatchMoneyApplyDetailStatusPay(BatchMoneyApplyDetail model)
        {
            return manager.UpdateBatchMoneyApplyDetailStatusPay(model);
        }

        public DataTable SelectBatchMoneyApplyDetailByBatchMoneyApplyDetailIdNew(long batchMoneyApplyDetailId)
        {
            return manager.SelectBatchMoneyApplyDetailByBatchMoneyApplyDetailIdNew(batchMoneyApplyDetailId);
        }

        /// <summary>
        /// 保存一条申请批量打款明细记录
        /// </summary>
        public bool ModifySaveBatchMoneyApplyDetailFinance(BatchMoneyApplyDetail model)
        {
            return manager.UpdateSaveBatchMoneyApplyDetailFinance(model);
        }

           /// <summary>
        /// 查询打款申请明细记录管理页
        /// </summary>
        public DataTable SelectBatchMoneyApplyDetailByManager(string batchMoneyApplyDetailCode, int shopID, int status, string OperateBeginTime, string OperateEndTime,int cityid,int isFirst)
        {
            return manager.SelectBatchMoneyApplyDetailByManager(batchMoneyApplyDetailCode, shopID, status, OperateBeginTime, OperateEndTime, cityid, isFirst);
        }

        public DataTable QueryBatchMoneyMerchantApplyByWithdrawType(int cityid)
        {
            return manager.QueryBatchMoneyMerchantApplyByWithdrawType(cityid);
        }
    }
}
