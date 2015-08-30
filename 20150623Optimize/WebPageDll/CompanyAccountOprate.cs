using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.SQLServerDAL;
using VAGastronomistMobileApp.Model;
using System.Data;

namespace VAGastronomistMobileApp.WebPageDll
{
    public class CompanyAccountOprate
    {
        /// <summary>
        /// 公司门店银行帐号业务逻辑
        /// </summary>
        CompanyAccountManager manager = new CompanyAccountManager();
        /// <summary>
        /// 新增一条账户记录
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public int QueryNewAccount(CompanyAccountInfo account)
        {
            DataTable dt = manager.SelectAccountByAccountNum(account.accountNum, account.companyId);
            if (dt.Rows.Count == 0)
            {
                bool result = manager.InsertNewAccountInfo(account);
                if (result)
                {
                    return 1;
                }
                else
                {
                    return 2;
                }
            }
            else
            {
                return 0;
            }
        }
        /// <summary>
        /// 查询账户信息
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public DataTable QueryAccountByCompanyId(int companyId)
        {
            return manager.SelectAccountByCompanyId(companyId);
        }
        /// <summary>
        /// 通过公司查询账户信息 add by wangc 20140516
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <returns></returns>
        public List<CompanyAccountInfo> QueryAccountByListCompanyId(int companyId)
        {
            return manager.SelectAccountListByCompanyId(companyId);
        }
        /// <summary>
        /// 通过公司查询账户信息
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public DataTable QueryAccountNameAndAccountNumByCompanyId(int companyId)
        {
            return manager.SelectAccountNameAndAccountNumByCompanyId(companyId);
        }
        /// <summary>
        /// 账户id查询账户信息
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public DataTable QueryAccountById(int accountId)
        {
            return manager.SelectAccountById(accountId);
        }
        /// <summary>
        /// 更新账户信息
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool QueryUpdateAccount(long identity_Id, string accountNum, string bankName, string remark, string accountName)
        {
            return manager.UpdateAccountInfo(identity_Id, accountNum, bankName, remark, accountName);
        }
        /// <summary>
        /// 更新一条账户记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool ModifyAccountInfo(CompanyAccountInfo model,DataTable dt)
        {
            return manager.UpdateAccountInfo(model, dt);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="identity_Id"></param>
        /// <returns></returns>
        public bool DeleteAccountInfo(long identity_Id)
        {
            return manager.DeleteAccountInfo(identity_Id);
        }

        /// <summary>
        /// 查询门店银行帐户信息
        /// </summary>
        /// <param name="shopid"></param>
        /// <returns></returns>
        public CompanyAccountInfo GetAccountInfo(int shopid)
        {
            return manager.GetAccountInfo(shopid);
        }

        /// <summary>
        /// 新增一条账户记录
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public int InsertAccount(CompanyAccountInfo account)
        {
            //DataTable dt = manager.SelectAccountByAccountNum(account.accountNum, account.companyId);
            //if (dt.Rows.Count == 0)
            //{
               
            //}

            int identity_Id = manager.InsertAccountInfo(account);
            if (identity_Id > 0)
            {
                return identity_Id;
            }

            return 0;
        }
    }
}
