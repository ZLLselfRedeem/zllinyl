using System;
using System.Transactions;
using Microsoft.VisualBasic.CompilerServices;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL;
using VAGastronomistMobileApp.WebPageDll.Services.Infrastructure;
using System.Threading;
using System.Data;
using VA.Cache.Distributed;

namespace VAGastronomistMobileApp.WebPageDll.Services
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDishPriceInfoService
    {
        /// <summary>
        /// 修改菜品价格,并生成菜品更新任务
        /// </summary>
        /// <param name="dishPriceId"></param>
        /// <param name="price"></param>
        /// <param name="employeeId"></param>
        //[Transaction]
        Task ModifyDishPriceAndReturnDishMenuUpdateTask(int dishPriceId, double price, int employeeId);
    }

    /// <summary>
    /// 
    /// </summary>
    public class DishPriceInfoService : BaseService, IDishPriceInfoService
    {
        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="repositoryContext"></param>
        public DishPriceInfoService(IRepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public Task ModifyDishPriceAndReturnDishMenuUpdateTask(int dishPriceId, double price, int employeeId)
        {
            var dishPrice = RepositoryContext.GetDishPriceInfoRepository().GetById(dishPriceId);
            if (dishPrice != null && dishPrice.DishID.HasValue)
            {
                var dish = RepositoryContext.GetDishInfoRepository().GetById(dishPrice.DishID.Value);
                if (dish != null && dish.MenuID.HasValue)
                {
                    int menuId = dish.MenuID.Value;
                    var task = new MenuUpdateTask
                    {
                        CreateTime = DateTime.Now,
                        Status = TaskStatus.未开始,
                        MenuId = menuId,
                        EmployeeId = employeeId
                    };

                    DataTable dtable = new MenuConnShopOperate().QueryShopsByMenuId(menuId);
                    string key = "shopOfMenu_" + Common.ToString(dtable.Rows[0]["shopId"]);
                    if (MemcachedHelper.IsKeyExists(key))
                    {
                        MemcachedHelper.DeleteMemcached(key);
                    }

                    using (var scope = new TransactionScope())
                    {
                        try
                        {
                            RepositoryContext.GetDishPriceInfoRepository().UpdatePrice(dishPrice.DishPriceID, price);
                            RepositoryContext.GetMenuUpdateTaskRepository().Add(task);
                            scope.Complete();

                            var employeeOperateLogInfo = new EmployeeOperateLogInfo
                            {
                                employeeId = employeeId,
                                operateTime = DateTime.Now,
                                operateType = 2,
                                pageType = 83,
                                employeeName = "",
                                operateDes = string.Format("悠先服务修改菜品{0}价格为{1:F}", dishPriceId, price)
                            };

                            //开启单独线程插入数据
                            var threadstart = new ParameterizedThreadStart(Common.InsertEmployeeOperateLog);
                            var thread = new Thread(threadstart) { IsBackground = true };
                            thread.Start(employeeOperateLogInfo);
                        }
                        catch (Exception)
                        {

                            //throw;
                        }
                    }


                    //新的
                    //RepositoryContext.GetEmployeeOperateLogRepository().Add(new EmployeeOperateLogInfo()
                    //{
                    //    employeeId = employeeId,
                    //    operateTime = DateTime.Now,
                    //    operateType = 2,
                    //    pageType = 83,
                    //    employeeName = "",
                    //    operateDes = string.Format("悠先服务修改菜品{0}价格为{1:F}", dishPriceId, price)
                    //});



                    

                    return task;
                }
            }

            return null;
        }
    }
}
