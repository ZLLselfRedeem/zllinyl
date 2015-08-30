﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Transactions;
using System.Data;
using LogDll;
using VAGastronomistMobileApp.SQLServerDAL;
using VAGastronomistMobileApp.Model;
using System.Web;
using VA.AllNotifications;
using System.Configuration;
using System.IO;
using VAGastronomistMobileApp.WebPageDll.ThreadCallBacks;
using VAGastronomistMobileApp.TheThirdPartyPaymentDll;
using VAGastronomistMobileApp.Model.Interface;
using VAGastronomistMobileApp.DBUtility;
using System.Data.SqlClient;

namespace VAGastronomistMobileApp.WebPageDll
{
    /// <summary>
    /// 功能描述:收银宝账户体系类-用户部分
    /// 创建标识:罗国华 20131120
    /// </summary>
    public class SybMoneyCustomerOperate
    {
        /// <summary>
        /// 收银宝商户退款
        /// </summary>
        /// <param name="refundAccount">退款金额</param>
        /// <param name="refundDes">退款原因</param>
        /// <param name="preOrder19dianId">点单流水号</param>
        /// <returns></returns>
        public static string OrderRefundOperate(double refundAccount, string refundDes, Guid preOrder19dianId)
        {
            // 新版本补差价功能，加入orderID主键,这里把preOrder19dianId就当作主键Order表的orderID使用 add by zhujinlei 2015/06/30
            Guid orderID = preOrder19dianId;

            SybMsg sybMsg = new SybMsg();
            int result = OrderRefund(refundAccount, refundDes, orderID);
            switch (result)
            {
                case 1:
                    sybMsg.Insert(1, "退款成功");
                    break;
                case -1:
                    sybMsg.Insert(-1, "退款失败，请重试");
                    break;
                case -4:
                    sybMsg.Insert(-4, "未找到该点单");
                    break;
                case -2:
                    sybMsg.Insert(-2, "退款金额填写错误");
                    break;
                case -3:
                    sybMsg.Insert(-3, "该点单属于历史点单，系统无法退款，请联系管理员");
                    break;
                case -5:
                    sybMsg.Insert(-5, "该点单未入座，不能退款");
                    break;
                case -6:
                    sybMsg.Insert(-6, "退款原因不能忘为空");
                    break;
                case -7:
                    sybMsg.Insert(-7, "已全额退款，请联系管理员");
                    break;
                default:
                    break;
            }
            return sybMsg.Value;
        }
        /// <summary>
        /// 获得该点单退款的信息（可能有多条数据）
        /// </summary>
        /// <param name="preOrder19dianId">点单流水号</param>
        /// <returns></returns>
        public static string GetRefundInfo(long preOrder19dianId, Guid orderID)
        {
            List<MoneyRefundDetail> list = Money19dianDetailOperate.GetMoney19dianDetail(preOrder19dianId, orderID);
            return SysJson.JsonSerializer<List<MoneyRefundDetail>>(list);
        }
        /// <summary>
        /// 用户账户余额变更，用户账户变更明细记录
        /// </summary>
        /// <param name="money19dianDetail">Money19dianDetail model</param>
        /// <param name="flag">标记阿里，银联，微信订单号充值</param>
        /// <returns></returns>
        public static bool AccountBalanceChanges(Money19dianDetail money19dianDetail, bool flag = false)
        {
            Money19dianDetailManager money19dianDetailMan = new Money19dianDetailManager();
            bool result1 = money19dianDetailMan.Insert(money19dianDetail); //用户账户余额明细
            bool result0 = false;
            bool result2 = true;
            try
            {
                using (TransactionScope tScope = new TransactionScope())
                {
                    CustomerManager customermanager = new CustomerManager();
                    //money19dianDetail.changeValue  字段带有+-号
                    if (flag)
                    {
                        ClientRechargeOperate clientRechargeOperate = new ClientRechargeOperate();
                        double presentAmount = clientRechargeOperate.QueryPresentMoney(Common.ToInt64(money19dianDetail.accountTypeConnId));//赠送余额
                        result0 = customermanager.UpdateMoney19dianRemained(money19dianDetail.customerId, money19dianDetail.changeValue + presentAmount); //用户账户余额更新
                        money19dianDetail.changeReason = Common.GetEnumDescription(VA19dianMoneyChangeReason.MONEY19DIAN_SERVICE_RECHARGE_PRESENT).Replace("{0}", Common.ToString(presentAmount));
                        money19dianDetail.changeValue = presentAmount;
                        money19dianDetail.accountType = (int)AccountType.SERVICERECHARGE_PRESENT;
                        result2 = money19dianDetailMan.Insert(money19dianDetail); //用户账户余额明细
                    }
                    else
                    {
                        result0 = customermanager.UpdateMoney19dianRemained(money19dianDetail.customerId, money19dianDetail.changeValue); //用户账户余额更新
                    }
                    if (result0 && result1 && result2)
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
            catch //(Exception ex)
            {
                //string filePath = HttpContext.Current.Server.MapPath("~/Logs/paymentLogwangc.txt");
                //using (StreamWriter file = new StreamWriter(@filePath, true))
                //{
                //    file.WriteLine(System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + "22222222" + ex.ToString() + "-----" + result0.ToString() + "-----" + result1.ToString() + "-----" + result2.ToString());
                //}
                return false;
            }
        }
        /// <summary>
        /// 查询当前用户的当前时间的账户余额
        /// </summary>
        /// <param name="customerId">用户ID</param>
        /// <returns></returns>
        public static double GetCustomerRemainMoney(long customerId)
        {
            return Money19dianDetailManager.GetCustomerRemainMoney(customerId);
        }
        /// <summary>
        ///  商户（收银宝）退款，将记录插入临时记录表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool InsertMoneyRefundDetail(MoneyRefundDetail model)
        {
            return Money19dianDetailManager.InsertMoneyRefundDetail(model);
        }
        /// <summary>
        /// 收银宝退款成功推送
        /// </summary>
        /// <param name="dtPreOrderInfo">点单信息DataTable</param>
        /// <param name="refundAccount">退款金额</param>
        private static void SybRefundPushMessage(DataTable dtPreOrderInfo, double refundAccount)
        {
            //推送格式
            //您好，由于您在{0}餐厅消费不便，已退款{1}元到您的悠先账户中，请注意查看
            NotificationRecordManager notificationRecordMan = new NotificationRecordManager();
            DataTable dt = notificationRecordMan.SelectNewDeviceInfo(Common.ToInt32(dtPreOrderInfo.Rows[0]["customerId"]));
            if (dt.Rows.Count > 0)
            {
                string pushToken = Common.ToString(dt.Rows[0]["pushToken"]);
                ShopOperate shopOeperate = new ShopOperate();
                string shopName = shopOeperate.QueryShop(Common.ToInt32(dtPreOrderInfo.Rows[0]["shopId"])).shopName;
                if (!string.IsNullOrEmpty(pushToken))
                {
                    NotificationRecord notificationRecord = new NotificationRecord();
                    notificationRecord.addTime = System.DateTime.Now;
                    notificationRecord.appType = Common.ToInt32(dt.Rows[0]["appType"]);
                    notificationRecord.isLocked = false;
                    notificationRecord.isSent = false;
                    notificationRecord.pushToken = pushToken;
                    notificationRecord.sendCount = 0;
                    string message = ConfigurationManager.AppSettings["sybRefundMessage"];
                    message = message.Replace("{0}", shopName);
                    message = message.Replace("{1}", Common.ToString(refundAccount));
                    notificationRecord.message = message;
                    notificationRecord.customType = (int)VANotificationsCustomType.NOTIFICATIONS_SYB_REFUND;//收银宝退款
                    notificationRecord.customValue = Common.ToString(dtPreOrderInfo.Rows[0]["customerId"]);//用户编号customerId
                    System.Threading.Thread encourageThread = new System.Threading.Thread(Common.AddNotificationRecord);
                    encourageThread.Start((object)notificationRecord);
                }
            }
        }
        /// <summary>
        /// 收银宝商户退款（子方法）
        /// </summary>
        /// <param name="refundAccount">退款金额</param>
        /// <param name="refundDes">退款原因</param>
        /// <param name="orderID">点单ID</param>
        /// <returns></returns>
        public static int OrderRefund(double refundAccount, string refundDes, Guid orderID)
        {
            refundAccount = Common.ToDouble(refundAccount);
            PreOrder19dianManager preOrder19dianManager = new PreOrder19dianManager();

            if (refundAccount <= 0 || orderID == Guid.Empty)
            {
                return -2;
            }
            if (string.IsNullOrEmpty(refundDes))
            {
                return -6;
            }
            //退款成功的条件：点单支付，点单验证，点单未对账，退款金额小于支付金额
            //PreOrder19dianOperate preOperate = new PreOrder19dianOperate();
            //DataTable dtPreOrderInfo = preOperate.QueryOrderById(orderID);

            List<PreOrder19dianInfo> listPreOrder19dianInfo = new List<PreOrder19dianInfo>();
            PreOrder19dianManager preorder19dianMan = new PreOrder19dianManager();
            listPreOrder19dianInfo = preorder19dianMan.GetPreOrder19dianByOrderId(orderID);

            return FillPostMoney(refundAccount, refundDes, orderID, listPreOrder19dianInfo, preorder19dianMan);
        }

        /// <summary>
        /// 收银宝补差价退款特殊处理 add by zhujinlei 2015/06/30
        /// </summary>
        /// <param name="refundAccount"></param>
        /// <param name="refundDes"></param>
        /// <param name="orderID"></param>
        /// <param name="list"></param>
        /// <param name="preorder19dianMan"></param>
        /// <returns></returns>
        public static int FillPostMoney(double refundAccount, string refundDes, Guid orderID, List<PreOrder19dianInfo> list, PreOrder19dianManager preorder19dianMan)
        {
            int result = 0;
            // 首先获取订单表数据
            OrderManager objOrderManager = new OrderManager();
            var objOrder = objOrderManager.GetEntityById(orderID);

            if (objOrder == null)
            {
                return -4;
            }

            int orderStatus = Common.ToInt32(objOrder.Status);
            if (orderStatus == (int)VAPreorderStatus.Refund || orderStatus == (int)VAPreorderStatus.Overtime || orderStatus == (int)VAPreorderStatus.OriginalRefunding)
            {
                return -7;//当前点单状态为已退款，退款中，已过期，return
            }

            if (objOrder.IsShopConfirmed != 1)
            {
                return -5;
            }

            //商户端（收银宝）退款
            EmployeeOperate employeeOperate = new EmployeeOperate();
            string employeeID = string.Empty;
            try
            {
                employeeID = Common.ToString(((VAEmployeeLoginResponse)HttpContext.Current.Session["MerchantsTreasureUserInfo"]).employeeID);//收银宝
            }
            catch
            {
                try
                {
                    employeeID = Common.ToString(((VAEmployeeLoginResponse)HttpContext.Current.Session["UserInfo"]).employeeID); //老后台
                }
                catch
                {
                    employeeID = "0";
                }
            }

            // 已退款总额
            double refundMoneySumTotal = objOrder.RefundMoneySum;
            // 支付金额
            double prepaidSumTotal = objOrder.PrePaidSum;
            // 最大退款的额度
            double canRefundSumTotal = Common.ToDouble(prepaidSumTotal - refundMoneySumTotal);
            // 已结算的退款总额
            double refundMoneyClosedSumTotal = objOrder.RefundMoneyClosedSum;

            // 申请的退款金额
            double refundsum = Common.ToDouble(refundAccount);
            // 最终需要修改order表里面的退款金额
            double finalRefundSum = refundsum;

            // 处理步骤

            //01 获取第三方支付的总金额
            // 第三方支付的总额
            double thirdPayAmount = 0;
            foreach (PreOrder19dianInfo obj in list)
            {
                //已支付，已审核，未对账
                if (Common.ToInt32(obj.isPaid) == 1 && Common.ToInt32(obj.isApproved) == 0 && Common.ToInt32(obj.isShopConfirmed) == 1)
                {
                    //该点单使用第三方支付的金额
                    ThirdPartyPaymentInfo thirdPartyPaymentInfo = preorder19dianMan.SelectPreorderPayAmount(obj.preOrder19dianId);
                    //额外收取的钱
                    double extendPay = preorder19dianMan.SelectExtendPay(obj.preOrder19dianId);
                    //不退还系统额外收取的钱
                    double payAmount = thirdPartyPaymentInfo.Amount - extendPay;
                    thirdPayAmount = thirdPayAmount + payAmount;

                    // 赋值支付类型，用于排序退款，先退微信再退支付宝
                    obj.OrderPayType = (int)thirdPartyPaymentInfo.Type;
                }
            }

            // 排序，按先退微信再退支付宝顺序排
            list = list.OrderBy(p => p.OrderPayType).ToList();


            // 判断是否可以退款(申请的退款金额要小于最大可退款金额）
            if ((canRefundSumTotal - refundsum) > -0.001)
            {
                // 用于标识整个退款流程是否正确结束的标识，有一个失败就不发送成功短信和推送消息
                bool isRefundOk = false;
                int shopId = 0;
                string customerMobilephone = string.Empty;
                string appBuildID = string.Empty;
                long finalPreOrder19dianId = 0;
                // 是否涉及第三方退款
                bool hasThirdRefundMoney = false;
                // 用户ID
                long customerId = 0;
                // 记录退款日志用
                List<MoneyRefundOrder> listMoneyRefundOrder = new List<MoneyRefundOrder>();
                MoneyRefundOrder objMoneyRefundOrder = null;

                //VAGastronomistMobileAppLog数据库员工操作日志，单独开启线程，不用判断成功与否
                Common.RecordEmployeeOperateLogBySYB((int)VAEmployeeOperateLogOperatePageType.SYB_REFUND, (int)VAEmployeeOperateLogOperateType.UPDATE_OPERATE, "点单退款" + Common.ToString(refundsum) + "元");


                //如果申请的退款金额大于第三方支付的总金额，先把所有单子第三方支付的先退完，再依次处理粮票和红包
                //a、循环退每一个单子的第三方支付
                #region 第三方支付退款
                foreach (PreOrder19dianInfo obj in list)
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        //已支付，已审核，未对账
                        if (Common.ToInt32(obj.isPaid) == 1 && Common.ToInt32(obj.isApproved) == 0 && Common.ToInt32(obj.isShopConfirmed) == 1)
                        {
                            if (refundsum <= 0)
                            {
                                break;
                            }
                            shopId = obj.shopId;
                            int companyId = obj.companyId;
                            customerId = obj.customerId;
                            long preOrder19dianId = obj.preOrder19dianId;
                            // 记录正常点单的点单ID
                            if (obj.OrderType == OrderTypeEnum.Normal)
                            {
                                finalPreOrder19dianId = preOrder19dianId;
                            }
                            appBuildID = obj.appBuild;

                            // 已退款总额
                            double refundMoneySum = Convert.ToDouble(obj.refundMoneySum);
                            // 支付金额
                            double prepaidSum = Convert.ToDouble(obj.prePaidSum);
                            // 可以退款的额度
                            double canRefundSum = Common.ToDouble(prepaidSum - refundMoneySum);
                            // 已结算的退款总额
                            double refundMoneyClosedSum = Convert.ToDouble(obj.refundMoneyClosedSum);

                            //该点单使用第三方支付的金额
                            ThirdPartyPaymentInfo thirdPartyPaymentInfo = preorder19dianMan.SelectPreorderPayAmount(obj.preOrder19dianId);
                            //额外收取的钱
                            double extendPay = preorder19dianMan.SelectExtendPay(obj.preOrder19dianId);
                            //不退还系统额外收取的钱
                            double payAmount = thirdPartyPaymentInfo.Amount - extendPay;

                            // 第三方支付为零，不处理
                            if (payAmount <= 0)
                            {
                                continue;
                            }

                            // 已退款总额大于第三方支付的额度，需要处理下一条
                            if ((refundMoneySum - payAmount) > -0.001)
                            {
                                continue;
                            }

                            isRefundOk = false;

                            // 该单实际需要退还的第三方金额
                            double actualPayAmount = payAmount - refundMoneySum;
                            if (actualPayAmount > refundsum)
                                actualPayAmount = refundsum;

                            // 该单第三方退款完成后，还需要退还的申请退款额度
                            refundsum = refundsum - actualPayAmount;

                            //修改预点单状态
                            bool updatePreorder = false;
                            // 本单的实际退款金额等于最大可退款金额，表示全部退完了
                            if (Common.ToDecimal(actualPayAmount) == Common.ToDecimal(canRefundSum))
                            {
                                updatePreorder = preorder19dianMan.UpdatePreOrderRefundInfo(preOrder19dianId, VAPreorderStatus.OriginalRefunding, actualPayAmount);
                            }
                            else
                            {
                                updatePreorder = preorder19dianMan.UpdatePreOrderRefundMoneySum(preOrder19dianId, actualPayAmount);
                            }
                            bool flagUpdatePreOrderRefundMoneyClosedSum = preorder19dianMan.UpdatePreOrderRefundMoneyClosedSum(preOrder19dianId, actualPayAmount);
                            // 更新修改后的退款金额
                            obj.refundMoneySum = Common.ToDouble(obj.refundMoneySum) + actualPayAmount;
                            obj.refundMoneyClosedSum = Common.ToDouble(obj.refundMoneyClosedSum) + actualPayAmount;

                            //updatePreorder = preorder19dianMan.UpdatePreOrderRefundInfo(preOrder19dianId, VAPreorderStatus.OriginalRefunding, actualPayAmount);

                            // 添加退款日志相关备用
                            objMoneyRefundOrder = listMoneyRefundOrder.FirstOrDefault(m => m.preOrder19dianId == preOrder19dianId);
                            if (objMoneyRefundOrder != null)
                            {
                                objMoneyRefundOrder.moneyAmount = Common.ToDouble(objMoneyRefundOrder.moneyAmount) + actualPayAmount;
                            }
                            else
                            {
                                objMoneyRefundOrder = new MoneyRefundOrder();
                                objMoneyRefundOrder.moneyAmount = actualPayAmount;
                                objMoneyRefundOrder.preOrder19dianId = preOrder19dianId;
                                objMoneyRefundOrder.orderID = orderID;
                                listMoneyRefundOrder.Add(objMoneyRefundOrder);
                            }

                            CustomerManager customerMan = new CustomerManager();
                            DataTable dtCustomer = customerMan.SelectCustomer(customerId);

                            if (dtCustomer.Rows.Count == 1)
                            {
                                // 原路退款申请
                                bool flagOriginalRefundRecord = false; // 原路退款记录成功标识
                                customerMobilephone = Common.ToString(dtCustomer.Rows[0]["mobilePhoneNumber"]);
                                OriginalRoadRefundInfo originalRoadRefund = new OriginalRoadRefundInfo();
                                originalRoadRefund.refundAmount = actualPayAmount;
                                originalRoadRefund.type = VAOriginalRefundType.PREORDER;
                                originalRoadRefund.connId = preOrder19dianId;
                                originalRoadRefund.customerMobilephone = customerMobilephone;
                                originalRoadRefund.customerUserName = Common.ToString(dtCustomer.Rows[0]["UserName"]);
                                originalRoadRefund.status = (int)VAOriginalRefundStatus.REMITTING;
                                originalRoadRefund.employeeId = Common.ToInt32(employeeID);
                                switch (thirdPartyPaymentInfo.Type)
                                {
                                    case PayType.微信支付:
                                        originalRoadRefund.RefundPayType = RefundPayType.微信;
                                        break;
                                    case PayType.支付宝:
                                        originalRoadRefund.RefundPayType = RefundPayType.支付宝;
                                        break;
                                    default:
                                        break;
                                }
                                originalRoadRefund.id = preorder19dianMan.InsertOriginalRoadRefund(originalRoadRefund);
                                if (originalRoadRefund.id > 0)
                                {
                                    //记录原路退款记录
                                    flagOriginalRefundRecord = true;
                                    originalRoadRefund.tradeNo = thirdPartyPaymentInfo.tradeNo;
                                    //这里加入退款流程
                                    ThreadPool.QueueUserWorkItem(
                                        new RefundCallBack(preOrder19dianId, thirdPartyPaymentInfo,
                                            (float)originalRoadRefund.refundAmount, originalRoadRefund)
                                            .Refund);
                                }
                                else
                                {
                                    flagOriginalRefundRecord = false;
                                }

                                //记录退款记录
                                bool refundData = false;
                                refundData = Common.InsertRefundData(customerId, actualPayAmount, preOrder19dianId, "悠先收银原路退款");

                                // 修改用户在平台的消费金额
                                bool modifyVipInfo = customerMan.UpdateCustomerPartInfo(-actualPayAmount, 0, customerId);

                                //退款要更新支付明细表的退款金额及状态
                                Preorder19DianLineManager lineManager = new Preorder19DianLineManager();
                                Model.QueryObject.Preorder19DianLineQueryObject queryObject = new Model.QueryObject.Preorder19DianLineQueryObject()
                                {
                                    Preorder19DianId = preOrder19dianId
                                };
                                List<IPreorder19DianLine> orderLineList = lineManager.GetListByQuery(queryObject);
                                bool updateLine = false;
                                foreach (IPreorder19DianLine line in orderLineList)
                                {
                                    switch (line.PayType)
                                    {
                                        case (int)VAOrderUsedPayMode.ALIPAY:
                                        case (int)VAOrderUsedPayMode.WECHAT:
                                        case (int)VAOrderUsedPayMode.UNIONPAY:
                                            line.RefundAmount = line.RefundAmount + originalRoadRefund.refundAmount;//已经退的钱+本次退的第三方金额
                                            break;
                                        case (int)VAOrderUsedPayMode.BALANCE:
                                        case (int)VAOrderUsedPayMode.COUPON:
                                        case (int)VAOrderUsedPayMode.REDENVELOPE:
                                            continue;
                                    }
                                    updateLine = lineManager.Update(line);//更新每种支付类型的退款金额及状态
                                }

                                if (updatePreorder && flagOriginalRefundRecord && refundData && modifyVipInfo && flagUpdatePreOrderRefundMoneyClosedSum)
                                {
                                    scope.Complete();
                                    isRefundOk = true;
                                }
                                else
                                {
                                    result = -1; //退款失败
                                    refundsum = 0;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            // 未支付，未审核，已对账 不需要处理
                            continue;
                        }
                    }
                }
                #endregion

                //b、第三方支付都退完了， 再循环退每一个单子的粮票
                #region 粮票退款
                foreach (PreOrder19dianInfo obj in list)
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        //已支付，已审核，未对账
                        if (Common.ToInt32(obj.isPaid) == 1 && Common.ToInt32(obj.isApproved) == 0 && Common.ToInt32(obj.isShopConfirmed) == 1)
                        {
                            if (refundsum <= 0)
                            {
                                break;
                            }

                            shopId = obj.shopId;
                            int companyId = obj.companyId;
                            customerId = obj.customerId;
                            long preOrder19dianId = obj.preOrder19dianId;
                            // 记录正常点单的点单ID
                            if (obj.OrderType == OrderTypeEnum.Normal)
                            {
                                finalPreOrder19dianId = preOrder19dianId;
                            }
                            appBuildID = obj.appBuild;

                            // 已退款总额
                            double refundMoneySum = Convert.ToDouble(obj.refundMoneySum);
                            // 支付金额
                            double prepaidSum = Convert.ToDouble(obj.prePaidSum);
                            // 可以退款的额度
                            double canRefundSum = Common.ToDouble(prepaidSum - refundMoneySum);
                            // 已结算的退款总额
                            double refundMoneyClosedSum = Convert.ToDouble(obj.refundMoneyClosedSum);

                            //该点单使用第三方支付的金额
                            ThirdPartyPaymentInfo thirdPartyPaymentInfo = preorder19dianMan.SelectPreorderPayAmount(obj.preOrder19dianId);
                            //额外收取的钱
                            double extendPay = preorder19dianMan.SelectExtendPay(obj.preOrder19dianId);
                            //不退还系统额外收取的钱
                            double payAmount = thirdPartyPaymentInfo.Amount - extendPay;

                            RedEnvelopeConnPreOrderOperate redEnvelopeOperate = new RedEnvelopeConnPreOrderOperate();
                            //当前点单使用红包抵扣的金额
                            double redEnvelopeConsumed = redEnvelopeOperate.GetPayOrderConsumeRedEnvelopeAmount(preOrder19dianId);
                            //该点单使用粮票支付的金额
                            double foodCoupon = prepaidSum - payAmount - redEnvelopeConsumed;

                            // 粮票支付为零，不处理
                            if (foodCoupon <= 0)
                            {
                                continue;
                            }

                            // 已退款总额大于第三方支付的额度和粮票，需要处理下一条
                            if ((refundMoneySum - payAmount - foodCoupon) > -0.001)
                            {
                                continue;
                            }
                            isRefundOk = false;

                            // 该单实际需要退还的粮票金额
                            double actualPayAmount = foodCoupon - (refundMoneySum - payAmount);
                            if (actualPayAmount > refundsum)
                                actualPayAmount = refundsum;
                            // 该单第三方退款完成后，还需要退还的申请退款额度
                            refundsum = refundsum - actualPayAmount;

                            // 如果申请退款的金额已经小于0了， 表示不需要把余下的粮票全部退完了， 只需要退实际要求退款的量即可
                            if (refundsum < 0)
                            {
                                actualPayAmount = refundsum + actualPayAmount;
                                refundsum = 0;
                            }


                            //修改预点单状态
                            bool updatePreorder = false;
                            // 本单的实际退款金额等于最大可退款金额，表示全部退完了
                            if (Common.ToDecimal(actualPayAmount) == Common.ToDecimal(canRefundSum))
                            {
                                updatePreorder = preorder19dianMan.UpdatePreOrderRefundInfo(preOrder19dianId, VAPreorderStatus.Refund, actualPayAmount);
                            }
                            else
                            {
                                updatePreorder = preorder19dianMan.UpdatePreOrderRefundMoneySum(preOrder19dianId, actualPayAmount);
                            }
                            //updatePreorder = preorder19dianMan.UpdatePreOrderRefundInfo(preOrder19dianId, VAPreorderStatus.Refund, actualPayAmount);

                            Money19dianDetail money19dianDetail = new Money19dianDetail
                            {
                                customerId = customerId,
                                changeReason = Common.GetEnumDescription(VA19dianMoneyChangeReason.MONEY19DIAN_REFUND_PREORDER).Replace("{0}", Common.ToString(preOrder19dianId)),
                                changeTime = System.DateTime.Now,
                                flowNumber = SybMoneyOperate.CreateCustomerFlowNumber(customerId), //流水号
                                accountType = (int)AccountType.ORDER_OUTCOME,//来源类型，收银宝退款
                                accountTypeConnId = Common.ToString(preOrder19dianId),
                                inoutcomeType = (int)InoutcomeType.IN, //相对于用户退款就是收入
                                companyId = companyId,
                                shopId = shopId
                            };
                            money19dianDetail.changeValue = actualPayAmount;
                            // 修改用户余额（加上退款的粮票）
                            money19dianDetail.remainMoney = Money19dianDetailManager.GetCustomerRemainMoney(customerId) + actualPayAmount;
                            // 用户余额变理是否成功标识
                            bool flagUpdateCustomerMoneyRemain = true;
                            // 修改预订单打款金额是否成功标识
                            bool flagUpdatePreOrderRefundMoneyClosedSum = true;
                            flagUpdateCustomerMoneyRemain = SybMoneyCustomerOperate.AccountBalanceChanges(money19dianDetail);
                            flagUpdatePreOrderRefundMoneyClosedSum = preorder19dianMan.UpdatePreOrderRefundMoneyClosedSum(preOrder19dianId, money19dianDetail.changeValue);

                            // 更新修改后的退款金额
                            obj.refundMoneySum = Common.ToDouble(obj.refundMoneySum) + actualPayAmount;
                            obj.refundMoneyClosedSum = Common.ToDouble(obj.refundMoneyClosedSum) + actualPayAmount;

                            // 添加退款日志相关备用
                            objMoneyRefundOrder = listMoneyRefundOrder.FirstOrDefault(m => m.preOrder19dianId == preOrder19dianId);
                            if (objMoneyRefundOrder != null)
                            {
                                objMoneyRefundOrder.moneyAmount = Common.ToDouble(objMoneyRefundOrder.moneyAmount) + actualPayAmount;
                            }
                            else
                            {
                                objMoneyRefundOrder = new MoneyRefundOrder();
                                objMoneyRefundOrder.moneyAmount = actualPayAmount;
                                objMoneyRefundOrder.preOrder19dianId = preOrder19dianId;
                                objMoneyRefundOrder.orderID = orderID;
                                listMoneyRefundOrder.Add(objMoneyRefundOrder);
                            }

                            CustomerManager customerMan = new CustomerManager();
                            DataTable dtCustomer = customerMan.SelectCustomer(customerId);
                            customerMobilephone = Common.ToString(dtCustomer.Rows[0]["mobilePhoneNumber"]);

                            if (dtCustomer.Rows.Count == 1)
                            {
                                //记录退款记录
                                bool refundData = false;
                                refundData = Common.InsertRefundData(customerId, actualPayAmount, preOrder19dianId, "悠先服务粮票退款");

                                // 修改用户在平台的消费金额
                                bool modifyVipInfo = customerMan.UpdateCustomerPartInfo(-actualPayAmount, 0, customerId);

                                //退款要更新支付明细表的退款金额及状态
                                Preorder19DianLineManager lineManager = new Preorder19DianLineManager();
                                Model.QueryObject.Preorder19DianLineQueryObject queryObject = new Model.QueryObject.Preorder19DianLineQueryObject()
                                {
                                    Preorder19DianId = preOrder19dianId
                                };
                                List<IPreorder19DianLine> orderLineList = lineManager.GetListByQuery(queryObject);
                                bool updateLine = false;
                                foreach (IPreorder19DianLine line in orderLineList)
                                {
                                    switch (line.PayType)
                                    {
                                        case (int)VAOrderUsedPayMode.ALIPAY:
                                        case (int)VAOrderUsedPayMode.WECHAT:
                                        case (int)VAOrderUsedPayMode.UNIONPAY:
                                            continue;
                                        case (int)VAOrderUsedPayMode.BALANCE:
                                            line.RefundAmount = line.RefundAmount + actualPayAmount;//已经退的钱+本次退的粮票金额
                                            break;
                                        case (int)VAOrderUsedPayMode.COUPON:
                                        case (int)VAOrderUsedPayMode.REDENVELOPE:
                                            continue;
                                    }
                                    updateLine = lineManager.Update(line);//更新每种支付类型的退款金额及状态
                                }

                                if (updatePreorder && refundData && modifyVipInfo && flagUpdateCustomerMoneyRemain && flagUpdatePreOrderRefundMoneyClosedSum)
                                {
                                    scope.Complete();
                                    isRefundOk = true;
                                }
                                else
                                {
                                    result = -1;
                                    refundsum = 0;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            // 未支付，未审核，已对账 不需要处理
                            continue;
                        }
                    }
                }
                #endregion

                //c、粮票都退完了，再循环退每一个单子的红包
                #region 红包退款
                foreach (PreOrder19dianInfo obj in list)
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        //已支付，已审核，未对账
                        if (Common.ToInt32(obj.isPaid) == 1 && Common.ToInt32(obj.isApproved) == 0 && Common.ToInt32(obj.isShopConfirmed) == 1)
                        {
                            if (refundsum <= 0)
                            {
                                break;
                            }
                            //----------------------------------------------------------------------------------

                            //bool cancelAwardRedEnvelope = false;

                            ////本次退款是最后一次退款
                            //if (refundAccount + refundMoneySum - prePaidSum > -0.001)
                            //{
                            //    //全额退款时，中奖赠送的红包作废
                            //    AwardConnPreOrderOperate awardOperate = new AwardConnPreOrderOperate();
                            //    cancelAwardRedEnvelope = awardOperate.CancelAwardRedEnvelope(preOrder19dianId);
                            //}
                            //else
                            //{
                            //    cancelAwardRedEnvelope = true;
                            //}
                            //----------------------------------------------------------------------------------

                            shopId = obj.shopId;
                            int companyId = obj.companyId;
                            customerId = obj.customerId;
                            long preOrder19dianId = obj.preOrder19dianId;
                            // 记录正常点单的点单ID
                            if (obj.OrderType == OrderTypeEnum.Normal)
                            {
                                finalPreOrder19dianId = preOrder19dianId;
                            }
                            appBuildID = obj.appBuild;

                            // 已退款总额
                            double refundMoneySum = Convert.ToDouble(obj.refundMoneySum);
                            // 支付金额
                            double prepaidSum = Convert.ToDouble(obj.prePaidSum);
                            // 可以退款的额度
                            double canRefundSum = Common.ToDouble(prepaidSum - refundMoneySum);
                            // 已结算的退款总额
                            double refundMoneyClosedSum = Convert.ToDouble(obj.refundMoneyClosedSum);

                            //该点单使用第三方支付的金额
                            ThirdPartyPaymentInfo thirdPartyPaymentInfo = preorder19dianMan.SelectPreorderPayAmount(obj.preOrder19dianId);
                            //额外收取的钱
                            double extendPay = preorder19dianMan.SelectExtendPay(obj.preOrder19dianId);
                            //不退还系统额外收取的钱
                            double payAmount = thirdPartyPaymentInfo.Amount - extendPay;

                            RedEnvelopeConnPreOrderOperate redEnvelopeOperate = new RedEnvelopeConnPreOrderOperate();
                            //当前点单使用红包抵扣的金额
                            double redEnvelopeConsumed = redEnvelopeOperate.GetPayOrderConsumeRedEnvelopeAmount(preOrder19dianId);
                            //该点单使用粮票支付的金额
                            double foodCoupon = prepaidSum - payAmount - redEnvelopeConsumed;

                            // 红包支付为零，不处理
                            if (redEnvelopeConsumed <= 0)
                            {
                                continue;
                            }

                            // 已退款总额大于第三方支付的额度和粮票+红包，需要处理下一条
                            if ((refundMoneySum - payAmount - foodCoupon - redEnvelopeConsumed) > -0.001)
                            {
                                continue;
                            }

                            isRefundOk = false;

                            // 该单实际需要退还的红包金额
                            double actualPayAmount = redEnvelopeConsumed - (refundMoneySum - payAmount - foodCoupon);
                            if (actualPayAmount > refundsum)
                                actualPayAmount = refundsum;
                            // 该单第三方+粮票退款完成后，还需要退还的申请退款额度
                            refundsum = refundsum - actualPayAmount;

                            // 如果申请退款的金额已经小于0了， 表示不需要把余下的红包全部退完了， 只需要退实际要求退款的量即可
                            if (refundsum < 0)
                            {
                                actualPayAmount = refundsum + actualPayAmount;
                                refundsum = 0;
                            }

                            // 本单的实际退款金额等于最大可退款金额，表示全部退完了
                            bool updatePreorder = false;
                            if (Common.ToDecimal(actualPayAmount) == Common.ToDecimal(canRefundSum))
                            {
                                updatePreorder = preorder19dianMan.UpdatePreOrderRefundInfo(preOrder19dianId, VAPreorderStatus.Refund, actualPayAmount);
                            }
                            else
                            {
                                updatePreorder = preorder19dianMan.UpdatePreOrderRefundMoneySum(preOrder19dianId, actualPayAmount);
                            }

                            // 红包退款
                            bool flagUpdateRefundRedEnvelope = false;
                            if (actualPayAmount > 0.001)
                            {
                                SybMoneyCustomerOperate.RedEnvelopePartialRefund(preOrder19dianId, customerId, actualPayAmount, ref flagUpdateRefundRedEnvelope);
                            }
                            else
                            {
                                flagUpdateRefundRedEnvelope = true;
                            }

                            // 更新修改后的退款金额
                            obj.refundMoneySum = Common.ToDouble(obj.refundMoneySum) + actualPayAmount;
                            obj.refundMoneyClosedSum = Common.ToDouble(obj.refundMoneyClosedSum) + actualPayAmount;
                            obj.refundRedEnvelope = Common.ToDouble(obj.refundRedEnvelope) + actualPayAmount;

                            // 添加退款日志相关备用
                            objMoneyRefundOrder = listMoneyRefundOrder.FirstOrDefault(m => m.preOrder19dianId == preOrder19dianId);
                            if (objMoneyRefundOrder != null)
                            {
                                objMoneyRefundOrder.moneyAmount = Common.ToDouble(objMoneyRefundOrder.moneyAmount) + actualPayAmount;
                            }
                            else
                            {
                                objMoneyRefundOrder = new MoneyRefundOrder();
                                objMoneyRefundOrder.moneyAmount = actualPayAmount;
                                objMoneyRefundOrder.preOrder19dianId = preOrder19dianId;
                                objMoneyRefundOrder.orderID = orderID;
                                listMoneyRefundOrder.Add(objMoneyRefundOrder);
                            }

                            CustomerManager customerMan = new CustomerManager();
                            DataTable dtCustomer = customerMan.SelectCustomer(customerId);
                            customerMobilephone = Common.ToString(dtCustomer.Rows[0]["mobilePhoneNumber"]);

                            if (dtCustomer.Rows.Count == 1)
                            {   //记录退款记录
                                bool refundData = false;
                                refundData = Common.InsertRefundData(customerId, actualPayAmount, preOrder19dianId, "悠先服务红包退款");

                                // 修改用户在平台的消费金额
                                bool modifyVipInfo = customerMan.UpdateCustomerPartInfo(-actualPayAmount, 0, customerId);

                                //退款要更新支付明细表的退款金额及状态
                                Preorder19DianLineManager lineManager = new Preorder19DianLineManager();
                                Model.QueryObject.Preorder19DianLineQueryObject queryObject = new Model.QueryObject.Preorder19DianLineQueryObject()
                                {
                                    Preorder19DianId = preOrder19dianId
                                };
                                List<IPreorder19DianLine> orderLineList = lineManager.GetListByQuery(queryObject);
                                bool updateLine = false;
                                foreach (IPreorder19DianLine line in orderLineList)
                                {
                                    switch (line.PayType)
                                    {
                                        case (int)VAOrderUsedPayMode.ALIPAY:
                                        case (int)VAOrderUsedPayMode.WECHAT:
                                        case (int)VAOrderUsedPayMode.UNIONPAY:
                                        case (int)VAOrderUsedPayMode.BALANCE:
                                        case (int)VAOrderUsedPayMode.COUPON:
                                            continue;
                                        case (int)VAOrderUsedPayMode.REDENVELOPE:
                                            line.RefundAmount = line.RefundAmount + actualPayAmount;//已经退的钱+本次退的红包金额
                                            break;
                                    }
                                    updateLine = lineManager.Update(line);//更新每种支付类型的退款金额及状态
                                }

                                if (flagUpdateRefundRedEnvelope && refundData && modifyVipInfo && updatePreorder)
                                {
                                    scope.Complete();
                                    isRefundOk = true;
                                }
                                else
                                {
                                    result = -1;
                                    refundsum = 0;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            // 未支付，未审核，已对账 不需要处理
                            continue;
                        }
                    }
                }
                #endregion

                // 分步退款成功后， 统一发布短信和推送
                #region 推送
                if (isRefundOk)
                {
                    // 修改order表里面总退款和额度
                    // 退款的额度等于最大可退款额度，表示已退完
                    if (Common.ToDecimal(finalRefundSum) == Common.ToDecimal(canRefundSumTotal))
                    {
                        // 涉及第三方退款并且已退完， 把退款状态改成退款中
                        // 查找是否存在第三方支付
                        hasThirdRefundMoney = preorder19dianMan.GetThirdPay(orderID);
                        if (hasThirdRefundMoney)
                        {
                            preorder19dianMan.UpdateOrderRefundMoney(orderID, VAPreorderStatus.OriginalRefunding, finalRefundSum, finalRefundSum);
                            Updatepreorder19dianRefundMoney(orderID, VAPreorderStatus.OriginalRefunding);
                        }
                        else
                        {
                            var status = (VAPreorderStatus)new OrderManager().GetEntityById(orderID).Status;
                            if (status != VAPreorderStatus.OriginalRefunding)
                                status = VAPreorderStatus.Refund;

                            if (status != VAPreorderStatus.Refund && new Preorder19DianLineManager().IsMoneyPaymentOfOrderId(orderID))
                                status = VAPreorderStatus.OriginalRefunding;

                            // order表里的退款状态改成 已退款
                            preorder19dianMan.UpdateOrderRefundMoney(orderID, status, finalRefundSum, finalRefundSum);
                        }

                        // 全额退款修改用户在平台的消费次数 -1
                        CustomerManager customerMan = new CustomerManager();
                        bool modifyVipInfo = customerMan.UpdateCustomerPartInfo(0, -1, customerId);
                    }
                    else
                    {
                        // 没有退完，退款状态依然为已付款
                        preorder19dianMan.UpdateOrderRefundMoney(orderID, VAPreorderStatus.Prepaid, finalRefundSum, finalRefundSum);
                    }

                    var dateTime = System.DateTime.Now;
                    foreach (var item in listMoneyRefundOrder)
                    {
                        MoneyRefundDetail moneyRefundDetail = new MoneyRefundDetail
                        {
                            preOrder19dianId = Common.ToInt64(item.preOrder19dianId),
                            refundMoney = Common.ToDecimal(item.moneyAmount),
                            remark = refundDes.Trim(),
                            operUser = employeeID,
                            //不记录当前员工编号，后期无法查询当前的退款操作人
                            operTime = dateTime,
                            orderID = item.orderID
                        };
                        //退款日志
                        bool resultInsertMoneyRefundDetail = SybMoneyCustomerOperate.InsertMoneyRefundDetail(moneyRefundDetail);
                    }

                    ShopManager shopMan = new ShopManager();
                    DataTable dtShop = shopMan.SelectShop(shopId);
                    isRefundOk = true;
                    result = 1;
                    if (customerMobilephone.Length == 11)
                    {
                        string shopName = "";
                        if (dtShop.Rows.Count == 1)
                        {
                            shopName = Common.ToString(dtShop.Rows[0]["shopName"]);
                        }
                        string smsContent =
                            ConfigurationManager.AppSettings["sybRefundMessage"].Trim();
                        smsContent = smsContent.Replace("{0}", shopName);
                        smsContent = smsContent.Replace("{1}", Common.ToString(finalRefundSum));
                        Common.SendMessageBySms(customerMobilephone, smsContent);

                        CustomPushRecordOperate customPushRecordOperate = new CustomPushRecordOperate();
                        UniPushInfo uniPushInfo = new UniPushInfo()
                        {
                            customerPhone = customerMobilephone,
                            //preOrder19dianId = preOrder19dianId,
                            orderId = orderID,
                            shopName = shopName,
                            pushMessage = VAPushMessage.退款成功,
                            clientBuild = appBuildID
                        };
                        Thread refundThread = new Thread(customPushRecordOperate.UniPush);
                        refundThread.Start(uniPushInfo);
                    }
                }
                else
                {
                    result = -1;
                }
                #endregion

            }
            else
            {
                // 申请的退款金额不能大于最大退款额度
                result = -2;
            }
            return result;
        }

        /// <summary>
        /// 更新preorder19dian表中退款状态改为退款中
        /// </summary>
        /// <param name="orderID">订单ID</param>
        /// <param name="status">退款状态</param>
        /// <returns></returns>
        public static bool Updatepreorder19dianRefundMoney(Guid orderID, VAPreorderStatus status)
        {
            string strSql = string.Format(@"update preorder19dian set status ={0} where OrderId=@orderID", (int)status);
            SqlParameter[] para = {
					new SqlParameter("@orderID", SqlDbType.UniqueIdentifier) { Value = orderID}};
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int i = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql, para);
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

        /// <summary>
        /// 收银宝商户退款（子方法）备份
        /// </summary>
        /// <param name="refundAccount">退款金额</param>
        /// <param name="refundDes">退款原因</param>
        /// <param name="preOrder19dianId">点单流水号</param>
        /// <returns></returns>
        //public static int OrderRefundBak(double refundAccount, string refundDes, Guid preOrder19dianId)
        //{
        //    int result = 0;
        //    refundAccount = Common.ToDouble(refundAccount);
        //    PreOrder19dianManager preOrder19dianManager = new PreOrder19dianManager();

        //    if (refundAccount <= 0 || preOrder19dianId ==Guid.Empty)
        //    {
        //        return -2;
        //    }
        //    if (string.IsNullOrEmpty(refundDes))
        //    {
        //        return -6;
        //    }
        //    //退款成功的条件：点单支付，点单验证，点单未对账，退款金额小于支付金额
        //    PreOrder19dianOperate preOperate = new PreOrder19dianOperate();
        //    DataTable dtPreOrderInfo = preOperate.QueryPreOrderById(preOrder19dianId);
        //    if (dtPreOrderInfo.Rows.Count == 1)
        //    {
        //        int orderStatus = Common.ToInt32(dtPreOrderInfo.Rows[0]["status"]);
        //        if (orderStatus == (int)VAPreorderStatus.Refund || orderStatus == (int)VAPreorderStatus.Overtime || orderStatus == (int)VAPreorderStatus.OriginalRefunding)
        //        {
        //            return -7;//当前点单状态为已退款，退款中，已过期，return
        //        }
        //        if (Common.ToInt32(dtPreOrderInfo.Rows[0]["isShopConfirmed"]) == 1)
        //        {
        //            int companyId = Common.ToInt32(dtPreOrderInfo.Rows[0]["companyId"]);
        //            int shopId = Common.ToInt32(dtPreOrderInfo.Rows[0]["shopId"]);
        //            long customerId = Common.ToInt64(dtPreOrderInfo.Rows[0]["customerId"]);
        //            double prePaidSum = Common.ToDouble(dtPreOrderInfo.Rows[0]["prePaidSum"]);//该点单支付金额
        //            double refundMoneySum = Common.ToDouble(dtPreOrderInfo.Rows[0]["refundMoneySum"]);//该点单已退款的总金额
        //            double canRefundMoney = Common.ToDouble(prePaidSum - refundMoneySum);//当前点单可退款的最大金额
        //            double refundMoneyClosedSum = Common.ToDouble(dtPreOrderInfo.Rows[0]["refundMoneyClosedSum"]);//已结算的退款总额
        //            //退款成功的条件：点单支付，点单验证，点单未对账，退款金额小于（支付金额—已退款金额（默认为0））
        //            if ((canRefundMoney - refundAccount >= -0.001) && Common.ToInt32(dtPreOrderInfo.Rows[0]["isPaid"]) == 1 && Common.ToInt32(dtPreOrderInfo.Rows[0]["isApproved"]) == 0)
        //            {
        //                bool flag = false;//标记是否推送
        //                //VAGastronomistMobileAppLog数据库员工操作日志，单独开启线程，不用判断成功与否
        //                Common.RecordEmployeeOperateLogBySYB((int)VAEmployeeOperateLogOperatePageType.SYB_REFUND, (int)VAEmployeeOperateLogOperateType.UPDATE_OPERATE, "点单退款" + Common.ToString(refundAccount) + "元");

        //                //商户端（收银宝）退款
        //                EmployeeOperate employeeOperate = new EmployeeOperate();
        //                string employeeID = string.Empty;
        //                try
        //                {
        //                    employeeID = Common.ToString(((VAEmployeeLoginResponse)HttpContext.Current.Session["MerchantsTreasureUserInfo"]).employeeID);//收银宝
        //                }
        //                catch
        //                {
        //                    try
        //                    {
        //                        employeeID = Common.ToString(((VAEmployeeLoginResponse)HttpContext.Current.Session["UserInfo"]).employeeID); //老后台
        //                    }
        //                    catch
        //                    {
        //                        employeeID = "0";
        //                    }
        //                }
        //                MoneyRefundDetail moneyRefundDetail = new MoneyRefundDetail
        //                {
        //                    preOrder19dianId = Common.ToInt64(preOrder19dianId),
        //                    refundMoney = Common.ToDouble(refundAccount),
        //                    remark = refundDes.Trim(),
        //                    operUser = employeeID,
        //                    operTime = System.DateTime.Now
        //                };
        //                //原路退回
        //                OriginalRoadRefundInfo originalRoadRefund = new OriginalRoadRefundInfo();
        //                Money19dianDetail money19dianDetail = new Money19dianDetail();
        //                ThirdPartyPaymentInfo thirdPartyPaymentInfo = preOrder19dianManager.SelectPreorderPayAmount(preOrder19dianId);//该点单使用第三方支付的金额
        //                double extendPay = preOrder19dianManager.SelectExtendPay(preOrder19dianId);//额外收取的钱
        //                double payAmount = thirdPartyPaymentInfo.Amount - extendPay;//不退还系统额外收取的钱

        //                RedEnvelopeConnPreOrderOperate redEnvelopeOperate = new RedEnvelopeConnPreOrderOperate();
        //                bool flagUpdateRefundRedEnvelope = false;
        //                double redEnvelopeConsumed = redEnvelopeOperate.GetPayOrderConsumeRedEnvelopeAmount(preOrder19dianId);//当前点单使用红包抵扣的金额
        //                double foodCoupon = prePaidSum - payAmount - redEnvelopeConsumed;//该点单使用粮票支付的金额
        //                double refundedFoodCoupon = 0;//已退到粮票的钱
        //                if (refundMoneySum - payAmount < -0.001)
        //                {
        //                    refundedFoodCoupon = 0;//第三方还未退完，已退粮票必然为0
        //                }
        //                else
        //                {
        //                    if (refundMoneySum - payAmount - foodCoupon < -0.001)
        //                    {
        //                        refundedFoodCoupon = refundMoneySum - payAmount;
        //                    }
        //                    else
        //                    {
        //                        refundedFoodCoupon = foodCoupon;
        //                    }
        //                }
        //                double refundFoodCoupon = 0;//本次退到粮票的钱
        //                double refundRedEnvelope = 0;//本次退到红包的钱
        //                double moneyBackToRemain = Common.ToDouble((refundAccount + refundMoneySum) - payAmount); //需要退回到粮票和红包的金额

        //                if (moneyBackToRemain > 0)
        //                {//用户余额变更
        //                    if (moneyBackToRemain - refundAccount > 0.001)
        //                    {
        //                        moneyBackToRemain = refundAccount;
        //                    }

        //                    money19dianDetail.customerId = customerId;
        //                    money19dianDetail.changeReason = Common.GetEnumDescription(VA19dianMoneyChangeReason.MONEY19DIAN_REFUND_PREORDER).Replace("{0}", Common.ToString(preOrder19dianId));
        //                    money19dianDetail.changeTime = System.DateTime.Now;
        //                    money19dianDetail.flowNumber = SybMoneyOperate.CreateCustomerFlowNumber(customerId);//流水号
        //                    money19dianDetail.accountType = (int)AccountType.ORDER_OUTCOME;//来源类型，收银宝退款
        //                    money19dianDetail.accountTypeConnId = Common.ToString(preOrder19dianId);
        //                    money19dianDetail.inoutcomeType = (int)InoutcomeType.IN;//相对于用户退款就是收入
        //                    money19dianDetail.companyId = companyId;
        //                    money19dianDetail.shopId = shopId;

        //                    if (refundedFoodCoupon - foodCoupon < -0.001)//有要退到粮票的钱
        //                    {
        //                        refundFoodCoupon = foodCoupon - refundedFoodCoupon;//本次退到粮票的钱
        //                        if (moneyBackToRemain - refundFoodCoupon < -0.001)
        //                        {
        //                            refundFoodCoupon = moneyBackToRemain;
        //                        }
        //                        refundRedEnvelope = moneyBackToRemain - refundFoodCoupon;
        //                        money19dianDetail.changeValue = Common.ToDouble(refundFoodCoupon);
        //                        money19dianDetail.remainMoney = Common.ToDouble(Money19dianDetailManager.GetCustomerRemainMoney(customerId) + refundFoodCoupon);//
        //                        originalRoadRefund.refundAmount = Common.ToDouble(refundAccount - moneyBackToRemain); //以退金额大于第三方金额会出现0的情况
        //                    }
        //                    else
        //                    {
        //                        money19dianDetail.changeValue = Common.ToDouble(0);
        //                        money19dianDetail.remainMoney = Common.ToDouble(Money19dianDetailManager.GetCustomerRemainMoney(customerId) + 0);//

        //                        originalRoadRefund.refundAmount = refundAccount - moneyBackToRemain;

        //                        refundRedEnvelope = moneyBackToRemain;//红包返还
        //                    }
        //                    if (refundRedEnvelope > 0.001)
        //                    {
        //                        RedEnvelopePartialRefund(preOrder19dianId, customerId, refundRedEnvelope, ref flagUpdateRefundRedEnvelope);
        //                    }
        //                    else
        //                    {
        //                        flagUpdateRefundRedEnvelope = true;
        //                    }
        //                }
        //                else
        //                {
        //                    originalRoadRefund.refundAmount = refundAccount;
        //                }
        //                CustomerManager customerMan = new CustomerManager();
        //                DataTable dtCustomer = customerMan.SelectCustomer(customerId);
        //                string customerphone = Common.ToString(dtCustomer.Rows[0]["mobilePhoneNumber"]);
        //                double preOrderTotalAmount = Common.ToDouble(dtCustomer.Rows[0]["preOrderTotalAmount"]);
        //                int preOrderTotalQuantity = Common.ToInt32(dtCustomer.Rows[0]["preOrderTotalQuantity"]);
        //                int currentPlatformVipGrade = Common.ToInt32(dtCustomer.Rows[0]["currentPlatformVipGrade"]);

        //                originalRoadRefund.type = VAOriginalRefundType.PREORDER;
        //                originalRoadRefund.connId = preOrder19dianId;
        //                originalRoadRefund.customerMobilephone = customerphone;
        //                originalRoadRefund.customerUserName = Common.ToString(dtCustomer.Rows[0]["UserName"]);
        //                originalRoadRefund.status = (int)VAOriginalRefundStatus.REMITTING;
        //                originalRoadRefund.employeeId = ((VAEmployeeLoginResponse)HttpContext.Current.Session["MerchantsTreasureUserInfo"]).employeeID;

        //                using (TransactionScope scope = new TransactionScope())
        //                {
        //                    bool resultLog = Common.InsertRefundData(customerId, refundAccount, preOrder19dianId, "悠先收银原路退款"); //VAGastronomistMobileApp数据库退款日志
        //                    bool resultUpdatePreOrder = false;
        //                    bool modifyVipInfo = false;
        //                    double cumulativeAmount = preOrderTotalAmount - refundAccount;
        //                    if (payAmount > 0.009)
        //                    {//有支付
        //                        if ((payAmount - refundAccount - refundMoneySum) < 0.001)
        //                        {//原路的全部退完了
        //                            if (payAmount - refundMoneySum < 0.001)
        //                            {//当次退款前原路已经全部退完了
        //                                if (refundAccount + refundMoneySum - prePaidSum > -0.001)
        //                                {
        //                                    if (refundMoneyClosedSum - refundMoneySum > -0.001)
        //                                    {
        //                                        resultUpdatePreOrder = preOrder19dianManager.UpdatePreOrderRefundInfo(preOrder19dianId, VAPreorderStatus.Refund, refundAccount);
        //                                    }
        //                                    else
        //                                    {
        //                                        resultUpdatePreOrder = preOrder19dianManager.UpdatePreOrderRefundInfo(preOrder19dianId, VAPreorderStatus.OriginalRefunding, refundAccount);
        //                                    }
        //                                    modifyVipInfo = Common.ModifyUserPlatVip(customerId, preOrderTotalQuantity, currentPlatformVipGrade, -refundAccount, cumulativeAmount, true);
        //                                }
        //                                else
        //                                {
        //                                    resultUpdatePreOrder = preOrder19dianManager.UpdatePreOrderRefundMoneySum(preOrder19dianId, refundAccount);
        //                                    modifyVipInfo = Common.ModifyUserPlatVip(customerId, preOrderTotalQuantity, currentPlatformVipGrade, -refundAccount, cumulativeAmount, false);
        //                                }
        //                            }
        //                            else
        //                            {
        //                                if (refundAccount + refundMoneySum - prePaidSum > -0.001)
        //                                {
        //                                    resultUpdatePreOrder = preOrder19dianManager.UpdatePreOrderRefundInfo(preOrder19dianId, VAPreorderStatus.OriginalRefunding, refundAccount);
        //                                    modifyVipInfo = Common.ModifyUserPlatVip(customerId, preOrderTotalQuantity, currentPlatformVipGrade, -refundAccount, cumulativeAmount, true);
        //                                }
        //                                else
        //                                {
        //                                    resultUpdatePreOrder = preOrder19dianManager.UpdatePreOrderRefundMoneySum(preOrder19dianId, refundAccount);
        //                                    modifyVipInfo = Common.ModifyUserPlatVip(customerId, preOrderTotalQuantity, currentPlatformVipGrade, -refundAccount, cumulativeAmount, false);
        //                                }
        //                            }
        //                        }
        //                        else
        //                        {
        //                            resultUpdatePreOrder = preOrder19dianManager.UpdatePreOrderRefundMoneySum(preOrder19dianId, refundAccount);
        //                        }
        //                    }
        //                    else
        //                    {
        //                        if (refundAccount + refundMoneySum - prePaidSum > -0.001)
        //                        {
        //                            resultUpdatePreOrder = preOrder19dianManager.UpdatePreOrderRefundInfo(preOrder19dianId, VAPreorderStatus.Refund, refundAccount);
        //                            modifyVipInfo = Common.ModifyUserPlatVip(customerId, preOrderTotalQuantity, currentPlatformVipGrade, -refundAccount, cumulativeAmount, true);
        //                        }
        //                        else
        //                        {
        //                            resultUpdatePreOrder = preOrder19dianManager.UpdatePreOrderRefundMoneySum(preOrder19dianId, refundAccount);
        //                            modifyVipInfo = Common.ModifyUserPlatVip(customerId, preOrderTotalQuantity, currentPlatformVipGrade, -refundAccount, cumulativeAmount, false);
        //                        }
        //                    }
        //                    bool resultInsertMoneyRefundDetail = InsertMoneyRefundDetail(moneyRefundDetail);//退款日志
        //                    EmployeePointOperate pointOperate = new EmployeePointOperate();
        //                    //更新当前服务员积分信息
        //                    //bool opintOperateFlag = pointOperate.RefundOpdatePoint(dtPreOrderInfo, Common.ToInt32(employeeID), refundAccount);
        //                    bool flagOriginalRefundRecord = false;
        //                    if (originalRoadRefund.refundAmount > 0.009)
        //                    {
        //                        switch (thirdPartyPaymentInfo.Type)
        //                        {
        //                            case PayType.微信支付:
        //                                originalRoadRefund.RefundPayType = RefundPayType.微信;
        //                                break;
        //                            case PayType.支付宝:
        //                                originalRoadRefund.RefundPayType = RefundPayType.支付宝;
        //                                break;
        //                            default:
        //                                break;
        //                        }

        //                        originalRoadRefund.id =
        //                            preOrder19dianManager.InsertOriginalRoadRefund(originalRoadRefund);
        //                        if (originalRoadRefund.id > 0)
        //                        {//记录原路退款记录
        //                            flagOriginalRefundRecord = true;
        //                            originalRoadRefund.tradeNo = thirdPartyPaymentInfo.tradeNo;
        //                            //这里加入退款流程
        //                            ThreadPool.QueueUserWorkItem(
        //                                new RefundCallBack(preOrder19dianId, thirdPartyPaymentInfo,
        //                                    (float)originalRoadRefund.refundAmount, originalRoadRefund).Refund);
        //                        }
        //                        else
        //                        {
        //                            flagOriginalRefundRecord = false;
        //                        }
        //                    }
        //                    else
        //                    {
        //                        flagOriginalRefundRecord = true;
        //                    }

        //                    bool flagUpdateCustomerMoneyRemain = true;
        //                    bool flagUpdatePreOrderRefundMoneyClosedSum = true;
        //                    if (moneyBackToRemain > 0)
        //                    {
        //                        flagUpdateCustomerMoneyRemain = SybMoneyCustomerOperate.AccountBalanceChanges(money19dianDetail);
        //                        flagUpdatePreOrderRefundMoneyClosedSum = preOrder19dianManager.UpdatePreOrderRefundMoneyClosedSum(preOrder19dianId, money19dianDetail.changeValue);
        //                    }

        //                    //退款要更新支付明细表的退款金额及状态，Add at 2015-4-15
        //                    Preorder19DianLineManager lineManager = new Preorder19DianLineManager();
        //                    Model.QueryObject.Preorder19DianLineQueryObject queryObject = new Model.QueryObject.Preorder19DianLineQueryObject()
        //                    {
        //                        Preorder19DianId = preOrder19dianId
        //                    };
        //                    List<IPreorder19DianLine> orderLineList = lineManager.GetListByQuery(queryObject);
        //                    bool updateLine = false;
        //                    foreach (IPreorder19DianLine line in orderLineList)
        //                    {
        //                        switch (line.PayType)
        //                        {
        //                            case (int)VAOrderUsedPayMode.ALIPAY:
        //                            case (int)VAOrderUsedPayMode.WECHAT:
        //                            case (int)VAOrderUsedPayMode.UNIONPAY:
        //                                line.RefundAmount = line.RefundAmount + originalRoadRefund.refundAmount;//已经退的钱+本次退的第三方金额
        //                                break;
        //                            case (int)VAOrderUsedPayMode.BALANCE:
        //                                line.RefundAmount = line.RefundAmount + refundFoodCoupon;//已经退的钱+本次退到粮票的钱
        //                                break;
        //                            case (int)VAOrderUsedPayMode.COUPON:
        //                                line.RefundAmount = 0;//商户退款，抵扣券不返还
        //                                break;
        //                            case (int)VAOrderUsedPayMode.REDENVELOPE:
        //                                line.RefundAmount = line.RefundAmount + refundRedEnvelope;//已经退的钱+本次退到红包的钱
        //                                break;
        //                        }
        //                        updateLine = lineManager.Update(line);//更新每种支付类型的退款金额及状态
        //                    }

        //                    if (resultLog && resultInsertMoneyRefundDetail && flagOriginalRefundRecord && resultUpdatePreOrder && flagUpdateCustomerMoneyRemain && flagUpdatePreOrderRefundMoneyClosedSum && updateLine)//&& resultViewallocAmount&& opintOperateFlag
        //                    {
        //                        scope.Complete();
        //                        result = 1;
        //                        flag = true;
        //                    }
        //                    else
        //                    {
        //                        LogManager.WriteLog(LogFile.Error, string.Format("{0:s}:{1},{2},{3}{4},{5},{6}", DateTime.Now, resultLog, resultInsertMoneyRefundDetail, flagOriginalRefundRecord, resultUpdatePreOrder, flagUpdateCustomerMoneyRemain, flagUpdatePreOrderRefundMoneyClosedSum));//resultViewallocAmount,opintOperateFlag, 
        //                        result = -1;
        //                    }
        //                }
        //                if (flag)
        //                {
        //                    if (customerphone.Length == 11)
        //                    {
        //                        string shopName = "";
        //                        ShopManager shopMan = new ShopManager();
        //                        DataTable dtShop = shopMan.SelectShop(shopId);
        //                        if (dtShop.Rows.Count == 1)
        //                        {
        //                            shopName = Common.ToString(dtShop.Rows[0]["shopName"]);
        //                        }
        //                        string smsContent = ConfigurationManager.AppSettings["sybRefundMessage"].Trim();
        //                        smsContent = smsContent.Replace("{0}", shopName);
        //                        smsContent = smsContent.Replace("{1}", Common.ToString(refundAccount));
        //                        Common.SendMessageBySms(customerphone, smsContent);

        //                        CustomPushRecordOperate customPushRecordOperate = new CustomPushRecordOperate();
        //                        UniPushInfo uniPushInfo = new UniPushInfo()
        //                        {
        //                            customerPhone = customerphone,
        //                            preOrder19dianId = preOrder19dianId,
        //                            shopName = shopName,
        //                            pushMessage = VAPushMessage.退款成功,
        //                            clientBuild = dtPreOrderInfo.Rows[0]["appBuild"].ToString()
        //                        };
        //                        Thread refundThread = new Thread(customPushRecordOperate.UniPush);
        //                        refundThread.Start(uniPushInfo);
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                result = -2;
        //            }
        //        }
        //        else
        //        {
        //            result = -5;
        //        }
        //    }
        //    else
        //    {
        //        result = -4;
        //    }
        //    return result;
        //}

        public static void RedEnvelopePartialRefund(long preOrder19dianId, long customerId, double refundRedEnvelope, ref bool flagUpdateRefundRedEnvelope)
        {
            PreOrder19dianManager preOrder19dianManager = new PreOrder19dianManager();
            RedEnvelopeManager redEnvelopeManager = new RedEnvelopeManager();
            //先抓出该点单使用的所有红包
            List<RedEnvelopeRefund> connPreOrder = redEnvelopeManager.SelectRedEnvelopeRefund(preOrder19dianId);
            if (connPreOrder != null && connPreOrder.Count > 0)
            {
                //1. 更新点单表退款相关金额                                   
                bool a = preOrder19dianManager.UpdatePreOrderRefundRedEnvelope(preOrder19dianId, refundRedEnvelope, refundRedEnvelope);

                //2. 更新红包Detail表，支付记录应该加上的钱
                //bool b = true;
                //b = redEnvelopeManager.updateRedEnvelopePaidAmount(preOrder19dianId, refundRedEnvelope);
                //if (b)
                //{
                //    b = redEnvelopeManager.UpdateCustomerRedEnvelope2(customerId, refundRedEnvelope);
                //}

                //3.红包表应该更改状态的 RedEnvelopeId 
                bool c = true;
                foreach (RedEnvelopeRefund order in connPreOrder)
                {
                    if (refundRedEnvelope > 0.001)
                    {
                        if (refundRedEnvelope - order.currectUsedAmount > 0.001)//总退款金额大于当前红包抵扣金额
                        {
                            if (refundRedEnvelope - order.canRefundAmount > 0.001)//要退款的钱大于当前可退最大金额
                            {
                                c = redEnvelopeManager.UpdateRedEnvelopeStatus(order.redEnvelopeId, order.canRefundAmount);//只退红包的 当前可退最大金额
                                refundRedEnvelope = refundRedEnvelope - order.canRefundAmount;
                            }
                            else
                            {
                                c = redEnvelopeManager.UpdateRedEnvelopeStatus(order.redEnvelopeId, order.currectUsedAmount);//只退红包抵扣的钱
                                refundRedEnvelope = refundRedEnvelope - order.currectUsedAmount;//把退到当前红包的钱扣掉
                            }
                        }
                        else
                        {
                            if (refundRedEnvelope - order.canRefundAmount > 0.001)//要退款的钱大于当前可退最大金额
                            {
                                c = redEnvelopeManager.UpdateRedEnvelopeStatus(order.redEnvelopeId, order.canRefundAmount);//只退红包的 当前可退最大金额
                                refundRedEnvelope = refundRedEnvelope - order.canRefundAmount;
                            }
                            else
                            {
                                c = redEnvelopeManager.UpdateRedEnvelopeStatus(order.redEnvelopeId, refundRedEnvelope);//直接退当前要退的钱
                                refundRedEnvelope = refundRedEnvelope - refundRedEnvelope;
                            }
                        }
                        if (!c)
                        {
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
                if (a && c)
                {
                    flagUpdateRefundRedEnvelope = true;
                }
                else
                {
                    flagUpdateRefundRedEnvelope = false;
                }
            }
            else
            {
                flagUpdateRefundRedEnvelope = false;
            }
        }
    }
}
