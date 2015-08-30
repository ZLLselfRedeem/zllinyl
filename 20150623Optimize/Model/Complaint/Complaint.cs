using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    /// <summary>
    /// 用户投诉相关
    /// add by wangc 
    /// 20140320
    /// </summary>
    public class CustomerComplaint
    {
        public long id { get; set; }
        public long preOrder19dianId { get; set; }
        public string complaintMsg { get; set; }
        public DateTime complaintTime { get; set; }
    }
    /// <summary>
    /// 客户端投诉页面
    /// </summary>
    public class ComplaintInfo
    {
        public List<string> dishName { get; set; }//菜品名称
        public string waiterPhone { get; set; }//服务员电话
        public string waiterName { get; set; }//服务员姓名
    }
}
