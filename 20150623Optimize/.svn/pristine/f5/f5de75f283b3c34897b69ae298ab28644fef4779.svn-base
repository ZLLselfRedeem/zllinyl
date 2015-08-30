using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    [Serializable]
    [DataContract]
    public class ClientOrderDetailConfig
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public int Type { get; set; }
        [DataMember]
        public int Status { get; set; }
    }

    public enum ClientOrderDetailStatus
    {
        普通订单 = 1,
    }
}
