using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    [Serializable]
    [DataContract]
    /// <summary>
    /// 店铺坐标信息
    /// </summary>
    public class ShopCoordinate
    {
        [DataMember]
        /// <summary>
        /// 店铺坐标编号
        /// </summary>
        public int shopCoordinateId { get; set; }
        [DataMember]
        /// <summary>
        /// 店铺编号
        /// </summary>
        public int shopId { get; set; }
        [DataMember]
        /// <summary>
        /// 地图编号
        /// </summary>
        public int mapId { get; set; }
        [DataMember]
        /// <summary>
        /// 经度
        /// </summary>
        public double longitude { get; set; }
        [DataMember]
        /// <summary>
        /// 纬度
        /// </summary>
        public double latitude { get; set; }
    }
}
