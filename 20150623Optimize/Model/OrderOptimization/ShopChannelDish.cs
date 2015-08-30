using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    /// <summary>
    /// 商家频道菜品
    /// </summary>
    public class ShopChannelDish
    {
        public int id
        {
            get;
            set;
        }
        /// <summary>
        /// 商家频道ID
        /// </summary>		
        public int shopChannelID
        {
            get;
            set;
        }
        /// <summary>
        /// 菜品ID
        /// </summary>		
        public int dishID
        {
            get;
            set;
        }
        /// <summary>
        /// 菜品规格ID
        /// </summary>		
        public int dishPriceID
        {
            get;
            set;
        }
        /// <summary>
        /// 菜品名称
        /// </summary>		
        public string dishName
        {
            get;
            set;
        }
        /// <summary>
        /// 菜品显示顺序
        /// </summary>		
        public int dishIndex
        {
            get;
            set;
        }
        /// <summary>
        /// 菜品摘要
        /// </summary>		
        public string dishContent
        {
            get;
            set;
        }
        /// <summary>
        /// 广告菜品图片URL
        /// </summary>		
        public string dishImageUrl
        {
            get;
            set;
        }
        /// <summary>
        /// 创建时间
        /// </summary>		
        public DateTime createTime
        {
            get;
            set;
        }
        /// <summary>
        /// 是否删除
        /// </summary>		
        public bool isDelete
        {
            get;
            set;
        }

        /// <summary>
        /// 发布状态--1-发布；0-未发布
        /// </summary>
        public bool status
        {
            get;
            set;
        }
    }
}
