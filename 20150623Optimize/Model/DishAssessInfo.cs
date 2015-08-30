using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    /// <summary>
    /// 菜点评信息
    /// </summary>
    public class DishAssessInfo
    {
        /// <summary>
        /// 点评编号
        /// </summary>
        public int AssessmentID { get; set; }
        /// <summary>
        /// 对应账单编号
        /// </summary>
        public int OrderID { get; set; }
        /// <summary>
        /// 对应菜名编号
        /// </summary>
        public int DishID { get; set; }
        /// <summary>
        /// 点评内容
        /// </summary>
        public string AssessContent { get; set; }
        /// <summary>
        /// 点评分数
        /// </summary>
        public int AssessScore { get; set; }
        /// <summary>
        /// 点评状态
        /// 0：已删除，1：正常
        /// </summary>
        public int AssessStatus { get; set; }
    }
}
