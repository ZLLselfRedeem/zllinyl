using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    public class MenuUpdateTask : Task
    {
        public int MenuId { set; get; }
        public int EmployeeId { set; get; }
    }
}
