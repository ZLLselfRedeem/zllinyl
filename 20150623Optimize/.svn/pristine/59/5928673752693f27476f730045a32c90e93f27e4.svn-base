using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.Model;
using System.Data;
using System.Web;
using VAGastronomistMobileApp.SQLServerDAL;

namespace VAGastronomistMobileApp.WebPageDll
{
    /// <summary>
    /// 收银宝点单
    /// 20140304 created by wangcheng
    /// </summary>
    public class SybPreOrder
    {
        /// <summary>
        /// 收银宝点单详情查看详情
        /// </summary>
        /// <param name="orderId">preOrder19dianId</param>
        /// PS 加入补差价功能后， 新版本需要把preOrder19dianId当作orderID用 add by zhujinlei 2015/07/01
        /// <param name="status"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        public static string PreOrderDetail(Guid orderId, int status = 0, bool flag = false)
        {
            if (flag == false)//微信点菜不需要判断是session的存在
            {
                if (HttpContext.Current.Session["MerchantsTreasureUserInfo"] == null)
                {
                    return "-1000";
                }
            }
            string sign = "";
            PreOrder19dianOperate preOrder19dianOperate = new PreOrder19dianOperate();
            //DataTable dtpreOrder19dian = preOrder19dianOperate.QueryPreOrderById(preOrder19dianId);
            //DataTable dtpreOrder19dian = preOrder19dianOperate.SybSelectPreOrder(preOrder19dianId);

            // add by zhujinlei 2015/06/30 补差价获取订单总表的数据
            DataTable dtpreOrder19dian = preOrder19dianOperate.SybSelectOrder(orderId);
            long preOrder19dianId = 0;
            if (dtpreOrder19dian.Rows.Count == 1)
            {
                preOrder19dianId = (long)dtpreOrder19dian.Rows[0]["preorder19dianid"];

                double discount = Common.ToDouble(dtpreOrder19dian.Rows[0]["discount"]);//当前点单支付时折扣
                string orderInJson = dtpreOrder19dian.Rows[0]["orderInJson"].ToString();//单独提出来
                string sundryJson = Common.ToString(dtpreOrder19dian.Rows[0]["sundryJson"]);
                List<PreOrderIn19dian> listOrderInfo = JsonOperate.JsonDeserialize<List<PreOrderIn19dian>>(orderInJson);
                List<VASundryInfo> sundryList = JsonOperate.JsonDeserialize<List<VASundryInfo>>(Common.ToString(sundryJson));
                if (string.IsNullOrEmpty(orderInJson) && string.IsNullOrEmpty(sundryJson))//直接支付
                {
                    orderInJson = "[]";
                }
                else
                {
                    //重新操作orderjson，重新计算价格
                    double originalPrice = preOrder19dianOperate.CalcPreorederOriginalPrice(orderInJson);//计算菜品的原价
                    for (int j = 0; j < listOrderInfo.Count; j++)
                    {
                        double currectDishIngredientsPrice = 0;
                        if (listOrderInfo[j].dishIngredients != null && listOrderInfo[j].dishIngredients.Count > 0)
                        {
                            for (int i = 0; i < listOrderInfo[j].dishIngredients.Count; i++)
                            {
                                double price = 0;
                                if (listOrderInfo[j].dishIngredients[i].vipDiscountable == "True")
                                {
                                    price = listOrderInfo[j].dishIngredients[i].ingredientsPrice * discount * listOrderInfo[j].dishIngredients[i].quantity;
                                }
                                else
                                {
                                    price = listOrderInfo[j].dishIngredients[i].ingredientsPrice * listOrderInfo[j].dishIngredients[i].quantity;
                                }
                                currectDishIngredientsPrice = currectDishIngredientsPrice + price;
                            }
                        }
                        currectDishIngredientsPrice = currectDishIngredientsPrice * listOrderInfo[j].quantity;
                        if (listOrderInfo[j].vipDiscountable == true)
                        {
                            listOrderInfo[j].unitPrice = Common.ToDouble(listOrderInfo[j].unitPrice * listOrderInfo[j].quantity * discount + currectDishIngredientsPrice);
                        }
                        else
                        {
                            listOrderInfo[j].unitPrice = Common.ToDouble(listOrderInfo[j].unitPrice * listOrderInfo[j].quantity + currectDishIngredientsPrice);
                        }
                    }
                    if (sundryList != null && sundryList.Count > 0)
                    {
                        double sundryCount = 0;
                        for (int i = 0; i < sundryList.Count; i++)
                        {
                            switch (Common.ToInt32(sundryList[i].sundryChargeMode))
                            {
                                case 1://固定金额
                                    {
                                        if (Common.ToBool(sundryList[i].vipDiscountable))
                                        {
                                            sundryCount += Common.ToDouble(sundryList[i].price) * discount;
                                        }
                                        else
                                        {
                                            sundryCount += Common.ToDouble(sundryList[i].price);
                                        }
                                        break;
                                    }
                                case 2://按比例
                                    {
                                        break;
                                    }
                                case 3://按人次
                                    {
                                        if (Common.ToBool(sundryList[i].vipDiscountable))
                                        {
                                            sundryCount += Common.ToDouble(sundryList[i].price) * sundryList[i].quantity * discount;
                                        }
                                        else
                                        {
                                            sundryCount += Common.ToDouble(sundryList[i].price) * sundryList[i].quantity;
                                        }
                                        break;
                                    }
                            }
                        }
                        for (int i = 0; i < sundryList.Count; i++)
                        {
                            PreOrderIn19dian sundryInfo = new PreOrderIn19dian();//将杂项信息转化添加到点单信息json中
                            sundryInfo.dishName = sundryList[i].sundryName;
                            sundryInfo.dishPriceName = sundryList[i].sundryStandard;
                            switch (sundryList[i].sundryChargeMode)
                            {
                                case 1:
                                    {//固定金额
                                        sundryInfo.quantity = 1;
                                        sundryInfo.unitPrice = Common.ToDouble(sundryList[i].price);
                                    }
                                    break;
                                case 2:
                                    {//按比例
                                        sundryInfo.quantity = 1;
                                        sundryInfo.unitPrice = Common.ToDouble(Math.Round(sundryList[i].price * (sundryCount + originalPrice), 2));
                                    }
                                    break;
                                case 3:
                                    {//按人次
                                        sundryInfo.dishName = sundryList[i].sundryName;
                                        sundryInfo.quantity = sundryList[i].quantity;
                                        sundryInfo.unitPrice = Common.ToDouble(sundryList[i].price * sundryList[i].quantity);
                                    }
                                    break;
                            }
                            listOrderInfo.Add(sundryInfo);
                        }
                    }
                    orderInJson = JsonOperate.JsonSerializer<List<PreOrderIn19dian>>(listOrderInfo);
                }
                dtpreOrder19dian.Rows[0]["orderInJson"] = "";//制空，否则js报错                
                dtpreOrder19dian.Rows[0]["sundryJson"] = "";
                dtpreOrder19dian.Columns.Add("prePayCashBackDescription", typeof(string));
                int isStatus = 0;
                if (status == 1)
                {
                    isStatus = GetPreOrderConfrimStatus(preOrder19dianId);
                }
                else
                {
                    isStatus = GetPreOrderApproveStatus(preOrder19dianId);
                }
                DataTable dtPreOrderVerifiedInfo = preOrder19dianOperate.QueryPreOrderCheckInfo(preOrder19dianId);
                DataTable dtPreOrderConfirmedInfo = preOrder19dianOperate.QueryPreorderShopConfirmedInfo(preOrder19dianId);

                double verifiedSaving = Common.ToDouble(dtpreOrder19dian.Rows[0]["verifiedSaving"]);
                double fillPostAmount = Common.ToDouble(dtpreOrder19dian.Rows[0]["PayDifferenceSum"]);
                PreOrder19dianPayment payment = GetPreOrderPayment(preOrder19dianId, Common.ToDouble(dtpreOrder19dian.Rows[0]["preOrderSum"]), Common.ToDouble(dtpreOrder19dian.Rows[0]["prePaidSum"]), Common.ToDouble(dtpreOrder19dian.Rows[0]["discount"]), verifiedSaving, orderId, fillPostAmount);


                sign = "{" + "\"commonInfo\":[" + Common.ConvertDateTableToJson(dtpreOrder19dian) //commonInfo 基本信息，支付信息，结算信息等其他信息展示
                     + "],\"customerPartInfo\":[" + Common.ConvertDateTableToJson(CustomerOperate.QueryPartCustomerInfo(preOrder19dianId)) //用户部分基本信息
                     + "],\"orderInfo\":" + orderInJson //orderInfo 菜品详细信息
                     + ",\"couponInfo\":[" + SybMsg.SetStr(Common.ConvertDateTableToJson(null))//couponInfo 优惠券信息展示，可能为空
                     + "],\"preOrderVerifiedInfo\":[" + SybMsg.SetStr(Common.ConvertDateTableToJson(dtPreOrderVerifiedInfo))  //preOrderVerifiedInfo 财务对账日志信息输出
                     + "],\"isStatus\":" + isStatus //isStatus 对账还是未对账信息
                     + ",\"preOrderConfirmedInfo\":[" + SybMsg.SetStr(Common.ConvertDateTableToJson(dtPreOrderConfirmedInfo))//preOrderConfirmedInfo 收银审核日志信息输出
                     + "],\"refundInfo\":" + SybMsg.SetStr(SybMoneyCustomerOperate.GetRefundInfo(preOrder19dianId, orderId))
                     + ",\"payment\":" + SysJson.JsonSerializer(payment) + "}";
            }
            return sign;
        }

        private static PreOrder19dianPayment GetPreOrderPayment(long preOrder19dianId, double preOrderSum, double prePaidSum, double discount, double verifiedSaving, Guid orderID, double fillPostAmount)
        {
            CouponOperate couponOperate = new WebPageDll.CouponOperate();
            RedEnvelopeConnPreOrderOperate redEnvelopeConnPreOrderOperate = new RedEnvelopeConnPreOrderOperate();
            PreOrder19dianManager preOrder19dianManager = new PreOrder19dianManager();
            PreOrder19dianPayment payment = new PreOrder19dianPayment();

            payment.preOrder19dianId = preOrder19dianId;
            payment.preOrderSum = Common.ToDecimal(preOrderSum);//原价
            payment.prePaidSum = Common.ToDecimal(prePaidSum);//折扣后价格
            payment.discount = Common.ToDecimal(discount);//折扣
            payment.discountAmount = Common.ToDecimal(verifiedSaving);//折扣优惠价格
            List<double> coupon = couponOperate.GetAmountOfOrder(preOrder19dianId);//抵价券抵扣金额
            if (coupon != null && coupon.Count > 0)
            {
                payment.couponName = "每满" + coupon[0] + "元减" + coupon[1] + "元，最多减" + coupon[3];
                payment.couponAmount = Common.ToDecimal(coupon[2]);
            }
            else
            {
                payment.couponName = "";
            }

            // 补差价 add by zhujinlei 2015/7/01 补差价=总原价-折扣-抵扣券
            //payment.fillpostAmount =  zzbPreOrderMan.ZZB_SelectFillPostAmount(zzb_vaPreOrderListDetailRequest.orderId);
            payment.fillpostAmount = Common.ToDecimal(fillPostAmount);

            // 红包抵扣金额
            double redEnvelopeAmountTotal = 0;
            // 粮票抵扣金额
            double balanceAmountTotal = 0;
            // 第三方支付总额
            double thirdAmountTotal = 0;

            // 根据orderID获取点单列表，获取总的红包和粮票第三方使用量
            List<PreOrder19dianInfo> listPreOrder19dianInfo = new List<PreOrder19dianInfo>();
            PreOrder19dianManager preorder19dianMan = new PreOrder19dianManager();
            listPreOrder19dianInfo = preorder19dianMan.GetPreOrder19dianByOrderId(orderID);

            foreach (PreOrder19dianInfo obj in listPreOrder19dianInfo)
            {
                //redEnvelopeAmountTotal = redEnvelopeAmountTotal + redEnvelopeConnPreOrderOperate.GetPayOrderConsumeRedEnvelopeAmount(obj.preOrder19dianId);//红包抵扣金额

                //ThirdPartyPaymentInfo thirdPayment = preOrder19dianManager.SelectPreorderPayAmount(obj.preOrder19dianId);//第三方支付金额
                //double extendPay = preOrder19dianManager.SelectExtendPay(obj.preOrder19dianId);//额外收取的钱

                //double thirdAmount= Common.ToDouble(thirdPayment.Amount) - extendPay;
                //thirdAmountTotal = thirdAmountTotal + thirdAmount;

                //double balanceAmount = Common.ToDouble(prePaidSum - payment.thirdAmount - payment.redEnvelopeAmount);//粮票抵扣金额 
                //balanceAmountTotal = balanceAmountTotal + balanceAmount;

                Preorder19DianLineManager lineManager = new Preorder19DianLineManager();
                var orderLineList = lineManager.GetListByQuery(new Model.QueryObject.Preorder19DianLineQueryObject() { Preorder19DianId = obj.preOrder19dianId, State = 102 });
                if (orderLineList != null && orderLineList.Count > 0)
                {

                    var orderLine = orderLineList.FirstOrDefault(p => (VAOrderUsedPayMode)p.PayType == VAOrderUsedPayMode.ALIPAY || (VAOrderUsedPayMode)p.PayType == VAOrderUsedPayMode.WECHAT);
                    // 第三方支付
                    if (orderLine != null)
                    {
                        var thirdAmount = orderLine.Amount;
                        thirdAmountTotal = thirdAmountTotal + thirdAmount;
                    }
                    // 红包支付
                    orderLine = orderLineList.FirstOrDefault(p => (VAOrderUsedPayMode)p.PayType == VAOrderUsedPayMode.REDENVELOPE);
                    if (orderLine != null)
                    {
                        var redEnvelopeAmount = orderLine.Amount;
                        redEnvelopeAmountTotal = redEnvelopeAmountTotal + redEnvelopeAmount;
                    }
                    // 粮票支付
                    orderLine = orderLineList.FirstOrDefault(p => (VAOrderUsedPayMode)p.PayType == VAOrderUsedPayMode.BALANCE);
                    if (orderLine != null)
                    {
                        var balanceAmount = orderLine.Amount;
                        balanceAmountTotal = balanceAmountTotal + balanceAmount;
                    }
                }
            }
            decimal extendPay = (decimal)preOrder19dianManager.SelectExtendPay(preOrder19dianId);//纯红包补贴
            payment.redEnvelopeAmount = Common.ToDecimal(redEnvelopeAmountTotal);
            payment.balanceAmount = Common.ToDecimal(balanceAmountTotal);
            payment.thirdAmount = Common.ToDecimal(thirdAmountTotal) - extendPay;

            return payment;
        }
        /// <summary>
        /// 获取该点单对账的状态
        /// </summary>
        /// <param name="preOrder19dianId">点单流水号</param>
        /// <returns></returns>
        public static int GetPreOrderApproveStatus(long preOrder19dianId)
        {
            int isApproved = Common.ToInt32(Common.GetFieldValue("PreOrder19dian", "isApproved", "preOrder19dianId='" + preOrder19dianId + "'"));
            isApproved = isApproved == 1 ? 1 : 0;//1 已对账//0 未对账
            return isApproved;
        }
        /// <summary>
        /// 获取该点单审核的状态
        /// </summary>
        /// <param name="preOrder19dianId">点单流水号</param>
        /// <returns></returns>
        public static int GetPreOrderConfrimStatus(long preOrder19dianId)
        {
            int isShopConfirmed = Common.ToInt32(Common.GetFieldValue("PreOrder19dian", "isShopConfirmed", "preOrder19dianId='" + preOrder19dianId + "'"));
            isShopConfirmed = isShopConfirmed == 1 ? 1 : 0;//1 已审核//0 未审核
            return isShopConfirmed;
        }
    }
}
