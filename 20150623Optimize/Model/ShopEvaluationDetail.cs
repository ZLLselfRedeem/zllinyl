using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    /// <summary>
    /// 店铺评价明细
    /// </summary>
    public class ShopEvaluationDetail
    {
        public int ShopEvaluationDetailId { get; set; }
        public int EvaluationValue { get; set; }
        public int EvaluationCount { get; set; }
        public int ShopId { get; set; }
    }

    public class ShopEvaluationDetailQueryObject
    {
        public int? ShopEvaluationDetailId { get; set; }
        public int? EvaluationValue { get; set; }
        public int? EvaluationCount { get; set; }
        public int? ShopId { get; set; }
    }
}
