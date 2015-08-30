using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL;
using System.Data;

namespace VAGastronomistMobileApp.WebPageDll
{
    /// <summary> 
    /// FileName: SystemConfigOperate.cs 
    /// CLRVersion: 4.0.30319.269 
    /// Author: TDQ 
    /// Corporation:杭州友络科技有限公司
    /// Description: 
    /// DateTime: 2012-07-25 09:41:39 
    /// </summary>
    public class SystemConfigOperate
    {
        private readonly SystemConfigManager systemConfigMan = new SystemConfigManager();
        /// <summary>
        /// 添加系统设置
        /// </summary>
        /// <param name="systemConfigInfo"></param>
        public int AddSystemConfig(SystemConfigInfo systemConfigInfo)
        {
            return systemConfigMan.InsertSystemConfig(systemConfigInfo);
        }
        /// <summary>
        /// 修改系统设置
        /// </summary>
        /// <param name="systemConfigInfo"></param>
        public bool ModifySystemConfig(SystemConfigInfo systemConfigInfo)
        {
            return systemConfigMan.UpdateSystemConfig(systemConfigInfo);
        }
        /// <summary>
        /// 查询系统设置信息
        /// </summary>
        /// <returns></returns>
        public DataTable QuerySystemConfig(string configName, string configDescription)
        {
            return systemConfigMan.SelectSystemConfig(configName, configDescription);
        }
        /// <summary>
        /// 获取客户端201401版本背景图片
        /// </summary>
        /// <returns></returns>
        public string QueryClientBgImage()
        {
            return SystemConfigManager.GetClientBgImage();
        }
        /// <summary>
        /// 查询账单上的小计是否显示原价
        /// </summary>
        /// <returns></returns>
        public bool IsBillOrderPriceOriginal()
        {
            DataTable dtSystemConfig = systemConfigMan.SelectSystemConfig(string.Empty,string.Empty);
            DataView dvSystemConfig = dtSystemConfig.DefaultView;
            dvSystemConfig.RowFilter = "configName = 'billOrderPriceType' ";
            bool billOrderPriceType = false;
            if (dvSystemConfig.Count == 1)
            {
                if (Common.ToInt32(dvSystemConfig[0]["configContent"]) == 1)
                {
                    billOrderPriceType = true;//表示不显示原价
                }
            }
            return billOrderPriceType;
        }

        /// <summary>
        /// 检查某个支付方式是否有效
        /// </summary>
        /// <param name="payModeValue"></param>
        /// <returns></returns>
        public bool CheckPayMode(int payModeValue)
        {
            DataTable dt = systemConfigMan.SelectPayMode();
            DataView dv = dt.DefaultView;
            dv.RowFilter = "payModeValue=" + payModeValue + "";
            DataTable dtCopy = dv.ToTable();
            if (dtCopy != null && dtCopy.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 客户端充值功能开启 add by wangc 20140512
        /// </summary>
        /// <returns></returns>
        public static bool ClientRechargeFeatureIsOpen()
        {
            return SystemConfigManager.ClientRechargeFeatureIsOpen();
        }
        /// <summary>
        /// 年夜饭套餐有效周期(单位:分钟)
        /// </summary>
        /// <returns></returns>
        public static double GetVAMealValidPeriod()
        {
            return SystemConfigManager.GetVAMealValidPeriod();
        }

        public static int GetCouponCount()
        {
            return SystemConfigManager.GetCouponCount();
        }
        public static double GetCouponValidDate()
        {
            return SystemConfigManager.GetCouponValidDate();
        }
    }
}
