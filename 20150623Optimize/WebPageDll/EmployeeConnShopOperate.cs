﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Transactions;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL;

namespace VAGastronomistMobileApp.WebPageDll
{
    /// <summary>
    /// 店铺管理员与店铺关系操作类
    /// </summary>
    public class EmployeeConnShopOperate
    {
        /// <summary>
        /// 新增店铺管理员与店铺关系信息
        /// </summary>
        /// <param name="employeeShop"></param>
        /// <returns></returns>
        public bool AddEmployeeShop(EmployeeConnShop employeeShop)
        {
            AuthorityManager authorityMan = new AuthorityManager();
            DataTable dtEmployeeShop = authorityMan.SelectEmployeeShop();
            DataView dvEmployeeShop = dtEmployeeShop.DefaultView;
            dvEmployeeShop.RowFilter = "employeeID = '" + employeeShop.employeeID
                + "' and shopID = '" + employeeShop.shopID + "'";
            if (dvEmployeeShop.Count > 0)
            {//店铺管理员与店铺关系表中已有相同信息（同一employeeID和同一shopID），则直接返回false
                return false;
            }
            else
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    if (authorityMan.InsertEmployeeShop(employeeShop) > 0)
                    {//插入数据库表成功则返回ture
                        scope.Complete();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }
        /// <summary>
        /// 根据employeeID和shopID将其对应的门店关系数据重置status
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        public bool UpdateEmployeeShop(int employeeID, int shopID, int status)
        {
            AuthorityManager authorityMan = new AuthorityManager();
            DataTable dtEmployeeShop = authorityMan.SelectEmployeeShopAll(employeeID, shopID);
            if (dtEmployeeShop.Rows.Count > 0)
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    if (authorityMan.UpdateEmployeeShopStatus(employeeID, shopID, status) > 0)//更新成功
                    {
                        scope.Complete();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 根据employeeID将其对应的所有门店关系数据置-1
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        public bool UpdateEmployeeShop(int employeeID)
        {
            AuthorityManager authorityMan = new AuthorityManager();
            if (authorityMan.UpdateEmployeeShopStatus(employeeID) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 查询店铺管理员与店铺关系信息是否存在对应关系（曾经被删除了）
        /// </summary>
        /// <returns></returns>
        public bool SelectEmployeeShop(int employeeId, int shopID)
        {
            AuthorityManager authorityMan = new AuthorityManager();
            DataTable dt = authorityMan.SelectEmployeeShopAll(employeeId, shopID);
            if (dt != null && dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["status"].ToString() == "-1")
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
        /// 根据店铺管理员编号删除对应的所有店铺管理员与店铺关系信息
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        public bool RemoveEmployeeShop(int employeeID)
        {
            AuthorityManager authorityMan = new AuthorityManager();
            DataTable dtEmployeeShop = authorityMan.SelectEmployeeShop();
            DataView dvEmployeeShop = dtEmployeeShop.DefaultView;
            dvEmployeeShop.RowFilter = "employeeID = '" + employeeID + "'";
            if (dvEmployeeShop.Count > 0)
            {//判断此employeeShopID是否存在，是则删除
                using (TransactionScope scope = new TransactionScope())
                {
                    if (authorityMan.DeleteEmployeeShop(employeeID))//删除成功则返回true                    
                    {
                        scope.Complete();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 根据店铺管理员编号查询对应公司信息
        /// （wangcheng修改）
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        public List<VAEmployeeCompany> QueryEmployeeCompany(int employeeID, bool isControlSyb = false)
        {
            RoleOperate roleOperate = new RoleOperate();
            List<VAEmployeeCompany> companyList = new List<VAEmployeeCompany>();
            DataTable dtQueryAllCompamnyInfo = roleOperate.QuerySpecialAuthorityInfoByEmployeeID(employeeID, (int)VASpecialAuthority.CHECK_ALL_COMPANY);
            if (dtQueryAllCompamnyInfo.Rows.Count > 0)//第一层权限查询
            {
                CompanyOperate companyOperate = new CompanyOperate();
                companyList = companyOperate.QueryCompanyNameAndId();//所有公司
            }
            else
            {
                DataTable dt = roleOperate.QuerySpecialAuthorityInfoByEmployeeID(employeeID, (int)VASpecialAuthority.CHECK_PREORDER_ONLINE_COMPANY);
                if (dt.Rows.Count > 0)//第二层权限查询
                {
                    EmployeeConnShopOperate employeeConnShopOperate = new EmployeeConnShopOperate();
                    companyList = employeeConnShopOperate.QueryCompanyIsHandle();//上线公司
                }
                else
                {
                    AuthorityManager authorityMan = new AuthorityManager();
                    DataTable dtEmployeeCompany = new DataTable();
                    if (isControlSyb == true)
                    {
                        dtEmployeeCompany = authorityMan.SelectEmployeeCompany(employeeID, true);
                    }
                    else
                    {
                        dtEmployeeCompany = authorityMan.SelectEmployeeCompany(employeeID);
                    }
                    if (dtEmployeeCompany.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtEmployeeCompany.Rows.Count; i++)
                        {
                            VAEmployeeCompany company = new VAEmployeeCompany();
                            company.companyID = Common.ToInt32(dtEmployeeCompany.Rows[i]["companyID"]);
                            company.companyName = Common.ToString(dtEmployeeCompany.Rows[i]["companyName"]);
                            companyList.Add(company);
                        }
                    }
                }
            }
            return companyList;
        }
        public List<VAEmployeeCompany> QueryEmployeeCompanyRemoveNotShop(int employeeID, bool isControlSyb = false)
        {
            RoleOperate roleOperate = new RoleOperate();
            List<VAEmployeeCompany> companyList = new List<VAEmployeeCompany>();
            DataTable dtQueryAllCompamnyInfo = roleOperate.QuerySpecialAuthorityInfoByEmployeeID(employeeID, (int)VASpecialAuthority.CHECK_ALL_COMPANY);
            if (dtQueryAllCompamnyInfo.Rows.Count > 0)//第一层权限查询
            {
                CompanyOperate companyOperate = new CompanyOperate();
                companyList = companyOperate.QueryCompanyNameAndIdRemovenNotHaveShop();//所有公司，过滤掉没有门店的公司
            }
            else
            {
                DataTable dt = roleOperate.QuerySpecialAuthorityInfoByEmployeeID(employeeID, (int)VASpecialAuthority.CHECK_PREORDER_ONLINE_COMPANY);
                if (dt.Rows.Count > 0)//第二层权限查询
                {
                    EmployeeConnShopOperate employeeConnShopOperate = new EmployeeConnShopOperate();
                    companyList = employeeConnShopOperate.QueryCompanyIsHandle();//上线公司，过滤没有门店，没有上线门店
                }
                else
                {
                    AuthorityManager authorityMan = new AuthorityManager();
                    DataTable dtEmployeeCompany = new DataTable();
                    if (isControlSyb == true)
                    {
                        dtEmployeeCompany = authorityMan.SelectEmployeeCompanyRemoveNotShop(employeeID, true);
                    }
                    else
                    {
                        dtEmployeeCompany = authorityMan.SelectEmployeeCompanyRemoveNotShop(employeeID);
                    }
                    if (dtEmployeeCompany.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtEmployeeCompany.Rows.Count; i++)
                        {
                            VAEmployeeCompany company = new VAEmployeeCompany();
                            company.companyID = Common.ToInt32(dtEmployeeCompany.Rows[i]["companyID"]);
                            company.companyName = Common.ToString(dtEmployeeCompany.Rows[i]["companyName"]);
                            companyList.Add(company);
                        }
                    }
                    else
                    {
                        companyList = null;
                    }
                }
            }
            return companyList;
        }
        /// <summary>
        /// 根据店铺管理员编号查询对应店铺信息
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        public List<VAEmployeeShop> QueryEmployeeShop(int employeeID, bool onlyHandled = false)
        {
            AuthorityManager authorityMan = new AuthorityManager();
            DataTable dtEmployeeShop = authorityMan.SelectEmployeeShop(employeeID,onlyHandled);

            List<VAEmployeeShop> shopList = new List<VAEmployeeShop>();
            if (dtEmployeeShop.Rows.Count > 0)
            {
                for (int i = 0; i < dtEmployeeShop.Rows.Count; i++)
                {
                    VAEmployeeShop shop = new VAEmployeeShop();
                    shop.shopID = Common.ToInt32(dtEmployeeShop.Rows[i]["shopID"]);
                    shop.shopName = Common.ToString(dtEmployeeShop.Rows[i]["shopName"]);
                    shop.companyName = Common.ToString(dtEmployeeShop.Rows[i]["companyName"]);
                    shop.cityId = Common.ToInt32(dtEmployeeShop.Rows[i]["cityID"]);
                    shopList.Add(shop);
                }
            }
            return shopList;
        }
        public List<VAEmployeeShop> QueryHandleShop()
        {
            AuthorityManager authorityMan = new AuthorityManager();
            DataTable dtEmployeeShop = authorityMan.SelectHandleShop();
            List<VAEmployeeShop> shopList = new List<VAEmployeeShop>();
            int count = dtEmployeeShop.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    VAEmployeeShop shop = new VAEmployeeShop();
                    shop.shopID = Common.ToInt32(dtEmployeeShop.Rows[i]["shopID"]);
                    shop.shopName = Common.ToString(dtEmployeeShop.Rows[i]["shopName"]);
                    shop.companyName = Common.ToString(dtEmployeeShop.Rows[i]["companyName"]);
                    shop.cityId = Common.ToInt32(dtEmployeeShop.Rows[i]["cityID"]);
                    shopList.Add(shop);
                }
            }
            return shopList;
        }
        /// <summary>
        /// 根据店铺管理员编号查询对应店铺信息
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        public DataTable QueryEmployeeConnShop(int employeeID)
        {
            AuthorityManager authorityMan = new AuthorityManager();
            DataTable dtEmployeeShop = authorityMan.SelectEmployeeShop();
            DataView dvEmployeeShop = dtEmployeeShop.DefaultView;
            dvEmployeeShop.RowFilter = "employeeID = '" + employeeID + "'";
            return dvEmployeeShop.ToTable();
        }
        /// <summary>
        /// 根据店铺管理员编号和公司信息查询对应店铺信息
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="companyId"></param>
        /// <param name="isControlSyb">是否控制收银宝（wangcheng）</param>
        /// <returns></returns>
        public List<VAEmployeeShop> QueryEmployeeShopByCompanyAndEmplyee(int employeeId, int companyId, bool isControlSyb = false)
        {
            AuthorityManager authorityMan = new AuthorityManager();
            DataTable dtEmployeeShop = new DataTable();
            DataView dvEmployeeShop = new DataView();
            RoleOperate roleOperate = new RoleOperate();
            ShopManager shopManager = new ShopManager();
            DataTable dtQueryAllCompamnyInfo = roleOperate.QuerySpecialAuthorityInfoByEmployeeID(employeeId, (int)VASpecialAuthority.CHECK_ALL_COMPANY);//查看所有公司权限
            //注：具备查看所有公司或者查看所有上线公司中的一种特殊权限，则对应的门店列表显示不去查询employee对应的表信息
            if (dtQueryAllCompamnyInfo.Rows.Count > 0)//第一层权限查询
            {
                dtEmployeeShop = shopManager.SelectShopByCompany(companyId);//所有门店
                dvEmployeeShop = dtEmployeeShop.DefaultView;
            }
            else
            {
                DataTable dtQueryOnlineCompamnyInfo = roleOperate.QuerySpecialAuthorityInfoByEmployeeID(employeeId, (int)VASpecialAuthority.CHECK_PREORDER_ONLINE_COMPANY);//查看所有上线公司权限
                if (dtQueryOnlineCompamnyInfo.Rows.Count > 0)//第二层权限
                {
                    dtEmployeeShop = shopManager.SelectShopByCompany(companyId, true);//所有上线门店
                    dvEmployeeShop = dtEmployeeShop.DefaultView;
                }
                else//第三层权限，分配
                {
                    if (isControlSyb == true)//收银宝
                    {
                        dtEmployeeShop = authorityMan.SelectEmployeeShop(true);
                    }
                    else
                    {
                        dtEmployeeShop = authorityMan.SelectEmployeeShop();
                    }
                    dvEmployeeShop = dtEmployeeShop.DefaultView;
                    dvEmployeeShop.RowFilter = "employeeID = '" + employeeId + "' and companyId=" + companyId;
                }
            }
            List<VAEmployeeShop> shopList = new List<VAEmployeeShop>();
            if (dvEmployeeShop.Count > 0)
            {
                for (int i = 0; i < dvEmployeeShop.Count; i++)
                {
                    VAEmployeeShop shop = new VAEmployeeShop();
                    shop.shopID = Common.ToInt32(dvEmployeeShop[i]["shopID"]);
                    shop.shopName = Common.ToString(dvEmployeeShop[i]["shopName"]);
                    shop.shopImagePath = Common.ToString(dvEmployeeShop[i]["shopImagePath"]);
                    shop.isHandle = Common.ToInt32(dvEmployeeShop[i]["isHandle"]);
                    shopList.Add(shop);
                }
            }
            else
            {
                shopList = null;
            }
            return shopList;
        }
        /// <summary>
        /// 根据employeeID获取companyImagePath
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        public string QuerycompanyImagePathByemploeeId(int employeeID)
        {
            AuthorityManager am = new AuthorityManager();
            DataTable dt = am.SelectEmployeeCompany(employeeID);
            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0]["companyImagePath"].ToString();
            }
            else
            {
                return null;
            }
        }
        public List<VAEmployeeCompany> QueryEmployeeCompanyIsHandle(int employeeID)
        {
            AuthorityManager am = new AuthorityManager();
            DataTable dt = am.SelectEmployeeCompanyIsHandle(employeeID);
            List<VAEmployeeCompany> companyList = new List<VAEmployeeCompany>();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    VAEmployeeCompany company = new VAEmployeeCompany();
                    company.companyID = Common.ToInt32(dt.Rows[i]["companyID"]);
                    company.companyName = Common.ToString(dt.Rows[i]["companyName"]);
                    companyList.Add(company);
                }
            }
            else
            {
                companyList = null;
            }
            return companyList;
        }
        public DataTable QueryEmployeeShopInfoAndCompanyInfo(int employeeID)
        {
            AuthorityManager am = new AuthorityManager();
            DataTable dt = am.SelectEmployeeShopInfoAndCompanyInfo(employeeID);
            return dt;
        }
        /// <summary>
        /// 查询所有上线公司
        /// </summary>
        /// <returns></returns>
        public List<VAEmployeeCompany> QueryCompanyIsHandle()
        {
            AuthorityManager am = new AuthorityManager();
            DataTable dt = am.SelectCompanyIsHandle();
            List<VAEmployeeCompany> companyList = new List<VAEmployeeCompany>();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    VAEmployeeCompany company = new VAEmployeeCompany();
                    company.companyID = Common.ToInt32(dt.Rows[i]["companyID"]);
                    company.companyName = Common.ToString(dt.Rows[i]["companyName"]);
                    companyList.Add(company);
                }
            }
            else
            {
                companyList = null;
            }
            return companyList;
        }
        /// <summary>
        /// 查询当前城市所有上线公司
        /// </summary>
        /// <param name="cityId"></param>
        /// <returns></returns>
        public List<VAEmployeeCompany> QueryCompanyIsHandle(int cityId)
        {
            AuthorityManager am = new AuthorityManager();
            DataTable dt = am.SelectCompanyIsHandle(cityId);
            List<VAEmployeeCompany> companyList = new List<VAEmployeeCompany>();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    companyList.Add(new VAEmployeeCompany()
                    {
                        companyID = Common.ToInt32(dt.Rows[i]["companyID"]),
                        companyName = Common.ToString(dt.Rows[i]["companyName"])
                    });
                }
            }
            return companyList;
        }
        /// <summary>
        /// 查询员工可以查看的门店
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public DataTable QueryEmployeeShopByEmplyee(int employeeId)
        {
            AuthorityManager authorityMan = new AuthorityManager();
            DataTable dtEmployeeShop = new DataTable();
            DataView dvEmployeeShop = new DataView();
            RoleOperate roleOperate = new RoleOperate();
            ShopManager shopManager = new ShopManager();
            DataTable dtQueryAllCompamnyInfo = roleOperate.QuerySpecialAuthorityInfoByEmployeeID(employeeId, (int)VASpecialAuthority.CHECK_ALL_COMPANY);//查看所有公司权限
            //注：具备查看所有公司或者查看所有上线公司中的一种特殊权限，则对应的门店列表显示不去查询employee对应的表信息
            if (dtQueryAllCompamnyInfo.Rows.Count > 0)//第一层权限查询
            {
                dtEmployeeShop = shopManager.SelectShop();//所有门店
                dvEmployeeShop = dtEmployeeShop.DefaultView;
            }
            else
            {
                DataTable dtQueryOnlineCompamnyInfo = roleOperate.QuerySpecialAuthorityInfoByEmployeeID(employeeId, (int)VASpecialAuthority.CHECK_PREORDER_ONLINE_COMPANY);//查看所有上线公司权限
                if (dtQueryOnlineCompamnyInfo.Rows.Count > 0)//第二层权限
                {
                    dtEmployeeShop = shopManager.SelectShop();//所有上线门店
                    dvEmployeeShop = dtEmployeeShop.DefaultView;
                    dvEmployeeShop.RowFilter = "isHandle=1";
                }
                else//第三层权限，分配
                {
                    dtEmployeeShop = authorityMan.SelectEmployeeShop();
                    dvEmployeeShop = dtEmployeeShop.DefaultView;
                    dvEmployeeShop.RowFilter = "employeeID = '" + employeeId + "'";
                }
            }
            dvEmployeeShop = dtEmployeeShop.DefaultView;
            return dvEmployeeShop.ToTable();
        }
        /// <summary>
        /// 根据employeeID将其对应的所有门店关系数据置-1
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        public bool RemoveEmployeeShopStatusByemployeeShopID(int employeeShopID)
        {
            AuthorityManager manager = new AuthorityManager();
            return manager.UpdateEmployeeShopStatusByemployeeShopID(employeeShopID) > 0;
        }

        /// <summary>
        /// 查询员工权限下公司列表
        /// </summary>
        /// <param name="employeeID"></param>
        /// <param name="cityId"></param>
        /// <returns></returns>
        public List<CompanyViewModel> QueryEmployeeCompany(int employeeID, int cityId)
        {
            var companyOperate = new CompanyManager();
            var companyList = new List<CompanyViewModel>();
            DataTable dtQueryAllCompamnyInfo = new RoleOperate().QuerySpecialAuthorityInfoByEmployeeID(employeeID, (int)VASpecialAuthority.CHECK_ALL_COMPANY);
            if (dtQueryAllCompamnyInfo.Rows.Count > 0)//第一层权限查询
            {
                companyList = companyOperate.SelectCompanyByCity(cityId);//所有公司
            }
            else
            {
                DataTable dt = new RoleOperate().QuerySpecialAuthorityInfoByEmployeeID(employeeID, (int)VASpecialAuthority.CHECK_PREORDER_ONLINE_COMPANY);
                companyList = dt.Rows.Count > 0 ? companyOperate.SelectCompanyByCity(cityId, true) : companyOperate.SelectEmlpoyeeCompanyByCity(cityId, employeeID);//上线公司
            }
            return companyList;
        }

        /// <summary>
        /// 查询员工可以查看的门店
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public DataTable QueryEmployeeShopByEmplyeeNew(int employeeId, string searchKeyWords)
        {
            AuthorityManager authorityMan = new AuthorityManager();
            DataTable dtEmployeeShop = new DataTable();
            DataView dvEmployeeShop = new DataView();
            RoleOperate roleOperate = new RoleOperate();
            ShopManager shopManager = new ShopManager();
            DataTable dtQueryAllCompamnyInfo = roleOperate.QuerySpecialAuthorityInfoByEmployeeID(employeeId, (int)VASpecialAuthority.CHECK_ALL_COMPANY);//查看所有公司权限
            //注：具备查看所有公司或者查看所有上线公司中的一种特殊权限，则对应的门店列表显示不去查询employee对应的表信息
            if (dtQueryAllCompamnyInfo.Rows.Count > 0)//第一层权限查询
            {
                //dtEmployeeShop = shopManager.SelectShop();//所有门店
                //dvEmployeeShop = dtEmployeeShop.DefaultView;
                dtEmployeeShop = shopManager.SelectShopNew(0, searchKeyWords);
            }
            else
            {
                DataTable dtQueryOnlineCompamnyInfo = roleOperate.QuerySpecialAuthorityInfoByEmployeeID(employeeId, (int)VASpecialAuthority.CHECK_PREORDER_ONLINE_COMPANY);//查看所有上线公司权限
                if (dtQueryOnlineCompamnyInfo.Rows.Count > 0)//第二层权限
                {
                    //dtEmployeeShop = shopManager.SelectShop();//所有上线门店
                    //dvEmployeeShop = dtEmployeeShop.DefaultView;
                    //dvEmployeeShop.RowFilter = "isHandle=1";
                    dtEmployeeShop = shopManager.SelectShopNew(1, searchKeyWords);
                }
                else//第三层权限，分配
                {
                    dtEmployeeShop = authorityMan.SelectEmployeeShopNew(employeeId, searchKeyWords);
                    //dtEmployeeShop = authorityMan.SelectEmployeeShop();
                    //dvEmployeeShop = dtEmployeeShop.DefaultView;
                    //dvEmployeeShop.RowFilter = "employeeID = '" + employeeId + "'";
                }
            }
            dvEmployeeShop = dtEmployeeShop.DefaultView;
            return dvEmployeeShop.ToTable();
        }
    }
}
