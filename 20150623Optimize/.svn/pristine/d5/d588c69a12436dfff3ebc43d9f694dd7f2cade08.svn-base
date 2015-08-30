using System;
using System.Collections.Generic;

using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL;

namespace VAGastronomistMobileApp.WebPageDll
{
    public class WeChatAuthorizationOperate
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Insert(WeChatAuthorization model)
        {
            return new WeChatAuthorizationManager().Insert(model);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Update(WeChatAuthorization model)
        {
            return new WeChatAuthorizationManager().Update(model);
        }

        /// <summary>
        /// 按OpenId更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateOfOpenId(WeChatAuthorization model)
        {
            return new WeChatAuthorizationManager().Update(model);
        }

        /// <summary>
        /// 返回模型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public WeChatAuthorization GetModel(Guid id)
        {
            return new WeChatAuthorizationManager().GetModel(id);
        }

        /// <summary>
        /// 返回model
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public WeChatAuthorization GetOpenIdOfModel(string openId)
        {
            return new WeChatAuthorizationManager().GetOpenIdOfModel(openId);
        }
    }
}
