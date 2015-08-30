using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    public class AwardConnPreOrder
    {
        /// <summary>
        /// 用户奖品ID
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 奖品对应的中奖商家ID
        /// </summary>
        public int ShopId { get; set; }
        /// <summary>
        /// 点单ID
        /// </summary>
        public long PreOrder19dianId { get; set; }
        /// <summary>
        /// 订单ID
        /// </summary>
        public Guid OrderId { get; set; }

        /// <summary>
        /// 奖品类型(1、未中奖 2、免排队 3、菜 4、红包 5、第三方)
        /// </summary>
        public AwardType Type { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public long CustomerId { get; set; }
        /// <summary>
        /// 奖品ID
        /// </summary>
        public Guid AwardId { get; set; }
        /// <summary>
        /// 获取时间
        /// </summary>
        public DateTime LotteryTime { get; set; }
        /// <summary>
        /// 生效时间
        /// </summary>
        public DateTime ValidTime { get; set; }
        /// <summary>
        /// 数据是否有效
        /// </summary>
        public bool Status { get; set; }
        /// <summary>
        /// 红包奖品对应的RedEnvelopeId
        /// </summary>
        public long redEnvelopeId { get; set; }
    }
}
