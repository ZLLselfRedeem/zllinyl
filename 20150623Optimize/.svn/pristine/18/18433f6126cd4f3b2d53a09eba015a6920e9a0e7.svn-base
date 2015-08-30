using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Web;
using System.Xml;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.TheThirdPartyPaymentDll;

namespace VAGastronomistMobileApp.WebPageDll
{
    /// <summary>
    /// 第三方支付Url签名组装
    /// </summary>
    internal class ThirdPaymentSign
    {
        /// <summary>
        /// 悠先点菜支付ali第三方url
        /// </summary>
        /// <param name="paymentOrderScope">事物</param>
        /// <param name="totalFee">支付金额</param>
        /// <param name="shopName">门店名称</param>
        /// <param name="preOrder19DianId">点单编号</param>
        /// <param name="customerId">用户编号</param>
        /// <param name="payMode">支付方式</param>
        /// <param name="type"></param>
        /// <returns></returns>
        internal dynamic AliSignPackage(TransactionScope paymentOrderScope, double totalFee, string shopName, long preOrder19DianId, long customerId,
            int payMode, VAPayOrderType type)
        {
            AlipayOperate alipayOpe = new AlipayOperate();
            AlipayOrderInfo alipayOrder = new AlipayOrderInfo();
            string callBackUrl = WebConfig.ServerDomain + Config.Call_back_url;
            alipayOrder.orderCreatTime = System.DateTime.Now;
            alipayOrder.orderStatus = VAAlipayOrderStatus.NOT_PAID;
            totalFee = Common.ToDouble(totalFee);
            alipayOrder.totalFee = totalFee;
            string subject = Common.GetEnumDescription(type).Replace("{0}", shopName);
            if (subject.Length > 128)
            {
                subject = subject.Substring(0, 128);
            }
            alipayOrder.subject = subject;
            alipayOrder.conn19dianOrderType = type;
            alipayOrder.connId = preOrder19DianId;
            alipayOrder.customerId = customerId;
            long outTradeNo = alipayOpe.AddAlipayOrder(alipayOrder);
            string alipayOrderUrl = string.Empty;
            if (outTradeNo > 0)
            {
                string strOutTradeNo = WebConfig.WechatPrefix + outTradeNo;
                paymentOrderScope.Complete();
                //组装待签名数据
                string alipayOrderString = "partner=" + "\"" + Config.Partner + "\"";
                alipayOrderString += "&";
                alipayOrderString += "seller=" + "\"" + Config.Partner + "\"";
                alipayOrderString += "&";
                alipayOrderString += "out_trade_no=" + "\"" + strOutTradeNo + "\"";
                alipayOrderString += "&";
                alipayOrderString += "subject=" + "\"" + subject + "\"";
                alipayOrderString += "&";
                alipayOrderString += "body=" + "\"" + "点单" + "\"";
                alipayOrderString += "&";
                alipayOrderString += "total_fee=" + "\"" + totalFee + "\"";
                alipayOrderString += "&";
                string notifyURL = WebConfig.ServerDomain + Config.QuickNotify_url;
                alipayOrderString += "notify_url=" + "\"" + notifyURL + "\"";
                string orderSign = RSAFromPkcs8.sign(alipayOrderString, Config.PrivateKey, Config.Input_charset_UTF8);
                alipayOrderString += "&";
                alipayOrderString += "sign=" + "\"" +
                                     HttpUtility.UrlEncode(orderSign,
                                         Encoding.GetEncoding(Config.Input_charset_UTF8)) + "\"";
                alipayOrderString += "&";
                alipayOrderString += "sign_type=\"RSA\"";
                alipayOrderUrl = HttpUtility.UrlEncode(alipayOrderString, Encoding.GetEncoding(Config.Input_charset_UTF8));
            }
            return new { value = alipayOrderUrl, status = VAResult.VA_FAILED_MONEYREMAINED_NOT_ENOUGH };
        }

        /// <summary>
        /// 悠先点菜wechat第三方签名
        /// </summary>
        /// <param name="paymentOrderScope">事物</param>
        /// <param name="totalFee">支付金额</param>
        /// <param name="shopName">门店名称</param>
        /// <param name="preOrder19DianId">点单编号</param>
        /// <param name="customerId">用户编号</param>
        /// <param name="payMode">支付方式</param>
        /// <param name="type"></param>
        /// <returns></returns>
        internal dynamic WechatSignPackage(TransactionScope paymentOrderScope, double totalFee, string shopName, long preOrder19DianId, long customerId,
            int payMode, VAPayOrderType type)
        {
            //首次：获取AccessToken，保存预支付点单至DB，获取PrePayId并更新预支付点单，返回客户端数据
            //1.先获取AccessToken
            WechatAccessTokenOperate _accessTokenOperate = new WechatAccessTokenOperate();//BLL
            string access_token = _accessTokenOperate.CheckExistsAccessTokenIsExpire();
            VAClientWechatPay weChatPay = new VAClientWechatPay();
            VAResult resultMsg = VAResult.VA_FAILED_MONEYREMAINED_NOT_ENOUGH;
            totalFee = Common.ToDouble(totalFee);
            if (!string.IsNullOrEmpty(access_token))//获取到AccessToken
            {
                //2再组装预支付订单，调用接口获取prepayId
                WechatPayOperate _wechatOperate = new WechatPayOperate();//BLL
                WechatPayOrderInfo wechatOrder = new WechatPayOrderInfo();//Model
                //获取预支付订单的Model
                wechatOrder = _wechatOperate.GetWechatPayOrderModel(customerId, preOrder19DianId, totalFee, type, shopName);
                //2.1 先将预支付订单保存至DB
                long wechatOutTradeNo = _wechatOperate.AddWechatPayOrder(wechatOrder);
                if (wechatOutTradeNo > 0)
                {
                    //2.2 向微信索取相应的prePayId，同时记录随即串和时间戳，用于调起微信支付
                    object[] objResult = _wechatOperate.CreatePrePay(access_token, wechatOutTradeNo.ToString(), (totalFee * 100).ToString(), type, shopName);
                    string prePayId = objResult[0].ToString();
                    string noncestr = objResult[1].ToString();
                    string timestamp = objResult[2].ToString();
                    string errcode = objResult[3].ToString();
                    if (!string.IsNullOrEmpty(prePayId))//获取到prePayId                                                                            
                    {
                        //3.更新订单prePayId信息
                        bool updatePrePayId = _wechatOperate.UpdateWechatPrePayId(wechatOutTradeNo, prePayId);
                        if (updatePrePayId)
                        {
                            paymentOrderScope.Complete();
                            //4.最后返回给客户端调用支付接口所需信息
                            weChatPay = _wechatOperate.GetClientWechatPay(prePayId, noncestr, timestamp);
                        }
                        else
                        {
                            resultMsg = VAResult.VA_FAILED_WECHATPAY_UPDATE_PREPAYID;//更新微信支付预点单的prepayId失败
                        }
                    }
                    else
                    {
                        //如果没有获取到prepayId，则去检查首次获取AccessToken的返回结果，若是AccessToken已过期，则重新走一遍流程
                        //第二次：直接调接口获取AccessToken，获取PrePayId并更新预支付点单，返回客户端数据
                        //if (errcode == "42001")//AccessToken过期，目前微信的返回信息：{"errcode":42001,"errmsg":"access_token expired"}
                        //获取prepayId正常的格式：{"prepayid":"1101000000140828d29f022025fcce9b","errcode":0,"errmsg":"Success"}
                        //若返回结果不为0，则从微信方重新获取access_token
                        if (errcode != "0")
                        {
                            //重新获取
                            access_token = _accessTokenOperate.GetAccessToken();
                            if (!string.IsNullOrEmpty(access_token))
                            {
                                //获取PrePayId
                                object[] objResultAgain = _wechatOperate.CreatePrePay(access_token, wechatOutTradeNo.ToString(), (totalFee * 100).ToString(), type, shopName);
                                prePayId = objResultAgain[0].ToString();
                                noncestr = objResultAgain[1].ToString();
                                timestamp = objResultAgain[2].ToString();

                                if (!string.IsNullOrEmpty(prePayId))//第二次成功获取到了prePayId                                                                            
                                {
                                    //3.更新订单prePayId信息
                                    bool updatePrePayId = _wechatOperate.UpdateWechatPrePayId(wechatOutTradeNo, prePayId);

                                    if (updatePrePayId)
                                    {
                                        paymentOrderScope.Complete();
                                        //4.最后返回给客户端调用支付接口所需信息
                                        weChatPay = _wechatOperate.GetClientWechatPay(prePayId, noncestr, timestamp);
                                    }
                                    else
                                    {
                                        resultMsg = VAResult.VA_FAILED_WECHATPAY_UPDATE_PREPAYID;//更新微信支付预点单的prepayId失败
                                    }
                                }
                                else
                                {
                                    resultMsg = VAResult.VA_FAILED_WECHATPAY_NOTFOUND_PREPAYID;
                                }
                            }
                            else
                            {
                                resultMsg = VAResult.VA_FAILED_WECHATPAY_NOTFOUND_ACCESSTOKEN;//调用微信支付接口未获取到AccessToken
                            }
                        }
                        else
                        {
                            resultMsg = VAResult.VA_FAILED_WECHATPAY_NOTFOUND_PREPAYID;
                        }
                    }
                }
                else
                {
                    resultMsg = VAResult.VA_FAILED_DB_ERROR; //保存预点单信息出错
                }
            }
            else
            {
                resultMsg = VAResult.VA_FAILED_WECHATPAY_NOTFOUND_ACCESSTOKEN;//调用微信支付接口未获取到AccessToken
            }
            return new { value = weChatPay, status = resultMsg };
        }

        /// <summary>
        /// 悠先点菜支付union第三方签名
        /// </summary>
        /// <param name="preOrderOper">上下文操作类</param>
        /// <param name="paymentOrderScope">事物</param>
        /// <param name="totalFee">支付金额</param>
        /// <param name="shopName">门店名称</param>
        /// <param name="preOrder19DianId">点单编号</param>
        /// <param name="customerId">用户编号</param>
        /// <param name="payMode">支付方式</param>
        /// <param name="type"></param>
        /// <returns></returns>
        internal dynamic UnionSignPackage(PreOrder19dianOperate preOrderOper, TransactionScope paymentOrderScope, double totalFee, string shopName, long preOrder19DianId, long customerId,
            int payMode, VAPayOrderType type)
        {
            UnionPayOperate operate = new UnionPayOperate();
            UnionPayInfo orderInfo = new UnionPayInfo();
            orderInfo.merchantOrderTime = System.DateTime.Now;
            orderInfo.orderStatus = VAAlipayOrderStatus.NOT_PAID;
            orderInfo.conn19dianOrderType = VAPayOrderType.PAY_PREORDER_AND_RECHARGE;
            orderInfo.merchantOrderAmt = totalFee;
            orderInfo.customerId = customerId;
            string subjectTwo = Common.GetEnumDescription(type).Replace("{0}", shopName);
            if (subjectTwo.Length > 128)
            {
                subjectTwo = subjectTwo.Substring(0, 128);
            }
            orderInfo.merchantOrderDesc = subjectTwo;
            string unionPayOrder = string.Empty;
            VAResult resultMsg = VAResult.VA_FAILED_MONEYREMAINED_NOT_ENOUGH;
            orderInfo.connId = preOrder19DianId;
            long payOrderId = operate.AddUnionpayOrder(orderInfo);
            if (payOrderId > 0)
            {
                ////订单号,  从数据库中获取@@identity 自增长列
                orderInfo.merchantOrderId = UnionPayParameters.UNION_PAY_FRONT_CODE + payOrderId;
                StringBuilder originalInfo = new StringBuilder();
                originalInfo
                    .Append("merchantName=").Append(UnionPayParameters.merchantName)
                    .Append("&merchantId=").Append(UnionPayParameters.merchantId)
                    .Append("&merchantOrderId=").Append(orderInfo.merchantOrderId)
                    .Append("&merchantOrderTime=").Append(orderInfo.merchantOrderTime.ToString("yyyyMMddHHmmss"))
                    .Append("&merchantOrderAmt=").Append(orderInfo.merchantOrderAmt * 100)
                    .Append("&merchantOrderDesc=").Append(orderInfo.merchantOrderDesc)
                    .Append("&transTimeout=").Append(UnionPayParameters.transTimeout);
                string originalsign = originalInfo.ToString();
                string xmlSign = UnionPaySignEncode.CreateSign(originalsign, UnionPayParameters.alias, UnionPayParameters.password, UnionPayParameters.PrivatePath);
                string Submit = preOrderOper.SubmitString(orderInfo, xmlSign);
                String returnContent = UnionPaySender.Send(UnionPayParameters.UNION_PAY_SERVER, Submit);
                string decodeStr = HttpUtility.UrlDecode(returnContent);
                XmlDocument xml = new XmlDocument();
                xml.LoadXml(decodeStr);
                string merchantId = xml.SelectSingleNode("/upomp/merchantId").InnerText;
                string merchantOrderIdNew = xml.SelectSingleNode("/upomp/merchantOrderId").InnerText;
                string merchantOrderTime = xml.SelectSingleNode("/upomp/merchantOrderTime").InnerText;
                string resCode = xml.SelectSingleNode("/upomp/respCode").InnerText;
                string resDesc = xml.SelectSingleNode("/upomp/respDesc").InnerText;
                if (resCode == "0000")
                {
                    StringBuilder builder = new StringBuilder();
                    builder
                        .Append("merchantId=").Append(merchantId)
                        .Append("&merchantOrderId=").Append(merchantOrderIdNew)
                        .Append("&merchantOrderTime=").Append(merchantOrderTime);
                    string threeElement = builder.ToString();
                    string threeElementSign = UnionPaySignEncode.CreateSign(threeElement, UnionPayParameters.alias, UnionPayParameters.password, UnionPayParameters.PrivatePath);
                    //生成xml格式字符串
                    unionPayOrder = "<?xml version=" + "'1.0' "
                        + "encoding=" + "'utf-8' " + "standalone='yes'" + "?>" + "<upomp  application=" + "'LanchPay.Req' " + "version=" + "'1.0.0'" + ">"
                        + "<merchantId>"
                        + merchantId
                        + "</merchantId>"
                        + "<merchantOrderId>"
                        + merchantOrderIdNew
                        + "</merchantOrderId>"
                        + "<merchantOrderTime>"
                        + merchantOrderTime
                        + "</merchantOrderTime>"
                        + "<sign>"
                        + threeElementSign
                        + "</sign>" + "</upomp>";
                }
                else
                {
                    resultMsg = VAResult.VA_FAILED_UNION_PAY_ORDER_SUMMIT_ERROR;
                }
            }
            else
            {
                resultMsg = VAResult.VA_FAILED_DB_ERROR;
            }
            return new { value = unionPayOrder, status = resultMsg };
        }
    }
}
