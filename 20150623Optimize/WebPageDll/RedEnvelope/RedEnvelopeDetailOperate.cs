﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL;
using VAGastronomistMobileApp.SQLServerDAL.Persistence;
using VAGastronomistMobileApp.WebPageDll.Services.Infrastructure;

namespace VAGastronomistMobileApp.WebPageDll
{
    /// <summary>
    /// 红包领用记录业务逻辑
    /// </summary>
    public class RedEnvelopeDetailOperate
    {
        private readonly IRedEnvelopeDetailManager manager;
        /// <summary>
        /// 构造函数
        /// </summary>
        public RedEnvelopeDetailOperate()
        {
            manager = new RedEnvelopeDetailManager();
        }
        /// <summary>
        /// 客户端查看红包领用详情接口实现
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        //public ClientCheckRedEnvelopeDetailsResponse ClientCheckRedEnvelopeDetails(ClientCheckRedEnvelopeDetailsRequest request)
        //{
        //    ClientCheckRedEnvelopeDetailsResponse response = new ClientCheckRedEnvelopeDetailsResponse();
        //    response.type = VAMessageType.CLIENT_CHECK_REDENVELOPE_DETAIL_RESPONSE;
        //    response.cookie = request.cookie;
        //    response.uuid = request.uuid;
        //    CheckCookieAndMsgtypeInfo checkResult = Common.CheckCookieAndMsgtype(request.cookie, request.uuid, (int)request.type, (int)VAMessageType.CLIENT_CHECK_REDENVELOPE_DETAIL_REQUEST);
        //    List<ClientRedEnvelopeDetail> list = new List<ClientRedEnvelopeDetail>();
        //    if (checkResult.result == VAResult.VA_OK)
        //    {
        //        string mobilePhoneNumber = Common.ToString(checkResult.dtCustomer.Rows[0]["mobilePhoneNumber"]);
        //        CheckRedEnvelopeDetails(request.pageIndex, request.pageSize, mobilePhoneNumber, list);
        //        response.result = VAResult.VA_OK;
        //    }
        //    else
        //    {
        //        response.result = checkResult.result;
        //    }
        //    response.detailList = list;
        //    return response;
        //}
        /// <summary>
        /// 组装接口属性
        /// </summary>
        /// <param name="item"></param>
        /// <param name="usedRedEnvelope"></param>
        /// <param name="partUsedRedEnvelope"></param>
        /// <returns></returns>
        //private dynamic GetRedEnvelopeStateType(RedEnvelopeDetail item, List<long> usedRedEnvelope, List<long> partUsedRedEnvelope)
        //{
        //    string result = string.Empty;
        //    double amount = 0;
        //    double expirationTime = 0;
        //    switch (item.stateType)
        //    {
        //        case (int)VARedEnvelopeStateType.未生效:
        //            amount = item.redEnvelopeAmount;
        //            expirationTime = Common.ToSecondFrom1970(item.redEnvelopeExpirationTime);
        //            result = "未生效";
        //            break;
        //        case (int)VARedEnvelopeStateType.已生效:
        //            amount = item.redEnvelopeAmount;
        //            result = "已生效";
        //            if (usedRedEnvelope.Contains(item.redEnvelopeId))//表示当前红包信息已使用或者用了一半
        //            {
        //                if (partUsedRedEnvelope.Contains(item.redEnvelopeId))//表示当前红包被部分使用，会过期
        //                {
        //                    expirationTime = Common.ToSecondFrom1970(item.redEnvelopeExpirationTime);
        //                }
        //                else//当前红包全部使用，不会过期
        //                {
        //                    expirationTime = 0;
        //                }
        //            }
        //            else
        //            {
        //                expirationTime = Common.ToSecondFrom1970(item.redEnvelopeExpirationTime);
        //            }
        //            break;
        //        case (int)VARedEnvelopeStateType.已使用:
        //            amount = (-1) * item.usedAmount;
        //            result = "支付点单";
        //            expirationTime = 0;//不需要展示
        //            break;
        //        case (int)VARedEnvelopeStateType.已过期:
        //            amount = item.redEnvelopeAmount;
        //            expirationTime = Common.ToSecondFrom1970(item.redEnvelopeExpirationTime);
        //            result = "已过期";
        //            break;
        //        case (int)VARedEnvelopeStateType.已删除:
        //        default:
        //            amount = item.redEnvelopeAmount;
        //            expirationTime = Common.ToSecondFrom1970(item.redEnvelopeExpirationTime);
        //            result = "已删除";//预留状态
        //            break;
        //    }
        //    return new { amount = amount, result = result, expirationTime = expirationTime };
        //}
        /// <summary>
        /// 悠先服务查看红包领用详情接口实现
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        //public ZZB_ClientCheckRedEnvelopeDetailsResponse ZZB_ClientCheckRedEnvelopeDetails(ZZB_ClientCheckRedEnvelopeDetailsRequest request)
        //{
        //    ZZB_ClientCheckRedEnvelopeDetailsResponse response = new ZZB_ClientCheckRedEnvelopeDetailsResponse()
        //    {
        //        type = VAMessageType.ZZB_CLIENT_CHECK_REDENVELOPE_DETAIL_RESPONSE,
        //        cookie = request.cookie,
        //        uuid = request.uuid
        //    };
        //    CheckCookieAndMsgForZZZ checkResult = Common.CheckCookieAndMsgForZZZ(request.cookie, (int)request.type, (int)VAMessageType.ZZB_CLIENT_CHECK_REDENVELOPE_DETAIL_REQUEST);
        //    List<ClientRedEnvelopeDetail> list = new List<ClientRedEnvelopeDetail>();
        //    CustomerManager man = new CustomerManager();
        //    if (checkResult.result == VAResult.VA_OK)
        //    {
        //        CheckRedEnvelopeDetails(request.pageIndex, request.pageSize, request.mobilePhoneNumber, list);
        //        response.result = VAResult.VA_OK;
        //    }
        //    else
        //    {
        //        response.result = checkResult.result;
        //    }
        //    response.detailList = list;
        //    return response;
        //}
        //private void CheckRedEnvelopeDetails(int pageIndex, int pageSize, string mobilePhoneNumber, List<ClientRedEnvelopeDetail> list)
        //{
        //    bool falg = manager.UpdateExpirationRedEnvelopeStatus(mobilePhoneNumber, (int)VARedEnvelopeStateType.已过期);

        //    //查询用户使用的红包
        //    List<long> usedRedEnvelope = manager.GetCustomerUnusedRedEnvelope(mobilePhoneNumber, false);
        //    //查询用户部分使用的红包
        //    List<long> partUsedRedEnvelope = manager.GetCustomerUnusedRedEnvelope(mobilePhoneNumber, true);
        //    List<RedEnvelopeDetail> data = manager.GetClientRedEnvelopeDetail(pageIndex, pageSize, mobilePhoneNumber);
        //    if (data.Any())
        //    {
        //        foreach (var item in data)
        //        {
        //            dynamic dynamicData = GetRedEnvelopeStateType(item, usedRedEnvelope, partUsedRedEnvelope);
        //            list.Add(new ClientRedEnvelopeDetail()
        //            {
        //                amount = dynamicData.amount,
        //                statusDes = dynamicData.result,
        //                time = Common.ToSecondFrom1970(item.operationTime),//领用时间，可能是消费时间
        //                expirationTime = dynamicData.expirationTime//优惠券过期时间，传递给可以端0则不需要显示
        //            });
        //        }
        //    }
        //}
        /// <summary>
        /// 根据用户手机查询红包领用详情
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="mobilePhoneNumber"></param>
        /// <returns></returns>
        //public List<RedEnvelopeDetail> QueryRedEnvelopeDetail(int pageIndex, int pageSize, string mobilePhoneNumber)
        //{
        //    return manager.GetClientRedEnvelopeDetail(pageIndex, pageSize, mobilePhoneNumber);
        //}

        /// <summary>
        /// 添加红包使用详情
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        //public long AddRedEnvelopeDetail(RedEnvelopeDetail model)
        //{
        //    return manager.InsertRedEnvelopeDetail(model);
        //}

        /// <summary>
        /// 分页web view查询红包领用记录
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="phoneNum"></param>
        /// <returns></returns>
        public WebRedEnvelope GetWebRedEnvelopeDetail(int pageIndex, int pageSize, string phoneNum, string type)
        {
            var resultData = new WebRedEnvelope();
            try
            {
                //if (pageIndex == 1)
                //{
                //    double executedRedEnvelopeAmount = 0;
                //    if (!DoExpirationRedEnvelopeLogic(phoneNum, ref  executedRedEnvelopeAmount))
                //    {
                //        return new WebRedEnvelope()
                //        {
                //            detailList = new List<WebRedEnvelopeDetailViewModel>(),
                //            isHaveMore = false
                //        };
                //    }
                //}

                //var resultDataList = new List<WebRedEnvelopeDetailViewModel>();
                bool isHaveMore = false;
                List<WebRedEnvelopeDetailModel> initDateList = manager.GetWebRedEnvelopeDetail(pageIndex, pageSize, phoneNum, type, ref  isHaveMore);
                if (initDateList != null && initDateList.Any())
                {
                    List<WebRedEnvelopeDetailViewModel> viewModelList = new List<WebRedEnvelopeDetailViewModel>();

                    foreach (WebRedEnvelopeDetailModel model in initDateList)
                    {
                        WebRedEnvelopeDetailViewModel viewModel = new WebRedEnvelopeDetailViewModel();
                        viewModel.amount = Math.Round(model.amount, 2);                        
                        viewModel.expireTime = model.expireTime.ToString("yyyy/MM/dd");
                        viewModel.statusType = model.statusType;
                        if (model.statusType == (int)VARedEnvelopeStateType.已作废)
                        {
                            viewModel.usedAmount = 0;
                        }
                        else
                        {
                            viewModel.usedAmount = Math.Round(model.usedAmount, 2);
                        }


                        //----------------------------------------------------------------------------------

                        if ((model.effectTime - DateTime.Now).TotalDays / 365 > 50)
                        {
                            viewModel.effectTime = "inactive";
                            viewModel.statusType = (int)VARedEnvelopeStateType.未激活;
                        }
                        else
                        {
                            viewModel.effectTime = model.effectTime.ToString("yyyy/MM/dd");
                        }

                        //----------------------------------------------------------------------------------


                        viewModelList.Add(viewModel);
                    }

                    resultData.detailList = viewModelList;
                    resultData.isHaveMore = isHaveMore;
                }

                #region
                //TreasureChestOperate treasureChestOper = new TreasureChestOperate();
                //if (initDateList.Any())
                //{
                //foreach (var item in initDateList)
                //{
                //    var model = new WebRedEnvelopeDetailViewModel();
                //    model.statusType = item.stateType;
                //    switch (item.stateType)
                //    {
                //        case (int)VARedEnvelopeStateType.未生效:
                //            model.amount = Common.ToDouble(item.redEnvelopeAmount);
                //            model.fromDescription = "";// item.phone2 == item.phone1 ? "我的宝箱" : item.phone2.Substring(0, 3) + "xxxx" + item.phone2.Substring(7, 4) + "的宝箱";
                //            model.description = Common.ToDateTime(item.redEnvelopeEffectiveBeginTime) <= DateTime.MinValue ? "未生效" : item.redEnvelopeEffectiveBeginTime.ToString() + "生效";//未完全解锁的宝箱，回给前端未解锁数，宝箱编号
                //            model.time = item.operationTime.ToString();//领用时间
                //            break;
                //        case (int)VARedEnvelopeStateType.已生效:
                //            model.amount = Common.ToDouble(item.redEnvelopeAmount);
                //            model.fromDescription = "";//item.phone2 == item.phone1 ? "我的宝箱" : item.phone2.Substring(0, 3) + "xxxx" + item.phone2.Substring(7, 4) + "的宝箱";
                //            model.time = item.operationTime.ToString();//领用时间
                //            if (item.flag == 1)//全部已使用，flag属性只有在此type下才有效
                //            {
                //                model.description = "已使用";
                //            }
                //            else//部分使用，或者未使用
                //            {
                //                model.description = item.redEnvelopeExpirationTime + "到期";
                //            }
                //            break;
                //        case (int)VARedEnvelopeStateType.已使用:
                //            model.amount = (-1) * Common.ToDouble(item.usedAmount);//使用金额
                //            model.fromDescription = "";
                //            model.description = item.shopName + "已使用";
                //            model.time = item.operationTime.ToString();//使用时间
                //            break;
                //        case (int)VARedEnvelopeStateType.已过期:
                //            model.amount = (-1) * Common.ToDouble(item.usedAmount);//过期金额//待处理
                //            model.fromDescription = "";
                //            model.time = item.operationTime.ToString();//过期时间
                //            model.description = "红包过期";
                //            break;
                //        case (int)VARedEnvelopeStateType.已作废:
                //            model.amount = Common.ToDouble(item.redEnvelopeAmount);
                //            model.fromDescription = "";
                //            model.time = item.operationTime.ToString();
                //            model.description = "红包作废";
                //            break;
                //        //case (int)VARedEnvelopeStateType.红包满:
                //        //    model.amount = Common.ToDouble(item.redEnvelopeAmount);
                //        //    model.fromDescription = item.phone2 == item.phone1 ? "我的宝箱" : (String.IsNullOrEmpty(item.phone2) ? "来自匿名好友" : item.phone2.Substring(0, 3) + "xxxx" + item.phone2.Substring(7, 4));
                //        //    model.description = "已失效";
                //        //    model.time = item.operationTime.ToString();
                //        //    break;
                //        case (int)VARedEnvelopeStateType.已删除://预留
                //        default:
                //            break;
                //    }
                //    resultDataList.Add(model);
                //}
                //}
                #endregion
                return resultData;
            }
            catch
            {
                return resultData;
            }
        }
        /// <summary>
        /// 处理：1.插入用户过期红包详情；2.修改用户红包余额；3.处理用户红包状态
        /// </summary>
        /// <param name="phoneNum">用户手机号码</param>
        /// <param name="executedRedEnvelopeAmount">有效红包金额</param>
        /// <returns>true表示处理成功；false表示处理失败</returns>
        //public bool DoExpirationRedEnvelopeLogic(string phoneNum, ref double executedRedEnvelopeAmount)
        //{
        //    try
        //    {
        //        if (String.IsNullOrWhiteSpace(phoneNum))
        //        {
        //            return true;
        //        }
        //        //处理生效红包操作
        //        var notEffectiveRedEnvelopes = manager.GetNotEffectiveRedEnvelope(phoneNum);//查询所有未生效红包
        //        if (notEffectiveRedEnvelopes.Any())
        //        {
        //            string rIds = CommonPageOperate.SplicingListStr(notEffectiveRedEnvelopes, "redEnvelopeId");
        //            double rAmount = Common.ToDouble(notEffectiveRedEnvelopes.Sum(r => r.Amount));//未生效金额
        //            using (var ts = new TransactionScope())
        //            {
        //                bool flag1 = manager.UpdateNotEffectiveRedEnvelopeAndDetail(rIds);//修改红包记录表和红包领用详情记录表
        //                bool flag2 = manager.AddCustomerRedEnvelopeAmount(phoneNum, rAmount) > 0;//未生效减钱//已生效加钱
        //                if (flag1 && flag2)
        //                {
        //                    ts.Complete();
        //                }
        //                else
        //                {
        //                    return false;
        //                }
        //            }
        //        }

        //        //处理过期红包操作
        //        double expirationEffectRedEnvelopeAmount = Common.ToDouble(GetCustomerExpirationRedEnvelopeAmount(phoneNum, true));//过期有效红包金额
        //        double expirationNotEffectRedEnvelopeAmount = Common.ToDouble(GetCustomerExpirationRedEnvelopeAmount(phoneNum, false));//过期无效红包金额
        //        bool flag = true;
        //        //需要先处理处理红包过期的业务，下面在查询进行逻辑处理
        //        using (TransactionScope ts = new TransactionScope())
        //        {
        //            if (Common.ToDouble(expirationEffectRedEnvelopeAmount + expirationNotEffectRedEnvelopeAmount) > 0)//有过期金额，有必要处理数据
        //            {
        //                bool flag1 = manager.UpdateCustomerRedEnvelope(phoneNum, (-1) * expirationEffectRedEnvelopeAmount, (-1) * expirationNotEffectRedEnvelopeAmount);//更新用户余额
        //                //bool flag2 = manager.InsertRedEnvelopeDetail_1(new RedEnvelopeDetail()//插入过期金额记录
        //                //  {
        //                //      mobilePhoneNumber = phoneNum,
        //                //      operationTime = DateTime.Now,
        //                //      stateType = (int)VARedEnvelopeStateType.已过期,
        //                //      usedAmount = expirationEffectRedEnvelopeAmount + expirationNotEffectRedEnvelopeAmount
        //                //  }) > 0;
        //                bool flag3 = manager.DoUpdateExpirationRedEnvelope(phoneNum);//处理过期红包
        //                if (flag1 && flag3)
        //                {
        //                    flag = true;
        //                    ts.Complete();
        //                }
        //                else
        //                {
        //                    flag = false;
        //                }
        //            }
        //        }
        //        if (!flag)
        //        {
        //            return false;
        //        }
        //        double[] redEnvelope = GetCustomerRedEnvelope(phoneNum);
        //        executedRedEnvelopeAmount = Common.ToDouble(redEnvelope[0]);
        //        return true;
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}

        /// <summary>
        /// 查看当前手机号码的红包总额
        /// </summary>
        /// <param name="mobilePhoneNumber"></param>
        /// <returns></returns>
        //public double[] GetCustomerRedEnvelope(string mobilePhoneNumber)
        //{
        //    bool isMobileVerified = CustomerOperate.IsMobileVerified(mobilePhoneNumber);
        //    if (isMobileVerified)
        //    {
        //        return manager.GetCustomerRedEnvelope(mobilePhoneNumber);
        //    }
        //    else
        //    {
        //        return manager.GetCustomerRedEnvelope(mobilePhoneNumber, true);
        //    }
        //}
        /// <summary>
        /// 查询当前用户过期红包的金额
        /// </summary>
        /// <param name="mobilePhoneNumber"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        public double GetCustomerExpirationRedEnvelopeAmount(string mobilePhoneNumber, bool flag)
        {
            return manager.GetCustomerExpirationRedEnvelopeAmount(mobilePhoneNumber, flag);
        }
    }
}
