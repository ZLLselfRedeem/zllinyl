using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    public class ShopRevealImage
    {
        public long Id { set; get; }
        public int ShopId { set; get; }
        public string RevealImageName { set; get; }
        public int Status { set; get; }
    }
}
