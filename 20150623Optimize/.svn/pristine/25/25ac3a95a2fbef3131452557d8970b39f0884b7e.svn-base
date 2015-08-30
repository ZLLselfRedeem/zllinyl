using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.SQLServerDAL;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL.Persistence.Infrastructure;

namespace VAGastronomistMobileApp.WebPageDll
{
    /// <summary>
    /// 红包活动
    /// 2014-7-29
    /// </summary>
    public class ActivityOperate
    {
        ActivityManager activityManager = new ActivityManager();

        /// <summary>
        /// 新增活动
        /// </summary>
        /// <param name="activity"></param>
        /// <returns></returns>
        public int InsertActivity(Activity activity)
        {
            return activityManager.InsertActivity(activity);
        }
        public int InsertActivityExt(Activity activity)
        {
            return activityManager.InsertActivityExt(activity);
        }
        /// <summary>
        /// 修改活动
        /// </summary>
        /// <param name="activity"></param>
        /// <returns></returns>
        public bool UpdateActivity(Activity activity)
        {
            return activityManager.UpdateActivity(activity);
        }
        public bool UpdateActivityExt(Activity activity)
        {
            return activityManager.UpdateActivityExt(activity);
        }

        /// <summary>
        /// 更改活动的启停状态
        /// </summary>
        /// <param name="activityId"></param>
        /// <returns></returns>
        public bool EnableActivity(int activityId)
        {
            Activity activity = new Activity();
            activity = activityManager.QueryActivity(activityId);
            if (activity.enabled == true)
            {
                return activityManager.EnableActivity(activityId, 0);
            }
            else
            {
                return activityManager.EnableActivity(activityId, 1);
            }
        }

        /// <summary>
        /// 删除指定活动
        /// </summary>
        /// <param name="activityId"></param>
        /// <returns></returns>
        public bool DeleteActivity(int activityId)
        {
            return activityManager.DeleteActivity(activityId);
        }

        /// <summary>
        /// 根据活动Id查询相应信息
        /// </summary>
        /// <param name="activityId"></param>
        /// <returns></returns>
        public Activity QueryActivity(int activityId)
        {
            return activityManager.QueryActivity(activityId);
        }

        /// <summary>
        /// 查询所有活动，分页
        /// </summary>
        /// <returns></returns>
        public List<Activity> QueryActivity(Page page, string name, out int cnt)
        {
            return activityManager.QueryActivity(page, name, out cnt);
        }

        /// <summary>
        /// 查询所有活动
        /// </summary>
        /// <returns></returns>
        public List<Activity> QueryActivity(bool present = false)
        {
            return activityManager.QueryActivity(present);
        }

        public List<Activity> QueryAllActivity()
        {
            return activityManager.QueryAllActivity();
        }

        public List<Activity> QueryAllActivityNew()
        {
            return activityManager.QueryAllActivityNew();
        }
    }
}
