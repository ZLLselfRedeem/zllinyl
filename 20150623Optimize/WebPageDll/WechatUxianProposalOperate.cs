using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.SQLServerDAL;
using System.Data;
using VAGastronomistMobileApp.Model;

namespace VAGastronomistMobileApp.WebPageDll
{
    public class WechatUxianProposalOperate
    {
        private WechatUxianProposalManager uxianProposalManager;
        public WechatUxianProposalOperate()
        {
            if (uxianProposalManager == null)
                uxianProposalManager = new WechatUxianProposalManager();
        }

        /// <summary>
        /// 获取WechatUxianProposalInfo
        /// </summary>
        /// <returns></returns>
        public DataTable GetUxianProposalInfo()
        {
            return uxianProposalManager.GetUxianProposalInfo();
        }
        // 获取意见建议 微信客户端显示用
        public string GetUxianProposal()
        {
            return uxianProposalManager.GetUxianProposal();
        }

        /// <summary>
        /// 新增WechatUxianProposalInfo
        /// </summary>
        /// <param name="uxianProposalInfo"></param>
        /// <returns></returns>
        public int Insert(WechatUxianProposalInfo uxianProposalInfo)
        {
            return uxianProposalManager.Insert(uxianProposalInfo);
        }

        /// <summary>
        /// 更新WechatUxianProposalInfo
        /// </summary>
        /// <param name="uxianProposalInfo"></param>
        /// <returns></returns>
        public int Update(WechatUxianProposalInfo uxianProposalInfo)
        {
            return uxianProposalManager.Update(uxianProposalInfo);
        }


        /// <summary>
        /// 删除 WechatUxianProposalInfo 记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(int id)
        {
            return uxianProposalManager.Delete(id);
        }


        /// <summary>
        /// 获取 微信客户端发送的 意见建议信息
        /// </summary>
        /// <returns></returns>
        public DataTable GetProposalReceiveInfo()
        {
            return uxianProposalManager.GetProposalReceiveInfo();
        }

        /// <summary>
        /// 新增 微信客户端发送的意见建议记录
        /// </summary>
        /// <param name="proposalReceiveInfo"></param>
        /// <returns></returns>
        public int InsertProposalReceiveInfo(WechatProposalReceiveInfo proposalReceiveInfo)
        {
            return uxianProposalManager.InsertProposalReceiveInfo(proposalReceiveInfo);
        }

        /// <summary>
        /// 更新WechatProposalReceiveInfo 处理人,处理时间
        /// </summary>
        /// <param name="proposalReceiveInfo"></param>
        /// <returns></returns>
        public int UpdateProposalReceiveInfo(WechatProposalReceiveInfo proposalReceiveInfo)
        {
            return uxianProposalManager.UpdateProposalReceiveInfo(proposalReceiveInfo);
        }
    }
}
