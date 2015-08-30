using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.Model;

namespace VAGastronomistMobileApp.SQLServerDAL.Persistence
{
    public interface IRedEnvelopeRankListRepository
    {
        RedEnvelopeRankList GetByMobilePhoneNumber(string mobilePhoneNumber);
        void Insert(RedEnvelopeRankList rankList);

        //void UpdateRanking(long id, long ranking, RankState rankState);

        void UpdateRanking(long id, long ranking, double totalamount, RankState rankState);
    }
}
