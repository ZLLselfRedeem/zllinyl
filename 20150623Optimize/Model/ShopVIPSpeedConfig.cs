using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    /// <summary>
    /// DB中存储的数据
    /// </summary>
    public class ShopVIPSpeedConfig
    {
        public int Id { get; set; }
        public int City { get; set; }
        public short StartHour { get; set; }
        public short EndHour { get; set; }
        public int PreUnit { get; set; }
        public ShopVIPSpeedConfigUnit Unit { get; set; }
        public int MinSpeed { get; set; }
        public int MaxSpeed { get; set; }
    }


    public enum ShopVIPSpeedConfigUnit
    {
        分钟 = 1,
        小时 = 2
    }
}
