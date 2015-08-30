using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.SQLServerDAL;
using VAGastronomistMobileApp.Model;
using System.Data;
using System.Transactions;

namespace VAGastronomistMobileApp.WebPageDll
{
   public class NotificationRecordOperate
    {
        private readonly NotificationRecordManager dal = new NotificationRecordManager();
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public long AddNotificationRecord(NotificationRecord model)
        {
            return dal.InsertNotificationRecord(model);

        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool ModifyNotificationRecord(NotificationRecord model)
        {
            return dal.UpdateNotificationRecord(model);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool RemoveNotificationRecord(long id)
        {
            return dal.DeleteNotificationRecord(id);
        }

        public DataTable LockNotification()
        {
            DataTable result = new DataTable();
            DataTable dtNotificationRecord = dal.SelectNotificationRecord();
            if (dtNotificationRecord.Rows.Count == 1)
            {
                long id = Common.ToInt64(dtNotificationRecord.Rows[0]["id"]);
                using (TransactionScope scope = new TransactionScope())
                {
                    if (dal.UpdateNotificationRecordLock(id))
                    {
                        scope.Complete();
                        result = dtNotificationRecord;
                    }
                }
            }
            return result;
        }

       /// <summary>
       /// 根据添加时间，查询悠先点菜推送表数据
       /// </summary>
       /// <param name="strAddTime"></param>
       /// <param name="endAddTime"></param>
       /// <returns></returns>
       public DataTable QueryNotification(string strAddTime,string endAddTime)
       {
           return dal.SelectNotification(strAddTime,endAddTime);
       }

        #region 悠先服务

       /// <summary>
       /// 增加一条数据
       /// </summary>
       public long AddUxianServiceNotificationRecord(NotificationRecord model)
       {
           return dal.InsertUxianServiceNotificationRecord(model);

       }
       /// <summary>
       /// 更新一条数据
       /// </summary>
       public bool ModifyUxianServiceNotificationRecord(NotificationRecord model)
       {
           return dal.UpdateUxianServiceNotificationRecord(model);
       }
       /// <summary>
       /// 删除一条数据
       /// </summary>
       public bool RemoveUxianServiceNotificationRecord(long id)
       {
           return dal.DeleteUxianServiceNotificationRecord(id);
       }

       public DataTable LockUxianServiceNotification()
       {
           DataTable result = new DataTable();
           DataTable dtNotificationRecord = dal.SelectUxianServiceNotificationRecord();
           if (dtNotificationRecord.Rows.Count == 1)
           {
               long id = Common.ToInt64(dtNotificationRecord.Rows[0]["id"]);
               using (TransactionScope scope = new TransactionScope())
               {
                   if (dal.UpdateUxianServiceNotificationRecordLock(id))
                   {
                       scope.Complete();
                       result = dtNotificationRecord;
                   }
               }
           }
           return result;
       }

       /// <summary>
       /// 根据添加时间，查询悠先服务推送表数据
       /// </summary>
       /// <param name="strAddTime"></param>
       /// <param name="endAddTime"></param>
       /// <returns></returns>
       public DataTable QueryUxianServiceNotification(string strAddTime, string endAddTime)
       {
           return dal.SelectUxianServiceNotification(strAddTime, endAddTime);
       }

        #endregion
    }
}
