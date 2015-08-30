using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using VAGastronomistMobileApp.SQLServerDAL;
using VAGastronomistMobileApp.Model;
namespace VAGastronomistMobileApp.WebPageDll
{
    /// <summary>
    /// 收银宝消息返回类
    /// </summary>
    public class SybMsg
    {
        private SybWebMsgList list = null;
        public SybMsg() {
            list = new SybWebMsgList();
        }

        /// <summary>
        /// 插入消息，返还给客户端消息 格式如 {status:1,info="保存菜品信息成功"} 说明：1 成功 -1失败 （负值全是失败）
        /// </summary>
        /// <param name="status"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        public void Insert(int status, string info)
        {
            if (list.list == null)
            {
                list.list = new List<SybWebMsg>();
            }
            SybWebMsg msg = new SybWebMsg() { status = status, info = info };
            list.list.Add(msg);
        }

        /// <summary>
        /// 返回消息json串
        /// </summary>
        public string Value
        {
            get { return SysJson.JsonSerializer(list); }
        }
        /// <summary>
        /// 传递json到前端空字符串转义
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns></returns>
        public static string SetStr(string str)
        {
            if (String.IsNullOrEmpty(str))
            {
                return "\"\"";
            }
            else
            {
                return str;
            }
        }
    }
}
