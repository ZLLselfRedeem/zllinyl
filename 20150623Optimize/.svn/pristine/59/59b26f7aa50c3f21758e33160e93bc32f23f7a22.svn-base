using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    /// <summary>
    /// 美食广场模型
    /// </summary>
    public class FoodPlaza
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long foodPlazaId { set; get; }

        /// <summary>
        /// 点单编号
        /// </summary>
        public long preOrder19DianId { set; get; }

        /// <summary>
        /// 城市编号
        /// </summary>
        public int cityId { set; get; }

        /// <summary>
        /// 门店编号
        /// </summary>
        public int shopId { set; get; }

        /// <summary>
        /// 门店名称
        /// </summary>
        public string shopName { set; get; }

        /// <summary>
        /// 标记状态
        /// </summary>
        public bool status { set; get; }

        /// <summary>
        /// 最后更新时间
        /// </summary>
        public DateTime latestUpdateTime { set; get; }

        /// <summary>
        /// 是否置顶
        /// </summary>
        public bool isListTop { set; get; }

        /// <summary>
        /// 菜品编号集
        /// </summary>
        public string dishIds { set; get; }

        /// <summary>
        /// 用户姓名
        /// </summary>
        public string customerName { set; get; }

        /// <summary>
        /// 用户编号
        /// </summary>
        public long customerId { set; get; }

        /// <summary>
        /// 用户图像URL
        /// </summary>
        public string personImgUrl { set; get; }

        /// <summary>
        /// 最后变更数据的员工编号
        /// </summary>
        public int latestOperateEmployeeId { set; get; }

        /// <summary>
        /// 当前点单金额
        /// </summary>
        public double orderAmount { get; set; }
    }

    public class FoodPlazaTempClild : FoodPlaza
    {
        public DateTime registerDate { get; set; }
    }
    #region 辅助model

    public class FoodPlazaOrder
    {
        public long foodPlazaId { get; set; }
        public long preOrder19dianId { get; set; }
        public string orderInJson { get; set; }
        public string shopName { get; set; }
        public string personImgUrl { get; set; }
        public double preOrderSum { get; set; }
        public long customerId { set; get; }
        public int shopId { set; get; }
        public string customerName { set; get; }
        public DateTime registerDate { get; set; }
        public int cityId { set; get; }
    }
    public class FoodPlazaConfigPage
    {
        public long foodPlazaId { get; set; }
        public long preOrder19dianId { get; set; }
        public string shopName { get; set; }
        public string personImgUrl { get; set; }
        public double preOrderSum { get; set; }
        public long customerId { set; get; }
        public List<FoodPlazaDish> dishImgs { get; set; }
        public int shopId { set; get; }
        public string customerName { set; get; }
        public int cityId { set; get; }
        public bool isListTop { set; get; }
    }
    [Serializable]
    public class FoodPlazaDish
    {
        public int dishId { get; set; }
        public string dishImg { get; set; }
    }
    [Serializable]
    public class ClientFoodPlazaTemp
    {
        public string dishIds { get; set; }
        public string customerName { get; set; }
        public string content { get; set; }
        public string picture { get; set; }
        public long foodDiaryId { get; set; }
        public DateTime registerDate { get; set; }
        public int shopId { set; get; }
    }
    #endregion
}
