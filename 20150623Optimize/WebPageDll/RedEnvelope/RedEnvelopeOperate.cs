﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL;
using System.Data;
using System.Transactions;
using VA.CacheLogic;
using VA.CacheLogic.OrderClient;

namespace VAGastronomistMobileApp.WebPageDll
{
    public class RedEnvelopeOperate
    {
        RedEnvelopeManager redEnvelopeManager = new RedEnvelopeManager();

        /// <summary>
        /// 新建宝箱时
        /// </summary>
        /// <param name="redEnvelope"></param>
        /// <returns></returns>
        public long InsertRedEnvelope(RedEnvelope redEnvelope)
        {
            return redEnvelopeManager.InsertRedEnvelope(redEnvelope);
        }

        /// <summary>
        /// 客户领取某宝箱中红包时，要更新此红包数据
        /// </summary>
        /// <param name="redEnvelope"></param>
        /// <returns></returns>
        public bool UpdateRedEnvelope(RedEnvelope redEnvelope)
        {
            return redEnvelopeManager.UpdateRedEnvelope(redEnvelope);
        }

        /// <summary>
        /// 根据用户手机号码查询其领到的所有红包，包括宝箱信息
        /// </summary>
        /// <param name="mobilePhoneNumber"></param>
        /// <returns></returns>
        public DataTable QueryRedEnvelope(string mobilePhoneNumber)
        {
            return redEnvelopeManager.SelectRedEnvelope(mobilePhoneNumber);
        }

        /// <summary>
        /// 查看宝箱红包
        /// </summary>
        /// <param name="treasureChestId"></param>
        /// <returns></returns>
        //public List<RedEnvelopeViewModel> QueryRedEnvelopeViewModel(long treasureChestId)
        //{
        //    return redEnvelopeManager.SelectRedEnvelopeViewModel(treasureChestId);
        //}

        /// <summary>
        /// 查询红包使用的点单
        /// </summary>
        /// <param name="redEnvelopeId"></param>
        /// <returns></returns>
        public DataTable QueryRedEnvelopeUsedOrder(long redEnvelopeId)
        {
            return redEnvelopeManager.SelectRedEnvelopeUsedOrder(redEnvelopeId);
        }

        /// <summary>
        /// 查询用户所有可使用红包
        /// </summary>
        /// <param name="mobilePhoneNumber"></param>
        /// <returns></returns>
        public List<RedEnvelope> QueryCustomerAvailableRedEnvelope(string mobilePhoneNumber)
        {
            return redEnvelopeManager.SelectCustomerAvailableRedEnvelope(mobilePhoneNumber);
        }

        /// <summary>
        /// 支付点单批量修改红包状态
        /// </summary>
        /// <param name="mobilePhoneNumber"></param>
        /// <param name="isExecuted"></param>
        /// <param name="unusedAmount"></param>
        /// <param name="redEnvelopeIdStr"></param>
        /// <returns></returns>
        public bool ModifyRedEnvelope(string mobilePhoneNumber, int isExecuted, double unusedAmount, string redEnvelopeIdStr)
        {
            return redEnvelopeManager.UpdateRedEnvelope(mobilePhoneNumber, isExecuted, unusedAmount, redEnvelopeIdStr);
        }
        /// <summary>
        /// 支付点单批量修改红包状态（半个红包调用）
        /// </summary>
        /// <param name="mobilePhoneNumber"></param>
        /// <param name="isExecuted"></param>
        /// <param name="unusedAmount"></param>
        /// <param name="redEnvelopeIdStr"></param>
        /// <returns></returns>
        public bool ModifyHrefRedEnvelope(string mobilePhoneNumber, int isExecuted, double unusedAmount, string redEnvelopeIdStr)
        {
            return redEnvelopeManager.UpdateHrefRedEnvelope(mobilePhoneNumber, isExecuted, unusedAmount, redEnvelopeIdStr);
        }
        /// <summary>
        /// 支付红包逻辑处理（支付接口和直接支付接口调用）
        /// </summary>
        /// <param name="cookie">用户cookie</param>
        /// <param name="executedRedEnvelopeAmount">当前用户已生效红包</param>
        /// <param name="price">悠先价菜品金额</param>
        /// <param name="mobilePhoneNumber">手机号码</param>
        /// <param name="preOrder19dianId">点单流水号</param>
        /// <param name="canRedEnvelopePay">是否支持红包支付</param>
        /// <param name="extendPay">防作弊额外支付金额</param>
        /// <param name="rationBalancePayment">需要余额支付金额</param>
        /// <param name="insertResult">红包使用详情RedEnvelopeDetail插入结果</param>
        /// <param name="modifyRedEnvelope">红包RedEnvelope表批量更新结果</param>
        /// <param name="batchRedEnvelopeConnPreOrder">红包点单关联表RedEnvelopeConnPreOrder批量插入红包和点单关联关系</param>
        public void DoRedEnvelopePaymentLogic(string cookie, double executedRedEnvelopeAmount, double price, string mobilePhoneNumber,
            long preOrder19dianId, bool canRedEnvelopePay, double extendPay, ref double rationBalancePayment, ref bool modifyRedEnvelope,
            ref bool batchRedEnvelopeConnPreOrder, ref DateTime expireTime)
        {
            SystemConfigCacheLogic systemConfigCacheLogic = new SystemConfigCacheLogic();

            if (canRedEnvelopePay && price > 0)//最新版本才参与计算红包逻辑，菜价格为0特殊处理，余额支付处理
            {
                List<RedEnvelope> redEnvelopeList = QueryCustomerAvailableRedEnvelope(mobilePhoneNumber);
                //该次点单需要参与的红包
                List<RedEnvelope> redEnvelopeIdList = new List<RedEnvelope>();
                DataTable dt = CreateTempDt();
                RedEnvelopeDetailOperate detailOperate = new RedEnvelopeDetailOperate();
                RedEnvelopeConnPreOrderOperate redEnvelopeOrderOper = new RedEnvelopeConnPreOrderOperate();
                if (Common.ToDouble(executedRedEnvelopeAmount - price) > -0.01)
                {
                    #region  //1>红包支付

                    rationBalancePayment = 0;
                    double accruingAmount = 0;
                    for (int i = 0; i < redEnvelopeList.Count; i++)
                    {
                        accruingAmount += redEnvelopeList[i].unusedAmount;
                        redEnvelopeIdList.Add(redEnvelopeList[i]);
                        if (Common.ToDouble(accruingAmount) >= price)
                        {
                            break;
                        }
                    }
                    accruingAmount = Common.ToDouble(accruingAmount);
                    if (accruingAmount < price)
                    {
                        return;//当前用户红包数据有误
                    }
                    if (accruingAmount == price) //不会出现使用半个的红包
                    {
                        modifyRedEnvelope = ModifyRedEnvelope(mobilePhoneNumber, (int)VARedEnvelopeStateType.已使用, 0, CommonPageOperate.SplicingListStr<RedEnvelope>(redEnvelopeIdList, "redEnvelopeId"));
                        for (int i = 0; i < redEnvelopeIdList.Count; i++)
                        {
                            DataRow dr = dt.NewRow();
                            dr["preOrder19dianId"] = preOrder19dianId;
                            dr["redEnvelopeId"] = redEnvelopeIdList[i].redEnvelopeId;
                            dr["currectUsedAmount"] = redEnvelopeIdList[i].unusedAmount;
                            dr["currectUsedTime"] = DateTime.Now; 
                            dt.Rows.Add(dr);
                        }
                    }
                    else //会出现使用半个的红包
                    {
                        bool modifyRedEnvelope1 = true;
                        bool modifyRedEnvelope2 = true;
                        if (redEnvelopeIdList.Count == 1)
                        {
                            modifyRedEnvelope1 = ModifyHrefRedEnvelope(mobilePhoneNumber, (int)VARedEnvelopeStateType.已生效, (-1) * price, "(" + redEnvelopeIdList[0].redEnvelopeId + ")");
                            DataRow dr = dt.NewRow();
                            dr["preOrder19dianId"] = preOrder19dianId;
                            dr["redEnvelopeId"] = redEnvelopeIdList[0].redEnvelopeId;
                            dr["currectUsedAmount"] = Common.ToDouble(redEnvelopeIdList[0].unusedAmount - (accruingAmount - price));
                            dr["currectUsedTime"] = DateTime.Now;
                            dt.Rows.Add(dr); 
                        }
                        else
                        {
                            modifyRedEnvelope1 = ModifyRedEnvelope(mobilePhoneNumber, (int)VARedEnvelopeStateType.已使用, 0, CommonPageOperate.SplicingListStr<RedEnvelope>(redEnvelopeIdList.GetRange(0, redEnvelopeIdList.Count - 1), "redEnvelopeId"));
                            modifyRedEnvelope2 = ModifyRedEnvelope(mobilePhoneNumber, (int)VARedEnvelopeStateType.已生效, Common.ToDouble(accruingAmount - price), "(" + redEnvelopeIdList[redEnvelopeIdList.Count - 1].redEnvelopeId + ")");
                            for (int i = 0; i < redEnvelopeIdList.Count - 1; i++)
                            {
                                DataRow dr = dt.NewRow();
                                dr["preOrder19dianId"] = preOrder19dianId;
                                dr["redEnvelopeId"] = redEnvelopeIdList[i].redEnvelopeId;
                                dr["currectUsedAmount"] = Common.ToDouble(redEnvelopeIdList[i].unusedAmount);
                                dr["currectUsedTime"] = DateTime.Now;
                                dt.Rows.Add(dr); 
                            }
                            DataRow dr1 = dt.NewRow();
                            dr1["preOrder19dianId"] = preOrder19dianId;
                            dr1["redEnvelopeId"] = redEnvelopeIdList[redEnvelopeIdList.Count - 1].redEnvelopeId;
                            dr1["currectUsedAmount"] = Common.ToDouble(redEnvelopeIdList[redEnvelopeIdList.Count - 1].unusedAmount - (accruingAmount - price));//最后一个红包用了这么多钱
                            dr1["currectUsedTime"] = DateTime.Now;
                            dt.Rows.Add(dr1); 
                        }
                        modifyRedEnvelope = modifyRedEnvelope1 && modifyRedEnvelope2;
                    }
                    //insertResult = detailOperate.AddRedEnvelopeDetail(new RedEnvelopeDetail()
                    //{
                    //    mobilePhoneNumber = mobilePhoneNumber,
                    //    cookie = cookie,
                    //    operationTime = DateTime.Now,
                    //    stateType = (int)VARedEnvelopeStateType.已使用,
                    //    usedAmount = price,
                    //    //shopName = shopName,
                    //    preOrder19dianId = preOrder19dianId
                    //}) > 0;
                    //batchRedEnvelopeConnPreOrder = redEnvelopeOrderOper.BatchInsertRedEnvelopeConnPreOrder(dt) && UpdateCustomerRedEnvelope(mobilePhoneNumber, (-1) * price) && UpdatePreOrderExpireTime(preOrder19dianId, redEnvelopeList[0].expireTime);
                    batchRedEnvelopeConnPreOrder = redEnvelopeOrderOper.BatchInsertRedEnvelopeConnPreOrder(dt);

                    expireTime = redEnvelopeList[0].expireTime;
                    rationBalancePayment = 0;
                    #endregion
                }
                else
                {
                    if (executedRedEnvelopeAmount > 0)
                    {
                        #region //2>红包+余额支付

                        rationBalancePayment = Common.ToDouble(price - executedRedEnvelopeAmount);
                        modifyRedEnvelope = ModifyRedEnvelope(mobilePhoneNumber, (int)VARedEnvelopeStateType.已使用, 0, CommonPageOperate.SplicingListStr<RedEnvelope>(redEnvelopeList, "redEnvelopeId"));
                        //insertResult = detailOperate.AddRedEnvelopeDetail(new RedEnvelopeDetail()
                        //{
                        //    mobilePhoneNumber = mobilePhoneNumber,
                        //    cookie = cookie,
                        //    operationTime = DateTime.Now,
                        //    stateType = (int)VARedEnvelopeStateType.已使用,
                        //    usedAmount = executedRedEnvelopeAmount,
                        //    //shopName = shopName,
                        //    preOrder19dianId = preOrder19dianId
                        //}) > 0;
                        for (int i = 0; i < redEnvelopeList.Count; i++)
                        {
                            DataRow dr = dt.NewRow();
                            dr["preOrder19dianId"] = preOrder19dianId;
                            dr["redEnvelopeId"] = redEnvelopeList[i].redEnvelopeId;
                            dr["currectUsedAmount"] = redEnvelopeList[i].unusedAmount;//红包未使用金额
                            dr["currectUsedTime"] = DateTime.Now;
                            dt.Rows.Add(dr);
                        }
                        //batchRedEnvelopeConnPreOrder = redEnvelopeOrderOper.BatchInsertRedEnvelopeConnPreOrder(dt) && UpdateCustomerRedEnvelope(mobilePhoneNumber, (-1) * executedRedEnvelopeAmount) && UpdatePreOrderExpireTime(preOrder19dianId, redEnvelopeList[0].expireTime);
                        batchRedEnvelopeConnPreOrder = redEnvelopeOrderOper.BatchInsertRedEnvelopeConnPreOrder(dt);
                        expireTime = redEnvelopeList[0].expireTime;
                        #endregion
                    }
                    else
                    {
                        #region //3>余额支付

                        // 修改用户基本信息（消费次数，消费金额，vip等级）
                        rationBalancePayment = price;
                        //insertResult = true;
                        modifyRedEnvelope = true;
                        batchRedEnvelopeConnPreOrder = true;

                        #endregion
                    }
                }


            }
            else
            {
                #region //3>余额支付

                // 修改用户基本信息（消费次数，消费金额，vip等级）
                rationBalancePayment = price;
                //insertResult = true;
                modifyRedEnvelope = true;
                batchRedEnvelopeConnPreOrder = true;

                #endregion
            }

            rationBalancePayment = rationBalancePayment + extendPay;
        }

        /// <summary>
        /// 更新点单过期时间
        /// </summary>
        /// <param name="preOrderId">点单编号</param>
        /// <param name="dtTime">过期时间</param>
        /// <returns></returns>
        public bool UpdatePreOrderExpireTime(long preOrderId, DateTime dtTime)
        {
            return redEnvelopeManager.UpdatePreOrderExpireTime(preOrderId, dtTime);
        }

        /// <summary>
        /// 组装DataTable
        /// </summary>
        /// <returns></returns>
        private DataTable CreateTempDt()
        {
            DataTable dt = new DataTable("tempTable");
            DataColumn dc1 = new DataColumn("preOrder19dianId", Type.GetType("System.Int64"));
            DataColumn dc2 = new DataColumn("redEnvelopeId", Type.GetType("System.Int64"));
            DataColumn dc3 = new DataColumn("currectUsedAmount", Type.GetType("System.Double"));
            DataColumn dc4 = new DataColumn("currectUsedTime", Type.GetType("System.DateTime"));
            dt.Columns.Add(dc1);
            dt.Columns.Add(dc2);
            dt.Columns.Add(dc3);
            dt.Columns.Add(dc4);
            return dt;
        }

        /// <summary>
        /// 根据用户cookie或者手机号码查询其红包总额（可使用+未生效）
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ClientCheckRedEnvelopeResponse QueryCustomerRedEnvelope(ClientCheckRedEnvelopeRequest request)
        {
            ClientCheckRedEnvelopeResponse response = new ClientCheckRedEnvelopeResponse();
            response.type = VAMessageType.CLIENT_CHECK_REDENVELOPE_RESPONSE;
            response.cookie = request.cookie;
            response.uuid = request.uuid;
            CheckCookieAndMsgtypeInfo checkResult = Common.CheckCookieAndMsgtype(request.cookie, request.uuid, (int)request.type, (int)VAMessageType.CLIENT_CHECK_REDENVELOPE_REQUEST);
            if (checkResult.result == VAResult.VA_OK)
            {
                string mobilePhoneNumber = string.IsNullOrEmpty(request.mobilePhoneNumber) ? "" : request.mobilePhoneNumber;
                //RedEnvelopeDetailOperate redEnvelopeOperate = new RedEnvelopeDetailOperate();
                //double executedRedEnvelopeAmount = 0;
                //bool redEnvelopeFlag = redEnvelopeOperate.DoExpirationRedEnvelopeLogic(mobilePhoneNumber, ref  executedRedEnvelopeAmount);
                //if (!redEnvelopeFlag)
                //{
                //    response.result = VAResult.VA_FAILED_DB_ERROR;
                //    return response;
                //}
                RedEnvelopeOperate redEnvelopeOperate = new RedEnvelopeOperate();
                CouponOperate couponOperate = new CouponOperate();
                response.result = VAResult.VA_OK;
                if (!string.IsNullOrEmpty(mobilePhoneNumber))
                {
                    //response.amount = Common.CheckLatestBuild_February(request.appType, request.clientBuild) ? redEnvelopeManager.SelectCustomerRedEnvelope(request.mobilePhoneNumber) : 0;
                    //response.amount = redEnvelopeManager.SelectCustomerRedEnvelope(request.mobilePhoneNumber);
                    if (!string.IsNullOrEmpty(request.mobilePhoneNumber))
                    {
                        response.amount = redEnvelopeManager.QueryCustomerExcutedRedEnvelope(request.mobilePhoneNumber);
                        response.haveUncheckCoupon = new CouponOperate().HaveUnCheckCoupon(mobilePhoneNumber);
                    }
                    else
                    {
                        response.amount = 0;
                    }
                    response.haveCouponCount = string.IsNullOrWhiteSpace(mobilePhoneNumber) ? 0 : (Common.CheckLatestBuild_February(response.appType, response.clientBuild) ? new CouponOperate().GetHadCouponCount(mobilePhoneNumber) : 0);
                    bool isHaveMore = false;

                    CouponCacheLogic couponCacheLogic = new CouponCacheLogic();
                    bool isShowIndex = couponCacheLogic.GetCouponIsShowOnIndexOfCache();
                    if (isShowIndex)
                    {
                        response.customerCouponDetail = couponOperate.SelectCustomerCouponDetail(mobilePhoneNumber, request.cityId, out isHaveMore);
                    }
                    else
                    {
                        response.customerCouponDetail = null;
                    }
                    response.isHaveMore = isHaveMore;
                }
                else
                {
                    response.amount = 0;
                    response.haveCouponCount = 0;
                }
                SystemConfigCacheLogic systemCache = new SystemConfigCacheLogic();
                response.enableClientConfirm = Common.ToBool(systemCache.GetSystemConfig("enableClientConfirm", "false"));
            }
            else
            {
                response.result = checkResult.result;
            }
            return response;
        }

        /// <summary>
        /// 用户绑定电话号码时，更新Customer中已经领取的红包金额
        /// </summary>
        /// <param name="mobilePhoneNumber"></param>
        /// <param name="cookie"></param>
        //public bool UpdateCustomerRedEnvelope(object customerObject)
        //{
        //    bool result = true;
        //    CustomerManager customerManager = new CustomerManager();
        //    RedEnvelopeDetailManager redEnvelopeDetailManager = new RedEnvelopeDetailManager();
        //    CustomerRedEnvelope customer = (CustomerRedEnvelope)customerObject;
        //    //查询此时已生效的红包，和未生效的红包(包括获得的和作废的)，并更新至CustomerInfo相应栏位
        //    double executedAmount = redEnvelopeManager.SelectCustomerPartRedEnvelope(customer.mobilePhoneNumber, true, false);//天天红包+大红包
        //    double executedAmountGan = redEnvelopeManager.SelectCustomerPartRedEnvelope(customer.mobilePhoneNumber, true, true, customer.uuid);//已生效的节日红包
        //    double validNotExecuteAmount = redEnvelopeManager.SelectCustomerPartRedEnvelope(customer.mobilePhoneNumber, false, false, customer.uuid);//获得的未生效金额
        //    double invalidNotExecuteAmount = redEnvelopeManager.SelectCustomerPartRedEnvelope(customer.mobilePhoneNumber, false, true, customer.uuid);//作废的未生效金额
        //    bool updateCustomer = true;

        //    executedAmountGan = executedAmountGan + executedAmount;
        //    if (executedAmountGan > 0 || validNotExecuteAmount > 0)
        //    {
        //        using (TransactionScope ts = new TransactionScope())
        //        {
        //            //updateCustomer = customerManager.UpdateCustomerRedEnvelope(customer.mobilePhoneNumber, executedAmountGan, validNotExecuteAmount);
        //            bool updateUuid = true;
        //            if (validNotExecuteAmount > 0)
        //            {
        //                //1.redEnvelope 更新uuid
        //                updateUuid = redEnvelopeManager.UpdateRedEnvelopeUuid(customer);
        //            }
        //            if (updateCustomer && updateUuid)
        //            {
        //                ts.Complete();
        //                result = true;
        //            }
        //            else
        //            {
        //                result = false;
        //            }
        //        }
        //    }

        //    //有获得的未生效金额
        //    //if (validNotExecuteAmount > 0)
        //    //{
        //    //    using (TransactionScope ts = new TransactionScope())
        //    //    {
        //    //        //1.redEnvelope 更新uuid
        //    //        bool updateUuid = redEnvelopeManager.UpdateRedEnvelopeUuid(customer);
        //    //        //2.customerInfo 更新未生效金额
        //    //        updateCustomer = customerManager.UpdateCustomerRedEnvelope(customer.mobilePhoneNumber, 0, validNotExecuteAmount);

        //    //        if (updateCustomer && updateUuid)
        //    //        {
        //    //            ts.Complete();
        //    //            result = true;
        //    //        }
        //    //        else
        //    //        {
        //    //            result = false;
        //    //        }
        //    //    }
        //    //}
        //    //有作废的未生效金额
        //    if (invalidNotExecuteAmount > 0)
        //    {
        //        using (TransactionScope ts = new TransactionScope())
        //        {
        //            //1.redEnvelope 节日红包状态更改为作废
        //            bool changeStatus = redEnvelopeManager.UpdateRedEnvelopeStatus(customer);

        //            //2.redEnvelopeDetail 新增作废记录
        //            //RedEnvelopeDetail detail = new RedEnvelopeDetail()
        //            //{
        //            //    mobilePhoneNumber = customer.mobilePhoneNumber,
        //            //    stateType = (int)VARedEnvelopeStateType.已作废,
        //            //    usedAmount = invalidNotExecuteAmount,
        //            //    operationTime = DateTime.Now
        //            //};
        //            //long insert = redEnvelopeDetailManager.InsertRedEnvelopeDetail_1(detail);
        //            bool changeDetailStatus = redEnvelopeDetailManager.UpdateRedEnvelopeDetailStatus(customer);

        //            if (changeStatus && changeDetailStatus)
        //            {
        //                ts.Complete();
        //                result = true;
        //            }
        //            else
        //            {
        //                result = false;
        //            }
        //        }
        //    }
        //    return result;
        //}
        /// <summary>
        /// 更新用户红包余额
        /// </summary>
        /// <param name="mobilePhoneNumber"></param>
        /// <param name="executedRedEnvelopeAmount"></param>
        /// <returns></returns>
        public bool UpdateCustomerRedEnvelope(string mobilePhoneNumber, double executedRedEnvelopeAmount)
        {
            return redEnvelopeManager.UpdateCustomerRedEnvelope(mobilePhoneNumber, executedRedEnvelopeAmount);
        }
        public List<TopRedEnvelopeRankList> GetTopRankList()
        {
            return redEnvelopeManager.GetTopRankList();
        }

        public bool BatchInsert(DataTable dt)
        {
            return redEnvelopeManager.BatchInsert(dt);
        }

        public DataTable SelectCustomer(string mobilePhoneNumber)
        {
            RedEnvelopeTestManager manager = new RedEnvelopeTestManager();
            return manager.SelectCustomer(mobilePhoneNumber);
        }
        public DataTable SelectTestCustomer()
        {
            RedEnvelopeTestManager manager = new RedEnvelopeTestManager();
            return manager.SelectTestCustomer();
        }

        public double Sum(int activityId)
        {
            return redEnvelopeManager.Sum(activityId);
        }

        /// <summary>
        /// 给指定用户发送指定红包
        /// </summary>
        /// <param name="strPhone"></param>
        /// <param name="activityId"></param>
        /// <returns></returns>
        public object[] SendRedEnvelope(string[] strPhone, int activityId)
        {
            object[] objResult = new object[] { false, "" };
            StringBuilder errorPhone = new StringBuilder();
            StringBuilder isHavePhone = new StringBuilder();
            StringBuilder emptyPhone = new StringBuilder();
            StringBuilder repeatPhone = new StringBuilder();
            bool isHave = false;

            ActivityOperate activityOperate = new ActivityOperate();
            RedEnvelopeOperate redEnvelopeOperate = new RedEnvelopeOperate();
            TreasureChestOperate treasureChestOperate = new TreasureChestOperate();
            TreasureChestConfigOperate configOperate = new TreasureChestConfigOperate();
            CustomerManager customerManager = new CustomerManager();

            Activity activity = activityOperate.QueryActivity(activityId);//活动信息

            if (activity != null && activity.activityId > 0)
            {
                if (activity.endTime > DateTime.Now)
                {
                    TreasureChestConfig config = configOperate.QueryConfigOfActivity(activityId);//配置信息

                    List<RedEnvelope> RedEnvelopeList = new List<RedEnvelope>();
                    RedEnvelope redEnvelope = new RedEnvelope();

                    var repeatPhoneList = strPhone.GroupBy(i => i).Where(g => g.Count() > 1);
                    if (repeatPhoneList != null && repeatPhoneList.Any())
                    {
                        foreach (var phone in repeatPhoneList)
                        {
                            repeatPhone.Append(phone.Key + ",");
                        }
                    }

                    strPhone = strPhone.Distinct().ToArray();
                    double remainAmount = 0;
                    double amountSum = redEnvelopeOperate.Sum(activityId);
                    foreach (string phone in strPhone)
                    {
                        remainAmount = config.amount - amountSum;

                        if (remainAmount <= 0)//没钱了
                        {
                            emptyPhone.Append(phone + ",");
                        }
                        else//宝箱剩余还有钱
                        {
                            if (phone.Length != 11)
                            {
                                errorPhone.Append(phone + ",");
                            }
                            else
                            {
                                isHave = redEnvelopeManager.CheckCustomerHadRedEnvelope(phone, activityId);
                                if (isHave)
                                {
                                    isHavePhone.Append(phone + ",");
                                }
                                else
                                {
                                    redEnvelope = new RedEnvelope
                                    {
                                        Amount = 0,//初始值
                                        expireTime = DateTime.Now,
                                        getTime = DateTime.Now,
                                        isExecuted = 1,
                                        isExpire = false,
                                        status = true,
                                        treasureChestId = 0,
                                        mobilePhoneNumber = phone,
                                        unusedAmount = 0,//初始指
                                        activityId = activity.activityId,
                                        isOwner = false,
                                        isOverflow = false,
                                        cookie = "",
                                        uuid = "",
                                        effectTime = DateTime.Now,
                                        isChange = false
                                    };
                                    switch (activity.expirationTimeRule)
                                    {
                                        case ExpirationTimeRule.postpone:

                                            if (activity.beginTime > DateTime.Now)
                                            {
                                                redEnvelope.effectTime = activity.beginTime;
                                                redEnvelope.expireTime = Common.ToDateTime(activity.beginTime.AddDays(activity.ruleValue).ToString("yyyy/MM/dd 23:59:59"));
                                            }
                                            else
                                            {
                                                redEnvelope.expireTime = Common.ToDateTime(DateTime.Now.AddDays(activity.ruleValue).ToString("yyyy/MM/dd 23:59:59"));
                                            }
                                            break;
                                        case ExpirationTimeRule.unify:
                                            redEnvelope.effectTime = activity.redEnvelopeEffectiveBeginTime;
                                            redEnvelope.expireTime = activity.redEnvelopeEffectiveEndTime;
                                            break;
                                    }
                                    //if (activity.activityType == ActivityType.抽奖红包)
                                    //{
                                    //    redEnvelope.effectTime = Common.ToDateTime(DateTime.Now.AddYears(100).ToString("yyyy/MM/dd 23:59:59"));
                                    //    redEnvelope.expireTime = Common.ToDateTime(DateTime.Now.AddYears(100).ToString("yyyy/MM/dd 23:59:59"));
                                    //}

                                    switch (config.amountRule)
                                    {
                                        case (int)RedEnvelopeAmountRule.概率取值:
                                            redEnvelope.Amount = GetAmount(config.defaultAmountRange, config.defaultRateRange);
                                            break;
                                        default:
                                        case (int)RedEnvelopeAmountRule.最小最大值:
                                            redEnvelope.Amount = GetAmount((int)(config.min * 10), (int)(config.max * 10));//计算获取红包金额算法
                                            break;
                                    }
                                    redEnvelope.unusedAmount = redEnvelope.Amount;
                                    //此处记录已发放金额
                                    amountSum += redEnvelope.Amount;

                                    RedEnvelopeList.Add(redEnvelope);
                                }
                            }
                        }
                    }

                    if (RedEnvelopeList != null && RedEnvelopeList.Any())
                    {
                        if (BatchInsertRedEnvelope(RedEnvelopeList))
                        {
                            objResult[0] = true;
                            objResult[1] = "发送成功共" + RedEnvelopeList.Count + "个。";
                        }
                        else
                        {
                            objResult[1] = "发送失败";
                        }
                    }
                }
                else
                {
                    objResult[1] = "活动已过期。";
                }
            }
            else
            {
                objResult[1] = "活动无效。";
            }
            if (repeatPhone != null && repeatPhone.Length > 0)
            {
                objResult[1] += "重复数据：" + repeatPhone.ToString() + "。\\r\\n";
            }
            if (errorPhone != null && errorPhone.Length > 0)
            {
                objResult[1] += "\\r\\n用户手机号码有误：" + errorPhone.ToString() + "。\\r\\n";
            }
            if (isHavePhone != null && isHavePhone.Length > 0)
            {
                objResult[1] += "\\r\\n用户已经领过该红包：" + isHavePhone.ToString() + "。\\r\\n";
            }
            if (emptyPhone != null && emptyPhone.Length > 0)
            {
                objResult[1] += "\\r\\n活动已结束:" + emptyPhone.ToString() + "。\\r\\n";
            }
            return objResult;
        }


        static Random randomAmount = new Random();

        /// <summary>
        /// 给指定用户发送抽奖红包
        /// </summary>
        /// <param name="strPhone"></param>
        /// <param name="activityId"></param>
        /// <returns></returns>
        public RedEnvelopeAwardResponse SendLotteryRedEnvelope(RedEnvelopeAwardRequest request)
        {
            RedEnvelopeAwardResponse response = new RedEnvelopeAwardResponse();

            ActivityOperate activityOperate = new ActivityOperate();
            RedEnvelopeOperate redEnvelopeOperate = new RedEnvelopeOperate();
            TreasureChestOperate treasureChestOperate = new TreasureChestOperate();
            TreasureChestConfigOperate configOperate = new TreasureChestConfigOperate();
            CustomerManager customerManager = new CustomerManager();
            PreOrder19dianOperate preOrder19dianOperate = new PreOrder19dianOperate();

            Activity activity = activityOperate.QueryActivity(request.activityId);//活动信息

            if (activity != null && activity.activityId > 0)
            {
                if (activity.endTime > DateTime.Now)
                {

                    if (activity.activityType == ActivityType.抽奖红包)
                    {

                        TreasureChestConfig config = configOperate.QueryConfigOfActivity(request.activityId);//配置信息

                        List<RedEnvelope> RedEnvelopeList = new List<RedEnvelope>();
                        RedEnvelope redEnvelope = new RedEnvelope();

                        double remainAmount = 0;
                        double amountSum = redEnvelopeOperate.Sum(request.activityId);

                        remainAmount = config.amount - amountSum;

                        if (remainAmount <= 0)//没钱了
                        {
                            response.message = "红包余额不足";
                        }
                        else//宝箱剩余还有钱
                        {
                            if (request.mobile.Length != 11)
                            {
                                response.message = "电话号码错误";
                            }
                            else
                            {
                                redEnvelope = new RedEnvelope
                                {
                                    Amount = 0,//初始值
                                    expireTime = DateTime.Now,
                                    getTime = DateTime.Now,
                                    isExecuted = 1,
                                    isExpire = false,
                                    status = true,
                                    treasureChestId = 0,
                                    mobilePhoneNumber = request.mobile,
                                    unusedAmount = 0,//初始指
                                    activityId = activity.activityId,
                                    isOwner = false,
                                    isOverflow = false,
                                    cookie = request.cookie,
                                    uuid = request.uuid,
                                    effectTime = DateTime.Now,
                                    isChange = false,
                                    from = "award"
                                };

                                if (activity.activityType == ActivityType.抽奖红包)
                                {
                                    redEnvelope.effectTime = Common.ToDateTime(DateTime.Now.AddYears(100).ToString("yyyy/MM/dd 23:59:59"));
                                    redEnvelope.expireTime = Common.ToDateTime(DateTime.Now.AddYears(100).ToString("yyyy/MM/dd 23:59:59"));
                                    redEnvelope.Amount = 0;

                                    //bool isMobileVerified = CustomerOperate.IsMobileVerified(request.mobile);//用户是否已经注册
                                    bool isOnePaidOrder = preOrder19dianOperate.IsOnePaidOrder(request.customerId);

                                    //取门店返现规则

                                    ShopReturnMoneyLimitOperate shopReturnMoneyLimitOperate = new ShopReturnMoneyLimitOperate();
                                    ShopReturnMoneyLimit shopReturnMoneyLimit = shopReturnMoneyLimitOperate.SelectShopReturnMoneyLimit(request.shopId);
                                    if (shopReturnMoneyLimit != null && shopReturnMoneyLimit.Id > 0)
                                    {
                                        string[] ratioRange = shopReturnMoneyLimit.CustomerBackMoneyRatio.Split(',');
                                        int ratio = randomAmount.Next(Common.ToInt32(ratioRange[0]), Common.ToInt32(ratioRange[1]) + 1);

                                        redEnvelope.Amount = Common.ToDouble(request.thirdPayAmount * ratio / 100);

                                        if (Convert.ToDecimal(redEnvelope.Amount) > shopReturnMoneyLimit.CustomerMaxBackMoney)
                                        {
                                            redEnvelope.Amount = Convert.ToDouble(shopReturnMoneyLimit.CustomerMaxBackMoney);
                                        }
                                    }
                                    else//门店没有限制，取平台返现规则
                                    {
                                        AwardCacheLogic awardCacheLogic = new AwardCacheLogic();

                                        if (!isOnePaidOrder)//老用户
                                        {
                                            string[] ratioRange = awardCacheLogic.GetAwardConfig("OldCustomerBackMoneyRatio", "").Split(',');

                                            int ratio = randomAmount.Next(Common.ToInt32(ratioRange[0]), Common.ToInt32(ratioRange[1]) + 1);

                                            redEnvelope.Amount = Common.ToDouble(request.thirdPayAmount * ratio / 100);

                                            double CustomerMaxBackMoney = Common.ToDouble(awardCacheLogic.GetAwardConfig("OldCustomerMaxBackMoney", ""));

                                            if (redEnvelope.Amount > CustomerMaxBackMoney)
                                            {
                                                redEnvelope.Amount = CustomerMaxBackMoney;
                                            }
                                        }
                                        else//新用户
                                        {
                                            string[] ratioRange = awardCacheLogic.GetAwardConfig("NewCustomerBackMoneyRatio", "").Split(',');

                                            int ratio = randomAmount.Next(Common.ToInt32(ratioRange[0]), Common.ToInt32(ratioRange[1]) + 1);

                                            redEnvelope.Amount = request.thirdPayAmount * ratio / 100;

                                            double CustomerMaxBackMoney = Common.ToDouble(awardCacheLogic.GetAwardConfig("NewCustomerMaxBackMoney", ""));

                                            if (redEnvelope.Amount > CustomerMaxBackMoney)
                                            {
                                                redEnvelope.Amount = CustomerMaxBackMoney;
                                            }
                                        }
                                    }

                                    if (redEnvelope.Amount == 0)
                                    {
                                        redEnvelope.Amount = 0.01;
                                    }

                                    redEnvelope.unusedAmount = redEnvelope.Amount;

                                    response.redEnvelope = redEnvelope;
                                    response.result = true;

                                    //long redEnvelopeId = redEnvelopeOperate.InsertRedEnvelope(redEnvelope);
                                    //if (redEnvelopeId > 0)
                                    //{
                                    //    response.result = true;
                                    //    response.redEnvelopeAmount = redEnvelope.Amount;
                                    //    response.message = "发送成功";

                                    //}
                                    //else
                                    //{
                                    //    response.message = "发送失败";
                                    //}
                                }
                            }
                        }
                    }
                    else
                    {
                        response.message = "红包类别错误";
                    }
                }
                else
                {
                    response.message = "活动已过期。";
                }
            }
            else
            {
                response.message = "活动无效。";
            }
            return response;
        }


        static Random random = new Random();
        private double GetAmount(int min, int max)
        {
            int money = random.Next(min, max);
            return money / 10.0;
        }
        private double GetAmount(string range, string scale)
        {
            string[] rangeStr = range.Split(',');
            string[] scaleStr = scale.Split(',');
            int count = 0;
            int rmdCount = random.Next(0, 100);
            for (int i = 0; i < scaleStr.Length; i++)
            {
                if (rmdCount >= Convert.ToInt32(scaleStr[i]) && rmdCount < Convert.ToInt32(scaleStr[i + 1]))
                {
                    count = random.Next(Convert.ToInt32(rangeStr[i]) * 10, Convert.ToInt32(rangeStr[i + 1]) * 10);
                    break;
                }
            }
            return count / 10.0;
        }

        public bool BatchInsertRedEnvelope(object value)
        {
            RedEnvelopeOperate redEnvelopeOperate = new RedEnvelopeOperate();
            DataTable dtRedEnvelope = Common.ListToDataTable((List<RedEnvelope>)value);
            dtRedEnvelope.TableName = "RedEnvelope";
            return redEnvelopeOperate.BatchInsert(dtRedEnvelope);
        }

        /// <summary>
        /// 查询某个用户所有的可用红包
        /// </summary>
        /// <param name="mobilePhoneNumber"></param>
        /// <returns></returns>
        public double QueryCustomerExcutedRedEnvelope(string mobilePhoneNumber)
        {
            return redEnvelopeManager.QueryCustomerExcutedRedEnvelope(mobilePhoneNumber);
        }

        public double QueryCustomerNotExecutedRedEnvelope(string mobilePhoneNumber)
        {
            return redEnvelopeManager.QueryCustomerNotExecutedRedEnvelope(mobilePhoneNumber);
        }
        /// <summary>
        /// 查询用户的可用及未生效红包
        /// </summary>
        /// <param name="mobilePhoneNumber"></param>
        /// <returns></returns>
        public double[] QueryCustomerRedEnvelope(string mobilePhoneNumber)
        {
            double[] amount = new double[] { 0, 0 };
            amount[0] = QueryCustomerExcutedRedEnvelope(mobilePhoneNumber);
            amount[1] = QueryCustomerNotExecutedRedEnvelope(mobilePhoneNumber);
            return amount;
        }

        public int QueryExpireCount(string mobilePhoneNumber)
        {
            SystemConfigCacheLogic systemConfigCacheLogic = new SystemConfigCacheLogic();
            int redEnvelopeExpireDay = systemConfigCacheLogic.GetRedEnvelopeExpireDayOfCache();
            return redEnvelopeManager.QueryExpireCount(mobilePhoneNumber, redEnvelopeExpireDay);
        }

        /// <summary>
        /// 财务查询指定时期指定类别红包支付金额
        /// </summary>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <param name="activityType"></param>
        /// <returns></returns>
        public double QueryFinanceRedEnvelopePay(DateTime beginTime, DateTime endTime, int activityType)
        {
            return redEnvelopeManager.QueryFinanceRedEnvelopePay(beginTime, endTime, activityType);
        }

        /// <summary>
        /// 财务查询指定时期红包退款金额
        /// </summary>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public double QueryFinanceRedEnvelopeRefund(DateTime beginTime, DateTime endTime)
        {
            return redEnvelopeManager.QueryFinanceRedEnvelopeRefund(beginTime, endTime);
        }


        public void ClearIllegalRedEnvelope(object customerRedEnvelope)
        {
            CustomerRedEnvelope customer = (CustomerRedEnvelope)customerRedEnvelope;

            RedEnvelopeManager redEnvelopeManager = new RedEnvelopeManager();
            try
            {
                //1.取出用户所有红包
                List<RedEnvelopeOfActivity> redEnvelopeOfActivityList = redEnvelopeManager.QueryRedEnvelopeOfActivity(customer.mobilePhoneNumber);
                //2.检查每一条，是否已经在此设备用别的号码领过
                //  若是，则作废该红包
                //  反之，将此设备的uuid赋给该红包
                if (redEnvelopeOfActivityList != null && redEnvelopeOfActivityList.Any())
                {
                    SystemConfigCacheLogic systemConfigCacheLogic = new SystemConfigCacheLogic();
                    int maxRedEnvelopeCount = systemConfigCacheLogic.GetMaxRedEnvelopeCountOfCache();

                    //如果注册前红包个数大于设定个数，则作废所有红包
                    if (redEnvelopeOfActivityList.Count > maxRedEnvelopeCount)
                    {
                        foreach (RedEnvelopeOfActivity item in redEnvelopeOfActivityList)
                        {
                            redEnvelopeManager.CancelRedEnvelope(item.redEnvelopeId);
                        }
                    }
                    else
                    {
                        bool isOwnerOther = false;//是否被别的号码在这个设备上领过
                        foreach (RedEnvelopeOfActivity item in redEnvelopeOfActivityList)
                        {
                            isOwnerOther = redEnvelopeManager.QueryRedEnvelopeIsOwnUUID(item.activityId, customer.uuid);
                            if (isOwnerOther)//则要作废
                            {
                                redEnvelopeManager.CancelRedEnvelope(item.redEnvelopeId);
                            }
                            else//将uuid更新到此红包上
                            {
                                redEnvelopeManager.UpdateRedEnvelopeUUID(item.redEnvelopeId, customer.uuid);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            { }
        }

        /// <summary>
        /// 财务查询指定时期红包退款金额
        /// </summary>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public DataTable QueryRedEnvelopeFinance(DateTime beginTime, DateTime endTime, int activityType, int cityID)
        {
            return redEnvelopeManager.SelectRedEnvelopeFinance(beginTime, endTime, activityType, cityID);
        }

        /// <summary>
        /// 查询指定红包领取情况（0：数量；1：金额）
        /// </summary>
        /// <param name="activityId"></param>
        /// <returns></returns>
        public RedEnvelopeConsume SelectRedEnvelopeConsume(int activityId)
        {
            return redEnvelopeManager.SelectRedEnvelopeConsume(activityId);
        }

        /// <summary>
        /// 根据红包Id，查询某用户领取的红包金额
        /// </summary>
        /// <param name="activityId"></param>
        /// <param name="mobilePhoneNumber"></param>
        /// <returns></returns>
        public double SelectRedEnvelopeAmount(long redEnvelopeId)
        {
            return redEnvelopeManager.SelectRedEnvelopeAmount(redEnvelopeId);
        }

        /// <summary>
        /// 用户全额退款，作废其中奖红包
        /// </summary>
        /// <param name="activityId"></param>
        /// <param name="mobilePhoneNumber"></param>
        /// <returns></returns>
        public bool CancelAwardRedEnvelope(long redEnvelopeId)
        {
            return redEnvelopeManager.CancelAwardRedEnvelope(redEnvelopeId);
        }

        /// <summary>
        /// 用户中奖订单对账后，把中奖红包生效
        /// </summary>
        /// <param name="activityId"></param>
        /// <param name="expireTime"></param>
        /// <param name="mobilePhoneNumber"></param>
        /// <returns></returns>
        public bool EffectAwardRedEnvelope(long redEnvelopeId, DateTime expireTime)
        {
            return redEnvelopeManager.EffectAwardRedEnvelope(redEnvelopeId, expireTime);
        }
    }
}
