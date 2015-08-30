﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VA.CacheLogic;
using VA.CacheLogic.OrderClient;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.Model.Interface;
using VAGastronomistMobileApp.Model.QueryObject;
using VAGastronomistMobileApp.SQLServerDAL;

namespace VAGastronomistMobileApp.WebPageDll
{
    /// <summary>
    /// 抵价券业务逻辑扩展类（数据访问严格经过此入口，有利于缓存业务提取和封装。）
    /// </summary>
    public partial class CouponOperate
    {
        private readonly CouponManager _couponManager = new CouponManager();
        /// <summary>
        /// 查询当前城市有有效优惠券的门店编号
        /// </summary>
        /// <param name="cityId">城市编号</param>
        /// <returns></returns>
        public List<int> GetHadCouponShopId(int cityId)
        {
            string[] shops = new CouponCacheLogic().GetShareCouponsWhiteShops(cityId);
            if (shops.Any())
            {
                var list = _couponManager.SelectHadCouponShopId(cityId);
                if (list.Any())
                {
                    return (from s in shops
                            join l in list on Common.ToInt32(s) equals l
                            select Common.ToInt32(s)).ToList();
                }
                return new List<int>();
            }
            return _couponManager.SelectHadCouponShopId(cityId);

            //string[] citys = new CouponCacheLogic().GetShareCouponsWhiteCitys();
            //if (citys.Any())
            //{
            //    if (((IList)citys).Contains(citys))//城市在白名单
            //    {
            //        string[] shops = new CouponCacheLogic().GetShareCouponsWhiteShops(cityId);
            //        if (shops.Any())
            //        {
            //            var list = _couponManager.SelectHadCouponShopId(cityId);
            //            if (list.Any())
            //            {
            //                return (from s in shops
            //                        join l in list on Common.ToInt32(s) equals l
            //                        select Common.ToInt32(s)).ToList();
            //            }                       
            //        }
            //    }
            //    return new List<int>();
            //}
            //else
            //{
            //    return _couponManager.SelectHadCouponShopId(cityId);
            //}
        }

        /// <summary>
        /// 查询当恰手机号码用户拥有的可用有效优惠券数量
        /// </summary>
        /// <param name="phone">手机号码</param>
        /// <returns></returns>
        public int GetHadCouponCount(string phone)
        {
            return _couponManager.SelectHadCouponCount(phone);
        }
        /// <summary>
        /// 检查用户是否有未查看的有效红包
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        public bool HaveUnCheckCoupon(string phone)
        {
            return _couponManager.HaveUnCheckCoupon(phone);
        }

        /// <summary>
        /// 查询当前手机号码用户拥有可用优惠券列表
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="isHaveMore"></param>
        /// <param name="cityId">城市编号</param>
        /// <returns></returns>
        public List<ClientCouponPacketDetail> GetHadCouponDetail(string phone, int pageIndex, int pageSize, int cityId, out bool isHaveMore)
        {
            return _couponManager.SelectHadCouponDetail(phone, pageIndex, pageSize, cityId, out isHaveMore);
        }

        /// <summary>
        /// 查询当前点单是否已分享过优惠券
        /// </summary>
        /// <param name="orderId">点单流水号</param>
        /// <returns></returns>
        public bool IsHaveShareCoupon(long orderId)
        {
            return _couponManager.SelectIsHaveShareCoupon(orderId);
        }

        /// <summary>
        /// 查询当前点单抵扣金额
        /// </summary>
        /// <param name="preOrder19dianID"></param>
        /// <returns></returns>
        public double[] GetCouponDeductibleAmount(long preOrder19dianID)
        {
            return _couponManager.SelectCouponDeductibleAmount(preOrder19dianID);
        }

        /// <summary>
        /// 查询当前支付中点单对应使用的优惠券编号
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="appType"></param>
        /// <param name="clientBulid"></param>
        /// <returns></returns>
        public int GetCouponGetDetailIdByOrderId(long orderId, VAAppType appType, string clientBulid)
        {
            if (Common.CheckLatestBuild_February(appType, clientBulid))
            {
                return _couponManager.SelecCouponGetDetailIdByOrderId(orderId);
            }
            return 0;
        }

        /// <summary>
        /// 查询当前用户当前门店可用抵价券列表（支付接口）
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public List<OrderPaymentCouponDetail> GetShopCouponDetails(string phone, int shopId)
        {
            // 手机号为空，代表末登录直接返回空对象
            if (string.IsNullOrEmpty(phone))
            {
                return new List<OrderPaymentCouponDetail>();
            }

            var couponGetDetailVs = CouponGetDetailVOperate.GetListByQuery(new CouponGetDetailVQueryObject()
            {
                MobilePhoneNumber = phone,
                State = 1,
                ShopId = shopId,
                ValidityEndFrom = DateTime.Now
            });
            // 优惠卷列表中会显示状态为3的卷（表示使用中），这里为了同步也需要可以使用状态为3的卷
            // add by zhujinlei 2015/06/02
            couponGetDetailVs.AddRange(CouponGetDetailVOperate.GetListByQuery(new CouponGetDetailVQueryObject()
            {
                MobilePhoneNumber = phone,
                State = 3,
                ShopId = shopId,
                ValidityEndFrom = DateTime.Now
            }));

            var orderPaymentCouponDetails = couponGetDetailVs.Select(e =>
                                             new OrderPaymentCouponDetail()
                                             {
                                                 couponId = e.CouponGetDetailId,
                                                 couponName = string.Format("满{0}减{1}", e.RequirementMoney, e.DeductibleAmount),
                                                 maxAmount = e.MaxAmount == null ? e.DeductibleAmount : e.MaxAmount.Value,
                                                 couponValidityEnd = Common.ToSecondFrom1970(e.ValidityEnd),
                                                 deductibleAmount = e.DeductibleAmount,
                                                 requirementMoney = e.RequirementMoney,
                                                 couponType = CouponTypeEnum.OneSelf
                                             }).ToList();

            return orderPaymentCouponDetails;
        }

        public List<OrderPaymentCouponDetail> GetShopCouponDetails(string phone, int shopId, VAAppType apptype, string currectBuild)
        {
            // 手机号为空，代表末登录直接返回空对象
            //if (string.IsNullOrEmpty(phone))
            //{
            //    return new List<OrderPaymentCouponDetail>();
            //}

            var couponGetDetailVs = CouponGetDetailVOperate.GetListByQuery(new CouponGetDetailVQueryObject()
            {
                MobilePhoneNumber = phone,
                State = 1,
                ShopId = shopId,
                ValidityEndFrom = DateTime.Now
            });
            // 优惠卷列表中会显示状态为3的卷（表示使用中），这里为了同步也需要可以使用状态为3的卷
            // add by zhujinlei 2015/06/02
            couponGetDetailVs.AddRange(CouponGetDetailVOperate.GetListByQuery(new CouponGetDetailVQueryObject()
            {
                MobilePhoneNumber = phone,
                State = 3,
                ShopId = shopId,
                ValidityEndFrom = DateTime.Now
            }));

            var orderPaymentCouponDetails = couponGetDetailVs.Select(e =>
                                             new OrderPaymentCouponDetail()
                                             {
                                                 couponId = e.CouponGetDetailId,
                                                 couponName = string.Format("满{0}减{1}", e.RequirementMoney, e.DeductibleAmount),
                                                 maxAmount = e.MaxAmount == null ? e.DeductibleAmount : e.MaxAmount.Value,
                                                 couponValidityEnd = Common.ToSecondFrom1970(e.ValidityEnd),
                                                 deductibleAmount = e.DeductibleAmount,
                                                 requirementMoney = e.RequirementMoney,
                                                 couponType = CouponTypeEnum.OneSelf
                                             }).ToList();

            // 最新版本才需要加载店铺的抵扣券
            if (Common.CheckLatestBuild_201506(apptype, currectBuild))
            {
                // 获取店铺的优惠卷
                orderPaymentCouponDetails.AddRange(CouponGetDetailVOperate.GetOrderPaymentCouponDetail(shopId).Select(e =>
                                                 new OrderPaymentCouponDetail()
                                                 {
                                                     couponId = e.CouponGetDetailId,
                                                     couponName = string.Format("满{0}减{1}", e.RequirementMoney, e.DeductibleAmount),
                                                     maxAmount = e.MaxAmount == null ? e.DeductibleAmount : e.MaxAmount.Value,
                                                     couponValidityEnd = Common.ToSecondFrom1970(e.ValidityEnd),
                                                     deductibleAmount = e.DeductibleAmount,
                                                     requirementMoney = e.RequirementMoney,
                                                     couponType = CouponTypeEnum.Bussiness
                                                 }));
            }
            orderPaymentCouponDetails =orderPaymentCouponDetails.Distinct<OrderPaymentCouponDetail>().ToList();
            return orderPaymentCouponDetails;
        }

        /// <summary>
        /// 查询当前店铺可用的抵扣券列表 add by zhujinlei 2015/06/17
        /// </summary>
        /// <param name="shopId">店铺ID</param>
        /// <returns></returns>
        public List<OrderPaymentCouponDetail> GetShopCouponDetails(int shopId)
        {
            var couponGetDetailVs = CouponGetDetailVOperate.GetOrderPaymentCouponDetail(shopId);

            var orderPaymentCouponDetails = couponGetDetailVs.Select(e =>
                                             new OrderPaymentCouponDetail()
                                             {
                                                 couponId = e.CouponGetDetailId,
                                                 couponName = string.Format("满{0}减{1}", e.RequirementMoney, e.DeductibleAmount),
                                                 maxAmount = e.MaxAmount == null ? e.DeductibleAmount : e.MaxAmount.Value,
                                                 couponValidityEnd = Common.ToSecondFrom1970(e.ValidityEnd),
                                                 deductibleAmount = e.DeductibleAmount,
                                                 requirementMoney = e.RequirementMoney,
                                                 couponType = CouponTypeEnum.Bussiness
                                             }).ToList();

            return orderPaymentCouponDetails;
        }

        /// <summary>
        /// 计算点单支付使用优惠券抵扣后的价格
        /// </summary> 
        public double GetCurrectPreOrderAfterDiscountAmount(int couponGetDetailId, double serverUxianPriceSum, double deductionCoupon, VAAppType appType, string clientBuild, out int result, out double realDeductibleAmount)
        {
            realDeductibleAmount = 0;
            bool latestBuild = Common.CheckLatestBuild_February(appType, clientBuild);
            if (latestBuild == false)//老版本，不支持抵价券抵扣
            {
                result = -1;
                return serverUxianPriceSum;
            }
            if (couponGetDetailId <= 0)
            {
                result = -1;
                return serverUxianPriceSum;
            }
            var couponGetDetail = CouponGetDetailOperate.GetEntityById(couponGetDetailId);
            if (couponGetDetail == null)
            {
                result = -2;//没有可使用的优惠券
                return serverUxianPriceSum;
            }
            if (deductionCoupon < couponGetDetail.RequirementMoney)
            {
                result = -3;//没有满足用户选择的优惠券可使用
                return serverUxianPriceSum;
            }
            var coupon = CouponOperate.GetEntityById(couponGetDetail.CouponId);
            if (coupon == null)
            {
                result = -1;
                return serverUxianPriceSum;
            }
            realDeductibleAmount = coupon.DeductibleAmount;

            result = 1;
            return Common.ToDouble(serverUxianPriceSum - realDeductibleAmount);
        }

        public static double GetAmountAfterUseCoupon(int couponGetDetailId, double serverUxianPriceSum, double deductionCoupon,
                            out int result, out double realDeductibleAmount)
        {
            realDeductibleAmount = 0;
            if (couponGetDetailId <= 0)
            {
                result = -1;
                return serverUxianPriceSum;
            }
            var couponGetDetail = CouponGetDetailOperate.GetEntityById(couponGetDetailId);
            if (couponGetDetail == null)
            {
                result = -2;//没有可使用的优惠券
                return serverUxianPriceSum;
            }
            if (couponGetDetail.RequirementMoney > deductionCoupon)
            {
                result = -3;//没有满足用户选择的优惠券可使用
                return serverUxianPriceSum;
            }
            var coupon = CouponOperate.GetEntityById(couponGetDetail.CouponId);
            if (coupon == null)
            {
                result = -1;
                return serverUxianPriceSum;
            }
            int foldCount = (int)(deductionCoupon / couponGetDetail.RequirementMoney);
            if (foldCount * couponGetDetail.DeductibleAmount > coupon.MaxAmount)
            {
                realDeductibleAmount = coupon.MaxAmount;
            }
            else
            {
                realDeductibleAmount = foldCount * couponGetDetail.DeductibleAmount;
            }
            result = 1;
            return Common.ToDouble(serverUxianPriceSum - realDeductibleAmount);
        }

        /// <summary>
        /// 扣除抵扣卷后还需要支付的金额
        /// </summary>
        /// <param name="couponGetDetailId">抵扣卷详情ID CouponGetDetailID 或者 抵扣卷ID CouponID</param>
        /// <param name="serverUxianPriceSum">客户端计算出可以用抵扣卷的金额</param>
        /// <param name="deductionCoupon">客户端计算出可以用抵扣卷的金额</param>
        /// <param name="couponType">客户端使用的低扣卷金额1.自己领取的2.店铺的优惠卷</param>
        /// <param name="result">标记是否使用优惠券</param>
        /// <param name="realDeductibleAmount">使用优惠卷的金额</param>
        /// <param name="couponGetDetailIDNew">新产生的抵扣券ID</param>
        /// <returns></returns>
        public static double GetAmountAfterUseCoupon(int couponGetDetailId, double serverUxianPriceSum, double deductionCoupon, int couponType, string mobilePhoneNumber, long preOrder19DianId,
                            out int result, out double realDeductibleAmount, out int couponGetDetailIDNew)
        {
            realDeductibleAmount = 0;
            couponGetDetailIDNew = 0;
            if (couponGetDetailId <= 0)
            {
                result = -1;
                return serverUxianPriceSum;
            }

            // 自己领取的抵扣券需要判断之前是否存在相同的订单preOrder19DianId,存在需要更改成0
            if (couponType == (int)CouponTypeEnum.OneSelf)
            {
                var listCouponGetDetail = CouponGetDetailOperate.GetListByQuery(new CouponGetDetailQueryObject() { PreOrder19DianId = preOrder19DianId });
                foreach (var obj in listCouponGetDetail)
                {
                    obj.PreOrder19DianId = 0;
                    CouponGetDetailOperate.Update(obj);
                }
            }

            // 门店的优惠卷
            if (couponType == (int)CouponTypeEnum.Bussiness)
            {
                //1、更新coupon表中的sendCound字段，判断是否能成功(成功表示还有优惠券）
                // 门店的优惠卷
                var hasCoupon = _CouponManager.CheckHasCouponAndUpdate(couponGetDetailId);

                if (!hasCoupon)
                {
                    // 修改Coupon表中的IsGot为0
                    _CouponManager.Update(couponGetDetailId, false);
                    result = -4; // Coupon表没有可领用的抵扣券
                    return serverUxianPriceSum;
                }

                // 判断订单preOrder19DianId 是否存在已领用的抵券,存在后,需要删除
                CouponGetDetailOperate.DeleteEntity(new CouponGetDetail() { PreOrder19DianId = preOrder19DianId });

                //2、成功后插入一条未使用的抵扣券到用户抵扣券表中
                var objCoupon = CouponOperate.GetEntityById(couponGetDetailId);
                if (objCoupon == null)
                {
                    result = -2;//没有可使用的优惠券
                    return serverUxianPriceSum;
                }
                var objCouponGetDetail = new CouponGetDetail()
                {
                    CouponDetailNumber = mobilePhoneNumber.Substring(7, 4) + VAGastronomistMobileApp.WebPageDll.Common.ToSecondFrom1970(DateTime.Now).ToString(),
                    GetTime = DateTime.Now,
                    ValidityEnd = DateTime.Today.AddDays(objCoupon.ValidityPeriod).AddDays(1).AddSeconds(-1),
                    RequirementMoney = objCoupon.RequirementMoney,
                    DeductibleAmount = objCoupon.DeductibleAmount,
                    State = (int)CouponUseStateType.auto,
                    MobilePhoneNumber = mobilePhoneNumber,
                    CouponId = couponGetDetailId,
                    PreOrder19DianId = preOrder19DianId
                };
                bool isCouponSuccess = CouponGetDetailOperate.Add(objCouponGetDetail);

                //3、把插入后生成的couponGetDetailID 赋值给旧的couponGetDetailId(实际为couponID)
                if (isCouponSuccess)
                {
                    couponGetDetailId = (int)objCouponGetDetail.CouponGetDetailID;
                    couponGetDetailIDNew = couponGetDetailId;
                }
                else
                {
                    result = -1;
                    return serverUxianPriceSum;
                }
            }

            var couponGetDetail = CouponGetDetailOperate.GetEntityById(couponGetDetailId);
            if (couponGetDetail == null)
            {
                result = -2;//没有可使用的优惠券
                return serverUxianPriceSum;
            }
            if (couponGetDetail.RequirementMoney > deductionCoupon)
            {
                result = -3;//没有满足用户选择的优惠券可使用
                return serverUxianPriceSum;
            }
            var coupon = CouponOperate.GetEntityById(couponGetDetail.CouponId);



            LogDll.LogManager.WriteLog(LogDll.LogFile.Error, string.Format("时间:{0},RequirementMoney:{1},DeductibleAmount:{2}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), couponGetDetail.RequirementMoney, couponGetDetail.DeductibleAmount));


            if (coupon == null)
            {
                result = -1;
                return serverUxianPriceSum;
            }
            int foldCount = (int)(deductionCoupon / couponGetDetail.RequirementMoney);
            if (foldCount * couponGetDetail.DeductibleAmount > coupon.MaxAmount)
            {
                realDeductibleAmount = coupon.MaxAmount;
            }
            else
            {
                realDeductibleAmount = foldCount * couponGetDetail.DeductibleAmount;
            }
            result = 1;
            return Common.ToDouble(serverUxianPriceSum - realDeductibleAmount);
        }

        /// <summary>
        /// 新增点单支付关联优惠券记录，修改优惠券使用状态=
        /// </summary>
        /// <param name="shopName"></param>
        /// <param name="couponGetDetailId">用户获取优惠券编号（对应客户端传递couponId）</param>
        /// <param name="preOrderId"></param>
        /// <returns></returns>
        public bool AddPayOrderConnCoupon(string shopName, long couponGetDetailId, long preOrderId, double realDeductibleAmount, double calculationAmount)
        {
            var model = new CouponUseRecord()
            {
                Id = 0,
                CouponGetDetailID = couponGetDetailId,
                CouponId = 0,
                StateType = CouponUseStateType.pay,
                ChangeReason = shopName + "支付",
                ChangeTime = DateTime.Now,
                PreOrder19DianId = preOrderId
            };
            var couponGetDetail = CouponGetDetailOperate.GetEntityById(couponGetDetailId);
            if (couponGetDetail == null)
            {
                return false;
            }
            couponGetDetail.PreOrder19DianId = preOrderId;
            couponGetDetail.State = (int)CouponUseStateType.pay;
            couponGetDetail.RealDeductibleAmount = realDeductibleAmount;
            couponGetDetail.UseTime = DateTime.Now;
            couponGetDetail.CalculationAmount = calculationAmount;

            
            var flag = CouponGetDetailOperate.Update(couponGetDetail);
            model.CouponId = couponGetDetail.CouponId;

            return InsertCouponUseRecord(model) > 0 && flag;
        }

        /// <summary>
        /// 抵扣券使用完成后记录
        /// </summary>
        /// <param name="shopName">店铺名称</param>
        /// <param name="couponGetDetailId">抵扣券详情ID/抵扣券ID</param>
        /// <param name="preOrderId">订单ID</param>
        /// <param name="realDeductibleAmount">实际抵扣金额</param>
        /// <param name="couponType">抵扣券类型1、自己的 2、店铺的</param>
        /// <param name="mobilePhoneNumber">用户手机号码</param>
        /// <param name="preOrder19DianId">订单ID</param>
        /// <param name="hasCoupon">是否还有可用的抵扣券</param>
        /// <returns></returns>
        public bool AddPayOrderConnCoupon(string shopName, int couponGetDetailId, long preOrderId, double realDeductibleAmount, int couponType, string mobilePhoneNumber, out bool hasCoupon)
        {
            var result = false;
            hasCoupon = true;
            var model = new CouponUseRecord()
            {
                Id = 0,
                CouponGetDetailID = 0,
                CouponId = 0,
                StateType = CouponUseStateType.pay,
                ChangeReason = shopName + "支付",
                ChangeTime = DateTime.Now,
                PreOrder19DianId = preOrderId
            };
            var flag = false;

            if (couponType == (int)CouponTypeEnum.OneSelf)
            {
                model.CouponGetDetailID = couponGetDetailId;
                var couponGetDetail = CouponGetDetailOperate.GetEntityById(couponGetDetailId);
                if (couponGetDetail == null)
                {
                    return false;
                }
                couponGetDetail.PreOrder19DianId = preOrderId;
                couponGetDetail.State = (int)CouponUseStateType.pay;
                couponGetDetail.RealDeductibleAmount = realDeductibleAmount;
                couponGetDetail.UseTime = DateTime.Now;
                flag = CouponGetDetailOperate.Update(couponGetDetail);
                model.CouponId = couponGetDetail.CouponId;

                result = InsertCouponUseRecord(model) > 0 && flag;
            }
            // 店铺的抵扣券
            if (couponType == (int)CouponTypeEnum.Bussiness)
            {
                model.CouponId = couponGetDetailId;
                //1、先在CouponGetDetail表里添加一条已使用的抵扣券
                var objCoupon = CouponOperate.GetEntityById(couponGetDetailId);
                if (objCoupon == null)
                {
                    return false;
                }
                var objCouponGetDetail = new CouponGetDetail()
                {
                    CouponDetailNumber = mobilePhoneNumber.Substring(7, 4) + VAGastronomistMobileApp.WebPageDll.Common.ToSecondFrom1970(DateTime.Now).ToString(),
                    GetTime = DateTime.Now,
                    ValidityEnd = DateTime.Today.AddDays(objCoupon.ValidityPeriod).AddDays(1).AddSeconds(-1),
                    RequirementMoney = objCoupon.RequirementMoney,
                    DeductibleAmount = objCoupon.DeductibleAmount,
                    State = (int)CouponUseStateType.pay,
                    MobilePhoneNumber = mobilePhoneNumber,
                    CouponId = couponGetDetailId,
                    UseTime = DateTime.Now,
                    PreOrder19DianId = preOrderId,
                    RealDeductibleAmount = realDeductibleAmount
                };
                bool isCouponSuccess = CouponGetDetailOperate.Add(objCouponGetDetail);
                if (isCouponSuccess)
                {
                    model.CouponGetDetailID = objCouponGetDetail.CouponGetDetailID;
                }

                //2、然后在CouponUseRecord表里添加一条日志记录
                bool isCouponUseRecord = InsertCouponUseRecord(model) > 0;

                //3、修改Coupon表里面的SendCount已发券数量 需要加lock
                bool isUpdateCouponSendCount = _couponManager.UpdateCouponSendCount(couponGetDetailId);

                hasCoupon = isUpdateCouponSendCount;
                result = isCouponSuccess && isCouponUseRecord && isUpdateCouponSendCount;
            }

            return result;
        }

        /// <summary>
        /// 查询个人优惠券列表信息接口
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public VAClientCouponPacketDetailResponse GetClientCouponPacketDetails(VAClientCouponPacketDetailRequest request)
        {
            VAClientCouponPacketDetailResponse response = new VAClientCouponPacketDetailResponse()
            {
                type = VAMessageType.CLIENT_CUSTOMER_COUPONDETAIL_RESPONSE,
                cookie = request.cookie,
                uuid = request.uuid
            };
            CheckCookieAndMsgtypeInfo checkResult = Common.CheckCookieAndMsgtype(request.cookie, request.uuid, (int)request.type, (int)VAMessageType.CLIENT_CUSTOMER_COUPONDETAIL_REQUEST);
            if (checkResult.result == VAResult.VA_OK)
            {
                if (checkResult.dtCustomer.Rows.Count <= 0)
                {
                    response.result = VAResult.VA_FAILED_CUSTOMER_NOT_FOUND;
                    return response;
                }
                if (Common.CheckLatestBuild_February(request.appType, request.clientBuild) == false)
                {
                    response.result = VAResult.VA_VERSION_IS_TOO_LOW;//老版本不处理，可直接return va_ok
                    return response;
                }
                string phone = Common.ToString(checkResult.dtCustomer.Rows[0]["mobilePhoneNumber"]);
                if (String.IsNullOrWhiteSpace(phone))
                {
                    response.result = VAResult.VA_FAILED_CUSTOMER_NOT_FOUND;
                    return response;
                }
                bool isHaveMore = false;
                var list = GetHadCouponDetail(phone, request.pageIndex, request.pageSize, request.cityId, out isHaveMore);
                list = (from q in list
                        select new ClientCouponPacketDetail()
                        {
                            shopId = q.shopId,
                            shopName = q.shopName,
                            shopLogoUrl = (q.shopLogoUrl.ToLower().EndsWith(".jpg") || q.shopLogoUrl.ToLower().EndsWith(".jpeg") || q.shopLogoUrl.ToLower().EndsWith(".png")) ?
                             WebConfig.CdnDomain + WebConfig.ImagePath + q.shopLogoUrl : "",
                            couponId = q.couponId,
                            couponDeductibleAmount = q.couponDeductibleAmount,
                            couponRequirementMoney = q.couponRequirementMoney,
                            couponValidityEnd = q.couponValidityEnd,
                            couponSatatus = q.couponSatatus,
                            CheckTime = q.CheckTime,
                            couponGetDetailId = q.couponGetDetailId,
                            cityId = q.cityId
                        }).ToList();

                UpdateCouponCheckTime(phone);
                response.couponPacketListDetails = list;
                response.isHaveMore = isHaveMore;
                response.result = VAResult.VA_OK;
            }
            else
            {
                response.result = checkResult.result;
            }
            return response;
        }

        /// <summary>
        /// 当前抵扣券
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public VAClientRebateDetailResponse GetCurrentRebateDetails(VAClientCouponPacketDetailRequest request)
        {
            VAClientRebateDetailResponse response = new VAClientRebateDetailResponse()
            {
                type = VAMessageType.CLIENT_CUSTOMER_CURRENTCOUPONDETAIL_RESPONSE,
                cookie = request.cookie,
                uuid = request.uuid
            };
            CheckCookieAndMsgtypeInfo checkResult = Common.CheckCookieAndMsgtype(request.cookie, request.uuid, (int)request.type, (int)VAMessageType.CLIENT_CUSTOMER_CURRENTCOUPONDETAIL_REQUEST);
            if (checkResult.result != VAResult.VA_OK)
            {
                response.result = checkResult.result;
                return response;
            }

            if (checkResult.dtCustomer.Rows.Count <= 0)
            {
                response.result = VAResult.VA_FAILED_CUSTOMER_NOT_FOUND;
                return response;
            }

            if (Common.CheckLatestBuild_February(request.appType, request.clientBuild) == false)
            {
                response.result = VAResult.VA_VERSION_IS_TOO_LOW;//老版本不处理，可直接return va_ok
                return response;
            }

            string phone = Common.ToString(checkResult.dtCustomer.Rows[0]["mobilePhoneNumber"]);
            if (String.IsNullOrWhiteSpace(phone))
            {
                response.result = VAResult.VA_FAILED_CUSTOMER_NOT_FOUND;
                return response;
            }

            var responseTuple = new CouponManager().GetCurrentRebates(phone, request.pageIndex, request.pageSize);
            var list = responseTuple.Item2.Select(p => new ClientDeductionVolumeDetail()
            {
                shopId = p.ShopId,
                shopName = p.ShopName,
                shopLogoUrl = (p.Image.ToLower().EndsWith(".jpg") || p.Image.ToLower().EndsWith(".jpeg") || p.Image.ToLower().EndsWith(".png")) ?
                 WebConfig.CdnDomain + WebConfig.ImagePath + p.Image : "",
                couponId = p.CouponId,
                couponDeductibleAmount = p.DeductibleAmount,
                couponRequirementMoney = p.RequirementMoney,
                couponValidityEnd = (p.ValidityEnd - new DateTime(1970, 1, 1).ToLocalTime()).TotalSeconds,
                couponSatatus = 1,
                CheckTime = p.CheckTime,
                couponGetDetailId = p.CouponGetDetailID,
                cityId = p.CityId,
                couponTime = p.UseTime == null ? 0 : ((DateTime)p.UseTime - new DateTime(1970, 1, 1).ToLocalTime()).TotalSeconds,
                usePrompt = string.Format(new SystemConfigCacheLogic().GetSystemConfig("CouponPrompt", "每满{0}可用|最多减{1}"), p.RequirementMoney, p.MaxAmount == 0 ? p.DeductibleAmount : p.MaxAmount)
            }).ToList();

            UpdateCouponCheckTime(phone);
            response.couponPacketListDetails = list;
            response.isHaveMore = responseTuple.Item1 - request.pageIndex * request.pageSize > 0;
            response.result = VAResult.VA_OK;
            return response;
        }

        /// <summary>
        /// 历史抵扣券
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public VAClientRebateDetailResponse GetHistoryRebateDetails(VAClientCouponPacketDetailRequest request)
        {
            VAClientRebateDetailResponse response = new VAClientRebateDetailResponse()
            {
                type = VAMessageType.CLIENT_CUSTOMER_HISTORYCOUPONDETAIL_RESPONSE,
                cookie = request.cookie,
                uuid = request.uuid
            };
            CheckCookieAndMsgtypeInfo checkResult = Common.CheckCookieAndMsgtype(request.cookie, request.uuid, (int)request.type, (int)VAMessageType.CLIENT_CUSTOMER_HISTORYCOUPONDETAIL_REQUEST);
            if (checkResult.result != VAResult.VA_OK)
            {
                response.result = checkResult.result;
                return response;
            }

            if (checkResult.dtCustomer.Rows.Count <= 0)
            {
                response.result = VAResult.VA_FAILED_CUSTOMER_NOT_FOUND;
                return response;
            }

            if (Common.CheckLatestBuild_February(request.appType, request.clientBuild) == false)
            {
                response.result = VAResult.VA_VERSION_IS_TOO_LOW;//老版本不处理，可直接return va_ok
                return response;
            }

            string phone = Common.ToString(checkResult.dtCustomer.Rows[0]["mobilePhoneNumber"]);
            if (String.IsNullOrWhiteSpace(phone))
            {
                response.result = VAResult.VA_FAILED_CUSTOMER_NOT_FOUND;
                return response;
            }

            var responseTuple = new CouponManager().GetHistoryRebates(phone, request.pageIndex, request.pageSize);
            var list = responseTuple.Item2.Select(p => new ClientDeductionVolumeDetail()
            {
                shopId = p.ShopId,
                shopName = p.ShopName,
                shopLogoUrl = (p.Image.ToLower().EndsWith(".jpg") || p.Image.ToLower().EndsWith(".jpeg") || p.Image.ToLower().EndsWith(".png")) ?
                 WebConfig.CdnDomain + WebConfig.ImagePath + p.Image : "",
                couponId = p.CouponId,
                couponDeductibleAmount = p.DeductibleAmount,
                couponRequirementMoney = p.RequirementMoney,
                couponValidityEnd = (p.ValidityEnd - new DateTime(1970, 1, 1).ToLocalTime()).TotalSeconds,
                couponSatatus = GetCouponSatatus(p.ShopId, p.State, p.ValidityEnd),
                CheckTime = p.CheckTime,
                couponGetDetailId = p.CouponGetDetailID,
                cityId = p.CityId,
                couponTime = p.UseTime == null ? 0 : ((DateTime)p.UseTime - new DateTime(1970, 1, 1).ToLocalTime()).TotalSeconds,
                usePrompt = string.Format(new SystemConfigCacheLogic().GetSystemConfig("CouponPrompt", "每满{0}可用|最多减{1}"), p.RequirementMoney, p.MaxAmount == 0 ? p.DeductibleAmount : p.MaxAmount)
            }).ToList();

            UpdateCouponCheckTime(phone);
            response.couponPacketListDetails = list;
            response.isHaveMore = responseTuple.Item1 - request.pageIndex * request.pageSize > 0;
            response.result = VAResult.VA_OK;
            return response;
        }

        /// <summary>
        /// 返回数据状态
        /// </summary>
        /// <param name="shopId"></param>
        /// <param name="couponSatatus"></param>
        /// <param name="ValidityEnd"></param>
        /// <returns></returns>
        public int GetCouponSatatus(int shopId, int couponSatatus, DateTime validityEnd)
        {
            if (couponSatatus == 3)
                couponSatatus = 1;
            if (couponSatatus == 1 && (DateTime.Now - validityEnd).TotalSeconds > 0)
                couponSatatus = -1;

            if (couponSatatus == 1 && new ShopManager().IsOffline(shopId))
                couponSatatus = -2;

            return couponSatatus;
        }

        /// <summary>
        /// 获取当前点单可分享的优惠列表
        /// </summary>
        /// <param name="preOrder19DianId"></param>
        /// <returns></returns>
        //public static List<CouponV> GetShareCouponList(long preOrder19DianId)
        //{
        //    PreOrder19dianOperate preOrder19dianOperate = new PreOrder19dianOperate();
        //    var preOrder19dian = preOrder19dianOperate.GetPreOrder19dianById(preOrder19DianId);
        //    CouponVQueryObject couponQueryObject = new CouponVQueryObject()
        //    {
        //        ShopId = preOrder19dian.shopId
        //    };
        //    var couponList = new List<CouponV>();
        //    var couponFirst = CouponVOperate.GetListByQuery(couponQueryObject, CouponVOrderColumn.LastUpdatedTime).FirstOrDefault();
        //    if (couponFirst != null)
        //    {
        //        couponList.Add(couponFirst);
        //    }
        //    int remainCount = 8 - couponList.Count;
        //    if (remainCount > 0)
        //    {
        //        var tempCouponList = CouponVOperate.GetListByQuery(remainCount, 1, null, CouponVOrderColumn.LastUpdatedTime);
        //        if (tempCouponList != null && couponFirst != null)
        //        {
        //            //第一条抵扣券是否在查询结果中
        //            var currentShopCoupon = tempCouponList.FirstOrDefault(p => p.CouponId == couponFirst.CouponId);
        //            if (currentShopCoupon != null)
        //            {
        //                tempCouponList.Remove(currentShopCoupon);
        //            }
        //            couponList.AddRange(tempCouponList);
        //        }
        //    }
        //    if (couponList.Count < 8)
        //    {
        //        while (true)
        //        {
        //            var remainCouponList = CouponVOperate.GetListByQuery(1, couponList.Count, null, CouponVOrderColumn.LastUpdatedTime);
        //            if (remainCouponList == null)
        //            {
        //                break;
        //            }
        //            else
        //            {
        //                couponList.AddRange(remainCouponList);
        //                if (couponList.Count >= 8)
        //                {
        //                    break;
        //                }
        //            }
        //        }
        //    }
        //    return couponList;
        //}

        /// <summary>
        /// 更新用户抵扣券的查看时间
        /// </summary>
        /// <param name="strCouponGetDetailId"></param>
        /// <returns></returns>
        public bool UpdateCouponCheckTime(string mobilePhoneNumber)
        {
            return _couponManager.UpdateCouponCheckTime(mobilePhoneNumber);
        }

        public static List<ICoupon> GetListByDistanceOrder(int pageSize, int pageIndex, CouponQueryObject queryObject)
        {
            if (queryObject.Longitude == null | queryObject.Latitude == null)
            {
                return null;
            }
            return _CouponManager.GetListByDistanceOrder(pageSize, pageIndex, queryObject);
        }

        /// <summary>
        /// 通过关键字查询有券的店 <remarks>查询对象中的CityID为必填</remarks>
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public static List<ShopInfo> SearchShopWithCouponByKeyWord(int pageSize, int pageIndex, string keyWord, CouponQueryObject queryObject)
        {
            if (queryObject.CityId.HasValue == false)
            {
                return null;
            }
            return _CouponManager.SearchShopWithCouponByKeyWord(pageSize, pageIndex, keyWord, queryObject);
        }

        /// <summary>
        /// 获取某家店指定的抵扣券
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public Coupon GetCouponListOfShop(int shopId)
        {
            CouponManager couponManager = new CouponManager();
            return couponManager.GetCouponListOfShop(shopId);
        }

        /// <summary>
        /// 查询指定订单对应的抵扣金额
        /// </summary>
        /// <param name="preOrder19dianIds"></param>
        /// <returns></returns>
        public List<OrderCoupon> SelectOrderDeductibleAmount(List<long> preOrder19dianIds)
        {
            return _couponManager.SelectOrderDeductibleAmount(preOrder19dianIds);
        }

        /// <summary>
        /// 获取某家店指定的抵扣券
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public List<CouponList> GetCouponListByShopName(String ShopName)
        {
            CouponManager couponManager = new CouponManager();
            return couponManager.GetCouponListByShopName(ShopName);
        }
    }
}
