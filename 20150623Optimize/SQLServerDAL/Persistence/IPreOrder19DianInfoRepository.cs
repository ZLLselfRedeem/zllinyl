using System.Collections.Generic;
using PagedList;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL.Persistence.Infrastructure;

namespace VAGastronomistMobileApp.SQLServerDAL.Persistence
{
    public interface IPreOrder19DianInfoRepository
    {
        PreOrder19dianInfo GetById(long id);
        /// <summary>
        /// 修改订单退款状态及金额 
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="money"></param>
        void UpdateOrderRefundStatusAndMoney(long orderId, double money, int refundStatus);

        /// <summary>
        /// 修改订单退款金额
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="money"></param>
        void UpdateOrderRefundMoney(long orderId, double money);

        /// <summary>
        /// 获取用户已支付过的订单列表
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        IPagedList<CustomerPreOrder> GetPageCustomerPayforOrders(Page page, long customerId);

        /// <summary>
        /// 获取门店以支付未对帐订单列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="shopId"></param>
        /// <returns></returns>
        IPagedList<ShopPreOrder> GetPageNoApprovedShopOrders(Page page, int shopId);

        /// <summary>
        /// 修改订单桌号
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="deskNumber"></param>
        void UpdateOrderDeskNumber(long orderId, string deskNumber);
    }
}