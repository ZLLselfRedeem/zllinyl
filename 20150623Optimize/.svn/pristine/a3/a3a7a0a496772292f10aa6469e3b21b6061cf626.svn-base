using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL;


namespace VAGastronomistMobileApp.WebPageDll
{
    /// <summary>
    /// 功能描述:收银宝账户体系类
    /// 创建标识:罗国华 20131120
    /// </summary>
    public class SybMoneyOperate
    {
        /// <summary>
        /// 获取使用新账户体系时间 (用于启用新的账户体系)
        /// </summary>
        /// <returns></returns>
        public static DateTime GetUseNewAccountTime()
        {
            return SystemConfigManager.GetUseNewAccountTime();
        }

        /// <summary>
        /// 功能描述:创建商户流水号
        /// 创建标识:罗国华 20131120
        /// </summary>
        /// <param name="shopID">店铺Id</param>
        /// <returns>11位随机数字字符串 + shopId</returns>
        public static string CreateMerchantFlowNumber(int shopID)
        {
            Random ran = new Random();
            return RandomNumNo(ran, 11) + shopID.ToString();
        }

        /// <summary>
        /// 功能描述:创建用户流水号
        /// 创建标识:罗国华 20131120
        /// </summary>
        /// <param name="customerID">用户Id</param>
        /// <returns>11位随机数字字符串 + customerID</returns>
        public static string CreateCustomerFlowNumber(long customerID)
        {
            Random ran = new Random();
            return RandomNumNo(ran, 11) + customerID.ToString();
        }

        /// <summary>
        /// 随机数字串(数字 1-9)
        /// </summary>
        /// <param name="ran"></param>
        /// <param name="xLen"></param>
        /// <returns></returns>
        public static string RandomNumNo(Random ran, int xLen)
        {
            string[] char_array = new string[9];
            char_array[0] = "1";
            char_array[1] = "2";
            char_array[2] = "3";
            char_array[3] = "4";
            char_array[4] = "5";
            char_array[5] = "6";
            char_array[6] = "7";
            char_array[7] = "8";
            char_array[8] = "9";

            string output = "";
            double tmp = 0;
            while (output.Length < xLen)
            {
                tmp = ran.NextDouble();
                output = output + char_array[(int)(tmp * 9)].ToString();
            }
            return output;
        }
    }
}
