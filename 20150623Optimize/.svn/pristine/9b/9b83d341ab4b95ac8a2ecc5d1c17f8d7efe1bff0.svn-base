using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using VAGastronomistMobileApp.Model;

namespace VAGastronomistMobileApp.IDAL
{
    public interface IDishTypeManager
    {
        /// <summary>
        /// 新增菜分类
        /// </summary>
        /// <param name="dishType"></param>
        int InsertDishType(DishTypeInfo dishType);
        /// <summary>
        /// 新增折扣分类
        /// </summary>
        /// <param name="discountType"></param>
        int InsertDiscountType(DiscountType discountType);
        /// <summary>
        /// 新增菜分类多语言
        /// </summary>
        /// <param name="dishType"></param>
        int InsertDishTypeI18n(DishTypeI18n dishType);
        /// <summary>
        /// 删除菜分类
        /// </summary>
        /// <param name="dishTypeID"></param>
        bool DeleteDishType(int dishTypeID);
        /// <summary>
        /// 删除折扣分类
        /// </summary>
        /// <param name="discountType"></param>
        bool DeleteDiscountType(int discountType);
        /// <summary>
        /// 修改菜分类
        /// </summary>
        /// <param name="dishType"></param>
        bool UpdateDishType(DishTypeInfo dishType);
        /// <summary>
        /// 修改折扣分类
        /// </summary>
        /// <param name="discountType"></param>
        bool UpdateDiscountType(DiscountType discountType);
        /// <summary>
        /// 修改菜分类多语言
        /// </summary>
        /// <param name="dishType"></param>
        bool UpdateDishTypeI18n(DishTypeI18n dishType);
        /// <summary>
        /// 根据菜分类多语言编号查询分类编号
        /// </summary>
        /// <param name="dishTypeI18nID"></param>
        int QueryDishTypeID(int dishTypeI18nID);
        /// <summary>
        /// 查询所有菜分类信息
        /// </summary>
        DataTable QueryDishType();
        /// <summary>
        /// 查询所有折扣分类信息
        /// </summary>
        DataTable QueryDiscountType();
    }
}
