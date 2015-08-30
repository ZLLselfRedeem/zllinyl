using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Transactions;
//using Alipay.Class;
using VAGastronomistMobileApp.TheThirdPartyPaymentDll;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.WebPageDll;
using System.Configuration;

public partial class alipaynotify : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string currentDate = System.DateTime.Now.Date.ToString("yyyyMMdd");
            string filePath = Server.MapPath("~/Logs/paymentLog" + currentDate + ".txt");
            //获取加密的notify_data数据
            string notify_data = Request.Form["notify_data"];

            //通过商户私钥进行解密
            notify_data = Function.Decrypt(notify_data, Config.PrivateKey, Config.Input_charset_UTF8);
            //获取签名
            string sign = Request.Form["sign"];

            //创建待签名数组，注意Notify这里数组不需要进行排序，请保持以下顺序
            Dictionary<string, string> sArrary = new Dictionary<string, string>();

            //组装验签数组
            sArrary.Add("service", Request.Form["service"]);
            sArrary.Add("v", Request.Form["v"]);
            sArrary.Add("sec_id", Request.Form["sec_id"]);
            sArrary.Add("notify_data", notify_data);

            //把数组所有元素，按照“参数=参数值”的模式用“&”字符拼接成字符串
            string content = Function.CreateLinkString(sArrary);

            //验证签名
            bool vailSign = Function.Verify(content, sign, Config.Alipaypublick, Config.Input_charset_UTF8);
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
                try
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
                            if (alipayOpe.ModifyAlipayOrder(alipayOrder))
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
                                switch (alipayOrderConnOrderType)
                                {
                                    case (int)VAPayOrderType.PAY_PREORDER:
                                        PreOrder19dianOperate preOrder19dianOpe = new PreOrder19dianOperate();
                                        DataTable dtPreorder = preOrder19dianOpe.QueryPreorder(alipayOrderConnOrderId);
                                        DataView dvPreorder = dtPreorder.DefaultView;
                                        //dvPreorder.RowFilter = "status <> '" + (int)VAPreorderStatus.Prepaid + "'";
                                        dvPreorder.RowFilter = "isPaid <> '" + (int)VAPreorderIsPaid.PAID + "'";//Edit at 2014-3-24
                                        money19dianDetail.changeReason = Common.GetEnumDescription(VA19dianMoneyChangeReason.MONEY19DIAN_PREPAY_PREORDER).Replace("{0}", Common.ToString(alipayOrderConnOrderId));
                                        money19dianDetail.companyId = Common.ToInt32(dvPreorder[0]["companyId"]);
                                        money19dianDetail.shopId = Common.ToInt32(dvPreorder[0]["shopId"]);
                                        break; 
                                    case (int)VAPayOrderType.PAY_CHARGE:
                                        money19dianDetail.changeReason = Common.GetEnumDescription(VA19dianMoneyChangeReason.MONEY19DIAN_Account_Recharge);
                                        break;
                                }
                                if (SybMoneyCustomerOperate.AccountBalanceChanges(money19dianDetail))
                                {
                                    scope.Complete();
                                    flagModifyMoneyRemained = true;
                                }
                                else
                                {
                                    using (StreamWriter file = new StreamWriter(@filePath, true))
                                    {
                                        file.WriteLine(System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + " alipay通知时插入余额变动记录失败：" + out_trade_no.ToString());
                                    }
                                    Response.Write("fail");
                                }
                            }
                            else
                            {
                                using (StreamWriter file = new StreamWriter(@filePath, true))
                                {
                                    file.WriteLine(System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + " alipay通知时更新订单失败：" + out_trade_no.ToString());
                                }
                                Response.Write("fail");
                            }
                        }
                        if (flagModifyMoneyRemained)
                        {
                            try
                            {
                                if (alipayOrderConnOrderType > 0 && alipayOrderConnOrderId > 0)
                                {//再去执行相应的购买代码
                                    switch (alipayOrderConnOrderType)
                                    {
                                        case (int)VAPayOrderType.PAY_PREORDER:
                                            {
                                                //PreOrder19dianOperate preOrder19dianOpe = new PreOrder19dianOperate();
                                                //DataTable dtPreorder = preOrder19dianOpe.QueryPreorder(alipayOrderConnOrderId);
                                                //DataView dvPreorder = dtPreorder.DefaultView;
                                                //dvPreorder.RowFilter = "status <> '" + (int)VAPreorderStatus.Prepaid + "'";
                                                //if (dvPreorder.Count == 1)
                                                //{
                                                //    VAClientPreorderPrepayWithCreditRequest clientPreorderPrepayWithCreditRequest = new VAClientPreorderPrepayWithCreditRequest();
                                                //    clientPreorderPrepayWithCreditRequest.preorderId = alipayOrderConnOrderId;
                                                //    clientPreorderPrepayWithCreditRequest.selectedPolicyId = Common.ToInt32(dvPreorder[0]["prePayPrivilegeId"]);
                                                //    clientPreorderPrepayWithCreditRequest.cookie = Common.ToString(dvPreorder[0]["customerCookie"]);
                                                //    clientPreorderPrepayWithCreditRequest.uuid = Common.ToString(dvPreorder[0]["customerUUID"]);
                                                //    clientPreorderPrepayWithCreditRequest.type = VAMessageType.CLIENT_PREORDER_PREPAY_WITH_CREDIT_REQUEST;
                                                //    VAClientPreorderPrepayWithCreditResponse clientPreorderPrepayWithCreditResponse = preOrder19dianOpe.ClientPreorderPrepayWithCreditEX(clientPreorderPrepayWithCreditRequest);
                                                //    if (clientPreorderPrepayWithCreditResponse.result == VAResult.VA_OK)
                                                //    {
                                                //        if (Common.debugFlag == "true")
                                                //        {
                                                //            using (StreamWriter file = new StreamWriter(@filePath, true))
                                                //            {
                                                //                file.WriteLine("继续支付点单成功：" + alipayOrderConnOrderId.ToString());
                                                //            }
                                                //        }
                                                //    }
                                                //    else
                                                //    {
                                                //        using (StreamWriter file = new StreamWriter(@filePath, true))
                                                //        {
                                                //            file.WriteLine(System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + " alipay继续支付点单失败：" + alipayOrderConnOrderId.ToString() + clientPreorderPrepayWithCreditResponse.result.ToString());
                                                //        }
                                                //    }
                                                //}
                                                //else
                                                //{
                                                //    using (StreamWriter file = new StreamWriter(@filePath, true))
                                                //    {
                                                //        file.WriteLine(System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + " alipay继续支付点单时没找到对应的点单：" + alipayOrderConnOrderId.ToString());
                                                //    }
                                                //}
                                            }
                                            break;
                                        case (int)VAPayOrderType.PAY_CHARGE:
                                            {
                                                CustomerOperate customerOpe = new CustomerOperate();
                                                if (customerOpe.ModifyCustomerChargeOrderStatus(alipayOrderConnOrderId))
                                                {
                                                    if (Common.debugFlag == "true")
                                                    {
                                                        using (StreamWriter file = new StreamWriter(@filePath, true))
                                                        {
                                                            file.WriteLine("继续充值成功：" + alipayOrderConnOrderId.ToString());
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    using (StreamWriter file = new StreamWriter(@filePath, true))
                                                    {
                                                        file.WriteLine(System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + " alipay继续充值失败：" + alipayOrderConnOrderId.ToString());
                                                    }
                                                }
                                            }
                                            break;
                                        default:
                                            break;
                                    }
                                }
                            }
                            catch (System.Exception ex)
                            {
                                using (StreamWriter file = new StreamWriter(@filePath, true))
                                {
                                    file.WriteLine(System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + "---alipay通知时出错了:" + notify_data + "---" + ex.ToString());
                                }
                            }
                            finally
                            {
                                //成功必须在页面上输出success，支付宝才不会再发送通知
                                Response.Write("success");
                            }
                        }
                    }
                }
                catch (System.Exception ex)
                {
                    using (StreamWriter file = new StreamWriter(@filePath, true))
                    {
                        file.WriteLine(System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + "---alipay通知时出错了:" + notify_data + "---" + ex.ToString());
                    }
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
}