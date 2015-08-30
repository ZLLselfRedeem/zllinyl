using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    [DataContract]
    public class FoodDiary
    {
        [DataMember(Name = "id")]
        public long Id { set; get; }
        [DataMember(Name = "name")]
        public string Name { set; get; }

        [DataMember(Name = "orderId")]
        public long OrderId { set; get; }
        [DataMember(Name = "content")]
        public string Content { set; get; }
        [DataMember(Name = "weather")]
        public string Weather { set; get; }
        [DataMember(Name = "shopName")]
        public string ShopName { set; get; }
        [DataMember(Name = "shoppingDate")]
        public DateTime ShoppingDate { set; get; }
        [DataMember(Name = "shared")]
        public FoodDiaryShared Shared { set; get; }
        [DataMember(Name = "createTime")]
        public DateTime CreateTime { set; get; }
        public int Hit { get; set; }
        [DataMember(Name = "isBig")]
        public bool IsBig { get; set; }
        [DataMember(Name = "isHideDishName")]
        public bool IsHideDishName { get; set; }

        [DataMember(Name = "foodDiaryDishes")]
        public virtual ICollection<FoodDiaryDish> FoodDiaryDishes { set; get; }

    }

    [Flags]
    public enum FoodDiaryShared : byte
    {
        没有分享 = 0,
        新浪微博 = 1,
        QQ空间 = 2,
        微信朋友圈 = 4,
        微信好友 = 8,
        QQ好友 = 16
    }
}
