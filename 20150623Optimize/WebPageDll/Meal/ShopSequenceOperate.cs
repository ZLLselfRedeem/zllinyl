using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL;

namespace VAGastronomistMobileApp.WebPageDll
{
    public class ShopSequenceOperate
    {
        private readonly ShopSequenceManager manager = new ShopSequenceManager();
        /// <summary>
        /// 新增年夜饭套餐门店排序
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool AddShopSequence(int shopId)
        {
            if (manager.ExistShopSequence(shopId) == false)
            {
                ShopSequence entity = new ShopSequence()
                {
                    Id = 0,
                    sequenceNumber = 99999,
                    type = (int)MealNameType.年夜饭,
                    shopId = shopId
                };
                return manager.AddShopSequence(entity);
            }
            return true;
        }
        /// <summary>
        /// 查询年夜饭门店排序信息
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public List<ShopSequenceMedel> GetShopSequence(int type)
        {
            return manager.GetShopSequence(type);
        }
        /// <summary>
        /// 批量新增年夜饭套餐门店排序
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public bool BatchAddShopSequence(DataTable dt)
        {
            return manager.BatchAddShopSequence(dt);
        }
        /// <summary>
        /// 批量删除年夜饭门店排序
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public bool DeleteShopSequenceByType(int type)
        {
            return manager.DeleteShopSequenceByType(type);
        }
    }
}
