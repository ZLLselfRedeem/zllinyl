using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    /// <summary>
    /// 员工信息
    /// </summary>
    public class EmployeeInfo
    {
        public EmployeeInfo()
        {
            EmployeeSex = 2;
            EmployeeAge = 0;
            EmployeeSequence = 0;
            EmployeeStatus = 0;
            isViewAllocWorker = false;
            settlementPoint = 0;
            notSettlementPoint = 0;
            registerTime = new DateTime(1970, 1, 1);
            birthday = new DateTime(1970, 1, 1);
            isSupportLoginBgSYS = false;
        }

        /// <summary>
        /// 员工编号
        /// </summary>
        public int EmployeeID { get; set; }
        /// <summary>
        /// 员工登录名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 员工登录密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 员工姓名
        /// </summary>
        public string EmployeeFirstName { get; set; }
        /// <summary>
        /// 员工名
        /// </summary>
        /// 2014-2-23 取消LastName
        //public string EmployeeLastName { get; set; }
        /// <summary>
        /// 员工性别
        /// 0：女，1：男，2：不详
        /// </summary>
        public int? EmployeeSex { get; set; }
        /// <summary>
        /// 员工年龄
        /// </summary>
        public int? EmployeeAge { get; set; }
        /// <summary>
        /// 员工电话
        /// </summary>
        public string EmployeePhone { get; set; }
        /// <summary>
        /// 员工显示序号
        /// </summary>
        public int? EmployeeSequence { get; set; }
        /// <summary>
        /// 员工状态
        /// -1：已删除，1：正常
        /// </summary>
        public int? EmployeeStatus { get; set; }
        //2014-2-23 取消 员工抹零金额最大值，快速结账权限，清台权限，称重权限
        /// <summary>
        /// 员工抹零金额最大值
        /// </summary>
        //public double removeChangeMaxValue { get; set; }
        /// <summary>
        /// 员工是否有快速结账权限
        /// </summary>
        //public bool canQuickCheckout { get; set; }
        /// <summary>
        /// 员工是否有清台权限
        /// </summary>
        //public bool canClearTable { get; set; }
        /// <summary>
        /// 员工是否有称重权限
        /// </summary>
        //public bool canWeigh { get; set; }
        /// <summary>
        /// 员工职位
        /// </summary>
        public string position { get; set; }
        //2013-7-26 wangcheng
        /// <summary>
        /// 初始化显示页面
        /// </summary>
        public string defaultPage { get; set; }
        /// <summary>
        /// 员工cookie
        /// </summary>
        public string cookie { get; set; }
        ///2014-2-18 jinyanni
        /// <summary>
        /// 是否是友络工作人员
        /// </summary>
        public bool? isViewAllocWorker { get; set; }
        /// <summary>
        /// 已结算积分
        /// </summary>
        public double? settlementPoint { get; set; }
        /// <summary>
        /// 未结算积分
        /// </summary>
        public double? notSettlementPoint { get; set; }
        /// <summary>
        /// 注册时间
        /// </summary>
        public DateTime? registerTime { get; set; }
        /// <summary>
        /// 生日
        /// </summary>
        public DateTime? birthday { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string remark { get; set; }
        /// <summary>
        /// 可登陆后台系统 add by wangc 20140324
        /// </summary>
        public bool? isSupportLoginBgSYS { get; set; }
        /// <summary>
        /// 推送token
        /// </summary>
        public string pushToken { set; get; }
        /// <summary>
        /// 应用类型
        /// </summary>
        public int? AppType { set; get; }
    }
    /// <summary>
    /// 短信提醒门店员工收到点单
    /// </summary>
    public class PayOrderSMSRemaid
    {
        public int shopId { get; set; }
        public double amount { get; set; }
        /// <summary>
        /// 用户手机号码
        /// </summary>
        public string customerPhone { get; set; }
        public long preOrderId { get; set; }
        public long customerId { get; set; }
        public string shopName { get; set; }
        public string clientBuild { get; set; }
    }
    /// <summary>
    /// 客户经理信息 add by wangc 20140328
    /// </summary>
    public class PartEmployee
    {
        public int employeeId { get; set; }
        public string employeeName { get; set; }
        public string employeePhone { get; set; }
    }

    public class RefundDetail
    {
        public string name { get; set; }
        public double amount { get; set; }
        public double time { get; set; }
    }
}
