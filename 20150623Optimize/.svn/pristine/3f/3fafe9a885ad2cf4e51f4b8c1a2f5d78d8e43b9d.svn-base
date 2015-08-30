using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL;

namespace VAGastronomistMobileApp.WebPageDll
{
    /// <summary>
    /// 活动分享配置
    /// 2014-10-15
    /// </summary>
    public class ActivityShareOperate
    {
        ActivityShareManager activityShareManager = new ActivityShareManager();

        /// <summary>
        /// 新增分享信息
        /// </summary>
        /// <param name="activityShareInfo"></param>
        /// <returns></returns>
        public int InsertActivityShareInfo(ActivityShareInfo activityShareInfo)
        {
            return activityShareManager.InsertActivityShareInfo(activityShareInfo);
        }

        /// <summary>
        /// 删除分享信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteActivityShareInfo(int id)
        {
            return activityShareManager.DeleteActivityShareInfo(id);
        }

        /// <summary>
        /// 根据类别查询分享信息
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public List<ActivityShareInfo> QueryActivityShareInfo(ActivityShareInfoType type)
        {
            return activityShareManager.SelectActivityShareInfo(type);
        }

        /// <summary>
        /// 根据活动Id查询分享信息
        /// </summary>
        /// <param name="activityId"></param>
        /// <returns></returns>
        public List<ActivityShareInfo> QueryActivityShareInfo(ActivityShareInfoType type, int activityId)
        {
            return activityShareManager.SelectActivityShareInfo(type, activityId);
        }

        /// <summary>
        /// 根据Id查询其详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActivityShareInfo QueryActivityShareInfoById(int id)
        {
            return activityShareManager.SelectActivityShareInfoById(id);
        }

        public List<ActivityShareInfo> GetManyByActivity(int activityId)
        {
            return activityShareManager.GetManyByActivity(activityId);
        }
    }
}
