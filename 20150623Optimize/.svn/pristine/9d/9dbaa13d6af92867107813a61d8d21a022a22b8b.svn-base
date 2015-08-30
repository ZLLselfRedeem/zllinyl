using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    /// <summary>
    /// 年夜饭套餐
    /// 2015-1-4
    /// </summary>
    public class Meal
    {
        public int MealID { get; set; }
        public int ShopID { get; set; }
        public double Price { get; set; }
        public double OriginalPrice { get; set; }
        public string MealName { get; set; }
        public string Menu { get; set; }
        public string Suggestion { get; set; }
        public string ImageURL { get; set; }

        public int IsActive { get; set; }

        public int OrderNumber { get; set; }

        public int CreatedBy { get; set; }
        public DateTime CreationDate { get; set; }
        public int? LastUpdatedBy { get; set; }

        public DateTime? LastUpdateDate { get; set; }
    }

    public class MealQueryObject
    {
        public string MealName { get; set; }
        public int? MealID { get; set; }
        public int? ShopID { get; set; }

        public int? IsActive { get; set; }

        public int? CompanyID { get; set; }

        public int? CityID { get; set; }

        public DateTime? CreationDateFrom { get; set; }
        public DateTime? CreationDateTo { get; set; }
    }
    public enum IsActive
    {
        /// </summary>
        [DescriptionAttribute("已上架")]
        Active = 1,
        /// <summary>
        /// 未启用
        /// </summary>
        [DescriptionAttribute("未上架")]
        NotActive = 0
    }
    /// <summary>
    /// 年夜饭套餐列表
    /// </summary>
    [Serializable]
    [DataContract]
    public class MealList
    {
        /// <summary>
        /// 套餐Id
        /// </summary>
        [DataMember]
        public int mealId { get; set; }
        [DataMember]
        public int shopId { get; set; }
        /// <summary>
        /// 门店名称
        /// </summary>
        [DataMember]
        public string shopName { get; set; }
        /// <summary>
        /// 门店联系电话
        /// </summary>
        [DataMember]
        public string contactPhone { get; set; }
        /// <summary>
        /// 门店地址
        /// </summary>
        [DataMember]
        public string shopAddress { get; set; }
        /// <summary>
        /// 套餐配图
        /// </summary>
        [DataMember]
        public string imageURL { get; set; }
        /// <summary>
        /// 套餐菜品简介
        /// </summary>
        [DataMember]
        public string menu { get; set; }
        /// <summary>
        /// 套餐价格
        /// </summary>
        [DataMember]
        public double price { get; set; }
        /// <summary>
        /// 套餐原价
        /// </summary>
        [DataMember]
        public double originalPrice { get; set; }
        /// <summary>
        /// 套餐建议人数
        /// </summary>
        [DataMember]
        public string suggestion { get; set; }
        /// <summary>
        /// 是否已售罄
        /// </summary>
        [DataMember]
        public bool isSoldOut { get; set; }
    }

    /// <summary>
    /// 年夜饭套餐剩余总份数
    /// </summary>
    public class MealListStatus
    {
        public int mealId { get; set; }
        public int remainCount { get; set; }
    }

    /// <summary>
    /// 年夜饭活动
    /// </summary>
    [Serializable]
    [DataContract]
    public class MealActivity
    {
        [DataMember]
        public List<MealOrder> customerMealOrder { get; set; }
        /// <summary>
        /// 活动规则
        /// </summary>
        [DataMember]
        public string activityRule { get; set; }
        /// <summary>
        /// 活动规则简版
        /// </summary>
        [DataMember]
        public string activityRuleMini { get; set; }
        /// <summary>
        /// 签约店铺所在的一级商圈
        /// </summary>
        [DataMember]
        public List<ShopTag> shopTag { get; set; }
        [DataMember]
        public List<MealShopList> mealShopList { get; set; }
        /// <summary>
        /// 是否还有下一页
        /// </summary>
        [DataMember]
        public bool isMore { get; set; }

    }
    /// <summary>
    /// 门店信息及所含套餐
    /// </summary>
    public class MealShopList
    {
        /// <summary>
        /// 门店Id
        /// </summary>
        public int shopId { get; set; }
        /// <summary>
        /// 门店名称
        /// </summary>
        public string shopName { get; set; }
        public List<MealList> mealList { get; set; }
    }

    /// <summary>
    /// 用户年夜饭订单
    /// </summary>
    public class MealOrder
    {
        /// <summary>
        /// 订单ID
        /// </summary>
        public long preOrder19dianId { get; set; }
        public OrderStatus status { get; set; }
        /// <summary>
        /// 下单时间
        /// </summary>
        public DateTime preOrderTime { get; set; }
        /// <summary>
        /// 有效支付时间点
        /// </summary>
        public DateTime validPayTime { get; set; }
        /// <summary>
        /// 门店名称
        /// </summary>
        public string shopName { get; set; }
        /// <summary>
        /// 套餐现价
        /// </summary>
        public double price { get; set; }
        /// <summary>
        /// 套餐时间
        /// </summary>
        public DateTime dinnerTime { get; set; }
        public int DinnerType { get; set; }
    }

    /// <summary>
    /// 套餐详情
    /// </summary>
    public class MealDetail
    {
        public MealList mealList;
        public List<MealSchedule> mealSchedule;
    }

    /// <summary>
    /// 年夜饭点单状态
    /// </summary>
    public enum OrderStatus
    {
        未付款 = 1,
        待确认 = 2,
        已确认 = 3,
        已退款 = 4,
        超时未付款 = 5,
        退款中 = 6,
    }
    /// <summary>
    /// 返回给前端的结果
    /// </summary>
    public class MealResult
    {
        public int error;
        public string msg;
    }
}
