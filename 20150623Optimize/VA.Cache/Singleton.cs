using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VA.Cache
{
    /// <summary>
    /// 获取单例对象
    /// </summary>
    public sealed class Singleton<T> where T : new()
    {
        private Singleton()
        {
        }
        public static T Instance
        {
            get { return SingletonCreator.instance; }
        }
        internal class SingletonCreator
        {
            static SingletonCreator() { }
            internal static readonly T instance = new T();
        }
    }
}
