using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    /// <summary>
    /// 菜品信息
    /// </summary>  
    public class SybDishInfo
    {
        public int menuId { get; set; }
        /// <summary>
        /// 菜单编号
        /// </summary>
        public int DishID { get; set; }
        /// <summary>
        /// 菜品名称
        /// </summary>
        public string DishName { get; set; }
        /// <summary>
        /// 全拼
        /// </summary>
        public string dishQuanPin { get; set; }
        /// <summary>
        /// 简拼
        /// </summary>
        public string dishJianPin { get; set; }

        /// <summary>
        /// 菜的显示顺序
        /// </summary>
        public int DishDisplaySequence { get; set; }
        /// <summary>
        /// 简介
        /// </summary>
        public string DishDescShort { get; set; }
        /// <summary>
        /// 详情
        /// </summary>
        public string DishDescDetail { get; set; }
        /// <summary>
        /// 大图地址
        /// </summary>
        public string DishImageUrlBig { get; set; }

        //public string DishImageUrlSmall { get; set; }

        /// <summary>
        /// 用于获取图片的Id
        /// </summary>
        public string SessionId { get; set; }
        /// <summary>
        /// 图片X左边
        /// </summary>
        public int PicX { get; set; }
        /// <summary>
        /// 图片Y左边
        /// </summary>
        public int PicY { get; set; }
        /// <summary>
        /// 图片宽度
        /// </summary>
        public int PicWidth { get; set; }
        /// <summary>
        /// 图片高度
        /// </summary>
        public int PicHeight { get; set; }
        /// <summary>
        /// 图片客户端高度
        /// </summary>
        public int PicClientWidth { get; set; }
        /// <summary>
        /// 图片处理状态 1 未处理 2图片修改
        /// </summary>
        public int PicStatus { get; set; }
        /// <summary>
        /// 规格
        /// </summary>
        public List<SybDishPriceInfo> DishPriceList { get; set; }
        /// <summary>
        /// 分类
        /// </summary>
        public List<int> DishTypeList { get; set; }
    }
    /// <summary>
    /// 菜品规格
    /// </summary>
    public class SybDishPriceInfo
    {
        /// <summary>
        /// 菜价格编号
        /// </summary>
        public int DishPriceID { get; set; }
        /// <summary>
        /// 规格名称
        /// </summary>
        public string ScaleName { get; set; }
        /// <summary>
        /// 菜单价
        /// </summary>
        public double DishPrice { get; set; }
        /// <summary>
        /// 掌中宝编号
        /// </summary>
        public string markName { get; set; }
        /// <summary>
        /// 对应菜编号
        /// </summary>
        public int DishID { get; set; }
        /// <summary>
        /// 菜是否售罄
        /// </summary>
        public bool DishSoldout { get; set; }
        /// <summary>
        /// 菜是否需要称重
        /// </summary>
        public bool DishNeedWeigh { get; set; }
        /// <summary>
        /// 菜是否能享受Vip折扣
        /// </summary>
        public bool vipDiscountable { get; set; }
        /// <summary>
        /// 菜能否支持返送
        /// </summary>
        public bool backDiscountable { get; set; }
        /// <summary>
        /// 配料种类最小数量
        /// </summary>
        public int dishIngredientsMinAmount { get; set; }
        /// <summary>
        /// 配料种类最大数量
        /// </summary>
        public int dishIngredientsMaxAmount { get; set; }

        public int DishPriceStatus { get; set; }

        /// <summary>
        /// 操作状态
        /// </summary>
        public OperStatus operStatus { get; set; }

        /// <summary>
        /// 口味
        /// </summary>
        public List<int> DishTasteList { get; set; }
        /// <summary>
        /// 配料
        /// </summary>
        public List<int> DishIngredientsList { get; set; }
      
    }

    /// <summary>
    /// 菜品修改页面的配置信息
    /// </summary>
    public class SybDishConfig
    {
        public List<SybTypeinfoModel> DishTypeInfoList { get; set; }
        public List<SybTypeModel> DishTasteList { get; set; }
        public List<SybTypeModel> DishIngredientsList { get; set; }
        public SybDishImage dishImage { get; set; }
    }
}