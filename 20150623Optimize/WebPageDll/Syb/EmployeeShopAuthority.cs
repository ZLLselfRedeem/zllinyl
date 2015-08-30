using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.SQLServerDAL;

namespace VAGastronomistMobileApp.WebPageDll
{
    public class SybEmployeeShopAuthority
    {
        readonly ShopAuthorityManager manager = new ShopAuthorityManager();
        /// <summary>
        /// 收银宝查询用户在某家门店是否有某个权限
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="shopId"></param>
        /// <param name="shopAuthorityName"></param>
        /// <returns></returns>
        public bool QuerySybAuthority(int employeeId, int shopId, string shopAuthorityName)
        {
            return manager.SelectSybAuthority(employeeId, shopId, shopAuthorityName);
        }

        /// <summary>
        /// <para>获取员工在该店的模块权限</para>
        /// <para>bruke</para>
        /// </summary>
        /// <param name="shopId"></param>
        /// <param name="employeeId"></param>
        /// <param name="shopAuthorityType">模块类型(1-收银宝,2-悠先服务)</param>
        /// <returns></returns>
        public string[] GetEmployeeInShopAuthorityNames(int shopId, int employeeId, short shopAuthorityType)
        {
            AuthorityManager aManager = new AuthorityManager();
            return aManager.GetEmployeeInShopAuthorityNames(shopId, employeeId, shopAuthorityType);
        }
    }
}
