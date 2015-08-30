using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL;

namespace VAGastronomistMobileApp.WebPageDll
{
    public class ActivityMessageOperate
    {
         /// <summary>
        /// 获取活动消息明细
       /// </summary>
       /// <param name="ID"></param>
       /// <returns></returns>
        public DataTable ActivityMessageDetail(int ID)
        {
            ActivityMessageManager amm = new ActivityMessageManager();
            return amm.ActivityMessageDetail(ID);
        }

         /// <summary>
        /// 获取活动消息列表
       /// </summary>
       /// <param name="model">活动消息实体</param>
       /// <param name="BeginDate">开始时间</param>
       /// <param name="EndDate">结束时间</param>
       /// <returns></returns>
        public DataTable ActivityMessages(ActivityMessage model, DateTime BeginDate, DateTime EndDate)
        {
            ActivityMessageManager amm = new ActivityMessageManager();
            return amm.ActivityMessages(model, BeginDate, EndDate);
        }

          /// <summary>
        /// 插入一条活动消息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Insert(ActivityMessage model)
        {
            ActivityMessageManager amm = new ActivityMessageManager();
            return amm.Insert(model);
        }

         /// <summary>
        /// 修改一条活动消息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Update(ActivityMessage model)
        {
            ActivityMessageManager amm = new ActivityMessageManager();
            return amm.Update(model);
        }
    }
}
