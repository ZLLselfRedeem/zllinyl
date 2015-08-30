using System.Collections.Generic;
using VAGastronomistMobileApp.Model;
using System.Data;

namespace VAGastronomistMobileApp.SQLServerDAL.Persistence
{
    public interface IShopAuthorityRepository
    {
        /// <summary>
        /// 获取员工在指定店的管理权限(悠先服务)
        /// </summary>
        /// <param name="shopId"></param>
        /// <param name="employeeId"></param>
        /// <param name="types"></param>
        /// <returns></returns>
        IEnumerable<ShopAuthority> GetShopAuthoritiesInViewAllocService(int shopId, int employeeId,
            params ShopAuthorityType[] types);

        /// <summary>
        /// 获取悠先员工的管理权限(悠先服务)
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="types"></param>
        /// <returns></returns>
        IEnumerable<ShopAuthority> GetShopAuthoritiesWithViewAllocWorkerInViewAllocService(int employeeId, params ShopAuthorityType[] types);

        /// <summary>
        /// 获取指定类型的悠先员工自动获取的权限(悠先服务)
        /// </summary>
        /// <param name="types"></param>
        /// <returns></returns>
        IEnumerable<ShopAuthority> GetShopAuthoritiesByTypeInViewAllocService(params ShopAuthorityType[] types);

        /// <summary>
        /// 获取显示在收银宝的门店权限
        /// </summary>
        /// <param name="isSybShow"></param>
        /// <param name="types"></param>
        /// <returns></returns>
        IEnumerable<ShopAuthority> GetShopAuthoritiesByType(bool? isSybShow = null, params ShopAuthorityType[] types);


        void Add(ShopAuthority shopAuthority);

        ShopAuthority GetShopAuthorityByCode(string code);

        DataTable QueryUxianServiceAuthorityOld();

        IEnumerable<ShopAuthority> GetAllShopAuthorities();

        void Delete(ShopAuthority shopAuthority);
    }
}
