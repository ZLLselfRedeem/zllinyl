using System; 
//
// 功能描述：QrCode实体接口，系统自动生成，请勿手动修改
// 创建标识：2015/5/20 11:07:25
//
// 修改标识：
// 修改描述：

namespace VAGastronomistMobileApp.Model.Interface
{
    /// <summary>
    /// QrCode
    /// </summary>
    public interface IQrCode
    {
        /// <summary>
        /// Id
        /// </summary>
        int  Id { get; set; }
        /// <summary>
        /// Name
        /// </summary>
        string  Name { get; set; }
        /// <summary>
        /// Type
        /// </summary>
        int  Type { get; set; }
        /// <summary>
        /// State
        /// </summary>
        int  State { get; set; }
        /// <summary>
        /// Remark
        /// </summary>
        string  Remark { get; set; }
        /// <summary>
        /// CreatedBy
        /// </summary>
        long  CreatedBy { get; set; }
        /// <summary>
        /// CreateTime
        /// </summary>
        DateTime  CreateTime { get; set; }
        /// <summary>
        /// LastUpdatedBy
        /// </summary>
        long  LastUpdatedBy { get; set; }
        /// <summary>
        /// LastUpdateTime
        /// </summary>
        DateTime  LastUpdateTime { get; set; }
        /// <summary>
        /// LinkKey
        /// </summary>
        long  LinkKey { get; set; }
        /// <summary>
        /// CityId
        /// </summary>
        int  CityId { get; set; }

          long Pv { get; set; }

          long Uv { get; set; }
    }
}