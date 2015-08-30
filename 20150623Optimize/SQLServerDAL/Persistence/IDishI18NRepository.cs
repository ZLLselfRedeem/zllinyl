using System.Collections.Generic;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL.Persistence.Infrastructure;

namespace VAGastronomistMobileApp.SQLServerDAL.Persistence
{
    public interface IDishI18NRepository
    {
        /// <summary>
        /// ��ȡָ��ID�Ĳ�Ʒ�б�
        /// </summary>
        /// <param name="dishIds"></param>
        /// <returns></returns>
        IEnumerable<DishI18n> GetDishI18NsByDishIds(params int[] dishIds);
    }
}