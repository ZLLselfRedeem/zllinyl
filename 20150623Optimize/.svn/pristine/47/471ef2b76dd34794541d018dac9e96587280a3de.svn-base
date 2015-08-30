using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL;
using VAGastronomistMobileApp.TheThirdPartyPaymentDll;

namespace VAGastronomistMobileApp.WebPageDll
{
    /// <summary>
    /// 微信JS-SDK
    /// 2015-1-22
    /// </summary>
    public class WechatGongzhongOperate
    {
        /// <summary>
        /// 检查AccessToken是否过期，未过期则取DB，过期则调用API
        /// </summary>
        /// <param name="accessTokenType"></param>
        /// <returns></returns>
        public string CheckAccessTokenIsExpire(VAAccessTokenType accessTokenType)
        {
            WechatAccessTokenManager _manager = new WechatAccessTokenManager();
            DataTable dt = _manager.QueryNewestAccessToken(accessTokenType);
            if (dt != null && dt.Rows.Count > 0)
            {
                DateTime createtime = Common.ToDateTime(dt.Rows[0]["createtime"]);
                DateTime expireTime = Common.ToDateTime(dt.Rows[0]["expireTime"]);

                //若上次获取时间是昨天，到期时间是今天，即使DB中存的还没有过期，也重新取【createtime 2014-03-24 23:59:29 expireTime 2014-03-25 01:49:29】
                if (Common.ToDateTime(createtime.ToString("yyyy-MM-dd")) < Common.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd")))
                {
                    return GetAccessTokenOfGongzhong();
                }
                else
                {
                    if (expireTime < DateTime.Now)//已经过期，重新获取AccessToken
                    {
                        return GetAccessTokenOfGongzhong();
                    }
                    else//未过期直接返回有效的AccessToken
                    {
                        return dt.Rows[0]["accessToken"].ToString();
                    }
                }
            }
            else
            {
                return GetAccessTokenOfGongzhong();
            }
        }

        /// <summary>
        /// 检查JsapiTicket是否过期，未过期则取DB，过期了则调用API
        /// </summary>
        /// <param name="accessTokenType"></param>
        /// <returns></returns>
        public string CheckJsapiTicketIsExpire(VAAccessTokenType accessTokenType)
        {
            WechatAccessTokenManager _manager = new WechatAccessTokenManager();
            DataTable dt = _manager.QueryNewestAccessToken(accessTokenType);
            if (dt != null && dt.Rows.Count > 0)
            {
                DateTime createtime = Common.ToDateTime(dt.Rows[0]["createtime"]);
                DateTime expireTime = Common.ToDateTime(dt.Rows[0]["expireTime"]);

                //若上次获取时间是昨天，到期时间是今天，即使DB中存的还没有过期，也重新取【createtime 2014-03-24 23:59:29 expireTime 2014-03-25 01:49:29】
                if (Common.ToDateTime(createtime.ToString("yyyy-MM-dd")) < Common.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd")))
                {
                    return GetJsapi_ticket();
                }
                else
                {
                    if (expireTime < DateTime.Now)//已经过期，重新获取AccessToken
                    {
                        return GetJsapi_ticket();
                    }
                    else//未过期直接返回有效的AccessToken
                    {
                        return dt.Rows[0]["accessToken"].ToString();
                    }
                }
            }
            else
            {
                return GetJsapi_ticket();
            }
        }


        /// <summary>
        /// 获取微信公众号的AccessToken
        /// </summary>
        /// <returns></returns>
        public string GetAccessTokenOfGongzhong()
        {
            string appId = WechatGongzhongConfig.appid;
            string secret = WechatGongzhongConfig.secret;

            string url = string.Format("https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}", appId, secret);
            string result = HttpUtilityHelper.GetData(url);
            if (!string.IsNullOrEmpty(result))
            {
                WechatAccessToken accessTokenModel = JsonOperate.JsonDeserialize<WechatAccessToken>(result);

                string access_token = accessTokenModel.access_token;
                string expiresln = accessTokenModel.expires_in;

                DateTime createTime = DateTime.Now;
                DateTime expireTime = createTime.AddSeconds(Convert.ToDouble(expiresln)).AddSeconds(-3600);//1小时

                if (!string.IsNullOrEmpty(access_token))
                {
                    WechatAccessTokenOperate _accessTokenOperate = new WechatAccessTokenOperate();//BLL
                    bool update = _accessTokenOperate.UpdateWechatAccessToken(access_token, createTime, expireTime, VAAccessTokenType.wechatGongzhong);
                }
                return access_token;
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 获取公众号用于调用微信JS接口的临时票据
        /// </summary>
        /// <returns></returns>
        public string GetJsapi_ticket()
        {
            //string accessToken = GetAccessTokenOfGongzhong();
            string accessToken = CheckAccessTokenIsExpire(VAAccessTokenType.wechatGongzhong);

            string url = string.Format("https://api.weixin.qq.com/cgi-bin/ticket/getticket?access_token={0}&type=jsapi", accessToken);

            string result = HttpUtilityHelper.GetData(url);
            if (!string.IsNullOrEmpty(result))
            {
                WechatJsApiTicket jsapiTicket = JsonOperate.JsonDeserialize<WechatJsApiTicket>(result);
                if (jsapiTicket != null)
                {
                    string ticket = jsapiTicket.ticket;
                    string expiresIn = jsapiTicket.expires_in;

                    DateTime createTime = DateTime.Now;
                    DateTime expireTime = createTime.AddSeconds(Convert.ToDouble(expiresIn)).AddSeconds(-3600);//只记录一个小时

                    if (!string.IsNullOrEmpty(ticket))
                    {
                        WechatAccessTokenOperate _accessTokenOperate = new WechatAccessTokenOperate();//BLL
                        bool update = _accessTokenOperate.UpdateWechatAccessToken(ticket, createTime, expireTime, VAAccessTokenType.jsApiTicket);
                    }
                    return ticket;
                }
                else
                {
                    return "";
                }
            }
            else
            {
                return "";
            }
        }

        public WechatJsApiConfig GetWechatJsApiConfig(string url)
        {
            //参与签名的字段包括noncestr（随机字符串）, 有效的jsapi_ticket, timestamp（时间戳）, url（当前网页的URL，不包含#及其后面部分）
            string noncestr = DateTime.Now.ToString("yyyyMMddHHmmss") + WechatPayFunction.BuildRandomStr(10);
            //string jsapi_ticket = GetJsapi_ticket();
            string jsapi_ticket = CheckJsapiTicketIsExpire(VAAccessTokenType.jsApiTicket);

            string timestamp = Common.ToSecondFrom1970(DateTime.Now).ToString();

            //对所有待签名参数按照字段名的ASCII 码从小到大排序（字典序）后，使用URL键值对的格式（即key1=value1&key2=value2…）拼接成字符串string1
            //这里需要注意的是所有参数名均为小写字符

            string string1 = "jsapi_ticket=" + jsapi_ticket + "&noncestr=" + noncestr + "&timestamp=" + timestamp + "&url=" + url;

            //对string1作sha1加密，字段名和字段值都采用原始值，不进行URL 转义  
            //signature=sha1(string1)

            string signature = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(string1, "SHA1").ToLower();

            WechatJsApiConfig config = new WechatJsApiConfig()
            {
                appId = WechatGongzhongConfig.appid,
                nonceStr = noncestr,
                timestamp = timestamp,
                signature = signature
            };
            return config;
        }
    }
}

