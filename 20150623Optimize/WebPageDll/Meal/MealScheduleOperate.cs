using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL;

namespace VAGastronomistMobileApp.WebPageDll
{
    public class MealScheduleOperate
    {
        MealScheduleManager manager = new MealScheduleManager();
        public MealSchedule GetEntityByID(int mealScheduleID)
        {

            return manager.GetEntityByID(mealScheduleID);
        }

        public int GetCountByQuery(MealScheduleQueryObject queryObject)
        {
             return manager.GetCountByQuery(queryObject);
        }

        public List<MealSchedule> GetListByQuery(MealScheduleQueryObject queryObject, int pageIndex, int pageSize)
        {
            return manager.GetListByQuery(queryObject, pageIndex, pageSize);
        }

        public List<MealSchedule> GetListByQuery(MealScheduleQueryObject queryObject)
        {
            return manager.GetListByQuery(queryObject);
        }

        public bool AddEntity(MealSchedule entity)
        {
            return manager.AddEntity(entity);
        }

        public bool UpdateEntity(MealSchedule entity)
        {
            return manager.UpdateEntity(entity);
        }
    }
}
