<%@ WebHandler Language="C#" Class="TreasureChestHandler" %>

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Transactions;
using System.Web;
using Autofac;
using Autofac.Integration.Web;
using LogDll;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL.Persistence;
using VAGastronomistMobileApp.WebPageDll;
using VAGastronomistMobileApp.WebPageDll.Services.Infrastructure;
using VA.Cache.HttpRuntime;
using VA.CacheLogic.OrderClient;
using VAEncryptDecrypt;

public class TreasureChestHandler : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    public IRepositoryContext RepositoryContext { get; set; }

    public void ProcessRequest(HttpContext context)
    {
        ProcessRequest1(context);
    }

    private void ProcessRequest1(HttpContext context)
    {
        context.Response.ContentType = "text/json";
        string json = string.Empty;
        string m = context.Request["m"];
        string from = context.Request["from"] == null ? "" : context.Request["from"];

        //排行榜前5名
        RedEnvelopeCacheLogic redEnvelopeCacheLogic = new RedEnvelopeCacheLogic();
        object rankListCache = redEnvelopeCacheLogic.GetRankListOfCache();

        if (m == "pageload")//初始化
        {
            //活动逻辑
            int activityId;
            string cookie = context.Request["cookie"];
            string mobilePhoneNumber = context.Request["mobilePhoneNumber"];
            string activityIdReal = "";
            if (!string.IsNullOrEmpty(context.Request["activityId"]))
            {
                activityIdReal = HttpUtility.UrlDecode(EncryptDecrypt.Decrypt(context.Request["activityId"].ToString(), "va"));
                //activityIdReal = EncryptDecrypt.Decrypt(context.Request["activityId"].ToString().Replace("#","="), "va");
            }
            //校验活动（是否开始，是否结束）  

            if (int.TryParse(activityIdReal, out activityId) && activityId > 0)
            {
                try
                {
                    var activity = redEnvelopeCacheLogic.GetActivityOfCache(activityId);
                    string shareText = "", shareImage = "", activityRule = "";
                    GetShareInfos(activityId, out shareText, out shareImage, out activityRule);
                    if (activity != null && activity.enabled)
                    {
                        if (activity.activityType != ActivityType.赠送红包 && activity.activityType != ActivityType.抽奖红包)
                        {
                            if (activity.beginTime > DateTime.Now)
                            {
                                //活动未开始
                                TimeSpan ts = activity.beginTime.ToUniversalTime() - DateTime.Now.ToUniversalTime();
                                json = Newtonsoft.Json.JsonConvert.SerializeObject(new
                                        {
                                            status = (int)TreasureChestReponseStatus.活动未开始,
                                            totalSeconds = (long)ts.TotalSeconds,
                                            shareText = shareText,
                                            shareImage = WebConfig.CdnDomain + shareImage,
                                            activityRule = activityRule
                                        });
                            }
                            else if (activity.endTime < DateTime.Now)
                            {
                                //活动结束
                                json = Newtonsoft.Json.JsonConvert.SerializeObject(new
                                {
                                    status = (int)TreasureChestReponseStatus.活动结束,
                                    ranklist = rankListCache != null ? rankListCache : "",
                                    activityRule = activityRule,
                                    shareText = shareText,
                                    shareImage = WebConfig.CdnDomain + shareImage
                                });
                            }
                            else
                            {
                                //活动进行时
                                //ITreasureChestRepository treasureChestRepository = RepositoryContext.GetTreasureChestRepository();
                                TreasureChestConfig config = null;
                                //var treasureChest = redEnvelopeCacheLogic.GetTreasureChestOfCache(activityId);
                                //if (treasureChest == null)
                                //{
                                var treasureChestConfigRepository = RepositoryContext.GetTreasureChestConfigRepository();
                                var treasureChestConfigs = treasureChestConfigRepository.GetManyByActivity(activityId);
                                Func<TreasureChestConfig, bool> @where = a => a.status;
                                var chestConfigs = treasureChestConfigs as IList<TreasureChestConfig> ?? treasureChestConfigs.ToList();
                                if (chestConfigs.Any(@where))
                                {
                                    //随机分发宝箱
                                    TreasureChestConfig[] hadConfigs = chestConfigs.Where(@where).ToArray();//宝箱和活动一一对应，取消随机获取宝箱算法
                                    config = hadConfigs[0];
                                    //treasureChest = new TreasureChest
                                    //{
                                    //    activityId = activityId,
                                    //    amount = config.amount,
                                    //    remainAmount = config.amount,
                                    //    count = config.count,
                                    //    createTime = DateTime.Now,
                                    //    expireTime = activity.endTime,
                                    //    isExpire = false,
                                    //    lockCount = config.count,
                                    //    status = true,
                                    //    treasureChestConfigId = config.treasureChestConfigId,
                                    //    cookie = "",
                                    //    mobilePhoneNumber = ""
                                    //};
                                    //treasureChestRepository.Add(treasureChest);
                                    //treasureChest = redEnvelopeCacheLogic.GetTreasureChestOfCache(activityId);
                                }
                                else
                                {
                                    //没有宝箱配置文件
                                    json = Newtonsoft.Json.JsonConvert.SerializeObject(new { status = (int)TreasureChestReponseStatus.配置文件不见 });
                                    context.Response.Write(json);
                                    return;
                                }
                                //}

                                string cookieKey = "cookie_" + activityId;
                                //有宝箱处理
                                if (!string.IsNullOrEmpty(cookie))
                                {
                                    ICustomerInfoRepository customerInfoRepository = RepositoryContext.GetCustomerInfoRepository();
                                    var customer = customerInfoRepository.GetByCookie(cookie);
                                    if (customer != null && !string.IsNullOrEmpty(customer.mobilePhoneNumber))
                                    {
                                        mobilePhoneNumber = customer.mobilePhoneNumber;
                                    }
                                }
                                else
                                {
                                    var httpCookie = context.Request.Cookies[cookieKey];
                                    if (httpCookie != null) cookie = httpCookie.Value;
                                    if (string.IsNullOrEmpty(cookie)) cookie = Common.ToString(Guid.NewGuid()) + Common.ToString(DateTime.Now.Ticks);
                                }
                                context.Response.AppendCookie(new HttpCookie(cookieKey, cookie));
                                RedEnvelope redEnvelope = null;
                                IRedEnvelopeRepository redEnvelopeRepository = RepositoryContext.GetRedEnvelopeRepository();

                                string weChatUserId = null;

                                //有红包
                                int wx = GetIntParameter(context, "wx");
                                if (wx == 1)
                                {
                                    weChatUserId = GetCookies(context);
                                    if (string.IsNullOrEmpty(weChatUserId))
                                    {
                                        json = Newtonsoft.Json.JsonConvert.SerializeObject(new
                                        {
                                            status = (int)TreasureChestReponseStatus.WeChatUserAuthorization
                                        });
                                        context.Response.Write(json);
                                        return;
                                    }

                                    var wechatEntity = new WeChatUserOperator().GetModel(Guid.Parse(weChatUserId));
                                    mobilePhoneNumber = wechatEntity.MobilePhoneNumber;

                                    var redModel = redEnvelopeRepository.GetWeChatModel(activityId, Guid.Parse(weChatUserId));
                                    if (redModel != null)
                                    {
                                        if (redModel.mobilePhoneNumber == mobilePhoneNumber)
                                        {
                                            json = Newtonsoft.Json.JsonConvert.SerializeObject(new
                                            {
                                                status = (int)TreasureChestReponseStatus.已经领过,
                                                context = "你已经领过红包了",
                                                mobilePhoneNumber = redModel.mobilePhoneNumber,
                                                redEnvelopeId = redModel.redEnvelopeId,
                                                amount = redModel.Amount.ToString("F1"),
                                                isGet = true,
                                                activityRule = activityRule,
                                                ranklist = rankListCache != null ? rankListCache : "",
                                                ranking = "",
                                                rankState = 0,
                                                activityType = (int)activity.activityType,
                                                shareText = shareText,
                                                shareImage = WebConfig.CdnDomain + shareImage,
                                                isChange = redModel.isChange

                                            });
                                            context.Response.Write(json);
                                            return;
                                        }
                                        json = Newtonsoft.Json.JsonConvert.SerializeObject(new
                                        {
                                            status = (int)TreasureChestReponseStatus.已经拥有过红包,
                                            context = "你已经领过红包了",
                                            mobilePhoneNumber = redModel.mobilePhoneNumber,
                                            redEnvelopeId = redModel.redEnvelopeId,
                                            amount = redModel.Amount.ToString("F1"),
                                            isGet = true,
                                            activityRule = activityRule,
                                            ranklist = rankListCache != null ? rankListCache : "",
                                            ranking = "",
                                            rankState = 0,
                                            activityType = (int)activity.activityType,
                                            shareText = shareText,
                                            shareImage = WebConfig.CdnDomain + shareImage,
                                            isChange = redModel.isChange

                                        });
                                        context.Response.Write(json);
                                        return;
                                    }
                                }

                                if (!string.IsNullOrEmpty(mobilePhoneNumber))
                                {
                                    //有号码,可能已经领过
                                    //redEnvelope = redEnvelopeRepository.GetByTreasureChestAndMobilePhone(treasureChest.treasureChestId, mobilePhoneNumber);
                                    redEnvelope = redEnvelopeRepository.GetByActivityAndMobilePhone(activity.activityId, mobilePhoneNumber, weChatUserId);

                                    if (redEnvelope != null)
                                    {
                                        if (wx == 1)
                                            redEnvelopeRepository.UpdateWeChatUserId(redEnvelope.redEnvelopeId, Guid.Parse(weChatUserId));

                                        double totalamount = 0;
                                        if (redEnvelope.mobilePhoneNumber == mobilePhoneNumber)
                                        {
                                            json = Newtonsoft.Json.JsonConvert.SerializeObject(new
                                            {
                                                status = (int)TreasureChestReponseStatus.已经领过,
                                                mobilePhoneNumber = mobilePhoneNumber,
                                                redEnvelopeId = redEnvelope.redEnvelopeId,
                                                amount = redEnvelope.Amount.ToString("F1"),
                                                isGet = true,
                                                activityRule = activityRule,
                                                ranklist = rankListCache != null ? rankListCache : "",
                                                ranking = "",
                                                rankState = 0,
                                                activityType = (int)activity.activityType,
                                                shareText = shareText,
                                                shareImage = WebConfig.CdnDomain + shareImage,
                                                totalamount = totalamount.ToString("F1"),
                                                isChange = redEnvelope.isChange
                                            });
                                            context.Response.Write(json);
                                            return;
                                        }

                                        json = Newtonsoft.Json.JsonConvert.SerializeObject(new
                                        {
                                            status = (int)TreasureChestReponseStatus.已经拥有过红包,
                                            context = "你已经领过红包了",
                                            mobilePhoneNumber = mobilePhoneNumber,
                                            redEnvelopeId = redEnvelope.redEnvelopeId,
                                            amount = redEnvelope.Amount.ToString("F1"),
                                            isGet = true,
                                            activityRule = activityRule,
                                            ranklist = rankListCache != null ? rankListCache : "",
                                            ranking = "",
                                            rankState = 0,
                                            activityType = (int)activity.activityType,
                                            shareText = shareText,
                                            shareImage = WebConfig.CdnDomain + shareImage,
                                            totalamount = totalamount.ToString("F1"),
                                            isChange = redEnvelope.isChange
                                        });
                                        context.Response.Write(json);
                                        return;
                                    }
                                }

                                redEnvelope = redEnvelopeRepository.GetByActivityAndCookie(activity.activityId, cookie);
                                //redEnvelope = redEnvelopeRepository.GetByTreasureChestAndCookie(treasureChest.treasureChestId, cookie);
                                if (redEnvelope != null)
                                {
                                    if (string.IsNullOrEmpty(redEnvelope.mobilePhoneNumber))
                                    {
                                        json = Newtonsoft.Json.JsonConvert.SerializeObject(new
                                        {
                                            status = (int)TreasureChestReponseStatus.成功,
                                            mobilePhoneNumber = mobilePhoneNumber,
                                            redEnvelopeId = redEnvelope.redEnvelopeId,
                                            amount = redEnvelope.Amount.ToString("F1"),
                                            activityType = (int)activity.activityType,
                                            activityRule = activityRule,
                                            isGet = false,
                                            ranklist = rankListCache != null ? rankListCache : "",
                                            shareText = shareText,
                                            shareImage = WebConfig.CdnDomain + shareImage
                                        });
                                        context.Response.Write(json);
                                        return;
                                    }
                                    else
                                    {
                                        if (wx == 1)
                                            redEnvelopeRepository.UpdateWeChatUserId(redEnvelope.redEnvelopeId, Guid.Parse(weChatUserId));

                                        double totalamount = 0;
                                        if (redEnvelope.mobilePhoneNumber == mobilePhoneNumber)
                                        {
                                            json = Newtonsoft.Json.JsonConvert.SerializeObject(new
                                            {
                                                status = (int)TreasureChestReponseStatus.已经领过,
                                                mobilePhoneNumber = mobilePhoneNumber,
                                                redEnvelopeId = redEnvelope.redEnvelopeId,
                                                amount = redEnvelope.Amount.ToString("F1"),
                                                isGet = true,
                                                activityRule = activityRule,
                                                ranklist = rankListCache != null ? rankListCache : "",
                                                ranking = "",
                                                rankState = 0,
                                                activityType = (int)activity.activityType,
                                                shareText = shareText,
                                                shareImage = WebConfig.CdnDomain + shareImage,
                                                totalamount = totalamount.ToString("F1"),
                                                isChange = redEnvelope.isChange
                                            });
                                            context.Response.Write(json);
                                            return;
                                        }

                                        json = Newtonsoft.Json.JsonConvert.SerializeObject(new
                                        {
                                            status = (int)TreasureChestReponseStatus.已经拥有过红包,
                                            context = "你已经领过红包了",
                                            mobilePhoneNumber = redEnvelope.mobilePhoneNumber,
                                            redEnvelopeId = redEnvelope.redEnvelopeId,
                                            amount = redEnvelope.Amount.ToString("F1"),
                                            isGet = true,
                                            activityRule = activityRule,
                                            ranklist = rankListCache != null ? rankListCache : "",
                                            ranking = "",
                                            rankState = 0,
                                            activityType = (int)activity.activityType,
                                            shareText = shareText,
                                            shareImage = WebConfig.CdnDomain + shareImage,
                                            totalamount = totalamount.ToString("F1"),
                                            isChange = redEnvelope.isChange
                                        });
                                        context.Response.Write(json);
                                        return;
                                    }
                                }

                                int count = 0;
                                double amountSum = 0.0;
                                double remainAmount = 0.0;
                                //redEnvelopeRepository.Sum(treasureChest.treasureChestId, out count, out amountSum);                            
                                //treasureChest.remainAmount = treasureChest.amount - amountSum;

                                redEnvelopeRepository.Sum(activity.activityId, out amountSum);
                                remainAmount = config.amount - amountSum;
                                //没有领过
                                //if (treasureChest.remainAmount > 0)//宝箱剩余还有钱
                                if (remainAmount > 0)//活动还有钱
                                {
                                    //if (config == null || config.treasureChestConfigId != treasureChest.treasureChestConfigId)
                                    //{
                                    //    config = redEnvelopeCacheLogic.GetTreasureChestConfigOfCache(treasureChest.treasureChestConfigId);
                                    //    if (config == null)
                                    //    {
                                    //        //没有配置文件
                                    //        json = Newtonsoft.Json.JsonConvert.SerializeObject(new { status = (int)TreasureChestReponseStatus.配置文件不见 });
                                    //        context.Response.Write(json);
                                    //        return;
                                    //    }
                                    //}

                                    redEnvelope = new RedEnvelope
                                    {
                                        Amount = 0,//初始值
                                        effectTime = DateTime.Now,//初始值
                                        expireTime = DateTime.Now,//初始值
                                        getTime = DateTime.Now,
                                        isExecuted = 0,
                                        //isExpire = treasureChest.isExpire,
                                        isExpire = false,
                                        status = true,
                                        //reasureChestId = treasureChest.treasureChestId,
                                        treasureChestId = 0,
                                        mobilePhoneNumber = "",
                                        unusedAmount = 0,//初始指
                                        activityId = activity.activityId,
                                        isOwner = false,
                                        isOverflow = false,
                                        cookie = cookie,
                                        uuid = "",//执行抢操作存空，分享操作Update此处，跟手机号码同步
                                        from = from
                                    };
                                    switch (activity.expirationTimeRule)
                                    {
                                        case ExpirationTimeRule.postpone:
                                            redEnvelope.effectTime = redEnvelope.getTime;
                                            redEnvelope.expireTime = Common.ToDateTime(DateTime.Now.AddDays(activity.ruleValue).ToString("yyyy/MM/dd 23:59:59"));
                                            break;
                                        case ExpirationTimeRule.unify:
                                            redEnvelope.effectTime = activity.redEnvelopeEffectiveBeginTime;
                                            redEnvelope.expireTime = activity.redEnvelopeEffectiveEndTime;
                                            break;
                                    }
                                    if (activity.activityType == ActivityType.节日免单红包 || activity.activityType == ActivityType.大红包)//节日免单红包，大红包
                                    {
                                        //redEnvelope.expireTime = activity.redEnvelopeEffectiveEndTime;
                                        //if (activity.activityType == ActivityType.大红包)
                                        //{
                                        //    redEnvelope.expireTime = activity.expirationTimeRule == ExpirationTimeRule.postpone ? DateTime.Now.AddDays(activity.ruleValue) : activity.endTime;
                                        //}
                                        redEnvelope.Amount = 0;
                                        redEnvelope.unusedAmount = 0;
                                    }
                                    else//天天红包
                                    {
                                        //redEnvelope.expireTime = activity.expirationTimeRule == ExpirationTimeRule.postpone ? DateTime.Now.AddDays(activity.ruleValue) : activity.endTime;                                    
                                        switch (config.amountRule)
                                        {
                                            case (int)RedEnvelopeAmountRule.概率取值:
                                                if (!string.IsNullOrEmpty(config.newAmountRange) && !string.IsNullOrEmpty(config.newRateRange))
                                                {
                                                    bool isMobileVerified = CustomerOperate.IsMobileVerified(mobilePhoneNumber);//手机号码为空，bool应该为false，不影响逻辑
                                                    redEnvelope.Amount = isMobileVerified == true ? GetAmount(config.defaultAmountRange, config.defaultRateRange)
                                                        : GetAmount(config.newAmountRange, config.newRateRange); //区分新老用户
                                                }
                                                else
                                                {
                                                    //默认所有用户
                                                    redEnvelope.Amount = GetAmount(config.defaultAmountRange, config.defaultRateRange);
                                                }
                                                break;
                                            default:
                                            case (int)RedEnvelopeAmountRule.最小最大值:
                                                redEnvelope.Amount = GetAmount((int)(config.min * 10), (int)(config.max * 10));//计算获取红包金额算法
                                                break;
                                        }
                                        redEnvelope.unusedAmount = redEnvelope.Amount;
                                    }

                                    redEnvelopeRepository.Add(redEnvelope);//新增红包记录

                                    //绑定
                                    if (wx == 1 && string.IsNullOrEmpty(mobilePhoneNumber))
                                    {
                                        json = Newtonsoft.Json.JsonConvert.SerializeObject(new
                                        {
                                            status = (int)TreasureChestReponseStatus.WeChatUserWaiting,
                                            mobilePhoneNumber = mobilePhoneNumber,
                                            redEnvelopeId = redEnvelope.redEnvelopeId,
                                            amount = redEnvelope.Amount.ToString("F1"),
                                            isGet = false,
                                            activityType = (int)activity.activityType,
                                            activityRule = activityRule,
                                            ranklist = rankListCache != null ? rankListCache : "",
                                            shareText = shareText,
                                            shareImage = WebConfig.CdnDomain + shareImage
                                        });
                                        context.Response.Write(json);
                                        return;
                                    }

                                    json = Newtonsoft.Json.JsonConvert.SerializeObject(new
                                    {
                                        status = (int)TreasureChestReponseStatus.成功,
                                        mobilePhoneNumber = mobilePhoneNumber,
                                        redEnvelopeId = redEnvelope.redEnvelopeId,
                                        amount = redEnvelope.Amount.ToString("F1"),
                                        isGet = false,
                                        activityType = (int)activity.activityType,
                                        activityRule = activityRule,
                                        ranklist = rankListCache != null ? rankListCache : "",
                                        shareText = shareText,
                                        shareImage = WebConfig.CdnDomain + shareImage
                                    });
                                }
                                else
                                {
                                    //活动结束
                                    json = Newtonsoft.Json.JsonConvert.SerializeObject(new
                                    {
                                        status = (int)TreasureChestReponseStatus.活动结束,
                                        activityRule = activityRule,
                                        ranklist = rankListCache != null ? rankListCache : ""
                                    });
                                }
                            }
                        }
                        else
                        {
                            //赠送红包不应出现在抢红包处
                            json = Newtonsoft.Json.JsonConvert.SerializeObject(new { status = (int)TreasureChestReponseStatus.未找到活动 });
                        }
                    }
                    else
                    {
                        //没有活动
                        json = Newtonsoft.Json.JsonConvert.SerializeObject(new { status = (int)TreasureChestReponseStatus.未找到活动 });
                    }
                }
                catch (Exception exc)
                {
                    LogDll.LogManager.WriteLog(LogFile.Error, string.Format("pageload--{0:G} [{2}] {1}", DateTime.Now, exc.ToString(), activityId));
                    json = Newtonsoft.Json.JsonConvert.SerializeObject(new
                    {
                        status = (int)TreasureChestReponseStatus.服务器异常,
                    });
                }

                //VAAppType appType = VAAppType.WAP;
                //if (Regex.Match(context.Request.UserAgent, "android", RegexOptions.IgnoreCase).Success)
                //{
                //    appType = VAAppType.ANDROID;
                //}
                //else if (Regex.Match(context.Request.UserAgent, "iPhone|iPod|iPad", RegexOptions.IgnoreCase).Success)
                //{
                //    appType = VAAppType.IPHONE;
                //}

                //TreasureChestAccessInfo treasureChestAccessInfo = new TreasureChestAccessInfo()
                //{
                //    accessTime = DateTime.Now,
                //    activityId = activityId,
                //    cookie = string.IsNullOrEmpty(cookie) ? "" : cookie,
                //    mobilePhoneNumber = string.IsNullOrEmpty(mobilePhoneNumber) ? "" : mobilePhoneNumber,
                //    sourceType = "pageload",
                //    url = context.Request.RawUrl,
                //    ip = string.IsNullOrEmpty(context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"]) ? context.Request.UserHostAddress : context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"],
                //    appType = appType
                //};
                //BatchInsertTreasureChestAccessInfo(treasureChestAccessInfo);
            }
            else
            {
                //json = Newtonsoft.Json.JsonConvert.SerializeObject(new { status = (int)TreasureChestReponseStatus.参数错误, });
                json = Newtonsoft.Json.JsonConvert.SerializeObject(new { status = (int)TreasureChestReponseStatus.未找到活动 });
            }
        }
        else if (m == "shared")
        {
            //手机与微信帐户绑定
            //如果是微信环境
            string mobilePhoneNumber = context.Request["mobilePhoneNumber"];
            int wx = GetIntParameter(context, "wx");
            string weChatUserId = null;
            bool isUpdateMobile = true;
            if (wx == 1)
            {
                weChatUserId = GetCookies(context);
                if (string.IsNullOrEmpty(weChatUserId))
                {
                    json = Newtonsoft.Json.JsonConvert.SerializeObject(new
                    {
                        status = (int)TreasureChestReponseStatus.WeChatUserAuthorization
                    });
                    context.Response.Write(json);
                    return;
                }

                //微信用户验证
                if (mobilePhoneNumber == null)
                {
                    json = Newtonsoft.Json.JsonConvert.SerializeObject(new
                    {
                        status = (int)TreasureChestReponseStatus.参数错误
                    });
                    context.Response.Write(json);
                    context.Response.End();
                }
                if (!System.Text.RegularExpressions.Regex.IsMatch(mobilePhoneNumber, @"^[1][3-8]\d{9}$"))
                {
                    json = Newtonsoft.Json.JsonConvert.SerializeObject(new
                    {
                        status = (int)TreasureChestReponseStatus.参数错误
                    });
                    context.Response.Write(json);
                    return;
                }

                var wechatEntity = new WeChatUserOperator().GetModel(Guid.Parse(weChatUserId));
                if (!string.IsNullOrEmpty(wechatEntity.MobilePhoneNumber))
                {
                    mobilePhoneNumber = wechatEntity.MobilePhoneNumber;
                    isUpdateMobile = false;
                }
                //mobilePhoneNumber = string.IsNullOrEmpty(wechatEntity.MobilePhoneNumber) ? mobilePhoneNumber : wechatEntity.MobilePhoneNumber;
            }

            //活动逻辑
            long redEnvelopeId;
            if (long.TryParse(context.Request["redEnvelopeId"], out redEnvelopeId) && redEnvelopeId > 0 && !string.IsNullOrEmpty(mobilePhoneNumber))
            {
                try
                {
                    IRedEnvelopeRepository redEnvelopeRepository = RepositoryContext.GetRedEnvelopeRepository();
                    var redEnvelope = redEnvelopeRepository.GetById(redEnvelopeId);
                    if (redEnvelope != null)
                    {
                        //var otherRedEnvelope = redEnvelopeRepository.GetByTreasureChestAndMobilePhone(redEnvelope.treasureChestId, mobilePhoneNumber);
                        var otherRedEnvelope = redEnvelopeRepository.GetByActivityAndMobilePhone(redEnvelope.activityId, mobilePhoneNumber, weChatUserId);
                        if (otherRedEnvelope != null && otherRedEnvelope.redEnvelopeId != redEnvelopeId)
                        {
                            //绑定正确的手机号
                            if (wx == 1)
                            {
                                if (isUpdateMobile)
                                {
                                    var weChatUserBll = new WeChatUserOperator();
                                    //手机号已存在报错
                                    if (weChatUserBll.IsExistMombile(mobilePhoneNumber))
                                    {
                                        json = Newtonsoft.Json.JsonConvert.SerializeObject(new
                                        {
                                            status = (int)TreasureChestReponseStatus.WeChatUserMobileIsExist
                                        });
                                        context.Response.Write(json);
                                        return;
                                    }

                                    weChatUserBll.UpdateMobile(weChatUserId, mobilePhoneNumber, "系统", IPAddress);
                                }
                                redEnvelopeRepository.UpdateWeChatUserId(otherRedEnvelope.redEnvelopeId, Guid.Parse(weChatUserId));
                            }
                            redEnvelopeRepository.DelSingleData(redEnvelopeId);

                            if (otherRedEnvelope.mobilePhoneNumber == mobilePhoneNumber)
                            {
                                json = Newtonsoft.Json.JsonConvert.SerializeObject(new
                                {
                                    status = (int)TreasureChestReponseStatus.已经领过,
                                    context = "你已经领过红包了",
                                    amount = otherRedEnvelope.Amount.ToString("F1"),
                                    redEnvelopeId = otherRedEnvelope.redEnvelopeId,
                                    isChange = otherRedEnvelope.isChange
                                });
                                context.Response.Write(json);
                                return;
                            }

                            json = Newtonsoft.Json.JsonConvert.SerializeObject(new
                            {
                                status = (int)TreasureChestReponseStatus.已经拥有过红包,
                                context = "你已经领过红包了",
                                amount = otherRedEnvelope.Amount.ToString("F1"),
                                redEnvelopeId = otherRedEnvelope.redEnvelopeId,
                                isChange = otherRedEnvelope.isChange
                            });
                            context.Response.Write(json);
                            return;
                        }

                        string tempUUID = "";
                        var activity = redEnvelopeCacheLogic.GetActivityOfCache(redEnvelope.activityId);

                        //var treasureChest = redEnvelopeCacheLogic.GetTreasureChestOfCache(redEnvelope.activityId);
                        //TreasureChestConfig config = redEnvelopeCacheLogic.GetTreasureChestConfigOfCache(treasureChest.treasureChestConfigId);

                        TreasureChestConfig config = redEnvelopeCacheLogic.GetConfigOfActivityOfCache(activity.activityId);
                        bool isMobileVerified = CustomerOperate.IsMobileVerified(mobilePhoneNumber);
                        if (config.isPreventCheat == true)//启用防作弊
                        {
                            if (isMobileVerified)
                            {
                                var listUUID = redEnvelopeRepository.GetCustomerDeviceUUID(mobilePhoneNumber, activity.activityId);
                                if (!listUUID.Any())
                                {
                                    var uuidModel = redEnvelopeRepository.GetAcitvityIdAndUuidModel(activity.activityId, mobilePhoneNumber);
                                    listUUID = listUUID.OrderByDescending(p => p.updateTime).ToList();
                                    redEnvelopeRepository.DelSingleData(redEnvelopeId);

                                    //绑定正确的手机号
                                    if (wx == 1)
                                    {
                                        if (isUpdateMobile)
                                        {
                                            var weChatUserBll = new WeChatUserOperator();
                                            //手机号已存在报错
                                            if (weChatUserBll.IsExistMombile(mobilePhoneNumber))
                                            {
                                                json = Newtonsoft.Json.JsonConvert.SerializeObject(new
                                                {
                                                    status = (int)TreasureChestReponseStatus.WeChatUserMobileIsExist
                                                });
                                                context.Response.Write(json);
                                                return;
                                            }
                                            if (uuidModel != null)
                                                weChatUserBll.UpdateMobile(weChatUserId, uuidModel.mobilePhoneNumber, "系统", IPAddress);
                                        }
                                        if (uuidModel != null)
                                            redEnvelopeRepository.UpdateWeChatUserId(uuidModel.redEnvelopeId, Guid.Parse(weChatUserId));
                                    }
                                    if (uuidModel != null)
                                    {
                                        if (uuidModel.mobilePhoneNumber == mobilePhoneNumber)
                                        {
                                            json = Newtonsoft.Json.JsonConvert.SerializeObject(new
                                            {
                                                status = (int)TreasureChestReponseStatus.已经领过,
                                                context = "你已经领过红包了",
                                                amount = uuidModel.Amount.ToString("F1"),
                                                redEnvelopeId = uuidModel.redEnvelopeId,
                                                isChange = uuidModel.isChange,
                                                mobilePhoneNumber = uuidModel.mobilePhoneNumber
                                            });
                                            context.Response.Write(json);
                                            return;
                                        }

                                        json = Newtonsoft.Json.JsonConvert.SerializeObject(new
                                        {
                                            status = (int)TreasureChestReponseStatus.已经拥有过红包,
                                            context = "你已经领过红包了",
                                            amount = uuidModel.Amount.ToString("F1"),
                                            redEnvelopeId = uuidModel.redEnvelopeId,
                                            isChange = uuidModel.isChange,
                                            mobilePhoneNumber = uuidModel.mobilePhoneNumber
                                        });
                                        context.Response.Write(json);
                                        return;
                                    }
                                }
                                else
                                    listUUID = listUUID.OrderByDescending(p => p.updateTime).ToList();
                                tempUUID = listUUID[0].uuid.ToString();
                            }
                        }
                        //绑定手机号
                        if (wx == 1 && isUpdateMobile)
                        {
                            var weChatUserBll = new WeChatUserOperator();
                            //手机号已存在报错
                            if (weChatUserBll.IsExistMombile(mobilePhoneNumber))
                            {
                                json = Newtonsoft.Json.JsonConvert.SerializeObject(new
                                {
                                    status = (int)TreasureChestReponseStatus.WeChatUserMobileIsExist
                                });
                                context.Response.Write(json);
                                return;
                            }

                            weChatUserBll.UpdateMobile(weChatUserId, mobilePhoneNumber, "系统", IPAddress);
                        }

                        if (activity.activityType == ActivityType.节日免单红包 || activity.activityType == ActivityType.大红包)
                        {
                            switch (config.amountRule)
                            {
                                case (int)RedEnvelopeAmountRule.概率取值:
                                    if (!string.IsNullOrEmpty(config.newAmountRange) && !string.IsNullOrEmpty(config.newRateRange))
                                    {
                                        redEnvelope.Amount = isMobileVerified == true ? GetAmount(config.defaultAmountRange, config.defaultRateRange)
                                            : GetAmount(config.newAmountRange, config.newRateRange); //区分新老用户
                                    }
                                    else
                                    {
                                        //默认所有用户
                                        redEnvelope.Amount = GetAmount(config.defaultAmountRange, config.defaultRateRange);
                                    }
                                    break;
                                default:
                                case (int)RedEnvelopeAmountRule.最小最大值:
                                    redEnvelope.Amount = GetAmount((int)(config.min * 10), (int)(config.max * 10));//计算获取红包金额算法
                                    break;
                            }
                            redEnvelope.unusedAmount = redEnvelope.Amount;
                        }

                        //RedEnvelopeDetail redEnvelopeDetail = new RedEnvelopeDetail
                        //{
                        //    mobilePhoneNumber = mobilePhoneNumber,
                        //    operationTime = DateTime.Now,
                        //    treasureChestId = redEnvelope.treasureChestId,
                        //    redEnvelopeAmount = redEnvelope.Amount,
                        //    redEnvelopeExpirationTime = redEnvelope.expireTime,
                        //    usedAmount = 0,
                        //    stateType = (byte)VARedEnvelopeStateType.已生效,
                        //    redEnvelopeId = redEnvelopeId
                        //};

                        IRedEnvelopeDetailRepository reaEnvelopeDetailRepository = RepositoryContext.GetRedEnvelopeDetailRepository();
                        long ranking = 0;
                        double totalamount = 0;
                        RedEnvelopeRankList ranklist = null;


                        int c = redEnvelopeRepository.UpdateMobilePhoneNumberAndisExecuted(redEnvelopeId, mobilePhoneNumber, VARedEnvelopeStateType.已生效, tempUUID, redEnvelope.Amount, weChatUserId);
                        //using (var scope = new TransactionScope())
                        //{
                        //    //wangc
                        //    int a = 0;
                        //    int c = 0;
                        //    int b = 0;
                        //    if (activity.activityType == ActivityType.节日免单红包)//未生效红包存在条件
                        //    {
                        //        redEnvelopeDetail.stateType = (byte)VARedEnvelopeStateType.未生效;
                        //        a = reaEnvelopeDetailRepository.Add(redEnvelopeDetail);
                        //        c = redEnvelopeRepository.UpdateMobilePhoneNumberAndisExecuted(redEnvelopeId, mobilePhoneNumber, VARedEnvelopeStateType.未生效, tempUUID, redEnvelope.Amount);
                        //        if (isMobileVerified)//注册过用户
                        //        {
                        //            b = redEnvelopeRepository.AddCustomerRedEnvelopeAmount(mobilePhoneNumber, redEnvelope.Amount, false);
                        //        }
                        //        else
                        //        {
                        //            b = 1;
                        //        }
                        //    }
                        //    else//及时生效红包，天天红包和大红包
                        //    {
                        //        redEnvelopeDetail.stateType = (byte)VARedEnvelopeStateType.已生效;
                        //        a = reaEnvelopeDetailRepository.Add(redEnvelopeDetail);
                        //        c = redEnvelopeRepository.UpdateMobilePhoneNumberAndisExecuted(redEnvelopeId, mobilePhoneNumber, VARedEnvelopeStateType.已生效, tempUUID, redEnvelope.Amount);
                        //        if (isMobileVerified)
                        //        {
                        //            b = redEnvelopeRepository.AddCustomerRedEnvelopeAmount(mobilePhoneNumber, redEnvelope.Amount, true);
                        //        }
                        //        else
                        //        {
                        //            b = 1;
                        //        }
                        //    }
                        //    if (a == 1 && c == 1 && b > 0)
                        //    {
                        //        scope.Complete();
                        //    }
                        //}
                        string shareText;
                        string shareImage;
                        string activityRule;
                        GetShareInfos(redEnvelope.activityId, out shareText, out shareImage, out activityRule);
                        json = Newtonsoft.Json.JsonConvert.SerializeObject(new
                        {
                            status = (int)TreasureChestReponseStatus.分享成功,
                            mobilePhoneNumber = mobilePhoneNumber,
                            redEnvelopeId = redEnvelope.redEnvelopeId,
                            amount = redEnvelope.Amount.ToString("F1"),
                            ranking = ranking,
                            rankState = ranklist != null ? (int)ranklist.rankState : 0,
                            ranklist = rankListCache != null ? rankListCache : "",
                            activityType = (int)activity.activityType,
                            activityRule = activityRule,
                            shareText = shareText,
                            shareImage = WebConfig.CdnDomain + shareImage,
                            totalamount = totalamount.ToString("F1")
                        });
                    }
                    else
                    {
                        json = Newtonsoft.Json.JsonConvert.SerializeObject(new
                        {
                            status = (int)TreasureChestReponseStatus.未找到红包
                        });
                    }
                }
                catch (Exception exc)
                {
                    LogDll.LogManager.WriteLog(LogFile.Error, string.Format("share--{0:G} [{2}] {1}", DateTime.Now, exc.ToString(), redEnvelopeId));
                    json = Newtonsoft.Json.JsonConvert.SerializeObject(new
                    {
                        status = (int)TreasureChestReponseStatus.服务器异常
                    });
                }
            }
            else
            {
                json = Newtonsoft.Json.JsonConvert.SerializeObject(new
                {
                    status = (int)TreasureChestReponseStatus.参数错误
                });
            }
        }
        else if (m == "modify")//号码纠错
        {
            string mobilePhoneNumber = context.Request["mobilePhoneNumber"];
            long redEnvelopeId;
            if (long.TryParse(context.Request["redEnvelopeId"], out redEnvelopeId) && redEnvelopeId > 0 && !string.IsNullOrEmpty(mobilePhoneNumber))
            {
                IRedEnvelopeRepository redEnvelopeRepository = RepositoryContext.GetRedEnvelopeRepository();
                var redEnvelope = redEnvelopeRepository.GetById(redEnvelopeId);
                if (redEnvelope != null && redEnvelope.isExpire == false && redEnvelope.expireTime > DateTime.Now
                    && (decimal)redEnvelope.Amount == (decimal)redEnvelope.unusedAmount && redEnvelope.isChange == false
                    && redEnvelope.mobilePhoneNumber != mobilePhoneNumber
                    && (redEnvelope.isExecuted == (int)VARedEnvelopeStateType.已生效 || redEnvelope.isExecuted == (int)VARedEnvelopeStateType.未生效))
                {
                    //var otherRedEnvelope = redEnvelopeRepository.GetByTreasureChestAndMobilePhone(redEnvelope.treasureChestId, mobilePhoneNumber);//查询当前用户是否领取过当前活动红包

                    var otherRedEnvelope = redEnvelopeRepository.GetByActivityAndMobilePhone(redEnvelope.activityId, mobilePhoneNumber, null);
                    if (otherRedEnvelope == null)
                    {
                        //判断当前手机号码是否已注册
                        //判断当前手机号码所有登录的设备是否都已经领取过红包
                        string tempUUID = "";
                        //var treasureChest = redEnvelopeCacheLogic.GetTreasureChestOfCache(redEnvelope.activityId);                        
                        //TreasureChestConfig config = redEnvelopeCacheLogic.GetTreasureChestConfigOfCache(treasureChest.treasureChestConfigId);

                        TreasureChestConfig config = redEnvelopeCacheLogic.GetConfigOfActivityOfCache(redEnvelope.activityId);

                        bool isMobileVerified1 = CustomerOperate.IsMobileVerified(mobilePhoneNumber);//修改后号码是否已注册
                        if (config.isPreventCheat == true && isMobileVerified1 == true)//启用防作弊，纠错给老用户号码才有意义
                        {
                            IActivityRepository activityRepository = RepositoryContext.GetActivityRepository();
                            var activity = activityRepository.GetById(redEnvelope.activityId);
                            IRedEnvelopeRepository _redEnvelopeRepository = RepositoryContext.GetRedEnvelopeRepository();
                            var listUUID = _redEnvelopeRepository.GetCustomerDeviceUUID(mobilePhoneNumber, activity.activityId);
                            if (!listUUID.Any())
                            {
                                var uuidModel = redEnvelopeRepository.GetAcitvityIdAndUuidModel(activity.activityId, mobilePhoneNumber);
                                if (uuidModel == null)
                                {
                                    json = Newtonsoft.Json.JsonConvert.SerializeObject(new
                                    {
                                        status = (int)TreasureChestReponseStatus.已经拥有过红包,
                                        isChange = 1
                                    });
                                    context.Response.Write(json);
                                    return;
                                }

                                if (uuidModel.mobilePhoneNumber == mobilePhoneNumber)
                                {
                                    json = Newtonsoft.Json.JsonConvert.SerializeObject(new
                                    {
                                        status = (int)TreasureChestReponseStatus.已经领过,
                                        isChange = 1,
                                        context = "你已经领过红包了"
                                    });
                                    context.Response.Write(json);
                                    return;
                                }

                                json = Newtonsoft.Json.JsonConvert.SerializeObject(new
                                {
                                    status = (int)TreasureChestReponseStatus.已经拥有过红包,
                                    context = "你已经领过红包了",
                                    isChange = 1
                                });
                                context.Response.Write(json);
                                return;
                            }
                            listUUID = listUUID.OrderByDescending(p => p.updateTime).ToList();
                            tempUUID = listUUID[0].uuid.ToString();
                        }

                        //bool isMobileVerified2 = CustomerOperate.IsMobileVerified(redEnvelope.mobilePhoneNumber);//错误号码是否已注册

                        //int a = 0, b = 0, c = 0;
                        //IRedEnvelopeDetailRepository redEnvelopeDetailRepository = RepositoryContext.GetRedEnvelopeDetailRepository();
                        //using (var scope = new TransactionScope())
                        //{
                        //    a = redEnvelopeRepository.UpdateMobilePhoneNumberAndIsChange(redEnvelopeId, mobilePhoneNumber, tempUUID);//修改红包表红包和手机号码对应信息
                        //    b = redEnvelopeDetailRepository.UpdateMobilePhoneNumberByRedEnvelope(redEnvelopeId, mobilePhoneNumber);//修改红包详情和手机号码信息
                        //    //号码纠错，弃用触发器 wangc
                        //    //红包未生效 activity.activityType == ActivityType.节日免单红包
                        //    //isMobileVerified1新号码；isMobileVerified2旧号码
                        //    if (isMobileVerified1 && isMobileVerified2)//已注册号码→已注册号码
                        //    {
                        //        if (redEnvelope.isExecuted == 0)
                        //        {
                        //            c = redEnvelopeRepository.UpdateCustomerRedEnvelopeAmount(redEnvelope.mobilePhoneNumber, mobilePhoneNumber, redEnvelope.Amount, false);
                        //        }
                        //        else
                        //        {
                        //            c = redEnvelopeRepository.UpdateCustomerRedEnvelopeAmount(redEnvelope.mobilePhoneNumber, mobilePhoneNumber, redEnvelope.Amount, true);
                        //        }
                        //    }
                        //    else if (!isMobileVerified1 && isMobileVerified2)//已注册号码→未注册号码
                        //    {
                        //        if (redEnvelope.isExecuted == 0)
                        //        {
                        //            c = redEnvelopeRepository.AddCustomerRedEnvelopeAmount(redEnvelope.mobilePhoneNumber, (-1) * redEnvelope.Amount, false);
                        //        }
                        //        else
                        //        {
                        //            c = redEnvelopeRepository.AddCustomerRedEnvelopeAmount(redEnvelope.mobilePhoneNumber, (-1) * redEnvelope.Amount, true);
                        //        }
                        //    }
                        //    else if (isMobileVerified1 && !isMobileVerified2)//未注册号码→已注册号码
                        //    {
                        //        if (redEnvelope.isExecuted == 0)
                        //        {
                        //            c = redEnvelopeRepository.AddCustomerRedEnvelopeAmount(mobilePhoneNumber, redEnvelope.Amount, false);
                        //        }
                        //        else
                        //        {
                        //            c = redEnvelopeRepository.AddCustomerRedEnvelopeAmount(mobilePhoneNumber, redEnvelope.Amount, true);
                        //        }
                        //    }
                        //    else if (!isMobileVerified1 && !isMobileVerified2)//未注册号码→未注册号码
                        //    {
                        //        c = 1;
                        //    }

                        //    if (a > 0 && b > 0 && c > 0)
                        //    {
                        //        scope.Complete();
                        //    }
                        //    else
                        //    {
                        //        json = Newtonsoft.Json.JsonConvert.SerializeObject(new { status = (int)TreasureChestReponseStatus.服务器异常 });
                        //        context.Response.Write(json);
                        //        return;
                        //    }
                        //}
                        int a = redEnvelopeRepository.UpdateMobilePhoneNumberAndIsChange(redEnvelopeId, mobilePhoneNumber, tempUUID);//修改红包表红包和手机号码对应信息
                        if (a <= 0)
                        {
                            json = Newtonsoft.Json.JsonConvert.SerializeObject(new { status = (int)TreasureChestReponseStatus.服务器异常 });
                            context.Response.Write(json);
                            return;
                        }
                        int wx = GetIntParameter(context, "wx");
                        if (wx == 1)
                        {
                            var weChatUserBll = new WeChatUserOperator();
                            if (!weChatUserBll.IsExistMombile(mobilePhoneNumber))
                                new WeChatUserOperator().UpdateNewMobile(redEnvelope.mobilePhoneNumber, mobilePhoneNumber, "系统", IPAddress);

                            string weChatUserId = GetCookies(context);
                            if (!string.IsNullOrEmpty(weChatUserId))
                                redEnvelopeRepository.UpdateWeChatUserId(redEnvelopeId, Guid.Parse(weChatUserId));
                        }

                        json = Newtonsoft.Json.JsonConvert.SerializeObject(new
                        {
                            status = 0,
                            mobilePhoneNumber = mobilePhoneNumber
                        });
                        context.Response.Write(json);
                        return;
                    }
                    else
                    {
                        if (otherRedEnvelope.mobilePhoneNumber == mobilePhoneNumber)
                        {
                            json = Newtonsoft.Json.JsonConvert.SerializeObject(new { status = (int)TreasureChestReponseStatus.已经领过, context = "你已经领过红包了", isChange = redEnvelope.isChange });
                            context.Response.Write(json);
                            return;
                        }
                        json = Newtonsoft.Json.JsonConvert.SerializeObject(new { status = (int)TreasureChestReponseStatus.已经拥有过红包, context = "你已经领过红包了", isChange = redEnvelope.isChange });
                        context.Response.Write(json);
                        return;
                    }
                }
            }
            json = Newtonsoft.Json.JsonConvert.SerializeObject(new { status = 1 });
        }
        context.Response.Write(json);
    }

    private static void GetShareInfos(int activityId, out string shareText, out string shareImage, out string activityRule)
    {
        RedEnvelopeCacheLogic redEnvelopeCacheLogic = new RedEnvelopeCacheLogic();
        var shareInfos = redEnvelopeCacheLogic.GetActivityShareInfoOfCache(activityId);
        shareText = string.Empty;
        shareImage = string.Empty;
        activityRule = string.Empty;
        int index = 0;
        if (shareInfos != null)
        {
            var shareTexts = shareInfos.Where(p => p.type == ActivityShareInfoType.Text);
            var shareImages = shareInfos.Where(p => p.type == ActivityShareInfoType.Image);
            var activityRules = shareInfos.Where(p => p.type == ActivityShareInfoType.activityRule);
            if (shareTexts.Any())
            {
                index = random.Next(0, shareTexts.Count());
                shareText = shareTexts.Skip(index).Take(1).Select(p => p.remark).FirstOrDefault();
            }
            if (shareImages.Any())
            {
                index = random.Next(0, shareImages.Count());
                shareImage = shareImages.Skip(index).Take(1).Select(p => p.remark).FirstOrDefault();
            }
            if (activityRules.Any())
            {
                activityRule = activityRules.Select(p => p.remark).FirstOrDefault();
            }
        }
        if (string.IsNullOrEmpty(shareText) || string.IsNullOrEmpty(shareImage) || string.IsNullOrEmpty(activityRule))
        {
            var defaultShareInfos = redEnvelopeCacheLogic.GetActivityShareInfoOfCache(0);
            if (defaultShareInfos != null)
            {
                var shareTexts = defaultShareInfos.Where(p => p.type == ActivityShareInfoType.Text);
                var shareImages = defaultShareInfos.Where(p => p.type == ActivityShareInfoType.Image);
                var activityRules = defaultShareInfos.Where(p => p.type == ActivityShareInfoType.activityRule);
                if (shareTexts.Any() && string.IsNullOrEmpty(shareText))
                {
                    index = random.Next(0, shareTexts.Count());
                    shareText = shareTexts.Skip(index).Take(1).Select(p => p.remark).FirstOrDefault();
                }
                if (shareImages.Any() && string.IsNullOrEmpty(shareImage))
                {
                    index = random.Next(0, shareImages.Count());
                    shareImage = shareImages.Skip(index).Take(1).Select(p => p.remark).FirstOrDefault();
                }
                if (activityRules.Any() && string.IsNullOrEmpty(activityRule))
                {
                    activityRule = activityRules.Select(p => p.remark).FirstOrDefault();
                }
            }
        }
    }

    /// <summary>
    /// 取cookie
    /// </summary>
    /// <param name="context"></param>
    /// <param name="cookiesid"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    private static string GetCookies(HttpContext context, string cookiesid = "weChatUserId", int count = 3)
    {
        HttpCookie cookiesValue = null;
        do
        {
            Thread.Sleep(1);
            cookiesValue = context.Request.Cookies[cookiesid];
        }
        while (cookiesValue == null && --count != 0);
        return cookiesValue == null ? null : cookiesValue.Value;
    }

    /// <summary>
    /// 取int参数
    /// </summary>
    /// <param name="context"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    private static int GetIntParameter(HttpContext context, string key)
    {
        string value = context.Request.QueryString[key];
        if (string.IsNullOrEmpty(value))
            value = context.Request.Form[key];
        return int.Parse(value ?? "0");
    }


    /// <summary>
    /// 穿过代理服务器取远程用户真实IP地址
    /// </summary>
    /// <returns></returns>
    private string IPAddress
    {
        get
        {
            if (HttpContext.Current.Request.ServerVariables["HTTP_VIA"] != null)
                return HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
            else
                return HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
        }
    }

    //private void BatchInsertTreasureChestAccessInfo(TreasureChestAccessInfo access)
    //{
    //    object TreasureChestAccessInfoCache = CacheHelper.GetCache("redEnvelope_treasureChestAccess");

    //    if (TreasureChestAccessInfoCache == null)
    //    {
    //        List<TreasureChestAccessInfo> accessList = new List<TreasureChestAccessInfo>();
    //        accessList.Add(access);

    //        CacheHelper.AddCache("redEnvelope_treasureChestAccess", accessList, 3600, delegateTreasureChestAccess);//3600
    //    }
    //    else
    //    {
    //        List<TreasureChestAccessInfo> accessList = (List<TreasureChestAccessInfo>)TreasureChestAccessInfoCache;
    //        accessList.Add(access);
    //        if (accessList.Count > 299)
    //        {
    //            CacheHelper.RemoveCache("redEnvelope_treasureChestAccess");
    //        }
    //    }
    //}

    //private void delegateTreasureChestAccess(string key, object value, System.Web.Caching.CacheItemRemovedReason reason)
    //{
    //    Thread thread = new Thread(BatchInsertTreasureChestAccess);
    //    thread.Start(value);
    //}

    //private void BatchInsertTreasureChestAccess(object value)
    //{
    //    RedEnvelopeOperate redEnvelopeOperate = new RedEnvelopeOperate();
    //    System.Data.DataTable dtAccess = Common.ListToDataTable((List<TreasureChestAccessInfo>)value);
    //    dtAccess.TableName = "TreasureChestAccessInfo";
    //    redEnvelopeOperate.BatchInsert(dtAccess);
    //}

    private double GetAmount(string range, string scale)
    {
        string[] rangeStr = range.Split(',');
        string[] scaleStr = scale.Split(',');
        int count = 0;
        int rmdCount = random.Next(0, 100);
        for (int i = 0; i < scaleStr.Length; i++)
        {
            if (rmdCount >= Convert.ToInt32(scaleStr[i]) && rmdCount < Convert.ToInt32(scaleStr[i + 1]))
            {
                count = random.Next(Convert.ToInt32(rangeStr[i]) * 10, Convert.ToInt32(rangeStr[i + 1]) * 10);
                break;
            }
        }
        return count / 10.0;
    }

    private double GetAmount(int min, int max)
    {
        int money = random.Next(min, max);
        return money / 10.0;
    }

    static Random random = new Random();
    private double GetAmount(int min, int max, int amount, int count)
    {
        int money = 0;
        int remain = 0;
        do
        {
            money = random.Next(min, max);
            remain = amount - money;
            if (remain < min * (count - 1) || remain > max * (count - 1))
            {
                if (remain < min * (count - 1))
                {
                    max = amount - min * (count - 1);
                }
                if (remain > max * (count - 1))
                {
                    min = amount - max * (count - 1);
                }
            }
            else
            {
                break;
            }
        } while (true);
        return money / 10.0;
        //max
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}
public enum TreasureChestReponseStatus
{
    成功 = 0,
    //宝箱解锁完 = 1,
    活动结束 = 2,
    活动未开始 = 3,
    分享成功 = 4,
    /// <summary>
    /// 微信授权
    /// </summary>
    WeChatUserAuthorization = 100,
    /// <summary>
    /// 微信用户待绑定
    /// </summary>
    WeChatUserWaiting = 101,
    /// <summary>
    /// 微信用户绑定失败
    /// </summary>
    WeChatUserBindingError = 102,
    /// <summary>
    /// 该手机号已跟微信绑定
    /// </summary>
    WeChatUserMobileIsExist = 103,
    开箱成功 = 1000,
    红包侧漏 = 1001,
    没有体力 = 1002,
    红包抢完 = 1003,
    已经领过 = 1004,
    箱主开箱成功 = 1005,

    已经拥有过宝箱 = 1006,
    已经拥有过红包 = 1007,
    未找到宝箱 = -1,
    服务器异常 = -2,
    配置文件不见 = -3,
    参数错误 = -4,
    未找到红包 = -5,
    未找到活动 = -6,

    当前设备已经领取过红包 = -10,
}