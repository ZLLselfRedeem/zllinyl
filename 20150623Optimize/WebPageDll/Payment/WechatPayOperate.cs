using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL;
using VAGastronomistMobileApp.TheThirdPartyPaymentDll;
using System.Web;
using System.Data;
using System.Configuration;

namespace VAGastronomistMobileApp.WebPageDll
{
    /// <summary>
    /// 微信支付数据处理层
    /// </summary>
    public class WechatPayOperate
    {
        private readonly WechatPayManager _wechatMan = new WechatPayManager();

        /// <summary>
        /// 新增微信支付订单信息
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public long AddWechatPayOrder(WechatPayOrderInfo order)
        {
            long wechatPayOrderId = _wechatMan.InsertWechatPayOrder(order);
            if (wechatPayOrderId > 0)
            {
                return wechatPayOrderId;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 根据outTradeNo更新prepayid
        /// </summary>
        /// <param name="outTradeNo">我方产生的订单编号</param>
        /// <param name="PrePayId">微信返回的相应的订单编号</param>
        /// <returns></returns>
        public bool UpdateWechatPrePayId(long outTradeNo, string PrePayId)
        {
            int i = _wechatMan.UpdateWechatPrePayId(outTradeNo, PrePayId);
            if (i > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 根据通知结果更新微信支付订单信息
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public bool UpdateWechatOrder(WechatPayOrderInfo order)
        {
            int i = _wechatMan.UpdateWechatOrder(order);
            if (i > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 更新第三方订单的状态
        /// </summary>
        /// <param name="outTradeNo"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public bool UpdateOrderStatus(string outTradeNo, VAAlipayOrderStatus status)
        {
            return _wechatMan.UpdateOrderStatus(outTradeNo, status);
        }

        /// <summary>
        /// 根据outTradeNo查询微信支付订单信息
        /// </summary>
        /// <param name="outTradeNo"></param>
        /// <returns></returns>
        public DataTable QueryWechatPayOrder(string outTradeNo)
        {
            return _wechatMan.QueryWechatPayOrder(outTradeNo);
        }

        /// <summary>
        /// 组装微信预支付订单的Model
        /// </summary>
        /// <param name="customerId">客户ID</param>
        /// <param name="preOrder19dianId">点单ID</param>
        /// <param name="totalFee">支付金额</param>
        /// <returns></returns>
        public WechatPayOrderInfo GetWechatPayOrderModel(long customerId, long preOrder19dianId, double totalFee, VAPayOrderType payOrderType, string shopName)
        {
            WechatPayOrderInfo wechatOrder = new WechatPayOrderInfo();
            wechatOrder.totalFee = Common.ToDouble(totalFee);
            wechatOrder.orderCreateTime = DateTime.Now;
            wechatOrder.orderStatus = VAAlipayOrderStatus.NOT_PAID;//公用状态
            wechatOrder.conn19dianOrderType = payOrderType;
            wechatOrder.connId = preOrder19dianId;
            string body = Common.GetEnumDescription(payOrderType).Replace("{0}", shopName);
            if (body.Length > 128)
            {
                body = body.Substring(0, 128);
            }
            wechatOrder.body = body;
            wechatOrder.customerId = customerId;
            return wechatOrder;
        }

        /// <summary>
        /// 调用微信支付接口，通过AccessToken和PostData(订单信息)获取PrePayId
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="out_trade_no">商户系统内部的订单号,32 个字符内、可包含字母,确保在商户系统唯一</param>
        /// <param name="total_fee">订单总金额，单位为分</param>
        /// <returns></returns>
        public object[] CreatePrePay(string accessToken, string out_trade_no, string total_fee, VAPayOrderType payOrderType, string shopName)
        {
            object[] objResult = new object[4];

            string appId = WechatPayConfig.AppID;
            string traceid = "viewalloc";
            string noncestr = DateTime.Now.ToString("yyyyMMddHHmmss") + WechatPayFunction.BuildRandomStr(10);
            string timestamp = Common.ToSecondFrom1970(DateTime.Now).ToString();
            string app_signature = "";
            string sign_method = WechatPayConfig.signMethod;
            string url = string.Format("https://api.weixin.qq.com/pay/genprepay?access_token={0}", accessToken);

            //Package组装
            //发给微信方的out_trade_no加上前缀区分是正式还是测试，因为out_trade_no要求在微信方要唯一 Add at 2014-4-2
            string prefix = WebConfig.WechatPrefix;
            //if (string.IsNullOrEmpty(prefix))
            //{
            //    prefix = "va";
            //}
            out_trade_no = prefix + out_trade_no;

            string package = GetPackage(out_trade_no, total_fee, payOrderType, shopName);

            //支付签名（paySign）生成方法 app_signature
            app_signature = GetSign(appId, noncestr, timestamp, package).ToLower();

            #region PostData
            StringBuilder strData = new StringBuilder();
            strData.Append("{");
            strData.Append("\"appid\":\"" + appId + "\",");
            strData.Append("\"traceid\":\"" + traceid + "\",");
            strData.Append("\"noncestr\":\"" + noncestr + "\",");
            strData.Append("\"package\":\"" + package + "\",");
            strData.Append("\"timestamp\":\"" + timestamp + "\",");
            strData.Append("\"app_signature\":\"" + app_signature + "\",");
            strData.Append("\"sign_method\":\"" + sign_method + "\"");
            strData.Append("}");
            #endregion

            //第二次调用微信支付接口，通过AccessToken和PostData(订单信息)获取PrePayId
            string result = HttpUtilityHelper.SendHttpRequest(url, strData.ToString());

            WechatPrePayResult prePayModel = JsonOperate.JsonDeserialize<WechatPrePayResult>(result);

            string prePayId = prePayModel.prepayid;//预支付订单编号

            if (string.IsNullOrEmpty(prePayId))
            {
                prePayId = "";
            }
            objResult[0] = prePayId;
            objResult[1] = noncestr;
            objResult[2] = timestamp;
            objResult[3] = prePayModel.errcode;

            return objResult;
        }

        /// <summary>
        /// 组装微信预支付订单Package信息
        /// </summary>
        /// <param name="out_trade_no">我方提供给微信的订单编号</param>
        /// <param name="total_fee">订单总金额，单位为分</param>
        /// <returns></returns>
        private static string GetPackage(string out_trade_no, string total_fee, VAPayOrderType payOrderType, string shopName)
        {
            string bank_type = WechatPayConfig.bank_type;
            string body = Common.GetEnumDescription(payOrderType).Replace("{0}", shopName); //商品描述
            string fee_type = WechatPayConfig.fee_type;
            string input_charset = WechatPayConfig.input_charset;
            string notify_url = WebConfig.ServerDomain + WechatPayConfig.notifyURL;
            string partner = WechatPayConfig.partnerId;
            string spbill_create_ip = WechatPayConfig.spbill_create_ip;

            //a.对所有传入参数按照字段名的 ASCII 码从小到大排序（字典序）后，使用 URL 键值对的格式（即 key1=value1&key2=value2…）拼接成字符串 string1
            string string1 = "bank_type=" + bank_type + "&body=" + body + "&fee_type=" + fee_type + "&input_charset=" + input_charset + "&notify_url=" + notify_url + "&out_trade_no=" + out_trade_no + "&partner=" + partner + "&spbill_create_ip=" + spbill_create_ip + "&total_fee=" + total_fee + "";

            //b.在 string1 最 后 拼 接 上 key=paternerKey 得 到 stringSignTemp 字 符 串 ， 并 对
            //stringSignTemp 进行 md5 运算，再将得到的字符串所有字符转换为大写，得到 sign 值signValue。
            string paternerKey = WechatPayConfig.partnerKey;

            string stringSignTemp = string1 + "&key=" + paternerKey + "";
            string sign = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(stringSignTemp, "MD5");

            //c.对 string1 中的所有键值对中的 value 进行 urlencode 转码， 按照 a 步骤重新拼接成字符
            //串，得到 string2。对于 js 前端程序，一定要使用函数 encodeURIComponent 进行 urlencode
            //编码（注意！进行 urlencode 时要将空格转化为%20 而不是+） 。  

            bank_type = HttpUtility.UrlEncode(bank_type);
            body = HttpUtility.UrlEncode(body);
            fee_type = HttpUtility.UrlEncode(fee_type);
            input_charset = HttpUtility.UrlEncode(input_charset);
            notify_url = HttpUtility.UrlEncode(notify_url);
            out_trade_no = HttpUtility.UrlEncode(out_trade_no);
            partner = HttpUtility.UrlEncode(partner);
            spbill_create_ip = HttpUtility.UrlEncode(spbill_create_ip);
            total_fee = HttpUtility.UrlEncode(total_fee);

            string string2 = "bank_type=" + bank_type + "&body=" + body + "&fee_type=" + fee_type + "&input_charset=" + input_charset + "&notify_url=" + notify_url + "&out_trade_no=" + out_trade_no + "&partner=" + partner + "&spbill_create_ip=" + spbill_create_ip + "&total_fee=" + total_fee + "";

            //d.将 sign=signValue 拼接到 string2 后面得到最终的 package 字符串。
            string package = string2 + "&sign=" + sign + "";
            return package;
        }

        /// <summary>
        /// 组装微信预支付订单签名信息
        /// </summary>
        /// <param name="appId">应用id</param>
        /// <param name="noncestr">随机串</param>
        /// <param name="timestamp">时间戳</param>
        /// <param name="package">微信预支付订单Package信息</param>
        /// <returns></returns>
        private static string GetSign(string appId, string noncestr, string timestamp, string package)
        {
            //参不 paySign 签名的字段包括：appid、timestamp、noncestr、package 以及 appkey（即paySignkey）.........还有 traceid

            string appkey = WechatPayConfig.PaySignKey;
            string traceid = "viewalloc";

            //a.对所有待签名参数按照字段名的 ASCII 码从小到大排序（字典序）后，使用 URL 键值
            //对的格式（即 key1=value1&key2=value2…）拼接成字符串 string1。这里需要注意的是所有参数名均为小写字符

            string string11 = "appid=" + appId + "&appkey=" + appkey + "&noncestr=" + noncestr + "&package=" + package + "&timestamp=" + timestamp + "&traceid=" + traceid;

            //b.对 string1 作签名算法，字段名和字段值都采用原始值（此时 package 的 value 就对应了
            //使用 2.6 中描述的方式生成的 package） ，不进行 URL 转义。具体签名算法为 paySign =SHA1(string)。     

            string app_signature = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(string11, "SHA1");
            return app_signature;
        }

        /// <summary>
        /// 组装提供给客户端的待支付信息
        /// </summary>
        /// <param name="prepayId">微信返回的预支付订单编号</param>
        /// <param name="noncestr">随即串</param>
        /// <param name="timestamp">时间戳</param>
        /// <returns></returns>
        public VAClientWechatPay GetClientWechatPay(string prepayId, string noncestr, string timestamp)
        {
            VAClientWechatPay pay = new VAClientWechatPay();

            pay.appid = WechatPayConfig.AppID;
            pay.partnerId = WechatPayConfig.partnerId;
            pay.prepayId = prepayId;
            pay.packageValue = WechatPayConfig.payPackage;
            pay.nonceStr = noncestr;
            pay.timeStamp = timestamp;
            pay.sign = GetPaySign(noncestr, timestamp, prepayId);

            return pay;
        }

        /// <summary>
        ///  调起微信支付的签名
        /// </summary>
        /// <param name="noncestr">随即串（同预支付）</param>
        /// <param name="timestamp">时间戳（同预支付）</param>
        /// <param name="prepayid">微信返回的预支付订单编号</param>
        /// <returns></returns>
        private static string GetPaySign(string noncestr, string timestamp, string prepayid)
        {
            string appId = WechatPayConfig.AppID;
            string appkey = WechatPayConfig.PaySignKey;
            string partnerid = WechatPayConfig.partnerId;
            string package = WechatPayConfig.payPackage;

            //a.对所有待签名参数按照字段名的 ASCII 码从小到大排序（字典序）后，使用 URL 键值
            //对的格式（即 key1=value1&key2=value2…）拼接成字符串 string1。这里需要注意的是所有参数名均为小写字符

            string string11 = "appid=" + appId + "&appkey=" + appkey + "&noncestr=" + noncestr + "&package=" + package + "&partnerid=" + partnerid + "&prepayid=" + prepayid + "&timestamp=" + timestamp;

            //b.对 string1 作签名算法，字段名和字段值都采用原始值（此时 package 的 value 就对应了
            //使用 2.6 中描述的方式生成的 package） ，不进行 URL 转义。具体签名算法为 paySign =SHA1(string)。     

            string app_signature = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(string11, "SHA1").ToLower();
            return app_signature;
        }
    }
}
