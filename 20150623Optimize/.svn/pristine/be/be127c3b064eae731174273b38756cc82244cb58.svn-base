using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL;
using VA.CacheLogic.OrderClient;

namespace VAGastronomistMobileApp.WebPageDll
{
    public class AwardConnPreOrderOperate
    {
        private readonly AwardConnPreOrderManager manager = new AwardConnPreOrderManager();

        /// <summary>
        /// 获取商家对应的奖品列表
        /// </summary>
        /// <param name="shopID"></param>
        /// <param name="isAvoidQueue">是否开启免排队</param>
        /// <returns></returns>
        public List<AwardConnPreOrder> GetAwardConnPreOrderList(int shopID, bool isAvoidQueue)
        {
            return manager.GetAwardConnPreOrderList(shopID, isAvoidQueue);
        }

        /// <summary>
        /// 获取最近获取的免排队实体
        /// </summary>
        /// <param name="shopID"></param>
        /// <returns></returns>
        public AwardConnPreOrder GetAwardConnPreOrder(int shopID)
        {
            return manager.GetAwardConnPreOrder(shopID);
        }

        /// <summary>
        /// 根据用户退当日退款次数，检查其是否能抽奖
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public bool CheckCusCanLotteryByRefundCnt( string uuid,string mobilePhone)
        {
            DeviceOperate deviceOperate = new DeviceOperate();
            long deviceId = deviceOperate.SelectDeviceIdByUUID(uuid);

            CustomerConnDeviceOperate connOperate = new CustomerConnDeviceOperate();
            List<long> customerIdList = connOperate.SelectCustomerId(deviceId);
            long[] customerIds = customerIdList.ToArray();
           
            //检查该设备今日所有的用户退款单据个数
            int refundCnt = manager.CheckCusRefundCnt(customerIds);

            AwardCacheLogic cacheLogic = new AwardCacheLogic();
            int refundSetCnt = Common.ToInt32(cacheLogic.GetAwardConfig("CustomerRefundCountOneDay", ""));
            if (refundCnt < refundSetCnt)//未超过设置的退款次数
            {
                return true;
            }
            else
            {
                return false;
            }            
        }

        /// <summary>
        /// 根据用户在门店的未消费订单个数，检查用户是否能抽奖
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public bool CheckCusCanLotteryByUnConfirmedOrderCntOfShop(long customerId, string uuid, int shopId)
        {
            DeviceOperate deviceOperate = new DeviceOperate();
            long deviceId = deviceOperate.SelectDeviceIdByUUID(uuid);

            CustomerConnDeviceOperate connOperate = new CustomerConnDeviceOperate();
            
            //该用户登录过的所有设备，对应的用户
            List<long> customerIdList = connOperate.SelectCustomerId(deviceId);
            long[] customerIds = customerIdList.ToArray();
            
            int unConfirmedOrderCnt = manager.CheckCusUnConfirmedOrderCntOfShop(customerIds, shopId);
            AwardCacheLogic cacheLogic = new AwardCacheLogic();
            int unConfirmedSetCnt = Common.ToInt32(cacheLogic.GetAwardConfig("CustomerOwnUnConfirmedOrderCountOfShop", ""));
            if (unConfirmedOrderCnt < unConfirmedSetCnt)//未超过设置的未消费订单个数
            {
                List<string> accounts = manager.GetPayAccount(customerId);

                if (accounts != null && accounts.Any())
                {
                    string[] strAccounts = accounts.ToArray();

                    unConfirmedOrderCnt = manager.GetUnConfirmedOrderCountOfShop(strAccounts, shopId);

                    if (unConfirmedOrderCnt < unConfirmedSetCnt)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
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
        /// 根据用户在平台的未消费订单个数，检查用户是否能继续支付
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public bool CheckCusCanLotteryByUnConfirmedOrderCntOfVA(long customerId, string uuid)
        {
            DeviceOperate deviceOperate = new DeviceOperate();
            long deviceId = deviceOperate.SelectDeviceIdByUUID(uuid);

            CustomerConnDeviceOperate connOperate = new CustomerConnDeviceOperate();

            //该用户登录过的所有设备，对应的用户
            List<long> customerIdList = connOperate.SelectCustomerId(deviceId);
            long[] customerIds = customerIdList.ToArray();
            
            int unConfirmedOrderCnt = manager.CheckCusUnConfirmedOrderCntOfVA(customerIds);
            AwardCacheLogic cacheLogic = new AwardCacheLogic();
            int unConfirmedSetCnt = Common.ToInt32(cacheLogic.GetAwardConfig("CustomerOwnUnConfirmedOrderCountOfVA", ""));
            if (unConfirmedOrderCnt < unConfirmedSetCnt)//未超过平台设置的未消费订单个数
            {
                List<string> accounts = manager.GetPayAccount(customerId);

                string[] strAccounts = accounts.ToArray();

                unConfirmedOrderCnt = manager.GetUnConfirmedOrderCountOfVA(strAccounts);

                if (unConfirmedOrderCnt < unConfirmedSetCnt)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 获取商家对应的奖品统计
        /// </summary>
        /// <param name="shopID"></param>
        /// <returns></returns>
        public DataTable GetAwardTotalList(int shopID)
        {
            return manager.GetAwardTotalList(shopID);
        }

        /// <summary>
        /// 获取商家月奖品统计
        /// </summary>
        /// <param name="shopID"></param>
        /// <returns></returns>
        public DataTable GetAwardTotalMonthList(int shopID)
        {
            return manager.GetAwardTotalMonthList(shopID);
        }

        public bool InsertAwardConnPreOrder(AwardConnPreOrder awardConnPreOrder)
        {
            return manager.InsertAwardConnPreOrder(awardConnPreOrder);
        }


        /// <summary>
        /// 查询指定商户指定类型的奖品
        /// </summary>
        /// <param name="shopID"></param>
        /// <param name="awardType"></param>
        /// <returns></returns>
        public List<AwardConnPreOrder> GetAwardConnPreOrderList(int shopID, AwardType awardType)
        {
            return manager.GetAwardConnPreOrderList(shopID, awardType);
        }


        /// <summary>
        /// 查询某奖项发了多少个
        /// </summary>
        /// <param name="awardId"></param>
        /// <returns></returns>
        public int SelectAwardCount(Guid awardId)
        {
            return manager.SelectAwardCount(awardId);
        }

        /// <summary>
        /// 统计第三方奖品领取数量
        /// </summary>
        /// <returns></returns>
        public DataTable SelectThirdAwardConsume()
        {
            return manager.SelectThirdAwardConsume();
        }

        /// <summary>
        /// 统计菜品奖品发放数量
        /// </summary>
        /// <returns></returns>
        public DataTable SelectDishAwardConsume(Guid[] awardIds)
        {
            return manager.SelectDishAwardConsume(awardIds);
        }

        /// <summary>
        /// 根据点单Id查询中奖信息
        /// </summary>
        /// <param name="preOrder19dianId"></param>
        /// <returns></returns>
        public AwardConnPreOrder SelectAwardConnPreOrderByOrderId(long preOrder19dianId)
        {
            return manager.SelectAwardConnPreOrderByOrderId(preOrder19dianId);
        }

        /// <summary>
        /// 查询中奖的订单
        /// </summary>
        /// <param name="preOrder19dianIds"></param>
        /// <returns></returns>
        public List<long> SelectAwardPreOrderByOrderIds(long[] preOrder19dianIds)
        {
            return manager.SelectAwardPreOrderByOrderIds(preOrder19dianIds);
        }

        /// <summary>
        /// 用户全额退款，作废用户中奖的红包
        /// </summary>
        /// <param name="preOrder19dianId"></param>
        /// <param name="mobilePhoneNumber"></param>
        /// <returns></returns>
        public bool CancelAwardRedEnvelope(long preOrder19dianId)
        {
            //1、检查订单是否有中奖红包
            AwardConnPreOrderOperate awardOperate = new AwardConnPreOrderOperate();
            ViewAllocAwardOperate vaAwardOperate = new ViewAllocAwardOperate();

            AwardConnPreOrder awardOrder = awardOperate.SelectAwardConnPreOrderByOrderId(preOrder19dianId);
            if (awardOrder != null && awardOrder.Type == AwardType.PresentRedEnvelope)
            {
                //2.将红包作废（改状态，改有效期）
                RedEnvelopeOperate redEnvelopeOperate = new RedEnvelopeOperate();
                return redEnvelopeOperate.CancelAwardRedEnvelope(awardOrder.redEnvelopeId);
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 用户点单对账后，将中奖红包生效
        /// </summary>
        /// <param name="preOrder19dianId"></param>
        /// <returns></returns>
        public bool EffectAwardRedEnvelope(long preOrder19dianId)
        {
            //1、检查订单是否有中奖红包
            AwardConnPreOrderOperate awardOperate = new AwardConnPreOrderOperate();
            ViewAllocAwardOperate vaAwardOperate = new ViewAllocAwardOperate();

            AwardConnPreOrder awardOrder = awardOperate.SelectAwardConnPreOrderByOrderId(preOrder19dianId);
            if (awardOrder != null && awardOrder.Type == AwardType.PresentRedEnvelope)
            {
                //2.抓出奖品信息
                ViewAllocAward vaAward = vaAwardOperate.SelectVAAward(awardOrder.AwardId);

                //3.抓出红包信息，提取过期时间
                ActivityOperate activityOperate = new ActivityOperate();
                Activity activity = activityOperate.QueryActivity(vaAward.ActivityId);

                DateTime expireTime = DateTime.Now;

                switch (activity.expirationTimeRule)
                {
                    case ExpirationTimeRule.postpone:

                        if (activity.beginTime > DateTime.Now)
                        {
                            expireTime = Common.ToDateTime(activity.beginTime.AddDays(activity.ruleValue).ToString("yyyy/MM/dd 23:59:59"));
                        }
                        else
                        {
                            expireTime = Common.ToDateTime(DateTime.Now.AddDays(activity.ruleValue).ToString("yyyy/MM/dd 23:59:59"));
                        }
                        break;
                    case ExpirationTimeRule.unify:
                        expireTime = activity.redEnvelopeEffectiveEndTime;
                        break;
                }

                //3.将红包生效（改有效期）

                RedEnvelopeOperate redEnvelopeOperate = new RedEnvelopeOperate();
                return redEnvelopeOperate.EffectAwardRedEnvelope(awardOrder.redEnvelopeId, expireTime);
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 查询某用户当日支付的第三方账号对应的退款订单个数
        /// </summary>
        /// <param name="mobilePhone"></param>
        /// <returns></returns>
        public int selectRefundOrderCount(string mobilePhone)
        {
            List<string> payAccounts = manager.selectPayAccountOfCustomer(mobilePhone);
            if (payAccounts != null && payAccounts.Any())
            {
                return manager.SelectRefundOrderCount(payAccounts.ToArray());
            }
            else
            {
                return 0;
            }
        }
    }
}
