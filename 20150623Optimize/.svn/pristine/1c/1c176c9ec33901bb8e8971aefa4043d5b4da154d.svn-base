using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Transactions;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.Model.Interface;
using VAGastronomistMobileApp.SQLServerDAL;
using System.Web;
using System.Threading;
using System.Configuration;
using VA.AllNotifications;
using VAGastronomistMobileApp.WebPageDll.Adjunction;
using VAGastronomistMobileApp.Model.Adjunction;


namespace VAGastronomistMobileApp.WebPageDll
{
    /// <summary>
    /// 功能描述:收银宝账户体系类-商户部分
    /// 创建标识:罗国华 20131120
    /// </summary>
    public class SybMoneyMerchantOperate
    {
        /// <summary>
        /// 功能描述：对账操作  待处理 所有的预点单流水号需要改为long类型
        /// </summary>
        /// <param name="preOrder19DianId">点单流水号</param>
        /// <param name="employeeId">用户编号</param>
        /// <returns></returns>
        public string ApproveMoneyMerchant(long preOrder19DianId, int employeeId)
        {
            PreOrder19dianOperate preOrder19DianOperate = new PreOrder19dianOperate();
            SybMoneyMerchantManager moneyMerchantManager = new SybMoneyMerchantManager();
            EmployeeOperate employeeOperate = new EmployeeOperate();
            CustomerServiceOperateLogOperate customerServiceOperateLogOperate = new CustomerServiceOperateLogOperate();
            CompanyOperate companyOperate = new CompanyOperate();
            PreOrder19dianManager preOrder19DianMan = new PreOrder19dianManager();
            EmployeePointLogOperate pointLogOper = new EmployeePointLogOperate();
            ShopVIPOperate shopVipOper = new ShopVIPOperate();
            const int approveTag = (int)VAPreorderIsApproved.APPROVED;//操作类型，对账

            DataTable dtOrder = preOrder19DianOperate.QueryPreOrderById(preOrder19DianId);
            if (dtOrder.Rows.Count != 1)
            {
                return "-1"; //判断预订单存在
            }
            int isShopConfirmed = Common.ToInt32(dtOrder.Rows[0]["isShopConfirmed"]);//入座
            int isApproved = Common.ToInt32(dtOrder.Rows[0]["isApproved"]);//对账
            if (isApproved == 1 || isShopConfirmed != 1)//已对账，或者未入座
            {
                return "-2";
            }
            int shopId = Common.ToInt32(dtOrder.Rows[0]["shopID"].ToString());
            int companyId = Common.ToInt32(dtOrder.Rows[0]["companyID"].ToString());
            string flowNum = Common.ToString(SybMoneyOperate.CreateMerchantFlowNumber(shopId));
            double remainMoney = moneyMerchantManager.GetShopInfoRemainMoney(shopId);
            double paySum = Common.ToDouble(dtOrder.Rows[0]["prePaidSum"]);
            double refundMoneySum = Common.ToDouble(dtOrder.Rows[0]["refundMoneySum"]);
            long customerId = Common.ToInt32(dtOrder.Rows[0]["customerId"]);

            EmployeeInfo employeeInfo = employeeOperate.QueryEmployee(employeeId);
            if (employeeInfo == null || employeeInfo.EmployeeID <= 0)
            {
                return "0";
            }
            string employeeName = employeeInfo.EmployeeFirstName;
            string employeePosition = employeeInfo.position;


            #region 计算佣金

            ShopOperate shopOperate = new ShopOperate();

            double ratio = shopOperate.getViewallocCommissionValue(shopId);//门店中的佣金比例
            double forViewalloc = Math.Round((paySum - refundMoneySum) * ratio, 2);

            //double forViewalloc = 0;
            //CompanyInfo comInfo = companyOperate.QueryCompany(companyId);
            //if (comInfo == null || comInfo.companyID <= 0)
            //{
            //    return "0";
            //}
            //if (Common.ToInt32(comInfo.viewallocCommissionType) == (int)VACommissionType.Proportion)//佣金按比例收取
            //{
            //    forViewalloc = Math.Round((paySum - refundMoneySum) * comInfo.viewallocCommissionValue, 2);
            //}
            //else//固定金额
            //{
            //    if (comInfo.viewallocCommissionValue < paySum - refundMoneySum) //如果账单金额小于等于友络佣金数额，佣金数额变为账单金额
            //    {
            //        forViewalloc = Common.ToDouble(comInfo.viewallocCommissionValue);
            //    }
            //    else
            //    {
            //        forViewalloc = Common.ToDouble(paySum - refundMoneySum);
            //    }
            //}
            #endregion

            ShopMoney shopMoney = new ShopMoney();

            #region 给商户结账对应的明细 2015-6-24 砍掉此方法
            ////这个方法，银企直联项目中开启
            //var queryObject = new Model.QueryObject.Preorder19DianLineQueryObject()
            //{
            //    Preorder19DianId = preOrder19DianId
            //};
            //List<IPreorder19DianLine> preOrder19dianLines = Preorder19DianLineOperate.GetListByQuery(queryObject);
            //if (preOrder19dianLines != null && preOrder19dianLines.Any())
            //{
            //    foreach (IPreorder19DianLine line in preOrder19dianLines)
            //    {
            //        switch (line.PayType)
            //        {
            //            case (int)VAOrderUsedPayMode.ALIPAY:
            //                shopMoney.remainAlipayAmount = Math.Round((line.Amount - line.RefundAmount) * (1 - comInfo.viewallocCommissionValue), 2);
            //                break;
            //            case (int)VAOrderUsedPayMode.COUPON:
            //                break;
            //            case (int)VAOrderUsedPayMode.BALANCE:
            //                shopMoney.remainFoodCouponAmount = Math.Round((line.Amount - line.RefundAmount) * (1 - comInfo.viewallocCommissionValue), 2);
            //                break;
            //            case (int)VAOrderUsedPayMode.REDENVELOPE:
            //                shopMoney.remainRedEnvelopeAmount = Math.Round((line.Amount - line.RefundAmount) * (1 - comInfo.viewallocCommissionValue), 2);
            //                break;
            //            case (int)VAOrderUsedPayMode.WECHAT:
            //                shopMoney.remainWechatPayAmount = Math.Round((line.Amount - line.RefundAmount) * (1 - comInfo.viewallocCommissionValue), 2);
            //                break;
            //            default:
            //                break;
            //        }
            //    }
            //    //如果有佣金，明细要特殊处理
            //    if (comInfo.viewallocCommissionValue > 0)
            //    {
            //        //计算完成的明细
            //        double afterMoney = Math.Round(shopMoney.remainAlipayAmount + shopMoney.remainFoodCouponAmount + shopMoney.remainRedEnvelopeAmount + shopMoney.remainWechatPayAmount, 2);
            //        //点单除佣金的钱
            //        double realMoney = Math.Round((paySum - refundMoneySum) * (1 - comInfo.viewallocCommissionValue), 2);
            //        //误差
            //        double errorMoney = 0;
            //        //四舍五入之后明细总和 不等于 点单除佣金的钱
            //        if (afterMoney - realMoney > 0.001)
            //        {
            //            errorMoney = afterMoney - realMoney;//明细算多了
            //            //按照 粮票，红包，微信，支付宝的顺序减去差价
            //            if (shopMoney.remainFoodCouponAmount - errorMoney > 0.001)
            //            {
            //                shopMoney.remainFoodCouponAmount = Math.Round(shopMoney.remainFoodCouponAmount - errorMoney, 2);
            //            }
            //            else if (shopMoney.remainRedEnvelopeAmount - errorMoney > 0.001)
            //            {
            //                shopMoney.remainRedEnvelopeAmount = Math.Round(shopMoney.remainRedEnvelopeAmount - errorMoney, 2);
            //            }
            //            else if (shopMoney.remainWechatPayAmount - errorMoney > 0.001)
            //            {
            //                shopMoney.remainWechatPayAmount = Math.Round(shopMoney.remainWechatPayAmount - errorMoney, 2);
            //            }
            //            else
            //            {
            //                shopMoney.remainAlipayAmount = Math.Round(shopMoney.remainAlipayAmount - errorMoney, 2);
            //            }
            //        }
            //        if (realMoney - afterMoney > 0.001)
            //        {
            //            errorMoney = realMoney - afterMoney;//明细算少了
            //            shopMoney.remainFoodCouponAmount = Math.Round(shopMoney.remainFoodCouponAmount + errorMoney, 2);
            //        }
            //    }
            //}
            #endregion

            #region 记录门店VIP信息

            ShopVIP shopVip = new ShopVIP()
            {
                CityId = 0,
                ShopId = shopId,
                CustomerId = customerId,
                PreOrderTotalQuantity = 1,
                PreOrderTotalAmount = Common.ToDecimal(paySum - refundMoneySum),
                CreateTime = DateTime.Now,
                LastPreOrderTime = DateTime.Now,
                Id = 0
            };
            #endregion

            #region 员工操作日志

            CustomerServiceOperateLogInfo customerServiceOperateLogInfo = new CustomerServiceOperateLogInfo()
            {
                employeeId = employeeId,
                employeeName = employeeName,
                employeeOperateTime = DateTime.Now,
                employeeOperate = string.Format("用户ID：{0}，点单流水号：{1}，操作类型：对账", customerId, preOrder19DianId)
            };

            #endregion

            string val = "0";
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    //对账记录
                    bool flag1 = preOrder19DianMan.InsertPreOrderCheckInfo(preOrder19DianId, approveTag, employeeId, employeeName, employeePosition);
                    //修改点单状态为已对账
                    bool flag2 = preOrder19DianMan.ApprovePreOrder19dian(preOrder19DianId, approveTag);
                    //增加客服日志信息
                    bool flag3 = customerServiceOperateLogOperate.InsertCustomerServiceOperateLog(customerServiceOperateLogInfo) > 0;

                    bool flag4 = true;
                    #region
                    MoneyMerchantAccountDetail detail = new MoneyMerchantAccountDetail();
                    detail.shopId = shopId;
                    detail.companyId = companyId;
                    detail.flowNumber = flowNum;
                    detail.accountTypeConnId = preOrder19DianId.ToString();
                    detail.operUser = Common.ToString(employeeId);

                    if (forViewalloc > 0)//佣金不为零的时候插入付佣金给友络的流水
                    {
                        detail.accountMoney = Common.ToDouble(0 - forViewalloc);
                        detail.remainMoney = Common.ToDouble(remainMoney - forViewalloc);
                        detail.accountType = Common.ToInt32(AccountType.VIEWALLOC_COMMISSION);
                        detail.inoutcomeType = Common.ToInt32(InoutcomeType.OUT);
                        flag4 = moneyMerchantManager.InsertMoneyMerchantAccountDetail(detail);
                    }
                    if (refundMoneySum > 0)//退款不为零的时候插入退款的商户流水
                    {
                        detail.accountMoney = Common.ToDouble(0 - refundMoneySum);
                        detail.remainMoney = Common.ToDouble(remainMoney - forViewalloc - refundMoneySum);
                        detail.accountType = Common.ToInt32(AccountType.ORDER_OUTCOME);
                        detail.inoutcomeType = Common.ToInt32(InoutcomeType.OUT);
                        flag4 = moneyMerchantManager.InsertMoneyMerchantAccountDetail(detail);
                    }
                    detail.accountMoney = Common.ToDouble(paySum);
                    detail.remainMoney = Common.ToDouble(remainMoney - forViewalloc - refundMoneySum + paySum);
                    detail.accountType = Common.ToInt32(AccountType.ORDER_INCOME);
                    detail.inoutcomeType = Common.ToInt32(InoutcomeType.IN);
                    bool flag5 = moneyMerchantManager.InsertMoneyMerchantAccountDetail(detail);

                    #endregion

                    bool flag9 = true;//合并原来的flag6，flag7，flag9

                    shopMoney.remainMoney = Common.ToDouble(paySum - refundMoneySum - forViewalloc);
                    shopMoney.paySum = Common.ToDouble(paySum - refundMoneySum - forViewalloc);
                    shopMoney.remainCommissionAmount = forViewalloc;
                    shopMoney.shopId = shopId;

                    if (paySum > refundMoneySum)//退款金额小于支付金额，更新用户支付点单数量
                    {
                        flag9 = SybMoneyMerchantManager.UpdateShopInfoAboutMoney(shopMoney, true);
                    }
                    else
                    {
                        flag9 = SybMoneyMerchantManager.UpdateShopInfoAboutMoney(shopMoney);
                    }
                    bool flag10 = shopVipOper.OperateShopVIP(shopVip);

                    if (flag1 && flag2 && flag3 && flag4 && flag5 && flag9 && flag10)
                    {
                        scope.Complete();
                        val = "1";
                    }
                    else
                    {
                        val = "0";
                        LogDll.LogManager.WriteLog(LogDll.LogFile.Trace, DateTime.Now.ToString() + "--flag1：" + flag1 + "--flag2：" + flag2 + "--flag3：" + flag3 + "--flag4：" + flag4 + "--flag5：" + flag5 + "--flag9：" + flag9 + "--flag10：" + flag10);
                    }
                }
                catch (Exception e)
                {
                    //val = "0";
                    val = e.Message;
                }
                return val;
            }
        }

        public string ApproveMoneyMerchantByOrderId(Guid orderId, int employeeId)
        {
            PreOrder19dianOperate preOrder19DianOperate = new PreOrder19dianOperate();
            SybMoneyMerchantManager moneyMerchantManager = new SybMoneyMerchantManager();
            EmployeeOperate employeeOperate = new EmployeeOperate();
            CustomerServiceOperateLogOperate customerServiceOperateLogOperate = new CustomerServiceOperateLogOperate();
            CompanyOperate companyOperate = new CompanyOperate();
            PreOrder19dianManager preOrder19DianMan = new PreOrder19dianManager();
            EmployeePointLogOperate pointLogOper = new EmployeePointLogOperate();
            ShopVIPOperate shopVipOper = new ShopVIPOperate();
            const int approveTag = (int)VAPreorderIsApproved.APPROVED;//操作类型，对账

            DataTable dtOrder = preOrder19DianOperate.QueryPreOrderByOrderId(orderId);
            if (dtOrder.Rows.Count != 1)
            {
                return "-1"; //判断预订单存在
            }
            int isShopConfirmed = Common.ToInt32(dtOrder.Rows[0]["isShopConfirmed"]);//入座
            int isApproved = Common.ToInt32(dtOrder.Rows[0]["isApproved"]);//对账
            if (isApproved == 1 || isShopConfirmed != 1)//已对账，或者未入座
            {
                return "-2";
            }
            int shopId = Common.ToInt32(dtOrder.Rows[0]["shopID"].ToString());
            int companyId = Common.ToInt32(dtOrder.Rows[0]["companyID"].ToString());
            string flowNum = Common.ToString(SybMoneyOperate.CreateMerchantFlowNumber(shopId));
            double remainMoney = moneyMerchantManager.GetShopInfoRemainMoney(shopId);

            //double paySum = Common.ToDouble(dtOrder.Rows[0]["prePaidSum"]);
            //double refundMoneySum = Common.ToDouble(dtOrder.Rows[0]["refundMoneySum"]);

            OrderOperate orderOperate = new OrderOperate();
            OrderPaidDetail orderPaidDetail = orderOperate.GetOrderPaidDeatial(orderId);

            double paySum = Common.ToDouble(orderPaidDetail.PrePaidSum);
            double refundMoneySum = Common.ToDouble(orderPaidDetail.refundMoneySum);

            long customerId = Common.ToInt32(dtOrder.Rows[0]["customerId"]);

            EmployeeInfo employeeInfo = employeeOperate.QueryEmployee(employeeId);
            if (employeeInfo == null || employeeInfo.EmployeeID <= 0)
            {
                return "0";
            }
            string employeeName = employeeInfo.EmployeeFirstName;
            string employeePosition = employeeInfo.position;


            #region 计算佣金

            ShopOperate shopOperate = new ShopOperate();

            double ratio = shopOperate.getViewallocCommissionValue(shopId);//门店中的佣金比例
            double forViewalloc = Math.Round((paySum - refundMoneySum) * ratio, 2);

            //double forViewalloc = 0;
            //CompanyInfo comInfo = companyOperate.QueryCompany(companyId);
            //if (comInfo == null || comInfo.companyID <= 0)
            //{
            //    return "0";
            //}
            //if (Common.ToInt32(comInfo.viewallocCommissionType) == (int)VACommissionType.Proportion)//佣金按比例收取
            //{
            //    forViewalloc = Math.Round((paySum - refundMoneySum) * comInfo.viewallocCommissionValue, 2);
            //}
            //else//固定金额
            //{
            //    if (comInfo.viewallocCommissionValue < paySum - refundMoneySum) //如果账单金额小于等于友络佣金数额，佣金数额变为账单金额
            //    {
            //        forViewalloc = Common.ToDouble(comInfo.viewallocCommissionValue);
            //    }
            //    else
            //    {
            //        forViewalloc = Common.ToDouble(paySum - refundMoneySum);
            //    }
            //}
            #endregion

            ShopMoney shopMoney = new ShopMoney();

            #region 给商户结账对应的明细 2015-6-24 砍掉此方法
            ////这个方法，银企直联项目中开启
            //var queryObject = new Model.QueryObject.Preorder19DianLineQueryObject()
            //{
            //    Preorder19DianId = preOrder19DianId
            //};
            //List<IPreorder19DianLine> preOrder19dianLines = Preorder19DianLineOperate.GetListByQuery(queryObject);
            //if (preOrder19dianLines != null && preOrder19dianLines.Any())
            //{
            //    foreach (IPreorder19DianLine line in preOrder19dianLines)
            //    {
            //        switch (line.PayType)
            //        {
            //            case (int)VAOrderUsedPayMode.ALIPAY:
            //                shopMoney.remainAlipayAmount = Math.Round((line.Amount - line.RefundAmount) * (1 - comInfo.viewallocCommissionValue), 2);
            //                break;
            //            case (int)VAOrderUsedPayMode.COUPON:
            //                break;
            //            case (int)VAOrderUsedPayMode.BALANCE:
            //                shopMoney.remainFoodCouponAmount = Math.Round((line.Amount - line.RefundAmount) * (1 - comInfo.viewallocCommissionValue), 2);
            //                break;
            //            case (int)VAOrderUsedPayMode.REDENVELOPE:
            //                shopMoney.remainRedEnvelopeAmount = Math.Round((line.Amount - line.RefundAmount) * (1 - comInfo.viewallocCommissionValue), 2);
            //                break;
            //            case (int)VAOrderUsedPayMode.WECHAT:
            //                shopMoney.remainWechatPayAmount = Math.Round((line.Amount - line.RefundAmount) * (1 - comInfo.viewallocCommissionValue), 2);
            //                break;
            //            default:
            //                break;
            //        }
            //    }
            //    //如果有佣金，明细要特殊处理
            //    if (comInfo.viewallocCommissionValue > 0)
            //    {
            //        //计算完成的明细
            //        double afterMoney = Math.Round(shopMoney.remainAlipayAmount + shopMoney.remainFoodCouponAmount + shopMoney.remainRedEnvelopeAmount + shopMoney.remainWechatPayAmount, 2);
            //        //点单除佣金的钱
            //        double realMoney = Math.Round((paySum - refundMoneySum) * (1 - comInfo.viewallocCommissionValue), 2);
            //        //误差
            //        double errorMoney = 0;
            //        //四舍五入之后明细总和 不等于 点单除佣金的钱
            //        if (afterMoney - realMoney > 0.001)
            //        {
            //            errorMoney = afterMoney - realMoney;//明细算多了
            //            //按照 粮票，红包，微信，支付宝的顺序减去差价
            //            if (shopMoney.remainFoodCouponAmount - errorMoney > 0.001)
            //            {
            //                shopMoney.remainFoodCouponAmount = Math.Round(shopMoney.remainFoodCouponAmount - errorMoney, 2);
            //            }
            //            else if (shopMoney.remainRedEnvelopeAmount - errorMoney > 0.001)
            //            {
            //                shopMoney.remainRedEnvelopeAmount = Math.Round(shopMoney.remainRedEnvelopeAmount - errorMoney, 2);
            //            }
            //            else if (shopMoney.remainWechatPayAmount - errorMoney > 0.001)
            //            {
            //                shopMoney.remainWechatPayAmount = Math.Round(shopMoney.remainWechatPayAmount - errorMoney, 2);
            //            }
            //            else
            //            {
            //                shopMoney.remainAlipayAmount = Math.Round(shopMoney.remainAlipayAmount - errorMoney, 2);
            //            }
            //        }
            //        if (realMoney - afterMoney > 0.001)
            //        {
            //            errorMoney = realMoney - afterMoney;//明细算少了
            //            shopMoney.remainFoodCouponAmount = Math.Round(shopMoney.remainFoodCouponAmount + errorMoney, 2);
            //        }
            //    }
            //}
            #endregion

            #region 记录门店VIP信息

            ShopVIP shopVip = new ShopVIP()
            {
                CityId = 0,
                ShopId = shopId,
                CustomerId = customerId,
                PreOrderTotalQuantity = 1,
                PreOrderTotalAmount = Common.ToDecimal(paySum - refundMoneySum),
                CreateTime = DateTime.Now,
                LastPreOrderTime = DateTime.Now,
                Id = 0
            };
            #endregion

            #region 员工操作日志

            CustomerServiceOperateLogInfo customerServiceOperateLogInfo = new CustomerServiceOperateLogInfo()
            {
                employeeId = employeeId,
                employeeName = employeeName,
                employeeOperateTime = DateTime.Now,
                employeeOperate = string.Format("用户ID：{0}，点单流水号：{1}，操作类型：对账", customerId, orderId)
            };

            #endregion

            string val = "0";
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    //对账记录
                    bool flag1 = preOrder19DianMan.InsertPreOrderCheckInfo(Common.ToInt64(dtOrder.Rows[0]["preOrder19DianId"]), approveTag, employeeId, employeeName, employeePosition);
                    //修改点单状态为已对账
                    bool flag2 = preOrder19DianMan.ApprovePreOrder19dianByOrderId(orderId, approveTag);
                    //增加客服日志信息
                    bool flag3 = customerServiceOperateLogOperate.InsertCustomerServiceOperateLog(customerServiceOperateLogInfo) > 0;

                    bool flag4 = true;
                    #region
                    MoneyMerchantAccountDetail detail = new MoneyMerchantAccountDetail();
                    detail.shopId = shopId;
                    detail.companyId = companyId;
                    detail.flowNumber = flowNum;
                    detail.accountTypeConnId = dtOrder.Rows[0]["preOrder19DianId"].ToString();
                    detail.operUser = Common.ToString(employeeId);

                    if (forViewalloc > 0)//佣金不为零的时候插入付佣金给友络的流水
                    {
                        detail.accountMoney = Common.ToDouble(0 - forViewalloc);
                        detail.remainMoney = Common.ToDouble(remainMoney - forViewalloc);
                        detail.accountType = Common.ToInt32(AccountType.VIEWALLOC_COMMISSION);
                        detail.inoutcomeType = Common.ToInt32(InoutcomeType.OUT);
                        flag4 = moneyMerchantManager.InsertMoneyMerchantAccountDetail(detail);
                    }
                    if (refundMoneySum > 0)//退款不为零的时候插入退款的商户流水
                    {
                        detail.accountMoney = Common.ToDouble(0 - refundMoneySum);
                        detail.remainMoney = Common.ToDouble(remainMoney - forViewalloc - refundMoneySum);
                        detail.accountType = Common.ToInt32(AccountType.ORDER_OUTCOME);
                        detail.inoutcomeType = Common.ToInt32(InoutcomeType.OUT);
                        flag4 = moneyMerchantManager.InsertMoneyMerchantAccountDetail(detail);
                    }
                    detail.accountMoney = Common.ToDouble(paySum);
                    detail.remainMoney = Common.ToDouble(remainMoney - forViewalloc - refundMoneySum + paySum);
                    detail.accountType = Common.ToInt32(AccountType.ORDER_INCOME);
                    detail.inoutcomeType = Common.ToInt32(InoutcomeType.IN);
                    bool flag5 = moneyMerchantManager.InsertMoneyMerchantAccountDetail(detail);

                    #endregion

                    bool flag9 = true;//合并原来的flag6，flag7，flag9

                    shopMoney.remainMoney = Common.ToDouble(paySum - refundMoneySum - forViewalloc);
                    shopMoney.paySum = Common.ToDouble(paySum - refundMoneySum - forViewalloc);
                    shopMoney.remainCommissionAmount = forViewalloc;
                    shopMoney.shopId = shopId;

                    if (paySum > refundMoneySum)//退款金额小于支付金额，更新用户支付点单数量
                    {
                        flag9 = SybMoneyMerchantManager.UpdateShopInfoAboutMoney(shopMoney, true);
                    }
                    else
                    {
                        flag9 = SybMoneyMerchantManager.UpdateShopInfoAboutMoney(shopMoney);
                    }
                    bool flag10 = shopVipOper.OperateShopVIP(shopVip);



                    //--------------------------------------------------------------------------------------------

                    bool flag11 = false;
                    //1、检查订单是否有中奖红包
                    AwardConnPreOrderOperate awardOperate = new AwardConnPreOrderOperate();

                    AwardConnPreOrder awardOrder = awardOperate.SelectAwardConnPreOrderByOrderId(Common.ToInt64(dtOrder.Rows[0]["preOrder19DianId"]));
                    if (awardOrder != null && awardOrder.Type == AwardType.PresentRedEnvelope)
                    {
                        flag11 = awardOperate.EffectAwardRedEnvelope(Common.ToInt64(dtOrder.Rows[0]["preOrder19DianId"]));
                    }
                    else
                    {
                        flag11 = true;
                    }

                    //--------------------------------------------------------------------------------------------



                    if (flag1 && flag2 && flag3 && flag4 && flag5 && flag9 && flag10 && flag11)
                    {
                        scope.Complete();
                        val = "1";
                    }
                    else
                    {
                        val = "0";
                        LogDll.LogManager.WriteLog(LogDll.LogFile.Trace, DateTime.Now.ToString() + "--flag1：" + flag1 + "--flag2：" + flag2 + "--flag3：" + flag3 + "--flag4：" + flag4 + "--flag5：" + flag5 + "--flag9：" + flag9 + "--flag10：" + flag10);
                    }
                }
                catch (Exception e)
                {
                    //val = "0";
                    val = e.Message;
                }
                return val;
            }
        }

        /// <summary>
        /// 功能描述：获取一段时间内的余额
        /// </summary>
        /// <param name="datetimestart"></param>
        /// <param name="datetimeend"></param>
        /// <param name="shopId"></param>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public double GetSumForTime(string datetimestart, string datetimeend, int shopId, int companyId)
        {
            Double sum = 0;
            SybMoneyMerchantManager sybMoneyMerchantManager = new SybMoneyMerchantManager();
            sum = Common.ToDouble(sybMoneyMerchantManager.GetShopInfoTotalMoney(shopId));
            //sum = Common.ToDouble(sybMoneyMerchantManager.GetAccountMoneySumForTime(datetimestart, datetimeend, shopId, companyId));
            return sum;
        }

        /// <summary>
        /// 功能描述：获取一段时间内的净余额
        /// </summary>
        /// <param name="datetimestart"></param>
        /// <param name="datetimeend"></param>
        /// <param name="shopId"></param>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public double GetCleanSumForTime(string datetimestart, string datetimeend, int shopId, int companyId)
        {
            Double sum = 0;
            SybMoneyMerchantManager sybMoneyMerchantManager = new SybMoneyMerchantManager();
            sum = Common.ToDouble(sybMoneyMerchantManager.GetShopInfoRemainMoney(shopId));
            //sum = Common.ToDouble(sybMoneyMerchantManager.GetAccountMoneyCleanSumForTime(datetimestart, datetimeend, shopId, companyId));
            return sum;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="datetimestart"></param>
        /// <param name="datetimeend"></param>
        /// <param name="shopId"></param>
        /// <param name="companyId"></param>
        /// <param name="type"></param>
        /// <param name="paystart"></param>
        /// <param name="payend"></param>
        /// <param name="mainkey"></param>
        /// <returns></returns>
        public double GetConditionSum(string datetimestart, string datetimeend, int shopId, int companyId, int type, double paystart, double payend, string mainkey)
        {
            Double sum = 0;
            SybMoneyMerchantManager sybMoneyMerchantManager = new SybMoneyMerchantManager();
            sum = Common.ToDouble(sybMoneyMerchantManager.GetAccountMoneyGetConditionSum(datetimestart, datetimeend, shopId, companyId, type, paystart, payend, mainkey));
            return sum;
        }

        /// <summary>
        /// 商户结账扣款
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="shopId"></param>
        /// <param name="accountMoney"></param>
        /// <param name="employeeID"></param>
        /// <param name="remarkDes"></param>
        /// <param name="accountId"></param>
        /// <returns></returns>
        public static bool MerchantCheckout(int companyId, int shopId, double accountMoney, int employeeID, string remarkDes, ref long accountId)
        {
            bool val = false;
            string flowNumber = SybMoneyOperate.CreateMerchantFlowNumber(shopId);
            double remainMoney = new SybMoneyMerchantManager().GetShopInfoRemainMoney(shopId);
            MoneyMerchantAccountDetail data = new MoneyMerchantAccountDetail()
            {
                companyId = companyId,
                shopId = shopId,
                accountMoney = Common.ToDouble(-accountMoney),
                remainMoney = Common.ToDouble(remainMoney - accountMoney),
                flowNumber = flowNumber,
                accountType = (int)AccountType.MERCHANT_CHECKOUT,
                inoutcomeType = (int)InoutcomeType.OUT,
                accountTypeConnId = "",
                operUser = employeeID.ToString(),
                operTime = DateTime.Now,
                remark = remarkDes
            };
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    accountId = MoneyMerchantAccountDetailManager.Insert(data);
                    SybMoneyMerchantManager.AddRemainMoney(shopId, -accountMoney);
                    scope.Complete();
                    val = true;
                }
                catch { }
            }
            return val;
        }
        /// <summary>
        /// 审核点单
        /// (掌中宝和收银宝调用模块)
        /// </summary>
        /// <param name="preOrder19dianId"></param>
        /// <param name="statusFlag"></param>
        /// <returns></returns>
        public int ConfrimPreOrder(long preOrder19dianId, int statusFlag, PreOrderConfirmOperater preOrderConfirmOperater, int employeeId = 0)//bool flag = false, 
        {
            int result = 0;
            bool flagSendNotification = false;
            UniPushInfo uniPushInfo = null;
            PreOrder19dianOperate preOrder19dianOperate = new PreOrder19dianOperate();

            //查询员工信息
            int employeeID = 0;
            try
            {
                employeeID = Common.ToInt32(((VAEmployeeLoginResponse)HttpContext.Current.Session["MerchantsTreasureUserInfo"]).employeeID);//收银宝
            }
            catch
            {
                try
                {
                    employeeID = Common.ToInt32(((VAEmployeeLoginResponse)HttpContext.Current.Session["UserInfo"]).employeeID); //老后台
                }
                catch
                {
                    employeeID = employeeId;
                }
            }
            EmployeeOperate employeeOperate = new EmployeeOperate();
            EmployeeInfo employeeInfo = employeeOperate.QueryEmployee(employeeID);
            //string employeeName = employeeInfo.EmployeeFirstName + employeeInfo.EmployeeLastName;//2014-2-23 取消LastName
            string employeeName = employeeInfo.EmployeeFirstName;
            string employeePosition = employeeInfo.position;

            //审核做三件事：插入日志，修改点单审核状态，记录当前审核操作信息，三张表操作

            DataTable dtpreOrder19dian = preOrder19dianOperate.QueryPreOrderById(preOrder19dianId);

            if (dtpreOrder19dian.Rows.Count == 1)
            {
                int shopId = Common.ToInt32(dtpreOrder19dian.Rows[0]["shopId"]);
                //string eCardNumber = dtpreOrder19dian.Rows[0]["eCardNumber"].ToString();
                //string verificationCode = dtpreOrder19dian.Rows[0]["verificationCode"].ToString();
                int isShopConfrim = Common.ToInt32(dtpreOrder19dian.Rows[0]["isShopConfirmed"]);//表示当前点单审核状态
                int isApproved = Common.ToInt32(dtpreOrder19dian.Rows[0]["isApproved"]);//表示当前点单对账状态
                double refundMoneySum = Common.ToDouble(dtpreOrder19dian.Rows[0]["refundMoneySum"]);//表示当前点单已退款金额
                double refundMoneySumOrder = Common.ToDouble(dtpreOrder19dian.Rows[0]["refundMoneySum1"]);//表示当前点单order表已退款金额
                int status = Common.ToInt32(dtpreOrder19dian.Rows[0]["status"]);//表示当前点单状态
                double prePaidSum = Common.ToDouble(dtpreOrder19dian.Rows[0]["prePaidSum"]);//当前点单支付的金额
                long customerId = Common.ToInt64(dtpreOrder19dian.Rows[0]["customerId"]);
                int currentPlatformVipGrade = Common.ToInt32(dtpreOrder19dian.Rows[0]["currentPlatformVipGrade"]);
                int shopConfirmenStatus = 0;
                string confirmenDes = "";

                //if (employeeId > 0 && flag == false)//掌中宝客户端操作
                //{
                //    confirmenDes += "悠先服务端，";
                //}
                //if (employeeId > 0 && flag == true)//老后台操作
                //{
                //    confirmenDes += "悠先收银端，";
                //}
                if (employeeId > 0)//Add at 2015-4-23
                {
                    switch (preOrderConfirmOperater)
                    {
                        case PreOrderConfirmOperater.Service:
                            confirmenDes += "悠先服务端，";
                            break;
                        case PreOrderConfirmOperater.Cash:
                            confirmenDes += "悠先收银端，";
                            break;
                        case PreOrderConfirmOperater.Client:
                            confirmenDes += "悠先点菜端，";
                            break;
                    }
                }
                if (statusFlag == 1)
                {
                    shopConfirmenStatus = (int)VAPreOrderShopConfirmed.SHOPCONFIRMED;
                    confirmenDes = "审核";
                }
                else//取消审核  
                {
                    shopConfirmenStatus = (int)VAPreOrderShopConfirmed.NOT_SHOPCONFIRMED;
                    confirmenDes = "取消审核";
                }


                using (TransactionScope scope = new TransactionScope())
                {
                    //EmployeePointOperate pointOperate = new EmployeePointOperate();
                    if (statusFlag != 1)//取消审核 待处理 条件是已审核
                    {
                        if (status != (int)VAPreorderStatus.Refund && status != (int)VAPreorderStatus.OriginalRefunding && refundMoneySumOrder == 0)
                        {
                            if (isShopConfrim == 1)//确定当前单子已审核
                            {
                                if (isApproved == 0)//未对账
                                {
                                    if (preOrder19dianOperate.ShopConfrimedPreOrder(preOrder19dianId, shopConfirmenStatus, employeeID, employeeName, employeePosition))
                                    {
                                        int preOrderTotalQuantity = Common.ToInt32(dtpreOrder19dian.Rows[0]["preOrderTotalQuantity"]);
                                        double preOrderTotalAmount = Common.ToDouble(dtpreOrder19dian.Rows[0]["preOrderTotalAmount"]) + (-1 * (prePaidSum - refundMoneySum));
                                        bool updateCustomerPartInfo = Common.ModifyUserPlatVip(customerId, preOrderTotalQuantity, currentPlatformVipGrade, -1 * (prePaidSum - refundMoneySum), preOrderTotalAmount, true);
                                        //string employeeOperateStr = "eVIP卡号：" + eCardNumber + "，点单流水号：" + preOrder19dianId + "，验证码：" + verificationCode + "，操作类型：" + confirmenDes;
                                        string employeeOperateStr = "用户ID：" + customerId + "，点单流水号：" + preOrder19dianId + "，操作类型：" + confirmenDes;
                                        CustomerServiceOperateLogInfo customerServiceOperateLogInfo = new CustomerServiceOperateLogInfo();
                                        CustomerServiceOperateLogOperate customerServiceOperateLogOperate = new CustomerServiceOperateLogOperate();
                                        customerServiceOperateLogInfo.employeeId = employeeID;
                                        customerServiceOperateLogInfo.employeeName = employeeName;
                                        customerServiceOperateLogInfo.employeeOperateTime = DateTime.Now;
                                        customerServiceOperateLogInfo.employeeOperate = employeeOperateStr;
                                        result = customerServiceOperateLogOperate.InsertCustomerServiceOperateLog(customerServiceOperateLogInfo);
                                        if (result > 0) //&& pointOperate.UpdateEmployeeNotSettlementPoint(dtpreOrder19dian, employeeID)
                                        {
                                            result = 1;
                                            scope.Complete();
                                        }
                                    }
                                }
                                else//已对账
                                {
                                    return -1;//前端提示：当前点单已对账，无法取消审核
                                }
                            }
                            else
                            {
                                return -2;//前端提示：当前单子是未审核状态，无法取消审核
                            }
                        }
                        else
                        {
                            return -7;//前端提示：当前单子已部分退款或者全部退款，无法取消审核  
                        }
                    }
                    else//审核   待处理 需要审核前
                    {
                        // if (status == (int)VAPreorderStatus.Refund || status == (int)VAPreorderStatus.OriginalRefunding)//流水状态，用给客户端显示点单状态
                        if (refundMoneySumOrder > 0)//退款金额大于0，表示当前点单有退款操作
                        {
                            return -8;
                        }
                        if (isShopConfrim == 0)//确定当前点单是未审核状态
                        {
                            if (preOrder19dianOperate.ShopConfrimedPreOrder(preOrder19dianId, shopConfirmenStatus, employeeID, employeeName, employeePosition))
                            {
                                int preOrderTotalQuantity = Common.ToInt32(dtpreOrder19dian.Rows[0]["preOrderTotalQuantity"]);
                                double preOrderTotalAmount = Common.ToDouble(dtpreOrder19dian.Rows[0]["preOrderTotalAmount"]) + prePaidSum - refundMoneySum;
                                bool updateCustomerPartInfo = Common.ModifyUserPlatVip(customerId, preOrderTotalQuantity, currentPlatformVipGrade, prePaidSum - refundMoneySum, preOrderTotalAmount, false);
                                //string employeeOperateStr = "eVIP卡号：" + eCardNumber + "，点单流水号：" + preOrder19dianId + "，验证码：" + verificationCode + "，操作类型：" + confirmenDes;
                                string employeeOperateStr = "用户ID：" + customerId + "，点单流水号：" + preOrder19dianId + "，操作类型：" + confirmenDes;
                                CustomerServiceOperateLogInfo customerServiceOperateLogInfo = new CustomerServiceOperateLogInfo();
                                CustomerServiceOperateLogOperate customerServiceOperateLogOperate = new CustomerServiceOperateLogOperate();
                                customerServiceOperateLogInfo.employeeId = employeeID;
                                customerServiceOperateLogInfo.employeeName = employeeName;
                                customerServiceOperateLogInfo.employeeOperateTime = DateTime.Now;
                                customerServiceOperateLogInfo.employeeOperate = employeeOperateStr;
                                result = customerServiceOperateLogOperate.InsertCustomerServiceOperateLog(customerServiceOperateLogInfo);
                                if (result > 0)// && pointOperate.UpdateEmployeeNotSettlementPoint(dtpreOrder19dian, employeeID)
                                {
                                    result = 1;
                                    scope.Complete();
                                    flagSendNotification = true;
                                }
                            }
                        }
                        else
                        {
                            return -3;//前端提示：当前单子是已审核状态，无法审核
                        }
                    }
                    uniPushInfo = new UniPushInfo()
                   {
                       customerPhone = dtpreOrder19dian.Rows[0]["mobilePhoneNumber"].ToString(),
                       shopName = "",
                       preOrder19dianId = preOrder19dianId,
                       pushMessage = VAPushMessage.入座成功,
                       clientBuild = dtpreOrder19dian.Rows[0]["appBuild"].ToString(),
                       orderId = Guid.Parse(dtpreOrder19dian.Rows[0]["OrderId"].ToString())
                   };
                }

                if (statusFlag == 1 && result > 0)
                {
                    //-----------------------------调用积分系统-------------------------------

                    Dictionary<string, object> parameters = new Dictionary<string, object>();
                    parameters.Add("userId", customerId);
                    parameters.Add("preOrder19dianId", preOrder19dianId);

                    IntegralWebSend sendPara = new IntegralWebSend()
                    {
                        type = (int)VAMessageType.CLIENT_PREORDER_CONFIRM_REQUEST,
                        parameters = parameters
                    };

                    Thread integralThread = new Thread(IntegralWebSend);
                    integralThread.Start(sendPara);

                    //--------------------------------------------------------------------- 
                }
            }
            else
            {
                result = 0;
            }

            if (flagSendNotification)
            {
                Thread notificationThread = new Thread(SendEvaluationNotification);
                notificationThread.Start(uniPushInfo);
            }
            return result;//
        }

        public void IntegralWebSend(object sendPara)
        {
            IntegralWebSend integralWebSend = (IntegralWebSend)sendPara;
            new UserIntegralSend().Send(integralWebSend.type, integralWebSend.parameters);
        }

        /// <summary>
        /// 发送评分提示推送
        /// </summary>
        /// <param name="preorderId"></param>
        public static void SendEvaluationNotification(object objUniPushInfo)
        {
            UniPushInfo uniPushInfo = (UniPushInfo)objUniPushInfo;
            PreOrder19dianManager preorder19dianMan = new PreOrder19dianManager();
            //DataTable dtPreorder = preorder19dianMan.SelectPreOrderAndCompany(uniPushInfo.preOrder19dianId);
            //DataView dvPreorder = dtPreorder.DefaultView;
            //dvPreorder.RowFilter = "evaluationValue = -1";
            //if (dvPreorder.Count == 1)
            //{
            uniPushInfo.shopName = preorder19dianMan.GetShopNameByOrderId(uniPushInfo.preOrder19dianId);
            CustomPushRecordOperate customPushRecordOperate = new CustomPushRecordOperate();
            customPushRecordOperate.UniPush(uniPushInfo);
            #region 注释老方法
            //long customerId = Common.ToInt64(dvPreorder[0]["customerId"]);
            //CustomerManager customerMan = new CustomerManager();
            //DataTable dtCustomer = customerMan.SelectCustomerTopDevice(customerId);
            //if (dtCustomer.Rows.Count == 1)
            //{
            //    using (TransactionScope scope = new TransactionScope())
            //    {//插入推送记录
            //        NotificationRecord notificationRecord = new NotificationRecord();
            //        notificationRecord.addTime = DateTime.Now;
            //        notificationRecord.appType = Common.ToInt32(dtCustomer.Rows[0]["appType"]);
            //        notificationRecord.isLocked = false;
            //        notificationRecord.isSent = false;
            //        notificationRecord.pushToken = Common.ToString(dtCustomer.Rows[0]["pushToken"]);
            //        notificationRecord.sendCount = 0;
            //        string evaluationNotificationMsg = ConfigurationManager.AppSettings["evaluationNotificationMsg"];

            //        notificationRecord.message = evaluationNotificationMsg;
            //        notificationRecord.customType = (int)VANotificationsCustomType.NOTIFICATIONS_EVALUATION;
            //        notificationRecord.customValue = preorderId.ToString();
            //        NotificationRecordManager notificationRecordMan = new NotificationRecordManager();
            //        if (!string.IsNullOrEmpty(evaluationNotificationMsg) && !string.IsNullOrEmpty(notificationRecord.pushToken))
            //        {
            //            long flagInsertNotificationRecord = notificationRecordMan.InsertNotificationRecord(notificationRecord);
            //            if (flagInsertNotificationRecord > 0)
            //            {
            //                scope.Complete();
            //            }
            //        }
            //    }
            //}
            #endregion
            //}
        }

        public static void SendMenuUpdateNotification()
        {

        }


        /// <summary>
        /// 获取某门店未结款金额
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public double GetShopInfoRemainMoney(int shopId)
        {
            SybMoneyMerchantManager sybMoneyMerchantManager = new SybMoneyMerchantManager();
            return sybMoneyMerchantManager.GetShopInfoRemainMoney(shopId);
        }

        /// <summary>
        /// 用按orderId统计账户明细
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public DataTable getAccountDetailByOrderID(string preOrder19dianIds, int shopId)
        {
            SybMoneyMerchantManager sybMoneyMerchantManager = new SybMoneyMerchantManager();
            return sybMoneyMerchantManager.getAccountDetailByOrderID(preOrder19dianIds, shopId);
        }

        /// <summary>
        /// 按orderId查出要显示的主单ID，只退补差价单子时用
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public DataTable getAccountDetailByAccountType(string preOrder19dianIds)
        {
            SybMoneyMerchantManager sybMoneyMerchantManager = new SybMoneyMerchantManager();
            return sybMoneyMerchantManager.getAccountDetailByAccountType(preOrder19dianIds);
        }

        #region 作废对账代码，先循环点单收入，再佣金，再退款
        //public string ApproveMoneyMerchantNew(Guid orderId, int employeeId)
        //{
        //    PreOrder19dianOperate preOrder19DianOperate = new PreOrder19dianOperate();
        //    SybMoneyMerchantManager moneyMerchantManager = new SybMoneyMerchantManager();
        //    EmployeeOperate employeeOperate = new EmployeeOperate();
        //    CustomerServiceOperateLogOperate customerServiceOperateLogOperate = new CustomerServiceOperateLogOperate();
        //    CompanyOperate companyOperate = new CompanyOperate();
        //    PreOrder19dianManager preOrder19DianMan = new PreOrder19dianManager();
        //    EmployeePointLogOperate pointLogOper = new EmployeePointLogOperate();
        //    ShopVIPOperate shopVipOper = new ShopVIPOperate();
        //    const int approveTag = (int)VAPreorderIsApproved.APPROVED;//操作类型，对账

        //    DataTable dtOrder = preOrder19DianOperate.QueryPreOrderByOrderId(orderId);
        //    if (dtOrder.Rows.Count != 1)
        //    {
        //        return "-1"; //判断预订单存在
        //    }
        //    int isShopConfirmed = Common.ToInt32(dtOrder.Rows[0]["isShopConfirmed"]);//入座
        //    int isApproved = Common.ToInt32(dtOrder.Rows[0]["isApproved"]);//对账
        //    if (isApproved == 1 || isShopConfirmed != 1)//已对账，或者未入座
        //    {
        //        return "-2";
        //    }
        //    int shopId = Common.ToInt32(dtOrder.Rows[0]["shopID"].ToString());
        //    int companyId = Common.ToInt32(dtOrder.Rows[0]["companyID"].ToString());
        //    string flowNum = Common.ToString(SybMoneyOperate.CreateMerchantFlowNumber(shopId));
        //    double remainMoney = moneyMerchantManager.GetShopInfoRemainMoney(shopId);

        //    //double paySum = Common.ToDouble(dtOrder.Rows[0]["prePaidSum"]);
        //    //double refundMoneySum = Common.ToDouble(dtOrder.Rows[0]["refundMoneySum"]);

        //    OrderOperate orderOperate = new OrderOperate();
        //    OrderPaidDetail orderPaidDetail = orderOperate.GetOrderPaidDeatial(orderId);

        //    double paySum = Common.ToDouble(orderPaidDetail.PrePaidSum);
        //    double refundMoneySum = Common.ToDouble(orderPaidDetail.refundMoneySum);

        //    long customerId = Common.ToInt32(dtOrder.Rows[0]["customerId"]);

        //    EmployeeInfo employeeInfo = employeeOperate.QueryEmployee(employeeId);
        //    if (employeeInfo == null || employeeInfo.EmployeeID <= 0)
        //    {
        //        return "0";
        //    }
        //    string employeeName = employeeInfo.EmployeeFirstName;
        //    string employeePosition = employeeInfo.position;


        //    #region 计算佣金

        //    ShopOperate shopOperate = new ShopOperate();

        //    double ratio = shopOperate.getViewallocCommissionValue(shopId);//门店中的佣金比例
        //    double forViewalloc = Math.Round((paySum - refundMoneySum) * ratio, 2);

        //    #endregion

        //    ShopMoney shopMoney = new ShopMoney();

        //    #region 记录门店VIP信息

        //    ShopVIP shopVip = new ShopVIP()
        //    {
        //        CityId = 0,
        //        ShopId = shopId,
        //        CustomerId = customerId,
        //        PreOrderTotalQuantity = 1,
        //        PreOrderTotalAmount = Common.ToDecimal(paySum - refundMoneySum),
        //        CreateTime = DateTime.Now,
        //        LastPreOrderTime = DateTime.Now,
        //        Id = 0
        //    };
        //    #endregion

        //    #region 员工操作日志

        //    CustomerServiceOperateLogInfo customerServiceOperateLogInfo = new CustomerServiceOperateLogInfo()
        //    {
        //        employeeId = employeeId,
        //        employeeName = employeeName,
        //        employeeOperateTime = DateTime.Now,
        //        employeeOperate = string.Format("用户ID：{0}，点单流水号：{1}，操作类型：对账", customerId, orderId)
        //    };

        //    #endregion

        //    string val = "0";
        //    using (TransactionScope scope = new TransactionScope())
        //    {
        //        try
        //        {
        //            //对账记录
        //            bool flag1 = preOrder19DianMan.InsertPreOrderCheckInfo(Common.ToInt64(dtOrder.Rows[0]["preOrder19DianId"]), approveTag, employeeId, employeeName, employeePosition);
        //            //修改点单状态为已对账
        //            bool flag2 = preOrder19DianMan.ApprovePreOrder19dianByOrderId(orderId, approveTag);
        //            //增加客服日志信息
        //            bool flag3 = customerServiceOperateLogOperate.InsertCustomerServiceOperateLog(customerServiceOperateLogInfo) > 0;

        //            bool flag4 = true;
        //            #region
        //            MoneyMerchantAccountDetail detail = new MoneyMerchantAccountDetail();
        //            detail.shopId = shopId;
        //            detail.companyId = companyId;
        //            detail.flowNumber = flowNum;
        //            //detail.accountTypeConnId = dtOrder.Rows[0]["preOrder19DianId"].ToString();
        //            detail.operUser = Common.ToString(employeeId);

        //            DataTable dtPreOrder = preOrder19DianMan.SelectPreorder19dianByOrderId(orderId);

        //            bool flag5 = true;
        //            double realremainMoney = remainMoney;
        //            for (int i = 0; i < dtPreOrder.Rows.Count; i++)
        //            {
        //                double realPrePaidSum = Common.ToDouble(dtPreOrder.Rows[i]["PrePaidSum"]);
        //                detail.accountTypeConnId = dtPreOrder.Rows[i]["preOrder19DianId"].ToString();
        //                detail.accountMoney = realPrePaidSum;
        //                realremainMoney = realremainMoney + realPrePaidSum;
        //                detail.remainMoney = realremainMoney;
        //                detail.accountType = Common.ToInt32(AccountType.ORDER_INCOME);
        //                detail.inoutcomeType = Common.ToInt32(InoutcomeType.IN);
        //                if (!moneyMerchantManager.InsertMoneyMerchantAccountDetail(detail))
        //                {
        //                    flag5 = false;
        //                }
        //            }

        //            for (int i = 0; i < dtPreOrder.Rows.Count; i++)
        //            {
        //                double realpaySum = Common.ToDouble(dtPreOrder.Rows[i]["PrePaidSum"]);
        //                double realrefundMoneySum = Common.ToDouble(dtPreOrder.Rows[i]["refundMoneySum"]);
        //                double realforViewalloc = Math.Round((realpaySum - realrefundMoneySum) * ratio, 2);
        //                if (forViewalloc > 0)//佣金不为零的时候插入付佣金给友络的流水
        //                {
        //                    detail.accountMoney = Common.ToDouble(0 - realforViewalloc);
        //                    realremainMoney = realremainMoney - realforViewalloc;
        //                    detail.remainMoney = realremainMoney;
        //                    detail.accountType = Common.ToInt32(AccountType.VIEWALLOC_COMMISSION);
        //                    detail.inoutcomeType = Common.ToInt32(InoutcomeType.OUT);
        //                    if (!moneyMerchantManager.InsertMoneyMerchantAccountDetail(detail))
        //                    {
        //                        flag4 = false;
        //                    }
        //                }
        //            }

        //            for (int i = 0; i < dtPreOrder.Rows.Count; i++)
        //            {
        //                double realrefundMoneySum = Common.ToDouble(dtPreOrder.Rows[i]["refundMoneySum"]);
        //                if (refundMoneySum > 0)//退款不为零的时候插入退款的商户流水
        //                {
        //                    detail.accountMoney = Common.ToDouble(0 - realrefundMoneySum);
        //                    realremainMoney = realremainMoney - realrefundMoneySum;
        //                    detail.remainMoney = realremainMoney;
        //                    detail.accountType = Common.ToInt32(AccountType.ORDER_OUTCOME);
        //                    detail.inoutcomeType = Common.ToInt32(InoutcomeType.OUT);
        //                    if (!moneyMerchantManager.InsertMoneyMerchantAccountDetail(detail))
        //                    {
        //                        flag4 = false;
        //                    }
        //                }
        //            }
                   
        //            detail.accountMoney = Common.ToDouble(paySum);
        //            detail.remainMoney = Common.ToDouble(remainMoney - forViewalloc - refundMoneySum + paySum);
        //            detail.accountType = Common.ToInt32(AccountType.ORDER_INCOME);
        //            detail.inoutcomeType = Common.ToInt32(InoutcomeType.IN);
        //            //bool flag5 = moneyMerchantManager.InsertMoneyMerchantAccountDetail(detail);

        //            #endregion

        //            bool flag9 = true;//合并原来的flag6，flag7，flag9

        //            shopMoney.remainMoney = Common.ToDouble(paySum - refundMoneySum - forViewalloc);
        //            shopMoney.paySum = Common.ToDouble(paySum - refundMoneySum - forViewalloc);
        //            shopMoney.remainCommissionAmount = forViewalloc;
        //            shopMoney.shopId = shopId;

        //            if (paySum > refundMoneySum)//退款金额小于支付金额，更新用户支付点单数量
        //            {
        //                flag9 = SybMoneyMerchantManager.UpdateShopInfoAboutMoney(shopMoney, true);
        //            }
        //            else
        //            {
        //                flag9 = SybMoneyMerchantManager.UpdateShopInfoAboutMoney(shopMoney);
        //            }
        //            bool flag10 = shopVipOper.OperateShopVIP(shopVip);

        //            if (flag1 && flag2 && flag3 && flag4 && flag5 && flag9 && flag10)
        //            {
        //                scope.Complete();
        //                val = "1";
        //            }
        //            else
        //            {
        //                val = "0";
        //                LogDll.LogManager.WriteLog(LogDll.LogFile.Trace, DateTime.Now.ToString() + "--flag1：" + flag1 + "--flag2：" + flag2 + "--flag3：" + flag3 + "--flag4：" + flag4 + "--flag5：" + flag5 + "--flag9：" + flag9 + "--flag10：" + flag10);
        //            }
        //        }
        //        catch (Exception e)
        //        {
        //            //val = "0";
        //            val = e.Message;
        //        }
        //        return val;
        //    }
        //}
    #endregion

        public string ApproveMoneyMerchantNew(long preOrder19DianId, int employeeId)
        {
            PreOrder19dianOperate preOrder19DianOperate = new PreOrder19dianOperate();
            SybMoneyMerchantManager moneyMerchantManager = new SybMoneyMerchantManager();
            EmployeeOperate employeeOperate = new EmployeeOperate();
            CustomerServiceOperateLogOperate customerServiceOperateLogOperate = new CustomerServiceOperateLogOperate();
            CompanyOperate companyOperate = new CompanyOperate();
            PreOrder19dianManager preOrder19DianMan = new PreOrder19dianManager();
            EmployeePointLogOperate pointLogOper = new EmployeePointLogOperate();
            ShopVIPOperate shopVipOper = new ShopVIPOperate();
            const int approveTag = (int)VAPreorderIsApproved.APPROVED;//操作类型，对账

            DataTable dtOrder = preOrder19DianOperate.QueryPreOrderGroupByOrderID(preOrder19DianId);
            if (dtOrder.Rows.Count != 1)
            {
                return "-1"; //判断预订单存在
            }
            int isShopConfirmed = Common.ToInt32(dtOrder.Rows[0]["isShopConfirmed"]);//入座
            int isApproved = Common.ToInt32(dtOrder.Rows[0]["isApproved"]);//对账
            if (isApproved == 1 || isShopConfirmed != 1)//已对账，或者未入座
            {
                return "-2";
            }
            int shopId = Common.ToInt32(dtOrder.Rows[0]["shopID"].ToString());
            int companyId = Common.ToInt32(dtOrder.Rows[0]["companyID"].ToString());
            string flowNum = Common.ToString(SybMoneyOperate.CreateMerchantFlowNumber(shopId));
            double remainMoney = moneyMerchantManager.GetShopInfoRemainMoney(shopId);
            double paySum = Common.ToDouble(dtOrder.Rows[0]["prePaidSum"]);
            double refundMoneySum = Common.ToDouble(dtOrder.Rows[0]["refundMoneySum"]);
            long customerId = Common.ToInt32(dtOrder.Rows[0]["customerId"]);

            EmployeeInfo employeeInfo = employeeOperate.QueryEmployee(employeeId);
            if (employeeInfo == null || employeeInfo.EmployeeID <= 0)
            {
                return "0";
            }
            string employeeName = employeeInfo.EmployeeFirstName;
            string employeePosition = employeeInfo.position;


            #region 计算佣金

            ShopOperate shopOperate = new ShopOperate();

            double ratio = shopOperate.getViewallocCommissionValue(shopId);//门店中的佣金比例
            double forViewalloc = Math.Round((paySum - refundMoneySum) * ratio, 2);

            //double forViewalloc = 0;
            //CompanyInfo comInfo = companyOperate.QueryCompany(companyId);
            //if (comInfo == null || comInfo.companyID <= 0)
            //{
            //    return "0";
            //}
            //if (Common.ToInt32(comInfo.viewallocCommissionType) == (int)VACommissionType.Proportion)//佣金按比例收取
            //{
            //    forViewalloc = Math.Round((paySum - refundMoneySum) * comInfo.viewallocCommissionValue, 2);
            //}
            //else//固定金额
            //{
            //    if (comInfo.viewallocCommissionValue < paySum - refundMoneySum) //如果账单金额小于等于友络佣金数额，佣金数额变为账单金额
            //    {
            //        forViewalloc = Common.ToDouble(comInfo.viewallocCommissionValue);
            //    }
            //    else
            //    {
            //        forViewalloc = Common.ToDouble(paySum - refundMoneySum);
            //    }
            //}
            #endregion

            ShopMoney shopMoney = new ShopMoney();

            #region 记录门店VIP信息

            ShopVIP shopVip = new ShopVIP()
            {
                CityId = 0,
                ShopId = shopId,
                CustomerId = customerId,
                PreOrderTotalQuantity = 1,
                PreOrderTotalAmount = Common.ToDecimal(paySum - refundMoneySum),
                CreateTime = DateTime.Now,
                LastPreOrderTime = DateTime.Now,
                Id = 0
            };
            #endregion

            #region 员工操作日志

            CustomerServiceOperateLogInfo customerServiceOperateLogInfo = new CustomerServiceOperateLogInfo()
            {
                employeeId = employeeId,
                employeeName = employeeName,
                employeeOperateTime = DateTime.Now,
                employeeOperate = string.Format("用户ID：{0}，点单流水号：{1}，操作类型：对账", customerId, preOrder19DianId)
            };

            #endregion

            string val = "0";
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    //对账记录
                    bool flag1 = preOrder19DianMan.InsertPreOrderCheckInfo(preOrder19DianId, approveTag, employeeId, employeeName, employeePosition);
                    //修改点单状态为已对账
                    bool flag2 = preOrder19DianMan.ApprovePreOrder19dian(preOrder19DianId, approveTag);
                    //增加客服日志信息
                    bool flag3 = customerServiceOperateLogOperate.InsertCustomerServiceOperateLog(customerServiceOperateLogInfo) > 0;

                    bool flag4 = true;
                    #region
                    MoneyMerchantAccountDetail detail = new MoneyMerchantAccountDetail();
                    detail.shopId = shopId;
                    detail.companyId = companyId;
                    detail.flowNumber = flowNum;
                    detail.accountTypeConnId = preOrder19DianId.ToString();
                    detail.operUser = Common.ToString(employeeId);

                    detail.accountMoney = Common.ToDouble(paySum);
                    detail.remainMoney = Common.ToDouble(remainMoney + paySum);
                    detail.accountType = Common.ToInt32(AccountType.ORDER_INCOME);
                    detail.inoutcomeType = Common.ToInt32(InoutcomeType.IN);
                    bool flag5 = moneyMerchantManager.InsertMoneyMerchantAccountDetail(detail);

                    if (forViewalloc > 0)//佣金不为零的时候插入付佣金给友络的流水
                    {
                        detail.accountMoney = Common.ToDouble(0 - forViewalloc);
                        detail.remainMoney = Common.ToDouble(remainMoney + paySum - forViewalloc);
                        detail.accountType = Common.ToInt32(AccountType.VIEWALLOC_COMMISSION);
                        detail.inoutcomeType = Common.ToInt32(InoutcomeType.OUT);
                        flag4 = moneyMerchantManager.InsertMoneyMerchantAccountDetail(detail);
                    }
                    if (refundMoneySum > 0)//退款不为零的时候插入退款的商户流水
                    {
                        detail.accountMoney = Common.ToDouble(0 - refundMoneySum);
                        detail.remainMoney = Common.ToDouble(remainMoney + paySum - forViewalloc - refundMoneySum);
                        detail.accountType = Common.ToInt32(AccountType.ORDER_OUTCOME);
                        detail.inoutcomeType = Common.ToInt32(InoutcomeType.OUT);
                        flag4 = moneyMerchantManager.InsertMoneyMerchantAccountDetail(detail);
                    }
                    #endregion

                    bool flag9 = true;//合并原来的flag6，flag7，flag9

                    shopMoney.remainMoney = Common.ToDouble(paySum - refundMoneySum - forViewalloc);
                    shopMoney.paySum = Common.ToDouble(paySum - refundMoneySum - forViewalloc);
                    shopMoney.remainCommissionAmount = forViewalloc;
                    shopMoney.shopId = shopId;

                    if (paySum > refundMoneySum)//退款金额小于支付金额，更新用户支付点单数量
                    {
                        flag9 = SybMoneyMerchantManager.UpdateShopInfoAboutMoney(shopMoney, true);
                    }
                    else
                    {
                        flag9 = SybMoneyMerchantManager.UpdateShopInfoAboutMoney(shopMoney);
                    }
                    bool flag10 = shopVipOper.OperateShopVIP(shopVip);

                    if (flag1 && flag2 && flag3 && flag4 && flag5 && flag9 && flag10)
                    {
                        scope.Complete();
                        val = "1";
                    }
                    else
                    {
                        val = "0";
                        LogDll.LogManager.WriteLog(LogDll.LogFile.Trace, DateTime.Now.ToString() + "--flag1：" + flag1 + "--flag2：" + flag2 + "--flag3：" + flag3 + "--flag4：" + flag4 + "--flag5：" + flag5 + "--flag9：" + flag9 + "--flag10：" + flag10);
                    }
                }
                catch (Exception e)
                {
                    //val = "0";
                    val = e.Message;
                }
                return val;
            }
        }
    }
}
