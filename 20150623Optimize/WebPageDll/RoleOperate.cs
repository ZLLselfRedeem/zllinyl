using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL;

namespace VAGastronomistMobileApp.WebPageDll
{
    /// <summary>
    /// 角色信息操作类
    /// </summary>
    public class RoleOperate
    {
        private readonly AuthorityManager authorityMan = new AuthorityManager();
        /// <summary>
        /// 新增角色信息
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public bool AddRole(RoleInfo role)
        {
            DataTable dtRole = authorityMan.SelectRole();
            DataView dvRole = dtRole.DefaultView;
            dvRole.RowFilter = "RoleName = '" + role.RoleName + "'";
            if (dvRole.Count > 0 || role.RoleName == "" || role.RoleName == null)
            {//如果所加角色信息的名称为空，或者角色信息表中已有该名称的角色，则直接返回false
                return false;
            }
            else
            {
                if (authorityMan.InsertRole(role) > 0)
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
        /// 删除角色信息
        /// </summary>
        /// <param name="roleID"></param>
        /// <returns></returns>
        public bool RemoveRole(int roleID)
        {
            DataTable dtRole = authorityMan.SelectRole();
            DataView dvRole = dtRole.DefaultView;
            dvRole.RowFilter = "RoleID = '" + roleID + "'";
            if (1 == dvRole.Count)
            {//判断此employeeID是否存在，是则删除
                if (authorityMan.DeleteRole(roleID))
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
        /// 修改角色信息
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public bool ModifyRole(RoleInfo role)
        {
            DataTable dtRole = authorityMan.SelectRole();
            DataTable dtRoleCopy = dtRole.Copy();
            DataView dvRoleCopy = dtRoleCopy.DefaultView;
            DataView dvRole = dtRole.DefaultView;
            dvRoleCopy.RowFilter = "RoleID = '" + role.RoleID + "'";//判断此ID存在
            dvRole.RowFilter = "RoleID <> '" + role.RoleID
                + "' and RoleName = '" + role.RoleName + "'";//判断修改的名称不存在
            if (1 == dvRoleCopy.Count && 0 == dvRole.Count)
            {//判断此RoleID是否存在，是则修改
                if (authorityMan.UpdateRole(role))
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
        /// 查询角色信息
        /// </summary>
        /// <returns></returns>
        public DataTable QueryRole()
        {
            DataTable dtRole = authorityMan.SelectRole();
            DataView dvRole = dtRole.DefaultView;
            dvRole.Sort = "RoleName ASC";
            return dvRole.ToTable();
        }
        /// <summary>
        /// 查询特殊权限信息
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public DataTable QuerySpecialAuthorityInfo(int roleId)
        {
            DataTable dtSpecialAuthorityInfo = authorityMan.SelectSpecialAuthorityInfo(roleId);
            return dtSpecialAuthorityInfo;
        }
        /// <summary>
        /// 查询特殊权限信息
        /// </summary>
        /// <param name="specialAuthorityInfo"></param>
        /// <returns></returns>
        public int InsertSpecialAuthorityInfo(SpecialAuthorityInfo specialAuthorityInfo)
        {
            int specialAuthorityId = authorityMan.InsertSpecialAuthority(specialAuthorityInfo);
            return specialAuthorityId;
        }
        /// <summary>
        /// 修改特殊权限信息
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="specialAuthorityId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public int UpdateSpecialAuthority(int roleId, int specialAuthorityId, bool status)
        {
            int returnSpecialAuthorityId = authorityMan.UpdateSpecialAuthorityStatus(roleId, specialAuthorityId, status);
            return returnSpecialAuthorityId;
        }
        /// <summary>
        /// 删除该角色所有特殊权限
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public bool UpdateSpecialAuthority(int roleId)
        {
            int returnSpecialAuthorityId = authorityMan.UpdateSpecialAuthorityStatus(roleId);
            return returnSpecialAuthorityId >= 0;
        }
        /// <summary>
        /// 根据employId查询特殊权限信息
        /// </summary>
        /// <param name="employeeID"></param>
        /// <param name="specialAuthorityId"></param>
        /// <returns></returns>
        public DataTable QuerySpecialAuthorityInfoByEmployeeID(int employeeID, int specialAuthorityId)
        {
            DataTable returnSpecialAuthorityDt = authorityMan.SelectSpecialAuthorityInfoByEmployeeID(employeeID, specialAuthorityId);
            return returnSpecialAuthorityDt;
        }
        /// <summary>
        /// 插入特殊权限和城市官关联信息
        /// </summary>
        /// <param name="provinceId"></param>
        /// <param name="cityId"></param>
        /// <param name="connSpecialAuthorityId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public int InsertSpecialAuthorityConnCity(int provinceId, int cityId, int connSpecialAuthorityId, int status)
        {
            int result = 0;
            DataTable dt = authorityMan.SelectSpecialAuthorityConnCityInfo(provinceId, cityId, connSpecialAuthorityId, status);
            if (dt.Rows.Count == 0)
            {
                result = authorityMan.InsertSpecialAuthorityConnCityInfo(provinceId, cityId, connSpecialAuthorityId, status);
            }
            return result;
        }
        /// <summary>
        /// 删除特殊权限和城市的关联关系
        /// </summary>
        /// <param name="provinceId"></param>
        /// <param name="cityId"></param>
        /// <param name="connSpecialAuthorityId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public int DeleteSpecialAuthorityConnCity(int provinceId, int cityId, int connSpecialAuthorityId, int status)
        {
            AuthorityManager authorityMan = new AuthorityManager();
            return authorityMan.DeleteSpecialAuthorityConnCityInfo(provinceId, cityId, connSpecialAuthorityId, status);
        }
    }
}
