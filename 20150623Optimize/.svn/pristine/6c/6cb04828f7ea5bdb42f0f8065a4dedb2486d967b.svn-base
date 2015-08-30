using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text;
using VAGastronomistMobileApp.TheThirdPartyPaymentDll;
using VAGastronomistMobileApp.WebPageDll;
using VAGastronomistMobileApp.Model;
using System.Data;
using System.Transactions;
using System.Configuration;
using System.Threading;
public partial class wechatPayNotify : System.Web.UI.Page
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
            try
            {
                if (debugFlag)
                {//写日志文件开启
                    using (StreamWriter file = new StreamWriter(@filePath, true))
                    {
                        file.WriteLine(currentDate + ":" + "收到wechat通知");
                    }
                }
                #region 1.先获取notifyUrl中参数值
                //协议参数 5个
                string sign = Request.QueryString["sign"];//签名
                string sign_type = Request.QueryString["sign_type"];//签名方式
                string service_version = Request.QueryString["service_version"];//接口版本
                string input_charset = Request.QueryString["input_charset"];//字符集
                string sign_key_index = Request.QueryString["sign_key_index"];//密钥序号
                //业务参数 17个
                string trade_mode = Request.QueryString["trade_mode"];//交易模式
                string trade_state = Request.QueryString["trade_state"];//交易状态
                string pay_info = Request.QueryString["pay_info"];//支付结果信息
                string partner = Request.QueryString["partner"];//商户号
                string bank_type = Request.QueryString["bank_type"];//付款银行
                string bank_billno = Request.QueryString["bank_billno"];//银行订单号
                string total_fee = Request.QueryString["total_fee"];//总金额
                string fee_type = Request.QueryString["fee_type"];//币种
                string notify_id = Request.QueryString["notify_id"];//通知 ID
                string transaction_id = Request.QueryString["transaction_id"];//订单号
                string out_trade_no = Request.QueryString["out_trade_no"];//商户订单号
                string attach = Request.QueryString["attach"];//商家数据包
                string time_end = Request.QueryString["time_end"];//支付完成时间
                string transport_fee = Request.QueryString["transport_fee"];//物流费用
                string product_fee = Request.QueryString["product_fee"];//物品费用
                string discount = Request.QueryString["discount"];//折扣价格
                string buyer_alias = Request.QueryString["buyer_alias"];//买家别名
                if (debugFlag)
                {//写日志文件开启
                    using (StreamWriter file = new StreamWriter(@filePath, true))
                    {
                        file.WriteLine(currentDate + ":" + "sign:" + sign);
                        file.WriteLine(currentDate + ":" + "sign_type:" + sign_type);
                        file.WriteLine(currentDate + ":" + "service_version:" + service_version);
                        file.WriteLine(currentDate + ":" + "input_charset:" + input_charset);
                        file.WriteLine(currentDate + ":" + "sign_key_index:" + sign_key_index);
                        file.WriteLine(currentDate + ":" + "trade_mode:" + trade_mode);
                        file.WriteLine(currentDate + ":" + "trade_state:" + trade_state);
                        file.WriteLine(currentDate + ":" + "pay_info:" + pay_info);
                        file.WriteLine(currentDate + ":" + "partner:" + partner);
                        file.WriteLine(currentDate + ":" + "bank_type:" + bank_type);
                        file.WriteLine(currentDate + ":" + "bank_billno:" + bank_billno);
                        file.WriteLine(currentDate + ":" + "total_fee:" + total_fee);
                        file.WriteLine(currentDate + ":" + "fee_type:" + fee_type);
                        file.WriteLine(currentDate + ":" + "notify_id:" + notify_id);
                        file.WriteLine(currentDate + ":" + "transaction_id:" + transaction_id);
                        file.WriteLine(currentDate + ":" + "out_trade_no:" + out_trade_no);
                        file.WriteLine(currentDate + ":" + "attach:" + attach);
                        file.WriteLine(currentDate + ":" + "time_end:" + time_end);
                        file.WriteLine(currentDate + ":" + "transport_fee:" + transport_fee);
                        file.WriteLine(currentDate + ":" + "product_fee:" + product_fee);
                        file.WriteLine(currentDate + ":" + "discount:" + discount);
                        file.WriteLine(currentDate + ":" + "buyer_alias:" + buyer_alias);
                    }
                }
                //该次支付的用户相关信息
                StreamReader reader = new StreamReader(Request.InputStream);
                string xml = reader.ReadToEnd();
                string openId = "";
                if (!string.IsNullOrEmpty(xml))
                {
                    openId = Function.GetStrForXmlDoc(xml, "xml/OpenId");
                    if (debugFlag)
                    {//写日志文件开启
                        using (StreamWriter file = new StreamWriter(@filePath, true))
                        {
                            file.WriteLine(currentDate + "xml:" + xml);
                        }
                    }
                }
                #endregion

                #region 2.验证Sign

                if (string.IsNullOrEmpty(sign))
                {
                    using (StreamWriter file = new StreamWriter(@filePath, true))
                    {
                        file.WriteLine(System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + " wechatPay未接收到签名：" + out_trade_no);
                    }
                    Response.Write("fail");
                    return;
                }

                //a.除 sign 字段外，所有参数按照字段名的 ascii 码从小到大排序后使用 QueryString 的格
                //式（即 key1=value1&key2=value2…）拼接而成字符串 string1，空值不传递，不参与签名组串
                StringBuilder string1 = new StringBuilder();
                string1.Append(WechatPayFunction.GetAssembly("attach", attach));
                string1.Append(WechatPayFunction.GetAssembly("bank_billno", bank_billno));
                string1.Append(WechatPayFunction.GetAssembly("bank_type", bank_type));
                string1.Append(WechatPayFunction.GetAssembly("buyer_alias", buyer_alias));
                string1.Append(WechatPayFunction.GetAssembly("discount", discount));
                string1.Append(WechatPayFunction.GetAssembly("fee_type", fee_type));
                string1.Append(WechatPayFunction.GetAssembly("input_charset", input_charset));
                string1.Append(WechatPayFunction.GetAssembly("notify_id", notify_id));
                string1.Append(WechatPayFunction.GetAssembly("out_trade_no", out_trade_no));
                string1.Append(WechatPayFunction.GetAssembly("partner", partner));
                string1.Append(WechatPayFunction.GetAssembly("pay_info", pay_info));
                string1.Append(WechatPayFunction.GetAssembly("product_fee", product_fee));
                string1.Append(WechatPayFunction.GetAssembly("service_version", service_version));
                string1.Append(WechatPayFunction.GetAssembly("sign_key_index", sign_key_index));
                string1.Append(WechatPayFunction.GetAssembly("sign_type", sign_type));
                string1.Append(WechatPayFunction.GetAssembly("time_end", time_end));
                string1.Append(WechatPayFunction.GetAssembly("total_fee", total_fee));
                string1.Append(WechatPayFunction.GetAssembly("trade_mode", trade_mode));
                string1.Append(WechatPayFunction.GetAssembly("trade_state", trade_state));
                string1.Append(WechatPayFunction.GetAssembly("transaction_id", transaction_id));
                string1.Append(WechatPayFunction.GetAssembly("transport_fee", transport_fee));

                //if (debugFlag)
                //{//写日志文件开启
                //    using (StreamWriter file = new StreamWriter(@filePath, true))
                //    {
                //        file.WriteLine(currentDate + "string1&:" + string1);
                //    }
                //}

                string1.Remove(string1.Length - 1, 1);//移除最后一个&

                //if (debugFlag)
                //{//写日志文件开启
                //    using (StreamWriter file = new StreamWriter(@filePath, true))
                //    {
                //        file.WriteLine(currentDate + "string1:" + string1);
                //    }
                //}

                //b. 在 string1 最 后 拼 接 上 key=paternerKey 得 到 stringSignTemp 字 符 串 ， 并 对
                //stringSignTemp 进行 md5 运算，再将得到的字符串所有字符转换为大写，得到 sign 值signValue

                string stringSignTemp = string1.ToString() + "&key=" + WechatPayConfig.partnerKey + "";
                string signValue = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(stringSignTemp, "MD5").ToUpper();
               
                if (sign != signValue)//验证签名失败
                {
                    using (StreamWriter file = new StreamWriter(@filePath, true))
                    {
                        file.WriteLine(System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + " wechatPay验证签名失败：" + out_trade_no);
                    }
                    Response.Write("fail");
                    return;
                }

                #endregion

                #region 3.检查交易状态，若成功则执行业务逻辑
                if (trade_state != "0")//交易失败
                {
                    using (StreamWriter file = new StreamWriter(@filePath, true))
                    {
                        file.WriteLine(System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + " wechatPay交易失败：" + out_trade_no);
                    }
                    Response.Write("fail");
                }
                else
                {
                    //先拿掉out_trade_no的前缀，才是我方DB中真实的out_trade_no，Add at 2014-4-2
                    string prefix = string.Empty;
                    if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["wechatPrefix"]))
                    {
                        prefix = ConfigurationManager.AppSettings["wechatPrefix"].ToString();
                    }
                    out_trade_no = out_trade_no.Replace(prefix, "");//拿掉前缀后的out_trade_no
                    if (debugFlag)
                    {//写日志文件开启
                        using (StreamWriter file = new StreamWriter(@filePath, true))
                        {
                            file.WriteLine(currentDate + ":" + "交易状态OK");
                        }
                    }
                    //交易成功，更新订单状态及其他操作
                    WechatPayOperate _payOperate = new WechatPayOperate();//BLL

                    DataTable dtPayOrder = _payOperate.QueryWechatPayOrder(out_trade_no);
                    if (dtPayOrder != null && dtPayOrder.Rows.Count == 1)//如果找到相应订单
                    {
                        if (debugFlag)
                        {//写日志文件开启
                            using (StreamWriter file = new StreamWriter(@filePath, true))
                            {
                                file.WriteLine(currentDate + ":" + "找到订单：" + out_trade_no);
                            }
                        }
                        WechatPayOrderInfo order = new WechatPayOrderInfo();//Model
                        order.outTradeNo = Common.ToInt64(out_trade_no);
                        order.totalFee = Common.ToDouble(total_fee) / 100;
                        order.orderPayTime = DateTime.Now;
                        order.orderStatus = VAAlipayOrderStatus.PAID;
                        #region
                        if (!string.IsNullOrEmpty(openId))
                        {
                            order.openId = openId;
                        }
                        else
                        {
                            order.openId = "";
                        }
                        if (!string.IsNullOrEmpty(bank_type))
                        {
                            order.bankType = bank_type;
                        }
                        else
                        {
                            order.bankType = "";
                        }
                        if (!string.IsNullOrEmpty(bank_billno))
                        {
                            order.bankBillno = bank_billno;
                        }
                        else
                        {
                            order.bankBillno = "";
                        }
                        if (!string.IsNullOrEmpty(notify_id))
                        {
                            order.notifyId = notify_id;
                        }
                        else
                        {
                            order.notifyId = "";
                        }
                        if (!string.IsNullOrEmpty(transaction_id))
                        {
                            order.transactionId = transaction_id;
                        }
                        else
                        {
                            order.transactionId = "";
                        }
                        #endregion
                        long customerId = Common.ToInt64(dtPayOrder.Rows[0]["customerId"]);
                        bool flagModifyMoneyRemained = false;
                        long wechatOrderConnOrderId = Common.ToInt64(dtPayOrder.Rows[0]["connId"]);
                        int wechatOrderConnOrderType = Common.ToInt32(dtPayOrder.Rows[0]["conn19dianOrderType"]);

                        using (TransactionScope scope = new TransactionScope())
                        {
                            #region 微信支付订单更新成功
                            if (_payOperate.UpdateWechatOrder(order))
                            {
                                if (debugFlag)
                                {//写日志文件开启
                                    using (StreamWriter file = new StreamWriter(@filePath, true))
                                    {
                                        file.WriteLine(currentDate + ":" + "微信支付订单更新成功：" + out_trade_no);
                                    }
                                }
                                Money19dianDetail money19dianDetail = new Money19dianDetail
                                {
                                    customerId = customerId,
                                    changeTime = DateTime.Now,
                                    changeValue = Common.ToDouble(total_fee) / 100,
                                    remainMoney = SybMoneyCustomerOperate.GetCustomerRemainMoney(customerId) + Common.ToDouble(total_fee) / 100,
                                    flowNumber = SybMoneyOperate.CreateCustomerFlowNumber(customerId),//流水号
                                    accountType = (int)AccountType.WECHATPAY,//来源类型
                                    accountTypeConnId = Common.ToString(wechatOrderConnOrderId),
                                    inoutcomeType = (int)InoutcomeType.IN,
                                    companyId = 0,
                                    shopId = 0
                                };
                                PreOrder19dianOperate _preOrderOperate = new PreOrder19dianOperate();//BLL
                                DataTable dtPreOrder = _preOrderOperate.QueryPreorder(wechatOrderConnOrderId);//找出对应点单
                                DataView dvPreOder = dtPreOrder.DefaultView;
                                dvPreOder.RowFilter = "isPaid <>'" + (int)VAPreorderIsPaid.PAID + "'";//过滤掉已付款 Edit at 2014-3-24
                                if (dvPreOder.Count == 1)
                                {
                                    money19dianDetail.changeReason = Common.GetEnumDescription(VA19dianMoneyChangeReason.MONEY19DIAN_PREPAY_PREORDER).Replace("{0}", Common.ToString(wechatOrderConnOrderId));
                                    money19dianDetail.companyId = Common.ToInt32(dvPreOder[0]["companyId"]);
                                    money19dianDetail.shopId = Common.ToInt32(dvPreOder[0]["shopId"]);
                                }
                                bool flag = false;
                                switch (wechatOrderConnOrderType)
                                {
                                    #region
                                    case (int)VAPayOrderType.PAY_CHARGE:
                                        ClientRechargeOperate clientRechargeOperate = new ClientRechargeOperate();
                                        DataTable dtRecharge = clientRechargeOperate.QueryCustomerRecharge(wechatOrderConnOrderId);
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
                                                file.WriteLine(System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + " 微信支付更新用户余额时没找到对应的点单：" + out_trade_no.ToString());
                                            }
                                            Response.Write("fail");
                                            return;
                                        }
                                        break;
                                    case (int)VAPayOrderType.PAY_PREORDER:
                                    case (int)VAPayOrderType.PAY_PREORDER_NEW://201401新支付流程
                                        if (dvPreOder.Count == 1)
                                        {
                                            money19dianDetail.changeReason = Common.GetEnumDescription(VA19dianMoneyChangeReason.MONEY19DIAN_PREPAY_PREORDER).Replace("{0}", Common.ToString(wechatOrderConnOrderId));
                                        }
                                        break;
                                    case (int)VAPayOrderType.DIRECT_PAYMENT://直接付款 add by wangc 20140319
                                        if (dvPreOder.Count == 1)
                                        {
                                            money19dianDetail.changeReason = Common.GetEnumDescription(VA19dianMoneyChangeReason.MONEY19DIAN_DIRECT_PAYMENT).Replace("{0}", Common.ToString(wechatOrderConnOrderId));
                                        }
                                        break;
                                    case (int)VAPayOrderType.PAY_PREORDER_AND_RECHARGE://20140505增加充值购买粮票功能
                                        if (dvPreOder.Count == 1)
                                        {
                                            money19dianDetail.changeReason = Common.GetEnumDescription(VA19dianMoneyChangeReason.MONEY19DIAN_PREPAY_PREORDER).Replace("{0}", Common.ToString(wechatOrderConnOrderId));
                                        }
                                        break;
                                    case (int)VAPayOrderType.DIRECT_PAYMENT_AND_RECHARGE://20140505直接付款 add by wangc 
                                        if (dvPreOder.Count == 1)
                                        {
                                            money19dianDetail.changeReason = Common.GetEnumDescription(VA19dianMoneyChangeReason.MONEY19DIAN_DIRECT_PAYMENT).Replace("{0}", Common.ToString(wechatOrderConnOrderId));
                                        }
                                        break; 
                                    default:
                                        break;
                                    #endregion
                                }
                                if (dvPreOder.Count < 1)
                                {
                                    bool repeat = _payOperate.UpdateOrderStatus(out_trade_no, VAAlipayOrderStatus.REPEAT_PAID);
                                    scope.Complete();
                                    using (StreamWriter file = new StreamWriter(@filePath, true))
                                    {
                                        file.WriteLine(System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + " 微信支付更新用户余额时没找到对应的点单：" + out_trade_no.ToString());
                                        file.WriteLine(System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + " 视为重复支付，更新第三方订单状态为REPEAT_PAID，结果：" + repeat.ToString());
                                    }
                                    Response.Write("success");
                                    return;
                                }
                                //更新客户余额及明细
                                if (SybMoneyCustomerOperate.AccountBalanceChanges(money19dianDetail, flag))
                                {
                                    if (debugFlag)
                                    {//写日志文件开启
                                        using (StreamWriter file = new StreamWriter(@filePath, true))
                                        {
                                            file.WriteLine(currentDate + ":" + "更新余额成功：" + out_trade_no);
                                        }
                                    }
                                    scope.Complete();
                                    flagModifyMoneyRemained = true;

                                    #region 购买代码
                                    if (flagModifyMoneyRemained)
                                    {
                                        if (debugFlag)
                                        {//写日志文件开启
                                            using (StreamWriter file = new StreamWriter(@filePath, true))
                                            {
                                                file.WriteLine(currentDate + ":" + "更新余额成功2：" + out_trade_no);
                                            }
                                        }
                                        try
                                        {
                                            if (wechatOrderConnOrderType > 0 && wechatOrderConnOrderId > 0)
                                            {
                                                //再去执行相应的购买代码
                                                #region
                                                WechatNotify wechatNotify = new WechatNotify()
                                                {
                                                    out_trade_no = out_trade_no,
                                                    wechatOrderConnOrderId = wechatOrderConnOrderId,
                                                    wechatOrderConnOrderType = wechatOrderConnOrderType,
                                                    totalFee = double.Parse(total_fee) / 100,
                                                    payAccount = openId
                                                };
                                                Thread purchaseThread = new Thread(Purchase);
                                                purchaseThread.Start(wechatNotify);
                                                #endregion
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            using (StreamWriter file = new StreamWriter(@filePath, true))
                                            {
                                                file.WriteLine(System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + "---wechatPay通知时出错了:" + out_trade_no + "---" + ex.ToString());
                                            }
                                        }
                                        finally
                                        {
                                            //成功必须在页面上输出success，微信才不会继续通知
                                            Response.Write("success");
                                        }
                                    }
                                    else
                                    {
                                        using (StreamWriter file = new StreamWriter(@filePath, true))
                                        {
                                            file.WriteLine(System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + " wechatPay通知时更新订单失败：" + out_trade_no.ToString());
                                        }
                                        Response.Write("fail");
                                    }
                                    #endregion
                                }
                                else
                                {
                                    using (StreamWriter file = new StreamWriter(@filePath, true))
                                    {
                                        file.WriteLine(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + " wechatPay通知时更新余额失败：" + out_trade_no);
                                    }
                                    Response.Write("fail");
                                }
                            }
                            #endregion
                            else
                            {
                                using (StreamWriter file = new StreamWriter(@filePath, true))
                                {
                                    file.WriteLine(System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + " wechatPay通知时更新订单失败：" + out_trade_no);
                                }
                                Response.Write("fail");
                            }
                        }
                    }
                    else
                    {
                        using (StreamWriter file = new StreamWriter(@filePath, true))
                        {
                            file.WriteLine(System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + " wechatPay首次支付点单时没找到对应的点单：" + out_trade_no);
                        }
                        Response.Write("fail");
                    }
                }
                #endregion
            }
            catch (System.Exception ex)
            {
                using (StreamWriter file = new StreamWriter(@filePath, true))
                {
                    file.WriteLine("ex:" + ex.ToString());
                }
                Response.Write("fail");
            }
        }
    }

    private void Purchase(object wechatNotify)
    {
        CustomerOperate customerOperate = new CustomerOperate();
        WechatNotify notify = (WechatNotify)wechatNotify;
        string out_trade_no = notify.out_trade_no;
        long wechatOrderConnOrderId = notify.wechatOrderConnOrderId;
        int wechatOrderConnOrderType = notify.wechatOrderConnOrderType;

        string currentDate = System.DateTime.Now.Date.ToString("yyyyMMdd");
        string filePath = Server.MapPath("~/Logs/paymentLog" + currentDate + ".txt");
        bool debugFlag = false;
        if (Common.debugFlag == "true")
        {
            debugFlag = true;
        }

        switch (wechatOrderConnOrderType)
        {
            case (int)VAPayOrderType.PAY_PREORDER:
                break;
            case (int)VAPayOrderType.PAY_CHARGE://20140504 wangc 客户端充值，支付粮票
                ClientRechargeOperate clientRechargeOperate = new ClientRechargeOperate();
                DataTable dtRecharge = clientRechargeOperate.QueryCustomerRecharge(wechatOrderConnOrderId);
                DataView dvRecharge = dtRecharge.DefaultView;
                dvRecharge.RowFilter = "payStatus = '" + (int)VACustomerChargeOrderStatus.NOT_PAID + "'";
                if (dvRecharge.Count == 1)
                {
                    ClientPersonCenterRechargeRequest request = new ClientPersonCenterRechargeRequest();
                    request.rechargeOrderId = wechatOrderConnOrderId;
                    request.rechargeActivityId = Common.ToInt32(dvRecharge[0]["rechargeId"]);
                    request.payMode = (int)VAClientPayMode.WECHAT_PAY_PLUGIN;
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
                                file.WriteLine("微信继续充值成功：" + wechatOrderConnOrderId.ToString());
                            }
                        }
                    }
                    else
                    {
                        using (StreamWriter file = new StreamWriter(@filePath, true))
                        {
                            file.WriteLine(System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + " 微信继续充值失败：" + wechatOrderConnOrderId.ToString() + reponse.result.ToString());
                        }
                    }
                }
                else
                {
                    using (StreamWriter file = new StreamWriter(@filePath, true))
                    {
                        file.WriteLine(System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + "微信继续充值时没找到对应的订单：" + wechatOrderConnOrderId.ToString());
                    }
                }
                break;
            case (int)VAPayOrderType.PAY_DIFFENENCE://20140505新支付流程 wangc
                using (StreamWriter file = new StreamWriter(@filePath, true))
                {
                    file.WriteLine(System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + " 2015078补差价支付：" + out_trade_no);
                }
                PreOrder19dianOperate payDifferencePreOrderOperate = new PreOrder19dianOperate();
                var payDifferencePreOrder = payDifferencePreOrderOperate.GetPreOrder19dianById(wechatOrderConnOrderId);
                if (payDifferencePreOrder != null && payDifferencePreOrder.isPaid != 1)
                {
                    bool chargeResult = false;
                    ClientIndexListOperate clientIndexListOperate = new ClientIndexListOperate();
                    chargeResult =
                        clientIndexListOperate.ThirdPayDiffenenceQuery(payDifferencePreOrder.preOrder19dianId, notify.payAccount, 3, notify.totalFee);
                    if (chargeResult)
                    {
                        if (debugFlag)
                        {
                            using (StreamWriter file = new StreamWriter(@filePath, true))
                            {
                                file.WriteLine("wechatPay补差价点单成功：" + wechatOrderConnOrderId.ToString());
                            }
                        }
                    }
                }
                break;
            case (int)VAPayOrderType.PAY_PREORDER_AND_RECHARGE://20140505新支付流程 wangc
                using (StreamWriter file = new StreamWriter(@filePath, true))
                {
                    file.WriteLine(System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + " 20140505支付新流程：" + out_trade_no);
                }
                PreOrder19dianOperate preOrderOper = new PreOrder19dianOperate();
                DataTable dtOrderNew = preOrderOper.QueryPreorder(wechatOrderConnOrderId);
                DataView dvOrderNew = dtOrderNew.DefaultView;
                dvOrderNew.RowFilter = "isPaid <> '" + (int)VAPreorderIsPaid.PAID + "'";
                if (dvOrderNew.Count == 1)
                {
                    ClientRechargePaymentOrderRequest clientRechargePaymentOrderRequest = new ClientRechargePaymentOrderRequest();
                    clientRechargePaymentOrderRequest.preOrderId = wechatOrderConnOrderId;
                    clientRechargePaymentOrderRequest.orderInJson = Common.ToString(dvOrderNew[0]["orderInJson"]);//选择菜品JSON
                    clientRechargePaymentOrderRequest.shopId = Common.ToInt32(dvOrderNew[0]["shopId"]);
                    clientRechargePaymentOrderRequest.isAddbyList = 1;
                    clientRechargePaymentOrderRequest.sundryList = JsonOperate.JsonDeserialize<List<VASundryInfo>>(Common.ToString(dvOrderNew[0]["sundryJson"]));
                    clientRechargePaymentOrderRequest.preOrderPayMode = (int)VAClientPayMode.WECHAT_PAY_PLUGIN;
                    clientRechargePaymentOrderRequest.boolDualPayment = true;//是否二次支付，服务器端使用，客户端不需要处理
                    clientRechargePaymentOrderRequest.cookie = Common.ToString(customerOperate.SelectCustomerCookieById(Common.ToInt64(dvOrderNew[0]["customerId"])));
                    clientRechargePaymentOrderRequest.uuid = Common.ToString(dvOrderNew[0]["customerUUID"]);
                    clientRechargePaymentOrderRequest.type = VAMessageType.CLIENT_RECHARGE_PAYMENT_ORDER_REQUEST;
                    clientRechargePaymentOrderRequest.appType = (VAAppType)dvOrderNew[0]["appType"];//客户端设备类别
                    clientRechargePaymentOrderRequest.clientBuild = dvOrderNew[0]["appBuild"].ToString();//客户端设备版本号
                    clientRechargePaymentOrderRequest.couponId =
                      new CouponOperate().GetCouponGetDetailIdByOrderId(wechatOrderConnOrderId, clientRechargePaymentOrderRequest.appType,
                          clientRechargePaymentOrderRequest.clientBuild);
                    clientRechargePaymentOrderRequest.couponType = (int)CouponTypeEnum.OneSelf;
                    clientRechargePaymentOrderRequest.thirdPayAccount = notify.payAccount;
                    clientRechargePaymentOrderRequest.thirdPayAmount = notify.totalFee;
                    clientRechargePaymentOrderRequest.thirdPayType = (int)VAOrderUsedPayMode.WECHAT;

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
                                file.WriteLine("wechatPay继续支付点单成功：" + wechatOrderConnOrderId.ToString());
                            }
                        }
                    }
                    else
                    {
                        using (StreamWriter file = new StreamWriter(@filePath, true))
                        {
                            file.WriteLine(System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + " wechatPay继续支付点单失败：" + wechatOrderConnOrderId.ToString() + clientRechargePaymentOrderResponse.result.ToString());
                        }
                    }
                }
                else
                {
                    using (StreamWriter file = new StreamWriter(@filePath, true))
                    {
                        file.WriteLine(System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + " wechatPay继续支付点单时没找到对应的点单：" + wechatOrderConnOrderId.ToString());
                    }
                }
                break;
            case (int)VAPayOrderType.DIRECT_PAYMENT_AND_RECHARGE://20140505 wangc 直接付款
                PreOrder19dianOperate directPaymentOperate = new PreOrder19dianOperate();
                DataTable dtDirectPaymentNew = directPaymentOperate.QueryPreorder(wechatOrderConnOrderId);
                DataView dvDirectPaymentNew = dtDirectPaymentNew.DefaultView;
                dvDirectPaymentNew.RowFilter = "isPaid <> '" + (int)VAPreorderIsPaid.PAID + "'";//Edit at 2014-3-24
                if (dvDirectPaymentNew.Count == 1)
                {
                    ClientRechargeDirectPaymentRequest clientRechargeDirectPaymentRequest = new ClientRechargeDirectPaymentRequest();
                    clientRechargeDirectPaymentRequest.preOrderId = wechatOrderConnOrderId;
                    clientRechargeDirectPaymentRequest.shopId = Common.ToInt32(dvDirectPaymentNew[0]["shopId"]);
                    clientRechargeDirectPaymentRequest.preOrderPayMode = (int)VAClientPayMode.WECHAT_PAY_PLUGIN;
                    clientRechargeDirectPaymentRequest.boolDualPayment = true;//是否二次支付，服务器端使用，客户端不需要处理
                    clientRechargeDirectPaymentRequest.cookie = Common.ToString(customerOperate.SelectCustomerCookieById(Common.ToInt64(dvDirectPaymentNew[0]["customerId"])));
                    clientRechargeDirectPaymentRequest.uuid = Common.ToString(dvDirectPaymentNew[0]["customerUUID"]);
                    clientRechargeDirectPaymentRequest.payAmount = Common.ToDouble(dvDirectPaymentNew[0]["preOrderSum"]);
                    clientRechargeDirectPaymentRequest.deskNumber = Common.ToString(dvDirectPaymentNew[0]["deskNumber"]);
                    clientRechargeDirectPaymentRequest.appType = (VAAppType)dvDirectPaymentNew[0]["appType"];//客户端设备类别
                    clientRechargeDirectPaymentRequest.clientBuild = dvDirectPaymentNew[0]["appBuild"].ToString();//客户端设备版本号
                    clientRechargeDirectPaymentRequest.couponId =
                      new CouponOperate().GetCouponGetDetailIdByOrderId(wechatOrderConnOrderId, clientRechargeDirectPaymentRequest.appType,
                          clientRechargeDirectPaymentRequest.clientBuild);
                    clientRechargeDirectPaymentRequest.couponType = (int)CouponTypeEnum.OneSelf;
                    clientRechargeDirectPaymentRequest.thirdPayAccount = notify.payAccount;
                    clientRechargeDirectPaymentRequest.thirdPayAmount = notify.totalFee;
                    clientRechargeDirectPaymentRequest.thirdPayType = (int)VAOrderUsedPayMode.WECHAT;

                    ClientIndexListOperate clientIndexListOperate = new ClientIndexListOperate();
                    ClientRechargeDirectPaymentResponse clientRechargeDirectPaymentResponse = null;

                    if (Common.CheckLatestBuild_201506(clientRechargeDirectPaymentRequest.appType, clientRechargeDirectPaymentRequest.clientBuild))//新版本
                    {
                        clientRechargeDirectPaymentRequest.type = VAMessageType.CLIENT_RECHARGE_DIRECT_PAYMENT_V1_REQUEST;
                        clientRechargeDirectPaymentResponse = clientIndexListOperate.ClientDirectPayment(clientRechargeDirectPaymentRequest);
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
                                file.WriteLine("继续直接付款成功：" + wechatOrderConnOrderId.ToString());
                            }
                        }
                    }
                    else
                    {
                        using (StreamWriter file = new StreamWriter(@filePath, true))
                        {
                            file.WriteLine(System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + " 微信支付继续直接付款失败：" + wechatOrderConnOrderId.ToString() + clientRechargeDirectPaymentResponse.result.ToString());
                        }
                    }
                }
                else
                {
                    using (StreamWriter file = new StreamWriter(@filePath, true))
                    {
                        file.WriteLine(System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + "微信支付继续直接付款时没找到对应的点单：" + wechatOrderConnOrderId.ToString());
                    }
                }
                break;
            default:
                break;
        }
    }
}