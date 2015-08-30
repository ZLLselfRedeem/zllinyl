using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    /// <summary>
    /// 官网配置文件Model
    /// 创建日期：2014-4-22
    /// </summary>
    public class OfficialWebConfig
    {
        /// <summary>
        /// 编号
        /// </summary>
        public long id;
        /// <summary>
        /// 类别
        /// </summary>
        public int type;
        /// <summary>
        /// 名称
        /// </summary>
        public string title;
        /// <summary>
        /// 日期
        /// </summary>
        public string date;
        /// <summary>
        /// 内容
        /// </summary>
        public string content;
        /// <summary>
        /// 排序号
        /// </summary>
        public int sequence;
        /// <summary>
        /// 图片名称
        /// </summary>
        public string imageName;
        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime updateTime;
        /// <summary>
        /// 数据状态
        /// </summary>
        public int status;
        /// <summary>
        /// 备注
        /// </summary>
        public string remark;
    }
}
