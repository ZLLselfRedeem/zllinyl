using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    /// <summary>
    /// 19点预点单信息
    /// </summary>
    public class PreOrder19dianInfo
    {
        public PreOrder19dianInfo()
        {
            isPaid = 0;
            preOrderServerSum = 0;
            prePaidSum = 0;
            prePayTime = Convert.ToDateTime("1970-1-1");
            viewallocCommission = 0;
            transactionFee = 0;
            viewallocNeedsToPayToShop = 0;
            viewallocPaidToShopSum = 0;
            viewallocTransactionCompleted = 0;
            discount = 0;
            refundMoneyClosedSum = 0;
            refundRedEnvelope = 0;
            expireTime = Convert.ToDateTime("1970-1-1");
        }
        /// <summary>
        /// 预点单编号
        /// </summary>
        public long preOrder19dianId { get; set; }
        /// <summary>
        /// 用户编号
        /// </summary>
        public long customerId { get; set; }
        /// <summary>
        /// 公司编号
        /// </summary>
        public int companyId { get; set; }
        /// <summary>
        /// 门店编号
        /// </summary>
        public int shopId { get; set; }
        /// <summary>
        /// 菜谱编号
        /// </summary>
        public int menuId { get; set; }
        /// <summary>
        /// 点单内容Json
        /// </summary>
        public string orderInJson { get; set; }
        /// <summary>
        /// 预点单时间
        /// </summary>
        public DateTime preOrderTime { get; set; }
        /// <summary>
        /// 预点单是否已支付
        /// </summary>
        public byte? isPaid { get; set; }
        /// <summary>
        /// 预点单总金额(客户端计算的)
        /// </summary>
        public double preOrderSum { get; set; }
        /// <summary>
        /// 预点单总金额(服务器计算的)
        /// </summary>
        public double? preOrderServerSum { get; set; }
        /// <summary>
        /// 预点单预支付金额
        /// </summary>
        public double? prePaidSum { get; set; }
        /// <summary>
        /// 预支付时间
        /// </summary>
        public DateTime? prePayTime { get; set; }
        /// <summary>
        /// 友络收取的佣金
        /// </summary>
        public double? viewallocCommission { get; set; }
        /// <summary>
        /// 第三方支付手续费
        /// </summary>
        public double? transactionFee { get; set; }
        /// <summary>
        /// 友络需要支付给商户的费用
        /// </summary>
        public double? viewallocNeedsToPayToShop { get; set; }
        /// <summary>
        /// 友络已经支付给商家的金额
        /// </summary>
        public double? viewallocPaidToShopSum { get; set; }
        /// <summary>
        /// 友络已经和商家结算完成
        /// </summary>
        public byte? viewallocTransactionCompleted { get; set; }
        /// <summary>
        /// 预点单状态
        /// </summary>
        public VAPreorderStatus status { get; set; }
        /// <summary>
        /// 用户设备号码
        /// </summary>
        public string customerUUID { get; set; }
        /// <summary>
        /// 杂项json
        /// </summary>
        public string sundryJson { get; set; }
        /// <summary>
        /// 用户享受的折扣
        /// </summary>
        public double? discount { get; set; }
        /// <summary>
        /// 桌号 add by wangc 20140319
        /// </summary>
        public string deskNumber { get; set; }
        /// <summary>
        /// 已完成打款的退款金额
        /// </summary>
        public double? refundMoneyClosedSum { get; set; }
        /// <summary>
        /// 已经退款的红包金额
        /// </summary>
        public double? refundRedEnvelope { get; set; }
        /// <summary>
        /// app类型
        /// </summary>
        public int appType { get; set; }
        /// <summary>
        /// app版本号
        /// </summary>
        public string appBuild { get; set; }
        public double? verifiedSaving { get; set; }
        public byte? isApproved { get; set; }
        public byte? isShopConfirmed { get; set; }
        public byte? isEvaluation { get; set; }
        public string invoiceTitle { get; set; }
        public double? refundMoneySum { get; set; }
        /// <summary>
        /// 订单过期时间
        /// </summary>
        public DateTime? expireTime { get; set; }
        public Guid OrderId { get; set; }

        public OrderTypeEnum OrderType { get; set; }

        /// <summary>
        /// 支付方式1、微信 2、支付宝 服务器端用于排序,客户端不需要
        /// </summary>
        public int OrderPayType { get; set; }

    } 
    /// <summary>
    /// 订单支付信息
    /// </summary>
    public class PreOrder19dianPayment
    {
        /// <summary>
        /// 点单编号
        /// </summary>
        public long preOrder19dianId { get; set; }
        /// <summary>
        /// 原价
        /// </summary>
        public decimal preOrderSum { get; set; }
        /// <summary>
        /// 支付金额
        /// </summary>
        public decimal prePaidSum { get; set; }
        /// <summary>
        /// 折扣优惠金额
        /// </summary>
        public decimal discountAmount { get; set; }
        /// <summary>
        /// 第三方金额
        /// </summary>
        public decimal thirdAmount { get; set; }
        /// <summary>
        /// 粮票金额
        /// </summary>
        public decimal balanceAmount { get; set; }
        /// <summary>
        /// 红包抵扣金额
        /// </summary>
        public decimal redEnvelopeAmount { get; set; }
        /// <summary>
        /// 抵价券抵扣金额
        /// </summary>
        public decimal couponAmount { get; set; }
        /// <summary>
        /// 折扣
        /// </summary>
        public decimal discount { get; set; }
        /// <summary>
        /// 抵价券名称
        /// </summary>
        public string couponName { get; set; }

        /// <summary>
        /// 补查价金额 add by zhujinlei 2015/06/30
        /// </summary>
        public decimal fillpostAmount { get; set; }
    }

    /// <summary>
    /// 未入座订单提醒信息
    /// </summary>
    public class PreOrder19dianRemindInfo
    {
        /// <summary>
        /// 订单流水号
        /// </summary>
        public long preOrder19dianId { get; set; }
        /// <summary>
        /// 门店名称
        /// </summary>
        public string shopName { get; set; }
        /// <summary>
        /// 支付金额
        /// </summary>
        public double prePaidSum { get; set; }
        /// <summary>
        /// 支付时间
        /// </summary>
        public double prePayTime { get; set; }
        /// <summary>
        /// 店铺经度
        /// </summary>
        public double longitude { get; set; }
        /// <summary>
        /// 店铺纬度
        /// </summary>
        public double latitude { get; set; }
    }
    /// <summary>
    /// 未入座订单提醒信息
    /// </summary>
    public class PreOrder19dianRemindInfoDBModel
    {
        /// <summary>
        /// 订单流水号
        /// </summary>
        public long preOrder19dianId { get; set; }
        /// <summary>
        /// 门店名称
        /// </summary>
        public string shopName { get; set; }
        /// <summary>
        /// 支付金额
        /// </summary>
        public double prePaidSum { get; set; }
        /// <summary>
        /// 支付时间
        /// </summary>
        public DateTime prePayTime { get; set; }
        /// <summary>
        /// 店铺经度
        /// </summary>
        public double longitude { get; set; }
        /// <summary>
        /// 店铺纬度
        /// </summary>
        public double latitude { get; set; }
        /// <summary>
        /// 服务端原价
        /// </summary>
        public double preOrderServerSum { get; set; }
        /// <summary>
        /// 折扣
        /// </summary>
        public double verifiedSaving { get; set; }
    }
}
