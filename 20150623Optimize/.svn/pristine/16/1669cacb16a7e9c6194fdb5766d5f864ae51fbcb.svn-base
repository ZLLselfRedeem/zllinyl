using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL;
using VAGastronomistMobileApp.WebPageDll.Services;
using VAGastronomistMobileApp.WebPageDll.Services.Infrastructure;

namespace VAGastronomistMobileApp.WebPageDll
{
    /// <summary>
    /// 配菜沽清业务逻辑层
    /// </summary>
    public class CurrectIngredientsSellOffInfoOperate
    {
        private readonly ICurrectIngredientsSellOffInfoManager manager;
        private int shopId = 0;
        private int companyId = 0;
        public CurrectIngredientsSellOffInfoOperate()
        {
            manager = new CurrectIngredientsSellOffInfoManager();
            shopId = Common.ToInt32(HttpContext.Current.Session["loginshop"]);
            companyId = Common.ToInt32(HttpContext.Current.Session["logincompany"]);
        }

        /// <summary>
        /// 新增沽清信息
        /// </summary>
        /// <param name="ingredientsId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public string IngredientsSellOffOperate(int ingredientsId, int status)
        {
            if (shopId <= 0 || companyId <= 0)
            {
                return "操作超时，请退出登录重试！";
            }
            var model = new CurrectIngredientsSellOffInfo()
            {
                Id = 0,
                ingredientsId = ingredientsId,
                shopId = shopId,
                companyId = companyId,
                status = true,
                operateEmployeeId = ((VAEmployeeLoginResponse)HttpContext.Current.Session["MerchantsTreasureUserInfo"]).employeeID,
                expirationTime = Common.ToDateTime(DateTime.Now.ToString("yyyy/MM/dd") + " 23:59:59"),
                operateTime = DateTime.Now
            };
            bool exit = manager.Exists(model);//是否存在沽清记录
            if (status == 1)//沽清操作
            {
                if (exit)
                {
                    return "已被沽清";
                }
                else
                {
                    return manager.Add(model) ? "ok" : "沽清失败";
                }
            }
            else//取消沽清操作
            {
                if (exit)
                {
                    model.status = false;
                    return manager.Update(model) ? "ok" : "取消沽清失败";
                }
                else
                {
                    return "沽清记录不存在";
                }
            }
        }

        /// <summary>
        /// 查询当前公司当前门店所有沽清配菜编号
        /// </summary>
        /// <returns></returns>
        public List<int> Select()
        {
            return manager.Select(companyId, shopId);
        }

        /// <summary>
        /// 查询当前门店所有沽清配菜编号
        /// </summary>
        /// <param name="_shopId"></param>
        /// <returns></returns>
        public List<int> Select(int _shopId)
        {
            return manager.Select(_shopId);
        }

        /// <summary>
        /// （悠先服务）配菜查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ZZBCheckDishIngredientsResponse ZZBCheckDishIngredients(ZZBCheckDishIngredientsRequest request)
        {
            var response = new ZZBCheckDishIngredientsResponse()
            {
                type = VAMessageType.ZZB_CHECK_DISHINGREDIENTS_REPONSE,
                cookie = request.cookie,
                uuid = request.uuid
            };
            CheckCookieAndMsgForZZZ checkResult = Common.CheckCookieAndMsgForZZZ(request.cookie, (int)request.type, (int)VAMessageType.ZZB_CHECK_DISHINGREDIENTS_REQUEST);
            if (checkResult.result == VAResult.VA_OK)
            {
                int employeeId = Common.ToInt32(checkResult.dtEmployee.Rows[0]["EmployeeID"]);
                bool isViewAllocWorker = Common.ToBool(checkResult.dtEmployee.Rows[0]["isViewAllocWorker"]);
                if (ServiceFactory.Resolve<IShopAuthorityService>().GetEmployeeHasShopAuthority(request.shopId, employeeId, isViewAllocWorker, ShopRole.配菜沽清.GetString()))
                {
                    request.key = String.IsNullOrEmpty(request.key) ? "" : request.key;

                    List<DishIngredients> allDishIngredients = manager.GetShopDishIngredients(request.shopId, request.key);//门店菜谱下所有配菜
                    List<int> allSellOffIngredientIds = Select(request.shopId);//门店菜谱下所有沽清配菜编号
                    List<DishIngredientsDetail> dishIngredientsList = (from q in allDishIngredients
                                                                       select new DishIngredientsDetail()
                                                                       {
                                                                           dishIngredientId = q.ingredientsId,
                                                                           dishIngredientName = q.ingredientsName,
                                                                           dishIngredientPrice = q.ingredientsPrice,
                                                                           sellOff = allSellOffIngredientIds.Any(p => p == q.ingredientsId)//是否沽清
                                                                       }).ToList();
                    if (String.IsNullOrWhiteSpace(request.key))//不是搜索
                    {
                        dishIngredientsList = request.sellOff == true ? dishIngredientsList.Where(n => n.sellOff == true).ToList() : dishIngredientsList.Where(n => n.sellOff == false).ToList();
                    }
                    response.isHaveMore = dishIngredientsList.Count > request.pageIndex * request.pageSize;
                    response.dishIngredientsList = dishIngredientsList.Skip((request.pageIndex - 1) * request.pageSize).Take(request.pageSize).ToList();//分页
                    response.result = VAResult.VA_OK;
                }
                else
                {
                    response.result = VAResult.VA_NO_ACCESS_INTERFACE_AUTHORITY;
                }
            }
            else
            {
                response.result = checkResult.result;
            }
            return response;
        }

        /// <summary>
        /// （悠先服务）配菜沽清
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ZZBSellOffDishIngredientsResponse ZZBSellOffDishIngredients(ZZBSellOffDishIngredientsRequest request)
        {
            var response = new ZZBSellOffDishIngredientsResponse()
            {
                type = VAMessageType.ZZB_SELLOFF_DISHINGREDIENTS_REPONSE,
                cookie = request.cookie,
                uuid = request.uuid
            };
            CheckCookieAndMsgForZZZ checkResult = Common.CheckCookieAndMsgForZZZ(request.cookie, (int)request.type, (int)VAMessageType.ZZB_SELLOFF_DISHINGREDIENTS_REQUEST);
            if (checkResult.result == VAResult.VA_OK)
            {
                int employeeId = Common.ToInt32(checkResult.dtEmployee.Rows[0]["EmployeeID"]);
                bool isViewAllocWorker = Common.ToBool(checkResult.dtEmployee.Rows[0]["isViewAllocWorker"]);
                if (ServiceFactory.Resolve<IShopAuthorityService>().GetEmployeeHasShopAuthority(request.shopId, employeeId, isViewAllocWorker, ShopRole.配菜沽清.GetString()))
                {
                    var shop = new ShopOperate().QueryShop(request.shopId);
                    if (shop == null || request.dishIngredientId <= 0)
                    {
                        response.result = VAResult.VA_FAILED_DB_ERROR;
                        return response;
                    }
                    var model = new CurrectIngredientsSellOffInfo()
                    {
                        Id = 0,
                        ingredientsId = request.dishIngredientId,
                        shopId = request.shopId,
                        companyId = shop.companyID,
                        status = true,
                        expirationTime = Common.ToDateTime(DateTime.Now.ToString("yyyy/MM/dd") + " 23:59:59"),
                        operateTime = DateTime.Now,
                        operateEmployeeId = employeeId
                    };
                    bool exit = manager.Exists(model);//是否存在沽清记录
                    if (request.sellOff == true)//沽清操作
                    {
                        if (exit)
                        {
                            response.result = VAResult.VA_OK;
                        }
                        else
                        {
                            response.result = manager.Add(model) ? VAResult.VA_OK : VAResult.VA_FAILED_DB_ERROR;
                        }
                    }
                    else//取消沽清操作
                    {
                        if (exit)
                        {
                            model.status = false;
                            response.result = manager.Update(model) ? VAResult.VA_OK : VAResult.VA_FAILED_DB_ERROR;
                        }
                        else
                        {
                            response.result = VAResult.VA_OK;
                        }
                    }
                }
                else
                {
                    response.result = VAResult.VA_NO_ACCESS_INTERFACE_AUTHORITY;
                }
            }
            else
            {
                response.result = checkResult.result;
            }
            return response;
        }
    }
}
