using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VAGastronomistMobileApp.WebPageDll
{
    /// <summary>
    /// 收银宝消息返回类2
    /// </summary>
    public class SybMsg2
    {
        private SybSystemMsg model = null;
        public SybMsg2()
        {

        }

        /// <summary>
        /// 插入消息，返还给客户端消息
        /// </summary>
        /// <param name="error"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public void Insert(int error, object msg)
        {
            model = new SybSystemMsg() { error = error, msg = msg };
        }

        /// <summary>
        /// 返回消息json串
        /// </summary>
        public string Value
        {
            get { return Newtonsoft.Json.JsonConvert.SerializeObject(model); }
        }
    }
    public class SybSystemMsg
    {
        public int error { get; set; }
        public object msg { get; set; }
    }
}
