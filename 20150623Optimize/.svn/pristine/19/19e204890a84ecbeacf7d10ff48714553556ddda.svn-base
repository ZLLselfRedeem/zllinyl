using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.WebPageDll.Services.Infrastructure;

namespace VAGastronomistMobileApp.WebPageDll.Services
{
    /// <summary>
    /// 任务逻辑接口
    /// </summary>
    public interface ITaskService
    {
        /// <summary>
        /// 根据Id获取任务
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task GetTaskById(long id);
    }

    /// <summary>
    /// 任务逻辑实现
    /// </summary>
    public class TaskService : BaseService, ITaskService
    {
        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="repositoryContext"></param>
        public TaskService(IRepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public Task GetTaskById(long id)
        {
            Task task = RepositoryContext.GetMenuUpdateTaskRepository().GetById(id);
            return task;
        }
    }
}
