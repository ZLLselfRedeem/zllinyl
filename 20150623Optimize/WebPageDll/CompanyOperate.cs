using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Transactions;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL;
using System.Threading;
using System.Configuration;
//
//  Copyright 2011 View Alloc inc. All rights reserved.
//  Created by Jason Xiao on 2012-04-10.
//
namespace VAGastronomistMobileApp.WebPageDll
{
    public class CompanyOperate
    {
        private readonly CompanyManager companyMan = new CompanyManager();
        /// <summary>
        /// 新增公司信息
        /// </summary>
        /// <returns></returns>
        public int AddCompany(CompanyInfo company)
        {
            int newCompanyId = 0;
            //CompanyManager companyManager = new CompanyManager();
            DataTable dtCompany = companyMan.SelectCompany();
            DataView dvCompany = dtCompany.DefaultView;
            dvCompany.RowFilter = "companyName = '" + company.companyName + "'";
            if (dvCompany.Count > 0 || company.companyName == "" || company.companyName == null)
            {//如果所加公司信息的名称为空，或者公司信息表中已有该名称的公司，则直接返回false

            }
            else
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    newCompanyId = companyMan.InsertCompany(company);
                    if (newCompanyId > 0)
                    {
                        scope.Complete();
                    }
                }
            }
            return newCompanyId;
        }
        /// <summary>
        /// 收银宝添加详情公司信息
        /// </summary>
        /// <param name="company"></param>
        /// <returns></returns>
        public int Syb_AddCompany(CompanyInfo company)
        {
            return companyMan.Syb_InsertCompany(company);
        }
        /// <summary>
        /// 删除公司信息
        /// </summary>
        /// <returns></returns>
        public bool RemoveCompany(int companyID)
        {
            //CompanyManager companyManager = new CompanyManager();
            DataTable dtCompany = companyMan.SelectCompany(companyID);
            if (dtCompany.Rows.Count == 1)
            {//判断此companyID是否存在，是则删除
                using (TransactionScope scope = new TransactionScope())
                {
                    if (companyMan.DeleteCompany(companyID))
                    {//删除成功则返回true
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
        /// 修改公司基本信息
        /// </summary>
        /// <returns></returns>
        public bool ModifyCompany(CompanyInfo company)
        {
            DataTable dtCompany = companyMan.SelectCompany(company.companyID);
            if (dtCompany.Rows.Count == 1)
            {//判断此companyID是否存在，是则修改
                using (TransactionScope scope = new TransactionScope())
                {
                    if (company.acpp > 0)
                    {
                        if (companyMan.UpdateCompany(company))
                        {//修改成功则返回true
                            scope.Complete();
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        if (companyMan.UpdateCurrentCompany(company))
                        {//修改成功则返回true
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
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 修改公司支付给友络的佣金和无忧退款时间（小时）
        /// </summary>
        /// <param name="company"></param>
        /// <returns></returns>
        public bool ModifyCompanyCommissionAndRefundHour(CompanyInfo company)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                if (companyMan.UpdateCompanyCommissionAndRefundHour(company))
                {//修改成功则返回true
                    scope.Complete();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        /// <summary>
        /// 获取公司默认的菜谱
        /// </summary>
        /// <param name="company"></param>
        public bool SetCompanyDefaultMenu(int company, int menuId)
        {
            return companyMan.SetCompanyDefaultMenu(company, menuId);
        }
        /// <summary>
        /// 查询所有公司信息
        /// </summary>
        /// <returns></returns>
        public DataTable QueryCompany()
        {
            //CompanyManager companyManager = new CompanyManager();
            return companyMan.SelectCompany();
        }
        /// <summary>
        /// 查询这个公司是否存在
        /// </summary>
        /// <param name="companyName"></param>
        /// <returns></returns>
        public bool CompanyNameValid(string companyName)
        {
            //CompanyManager companyManager = new CompanyManager();
            DataTable dtCompany = companyMan.SelectCompany();
            DataView dvCompany = dtCompany.DefaultView;
            dvCompany.RowFilter = "companyName = '" + companyName + "'";
            if (dvCompany.Count > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        /// <summary>
        /// 根据公司编号查询对应的公司信息
        /// </summary>
        /// <returns></returns>
        public CompanyInfo QueryCompany(int companyID)
        {
            //CompanyManager companyManager = new CompanyManager();
            CompanyInfo company = new CompanyInfo();
            DataTable dtCompany = companyMan.SelectCompany(companyID);
            if (1 == dtCompany.Rows.Count)
            {
                company.companyID = companyID;
                company.companyName = Common.ToString(dtCompany.Rows[0]["companyName"]);
                company.companyAddress = Common.ToString(dtCompany.Rows[0]["companyAddress"]);
                company.companyTelePhone = Common.ToString(dtCompany.Rows[0]["companyTelePhone"]);
                company.companyLogo = Common.ToString(dtCompany.Rows[0]["companyLogo"]);
                company.contactPerson = Common.ToString(dtCompany.Rows[0]["contactPerson"]);
                company.contactPhone = Common.ToString(dtCompany.Rows[0]["contactPhone"]);
                company.companyImagePath = Common.ToString(dtCompany.Rows[0]["companyImagePath"]);
                company.freeRefundHour = Common.ToDouble(dtCompany.Rows[0]["freeRefundHour"]);
                company.viewallocCommissionType = (VACommissionType)Common.ToInt32(dtCompany.Rows[0]["viewallocCommissionType"]);
                company.viewallocCommissionValue = Common.ToDouble(dtCompany.Rows[0]["viewallocCommissionValue"]);
                company.sinaWeiboName = Common.ToString(dtCompany.Rows[0]["sinaWeiboName"]);
                company.qqWeiboName = Common.ToString(dtCompany.Rows[0]["qqWeiboName"]);

                company.wechatPublicName = Common.ToString(dtCompany.Rows[0]["wechatPublicName"]);
                company.acpp = Common.ToDouble(dtCompany.Rows[0]["acpp"]);
                company.companyDescription = Common.ToString(dtCompany.Rows[0]["companyDescription"]);

                company.ownedCompany = Common.ToString(dtCompany.Rows[0]["ownedCompany"]);
            }
            return company;
        }
        /// <summary>
        /// new客户端根据城市编号查询公司信息
        /// </summary>
        /// <param name="companyListRequest"></param>
        /// <returns></returns>
        public VACompanyListResponse ClientQueryCompanyByCityId(VACompanyListRequest companyListRequest)
        {
            VACompanyListResponse companyListResponse = new VACompanyListResponse();
            companyListResponse.type = VAMessageType.COMPANY_LIST_RESPONSE;
            companyListResponse.cookie = companyListRequest.cookie;
            companyListResponse.uuid = companyListRequest.uuid;
            companyListResponse.cityId = companyListRequest.cityId;
            CheckCookieAndMsgtypeInfo checkResult = Common.CheckCookieAndMsgtype(companyListRequest.cookie,
                companyListRequest.uuid, (int)companyListRequest.type, (int)VAMessageType.COMPANY_LIST_REQUEST);
            if (checkResult.result == VAResult.VA_OK)
            {
                companyListResponse.result = VAResult.VA_OK;
                CustomerManager customerMan = new CustomerManager();
                long customerId = Common.ToInt64(checkResult.dtCustomer.Rows[0]["CustomerID"]);
                DataTable dtCustomerFavoriteCompany = customerMan.SelectCustomerFavoriteCompany(customerId);
                DataTable dtCityCompany = companyMan.SelectCompanyByCityId(companyListRequest.cityId, companyListRequest.companyId);
                companyListResponse.companyList = Common.FillBrand(dtCityCompany, companyListRequest.cityId,
                    (VAAppType)Common.ToInt32(checkResult.dtCustomer.Rows[0]["appType"]), dtCustomerFavoriteCompany);
                if (companyListRequest.companyId == 0)
                {
                    DataTable dtCityCompanyBannerold = companyMan.SelectCompanyBannerByCityId(companyListRequest.cityId, VAAdvertisementClassify.INDEX_AD);
                    //随机banner 随机table实现？
                    DataTable dtCityCompanyBanner = dtCityCompanyBannerold.Clone();
                    foreach (DataRow row in dtCityCompanyBannerold.Rows)
                    {
                        DataRow drn = dtCityCompanyBanner.NewRow();
                        drn.ItemArray = row.ItemArray;
                        Random random = new Random();
                        dtCityCompanyBanner.Rows.InsertAt(drn, random.Next(dtCityCompanyBanner.Rows.Count + 1));//这里注意随机出来的数字是否会超界
                    }
                    companyListResponse.companyBannerList = Common.FillBrandBanner(dtCityCompanyBanner, companyListRequest.cityId,
                        (VAAppType)Common.ToInt32(checkResult.dtCustomer.Rows[0]["appType"]));
                    string advertisementConnAdColumnId = string.Empty;
                    for (int i = 0; i < dtCityCompanyBanner.Rows.Count; i++)
                    {
                        if (i == 0)
                        {
                            advertisementConnAdColumnId += "(";
                        }
                        advertisementConnAdColumnId += Common.ToString(dtCityCompanyBanner.Rows[i]["advertisementConnAdColumnId"]);
                        if (i != dtCityCompanyBanner.Rows.Count - 1)
                        {
                            advertisementConnAdColumnId += ",";
                        }
                        else
                        {
                            advertisementConnAdColumnId += ")";
                        }
                    }
                    AdvertisementManager advertisementMan = new AdvertisementManager();
                    using (TransactionScope scope = new TransactionScope())
                    {
                        advertisementMan.UpdateAdvertisementDisplayCount(advertisementConnAdColumnId, 1);
                        scope.Complete();
                    }
                }
            }
            else
            {
                companyListResponse.result = checkResult.result;
            }
            #region 加入刷新公司列表日志记录

            Thread thread = new Thread(Excuit);
            thread.IsBackground = true;
            thread.Start(VAInvokedAPIType.API_REFRESH_COMPANY_LIST);
            #endregion
            return companyListResponse;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public void Excuit(object value)
        {
            StatisticalStatementOperate sso = new StatisticalStatementOperate();
            sso.InsertInvokedAPILog((int)value);
        }
        /// <summary>
        /// 查询所有公司名称和Id
        /// </summary>
        /// <returns></returns>
        public List<VAEmployeeCompany> QueryCompanyNameAndId()
        {
            DataTable dt = companyMan.SelectCompanyNameAndId();
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
        /// 查询公司信息（不包含没有门店的公司）
        /// </summary>
        /// <returns></returns>
        public List<VAEmployeeCompany> QueryCompanyNameAndIdRemovenNotHaveShop()
        {
            DataTable dt = companyMan.SelectCompanyNameAndIdRemovenNotHaveShop();
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
        public List<VAEmployeeCompany> QueryCompanyNameAndIdByAuthority(int employeeID)
        {
            DataTable dt = companyMan.SelectCompanyNameAndIdByAuthority(employeeID);
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
        #region 公司官网模块
        /// <summary>
        /// 公司官网查询最近上线公司的信息
        /// </summary>
        /// <param name="cityId"></param>
        /// <param name="logoImageCount"></param>
        /// <returns></returns>
        public DataTable QueryCompanyLogoAndName(int cityId, int logoImageCount)
        {
            return companyMan.SelectCompanyLogoAndName(cityId, logoImageCount);
        }
        #endregion
        /// <summary>
        /// 根据门店编号查询公司编号（add by wangc 20140321）
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public int GetCompanyId(int shopId)
        {
            return companyMan.GetCompanyId(shopId);
        }
    }
}
