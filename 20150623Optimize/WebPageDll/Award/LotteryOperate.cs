using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.SQLServerDAL;
using VAGastronomistMobileApp.Model;
using VA.CacheLogic.OrderClient;
using System.Transactions;
using System.Data;
using LogDll;
using System.Threading;
using VAGastronomistMobileApp.SQLServerDAL.Persistence;
using VAGastronomistMobileApp.WebPageDll.Services.Infrastructure;

namespace VAGastronomistMobileApp.WebPageDll
{
    public class LotteryOperate
    {
        static Random randomDefaultAwardType = new Random();
        static Random randomAwardType = new Random();
        static Random randomThirdType = new Random();
        static Random randomDish = new Random();

        /// <summary>
        /// 获取中奖类型
        /// </summary>
        /// <returns></returns>
        public AwardType GetLotteryType()
        {
            AwardCacheLogic awardCacheLogic = new AwardCacheLogic();
            string strLotteryRateRange = awardCacheLogic.GetAwardConfig("LotteryRateRange", "");
            string strLotteryType = awardCacheLogic.GetAwardConfig("LotteryType", "");

            if (!string.IsNullOrEmpty(strLotteryRateRange) && !string.IsNullOrEmpty(strLotteryType))
            {
                string[] arrRateRange = strLotteryRateRange.Split(',');
                string[] arrType = strLotteryType.Split(',');

                int lotteryType = 0;
                int randomValue = randomDefaultAwardType.Next(0, 101);
                for (int i = 0; i < arrRateRange.Length; i++)
                {
                    if (randomValue >= Common.ToInt32(arrRateRange[i]) && randomValue <= Common.ToInt32(arrRateRange[i + 1]))
                    {
                        lotteryType = Common.ToInt32(arrType[i]);
                        break;
                    }
                }
                return (AwardType)lotteryType;
            }
            else
            {
                return AwardType.NotWin;
            }
        }

        public AwardType GetLotteryTypeNew(int cityId, int shopId)
        {
            ShopAwardOperate shopAwardOperate = new ShopAwardOperate();
            ViewAllocAwardOperate vaAwardOperate = new ViewAllocAwardOperate();
            AwardConnPreOrderOperate connOperate = new AwardConnPreOrderOperate();

            //1.先抓出数据库最全配置（每种奖品类型对应的概率）

            AwardCacheLogic awardCacheLogic = new AwardCacheLogic();
            string strLotteryRateRange = awardCacheLogic.GetAwardConfig("LotteryRateRange", "");
            string strLotteryType = awardCacheLogic.GetAwardConfig("LotteryType", "");

            if (!string.IsNullOrEmpty(strLotteryRateRange) && !string.IsNullOrEmpty(strLotteryType))
            {
                string[] arrType = strLotteryType.Split(',');
                string[] arrRateRange = strLotteryRateRange.Split(',');

                //删除系统配置中无中奖概率的奖品类别
                List<string> tempType = new List<string>(arrType);
                List<string> tempRateRange = new List<string>(arrRateRange);

                List<string> newType = new List<string>();
                List<string> newRateRange = new List<string>();

                newRateRange.Add(tempRateRange[0]);
                for (int i = 1; i < tempRateRange.Count; i++)
                {
                    if (Common.ToInt32(tempRateRange[i]) > 0 && Common.ToInt32(tempRateRange[i]) <= 100)
                    {
                        newType.Add(tempType[i - 1]);
                        newRateRange.Add(tempRateRange[i]);
                    }
                }

                //重新赋值
                arrRateRange = newRateRange.ToArray();
                arrType = newType.ToArray();

                //拿到每个奖品类别对应的概率
                List<AwardRate> awardRates = new List<AwardRate>();
                int tempRate = 0;
                for (int i = 0; i < arrType.Length - 1; i++)
                {
                    if (Common.ToInt32(arrRateRange[i + 1]) == Common.ToInt32(arrRateRange[i]))
                    {
                        continue;
                    }
                    else
                    {
                        AwardRate awardRate = new AwardRate()
                        {
                            awardType = (AwardType)Common.ToInt32(arrType[i]),
                            rate = Common.ToInt32(arrRateRange[i + 1]) - Common.ToInt32(arrRateRange[i])
                        };
                        awardRates.Add(awardRate);

                        tempRate += awardRate.rate;
                    }
                }
                AwardRate awardRateLast = new AwardRate()
                {
                    awardType = (AwardType)Common.ToInt32(arrType[arrType.Length - 1]),
                    rate = 100 - tempRate
                };
                awardRates.Add(awardRateLast);

                #region
                //2.检查门店奖品，将没有设置的奖品移除
                //免排队
                //bool IsHaveAvoidQueueAward = shopAwardOperate.SelectShopAwardType(shopId, AwardType.AvoidQueue);
                //if (!IsHaveAvoidQueueAward)
                //{
                //    for (int i = 0; i < awardRates.Count; i++)
                //    {
                //        if (awardRates[i].awardType == AwardType.AvoidQueue)
                //        {
                //            awardRates.Remove(awardRates[i]);
                //            i--;
                //            break;
                //        }
                //    }
                //}

                //赠菜
                //bool IsHaveDishAward = shopAwardOperate.SelectShopAwardType(shopId, AwardType.PresentDish);
                //if (!IsHaveDishAward)
                //{
                //    for (int i = 0; i < awardRates.Count; i++)
                //    {
                //        if (awardRates[i].awardType == AwardType.PresentDish)
                //        {
                //            awardRates.Remove(awardRates[i]);
                //            i--;
                //            break;
                //        }
                //    }
                //}
                #endregion

                //免排队功能未开放时，剔除此奖品
                bool AvoidQueue = IsAvoidQueueSwitchOpen(cityId, shopId);
                if (!AvoidQueue)
                {
                    for (int i = 0; i < awardRates.Count; i++)
                    {
                        if (awardRates[i].awardType == AwardType.AvoidQueue)
                        {
                            awardRates.Remove(awardRates[i]);
                            i--;
                            break;
                        }
                    }
                }

                //3.检查门店和平台奖品，将没有设置或者已经发放完的奖品移除

                #region 红包

                List<ViewAllocAward> vaAward = vaAwardOperate.SelectVAAwardList(AwardType.PresentRedEnvelope);//平台的红包奖项

                if (vaAward != null && vaAward.Any())
                {
                    //查询已发放数量及金额
                    RedEnvelopeOperate redEnvelopeOperate = new RedEnvelopeOperate();
                    RedEnvelopeConsume redEnvelopeConsume = redEnvelopeOperate.SelectRedEnvelopeConsume(vaAward[0].ActivityId);

                    //查验该门店红包发放金额及数量是否达到上限
                    //先看门店是否有标准，没有的话参考平台标准

                    ShopConnRedEnvelopeOperate shopConnRedEnvelopeOperate = new ShopConnRedEnvelopeOperate();
                    ShopConnRedEnvelope shopConnRedEnvelope = shopConnRedEnvelopeOperate.SelectShopConnRedEnvelope(shopId);

                    if (shopConnRedEnvelope != null && shopConnRedEnvelope.Id > 0)//门店有自己限额标准
                    {
                        //数量及金额未超标
                        if (redEnvelopeConsume.consumeCount >= shopConnRedEnvelope.RedEnvelopeConsumeCount
                         || redEnvelopeConsume.consumeAmount >= shopConnRedEnvelope.RedEnvelopeConsumeAmount)
                        {
                            for (int i = 0; i < awardRates.Count; i++)
                            {
                                if (awardRates[i].awardType == AwardType.PresentRedEnvelope)
                                {
                                    awardRates.Remove(awardRates[i]);
                                    i--;
                                    break;
                                }
                            }
                        }
                    }
                    else//取平台限额
                    {
                        int consumeCount = Common.ToInt32(awardCacheLogic.GetAwardConfig("RedEnvelopeConsumeCountOnPlatform", ""));
                        double consumeAmount = Common.ToDouble(awardCacheLogic.GetAwardConfig("RedEnvelopeConsumeAmountOnPlatform", ""));

                        //数量及金额未超标
                        if (redEnvelopeConsume.consumeCount >= consumeCount
                         && redEnvelopeConsume.consumeAmount >= consumeAmount)
                        {
                            for (int i = 0; i < awardRates.Count; i++)
                            {
                                if (awardRates[i].awardType == AwardType.PresentRedEnvelope)
                                {
                                    awardRates.Remove(awardRates[i]);
                                    i--;
                                    break;
                                }
                            }
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < awardRates.Count; i++)
                    {
                        if (awardRates[i].awardType == AwardType.PresentRedEnvelope)
                        {
                            awardRates.Remove(awardRates[i]);
                            i--;
                            break;
                        }
                    }
                }
                #endregion

                #region 第三方

                vaAward = vaAwardOperate.SelectVAAwardList(AwardType.PresentThirdParty);//平台的第三方奖项

                if (vaAward != null && vaAward.Any())
                {
                    DataTable dtThirdAwardConsume = connOperate.SelectThirdAwardConsume();//第三方奖品领取情况

                    int invalidAwardCnt = 0;
                    if (dtThirdAwardConsume != null && dtThirdAwardConsume.Rows.Count >= 0)
                    {
                        for (int i = 0; i < dtThirdAwardConsume.Rows.Count; i++)
                        {
                            for (int j = 0; j < vaAward.Count; j++)
                            {
                                //如果指定奖品已经发放完
                                if (dtThirdAwardConsume.Rows[i]["AwardId"].ToString() == vaAward[j].Id.ToString()
                                && Common.ToInt32(dtThirdAwardConsume.Rows[i]["cnt"]) >= Common.ToInt32(vaAward[j].Count))
                                {
                                    invalidAwardCnt++;
                                    break;
                                }
                            }
                        }
                    }
                    if (vaAward.Count == invalidAwardCnt)
                    {
                        for (int i = 0; i < awardRates.Count; i++)
                        {
                            if (awardRates[i].awardType == AwardType.PresentThirdParty)
                            {
                                awardRates.Remove(awardRates[i]);
                                i--;
                                break;
                            }
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < awardRates.Count; i++)
                    {
                        if (awardRates[i].awardType == AwardType.PresentThirdParty)
                        {
                            awardRates.Remove(awardRates[i]);
                            i--;
                            break;
                        }
                    }
                }
                #endregion

                #region 免排队

                List<ShopAward> shopAwards = shopAwardOperate.SelectShopAwardList(shopId, AwardType.AvoidQueue);

                if (shopAwards != null && shopAwards.Any() && shopAwards.Count == 1)
                {
                    //检查是否还有名额

                    int alreadySend = connOperate.SelectAwardCount(shopAwards[0].Id);

                    if (alreadySend >= shopAwards[0].Count)//没有名额
                    {
                        for (int i = 0; i < awardRates.Count; i++)
                        {
                            if (awardRates[i].awardType == AwardType.AvoidQueue)
                            {
                                awardRates.Remove(awardRates[i]);
                                i--;
                                break;
                            }
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < awardRates.Count; i++)
                    {
                        if (awardRates[i].awardType == AwardType.AvoidQueue)
                        {
                            awardRates.Remove(awardRates[i]);
                            i--;
                            break;
                        }
                    }
                }
                #endregion

                #region 赠菜

                List<ShopAward> dishAwards = shopAwardOperate.SelectShopAwardList(shopId, AwardType.PresentDish);

                int invalidDishCount = 0;
                if (dishAwards != null && dishAwards.Any())
                {
                    Guid[] strAwardIds = new Guid[dishAwards.Count];
                    //拿到所有奖品ID
                    for (int i = 0; i < dishAwards.Count; i++)
                    {
                        strAwardIds[i] = dishAwards[i].Id;
                    }
                    //检查所有菜品的赠送情况
                    DataTable dtDishAwardConsume = connOperate.SelectDishAwardConsume(strAwardIds);

                    if (dtDishAwardConsume != null && dtDishAwardConsume.Rows.Count >= 0)
                    {
                        //删除掉已经发完的菜
                        for (int i = 0; i < dtDishAwardConsume.Rows.Count; i++)
                        {
                            for (int j = 0; j < dishAwards.Count; j++)
                            {
                                //如果指定菜品已经发完，则剔除掉
                                if (dtDishAwardConsume.Rows[i]["AwardId"].ToString() == dishAwards[j].Id.ToString()
                                && Common.ToInt32(dtDishAwardConsume.Rows[i]["cnt"]) == Common.ToInt32(dishAwards[j].Count))
                                {
                                    invalidDishCount++;
                                    break;
                                }
                            }
                        }
                    }

                    if (dishAwards.Count == invalidDishCount)
                    {
                        for (int i = 0; i < awardRates.Count; i++)
                        {
                            if (awardRates[i].awardType == AwardType.PresentDish)
                            {
                                awardRates.Remove(awardRates[i]);
                                i--;
                                break;
                            }
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < awardRates.Count; i++)
                    {
                        if (awardRates[i].awardType == AwardType.PresentDish)
                        {
                            awardRates.Remove(awardRates[i]);
                            i--;
                            break;
                        }
                    }
                }
                #endregion

                //4.重新计算奖品对应的类型（求和，求占比）

                double newSum = 0;
                foreach (AwardRate item in awardRates)
                {
                    newSum += item.rate;
                }

                if (newSum > 0)
                {

                    //5.四舍五入取整,重新计算0~100直接各类型分配

                    int rateSum = 0;
                    List<AwardRate> newAwardRates = new List<AwardRate>();
                    for (int i = 0; i < awardRates.Count - 1; i++)
                    {
                        AwardRate newRate = new AwardRate();
                        newRate.awardType = awardRates[i].awardType;
                        newRate.rate = Common.ToInt32(awardRates[i].rate / newSum * 100);

                        newAwardRates.Add(newRate);

                        rateSum += newRate.rate;
                    }

                    AwardRate newRateLast = new AwardRate();
                    newRateLast.awardType = awardRates[awardRates.Count - 1].awardType;
                    newRateLast.rate = 100 - rateSum;

                    newAwardRates.Add(newRateLast);

                    //6.确定中奖类别

                    string[] newArrType = new string[newAwardRates.Count];
                    string[] newArrRateRange = new string[newAwardRates.Count + 1];

                    newArrRateRange[0] = "0";

                    for (int i = 0; i < newAwardRates.Count; i++)
                    {
                        newArrType[i] = ((int)newAwardRates[i].awardType).ToString();
                    }
                    for (int i = 0; i < newAwardRates.Count; i++)
                    {
                        newArrRateRange[i + 1] = (Common.ToDouble(newAwardRates[i].rate) + Common.ToDouble(newArrRateRange[i])).ToString();
                    }

                    int lotteryType = 0;
                    int randomValue = randomAwardType.Next(1, 101);
                    for (int i = 0; i < newArrRateRange.Length; i++)
                    {
                        if (randomValue > Common.ToInt32(newArrRateRange[i]) && randomValue <= Common.ToInt32(newArrRateRange[i + 1]))
                        {
                            lotteryType = Common.ToInt32(newArrType[i]);
                            break;
                        }
                    }

                    return (AwardType)lotteryType;
                }
                else
                {
                    return AwardType.NotWin;
                }
            }
            else
            {
                return AwardType.NotLottery;
            }
        }

        public AwardType GetLotteryTypeWithoutDish(int cityId, int shopId)
        {
            ShopAwardOperate shopAwardOperate = new ShopAwardOperate();
            ViewAllocAwardOperate vaAwardOperate = new ViewAllocAwardOperate();
            AwardConnPreOrderOperate connOperate = new AwardConnPreOrderOperate();

            //1.先抓出数据库最全配置（每种奖品类型对应的概率）

            AwardCacheLogic awardCacheLogic = new AwardCacheLogic();
            string strLotteryRateRange = awardCacheLogic.GetAwardConfig("LotteryRateRange", "");
            string strLotteryType = awardCacheLogic.GetAwardConfig("LotteryType", "");

            if (!string.IsNullOrEmpty(strLotteryRateRange) && !string.IsNullOrEmpty(strLotteryType))
            {
                string[] arrType = strLotteryType.Split(',');
                string[] arrRateRange = strLotteryRateRange.Split(',');

                //删除系统配置中无中奖概率的奖品类别
                List<string> tempType = new List<string>(arrType);
                List<string> tempRateRange = new List<string>(arrRateRange);

                List<string> newType = new List<string>();
                List<string> newRateRange = new List<string>();

                newRateRange.Add(tempRateRange[0]);
                for (int i = 1; i < tempRateRange.Count; i++)
                {
                    if (Common.ToInt32(tempRateRange[i]) > 0 && Common.ToInt32(tempRateRange[i]) <= 100)
                    {
                        newType.Add(tempType[i - 1]);
                        newRateRange.Add(tempRateRange[i]);
                    }
                }

                //重新赋值
                arrRateRange = newRateRange.ToArray();
                arrType = newType.ToArray();

                //拿到每个奖品类别对应的概率
                List<AwardRate> awardRates = new List<AwardRate>();
                int tempRate = 0;
                for (int i = 0; i < arrType.Length - 1; i++)
                {
                    if (Common.ToInt32(arrRateRange[i + 1]) == Common.ToInt32(arrRateRange[i]))
                    {
                        continue;
                    }
                    else
                    {
                        AwardRate awardRate = new AwardRate()
                        {
                            awardType = (AwardType)Common.ToInt32(arrType[i]),
                            rate = Common.ToInt32(arrRateRange[i + 1]) - Common.ToInt32(arrRateRange[i])
                        };
                        awardRates.Add(awardRate);

                        tempRate += awardRate.rate;
                    }
                }
                AwardRate awardRateLast = new AwardRate()
                {
                    awardType = (AwardType)Common.ToInt32(arrType[arrType.Length - 1]),
                    rate = 100 - tempRate
                };
                awardRates.Add(awardRateLast);

                //2.检查门店奖品，将没有设置的奖品移除
                //免排队
                //bool IsHaveAvoidQueueAward = shopAwardOperate.SelectShopAwardType(shopId, AwardType.AvoidQueue);
                //if (!IsHaveAvoidQueueAward)
                //{
                //    for (int i = 0; i < awardRates.Count; i++)
                //    {
                //        if (awardRates[i].awardType == AwardType.AvoidQueue)
                //        {
                //            awardRates.Remove(awardRates[i]);
                //            i--;
                //            break;
                //        }
                //    }
                //}

                //免排队功能未开放时，剔除此奖品
                bool AvoidQueue = IsAvoidQueueSwitchOpen(cityId, shopId);
                if (!AvoidQueue)
                {
                    for (int i = 0; i < awardRates.Count; i++)
                    {
                        if (awardRates[i].awardType == AwardType.AvoidQueue)
                        {
                            awardRates.Remove(awardRates[i]);
                            i--;
                            break;
                        }
                    }
                }

                //删除赠菜
                for (int i = 0; i < awardRates.Count; i++)
                {
                    if (awardRates[i].awardType == AwardType.PresentDish)
                    {
                        awardRates.Remove(awardRates[i]);
                        i--;
                        break;
                    }
                }



                //3.检查门店和平台奖品，将已经发放完的奖品移除

                #region 红包

                List<ViewAllocAward> vaAward = vaAwardOperate.SelectVAAwardList(AwardType.PresentRedEnvelope);//平台的红包奖项

                if (vaAward != null && vaAward.Any())
                {
                    //查询已发放数量及金额
                    RedEnvelopeOperate redEnvelopeOperate = new RedEnvelopeOperate();
                    RedEnvelopeConsume redEnvelopeConsume = redEnvelopeOperate.SelectRedEnvelopeConsume(vaAward[0].ActivityId);

                    //查验该门店红包发放金额及数量是否达到上限
                    //先看门店是否有标准，没有的话参考平台标准

                    ShopConnRedEnvelopeOperate shopConnRedEnvelopeOperate = new ShopConnRedEnvelopeOperate();
                    ShopConnRedEnvelope shopConnRedEnvelope = shopConnRedEnvelopeOperate.SelectShopConnRedEnvelope(shopId);

                    if (shopConnRedEnvelope != null && shopConnRedEnvelope.Id > 0)//门店有自己限额标准
                    {
                        //数量及金额未超标
                        if (redEnvelopeConsume.consumeCount >= shopConnRedEnvelope.RedEnvelopeConsumeCount
                         || redEnvelopeConsume.consumeAmount >= shopConnRedEnvelope.RedEnvelopeConsumeAmount)
                        {
                            for (int i = 0; i < awardRates.Count; i++)
                            {
                                if (awardRates[i].awardType == AwardType.PresentRedEnvelope)
                                {
                                    awardRates.Remove(awardRates[i]);
                                    i--;
                                    break;
                                }
                            }
                        }
                    }
                    else//取平台限额
                    {
                        int consumeCount = Common.ToInt32(awardCacheLogic.GetAwardConfig("RedEnvelopeConsumeCountOnPlatform", ""));
                        double consumeAmount = Common.ToDouble(awardCacheLogic.GetAwardConfig("RedEnvelopeConsumeAmountOnPlatform", ""));

                        //数量及金额未超标
                        if (redEnvelopeConsume.consumeCount >= consumeCount
                         && redEnvelopeConsume.consumeAmount >= consumeAmount)
                        {
                            for (int i = 0; i < awardRates.Count; i++)
                            {
                                if (awardRates[i].awardType == AwardType.PresentRedEnvelope)
                                {
                                    awardRates.Remove(awardRates[i]);
                                    i--;
                                    break;
                                }
                            }
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < awardRates.Count; i++)
                    {
                        if (awardRates[i].awardType == AwardType.PresentRedEnvelope)
                        {
                            awardRates.Remove(awardRates[i]);
                            i--;
                            break;
                        }
                    }
                }
                #endregion

                #region 第三方

                vaAward = vaAwardOperate.SelectVAAwardList(AwardType.PresentThirdParty);//平台的第三方奖项

                if (vaAward != null && vaAward.Any())
                {
                    DataTable dtThirdAwardConsume = connOperate.SelectThirdAwardConsume();//第三方奖品领取情况

                    int invalidAwardCnt = 0;
                    if (dtThirdAwardConsume != null && dtThirdAwardConsume.Rows.Count >= 0)
                    {
                        for (int i = 0; i < dtThirdAwardConsume.Rows.Count; i++)
                        {
                            for (int j = 0; j < vaAward.Count; j++)
                            {
                                //如果指定奖品已经发放完
                                if (dtThirdAwardConsume.Rows[i]["AwardId"].ToString() == vaAward[j].Id.ToString()
                                && Common.ToInt32(dtThirdAwardConsume.Rows[i]["cnt"]) >= Common.ToInt32(vaAward[j].Count))
                                {
                                    invalidAwardCnt++;
                                    break;
                                }
                            }
                        }
                    }
                    if (vaAward.Count == invalidAwardCnt)
                    {
                        for (int i = 0; i < awardRates.Count; i++)
                        {
                            if (awardRates[i].awardType == AwardType.PresentThirdParty)
                            {
                                awardRates.Remove(awardRates[i]);
                                i--;
                                break;
                            }
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < awardRates.Count; i++)
                    {
                        if (awardRates[i].awardType == AwardType.PresentThirdParty)
                        {
                            awardRates.Remove(awardRates[i]);
                            i--;
                            break;
                        }
                    }
                }
                #endregion

                #region 免排队

                List<ShopAward> shopAwards = shopAwardOperate.SelectShopAwardList(shopId, AwardType.AvoidQueue);

                if (shopAwards != null && shopAwards.Any() && shopAwards.Count == 1)
                {
                    //检查是否还有名额

                    int alreadySend = connOperate.SelectAwardCount(shopAwards[0].Id);

                    if (alreadySend >= shopAwards[0].Count)//没有名额
                    {
                        for (int i = 0; i < awardRates.Count; i++)
                        {
                            if (awardRates[i].awardType == AwardType.AvoidQueue)
                            {
                                awardRates.Remove(awardRates[i]);
                                i--;
                                break;
                            }
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < awardRates.Count; i++)
                    {
                        if (awardRates[i].awardType == AwardType.AvoidQueue)
                        {
                            awardRates.Remove(awardRates[i]);
                            i--;
                            break;
                        }
                    }
                }
                #endregion

                //4.重新计算奖品对应的类型（求和，求占比）

                double newSum = 0;
                foreach (AwardRate item in awardRates)
                {
                    newSum += item.rate;
                }


                //5.四舍五入取整,重新计算0~100直接各类型分配

                int rateSum = 0;
                List<AwardRate> newAwardRates = new List<AwardRate>();
                for (int i = 0; i < awardRates.Count - 1; i++)
                {
                    AwardRate newRate = new AwardRate();
                    newRate.awardType = awardRates[i].awardType;
                    newRate.rate = Common.ToInt32(awardRates[i].rate / newSum * 100);

                    newAwardRates.Add(newRate);

                    rateSum += newRate.rate;
                }

                AwardRate newRateLast = new AwardRate();
                newRateLast.awardType = awardRates[awardRates.Count - 1].awardType;
                newRateLast.rate = 100 - rateSum;

                newAwardRates.Add(newRateLast);

                //6.确定中奖类别

                string[] newArrType = new string[newAwardRates.Count];
                string[] newArrRateRange = new string[newAwardRates.Count + 1];

                newArrRateRange[0] = "0";

                for (int i = 0; i < newAwardRates.Count; i++)
                {
                    newArrType[i] = ((int)newAwardRates[i].awardType).ToString();
                }
                for (int i = 0; i < newAwardRates.Count; i++)
                {
                    newArrRateRange[i + 1] = (Common.ToDouble(newAwardRates[i].rate) + Common.ToDouble(newArrRateRange[i])).ToString();
                }

                int lotteryType = 0;
                int randomValue = randomAwardType.Next(1, 101);
                for (int i = 0; i < newArrRateRange.Length; i++)
                {
                    if (randomValue > Common.ToInt32(newArrRateRange[i]) && randomValue <= Common.ToInt32(newArrRateRange[i + 1]))
                    {
                        lotteryType = Common.ToInt32(newArrType[i]);
                        break;
                    }
                }
                return (AwardType)lotteryType;
            }
            else
            {
                return AwardType.NotLottery;
            }
        }


        /// <summary>
        /// 客户端抽奖
        /// </summary>
        /// <param name="clientLotteryRequest"></param>
        /// <returns></returns>
        public VAClientLotteryResponse ClientLottery(VAClientLotteryRequest clientLotteryRequest)
        {
            VAClientLotteryResponse clientLotteryResponse = new VAClientLotteryResponse();
            clientLotteryResponse.type = VAMessageType.CLIENT_LOTTERY_RESPONSE;
            clientLotteryResponse.cookie = clientLotteryRequest.cookie;
            clientLotteryResponse.uuid = clientLotteryResponse.uuid;

            CheckCookieAndMsgtypeInfo checkResult = Common.CheckCookieAndMsgtype(clientLotteryRequest.cookie, clientLotteryRequest.uuid, (int)clientLotteryRequest.type, (int)VAMessageType.CLIENT_LOTTERY_REQUEST, false);
            if (checkResult.result == VAResult.VA_OK)
            {
                AwardConnPreOrderOperate operate = new AwardConnPreOrderOperate();
                PreOrder19dianOperate orderOperate = new PreOrder19dianOperate();


                long customerId = Common.ToInt64(checkResult.dtCustomer.Rows[0]["CustomerID"]);

                AwardInfo userAward = new AwardInfo()
                {
                    awardDesc = "",
                    awardPushMessage = "",
                    awardShowUrl = "",
                    awardType = AwardType.NotWin,
                    orderInJson = ""
                };

                //判断抽奖开关
                bool LotterySwitch = IsLotterySwitchOpen(clientLotteryRequest.cityId, clientLotteryRequest.shopId);
                if (!LotterySwitch)
                {
                    clientLotteryResponse.result = VAResult.VA_FAILED_LOTTERY;
                    return clientLotteryResponse;
                }

                //5.判断用户今日是否还能抽奖

                bool preOrderStatus = CheckPreOrderStatus(clientLotteryRequest.preorderId);

                bool canLottery = CheckCustomerCanLottery(clientLotteryRequest.uuid, clientLotteryRequest.shopId, checkResult.dtCustomer.Rows[0]["mobilePhoneNumber"].ToString(), customerId);

                if (!preOrderStatus || !canLottery)
                {
                    clientLotteryResponse.result = VAResult.VA_FAILED_LOTTERY;
                    return clientLotteryResponse;
                }

                //6.确定抽奖类型

                AwardType awardType = GetLotteryTypeNew(clientLotteryRequest.cityId, clientLotteryRequest.shopId);

                clientLotteryResponse = SendCustomerAward(clientLotteryRequest, checkResult, awardType);
                clientLotteryResponse.type = VAMessageType.CLIENT_LOTTERY_RESPONSE;

                if (!string.IsNullOrEmpty(clientLotteryResponse.userAwardInfo.awardShowUrl))
                {
                    UniPushAfterLottery uniPushInfo = new UniPushAfterLottery();

                    uniPushInfo.customerPhone = checkResult.dtCustomer.Rows[0]["mobilePhoneNumber"].ToString();
                    uniPushInfo.preOrder19dianId = clientLotteryRequest.preorderId;
                    uniPushInfo.pushMessage = clientLotteryResponse.userAwardInfo.awardPushMessage;
                    uniPushInfo.clientBuild = clientLotteryRequest.clientBuild;

                    Thread notificationThread = new Thread(SendEvaluationNotification);
                    notificationThread.Start(uniPushInfo);
                }
            }
            else
            {
                clientLotteryResponse.result = checkResult.result;
            }

            return clientLotteryResponse;
        }

        /// <summary>
        /// 检查指定门店是否开放抽奖功能,True:开放
        /// </summary>
        /// <param name="cityId"></param>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public bool IsLotterySwitchOpen(int cityId, int shopId)
        {
            AwardCacheLogic awardCacheLogic = new AwardCacheLogic();

            //1.判断城市抽奖开关是否打开
            bool citylotterySwitch = Common.ToBool(awardCacheLogic.GetAwardConfig(cityId + "Lottery", ""));
            if (!citylotterySwitch)//
            {
                //2.判断是否在抽奖白名单
                string[] shops = awardCacheLogic.GetAwardConfig("LotteryWhiteShops", "").Split(',');

                var data = shops.Where(s => Common.ToInt32(s) == shopId).ToList();
                if (data.Any())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 检查指定门店是否开放免排队功能,True:开放
        /// </summary>
        /// <param name="cityId"></param>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public bool IsAvoidQueueSwitchOpen(int cityId, int shopId)
        {
            AwardCacheLogic awardCacheLogic = new AwardCacheLogic();

            //1.判断城市免排队开关是否打开
            bool citylotterySwitch = Common.ToBool(awardCacheLogic.GetAwardConfig(cityId + "AvoidQueue", ""));
            if (!citylotterySwitch)//
            {
                //2.判断是否在免排队白名单
                string[] shops = awardCacheLogic.GetAwardConfig("AvoidQueueWhiteShops", "").Split(',');

                var data = shops.Where(s => Common.ToInt32(s) == shopId).ToList();
                if (data.Any())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return true;
            }
        }

        private bool CheckPreOrderStatus(long preorderId)
        {
            AwardConnPreOrderOperate operate = new AwardConnPreOrderOperate();
            PreOrder19dianOperate orderOperate = new PreOrder19dianOperate();

            bool hadLottery = false;

            //已经抽过奖的点单不能继续抽奖
            AwardConnPreOrder awardConnPreOrder = operate.SelectAwardConnPreOrderByOrderId(preorderId);
            if (awardConnPreOrder != null && awardConnPreOrder.CustomerId > 0)
            {
                hadLottery = true;
            }
            else
            {
                //hadLottery = false;//正式
                hadLottery = true;//测试
            }
            //只有已支付未入座且支付时间是当日才能抽奖

            bool preOrderIsToday = orderOperate.PreOrderIsToday(preorderId);
            if (hadLottery && !preOrderIsToday)//点单已经抽过奖，或者状态不对，均不能抽奖
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool CheckCustomerCanLottery(string uuid, int shopId, string phone, long customerId)
        {
            AwardConnPreOrderOperate operate = new AwardConnPreOrderOperate();

            //退款次数过多，不能抽奖
            bool canLotteryByRefundCnt = operate.CheckCusCanLotteryByRefundCnt(uuid, phone);

            //未消费订单个数过多，不能抽奖
            bool canLotteryByUnConfirmedOrderCnt = operate.CheckCusCanLotteryByUnConfirmedOrderCntOfShop(customerId, uuid, shopId);

            if (canLotteryByRefundCnt && canLotteryByUnConfirmedOrderCnt)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private VAClientLotteryResponse SendCustomerAward(VAClientLotteryRequest clientLotteryRequest, CheckCookieAndMsgtypeInfo checkResult, AwardType awardType)
        {
            VAClientLotteryResponse clientLotteryResponse = new VAClientLotteryResponse();
            AwardInfo userAward = new AwardInfo();

            AwardConnPreOrderOperate operate = new AwardConnPreOrderOperate();
            PreOrder19dianOperate orderOperate = new PreOrder19dianOperate();
            ShopAwardOperate shopAwardOperate = new ShopAwardOperate();
            ViewAllocAwardOperate vaAwardOperate = new ViewAllocAwardOperate();

            long customerId = Common.ToInt64(checkResult.dtCustomer.Rows[0]["CustomerID"]);

            userAward.awardType = awardType;
            userAward.awardShowUrl = WebConfig.ServerDomain + "Award/awardMobile/lottery/lottery.html?p=" + clientLotteryRequest.preorderId + "&s=" + clientLotteryRequest.shopId;
            userAward.awardShowUrl = userAward.awardShowUrl + "&ww={0}&wh={1}";

            switch (awardType)
            {
                case AwardType.NotWin:

                    userAward.awardDesc = "";
                    userAward.awardShowUrl = "";

                    break;
                case AwardType.AvoidQueue:
                    #region
                    List<ShopAward> shopAwards = shopAwardOperate.SelectShopAwardList(clientLotteryRequest.shopId, awardType);

                    if (shopAwards != null && shopAwards.Any() && shopAwards.Count == 1)
                    {
                        //检查是否还有名额

                        int alreadySend = operate.SelectAwardCount(shopAwards[0].Id);

                        if (alreadySend < shopAwards[0].Count)//还有名额
                        {
                            AwardConnPreOrder award = new AwardConnPreOrder();
                            award.Id = Guid.NewGuid();
                            award.PreOrder19dianId = clientLotteryRequest.preorderId;
                            award.OrderId = clientLotteryRequest.orderId;
                            award.ShopId = clientLotteryRequest.shopId;
                            award.Type = awardType;
                            award.LotteryTime = DateTime.Now;
                            award.CustomerId = customerId;
                            award.AwardId = shopAwards[0].Id;
                            award.Status = true;
                            award.ValidTime = Common.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59");

                            bool insertAward = operate.InsertAwardConnPreOrder(award);
                            if (insertAward)
                            {
                                clientLotteryResponse.result = VAResult.VA_OK;

                                userAward.awardDesc = "【专享" + DateTime.Now.ToString("MM月dd日") + "免排队特权】";
                                userAward.awardPushMessage = "恭喜你抽中悠先随机奖品【" + DateTime.Now.ToString("MM月dd日") + "外婆家计量大厦店免排队机会】";
                            }
                            else
                            {
                                clientLotteryResponse.result = VAResult.VA_FAILED_DB_ERROR;

                                userAward.awardType = AwardType.NotLottery;


                                LogManager.WriteLog(LogFile.Trace, DateTime.Now.ToString() + "--免排队--DB_ERROR");
                            }
                        }
                        else
                        {
                            clientLotteryResponse.result = VAResult.VA_FAILED_LOTTERY;

                            userAward.awardType = AwardType.NotWin;
                        }
                    }
                    else
                    {
                        clientLotteryResponse.result = VAResult.VA_FAILED_LOTTERY;

                        userAward.awardType = AwardType.NotWin;
                    }
                    #endregion
                    break;
                case AwardType.PresentDish:
                    #region
                    //查询门店的奖品
                    List<ShopAward> dishAwards = shopAwardOperate.SelectShopAwardList(clientLotteryRequest.shopId, awardType);

                    if (dishAwards != null && dishAwards.Any())
                    {
                        Guid[] strAwardIds = new Guid[dishAwards.Count];
                        //拿到所有奖品ID
                        for (int i = 0; i < dishAwards.Count; i++)
                        {
                            strAwardIds[i] = dishAwards[i].Id;
                        }
                        //检查所有菜品的赠送情况
                        DataTable dtDishAwardConsume = operate.SelectDishAwardConsume(strAwardIds);

                        if (dtDishAwardConsume != null && dtDishAwardConsume.Rows.Count >= 0)
                        {
                            List<ShopAward> dishAwardsCopy = dishAwards;
                            //删除掉已经发完的菜
                            for (int i = 0; i < dtDishAwardConsume.Rows.Count; i++)
                            {
                                for (int j = 0; j < dishAwardsCopy.Count; j++)
                                {
                                    //如果指定菜品已经发完，则剔除掉
                                    if (dtDishAwardConsume.Rows[i]["AwardId"].ToString() == dishAwardsCopy[j].Id.ToString()
                                    && dtDishAwardConsume.Rows[i]["cnt"].ToString() == dishAwardsCopy[j].Count.ToString())
                                    {
                                        dishAwards.Remove(dishAwards[j]);//移除已经发完的奖品
                                        break;
                                    }
                                }
                            }

                            //若是多个，随机给一个
                            if (dishAwards != null && dishAwards.Any())
                            {
                                //将赠送菜品合并到OrderInJson
                                //a.查询出orderInJson

                                string oldOrderInJson = orderOperate.GetPreOrder19DianExtendOrderJson(clientLotteryRequest.preorderId);

                                if (!string.IsNullOrEmpty(oldOrderInJson))
                                {
                                    //b.查询赠送菜品信息
                                    //dishAwards

                                    //c.重组OrderInJson

                                    List<PreOrderIn19dian> listOrderInfo = JsonOperate.JsonDeserialize<List<PreOrderIn19dian>>(oldOrderInJson);

                                    //先将赠菜中，用户已有菜品删除
                                    for (int i = 0; i < dishAwardsCopy.Count; i++)
                                    {
                                        for (int j = 0; j < listOrderInfo.Count; j++)
                                        {
                                            if (dishAwardsCopy[i].DishId == listOrderInfo[j].dishId)
                                            {
                                                dishAwards.Remove(dishAwardsCopy[i]);
                                                i--;
                                                break;
                                            }
                                        }
                                    }

                                    if (dishAwards.Count == 0)//若删光了赠菜，则给用户重新挑选除赠菜外的奖品类别
                                    {
                                        AwardType newAwardType = GetLotteryTypeWithoutDish(clientLotteryRequest.cityId, clientLotteryRequest.shopId);

                                        SendCustomerAward(clientLotteryRequest, checkResult, newAwardType);
                                    }
                                    else
                                    {
                                        //若删除后还有赠菜，则随机给用户挑选一个赠菜

                                        int randomValue = randomDish.Next(0, dishAwards.Count);

                                        DishAwardOperate dishAwardOperate = new DishAwardOperate();

                                        PreOrderIn19dian dishAward = dishAwardOperate.SelectDishInfo(dishAwards[randomValue].DishId, dishAwards[randomValue].DishPriceId);
                                        listOrderInfo.Add(dishAward);

                                        //d.更新PreOrder19dian & PreOrder19dianExtend 

                                        string newOrderJson = JsonOperate.JsonSerializer<List<PreOrderIn19dian>>(listOrderInfo);


                                        //记录领奖数据
                                        AwardConnPreOrder award = new AwardConnPreOrder();
                                        award.Id = Guid.NewGuid();
                                        award.PreOrder19dianId = clientLotteryRequest.preorderId;
                                        award.OrderId = clientLotteryRequest.orderId;
                                        award.ShopId = clientLotteryRequest.shopId;
                                        award.Type = awardType;
                                        award.LotteryTime = DateTime.Now;
                                        award.CustomerId = customerId;
                                        award.AwardId = dishAwards[randomValue].Id;
                                        award.Status = true;
                                        award.ValidTime = Common.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59");



                                        List<OrderDishOtherInfo> orderDishOtherList = new List<OrderDishOtherInfo>();

                                        if (!String.IsNullOrWhiteSpace(newOrderJson))
                                        {
                                            IImageInfoRepository repositoryContext = ServiceFactory.Resolve<IImageInfoRepository>();
                                            IMenuInfoRepository menuRepositoryContext = ServiceFactory.Resolve<IMenuInfoRepository>();
                                            var menuInfo = menuRepositoryContext.GetMenuInfoByShopId(clientLotteryRequest.shopId);
                                            var orders = JsonOperate.JsonDeserialize<List<PreOrderIn19dianOrderJson>>(newOrderJson);
                                            var dishIds = orders.Select(p => p.dishId).ToList();
                                            var orderDishPraiseInfos = repositoryContext.GetDishPraiseInfosByDishId(CommonPageOperate.SplicingListStr(dishIds, ""));
                                            //组装菜品图片URL
                                            var imageList = repositoryContext.GetAssignScaleImageInfosByDishId(ImageScale.普通图片, dishIds.ToArray());
                                            foreach (var dishItem in orders)
                                            {
                                                string imagePath = (from b in imageList
                                                                    where b.DishID == dishItem.dishId
                                                                    orderby b.ImageID ascending
                                                                    select WebConfig.CdnDomain + WebConfig.ImagePath + menuInfo.menuImagePath + b.ImageName).FirstOrDefault();
                                                orderDishOtherList.Add(new OrderDishOtherInfo()
                                                {
                                                    dishId = dishItem.dishId,
                                                    orderDishImageUrl = imagePath,
                                                    orderDishIsPraise = dishItem.isHavePraise,
                                                    orderDishPraiseNum = (from q in orderDishPraiseInfos
                                                                          where q.dishId == dishItem.dishId
                                                                          select q.orderDishPraiseNum).FirstOrDefault()
                                                });
                                            }
                                        }

                                        using (TransactionScope ts = new TransactionScope())
                                        {
                                            //更新 PreOrder19dian 的 OrderInJson
                                            bool updatePreOrder = orderOperate.UpdatePreOrderOrderJson(newOrderJson, clientLotteryRequest.preorderId);

                                            //更新 PreOrder19dianExtend 的 OrderInJson
                                            bool updatePreOrderExtend = orderOperate.UpdateOrderJson(newOrderJson, clientLotteryRequest.preorderId);

                                            //记录奖品
                                            bool insertAward = operate.InsertAwardConnPreOrder(award);

                                            if (updatePreOrder && updatePreOrderExtend && insertAward)
                                            {
                                                ts.Complete();

                                                clientLotteryResponse.result = VAResult.VA_OK;

                                                userAward.awardDesc = "【门店赠菜 - " + dishAward.dishName.Replace("【赠菜】", "") + "】";
                                                userAward.awardPushMessage = "恭喜你抽中悠先随机奖品【外婆家计量大厦店赠菜 - " + dishAward.dishName + "】";
                                                userAward.orderInJson = newOrderJson;

                                                userAward.orderDishOtherList = orderDishOtherList;
                                            }
                                            else
                                            {
                                                clientLotteryResponse.result = VAResult.VA_FAILED_DB_ERROR;

                                                userAward.awardType = AwardType.NotLottery;

                                                LogManager.WriteLog(LogFile.Trace, DateTime.Now.ToString() + "--赠菜--DB_ERROR");
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    clientLotteryResponse.result = VAResult.VA_FAILED_LOTTERY;

                                    userAward.awardType = AwardType.NotLottery;

                                    LogManager.WriteLog(LogFile.Trace, DateTime.Now.ToString() + "--赠菜--直接支付暂时不支持抽奖");
                                }
                            }
                            else//没有奖品了
                            {
                                clientLotteryResponse.result = VAResult.VA_FAILED_DB_ERROR;

                                userAward.awardType = AwardType.NotWin;
                            }
                        }
                    }

                    #endregion
                    break;
                case AwardType.PresentRedEnvelope:
                    #region

                    Preorder19DianLineOperate preorder19DianLineOperate = new Preorder19DianLineOperate();

                    List<ViewAllocAward> vaAward = vaAwardOperate.SelectVAAwardList(AwardType.PresentRedEnvelope);//平台的红包奖项

                    if (vaAward != null && vaAward.Any())
                    {

                        //查询已发放数量及金额
                        RedEnvelopeOperate redEnvelopeOperate = new RedEnvelopeOperate();
                        RedEnvelopeConsume redEnvelopeConsume = redEnvelopeOperate.SelectRedEnvelopeConsume(vaAward[0].ActivityId);

                        //查验该门店红包发放金额及数量是否达到上限
                        //先看门店是否有标准，没有的话参考平台标准

                        ShopConnRedEnvelopeOperate shopConnRedEnvelopeOperate = new ShopConnRedEnvelopeOperate();
                        ShopConnRedEnvelope shopConnRedEnvelope = shopConnRedEnvelopeOperate.SelectShopConnRedEnvelope(clientLotteryRequest.shopId);

                        if (shopConnRedEnvelope != null && shopConnRedEnvelope.Id > 0)//门店有自己限额标准
                        {
                            //数量及金额未超标
                            if (redEnvelopeConsume.consumeCount < shopConnRedEnvelope.RedEnvelopeConsumeCount
                             && redEnvelopeConsume.consumeAmount < shopConnRedEnvelope.RedEnvelopeConsumeAmount)
                            {
                                string mobile = checkResult.dtCustomer.Rows[0]["mobilePhoneNumber"].ToString();

                                //记录领奖数据
                                AwardConnPreOrder award = new AwardConnPreOrder();
                                award.Id = Guid.NewGuid();
                                award.PreOrder19dianId = clientLotteryRequest.preorderId;
                                award.OrderId = clientLotteryRequest.orderId;
                                award.ShopId = clientLotteryRequest.shopId;
                                award.Type = awardType;
                                award.LotteryTime = DateTime.Now;
                                award.CustomerId = customerId;
                                award.AwardId = vaAward[0].Id;
                                award.Status = true;
                                award.ValidTime = Common.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59");
                                RedEnvelopeAwardRequest request = new RedEnvelopeAwardRequest()
                                                             {
                                                                 activityId = vaAward[0].ActivityId,
                                                                 cookie = clientLotteryRequest.cookie,
                                                                 mobile = mobile,
                                                                 customerId = customerId,
                                                                 shopId = clientLotteryRequest.shopId,
                                                                 uuid = clientLotteryRequest.uuid,
                                                                 thirdPayAmount = preorder19DianLineOperate.SelectThirdPayAmountOfOrder(clientLotteryRequest.preorderId)
                                                             };

                                RedEnvelopeAwardResponse sendResult = redEnvelopeOperate.SendLotteryRedEnvelope(request);

                                if (sendResult.result)
                                {
                                    using (TransactionScope ts = new TransactionScope())
                                    {
                                        long redEnvelopeId = redEnvelopeOperate.InsertRedEnvelope(sendResult.redEnvelope);
                                        award.redEnvelopeId = redEnvelopeId;

                                        bool insertAward = operate.InsertAwardConnPreOrder(award);

                                        if (redEnvelopeId > 0 && insertAward)
                                        {
                                            ts.Complete();
                                            clientLotteryResponse.result = VAResult.VA_OK;

                                            userAward.awardDesc = "【全场通用红包￥" + sendResult.redEnvelope.Amount.ToString() + "元】";
                                            userAward.awardPushMessage = "恭喜你抽中悠先随机奖品【悠先返现￥" + sendResult.redEnvelope.Amount.ToString() + "元】";
                                        }
                                        else
                                        {
                                            clientLotteryResponse.result = VAResult.VA_FAILED_DB_ERROR;

                                            userAward.awardType = AwardType.NotLottery;

                                            LogManager.WriteLog(LogFile.Trace, DateTime.Now.ToString() + "--送红包--门店限额--DB_ERROR");
                                        }
                                    }
                                }
                                else
                                {
                                    clientLotteryResponse.result = VAResult.VA_FAILED_DB_ERROR;

                                    userAward.awardType = AwardType.NotLottery;

                                    LogManager.WriteLog(LogFile.Trace, DateTime.Now.ToString() + "--送红包--门店限额--DB_ERROR");
                                }
                            }
                            else
                            {
                                clientLotteryResponse.result = VAResult.VA_FAILED_LOTTERY;

                                userAward.awardType = AwardType.NotWin;
                            }
                        }
                        else//没有则取平台限额标准
                        {
                            AwardCacheLogic awardCacheLogic = new AwardCacheLogic();
                            int consumeCount = Common.ToInt32(awardCacheLogic.GetAwardConfig("RedEnvelopeConsumeCountOnPlatform", ""));
                            double consumeAmount = Common.ToDouble(awardCacheLogic.GetAwardConfig("RedEnvelopeConsumeAmountOnPlatform", ""));

                            //数量及金额未超标
                            if (redEnvelopeConsume.consumeCount < consumeCount
                             && redEnvelopeConsume.consumeAmount < consumeAmount)
                            {
                                string mobile = checkResult.dtCustomer.Rows[0]["mobilePhoneNumber"].ToString();

                                //记录领奖数据
                                AwardConnPreOrder award = new AwardConnPreOrder();
                                award.Id = Guid.NewGuid();
                                award.PreOrder19dianId = clientLotteryRequest.preorderId;
                                award.OrderId = clientLotteryRequest.orderId;
                                award.ShopId = clientLotteryRequest.shopId;
                                award.Type = awardType;
                                award.LotteryTime = DateTime.Now;
                                award.CustomerId = customerId;
                                award.AwardId = vaAward[0].Id;
                                award.Status = true;
                                award.ValidTime = Common.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59");

                                RedEnvelopeAwardRequest request = new RedEnvelopeAwardRequest()
                                {
                                    activityId = vaAward[0].ActivityId,
                                    cookie = clientLotteryRequest.cookie,
                                    mobile = mobile,
                                    customerId = customerId,
                                    shopId = clientLotteryRequest.shopId,
                                    uuid = clientLotteryRequest.uuid,
                                    thirdPayAmount = preorder19DianLineOperate.SelectThirdPayAmountOfOrder(clientLotteryRequest.preorderId)
                                };
                                RedEnvelopeAwardResponse sendResult = redEnvelopeOperate.SendLotteryRedEnvelope(request);

                                if (sendResult.result)
                                {
                                    using (TransactionScope ts = new TransactionScope())
                                    {
                                        long redEnvelopeId = redEnvelopeOperate.InsertRedEnvelope(sendResult.redEnvelope);
                                        award.redEnvelopeId = redEnvelopeId;

                                        bool insertAward = operate.InsertAwardConnPreOrder(award);//记录奖品

                                        if (insertAward)
                                        {
                                            ts.Complete();
                                            clientLotteryResponse.result = VAResult.VA_OK;

                                            userAward.awardDesc = "【全场通用红包￥" + sendResult.redEnvelope.Amount.ToString() + "元】";
                                            userAward.awardPushMessage = "恭喜你抽中悠先随机奖品【悠先返现￥" + sendResult.redEnvelope.Amount.ToString() + "元】";
                                        }
                                        else
                                        {
                                            clientLotteryResponse.result = VAResult.VA_FAILED_DB_ERROR;

                                            userAward.awardType = AwardType.NotLottery;

                                            LogManager.WriteLog(LogFile.Trace, DateTime.Now.ToString() + "--送红包--平台限额--DB_ERROR");
                                        }
                                    }
                                }
                                else
                                {
                                    clientLotteryResponse.result = VAResult.VA_FAILED_DB_ERROR;

                                    userAward.awardType = AwardType.NotLottery;

                                    LogManager.WriteLog(LogFile.Trace, DateTime.Now.ToString() + "--送红包--平台限额--DB_ERROR");
                                }
                            }
                            else
                            {
                                clientLotteryResponse.result = VAResult.VA_FAILED_LOTTERY;

                                userAward.awardType = AwardType.NotWin;
                            }
                        }
                    }
                    else
                    {
                        clientLotteryResponse.result = VAResult.VA_FAILED_LOTTERY;

                        userAward.awardType = AwardType.NotWin;
                    }
                    #endregion
                    break;
                case AwardType.PresentThirdParty:
                    #region
                    vaAward = vaAwardOperate.SelectVAAwardList(AwardType.PresentThirdParty);//平台的第三方奖项

                    DataTable dtThirdAwardConsume = operate.SelectThirdAwardConsume();//第三方奖品领取情况

                    if (dtThirdAwardConsume != null && dtThirdAwardConsume.Rows.Count >= 0)
                    {
                        List<ViewAllocAward> vaAwardCopy = vaAward;
                        for (int i = 0; i < dtThirdAwardConsume.Rows.Count; i++)
                        {
                            for (int j = 0; j < vaAwardCopy.Count; j++)
                            {
                                //如果指定奖品已经发放完
                                if (dtThirdAwardConsume.Rows[i]["AwardId"].ToString() == vaAwardCopy[j].Id.ToString()
                                && dtThirdAwardConsume.Rows[i]["cnt"].ToString() == vaAwardCopy[j].Count.ToString())
                                {
                                    vaAward.Remove(vaAward[j]);//移除已经发完的奖品
                                    break;
                                }
                            }
                        }

                        //若是多个，随机给一个
                        if (vaAward != null && vaAward.Any())
                        {
                            int randomValue = randomThirdType.Next(0, vaAward.Count);

                            //记录领奖数据
                            AwardConnPreOrder award = new AwardConnPreOrder();
                            award.Id = Guid.NewGuid();
                            award.PreOrder19dianId = clientLotteryRequest.preorderId;
                            award.OrderId = clientLotteryRequest.orderId;
                            award.ShopId = clientLotteryRequest.shopId;
                            award.Type = awardType;
                            award.LotteryTime = DateTime.Now;
                            award.CustomerId = customerId;
                            award.AwardId = vaAward[randomValue].Id;
                            award.Status = true;
                            award.ValidTime = Common.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59");

                            bool insertAward = operate.InsertAwardConnPreOrder(award);//记录奖品

                            if (insertAward)
                            {
                                clientLotteryResponse.result = VAResult.VA_OK;

                                userAward.awardDesc = "【" + vaAward[0].Name + "】";
                                userAward.awardPushMessage = "恭喜你抽中悠先随机奖品【悠先返现￥" + vaAward[0].Name + "元】";
                            }
                            else
                            {
                                clientLotteryResponse.result = VAResult.VA_FAILED_DB_ERROR;

                                userAward.awardType = AwardType.NotLottery;

                                LogManager.WriteLog(LogFile.Trace, DateTime.Now.ToString() + "--送第三方--DB_ERROR");
                            }
                        }
                        else
                        {
                            clientLotteryResponse.result = VAResult.VA_FAILED_LOTTERY;

                            userAward.awardType = AwardType.NotWin;
                        }
                    }
                    #endregion
                    break;
            }

            clientLotteryResponse.userAwardInfo = userAward;
            return clientLotteryResponse;
        }

        private PreOrderIn19dian GetPreOrderIn19dian()
        {
            return null;
        }

        public static void SendEvaluationNotification(object objUniPushInfo)
        {
            UniPushAfterLottery uniPushInfo = (UniPushAfterLottery)objUniPushInfo;
            CustomPushRecordOperate customPushRecordOperate = new CustomPushRecordOperate();
            customPushRecordOperate.UniPushAfterLottery(uniPushInfo);
        }
    }
}
