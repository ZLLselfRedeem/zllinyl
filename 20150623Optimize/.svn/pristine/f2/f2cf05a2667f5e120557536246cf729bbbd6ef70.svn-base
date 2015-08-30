using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VA.Cache.Distributed;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL;

namespace VA.CacheLogic.Dish
{
    public class DishCacheLogic
    {
        /// <summary>
        /// 缓存当前菜谱点赞次数
        /// </summary>
        /// <param name="menuId"></param>
        /// <returns></returns>
        public List<DishPraiseInfo> GetDishPraiseNumByMenuOfCache(int menuId)
        {
            object cache = MemcachedHelper.GetMemcached("dishPraiseNumByMenu_" + menuId);
            if (cache == null)
            {
                cache = new MenuManager().SelectDishPraiseNumByMenu(menuId);
                if (cache != null)
                {
                    MemcachedHelper.AddMemcached("dishPraiseNumByMenu_" + menuId, cache, 60 * 60);//缓存一个小时
                }
            }
            return (List<DishPraiseInfo>)cache;
        }
    }
}
