using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    /// <summary> 
    /// FileName: ProvinceInfo.cs 
    /// CLRVersion: 4.0.30319.269 
    /// Author: TDQ 
    /// Corporation:杭州友络科技有限公司
    /// Description: 
    /// DateTime: 2012-05-15 15:12:10 
    /// </summary>
    [Serializable]
    [DataContract]
    public class Province
    {
        [DataMember]
        /// <summary>
        /// 省ID
        /// </summary>
        public int provinceID { get; set; }
        [DataMember]
        /// <summary>
        /// 省名称
        /// </summary>
        public string provinceName { get; set; }

    }
}
