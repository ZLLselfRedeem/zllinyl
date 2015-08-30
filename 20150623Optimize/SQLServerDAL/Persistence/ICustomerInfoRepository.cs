using PagedList;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL.Persistence.Infrastructure;

namespace VAGastronomistMobileApp.SQLServerDAL.Persistence
{
    public interface ICustomerInfoRepository
    {
        CustomerInfo GetById(long id);

        IPagedList<CustomerInfo> GetPage(Page page);

        void UpdateCustomerPicture(long id, string picture);

        CustomerInfo GetByCookie(string cookie);

        CustomerInfo GetByMobilePhoneNumber(string mobilePhone);
    }
}