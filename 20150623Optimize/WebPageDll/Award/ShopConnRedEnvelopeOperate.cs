using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL;

namespace VAGastronomistMobileApp.WebPageDll
{
    public class ShopConnRedEnvelopeOperate
    {
        ShopConnRedEnvelopeManager manager = new ShopConnRedEnvelopeManager();

        /// <summary>
        /// 查询店铺有没有单独配置红包
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public ShopConnRedEnvelope SelectShopConnRedEnvelope(int shopId)
        {
            return manager.SelectShopConnRedEnvelope(shopId);
        }

        public bool UpdateShopConnRedEnvelope(int shopId, int status, int RedEnvelopeConsumeCount, double RedEnvelopeConsumeAmount)
        {
            return manager.UpdateShopConnRedEnvelope(shopId, status, RedEnvelopeConsumeCount, RedEnvelopeConsumeAmount);
        }

        public bool InsertShopConnRedEnvelope(ShopConnRedEnvelope shopConnRedEnvelope)
        {
            return manager.InsertShopConnRedEnvelope(shopConnRedEnvelope);
        }
    }
}
