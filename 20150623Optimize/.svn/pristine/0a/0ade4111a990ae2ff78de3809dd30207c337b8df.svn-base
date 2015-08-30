using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.SQLServerDAL;
using System.Data;
using System.Collections;
using System.Reflection;
using VAGastronomistMobileApp.Model;

namespace VAGastronomistMobileApp.WebPageDll
{
    /// <summary>
    /// 报表统计操作
    /// </summary>
    public class StatisticalStatementOperate
    {
        protected StatisticalStatementManager ssManager = new StatisticalStatementManager();
        #region 订单model（没用）
        ///// <summary>
        ///// 订单量统计model
        ///// </summary>
        //public class OrderStatisticsInfo
        //{
        //    public string orderTime { get; set; }//订单时间
        //    public int orderNumber { get; set; }//订单笔数
        //    public int orderNumberIncrement { get; set; }//订单笔数增量
        //    public double orderAmount { get; set; }//订单金额
        //    public string orderAmountIncrement { get; set; }//订单金额增量
        //    public double perCustomerTransaction { get; set; }//客单价
        //    public string perCustomerTransactionDiurnalVariation { get; set; }//客单价日变化
        //}
        ///// <summary>
        ///// 支付量统计model
        ///// </summary>
        //public class PayAmountStatisticsInfo
        //{
        //    public string payOrderTime { get; set; }//支付日期
        //    public int orderNumber { get; set; }//订单笔数
        //    public int payOrderNumber { get; set; }//支付订单笔数
        //    public string payCompleteProportion { get; set; }//支付完成比例
        //    public int payOrderNumberFloat { get; set; }//支付笔数浮动
        //    public double payOrderAmount { get; set; }//支付金额
        //    public string payOrderAmountIncrement { get; set; }//支付金额增量
        //    public double payPerCustomerTransaction { get; set; }//支付客单价
        //    public string payPerCustomerTransactionDiurnalVariation { get; set; }//支付客单价日变化
        //}
        #endregion
        /// <summary>
        /// 获得统计订单量的基本信息
        /// </summary>
        public DataTable GetOrderStatisticsInfo(DateTime strTime, DateTime endTime, int company, int shopId, int cityId, string dinnerStarTime, string einnerEndTime, int orderStatus)
        {
            DataTable dt = new DataTable();
            if (strTime <= endTime)
            {
                if (CompareTimerStr(dinnerStarTime, einnerEndTime) == false)
                {
                    //跨天查询
                    dt = ssManager.GetOrderStatistics(strTime, endTime, company, shopId, cityId, dinnerStarTime, einnerEndTime, orderStatus, true);
                }
                else
                {
                    //本天查询
                    dt = ssManager.GetOrderStatistics(strTime, endTime, company, shopId, cityId, dinnerStarTime, einnerEndTime, orderStatus, false);
                }
                //  List<OrderStatisticsInfo> list = new List<OrderStatisticsInfo>();
                // dt = ssManager.GetOrderStatistics(strTime, endTime, company, dinnerStarTime, einnerEndTime, orderStatus);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        // OrderStatisticsInfo orderStatisticsInformation = new OrderStatisticsInfo();
                        // orderStatisticsInformation.orderTime = dt.Rows[i]["orderTime"].ToString();
                        // orderStatisticsInformation.orderAmount = Common.ToDouble(dt.Rows[i]["orderAmount"]);
                        // orderStatisticsInformation.orderNumber = Common.ToInt32(dt.Rows[i]["orderNumber"]);
                        // orderStatisticsInformation.perCustomerTransaction = Common.ToDouble(dt.Rows[i]["perCustomerTransaction"]);
                        if (i > 0)
                        {
                            dt.Rows[i]["orderAmountIncrement"] = (Common.ToDouble(dt.Rows[i]["orderAmount"]) - Common.ToDouble(dt.Rows[i - 1]["orderAmount"])).ToString("0.00");
                            // orderStatisticsInformation.orderAmountIncrement = (Common.ToDouble(dt.Rows[i]["orderAmount"]) - Common.ToDouble(dt.Rows[i - 1]["orderAmount"])).ToString("0.0");
                            dt.Rows[i]["orderNumberIncrement"] = Common.ToInt32(dt.Rows[i]["orderNumber"]) - Common.ToInt32(dt.Rows[i - 1]["orderNumber"]);
                            // orderStatisticsInformation.orderNumberIncrement = Common.ToInt32(dt.Rows[i]["orderNumber"]) - Common.ToInt32(dt.Rows[i - 1]["orderNumber"]);
                            //支付金额增量
                            dt.Rows[i]["payOrderAmountIncrement"] = Common.ToInt32(dt.Rows[i]["payOrderAmount"]) - Common.ToInt32(dt.Rows[i - 1]["payOrderAmount"]);
                            if (Common.ToDouble(dt.Rows[i]["perCustomerTransaction"]) == 0)
                            {
                                dt.Rows[i]["orderAmountIncrement"] = "0.00";
                                // orderStatisticsInformation.perCustomerTransactionDiurnalVariation = "0";
                            }
                            else
                            {
                                if (Common.ToDouble(dt.Rows[i - 1]["perCustomerTransaction"]) != 0)
                                {
                                    dt.Rows[i]["perCustomerTransactionDiurnalVariation"] = ((Common.ToDouble(dt.Rows[i]["perCustomerTransaction"])
                                                                 - Common.ToDouble(dt.Rows[i - 1]["perCustomerTransaction"])) / Common.ToDouble(dt.Rows[i - 1]["perCustomerTransaction"])).ToString("0.00%");
                                    // orderStatisticsInformation.perCustomerTransactionDiurnalVariation = ((Common.ToDouble(dt.Rows[i]["perCustomerTransaction"])
                                    //  - Common.ToDouble(dt.Rows[i - 1]["perCustomerTransaction"])) / Common.ToDouble(dt.Rows[i - 1]["perCustomerTransaction"])).ToString("0.0%");
                                }
                                else
                                {
                                    dt.Rows[i]["perCustomerTransactionDiurnalVariation"] = "0.00%";
                                }
                            }
                        }
                        // list.Add(orderStatisticsInformation);
                    }
                    return dt;
                }
                else
                {
                    return dt;
                }
            }
            else
            {
                return dt;
            }
        }
        /// <summary>
        /// 将集合类转换成DataTable
        /// </summary>
        public DataTable ToDataTable(IList list)
        {
            DataTable result = new DataTable();
            if (list.Count > 0)
            {
                PropertyInfo[] propertys = list[0].GetType().GetProperties();
                foreach (PropertyInfo pi in propertys)
                {
                    result.Columns.Add(pi.Name, pi.PropertyType);
                }
                for (int i = 0; i < list.Count; i++)
                {
                    ArrayList tempList = new ArrayList();
                    foreach (PropertyInfo pi in propertys)
                    {
                        object obj = pi.GetValue(list[i], null);
                        tempList.Add(obj);
                    }
                    object[] array = tempList.ToArray();
                    result.LoadDataRow(array, true);
                }
            }
            return result;
        }
        /// <summary>
        /// 获得统计支付订单量的基本信息
        /// </summary>
        public DataTable GetPayOrderStatisticsInfo(DateTime strTime, DateTime endTime, int company, int shopId, int cityId, string dinnerStarTime, string einnerEndTime)
        {
            DataTable dt = new DataTable();
            if (strTime <= endTime)
            {
                if (CompareTimerStr(dinnerStarTime, einnerEndTime) == false)
                {
                    //跨天查询
                    dt = ssManager.GetPayOrderStatistics(strTime, endTime, company, shopId, cityId, dinnerStarTime, einnerEndTime, true);
                }
                else
                {
                    //本天查询
                    dt = ssManager.GetPayOrderStatistics(strTime, endTime, company, shopId, cityId, dinnerStarTime, einnerEndTime, false);
                }
                // List<PayAmountStatisticsInfo> list = new List<PayAmountStatisticsInfo>();
                //dt = ssManager.GetPayOrderStatistics(strTime, endTime, company, dinnerStarTime, einnerEndTime);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        // PayAmountStatisticsInfo payAmountStatisticsInfo = new PayAmountStatisticsInfo();
                        // payAmountStatisticsInfo.payOrderTime = dt.Rows[i]["payOrderTime"].ToString();
                        //  payAmountStatisticsInfo.orderNumber = Common.ToInt32(dt.Rows[i]["orderNumber"]);
                        // payAmountStatisticsInfo.payOrderNumber = Common.ToInt32(dt.Rows[i]["payOrderNumber"]);//(30.0/25).ToString("0.0%")
                        //  payAmountStatisticsInfo.payOrderAmount = Common.ToDouble(dt.Rows[i]["payOrderAmount"]);
                        //  payAmountStatisticsInfo.payPerCustomerTransaction = Common.ToDouble(dt.Rows[i]["payPerCustomerTransaction"]);
                        // payAmountStatisticsInfo.payCompleteProportion = (Common.ToDouble(dt.Rows[i]["payOrderNumber"]+".0") / Common.ToInt32(dt.Rows[i]["orderNumber"])).ToString("0.0%");
                        dt.Rows[i]["payCompleteProportion"] = (Common.ToDouble(dt.Rows[i]["payOrderNumber"] + ".00") / Common.ToInt32(dt.Rows[i]["orderNumber"])).ToString("0.00%");
                        if (i > 0)
                        {
                            dt.Rows[i]["payOrderNumberFloat"] = Common.ToInt32(dt.Rows[i]["payOrderNumber"]) - Common.ToInt32(dt.Rows[i - 1]["payOrderNumber"]);
                            dt.Rows[i]["payOrderAmountIncrement"] = (Common.ToDouble(dt.Rows[i]["payOrderAmount"]) - Common.ToDouble(dt.Rows[i - 1]["payOrderAmount"])).ToString("0.00");
                            // payAmountStatisticsInfo.payOrderNumberFloat = Common.ToInt32(dt.Rows[i]["payOrderNumber"]) - Common.ToInt32(dt.Rows[i - 1]["payOrderNumber"]);
                            // payAmountStatisticsInfo.payOrderAmountIncrement = (Common.ToDouble(dt.Rows[i]["payOrderAmount"]) - Common.ToDouble(dt.Rows[i - 1]["payOrderAmount"])).ToString("0.0");
                            if (Common.ToDouble(dt.Rows[i]["payPerCustomerTransaction"]) == 0)
                            {
                                dt.Rows[i]["payOrderAmountIncrement"] = "0.00";
                                // payAmountStatisticsInfo.payPerCustomerTransactionDiurnalVariation = "0";
                            }
                            else
                            {
                                if (Common.ToDouble(dt.Rows[i - 1]["payPerCustomerTransaction"]) != 0)
                                {
                                    dt.Rows[i]["payPerCustomerTransactionDiurnalVariation"] = ((Common.ToDouble(dt.Rows[i]["payPerCustomerTransaction"])
                                        - Common.ToDouble(dt.Rows[i - 1]["payPerCustomerTransaction"])) / Common.ToDouble(dt.Rows[i - 1]["payPerCustomerTransaction"])).ToString("0.00%");
                                    // payAmountStatisticsInfo.payPerCustomerTransactionDiurnalVariation = ((Common.ToDouble(dt.Rows[i]["payPerCustomerTransaction"])
                                    //  - Common.ToDouble(dt.Rows[i - 1]["payPerCustomerTransaction"])) / Common.ToDouble(dt.Rows[i - 1]["payPerCustomerTransaction"])).ToString("0.0%");
                                }
                                else
                                {
                                    dt.Rows[i]["payPerCustomerTransactionDiurnalVariation"] = "0.00%";
                                }
                            }
                        }
                        // list.Add(payAmountStatisticsInfo);
                    }
                    return dt;
                }
                else
                {
                    return dt;
                }
            }
            else
            {
                return dt;
            }
        }
        /// <summary>
        /// 每个时间段的详细的订单信息
        /// </summary>
        /// <param name="condition">condition = PayOrder，返回为支付过订单信息</param>
        /// <param name="orderStatus">查询订单时过滤所有和已验证的订单</param>
        /// <returns></returns>
        public DataTable QueryOrderDetailStatisticsInfo(DateTime strTime, DateTime endTime, int company, int shopId, string strHour, string endHour, string condition, int orderStatus)
        {
            if (CompareTimerStr(strHour, endHour) == false)
            {
                //跨天查询
                return ssManager.GetOrderDetailStatisticsInfo(strTime, endTime, company, shopId, strHour, endHour, condition, orderStatus, true);
            }
            else
            {
                //本天查询
                return ssManager.GetOrderDetailStatisticsInfo(strTime, endTime, company, shopId, strHour, endHour, condition, orderStatus, false);
            }
        }
        /// <summary>
        /// 比较时间字符串
        /// </summary>
        protected bool CompareTimerStr(string hourTimerStr, string hourTimerEnd)
        {
            string[] intStart = hourTimerStr.Split(':');
            string[] intEnd = hourTimerEnd.Split(':');
            if (Common.ToInt32(intStart[0]) > Common.ToInt32(intEnd[0]))
            {
                return false;
            }
            else
            {
                if (Common.ToInt32(intStart[1]) > Common.ToInt32(intEnd[1]))
                {
                    return false;
                }
                else
                {
                    if (Common.ToInt32(intStart[2]) > Common.ToInt32(intEnd[2]))
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
        }
        /// <summary>
        /// 查询获得城市用户增量
        /// </summary>
        /// <returns></returns>
        public DataTable QueryUsersAmountByDiffCity(DateTime strTime, DateTime endTime, string strHour, string endHour)
        {
            DataTable dt = ssManager.QueryUsersAmountByDifferentCity(strTime, endTime, strHour, endHour);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (i != 0)
                {
                    dt.Rows[i]["dayAddAmout"] = Common.ToInt32(dt.Rows[i]["usersAddAmount"]) - Common.ToInt32(dt.Rows[i]["usersAddAmount"]);//计算出的是日新增量
                }
            }
            return dt;
        }
        /// <summary>
        /// 查询获得城市每个不同时间段用户数量的增量
        /// </summary>
        public DataTable QueryUserAmountByDiffHour(DateTime strTime, DateTime endTime, string strHour, string endHour)
        {
            DataTable dt = ssManager.QueryUserAmountByDifferentHour(strTime, endTime, strHour, endHour);
            return dt;
        }
        /// <summary>
        ///  门店数量统计
        /// </summary>
        /// <param name="strTime"></param>
        /// <param name="endTime"></param>
        /// <param name="handleStusts"></param>
        /// <param name="cityId"></param>
        /// <returns></returns>
        public DataTable QueryShopAmountByDiffCity(string strTime, string endTime, int handleStusts, int cityId)
        {
            DataTable dt = ssManager.QueryShopAmountByDifferentCity(strTime, endTime, handleStusts, cityId);
            return dt;
        }
        /// <summary>
        /// 按城市、设备类型统计会员数量
        /// </summary>
        /// <returns></returns>
        public DataTable QueryMemberAmountByDiffCity(DateTime strTime, DateTime endTime, int provinceID, int cityID, out int nTotal)
        {
            DataTable dt = ssManager.QueryMemberAmountByDifferentCity(strTime, endTime, provinceID, cityID);
            nTotal = 0; //返回总的会员数量
            foreach (DataRow dr in dt.Rows)
            {
                nTotal += Common.ToInt32(dr[0].ToString());
            }
            return dt;
        }
        /// <summary>
        /// 按城市显示会员信息
        /// </summary>
        /// <returns></returns>
        public DataTable QueryMemberDetailByDiffCity(DateTime strTime, DateTime endTime, int provinceID, int cityID)
        {
            DataTable dt = ssManager.QueryMemberByDifferentCity(strTime, endTime, provinceID, cityID);

            return dt;
        }
        /// <summary>
        /// 插入访问API接口日志记录
        /// </summary>
        /// <returns></returns>
        public long InsertInvokedAPILog(int invokedAPIType)
        {
            return ssManager.InsertInvokedAPILogInfo(invokedAPIType);
        }
        /// <summary>
        /// 查询访问API的次数
        /// </summary>
        /// <returns></returns>
        public DataTable QueryInvokedAPILog(DateTime strTime, DateTime endTime)
        {
            return ssManager.QueryInvokedAPILogInfo(strTime, endTime);
        }
        /// <summary>
        ///  查询需要导出excel表格的预点单信息
        /// </summary>
        /// <param name="cityId"></param>
        /// <returns></returns>
        public DataTable QueryPreOrder(int cityId)
        {
            return ssManager.QueryPreOrderInfo(cityId);
        }
        /// <summary>
        /// 统计预点单连续四周的数据
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="shopId"></param>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public DataTable QueryConsumptionWeekStatistics(int companyId, int shopId, DateTime dateTime)
        {
            string chainGrowth_newPreOrderCount = "";//新增单数(环比增长率)
            string chainGrowth_newPreOrderAmount = "";//新增金额(环比增长率)
            string chainGrowth_newPreOrderCount_Shop = "";//新增到店(环比增长率)
            string chainGrowth_newPreOrderAmount_Shop = "";//新增到店金额(环比增长率)
            string chainGrowth_newPreOrderCount_Shop_Proportion = "";//到店比例(环比增长率)
            string chainGrowth_newPreOrderCount_isPaid = "";//新增手机支付（笔）(环比增长率)
            string chainGrowth_newPreOrderAmount_isPaid = "";//新增手机支付金额(环比增长率)
            string chainGrowth_newPreOrderAmount_isPaid_Proportion = "";//支付比例(环比增长率)
            DataTable dt = ssManager.QueryConsumptionWeekStatisticsInfo(companyId, shopId, dateTime);
            int countDt = dt.Rows.Count;
            dt.Columns.Add("id");//标记列

            for (int j = 0; j < dt.Rows.Count; j++)
            {
                dt.Rows[j]["id"] = j * 2 + 1;
                //到店比例
                dt.Rows[j]["newPreOrderCount_Shop_Proportion"] = (Common.ToDouble(Common.ToInt32(dt.Rows[j]["newPreOrderCount_Shop"]) + ".0") / Common.ToInt32(dt.Rows[j]["newPreOrderCount"])).ToString("0.0%");
                //支付比例
                dt.Rows[j]["newPreOrderAmount_isPaid_Proportion"] = (Common.ToDouble(Common.ToInt32(dt.Rows[j]["newPreOrderCount_isPaid"]) + ".0") / Common.ToInt32(dt.Rows[j]["newPreOrderCount"])).ToString("0.0%");
            }
            for (int i = 0; i < countDt; i++)
            {

                if (i != 0)
                {
                    //求环比增长率
                    chainGrowth_newPreOrderCount = (Common.ToDouble((Common.ToInt32(dt.Rows[i]["newPreOrderCount"]) - Common.ToInt32(dt.Rows[i - 1]["newPreOrderCount"])) + ".0") / Common.ToInt32(dt.Rows[i]["newPreOrderCount"])).ToString("0.0%");
                    chainGrowth_newPreOrderAmount = (Common.ToDouble((Common.ToInt32(dt.Rows[i]["newPreOrderAmount"]) - Common.ToInt32(dt.Rows[i - 1]["newPreOrderAmount"])) + ".0") / Common.ToInt32(dt.Rows[i]["newPreOrderAmount"])).ToString("0.0%");
                    chainGrowth_newPreOrderCount_Shop = (Common.ToDouble((Common.ToInt32(dt.Rows[i]["newPreOrderCount_Shop"]) - Common.ToInt32(dt.Rows[i - 1]["newPreOrderCount_Shop"])) + ".0") / Common.ToInt32(dt.Rows[i]["newPreOrderCount_Shop"])).ToString("0.0%");
                    chainGrowth_newPreOrderAmount_Shop = (Common.ToDouble((Common.ToInt32(dt.Rows[i]["newPreOrderAmount_Shop"]) - Common.ToInt32(dt.Rows[i - 1]["newPreOrderAmount_Shop"])) + ".0") / Common.ToInt32(dt.Rows[i]["newPreOrderAmount_Shop"])).ToString("0.0%");
                    // chainGrowth_newPreOrderCount_Shop_Proportion = (Common.ToDouble((Common.ToInt32(dt.Rows[i]["newPreOrderCount_Shop_Proportion"]) - Common.ToInt32(dt.Rows[i - 1]["newPreOrderCount_Shop_Proportion"])) + ".0") / Common.ToInt32(dt.Rows[i]["newPreOrderCount_Shop_Proportion"])).ToString("0.0%");
                    //这两条数据上面for循环已经计算为**%数值，此处再转换成double类型
                    double double1 = float.Parse(dt.Rows[i]["newPreOrderCount_Shop_Proportion"].ToString().Substring(0, dt.Rows[i]["newPreOrderCount_Shop_Proportion"].ToString().Length - 1));
                    double double2 = float.Parse(dt.Rows[i - 1]["newPreOrderCount_Shop_Proportion"].ToString().Substring(0, dt.Rows[i - 1]["newPreOrderCount_Shop_Proportion"].ToString().Length - 1));
                    chainGrowth_newPreOrderCount_Shop_Proportion = ((double1 - double2) / double1).ToString("0.0%");

                    chainGrowth_newPreOrderCount_isPaid = (Common.ToDouble((Common.ToInt32(dt.Rows[i]["newPreOrderCount_isPaid"]) - Common.ToInt32(dt.Rows[i - 1]["newPreOrderCount_isPaid"])) + ".0") / Common.ToInt32(dt.Rows[i]["newPreOrderCount_isPaid"])).ToString("0.0%");
                    chainGrowth_newPreOrderAmount_isPaid = (Common.ToDouble((Common.ToInt32(dt.Rows[i]["newPreOrderAmount_isPaid"]) - Common.ToInt32(dt.Rows[i - 1]["newPreOrderAmount_isPaid"])) + ".0") / Common.ToInt32(dt.Rows[i]["newPreOrderAmount_isPaid"])).ToString("0.0%");
                    //chainGrowth_newPreOrderAmount_isPaid_Proportion = (Common.ToDouble((Common.ToInt32(dt.Rows[i]["newPreOrderAmount_isPaid_Proportion"]) - Common.ToInt32(dt.Rows[i - 1]["newPreOrderAmount_isPaid_Proportion"])) + ".0") / Common.ToInt32(dt.Rows[i]["newPreOrderAmount_isPaid_Proportion"])).ToString("0.0%");

                    double double3 = float.Parse(dt.Rows[i]["newPreOrderAmount_isPaid_Proportion"].ToString().Substring(0, dt.Rows[i]["newPreOrderAmount_isPaid_Proportion"].ToString().Length - 1));
                    double double4 = float.Parse(dt.Rows[i - 1]["newPreOrderAmount_isPaid_Proportion"].ToString().Substring(0, dt.Rows[i - 1]["newPreOrderAmount_isPaid_Proportion"].ToString().Length - 1));
                    chainGrowth_newPreOrderAmount_isPaid_Proportion = ((double3 - double4) / double3).ToString("0.0%");
                }
                switch (i)
                {
                    case 0:
                        dt.Rows[i]["week"] = "本周";
                        break;
                    case 1:
                        dt.Rows[i]["week"] = "上周";
                        dt.Rows.Add(new object[] { "环比增长", chainGrowth_newPreOrderCount, chainGrowth_newPreOrderAmount, chainGrowth_newPreOrderCount_Shop,
                            chainGrowth_newPreOrderCount_Shop_Proportion, chainGrowth_newPreOrderAmount_Shop, chainGrowth_newPreOrderCount_isPaid, chainGrowth_newPreOrderAmount_isPaid, chainGrowth_newPreOrderAmount_isPaid_Proportion ,i*2});
                        break;
                    case 2:
                        dt.Rows[i]["week"] = "上上周";
                        dt.Rows.Add(new object[] { "环比增长", chainGrowth_newPreOrderCount, chainGrowth_newPreOrderAmount, chainGrowth_newPreOrderCount_Shop,
                            chainGrowth_newPreOrderCount_Shop_Proportion, chainGrowth_newPreOrderAmount_Shop, chainGrowth_newPreOrderCount_isPaid, chainGrowth_newPreOrderAmount_isPaid, chainGrowth_newPreOrderAmount_isPaid_Proportion ,i*2});
                        break;
                    case 3:
                        dt.Rows[i]["week"] = "上上上周";
                        dt.Rows.Add(new object[] { "环比增长", chainGrowth_newPreOrderCount, chainGrowth_newPreOrderAmount, chainGrowth_newPreOrderCount_Shop,
                            chainGrowth_newPreOrderCount_Shop_Proportion, chainGrowth_newPreOrderAmount_Shop, chainGrowth_newPreOrderCount_isPaid, chainGrowth_newPreOrderAmount_isPaid, chainGrowth_newPreOrderAmount_isPaid_Proportion ,i*2});
                        break;
                }
            }
            dt.DefaultView.Sort = "id ASC";//重新排序
            return dt;
        }
        /// <summary>
        /// 统计预点单汇总
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public DataTable QueryAllConsumptionWeekStatistics(int companyId, int shopId)
        {
            DataTable dt = ssManager.QueryAllConsumptionWeekStatisticsInfo(companyId, shopId);
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                //到店比例
                dt.Rows[j]["newPreOrderCount_Shop_Proportion"] = (Common.ToDouble(Common.ToInt32(dt.Rows[j]["newPreOrderCount_Shop"]) + ".0") / Common.ToInt32(dt.Rows[j]["newPreOrderCount"])).ToString("0.0%");
                //支付比例
                dt.Rows[j]["newPreOrderAmount_isPaid_Proportion"] = (Common.ToDouble(Common.ToInt32(dt.Rows[j]["newPreOrderCount_isPaid"]) + ".0") / Common.ToInt32(dt.Rows[j]["newPreOrderCount"])).ToString("0.0%");
            }
            return dt;
        }
        /// <summary>
        /// 综合查询
        /// </summary>
        /// <param name="strTime"></param>
        /// <param name="endTime"></param>
        /// <param name="company"></param>
        /// <param name="shopId"></param>
        /// <param name="cityId"></param>
        /// <param name="strHour"></param>
        /// <param name="endHour"></param>
        /// <param name="flag"></param>
        /// <param name="flagStatus">为true表示需要减掉点单退款金额</param>
        /// <returns></returns>
        public DataTable GetComprehensiveStatistics(DateTime strTime, DateTime endTime, int company, int shopId, int cityId, string strHour, string endHour, bool flag, bool flagStatus = false)
        {
            DataTable dt = ssManager.GetComprehensiveStatisticsInfo(strTime, endTime, company, shopId, cityId, strHour, endHour, flag, flagStatus);
            return dt;
        }
        /// <summary>
        /// 门店列表 add by wangc 20140321
        /// </summary>
        /// <param name="strTime"></param>
        /// <param name="endTime"></param>
        /// <param name="companyId"></param>
        /// <param name="shopId"></param>
        /// <param name="cityId"></param>
        /// <returns></returns>
        public DataTable QueryShopOrderList(string strTime, string endTime, int companyId, int shopId, int cityId)
        {
            return ssManager.GetShopOrderList(strTime, endTime, companyId, shopId, cityId);
        }
        /// <summary>
        /// 单店单日订单列表  add by wangc 20140321
        /// </summary>
        /// <param name="strTime"></param>
        /// <param name="endTime"></param>
        /// <param name="shopId"></param>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public DataTable QuerySingleShopDayOrderList(string strTime, string endTime, int shopId, int companyId)
        {
            return ssManager.GetSingleShopDayOrderList(strTime, endTime, shopId, companyId);
        }
        /// <summary>
        /// 时间段点单用户数量统计 add by wangc 20140322
        /// </summary>
        /// <param name="strTime">开始时间</param>
        /// <param name="endTime">截止时间</param>
        /// <returns></returns>
        public DataTable QueryOrderCustomer(string strTime, string endTime)
        {
            return ssManager.GetOrderCustomer(strTime, endTime);
        }
        /// <summary>
        /// 用户积分统计  add by wangc 20140324
        /// </summary>
        /// <param name="strTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public DataTable QueryPointEmployee(string strTime, string endTime)
        {
            return ssManager.GetPointEmployee(strTime, endTime);
        }
        /// <summary>
        /// 查询悠先服务活跃用户 add by wangc 20140324
        /// </summary>
        /// <param name="strTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public DataTable QueryActiveEmployee(string strTime, string endTime)
        {
            return ssManager.GetActiveEmployee(strTime, endTime);
        }
        /// <summary>
        /// 查询门店点单数量 add by wangc 20140325
        /// </summary>
        /// <param name="minAmount"></param>
        /// <param name="maxAmount"></param>
        /// <param name="strTime"></param>
        /// <param name="endTime"></param>
        /// <param name="flag"></param>
        /// <param name="flag1"></param>
        /// <param name="cityId"></param>
        /// <returns></returns>
        public DataTable QueryShopOrder(double minAmount, double maxAmount, string strTime, string endTime, int flag, int flag1, int cityId,int? shopState = null)
        {
            DataTable initData = ssManager.SelectShopOrderByCount(minAmount, maxAmount, strTime, endTime, flag, flag1, cityId);
            DataTable tempTable = new DataTable();
            TimeSpan d = Common.ToDateTime(endTime) - Common.ToDateTime(strTime);
            int dateCount = d.Days + 1;//需要统计的天数
            int tempTableCount = dateCount + 5;//需要添加五列
            for (int i = 0; i < tempTableCount; i++)
            {
                //动态添加Table列数据
                switch (i)
                {
                    case 0:
                        tempTable.Columns.Add(new DataColumn("城市", typeof(String)));
                        break;
                    case 1://门店状态
                        tempTable.Columns.Add(new DataColumn("是否上线", typeof(String)));
                        break;
                    case 2://门店名称
                        tempTable.Columns.Add(new DataColumn("店铺名称", typeof(String)));
                        break;
                    case 3://客户经理
                        tempTable.Columns.Add(new DataColumn("客户代表", typeof(String)));
                        break;
                    case 4://交易累计
                        tempTable.Columns.Add(new DataColumn("交易累计", typeof(String)));
                        break;
                    default://具体天数日期统计
                        tempTable.Columns.Add(new DataColumn(Common.ToDateTime(strTime).AddDays(i - 5).Month.ToString() + "月" + Common.ToDateTime(strTime).AddDays(i - 5).Day.ToString() + "日", typeof(String)));
                        break;
                }
            }
            DataView dv = initData.DefaultView;
            DataTable shopIdDt = ssManager.GetShopId(cityId);//获取所有公司的ID
            if (shopIdDt.Rows.Count > 0)
            {
                DataRow newDr;
                EmployeeOperate employeeOper = new EmployeeOperate();
                CityOperate cityOperate = new CityOperate();
                for (int i = 0; i < shopIdDt.Rows.Count; i++)//循环门店
                {
                    dv.RowFilter = "shopID = " + Common.ToInt32(shopIdDt.Rows[i]["shopID"]);//对于上线的门店一家一家处理
                    dv.Sort = " time asc";
                    newDr = tempTable.NewRow();//创建一行
                    if (dv.Count > 0)
                    {
                        newDr[0] = Common.ToString(dv[0]["cityName"]);//城市名称
                        newDr[1] = Common.ToString(dv[0]["shopStatus"]);//门店状态
                        newDr[2] = Common.ToString(dv[0]["shopName"]);//门店名称
                        newDr[3] = Common.ToString(dv[0]["EmployeeFirstName"]);//客户代表
                        double total = 0;//总计
                        for (int n = 0; n < dv.Count; n++)//门店
                        {
                            total += Common.ToDouble(dv[n]["total"]);
                            for (int j = 0; j < dateCount; j++)//循环统计日期天数
                            {
                                if (Common.ToDateTime(strTime).AddDays(j).ToString("yyyy/MM/dd") == Common.ToString(dv[n]["time"]))
                                {
                                    newDr[j + 5] = Common.ToString(dv[n]["total"]);//单日统计
                                }
                                else
                                {
                                    if (Common.ToString(newDr[j + 5]) == "" || Common.ToString(newDr[j + 5]) == "0")
                                    {
                                        newDr[j + 5] = "0";//单日统计
                                    }
                                }
                            }
                        }
                        newDr[4] = Common.ToString(total);
                    }
                    else
                    {
                        newDr = tempTable.NewRow();//创建一行
                        //表示当前门店这段时间没有一个订单
                        CityExt city = cityOperate.GetCityNameAndShopName(Common.ToInt32(shopIdDt.Rows[i]["shopID"]));
                        if (city != null)
                        {
                            newDr[0] = city.cityName;//城市名称
                            newDr[1] = city.isHandle;//门店状态
                            newDr[2] = city.shopName;//门店名称
                            if (city.accountManager > 0)
                            {
                                EmployeeInfo employeeInfo = employeeOper.QueryEmployee(city.accountManager);
                                if (employeeInfo != null)
                                {
                                    newDr[3] = employeeInfo.EmployeeFirstName;//客户代表
                                }
                                else
                                {
                                    newDr[3] = "";
                                }
                            }
                            else
                            {
                                newDr[3] = "";
                            }
                        }
                        newDr[4] = "0";//此时总计为0
                        for (int j = 5; j < tempTableCount; j++)
                        {
                            newDr[j] = "0";//每天各项统计也为0
                        }
                    }
                    tempTable.Rows.Add(newDr);
                }
            }
            return tempTable;
        }
        /// <summary>
        /// 数据统计 add by wangc 
        /// 20140411
        /// </summary>
        /// <param name="strTime"></param>
        /// <param name="endTime"></param>
        /// <param name="companyId"></param>
        /// <param name="shopId"></param>
        /// <param name="cityId"></param>
        /// <param name="strHour"></param>
        /// <param name="endHour"></param>
        /// <param name="flag"></param>
        /// <param name="flagStatus"></param>
        /// <returns></returns>
        public DataTable GetDataStatistics(DateTime strTime, DateTime endTime, int companyId, int shopId, int cityId, string strHour, string endHour, bool flag, bool flagStatus = false)
        {
            DataTable dt = ssManager.GetComprehensiveStatisticsInfo(strTime, endTime, companyId, shopId, cityId, strHour, endHour, flag, flagStatus);
            DataTable tempTable = new DataTable();
            TimeSpan d = Common.ToDateTime(endTime) - Common.ToDateTime(strTime);
            int dateCount = d.Days + 1;//需要统计的天数
            //Table列
            tempTable.Columns.Add(new DataColumn("orderTime", typeof(String)));//日期
            tempTable.Columns.Add(new DataColumn("dayAddUserCount", typeof(Int32)));//日新增用户
            tempTable.Columns.Add(new DataColumn("userCount", typeof(Int32)));//APP总激活数
            tempTable.Columns.Add(new DataColumn("dayActiveUserCount", typeof(Int32)));//日活跃数（登录用户）
            tempTable.Columns.Add(new DataColumn("orderCount", typeof(Int32)));//订单笔数
            tempTable.Columns.Add(new DataColumn("orderSumAmount", typeof(Double)));//订单金额
            tempTable.Columns.Add(new DataColumn("timesOrderQuantity", typeof(Int32)));//属于第二次及以上下单的数量
            tempTable.Columns.Add(new DataColumn("timersOrderRate", typeof(Double)));//第二次及以上下单比例
            tempTable.Columns.Add(new DataColumn("isPaidOrderCount", typeof(Int32)));//支付笔数
            tempTable.Columns.Add(new DataColumn("timersPayCount", typeof(Int32)));//二次及以上支付
            tempTable.Columns.Add(new DataColumn("timersPayRate", typeof(Double)));//第二次及以上支付比例
            tempTable.Columns.Add(new DataColumn("payRate", typeof(Double)));//支付率
            tempTable.Columns.Add(new DataColumn("isPaidOrderAmount", typeof(Double)));//支付金额
            tempTable.Columns.Add(new DataColumn("refundOrderCount", typeof(Int32)));//日退单数
            tempTable.Columns.Add(new DataColumn("refundOrderAmount", typeof(Double)));//退款金额
            DataRow newDr;
            DataTable dtTemporary = new DataTable();
            //添加Table行数据
            int dtCount = dt.Rows.Count;//节省效率
            string initTime = string.Empty;
            string dtTime = string.Empty;
            bool status = false;
            int timesOrderQuantity = 0;
            int timersPayCount = 0;
            for (int i = 0; i < dateCount; i++)
            {
                newDr = tempTable.NewRow();//创建一行
                status = false;
                dtTime = strTime.AddDays(i).ToString("yyyy/MM/dd");
                for (int j = 0; j < dtCount; j++)
                {
                    if (dtTime == Common.ToDateTime(dt.Rows[j]["orderTime"]).ToString("yyyy/MM/dd"))
                    {
                        //存在点单数据
                        initTime = Common.ToString(dt.Rows[j]["orderTime"]);
                        newDr[0] = initTime;//日期
                        dtTemporary = ssManager.GetUserStatistics(initTime);//不会count<>1
                        newDr[1] = Common.ToString(dtTemporary.Rows[0]["dayAddUserCount"]);//日新增用户
                        newDr[2] = Common.ToString(dtTemporary.Rows[0]["userCount"]);//APP总激活数
                        newDr[3] = ssManager.GetActiveUserStatistics(initTime); //日活跃数（登录用户）
                        newDr[4] = Common.ToString(dt.Rows[j]["orderCount"]);//订单笔数
                        newDr[5] = Common.ToString(dt.Rows[j]["orderSumAmount"]);//订单金额
                        timesOrderQuantity = ssManager.GetTimesOrderQuantity(initTime, companyId, shopId);
                        newDr[6] = timesOrderQuantity.ToString();//属于第二次及以上下单的数量
                        newDr[7] = Common.ToDouble((timesOrderQuantity / Common.ToDouble(dt.Rows[j]["orderCount"])) * 100);//第二次及以上下单比例
                        newDr[8] = Common.ToString(dt.Rows[j]["isPaidOrderCount"]);//支付笔数
                        timersPayCount = ssManager.GetTimesPayCount(initTime, companyId, shopId);
                        newDr[9] = timersPayCount.ToString();//二次及以上支付
                        if (timersPayCount == 0 || Common.ToInt32(dt.Rows[j]["isPaidOrderCount"]) == 0)
                        {
                            newDr[10] = 0;
                        }
                        else
                        {
                            newDr[10] = Common.ToDouble((timersPayCount / Common.ToDouble(dt.Rows[j]["isPaidOrderCount"])) * 100);//第二次及以上支付比例
                        }
                        newDr[11] = Common.ToDouble(dt.Rows[j]["payRate"].ToString().Trim('%'));//支付率
                        newDr[12] = Common.ToString(dt.Rows[j]["isPaidOrderAmount"]);//支付金额
                        newDr[13] = Common.ToString(dt.Rows[j]["refundOrderCount"]);//日退单数
                        newDr[14] = Common.ToString(dt.Rows[j]["refundOrderAmount"]);//退款金额
                        status = true;
                        break;
                    }
                }
                if (status == false)
                {
                    //不存在点单数据
                    initTime = Common.ToDateTime(strTime).AddDays(i).ToString("yyyy-MM-dd");
                    newDr[0] = initTime; //日期
                    dtTemporary = ssManager.GetUserStatistics(initTime);//不会count<>1
                    newDr[1] = Common.ToString(dtTemporary.Rows[0]["dayAddUserCount"]);//日新增用户
                    newDr[2] = Common.ToString(dtTemporary.Rows[0]["userCount"]);//APP总激活数
                    newDr[3] = ssManager.GetActiveUserStatistics(initTime);//日活跃数（登录用户）
                    newDr[4] = "0";//订单笔数
                    newDr[5] = "0";//订单金额
                    newDr[6] = "0";//属于第二次及以上下单的数量
                    newDr[7] = "0";//第二次及以上下单比例
                    newDr[8] = "0";//支付笔数
                    newDr[9] = "0";//二次及以上支付
                    newDr[10] = "0";//第二次及以上支付比例
                    newDr[11] = "0";//支付率
                    newDr[12] = "0";//支付金额
                    newDr[13] = "0";//日退单数
                    newDr[14] = "0";//退款金额
                }
                tempTable.Rows.Add(newDr);
            }
            return tempTable;
        }
        #region 后台综合统计注释代码保留
        //public DataTable GetDataStatistics(DateTime strTime, DateTime endTime, int companyId, int shopId, int cityId, string strHour, string endHour, bool flag, bool flagStatus = false)
        //{
        //    StatisticalStatementManager manager = new StatisticalStatementManager();
        //    List<ComprehensiveStatistics> list_one = manager.GetData_One();//统计订单
        //    List<ComprehensiveStatistics> list_two = manager.GetData_Two(strTime, endTime, strHour, endHour);//统计注册量
        //    List<ComprehensiveStatistics> list_three = manager.GetData_Three();//统计登录量
        //    var query1 = (from a in list_one
        //                  join b in list_three on a.orderTime equals b.orderTime into g
        //                  from c in g.DefaultIfEmpty()
        //                  select new ComprehensiveStatistics
        //                  {
        //                      orderTime = a.orderTime,
        //                      orderCount = a.orderCount,
        //                      dayActiveUserCount = c == null ? 0 : c.dayActiveUserCount,
        //                      dayAddUserCount = c == null ? 0 : c.dayAddUserCount,
        //                      isPaidOrderAmount = a.isPaidOrderAmount,
        //                      isPaidOrderCount = a.isPaidOrderCount,
        //                      orderSumAmount = a.orderSumAmount,
        //                      payRate = Common.ToDouble(a.timersPayRate) + "%",
        //                      refundOrderAmount = a.refundOrderAmount,
        //                      refundOrderCount = a.refundOrderCount,
        //                      timersOrderRate = Common.ToDouble(a.timersOrderRate) + "%",
        //                      timersPayCount = a.timersPayCount,
        //                      timersPayRate = Common.ToDouble(a.timersPayRate) + "%",
        //                      timesOrderQuantity = a.timesOrderQuantity,
        //                      userCount = c == null ? 0 : c.userCount
        //                  }).ToList();

        //    var query2 = (from a in list_three
        //                  join b in list_one on a.orderTime equals b.orderTime into g
        //                  from c in g.DefaultIfEmpty()
        //                  select new ComprehensiveStatistics
        //                  {
        //                      orderTime = a.orderTime,
        //                      dayActiveUserCount = a.dayActiveUserCount,
        //                      orderCount = c == null ? 0 : c.orderCount,
        //                      dayAddUserCount = a.dayAddUserCount,
        //                      isPaidOrderAmount = c == null ? 0 : c.isPaidOrderAmount,
        //                      isPaidOrderCount = c == null ? 0 : c.isPaidOrderCount,
        //                      orderSumAmount = c == null ? 0 : c.orderSumAmount,
        //                      payRate = c == null ? "0" : c.payRate,
        //                      userCount = a.userCount,
        //                      timesOrderQuantity = c == null ? 0 : c.timesOrderQuantity,
        //                      timersPayRate = c == null ? "0" : c.timersPayRate,
        //                      timersPayCount = c == null ? 0 : c.timersPayCount
        //                  }).ToList();

        //    var query = query1.Union(query2, new ComprehensiveStatisticsEqualityComparer()).OrderBy(a => a.orderTime).ToList();
        //    var query3 = (from a in query
        //                  join b in list_two on a.orderTime equals b.orderTime into g
        //                  from c in g.DefaultIfEmpty()
        //                  select new ComprehensiveStatistics
        //                  {
        //                      orderTime = a.orderTime,
        //                      orderCount = a.orderCount,
        //                      dayActiveUserCount = a.dayActiveUserCount,
        //                      dayAddUserCount = c == null ? 0 : c.dayAddUserCount,
        //                      isPaidOrderAmount = a.isPaidOrderAmount,
        //                      isPaidOrderCount = a.isPaidOrderCount,
        //                      orderSumAmount = a.orderSumAmount,
        //                      payRate = a.payRate,
        //                      refundOrderAmount = a.refundOrderAmount,
        //                      refundOrderCount = a.refundOrderCount,
        //                      timersOrderRate = a.timersOrderRate,
        //                      timersPayCount = a.timersPayCount,
        //                      timersPayRate = a.timersPayRate,
        //                      timesOrderQuantity = a.timesOrderQuantity,
        //                      userCount = c == null ? 0 : c.userCount
        //                  }).ToList();

        //    var query4 = (from a in list_two
        //                  join b in query on a.orderTime equals b.orderTime into g
        //                  from c in g.DefaultIfEmpty()
        //                  select new ComprehensiveStatistics
        //                  {
        //                      orderTime = a.orderTime,
        //                      dayActiveUserCount = c == null ? 0 : c.dayActiveUserCount,
        //                      orderCount = c == null ? 0 : c.orderCount,
        //                      dayAddUserCount = a.dayAddUserCount,
        //                      isPaidOrderAmount = c == null ? 0 : c.isPaidOrderAmount,
        //                      isPaidOrderCount = c == null ? 0 : c.isPaidOrderCount,
        //                      orderSumAmount = c == null ? 0 : c.orderSumAmount,
        //                      payRate = c == null ? "0" : c.payRate,
        //                      userCount = a.userCount,
        //                      timesOrderQuantity = c == null ? 0 : c.timesOrderQuantity,
        //                      timersPayRate = c == null ? "0" : c.timersPayRate,
        //                      timersPayCount = c == null ? 0 : c.timersPayCount
        //                  }).ToList();
        //    var data = query3.Union(query4, new ComprehensiveStatisticsEqualityComparer()).OrderBy(a => a.orderTime).ToList();
        //    return ToDataTable<ComprehensiveStatistics>(data);
        //}
        //public DataTable ToDataTable<T>(IEnumerable<T> collection)
        //{
        //    var props = typeof(T).GetProperties();
        //    var dt = new DataTable();
        //    dt.Columns.AddRange(props.Select(p => new DataColumn(p.Name, p.PropertyType)).ToArray());
        //    if (collection.Count() > 0)
        //    {
        //        for (int i = 0; i < collection.Count(); i++)
        //        {
        //            ArrayList tempList = new ArrayList();
        //            foreach (PropertyInfo pi in props)
        //            {
        //                object obj = pi.GetValue(collection.ElementAt(i), null);
        //                tempList.Add(obj);
        //            }
        //            object[] array = tempList.ToArray();
        //            dt.LoadDataRow(array, true);
        //        }
        //    }
        //    return dt;
        //}
        //public class ComprehensiveStatisticsEqualityComparer : IEqualityComparer<ComprehensiveStatistics>
        //{
        //    public bool Equals(ComprehensiveStatistics x, ComprehensiveStatistics y)
        //    {
        //        return x.Equals(y);
        //    }
        //    public int GetHashCode(ComprehensiveStatistics obj)
        //    {
        //        return obj.GetHashCode();
        //    }
        //} 
        #endregion
        public DataTable ComprehensiveStatisticalQuery(DateTime strTime, DateTime endTime, int company, int shopId, int cityId)
        {
            return ssManager.ComprehensiveStatisticalQuery(strTime, endTime, company, shopId, cityId);
        }
        public DataTable GetActityUser(DateTime strTime, DateTime endTime)
        {
            return ssManager.GetActityUser(strTime, endTime);
        }
        public DataTable ComprehensiveStatisticalQueryDetail(string strTime, string endTime, int company, int shopId, int cityId)
        {
            DataTable dt = ssManager.ComprehensiveStatisticalQueryDetail(strTime, endTime, company, shopId, cityId);
            if (dt.Rows.Count == 1)
            {
                dt.Rows[0]["timersOrderRate"] = Common.ToInt32(dt.Rows[0]["timesOrderQuantity"]) == 0 ? "0" : ((Common.ToDouble(dt.Rows[0]["timesOrderQuantity"]) * 100) / Common.ToInt32(dt.Rows[0]["orderCount"])).ToString("0.00") + "%";
                dt.Rows[0]["timersPayRate"] = Common.ToInt32(dt.Rows[0]["timersPayCount"]) == 0 ? "0" : ((Common.ToDouble(dt.Rows[0]["timersPayCount"]) * 100) / Common.ToInt32(dt.Rows[0]["isPaidOrderCount"])).ToString("0.00") + "%";
                dt.Rows[0]["payRate"] = Common.ToDouble(dt.Rows[0]["payRate"]) == 0 ? "0" : Common.ToString(dt.Rows[0]["payRate"]) + "%";
                return dt;
            }
            return null;
        }
        /// <summary>
        /// 查询当前时间段累计活跃用户数量
        /// </summary>
        /// <param name="strTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public int GetActityUserCount(DateTime strTime, DateTime endTime)
        {
            return ssManager.GetActityUserCount(strTime, endTime);
        }

        public DataTable GetCustomerExpenseRecord(string phone, string userName)
        {
            return ssManager.GetCustomerExpenseRecord(phone, userName);
        }
        public double[] GetGetCustomerMoneyRecord(double initMoney, string initDate, string strTime, string endTime)
        {
            return ssManager.GetGetCustomerMoneyRecord(initMoney, initDate, strTime, endTime);
        }

        /// <summary>
        ///  周，月数据统计
        /// </summary>
        /// <param name="flag"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        public DataTable GetBigData_1(string flag, int year)
        {
            if (flag == "week")
            {
                DataTable initDt = ssManager.GetWeekTempTableBigData(year);
                int maxWeek = initDt == null || initDt.Rows.Count <= 0 ? 0 : Common.ToInt32(initDt.Rows[initDt.Rows.Count - 1]["monthweek"]);//年历史最大周次
                int currectWeek = Common.GetWeekOfYear(DateTime.Now);//当前服务器时间所处周次
                if (currectWeek - 1 == maxWeek)
                {
                    DateTime strTime;//周日开始
                    DateTime endTime;//周六结束
                    Common.GetDaysOfWeeks(year, currectWeek, out  strTime, out  endTime);
                    DataTable currectDt = ssManager.GetWeekCurrectTableBigData(year, strTime, endTime);
                    initDt.Merge(currectDt);
                    return initDt;
                }
                if (currectWeek <= maxWeek)
                {
                    return initDt;
                }
                if (currectWeek - 1 > maxWeek)
                {
                    DateTime strTime;
                    DateTime endTime;
                    Common.GetDaysOfWeeks(year, maxWeek + 1, out  strTime, out  endTime);
                    if (ssManager.InsertWeekTempTableBigData(year, maxWeek + 1, strTime, endTime))
                    {
                        return GetBigData_1(flag, year);
                    }
                }
                return initDt;
            }
            else
            {
                DataTable initDt = ssManager.GetMonthTempTableBigData(year);
                int maxMonth = initDt == null || initDt.Rows.Count <= 0 ? 0 : Common.ToInt32(initDt.Rows[initDt.Rows.Count - 1]["monthweek"]);//年历史最大月
                int currectMonth = DateTime.Now.Month;//当前服务器时间所处月
                if (currectMonth - 1 == maxMonth)
                {
                    DateTime strTime = Common.ToDateTime(DateTime.Now.AddDays(1 - DateTime.Now.Day).ToString("yyyy-MM-dd") + " 00:00:00");
                    DateTime endTime = Common.ToDateTime(DateTime.Now.AddDays(1 - DateTime.Now.Day).AddMonths(1).AddDays(-1).ToString("yyyy-MM-dd") + " 23:59:59");
                    DataTable currectDt = ssManager.GetMonthCurrectTableBigData(year, strTime, endTime);
                    initDt.Merge(currectDt);
                    return initDt;
                }
                if (currectMonth <= maxMonth)
                {
                    return initDt;
                }
                if (currectMonth - 1 > maxMonth)
                {
                    int month = maxMonth + 1;
                    DateTime strTime = Common.ToDateTime(year + "/" + month + "/1" + " 00:00:00");
                    DateTime endTime = Common.ToDateTime(strTime.AddDays(1 - strTime.Day).AddMonths(1).AddDays(-1).ToString("yyyy-MM-dd") + " 23:59:59");
                    if (ssManager.InsertMonthTempTableBigData(year, maxMonth + 1, strTime, endTime))
                    {
                        return GetBigData_1(flag, year);
                    }
                }
                return initDt;
            }
        }

        public DataTable GetOrderStatusDetail(string startTime, string endTime, int cityId, int companyId, int shopId)
        {
            return ssManager.SelectOrderStatusDetail(startTime, endTime, cityId, companyId, shopId);
        }
    }
}
