using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    /// <summary>
    /// 积分商城兑换商品
    /// 2014-2-21 jinyanni
    /// </summary>
    public class Goods
    {
        /// <summary>
        /// 商品编号：唯一标识
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 商品名
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 商品图片
        /// </summary>
        public string pictureName { get; set; }
        /// <summary>
        /// 兑换价格
        /// </summary>
        public double exchangePrice { get; set; }
        /// <summary>
        /// 库存剩余
        /// </summary>
        public int residueQuantity { get; set; }
        /// <summary>
        /// 已兑换数量
        /// </summary>
        public int haveExchangeQuantity { get; set; }
        /// <summary>
        /// 用户可见
        /// </summary>
        public string remark { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public bool visible { get; set; }
        /// <summary>
        /// 数据状态
        /// </summary>
        public int status { get; set; }
    }
}
