using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Autofac;
using Autofac.Integration.Web;
using CloudStorage;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL;
using VAGastronomistMobileApp.SQLServerDAL.Persistence;
using VAGastronomistMobileApp.TheThirdPartyPaymentDll;
using System.Transactions;
using System.Configuration;
using System.Threading;
using System.Web;
using System.Xml;
using System.IO;
//
//  Copyright 2011 View Alloc inc. All rights reserved.
//  Created by Jason Xiao on 2012-04-10.
//
using VAGastronomistMobileApp.WebPageDll.Services;
using VAGastronomistMobileApp.WebPageDll.Services.Infrastructure;
using VA.Cache.HttpRuntime;
using VA.CacheLogic;
using VAGastronomistMobileApp.Model.QueryObject;
using System.Text.RegularExpressions;

namespace VAGastronomistMobileApp.WebPageDll
{
    /// <summary>
    /// 用户相关操作类
    /// </summary>
    /// <returns></returns>
    public class CustomerOperate
    {
        private readonly CustomerManager customerMan = new CustomerManager();
        /// <summary>
        /// 查询用户信息
        /// </summary>
        /// <returns></returns>
        //public DataTable QueryCustomer()
        //{
        //    CustomerManager customerMan = new CustomerManager();
        //    DataTable dtCustomer = customerMan.SelectCustomer();//查询用户信息
        //    DataView dvCustomer = dtCustomer.DefaultView;
        //    dvCustomer.Sort = "RegisterDate ASC";
        //    return dvCustomer.ToTable();
        //}
        /// <summary>
        ///  通过菜均价查询用户信息
        /// </summary>
        /// <returns></returns>
        public DataTable QueryCustomerByDishAverageAmount(double dishAverageAmount)
        {
            CustomerManager customerMan = new CustomerManager();
            DataTable dtCustomer = customerMan.SelectCustomerByDishAverageAmount(dishAverageAmount);
            return dtCustomer;
        }
        /// <summary>
        ///  通过回头次数查询用户信息
        /// </summary>
        /// <returns></returns>
        public DataTable QueryCustomerByBackShopCountAndDate(DateTime strDate, DateTime endDate, int companyId, int returnCount)
        {
            CustomerManager customerMan = new CustomerManager();
            DataTable dtCustomer = customerMan.SelectCustomerByBackShopCountAndDate(strDate, endDate, companyId, returnCount);
            return dtCustomer;
        }
        /// <summary>
        /// 根据用户编号查询对应用户信息
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public CustomerInfo QueryCustomer(long customerID)
        {
            CustomerManager customerMan = new CustomerManager();
            DataTable dtCustomer = customerMan.SelectCustomer(customerID);
            DataView dvCustomer = dtCustomer.DefaultView;
            dvCustomer.RowFilter = "CustomerID = '" + customerID + "'";
            CustomerInfo customer = new CustomerInfo();
            if (1 == dvCustomer.Count)
            {
                customer.CustomerID = Common.ToInt64(dvCustomer[0]["CustomerID"]);
                customer.UserName = Common.ToString(dvCustomer[0]["UserName"]);
                customer.Password = Common.ToString(dvCustomer[0]["Password"]);
                customer.CustomerFirstName = Common.ToString(dvCustomer[0]["CustomerFirstName"]);
                customer.CustomerLastName = Common.ToString(dvCustomer[0]["CustomerLastName"]);
                customer.CustomerSex = Common.ToInt32(dvCustomer[0]["CustomerSex"]);
                customer.CustomerRankID = Common.ToInt32(dvCustomer[0]["CustomerRankID"]);
                customer.RegisterDate = Common.ToDateTime(dvCustomer[0]["RegisterDate"]);
                customer.CustomerBirthday = Common.ToDateTime(dvCustomer[0]["CustomerBirthday"]);
                customer.mobilePhoneNumber = Common.ToString(dvCustomer[0]["mobilePhoneNumber"]);
                customer.CustomerAddress = Common.ToString(dvCustomer[0]["CustomerAddress"]);
                customer.customerEmail = Common.ToString(dvCustomer[0]["customerEmail"]);
                customer.cookie = Common.ToString(dvCustomer[0]["cookie"]);
            }
            else
            {
                customer = null;
            }
            return customer;
        }
        /// <summary>
        /// 查询用户等级信息
        /// </summary>
        /// <returns></returns>
        public DataTable QueryCustomerRank()
        {
            CustomerManager customerMan = new CustomerManager();
            DataTable dtCustomerRank = customerMan.QueryCustomerRank();//查询用户等级信息
            DataView dvCustomerRank = dtCustomerRank.DefaultView;
            dvCustomerRank.Sort = "CustomerRankSequence ASC";
            return dvCustomerRank.ToTable();
        }
        /// <summary>
        /// 查询用户等级及详细折扣分类信息
        /// </summary>
        /// <returns></returns>
        public DataTable QueryCustomerRankDetial()
        {
            CustomerManager customerMan = new CustomerManager();
            DataTable dtCustomerRankConn = customerMan.QueryCustomerRankDetail();//查询用户等级以及相应的折扣分类的信息
            DataView dvCustomerRankConn = dtCustomerRankConn.DefaultView;
            dvCustomerRankConn.Sort = "CustomerRankName ASC, DiscountTypeName ASC";
            return dvCustomerRankConn.ToTable();
        }
        /// <summary>
        /// 新增用户信息
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        //public bool AddCustomer(CustomerInfo customer)
        //{
        //    CustomerManager customerMan = new CustomerManager();
        //    DataTable dtCustomer = customerMan.SelectCustomer();
        //    DataView dvCustomer = dtCustomer.DefaultView;
        //    dvCustomer.RowFilter = "UserName = '" + customer.UserName + "'";
        //    if (dvCustomer.Count > 0 || "" == customer.CustomerFirstName || null == customer.CustomerFirstName
        //        || "" == customer.CustomerLastName || null == customer.CustomerLastName)
        //    {//如果所加用户信息的姓或名为空，或者用户信息表中已有该用户名的用户，则直接返回false
        //        return false;
        //    }
        //    else
        //    {
        //        if (customerMan.InsertCustomer(customer) > 0)
        //        {//插入数据库表成功则返回ture
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //}
        /// <summary>
        /// 新增用户等级信息
        /// </summary>
        /// <param name="customerRank"></param>
        /// <returns></returns>
        public bool AddCustomerRank(CustomerRank customerRank)
        {
            CustomerManager customerMan = new CustomerManager();
            DataTable dtCustomerRank = customerMan.QueryCustomerRank();
            DataView dvCustomerRank = dtCustomerRank.DefaultView;
            dvCustomerRank.RowFilter = "CustomerRankName = '" + customerRank.CustomerRankName + "'";
            if (dvCustomerRank.Count > 0 || customerRank.CustomerRankName == "" || customerRank.CustomerRankName == null)
            {//如果所加用户等级信息的名称为空，或者餐桌信息表中已有该名称的餐桌，则直接返回false
                return false;
            }
            else
            {
                //if (customerMan.InsertCustomerRank(customerRank) > 0)
                //{//插入数据库表成功则返回ture
                //    return true;
                //}
                //else
                //{
                //    return false;
                //}
                return false;
            }
        }
        /// <summary>
        /// 新增用户等级与折扣关系详细信息
        /// </summary>
        /// <param name="rankConnDis"></param>
        /// <returns></returns>
        public bool AddRankConnDis(RankConnDiscount rankConnDis)
        {
            CustomerManager customerMan = new CustomerManager();
            DataTable dtCustomerRank = customerMan.QueryCustomerRankDetail();
            DataView dvCustomerRank = dtCustomerRank.DefaultView;
            dvCustomerRank.RowFilter = "CustomerRankID = '" + rankConnDis.CustomerRankID +
                "' and DiscountTypeID = '" + rankConnDis.DiscountTypeID + "'";
            if (dvCustomerRank.Count > 0 || rankConnDis.DiscountValue.Equals(null))//如果所加用户等级与折扣关系详细信息的折扣值为空，
            {//或者RankConnDiscount信息表中已有相同信息（同一CustomerRankID和同一DiscountTypeID），则直接返回false
                return false;
            }
            else
            {
                //if (customerMan.InsertRankConnDis(rankConnDis) > 0)
                //{//插入数据库表成功则返回ture
                //    return true;
                //}
                //else
                //{
                //    return false;
                //}
                return false;
            }
        }
        /// <summary>
        /// 删除用户信息
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public bool RemoveCustomer(long customerID)
        {
            CustomerManager customerMan = new CustomerManager();
            DataTable dtCustomer = customerMan.SelectCustomer();
            DataView dvCustomer = dtCustomer.DefaultView;
            dvCustomer.RowFilter = "CustomerID = '" + customerID + "'";
            if (1 == dvCustomer.Count)
            {//判断此customerID是否存在，是则删除
                if (customerMan.DeleteCustomer(customerID))
                {//删除成功则返回true
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 删除用户等级信息
        /// </summary>
        /// <param name="customerRankID"></param>
        /// <returns></returns>
        public bool RemoveCustomerRank(int customerRankID)
        {
            CustomerManager customerMan = new CustomerManager();
            DataTable dtCustomerRank = customerMan.QueryCustomerRank();
            DataView dvCustomerRank = dtCustomerRank.DefaultView;
            dvCustomerRank.RowFilter = "CustomerRankID = '" + customerRankID + "'";
            if (dvCustomerRank.Count == 1)
            {//判断此CustomerRankID是否存在，是则删除
                if (customerMan.DeleteCustomerRankByID(customerRankID))
                {//删除成功则返回true
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 删除用户等级与折扣关系详细信息
        /// </summary>
        /// <param name="rankConnDisID"></param>
        /// <returns></returns>
        public bool RemoveRankConnDis(int rankConnDisID)
        {
            CustomerManager customerMan = new CustomerManager();
            DataTable dtCustomerRank = customerMan.QueryCustomerRankDetail();
            DataView dvCustomerRank = dtCustomerRank.DefaultView;
            dvCustomerRank.RowFilter = "RankConnDisID = '" + rankConnDisID + "'";
            if (dvCustomerRank.Count == 1)
            {//判断此rankConnDisID是否存在，是则删除
                if (customerMan.DeleteRankConnDisByID(rankConnDisID))
                {//删除成功则返回true
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 修改用户基本信息
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public bool ModifyCustomerBaseInfo(CustomerInfo customer)
        {
            CustomerManager customerMan = new CustomerManager();
            DataTable dtCustomer = customerMan.SelectCustomer();
            DataView dvCustomer = dtCustomer.DefaultView;
            dvCustomer.RowFilter = "CustomerID = '" + customer.CustomerID + "'";//判断此ID存在
            if (1 == dvCustomer.Count)
            {//判断此EmployeeID是否存在，是则修改
                if (customerMan.UpdateCustomerBaseInfo(customer))
                {//修改成功则返回true
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 修改用户登录密码
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool ModifyCustomerPassword(long customerID, string password)
        {
            CustomerManager customerMan = new CustomerManager();
            DataTable dtCustomer = customerMan.SelectCustomer();
            DataView dvCustomer = dtCustomer.DefaultView;
            dvCustomer.RowFilter = "CustomerID = '" + customerID + "'";
            if (1 == dvCustomer.Count)
            {//判断此CustomerID是否存在，是则修改
                using (TransactionScope scope = new TransactionScope())
                {
                    if (customerMan.UpdateCustomerPassword(customerID, password))
                    {//修改成功则返回true
                        scope.Complete();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 修改用户等级信息
        /// </summary>
        /// <param name="customerRank"></param>
        /// <returns></returns>
        public bool ModifyCustomerRank(CustomerRank customerRank)
        {
            CustomerManager customerMan = new CustomerManager();
            DataTable dtCustomerRank = customerMan.QueryCustomerRank();
            DataTable dtCustomerRankCopy = dtCustomerRank.Copy();
            DataView dvCustomerRank = dtCustomerRank.DefaultView;
            DataView dvCustomerRankCopy = dtCustomerRankCopy.DefaultView;
            dvCustomerRank.RowFilter = "CustomerRankID = '" + customerRank.CustomerRankID + "'";
            dvCustomerRankCopy.RowFilter = "CustomerRankID <> '" + customerRank.CustomerRankID
                + "' and CustomerRankName = '" + customerRank.CustomerRankName + "'";
            if (dvCustomerRank.Count == 1 && 0 == dvCustomerRankCopy.Count)
            {//判断此tableID是否存在，是则修改
                if (customerMan.UpdateCustomerRank(customerRank))
                {//修改成功则返回true
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 修改用户等级与折扣关系详细信息
        /// </summary>
        /// <param name="rankConnDis"></param>
        /// <returns></returns>
        public bool ModifyRankConnDis(RankConnDiscount rankConnDis)
        {
            CustomerManager customerMan = new CustomerManager();
            DataTable dtCustomerRank = customerMan.QueryCustomerRankDetail();
            DataView dvCustomerRank = dtCustomerRank.DefaultView;
            dvCustomerRank.RowFilter = "RankConnDisID = '" + rankConnDis.RankConnDisID + "'";
            if (dvCustomerRank.Count == 1)
            {//判断此RankConnDisID是否存在，是则修改
                if (customerMan.UpdateRankConnDis(rankConnDis))
                {//修改成功则返回true
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 客户端设备注册20140313
        /// </summary>
        /// <param name="clientRegisterRequest"></param>
        /// <returns></returns>
        public VAClientRegisterResponse ClientRegister(VAClientRegisterRequest clientRegisterRequest)
        {
            VAClientRegisterResponse clientRegisterResponse = new VAClientRegisterResponse();
            clientRegisterResponse.type = VAMessageType.CLIENT_REGISTER_RESPONSE;
            clientRegisterResponse.cookie = clientRegisterRequest.cookie;
            clientRegisterResponse.uuid = clientRegisterRequest.uuid;
            if (clientRegisterRequest.type == VAMessageType.CLIENT_REGISTER_REQUEST)
            {
                if (string.IsNullOrEmpty(clientRegisterRequest.uuid))
                {
                    clientRegisterResponse.result = VAResult.VA_FAILED_UUID_NULL;
                }
                else
                {
                    clientRegisterResponse.uuid = clientRegisterRequest.uuid;

                    string pushToken = "";
                    long newCustomerId = 0;
                    long newDeviceId = 0;
                    CustomerInfo newCustomer = new CustomerInfo()
                    {
                        RegisterDate = DateTime.Now,
                        CustomerStatus = 1,
                        cookie = Common.ToString(Guid.NewGuid()) + Common.ToString(DateTime.Now.Ticks),
                        registerCityId = clientRegisterRequest.cityId,
                        wechatId = !string.IsNullOrEmpty(clientRegisterRequest.wechatId) ? clientRegisterRequest.wechatId : ""
                    };
                    CustomerManager customerMan = new CustomerManager();
                    using (TransactionScope scope = new TransactionScope())
                    {
                        newCustomerId = customerMan.InsertCustomer(newCustomer);
                        if (newCustomerId > 0)
                        {
                            if (!string.IsNullOrEmpty(clientRegisterRequest.pushToken))
                            {
                                pushToken = clientRegisterRequest.pushToken;//避免不允许发推送的用户传来的空推送码(null)
                            }
                            //需要考虑前一次注册已经插入了设备信息，但是客户端由于某种原因没收到回复而失败，重新注册，此时设备表已经有这个uuid了
                            DataTable dtDevice = customerMan.SelectDevice(clientRegisterRequest.uuid);
                            bool updateDeviceToken = true;
                            if (dtDevice.Rows.Count == 1)
                            {
                                if ((!dtDevice.Rows[0]["pushToken"].Equals(pushToken) && !string.IsNullOrEmpty(pushToken)) || (!dtDevice.Rows[0]["appBuild"].Equals(clientRegisterRequest.clientBuild) && !string.IsNullOrEmpty(clientRegisterRequest.clientBuild)))
                                {
                                    updateDeviceToken = customerMan.UpdateDeviceToken(clientRegisterRequest.uuid, pushToken, clientRegisterRequest.clientBuild);
                                }
                            }
                            else
                            {
                                DeviceInfo newDevice = new DeviceInfo()
                                {
                                    uuid = clientRegisterRequest.uuid,
                                    pushToken = pushToken,
                                    updateTime = DateTime.Now,
                                    type = clientRegisterRequest.appType
                                };
                                newDeviceId = customerMan.InsertDevice(newDevice);
                            }
                            if (newDeviceId > 0 || updateDeviceToken)
                            {
                                CustomerConnDevice newCustomerConnDevice = new CustomerConnDevice();
                                newCustomerConnDevice.customerId = newCustomerId;
                                newCustomerConnDevice.deviceId = dtDevice.Rows.Count == 1 ? Common.ToInt64(dtDevice.Rows[0]["deviceId"]) : newDeviceId;
                                newCustomerConnDevice.updateTime = DateTime.Now;
                                if (customerMan.InsertCustomerConnDevice(newCustomerConnDevice) > 0 && customerMan.UpdateCustomerPartInfo(0, 0, 1, newCustomerId))
                                {
                                    clientRegisterResponse.eCardNumber = GetEvipCard(newCustomerId);//回给Android端用于推送的token
                                    clientRegisterResponse.result = VAResult.VA_OK;
                                    clientRegisterResponse.cookie = newCustomer.cookie;
                                    scope.Complete();
                                }
                                else
                                {
                                    clientRegisterResponse.result = VAResult.VA_FAILED_DB_ERROR;
                                }
                            }
                            else
                            {
                                clientRegisterResponse.result = VAResult.VA_FAILED_DB_ERROR;
                            }
                        }
                        else
                        {
                            clientRegisterResponse.result = VAResult.VA_FAILED_DB_ERROR;
                        }
                    }
                    if (clientRegisterResponse.result == VAResult.VA_OK)
                    {
                        #region 调用激励模块
                        //if (SystemConfigManager.IsDeviceCheck() && newDeviceId > 0)
                        //{
                        //    VAEncourageDetail encourageDetail = new VAEncourageDetail();
                        //    encourageDetail.appType = clientRegisterRequest.appType;
                        //    encourageDetail.customerId = newCustomerId;
                        //    encourageDetail.messageType = VAMessageType.CLIENT_REGISTER_REQUEST;
                        //    encourageDetail.pushToken = pushToken;
                        //    Thread encourageThread = new Thread(Common.Encourage);
                        //    encourageThread.Start((object)encourageDetail);
                        //}
                        #endregion
                    }
                }
            }
            else
            {
                clientRegisterResponse.result = VAResult.VA_FAILED_TYPE_ERROR;
            }
            #region 用户注册日志记录
            Thread thread = new Thread(Excuit);
            thread.IsBackground = true;
            thread.Start(VAInvokedAPIType.API_USERS_REGISTER);
            #endregion
            return clientRegisterResponse;
        }
        /// <summary>
        /// 20140313
        /// </summary>
        /// <param name="value"></param>
        public void Excuit(object value)
        {
            StatisticalStatementOperate sso = new StatisticalStatementOperate();
            sso.InsertInvokedAPILog((int)value);
        }
        /// <summary>
        /// 客户端证书登录20140313
        /// </summary>
        /// <param name="clientCookieLoginRequest"></param>
        /// <returns></returns>
        public VAClientCookieLoginResponse ClientCookieLogin(VAClientCookieLoginRequest clientCookieLoginRequest)
        {
            VAClientCookieLoginResponse clientCookieLoginResponse = new VAClientCookieLoginResponse();
            clientCookieLoginResponse.type = VAMessageType.CLIENT_COOKIE_LOGIN_RESPONSE;
            clientCookieLoginResponse.cookie = clientCookieLoginRequest.cookie;
            clientCookieLoginResponse.uuid = clientCookieLoginRequest.uuid;
            CheckCookieAndMsgtypeInfo checkResult = Common.CheckCookieAndMsgtype(clientCookieLoginRequest.cookie, clientCookieLoginRequest.uuid, (int)clientCookieLoginRequest.type, (int)VAMessageType.CLIENT_COOKIE_LOGIN_REQUEST, false);
            SystemConfigCacheLogic systemConfigCacheLogic = new SystemConfigCacheLogic();
            if (checkResult.result == VAResult.VA_OK)
            {
                clientCookieLoginResponse.result = VAResult.VA_OK;
                VAAppType currentUserAppType = (VAAppType)Common.ToInt32(checkResult.dtCustomer.Rows[0]["appType"]);

                DataTable dtLatestBuild = systemConfigCacheLogic.GetAppLatestBuild(currentUserAppType);
                if (dtLatestBuild.Rows.Count == 1)
                {
                    clientCookieLoginResponse.latestBuild = Common.ToString(dtLatestBuild.Rows[0]["latestBuild"]);
                    clientCookieLoginResponse.latestUpdateDescription = Common.ToString(dtLatestBuild.Rows[0]["latestUpdateDescription"]);
                    clientCookieLoginResponse.latestUpdateUrl = Common.ToString(dtLatestBuild.Rows[0]["latestUpdateUrl"]);
                    string oldBuildSupport = Common.ToString(dtLatestBuild.Rows[0]["oldBuildSupport"]);
                    string clientBuild = "";
                    if (!string.IsNullOrEmpty(clientCookieLoginRequest.clientBuild))
                    {
                        clientBuild = clientCookieLoginRequest.clientBuild;
                    }
                    if (string.Compare(clientBuild, oldBuildSupport) == -1)
                    {
                        clientCookieLoginResponse.forceUpdate = true;
                    }
                    else
                    {
                        clientCookieLoginResponse.forceUpdate = false;
                    }
                }
                else
                {
                    clientCookieLoginResponse.latestBuild = "";
                    clientCookieLoginResponse.latestUpdateDescription = "";
                    clientCookieLoginResponse.latestUpdateUrl = "";
                    clientCookieLoginResponse.forceUpdate = false;
                }
                long customerId = Common.ToInt64(checkResult.dtCustomer.Rows[0]["CustomerID"]);
                long deviceId = Common.ToInt64(checkResult.dtCustomer.Rows[0]["deviceId"]);
                using (TransactionScope scope = new TransactionScope())
                {
                    customerMan.UpdateCustomerConnDeviceTime(customerId, deviceId);
                    clientCookieLoginResponse.userInfo = GetUserInfo(checkResult.dtCustomer, clientCookieLoginRequest.uuid, clientCookieLoginRequest.pushToken, clientCookieLoginRequest.cityId, currentUserAppType, clientCookieLoginRequest.clientBuild);
                    scope.Complete();
                }
                #region 查询客户端所需图片处理参数 2014-5-30
                ClientImageSizeOperate clientImageSizeOperate = new ClientImageSizeOperate();
                clientCookieLoginResponse.clientImageSize = clientImageSizeOperate.QueryClientImageSize(clientCookieLoginRequest.screenWith, (int)currentUserAppType, clientCookieLoginRequest.clientBuild);
                #endregion
                #region 调用激励模块
                //VAEncourageDetail encourageDetail = new VAEncourageDetail();
                //encourageDetail.appType = currentUserAppType;
                //encourageDetail.customerId = Common.ToInt64(checkResult.dtCustomer.Rows[0]["CustomerID"]);
                //encourageDetail.messageType = VAMessageType.CLIENT_COOKIE_LOGIN_REQUEST;
                //encourageDetail.pushToken = Common.ToString(checkResult.dtCustomer.Rows[0]["pushToken"]);
                //Thread encourageThread = new Thread(Common.Encourage);
                //encourageThread.Start((object)encourageDetail);
                #endregion
            }
            else
            {
                clientCookieLoginResponse.result = checkResult.result;
            }

            #region 用户登陆日志记录
            Thread thread = new Thread(Excuit);
            thread.IsBackground = true;
            thread.Start(VAInvokedAPIType.API_USERS_LOGIN);
            #endregion

            int scaleType = 0;
            if (clientCookieLoginRequest.screenWith == "641" || clientCookieLoginRequest.screenWith == "320")
            {
                scaleType = (int)ClientStartImgScaleType.三比二;
            }
            else
            {
                scaleType = (int)ClientStartImgScaleType.十六比九;
            }
            var list = new ClientStartImgConfigOperate().GetClientStartImgConfigInfos((int)ClientStartImg.图片);
            if (list.Any())
            {
                clientCookieLoginResponse.startMapUrlList = (from q in list
                                                             where q.AppType == (int)clientCookieLoginRequest.appType && q.ScaleType == scaleType
                                                             orderby q.Sequence descending
                                                             select WebConfig.CdnDomain + WebConfig.ImagePath + "Client/" + q.ImgUrl).ToList();
            }
            else
            {
                clientCookieLoginResponse.startMapUrlList = new List<string>();
            }
            clientCookieLoginResponse.dishSortAlgorithmBase = systemConfigCacheLogic.ClientDishSortAlgorithmBaseOfCache();
            return clientCookieLoginResponse;
        }
        /// <summary>
        /// 客户端用户基本信息更新new 20140313
        /// 暂时只修改昵称和性别,wangcheng添加修改个人图片信息
        /// </summary>
        /// <param name="clientUpdateUserInfoNewRequest"></param>
        /// <returns></returns>
        public VAClientUpdateUserInfoNewResponse ClientUpdateUserInfoNew(VAClientUpdateUserInfoNewRequest clientUpdateUserInfoNewRequest)
        {
            VAClientUpdateUserInfoNewResponse clientUpdateUserInfoNewResponse = new VAClientUpdateUserInfoNewResponse();
            clientUpdateUserInfoNewResponse.type = VAMessageType.USER_INFO_MODIFYNEW_RESPONSE;
            clientUpdateUserInfoNewResponse.cookie = clientUpdateUserInfoNewRequest.cookie;
            clientUpdateUserInfoNewResponse.uuid = clientUpdateUserInfoNewRequest.uuid;
            CheckCookieAndMsgtypeInfo checkResult = Common.CheckCookieAndMsgtype(clientUpdateUserInfoNewRequest.cookie,
                clientUpdateUserInfoNewRequest.uuid, (int)clientUpdateUserInfoNewRequest.type, (int)VAMessageType.USER_INFO_MODIFYNEW_REQUEST);
            if (checkResult.result == VAResult.VA_OK)
            {
                DataRow checkResult_dtCustomer_Rows = checkResult.dtCustomer.Rows[0];
                CustomerInfo customer = new CustomerInfo();
                customer.cookie = clientUpdateUserInfoNewRequest.cookie;
                customer.CustomerSex = clientUpdateUserInfoNewRequest.customerSex;
                bool falg = false;
                if (string.IsNullOrEmpty(clientUpdateUserInfoNewRequest.personalImgInfo))
                {
                    customer.personalImgInfo = "";
                }
                else
                {
                    string pictureContent = clientUpdateUserInfoNewRequest.personalImgInfo.ToString();
                    customer.personalImgInfo = pictureContent;
                }
                customer.UserName = string.IsNullOrEmpty(clientUpdateUserInfoNewRequest.displayName) ? "" : clientUpdateUserInfoNewRequest.displayName;
                customer.defaultPayment = Common.ToInt32(clientUpdateUserInfoNewRequest.defaultPayment);//客户端传递默认支付方式
                using (TransactionScope scope = new TransactionScope())
                {
                    if (!string.IsNullOrWhiteSpace(customer.personalImgInfo) && customer.personalImgInfo.ToLower().IndexOf("http") == -1)
                    {
                        ClientExtendOperate.ClientModifyPersonalImg(checkResult_dtCustomer_Rows, customer.personalImgInfo);
                    }
                    bool dbResult = falg ? customerMan.ClientUpdateCustomerInfo(customer) : customerMan.UpdateCustomerInfo(customer);
                    if (dbResult)
                    {
                        scope.Complete();
                        clientUpdateUserInfoNewResponse.result = VAResult.VA_OK;
                    }
                    else
                    {
                        clientUpdateUserInfoNewResponse.result = VAResult.VA_FAILED_DB_ERROR;
                    }
                }

                #region 微信用户绑定

                if (clientUpdateUserInfoNewResponse.result == VAResult.VA_OK && clientUpdateUserInfoNewRequest.bindingWeChat == 1)
                {
                    long customerId = Convert.ToInt64(checkResult.dtCustomer.Rows[0]["CustomerID"]);

                    string strHeadImgUrl = null;
                    string headImgUrl = "";
                    int? headImgSize = 0;
                    if (!string.IsNullOrEmpty(clientUpdateUserInfoNewRequest.headImg))
                    {
                        strHeadImgUrl = clientUpdateUserInfoNewRequest.headImg;
                        headImgUrl = strHeadImgUrl;
                        headImgSize = int.Parse(strHeadImgUrl.Substring(strHeadImgUrl.LastIndexOf('/') + 1));
                    }

                    WeChatUser vCUEntity = new WeChatUserOperator().GetCustomerIDToEntity(customerId);
                    if (vCUEntity == null)
                    {
                        vCUEntity = new WeChatUser();
                        vCUEntity.Id = CreateCombGuid();
                        vCUEntity.OpenId = clientUpdateUserInfoNewRequest.openId;
                        vCUEntity.NickName = clientUpdateUserInfoNewRequest.nickName;
                        vCUEntity.Sex = clientUpdateUserInfoNewRequest.sex;
                        vCUEntity.Province = clientUpdateUserInfoNewRequest.province;
                        vCUEntity.City = clientUpdateUserInfoNewRequest.city;
                        vCUEntity.Country = clientUpdateUserInfoNewRequest.country;
                        vCUEntity.HeadImgUrl = headImgUrl;
                        vCUEntity.HeadImgSize = headImgSize;
                        vCUEntity.UnionId = clientUpdateUserInfoNewRequest.unionId;
                        vCUEntity.ModifyUser = "System";
                        vCUEntity.ModifyIP = IPAddress;


                        IList<WeChatUserPrivilege> privilegeList = new List<WeChatUserPrivilege>();
                        if (clientUpdateUserInfoNewRequest.privilege != null)
                        {
                            var privilegeRegex = new Regex("[(?<privilege>.+)]").Match(clientUpdateUserInfoNewRequest.privilege).Groups["privilege"].Value;
                            IList<string> privilege = new List<string>();
                            if (!string.IsNullOrEmpty(privilegeRegex))
                                privilege = privilegeRegex.Split(',').ToList();
                            foreach (var item in privilege)
                            {
                                privilegeList.Add(new WeChatUserPrivilege
                                {
                                    Id = CreateCombGuid(),
                                    WeChatUser_Id = vCUEntity.Id,
                                    Privilege = item.ToString()
                                });
                            }
                        }

                        int s = new WeChatUserOperator().Insert(vCUEntity, privilegeList);
                        if (s == 0)
                            clientUpdateUserInfoNewResponse.result = VAResult.VA_FAILED_WeChat_BindingUpdate;
                        clientUpdateUserInfoNewResponse.isExists = 0;

                        if (vCUEntity.UnionId != clientUpdateUserInfoNewRequest.unionId)
                            clientUpdateUserInfoNewResponse.message = new SystemConfigCacheLogic().GetSystemConfig("RemoveBindingWeChatUser", string.Format("解除绑定{0}微信用户，重新绑定{1}用户", clientUpdateUserInfoNewRequest.nickName, vCUEntity.NickName));
                    }
                    else
                    {
                        vCUEntity.OpenId = clientUpdateUserInfoNewRequest.openId;
                        vCUEntity.NickName = clientUpdateUserInfoNewRequest.nickName;
                        vCUEntity.Sex = clientUpdateUserInfoNewRequest.sex;
                        vCUEntity.Province = clientUpdateUserInfoNewRequest.province;
                        vCUEntity.City = clientUpdateUserInfoNewRequest.city;
                        vCUEntity.Country = clientUpdateUserInfoNewRequest.country;
                        vCUEntity.HeadImgUrl = headImgUrl;
                        vCUEntity.HeadImgSize = headImgSize;
                        vCUEntity.UnionId = clientUpdateUserInfoNewRequest.unionId;
                        vCUEntity.ModifyUser = "System";
                        vCUEntity.ModifyIP = IPAddress;
                        IList<WeChatUserPrivilege> privilegeList = new List<WeChatUserPrivilege>();
                        if (clientUpdateUserInfoNewRequest.privilege != null)
                        {
                            var privilegeRegex = new Regex("[(?<privilege>.+)]").Match(clientUpdateUserInfoNewRequest.privilege).Groups["privilege"].Value;
                            IList<string> privilege = new List<string>();
                            if (!string.IsNullOrEmpty(privilegeRegex))
                                privilege = privilegeRegex.Split(',').ToList();
                            foreach (var item in privilege)
                            {
                                privilegeList.Add(new WeChatUserPrivilege
                                {
                                    Id = CreateCombGuid(),
                                    WeChatUser_Id = vCUEntity.Id,
                                    Privilege = item.ToString()
                                });
                            }
                        }
                        int s = new WeChatUserOperator().Update(vCUEntity, privilegeList);
                        if (s == 0)
                            clientUpdateUserInfoNewResponse.result = VAResult.VA_FAILED_WeChat_BindingUpdate;
                        clientUpdateUserInfoNewResponse.isExists = 1;
                    }
                }

                #endregion

                new CustomerManager().UpdateIsUpdateUserName((long)checkResult.dtCustomer.Rows[0]["CustomerID"], 1);
            }
            else
            {
                clientUpdateUserInfoNewResponse.result = checkResult.result;
            }
            return clientUpdateUserInfoNewResponse;
        }

        /// <summary>
        /// 客户端手机认证new
        /// 20140126 xiaoyu
        /// 新手机绑定接口取消密码，每次登录时均发送验证码
        /// </summary>
        /// <param name="clientVerifyMobileNewRequest"></param>
        /// <returns></returns>
        public VAClientMobileVerifyNewResponse ClientMobileVerifyNew(VAClientMobileVerifyNewRequest clientVerifyMobileNewRequest)
        {
            VAClientMobileVerifyNewResponse clientVerifyMobileNewResponse = new VAClientMobileVerifyNewResponse();
            clientVerifyMobileNewResponse.type = VAMessageType.CLIENT_MOBILE_VERIFYNEW_RESPONSE;
            clientVerifyMobileNewResponse.cookie = clientVerifyMobileNewRequest.cookie;
            clientVerifyMobileNewResponse.uuid = clientVerifyMobileNewRequest.uuid;
            VAUserInfo userInfo = new VAUserInfo();
            clientVerifyMobileNewResponse.userInfo = userInfo;//先赋空值
            CheckCookieAndMsgtypeInfo checkResult = Common.CheckCookieAndMsgtype(clientVerifyMobileNewRequest.cookie,
                clientVerifyMobileNewRequest.uuid, (int)clientVerifyMobileNewRequest.type, (int)VAMessageType.CLIENT_MOBILE_VERIFYNEW_REQUEST, false);
            if (checkResult.result == VAResult.VA_OK)
            {
                //请求状态
                int requestState = 0;
                if (clientVerifyMobileNewRequest.bindingWeChat == 1)
                {
                    if (!string.IsNullOrEmpty(clientVerifyMobileNewRequest.mobilePhoneNumber))
                        requestState = 1;
                    else
                        requestState = 2;
                }
                if (requestState != 2)
                    clientVerifyMobileNewRequest.mobilePhoneNumber = clientVerifyMobileNewRequest.mobilePhoneNumber.Replace("-", "").Replace("+86", "").Replace("+", "").Replace("*", "").Replace("#", "");

                if (requestState == 0 || requestState == 1)
                {
                    if (!string.IsNullOrEmpty(clientVerifyMobileNewRequest.mobilePhoneNumber))
                    {
                        DataTable dtDevice = customerMan.SelectDevice(clientVerifyMobileNewRequest.uuid);
                        if (dtDevice.Rows.Count == 1)
                        {
                            bool isMobileVerified = IsMobileVerified(clientVerifyMobileNewRequest.mobilePhoneNumber);
                            if (isMobileVerified)
                            {
                                #region 手机登录
                                //RedEnvelopeDetailOperate redEnvelopeOperate = new RedEnvelopeDetailOperate();
                                //double executedRedEnvelopeAmount = 0;
                                //bool redEnvelopeFlag = redEnvelopeOperate.DoExpirationRedEnvelopeLogic(string.IsNullOrEmpty(clientVerifyMobileNewRequest.mobilePhoneNumber) ? ""
                                //    : clientVerifyMobileNewRequest.mobilePhoneNumber, ref  executedRedEnvelopeAmount);
                                //if (!redEnvelopeFlag)
                                //{
                                //    clientVerifyMobileNewResponse.result = VAResult.VA_FAILED_DB_ERROR;
                                //    return clientVerifyMobileNewResponse;
                                //}
                                if (!string.IsNullOrEmpty(clientVerifyMobileNewRequest.uuid))
                                {
                                    DataTable dtCustomer = customerMan.SelectCustomerByMobilephone(clientVerifyMobileNewRequest.mobilePhoneNumber);
                                    if (dtCustomer.Rows.Count == 1)
                                    {
                                        DateTime verificationCodeTime = Common.ToDateTime(dtCustomer.Rows[0]["verificationCodeTime"]);
                                        string verificationCode = Common.ToString(dtCustomer.Rows[0]["verificationCode"]);
                                        int errorCount = Common.ToInt32(dtCustomer.Rows[0]["verificationCodeErrCnt"]);//用户验证码错误次数
                                        bool clearErrorCount = errorCount > 0;

                                        if (clientVerifyMobileNewRequest.noCode == 0 && string.IsNullOrEmpty(clientVerifyMobileNewRequest.verificationCode))
                                        {//验证码为空则为发送验证码请求

                                            if (clientVerifyMobileNewRequest.mobilePhoneNumber == "23588776637")//苹果审核专用账号
                                            {
                                                clientVerifyMobileNewResponse.result = VAResult.VA_OK;
                                            }
                                            else
                                            {
                                                if (((System.DateTime.Now - verificationCodeTime) < TimeSpan.FromSeconds(Common.smsValidTime)) && !string.IsNullOrEmpty(verificationCode))
                                                {
                                                    clientVerifyMobileNewResponse.result = SendVerificationCodeBySmsForClient("", clientVerifyMobileNewRequest.mobilePhoneNumber, verificationCode, false, clearErrorCount);
                                                }
                                                else
                                                {
                                                    clientVerifyMobileNewResponse.result = SendVerificationCodeBySmsForClient("", clientVerifyMobileNewRequest.mobilePhoneNumber, "", false, clearErrorCount);
                                                }
                                            }
                                            VAAppType currentUserAppType = (VAAppType)Common.ToInt32(dtDevice.Rows[0]["appType"]);
                                            clientVerifyMobileNewResponse.userInfo = GetUserInfo(dtCustomer, clientVerifyMobileNewRequest.uuid, clientVerifyMobileNewRequest.pushToken, clientVerifyMobileNewRequest.cityId, currentUserAppType, clientVerifyMobileNewRequest.clientBuild);
                                        }
                                        else
                                        {//验证码不为空则为手机登录请求
                                            if (clientVerifyMobileNewRequest.noCode == 1 || (System.DateTime.Now - verificationCodeTime) < TimeSpan.FromSeconds(Common.smsValidTime) || clientVerifyMobileNewRequest.mobilePhoneNumber == "23588776637")
                                            {
                                                DateTime unlockTime = Common.ToDateTime(dtDevice.Rows[0]["unlockTime"]);//设备解锁时间
                                                if (clientVerifyMobileNewRequest.noCode == 1 || unlockTime < DateTime.Now)//正常状态
                                                {
                                                    if (clientVerifyMobileNewRequest.noCode == 1 || string.Equals(clientVerifyMobileNewRequest.verificationCode, verificationCode))
                                                    {
                                                        VAAppType currentUserAppType = (VAAppType)Common.ToInt32(dtDevice.Rows[0]["appType"]);
                                                        long customerId = Common.ToInt64(dtCustomer.Rows[0]["CustomerID"]);
                                                        using (TransactionScope scope = new TransactionScope())
                                                        {
                                                            long deviceId = Common.ToInt64(dtDevice.Rows[0]["deviceId"]);
                                                            DataTable dtCustomerConnDevice = customerMan.SelectCustomerConnDevice(customerId, deviceId);
                                                            long flagInsertCustomerConnDevice = 0;
                                                            bool flagUpdateCustomerConnDeviceTime = false;
                                                            if (dtCustomerConnDevice.Rows.Count == 0)
                                                            {//用户与设备关系表中没有记录，说明用户在该设备上未登陆过,则插入一条记录
                                                                CustomerConnDevice customerConnDevice = new CustomerConnDevice();
                                                                customerConnDevice.customerId = customerId;
                                                                customerConnDevice.deviceId = deviceId;
                                                                customerConnDevice.updateTime = System.DateTime.Now;
                                                                flagInsertCustomerConnDevice = customerMan.InsertCustomerConnDevice(customerConnDevice);
                                                            }
                                                            else
                                                            {//更新时间
                                                                flagUpdateCustomerConnDeviceTime = customerMan.UpdateCustomerConnDeviceTime(customerId, deviceId);
                                                            }
                                                            clientVerifyMobileNewResponse.rationBalance = Common.ToDouble(new ClientExtendManager().GetCustomerCurrectInfo(customerId)); //Common.ToDouble(dtCustomer.Rows[0]["money19dianRemained"]);
                                                            CustomerInfo customer = new CustomerInfo();
                                                            bool flagUpdateCustomerInfo = true;
                                                            if (!string.IsNullOrEmpty(clientVerifyMobileNewRequest.displayName))
                                                            {
                                                                customer.mobilePhoneNumber = clientVerifyMobileNewRequest.mobilePhoneNumber;


                                                                customer.cookie = Common.ToString(dtCustomer.Rows[0]["cookie"]);

                                                                bool falg = false;//true：不需要更新用户的头像；false：需要更新用户的头像

                                                                #region 处理用户头像
                                                                if (string.IsNullOrEmpty(clientVerifyMobileNewRequest.personalImgInfo))
                                                                {
                                                                    customer.personalImgInfo = "";
                                                                    falg = true;
                                                                }
                                                                else
                                                                {
                                                                    string pictureContent = clientVerifyMobileNewRequest.personalImgInfo.ToString();
                                                                    if (pictureContent.ToLower().IndexOf("http") == 0)
                                                                    {
                                                                        //新版本传递URL
                                                                        //falg = true;
                                                                        customer.personalImgInfo = pictureContent;
                                                                    }
                                                                    else
                                                                    {
                                                                        falg = false;
                                                                        customer.personalImgInfo = clientVerifyMobileNewRequest.personalImgInfo;
                                                                        ClientExtendOperate.ClientModifyPersonalImg(dtCustomer.Rows[0], customer.personalImgInfo);
                                                                    }
                                                                }
                                                                #endregion

                                                                if (!string.IsNullOrEmpty(clientVerifyMobileNewRequest.unionId))
                                                                {
                                                                    int isUpdateUserName = dtCustomer.Rows[0]["IsUpdateUserName"] == null || dtCustomer.Rows[0]["IsUpdateUserName"] is System.DBNull ? 0 : (int)dtCustomer.Rows[0]["IsUpdateUserName"];

                                                                    long customerId1 = (long)dtCustomer.Rows[0]["CustomerId"];
                                                                    var weChatUserEntity = new WeChatUserManager().GetCustomerIDToEntity(customerId1);
                                                                    if (weChatUserEntity != null && weChatUserEntity.UnionId == clientVerifyMobileNewRequest.unionId && isUpdateUserName != 0)
                                                                    {
                                                                        customer.CustomerSex = dtCustomer.Rows[0]["CustomerSex"] == null ? 0 : (int)dtCustomer.Rows[0]["customerSex"];
                                                                        customer.personalImgInfo = dtCustomer.Rows[0]["personalImgInfo"] == null ? "" : dtCustomer.Rows[0]["personalImgInfo"].ToString();
                                                                        customer.UserName = dtCustomer.Rows[0]["UserName"] == null ? "" : dtCustomer.Rows[0]["UserName"].ToString();
                                                                    }
                                                                    else
                                                                    {
                                                                        customer.CustomerSex = clientVerifyMobileNewRequest.customerSex;
                                                                        customer.personalImgInfo = string.IsNullOrEmpty(clientVerifyMobileNewRequest.personalImgInfo) ? "" : clientVerifyMobileNewRequest.personalImgInfo;
                                                                        customer.UserName = string.IsNullOrEmpty(clientVerifyMobileNewRequest.displayName) ? "" : clientVerifyMobileNewRequest.displayName;
                                                                        new CustomerManager().UpdateIsUpdateUserName(customerId1, 0);

                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    customer.CustomerSex = clientVerifyMobileNewRequest.customerSex;
                                                                    customer.personalImgInfo = string.IsNullOrEmpty(clientVerifyMobileNewRequest.personalImgInfo) ? "" : clientVerifyMobileNewRequest.personalImgInfo;
                                                                    customer.UserName = string.IsNullOrEmpty(clientVerifyMobileNewRequest.displayName) ? "" : clientVerifyMobileNewRequest.displayName;
                                                                }


                                                                flagUpdateCustomerInfo = falg ? customerMan.ClientUpdateCustomerInfo1(customer) : customerMan.UpdateCustomerInfoByMobile(customer);
                                                            }
                                                            if ((flagInsertCustomerConnDevice > 0 || flagUpdateCustomerConnDeviceTime) && flagUpdateCustomerInfo)
                                                            {
                                                                clientVerifyMobileNewResponse.result = VAResult.VA_OK;
                                                                clientVerifyMobileNewResponse.cookie = Common.ToString(dtCustomer.Rows[0]["cookie"]);
                                                                if (clientVerifyMobileNewRequest.shopId > 0)
                                                                {
                                                                    ShopManager shopMan = new ShopManager();
                                                                    DataTable dtShopVipInfo = shopMan.SelectShopVipInfo(clientVerifyMobileNewRequest.shopId);//查询当前门店的VIP等级信息
                                                                    if (dtShopVipInfo.Rows.Count > 0)
                                                                    {
                                                                        int currentVipGride = Common.ToInt32(dtCustomer.Rows[0]["currentPlatformVipGrade"]);
                                                                        VAShopVipInfo userVipInfo = new VAShopVipInfo();
                                                                        ClientExtendOperate.GetUserVipInfo(currentVipGride, dtShopVipInfo, userVipInfo);
                                                                        clientVerifyMobileNewResponse.userCurrentShopDiscount = userVipInfo.discount;
                                                                    }
                                                                    else
                                                                    {
                                                                        clientVerifyMobileNewResponse.userCurrentShopDiscount = 1;
                                                                    }
                                                                    ShopInfo shopInfo = new ShopOperate().QueryShop(clientVerifyMobileNewRequest.shopId);
                                                                    if (Common.CheckLatestBuild_August(clientVerifyMobileNewRequest.appType, clientVerifyMobileNewRequest.clientBuild))
                                                                    {
                                                                        RedEnvelopeOperate redEnvelopeOperate = new RedEnvelopeOperate();
                                                                        double executedRedEnvelopeAmount = redEnvelopeOperate.QueryCustomerExcutedRedEnvelope(clientVerifyMobileNewRequest.mobilePhoneNumber);

                                                                        clientVerifyMobileNewResponse.executedRedEnvelopeAmount = shopInfo == null ? 0 : (shopInfo.isSupportRedEnvelopePayment ? executedRedEnvelopeAmount : 0);
                                                                    }
                                                                    else
                                                                    {
                                                                        clientVerifyMobileNewResponse.executedRedEnvelopeAmount = 0;
                                                                    }
                                                                    if (Common.CheckLatestBuild_February(clientVerifyMobileNewRequest.appType, clientVerifyMobileNewRequest.clientBuild))
                                                                    {
                                                                        var operate = new CouponOperate();
                                                                        List<OrderPaymentCouponDetail> list = operate.GetShopCouponDetails(Common.ToString(checkResult.dtCustomer.Rows[0]["mobilePhoneNumber"]), clientVerifyMobileNewRequest.shopId);
                                                                        clientVerifyMobileNewResponse.couponDetails = list;
                                                                    }
                                                                    else
                                                                    {
                                                                        clientVerifyMobileNewResponse.couponDetails = new List<OrderPaymentCouponDetail>();
                                                                    }
                                                                }
                                                                scope.Complete();
                                                            }
                                                            else
                                                            {
                                                                clientVerifyMobileNewResponse.result = VAResult.VA_FAILED_DB_ERROR;
                                                            }
                                                        }

                                                        #region 微信相关

                                                        if (clientVerifyMobileNewResponse.result == VAResult.VA_OK)
                                                        {
                                                            var weChatUserEntity = new WeChatUserOperator().GetCustomerIDToEntity(customerId);
                                                            if (weChatUserEntity == null)
                                                                clientVerifyMobileNewResponse.isExists = 0;
                                                            else
                                                            {
                                                                clientVerifyMobileNewResponse.isExists = 1;
                                                                clientVerifyMobileNewResponse.unionId = weChatUserEntity.UnionId;
                                                            }
                                                        }
                                                        #endregion

                                                        if (clientVerifyMobileNewResponse.result == VAResult.VA_OK)
                                                        {//因为激励函数中单独使用自己的事务，此处为了避免事务冲突，将此部分函数放在优惠券使用事务之外
                                                            DataTable dtCustomerNew = customerMan.SelectCustomerByMobilephone(clientVerifyMobileNewRequest.mobilePhoneNumber);
                                                            clientVerifyMobileNewResponse.userInfo = GetUserInfo(dtCustomerNew, clientVerifyMobileNewRequest.uuid, clientVerifyMobileNewRequest.pushToken, clientVerifyMobileNewRequest.cityId, currentUserAppType, clientVerifyMobileNewRequest.clientBuild);
                                                            #region 调用激励模块
                                                            //VAEncourageDetail encourageDetail = new VAEncourageDetail();
                                                            //encourageDetail.appType = currentUserAppType;
                                                            //encourageDetail.customerId = customerId;
                                                            //encourageDetail.messageType = VAMessageType.CLIENT_MOBILE_LOGIN_REQUEST;
                                                            //encourageDetail.pushToken = clientVerifyMobileNewRequest.pushToken;
                                                            //Thread encourageThread = new Thread(Common.Encourage);
                                                            //encourageThread.Start((object)encourageDetail);
                                                            #endregion
                                                        }
                                                    }
                                                    else
                                                    {
                                                        //验证码错误，要更新错误次数
                                                        customerMan.UpdateVerificationCodeErrorCount(Common.ToInt64(dtCustomer.Rows[0]["customerId"]));

                                                        SystemConfigCacheLogic systemConfigCacheLogic = new SystemConfigCacheLogic();
                                                        int count = systemConfigCacheLogic.GetVerificationCodeErrorCountOfCache();

                                                        //如果超过设定次数，则封锁设备                                                   
                                                        if (errorCount + 1 > count)
                                                        {
                                                            double lockTime = systemConfigCacheLogic.GetDeviceLockTimeOfCache();
                                                            customerMan.UpdateDeviceUnlockTime(Common.ToInt64(dtDevice.Rows[0]["deviceId"]), DateTime.Now.AddMinutes(lockTime));

                                                            clientVerifyMobileNewResponse.deviceLockMessage = "您验证码错误次数过多，请" + lockTime + "分钟后重新获取验证码登录";

                                                            clientVerifyMobileNewResponse.result = VAResult.VA_FAILED_DEVICE_LOCKED;
                                                        }
                                                        else
                                                        {
                                                            clientVerifyMobileNewResponse.result = VAResult.VA_FAILED_VERIFICATIONCODE_WRONG;
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    int lockTime = (unlockTime - DateTime.Now).Minutes + 1;
                                                    clientVerifyMobileNewResponse.deviceLockMessage = "请" + lockTime + "分钟后重新获取验证码登录";
                                                    clientVerifyMobileNewResponse.result = VAResult.VA_FAILED_DEVICE_LOCKED;
                                                }
                                            }
                                            else
                                            {
                                                clientVerifyMobileNewResponse.result = VAResult.VA_FAILED_VERIFICATIONCODE_OUTOFTIME;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        clientVerifyMobileNewResponse.result = VAResult.VA_FAILED_PHONENUMBER_NOT_FOUND;
                                    }

                                }
                                else
                                {
                                    clientVerifyMobileNewResponse.result = VAResult.VA_FAILED_UUID_NULL;
                                }
                                clientVerifyMobileNewResponse.isNewMobile = false;
                                #endregion
                            }
                            else
                            {
                                #region 手机注册
                                DateTime verificationCodeTime = Common.ToDateTime(checkResult.dtCustomer.Rows[0]["verificationCodeTime"]);
                                string verificationCode = Common.ToString(checkResult.dtCustomer.Rows[0]["verificationCode"]);
                                int errorCount = Common.ToInt32(checkResult.dtCustomer.Rows[0]["verificationCodeErrCnt"]);//用户验证码错误次数
                                bool clearErrorCount = errorCount > 0;

                                if (clientVerifyMobileNewRequest.noCode == 0 && string.IsNullOrEmpty(clientVerifyMobileNewRequest.verificationCode))
                                {//验证码为空则为发送验证码请求
                                    if (clientVerifyMobileNewRequest.mobilePhoneNumber == "23588776637")//苹果审核专用账号
                                    {
                                        clientVerifyMobileNewResponse.result = VAResult.VA_OK;
                                    }
                                    else
                                    {
                                        if (((System.DateTime.Now - verificationCodeTime) < TimeSpan.FromSeconds(Common.smsValidTime)) && !string.IsNullOrEmpty(verificationCode))
                                        {
                                            clientVerifyMobileNewResponse.result = SendVerificationCodeBySmsForClient(clientVerifyMobileNewRequest.cookie, clientVerifyMobileNewRequest.mobilePhoneNumber, verificationCode, false, clearErrorCount);
                                        }
                                        else
                                        {
                                            clientVerifyMobileNewResponse.result = SendVerificationCodeBySmsForClient(clientVerifyMobileNewRequest.cookie, clientVerifyMobileNewRequest.mobilePhoneNumber, "", false, clearErrorCount);
                                        }

                                        #region 处理用户非法红包

                                        CustomerRedEnvelope customerRedEnvelope = new CustomerRedEnvelope()
                                        {
                                            mobilePhoneNumber = clientVerifyMobileNewRequest.mobilePhoneNumber,
                                            uuid = clientVerifyMobileNewRequest.uuid
                                        };
                                        RedEnvelopeOperate redEnvelopeOperate1 = new RedEnvelopeOperate();

                                        Thread redEnvelopeThread = new Thread(redEnvelopeOperate1.ClearIllegalRedEnvelope);
                                        redEnvelopeThread.Start((object)customerRedEnvelope);

                                        #endregion
                                    }
                                }
                                else
                                {//验证码不为空则为认证手机请求

                                    if (clientVerifyMobileNewRequest.noCode == 1 || (System.DateTime.Now - verificationCodeTime) < TimeSpan.FromSeconds(Common.smsValidTime))
                                    {
                                        DateTime unlockTime = Common.ToDateTime(dtDevice.Rows[0]["unlockTime"]);//设备解锁时间
                                        if (clientVerifyMobileNewRequest.noCode == 1 || unlockTime < DateTime.Now)//正常状态
                                        {
                                            DataTable dtRegisterInfo = customerMan.SelectCustomerRegisterInfo(clientVerifyMobileNewRequest.cookie);
                                            if (dtRegisterInfo != null && dtRegisterInfo.Rows.Count > 0)
                                            {
                                                using (TransactionScope scope = new TransactionScope())
                                                {
                                                    //取出DB中发验证码的手机号码
                                                    string verificationCodeMobile = dtRegisterInfo.Rows[0]["verificationCodeMobile"].ToString();

                                                    if (clientVerifyMobileNewRequest.noCode == 1 || string.Equals(clientVerifyMobileNewRequest.verificationCode, verificationCode))
                                                    {
                                                        //取出DB中发验证码的手机号码
                                                        //string verificationCodeMobile = customerMan.SelectVerificationCodeMobile(clientVerifyMobileNewRequest.cookie);

                                                        CustomerInfo customer = new CustomerInfo();
                                                        customer.cookie = clientVerifyMobileNewRequest.cookie;
                                                        //if (!string.IsNullOrEmpty(verificationCodeMobile))
                                                        //{
                                                        //    //之所以注册用户的手机号，取DB中发验证码的手机号码，是为了避免客户端异常，导致注册时的手机号已不是发验证的手机号
                                                        //    customer.mobilePhoneNumber = verificationCodeMobile;
                                                        //}
                                                        //else
                                                        //{
                                                        //    customer.mobilePhoneNumber = clientVerifyMobileNewRequest.mobilePhoneNumber;
                                                        //}
                                                        customer.mobilePhoneNumber = clientVerifyMobileNewRequest.mobilePhoneNumber;

                                                        customer.CustomerSex = clientVerifyMobileNewRequest.customerSex;
                                                        customer.personalImgInfo = string.IsNullOrEmpty(clientVerifyMobileNewRequest.personalImgInfo) ? "" : clientVerifyMobileNewRequest.personalImgInfo;
                                                        customer.UserName = string.IsNullOrEmpty(clientVerifyMobileNewRequest.displayName) ? "" : clientVerifyMobileNewRequest.displayName;
                                                        customer.registerCityId = clientVerifyMobileNewRequest.cityId;

                                                        bool falg = false;//true：不需要更新用户的头像；false：需要更新用户的头像
                                                        #region 处理用户头像
                                                        if (string.IsNullOrEmpty(clientVerifyMobileNewRequest.personalImgInfo))
                                                        {
                                                            customer.personalImgInfo = "";
                                                            falg = true;
                                                        }
                                                        else
                                                        {
                                                            string pictureContent = clientVerifyMobileNewRequest.personalImgInfo.ToString();
                                                            if (pictureContent.ToLower().IndexOf("http") == 0)
                                                            {
                                                                //新版本传递URL
                                                                //falg = true;
                                                                customer.personalImgInfo = pictureContent;
                                                            }
                                                            else
                                                            {
                                                                falg = false;
                                                                customer.personalImgInfo = clientVerifyMobileNewRequest.personalImgInfo;
                                                                ClientExtendOperate.ClientModifyPersonalImg(checkResult.dtCustomer.Rows[0], customer.personalImgInfo);
                                                            }
                                                        }
                                                        #endregion
                                                        bool flagUpdateCustomerInfo = falg ? customerMan.ClientUpdateCustomerInfoAndphone(customer) : customerMan.UpdateCustomerInfoAndphone(customer);
                                                        if (flagUpdateCustomerInfo)
                                                        {
                                                            #region 返回用户的红包金额及优惠券信息

                                                            if (Common.CheckLatestBuild_February(clientVerifyMobileNewRequest.appType, clientVerifyMobileNewRequest.clientBuild))
                                                            {
                                                                var operate = new CouponOperate();
                                                                List<OrderPaymentCouponDetail> list = operate.GetShopCouponDetails(Common.ToString(checkResult.dtCustomer.Rows[0]["mobilePhoneNumber"]), clientVerifyMobileNewRequest.shopId);
                                                                clientVerifyMobileNewResponse.couponDetails = list;
                                                            }
                                                            else
                                                            {
                                                                clientVerifyMobileNewResponse.couponDetails = new List<OrderPaymentCouponDetail>();
                                                            }
                                                            ShopInfo shopInfo = new ShopOperate().QueryShop(clientVerifyMobileNewRequest.shopId);

                                                            if (Common.CheckLatestBuild_August(clientVerifyMobileNewRequest.appType, clientVerifyMobileNewRequest.clientBuild))
                                                            {
                                                                RedEnvelopeOperate redEnvelopeOperate = new RedEnvelopeOperate();
                                                                double executedRedEnvelopeAmount = redEnvelopeOperate.QueryCustomerExcutedRedEnvelope(clientVerifyMobileNewRequest.mobilePhoneNumber);

                                                                clientVerifyMobileNewResponse.executedRedEnvelopeAmount = shopInfo == null ? 0
                                                                                                                               : (shopInfo.isSupportRedEnvelopePayment ? executedRedEnvelopeAmount : 0);
                                                            }
                                                            else
                                                            {
                                                                clientVerifyMobileNewResponse.executedRedEnvelopeAmount = 0;
                                                            }
                                                            #endregion

                                                            clientVerifyMobileNewResponse.result = VAResult.VA_OK;
                                                            scope.Complete();
                                                        }
                                                        else
                                                        {
                                                            clientVerifyMobileNewResponse.result = VAResult.VA_FAILED_DB_ERROR;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        SystemConfigCacheLogic systemConfigCacheLogic = new SystemConfigCacheLogic();

                                                        //验证码错误，要更新错误次数
                                                        customerMan.UpdateVerificationCodeErrorCount(Common.ToInt64(dtRegisterInfo.Rows[0]["customerId"]));

                                                        int count = systemConfigCacheLogic.GetVerificationCodeErrorCountOfCache();

                                                        //如果超过设定次数，则封锁设备                                                   
                                                        if (errorCount + 1 > count)
                                                        {
                                                            double lockTime = systemConfigCacheLogic.GetDeviceLockTimeOfCache();
                                                            customerMan.UpdateDeviceUnlockTime(Common.ToInt64(dtDevice.Rows[0]["deviceId"]), DateTime.Now.AddMinutes(lockTime));

                                                            clientVerifyMobileNewResponse.deviceLockMessage = "您验证码错误次数过多，请" + lockTime + "分钟后重新获取验证码登录";

                                                            clientVerifyMobileNewResponse.result = VAResult.VA_FAILED_DEVICE_LOCKED;
                                                        }
                                                        else
                                                        {
                                                            clientVerifyMobileNewResponse.result = VAResult.VA_FAILED_VERIFICATIONCODE_WRONG;
                                                        }
                                                        scope.Complete();
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                clientVerifyMobileNewResponse.result = VAResult.VA_FAILED_COOKIE_NOT_FOUND;
                                            }
                                        }
                                        else
                                        {
                                            int lockTime = (unlockTime - DateTime.Now).Minutes + 1;
                                            clientVerifyMobileNewResponse.deviceLockMessage = "请" + lockTime + "分钟后重新获取验证码登录";
                                            clientVerifyMobileNewResponse.result = VAResult.VA_FAILED_DEVICE_LOCKED;
                                        }
                                    }
                                    else
                                    {
                                        clientVerifyMobileNewResponse.result = VAResult.VA_FAILED_VERIFICATIONCODE_OUTOFTIME;
                                    }
                                    if (clientVerifyMobileNewResponse.result == VAResult.VA_OK)
                                    {//因为激励函数中单独使用自己的事务，此处为了避免事务冲突，将此部分函数放在优惠券使用事务之外
                                        VAAppType currentUserAppType = (VAAppType)Common.ToInt32(checkResult.dtCustomer.Rows[0]["appType"]);
                                        DataTable dtCustomer = customerMan.SelectCustomerByMobilephone(clientVerifyMobileNewRequest.mobilePhoneNumber);
                                        if (dtCustomer.Rows.Count == 1)
                                        {
                                            clientVerifyMobileNewResponse.userInfo = GetUserInfo(dtCustomer, clientVerifyMobileNewRequest.uuid, clientVerifyMobileNewRequest.pushToken, clientVerifyMobileNewRequest.cityId, currentUserAppType, clientVerifyMobileNewRequest.clientBuild);
                                        }
                                        //用户注册成功，返回折扣值，
                                        if (clientVerifyMobileNewRequest.shopId > 0)
                                        {
                                            ShopManager shopMan = new ShopManager();
                                            DataTable dtShopVipInfo = shopMan.SelectShopVipInfo(clientVerifyMobileNewRequest.shopId);//查询当前门店的VIP等级信息
                                            if (dtShopVipInfo.Rows.Count > 0)
                                            {
                                                int currentVipGride = Common.ToInt32(dtCustomer.Rows[0]["currentPlatformVipGrade"]);
                                                VAShopVipInfo userVipInfo = new VAShopVipInfo();
                                                ClientExtendOperate.GetUserVipInfo(currentVipGride, dtShopVipInfo, userVipInfo);
                                                clientVerifyMobileNewResponse.userCurrentShopDiscount = userVipInfo.discount;
                                            }
                                            else
                                            {
                                                clientVerifyMobileNewResponse.userCurrentShopDiscount = 1;
                                            }
                                        }
                                        else
                                        {
                                            clientVerifyMobileNewResponse.userCurrentShopDiscount = 1;
                                        }
                                        #region 调用激励模块执行该用户手机绑定奖励
                                        //long customerId = Common.ToInt64(checkResult.dtCustomer.Rows[0]["CustomerID"]);
                                        //VAEncourageDetail encourageDetail = new VAEncourageDetail();
                                        //encourageDetail.appType = currentUserAppType;
                                        //encourageDetail.customerId = customerId;
                                        //encourageDetail.messageType = VAMessageType.CLIENT_MOBILE_VERIFY_REQUEST;
                                        //encourageDetail.pushToken = Common.ToString(checkResult.dtCustomer.Rows[0]["pushToken"]);
                                        //Thread encourageThread = new Thread(Common.Encourage);
                                        //encourageThread.Start((object)encourageDetail);
                                        #endregion

                                        #region 调用激励模块执行用户邀请信息奖励
                                        //VAEncourageDetail customerInviteEncourageDetail = new VAEncourageDetail();
                                        //customerInviteEncourageDetail.phoneNumberInvited = clientVerifyMobileNewRequest.mobilePhoneNumber;
                                        //customerInviteEncourageDetail.messageType = VAMessageType.CLIENT_INVITE_CUSTOMER_REQUEST;
                                        //Thread customerInviteEncourageThread = new Thread(Common.Encourage);
                                        //customerInviteEncourageThread.Start((object)customerInviteEncourageDetail);
                                        #endregion
                                    }
                                }
                                clientVerifyMobileNewResponse.isNewMobile = true;
                                #endregion
                            }
                        }
                        else
                        {
                            clientVerifyMobileNewResponse.result = VAResult.VA_FAILED_UUID_NOT_FOUND;
                        }
                    }
                    else
                    {
                        clientVerifyMobileNewResponse.result = VAResult.VA_FAILED_PHONENUMBER_NULL;
                    }
                }

                if (requestState == 1 && clientVerifyMobileNewResponse.result == VAResult.VA_OK)
                {
                    #region 微信用户

                    var customerDal = new CustomerManager();
                    var weChatUserDal = new WeChatUserManager();
                    CustomerInfo userModel = customerDal.GetMobileOfModel(clientVerifyMobileNewRequest.mobilePhoneNumber);
                    var vCUCustomerIdEntity = weChatUserDal.GetCustomerIDToEntity(userModel.CustomerID);
                    var vCUUnionIdEntity = weChatUserDal.GetUnionIdOfModel(clientVerifyMobileNewRequest.unionId);

                    if (vCUCustomerIdEntity == null && vCUUnionIdEntity != null)
                    {
                        vCUCustomerIdEntity = vCUUnionIdEntity;
                        new CustomerManager().UpdateIsUpdateUserName(vCUCustomerIdEntity.CustomerInfo_CustomerID, 0);
                    }
                    else if (vCUCustomerIdEntity != null && vCUUnionIdEntity != null && vCUCustomerIdEntity.CustomerInfo_CustomerID != 0 && vCUCustomerIdEntity.Id != vCUUnionIdEntity.Id)
                    {
                        int s = weChatUserDal.UpdateRemoveCustomerInfoCustomerID(vCUCustomerIdEntity.Id);
                        vCUCustomerIdEntity = vCUUnionIdEntity;
                        new CustomerManager().UpdateIsUpdateUserName(vCUCustomerIdEntity.CustomerInfo_CustomerID, 0);
                    }
                    //else if (vCUUnionIdEntity != null && vCUCustomerIdEntity != null && vCUUnionIdEntity.CustomerInfo_CustomerID != 0 && vCUUnionIdEntity.CustomerInfo_CustomerID != 0 && vCUCustomerIdEntity.CustomerInfo_CustomerID != vCUUnionIdEntity.CustomerInfo_CustomerID)
                    //{
                    //    int s = weChatUserDal.UpdateRemoveCustomerInfoCustomerID(vCUCustomerIdEntity.Id);
                    //    vCUCustomerIdEntity = vCUUnionIdEntity;
                    //}

                    //if (vCUCustomerIdEntity != null)
                    //clientVerifyMobileNewResponse.cookie = new CustomerManager().GetModelOfId(vCUCustomerIdEntity.CustomerInfo_CustomerID).cookie;

                    string strHeadImgUrl = null;
                    string headImgUrl = "";
                    int? headImgSize = 0;
                    if (!string.IsNullOrEmpty(clientVerifyMobileNewRequest.headImg))
                    {
                        strHeadImgUrl = clientVerifyMobileNewRequest.headImg;
                        headImgUrl = strHeadImgUrl;
                        headImgSize = int.Parse(strHeadImgUrl.Substring(strHeadImgUrl.LastIndexOf('/') + 1));
                    }

                    long customerId = 0;
                    if (vCUCustomerIdEntity == null)
                    {
                        vCUCustomerIdEntity = new WeChatUser();
                        vCUCustomerIdEntity.Id = CreateCombGuid();
                        vCUCustomerIdEntity.OpenId = clientVerifyMobileNewRequest.openId;
                        vCUCustomerIdEntity.NickName = clientVerifyMobileNewRequest.nickName;
                        vCUCustomerIdEntity.Sex = clientVerifyMobileNewRequest.sex;
                        vCUCustomerIdEntity.Province = clientVerifyMobileNewRequest.province;
                        vCUCustomerIdEntity.City = clientVerifyMobileNewRequest.city;
                        vCUCustomerIdEntity.Country = clientVerifyMobileNewRequest.country;
                        vCUCustomerIdEntity.HeadImgUrl = headImgUrl;
                        vCUCustomerIdEntity.HeadImgSize = headImgSize;
                        vCUCustomerIdEntity.UnionId = clientVerifyMobileNewRequest.unionId;
                        vCUCustomerIdEntity.ModifyUser = "System";
                        vCUCustomerIdEntity.ModifyIP = IPAddress;
                        if (userModel != null)
                            vCUCustomerIdEntity.CustomerInfo_CustomerID = userModel.CustomerID;
                        IList<WeChatUserPrivilege> privilegeList = new List<WeChatUserPrivilege>();
                        if (clientVerifyMobileNewRequest.privilege != null)
                        {
                            var privilegeRegex = new Regex("[(?<privilege>.+)]").Match(clientVerifyMobileNewRequest.privilege).Groups["privilege"].Value;
                            IList<string> privilege = new List<string>();
                            if (!string.IsNullOrEmpty(privilegeRegex))
                                privilege = privilegeRegex.Split(',').ToList();
                            foreach (var item in privilege)
                            {
                                privilegeList.Add(new WeChatUserPrivilege
                                {
                                    Id = CreateCombGuid(),
                                    WeChatUser_Id = vCUCustomerIdEntity.Id,
                                    Privilege = item.ToString()
                                });
                            }
                        }

                        int s = new WeChatUserOperator().Insert(vCUCustomerIdEntity, privilegeList);
                        if (s == 0)
                            clientVerifyMobileNewRequest.result = VAResult.VA_FAILED_WeChat_BindingUpdate;
                        clientVerifyMobileNewResponse.isExists = 0;
                    }
                    else
                    {
                        customerId = vCUCustomerIdEntity.CustomerInfo_CustomerID;
                        vCUCustomerIdEntity.OpenId = clientVerifyMobileNewRequest.openId;
                        vCUCustomerIdEntity.NickName = clientVerifyMobileNewRequest.nickName;
                        vCUCustomerIdEntity.Sex = clientVerifyMobileNewRequest.sex;
                        vCUCustomerIdEntity.Province = clientVerifyMobileNewRequest.province;
                        vCUCustomerIdEntity.City = clientVerifyMobileNewRequest.city;
                        vCUCustomerIdEntity.Country = clientVerifyMobileNewRequest.country;
                        vCUCustomerIdEntity.HeadImgUrl = headImgUrl;
                        vCUCustomerIdEntity.HeadImgSize = headImgSize;
                        vCUCustomerIdEntity.UnionId = clientVerifyMobileNewRequest.unionId;
                        vCUCustomerIdEntity.ModifyUser = "System";
                        vCUCustomerIdEntity.ModifyIP = IPAddress;

                        if (userModel != null)
                            vCUCustomerIdEntity.CustomerInfo_CustomerID = userModel.CustomerID;

                        IList<WeChatUserPrivilege> privilegeList = new List<WeChatUserPrivilege>();
                        if (clientVerifyMobileNewRequest.privilege != null)
                        {
                            var privilegeRegex = new Regex("[(?<privilege>.+)]").Match(clientVerifyMobileNewRequest.privilege).Groups["privilege"].Value;
                            IList<string> privilege = new List<string>();
                            if (!string.IsNullOrEmpty(privilegeRegex))
                                privilege = privilegeRegex.Split(',').ToList();
                            foreach (var item in privilege)
                            {
                                privilegeList.Add(new WeChatUserPrivilege
                                {
                                    Id = CreateCombGuid(),
                                    WeChatUser_Id = vCUCustomerIdEntity.Id,
                                    Privilege = item.ToString()
                                });
                            }
                        }
                        int s = new WeChatUserOperator().Update(vCUCustomerIdEntity, privilegeList);
                        if (s == 0)
                            clientVerifyMobileNewRequest.result = VAResult.VA_FAILED_WeChat_BindingUpdate;
                        clientVerifyMobileNewResponse.isExists = 1;
                    }

                    clientVerifyMobileNewResponse.unionId = vCUCustomerIdEntity.UnionId;
                    //微信登录并且跳过手机号
                    if (clientVerifyMobileNewRequest.mobilePhoneNumber == null)
                    {
                        clientVerifyMobileNewResponse.isExists = 0;
                    }

                    //修改用户表信息
                    if ((userModel.IsUpdateUserName == 0 || userModel.IsUpdateUserName == null) && userModel.CustomerID != customerId && vCUCustomerIdEntity != null && vCUCustomerIdEntity.UnionId != clientVerifyMobileNewRequest.unionId)
                    {
                        customerDal.UpdateWeChatUserRegistered(userModel.CustomerID, string.IsNullOrEmpty(clientVerifyMobileNewRequest.nickName) ? userModel.UserName : clientVerifyMobileNewRequest.nickName, clientVerifyMobileNewRequest.sex, string.IsNullOrEmpty(headImgUrl) ? userModel.personalImgInfo : headImgUrl);
                    }

                    #endregion
                }

                if (requestState == 2)
                {
                    #region 微信用户

                    string strHeadImgUrl = null;
                    string headImgUrl = "";
                    int? headImgSize = 0;
                    if (!string.IsNullOrEmpty(clientVerifyMobileNewRequest.headImg))
                    {
                        strHeadImgUrl = clientVerifyMobileNewRequest.headImg;
                        headImgUrl = strHeadImgUrl;
                        headImgSize = int.Parse(strHeadImgUrl.Substring(strHeadImgUrl.LastIndexOf('/') + 1));
                    }

                    var vCUEntity = new WeChatUserManager().GetUnionIdOfModel(clientVerifyMobileNewRequest.unionId);
                    if (vCUEntity == null)
                    {
                        DataTable dtRegisterInfo = customerMan.SelectCustomerRegisterInfo(clientVerifyMobileNewRequest.cookie);
                        if (dtRegisterInfo == null)
                        {
                            clientVerifyMobileNewResponse.result = VAResult.VA_FAILED_COOKIE_NOT_FOUND;
                            return clientVerifyMobileNewResponse;
                        }

                        long CustomerInfo_CustomerID = long.Parse(dtRegisterInfo.Rows[0]["customerId"].ToString());
                        vCUEntity = new WeChatUser();
                        vCUEntity.Id = CreateCombGuid();
                        vCUEntity.OpenId = clientVerifyMobileNewRequest.openId;
                        vCUEntity.NickName = clientVerifyMobileNewRequest.nickName;
                        vCUEntity.Sex = clientVerifyMobileNewRequest.sex;
                        vCUEntity.Province = clientVerifyMobileNewRequest.province;
                        vCUEntity.City = clientVerifyMobileNewRequest.city;
                        vCUEntity.Country = clientVerifyMobileNewRequest.country;
                        vCUEntity.HeadImgUrl = headImgUrl;
                        vCUEntity.HeadImgSize = headImgSize;
                        vCUEntity.UnionId = clientVerifyMobileNewRequest.unionId;
                        vCUEntity.ModifyUser = "System";
                        vCUEntity.ModifyIP = IPAddress;

                        CustomerInfo customer = new CustomerInfo();
                        customer.cookie = clientVerifyMobileNewRequest.cookie;
                        customer.mobilePhoneNumber = "";
                        customer.registerCityId = clientVerifyMobileNewRequest.cityId;

                        customer.CustomerSex = clientVerifyMobileNewRequest.sex;
                        customer.personalImgInfo = vCUEntity.HeadImgUrl;
                        customer.UserName = vCUEntity.NickName;
                        vCUEntity.CustomerInfo_CustomerID = customer.CustomerID = CustomerInfo_CustomerID;
                        customerMan.UpdateCustomerInfoAndphoneOfId(customer);

                        //vCUEntity.CustomerInfo_CustomerID = CustomerInfo_CustomerID;

                        IList<WeChatUserPrivilege> privilegeList = new List<WeChatUserPrivilege>();
                        if (clientVerifyMobileNewRequest.privilege != null)
                        {
                            var privilegeRegex = new Regex("[(?<privilege>.+)]").Match(clientVerifyMobileNewRequest.privilege).Groups["privilege"].Value;
                            IList<string> privilege = new List<string>();
                            if (!string.IsNullOrEmpty(privilegeRegex))
                                privilege = privilegeRegex.Split(',').ToList();
                            foreach (var item in privilege)
                            {
                                privilegeList.Add(new WeChatUserPrivilege
                                {
                                    Id = CreateCombGuid(),
                                    WeChatUser_Id = vCUEntity.Id,
                                    Privilege = item.ToString()
                                });
                            }
                        }

                        int s = new WeChatUserOperator().Insert(vCUEntity, privilegeList);
                        if (s == 0)
                            clientVerifyMobileNewResponse.result = VAResult.VA_FAILED_WeChat_BindingUpdate;
                        clientVerifyMobileNewResponse.isExists = 0;
                        clientVerifyMobileNewResponse.cookie = clientVerifyMobileNewRequest.cookie;
                    }
                    else
                    {
                        if (vCUEntity.CustomerInfo_CustomerID == 0)
                        {
                            DataTable dtRegisterInfo = customerMan.SelectCustomerRegisterInfo(clientVerifyMobileNewRequest.cookie);
                            if (dtRegisterInfo == null)
                            {
                                clientVerifyMobileNewResponse.result = VAResult.VA_FAILED_COOKIE_NOT_FOUND;
                                return clientVerifyMobileNewResponse;
                            }

                            vCUEntity.CustomerInfo_CustomerID = long.Parse(dtRegisterInfo.Rows[0]["customerId"].ToString());

                            CustomerInfo customer = new CustomerInfo();
                            customer.cookie = clientVerifyMobileNewRequest.cookie;
                            customer.mobilePhoneNumber = "";
                            customer.registerCityId = clientVerifyMobileNewRequest.cityId;

                            customer.CustomerSex = clientVerifyMobileNewRequest.sex;
                            customer.personalImgInfo = vCUEntity.HeadImgUrl;
                            customer.UserName = vCUEntity.NickName;
                            customer.CustomerID = vCUEntity.CustomerInfo_CustomerID;
                            customerMan.UpdateCustomerInfoAndphoneOfId(customer);

                            clientVerifyMobileNewResponse.cookie = clientVerifyMobileNewRequest.cookie;
                        }
                        else
                        {
                            var userEntity = new CustomerManager().GetModelOfId(vCUEntity.CustomerInfo_CustomerID);
                            if (userEntity == null)
                            {
                                clientVerifyMobileNewResponse.result = VAResult.VA_FAILED_COOKIE_NOT_FOUND;
                                return clientVerifyMobileNewResponse;
                            }

                            clientVerifyMobileNewResponse.cookie = userEntity.cookie;
                        }

                        vCUEntity.OpenId = clientVerifyMobileNewRequest.openId;
                        vCUEntity.NickName = clientVerifyMobileNewRequest.nickName;
                        vCUEntity.Sex = clientVerifyMobileNewRequest.sex;
                        vCUEntity.Province = clientVerifyMobileNewRequest.province;
                        vCUEntity.City = clientVerifyMobileNewRequest.city;
                        vCUEntity.Country = clientVerifyMobileNewRequest.country;
                        vCUEntity.HeadImgUrl = headImgUrl;
                        vCUEntity.HeadImgSize = headImgSize;
                        vCUEntity.UnionId = clientVerifyMobileNewRequest.unionId;
                        vCUEntity.ModifyUser = "System";
                        vCUEntity.ModifyIP = IPAddress;

                        IList<WeChatUserPrivilege> privilegeList = new List<WeChatUserPrivilege>();
                        if (clientVerifyMobileNewRequest.privilege != null)
                        {
                            var privilegeRegex = new Regex("[(?<privilege>.+)]").Match(clientVerifyMobileNewRequest.privilege).Groups["privilege"].Value;
                            IList<string> privilege = new List<string>();
                            if (!string.IsNullOrEmpty(privilegeRegex))
                                privilege = privilegeRegex.Split(',').ToList();
                            foreach (var item in privilege)
                            {
                                privilegeList.Add(new WeChatUserPrivilege
                                {
                                    Id = CreateCombGuid(),
                                    WeChatUser_Id = vCUEntity.Id,
                                    Privilege = item.ToString()
                                });
                            }
                        }
                        int s = new WeChatUserOperator().Update(vCUEntity, privilegeList);
                        if (s == 0)
                            clientVerifyMobileNewResponse.result = VAResult.VA_FAILED_WeChat_BindingUpdate;
                        clientVerifyMobileNewResponse.isExists = 1;
                    }

                    clientVerifyMobileNewResponse.unionId = vCUEntity.UnionId;
                    clientVerifyMobileNewResponse.result = VAResult.VA_OK;

                    #endregion
                }
            }
            else
            {
                clientVerifyMobileNewResponse.result = checkResult.result;
            }

            if (clientVerifyMobileNewResponse.result == VAResult.VA_OK && clientVerifyMobileNewResponse.userInfo != null && string.IsNullOrEmpty(clientVerifyMobileNewResponse.userInfo.displayName) && !string.IsNullOrEmpty(clientVerifyMobileNewRequest.mobilePhoneNumber))
            {
                clientVerifyMobileNewResponse.userInfo.displayName = new SystemConfigCacheLogic().GetSystemConfig("visitorDefaultName", "悠先吃货") + clientVerifyMobileNewRequest.mobilePhoneNumber.Substring(7);
            }

            clientVerifyMobileNewResponse.verificationCodeDigit = int.Parse(new SystemConfigCacheLogic().GetSystemConfig("verificationCodeDigit", "5"));

            if (clientVerifyMobileNewResponse.result == VAResult.VA_OK)
            {
                var cDal = new CustomerManager();
                if (cDal.IsExistUuid(clientVerifyMobileNewResponse.cookie, clientVerifyMobileNewRequest.uuid) == 0)
                    cDal.InsertUuid(clientVerifyMobileNewResponse.cookie, clientVerifyMobileNewRequest.uuid, (int)clientVerifyMobileNewRequest.appType, clientVerifyMobileNewRequest.clientBuild);
            }

            return clientVerifyMobileNewResponse;
        }

        /// <summary>
        ///  微信用户绑定手机号
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public VAClientWeChatBindingMobileResponse ClientWeChatUserBindingMobile(VAClientWeChatBindingMobileRequest entity)
        {
            VAClientWeChatBindingMobileResponse weChatBindingMobileEntity = new VAClientWeChatBindingMobileResponse() { type = VAMessageType.CLIENT_CUSTOMER_WECHAT_BINDINGMOBILE_RESPONSE };
            if (string.IsNullOrEmpty(entity.mobilePhoneNumber) || string.IsNullOrEmpty(entity.unionId))
            {
                weChatBindingMobileEntity.result = VAResult.VA_FAILED_MSG_ERROR;
                return weChatBindingMobileEntity;
            }

            var customerDal = new CustomerManager();
            var userEntity = customerDal.GetModelOfUnionId(entity.unionId);
            weChatBindingMobileEntity.cookie = userEntity.cookie;
            weChatBindingMobileEntity.uuid = entity.uuid;
            if (userEntity == null)
            {
                weChatBindingMobileEntity.result = VAResult.VA_FAILED_FIND_CUSTOMER;
                return weChatBindingMobileEntity;
            }

            if (!string.IsNullOrEmpty(userEntity.mobilePhoneNumber) && entity.mobilePhoneNumber != userEntity.mobilePhoneNumber)
            {
                weChatBindingMobileEntity.result = VAResult.VA_FAILED_WeChat_BindingExists;
                return weChatBindingMobileEntity;
            }

            if (string.IsNullOrEmpty(entity.verificationCode))
            {//验证码为空则为发送验证码请求

                if (entity.mobilePhoneNumber == "23588776637")//苹果审核专用账号
                {
                    weChatBindingMobileEntity.result = VAResult.VA_OK;
                    return weChatBindingMobileEntity;
                }
                else
                {
                    weChatBindingMobileEntity.result = SendVerificationCodeBySmsForClient(userEntity.cookie, entity.mobilePhoneNumber, null, false, true);
                    return weChatBindingMobileEntity;
                }
            }
            SystemConfigCacheLogic systemConfigCacheLogic = new SystemConfigCacheLogic();
            //判断设备锁定
            if ((DateTime.Now - userEntity.unlockTime).Minutes < 0)
            {
                weChatBindingMobileEntity.deviceLockMessage = "您验证码错误次数过多，请" + (userEntity.unlockTime - DateTime.Now).Minutes + "分钟后重新获取验证码登录";
                weChatBindingMobileEntity.result = VAResult.VA_FAILED_DEVICE_LOCKED;
                return weChatBindingMobileEntity;
            }

            //判断验证码
            if (userEntity.verificationCode.ToLower() != entity.verificationCode.ToLower() || entity.mobilePhoneNumber != userEntity.verificationCodeMobile || (DateTime.Now - userEntity.verificationCodeTime).TotalSeconds > Common.smsValidTime || entity.mobilePhoneNumber == "23588776637")
            {
                int count = systemConfigCacheLogic.GetVerificationCodeErrorCountOfCache();
                if (userEntity.verificationCodeErrCnt + 1 > count)
                {
                    double lockTime = systemConfigCacheLogic.GetDeviceLockTimeOfCache();
                    customerMan.UpdateDeviceUnlockTime(userEntity.CustomerID, DateTime.Now.AddMinutes(lockTime));
                    weChatBindingMobileEntity.deviceLockMessage = "您验证码错误次数过多，请" + lockTime + "分钟后重新获取验证码登录";
                    weChatBindingMobileEntity.result = VAResult.VA_FAILED_DEVICE_LOCKED;
                }
                weChatBindingMobileEntity.result = VAResult.VA_FAILED_VERIFICATIONCODE_WRONG;
                return weChatBindingMobileEntity;
            }

            //判断手机号是否以前有绑定
            var userMobileEntity = customerDal.GetMobileOfModel(entity.mobilePhoneNumber);
            if (userMobileEntity == null)
            {
                int s = customerDal.UpdateMobileOfId(userEntity.CustomerID, entity.mobilePhoneNumber);
                if (s == 0)
                {
                    weChatBindingMobileEntity.result = VAResult.VA_FAILED_DB_ERROR;
                    return weChatBindingMobileEntity;
                }
                //new CustomerManager().UpdateIsUpdateUserName(userEntity.CustomerID, 0);
                //返回用户信息
                weChatBindingMobileEntity.rationBalance = Common.ToDouble(new ClientExtendManager().GetCustomerCurrectInfo(userEntity.CustomerID));
                weChatBindingMobileEntity.isNewMobile = true;
            }
            else
            {
                var weChatUserDal = new WeChatUserManager();
                int s = new WeChatUserManager().UpdateCustomerIdBinding(userMobileEntity.CustomerID, entity.unionId);
                if (s == 0)
                {
                    weChatBindingMobileEntity.result = VAResult.VA_FAILED_DB_ERROR;
                    return weChatBindingMobileEntity;
                }
                //new CustomerManager().UpdateIsUpdateUserName(userEntity.CustomerID, 0);

                if (userEntity.IsUpdateUserName == 0)
                {
                    var weChatUserEntity = weChatUserDal.GetUnionIdOfModel(entity.unionId);
                    customerDal.UpdateWeChatUserRegistered(weChatUserEntity.CustomerInfo_CustomerID, weChatUserEntity.NickName, weChatUserEntity.Sex, weChatUserEntity.HeadImgUrl);
                }

                if (entity.cookie != userMobileEntity.cookie)
                {
                    var customerEntity = customerDal.GetModelOfCookieId(entity.cookie);
                    customerDal.UpdateWeChatUserRegistered(userMobileEntity.CustomerID, customerEntity.UserName, customerEntity.CustomerSex == null ? 0 : (int)customerEntity.CustomerSex, customerEntity.personalImgInfo, customerEntity.IsUpdateUserName == null ? 0 : (int)customerEntity.IsUpdateUserName, customerEntity.Picture == null ? "" : customerEntity.Picture, customerEntity.RegisterDate);
                }

                //返回用户信息
                weChatBindingMobileEntity.rationBalance = Common.ToDouble(new ClientExtendManager().GetCustomerCurrectInfo(userMobileEntity.CustomerID));
                weChatBindingMobileEntity.isNewMobile = true;
                weChatBindingMobileEntity.cookie = userMobileEntity.cookie;
            }

            weChatBindingMobileEntity.result = VAResult.VA_OK;
            weChatBindingMobileEntity.type = VAMessageType.CLIENT_CUSTOMER_WECHAT_BINDINGMOBILE_RESPONSE;

            //添加uuid
            if (weChatBindingMobileEntity.result == VAResult.VA_OK)
            {
                var cDal = new CustomerManager();
                if (cDal.IsExistUuid(weChatBindingMobileEntity.cookie, entity.uuid) == 0)
                    cDal.InsertUuid(weChatBindingMobileEntity.cookie, entity.uuid, (int)entity.appType, entity.clientBuild);
            }

            CheckCookieAndMsgtypeInfo checkResult = Common.CheckCookieAndMsgtype(weChatBindingMobileEntity.cookie, entity.uuid, (int)entity.type, (int)VAMessageType.CLIENT_CUSTOMER_WECHAT_BINDINGMOBILE_REQUEST, false);
            DataTable dtCustomer = customerMan.SelectCustomerByMobilephone(entity.mobilePhoneNumber);
            VAAppType currentUserAppType = (VAAppType)Common.ToInt32(checkResult.dtCustomer.Rows[0]["appType"]);
            weChatBindingMobileEntity.userInfo = GetUserInfo(dtCustomer, entity.uuid, entity.pushToken, entity.cityId, currentUserAppType, weChatBindingMobileEntity.clientBuild);

            ShopManager shopMan = new ShopManager();
            DataTable dtShopVipInfo = shopMan.SelectShopVipInfo(entity.shopId);//查询当前门店的VIP等级信息
            if (dtShopVipInfo.Rows.Count > 0)
            {
                int currentVipGride = Common.ToInt32(dtCustomer.Rows[0]["currentPlatformVipGrade"]);
                VAShopVipInfo userVipInfo = new VAShopVipInfo();
                ClientExtendOperate.GetUserVipInfo(currentVipGride, dtShopVipInfo, userVipInfo);
                weChatBindingMobileEntity.userCurrentShopDiscount = userVipInfo.discount;
            }
            else
                weChatBindingMobileEntity.userCurrentShopDiscount = 1;

            #region 返回用户的红包金额及优惠券信息

            if (Common.CheckLatestBuild_February(entity.appType, entity.clientBuild))
            {
                var operate = new CouponOperate();
                List<OrderPaymentCouponDetail> list = operate.GetShopCouponDetails(entity.mobilePhoneNumber, entity.shopId);
                weChatBindingMobileEntity.couponDetails = list;
            }
            else
                weChatBindingMobileEntity.couponDetails = new List<OrderPaymentCouponDetail>();
            ShopInfo shopInfo = new ShopOperate().QueryShop(entity.shopId);

            if (Common.CheckLatestBuild_August(entity.appType, entity.clientBuild))
            {
                RedEnvelopeOperate redEnvelopeOperate = new RedEnvelopeOperate();
                double executedRedEnvelopeAmount = redEnvelopeOperate.QueryCustomerExcutedRedEnvelope(entity.mobilePhoneNumber);
                weChatBindingMobileEntity.executedRedEnvelopeAmount = shopInfo == null ? 0 : (shopInfo.isSupportRedEnvelopePayment ? executedRedEnvelopeAmount : 0);
            }
            else
                weChatBindingMobileEntity.executedRedEnvelopeAmount = 0;

            #endregion

            if (weChatBindingMobileEntity.result == VAResult.VA_OK && weChatBindingMobileEntity.userInfo != null && string.IsNullOrEmpty(weChatBindingMobileEntity.userInfo.displayName) && !string.IsNullOrEmpty(entity.mobilePhoneNumber))
                weChatBindingMobileEntity.userInfo.displayName = new SystemConfigCacheLogic().GetSystemConfig("visitorDefaultName", "悠先吃货") + entity.mobilePhoneNumber.Substring(7);

            return weChatBindingMobileEntity;
        }

        /// <summary>
        /// 手机是否绑定微信号
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public VAClientWeChatIsBindingMobileResponse ClientWeChatUserIsBindingMobile(VAClientWeChatIsBindingMobileRequest entity)
        {
            VAClientWeChatIsBindingMobileResponse weChatIsBindingMobileEntity = new VAClientWeChatIsBindingMobileResponse() { type = VAMessageType.CLIENT_CUSTOMER_WECHAT_ISBINDINGMOBILE_RESPONSE };
            if (string.IsNullOrEmpty(entity.mobilePhoneNumber) || string.IsNullOrEmpty(entity.unionId))
            {
                weChatIsBindingMobileEntity.result = VAResult.VA_FAILED_MSG_ERROR;
                return weChatIsBindingMobileEntity;
            }
            var customerDal = new CustomerManager();
            var userEntity = customerDal.GetMobileOfModel(entity.mobilePhoneNumber);
            if (userEntity == null)
            {
                weChatIsBindingMobileEntity.result = VAResult.VA_FAILED_DB_ERROR;
                return weChatIsBindingMobileEntity;
            }

            var customerEntity = customerDal.GetModelOfUnionId(entity.unionId);
            if (customerEntity != null && !string.IsNullOrEmpty(customerEntity.mobilePhoneNumber) && userEntity.CustomerID != customerEntity.CustomerID)
            {
                weChatIsBindingMobileEntity.isBinding = 1;
                weChatIsBindingMobileEntity.result = VAResult.VA_OK;
                weChatIsBindingMobileEntity.message = string.Format(new SystemConfigCacheLogic().GetSystemConfig("WeChatUserIsBinding", "该微信号已与手机号{0}解绑，下次登录帐户{0}，需要换新微信号绑定登录!"), customerEntity.mobilePhoneNumber);
                return weChatIsBindingMobileEntity;
            }
            weChatIsBindingMobileEntity.result = VAResult.VA_OK;
            return weChatIsBindingMobileEntity;


            #region 废代码



            //var weChatUserDal = new WeChatUserManager();
            //var userCustomerIdEntity = weChatUserDal.GetCustomerIDToEntity(userEntity.CustomerID);

            ////var userUnionIdEntity = weChatUserDal.GetUnionIdOfModel(entity.unionId);
            //////双方互相绑定
            ////if (userCustomerIdEntity != null && userUnionIdEntity != null && userCustomerIdEntity.CustomerInfo_CustomerID != 0 && userUnionIdEntity.CustomerInfo_CustomerID != 0 && userCustomerIdEntity.CustomerInfo_CustomerID != userUnionIdEntity.CustomerInfo_CustomerID)
            ////{
            ////    var customerinfoEntity = customerDal.GetModelOfId(userUnionIdEntity.CustomerInfo_CustomerID);
            ////    if (!string.IsNullOrEmpty(customerinfoEntity.mobilePhoneNumber))
            ////    {
            ////        weChatIsBindingMobileEntity.isBinding = 1;
            ////        weChatIsBindingMobileEntity.result = VAResult.VA_OK;
            ////        weChatIsBindingMobileEntity.message = string.Format(new SystemConfigCacheLogic().GetSystemConfig("WeChatUserIsBinding", "该微信号已与手机号{0}解绑，下次登录帐户{0}，需要换新微信号绑定登录!"), customerinfoEntity.mobilePhoneNumber);
            ////        return weChatIsBindingMobileEntity;
            ////    }
            ////}

            //if (userCustomerIdEntity != null && userCustomerIdEntity.UnionId != entity.unionId)
            //{
            //    weChatIsBindingMobileEntity.isBinding = 1;
            //    weChatIsBindingMobileEntity.result = VAResult.VA_OK;
            //    weChatIsBindingMobileEntity.message = string.Format(new SystemConfigCacheLogic().GetSystemConfig("WeChatUserIsBinding", "该微信号已与手机号{0}解绑，下次登录帐户{0}，需要换新微信号绑定登录!"), userEntity.mobilePhoneNumber);
            //    return weChatIsBindingMobileEntity;
            //}

            //weChatIsBindingMobileEntity.result = VAResult.VA_OK;
            //return weChatIsBindingMobileEntity;

            #endregion
        }

        /// <summary>
        /// 用户修改密码
        /// </summary>
        /// <param name="clientModifyPasswordRequest"></param>
        /// <returns></returns>
        public VAClientModifyPasswordResponse ClientModifyPassword(VAClientModifyPasswordRequest clientModifyPasswordRequest)
        {
            VAClientModifyPasswordResponse clientModifyPasswordResponse = new VAClientModifyPasswordResponse();
            clientModifyPasswordResponse.type = VAMessageType.CLIENT_MODIFY_PASSWORD_RESPONSE;
            clientModifyPasswordResponse.cookie = clientModifyPasswordRequest.cookie;
            clientModifyPasswordResponse.uuid = clientModifyPasswordRequest.uuid;
            CheckCookieAndMsgtypeInfo checkResult = Common.CheckCookieAndMsgtype(clientModifyPasswordRequest.cookie,
                clientModifyPasswordRequest.uuid, (int)clientModifyPasswordRequest.type, (int)VAMessageType.CLIENT_MODIFY_PASSWORD_REQUEST);
            if (checkResult.result == VAResult.VA_OK)
            {
                string oldPasswordMD5 = Common.ToString(checkResult.dtCustomer.Rows[0]["Password"]);
                if (!string.IsNullOrEmpty(oldPasswordMD5))
                {
                    if (string.Equals(clientModifyPasswordRequest.oldPasswordMD5, oldPasswordMD5))
                    {
                        if (!string.IsNullOrEmpty(clientModifyPasswordRequest.passwordMD5))
                        {
                            using (TransactionScope scope = new TransactionScope())
                            {
                                CustomerManager customerMan = new CustomerManager();
                                if (customerMan.UpdateCustomerPasswordByCookie(clientModifyPasswordRequest.cookie, clientModifyPasswordRequest.passwordMD5))
                                {
                                    scope.Complete();
                                    clientModifyPasswordResponse.result = VAResult.VA_OK;
                                }
                                else
                                {
                                    clientModifyPasswordResponse.result = VAResult.VA_FAILED_DB_ERROR;
                                }
                            }
                        }
                        else
                        {
                            clientModifyPasswordResponse.result = VAResult.VA_FAILED_PASSWORD_NULL;
                        }
                    }
                    else
                    {
                        clientModifyPasswordResponse.result = VAResult.VA_FAILED_PASSWORD_WRONG;
                    }
                }
                else
                {
                    clientModifyPasswordResponse.result = VAResult.VA_FAILED_USER_NOT_BIND;
                }
            }
            else
            {
                clientModifyPasswordResponse.result = checkResult.result;
            }
            return clientModifyPasswordResponse;
        }
        /// <summary>
        /// 用户忘记密码
        /// </summary>
        /// <param name="clientForgetPasswordRequest"></param>
        /// <returns></returns>
        public VAClientForgetPasswordResponse ClientForgetPassword(VAClientForgetPasswordRequest clientForgetPasswordRequest)
        {
            VAClientForgetPasswordResponse clientForgetPasswordResponse = new VAClientForgetPasswordResponse();
            clientForgetPasswordResponse.type = VAMessageType.CLIENT_FORGET_PASSWORD_RESPONSE;
            clientForgetPasswordResponse.cookie = clientForgetPasswordRequest.cookie;
            clientForgetPasswordResponse.uuid = clientForgetPasswordRequest.uuid;
            if (!string.IsNullOrEmpty(clientForgetPasswordRequest.mobilePhone))
            {
                CustomerManager customerMan = new CustomerManager();
                DataTable dtCustomer = customerMan.SelectCustomerByMobilephone(clientForgetPasswordRequest.mobilePhone);
                if (dtCustomer.Rows.Count == 1)
                {
                    string email = Common.ToString(dtCustomer.Rows[0]["customerEmail"]);
                    if (!string.IsNullOrEmpty(email))
                    {
                        using (TransactionScope scope = new TransactionScope())
                        {
                            string subject = "[悠先]密码重置";
                            string messageBody = ConfigForgetPasswordEmailBody(Common.ToInt64(dtCustomer.Rows[0]["CustomerID"]));
                            if (!string.IsNullOrEmpty(messageBody))
                            {
                                if (Common.SendEmailFrom19dianService(email, messageBody, subject))
                                {
                                    scope.Complete();
                                    clientForgetPasswordResponse.result = VAResult.VA_OK;
                                }
                                else
                                {
                                    clientForgetPasswordResponse.result = VAResult.VA_FAILED_EMAIL_SEND_ERROR;
                                }
                            }
                            else
                            {
                                clientForgetPasswordResponse.result = VAResult.VA_FAILED_EMAIL_BODY_ERROR;
                            }
                        }
                    }
                    else
                    {
                        clientForgetPasswordResponse.result = VAResult.VA_FAILED_EMAIL_NOT_BIND;
                    }
                }
                else
                {
                    clientForgetPasswordResponse.result = VAResult.VA_FAILED_PHONENUMBER_NOT_FOUND;
                }
            }
            else
            {
                clientForgetPasswordResponse.result = VAResult.VA_FAILED_PHONENUMBER_NULL;
            }
            return clientForgetPasswordResponse;
        }
        /// <summary>
        /// 验证忘记密码的验证码是否有效
        /// </summary>
        /// <param name="veriryCode"></param>
        /// <returns></returns>
        public bool CheckForgetPasswordVeriryCode(string veriryCode)
        {
            CustomerManager customerMan = new CustomerManager();
            DataTable dtCustomerForgetPassword = customerMan.SelectCustomerForgetPassword(veriryCode);
            if (dtCustomerForgetPassword.Rows.Count == 1)
            {
                if ((System.DateTime.Now - Common.ToDateTime(dtCustomerForgetPassword.Rows[0]["sendEmailTime"])) < TimeSpan.FromMinutes(10))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 根据忘记密码后生成的验证码修改密码
        /// 暂时只用于Web端
        /// </summary>
        /// <param name="clientResetPasswordRequest"></param>
        /// <returns></returns>
        public VAClientResetPasswordResponse ClientResetPassword(VAClientResetPasswordRequest clientResetPasswordRequest)
        {
            VAClientResetPasswordResponse clientResetPasswordResponse = new VAClientResetPasswordResponse();
            if (!string.IsNullOrEmpty(clientResetPasswordRequest.veriryCode) && !string.IsNullOrEmpty(clientResetPasswordRequest.myNewPasswordMD5))
            {
                CustomerManager customerMan = new CustomerManager();
                DataTable dtCustomerForgetPassword = customerMan.SelectCustomerForgetPassword(clientResetPasswordRequest.veriryCode);
                if (dtCustomerForgetPassword.Rows.Count == 1)
                {
                    if ((System.DateTime.Now - Common.ToDateTime(dtCustomerForgetPassword.Rows[0]["sendEmailTime"])) < TimeSpan.FromMinutes(10))
                    {
                        using (TransactionScope scope = new TransactionScope())
                        {
                            bool updateCustomerPassword = customerMan.UpdateCustomerPassword(Common.ToInt64(dtCustomerForgetPassword.Rows[0]["customerId"]),
                                clientResetPasswordRequest.myNewPasswordMD5);
                            bool updateCustomerForgetPassword = customerMan.UpdateCustomerForgetPassword(clientResetPasswordRequest.veriryCode);
                            if (updateCustomerPassword && updateCustomerForgetPassword)
                            {
                                scope.Complete();
                                clientResetPasswordResponse.result = VAResult.VA_OK;
                            }
                            else
                            {
                                clientResetPasswordResponse.result = VAResult.VA_FAILED_DB_ERROR;
                            }
                        }
                    }
                    else
                    {
                        clientResetPasswordResponse.result = VAResult.VA_FAILED_OTHER;
                    }
                }
                else
                {
                    clientResetPasswordResponse.result = VAResult.VA_FAILED_OTHER;
                }
            }
            else
            {
                clientResetPasswordResponse.result = VAResult.VA_FAILED_OTHER;
            }
            return clientResetPasswordResponse;
        }
        /// <summary>
        /// 用户绑定第三方登录信息
        /// </summary>
        /// <param name="clientBindOpenIdRequest"></param>
        /// <returns></returns>
        public VAClientBindOpenIdResponse ClientBindOpenId(VAClientBindOpenIdRequest clientBindOpenIdRequest)
        {
            VAClientBindOpenIdResponse clientBindOpenIdResponse = new VAClientBindOpenIdResponse();
            clientBindOpenIdResponse.type = VAMessageType.CLIENT_BIND_OPEN_ID_RESPONSE;
            clientBindOpenIdResponse.cookie = clientBindOpenIdRequest.cookie;
            clientBindOpenIdResponse.uuid = clientBindOpenIdRequest.uuid;
            CheckCookieAndMsgtypeInfo checkResult = Common.CheckCookieAndMsgtype(clientBindOpenIdRequest.cookie,
                clientBindOpenIdRequest.uuid, (int)clientBindOpenIdRequest.type, (int)VAMessageType.CLIENT_BIND_OPEN_ID_REQUEST);
            if (checkResult.result == VAResult.VA_OK)
            {
                if (!string.IsNullOrEmpty(clientBindOpenIdRequest.openIdUid))
                {
                    CustomerManager customerMan = new CustomerManager();
                    if (customerMan.IsOpenIdUidExist(clientBindOpenIdRequest.openIdUid, clientBindOpenIdRequest.vendor))
                    {//该OpenIdUid已经存在
                        if (customerMan.UpdateCustomerOpenId(clientBindOpenIdRequest.openIdUid, Common.ToDateTimeFrom1970(clientBindOpenIdRequest.expirationDate)))
                        {
                            clientBindOpenIdResponse.result = VAResult.VA_OK;
                        }
                        else
                        {
                            clientBindOpenIdResponse.result = VAResult.VA_FAILED_DB_ERROR;
                        }
                    }
                    else
                    {
                        using (TransactionScope scope = new TransactionScope())
                        {
                            OpenIdInfo openId = new OpenIdInfo();
                            openId.customerId = Common.ToInt64(checkResult.dtCustomer.Rows[0]["CustomerID"]);
                            openId.openIdUid = clientBindOpenIdRequest.openIdUid;
                            openId.openIdBindTime = System.DateTime.Now;
                            openId.expirationDate = Common.ToDateTimeFrom1970(clientBindOpenIdRequest.expirationDate);
                            openId.openIdType = clientBindOpenIdRequest.vendor;
                            openId.openIdUpdateTime = Common.ToDateTimeFrom1970(clientBindOpenIdRequest.expirationDate);
                            if (customerMan.InsertOpenId(openId) > 0)
                            {
                                scope.Complete();
                                clientBindOpenIdResponse.result = VAResult.VA_OK;
                            }
                            else
                            {
                                clientBindOpenIdResponse.result = VAResult.VA_FAILED_DB_ERROR;
                            }
                        }
                    }
                }
                else
                {
                    clientBindOpenIdResponse.result = VAResult.VA_FAILED_OPENIDUID_NULL;
                }
            }
            else
            {
                clientBindOpenIdResponse.result = checkResult.result;
            }
            return clientBindOpenIdResponse;
        }
        /// <summary>
        /// 根据用户Id，查询设备信息
        /// tdq
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public DataTable QueryCustomerTopDevice(long customerId)
        {
            CustomerManager customerMan = new CustomerManager();
            DataTable device = customerMan.SelectCustomerTopDevice(customerId);
            return device;
        }
        /// <summary>
        /// 客户端查询对应的用户钱记录20140313
        /// </summary>
        /// <param name="userWalletTransactionListRequest"></param>
        /// <returns></returns>
        public VAUserWalletTransactionListResponse ClientQueryUserWallet(VAUserWalletTransactionListRequest userWalletTransactionListRequest)
        {
            CustomerManager customerMan = new CustomerManager();
            VAUserWalletTransactionListResponse userWalletTransactionListResponse = new VAUserWalletTransactionListResponse();
            userWalletTransactionListResponse.type = VAMessageType.USER_WALLET_TRANSACTION_LIST_RESPONSE;
            userWalletTransactionListResponse.cookie = userWalletTransactionListRequest.cookie;
            userWalletTransactionListResponse.uuid = userWalletTransactionListRequest.uuid;
            CheckCookieAndMsgtypeInfo checkResult = Common.CheckCookieAndMsgtype(userWalletTransactionListRequest.cookie,
                userWalletTransactionListRequest.uuid, (int)userWalletTransactionListRequest.type, (int)VAMessageType.USER_WALLET_TRANSACTION_LIST_REQUEST);
            if (checkResult.result == VAResult.VA_OK)
            {
                userWalletTransactionListResponse.result = VAResult.VA_OK;
                long customerId = Common.ToInt64(checkResult.dtCustomer.Rows[0]["CustomerID"]);
                userWalletTransactionListResponse.current = Common.ToDouble(checkResult.dtCustomer.Rows[0]["money19dianRemained"]);
                DataTable dtCustomerMoneyDetail = customerMan.SelectCustomerMoneyDetail(customerId);
                if (dtCustomerMoneyDetail.Rows.Count > 0)
                {
                    List<VAUserWalletHistoryRecord> walletHistoryRecordList = new List<VAUserWalletHistoryRecord>();
                    for (int i = 0; i < dtCustomerMoneyDetail.Rows.Count; i++)
                    {
                        VAUserWalletHistoryRecord walletHistoryRecord = new VAUserWalletHistoryRecord();
                        walletHistoryRecord.date = Common.ToSecondFrom1970(Common.ToDateTime(dtCustomerMoneyDetail.Rows[i]["changeTime"]));
                        walletHistoryRecord.value = Common.ToDouble(dtCustomerMoneyDetail.Rows[i]["changeValue"]);
                        walletHistoryRecord.reason = Common.ToString(dtCustomerMoneyDetail.Rows[i]["changeReason"]);
                        walletHistoryRecordList.Add(walletHistoryRecord);
                    }
                    userWalletTransactionListResponse.historyRecords = walletHistoryRecordList;
                }
            }
            else
            {
                userWalletTransactionListResponse.result = checkResult.result;
            }
            return userWalletTransactionListResponse;
        }
        /// <summary>
        /// 客户端添加/删除收藏
        /// </summary>
        /// <param name="userSetFavoriteCompanyRequest"></param>
        /// <returns></returns>
        public VAUserSetFavoriteCompanyResponse ClientSetFavoriteCompany(VAUserSetFavoriteCompanyRequest userSetFavoriteCompanyRequest)
        {
            VAUserSetFavoriteCompanyResponse userSetFavoriteCompanyResponse = new VAUserSetFavoriteCompanyResponse();
            userSetFavoriteCompanyResponse.type = VAMessageType.USER_SET_FAVORITE_COMPANY_RESPONSE;
            userSetFavoriteCompanyResponse.cookie = userSetFavoriteCompanyRequest.cookie;
            userSetFavoriteCompanyResponse.uuid = userSetFavoriteCompanyRequest.uuid;
            userSetFavoriteCompanyResponse.companyId = userSetFavoriteCompanyRequest.companyId;
            userSetFavoriteCompanyResponse.isFavorite = userSetFavoriteCompanyRequest.isFavorite;
            CheckCookieAndMsgtypeInfo checkResult = Common.CheckCookieAndMsgtype(userSetFavoriteCompanyRequest.cookie,
                userSetFavoriteCompanyRequest.uuid, (int)userSetFavoriteCompanyRequest.type, (int)VAMessageType.USER_SET_FAVORITE_COMPANY_REQUEST);
            if (checkResult.result == VAResult.VA_OK)
            {
                CustomerManager customerMan = new CustomerManager();
                long customerId = Common.ToInt64(checkResult.dtCustomer.Rows[0]["CustomerID"]);
                DataTable dtCustomerFavoriteCompany = customerMan.SelectCustomerFavoriteCompany(customerId);
                DataView dvCustomerFavoriteCompany = dtCustomerFavoriteCompany.DefaultView;
                dvCustomerFavoriteCompany.RowFilter = "companyId = '" + userSetFavoriteCompanyRequest.companyId + "'";
                if (userSetFavoriteCompanyRequest.isFavorite)
                {
                    if (dvCustomerFavoriteCompany.Count == 0)
                    {
                        CustomerFavoriteCompany customerFavoriteCompany = new CustomerFavoriteCompany();
                        customerFavoriteCompany.customerId = customerId;
                        customerFavoriteCompany.companyId = userSetFavoriteCompanyRequest.companyId;
                        using (TransactionScope scope = new TransactionScope())
                        {
                            if (customerMan.InsertCustomerFavoriteCompany(customerFavoriteCompany) > 0)
                            {
                                scope.Complete();
                                userSetFavoriteCompanyResponse.result = VAResult.VA_OK;
                            }
                            else
                            {
                                userSetFavoriteCompanyResponse.result = VAResult.VA_FAILED_DB_ERROR;
                            }
                        }
                    }
                    else
                    {
                        userSetFavoriteCompanyResponse.result = VAResult.VA_FAILED_COMPANY_COLLECTED;
                    }
                }
                else
                {
                    if (dvCustomerFavoriteCompany.Count == 1)
                    {
                        using (TransactionScope scope = new TransactionScope())
                        {
                            if (customerMan.DeleteCustomerFavoriteCompany(Common.ToInt64(dvCustomerFavoriteCompany[0]["id"])))
                            {
                                scope.Complete();
                                userSetFavoriteCompanyResponse.result = VAResult.VA_OK;
                            }
                            else
                            {
                                userSetFavoriteCompanyResponse.result = VAResult.VA_FAILED_DB_ERROR;
                            }
                        }
                    }
                    else
                    {
                        userSetFavoriteCompanyResponse.result = VAResult.VA_FAILED_COMPANY_NOT_COLLECTED;
                    }
                }
            }
            else
            {
                userSetFavoriteCompanyResponse.result = checkResult.result;
            }
            return userSetFavoriteCompanyResponse;
        }
        /// <summary>
        /// 客户端注册用户邀请用户20140313
        /// </summary>
        /// <param name="clientInviteCustomerRequest"></param>
        /// <returns></returns>
        public VAClientInviteCustomerResponse ClientInviteCustomer(VAClientInviteCustomerRequest clientInviteCustomerRequest)
        {
            VAClientInviteCustomerResponse clientInviteCustomerResponse = new VAClientInviteCustomerResponse();
            clientInviteCustomerResponse.type = VAMessageType.CLIENT_INVITE_CUSTOMER_RESPONSE;
            clientInviteCustomerResponse.cookie = clientInviteCustomerRequest.cookie;
            clientInviteCustomerResponse.uuid = clientInviteCustomerRequest.uuid;
            CheckCookieAndMsgtypeInfo checkResult = Common.CheckCookieAndMsgtype(clientInviteCustomerRequest.cookie,
                clientInviteCustomerRequest.uuid, (int)clientInviteCustomerRequest.type, (int)VAMessageType.CLIENT_INVITE_CUSTOMER_REQUEST);
            if (checkResult.result == VAResult.VA_OK)
            {
                string currentUserMobilePhone = Common.ToString(checkResult.dtCustomer.Rows[0]["mobilePhoneNumber"]);
                if (IsMobileVerified(currentUserMobilePhone))
                {
                    if (string.IsNullOrEmpty(clientInviteCustomerRequest.mobilePhoneInvited))
                    {
                        clientInviteCustomerResponse.result = VAResult.VA_FAILED_MOBILEPHONE_INVITED_NULL;
                    }
                    else
                    {
                        CustomerInviteRecord customerInviteRecord = new CustomerInviteRecord();
                        customerInviteRecord.customerId = Common.ToInt64(checkResult.dtCustomer.Rows[0]["CustomerID"]);
                        customerInviteRecord.inviteTime = DateTime.Now;
                        customerInviteRecord.phoneNumberInvite = currentUserMobilePhone;
                        customerInviteRecord.phoneNumberInvited = clientInviteCustomerRequest.mobilePhoneInvited;
                        CustomerManager customerMan = new CustomerManager();
                        if (customerMan.InsertCustomerInviteRecord(customerInviteRecord) > 0)
                        {
                            clientInviteCustomerResponse.result = VAResult.VA_OK;
                        }
                        else
                        {
                            clientInviteCustomerResponse.result = VAResult.VA_FAILED_DB_ERROR;
                        }
                    }
                }
                else
                {
                    clientInviteCustomerResponse.result = VAResult.VA_FAILED_USER_NOT_BIND;
                }
            }
            else
            {
                clientInviteCustomerResponse.result = checkResult.result;
            }
            return clientInviteCustomerResponse;
        }
        /// <summary>
        /// 用户挂失
        /// 暂时只用于Web
        /// </summary>
        /// <param name="clientCustomerReportLossRequest"></param>
        /// <returns></returns>
        public VAClientCustomerReportLossResponse ClientCustomerReportLoss(VAClientCustomerReportLossRequest clientCustomerReportLossRequest)
        {
            VAClientCustomerReportLossResponse clientCustomerReportLossResponse = new VAClientCustomerReportLossResponse();
            if (!string.IsNullOrEmpty(clientCustomerReportLossRequest.mobilePhone) && !string.IsNullOrEmpty(clientCustomerReportLossRequest.passwordMD5))
            {
                CustomerManager customerMan = new CustomerManager();
                DataTable dtCustomer = customerMan.SelectCustomerByMobilephone(clientCustomerReportLossRequest.mobilePhone);
                if (dtCustomer.Rows.Count == 1)
                {
                    if (string.Equals(clientCustomerReportLossRequest.passwordMD5, Common.ToString(dtCustomer.Rows[0]["Password"])))
                    {//密码匹配
                        string newCookie = Common.ToString(Guid.NewGuid()) + Common.ToString(System.DateTime.Now.Ticks);
                        long customerId = Common.ToInt64(dtCustomer.Rows[0]["CustomerID"]);
                        using (TransactionScope scope = new TransactionScope())
                        {
                            if (customerMan.UpdateCustomerCookie(customerId, newCookie))
                            {
                                scope.Complete();
                                clientCustomerReportLossResponse.result = VAResult.VA_OK;
                            }
                            else
                            {
                                clientCustomerReportLossResponse.result = VAResult.VA_FAILED_DB_ERROR;
                            }
                        }
                    }
                    else
                    {
                        clientCustomerReportLossResponse.result = VAResult.VA_FAILED_PASSWORD_WRONG;
                    }
                }
                else
                {
                    clientCustomerReportLossResponse.result = VAResult.VA_FAILED_OTHER;
                }
            }
            else
            {
                clientCustomerReportLossResponse.result = VAResult.VA_FAILED_OTHER;
            }
            return clientCustomerReportLossResponse;
        }
        /// <summary>
        /// 更新用户余额
        /// 外层自建回滚事务
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool ModifyCustomerMoney19dianRemained(long customerId, double value)
        {
            CustomerManager customerMan = new CustomerManager();
            return customerMan.UpdateMoney19dianRemained(customerId, value);
        }
        /// <summary>
        /// 用户充值
        /// </summary>
        /// <param name="topUpRequest"></param>
        /// <returns></returns>
        public VATopUpResponse ClientTopUp(VATopUpRequest topUpRequest)
        {
            VATopUpResponse topUpResponse = new VATopUpResponse();
            topUpResponse.type = VAMessageType.CLIENT_TOPUP_RESPONSE;
            topUpResponse.cookie = topUpRequest.cookie;
            topUpResponse.uuid = topUpRequest.uuid;
            CheckCookieAndMsgtypeInfo checkResult = Common.CheckCookieAndMsgtype(topUpRequest.cookie,
                topUpRequest.uuid, (int)topUpRequest.type, (int)VAMessageType.CLIENT_TOPUP_REQUEST);
            if (checkResult.result == VAResult.VA_OK)
            {
                if (topUpRequest.topUpAmount > 0)
                {
                    long customerId = Common.ToInt64(checkResult.dtCustomer.Rows[0]["CustomerID"]);
                    using (TransactionScope scope = new TransactionScope())
                    {
                        CustomerChargeOrder customerChargeOrder = new CustomerChargeOrder();
                        customerChargeOrder.createTime = DateTime.Now;
                        customerChargeOrder.customerCookie = topUpRequest.cookie;
                        customerChargeOrder.customerId = customerId;
                        customerChargeOrder.customerUUID = topUpRequest.uuid;
                        customerChargeOrder.status = VACustomerChargeOrderStatus.NOT_PAID;
                        customerChargeOrder.subjectName = "饭票";
                        long customerChargeOrderId = customerMan.InsertCustomerChargeOrder(customerChargeOrder);
                        if (customerChargeOrderId > 0)
                        {
                            //scope.Complete();
                            switch (topUpRequest.clientPayMode)
                            {
                                //不传clientPayMode时,给一个默认方式
                                default:
                                case VAClientPayMode.ALI_PAY_PLUGIN:
                                case VAClientPayMode.ALI_PAY_WEB:
                                    {
                                        #region MyRegion
                                        AlipayOperate alipayOpe = new AlipayOperate();
                                        AlipayOrderInfo alipayOrder = new AlipayOrderInfo();
                                        string callBackUrl = WebConfig.ServerDomain + Config.Call_back_url;
                                        alipayOrder.orderCreatTime = System.DateTime.Now;
                                        alipayOrder.orderStatus = VAAlipayOrderStatus.NOT_PAID;
                                        alipayOrder.totalFee = topUpRequest.topUpAmount;
                                        string subject = Common.GetEnumDescription(VAPayOrderType.PAY_CHARGE);
                                        if (subject.Length > 128)
                                        {
                                            subject = subject.Substring(0, 128);
                                        }
                                        alipayOrder.subject = subject;
                                        alipayOrder.conn19dianOrderType = VAPayOrderType.PAY_CHARGE; //支付饭票
                                        alipayOrder.connId = customerChargeOrderId;
                                        alipayOrder.customerId = customerId;
                                        long outTradeNo = alipayOpe.AddAlipayOrder(alipayOrder);//返回主键

                                        if (outTradeNo > 0)
                                        {
                                            scope.Complete();
                                            topUpResponse.result = VAResult.VA_OK;

                                            if (topUpRequest.clientPayMode == VAClientPayMode.ALI_PAY_WEB)
                                            {
                                                string urlToContinuePayment = WebConfig.ServerDomain + "alipaytrade.aspx?type=" + (int)VAPayOrderType.PAY_CHARGE
                                            + "&value=" + customerChargeOrderId + "&cookie=" + customerId + "&totalFee=" + topUpRequest.topUpAmount + "&outTradeNo=" + outTradeNo;
                                                topUpResponse.urlToContinuePayment = urlToContinuePayment;
                                            }
                                            else if (topUpRequest.clientPayMode == VAClientPayMode.ALI_PAY_PLUGIN)
                                            {
                                                //组装待签名数据
                                                string alipayOrderString = "partner=" + "\"" + Config.Partner + "\"";
                                                alipayOrderString += "&";
                                                alipayOrderString += "seller=" + "\"" + Config.Partner + "\"";
                                                alipayOrderString += "&";
                                                alipayOrderString += "out_trade_no=" + "\"" + outTradeNo + "\"";
                                                alipayOrderString += "&";
                                                alipayOrderString += "subject=" + "\"" + subject + "\"";
                                                alipayOrderString += "&";
                                                alipayOrderString += "body=" + "\"悠先充值" + "\"";
                                                alipayOrderString += "&";
                                                alipayOrderString += "total_fee=" + "\"" + topUpRequest.topUpAmount + "\"";
                                                alipayOrderString += "&";
                                                string notifyURL = WebConfig.ServerDomain + Config.QuickNotify_url;
                                                alipayOrderString += "notify_url=" + "\"" + notifyURL + "\"";
                                                string orderSign = RSAFromPkcs8.sign(alipayOrderString, Config.PrivateKey, Config.Input_charset_UTF8);
                                                alipayOrderString += "&";
                                                alipayOrderString += "sign=" + "\"" + HttpUtility.UrlEncode(orderSign, Encoding.GetEncoding(Config.Input_charset_UTF8)) + "\"";
                                                alipayOrderString += "&";
                                                alipayOrderString += "sign_type=\"RSA\"";
                                                topUpResponse.alipayOrder = HttpUtility.UrlEncode(alipayOrderString, Encoding.GetEncoding(Config.Input_charset_UTF8));
                                            }
                                        }
                                        #endregion
                                    }
                                    break;
                                case VAClientPayMode.UNION_PAY_PLUGIN:
                                    //银联支付  2013-09-11 woody.wang
                                    #region MyRegion
                                    UnionPayOperate operate = new UnionPayOperate();
                                    UnionPayInfo orderInfo = new UnionPayInfo();
                                    orderInfo.merchantOrderTime = System.DateTime.Now;
                                    orderInfo.orderStatus = VAAlipayOrderStatus.NOT_PAID;
                                    orderInfo.conn19dianOrderType = VAPayOrderType.PAY_CHARGE;
                                    orderInfo.merchantOrderAmt = (int)(topUpRequest.topUpAmount * 100);
                                    orderInfo.customerId = customerId;

                                    string subjectTwo = Common.GetEnumDescription(VAPayOrderType.PAY_CHARGE);
                                    if (subjectTwo.Length > 128)
                                    {
                                        subjectTwo = subjectTwo.Substring(0, 128);
                                    }
                                    orderInfo.merchantOrderDesc = subjectTwo;
                                    orderInfo.connId = customerChargeOrderId;  //用户充值订单信息 ID

                                    //StreamWriter sw1 = new StreamWriter("D:\\log.txt", true);
                                    //sw1.WriteLine(orderInfo.merchantOrderTime);
                                    //sw1.WriteLine(orderInfo.orderPayTime);
                                    //sw1.WriteLine(orderInfo.orderStatus);
                                    //sw1.WriteLine(orderInfo.conn19dianOrderType);
                                    //sw1.WriteLine(orderInfo.connId);
                                    //sw1.WriteLine(orderInfo.merchantOrderAmt);
                                    //sw1.WriteLine(orderInfo.merchantOrderDesc);
                                    //sw1.WriteLine(orderInfo.cupsQid);
                                    //sw1.WriteLine(orderInfo.customerId);
                                    //sw1.Close();

                                    long payOrderId = operate.AddUnionpayOrder(orderInfo);//返回主键

                                    if (payOrderId > 0)
                                    {

                                        scope.Complete();
                                        topUpResponse.result = VAResult.VA_OK;

                                        //StreamWriter sw2 = new StreamWriter("D:\\log.txt", true);
                                        //sw2.WriteLine("payOrderID=" + payOrderId);
                                        //sw2.Close();
                                        try
                                        {
                                            ////订单号,  从数据库中获取@@identity 自增长列
                                            orderInfo.merchantOrderId = UnionPayParameters.UNION_PAY_FRONT_CODE + payOrderId;

                                            //string merchantOrderId = UnionPayParameters.UNION_PAY_FRONT_CODE + payOrderId.ToString();
                                            StringBuilder originalInfo = new StringBuilder();
                                            originalInfo
                                                .Append("merchantName=").Append(UnionPayParameters.merchantName)
                                                .Append("&merchantId=").Append(UnionPayParameters.merchantId)
                                                .Append("&merchantOrderId=").Append(orderInfo.merchantOrderId)
                                                .Append("&merchantOrderTime=").Append(orderInfo.merchantOrderTime.ToString("yyyyMMddHHmmss"))
                                                .Append("&merchantOrderAmt=").Append(orderInfo.merchantOrderAmt)
                                                .Append("&merchantOrderDesc=").Append(orderInfo.merchantOrderDesc)
                                                .Append("&transTimeout=").Append(UnionPayParameters.transTimeout);

                                            string originalsign = originalInfo.ToString();
                                            string xmlSign = UnionPaySignEncode.CreateSign(originalsign, UnionPayParameters.alias, UnionPayParameters.password, UnionPayParameters.PrivatePath);

                                            string Submit = SubmitString(orderInfo, xmlSign);
                                            String returnContent = UnionPaySender.Send(UnionPayParameters.UNION_PAY_SERVER, Submit);

                                            string decodeStr = HttpUtility.UrlDecode(returnContent);

                                            XmlDocument xml = new XmlDocument();
                                            xml.LoadXml(decodeStr);

                                            string merchantId = xml.SelectSingleNode("/upomp/merchantId").InnerText;
                                            string merchantOrderIdNew = xml.SelectSingleNode("/upomp/merchantOrderId").InnerText;
                                            string merchantOrderTime = xml.SelectSingleNode("/upomp/merchantOrderTime").InnerText;
                                            string resCode = xml.SelectSingleNode("/upomp/respCode").InnerText;
                                            string resDesc = xml.SelectSingleNode("/upomp/respDesc").InnerText;

                                            //StreamWriter sw4 = new StreamWriter("D:\\log.txt", true);
                                            //sw4.WriteLine("resCode=" + resCode);
                                            //sw4.WriteLine("resDesc=" + resDesc);
                                            //sw4.Close();

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
                                                String unionPayOrder = "<?xml version=" + "'1.0' "
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

                                                //topUpResponse.unionpayOrder = HttpUtility.UrlEncode(unionPayOrder, Encoding.UTF8);
                                                topUpResponse.unionpayOrder = unionPayOrder;


                                            }
                                            else
                                            {
                                                topUpResponse.result = VAResult.VA_FAILED_UNION_PAY_ORDER_SUMMIT_ERROR;
                                            }

                                        }
                                        catch //(Exception ex)
                                        {
                                            //StreamWriter sw3 = new StreamWriter("D:\\log.txt", true);
                                            //sw3.WriteLine("哪里错了?" + ex.Message);
                                            //sw3.Close();
                                        }
                                    }
                                    else
                                    {
                                        topUpResponse.result = VAResult.VA_FAILED_DB_ERROR;
                                    }
                                    #endregion
                                    break;
                                //default:
                                //    {
                                //        topUpResponse.result = VAResult.VA_FAILED_OTHER;
                                //    }
                                //    break;
                            }
                        }
                        else
                        {
                            topUpResponse.result = VAResult.VA_FAILED_DB_ERROR;
                        }
                    }
                }
                else
                {
                    topUpResponse.result = VAResult.VA_FAILED_OTHER;
                }
            }
            else
            {
                topUpResponse.result = checkResult.result;
            }
            return topUpResponse;
        }
        /// <summary>
        /// 银联支付 生成报文
        /// </summary>
        public string SubmitString(UnionPayInfo orderInfo, string xmlSign)
        {
            #region xml格式
            //<merchantName>商户名称</ merchantName >
            //<merchantId>商户代码（15-24位数字）</merchantId>
            //<merchantOrderId>商户订单号(12-32位数字、字母)</merchantOrderId>
            //<merchantOrderTime>商户订单时间(YYYYMMDDHHMMSS)</merchantOrderTime>
            //<merchantOrderAmt>商户订单金额（12位整数, 单位为分）</merchantOrderAmt>
            //< merchantOrderDesc>描述而已 </ merchantOrderDesc>
            //< transTimeout>订单超时时间（默认为空，是24小时）</ transTimeout>
            //<sign>上方7个节点的签名值</sign>
            //< backEndUrl>商户服务器地址，用于接收交易结果通知</ backEndUrl>
            //< merchantPublicCert>公钥证书--Base64编码的结果</ merchantPublicCert>
            //</upomp> 
            #endregion
            StringBuilder sb = new StringBuilder();
            sb.Append("<?xml version=\"1.0\" encoding=\"UTF-8\" ?>");
            sb.Append("<upomp application=\"SubmitOrder.Req\" version=\"1.0.0\">");
            sb.AppendFormat("<merchantName>{0}</merchantName>", UnionPayParameters.merchantName);
            sb.AppendFormat("<merchantId>{0}</merchantId>", UnionPayParameters.merchantId);
            sb.AppendFormat("<merchantOrderId>{0}</merchantOrderId>", orderInfo.merchantOrderId);
            sb.AppendFormat("<merchantOrderTime>{0}</merchantOrderTime>", orderInfo.merchantOrderTime.ToString("yyyyMMddHHmmss"));
            sb.AppendFormat("<merchantOrderAmt>{0}</merchantOrderAmt>", orderInfo.merchantOrderAmt);
            sb.AppendFormat("<merchantOrderDesc>{0}</merchantOrderDesc>", orderInfo.merchantOrderDesc);
            sb.AppendFormat("<backEndUrl>{0}</backEndUrl>", UnionPayParameters.backEndUrl);
            sb.AppendFormat("<sign>{0}</sign>", xmlSign);
            sb.AppendFormat("<merchantPublicCert>{0}</merchantPublicCert></upomp>", UnionPayParameters.merchant_public_cer);

            return sb.ToString();
        }

        /// <summary>
        /// 修改用户充值订单状态
        /// </summary>
        /// <param name="customerChargeOrderId"></param>
        /// <returns></returns>
        public bool ModifyCustomerChargeOrderStatus(long customerChargeOrderId)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                if (customerMan.UpdateCustomerChargeStatus(customerChargeOrderId))
                {
                    scope.Complete();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        /// <summary>
        /// 微信帐号登录
        /// </summary>
        /// <param name="wechatId"></param>
        /// <returns></returns>
        public VAWechatLoginResponse VAWechatLogin(VAWechatLoginRequest wechatLoginRequest)
        {
            VAWechatLoginResponse wechatLoginResponse = new VAWechatLoginResponse();
            if (!string.IsNullOrEmpty(wechatLoginRequest.wechatId))
            {
                DataTable dtCustomer = customerMan.SelectCustomerInfoByWechatId(wechatLoginRequest.wechatId);
                if (dtCustomer.Rows.Count == 1)
                {
                    wechatLoginResponse.result = VAResult.VA_OK;
                    wechatLoginResponse.eVip = "";
                    wechatLoginResponse.cookie = Common.ToString(dtCustomer.Rows[0]["cookie"]);
                    wechatLoginResponse.uuid = Common.ToString(dtCustomer.Rows[0]["uuid"]);
                }
                else
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        VAClientRegisterRequest clientRegisterRequest = new VAClientRegisterRequest();
                        clientRegisterRequest.wechatId = wechatLoginRequest.wechatId;
                        clientRegisterRequest.type = VAMessageType.CLIENT_REGISTER_REQUEST;
                        clientRegisterRequest.uuid = Common.ToString(Guid.NewGuid()) + Common.ToString(System.DateTime.Now.Ticks);
                        clientRegisterRequest.appType = VAAppType.WAP;
                        VAClientRegisterResponse clientRegisterResponse = ClientRegister(clientRegisterRequest);
                        if (clientRegisterResponse.result == VAResult.VA_OK)
                        {
                            scope.Complete();
                            wechatLoginResponse.result = VAResult.VA_OK;
                            wechatLoginResponse.cookie = clientRegisterResponse.cookie;
                            wechatLoginResponse.eVip = clientRegisterResponse.eCardNumber;
                            wechatLoginResponse.uuid = clientRegisterResponse.uuid;
                        }
                        else
                        {
                            wechatLoginResponse.result = VAResult.VA_FAILED_OTHER;
                            wechatLoginResponse.cookie = "";
                            wechatLoginResponse.eVip = "";
                            wechatLoginResponse.uuid = "";
                        }
                    }
                }
            }
            else
            {
                wechatLoginResponse.result = VAResult.VA_FAILED_OTHER;
                wechatLoginResponse.cookie = "";
                wechatLoginResponse.eVip = "";
                wechatLoginResponse.uuid = "";
            }
            return wechatLoginResponse;
        }
        /// <summary>
        /// 传入用户信息生成userInfo结构，并插入用户登录表，更新pushToken 20140313
        /// </summary>
        /// <param name="dtCustomer"></param>
        /// <param name="uuid"></param>
        /// <param name="pushToken"></param>
        /// <returns></returns>
        private VAUserInfo GetUserInfo(DataTable dtCustomer, string uuid, string pushToken, int cityId, VAAppType appType, string currectBuild)
        {
            DataRow dtCustomer_Rows = dtCustomer.Rows[0];
            VAUserInfo userInfo = new VAUserInfo();
            DateTime currentTime = System.DateTime.Now;
            userInfo.mobilePhone = Common.ToString(dtCustomer_Rows["mobilePhoneNumber"]);
            userInfo.displayName = Common.ToString(dtCustomer_Rows["UserName"]);
            long customerId = Common.ToInt64(dtCustomer_Rows["CustomerID"]);
            userInfo.walletCash = Common.ToDouble(new ClientExtendManager().GetCustomerCurrectInfo(customerId));
            // userInfo.walletCash = Common.ToDouble(dtCustomer_Rows["money19dianRemained"]);
            userInfo.eCardNumber = GetEvipCard(customerId);
            if (Common.CheckLatestBuild_August(appType, currectBuild))
            {
                string personalImgInfo = dtCustomer_Rows["personalImgInfo"] as string;
                if (!string.IsNullOrEmpty(personalImgInfo) && personalImgInfo.ToLower().IndexOf("http") == 0)
                {
                    userInfo.personalImgInfo = personalImgInfo;
                }
                else
                {
                    string picture = Common.ToString(dtCustomer_Rows["Picture"]);
                    DateTime registerDate = Common.ToDateTime(dtCustomer_Rows["registerDate"]);
                    userInfo.personalImgInfo = string.IsNullOrEmpty(picture)
                        ? ""
                        : WebConfig.CdnDomain + WebConfig.ImagePath + WebConfig.CustomerPicturePath +
                          registerDate.ToString("yyyyMM/") + picture;
                }
            }
            else
            {
                userInfo.personalImgInfo = Common.ToString(dtCustomer_Rows["personalImgInfo"]);
            }
            userInfo.customerSex = Common.ToInt32(dtCustomer_Rows["CustomerSex"]);
            //查询该用户对应的有效预点单数量
            userInfo.validPreorderCount = customerMan.SelectCustomerValidPreorderCount(customerId);
            CustomerLoginInfo customerLogin = new CustomerLoginInfo();
            customerLogin.customerId = customerId;
            customerLogin.uuid = uuid;
            customerLogin.loginTime = currentTime;
            //登录城市ID
            customerLogin.loginCityId = cityId;

            BatchInsertCustomerLoginInfo(customerLogin);
            using (TransactionScope scope = new TransactionScope())
            {
                //customerMan.InsertCustomerLogin(customerLogin);

                DataTable dtDevice = customerMan.SelectDevice(uuid);
                if (dtDevice.Rows.Count == 1)
                {
                    if ((!dtDevice.Rows[0]["pushToken"].Equals(pushToken) && !string.IsNullOrEmpty(pushToken)) || (!string.IsNullOrEmpty(currectBuild) && !dtDevice.Rows[0]["appBuild"].Equals(currectBuild)))
                    {
                        customerMan.UpdateDeviceToken(uuid, pushToken, currectBuild);
                    }
                }
                scope.Complete();
            }
            return userInfo;
        }

        private void BatchInsertCustomerLoginInfo(CustomerLoginInfo customerLogin)
        {
            object CustomerLoginInfoCache = CacheHelper.GetCache("customer_loginInfo");

            if (CustomerLoginInfoCache == null)
            {
                List<CustomerLoginInfo> customerLoginList = new List<CustomerLoginInfo>();
                customerLoginList.Add(customerLogin);

                CacheHelper.AddCache("customer_loginInfo", customerLoginList, 3600, delegateCustomerLogin);//3600
            }
            else
            {
                List<CustomerLoginInfo> customerLoginList = (List<CustomerLoginInfo>)CustomerLoginInfoCache;
                customerLoginList.Add(customerLogin);
                if (customerLoginList.Count > 99)
                {
                    CacheHelper.RemoveCache("customer_loginInfo");
                }
            }
        }

        private void delegateCustomerLogin(string key, object value, System.Web.Caching.CacheItemRemovedReason reason)
        {
            Thread thread = new Thread(BatchInsertCustomerLoginToDB);
            thread.Start(value);
        }

        private void BatchInsertCustomerLoginToDB(object value)
        {
            RedEnvelopeOperate redEnvelopeOperate = new RedEnvelopeOperate();
            System.Data.DataTable dtCustomerLoginList = Common.ListToDataTable((List<CustomerLoginInfo>)value);
            dtCustomerLoginList.TableName = "CustomerLoginInfo";
            redEnvelopeOperate.BatchInsert(dtCustomerLoginList);
        }
        /// <summary>
        /// 配置忘记密码邮件内容
        /// 该函数中有数据库操作并且没有安排事务，请调用处自行处理事务
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        private string ConfigForgetPasswordEmailBody(long customerId)
        {
            string emailBody = string.Empty;
            try
            {
                emailBody += "亲爱的悠先用户：你好！<br/><br/>";
                emailBody += "这封邮件是由&nbsp;悠先&nbsp;发送的。<br/><br/><br/>";
                //emailBody += "您收到这封邮件，是因为在我们的客户端上<br/>";
                //emailBody += "进行了忘记密码操作。如果您并没有进行上<br/>";
                //emailBody += "述操作，请忽略这封邮件。您不需要退订或<br/>";
                emailBody += "您收到这封邮件，是因为在我们的客户端上";
                emailBody += "进行了忘记密码操作。如果您并没有进行上";
                emailBody += "述操作，请忽略这封邮件。您不需要退订或";
                emailBody += "进行其他进一步的操作。<br/><br/><br/>";
                emailBody += "-----------------------<br/>";
                emailBody += "重置密码说明<br/>";
                emailBody += "-----------------------<br/><br/><br/>";
                emailBody += "你只需要点击下面的连接即可修改密码：<br/><br/><br/>";
                CustomerForgetPassword forgetPasswordInfo = new CustomerForgetPassword();
                forgetPasswordInfo.customerId = customerId;
                forgetPasswordInfo.sendEmailTime = System.DateTime.Now;
                string verifyCode = string.Empty;
                verifyCode = Common.ToString(customerId) + Common.ToString(System.DateTime.Now);
                forgetPasswordInfo.verifyCode = MD5Operate.getMd5Hash(verifyCode);

                CustomerManager customerMan = new CustomerManager();
                if (customerMan.InsertCustomerForgetPassword(forgetPasswordInfo) > 0)
                {
                    string server = ConfigurationManager.AppSettings["Server"].Trim();
                    string link = server + "/ResetPassword.aspx?" + "verifyCode=" + forgetPasswordInfo.verifyCode;

                    emailBody += "<a href='" + link + "'>" + link + "</a><br/>";
                }
                else
                {
                    return string.Empty;
                }
                //emailBody += "(如果上面不是链接形式，请将地址手工粘<br/>";
                //emailBody += "贴到浏览器地址栏再访问,该链接在10分钟内有效！)<br/>";
                emailBody += "(如果上面不是链接形式，请将地址手工粘";
                emailBody += "贴到浏览器地址栏再访问,该链接在10分钟内有效！)";
                emailBody += "感谢您的访问，祝您使用愉快！<br/><br/><br/><br/><br/>";
                emailBody += "此致<br/><br/>";
                emailBody += "悠先 管理团队<br/>";
                emailBody += "http://viewalloc.com<br/>";
            }
            catch (System.Exception)
            {
                return string.Empty;
            }
            return emailBody;
        }
        /// <summary>
        /// 发送验证码20140313
        /// </summary>
        /// <param name="cookie"></param>
        /// <param name="mobilePhoneNumber"></param>
        /// <returns></returns>
        public VAResult SendVerificationCodeBySmsForClient(string cookie, string mobilePhoneNumber, string verificationCode, bool voice = false, bool clearErrorCount = false)
        {
            //using (TransactionScope scope = new TransactionScope())
            //{
            CustomerManager customerMan = new CustomerManager();
            Random randomNumber = new Random();
            bool updateVerificationCodeTime = false;
            if (string.IsNullOrEmpty(verificationCode))
            {
                int verificationCodeDigit = int.Parse(new SystemConfigCacheLogic().GetSystemConfig("verificationCodeDigit", "5"));
                int minValue = (int)Math.Pow(10, (verificationCodeDigit - 1));
                int maxValue = (int)Math.Pow(10, verificationCodeDigit);
                verificationCode = Common.ToString(randomNumber.Next(minValue, maxValue));
                updateVerificationCodeTime = true;
            }
            bool updateVerificationCodeSucess = false;
            if (!string.IsNullOrEmpty(cookie))
            {
                updateVerificationCodeSucess = customerMan.UpdateCustomerVerificationCode(cookie, verificationCode, mobilePhoneNumber, updateVerificationCodeTime, voice, clearErrorCount);
            }
            else
            {
                updateVerificationCodeSucess = customerMan.UpdateCustomerVerificationCodeByMobilephoneNumber(mobilePhoneNumber, verificationCode, updateVerificationCodeTime, voice, clearErrorCount);
            }
            if (updateVerificationCodeSucess)
            {
                //scope.Complete();

                if (voice)
                {
                    string[] mobiles = new string[1];
                    mobiles[0] = mobilePhoneNumber;
                    if (Common.SendVoiceMessage(mobiles, verificationCode))
                    {
                        return VAResult.VA_OK;
                    }
                    else
                    {
                        return VAResult.VA_FAILED_SMS_NOT_SEND;
                    }
                }
                else
                {
                    string smsContent = WebConfig.SmsContent;
                    smsContent = smsContent.Replace("{0}", verificationCode);
                    if (Common.SendMessageBySms(mobilePhoneNumber, smsContent))
                    {
                        return VAResult.VA_OK;
                    }
                    else
                    {
                        return VAResult.VA_FAILED_SMS_NOT_SEND;
                    }
                }
            }
            else
            {
                return VAResult.VA_FAILED_DB_ERROR;
            }
            //}
        }
        /// <summary>
        /// 查询用户充值订单信息
        /// </summary>
        /// <param name="customerChargeOrderId"></param>
        /// <returns></returns>
        public DataTable QueryCustomerChargeOrder(long customerChargeOrderId)
        {
            return customerMan.SelectCustomerChargeOrder(customerChargeOrderId);
        }
        /// <summary>
        /// 20140313
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        private string GetEvipCard(long customerId)
        {//回给Android端的推送码，早期的eCardNumber
            return Common.ToString(customerId + 100000);
        }

        public string SelectCustomerCookieById(long customerId)
        {
            return customerMan.SelectCustomerCookieById(customerId);
        }
        public void UpdateCustomerCountAmount(long customerId, double preOrderTotalAmount, int preOrderTotalQuantity)
        {
            customerMan.UpdateCustomerCountAmount(customerId, preOrderTotalAmount, preOrderTotalQuantity);
        }
        /// <summary>
        /// 根据点单流水号查询客人部分信息（wangcheng 收银宝）
        /// </summary>
        /// <param name="preOrder19dianId">点单流水号</param>
        /// <returns></returns>
        public static DataTable QueryPartCustomerInfo(long preOrder19dianId)
        {
            CustomerManager customerMan = new CustomerManager();
            return customerMan.SelectPartCustomerInfo(preOrder19dianId);
        }
        /// <summary>
        /// 客户端查询点单评分所需信息20140313
        /// </summary>
        /// <param name="queryPreorderEvaluationInfoRequest"></param>
        /// <returns></returns>
        public VAClientQueryPreorderEvaluationInfoResponse QueryPreorderEvaluationInfo(VAClientQueryPreorderEvaluationInfoRequest queryPreorderEvaluationInfoRequest)
        {
            VAClientQueryPreorderEvaluationInfoResponse queryPreorderEvaluationInfoResponse = new VAClientQueryPreorderEvaluationInfoResponse();
            queryPreorderEvaluationInfoResponse.type = VAMessageType.CLIENT_QUERY_PREORDER_EVALUATIONINFO_RESPONSE;
            queryPreorderEvaluationInfoResponse.cookie = queryPreorderEvaluationInfoRequest.cookie;
            queryPreorderEvaluationInfoResponse.uuid = queryPreorderEvaluationInfoRequest.uuid;
            queryPreorderEvaluationInfoResponse.preorderId = queryPreorderEvaluationInfoRequest.preorderId;
            CheckCookieAndMsgtypeInfo checkResult = Common.CheckCookieAndMsgtype(queryPreorderEvaluationInfoRequest.cookie,
                queryPreorderEvaluationInfoRequest.uuid, (int)queryPreorderEvaluationInfoRequest.type, (int)VAMessageType.CLIENT_QUERY_PREORDER_EVALUATIONINFO_REQUEST);
            if (checkResult.result == VAResult.VA_OK)
            {
                PreOrder19dianManager preOrder19dianMan = new PreOrder19dianManager();
                //DataTable dtPreorder = preOrder19dianMan.SelectPreOrderAndCompany(queryPreorderEvaluationInfoRequest.preorderId);
                DataTable dtPreorder = preOrder19dianMan.SelectEvaluation(queryPreorderEvaluationInfoRequest.preorderId);
                if (dtPreorder.Rows.Count == 1)
                {
                    queryPreorderEvaluationInfoResponse.result = VAResult.VA_OK;
                    queryPreorderEvaluationInfoResponse.preorderTime = Common.ToSecondFrom1970(Common.ToDateTime(dtPreorder.Rows[0]["prePayTime"]));
                    queryPreorderEvaluationInfoResponse.preorderPaidSum = Common.ToDouble(dtPreorder.Rows[0]["prePaidSum"]);

                    string preorderShareURL = WebConfig.ServerDomain + "s.aspx?t=" + Common.ToString((int)VAWeiboShareType.PREORDER_SHARE) + "&v=" + queryPreorderEvaluationInfoRequest.preorderId.ToString();
                    string shopName = Common.ToString(dtPreorder.Rows[0]["shopName"]);
                    string complainURL = String.Format(WebConfig.ComplaintURL, queryPreorderEvaluationInfoRequest.preorderId.ToString());
                    queryPreorderEvaluationInfoResponse.preorderShareMsg = String.Format(WebConfig.Preorder_wx, WebConfig.MyWechat, shopName, preorderShareURL); ;
                    queryPreorderEvaluationInfoResponse.shopName = shopName;
                    ComplaintOperate oper = new ComplaintOperate();
                    if (oper.IsComplaint(queryPreorderEvaluationInfoRequest.preorderId) == true)
                    {
                        queryPreorderEvaluationInfoResponse.complainUrl = "";//表示当前点单已评价  add by wangc 20140325
                    }
                    else
                    {
                        queryPreorderEvaluationInfoResponse.complainUrl = complainURL;
                    }
                    ShopOperate shopOper = new ShopOperate();
                    ShopInfo shopInfo = shopOper.QueryShop(Common.ToInt32(dtPreorder.Rows[0]["shopId"]));
                    if (shopInfo != null)
                    {
                        if (!string.IsNullOrEmpty(shopInfo.shopLogo) && !string.IsNullOrEmpty(shopInfo.shopImagePath))
                        {
                            queryPreorderEvaluationInfoResponse.shopLogoUrl = WebConfig.CdnDomain + WebConfig.ImagePath
                                                       + shopInfo.shopImagePath
                                                       + shopInfo.shopLogo;
                        }
                        else
                        {
                            queryPreorderEvaluationInfoResponse.shopLogoUrl = "";
                        }
                    }
                    else
                    {
                        queryPreorderEvaluationInfoResponse.shopLogoUrl = "";
                    }
                    queryPreorderEvaluationInfoResponse.foodDiariesTitle = Common.ToString(ConfigurationManager.AppSettings["foodDiaries"]);
                    queryPreorderEvaluationInfoResponse.foodDiariesUrl = WebConfig.ServerDomain + "AppPages/FoodDiaries.aspx?id=" + queryPreorderEvaluationInfoRequest.preorderId;
                    queryPreorderEvaluationInfoResponse.foodDiariesAfterShareUrl = WebConfig.ServerDomain + "AppPages/FoodDiariesShow.aspx?id={0}";
                    queryPreorderEvaluationInfoResponse.haveSharedType = ClientExtendOperate.GetShareFoodDiary(queryPreorderEvaluationInfoRequest.preorderId);

                    var preorderEvaluation
                        = PreorderEvaluationOperate.GetFirstByQuery(new PreorderEvaluationQueryObject() { PreOrder19dianId = queryPreorderEvaluationInfoRequest.preorderId });

                    if (preorderEvaluation != null)
                    {
                        queryPreorderEvaluationInfoResponse.evaluationContent = preorderEvaluation.EvaluationContent;
                        queryPreorderEvaluationInfoResponse.isEvaluation = true;
                        //评价数据需要判断历史数据
                        if (Common.CheckLatestBuild_December(queryPreorderEvaluationInfoResponse.appType, queryPreorderEvaluationInfoRequest.clientBuild))
                        {
                            queryPreorderEvaluationInfoResponse.evaluationValue = preorderEvaluation.EvaluationLevel + 7;
                        }
                        else
                        {
                            int evaluationValue = Common.ToInt32(preorderEvaluation.EvaluationValue);
                            switch (evaluationValue)
                            {
                                case 8:
                                    evaluationValue = 5;
                                    break;
                                case 7:
                                    evaluationValue = 3;
                                    break;
                                case 6:
                                    evaluationValue = 2;
                                    break;
                                default:
                                    break;
                            }
                            queryPreorderEvaluationInfoResponse.evaluationValue = evaluationValue;
                        }
                    }
                    else
                    {
                        queryPreorderEvaluationInfoResponse.evaluationContent = string.Empty;
                        queryPreorderEvaluationInfoResponse.isEvaluation = false;
                        //老版本未评价数据返回结果
                        queryPreorderEvaluationInfoResponse.evaluationValue = -1;
                    }

                }
                else
                {
                    queryPreorderEvaluationInfoResponse.result = VAResult.VA_FAILED_PREORDER_NOT_FOUND;
                }
            }
            else
            {
                queryPreorderEvaluationInfoResponse.result = checkResult.result;
            }
            return queryPreorderEvaluationInfoResponse;
        }
        /// <summary>
        /// 客户端评价点单20140313
        /// </summary>
        /// <param name="clientEvaluatePreorderRequest"></param>
        /// <returns></returns>
        public VAClientEvaluatePreorderResponse ClientEvaluatePreorder(VAClientEvaluatePreorderRequest clientEvaluatePreorderRequest)
        {
            VAClientEvaluatePreorderResponse clientEvaluatePreorderResponse = new VAClientEvaluatePreorderResponse();
            clientEvaluatePreorderResponse.type = VAMessageType.CLIENT_EVALUATE_PREORDER_RESPONSE;
            clientEvaluatePreorderResponse.cookie = clientEvaluatePreorderRequest.cookie;
            clientEvaluatePreorderResponse.uuid = clientEvaluatePreorderRequest.uuid;
            clientEvaluatePreorderResponse.value = clientEvaluatePreorderRequest.value;
            CheckCookieAndMsgtypeInfo checkResult = Common.CheckCookieAndMsgtype(clientEvaluatePreorderRequest.cookie,
                clientEvaluatePreorderRequest.uuid, (int)clientEvaluatePreorderRequest.type, (int)VAMessageType.CLIENT_EVALUATE_PREORDER_REQUEST);
            if (checkResult.result == VAResult.VA_OK)
            {
                PreOrder19dianManager preOrder19dianManager = new PreOrder19dianManager();
                ShopManager shopManager = new ShopManager();
                PreOrder19dianInfo preOrder19dianInfo = preOrder19dianManager.GetPreOrderById(clientEvaluatePreorderRequest.preorderId);
                if (preOrder19dianInfo != null && clientEvaluatePreorderRequest.value != 0)
                {
                    PreorderEvaluationQueryObject preorderEvaluationQueryObject = new PreorderEvaluationQueryObject()
                    {
                        PreOrder19dianId = preOrder19dianInfo.preOrder19dianId
                    };
                    if (PreorderEvaluationOperate.GetCountByQuery(preorderEvaluationQueryObject) > 0)
                    {
                        clientEvaluatePreorderResponse.result = VAResult.VA_FAILED_OTHER;
                        return clientEvaluatePreorderResponse;
                    }
                    //-1为未评价 不处理
                    if (clientEvaluatePreorderRequest.value > -1)
                    {
                        ShopEvaluationDetailManager shopEvaluationDetailManager = new ShopEvaluationDetailManager();
                        ShopEvaluationDetailQueryObject shopEvaluationDetailQueryObject = new ShopEvaluationDetailQueryObject()
                        {
                            ShopId = preOrder19dianInfo.shopId
                        };
                        List<ShopEvaluationDetail> ShopEvaluationDetailList =
                            shopEvaluationDetailManager.GetShopEvaluationDetailByQuery(shopEvaluationDetailQueryObject);
                        PreorderEvaluation preorderEvaluation = new PreorderEvaluation();
                        preorderEvaluation.EvaluationValue = clientEvaluatePreorderRequest.value;
                        preorderEvaluation.ShopId = preOrder19dianInfo.shopId;
                        preorderEvaluation.PreOrder19dianId = preOrder19dianInfo.preOrder19dianId;
                        preorderEvaluation.CustomerId = preOrder19dianInfo.customerId;

                        if (!string.IsNullOrEmpty(clientEvaluatePreorderRequest.evaluationContent))
                        {
                            preorderEvaluation.EvaluationContent = clientEvaluatePreorderRequest.evaluationContent;
                        }
                        preorderEvaluation.EvaluationTime = DateTime.Now;
                        //需要兼容历史数据的评价 0-5分
                        switch (clientEvaluatePreorderRequest.value)
                        {
                            case 1:
                            case 2:
                                //差评
                                preorderEvaluation.EvaluationLevel = -1;
                                preorderEvaluation.EvaluationContent = "差评";
                                break;
                            case 3:
                                //中评
                                preorderEvaluation.EvaluationLevel = 0;
                                preorderEvaluation.EvaluationContent = "中评";
                                break;
                            case 0:
                            case 4:
                            case 5:
                                //好评
                                preorderEvaluation.EvaluationLevel = 1;
                                preorderEvaluation.EvaluationContent = "好评";
                                break;
                            case 6:
                                preorderEvaluation.EvaluationLevel = clientEvaluatePreorderRequest.value - 7;
                                if (string.IsNullOrEmpty(clientEvaluatePreorderRequest.evaluationContent))
                                {
                                    preorderEvaluation.EvaluationContent = "差评";
                                }
                                break;
                            case 7:
                                preorderEvaluation.EvaluationLevel = clientEvaluatePreorderRequest.value - 7;
                                if (string.IsNullOrEmpty(clientEvaluatePreorderRequest.evaluationContent))
                                {
                                    preorderEvaluation.EvaluationContent = "中评";
                                }
                                break;
                            case 8:
                                //新版本里的好中差
                                preorderEvaluation.EvaluationLevel = clientEvaluatePreorderRequest.value - 7;
                                if (string.IsNullOrEmpty(clientEvaluatePreorderRequest.evaluationContent))
                                {
                                    preorderEvaluation.EvaluationContent = "好评";
                                }
                                break;
                        }
                        ShopEvaluationDetail shopEvaluationDetail = null;
                        if (ShopEvaluationDetailList == null || !ShopEvaluationDetailList.Exists(p => p.EvaluationValue == preorderEvaluation.EvaluationLevel))
                        {
                            shopEvaluationDetailQueryObject.EvaluationValue = null;
                            shopEvaluationDetail = new ShopEvaluationDetail()
                            {
                                EvaluationCount = 1,
                                EvaluationValue = preorderEvaluation.EvaluationLevel,
                                ShopId = preOrder19dianInfo.shopId
                            };
                            shopEvaluationDetailManager.AddShopEvaluationDetail(shopEvaluationDetail);
                        }
                        else
                        {
                            shopEvaluationDetail = ShopEvaluationDetailList.First(p => p.EvaluationValue == preorderEvaluation.EvaluationLevel);
                            shopEvaluationDetail.EvaluationCount = shopEvaluationDetail.EvaluationCount + 1;
                            shopEvaluationDetailManager.UpdateShopEvaluationDetail(shopEvaluationDetail);
                        }
                        preOrder19dianInfo.isEvaluation = 1;
                        var order = OrderOperate.GetEntityById(preOrder19dianInfo.OrderId);
                        if (order == null)
                        {
                            clientEvaluatePreorderResponse.result = VAResult.VA_FAILED_OTHER;
                            return clientEvaluatePreorderResponse;
                        }

                        order.IsEvaluation = 1;
                        OrderOperate.Update(order);

                        if (PreorderEvaluationOperate.Add(preorderEvaluation) && preOrder19dianManager.UpdatePreOrder19dian(preOrder19dianInfo))
                        {
                            clientEvaluatePreorderResponse.result = VAResult.VA_OK;   
                        }
                        else
                        {
                            clientEvaluatePreorderResponse.result = VAResult.VA_FAILED_OTHER;
                        }

                        ShopInfo shopInfo = shopManager.SelectShopInfo(preOrder19dianInfo.shopId);
                        if (shopInfo != null)
                        {
                            double shopLevelRate =
                                ShopEvaluationDetailList.Sum(p => (double)p.EvaluationCount * p.EvaluationValue);
                            int shopLevel = 0;
                            if (shopLevelRate <= 0)
                            {
                                shopLevel = 0;
                            }
                            else
                            {
                                shopLevel = (int)((5d + Math.Sqrt(25d + 60 * shopLevelRate)) / 30d);
                            }
                            if (shopInfo.shopLevel != shopLevel)
                            {
                                shopInfo.shopLevel = shopLevel;
                                shopManager.UpdateShop(shopInfo);
                            }

                        }
                    }
                    else
                    {
                        clientEvaluatePreorderResponse.result = VAResult.VA_FAILED_OTHER;
                    }
                }
                else
                {
                    clientEvaluatePreorderResponse.result = VAResult.VA_FAILED_PREORDER_NOT_FOUND;
                }

            }
            else
            {
                clientEvaluatePreorderResponse.result = checkResult.result;
            }
            return clientEvaluatePreorderResponse;
        }
        /// <summary>
        /// 客户端查询最近的未评价的预点单20140313
        /// </summary>
        /// <param name="clientQueryPreorderNotEvaluatedRequest"></param>
        /// <returns></returns>
        public VAClientQueryPreorderNotEvaluatedResponse ClientQueryPreorderNotEvaluated(VAClientQueryPreorderNotEvaluatedRequest clientQueryPreorderNotEvaluatedRequest)
        {
            VAClientQueryPreorderNotEvaluatedResponse clientQueryPreorderNotEvaluatedResponse = new VAClientQueryPreorderNotEvaluatedResponse();
            clientQueryPreorderNotEvaluatedResponse.type = VAMessageType.CLIENT_QUERY_PREORDER_NOTEVALUATED_RESPONSE;
            clientQueryPreorderNotEvaluatedResponse.cookie = clientQueryPreorderNotEvaluatedRequest.cookie;
            clientQueryPreorderNotEvaluatedResponse.uuid = clientQueryPreorderNotEvaluatedRequest.uuid;
            CheckCookieAndMsgtypeInfo checkResult = Common.CheckCookieAndMsgtype(clientQueryPreorderNotEvaluatedRequest.cookie,
                clientQueryPreorderNotEvaluatedRequest.uuid, (int)clientQueryPreorderNotEvaluatedRequest.type, (int)VAMessageType.CLIENT_QUERY_PREORDER_NOTEVALUATED_REQUEST);
            if (checkResult.result == VAResult.VA_OK)
            {
                long customerId = Common.ToInt64(checkResult.dtCustomer.Rows[0]["customerId"]);
                PreOrder19dianManager preOrder19dianMan = new PreOrder19dianManager();
                long preorderId = preOrder19dianMan.SelectLatestPreorderNotEvaluated(customerId);
                if (preorderId > 0)
                {
                    clientQueryPreorderNotEvaluatedResponse.result = VAResult.VA_OK;
                    clientQueryPreorderNotEvaluatedResponse.preorderId = preorderId;
                }
                else
                {
                    clientQueryPreorderNotEvaluatedResponse.result = VAResult.VA_FAILED_PREORDER_NOT_FOUND;
                }
            }
            else
            {
                clientQueryPreorderNotEvaluatedResponse.result = checkResult.result;
            }
            return clientQueryPreorderNotEvaluatedResponse;
        }
        /// <summary>
        /// 根据当前用户Cookie查询当前用户编号（wangcheng 20140312）
        /// </summary>
        /// <param name="cookie"></param>
        /// <returns></returns>
        public long QueryCustomerByCookie(string cookie)
        {
            return customerMan.SelectCustomerByCookie(cookie);
        }
        /// <summary>
        /// 插入客户端用户反馈信息（wangcheng 20140312）
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="feedbackMsg"></param>
        /// <returns></returns>
        public long AddCustomerFeedback(long customerId, string feedbackMsg)
        {
            return customerMan.InsertCustomerFeedback(customerId, feedbackMsg);
        }
        /// <summary>
        /// 查询客户端用户反馈信息（wangcheng 20140312）
        /// </summary>
        /// <returns></returns>
        public DataTable QueryCustomerFeedbackInfo()
        {
            return customerMan.SelectCustomerFeedbackInfo();
        }
        /// <summary>
        /// (悠先点菜)检查手机号码是否认证
        /// </summary>
        /// <param name="mobilePhoneNumber"></param>
        /// <returns></returns>
        public static bool IsMobileVerified(string mobilePhoneNumber)
        {
            return new CustomerManager().SelectIsExistsCustomerByMobilephone(mobilePhoneNumber);
        }
        /// <summary>
        /// 201401查询当前用户所有收藏门店
        /// created by wangcheng
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public List<int> QueryCustomerFavoriteShop(long customerId)
        {
            return customerMan.SelectCustomerFavoriteShop(customerId);
        }
        /// <summary>
        /// 查询当前用户平台VIP等级 add by wangc 20140320
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public int QueryCustomerCurrentPlatformVipGrade(long customerId)
        {
            return customerMan.SelectCustomerCurrentPlatformVipGrade(customerId);
        }
        /// <summary>
        /// new根据手机号码查询用户基本信息20140326
        /// </summary>
        /// <param name="mobilephone"></param>
        /// <returns></returns>
        public DataTable QueryCustomerByMobilephone(string mobilephone)
        {
            return customerMan.SelectCustomerByMobilephone(mobilephone);
        }
        /// <summary>
        /// 根据手机号码查询用户昵称和手机号码
        /// </summary>
        /// <param name="phones"></param>
        /// <returns></returns>
        public List<string[]> QueryCustomerNameAndPhoneByMobilephone(string phones)
        {
            return customerMan.SelectCustomerNameAndPhoneByMobilephone(phones);
        }
        /// <summary>
        /// 给悠先点菜用户充值 add by wangc 20140326
        /// </summary>
        /// <param name="phoneStr"></param>
        /// <param name="amount"></param>
        /// <param name="cookie"></param>
        /// <returns></returns>
        public bool CustomerRecharge(string phoneStr, double amount, string cookie)
        {
            bool result = false;
            try
            {
                if (string.IsNullOrEmpty(phoneStr))
                {
                    DataTable dtCustomer = customerMan.SelectCustomerTableByCookie(cookie);
                    if (dtCustomer.Rows.Count == 1)
                    {
                        long customerId = Common.ToInt64(dtCustomer.Rows[0]["CustomerID"]);
                        Money19dianDetail money19dianDetail = new Money19dianDetail()
                        {
                            customerId = customerId,
                            changeReason = Common.GetEnumDescription(VA19dianMoneyChangeReason.MONEY19DIAN_SERVICE_RECHARGE),
                            changeTime = DateTime.Now,
                            changeValue = amount,
                            inoutcomeType = Convert.ToInt32(InoutcomeType.IN),
                            accountType = Convert.ToInt32(AccountType.SERVICERECHARGE),
                            accountTypeConnId = "", //待处理 需要获取
                            companyId = 0,
                            shopId = 0,
                            flowNumber = SybMoneyOperate.CreateCustomerFlowNumber(customerId),
                            remainMoney = Common.ToDouble(amount + Common.ToDouble(dtCustomer.Rows[0]["money19dianRemained"]))
                        };
                        using (TransactionScope scope = new TransactionScope())
                        {
                            if (Money19dianDetailManager.InsertMoney19dianDetail(money19dianDetail) > 0)
                            {
                                if (customerMan.CustomerRecharge(phoneStr, amount, cookie))
                                {
                                    scope.Complete();
                                    result = true;
                                }
                            }
                        }
                    }

                }
                else
                {
                    string[] listPhoneStr = phoneStr.Split(new string[1] { "," }, StringSplitOptions.RemoveEmptyEntries);
                    int moneyDetailInsertCount = 0;
                    using (TransactionScope scope = new TransactionScope())
                    {
                        for (int i = 0; i < listPhoneStr.Length; i++)
                        {
                            string mobilePhone = listPhoneStr[i].Replace("'", "");
                            DataTable dtCustomer = customerMan.SelectCustomerByMobilephone(mobilePhone);
                            if (dtCustomer.Rows.Count == 1)
                            {
                                long customerId = Common.ToInt64(dtCustomer.Rows[0]["CustomerID"]);
                                Money19dianDetail money19dianDetail = new Money19dianDetail()
                                {
                                    customerId = customerId,
                                    changeReason = Common.GetEnumDescription(VA19dianMoneyChangeReason.MONEY19DIAN_SERVICE_RECHARGE),
                                    changeTime = DateTime.Now,
                                    changeValue = amount,
                                    inoutcomeType = Convert.ToInt32(InoutcomeType.IN),
                                    accountType = Convert.ToInt32(AccountType.SERVICERECHARGE),
                                    accountTypeConnId = "", //待处理 需要获取
                                    companyId = 0,
                                    shopId = 0,
                                    flowNumber = SybMoneyOperate.CreateCustomerFlowNumber(customerId),
                                    remainMoney = Common.ToDouble(amount + Common.ToDouble(dtCustomer.Rows[0]["money19dianRemained"]))
                                };
                                if (Money19dianDetailManager.InsertMoney19dianDetail(money19dianDetail) > 0)
                                {
                                    moneyDetailInsertCount++;
                                }
                            }
                        }
                        if (moneyDetailInsertCount == listPhoneStr.Length)
                        {
                            if (customerMan.CustomerRecharge(phoneStr, amount, cookie))
                            {
                                scope.Complete();
                                result = true;
                            }
                        }
                    }
                }
            }
            catch (System.Exception)
            {

            }
            return result;
        }
        public bool CustomerRecharge(List<RechargeLogPartMolde> list)
        {
            int moneyDetailInsertCount = 0;
            DataTable dtCustomer = null;
            for (int i = 0; i < list.Count; i++)
            {
                dtCustomer = customerMan.SelectCustomerByMobilephone(list[i].customerPhone.Replace("\'", ""));
                if (dtCustomer.Rows.Count == 1)
                {
                    long customerId = Common.ToInt64(dtCustomer.Rows[0]["CustomerID"]);
                    Money19dianDetail money19dianDetail = new Money19dianDetail()
                    {
                        customerId = customerId,
                        changeReason = Common.GetEnumDescription(VA19dianMoneyChangeReason.MONEY19DIAN_SERVICE_RECHARGE),
                        changeTime = DateTime.Now,
                        changeValue = list[i].amount,
                        inoutcomeType = Convert.ToInt32(InoutcomeType.IN),
                        accountType = Convert.ToInt32(AccountType.SERVICERECHARGE),
                        accountTypeConnId = "",
                        companyId = 0,
                        shopId = 0,
                        flowNumber = SybMoneyOperate.CreateCustomerFlowNumber(customerId),
                        remainMoney = Common.ToDouble(list[i].amount + Common.ToDouble(dtCustomer.Rows[0]["money19dianRemained"]))
                    };
                    if (Money19dianDetailManager.InsertMoney19dianDetail(money19dianDetail) > 0 && customerMan.CustomerRecharge(list[i].customerPhone, list[i].amount, ""))
                    {
                        moneyDetailInsertCount++;
                    }
                }
            }
            return moneyDetailInsertCount == list.Count;
        }
        /// <summary>
        /// 根据电话号码，找到此用户登录过的设备，再找到这些设备被哪些用户登录过
        /// 2014-4-11
        /// </summary>
        /// <param name="phoneNumber">电话号码</param>
        /// <returns></returns>
        public DataTable QueryCustomerInfoByPhone(string phoneNumber)
        {
            return customerMan.SelectCustomerInfoByPhone(phoneNumber);
        }

        /// <summary>
        /// 客户端发送语音短信
        /// 2014-04-14 xiaoyu
        /// </summary>
        /// <param name="clientSendVoiceMessage"></param>
        /// <returns></returns>
        public VAClientSendVoiceMessageResponse ClientSendVoiceMessage(VAClientSendVoiceMessageRequest clientSendVoiceMessageRequest)
        {
            VAClientSendVoiceMessageResponse clientSendVoiceMessageResponse = new VAClientSendVoiceMessageResponse();
            clientSendVoiceMessageResponse.type = VAMessageType.CLIENT_SEND_VOICEMESSAGE_RESPONSE;
            clientSendVoiceMessageResponse.cookie = clientSendVoiceMessageRequest.cookie;
            clientSendVoiceMessageResponse.uuid = clientSendVoiceMessageRequest.uuid;
            CheckCookieAndMsgtypeInfo checkResult = Common.CheckCookieAndMsgtype(clientSendVoiceMessageRequest.cookie,
                clientSendVoiceMessageRequest.uuid, (int)clientSendVoiceMessageRequest.type, (int)VAMessageType.CLIENT_SEND_VOICEMESSAGE_REQUEST);
            if (checkResult.result == VAResult.VA_OK)
            {
                if (!string.IsNullOrEmpty(clientSendVoiceMessageRequest.mobilePhone))
                {
                    DataTable dtCustomer = customerMan.SelectCustomerByMobilephone(clientSendVoiceMessageRequest.mobilePhone);
                    if (dtCustomer.Rows.Count == 1)
                    {
                        DateTime verificationCodeTime = Common.ToDateTime(dtCustomer.Rows[0]["verificationCodeTime"]);
                        string verificationCode = Common.ToString(dtCustomer.Rows[0]["verificationCode"]);
                        bool currentVCSentByVoice = Common.ToBool(dtCustomer.Rows[0]["isVCSendByVoice"]);
                        int errorCount = Common.ToInt32(dtCustomer.Rows[0]["verificationCodeErrCnt"]);//用户验证码错误次数
                        bool clearErrorCount = errorCount > 0;

                        if (currentVCSentByVoice && (System.DateTime.Now - verificationCodeTime) < TimeSpan.FromSeconds(600))
                        {
                            clientSendVoiceMessageResponse.result = VAResult.VA_FAILED_SMS_VOICE_TOO_OFTEN;
                        }
                        else
                        {
                            if (((System.DateTime.Now - verificationCodeTime) < TimeSpan.FromSeconds(Common.smsValidTime)) && !string.IsNullOrEmpty(verificationCode))
                            {
                                clientSendVoiceMessageResponse.result = SendVerificationCodeBySmsForClient("", clientSendVoiceMessageRequest.mobilePhone, verificationCode, true, clearErrorCount);
                            }
                            else
                            {
                                clientSendVoiceMessageResponse.result = SendVerificationCodeBySmsForClient("", clientSendVoiceMessageRequest.mobilePhone, "", true, clearErrorCount);
                            }
                        }
                    }
                    else
                    {
                        DateTime verificationCodeTime = Common.ToDateTime(checkResult.dtCustomer.Rows[0]["verificationCodeTime"]);
                        string verificationCode = Common.ToString(checkResult.dtCustomer.Rows[0]["verificationCode"]);
                        bool currentVCSentByVoice = Common.ToBool(checkResult.dtCustomer.Rows[0]["isVCSendByVoice"]);
                        int errorCount = Common.ToInt32(checkResult.dtCustomer.Rows[0]["verificationCodeErrCnt"]);//用户验证码错误次数
                        bool clearErrorCount = errorCount > 0;

                        if (currentVCSentByVoice && (System.DateTime.Now - verificationCodeTime) < TimeSpan.FromSeconds(600))
                        {
                            clientSendVoiceMessageResponse.result = VAResult.VA_FAILED_SMS_VOICE_TOO_OFTEN;
                        }
                        else
                        {
                            if (((System.DateTime.Now - verificationCodeTime) < TimeSpan.FromSeconds(Common.smsValidTime)) && !string.IsNullOrEmpty(verificationCode))
                            {
                                clientSendVoiceMessageResponse.result = SendVerificationCodeBySmsForClient(clientSendVoiceMessageRequest.cookie, clientSendVoiceMessageRequest.mobilePhone, verificationCode, true, clearErrorCount);
                            }
                            else
                            {
                                clientSendVoiceMessageResponse.result = SendVerificationCodeBySmsForClient(clientSendVoiceMessageRequest.cookie, clientSendVoiceMessageRequest.mobilePhone, "", true, clearErrorCount);
                            }
                        }
                    }
                }
                else
                {
                    clientSendVoiceMessageResponse.result = VAResult.VA_FAILED_OTHER;
                }
            }
            else
            {
                clientSendVoiceMessageResponse.result = checkResult.result;
            }
            clientSendVoiceMessageResponse.verificationCodeDigit = int.Parse(new SystemConfigCacheLogic().GetSystemConfig("verificationCodeDigit", "5"));
            return clientSendVoiceMessageResponse;
        }
        /// <summary>
        /// 客户端发送错误日志
        /// 2014-04-14 xiaoyu
        /// </summary>
        /// <param name="clientSendErrorMessageRequest"></param>
        /// <returns></returns>
        public VAClientSendErrorMessageResponse ClientSendErrorMessage(VAClientSendErrorMessageRequest clientSendErrorMessageRequest)
        {
            VAClientSendErrorMessageResponse clientSendErrorMessageResponse = new VAClientSendErrorMessageResponse();
            clientSendErrorMessageResponse.type = VAMessageType.CLIENT_SEND_ERRORMESSAGE_RESPONSE;
            clientSendErrorMessageResponse.cookie = clientSendErrorMessageRequest.cookie;
            clientSendErrorMessageResponse.uuid = clientSendErrorMessageRequest.uuid;
            CheckCookieAndMsgtypeInfo checkResult = Common.CheckCookieAndMsgtype(clientSendErrorMessageRequest.cookie,
                clientSendErrorMessageRequest.uuid, (int)clientSendErrorMessageRequest.type, (int)VAMessageType.CLIENT_SEND_ERRORMESSAGE_REQUEST);
            if (checkResult.result == VAResult.VA_OK)
            {
                ClientErrorManager clientErrorMan = new ClientErrorManager();
                ClientErrorInfo clientErrorInfo = new ClientErrorInfo();
                clientErrorInfo.appType = clientSendErrorMessageRequest.appType;
                clientErrorInfo.errorMessage = clientSendErrorMessageRequest.errorInfo;
                clientErrorInfo.time = DateTime.Now;
                clientErrorInfo.clientBuild = clientSendErrorMessageRequest.clientBuild;
                clientErrorInfo.clientType = VAClientType.UXIAN;
                using (TransactionScope scope = new TransactionScope())
                {
                    if (clientErrorMan.InsertClientError(clientErrorInfo) > 0)
                    {
                        scope.Complete();
                        clientSendErrorMessageResponse.result = VAResult.VA_OK;
                    }
                    else
                    {
                        clientSendErrorMessageResponse.result = VAResult.VA_FAILED_DB_ERROR;
                    }
                }
            }
            else
            {
                clientSendErrorMessageResponse.result = checkResult.result;
            }
            if (clientSendErrorMessageResponse.result == VAResult.VA_OK)
            {
                VAEmailInfo emailInfo = new VAEmailInfo();
                emailInfo.subject = "悠先点菜客户端出错日志";
                emailInfo.messageBody = clientSendErrorMessageRequest.errorInfo + "----------------------------------悠先点菜，当前版本号：" + clientSendErrorMessageRequest.clientBuild;
                emailInfo.emailAddressTo = ConfigurationManager.AppSettings["ClientErrorEmailAddressTo"].Trim();
                Thread emailThread = new Thread(Common.SendNEmailFrom19dianService);
                emailThread.Start((object)emailInfo);
            }
            return clientSendErrorMessageResponse;
        }
        /// <summary>
        /// 客户端查询版本信息
        /// 2014-04-14 xiaoyu
        /// </summary>
        /// <param name="clientQueryBuildRequest"></param>
        /// <returns></returns>
        public VAClientQueryBuildResponse ClientQueryBuild(VAClientQueryBuildRequest clientQueryBuildRequest)
        {
            VAClientQueryBuildResponse clientQueryBuildResponse = new VAClientQueryBuildResponse();
            clientQueryBuildResponse.type = VAMessageType.CLIENT_QUERY_BUILD_RESPONSE;
            clientQueryBuildResponse.cookie = clientQueryBuildRequest.cookie;
            clientQueryBuildResponse.uuid = clientQueryBuildRequest.uuid;
            CheckCookieAndMsgtypeInfo checkResult = Common.CheckCookieAndMsgtype(clientQueryBuildRequest.cookie,
                clientQueryBuildRequest.uuid, (int)clientQueryBuildRequest.type, (int)VAMessageType.CLIENT_QUERY_BUILD_REQUEST);
            if (checkResult.result == VAResult.VA_OK)
            {
                SystemConfigCacheLogic systemConfigCacheLogic = new SystemConfigCacheLogic();
                DataTable dtLatestBuild = systemConfigCacheLogic.GetAppLatestBuild(clientQueryBuildRequest.appType);
                if (dtLatestBuild.Rows.Count == 1)
                {
                    clientQueryBuildResponse.result = VAResult.VA_OK;
                    clientQueryBuildResponse.latestBuild = Common.ToString(dtLatestBuild.Rows[0]["latestBuild"]);
                    clientQueryBuildResponse.latestUpdateDescription = Common.ToString(dtLatestBuild.Rows[0]["latestUpdateDescription"]);
                    clientQueryBuildResponse.latestUpdateUrl = Common.ToString(dtLatestBuild.Rows[0]["latestUpdateUrl"]);
                    string oldBuildSupport = Common.ToString(dtLatestBuild.Rows[0]["oldBuildSupport"]);
                    string clientBuild = "";
                    if (!string.IsNullOrEmpty(clientQueryBuildRequest.clientBuild))
                    {
                        clientBuild = clientQueryBuildRequest.clientBuild;
                    }
                    if (string.Compare(clientBuild, oldBuildSupport) == -1)
                    {
                        clientQueryBuildResponse.forceUpdate = true;
                    }
                    else
                    {
                        clientQueryBuildResponse.forceUpdate = false;
                    }
                }
                else
                {
                    clientQueryBuildResponse.result = VAResult.VA_FAILED_OTHER;
                }
            }
            else
            {
                clientQueryBuildResponse.result = checkResult.result;
            }
            return clientQueryBuildResponse;
        }

        /// <summary>
        /// 美食日记
        /// </summary>
        /// <param name="clientFoodDiarySharedRequest"></param>
        /// <returns></returns>
        public VAClientFoodDiarySharedResponse FoodDiaryShared(VAClientFoodDiarySharedRequest clientFoodDiarySharedRequest)
        {
            VAClientFoodDiarySharedResponse clientFoodDiarySharedResponse = new VAClientFoodDiarySharedResponse();
            clientFoodDiarySharedResponse.type = VAMessageType.CLIENT_FOODDIARY_SHARED_RESPONSE;
            clientFoodDiarySharedResponse.cookie = clientFoodDiarySharedRequest.cookie;
            clientFoodDiarySharedResponse.uuid = clientFoodDiarySharedRequest.uuid;
            clientFoodDiarySharedResponse.cityId = clientFoodDiarySharedRequest.cityId;
            CheckCookieAndMsgtypeInfo checkResult = Common.CheckCookieAndMsgtype(clientFoodDiarySharedRequest.cookie,
                clientFoodDiarySharedRequest.uuid, (int)clientFoodDiarySharedRequest.type, (int)VAMessageType.CLIENT_FOODDIARY_SHARED_REQUEST);

            if (checkResult.result == VAResult.VA_OK)
            {
                var foodDiaryService = ServiceFactory.Resolve<IFoodDiaryService>();

                foodDiaryService.Update(clientFoodDiarySharedRequest.foodDiaryId, clientFoodDiarySharedRequest.content, clientFoodDiarySharedRequest.isBig, clientFoodDiarySharedRequest.isHideDishName,
                    clientFoodDiarySharedRequest.shared,
                    clientFoodDiarySharedRequest.dishes.Select(p => new FoodDiaryDish
                    {
                        DishId = p.DishId,
                        DishName = p.DishName,
                        FoodDiaryId = p.FoodDiaryId,
                        Id = p.Id,
                        ImagePath = p.ImagePath,
                        Source = p.Source,
                        Sort = p.Sort,
                        Status = p.Status
                    }));

                clientFoodDiarySharedResponse.result = VAResult.VA_OK;
            }
            else
            {
                clientFoodDiarySharedResponse.result = checkResult.result;
            }
            return clientFoodDiarySharedResponse;
        }

        /// <summary>
        /// 根据uuid更新pushToken和时间
        /// 2014-5-7
        /// </summary>
        /// <param name="clientUpdatePushTokenRequest"></param>
        /// <returns></returns>
        public VAClientUpdatePushTokenResponse UpdateDeviceToken(VAClientUpdatePushTokenRequest clientUpdatePushTokenRequest)
        {
            VAClientUpdatePushTokenResponse clientUpdatePushTokenResponse = new VAClientUpdatePushTokenResponse();
            clientUpdatePushTokenResponse.type = VAMessageType.CLIENT_PUSHTOKEN_UPDATE_RESPONSE;
            clientUpdatePushTokenResponse.cookie = clientUpdatePushTokenRequest.cookie;
            clientUpdatePushTokenResponse.uuid = clientUpdatePushTokenRequest.uuid;
            CheckCookieAndMsgtypeInfo checkResult = Common.CheckCookieAndMsgtype(clientUpdatePushTokenRequest.cookie,
                clientUpdatePushTokenRequest.uuid, (int)clientUpdatePushTokenRequest.type, (int)VAMessageType.CLIENT_PUSHTOKEN_UPDATE_REQUEST);
            if (checkResult.result == VAResult.VA_OK)
            {
                DataTable dtDevice = customerMan.SelectDevice(clientUpdatePushTokenRequest.uuid);
                if (dtDevice != null && dtDevice.Rows.Count == 1)
                {
                    if (dtDevice.Rows[0]["pushToken"].Equals(clientUpdatePushTokenRequest.pushToken))
                    {
                        clientUpdatePushTokenResponse.result = VAResult.VA_OK;
                        return clientUpdatePushTokenResponse;
                    }
                    if ((!dtDevice.Rows[0]["pushToken"].Equals(clientUpdatePushTokenRequest.pushToken) && !string.IsNullOrEmpty(clientUpdatePushTokenRequest.pushToken)) || (!dtDevice.Rows[0]["appBuild"].Equals(clientUpdatePushTokenRequest.clientBuild) && !string.IsNullOrEmpty(clientUpdatePushTokenRequest.clientBuild)))
                    {
                        using (TransactionScope scope = new TransactionScope())
                        {
                            if (customerMan.UpdateDeviceToken(clientUpdatePushTokenRequest.uuid, clientUpdatePushTokenRequest.pushToken, clientUpdatePushTokenRequest.clientBuild))
                            {
                                scope.Complete();
                                clientUpdatePushTokenResponse.result = VAResult.VA_OK;
                            }
                            else
                            {
                                clientUpdatePushTokenResponse.result = VAResult.VA_FAILED_DB_ERROR;
                            }
                        }
                    }
                    else
                    {
                        clientUpdatePushTokenResponse.result = VAResult.VA_FAILED_OTHER;
                    }
                }
                else
                {
                    clientUpdatePushTokenResponse.result = VAResult.VA_FAILED_OTHER;
                }
            }
            else
            {
                clientUpdatePushTokenResponse.result = checkResult.result;
            }
            return clientUpdatePushTokenResponse;
        }

        /// <summary>
        /// 按手机号返回model
        /// </summary>
        /// <param name="mobile"></param>
        /// <returns></returns>
        public CustomerInfo GetMobileOfModel(string mobile)
        {
            return new CustomerManager().GetMobileOfModel(mobile);
        }

        /// <summary>
        /// Android更新设备的UUID
        /// </summary>
        /// <param name="vaClientUpdateUUIDRequest"></param>
        /// <returns></returns>
        public VAClientUpdateUUIDResponse UpdateDevideUUID(VAClientUpdateUUIDRequest vaClientUpdateUUIDRequest)
        {
            VAClientUpdateUUIDResponse vaClientUpdateUUIDResponse = new VAClientUpdateUUIDResponse();
            vaClientUpdateUUIDResponse.type = VAMessageType.CLIENT_UPDATE_UUID_RESPONSE;
            long flagInsertCustomerConnDevice = 0;
            long customerId = customerMan.SelectCustomerByCookie(vaClientUpdateUUIDRequest.cookie);
            DataTable dtDeviceNew = customerMan.SelectDevice(vaClientUpdateUUIDRequest.newUUID);
            if (dtDeviceNew != null && dtDeviceNew.Rows.Count == 1)
            {
                long deviceId = Common.ToInt64(dtDeviceNew.Rows[0]["deviceId"]);
                DataTable dtCustomerConnDevice = customerMan.SelectCustomerConnDevice(customerId, deviceId);
                bool flagUpdateCustomerConnDeviceTime = false;
                if (dtCustomerConnDevice != null && dtCustomerConnDevice.Rows.Count == 0)
                {
                    //用户与设备关系表中没有记录，说明用户在该设备上未登陆过,则插入一条记录
                    CustomerConnDevice customerConnDevice = new CustomerConnDevice();
                    customerConnDevice.customerId = customerId;
                    customerConnDevice.deviceId = deviceId;
                    customerConnDevice.updateTime = System.DateTime.Now;
                    flagInsertCustomerConnDevice = customerMan.InsertCustomerConnDevice(customerConnDevice);
                }
                else
                {
                    //更新登录设备的时间
                    flagUpdateCustomerConnDeviceTime = customerMan.UpdateCustomerConnDeviceTime(customerId, deviceId);
                }
                if (flagInsertCustomerConnDevice > 0 || flagUpdateCustomerConnDeviceTime)
                {
                    vaClientUpdateUUIDResponse.result = VAResult.VA_OK;
                }
                else
                {
                    vaClientUpdateUUIDResponse.result = VAResult.VA_FAILED_DB_ERROR;
                }
            }
            else
            {
                DataTable dtDeviceOld = customerMan.SelectDevice(vaClientUpdateUUIDRequest.oldUUID);//新的不存在，旧的存在，则用新的把旧的替换掉
                if (dtDeviceOld != null && dtDeviceOld.Rows.Count == 1)
                {
                    if (!dtDeviceOld.Rows[0]["uuid"].Equals(vaClientUpdateUUIDRequest.newUUID))
                    {
                        bool updateUUid = customerMan.UpdateDeviceUUID(Common.ToInt64(dtDeviceOld.Rows[0]["deviceId"]), vaClientUpdateUUIDRequest.newUUID);
                        //CustomerConnDevice customerConnDevice = new CustomerConnDevice();
                        //customerConnDevice.customerId = customerId;
                        //customerConnDevice.deviceId = Common.ToInt64(dtDeviceOld.Rows[0]["deviceId"]);
                        //customerConnDevice.updateTime = System.DateTime.Now;
                        //flagInsertCustomerConnDevice = customerMan.InsertCustomerConnDevice(customerConnDevice);

                        if (updateUUid)
                        {
                            vaClientUpdateUUIDResponse.result = VAResult.VA_OK;
                        }
                        else
                        {
                            vaClientUpdateUUIDResponse.result = VAResult.VA_FAILED_UPDATE_UUID; //更新UUID失败
                        }
                    }
                }
                else
                {
                    vaClientUpdateUUIDResponse.result = VAResult.VA_FAILED_UUID_NOT_FOUND;//根据此UUID未找到相应设备
                }
            }
            return vaClientUpdateUUIDResponse;
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

          /// <summary>
        /// 查询用户在指定城市关注的店
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="cityId"></param>
        /// <returns></returns>
        public List<int> SelectCustomerFavoriteShop(long customerId, int cityId)
        {
            return customerMan.SelectCustomerFavoriteShop(customerId, cityId);
        }

        public DataTable SelectCustomer(string customerIds)
        {
            return customerMan.SelectCustomer(customerIds);
        }
    }
}
