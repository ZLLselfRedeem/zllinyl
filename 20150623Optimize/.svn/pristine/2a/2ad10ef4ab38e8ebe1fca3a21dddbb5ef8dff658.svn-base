using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    /// <summary>
    /// 菜的图片信息
    /// </summary>
    public class ImageInfo
    {
        /// <summary>
        /// 图片编号
        /// </summary>
        public int ImageID { get; set; }
        /// <summary>
        /// 对应菜编号
        /// </summary>
        public int DishID { get; set; }
        /// <summary>
        /// 图片存放名称
        /// </summary>
        public string ImageName { get; set; }
        /// <summary>
        /// 图片序号
        /// 同一菜多图排序
        /// </summary>
        public int ImageSequence { get; set; }
        /// <summary>
        /// 图片规格
        /// 0：普通图片，1：缩略图
        /// </summary>
        public ImageScale ImageScale { get; set; }
        /// <summary>
        /// 图片状态（是否删除）
        /// 0：已删除，1：正常
        /// </summary>
        public int ImageStatus { get; set; }
    }

    public enum ImageScale
    {
        普通图片 = 0,
        缩略图 = 1
    }
}
