using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    [Serializable]
    [DataContract]
    public class ClientStartImgConfig
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string ImgUrl { get; set; }
        [DataMember]
        public int Type { get; set; }
        [DataMember]
        public int Status { get; set; }
        [DataMember]
        public DateTime CreateDate { get; set; }
        [DataMember]
        public int ScaleType { get; set; }
        [DataMember]
        public int Sequence { get; set; }
        [DataMember]
        public int AppType { get; set; }
    }

    public enum ClientStartImg
    {
        图片 = 1,
    }

    public enum ClientStartImgScaleType
    {
        三比二 = 1,
        十六比九 = 2,
    }
}
