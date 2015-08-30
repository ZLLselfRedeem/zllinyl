using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.SQLServerDAL;
using VAGastronomistMobileApp.Model;
using System.Data;
using System.Collections;
using System.Transactions;

namespace VAGastronomistMobileApp.WebPageDll
{
    public class AdvertisementOperate
    {
        AdvertisementManager manager = new AdvertisementManager();
        /// <summary>
        /// 新增一条广告记录
        /// </summary>
        /// <returns></returns>
        public bool AddNewAD(AdvertisementInfo adver, ArrayList shopIdList)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                long advertisementId = manager.InsertNewAdvertisement(adver);
                if (advertisementId > 0)
                {
                    if (shopIdList.Count > 0)//表示选择了门店
                    {
                        for (int i = 0; i < shopIdList.Count; i++)
                        {
                            manager.InsertNewAdvertisementConnShopId(Common.ToInt64(shopIdList[i]), advertisementId);//插入广告门店关联表
                        }
                        scope.Complete();
                        return true;
                    }
                    else
                    {
                        scope.Complete();
                        return true;
                    }
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// 更新一条广告记录
        /// </summary>
        /// <returns></returns>
        public bool UpdateAD(AdvertisementInfo adver, ArrayList shopIdList)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                bool result = manager.UpdateAdvertisement(adver);
                if (result && shopIdList.Count > 0)
                {
                    for (int i = 0; i < shopIdList.Count; i++)
                    {
                        if (!manager.InsertNewAdvertisementConnShopId(Common.ToInt64(shopIdList[i]), adver.id))
                        {
                            return false;
                        }//插入广告门店关联表
                    }
                } 
                scope.Complete();
                return true;
            }
        }
        public int GetSopIDByAdvertisement(int AdvertisementID)
        {
            return manager.GetSopIDByAdvertisement(AdvertisementID);
        }
        /// <summary>
        /// 查询广告信息
        /// </summary>
        /// <returns></returns>
        public DataTable QueryAdvertisement(int companyId, int adClassify)
        {
            return manager.SelectAdByCompanyId(companyId, adClassify);
        }
        /// <summary>
        /// 查询广告信息
        /// </summary>
        /// <returns></returns>
        public AdvertisementInfo GetAdvertisementByID(int AdvertisementID)
        {
            return manager.GetAdvertisementInfoByID(AdvertisementID);
        }
        /**************************
         * Added by 林东宇 2014-11-12
         * 依据查询条件获取广告信息
         * ****************************/
        /// <summary>
        /// 查询广告信息
        /// </summary>
        /// <returns></returns>
        public DataTable QueryAdvertisement(AdvertisementInfoQueryObject queryObject)
        {
            return manager.SelectAdvertisement(queryObject);
        }
        /******************************************
        * Added by 林东宇 2014-11-13
        * 依据查询条件获取广告的排期信息
        * ******************************************/
        /// <summary>
        ///  查询所有广告排期信息 
        /// </summary>
        /// <returns>输出列：广告排期ID、广告名称、排期开始时间、排期截至时间、排期状态、城市名称</returns>
        public DataTable QueryAdvertisementBannerDetailInfo(AdvertisementInfoQueryObject queryObject)
        {
            return manager.SelectBannerDetailInfo(queryObject);
        }
        /// <summary>
        /// 按照广告类型查询其banner
        /// </summary>
        /// <returns></returns>
        public DataTable QueryAdvertisementBanner(int adType)
        {
            return manager.SelectBanner(adType);
        }
        /// <summary>
        /// （wangcheng）查询所有广告信息
        /// </summary>
        /// <returns></returns>
        public DataTable QueryAdvertisementBannerDetailInfo(int cityId)
        {
            return manager.SelectBannerDetailInfo(cityId);
        }
        /// <summary>
        /// （wangcheng）修改当前客户端展示广告状态（开启和关闭客户端广告）
        /// </summary>
        /// <returns></returns>
        public bool ModifyAdvertisementConnAdColumnStatus(long id, int status)
        {
            return manager.UpdateAdvertisementConnAdColumnStatus(id, status);
        }
        /// <summary>
        /// （wangcheng）修改当前客户端展示广告展示时间
        /// </summary>
        /// <returns></returns>
        public bool ModifyAdvertisementConnAdColumnTime(long id, DateTime displayStartTime, DateTime displayEndTime)
        {
            return manager.UpdateAdvertisementConnAdColumnTime(id, displayStartTime, displayEndTime);
        }
        /*******************
         * Modfed by 林东宇 at 2014-11-21
         * 允许一个广告栏位存在多个有效广告
         * ******************/
        /// <summary>
        /// 新增一条广告展示记录
        /// </summary>
        /// <returns></returns>
        public bool AddAdvertisementConnToBanner(AdvertisementConnAdColumnInfo adver)
        {

            //DataTable dtadver = new DataTable();
            //AdvertisementManager advertisementManager = new AdvertisementManager();
            //dtadver = advertisementManager.GetAdvertisementConnToBanner(Common.ToInt32(adver.advertisementColumnId));
            //if (dtadver.Rows.Count > 0)
            //{
            //    for (int i = 0; i < dtadver.Rows.Count; i++)
            //    {
            //        advertisementManager.DelAdvertisementConnToBanner(Common.ToInt32(dtadver.Rows[i]["advertisementColumnId"]));
            //    }

            //}
            return manager.InsertNewAdvertisementConnBanner(adver);
        } 
    }
}
