using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.SQLServerDAL;
using System.Data;
using VAGastronomistMobileApp.Model;

namespace VAGastronomistMobileApp.WebPageDll
{
    public class WechatUxianQandAOperate
    {
        private WechatUxianQandAManager uxianQandAManager;
        public WechatUxianQandAOperate()
        {
            if (uxianQandAManager == null)
                uxianQandAManager = new WechatUxianQandAManager();
        }

        /// <summary>
        /// 获取常见问答信息
        /// </summary>
        /// <returns></returns>
        public DataTable GetUxianQandAInfo()
        {
            return uxianQandAManager.GetUxianQandAInfo();
        }
        //获取问答信息 微信客户端显示用
        public DataTable GetUxianQandA()
        {
            return uxianQandAManager.GetUxianQandA();
        }

        /// <summary>
        /// 新增常见问答
        /// </summary>
        /// <param name="uxianQandAInfo"></param>
        /// <returns></returns>
        public int Insert(WechatUxianQandAInfo uxianQandAInfo)
        {
            return uxianQandAManager.Insert(uxianQandAInfo);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="uxianQandAInfo"></param>
        /// <returns></returns>
        public int Update(WechatUxianQandAInfo uxianQandAInfo)
        {
            return uxianQandAManager.Update(uxianQandAInfo);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(int id)
        {
            return uxianQandAManager.Delete(id);
        }
    }
}
