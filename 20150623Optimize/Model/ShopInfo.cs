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
    /// 门店基本信息
    /// </summary>
    public class ShopInfo
    {
        public ShopInfo()
        {
            preorderGiftValidTimeType = 0;
            preorderGiftValidTime = Convert.ToDateTime("1970-1-1");
            preorderGiftValidDay = 0;
            preorderGiftValid = 0;
            shopRegisterTime = Convert.ToDateTime("1970-1-1");
            shopVerifyTime = Convert.ToDateTime("1970-1-1");
            isSupportAccountsRound = true;
            shopRating = 0;
            acpp = 0;
            accountManager = 0;
            bankAccount = 0;
            isSupportRedEnvelopePayment = true;
        }
        [DataMember]
        /// <summary>
        /// 门店编号
        /// </summary>
        public int shopID { get; set; }
        [DataMember]
        /// <summary>
        /// 门店所属公司编号
        /// </summary>
        public int companyID { get; set; }
        [DataMember]
        /// <summary>
        /// 门店名称
        /// </summary>
        public string shopName { get; set; }
        [DataMember]
        /// <summary>
        /// 门店地址
        /// </summary>
        public string shopAddress { get; set; }
        [DataMember]
        /// <summary>
        /// 门店电话
        /// </summary>
        public string shopTelephone { get; set; }
        [DataMember]
        /// <summary>
        /// 门店图片
        /// </summary>
        public string shopLogo { get; set; }
        [DataMember]
        /// <summary>
        /// 门店营业执照
        /// </summary>
        public string shopBusinessLicense { get; set; }
        [DataMember]
        /// <summary>
        /// 门店卫生许可证
        /// </summary>
        public string shopHygieneLicense { get; set; }
        [DataMember]
        /// <summary>
        /// 门店联系人
        /// </summary>
        public string contactPerson { get; set; }
        [DataMember]
        /// <summary>
        /// 门店联系人电话
        /// </summary>
        public string contactPhone { get; set; }
        [DataMember]
        /// <summary>
        /// 门店是否能外送
        /// </summary>
        public bool canTakeout { get; set; }
        [DataMember]
        /// <summary>
        /// 门店是否可堂食
        /// </summary>
        public bool canEatInShop { get; set; }
        [DataMember]
        /// <summary>
        /// 门店所属省份编号
        /// </summary>
        public int provinceID { get; set; }
        [DataMember]
        /// <summary>
        /// 门店所属城市编号
        /// </summary>
        public int cityID { get; set; }
        [DataMember]
        /// <summary>
        /// 门店所属片区编号
        /// </summary>
        public int countyID { get; set; }
        [DataMember]
        /// <summary>
        /// 门店状态
        /// </summary>
        public int shopStatus { get; set; }
        [DataMember]
        /// <summary>
        /// 门店是否审批
        /// </summary>
        public int isHandle { get; set; }
        [DataMember]
        /// <summary>
        /// 图片地址
        /// </summary>
        public string shopImagePath { get; set; }
        [DataMember]
        /// <summary>
        /// 店铺描述
        /// </summary>
        public string shopDescription { get; set; }
        [DataMember]
        /// <summary>
        /// 店铺新浪微博
        /// </summary>
        public string sinaWeiboName { get; set; }
        [DataMember]
        /// <summary>
        /// 店铺赠送名称
        /// </summary>
        public string preorderGiftTitle { get; set; }
        [DataMember]
        /// <summary>
        /// 店铺赠送描述
        /// </summary>
        public string preorderGiftDesc { get; set; }
        [DataMember]
        /// <summary>
        /// 店铺赠送有效期类型
        /// </summary>
        public byte? preorderGiftValidTimeType { get; set; }
        [DataMember]
        /// <summary>
        /// 店铺赠送有效期时间
        /// </summary>
        public DateTime? preorderGiftValidTime { get; set; }
        [DataMember]
        /// <summary>
        /// 店铺赠送有效期天数
        /// </summary>
        public int? preorderGiftValidDay { get; set; }
        [DataMember]
        /// <summary>
        /// 店铺赠送是否有效
        /// </summary>
        public int? preorderGiftValid { get; set; }
        [DataMember]
        /// <summary>
        /// 店铺腾讯微博
        /// </summary>
        public string qqWeiboName { get; set; }
        [DataMember]
        /// <summary>
        /// 2013-7-26 wangcheng
        /// 店铺微信公共帐号
        /// </summary>
        public string wechatPublicName { get; set; }
        [DataMember]
        /// <summary>
        /// 营业时间
        /// </summary>
        public string openTimes { get; set; }
        [DataMember]
        /// <summary>
        /// 门店注册时间
        /// </summary>
        public DateTime? shopRegisterTime { get; set; }
        [DataMember]
        /// <summary>
        /// 门店验证时间
        /// </summary>
        public DateTime? shopVerifyTime { get; set; }
        [DataMember]
        /// <summary>
        /// 店铺结算是否支持四舍五入
        /// </summary>
        public bool? isSupportAccountsRound { get; set; }
        [DataMember]
        /// <summary>
        /// 店铺评分 
        /// Add at 2014-1-3 by jinyanni
        /// </summary>
        public double? shopRating { get; set; }
        [DataMember]
        /// <summary>
        /// 店铺形象宣传照路径
        /// Add at 2014-1-3 by jinyanni
        /// </summary>
        public string publicityPhotoPath { get; set; }
        [DataMember]
        /// <summary>
        /// 人均消费
        /// Add at 2014-1-7 by jinyanni
        /// </summary>
        public double? acpp { get; set; }
        [DataMember]
        /// <summary>
        /// 是否支持付款
        /// </summary>
        public bool isSupportPayment { get; set; }
        [DataMember]
        /// <summary>
        /// 门店点菜描述
        /// </summary>
        public string orderDishDesc { get; set; }
        [DataMember]
        /// <summary>
        /// 门店暂不支持支付原因
        /// </summary>
        public string notPaymentReason { get; set; }
        [DataMember]
        /// <summary>
        /// 客户经理编号 add by wangc 20140328
        /// </summary>
        public int? accountManager { get; set; }
        [DataMember]
        /// <summary>
        /// 门店银行帐号，关联CompanyAccounts表主键 add by wangc 20140425
        /// </summary>
        public int? bankAccount { get; set; }
        [DataMember]
        //public virtual City City { set; get; }
        /// <summary>
        /// 门店是否支持红包支付
        /// </summary>
        public bool isSupportRedEnvelopePayment { get; set; }
        [DataMember]
        /// <summary>
        /// 门店等级
        /// </summary>
        public int shopLevel { get; set; }
        [DataMember]
        public long prepayOrderCount { get; set; }
        [DataMember]
        /// <summary>
        /// 未结款余额
        /// </summary>
        public double remainMoney { get; set; }

        [DataMember]
        /// <summary>
        /// 区域经理
        /// </summary>
        public int? AreaManager { get; set; }
        /// <summary>
        /// 未结款余额
        /// </summary>
        public double remainRedEnvelopeAmount { get; set; }
        [DataMember]
        /// <summary>
        /// 未结款余额
        /// </summary>
        public double remainFoodCouponAmount { get; set; }
        [DataMember]
        /// <summary>
        /// 未结款余额
        /// </summary>
        public double remainAlipayAmount { get; set; }
        [DataMember]
        /// <summary>
        /// 未结款余额
        /// </summary>
        public double remainWechatPayAmount { get; set; }
        [DataMember]
        /// <summary>
        /// 未结款余额
        /// </summary>
        public double remainCommissionAmount { get; set; }
    }
    public class Shop
    {
        public int shopId { get; set; }
        public string shopName { get; set; }
    }

    public class ShopAndCompany
    {
        public int shopId { get; set; }
        public string shopName { get; set; }
        public string companyName { get; set; }
        public double? remainMoney { get; set; }
    }

    public partial class ShopInfoQueryObject
    {
        public int? ShopID
        {
            get;
            set;
        }

        public int? CompanyID
        {
            get;
            set;
        }

        public string ShopName
        {
            get;
            set;
        }
        public string ShopNameFuzzy
        {
            get;
            set;
        }

        public string ShopAddress
        {
            get;
            set;
        }

        public string ShopAddressFuzzy
        {
            get;
            set;
        }

        public int? CityID
        {
            get;
            set;
        }

        public int? CountyID
        {
            get;
            set;
        }

        public int? ShopStatus
        {
            get;
            set;
        }

        public double? RemainMoneyFrom
        {
            get;
            set;
        }

        public double? RemainMoneyTo
        {
            get;
            set;
        }
        public double? RemainMoney
        {
            get;
            set;
        }

        public DateTime?  ShopRegisterTime 
        {
            get;
            set;
        }

        public DateTime?  ShopRegisterTimeFrom
        {
            get;
            set;
        }

        public DateTime?  ShopRegisterTimeTo
        {
            get;
            set;
        }

        public int?  IsHandle
        {
            get;
            set;
        }
    }
    [Flags]
    public enum WithdrawType
    {
        monday = 1,
        tuesday = 2,
        wednesday = 4,
        thursday = 8,
        friday = 16,
    }
    
}
