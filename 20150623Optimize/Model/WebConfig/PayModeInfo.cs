using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    [Serializable]
    [DataContract]
    public class VAPayMode
    {
        [DataMember]
        public int payModeId { get; set; }
        [DataMember]
        public string payModeName { get; set; }
    }
}
