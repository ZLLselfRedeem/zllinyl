using System;

/*----------------------------------------------------------------
// 功能描述：ShopHandleLog查询对象，系统自动生成，请勿手动修改
// 创建标识：2015/3/16 17:20:47
//
// 修改标识：
// 修改描述：
----------------------------------------------------------------*/

namespace VAGastronomistMobileApp.Model.QueryObject
{
    public  class ShopHandleLogQueryObject
    {
        /// <summary>
        /// Id
        /// </summary>
        public long? Id { get; set; }
        /// <summary>
        /// ShopName
        /// </summary>
        public string ShopName { get; set; }
        /// <summary>
        /// ShopName模糊查询
        /// </summary>
        public string ShopNameFuzzy { get; set; }
        
        /// <summary>
        /// ShopId
        /// </summary>
        public int? ShopId { get; set; }
        /// <summary>
        /// EmployeeId
        /// </summary>
        public int? EmployeeId { get; set; }
        /// <summary>
        /// EmployeeName
        /// </summary>
        public string EmployeeName { get; set; }
        /// <summary>
        /// EmployeeName模糊查询
        /// </summary>
        public string EmployeeNameFuzzy { get; set; }
        
        /// <summary>
        /// HandleStatus
        /// </summary>
        public int? HandleStatus { get; set; }
        /// <summary>
        /// HandleDesc
        /// </summary>
        public string HandleDesc { get; set; }
        /// <summary>
        /// HandleDesc模糊查询
        /// </summary>
        public string HandleDescFuzzy { get; set; }
        
        /// <summary>
        /// OperateTime
        /// </summary>
        public DateTime? OperateTime { get; set; }
        /// <summary>
        /// OperateTime起始
        /// </summary>
        public DateTime? OperateTimeFrom { get; set; }
        /// <summary>
        /// OperateTime截至
        /// </summary>
        public DateTime? OperateTimeTo { get; set; }
        /// <summary>
        /// CityId
        /// </summary>
        public int? CityId { get; set; }
    }
    public enum ShopHandleLogOrderColumn
    {
        Id,
        ShopName,
        ShopId,
        EmployeeId,
        EmployeeName,
        HandleStatus,
        HandleDesc,
        OperateTime,
        CityId,
    }
}