using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL.Persistence;
using VAGastronomistMobileApp.WebPageDll.Services.Infrastructure;
using System.Data;

namespace VAGastronomistMobileApp.WebPageDll.Services
{
    /// <summary>
    /// 
    /// </summary>
    public interface IShopAuthorityService
    {
        /// <summary>
        /// 获取门店权限列表
        /// </summary>
        /// <param name="shopId"></param>
        /// <param name="employeeId"></param>
        /// <param name="isViewAllocWorker"></param>
        /// <returns></returns>
        IList<ShopAuthority> GetShopAuthorities(int shopId, int employeeId, bool isViewAllocWorker);

        /// <summary>
        /// 员工在该门店是否有指定权限
        /// </summary>
        /// <param name="shopId"></param>
        /// <param name="employeeId"></param>
        /// <param name="isViewAllocWorker"></param>
        /// <param name="authorityCodes"></param>
        /// <returns></returns>
        bool GetEmployeeHasShopAuthority(int shopId, int employeeId, bool isViewAllocWorker, params string[] authorityCodes);

        IList<WaiterRoleInfo> GetWaiterRoleInfos(int shopId, int employeeId);

        void Add(ShopAuthority shopAuthority);

        ShopAuthority GetShopAuthorityByCode(string code);

        string[] QueryUxianServiceAuthorityOld();

        IList<ShopAuthority> GetAllShopAuthorities();

        void Delete(ShopAuthority shopAuthority);

        #region ---------------------------------------------------------------------
        /// <summary>
        /// 获取店铺中奖统计权限列表 
        /// </summary>
        /// <param name="shopId"></param>
        /// <param name="employeeId"></param>
        /// <param name="isViewAllocWorker"></param>
        /// <returns></returns>
        IList<ShopAuthority> GetShopAwardTotalAuthorities(int shopId, int employeeId, bool isViewAllocWorker);
        #endregion
    }

    /// <summary>
    /// 
    /// </summary>
    public class ShopAuthorityService : BaseService, IShopAuthorityService
    {
        public ShopAuthorityService(IRepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public IList<ShopAuthority> GetShopAuthorities(int shopId, int employeeId, bool isViewAllocWorker)
        {
            IList<ShopAuthority> shopAuthorities = null;
            IShopAuthorityRepository shopAuthorityRepository = RepositoryContext.GetShopAuthorityRepository();
            if (isViewAllocWorker)
            {
                var list1 = shopAuthorityRepository.GetShopAuthoritiesInViewAllocService(shopId, employeeId, ShopAuthorityType.悠先服务,
                     ShopAuthorityType.悠先服务内部);

                var list2 = shopAuthorityRepository.GetShopAuthoritiesWithViewAllocWorkerInViewAllocService(employeeId,
                    ShopAuthorityType.悠先服务, ShopAuthorityType.悠先服务内部);

                var list3 = shopAuthorityRepository.GetShopAuthoritiesByTypeInViewAllocService(ShopAuthorityType.悠先服务,
                    ShopAuthorityType.悠先服务内部);
                ShopAuthorityEqualityComparer comparer = new ShopAuthorityEqualityComparer();
                shopAuthorities = list1.Union(list2, comparer).Union(list3, comparer).ToList();
            }
            else
            {
                shopAuthorities = shopAuthorityRepository.GetShopAuthoritiesInViewAllocService(shopId, employeeId, ShopAuthorityType.悠先服务).ToList();
            }

            return shopAuthorities;
        }

        #region ---------------------------------------------------------
        public IList<ShopAuthority> GetShopAuthoritiesAll(int shopId, int employeeId, bool isViewAllocWorker)
        {
            IList<ShopAuthority> shopAuthorities = null;
            IShopAuthorityRepository shopAuthorityRepository = RepositoryContext.GetShopAuthorityRepository();
            if (isViewAllocWorker)
            {
                #region-----------------------------------------------
                var list1 = shopAuthorityRepository.GetShopAuthoritiesInViewAllocService(shopId, employeeId, ShopAuthorityType.悠先服务,
                     ShopAuthorityType.悠先服务内部,ShopAuthorityType.悠先服务统计);

                var list2 = shopAuthorityRepository.GetShopAuthoritiesWithViewAllocWorkerInViewAllocService(employeeId,
                    ShopAuthorityType.悠先服务, ShopAuthorityType.悠先服务内部, ShopAuthorityType.悠先服务统计);

                var list3 = shopAuthorityRepository.GetShopAuthoritiesByTypeInViewAllocService(ShopAuthorityType.悠先服务,
                    ShopAuthorityType.悠先服务内部, ShopAuthorityType.悠先服务统计);
                #endregion
                ShopAuthorityEqualityComparer comparer = new ShopAuthorityEqualityComparer();
                shopAuthorities = list1.Union(list2, comparer).Union(list3, comparer).ToList();
            }
            else
            {
                shopAuthorities = shopAuthorityRepository.GetShopAuthoritiesInViewAllocService(shopId, employeeId, ShopAuthorityType.悠先服务, ShopAuthorityType.悠先服务统计).ToList();
            }

            return shopAuthorities;
        }
        #endregion

        public bool GetEmployeeHasShopAuthority(int shopId, int employeeId, bool isViewAllocWorker, params string[] authorityCodes)
        {
            #region ----------------------------------------修改
            //return GetShopAuthorities(shopId, employeeId, isViewAllocWorker).Any(p => authorityCodes.Contains(p.AuthorityCode));
            return GetShopAuthoritiesAll(shopId, employeeId, isViewAllocWorker).Any(p => authorityCodes.Contains(p.AuthorityCode));
            #endregion
        }

        public IList<WaiterRoleInfo> GetWaiterRoleInfos(int shopId, int employeeId)
        {
            IList<ShopAuthority> shopAuthorities = null;
            IShopAuthorityRepository shopAuthorityRepository = RepositoryContext.GetShopAuthorityRepository();
            var employee = RepositoryContext.GetEmployeeInfoRepository().GetById(employeeId);
            if (employee == null)
            {
                throw new ArgumentNullException("employee为空");
            }
            if (employee.isViewAllocWorker.HasValue && employee.isViewAllocWorker.Value)
            {
                var list1 = shopAuthorityRepository.GetShopAuthoritiesInViewAllocService(shopId, employeeId, ShopAuthorityType.悠先服务内部);

                var list2 = shopAuthorityRepository.GetShopAuthoritiesWithViewAllocWorkerInViewAllocService(employeeId, ShopAuthorityType.悠先服务内部);

                var list3 = shopAuthorityRepository.GetShopAuthoritiesByTypeInViewAllocService(ShopAuthorityType.悠先服务内部);

                ShopAuthorityEqualityComparer comparer = new ShopAuthorityEqualityComparer();
                shopAuthorities = list1.Union(list2, comparer).Union(list3, comparer).ToList();
            }
            else
            {
                shopAuthorities = new List<ShopAuthority>();
                throw new ArgumentException("不是友络员工不能添加此权限");
            }

            var q = from a in shopAuthorityRepository.GetShopAuthoritiesByType(null, ShopAuthorityType.悠先服务内部)
                    join b in shopAuthorities on a.ShopAuthorityId equals b.ShopAuthorityId into g
                    from c in g.DefaultIfEmpty()
                    select new WaiterRoleInfo { roleId = a.ShopAuthorityId, roleName = a.ShopAuthorityName, isHave = c != null };

            return q.ToList();

            //return shopAuthorities;

        }

        public void Add(ShopAuthority shopAuthority)
        {
            IShopAuthorityRepository shopAuthorityRepository = RepositoryContext.GetShopAuthorityRepository();
            shopAuthorityRepository.Add(shopAuthority);
        }

        public ShopAuthority GetShopAuthorityByCode(string code)
        {
            IShopAuthorityRepository shopAuthorityRepository = RepositoryContext.GetShopAuthorityRepository();
            ShopAuthority shopAuthority = shopAuthorityRepository.GetShopAuthorityByCode(code);
            return shopAuthority;
        }

        public string[] QueryUxianServiceAuthorityOld()
        {
            string[] strAuthorityName = new string[] { "", "" };
            IShopAuthorityRepository shopAuthorityRepository = RepositoryContext.GetShopAuthorityRepository();
            DataTable dt = shopAuthorityRepository.QueryUxianServiceAuthorityOld();
            if (dt != null && dt.Rows.Count > 0)
            {
                strAuthorityName[0] = dt.Rows[0]["shopAuthorityName"].ToString();
                strAuthorityName[1] = dt.Rows[1]["shopAuthorityName"].ToString();
            }
            return strAuthorityName;
        }

        public IList<ShopAuthority> GetAllShopAuthorities()
        {
            IShopAuthorityRepository shopAuthorityRepository = RepositoryContext.GetShopAuthorityRepository();
            return shopAuthorityRepository.GetAllShopAuthorities().ToList();
        }

        public void Delete(ShopAuthority shopAuthority)
        {
            IShopAuthorityRepository shopAuthorityRepository = RepositoryContext.GetShopAuthorityRepository();
            shopAuthorityRepository.Delete(shopAuthority);
        }

        #region -------------------------------------------
        /// <summary>
        /// 获取店铺中奖统计权限列表 
        /// </summary>
        /// <param name="shopId"></param>
        /// <param name="employeeId"></param>
        /// <param name="isViewAllocWorker"></param>
        /// <returns></returns>
        public IList<ShopAuthority> GetShopAwardTotalAuthorities(int shopId, int employeeId, bool isViewAllocWorker)
        {
            IList<ShopAuthority> shopAuthorities = null;
            IShopAuthorityRepository shopAuthorityRepository = RepositoryContext.GetShopAuthorityRepository();
            if (isViewAllocWorker)
            {
                var list1 = shopAuthorityRepository.GetShopAuthoritiesInViewAllocService(shopId, employeeId, ShopAuthorityType.悠先服务统计);

                var list2 = shopAuthorityRepository.GetShopAuthoritiesWithViewAllocWorkerInViewAllocService(employeeId, ShopAuthorityType.悠先服务统计);

                var list3 = shopAuthorityRepository.GetShopAuthoritiesByTypeInViewAllocService(ShopAuthorityType.悠先服务统计);

                ShopAuthorityEqualityComparer comparer = new ShopAuthorityEqualityComparer();
                shopAuthorities = list1.Union(list2, comparer).Union(list3, comparer).ToList();
            }
            else
            {
                shopAuthorities = shopAuthorityRepository.GetShopAuthoritiesInViewAllocService(shopId, employeeId, ShopAuthorityType.悠先服务统计).ToList();
            }

            //shopAuthorities = shopAuthorityRepository.GetShopAuthoritiesInViewAllocService(shopId, employeeId, ShopAuthorityType.悠先服务统计).ToList();

            // 商家的抽奖功能是否打开
            bool isLottery = false;
            ShopAwardOperate operateShopAward = new ShopAwardOperate();
            var listShopAward=operateShopAward.SelectShopAwardList(shopId);
            if(listShopAward.Count>0)
            {
                isLottery = listShopAward.FirstOrDefault().Enable;
            }
            // 开奖功能未开通，不显示抽奖活动统计
            if(!isLottery)
            {
                ShopAuthority objShopAuthority = new ShopAuthority();
                objShopAuthority = shopAuthorities.ToList().Find(s => s.AuthorityCode == Common.ToInt32(ShopRole.抽奖活动统计).ToString());
                if (objShopAuthority !=null)
                {
                    shopAuthorities.Remove(objShopAuthority);
                }
            }

            return shopAuthorities;
        }
        #endregion
    }
}
