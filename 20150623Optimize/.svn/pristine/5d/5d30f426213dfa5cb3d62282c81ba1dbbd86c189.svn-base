using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    /// <summary>
    /// 门店举报Model
    /// </summary>
    public class ShopReport
    {
        public long ShopReportId { get; set; }
        public long CustomId { get; set; }
        public DateTime ReportTime { get; set; }
        public int ShopId { get; set; }
        public int ReportValue { get; set; }
    }

    public class ShopReportInfo
    {
        public int ReportValue { get; set; }
        public string ReportDesc { get; set; }
    }
    /// <summary>
    /// 门店具备信息枚举
    /// </summary>
    public enum ShopReportEnum
    {
        [Description("商户已关")]
        商户已关 = 1,
        [Description("商户信息错误")]
        商户信息错误 = 2,
        [Description("商户电话错误")]
        商户电话错误 = 3,
        [Description("地图信息错误")]
        地图信息错误 = 4
    }
}
