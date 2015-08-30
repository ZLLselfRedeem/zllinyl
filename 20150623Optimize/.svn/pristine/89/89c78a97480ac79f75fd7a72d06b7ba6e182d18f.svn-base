using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL;
using System.Transactions;
using VA.CacheLogic.OrderClient;

namespace VAGastronomistMobileApp.WebPageDll
{
    public class MealOperate
    {
        private readonly MealManager manager = new MealManager();
        /// <summary>
        /// 处理用户未在规定时间内支付年夜饭点单服务
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool UpdateNotPayMealOrderOperate(double value)
        {
            return manager.UpdateNotPayMealOrderManager(value);
        }
        /// <summary>
        /// 处理用户未在规定时间内支付，修改套餐剩余份数
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool UpdateMealScheduleCanBuy(int mealScheduleID, int count)
        {
            return manager.UpdateMealScheduleCanBuy(mealScheduleID, count);
        }

        public DataTable SelectBackPreOrder(double value)
        {
            return manager.SelectBackPreOrder(value);
        }
        public Meal GetEntityByID(int mealID)
        {
            return manager.GetEntityByID(mealID);
        }

        public bool AddEntity(Meal entity)
        {
            return manager.AddEntity(entity);
        }
        public bool UpdateEntity(Meal entity)
        {
            return manager.UpdateEntity(entity);
        }
        public int GetCountByQuery(MealQueryObject queryObject)
        {
            return manager.GetCountByQuery(queryObject);
        }
        public List<Meal> GetListByQuery(MealQueryObject queryObject)
        {
            return manager.GetListByQuery(queryObject);
        }

        /// <summary>
        /// 检查客户端的cookie是否与mobile对应的cookie一致，如果不一致，说明用户未登陆
        /// </summary>
        /// <param name="mobile"></param>
        /// <param name="cookie"></param>
        /// <returns></returns>
        public static bool CheckLogin(string mobile, string cookie)
        {
            CustomerManager customerManager = new CustomerManager();
            string realCookie = customerManager.SelectCustomerCookieByMobile(mobile);
            if (cookie.Equals(realCookie))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 获取活动信息
        /// </summary>
        /// <param name="cityId">城市ID</param>
        /// <param name="tagId">商圈ID</param>
        /// <param name="mobile">客户手机号码</param>
        /// <param name="pageIndex">第几页</param>
        /// <param name="pageSize">多少条/每页</param>
        /// <returns></returns>
        public static string GetMealActivityList(int cityId, int tagId, string mobile, int pageIndex, int pageSize)
        {
            string result = "";
            MealCacheLogic mealCacheLogic = new MealCacheLogic();
            MealActivity mealActivity = mealCacheLogic.GetMealActivityRuleOfCache();

            List<MealOrder> mealOrder = MealManager.SelectMealOrder(mobile);
            if (mealOrder != null && mealOrder.Any())
            {
                double period = mealCacheLogic.GetMealValidPeriodOfCache();
                foreach (MealOrder order in mealOrder)
                {
                    order.validPayTime = Convert.ToDateTime("1970-1-1");
                    switch ((int)order.status)
                    {
                        case 101:
                            order.status = OrderStatus.未付款;
                            order.validPayTime = order.preOrderTime.AddMinutes(period);
                            break;
                        case 102:
                            order.status = OrderStatus.待确认;
                            break;
                        case 103:
                            order.status = OrderStatus.已确认;
                            break;
                        case 105:
                            order.status = OrderStatus.已退款;
                            break;
                        case 107:
                            order.status = OrderStatus.退款中;
                            break;
                        default:
                            order.status = OrderStatus.待确认;
                            break;
                    }
                }
                mealActivity.customerMealOrder = mealOrder;
            }
            List<ShopTag> shopTag = mealCacheLogic.GetMealShopTagOfCache(cityId);
            if (shopTag != null && shopTag.Count > 1)
            {
                mealActivity.shopTag = shopTag;
            }
            bool isMore = false;
            List<MealShopList> mealShopList = MealManager.SelectMealShopList(new VAGastronomistMobileApp.SQLServerDAL.Persistence.Infrastructure.Page(pageIndex, pageSize), cityId, tagId, out isMore);
            if (mealShopList != null && mealShopList.Any())
            {
                mealActivity.isMore = isMore;
                int[] arrShopId = new int[mealShopList.Count];

                for (int i = 0; i < mealShopList.Count; i++)
                {
                    arrShopId[i] = mealShopList[i].shopId;
                }

                //此分页中所有店铺的套餐
                List<MealList> mealList = MealManager.SelectMealList(arrShopId);
                List<int> MealID = MealManager.SelectMealStatus();
                if (mealList != null && mealList.Any() && MealID != null && MealID.Any())
                {
                    //找出每个店铺对应的套餐
                    foreach (MealShopList meal in mealShopList)
                    {
                        meal.mealList = mealList.FindAll(p => p.shopId == meal.shopId);

                        foreach (MealList list in meal.mealList)
                        {
                            List<MealListStatus> mealListStatus = MealManager.SelectMealListStatus(list.mealId);
                            MealListStatus status = new MealListStatus();

                            if (mealListStatus != null && mealListStatus.Any())
                            {
                                status = mealListStatus.Find(p => p.mealId == list.mealId);
                                if (status != null && (status.remainCount == 0))
                                {
                                    list.isSoldOut = true;
                                }
                                else
                                {
                                    list.isSoldOut = false;
                                }
                            }
                            list.imageURL = WebConfig.CdnDomain + WebConfig.ImagePath + list.imageURL + "@250w_200h";
                            if (!MealID.Contains(list.mealId))
                            {
                                list.isSoldOut = true;
                            }
                        }
                    }
                }
                mealActivity.mealShopList = mealShopList;
            }
            if (mealActivity != null)
            {
                result = SysJson.JsonSerializer(mealActivity);
            }
            return result;
        }

        /// <summary>
        /// 查询套餐及其排期详情
        /// </summary>
        /// <param name="mealId"></param>
        /// <returns></returns>
        public static string GetMealDetail(int mealId)
        {
            MealCacheLogic mealCacheLogic = new MealCacheLogic();

            MealDetail mealDetail = new MealDetail();
            mealDetail.mealList = mealCacheLogic.GetMealListOfCache(mealId);
            mealDetail.mealList.imageURL = WebConfig.CdnDomain + WebConfig.ImagePath + mealDetail.mealList.imageURL + "@640w_512h";
            List<MealSchedule> mealScheduleList = MealScheduleManager.SelectMealSchedule(mealId);
            if (mealScheduleList != null && mealScheduleList.Any())
            {
                List<MealScheduleCount> mealScheduleCount = MealScheduleManager.SelectMealScheduleCount(mealDetail.mealList.shopId);
                if (mealScheduleCount != null && mealScheduleCount.Any())
                {
                    foreach (MealSchedule meal in mealScheduleList)
                    {
                        meal.lunarCalendar = Common.GetChineseDateTimeForMeal(meal.DinnerTime);
                        MealScheduleCount schedule = mealScheduleCount.Find(p => p.mealId == meal.MealID && p.dinnerTime == meal.DinnerTime.ToString("yyyyMMdd"));
                        if (schedule.remainCount == 0)//该套餐所属
                        {
                            meal.isSoldOut = true;
                        }
                        else
                        {
                            meal.isSoldOut = false;
                        }
                        if (meal.ValidTo < DateTime.Now || (meal.TotalCount - meal.SoldCount < 1))//排期时间过了，或者真的卖完了
                        {
                            meal.IsActive = 0;//该排期已经失效，要盖失效戳
                        }
                        else
                        {
                            meal.IsActive = 1;//活动还在继续
                        }
                    }
                }
            }
            mealDetail.mealSchedule = mealScheduleList;
            string result = SysJson.JsonSerializer(mealDetail);
            return result;
        }

        /// <summary>
        /// 前端页面查询某个套餐排期的剩余份数
        /// </summary>
        /// <param name="mealScheduleId"></param>
        /// <returns></returns>
        //public static string GetRemainCount(int mealScheduleId)
        //{
        //    string remainCount = MealScheduleManager.SelectRemain(mealScheduleId);
        //    return "{\"remainCount\": \"" + remainCount + "\"}";
        //}

        public static string Order(string mobilePhoneNumber, int mealScheduleId)
        {
            string result = "";
            MealResult mealResult = new MealResult();
            DataTable dt = MealManager.SelectMealConnShop(mealScheduleId);
            if (dt != null && dt.Rows.Count > 0)
            {
                int shopId = Common.ToInt32(dt.Rows[0]["ShopID"]);
                if (Common.ToInt32(dt.Rows[0]["isHandle"]) != (int)VAShopHandleStatus.SHOP_Pass)//校验门店是否上线
                {
                    mealResult.error = (int)VAResult.VA_FAILED_SHOP_NOT_ONLINE;
                    mealResult.msg = "门店未上线";
                    result = SysJson.JsonSerializer(mealResult);
                    return result;
                }

                bool isSupportPayment = Common.ToBool(dt.Rows[0]["isSupportPayment"]);
                if (isSupportPayment != true)//支持付款
                {
                    mealResult.error = (int)VAResult.VA_SHOP_NOT_SUPPOORT_PAYMENY;//该门店不支持付款
                    mealResult.msg = Common.ToString(dt.Rows[0]["notPaymentReason"]);
                    result = SysJson.JsonSerializer(mealResult);
                    return result;
                }
                DataTable dtCustomer = CustomerManager.SelectCustomerForMeal(mobilePhoneNumber);
                if (dtCustomer != null && dtCustomer.Rows.Count > 0)
                {
                    long customerId = Common.ToInt64(dtCustomer.Rows[0]["CustomerID"]);//用户编号

                    PreOrder19dianInfo preOrder19dian = new PreOrder19dianInfo()
                    {
                        customerId = customerId,
                        shopId = shopId,
                        companyId = Common.ToInt32(dt.Rows[0]["companyID"]),
                        preOrderTime = DateTime.Now,
                        menuId = Common.ToInt32(dt.Rows[0]["MenuID"]),
                        status = VAPreorderStatus.Uploaded,
                        customerUUID = Common.ToString(dtCustomer.Rows[0]["uuid"]),
                        preOrderSum = Common.ToDouble(dt.Rows[0]["Price"]),//直接支付金额(缺少安全校验 >0)
                        orderInJson = "",
                        sundryJson = "",
                        deskNumber = "",// request.deskNumber,
                        appType = Common.ToInt32(dtCustomer.Rows[0]["appType"]),
                        appBuild = "",//request.clientBuild
                    };

                    PreOrder19dianOperate preOrderOperate = new PreOrder19dianOperate();

                    if (Common.ToInt32(dt.Rows[0]["remainCount"]) > 0)
                    {
                        using (TransactionScope ts = new TransactionScope())
                        {
                            //1.减套餐份数
                            bool minus = MealScheduleManager.AddSoldOutCount(mealScheduleId);
                            //1.新增点单
                            //long preOrder19dianId = preOrderMan.InsertPreOrder19dian(preOrder19dian);
                            long preOrder19dianId = preOrderOperate.AddPreOrder19Dian(preOrder19dian);
                            //3.新增关联表
                            int connId = 0;
                            if (preOrder19dianId > 0)
                            {
                                connId = MealManager.InsertIntoMealConnPreOrder(preOrder19dianId, mealScheduleId);
                            }

                            double period = SystemConfigManager.GetVAMealValidPeriod();

                            if (minus && preOrder19dianId > 0 && connId > 0)
                            {
                                ts.Complete();
                                DateTime validPayTime = DateTime.Now.AddMinutes(period);
                                string message = "{\"error\":\"0\",\"validPayTime\": \"" + validPayTime.ToString("HH:mm") + "\",\"msg\":\"下单成功\"}";
                                result = message;
                            }
                            else
                            {
                                mealResult.error = -3;
                                mealResult.msg = "下单失败";
                                result = SysJson.JsonSerializer(mealResult);
                            }
                        }
                    }
                    else
                    {
                        mealResult.error = -4;
                        mealResult.msg = "套餐已被抢光";
                        result = SysJson.JsonSerializer(mealResult);
                    }
                }
            }
            else
            {
                mealResult.error = -4;
                mealResult.msg = "套餐已被抢光";
                result = SysJson.JsonSerializer(mealResult);
            }
            return result;
        }

        /// <summary>
        /// 年夜饭订单报表
        /// </summary>
        /// <returns></returns>
        public List<MealOrderReport> SelectMealOrderReport()
        {
            return manager.SelectMealOrderReport();
        }
        /// <summary>
        /// 查询当前用户所有年夜饭点单编号
        /// </summary>
        /// <param name="customerId">用户编号</param>
        /// <returns></returns>
        public List<long> GetMealOrderIds(long customerId)
        {
            return manager.GetMealOrderIds(customerId);
        }


        public DataTable GetMealTableByQuery(MealQueryObject queryObject, int pageIndex, int pageSize)
        {
            return manager.GetMealTableByQuery(queryObject, pageIndex, pageSize);
        }
        public int GetMealTableCountByQuery(MealQueryObject queryObject)
        {
            return manager.GetMealTableCountByQuery(queryObject);

        }
        public DataTable GetMealTableByQuery(MealQueryObject queryObject)
        {
            return manager.GetMealTableByQuery(queryObject);
        }
    }
}
