using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Transactions;
using VAGastronomistMobileApp.TheThirdPartyPaymentDll;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.WebPageDll;
using System.Configuration;
using System.Threading;

public partial class aliquickpaynotify : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string currentDate = System.DateTime.Now.Date.ToString("yyyyMMdd");
            string filePath = Server.MapPath("~/Logs/paymentLog" + currentDate + ".txt");
            bool debugFlag = false;
            if (Common.debugFlag == "true")
            {
                debugFlag = true;
            }
            //获取notify_data数据，需要添加notify_data=
            //不需要解密，直接是明文xml格式
            string notify_data = Request.Form["notify_data"].ToString();
            using (StreamWriter file = new StreamWriter(@filePath, true))
            {
                file.WriteLine(currentDate + ":" + "notify_data:" + notify_data);
            }
            //获取签名
            string sign = Request.Form["sign"].ToString();

            //验证签名
            bool vailSign = Function.Verify("notify_data=" + notify_data, sign, Config.Alipaypublick, Config.Input_charset_UTF8);
            if (!vailSign)
            {
                Response.Write("fail");
                return;
            }

            //获取交易状态
            string trade_status = Function.GetStrForXmlDoc(notify_data, "notify/trade_status");

            if (!trade_status.Equals("TRADE_FINISHED") && !trade_status.Equals("TRADE_SUCCESS"))
            {
                Response.Write("fail");
            }
            else
            {
                string trade_no = Function.GetStrForXmlDoc(notify_data, "notify/trade_no");
                string buyer_email = Function.GetStrForXmlDoc(notify_data, "notify/buyer_email");
                string strOutTradeNo = Function.GetStrForXmlDoc(notify_data, "notify/out_trade_no");

                //先拿掉out_trade_no的前缀，才是我方DB中真实的out_trade_no
                string prefix = string.Empty;
                if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["wechatPrefix"]))
                {
                    prefix = ConfigurationManager.AppSettings["wechatPrefix"].ToString();
                }
                long out_trade_no = Common.ToInt64(strOutTradeNo.Replace(prefix, ""));//拿掉前缀后的out_trade_no


                double total_fee = Common.ToDouble(Function.GetStrForXmlDoc(notify_data, "notify/total_fee"));
                AlipayOperate alipayOpe = new AlipayOperate();
                DataTable dtAlipayOrder = alipayOpe.QueryAlipayOrder(out_trade_no);
                if (dtAlipayOrder.Rows.Count == 1)
                {
                    AlipayOrderInfo alipayOrder = new AlipayOrderInfo();
                    alipayOrder.alipayOrderId = out_trade_no;
                    alipayOrder.totalFee = total_fee;
                    alipayOrder.orderPayTime = System.DateTime.Now;
                    alipayOrder.orderStatus = VAAlipayOrderStatus.PAID;
                    alipayOrder.aliTradeNo = trade_no;
                    alipayOrder.aliBuyerEmail = buyer_email;
                    long customerId = Common.ToInt64(dtAlipayOrder.Rows[0]["customerId"]);
                    bool flagModifyMoneyRemained = false;
                    long alipayOrderConnOrderId = Common.ToInt64(dtAlipayOrder.Rows[0]["connId"]);
                    int alipayOrderConnOrderType = Common.ToInt32(dtAlipayOrder.Rows[0]["conn19dianOrderType"]);
                    using (TransactionScope scope = new TransactionScope())
                    {
                        if (alipayOpe.ModifyAlipayOrder(alipayOrder))//先修改支付宝订单状态
                        {
                            Money19dianDetail money19dianDetail = new Money19dianDetail
                            {
                                customerId = customerId,
                                changeTime = System.DateTime.Now,
                                changeValue = total_fee,
                                remainMoney = SybMoneyCustomerOperate.GetCustomerRemainMoney(customerId) + total_fee,//
                                flowNumber = SybMoneyOperate.CreateCustomerFlowNumber(customerId),//流水号
                                accountType = (int)AccountType.ALIPAY,//来源类型
                                accountTypeConnId = Common.ToString(alipayOrderConnOrderId),
                                inoutcomeType = (int)InoutcomeType.IN,
                                companyId = 0,
                                shopId = 0
                            };
                            PreOrder19dianOperate preOrder19dianOpe = new PreOrder19dianOperate();

                            DataTable dtPreorder = preOrder19dianOpe.QueryPreorder(alipayOrderConnOrderId);
                            DataView dvPreorder = dtPreorder.DefaultView;
                            dvPreorder.RowFilter = "isPaid <> '" + (int)VAPreorderIsPaid.PAID + "'";//Edit at 2014-3-24
                            if (dvPreorder.Count == 1)
                            {
                                money19dianDetail.companyId = Common.ToInt32(dvPreorder[0]["companyId"]);
                                money19dianDetail.shopId = Common.ToInt32(dvPreorder[0]["shopId"]);
                            }

                            bool flag = false;
                            switch (alipayOrderConnOrderType)
                            {
                                #region
                                case (int)VAPayOrderType.PAY_PREORDER:
                                case (int)VAPayOrderType.PAY_PREORDER_NEW://201401新支付流程                                  
                                    if (dvPreorder.Count == 1)
                                    {
                                        money19dianDetail.changeReason = Common.GetEnumDescription(VA19dianMoneyChangeReason.MONEY19DIAN_PREPAY_PREORDER).Replace("{0}", Common.ToString(alipayOrderConnOrderId));
                                    }
                                    break;
                                case (int)VAPayOrderType.PAY_CHARGE://支付粮票充值//20140504
                                    ClientRechargeOperate clientRechargeOperate = new ClientRechargeOperate();
                                    DataTable dtRecharge = clientRechargeOperate.QueryCustomerRecharge(alipayOrderConnOrderId);
                                    if (dtRecharge.Rows.Count == 1)
                                    {
                                        money19dianDetail.changeReason = Common.GetEnumDescription(VA19dianMoneyChangeReason.MONEY19DIAN_Account_Recharge);
                                        money19dianDetail.shopId = Common.ToInt32(dtRecharge.Rows[0]["shopId"]);
                                        CompanyOperate cOperate = new CompanyOperate();
                                        money19dianDetail.companyId = cOperate.GetCompanyId(money19dianDetail.shopId);
                                        flag = true;
                                    }
                                    else
                                    {
                                        using (StreamWriter file = new StreamWriter(@filePath, true))
                                        {
                                            file.WriteLine(System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + " aliquickpay更新用户余额时没找到对应的点单：" + out_trade_no.ToString());
                                        }
                                        Response.Write("fail");
                                        return;
                                    }
                                    break;
                                case (int)VAPayOrderType.DIRECT_PAYMENT://直接付款 add by wangc 20140319
                                    if (dvPreorder.Count == 1)
                                    {
                                        money19dianDetail.changeReason = Common.GetEnumDescription(VA19dianMoneyChangeReason.MONEY19DIAN_DIRECT_PAYMENT).Replace("{0}", Common.ToString(alipayOrderConnOrderId));
                                    }
                                    break;
                                case (int)VAPayOrderType.PAY_PREORDER_AND_RECHARGE://20140505增加充值购买粮票功能 add by wangc                                     
                                    if (dvPreorder.Count == 1)
                                    {
                                        money19dianDetail.changeReason = Common.GetEnumDescription(VA19dianMoneyChangeReason.MONEY19DIAN_PREPAY_PREORDER).Replace("{0}", Common.ToString(alipayOrderConnOrderId));
                                    }
                                    break;
                                case (int)VAPayOrderType.DIRECT_PAYMENT_AND_RECHARGE://20140505直接付款 add by wangc 
                                    if (dvPreorder.Count == 1)
                                    {
                                        money19dianDetail.changeReason = Common.GetEnumDescription(VA19dianMoneyChangeReason.MONEY19DIAN_DIRECT_PAYMENT).Replace("{0}", Common.ToString(alipayOrderConnOrderId));
                                    }
                                    break;
                                case (int)VAPayOrderType.PAY_DIFFENENCE://20140505直接付款 add by wangc 
                                    if (dvPreorder.Count == 1)
                                    {
                                        money19dianDetail.changeReason = Common.GetEnumDescription(VA19dianMoneyChangeReason.MONEY19DIAN_SERVICE_PAY_DIFFERENCE).Replace("{0}", Common.ToString(alipayOrderConnOrderId));
                                    }
                                    break;
                                default:
                                    break;
                                #endregion
                            }
                            if (dvPreorder.Count < 1)
                            {
                                bool repeat = alipayOpe.UpdateOrderStatus(out_trade_no.ToString(), VAAlipayOrderStatus.REPEAT_PAID);
                                scope.Complete();
                                using (StreamWriter file = new StreamWriter(@filePath, true))
                                {
                                    file.WriteLine(System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + " aliquickpay更新用户余额时没找到对应的点单：" + out_trade_no.ToString());
                                    file.WriteLine(System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + " 视为重复支付，更新第三方订单状态为REPEAT_PAID，结果：" + repeat.ToString());
                                }
                                Response.Write("success");
                                return;
                            }
                            if (SybMoneyCustomerOperate.AccountBalanceChanges(money19dianDetail, flag))
                            {
                                scope.Complete();
                                flagModifyMoneyRemained = true;
                                if (flagModifyMoneyRemained)
                                {
                                    if (debugFlag)
                                    {
                                        using (StreamWriter file = new StreamWriter(@filePath, true))
                                        {
                                            file.WriteLine(System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + " 更新余额成功：" + out_trade_no.ToString());
                                        }
                                    }
                                    try
                                    {
                                        if (alipayOrderConnOrderType > 0 && alipayOrderConnOrderId > 0)
                                        {
                                            //再去执行相应的购买代码
                                            AlipayNotify alipayNotify = new AlipayNotify()
                                            {
                                                out_trade_no = out_trade_no.ToString(),
                                                alipayOrderConnOrderId = alipayOrderConnOrderId,
                                                alipayOrderConnOrderType = alipayOrderConnOrderType,
                                                buyerEmail = buyer_email,
                                                totalFee = total_fee,
                                            };
                                            Thread purchaseThread = new Thread(Purchase);
                                            purchaseThread.Start(alipayNotify);
                                        }
                                    }
                                    catch (System.Exception ex)
                                    {
                                        using (StreamWriter file = new StreamWriter(@filePath, true))
                                        {
                                            file.WriteLine(System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + "---aliquickpay通知时出错了:" + notify_data + "---" + ex.ToString());
                                        }
                                    }
                                    finally
                                    {
                                        //成功必须在页面上输出success，支付宝才不会再发送通知
                                        Response.Write("success");
                                    }
                                }
                                else
                                {
                                    using (StreamWriter file = new StreamWriter(@filePath, true))
                                    {
                                        file.WriteLine(System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + " aliquickpay通知时更新余额失败：" + out_trade_no.ToString());
                                    }
                                    Response.Write("fail");
                                }
                            }
                            else
                            {
                                using (StreamWriter file = new StreamWriter(@filePath, true))
                                {
                                    file.WriteLine(System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + " aliquickpay通知时更新余额失败：" + out_trade_no.ToString());
                                }
                                Response.Write("fail");
                            }
                        }
                        else
                        {
                            using (StreamWriter file = new StreamWriter(@filePath, true))
                            {
                                file.WriteLine(System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + " aliquickpay通知时更新订单失败：" + out_trade_no.ToString());
                            }
                            Response.Write("fail");
                        }
                    }
                }
                else
                {
                    using (StreamWriter file = new StreamWriter(@filePath, true))
                    {
                        file.WriteLine(System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + " aliquickpay首次支付点单时没找到对应的点单：" + out_trade_no.ToString());
                    }
                    Response.Write("fail");
                }
                ///////////////////////////////处理数据/////////////////////////////////
                // 用户这里可以写自己的商业逻辑
                // 例如：修改数据库订单状态
                // 以下数据仅仅进行演示如何调取
                // 参数对照请详细查阅开发文档
                // 里面有详细说明
                //string partner = Alipay.Class.Function.GetStrForXmlDoc(notify_data, "notify/partner");
                //string discount = Alipay.Class.Function.GetStrForXmlDoc(notify_data, "notify/discount");
                //string payment_type = Alipay.Class.Function.GetStrForXmlDoc(notify_data, "notify/payment_type");
                //string subject = Alipay.Class.Function.GetStrForXmlDoc(notify_data, "notify/subject");
                //string trade_no = Alipay.Class.Function.GetStrForXmlDoc(notify_data, "notify/trade_no");
                //string buyer_email = Alipay.Class.Function.GetStrForXmlDoc(notify_data, "notify/buyer_email");
                //string gmt_create = Alipay.Class.Function.GetStrForXmlDoc(notify_data, "notify/gmt_create");
                //string quantity = Alipay.Class.Function.GetStrForXmlDoc(notify_data, "notify/quantity");
                //string out_trade_no = Alipay.Class.Function.GetStrForXmlDoc(notify_data, "notify/out_trade_no");
                //string seller_id = Alipay.Class.Function.GetStrForXmlDoc(notify_data, "notify/seller_id");
                //string is_total_fee_adjust = Alipay.Class.Function.GetStrForXmlDoc(notify_data, "notify/trade_status");
                //string total_fee = Alipay.Class.Function.GetStrForXmlDoc(notify_data, "notify/total_fee");
                //string gmt_payment = Alipay.Class.Function.GetStrForXmlDoc(notify_data, "notify/gmt_payment");
                //string seller_email = Alipay.Class.Function.GetStrForXmlDoc(notify_data, "notify/seller_email");
                //string gmt_close = Alipay.Class.Function.GetStrForXmlDoc(notify_data, "notify/gmt_close");
                //string price = Alipay.Class.Function.GetStrForXmlDoc(notify_data, "notify/price");
                //string buyer_id = Alipay.Class.Function.GetStrForXmlDoc(notify_data, "notify/buyer_id");
                //string use_coupon = Alipay.Class.Function.GetStrForXmlDoc(notify_data, "notify/use_coupon");
                ////////////////////////////////////////////////////////////////////////////
            }
        }
    }

    private void Purchase(object alipayNotify)
    {
        CustomerOperate customerOperate = new CustomerOperate();
        AlipayNotify notify = (AlipayNotify)alipayNotify;
        string out_trade_no = notify.out_trade_no;
        long alipayOrderConnOrderId = notify.alipayOrderConnOrderId;
        int alipayOrderConnOrderType = notify.alipayOrderConnOrderType;

        string currentDate = System.DateTime.Now.Date.ToString("yyyyMMdd");
        string filePath = Server.MapPath("~/Logs/paymentLog" + currentDate + ".txt");
        bool debugFlag = false;
        if (Common.debugFlag == "true")
        {
            debugFlag = true;
        }
        switch (alipayOrderConnOrderType)
        {
            #region

            case (int)VAPayOrderType.PAY_PREORDER_AND_RECHARGE://20140505点菜新支付流程 wangc
                using (StreamWriter file = new StreamWriter(@filePath, true))
                {
                    file.WriteLine(System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + " 新流程：" + out_trade_no.ToString());
                }
                PreOrder19dianOperate preOrderOper = new PreOrder19dianOperate();
                DataTable dtOrderNew = preOrderOper.QueryPreorder(alipayOrderConnOrderId);
                DataView dvOrderNew = dtOrderNew.DefaultView;
                dvOrderNew.RowFilter = "isPaid <> '" + (int)VAPreorderIsPaid.PAID + "'";
                if (dvOrderNew.Count == 1)
                {
                    ClientRechargePaymentOrderRequest clientRechargePaymentOrderRequest = new ClientRechargePaymentOrderRequest();
                    clientRechargePaymentOrderRequest.preOrderId = alipayOrderConnOrderId;
                    clientRechargePaymentOrderRequest.orderInJson = Common.ToString(dvOrderNew[0]["orderInJson"]);//选择菜品JSON
                    clientRechargePaymentOrderRequest.shopId = Common.ToInt32(dvOrderNew[0]["shopId"]);
                    clientRechargePaymentOrderRequest.isAddbyList = 1;
                    clientRechargePaymentOrderRequest.sundryList = JsonOperate.JsonDeserialize<List<VASundryInfo>>(Common.ToString(dvOrderNew[0]["sundryJson"]));
                    clientRechargePaymentOrderRequest.preOrderPayMode = (int)VAClientPayMode.ALI_PAY_PLUGIN;
                    clientRechargePaymentOrderRequest.boolDualPayment = true;//是否二次支付，服务器端使用，客户端不需要处理
                    clientRechargePaymentOrderRequest.cookie = Common.ToString(customerOperate.SelectCustomerCookieById(Common.ToInt64(dvOrderNew[0]["customerId"])));
                    clientRechargePaymentOrderRequest.uuid = Common.ToString(dvOrderNew[0]["customerUUID"]);
                    clientRechargePaymentOrderRequest.appType = (VAAppType)dvOrderNew[0]["appType"];//客户端设备类别
                    clientRechargePaymentOrderRequest.clientBuild = dvOrderNew[0]["appBuild"].ToString();//客户端设备版本号
                    clientRechargePaymentOrderRequest.couponId =
                        new CouponOperate().GetCouponGetDetailIdByOrderId(alipayOrderConnOrderId, clientRechargePaymentOrderRequest.appType,
                            clientRechargePaymentOrderRequest.clientBuild);
                    clientRechargePaymentOrderRequest.couponType = (int)CouponTypeEnum.OneSelf;
                    clientRechargePaymentOrderRequest.thirdPayAccount = notify.buyerEmail;
                    clientRechargePaymentOrderRequest.thirdPayType = (int)VAOrderUsedPayMode.ALIPAY;
                    clientRechargePaymentOrderRequest.thirdPayAmount = notify.totalFee;

                    ClientIndexListOperate clientIndexListOperate = new ClientIndexListOperate();
                    ClientRechargePaymentOrderResponse clientRechargePaymentOrderResponse = null;
                    if (Common.CheckLatestBuild_201506(clientRechargePaymentOrderRequest.appType, clientRechargePaymentOrderRequest.clientBuild))//新版本
                    {
                        clientRechargePaymentOrderRequest.type = VAMessageType.CLIENT_RECHARGE_PAYMENT_ORDER_V1_REQUEST;
                        clientRechargePaymentOrderResponse = ClientIndexListOperate.ClientPaymentOrder(clientRechargePaymentOrderRequest);
                    }
                    else//老版本
                    {
                        clientRechargePaymentOrderRequest.type = VAMessageType.CLIENT_RECHARGE_PAYMENT_ORDER_REQUEST;
                        clientRechargePaymentOrderResponse = clientIndexListOperate.ClientRechargePaymentOrder(clientRechargePaymentOrderRequest);
                    }


                    if (clientRechargePaymentOrderResponse.result == VAResult.VA_OK)
                    {
                        if (debugFlag)
                        {
                            using (StreamWriter file = new StreamWriter(@filePath, true))
                            {
                                file.WriteLine("继续支付点单成功：" + alipayOrderConnOrderId.ToString());
                            }
                        }
                    }
                    else
                    {
                        using (StreamWriter file = new StreamWriter(@filePath, true))
                        {
                            file.WriteLine(System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + " aliquickpay继续支付点单失败：" + alipayOrderConnOrderId.ToString() + clientRechargePaymentOrderResponse.result.ToString());
                        }
                    }
                }
                else
                {
                    using (StreamWriter file = new StreamWriter(@filePath, true))
                    {
                        file.WriteLine(System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + " aliquickpay继续支付点单时没找到对应的点单：" + alipayOrderConnOrderId.ToString());
                    }
                }
                break;
            case (int)VAPayOrderType.DIRECT_PAYMENT_AND_RECHARGE://20140505 wangc 直接付款
                PreOrder19dianOperate directPaymentOperate = new PreOrder19dianOperate();
                DataTable dtDirectPaymentNew = directPaymentOperate.QueryPreorder(alipayOrderConnOrderId);
                DataView dvDirectPaymentNew = dtDirectPaymentNew.DefaultView;
                dvDirectPaymentNew.RowFilter = "isPaid <> '" + (int)VAPreorderIsPaid.PAID + "'";//Edit at 2014-3-24
                if (dvDirectPaymentNew.Count == 1)
                {
                    ClientRechargeDirectPaymentRequest clientRechargeDirectPaymentRequest = new ClientRechargeDirectPaymentRequest();
                    clientRechargeDirectPaymentRequest.preOrderId = alipayOrderConnOrderId;
                    clientRechargeDirectPaymentRequest.shopId = Common.ToInt32(dvDirectPaymentNew[0]["shopId"]);
                    clientRechargeDirectPaymentRequest.preOrderPayMode = (int)VAClientPayMode.ALI_PAY_PLUGIN;
                    clientRechargeDirectPaymentRequest.boolDualPayment = true;//是否二次支付，服务器端使用，客户端不需要处理
                    clientRechargeDirectPaymentRequest.cookie = Common.ToString(customerOperate.SelectCustomerCookieById(Common.ToInt64(dvDirectPaymentNew[0]["customerId"])));
                    clientRechargeDirectPaymentRequest.uuid = Common.ToString(dvDirectPaymentNew[0]["customerUUID"]);
                    clientRechargeDirectPaymentRequest.payAmount = Common.ToDouble(dvDirectPaymentNew[0]["preOrderSum"]);
                    clientRechargeDirectPaymentRequest.deskNumber = Common.ToString(dvDirectPaymentNew[0]["deskNumber"]);
                    clientRechargeDirectPaymentRequest.appType = (VAAppType)dvDirectPaymentNew[0]["appType"];//客户端设备类别
                    clientRechargeDirectPaymentRequest.clientBuild = dvDirectPaymentNew[0]["appBuild"].ToString();//客户端设备版本号
                    clientRechargeDirectPaymentRequest.couponId =
                       new CouponOperate().GetCouponGetDetailIdByOrderId(alipayOrderConnOrderId, clientRechargeDirectPaymentRequest.appType,
                           clientRechargeDirectPaymentRequest.clientBuild);

                    clientRechargeDirectPaymentRequest.couponType = (int)CouponTypeEnum.OneSelf;

                    using (StreamWriter file = new StreamWriter(@filePath, true))
                    {
                        file.WriteLine(System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + " thirdPay1：" + alipayOrderConnOrderId.ToString() + "--" + clientRechargeDirectPaymentRequest.thirdPayAccount + "--" + clientRechargeDirectPaymentRequest.thirdPayType + "--" + clientRechargeDirectPaymentRequest.thirdPayAmount);
                    }

                    clientRechargeDirectPaymentRequest.thirdPayAccount = notify.buyerEmail;
                    clientRechargeDirectPaymentRequest.thirdPayType = (int)VAOrderUsedPayMode.ALIPAY;
                    clientRechargeDirectPaymentRequest.thirdPayAmount = Math.Round(notify.totalFee, 2);


                    using (StreamWriter file = new StreamWriter(@filePath, true))
                    {
                        file.WriteLine(System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + " thirdPay2：" + alipayOrderConnOrderId.ToString() + "--" + clientRechargeDirectPaymentRequest.thirdPayAccount + "--" + clientRechargeDirectPaymentRequest.thirdPayType + "--" + clientRechargeDirectPaymentRequest.thirdPayAmount);
                    }

                    ClientIndexListOperate clientIndexListOperate = new ClientIndexListOperate();
                    ClientRechargeDirectPaymentResponse clientRechargeDirectPaymentResponse = null;

                    if (Common.CheckLatestBuild_201506(clientRechargeDirectPaymentRequest.appType, clientRechargeDirectPaymentRequest.clientBuild))//新版本
                    {
                        clientRechargeDirectPaymentRequest.type = VAMessageType.CLIENT_RECHARGE_DIRECT_PAYMENT_V1_REQUEST;
                        clientRechargeDirectPaymentResponse = clientIndexListOperate.ClientDirectPayment(clientRechargeDirectPaymentRequest);

                        using (StreamWriter file = new StreamWriter(@filePath, true))
                        {
                            file.WriteLine(System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + " thirdPay3：" + alipayOrderConnOrderId.ToString() + "--" + clientRechargeDirectPaymentRequest.thirdPayAccount + "--" + clientRechargeDirectPaymentRequest.thirdPayType + "--" + clientRechargeDirectPaymentRequest.thirdPayAmount);
                        }
                    }
                    else//老版本
                    {
                        clientRechargeDirectPaymentRequest.type = VAMessageType.CLIENT_RECHARGE_DIRECT_PAYMENT_REQUEST;
                        clientRechargeDirectPaymentResponse = clientIndexListOperate.ClientRechargeDirectPayment(clientRechargeDirectPaymentRequest);
                    }

                    if (clientRechargeDirectPaymentResponse.result == VAResult.VA_OK)
                    {
                        if (debugFlag)
                        {
                            using (StreamWriter file = new StreamWriter(@filePath, true))
                            {
                                file.WriteLine("aliquickpay继续直接付款成功：" + alipayOrderConnOrderId.ToString());
                            }
                        }
                    }
                    else
                    {
                        using (StreamWriter file = new StreamWriter(@filePath, true))
                        {
                            file.WriteLine(System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + " aliquickpay继续直接付款失败：" + alipayOrderConnOrderId.ToString() + clientRechargeDirectPaymentResponse.result.ToString());
                        }
                    }
                }
                else
                {
                    using (StreamWriter file = new StreamWriter(@filePath, true))
                    {
                        file.WriteLine(System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + "aliquickpay继续直接付款时没找到对应的点单：" + alipayOrderConnOrderId.ToString());
                    }
                }
                break;
            case (int)VAPayOrderType.PAY_CHARGE://20140504 wangc 客户端充值，支付粮票
                ClientRechargeOperate clientRechargeOperate = new ClientRechargeOperate();
                DataTable dtRecharge = clientRechargeOperate.QueryCustomerRecharge(alipayOrderConnOrderId);
                DataView dvRecharge = dtRecharge.DefaultView;
                dvRecharge.RowFilter = "payStatus = '" + (int)VACustomerChargeOrderStatus.NOT_PAID + "'";
                if (dvRecharge.Count == 1)
                {
                    ClientPersonCenterRechargeRequest request = new ClientPersonCenterRechargeRequest();
                    request.rechargeOrderId = alipayOrderConnOrderId;
                    request.rechargeActivityId = Common.ToInt32(dvRecharge[0]["rechargeId"]);
                    request.payMode = (int)VAClientPayMode.ALI_PAY_PLUGIN;
                    request.boolDualPayment = true;
                    request.cookie = Common.ToString(customerOperate.SelectCustomerCookieById(Common.ToInt64(dvRecharge[0]["customerId"])));
                    request.uuid = Common.ToString(dvRecharge[0]["customerUUID"]);
                    request.type = VAMessageType.CLIENT_PERSON_CENTER_RECHARGE_REQUEST;

                    ClientIndexListOperate clientIndexListOperate = new ClientIndexListOperate();
                    ClientPersonCenterRechargeReponse reponse = ClientIndexListOperate.ClientPersonCenterRecharge(request);
                    if (reponse.result == VAResult.VA_OK)
                    {
                        if (debugFlag)
                        {
                            using (StreamWriter file = new StreamWriter(@filePath, true))
                            {
                                file.WriteLine("aliquickpay继续充值成功：" + alipayOrderConnOrderId.ToString());
                            }
                        }
                    }
                    else
                    {
                        using (StreamWriter file = new StreamWriter(@filePath, true))
                        {
                            file.WriteLine(System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + " aliquickpay继续充值失败：" + alipayOrderConnOrderId.ToString() + reponse.result.ToString());
                        }
                    }
                }
                else
                {
                    using (StreamWriter file = new StreamWriter(@filePath, true))
                    {
                        file.WriteLine(System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + "aliquickpay继续充值时没找到对应的点单：" + alipayOrderConnOrderId.ToString());
                    }
                }
                break;
            case (int)VAPayOrderType.PAY_DIFFENENCE:
                using (StreamWriter file = new StreamWriter(@filePath, true))
                {
                    file.WriteLine(System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + " 补差价：" + alipayOrderConnOrderId.ToString());
                }
                PreOrder19dianOperate preOrder19dianOperate = new PreOrder19dianOperate();
                DataTable dtpayDifferenceRecharge = preOrder19dianOperate.QueryPreorder(alipayOrderConnOrderId);
                DataView dvpayDifferenceRecharge = dtpayDifferenceRecharge.DefaultView;  
                dvpayDifferenceRecharge.RowFilter = "isPaid <> '" + (int)VAPreorderIsPaid.PAID + "'";
                if (dvpayDifferenceRecharge.Count == 1)
                {
                    bool chargeResult = false;
                    ClientIndexListOperate clientIndexListOperate = new ClientIndexListOperate();
                    chargeResult =
                        clientIndexListOperate.ThirdPayDiffenenceQuery(Int64.Parse(dtpayDifferenceRecharge.Rows[0]["preOrder19dianId"].ToString()), notify.buyerEmail, 2, notify.totalFee);
                    if (chargeResult)
                    {
                        if (debugFlag)
                        {
                            using (StreamWriter file = new StreamWriter(@filePath, true))
                            {
                                file.WriteLine("aliquickpay继续充值成功：" + alipayOrderConnOrderId.ToString());
                            }
                        }
                    }
                    else
                    {
                        using (StreamWriter file = new StreamWriter(@filePath, true))
                        {
                            file.WriteLine(System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + " aliquickpay继续充值失败：" + alipayOrderConnOrderId.ToString() );
                        }
                    }
                }
                else
                {
                    using (StreamWriter file = new StreamWriter(@filePath, true))
                    {
                        file.WriteLine(System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + "aliquickpay继续充值时没找到对应的点单：" + alipayOrderConnOrderId.ToString());
                    }
                }
                break;
            default:
                break;
            #endregion
        }
    }
}