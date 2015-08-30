﻿using System.Collections.Generic;
using PagedList;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL.Persistence.Infrastructure;

namespace VAGastronomistMobileApp.SQLServerDAL.Persistence
{
    public interface IDishInfoRepository
    {
        DishInfo GetById(long id);
        /// <summary>
        /// 随机获取菜单下菜品
        /// </summary>
        /// <param name="menuId"></param>
        /// <returns></returns>
        IEnumerable<Dish> GetRandomDishInfosByMenu(int top, int menuId, params int[] dishIds);

        /// <summary>
        /// 返回门店沽清菜品列表(分页)
        /// </summary>
        /// <param name="page"></param>
        /// <param name="shopId"></param>
        /// <returns></returns>
        IPagedList<DishDetails> GetPageShopSellOffDishDetailses(Page page, int shopId);

        /// <summary>
        /// 返回门店在售菜品列表(分页)
        /// </summary>
        /// <param name="page"></param>
        /// <param name="shopId"></param>
        /// <returns></returns>
        IPagedList<DishDetails> GetPageShopSellOnDishDetailses(Page page, int shopId);

        /// <summary>
        /// 返回门店所有菜品列表[含沽清](分页)
        /// </summary>
        /// <param name="page"></param>
        /// <param name="shopId"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        IPagedList<DishDetails> GetPageShopAllDishDetailses(Page page, int shopId, string keyword);

        /// <summary>
        /// 添加奖品时，搜索菜品列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="shopId"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        IPagedList<DishDetails> GetPageShopAllDishDetailsForAward(Page page, int shopId, string keyword);
    }
}