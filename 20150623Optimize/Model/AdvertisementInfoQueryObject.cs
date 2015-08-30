using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    /*********************************************
     * Added by 林东宇 2014-11-12
     * 广告数据查询条件集合
     * *********************************************/
    /// <summary>
    /// 广告查询条件
    /// </summary>
    public class AdvertisementInfoQueryObject
    {
        public AdvertisementInfoQueryObject()
        {
        }
        #region Model
        private long? _id;
        private string _name;
        private string _imageurl;
        private int? _status;
        private int? _advertisementtype;
        private string _value;
        private string _advertisementDescription;
        private int? _advertisementClassify;
        /// <summary>
        /// 
        /// </summary>
        public long? id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string name
        {
            set { _name = value; }
            get { return _name; }
        }
        /// <summary>
        /// 精确匹配
        /// </summary>
        public string nameEqual
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        public string imageURL
        {
            set { _imageurl = value; }
            get { return _imageurl; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? status
        {
            set { _status = value; }
            get { return _status; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? advertisementType
        {
            set { _advertisementtype = value; }
            get { return _advertisementtype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string value
        {
            set { _value = value; }
            get { return _value; }
        }
        /// <summary>
        /// 广告描述信息
        /// </summary>
        public string advertisementDescription
        {
            set { _advertisementDescription = value; }
            get { return _advertisementDescription; }
        }
        /// <summary>
        /// 公司广告地址
        /// </summary>
        public string webAdvertisementUrl
        {
            get;
            set;
        }
        /// <summary>
        /// 广告大分类：首页广告：1，美食广场广告：2
        /// </summary>
        public int? advertisementClassify
        {
            get { return _advertisementClassify; }
            set { _advertisementClassify = value; }
        }
        /// <summary>
        /// 已对广告进行排期的城市
        /// </summary>
        public int? cityID
        {
            get;
            set;
        }
        /// <summary>
        /// 广告栏位ID
        /// </summary>
        public int?  advertisementColumnId 
        {
            get;
            set;
        }
        /// <summary>
        /// 广告开始时段
        /// </summary>
        public DateTime? IntervalStart
        {
            get;
            set;
        }
        /// <summary>
        /// 广告截至时段
        /// </summary>
        public DateTime? IntervalEnd
        {
            get;
            set;
        }
        #endregion Model
    }
}