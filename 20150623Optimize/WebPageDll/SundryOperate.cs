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
    /// 店铺杂项管理（wangcheng）
    /// </summary>
    public class SundryOperate
    {
        /// <summary>
        /// 添加默认杂项
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public long InsertDefaultSundryInfo(int shopId)
        {
            SundryInfo sundryInfo = new SundryInfo();
            sundryInfo.shopId = shopId;
            sundryInfo.status = 2;//默认关闭
            sundryInfo.price = 0.1;
            sundryInfo.sundryChargeMode = (int)VASundry.CLOSED;
            sundryInfo.sundryName = "服务费";
            sundryInfo.sundryStandard = "次";
            sundryInfo.description = "";
            sundryInfo.supportChangeQuantity = false;
            sundryInfo.vipDiscountable = false;
            sundryInfo.backDiscountable = false;
            sundryInfo.required = true;
            return InsertSundryInfo(sundryInfo);
        }
        /// <summary>
        /// （wangcheng）插入店铺杂项信息
        /// </summary>
        public long InsertSundryInfo(SundryInfo sundryInfo)
        {
            SundryManager sundryManager = new SundryManager();
            return sundryManager.InsertSundryInfoReturnidentity(sundryInfo);
        }
        /// <summary>
        /// （wangcheng）查询当前shop店铺杂项信息
        /// </summary>
        public DataTable QuerySundryInfo(int shopId)
        {
            SundryManager sundryManager = new SundryManager();
            return sundryManager.SelectSundryInfoByShopId(shopId);
        }
        /// <summary>
        /// （wangcheng）查询默认店铺杂项信息
        /// </summary>
        public DataTable QueryDefaultSundryInfo()
        {
            SundryManager sundryManager = new SundryManager();
            return sundryManager.SelectDefaultSundryInfo();
        }
        /// <summary>
        /// （wangcheng）修改杂项开启状态
        /// </summary>
        public bool UpdateSundryStatus(long sundryId, int status)
        {
            SundryManager sundryManager = new SundryManager();
            return sundryManager.UpdateSundryStatusBySundryId(sundryId, status);
        }
        /// <summary>
        /// （wangcheng）根据sundryId查询杂项信息UpdateShopSundayInfo
        /// </summary>
        public DataTable QuerySundryInfoBySundryId(long sundryId)
        {
            SundryManager sundryManager = new SundryManager();
            return sundryManager.SelectSundryInfoBySundryId(sundryId);
        }
        /// <summary>
        /// （wangcheng）修改更新杂项信息
        /// </summary>
        public long UpdateSundayInfo(SundryInfo sundryInfo, long sundryId)
        {
            SundryManager sundryManager = new SundryManager();
            return sundryManager.UpdateShopSundayInfo(sundryInfo, sundryId);
        }

        /// <summary>
        /// 更新店铺杂项信息（重载不修改状态）
        /// </summary>
        public long UpdateSundayInfo(long sundryId, SundryInfo sundryInfo)
        {
            SundryManager sundryManager = new SundryManager();
            return sundryManager.UpdateShopSundayInfo(sundryId, sundryInfo);
        }
        /// <summary>
        /// 检测杂项名称是否重复
        /// </summary>
        /// <param name="sundryName"></param>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public bool CheckSundryName(string sundryName, int shopId)
        {
            SundryManager man = new SundryManager();
            return man.SelectSundryInfoBySundryName(sundryName, shopId);
        }
    }
}
