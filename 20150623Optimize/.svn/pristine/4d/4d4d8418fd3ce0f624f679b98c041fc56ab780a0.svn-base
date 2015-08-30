using System;
using System.IO;
using System.Net;
using System.Web;
using System.Text;
using System.Collections.Generic;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using VAGastronomistMobileApp.Model;
using LogDll;
using VAGastronomistMobileApp.WebPageDll;
/// <summary>
/// 微信登录
/// </summary>
public class WeChatLogin
{
    //正式环境
    private const string appId = "wxcc9c7eed4384a147";
    private const string appSecret = "ca2440a36c3fa776fb61b7188a176234";
    //测试环境
    //private const string appId = "wx3f9ff3043cbcb3b7";
    //private const string appSecret = "c685e2673928ee65bdc04205f0b01396";
    /// <summary>
    /// 按code返回令牌
    /// </summary>
    /// <param name="code">code</param>
    /// <returns></returns>
    public static Tuple<string, string> GetCodeOfAccessToken(string code)
    {
        string url = string.Format("https://api.weixin.qq.com/sns/oauth2/access_token?appid={0}&secret={1}&code={2}&grant_type=authorization_code", appId, appSecret, code);
        string rsStr = GetWebContent(url);
        if (string.IsNullOrEmpty(rsStr))
            return null;

        JObject rsJson = JObject.Parse(rsStr);
        if (rsJson["errcode"] != null)
        {
            LogManager.WriteLog(LogFile.Error, string.Format("错误码:{0},确定信息{1}", GetJsonStr(rsJson["errcode"].ToString()), GetJsonStr(rsJson["errmsg"].ToString())));
            return null;
        }

        WeChatAuthorization model = new WeChatAuthorization();
        model.Id = CreateCombGuid();
        model.OpenId = GetJsonStr(rsJson["openid"].ToString());
        model.AccessToken = GetJsonStr(rsJson["access_token"].ToString());
        model.ExpiresIn = Convert.ToInt32(rsJson["expires_in"].ToString());
        model.RefreshToken = GetJsonStr(rsJson["refresh_token"].ToString());
        model.Scope = GetJsonStr(rsJson["scope"].ToString());
        model.ModifyUser = "System";
        model.ModifyIP = IPAddress;

        var weChatBll = new WeChatAuthorizationOperate();
        var weModel = weChatBll.GetOpenIdOfModel(model.OpenId);
        int s = 0;
        if (weModel != null)
        {
            model.Id = weModel.Id;
            s = weChatBll.Update(model);
        }
        else
            s = weChatBll.Insert(model);
        return new Tuple<string, string>(model.AccessToken, model.OpenId);
    }

    /// <summary>
    /// 刷新令牌
    /// </summary>
    /// <param name="refreshToken">refreshToken</param>
    /// <returns></returns>
    public static Tuple<string, string> GetRefreshTokenOfAccessToken(string refreshToken)
    {
        string url = string.Format("https://api.weixin.qq.com/sns/oauth2/refresh_token?appid={0}&grant_type=refresh_token&refresh_token={1}", appId, refreshToken);
        string rsStr = GetWebContent(url);
        if (string.IsNullOrEmpty(rsStr))
            return null;

        JObject rsJson = JObject.Parse(rsStr);
        if (rsJson["errcode"] != null)
        {
            LogManager.WriteLog(LogFile.Error, string.Format("错误码:{0},确定信息{1}", GetJsonStr(rsJson["errcode"].ToString()), GetJsonStr(rsJson["errmsg"].ToString())));
            return null;
        }

        WeChatAuthorization model = new WeChatAuthorization();
        model.OpenId = GetJsonStr(rsJson["openid"].ToString());
        model.AccessToken = GetJsonStr(rsJson["access_token"].ToString());
        model.ExpiresIn = Convert.ToInt32(rsJson["expires_in"].ToString());
        model.RefreshToken = GetJsonStr(rsJson["refresh_token"].ToString());
        model.Scope = GetJsonStr(rsJson["scope"].ToString());
        model.ModifyUser = "System";
        model.ModifyIP = IPAddress;
        int s = new WeChatAuthorizationOperate().UpdateOfOpenId(model);
        return new Tuple<string, string>(model.AccessToken, model.OpenId);
    }

    /// <summary>
    /// 验证是否存在
    /// </summary>
    /// <param name="accessToken">令牌</param>
    /// <param name="openId">用户OpenId</param>
    /// <returns></returns>
    public static int Auth(string accessToken, string openId)
    {
        string url = string.Format("https://api.weixin.qq.com/sns/auth?access_token={0}&openid={1}", accessToken, openId);
        string rsStr = GetWebContent(url);
        if (string.IsNullOrEmpty(rsStr))
            return 1;

        JObject rsJson = JObject.Parse(rsStr);
        if (Convert.ToInt32(rsJson["errcode"]) != 0)
        {
            LogManager.WriteLog(LogFile.Error, string.Format("错误码:{0},确定信息{1}", GetJsonStr(rsJson["errcode"].ToString()), GetJsonStr(rsJson["errmsg"].ToString())));
            return 1;
        }
        return 0;
    }

    /// <summary>
    /// 添加微信用户
    /// </summary>
    /// <param name="accessToken">令牌</param>
    /// <param name="openId">用户Openid</param>
    /// <returns></returns>
    public static Tuple<int, string> AddWeChatUser(string accessToken, string openId)
    {
        string url = string.Format("https://api.weixin.qq.com/sns/userinfo?access_token={0}&openid={1}", accessToken, openId);
        string rsStr = GetWebContent(url);
        JObject rsJson = JObject.Parse(rsStr);
        if (string.IsNullOrEmpty(rsStr))
            return new Tuple<int, string>(-1, null);

        if (rsJson["errcode"] != null)
        {
            LogManager.WriteLog(LogFile.Error, string.Format("错误码:{0},确定信息{1}", GetJsonStr(rsJson["errcode"].ToString()), GetJsonStr(rsJson["errmsg"].ToString())));
            return new Tuple<int, string>(-1, null);
        }

        string strHeadImgUrl = null;
        string headImgUrl = "";
        int? headImgSize = 0;
        if (rsJson["headimgurl"] != null && !string.IsNullOrEmpty(rsJson["headimgurl"].ToString()))
        {
            strHeadImgUrl = GetJsonStr(rsJson["headimgurl"].ToString());
            if (!string.IsNullOrEmpty(strHeadImgUrl))
            {
                headImgUrl = strHeadImgUrl;
                headImgSize = int.Parse(strHeadImgUrl.Substring(strHeadImgUrl.LastIndexOf('/') + 1));
            }
        }

        WeChatUser model = new WeChatUser();
        model.Id = CreateCombGuid();
        model.OpenId = GetJsonStr(rsJson["openid"].ToString());
        if (rsJson["unionid"] != null)
            model.UnionId = GetJsonStr(rsJson["unionid"].ToString());
        else
            model.UnionId = model.OpenId;

        if (rsJson["nickname"] != null)
            model.NickName = GetJsonStr(rsJson["nickname"].ToString());
        else
            model.NickName = "";
        if (rsJson["sex"] != null)
            model.Sex = Convert.ToInt32(GetJsonStr(rsJson["sex"].ToString()));
        else
            model.Sex = 0;
        if (rsJson["province"] != null)
            model.Province = GetJsonStr(rsJson["province"].ToString());
        else
            model.Province = "";
        if (rsJson["city"] != null)
            model.City = GetJsonStr(rsJson["city"].ToString());
        else
            model.City = "";
        if (rsJson["country"] != null)
            model.Country = GetJsonStr(rsJson["country"].ToString());
        else
            model.Country = "";

        model.HeadImgUrl = headImgUrl;
        model.HeadImgSize = headImgSize;
        model.ModifyUser = "System";
        model.ModifyIP = IPAddress;

        IList<WeChatUserPrivilege> privilegeList = new List<WeChatUserPrivilege>();
        if (rsJson["privilege"] != null)
        {
            foreach (var item in rsJson["privilege"])
            {
                privilegeList.Add(new WeChatUserPrivilege
                {
                    Id = CreateCombGuid(),
                    WeChatUser_Id = model.Id,
                    Privilege = item.ToString()
                });
            }
        }

        var weChatDal = new WeChatUserOperator();
        var weChatModel = weChatDal.GetUnionIdOfModel(model.UnionId);
        int s = 0;
        if (weChatModel == null)
        {
            s = new WeChatUserOperator().Insert(model, privilegeList);
        }
        else
        {
            model.Id = weChatModel.Id;
            model.CustomerInfo_CustomerID = weChatModel.CustomerInfo_CustomerID;
            s = new WeChatUserOperator().Update(model, privilegeList);
        }

        return new Tuple<int, string>(s, model.Id.ToString());
    }

    /// <summary>
    /// 取数据
    /// </summary>
    /// <param name="url">url地址</param>
    /// <returns></returns>
    private static string GetWebContent(string url)
    {
        string strResult = null;
        try
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);//声明一个HttpWebRequest请求 
            request.Timeout = 30000;//设置连接超时时间 
            request.Headers.Set("Pragma", "no-cache");
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream streamReceive = response.GetResponseStream();
            //Encoding encoding = Encoding.GetEncoding("GB2312");
            Encoding encoding = Encoding.UTF8;
            StreamReader streamReader = new StreamReader(streamReceive, encoding);
            strResult = streamReader.ReadToEnd();
        }
        catch (Exception e)
        {
            LogManager.WriteLog(LogFile.Error, e.Message);
        }
        return strResult;
    }

    /// <summary>
    /// 以顺序guid
    /// </summary>
    /// <returns></returns>
    public static Guid CreateCombGuid()
    {
        byte[] guidArray = Guid.NewGuid().ToByteArray();

        DateTime baseDate = new DateTime(1900, 1, 1);
        DateTime now = DateTime.Now;

        //Get the days and milliseconds which will be used to build the byte string 
        TimeSpan days = new TimeSpan(now.Ticks - baseDate.Ticks);
        TimeSpan msecs = now.TimeOfDay;

        //Convert to a byte array 
        //Note that SQL Server is accurate to 1/300th of a millisecond so we divide by 3.333333 
        byte[] daysArray = BitConverter.GetBytes(days.Days);
        byte[] msecsArray = BitConverter.GetBytes((long)(msecs.TotalMilliseconds / 3.333333));

        //Reverse the bytes to match SQL Servers ordering 
        Array.Reverse(daysArray);
        Array.Reverse(msecsArray);

        //Copy the bytes into the guid 
        Array.Copy(daysArray, daysArray.Length - 2, guidArray, guidArray.Length - 6, 2);
        Array.Copy(msecsArray, msecsArray.Length - 4, guidArray, guidArray.Length - 4, 4);

        return new Guid(guidArray);
    }

    /// <summary>
    /// 去掉json的双引号
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    private static string GetJsonStr(string str)
    {
        if (str.IndexOf("\"") != 0)
            return str;
        return str.Substring(1, str.Length - 2);
    }

    /// <summary>
    /// 穿过代理服务器取远程用户真实IP地址
    /// </summary>
    /// <returns></returns>
    public static string IPAddress
    {
        get
        {
            if (HttpContext.Current.Request.ServerVariables["HTTP_VIA"] != null)
                return HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
            else
                return HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
        }
    }
}