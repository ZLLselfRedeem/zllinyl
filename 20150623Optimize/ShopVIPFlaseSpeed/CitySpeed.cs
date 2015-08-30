using System;
using System.Threading;
using Autofac;
using Autofac.Integration.Web;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL.Persistence;
using VAGastronomistMobileApp.WebPageDll.Services.Infrastructure;

namespace ShopVIPFlaseSpeed
{
    internal class CitySpeed
    {
        private readonly int _cityId;
        private CancellationToken _cancellationToken;
        private DateTime _nextTime;
        public CitySpeed(int cityId, CancellationToken cancellationToken)
        {
            this._cityId = cityId;
            this._cancellationToken = cancellationToken;


            cancellationToken.Register(CancelCallBack);
            using (var scope = Bootstrapper.Container.BeginLifetimeScope(WebLifetime.Request))
            {
                IShopVIPSpeedNextTimeRepository shopVipSpeedNextTimeRepository =
                    scope.Resolve<IShopVIPSpeedNextTimeRepository>();

                ShopVIPSpeedNextTime shopVipSpeedNextTime = shopVipSpeedNextTimeRepository.GetByCity(_cityId);
                if (shopVipSpeedNextTime != null && shopVipSpeedNextTime.NextTime.HasValue &&
                    shopVipSpeedNextTime.NextTime.Value > DateTime.Now)
                {
                    _nextTime = shopVipSpeedNextTime.NextTime.Value;
                }
                else
                {
                    _nextTime = DateTime.Now;
                }
            }
        }

        private void CancelCallBack()
        {
            Console.WriteLine("{0}ȡ����,�¸�����ʱ��Ϊ{1:g}", _cityId, _nextTime);
            using (var scope = Bootstrapper.Container.BeginLifetimeScope(WebLifetime.Request))
            {
                IShopVIPSpeedNextTimeRepository shopVipSpeedNextTimeRepository =
                    scope.Resolve<IShopVIPSpeedNextTimeRepository>();

                ShopVIPSpeedNextTime shopVipSpeedNextTime = shopVipSpeedNextTimeRepository.GetByCity(_cityId);
                if (shopVipSpeedNextTime == null)
                {
                    shopVipSpeedNextTime = new ShopVIPSpeedNextTime()
                    {
                        City = _cityId,
                        NextTime = _nextTime
                    };

                    shopVipSpeedNextTimeRepository.Add(shopVipSpeedNextTime);
                }
                else
                {
                    shopVipSpeedNextTime.NextTime = _nextTime;
                    shopVipSpeedNextTimeRepository.Update(shopVipSpeedNextTime);
                }
            }
        }




        public void Run()
        {
            while (!_cancellationToken.IsCancellationRequested)
            {
                DateTime now = DateTime.Now;
                TimeSpan timeSpanSleep = TimeSpan.Zero;
                if (_nextTime > now)
                {
                    timeSpanSleep = (_nextTime - now);
                }
                else
                {


                    using (var scope = Bootstrapper.Container.BeginLifetimeScope(WebLifetime.Request))
                    {
                        IShopVIPSpeedConfigRepository shopVipSpeedConfigRepository =
                            scope.Resolve<IShopVIPSpeedConfigRepository>();
                        ShopVIPSpeedConfig shopVipSpeedConfig =
                            shopVipSpeedConfigRepository.GetShopVipSpeedConfigByCityAndHour(_cityId, now.Hour);
                        if (shopVipSpeedConfig != null)
                        {
                            int minute = 0;
                            if (shopVipSpeedConfig.Unit == ShopVIPSpeedConfigUnit.Сʱ)
                            {
                                minute = shopVipSpeedConfig.PreUnit * 60;
                            }
                            else
                            {
                                minute = shopVipSpeedConfig.PreUnit;
                            }

                            Random random = new Random();
                            int speed = random.Next(shopVipSpeedConfig.MinSpeed, shopVipSpeedConfig.MaxSpeed);
                            //������ʱ����������������
                            double preMinute = (double)minute / speed; //ÿ����һ����Ҫ�೤ʱ��(����)

                            DateTime nextTime = now.Date;
                            if (shopVipSpeedConfig.StartHour < shopVipSpeedConfig.EndHour)
                            {
                                nextTime = nextTime.AddHours(shopVipSpeedConfig.EndHour);
                            }
                            else
                            {
                                nextTime = nextTime.AddDays(1).AddHours(shopVipSpeedConfig.EndHour);
                            }
                            if (now.AddMinutes(preMinute) >= nextTime)
                            {
                                //��һʱ�ε�����
                                ShopVIPSpeedConfig nextTimeShopVipSpeedConfig =
                                    shopVipSpeedConfigRepository.GetShopVipSpeedConfigByCityAndHour(_cityId,
                                        nextTime.Hour);
                                if (nextTimeShopVipSpeedConfig != null)
                                {
                                    speed =
                                        random.Next(shopVipSpeedConfig.MinSpeed + nextTimeShopVipSpeedConfig.MinSpeed,
                                            shopVipSpeedConfig.MaxSpeed + nextTimeShopVipSpeedConfig.MaxSpeed);
                                    if (nextTimeShopVipSpeedConfig.Unit == ShopVIPSpeedConfigUnit.Сʱ)
                                    {
                                        minute += nextTimeShopVipSpeedConfig.PreUnit * 60;
                                    }
                                    else
                                    {
                                        minute += nextTimeShopVipSpeedConfig.PreUnit;
                                    }
                                    preMinute = (double)minute / speed;
                                }
                            }
                            int count = 1;
                            if (preMinute < 5)
                            {
                                //5/preMinute;
                                count = (int)Math.Ceiling(5 / preMinute);
                                preMinute *= count;
                            }
                            //����count
                            Console.WriteLine("����{0}", count);

                            IShopVIPFlaseCountRepository shopVipFlaseCountRepository =
                                scope.Resolve<IShopVIPFlaseCountRepository>();
                            var shopVIPFlaseCount = shopVipFlaseCountRepository.GetByCityAndMonth(_cityId,
                                now.ToString("yyyyMM"));
                            if (shopVIPFlaseCount == null)
                            {
                                shopVIPFlaseCount = new ShopVIPFlaseCount
                                {
                                    City = _cityId,
                                    Count = count,
                                    Date = now.ToString("yyyyMM"),
                                    Enable = true
                                };
                                shopVipFlaseCountRepository.Add(shopVIPFlaseCount);
                            }
                            else
                            {
                                shopVIPFlaseCount.Count += count;
                                shopVIPFlaseCount.Enable = true;
                                shopVipFlaseCountRepository.Update(shopVIPFlaseCount);
                            }
                            timeSpanSleep = TimeSpan.FromMinutes(preMinute);

                        }
                        else
                        {
                            DateTime nextHour = now.AddHours(1).AddMinutes(-now.Minute); //��һ������
                            timeSpanSleep = nextHour - now;
                        }
                    }
                }

                if (timeSpanSleep > TimeSpan.Zero)
                {
                    Console.WriteLine("{1}��һ��������{0:g}", timeSpanSleep, _cityId);
                    _nextTime = now.Add(timeSpanSleep);
                    int ti = (int)Math.Ceiling(timeSpanSleep.TotalSeconds / 60);
                    while (!_cancellationToken.IsCancellationRequested && ti > 0)
                    {
                        Thread.Sleep(TimeSpan.FromMinutes(1));
                        ti--;
                    }
                    //Thread.Sleep(timeSpanSleep);
                }
            }

        }
    }
}