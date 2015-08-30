using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    public class ViewAllocEmployeeAuthority
    {
        public int Id { set; get; }
        public int EmployeeId { set; get; }
        public int ShopAuthorityId { set; get; }
        public bool Status { set; get; }
    }
}
