using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.SQLServerDAL;
using VAGastronomistMobileApp.Model;
using System.Web;
using System.Transactions;


namespace VAGastronomistMobileApp.WebPageDll
{
    /// <summary>
    /// 功能描述:收银宝账户体系类-公司部分
    /// 创建标识:罗国华 20131120
    /// </summary>
    public class SybMoneyViewallocOperate
    {
        /// <summary>
        /// viewalloc公司余额变更
        /// </summary>
        /// <param name="refundAccount">余额变换值，有正负之分</param>
        /// <returns></returns>
        public static bool UpdateViewallocInfo(double refundAccount)
        {
            Money19dianDetailManager money19dianDetailMan = new Money19dianDetailManager(); //viewalloc公司账户余额
            refundAccount = Common.ToDouble(refundAccount);
            return money19dianDetailMan.UpdateViewallocInfo(refundAccount);
        }
        /// <summary>
        /// 查询公司的当前时间的余额（只有一条记录）
        /// </summary>
        /// <returns></returns>
        public static double GetViewAllocRemainMoney()
        {
            return Money19dianDetailManager.GetViewAllocRemainMoney();
        }
        /// <summary>
        /// 插入公司资金变动详情表
        /// </summary>
        /// <param name="moneyViewallocAccountDetailModel"></param>
        /// <returns></returns>
        public static long InsertMoneyViewallocAccountDetail(MoneyViewallocAccountDetail moneyViewallocAccountDetailModel)
        {
            moneyViewallocAccountDetailModel.accountMoney = Common.ToDouble(moneyViewallocAccountDetailModel.accountMoney);
            return Money19dianDetailManager.InsertMoneyViewallocAccountDetail(moneyViewallocAccountDetailModel);
        }
        /// <summary>
        /// viewalloc公司余额变更，及变更明细记录
        /// </summary>
        /// <param name="moneyViewallocAccountDetailModel">MoneyViewallocAccountDetail model</param>
        /// <returns></returns>
        public static bool ViewAllocAccountBalanceChanges(MoneyViewallocAccountDetail moneyViewallocAccountDetailModel)
        {
            using (TransactionScope tScope = new TransactionScope())
            {
                moneyViewallocAccountDetailModel.accountMoney = Common.ToDouble(moneyViewallocAccountDetailModel.accountMoney);
                //公司账户余额
                //moneyViewallocAccountDetailModel.accountMoney，原始为+—值
                bool result1 = SybMoneyViewallocOperate.UpdateViewallocInfo(moneyViewallocAccountDetailModel.accountMoney);//viewalloc公司账户余额变更（加减）
                long result2 = SybMoneyViewallocOperate.InsertMoneyViewallocAccountDetail(moneyViewallocAccountDetailModel);//viewalloc插入余额变更记录表
                if (result1 && result2 > 0)
                {
                    tScope.Complete();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
