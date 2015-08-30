using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using VAGastronomistMobileApp.SQLServerDAL;
namespace VAGastronomistMobileApp.WebPageDll
{
    /// <summary> 
    /// FileName: DishOptionOperate.cs 
    /// CLRVersion: 4.0.30319.269 
    /// Author: TDQ 
    /// Corporation:杭州友络科技有限公司
    /// Description: 
    /// DateTime: 2012-06-28 10:37:57 
    /// </summary>
   public class DishOptionOperate
    {
        /// <summary>
        /// 查询所有备注分类信息
        /// </summary>
        /// <returns></returns>
        public DataTable QueryDishOptionType()
        {
            DishOptionManager dishOptionManager = new DishOptionManager();
            return dishOptionManager.SelectDishOptionType();
        }
        /// <summary>
        /// 根据备注分类编号查询对应的备注详情
        /// </summary>
        /// <returns></returns>
        public DataTable QueryDishOptionDetail(int optionTypeId)
        {
            DishOptionManager dishOptionManager = new DishOptionManager();
            return dishOptionManager.SelectDishOptionDetail(optionTypeId);
        }
    }
}
