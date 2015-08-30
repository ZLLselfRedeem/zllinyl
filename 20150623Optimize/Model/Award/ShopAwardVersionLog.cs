using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    public class ShopAwardVersionLog
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 奖品ID
        /// </summary>
        public Guid ShopAwardId { get; set; }

        /// <summary>
        /// 版本ID
        /// </summary>
        public int ShopAwardVersionId { get; set; }

        /// <summary>
        /// 商家ID
        /// </summary>
        public int ShopId { get; set; }

        /// <summary>
        /// 变更内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 变更平台 
        /// </summary>
        public string ChangeSource { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateBy { get; set; }
    }
}
