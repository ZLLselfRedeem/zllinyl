using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    public class ActivityMessage
    {
        /// <summary>
        /// ID
        /// </summary>
        public long ID { get; set; }

        /// <summary>
        /// 活动名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 城市ID
        /// </summary>
        public int CityID { get; set; }

        /// <summary>
        /// 标题ID
        /// </summary>
        public int MessageFirstTitleID { get; set; }

        /// <summary>
        /// 消息类型
        /// </summary>
        public int MsgType { get; set; }

        /// <summary>
        /// 活动Logo
        /// </summary>
        public string ActivityLogo { get; set; }

        /// <summary>
        /// 活动说明
        /// </summary>
        public string ActivityExplain { get; set; }

        /// <summary>
        /// 店铺ID
        /// </summary>
        public int ShopID { get; set; }

        /// <summary>
        /// 活动广告图
        /// </summary>
        public string AdvertisementAddress { get; set; }

        /// <summary>
        /// 广告地址
        /// </summary>
        public string AdvertisementURL { get; set; }

        /// <summary>
        /// 红包活动表ID
        /// </summary>
        public int? ActivityID { get; set; }

        /// <summary>
        /// 礼券ID
        /// </summary>
        public int? CouponID { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public int CreateUser { get; set; }
        
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 修改人
        /// </summary>
        public int UpdateUser { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime UpdateDate { get; set; }
    }
}
