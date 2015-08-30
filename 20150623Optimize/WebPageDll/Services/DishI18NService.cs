using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL.Persistence;

namespace VAGastronomistMobileApp.WebPageDll.Services
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDishI18NService
    {
        /// <summary>
        /// 更据ID获取菜品
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        IList<DishI18n> GetDishI18NsByIds(int[] ids);
    }

    /// <summary>
    /// 
    /// </summary>
    public class DishI18NService : IDishI18NService
    {
        private readonly IDishI18NRepository _dishI18NRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dishI18NRepository"></param>
        public DishI18NService(IDishI18NRepository dishI18NRepository)
        {
            _dishI18NRepository = dishI18NRepository;
        }

        public IList<DishI18n> GetDishI18NsByIds(int[] ids)
        {
            var list = _dishI18NRepository.GetMany(p => ids.Contains(p.DishID) && p.DishI18nStatus == 1).ToList();
            return list;
        }
    }
}
