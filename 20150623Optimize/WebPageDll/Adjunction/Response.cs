using System;

namespace VAGastronomistMobileApp.WebPageDll.Adjunction
{
    /// <summary>
    /// 响应请求结果
    /// </summary>
    /// <typeparam name="T">返回的结果对象</typeparam>
    public class Response<T>
    {
        /// <summary>
        /// 服务器响应结果
        /// </summary>
        public Results Result { get; set; }

        /// <summary>
        /// 返回给客户端消息
        /// </summary>
        public T Content { get; set; }
    }

    public enum Results
    {
        Success,
        Failed,
        Exception
    }
}
