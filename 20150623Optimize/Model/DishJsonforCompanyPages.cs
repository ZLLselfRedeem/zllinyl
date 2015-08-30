using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    public class DishJsonforCompanyPages
    {
        public List<DishJsonInfoforCompanyPages> DishJson { get; set; }
    }
    public class DishJsonInfoforCompanyPages
    {
        public List<string> DishTypeID { get; set; }

        public string dishDescDetail { get; set; }

        public string dishDescShort { get; set; }

        public int dishDisplaySequence { get; set; }

        public string dishJianPin { get; set; }

        public string dishName { get; set; }

        public string dishQuanPin { get; set; }

        public string MenuID { get; set; }

        public int dishI18nID { get; set; }

        public int dishID { get; set; }
 
    }

    public class DishPriceJsonforCompanyPages
    {
        public List<DishPriceInfojson> DishPriceJson { get; set; }
        
    }

    public class DishPriceInfojson
    {
        public string DishPriceID { get; set; }

        public string DishPrice { get; set; }

        public string vipDiscountable { get; set; }

        public string DishPriceI18nID { get; set; }

        public string ScaleName { get; set; }

        public string markName { get; set; }

        public string backDiscountable { get; set; }

        public string status { get; set; }
    }

    public class DishImageJsonforCompanyPages
    {
        public List<DishImageInfojson> DishImageJson { get; set; }
    }

    public class DishImageInfojson
    {
        public string ImageScale { get; set; }

        public List<string> selectRect { get; set; }

        public string imgurlandname { get; set; }

        public string imgurlId { get; set; }

        public string EditStatus { get; set; }
    }
}
