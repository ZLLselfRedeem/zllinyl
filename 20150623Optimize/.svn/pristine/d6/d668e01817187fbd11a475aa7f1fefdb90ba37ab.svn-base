using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using VAGastronomistMobileApp.SQLServerDAL;
namespace VAGastronomistMobileApp.WebPageDll
{
    /// <summary> 
    /// FileName: CountyOperate.cs 
    /// CLRVersion: 4.0.30319.269 
    /// Author: TDQ 
    /// Corporation:杭州友络科技有限公司
    /// Description: 
    /// DateTime: 2012-05-17 12:57:01 
    /// </summary>
   public class CountyOperate
    {
        /// <summary>
        /// 根据城市查询地区信息
        /// </summary>
        /// <returns></returns>
        public DataTable QueryCounty(int cityID)
        {
            CountyManager countyManager = new CountyManager();
            DataTable dtCounty = countyManager.SelectCounty();
            DataView dvCounty = dtCounty.DefaultView;
            dvCounty.RowFilter = "CityID=" + cityID;
            return dvCounty.ToTable();
        }
    }
}
