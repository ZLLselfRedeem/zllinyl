using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using VAGastronomistMobileApp.Model;

namespace VAGastronomistMobileApp.IDAL
{
    public interface ILangManager
    {
        /// <summary>
        /// 新增语言信息
        /// </summary>
        /// <param name="lang"></param>
        int InsertLang(LanguageInfo lang);
        /// <summary>
        /// 删除语言信息
        /// </summary>
        /// <param name="langID"></param>
        void DeleteLangByID(int langID);
        /// <summary>
        /// 修改语言信息
        /// </summary>
        /// <param name="lang"></param>
        void UpdateLang(LanguageInfo lang);
        /// <summary>
        /// 查询菜单信息
        /// </summary>
        /// <returns></returns>
        DataTable QueryLang();
    }
}
