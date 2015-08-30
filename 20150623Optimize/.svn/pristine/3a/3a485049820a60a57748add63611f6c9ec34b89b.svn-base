using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL;
using System.Transactions;
using System.Data;

namespace VAGastronomistMobileApp.WebPageDll
{
    public partial class CouponGetDetailOperate
    {
        private static CouponGetDetailManager _CouponGetDetailManager = new CouponGetDetailManager();

        public static bool Add(CouponGetDetail Entity)
        {
            return _CouponGetDetailManager.Add(Entity);
        }

        public static bool Update(CouponGetDetail Entity)
        {
            return _CouponGetDetailManager.Update(Entity);
        }
        public static bool DeleteEntity(CouponGetDetail Entity)
        {
            return _CouponGetDetailManager.DeleteEntity(Entity);
        }
        public static CouponGetDetail GetEntityById(long cuponGetDetailId)
        {
            return _CouponGetDetailManager.GetEntityById(cuponGetDetailId);
        }
        public static List<CouponGetDetail> GetListByQuery(int pageSize, int pageIndex, CouponGetDetailQueryObject queryObject = null,
            CouponGetDetailOrderColumn orderColumn = CouponGetDetailOrderColumn.CouponGetDetailId, SortOrder order = SortOrder.Descending)
        {
            return _CouponGetDetailManager.GetListByQuery(pageSize, pageIndex, queryObject, orderColumn, order);
        }
        public static List<CouponGetDetail> GetListByQuery(CouponGetDetailQueryObject queryObject = null, CouponGetDetailOrderColumn orderColumn = CouponGetDetailOrderColumn.CouponGetDetailId,
            SortOrder order = SortOrder.Descending)
        {
            return _CouponGetDetailManager.GetListByQuery(queryObject, orderColumn, order);
        }
        public static long GetCountByQuery(CouponGetDetailQueryObject queryObject)
        {
            return _CouponGetDetailManager.GetCountByQuery(queryObject);
        }

        /// <summary>
        /// 支付或退款时更新用户优惠券状态
        /// </summary>
        /// <param name="couponGetDetailID"></param>
        /// <param name="preOrder19DianId"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public bool UpdateCouponState(long couponGetDetailID, long preOrder19DianId, CouponUseStateType type)
        {
            return _CouponGetDetailManager.UpdateCouponState(couponGetDetailID, preOrder19DianId, type);
        }

        /// <summary>
        /// 悠先点菜用户退款时，返还抵价券
        /// </summary>
        /// <param name="preOrder19DianId"></param>
        /// <param name="shopName"></param>
        /// <returns></returns>
        public bool RefundCustomerCoupon(long preOrder19DianId, string shopName)
        {
            CouponGetDetail detail = _CouponGetDetailManager.SelectCouponGetDetail(preOrder19DianId);
            CouponManager couponManager = new CouponManager();

            if (detail != null && detail.CouponGetDetailID > 0)
            {
                Coupon c = couponManager.GetEntityById(detail.CouponId);
                if (c.TicketType.Equals(0))
                {//普通券不做退回
                    return true;
                }

                CouponUseRecord record = new CouponUseRecord()
                {
                    CouponGetDetailID = detail.CouponGetDetailID,
                    CouponId = detail.CouponId,
                    PreOrder19DianId = preOrder19DianId,
                    StateType = CouponUseStateType.refund,
                    ChangeReason = shopName + "退款",
                    ChangeTime = DateTime.Now
                };
                using (TransactionScope ts = new TransactionScope())
                {
                    bool a = UpdateCouponState(detail.CouponGetDetailID, 0, CouponUseStateType.refund);

                    long b = couponManager.InsertCouponUseRecord(record);

                    if (a && b > 0)
                    {
                        ts.Complete();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            else
            {
                return true;
            }
        }

        public DataTable GetCouponCount(int cityid, string OperateBeginTime, string OperateEndTime)
        {
            CouponManager cgdm = new CouponManager();
            return cgdm.GetCouponCount(cityid, OperateBeginTime, OperateEndTime);
        }

        /// <summary>
        /// 某张券的领取情况
        /// </summary>
        /// <param name="CouponId">券ID</param>
        /// <param name="Used">是否使用过</param>
        /// <returns></returns>
        public long GetCountByCouponId(int CouponId, bool Used)
        {
            CouponManager cgdm = new CouponManager();
            return cgdm.GetCountByCouponId(CouponId, Used);
        }
    }
}
