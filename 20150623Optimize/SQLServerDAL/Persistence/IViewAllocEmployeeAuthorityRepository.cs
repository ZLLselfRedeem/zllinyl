using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.Model;

namespace VAGastronomistMobileApp.SQLServerDAL.Persistence
{
    public interface IViewAllocEmployeeAuthorityRepository
    {
        void Add(ViewAllocEmployeeAuthority viewAllocEmployeeAuthority);

        IEnumerable<ViewAllocEmployeeAuthority> GetViewAllocEmployeeAuthorityByEmployeeAndShopAuthority(int employeeId,
            int shopAuthorityId);

        void Update(ViewAllocEmployeeAuthority viewAllocEmployeeAuthority);

        void Delete(ViewAllocEmployeeAuthority viewAllocEmployeeAuthority);
    }
}
