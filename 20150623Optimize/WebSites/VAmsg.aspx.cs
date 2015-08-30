﻿using System;
using System.IO;
using VAGastronomistMobileApp.WebPageDll;
using VAGastronomistMobileApp.Model;
using System.Threading;
using LogDll;
using VAGastronomistMobileApp.WebPageDll;
using VAGastronomistMobileApp.WebPageDll.Adjunction;

//  Copyright 2011 View Alloc inc. All rights reserved.
//  Created by Jason Xiao on 2012-04-10.
//

public partial class VAmsg : System.Web.UI.Page
{
    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        
        string responseJson = null;
        int type = Common.ToInt32(Request.Form["type"]);
        string msg = Request.Form["msg"];

        #region 正式环境注释
        //type = Common.ToInt32(Request.QueryString["type"]);
        //msg = Request.QueryString["msg"];
        #endregion

        // 记录请求和返回的guid值
        Guid requestGuid = Guid.NewGuid();
        DateTime requestTime = DateTime.Now;
        if (String.Compare(Common.debugFlag, "true", StringComparison.OrdinalIgnoreCase) == 0)
        {
            // 添加接口调试记录日志,调用log4net add by zhujinlei 2015/05/30
            Log4netManager.WriteLog("type:" + type + "- msg:" + msg);
            Log4netManager.WriteLogDB("request", type, msg, requestTime, requestGuid);
            //using (StreamWriter file = new StreamWriter(@filePath, true))
            //{
            //    file.WriteLine(requestTime.ToString("yyyy/MM/dd HH:mm:ss:fff") + ":type:" + type + "- msg:" + msg);
            //}
        }

        switch (type)
        {
            //客户端登录注册相关
            #region 设备注册
            case (int)VAMessageType.CLIENT_REGISTER_REQUEST:
                VAClientRegisterRequest clientRegisterRequest = JsonOperate.JsonDeserialize<VAClientRegisterRequest>(msg);
                VAClientRegisterResponse clientRegisterResponse = new VAClientRegisterResponse();
                if (clientRegisterRequest == null)
                {
                    clientRegisterResponse.result = VAResult.VA_FAILED_MSG_ERROR;
                }
                else
                {
                    CustomerOperate customerOperate = new CustomerOperate();
                    clientRegisterResponse = customerOperate.ClientRegister(clientRegisterRequest);
                }
                responseJson = JsonOperate.JsonSerializer<VAClientRegisterResponse>(clientRegisterResponse);
                break;
            #endregion

            #region 证书登录
            case (int)VAMessageType.CLIENT_COOKIE_LOGIN_REQUEST:
                VAClientCookieLoginRequest clientCookieLoginRequest = JsonOperate.JsonDeserialize<VAClientCookieLoginRequest>(msg);
                VAClientCookieLoginResponse clientCookieLoginResponse = new VAClientCookieLoginResponse();
                if (clientCookieLoginRequest == null)
                {
                    clientCookieLoginResponse.result = VAResult.VA_FAILED_MSG_ERROR;
                }
                else
                {
                    CustomerOperate customerOperate = new CustomerOperate();
                    clientCookieLoginResponse = customerOperate.ClientCookieLogin(clientCookieLoginRequest);
                }
                responseJson = JsonOperate.JsonSerializer<VAClientCookieLoginResponse>(clientCookieLoginResponse);
                break;
            #endregion

            #region 手机认证New
            case (int)VAMessageType.CLIENT_MOBILE_VERIFYNEW_REQUEST:
                VAClientMobileVerifyNewRequest clientMobileVerifyNewRequest = JsonOperate.JsonDeserialize<VAClientMobileVerifyNewRequest>(msg);
                VAClientMobileVerifyNewResponse clientMobileVerifyNewResponse = new VAClientMobileVerifyNewResponse() { type = VAMessageType.CLIENT_MOBILE_VERIFYNEW_RESPONSE };
                if (clientMobileVerifyNewRequest == null)
                {
                    clientMobileVerifyNewResponse.result = VAResult.VA_FAILED_MSG_ERROR;
                }
                else
                {
                    CustomerOperate customerOperate = new CustomerOperate();
                    clientMobileVerifyNewResponse = customerOperate.ClientMobileVerifyNew(clientMobileVerifyNewRequest);
                }
                responseJson = JsonOperate.JsonSerializer<VAClientMobileVerifyNewResponse>(clientMobileVerifyNewResponse);
                break;
            #endregion

            #region 用户绑定第三方登录信息
            case (int)VAMessageType.CLIENT_BIND_OPEN_ID_REQUEST:
                VAClientBindOpenIdRequest clientBindOpenIdRequest = JsonOperate.JsonDeserialize<VAClientBindOpenIdRequest>(msg);
                VAClientBindOpenIdResponse clientBindOpenIdResponse = new VAClientBindOpenIdResponse();
                if (clientBindOpenIdRequest == null)
                {
                    clientBindOpenIdResponse.result = VAResult.VA_FAILED_MSG_ERROR;
                }
                else
                {
                    CustomerOperate customerOpe = new CustomerOperate();
                    clientBindOpenIdResponse = customerOpe.ClientBindOpenId(clientBindOpenIdRequest);
                }
                responseJson = JsonOperate.JsonSerializer<VAClientBindOpenIdResponse>(clientBindOpenIdResponse);
                break;
            #endregion

            #region 获取城市列表
            case (int)VAMessageType.CITY_LIST_REQUEST:
                VACityListRequest cityListRequest = JsonOperate.JsonDeserialize<VACityListRequest>(msg);
                VACityListResponse cityListResponse = new VACityListResponse();
                if (cityListRequest == null)
                {
                    cityListResponse.result = VAResult.VA_FAILED_MSG_ERROR;
                }
                else
                {
                    CityOperate cityOperate = new CityOperate();
                    cityListResponse = cityOperate.ClientCityList(cityListRequest);
                }
                responseJson = JsonOperate.JsonSerializer<VACityListResponse>(cityListResponse);
                break;
            #endregion

            #region 更新用户信息New(修改个人信息)
            case (int)VAMessageType.USER_INFO_MODIFYNEW_REQUEST:
                VAClientUpdateUserInfoNewRequest clientUpdateUserInfoNewRequest = JsonOperate.JsonDeserialize<VAClientUpdateUserInfoNewRequest>(msg);
                VAClientUpdateUserInfoNewResponse clientUpdateUserInfoNewResponse = new VAClientUpdateUserInfoNewResponse();
                if (clientUpdateUserInfoNewRequest == null)
                {
                    clientUpdateUserInfoNewResponse.result = VAResult.VA_FAILED_MSG_ERROR;
                }
                else
                {
                    CustomerOperate customerOperate = new CustomerOperate();
                    clientUpdateUserInfoNewResponse = customerOperate.ClientUpdateUserInfoNew(clientUpdateUserInfoNewRequest);
                }
                responseJson = JsonOperate.JsonSerializer<VAClientUpdateUserInfoNewResponse>(clientUpdateUserInfoNewResponse);

                new UserIntegralSend().Send(clientUpdateUserInfoNewRequest, responseJson);
                break;
            #endregion

            #region 客户端注册用户邀请用户
            case (int)VAMessageType.CLIENT_INVITE_CUSTOMER_REQUEST:
                VAClientInviteCustomerRequest clientInviteCustomerRequest = JsonOperate.JsonDeserialize<VAClientInviteCustomerRequest>(msg);
                VAClientInviteCustomerResponse clientInviteCustomerResponse = new VAClientInviteCustomerResponse();
                if (clientInviteCustomerRequest == null)
                {
                    clientInviteCustomerResponse.result = VAResult.VA_FAILED_MSG_ERROR;
                }
                else
                {
                    CustomerOperate customerOperate = new CustomerOperate();
                    clientInviteCustomerResponse = customerOperate.ClientInviteCustomer(clientInviteCustomerRequest);
                }
                responseJson = JsonOperate.JsonSerializer<VAClientInviteCustomerResponse>(clientInviteCustomerResponse);
                break;
            #endregion

            #region 客户端更新用户PushToken
            case (int)VAMessageType.CLIENT_PUSHTOKEN_UPDATE_REQUEST:
                VAClientUpdatePushTokenRequest clientUpdatePushTokenRequest = JsonOperate.JsonDeserialize<VAClientUpdatePushTokenRequest>(msg);
                VAClientUpdatePushTokenResponse clientUpdatePushTokenResponse = new VAClientUpdatePushTokenResponse();
                if (clientUpdatePushTokenRequest == null)
                {
                    clientUpdatePushTokenResponse.result = VAResult.VA_FAILED_MSG_ERROR;
                }
                else
                {
                    CustomerOperate customerOperate = new CustomerOperate();
                    clientUpdatePushTokenResponse = customerOperate.UpdateDeviceToken(clientUpdatePushTokenRequest);
                }
                responseJson = JsonOperate.JsonSerializer<VAClientUpdatePushTokenResponse>(clientUpdatePushTokenResponse);
                break;
            #endregion

            #region 悠先点菜更新设备的UUID
            case (int)VAMessageType.CLIENT_UPDATE_UUID_RESQUEST:
                VAClientUpdateUUIDRequest vAClientUpdateUUIDRequest = JsonOperate.JsonDeserialize<VAClientUpdateUUIDRequest>(msg);
                VAClientUpdateUUIDResponse vAClientUpdateUUIDResponse = new VAClientUpdateUUIDResponse();
                if (vAClientUpdateUUIDRequest == null)
                {
                    vAClientUpdateUUIDResponse.result = VAResult.VA_FAILED_MSG_ERROR;
                }
                else
                {
                    CustomerOperate customerOperate = new CustomerOperate();
                    vAClientUpdateUUIDResponse = customerOperate.UpdateDevideUUID(vAClientUpdateUUIDRequest);
                }
                responseJson = JsonOperate.JsonSerializer<VAClientUpdateUUIDResponse>(vAClientUpdateUUIDResponse);
                break;
            #endregion

            //客户端点单相关
            #region 客户端查询点单详情
            case (int)VAMessageType.CLIENT_PREORDER_QUERY_REQUEST:
                VAClientPreOrderQueryRequest clientPreOrderQueryRequest = JsonOperate.JsonDeserialize<VAClientPreOrderQueryRequest>(msg);
                VAClientPreOrderQueryResponse clientPreOrderQueryResponse = new VAClientPreOrderQueryResponse();
                if (clientPreOrderQueryRequest == null)
                {
                    clientPreOrderQueryResponse.result = VAResult.VA_FAILED_MSG_ERROR;
                }
                else
                {
                    PreOrder19dianOperate preOrder19DianOpe = new PreOrder19dianOperate();
                    clientPreOrderQueryResponse = preOrder19DianOpe.ClientQueryPreOrder(clientPreOrderQueryRequest);
                }
                responseJson = JsonOperate.JsonSerializer<VAClientPreOrderQueryResponse>(clientPreOrderQueryResponse);
                break;
            #endregion

            #region 客户端微博分享预点单
            //case (int)VAMessageType.CLIENT_PREORDER_SHARE_REQUEST:
            //    VAClientSharePreOrderRequest clientSharePreOrderRequest = JsonOperate.JsonDeserialize<VAClientSharePreOrderRequest>(msg);
            //    VAClientSharePreOrderResponse clientSharePreOrderResponse = new VAClientSharePreOrderResponse();
            //    if (clientSharePreOrderRequest == null)
            //    {
            //        clientSharePreOrderResponse.result = VAResult.VA_FAILED_MSG_ERROR;
            //    }
            //    else
            //    {
            //        PreOrder19dianOperate preOrder19dianOpe = new PreOrder19dianOperate();
            //        clientSharePreOrderResponse = preOrder19dianOpe.ClientSharePreOrder(clientSharePreOrderRequest);
            //    }
            //    responseJson = JsonOperate.JsonSerializer<VAClientSharePreOrderResponse>(clientSharePreOrderResponse);
            //    break;
            #endregion

            #region 客户端查询点单列表
            case (int)VAMessageType.CLIENT_PREORDER_LIST_REQUEST:
                VAClientPreorderListRequest clientPreorderListRequest = JsonOperate.JsonDeserialize<VAClientPreorderListRequest>(msg);
                VAClientPreorderListResponse clientPreorderListResponse = new VAClientPreorderListResponse();
                if (clientPreorderListRequest == null)
                {
                    clientPreorderListResponse.result = VAResult.VA_FAILED_MSG_ERROR;
                }
                else
                {
                    PreOrder19dianOperate preOrder19DianOpe = new PreOrder19dianOperate();
                    clientPreorderListResponse = preOrder19DianOpe.ClientQueryPreOrderList(clientPreorderListRequest);
                }
                responseJson = JsonOperate.JsonSerializer<VAClientPreorderListResponse>(clientPreorderListResponse);
                break;
            #endregion

            #region 客户端删除状态预点单
            case (int)VAMessageType.CLIENT_PREORDER_DELETE_REQUEST:
                VAClientPreorderDeleteRequest clientPreorderDeleteRequest = JsonOperate.JsonDeserialize<VAClientPreorderDeleteRequest>(msg);
                VAClientPreorderDeleteResponse clientPreorderDeleteResponse = new VAClientPreorderDeleteResponse();
                if (clientPreorderDeleteRequest == null)
                {
                    clientPreorderDeleteResponse.result = VAResult.VA_FAILED_MSG_ERROR;
                }
                else
                {
                    PreOrder19dianOperate preOrder19dianOpe = new PreOrder19dianOperate();
                    clientPreorderDeleteResponse = preOrder19dianOpe.ClientRemovePreorder19dian(clientPreorderDeleteRequest);
                }
                responseJson = JsonOperate.JsonSerializer<VAClientPreorderDeleteResponse>(clientPreorderDeleteResponse);
                break;
            #endregion

            #region 查询点单评价信息
            case (int)VAMessageType.CLIENT_QUERY_PREORDER_EVALUATIONINFO_REQUEST:
                VAClientQueryPreorderEvaluationInfoRequest queryPreorderEvaluationInfoRequest = JsonOperate.JsonDeserialize<VAClientQueryPreorderEvaluationInfoRequest>(msg);
                VAClientQueryPreorderEvaluationInfoResponse queryPreorderEvaluationInfoResponse = new VAClientQueryPreorderEvaluationInfoResponse();
                if (queryPreorderEvaluationInfoRequest == null)
                {
                    queryPreorderEvaluationInfoResponse.result = VAResult.VA_FAILED_MSG_ERROR;
                }
                else
                {
                    CustomerOperate customerOpe = new CustomerOperate();
                    queryPreorderEvaluationInfoResponse = customerOpe.QueryPreorderEvaluationInfo(queryPreorderEvaluationInfoRequest);

                }
                responseJson = JsonOperate.JsonSerializer<VAClientQueryPreorderEvaluationInfoResponse>(queryPreorderEvaluationInfoResponse);//回复信息反序列化
                break;
            #endregion

            #region 用户评价点单
            case (int)VAMessageType.CLIENT_EVALUATE_PREORDER_REQUEST:
                VAClientEvaluatePreorderRequest clientEvaluatePreorderRequest = JsonOperate.JsonDeserialize<VAClientEvaluatePreorderRequest>(msg);
                VAClientEvaluatePreorderResponse clientEvaluatePreorderResponse = new VAClientEvaluatePreorderResponse();
                if (clientEvaluatePreorderRequest == null)
                {
                    clientEvaluatePreorderResponse.result = VAResult.VA_FAILED_MSG_ERROR;
                }
                else
                {
                    CustomerOperate customerOpe = new CustomerOperate();
                    clientEvaluatePreorderResponse = customerOpe.ClientEvaluatePreorder(clientEvaluatePreorderRequest);
                }
                responseJson = JsonOperate.JsonSerializer<VAClientEvaluatePreorderResponse>(clientEvaluatePreorderResponse);//回复信息反序列化

                new UserIntegralSend().Send(clientEvaluatePreorderRequest, responseJson);
                break;
            #endregion

            #region 用户查询最新的需要评价的点单
            case (int)VAMessageType.CLIENT_QUERY_PREORDER_NOTEVALUATED_REQUEST:
                VAClientQueryPreorderNotEvaluatedRequest clientQueryPreorderNotEvaluatedRequest = JsonOperate.JsonDeserialize<VAClientQueryPreorderNotEvaluatedRequest>(msg);
                VAClientQueryPreorderNotEvaluatedResponse clientQueryPreorderNotEvaluatedResponse = new VAClientQueryPreorderNotEvaluatedResponse();
                if (clientQueryPreorderNotEvaluatedRequest == null)
                {
                    clientQueryPreorderNotEvaluatedResponse.result = VAResult.VA_FAILED_MSG_ERROR;
                }
                else
                {
                    CustomerOperate customerOpe = new CustomerOperate();
                    clientQueryPreorderNotEvaluatedResponse = customerOpe.ClientQueryPreorderNotEvaluated(clientQueryPreorderNotEvaluatedRequest);
                }
                responseJson = JsonOperate.JsonSerializer<VAClientQueryPreorderNotEvaluatedResponse>(clientQueryPreorderNotEvaluatedResponse);
                break;
            #endregion

            #region 点单详情点赞
            case (int)VAMessageType.CLIENT_DISH_PRAISE_REQUEST:
                VAClientDishPraiseRequest request1214 = JsonOperate.JsonDeserialize<VAClientDishPraiseRequest>(msg);
                VAClientDishPraiseResponse response1215 = new VAClientDishPraiseResponse();
                if (request1214 == null)
                {
                    response1215.result = VAResult.VA_FAILED_MSG_ERROR;
                }
                else
                {
                    response1215 = new PreOrder19dianOperate().ClientDishPraise(request1214);
                }
                responseJson = JsonOperate.JsonSerializer<VAClientDishPraiseResponse>(response1215);

                new UserIntegralSend().Send(request1214, responseJson);
                break;
            #endregion

            //菜品相关
            #region 客户端查询店铺的其他信息（沽清菜品，支付方式，最新菜谱，粮票红包金额）
            case (int)VAMessageType.CLIENT_SHOP_SELLOFF_REQUEST:
                VAShopSellOffDishRequest vAShopSellOffDishRequest = JsonOperate.JsonDeserialize<VAShopSellOffDishRequest>(msg);
                VAShopSellOffDishResponse vAShopSellOffDishResponse = new VAShopSellOffDishResponse();
                if (vAShopSellOffDishRequest == null)
                {
                    vAShopSellOffDishResponse.result = VAResult.VA_FAILED_MSG_ERROR;
                }
                else
                {
                    MenuOperate menuOperate = new MenuOperate();
                    vAShopSellOffDishResponse = menuOperate.ClientQuerySellOffDishs(vAShopSellOffDishRequest);
                }
                responseJson = JsonOperate.JsonSerializer<VAShopSellOffDishResponse>(vAShopSellOffDishResponse);
                break;
            #endregion

            #region 客户端显示推荐菜品列表
            case (int)VAMessageType.CLIENT_COMMON_DISHLIST_REQUEST:
                VACommonDishListRequest commonDishListRequest = JsonOperate.JsonDeserialize<VACommonDishListRequest>(msg);
                VACommonDishListReponse commonDishListReponse = new VACommonDishListReponse();
                if (commonDishListRequest == null)
                {
                    commonDishListReponse.result = VAResult.VA_FAILED_MSG_ERROR;
                }
                else
                {
                    ClientIndexListOperate clientIndexOper = new ClientIndexListOperate();
                    commonDishListReponse = clientIndexOper.ClientCommonDishList(commonDishListRequest);
                }
                responseJson = JsonOperate.JsonSerializer<VACommonDishListReponse>(commonDishListReponse);
                break;
            #endregion

            #region 客户端修改发票抬头 wangcheng 2013/9/15
            case (int)VAMessageType.CLIENT_PREORDER_ADD_INVOICETITLE_REQUEST:
                VAPreOrderInvoiceTitleRequest vaPreOrderInvoiceTitleRequest = JsonOperate.JsonDeserialize<VAPreOrderInvoiceTitleRequest>(msg);
                VAPreOrderInvoiceTitleResponse vaPreOrderInvoiceTitleResponce = new VAPreOrderInvoiceTitleResponse();
                if (vaPreOrderInvoiceTitleRequest == null)
                {
                    vaPreOrderInvoiceTitleResponce.result = VAResult.VA_FAILED_MSG_ERROR;
                }
                else
                {
                    PreOrder19dianOperate preOrder19dianOperate = new PreOrder19dianOperate();
                    vaPreOrderInvoiceTitleResponce = preOrder19dianOperate.ClientAddPreOrderInvoiceTitle(vaPreOrderInvoiceTitleRequest);
                }
                responseJson = JsonOperate.JsonSerializer<VAPreOrderInvoiceTitleResponse>(vaPreOrderInvoiceTitleResponce);
                break;
            #endregion

            //客户端余额，账户相关
            #region 客户端退款到余额  2013/9/15
            case (int)VAMessageType.CLIENT_PREORDER_REFUND_REQUEST:
                VAPreOrderRefundRequest VAPreOrderRefundRequest = JsonOperate.JsonDeserialize<VAPreOrderRefundRequest>(msg);
                VAPreOrderRefundResponse VAPreOrderRefundResponse = new VAPreOrderRefundResponse();
                if (VAPreOrderRefundRequest == null)
                {
                    VAPreOrderRefundResponse.result = VAResult.VA_FAILED_MSG_ERROR;
                }
                else
                {
                    PreOrder19dianOperate preOrder19dianOperate = new PreOrder19dianOperate();
                    VAPreOrderRefundResponse = preOrder19dianOperate.ClientRefund(VAPreOrderRefundRequest);
                }
                responseJson = JsonOperate.JsonSerializer<VAPreOrderRefundResponse>(VAPreOrderRefundResponse);
                break;
            #endregion

            #region 客户端原路退款申请  2014/1/28
            case (int)VAMessageType.CLIENT_ORIGINAL_REFUNF_REQUEST:
                VAOriginalRefundRequest originalRefundRequest = JsonOperate.JsonDeserialize<VAOriginalRefundRequest>(msg);
                VAOriginalRefundResponse originalRefundResponse = new VAOriginalRefundResponse();
                if (originalRefundRequest == null)
                {//如果Json反序列化出错，则直接返回错误信息
                    originalRefundResponse.result = VAResult.VA_FAILED_MSG_ERROR;
                }
                else
                {
                    PreOrder19dianOperate preOrder19dianOperate = new PreOrder19dianOperate();
                    originalRefundResponse = preOrder19dianOperate.ClientOriginalRefund(originalRefundRequest);
                }
                responseJson = JsonOperate.JsonSerializer<VAOriginalRefundResponse>(originalRefundResponse);//回复信息反序列化
                break;
            #endregion

            #region 获取用户余额以及详细记录
            case (int)VAMessageType.USER_WALLET_TRANSACTION_LIST_REQUEST:
                VAUserWalletTransactionListRequest userWalletTransactionListRequest = JsonOperate.JsonDeserialize<VAUserWalletTransactionListRequest>(msg);
                VAUserWalletTransactionListResponse userWalletTransactionListResponse = new VAUserWalletTransactionListResponse();
                if (userWalletTransactionListRequest == null)
                {//如果Json反序列化出错，则直接返回错误信息
                    userWalletTransactionListResponse.result = VAResult.VA_FAILED_MSG_ERROR;
                }
                else
                {//Json序列化正常，则调用相应函数
                    CustomerOperate customerOperate = new CustomerOperate();
                    userWalletTransactionListResponse = customerOperate.ClientQueryUserWallet(userWalletTransactionListRequest);
                }
                responseJson = JsonOperate.JsonSerializer<VAUserWalletTransactionListResponse>(userWalletTransactionListResponse);//回复信息反序列化
                break;
            #endregion

            #region 客户端充值
            //case (int)VAMessageType.CLIENT_TOPUP_REQUEST:
            //    VATopUpRequest topUpRequest = JsonOperate.JsonDeserialize<VATopUpRequest>(msg);
            //    VATopUpResponse topUpResponse = new VATopUpResponse();
            //    if (topUpRequest == null)
            //    {//如果Json反序列化出错，则直接返回错误信息
            //        topUpResponse.result = VAResult.VA_FAILED_MSG_ERROR;
            //    }
            //    else
            //    {//Json序列化正常，则调用相应函数
            //        CustomerOperate customerOperate = new CustomerOperate();
            //        topUpResponse = customerOperate.ClientTopUp(topUpRequest);
            //    }
            //    responseJson = JsonOperate.JsonSerializer<VATopUpResponse>(topUpResponse);//回复信息反序列化
            //    break;
            #endregion

            #region 客户端查询获取个人信息
            case (int)VAMessageType.USER_INFO_QUERY_REQUEST:
                VAClientUserInfoRequest clientUserInfoRequest = JsonOperate.JsonDeserialize<VAClientUserInfoRequest>(msg);
                VAClientUserInfoReponse clientUserInfoReponse = new VAClientUserInfoReponse();
                if (clientUserInfoRequest == null)
                {
                    clientUserInfoReponse.result = VAResult.VA_FAILED_MSG_ERROR;
                }
                else
                {
                    clientUserInfoReponse = ClientIndexListOperate.ClientQueryUserInfo(clientUserInfoRequest);
                }
                responseJson = JsonOperate.JsonSerializer<VAClientUserInfoReponse>(clientUserInfoReponse);
                break;

            #endregion

            #region 客户端查看用户红包金额，和优惠券数量
            case (int)VAMessageType.CLIENT_CHECK_REDENVELOPE_REQUEST:
                ClientCheckRedEnvelopeRequest clientCheckRedEnvelopeRequest = JsonOperate.JsonDeserialize<ClientCheckRedEnvelopeRequest>(msg);
                ClientCheckRedEnvelopeResponse clientCheckRedEnvelopeResponse = new ClientCheckRedEnvelopeResponse();
                if (clientCheckRedEnvelopeRequest == null)
                {
                    clientCheckRedEnvelopeResponse.result = VAResult.VA_FAILED_MSG_ERROR;
                }
                else
                {
                    RedEnvelopeOperate redEnvelopeOperate = new RedEnvelopeOperate();
                    clientCheckRedEnvelopeResponse = redEnvelopeOperate.QueryCustomerRedEnvelope(clientCheckRedEnvelopeRequest);
                }
                responseJson = JsonOperate.JsonSerializer<ClientCheckRedEnvelopeResponse>(clientCheckRedEnvelopeResponse);
                break;
            #endregion

            //客户端门店信息相关
            #region 客户端首页加载门店列表（分页）
            case (int)VAMessageType.CLIENT_INDEX_LIST_REQUEST:
                ClientIndexListRequest clientIndexListRequest = JsonOperate.JsonDeserialize<ClientIndexListRequest>(msg);
                ClientIndexListResponse clientIndexListResponse = new ClientIndexListResponse();
                if (clientIndexListRequest == null)
                {
                    clientIndexListResponse.result = VAResult.VA_FAILED_MSG_ERROR;
                }
                else
                {
                    ClientIndexListOperate clientIndexOper = new ClientIndexListOperate();
                    clientIndexListResponse = clientIndexOper.ClientQueryIndexList(clientIndexListRequest);
                }
                responseJson = JsonOperate.JsonSerializer<ClientIndexListResponse>(clientIndexListResponse);
                break;
            #endregion

            #region 客户端搜索门店列表
            case (int)VAMessageType.CLIENT_SEARCH_SHOP_LIST_REQUEST:
                VAClientSearchShopListRequest vaClientSearchShopListRequest = JsonOperate.JsonDeserialize<VAClientSearchShopListRequest>(msg);
                VAClientSearchShopListReponse vaClientSearchShopListReponse = new VAClientSearchShopListReponse();
                if (vaClientSearchShopListRequest == null)
                {
                    vaClientSearchShopListReponse.result = VAResult.VA_FAILED_MSG_ERROR;
                }
                else
                {
                    ClientIndexListOperate clientIndexOper = new ClientIndexListOperate();
                    vaClientSearchShopListReponse = clientIndexOper.ClientSearchShopList(vaClientSearchShopListRequest);
                }
                responseJson = JsonOperate.JsonSerializer<VAClientSearchShopListReponse>(vaClientSearchShopListReponse);//回复信息反序列化
                break;
            #endregion

            #region 客户端查询门店详情
            case (int)VAMessageType.CLIENT_SHOP_DETAIL_REQUEST:
                VAClientShopDetailRequest shopDetailRequset = JsonOperate.JsonDeserialize<VAClientShopDetailRequest>(msg);
                VAClientShopDetailResponse shopDetailResponse = new VAClientShopDetailResponse();
                if (shopDetailRequset == null)
                {//如果Json反序列化出错，则直接返回错误信息
                    shopDetailResponse.result = VAResult.VA_FAILED_MSG_ERROR;
                }
                else
                {
                    ShopOperate shopOperate = new ShopOperate();
                    shopDetailResponse = shopOperate.ClientQueryShopEnvironment(shopDetailRequset);
                }
                responseJson = JsonOperate.JsonSerializer<VAClientShopDetailResponse>(shopDetailResponse);//回复信息反序列化
                break;
            #endregion

            #region 用户收藏和删除收藏门店
            case (int)VAMessageType.USER_SETFAVORITESHOP_REQUEST:
                VAUserSetFavoriteShopRequest vaUserSetFavoriteShopRequest = JsonOperate.JsonDeserialize<VAUserSetFavoriteShopRequest>(msg);
                VAUserSetFavoriteShopResponse vaUserSetFavoriteShopResponse = new VAUserSetFavoriteShopResponse();
                if (vaUserSetFavoriteShopRequest == null)
                {
                    vaUserSetFavoriteShopResponse.result = VAResult.VA_FAILED_MSG_ERROR;
                }
                else
                {
                    ClientIndexListOperate clientIndexOper = new ClientIndexListOperate();
                    vaUserSetFavoriteShopResponse = clientIndexOper.ClientSetFavoriteCompany(vaUserSetFavoriteShopRequest);
                }
                responseJson = JsonOperate.JsonSerializer<VAUserSetFavoriteShopResponse>(vaUserSetFavoriteShopResponse);//回复信息反序列化

                new UserIntegralSend().Send(vaUserSetFavoriteShopRequest, responseJson);
                break;
            #endregion

            //客户端支付相关
            #region 充值，购买粮票
            case (int)VAMessageType.CLIENT_PERSON_CENTER_RECHARGE_REQUEST:
                ClientPersonCenterRechargeRequest clientPersonCenterRechargeRequest = JsonOperate.JsonDeserialize<ClientPersonCenterRechargeRequest>(msg);
                ClientPersonCenterRechargeReponse clientPersonCenterRechargeReponse = new ClientPersonCenterRechargeReponse();
                if (clientPersonCenterRechargeRequest == null)
                {
                    clientPersonCenterRechargeReponse.result = VAResult.VA_FAILED_MSG_ERROR;
                }
                else
                {
                    ClientIndexListOperate clientIndexOper = new ClientIndexListOperate();
                    clientPersonCenterRechargeReponse = ClientIndexListOperate.ClientPersonCenterRecharge(clientPersonCenterRechargeRequest);
                }
                responseJson = JsonOperate.JsonSerializer<ClientPersonCenterRechargeReponse>(clientPersonCenterRechargeReponse);//回复信息反序列化
                break;
            #endregion

            #region 客户端点菜支付（new）
            case (int)VAMessageType.CLIENT_RECHARGE_PAYMENT_ORDER_REQUEST:
                ClientRechargePaymentOrderRequest clientRechargePaymentOrderRequest = JsonOperate.JsonDeserialize<ClientRechargePaymentOrderRequest>(msg);
                ClientRechargePaymentOrderResponse clientRechargePaymentOrderResponse = new ClientRechargePaymentOrderResponse();
                if (clientRechargePaymentOrderRequest == null)
                {
                    clientRechargePaymentOrderResponse.result = VAResult.VA_FAILED_MSG_ERROR;
                }
                else
                {
                    ClientIndexListOperate operate = new ClientIndexListOperate();
                    clientRechargePaymentOrderResponse = operate.ClientRechargePaymentOrder(clientRechargePaymentOrderRequest);
                }
                responseJson = JsonOperate.JsonSerializer<ClientRechargePaymentOrderResponse>(clientRechargePaymentOrderResponse);

                new UserIntegralSend().Send(clientRechargePaymentOrderRequest, responseJson);
                break;
            #endregion

            #region 客户端直接支付（new）
            case (int)VAMessageType.CLIENT_RECHARGE_DIRECT_PAYMENT_REQUEST:
                ClientRechargeDirectPaymentRequest clientRechargeDirectPaymentRequest = JsonOperate.JsonDeserialize<ClientRechargeDirectPaymentRequest>(msg);
                ClientRechargeDirectPaymentResponse clientRechargeDirectPaymentResponse = new ClientRechargeDirectPaymentResponse();
                if (clientRechargeDirectPaymentRequest == null)
                {
                    clientRechargeDirectPaymentResponse.result = VAResult.VA_FAILED_MSG_ERROR;
                }
                else
                {
                    ClientIndexListOperate operate = new ClientIndexListOperate();
                    clientRechargeDirectPaymentResponse = operate.ClientRechargeDirectPayment(clientRechargeDirectPaymentRequest);
                }
                responseJson = JsonOperate.JsonSerializer<ClientRechargeDirectPaymentResponse>(clientRechargeDirectPaymentResponse);

                new UserIntegralSend().Send(clientRechargeDirectPaymentRequest, responseJson);
                break;
            #endregion

            #region 客户端点菜支付（V1）
            case (int)VAMessageType.CLIENT_RECHARGE_PAYMENT_ORDER_V1_REQUEST:
                clientRechargePaymentOrderRequest = JsonOperate.JsonDeserialize<ClientRechargePaymentOrderRequest>(msg);
                clientRechargePaymentOrderResponse = null;
                if (clientRechargePaymentOrderRequest == null)
                {
                    clientRechargePaymentOrderResponse.result = VAResult.VA_FAILED_MSG_ERROR;
                }
                else
                {
                    clientRechargePaymentOrderResponse = ClientIndexListOperate.ClientPaymentOrder(clientRechargePaymentOrderRequest);
                }
                responseJson = JsonOperate.JsonSerializer<ClientRechargePaymentOrderResponse>(clientRechargePaymentOrderResponse);

                new UserIntegralSend().Send(clientRechargePaymentOrderRequest, responseJson);
                break;
            #endregion

            #region 客户端直接支付支付（V1）
            case (int)VAMessageType.CLIENT_RECHARGE_DIRECT_PAYMENT_V1_REQUEST:
                clientRechargeDirectPaymentRequest = JsonOperate.JsonDeserialize<ClientRechargeDirectPaymentRequest>(msg);
                clientRechargeDirectPaymentResponse = new ClientRechargeDirectPaymentResponse();
                if (clientRechargeDirectPaymentRequest == null)
                {
                    clientRechargeDirectPaymentResponse.result = VAResult.VA_FAILED_MSG_ERROR;
                }
                else
                {
                    ClientIndexListOperate operate = new ClientIndexListOperate();
                    clientRechargeDirectPaymentResponse = operate.ClientDirectPayment(clientRechargeDirectPaymentRequest);
                }
                responseJson = JsonOperate.JsonSerializer<ClientRechargeDirectPaymentResponse>(clientRechargeDirectPaymentResponse);

                new UserIntegralSend().Send(clientRechargeDirectPaymentRequest, responseJson);
                break;
            #endregion

            #region 客户端补差价请求
            case (int)VAMessageType.CLIENT_PAY_DIFFERENCE_REQUEST:
                PayDiffenenceRequest payDiffenenceRequest = JsonOperate.JsonDeserialize<PayDiffenenceRequest>(msg);
                var payDiffenenceResponse = new PayDiffenenceResponse();
                if (payDiffenenceRequest == null)
                {
                    payDiffenenceRequest.result = VAResult.VA_FAILED_MSG_ERROR;
                }
                else
                {
                    ClientIndexListOperate operate = new ClientIndexListOperate();
                    payDiffenenceResponse = operate.ClinetPayDiffenence(payDiffenenceRequest);
                }
                responseJson = JsonOperate.JsonSerializer<PayDiffenenceResponse>(payDiffenenceResponse);
                break;
            #endregion

            #region 客户端补差价请求
            case (int)VAMessageType.CIIENT_PAY_SUCCESS_REQUEST:
                ClientPaySuccessRequest clientpaySuccessRequest = JsonOperate.JsonDeserialize<ClientPaySuccessRequest>(msg);
                var clientpaySuccessResponse = new ClientpaySuccessResponse();
                if (clientpaySuccessResponse == null)
                {
                    clientpaySuccessResponse.result = VAResult.VA_FAILED_MSG_ERROR;
                }
                else
                {
                    ClientIndexListOperate operate = new ClientIndexListOperate();
                    clientpaySuccessResponse = operate.PaySuccessRequest(clientpaySuccessRequest);
                }
                responseJson = JsonOperate.JsonSerializer<ClientpaySuccessResponse>(clientpaySuccessResponse);
                break;
            #endregion

            #region 客户端补差价查询
            case (int)VAMessageType.CLIENT_PAY_DIFFERENCE_QUERY_REQUEST:
                PayDiffenenceQueryRequest payDiffenenceQueryRequest = JsonOperate.JsonDeserialize<PayDiffenenceQueryRequest>(msg);
                var payDiffenenceQueryResponse = new PayDiffenenceQueryResponse();
                if (payDiffenenceQueryRequest == null)
                {
                    payDiffenenceQueryResponse.result = VAResult.VA_FAILED_MSG_ERROR;
                }
                else
                {
                    ClientIndexListOperate operate = new ClientIndexListOperate();
                    payDiffenenceQueryResponse = operate.ClinetPayDiffenenceQuery(payDiffenenceQueryRequest);
                }
                responseJson = JsonOperate.JsonSerializer<PayDiffenenceQueryResponse>(payDiffenenceQueryResponse);
                break;
            #endregion
            //其他
            #region 客户端发送语音短信
            case (int)VAMessageType.CLIENT_SEND_VOICEMESSAGE_REQUEST:
                VAClientSendVoiceMessageRequest clientSendVoiceMessageRequest = JsonOperate.JsonDeserialize<VAClientSendVoiceMessageRequest>(msg);
                VAClientSendVoiceMessageResponse clientSendVoiceMessageResponse = new VAClientSendVoiceMessageResponse();
                if (clientSendVoiceMessageRequest == null)
                {
                    clientSendVoiceMessageResponse.result = VAResult.VA_FAILED_MSG_ERROR;
                }
                else
                {
                    CustomerOperate customerOperate = new CustomerOperate();
                    clientSendVoiceMessageResponse = customerOperate.ClientSendVoiceMessage(clientSendVoiceMessageRequest);
                }
                responseJson = JsonOperate.JsonSerializer<VAClientSendVoiceMessageResponse>(clientSendVoiceMessageResponse);//回复信息反序列化
                break;
            #endregion

            #region 客户端发送错误日志
            case (int)VAMessageType.CLIENT_SEND_ERRORMESSAGE_REQUEST:
                VAClientSendErrorMessageRequest clientSendErrorMessageRequest = JsonOperate.JsonDeserialize<VAClientSendErrorMessageRequest>(msg);
                VAClientSendErrorMessageResponse clientSendErrorMessageResponse = new VAClientSendErrorMessageResponse();
                if (clientSendErrorMessageRequest == null)
                {
                    clientSendErrorMessageResponse.result = VAResult.VA_FAILED_MSG_ERROR;
                }
                else
                {
                    CustomerOperate customerOperate = new CustomerOperate();
                    clientSendErrorMessageResponse = customerOperate.ClientSendErrorMessage(clientSendErrorMessageRequest);
                }
                responseJson = JsonOperate.JsonSerializer<VAClientSendErrorMessageResponse>(clientSendErrorMessageResponse);//回复信息反序列化
                break;
            #endregion

            #region 客户端查询版本信息
            case (int)VAMessageType.CLIENT_QUERY_BUILD_REQUEST:
                VAClientQueryBuildRequest clientQueryBuildRequest = JsonOperate.JsonDeserialize<VAClientQueryBuildRequest>(msg);
                VAClientQueryBuildResponse clientQueryBuildResponse = new VAClientQueryBuildResponse();
                if (clientQueryBuildRequest == null)
                {
                    clientQueryBuildResponse.result = VAResult.VA_FAILED_MSG_ERROR;
                }
                else
                {
                    CustomerOperate customerOperate = new CustomerOperate();
                    clientQueryBuildResponse = customerOperate.ClientQueryBuild(clientQueryBuildRequest);
                }
                responseJson = JsonOperate.JsonSerializer<VAClientQueryBuildResponse>(clientQueryBuildResponse);//回复信息反序列化
                break;
            #endregion

            #region 美食日记
            case (int)VAMessageType.CLIENT_FOODDIARY_SHARED_REQUEST:
                VAClientFoodDiarySharedRequest clientFoodDiarySharedRequest =
                    JsonOperate.JsonDeserialize<VAClientFoodDiarySharedRequest>(msg);
                VAClientFoodDiarySharedResponse clientFoodDiarySharedResponse = new VAClientFoodDiarySharedResponse();
                if (clientFoodDiarySharedRequest == null)
                {
                    clientFoodDiarySharedRequest.result = VAResult.VA_FAILED_MSG_ERROR;
                }
                else
                {
                    CustomerOperate customerOperate = new CustomerOperate();
                    clientFoodDiarySharedResponse = customerOperate.FoodDiaryShared(clientFoodDiarySharedRequest);
                }
                responseJson = JsonOperate.JsonSerializer<VAClientFoodDiarySharedResponse>(clientFoodDiarySharedResponse);//回复信息反序列化
                break;

            #endregion

            #region -------------------------------------------------------------
            case (int)VAMessageType.CLIENT_SHOP_NOTICE_REQUEST://公告接口
                VAClientShopNoticeRequest shopNoticeRequest = JsonOperate.JsonDeserialize<VAClientShopNoticeRequest>(msg);
                VAClientShopNoticeResponse shopNoticeResponse = new VAClientShopNoticeResponse();
                if (shopNoticeRequest == null)
                {
                    shopNoticeResponse.result = VAResult.VA_FAILED_MSG_ERROR;
                }
                else
                {
                    ShopNoticeOperate objShopNoticeOperate = new ShopNoticeOperate();
                    shopNoticeResponse = objShopNoticeOperate.GetShopNotice(shopNoticeRequest);
                }
                responseJson = JsonOperate.JsonSerializer<VAClientShopNoticeResponse>(shopNoticeResponse);
                break;

            case (int)VAMessageType.CLIENT_LOTTERY_REQUEST://抽奖接口
                VAClientLotteryRequest lotteryRequest = JsonOperate.JsonDeserialize<VAClientLotteryRequest>(msg);
                VAClientLotteryResponse lotteryResponse = new VAClientLotteryResponse();
                if (lotteryRequest == null)
                {
                    lotteryResponse.result = VAResult.VA_FAILED_MSG_ERROR;
                }
                else
                {
                    LotteryOperate lotteryOperate = new LotteryOperate();
                    lotteryResponse = lotteryOperate.ClientLottery(lotteryRequest);
                }
                responseJson = JsonOperate.JsonSerializer<VAClientLotteryResponse>(lotteryResponse);
                break;

            case (int)VAMessageType.COUNTING_LIST_REQUEST://统计列表接口
                CountingListRequest countingRequest = JsonOperate.JsonDeserialize<CountingListRequest>(msg);
                CountingListResponse countingResponse = new CountingListResponse();
                if (countingRequest == null)
                {
                    countingResponse.result = VAResult.VA_FAILED_MSG_ERROR;
                }
                else
                {
                    ZZBPreOrderOperate zzbPreOrderOperate = new ZZBPreOrderOperate();
                    countingResponse = zzbPreOrderOperate.SelectAwardTotalRole(countingRequest);
                }
                responseJson = JsonOperate.JsonSerializer<CountingListResponse>(countingResponse);
                break;
            #endregion

            #region 悠先点菜商圈
            case (int)VAMessageType.CLIENT_CHECK_BUSINESSDISTRICT_REQUEST:
                ClientCheckBusinessDistrictRequest businessDistrictRequest = JsonOperate.JsonDeserialize<ClientCheckBusinessDistrictRequest>(msg);
                ClientCheckBusinessDistrictResponse businessDistrictResponse = new ClientCheckBusinessDistrictResponse();
                if (businessDistrictRequest == null)
                {
                    businessDistrictResponse.result = VAResult.VA_FAILED_MSG_ERROR;
                }
                else
                {
                    ClientIndexListOperate businessDistrictOperate = new ClientIndexListOperate();
                    businessDistrictResponse = businessDistrictOperate.ClientCheckBusinessDistrict(businessDistrictRequest);
                }
                responseJson = JsonOperate.JsonSerializer<ClientCheckBusinessDistrictResponse>(businessDistrictResponse);
                break;
            #endregion

            #region 悠先点菜美食广场
            case (int)VAMessageType.CLIENT_CHECK_FOODPLAZA_REQUEST:
                ClientCheckFoodPlazaRequest foodPlazaRequest = JsonOperate.JsonDeserialize<ClientCheckFoodPlazaRequest>(msg);
                ClientCheckFoodPlazaReponse foodPlazaResponse = new ClientCheckFoodPlazaReponse();
                if (foodPlazaRequest == null)
                {
                    foodPlazaResponse.result = VAResult.VA_FAILED_MSG_ERROR;
                }
                else
                {
                    FoodPlazaOperate foodPlazaOperate = new FoodPlazaOperate();
                    foodPlazaResponse = foodPlazaOperate.ClientCheckFoodPlaza(foodPlazaRequest);
                }
                responseJson = JsonOperate.JsonSerializer<ClientCheckFoodPlazaReponse>(foodPlazaResponse);
                break;
            #endregion

            #region 悠先点菜举报信息获取和举报操作
            case (int)VAMessageType.CLIENT_SHOP_REPORT_REQUEST:
                VAClientShopReportRequest reportRequest = JsonOperate.JsonDeserialize<VAClientShopReportRequest>(msg);
                VAClientShopReportResponse reportResponse = new VAClientShopReportResponse();
                if (reportRequest == null)
                {
                    reportResponse.result = VAResult.VA_FAILED_MSG_ERROR;
                }
                else
                {
                    reportResponse = new ClientExtendOperate().DoClientCustomerReportShop(reportRequest);
                }
                responseJson = JsonOperate.JsonSerializer<VAClientShopReportResponse>(reportResponse);
                break;
            #endregion

            #region 查询个人优惠券列表
            case (int)VAMessageType.CLIENT_CUSTOMER_COUPONDETAIL_REQUEST:
                VAClientCouponPacketDetailRequest couponDetailRequest = JsonOperate.JsonDeserialize<VAClientCouponPacketDetailRequest>(msg);
                VAClientCouponPacketDetailResponse couponDetailResponse = new VAClientCouponPacketDetailResponse();
                if (couponDetailRequest == null)
                {
                    couponDetailResponse.result = VAResult.VA_FAILED_MSG_ERROR;
                }
                else
                {
                    couponDetailResponse = new CouponOperate().GetClientCouponPacketDetails(couponDetailRequest);
                }
                responseJson = JsonOperate.JsonSerializer<VAClientCouponPacketDetailResponse>(couponDetailResponse);
                break;
            //当前优惠券
            case (int)VAMessageType.CLIENT_CUSTOMER_CURRENTCOUPONDETAIL_REQUEST:
                VAClientCouponPacketDetailRequest currentCouponDetailRequest = JsonOperate.JsonDeserialize<VAClientCouponPacketDetailRequest>(msg);
                VAClientRebateDetailResponse currentRebateDetailResponse = new VAClientRebateDetailResponse();
                if (currentCouponDetailRequest == null)
                {
                    currentRebateDetailResponse.result = VAResult.VA_FAILED_MSG_ERROR;
                }
                else
                {
                    currentRebateDetailResponse = new CouponOperate().GetCurrentRebateDetails(currentCouponDetailRequest);
                }
                responseJson = JsonOperate.JsonSerializer<VAClientRebateDetailResponse>(currentRebateDetailResponse);
                break;
            //历史优惠券
            case (int)VAMessageType.CLIENT_CUSTOMER_HISTORYCOUPONDETAIL_REQUEST:
                VAClientCouponPacketDetailRequest historyCouponDetailRequest = JsonOperate.JsonDeserialize<VAClientCouponPacketDetailRequest>(msg);
                VAClientRebateDetailResponse historyRebateDetailResponse = new VAClientRebateDetailResponse();
                if (historyCouponDetailRequest == null)
                {
                    historyRebateDetailResponse.result = VAResult.VA_FAILED_MSG_ERROR;
                }
                else
                {
                    historyRebateDetailResponse = new CouponOperate().GetHistoryRebateDetails(historyCouponDetailRequest);
                }
                responseJson = JsonOperate.JsonSerializer<VAClientRebateDetailResponse>(historyRebateDetailResponse);
                break;
            #endregion

            #region 微信相关

            #region 查看用户是否绑定过微信

            case (int)VAMessageType.CLIENT_CUSTOMER_WECHAT_ISBINDING_REQUEST:
                VAClientWeChatIsBindingRequest weChatIsBindingRequest = JsonOperate.JsonDeserialize<VAClientWeChatIsBindingRequest>(msg);
                VAClientWeChatIsBindingResponse weChatIsBindingResponse = new VAClientWeChatIsBindingResponse() { type = VAMessageType.CLIENT_CUSTOMER_WECHAT_ISBINDING_RESPONSE };
                if (weChatIsBindingRequest == null || string.IsNullOrEmpty(weChatIsBindingRequest.unionId))
                    weChatIsBindingResponse.result = VAResult.VA_FAILED_MSG_ERROR;
                else
                {
                    var weCharModel = new WeChatUserOperator().GetUnionIdOfModel(weChatIsBindingRequest.unionId);
                    if (weCharModel == null)
                    {
                        weChatIsBindingResponse.result = VAResult.VA_OK;
                        weChatIsBindingResponse.state = 0;
                    }
                    else
                    {
                        if (weCharModel.CustomerInfo_CustomerID != 0)
                        {
                            var userModel = new CustomerOperate().QueryCustomer(weCharModel.CustomerInfo_CustomerID);
                            if (userModel != null && !string.IsNullOrEmpty(userModel.mobilePhoneNumber))
                            {
                                weChatIsBindingResponse.state = 1;
                                weChatIsBindingResponse.mobile = userModel.mobilePhoneNumber;
                            }
                            else
                                weChatIsBindingResponse.state = 3;
                        }
                        else if (!string.IsNullOrEmpty(weCharModel.MobilePhoneNumber))
                        {
                            weChatIsBindingResponse.state = 2;
                            weChatIsBindingResponse.mobile = weCharModel.MobilePhoneNumber;
                        }
                        else
                            weChatIsBindingResponse.state = 0;

                        weChatIsBindingResponse.result = VAResult.VA_OK;
                    }
                }
                responseJson = JsonOperate.JsonSerializer<VAClientWeChatIsBindingResponse>(weChatIsBindingResponse);
                break;

            #endregion

            #region 微信用户绑定手机号

            case (int)VAMessageType.CLIENT_CUSTOMER_WECHAT_BINDINGMOBILE_REQUEST:
                VAClientWeChatBindingMobileRequest weChatBindingMobileRequest = JsonOperate.JsonDeserialize<VAClientWeChatBindingMobileRequest>(msg);
                VAClientWeChatBindingMobileResponse weChatBindingMobileResponse = new VAClientWeChatBindingMobileResponse() { type = VAMessageType.CLIENT_CUSTOMER_WECHAT_BINDINGMOBILE_RESPONSE };
                if (weChatBindingMobileRequest == null)
                    weChatBindingMobileRequest.result = VAResult.VA_FAILED_MSG_ERROR;
                else
                {
                    CustomerOperate customerOperate = new CustomerOperate();
                    weChatBindingMobileResponse = customerOperate.ClientWeChatUserBindingMobile(weChatBindingMobileRequest);
                }
                responseJson = JsonOperate.JsonSerializer<VAClientWeChatBindingMobileResponse>(weChatBindingMobileResponse);
                break;

            #endregion

            #region 判断微信用户是否绑定手机号

            case (int)VAMessageType.CLIENT_CUSTOMER_WECHAT_ISBINDINGMOBILE_REQUEST:
                VAClientWeChatIsBindingMobileRequest weChatIsBindingMobileRequest = JsonOperate.JsonDeserialize<VAClientWeChatIsBindingMobileRequest>(msg);
                VAClientWeChatIsBindingMobileResponse weChatIsBindingMobileResponse = new VAClientWeChatIsBindingMobileResponse();
                if (weChatIsBindingMobileRequest == null)
                    weChatIsBindingMobileRequest.result = VAResult.VA_FAILED_MSG_ERROR;
                else
                {
                    CustomerOperate customerOperate = new CustomerOperate();
                    weChatIsBindingMobileResponse = customerOperate.ClientWeChatUserIsBindingMobile(weChatIsBindingMobileRequest);
                }
                responseJson = JsonOperate.JsonSerializer<VAClientWeChatIsBindingMobileResponse>(weChatIsBindingMobileResponse);
                break;

            #endregion

            #endregion

            #region 悠先点菜剪刀手入座接口
            case (int)VAMessageType.CLIENT_PREORDER_CONFIRM_REQUEST:
                VAClientPreOrderConfirmRequest preOrderConfirmRequest = JsonOperate.JsonDeserialize<VAClientPreOrderConfirmRequest>(msg);
                VAClientPreOrderConfirmResponse preOrderConfirmResponse = new VAClientPreOrderConfirmResponse();
                if (preOrderConfirmRequest == null)
                {
                    preOrderConfirmResponse.result = VAResult.VA_FAILED_MSG_ERROR;
                }
                else
                {
                    PreOrder19dianOperate preOrder19dianOperate = new PreOrder19dianOperate();
                    preOrderConfirmResponse = preOrder19dianOperate.ClientPreOrderConfirm(preOrderConfirmRequest);
                }
                responseJson = JsonOperate.JsonSerializer<VAClientPreOrderConfirmResponse>(preOrderConfirmResponse);

                //new UserIntegralSend().Send(preOrderConfirmRequest, responseJson);
                break;
            #endregion

            #region 悠先点菜未入座点单提醒
            case (int)VAMessageType.CLIENT_UNCONFIRM_PREORDER_REMIND_REQUEST:
                VAClientUnConfirmPreOrderRequest unConfirmPreOrderRequest = JsonOperate.JsonDeserialize<VAClientUnConfirmPreOrderRequest>(msg);
                VAClientUnConfirmPreOrderResponse unConfirmPreOrderResponse = new VAClientUnConfirmPreOrderResponse();
                if (unConfirmPreOrderRequest == null)
                {
                    unConfirmPreOrderResponse.result = VAResult.VA_FAILED_MSG_ERROR;
                }
                else
                {
                    PreOrder19dianOperate preOrder19dianOperate = new PreOrder19dianOperate();
                    unConfirmPreOrderResponse = preOrder19dianOperate.ClientQueryUnConfirmPreOrder(unConfirmPreOrderRequest);
                }
                responseJson = JsonOperate.JsonSerializer<VAClientUnConfirmPreOrderResponse>(unConfirmPreOrderResponse);
                break;
            #endregion

            //悠先服务相关
            #region 悠先服务模块
            #region 悠先服务查询点单列表详情
            case (int)VAMessageType.ZZB_CLIENT_PREORDERLISTDETAIL_REQUEST:
                ZZB_VAPreOrderListDetailRequest vaPreOrderListDetailRequest = JsonOperate.JsonDeserialize<ZZB_VAPreOrderListDetailRequest>(msg);
                ZZB_VAPreOrderListDetailResponse vaPreOrderListDetailResponse = new ZZB_VAPreOrderListDetailResponse();
                if (vaPreOrderListDetailRequest == null)
                {
                    vaPreOrderListDetailResponse.result = VAResult.VA_FAILED_MSG_ERROR;
                }
                else
                {
                    ZZBPreOrderOperate zzbPreOrderOperate = new ZZBPreOrderOperate();
                    vaPreOrderListDetailResponse = zzbPreOrderOperate.ZZBClientQueryPreOrderListDetail(vaPreOrderListDetailRequest);
                }
                responseJson = JsonOperate.JsonSerializer<ZZB_VAPreOrderListDetailResponse>(vaPreOrderListDetailResponse);
                break;
            #endregion

            #region 悠先服务审核点单
            case (int)VAMessageType.ZZB_CLIENT_PREORDERCONFRIM_REQUEST:
                ZZB_VAPreOrderConfrimRequest vaPreOrderConfrimRequest = JsonOperate.JsonDeserialize<ZZB_VAPreOrderConfrimRequest>(msg);
                ZZB_VAPreOrderConfrimResponse vaPreOrderConfrimResponse = new ZZB_VAPreOrderConfrimResponse();
                if (vaPreOrderConfrimRequest == null)
                {
                    vaPreOrderConfrimResponse.result = VAResult.VA_FAILED_MSG_ERROR;
                }
                else
                {
                    ZZBPreOrderOperate zzbPreOrderOperate = new ZZBPreOrderOperate();
                    vaPreOrderConfrimResponse = zzbPreOrderOperate.ZZBClientPreOrderConfrim(vaPreOrderConfrimRequest);
                }
                responseJson = JsonOperate.JsonSerializer<ZZB_VAPreOrderConfrimResponse>(vaPreOrderConfrimResponse);

                new UserIntegralSend().Send(vaPreOrderConfrimRequest, responseJson);
                break;
            #endregion

            #region 悠先服务点单退款
            case (int)VAMessageType.ZZB_CLIENT_PREORDERREFUND_REQUEST:
                ZZB_VAPreOrderRefundRequest vaPreOrderRefundRequest = JsonOperate.JsonDeserialize<ZZB_VAPreOrderRefundRequest>(msg);
                ZZB_VAPreOrderRefundResponse vaPreOrderRefundResponse = new ZZB_VAPreOrderRefundResponse();
                if (vaPreOrderRefundRequest == null)
                {
                    vaPreOrderRefundResponse.result = VAResult.VA_FAILED_MSG_ERROR;
                }
                else
                {
                    ZZBPreOrderOperate zzbPreOrderOperate = new ZZBPreOrderOperate();
                    vaPreOrderRefundResponse = zzbPreOrderOperate.ZZBClientPreOrderRefund(vaPreOrderRefundRequest);
                }
                responseJson = JsonOperate.JsonSerializer<ZZB_VAPreOrderRefundResponse>(vaPreOrderRefundResponse);//回复信息反序列化
                break;
            #endregion

            #region 悠先服务客户端手机验证
            case (int)VAMessageType.ZZB_CLIENT_USER_MOBILE_REGISTER_REQUEST:
                ZZB_VAUserRegisterRequest userRegisterRequest = JsonOperate.JsonDeserialize<ZZB_VAUserRegisterRequest>(msg);
                ZZB_VAUserRegisterResponse userRegisterResponse = new ZZB_VAUserRegisterResponse();
                if (userRegisterRequest == null)
                {
                    userRegisterResponse.result = VAResult.VA_FAILED_MSG_ERROR;
                }
                else
                {
                    ZZBPreOrderOperate zzbPreOrderOperate = new ZZBPreOrderOperate();
                    userRegisterResponse = zzbPreOrderOperate.ZZBClientMobileVerify(userRegisterRequest);
                }
                responseJson = JsonOperate.JsonSerializer<ZZB_VAUserRegisterResponse>(userRegisterResponse);//回复信息反序列化
                break;
            #endregion

            #region  悠先服务客户端发送语音短信
            case (int)VAMessageType.ZZB_CLIENT_SEND_VOICEMESSAGE_REQUEST:
                ZZB_VAClientSendVoiceMessageRequest vaclientSendVoiceMessageRequest = JsonOperate.JsonDeserialize<ZZB_VAClientSendVoiceMessageRequest>(msg);
                ZZB_VAClientSendVoiceMessageResponse vaclientSendVoiceMessageResponse = new ZZB_VAClientSendVoiceMessageResponse();
                if (vaclientSendVoiceMessageRequest == null)
                {
                    vaclientSendVoiceMessageResponse.result = VAResult.VA_FAILED_MSG_ERROR;
                }
                else
                {
                    ZZBPreOrderOperate zzbPreOrderOperate = new ZZBPreOrderOperate();
                    vaclientSendVoiceMessageResponse = zzbPreOrderOperate.ClientSendVoiceMessage(vaclientSendVoiceMessageRequest);
                }
                responseJson = JsonOperate.JsonSerializer<ZZB_VAClientSendVoiceMessageResponse>(vaclientSendVoiceMessageResponse);//回复信息反序列化
                break;
            #endregion

            #region 悠先服务客户端修改用户信息
            case (int)VAMessageType.ZZB_CLIENT_MODIFY_USERINFO_REQUEST:
                ZZB_VAModifyUserInfoRequest modifyUserInfoRequest = JsonOperate.JsonDeserialize<ZZB_VAModifyUserInfoRequest>(msg);
                ZZB_VAModifyUserInfoResponse modifyUserInfoResponse = new ZZB_VAModifyUserInfoResponse();
                if (modifyUserInfoRequest == null)
                {
                    modifyUserInfoResponse.result = VAResult.VA_FAILED_MSG_ERROR;
                }
                else
                {
                    ZZBPreOrderOperate zzbPreOrderOperate = new ZZBPreOrderOperate();
                    modifyUserInfoResponse = zzbPreOrderOperate.ZZBClientModifyUserInfo(modifyUserInfoRequest);
                }
                responseJson = JsonOperate.JsonSerializer<ZZB_VAModifyUserInfoResponse>(modifyUserInfoResponse);//回复信息反序列化
                break;
            #endregion

            #region 悠先服务查询店铺列表
            case (int)VAMessageType.ZZB_CLIENT_QUERY_SHOPLIST_REQUEST:
                ZZB_VAQueryShopListRequest queryShopListRequest = JsonOperate.JsonDeserialize<ZZB_VAQueryShopListRequest>(msg);
                ZZB_VAQueryShopListResponse queryShopListResponse = new ZZB_VAQueryShopListResponse();
                if (queryShopListRequest == null)
                {
                    queryShopListResponse.result = VAResult.VA_FAILED_MSG_ERROR;
                }
                else
                {
                    ZZBPreOrderOperate zzbPreOrderOperate = new ZZBPreOrderOperate();
                    queryShopListResponse = zzbPreOrderOperate.ZZBQueryShopList(queryShopListRequest);
                }
                responseJson = JsonOperate.JsonSerializer<ZZB_VAQueryShopListResponse>(queryShopListResponse);//回复信息反序列化
                break;
            #endregion

            #region 悠先服务查询版本更新信息
            case (int)VAMessageType.ZZB_CLIENT_QUERY_UPDATEINFO_REQUEST:
                ZZB_VAQueryUpdateInfoResqeust queryUpdateInfoResqeust = JsonOperate.JsonDeserialize<ZZB_VAQueryUpdateInfoResqeust>(msg);
                ZZB_VAQueryUpdateInfoResponse queryUpdateInfoResponse = new ZZB_VAQueryUpdateInfoResponse();
                if (queryUpdateInfoResqeust == null)
                {
                    queryUpdateInfoResponse.result = VAResult.VA_FAILED_MSG_ERROR;
                }
                else
                {
                    ZZBPreOrderOperate zzbPreOrderOperate = new ZZBPreOrderOperate();
                    queryUpdateInfoResponse = zzbPreOrderOperate.QueryUpdateInfoResponse(queryUpdateInfoResqeust);
                }
                responseJson = JsonOperate.JsonSerializer<ZZB_VAQueryUpdateInfoResponse>(queryUpdateInfoResponse);//回复信息反序列化
                break;
            #endregion

            #region 悠先服务发送错误日志
            case (int)VAMessageType.ZZB_CLIENT_SEND_ERRORMESSAGE_REQUEST:
                ZZB_SendErrorMessageRequest sendErrorMessageRequest = JsonOperate.JsonDeserialize<ZZB_SendErrorMessageRequest>(msg);
                ZZB_SendErrorMessageResponse sendErrorMessageResponse = new ZZB_SendErrorMessageResponse();
                if (sendErrorMessageRequest == null)
                {
                    sendErrorMessageResponse.result = VAResult.VA_FAILED_MSG_ERROR;
                }
                else
                {
                    ZZBPreOrderOperate zzbPreOrderOperate = new ZZBPreOrderOperate();
                    sendErrorMessageResponse = zzbPreOrderOperate.ZZBSendErrorMessage(sendErrorMessageRequest);
                }
                responseJson = JsonOperate.JsonSerializer<ZZB_SendErrorMessageResponse>(sendErrorMessageResponse);//回复信息反序列化
                break;
            #endregion

            #region 菜品管理权限
            case (int)VAMessageType.ZZB_CLIENT_DISH_MANAGE_ROLE_REQUEST:
                ZZB_DishManageRoleRequest dishManageRoleRequest = JsonOperate.JsonDeserialize<ZZB_DishManageRoleRequest>(msg);

                if (dishManageRoleRequest == null)
                {
                    if ((dishManageRoleRequest.serviceType != null && dishManageRoleRequest.clientBuild != null) &&
                        ((dishManageRoleRequest.serviceType == VAServiceType.ANDROID &&
                          string.Compare(dishManageRoleRequest.clientBuild, "2.0") > 0) ||
                         dishManageRoleRequest.serviceType == VAServiceType.IPHONE &&
                         string.Compare(dishManageRoleRequest.clientBuild, "1.3") > 0))
                    {
                        ZZB_DishManageRoleResponse dishManageRoleResponse = new ZZB_DishManageRoleResponse();
                        dishManageRoleResponse.result = VAResult.VA_FAILED_MSG_ERROR;
                        responseJson = JsonOperate.JsonSerializer<ZZB_DishManageRoleResponse>(dishManageRoleResponse);
                    }
                    else
                    {
                        ZZB_DishManageRoleResponseOld dishManageRoleResponseOld = new ZZB_DishManageRoleResponseOld();
                        dishManageRoleResponseOld.result = VAResult.VA_FAILED_MSG_ERROR;
                        responseJson = JsonOperate.JsonSerializer<ZZB_DishManageRoleResponseOld>(dishManageRoleResponseOld);
                    }
                }
                else
                {
                    if ((dishManageRoleRequest.serviceType != null && dishManageRoleRequest.clientBuild != null) &&
                        ((dishManageRoleRequest.serviceType == VAServiceType.ANDROID &&
                          string.Compare(dishManageRoleRequest.clientBuild, "2.0") > 0) ||
                         dishManageRoleRequest.serviceType == VAServiceType.IPHONE &&
                         string.Compare(dishManageRoleRequest.clientBuild, "1.3") > 0))
                    {
                        ZZB_DishManageRoleResponse dishManageRoleResponse = new ZZB_DishManageRoleResponse();

                        ZZBPreOrderOperate zzbPreOrderOperate = new ZZBPreOrderOperate();
                        dishManageRoleResponse = zzbPreOrderOperate.ZZBDishManageRole(dishManageRoleRequest);
                        #region ---------------------------城市抽奖开关未开启 白名单里面的店铺可以抽奖
                        if (dishManageRoleResponse.roles != null)
                        {
                            LotteryOperate lotteryOperate = new LotteryOperate();
                            bool isLottery = lotteryOperate.IsLotterySwitchOpen(dishManageRoleRequest.cityId, dishManageRoleRequest.shopId);
                            var authorityCodes = dishManageRoleResponse.roles.Find(d => d.authorityCode == Convert.ToInt32(ShopRole.抽奖设置).ToString());
                            if (isLottery && authorityCodes == null)
                            {
                                var objShopAuthority = new ShopAuthority();
                                RoleAuthorityOperate operateRoleAuthority = new VAGastronomistMobileApp.WebPageDll.RoleAuthorityOperate();
                                objShopAuthority = operateRoleAuthority.GetShopAuthorityByCode(Convert.ToString((int)ShopRole.抽奖设置));
                                var shopHaveAuthority = new ShopHaveAuthority()
                                {
                                    authorityCode = objShopAuthority.AuthorityCode,
                                    authorityName = objShopAuthority.ShopAuthorityName,
                                    isClientShow = objShopAuthority.IsClientShow
                                };
                                dishManageRoleResponse.roles.Add(shopHaveAuthority);
                            }
                        }
                        #endregion
                        responseJson = JsonOperate.JsonSerializer<ZZB_DishManageRoleResponse>(dishManageRoleResponse);
                        //回复信息反序列化 
                    }
                    else
                    {
                        ZZB_DishManageRoleResponseOld dishManageRoleResponseOld = new ZZB_DishManageRoleResponseOld();
                        //if (dishManageRoleRequest == null)
                        //{
                        //    dishManageRoleResponseOld.result = VAResult.VA_FAILED_MSG_ERROR;
                        //}

                        ZZBPreOrderOperate zzbPreOrderOperate = new ZZBPreOrderOperate();
                        dishManageRoleResponseOld = zzbPreOrderOperate.ZZBDishManageRoleOld(dishManageRoleRequest);

                        responseJson = JsonOperate.JsonSerializer<ZZB_DishManageRoleResponseOld>(dishManageRoleResponseOld);
                    }
                }
                break;
            #endregion

            #region 权限判断---------------------------------------------------------------
            case (int)VAMessageType.ZZB_CLIENT_ROLE_CHECK_REQUEST:
                ZZB_RoleCheckRequest roleCheckRequest = JsonOperate.JsonDeserialize<ZZB_RoleCheckRequest>(msg);
                ZZB_RoleCheckResponse roleCheckResponse = new ZZB_RoleCheckResponse();
                if (roleCheckRequest == null)
                {
                    roleCheckRequest.result = VAResult.VA_FAILED_MSG_ERROR;
                }
                else
                {
                    ZZBPreOrderOperate zzbPreOrderOperate = new ZZBPreOrderOperate();
                    roleCheckResponse = zzbPreOrderOperate.RoleCheck(roleCheckRequest);
                }
                responseJson = JsonOperate.JsonSerializer<ZZB_RoleCheckResponse>(roleCheckResponse);//回复信息反序列化
                break;
            #endregion

            #region 悠先服务客户端菜品查询
            case (int)VAMessageType.ZZB_CLIENT_DISH_SEARCH_REQUEST:
                ZZB_DishSearchRequest dishSearchRequest = JsonOperate.JsonDeserialize<ZZB_DishSearchRequest>(msg);
                ZZB_DishSearchResponse dishSearchResponse = new ZZB_DishSearchResponse();
                if (dishSearchRequest == null)
                {
                    dishSearchResponse.result = VAResult.VA_FAILED_MSG_ERROR;
                }
                else
                {
                    ZZBPreOrderOperate zzbPreOrderOperate = new ZZBPreOrderOperate();
                    dishSearchResponse = zzbPreOrderOperate.DishSearch(dishSearchRequest);
                }
                responseJson = JsonOperate.JsonSerializer<ZZB_DishSearchResponse>(dishSearchResponse);//回复信息反序列化
                break;
            #endregion

            #region 悠先服务客户端菜品查询(新)
            case (int)VAMessageType.ZZB_CLIENT_DISH_ALL_SEARCH_REQUEST:
                ZZB_DishAllSearchRequest dishAllSearchRequest = JsonOperate.JsonDeserialize<ZZB_DishAllSearchRequest>(msg);
                ZZB_DishSearchResponse dishSearch2Response = new ZZB_DishSearchResponse();
                if (dishAllSearchRequest == null)
                {
                    dishSearch2Response.result = VAResult.VA_FAILED_MSG_ERROR;
                }
                else
                {
                    ZZBPreOrderOperate zzbPreOrderOperate = new ZZBPreOrderOperate();

                    dishSearch2Response = zzbPreOrderOperate.DishSearch(dishAllSearchRequest);
                }
                responseJson = JsonOperate.JsonSerializer<ZZB_DishSearchResponse>(dishSearch2Response);//回复信息反序列化
                break;
            #endregion

            #region 悠先服务客户端菜沽清
            case (int)VAMessageType.ZZB_CLIENT_DISH_SELLOFF_REQUEST:
                ZZB_DishSellOffRequest dishSellOffRequest = JsonOperate.JsonDeserialize<ZZB_DishSellOffRequest>(msg);
                ZZB_DishSellOffResponse dishSellOffResponse = new ZZB_DishSellOffResponse();
                if (dishSellOffRequest == null)
                {
                    dishSellOffResponse.result = VAResult.VA_FAILED_MSG_ERROR;
                }
                else
                {
                    ZZBPreOrderOperate zzbPreOrderOperate = new ZZBPreOrderOperate();
                    dishSellOffResponse = zzbPreOrderOperate.SellOff(dishSellOffRequest);
                }
                responseJson = JsonOperate.JsonSerializer<ZZB_DishSellOffResponse>(dishSellOffResponse);//回复信息反序列化
                break;
            #endregion

            #region 悠先服务客户端push token更新
            case (int)VAMessageType.ZZB_CLIENT_PUSHTOKEN_UPDATE_REQUEST:
                ZZB_PushTokenUpdateRequest pushTokenUpdateRequest =
                    JsonOperate.JsonDeserialize<ZZB_PushTokenUpdateRequest>(msg);
                ZZB_PushTokenUpdateResponse pushTokenUpdateResponse = new ZZB_PushTokenUpdateResponse();
                if (pushTokenUpdateRequest == null)
                {
                    pushTokenUpdateResponse.result = VAResult.VA_FAILED_MSG_ERROR;
                }
                else
                {
                    ZZBPreOrderOperate zzbPreOrderOperate = new ZZBPreOrderOperate();
                    pushTokenUpdateResponse = zzbPreOrderOperate.PushTokenUpdate(pushTokenUpdateRequest);
                }
                responseJson = JsonOperate.JsonSerializer<ZZB_PushTokenUpdateResponse>(pushTokenUpdateResponse);//回复信息反序列化
                break;

            #endregion

            #region 悠先服务客户端菜品价格修改
            case (int)VAMessageType.ZZB_CLIENT_DISH_PRICE_MODIFY_REQUEST:
                ZZB_ClientDishPriceModifyRequest clientDishPriceModifyRequest = JsonOperate.JsonDeserialize<ZZB_ClientDishPriceModifyRequest>(msg);
                ZZB_ClientDishPriceModifyResponse clientDishPriceModifyResponse = new ZZB_ClientDishPriceModifyResponse();
                if (clientDishPriceModifyRequest == null)
                {
                    clientDishPriceModifyResponse.result = VAResult.VA_FAILED_MSG_ERROR;
                }
                else
                {
                    ZZBPreOrderOperate zzbPreOrderOperate = new ZZBPreOrderOperate();
                    clientDishPriceModifyResponse = zzbPreOrderOperate.DishPriceModify(clientDishPriceModifyRequest);
                }
                responseJson = JsonOperate.JsonSerializer<ZZB_ClientDishPriceModifyResponse>(clientDishPriceModifyResponse);//回复信息反序列化
                break;
            #endregion

            #region 悠先服务菜谱更新状态
            case (int)VAMessageType.ZZB_CLIENT_TASK_CHECK_STATUS_REQUEST:
                ZZB_ClientTaskCheckStatusRequest clientTaskCheckStatusRequest = JsonOperate.JsonDeserialize<ZZB_ClientTaskCheckStatusRequest>(msg);
                ZZB_ClientTaskCheckStatusResponse clientTaskCheckStatusResponse = new ZZB_ClientTaskCheckStatusResponse();
                if (clientTaskCheckStatusRequest != null)
                {
                    ZZBPreOrderOperate zzbPreOrderOperate = new ZZBPreOrderOperate();
                    clientTaskCheckStatusResponse = zzbPreOrderOperate.CheckTaskStatus(clientTaskCheckStatusRequest);
                }
                else
                {
                    clientTaskCheckStatusResponse.result = VAResult.VA_FAILED_MSG_ERROR;
                }
                responseJson = JsonOperate.JsonSerializer<ZZB_ClientTaskCheckStatusResponse>(clientTaskCheckStatusResponse);//回复信息反序列化
                break;

            #endregion

            #region 悠先服务客户追溯模块 add by wangc
            //客户追溯用户信息
            case (int)VAMessageType.ZZB_CLIENT_RETROSPECT_CUSTOMER_REQUEST:
                ZZB_ClientRetrospectCustomerRequest clientRetrospectCustomerRequest = JsonOperate.JsonDeserialize<ZZB_ClientRetrospectCustomerRequest>(msg);
                ZZB_ClientRetrospectCustomerResponse clientRetrospectCustomerResponse = new ZZB_ClientRetrospectCustomerResponse();
                if (clientRetrospectCustomerRequest != null)
                {
                    ZZBPreOrderOperate operate = new ZZBPreOrderOperate();
                    clientRetrospectCustomerResponse = operate.ClientRetrospectCustomer(clientRetrospectCustomerRequest);
                }
                else
                {
                    clientRetrospectCustomerResponse.result = VAResult.VA_FAILED_MSG_ERROR;
                }
                responseJson = JsonOperate.JsonSerializer<ZZB_ClientRetrospectCustomerResponse>(clientRetrospectCustomerResponse);
                break;
            //查看追溯到当前用户的历史点单列表
            case (int)VAMessageType.ZZB_CLIENT_RETROSPECT_CUSTOMER_ORDER_REQUEST:
                ZZB_ClientRetrospectCustomerOrderRequest clientRetrospectCustomerOrderRequest = JsonOperate.JsonDeserialize<ZZB_ClientRetrospectCustomerOrderRequest>(msg);
                ZZB_ClientRetrospectCustomerOrderResponse clientRetrospectCustomerOrderResponse = new ZZB_ClientRetrospectCustomerOrderResponse();
                if (clientRetrospectCustomerOrderRequest != null)
                {
                    ZZBPreOrderOperate operate = new ZZBPreOrderOperate();
                    clientRetrospectCustomerOrderResponse = operate.ClientRetrospectCustomerOrderList(clientRetrospectCustomerOrderRequest);
                }
                else
                {
                    clientRetrospectCustomerOrderResponse.result = VAResult.VA_FAILED_MSG_ERROR;
                }
                responseJson = JsonOperate.JsonSerializer<ZZB_ClientRetrospectCustomerOrderResponse>(clientRetrospectCustomerOrderResponse);
                break;
            //查询追溯到用户某个历史点单的详情内容
            case (int)VAMessageType.ZZB_CLIENT_RETROSPECT_CUSTOMER_ORDERDETAIL_REQUEST:
                ZZB_ClientRetrospectCustomerOrderDetailRequest clientRetrospectCustomerOrderDetailRequest = JsonOperate.JsonDeserialize<ZZB_ClientRetrospectCustomerOrderDetailRequest>(msg);
                ZZB_ClientRetrospectCustomerOrderDetailResponse clientRetrospectCustomerOrderDetailResponse = new ZZB_ClientRetrospectCustomerOrderDetailResponse();
                if (clientRetrospectCustomerOrderDetailRequest != null)
                {
                    ZZBPreOrderOperate operate = new ZZBPreOrderOperate();
                    clientRetrospectCustomerOrderDetailResponse = operate.ClientRetrospectCustomerOrderDetail(clientRetrospectCustomerOrderDetailRequest);
                }
                else
                {
                    clientRetrospectCustomerOrderDetailResponse.result = VAResult.VA_FAILED_MSG_ERROR;
                }
                responseJson = JsonOperate.JsonSerializer<ZZB_ClientRetrospectCustomerOrderDetailResponse>(clientRetrospectCustomerOrderDetailResponse);
                break;
            //调整追溯到用户的余额
            case (int)VAMessageType.ZZB_CLIENT_RETROSPECT_CUSTOMER_CHANGEBALANCE_REQUEST:
                ZZB_ClientRetrospectCustomerChangeBalanceRequest clientRetrospectCustomerChangeBalanceRequest = JsonOperate.JsonDeserialize<ZZB_ClientRetrospectCustomerChangeBalanceRequest>(msg);
                ZZB_ClientRetrospectCustomerChangeBalanceResponse clientRetrospectCustomerChangeBalanceResponse = new ZZB_ClientRetrospectCustomerChangeBalanceResponse();
                if (clientRetrospectCustomerChangeBalanceRequest != null)
                {
                    ZZBPreOrderOperate operate = new ZZBPreOrderOperate();
                    clientRetrospectCustomerChangeBalanceResponse = operate.ClientRetrospectCustomerChangeBalance(clientRetrospectCustomerChangeBalanceRequest);
                }
                else
                {
                    clientRetrospectCustomerChangeBalanceResponse.result = VAResult.VA_FAILED_MSG_ERROR;
                }
                responseJson = JsonOperate.JsonSerializer<ZZB_ClientRetrospectCustomerChangeBalanceResponse>(clientRetrospectCustomerChangeBalanceResponse);//回复信息反序列化
                break;
            //悠先服务查看追溯用户红包领用详情
            //case (int)VAMessageType.ZZB_CLIENT_CHECK_REDENVELOPE_DETAIL_REQUEST:
            //    ZZB_ClientCheckRedEnvelopeDetailsRequest zzb_ClientCheckRedEnvelopeDetailsRequest = JsonOperate.JsonDeserialize<ZZB_ClientCheckRedEnvelopeDetailsRequest>(msg);
            //    ZZB_ClientCheckRedEnvelopeDetailsResponse zzb_ClientCheckRedEnvelopeDetailsResponse = new ZZB_ClientCheckRedEnvelopeDetailsResponse();
            //    if (zzb_ClientCheckRedEnvelopeDetailsRequest != null)
            //    {
            //        RedEnvelopeDetailOperate operate = new RedEnvelopeDetailOperate();
            //        zzb_ClientCheckRedEnvelopeDetailsResponse = operate.ZZB_ClientCheckRedEnvelopeDetails(zzb_ClientCheckRedEnvelopeDetailsRequest);
            //    }
            //    else
            //    {
            //        zzb_ClientCheckRedEnvelopeDetailsResponse.result = VAResult.VA_FAILED_MSG_ERROR;
            //    }
            //    responseJson = JsonOperate.JsonSerializer<ZZB_ClientCheckRedEnvelopeDetailsResponse>(zzb_ClientCheckRedEnvelopeDetailsResponse);
            //    break;
            #endregion

            #region 悠先服务门店查询门店会员数详情
            case (int)VAMessageType.ZZB_CLIENT_CHECK_SHOP_VIPUSERSINFO_REQUEST:
                ZZB_ClientCheckShopVipUsersInfoRequest clientCheckShopVipUsersInfoRequest = JsonOperate.JsonDeserialize<ZZB_ClientCheckShopVipUsersInfoRequest>(msg);
                ZZB_ClientCheckShopVipUsersInfoResponse clientCheckShopVipUsersInfoResponse = new ZZB_ClientCheckShopVipUsersInfoResponse();
                if (clientCheckShopVipUsersInfoRequest != null)
                {
                    ZZBPreOrderOperate operate = new ZZBPreOrderOperate();
                    clientCheckShopVipUsersInfoResponse = operate.ClientCheckShopVipUsersInfo(clientCheckShopVipUsersInfoRequest);
                }
                else
                {
                    clientCheckShopVipUsersInfoResponse.result = VAResult.VA_FAILED_MSG_ERROR;
                }
                responseJson = JsonOperate.JsonSerializer<ZZB_ClientCheckShopVipUsersInfoResponse>(clientCheckShopVipUsersInfoResponse);//回复信息反序列化
                break;
            #endregion

            #region  悠先服务客户信息(新)
            case (int)VAMessageType.ZZB_CLINET_CUSTOMER_DETAILS_REQUEST:
                ZZB_CustomerDetailsRequest customerDetailsRequest = JsonOperate.JsonDeserialize<ZZB_CustomerDetailsRequest>(msg);
                if (customerDetailsRequest == null)
                {
                    VANetworkMessage message = new VANetworkMessage()
                    {
                        result = VAResult.VA_FAILED_MSG_ERROR
                    };
                    responseJson = JsonOperate.JsonSerializer<VANetworkMessage>(message);
                }
                else
                {
                    ZZBPreOrderOperate operate = new ZZBPreOrderOperate();
                    if (customerDetailsRequest.pageNubmer <= 1)
                    {
                        ZZB_CustomerDetailsResponse customerDetailsResponse = operate.GetCustomerDetails(customerDetailsRequest);
                        responseJson = JsonOperate.JsonSerializer<ZZB_CustomerDetailsResponse>(customerDetailsResponse);
                    }
                    else
                    {
                        ZZB_CustomerPreOrdersResponse customerPreOrdersResponse = operate.GetCustomerPreOrders(customerDetailsRequest);
                        responseJson = JsonOperate.JsonSerializer<ZZB_CustomerPreOrdersResponse>(customerPreOrdersResponse);
                    }
                }
                break;
            #endregion

            #region 悠先服务点单列表(新)
            case (int)VAMessageType.ZZB_CLIENT_PREORDERLIST2_REQUEST:
                ZZB_VAPreOrderListRequest preOrderListRequest = JsonOperate.JsonDeserialize<ZZB_VAPreOrderListRequest>(msg);
                ZZB_PreOrderList2Response preOrderList2Response = new ZZB_PreOrderList2Response();
                if (preOrderListRequest == null)
                {
                    preOrderList2Response.result = VAResult.VA_FAILED_MSG_ERROR;
                }
                else
                {
                    ZZBPreOrderOperate operate = new ZZBPreOrderOperate();
                    preOrderList2Response = operate.ZZBClientQueryPreOrderList2(preOrderListRequest);
                }
                responseJson = JsonOperate.JsonSerializer<ZZB_PreOrderList2Response>(preOrderList2Response);
                break;
            #endregion

            #region 悠先服务点单列表（拆分版）
            case (int)VAMessageType.ZZB_CLIENT_PREORDERLIST_REQUEST:
                ZZB_VAPreOrderListRequest preOrderListRequest3 = JsonOperate.JsonDeserialize<ZZB_VAPreOrderListRequest>(msg);
                ZZB_PreOrderListResponse preOrderListResponse = new ZZB_PreOrderListResponse();
                if (preOrderListRequest3 == null)
                {
                    preOrderListResponse.result = VAResult.VA_FAILED_MSG_ERROR;
                }
                else
                {
                    ZZBPreOrderOperate operate = new ZZBPreOrderOperate();
                    preOrderListResponse = operate.ZZBClientQueryPreOrderList(preOrderListRequest3);
                }
                responseJson = JsonOperate.JsonSerializer<ZZB_PreOrderListResponse>(preOrderListResponse);
                break;
            #endregion

            #region 悠先服务点单列表附加信息（拆分版）
            case (int)VAMessageType.ZZB_CLIENT_PREORDERLIST_ATTACG_REQUEST:
                ZZB_PreOrderListAttachRequest preOrderListAttachRequest = JsonOperate.JsonDeserialize<ZZB_PreOrderListAttachRequest>(msg);
                ZZB_PreOrderListAttachResponse preOrderListAttachResponse = new ZZB_PreOrderListAttachResponse();
                if (preOrderListAttachRequest == null)
                {
                    preOrderListAttachResponse.result = VAResult.VA_FAILED_MSG_ERROR;
                }
                else
                {
                    ZZBPreOrderOperate operate = new ZZBPreOrderOperate();
                    preOrderListAttachResponse = operate.ZZBClientQueryPreOrderListAttach(preOrderListAttachRequest);
                }
                responseJson = JsonOperate.JsonSerializer<ZZB_PreOrderListAttachResponse>(preOrderListAttachResponse);
                break;
            #endregion

            #region 悠先服务查询点单评价列表
            case (int)VAMessageType.ZZB_CLIENT_SHOP_PREORDERLIST_REQUEST:
                ZZBClientShopPreorderListRequest zzbClientShopPreorderListRequest = JsonOperate.JsonDeserialize<ZZBClientShopPreorderListRequest>(msg);
                ZZBClientShopPreorderListResponse zzbClientShopPreorderListResponse = new ZZBClientShopPreorderListResponse();
                if (zzbClientShopPreorderListRequest == null)
                {
                    zzbClientShopPreorderListRequest.result = VAResult.VA_FAILED_MSG_ERROR;
                }
                else
                {
                    ZZBPreOrderOperate operate = new ZZBPreOrderOperate();
                    zzbClientShopPreorderListResponse = operate.ZZBQueryShopEvaluationList(zzbClientShopPreorderListRequest);
                }
                responseJson = JsonOperate.JsonSerializer<ZZBClientShopPreorderListResponse>(zzbClientShopPreorderListResponse);
                break;
            #endregion

            #region 悠先服务修改订单桌号(新)
            case (int)VAMessageType.ZZB_CLINET_MODIFY_DESK_NUMBER_REQUEST:
                ZZB_ModifyDeskNumberRequest modifyDeskNumberRequest = JsonOperate.JsonDeserialize<ZZB_ModifyDeskNumberRequest>(msg);
                ZZB_ModifyDeskNumberResponse modifyDeskNumberResponse = new ZZB_ModifyDeskNumberResponse();
                if (modifyDeskNumberRequest == null)
                {
                    modifyDeskNumberResponse.result = VAResult.VA_FAILED_MSG_ERROR;
                }
                else
                {
                    ZZBPreOrderOperate operate = new ZZBPreOrderOperate();
                    modifyDeskNumberResponse = operate.ModifyDeskNumber(modifyDeskNumberRequest);
                }
                responseJson = JsonOperate.JsonSerializer<ZZB_ModifyDeskNumberResponse>(modifyDeskNumberResponse);
                break;
            #endregion

            #region 悠先服务配菜模块
            case (int)VAMessageType.ZZB_CHECK_DISHINGREDIENTS_REQUEST://配菜查询
                ZZBCheckDishIngredientsRequest request_1321 = JsonOperate.JsonDeserialize<ZZBCheckDishIngredientsRequest>(msg);
                ZZBCheckDishIngredientsResponse response_1322 = new ZZBCheckDishIngredientsResponse();
                if (request_1321 == null)
                {
                    response_1322.result = VAResult.VA_FAILED_MSG_ERROR;
                }
                else
                {
                    response_1322 = new CurrectIngredientsSellOffInfoOperate().ZZBCheckDishIngredients(request_1321);
                }
                responseJson = JsonOperate.JsonSerializer<ZZBCheckDishIngredientsResponse>(response_1322);
                break;
            case (int)VAMessageType.ZZB_SELLOFF_DISHINGREDIENTS_REQUEST://配菜沽清
                ZZBSellOffDishIngredientsRequest request_1323 = JsonOperate.JsonDeserialize<ZZBSellOffDishIngredientsRequest>(msg);
                ZZBSellOffDishIngredientsResponse response_1324 = new ZZBSellOffDishIngredientsResponse();
                if (request_1323 == null)
                {
                    response_1324.result = VAResult.VA_FAILED_MSG_ERROR;
                }
                else
                {
                    response_1324 = new CurrectIngredientsSellOffInfoOperate().ZZBSellOffDishIngredients(request_1323);
                }
                responseJson = JsonOperate.JsonSerializer<ZZBSellOffDishIngredientsResponse>(response_1324);
                break;
            #endregion
            #endregion


            default:
                responseJson = "兄弟,干嘛呢!来一趟啥都不干有意思吗?小心哥弄你!";
                break;
        }

        if (String.Compare(Common.debugFlag, "true", StringComparison.OrdinalIgnoreCase) == 0)
        {
            // 添加接口调试记录日志,调用log4net add by zhujinlei 2015/05/30
            Log4netManager.WriteLog("response:" + responseJson + "-requestTime:" + requestTime.ToString("yyyy/MM/dd HH:mm:ss:fff"));
            // 日志记入数据库
            Log4netManager.WriteLogDB("response", type, responseJson, requestTime, requestGuid);
            ////写日志文件开启
            //using (St`reamWriter file = new StreamWriter(@filePath, true))
            //{
            //    file.WriteLine(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + ":-response:" + responseJson + requestTime.ToString("yyyy/MM/dd HH:mm:ss:fff"));
            //}
        }
        if (String.Compare(Common.timeOutDebugFlag, "true", StringComparison.OrdinalIgnoreCase) == 0)
        {
            DateTime responseTime = DateTime.Now;
            TimeSpan timeSpan = responseTime - requestTime;
            if (timeSpan.Seconds >= 1)
            {
                Log4netManager.WriteLogTimeOut("requestTime：" + requestTime.ToString("yyyy/MM/dd HH:mm:ss:fff") + "-type:" + type + "-msg:" + msg);
                Log4netManager.WriteLogTimeOut("responseTime：" + responseTime.ToString("yyyy/MM/dd HH:mm:ss:fff") + "-response:" + responseJson);
                // 日志记录入数据库中
                Log4netManager.WriteLogDB("TimeOutRequest", type, msg, requestTime, requestGuid);
                Log4netManager.WriteLogDB("TimeOutResponse", type, responseJson, requestTime, requestGuid);
                //string currentDate = System.DateTime.Now.Date.ToString("yyyyMMdd");//当前日期
                //string TimeOutFilePath = Server.MapPath("~/Logs/timeOutLog" + currentDate + ".txt");//log路径

                //using (StreamWriter file = new StreamWriter(@TimeOutFilePath, true))
                //{
                //    file.WriteLine(requestTime.ToString("yyyy/MM/dd HH:mm:ss:fff") + ":type:" + type + "- msg:" + msg);
                //    file.WriteLine(responseTime.ToString("yyyy/MM/dd HH:mm:ss:fff") + ":-response:" + responseJson);
                //}
            }
        }
        Response.Write(responseJson);//返回字符串
        Response.End();
    }
}