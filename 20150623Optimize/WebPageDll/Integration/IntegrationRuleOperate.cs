using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL;

namespace VAGastronomistMobileApp.WebPageDll
{
    public class IntegrationRuleOperate
    {
        private IntegrationRuleManager irm = new IntegrationRuleManager();
        /// <summary>
        /// 插入记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Insert(IntegrationRule model)
        {
            return irm.Insert(model);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Update(IntegrationRule model)
        {
            return irm.Update(model);
        }

        /// <summary>
        /// 修改状态，删除为-1
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateStatus(IntegrationRule model)
        {
            return irm.UpdateStatus(model);
        }

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="RuleType"></param>
        /// <returns></returns>
        public DataTable Integrations(Guid RuleType)
        {
            return irm.Integrations(RuleType);
        }

        /// <summary>
        /// 查询单条积分细则
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public DataTable IntegrationDetail(Guid ID)
        {
            return irm.IntegrationDetail(ID);
        }

        /// <summary>
        /// 查询多条细则in条件
        /// </summary>
        /// <param name="strRule"></param>
        /// <returns></returns>
        public DataTable Integrations(string strRule)
        {
            return irm.Integrations(strRule);
        }

        /// <summary>
        /// 查询积分规则类型
        /// </summary>
        /// <returns></returns>
        public DataTable RuleTypes()
        {
            return irm.RuleTypes();
        }

        /// <summary>
        /// 查询对应的积分规则类型下的细则
        /// </summary>
        /// <param name="RuleType"></param>
        /// <returns></returns>
        public DataTable IntegrationRules(Guid RuleType)
        {
            return irm.IntegrationRules(RuleType);
        }

        /// <summary>
        /// 查询总积分
        /// </summary>
        /// <param name="CityID">城市ID</param>
        /// <param name="RuleID">规则细则ID</param>
        /// <param name="RuleTypeID">规则类型ID</param>
        /// <returns></returns>
        public DataTable SumIntegration(int CityID, Guid RuleID, Guid RuleTypeID, DateTime BeginDate, DateTime EndDate)
        {
            return irm.SumIntegration(CityID, RuleID, RuleTypeID, BeginDate, EndDate);
        }
    }
}
