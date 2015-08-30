using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace VAGastronomistMobileApp.Model
{

    [Serializable]
    [DataContract]
    public class ShopTag
    {
         [DataMember]
        public int TagId { get; set; }
         [DataMember]
        public string Name { get; set; }
         [DataMember]
        public int Flag { get; set; }
         [DataMember]
        public int ShopCount { get; set; }
    }

    [Serializable]
    [DataContract]
    public class ShopTagExt
    {
        [DataMember]
        public int TagId { get; set; }
        [DataMember]
        public int ShopId { get; set; }
    }
}
