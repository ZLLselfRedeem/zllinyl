using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using VAGastronomistMobileApp.Model;

namespace VAGastronomistMobileApp.IDAL
{
    public interface IOrderManager
    {
        /// <summary>
        /// 新增点单
        /// </summary>
        /// <param name="dishOrder"></param>
        int InsertOrder(OrderInfo dishOrder);
        /// <summary>
        /// 新增点单详情
        /// </summary>
        /// <param name="dishOrderDetail"></param>
        int InsertOrderDetail(OrderDetailInfo dishOrderDetail);
        /// <summary>
        /// 删除点单
        /// </summary>
        /// <param name="orderID"></param>
        void DeleteOrder(int orderID);
        /// <summary>
        /// 删除点单详情
        /// </summary>
        /// <param name="orderDetailID"></param>
        bool DeleteOrderDetail(int orderDetailID, string orderDetailNote);
        /// <summary>
        /// 修改点单
        /// </summary>
        /// <param name="dishOrder"></param>
        void UpdateOrder(OrderInfo dishOrder);
        /// <summary>
        /// 修改点单状态
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        bool UpdateOrderStatus(int orderID, int status);
        /// <summary>
        /// 修改点单总价
        /// </summary>
        /// <param name="orderID"></param>
        bool UpdateOrderTotalPay(int orderID, double totalToPay);
        /// <summary>
        /// 修改点单详情
        /// </summary>
        /// <param name="dishOrderDetail"></param>
        /// <returns></returns>
        bool UpdateOrderDetail(OrderDetailInfo dishOrderDetail);
        /// <summary>
        /// 根据点单编号查询点单详情
        /// </summary>
        DataTable QueryOrderDetail(int orderID);
        /// <summary>
        /// 根据点单编号查询对应点单信息
        /// </summary>
        DataTable QueryOrder(int orderID);
    }
}
