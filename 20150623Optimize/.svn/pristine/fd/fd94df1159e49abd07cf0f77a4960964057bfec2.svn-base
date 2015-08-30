using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.SQLServerDAL;
using System.Data;

namespace VAGastronomistMobileApp.WebPageDll
{
    /// <summary>
    /// created by wangc 
    /// 20140512
    /// 客户端发送到服务器错误日志 数据访问层
    /// </summary>
    public class ClientErrorInfoOperate
    {
        readonly ClientErrorInfoManager man = new ClientErrorInfoManager();
        /// <summary>
        /// 查询客户端错误日志
        /// </summary>
        /// <returns></returns>
        public DataTable QueryClientErrorInfo(string strTime, string endTime)
        {
            return man.SelectClientErrorInfo(strTime, endTime);
        }
        /// <summary>
        /// 查询错误消息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string QueryErrorMessage(long id)
        {
            return man.SelectErrorMessage(id);
        }
    }
}
