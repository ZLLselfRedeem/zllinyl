using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.SQLServerDAL;
using System.Data;
using VAGastronomistMobileApp.Model;

namespace VAGastronomistMobileApp.WebPageDll
{
    public class WechatUxianComplaintOperate
    {
        private WechatUxianComplaintManager uxianComplaintManager;
        public WechatUxianComplaintOperate()
        {
            if (uxianComplaintManager == null)
                uxianComplaintManager = new WechatUxianComplaintManager();
        }

        /// <summary>
        /// 获取WechatUxianComplaintInfo
        /// </summary>
        /// <returns></returns>
        public DataTable GetUxianComplaintInfo()
        {
            return uxianComplaintManager.GetUxianComplaintInfo();
        }

        //获取投诉处理 微信客户端显示
        public string GetUxianComplaint()
        {
            return uxianComplaintManager.GetUxianComplaint();
        }

        /// <summary>
        /// 新增WechatUxianComplaintInfo
        /// </summary>
        /// <param name="uxianCompaintInfo"></param>
        /// <returns></returns>
        public int Insert(WechatUxianComplaintInfo uxianCompaintInfo)
        {
            return uxianComplaintManager.Insert(uxianCompaintInfo);
        }

        /// <summary>
        /// 更新WechatUxianComplaintInfo
        /// </summary>
        /// <param name="uxianCompaintInfo"></param>
        /// <returns></returns>
        public int Update(WechatUxianComplaintInfo uxianCompaintInfo)
        {
            return uxianComplaintManager.Update(uxianCompaintInfo);
        }

        /// <summary>
        /// 删除WechatUxianComplaintInfo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(int id)
        {
            return uxianComplaintManager.Delete(id);
        }


        /// <summary>
        /// 获取微信平台 客户端发送的投诉信息
        /// </summary>
        /// <returns></returns>
        public DataTable GetComplaintReceiveInfo()
        {
            return uxianComplaintManager.GetComplaintReceiveInfo();
        }

        /// <summary>
        /// 新增 微信平台 客户端发送的投诉信息
        /// </summary>
        /// <param name="complaintReceiverInfo"></param>
        /// <returns></returns>
        public int InsertComplaintReceiveInfo(WechatComplaintReceiveInfo complaintReceiverInfo)
        {
            return uxianComplaintManager.InsertComplaintReceiveInfo(complaintReceiverInfo);
        }

        /// <summary>
        /// 更新 微信平台 客户端发送的投诉信息 (处理人,处理时间)
        /// </summary>
        /// <param name="complaintReceiveInfo"></param>
        /// <returns></returns>
        public int UpdateComplaintReceiveInfo(WechatComplaintReceiveInfo complaintReceiveInfo)
        {
            return uxianComplaintManager.UpdateComplaintReceiveInfo(complaintReceiveInfo);
        }
    }
}
