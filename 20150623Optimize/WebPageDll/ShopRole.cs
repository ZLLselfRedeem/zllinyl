﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace VAGastronomistMobileApp.WebPageDll
{
    public enum ShopRole
    {
        菜品沽清 = 1,
        调整价格 = 2,
        客户追溯 = 3,
        余额调整 = 4,
        菜谱更新 = 0,
        短信通知 = 5,
        门店会员 = 6,
        客户信息 = 7,
        店员退款 = 8,
        配菜沽清 = 9,
        用户评价信息 = 10,
        抵扣券统计 = 11,
        抵扣券发布 = 12,
        抽奖设置 = 13,
        会员营销 = 14,
        抽奖活动统计 = 31,
        会员营销统计 = 32,
        //  增值管理=34， // 数据库写死数据，34请不要占用
    }

    /// <summary>
    /// 
    /// </summary>
    public static class ShopRoleExt
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sr"></param>
        /// <returns></returns>
        public static string GetString(this ShopRole sr)
        {
            return ((int)sr).ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sr"></param>
        /// <returns></returns>
        public static ListItem[] ToListItem(this ShopRole sr)
        {

            var query = (from int s in Enum.GetValues(typeof(ShopRole))
                         select new ListItem
                         {
                             Value = s.ToString(),
                             Text = Enum.GetName(typeof(ShopRole), s),
                             Selected = (s == (int)sr)
                         }).ToArray();



            return query;
        }
    }
}
