using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    /// <summary>
    /// created by wangc 20140402
    /// 收银宝商家信息配置Model
    /// </summary>
    public class MerchantInfo
    {
    }
    /// <summary>
    /// 收银宝公司基本信息
    /// </summary>
    public class MerchantCompanyInfo
    {
        public int companyID { get; set; }
        public string companyAddress { get; set; }
        public string companyLogo { get; set; }
        public string companyName { get; set; }
        public int companyStatus { get; set; }
        public string companyTelePhone { get; set; }
        public string contactPerson { get; set; }
        public string contactPhone { get; set; }
        public string companyDescription { get; set; }
        public string ownedCompany { get; set; }
        public string companyImagePath { get; set; }
        public string sinaWeiboName { get; set; }
        public string qqWeiboName { get; set; }
        public string wechatPublicName { get; set; }
        public double acpp { get; set; }
    }
    public class MerchantShopInfo
    {
        public int shopID { get; set; }
        public int companyID { get; set; }
        public string shopName { get; set; }
        public string shopAddress { get; set; }
        public string shopTelephone { get; set; }
        public string shopLogo { get; set; }
        public string shopBusinessLicense { get; set; }
        public string shopHygieneLicense { get; set; }
        public string contactPerson { get; set; }
        public string contactPhone { get; set; }
        public int provinceID { get; set; }
        public int cityID { get; set; }
        public int countyID { get; set; }
        public int shopStatus { get; set; }
        public int isHandle { get; set; }
        public string shopImagePath { get; set; }
        public string shopDescription { get; set; }
        public string sinaWeiboName { get; set; }
        public string qqWeiboName { get; set; }
        public string wechatPublicName { get; set; }
        public string openTimes { get; set; }
        public DateTime shopRegisterTime { get; set; }
        public bool isSupportAccountsRound { get; set; }
        public double shopRating { get; set; }
        public string publicityPhotoPath { get; set; }
        public double acpp { get; set; }
        public bool isSupportPayment { get; set; }
        public string orderDishDesc { get; set; }
        public string notPaymentReason { get; set; }
        public int accountManager { get; set; }
        //门店经纬度
        public double latitude { get; set; }
        public double longitude { get; set; }

        public int menuCompanyId { get; set; }

        public int bankAccount { get; set; }

        public int tagId { get; set; }
        public bool isTrueRedenvelopePayment { get; set; }
        //门店状态 
        //门店审核状态

        public int areaManager { get; set; }
    }
    public class SybShopHandeleDetail : MerchantShopInfo
    {
        public string cityName { get; set; }
        public string provinceName { get; set; }
        public string countyName { get; set; }
        public string companyName { get; set; }
        public string accountManagerName { get; set; }
        public string areaManagerName { get; set; }
    }
    public class SybShopBankAccount
    {
        public int bankAccount { get; set; }
        public string bankAccountDesc { get; set; }
    }
    /*
    收银宝菜谱模块
    */
    public class LangInfo
    {
        public int langId { get; set; }
        public string langName { get; set; }
    }
    public class CompanyList
    {
        public int companyId { get; set; }
        public string companyName { get; set; }
    }

    public class Menu
    {
        public int menuId { get; set; }
        public string menuDesc { get; set; }
        public string menuName { get; set; }
    }
    public class MerchantMenuInfo
    {
        //是否为默认菜谱功能去掉
        public List<LangInfo> langListInfo { get; set; }
        public int companyId { get; set; }
        public string companyName { get; set; }
        public Menu menuInfo { get; set; }
        public int menuCompanyId { get; set; }
    }
    /*
    收银宝门店模块
    */
    public class ShopList
    {
        public int shopId { get; set; }
        public string shopName { get; set; }
    }
    public class CompanyShopList
    {
        public int companyId { get; set; }
        public string companyName { get; set; }
        public int shopId { get; set; }
        public string shopName { get; set; }
    }
    public class SybProvince
    {
        public int provinceId { get; set; }
        public string provinceName { get; set; }
    }
    public class SybCity
    {
        public int cityId { get; set; }
        public string cityName { get; set; }
    }
    public class SybCounty
    {
        public int countryId { get; set; }
        public string countryName { get; set; }
    }
    public class ShopRevealImageInfo
    {
        public long id { get; set; }
        public string imgUrl { get; set; }
    }
    public class SybShopRevealImage : ShopRevealImageInfo
    {
        public int count { get; set; }
    }
    //门店审核
    public class ShopHandleListInfo
    {
        public int shopId { get; set; }
        public string shopName { get; set; }
        public string companyName { get; set; }
        public int handleStatus { get; set; }//门店审核状态
    }
    /*
     门店杂项
     */
    public class SybSundry
    {
        public int sundryId { get; set; }
        public int shopId { get; set; }
        public string sundryName { get; set; }
        public string sundryStandard { get; set; }
        public int sundryChargeMode { get; set; }
        public double price { get; set; }
        public int status { get; set; }
        public string description { get; set; }
        public bool required { get; set; }
    }
    public class SybSundryList
    {
        public int sundryId { get; set; }
        public string sundryName { get; set; }
        public int status { get; set; }
    }
    /*
    门店折扣 
    */
    public class SybVip
    {
        public SybShopVip shopVip { get; set; }
        public List<ViewAllocVip> vaVip { get; set; }
    }
    public class SybShopVipInfo
    {
        public int id { get; set; }
        public string name { get; set; }
        public string platformVipName { get; set; }
        public double discount { get; set; }
    }
    public class SybShopVip
    {
        public int id { get; set; }
        public int platformVipId { get; set; }
        public string name { get; set; }
        public int shopId { get; set; }
        public double discount { get; set; }
        public int status { get; set; }
    }
    public class ViewAllocVip
    {
        public int id { get; set; }
        public string name { get; set; }
    }
    /// <summary>
    /// 友络佣金
    /// </summary>
    public class VACommission
    {
        public int companyId { get; set; }
        public int viewallocCommissionType { get; set; }
        public double freeRefundHour { get; set; }
        public double viewallocCommissionValue { get; set; }
        public string companyName { get; set; }
    }
    /// <summary>
    /// 收银宝二维码信息
    /// </summary>
    public class QRCodeInfo
    {
        public int typeId { get; set; }
        public string typeName { get; set; }
        public string imgUrl { get; set; }
    }
    public class MenuConnCompanyInfo
    {
        public int menuCompanyId { get; set; }
        public int menuId { get; set; }
        public string menuName { get; set; }
        public string menuDes { get; set; }
    }
    public class MenuConnCompanyInfoExtension : MenuConnCompanyInfo
    {
        public int menuVersion { get; set; }
    }

    public class ShopMoney
    {
        /// <summary>
        /// 门店ID
        /// </summary>
        public int shopId { get; set; }
        /// <summary>
        /// 要追加的金额
        /// </summary>
        public double remainMoney { get; set; }
        /// <summary>
        /// 订单支付金额
        /// </summary>
        public double paySum { get; set; }
        /// <summary>
        /// 红包金额
        /// </summary>
        public double remainRedEnvelopeAmount { get; set; }
        /// <summary>
        /// 粮票金额
        /// </summary>
        public double remainFoodCouponAmount { get; set; }
        /// <summary>
        /// 支付宝支付金额
        /// </summary>
        public double remainAlipayAmount { get; set; }
        /// <summary>
        /// 微信支付金额
        /// </summary>
        public double remainWechatPayAmount { get; set; }
        /// <summary>
        /// 佣金金额
        /// </summary>
        public double remainCommissionAmount { get; set; }

    }
}
