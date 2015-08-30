using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.SQLServerDAL;
using VAGastronomistMobileApp.Model;
using System.Transactions;

namespace VAGastronomistMobileApp.WebPageDll
{
    /// <summary>
    /// 宝箱配置
    /// 2014-7-29
    /// </summary>
    public class TreasureChestConfigOperate
    {
        private readonly ITreasureChestManager treasureChestManage;

        public TreasureChestConfigOperate()
        {
            treasureChestManage = new TreasureChestManager();
        }
        TreasureChestConfigManager treasureChestConfigManager = new TreasureChestConfigManager();

        /// <summary>
        /// 新增宝箱
        /// </summary>
        /// <param name="treasureChest"></param>
        /// <returns></returns>
        public int InsertTreasureChest(TreasureChestConfig treasureChest)
        {
            return treasureChestConfigManager.InsertTreasureChest(treasureChest);
        }

        /// <summary>
        /// 修改宝箱配置（活动开始前，只修改配置表，开始后，两个表都要修改）
        /// </summary>
        /// <param name="treasureChest"></param>
        /// <returns></returns>
        public bool UpdateActivity(TreasureChestConfig treasureChest)
        {
            ActivityOperate activityOperate = new ActivityOperate();
            Activity activity = activityOperate.QueryActivity(treasureChest.activityId);
            bool updateConfig = treasureChestConfigManager.UpdateActivity(treasureChest);
            return updateConfig;
            //using (TransactionScope ts = new TransactionScope())
            //{
            //    bool update = true;
            //    bool updateConfig = true;
            //    if (activity.beginTime < DateTime.Now && activity.endTime > DateTime.Now)
            //    {
            //        update = treasureChestManage.UpdateTreasureChestAmount(treasureChest.activityId, treasureChest.amount);
            //    }
            //    updateConfig = treasureChestConfigManager.UpdateActivity(treasureChest);
            //    if (update && updateConfig)
            //    {
            //        ts.Complete();
            //        return true;
            //    }
            //    else
            //    {
            //        return false;
            //    }
            //}
        }

        /// <summary>
        /// 删除指定宝箱
        /// </summary>
        /// <param name="treasureChestConfigId"></param>
        /// <returns></returns>
        public bool DeleteTreasureChest(int treasureChestConfigId)
        {
            return treasureChestConfigManager.DeleteTreasureChest(treasureChestConfigId);
        }

        /// <summary>
        /// 根据宝箱Id查询相应宝箱
        /// </summary>
        /// <param name="treasureChestConfigId"></param>
        /// <returns></returns>
        public TreasureChestConfig QueryTreasureChest(int treasureChestConfigId)
        {
            return treasureChestConfigManager.QueryTreasureChest(treasureChestConfigId);
        }

        /// <summary>
        /// 查询所有宝箱配置
        /// </summary>
        /// <param name="page"></param>
        /// <param name="activityId"></param>
        /// <returns></returns>
        public List<TreasureChestConfig> QueryTreasureChest(SQLServerDAL.Persistence.Infrastructure.Page page, out int cnt, int activityId = 0)
        {
            return treasureChestConfigManager.QueryTreasureChest(page, out cnt, activityId);
        }
        /// <summary>
        /// 根据活动Id查询宝箱配置
        /// </summary>
        /// <param name="activityId"></param>
        /// <returns></returns>
        public TreasureChestConfig QueryConfigOfActivity(int activityId)
        {
            return treasureChestConfigManager.QueryConfigOfActivity(activityId);
        }
    }
}
