using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.SQLServerDAL;
using VAGastronomistMobileApp.Model;
using System.Data;

namespace VAGastronomistMobileApp.WebPageDll
{
    /// <summary> 
    /// FileName: MenuShopOperate.cs 
    /// CLRVersion: 4.0.30319.269 
    /// Author: TDQ 
    /// Corporation:杭州友络科技有限公司
    /// Description: 
    /// DateTime: 2012-08-17 10:08:02 
    /// </summary>
    public class MenuConnShopOperate
    {
        MenuConnShopManager menuShopManager = new MenuConnShopManager();
        /// <summary>
        /// 添加menushop
        /// </summary>
        /// <param name="menuConnShop"></param>
        /// <returns></returns>
        public int AddMenuShop(MenuConnShop menuConnShop)
        {
            return menuShopManager.InsertMenuConnShop(menuConnShop);
        }
        /// <summary>
        /// 根据菜谱编号删除其所有与店铺的关系信息
        /// </summary>
        /// <param name="menuId"></param>
        /// <returns></returns>
        public bool RemoveMenuConnShop(int menuId)
        {
            return menuShopManager.DeleteMenuConnShop(menuId);
        }
        /// <summary>
        /// 删除菜谱公司关系信息
        /// </summary>
        /// <param name="menuId"></param>
        public bool RemoveMenuConnCompany(int menuId)
        {
            return menuShopManager.DeleteMenuConnCompany(menuId);
        }
        /// <summary>
        /// 根据店铺Id删除其与菜谱对应关系 2014-2-14
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public bool RemoveMenuConnShopByShopId(int shopId)
        {
            return menuShopManager.DeleteMenuConnShopByShopId(shopId);
        }

        /// <summary>
        /// 根据菜谱id，查询相应的店铺
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public DataTable QueryShopsByMenuId(int menuId)
        {
            return menuShopManager.SelectShopsByMenuId(menuId);
        }
        /// <summary>
        /// 根据门店id，查询相应的菜谱
        /// </summary>
        /// <returns></returns>
        public DataTable QueryMenusByShopId(int shopId)
        {
            return menuShopManager.SelectMenusByShopId(shopId);
        }
        public string GetMeunVersion(string menuid)
        {
            return menuShopManager.GetMeunVersion(menuid);
        }
        /*
        公司菜谱模块
        */
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(MenuConnCompany model)
        {
            return menuShopManager.Add(model);
        }
        /// <summary>
        /// 查询当前公司所有菜谱信息
        /// </summary>
        public DataTable QueryMenuConnCompany(int companyId)
        {
            return menuShopManager.SelectMenuConnCompany(companyId);
        }
        /// <summary>
        /// 查询当前公司菜谱信息
        /// </summary>
        public DataTable QueryMenuConnCompanyByMenuCompanyId(int menuCompanyId)
        {
            return menuShopManager.SelectMenuConnCompanyByMenuCompanyId(menuCompanyId);
        }
        public int QueryShopCurrectMenuCompanyId(int shopId)
        {
            return menuShopManager.SelectShopCurrectMenuCompanyId(shopId);
        }
    }
}
