using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Transactions;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL;
//
//  Copyright 2013 View Alloc inc. All rights reserved.
//  Created by Jason Xiao on 2013-06-06.
//
namespace VAGastronomistMobileApp.WebPageDll
{
    public class VipOperate
    {
        private readonly VipManager vipMan = new VipManager();
        /// <summary>
        /// 开启会员奖励
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public bool OpenCompanyVip(int companyId)
        {
            DataTable dtCompanyVip = vipMan.SelectCompanyVip(companyId);
            if (dtCompanyVip.Rows.Count > 0)
            {
                return false;
            }
            else
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    bool result = true;
                    for (int i = 0; i < 4; i++)
                    {//开启时自动增加4种Vip等级
                        CompanyVipInfo companyVip = new CompanyVipInfo();
                        companyVip.companyId = companyId;
                        companyVip.sequence = i + 1;
                        companyVip.status = 1;
                        switch (i)
                        {
                            case 0:
                                {
                                    companyVip.discount = 1;
                                    companyVip.name = "VIP0";
                                    companyVip.nextRequirement = 1;
                                }
                                break;
                            case 1:
                                {
                                    companyVip.discount = 0.95;
                                    companyVip.name = "VIP1";
                                    companyVip.nextRequirement = 5;
                                }
                                break;
                            case 2:
                                {
                                    companyVip.discount = 0.92;
                                    companyVip.name = "VIP2";
                                    companyVip.nextRequirement = 8;
                                }
                                break;
                            case 3:
                                {
                                    companyVip.discount = 0.88;
                                    companyVip.name = "VIP3";
                                    companyVip.nextRequirement = 99999;
                                }
                                break;
                        }
                        if (vipMan.InsertCompanyVip(companyVip) == 0)
                        {
                            result = false;
                            break;
                        }
                    }
                    if (result)
                    {
                        scope.Complete();
                    }
                    return result;
                }
            }
        }
        /// <summary>
        /// 修改公司Vip政策
        /// </summary>
        /// <param name="companyVip"></param>
        /// <returns></returns>
        public bool ModifyCompanyVip(CompanyVipInfo companyVip)
        {
            bool result = false;
            using (TransactionScope scope = new TransactionScope())
            {
                if (vipMan.UpdateCompanyVip(companyVip))
                {
                    result = true;
                    scope.Complete();
                }
            }
            return result;
        }
        /// <summary>
        /// 查询指定公司的会员政策
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public DataTable QueryCompanyVip(int companyId)
        {
            return vipMan.SelectCompanyVip(companyId);
        }
        /// <summary>
        /// 关闭当前公司的vip
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public bool ClosedCompanyVip(int companyId)
        {
            return vipMan.DeleteCompanyVipStatus(companyId);
        }
        /// <summary>
        /// 获取当前平台的VIP等级信息
        /// </summary>
        /// <returns></returns>
        public DataTable QueryViewAllocPlatformVipInfo()
        {
            ViewallocInfoManager va = new ViewallocInfoManager();
            return va.SelectViewAllocPlatformVipInfo();
        }
    }
}
