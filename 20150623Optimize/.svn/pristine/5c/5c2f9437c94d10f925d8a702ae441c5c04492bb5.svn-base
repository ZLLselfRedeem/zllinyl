using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using VA.CacheLogic.OrderClient;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.Model.Interface;
using VAGastronomistMobileApp.Model.QueryObject;
using VAGastronomistMobileApp.SQLServerDAL;

namespace VAGastronomistMobileApp.WebPageDll
{
    public partial class CouponOperate
    {
        private static CouponManager _CouponManager = new CouponManager();

        public static bool Add(ICoupon Entity)
        {
            return _CouponManager.Add(Entity);
        }

        /// <summary>
        /// 修改抵扣券已发送数量 add by zhujinlei 2015/06/17
        /// </summary>
        /// <param name="CouponId"></param>
        /// <returns></returns>
        public static bool Update(int CouponId)
        {
            return _CouponManager.CheckHasCouponAndUpdate(CouponId);
        }

        public static bool Update(ICoupon Entity)
        {
            return _CouponManager.Update(Entity);
        }
        public static bool DeleteEntity(ICoupon Entity)
        {
            return _CouponManager.DeleteEntity(Entity);
        }
        public static ICoupon GetEntityById(int CouponId)
        {
            return _CouponManager.GetEntityById(CouponId);
        }

        public static ICoupon GetEntityByIdQuery(int CouponId)
        {
            return _CouponManager.GetEntityByIdQuery(CouponId);
        }

        
        public static List<ICoupon> GetListByQuery(int pageSize, int pageIndex, CouponQueryObject queryObject = null,
            CouponOrderColumn orderColumn = CouponOrderColumn.CouponId, SortOrder order = SortOrder.Descending)
        {
            return _CouponManager.GetListByQuery(pageSize, pageIndex, queryObject, orderColumn, order);
        }
        public static List<ICoupon> GetListByQuery(CouponQueryObject queryObject = null, CouponOrderColumn orderColumn = CouponOrderColumn.CouponId,
            SortOrder order = SortOrder.Descending)
        {
            return _CouponManager.GetListByQuery(queryObject, orderColumn, order);
        }
        public static long GetCountByQuery(CouponQueryObject queryObject)
        {
            return _CouponManager.GetCountByQuery(queryObject);
        }
        /// <summary>
        /// 查询点单使用抵价券抵扣的金额
        /// [0]  RequirementMoney [1] DeductibleAmount
        /// </summary>
        /// <param name="preOrder19DianId"></param>
        /// <returns></returns>
        public List<double> GetAmountOfOrder(long preOrder19DianId)
        {
            return _CouponManager.GetAmountOfOrder(preOrder19DianId);
        }

        /// <summary>
        /// 新增抵价券使用记录
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        public long InsertCouponUseRecord(CouponUseRecord record)
        {
            return _CouponManager.InsertCouponUseRecord(record);
        }

        /// <summary>
        /// 查询点单对应的抵扣券是否已退款返还
        /// </summary>
        /// <param name="preOrder19DianId"></param>
        /// <returns></returns>
        public bool IsCouponRefund(long preOrder19DianId)
        {
            return _CouponManager.IsCouponRefund(preOrder19DianId);
        }

        /// <summary>
        /// 可以分享优惠券门店白名单
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public static bool CheckShopIsWhite(int cityId,int shopId)
        {
            string[] shops = new CouponCacheLogic().GetShareCouponsWhiteShops(cityId);
            if (!shops.Any())
            {
                return true;//如果没有配置值，则全部为白名单
            }
            var data = shops.Where(s => Common.ToInt32(s) == shopId).ToList();
            return data.Any();
        }

        public static bool CheckCityIsWhite(int cityId)
        {
            string[] shops = new CouponCacheLogic().GetShareCouponsWhiteCitys();
            if ( shops== null  || shops.Length == 0)
            {
                return true;//如果没有配置值，则全部为白名单
            }
            return shops.Contains(cityId.ToString()); 
        }

        public static VAPromotionActivityList GetPromotionActivityList(int pageSize, int pageIndex, int shopId)
        {
            VAPromotionActivityList ActivityList = new VAPromotionActivityList();
            CouponQueryObject couponQueryObject = new CouponQueryObject()
            {
                ShopId = shopId
            };
            //查询指定门店对应的所有抵扣券
            List<VAPromotionActivity> promotionActivityList = new List<VAPromotionActivity>();
            long couponCount = GetCountByQuery(couponQueryObject);
            List<ICoupon> CouponList = GetListByQuery(pageSize, pageIndex, couponQueryObject);
            if (CouponList != null && CouponList.Any())
            {
                if (couponCount > (pageSize * pageIndex))
                {
                    ActivityList.isMore = true;
                }
                else
                {
                    ActivityList.isMore = false;
                }

                IEnumerable<ICoupon> query = null;
                query = from items in CouponList orderby items.CreateTime descending select items;

                foreach (Coupon coupon in query)
                {
                    VAPromotionActivity promotionActivity = new VAPromotionActivity();
                    promotionActivity.activityId = coupon.CouponId;
                    promotionActivity.activityType = PromotionActivityType.coupon;
                    promotionActivity.activityName = coupon.CouponName;
                    switch (coupon.State)
                    {
                        case 0:
                            promotionActivity.activityStatus = "已失效";
                            break;
                        case 1:
                            if (coupon.StartDate > DateTime.Now)
                            {
                                promotionActivity.activityStatus = "未开始";
                            }
                            if (coupon.StartDate <= DateTime.Now && coupon.EndDate > DateTime.Now)
                            {
                                promotionActivity.activityStatus = "进行中";
                            }
                            if (coupon.EndDate <= DateTime.Now)
                            {
                                promotionActivity.activityStatus = "已结束";
                            }
                            break;
                        case -1:
                            promotionActivity.activityStatus = "待审核";
                            break;
                        case -2:
                            promotionActivity.activityStatus = "未通过";
                            break;
                    }
                    promotionActivityList.Add(promotionActivity);
                }
            }
            ActivityList.PromotionActivity = promotionActivityList;
            return ActivityList;
        }

        public static string VACouponAdd(int shopId, string couponName, DateTime startDate, DateTime endDate, double RequirementMoney, double DeductibleAmount, int ValidityPeriod, int quantity, int employeeID,double MaxAmount)
        {
            SybMsg2 sybMsg = new SybMsg2();
            if (shopId <= 0)
            {
                sybMsg.Insert(-2, "店铺信息错误");
                return sybMsg.Value;
            }
            if (employeeID <= 0)
            {
                sybMsg.Insert(-2, "员工信息错误");
                return sybMsg.Value;
            }
            if (string.IsNullOrEmpty(couponName))
            {
                sybMsg.Insert(-2, "抵扣券名称不能为空");
                return sybMsg.Value;
            }
            if (RequirementMoney <= 0)
            {
                sybMsg.Insert(-2, "条件金额必须大于零");
                return sybMsg.Value;
            }
            if (DeductibleAmount <= 0)
            {
                sybMsg.Insert(-2, "抵扣金额必须大于零");
                return sybMsg.Value;
            }
            if (DeductibleAmount >= RequirementMoney)
            {
                sybMsg.Insert(-2, "条件金额必须大于抵扣金额");
                return sybMsg.Value;
            }
            if (ValidityPeriod <= 0)
            {
                sybMsg.Insert(-2, "有效期必须大于零");
                return sybMsg.Value;
            }
            if (quantity <= 0)
            {
                sybMsg.Insert(-2, "数量必须大于零");
                return sybMsg.Value;
            }
            if (!string.IsNullOrEmpty(couponName) && couponName.Length > 15)
            {
                sybMsg.Insert(-2, "抵扣券名称不能超过15个字");
                return sybMsg.Value;
            }
            if (MaxAmount / DeductibleAmount <= 0 || MaxAmount % DeductibleAmount != 0)
            {
                sybMsg.Insert(-2, "最多减金额必须为抵用金额的整数倍！");
                return sybMsg.Value;
            }
            ShopOperate shopOperate = new ShopOperate();
            var shop = shopOperate.QueryShop(shopId);
            var shopCoordinate = shopOperate.QueryShopCoordinate(2, shop.shopID);


            Coupon entity = new Coupon()
            {
                SendCount = 0,
                SheetNumber = quantity,
                ShopId = shopId,
                SortOrder = 0,
                EndDate = endDate,
                StartDate = startDate,
                State = -1,//待审核
                CouponName = couponName,
                DeductibleAmount = DeductibleAmount,
                RequirementMoney = RequirementMoney,
                ValidityPeriod = ValidityPeriod,
                Remark = string.Empty,
                CreateTime = DateTime.Now,
                LastUpdatedBy = employeeID,
                CreatedBy = employeeID,
                LastUpdatedTime = DateTime.Now,
                IsGot = false,
                ShopName = shop.shopName,
                ShopAddress = shop.shopAddress,
                CouponType = 1,
                IsDisplay = true,
                Image = shop.publicityPhotoPath,
                Latitude = shopCoordinate.latitude,
                Longitude = shopCoordinate.longitude,
                CityId = shop.cityID,
                SubsidyAmount = 0,
                MaxAmount=MaxAmount


                //SendCount = 0,
                //SheetNumber = quantity,
                //ShopId = shopId,
                //SortOrder = 0,
                ////EndDate = endDate.AddDays(0.99999),
                //EndDate = Common.ToDateTime(endDate.ToString("yyyy-MM-dd 23:59:59")),
                //StartDate = startDate,
                //State = -1,
                //CouponName = couponName,
                //DeductibleAmount = DeductibleAmount,
                //RequirementMoney = RequirementMoney,
                //ValidityPeriod = ValidityPeriod,
                //Remark = "悠先服务申请发布抵扣券",
                //CreateTime = DateTime.Now,
                //CreatedBy = employeeID,
                //LastUpdatedBy = employeeID,
                //LastUpdatedTime = DateTime.Now,
                //MaxAmount = MaxAmount
            };
            if (CouponOperate.Add(entity))
            {
                sybMsg.Insert(0, "申请成功");
            }
            else
            {
                sybMsg.Insert(-1, "数据提交失败，请重试");
            }
            return sybMsg.Value;
        }


        public List<CustomerCouponDetail> SelectCustomerCouponDetail(string phone, int cityId, out bool isHaveMore)
        {
            return _couponManager.SelectCustomerCouponDetail(phone, cityId, out isHaveMore);
        }
    }
}
