using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using Autofac.Integration.Web;
using VAGastronomistMobileApp.SQLServerDAL.Persistence;
using VAGastronomistMobileApp.WebPageDll.Services.Infrastructure;

namespace ShopVIPFlaseSpeed
{
    class Program
    {
        static void Main(string[] args)
        {
            //CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            //using (var scope = Bootstrapper.Container.BeginLifetimeScope(WebLifetime.Request))
            //{
            //    IShopVIPSpeedConfigRepository repository = scope.Resolve<IShopVIPSpeedConfigRepository>();
            //    int[] cityids = repository.GetCityForShopVipSpeedConfig();
            //    foreach (var cityid in cityids)
            //    {
            //        //taskFactory.StartNew(new CitySpeed(cityid).Run);
            //        Task.Factory.StartNew(new CitySpeed(cityid, cancellationTokenSource.Token).Run, cancellationTokenSource.Token,
            //            TaskCreationOptions.LongRunning, TaskScheduler.Default);
            //    }
            //}
            //Console.WriteLine("Press any key to cancel");
            //Console.ReadLine();
            //Thread.Sleep(10);
            //cancellationTokenSource.Cancel();
            //Console.WriteLine("Done");
            //Console.ReadLine();
            //cancellationToken.

            int count = 3;
            int amount = 1000;
            int min = 200;
            int max = 500;
            int j = random.Next(count/3, count / 2);
            for (int i = 0; i < count; i++)
            {
                int max1 = 0;
                
                if (i % j != 0)
                {
                    max1 = max - (count - i) * 100;
                    if ((count - i) * max1 < amount)
                    {
                        max1 = max;
                    }
                }
                else
                {
                    max1 = max;
                }

                int a = GetAmount(min, max1, amount, count - i);
                amount -= a;

                Console.WriteLine("{0}={1}", i + 1, a);
            }


            Console.WriteLine("{0}", amount);

            Console.Read();

        }
        static Random random = new Random();
        private static int GetAmount(int min, int max, int amount, int count)
        {
            //Random random = new Random();
            //int money = random.Next(min, max);//本次随机抽到的钱，单位为分
            //int remain = amount - money;//本次抽取后剩余的钱

            //if (remain < max || remain > max * (count - 1))
            //{
            //    //剩余的钱比最大值小，则重新定义本次最大值，要保证剩余的人至少能拿到最小值
            //    if (remain < max)
            //    {
            //        max = remain - (count - 1) * min;
            //    }
            //    //剩余的钱，比剩余的人全拿最大值都大，则重新定义本次最小值，要保证剩余的人拿的钱不超过最大值
            //    if (remain > max * (count - 1))
            //    {
            //        min = remain - (count - 1) * max;
            //    }
            //    //money = random.Next(min, max);//重新计算本次抽取的钱
            //    money = GetAmount(min, max, amount, count);
            //}
            //return money;
            int money = 0;
            int remain = 0;
            do
            {
                money = random.Next(min, max);
                remain = amount - money;
                if (remain < min * (count - 1) || remain > max * (count - 1))
                {
                    if (remain < min * (count - 1))
                    {
                        max = amount - min * (count - 1);
                    }
                    if (remain > max * (count - 1))
                    {
                        min = amount - max * (count - 1);
                    }
                }
                else
                {
                    break;
                }
            } while (true);

            return money;
        }

    }
}
