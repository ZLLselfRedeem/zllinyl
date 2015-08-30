using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Data;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.Model.Adjunction;

//
//  Copyright 2011 View Alloc inc. All rights reserved.
//  Created by Jason Xiao on 2012-04-10.
//
namespace VAGastronomistMobileApp.WebPageDll
{
    public class VAResponse
    {
        public VAMessageType type { get; set; }
        public VAResult result { get; set; }
    }
    public class VAEmployeeLoginRequest
    {
        public string userName { get; set; }
        public string statusGUID { get; set; }
    }

    #region 餐桌区域相关
    //查询所有桌台区域请求
    public class VATableAreaRequest : VAEmployeeLoginRequest
    {

    }
    //
    public class VATableAreaResponse : VAResponse
    {
        public List<TableAreaInfo> tableAreas;
    }
    #endregion

    #region 餐桌相关
    /// <summary>
    /// 开台Json序列化信息
    /// </summary>
    public class VAOpenTable : VAEmployeeLoginRequest
    {
        public int tableID { get; set; }
        public int peopleNumber { get; set; }
    }
    /// <summary>
    /// 开台回复Json序列化信息
    /// </summary>
    public class VAOpenTableResponse : VAResponse
    {
        public int orderID { get; set; }
    }
    /// <summary>
    /// 清台Json序列化信息
    /// </summary>
    public class VAClearTable : VAEmployeeLoginRequest
    {
        public int tableID { get; set; }
        public int orderID { get; set; }
    }
    /// <summary>
    /// 清台回复Json序列化信息
    /// </summary>
    public class VAClearTableResponse : VAResponse
    {

    }
    /// <summary>
    /// 餐桌Json序列化信息
    /// </summary>
    public class VATableAll : VAResponse
    {
        public TableList[] tables { get; set; }
    }
    public class VATableAllRequest : VAEmployeeLoginRequest
    {
        public int tableAreaID;//2012年7月16日15:45:52 tdq添加
    }
    public struct TableList
    {
        public int tableId { get; set; }
        public int defaultSeats { get; set; }
        public int seatedCustomers { get; set; }
        public string displayName { get; set; }
        public double bill { get; set; }
        public VATableStatus status { get; set; }
        public int orderID { get; set; }//2011-09-28 xiaoyu
        public int tableAreaID { get; set; }//2012-05-03 xiaoyu
        public string interfaceTableno { get; set; }//2012-06-04 tdq
        public string orderNote { get; set; }
    }
    /// <summary>
    /// 转台Json序列化信息
    /// </summary>
    public class VAChangeTable : VAEmployeeLoginRequest
    {
        public int originalTableID { get; set; }
        public int newTableID { get; set; }
        public int orderID { get; set; }
    }
    public class VAChangeTableResponse : VAResponse
    {

    }
    #endregion

    #region 预定相关
    /// <summary>
    /// 查询预定点单
    /// </summary>
    public class VAQueryReserveOrder
    {
        public int orderID { get; set; }
    }
    /// <summary>
    /// 预定点单回复Json序列化信息
    /// </summary>
    public class VAQueryReserveOrderResponse : VAResponse
    {
        public int orderID { get; set; }
        public string tableName { get; set; }
        public double totalToPay { get; set; }
        public string orderTime { get; set; }
        public string customerUserName { get; set; }
        public VAQueryOrderDetail[] orderDetails { get; set; }
    }
    /// <summary>
    /// 新增或修改预定信息
    /// </summary>
    public class VAModifyReserveRequest : VAEmployeeLoginRequest
    {
        public int reserveID { get; set; }
        public int tableID { get; set; }
        public string reserveName { get; set; }
        public string reservePhone { get; set; }
        public DateTime reserveDate { get; set; }
        public int dinnerTimeID { get; set; }
        public int peopleNumber { get; set; }
        public DateTime ReserveStartTime { get; set; }
        public DateTime ReserveEndTime { get; set; }
    }
    /// <summary>
    /// 预定返回信息
    /// </summary>
    public class VAReserveResponse : VAResponse
    {
        public int orderID { get; set; }
        public int reserveID { get; set; }
    }
    /// <summary>
    /// 预定列表信息
    /// </summary>
    public class VAReserveListRequest : VAEmployeeLoginRequest
    {

    }
    public class VAReserveListResponse : VAResponse
    {
        public List<VAReserve> reserveList { get; set; }
    }
    public struct VAReserve
    {
        public int reserveID { get; set; }
        public int tableID { get; set; }
        public string tableName { get; set; }
        public string reserveName { get; set; }
        public string reservePhone { get; set; }
        public DateTime reserveDate { get; set; }
        public int dinnerTimeID { get; set; }
        public string dinnerTimeName { get; set; }
        public int peopleNumber { get; set; }
        public int orderID { get; set; }//2011年12月17日tdq添加
        public DateTime ReserveStartTime { get; set; }
        public DateTime ReserveEndTime { get; set; }
    }
    /// <summary>
    /// 删除，到达，取消预定信息
    /// </summary>
    public class VAReserveRequest : VAEmployeeLoginRequest
    {
        public int orderID { get; set; }
        public int reserveID { get; set; }
    }
    /// <summary>
    /// 预定时对应的餐桌信息
    /// </summary>
    public class VAReserveTableRequest : VAEmployeeLoginRequest
    {
        public DateTime reserveDate { get; set; }
        public int dinnerTimeID { get; set; }
    }
    public class VAReserveTableResponse : VAResponse
    {
        public List<TableInfo> tableList { get; set; }
    }
    #endregion

    #region 排队相关
    /*******************查询排队队列信息接口***************/
    public struct VACallNumber
    {
        public int callNumberID { get; set; } // 唯一编号
        public string displayName { get; set; }
        public VAQueueStatus status { get; set; } // 状态
        public int waitingListId { get; set; } // 等候队列：A，B或C
        public DateTime creationTime { get; set; } // 开始排队的时间
        public int orderID { get; set; } // 相关的orderID，如果尚未点餐（QUEUE_WAITING、QUEUE_CANCEL）则为空
    }
    public class VAWaitingLine
    {
        //public int waitingLineID { get; set; }//队列编号
        //public string waitingLineName { get; set; }//队列名称
        public List<VACallNumber> callNumbers { get; set; }//队列内容
    }
    public class VAWaitingLineInfoRequest
    {

    }
    public class VAWaitingLineInfoResponse : VAResponse
    {
        public List<VAWaitingLine> waitingLines; // 队列数组，例如：有A B C 3个队列，则lines内又有3个NSArray，内层的array中含的是若干个VACallNumber对象
    }

    ///*******************排队开始点菜接口***************/
    //public class VAWaitingLineOrderStartRequest : VAEmployeeLoginRequest
    //{
    //    public int callNumberID { get; set; }
    //    public string password { get; set; }  // use string!
    //}
    //public class VAWaitingLineOrderStartResponse:VAResponse
    //{
    //    public int orderID { get; set; }
    //    public int callNumberID { get; set; }
    //}

    /*******************排队点菜提交接口***************/
    public class VAWaitingLineOrderSubmitRequest
    {
        public int orderID { get; set; }
        public int callNumberID { get; set; }
    }

    public class VAWaitingLineOrderSubmitResponse : VAResponse
    {
        public int orderID { get; set; }
        public int callNumberID { get; set; }
    }

    ///*******************排队查询点菜接口***************/
    //public class VAWaitingLineOrderQueryRequest
    //{
    //    public int orderID { get; set; }//
    //    public string password { get; set; } //
    //}
    //public class VAWaitingLineOrderQueryResponse
    //{
    //    public VAMessageType type { get; set; }
    //    public VAResult result { get; set; }
    //    public double totalToPay { get; set; }
    //    public string orderTime { get; set; }
    //    public List<VAQueryOrderDetail> orderDetails { get; set; }
    //}
    /// <summary>
    /// 排队点餐码验证请求信息
    /// </summary>
    public class VAVerifyWaitingLineCodeRequest
    {
        public string waitingLineCode { get; set; }
    }
    /// <summary>
    /// 排队点餐码验证返回信息
    /// </summary>
    public class VAVerifyWaitingLineCodeResponse : VAResponse
    {
        public int orderID { get; set; }
        public int seatedCustomers { get; set; }
        public string displayName { get; set; }//排队号
    }
    #endregion

    #region 点单相关
    /// <summary>
    /// 点单备注Json序列化信息
    /// </summary>
    public class VAOrderDetailNote
    {//
        public string option1 { get; set; }
        public string option2 { get; set; }
        public string option3 { get; set; }
        public string option4 { get; set; }
        public string option5 { get; set; }
        public string option6 { get; set; }
        public string option7 { get; set; }
        public string option8 { get; set; }
        public string option9 { get; set; }
        public string option10 { get; set; }
        public string other { get; set; }
    }

    /// <summary>
    /// 点单详情称重
    /// </summary>
    public class VAOrderDetailWeight
    {
        public int orderID { get; set; }
        public int orderDetailID { get; set; }
        public double dishQuantity { get; set; }
        public string userName { get; set; }
        public int employeeId { get; set; }
    }
    /// <summary>
    /// 点单详情Json序列化信息
    /// </summary>
    public class VAOrderDetail
    {
        public int orderID { get; set; }
        public int dishPriceI18nID { get; set; }
        public double dishPrice { get; set; }
        public double dishQuantity { get; set; }
        public string dishDetailNote { get; set; }
        public int orderDetailID { get; set; }
        public string dishName { get; set; }//自定义菜时传入菜名
        public string orderDetailOtherNote { get; set; }//2012-6-28  tdq
        public int discountTypeID { get; set; }//正常菜传0，自定义菜传入选择的编号
    }
    /// <summary>
    /// 点单新增和修改
    /// </summary>
    public class VAModifyOrderDetail : VAOrderDetail
    {
        public string userName { get; set; }
        public string statusGUID { get; set; }
    }
    /// <summary>
    /// 增加点单详情回复Json序列化信息
    /// </summary>
    public class VAOrderDetailResponse : VAResponse
    {
        public int orderDetailID { get; set; }
        public int dishID; //2011-12-31 增加
        public int dishPriceI18nID { get; set; }//2012-07-24
    }
    /// <summary>
    /// 删除点单详情Json序列化信息
    /// </summary>
    public class VARemoveOrderDetail
    {
        public int orderID { get; set; }
        public int orderDetailID { get; set; }
        public string dishDetailNote { get; set; }
    }
    /// <summary>
    /// 查询点单列表Json序列化信息
    /// </summary>
    public class VAQueryOrderList
    {
        public int orderID { get; set; }
    }
    /// <summary>
    /// 合并结账时查询多个点单列表
    /// </summary>
    public class VAQueryOrdersList
    {
        public List<int> orderID { get; set; }
    }
    /// <summary>
    /// 查询点单列表回复Json序列化信息
    /// </summary>
    public class VAQueryOrderListResponse : VAResponse
    {
        public int orderID { get; set; }
        public string tableName { get; set; }
        public double totalToPay { get; set; }
        public string orderTime { get; set; }
        public string customerUserName { get; set; }
        public VAQueryOrderDetail[] orderDetails { get; set; }
    }
    /// <summary>
    /// 合并结账时返回多个点单列表
    /// </summary>
    public class VAQueryOrdersListResponse : VAResponse
    {
        //public List<int> orderID { get; set; }
        public string tableName { get; set; }
        public double totalToPay { get; set; }
        public double serviceCharge { get; set; }//2011-02-28 xiaoyu 增加服务费
        public string orderTime { get; set; }
        public string customerUserName { get; set; }
        public List<VAQueryOrderDetail> orderDetails { get; set; }
    }
    public struct VAQueryOrderDetail
    {
        public int orderID { get; set; }
        public int orderDetailID { get; set; }
        public int dishPriceI18nID { get; set; }
        public double dishQuantity { get; set; }
        public double confirmedQuantity { get; set; }//2011-11-23 xiaoyu 增加，IPAD端判断是否能删除需要
        public double dishPriceSum { get; set; }
        public string dishDetailNote { get; set; }
        public string dishName { get; set; }
        public string scaleName { get; set; }
        public double dishPrice { get; set; }
        public int dishID { get; set; }//2011-9-27 xiaoyu 增加，B端需要
        public string tableName { get; set; }//2011-12-09 xiaoyu B端合并结账时显示每个菜所属餐桌
        public bool dishNeedWeigh { get; set; }//2012-03-15 xiaoyu 增加该菜是否需要称重
        public bool weighed { get; set; }//2012-03-15 xiaoyu 增加该菜是否已称重
        public string orderDetailOtherNote { get; set; }//2012年6月28日 tdq
        public int discountTypeID { get; set; }//2012年7月18日 tdq
        public double dishPriceOriginal { get; set; }//2012年7月19日 tdq   
        public int retreatOrderDetailId { get; set; }//2012年7月31日 tdq                    
    }
    /// <summary>
    /// 支付详情信息AddOrdersPay
    /// </summary>
    public class VAOrdersPay
    {
        public List<int> orderID { get; set; }//
        public int paymentID { get; set; }//本地支付方式id
        public double amount { get; set; }//支付总额
        public int cityLedgerCompanyID { get; set; }//2012-02-27 xiaoyu
        public int cityLedgerCustomerID { get; set; }//2012-02-27 xiaoyu
        public bool interfaceCheckOut { get; set; }//是否启用接口支付
        public string userName { get; set; }//用户名
        public string accnt;//帐号，如果是挂房帐，则是帐号accnt；团体帐，accnt帐号；单位帐，accnt；会员卡号取araccnt1
        public string roomno;//挂房帐时候的帐号roomno，团队帐时，roomno不用传
        public string vipno;//会员卡号no

    }
    /// <summary>
    /// 点单提交Json序列化信息
    /// </summary>
    public class VASubmitOrder
    {
        public int orderID { get; set; }
    }
    /// <summary>
    /// 点单提交回复Json序列化信息
    /// </summary>
    public class VASubmitOrderResponse : VAResponse
    {

    }
    /// <summary>
    /// 点单确认Json序列化信息
    /// </summary>
    public class VAConfirmOrder : VAEmployeeLoginRequest
    {
        public int orderID { get; set; }
    }
    /// <summary>
    /// 点单确认回复Json序列化信息
    /// </summary>
    public class VAConfirmOrderResponse : VAResponse
    {

    }
    /// <summary>
    /// 点单预结账Json序列化信息
    /// </summary>
    public class VAPreCheckOutOrder : VAEmployeeLoginRequest
    {
        public int orderID { get; set; }
        //public int customerRankID { get; set; }
        public string customerUserName { get; set; }//2011-11-17 xiaoyu修改 预结算时传入的用户等级编号修改为传入用户登录名
        public bool printFlag { get; set; }//是否打印账单标志
    }
    /// <summary>
    /// 多张点单预结账Json序列化信息
    /// </summary>
    public class VAPreCheckOutOrders : VAEmployeeLoginRequest
    {
        public List<int> orderID { get; set; }
        public int customerRankID { get; set; }//2012-02-29 xiaoyu 预结算时增加直接传入折扣编号方式，同时也保留用户名的方式
        public string customerUserName { get; set; }//2011-11-17 xiaoyu修改 预结算时传入的用户等级编号修改为传入用户登录名
        /// <summary>
        /// 0：不是原价，1：是原价
        /// </summary>
        public int dishPriceOriginalStatus { get; set; }//2011-12-13 xiaoyu增加 是否使用原价
        /// <summary>
        /// 0：不循环，1：循环
        /// </summary>
        public int findCustomerRankID { get; set; }//2011-12-13 xiaoyu增加 是否循环查找最高等级的折扣卡
    }
    /// <summary>
    /// 多张点单打折
    /// </summary>
    public class VADiscountModel : VAEmployeeLoginRequest
    {
        public int orderID { get; set; }
        public int orderDetailID { get; set; }
        public double dishPrice { get; set; }
        public double dishPriceSum { get; set; }
    }
    /// <summary>
    /// 点单预结账回复Json序列化信息
    /// </summary>
    public class VAPreCheckOutOrderResponse : VAResponse
    {
        public string customerRankName { get; set; }//2011-11-17 xiaoyu增加
        public string customerUserName { get; set; }//2011-11-17 xiaoyu增加
    }
    /// <summary>
    /// 多张点单预结账回复Json序列化信息
    /// </summary>
    public class VAPreCheckOutOrdersResponse : VAResponse
    {
        public string customerRankName { get; set; }//2011-11-17 xiaoyu增加
        public string customerUserName { get; set; }//2011-11-17 xiaoyu增加
    }
    /// <summary>
    /// 点单结账Json序列化信息
    /// </summary>
    public class VACheckOutOrder : VAEmployeeLoginRequest
    {
        public int orderID { get; set; }
    }

    /// <summary>
    /// 合并多张点单结账Json序列化信息
    /// </summary>
    public class VACheckOutOrders : VAEmployeeLoginRequest
    {
        public List<int> orderID { get; set; }
        public bool needRemoveChange { get; set; }//是否需要抹零 2012-03-31
        public string removeChangeUserName { get; set; }//抹零人的用户名
        public bool isRecheck { get; set; }
    }
    /// <summary>
    /// 点单结账回复Json序列化信息
    /// </summary>
    public class VACheckOutOrderResponse : VAResponse
    {

    }
    /// <summary>
    /// 台单打印信息
    /// </summary>
    public class VAPrintTableOrder
    {
        public string waiterName { get; set; }
        public int orderID { get; set; }
        public int peopleNumber { get; set; }
        public string tableName { get; set; }
        public string printerName { get; set; }//xiaoyu 2011-12-30 打印台单时按照系统设置的对应打印机名称进行打印
        public double totalToPay { get; set; }//xiaoyu 2012-01-05 打印台单时增加总价格
        public TableOrderDetail[] orderList { get; set; }
        public short printCopy { get; set; }//xiaoyu 2012-04-01 台单内容中增加打印份数
        public bool isCookOrder { get; set; }//xiaoyu 2012-04-01 台单中增加是否为厨单标志位
        public string orderNote { get; set; }
    }
    public struct TableOrderDetail
    {
        public string dishName { get; set; }//包括菜名和规格名
        public double dishQuantity { get; set; }
        public string dishNote { get; set; }
    }
    /// <summary>
    /// 划菜单打印信息
    /// </summary>
    public class VAPrintHuacaiOrder
    {
        public string waiterName { get; set; }
        public int orderID { get; set; }
        public int peopleNumber { get; set; }
        public string tableName { get; set; }
        public string printerName { get; set; }//xiaoyu 2011-12-30 打印台单时按照系统设置的对应打印机名称进行打印
        public double totalToPay { get; set; }//xiaoyu 2012-01-05 打印台单时增加总价格
        public HuacaiOrderDetail[] orderList { get; set; }
        public short printCopy { get; set; }//xiaoyu 2012-04-01 台单内容中增加打印份数
        public string orderNote { get; set; }
    }
    public struct HuacaiOrderDetail
    {
        public string dishName { get; set; }//包括菜名和规格名
        public double dishQuantity { get; set; }
        public string dishNote { get; set; }
        public double dishUnitPrice { get; set; }
    }
    /// <summary>
    /// 厨单打印信息
    /// </summary>
    public class VAPrintCookOrder
    {
        public int orderID { get; set; }
        public string tableName { get; set; }
        public string dishName { get; set; }//包括菜名和规格名
        public double dishQuantity { get; set; }
        public string dishNote { get; set; }
        public string printerName { get; set; }//xiaoyu 2011-12-30 打印厨单时按照折扣分类里设置的打印机名称进行打印
        public string employeeName { get; set; }//xiaoyu 2012-01-05 厨单内容中增加服务员名称
        public string discountTypeName { get; set; }//xiaoyu 2012-01-05 厨单内容中增加结算分类名称
        public string dishUnitPrice { get; set; }//xiaoyu 2012-01-05 厨单内容中增加菜单价
        public short printCopy { get; set; }//xiaoyu 2012-04-01 厨单内容中增加打印份数
        public string orderNote { get; set; }
    }
    /// <summary>
    /// 账单打印信息
    /// </summary>
    public class VAPrintCheckOutOrder
    {
        public string waiterName { get; set; }
        public string orderID { get; set; }
        public string peopleNumber { get; set; }
        public string tableName { get; set; }
        public double totalToPay { get; set; }
        public double alreadyPaid { get; set; }//xiaoyu 2012-04-02 账单中增加已支付金额
        public double removeChange { get; set; }//xiaoyu 2012-04-02 账单中增加抹零金额
        public double disCount { get; set; }
        public string printerName { get; set; }//xiaoyu 2011-12-30 打印账单时按照系统设置的对应打印机名称进行打印
        public string serviceCharge { get; set; }//xiaoyu 2012-02-29 打印账单时增加服务费
        public List<BillOrderDetail> orderList { get; set; }
        public List<BillOrderType> typeList { get; set; }
        public string openTableTime { get; set; }
        public string tableAreaName { get; set; }
    }
    public struct BillOrderDetail
    {
        public string dishName { get; set; }//包括菜名和规格名
        public double dishQuantity { get; set; }
        public double dishPrice { get; set; }
        public double dishPriceSum { get; set; }//价格小计（单价乘以数量）
    }
    public struct BillOrderType
    {
        public string typeName { get; set; }
        public double typePriceSum { get; set; }
    }
    /// <summary>
    /// 分类结算单打印信息
    /// </summary>
    public class VAPrintDiscountTypeOrder
    {
        public string waiterName { get; set; }
        public int orderID { get; set; }
        public string tableName { get; set; }
        public double totalToPay { get; set; }
        public string printerName { get; set; }
        public string discountTypeName { get; set; }
        public short printCopy { get; set; }
        public List<BillOrderDetail> orderList { get; set; }
    }

    public class VAPrintDishSales
    {
        public string printerName { get; set; }
        public string startTime { get; set; }
        public string endTime { get; set; }
        public string typeName { get; set; }
        public List<DishSalesDetail> dishSalesList { get; set; }
    }
    public struct DishSalesDetail
    {
        public string id { get; set; }
        public string dishName { get; set; }
        public string dishQuantity { get; set; }
    }
    /// <summary>
    /// 支付信息统计处小票打印信息
    /// </summary>
    public class VAPrintPayList
    {
        public string printerName { get; set; }
        public string startTime { get; set; }
        public string endTime { get; set; }
        public double sum { get; set; }
        public List<PaymentPrint> payList { get; set; }
    }
    public struct PaymentPrint
    {
        public string paymentName { get; set; }
        public double payQuantity { get; set; }
    }
    /// <summary>
    /// 查询点单支付信息
    /// </summary>
    public class VAQueryOrderPayResponse : VAResponse
    {
        public double alreadyPaid { get; set; }
        public double needToPay { get; set; }
        public List<OrderPay> orderPayList { get; set; }
    }
    public struct OrderPay
    {
        public int orderPayID { get; set; }
        public int orderID { get; set; }//2011年12月28日 tdq 添加
        public int paymentID { get; set; }
        public string paymentName { get; set; }
        public DateTime orderPayTime { get; set; }
        public double amount { get; set; }
        public string cityLedgerCompanyName { get; set; }//2012-02-27 xiaoyu
        public string cityLedgerCustomerName { get; set; }//2012-02-27 xiaoyu
    }
    /// <summary>
    /// 查询所有点单支付信息
    /// </summary>
    public class VAAllOrderPay
    {
        public DateTime startTime { get; set; }
        public DateTime endTime { get; set; }
        public int paymentID { get; set; }
    }
    public class VAAllOrderPayResponse : VAResponse
    {
        public double sum { get; set; }
        public OrderPay[] orderPayList { get; set; }
    }
    /// <summary>
    /// 查询所有点单信息
    /// </summary>
    public class VAAllOrder
    {
        public DateTime startTime { get; set; }
        public DateTime endTime { get; set; }
        public VAOrderStatus orderStatus { get; set; }
    }
    public class VAAllOrderResponse : VAResponse
    {
        public double sum { get; set; }
        public List<OrderInfo> orderList { get; set; }
    }
    /// <summary>
    /// 快速结账
    /// </summary>
    public class VAQuickCheckoutRequest
    {
        public int employeeID { get; set; }
        public int orderID { get; set; }
        public double amount { get; set; }
        public string userName { get; set; }
    }
    public class VAQuickCheckoutResponse : VAResponse
    {

    }
    /// <summary>
    /// 打印账单
    /// </summary>
    public class VAPrintBillOrderRequest
    {
        public int employeeID { get; set; }
        public int orderID { get; set; }
        public string userName { get; set; }
    }
    public class VAPrintBillOrderResponse : VAResponse
    {

    }
    /// <summary>
    /// 整单备注
    /// </summary>
    public class VAOrderNoteRequest : VAEmployeeLoginRequest
    {
        public int orderID { get; set; }
        public string note { get; set; }
    }
    public class VAOrderNoteResponse : VAResponse
    {

    }
    #endregion

    #region 菜相关
    /// <summary>
    /// 菜显示分类信息
    /// </summary>
    public class VADishType
    {
        public int menuID { get; set; }
        public int dishTypeSequence { get; set; }
        public int langID { get; set; }
        public int dishTypeID { get; set; }
        public string dishTypeName { get; set; }
        public int dishTypeI18nID { get; set; }
    }
    /// <summary>
    /// 菜基本信息（用于Browser端维护菜）
    /// </summary>
    public class VADish
    {
        public string dishName { get; set; }
        public string dishDescShort { get; set; }
        public string dishDescDetail { get; set; }
        public int dishID { get; set; }
        public int langID { get; set; }
        public int dishI18nID { get; set; }
        public int discountTypeID { get; set; }
        public int menuID { get; set; }
        public int dishDisplaySequence { get; set; }
        public bool sendToKitchen { get; set; }
        public bool isActive { get; set; }
        public int dishTotalQuantity { get; set; }
        public int dishConnTypeID { get; set; }
        public ArrayList DishTypeID { get; set; }
        public string dishQuanPin { get; set; }
        public string dishJianPin { get; set; }
        public string cookPrinterName { get; set; }
    }
    /// <summary>
    /// 菜价管理信息（用于Browser端维护菜价）
    /// </summary>
    public class VADishPrice
    {
        public string scaleName { get; set; }
        public double dishPrice { get; set; }
        public int dishID { get; set; }
        public int dishPriceID { get; set; }
        public int langID { get; set; }
        public int dishPriceI18nID { get; set; }
        public bool DishSoldout { get; set; }
        public bool dishNeedWeigh { get; set; }
        public bool vipDiscountable { get; set; }
        //2013-8-28 wangcheng
        public string markName { get; set; }

        public bool backDiscountable { get; set; }
    }
    /// <summary>
    /// 菜谱信息（用于Browser端维护菜价）
    /// </summary>
    public class VAMenu
    {
        public string menuName { get; set; }
        public string menuDesc { get; set; }
        public int menuSequence { get; set; }
        public int langID { get; set; }
        public int menuID { get; set; }
        public int menuI18nID { get; set; }
        public string menuImagePath { get; set; }
    }
    #endregion

    #region 菜谱结构for Andriod
    /// <summary>
    /// 提供给Android的菜单的最外层结构
    /// </summary>
    public class VAMenuInfoForAndroid
    {
        public string version { get; set; }
        public List<string> languages { get; set; }
        public List<VAMenuForAndroid> menus { get; set; }
    }
    /// <summary>
    /// 提供给Android的菜单的主题结构
    /// </summary>
    public class VAMenuForAndroid
    {
        public int menuId { get; set; }
        public string menuIcon { get; set; }//预留
        public Dictionary<string, string> menuDesc { get; set; }//菜谱描述
        public Dictionary<string, string> menuName { get; set; }//菜谱名称
        public List<VAMenuTypeForAndroid> typeList { get; set; }//菜谱分类



    }
    /// <summary>
    /// 口味
    /// </summary>
    public class VADishTaste
    {
        public int tasteId { get; set; }//口味ID
        public string tasteName { get; set; }//口味名称
    }
    /// <summary>
    /// 配料
    /// </summary>
    public class VADishIngredients
    {
        public int ingredientsId { get; set; }//配料ID
        public string ingredientsName { get; set; }//配料名称
        public double ingredientsPrice { get; set; }//配料价格
        public int quantity { get; set; }//该项数量
        public string vipDiscountable { get; set; }
    }
    /// <summary>
    /// 提供给Android的菜单的分类结构
    /// </summary>
    public class VAMenuTypeForAndroid
    {
        public List<VAMenuDishForAndroid> dishList { get; set; }//菜
        public Dictionary<string, string> dishTypeName { get; set; }//菜分类名称
    }
    /// <summary>
    /// 提供给Android的菜单的菜的结构
    /// </summary>
    public class VAMenuDishForAndroid
    {
        public int dishId { get; set; }
        public int dishStatus { get; set; }
        public double soldCount { get; set; }//菜的销量
        public string dishThumbnail { get; set; }//菜小图URL
        public Dictionary<string, string> dishDescDetail { get; set; }//菜详细说明
        public Dictionary<string, string> dishDescShort { get; set; }//菜简要说明
        public Dictionary<string, string> dishName { get; set; }//菜详细说明
        public Dictionary<string, string> dishQuanPin { get; set; }//菜全拼
        public Dictionary<string, string> dishJianPin { get; set; }//菜简拼
        public List<string> dishImages { get; set; }//菜的大图URL，暂时服务器上每个菜都只有一个大图20130520
        public string dishVideos { get; set; }//菜的视频，预留
        public List<VAMenuDishPriceForAndroid> dishPrices { get; set; }//菜的价格信息

    }
    /// <summary>
    /// 提供给Android的菜单的菜规格的结构
    /// </summary>
    public class VAMenuDishPriceForAndroid
    {
        public double dishPrice { get; set; }//菜价格
        public int dishPriceId { get; set; }//菜规格编号
        public string dishSoldout { get; set; }//菜是否售罄（True/False）
        public string dishNeedWeigh { get; set; }//菜是否需要称重（True/False）
        public string vipDiscountable { get; set; }//菜是否享受Vip折扣（True/False）
        public Dictionary<string, string> scaleName { get; set; }//菜规格名称
        public int dishIngredientsMinAmount { get; set; }//配料限定最小值
        public int dishIngredientsMaxAmount { get; set; }//配料限定最大值

        public List<VADishTaste> dishTasteList { get; set; }//口味列表
        public List<VADishIngredients> dishIngredientsList { get; set; }//配料列表
    }
    #endregion

    #region 权限相关
    public struct VAEmployeeRole
    {
        public int employeeRoleID { get; set; }
        public string roleName { get; set; }
    }
    public struct VARoleAuthority
    {
        public int roleAuthorityID { get; set; }
        public int authorityID { get; set; }
        public string authorityName { get; set; }
    }
    /// <summary>
    /// 员工登录信息
    /// </summary>
    public class VAEmployeeLogin
    {
        public string userName { get; set; }
        public string password { get; set; }
    }
    public class VAEmployeeLoginResponse : VAResponse
    {
        public int employeeID { get; set; }
        public string statusGUID { get; set; }
        public string userName { get; set; }
        //public bool isCashierAuthority { get; set; }//查看当前用户是否具备掌中宝操作点单特殊权限
        //public int canQuickCheckout { get; set; }
        //public int canClearTable { get; set; }
        //public int canWeigh { get; set; }
        //public string daysLeft { get; set; }
        public List<VAEmployeeShop> employeeShop { get; set; }
        public bool isViewAllocWorker { get; set; }
    }
    /// <summary>
    /// 员工注销信息
    /// </summary>
    public class VAEmployeeLogout : VAEmployeeLoginRequest
    {

    }
    public class VAEmployeeLogoutResponse : VAResponse
    {

    }
    /// <summary>
    /// 用户登录信息
    /// </summary>
    public class VACustomerLogin
    {
        public string userName { get; set; }
        public string password { get; set; }
        public int orderID { get; set; }
    }
    public class VACustomerLoginResponse : VAResponse
    {
        public string customerFirstName { get; set; }
        public string customerLastName { get; set; }
        public DateTime customerBirthday { get; set; }
        public string customerSex { get; set; }
        public string customerPhone { get; set; }
        public string customerAddress { get; set; }
        public string customerNote { get; set; }
        public string customerRank { get; set; }
        public string welcome1 { get; set; }
        public string welcome2 { get; set; }
        public string welcome3 { get; set; }
        public string welcome4 { get; set; }
    }
    [Serializable]
    [DataContract]
    public struct VAEmployeeShop
    {
        [DataMember]
        public int shopID { get; set; }
        [DataMember]
        public string shopName { get; set; }
        [DataMember]
        public string shopImagePath { get; set; }
        //2013-7-18
        //wangcheng
        [DataMember]
        public int isHandle { get; set; }
        [DataMember]
        public string companyName { get; set; }//公司名称
        [DataMember]
        public int cityId { get; set; }
    }
    public struct VAEmployeeShopJson
    {
        public int shopID { get; set; }
        public string shopName { get; set; }
    }

    public struct VACouponShopForWeb
    {
        public int shopID { get; set; }
        public string shopName { get; set; }
    }
    public struct VAEmployeeCompany
    {
        public int companyID { get; set; }
        public string companyName { get; set; }
    }
    public struct VAEmployeeCompanyJson
    {
        public int ID { get; set; }
        public string CN { get; set; }
        //public string companyLogo { get; set; }
    }
    public struct VAEmployeeCompanyID
    {
        public int companyID { get; set; }
    }
    #endregion

    #region App用户相关
    /// <summary>
    /// App消息公用消息
    /// </summary>
    public class VANetworkMessage
    {
        public VAMessageType type { get; set; }   // 请求的时候同时赋值检测用
        public VAResult result { get; set; }// 回复的时候填写，请求中此项为空
        public string uuid { get; set; }// UUID;
        [UserIntegralParams("userId")]
        public string cookie { get; set; }// 登录用户证书
        public int cityId { get; set; }
        //20140604 wangc 客户端当前版本号
        public string clientBuild { get; set; }
        public VAAppType appType { get; set; }//悠先点菜设备类别
        public VAServiceType serviceType { get; set; }//悠先服务设备类别

        /// <summary>
        /// 返回给客户端消息
        /// </summary>
        public string message { get; set; }
    }
    /// <summary>
    /// App用户信息
    /// </summary>
    public class VAUserInfo
    {
        public string displayName { get; set; }  // 昵称
        public string mobilePhone { get; set; }  // 手机
        public int validPreorderCount { get; set; } // 当前有效预点单数量 2013-05-03 xiaoyu
        public double walletCash { get; set; }//用户余额2012-10-18
        public string eCardNumber { get; set; }//电子会员卡号
        public string personalImgInfo { get; set; }
        public int customerSex { get; set; }
    }
    /// <summary>
    /// Cookie和type检查返回信息
    /// </summary>
    public class CheckCookieAndMsgtypeInfo
    {
        public VAResult result { get; set; }
        public DataTable dtCustomer { get; set; }
    }
    /// <summary>
    /// Cookie和type检查返回信息
    /// 悠先服务端
    /// </summary>
    public class CheckCookieAndMsgForZZZ
    {
        public VAResult result { get; set; }
        public DataTable dtEmployee { get; set; }
    }
    /// <summary>
    /// App设备激活
    /// </summary>
    public class VAClientRegisterRequest : VANetworkMessage
    {
        public string pushToken { get; set; }//设备推送码，可为空
        public string wechatId { get; set; }//微信号码，可为空
    }
    public class VAClientRegisterResponse : VANetworkMessage
    {
        public string eCardNumber { get; set; }//20130507 xiaoyu
    }
    /// <summary>
    /// App设备证书登录
    /// </summary>
    public class VAClientCookieLoginRequest : VANetworkMessage
    {
        public string pushToken { get; set; }//设备推送码，可为空
        public string screenWith { get; set; }//客户端设备的屏幕宽度
    }
    public class VAClientCookieLoginResponse : VANetworkMessage
    {
        public VAUserInfo userInfo;
        public string latestBuild { get; set; }// 新版本号
        public string latestUpdateDescription { get; set; }// 新功能简介，不超过50个字
        public string latestUpdateUrl { get; set; }// 更新版本的url
        public bool forceUpdate { get; set; }//客户端是否需要强制用户更新

        public List<VAClientImageSize> clientImageSize;//图片处理参数
        public List<string> startMapUrlList { get; set; }
        public string dishSortAlgorithmBase { get; set; }//点菜客户端根据点赞数和销量计算排序基数（点赞基数,销量基数）
    }
    public class VAClientImageSize
    {
        public VAImageType imageType;//{ get; set; }//图片类别
        public string value { get; set; }//图片处理参数
    }
    /// <summary>
    /// App用户更新用户信息
    /// </summary>
    public class VAClientUpdateUserInfoRequest : VANetworkMessage
    {
        public VAUserInfo userInfo;
    }
    public class VAClientUpdateUserInfoResponse : VANetworkMessage
    {

    }
    /// <summary>
    /// App用户更新用户信息new
    /// 20140126 xiaoyu
    /// 暂时只修改昵称和性别
    /// </summary>
    public class VAClientUpdateUserInfoNewRequest : VANetworkMessage
    {
        public int customerSex;//性别 0：不详，1：男，2：女
        public string displayName { get; set; }  // 昵称
        public string personalImgInfo { get; set; }//个人图片信息

        public int defaultPayment { get; set; }//默认支付方式

        /// <summary>
        /// 是否绑定微信(0不绑,1绑定)
        /// </summary>
        public int bindingWeChat { set; get; }

        /// <summary>
        /// 用户OpenId
        /// </summary>
        public string openId { set; get; }

        /// <summary>
        /// 用户统一标识。针对一个微信开放平台帐号下的应用，同一用户的unionid是唯一的。
        /// </summary>
        public string unionId { set; get; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string nickName { set; get; }

        /// <summary>
        /// 1为男性，2为女性
        /// </summary>
        public int sex { set; get; }

        /// <summary>
        /// 省份
        /// </summary>
        public string province { set; get; }

        /// <summary>
        /// 城市
        /// </summary>
        public string city { set; get; }

        /// <summary>
        /// 国家，如中国为CN
        /// </summary>
        public string country { set; get; }

        /// <summary>
        /// 用户头像
        /// </summary>
        public string headImg { set; get; }

        /// <summary>
        /// 微信权限
        /// </summary>
        public string privilege { set; get; }

    }
    public class VAClientUpdateUserInfoNewResponse : VANetworkMessage
    {
        /// <summary>
        /// 0原来未绑定,1原来已绑定
        /// </summary>
        public int isExists { set; get; }
    }
    /// <summary>
    /// App用户手机认证
    /// </summary>
    public class VAClientMobileVerifyRequest : VANetworkMessage
    {
        public string mobilePhoneNumber { get; set; }
        public string verificationCode { get; set; }
        public string passwordMD5 { get; set; }
        public bool resetPassword { get; set; }
    }
    public class VAClientMobileVerifyResponse : VANetworkMessage
    {

    }
    /// <summary>
    /// App用户手机认证new
    /// 20140126 xiaoyu
    /// </summary>
    public class VAClientMobileVerifyNewRequest : VANetworkMessage
    {
        public string mobilePhoneNumber { get; set; }
        public string verificationCode { get; set; }
        public string pushToken { get; set; }
        public int shopId { get; set; }//在点菜页面登录时带此编号用以查询用户在该店铺的折扣
        public int customerSex;//性别 0：不详，1：男，2：女
        public string displayName { get; set; }  // 昵称
        public string personalImgInfo { get; set; }//个人图片信息
        /// <summary>
        /// 是否需要验证码(0不要,1要)
        /// </summary>
        public int noCode { set; get; }

        /// <summary>
        /// 是否绑定微信(0不绑,1绑定)
        /// </summary>
        public int bindingWeChat { set; get; }

        /// <summary>
        /// 用户OpenId
        /// </summary>
        public string openId { set; get; }

        /// <summary>
        /// 用户统一标识。针对一个微信开放平台帐号下的应用，同一用户的unionid是唯一的。
        /// </summary>
        public string unionId { set; get; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string nickName { set; get; }

        /// <summary>
        /// 1为男性，2为女性
        /// </summary>
        public int sex { set; get; }

        /// <summary>
        /// 省份
        /// </summary>
        public string province { set; get; }

        /// <summary>
        /// 城市
        /// </summary>
        public string city { set; get; }

        /// <summary>
        /// 国家，如中国为CN
        /// </summary>
        public string country { set; get; }

        /// <summary>
        /// 用户头像
        /// </summary>
        public string headImg { set; get; }

        /// <summary>
        /// 微信权限
        /// </summary>
        public string privilege { set; get; }

    }
    public class VAClientMobileVerifyNewResponse : VANetworkMessage
    {
        public VAUserInfo userInfo;
        public double userCurrentShopDiscount { get; set; }//如果request中上传了店铺编号，则返回对应的该店铺的折扣值
        public bool isNewMobile { get; set; }
        public double rationBalance { get; set; }
        public double executedRedEnvelopeAmount { get; set; }
        public List<OrderPaymentCouponDetail> couponDetails { get; set; }//可参与支付抵扣金额优惠券列表
        public string deviceLockMessage { get; set; }//设备封锁信息
        /// <summary>
        /// 
        /// </summary>
        public int isExists { set; get; }

        /// <summary>
        /// 微信id
        /// </summary>
        public string unionId { set; get; }

        /// <summary>
        /// 验证码位数
        /// </summary>
        public int verificationCodeDigit { set; get; }
    }
    /// <summary>
    /// App用户手机登录
    /// </summary>
    public class VAClientMobileLoginRequest : VANetworkMessage
    {
        public string mobilePhoneNumber { get; set; }
        //public string verificationCode { get; set; }
        public string pushToken { get; set; }
        public string passwordMD5 { get; set; }
    }
    public class VAClientMobileLoginResponse : VANetworkMessage
    {
        public VAUserInfo userInfo;
    }
    /// <summary>
    /// App用户手机登录new
    /// 20140126 xiaoyu手机登录修改为每次都发送验证码登录
    /// </summary>
    public class VAClientMobileLoginNewRequest : VANetworkMessage
    {
        public string mobilePhoneNumber { get; set; }
        public string verificationCode { get; set; }//第一次为空表示请求验证码
        public string pushToken { get; set; }
    }
    public class VAClientMobileLoginNewResponse : VANetworkMessage
    {
        public VAUserInfo userInfo;
    }
    /// <summary>
    /// App用户手机号码更改
    /// </summary>
    public class VAClientMobileModifyRequest : VAClientMobileVerifyRequest
    {
        //public string mobilePhoneNumber { get; set; }//新手机号码
        //public string passwordMD5 { get; set; }//原来的密码
        //public string verificationCode { get; set; }//验证码，第一次为空
    }
    public class VAClientMobileModifyResponse : VANetworkMessage
    {

    }
    /// <summary>
    /// App用户更改密码
    /// </summary>
    public class VAClientModifyPasswordRequest : VANetworkMessage
    {
        public string oldPasswordMD5 { get; set; }//原密码
        public string passwordMD5 { get; set; }//新密码
    }
    public class VAClientModifyPasswordResponse : VANetworkMessage
    {

    }
    /// <summary>
    /// App用户刷新用户优惠券
    /// </summary>
    public class VAUserCouponListRequest : VANetworkMessage
    {
        public VAUserCouponType couponType { get; set; }//不用
    }
    public class VAUserCouponListResponse : VANetworkMessage
    {
        public List<VACustomerCouponDetail> couponList { get; set; }
        public VAUserCouponType couponType { get; set; }//废除不用移动到VACustomerCouponDetail里面
    }
    /// <summary>
    /// App用户忘记密码
    /// </summary>
    public class VAClientForgetPasswordRequest : VANetworkMessage
    {
        public string mobilePhone { get; set; }
    }
    public class VAClientForgetPasswordResponse : VANetworkMessage
    {

    }
    /// <summary>
    /// 用户执行忘记密码后根据发送邮件的验证码修改密码
    /// 暂时只用于Web端
    /// </summary>
    public class VAClientResetPasswordRequest : VANetworkMessage
    {
        public string veriryCode { get; set; }
        public string myNewPasswordMD5 { get; set; }//新密码
    }
    public class VAClientResetPasswordResponse : VANetworkMessage
    {

    }
    /// <summary>
    /// 用户挂失
    /// </summary>
    public class VAClientCustomerReportLossRequest : VANetworkMessage
    {
        public string mobilePhone { get; set; }
        public string passwordMD5 { get; set; }
    }
    public class VAClientCustomerReportLossResponse : VANetworkMessage
    {

    }
    /// <summary>
    /// 省份信息
    /// </summary>
    public class VAStateInfo
    {
        public string stateName { get; set; }
        public List<VACityInfo> onlineCityList { get; set; }
        public List<VACityInfo> offlineCityList { get; set; }
        public bool isOnline { get; set; }
    }
    /// <summary>
    /// 城市信息
    /// </summary>
    public class VACityInfo
    {
        public string cityName { get; set; }
        public int cityId { get; set; }
        public int restaurantCount { get; set; }        //签约门店数
        public int requestCount { get; set; }           //求开通次数
        public double cityCenterLongitude { get; set; }    //经度
        public double cityCenterLatitude { get; set; }     //纬度
        public bool isOnline { get; set; }
    }
    /// <summary>
    /// 查询所有城市
    /// </summary>
    public class VACityListRequest : VANetworkMessage
    {

    }
    public class VACityListResponse : VANetworkMessage
    {
        public List<VAStateInfo> stateList { get; set; }
    }
    /// <summary>
    /// 求开通城市
    /// </summary>
    public class VACityOpenningRequest : VANetworkMessage
    {
    }
    public class VACityOpenningResponse : VANetworkMessage
    {
        public VACityInfo cityInfo { get; set; }
    }
    /// <summary>
    /// 查询优惠活动
    /// </summary>
    public class VACouponSearchWithOptionRequest : VANetworkMessage
    {
        public double userLongitude { get; set; }
        public double userLatitude { get; set; }
        public VACouponSearchSortOption searchOption { get; set; }
        public int existingCount { get; set; }
        public int requestCount { get; set; }
        public int shopId { get; set; }//如果不是按照店铺查询时留空
        public int companyId { get; set; }//如果不是按照公司查询时留空，如果shopId和companyId都不为空时按照shopId查询
        public bool downloadBanner { get; set; }//20130410 xiaoyu 是否下载优惠券Banner广告
    }
    public class VACouponSearchWithOptionResponse : VANetworkMessage
    {
        public List<VACouponDetail> couponList { get; set; }
        public VACouponSearchSortOption searchOption { get; set; }
        public bool haveMoreCoupon { get; set; }
        public int existingCount { get; set; }//2012-10-18增加，以便客户端识别是第一次还是后续的
        public List<VACouponBanner> couponBannerList { get; set; }//20130410 xiaoyu 优惠券广告
    }
    public class VACouponDetail
    {
        public string campaignId { get; set; }     // 字符串：优惠活动的ID，不是具体的券ID
        public VACouponType campaignType { get; set; }   // 数字：活动类型, ref: VACampaignType
        public string name { get; set; }           // 字符串：活动名，例如："[首尔烧烤/焖鲜汇优惠券]每满百抵20"
        public string thumbnailImageUrl { get; set; } // 字符串：缩略图完整url
        public List<VADishItem> dishItems { get; set; }       // 数组：菜的信息，ref: VADishItem
        public List<string> detailImageUrls { get; set; }
        public string description { get; set; }    // 字符串：内容描述全文
        public VACouponRequirementType requirementType { get; set; }// 数字：VACouponRequirementType
        public List<VARestaurant> applicableRestaurants { get; set; } // 数组：适用商户信息数组 ref: VARestaurant
        public double visibleStartDate { get; set; }  // 数字：上线展示开始日期
        public double visibleEndDate { get; set; }    // 数字：上线展示结束日期
        public double usableStartDate { get; set; }   // 数字：兑换开始日期
        public double usableEndDate { get; set; }     // 数字：兑换结束日期
        public int limitedTotal { get; set; }    // 数字：限量优惠券的总数（可以为空），如：5，代表共5个
        public int limitedLeft { get; set; }     // 数字：限量优惠券的余量（可以为空），如：2，代表还剩2个
        public bool isValid { get; set; }              // 布尔：是否有效（软删除）
        public double originalPrice { get; set; }   // 原价（服务器提供）
        public double currentPrice { get; set; }    // 现价（服务器提供）
        public double discountedAmount { get; set; }// 优惠力度（服务器提供）
        public double discountPercentage { get; set; }// 折扣比例（0-10浮点）
        public bool isVIPOnly { get; set; }            // 会员独享优惠券
        public double couponPrice { get; set; }     // 优惠券下载所需支付的费用
        public bool canDownloadOnlyOnce { get; set; }//该优惠券是否只能下载一次
        public int downloadedCount { get; set; }
        public int viewedCount { get; set; }
        public string prompt { get; set; }//特别说明
    }
    public class VACustomerCouponDetail : VACouponDetail
    {
        public VACustomerCouponStatus customerCouponStatus { get; set; }
        public int limitedSerial { get; set; }// 数字：限量优惠券的序号（可以为空），如：3，代表第3个
        public int purchasedId { get; set; }      // 购买后生成的优惠券实例的ID
        public string verificationCode { get; set; }

        public VAUserCouponType customerCouponType { get; set; }
    }
    public class VACouponBanner : VACouponDetail
    {
        public string bannerImageUrlString { get; set; }
    }
    public class VADishItem
    {
        public int dishId { get; set; }
        public string dishImageFileUrl { get; set; }
        public string dishThumbnailUrl { get; set; }
        public double dishPrice { get; set; }
        public double dishVideo { get; set; }
        public string dishName { get; set; }
        public string dishDescDetail { get; set; }
        public string dishDescShort { get; set; }
        public int dishStatus { get; set; }
        public int dishPrice18nId { get; set; }
        public int dishQuantity { get; set; }//菜数量
    }
    public class VARestaurant
    {
        public string name { get; set; }
        public double longitude { get; set; }
        public double latitude { get; set; }
        public string country { get; set; }
        public string state { get; set; }
        public string city { get; set; }
        public string street { get; set; }
        public string address { get; set; }
        public string phone { get; set; }
        public string description { get; set; }
        public string thumbnailImageUrl { get; set; }
        public List<string> detailImageUrls { get; set; }
        public List<VAMenuForApp> menuList { get; set; }
        public int restaurantId { get; set; }
        public string dianPingLink { get; set; }
        public int companyId { get; set; }
        public bool supportPrePayCashBack { get; set; }
        public bool supportPrePayVIPEntrance { get; set; }
        public bool supportPrePayGift { get; set; }
        public List<VAOrderPrepayPolicy> prepayPolicies { get; set; }

        //20130815 add xiaoyu
        public List<VAMedalInfo> shopMedal { get; set; }
        public string openingTime { get; set; }

        public string queueFreeDesc { get; set; }
        public string couponBackDesc { get; set; }
        public string dishGiftDesc { get; set; }

        public List<VASundryInfo> sundryInfo { get; set; }//杂项
    }
    public class VAMenuForApp
    {
        public int menuId { get; set; }
        public string menuName { get; set; }
        public string menuUrl { get; set; }
    }
    /// <summary>
    /// 杂项
    /// </summary>
    public class VASundryInfo
    {
        public int sundryId { get; set; }//杂项编号
        public string sundryName { get; set; }
        public string sundryStandard { get; set; }//杂项规格
        public int sundryChargeMode { get; set; }//杂项收费模式:1固定金额,2按比例收取,3按人次
        public bool supportChangeQuantity { get; set; }//
        public double price { get; set; }//杂项单价
        public int quantity { get; set; }//该项数量
        public bool vipDiscountable { get; set; }//支持折扣
        public bool backDiscountable { get; set; }//享受返送
        public bool required { get; set; }//是否必选
    }
    /// <summary>
    /// 购买领取优惠券
    /// </summary>
    public class VACouponDownloadRequest : VANetworkMessage
    {
        // public string campaignId { get; set; }
        // public bool systemOperationAfterPay { get; set; }//是否是支付后系统执行的标志位
        public long activitiesId { get; set; }//活动id
        public long customerCouponOrderId { get; set; }//系统支付后执行时传入该编号以修改该订单状态
        public int preorderPayMode { get; set; }//默认支付方式
        public int companyId { get; set; }
    }
    public class VACouponDownloadResponse : VANetworkMessage
    {
        // public string verificationCode { get; set; }
        // public long customerCouponOrderId { get; set; }
        // public string alipayURL { get; set; }
        public string urlToContinuePayment { get; set; }
        public string alipayOrder { get; set; }
        public string unionpayOrder { get; set; }
        public int couponOperateStatus { get; set; }// 0就是 我要领取   1，2，3分别是支付宝客户端  支付宝网页  银联支付
        public long activitiesId { get; set; }//活动id
    }
    /// <summary>
    /// 绑定open id
    /// </summary>
    public class VAClientBindOpenIdRequest : VANetworkMessage
    {
        public VAOpenIdVendor vendor { get; set; }
        public string openIdUid { get; set; }
        public double expirationDate { get; set; }
    }
    public class VAClientBindOpenIdResponse : VANetworkMessage
    {

    }
    /// <summary>
    /// 客户端使用OpenId登录
    /// </summary>
    public class VAClientOpenIdLoginRequest : VANetworkMessage
    {
        public string pushToken { get; set; }
        public VAOpenIdVendor vendor { get; set; }
        public string openIdSession { get; set; }
        public string openIdUid { get; set; }
    }
    public class VAClientOpenIdLoginResponse : VANetworkMessage
    {
        public VAUserInfo userInfo;
    }
    /// <summary>
    /// 店家验证与消费优惠券(web和POSLite)
    /// </summary>
    public class VACouponVerifyRequest
    {
        public int shopId { get; set; }
        public string verificationCode { get; set; }
        public bool whetherToUse { get; set; }
    }
    public class VACouponVerifyResponse
    {
        public VAResult result { get; set; }
        public List<VACustomerCouponDetail> customerCouponList { get; set; }//取list中的第一个即可
        public string currentTime { get; set; }
    }
    /// <summary>
    /// 查询优惠券适用门店
    /// </summary>
    public class VAQueryCouponShopRequest
    {
        public string userName { get; set; }
        public long couponID { get; set; }
    }
    public class VACouponShopForApp
    {
        public int shopID { get; set; }
        public string shopName { get; set; }
        public string shopAddress { get; set; }
        public string shopTelephone { get; set; }
        public string shopImageURL { get; set; }
    }
    public class VAQueryCouponShopResponse : VAResponse
    {
        public long couponID { get; set; }
        public List<VACouponShopForApp> couponShop { get; set; }
    }
    public class VACouponCollectRequest
    {
        public string userName { get; set; }
        public long couponID { get; set; }
    }

    //USER_WALLET_TRANSACTION_LIST_REQUEST

    /// <summary>
    /// 用户查询余额以及变动信息
    /// </summary>
    public class VAUserWalletTransactionListRequest : VANetworkMessage
    {

    }
    public class VAUserWalletHistoryRecord
    {
        public double date { get; set; }
        public double value { get; set; }
        public string reason { get; set; }
    }
    public class VAUserWalletTransactionListResponse : VANetworkMessage
    {
        public double current { get; set; }
        public List<VAUserWalletHistoryRecord> historyRecords { get; set; }
    }
    /// <summary>
    /// 通过cityID查所有公司
    /// </summary>
    public class VACompanyListRequest : VANetworkMessage
    {
        public int companyId { get; set; }//wap端使用
    }
    public class VACompanyListResponse : VACompanyListRequest
    {
        public List<VABrand> companyList { get; set; }
        public List<VABrandBanner> companyBannerList { get; set; }
    }
    public class VABrandBaseInfo
    {
        public int companyId { get; set; }
        public string name { get; set; }
        public string logoUrlString { get; set; }
        public string description { get; set; }
        public string defaultMenuUrl { get; set; }
        public bool isFavorite { get; set; }
        public bool supportPrePayCashBack { get; set; }
        public bool supportPrePayVIPEntrance { get; set; }
        public bool supportPrePayGift { get; set; }
        public List<VARestaurant> restaurantList { get; set; }
        public long numberOfPreorders { get; set; }//手机点餐次数
        public long numberOfPrepays { get; set; }//手机支付次数
        public double acpp { get; set; }//Average Cost Per Person 人均消费

        //20130815 add xiaoyu
        public List<VAMedalInfo> companyMedal { get; set; }
    }

    public class VAMedalInfo
    {
        public string name { get; set; }
        public string imageURL { get; set; }
        public string medalDescription { get; set; }
        public string smallImageURL { get; set; }
    }
    public class VABrand : VABrandBaseInfo
    {
        //增加Vip政策相关,暂时还未优化BrandList中的多余信息
        public List<VAVipPolicy> vipPolicies { get; set; }
        public VAVipPolicy userPolicy { get; set; }
        public int userCompletedOrderCount { get; set; }//用户在该品牌的累计消费次数
    }

    public class VABrandBanner : VABrandBaseInfo
    {
        public string bannerImageUrlString { get; set; }

        //20130815 add xiaoyu
        public List<int> shopId { get; set; }
        public string bannerName { get; set; }
        public string bannerDescript { get; set; }
        public int bannerType { get; set; }//广告分类
    }
    /// <summary>
    /// 添加、删除收藏
    /// </summary>
    public class VAUserSetFavoriteCompanyRequest : VANetworkMessage
    {
        public int companyId { get; set; }
        public bool isFavorite { get; set; }
    }
    public class VAUserSetFavoriteCompanyResponse : VAUserSetFavoriteCompanyRequest
    {

    }
    /// <summary>
    /// 通过公司id和cityid查门店
    /// </summary>
    public class VARestaurantListByCompanyRequest : VANetworkMessage
    {
        public int companyId { get; set; }
    }
    public class VARestaurantListByCompanyResponse : VARestaurantListByCompanyRequest
    {
        public List<VARestaurant> companyList { get; set; }
    }
    /// <summary>
    /// 客户端邀请用户
    /// </summary>
    public class VAClientInviteCustomerRequest : VANetworkMessage
    {
        public string mobilePhoneInvited { get; set; }
    }
    public class VAClientInviteCustomerResponse : VANetworkMessage
    {

    }
    /// <summary>
    /// 客户端增加预点单
    /// </summary>
    public class VAClientPreOrderAddRequest : VANetworkMessage
    {
        public int menuId { get; set; }//早期只传菜谱编号，增加companyId后该参数目前只记录，不参与处理
        public string orderInJson { get; set; }
        public int companyId { get; set; }
        public int shopId { get; set; }//必须传
        public double clientCalculatedSum { get; set; }
        public long preorderId { get; set; }//如果不传或者为0则为新增，否则为修改原来的点单
        public string snsShareImageUrl { get; set; }//存储微博分享的图片的Url
        public int isAddbyList { get; set; } //是否列表传过来 1：不是2：是
        public List<long> couponList { get; set; }

        //public int sundryNumofPeople { get; set; }//杂项统计人数
        public List<VASundryInfo> sundryList { get; set; }
    }

    /// <summary>
    /// 个人优惠卷使用
    /// </summary>
    public class VACouponStatic
    {
        public long customerConnCouponID { get; set; }
        public long CouponId { get; set; }
        public string CouponName { get; set; }//优惠卷名称
        public int CouponValue { get; set; }//1不勾选 2勾选
        public double couponUseEndTime { get; set; }//使用截止时间
        public int couponUseTimesBysame { get; set; }//同种优惠劵可使用次数
        public bool ishistoryUse { get; set; }//历史使用，只有在修改已支付点单时候用到
    }
    /// <summary>
    /// 新增修改点单
    /// </summary>
    public class VAClientPreOrderAddResponse : VANetworkMessage
    {
        public long preorderId { get; set; }
        public int menuId { get; set; }
        //public string preorderShareDesc { get; set; }
        public VASNSShareMessage snsShareMessage { get; set; }
        public List<VAOrderPrepayPolicy> prepayPolicies { get; set; }
        public List<VACouponStatic> coupon { get; set; }

        public double youxianPrice { get; set; }//悠先价
        public double orginalPrice { get; set; }//原价

        public string orderInJson { get; set; }
        public double userRemain;//用户当前余额
        public List<HistoryOrder> historyOrder { get; set; }

        public List<SundryInfoResponse> sundryList { get; set; }//已选杂项列表

        public double youxianPaid { get; set; }//已支付给悠先金额（已支付修改点单使用）
        public double youxianNeedPaid { get; set; }//还需支付（已支付修改点单使用）
    }

    public class SundryInfoResponse
    {
        public int sundryId { get; set; }//杂项编号
        public string sundryName { get; set; }//杂项名称
        public double sundryPrice { get; set; }//杂项需支付的价格//总价
        public int quantity { get; set; }
        public double price { get; set; }//单价
        public int sundryChargeMode { get; set; }//杂项收费方式
        public string sundryStandard { get; set; }//杂项规格
    }
    public class HistoryOrder
    {
        public double payDateTime { get; set; }//支付时间
        public double prePaidSum { get; set; }//支付金额
    }
    public class VASNSShareMessage
    {
        public string userPrompt { get; set; }//转发微博好处
        public string sinaWeiboTemplate { get; set; }
        public string qqWeiboTemplate { get; set; }
        public string wxTemplate { get; set; }
    }
    public class VAOrderPrepayPolicy
    {
        public string policyId { get; set; }
        public string policyDescription { get; set; }
        // 服务器每次都返回订单全额预付，未来可以利用此接口显示折扣后金额，此数据为提交支付请求的用的默认金额
        public double paymentAmount { get; set; }
        public double numberOfHoursAhead { get; set; } // 享受此政策所需提前完成预付的小时数，可以大于24小时
    }

    /// <summary>
    /// 客户端查询预点单
    /// </summary>
    public class VAClientPreOrderQueryRequest : VANetworkMessage
    {
        public long preorderId { get; set; }

        /// <summary>
        /// 定单id
        /// </summary>
        public Guid orderId { set; get; }
    }
    public class VAClientPreOrderQueryResponse : VANetworkMessage
    {
        public VAPreorderDetail preorderDetail { get; set; }
    }
    /// <summary>
    /// 点单详细
    /// </summary>
    public class VAPreorderDetail
    {
        public long preorderId { get; set; } //必填
        public string orderInJson { get; set; } // 可选：查询列表接口时不返回此项以节省流量
        public double preorderDate { get; set; } //必填 上传时的时间，接口中用since1970的秒数（整型），反序列化时转成NSDate
        public string restaurantName { get; set; }//可选 通过Id查到当前的名字
        public int restaurantId { get; set; }//可选如果上传的时候有
        public int status { get; set; } //必填
        //增加Vip政策相关信息
        public List<VAVipPolicy> vipPolicies { get; set; }
        public VAVipPolicy userPolicy { get; set; }
        public double verifiedSaving { get; set; }
        public double estimatedSaving { get; set; }
        public List<SundryInfoResponse> sundryList { get; set; }//已选杂项列表
        public int isUsed { get; set; }//表示当前点单是否使用（0，未支付，1，未使用，2，已使用，3，已退款，4，退款中）
        public double usedDateTime { get; set; }
        public double refundMoneySum { get; set; }
        public double detailServerCalculatedSum { get; set; }//原价//直接支付价格
        public double detailServerUxianPriceSum { get; set; }//悠先价
        public double currentDiscount { get; set; }//用户在当前门店的折扣值
        public bool canBeDelete { get; set; }//用户是否可标记删除
        /// <summary>
        /// true:未评价
        /// </summary>
        public bool isNotEvaluated { get; set; }//客户端只显示可以评价却未评价的wangchehng
        public int evaluationValue { get; set; }//当前点单评分 add by wangc 20140321
        public bool isShopConfirmed { get; set; }//店铺是否审核，用来判断用户是否可以对点单进行评价
        public string invoiceTitle { get; set; }//add by wangc 20140320
        public string deskNumber { get; set; }//add by wangc 20140321
        public string snsMessageJson { get; set; }
        public string shopLogoUrl { get; set; }//add by wangc 20140403 门店logo路径
        public string foodDiariesUrl { get; set; }//美食日记URL
        public string foodDiariesAfterShareUrl { get; set; }//美食日记分享后URL
        public List<int> haveSharedType { get; set; }//点单已经分享的类型

        public List<OrderPayMode> payModeList { get; set; }

        public int praisedCount { get; set; }//点赞个数
        public bool isHaveShared { get; set; }//是否分享 
        public string sharedDesc { get; set; }//没有分享过的描述文字也是这个传递

        public double executedRedEnvelopeAmount { get; set; }
        public double rationBalance { get; set; }
        public double stillNeedPaySum { get; set; }

        public string evaluationContent { get; set; }

        public string preOrderExplain { get; set; }//订单注意事项
        public List<OrderDishOtherInfo> orderDishOtherList { get; set; }
        public string complainUrl { get; set; }
        public bool shopIsHandle { get; set; }
        public bool isSupportPayment { get; set; }//门店是否支持支付
        public bool isHaveShareCoupon { get; set; }//当前点单是否已分享优惠券
        public double deductibleAmount { get; set; }//优惠券抵扣金额
        public List<OrderPaymentCouponDetail> couponDetails { get; set; }
        public string shareCouponUrl { get; set; }
        public int shareCouponCount { get; set; }
        public bool isContantExpireRedEnvelope { get; set; }//是否包含过期红包
        public bool isPersonalFullRefund { get; set; }//是否用户自己全额退款
        public string couponShareImage { get; set; }//抵扣券分享图片
        public string couponShareText { get; set; }//抵扣券分享文字
        public string payee { get; set; }//收款人
        public double lastConfirmTime { get; set; }//最后一次入座时间
        public bool couldClientConfirmOrder { get; set; }//客户端是否启用入座功能.true：启用；false：停用

        /// <summary>
        /// 补差价
        /// </summary>
        public double compensatePrice { set; get; }

        /// <summary>
        /// 是否对账
        /// </summary>
        public int isApproved { set; get; }

        public Guid orderId { set; get; }

        public AwardInfo userAwardInfo { get; set; }//用户中奖信息
    }

    public class AwardInfo
    {
        public AwardType awardType { get; set; }//中奖类型
        public string awardDesc { get; set; }//中奖描述
        public string awardShowUrl { get; set; }//奖品展示的URL
        public string awardPushMessage { get; set; }//中奖推送信息
        public string orderInJson { get; set; }//奖品为赠菜时，返回客户端点菜信息
        public List<OrderDishOtherInfo> orderDishOtherList { get; set; }//菜品点赞，菜图URL
    }

    /// <summary>
    /// 简单优惠卷信息
    /// </summary>
    public class VACustomerSimpleCoupon
    {
        public long customerConnCouponID { get; set; }//用户优惠卷ID
        public string couponName { get; set; }//优惠劵名称
    }
    /// <summary>
    /// 客户端查询预点单列表
    /// </summary>
    public class VAClientPreorderListRequest : VANetworkMessage
    {
        public bool isWechatCustomer { get; set; }
        public int pageIndex { get; set; }
        public int pageSize { get; set; }
    }
    public class VAClientPreorderListResponse : VANetworkMessage
    {
        public List<VAPreorderDetail> preorders { get; set; }
        public string eVip { get; set; }//给wap端使用
        public bool isHaveMore { get; set; }
    }
    /// <summary>
    /// 客户端支付预点单
    /// </summary>
    public class VAClientPreorderPrepayWithCreditRequest : VANetworkMessage
    {
        public long preorderId { get; set; }
        public int selectedPolicyId { get; set; }
        public int preorderPayMode { get; set; }
        public bool isNotRemaining { get; set; }//余额支付
    }
    public class VAClientPreorderPrepayWithCreditResponse : VAClientPreorderPrepayWithCreditRequest
    {
        public string urlToContinuePayment { get; set; }
        public string alipayOrder { get; set; }
        public string unionpayOrder { get; set; }
        public double moneyRemained { get; set; }//用户当前余额
    }
    /// <summary>
    /// 商户端查询预点单
    /// </summary>
    public class VAPoslitePreOrderQueryRequest
    {
        public VAMessageType type { get; set; }
        public int shopId { get; set; }
        public string eCardNumber { get; set; }
        public string verificationCode { get; set; }
        public string companyUserName { get; set; }
        public string companyPassword { get; set; }
        public bool verify { get; set; }
        public bool cancel { get; set; }
        public long preorderId { get; set; }
    }
    public class VAPoslitePreOrderQueryResponse
    {
        public VAResult result { get; set; }
        public VAMessageType type { get; set; }
        public string orderInJson { get; set; }
        public string preOrderTime { get; set; }
        public string currentTime { get; set; }
        public string customerName { get; set; }
        public string customerSex { get; set; }
        public string customerPhoneNumber { get; set; }
        public string customerUserName { get; set; }
        public string isVIP { get; set; }
        public string eCardNumber { get; set; }
        public string prePaidSum { get; set; }
        public string prePayTime { get; set; }
        public string preOrder19dianId { get; set; }
        public string isPaid { get; set; }
        public string isVerified { get; set; }
        public string preorderGiftTitle { get; set; }
        public string preorderGiftDesc { get; set; }
        public string preorderGiftValidTime { get; set; }
        public string customerTitleName { get; set; }
        public string verifiedSaving { get; set; }
        public string preOrderServerSum { get; set; }
        public string preorderNote { get; set; }
        public string sundryJson { get; set; }//杂项
    }
    /// <summary>
    /// Web端查询预点单
    /// </summary>
    public class VAWebPreOrderQueryRequest
    {
        public int preorderId { get; set; }
    }
    public class VAWebPreOrderQueryResponse
    {
        public VAResult result { get; set; }
        public string orderInJson { get; set; }
        public string preOrderTime { get; set; }
        public string currentTime { get; set; }
        public string customerName { get; set; }
        public string customerSex { get; set; }
        public string customerPhoneNumber { get; set; }
        public string customerUserName { get; set; }
        public string isVIP { get; set; }
        public string eCardNumber { get; set; }
        public int preorderSupportCount { get; set; }
        public double seqencingForCurrentWeek { get; set; }
        public double seqencingForCurrentMonth { get; set; }
        public string companyLogoURL { get; set; }
        public double prePayAmount { get; set; }//点单支付金额
        public double preorderAmount { get; set; }//点单金额
    }
    public class PreOrderIn19dian
    {
        public int dishId { set; get; }

        public string dishName { get; set; }
        public double unitPrice { get; set; }
        public int quantity { get; set; }
        public string dishPriceName { get; set; }
        public int dishPriceI18nId { get; set; }
        public string dishTypeName { get; set; }
        //wangcheng
        public string markName { get; set; }

        public bool vipDiscountable { get; set; }
        //口味，配料
        public VADishTaste dishTaste { get; set; }//口味
        public List<VADishIngredients> dishIngredients { get; set; }//配料
    }

    public class PreOrderIn19dianOrderJson : PreOrderIn19dian
    {
        public bool isHavePraise { get; set; }
    }

    public class PreShortOrderIn19dian
    {
        public int dishPriceI18nId { get; set; }
        public int quantity { get; set; }
    }
    public class PreorderAndDishUpdateInfo
    {
        public string orderInJson { get; set; }
        //public int companyId { get; set; }
        public int shopId { get; set; }
    }
    /// <summary>
    /// 客户端微博分享成功
    /// </summary>
    public class VAClientSharePreOrderRequest : VANetworkMessage
    {
        public long preorderId { get; set; }
        public List<VAOpenIdVendor> openIdVendors { get; set; }
    }
    public class VAClientSharePreOrderResponse : VANetworkMessage
    {
        public long preorderId { get; set; }
    }
    /// <summary>
    /// 用户充值
    /// </summary>
    public class VATopUpRequest : VANetworkMessage
    {
        public double topUpAmount { get; set; }
        public VAClientPayMode clientPayMode { get; set; }
    }
    public class VATopUpResponse : VANetworkMessage
    {
        public string urlToContinuePayment { get; set; }
        public string alipayOrder { get; set; }
        public string unionpayOrder { get; set; }
    }
    /// <summary>
    /// 用户删除未结账的预点单
    /// </summary>
    public class VAClientPreorderDeleteRequest : VANetworkMessage
    {
        public List<long> preorderIds { get; set; }
    }
    public class VAClientPreorderDeleteResponse : VANetworkMessage
    {

    }
    /// <summary>
    /// 微信号登录
    /// </summary>
    public class VAWechatLoginRequest
    {
        public string wechatId { get; set; }
    }
    public class VAWechatLoginResponse : VANetworkMessage
    {
        public string eVip { get; set; }
    }
    /// <summary>
    /// 用户根据公司编号查询公司详情
    /// </summary>
    public class VABrandDetailRequest : VANetworkMessage
    {
        public int companyId { get; set; }
    }
    public class VABrandDetailResponse : VABrandDetailRequest
    {
        public VABrand brand { get; set; }
    }
    /// <summary>
    /// Vip政策
    /// </summary>
    public class VAVipPolicy
    {
        public int requirement { get; set; }
        public int nextRequirement { get; set; }
        public double discount { get; set; }
        public string name { get; set; }
        public long policyId { get; set; }
    }
    /// <summary>
    /// 计算可省金额和原价的结构
    /// </summary>
    public class VAEstimatedSavingAndOrginal
    {
        public double estimatedSaving { get; set; }
        public double orginalPriceSum { get; set; }
    }
    /// <summary>
    /// 根据店铺编号查询店铺详情Request modify by wangc 20140319
    /// </summary>
    public class VAClientShopDetailRequest : VANetworkMessage
    {
        public int shopId { get; set; }
        public int pageIndex { get; set; }
        public int pageSize { get; set; }

    }
    /// <summary>
    /// 根据店铺编号查询店铺详情Response
    /// </summary>
    public class VAClientShopDetailResponse : VANetworkMessage
    {
        public VAClientShopDetailResponse()
        {
            publicityPhotoPath = string.Empty;
            shopImage = new List<string>();
            shopName = string.Empty;
            shopAddress = string.Empty;
            shopTelephone = string.Empty;
            openTimes = string.Empty;
            userShareUrl = string.Empty;
            userShareMessage = string.Empty;
        }
        private List<EvaluationInfo> _evaluationList;
        private List<EvaluationPercent> _evaluationPercent;
        public int shopId { get; set; }
        public string publicityPhotoPath { get; set; }//门店背景图片
        public List<string> shopImage { get; set; }//门店详情图片
        public string shopLogoPath { get; set; }//门店缩略图，门店logo
        public string shopName { get; set; }//门店名称
        public double shopRating { get; set; }//门店评分
        public string shopAddress { get; set; }//门店地址
        public string shopTelephone { get; set; }//门店电话
        public string openTimes { get; set; }//门店营业时间
        public bool isFavorites { get; set; }//门店是否收藏
        public double currectDiscount { get; set; }//用户在当前门店享有的折扣
        public string userShareUrl { get; set; }//门店分享
        public double longitude { get; set; }//门店经度
        public double latitude { get; set; }//门店纬度
        public string userShareMessage { get; set; }//门店分享内容
        /// <summary>
        /// 店铺等级
        /// </summary>
        public int shopLevel { get; set; }
        /// <summary>
        /// 评价列表
        /// </summary>
        public List<EvaluationInfo> evaluationList
        {
            get
            {
                if (_evaluationList == null)
                {
                    _evaluationList = new List<EvaluationInfo>();
                }
                return _evaluationList;
            }
            set
            {
                _evaluationList = value;
            }
        }
        public List<EvaluationPercent> evaluationPercent
        {
            get
            {
                if (_evaluationPercent == null)
                {
                    _evaluationPercent = new List<EvaluationPercent>();
                }
                return _evaluationPercent;
            }
            set
            {
                _evaluationPercent = value;
            }
        }
        public int prepayOrderCount { get; set; }

        public bool isMore { get; set; }
        public int goodEvaluationCount { get; set; }

        /// <summary>
        /// 抵扣券列表
        /// </summary>
        public List<OrderPaymentCouponDetail> couponDetails { get; set; }

    }
    public class EvaluationInfo
    {
        public int customId { get; set; }
        public double evaluationDate { get; set; }
        public string evaluationContent { get; set; }
        public int evaluationValue { get; set; }

        public string mobilePhoneNumber { get; set; }

    }

    public class EvaluationPercent
    {
        public int evaluationValue { get; set; }
        public double percent { get; set; }

        public int count { get; set; }

    }
    /// <summary>
    /// 根据店铺编号查询对应的沽清菜品和当前开通的支付方式以及最新的菜谱信息
    /// </summary>
    public class VAShopSellOffDishRequest : VANetworkMessage
    {
        public int shopId { get; set; }
    }

    public class VAShopSellOffDishResponse : VANetworkMessage
    {
        public List<int> dishsSellOff { get; set; }
        public List<int> dishIngredientsSellOff { get; set; }
        public List<VAPayMode> payModeList { get; set; } //支付方式 add by wang 20140403
        public List<VAMenuForApp> menuList { get; set; } //填充手机端需要的菜谱信息add by wangc 20140414 
        public int defaultPayment { get; set; } //默认支付方式
        public string shopTelephone { get; set; } //门店电话
        public double longitude { get; set; } //门店经度
        public double latitude { get; set; } //门店纬度
        public string shopAddress { get; set; } //门店地址
        public double rationBalance { get; set; } //粮票
        public double executedRedEnvelopeAmount { get; set; } //生效红包金额
        public VAShopVipInfo userVipInfo { get; set; } //用户在当前门店折扣信息
        public List<DishPraiseInfo> dishPraiseNumList { get; set; }
        public List<int> recommendedDishList { get; set; }
        public bool isHaveUnusedCoupon { get; set; }//用户是否有当前门店未使用优惠券
        public List<OrderPaymentCouponDetail> couponDetails { get; set; }//可参与支付抵扣金额优惠券列表

        /// <summary>
        /// 是否使用过第三方支付 <remarks>在粮票金额大于0的情况下判断</remarks>
        /// </summary>
        public bool isUsedThirdPay { get; set; }
        /// <summary>
        /// 杂项 add by zhujinlei 2015/08/07 从首页列表接口转到沽清接口中
        /// </summary>
        public List<VASundryInfo> sundryInfo { get; set; }
    }
    #endregion

    #region 商户相关
    #endregion

    #region googleMap
    /// <summary>
    /// google map
    /// 地址查询经纬度返回信息
    /// 经纬度查询城市名等
    /// </summary>
    public class GoogleMapCoordinateInfo
    {
        public string status { get; set; }
        public List<GoogleMapResult> results { get; set; }
    }
    public class GoogleMapResult
    {
        public GoogleMapGeometry geometry { get; set; }
        public List<GoogleMapAddressComponet> address_components { get; set; }
    }
    public class GoogleMapAddressComponet
    {
        public string long_name { get; set; }
        public string short_name { get; set; }
        public List<string> types { get; set; }
    }
    public class GoogleMapGeometry
    {
        public MapLocation location { get; set; }
    }
    public class MapLocation
    {
        public double lat { get; set; }
        public double lng { get; set; }
    }
    #endregion

    #region baiduMap
    /// <summary>
    /// baidu map
    /// 地址查询经纬度返回信息
    /// 经纬度查询城市名等
    /// </summary>
    public class BaiduMapCoordinateInfo
    {
        public string status { get; set; }
        public BaiduMapResult result { get; set; }
    }
    public class BaiduMapResult
    {
        public MapLocation location { get; set; }
        public BaiduMapAddressComponet addressComponent { get; set; }
    }
    public class BaiduMapAddressComponet
    {
        public string city { get; set; }
        public string province { get; set; }
        public string street { get; set; }
        public string street_number { get; set; }
        public string district { get; set; }
    }
    #endregion

    #region 激励相关
    /// <summary>
    /// 激励函数参数
    /// </summary>
    public class VAEncourageDetail
    {
        public VAMessageType messageType { get; set; }
        public VAAppType appType { get; set; }
        public string pushToken { get; set; }
        public long customerId { get; set; }
        public long customerConnCouponID { get; set; }//使用优惠券奖励需要
        public string phoneNumberInvited { get; set; }//用户邀请奖励需要
    }
    /// <summary>
    /// 自定义活动使用时传入的参数
    /// VANetworkMessage暂时不使用
    /// </summary>
    public class VACustomEncourageRequest : VANetworkMessage
    {
        public List<long> customerList { get; set; }
        public long customEncourageId { get; set; }
    }

    /// <summary>
    /// 发送自定义活动时的返回值
    /// </summary>
    public class VACustomEncourageResponse : VANetworkMessage
    {

    }
    /// <summary>
    /// 新建活动时的返回值
    /// </summary>
    public class VANewCustomEncourage
    {
        public long encourageId { get; set; }
        public VAResult VAResult { get; set; }
    }

    /// <summary>
    /// 新建活动时的返回值(wangcheng)
    /// </summary>
    public class VANewCompanyEncourage
    {
        public long encourageId { get; set; }
        public VAResult VAResult { get; set; }
    }
    /// <summary>
    /// 公司自定义活动时传入的参数(wangcheng)
    /// </summary>
    public class VACompanyEncourageRequest : VANetworkMessage
    {
        public List<long> customerList { get; set; }
        public long companyEncourageId { get; set; }
    }
    ///  /// <summary>
    /// 发送自定义活动时的返回值(wangcheng)
    /// </summary>
    public class VACompanyEncourageResponse : VANetworkMessage
    {

    }
    #endregion

    #region POSLite相关
    public class VALatestPOSLiteInfo
    {
        public string POSLiteUpdatePackageName { get; set; }
        public string POSLiteVersion { get; set; }
        public string downloadURL { get; set; }
    }
    #endregion

    #region Alipay相关
    public class VAAlipayWebRequest : VANetworkMessage
    {
        public VAPayOrderType connType { get; set; }
        public double totalFee { get; set; }
    }
    #endregion

    /// <summary>
    /// 增加预点单发票抬头信息
    /// 2013/9/15
    /// </summary>
    public class VAPreOrderInvoiceTitleRequest : VANetworkMessage
    {
        public long preOrder19dianId { get; set; }
        public List<long> invoiceIdList { get; set; }//发票抬头Id（删除时使用）
        public string invoiceTitle { get; set; }//发票抬头信息
        public int invoiceStatus { get; set; }//发票抬头状态 1:查询，2：删除 3:新增
    }
    public class VAPreOrderInvoiceTitleResponse : VANetworkMessage
    {
        public List<InvoiceInfo> invoiceInfo { get; set; }
        public int invoiceStatus { get; set; }//发票抬头状态 1:查询，2：删除 3:新增
    }
    public class InvoiceInfo
    {
        public long invoiceId { get; set; }
        public string invoiceTitle { get; set; }
    }

    /// <summary>
    /// 增加退款功能
    /// 2013/9/15
    /// </summary>
    public class VAPreOrderRefundRequest : VANetworkMessage
    {
        public long preOrder19dianId { get; set; }

        /// <summary>
        /// 点单主键ID
        /// </summary>
        public Guid orderId { get; set; }
    }
    public class VAPreOrderRefundResponse : VANetworkMessage
    {

    }

    /// <summary>
    /// 原路退款功能
    /// 2014/1/28 xiaoyu
    /// </summary>
    public class VAOriginalRefundRequest : VANetworkMessage
    {
        public long preOrder19dianId { get; set; }

        /// <summary>
        /// 退款员工
        /// </summary>
        public int employeeId { get; set; }

        /// <summary>
        /// 订单ID
        /// </summary>
        public Guid orderId { get; set; }
    }
    /// <summary>
    /// 原路退款返回信息
    /// </summary>
    public class VAOriginalRefundResponse : VANetworkMessage
    {
        public string moneyOriginalRefund { get; set; }//原路返回的金额
        public string moneyBackToMoneyRemain { get; set; }//退到悠先余额的金额
    }
    #region 优惠券领取列表相关
    /// <summary>
    /// 优惠券领取列表
    /// </summary>
    public class VACouponActivityRequest : VANetworkMessage
    {
        //public int shopId { get; set; }    // 门店id

    }
    public class VACouponActivityResponse : VANetworkMessage
    {

        public List<CouponsReceiveActivities> couponActivitiesList { get; set; }//活动列表
        public int couponCount { get; set; }//优惠劵数量
    }

    public class CouponsReceiveActivities
    {
        public long activitiesId { get; set; }//活动ID，点击领用时候传此参数查找优惠卷
        public string couponsReceiveActivitiesName { get; set; } // 活动名称
        public int companyId { get; set; }// 公司id
        // public int shopId { get; set; }        // 门店id
        public long couponId { get; set; }        // 优惠券id
        public double activitiesValidStartTime { get; set; } // 活动开始时间
        public double activitiesValidEndTime { get; set; }        // 活动结束时间
        public double couponValidDayCount { get; set; }        // 优惠券可使用天数
        public string couponImage { get; set; }//小图
        public List<string> detailImageUrls { get; set; }//大图
        public double couponDownloadPrice { get; set; }//对应优惠卷价格，0为免费卷
        public int status { get; set; }//对应优惠卷使用 2：使用,1:未使用

        public string companyName { get; set; }//公司名称
        public VACouponType couponType { get; set; }//优惠卷类型
        public double discount { get; set; } //折扣
        public double discountedAmount { get; set; } //抵价金额
        public double usableStartDate { get; set; }   // 优惠卷可使用开始日期
        public double usableEndDate { get; set; }     // 优惠卷可使用结束日期
        public List<VARestaurant> applicableRestaurants { get; set; } // 数组：适用商户信息数组 ref: VARestaurant



    }
    #region 自定义优惠券活动领取操作-----暂时不用
    ///// <summary>
    ///// 自定义优惠券活动领取操作
    ///// </summary>
    //public class VACouponReceiveRequest : VANetworkMessage
    //{
    //    public long activitiesId { get; set; }//活动id
    //}
    //public class VACouponReceiveResponse : VANetworkMessage
    //{

    //} 
    #endregion
    #endregion
    /// <summary>
    /// 远程下单
    /// </summary>
    public class VARemoteOrderRequset : VANetworkMessage
    {
        public long preorderId { get; set; }
    }
    public class VARemoteOrderResponse : VANetworkMessage
    {

    }
    /// <summary>
    /// 根据优惠卷ID获取详细信息
    /// </summary>
    public class VACouponDetailRequset : VANetworkMessage
    {
        public long couponId { get; set; }
        public long activitiesId { get; set; }//活动ID，
        public int activitiesType { get; set; }//活动类型,1:自定义活动,2:预付返现,3:自定义优惠券领取活动
    }

    public class VACouponDetailResponse : VANetworkMessage
    {
        public VACustomerCouponDetail couponDetail { get; set; }
        public int couponCount { get; set; }//个人优惠劵数量
    }

    /// <summary>
    /// 账户明细
    /// </summary>
    public class VAAccountDetailRequest : VANetworkMessage
    {

    }
    public class VAAccountDetailResponse : VANetworkMessage
    {
        public List<VAAccountDetail> accountDetailList { get; set; }//
    }
    /// <summary>
    /// 账户明细详细
    /// </summary>
    public class VAAccountDetail
    {
        public string accountName { get; set; }
        public string accountPrice { get; set; }
        public string accountTime { get; set; }
        public string accountStatus { get; set; }
    }

    #region 掌中宝对账，审核，退款预点单接口
    /// <summary>
    /// 查询点单列表（已付款，已审核，未审核）
    /// </summary>
    public class ZZB_VAPreOrderListRequest : VANetworkMessage
    {
        //基本查询
        public int shopId { get; set; }//门店编号
    }
    public class ZZB_VAPreOrderListResponse : VANetworkMessage
    {
        public List<ZZB_VAPreOrderListModel> preOrderList;
        public string shoppingMallURL { get; set; }
    }
    public class ZZB_VAPreOrderListModel
    {
        public double prePaidSum { get; set; }//已付金额
        public string mobilePhoneNumber { get; set; }//手机号码
        public string customerUserName { get; set; }//顾客姓名
        public double prepaidTime { get; set; }//since1970的秒数（整型），反序列化时转成NSDate
        public long preOrder19dianId { get; set; }//流水号
        public int status { get; set; }//点单状态：1未使用，2已使用，3退款中，4已退款
    }
    /// <summary>
    /// 点单列表详情
    /// </summary>
    public class ZZB_VAPreOrderListDetailRequest : VANetworkMessage
    {
        public long preOrder19dianId { get; set; }//点单流水号
        public int shopId { get; set; }//Add at 2014-3-28

        /// <summary>
        /// 点单ID
        /// </summary>
        public Guid orderId { get; set; }
    }
    public class ZZB_VAPreOrderListDetailResponse : VANetworkMessage
    {
        public long preOrder19dianId { get; set; }//点单流水号
        public double prePaidTime { get; set; }//支付时间
        public double discount { get; set; }//支付时享受的折扣
        public decimal orginalPrice { get; set; }//原价
        public decimal savedAmount { get; set; }//已省金额
        public decimal prePaidSum { get; set; }//支付金额
        public decimal refundMoneySum { get; set; }//已退款金额
        public decimal canRefundAccount { get; set; }//当前可退最大金额 Add at 20131230 by jinyanni
        public int status { get; set; }//点单状态
        //public string sundryJson { get; set; }//杂项信息
        public string orderInJson { get; set; }//菜品信息
        public bool isShopConfirmed { get; set; }//是否审核
        public bool isApproved { get; set; }//是否对账
        public string mobilePhoneNumber { get; set; }
        public string customerUserName { get; set; }
        public string invoiceTitle { get; set; }//发票抬头
        public List<SundryInfoResponse> sundryList;//杂项
        public string deskNumber { get; set; }//直接支付时填写的桌号

        public long customerId { get; set; }

        public List<ShopHaveAuthority> roles { set; get; }
        /// <summary>
        /// 是否已评价
        /// false:已评价
        /// </summary>
        public bool isNotEvaluated { set; get; }
        /// <summary>
        /// 评价值
        /// </summary>
        public int evaluationValue { set; get; }
        /// <summary>
        /// 评价时间
        /// </summary>
        public double evaluationTime { set; get; }
        /// <summary>
        /// 评价内容
        /// </summary>
        public string evaluationContent { set; get; }
        public List<RefundDetail> refundDetails { get; set; }
        public double deductionPrice { get; set; }
        public List<OrderPreferentialDetail> preferentialDetails { get; set; }//优惠详情

        /// <summary>
        /// 补差价 add by zhujinlei 2015/06/30
        /// </summary>
        public double fillpostAmount { get; set; }

        /// <summary>
        /// 折扣价 add by zhujinlei 2015/07/06
        /// </summary>
        public double disCountMoney { get; set; }

        /// <summary>
        /// 订单ID add by zhujinlei 2015/07/06
        /// </summary>
        public Guid orderId { get; set; }
    }

    public class OrderPreferentialDetail
    {
        public string name { get; set; }//优惠名称：抵扣券，折扣
        public string description { get; set; }//抵扣描述
    }

    public class ZZBClientShopPreorderListRequest : VANetworkMessage
    {
        public int shopId { get; set; }
        public int pageIndex { get; set; }
        public int pageSize { get; set; }

    }
    public class ZZBClientShopPreorderListResponse : VANetworkMessage
    {
        public void VZZBClientShopPreorderListResponse()
        {
            evaluationList = new List<evaluationDetail>();
            evaluationPercent = new List<EvaluationPercent>();
        }
        public int shopId { get; set; }
        public int shopLevel { get; set; }
        public long hasSold { get; set; }
        public int shopScore { get; set; }
        public int upgradeScore { get; set; }
        public bool isMore { get; set; }
        public List<evaluationDetail> evaluationList { get; set; }
        public List<EvaluationPercent> evaluationPercent { get; set; }
        public string helpUrl { get; set; }

    }
    /// <summary>
    /// 用户评价详情
    /// </summary>
    public class evaluationDetail
    {
        public long customId { get; set; }
        public long preOrder19dianId { get; set; }
        public string customName { get; set; }
        public string mobilePhoneNumber { get; set; }
        public double evaluationTime { get; set; }
        public string evaluationContent { get; set; }
        public int evaluationValue { get; set; }

        public Guid orderId { get; set; }

    }
    /// <summary>
    /// 点单审核操作
    /// </summary>
    public class ZZB_VAPreOrderConfrimRequest : VANetworkMessage
    {
        [UserIntegralParamsAttribute]
        public long preOrder19dianId { get; set; }//点单流水号
        public int employeeId { get; set; }//员工编号
        public int shopId { get; set; }//Add at 2014-3-28
    }
    public class ZZB_VAPreOrderConfrimResponse : VANetworkMessage
    {

    }
    /// <summary>
    /// 点单对账操作
    /// 暂时取消不再使用
    /// </summary>
    public class ZZB_VAPreOrderApprovedRequest : VANetworkMessage
    {
        public string userName { get; set; }
        public string password { get; set; }
        public int preOrder19dianId { get; set; }//点单流水号
        public int employeeId { get; set; }//员工编号
    }
    public class ZZB_VAPreOrderApprovedResponse : VANetworkMessage
    {
        //VA_RESULT
    }
    /// <summary>
    /// 点单退款操作
    /// </summary>
    public class ZZB_VAPreOrderRefundRequest : VANetworkMessage
    {
        public int preOrder19dianId { get; set; }//点单流水号
        public double refundAccount { get; set; }//点单退款金额
        public int shopId { get; set; }//Add at 2014-3-28

        /// <summary>
        /// 点单OrderID
        /// </summary>
        public Guid orderId { get; set; }
    }
    public class ZZB_VAPreOrderRefundResponse : VANetworkMessage
    {

    }
    /// <summary>
    /// 悠先服务客户端用户手机验证
    /// </summary>
    public class ZZB_VAUserRegisterRequest : VANetworkMessage
    {
        public string mobilePhoneNumber { get; set; }
        public string verificationCode { get; set; }//验证码为空时表示发送验证码
    }
    public class ZZB_VAUserRegisterResponse : VANetworkMessage
    {
        public bool isNewNumber { get; set; }//标识该次验证是新号码，客户端应跳转到信息完善界面
        public List<VAEmployeeShop> employeeShop { get; set; }
        public int employeeId { get; set; }
    }

    /// <summary>
    /// 悠先服务客户端发送语音短信消息
    /// </summary>
    public class ZZB_VAClientSendVoiceMessageRequest : VANetworkMessage
    {
        /// <summary>
        /// 手机号码
        /// </summary>
        public string mobilePhone { set; get; }
    }
    /// <summary>
    /// 悠先服务客户端发送语音短信回复
    /// </summary>
    public class ZZB_VAClientSendVoiceMessageResponse : VANetworkMessage
    {
    }

    /// <summary>
    /// 悠先服务客户端用户信息修改
    /// </summary>
    public class ZZB_VAModifyUserInfoRequest : VANetworkMessage
    {
        public string name { get; set; }//不可为空，最多10个汉字
        public string employeeNumber { get; set; }//可为空，最多20个数字
    }
    public class ZZB_VAModifyUserInfoResponse : VANetworkMessage
    {

    }

    /// <summary>
    /// 悠先服务客户端用户查询店铺列表
    /// </summary>
    public class ZZB_VAQueryShopListRequest : VANetworkMessage
    {

    }
    public class ZZB_VAQueryShopListResponse : VANetworkMessage
    {
        public List<VAEmployeeShop> employeeShop { get; set; }
        public string servicePhone { get; set; }//客服热线
    }
    /// <summary>
    /// 悠先服务查询版本更新信息
    /// </summary>
    public class ZZB_VAQueryUpdateInfoResqeust : VANetworkMessage
    {
    }
    /// <summary>
    /// 悠先服务查询版本更新返回信息
    /// </summary>
    public class ZZB_VAQueryUpdateInfoResponse : VANetworkMessage
    {
        public string latestBuild { get; set; }// 新版本号
        public string latestUpdateDescription { get; set; }
        public string latestUpdateUrl { get; set; }// 更新版本的url
        public bool forceUpdate { get; set; }//客户端是否需要强制用户更新
    }
    /// <summary>
    /// 悠先服务客户端发送错误日志消息
    /// </summary>
    public class ZZB_SendErrorMessageRequest : VANetworkMessage
    {
        public string errorInfo { get; set; }//VACientErrorInfo结构的json字符串
    }
    /// <summary>
    /// 悠先服务客户端发送错误日志回复
    /// </summary>
    public class ZZB_SendErrorMessageResponse : VANetworkMessage
    {

    }

    /// <summary>
    /// 悠先服务客户端菜品管理权限消息
    /// </summary>
    public class ZZB_DishManageRoleRequest : VANetworkMessage
    {
        public int shopId { set; get; }
    }

    /// <summary>
    /// 悠先服务客户端菜品管理权限回复
    /// </summary>
    public class ZZB_DishManageRoleResponse : VANetworkMessage
    {
        public List<ShopHaveAuthority> roles { set; get; }
    }


    public class ZZB_DishManageRoleResponseOld : VANetworkMessage
    {
        public string[] roles { set; get; }
    }

    public class ShopHaveAuthority
    {

        public string authorityCode { set; get; }
        public string authorityName { set; get; }
        public bool isClientShow { set; get; }
    }
    /// <summary>
    /// 悠先服务权限判断请求
    /// </summary>
    public class ZZB_RoleCheckRequest : VANetworkMessage
    {
        public int shopId { set; get; }
        public string authorityCode { set; get; }
    }
    /// <summary>
    /// /悠先服务权限判断回复
    /// </summary>
    public class ZZB_RoleCheckResponse : VANetworkMessage
    {
        /// <summary>
        /// 需要跳转的url（抽奖设置、抽奖活动统计等h5页面url）
        /// </summary>
        public String url { get; set; }
    }
    /// <summary>
    /// 悠先服务客户端菜品查询消息
    /// </summary>
    public class ZZB_DishSearchRequest : VANetworkMessage
    {
        public int shopId { set; get; }
        public string key { set; get; }
        /// <summary>
        /// 是否沽清
        /// </summary>
        public bool sellOff { set; get; }

        public int pageIndex { set; get; }

        public int pageSize { set; get; }
    }

    public class ZZB_DishAllSearchRequest : ZZB_DishSearchRequest
    {
        public int state { set; get; }
    }

    /// <summary>
    /// 悠先服务客户端菜品查询回复
    /// </summary>
    public class ZZB_DishSearchResponse : VANetworkMessage
    {
        /// <summary>
        /// 菜品列表
        /// </summary>
        public List<ZZB_DishDetails> dishs { set; get; }
        /// <summary>
        /// 是否有下一页
        /// </summary>
        public bool hasNextPage { set; get; }

    }

    public class ZZB_DishDetails
    {
        public int companyId { set; get; }
        public int shopId { set; get; }
        public int menuId { set; get; }
        public int dishId { set; get; }
        public int dishPriceId { set; get; }
        public string dishName { set; get; }
        public string scaleName { set; get; }
        public float price { set; get; }
        public bool sellOff { set; get; }
    }


    /// <summary>
    /// 悠先服务客户端菜品沽清消息
    /// </summary>
    public class ZZB_DishSellOffRequest : VANetworkMessage
    {
        public int companyId { set; get; }
        public int shopId { set; get; }
        public int menuId { set; get; }
        public int dishId { set; get; }
        public int dishPriceId { set; get; }
        /// <summary>
        /// 需要做的操作
        /// </summary>
        public bool sellOff { set; get; }
    }


    /// <summary>
    /// 悠先服务客户端菜品沽清回复
    /// </summary>
    public class ZZB_DishSellOffResponse : VANetworkMessage
    {

    }

    /// <summary>
    /// 悠先服务客户端push token更新请求
    /// </summary>
    public class ZZB_PushTokenUpdateRequest : VANetworkMessage
    {
        public string pushToken { set; get; }
    }

    /// <summary>
    /// 悠先服务客户端push token更新回复
    /// </summary>
    public class ZZB_PushTokenUpdateResponse : VANetworkMessage
    {
    }

    /// <summary>
    /// 悠先服务菜品价格修改请求
    /// </summary>
    public class ZZB_ClientDishPriceModifyRequest : VANetworkMessage
    {

        public int shopId { set; get; }

        public int dishPriceId { set; get; }

        public double price { set; get; }
    }

    /// <summary>
    /// 悠先服务菜品价格修改回复
    /// </summary>
    public class ZZB_ClientDishPriceModifyResponse : VANetworkMessage
    {
        public long taskId { set; get; }
    }

    /// <summary>
    /// 悠先服务任务状态查询请求
    /// </summary>
    public class ZZB_ClientTaskCheckStatusRequest : VANetworkMessage
    {
        [DataMember(Name = "taskId")]
        public long taskId { set; get; }
    }

    /// <summary>
    /// 悠先服务任务状态回复
    /// </summary>
    public class ZZB_ClientTaskCheckStatusResponse : VANetworkMessage
    {
        [DataMember(Name = "status")]
        public TaskStatus status { set; get; }
    }
    /*
     * add by wangc 20140604 服务器后台功能客户追溯和部分统计页面接口模块
    */
    /// <summary>
    /// 客户追溯用户信息Request
    /// </summary>
    public class ZZB_ClientRetrospectCustomerRequest : VANetworkMessage
    {
        public string mobilePhoneNumber { get; set; }
    }
    /// <summary>
    /// 客户追溯用户信息Response(最多返回一个用户)
    /// </summary>
    public class ZZB_ClientRetrospectCustomerResponse : VANetworkMessage
    {
        public long customerId { get; set; }
        public string userName { get; set; }
        public string mobilePhoneNumber { get; set; }
        public double registerDate { get; set; }
        public double money19dianRemained { get; set; }
        public double executedRedEnvelopeAmount { get; set; }
        public double notExecutedRedEnvelopeAmount { get; set; }
    }

    /// <summary>
    /// 查看追溯到当前用户的历史点单列表Request，分页
    /// </summary>
    public class ZZB_ClientRetrospectCustomerOrderRequest : VANetworkMessage
    {
        public long customerId { get; set; }
        public int pageSize { get; set; }
        public int pageIndex { get; set; }
    }
    /// <summary>
    /// 查看追溯到当前用户的历史点单列表Response
    /// </summary>
    public class ZZB_ClientRetrospectCustomerOrderResponse : VANetworkMessage
    {
        public List<CustomerOrder> customerOrderList { get; set; }
        public bool isHaveMore { get; set; }
    }
    /// <summary>
    /// 查询追溯到用户某个历史点单的详情内容Request
    /// </summary>
    public class ZZB_ClientRetrospectCustomerOrderDetailRequest : VANetworkMessage
    {
        public long preOrder19dianId { get; set; }
    }
    /// <summary>
    /// 查询追溯到用户某个历史点单的详情内容Response
    /// </summary>
    public class ZZB_ClientRetrospectCustomerOrderDetailResponse : VANetworkMessage
    {
        public long preOrder19dianId { get; set; }
        public string shopName { get; set; }
        public double prePaidSum { get; set; }
        public double prePayTime { get; set; }
        public List<OrderPayMode> payModeList { get; set; }
        public double refundMoneySum { get; set; }
        public string isShopConfirmed { get; set; }//是否已入座
        public string isApproved { get; set; }//是否已对账
        public double deductibleAmount { get; set; }
        public bool isPersonalFullRefund { get; set; }
    }
    public class OrderPayMode
    {
        public string payModeName { get; set; }
        public decimal payAmount { get; set; }
        public int orderUsedPayMode { get; set; }

        /// <summary>
        /// 退款金额
        /// </summary>
        public decimal refundAmount { get; set; }

        /// <summary>
        /// 状态 <remarks>取值定义同点单的状态</remarks>
        /// </summary>
        public int status { get; set; }


        /// <summary>
        /// 排序号
        /// </summary>
        public int sortOrder { get; set; }
    }
    /// <summary>
    /// 调整追溯到用户的余额Request
    /// </summary>
    public class ZZB_ClientRetrospectCustomerChangeBalanceRequest : VANetworkMessage
    {
        public long customerId { get; set; }
        public double changeAmount { get; set; }
        public string remark { get; set; }
    }
    public class ZZB_ClientRetrospectCustomerChangeBalanceResponse : VANetworkMessage
    {
        public double remainMoney { get; set; }//有没有必要返回当前用户余额，因为页面需要刷新，还是重新请求接口，还是客户端本地处理
    }
    /// <summary>
    /// 悠先服务追溯用户红包领取记录Request
    /// </summary>
    public class ZZB_ClientCheckRedEnvelopeDetailsRequest : VANetworkMessage
    {
        public string mobilePhoneNumber { get; set; }//???
        public int pageSize { get; set; }
        public int pageIndex { get; set; }
    }
    /// <summary>
    /// 悠先服务追溯用户红包领取记录Response
    /// </summary>
    public class ZZB_ClientCheckRedEnvelopeDetailsResponse : VANetworkMessage
    {
        public List<ClientRedEnvelopeDetail> detailList { get; set; }
    }
    #endregion
    /// <summary>
    /// 悠先服务门店VIP数量信息Request
    /// </summary>
    public class ZZB_ClientCheckShopVipUsersInfoRequest : VANetworkMessage
    {
        public int shopId { get; set; }
    }
    /// <summary>
    /// 悠先服务门店VIP数量信息Response
    /// </summary>
    public class ZZB_ClientCheckShopVipUsersInfoResponse : VANetworkMessage
    {
        public int shopTotalCount { get; set; }//累计会员数量
        public int currectCityTopShopTotalCount { get; set; }//城市TOP门店累计会员数量
        public string currectCityName { get; set; }//当前门店所在城市名称
        public int currectMonthShopIncreasedCount { get; set; }//当前月份新增会员数量
        public int currectMonthTopShopIncreasedCount { get; set; }//当月城市TOP门店新增会员数量
        public int currectMonth { get; set; }//当前月份（服务器时间为准）
        public string shopVipWebUrl { get; set; }//web view页面地址
    }
    /// <summary>
    /// 201401客户端首页接口new(Request)
    /// </summary>
    public class ClientIndexListRequest : VANetworkMessage
    {
        public int pageSize { get; set; }
        public int pageIndex { get; set; }
        public double locateLongitude { get; set; }//定位经度（分页版本）
        public double locateLatitude { get; set; }//定位纬度（分页版本）
        public int dataType { get; set; }//客户端首页门店请求数据类型，默认无效值为0，当前只定义1：表示吃过的店，11：表示看过的店，12：关注的店
        public int tagId { get; set; }//客户端期望查看门店商圈编号
    }
    /// <summary>
    /// 201401客户端首页接口new(Response)
    /// </summary>
    public class ClientIndexListResponse : VANetworkMessage
    {
        public List<VABrandBannerList> vaAd { get; set; }
        public List<VAIndexList> indexList { get; set; }
        public List<VAIndexList> favoritesIndexList { get; set; }//收藏门店列表（分页版本）
        public string clientBgImage { get; set; }//客户端背景图片地址
        public int currentPlatformVipGrade { get; set; }//用户在公司平台VIP等级（目前设置为1，2，3，4等级）
        public bool isHaveMoreData { get; set; }//是否还有更多数据（分页版本）
        public int notEvaluatedCount { get; set; }//未评价数量
        public int dataType { get; set; }//客户端首页门店请求数据类型，默认无效值为0，当前只定义1：表示吃过的店
    }
    //[Serializable]
    ///// <summary>
    ///// 
    ///// </summary>
    //public class VAPayMode
    //{
    //    public int payModeId { get; set; }
    //    public string payModeName { get; set; }
    //}
    [Serializable]
    [DataContract]
    public class VABrandBannerList
    {
        [DataMember]
        public string bannerImageUrlString { get; set; }
        [DataMember]
        public List<int> shopId { get; set; }
        [DataMember]
        public string bannerName { get; set; }
        [DataMember]
        public string bannerDescript { get; set; }
        [DataMember]
        public int bannerType { get; set; }//1 代表门店广告，点击后直接进对应门店点菜页面=2是优惠券广告，这个暂时取消不用了=3是url链接广告，点击后webview打开对应的链接=4是红包活动，点击后webview打开对应的链接，并拼接相应参数
        [DataMember]
        public string bannerUrl { get; set; }//url链接类型广告对应的url地址，其他类型时该项为空字符
    }
    public class VAIndexList//门店列表信息
    {
        /*备注：该对象多数属性只有门店详情才用，现在门店详情单独接口查询下面部分信息，考虑版本问题现在保留一下部分字段信息，
         打*标记的为下个版本可以删除的字段，wangc 20140320*/
        //* 20140414 wangc 注释
        //* public int companyId { get; set; }//公司编号*
        //* public string companyName { get; set; }//公司名称*
        public bool isFavorite { get; set; }
        public int shopId { get; set; }//门店编号
        public string shopName { get; set; }//门店名称
        public double longitude { get; set; }//门店经度
        public double latitude { get; set; }//门店纬度
        //* public int city { get; set; }//城市编号
        public string shopAddress { get; set; }//门店地址
        //* public string phone { get; set; }//门店电话*
        //* public string description { get; set; }//门店描述*
        public string shopLogoUrl { get; set; }//门店缩略图，门店logo*
        //* public List<string> detailImageUrls { get; set; }//门店详情图片*
        //*public string openingTime { get; set; }//门店营业时间*
        public double acpp { get; set; }//取当前公司人均消费
        public double shopRating { get; set; }//星级评分*       
        public string publicityPhotoPath { get; set; }//门店背景图片
        //public List<VAMedalInfo> shopMedal { get; set; }//门店勋章信息
        public List<string> shopMedalUrl { get; set; }//门店勋章信息
        public List<VAMenuForApp> menuList { get; set; }//填充手机端需要的菜谱信息//20140414 下个版本可以删除，添加到沽清接口带给客户端
        public List<VASundryInfo> sundryInfo { get; set; }//杂项
        public List<VAShopVipInfo> shopVipInfo { get; set; }//当前门店的VIP等级信息
        public VAShopVipInfo userVipInfo { get; set; }//用户在当前门店折扣信息
        //*public string userShareUrl { get; set; }//用户分享页面地址*
        public bool isSupportAccountsRound { get; set; }//门店是否支持四舍五入
        public string orderDishDesc { get; set; }//点菜描述 add by wangc 20140319
        public int prepayOrderCount { get; set; }//当前门店支付点单次数(当前已售份数)
        public int shopLevel { get; set; }//门店等级

        public bool isSupportPayment { get; set; }//门店是否支持支付
        public int goodEvaluationCount { get; set; }
        public bool isHaveCoupon { get; set; }//门店是否有优惠券
        public string couponContent { get; set; }//抵扣券内容
        public string[] signList { get; set; }//免排队、送菜、用券、折扣标识
    }
    //门店的VIP信息
    public class VAShopVipInfo
    {
        public int platformVipId { get; set; }
        public string name { get; set; }
        public double discount { get; set; }
    }
    /// <summary>
    /// 201401客户端首页添加、删除收藏（针对门店）
    /// </summary>
    public class VAUserSetFavoriteShopRequest : VANetworkMessage
    {
        public bool isFavorite { get; set; }

        [UserIntegralParamsAttribute]
        public int shopId { get; set; }//201401客户端版本更新
    }
    public class VAUserSetFavoriteShopResponse : VAUserSetFavoriteShopRequest
    {

    }
    /// <summary>
    /// 201401客户端搜索门店列表请求
    /// </summary>
    public class VAClientSearchShopListRequest : VANetworkMessage
    {
        public int pageSize { get; set; }
        public string searchKeyWord { get; set; }
        public int shopId { get; set; }
    }
    /// <summary>
    ///  201401客户端搜索门店列表返回
    /// </summary>
    public class VAClientSearchShopListReponse : VANetworkMessage
    {
        public List<VAIndexList> indexList { get; set; }
    }
    /// <summary>
    ///  201401客户端常用菜品列表请求
    /// </summary>
    public class VACommonDishListRequest : VANetworkMessage
    {
        public int shopId { get; set; }//门店编号
    }
    /// <summary>
    ///  201401客户端常用菜品列表返回
    /// </summary>
    public class VACommonDishListReponse : VANetworkMessage
    {
        public List<int> commonDishId { get; set; }//常用菜品编号列表
    }
    /// <summary>
    ///  201401客户端一键支付点单请求
    /// </summary>
    public class VAClientFastPaymentOrderRequest : VANetworkMessage
    {
        public string orderInJson { get; set; }//选择菜品JSON
        public int shopId { get; set; }//门店编号
        public double clientCalculatedSum { get; set; }//客户端计算菜品原价
        public double clientUxianPriceSum { get; set; }//客户端计算菜品折扣后价格（已省=clientCalculatedSum-clientUxianPriceSum）
        public long preOrderId { get; set; }//如果不传或者为0则为新增，否则为修改原来的点单
        public int isAddbyList { get; set; } //是否列表传过来（1不是，2是）
        public List<VASundryInfo> sundryList { get; set; }//杂项信息
        public int preOrderPayMode { get; set; }//默认支付方式

        public bool boolDualPayment { get; set; }//是否二次支付，服务器端使用，客户端不需要处理
    }

    /// <summary>
    ///  201401客户端一键支付点单返回
    /// </summary>
    public class VAClientFastPaymentOrderReponse : VANetworkMessage
    {
        public double serverCalculatedSum { get; set; }//服务器计算菜品原价
        public double serverUxianPriceSum { get; set; }//客户端计算菜品折扣后价格
        public string urlToContinuePayment { get; set; }//支付宝网页支付地址
        public string alipayOrder { get; set; }//支付宝客户端支付地址
        public string unionpayOrder { get; set; }//支付宝银联客户端地址
        public long preOrderId { get; set; }//当前操作点单流水号
        public VAClientWechatPay ClientWechatPay { get; set; }//微信支付：客户端发送支付请求时所需参数
        public string notPaymentReason { get; set; }//门店不支持付款原因
        public List<VAPayMode> payModeList { get; set; }//支付方式 add by wang 20140403
    }
    /// <summary>
    ///  20140319客户端直接付款请求 add by wangc 
    /// </summary>
    public class VAClientDirectPaymentRequest : VANetworkMessage
    {
        public int shopId { get; set; }//门店编号
        public double payAmount { get; set; }//客户端支付金额
        public long preOrderId { get; set; }//如果不传或者为0则为新增，否则为修改原来的点单
        public int preOrderPayMode { get; set; }//默认支付方式
        public bool boolDualPayment { get; set; }//是否二次支付，服务器端使用，客户端不需要处理
        public string deskNumber { get; set; }//桌号
    }
    /// <summary>
    ///   20140319客户端直接付款返回 add by wangc 
    /// </summary>
    public class VAClientDirectPaymentReponse : VANetworkMessage
    {
        public string urlToContinuePayment { get; set; }//支付宝网页支付地址
        public string alipayOrder { get; set; }//支付宝客户端支付地址
        public string unionpayOrder { get; set; }//支付宝银联客户端地址
        public long preOrderId { get; set; }//当前操作点单流水号
        public VAClientWechatPay ClientWechatPay { get; set; }//微信支付：客户端发送支付请求时所需参数
        public string notPaymentReason { get; set; }//门店不支持付款原因
        public List<VAPayMode> payModeList { get; set; }//支付方式 add by wang 20140403
    }
    /// <summary>
    /// 微信支付：客户端发送支付请求时所需参数
    /// </summary>
    public class VAClientWechatPay
    {
        /*唯一标识*/
        public string appid;
        /** 商户id */
        public string partnerId;
        /** 预支付订单 */
        public string prepayId;
        /** 随机串，防重发 */
        public string nonceStr;
        /** 时间戳，防重发 */
        public string timeStamp;
        /** 商家根据文档填写的数据和签名 */
        public string packageValue;
        /** 商家根据微信开放平台文档对数据做的签名 */
        public string sign;
    }

    /// <summary>
    /// 201401客户端查询当前用户基本信息请求
    /// </summary>
    public class VAClientUserInfoRequest : VANetworkMessage
    {

    }
    /// <summary>
    /// 201401客户端查询当前用户基本信息返回
    /// </summary>
    public class VAClientUserInfoReponse : VANetworkMessage
    {
        public string userName { get; set; }//当前用户名
        public string mobilePhoneNumber { get; set; }//用户电话号码
        public int customerSex { get; set; }//男，女，未知
        public int currentPlatformVipGrade { get; set; }//当前用户在平台的VIP等级
        public string cureentPlatformVipName { get; set; }//当前用户VIP等级名称
        public double money19dianRemained { get; set; }//表示当前用户余额
        public string guideUrl { get; set; }//引导说明
        public string introductionUrl { get; set; }//功能介绍
        public string faqUrl { get; set; }//常见问题
        public string feedbackUrl { get; set; }//反馈
        public string serviceUrl { get; set; }//悠先服务
        public string personalImgInfo { get; set; }//个人图片信息
        public string vipImgUrl { get; set; }//VIP等级图片
        public int validPreorderCount { get; set; }
        public int notEvaluatedCount { get; set; }
        public List<VAPayMode> payModeList { get; set; }//支付方式 add by wang 20140403
        public List<RechargeActivitiesInfo> recharge { get; set; }//充值信息
        public bool isShowRechargeActivities { get; set; }//是否显示限时购粮票充值活动
        public bool isHaveEffectiveActivities { get; set; }//是否有在有效时间范围内的活动，客户端闹钟是否显示小红点
        public int defaultPayment { get; set; }//客户端默认支付方式
        public double executedRedEnvelopeAmount { get; set; }//已生效红包金额
        public double notExecutedRedEnvelopeAmount { get; set; }//未生效红包金额
        public string customerRedEnvelopeDetailUrl { get; set; }
        public string servicePhone { get; set; }//客服电话
        public int haveCouponCount { get; set; }//用户有几张券
        public bool haveCoupon { get; set; }//平台是否有抵扣券
        public bool haveUncheckCoupon { get; set; }//用户是否有未查看的抵扣券
        public ClientIntegralEntrance integralEntrance { get; set; }//客户端积分入口
    }

    /// <summary>
    /// 客户端积分入口
    /// </summary>
    public class ClientIntegralEntrance
    {
        /// <summary>
        /// 积分入口名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 积分入口URL
        /// </summary>
        public string url { get; set; }
    }

    /// <summary>
    /// 客户端查询点单评分所需信息request
    /// </summary>
    public class VAClientQueryPreorderEvaluationInfoRequest : VANetworkMessage
    {
        public long preorderId { get; set; }//点单编号
    }
    /// <summary>
    /// 客户端查询点单评分所需信息response
    /// </summary>
    public class VAClientQueryPreorderEvaluationInfoResponse : VANetworkMessage
    {
        public long preorderId { get; set; }//点单编号
        public string shopName { get; set; }
        public double preorderTime { get; set; }//点单入座时间
        public double preorderPaidSum { get; set; }
        public string preorderShareMsg { get; set; }
        public string complainUrl { get; set; }//投诉URL
        public int evaluationValue { get; set; }//[-1 5]，-1表示未评价，0表示暂不评价后面可以再次评价，1-5表示已评价且是评价的值
        public string shopLogoUrl { get; set; }//add by wangc 20140403 门店logo路径 

        public string foodDiariesUrl;//美食日记URL
        public string foodDiariesAfterShareUrl;//美食日记分享后URL,eg: 192.168.1.11/AppPages/FoodDiariesShow.aspx?id={0},{0}需要替换}
        public List<int> haveSharedType { get; set; }//点单已经分享的类型
        public string foodDiariesTitle { get; set; }

        public string evaluationContent { get; set; }

        public bool isEvaluation { get; set; }
    }
    /// <summary>
    /// 客户端评价点单request
    /// </summary>
    public class VAClientEvaluatePreorderRequest : VANetworkMessage
    {
        [UserIntegralParamsAttribute("preOrder19dianId")]
        public long preorderId { get; set; }//点单编号
        public short value { get; set; }//评价值[0-5]，0表示不评价
        /// <summary>
        /// 评价内容
        /// </summary> 
        public string evaluationContent { get; set; }
    }
    /// <summary>
    /// 客户端评价点单response
    /// </summary>
    public class VAClientEvaluatePreorderResponse : VANetworkMessage
    {
        public int value { get; set; }
    }
    /// <summary>
    /// 客户端查询最近的未评价的预点单request
    /// </summary>
    public class VAClientQueryPreorderNotEvaluatedRequest : VANetworkMessage
    {

    }
    /// <summary>
    /// 客户端评价点单response
    /// </summary>
    public class VAClientQueryPreorderNotEvaluatedResponse : VANetworkMessage
    {
        public long preorderId { get; set; }
    }

    /// <summary>
    /// 微信支付AccessToken / 微信公众号AccessToken
    /// </summary>
    public class WechatAccessToken
    {
        public string access_token { get; set; }
        public string expires_in { get; set; }//过期时间
    }

    /// <summary>
    /// 调用微信支付接口，生成预支付订单的返回结果
    /// </summary>
    public class WechatPrePayResult
    {
        public string prepayid;//预支付订单编号,
        public string errcode;//错误代码
        public string errmsg;//错误信息
    }
    /// <summary>
    /// 客户端发送语音短信消息
    /// </summary>
    public class VAClientSendVoiceMessageRequest : VANetworkMessage
    {
        public string mobilePhone { get; set; }
    }
    /// <summary>
    /// 客户端发送语音短信回复
    /// </summary>
    public class VAClientSendVoiceMessageResponse : VANetworkMessage
    {
        /// <summary>
        /// 验证码位数
        /// </summary>
        public int verificationCodeDigit { set; get; }
    }
    /// <summary>
    /// 客户端出错日志
    /// </summary>
    public class VACientErrorInfo
    {
        public string systemVersion { get; set; }//手机系统版本
        public string errorMessage { get; set; }//出错内容
        public string mobileModel { get; set; }//手机型号
        public string mobileBrand { get; set; }//手机品牌
    }
    /// <summary>
    /// 客户端发送错误日志消息
    /// </summary>
    public class VAClientSendErrorMessageRequest : VANetworkMessage
    {
        public string errorInfo { get; set; }//VACientErrorInfo结构的json字符串
    }
    /// <summary>
    /// 客户端发送错误日志回复
    /// </summary>
    public class VAClientSendErrorMessageResponse : VANetworkMessage
    {

    }
    /// <summary>
    /// 邮件结构
    /// </summary>
    public class VAEmailInfo
    {
        public string emailAddressTo { get; set; }
        public string messageBody { get; set; }
        public string subject { get; set; }
    }
    /// <summary>
    /// 客户端查询版本信息
    /// </summary>
    public class VAClientQueryBuildRequest : VANetworkMessage
    {
    }
    /// <summary>
    /// 客户端查询版本返回信息
    /// </summary>
    public class VAClientQueryBuildResponse : VANetworkMessage
    {
        public string latestBuild { get; set; }// 新版本号
        public string latestUpdateDescription { get; set; }// 新功能简介
        public string latestUpdateUrl { get; set; }// 更新版本的url
        public bool forceUpdate { get; set; }//客户端是否需要强制用户更新
    }
    public class RechargeActivitiesInfo
    {
        public int id { get; set; }//充值活动编号
        public string name { get; set; }//活动名称
        public string beginTime { get; set; }//活动开始时间
        public string endTime { get; set; }//活动结束时间
        public int externalSold { get; set; }//已售份数
        public int sequence { get; set; }//排序
    }
    public class VAClientFoodDiarySharedRequest : VANetworkMessage
    {
        public long foodDiaryId { set; get; }
        public string content { set; get; }
        public bool isBig { get; set; }
        public bool isHideDishName { get; set; }

        public FoodDiaryShared shared { set; get; }

        public List<VAClientFoodDiaryDish> dishes { set; get; }
    }
    public class VAClientFoodDiaryDish : FoodDiaryDish
    {
    }
    public class VAClientFoodDiarySharedResponse : VANetworkMessage
    {
    }
    /*
     20140504 客户端增加充值功能，需要调整对应的点单支付和直接支付接口，最新接口定义如下
     考虑老版本兼容问题，新接口实现方法重构
     wangc
    */
    /// <summary>
    /// 个人中心充值抢购粮票Request add by wangc 20140504
    /// </summary>
    public class ClientPersonCenterRechargeRequest : VANetworkMessage
    {
        public int rechargeActivityId { get; set; }//充值活动编号
        public int payMode { get; set; }//默认支付方式
        public bool boolDualPayment { get; set; }//是否二次支付，服务器调用
        public long rechargeOrderId { get; set; }//充值订单编号，服务器调用
        public long preOrder19dianId { get; set; }//点单编号，服务器充值后支付点单使用
    }
    /// <summary>
    /// 个人中心充值抢购粮票Reponse  add by wangc 20140504
    /// </summary>
    public class ClientPersonCenterRechargeReponse : VANetworkMessage
    {
        public string urlToContinuePayment { get; set; }//支付宝网页支付地址
        public string alipayOrder { get; set; }//支付宝客户端支付地址
        public string unionpayOrder { get; set; }//支付宝银联客户端地址
        public VAClientWechatPay ClientWechatPay { get; set; }//微信支付：客户端发送支付请求时所需参数
        public List<VAPayMode> payModeList { get; set; }//支付方式
    }
    /// <summary>
    /// 客户端带充值功能支付接口Request add by wangc 20140504
    /// </summary>
    public class ClientRechargePaymentOrderRequest : VANetworkMessage
    {
        public string orderInJson { get; set; }//选择菜品JSON
        public int shopId { get; set; }//门店编号
        public double clientCalculatedSum { get; set; }//客户端计算菜品原价
        public double clientUxianPriceSum { get; set; }//客户端计算菜品折扣后价格（已省=clientCalculatedSum-clientUxianPriceSum）

        [UserIntegralParamsAttribute("preOrder19dianId")]
        public long preOrderId { get; set; }//如果不传或者为0则为新增，否则为修改原来的点单
        public int isAddbyList { get; set; } //是否列表传过来（1不是，2是）
        public List<VASundryInfo> sundryList { get; set; }//杂项信息
        public int preOrderPayMode { get; set; }//默认支付方式
        public bool boolDualPayment { get; set; }//是否二次支付，服务器端使用，客户端不需要处理
        public int rechargeActivityId { get; set; }//充值活动编号
        public double clientStillNeedPaySum { get; set; }//客户端计算还需支付金额
        public int couponId { get; set; }//客户端选择使用优惠券编号

        /// <summary>
        /// 第三方支付帐号
        /// </summary>
        public string thirdPayAccount { get; set; }

        /// <summary>
        /// 第三方支付方式 <remarks>4:微信支付;5:支付宝支付</remarks>
        /// </summary>
        public int thirdPayType { get; set; }

        /// <summary>
        /// 第三方支付金额
        /// </summary>
        public double thirdPayAmount { get; set; }

        /// <summary>
        /// 客户端选择使用的低扣卷类型
        /// </summary>
        public int couponType { get; set; }
    }
    /// <summary>
    /// 客户端带充值功能支付接口Reponse add by wangc 20140504
    /// </summary>
    public class ClientRechargePaymentOrderResponse : VANetworkMessage
    {
        public double serverCalculatedSum { get; set; }//服务器计算菜品原价
        public double serverUxianPriceSum { get; set; }//客户端计算菜品折扣后价格
        public string urlToContinuePayment { get; set; }//支付宝网页支付地址
        public string alipayOrder { get; set; }//支付宝客户端支付地址
        public string unionpayOrder { get; set; }//支付宝银联客户端地址
        public long preOrderId { get; set; }//当前操作点单流水号
        public VAClientWechatPay ClientWechatPay { get; set; }//微信支付
        public string notPaymentReason { get; set; }//门店不支持付款原因
        public List<VAPayMode> payModeList { get; set; }//支付方式
        public List<RechargeActivitiesInfo> rechargeActivityList { get; set; }//充值活动列表
        public double rationBalance { get; set; }//粮票余额
        public double executedRedEnvelopeAmount { get; set; }
        public string serviceUrl { get; set; }//服务条款
        public double serverStillNeedPaySum { get; set; }//服务器计算还需支付金额
        public List<OrderPaymentCouponDetail> couponDetails { get; set; }

        /// <summary>
        /// 今日是否已使用红包支付
        /// </summary>
        public bool isUsedRedEnvelopeToday { get; set; }

        public Guid orderId { get; set; }
    }
    /// <summary>
    /// 客户端带充值功能直接支付接口Request add by wangc 20140504
    /// </summary>
    public class ClientRechargeDirectPaymentRequest : VANetworkMessage
    {
        public int shopId { get; set; }//门店编号
        public double payAmount { get; set; }//客户端支付金额

        [UserIntegralParamsAttribute("preOrder19dianId")]
        public long preOrderId { get; set; }//如果不传或者为0则为新增，否则为修改原来的点单
        public int preOrderPayMode { get; set; }//默认支付方式
        public bool boolDualPayment { get; set; }//是否二次支付，服务器端使用，客户端不需要处理
        public string deskNumber { get; set; }//桌号
        public int rechargeActivityId { get; set; }//充值活动编号
        public double clientStillNeedPaySum { get; set; }//客户端计算还需支付金额 
        public int couponId { get; set; }

        /// <summary>
        /// 第三方支付帐号
        /// </summary>
        public string thirdPayAccount { get; set; }

        /// <summary>
        /// 第三方支付方式 <remarks>4:微信支付;5:支付宝支付</remarks>
        /// </summary>
        public int thirdPayType { get; set; }

        /// <summary>
        /// 第三方支付金额
        /// </summary>
        public double thirdPayAmount { get; set; }

        /// <summary>
        /// 客户端返回的低扣卷类型1.用户自己抢的卷 2.店铺的卷
        /// </summary>
        public int couponType { get; set; }
    }
    public class ClientPaySuccessRequest : VANetworkMessage
    {
        public long preorderId { get; set; }
    }

    public class ClientpaySuccessResponse : VANetworkMessage
    {
    }
    /// <summary>
    /// 客户端带充值功能直接支付接口Reponse add by wangc 20140504
    /// </summary>
    public class ClientRechargeDirectPaymentResponse : VANetworkMessage
    {
        public string urlToContinuePayment { get; set; }//支付宝网页支付地址
        public string alipayOrder { get; set; }//支付宝客户端支付地址
        public string unionpayOrder { get; set; }//支付宝银联客户端地址
        public long preOrderId { get; set; }//当前操作点单流水号
        public VAClientWechatPay ClientWechatPay { get; set; }//微信支付
        public string notPaymentReason { get; set; }//门店不支持付款原因
        public List<VAPayMode> payModeList { get; set; }//支付方式
        public List<RechargeActivitiesInfo> rechargeActivityList { get; set; }//充值活动列表
        public double rationBalance { get; set; }//粮票余额
        public double executedRedEnvelopeAmount { get; set; }
        public string serviceUrl { get; set; }//服务条款
        public double serverStillNeedPaySum { get; set; }//服务器计算还需支付金额
        public List<OrderPaymentCouponDetail> couponDetails { get; set; }

        /// <summary>
        /// 今日是否已使用红包支付
        /// </summary>
        public bool isUsedRedEnvelopeToday { get; set; }
    }

    public class PayDiffenenceRequest : VANetworkMessage
    {
        public Guid orderId { get; set; }
        public long preorderId { get; set; }
        public int preOrderPayMode { get; set; }
        public double payDeifference { get; set; }

    }

    public class PayDiffenenceResponse : VANetworkMessage
    {
        public long preorderId { get; set; }
        public string alipayOrder { get; set; }
        public VAClientWechatPay ClientWechatPay { get; set; }//微信支付


    }

    public class PayDiffenenceQueryResponse : VANetworkMessage
    {
        public long preorderId { get; set; }
        public double rationBalance { get; set; }
        public double executedRedEnvelopeAmount { get; set; }//微信支付 

        public List<VAPayMode> payModeList { get; set; }
    }

    public class PayDiffenenceQueryRequest : VANetworkMessage
    {
        public Guid orderId { get; set; }
    }

    /// <summary>
    /// 悠先点菜客户端更新用户PushToken
    /// </summary>
    public class VAClientUpdatePushTokenRequest : VANetworkMessage
    {
        public string pushToken { get; set; }//设备推送码
    }
    public class VAClientUpdatePushTokenResponse : VANetworkMessage
    {

    }
    /// <summary>
    /// 悠先点菜更新设备的UUID
    /// </summary>
    public class VAClientUpdateUUIDRequest : VANetworkMessage
    {
        public string oldUUID { get; set; }//老格式的UUID
        public string newUUID { get; set; }//新格式的UUID
    }

    public class VAClientUpdateUUIDResponse : VANetworkMessage
    { }

    /// <summary>
    /// 悠先服务-客户信息请求
    /// </summary>
    public class ZZB_CustomerDetailsRequest : VANetworkMessage
    {
        public long customerId { get; set; }
        public int shopId { get; set; }
        public int pageNubmer { get; set; }
    }

    /// <summary>
    /// 悠先服务-客户信息回复
    /// </summary>
    public class ZZB_CustomerDetailsResponse : ZZB_CustomerPreOrdersResponse
    {
        public string custtomerName { get; set; }
        public short sex { get; set; }
        public string mobilePhoneNumber { get; set; }
        public string registerDate { get; set; }
        public double preOrderTotalAmount { get; set; }
    }

    /// <summary>
    /// 悠先服务-客户信息-消费记录(分页)回复
    /// </summary>
    public class ZZB_CustomerPreOrdersResponse : VANetworkMessage
    {
        public List<PreOrderLog> preOrders { get; set; }
        public bool nextPage { get; set; }
    }

    /// <summary>
    /// 消费记录
    /// </summary>
    public class PreOrderLog
    {
        public string shopLogo { get; set; }
        public string shopName { get; set; }
        public string preOrderDate { get; set; }
        public string preOrderAmount { get; set; }
        public string[] dishs { get; set; }
    }

    public class ZZB_PreOrderList2Response : VANetworkMessage
    {
        public List<PreOrderList2Model> preOrderList;
    }

    public class PreOrderList2Model
    {
        public string mobilePhoneNumber { get; set; }//手机号码
        public string customerUserName { get; set; }//顾客姓名
        public string prepaidTime { get; set; }//
        public long preOrder19dianId { get; set; }//流水号

        public int preOrderTotalQuantity { get; set; }
        public string customerPicture { get; set; }

        public bool hadSeat { get; set; }
        public bool hadInvoice { get; set; }

        public bool hadRefund { get; set; }

        public bool hadShared { get; set; }
        public string deskNumber { get; set; }
        public double afterDeductionPrice { get; set; }//抵扣后价格
        public double beforeDeductionPrice { get; set; }//抵扣前价格
        public bool hadCoupon { get; set; }//是否使用了抵扣券
        public long orderId { get; set; }//总订单号
    }

    public class ZZB_PreOrderListResponse : VANetworkMessage
    {
        public List<PreOrderListModel> preOrderList;
    }
    public class PreOrderListModel
    {
        public long preOrder19dianId { get; set; }//流水号
        public string customerUserName { get; set; }//顾客姓名
        public string mobilePhoneNumber { get; set; }//手机号码 
        public string prepaidTime { get; set; }//支付时间
        public bool hadSeat { get; set; }//是否已入座
        public double afterDeductionPrice { get; set; }//抵扣后价格
        public double beforeDeductionPrice { get; set; }//抵扣前价格
        public Guid orderId { get; set; }//总订单号
    }

    public class ZZB_PreOrderListAttachRequest : VANetworkMessage
    {
        public int shopId { get; set; }//门店编号
        public long[] preOrder19dianId;
        public Guid[] orderId { get; set; }//总订单号
    }

    public class ZZB_PreOrderListAttachResponse : VANetworkMessage
    {
        public List<PreOrderListAttachInfo> preOrderListAttachInfo { get; set; }
    }

    public class PreOrderListAttachInfo
    {
        public long preOrder19dianId { get; set; }//流水号
        public bool hadInvoice { get; set; }//是否开发票
        public bool hadRefund { get; set; }//是否有退款
        public string deskNumber { get; set; }//桌号 
        public string customerPicture { get; set; }//客户头像 
        public int preOrderTotalQuantity { get; set; }//客户订单数量    
        public bool hadShared { get; set; }//是否已分享
        public bool hadCoupon { get; set; }//是否使用了抵扣券
        public string mobilePhoneNumber { get; set; }//手机号码 
        public bool hadSeat { get; set; }//是否已入座
        public Guid orderId { get; set; }//总订单号

        public bool avoidQueue { get; set; }//是否免排队
    }

    /// <summary>
    /// 悠先服务点单列表请求
    /// </summary>
    public class ZZB_ModifyDeskNumberRequest : VANetworkMessage
    {
        public long preOrder19dianId { get; set; }
        public string deskNumber { get; set; }
    }

    /// <summary>
    /// 悠先服务点单列表回复
    /// </summary>
    public class ZZB_ModifyDeskNumberResponse : VANetworkMessage
    {

    }

    /// <summary>
    /// 悠先点菜客户端查看红包领取记录Request
    /// </summary>
    public class ClientCheckRedEnvelopeDetailsRequest : VANetworkMessage
    {
        public int pageSize { get; set; }
        public int pageIndex { get; set; }
    }
    /// <summary>
    /// 悠先点菜客户端查看红包领取记录Response
    /// </summary>
    public class ClientCheckRedEnvelopeDetailsResponse : VANetworkMessage
    {
        public List<ClientRedEnvelopeDetail> detailList { get; set; }
    }
    /// <summary>
    /// 悠先点菜客户端查看用户红包金额Request
    /// </summary>
    public class ClientCheckRedEnvelopeRequest : VANetworkMessage
    {
        public string mobilePhoneNumber { get; set; }//用户手机号码
    }
    /// <summary>
    /// 悠先点菜客户端查看用户红包金额Response
    /// </summary>
    public class ClientCheckRedEnvelopeResponse : VANetworkMessage
    {
        public double amount { get; set; }//用户领取的红包总金额（可使用+未生效）
        public int haveCouponCount { get; set; }//用户拥有抵扣券数量
        public bool haveUncheckCoupon { get; set; }//用户是否有未查看的抵扣券
        public List<CustomerCouponDetail> customerCouponDetail { get; set; }//用户抵扣券
        public bool isHaveMore { get; set; }//是否有下一页

        public bool enableClientConfirm { get; set; }//是否启用客户端入座功能


    }
    /// <summary>
    /// （悠先点菜）商圈Request
    /// </summary>
    public class ClientCheckBusinessDistrictRequest : VANetworkMessage
    {

    }
    /// <summary>
    /// （悠先点菜）商圈Response
    /// </summary>
    public class ClientCheckBusinessDistrictResponse : VANetworkMessage
    {
        public List<BusinessDistrictTag> tagList { get; set; }
    }
    [Serializable]
    [DataContract]
    public class BusinessDistrictTag
    {
        [DataMember]
        public int tagId { get; set; }
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public List<BusinessDistrictTag> childTags { get; set; }
    }
    /// <summary>
    /// （悠先点菜）美食广场Request
    /// </summary>
    public class ClientCheckFoodPlazaRequest : VANetworkMessage
    {
        public int pageSize { get; set; }
        public int pageIndex { get; set; }
    }
    /// <summary>
    /// （悠先点菜）美食广场Reponse
    /// </summary>
    public class ClientCheckFoodPlazaReponse : VANetworkMessage
    {
        public List<VABrandBannerList> vaAd { get; set; }
        public List<ClientFoodPlaza> foodPlazaList { get; set; }
        public bool isHaveMore { get; set; }
    }

    #region--------------------------------------------------
    /// <summary>
    /// 新增公告Request
    /// </summary>
    public class VAClientShopNoticeRequest : VANetworkMessage
    {
        /// <summary>
        /// 商家ID
        /// </summary>
        public int shopId { get; set; }
    }

    /// <summary>
    /// 新增公告Response
    /// </summary>
    public class VAClientShopNoticeResponse : VANetworkMessage
    {
        /// <summary>
        /// 公告H5url地址
        /// </summary>
        public string lotteryNoticeUrl;

        /// <summary>
        /// 公告版本号
        /// </summary>
        public long noticeVersion;

        /// <summary>
        /// 强制弹出时间戳
        /// </summary>
        public long noticeShowTime;
        /// <summary>
        /// 用户本日是否能抽奖
        /// </summary>
        public bool isCanLotteryToday;
        /// <summary>
        /// 不参与抽奖的描述
        /// </summary>
        public string notLotteryDesc;
    }

    /// <summary>
    /// 抽奖request
    /// </summary>
    public class VAClientLotteryRequest : VANetworkMessage
    {
        /// <summary>
        ///19dianID
        /// </summary>
        public long preorderId { get; set; }

        /// <summary>
        /// 主订单ID
        /// </summary>
        public Guid orderId { get; set; }
        /// <summary>
        /// 门店ID
        /// </summary>
        public int shopId { get; set; }
    }

    /// <summary>
    /// 抽奖response
    /// </summary>
    public class VAClientLotteryResponse : VANetworkMessage
    {
        public AwardInfo userAwardInfo { get; set; }//用户中奖信息
    }

    /// <summary>
    /// 统计列表请求
    /// </summary>
    public class CountingListRequest : VANetworkMessage
    {
        /// <summary>
        /// 门店ID
        /// </summary>
        public int shopId { get; set; }
    }

    /// <summary>
    /// 统计列表返回
    /// </summary>
    public class CountingListResponse : VANetworkMessage
    {
        /// <summary>
        /// 权限名称
        /// </summary>
        public List<ShopHaveAuthority> roles { get; set; }
    }

    #endregion

    [Serializable]
    [DataContract]
    /// <summary>
    /// 
    /// </summary>
    public class ClientFoodPlaza
    {
        [DataMember]
        public string personImgUrl { get; set; }//用户图像URL
        [DataMember]
        public List<string> dishUrlList { get; set; }//当前用户分享点单菜品图片URL集合
        [DataMember]
        public string foodDiaryUrl { get; set; }//当前用户分享点单美食日记页面URL
        [DataMember]
        public string shareContent { get; set; }
        [DataMember]
        public string customerName { get; set; }
        [DataMember]
        public int praisedCount { get; set; }
    }

    /// <summary>
    /// （悠先服务）配菜查询Request
    /// </summary>
    public class ZZBCheckDishIngredientsRequest : VANetworkMessage
    {
        public int shopId { set; get; }
        public string key { set; get; }
        public bool sellOff { set; get; }
        public int pageIndex { set; get; }
        public int pageSize { set; get; }
    }
    /// <summary>
    /// （悠先服务）配菜查询Reponse
    /// </summary>
    public class ZZBCheckDishIngredientsResponse : VANetworkMessage
    {
        public bool isHaveMore { get; set; }
        public List<DishIngredientsDetail> dishIngredientsList { get; set; }
    }
    public class DishIngredientsDetail
    {
        public int dishIngredientId { set; get; }
        public string dishIngredientName { set; get; }
        public double dishIngredientPrice { set; get; }
        public bool sellOff { set; get; }
    }

    /// <summary>
    /// （悠先服务）配菜沽清Request
    /// </summary>
    public class ZZBSellOffDishIngredientsRequest : VANetworkMessage
    {
        public int shopId { set; get; }
        public int dishIngredientId { set; get; }
        public bool sellOff { set; get; }
    }
    /// <summary>
    /// （悠先服务）配菜沽清Reponse
    /// </summary>
    public class ZZBSellOffDishIngredientsResponse : VANetworkMessage
    {

    }

    /// <summary>
    /// （悠先点菜）举报信息获取和举报操作Request
    /// </summary>
    public class VAClientShopReportRequest : VANetworkMessage
    {
        public int requestType { get; set; }//1：表示查询具备信息列表；2：表示提交举报信息
        public int shopId { get; set; }
        public int reportValue { get; set; }
    }
    /// <summary>
    /// （悠先点菜）举报信息获取和举报操作Response
    /// </summary>
    public class VAClientShopReportResponse : VANetworkMessage
    {
        public List<ShopReportInfo> reportList { get; set; }
    }

    /// <summary>
    /// 菜品点赞Request
    /// </summary>
    public class VAClientDishPraiseRequest : VANetworkMessage
    {
        public List<int> dishIdList { get; set; }

        [UserIntegralParamsAttribute("preOrder19dianId")]
        public long preOrderId { get; set; }
    }
    /// <summary>
    /// 菜品点赞Response
    /// </summary>
    public class VAClientDishPraiseResponse : VANetworkMessage
    {

    }

    /// <summary>
    /// 查看个人优惠券列表Request
    /// </summary>
    public class VAClientCouponPacketDetailRequest : VANetworkMessage
    {
        public int pageIndex { get; set; }
        public int pageSize { get; set; }
    }
    /// <summary>
    /// 查看个人优惠券列表Response
    /// </summary>
    public class VAClientCouponPacketDetailResponse : VANetworkMessage
    {
        public List<ClientCouponPacketDetail> couponPacketListDetails { get; set; }
        public bool isHaveMore { get; set; }
    }

    /// <summary>
    /// 查看个人优惠券列表Response
    /// </summary>
    public class VAClientRebateDetailResponse : VANetworkMessage
    {
        public List<ClientDeductionVolumeDetail> couponPacketListDetails { get; set; }
        public bool isHaveMore { get; set; }
    }

    /// <summary>
    /// 悠先点菜剪刀手入座接口Request
    /// </summary>
    public class VAClientPreOrderConfirmRequest : VANetworkMessage
    {
        /// <summary>
        ///点单Id
        /// </summary>
        [UserIntegralParamsAttribute]
        public long preOrder19dianId { get; set; }
    }
    /// <summary>
    /// 悠先点菜剪刀手入座接口Response
    /// </summary>
    public class VAClientPreOrderConfirmResponse : VANetworkMessage
    {

    }
    /// <summary>
    /// 悠先点菜未入座点单提醒 Request
    /// </summary>
    public class VAClientUnConfirmPreOrderRequest : VANetworkMessage
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public long customerId { get; set; }
    }
    /// <summary>
    /// 悠先点菜未入座点单提醒 Response
    /// </summary>
    public class VAClientUnConfirmPreOrderResponse : VANetworkMessage
    {
        public List<PreOrder19dianRemindInfo> preOrder19dianRemindInfo { get; set; }
        /// <summary>
        /// 指定范围（单位：米）
        /// </summary>
        public double range { get; set; }
    }


    /// <summary>
    /// 会员模块中门店信息 <remarks>参看"H5页面接口.xslx" query_shop_detail</remarks>
    /// </summary>
    public class ZZBShopInfo
    {
        public int shopId
        {
            get;
            set;
        }
        public int shopLevel
        {
            get;
            set;
        }
        public long prepayOrderCount
        {
            get;
            set;
        }
        public string shopName
        {
            get;
            set;
        }
        public int customerCount
        {
            get;
            set;
        }
        public int customerAddedCount
        {
            get;
            set;
        }
        public int shopScore
        {
            get;
            set;
        }
        public int upgradeScore
        {
            get;
            set;
        }
        public List<evaluationDetail> goodEvaluationList
        {
            get;
            set;
        }

        public List<evaluationDetail> middleEvaluationList
        {
            get;
            set;
        }

        public List<evaluationDetail> badEvaluationList
        {
            get;
            set;
        }
        public List<EvaluationPercent> evaluationPercent
        {
            get;
            set;
        }
        public List<string> activityList
        {
            get;
            set;
        }
        public couponDetail currentCoupon
        {
            get;
            set;
        }

        /// <summary>
        /// 查看统计权限 <remarks> 控制"统计"按钮是否显示</remarks> 
        /// </summary>
        public bool staticsRight
        {
            get;
            set;
        }

        /// <summary>
        /// 发送抵扣券权限 <remarks> 控制"发送抵扣券"按钮是否显示</remarks> 
        /// </summary>
        public bool sendCouponRight
        {
            set;
            get;
        }

    }

    /// <summary>
    /// 抵扣券详细信息 <remarks>参看"H5页面接口.xslx" query_shop_detail</remarks>
    /// </summary>
    public class couponDetail
    {
        public double requirementMoney
        {
            get;
            set;
        }
        public double deductibleAmount
        {
            get;
            set;
        }
        public int sendCount
        {
            get;
            set;
        }
        public int yesterdaySendCount
        {
            get;
            set;
        }
        public double totalAmount
        {
            get;
            set;
        }
        public double yesterdayAmount
        {
            get;
            set;
        }
        public int currentCouponId
        {
            get;
            set;
        }

    }

    /// <summary>
    /// 评价信息列表 <remarks>参看"H5页面接口.xslx" query_evaluation_list</remarks>
    /// </summary>
    public class EvaluationList
    {
        public List<evaluationDetail> evaluationList { get; set; }

        public bool isMore { get; set; }
    }

    #region 微信 2015.4.17
    /// <summary>
    /// 微信是否绑定
    /// </summary>
    public class VAClientWeChatIsBindingRequest : VANetworkMessage
    {
        /// <summary>
        /// 用户微信号
        /// </summary>
        public string unionId { set; get; }
    }

    public class VAClientWeChatIsBindingResponse : VANetworkMessage
    {
        /// <summary>
        /// 0未注册,1已注册,2未找到uid但有手机号3有uid找不到手机号
        /// </summary>
        public int state { set; get; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string mobile { set; get; }
    }

    /// <summary>
    /// 微信用户绑定手机号
    /// </summary>
    public class VAClientWeChatBindingMobileRequest : VANetworkMessage
    {
        public string pushToken { set; get; }

        public int shopId { get; set; }//在点菜页面登录时带此编号用以查询用户在该店铺的折扣

        /// <summary>
        /// 微信号
        /// </summary>
        public string unionId { set; get; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string mobilePhoneNumber { set; get; }

        /// <summary>
        /// 验证码
        /// </summary>
        public string verificationCode { set; get; }
    }

    /// <summary>
    /// 微信用户绑定手机号
    /// </summary>
    public class VAClientWeChatBindingMobileResponse : VANetworkMessage
    {
        public VAUserInfo userInfo;
        public double userCurrentShopDiscount { get; set; }//如果request中上传了店铺编号，则返回对应的该店铺的折扣值
        public bool isNewMobile { get; set; }
        public double rationBalance { get; set; }
        public double executedRedEnvelopeAmount { get; set; }
        public List<OrderPaymentCouponDetail> couponDetails { get; set; }//可参与支付抵扣金额优惠券列表
        public string deviceLockMessage { get; set; }//设备封锁信息
    }

    /// <summary>
    /// 微信用户绑定手机号
    /// </summary>
    public class VAClientWeChatIsBindingMobileRequest : VANetworkMessage
    {
        /// <summary>
        /// 微信id
        /// </summary>
        public string unionId { set; get; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string mobilePhoneNumber { set; get; }
    }

    /// <summary>
    /// 微信用户绑定手机号
    /// </summary>
    public class VAClientWeChatIsBindingMobileResponse : VANetworkMessage
    {
        /// <summary>
        /// 是否绑定(0未绑定,1已绑定)
        /// </summary>
        public int isBinding { set; get; }
    }
    #endregion


}