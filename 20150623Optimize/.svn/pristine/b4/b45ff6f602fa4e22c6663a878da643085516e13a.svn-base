using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using VAGastronomistMobileApp.SQLServerDAL;
using VAGastronomistMobileApp.Model;

namespace VAGastronomistMobileApp.WebPageDll
{
    /// <summary>
    /// 用户投诉相关
    /// add by wangc 
    /// 20140320
    /// </summary>
    public class ComplaintOperate
    {
        readonly ComplaintManager man = new ComplaintManager();
        /// <summary>
        /// 客户端查询投诉点单和服务员相关信息
        /// </summary>
        /// <param name="preOrder19dianId"></param>
        /// <returns></returns>
        public DataTable QueryComplaintDish(long preOrder19dianId)
        {
            return man.SelectComplaintDish(preOrder19dianId);
        }
        /// <summary>
        /// 查询用户投诉信息
        /// </summary>
        /// <returns></returns>
        public DataTable QueryCustomerComplaint()
        {
            return man.SelectCustomerComplaint();
        }
        /// <summary>
        /// 新增投诉信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddCustomerComplaint(CustomerComplaint model)
        {
            return man.InsertCustomerComplaint(model) > 0;
        }
        /// <summary>
        /// 判断当前用户是否对当前点单评价
        /// </summary>
        /// <param name="preOrder19DianId"></param>
        /// <returns></returns>
        public bool IsComplaint(long preOrder19DianId)
        {
            return man.IsComplaint(preOrder19DianId);
        }
    }
}
