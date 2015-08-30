using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using VAGastronomistMobileApp.Model;

namespace VAGastronomistMobileApp.IDAL
{
    public interface IDishManager
    {
        /// <summary>
        /// 新增菜名信息
        /// </summary>
        /// <param name="dish"></param>
        int InsertDish(DishInfo dish);
        /// <summary>
        /// 新增菜名多语言信息
        /// </summary>
        /// <param name="dish"></param>
        int InsertDishI18n(DishI18n dish);
        /// <summary>
        /// 新增菜和显示分类关系信息
        /// </summary>
        /// <param name="dishConnType"></param>
        int InsertDishConnType(DishConnType dishConnType);
        /// <summary>
        /// 新增菜价格信息
        /// </summary>
        /// <param name="dishPrice"></param>
        int InsertDishPrice(DishPriceInfo dishPrice);
        /// <summary>
        /// 新增菜价格多语言信息
        /// </summary>
        /// <param name="dishPrice"></param>
        int InsertDishPriceI18n(DishPriceI18n dishPrice);
        /// <summary>
        /// 新增菜图片信息
        /// </summary>
        /// <param name="image"></param>
        int InsertDishImage(ImageInfo image);
        /// <summary>
        /// 新增菜视频信息
        /// </summary>
        /// <param name="video"></param>
        int InsertDishVideo(VideoInfo video);
        /// <summary>
        /// 删除菜名信息（及对应的多语言信息，价格信息，图片信息，视频信息）
        /// </summary>
        /// <param name="dishID"></param>
        bool DeleteDishByID(int dishID);
        /// <summary>
        /// 删除菜名多语言信息
        /// </summary>
        /// <param name="dishI18nID"></param>
        void DeleteDishI18nByID(int dishI18nID);
        /// <summary>
        /// 删除菜和显示分类关系信息
        /// </summary>
        /// <param name="dishI18nID"></param>
        bool DeleteDishConnTypeByID(int dishConnTypeID);
        /// <summary>
        /// 删除菜价信息
        /// </summary>
        /// <param name="dishPriceID"></param>
        bool DeleteDishPriceByID(int dishPriceID);
        /// <summary>
        /// 删除菜价多语言信息
        /// </summary>
        /// <param name="dishPriceI18nID"></param>
        bool DeleteDishPriceI18nByID(int dishPriceI18nID);
        /// <summary>
        /// 删除菜图像信息
        /// </summary>
        /// <param name="imageID"></param>
        bool DeleteDishImageByID(int imageID);
        /// <summary>
        /// 删除菜视频信息
        /// </summary>
        /// <param name="videoID"></param>
        bool DeleteDishVideoByID(int videoID);
        /// <summary>
        /// 修改菜名信息
        /// </summary>
        /// <param name="dish"></param>
        bool UpdateDish(DishInfo dish);
        /// <summary>
        /// 修改菜名多语言信息
        /// </summary>
        /// <param name="dish"></param>
        bool UpdateDishI18n(DishI18n dish);
        /// <summary>
        /// 修改菜和显示分类关系信息
        /// </summary>
        /// <param name="dishConnType"></param>
        bool UpdateDishConnType(DishConnType dishConnType);
        /// <summary>
        /// 修改菜价信息
        /// </summary>
        /// <param name="dishPrice"></param>
        bool UpdateDishPrice(DishPriceInfo dishPrice);
        /// <summary>
        /// 修改菜价多语言信息
        /// </summary>
        /// <param name="dishPrice"></param>
        bool UpdateDishPriceI18n(DishPriceI18n dishPrice);
        /// <summary>
        /// 修改菜图片信息
        /// </summary>
        /// <param name="image"></param>
        bool UpdateDishImage(ImageInfo image);
        /// <summary>
        /// 修改菜视频信息
        /// </summary>
        /// <param name="video"></param>
        bool UpdateDishVideo(VideoInfo video);
        /// <summary>
        /// 根据语言编号查询该语言的所有菜名信息
        /// </summary>
        /// <param name="langID"></param>
        /// <returns></returns>
        DataTable QueryDish();
        /// <summary>
        /// 根据菜名编号查询菜价信息
        /// </summary>
        /// <param name="dishID"></param>
        /// <returns></returns>
        DataTable QueryDishPrice(int dishID);
        /// <summary>
        /// 根据菜名编号查询菜图片信息
        /// </summary>
        /// <param name="dishID"></param>
        /// <returns></returns>
        DataTable QueryDishImage(int dishID);
        /// <summary>
        /// 根据菜名编号查询菜视频信息
        /// </summary>
        /// <param name="dishID"></param>
        /// <returns></returns>
        DataTable QueryDishVideo(int dishID);
        /// <summary>
        /// 根据菜名多语言编号查询菜编号
        /// </summary>
        /// <param name="dishI18nID"></param>
        /// <returns></returns>
        int QueryDishID(int dishI18nID);
        /// <summary>
        /// 根据菜价多语言编号查询菜价编号
        /// </summary>
        /// <param name="dishPriceI18nID"></param>
        /// <returns></returns>
        int QueryDishPriceID(int dishPriceI18nID);
        /// <summary>
        /// 根据菜价多语言编号查询该规格的菜的所有信息
        /// </summary>
        /// <param name="dishPriceI18nID"></param>
        /// <returns></returns>
        DataTable QueryDishScale(int dishPriceI18nID);
    }
}
