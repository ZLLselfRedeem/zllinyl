using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    /// <summary>
    /// 门店VIP用户模型
    /// </summary>
    [Serializable]
    public class ShopVIP
    {
        public ShopVIP()
        {
        }
        public long Id { set; get; }
        public long CustomerId { set; get; }
        public int ShopId { set; get; }
        public int CityId { set; get; }
        public int PreOrderTotalQuantity { set; get; }
        public decimal PreOrderTotalAmount { set; get; }
        public DateTime CreateTime { set; get; }
        public DateTime LastPreOrderTime { set; get; }
    }
    [Serializable]
    public class ZZB_ShopVipResponse
    {
        public int shopTotalCount { get; set; }
        public int currectCityTopShopTotalCount { get; set; }
        public string currectCityName { get; set; }
        public int currectMonthShopIncreasedCount { get; set; }
        public int currectMonthTopShopIncreasedCount { get; set; }
        public int currectMonth { get; set; }
    }
    public class ZZB_ShopVIPFlaseCountModel
    {
        public int currectCityTopShopTotalCountFalse { get; set; }
        public int currectMonthTopShopIncreasedCountFalse { get; set; }
    }
}
