using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL.Persistence;
using VAGastronomistMobileApp.WebPageDll.Services.Infrastructure;
using System.Transactions;
using System.Data;

namespace VAGastronomistMobileApp.WebPageDll.Services
{
    /// <summary>
    /// 
    /// </summary>
    public interface IShopVipSpeedConfigService
    {
        /// <summary>
        /// 根据最小最大增长因子，生成该城市相应的各时段增速表
        /// </summary>
        /// <param name="city">城市ID</param>
        /// <param name="minSpeed">最小增长因子</param>
        /// <param name="maxSpeed">最大增长因子</param>
        /// <returns></returns>
        bool CreateShopVipSpeed(int city, int minSpeed, int maxSpeed);

        /// <summary>
        /// 查询指定城市对应的增速因子展示给客户
        /// </summary>
        /// <param name="cityId"></param>
        /// <returns></returns>
        DataTable GetCityVipSpeed(int cityId = 0);

        bool DeleteShopVipSpeed(int cityId);
    }
    /// <summary>
    /// 
    /// </summary>
    public class ShopVipSpeedConfigService : BaseService, IShopVipSpeedConfigService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="repositoryContext"></param>
        public ShopVipSpeedConfigService(IRepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        /// <summary>
        /// 根据最小最大增长因子，生成该城市相应的各时段增速表
        /// </summary>
        /// <param name="city">城市ID</param>
        /// <param name="minSpeed">最小增长因子</param>
        /// <param name="maxSpeed">最大增长因子</param>
        /// <returns></returns>
        public bool CreateShopVipSpeed(int city, int minSpeed, int maxSpeed)
        {
            try
            {
                IShopVIPSpeedConfigRepository shopVIPSpeedConfigRepository = RepositoryContext.GetShopVIPSpeedConfigRepository();

                //先检查此城市是否已经有配置的数据，有的话先删除再创建
                DataTable cityVIPSpeed = shopVIPSpeedConfigRepository.GetCityVipSpeed(city);
                if (cityVIPSpeed != null && cityVIPSpeed.Rows.Count > 0)
                {
                    shopVIPSpeedConfigRepository.DeleteShopVipSpeedConfig(city);
                }

                using (TransactionScope ts = new TransactionScope())
                {
                    //早市数据，设置8~11，实际取值为8，9，10三个小时
                    ShopVIPSpeedConfig config1 = new ShopVIPSpeedConfig();
                    config1.City = city;
                    config1.StartHour = 8;
                    config1.EndHour = 11;
                    config1.PreUnit = 3;
                    config1.Unit = ShopVIPSpeedConfigUnit.小时;
                    config1.MinSpeed = Common.ToInt32(Math.Round(minSpeed * 0.0164, 0));
                    config1.MaxSpeed = Common.ToInt32(Math.Round(maxSpeed * 0.0164, 0));

                    int i1 = shopVIPSpeedConfigRepository.InsertShopVipSpeedConfig(config1);

                    //午市数据
                    ShopVIPSpeedConfig config2 = new ShopVIPSpeedConfig();
                    config2.City = city;
                    config2.StartHour = 11;
                    config2.EndHour = 14;
                    config2.PreUnit = 3;
                    config2.Unit = ShopVIPSpeedConfigUnit.小时;
                    config2.MinSpeed = Common.ToInt32(Math.Round(minSpeed * 0.232, 0));
                    config2.MaxSpeed = Common.ToInt32(Math.Round(maxSpeed * 0.232, 0));

                    int i2 = shopVIPSpeedConfigRepository.InsertShopVipSpeedConfig(config2);

                    //下午茶时段数据
                    ShopVIPSpeedConfig config3 = new ShopVIPSpeedConfig();
                    config3.City = city;
                    config3.StartHour = 14;
                    config3.EndHour = 16;
                    config3.PreUnit = 2;
                    config3.Unit = ShopVIPSpeedConfigUnit.小时;
                    config3.MinSpeed = Common.ToInt32(Math.Round(minSpeed * 0.0345, 0));
                    config3.MaxSpeed = Common.ToInt32(Math.Round(maxSpeed * 0.0345, 0));

                    int i3 = shopVIPSpeedConfigRepository.InsertShopVipSpeedConfig(config3);

                    //晚市数据
                    ShopVIPSpeedConfig config4 = new ShopVIPSpeedConfig();
                    config4.City = city;
                    config4.StartHour = 16;
                    config4.EndHour = 21;
                    config4.PreUnit = 5;
                    config4.Unit = ShopVIPSpeedConfigUnit.小时;
                    config4.MinSpeed = Common.ToInt32(Math.Round(minSpeed * 0.6808, 0));
                    config4.MaxSpeed = Common.ToInt32(Math.Round(maxSpeed * 0.6808, 0));

                    int i4 = shopVIPSpeedConfigRepository.InsertShopVipSpeedConfig(config4);

                    //午夜时段数据
                    ShopVIPSpeedConfig config5 = new ShopVIPSpeedConfig();
                    config5.City = city;
                    config5.StartHour = 21;
                    config5.EndHour = 1;
                    config5.PreUnit = 4;
                    config5.Unit = ShopVIPSpeedConfigUnit.小时;
                    //config5.MinSpeed = Common.ToInt32(Math.Round(minSpeed * 0.0363, 0));
                    //config5.MaxSpeed = Common.ToInt32(Math.Round(maxSpeed * 0.0363, 0));
                    //为了保证五组数据的值加起来等于minSpeed或maxSpeed
                    config5.MinSpeed = minSpeed - config1.MinSpeed - config2.MinSpeed - config3.MinSpeed - config4.MinSpeed;
                    config5.MaxSpeed = maxSpeed - config1.MaxSpeed - config2.MaxSpeed - config3.MaxSpeed - config4.MaxSpeed;

                    int i5 = shopVIPSpeedConfigRepository.InsertShopVipSpeedConfig(config5);

                    if (i1 > 0 && i2 > 0 && i3 > 0 && i4 > 0 && i5 > 0)
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
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 查询所有/指定城市对应的增速因子展示给客户
        /// </summary>
        /// <param name="cityId"></param>
        /// <returns></returns>
        public DataTable GetCityVipSpeed(int cityId = 0)
        {
            IShopVIPSpeedConfigRepository shopVIPSpeedConfigRepository = RepositoryContext.GetShopVIPSpeedConfigRepository();
            DataTable cityVIPSpeed = shopVIPSpeedConfigRepository.GetCityVipSpeed(cityId);
            return cityVIPSpeed;
        }

        public bool DeleteShopVipSpeed(int cityId)
        {
            IShopVIPSpeedConfigRepository shopVIPSpeedConfigRepository = RepositoryContext.GetShopVIPSpeedConfigRepository();
            return shopVIPSpeedConfigRepository.DeleteShopVipSpeedConfig(cityId);
        }
    }
}
