using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL;

namespace VAGastronomistMobileApp.WebPageDll
{
    public partial class PreOrder19dianOperate
    {
        /// <summary>
        /// 点单菜品点赞
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public VAClientDishPraiseResponse ClientDishPraise(VAClientDishPraiseRequest request)
        {
            VAClientDishPraiseResponse response = new VAClientDishPraiseResponse()
            {
                type = VAMessageType.CLIENT_DISH_PRAISE_RESPONSE,
                cookie = request.cookie,
                uuid = request.uuid
            };

            CheckCookieAndMsgtypeInfo checkResult = Common.CheckCookieAndMsgtype(request.cookie, request.uuid, (int)request.type, (int)VAMessageType.CLIENT_DISH_PRAISE_REQUEST);
            if (checkResult.result == VAResult.VA_OK)
            {
                if (Common.CheckLatestBuild_January(request.appType, request.clientBuild) == false)
                {
                    response.result = VAResult.VA_VERSION_IS_TOO_LOW;//老版本不处理，可直接return va_ok
                    return response;
                }
                if (request.dishIdList == null || !request.dishIdList.Any())
                {
                    response.result = VAResult.VA_OK;
                    return response;
                }
                string newOrderJson = preorder19dianMan.GetPreOrder19DianExtendOrderJson(request.preOrderId);
                if (String.IsNullOrWhiteSpace(newOrderJson))
                {
                    response.result = VAResult.VA_FAILED_ORDERID_NOT_FOUND;
                    return response;
                }
                List<PreOrderIn19dianOrderJson> listOrderInfo = JsonOperate.JsonDeserialize<List<PreOrderIn19dianOrderJson>>(newOrderJson);
                if (listOrderInfo == null || !listOrderInfo.Any())
                {
                    response.result = VAResult.VA_FAILED_OTHER;
                    return response;
                }
                var list = new List<DishPraiseModel>();
                foreach (var dish in listOrderInfo)
                {
                    if (request.dishIdList.Contains(dish.dishId))
                    {
                        dish.isHavePraise = true;
                        list.Add(new DishPraiseModel()
                        {
                            DishID = dish.dishId,
                            Count = 1
                        });
                    }
                }
                newOrderJson = JsonOperate.JsonSerializer<List<PreOrderIn19dianOrderJson>>(listOrderInfo);
                using (TransactionScope ts = new TransactionScope())
                {
                    string dishIds = CommonPageOperate.SplicingListStr(list, "DishID");
                    bool flag1 = preorder19dianMan.UpdateOrderJson(newOrderJson, request.preOrderId);
                    bool falg2 = preorder19dianMan.UpdateDishInfoPraiseNum(dishIds);//更新点单菜品点赞次数，待处理
                    if (flag1 && falg2)
                    {
                        response.result = VAResult.VA_OK;
                        ts.Complete();
                    }
                }
            }
            else
            {
                response.result = checkResult.result;
            }
            return response;
        }
        /// <summary>
        /// 更新点单和杂项Json
        /// </summary>
        /// <param name="preOrder19DianId"></param>
        /// <param name="orderJson"></param>
        /// <param name="sundryJson"></param>
        /// <param name="preOrderSum"></param>
        /// <returns></returns>
        public bool ModifyPreOrderAndSundryJson(long preOrder19DianId, string orderJson, string sundryJson, double preOrderSum)
        {
            bool flag1 = preorder19dianMan.UpdatePreOrderAndSundryJson(preOrder19DianId, orderJson, sundryJson, preOrderSum);//更新主表json
            bool flag2 = preorder19dianMan.UpdateOrderJsonAndSundryJson(ConvertOrderJson(orderJson), sundryJson, preOrder19DianId);//更新扩展表json
            return flag1 && flag2;
        }

        /// <summary>
        /// 转换OrderJson，添加是否点赞字段存储
        /// </summary>
        /// <param name="orderJson"></param>
        /// <returns></returns>
        private string ConvertOrderJson(string orderJson)
        {
            if (string.IsNullOrWhiteSpace(orderJson))//点菜支付
            {
                return "";
            }
            List<PreOrderIn19dian> listOrderInfo = JsonOperate.JsonDeserialize<List<PreOrderIn19dian>>(orderJson);
            if (!listOrderInfo.Any())
            {
                return "";
            }
            List<PreOrderIn19dianOrderJson> newListOrderInfo = new List<PreOrderIn19dianOrderJson>();
            foreach (var item in listOrderInfo)
            {
                newListOrderInfo.Add(new PreOrderIn19dianOrderJson()
                {
                    dishId = item.dishId,
                    dishName = item.dishName,
                    unitPrice = item.unitPrice,
                    quantity = item.quantity,
                    dishPriceName = item.dishPriceName,
                    dishPriceI18nId = item.dishPriceI18nId,
                    dishTypeName = item.dishTypeName,
                    markName = item.markName,
                    vipDiscountable = item.vipDiscountable,
                    dishTaste = item.dishTaste,
                    dishIngredients = item.dishIngredients,
                    isHavePraise = false
                });
            }
            return JsonOperate.JsonSerializer<List<PreOrderIn19dianOrderJson>>(newListOrderInfo);
        }

        /// <summary>
        /// 新增预点单信息
        /// </summary>
        /// <param name="preOrder19Dian"></param>
        /// <returns></returns>
        public long AddPreOrder19Dian(PreOrder19dianInfo preOrder19Dian)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                long orderId = preorder19dianMan.InsertPreOrder19dian(preOrder19Dian);
                preOrder19Dian.preOrder19dianId = orderId;
                bool flag = preorder19dianMan.InsertPreOrder19DianExtend(ConvertOrderJson(preOrder19Dian.orderInJson), Common.ToString(preOrder19Dian.sundryJson), orderId) > 0;
                if (orderId <= 0 || flag == false)
                {
                    return 0;
                }
                ts.Complete();
                return orderId;
            }
        }

        public static double GetExtendPayByPreOrder19DianId(long preOrder19DianId)
        {
            return new PreOrder19dianManager().GetExtendPayByPreOrder19DianId(preOrder19DianId);
        }

        /// <summary>
        /// 查询某订单是否有扩展支付金额
        /// </summary>
        /// <param name="preOrder19DianId"></param>
        /// <returns></returns>
        public double SelectExtendPay(long preOrder19DianId)
        {
            return preorder19dianMan.SelectExtendPay(preOrder19DianId);
        }

        /// <summary>
        /// 按用户id返回订单数
        /// </summary>
        /// <param name="mobile">手机号</param>
        /// <returns></returns>
        public Tuple<int, decimal> GetMobileOrdersCount(string mobile)
        {
            return new PreOrder19dianManager().GetMobileOrdersCount(mobile);
        }
        /// <summary>
        /// 查询点单扩展表OrderJson信息
        /// </summary>
        /// <param name="preOrderId"></param>
        /// <returns></returns>
        public string GetPreOrder19DianExtendOrderJson(long preOrderId)
        {
            return preorder19dianMan.GetPreOrder19DianExtendOrderJson(preOrderId);
        }
        
         //-------------------------------------------------------------------------------------------------

        /// <summary>
        /// 更新点单PreOrder19dianExtend 的 OrderInJson
        /// </summary>
        /// <param name="orderJson"></param>
        /// <param name="preOrderId"></param>
        /// <returns></returns>
        public bool UpdateOrderJson(string orderJson, long preOrderId)
        {
            return preorder19dianMan.UpdateOrderJson(orderJson, preOrderId);
        }

        /// <summary>
        /// 检查订单是否符合已支付为入座且支付时间是当日
        /// </summary>
        /// <param name="preOrder19dianId"></param>
        /// <returns></returns>
        public bool PreOrderIsToday(long preOrder19dianId)
        {
            return preorder19dianMan.PreOrderIsToday(preOrder19dianId);
        }

        /// <summary>
        /// 检查用户是否只有一个点单
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public bool IsOnePaidOrder(long customerId)
        {
            return preorder19dianMan.IsOnePaidOrder(customerId);
        }
    }
}
