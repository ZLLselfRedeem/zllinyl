using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using VAGastronomistMobileApp.SQLServerDAL;

namespace VAGastronomistMobileApp.WebPageDll
{
    /// <summary>
    /// 系统语言操作类
    /// </summary>
    /// <returns></returns>
    public class LangOperate
    {
        /// <summary>
        /// 查询语言信息
        /// </summary>
        /// <returns></returns>
        public DataTable SearchLang()
        {
            LangManager langMan = new LangManager();
            return langMan.QueryLang();
        }
        /// <summary>
        /// 获取默认语言编号
        /// </summary>
        /// <returns></returns>
        public int DefaultLangID()
        {
            LangManager langMan = new LangManager();
            DataTable dtLang = langMan.QueryLang();
            DataView dvLang = dtLang.DefaultView;
            dvLang.RowFilter = "IsDefaultLang = TRUE";
            int defaultLangID = (int)dvLang[0]["LangID"];//获取默认语言的LangID
            return defaultLangID;
        }
    }
}
