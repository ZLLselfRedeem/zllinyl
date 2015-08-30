using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.SQLServerDAL;
using VAGastronomistMobileApp.TheThirdPartyPaymentDll;
using VAGastronomistMobileApp.Model;
using System.Data;

namespace VAGastronomistMobileApp.WebPageDll
{
    public class WechatAccessTokenOperate
    {
        /// <summary>
        /// 将获取到的AccessToken信息保存至DB
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="createTime"></param>
        /// <param name="expireTime"></param>
        /// <returns></returns>
        public bool InsertWechatAccessToken(string accessToken, DateTime createTime, DateTime expireTime)
        {
            WechatAccessTokenManager _manager = new WechatAccessTokenManager();
            long id = _manager.InsertWechatAccessToken(accessToken, createTime, expireTime);
            if (id > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 将最新获取的AccessToken更新至DB
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="createTime"></param>
        /// <param name="expireTime"></param>
        /// <returns></returns>
        public bool UpdateWechatAccessToken(string accessToken, DateTime createTime, DateTime expireTime, VAAccessTokenType accessTokenType)
        {
            WechatAccessTokenManager _manager = new WechatAccessTokenManager();
            return _manager.UpdateWechatAccessToken(accessToken, createTime, expireTime, accessTokenType);
        }

        /// <summary>
        /// 检查数据库中最新的AccessToken是否过期,如未过期返回有效AccessToken，过期则调用接口重新获取
        /// </summary>
        /// <returns>True未过期</returns>
        public string CheckExistsAccessTokenIsExpire()
        {
            WechatAccessTokenManager _manager = new WechatAccessTokenManager();
            DataTable dt = _manager.QueryNewestAccessToken(VAAccessTokenType.wechatPay);
            if (dt != null && dt.Rows.Count > 0)
            {
                DateTime createtime = Common.ToDateTime(dt.Rows[0]["createtime"]);
                DateTime expireTime = Common.ToDateTime(dt.Rows[0]["expireTime"]);

                //若上次获取时间是昨天，到期时间是今天，即使DB中存的还没有过期，也重新取【createtime 2014-03-24 23:59:29 expireTime 2014-03-25 01:49:29】
                if (Common.ToDateTime(createtime.ToString("yyyy-MM-dd")) < Common.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd")))
                {
                    return GetAccessToken();
                }
                else
                {
                    if (expireTime < DateTime.Now)//已经过期，重新获取AccessToken
                    {
                        return GetAccessToken();
                    }
                    else//未过期直接返回有效的AccessToken
                    {
                        return dt.Rows[0]["accessToken"].ToString();
                    }
                }
            }
            else
            {
                return GetAccessToken();
            }
        }

        /// <summary>
        /// 调用微信接口获取AccessToken，并保存至数据库
        /// </summary>
        /// <returns></returns>
        public string GetAccessToken()
        {
            string appId = WechatPayConfig.AppID;
            string appSecret = WechatPayConfig.AppSecret;

            string url = string.Format("https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}", appId, appSecret);
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
                    bool update = _accessTokenOperate.UpdateWechatAccessToken(access_token, createTime, expireTime, VAAccessTokenType.wechatPay);
                }
                return access_token;
            }
            else
            {
                return "";
            }
        }
    }
}
