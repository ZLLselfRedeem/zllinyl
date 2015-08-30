using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL.Persistence;

namespace VAGastronomistMobileApp.WebPageDll.Services
{
    /// <summary>
    /// 图片业务逻辑接口
    /// </summary>
    public interface IImageInfoService
    {
        /// <summary>
        /// 获收指定规格的菜品图片
        /// </summary>
        /// <param name="imageScale"></param>
        /// <param name="dishIds"></param>
        /// <returns></returns>
        IList<ImageInfo> GetDishImageInfosByScale(int imageScale, params int[] dishIds);
    }

    /// <summary>
    /// 图片业务逻辑实现
    /// </summary>
    public class ImageInfoService : IImageInfoService
    {
        private readonly IImageInfoRepository _imageInfoRepository;

        public ImageInfoService(IImageInfoRepository imageInfoRepository)
        {
            _imageInfoRepository = imageInfoRepository;
        }

        public IList<ImageInfo> GetDishImageInfosByScale(int imageScale, params int[] dishIds)
        {
            var list =
                _imageInfoRepository.GetMany(
                    p => dishIds.Contains(p.DishID) && p.ImageStatus == 1 && p.ImageScale == imageScale).ToList();

            return list;
        }
    }
}
