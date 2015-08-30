using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using VAGastronomistMobileApp.Model;

namespace VAGastronomistMobileApp.IDAL
{
    public interface IMenuManager
    {
        /// <summary>
        /// 新增菜单信息
        /// </summary>
        /// <param name="menu"></param>
        int InsertMenu(MenuInfo menu);
        /// <summary>
        /// 新增菜单多语言信息
        /// </summary>
        /// <param name="menu"></param>
        int InsertMenuI18n(MenuI18n menu);
        /// <summary>
        /// 删除菜单信息（及对应的多语言信息）
        /// </summary>
        /// <param name="menuID"></param>
        bool DeleteMenuByID(int menuID);
        /// <summary>
        /// 删除菜单多语言信息
        /// </summary>
        /// <param name="menuI18nID"></param>
        void DeleteMenuI18nByID(int menuI18nID);
        /// <summary>
        /// 修改菜单信息
        /// </summary>
        /// <param name="menu"></param>
        bool UpdateMenu(MenuInfo menu);
        /// <summary>
        /// 修改菜单多语言信息
        /// </summary>
        /// <param name="menu"></param>
        bool UpdateMenuI18n(MenuI18n menu);
        /// <summary>
        /// 修改版本信息（VersionInfo表）
        /// </summary>
        /// <param name="version"></param>
        void UpdateVersion(VersionInfo version);
        /// <summary>
        /// 查询菜单信息
        /// </summary>
        /// <returns></returns>
        DataTable QueryMenu();
        /// <summary>
        /// 根据菜单多语言编号查询菜单编号
        /// </summary>
        /// <param name="menuI18nID"></param>
        /// <returns></returns>
        int QueryMenuID(int menuI18nID);
        /// <summary>
        /// 查询总的菜单版本号
        /// </summary>
        /// <returns></returns>
        int QueryVersion();
    }
}
