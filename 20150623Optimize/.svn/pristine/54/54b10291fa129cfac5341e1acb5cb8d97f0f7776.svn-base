using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL;
using System.Data;
namespace VAGastronomistMobileApp.WebPageDll
{
    public class Money19dianDetailOperate
    {
        private readonly Money19dianDetailManager dal = new Money19dianDetailManager();
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public long AddMoney19dianDetail(Money19dianDetail model)
        {
            return Money19dianDetailManager.InsertMoney19dianDetail(model);
        }
        /// <summary>
        /// 增加一条数（New 待处理）
        /// </summary>
        public long Insert(Money19dianDetail model)
        {
            return Money19dianDetailManager.InsertMoney19dianDetail(model);
        }

        /// <summary>
        /// 根据时间，查询19点金钱操作记录
        /// </summary>
        /// <param name="strChangeTime"></param>
        /// <param name="endChangeTime"></param>
        /// <returns></returns>
        public DataTable QueryMoney19dianDetail(string strChangeTime, string endChangeTime)
        {
            return dal.SelectMoney19dian(strChangeTime, endChangeTime);
        }
        /// <summary>
        /// 收银宝查询当前点单的退款信息
        /// </summary>
        /// <param name="preOrder19dianId"></param>
        /// <returns></returns>
        public static List<MoneyRefundDetail> GetMoney19dianDetail(long  preOrder19dianId,Guid orderID)
        {
            return Money19dianDetailManager.GetMoney19dianDetailInfo(preOrder19dianId,orderID);
        }

        public static List<MoneyRefundDetail> GetMoney19dianDetail(long preOrder19dianId)
        {
            return Money19dianDetailManager.GetMoney19dianDetailInfo(preOrder19dianId);
        }
    }
}
