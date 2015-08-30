using System.Collections.Generic;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL.Persistence.Infrastructure;

namespace VAGastronomistMobileApp.SQLServerDAL.Persistence
{
    public interface IImageInfoRepository
    {
        /// <summary>
        /// 获取指定规格菜品图片
        /// </summary>
        /// <param name="scale"></param>
        /// <param name="dishIds"></param>
        /// <returns></returns>
        IEnumerable<ImageInfo> GetAssignScaleImageInfosByDishId(ImageScale scale, params int[] dishIds);

        List<DishImage> GetAssignScaleImageInfosByDishId(string dishIds, string path);
        List<DishPraiseInfo> GetDishPraiseInfosByDishId(string dishIds);
    }
}