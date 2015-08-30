using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using VAGastronomistMobileApp.Model;

namespace VAGastronomistMobileApp.IDAL
{
    public interface IDishAssessManager
    {
        /// <summary>
        /// 新增点评
        /// </summary>
        /// <param name="dishAssess"></param>
        void AddDishAssess(DishAssessInfo dishAssess);
        /// <summary>
        /// 删除点评
        /// </summary>
        /// <param name="assessmentID"></param>
        void DeleteDishAssess(int assessmentID);
        /// <summary>
        /// 修改点评
        /// </summary>
        /// <param name="dishAssess"></param>
        void UpdateDishAssess(DishAssessInfo dishAssess);
        /// <summary>
        /// 根据点评编号查询点评信息
        /// </summary>
        /// <param name="assessmentID"></param>
        DataSet QueryDishAssess(int assessmentID);
        /// <summary>
        /// 查询所有预定单信息
        /// </summary>
        DataTable QueryDishAssess();
    }
}
