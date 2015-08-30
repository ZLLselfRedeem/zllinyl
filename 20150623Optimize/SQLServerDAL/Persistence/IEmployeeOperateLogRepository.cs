using VAGastronomistMobileApp.Model;

namespace VAGastronomistMobileApp.SQLServerDAL.Persistence
{
    public interface IEmployeeOperateLogRepository
    {
        void Add(EmployeeOperateLogInfo employeeOperateLogInfo);
    }
}
