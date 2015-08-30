using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL;
using System.Data;

namespace VAGastronomistMobileApp.WebPageDll
{
    /// <summary>
    /// 下载页面访问量统计逻辑层
    /// 20140117 jinyanni
    /// </summary>
    public class QRCodePVOperate
    {
        QRCodePVManage _QRCodePV = new QRCodePVManage();

        /// <summary>
        /// 新增访问下载页面的记录
        /// </summary>
        /// <param name="QRCodePageView"></param>
        /// <returns></returns>
        public bool InsertQRCodePV(QRCodePageView QRCodePageView)
        {
            return _QRCodePV.InsertQRCodePV(QRCodePageView);
        }

        /// <summary>
        /// 查询指定条件下的“下载页面”访问记录
        /// </summary>
        /// <param name="typeId">类别ID</param>
        /// <param name="companyId">公司ID</param>
        /// <param name="shopId">店铺ID</param>
        /// <param name="timeFrom">起始时间</param>
        /// <param name="timeTo">结束时间</param>
        /// <returns>符合条件的集合</returns>
        public DataTable QueryQRCodePV(int typeId, int companyId, int shopId, DateTime timeFrom, DateTime timeTo)
        {
            return _QRCodePV.QueryQRCodePV(typeId, companyId, shopId, timeFrom, timeTo);
        }
    }
}
