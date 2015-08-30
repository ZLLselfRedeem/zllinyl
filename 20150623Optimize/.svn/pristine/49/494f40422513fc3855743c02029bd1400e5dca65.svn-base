using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data; 

using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.Model.Interface;
using VAGastronomistMobileApp.Model.QueryObject; 
namespace VAGastronomistMobileApp.WebPageDll
{
    public partial class Preorder19DianLineOperate
    {
        public static bool AddList(List<Preorder19DianLine> list)
        {
            return  _Preorder19DianLineManager.AddList(list);  
        }

        /// <summary>
        /// 财务查询指定时期粮票退款金额
        /// </summary>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public DataTable QueryFoodPreOrder19dian(DateTime beginTime, DateTime endTime, int cityID)
        {
            return _Preorder19DianLineManager.SelectFoodPreOrder19dian(beginTime, endTime, cityID);
        }

        /// <summary>
        /// 查询某点单的第三方支付金额
        /// </summary>
        /// <param name="preOrder19dianId"></param>
        /// <returns></returns>
        public double SelectThirdPayAmountOfOrder(long preOrder19dianId)
        {
            return _Preorder19DianLineManager.SelectThirdPayAmountOfOrder(preOrder19dianId);
        }
    }
}
