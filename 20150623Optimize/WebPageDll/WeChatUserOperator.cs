using System;
using System.Collections.Generic;
using System.Linq;

using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL;

namespace VAGastronomistMobileApp.WebPageDll
{
    public class WeChatUserOperator
    {

        /// <summary>
        /// 插入微信授权
        /// </summary>
        /// <param name="model">微信用户模型</param>
        /// <param name="privilegeList">微信用户特权列表</param>
        /// <returns></returns>
        public int Insert(WeChatUser model, IList<WeChatUserPrivilege> privilegeList)
        {
            return new WeChatUserManager().Insert(model, privilegeList);
        }

        /// <summary>
        /// 更新微信用户
        /// </summary>
        /// <param name="model">微信用户模型</param>
        /// <param name="privilegeList">微信用户特权列表</param>
        /// <returns></returns>
        public int Update(WeChatUser model, IList<WeChatUserPrivilege> privilegeList)
        {
            return new WeChatUserManager().Update(model, privilegeList);
        }

        /// <summary>
        /// 更新用户id
        /// </summary>
        /// <param name="unionId"></param>
        /// <param name="customerInfoCustomerID"></param>
        /// <param name="modifyUser"></param>
        /// <param name="modifyIP"></param>
        /// <returns></returns>
        public int UpdateCustomerInfoCustomerID(string unionId, long customerInfoCustomerID, string modifyUser, string modifyIP)
        {
            return new WeChatUserManager().UpdateCustomerInfoCustomerID(unionId, customerInfoCustomerID, modifyUser, modifyIP);
        }

        /// <summary>
        /// 更新手机号
        /// </summary>
        /// <param name="id"></param>
        /// <param name="mobile"></param>
        /// <param name="modifyUser"></param>
        /// <param name="modifyIP"></param>
        /// <returns></returns>
        public int UpdateMobile(string id, string mobile, string modifyUser, string modifyIP)
        {
            return new WeChatUserManager().UpdateMobile(id, mobile, modifyUser, modifyIP);
        }

        /// <summary>
        /// 更新手机号
        /// </summary>
        /// <param name="mobile">旧手机号</param>
        /// <param name="newMobile">新手机号</param>
        /// <param name="modifyUser">修改人</param>
        /// <param name="modifyIP">修改IP</param>
        /// <returns></returns>
        public int UpdateNewMobile(string mobile, string newMobile, string modifyUser, string modifyIP)
        {
            return new WeChatUserManager().UpdateNewMobile(mobile, newMobile, modifyUser, modifyIP);
        }

        /// <summary>
        /// 判断用户是否存在
        /// </summary>
        /// <param name="customerInfoCustomerID"></param>
        /// <returns></returns>
        public bool IsExistUser(long customerInfoCustomerID)
        {
            return new WeChatUserManager().IsExistUser(customerInfoCustomerID);
        }

        /// <summary>
        /// 判断手机号是否存在
        /// </summary>
        /// <param name="mobile"></param>
        /// <returns></returns>
        public bool IsExistMombile(string mobile)
        {
            return new WeChatUserManager().IsExistMombile(mobile);
        }

        /// <summary>
        /// 返回微信用户model
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public WeChatUser GetModel(Guid id)
        {
            return new WeChatUserManager().GetModel(id);
        }

        /// <summary>
        /// 按openId返回微信用户model
        /// </summary>
        /// <param name="openId">openid</param>
        /// <returns></returns>
        public WeChatUser GetOpenIdOfModel(string openId)
        {
            return new WeChatUserManager().GetOpenIdOfModel(openId);
        }

        /// <summary>
        /// 按openId返回微信用户model
        /// </summary>
        /// <param name="unionId">unionId</param>
        /// <returns></returns>
        public WeChatUser GetUnionIdOfModel(string unionId)
        {
            return new WeChatUserManager().GetUnionIdOfModel(unionId);
        }

        /// <summary>
        /// 按mobile返回微信用户model
        /// </summary>
        /// <param name="mobile">手机号</param>
        /// <returns></returns>
        public WeChatUser GetMobileOfModel(string mobile)
        {
            return new WeChatUserManager().GetMobileOfModel(mobile);
        }

        /// <summary>
        /// 按微信用户id返回特权列表
        /// </summary>
        /// <param name="weChatUserId"></param>
        /// <returns></returns>
        public IList<WeChatUserPrivilege> GetPrivilegeList(Guid weChatUserId)
        {
            return new WeChatUserManager().GetPrivilegeList(weChatUserId).ToList();
        }

        /// <summary>
        /// 按用户id返回entity
        /// </summary>
        /// <param name="customerId">用户id</param>
        /// <returns></returns>
        public WeChatUser GetCustomerIDToEntity(long customerId)
        {
            return new WeChatUserManager().GetCustomerIDToEntity(customerId);
        }
    }
}
