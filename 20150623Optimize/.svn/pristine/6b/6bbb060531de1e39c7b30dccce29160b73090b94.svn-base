using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL;

namespace VAGastronomistMobileApp.WebPageDll
{
    /// <summary>
    /// 平账管理业务逻辑
    /// Add at 2015-4-10
    /// </summary>
    public class BalanceAccountOperate
    {
        /// <summary>
        /// 分页查询某门店对应的平账记录
        /// </summary>
        /// <param name="page"></param>
        /// <param name="shopId"></param>
        /// <param name="cnt"></param>
        /// <returns></returns>
        public List<BalanceAccountDetail> QueryBusinessPay(VAGastronomistMobileApp.SQLServerDAL.Persistence.Infrastructure.Page page, int shopId, int status, long accountId, DateTime? beginTime, DateTime? endTime, int cityid, out int cnt)
        {
            BalanceAccountManager balanceAccountManager = new BalanceAccountManager();
            return balanceAccountManager.QueryBusinessPay(page, shopId, status, accountId, beginTime, endTime, cityid, out cnt);
        }

        /// <summary>
        /// 查询某门店对应的平账记录
        /// </summary>
        /// <param name="shopId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public List<BalanceAccountDetail> QueryBusinessPay(int shopId, int status, long? accountId, DateTime? beginTime, DateTime? endTime)
        {
            BalanceAccountManager balanceAccountManager = new BalanceAccountManager();
            return balanceAccountManager.QueryBusinessPay(shopId, status, accountId, beginTime, endTime);
        }

        /// <summary>
        /// 商户充值/扣款
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="shopId"></param>
        /// <param name="accountMoney"></param>
        /// <param name="employeeID"></param>
        /// <param name="remarkDes"></param>
        /// <param name="accountId"></param>
        /// <returns></returns>
        public static bool MerchantCheckout(MoneyMerchantAccountDetail detail, int employeeID, string IPAddress, ref long accountId)
        {
            bool result = false;
            string flowNumber = SybMoneyOperate.CreateMerchantFlowNumber(detail.shopId);
            double remainMoney = new SybMoneyMerchantManager().GetShopInfoRemainMoney(detail.shopId);
            MoneyMerchantAccountDetail data = new MoneyMerchantAccountDetail()
            {
                companyId = detail.companyId,
                shopId = detail.shopId,
                accountMoney = Common.ToDouble(detail.accountMoney),
                remainMoney = Common.ToDouble(remainMoney + detail.accountMoney),
                flowNumber = flowNumber,
                accountType = (int)AccountType.MERCHANT_CHECKOUT,
                inoutcomeType = detail.accountMoney > 0 ? (int)InoutcomeType.IN : (int)InoutcomeType.OUT,
                accountTypeConnId = "",
                operUser = employeeID.ToString(),
                operTime = DateTime.Now,
                remark = detail.remark,
                status = (int)BalanceAccountStatus.wait_for_check
            };
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    accountId = MoneyMerchantAccountDetailManager.InsertBalanceAccount(data);
                    bool updateShopMoney = true;
                    if (detail.accountMoney < 0)
                    {
                        BalanceAccountManager balanceAccountManager = new BalanceAccountManager();
                        BalanceAccountShopMoney shopMoney = new BalanceAccountShopMoney()
                        {
                            shopId = detail.shopId,
                            remainMoney = 0,
                            amountFrozen = Math.Abs(detail.accountMoney),//冻结金额一定是正数
                            payAlipayAmount = 0,
                            payWechatPayAmount = 0,
                            payRedEnvelopeAmount = 0,
                            payFoodCouponAmount = 0,
                            payCommissionAmount = 0
                        };
                        updateShopMoney = balanceAccountManager.UpdateShopMoney(shopMoney);//申请平账，更新冻结金额
                    }

                    if (accountId > 0 && updateShopMoney)
                    {
                        ts.Complete();
                        result = true;
                    }
                    else
                    {
                        result = false;
                    }
                }
                if (result)
                {
                    StoresMoneyLogManager logManager = new StoresMoneyLogManager();
                    StoresMoneyLog log = new StoresMoneyLog()
                    {
                        AddIP = IPAddress,
                        AddTime = DateTime.Now,
                        AddUser = detail.operUser,
                        ShopInfo_ShopID = detail.shopId,
                        BatchMoneyApply_Id = 0,
                        BatchMoneyApplyDetail_Id = "",
                        MoneyMerchantAccountDetail_AccountId = accountId,
                        Money = detail.accountMoney,
                        Content = "行政申请单据" + accountId
                    };
                    logManager.insertLog(log);
                }
            }
            catch { }
            return result;
        }

        /// <summary>
        /// 查询某门店的未结款金额及冻结金额
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public BalanceAccountShopMoney QueryShopMoney(int shopId)
        {
            BalanceAccountManager balanceAccountManager = new BalanceAccountManager();
            return balanceAccountManager.QueryShopMoney(shopId);
        }

        /// <summary>
        /// 更新平账单据信息
        /// </summary>
        /// <param name="accountId">单号</param>
        /// <param name="status">状态</param>
        /// <returns></returns>
        public bool UpdateMoneyMerchantAccountDetail(MoneyMerchantAccountDetail accountDetail, bool updateRemainMoney = false, double remainMoney = 0)
        {
            MoneyMerchantAccountDetailManager accountDetailManager = new MoneyMerchantAccountDetailManager();
            return accountDetailManager.UpdateMoneyMerchantAccountDetail(accountDetail, updateRemainMoney, remainMoney);
        }

        /// <summary>
        /// 根据单号查询平账信息
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        public MoneyMerchantAccountDetail QueryMoneyMerchantAccountDetail(long accountId)
        {
            MoneyMerchantAccountDetailManager accountDetailManager = new MoneyMerchantAccountDetailManager();
            return accountDetailManager.QueryMoneyMerchantAccountDetail(accountId);
        }

        /// <summary>
        /// 平账管理出纳确认单据
        /// </summary>
        /// <param name="accountDetail"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        public bool CheckBalanceAmount(MoneyMerchantAccountDetail accountDetail, StoresMoneyLog log)
        {
            //更改状态
            bool updateStatus = UpdateMoneyMerchantAccountDetail(accountDetail);
            if (updateStatus)
            {
                StoresMoneyLogManager logManager = new StoresMoneyLogManager();
                logManager.insertLog(log);
            }
            return updateStatus;
        }

        /// <summary>
        /// 平账管理财务主管平账
        /// </summary>
        /// <param name="accountDetail"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        public bool ConfirmBalanceAmount(MoneyMerchantAccountDetail accountDetail, StoresMoneyLog log)
        {
            BalanceAccountManager balanceAccountManager = new BalanceAccountManager();
            bool updateStatus = false;
            bool updateShopMoney = false;
            BalanceAccountShopRemainMoney shopRemainMoney = balanceAccountManager.QueryShopRemainMoney(accountDetail.shopId);

            //BalanceAccountShopMoney shopMoney = mathAmount(shopRemainMoney, accountDetail.accountMoney);

            BalanceAccountShopMoney shopMoney = new BalanceAccountShopMoney();
            using (TransactionScope ts = new TransactionScope())
            {
                //更改状态
                updateStatus = UpdateMoneyMerchantAccountDetail(accountDetail, true, shopRemainMoney.remainMoney + accountDetail.accountMoney);

                //变更商户余额，解冻金额
                shopMoney.shopId = accountDetail.shopId;
                shopMoney.remainMoney = accountDetail.accountMoney;

                if (accountDetail.accountMoney < 0)//只有在扣款时，才更改冻结金额
                {
                    shopMoney.amountFrozen = accountDetail.accountMoney;
                }
                else
                {
                    shopMoney.amountFrozen = 0;
                }
                updateShopMoney = balanceAccountManager.UpdateShopMoney(shopMoney);//财务主管平账，更新总余额及冻结金额
                if (updateStatus && updateShopMoney)
                {
                    ts.Complete();
                }
            }
            if (updateStatus && updateShopMoney)
            {
                StoresMoneyLogManager logManager = new StoresMoneyLogManager();
                logManager.insertLog(log);
            }
            return updateStatus && updateShopMoney;
        }

        private BalanceAccountShopMoney mathAmount(BalanceAccountShopRemainMoney shopRemainMoney, double accountMoney)
        {
            BalanceAccountShopMoney payMoney = new BalanceAccountShopMoney();
            if (shopRemainMoney.remainMoney == 0 && accountMoney > 0)
            {
                payMoney.payAlipayAmount = accountMoney;
            }
            else
            {
                //比例
                double ratio = Math.Round(Math.Abs(accountMoney) / shopRemainMoney.remainMoney, 8);

                //支付宝应扣金额
                payMoney.payAlipayAmount = Math.Round(shopRemainMoney.remainAlipayAmount * ratio, 2);
                //微信应扣金额
                payMoney.payWechatPayAmount = Math.Round(shopRemainMoney.remainWechatPayAmount * ratio, 2);
                //红包应扣金额
                payMoney.payRedEnvelopeAmount = Math.Round(shopRemainMoney.remainRedEnvelopeAmount * ratio, 2);

                //粮票应扣金额
                payMoney.payFoodCouponAmount = Math.Round(Math.Abs(accountMoney) - payMoney.payAlipayAmount - payMoney.payWechatPayAmount - payMoney.payRedEnvelopeAmount, 2);

                if (accountMoney < 0)
                {
                    payMoney.payCommissionAmount = Math.Round(shopRemainMoney.payCommissionAmount * ratio, 2);
                }

                double error = Math.Abs(payMoney.payFoodCouponAmount) - shopRemainMoney.remainFoodCouponAmount;//粮票不够扣

                if (accountMoney < 0 && error > 0.001)//是扣款 且 粮票不够扣
                {
                    //看红包够不够扣
                    if (shopRemainMoney.remainRedEnvelopeAmount - Math.Abs(payMoney.payRedEnvelopeAmount) - error > 0.001)
                    {
                        payMoney.payRedEnvelopeAmount += error;
                    }
                    else if (shopRemainMoney.remainWechatPayAmount - Math.Abs(payMoney.payWechatPayAmount) - error > 0.001)//看微信够不够扣
                    {
                        payMoney.payWechatPayAmount += error;
                    }
                    else if (shopRemainMoney.remainAlipayAmount - Math.Abs(payMoney.payAlipayAmount) - error > 0.001)//看支付宝够不够扣
                    {
                        payMoney.payAlipayAmount += error;
                    }
                    else//如果都不够扣，粮票就抹去多出的小数
                    {
                        if (accountMoney > 0)
                        {
                            payMoney.payFoodCouponAmount = payMoney.payFoodCouponAmount - error;
                        }
                        else
                        {
                            payMoney.payFoodCouponAmount = payMoney.payFoodCouponAmount + error;
                        }
                    }
                }
            }
            if (accountMoney < 0)
            {
                payMoney.payAlipayAmount = -payMoney.payAlipayAmount;
                payMoney.payWechatPayAmount = -payMoney.payWechatPayAmount;
                payMoney.payRedEnvelopeAmount = -payMoney.payRedEnvelopeAmount;
                payMoney.payFoodCouponAmount = -payMoney.payFoodCouponAmount;
                payMoney.payCommissionAmount = -payMoney.payCommissionAmount;
            }
            return payMoney;
        }

        /// <summary>
        /// 银企直联出纳/财务撤回单据
        /// </summary>
        /// <param name="accountDetail"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        public bool RejectBalanceAmount(MoneyMerchantAccountDetail accountDetail, StoresMoneyLog log)
        {
            bool updateShopMoney = true;
            bool updateStatus = false;
            BalanceAccountManager balanceAccountManager = new BalanceAccountManager();
            BalanceAccountShopMoney shopMoney = new BalanceAccountShopMoney();


            using (TransactionScope ts = new TransactionScope())
            {
                if (accountDetail.accountMoney < 0)//如果是扣款被撤回，要消掉冻结金额
                {
                    shopMoney = new BalanceAccountShopMoney()
                    {
                        shopId = accountDetail.shopId,
                        remainMoney = 0,
                        amountFrozen = accountDetail.accountMoney
                        //,
                        //payAlipayAmount = 0,
                        //payWechatPayAmount = 0,
                        //payRedEnvelopeAmount = 0,
                        //payFoodCouponAmount = 0
                    };
                    updateShopMoney = balanceAccountManager.UpdateShopMoney(shopMoney);//撤回单据，更新冻结金额
                }
                updateStatus = UpdateMoneyMerchantAccountDetail(accountDetail);//更新平账单据状态
                if (updateShopMoney && updateStatus)
                {
                    ts.Complete();
                }
            }
            if (updateShopMoney && updateStatus)
            {
                StoresMoneyLogManager logManager = new StoresMoneyLogManager();
                logManager.insertLog(log);
            }
            return updateShopMoney && updateStatus;
        }

        /// <summary>
        /// 检查某员工是否有出纳打款权限
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public bool IsHaveCheckAuthority(int employeeId)
        {
            BalanceAccountManager balanceAccountManager = new BalanceAccountManager();
            return balanceAccountManager.CheckAuthority(employeeId, "出纳（商户打款）");
        }

        /// <summary>
        /// 检查某员工是否有财务打款权限
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public bool IsHaveConfirmAuthority(int employeeId)
        {
            BalanceAccountManager balanceAccountManager = new BalanceAccountManager();
            return balanceAccountManager.CheckAuthority(employeeId, "财务主管（商户打款）");
        }

        /// <summary>
        /// 检查门店是否有申请未处理的打款单
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public bool CheckHaveUndoApply(int shopId)
        {
            BalanceAccountManager balanceAccountManager = new BalanceAccountManager();
            return balanceAccountManager.CheckHaveUndoApply(shopId);
        }
    }
}
