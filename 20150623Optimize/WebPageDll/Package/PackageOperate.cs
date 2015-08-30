using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL;

namespace VAGastronomistMobileApp.WebPageDll
{
    public class PackageOperate
    {
        /// <summary>
        /// 获取套餐模板列表
        /// </summary>
        /// <param name="cityId">适用城市</param>
        /// <param name="Status">是否启用</param>
        /// <returns></returns>
        public DataTable Packages(int cityId,int status)
        {
            PackageManager pm = new PackageManager();
            return pm.Packages(cityId, status);
        }

        /// <summary>
        /// 获取单条记录明细
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public DataTable PackageDetail(int ID)
        {
            PackageManager pm = new PackageManager();
            return pm.PackageDetail(ID);
        }

        /// <summary>
        /// 添加一个模板
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Insert(Package model)
        {
            PackageManager pm = new PackageManager();
            return pm.Insert(model);
        }

        /// <summary>
        /// 修改一个模板
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Update(Package model)
        {
            PackageManager pm = new PackageManager();
            return pm.Update(model);
        }

         /// <summary>
        /// 改变套餐模板状态
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="Status"></param>
        /// <returns></returns>
        public int ChangeStatus(int ID, int Status)
        {
            PackageManager pm = new PackageManager();
            return pm.ChangeStatus(ID, Status);
        }

        /// <summary>
        /// 获取套餐模板列表
        /// </summary>
        /// <param name="cityId">适用城市</param>
        /// <param name="Status">是否启用</param>
        /// <returns></returns>
        public DataTable PackageStatistics(string ShopName, DateTime BeginDate, DateTime EndDate, int cityId, int PackageID)
        {
            PackageManager pm = new PackageManager();
            return pm.PackageStatistics(ShopName, BeginDate, EndDate, cityId, PackageID);
        }

        /// <summary>
        /// 获取券信息
        /// </summary>
        /// <param name="CouponId">券ID</param>
        /// <returns></returns>
        public DataTable PackageStatisticsView(int CouponId)
        {
            PackageManager pm = new PackageManager();
            return pm.PackageStatisticsView(CouponId);
        }
    }
}
