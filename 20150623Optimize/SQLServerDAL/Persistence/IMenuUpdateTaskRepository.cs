using System.Collections.Generic;
using VAGastronomistMobileApp.Model;

namespace VAGastronomistMobileApp.SQLServerDAL.Persistence
{
    public interface IMenuUpdateTaskRepository
    {
        void Add(MenuUpdateTask entity);

        void Update(MenuUpdateTask entity);

        MenuUpdateTask GetById(long id);

        IEnumerable<MenuUpdateTask> GetAllFailureTask();
    }
}