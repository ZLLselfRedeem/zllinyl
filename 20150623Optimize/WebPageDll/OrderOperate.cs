using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL;

namespace VAGastronomistMobileApp.WebPageDll
{
   public  class OrderOperate
    {
       private readonly OrderManager orderManager = new OrderManager();

        /// <summary>
        /// 查询某些orderId对应的支付金额信息
        /// </summary>
        /// <param name="orderIds"></param>
        /// <returns></returns>
       public List<OrderPaidDetail> GetOrderPaidList(Guid[] orderIds)
       {
           return orderManager.GetOrderPaidList(orderIds);
       }

       /// <summary>
        /// 查询orderId对应的支付金额信息
        /// </summary>
        /// <param name="orderIds"></param>
        /// <returns></returns>
       public OrderPaidDetail GetOrderPaidDeatial(Guid orderId)
       {
           return orderManager.GetOrderPaidDeatial(orderId);
       }

       public static bool Add(Order entity)
       {
           OrderManager orderManager = new OrderManager();
           return orderManager.Add(entity); 
       }

       public static bool Update(Order entity)
       {
           OrderManager orderManager = new OrderManager();
           return orderManager.Update(entity); 
       }

       public static Order GetEntityById(Guid id)
       {
           OrderManager orderManager = new OrderManager();
           return orderManager.GetEntityById(id); 
       }
    }
}
