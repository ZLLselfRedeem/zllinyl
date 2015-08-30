using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL;
using System.Data;

namespace VAGastronomistMobileApp.WebPageDll
{
    /// <summary>
    /// 勋章信息操作
    /// </summary>
    public class MedalOperate
    {
        /// <summary>
        /// new新增勋章图片信息
        /// </summary>
        public long InsertMedalImageInfo(MedalImageInfo medalImageInfo)
        {
            MedalManager medalManager = new MedalManager();
            return medalManager.InsertMedalImageInfoTable(medalImageInfo);
        }
        /// <summary>
        /// new新增勋章信息
        /// </summary>
        public long InsertMedalConnShopCompany(MedalConnShopCompany medalConnShopCompany)
        {
            MedalManager medalManager = new MedalManager();
            return medalManager.InsertMedalConnShopCompanyTable(medalConnShopCompany);
        }

        /// <summary>
        /// 查询勋章信息
        /// </summary>
        public DataTable QueryMedalInfoTable(int companyOrShopId, int medalType, int medalScale)
        {
            MedalManager medalManager = new MedalManager();
            return medalManager.SelectMedalInfoTable(companyOrShopId, medalType, medalScale);
        }
        /// <summary>
        /// 查询勋章信息根据勋章Id
        /// </summary>
        public DataTable QueryMedalInfoTableById(long id)
        {
            MedalManager medalManager = new MedalManager();
            return medalManager.SelectMedalInfoTableById(id);
        }
        /// <summary>
        /// 
        /// </summary>
        public bool DeleteMedalInfo(long medalId, int companyOrShopId)
        {
            MedalManager medalManager = new MedalManager();
            return medalManager.DeleteMedalInfoTable(medalId, companyOrShopId);
        }
        public bool ModifyMedalNameAndDescription(string name, string description, long id)
        {
            MedalManager medalManager = new MedalManager();
            return medalManager.UpdateMedalNameAndDescription(name, description, id);
        }
        /// <summary>
        /// 查询勋章图片信息 add by wangc 20140419
        /// </summary>
        /// <param name="customEncourageId"></param>
        /// <returns></returns>
        public DataTable QueryMedalInfoByMedalId(long medalId)
        {
            MedalManager medalManager = new MedalManager();
            return medalManager.SelectMedalInfoByMedalId(medalId);
        }
    }
}
