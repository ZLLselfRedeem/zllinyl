using IDishMenuAsynUpdate;
using System;
using log4net;
using VA.AllNotifications;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL;
using VAGastronomistMobileApp.WebPageDll;
using VAGastronomistMobileApp.WebPageDll.Services.Infrastructure;

namespace DishMenuAsynUpdate.Core
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的类名“Service1”。
    public class MenuService : BaseService, IMenuService
    {
        private readonly ILog _log;
        public MenuService(IRepositoryContext repositoryContext, ILog log)
            : base(repositoryContext)
        {
            this._log = log;
        }


        public void Update(long taskId)
        {
            try
            {
                var menuUpdateTaskRepository = RepositoryContext.GetMenuUpdateTaskRepository();
                var task = menuUpdateTaskRepository.GetById(taskId);
                if (task != null && task.Status == TaskStatus.未开始)
                {
                    if (_log.IsInfoEnabled)
                    {
                        _log.InfoFormat("任务[{0}]开始工作", JsonHelper.Serialize(task));
                    }

                    task.Status = TaskStatus.正在处理中;
                    task.BeginTime = DateTime.Now;
                    menuUpdateTaskRepository.Update(task);

                    int v = new MenuOperate().UpdateMenu(task.MenuId, AppDomain.CurrentDomain.BaseDirectory);
                    if (_log.IsInfoEnabled)
                    {
                        _log.InfoFormat("菜谱{1}版本:{0}", v, task.MenuId);
                    }
                    if (v > 0)
                    {
                        task.Status = TaskStatus.处理成功;
                        task.EndTime = DateTime.Now;
                        menuUpdateTaskRepository.Update(task);
                        //task.EmployeeId;
                        var employeeInfo = RepositoryContext.GetEmployeeInfoRepository().GetById(task.EmployeeId);
                        if (employeeInfo != null && !string.IsNullOrEmpty(employeeInfo.pushToken) &&
                            employeeInfo.AppType.HasValue)
                        {

                            if (_log.IsInfoEnabled)
                            {
                                _log.InfoFormat("用户{0}开始推送{1}消息", employeeInfo.UserName, task.Id);
                            }
                            new NotificationRecordManager().InsertUxianServiceNotificationRecord(new NotificationRecord()
                            {
                                addTime = DateTime.Now,
                                appType = employeeInfo.AppType.Value,
                                customValue = task.Id.ToString(),
                                customType = (int)VANotificationsCustomType.NOTIFICATIONS_TASK,
                                isLocked = false,
                                isSent = false,
                                message = "菜谱更新成功啦!",
                                pushToken = employeeInfo.pushToken,
                                sendCount = 0
                            });
                        }
                        //NotificationRecordManager
                    }
                    else
                    {
                        task.Status = TaskStatus.处理失败;
                        task.EndTime = DateTime.Now;
                        task.FailureCount += 1;
                        menuUpdateTaskRepository.Update(task);
                    }
                }
            }
            catch (Exception exc)
            {
                if (_log.IsErrorEnabled)
                {
                    _log.Error("出错啦", exc);
                }
                //Console.WriteLine(exc.ToString());
                //throw;
            }
        }


    }
}
