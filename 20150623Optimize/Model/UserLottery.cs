using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    public class UserLottery
    {
        /// <summary>
        /// 店铺ID
        /// </summary>
        public int shopId { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public string startTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public string endTime { get; set; }
        /// <summary>
        /// 上次抽奖时间
        /// </summary>
        public DateTime lastLotteryDate { get; set; }
        /// <summary>
        /// 工作日奖励数量
        /// </summary>
        public int workdayPrizeNum { get; set; }
        /// <summary>
        /// 已发放奖励数量
        /// </summary>
        public int currentPrizeNum { get; set; }
        /// <summary>
        /// 周末奖励数量
        /// </summary>
        public int weekendPrizeNum { get; set; }
        /// <summary>
        /// 最高中奖率
        /// </summary>
        public double highestLotteryRate { get; set; }
        /// <summary>
        /// 最低中奖率
        /// </summary>
        public double lowestLotteryRate { get; set; }
        /// <summary>
        /// 中奖率递减因子
        /// </summary>
        public double decreaseRate { get; set; }
        /// <summary>
        /// 开启状态
        /// </summary>
        public int status { get; set; }
    }
}
