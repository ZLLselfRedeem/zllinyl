using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    [DataContract]
    public class FoodDiaryDish
    {
        [DataMember(Name = "id")]
        public long Id { set; get; }
        [DataMember(Name = "foodDiaryId")]
        public long FoodDiaryId { set; get; }
        [DataMember(Name = "dishId")]
        public int DishId { set; get; }
        [DataMember(Name = "dishName")]
        public string DishName { set; get; }
        [DataMember(Name = "imagePath")]
        public string ImagePath { set; get; }
        [DataMember(Name = "source")]
        public FoodDiaryDishSource Source { set; get; }
        /// <summary>
        /// 从大到小排序
        /// </summary>
        [DataMember(Name = "sort")]
        public int Sort { set; get; }
        [DataMember(Name = "status")]
        public bool Status { set; get; }
    }

    public enum FoodDiaryDishSource : byte
    {
        订单 = 0,
        门店菜谱 = 1,
        默认缺省配置 = 2,
        门店环境 = 3
    }
}
