using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VA.Cache.Distributed;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL;

namespace VA.CacheLogic.OrderClient
{
    public class ClientIndexCacheLogic
    {
        #region 根据经纬度计算距离
        private const double EARTH_RADIUS = 6378.137;//地球半径
        public static double GetDistance(double lat1, double lng1, double lat2, double lng2)
        {
            double radLat1 = lat1 * Math.PI / 180.0;
            double radLat2 = lat2 * Math.PI / 180.0;
            double a = radLat1 - radLat2;
            double b = lng1 * Math.PI / 180.0 - lng2 * Math.PI / 180.0;

            double s = 2 * Math.Asin(Math.Sqrt(Math.Pow(Math.Sin(a / 2), 2) + Math.Cos(radLat1) * Math.Cos(radLat2) * Math.Pow(Math.Sin(b / 2), 2)));
            s = s * EARTH_RADIUS;
            s = Math.Round(s * 10000) / 10000;
            return s;
        }
        #endregion

        public List<VAIndexExt> TransformCacheData(int cityId, double userLongtitude, double userLatitude, int shopId,
          int pageIndex, int pageSize, long customerId, int dataType, int tagId, ref int dataCount, string searchKeyWord = "")
        {
            ClientExtendManager clientExtendManager = new ClientExtendManager();
            //获取缓存
            List<VAIndexExt> allIndexData = MemcachedHelper.GetMemcached<List<VAIndexExt>>("yxdc_client_index" + cityId);
            if (allIndexData == null || !allIndexData.Any())
            {
                //查询数据库
                allIndexData = clientExtendManager.GetClientIndexAllDataByCity(cityId);
                if (!allIndexData.Any())
                {
                    return new List<VAIndexExt>();
                }
                MemcachedHelper.AddMemcached("yxdc_client_index" + cityId, allIndexData, 5 * 60);//缓存5分钟
            }

            //关注（收藏），查看，吃过
            if (dataType == (int)VAIndexSorting.关注的店 || dataType == (int)VAIndexSorting.我看过的
                || dataType == (int)VAIndexSorting.我吃过的 || dataType == (int)VAIndexSorting.有券的店)
            {
                var shopIdList = clientExtendManager.GetCustomerShop(customerId, cityId, dataType).Distinct().ToList();
                allIndexData = (from a in shopIdList
                                join b in allIndexData on a equals b.shopID
                                select b).ToList();
            }

            //商圈
            if (tagId > 1)
            {
                allIndexData = (from a in clientExtendManager.GetBusinessDistrictShop(tagId)
                                join b in allIndexData on a equals b.shopID
                                select b).Distinct().ToList();
            }

            //具体门店
            if (shopId > 0)
            {
                allIndexData = allIndexData.Where(p => p.shopID == shopId).Distinct().ToList();
            }

            //关键字查询门店
            if (!String.IsNullOrEmpty(searchKeyWord.Trim()))
            {
                allIndexData = allIndexData.Where(p => p.shopName.IndexOf(searchKeyWord, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
                //allIndexData = allIndexData.Where(p => p.shopName.Contains(searchKeyWord)).ToList();
            }
            var resultList = new List<VAIndexExt>();
            switch (dataType)
            {
                case 2://距离排序
                    resultList = allIndexData.OrderBy(p => GetDistance(p.latitude, p.longitude, userLatitude, userLongtitude)).ToList();
                    break;
                case 3://销量排序
                    resultList = allIndexData.OrderByDescending(p => p.acpp).ToList();
                    break;
                default:
                    resultList = allIndexData;
                    break;
            }
            pageIndex = pageIndex <= 0 ? 1 : pageIndex;
            pageSize = pageSize <= 0 ? 20 : pageSize;
            dataCount = resultList.Count / pageSize;

            return resultList.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        }
    }
}
