using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Data;
using System.Transactions;
using VAGastronomistMobileApp.TheThirdPartyPaymentDll;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.WebPageDll;
using System.Text;
using System.IO;

public partial class UnionPayNotify : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            byte[] content = new byte[Request.InputStream.Length];
            this.Request.InputStream.Read(content, 0, content.Length);
            string notify_data = Request.ContentEncoding.GetString(content);
            string currentDate = System.DateTime.Now.Date.ToString("yyyyMMdd");
            string filePath = Server.MapPath("~/Logs/paymentLog" + currentDate + ".txt");
            if (Common.debugFlag == "true")
            {
                using (StreamWriter file = new StreamWriter(@filePath, true))
                {
                    file.WriteLine(System.DateTime.Now.ToString() + ":成功跳转到UnionPayNotify页面~，开始更新操作..." + notify_data);
                }
            }
            try
            {
                XmlDocument xml = new XmlDocument();
                xml.LoadXml(notify_data);
                if (Common.debugFlag == "true")
                {
                    using (StreamWriter file = new StreamWriter(@filePath, true))
                    {
                        file.WriteLine("loadXml成功...");
                    }
                }
                string transType = xml.SelectSingleNode("/upomp/transType").InnerText;
                string merchantId = xml.SelectSingleNode("/upomp/merchantId").InnerText;
                string merchantOrderId = xml.SelectSingleNode("/upomp/merchantOrderId").InnerText;
                string merchantOrderAmt = xml.SelectSingleNode("/upomp/merchantOrderAmt").InnerText;
                string settleDate = xml.SelectSingleNode("/upomp/settleDate").InnerText;
                string setlAmt = xml.SelectSingleNode("/upomp/setlAmt").InnerText;
                string setlCurrency = xml.SelectSingleNode("/upomp/setlCurrency").InnerText;
                string converRate = xml.SelectSingleNode("/upomp/converRate").InnerText;
                string cupsQid = xml.SelectSingleNode("/upomp/cupsQid").InnerText; ;
                string cupsTraceNum = xml.SelectSingleNode("/upomp/cupsTraceNum").InnerText;
                string cupsTraceTime = xml.SelectSingleNode("/upomp/cupsTraceTime").InnerText;
                string cupsRespCode = xml.SelectSingleNode("/upomp/cupsRespCode").InnerText;
                string cupsRespDesc = xml.SelectSingleNode("/upomp/cupsRespDesc").InnerText;
                string respCode = xml.SelectSingleNode("/upomp/respCode").InnerText;
                string base64Sign = xml.SelectSingleNode("/upomp/sign").InnerText;
                if (Common.debugFlag == "true")
                {
                    using (StreamWriter file = new StreamWriter(@filePath, true))
                    {
                        file.WriteLine("base64Sign：" + base64Sign);
                    }
                }
                //创建待签名数组，注意Notify这里数组不需要进行排序，请保持以下顺序
                Dictionary<string, string> sArrary = new Dictionary<string, string>();
                #region //组装验签数组
                sArrary.Add("transType", transType);
                sArrary.Add("merchantId", merchantId);
                sArrary.Add("merchantOrderId", merchantOrderId);
                sArrary.Add("merchantOrderAmt", merchantOrderAmt);
                sArrary.Add("settleDate", settleDate);
                sArrary.Add("setlAmt", setlAmt);
                sArrary.Add("setlCurrency", setlCurrency);
                sArrary.Add("converRate", converRate);
                sArrary.Add("cupsQid", cupsQid);
                sArrary.Add("cupsTraceNum", cupsTraceNum);
                sArrary.Add("cupsTraceTime", cupsTraceTime);
                sArrary.Add("cupsRespCode", cupsRespCode);
                sArrary.Add("cupsRespDesc", cupsRespDesc);
                sArrary.Add("respCode", respCode);
                #endregion

                string src = Function.CreateLinkString(sArrary);
                if (Common.debugFlag == "true")
                {
                    using (StreamWriter file = new StreamWriter(@filePath, true))
                    {
                        file.WriteLine("src：" + src);
                    }
                }
                if (RSACryptoService.RSADecryptBC(src, base64Sign, UnionPayParameters.FrontPublicPath))
                {
                    if (Common.debugFlag == "true")
                    {
                        using (StreamWriter file = new StreamWriter(@filePath, true))
                        {
                            file.WriteLine("签名通过...");
                        }
                    }
                    //签名通过
                    int iTransType = Common.ToInt32(transType);

                    switch (iTransType)
                    {
                        case (int)TransStatus.PAYING:
                            #region //交易类型: 01 消费
                            string returnCode = "1000";

                            UnionPayOperate operate = new UnionPayOperate();
                            merchantOrderId = merchantOrderId.Replace(UnionPayParameters.UNION_PAY_FRONT_CODE, "");   //订单 viewallocpower100111   100111为数据库自增长ID
                            long unionPayId = Common.ToInt64(merchantOrderId);
                            if (Common.debugFlag == "true")
                            {
                                using (StreamWriter file = new StreamWriter(@filePath, true))
                                {
                                    file.WriteLine("cupsRespCode:" + cupsRespCode);
                                }
                            }
                            if (cupsRespCode == "00")
                            {//交易成功 
                                DataTable dtUnionPreorder = new DataTable();
                                dtUnionPreorder = operate.QueryUnionPayOrder(unionPayId);
                                if (dtUnionPreorder.Rows.Count == 1)//订单存在
                                {
                                    int unionOrderConnOrderType = Common.ToInt32(dtUnionPreorder.Rows[0]["conn19dianOrderType"]);
                                    long unionOrderConnOrderId = Common.ToInt64(dtUnionPreorder.Rows[0]["connId"]);
                                    long customerId = Common.ToInt64(dtUnionPreorder.Rows[0]["customerId"]);
                                    if (Common.debugFlag == "true")
                                    {
                                        using (StreamWriter file = new StreamWriter(@filePath, true))
                                        {
                                            file.WriteLine("customerId:" + customerId + "cupsQid:" + cupsQid);
                                        }
                                    }
                                    bool flagModifyMoneyRemained = false;
                                    double total_fee = Common.ToDouble(setlAmt) / 100;
                                    total_fee = Math.Round(total_fee, 2);
                                    using (TransactionScope scope = new TransactionScope())
                                    {
                                        if (Common.debugFlag == "true")
                                        {
                                            using (StreamWriter file = new StreamWriter(@filePath, true))
                                            {
                                                file.WriteLine("开始更新订单纪录...");
                                            }
                                        }
                                        #region 更新订单纪录
                                        if (operate.ModifyUnionpayOrderStatus((int)VAAlipayOrderStatus.PAID, DateTime.Now, unionPayId, cupsQid)) //更新订单纪录
                                        {
                                            CustomerOperate customerOpe = new CustomerOperate();

                                            if (Common.debugFlag == "true")
                                            {
                                                using (StreamWriter file = new StreamWriter(@filePath, true))
                                                {
                                                    file.WriteLine("更新订单纪录成功！开始更新余额...customerId:" + customerId + "total_fee:" + total_fee);
                                                }
                                            }
                                            //用户余额变更（加钱）
                                            Money19dianDetail money19dianDetail = new Money19dianDetail
                                            {
                                                customerId = customerId,

                                                changeTime = System.DateTime.Now,
                                                changeValue = total_fee,
                                                remainMoney = SybMoneyCustomerOperate.GetCustomerRemainMoney(customerId) + total_fee,//
                                                flowNumber = SybMoneyOperate.CreateCustomerFlowNumber(customerId),//流水号
                                                accountType = (int)AccountType.UNIONPAY,
                                                accountTypeConnId = Common.ToString(unionOrderConnOrderId),
                                                inoutcomeType = (int)InoutcomeType.IN,
                                                companyId = 0,
                                                shopId = 0
                                            };
                                            PreOrder19dianOperate preOrder19dianOpe = new PreOrder19dianOperate();
                                            bool flag = false;
                                            switch (unionOrderConnOrderType)
                                            {
                                                case (int)VAPayOrderType.PAY_PREORDER:
                                                case (int)VAPayOrderType.PAY_PREORDER_NEW://201401新支付流程
                                                    money19dianDetail.changeReason = Common.GetEnumDescription(VA19dianMoneyChangeReason.MONEY19DIAN_PREPAY_PREORDER).Replace("{0}", Common.ToString(unionOrderConnOrderId));
                                                    DataTable dtPreorder = preOrder19dianOpe.QueryPreorder(unionOrderConnOrderId);
                                                    DataView dvPreorder = dtPreorder.DefaultView;
                                                    dvPreorder.RowFilter = "isPaid <> '" + (int)VAPreorderIsPaid.PAID + "'";//Edit at 2014-3-24
                                                    money19dianDetail.companyId = Common.ToInt32(dvPreorder[0]["companyId"]);
                                                    money19dianDetail.shopId = Common.ToInt32(dvPreorder[0]["shopId"]);
                                                    break;
                                                case (int)VAPayOrderType.PAY_CHARGE:
                                                    ClientRechargeOperate clientRechargeOperate = new ClientRechargeOperate();
                                                    DataTable dtRecharge = clientRechargeOperate.QueryCustomerRecharge(unionOrderConnOrderId);
                                                    money19dianDetail.changeReason = Common.GetEnumDescription(VA19dianMoneyChangeReason.MONEY19DIAN_Account_Recharge);
                                                    money19dianDetail.shopId = Common.ToInt32(dtRecharge.Rows[0]["shopId"]);
                                                    CompanyOperate cOperate = new CompanyOperate();
                                                    money19dianDetail.companyId = cOperate.GetCompanyId(money19dianDetail.shopId);
                                                    flag = true;
                                                    break;
                                                case (int)VAPayOrderType.DIRECT_PAYMENT://直接付款 add by wangc 20140319
                                                    money19dianDetail.changeReason = Common.GetEnumDescription(VA19dianMoneyChangeReason.MONEY19DIAN_DIRECT_PAYMENT).Replace("{0}", Common.ToString(unionOrderConnOrderId));
                                                    DataTable dtDirectPaymentPreorder = preOrder19dianOpe.QueryPreorder(unionOrderConnOrderId);
                                                    DataView dvDirectPaymentPreorder = dtDirectPaymentPreorder.DefaultView;
                                                    dvDirectPaymentPreorder.RowFilter = "isPaid <> '" + (int)VAPreorderIsPaid.PAID + "'";//Edit at 2014-3-24
                                                    money19dianDetail.companyId = Common.ToInt32(dvDirectPaymentPreorder[0]["companyId"]);
                                                    money19dianDetail.shopId = Common.ToInt32(dvDirectPaymentPreorder[0]["shopId"]);
                                                    break;
                                                case (int)VAPayOrderType.PAY_PREORDER_AND_RECHARGE://20140505增加充值购买粮票功能
                                                    DataTable dtPreOrderNew = preOrder19dianOpe.QueryPreorder(unionOrderConnOrderId);
                                                    DataView dvPreOrderNew = dtPreOrderNew.DefaultView;
                                                    dvPreOrderNew.RowFilter = "isPaid <> '" + (int)VAPreorderIsPaid.PAID + "'";
                                                    money19dianDetail.changeReason = Common.GetEnumDescription(VA19dianMoneyChangeReason.MONEY19DIAN_PREPAY_PREORDER).Replace("{0}", Common.ToString(unionOrderConnOrderId));
                                                    money19dianDetail.companyId = Common.ToInt32(dvPreOrderNew[0]["companyId"]);
                                                    money19dianDetail.shopId = Common.ToInt32(dvPreOrderNew[0]["shopId"]);
                                                    break;
                                                case (int)VAPayOrderType.DIRECT_PAYMENT_AND_RECHARGE://20140505直接付款 add by wangc 
                                                    money19dianDetail.changeReason = Common.GetEnumDescription(VA19dianMoneyChangeReason.MONEY19DIAN_DIRECT_PAYMENT).Replace("{0}", Common.ToString(unionOrderConnOrderId));
                                                    DataTable dtDirectPaymentPreorderNew = preOrder19dianOpe.QueryPreorder(unionOrderConnOrderId);
                                                    DataView dvDirectPaymentPreorderNew = dtDirectPaymentPreorderNew.DefaultView;
                                                    dvDirectPaymentPreorderNew.RowFilter = "isPaid <> '" + (int)VAPreorderIsPaid.PAID + "'";
                                                    money19dianDetail.companyId = Common.ToInt32(dvDirectPaymentPreorderNew[0]["companyId"]);
                                                    money19dianDetail.shopId = Common.ToInt32(dvDirectPaymentPreorderNew[0]["shopId"]);
                                                    break;
                                                default:
                                                    break;
                                            }
                                            if (SybMoneyCustomerOperate.AccountBalanceChanges(money19dianDetail, flag))//用户余额变更（充值）
                                            {
                                                if (Common.debugFlag == "true")
                                                {
                                                    using (StreamWriter file = new StreamWriter(@filePath, true))
                                                    {
                                                        file.WriteLine("开始更新余额详情...");
                                                    }
                                                }
                                                returnCode = "0000";
                                                scope.Complete();
                                                flagModifyMoneyRemained = true;
                                                if (Common.debugFlag == "true")
                                                {
                                                    using (StreamWriter file = new StreamWriter(@filePath, true))
                                                    {
                                                        file.WriteLine("更新余额成功！");
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                using (StreamWriter file = new StreamWriter(@filePath, true))
                                                {
                                                    file.WriteLine(System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + " 银联通知时更新余额失败：" + unionOrderConnOrderId.ToString());
                                                }
                                                returnCode = "2000";
                                            }
                                        }
                                        else
                                        {
                                            using (StreamWriter file = new StreamWriter(@filePath, true))
                                            {
                                                file.WriteLine(System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + " 银联通知时更新订单失败：" + unionOrderConnOrderId.ToString());
                                            }
                                            returnCode = "2000";
                                        }
                                        #endregion
                                    }
                                    if (flagModifyMoneyRemained)
                                    {
                                        if (unionOrderConnOrderType > 0 && unionOrderConnOrderId > 0)
                                        {
                                            //再去执行相应的购买代码
                                            switch (unionOrderConnOrderType)
                                            {
                                                //case (int)VAPayOrderType.PAY_PREORDER_NEW://201401新支付流程
                                                //    PreOrder19dianOperate preOrder19dianOper = new PreOrder19dianOperate();
                                                //    DataTable dtOrder = preOrder19dianOper.QueryPreorder(unionOrderConnOrderId);
                                                //    DataView dvOrder = dtOrder.DefaultView;
                                                //    dvOrder.RowFilter = "isPaid <> '" + (int)VAPreorderIsPaid.PAID + "'";//Edit at 2014-3-24
                                                //    if (dvOrder.Count == 1)
                                                //    {
                                                //        VAClientFastPaymentOrderRequest vaClientFastPaymentOrderRequest = new VAClientFastPaymentOrderRequest();
                                                //        vaClientFastPaymentOrderRequest.preOrderId = unionOrderConnOrderId;
                                                //        vaClientFastPaymentOrderRequest.orderInJson = Common.ToString(dvOrder[0]["orderInJson"]);//选择菜品JSON
                                                //        vaClientFastPaymentOrderRequest.shopId = Common.ToInt32(dvOrder[0]["shopId"]);
                                                //        vaClientFastPaymentOrderRequest.isAddbyList = 1;
                                                //        vaClientFastPaymentOrderRequest.sundryList = JsonOperate.JsonDeserialize<List<VASundryInfo>>(Common.ToString(dvOrder[0]["sundryJson"]));
                                                //        vaClientFastPaymentOrderRequest.preOrderPayMode = (int)VAClientPayMode.UNION_PAY_PLUGIN;
                                                //        vaClientFastPaymentOrderRequest.boolDualPayment = true;//是否二次支付，服务器端使用，客户端不需要处理
                                                //        vaClientFastPaymentOrderRequest.cookie = Common.ToString(dvOrder[0]["customerCookie"]);
                                                //        vaClientFastPaymentOrderRequest.uuid = Common.ToString(dvOrder[0]["customerUUID"]);
                                                //        vaClientFastPaymentOrderRequest.type = VAMessageType.CLIENT_FAST_PAYMENT_ORDER_REQUEST;
                                                //        ClientIndexListOperate clientIndexListOperate = new ClientIndexListOperate();
                                                //        VAClientFastPaymentOrderReponse vaClientFastPaymentOrderReponse = clientIndexListOperate.ClientFastPaymentOrder(vaClientFastPaymentOrderRequest);
                                                //        if (vaClientFastPaymentOrderReponse.result == VAResult.VA_OK)
                                                //        {
                                                //            if (Common.debugFlag == "true")
                                                //            {
                                                //                using (StreamWriter file = new StreamWriter(@filePath, true))
                                                //                {
                                                //                    file.WriteLine("继续支付点单成功：" + unionOrderConnOrderId.ToString());
                                                //                }
                                                //            }
                                                //        }
                                                //        else
                                                //        {
                                                //            using (StreamWriter file = new StreamWriter(@filePath, true))
                                                //            {
                                                //                file.WriteLine(System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + " 银联继续支付点单失败：" + unionOrderConnOrderId.ToString() + vaClientFastPaymentOrderReponse.result.ToString());
                                                //            }
                                                //        }
                                                //    }
                                                //    else
                                                //    {
                                                //        using (StreamWriter file = new StreamWriter(@filePath, true))
                                                //        {
                                                //            file.WriteLine(System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + "银联继续支付点单时没找到对应的点单：" + unionOrderConnOrderId.ToString());
                                                //        }
                                                //    }
                                                //    break;
                                                case (int)VAPayOrderType.PAY_PREORDER_AND_RECHARGE://20140505新支付流程 wangc
                                                    PreOrder19dianOperate preOrderOper = new PreOrder19dianOperate();
                                                    DataTable dtOrderNew = preOrderOper.QueryPreorder(unionOrderConnOrderId);
                                                    DataView dvOrderNew = dtOrderNew.DefaultView;
                                                    dvOrderNew.RowFilter = "isPaid <> '" + (int)VAPreorderIsPaid.PAID + "'";
                                                    if (dvOrderNew.Count == 1)
                                                    {
                                                        ClientRechargePaymentOrderRequest clientRechargePaymentOrderRequest = new ClientRechargePaymentOrderRequest();
                                                        clientRechargePaymentOrderRequest.preOrderId = unionOrderConnOrderId;
                                                        clientRechargePaymentOrderRequest.orderInJson = Common.ToString(dvOrderNew[0]["orderInJson"]);//选择菜品JSON
                                                        clientRechargePaymentOrderRequest.shopId = Common.ToInt32(dvOrderNew[0]["shopId"]);
                                                        clientRechargePaymentOrderRequest.isAddbyList = 1;
                                                        clientRechargePaymentOrderRequest.sundryList = JsonOperate.JsonDeserialize<List<VASundryInfo>>(Common.ToString(dvOrderNew[0]["sundryJson"]));
                                                        clientRechargePaymentOrderRequest.preOrderPayMode = (int)VAClientPayMode.UNION_PAY_PLUGIN;
                                                        clientRechargePaymentOrderRequest.boolDualPayment = true;//是否二次支付，服务器端使用，客户端不需要处理
                                                        clientRechargePaymentOrderRequest.cookie = Common.ToString(dvOrderNew[0]["customerCookie"]);
                                                        clientRechargePaymentOrderRequest.uuid = Common.ToString(dvOrderNew[0]["customerUUID"]);
                                                        clientRechargePaymentOrderRequest.type = VAMessageType.CLIENT_RECHARGE_PAYMENT_ORDER_REQUEST;
                                                        ClientIndexListOperate clientIndexListOperate = new ClientIndexListOperate();
                                                        ClientRechargePaymentOrderResponse clientRechargePaymentOrderResponse = clientIndexListOperate.ClientRechargePaymentOrder(clientRechargePaymentOrderRequest);
                                                        if (clientRechargePaymentOrderResponse.result == VAResult.VA_OK)
                                                        {
                                                            if (Common.debugFlag == "true")
                                                            {
                                                                using (StreamWriter file = new StreamWriter(@filePath, true))
                                                                {
                                                                    file.WriteLine("继续支付点单成功：" + unionOrderConnOrderId.ToString());
                                                                }
                                                            }
                                                        }
                                                        else
                                                        {
                                                            using (StreamWriter file = new StreamWriter(@filePath, true))
                                                            {
                                                                file.WriteLine(System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + " 银联继续支付点单失败：" + unionOrderConnOrderId.ToString() + clientRechargePaymentOrderResponse.result.ToString());
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        using (StreamWriter file = new StreamWriter(@filePath, true))
                                                        {
                                                            file.WriteLine(System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + "银联继续支付点单时没找到对应的点单：" + unionOrderConnOrderId.ToString());
                                                        }
                                                    }
                                                    break;
                                                //case (int)VAPayOrderType.DIRECT_PAYMENT://20140319 wangc 直接付款
                                                //    PreOrder19dianOperate directPaymentOper = new PreOrder19dianOperate();
                                                //    DataTable dtDirectPayment = directPaymentOper.QueryPreorder(unionOrderConnOrderId);
                                                //    DataView dvDirectPayment = dtDirectPayment.DefaultView;
                                                //    dvDirectPayment.RowFilter = "isPaid <> '" + (int)VAPreorderIsPaid.PAID + "'";//Edit at 2014-3-24
                                                //    if (dvDirectPayment.Count == 1)
                                                //    {
                                                //        VAClientDirectPaymentRequest clientDirectPaymentRequest = new VAClientDirectPaymentRequest();
                                                //        clientDirectPaymentRequest.preOrderId = unionOrderConnOrderId;
                                                //        clientDirectPaymentRequest.shopId = Common.ToInt32(dvDirectPayment[0]["shopId"]);
                                                //        clientDirectPaymentRequest.preOrderPayMode = (int)VAClientPayMode.UNION_PAY_PLUGIN;
                                                //        clientDirectPaymentRequest.boolDualPayment = true;//是否二次支付，服务器端使用，客户端不需要处理
                                                //        clientDirectPaymentRequest.cookie = Common.ToString(dvDirectPayment[0]["customerCookie"]);
                                                //        clientDirectPaymentRequest.uuid = Common.ToString(dvDirectPayment[0]["customerUUID"]);
                                                //        clientDirectPaymentRequest.type = VAMessageType.CLIENT_DIRECT_PAYMENT_REQUEST;
                                                //        clientDirectPaymentRequest.payAmount = Common.ToDouble(dvDirectPayment[0]["preOrderSum"]);
                                                //        clientDirectPaymentRequest.deskNumber = Common.ToString(dvDirectPayment[0]["deskNumber"]);
                                                //        ClientIndexListOperate clientIndexListOperate = new ClientIndexListOperate();
                                                //        VAClientDirectPaymentReponse clientDirectPaymentReponse = clientIndexListOperate.ClientDirectPayment(clientDirectPaymentRequest);
                                                //        if (clientDirectPaymentReponse.result == VAResult.VA_OK)
                                                //        {
                                                //            if (Common.debugFlag == "true")
                                                //            {
                                                //                using (StreamWriter file = new StreamWriter(@filePath, true))
                                                //                {
                                                //                    file.WriteLine("继续直接付款成功：" + unionOrderConnOrderId.ToString());
                                                //                }
                                                //            }
                                                //        }
                                                //        else
                                                //        {
                                                //            using (StreamWriter file = new StreamWriter(@filePath, true))
                                                //            {
                                                //                file.WriteLine(System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + " 银联继续直接付款失败：" + unionOrderConnOrderId.ToString() + clientDirectPaymentReponse.result.ToString());
                                                //            }
                                                //        }
                                                //    }
                                                //    else
                                                //    {
                                                //        using (StreamWriter file = new StreamWriter(@filePath, true))
                                                //        {
                                                //            file.WriteLine(System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + "银联继续直接付款时没找到对应的点单：" + unionOrderConnOrderId.ToString());
                                                //        }
                                                //    }
                                                //    break;
                                                case (int)VAPayOrderType.DIRECT_PAYMENT_AND_RECHARGE://20140505 wangc 直接付款
                                                    PreOrder19dianOperate directPaymentOperate = new PreOrder19dianOperate();
                                                    DataTable dtDirectPaymentNew = directPaymentOperate.QueryPreorder(unionOrderConnOrderId);
                                                    DataView dvDirectPaymentNew = dtDirectPaymentNew.DefaultView;
                                                    dvDirectPaymentNew.RowFilter = "isPaid <> '" + (int)VAPreorderIsPaid.PAID + "'";//Edit at 2014-3-24
                                                    if (dvDirectPaymentNew.Count == 1)
                                                    {
                                                        ClientRechargeDirectPaymentRequest clientRechargeDirectPaymentRequest = new ClientRechargeDirectPaymentRequest();
                                                        clientRechargeDirectPaymentRequest.preOrderId = unionOrderConnOrderId;
                                                        clientRechargeDirectPaymentRequest.shopId = Common.ToInt32(dvDirectPaymentNew[0]["shopId"]);
                                                        clientRechargeDirectPaymentRequest.preOrderPayMode = (int)VAClientPayMode.UNION_PAY_PLUGIN;
                                                        clientRechargeDirectPaymentRequest.boolDualPayment = true;//是否二次支付，服务器端使用，客户端不需要处理
                                                        clientRechargeDirectPaymentRequest.cookie = Common.ToString(dvDirectPaymentNew[0]["customerCookie"]);
                                                        clientRechargeDirectPaymentRequest.uuid = Common.ToString(dvDirectPaymentNew[0]["customerUUID"]);
                                                        clientRechargeDirectPaymentRequest.type = VAMessageType.CLIENT_RECHARGE_DIRECT_PAYMENT_REQUEST;
                                                        clientRechargeDirectPaymentRequest.payAmount = Common.ToDouble(dvDirectPaymentNew[0]["preOrderSum"]);
                                                        clientRechargeDirectPaymentRequest.deskNumber = Common.ToString(dvDirectPaymentNew[0]["deskNumber"]);
                                                        ClientIndexListOperate clientIndexListOperate = new ClientIndexListOperate();
                                                        ClientRechargeDirectPaymentResponse clientRechargeDirectPaymentResponse = clientIndexListOperate.ClientRechargeDirectPayment(clientRechargeDirectPaymentRequest);
                                                        if (clientRechargeDirectPaymentResponse.result == VAResult.VA_OK)
                                                        {
                                                            if (Common.debugFlag == "true")
                                                            {
                                                                using (StreamWriter file = new StreamWriter(@filePath, true))
                                                                {
                                                                    file.WriteLine("继续直接付款成功：" + unionOrderConnOrderId.ToString());
                                                                }
                                                            }
                                                        }
                                                        else
                                                        {
                                                            using (StreamWriter file = new StreamWriter(@filePath, true))
                                                            {
                                                                file.WriteLine(System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + " 银联继续直接付款失败：" + unionOrderConnOrderId.ToString() + clientRechargeDirectPaymentResponse.result.ToString());
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        using (StreamWriter file = new StreamWriter(@filePath, true))
                                                        {
                                                            file.WriteLine(System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + "银联继续直接付款时没找到对应的点单：" + unionOrderConnOrderId.ToString());
                                                        }
                                                    }
                                                    break;
                                                case (int)VAPayOrderType.PAY_CHARGE://20140504 wangc 客户端充值，支付粮票
                                                    ClientRechargeOperate clientRechargeOperate = new ClientRechargeOperate();
                                                    DataTable dtRecharge = clientRechargeOperate.QueryCustomerRecharge(unionOrderConnOrderId);
                                                    DataView dvRecharge = dtRecharge.DefaultView;
                                                    dvRecharge.RowFilter = "payStatus = '" + (int)VACustomerChargeOrderStatus.NOT_PAID + "'";
                                                    if (dvRecharge.Count == 1)
                                                    {
                                                        ClientPersonCenterRechargeRequest request = new ClientPersonCenterRechargeRequest();
                                                        request.rechargeOrderId = unionOrderConnOrderId;
                                                        request.rechargeActivityId = Common.ToInt32(dvRecharge[0]["rechargeId"]);
                                                        request.payMode = (int)VAClientPayMode.UNION_PAY_PLUGIN;
                                                        request.boolDualPayment = true;
                                                        request.cookie = Common.ToString(dvRecharge[0]["customerCookie"]);
                                                        request.uuid = Common.ToString(dvRecharge[0]["customerUUID"]);
                                                        request.type = VAMessageType.CLIENT_PERSON_CENTER_RECHARGE_REQUEST;
                                                        ClientIndexListOperate clientIndexListOperate = new ClientIndexListOperate();
                                                        ClientPersonCenterRechargeReponse reponse = ClientIndexListOperate.ClientPersonCenterRecharge(request);
                                                        if (reponse.result == VAResult.VA_OK)
                                                        {
                                                            if (Common.debugFlag == "true")
                                                            {
                                                                using (StreamWriter file = new StreamWriter(@filePath, true))
                                                                {
                                                                    file.WriteLine("银联继续充值成功：" + unionOrderConnOrderId.ToString());
                                                                }
                                                            }
                                                        }
                                                        else
                                                        {
                                                            using (StreamWriter file = new StreamWriter(@filePath, true))
                                                            {
                                                                file.WriteLine(System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + " 银联继续充值失败：" + unionOrderConnOrderId.ToString() + reponse.result.ToString());
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        using (StreamWriter file = new StreamWriter(@filePath, true))
                                                        {
                                                            file.WriteLine(System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + "银联继续充值时没找到对应的订单：" + unionOrderConnOrderId.ToString());
                                                        }
                                                    }
                                                    break;
                                                default:
                                                    break;
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    returnCode = "2000";
                                }
                            }
                            else
                            {
                                returnCode = "2000";
                            }
                            //向银联返回报文
                            Response.Write(BuildXml(returnCode, transType, merchantId));

                            #endregion
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    if (Common.debugFlag == "true")
                    {
                        using (StreamWriter file = new StreamWriter(@filePath, true))
                        {
                            file.WriteLine("验签名失败了");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                using (StreamWriter file = new StreamWriter(@filePath, true))
                {
                    file.WriteLine(System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + ":银联通知时出错了---" + notify_data + "---" + ex.Message);
                }
            }
        }
    }

    private string BuildXml(string resCode, string transType, string merchantOrderId)
    {
        StringBuilder submit = new StringBuilder();
        submit.Append("<?xml version=\"1.0\" encoding=\"utf-8\" standalone=\"yes\"?>");
        submit.Append("<upomp application=\"TransNotify.Req\" version=\"1.0.0\">");

        submit.AppendFormat("<transType>{0}</transType>", transType);
        submit.AppendFormat("<merchantId>{0}</merchantId>", UnionPayParameters.merchantId);

        submit.AppendFormat("<merchantOrderId>{0}</merchantOrderId>", merchantOrderId);
        submit.AppendFormat("<respCode>{0}</respCode></upomp>", resCode);

        return submit.ToString();
    }

}