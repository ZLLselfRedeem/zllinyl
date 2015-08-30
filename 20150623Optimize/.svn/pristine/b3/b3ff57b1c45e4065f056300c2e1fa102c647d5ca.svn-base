using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    /// <summary>
    /// 活动分享内容信息
    /// </summary>
    [Serializable]
    [DataContract]
    public class ActivityShareInfo
    {
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public int activityId { get; set; }
        [DataMember]
        /// <summary>
        /// 分享信息类型
        /// </summary>
        public ActivityShareInfoType type { get; set; }
        [DataMember]
        /// <summary>
        /// 备注
        /// 当<code>this.type==ActivityShareInfoType.Image</code>为图片的路径
        /// 当<code>this.type==ActivityShareInfoType.Text</code>为文本内容
        /// </summary>
        public string remark { get; set; }
        [DataMember]
        public bool status { get; set; }
    }

    public enum ActivityShareInfoType
    {
        /// <summary>
        /// 图片
        /// </summary>
        Image = 1,
        /// <summary>
        /// 文本
        /// </summary>
        Text = 2,
        /// <summary>
        /// 活动规则
        /// </summary>
        activityRule = 3
    }
}
