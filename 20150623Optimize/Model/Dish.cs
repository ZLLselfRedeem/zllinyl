using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    public class Dish
    {
        public int DishId { set; get; }
        public string DishName { set; get; }
        /// <summary>
        /// 普通图片
        /// </summary>
        public string Image0 { set; get; }
    }

    public class DishImage
    {
        public string url { get; set; }
        public int dishId { get; set; }
        public int imageId { get; set; }
    }

    public class OrderDishOtherInfo
    {
        public int dishId { get; set; }
        public string orderDishImageUrl { get; set; }
        public bool orderDishIsPraise { get; set; }
        public int orderDishPraiseNum { get; set; }
    }
    [Serializable]
    [DataContract]
    public class DishPraiseInfo
    {
        [DataMember]
        public int dishId { get; set; }
        [DataMember]
        public int orderDishPraiseNum { get; set; }
        [DataMember]
        public int orderDishSaleCount { get; set; }
    }
}
