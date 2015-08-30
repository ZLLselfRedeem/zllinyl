using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    public class FoodDiaryDetails
    {
        public long Id { get; set; }
        public DateTime PrePayTime { get; set; }

        public DateTime CreateTime { get; set; }
        public string MobilePhoneNumber { get; set; }
        public string Name { get; set; }
        public byte IsPaid { get; set; }
        public double PrePaidSum { get; set; }
        public string ShopName { get; set; }
        public FoodDiaryShared Shared { get; set; }
        public string Content { get; set; }
        public int Hit { get; set; }
    }
}
