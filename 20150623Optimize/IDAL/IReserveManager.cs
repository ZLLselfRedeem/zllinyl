using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using VAGastronomistMobileApp.Model;

namespace VAGastronomistMobileApp.IDAL
{
    public interface IReserveManager
    {
        /// <summary>
        /// 新增预定单
        /// </summary>
        /// <param name="reserve"></param>
        void InsertReserve(ReserveInfo reserve);
        /// <summary>
        /// 删除预定单
        /// </summary>
        /// <param name="reserveID"></param>
        void DeleteReserve(int reserveID);
        /// <summary>
        /// 修改预定单
        /// </summary>
        /// <param name="reserve"></param>
        void UpdateReserve(ReserveInfo reserve);
        /// <summary>
        /// 根据预定编号查询预定单信息
        /// </summary>
        /// <param name="reserveID"></param>
        DataSet QueryReserve(int reserveID);
        /// <summary>
        /// 查询所有预定单信息
        /// </summary>
        DataTable QueryReserve();
    }
}
