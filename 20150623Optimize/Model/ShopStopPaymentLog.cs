using System;
using VAGastronomistMobileApp.Model.Interface; 

/*----------------------------------------------------------------
// 功能描述：门店暂停支付记录实体，系统自动生成，请勿手动修改
// 创建标识：2015/3/11 15:27:18
//
// 修改标识：
// 修改描述：
----------------------------------------------------------------*/

namespace VAGastronomistMobileApp.Model
{
    /// <summary>
    /// 门店暂停支付记录
    /// </summary>
    public class ShopStopPaymentLog:IShopStopPaymentLog
    {
        /// <summary>
        /// 门店暂停支付记录
        /// </summary>
        public int  ShopStopPaymentLogId { get; set; }
        /// <summary>
        /// 门店暂停支付记录
        /// </summary>
        public int  ShopId { get; set; }
        /// <summary>
        /// 门店暂停支付记录
        /// </summary>
        public DateTime  StopPaymentTime { get; set; }
        /// <summary>
        /// 门店暂停支付记录
        /// </summary>
        public DateTime?  StartPaymentTime { get; set; }
        /// <summary>
        /// 门店暂停支付记录
        /// </summary>
        public int  State { get; set; }
        /// <summary>
        /// 门店暂停支付记录
        /// </summary>
        public DateTime  CreateTime { get; set; }
        /// <summary>
        /// 门店暂停支付记录
        /// </summary>
        public long  CreatedBy { get; set; }
        /// <summary>
        /// 门店暂停支付记录
        /// </summary>
        public long  LastUpdatedBy { get; set; }
        /// <summary>
        /// 门店暂停支付记录
        /// </summary>
        public DateTime  LastUpdatedTime { get; set; }
        /// <summary>
        /// 门店暂停支付记录
        /// </summary>
        public string  Remark { get; set; }
    }
}