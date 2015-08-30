using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL;
using VAGastronomistMobileApp.WebPageDll.Services.Infrastructure;
using VAGastronomistMobileApp.WebPageDll.Services;

namespace VAGastronomistMobileApp.WebPageDll
{
    /// <summary>
    /// 角色与权限关系操作类
    /// </summary>
    public class RoleAuthorityOperate
    {
        /// <summary>
        /// 新增角色与权限关系信息
        /// </summary>
        /// <param name="roleAuthority"></param>
        /// <returns></returns>
        public bool AddRoleAuthority(RoleAuthority roleAuthority)
        {
            AuthorityManager authorityMan = new AuthorityManager();
            DataTable dtRoleAuthority = authorityMan.SelectRoleAuthority();
            DataView dvRoleAuthority = dtRoleAuthority.DefaultView;
            dvRoleAuthority.RowFilter = "AuthorityID = '" + roleAuthority.AuthorityID
                + "' and RoleID = '" + roleAuthority.RoleID + "'";
            if (dvRoleAuthority.Count > 0)
            {//角色与权限关系表中已有相同信息（同一AuthorityID和同一RoleID），则直接返回false
                return false;
            }
            else
            {
                if (authorityMan.InsertRoleAuthority(roleAuthority) > 0)
                {//插入数据库表成功则返回ture
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        /// <summary>
        /// 根据角色编号删除角色与权限关系信息
        /// </summary>
        /// <param name="roleID"></param>
        /// <returns></returns>
        public bool RemoveRoleAuthority(int roleID)
        {
            AuthorityManager authorityMan = new AuthorityManager();
            DataTable dtRoleAuthority = authorityMan.SelectRoleAuthority();
            DataView dvRoleAuthority = dtRoleAuthority.DefaultView;
            dvRoleAuthority.RowFilter = "RoleID = '" + roleID + "'";
            if (dvRoleAuthority.Count > 1)
            {//判断此employeeRoleID是否存在，是则删除
                if (authorityMan.DeleteRoleAuthority(roleID))
                {//删除成功则返回true
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 修改角色与权限关系信息
        /// </summary>
        /// <param name="roleAuthority"></param>
        /// <returns></returns>
        public bool ModifyRoleAuthority(RoleAuthority roleAuthority)
        {
            AuthorityManager authorityMan = new AuthorityManager();
            DataTable dtRoleAuthority = authorityMan.SelectRoleAuthority();
            DataTable dtRoleAuthorityCopy = dtRoleAuthority.Copy();
            DataView dvRoleAuthorityCopy = dtRoleAuthorityCopy.DefaultView;
            DataView dvRoleAuthority = dtRoleAuthority.DefaultView;
            dvRoleAuthorityCopy.RowFilter = "RoleAuthorityID = '" + roleAuthority.RoleAuthorityID + "'";//判断此ID存在
            dvRoleAuthority.RowFilter = "RoleAuthorityID <> '" + roleAuthority.RoleAuthorityID
                + "' and AuthorityID = '" + roleAuthority.AuthorityID
                + "' and RoleID = '" + roleAuthority.RoleID + "'";//判断角色与权限关系表中是否已有相同信息（同一AuthorityID和同一RoleID）
            if (1 == dvRoleAuthorityCopy.Count && 0 == dvRoleAuthority.Count)
            {//判断此RoleID是否存在，是则修改
                if (authorityMan.UpdateRoleAuthority(roleAuthority))
                {//修改成功则返回true
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 查询所有角色与权限关系信息
        /// </summary>
        /// <returns></returns>
        //public DataTable QueryRoleAuthority()
        //{
        //    AuthorityManager authorityMan = new AuthorityManager();
        //    DataTable dtRoleAuthority = authorityMan.SelectRoleAuthority();
        //    DataView dvRoleAuthority = dtRoleAuthority.DefaultView;
        //    dvRoleAuthority.Sort = "RoleNameName ASC,AuthorityNameName ASC";
        //    return dvRoleAuthority.ToTable();
        //}
        /// <summary>
        /// 根据角色编号查询对应角色与权限关系信息
        /// </summary>
        /// <param name="roleID"></param>
        /// <returns></returns>
        public List<VARoleAuthority> QueryRoleAuthority(int roleID)
        {
            AuthorityManager authorityMan = new AuthorityManager();
            DataTable dtRoleAuthority = authorityMan.SelectRoleAuthority();
            DataView dvRoleAuthority = dtRoleAuthority.DefaultView;
            dvRoleAuthority.RowFilter = "RoleID = '" + roleID + "'";
            List<VARoleAuthority> authorityList = new List<VARoleAuthority>();
            if (dvRoleAuthority.Count > 0)
            {
                for (int i = 0; i < dvRoleAuthority.Count; i++)
                {
                    VARoleAuthority Authority = new VARoleAuthority();
                    Authority.roleAuthorityID = Common.ToInt32(dvRoleAuthority[i]["RoleAuthorityID"]);
                    Authority.authorityID = Common.ToInt32(dvRoleAuthority[i]["AuthorityID"]);
                    Authority.authorityName = Common.ToString(dvRoleAuthority[i]["AuthorityName"]);
                    authorityList.Add(Authority);
                }
            }
            else
            {
                authorityList = null;
            }
            return authorityList;
        }
        #region -------------------------------------------------------------
        public ShopAuthority GetShopAuthorityByCode(string code)
        {
            var shopAuthorityService = ServiceFactory.Resolve<IShopAuthorityService>();
            return shopAuthorityService.GetShopAuthorityByCode(code);
        }
        #endregion
    }
}
