using System;
using VAGastronomistMobileApp.Model.Interface; 

/*----------------------------------------------------------------
// 功能描述：Preorder19DianLineV实体接口，系统自动生成，请勿手动修改
// 创建标识：2015/4/30 13:34:36
//
// 修改标识：
// 修改描述：
----------------------------------------------------------------*/

namespace VAGastronomistMobileApp.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class Preorder19DianLineV:IPreorder19DianLineV
    {
        /// <summary>
        /// Preorder19DianLineId
        /// </summary>
        public long  Preorder19DianLineId { get; set; }
        /// <summary>
        /// Preorder19DianId
        /// </summary>
        public long  Preorder19DianId { get; set; }
        /// <summary>
        /// CustomerId
        /// </summary>
        public long  CustomerId { get; set; }
        /// <summary>
        /// PayType
        /// </summary>
        public int  PayType { get; set; }
        /// <summary>
        /// PayAccount
        /// </summary>
        public string  PayAccount { get; set; }
        /// <summary>
        /// Amount
        /// </summary>
        public double  Amount { get; set; }
        /// <summary>
        /// CreateTime
        /// </summary>
        public DateTime  CreateTime { get; set; }
        /// <summary>
        /// Remark
        /// </summary>
        public string  Remark { get; set; }
        /// <summary>
        /// State
        /// </summary>
        public int  State { get; set; }
        /// <summary>
        /// Uuid
        /// </summary>
        public string  Uuid { get; set; }
        /// <summary>
        /// RefundAmount
        /// </summary>
        public double  RefundAmount { get; set; }
        /// <summary>
        /// preOrderTime
        /// </summary>
        public DateTime?   PreOrderTime { get; set; }
        /// <summary>
        /// status
        /// </summary>
        public byte?   Status { get; set; }
        /// <summary>
        /// cityID
        /// </summary>
        public int?   CityId { get; set; }

        /// <summary>
        /// ShopId
        /// </summary>
        public int ShopID { get; set; } 
    }
}