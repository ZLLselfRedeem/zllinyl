using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL;

namespace VAGastronomistMobileApp.WebPageDll
{
    public class CustomerIntegrationDetailOperate
    {
        private CustomerIntegrationDetailManager cidm = new CustomerIntegrationDetailManager();
        public int Insert(CustomerIntegrationDetail model, Guid CustomerIntegrationID)
        {
            return cidm.Insert(model, CustomerIntegrationID);
        }

        public int CountDetail(int CityID, DateTime BeginDate, DateTime EndDate, long CustomerID)
        {
            return cidm.CountDetail(CityID,BeginDate,EndDate,CustomerID);
        }

        public DataTable CustomerIntegrationDetails(int CityID, DateTime BeginDate, DateTime EndDate, long CustomerID, int str, int end)
        {
            return cidm.CustomerIntegrationDetails(CityID, BeginDate, EndDate, CustomerID, str, end);
        }

        public DataTable Rules()
        {
            return cidm.Rules();
        }

          /// <summary>
        /// 用户积分总表
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <returns></returns>
        public DataTable CustomerIntegration(long CustomerID)
        {
            return cidm.CustomerIntegration(CustomerID);
        }
    }
}
