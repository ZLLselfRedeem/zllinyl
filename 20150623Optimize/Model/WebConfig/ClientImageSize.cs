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
    /// 客户端图片处理参数
    /// 创建日期：2014-5-29
    /// </summary>
    public class ClientImageSize
    {
        [DataMember]
        /// <summary>
        /// 编号
        /// </summary>
        public int id { get; set; }
        [DataMember]
        /// <summary>
        /// 设备类型（iphone，android...）
        /// </summary>
        public int apptype { get; set; }
        [DataMember]
        /// <summary>
        /// 屏幕宽度
        /// </summary>
        public string screenWidth { get; set; }
        [DataMember]
        /// <summary>
        /// 图片类别（菜图，门店形象...）
        /// </summary>
        public int imageType { get; set; }
        [DataMember]
        /// <summary>
        /// 图片处理参数
        /// </summary>
        public string value { get; set; }
        [DataMember]
        /// <summary>
        /// 数据状态（1,-1）
        /// </summary>
        public int status { get; set; }
    }
}
