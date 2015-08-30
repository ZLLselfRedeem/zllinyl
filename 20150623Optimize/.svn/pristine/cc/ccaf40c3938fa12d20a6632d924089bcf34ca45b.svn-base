using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.SQLServerDAL;
using System.Data;
using VAGastronomistMobileApp.Model;

namespace VAGastronomistMobileApp.WebPageDll
{
    public class WechatUxianCooperationOperate
    {
        private WechatUxianCooperationManager uxianCoopertionManger;
        public WechatUxianCooperationOperate()
        {
            if (uxianCoopertionManger == null)
                uxianCoopertionManger = new WechatUxianCooperationManager();
        }

        /// <summary>
        /// 获取发布的信息 
        /// </summary>
        /// <returns></returns>
        public DataTable GetCooperationInfo()
        {
            return uxianCoopertionManger.GetCooperationInfo();
        }
        //获取第一条msgcontent
        public string GetCooperation()
        {
            return uxianCoopertionManger.GetCooperation();
        }

        /// <summary>
        /// 新增发布信息 UxianCooperationInfo表
        /// </summary>
        /// <param name="uxianCooperationInfo"></param>
        /// <returns></returns>
        public int Insert(WechatUxianCooperationInfo uxianCooperationInfo)
        {
            return uxianCoopertionManger.Insert(uxianCooperationInfo);
        }

        /// <summary>
        /// 更新UxianCooperationInfo表
        /// </summary>
        /// <param name="uxianCooperationInfo"></param>
        /// <returns></returns>
        public int Update(WechatUxianCooperationInfo uxianCooperationInfo)
        {
            return uxianCoopertionManger.Update(uxianCooperationInfo);
        }

        /// <summary>
        /// 删除 UxianCooperationInfo表中一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(int id)
        {
            return uxianCoopertionManger.Delete(id);
        }


        /// <summary>
        /// 获取 商户发送的 合作信息
        /// </summary>
        /// <returns></returns>
        public DataTable GetMerchantSendInfo()
        {
            return uxianCoopertionManger.GetMerchantSendInfo();
        }

        /// <summary>
        /// 向商户发送信息 表中增加一条记录 (由微信公众平台接收)
        /// </summary>
        /// <param name="merchantSendInfo"></param>
        /// <returns></returns>
        public int InsertMerchantSendInfo(WechatMerchantSendInfo merchantSendInfo)
        {
            return uxianCoopertionManger.InsertMerchantSendInfo(merchantSendInfo);
        }

        /// <summary>
        /// 更新 WechatMerchantSendInfo 商户发送信息 表
        /// </summary>
        /// <param name="merchantSendInfo"></param>
        /// <returns></returns>
        public int UpdateMerchantSendInf(WechatMerchantSendInfo merchantSendInfo)
        {
            return uxianCoopertionManger.UpdateMerchantSendInf(merchantSendInfo);
        }

    }
}
