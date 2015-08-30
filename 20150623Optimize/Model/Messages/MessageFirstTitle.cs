using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    public class MessageFirstTitle
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 城市ID
        /// </summary>
        public int CityID { get; set; }

        /// <summary>
        /// 标签名称
        /// </summary>
        public string TitleName { get; set; }

        /// <summary>
        /// 标签排序
        /// </summary>
        public int TitleIndex { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool Enable { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public int CreateBy { get; set; }

        /// <summary>
        /// 是否主标签
        /// </summary>
        public bool IsMaster { get; set; }

        /// <summary>
        /// 是否商户
        /// </summary>
        public bool IsMerchant { get; set; }
    }

    public class MessageFirstTitleViewModel
    {
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 标签名
        /// </summary>
        public string TitleName { get; set; }

        /// <summary>
        /// 标签名
        /// </summary>
        public bool IsMaster { get; set; }
    }
}
