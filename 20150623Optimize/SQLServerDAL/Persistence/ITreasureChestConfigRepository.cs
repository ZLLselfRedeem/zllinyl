using System.Collections.Generic;
using VAGastronomistMobileApp.Model;

namespace VAGastronomistMobileApp.SQLServerDAL.Persistence
{
    public interface ITreasureChestConfigRepository
    {
        IEnumerable<TreasureChestConfig> GetManyByActivity(int activityId);

        void UpdateRemainQuantity(long id,int remainQuantity);

        int UpdateRemainQuantity(long id, int originRemainQuantity, int remainQuantity);
        TreasureChestConfig GetById(int id);
        double SumActivityRemainAmount(int activityId);
    }
}