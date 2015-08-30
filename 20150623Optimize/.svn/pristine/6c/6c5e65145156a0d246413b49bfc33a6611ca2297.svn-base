using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    /// <summary>
    /// 服务员积分日志操作类
    /// created by wangcheng 
    /// 20140222
    /// </summary>
    public class EmployeePointLog
    {
        /// <summary>
        ///主键
        /// </summary>
        public long id { get; set; }
        /// <summary>
        /// 记录日志时间
        /// </summary>
        public DateTime operateTime { get; set; }
        /// <summary>
        /// 服务员编号
        /// </summary>
        public int employeeId { get; set; }
        /// <summary>
        /// 点单流水号
        /// </summary>
        public long preOrder19dianId { get; set; }
        /// <summary>
        /// 门店编号
        /// </summary>
        public int shopId { get; set; }
        /// <summary>
        /// 用户编号
        /// </summary>
        public long customerId { get; set; }
        /// <summary>
        /// 友络管理员编号（注：0 表示系统）
        /// </summary>
        public int viewallocEmployeeId { get; set; }
        /// <summary>
        /// 变动积分数量
        /// </summary>
        public double pointVariation { get; set; }
        /// <summary>
        /// 积分变动方式
        /// </summary>
        public int pointVariationMethods { get; set; }
        /// <summary>
        /// 消费金额
        /// </summary>
        public double monetary { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string remark { get; set; }
        /// <summary>
        /// 状态（预留字段）
        /// </summary>
        public int status { get; set; }

        /// <summary>
        /// 积分商城商品ID
        /// </summary>
        public int goodsId { get; set; }

        /// <summary>
        /// 服务员店铺及收货地址
        /// </summary>
        public string address { get; set; }

        /// <summary>
        /// 兑换状态（-1积分异常；1：处理中；2：已兑换；）
        /// 服务员当前可用积分小于0
        /// </summary>
        public int exchangeStatus { get; set; }

        /// <summary>
        /// 确认状态（1：已确认；-1：未确认）
        /// </summary>
        public int confirmStatus { get; set; }

        /// <summary>
        /// 确认时间
        /// </summary>
        public DateTime confirmTime { get; set; }

        /// <summary>
        /// 确认人（友络工作人员）
        /// </summary>
        public int confirmBy { get; set; }

        /// <summary>
        /// 发货状态 -1未发货/未充值，1已发货/已充值
        /// </summary>
        public int shipStatus { get; set; }

        /// <summary>
        /// 发货人（友络工作人员）
        /// </summary>
        public int shipBy { get; set; }

        /// <summary>
        /// 快递公司/充值平台
        /// </summary>
        public string platform { get; set; }

        /// <summary>
        /// 快递单号/充值单号
        /// </summary>
        public string serialNumber { get; set; }

        /// <summary>
        /// 兑换商品备注
        /// </summary>
        public string exchangeRemark { get; set; }
    }
}
