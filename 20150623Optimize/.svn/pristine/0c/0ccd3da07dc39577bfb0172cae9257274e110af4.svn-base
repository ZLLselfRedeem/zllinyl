using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    public class RedEnvelopeRankList
    {
        public long Id { get; set; }
        public string mobilePhoneNumber { get; set; }
        public long ranking { get; set; }
        public DateTime createTime { get; set; }
        public DateTime lastUpdateTime { get; set; }

        public RankState rankState { get; set; }

        public double amount { get; set; }
    }

    public enum RankState
    {
        上升 = 1,
        下降 = -1,
        保持 = 0
    }
}
