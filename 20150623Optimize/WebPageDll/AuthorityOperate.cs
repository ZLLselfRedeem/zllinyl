using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL;
using System.Web;
using System.Transactions;

namespace VAGastronomistMobileApp.WebPageDll
{
    /// <summary>
    /// 权限信息操作类
    /// </summary>
    public class AuthorityOperate
    {
        /// <summary>
        /// 新增权限信息
        /// </summary>
        /// <param name="authority"></param>
        /// <returns></returns>
        public bool AddAuthority(AuthorityInfo authority)
        {
            AuthorityManager authorityMan = new AuthorityManager();
            DataTable dtAuthority = authorityMan.SelectAuthority();
            DataView dvAuthority = dtAuthority.DefaultView;
            dvAuthority.RowFilter = "AuthorityName = '" + authority.AuthorityName + "'";
            if (dvAuthority.Count > 0 || authority.AuthorityName == "" || authority.AuthorityName == null)
            {//如果所加权限信息的名称为空，或者权限信息表中已有该名称的权限，则直接返回false
                return false;
            }
            else
            {
                if (authorityMan.InsertAuthority(authority) > 0)
                {//插入数据库表成功则返回ture
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        /// <summary>
        /// 删除权限信息
        /// </summary>
        /// <param name="authorityID"></param>
        /// <returns></returns>
        public bool RemoveAuthority(int authorityID)
        {
            AuthorityManager authorityMan = new AuthorityManager();
            DataTable dtAuthority = authorityMan.SelectAuthority();
            DataView dvAuthority = dtAuthority.DefaultView;
            dvAuthority.RowFilter = "AuthorityID = '" + authorityID + "'";
            if (1 == dvAuthority.Count)
            {//判断此authorityID是否存在，是则删除
                if (authorityMan.DeleteAuthority(authorityID))
                {//删除成功则返回true
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
        /// 修改权限信息
        /// </summary>
        /// <param name="authority"></param>
        /// <returns></returns>
        public bool ModifyAuthority(AuthorityInfo authority)
        {
            AuthorityManager authorityMan = new AuthorityManager();
            DataTable dtAuthority = authorityMan.SelectAuthority();
            DataTable dtAuthorityCopy = dtAuthority.Copy();
            DataView dvAuthorityCopy = dtAuthorityCopy.DefaultView;
            DataView dvAuthority = dtAuthority.DefaultView;
            dvAuthorityCopy.RowFilter = "AuthorityID = '" + authority.AuthorityID + "'";//判断此ID存在
            dvAuthority.RowFilter = "AuthorityID <> '" + authority.AuthorityID
                + "' and AuthorityName = '" + authority.AuthorityName + "'";//判断修改的姓和名不存在
            if (1 == dvAuthorityCopy.Count && 0 == dvAuthority.Count)
            {//判断此EmployeeID是否存在，是则修改
                if (authorityMan.UpdateAuthority(authority))
                {//修改成功则返回true
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
        /// 查询所有权限信息
        /// </summary>
        /// <returns></returns>
        public DataTable QueryAuthority()
        {
            AuthorityManager authorityMan = new AuthorityManager();
            DataTable dtAuthority = authorityMan.SelectAuthority();
            DataView dvAuthority = dtAuthority.DefaultView;
            dvAuthority.Sort = "AuthorityName ASC";
            return dvAuthority.ToTable();
        }
        /// <summary>
        /// 当前
        /// </summary>
        /// <returns></returns>
        public bool ShowAuthorityVerification()
        {
            SystemConfigManager systemConfigMan = new SystemConfigManager();
            DataTable dtSystemConfig = systemConfigMan.SelectSystemConfig(string.Empty,string.Empty);
            DataRow[] drSystemConfig = dtSystemConfig.Select("configName = 'ShowAuthorityVerification' ");
            int show = Common.ToInt32(drSystemConfig[0]["configContent"]);
            if (show == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        /// <summary>
        /// 查询当前门店关联员工信息(wangcheng)
        /// </summary>
        /// <returns></returns>
        public string SYBQueryEmployeeShop(int pageIndex, int pageSize, int shopId, bool isViewAllocWorker)
        {
            AuthorityManager authorityMan = new AuthorityManager();
            return JsonOperate.JsonSerializer<SybEmployeeShopInfoList>(authorityMan.SYBSelectEmployeeShop(pageIndex, pageSize, shopId, isViewAllocWorker));
        }
        /// <summary>
        /// 商户为当前门店添加管理员
        /// </summary>
        /// <param name="shopId"></param>
        /// <param name="phoneNum"></param>
        /// <returns></returns>
        public string SYBAddEmployeeShop(int shopId, string phoneNum)
        {
            EmployeeConnShopOperate employeeShopOperate = new EmployeeConnShopOperate();
            EmployeeConnShop employeeShop = new EmployeeConnShop();
            employeeShop.shopID = shopId;
            //employeeShop.isSupportEnterSyb = false;
            employeeShop.status = 1;
            employeeShop.serviceStartTime = DateTime.Now;//服务员在当前门店服务开始时间
            AuthorityManager manager = new AuthorityManager();
            DataTable dtEmployeeShop = new DataTable();
            DataView dvEmployeeShop = new DataView();
            SybMsg sysMsg = new SybMsg();
            if (string.IsNullOrWhiteSpace(phoneNum))
            {
                sysMsg.Insert(-1, "请输入手机号码");
            }
            else
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    DataTable dt = manager.SelectEmployeeIdByEmployeePhone(Common.ToClearSpecialCharString(phoneNum));//注：此时用户名就是手机号码
                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows.Count == 1)
                        {
                            ShopManager shopMan = new ShopManager();
                            DataTable dtShop = shopMan.SelectShop(shopId);
                            if (dtShop.Rows.Count > 0)
                            {
                                employeeShop.companyID = Common.ToInt32(dtShop.Rows[0]["companyID"]);
                                employeeShop.employeeID = Common.ToInt32(dt.Rows[0]["EmployeeID"]);
                                dtEmployeeShop = manager.SelectEmployeeShop();
                                dvEmployeeShop = dtEmployeeShop.DefaultView;
                                dvEmployeeShop.RowFilter = "employeeID = '" + employeeShop.employeeID + "' and shopID = '" + employeeShop.shopID + "'";
                                if (dvEmployeeShop.Count > 0)
                                {
                                    sysMsg.Insert(-5, "该店员已添加");
                                }
                                else
                                {
                                    if (manager.InsertEmployeeShop(employeeShop) > 0)
                                    {
                                        scope.Complete();
                                        sysMsg.Insert(1, "添加成功");
                                    }
                                    else
                                    {
                                        sysMsg.Insert(-4, "添加失败");
                                    }
                                }
                            }
                            else
                            {
                                sysMsg.Insert(-3, "门店信息未找到");
                            }
                        }
                        else
                        {
                            sysMsg.Insert(-6, "该手机号码对应多个用户，请联系管理员");
                        }
                    }
                    else
                    {
                        sysMsg.Insert(-2, "手机号码不存在");
                    }
                }
            }
            return sysMsg.Value;
        }
        /// <summary>
        /// 删除商户为当前门店添加管理员
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        public string SYBDeleteEmployeeShop(int employeeID)
        {
            SybMsg sysMsg = new SybMsg();
            AuthorityManager manager = new AuthorityManager();
            if (employeeID == 0)
            {
                sysMsg.Insert(-2, "选中员工信息有误");
            }
            else
            {
                int shopID = Common.ToInt32(HttpContext.Current.Session["loginshop"]);
                if (shopID > 0)
                {
                    if (manager.UpdateEmployeeShopStatus(employeeID, shopID, -1) > 0)
                    {
                        sysMsg.Insert(1, "删除成功");
                    }
                    else
                    {
                        sysMsg.Insert(-1, "删除失败");
                    }
                }
                else
                {
                    sysMsg.Insert(-1, "删除失败");
                }
            }
            return sysMsg.Value;
        }
        /// <summary>
        /// 收银宝功能模块二级页面权限
        /// </summary>
        /// <param name="currentPageName"></param>
        /// <returns></returns>
        public string SybGetPageNameStr(string currentPageName)
        {
            int employeeID = ((VAEmployeeLoginResponse)HttpContext.Current.Session["MerchantsTreasureUserInfo"]).employeeID;
            int shopId = Common.ToInt32(HttpContext.Current.Session["loginshop"]);
            if (employeeID > 0 && shopId > 0)
            {
                SybEmployeeShopAuthority operate = new SybEmployeeShopAuthority();
                EmployeeOperate employeeOperate = new EmployeeOperate();
                EmployeeInfo employee = employeeOperate.QueryEmployee(employeeID);
                bool isSupportShopManagePage = employee.isViewAllocWorker == true ? true
                                                      : (operate.QuerySybAuthority(employeeID, shopId, "店员管理") == true ? true
                                                      : false);//查询是否具备店员管理页面权限
                if (isSupportShopManagePage == true)
                {
                    return "{\"current\":\"" + currentPageName + "\",\"tab\":\"configurationEmployees,configurationPassword\"}";
                }
                else
                {
                    return "{\"current\":\"" + currentPageName + "\",\"tab\":\"configurationPassword\"}";
                }
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 查询当前员工可进入收银宝的门店
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        public DataTable QueryEmployeeConnShopIsSupportEnterSyb(int employeeID)
        {
            AuthorityManager man = new AuthorityManager();
            return man.SelectEmployeeConnShopIsSupportEnterSyb(employeeID);
        }
        /// <summary>
        /// 根据电话号码查询员工信息 add by wangc 20140326
        /// </summary>
        /// <param name="phoneNum"></param>
        /// <returns></returns>
        public DataTable QueryEmployeeIdByEmployeePhone(string phoneNum)
        {
            AuthorityManager man = new AuthorityManager();
            return man.SelectEmployeeIdByEmployeePhone(phoneNum);
        }
    }
}
