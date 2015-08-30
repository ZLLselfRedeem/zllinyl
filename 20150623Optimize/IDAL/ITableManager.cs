using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using VAGastronomistMobileApp.Model;

namespace VAGastronomistMobileApp.IDAL
{
    public interface ITableManager
    {
        /// <summary>
        /// 新增餐桌
        /// </summary>
        /// <param name="table"></param>
        int InsertTable(TableInfo table);
        /// <summary>
        /// 删除餐桌
        /// </summary>
        /// <param name="tableID"></param>
        bool DeleteTable(int tableID);
        /// <summary>
        /// 修改餐桌
        /// </summary>
        /// <param name="table"></param>
        bool UpdateTable(TableInfo table);
        /// <summary>
        /// 修改餐桌状态
        /// </summary>
        /// <param name="tableID"></param>
        /// <param name="status"></param>
        bool UpdateTableStatus(int tableID,int status);
        /// <summary>
        /// 根据餐桌ID查询餐桌信息
        /// </summary>
        /// <param name="tableID"></param>
        DataTable QueryTable(int tableID);
        /// <summary>
        /// 查询所有餐桌信息
        /// </summary>
        DataTable QueryTable();
        /// <summary>
        /// 查询所有非空餐桌信息，包括点单基本信息
        /// </summary>
        DataTable QueryTableOrder();
    }
}
