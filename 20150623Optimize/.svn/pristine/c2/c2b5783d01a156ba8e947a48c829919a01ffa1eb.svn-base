using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.SQLServerDAL;
using System.Data;
using VAGastronomistMobileApp.Model;

namespace VAGastronomistMobileApp.WebPageDll
{
    public class WechatLandladyVoiceOperate
    {
        private WechatLandladyVoiceManager landladyVoiceManager;
        public WechatLandladyVoiceOperate()
        {
            if (landladyVoiceManager == null)
                landladyVoiceManager = new WechatLandladyVoiceManager();
        }

        /// <summary>
        /// 获取 聆听老板娘info
        /// </summary>
        /// <returns></returns>
        public DataTable GetLandladysVoiceInfo()
        {
            return landladyVoiceManager.GetLandladysVoiceInfo();
        }

        /// <summary>
        /// 先删 后增
        /// </summary>
        /// <param name="landyVoiceInfo"></param>
        /// <returns></returns>
        public int Insert(WechatLandladyVoiceInfo landyVoiceInfo)
        {
            return landladyVoiceManager.Insert(landyVoiceInfo);
        }

        //更新media_id,status
        public int UpdateMediaIDAndStatus(string media_id, int rowId)
        {
            return landladyVoiceManager.UpdateMediaIDAndStatus(media_id, rowId);
        }

        //取出media_id
        public string GetMediaId()
        {
            return landladyVoiceManager.GetMediaId();
        }

        public bool Delete()
        {
            return landladyVoiceManager.Delete();
        }
    }
}
