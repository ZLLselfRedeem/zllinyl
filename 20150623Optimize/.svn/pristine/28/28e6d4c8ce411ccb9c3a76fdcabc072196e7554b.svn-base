using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL;
using VAGastronomistMobileApp.SQLServerDAL.Persistence.Infrastructure;

namespace VAGastronomistMobileApp.WebPageDll
{
    /// <summary>
    /// 宝箱信息业务操作层
    /// </summary>
    public class TreasureChestOperate
    {
        private readonly ITreasureChestManager manager;

        public TreasureChestOperate()
        {
            manager = new TreasureChestManager();
        }
        /// <summary>
        /// 查询宝箱列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="activityId"></param>
        /// <returns></returns>
        public List<TreasureChest> QueryTreasureChest(Page page, int activityId)
        {
            return manager.SelectTreasureChest(page, activityId);
        }
        /// <summary>
        /// 查询某个红包对应宝箱开锁人数
        /// </summary>
        /// <param name="redEnvelopeId"></param>
        /// <returns></returns>
        public int GetNotLockTreasureChestCount(long redEnvelopeId)
        {
            return manager.GetNotLockTreasureChestCount(redEnvelopeId);
        }

        /// <summary>
        /// 根据用户cookie更新其手机号码
        /// </summary>
        /// <param name="mobilePhoneNumber"></param>
        /// <param name="cookie"></param>
        /// <returns></returns>
        public bool UpdateTreasureChestOwner(string mobilePhoneNumber, string cookie)
        {
            return manager.UpdateTreasureChestOwner(mobilePhoneNumber, cookie);
        }
        public long InsertTreasureChest(TreasureChest treasureChest)
        {
            return manager.InsertTreasureChest(treasureChest);
        }

        public TreasureChest GetByActivity(int activityId)
        {
            return manager.GetByActivity(activityId);
        }
    }
}
