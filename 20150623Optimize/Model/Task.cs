using System;

namespace VAGastronomistMobileApp.Model
{
    public abstract class Task
    {
        public long Id { set; get; }
        /// <summary>
        /// 任务状态
        /// </summary>
        public TaskStatus Status { set; get; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { set; get; }
        /// <summary>
        /// 任务开始时间
        /// </summary>
        public DateTime? BeginTime { set; get; }
        /// <summary>
        /// 任务结束时间
        /// </summary>
        public DateTime? EndTime { set; get; }
        /// <summary>
        /// 失败次数
        /// </summary>
        public int FailureCount { set; get; }
    }

    public enum TaskStatus : byte
    {
        未开始 = 0,
        正在处理中 = 1,
        处理失败 = 2,
        处理成功 = 3
    }
}
