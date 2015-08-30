using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL;

namespace VAGastronomistMobileApp.WebPageDll
{
    public class ViewAllocAwardOperate
    {
        private readonly ViewAllocAwardManager viewAllocAwardManager = new ViewAllocAwardManager();
        /// <summary>
        /// 获取全平台对应的奖品列表
        /// </summary>
        /// <returns></returns>
        public List<ViewAllocAward> SelectShopAwardList()
        {
            return viewAllocAwardManager.SelectVAAwardList();
        }

        public List<ViewAllocAward> SelectShopAwardList(string awardIDS)
        {
            return viewAllocAwardManager.SelectVAAwardList(awardIDS);
        }

        /// <summary>
        /// 查询平台某个类型奖品
        /// </summary>
        /// <param name="awardType"></param>
        /// <returns></returns>
        public List<ViewAllocAward> SelectVAAwardList(AwardType awardType)
        {
            return viewAllocAwardManager.SelectVAAwardList(awardType);
        }

        
        /// <summary>
        /// 根据Id获取具体奖品信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ViewAllocAward SelectVAAward(Guid Id)
        {
            return viewAllocAwardManager.SelectVAAward(Id);
        }
    }
}
