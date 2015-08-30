using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    /// <summary>
    /// 红包领用记录
    /// </summary>
    //public class RedEnvelopeDetail
    //{
    //    /// <summary>
    //    /// 主键标识
    //    /// </summary>
    //    public long Id { get; set; }

    //    /// <summary>
    //    /// 宝箱id
    //    /// </summary>
    //    public long treasureChestId { get; set; }

    //    /// <summary>
    //    /// 用户手机号码
    //    /// </summary>
    //    public string mobilePhoneNumber { get; set; }
    //    /// <summary>
    //    /// 红包编号
    //    /// </summary>
    //    public long redEnvelopeId { get; set; }
    //    /// <summary>
    //    /// 红包金额
    //    /// </summary>
    //    public double redEnvelopeAmount { get; set; }
    //    /// <summary>
    //    /// 红包过期时间
    //    /// </summary>
    //    public DateTime redEnvelopeExpirationTime { get; set; }
    //    /// <summary>
    //    /// 操作时间（领取时间、使用消费时间）
    //    /// </summary>
    //    public DateTime operationTime { get; set; }
    //    /// <summary>
    //    /// 记录类别（未生效类型，已生效类型，过期类型，预留已删除类型）
    //    /// </summary>
    //    public int stateType { get; set; }
    //    /// <summary>
    //    /// 使用金额
    //    /// </summary>
    //    public double usedAmount { get; set; }
    //    /// <summary>
    //    /// 用户Cookie
    //    /// </summary>
    //    public string cookie { get; set; }
    //    /// <summary>
    //    /// 门店名称
    //    /// </summary>
    //    public string shopName { get; set; }
    //    /// <summary>
    //    /// 点单编号
    //    /// </summary>
    //    public long preOrder19dianId { get; set; }
    //}
    /// <summary>
    /// 客户端交互展示结构
    /// </summary>
    public class ClientRedEnvelopeDetail
    {
        public double amount { get; set; }
        public double time { get; set; }
        public string statusDes { get; set; }
        public double expirationTime { get; set; }
    }
    /// <summary>
    /// 页面web view交互展示结构
    /// </summary>
    public class WebRedEnvelopeDetailModel
    {
        public double amount { get; set; }//红包金额
        public double usedAmount { get; set; }//已使用金额
        public DateTime effectTime { get; set; }//生效时间
        public DateTime expireTime { get; set; }//到期时间
        public int statusType { get; set; }//状态  0：已生效，1：未生效，2：已使用，3：已过期
    }

    public class WebRedEnvelopeDetailViewModel
    {
        public double amount { get; set; }//红包金额
        public double usedAmount { get; set; }//已使用金额
        public string effectTime { get; set; }//生效时间
        public string expireTime { get; set; }//到期时间
        public int statusType { get; set; }//状态  0：已生效，1：未生效，2：已使用，3：已过期
    }

    public class WebRedEnvelope
    {
        public List<WebRedEnvelopeDetailViewModel> detailList { get; set; }
        public bool isHaveMore { get; set; }
    }

    /// <summary>
    /// 红包状态
    /// </summary>
    public enum VARedEnvelopeStateType : byte
    {
        未生效 = 0,
        已生效 = 1,
        已使用 = 2,
        已过期 = 3,
        已删除 = 4,
        红包满 = 5,
        已作废 = 6,
        未激活 = 7,
    }

    public class WebRedEnvelopeViewModel
    {
        //用户手机号码
        //  public string phone1 { get; set; }
        //箱主手机号码
        //  public string phone2 { get; set; }
        public double redEnvelopeAmount { get; set; }
        public DateTime redEnvelopeExpirationTime { get; set; }
        public DateTime operationTime { get; set; }
        public int stateType { get; set; }
        public double usedAmount { get; set; }
        public string shopName { get; set; }
        // public int lockCount { get; set; }
        // public long treasureChestId { get; set; }
        public int flag { get; set; }
        public DateTime redEnvelopeEffectiveBeginTime { get; set; }
    }
}
