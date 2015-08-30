using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.Model;

namespace VAGastronomistMobileApp.SQLServerDAL.Persistence
{
    public interface IActivityShareInfoRepository
    {
        IEnumerable<ActivityShareInfo> GetManyByActivity(int activity);
    }
}
