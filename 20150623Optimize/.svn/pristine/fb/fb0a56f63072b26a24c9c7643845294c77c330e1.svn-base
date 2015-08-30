using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VAGastronomistMobileApp.WebPageDll.Adjunction
{
    [Serializable]
    public class IntegralOperate
    {
        /// <summary>
        /// 老架构区分接口的type
        /// </summary>
        [JsonProperty("type")]
        public int Type { get; set; }
        /// <summary>
        /// 新架构接口地址
        /// </summary>
        [JsonProperty("uxianAppAddress")]
        public string UxianAppAddress { get; set; }
        /// <summary>
        /// 积分系统接口地址
        /// </summary>
        [JsonProperty("requestAddress")]
        public string RequestAddress { get; set; }

        /// <summary>
        /// 业务类别ID
        /// </summary>
        [JsonProperty("ruleTypeId")]
        public Guid RuleTypeId { get; set; }
    }


    public class IntegralOperateListsResponse
    {
        public List<IntegralOperate> IntegralOperates { get; set; }
    }

    public class IntegralWebSend
    {
        public int type { get; set; }
        public Dictionary<string, object> parameters { get; set; }
    }

    public class IntegralInterfaceSend
    {
        public object requestParam { get; set; }
        public string requestJson { get; set; }
    }
}
