using System;

/*----------------------------------------------------------------
// 功能描述：门店暂停支付记录查询对象，系统自动生成，请勿手动修改
// 创建标识：2015/3/10 16:27:28
//
// 修改标识：
// 修改描述：
----------------------------------------------------------------*/

namespace VAGastronomistMobileApp.Model.QueryObject
{
    public  class ShopStopPaymentLogQueryObject
    {
        /// <summary>
        /// ShopStopPaymentLogId
        /// </summary>
        public int? ShopStopPaymentLogId { get; set; }
        /// <summary>
        /// 门店编号
        /// </summary>
        public int? ShopId { get; set; }
        /// <summary>
        /// 暂停支付时间
        /// </summary>
        public DateTime? StopPaymentTime { get; set; }
        /// <summary>
        /// 暂停支付时间起始
        /// </summary>
        public DateTime? StopPaymentTimeFrom { get; set; }
        /// <summary>
        /// 暂停支付时间截至
        /// </summary>
        public DateTime? StopPaymentTimeTo { get; set; }
        /// <summary>
        /// 开启支付时间
        /// </summary>
        public DateTime? StartPaymentTime { get; set; }
        /// <summary>
        /// 开启支付时间起始
        /// </summary>
        public DateTime? StartPaymentTimeFrom { get; set; }
        /// <summary>
        /// 开启支付时间截至
        /// </summary>
        public DateTime? StartPaymentTimeTo { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int? State { get; set; }
        /// <summary>
        /// CreateTime
        /// </summary>
        public DateTime? CreateTime { get; set; }
        /// <summary>
        /// CreateTime起始
        /// </summary>
        public DateTime? CreateTimeFrom { get; set; }
        /// <summary>
        /// CreateTime截至
        /// </summary>
        public DateTime? CreateTimeTo { get; set; }
        /// <summary>
        /// CreatedBy
        /// </summary>
        public long? CreatedBy { get; set; }
        /// <summary>
        /// LastUpdatedBy
        /// </summary>
        public long? LastUpdatedBy { get; set; }
        /// <summary>
        /// LastUpdatedTime
        /// </summary>
        public DateTime? LastUpdatedTime { get; set; }
        /// <summary>
        /// LastUpdatedTime起始
        /// </summary>
        public DateTime? LastUpdatedTimeFrom { get; set; }
        /// <summary>
        /// LastUpdatedTime截至
        /// </summary>
        public DateTime? LastUpdatedTimeTo { get; set; }
        /// <summary>
        /// 门店暂停支付记录
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 备注模糊查询
        /// </summary>
        public string RemarkFuzzy { get; set; }
        
    }
    public enum ShopStopPaymentLogOrderColumn
    {
        ShopStopPaymentLogId,
        ShopId,
        StopPaymentTime,
        StartPaymentTime,
        State,
        CreateTime,
        CreatedBy,
        LastUpdatedBy,
        LastUpdatedTime,
        Remark,
    }
}