using System; 
//
// 功能描述：门店暂停支付记录实体接口，系统自动生成，请勿手动修改
// 创建标识：2015/3/10 16:27:28
//
// 修改标识：
// 修改描述：

namespace VAGastronomistMobileApp.Model.Interface
{
    /// <summary>
    /// 门店暂停支付记录
    /// </summary>
    public interface IShopStopPaymentLog
    {
        /// <summary>
        ///  
        /// </summary>
        int  ShopStopPaymentLogId { get; set; }
        /// <summary>
        ///  
        /// </summary>
        int  ShopId { get; set; }
        /// <summary>
        ///  
        /// </summary>
        DateTime  StopPaymentTime { get; set; }
        /// <summary>
        ///  
        /// </summary>
        DateTime?  StartPaymentTime { get; set; }
        /// <summary>
        ///  
        /// </summary>
        int  State { get; set; }
        /// <summary>
        ///  
        /// </summary>
        DateTime  CreateTime { get; set; }
        /// <summary>
        ///  
        /// </summary>
        long  CreatedBy { get; set; }
        /// <summary>
        ///  
        /// </summary>
        long  LastUpdatedBy { get; set; }
        /// <summary>
        ///  
        /// </summary>
        DateTime  LastUpdatedTime { get; set; }
        /// <summary>
        ///  
        /// </summary>
        string  Remark { get; set; }
    }
}