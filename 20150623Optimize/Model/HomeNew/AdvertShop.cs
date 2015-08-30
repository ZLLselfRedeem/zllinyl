using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VAGastronomistMobileApp.Model.HomeNew
{
    public class AdvertShop
    {
        public int id
        {
            get;
            set;
        }

        /// <summary>
        /// 城市ID
        /// </summary>
        public int cityID
        {
            get;
            set;
        }
        /// <summary>
        /// 广告商家ID
        /// </summary>		
        public int shopID
        {
            get;
            set;
        }
        /// <summary>
        /// 对应的一级标题ID
        /// </summary>		
        public int firstTitleID
        {
            get;
            set;
        }
        public string firstTitleName
        {
            get;
            set;
        }

        /// <summary>
        /// 对应的二级标题ID
        /// </summary>		
        public int secondTitleID
        {
            get;
            set;
        }

        public string secondTitleName
        {
            get;
            set;
        }
        /// <summary>
        /// 推荐顺序
        /// </summary>		
        public int index
        {
            get;
            set;
        }
        /// <summary>
        /// 标题名称
        /// </summary>		
        public string title
        {
            get;
            set;
        }
        /// <summary>
        /// 副标题名称
        /// </summary>		
        public string subtitle
        {
            get;
            set;
        }

        public string yuanImageUrl
        {
            get;
            set;
        }
        /// <summary>
        /// 状态  1、上架 2、下架
        /// </summary>		
        public int status
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
        /// 创建人
        /// </summary>		
        public string createBy
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
    }
}
