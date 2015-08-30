using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    [Serializable]
    [DataContract]
    public class ShopPreOrder
    {
        [DataMember]
        public long preOrder19dianId { get; set; }
        [DataMember]
        public int shopId { get; set; }
        [DataMember]
        public long customerId { get; set; }
        [DataMember]
        public DateTime prePayTime { get; set; }
        [DataMember]
        public byte isApproved { get; set; }
        [DataMember]
        public double refundMoneySum { get; set; }
        [DataMember]
        public string deskNumber { get; set; }
        [DataMember]
        public string invoiceTitle { get; set; }
        [DataMember]
        public byte isShopConfirmed { get; set; }
        [DataMember]
        public string UserName { get; set; }
        [DataMember]
        public string mobilePhoneNumber { get; set; }
        [DataMember]
        public int PreOrderTotalQuantity { get; set; }
        [DataMember]
        public string Picture { get; set; }
        [DataMember]
        public DateTime RegisterDate { get; set; }
        [DataMember]
        public int Shared { get; set; }
        [DataMember]
        public double afterDeductionPrice { get; set; }//抵扣后价格
        [DataMember]
        public double beforeDeductionPrice { get; set; }//抵扣前价格
        [DataMember]
        public int hadCoupon { get; set; }//是否使用了抵价券

    }

    [Serializable]
    [DataContract]
    public class PreOrderList
    {
        [DataMember]
        public long preOrder19dianId { get; set; }//流水号
        [DataMember]
        public string UserName { get; set; }//顾客姓名
        [DataMember]
        public string mobilePhoneNumber { get; set; }//手机号码
        [DataMember]
        public DateTime prePayTime { get; set; }//支付时间
        [DataMember]
        public byte isShopConfirmed { get; set; }//是否已入座  
        [DataMember]
        public double afterDeductionPrice { get; set; }//抵扣后价格
        [DataMember]
        public double beforeDeductionPrice { get; set; }//抵扣前价格
        [DataMember]
        public double refundMoneySum { get; set; }//退款金额
        [DataMember]
        public Guid orderId { get; set; }//总订单号
        [DataMember]
        public byte orderType { get; set; }//订单类型，1:正常点单;2:补差价点单
    }

    [Serializable]
    [DataContract]
    public class PreOrderListAttach
    {
        [DataMember]
        public long preOrder19dianId { get; set; }//流水号
        [DataMember]
        public int PreOrderTotalQuantity { get; set; }//客户订单数量
        [DataMember]
        public string Picture { get; set; }//客户头像  
        [DataMember]
        public DateTime RegisterDate { get; set; }//注册时间
        [DataMember]
        public string invoiceTitle { get; set; }//发票
        [DataMember]
        public double refundMoneySum { get; set; }//退款金额
        [DataMember]
        public string deskNumber { get; set; }//桌号
        [DataMember]
        public bool hadShared { get; set; }//是否已分享
        [DataMember]
        public bool hadCoupon { get; set; }//是否使用了抵扣券
        [DataMember]
        public string mobilePhoneNumber { get; set; }//手机号码 
        [DataMember]
        public byte isShopConfirmed { get; set; }//是否已入座
        [DataMember]
        public Guid orderId { get; set; }//总订单号
    }   
}
