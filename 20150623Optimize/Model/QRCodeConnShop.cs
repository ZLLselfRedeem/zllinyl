using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    /// <summary>
    /// 店铺的二维码信息
    /// 20140116 jinyanni
    /// </summary>
    public class QRCodeConnShop
    {
        /// <summary>
        /// 店铺与二维码关系ID
        /// </summary>
        public long shopQRId { get; set; }
        
        /// <summary>
        /// 店铺ID 
        /// </summary>
        public int shopId { get; set; }

        /// <summary>
        /// 类别ID
        /// </summary>
        public int typeId { get; set; }

        /// <summary>
        /// 二维码图片存放路径
        /// </summary>
        public string QRCodeImage { get; set; }

        /// <summary>
        /// 数据状态
        /// </summary>
        public int status { get; set; }

        /// <summary>
        /// 二维码图片名称
        /// </summary>
        public string imageName { get; set; }
    }
}
