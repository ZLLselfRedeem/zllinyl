using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    public class ClientExtendModel
    {

    }
    [Serializable]
    [DataContract]
    public class VAIndexExt//门店列表信息
    {
        [DataMember]
        public int prepayOrderCount { get; set; }
        [DataMember]
        public int shopID { get; set; }
        [DataMember]
        public string shopName { get; set; }
        [DataMember]
        public string shopLogo { get; set; }
        [DataMember]
        public string shopAddress { get; set; }//门店地址
        [DataMember]
        public int defaultMenuId { get; set; }
        [DataMember]
        public string shopImagePath { get; set; }
        [DataMember]
        public int prePayVIPCount { get; set; }
        [DataMember]
        public string orderDishDesc { get; set; }
        [DataMember]
        public double shopRating { get; set; }
        [DataMember]
        public string publicityPhotoPath { get; set; }
        [DataMember]
        public double acpp { get; set; }
        [DataMember]
        public bool isSupportAccountsRound { get; set; }
        [DataMember]
        public double longitude { get; set; }//门店经度
        [DataMember]
        public double latitude { get; set; }//门店纬度
        [DataMember]
        public int menuId { get; set; }
        [DataMember]
        public int shopLevel { get; set; }
        [DataMember]
        public bool isSupportPayment { get; set; }
        [DataMember]
        public int goodEvaluationCount { get; set; }
    }

    public enum VAIndexSorting
    {
        我吃过的 = 1,
        我看过的 = 11,
        关注的店 = 12,
        有券的店= 13
    }
}
