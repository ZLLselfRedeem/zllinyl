using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VA.Cache.HttpRuntime
{
    /// <summary>
    /// 缓存移除原因
    /// </summary>
    public enum CacheItemRemovedReasonExt
    {
        /// <summary>
        /// 手动调用移除缓存
        /// </summary>
        手动移除 = 1,

        /// <summary>
        /// 过期移除缓存
        /// </summary>
        过期移除 = 2,

        /// <summary>
        /// 系统释放内存移除缓存
        /// </summary>
        系统移除 = 3,

        /// <summary>
        /// 依赖项发生变化移除缓存
        /// </summary>
        依赖项修改移除 = 4,
    }
}
