using System;

/*----------------------------------------------------------------
// 功能描述：点单评价查询对象，系统自动生成，请勿手动修改
// 创建标识：2015/3/4 14:44:13
//
// 修改标识：
// 修改描述：
----------------------------------------------------------------*/

namespace VAGastronomistMobileApp.Model.QueryObject
{
    public  class PreorderEvaluationQueryObject
    {
        /// <summary>
        /// PreorderEvaluationId
        /// </summary>
        public long? PreorderEvaluationId { get; set; }
        /// <summary>
        /// 点单编号
        /// </summary>
        public long? PreOrder19dianId { get; set; }
        /// <summary>
        /// 店铺编号
        /// </summary>
        public int? ShopId { get; set; }
        /// <summary>
        /// 用户编号
        /// </summary>
        public long? CustomerId { get; set; }
        /// <summary>
        /// 评价值
        /// </summary>
        public int? EvaluationValue { get; set; }
        /// <summary>
        /// 评价值起始
        /// </summary>
        public int? EvaluationValueFrom { get; set; }
        /// <summary>
        /// 评价值截至
        /// </summary>
        public int? EvaluationValueTo { get; set; }
        /// <summary>
        /// 点单评价
        /// </summary>
        public string EvaluationContent { get; set; }
        /// <summary>
        /// 评价内容模糊查询
        /// </summary>
        public string EvaluationContentFuzzy { get; set; }
        
        /// <summary>
        /// 评价时间
        /// </summary>
        public DateTime? EvaluationTime { get; set; }
        /// <summary>
        /// 评价时间起始
        /// </summary>
        public DateTime? EvaluationTimeFrom { get; set; }
        /// <summary>
        /// 评价时间截至
        /// </summary>
        public DateTime? EvaluationTimeTo { get; set; }
        /// <summary>
        /// 评价等级
        /// </summary>
        public int? EvaluationLevel { get; set; }
    }
    public enum PreorderEvaluationOrderColumn
    {
        PreorderEvaluationId,
        PreOrder19dianId,
        ShopId,
        CustomerId,
        EvaluationValue,
        EvaluationContent,
        EvaluationTime,
        EvaluationLevel,
    }
}