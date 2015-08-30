using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Transactions;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL;

namespace VAGastronomistMobileApp.WebPageDll
{
    /// <summary>
    /// 员工与角色关系操作类
    /// </summary>
    public class EmployeeRoleOperate
    {
        /// <summary>
        /// 新增员工与角色关系信息
        /// </summary>
        /// <param name="employeeRole"></param>
        /// <returns></returns>
        public bool AddEmployeeRole(EmployeeRole employeeRole)
        {
            AuthorityManager authorityMan = new AuthorityManager();
            DataTable dtEmployeeRole = authorityMan.SelectEmployeeRole();
            DataView dvEmployeeRole = dtEmployeeRole.DefaultView;
            dvEmployeeRole.RowFilter = "EmployeeID = '" + employeeRole.EmployeeID
                + "' and RoleID = '" + employeeRole.RoleID + "'";
            if (dvEmployeeRole.Count > 0)
            {//员工与角色关系表中已有相同信息（同一EmployeeID和同一RoleID），则直接返回false
                return false;
            }
            else
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    if (authorityMan.InsertEmployeeRole(employeeRole) > 0)
                    {//插入数据库表成功则返回ture
                        scope.Complete();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }
        /// <summary>
        /// 删除员工与角色关系信息
        /// </summary>
        /// <param name="employeeRoleID"></param>
        /// <returns></returns>
        public bool RemoveEmployeeRole(int employeeRoleID)
        {
            AuthorityManager authorityMan = new AuthorityManager();
            DataTable dtEmployeeRole = authorityMan.SelectEmployeeRole();
            DataView dvEmployeeRole = dtEmployeeRole.DefaultView;
            dvEmployeeRole.RowFilter = "EmployeeRoleID = '" + employeeRoleID + "'";
            if (1 == dvEmployeeRole.Count)
            {//判断此employeeRoleID是否存在，是则删除
                if (authorityMan.DeleteEmployeeRole(employeeRoleID))
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
        /// 当角色删除时，删除所有与该角色有关的信息 add by wangc 20140414
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public bool RemoveEmployeeRoleByRoleID(int roleId)
        {
            AuthorityManager authorityMan = new AuthorityManager();
            return authorityMan.DeleteEmployeeRoleByRoleID(roleId);
        }
        /// <summary>
        /// 修改员工与角色关系信息
        /// </summary>
        /// <param name="employeeRole"></param>
        /// <returns></returns>
        public bool ModifyEmployeeRole(EmployeeRole employeeRole)
        {
            AuthorityManager authorityMan = new AuthorityManager();
            DataTable dtEmployeeRole = authorityMan.SelectEmployeeRole();
            DataTable dtEmployeeRoleCopy = dtEmployeeRole.Copy();
            DataView dvEmployeeRoleCopy = dtEmployeeRoleCopy.DefaultView;
            DataView dvEmployeeRole = dtEmployeeRole.DefaultView;
            dvEmployeeRoleCopy.RowFilter = "EmployeeRoleID = '" + employeeRole.EmployeeRoleID + "'";//判断此ID存在
            dvEmployeeRole.RowFilter = "EmployeeRoleID <> '" + employeeRole.RoleID
                + "' and EmployeeID = '" + employeeRole.EmployeeID
                + "' and RoleID = '" + employeeRole.RoleID + "'";//判断员工与角色关系表中是否已有相同信息（同一EmployeeID和同一RoleID）
            if (1 == dvEmployeeRoleCopy.Count && 0 == dvEmployeeRole.Count)
            {//判断此RoleID是否存在，是则修改
                if (authorityMan.UpdateEmployeeRole(employeeRole))
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
        /// 查询所有员工与角色关系信息
        /// </summary>
        /// <returns></returns>
        public DataTable QueryEmployeeRole()
        {
            AuthorityManager authorityMan = new AuthorityManager();
            DataTable dtEmployeeRole = authorityMan.SelectEmployeeRole();
            DataView dvEmployeeRole = dtEmployeeRole.DefaultView;
            dvEmployeeRole.Sort = "EmployeeSequence ASC,EmployeeFirstName ASC,EmployeeLastName ASC,RoleName ASC";
            return dvEmployeeRole.ToTable();
        }
    }
}
