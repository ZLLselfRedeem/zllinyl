using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.Model;
using System.Configuration;
using ChineseCharacterToPinyin;
using System.Web;
using System.Transactions;
using System.Data;
using VAGastronomistMobileApp.SQLServerDAL;
using System.Runtime.Serialization;
using CloudStorage;
using System.Net;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing;
using System.Reflection;
using VAGastronomistMobileApp.WebPageDll.Services;
using VAGastronomistMobileApp.WebPageDll.Services.Infrastructure;
using VAGastronomistMobileApp.Model.QueryObject;
using VAGastronomistMobileApp.Model.Interface;

namespace VAGastronomistMobileApp.WebPageDll
{
    public enum SybAuthorityEnum
    {
        公司管理,//公司列表
        公司添加,
        门店管理,//门店列表
        门店添加,
        门店VIP配置,
        门店杂项配置,
        门店环境图配置,
        门店审核
    }
    /// <summary>
    /// created by wangc 20140402
    /// 收银宝商家信息配置业务逻辑
    /// </summary>
    public partial class MerchantInfoOperate
    {
        /// <summary>
        /// 收银宝权限
        /// </summary>
        /// <param name="interactionStatus"></param>
        /// <returns></returns>
        public static string GetUserAuthorityString(int interactionStatus)
        {
            //interactionStatus  1  仅员工版;2  仅商户版;3  员工版+商户版;4  表示没有任何权限
            VAEmployeeLoginResponse sessionInfo = (VAEmployeeLoginResponse)HttpContext.Current.Session["MerchantsTreasureUserInfo"];
            int employeeId = sessionInfo.employeeID;
            string userName = sessionInfo.userName;

            EmployeeConnShopOperate employeeConnShopOperate = new EmployeeConnShopOperate();
            List<VAEmployeeCompany> employeeCompany = new List<VAEmployeeCompany>();
            EmployeeOperate employeeOperate = new EmployeeOperate();
            EmployeeInfo employee = employeeOperate.QueryEmployee(employeeId);
            bool isViewAllocWorker = false;
            if (employee.isViewAllocWorker.HasValue && employee.isViewAllocWorker.Value)
            {
                employeeCompany = employeeConnShopOperate.QueryEmployeeCompanyRemoveNotShop(employeeId);
                isViewAllocWorker = true;
            }
            else
            {
                employeeCompany = employeeConnShopOperate.QueryEmployeeCompanyRemoveNotShop(employeeId, true);
                isViewAllocWorker = false;
            }

            bool canLoginMerchantModule = (employeeCompany != null && employeeCompany.Count > 0) ? true : false;//有商户版权限

            //interactionStatus = 2;//动态屏蔽收银宝后台功能模块

            string authorityStr = string.Empty;
            switch (interactionStatus)
            {
                case 0://初始化（规则：悠先进入员工板块）
                    if (canLoginMerchantModule == true && isViewAllocWorker == true)
                    {
                        //表示有进入商户版权限，和进入员工版权限
                        interactionStatus = 3;
                        authorityStr = GetViewAllocWorkerAuthority(employeeId, isViewAllocWorker, employeeOperate, userName);
                        if (string.IsNullOrEmpty(authorityStr))
                        {
                            interactionStatus = 2;
                            authorityStr = GetMerchantUserAuthority(employeeId, isViewAllocWorker, employeeOperate);
                        }
                    }
                    else if (canLoginMerchantModule == true && isViewAllocWorker == false)
                    {
                        //表示有进入商户版权限，和没有员工版权限
                        interactionStatus = 2;
                        authorityStr = GetMerchantUserAuthority(employeeId, isViewAllocWorker, employeeOperate);
                    }
                    else if (canLoginMerchantModule == false && isViewAllocWorker == true)
                    {
                        //表示有进入员工版权限，和没有商户版权限
                        interactionStatus = 1;
                        authorityStr = GetViewAllocWorkerAuthority(employeeId, isViewAllocWorker, employeeOperate, userName);
                    }
                    else//(canLoginMerchantModule == false && isViewAllocWorker == false)
                    {
                        interactionStatus = 0;
                        authorityStr = "";//没有任何权限
                    }
                    break;
                case 1://获取员工版权限
                    authorityStr = GetViewAllocWorkerAuthority(employeeId, isViewAllocWorker, employeeOperate, userName);
                    if (canLoginMerchantModule == true && isViewAllocWorker == true)
                    {
                        //表示有进入商户版权限，和进入员工版权限
                        interactionStatus = 3;
                    }
                    else
                    {
                        interactionStatus = 1;
                    }
                    break;
                case 2://获取商户版权限
                    authorityStr = GetMerchantUserAuthority(employeeId, isViewAllocWorker, employeeOperate);
                    if (canLoginMerchantModule == true && isViewAllocWorker == true)
                    {
                        //表示有进入商户版权限，和进入员工版权限
                        interactionStatus = 3;
                    }
                    else
                    {
                        interactionStatus = 2;
                    }
                    break;
                default:
                    interactionStatus = 0;
                    authorityStr = "";//没有任何权限
                    break;
            }

            //interactionStatus = 2;//动态屏蔽收银宝后台功能模块 
            return "{" + "\"userAuthority\":" + Common.GetJSON<string>(authorityStr) + ",\"status\":\"" + interactionStatus + "\"}";
        }
        /// <summary>
        /// 获取收银宝员工板块用户权限
        /// </summary>
        /// <returns></returns>
        private static string GetViewAllocWorkerAuthority(int employeeId, bool isViewAllocWorker, EmployeeOperate employeeOperate, string userName)
        {
            StringBuilder authorityStr = new StringBuilder();
            RoleOperate roleOperate = new RoleOperate();
            if (isViewAllocWorker)
            {
                DataTable dt = employeeOperate.QueryEmployeeAuthortiy(userName);
                DataTable specialAuthority = null;
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        switch (dt.Rows[i]["AuthorityURL"].ToString())
                        {
                            case "CompanyManage/CompanyAdd.aspx"://公司添加权限
                                authorityStr.Append("companyadd|");
                                break;
                            case "CompanyManage/CompanyManage.aspx"://公司列表权限
                                authorityStr.Append("companylist|");
                                break;
                            case "ShopManage/ShopAdd.aspx"://门店添加权限
                                authorityStr.Append("shopadd|");
                                break;
                            case "ShopManage/ShopManage.aspx"://门店列表权限
                                authorityStr.Append("shoplist|");
                                specialAuthority = roleOperate.QuerySpecialAuthorityInfoByEmployeeID(employeeId, 0);//获得特殊权限
                                if (specialAuthority.Rows.Count > 0)
                                {
                                    for (int j = 0; j < specialAuthority.Rows.Count; j++)
                                    {
                                        switch (Common.ToInt32(specialAuthority.Rows[j]["specialAuthorityId"]))
                                        {
                                            case (int)VASpecialAuthority.SHOP_SHOPIMAGEREVELATION://门店环境图片权限
                                                authorityStr.Append("shopimagerevelation|");
                                                break;
                                            case (int)VASpecialAuthority.SHOP_SHOPSUNDRYMANAGE://杂项
                                                authorityStr.Append("shopsundrymanage|");
                                                break;
                                            //case (int)VASpecialAuthority.SHOP_VIPDISCOUNT://折扣管理shopvipdiscount
                                            //    authorityStr.Append("ShopVipDicount|");
                                            //    break;
                                            case (int)VASpecialAuthority.SHOP_VIPDISCOUNT://折扣管理
                                                authorityStr.Append("shopvipdiscount|");
                                                break;
                                            default:
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "ShopManage/ShopHandle.aspx"://门店审核权限
                                authorityStr.Append("shophandle|");
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            authorityStr.Append("withdrawtype|");
            authorityStr.Append("commision|");
            authorityStr.Append("companyAccount|");
            return authorityStr.ToString().Trim().TrimEnd('|');
        }
        /// <summary>
        /// 获取收银宝商户板块用户权限
        /// </summary>
        /// <returns></returns>
        private static string GetMerchantUserAuthority(int employeeId, bool isViewAllocWorker, EmployeeOperate employeeOperate)
        {
            string authorityStr = "";
            if (isViewAllocWorker)
            {
                return "accountTotal|currentSellOff|dishList|preOrderShopVerified|preOrderShopConfirmed|configurationPassword|isSupportShopManagePage|increment";
            }
            int shopId = Common.ToInt32(HttpContext.Current.Session["loginshop"]);
            SybEmployeeShopAuthority operate = new SybEmployeeShopAuthority();
            string[] authorityNames = operate.GetEmployeeInShopAuthorityNames(shopId, employeeId, 1);
            if (authorityNames.Length > 0)
            {
                for (int i = 0; i < authorityNames.Length; i++)
                {
                    switch (authorityNames[i])
                    {
                        case "账户明细":
                            authorityStr += "accountTotal|";
                            break;
                        case "沽清管理":
                            authorityStr += "currentSellOff|";
                            break;
                        case "菜品管理":
                            authorityStr += "dishList|";
                            break;
                        case "财务对账":
                            authorityStr += "preOrderShopVerified|";
                            break;
                        case "入座管理":
                            authorityStr += "preOrderShopConfirmed|";
                            break;
                        case "增值管理":
                            authorityStr += "increment|";
                            break;
                        default:
                            break;
                    }
                }
            }
            authorityStr += "configurationPassword|";
            bool isSupportShopManagePage = isViewAllocWorker == true ? true
                                            : (operate.QuerySybAuthority(employeeId, shopId, "店员管理") == true ? true
                                             : false);//查询是否具备店员管理页面权限
            if (isSupportShopManagePage)
            {
                authorityStr += "isSupportShopManagePage|";
            }
            return authorityStr.Trim().TrimEnd('|');//权限字符串或者空字符串
        }
        /// <summary>
        /// 收银宝判断某个用户是否具备某个页面权限
        /// </summary>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public static bool IsHaveQuanxian(SybAuthorityEnum enumValue)
        {
            if (HttpContext.Current.Session["MerchantsTreasureUserInfo"] == null)
            {
                return false;
            }
            VAEmployeeLoginResponse sessionInfo = (VAEmployeeLoginResponse)HttpContext.Current.Session["MerchantsTreasureUserInfo"];
            string userName = sessionInfo.userName;
            int employeeId = sessionInfo.employeeID;
            EmployeeOperate operate = new EmployeeOperate();
            bool result = false;
            switch (enumValue)
            {
                case SybAuthorityEnum.公司管理:
                    result = operate.CheckEmployeeAuthortiy(userName, "CompanyManage/CompanyManage.aspx");
                    break;
                case SybAuthorityEnum.公司添加:
                    result = operate.CheckEmployeeAuthortiy(userName, "CompanyManage/CompanyAdd.aspx");
                    break;
                case SybAuthorityEnum.门店管理:
                    result = operate.CheckEmployeeAuthortiy(userName, "ShopManage/ShopManage.aspx");
                    break;
                case SybAuthorityEnum.门店添加:
                    result = operate.CheckEmployeeAuthortiy(userName, "ShopManage/ShopAdd.aspx");
                    break;
                case SybAuthorityEnum.门店VIP配置:
                    result = QuerySpecialAuthority(employeeId, VASpecialAuthority.SHOP_VIPDISCOUNT);
                    break;
                case SybAuthorityEnum.门店杂项配置:
                    result = QuerySpecialAuthority(employeeId, VASpecialAuthority.SHOP_SHOPSUNDRYMANAGE);
                    break;
                case SybAuthorityEnum.门店环境图配置:
                    result = QuerySpecialAuthority(employeeId, VASpecialAuthority.SHOP_SHOPIMAGEREVELATION);
                    break;
                case SybAuthorityEnum.门店审核:
                    result = operate.CheckEmployeeAuthortiy(userName, "ShopManage/ShopHandle.aspx");
                    break;
                default:
                    break;
            }
            return result;
        }
        static bool QuerySpecialAuthority(int employeeId, VASpecialAuthority enumValue)
        {
            bool result = false;
            RoleOperate roleOperate = new RoleOperate();
            DataTable specialAuthority = roleOperate.QuerySpecialAuthorityInfoByEmployeeID(employeeId, 0);
            if (specialAuthority.Rows.Count > 0)
            {
                for (int j = 0; j < specialAuthority.Rows.Count; j++)
                {
                    if (Common.ToInt32(specialAuthority.Rows[j]["specialAuthorityId"]) == (int)enumValue)
                    {
                        result = true;
                        break;
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// 添加公司，修改公司信息
        /// </summary>
        /// <param name="companyInfoJson"></param>
        /// <returns></returns>
        public static string CompanyOperate(string companyInfoJson)
        {
            SybMsg resultMessage = new SybMsg();
            try
            {
                if (string.IsNullOrEmpty(companyInfoJson))
                {
                    resultMessage.Insert(-2, "填写信息有误");
                    return resultMessage.Value;
                }
                companyInfoJson = companyInfoJson.Replace("'", "\\\\'");
                MerchantCompanyInfo mCompanyInfo = JsonOperate.JsonDeserialize<MerchantCompanyInfo>(companyInfoJson);//反序列化页面json参数
                if (mCompanyInfo == null)
                {
                    resultMessage.Insert(-2, "填写信息有误");
                    return resultMessage.Value;
                }
                mCompanyInfo.companyName = mCompanyInfo.companyName.Replace("\\'", "'");
                if (string.IsNullOrWhiteSpace(mCompanyInfo.ownedCompany))
                {
                    resultMessage.Insert(-99, "品牌名称不能为空");
                    return resultMessage.Value;
                }
                if (string.IsNullOrWhiteSpace(mCompanyInfo.companyName))
                {
                    resultMessage.Insert(-100, "公司名称不能为空");
                    return resultMessage.Value;
                }
                using (TransactionScope scope = new TransactionScope())
                {
                    CompanyOperate companyOperate = new CompanyOperate();
                    CompanyInfo companyInfo = new CompanyInfo();
                    if (mCompanyInfo.companyID <= 0)
                    {
                        if (!IsHaveQuanxian(SybAuthorityEnum.公司添加))
                        {
                            resultMessage.Insert(-999, "无操作权限");
                            return resultMessage.Value;
                        }
                        if (!companyOperate.CompanyNameValid(mCompanyInfo.companyName))
                        {
                            resultMessage.Insert(-98, "该公司名称已存在");
                            return resultMessage.Value;
                        }
                        #region 添加公司
                        //TODO：此处对象错位赋值好像没有什么意义
                        //string imagePath = WebConfig.OssDomain + WebConfig.ImagePath;//@"../" + ConfigurationManager.AppSettings["ImagePath"].ToString();
                        companyInfo.companyAddress = mCompanyInfo.companyAddress.Replace(" ", "");
                        companyInfo.companyLogo = mCompanyInfo.companyLogo;
                        companyInfo.companyName = mCompanyInfo.companyName.Replace(" ", "");
                        companyInfo.companyStatus = mCompanyInfo.companyStatus;//公司状态
                        companyInfo.companyTelePhone = mCompanyInfo.companyTelePhone.Replace(" ", "");
                        companyInfo.contactPerson = mCompanyInfo.contactPerson.Replace(" ", "");
                        companyInfo.contactPhone = mCompanyInfo.contactPhone.Replace(" ", "");
                        companyInfo.companyDescription = mCompanyInfo.companyDescription.Replace(" ", "");
                        companyInfo.ownedCompany = mCompanyInfo.ownedCompany.Replace(" ", "");//所属公司
                        companyInfo.sinaWeiboName = mCompanyInfo.sinaWeiboName.Replace(" ", "");
                        companyInfo.acpp = mCompanyInfo.acpp;//公司人均
                        companyInfo.qqWeiboName = mCompanyInfo.qqWeiboName.Replace(" ", "");
                        companyInfo.wechatPublicName = mCompanyInfo.wechatPublicName.Replace(" ", "");
                        string companyImagePath = CharacterToPinyin.GetAllPYLetters(Common.ToClearSpecialCharString(companyInfo.companyName)) + DateTime.Now.ToString("yyyyMMddHHmmssfff") + "/";
                        // Common.FolderCreate(HttpContext.Current.Server.MapPath(imagePath + companyImagePath));//生成一个文件夹
                        companyInfo.companyImagePath = companyImagePath;
                        int newCompanyId = companyOperate.Syb_AddCompany(companyInfo);
                        if (newCompanyId > 0)
                        {
                            EmployeeConnShopOperate employeeShopOperate = new EmployeeConnShopOperate();
                            EmployeeConnShop employeeShop = new EmployeeConnShop();
                            employeeShop.shopID = 0;
                            employeeShop.companyID = newCompanyId;
                            VAEmployeeLoginResponse vAEmployeeLoginResponse = ((VAEmployeeLoginResponse)HttpContext.Current.Session["MerchantsTreasureUserInfo"]);//根据session获取用户信息
                            employeeShop.employeeID = vAEmployeeLoginResponse.employeeID;
                            employeeShop.status = 1;
                            if (employeeShopOperate.AddEmployeeShop(employeeShop))
                            {
                                scope.Complete();
                                resultMessage.Insert(1, "添加成功");
                                Common.RecordEmployeeOperateLogBySYB((int)VAEmployeeOperateLogOperatePageType.COMPANYINFO, (int)VAEmployeeOperateLogOperateType.ADD_OPERATE, "收银宝添加公司，" + companyInfoJson);
                            }
                        }
                        else
                        {
                            resultMessage.Insert(-1, "添加失败");
                        }
                        #endregion
                    }
                    else
                    {
                        #region 修改公司
                        CompanyInfo companyInfoTemp = companyOperate.QueryCompany(mCompanyInfo.companyID);
                        if (companyInfoTemp != null)//先判断该公司是否存在
                        {
                            companyInfo.companyID = mCompanyInfo.companyID;
                            companyInfo.companyAddress = mCompanyInfo.companyAddress;
                            companyInfo.companyLogo = mCompanyInfo.companyLogo;
                            companyInfo.companyName = mCompanyInfo.companyName;
                            companyInfo.companyStatus = 1;//状态
                            companyInfo.companyTelePhone = mCompanyInfo.companyTelePhone;
                            companyInfo.contactPerson = mCompanyInfo.contactPerson;
                            companyInfo.contactPhone = mCompanyInfo.contactPhone;
                            companyInfo.companyDescription = mCompanyInfo.companyDescription;
                            companyInfo.sinaWeiboName = mCompanyInfo.sinaWeiboName;
                            companyInfo.acpp = mCompanyInfo.acpp;//公司人均
                            companyInfo.qqWeiboName = mCompanyInfo.qqWeiboName;
                            companyInfo.wechatPublicName = mCompanyInfo.wechatPublicName;
                            companyInfo.ownedCompany = mCompanyInfo.ownedCompany;
                            bool result = companyOperate.ModifyCompany(companyInfo);
                            if (result)
                            {
                                scope.Complete();
                                resultMessage.Insert(1, "修改成功");
                                //操作日志
                                Common.RecordEmployeeOperateLogBySYB((int)VAEmployeeOperateLogOperatePageType.COMPANYINFO, (int)VAEmployeeOperateLogOperateType.UPDATE_OPERATE, "收银宝修改公司，" + companyInfoJson);
                            }
                            else
                            {
                                resultMessage.Insert(-1, "修改失败");
                            }
                        }
                        else
                        {
                            resultMessage.Insert(-3, "该公司编号信息有误");
                        }
                        #endregion
                    }
                }
            }
            catch
            {
                resultMessage.Insert(-1, "操作失败");
            }
            return resultMessage.Value;
        }
        /// <summary>
        /// 获取公司列表信息
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="searchKeyWords"></param>
        /// <returns></returns>
        public static string GetCompany(int pageSize, int pageIndex, string searchKeyWords)
        {
            SybMsg resultMessage = new SybMsg();
            if (HttpContext.Current.Session["MerchantsTreasureUserInfo"] == null)
            {
                resultMessage.Insert(-1000, "未找到用户信息");
                return resultMessage.Value;
            }
            if (!IsHaveQuanxian(SybAuthorityEnum.公司管理))
            {
                resultMessage.Insert(-999, "无操作权限");
                return resultMessage.Value;
            }
            VAEmployeeLoginResponse vAEmployeeLoginResponse = ((VAEmployeeLoginResponse)HttpContext.Current.Session["MerchantsTreasureUserInfo"]);//根据session获取用户信息
            EmployeeConnShopOperate employeeConnShopOperate = new EmployeeConnShopOperate();
            EmployeeOperate employeeOperate = new EmployeeOperate();
            List<VAEmployeeCompany> employeeCompany = new List<VAEmployeeCompany>();
            int employeeID = vAEmployeeLoginResponse.employeeID;
            if (employeeID > 0)
            {
                employeeCompany = employeeConnShopOperate.QueryEmployeeCompany(employeeID, true);//当前是友络员工
                if (employeeCompany != null)
                {
                    if (!String.IsNullOrWhiteSpace(searchKeyWords))
                    {
                        employeeCompany = employeeCompany.Where(p => p.companyName.Contains(searchKeyWords.Replace(" ", ""))).ToList<VAEmployeeCompany>();//模糊查询过滤
                    }
                    string resultJson = PagingOperate<VAEmployeeCompany>.GetPagingListData(pageIndex, pageSize, employeeCompany);//获取分页数据
                    resultMessage.Insert(1, resultJson);//返回公司列表信息
                }
                else
                {
                    string resultJson = PagingOperate<VAEmployeeCompany>.GetPagingListData(pageIndex, pageSize, employeeCompany);//获取分页数据
                    resultMessage.Insert(1, resultJson);//返回公司空列表信息
                }
            }
            else
            {
                resultMessage.Insert(-1, "未找到员工信息");
            }
            return resultMessage.Value;
        }
        /// <summary>
        /// 查询公司基本信息
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public static string GetCompanyDetail(int companyId)
        {
            SybMsg resultmessage = new SybMsg();
            if (companyId <= 0)
            {
                resultmessage.Insert(-1, "未找到公司信息");
                return resultmessage.Value;
            }
            string imagePath = WebConfig.CdnDomain + WebConfig.ImagePath;//@"../" + ConfigurationManager.AppSettings["ImagePath"].ToString();
            CompanyOperate companyOperate = new CompanyOperate();
            CompanyInfo companyInfo = companyOperate.QueryCompany(companyId);
            if (companyInfo == null)
            {
                resultmessage.Insert(-1, "未找到公司信息");
                return resultmessage.Value;
            }
            companyInfo.companyImagePath = imagePath + companyInfo.companyImagePath;
            string strJson = JsonOperate.JsonSerializer<CompanyInfo>(companyInfo);
            resultmessage.Insert(1, strJson);
            return resultmessage.Value;
        }
        /// <summary>
        /// 删除公司信息
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public static string DeleteCompany(int companyId)
        {
            SybMsg resultMessage = new SybMsg();
            if (companyId <= 0)
            {
                resultMessage.Insert(-1, "未找到公司信息");
                return resultMessage.Value;
            }
            CompanyOperate companyOperate = new CompanyOperate();
            CompanyInfo companyInfo = companyOperate.QueryCompany(companyId);//查询判断下当前公司是否在数据库中存在
            if (companyInfo == null)
            {
                resultMessage.Insert(-1, "未找到公司信息");
                return resultMessage.Value;
            }
            ShopOperate shopOper = new ShopOperate();
            DataTable dtShop = shopOper.QueryCompanyShop(companyId);
            if (dtShop.Rows.Count > 0)
            {
                resultMessage.Insert(-3, "该公司旗下有门店，请先删除旗下门店，再执行删除公司操作");
                return resultMessage.Value;
            }
            if (companyOperate.RemoveCompany(companyId))
            {
                resultMessage.Insert(1, "删除成功");
                Common.RecordEmployeeOperateLogBySYB((int)VAEmployeeOperateLogOperatePageType.COMPANYINFO, (int)VAEmployeeOperateLogOperateType.DELETE_OPERATE, "收银宝删除公司，" + companyInfo.companyName);
            }
            else
            {
                resultMessage.Insert(-2, "删除失败");
            }
            return resultMessage.Value;
        }
        /// <summary>
        /// 检测公司名称是否存在
        /// </summary>
        /// <param name="companyName"></param>
        /// <returns></returns>
        public static string CheckCompanyName(string companyName)
        {
            SybMsg resultMessage = new SybMsg();
            CompanyOperate companyOperate = new CompanyOperate();
            if (!companyOperate.CompanyNameValid(companyName.Replace(" ", "")))
            {
                resultMessage.Insert(-1, "公司名称已注册");
                return resultMessage.Value;
            }
            resultMessage.Insert(1, "门店名称未注册");
            return resultMessage.Value;
        }
        /// <summary>
        ///  添加和修改菜谱基本信息前页面数据信息
        /// </summary>
        /// <param name="menuCompanyId"></param>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public static string BeforeMenuOperate(int menuCompanyId, int companyId)
        {
            SybMsg resultMessage = new SybMsg();
            if (HttpContext.Current.Session["MerchantsTreasureUserInfo"] == null)
            {
                resultMessage.Insert(-1000, "未找到用户信息");
                return resultMessage.Value;
            }
            MerchantMenuInfo modelInfo = new MerchantMenuInfo();
            List<LangInfo> langList = new List<LangInfo>();
            int employeeId = ((VAEmployeeLoginResponse)HttpContext.Current.Session["MerchantsTreasureUserInfo"]).employeeID;
            if (employeeId > 0)
            {
                if (menuCompanyId > 0)
                {
                    #region 修改菜谱信息 page load数据返回
                    MenuConnShopOperate menuOpe = new MenuConnShopOperate();
                    MenuManager man = new MenuManager();
                    DataTable dtCompany = menuOpe.QueryMenuConnCompanyByMenuCompanyId(menuCompanyId);
                    if (dtCompany.Rows.Count == 1)
                    {
                        modelInfo.companyId = Common.ToInt32(dtCompany.Rows[0]["companyID"]);
                        modelInfo.companyName = Common.ToString(dtCompany.Rows[0]["companyName"]);
                    }
                    DataTable dtMenu = man.QueryMenu(Common.ToInt32(dtCompany.Rows[0]["menuId"]));
                    Menu menu = new Menu();
                    if (dtMenu.Rows.Count > 0)
                    {
                        menu.menuId = Common.ToInt32(dtCompany.Rows[0]["menuId"]);
                        menu.menuDesc = dtMenu.Rows[0]["MenuDesc"].ToString();
                        menu.menuName = dtMenu.Rows[0]["MenuName"].ToString();
                        LangOperate langOper = new LangOperate();
                        DataTable dtLang = langOper.SearchLang();
                        DataView dv = dtLang.DefaultView;
                        dv.RowFilter = "LangID=" + Common.ToInt32(dtMenu.Rows[0]["LangID"]);
                        if (dv.Count > 0)
                        {
                            string langName = Common.ToString(dv[0]["LangName"]);
                            LangInfo langInfo = new LangInfo()
                            {
                                langId = Common.ToInt32(dtMenu.Rows[0]["LangID"]),
                                langName = langName
                            };
                            langList.Add(langInfo);
                        }
                        modelInfo.menuInfo = menu;
                        modelInfo.langListInfo = langList;
                    }
                    #endregion
                }
                else
                {
                    #region 添加菜谱信息 page load数据返回
                    CompanyOperate companyOper = new CompanyOperate();
                    CompanyInfo cInfo = companyOper.QueryCompany(companyId);
                    modelInfo.companyId = cInfo.companyID;
                    modelInfo.companyName = cInfo.companyName;
                    LangOperate lo = new LangOperate();
                    DataTable dtLang = lo.SearchLang();
                    if (dtLang.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtLang.Rows.Count; i++)
                        {
                            LangInfo langModel = new LangInfo();
                            langModel.langId = Common.ToInt32(dtLang.Rows[i]["LangID"]);
                            langModel.langName = Common.ToString(dtLang.Rows[i]["LangName"]);
                            langList.Add(langModel);
                        }
                    }
                    modelInfo.langListInfo = langList;
                    modelInfo.menuInfo = new Menu();//空对象
                    #endregion
                }
                modelInfo.menuCompanyId = menuCompanyId;
                string returnJson = JsonOperate.JsonSerializer<MerchantMenuInfo>(modelInfo);
                resultMessage.Insert(1, returnJson);
            }
            else
            {
                resultMessage.Insert(-1, "未找到员工信息");
            }
            return resultMessage.Value;
        }
        /// <summary>
        /// 添加和修改菜谱基本信息
        /// </summary>
        /// <param name="menuJson"></param>
        /// <returns></returns>
        public static string MenuOperate(string menuJson)
        {
            SybMsg resultMessage = new SybMsg();
            if (string.IsNullOrEmpty(menuJson))
            {
                resultMessage.Insert(-1, "请求信息有误");
                return resultMessage.Value;
            }
            MerchantMenuInfo merchantMenuInfo = JsonOperate.JsonDeserialize<MerchantMenuInfo>(menuJson.Replace(" ", ""));
            if (merchantMenuInfo == null)
            {
                resultMessage.Insert(-1, "菜谱信息有误");
                return resultMessage.Value;
            }
            if (string.IsNullOrWhiteSpace(merchantMenuInfo.menuInfo.menuName))
            {
                resultMessage.Insert(-100, "菜谱名称不能为空");
                return resultMessage.Value;
            }
            List<LangInfo> langListInfo = merchantMenuInfo.langListInfo;//菜谱语言
            Menu menuInfo = merchantMenuInfo.menuInfo;//菜谱基本信息
            MenuOperate menuOperate = new MenuOperate();
            MenuManager man = new MenuManager();
            VAMenu vAMenu = new VAMenu();
            using (TransactionScope scope = new TransactionScope())
            {
                if (merchantMenuInfo.menuCompanyId > 0)
                {
                    if (merchantMenuInfo.menuCompanyId <= 0)
                    {
                        resultMessage.Insert(-99, "提交数据有误");
                        return resultMessage.Value;
                    }
                    MenuConnShopOperate operate = new MenuConnShopOperate();
                    DataTable dt = operate.QueryMenuConnCompanyByMenuCompanyId(merchantMenuInfo.menuCompanyId);
                    if (dt.Rows.Count != 1)
                    {
                        resultMessage.Insert(-1, "菜谱信息有误");
                        return resultMessage.Value;
                    }
                    #region 修改菜谱信息
                    //修改菜谱信息，只能修改menu名称和描述信息
                    DataTable dtMenu = man.QueryMenu(Common.ToInt32(dt.Rows[0]["menuId"]));
                    if (dtMenu.Rows.Count > 0)
                    {
                        vAMenu.menuDesc = menuInfo.menuDesc;
                        vAMenu.menuName = menuInfo.menuName;
                        vAMenu.langID = langListInfo[0].langId;//多语言，现在只支持一种语言，这个可以直接从数据库查询
                        vAMenu.menuSequence = Common.ToInt32(dtMenu.Rows[0]["MenuSequence"]);
                        vAMenu.menuI18nID = Common.ToInt32(dtMenu.Rows[0]["MenuI18nID"]);
                        vAMenu.menuID = Common.ToInt32(dt.Rows[0]["menuId"]);
                        if (menuOperate.ModifyMenu(vAMenu))
                        {
                            scope.Complete();
                            resultMessage.Insert(1, "修改成功");
                        }
                        else
                        {
                            resultMessage.Insert(-2, "修改失败");
                        }
                    }
                    else
                    {
                        resultMessage.Insert(-3, "未找到菜谱信息");
                    }
                    #endregion
                }
                else
                {
                    ShopOperate shopOper = new ShopOperate();
                    DataTable dtShop = shopOper.QueryCompanyShop(merchantMenuInfo.companyId);
                    if (dtShop.Rows.Count <= 0)//不判断下面文件夹创建不出来，因为默认的图片存储在公司下的第一个默认的门店下
                    {
                        resultMessage.Insert(-5, "请先为当前公司添加门店");
                        return resultMessage.Value;
                    }
                    if (menuOperate.BoolCheckShopMenuName(menuInfo.menuName, merchantMenuInfo.companyId))
                    {
                        resultMessage.Insert(-55, "当前菜谱名称已存在");
                        return resultMessage.Value;
                    }
                    #region 添加菜谱信息
                    vAMenu.menuDesc = menuInfo.menuDesc;
                    vAMenu.menuName = menuInfo.menuName;
                    vAMenu.langID = langListInfo[0].langId;
                    vAMenu.menuSequence = 0;//排序字段
                    vAMenu.menuImagePath = Common.ToString(dtShop.Rows[0]["shopImagePath"]) + CharacterToPinyin.GetAllPYLetters(Common.ToClearSpecialCharString(vAMenu.menuName)) + DateTime.Now.ToString("yyyyMMddHHmmssfff") + "/";
                    MenuConnCompany menuConnCompany = new MenuConnCompany();
                    menuConnCompany.menuId = 0;//逻辑层会赋值
                    menuConnCompany.status = 1;
                    menuConnCompany.companyId = merchantMenuInfo.companyId;
                    menuConnCompany.menuCompanyId = 0;//数据库自增字段
                    if (menuOperate.AddMenuAndMenuCompany(vAMenu, menuConnCompany))
                    {
                        scope.Complete();
                        resultMessage.Insert(1, "添加成功");
                    }
                    else
                    {
                        resultMessage.Insert(-4, "添加失败");
                    }
                    #endregion
                }
            }
            return resultMessage.Value;
        }
        /// <summary>
        /// 删除菜谱信息
        /// </summary>
        /// <param name="menuCompanyId"></param>
        /// <returns></returns>
        public static string DeleteMenu(int menuCompanyId)
        {
            SybMsg resuletMessage = new SybMsg();
            if (menuCompanyId <= 0)
            {
                resuletMessage.Insert(-1, "未找到菜谱信息");
                return resuletMessage.Value;
            }
            MenuConnShopOperate operate = new MenuConnShopOperate();
            DataTable dt = operate.QueryMenuConnCompanyByMenuCompanyId(menuCompanyId);
            if (dt.Rows.Count != 1)
            {
                resuletMessage.Insert(-1, "未找到菜谱信息");
                return resuletMessage.Value;
            }
            int menuId = Common.ToInt32(dt.Rows[0]["menuId"]);
            using (TransactionScope scope = new TransactionScope())
            {
                MenuOperate mo = new MenuOperate();
                bool flag = operate.RemoveMenuConnShop(menuId); //----需要移除门店和菜谱关联关系，可能没有关联门店，暂时不作为判断事物flag
                bool flag1 = operate.RemoveMenuConnCompany(menuId); //----需要移除公司和菜谱的关联关系
                bool flag2 = mo.RemoveMenu(menuId);//----需要移除菜谱信息
                if (flag1 && flag2)
                {
                    scope.Complete();
                    resuletMessage.Insert(1, "删除成功");
                }
                else
                {
                    resuletMessage.Insert(-2, "删除失败");
                }
            }
            return resuletMessage.Value;
        }
        /// <summary>
        /// 获取门店列表信息
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchKeyWords"></param>
        /// <returns></returns>
        public static string GetShopList(int pageIndex, int pageSize, string searchKeyWords)
        {
            SybMsg resultMessage = new SybMsg();
            if (HttpContext.Current.Session["MerchantsTreasureUserInfo"] == null)
            {
                resultMessage.Insert(-1000, "未找到用户信息");
                return resultMessage.Value;
            }
            if (!IsHaveQuanxian(SybAuthorityEnum.门店管理))
            {
                resultMessage.Insert(-999, "无操作权限");
                return resultMessage.Value;
            }
            List<CompanyShopList> companyShopList = new List<CompanyShopList>();
            int employeeID = ((VAEmployeeLoginResponse)HttpContext.Current.Session["MerchantsTreasureUserInfo"]).employeeID;
            if (employeeID <= 0)
            {
                resultMessage.Insert(-1000, "未找到用户信息");
                return resultMessage.Value;
            }
            EmployeeConnShopOperate employeeConnShopOperate = new EmployeeConnShopOperate();
            DataTable dtInfo = employeeConnShopOperate.QueryEmployeeShopByEmplyeeNew(employeeID, searchKeyWords);//当前是友络员工
            //CompanyOperate companyOper = new CompanyOperate();
            //DataTable dtCompany = companyOper.QueryCompany();
            //if (dtInfo.Rows.Count > 0 && dtCompany.Rows.Count > 0)
            //{
            //    foreach (DataRow item in dtInfo.Rows)
            //    {
            //        CompanyShopList companyShopModel = new CompanyShopList();
            //        companyShopModel.shopId = (int)item["shopId"];
            //        companyShopModel.shopName = item["shopName"].ToString();
            //        var company = (from r in dtCompany.AsEnumerable()
            //                       where r.Field<int>("companyID") == (int)item["companyId"]
            //                       select new
            //                       {
            //                           companyId = r.Field<int>("companyId"),
            //                           companyName = r.Field<string>("companyName")
            //                       }).Distinct().FirstOrDefault();

            //        if (company != null)
            //        {
            //            companyShopModel.companyId = company.companyId;
            //            companyShopModel.companyName = company.companyName;
            //            companyShopList.Add(companyShopModel);
            //        }
            //    }
            //    if (!String.IsNullOrWhiteSpace(searchKeyWords))
            //    {
            //        //模糊匹配门店
            //        //方案一：
            //        //companyShopList=companyShopList.Where(p=>p.shopName.Contains("")).ToList();
            //        //方案二：
            //        companyShopList = (from a in companyShopList
            //                           where a.shopName.Contains(searchKeyWords.Replace(" ", ""))
            //                           select a).ToList();
            //    }
            //    if (companyShopList != null & companyShopList.Count > 0)
            //    {
            //        companyShopList = companyShopList.OrderByDescending(n => n.shopId).ToList();
            //    }
            //    string returnJson = PagingOperate<CompanyShopList>.GetPagingListData(pageIndex, pageSize, companyShopList);//获取分页数据
            //    resultMessage.Insert(1, returnJson);
            //}
            //else
            //{
            //    string returnJson = PagingOperate<CompanyShopList>.GetPagingListData(pageIndex, pageSize, companyShopList);//获取分页数据
            //    resultMessage.Insert(1, returnJson);
            //}
            foreach (DataRow item in dtInfo.Rows)
            {
                CompanyShopList model = new CompanyShopList();
                model.shopId = Common.ToInt32(item["shopId"]);
                model.shopName = item["shopName"].ToString();
                model.companyId = Common.ToInt32(item["companyId"]);
                model.companyName = item["companyName"].ToString();
                companyShopList.Add(model);
            }
            string returnJson = PagingOperate<CompanyShopList>.GetPagingListData(pageIndex, pageSize, companyShopList);//获取分页数据
            resultMessage.Insert(1, returnJson);
            return resultMessage.Value;
        }
        /// <summary>
        /// 删除门店信息
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public static string DeleteShop(int shopId)
        {
            SybMsg resultMessage = new SybMsg();
            if (shopId <= 0)
            {
                resultMessage.Insert(-1, "未找到门店信息");
                return resultMessage.Value;
            }
            ShopOperate shopOper = new ShopOperate();
            ShopInfo shopInfo = shopOper.QueryShop(shopId);
            if (shopInfo == null)
            {
                resultMessage.Insert(-1, "未找到门店信息");
                return resultMessage.Value;
            }
            if (shopInfo.isHandle == (int)VAShopHandleStatus.SHOP_Pass)
            {
                resultMessage.Insert(-2, "门店已上线，请先联系管理员下线再删除");
                return resultMessage.Value;
            }
            if (shopInfo.remainMoney > 0.01)
            {
                resultMessage.Insert(-3, "门店余款未结清，请先联系门店负责人结清余款再删除");
                return resultMessage.Value;
            }
            if (SybPreOrderManager.CheckSybShopIsHaveUntreatedOrder(shopId))
            {
                resultMessage.Insert(-4, "门店有未处理（支付未对帐）点单，请先处理再删除");
                return resultMessage.Value;
            }
            if (shopOper.RemoveShop(shopId))
            {
                resultMessage.Insert(1, "删除成功");
            }
            else
            {
                resultMessage.Insert(-3, "删除失败");
            }
            return resultMessage.Value;
        }
        /// <summary>
        /// 添加和修改门店信息前页面加载信息
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public static string BeforeShopOperate(int shopId)
        {
            SybMsg resultMessage = new SybMsg();
            if (HttpContext.Current.Session["MerchantsTreasureUserInfo"] == null)
            {
                resultMessage.Insert(-1000, "未找到用户信息");
                return resultMessage.Value;
            }
            int employeeID = ((VAEmployeeLoginResponse)HttpContext.Current.Session["MerchantsTreasureUserInfo"]).employeeID;
            if (employeeID <= 0)
            {
                resultMessage.Insert(-1000, "未找到用户信息");
                return resultMessage.Value;
            }
            SybShopHandeleDetail initShopInfo = new SybShopHandeleDetail();
            if (shopId > 0)
            {
                #region 修改门店信息
                ShopOperate shopOper = new ShopOperate();
                ShopInfo shopInfo = shopOper.QueryShop(shopId);
                if (shopInfo != null)
                {
                    initShopInfo.accountManager = shopInfo.accountManager.HasValue ? shopInfo.accountManager.Value : 0;
                    initShopInfo.acpp = shopInfo.acpp.HasValue ? shopInfo.acpp.Value : 0;
                    initShopInfo.cityID = shopInfo.cityID;
                    initShopInfo.companyID = shopInfo.companyID;
                    initShopInfo.contactPerson = shopInfo.contactPerson;
                    initShopInfo.contactPhone = shopInfo.contactPhone;
                    initShopInfo.countyID = shopInfo.countyID;
                    initShopInfo.isHandle = shopInfo.isHandle;
                    initShopInfo.isSupportAccountsRound = shopInfo.isSupportAccountsRound.HasValue && shopInfo.isSupportAccountsRound.Value;
                    initShopInfo.isSupportPayment = shopInfo.isSupportPayment;
                    initShopInfo.notPaymentReason = shopInfo.notPaymentReason;
                    initShopInfo.openTimes = shopInfo.openTimes;
                    initShopInfo.orderDishDesc = shopInfo.orderDishDesc;
                    initShopInfo.provinceID = shopInfo.provinceID;
                    string initPath = WebConfig.CdnDomain + WebConfig.ImagePath;
                    initShopInfo.publicityPhotoPath = !String.IsNullOrEmpty(shopInfo.publicityPhotoPath) ? (initPath + shopInfo.publicityPhotoPath + "@320w_106h_50Q") : "";
                    initShopInfo.qqWeiboName = shopInfo.qqWeiboName;
                    initShopInfo.shopAddress = shopInfo.shopAddress;
                    initShopInfo.shopBusinessLicense = shopInfo.shopBusinessLicense;
                    initShopInfo.shopDescription = shopInfo.shopDescription;
                    initShopInfo.shopHygieneLicense = shopInfo.shopHygieneLicense;
                    initShopInfo.shopID = shopInfo.shopID;
                    initShopInfo.shopImagePath = shopInfo.shopImagePath;
                    initShopInfo.shopLogo = (!String.IsNullOrEmpty(shopInfo.shopImagePath) && !String.IsNullOrEmpty(shopInfo.shopLogo)) ? (initPath + shopInfo.shopImagePath + shopInfo.shopLogo + "@136w_136h_50Q") : "";
                    initShopInfo.shopName = shopInfo.shopName;
                    initShopInfo.shopRating = shopInfo.shopRating.HasValue ? shopInfo.shopRating.Value : 0;
                    initShopInfo.shopRegisterTime = shopInfo.shopRegisterTime.HasValue ? shopInfo.shopRegisterTime.Value : new DateTime(1970, 1, 1);
                    initShopInfo.shopStatus = shopInfo.shopStatus;
                    initShopInfo.shopTelephone = shopInfo.shopTelephone;
                    initShopInfo.sinaWeiboName = shopInfo.sinaWeiboName;
                    initShopInfo.wechatPublicName = shopInfo.wechatPublicName;
                    ShopCoordinate shopCoordinateBaidu = shopOper.QueryShopCoordinate(2, shopInfo.shopID);//百度经纬度
                    initShopInfo.latitude = shopCoordinateBaidu.latitude;
                    initShopInfo.longitude = shopCoordinateBaidu.longitude;
                    CompanyOperate companyOperate = new CompanyOperate();
                    CompanyInfo companyInfo = companyOperate.QueryCompany(shopInfo.companyID);
                    initShopInfo.companyName = companyInfo != null ? companyInfo.companyName : "";
                    initShopInfo.menuCompanyId = 0;
                    initShopInfo.accountManagerName = "";
                    EmployeeOperate operate = new EmployeeOperate();
                    if (initShopInfo.accountManager > 0)
                    {
                        EmployeeInfo employeeInfo = operate.QueryEmployee(initShopInfo.accountManager);
                        initShopInfo.accountManagerName = employeeInfo != null ? employeeInfo.EmployeeFirstName : "";
                    }

                    initShopInfo.areaManagerName = string.Empty;
                    initShopInfo.areaManager = 0;
                    if (shopInfo.AreaManager.HasValue && shopInfo.AreaManager > 0)
                    {
                        var areaManager = operate.QueryEmployee(shopInfo.AreaManager.Value);
                        if (areaManager != null && areaManager.EmployeeID > 0)
                        {
                            initShopInfo.areaManager = areaManager.EmployeeID;
                            initShopInfo.areaManagerName = areaManager.EmployeeFirstName == null ? string.Empty : areaManager.EmployeeFirstName;
                        }
                    }

                    SybShopHandeleDetail ssx_name = CityOperate.QueryCountyCityProvinceName(shopInfo.countyID);
                    //固定返回省市县的名称，因为审核页面门店的相关信息是不可操作的
                    initShopInfo.cityName = ssx_name.cityName;
                    initShopInfo.provinceName = ssx_name.provinceName;
                    initShopInfo.countyName = ssx_name.countyName;
                    MenuConnShopOperate menuOper = new MenuConnShopOperate();
                    initShopInfo.menuCompanyId = menuOper.QueryShopCurrectMenuCompanyId(shopId);
                    initShopInfo.bankAccount = shopInfo.bankAccount.HasValue ? Common.ToInt32(shopInfo.bankAccount) : 0;
                    initShopInfo.isTrueRedenvelopePayment = shopInfo.isSupportRedEnvelopePayment;
                }
                else
                {
                    resultMessage.Insert(-2, "未找到门店信息");
                }
                #endregion
            }
            else
            {
                #region 添加门店信息
                #endregion
            }
            string returnJson = JsonOperate.JsonSerializer<SybShopHandeleDetail>(initShopInfo);
            resultMessage.Insert(1, returnJson);
            return resultMessage.Value;
        }
        /// <summary>
        /// 添加和修改门店信息
        /// </summary>
        /// <param name="shopJson"></param>
        /// <returns></returns>
        public static string ShopOperate(string shopJson)
        {
            shopJson = shopJson.Replace("'", "\\\\'");
            SybMsg resultMessage = new SybMsg();
            if (HttpContext.Current.Session["MerchantsTreasureUserInfo"] == null)
            {
                resultMessage.Insert(-1000, "未找到用户信息");
                return resultMessage.Value;
            }
            if (string.IsNullOrEmpty(shopJson))
            {
                resultMessage.Insert(-1, "提交数据有误");
                return resultMessage.Value;
            }
            int employeeID = ((VAEmployeeLoginResponse)HttpContext.Current.Session["MerchantsTreasureUserInfo"]).employeeID;
            if (employeeID <= 0)
            {
                resultMessage.Insert(-1000, "未找到用户信息");
                return resultMessage.Value;
            }
            MerchantShopInfo merchantShopInfo = JsonOperate.JsonDeserialize<MerchantShopInfo>(shopJson);
            if (merchantShopInfo == null)
            {
                resultMessage.Insert(-1, "提交数据有误");
                return resultMessage.Value;
            }
            merchantShopInfo.shopName = merchantShopInfo.shopName.Replace("\\'", "'");
            if (string.IsNullOrWhiteSpace(merchantShopInfo.shopName))
            {
                resultMessage.Insert(-100, "门店名称不能为空");
                return resultMessage.Value;
            }
            ShopOperate shopOperate = new ShopOperate();
            if (merchantShopInfo.shopID <= 0)//新增判断门店名称
            {
                DataTable dtShop = shopOperate.QueryShop();
                DataRow[] drShop = dtShop.Select("shopName='" + merchantShopInfo.shopName + "'");
                if (drShop.Length > 0)
                {
                    resultMessage.Insert(-98, "当前门店名称已注册");
                    return resultMessage.Value;
                }
            }
            if (merchantShopInfo.areaManager <= 0)
            {
                resultMessage.Insert(-1, "区域经理不能为空");
                return resultMessage.Value;
            }
            if (merchantShopInfo.companyID <= 0)
            {
                resultMessage.Insert(-50, "请选择公司");
                return resultMessage.Value;

            }
            if (string.IsNullOrWhiteSpace(merchantShopInfo.shopAddress))
            {
                resultMessage.Insert(-49, "门店地址不能为空");
                return resultMessage.Value;
            }
            if (merchantShopInfo.acpp <= 0)
            {
                resultMessage.Insert(-99, "门店人均必须大于0");
                return resultMessage.Value;
            }
            if (merchantShopInfo.isSupportPayment == false)
            {
                if (string.IsNullOrEmpty(merchantShopInfo.notPaymentReason))
                {
                    resultMessage.Insert(-47, "暂不支付原因不能为空");
                    return resultMessage.Value;
                }
            }
            if (merchantShopInfo.orderDishDesc.Length > 200)
            {
                resultMessage.Insert(-70, "点菜描述文字不能超过200个字符");
                return resultMessage.Value;
            }
            ShopInfo shopInfo = shopOperate.QueryShop(merchantShopInfo.shopID);

            #region ----------------------------------------------------
            // 门店公告修改后，需要添加抽奖变更日志
            if (shopInfo.orderDishDesc != merchantShopInfo.orderDishDesc && !string.IsNullOrEmpty(merchantShopInfo.orderDishDesc))
            {
                ShopAwardVersionOperate operateVersion = new ShopAwardVersionOperate();
                operateVersion.InsertShopAwardVersionAndLog(employeeID, merchantShopInfo.shopID, "修改店铺公告", "老后台", Guid.Empty);
            }
            #endregion
            //记录门店暂停支付记录
            VAEmployeeLoginResponse sessionInfo = (VAEmployeeLoginResponse)HttpContext.Current.Session["MerchantsTreasureUserInfo"];
            IShopStopPaymentLog log = null;
            if (shopInfo.shopID >= 0 && shopInfo.isSupportPayment != merchantShopInfo.isSupportPayment)
            {
                if (merchantShopInfo.isSupportPayment == false)
                {
                    log = new ShopStopPaymentLog()
                    {
                        ShopId = merchantShopInfo.shopID,
                        CreatedBy = sessionInfo.employeeID,
                        LastUpdatedBy = sessionInfo.employeeID,
                        CreateTime = DateTime.Now,
                        LastUpdatedTime = DateTime.Now,
                        Remark = merchantShopInfo.notPaymentReason,
                        StartPaymentTime = null,
                        StopPaymentTime = DateTime.Now,
                        State = 1
                    };
                    ShopStopPaymentLogOperate.Add(log);

                }
                else
                {
                    log =
                        ShopStopPaymentLogOperate.GetFirstByQuery(new ShopStopPaymentLogQueryObject() { ShopId = merchantShopInfo.shopID, State = 1 });
                    if (log != null)
                    {
                        log.LastUpdatedBy = sessionInfo.employeeID;
                        log.LastUpdatedTime = DateTime.Now;
                        log.StartPaymentTime = DateTime.Now;
                        log.State = 2;
                        ShopStopPaymentLogOperate.Update(log);
                    }
                }
            }

            shopInfo.canEatInShop = true;
            shopInfo.canTakeout = true;
            shopInfo.cityID = merchantShopInfo.cityID;
            shopInfo.companyID = merchantShopInfo.companyID;
            shopInfo.contactPerson = merchantShopInfo.contactPerson;
            shopInfo.contactPhone = merchantShopInfo.contactPhone;
            shopInfo.countyID = merchantShopInfo.countyID;
            shopInfo.provinceID = merchantShopInfo.provinceID;
            shopInfo.shopAddress = merchantShopInfo.shopAddress;
            shopInfo.shopBusinessLicense = merchantShopInfo.shopBusinessLicense;
            shopInfo.shopHygieneLicense = merchantShopInfo.shopHygieneLicense;
            shopInfo.shopLogo = merchantShopInfo.shopID <= 0 ? "" : shopInfo.shopLogo;
            shopInfo.shopName = merchantShopInfo.shopName;
            shopInfo.shopStatus = 1;
            shopInfo.shopTelephone = merchantShopInfo.shopTelephone;
            shopInfo.isHandle = (int)VAShopHandleStatus.SHOP_UnHandle;
            shopInfo.shopDescription = merchantShopInfo.shopDescription;
            shopInfo.sinaWeiboName = merchantShopInfo.sinaWeiboName;
            shopInfo.qqWeiboName = merchantShopInfo.qqWeiboName;
            shopInfo.wechatPublicName = merchantShopInfo.wechatPublicName;
            shopInfo.openTimes = merchantShopInfo.openTimes;
            shopInfo.acpp = merchantShopInfo.acpp;
            shopInfo.isSupportAccountsRound = false;
            shopInfo.shopRating = merchantShopInfo.shopRating;
            shopInfo.isSupportPayment = merchantShopInfo.isSupportPayment;
            shopInfo.orderDishDesc = merchantShopInfo.orderDishDesc;
            shopInfo.notPaymentReason = merchantShopInfo.notPaymentReason;
            shopInfo.accountManager = merchantShopInfo.accountManager;
            shopInfo.shopID = merchantShopInfo.shopID;
            //shopInfo.bankAccount = merchantShopInfo.bankAccount;//门店银行帐号关联ID
            shopInfo.isSupportRedEnvelopePayment = merchantShopInfo.isTrueRedenvelopePayment;

            shopInfo.AreaManager = merchantShopInfo.areaManager;

            int shopId = 0;
            using (TransactionScope scope = new TransactionScope())
            {
                List<ShopCoordinate> shopCoordinateList = new List<ShopCoordinate>();
                ShopCoordinate shopCoordinate = new ShopCoordinate();
                //获取店铺的经纬度
                shopCoordinate.latitude = Common.ToDouble11Digit(merchantShopInfo.latitude);
                shopCoordinate.longitude = Common.ToDouble11Digit(merchantShopInfo.longitude);
                shopCoordinate.mapId = 2;
                if (merchantShopInfo.shopID > 0)
                {
                    //ShopInfo oldShopInfo =
                    //shopInfo.shopVerifyTime = oldShopInfo.shopVerifyTime;
                    //shopInfo.shopLogo = oldShopInfo.shopLogo;//暂时无法修改下面三项
                    //shopInfo.shopImagePath = oldShopInfo.shopImagePath;
                    //shopInfo.publicityPhotoPath = oldShopInfo.publicityPhotoPath;
                    //shopInfo.isHandle = oldShopInfo.isHandle;
                    //shopInfo.shopRegisterTime = oldShopInfo.shopRegisterTime;
                    #region 修改门店信息
                    if (shopOperate.ModifyShop(shopInfo))
                    {
                        shopCoordinate.shopId = merchantShopInfo.shopID;
                        shopCoordinateList.Add(shopCoordinate);
                        bool isAddShopMenu = true;
                        if (merchantShopInfo.menuCompanyId > 0)//有菜谱才添加关联关系
                        {
                            isAddShopMenu = AddShopMenu(merchantShopInfo.companyID, merchantShopInfo.shopID, merchantShopInfo.menuCompanyId);
                        }
                        if (shopOperate.UpdateShopCoordinate(shopCoordinateList) && isAddShopMenu)
                        {
                            scope.Complete();
                            resultMessage.Insert(1, "修改成功");
                        }
                        else
                        {
                            resultMessage.Insert(-1, "修改失败");
                        }
                    }
                    else
                    {
                        resultMessage.Insert(-1, "修改失败");
                    }
                    #endregion
                }
                else
                {
                    if (!IsHaveQuanxian(SybAuthorityEnum.门店添加))
                    {
                        resultMessage.Insert(-999, "无操作权限");
                        return resultMessage.Value;
                    }
                    #region 添加门店信息
                    if (merchantShopInfo.companyID <= 0)
                    {
                        resultMessage.Insert(-95, "请选择公司");
                        return resultMessage.Value;
                    }
                    shopInfo.shopRegisterTime = DateTime.Now;
                    shopInfo.shopVerifyTime = DateTime.Now;
                    shopInfo.publicityPhotoPath = "";
                    CompanyOperate companyOperate = new CompanyOperate();
                    CompanyInfo companyInfo = companyOperate.QueryCompany(shopInfo.companyID);
                    if (companyInfo == null)
                    {
                        resultMessage.Insert(-94, "未找到当前选择公司信息");
                        return resultMessage.Value;
                    }
                    string companyImagePath = companyInfo.companyImagePath;
                    shopInfo.shopImagePath = companyImagePath + CharacterToPinyin.GetAllPYLetters(Common.ToClearSpecialCharString(shopInfo.shopName)) + DateTime.Now.ToString("yyyyMMddHHmmssfff") + "/";
                    shopCoordinateList.Add(shopCoordinate);
                    shopId = shopOperate.AddShop(shopInfo, shopCoordinateList);
                    if (shopId > 0)
                    {
                        if (merchantShopInfo.tagId > 0)
                        {
                            AddShopTag(merchantShopInfo.tagId.ToString(), shopId);
                        }
                        //将这个店铺管理权限对应给这个用户
                        EmployeeConnShopOperate employeeShopOperate = new EmployeeConnShopOperate();
                        EmployeeConnShop employeeShop = new EmployeeConnShop();
                        employeeShop.shopID = shopId;
                        employeeShop.status = 1;
                        employeeShop.companyID = shopInfo.companyID;
                        employeeShop.employeeID = employeeID;
                        bool isAddShopMenu = true;
                        if (merchantShopInfo.menuCompanyId > 0)//有菜谱才添加关联关系
                        {
                            isAddShopMenu = AddShopMenu(merchantShopInfo.companyID, merchantShopInfo.shopID, merchantShopInfo.menuCompanyId);
                        }
                        if (employeeShopOperate.AddEmployeeShop(employeeShop) && isAddShopMenu)
                        {
                            scope.Complete();
                            resultMessage.Insert(1, "添加成功");
                        }
                        else
                        {
                            resultMessage.Insert(-1, "添加失败");
                        }
                    }
                    else
                    {
                        resultMessage.Insert(-1, "添加失败");
                    }
                    #endregion
                }
            }


            if (merchantShopInfo.shopID <= 0 && merchantShopInfo.isSupportPayment == false)
            {
                log = new ShopStopPaymentLog()
                {
                    ShopId = shopId,
                    CreatedBy = sessionInfo.employeeID,
                    LastUpdatedBy = sessionInfo.employeeID,
                    CreateTime = DateTime.Now,
                    LastUpdatedTime = DateTime.Now,
                    Remark = merchantShopInfo.notPaymentReason,
                    StartPaymentTime = null,
                    StopPaymentTime = DateTime.Now,
                    State = 1
                };
                ShopStopPaymentLogOperate.Add(log);
            }

            return resultMessage.Value;
        }
        /// <summary>
        /// 检测门店名称是否存在
        /// </summary>
        /// <param name="shopName"></param>
        /// <returns></returns>
        public static string CheckShopName(string shopName)
        {
            SybMsg resultMessage = new SybMsg();
            ShopOperate shopOperate = new ShopOperate();
            DataTable dtShop = shopOperate.QueryShop();
            DataRow[] drShop = dtShop.Select("shopName='" + shopName + "'");
            if (drShop.Length > 0)
            {
                resultMessage.Insert(-1, "门店名称已注册");
            }
            else
            {
                resultMessage.Insert(1, "门店名称未注册");
            }
            return resultMessage.Value;
        }
        /// <summary>
        /// 获取杂项列表信息
        /// </summary>
        /// <param name="shopId"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public static string GetSundryList(int shopId, int pageSize, int pageIndex)
        {
            SybMsg resultMessage = new SybMsg();
            if (shopId <= 0)
            {
                resultMessage.Insert(-1, "参数有误");
                return resultMessage.Value;
            }
            if (!IsHaveQuanxian(SybAuthorityEnum.门店杂项配置))
            {
                resultMessage.Insert(-999, "无操作权限");
                return resultMessage.Value;
            }
            SundryOperate sundryOperate = new SundryOperate();
            DataTable dt = sundryOperate.QuerySundryInfo(shopId);
            List<SybSundryList> list = new List<SybSundryList>();
            if (dt.Rows.Count > 0)
            {
                DataTable dtTemp = dt.Copy();
                if (dtTemp.Select("sundryChargeMode=" + (int)VASundryChargeMode.PROPORTION).Length <= 0)//表示杂项中不包含按比例收费杂项，则在if中添加默认杂项保存数据库在组装数据返回数据库
                {
                    if (sundryOperate.InsertDefaultSundryInfo(shopId) > 0)
                    {
                        dt = sundryOperate.QuerySundryInfo(shopId);//重新查询获取数据，肯定有count
                        GetSybSundryList(dt, list);
                    }
                    else
                    {
                        GetSybSundryList(dt, list);
                    }
                }
                else
                {
                    GetSybSundryList(dt, list);
                }
            }
            else
            {
                if (sundryOperate.InsertDefaultSundryInfo(shopId) > 0)
                {
                    dt = sundryOperate.QuerySundryInfo(shopId);
                    GetSybSundryList(dt, list);
                }
            }
            string returnJson = PagingOperate<SybSundryList>.GetPagingListData(pageIndex, pageSize, list);//获取分页数据
            resultMessage.Insert(1, returnJson);
            return resultMessage.Value;
        }
        /// <summary>
        /// GetSundryList私有辅助方法
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="list"></param>
        private static void GetSybSundryList(DataTable dt, List<SybSundryList> list)
        {
            foreach (DataRow item in dt.Rows)
            {
                SybSundryList model = new SybSundryList();
                model.sundryId = Common.ToInt32(item["sundryId"]);
                model.status = Common.ToInt32(item["status"]);
                model.sundryName = Common.ToString(item["sundryName"]);
                list.Add(model);
            }
        }
        /// <summary>
        /// 查询公司所有菜谱信息
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pagingFlag">标记是否分页，公司查看列表需要分页，门店分配菜谱不需要分页</param>
        /// <returns></returns>
        public static string GetCompanyMenuList(int companyId, int pageSize, int pageIndex, bool pagingFlag)
        {
            SybMsg resultMessage = new SybMsg();
            if (companyId <= 0)
            {
                resultMessage.Insert(-1, "请求数据有误");
                return resultMessage.Value;
            }
            MenuConnShopOperate menuConnShopOperate = new MenuConnShopOperate();
            DataTable dt = menuConnShopOperate.QueryMenuConnCompany(companyId);
            List<MenuConnCompanyInfoExtension> list = new List<MenuConnCompanyInfoExtension>();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {
                    MenuConnCompanyInfoExtension model = new MenuConnCompanyInfoExtension();
                    model.menuDes = Common.ToString(item["MenuDesc"]);
                    model.menuName = Common.ToString(item["MenuName"]);
                    model.menuCompanyId = Common.ToInt32(item["menuCompanyId"]);
                    model.menuId = Common.ToInt32(item["menuId"]);
                    model.menuVersion = Common.ToInt32(item["MenuVersion"]);//当前菜谱版本号
                    list.Add(model);
                }
            }
            string returnJson = string.Empty;
            if (pagingFlag)
            {
                returnJson = PagingOperate<MenuConnCompanyInfoExtension>.GetPagingListData(pageIndex, pageSize, list);//获取分页数据
            }
            else
            {
                returnJson = JsonOperate.JsonSerializer<List<MenuConnCompanyInfoExtension>>(list);//不处理分页
            }
            resultMessage.Insert(1, returnJson);
            return resultMessage.Value;
        }
        /// <summary>
        /// 门店添加菜谱关联
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="shopId"></param>
        /// <param name="menuCompanyId"></param>
        /// <returns></returns>
        static bool AddShopMenu(int companyId, int shopId, int menuCompanyId)
        {
            SybMsg resultMessage = new SybMsg();
            if (companyId > 0 && shopId > 0 && menuCompanyId > 0)
            {
                MenuConnShopOperate menuConnShopOperate = new MenuConnShopOperate();
                menuConnShopOperate.RemoveMenuConnShopByShopId(shopId);//删除当前门店的菜谱关联信息，备注：一个门店能有一个菜谱
                MenuConnShop menuConnShop = new MenuConnShop();
                MenuConnShopOperate operate = new MenuConnShopOperate();
                DataTable dt = operate.QueryMenuConnCompanyByMenuCompanyId(menuCompanyId);
                if (dt.Rows.Count != 1)
                {
                    return false;
                }
                menuConnShop.menuId = Common.ToInt32(dt.Rows[0]["menuId"]);//最新选中的菜谱
                menuConnShop.shopId = shopId;//当前操作门店
                menuConnShop.companyId = companyId;//当前操作公司
                if (menuConnShopOperate.AddMenuShop(menuConnShop) > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 开启或关闭杂项
        /// </summary>
        /// <returns></returns>
        public static string OperateSundry(long sundryId, int status)
        {
            SybMsg resultMessage = new SybMsg();
            SundryOperate sundryOperate = new SundryOperate();
            DataTable dtSundry = sundryOperate.QuerySundryInfoBySundryId(sundryId);
            if (dtSundry.Rows.Count > 0)
            {
                //int dbStatus = Common.ToInt32(dtSundry.Rows[0]["status"]);//数据库当期状态
                // dbStatus = dbStatus == (int)VASundry.OPENED ? (int)VASundry.CLOSED : (int)VASundry.OPENED;
                if (status == (int)VASundry.CLOSED || status == (int)VASundry.OPENED)
                {
                    if (status == (int)VASundry.OPENED)
                    {
                        if (sundryOperate.UpdateSundryStatus(sundryId, (int)VASundry.CLOSED))//数据层反向判断，这里也反向传递数据
                        {
                            resultMessage.Insert(1, "开启成功");
                        }
                        else
                        {
                            resultMessage.Insert(-3, "开启失败");
                        }
                    }
                    else
                    {
                        if (sundryOperate.UpdateSundryStatus(sundryId, (int)VASundry.OPENED))
                        {
                            resultMessage.Insert(1, "关闭成功");
                        }
                        else
                        {
                            resultMessage.Insert(1, "关闭失败");
                        }
                    }
                }
                else
                {
                    resultMessage.Insert(-1, "状态信息有误");
                }
            }
            else
            {
                resultMessage.Insert(-2, "未找到杂项信息");
            }
            return resultMessage.Value;
        }
        /// <summary>
        /// 修改杂项信息页面加载页面信息
        /// </summary>
        /// <param name="sundryId"></param>
        /// <returns></returns>
        public static string BeforeSundryOperate(long sundryId)
        {
            SybMsg resultMessage = new SybMsg();
            if (sundryId <= 0)
            {
                resultMessage.Insert(-1, "参数有误");
                return resultMessage.Value;
            }
            SundryOperate sundryOperate = new SundryOperate();
            DataTable dt = sundryOperate.QuerySundryInfoBySundryId(sundryId);
            if (dt.Rows.Count == 1)
            {
                SybSundry sybSundry = new SybSundry();
                sybSundry.description = Common.ToString(dt.Rows[0]["description"]);
                sybSundry.price = Common.ToDouble(dt.Rows[0]["price"]);
                sybSundry.required = Common.ToBool(dt.Rows[0]["required"]);
                sybSundry.shopId = Common.ToInt32(dt.Rows[0]["shopId"]);
                sybSundry.status = Common.ToInt32(dt.Rows[0]["status"]);
                sybSundry.sundryChargeMode = Common.ToInt32(dt.Rows[0]["sundryChargeMode"]);
                sybSundry.sundryId = Common.ToInt32(dt.Rows[0]["sundryId"]);
                sybSundry.sundryName = Common.ToString(dt.Rows[0]["sundryName"]);
                sybSundry.sundryStandard = Common.ToString(dt.Rows[0]["sundryStandard"]);
                string returnJson = JsonOperate.JsonSerializer<SybSundry>(sybSundry);
                resultMessage.Insert(1, returnJson);
            }
            else
            {
                resultMessage.Insert(-2, "未找到杂项信息");
            }
            return resultMessage.Value;
        }
        /// <summary>
        /// 添加，修改杂项信息保存数据
        /// </summary>
        /// <param name="sundryJson"></param>
        /// <returns></returns>
        public static string SundryOperate(string sundryJson)
        {
            SybMsg resultMessage = new SybMsg();
            if (string.IsNullOrEmpty(sundryJson))
            {
                resultMessage.Insert(-1, "保存信息有误");
                return resultMessage.Value;
            }
            SybSundry sybSundry = JsonOperate.JsonDeserialize<SybSundry>(sundryJson.Replace(" ", ""));
            if (sybSundry == null || sybSundry.shopId <= 0)
            {
                resultMessage.Insert(-1, "提交有误");
                return resultMessage.Value;
            }
            if (string.IsNullOrWhiteSpace(sybSundry.sundryName))
            {
                resultMessage.Insert(-99, "杂项名称不能为空");
                return resultMessage.Value;
            }
            if (string.IsNullOrWhiteSpace(sybSundry.sundryStandard))
            {
                resultMessage.Insert(-98, "杂项规格不能为空");
                return resultMessage.Value;
            }
            if (sybSundry.price <= 0)
            {
                resultMessage.Insert(-100, "额度不能为0");
                return resultMessage.Value;
            }
            SundryOperate sundryOperate = new SundryOperate();
            SundryInfo sundryInfo = new SundryInfo();
            sundryInfo.shopId = sybSundry.shopId;
            sundryInfo.sundryName = sybSundry.sundryName;
            sundryInfo.sundryChargeMode = sybSundry.sundryChargeMode;
            sundryInfo.sundryStandard = sybSundry.sundryStandard.ToString();
            sundryInfo.price = sybSundry.price;
            sundryInfo.description = sybSundry.description;
            sundryInfo.required = sybSundry.required;
            sundryInfo.vipDiscountable = false;
            sundryInfo.backDiscountable = false;
            if (sybSundry.sundryId > 0)
            {
                #region 修改杂项信息
                sundryInfo.status = sybSundry.status;
                if (sundryOperate.UpdateSundayInfo(sybSundry.sundryId, sundryInfo) > 0)
                {
                    resultMessage.Insert(1, "修改成功");
                }
                else
                {
                    resultMessage.Insert(-2, "修改失败");
                }
                #endregion
            }
            else
            {
                if (sundryOperate.CheckSundryName(sybSundry.sundryName, sybSundry.shopId))//检测杂项名称是否已存在，针对门店不能重复
                {
                    resultMessage.Insert(-97, "当前门店存在" + sybSundry.sundryName + "杂项信息");
                    return resultMessage.Value;
                }
                #region 添加杂项信息
                sundryInfo.status = (int)VASundry.OPENED;//默认状态开启
                if (sundryOperate.InsertSundryInfo(sundryInfo) > 0)
                {
                    resultMessage.Insert(1, "添加成功");
                }
                else
                {
                    resultMessage.Insert(1, "添加失败");
                }
                #endregion
            }
            return resultMessage.Value;
        }

        /// <summary>
        /// 门店vip列表信息
        /// </summary>
        /// <param name="shopId"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static string GetShopVip(int shopId, int pageIndex, int pageSize)
        {
            SybMsg resultMessage = new SybMsg();
            if (shopId <= 0)
            {
                resultMessage.Insert(-1, "参数有误");
                return resultMessage.Value;
            }
            if (!IsHaveQuanxian(SybAuthorityEnum.门店VIP配置))
            {
                resultMessage.Insert(-999, "无操作权限");
                return resultMessage.Value;
            }
            ShopOperate _ShopO = new ShopOperate();
            DataTable dtList = _ShopO.GetShopVipInfoAndViewAllocPlatformVipInfo(shopId);
            List<SybShopVipInfo> list = new List<SybShopVipInfo>();
            if (dtList.Rows.Count > 0)
            {
                foreach (DataRow dr in dtList.Rows)
                {
                    SybShopVipInfo shopVip = new SybShopVipInfo();
                    shopVip.id = Common.ToInt32(dr["id"]);
                    shopVip.name = Common.ToString(dr["name"]);
                    shopVip.discount = Common.ToDouble(dr["discount"]);
                    shopVip.platformVipName = Common.ToString(dr["platformVipName"]);
                    list.Add(shopVip);
                }
            }
            string returnJson = PagingOperate<SybShopVipInfo>.GetPagingListData(pageIndex, pageSize, list);//获取分页数据
            resultMessage.Insert(1, returnJson);
            return resultMessage.Value;
        }
        /// <summary>
        /// 添加和修改门店vip等级页面初始化信息
        /// </summary>
        /// <param name="shopId"></param>
        /// <param name="shopVipId"></param>
        /// <returns></returns>
        public static string BeforeShopVipOperate(int shopId, int shopVipId)
        {
            SybMsg resultMessage = new SybMsg();
            if (shopId <= 0)
            {
                resultMessage.Insert(-1, "参数有误");
                return resultMessage.Value;
            }
            SybVip sybVip = new SybVip();
            SybShopVip shopVip = new SybShopVip();
            List<ViewAllocVip> vaVip = new List<ViewAllocVip>();
            VipOperate vipOper = new VipOperate();
            DataTable dt = vipOper.QueryViewAllocPlatformVipInfo();
            foreach (DataRow item in dt.Rows)
            {
                ViewAllocVip vip = new ViewAllocVip();
                vip.id = Common.ToInt32(item["id"]);
                vip.name = Common.ToString(item["name"]);
                vaVip.Add(vip);
            }
            if (shopVipId > 0)
            {
                ShopOperate _ShopO = new ShopOperate();
                DataTable dtList = _ShopO.GetShopVipInfo(shopId);
                DataRow[] dr = dtList.Select("id=" + shopVipId);
                if (dr.Length == 1)
                {
                    shopVip.id = Common.ToInt32(dr[0]["id"]);
                    shopVip.discount = Common.ToDouble(dr[0]["discount"]);
                    shopVip.name = Common.ToString(dr[0]["name"]);
                    shopVip.platformVipId = Common.ToInt32(dr[0]["platformVipId"]);
                    shopVip.shopId = Common.ToInt32(dr[0]["shopId"]);
                    shopVip.status = Common.ToInt32(dr[0]["status"]);
                }
                else
                {
                    resultMessage.Insert(-2, "未找到vip等级信息");
                    return resultMessage.Value;
                }
            }
            sybVip.vaVip = vaVip;
            sybVip.shopVip = shopVip;
            string returnJson = JsonOperate.JsonSerializer<SybVip>(sybVip);
            resultMessage.Insert(1, returnJson);
            return resultMessage.Value;
        }
        /// <summary>
        /// 删除门店vip等级信息
        /// </summary>
        /// <param name="shopVipId"></param>
        /// <returns></returns>
        public static string DeleteShopVip(int shopVipId)
        {
            SybMsg resultMessage = new SybMsg();
            if (shopVipId <= 0)
            {
                resultMessage.Insert(-1, "参数有误");
                return resultMessage.Value;
            }
            ShopOperate _ShopO = new ShopOperate();
            object[] result = _ShopO.DeleteShopVipInfo(shopVipId);
            if ((int)result[0] > 0)
            {
                resultMessage.Insert(1, "删除成功");
            }
            else
            {
                resultMessage.Insert(-2, "删除失败");
            }
            return resultMessage.Value;
        }
        /// <summary>
        /// 添加和修改门店vip等级信息
        /// </summary>
        /// <param name="shopVipJson"></param>
        /// <returns></returns>
        public static string ShopVipOperate(string shopVipJson)
        {
            SybMsg resultMessage = new SybMsg();
            if (string.IsNullOrEmpty(shopVipJson))
            {
                resultMessage.Insert(-1, "参数有误");
                return resultMessage.Value;
            }
            SybShopVip sybShopVip = JsonOperate.JsonDeserialize<SybShopVip>(shopVipJson.Replace(" ", ""));
            if (sybShopVip == null)
            {
                resultMessage.Insert(-1, "参数有误");
                return resultMessage.Value;
            }
            if (string.IsNullOrWhiteSpace(sybShopVip.name))
            {
                resultMessage.Insert(-100, "vip等级名称不能为空");
                return resultMessage.Value;
            }
            if (sybShopVip.discount <= 0)
            {
                resultMessage.Insert(-99, "vip折扣不能小于0");
                return resultMessage.Value;
            }
            int platformVipId = sybShopVip.platformVipId;
            int shopId = sybShopVip.shopId;
            string name = sybShopVip.name;
            if (sybShopVip.discount >= 1)
            {
                resultMessage.Insert(-98, "vip折扣不能大于1");
                return resultMessage.Value;
            }
            //double discount = Math.Round(Common.ToDouble(sybShopVip.discount) / 100, 2);
            double discount = Common.ToDouble(sybShopVip.discount);
            ShopOperate _ShopO = new ShopOperate();
            if (sybShopVip.id > 0)
            {
                object[] result = new object[] { false, "" };
                result = _ShopO.UpdateShopVipInfo(sybShopVip.id, name, platformVipId, discount);
                if ((bool)result[0])
                {
                    resultMessage.Insert(1, "修改成功");
                }
                else
                {
                    resultMessage.Insert(-2, "修改失败");
                }
            }
            else
            {
                int insertIesult = _ShopO.InsertShopVipInfo(platformVipId, name, shopId, discount);
                if (insertIesult > 0)
                {
                    resultMessage.Insert(1, "添加成功");
                }
                else
                {
                    resultMessage.Insert(-3, "添加失败");
                }
            }
            return resultMessage.Value;
        }
        /// <summary>
        /// 获取显示门店环境图片的地址
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public static string GetShopRevelationImage(int shopId)
        {
            SybMsg resultMessage = new SybMsg();
            if (shopId <= 0)
            {
                resultMessage.Insert(-1, "参数有误");
                return resultMessage.Value;
            }
            if (!IsHaveQuanxian(SybAuthorityEnum.门店环境图配置))
            {
                resultMessage.Insert(-999, "无操作权限");
                return resultMessage.Value;
            }
            ShopOperate shopOperate = new ShopOperate();
            ShopInfo shopInfo = shopOperate.QueryShop(shopId);
            if (shopInfo == null)
            {
                resultMessage.Insert(-2, "未找到当前门店信息");
                return resultMessage.Value;
            }
            DataRow[] dr = shopOperate.QueryShopRevealImageInfo(shopId).Select("status = 1");//查询有效的环境图片
            List<ShopRevealImageInfo> list = new List<ShopRevealImageInfo>();
            if (dr.Length > 0)
            {
                string imagePath = WebConfig.CdnDomain + WebConfig.ImagePath + shopInfo.shopImagePath + WebConfig.ShopImg;
                foreach (DataRow item in dr)
                {
                    ShopRevealImageInfo model = new ShopRevealImageInfo()
                    {
                        id = Common.ToInt64(item["id"]),//图片编号
                        imgUrl = imagePath + Common.ToString(item["revealImageName"]) + "@320w_106h_50Q"
                    };
                    list.Add(model);
                }
            }
            string resultJson = JsonOperate.JsonSerializer<List<ShopRevealImageInfo>>(list);
            resultMessage.Insert(1, resultJson);
            return resultMessage.Value;
        }
        /// <summary>
        /// 保存上传门店环境图片
        /// </summary>
        /// <param name="shopId"></param>
        /// <param name="fs"></param>
        /// <returns></returns>
        public static string SaveShopRevelationImage(int shopId, Stream fs)
        {
            SybMsg resultMessage = new SybMsg();
            if (shopId <= 0 || fs == null)
            {
                resultMessage.Insert(-1, "请求参数有误");
                return resultMessage.Value;
            }
            string extension = "";
            //开始处理流文件，从fs开始处理
            using (Image original_image = Image.FromStream(fs))
            {
                //结束处理流文件，现在是在fs的末尾
                if (original_image.Width < 625 || original_image.Height < 489)
                {
                    resultMessage.Insert(-8, "图片的宽度至少为625，高度至少为489");
                    return resultMessage.Value;
                }
                if (Math.Floor(Common.ToDouble(original_image.Width * 3 / 4)) != original_image.Height)
                {
                    resultMessage.Insert(-7, "图片的宽度高比必须为4:3");
                    return resultMessage.Value;
                }
                if (!CheckImageExtension(original_image, ref extension))
                {
                    resultMessage.Insert(-6, "图片格式必须为jpg，png");
                    return resultMessage.Value;
                }
            }
            if (fs.Length > WebConfig.ShopEnvironmentSpace * 1024)//kb=千字节=1024Byte字节
            {
                resultMessage.Insert(-5, "图片大小不能超过500KB");
                return resultMessage.Value;
            }
            fs.Seek(0, SeekOrigin.Begin);//将流文件的处理位置初始化带流文件的开头
            ShopOperate shopOperate = new ShopOperate();
            ShopInfo shopInfo = shopOperate.QueryShop(shopId);
            string imgName = shopId + "_" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + "." + extension;//图片名称
            string imgPath = WebConfig.ImagePath + shopInfo.shopImagePath + WebConfig.ShopImg + imgName;//图片上传地址
            DataTable dt = shopOperate.QueryShopRevealImageInfo(shopId);
            if (dt.Rows.Count < 4)
            {
                //=>上传图片
                CloudStorage.AliyunOpenStorageService service = new CloudStorage.AliyunOpenStorageService();
                CloudStorage.AliyunOSSResult result = service.PutObject(imgPath, fs);
                if (result.code)
                {
                    //=>保存数据库
                    int returnId = shopOperate.InsertShopRevealImage(shopId, imgName, 1);
                    if (returnId > 0)
                    {
                        SybShopRevealImage model = new SybShopRevealImage()
                        {
                            count = dt.Rows.Count + 1,
                            imgUrl = WebConfig.CdnDomain + imgPath + "@320w_106h_50Q",
                            id = returnId
                        };
                        //原交互方式，序列化json回给前端，前端针对此类方法（上传图片成功后返回当前图片信息）无法解析json格式，所以定义下面的交互方式
                        // resultMessage.Insert(1, JsonOperate.JsonSerializer<SybShopRevealImage>(model));
                        /* resultMessage.Insert(1, "id,1325|imgUrl,http://image220.u-xian.com/UploadFiles/Images/shangdaokafei20121130151227110/shangdaokafei20121130151326223/ShopImage/34_20140701214101510.jpg@320w_106h_50Q|count,4"); */
                        resultMessage.Insert(1, "id," + model.id + "|imgUrl," + model.imgUrl + "|count," + model.count);
                    }
                    else
                    {
                        resultMessage.Insert(-2, "保存数据失败");
                    }
                }
                else
                {
                    resultMessage.Insert(-3, "上传失败");
                }
            }
            else
            {
                resultMessage.Insert(-4, "最多只能上传4张");
            }
            return resultMessage.Value;
        }
        /// <summary>
        /// 删除门店环境图片信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public static string DeleteShopRevelationImage(long id, int shopId)
        {
            //数据库逻辑层正常处理，阿里图片删除不在事物控制范围（做删除操作，不做标记位判断）
            SybMsg resultMessage = new SybMsg();
            if (id <= 0 && shopId <= 0)
            {
                resultMessage.Insert(-1, "请求参数有误");
                return resultMessage.Value;
            }
            ShopOperate shopOperate = new ShopOperate();
            DataRow[] drShopRevealImageInfo = shopOperate.QueryShopRevealImageInfo(shopId).Select("id = " + id);//查询有效的环境图片
            if (drShopRevealImageInfo.Length != 1)
            {
                resultMessage.Insert(-1, "请求参数有误");
                return resultMessage.Value;
            }
            bool result = shopOperate.DeleteShopRevealImage(id);
            if (result == false)
            {
                resultMessage.Insert(-1, "删除失败");
            }
            else
            {
                ShopInfo shopInfo = shopOperate.QueryShop(shopId);
                string imgName = Common.ToString(drShopRevealImageInfo[0]["revealImageName"]);
                string saveImagePath = WebConfig.ImagePath + shopInfo.shopImagePath + WebConfig.ShopImg + imgName;
                CloudStorageResult deleteResult = CloudStorageOperate.DeleteObject(saveImagePath);//阿里删除图片操作，上面是图片的路径
                resultMessage.Insert(1, "删除成功");
            }
            return resultMessage.Value;
        }
        /// <summary>
        /// 上传保存门店LOGO图片
        /// </summary>
        /// <param name="shopId"></param>
        /// <param name="fs"></param>
        /// <returns></returns>
        public static string SaveShopLogoImage(int shopId, Stream fs)
        {
            SybMsg resultMessage = new SybMsg();
            if (shopId <= 0 || fs == null)
            {
                resultMessage.Insert(-1, "请求参数有误");
                return resultMessage.Value;
            }
            string extension = "";
            using (Image original_image = Image.FromStream(fs))
            {
                if (original_image.Width < 300 || original_image.Height < 300)
                {
                    resultMessage.Insert(-7, "图片的宽度至少为300，高度至少为300");
                    return resultMessage.Value;
                }
                if (original_image.Width != original_image.Height)
                {
                    resultMessage.Insert(-6, "图片的宽度高比必须为1:1");
                    return resultMessage.Value;
                }
                if (!CheckImageExtension(original_image, ref extension))
                {
                    resultMessage.Insert(-5, "图片格式必须为jpg，png");
                    return resultMessage.Value;
                }
            }
            if (fs.Length > WebConfig.ShopLogoSpace * 1024)
            {
                resultMessage.Insert(-4, "图片大小不能超过300KB");
                return resultMessage.Value;
            }
            fs.Seek(0, SeekOrigin.Begin);
            ShopOperate shopOperate = new ShopOperate();
            ShopInfo shopInfo = shopOperate.QueryShop(shopId);
            string imgName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + "." + extension;//图片名称
            string imgPath = WebConfig.ImagePath + shopInfo.shopImagePath + imgName;//图片上传地址
            CloudStorage.AliyunOpenStorageService service = new CloudStorage.AliyunOpenStorageService();
            CloudStorage.AliyunOSSResult result = service.PutObject(imgPath, fs);
            if (result.code)
            {
                if (!string.IsNullOrEmpty(shopInfo.shopLogo))
                {
                    service.DeleteObject(WebConfig.ImagePath + shopInfo.shopImagePath + shopInfo.shopLogo);//删除原有的图片
                }
                shopInfo.shopLogo = imgName;
                if (shopOperate.ModifyShop(shopInfo))
                {
                    //resultMessage.Insert(1, WebConfig.CdnDomain + imgPath + "@136w_136h_50Q");//返回图片路径
                    resultMessage.Insert(1, "imgUrl," + WebConfig.CdnDomain + imgPath + "@136w_136h_50Q");
                }
                else
                {
                    resultMessage.Insert(-2, "保存数据失败");
                }
            }
            else
            {
                resultMessage.Insert(-3, "上传失败");
            }
            return resultMessage.Value;
        }
        /// <summary>
        /// 上传保存门店背景图片
        /// </summary>
        /// <param name="shopId"></param>
        /// <param name="fs"></param>
        /// <returns></returns>
        public static string SaveShopPublicityPhotoImage(int shopId, Stream fs)
        {
            SybMsg resultMessage = new SybMsg();
            if (shopId <= 0 || fs == null)
            {
                resultMessage.Insert(-1, "请求参数有误");
                return resultMessage.Value;
            }
            string extension = "";
            using (Image original_image = Image.FromStream(fs))
            {
                if (original_image.Width < 1440 || original_image.Height < 677)
                {
                    resultMessage.Insert(-6, "图片的宽度至少为1440，高度至少为677");
                    return resultMessage.Value;
                }
                if (Math.Floor(Common.ToDouble(original_image.Width * 677 / 1440)) != original_image.Height)
                {
                    resultMessage.Insert(-7, "图片的宽度高比必须为1440:677");
                    return resultMessage.Value;
                }
                if (!CheckImageExtension(original_image, ref extension))
                {
                    resultMessage.Insert(-5, "图片格式必须为jpg，png");
                    return resultMessage.Value;
                }
            }
            if (fs.Length > WebConfig.ShopFaceSpace * 1024)
            {
                resultMessage.Insert(-4, "图片大小不能超过0.5M");
                return resultMessage.Value;
            }
            fs.Seek(0, SeekOrigin.Begin);
            ShopOperate shopOperate = new ShopOperate();
            ShopInfo shopInfo = shopOperate.QueryShop(shopId);
            string imgName = shopInfo.shopImagePath + "shopPublicityPhoto/" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + "." + extension;//图片名称
            string imgPath = WebConfig.ImagePath + imgName;//图片上传地址
            CloudStorage.AliyunOpenStorageService service = new CloudStorage.AliyunOpenStorageService();
            CloudStorage.AliyunOSSResult result = service.PutObject(imgPath, fs);
            if (result.code)
            {
                if (!string.IsNullOrEmpty(shopInfo.publicityPhotoPath))
                {
                    service.DeleteObject(WebConfig.ImagePath + shopInfo.publicityPhotoPath);//删除原有的图片
                }
                shopInfo.publicityPhotoPath = imgName;
                if (shopOperate.ModifyShop(shopInfo))
                {
                    //resultMessage.Insert(1, WebConfig.CdnDomain + imgPath + "@320w_106h_50Q");//返回图片路径
                    resultMessage.Insert(1, "imgUrl," + WebConfig.CdnDomain + imgPath + "@320w_150h_50Q");
                }
                else
                {
                    resultMessage.Insert(-2, "保存数据失败");
                }
            }
            else
            {
                resultMessage.Insert(-3, "上传失败");
            }
            return resultMessage.Value;
        }
        /// <summary>
        /// 查询门店审核列表数据
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchKeyWords"></param>
        /// <returns></returns>
        public static string GetShopHandleList(int pageIndex, int pageSize, string searchKeyWords)
        {
            SybMsg message = new SybMsg();
            if (!IsHaveQuanxian(SybAuthorityEnum.门店审核))
            {
                message.Insert(-999, "无操作权限");
                return message.Value;
            }
            ShopOperate shopOper = new ShopOperate();
            List<ShopHandleListInfo> list = shopOper.SybQueryShopHandleList();
            if (!String.IsNullOrWhiteSpace(searchKeyWords))
            {
                list = list.Where(q => q.shopName.Contains(searchKeyWords.Replace(" ", ""))).ToList();
            }
            string returnJson = PagingOperate<ShopHandleListInfo>.GetPagingListData(pageIndex, pageSize, list);//获取分页数据
            message.Insert(1, returnJson);
            return message.Value;
        }
        /// <summary>
        /// 门店审核页面加载数据
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public static string ShopHandleBefore(int shopId)
        {
            SybMsg message = new SybMsg();
            if (shopId <= 0)
            {
                message.Insert(-1, "未找到当前门店信息");
                return message.Value;
            }
            SybShopHandeleDetail initShopInfo = new SybShopHandeleDetail();
            ShopOperate shopOper = new ShopOperate();
            ShopInfo shopInfo = shopOper.QueryShop(shopId);
            if (shopInfo != null)
            {
                //id  待处理
                initShopInfo.accountManager = shopInfo.accountManager.HasValue ? shopInfo.accountManager.Value : 0;
                initShopInfo.acpp = shopInfo.acpp.HasValue ? shopInfo.acpp.Value : 0;
                initShopInfo.cityID = shopInfo.cityID;
                initShopInfo.companyID = shopInfo.companyID;
                initShopInfo.contactPerson = shopInfo.contactPerson;
                initShopInfo.contactPhone = shopInfo.contactPhone;
                initShopInfo.countyID = shopInfo.countyID;
                initShopInfo.isHandle = shopInfo.isHandle;//数据结构前端可以写死，绑定下拉表单，根据服务器传值显示对应的item
                initShopInfo.isSupportAccountsRound = shopInfo.isSupportAccountsRound.HasValue && shopInfo.isSupportAccountsRound.Value;
                initShopInfo.isSupportPayment = shopInfo.isSupportPayment;
                initShopInfo.notPaymentReason = shopInfo.notPaymentReason;
                initShopInfo.openTimes = shopInfo.openTimes;
                initShopInfo.orderDishDesc = shopInfo.orderDishDesc;
                initShopInfo.provinceID = shopInfo.provinceID;
                string initPath = WebConfig.CdnDomain + WebConfig.ImagePath;
                initShopInfo.publicityPhotoPath = !String.IsNullOrEmpty(shopInfo.publicityPhotoPath) ? (initPath + shopInfo.publicityPhotoPath + "@320w_106h_50Q") : "";//门店背景图片访问地址
                initShopInfo.qqWeiboName = shopInfo.qqWeiboName;
                initShopInfo.shopAddress = shopInfo.shopAddress;
                initShopInfo.shopBusinessLicense = shopInfo.shopBusinessLicense;
                initShopInfo.shopDescription = shopInfo.shopDescription;
                initShopInfo.shopHygieneLicense = shopInfo.shopHygieneLicense;
                initShopInfo.shopID = shopInfo.shopID;
                initShopInfo.shopImagePath = shopInfo.shopImagePath;//没有用
                initShopInfo.shopLogo = (!String.IsNullOrEmpty(shopInfo.shopImagePath) && !String.IsNullOrEmpty(shopInfo.shopLogo)) ? (initPath + shopInfo.shopImagePath + shopInfo.shopLogo + "@136w_136h_50Q") : "";//门店LOGO图片访问地址
                initShopInfo.shopName = shopInfo.shopName;
                initShopInfo.shopRating = shopInfo.shopRating.HasValue ? shopInfo.shopRating.Value : 0;
                initShopInfo.shopRegisterTime = shopInfo.shopRegisterTime.HasValue ? shopInfo.shopRegisterTime.Value : new DateTime(1970, 1, 1);
                initShopInfo.shopStatus = shopInfo.shopStatus;
                initShopInfo.shopTelephone = shopInfo.shopTelephone;
                initShopInfo.sinaWeiboName = shopInfo.sinaWeiboName;
                initShopInfo.wechatPublicName = shopInfo.wechatPublicName;
                ShopCoordinate shopCoordinateBaidu = shopOper.QueryShopCoordinate(2, shopInfo.shopID);//百度经纬度
                initShopInfo.latitude = shopCoordinateBaidu.latitude;
                initShopInfo.longitude = shopCoordinateBaidu.longitude;
                CompanyOperate companyOperate = new CompanyOperate();//可以获取当前门店的公司名称
                CompanyInfo companyInfo = companyOperate.QueryCompany(shopInfo.companyID);
                initShopInfo.companyName = companyInfo != null ? companyInfo.companyName : "";
                SybShopHandeleDetail ssx_name = CityOperate.QueryCountyCityProvinceName(shopInfo.countyID);
                //固定返回省市县的名称，因为审核页面门店的相关信息是不可操作的
                initShopInfo.cityName = ssx_name.cityName;
                initShopInfo.provinceName = ssx_name.provinceName;
                initShopInfo.countyName = ssx_name.countyName;
                if (initShopInfo.accountManager > 0)
                {
                    EmployeeOperate employeeOper = new EmployeeOperate();
                    EmployeeInfo employeeInfo = employeeOper.QueryEmployee(initShopInfo.accountManager);
                    initShopInfo.accountManagerName = employeeInfo == null ? "" : employeeInfo.EmployeeFirstName;
                }
                else
                {
                    initShopInfo.accountManagerName = "";
                }
                message.Insert(1, JsonOperate.JsonSerializer<SybShopHandeleDetail>(initShopInfo));
            }
            else
            {
                message.Insert(-2, "未找到门店信息");
            }
            return message.Value;
        }
        /// <summary>
        /// 审核上线门店操作
        /// </summary>
        /// <param name="shopId"></param>
        /// <param name="handleStatus"></param>
        /// <returns></returns>
        public static string ShopHandleOperate(int shopId, int handleStatus)
        {
            SybMsg message = new SybMsg();
            int[] initHandleStatus = new int[] { (int)VAShopHandleStatus.SHOP_Pass, (int)VAShopHandleStatus.SHOP_UnHandle, (int)VAShopHandleStatus.SHOP_UnPass };
            if (!initHandleStatus.Contains(handleStatus))
            {
                message.Insert(-100, "提交数据有误");
                return message.Value;
            }
            if (shopId <= 0)
            {
                message.Insert(-1, "未找到当前门店信息");
                return message.Value;
            }
            ShopOperate shopOperate = new ShopOperate();
            ShopInfo shopInfo = shopOperate.QueryShop(shopId);
            if (shopInfo == null)
            {
                message.Insert(-1, "未找到当前门店信息");
                return message.Value;
            }
            if (shopOperate.ModifyShopIsHandle(shopId, handleStatus))
            {
                message.Insert(1, "操作成功");
                string statusStr = "";
                switch (handleStatus)
                {
                    case (int)VAShopHandleStatus.SHOP_Pass:
                        statusStr = "审核通过";
                        break;
                    case (int)VAShopHandleStatus.SHOP_UnHandle:
                        statusStr = "未审核";
                        break;
                    case (int)VAShopHandleStatus.SHOP_UnPass:
                        statusStr = "未审核通过";
                        break;
                    default:
                        break;
                }
                //审核日志
                Common.RecordEmployeeOperateLog((int)VAEmployeeOperateLogOperatePageType.SHOP_HANDLE, (int)VAEmployeeOperateLogOperateType.UPDATE_OPERATE, "门店名称：" + shopInfo.shopName + "，审核状态：" + statusStr);
                int employeeId = ((VAEmployeeLoginResponse)HttpContext.Current.Session["MerchantsTreasureUserInfo"]).employeeID;
                ShopHandleLogOperate.InsertShopHandleLog(handleStatus, shopId, shopInfo.shopName, shopInfo.cityID, employeeId);
            }
            else
            {
                message.Insert(-1, "操作失败");
            }
            return message.Value;
        }
        /// <summary>
        /// 门店审核搜索全部公司
        /// </summary>
        /// <param name="searchKeyWords"></param>
        /// <returns></returns>
        public static string QueryAllShop(string searchKeyWords)
        {
            SybMsg message = new SybMsg();
            if (!String.IsNullOrWhiteSpace(searchKeyWords))
            {
                ShopOperate shopOper = new ShopOperate();
                DataTable dtShop = shopOper.QueryShop();
                DataRow[] dr = dtShop.Select("shopName like '%" + Common.ToClearSpecialCharString(searchKeyWords.Replace(" ", "")) + "%'");
                List<Shop> list = new List<Shop>();
                if (dr.Length > 0)
                {
                    foreach (DataRow item in dr)
                    {
                        Shop model = new Shop();
                        model.shopId = Common.ToInt32(item["shopID"]);
                        model.shopName = Common.ToString(item["shopName"]);
                        list.Add(model);
                    }
                }
                message.Insert(1, JsonOperate.JsonSerializer<List<Shop>>(list));
            }
            else
            {
                message.Insert(1, "");
            }
            return message.Value;
        }
        /// <summary>
        /// 公司银行帐号列表
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static string GetCompanyBankAcount(int companyId, int pageIndex, int pageSize)
        {
            SybMsg message = new SybMsg();
            if (companyId <= 0)
            {
                message.Insert(-1, "未找到当前公司信息");
                return message.Value;
            }
            CompanyAccountOprate oprate = new CompanyAccountOprate();
            List<CompanyAccountInfo> list = oprate.QueryAccountByListCompanyId(companyId);
            string resultJson = PagingOperate<CompanyAccountInfo>.GetPagingListData(pageIndex, pageSize, list);//获取分页数据
            message.Insert(1, resultJson);
            return message.Value;
        }
        /// <summary>
        /// 删除银行帐号
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        public static string DeleteCompanyBankAccount(int accountId)
        {
            SybMsg message = new SybMsg();
            if (accountId <= 0)
            {
                message.Insert(-1, "未找到当前银行帐号信息");
                return message.Value;
            }
            CompanyAccountManager companyAccountManager = new CompanyAccountManager();
            if (companyAccountManager.DeleteAccountInfo(accountId))
            {
                message.Insert(1, "删除银行帐号成功");
            }
            else
            {
                message.Insert(1, "删除银行帐号失败");
            }
            return message.Value;
        }
        /// <summary>
        /// 新增和修改银行帐号信息
        /// </summary>
        /// <param name="accountJson"></param>
        /// <returns></returns>
        public static string CompanyBankAccountOperate(string accountJson)
        {
            SybMsg message = new SybMsg();
            CompanyAccountInfo model = JsonOperate.JsonDeserialize<CompanyAccountInfo>(accountJson.Replace(" ", "").Replace("\n", ""));
            if (model == null)
            {
                message.Insert(-2, "提交数据有误");
                return message.Value;
            }
            if (model.companyId <= 0)
            {
                message.Insert(-2, "未找到当前公司信息");
                return message.Value;
            }
            if (String.IsNullOrWhiteSpace(model.accountName))
            {
                message.Insert(-100, "开户名不能为空");
                return message.Value;
            }
            if (String.IsNullOrWhiteSpace(model.accountNum))
            {
                message.Insert(-99, "账户号码不能为空");
                return message.Value;
            }
            if (model.accountNum.Length > 21 || model.accountNum.Length < 9)
            {
                message.Insert(-89, "账户号码位数错误");
                return message.Value;
            }
            if (String.IsNullOrWhiteSpace(model.bankName))
            {
                message.Insert(-98, "开户银行不能为空");
                return message.Value;
            }
            if (String.IsNullOrWhiteSpace(model.payeeBankName))
            {
                message.Insert(-98, "开户银行分行不能为空");
                return message.Value;
            }

            CompanyAccountOprate operate = new CompanyAccountOprate();
            if (model.identity_Id > 0)//修改
            {
                DataTable dtAccount = operate.QueryAccountById(model.identity_Id);
                if (dtAccount.Rows.Count == 1)
                {
                    if (operate.ModifyAccountInfo(model, dtAccount))
                    {
                        message.Insert(1, "修改成功");
                    }
                    else
                    {
                        message.Insert(-1, "修改失败");
                    }
                }
                else
                {
                    message.Insert(-3, "当前银行编号信息未找到");
                }
            }
            else//添加
            {
                if (operate.QueryNewAccount(model) > 0)
                {
                    message.Insert(1, "新增成功");
                }
                else
                {
                    message.Insert(-1, "新增失败");
                }
            }
            return message.Value;
        }
        /// <summary>
        /// 修改公司银行帐号初始化信息
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        public static string GetCompanyBankAccount(int accountId)
        {
            SybMsg message = new SybMsg();
            if (accountId <= 0)
            {
                message.Insert(-1, "未找到当前账户信息");
                return message.Value;
            }
            CompanyAccountOprate operate = new CompanyAccountOprate();
            DataTable dtAccount = operate.QueryAccountById(accountId);
            if (dtAccount.Rows.Count == 1)
            {
                CompanyAccountInfo info = new CompanyAccountInfo()
                {
                    accountName = Common.ToString(dtAccount.Rows[0]["accountName"]),
                    accountNum = Common.ToString(dtAccount.Rows[0]["accountNum"]),
                    bankName = Common.ToString(dtAccount.Rows[0]["bankName"]),
                    payeeBankName = Common.ToString(dtAccount.Rows[0]["payeeBankName"]),
                    companyId = Common.ToInt32(dtAccount.Rows[0]["companyId"]),
                    identity_Id = accountId,
                    remark = Common.ToString(dtAccount.Rows[0]["remark"]),
                    status = 1
                };
                message.Insert(1, JsonOperate.JsonSerializer<CompanyAccountInfo>(info));
            }
            else
            {
                message.Insert(-2, "未找到任何信息");
            }
            return message.Value;
        }
        /// <summary>
        /// 加载公司佣金信息
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public static string GetCommissionInfo(int companyId)
        {
            SybMsg message = new SybMsg();
            if (companyId <= 0)
            {
                message.Insert(-1, "提交数据有误");
                return message.Value;
            }
            CompanyInfo companyInfo = new CompanyInfo();
            CompanyOperate companyOperate = new CompanyOperate();
            VACommission model = new VACommission();
            companyInfo = companyOperate.QueryCompany(companyId);
            if (companyInfo != null)
            {
                model.viewallocCommissionType = (int)companyInfo.viewallocCommissionType == 0 ? (int)VACommissionType.Normal_Value : (int)companyInfo.viewallocCommissionType;
                model.freeRefundHour = companyInfo.freeRefundHour;
                model.viewallocCommissionValue = companyInfo.viewallocCommissionValue;
                model.companyId = companyId;
                model.companyName = companyInfo.companyName;
                message.Insert(1, JsonOperate.JsonSerializer<VACommission>(model));
            }
            return message.Value;
        }
        /// <summary>
        /// 保存公司佣金信息
        /// </summary>
        /// <param name="commissionJson"></param>
        /// <returns></returns>
        public static string CommissionOperate(string commissionJson)
        {
            SybMsg message = new SybMsg();
            VACommission model = JsonOperate.JsonDeserialize<VACommission>(commissionJson);
            if (model == null)
            {
                message.Insert(-1, "提交数据有误");
                return message.Value;
            }
            if (model.companyId <= 0)
            {
                message.Insert(-1, "提交数据有误");
                return message.Value;
            }
            CompanyInfo companyInfo = new CompanyInfo();
            companyInfo.companyID = model.companyId;
            companyInfo.viewallocCommissionType = (VACommissionType)model.viewallocCommissionType;
            if (companyInfo.viewallocCommissionType == VACommissionType.Proportion)
            {
                if (model.viewallocCommissionValue < 0 || model.viewallocCommissionValue >= 1)
                {
                    message.Insert(-10, "请输入0-1(不包含1)间的小数值(仅支持两位)");
                    return message.Value;
                }
            }
            companyInfo.viewallocCommissionValue = model.viewallocCommissionValue;
            companyInfo.freeRefundHour = model.freeRefundHour;
            CompanyOperate companyOperate = new CompanyOperate();
            if (companyOperate.ModifyCompanyCommissionAndRefundHour(companyInfo))
            {
                message.Insert(1, "保存成功");
            }
            else
            {
                message.Insert(-1, "保存失败");
            }
            return message.Value;
        }
        /// <summary>
        /// 初始化加载二维码操作页面
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public static string BeforeQrcodeOperate(int shopId)
        {
            SybMsg message = new SybMsg();
            if (shopId <= 0)
            {
                message.Insert(-1, "未找到当前门店");
                return message.Value;
            }
            QRCodeOperate _QRCode = new QRCodeOperate();
            DataTable dtQRCode = _QRCode.QueryQRCodeShopType();
            string qrcodePath = WebConfig.CdnDomain;
            List<QRCodeInfo> list = new List<QRCodeInfo>();
            if (dtQRCode.Rows.Count > 0)
            {
                List<QRCodeConnShop> QRCodeConnShops = _QRCode.QueryQRByShopId(shopId);
                for (int i = 0; i < dtQRCode.Rows.Count; i++)
                {
                    QRCodeInfo model = new QRCodeInfo();
                    model.typeId = Common.ToInt32(dtQRCode.Rows[i]["id"]);
                    model.typeName = Common.ToString(dtQRCode.Rows[i]["name"]);
                    model.imgUrl = "";
                    foreach (QRCodeConnShop qr in QRCodeConnShops)
                    {
                        if (model.typeId == qr.typeId)
                        {
                            model.imgUrl = qrcodePath + qr.QRCodeImage.Replace("../", "");
                            break;
                        }
                    }
                    list.Add(model);
                }
                message.Insert(1, JsonOperate.JsonSerializer<List<QRCodeInfo>>(list));
            }
            else
            {
                message.Insert(-2, "数据库信息有误");
            }
            return message.Value;
        }
        /// <summary>
        /// 生成二维码图片
        /// </summary>
        /// <param name="typeId"></param>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public static string QrcodeOperate(string typeId, int shopId)
        {
            string resultUrl = string.Empty;
            SybMsg message = new SybMsg();
            if (Common.ToInt32(typeId) <= 0 || shopId <= 0)
            {
                message.Insert(-3, "请求参数有误");
                return message.Value;
            }
            ShopOperate shopOperate = new ShopOperate();
            ShopInfo shopInfo = shopOperate.QueryShop(shopId);
            string oldQrcodeName = "";
            if (shopInfo != null)
            {
                QRCodeOperate _QRCode = new QRCodeOperate();
                List<QRCodeConnShop> QRCodeConnShops = _QRCode.QueryQRByShopId(shopId).Where(p => p.typeId == Common.ToInt32(typeId)).ToList();//查询当前分类下门店老门店二维码的名称
                if (QRCodeConnShops != null && QRCodeConnShops.Count == 1)
                {
                    oldQrcodeName = QRCodeConnShops[0].imageName;
                }
                string sourcePath = @"../../" + WebConfig.ImagePath + "icon.png";//悠先标志路径
                // string sourcePath = WebConfig.CdnDomain + WebConfig.ImagePath + "icon.png";//悠先标志路径
                string imageName = typeId + "-" + shopId + "-" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".png";//二维码名称
                Common.FolderCreate(HttpContext.Current.Server.MapPath(WebConfig.Temp));
                string imagePath = WebConfig.Temp + imageName;//生成的二维码暂时存放在UploadFiles/temp/中，等上传到阿里云后删除
                string objectKey = WebConfig.ImagePath + shopInfo.shopImagePath + WebConfig.QRCodeImage + imageName;//路径+名称
                object[] createResult = _QRCode.CreateQRCode(typeId, shopId, HttpContext.Current.Server.MapPath(imagePath), HttpContext.Current.Server.MapPath(sourcePath));//生成二维码
                if (Common.ToBool(createResult[0]))
                {
                    CloudStorageResult UploadResult = CloudStorageOperate.PutObject(objectKey, HttpContext.Current.Server.MapPath(imagePath));
                    if (UploadResult.code)
                    {
                        System.IO.File.Delete(HttpContext.Current.Server.MapPath(imagePath));//删除temp文件夹中暂存的二维码
                        resultUrl = WebConfig.CdnDomain + objectKey;
                        bool result = _QRCode.SaveQRCodeConnShop(new QRCodeConnShop()
                        {
                            shopId = shopId,
                            typeId = Common.ToInt32(typeId),
                            QRCodeImage = objectKey,
                            status = 1,
                            imageName = imageName
                        });//保存该店铺的二维码信息
                        if (result)
                        {
                            File.Delete(HttpContext.Current.Server.MapPath(imagePath));//删除temp文件夹中暂存的二维码
                            message.Insert(1, resultUrl);
                            if (!String.IsNullOrEmpty(oldQrcodeName))
                            {
                                CloudStorageOperate.DeleteObject(WebConfig.ImagePath + shopInfo.shopImagePath + WebConfig.QRCodeImage + oldQrcodeName);//删除云空间上旧二维码图片
                            }
                        }
                        else
                        {
                            message.Insert(-1, "二维码生成失败");
                        }
                    }
                    else
                    {
                        message.Insert(-1, "二维码生成失败");
                    }
                }
                else
                {
                    message.Insert(-1, "二维码生成失败");
                }
            }
            else
            {
                message.Insert(-2, "未找到当前门店信息");
            }
            return message.Value;
        }
        /// <summary>
        /// 下载二维码
        /// </summary>
        /// <param name="path"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string QrcodeDownload(string path)
        {
            SybMsg message = new SybMsg();
            path = path.Replace(WebConfig.CdnDomain, "");
            CloudStorageObject cloudStorageObject = CloudStorageOperate.GetObject(path);
            if (cloudStorageObject.Key != null)
            {
                WebClient wc = new WebClient();
                byte[] imgData = wc.DownloadData(WebConfig.CdnDomain + cloudStorageObject.Key);
                HttpContext.Current.Response.ContentType = "application/octet-stream";
                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;FileName=" + HttpUtility.UrlEncode(cloudStorageObject.Key));
                HttpContext.Current.Response.BinaryWrite(imgData);
                message.Insert(1, "下载二维码成功");

            }
            else
            {
                message.Insert(-1, "下载二维码失败");
            }
            return message.Value;
        }
        /// <summary>
        /// 查询客户经理列表信息
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetViewAllocEmployee(string str)
        {
            SybMsg message = new SybMsg();
            List<PartEmployee> list = new List<PartEmployee>();
            if (!string.IsNullOrWhiteSpace(str))
            {
                EmployeeOperate operate = new EmployeeOperate();
                list = operate.GetPartEmployeeInfo(Common.ToClearSpecialCharString(str));
            }
            message.Insert(1, JsonOperate.JsonSerializer<List<PartEmployee>>(list));
            return message.Value;
        }
        /// <summary>
        /// 门店添加搜索公司
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string QueryAllCompany(string str)
        {
            SybMsg message = new SybMsg();
            CompanyOperate companyOperate = new CompanyOperate();
            DataTable dtCompany = companyOperate.QueryCompany();
            DataRow[] drCompany = companyOperate.QueryCompany().Select("companyName like '%" + Common.ToClearSpecialCharString(str) + "%'");
            List<VAEmployeeCompany> companyList = new List<VAEmployeeCompany>();
            if (drCompany.Length > 0)
            {
                foreach (DataRow item in drCompany)
                {
                    companyList.Add(new VAEmployeeCompany()
                    {
                        companyID = Common.ToInt32(item["companyID"]),
                        companyName = Common.ToString(item["companyName"])
                    });
                }
            }
            message.Insert(1, JsonOperate.JsonSerializer<List<VAEmployeeCompany>>(companyList));
            return message.Value;
        }
        /// <summary>
        /// 根据门店地址和城市名称获取门店经纬度坐标
        /// </summary>
        /// <param name="shopDetailAddress"></param>
        /// <param name="cityName"></param>
        /// <returns></returns>
        public static string QueryBaiduCoordinates(string shopDetailAddress, string cityName)
        {
            SybMsg message = new SybMsg();
            MapLocation baiduMapLocation = Common.GetBaiduMapCoordinate(shopDetailAddress, cityName);
            if (baiduMapLocation != null)
            {
                message.Insert(1, JsonOperate.JsonSerializer<MapLocation>(baiduMapLocation));
            }
            else
            {
                message.Insert(-1, "获取经纬度失败");
            }
            return message.Value;
        }
        public static string SearchCompanyShop(int searchType, string searchKeyWords)
        {
            SybMsg message = new SybMsg();
            switch (searchType)
            {
                case 1://搜索公司

                    break;
                case 2://搜索门店

                    break;
                default:
                    break;
            }
            return message.Value;
        }
        /// <summary>
        /// 获取省市县名称和编号
        /// </summary>
        /// <param name="type"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string GetProvinceCityCountry(string type, int id)
        {
            SybMsg message = new SybMsg();
            switch (type)
            {
                case "province"://查询省列表
                    //id无效，无用
                    ProvinceOperate provinceOperate = new ProvinceOperate();//省
                    DataTable dtProvince = provinceOperate.QueryProvince();
                    List<SybProvince> sybProvince = new List<SybProvince>();
                    if (dtProvince.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtProvince.Rows.Count; i++)
                        {
                            sybProvince.Add(new SybProvince()
                            {
                                provinceId = Common.ToInt32(dtProvince.Rows[i]["ProvinceID"]),//省ID
                                provinceName = Common.ToString(dtProvince.Rows[i]["ProvinceName"])//省名称
                            });
                        }
                    }
                    message.Insert(1, JsonOperate.JsonSerializer<List<SybProvince>>(sybProvince));
                    break;
                case "city":
                    //id表示省id
                    CityOperate cityOperate = new CityOperate();//市
                    DataTable dtCity = cityOperate.QueryCity(id);
                    List<SybCity> city = new List<SybCity>();
                    if (dtCity.Rows.Count > 0)
                    {
                        for (int j = 0; j < dtCity.Rows.Count; j++)
                        {
                            city.Add(new SybCity()
                            {
                                cityId = Common.ToInt32(dtCity.Rows[j]["CityID"]),//市ID
                                cityName = Common.ToString(dtCity.Rows[j]["CityName"])//市名称
                            });
                        }
                    }
                    message.Insert(1, JsonOperate.JsonSerializer<List<SybCity>>(city));
                    break;
                case "country":
                    //id表示市id
                    CountyOperate countyOperate = new CountyOperate();//区
                    DataTable dtCountry = countyOperate.QueryCounty(id);
                    List<SybCounty> country = new List<SybCounty>();
                    if (dtCountry.Rows.Count > 0)
                    {
                        for (int n = 0; n < dtCountry.Rows.Count; n++)
                        {
                            country.Add(new SybCounty()
                            {
                                countryId = Common.ToInt32(dtCountry.Rows[n]["CountyID"]),//区ID
                                countryName = Common.ToString(dtCountry.Rows[n]["CountyName"])//区名称
                            });
                        }
                    }
                    message.Insert(1, JsonOperate.JsonSerializer<List<SybCounty>>(country));
                    break;
                default:
                    message.Insert(-1, "请求数据有误");
                    break;
            }
            return message.Value;
        }
        /// <summary>
        /// 查询门店银行帐号列表
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public static string GetShopAccountList(int companyId)
        {
            SybMsg message = new SybMsg();
            CompanyAccountOprate operate = new CompanyAccountOprate();
            DataTable dtAccount = operate.QueryAccountNameAndAccountNumByCompanyId(companyId);
            List<SybShopBankAccount> list = new List<SybShopBankAccount>();
            if (dtAccount.Rows.Count > 0)
            {
                foreach (DataRow item in dtAccount.Rows)
                {
                    list.Add(new SybShopBankAccount()
                    {
                        bankAccount = (int)item["identity_Id"],
                        bankAccountDesc = (string)item["accountStr"]
                    });
                }
            }
            message.Insert(1, JsonOperate.JsonSerializer<List<SybShopBankAccount>>(list));
            return message.Value;
        }
        /*
         *辅助方法
        */
        //判断Image对象的后缀
        static bool CheckImageExtension(Image p_Image, ref string extension)
        {
            string[] str = new string[] { "jpg", "png", "jpeg" };
            string currectExtension = GetImageExtension(p_Image).ToLower();
            extension = currectExtension;
            return str.Any(p => p.Contains(currectExtension));
        }
        //获取Image对象的后缀
        static string GetImageExtension(Image p_Image)
        {
            Type type = typeof(ImageFormat);
            PropertyInfo[] _ImageFormatList = type.GetProperties(BindingFlags.Static | BindingFlags.Public);
            for (int i = 0; i < _ImageFormatList.Length; i++)
            {
                ImageFormat _FormatClass = (ImageFormat)_ImageFormatList[i].GetValue(null, null);
                if (_FormatClass.Guid.Equals(p_Image.RawFormat.Guid))
                {
                    return _ImageFormatList[i].Name;
                }
            }
            return "";
        }
        /// <summary>
        /// 获取一级商圈信息
        /// </summary>
        /// <param name="cityId"></param>
        /// <returns></returns>
        public static string GetLevel1(int cityId)
        {
            SybMsg message = new SybMsg();
            IShopTagService shopTagService = ServiceFactory.Resolve<IShopTagService>();
            List<ShopTag> shopLevel1 = shopTagService.GetFirstGradeShopTagByCityId(cityId);
            if (shopLevel1 != null && shopLevel1.Count > 0)
            {
                message.Insert(1, JsonOperate.JsonSerializer<List<ShopTag>>(shopLevel1));
                return message.Value;
            }
            shopLevel1.Add(new ShopTag()
            {
                TagId = 0,
                Name = "暂无信息",
                Flag = 0,
                ShopCount = 0
            });
            message.Insert(1, JsonOperate.JsonSerializer<List<ShopTag>>(shopLevel1));
            return message.Value;
        }
        /// <summary>
        /// 获取二级商圈信息
        /// </summary>
        /// <param name="tagId"></param>
        /// <returns></returns>
        public static string GetLevel2(int tagId)
        {
            SybMsg message = new SybMsg();
            IShopTagService shopTagService = ServiceFactory.Resolve<IShopTagService>();
            List<ShopTag> shopLevel2 = shopTagService.GetSecondGradeShopTagByFirstGrade(tagId);
            if (shopLevel2 != null && shopLevel2.Count > 0)
            {
                message.Insert(1, JsonOperate.JsonSerializer<List<ShopTag>>(shopLevel2));
                return message.Value;
            }
            shopLevel2.Add(new ShopTag()
            {
                TagId = 0,
                Name = "暂无信息",
                Flag = 0,
                ShopCount = 0
            });
            message.Insert(1, JsonOperate.JsonSerializer<List<ShopTag>>(shopLevel2));
            return message.Value;
        }
        /// <summary>
        /// 查询门店已有商圈
        /// </summary>
        /// <param name="shopId"></param>
        public static string GetShopTag(int shopId)
        {
            SybMsg message = new SybMsg();
            IShopTagService shopTagService = ServiceFactory.Resolve<IShopTagService>();
            List<ShopTag> shopTags = shopTagService.GetShopTagByShopId(shopId);
            if (shopTags != null && shopTags.Count > 0)
            {
                message.Insert(1, JsonOperate.JsonSerializer<List<ShopTag>>(shopTags));
                return message.Value;
            }
            message.Insert(-1, "无任何信息");
            return message.Value;
        }
        /// <summary>
        /// 门店添加商圈
        /// </summary>
        /// <param name="tagId"></param>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public static string AddShopTag(string tagId, int shopId)
        {
            SybMsg message = new SybMsg();
            IShopTagService shopTagService = ServiceFactory.Resolve<IShopTagService>();
            object[] shopWithTag = shopTagService.MaintainShopTag(tagId, shopId, false);
            if (Common.ToBool(shopWithTag[0]))
            {
                message.Insert(1, "添加成功");
            }
            else
            {
                message.Insert(-1, "添加失败");
            }
            return message.Value;
        }
        /// <summary>
        /// 删除门店商圈
        /// </summary>
        /// <param name="tagId"></param>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public static string RemoveShopTag(string tagId, int shopId)
        {
            SybMsg message = new SybMsg();
            IShopTagService shopTagService = ServiceFactory.Resolve<IShopTagService>();
            object[] shopWithTag = shopTagService.MaintainShopTag(tagId, shopId, true);
            if (Common.ToBool(shopWithTag[0]))
            {
                message.Insert(1, "删除成功");
            }
            else
            {
                message.Insert(-1, "删除失败");
            }
            return message.Value;
        }

        /// <summary>
        /// 查询门店提款方式
        /// </summary>
        /// <param name="shopId"></param>
        public static string GetWithdrawType(int shopId)
        {
            SybMsg message = new SybMsg();
            IShopTagService shopTagService = ServiceFactory.Resolve<IShopTagService>();
            int withdrawType = shopTagService.GetWithdrawTypeShopId(shopId);
            string strWithdraw = string.Empty;

            if (withdrawType > 0)
            {
                WithdrawType wt = (WithdrawType)withdrawType;
                if (wt.HasFlag(WithdrawType.monday))
                {
                    strWithdraw += "1,";
                }
                if (wt.HasFlag(WithdrawType.tuesday))
                {
                    strWithdraw += "2,";
                }
                if (wt.HasFlag(WithdrawType.wednesday))
                {
                    strWithdraw += "3,";
                }
                if (wt.HasFlag(WithdrawType.thursday))
                {
                    strWithdraw += "4,";
                }
                if (wt.HasFlag(WithdrawType.friday))
                {
                    strWithdraw += "5,";
                }

                if (strWithdraw.Length > 0)
                {
                    strWithdraw = strWithdraw.Substring(0, strWithdraw.Length - 1);
                }

                //message.Insert(1, JsonOperate.JsonSerializer<>(withdrawType));
                message.Insert(1, strWithdraw);
                return message.Value;
            }
            else if (withdrawType == 0)
            {
                message.Insert(1, string.Empty);
                return message.Value;
            }
            message.Insert(-1, "无任何信息");
            return message.Value;
        }

        /// <summary>
        /// 修改门店提款方式
        /// </summary>
        /// <param name="shopId"></param>
        public static string UpdateWithdrawType(int shopId, string strWithdrawtype)
        {
            int withdrawtype = 0;
            if (strWithdrawtype.IndexOf("1") != -1)
            {
                withdrawtype += 1;
            }
            if (strWithdrawtype.IndexOf("2") != -1)
            {
                withdrawtype += 2;
            }
            if (strWithdrawtype.IndexOf("3") != -1)
            {
                withdrawtype += 4;
            }
            if (strWithdrawtype.IndexOf("4") != -1)
            {
                withdrawtype += 8;
            }
            if (strWithdrawtype.IndexOf("5") != -1)
            {
                withdrawtype += 16;
            }

            SybMsg message = new SybMsg();
            IShopTagService shopTagService = ServiceFactory.Resolve<IShopTagService>();
            int result = shopTagService.UpdateWithdrawType(shopId, withdrawtype);
            if (result != 0)
            {
                message.Insert(1, "修改成功");
                return message.Value;

            }
            message.Insert(-1, "修改失败");
            return message.Value;
        }

        /// <summary>
        /// 查询佣金比例
        /// </summary>
        /// <param name="shopid"></param>
        /// <returns></returns>
        public static string GetViewallocCommissionValue(int shopid)
        {
            SybMsg message = new SybMsg();
            IShopTagService shopTagService = ServiceFactory.Resolve<IShopTagService>();
            double result = Math.Round(shopTagService.GetViewallocCommissionValue(shopid) * 100, 2);
            message.Insert(1, result.ToString());
            return message.Value;
        }

        /// <summary>
        /// 修改佣金比例
        /// </summary>
        /// <param name="shopid"></param>
        /// <returns></returns>
        public static string UpdateViewallocCommissionValue(int shopid, double viewalloccommissionvalue)
        {
            SybMsg message = new SybMsg();
            if (viewalloccommissionvalue < 0 || viewalloccommissionvalue > 100)
            {
                message.Insert(-1, "修改失败");
                return message.Value;
            }

            viewalloccommissionvalue = viewalloccommissionvalue / 100;

            IShopTagService shopTagService = ServiceFactory.Resolve<IShopTagService>();
            int result = shopTagService.UpdateViewallocCommissionValue(shopid, viewalloccommissionvalue);
            if (result != 0)
            {
                message.Insert(1, "修改成功");
                return message.Value;

            }
            message.Insert(-1, "修改失败");
            return message.Value;
        }

        /// <summary>
        /// 查询折扣信息
        /// </summary>
        /// <param name="shopid"></param>
        /// <returns></returns>
        public static string GetShopVipInfo(int shopid)
        {
            SybMsg message = new SybMsg();
            IShopTagService shopTagService = ServiceFactory.Resolve<IShopTagService>();
            double result = shopTagService.GetShopVipInfo(shopid);
            message.Insert(1, result.ToString());
            return message.Value;
        }

        public static string UpdateShopVipInfo(int shopid, double discount)
        {
            SybMsg message = new SybMsg();
            if (discount < 0 || discount > 100)
            {
                message.Insert(-1, "修改失败");
                return message.Value;
            }

            discount = discount / 100;

            IShopTagService shopTagService = ServiceFactory.Resolve<IShopTagService>();
            #region------------------------------------
            double oldResult = shopTagService.GetShopVipInfo(shopid);
            if(oldResult!=discount)
            {
                int employeeID = ((VAEmployeeLoginResponse)HttpContext.Current.Session["MerchantsTreasureUserInfo"]).employeeID;
                ShopAwardVersionOperate operateVersion = new ShopAwardVersionOperate();
                operateVersion.InsertShopAwardVersionAndLog(employeeID, shopid, "修改店铺折扣", "老后台", Guid.Empty);
            }
            #endregion
            int result = shopTagService.UpdateShopVipInfo(shopid, discount);
            if (result != 0)
            {
                message.Insert(1, "修改成功");
                return message.Value;

            }
            message.Insert(-1, "修改失败");
            return message.Value;
        }

        public static string GetAccountInfo(int shopid)
        {
            SybMsg message = new SybMsg();
            IShopTagService shopTagService = ServiceFactory.Resolve<IShopTagService>();
            CompanyAccountInfo model = shopTagService.GetAccountInfo(shopid);
            if (model == null)
            {
                message.Insert(-1, "当前门店无对应的银行帐户信息");
                return message.Value;
            }
            message.Insert(1, JsonOperate.JsonSerializer<CompanyAccountInfo>(model));
            return message.Value;
        }

        public static string UpdateAccountInfo(int shopid, string accountJson)
        {
            SybMsg message = new SybMsg();
            CompanyAccountInfo model = JsonOperate.JsonDeserialize<CompanyAccountInfo>(accountJson.Replace(" ", "").Replace("\n", ""));
            if (model == null)
            {
                message.Insert(-2, "提交数据有误");
                return message.Value;
            }
            if (String.IsNullOrWhiteSpace(model.accountName))
            {
                message.Insert(-100, "开户名不能为空");
                return message.Value;
            }
            if (model.accountName.IndexOf("<") != -1 || model.accountName.IndexOf(">") != -1
                || model.accountName.IndexOf("（") != -1 || model.accountName.IndexOf("）") != -1)
            {
                message.Insert(-100, "开户名 不得包含尖括号<> ，圆括号使用半角符号()");
                return message.Value;
            }
            if (String.IsNullOrWhiteSpace(model.accountNum))
            {
                message.Insert(-99, "账户号码不能为空");
                return message.Value;
            }
            if (model.accountNum.IndexOf("<") != -1 || model.accountNum.IndexOf(">") != -1
               || model.accountNum.IndexOf("（") != -1 || model.accountNum.IndexOf("）") != -1)
            {
                message.Insert(-100, "账户号码 不得包含尖括号<> ，圆括号使用半角符号()");
                return message.Value;
            }
            if (model.accountNum.Length > 21 || model.accountNum.Length < 9)
            {
                message.Insert(-89, "账户号码位数错误");
                return message.Value;
            }
            if (String.IsNullOrWhiteSpace(model.bankName))
            {
                message.Insert(-98, "开户银行不能为空");
                return message.Value;
            }
            if (model.bankName.IndexOf("<") != -1 || model.bankName.IndexOf(">") != -1
               || model.bankName.IndexOf("（") != -1 || model.bankName.IndexOf("）") != -1)
            {
                message.Insert(-100, "开户银行 不得包含尖括号<> ，圆括号使用半角符号()");
                return message.Value;
            }
            if (String.IsNullOrWhiteSpace(model.payeeBankName))
            {
                message.Insert(-98, "开户银行分行不能为空");
                return message.Value;
            }
            if (model.payeeBankName.IndexOf("<") != -1 || model.payeeBankName.IndexOf(">") != -1
               || model.payeeBankName.IndexOf("（") != -1 || model.payeeBankName.IndexOf("）") != -1)
            {
                message.Insert(-100, "开户银行分行 不得包含尖括号<> ，圆括号使用半角符号()");
                return message.Value;
            }

            CompanyAccountOprate operate = new CompanyAccountOprate();
            if (model.identity_Id > 0)//修改
            {
                DataTable dtAccount = operate.QueryAccountById(model.identity_Id);
                if (dtAccount.Rows.Count == 1)
                {
                    if (operate.ModifyAccountInfo(model, dtAccount))
                    {
                        message.Insert(1, "修改成功");
                    }
                    else
                    {
                        message.Insert(-1, "修改失败");
                    }
                }
                else
                {
                    message.Insert(-3, "当前银行编号信息未找到");
                }
            }
            else//添加
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    int identity_Id = operate.InsertAccount(model);
                    if (identity_Id > 0)
                    {
                        ShopManager sm = new ShopManager();

                        if (sm.updateShopAccount(shopid, identity_Id) > 0)
                        {
                            scope.Complete();
                        }
                        else
                        {
                            message.Insert(-1, "新增失败");
                        }

                        message.Insert(1, "新增成功");
                    }
                    else
                    {
                        message.Insert(-1, "新增失败");
                    }
                }
            }
            return message.Value;
        }
    }
}
