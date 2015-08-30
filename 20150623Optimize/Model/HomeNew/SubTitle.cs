using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VAGastronomistMobileApp.Model.HomeNew
{
    /// <summary>
    /// 二级栏目
    /// </summary>
    public class SubTitle
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 对应的一级栏目ID
        /// </summary>
        public int FirstTitleID { get; set; }

        /// <summary>
        /// 栏目名称
        /// </summary>
        public string TitleName { get; set; }

        /// <summary>
        /// 栏目顺序
        /// </summary>
        public int TitleIndex { get; set; }

        /// <summary>
        /// 栏目规则类型
        /// </summary>
        public int RuleType { get; set; }

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
}
