using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using VAGastronomistMobileApp.Model;
using System.Configuration;
using ChineseCharacterToPinyin;
using System.Transactions;
using System.Data;

namespace VAGastronomistMobileApp.WebPageDll
{
    /// <summary>
    /// 商户注册
    /// 老后台和公司官网调用
    /// </summary>
    public class RegisterOperate
    {
        ///// <summary>
        /////网站注册
        ///// </summary>
        ///// <param name="txt_VerifyCode">验证码，暂时不用</param>
        ///// <param name="txt_OwndCompany">品牌名称</param>
        ///// <param name="txt_CompanyName">公司名称</param>
        ///// <param name="txt_ComfirmPassword">重复密码</param>
        ///// <param name="txt_UserName">用户名</param>
        ///// <param name="txt_CompanyAddress">公司地址</param>
        ///// <param name="txt_CompanyTelePhone">公司电话</param>
        ///// <param name="txt_ContactPerson">公司联系人</param>
        ///// <param name="txt_ContactPhone">联系人电话</param>
        ///// <returns></returns>
        //public string MerchantRegister(string txt_VerifyCode, string txt_OwndCompany, string txt_CompanyName,
        //string txt_UserName, string txt_CompanyAddress, string txt_CompanyTelePhone, string txt_ContactPerson, string txt_ContactPhone)
        //{
        //    string resultStr = string.Empty;
        //    EmployeeOperate employeeOperate = new EmployeeOperate();
        //    if (employeeOperate.IsEmployeeUserNameExit(txt_UserName.Trim()) == false)
        //    {
        //        return "该用户已注册";
        //    }
        //    CompanyOperate companyOperate = new CompanyOperate();
        //    if (companyOperate.CompanyNameValid(txt_CompanyName.Trim()))
        //    {
        //        return "该公司已注册";
        //    }
        //    EmployeeInfo employeeInfo = new EmployeeInfo();
        //    CompanyInfo companyInfo = new CompanyInfo();
        //    ShopInfo shopInfo = new ShopInfo();
        //    VAMenu vAMenu = new VAMenu();
        //    using (TransactionScope scope = new TransactionScope())
        //    {
        //        if (RegisterInfo(txt_CompanyName, txt_UserName, txt_CompanyAddress, txt_CompanyTelePhone, txt_ContactPerson,
        //       txt_ContactPhone, txt_OwndCompany, ref  employeeInfo, ref  companyInfo, ref  shopInfo, ref  vAMenu))
        //        {
        //            FunctionResult functionResult = new FunctionResult();
        //            functionResult = employeeOperate.Register(employeeInfo, companyInfo, shopInfo, vAMenu);
        //            if (functionResult.returnResult > 0)
        //            {
        //                scope.Complete();
        //                resultStr = "注册成功";
        //            }
        //            else
        //            {
        //                resultStr = "注册失败";
        //            }
        //        }
        //        return resultStr;
        //    }
        //}
        /// <summary>
        /// 注册子方法（老后台和公司网站调用次方法）
        /// </summary>
        /// <param name="txt_CompanyName">公司名称</param>
        /// <param name="txt_UserName">用户名</param>
        /// <param name="txt_CompanyAddress">公司地址</param>
        /// <param name="txt_CompanyTelePhone">公司电话</param>
        /// <param name="txt_ContactPerson">公司联系人</param>
        /// <param name="txt_ContactPhone">联系人电话</param>
        /// <param name="txt_OwndCompany">品牌名称</param>
        /// <param name="employeeInfo"></param>
        /// <param name="companyInfo"></param>
        /// <param name="shopInfo"></param>
        /// <param name="vAMenu"></param>
        /// <returns></returns>
        public bool RegisterInfo(DataTable dtEmployee, string txt_CompanyName, string txt_UserName, string txt_CompanyAddress,
            string txt_CompanyTelePhone, string txt_ContactPerson, string txt_ContactPhone, string txt_OwndCompany,
            ref CompanyInfo companyInfo, ref ShopInfo shopInfo, ref VAMenu vAMenu, int flag = 0)
        {
            //公司信息
            companyInfo.companyAddress = txt_CompanyAddress.Trim();
            companyInfo.companyLogo = "";
            companyInfo.companyName = txt_CompanyName.Trim();
            companyInfo.companyStatus = 1;//公司状态
            companyInfo.companyTelePhone = txt_CompanyTelePhone.Trim();
            companyInfo.contactPerson = txt_ContactPerson.Trim();
            companyInfo.contactPhone = txt_ContactPhone.Trim();
            companyInfo.companyDescription = "";
            companyInfo.ownedCompany = txt_OwndCompany.Trim();

            // string imagePath = ConfigurationManager.AppSettings["ImagePath"].ToString();
            //需要处理一下
            //if (flag == 1)
            // {
            //     imagePath = "../" + imagePath;
            // }
            string companyImagePath = CharacterToPinyin.GetAllPYLetters(companyInfo.companyName) + DateTime.Now.ToString("yyyyMMddHHmmssfff") + "/";
            companyInfo.companyImagePath = companyImagePath;
            //  bool companyFolderCreateTag = Common.FolderCreate(HttpContext.Current.Server.MapPath(imagePath + companyImagePath));//生成一个文件夹

            //默认店铺信息
            shopInfo.canEatInShop = false;
            shopInfo.canTakeout = false;
            shopInfo.cityID = 87;//默认为杭州
            shopInfo.contactPerson = string.Empty;
            shopInfo.contactPhone = string.Empty;
            shopInfo.countyID = 846;
            shopInfo.provinceID = 11;//默认浙江
            shopInfo.shopAddress = string.Empty;
            shopInfo.shopBusinessLicense = string.Empty;
            shopInfo.shopHygieneLicense = string.Empty;
            shopInfo.shopLogo = "";
            shopInfo.shopName = "默认门店";
            shopInfo.shopStatus = (int)VAShopStatus.SHOP_NORMAL;
            shopInfo.shopTelephone = string.Empty;
            shopInfo.isHandle = (int)VAShopHandleStatus.SHOP_UnHandle;
            shopInfo.shopDescription = string.Empty;
            shopInfo.sinaWeiboName = "";
            shopInfo.qqWeiboName = "";
            shopInfo.wechatPublicName = "";
            shopInfo.openTimes = "";
            shopInfo.shopRegisterTime = DateTime.Now;//注册时间为当前系统时间
            shopInfo.shopVerifyTime = DateTime.Now;//门店初始为未审核状态，默认时间也是取系统当前时间
            shopInfo.acpp = 0;
            shopInfo.publicityPhotoPath = "";

            shopInfo.shopImagePath = companyInfo.companyImagePath + CharacterToPinyin.GetAllPYLetters(Common.ToClearSpecialCharString(shopInfo.shopName)) + DateTime.Now.ToString("yyyyMMddHHmmssfff") + "/";
            // bool shopFolderCreateTag = Common.FolderCreate(HttpContext.Current.Server.MapPath(imagePath + shopInfo.shopImagePath));
            //默认菜谱信息
            vAMenu.menuDesc = string.Empty;
            vAMenu.menuName = "默认菜谱";
            vAMenu.langID = 1;//默认中文
            vAMenu.menuSequence = 1;
            vAMenu.menuImagePath = shopInfo.shopImagePath + CharacterToPinyin.GetAllPYLetters(Common.ToClearSpecialCharString(vAMenu.menuName)) + DateTime.Now.ToString("yyyyMMddHHmmssfff") + "/";
            // bool menuFolderCreateTag = Common.FolderCreate(HttpContext.Current.Server.MapPath(imagePath + vAMenu.menuImagePath));
            // if (shopFolderCreateTag)
            // {
            return true;
            // }
            // else
            // {
            //     return false;
            // }
        }
    }
}
