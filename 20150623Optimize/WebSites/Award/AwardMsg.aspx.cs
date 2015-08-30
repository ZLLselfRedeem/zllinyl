﻿using Org.BouncyCastle.Utilities.Encoders;
using PagedList;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using VA.CacheLogic;
using VA.CacheLogic.OrderClient;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.Model.Interface;
using VAGastronomistMobileApp.Model.QueryObject;
using VAGastronomistMobileApp.SQLServerDAL;
using VAGastronomistMobileApp.SQLServerDAL.Persistence;
using VAGastronomistMobileApp.WebPageDll;
using VAGastronomistMobileApp.WebPageDll.Services.Infrastructure;
using VAGastronomistMobileApp.SQLServerDAL.Persistence.Infrastructure;
using LogDll;
public partial class Award_AwardMsg : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        #region

        //if (!IsPostBack)
        //{
        //    if (!string.IsNullOrEmpty(Request.QueryString["action"]))
        //    {
        //        try
        //        {
        //            string action = Request.QueryString["action"].ToString();
        //            string result = "";
        //            switch (action)
        //            {
        //                case "GetShopAwardNoticeDetail":
        //                    int shopId = Common.ToInt32(Request.QueryString["s"]);
        //                    int c = Common.ToInt32(Request.QueryString["c"]);
        //                    result = GetShopAwardNoticeDetail(shopId,c);
        //                    Response.Write(result);
        //                    break;
        //                case "AddQueue":
        //                    shopId = Common.ToInt32(Request.QueryString["s"]);
        //                    int count = Common.ToInt32(Request.QueryString["count"]);
        //                    int employeeID = Common.ToInt32(Request.QueryString["e"]);
        //                    result = AddQueue(shopId, count, employeeID);
        //                    Response.Write(result);
        //                    break;
        //                case "AddDishMeau":
        //                    string dishJson = Convert.ToString(Request.QueryString["dishJson"]);
        //                    result = AddDishMeau(dishJson);
        //                    Response.Write(result);
        //                    break;
        //                case "SearchDishMeau":
        //                    shopId = Common.ToInt32(Request.QueryString["s"]);
        //                    int pageIndex = Common.ToInt32(Request.QueryString["pageIndex"]);
        //                    int pageSize = Common.ToInt32(Request.QueryString["pageSize"]);
        //                    string key = Convert.ToString(Request.QueryString["key"]);
        //                    result = SearchDishMeau(pageIndex, pageSize, key, shopId);
        //                    Response.Write(result);
        //                    break;
        //                case "SearchAwardList":
        //                    shopId = Common.ToInt32(Request.QueryString["s"]);
        //                    result = SearchAwardList(shopId);
        //                    Response.Write(result);
        //                    break;
        //                case "SearchAwardTotal":
        //                    shopId = Common.ToInt32(Request.QueryString["s"]);
        //                    pageIndex = Common.ToInt32(Request.QueryString["pageIndex"]);
        //                    pageSize = Common.ToInt32(Request.QueryString["pageSize"]);
        //                    result = SearchAwardTotal(pageIndex, pageSize, shopId);
        //                    Response.Write(result);
        //                    break;
        //                case "OpenDraw":
        //                    shopId = Common.ToInt32(Request.QueryString["s"]);
        //                    result = OpenDraw(shopId);
        //                    Response.Write(result);
        //                    break;
        //                case "SetAwards":
        //                    string awardJson = Convert.ToString(Request.QueryString["awardJson"]);
        //                    string oldAwardJson = Convert.ToString(Request.QueryString["oldAwardJson"]);
        //                    shopId = Common.ToInt32(Request.QueryString["s"]);
        //                    employeeID = Common.ToInt32(Request.QueryString["e"]);
        //                    result = SetAwards(awardJson, oldAwardJson, shopId, employeeID);
        //                    Response.Write(result);
        //                    break;
        //                case "GetAwardDetail":
        //                    long preOrder19DianId = Convert.ToInt64(Request.QueryString["preOrder19DianId"]);
        //                    shopId = Common.ToInt32(Request.QueryString["s"]);
        //                    result = GetAwardDetail(preOrder19DianId, shopId);
        //                    Response.Write(result);
        //                    break;
        //                case "GetAwardPicTable":
        //                    shopId = Common.ToInt32(Request.QueryString["s"]);
        //                    result = GetAwardPicTable(shopId);
        //                    Response.Write(result);
        //                    break;
        //                default:
        //                    break;
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            Response.Write(ex.Message);
        //        }
        //    }
        //}
        #endregion

        //GetAwardDetail(642548,17);
        //GetShopAwardNoticeDetail(3924,87);
    }

    #region 悠先点菜
    /// <summary>
    /// 显示门店公告的h5接口
    /// </summary>
    /// <param name="shopID"></param>
    /// <returns></returns>
    [WebMethod]
    public static string GetShopAwardNoticeDetail(int shopID, int cityID)
    {
        VAShopAwardNoticeResponse response = new VAShopAwardNoticeResponse();
        LotteryOperate lotteryOperate = new LotteryOperate();

        try
        {
            // 开关规则
            var operateAwardConfig = new AwardConfigOperate();
            // 是否开启免排队
            //var isAvoidQueue = Convert.ToBoolean(Convert.ToInt32(operateAwardConfig.GetAwardConfig("AvoidQueue").ConfigCotent));
            var isAvoidQueue = lotteryOperate.IsAvoidQueueSwitchOpen(cityID, shopID);
            response.isAvoidQueue = isAvoidQueue;//前端不用，核实后可删除

            // 是否开启抽奖功能
            //var isLottery = Convert.ToBoolean(Convert.ToInt32(operateAwardConfig.GetAwardConfig("Lottery").ConfigCotent));
            var isLottery = lotteryOperate.IsLotterySwitchOpen(cityID, shopID);

            response.isLottery = isLottery;

            //1、ShopAward、ViewAllocAward表根据shopID获取奖品列表 
            response.ListShopAwardDetail = new List<ShopAwardDetail>();
            if (isLottery)
            {
                ShopAwardOperate shopAwardOperate = new ShopAwardOperate();
                var listShopAward = shopAwardOperate.SelectShopAwardList(shopID);
                ViewAllocAwardOperate viewAllocAwardOperate = new ViewAllocAwardOperate();
                var listVAAward = viewAllocAwardOperate.SelectShopAwardList();

                ShopAwardDetail objShopAwardDetail = new ShopAwardDetail();
                bool isFirstIn = false;
                // 店家的奖品
                foreach (var shopAward in listShopAward)
                {
                    objShopAwardDetail = new ShopAwardDetail();
                    // 免排队功能未开启不需要显示
                    if (!isAvoidQueue && shopAward.AwardType == AwardType.AvoidQueue)
                    {
                        continue;
                    }
                    if (isFirstIn)
                    {
                        continue;
                    }
                    // 赠菜只需要显示一次， 这里记录第一次进入
                    if (shopAward.AwardType == AwardType.PresentDish)
                    {
                        isFirstIn = true;
                    }
                    objShopAwardDetail.ShopAwardType = shopAward.AwardType;
                    objShopAwardDetail.Name = shopAward.Name;
                    objShopAwardDetail.Content = "";// shopAward.Content;
                    response.ListShopAwardDetail.Add(objShopAwardDetail);
                }
                // 全局奖品
                foreach (var vaAward in listVAAward)
                {
                    objShopAwardDetail = new ShopAwardDetail();
                    objShopAwardDetail.ShopAwardType = vaAward.AwardType;
                    objShopAwardDetail.Name = vaAward.Name;
                    objShopAwardDetail.Content = vaAward.Content;
                    response.ListShopAwardDetail.Add(objShopAwardDetail);
                }
            }

            if (response.ListShopAwardDetail == null && response.ListShopAwardDetail.Count == 0)
            {
                response.isLottery = false;
            }

            //2、awardConfig表获取获奖规则
            if (isLottery)
            {
                response.AwardRule = operateAwardConfig.GetAwardConfig("AwardRule").ConfigCotent;
            }
            else
            {
                response.AwardRule = null;
            }

            //3、awardConnPreOrder表获取中奖用户， 要求优先显示免排队
            List<AwardUserDetail> listAwardUser = new List<AwardUserDetail>();
            if (isLottery)
            {
                var operateAwardConnPreOrder = new AwardConnPreOrderOperate();
                var listAwardConnPreOrder = operateAwardConnPreOrder.GetAwardConnPreOrderList(shopID, isAvoidQueue);
                if (listAwardConnPreOrder.Count > 0)
                {

                    listAwardConnPreOrder = listAwardConnPreOrder.Take(3).ToList();

                    if (isAvoidQueue)
                    {
                        // 免排队需要放一个中奖免排队的在第一位
                        var objAwardConnPreOrder = operateAwardConnPreOrder.GetAwardConnPreOrder(shopID);
                        if (!listAwardConnPreOrder.Contains(objAwardConnPreOrder))
                        {
                            listAwardConnPreOrder.Add(objAwardConnPreOrder);
                            listAwardConnPreOrder = listAwardConnPreOrder.OrderBy(a => a.Type).Take(3).ToList();
                        }
                        else
                        {
                            listAwardConnPreOrder = listAwardConnPreOrder.OrderBy(a => a.Type).ToList();
                        }
                    }


                    string customerIDS = "";
                    string shopAwardIDS = "'";
                    string vaAwardIDS = "'";
                    AwardUserDetail objAwardUserDetail = new AwardUserDetail();

                    foreach (var item in listAwardConnPreOrder)
                    {
                        objAwardUserDetail = new AwardUserDetail();
                        customerIDS += item.CustomerId + ",";
                        //2、免排队 3、菜
                        if (Common.ToInt32(item.Type) < 4 || Common.ToInt32(item.Type) > 1)
                        {
                            // 免排队功能未开启不需要显示
                            if (!isAvoidQueue && item.Type == AwardType.AvoidQueue)
                            {
                                continue;
                            }
                            shopAwardIDS += item.AwardId + "','";
                        }
                        else
                        {
                            vaAwardIDS += item.AwardId + "','";
                        }
                        objAwardUserDetail.CustomerID = item.CustomerId;
                        objAwardUserDetail.AwardID = item.AwardId;
                        listAwardUser.Add(objAwardUserDetail);
                    }
                    customerIDS = customerIDS.TrimEnd(',');
                    if (vaAwardIDS != "'")
                    {
                        vaAwardIDS = vaAwardIDS.Substring(0, vaAwardIDS.Length - 2);
                    }
                    if (shopAwardIDS != "'")
                    {
                        shopAwardIDS = shopAwardIDS.Substring(0, shopAwardIDS.Length - 2);
                    }

                    // 获取电话号码
                    if (!string.IsNullOrEmpty(customerIDS))
                    {
                        CustomerManager objCustomerManager = new CustomerManager();
                        DataTable dtCustomer = objCustomerManager.SelectCustomer(customerIDS);
                        if (dtCustomer != null && dtCustomer.Rows.Count > 0)
                        {
                            foreach (var awardUser in listAwardUser)
                            {
                                foreach (DataRow drCustomer in dtCustomer.Rows)
                                {
                                    if (awardUser.CustomerID == Convert.ToInt64(drCustomer["CustomerID"]))
                                    {
                                        awardUser.MobilePhone = Convert.ToString(drCustomer["mobilePhoneNumber"]);
                                        if (!string.IsNullOrEmpty(awardUser.MobilePhone) && awardUser.MobilePhone.Length == 11)
                                        {
                                            // 隐藏mobilePhone中间的号码为 xxxx
                                            awardUser.MobilePhone = awardUser.MobilePhone.Substring(0, 3) + "xxxx" + awardUser.MobilePhone.Substring(7, 4);
                                        }
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    // 获取奖名称
                    if (shopAwardIDS != "'")
                    {
                        ShopAwardOperate objShopAwardOperate = new ShopAwardOperate();
                        var listShopAward = objShopAwardOperate.SelectShopAwardList(shopAwardIDS);

                        foreach (var awardUser in listAwardUser)
                        {
                            foreach (var shopAward in listShopAward)
                            {
                                if (awardUser.AwardID == shopAward.Id)
                                {
                                    awardUser.AwardName = shopAward.Name;
                                    break;
                                }
                            }
                        }
                    }
                    // 获取全局奖品名称
                    if (vaAwardIDS != "'")
                    {
                        ViewAllocAwardOperate objViewAllocAwardOperate = new ViewAllocAwardOperate();
                        var listViewAllocAward = objViewAllocAwardOperate.SelectShopAwardList(vaAwardIDS);
                        foreach (var viewAllocAward in listViewAllocAward)
                        {
                            foreach (var awardUser in listAwardUser)
                            {
                                if (awardUser.AwardID == viewAllocAward.Id)
                                {
                                    awardUser.AwardName = viewAllocAward.Name;
                                    break;
                                }
                            }

                        }
                    }
                }
            }
            response.ListAwardUserDetail = listAwardUser;

            //4、显示店铺的抵扣券列表
            var operate = new CouponOperate();
            response.couponDetails = operate.GetShopCouponDetails(shopID);

            //5、显示店铺的折扣
            var operateShopVip = new ShopVIPOperate();
            response.CouponDiscount = operateShopVip.GetShopVipDiscount(shopID);

            //6、显示店铺公告
            var operateShopInfo = new ShopOperate();
            response.ShopNoticeContent = operateShopInfo.GetShop(shopID).orderDishDesc;

            response.ErrorState = 1;
        }
        catch
        {
            response.ErrorState = -1;
        }
        return JsonOperate.JsonSerializer<VAShopAwardNoticeResponse>(response);
    }

    /// <summary>
    /// 随机奖品展示页面
    /// </summary>
    /// <param name="preOrder19DianId"></param>
    /// <param name="shopID"></param>
    /// <returns></returns>
    [WebMethod]
    public static string GetAwardDetail(long preOrder19DianId, int shopID)
    {
        AwardShowResponse response = new AwardShowResponse();
        try
        {
            AwardConnPreOrderOperate operateAwardConnPreOrder = new AwardConnPreOrderOperate();
            ShopAwardOperate operateShopAward = new ShopAwardOperate();
            ViewAllocAwardOperate operateViewAllocAward = new ViewAllocAwardOperate();
            ShopOperate operateShop = new ShopOperate();
            var objAwardConnPreOrder = operateAwardConnPreOrder.SelectAwardConnPreOrderByOrderId(preOrder19DianId);
            if (objAwardConnPreOrder.Id != Guid.Empty)
            {
                ShopAward objShopAward = new ShopAward();
                ViewAllocAward objVAAward = new ViewAllocAward();

                response.ShopAwardType = objAwardConnPreOrder.Type;
                response.ShopName = operateShop.GetShop(objAwardConnPreOrder.ShopId).shopName;
                // 免排队|菜
                if (objAwardConnPreOrder.Type == AwardType.AvoidQueue || objAwardConnPreOrder.Type == AwardType.PresentDish)
                {
                    objShopAward = operateShopAward.QueryShopAward(objAwardConnPreOrder.AwardId);
                    response.AwardName = objShopAward.Name;
                    if (objAwardConnPreOrder.Type == AwardType.AvoidQueue)
                    {
                        response.NotLotteryDate = objAwardConnPreOrder.ValidTime.ToShortDateString();
                    }
                    else
                    {
                        // 菜品名称
                        string dishName = operateShopAward.GetDishNameI18nID(objShopAward.DishId);
                        response.AwardName = dishName;

                        // 菜品图片
                        string dishPicUrl = operateShopAward.GetDishPicUrl(shopID, objShopAward.DishId);
                        response.DishPicUrl = WebConfig.CdnDomain + WebConfig.ImagePath + dishPicUrl;
                    }
                }
                // 红包|第三方
                else if (objAwardConnPreOrder.Type == AwardType.PresentRedEnvelope || objAwardConnPreOrder.Type == AwardType.PresentThirdParty)
                {
                    objVAAward = operateViewAllocAward.SelectVAAward(objAwardConnPreOrder.AwardId);
                    response.AwardName = objVAAward.Name;
                    if (objAwardConnPreOrder.Type == AwardType.PresentRedEnvelope)
                    {
                        //获取用户信息
                        string mobile = "";
                        DataTable dtUser = CustomerOperate.QueryPartCustomerInfo(preOrder19DianId);
                        if (dtUser != null && dtUser.Rows.Count > 0)
                        {
                            mobile = dtUser.Rows[0]["mobilePhoneNumber"].ToString();
                            //查询用户红包
                            RedEnvelopeOperate redEnvelopeOperate = new RedEnvelopeOperate();
                            double redEnvelopeAmount = redEnvelopeOperate.SelectRedEnvelopeAmount(objAwardConnPreOrder.redEnvelopeId);
                            response.RedMoneyTotal = Common.ToDecimal(redEnvelopeAmount);

                            response.RedUrl = WebConfig.ServerDomain + "AppPages/RedEnvelope/list.aspx?mobilePhone={0}";
                            response.RedUrl = string.Format(response.RedUrl, mobile);

                        }
                    }
                    else
                    {
                        // 第三方描述
                        response.ThirdMemoInfo = objVAAward.Description;
                        // 这里订单入座后才返回url ,未入座提示 入座后才能启用
                        PreOrder19dianOperate operate19dian = new PreOrder19dianOperate();
                        var objPreOrder19Dian = operate19dian.GetPreOrder19dianById(preOrder19DianId);
                        if (objPreOrder19Dian != null && objPreOrder19Dian.status == VAPreorderStatus.Completed)
                        {
                            // 第三方URL
                            response.ThirdUrl = objVAAward.Content;//(content里面存取第三方URL的内容)
                            response.ThirdMemoInfo = objVAAward.Description;
                            response.IsThirdCompoate = true;
                        }
                        else
                        {
                            response.ThirdMemoInfo = "订单消费完成后才能获取奖品哦";
                            response.IsThirdCompoate = false;
                        }
                    }
                }
            }
            response.ErrorState = 1;
        }
        catch
        {
            response.ErrorState = -1;
        }
        return JsonOperate.JsonSerializer<AwardShowResponse>(response);
    }

    /// <summary>
    /// 随机奖品展示model
    /// </summary>
    public class AwardShowResponse
    {
        /// <summary>
        /// 奖品名称
        /// </summary>
        public string AwardName { get; set; }

        /// <summary>
        /// 奖品类型
        /// </summary>
        public AwardType ShopAwardType { get; set; }

        /// <summary>
        /// 菜品图片地址
        /// </summary>
        public string DishPicUrl { get; set; }

        /// <summary>
        /// 商家名称
        /// </summary>
        public string ShopName { get; set; }

        /// <summary>
        /// 免排队日期
        /// </summary>
        public string NotLotteryDate { get; set; }

        /// <summary>
        /// 红包金额
        /// </summary>
        public decimal RedMoneyTotal { get; set; }

        /// <summary>
        /// 第三方奖品的提示信息
        /// </summary>
        public string ThirdMemoInfo { get; set; }

        /// <summary>
        /// 第三方URL链接
        /// </summary>
        public string ThirdUrl { get; set; }

        /// <summary>
        /// 第三方需要判断是否入座
        /// </summary>
        public bool IsThirdCompoate { get; set; }

        /// <summary>
        /// 红包URL
        /// </summary>
        public string RedUrl { get; set; }

        /// <summary>
        /// 错误码 1:正常 -1:系统异常
        /// </summary>
        public int ErrorState { get; set; }
    }

    /// <summary>
    /// 门店公告需要显示的内容
    /// </summary>
    public class VAShopAwardNoticeResponse
    {
        /// <summary>
        /// 抽奖规则
        /// </summary>
        public string AwardRule { get; set; }

        /// <summary>
        /// 折扣
        /// </summary>
        public double CouponDiscount { get; set; }

        /// <summary>
        /// 公告内容
        /// </summary>
        public string ShopNoticeContent { get; set; }

        /// <summary>
        /// 是否开启免排队
        /// </summary>
        public bool isAvoidQueue { get; set; }

        /// <summary>
        /// 是否开启抽奖功能
        /// </summary>
        public bool isLottery { get; set; }

        /// <summary>
        /// 错误状态码(-1:系统异常 1：正常状态）
        /// </summary>
        public int ErrorState { get; set; }

        /// <summary>
        /// 奖品列表
        /// </summary>
        public List<ShopAwardDetail> ListShopAwardDetail { get; set; }

        /// <summary>
        /// 最近中奖list
        /// </summary>
        public List<AwardUserDetail> ListAwardUserDetail { get; set; }

        /// <summary>
        /// 抵扣券列表
        /// </summary>
        public List<OrderPaymentCouponDetail> couponDetails { get; set; }
    }

    /// <summary>
    /// 店铺奖品详情
    /// </summary>
    public class ShopAwardDetail
    {
        /// <summary>
        /// 奖项名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 奖品类型
        /// </summary>
        public AwardType ShopAwardType { get; set; }
        /// <summary>
        /// 奖项描述
        /// </summary>
        public string Content { get; set; }
    }

    /// <summary>
    /// 中奖用户详情
    /// </summary>
    public class AwardUserDetail
    {
        /// <summary>
        /// 中奖用户电话
        /// </summary>
        public string MobilePhone { get; set; }

        /// <summary>
        /// 中奖奖项名称
        /// </summary>
        public string AwardName { get; set; }

        /// <summary>
        /// 客户ID
        /// </summary>
        public long CustomerID { get; set; }

        /// <summary>
        /// 奖品ID
        /// </summary>
        public Guid AwardID { get; set; }
    }
    #endregion

    #region 悠先服务
    /// <summary>
    /// 添加免排队
    /// </summary>
    /// <param name="shopID">对应商家ID</param>
    /// <param name="count">免排队数量</param>
    /// <param name="employeeID">员工ID</param>
    /// <returns></returns>
    [WebMethod]
    public static string AddQueue(int shopID, int count, int employeeID)
    {
        ShopAwardOperate operateShopAward = new ShopAwardOperate();
        Guid shopAwardID = Guid.NewGuid();
        ShopAward objShopAward = new ShopAward()
        {
            Id = shopAwardID,
            ShopId = shopID,
            AwardType = AwardType.AvoidQueue,
            DishId = 0,
            Name = "免排队",
            Count = count,
            SubsidyAmount = 0,
            Probability = 0,
            Enable = true,
            Status = true,
            CreateTime = DateTime.Now,
            CreatedBy = Convert.ToString(employeeID),
            LastUpdateTime = DateTime.Now,
            LastUpdatedBy = Convert.ToString(employeeID)
        };

        bool isSuccess = operateShopAward.InsertShopAward(objShopAward);
        ReturnResult objReturnResult = new ReturnResult()
        {
            ErrorState = isSuccess ? 1 : -1,
            IsSuccess = isSuccess,
            Message = isSuccess ? "添加成功" : "添加失败"
        };

        // 添加商家奖品版本变更记录
        if (isSuccess)
        {
            ShopAwardVersionOperate operateVersion = new ShopAwardVersionOperate();
            operateVersion.InsertShopAwardVersionAndLog(employeeID, shopID, "添加免排队", "悠先服务", shopAwardID);
        }
        return JsonOperate.JsonSerializer<ReturnResult>(objReturnResult);
    }

    /// <summary>
    /// 添加菜品列表
    /// </summary>
    /// <param name="dishJson"></param>
    /// <returns></returns>
    [WebMethod]
    public static string AddDishMeau(string dishJson)
    {
        //LogDll.LogManager.WriteLog(LogFile.Trace, DateTime.Now + "--" + dishJson);
        bool isSuccess = false;
        ListDishInfo listDishInfo = new ListDishInfo();
        dishJson = dishJson.Replace("\\", "").Replace("dishJson:", "");
        listDishInfo = JsonOperate.JsonDeserialize<ListDishInfo>(dishJson);
        ShopAwardOperate operateShopAward = new ShopAwardOperate();

        foreach (DishInfo obj in listDishInfo.dishInfoList)
        {
            Guid shopAwardID = Guid.NewGuid();
            ShopAward objShopAward = new ShopAward()
            {
                Id = shopAwardID,
                ShopId = obj.shopID,
                AwardType = AwardType.PresentDish,
                DishId = obj.dishID,
                DishPriceId = obj.dishPriceID,
                Name = "赠菜",
                Count = obj.count,
                SubsidyAmount = 0,
                Probability = 0,
                Enable = true,
                Status = true,
                CreateTime = DateTime.Now,
                CreatedBy = Convert.ToString(obj.employeeID),
                LastUpdateTime = DateTime.Now,
                LastUpdatedBy = Convert.ToString(obj.employeeID)
            };
            isSuccess = operateShopAward.InsertShopAward(objShopAward);

            // 添加商家奖品版本变更记录
            if (isSuccess)
            {
                var dishName = operateShopAward.GetDishNameI18nID(obj.dishID);
                ShopAwardVersionOperate operateVersion = new ShopAwardVersionOperate();
                operateVersion.InsertShopAwardVersionAndLog(obj.employeeID, obj.shopID, "新赠赠菜【" + dishName + "】X" + obj.count, "悠先服务", shopAwardID);
            }
        }

        ReturnResult objReturnResult = new ReturnResult()
        {
            ErrorState = isSuccess ? 1 : -1,
            IsSuccess = isSuccess,
            Message = isSuccess ? "添加成功" : "添加失败"
        };
        return JsonOperate.JsonSerializer<ReturnResult>(objReturnResult);
    }


    /// <summary>
    /// 抽奖活动设置-查看奖品
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    public static string SearchAwardList(int shopID)
    {
        ListAwardDetail response = new ListAwardDetail();
        try
        {
            // 开关规则
            var operateAwardConfig = new AwardConfigOperate();
            // 是否开启免排队
            var isAvoidQueue = Convert.ToBoolean(Convert.ToInt32(operateAwardConfig.GetAwardConfig("AvoidQueue").ConfigCotent));

            ShopAwardOperate shopAwardOperate = new ShopAwardOperate();
            var listShopAward = shopAwardOperate.SelectShopAwardList(shopID);
            ViewAllocAwardOperate viewAllocAwardOperate = new ViewAllocAwardOperate();
            var listVAAward = viewAllocAwardOperate.SelectShopAwardList();

            response.IsAvoidQueue = isAvoidQueue;

            response.AwardDetailList = new List<AwardDetail>();

            AwardDetail objAwardDetail = new AwardDetail();
            ShopInfoCacheLogic shopInfoCacheLogic = new ShopInfoCacheLogic();
            // 店家的奖品
            foreach (var shopAward in listShopAward)
            {
                objAwardDetail = new AwardDetail();
                // 免排队功能未开启不需要显示
                if (!isAvoidQueue && shopAward.AwardType == AwardType.AvoidQueue)
                {
                    continue;
                }
                objAwardDetail.AwardID = shopAward.Id.ToString();
                objAwardDetail.AwardName = shopAward.Name;
                objAwardDetail.Count = shopAward.Count;
                objAwardDetail.Type = shopAward.AwardType;
                objAwardDetail.DishID = shopAward.DishId;
                objAwardDetail.DishPriceID = shopAward.DishPriceId;
                // 根据shopID获取shopName
                objAwardDetail.SuppliersName = shopInfoCacheLogic.GetShopInfo(shopID).shopName;
                response.AwardDetailList.Add(objAwardDetail);
            }
            // 全局奖品
            foreach (var vaAward in listVAAward)
            {
                objAwardDetail = new AwardDetail();
                objAwardDetail.AwardID = vaAward.Id.ToString();
                objAwardDetail.AwardName = vaAward.Name;
                objAwardDetail.Count = vaAward.Count;
                objAwardDetail.Type = vaAward.AwardType;
                objAwardDetail.DishID = vaAward.ActivityId;
                objAwardDetail.SuppliersName = "悠先点菜";
                response.AwardDetailList.Add(objAwardDetail);
            }

            // 获取菜品名称
            foreach (var item in response.AwardDetailList)
            {
                if (item.Type == AwardType.PresentDish)
                {
                    // 菜品名称
                    string dishName = shopAwardOperate.GetDishNameI18nID(item.DishID);
                    item.AwardName = dishName;
                }
            }

            response.ErrorState = 1;
        }
        catch
        {
            response.ErrorState = -1;
        }
        return JsonOperate.JsonSerializer<ListAwardDetail>(response);
    }

    /// <summary>
    /// 查询菜品
    /// </summary>
    /// <param name="pageIndex"></param>
    /// <param name="pageSize"></param>
    /// <param name="key">查询关键字</param>
    /// <returns></returns>
    [WebMethod]
    public static string SearchDishMeau(int pageIndex, int pageSize, string key, int shopID)
    {
        IDishInfoRepository dishInfoRepository = ServiceFactory.Resolve<IDishInfoRepository>();
        IPagedList<DishDetails> list = null;
        //list = dishInfoRepository.GetPageShopAllDishDetailses(
        //                            new VAGastronomistMobileApp.SQLServerDAL.Persistence.Infrastructure.Page(pageIndex, pageSize), shopID,key);

        list = dishInfoRepository.GetPageShopAllDishDetailsForAward(
                                    new VAGastronomistMobileApp.SQLServerDAL.Persistence.Infrastructure.Page(pageIndex, pageSize), shopID, key);

        ListDishInfoDetail dishInfoList = new ListDishInfoDetail();
        dishInfoList.dishInfoDetailList = list.Select(p => new DishInfoDetail
        {
            dishID = p.DishId,
            dishPriceID = p.DishPriceId,
            dishName = p.DishName +"-"+ p.ScaleName
        }).ToList();
        dishInfoList.hasPageNext = list.HasNextPage;
        return JsonOperate.JsonSerializer<ListDishInfoDetail>(dishInfoList);
    }

    /// <summary>
    /// 抽奖活动统计
    /// </summary>
    /// <param name="pageIndex"></param>
    /// <param name="pageSize"></param>
    /// <param name="shopID"></param>
    /// <returns></returns>
    [WebMethod]
    public static string SearchAwardTotal(int pageIndex, int pageSize, int shopID)
    {
        ListAwardTotalDetail response = new ListAwardTotalDetail();
        response.AwardTotalDetailList = new List<AwardTotalDetail>();
        try
        {
            AwardConnPreOrderOperate operateAwardConnPreOrder = new AwardConnPreOrderOperate();
            DataTable dt = operateAwardConnPreOrder.GetAwardTotalList(shopID);

            AwardTotalDetail objAwardTotalDetail = new AwardTotalDetail();
            objAwardTotalDetail.AwardCountList = new List<AwardCount>();
            AwardCount objAwardCount = new AwardCount();

            // 未中奖设置，用于添加未中奖实体
            bool isNoAward = false;
            foreach (DataRow dr in dt.Rows)
            {
                isNoAward = false;
                if (Convert.ToDateTime(dr["getTime"]) == objAwardTotalDetail.AwardDate && objAwardTotalDetail.OrderTotalCount == 0)
                {
                    continue;
                }
                // 中奖时间不同属于不同的页
                if (Convert.ToDateTime(dr["getTime"]) != objAwardTotalDetail.AwardDate)
                {
                    // 中奖时间为空，表示第一次进入，objAwardTotalDetail.AwardCountList 还为空
                    if (objAwardTotalDetail.AwardDate.ToString() != "0001/1/1 0:00:00" && objAwardTotalDetail.OrderTotalCount > 0)
                    {
                        isNoAward = true;
                        // 这里需要添加未中奖的实体 
                        // 未中奖的数量=总的数量-其它中奖的数量
                        objAwardCount = new AwardCount();
                        objAwardCount.AwardName = "未中奖";
                        int awardCount = 0;
                        foreach (var item in objAwardTotalDetail.AwardCountList)
                        {
                            awardCount += item.Count;
                        }
                        objAwardCount.Count = objAwardTotalDetail.OrderTotalCount - awardCount;
                        objAwardTotalDetail.AwardCountList.Add(objAwardCount);
                    }

                    objAwardTotalDetail = new AwardTotalDetail();
                    objAwardTotalDetail.AwardCountList = new List<AwardCount>();
                    objAwardTotalDetail.AwardDate = Common.ToDateTime(dr["getTime"]);
                    // 这里获取对应时间下的订单总量和订单总金额
                    PreOrder19dianOperate operatePreOrder19dian = new PreOrder19dianOperate();
                    DataTable dtPreOrder19dian = operatePreOrder19dian.GetPreOrder19DianByPayTime(objAwardTotalDetail.AwardDate, Common.ToDateTime(dr["getTime"]).AddDays(1).AddSeconds(-1), shopID);
                    if (dtPreOrder19dian.Rows.Count > 0)
                    {
                        DataRow drPreOrder19dian = dtPreOrder19dian.Rows[0];
                        objAwardTotalDetail.OrderMoneyTotal = Common.ToDecimal(drPreOrder19dian["prePaidSumTotal"]);
                        objAwardTotalDetail.OrderTotalCount = Common.ToInt32(drPreOrder19dian["prePaidCount"]);
                        if (objAwardTotalDetail.OrderTotalCount == 0)
                        {
                            continue;
                        }
                    }
                    //response.AwardTotalDetailList = new List<AwardTotalDetail>();
                    response.AwardTotalDetailList.Add(objAwardTotalDetail);

                }

                objAwardCount = new AwardCount();
                objAwardCount.AwardName = Convert.ToString(dr["AwardName"]);
                objAwardCount.Count = Common.ToInt32(dr["Count"]);
                objAwardTotalDetail.AwardCountList.Add(objAwardCount);
            }
            // 分页
            if (response.AwardTotalDetailList != null && response.AwardTotalDetailList.Count > 0)
            {
                if (!isNoAward)
                {
                    // 这里需要添加未中奖的实体 
                    // 未中奖的数量=总的数量-其它中奖的数量
                    objAwardCount = new AwardCount();
                    objAwardCount.AwardName = "未中奖";
                    int awardCount = 0;
                    foreach (var item in objAwardTotalDetail.AwardCountList)
                    {
                        awardCount += item.Count;
                    }
                    objAwardCount.Count = objAwardTotalDetail.OrderTotalCount - awardCount;
                    objAwardTotalDetail.AwardCountList.Add(objAwardCount);
                }

                int totalCount = response.AwardTotalDetailList.Count;
                response.AwardTotalDetailList = response.AwardTotalDetailList.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                if (totalCount % pageIndex > 0)
                {
                    response.HasNext = true;
                }
                else
                {
                    response.HasNext = false;
                }
            }
            response.ErrorState = 1;
        }
        catch
        {
            response.ErrorState = -1;
        }
        return JsonOperate.JsonSerializer<ListAwardTotalDetail>(response);
    }

    /// <summary>
    /// 开通抽奖功能
    /// </summary>
    /// <param name="shopID"></param>
    /// <returns></returns>
    [WebMethod]
    public static string OpenDraw(int shopID)
    {
        bool isUpdate = true;
        ReturnResult response = new ReturnResult();
        try
        {
            ShopAwardOperate operateShopAward = new ShopAwardOperate();
            var shopAwardList = operateShopAward.SelectShopAwardList(shopID);
            if (shopAwardList.Count > 0)
            {
                isUpdate = operateShopAward.UpdateShopAwardEnable(shopID, 1);
            }

            response.ErrorState = isUpdate ? 1 : -1;
            response.IsSuccess = isUpdate;
            response.Message = isUpdate ? "开通成功" : "开通失败";
        }
        catch
        {
            response.ErrorState = -1;
            response.IsSuccess = false;
            response.Message = "开通失败";
        }
        return JsonOperate.JsonSerializer<ReturnResult>(response);
    }

    /// <summary>
    /// 抽奖活动批量设置 
    /// </summary>
    /// <param name="awardJson">批量修改的图片</param>
    /// <returns></returns>
    [WebMethod]
    public static string SetAwards(string awardJson, string oldAwardJson, int shopID, int employeeID)
    {
        ReturnResult response = new ReturnResult();
        response.IsSuccess = true;
        response.ErrorState = 1;
        response.Message = "修改成功";

        try
        {
            ListAwardDetail newListAwardDetail = new ListAwardDetail();
            ListAwardDetail oldListAwardDetail = new ListAwardDetail();
            // 修改后返回的活动列表
            newListAwardDetail = JsonOperate.JsonDeserialize<ListAwardDetail>(awardJson);
            // 修改之前的活动列表
            oldListAwardDetail = JsonOperate.JsonDeserialize<ListAwardDetail>(oldAwardJson);
            oldListAwardDetail.AwardDetailList = oldListAwardDetail.AwardDetailList.FindAll(a => a.Type == AwardType.AvoidQueue || a.Type == AwardType.PresentDish);

            //// 全部相同表示没做任何修改
            //bool isSame = true;
            //if(newListAwardDetail.AwardDetailList.Count==oldListAwardDetail.AwardDetailList.Count)
            //{
            //    foreach (AwardDetail newAwardDetail in newListAwardDetail.AwardDetailList)
            //    { 
            //        List<AwardDetail> oldAwardDetailList = new List<AwardDetail>();
            //        if (!string.IsNullOrEmpty(newAwardDetail.AwardID))
            //        {
            //            oldAwardDetailList = oldListAwardDetail.AwardDetailList.FindAll(a => a.AwardID == newAwardDetail.AwardID);
            //        }

            //        // 找到老数据
            //        if (oldAwardDetailList.Count == 0)
            //        {
            //            isSame = false;
            //            break;
            //        }
            //        else
            //        {
            //            if(oldListAwardDetail.AwardDetailList.FirstOrDefault()!=newAwardDetail)
            //            {
            //                isSame = false;
            //                break;
            //            }
            //        }
            //    }
            //    if (isSame)
            //    {
            //        return JsonOperate.JsonSerializer<ReturnResult>(response);
            //    }
            //}

            foreach (AwardDetail newAwardDetail in newListAwardDetail.AwardDetailList)
            {
                // 1、根据type获取奖品类型
                if (newAwardDetail.Type == AwardType.AvoidQueue || newAwardDetail.Type == AwardType.PresentDish) // 免排队|赠菜
                {
                    List<AwardDetail> oldAwardDetailList = new List<AwardDetail>();
                    if (!string.IsNullOrEmpty(newAwardDetail.AwardID))
                    {
                        oldAwardDetailList = oldListAwardDetail.AwardDetailList.FindAll(a => a.AwardID == newAwardDetail.AwardID);
                    }

                    // 找到老数据
                    if (oldAwardDetailList.Count > 0)
                    {
                        ShopAwardOperate operateShopAward = new ShopAwardOperate();
                        ShopAward objShopAward = operateShopAward.QueryShopAward(new Guid(newAwardDetail.AwardID));
                        objShopAward.Count = newAwardDetail.Count;
                        objShopAward.Id = new Guid(newAwardDetail.AwardID);

                        if (newAwardDetail.Count == 0)
                        {
                            objShopAward.Enable = false;
                        }
                        else
                        {
                            objShopAward.Enable = true;
                        }
                        objShopAward.DishId = newAwardDetail.DishID;
                        objShopAward.DishPriceId = newAwardDetail.DishPriceID;
                        objShopAward.LastUpdatedBy = employeeID.ToString();
                        objShopAward.LastUpdateTime = DateTime.Now;

                        // 修改之前存在的免排队数量为0时， 直接删除原有的免排队
                        if (newAwardDetail.Count == 0 && newAwardDetail.Type == AwardType.AvoidQueue)
                        {
                            // 直接删除老数据
                            bool isSuccess = operateShopAward.DeleteShopAward(new Guid(newAwardDetail.AwardID));
                            response = new ReturnResult()
                            {
                                ErrorState = isSuccess ? 1 : -1,
                                IsSuccess = isSuccess,
                                Message = isSuccess ? "修改成功" : "修改失败"
                            };

                            // 添加商家奖品版本变更记录
                            if (isSuccess)
                            {
                                ShopAwardVersionOperate operateVersion = new ShopAwardVersionOperate();
                                operateVersion.InsertShopAwardVersionAndLog(employeeID, shopID, "关闭免排队", "悠先服务", objShopAward.Id);
                            }
                        }
                        else
                        {
                            bool isSuccess = operateShopAward.UpdateShopAwardOfDish(objShopAward);
                            response = new ReturnResult()
                            {
                                ErrorState = isSuccess ? 1 : -1,
                                IsSuccess = isSuccess,
                                Message = isSuccess ? "修改成功" : "修改失败"
                            };

                            // 添加商家奖品版本变更记录
                            if (isSuccess)
                            {
                                ShopAwardVersionOperate operateVersion = new ShopAwardVersionOperate();
                                operateVersion.InsertShopAwardVersionAndLog(employeeID, shopID, "修改免排队名额为" + objShopAward.Count, "悠先服务", objShopAward.Id);
                            }
                        }
                    }
                    else // 未找到对应的老数据，表示新增数据
                    {
                        ShopAwardOperate operateShopAward = new ShopAwardOperate();
                        if (newAwardDetail.Type == AwardType.AvoidQueue && newAwardDetail.Count > 0) // 免排队正常的都有
                        {
                            // 新增一条免排队
                            ShopAward objShopAward = new ShopAward()
                            {
                                Id = Guid.NewGuid(),
                                ShopId = shopID,
                                AwardType = AwardType.AvoidQueue,
                                DishId = 0,
                                Name = "免排队",
                                Count = newAwardDetail.Count,
                                SubsidyAmount = 0,
                                Probability = 0,
                                //Enable = false,
                                Enable=true,
                                Status = true,
                                CreateTime = DateTime.Now,
                                CreatedBy = Convert.ToString(employeeID),
                                LastUpdateTime = DateTime.Now,
                                LastUpdatedBy = null
                            };

                            bool isSuccess = operateShopAward.InsertShopAward(objShopAward);
                            response = new ReturnResult()
                            {
                                ErrorState = isSuccess ? 1 : -1,
                                IsSuccess = isSuccess,
                                Message = isSuccess ? "修改成功" : "修改失败"
                            };

                            // 添加商家奖品版本变更记录
                            if (isSuccess)
                            {
                                ShopAwardVersionOperate operateVersion = new ShopAwardVersionOperate();
                                operateVersion.InsertShopAwardVersionAndLog(employeeID, shopID, "开启免排队" + objShopAward.Count + "份", "悠先服务", objShopAward.Id);
                            }
                        }
                        else if (newAwardDetail.Type == AwardType.PresentDish)// 赠菜新增
                        {
                            ShopAward objShopAward = new ShopAward()
                            {
                                Id = Guid.NewGuid(),
                                ShopId = shopID,
                                AwardType = AwardType.PresentDish,
                                DishId = newAwardDetail.DishID,
                                DishPriceId = newAwardDetail.DishPriceID,
                                Name = "赠菜",
                                Count = newAwardDetail.Count,
                                SubsidyAmount = 0,
                                Probability = 0,
                                Enable = true,
                                Status = true,
                                CreateTime = DateTime.Now,
                                CreatedBy = Convert.ToString(employeeID),
                                LastUpdateTime = DateTime.Now,
                                LastUpdatedBy = null
                            };
                            bool isSuccess = operateShopAward.InsertShopAward(objShopAward);
                            response = new ReturnResult()
                            {
                                ErrorState = isSuccess ? 1 : -1,
                                IsSuccess = isSuccess,
                                Message = isSuccess ? "修改成功" : "修改失败"
                            };

                            // 添加商家奖品版本变更记录
                            if (isSuccess)
                            {
                                var dishName = operateShopAward.GetDishNameI18nID(objShopAward.DishId);
                                ShopAwardVersionOperate operateVersion = new ShopAwardVersionOperate();
                                operateVersion.InsertShopAwardVersionAndLog(employeeID, objShopAward.ShopId, "新赠赠菜【" + dishName + "】X" + objShopAward.Count, "悠先服务", objShopAward.Id);
                            }
                        }
                    }
                }
            }
            // 查看老的奖品列表里面是否存在新的奖品项， 如果不存则删除老的奖项
            foreach (AwardDetail oldAwardDetail in oldListAwardDetail.AwardDetailList)
            {
                // 1、根据type获取奖品类型
                if (oldAwardDetail.Type == AwardType.AvoidQueue || oldAwardDetail.Type == AwardType.PresentDish) // 免排队|赠菜
                {
                    AwardDetail newAwardDetail = new AwardDetail();
                    newAwardDetail = newListAwardDetail.AwardDetailList.Find(a => a.AwardID == oldAwardDetail.AwardID);
                    // 未找到老数据
                    if (newAwardDetail == null)
                    {
                        ShopAwardOperate operateShopAward = new ShopAwardOperate();
                        // 直接删除老数据
                        bool isSuccess = operateShopAward.DeleteShopAward(new Guid(oldAwardDetail.AwardID));
                        response = new ReturnResult()
                        {
                            ErrorState = isSuccess ? 1 : -1,
                            IsSuccess = isSuccess,
                            Message = isSuccess ? "修改成功" : "修改失败"
                        };

                        // 添加商家奖品版本变更记录
                        if (isSuccess)
                        {

                            ShopAwardVersionOperate operateVersion = new ShopAwardVersionOperate();
                            // 删除赠菜
                            if (oldAwardDetail.Type == AwardType.PresentDish)
                            {
                                var dishName = operateShopAward.GetDishNameI18nID(oldAwardDetail.DishID);
                                operateVersion.InsertShopAwardVersionAndLog(employeeID, shopID, "删除赠菜【" + dishName + "】X" + oldAwardDetail.Count, "悠先服务", new Guid(oldAwardDetail.AwardID));
                            }
                            else
                            {
                                operateVersion.InsertShopAwardVersionAndLog(employeeID, shopID, "关闭免排队", "悠先服务", new Guid(oldAwardDetail.AwardID));
                            }
                        }
                    }
                }
            }
        }
        catch
        {
            response.IsSuccess = false;
            response.ErrorState = -1;
            response.Message = "系统异常";
        }
        return JsonOperate.JsonSerializer<ReturnResult>(response);
    }

    /// <summary>
    /// 抽奖活动统计图表
    /// </summary>
    /// <param name="shopID">商家ID</param>
    /// <returns></returns>
    [WebMethod]
    public static string GetAwardPicTable(int shopID)
    {
        AwardPicTotalDetail response = new AwardPicTotalDetail();
        try
        {
            // 获取当月统计金额
            response.listAwardPicTotalMonthDetail = new List<AwardPicTotalMonthDetail>();
            AwardPicTotalMonthDetail objAwardPicTotalMonthDetail = new AwardPicTotalMonthDetail();

            AwardConnPreOrderOperate operateAwardConnPreOrder = new AwardConnPreOrderOperate();
            DataTable dt = operateAwardConnPreOrder.GetAwardTotalMonthList(shopID);
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    objAwardPicTotalMonthDetail = new AwardPicTotalMonthDetail()
                    {
                        MonthAwardName = Convert.ToString(dr["AwardName"]),
                        MonthAwardCount = Common.ToInt32(dr["Count"])
                    };
                    response.listAwardPicTotalMonthDetail.Add(objAwardPicTotalMonthDetail);
                }
            }
            // 获取店铺月订单总量
            PreOrder19dianOperate operatePreOrder19dian = new PreOrder19dianOperate();
            DataTable dtPreOrder19dian = operatePreOrder19dian.GetPreOrder19DianByPayTime(Convert.ToDateTime(DateTime.Now.Year + "-" + DateTime.Now.Month + "-01 00:00:00"), Convert.ToDateTime(DateTime.Now.Year + "-" + DateTime.Now.AddMonths(1).Month + "-01 00:00:00"), shopID);
            int awardCount = 0;
            foreach (var item in response.listAwardPicTotalMonthDetail)
            {
                awardCount += item.MonthAwardCount;
            }
            if (dtPreOrder19dian.Rows.Count > 0)
            {
                DataRow drPreOrder19dian = dtPreOrder19dian.Rows[0];
                objAwardPicTotalMonthDetail = new AwardPicTotalMonthDetail()
                {
                    MonthAwardName = "未中奖",
                    MonthAwardCount = Common.ToInt32(drPreOrder19dian["prePaidCount"]) - awardCount
                };
                response.listAwardPicTotalMonthDetail.Add(objAwardPicTotalMonthDetail);
            }

            // 获取最近3天的订单量
            response.listAwardPicTotalDayDetail = new List<AwardPicTotalDayDetail>();
            AwardPicTotalDayDetail objAwardPicTotalDayDetail = new AwardPicTotalDayDetail();


            dtPreOrder19dian = operatePreOrder19dian.GetPreOrder19DianByPayTime(Common.ToDateTime(DateTime.Now.AddMonths(-1).ToShortDateString() + " 00:00:00"), Common.ToDateTime(DateTime.Now.ToShortDateString() + " 23:59:59"), shopID);
            if (dtPreOrder19dian.Rows.Count > 0)
            {
                foreach (DataRow dr in dtPreOrder19dian.Rows)
                {
                    if (response.listAwardPicTotalDayDetail.Count == 3)
                    {
                        break;
                    }

                    objAwardPicTotalDayDetail = new AwardPicTotalDayDetail()
                    {
                        OrderCount = Common.ToInt32(dr["prePaidCount"]),
                        OrderMoneyTotal = Common.ToDecimal(dr["prePaidSumTotal"]),
                        Day = Common.ToDateTime(dr["preOrderTime"]).Month + "." + Common.ToDateTime(dr["preOrderTime"]).Day
                    };

                    response.listAwardPicTotalDayDetail.Add(objAwardPicTotalDayDetail);
                }
            }
            response.ErrorState = 1;
            response.Month = DateTime.Now.Month;
            response.OrderCountName = "订单量";
            response.OrderMoneyTotalName = "订单金额";
        }
        catch
        {
            response.ErrorState = -1;
        }
        return JsonOperate.JsonSerializer<AwardPicTotalDetail>(response);
    }

    #region model

    /// <summary>
    /// 奖品图表统计详情实体
    /// </summary>
    public class AwardPicTotalDetail
    {
        public List<AwardPicTotalMonthDetail> listAwardPicTotalMonthDetail { get; set; }

        public List<AwardPicTotalDayDetail> listAwardPicTotalDayDetail { get; set; }

        /// <summary>
        /// 错误状态 1:正常状态 -1:系统异常
        /// </summary>
        public int ErrorState { get; set; }

        /// <summary>
        /// 当前月
        /// </summary>
        public int Month { get; set; }

        /// <summary>
        /// 订单金额名称
        /// </summary>
        public string OrderMoneyTotalName { get; set; }

        /// <summary>
        /// 订单量名称 
        /// </summary>
        public string OrderCountName { get; set; }
    }

    /// <summary>
    /// 奖品月统计详情
    /// </summary>
    public class AwardPicTotalMonthDetail
    {
        /// <summary>
        /// 活动奖品名称
        /// </summary>
        public string MonthAwardName { get; set; }

        /// <summary>
        /// 活动奖品数量
        /// </summary>
        public int MonthAwardCount { get; set; }
    }

    /// <summary>
    /// 奖品日统计详情
    /// </summary>
    public class AwardPicTotalDayDetail
    {
        /// <summary>
        /// 订单金额
        /// </summary>
        public decimal OrderMoneyTotal { get; set; }

        /// <summary>
        /// 订单量
        /// </summary>
        public int OrderCount { get; set; }

        /// <summary>
        /// 对应的日期
        /// </summary>
        public string Day { get; set; }
    }

    /// <summary>
    /// 菜品list
    /// </summary>
    public class ListDishInfo
    {
        public List<DishInfo> dishInfoList { get; set; }
    }

    /// <summary>
    /// 菜品INFO
    /// </summary>
    public class DishInfo
    {
        /// <summary>
        /// 商家ID
        /// </summary>
        public int shopID { get; set; }

        /// <summary>
        /// 菜品ID
        /// </summary>
        public int dishID { get; set; }

        /// <summary>
        /// 菜品规格ID
        /// </summary>
        public int dishPriceID { get; set; }

        /// <summary>
        /// 菜品名称
        /// </summary>
        public string dishName { get; set; }

        /// <summary>
        /// 每日菜品份数
        /// </summary>
        public int count { get; set; }

        /// <summary>
        /// 创建者ID
        /// </summary>
        public int employeeID { get; set; }
    }

    /// <summary>
    /// 菜品详情LIST
    /// </summary>
    public class ListDishInfoDetail
    {
        public List<DishInfoDetail> dishInfoDetailList { get; set; }

        /// <summary>
        /// 是否还有下一页
        /// </summary>
        public bool hasPageNext { get; set; }
    }

    public class DishInfoDetail
    {
        /// <summary>
        /// 菜品ID
        /// </summary>
        public int dishID { get; set; }

        /// <summary>
        /// 菜品价格ID
        /// </summary>
        public int dishPriceID { get; set; }

        /// <summary>
        /// 菜品名称
        /// </summary>
        public string dishName { get; set; }
    }

    /// <summary>
    /// 返回结果
    /// </summary>
    public class ReturnResult
    {
        /// <summary>
        /// 错误状态 1:正常状态 -1:系统异常
        /// </summary>
        public int ErrorState { get; set; }

        /// <summary>
        /// 是否成功
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; }
    }

    /// <summary>
    /// 奖品列表
    /// </summary>
    public class ListAwardDetail
    {
        /// <summary>
        /// 奖品列表
        /// </summary>
        public List<AwardDetail> AwardDetailList { get; set; }

        /// <summary>
        /// 是否开启免排队
        /// </summary>
        public bool IsAvoidQueue { get; set; }

        /// <summary>
        /// 错误状态 1:正常状态 -1:系统异常
        /// </summary>
        public int ErrorState { get; set; }
    }

    public class AwardDetail
    {
        public string AwardID { get; set; }

        /// <summary>
        /// 菜品ID
        /// </summary>
        public int DishID { get; set; }

        /// <summary>
        /// 菜品价格ID
        /// </summary>
        public int DishPriceID { get; set; }

        /// <summary>
        /// 奖品名称
        /// </summary>
        public string AwardName { get; set; }

        /// <summary>
        /// 奖品数量
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 供应商名称
        /// </summary>
        public string SuppliersName { get; set; }

        /// <summary>
        /// 奖品类型(1、未中奖 2、免排队 3、菜 4、红包 5、第三方)
        /// </summary>
        public AwardType Type { get; set; }
    }

    /// <summary>
    /// 抽奖活动统计列表
    /// </summary>
    public class ListAwardTotalDetail
    {
        public List<AwardTotalDetail> AwardTotalDetailList { get; set; }

        /// <summary>
        /// 是否还有下一页
        /// </summary>
        public bool HasNext { get; set; }

        /// <summary>
        /// 错误状态 1:正常状态 -1:系统异常
        /// </summary>
        public int ErrorState { get; set; }
    }

    /// <summary>
    /// 统计详情
    /// </summary>
    public class AwardTotalDetail
    {
        /// <summary>
        /// 奖品列表
        /// </summary>
        public List<AwardCount> AwardCountList { get; set; }

        /// <summary>
        /// 订单总金额
        /// </summary>
        public decimal OrderMoneyTotal { get; set; }

        /// <summary>
        /// 订单总量
        /// </summary>
        public int OrderTotalCount { get; set; }

        /// <summary>
        /// 中奖日期
        /// </summary>
        public DateTime AwardDate { get; set; }
    }

    /// <summary>
    /// 奖品名称数量
    /// </summary>
    public class AwardCount
    {
        /// <summary>
        /// 奖品名称
        /// </summary>
        public string AwardName { get; set; }

        /// <summary>
        /// 奖品数量
        /// </summary>
        public int Count { get; set; }
    }
    #endregion

    #endregion
}