using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VAGastronomistMobileApp.Model.HomeNew
{
    /// <summary>
    /// 一级栏目
    /// </summary>
    public class Title
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 城市ID
        /// </summary>
        public int CityID { get; set; }

        /// <summary>
        /// 栏目名称
        /// </summary>
        public string TitleName { get; set; }

        /// <summary>
        /// 栏目顺序
        /// </summary>
        public int TitleIndex { get; set; }

        /// <summary>
        /// 栏目类型---广告or正常
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 客户端是否开启
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public int CreateBy { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDelete { get; set; }  
    }

    /// <summary>
    /// 一级栏目下拉框相关类
    /// </summary>
    public class TitleViewModel
    {
        public int titleID { get; set; }

        public string titleName { get; set; }
    }

    public class SubTitleModel
    {
        public string titleID { get; set; }

        public string titleName { get; set; }
    }


    public class RuleViewModel
    {
        public int ruleID { get; set; }
        public string ruleName { get; set; }
    }
}
