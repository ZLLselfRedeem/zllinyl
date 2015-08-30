using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.Model;

namespace VAGastronomistMobileApp.SQLServerDAL.Persistence
{
    public interface ITreasureChestRepository
    {
        TreasureChest GetById(long id);
        TreasureChest GetByActivityCookie(string cookie, int activityId);

        TreasureChest GetByActivityMobilePhone(string mobilePhone, int activityId);

        TreasureChest GetByActivity(int activityId);

        void Add(TreasureChest treasureChest);
        void UpdateLockAndAmount(long treasureChestId, double remainAmount, int lockCount);
        void UpdateLockAndAmountAndExecutedTime(long treasureChestId, double remainAmount, int lockCount, DateTime executedTime);

        void UpdateMobilePhoneLockAndAmount(long treasureChestId, string mobilePhone, double remainAmount, int lockCount);
        void UpdateCookie(long treasureChestId, string cookie);
        void UpdateStatus(long treasureChestId, bool status);

        int UpdateLockAndAmount(long treasureChestId, double originRemainAmount, double remainAmount,
            int originLockCount, int lockCount);
        bool AnyMobilePhone(string mobilePhone);
    }
}
