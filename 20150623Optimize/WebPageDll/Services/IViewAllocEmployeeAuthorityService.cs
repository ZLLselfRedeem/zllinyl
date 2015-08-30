using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.WebPageDll.Services.Infrastructure;

namespace VAGastronomistMobileApp.WebPageDll.Services
{
    public interface IViewAllocEmployeeAuthorityService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewAllocEmployeeAuthority"></param>
        void Add(ViewAllocEmployeeAuthority viewAllocEmployeeAuthority);

        IList<ViewAllocEmployeeAuthority> GetViewAllocEmployeeAuthorityByEmployeeAndShopAuthority(int employeeId,
            int shopAuthorityId);

        void Delete(ViewAllocEmployeeAuthority viewAllocEmployeeAuthority);
    }

    public class ViewAllocEmployeeAuthorityService : BaseService, IViewAllocEmployeeAuthorityService
    {
        public ViewAllocEmployeeAuthorityService(IRepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public void Add(ViewAllocEmployeeAuthority viewAllocEmployeeAuthority)
        {
            RepositoryContext.GetViewAllocEmployeeAuthorityRepository().Add(viewAllocEmployeeAuthority);
        }

        public IList<ViewAllocEmployeeAuthority> GetViewAllocEmployeeAuthorityByEmployeeAndShopAuthority(int employeeId, int shopAuthorityId)
        {
            return
                RepositoryContext.GetViewAllocEmployeeAuthorityRepository()
                    .GetViewAllocEmployeeAuthorityByEmployeeAndShopAuthority(employeeId, shopAuthorityId).ToList();
        }

        public void Delete(ViewAllocEmployeeAuthority viewAllocEmployeeAuthority)
        {
            RepositoryContext.GetViewAllocEmployeeAuthorityRepository().Delete(viewAllocEmployeeAuthority);
        }
    }
}
