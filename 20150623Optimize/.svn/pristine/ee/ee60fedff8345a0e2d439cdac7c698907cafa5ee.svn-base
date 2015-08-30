using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using VAGastronomistMobileApp.SQLServerDAL;
using VAGastronomistMobileApp.Model;
using System.IO;
using System.Web;

namespace VAGastronomistMobileApp.WebPageDll
{
    /// <summary>
    /// 微信点菜业务逻辑层
    /// add by wangc 
    /// 20140317
    /// </summary>
    public class WechatOrderOperate
    {
        /// <summary>
        /// 微信登录进入点菜页面
        /// </summary>
        /// <param name="shopCookie"></param>
        /// <param name="cityId"></param>
        /// <param name="userCookie"></param>
        /// <param name="userUuid"></param>
        /// <returns></returns>
        public string WechatLogin(string shopCookie, int cityId, string userCookie, string userUuid)
        {
            SybMsg resultMesage = new SybMsg();
            if (!string.IsNullOrEmpty(shopCookie) && cityId > 0)
            {
                CustomerOperate customerOper = new CustomerOperate();
                if (!string.IsNullOrEmpty(userCookie) && !string.IsNullOrEmpty(userUuid))
                {
                    //用户Cookie登录
                    VAClientCookieLoginRequest cookieLoginRequest = new VAClientCookieLoginRequest();
                    cookieLoginRequest.cityId = cityId;
                    cookieLoginRequest.cookie = userCookie;
                    cookieLoginRequest.uuid = userUuid;
                    cookieLoginRequest.type = VAMessageType.CLIENT_COOKIE_LOGIN_REQUEST;
                    VAClientCookieLoginResponse cookieLoginReponse = customerOper.ClientCookieLogin(cookieLoginRequest);
                    if (cookieLoginReponse.result == VAResult.VA_OK)
                    {
                        if (!string.IsNullOrEmpty(cookieLoginReponse.userInfo.mobilePhone))
                        {
                            WechatOrderManager wechatMan = new WechatOrderManager();
                            DataTable dtShopWechatOrder = wechatMan.SelectShopWechatOrderConfigByCookie(shopCookie);
                            int shopId = Common.ToInt32(dtShopWechatOrder.Rows[0]["shopId"]);
                            if (shopId > 0)
                            {
                                //登录成功，存在手机号码，显示点菜页面
                                ClientIndexListOperate orderDishOper = new ClientIndexListOperate();
                                VAClientSearchShopListRequest orderDishInfoRequest = new VAClientSearchShopListRequest();
                                orderDishInfoRequest.cityId = cityId;
                                orderDishInfoRequest.cookie = userCookie;
                                orderDishInfoRequest.pageSize = 1;
                                orderDishInfoRequest.searchKeyWord = "";
                                orderDishInfoRequest.shopId = shopId;
                                orderDishInfoRequest.cookie = userCookie;
                                orderDishInfoRequest.uuid = userUuid;
                                orderDishInfoRequest.type = VAMessageType.CLIENT_SEARCH_SHOP_LIST_REQUEST;
                                VAClientSearchShopListReponse orderDishInfoReponse = orderDishOper.ClientSearchShopList(orderDishInfoRequest);
                                if (orderDishInfoReponse.result == VAResult.VA_OK)
                                {
                                    resultMesage.Insert(1, JsonOperate.JsonSerializer<VAClientSearchShopListReponse>(orderDishInfoReponse));
                                }
                                else
                                {
                                    resultMesage.Insert(-3, "获取门店点菜基本信息失败");
                                }
                            }
                            else
                            {
                                resultMesage.Insert(-1, "点菜链接无效");//当前门店不支持点菜
                            }
                        }
                        else
                        {
                            //手机验证页面
                            resultMesage.Insert(2, "前端跳转手机验证页面");
                        }
                    }
                    else
                    {
                        resultMesage.Insert(2, "前端跳转手机验证页面");//Cookie登录失败
                    }
                }
                else
                {
                    VAClientRegisterRequest clientRegisterRequest = new VAClientRegisterRequest();
                    clientRegisterRequest.appType = VAAppType.WAP;
                    clientRegisterRequest.cityId = cityId;
                    clientRegisterRequest.cookie = userCookie;
                    clientRegisterRequest.type = VAMessageType.CLIENT_REGISTER_REQUEST;
                    clientRegisterRequest.uuid = userUuid;
                    VAClientRegisterResponse clientRegisterReponse = customerOper.ClientRegister(clientRegisterRequest);
                    resultMesage.Insert(3, JsonOperate.JsonSerializer<VAClientRegisterResponse>(clientRegisterReponse));//跳转手机验证页面，前端需要把当前userCookie,userUuid存在本地
                }
            }
            else
            {
                resultMesage.Insert(-1, "点菜链接无效");
            }
            return resultMesage.Value;
        }
        /// <summary>
        /// 微信用户手机验证
        /// </summary>
        /// <param name="shopCookie"></param>
        /// <param name="cityId"></param>
        /// <param name="userCookie"></param>
        /// <param name="userUuid"></param>
        /// <param name="mobilePhoneNumber"></param>
        /// <param name="verificationCode"></param>
        /// <returns></returns>
        public string WechatRegister(string shopCookie, int cityId, string userCookie, string userUuid, string mobilePhoneNumber, string verificationCode)
        {
            //DateTime requestTime = System.DateTime.Now;
            //string filePath = HttpContext.Current.Server.MapPath("~/Logs/wechatLog.txt");
            SybMsg message = new SybMsg();
            //using (StreamWriter file = new StreamWriter(@filePath, true))
            //{
            //    file.WriteLine("门店Cookie：" + shopCookie + "城市编号：" + cityId.ToString() + "门店Cookie" + userCookie + "用户userUuid" + userUuid + "手机号码：" + mobilePhoneNumber + "验证码：" + verificationCode);
            //}
            if (!string.IsNullOrEmpty(shopCookie) && cityId > 0 && !string.IsNullOrEmpty(userCookie) && !string.IsNullOrEmpty(userUuid) && !string.IsNullOrEmpty(mobilePhoneNumber))
            {
                //using (StreamWriter file = new StreamWriter(@filePath, true))
                //{
                //    file.WriteLine(requestTime.ToString("yyyy/MM/dd HH:mm:ss:fff") + ":" + "1111");
                //}
                WechatOrderManager wechatMan = new WechatOrderManager();
                DataTable dtShopWechatOrder = wechatMan.SelectShopWechatOrderConfigByCookie(shopCookie);
                if (dtShopWechatOrder.Rows.Count > 0)
                {
                    //using (StreamWriter file = new StreamWriter(@filePath, true))
                    //{
                    //    file.WriteLine(requestTime.ToString("yyyy/MM/dd HH:mm:ss:fff") + ":" + "1111");
                    //}
                    int shopId = Common.ToInt32(dtShopWechatOrder.Rows[0]["shopId"]);
                    VAClientMobileVerifyNewRequest mobileVerifyRequest = new VAClientMobileVerifyNewRequest();
                    mobileVerifyRequest.cityId = cityId;
                    mobileVerifyRequest.cookie = userCookie;
                    mobileVerifyRequest.mobilePhoneNumber = mobilePhoneNumber;
                    mobileVerifyRequest.shopId = shopId;
                    mobileVerifyRequest.type = VAMessageType.CLIENT_MOBILE_VERIFYNEW_REQUEST;
                    mobileVerifyRequest.uuid = userUuid;
                    mobileVerifyRequest.verificationCode = verificationCode;//首次调用传“”
                    CustomerOperate customerOper = new CustomerOperate();
                    VAClientMobileVerifyNewResponse mobileVerifyReponse = customerOper.ClientMobileVerifyNew(mobileVerifyRequest);
                    //using (StreamWriter file = new StreamWriter(@filePath, true))
                    //{
                    //    file.WriteLine(requestTime.ToString("yyyy/MM/dd HH:mm:ss:fff") + ":" + mobileVerifyReponse.result);
                    //}
                    if (mobileVerifyReponse.result == VAResult.VA_OK)
                    {
                        string resultJson = JsonOperate.JsonSerializer<VAClientMobileVerifyNewResponse>(mobileVerifyReponse);
                        if (!string.IsNullOrEmpty(verificationCode))//验证
                        {
                            //using (StreamWriter file = new StreamWriter(@filePath, true))
                            //{
                            //    file.WriteLine(requestTime.ToString("yyyy/MM/dd HH:mm:ss:fff") + ":1111" + mobileVerifyReponse.result);
                            //}
                            //验证成功
                            message.Insert(1, resultJson);
                        }
                        else//获取验证码
                        {
                            //using (StreamWriter file = new StreamWriter(@filePath, true))
                            //{
                            //    file.WriteLine(requestTime.ToString("yyyy/MM/dd HH:mm:ss:fff") + ":2222" + mobileVerifyReponse.result);
                            //}
                            message.Insert(2, resultJson);
                        }
                    }
                    else
                    {
                        //using (StreamWriter file = new StreamWriter(@filePath, true))
                        //{
                        //    file.WriteLine(requestTime.ToString("yyyy/MM/dd HH:mm:ss:fff") + ":shibai" + mobileVerifyReponse.result);
                        //}
                        if (!string.IsNullOrEmpty(verificationCode))
                        {
                            message.Insert(-4, "验证码不正确，验证失败");//验证失败
                        }
                        else
                        {
                            message.Insert(-1, "获取验证码失败");//获取验证码失败
                        }

                    }
                }
                else
                {
                    //using (StreamWriter file = new StreamWriter(@filePath, true))
                    //{
                    //    file.WriteLine(requestTime.ToString("yyyy/MM/dd HH:mm:ss:fff") + ":111参数错");
                    //}
                    message.Insert(-2, "当前门店不支持微信点菜");
                }
            }
            else
            {
                //using (StreamWriter file = new StreamWriter(@filePath, true))
                //{
                //    file.WriteLine(requestTime.ToString("yyyy/MM/dd HH:mm:ss:fff") + "222参数错");
                //}
                message.Insert(-3, "点菜链接非法，请求参数有误");
            }
            return message.Value;
        }
        /// <summary>
        /// 微信点菜保存订单
        /// </summary>
        /// <param name="shopCookie"></param>
        /// <param name="cityId"></param>
        /// <param name="userCookie"></param>
        /// <param name="userUuid"></param>
        /// <param name="orderJson"></param>
        /// <param name="sundryJson"></param>
        /// <param name="clientUxianPriceSum"></param>
        /// <param name="clientCalculatedSum"></param>
        /// <param name="preOrderId"></param>
        /// <returns></returns>
        public string WechatSaveOrder(string shopCookie, int cityId, string userCookie, string userUuid,
            string orderJson, string sundryJson, double clientUxianPriceSum, double clientCalculatedSum, long preOrderId)
        {
            SybMsg message = new SybMsg();
            VAClientFastPaymentOrderRequest request = new VAClientFastPaymentOrderRequest();
            WechatOrderManager wechatMan = new WechatOrderManager();
            request.orderInJson = orderJson;//选择菜品JSON
            DataTable dtShopWechatOrder = wechatMan.SelectShopWechatOrderConfigByCookie(shopCookie);
            if (dtShopWechatOrder.Rows.Count > 0)
            {
                request.shopId = Common.ToInt32(dtShopWechatOrder.Rows[0]["shopId"]);
                request.clientCalculatedSum = clientCalculatedSum;
                request.clientUxianPriceSum = clientUxianPriceSum;
                request.preOrderId = preOrderId;
                request.isAddbyList = 1;
                request.sundryList = JsonOperate.JsonDeserialize<List<VASundryInfo>>(sundryJson);
                request.preOrderPayMode = 0;
                request.boolDualPayment = false;
                request.cookie = userCookie;
                request.uuid = userUuid;
                request.type = VAMessageType.CLIENT_FAST_PAYMENT_ORDER_REQUEST;
                request.cityId = cityId;
                //ClientIndexListOperate operate = new ClientIndexListOperate();
                // VAClientFastPaymentOrderReponse reponse = operate.ClientFastPaymentOrder(request, true);
                //   if (reponse.result == VAResult.VA_WECHAT_ORDER_OK)
                // {
                //    message.Insert(1, Common.ToString(reponse.preOrderId));//返回当前生成的点单流水号
                // }
                // else
                //  {
                //     message.Insert(-1, "点单失败");
                // }
            }
            return message.Value;
        }
        /// <summary>
        /// 微信点菜查看点单详情
        /// </summary>
        /// <param name="preOrderId">点单流水号</param>
        /// <returns></returns>
        public string WechatOrderDetail(Guid preOrderId)
        {
            SybMsg message = new SybMsg();
            //PreOrder19dianOperate operate = new PreOrder19dianOperate();
            OrderManager operate = new OrderManager();
            //DataTable dtOrderInfo = operate.QueryPreOrderInfoByPreOrder19dianId(preOrderId);
            Order objOrder = operate.GetEntityById(preOrderId);
            if (objOrder!=null)
            {
                //string orderInJson = Common.ToString(dtOrderInfo.Rows[0]["orderInJson"]);//点单json
                //string sundryJson = Common.ToString(dtOrderInfo.Rows[0]["sundryJson"]);//杂项json
                //
                string returnJson = SybPreOrder.PreOrderDetail(preOrderId, 0, true);
                message.Insert(1, returnJson);
            }
            else
            {
                message.Insert(-1, "未找到点单");
            }
            return message.Value;
        }
        /// <summary>
        /// 查询沽清菜品信息
        /// </summary>
        /// <param name="shopCookie"></param>
        /// <returns></returns>
        public string GetSellOutDish(string shopCookie)
        {
            SybMsg resultMessage = new SybMsg();
            WechatOrderManager wechatMan = new WechatOrderManager();
            DataTable dtShopWechatOrder = wechatMan.SelectShopWechatOrderConfigByCookie(shopCookie);
            if (dtShopWechatOrder.Rows.Count > 0)
            {
                int shopId = Common.ToInt32(dtShopWechatOrder.Rows[0]["shopId"]);
                DishManager dishMan = new DishManager();
                List<int> dishPriceList = dishMan.SelectCurrentSellOffInfoByShopId(shopId);
                resultMessage.Insert(1, JsonOperate.JsonSerializer<List<int>>(dishPriceList));
            }
            else
            {
                resultMessage.Insert(-1, "请求失败");
            }
            return resultMessage.Value;
        }
    }
}
