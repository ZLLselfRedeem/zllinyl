using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.Model.Interface;
using VAGastronomistMobileApp.Model.QueryObject;
using VAGastronomistMobileApp.SQLServerDAL;

namespace VAGastronomistMobileApp.WebPageDll
{
    public partial class CouponVOperate
    {
        /// <summary>
        /// 依靠距离排序获取查询结果
        /// </summary>
        /// <param name="queryObject">查询条件中的经纬度数据不能为空</param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public static List<ICouponV> GetListByDistanceOrder(int pageSize, int pageIndex,CouponVQueryObject queryObject)
        {
            if (queryObject.Longitude == null | queryObject.Latitude == null)
            {
                return null;
            }
            return  _CouponVManager.GetListByDistanceOrder(pageSize, pageIndex , queryObject );
        }
    }
}