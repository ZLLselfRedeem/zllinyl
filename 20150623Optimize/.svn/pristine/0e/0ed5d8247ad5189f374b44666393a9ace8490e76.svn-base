using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Data;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL;
using System.IO;
using System.Collections;
using VAEncryptDecrypt;
using System.Configuration;
using System.Net;
using Microsoft.VisualBasic;
using System.Net.Mail;
using System.Transactions;
using System.Threading;
using VA.AllNotifications;
using System.ComponentModel;
using System.Reflection;
using System.Drawing;
using System.Drawing.Imaging;
using ICSharpCode.SharpZipLib.Zip;
using System.Web;
using System.Globalization;
using VA.Cache.HttpRuntime;
using VA.CacheLogic.OrderClient;
using VA.Cache.Distributed;
using System.Globalization;
using VA.CacheLogic;
using Org.BouncyCastle.Utilities.Encoders;

//
//  Copyright 2011 View Alloc inc. All rights reserved.
//  Created by Jason Xiao on 2012-04-10.
//
namespace VAGastronomistMobileApp.WebPageDll
{
    public class Common
    {
        private readonly static CustomerManager customerMan = new CustomerManager();
        private static Object loginRewardLock = new Object();
        private static Object registerRewardLock = new Object();
        private static Object verifyCouponRewardLock = new Object();
        private static Object verifyMobileRewardLock = new Object();
        private static Object inviteCustomerRewardLock = new Object();
        private static Object getVerificationCodeLock = new Object();
        /// <summary>
        /// 根据datatable获取子datatable
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static DataTable GetPageDataTable(DataTable dt, int str, int end)
        {
            int tableCount = dt.Rows.Count;
            DataTable dt_page = dt.Copy();//分页的DataTable
            dt_page.Clear();
            if (end > tableCount)
                end = tableCount;
            for (int i = str; i < end; i++)
            {
                DataRow dataRow = dt_page.NewRow();
                DataRow dr = dt.Rows[i];
                foreach (DataColumn column in dt.Columns)
                {
                    dataRow[column.ColumnName] = dr[column.ColumnName];
                }
                dt_page.Rows.Add(dataRow);
            }
            return dt_page;
        }
        /// <summary>
        /// List 转json"
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string ConvertListToJson<T>(T data)
        {

            try
            {

                System.Runtime.Serialization.Json.DataContractJsonSerializer serializer = new System.Runtime.Serialization.Json.DataContractJsonSerializer(data.GetType());

                using (MemoryStream ms = new MemoryStream())
                {

                    serializer.WriteObject(ms, data);

                    return Encoding.UTF8.GetString(ms.ToArray());

                }

            }

            catch
            {

                return null;

            }

        }

        /// <summary>    
        /// 对象转换为Json字符串    
        /// </summary>    
        /// <param name="jsonObject">对象</param>    
        /// <returns>Json字符串</returns>    
        public static string ConvertToJson(object jsonObject)
        {
            string jsonString = "{";
            PropertyInfo[] propertyInfo = jsonObject.GetType().GetProperties();
            for (int i = 0; i < propertyInfo.Length; i++)
            {
                object objectValue = propertyInfo[i].GetGetMethod().Invoke(jsonObject, null);
                string value = string.Empty;
                if (objectValue is DateTime || objectValue is Guid || objectValue is TimeSpan)
                {
                    value = "'" + objectValue.ToString() + "'";
                }
                else if (objectValue is string)
                {
                    value = "'" + ConvertToJson(objectValue.ToString()) + "'";
                }
                else if (objectValue is IEnumerable)
                {
                    value = ConvertToJson((IEnumerable)objectValue);
                }
                else
                {
                    value = ConvertToJson(objectValue.ToString());
                }
                jsonString += "\"" + ConvertToJson(propertyInfo[i].Name) + "\":" + value + ",";
            }
            jsonString.Remove(jsonString.Length - 1, jsonString.Length);
            return jsonString + "}";
        }
        /// <summary>
        /// 转换对象为JSON格式数据
        /// </summary>
        /// <typeparam name="T">类</typeparam>
        /// <param name="obj">对象</param>
        /// <returns>字符格式的JSON数据</returns>
        public static string GetJSON<T>(object obj)
        {
            string result = String.Empty;
            try
            {
                System.Runtime.Serialization.Json.DataContractJsonSerializer serializer =
                new System.Runtime.Serialization.Json.DataContractJsonSerializer(typeof(T));
                using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
                {
                    serializer.WriteObject(ms, obj);
                    result = System.Text.Encoding.UTF8.GetString(ms.ToArray());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        /// <summary>   
        /// 根据Json返回DateTable,JSON数据格式如:   
        /// {table:[{column1:1,column2:2,column3:3},{column1:1,column2:2,column3:3}]}   
        /// </summary>   
        /// <param name="strJson">Json字符串</param>   
        /// <returns></returns>   
        public static DataTable JsonToDataTable(string strJson)
        {
            //取出表名   
            var rg = new Regex(@"(?<={)[^:]+(?=:\[)", RegexOptions.IgnoreCase);
            string strName = rg.Match(strJson).Value;
            DataTable tb = null;
            //去除表名   
            strJson = strJson.Substring(strJson.IndexOf("[") + 1);
            strJson = strJson.Substring(0, strJson.IndexOf("]"));

            //获取数据   
            rg = new Regex(@"(?<={)[^}]+(?=})");
            MatchCollection mc = rg.Matches(strJson);
            for (int i = 0; i < mc.Count; i++)
            {
                string strRow = mc[i].Value;
                string[] strRows = strRow.Split(',');

                //创建表   
                if (tb == null)
                {
                    tb = new DataTable();
                    tb.TableName = strName;
                    foreach (string str in strRows)
                    {
                        var dc = new DataColumn();
                        string[] strCell = str.Split(':');
                        dc.ColumnName = strCell[0];
                        tb.Columns.Add(dc);
                    }
                    tb.AcceptChanges();
                }

                //增加内容   
                DataRow dr = tb.NewRow();
                for (int r = 0; r < strRows.Length; r++)
                {
                    dr[r] = strRows[r].Split(':')[1].Trim().Replace("，", ",").Replace("：", ":").Replace("\"", "");
                }
                tb.Rows.Add(dr);
                tb.AcceptChanges();
            }

            return tb;
        }
        /// <summary>
        /// DataTable 转 Json
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ConvertDateTableToJson(DataTable dt)
        {
            /**/
            /**/
            /**/
            /* /****************************************************************************
          * Without goingin to the depth of the functioning of this Method, i will try to give an overview
          * As soon as this method gets a DataTable it starts to convert it into JSON String,
          * it takes each row and in each row it grabs the cell name and its data.
          * This kind of JSON is very usefull when developer have to have Column name of the .
          * Values Can be Access on clien in this way. OBJ.HEAD[0].<ColumnName>
          * NOTE: One negative point. by this method user will not be able to call any cell by its index.
         * *************************************************************************/
            StringBuilder JsonString = new StringBuilder();
            //Exception Handling        
            if (dt != null && dt.Rows.Count > 0)
            {
                JsonString.Append("{ ");
                JsonString.Append("\"TableJson\":[ ");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    JsonString.Append("{ ");
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        if (j < dt.Columns.Count - 1)
                        {
                            JsonString.Append("\"" + dt.Columns[j].ColumnName.ToString() + "\":" + "\"" + TransferredStr(dt.Rows[i][j].ToString()) + "\",");
                        }
                        else if (j == dt.Columns.Count - 1)
                        {
                            JsonString.Append("\"" + dt.Columns[j].ColumnName.ToString() + "\":" + "\"" + TransferredStr(dt.Rows[i][j].ToString()) + "\"");
                        }
                    }
                    /**/
                    /**/
                    /**/
                    /*end Of String*/
                    if (i == dt.Rows.Count - 1)
                    {
                        JsonString.Append("} ");
                    }
                    else
                    {
                        JsonString.Append("}, ");
                    }
                }
                JsonString.Append("]}");
                return JsonString.ToString();
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 私有方法，DataTable转义数据调用
        /// </summary>
        /// <param name="str">需要转义的字符串</param>
        /// <returns></returns>
        static string TransferredStr(string str)
        {
            return str.Replace("\\", "\\\\").Replace("\"", "\\\"");
        }
        /// <summary>
        /// 对象转换成double
        /// 保留2位小数
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static double ToDouble(object obj)
        {
            double value = 0;
            try
            {
                value = Convert.ToDouble(obj);
            }
            catch (System.Exception)
            {

            }
            value = double.Parse(value.ToString("N2"));//保留2位小数
            return value;
        }
        /// <summary>
        /// 对象转换成double
        /// 保留5位小数
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static double ToDouble11Digit(object obj)
        {
            double value = 0;
            try
            {
                value = Convert.ToDouble(obj);
            }
            catch (System.Exception)
            {

            }
            value = Math.Round(value, 11);//保留11位小数
            return value;
        }
        /// <summary>
        /// 对象转换成Decimal
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static decimal ToDecimal(object obj)
        {
            decimal value = 0;
            try
            {
                value = Convert.ToDecimal(obj);
            }
            catch (System.Exception)
            {

            }
            value = Math.Round(value, 2);//保留2位小数
            return value;
        }
        /// <summary>
        /// wangcheng 2013/8/21
        /// 对象转化为只有中文，英文和数字的字符串（去除所有的非次三类的字符）
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToClearSpecialCharString(object obj)
        {
            string value = "";
            try
            {
                value = Regex.Replace(Common.ToString(obj), @"[^a-zA-Z\d\u4e00-\u9fa5]", "");
            }
            catch (System.Exception)
            {

            }
            return value;
        }
        /// <summary>
        /// 对象转换成Int
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int ToInt32(object obj)
        {
            int value = 0;
            try
            {
                value = Convert.ToInt32(obj);
            }
            catch (System.Exception)
            {

            }
            return value;
        }

        /// <summary>
        /// 对象转换成byte
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static byte ToByte(object obj)
        {
            byte value = 0;
            try
            {
                value = Convert.ToByte(obj);
            }
            catch (System.Exception)
            {

            }
            return value;
        }
        /// <summary>
        /// 对象转换成Int64
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static long ToInt64(object obj)
        {
            long value = 0;
            try
            {
                value = Convert.ToInt64(obj);
            }
            catch (System.Exception)
            {

            }
            return value;
        }
        /// <summary>
        /// 对象转换成String
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToString(object obj)
        {
            return (obj ?? "").ToString();
        }
        /// <summary>
        /// 2011年12月20日
        /// tdq
        /// 对象转换成bool
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool ToBool(object obj)
        {
            bool value = false;
            try
            {
                value = bool.Parse(obj.ToString());
            }
            catch (System.Exception)
            {

            }
            return value;
        }
        /// <summary>
        /// 对象转换成DateTime
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(object obj)
        {
            DateTime value = new DateTime();
            //try
            //{
            //    value = Convert.ToDateTime(obj);
            //}
            //catch (System.Exception)
            //{

            //}
            //return value;
            return ToDateTime(obj, value);
        }

        public static DateTime ToDateTime(object obj, DateTime defaultValue)
        {
            DateTime value = defaultValue;
            try
            {
                value = Convert.ToDateTime(obj);
            }
            catch (System.Exception)
            {

            }
            return value;
        }

        /// <summary>
        /// 创建文件夹
        /// </summary>
        /// <param name="Path"></param>
        public static bool FolderCreate(string Path)
        {
            bool result = false;
            // 判断目标目录是否存在如果不存在则新建之
            if (!Directory.Exists(Path))
            {
                try
                {
                    DirectoryInfo directoryInfo = Directory.CreateDirectory(Path);
                    if (directoryInfo != null)
                    {
                        result = true;
                    }
                }
                catch
                {

                }
            }
            return result;
        }
        /// <summary>
        /// 判断文件是否存在
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static bool FileExist(string filePath)
        {
            FileInfo file = new FileInfo(filePath);
            if (file.Exists)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 验证图片后缀是不是合法
        /// </summary>
        /// <param name="extension"></param>
        /// <returns></returns>
        public static bool ValidateExtension(string extension)
        {
            string validExtension = ConfigurationManager.AppSettings["extension"].ToString();
            string[] validExtensions = validExtension.Split('|');
            bool isImage = false;
            for (int j = 0; j < validExtensions.Length; j++)
            {
                if (extension.ToLower() == validExtensions[j])
                {
                    isImage = true;
                }
            }
            return isImage;
        }
        /// <summary>
        /// 将传入的时间转化为与1970-1-1的秒的差值
        /// </summary>
        /// <param name="inputDateTime"></param>
        /// <returns></returns>
        public static double ToSecondFrom1970(DateTime inputDateTime)
        {
            DateTime beginTime = new DateTime(1970, 1, 1).ToLocalTime();//new DateTime取出来的时间是UTC的，转换为当前时区
            long elapsedTicks = inputDateTime.Ticks - beginTime.Ticks;
            TimeSpan elapsedSpan = new TimeSpan(elapsedTicks);
            return Math.Round(elapsedSpan.TotalSeconds, 0);
        }
        /// <summary>
        /// 将传入的秒数转化为Datetime
        /// </summary>
        /// <param name="inputDateTime"></param>
        /// <returns></returns>
        public static DateTime ToDateTimeFrom1970(double sec)
        {
            DateTime dateTime = new DateTime(1970, 1, 1).ToLocalTime();//new DateTime取出来的时间是UTC的，转换为当前时区
            dateTime.AddSeconds(sec);
            return dateTime;
        }
        /// <summary>
        /// 检查Cookie是否合法，消息类型是否匹配20140313
        /// </summary>
        /// <param name="cookie"></param>
        /// <param name="type"></param>
        /// <param name="typeToCompare"></param>
        /// <param name="readCache">标记是否读取缓存数据，默认true读取</param>
        /// <returns></returns>
        public static CheckCookieAndMsgtypeInfo CheckCookieAndMsgtype(string cookie, string uuid, int type, int typeToCompare, bool readCache = true)
        {
            CheckCookieAndMsgtypeInfo checkCookieAndMsgtypeInfo = new CheckCookieAndMsgtypeInfo();
            if (type != typeToCompare)
            {
                checkCookieAndMsgtypeInfo.result = VAResult.VA_FAILED_TYPE_ERROR;
                return checkCookieAndMsgtypeInfo;
            }
            if (string.IsNullOrEmpty(cookie))
            {
                checkCookieAndMsgtypeInfo.result = VAResult.VA_FAILED_COOKIE_NULL;
                return checkCookieAndMsgtypeInfo;
            }
            DataTable dtCustomer = null;
            if (readCache == false)//不读取缓存信息
            {
                dtCustomer = customerMan.SelectCustomer(cookie, uuid);
                if (dtCustomer.Rows.Count == 1)
                {
                    checkCookieAndMsgtypeInfo.result = VAResult.VA_OK;
                    checkCookieAndMsgtypeInfo.dtCustomer = dtCustomer;
                }
                else
                {
                    checkCookieAndMsgtypeInfo.result = VAResult.VA_FAILED_COOKIE_NOT_FOUND;
                }
            }
            else//读取缓存信息
            {
                DataTable cache = MemcachedHelper.GetMemcached<DataTable>("CheckCookieAndMsgtypeInfo_" + cookie);
                if (cache == null || cache.Rows.Count != 1)//缓存不存在
                {
                    dtCustomer = customerMan.SelectCustomer(cookie, uuid);
                    MemcachedHelper.AddMemcached("CheckCookieAndMsgtypeInfo_" + cookie, dtCustomer, 30);
                }
                else//缓存存在
                {
                    dtCustomer = (DataTable)cache;
                }
                if (dtCustomer.Rows.Count == 1)
                {
                    checkCookieAndMsgtypeInfo.result = VAResult.VA_OK;
                    checkCookieAndMsgtypeInfo.dtCustomer = dtCustomer;
                }
                else
                {
                    checkCookieAndMsgtypeInfo.result = VAResult.VA_FAILED_COOKIE_NOT_FOUND;
                }
            }
            return checkCookieAndMsgtypeInfo;
        }
        /// <summary>
        /// 检查Cookie是否合法，消息类型是否匹配
        /// </summary>
        /// <param name="cookie"></param>
        /// <param name="type"></param>
        /// <param name="typeToCompare"></param>
        /// <returns></returns>
        public static CheckCookieAndMsgForZZZ CheckCookieAndMsgForZZZ(string cookie, int type, int typeToCompare, int shopId = 0)
        {
            CheckCookieAndMsgForZZZ checkCookieAndMsgForZZZ = new CheckCookieAndMsgForZZZ();
            if (type == typeToCompare)
            {
                if (string.IsNullOrEmpty(cookie))
                {
                    checkCookieAndMsgForZZZ.result = VAResult.VA_FAILED_COOKIE_NULL;
                }
                else
                {
                    AuthorityManager authorityMan = new AuthorityManager();
                    DataTable dtEmployee = null;
                    DataTable cache = MemcachedHelper.GetMemcached<DataTable>("CheckCookieAndMsgForZZZ_" + cookie);
                    if (cache == null || cache.Rows.Count != 1)
                    {
                        dtEmployee = authorityMan.SelectEmployeeByCookie(cookie);
                        MemcachedHelper.AddMemcached("CheckCookieAndMsgForZZZ_" + cookie, 60 * 5);//五分钟
                    }
                    else
                    {
                        dtEmployee = (DataTable)cache;
                    }
                    if (dtEmployee.Rows.Count == 1)
                    {
                        //CacheHelper.AddCache("CheckCookieAndMsgForZZZ_" + cookie, dtEmployee, 300);//五分钟
                        if (shopId > 0)//Edit at 2014-3-27
                        {
                            int employeeId = Common.ToInt32(dtEmployee.Rows[0]["EmployeeID"]);
                            //查询是否具备查看所有上线门店的特殊权限
                            DataTable dtQueryOnlineCompamnyInfo = new RoleOperate().QuerySpecialAuthorityInfoByEmployeeID(employeeId, (int)VASpecialAuthority.CHECK_PREORDER_ONLINE_COMPANY);
                            if (dtQueryOnlineCompamnyInfo.Rows.Count > 0)
                            {
                                ShopInfo shop = new ShopOperate().QueryShop(shopId);//只需判断当前门店是否上线即可
                                if (shop == null)
                                {
                                    checkCookieAndMsgForZZZ.result = VAResult.VA_FAILED_EMPLOYEE_NOT_MATCH_SHOP;
                                    return checkCookieAndMsgForZZZ;
                                }
                                if (shop.isHandle == (int)VAShopHandleStatus.SHOP_Pass)
                                {
                                    checkCookieAndMsgForZZZ.result = VAResult.VA_OK;
                                    checkCookieAndMsgForZZZ.dtEmployee = dtEmployee;
                                }
                                else
                                {
                                    checkCookieAndMsgForZZZ.result = VAResult.VA_FAILED_EMPLOYEE_NOT_MATCH_SHOP;
                                }
                            }
                            else
                            {
                                DataTable dtEmployeeConnShop = authorityMan.SelectEmployeeShop(employeeId, shopId);
                                if (dtEmployeeConnShop.Rows.Count > 0)
                                {
                                    checkCookieAndMsgForZZZ.result = VAResult.VA_OK;
                                    checkCookieAndMsgForZZZ.dtEmployee = dtEmployee;
                                }
                                else
                                {
                                    checkCookieAndMsgForZZZ.result = VAResult.VA_FAILED_EMPLOYEE_NOT_MATCH_SHOP;
                                }
                            }
                        }
                        else
                        {
                            checkCookieAndMsgForZZZ.result = VAResult.VA_OK;
                            checkCookieAndMsgForZZZ.dtEmployee = dtEmployee;
                        }
                    }
                    else
                    {
                        checkCookieAndMsgForZZZ.result = VAResult.VA_FAILED_COOKIE_NOT_FOUND;
                    }
                }
            }
            else
            {
                checkCookieAndMsgForZZZ.result = VAResult.VA_FAILED_TYPE_ERROR;
            }
            return checkCookieAndMsgForZZZ;
        }
        /// <summary>
        /// 激励模块20140313
        /// </summary>
        /// <param name="encourageDetail"></param>
        public static void Encourage(object encourageDetail)
        {
            try
            {
                //需要使用的公用SQL操作类对象
                VAEncourageDetail encourage = (VAEncourageDetail)encourageDetail;
                CustomerManager customerMan = new CustomerManager();
                CouponOperate couponMan = new CouponOperate();
                SystemConfigManager systemConfigMan = new SystemConfigManager();
                //Money19dianDetailManager money19dianDetailMan = new Money19dianDetailManager();
                NotificationRecordManager notificationRecordMan = new NotificationRecordManager();
                switch (encourage.messageType)
                {
                    #region 每日登录奖励
                    case VAMessageType.CLIENT_COOKIE_LOGIN_REQUEST:
                    case VAMessageType.CLIENT_MOBILE_LOGIN_REQUEST:
                    case VAMessageType.CLIENT_OPEN_ID_LOGIN_REQUEST:
                        {
                            lock (loginRewardLock)
                            {//确保此段函数段唯一，以免出现多次奖励
                                DataTable dtCustomer = customerMan.SelectCustomer(encourage.customerId);
                                if (dtCustomer.Rows.Count == 1)
                                {
                                    DateTime currentTime = System.DateTime.Now;
                                    DateTime registerTime = Common.ToDateTime(dtCustomer.Rows[0]["RegisterDate"]);
                                    DateTime loginRewardTime = Common.ToDateTime(dtCustomer.Rows[0]["loginRewardTime"]);
                                    if (loginRewardTime.Date.CompareTo(currentTime.Date) < 0 && registerTime.Date.CompareTo(currentTime.Date) < 0)
                                    {//上次获得登录奖励的日期小于当前日期且注册日期小于当前日期（注册当天登录不奖励）
                                        DataTable dtEncourage = systemConfigMan.SelectEncourageConfig();
                                        DataView dvEncourage = dtEncourage.DefaultView;
                                        dvEncourage.RowFilter = "configName = '" + (int)VAMessageType.CLIENT_COOKIE_LOGIN_REQUEST + "'";
                                        if (dvEncourage.Count == 1)
                                        {
                                            #region 设置登录奖励额度以及推送内容
                                            string encourageValuestring = string.Empty;
                                            string[] encourageValueList;
                                            double encourageMinValueForEverydayReward = 0.1;
                                            double encourageMaxValueForEverydayReward = 1;
                                            double encourageMinValueForContinuousReward = 1;
                                            double encourageMaxValueForContinuousReward = 2;
                                            string[] notificationMessageList;
                                            string notificationMessageString = string.Empty;
                                            string notificationMessageForEverydayLogin = string.Empty;
                                            string notificationMessageForContinuousLogin = string.Empty;
                                            try
                                            {
                                                notificationMessageString = Common.ToString(dvEncourage[0]["configMessage"]);
                                                notificationMessageList = notificationMessageString.Split(new string[1] { "*" }, StringSplitOptions.RemoveEmptyEntries);
                                                encourageValuestring = Common.ToString(dvEncourage[0]["configContent"]);
                                                encourageValueList = encourageValuestring.Split(new string[1] { "*" }, StringSplitOptions.RemoveEmptyEntries);

                                                if (encourageValueList.Length == 2)
                                                {
                                                    string[] encourageValueForEverydayReward = encourageValueList[0].Split(new string[1] { "-" }, StringSplitOptions.RemoveEmptyEntries);
                                                    string[] encourageValueForContinuousReward = encourageValueList[1].Split(new string[1] { "-" }, StringSplitOptions.RemoveEmptyEntries);
                                                    if (encourageValueForEverydayReward.Length == 2)
                                                    {
                                                        encourageMinValueForEverydayReward = Common.ToDouble(encourageValueForEverydayReward[0]);
                                                        encourageMaxValueForEverydayReward = Common.ToDouble(encourageValueForEverydayReward[1]);
                                                    }
                                                    if (encourageValueForContinuousReward.Length == 2)
                                                    {
                                                        encourageMinValueForContinuousReward = Common.ToDouble(encourageValueForContinuousReward[0]);
                                                        encourageMaxValueForContinuousReward = Common.ToDouble(encourageValueForContinuousReward[1]);
                                                    }
                                                }
                                                if (notificationMessageList.Length == 2)
                                                {
                                                    notificationMessageForEverydayLogin = notificationMessageList[0];
                                                    notificationMessageForContinuousLogin = notificationMessageList[1];
                                                }
                                            }
                                            catch (System.Exception)
                                            {
                                                encourageMinValueForEverydayReward = 0.1;
                                                encourageMaxValueForEverydayReward = 1;
                                                encourageMinValueForContinuousReward = 1;
                                                encourageMaxValueForContinuousReward = 2;
                                                notificationMessageForEverydayLogin = "恭喜你获得每日登录奖励";
                                                notificationMessageForContinuousLogin = "你已经连续登录多天，恭喜你获得连续登录奖励";
                                            }
                                            #endregion
                                            double encourageValue = 0;
                                            string notificationMessage = string.Empty;
                                            bool clearContinuousLoginNumber = false;
                                            bool isContinuousLogin = false;//是否连续登录标志位
                                            Random random = new Random(~unchecked((int)DateTime.Now.Ticks));
                                            if (loginRewardTime.Date.Equals(currentTime.AddDays(-1).Date))
                                            {
                                                isContinuousLogin = true;
                                                //判断该用户是否满足连续登录奖励
                                                DataTable dtSystem = systemConfigMan.SelectSystemConfig(string.Empty, string.Empty);
                                                DataView dvSystem = dtSystem.DefaultView;
                                                dvSystem.RowFilter = "configName = 'continuousLoginRewardNumber'";
                                                int continuousLoginRewardNumber = 6;//设置默认值为6天，避免读取配置失败时的错误
                                                if (dvSystem.Count == 1)
                                                {
                                                    int continuousLoginRewardNumberFromConfig = Common.ToInt32(dvSystem[0]["configContent"]);
                                                    if (continuousLoginRewardNumberFromConfig > 0)
                                                    {
                                                        continuousLoginRewardNumber = continuousLoginRewardNumberFromConfig;
                                                    }
                                                }
                                                int currentcontinuousLoginNumber = Common.ToInt32(dtCustomer.Rows[0]["continuousLoginNumber"]);

                                                if (currentcontinuousLoginNumber + 1 == continuousLoginRewardNumber)
                                                {//如果该用户的当前连续登录天数+1等于抽奖的天数,执行连续登录奖励，并将连续天数清零
                                                    encourageValue = encourageMinValueForContinuousReward +
                                                        (encourageMaxValueForContinuousReward - encourageMinValueForContinuousReward) * random.NextDouble();
                                                    encourageValue = Math.Round(encourageValue, 1);
                                                    clearContinuousLoginNumber = true;
                                                    notificationMessage = notificationMessageForContinuousLogin.Replace("{0}", Common.ToString(continuousLoginRewardNumber));
                                                    notificationMessage = notificationMessage.Replace("{1}", Common.ToString(encourageValue));
                                                }
                                                else
                                                {//执行每日登录奖励，并判断是否是连续登录
                                                    encourageValue = encourageMinValueForEverydayReward +
                                                        (encourageMaxValueForEverydayReward - encourageMinValueForEverydayReward) * random.NextDouble();
                                                    encourageValue = Math.Round(encourageValue, 1);
                                                    if (!isContinuousLogin)
                                                    {
                                                        clearContinuousLoginNumber = true;
                                                    }
                                                    notificationMessage = notificationMessageForEverydayLogin.Replace("{0}", Common.ToString(encourageValue));
                                                }
                                            }
                                            else
                                            {//执行每日登录奖励
                                                encourageValue = encourageMinValueForEverydayReward +
                                                    (encourageMaxValueForEverydayReward - encourageMinValueForEverydayReward) * random.NextDouble();
                                                encourageValue = Math.Round(encourageValue, 1);
                                                clearContinuousLoginNumber = true;
                                                notificationMessage = notificationMessageForEverydayLogin.Replace("{0}", Common.ToString(encourageValue));
                                            }
                                            if (encourageValue > 0)
                                            {
                                                double remainMoney = SybMoneyCustomerOperate.GetCustomerRemainMoney(encourage.customerId);

                                                using (TransactionScope scope = new TransactionScope())
                                                {
                                                    //更新用户连续登录天数
                                                    bool flagUpdateCustomerContinuousLoginNumber = customerMan.UpdateCustomerContinuousLoginNumber(encourage.customerId, clearContinuousLoginNumber);
                                                    //更新用户登录奖励时间
                                                    bool flagUpdateLoginRewardTime = customerMan.UpdateLoginRewardTime(encourage.customerId);
                                                    //更新用户货币余额
                                                    bool flagUpdateMoney19dianRemained = customerMan.UpdateMoney19dianRemained(encourage.customerId, encourageValue);
                                                    //插入用户货币变动记录
                                                    Money19dianDetail money19dianDetail = new Money19dianDetail()
                                                    {
                                                        customerId = encourage.customerId,
                                                        changeReason = Common.GetEnumDescription(VA19dianMoneyChangeReason.MONEY19DIAN_LOGIN_REWORD),
                                                        changeTime = currentTime,
                                                        changeValue = encourageValue,
                                                        inoutcomeType = Convert.ToInt32(InoutcomeType.IN),
                                                        accountType = Convert.ToInt32(AccountType.USER_LOGIN),
                                                        accountTypeConnId = "", //待处理 需要获取
                                                        companyId = 0,
                                                        shopId = 0,
                                                        flowNumber = SybMoneyOperate.CreateCustomerFlowNumber(encourage.customerId),
                                                        remainMoney = encourageValue + remainMoney
                                                    };
                                                    long flagInsertMoney19dianDetail = Money19dianDetailManager.InsertMoney19dianDetail(money19dianDetail);
                                                    if (flagUpdateLoginRewardTime && flagUpdateMoney19dianRemained && flagUpdateCustomerContinuousLoginNumber
                                                        && flagInsertMoney19dianDetail > 0)
                                                    {
                                                        scope.Complete();
                                                    }
                                                }
                                                if (!string.IsNullOrEmpty(encourage.pushToken))
                                                {
                                                    //插入推送记录
                                                    NotificationRecord notificationRecord = new NotificationRecord();
                                                    notificationRecord.addTime = currentTime;
                                                    notificationRecord.appType = (int)encourage.appType;
                                                    notificationRecord.isLocked = false;
                                                    notificationRecord.isSent = false;
                                                    notificationRecord.pushToken = encourage.pushToken;
                                                    notificationRecord.sendCount = 0;

                                                    notificationRecord.message = notificationMessage;
                                                    notificationRecord.customType = (int)VANotificationsCustomType.NOTIFICATIONS_LOGIN_REWORD;
                                                    notificationRecord.customValue = "-999";
                                                    notificationRecordMan.InsertNotificationRecord(notificationRecord);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        break;
                    #endregion

                    #region 注册奖励
                    case VAMessageType.CLIENT_REGISTER_REQUEST:
                        {
                            lock (registerRewardLock)
                            {
                                DataTable dtCustomer = customerMan.SelectCustomer(encourage.customerId);
                                if (dtCustomer.Rows.Count == 1)
                                {
                                    if (Common.ToDateTime(dtCustomer.Rows[0]["registerRewardTime"]) == DateTime.MinValue)
                                    {
                                        DateTime currentTime = System.DateTime.Now;
                                        DataTable dtEncourage = systemConfigMan.SelectEncourageConfig();
                                        DataView dvEncourage = dtEncourage.DefaultView;
                                        dvEncourage.RowFilter = "configName = '" + (int)VAMessageType.CLIENT_REGISTER_REQUEST + "'";
                                        if (dvEncourage.Count == 1)
                                        {
                                            double encourageValue = Common.ToDouble(dvEncourage[0]["configContent"]);
                                            if (encourageValue > 0)
                                            {
                                                double remainMoney = SybMoneyCustomerOperate.GetCustomerRemainMoney(encourage.customerId);

                                                using (TransactionScope scope = new TransactionScope())
                                                {
                                                    bool flagUpdateRegisterRewardTime = customerMan.UpdateRegisterRewardTime(encourage.customerId);
                                                    //更新用户货币余额
                                                    bool flagUpdateMoney19dianRemained = customerMan.UpdateMoney19dianRemained(encourage.customerId, encourageValue);
                                                    //插入用户货币变动记录
                                                    Money19dianDetail money19dianDetail = new Money19dianDetail()
                                                    {
                                                        customerId = encourage.customerId,
                                                        changeReason = Common.GetEnumDescription(VA19dianMoneyChangeReason.MONEY19DIAN_REGISTER_REWORD),
                                                        changeTime = currentTime,
                                                        changeValue = encourageValue,
                                                        inoutcomeType = Convert.ToInt32(InoutcomeType.IN),
                                                        accountType = Convert.ToInt32(AccountType.USER_REGISTER),
                                                        accountTypeConnId = "", //待处理 需要获取
                                                        companyId = 0,
                                                        shopId = 0,
                                                        flowNumber = SybMoneyOperate.CreateCustomerFlowNumber(encourage.customerId),
                                                        remainMoney = encourageValue + remainMoney
                                                    };
                                                    long flagInsertMoney19dianDetail = Money19dianDetailManager.InsertMoney19dianDetail(money19dianDetail);
                                                    if (flagUpdateRegisterRewardTime && flagUpdateMoney19dianRemained
                                                        && flagInsertMoney19dianDetail > 0)
                                                    {
                                                        scope.Complete();
                                                    }
                                                }
                                                if (!string.IsNullOrEmpty(encourage.pushToken))
                                                {
                                                    //插入推送记录
                                                    NotificationRecord notificationRecord = new NotificationRecord();
                                                    notificationRecord.addTime = currentTime;
                                                    notificationRecord.appType = (int)encourage.appType;
                                                    notificationRecord.isLocked = false;
                                                    notificationRecord.isSent = false;
                                                    notificationRecord.pushToken = encourage.pushToken;
                                                    notificationRecord.sendCount = 0;
                                                    string notificationMessage = Common.ToString(dvEncourage[0]["configMessage"]);
                                                    notificationRecord.message = notificationMessage.Replace("{0}", Common.ToString(encourageValue));
                                                    notificationRecord.customType = (int)VANotificationsCustomType.NOTIFICATIONS_REGISTER_REWORD;
                                                    notificationRecord.customValue = "-999";
                                                    notificationRecordMan.InsertNotificationRecord(notificationRecord);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        break;
                    #endregion

                    #region 优惠券使用奖励
                    //case VAMessageType.CLIENT_VERIFY_COUPON_REQUEST:
                    //    {
                    //        lock (verifyCouponRewardLock)
                    //        {
                    //            DataTable dtCustomerConnCoupon = couponMan.SelectCustomerConnCoupon(encourage.customerConnCouponID);
                    //            if (dtCustomerConnCoupon.Rows.Count == 1)
                    //            {
                    //                if (Common.ToDateTime(dtCustomerConnCoupon.Rows[0]["verifyRewardTime"]) == DateTime.MinValue)
                    //                {
                    //                    double encourageValue = Common.ToDouble(dtCustomerConnCoupon.Rows[0]["couponVerifyReward"]);
                    //                    DateTime currentTime = System.DateTime.Now;
                    //                    DataTable dtEncourage = systemConfigMan.SelectEncourageConfig();
                    //                    DataView dvEncourage = dtEncourage.DefaultView;
                    //                    dvEncourage.RowFilter = "configName = '" + (int)VAMessageType.CLIENT_VERIFY_COUPON_REQUEST + "'";
                    //                    if (dvEncourage.Count == 1)
                    //                    {
                    //                        if (encourageValue > 0 && !string.IsNullOrEmpty(encourage.pushToken))
                    //                        {
                    //                            using (TransactionScope scope = new TransactionScope())
                    //                            {
                    //                                bool flagUpdateCustomerConnCouponVerifyRewardTime = couponMan.UpdateCustomerConnCouponVerifyRewardTime(encourage.customerConnCouponID);
                    //                                //更新用户货币余额
                    //                                bool flagUpdateMoney19dianRemained = customerMan.UpdateMoney19dianRemained(encourage.customerId, encourageValue);
                    //                                //插入用户货币变动记录
                    //                                Money19dianDetail money19dianDetail = new Money19dianDetail();
                    //                                money19dianDetail.customerId = encourage.customerId;
                    //                                money19dianDetail.changeReason = Common.GetEnumDescription(VA19dianMoneyChangeReason.MONEY19DIAN_VERIFY_COUPON_REWORD);
                    //                                money19dianDetail.changeTime = currentTime;
                    //                                money19dianDetail.changeValue = encourageValue;
                    //                                long flagInsertMoney19dianDetail = money19dianDetailMan.InsertMoney19dianDetail(money19dianDetail);
                    //                                //插入推送记录
                    //                                NotificationRecord notificationRecord = new NotificationRecord();
                    //                                notificationRecord.addTime = currentTime;
                    //                                notificationRecord.appType = (int)encourage.appType;
                    //                                notificationRecord.isLocked = false;
                    //                                notificationRecord.isSent = false;
                    //                                notificationRecord.pushToken = encourage.pushToken;
                    //                                notificationRecord.sendCount = 0;
                    //                                string notificationMessage = Common.ToString(dvEncourage[0]["configMessage"]);
                    //                                notificationRecord.message = notificationMessage.Replace("{0}", Common.ToString(encourageValue));
                    //                                notificationRecord.customType = (int)VANotificationsCustomType.NOTIFICATIONS_VERIFY_COUPON_REWORD;
                    //                                notificationRecord.customValue = "-999";
                    //                                long flagInsertNotificationRecord = notificationRecordMan.InsertNotificationRecord(notificationRecord);
                    //                                if (flagUpdateCustomerConnCouponVerifyRewardTime && flagUpdateMoney19dianRemained
                    //                                    && flagInsertNotificationRecord > 0 && flagInsertMoney19dianDetail > 0)
                    //                                {
                    //                                    scope.Complete();
                    //                                }
                    //                            }
                    //                        }
                    //                    }
                    //                }
                    //            }
                    //        }
                    //    }
                    //    break;
                    #endregion

                    #region 手机绑定奖励
                    case VAMessageType.CLIENT_MOBILE_VERIFY_REQUEST:
                        {
                            lock (verifyMobileRewardLock)
                            {
                                DataTable dtCustomer = customerMan.SelectCustomer(encourage.customerId);
                                if (dtCustomer.Rows.Count == 1)
                                {
                                    if (Common.ToDateTime(dtCustomer.Rows[0]["verifyMobileRewardTime"]) == DateTime.MinValue)
                                    {
                                        DateTime currentTime = System.DateTime.Now;
                                        DataTable dtEncourage = systemConfigMan.SelectEncourageConfig();
                                        DataView dvEncourage = dtEncourage.DefaultView;
                                        dvEncourage.RowFilter = "configName = '" + (int)VAMessageType.CLIENT_MOBILE_VERIFY_REQUEST + "'";
                                        if (dvEncourage.Count == 1)
                                        {
                                            double encourageValue = Common.ToDouble(dvEncourage[0]["configContent"]);
                                            if (encourageValue > 0)
                                            {
                                                double remainMoney = SybMoneyCustomerOperate.GetCustomerRemainMoney(encourage.customerId);
                                                using (TransactionScope scope = new TransactionScope())
                                                {
                                                    bool flagUpdateVerifyMobileRewardTime = customerMan.UpdateVerifyMobileRewardTime(encourage.customerId);
                                                    //更新用户货币余额
                                                    bool flagUpdateMoney19dianRemained = customerMan.UpdateMoney19dianRemained(encourage.customerId, encourageValue);
                                                    //插入用户货币变动记录
                                                    Money19dianDetail money19dianDetail = new Money19dianDetail()
                                                    {
                                                        customerId = encourage.customerId,
                                                        changeReason = Common.GetEnumDescription(VA19dianMoneyChangeReason.MONEY19DIAN_VERIFY_MOBILE_REWORD),
                                                        changeTime = currentTime,
                                                        changeValue = encourageValue,
                                                        inoutcomeType = Convert.ToInt32(InoutcomeType.IN),
                                                        accountType = Convert.ToInt32(AccountType.BIND_MOBILE),
                                                        accountTypeConnId = "", //待处理 需要获取
                                                        companyId = 0,
                                                        shopId = 0,
                                                        flowNumber = SybMoneyOperate.CreateCustomerFlowNumber(encourage.customerId),
                                                        remainMoney = encourageValue + remainMoney
                                                    };
                                                    long flagInsertMoney19dianDetail = Money19dianDetailManager.InsertMoney19dianDetail(money19dianDetail);
                                                    if (flagUpdateVerifyMobileRewardTime && flagUpdateMoney19dianRemained
                                                        && flagInsertMoney19dianDetail > 0)
                                                    {
                                                        scope.Complete();
                                                    }
                                                }
                                                if (string.IsNullOrEmpty(encourage.pushToken))
                                                {
                                                    //插入推送记录
                                                    NotificationRecord notificationRecord = new NotificationRecord();
                                                    notificationRecord.addTime = currentTime;
                                                    notificationRecord.appType = (int)encourage.appType;
                                                    notificationRecord.isLocked = false;
                                                    notificationRecord.isSent = false;
                                                    notificationRecord.pushToken = encourage.pushToken;
                                                    notificationRecord.sendCount = 0;
                                                    string notificationMessage = Common.ToString(dvEncourage[0]["configMessage"]);
                                                    notificationRecord.message = notificationMessage.Replace("{0}", Common.ToString(encourageValue));
                                                    notificationRecord.customType = (int)VANotificationsCustomType.NOTIFICATIONS_VERIFY_MOBILE_REWORD;
                                                    notificationRecord.customValue = "-999";
                                                    notificationRecordMan.InsertNotificationRecord(notificationRecord);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        break;
                    #endregion

                    #region 邀请用户奖励
                    case VAMessageType.CLIENT_INVITE_CUSTOMER_REQUEST:
                        {
                            lock (inviteCustomerRewardLock)
                            {
                                DataTable dtCustomerInviteRecord = customerMan.SelectCustomerInviteRecord(encourage.phoneNumberInvited);
                                if (dtCustomerInviteRecord.Rows.Count > 0)
                                {
                                    DateTime currentTime = System.DateTime.Now;
                                    DataTable dtEncourage = systemConfigMan.SelectEncourageConfig();
                                    DataView dvEncourage = dtEncourage.DefaultView;
                                    dvEncourage.RowFilter = "configName = '" + (int)VAMessageType.CLIENT_INVITE_CUSTOMER_REQUEST + "'";
                                    DataTable dtSystemConfig = systemConfigMan.SelectSystemConfig(string.Empty, string.Empty);
                                    DataTable dtSystemConfigCopy = dtSystemConfig.Copy();
                                    DataView dvSystemConfig = dtSystemConfig.DefaultView;
                                    DataView dvSystemConfigCopy = dtSystemConfigCopy.DefaultView;
                                    dvSystemConfig.RowFilter = "configName = 'secondBatchInviteRewardNumber'";
                                    dvSystemConfigCopy.RowFilter = "configName = 'inviteTimeSpan'";
                                    //设置默认的非首邀奖励人数和过期时间，避免读取配置失败时出错
                                    int secondBatchInviteRewardNumber = 1;
                                    int inviteTimeSpan = 3;
                                    if (dvSystemConfig.Count == 1)
                                    {
                                        int secondBatchInviteRewardNumberFromConfig = Common.ToInt32(dvSystemConfig[0]["configContent"]);
                                        if (secondBatchInviteRewardNumberFromConfig > 0)
                                        {
                                            secondBatchInviteRewardNumber = secondBatchInviteRewardNumberFromConfig;
                                        }
                                    }
                                    if (dvSystemConfigCopy.Count == 1)
                                    {
                                        int inviteTimeSpanFromConfig = Common.ToInt32(dvSystemConfigCopy[0]["configContent"]);
                                        if (inviteTimeSpanFromConfig > 0)
                                        {
                                            inviteTimeSpan = inviteTimeSpanFromConfig;
                                        }
                                    }
                                    if (dvEncourage.Count == 1)
                                    {
                                        string encourageValuestring = Common.ToString(dvEncourage[0]["configContent"]);
                                        string[] encourageValueList;
                                        encourageValueList = encourageValuestring.Split(new string[1] { "*" }, StringSplitOptions.RemoveEmptyEntries);
                                        double firstInviteRewardValue = 5;
                                        double secondBatchRewardValue = 1;
                                        if (encourageValueList.Length == 2)
                                        {
                                            firstInviteRewardValue = Common.ToDouble(encourageValueList[0]);
                                            secondBatchRewardValue = Common.ToDouble(encourageValueList[1]);
                                        }
                                        string notificationMessageString = Common.ToString(dvEncourage[0]["configMessage"]);
                                        string[] notificationMessageList;
                                        //string abc = "您邀请的手机号码为{0}的用户认证成功,您是第一位邀请他的用户，恭喜您获得首邀奖{1}元";
                                        //string agc = "您邀请的手机号码为{0}的用户认证成功,但是被手机尾号为{1}的用户抢先邀请，他获得首邀奖{2}元,您获得奖励{3}元";
                                        //string adw = "您邀请的手机号码为{0}的用户认证成功,但是您的邀请已经过期或者您不是前{1}位邀请他的用户，很遗憾未能获得{2}元奖励";
                                        notificationMessageList = notificationMessageString.Split(new string[1] { "*" }, StringSplitOptions.RemoveEmptyEntries);
                                        string firstInviteRewardMessage = "您邀请的用户认证成功,您是第一位邀请他的用户，恭喜您获得首邀奖";
                                        string secondBatchRewardMessage = "您邀请的用户认证成功,但是被其他抢先邀请，他获得首邀奖,您获得非首邀奖励";
                                        string noRewardMessage = "您邀请的用户认证成功,但是您的邀请已经过期或者您不是前2位邀请他的用户，很遗憾未能获得5元奖励";
                                        if (notificationMessageList.Length == 3)
                                        {
                                            firstInviteRewardMessage = Common.ToString(notificationMessageList[0]);
                                            secondBatchRewardMessage = Common.ToString(notificationMessageList[1]);
                                            noRewardMessage = Common.ToString(notificationMessageList[2]);
                                        }
                                        int inviteNumberOutOfDate = 0;
                                        string firstInviterPhoneNumber = "13777485730";
                                        for (int i = 0; i < dtCustomerInviteRecord.Rows.Count; i++)
                                        {
                                            DataTable dtCustomer = customerMan.SelectCustomerTopDevice(Common.ToInt64(dtCustomerInviteRecord.Rows[i]["customerId"]));
                                            if (dtCustomer.Rows.Count == 1)
                                            {
                                                long customerId = Common.ToInt64(dtCustomer.Rows[0]["CustomerID"]);
                                                //待处理 可能获取的不对
                                                double remainMoney = SybMoneyCustomerOperate.GetCustomerRemainMoney(customerId);
                                                string pushToken = Common.ToString(dtCustomer.Rows[0]["pushToken"]);
                                                bool flagUpdateInviteRewardTimeAndValue = false;
                                                bool flagUpdateMoney19dianRemained = false;
                                                long flagInsertMoney19dianDetail = 0;
                                                double encourageValue = 0;
                                                string notificationMessage = "";
                                                using (TransactionScope scope = new TransactionScope())
                                                {
                                                    DateTime inviteTime = Common.ToDateTime(dtCustomerInviteRecord.Rows[i]["inviteTime"]);
                                                    if ((System.DateTime.Now - inviteTime) < TimeSpan.FromDays(inviteTimeSpan))
                                                    {
                                                        if (inviteNumberOutOfDate == 0)
                                                        {
                                                            firstInviterPhoneNumber = Common.ToString(dtCustomerInviteRecord.Rows[i]["phoneNumberInvite"]);
                                                            encourageValue = firstInviteRewardValue;
                                                            notificationMessage = firstInviteRewardMessage.Replace("{0}", Common.ToString(dtCustomerInviteRecord.Rows[i]["phoneNumberInvited"]));
                                                            notificationMessage = notificationMessage.Replace("{1}", Common.ToString(firstInviteRewardValue));
                                                        }
                                                        else if (inviteNumberOutOfDate <= secondBatchInviteRewardNumber)
                                                        {
                                                            encourageValue = secondBatchRewardValue;
                                                            notificationMessage = secondBatchRewardMessage.Replace("{0}", Common.ToString(dtCustomerInviteRecord.Rows[i]["phoneNumberInvited"]));
                                                            notificationMessage = notificationMessage.Replace("{1}", firstInviterPhoneNumber.Substring(7, 4));//截取后四位作为手机尾号
                                                            notificationMessage = notificationMessage.Replace("{2}", Common.ToString(firstInviteRewardValue));
                                                            notificationMessage = notificationMessage.Replace("{3}", Common.ToString(secondBatchRewardValue));
                                                        }
                                                        flagUpdateInviteRewardTimeAndValue = customerMan.UpdateInviteRewardTimeAndValue(
                                                            Common.ToInt32(dtCustomerInviteRecord.Rows[i]["id"]), encourageValue);
                                                        if (encourageValue > 0)
                                                        {
                                                            //更新用户货币余额
                                                            flagUpdateMoney19dianRemained = customerMan.UpdateMoney19dianRemained(customerId, encourageValue);
                                                            //插入用户货币变动记录
                                                            //修改标识: 罗国华20131121
                                                            Money19dianDetail money19dianDetail = new Money19dianDetail()
                                                            {
                                                                customerId = customerId,
                                                                changeReason = Common.GetEnumDescription(VA19dianMoneyChangeReason.MONEY19DIAN_INVITE_CUSTOMER_REWORD),
                                                                changeTime = currentTime,
                                                                changeValue = encourageValue,
                                                                inoutcomeType = Convert.ToInt32(InoutcomeType.IN),
                                                                accountType = Convert.ToInt32(AccountType.INVITE_USER),
                                                                accountTypeConnId = "", //待处理 需要获取
                                                                companyId = 0,
                                                                shopId = 0,
                                                                flowNumber = SybMoneyOperate.CreateCustomerFlowNumber(customerId),
                                                                remainMoney = encourageValue + remainMoney
                                                            };

                                                            flagInsertMoney19dianDetail = Money19dianDetailManager.InsertMoney19dianDetail(money19dianDetail);
                                                        }
                                                        else
                                                        {
                                                            flagUpdateMoney19dianRemained = true;
                                                            flagInsertMoney19dianDetail = 1;
                                                        }
                                                        inviteNumberOutOfDate++;
                                                    }
                                                    else
                                                    {
                                                        flagUpdateInviteRewardTimeAndValue = true;
                                                        flagUpdateMoney19dianRemained = true;
                                                        flagInsertMoney19dianDetail = 1;
                                                        notificationMessage = noRewardMessage.Replace("{0}", Common.ToString(dtCustomerInviteRecord.Rows[i]["phoneNumberInvited"]));
                                                        notificationMessage = notificationMessage.Replace("{1}", Common.ToString(secondBatchRewardValue + 1));
                                                        notificationMessage = notificationMessage.Replace("{2}", Common.ToString(firstInviteRewardValue));
                                                    }
                                                    if (flagUpdateInviteRewardTimeAndValue && flagUpdateMoney19dianRemained
                                                        && flagInsertMoney19dianDetail > 0)
                                                    {
                                                        scope.Complete();
                                                    }
                                                }
                                                if (encourageValue > 0 && !string.IsNullOrEmpty(encourage.pushToken))
                                                {
                                                    NotificationRecord notificationRecord = new NotificationRecord();
                                                    notificationRecord.addTime = currentTime;
                                                    notificationRecord.appType = Common.ToInt32(dtCustomer.Rows[0]["appType"]);
                                                    notificationRecord.isLocked = false;
                                                    notificationRecord.isSent = false;
                                                    notificationRecord.pushToken = pushToken;
                                                    notificationRecord.sendCount = 0;

                                                    notificationRecord.message = notificationMessage;
                                                    notificationRecord.customType = (int)VANotificationsCustomType.NOTIFICATIONS_INVITE_CUSTOMER_REWORD;
                                                    notificationRecord.customValue = "-999";
                                                    notificationRecordMan.InsertNotificationRecord(notificationRecord);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        break;
                    #endregion

                    default:
                        break;
                }
            }
            catch (System.Exception)
            {
                //throw ex;
            }
        }
        /// <summary>
        /// 通过地址查询百度地图经纬度
        /// </summary>
        /// <param name="address"></param>
        /// <returns>成功则返回经纬度对象location，失败则返回null</returns>
        /// 2013-7-20 wangcheng添加参数，cityName
        public static MapLocation GetBaiduMapCoordinate(string address, string cityName)
        {
            BaiduMapCoordinateInfo baiduMapCoordinateInfo = new BaiduMapCoordinateInfo();
            try
            {
                string url = "http://api.map.baidu.com/geocoder/v2/?ak=xIaOmBpthTUf8zF1WurZyBkU&output=json&address=" + address + "&city=" + cityName;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                String responseString = String.Empty;
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    responseString = reader.ReadToEnd();
                }
                baiduMapCoordinateInfo = JsonOperate.JsonDeserialize<BaiduMapCoordinateInfo>(responseString);
                if (!string.Equals(baiduMapCoordinateInfo.status, "0"))
                {
                    return null;
                }
            }
            catch (System.Exception)
            {
                return null;
            }
            return baiduMapCoordinateInfo.result.location;
        }

        /// <summary>
        /// 将GPS座标转换为百度座标
        /// </summary>
        /// <param name="address"></param>
        /// <param name="cityName"></param>
        /// <returns></returns>
        public static MapLocation GetBaiduMapCoordinateFromGps(double longitude, double latitude)
        {
            String url = String.Format("http://api.map.baidu.com/ag/coord/convert?from=0&to=4&x={0}&y={1}", longitude, latitude);
            HttpWebRequest urlConnection = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse response = (HttpWebResponse)urlConnection.GetResponse();
            String responseString = String.Empty;
            MapLocation mapLocation = new MapLocation();
            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                responseString = reader.ReadToEnd();
            }
            if (responseString.StartsWith("{") && responseString.EndsWith("}"))
            {
                responseString = responseString.Substring(1, responseString.Length - 2).Replace("\"", "");
                String[] lines = responseString.Split(',');
                foreach (var line in lines)
                {
                    String[] items = line.Split(':');
                    if (items.Length == 2)
                    {
                        //if ("error".Equals(items[0]))
                        //{
                        //    bl.ok = "0".Equals(items[1]);
                        //}
                        if ("x".Equals(items[0]))
                        {
                            byte[] bs = Base64.Decode(items[1]);
                            mapLocation.lng = double.Parse(System.Text.Encoding.Default.GetString(bs));
                        }
                        if ("y".Equals(items[0]))
                        {
                            byte[] bs = Base64.Decode(items[1]);
                            mapLocation.lat = double.Parse(System.Text.Encoding.Default.GetString(bs));
                        }
                    }
                }
            }
            return mapLocation;
        }
        /// <summary>
        /// 根据经纬度获取城市名称
        /// </summary>
        /// <param name="lat"></param>
        /// <param name="lng"></param>
        /// <returns></returns>
        public static string GetCityNameByBaiduAPI(double lat, double lng)
        {
            BaiduMapCoordinateInfo baiduMapCoordinateInfo = new BaiduMapCoordinateInfo();
            try
            {
                string url = "http://api.map.baidu.com/geocoder/v2/?ak=5624f9fa22d13db3a434628282476cdd&location=" + lat + "," + lng + "&output=json&pois=0";
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                String responseString = String.Empty;
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    responseString = reader.ReadToEnd();
                }
                baiduMapCoordinateInfo = JsonOperate.JsonDeserialize<BaiduMapCoordinateInfo>(responseString);
                if (!string.Equals(baiduMapCoordinateInfo.status, "0"))
                {
                    return "";
                }
                else
                {
                    return baiduMapCoordinateInfo.result.addressComponent.city;
                }
            }
            catch (System.Exception)
            {
                return "";
            }
        }
        /// <summary>
        /// 生成字母和数字随机数
        /// </summary>
        /// <param name="length"></param>
        /// <param name="sleep"></param>
        /// <returns></returns>
        public static string randomStrAndNum(int length, bool sleep)
        {
            if (sleep)
            {
                System.Threading.Thread.Sleep(3);
            }
            char[] Pattern = new char[] { '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'J', 'K', 'L', 'M', 'N', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y' };
            string result = "";
            int n = Pattern.Length;
            System.Random random = new Random(~unchecked((int)DateTime.Now.Ticks));
            for (int i = 0; i < length; i++)
            {
                int rnd = random.Next(0, n);
                result += Pattern[rnd];
            }
            return result;
        }
        /// <summary>
        /// 生成随机数字字符串
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string randomStrAndNum(int length)
        {
            char[] Pattern = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            string result = "";
            int n = Pattern.Length;
            System.Random random = new Random(~unchecked((int)DateTime.Now.Ticks));
            for (int i = 0; i < length; i++)
            {
                int rnd = random.Next(0, n);
                result += Pattern[rnd];
            }
            return result;
        }
        /// <summary>
        /// 通用静态变量
        /// </summary>
        public static class Global
        {
            /// <summary>
            /// 纬度0.00001对应的距离，1.12米
            /// </summary>
            private static double _unitDistanceOfCoordinate = 1.12;
            public static double unitDistanceOfCoordinate
            {
                get { return _unitDistanceOfCoordinate; }
            }
        }
        /// <summary>
        /// 全角转半角
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToNarrowString(string str)
        {
            try
            {
                str = Strings.StrConv(str, VbStrConv.Narrow);
            }
            catch (System.Exception)
            {
            }
            return str;
        }
        /// <summary>
        /// 通过Webconfig指定的邮箱发送邮件
        /// </summary>
        /// <param name="emailAddressTo"></param>
        /// <param name="messageBody"></param>
        /// <param name="subject"></param>
        /// <returns></returns>
        public static bool SendEmailFrom19dianService(string emailAddressTo, string messageBody, string subject)
        {
            try
            {
                string smtp = ConfigurationManager.AppSettings["smtp"].Trim();
                string fromEmailAddress = ConfigurationManager.AppSettings["ForgetEmailAddress"].Trim();
                string fromEmailAddressPassword = ConfigurationManager.AppSettings["ForgetEmailAddressPassword"].Trim();
                MailAddress from = new MailAddress(fromEmailAddress, "ViewAlloc");
                MailAddress to = new MailAddress(emailAddressTo, "ViewAlloc");
                MailMessage message = new MailMessage(from, to);
                message.Subject = subject;
                message.Body = messageBody;
                message.IsBodyHtml = true;

                SmtpClient client = new SmtpClient(smtp);
                client.Credentials = CredentialCache.DefaultNetworkCredentials;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Port = 25;
                client.Credentials = new System.Net.NetworkCredential(fromEmailAddress, fromEmailAddressPassword);

                client.Send(message);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 通过Webconfig指定的邮箱给N个收件者发送邮件
        /// </summary>
        /// <param name="emailAddressTo">收件者地址，多个收件者直接用英文分号分隔</param>
        /// <param name="messageBody">内容</param>
        /// <param name="subject">主题</param>
        /// <returns></returns>
        public static void SendNEmailFrom19dianService(object emailInfo)
        {
            try
            {
                VAEmailInfo currentEmailInfo = (VAEmailInfo)emailInfo;
                string smtp = ConfigurationManager.AppSettings["smtp"].Trim();
                string fromEmailAddress = ConfigurationManager.AppSettings["ForgetEmailAddress"].Trim();
                string fromEmailAddressPassword = ConfigurationManager.AppSettings["ForgetEmailAddressPassword"].Trim();

                SmtpClient client = new SmtpClient(smtp);
                client.Credentials = CredentialCache.DefaultNetworkCredentials;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Port = 25;
                client.Credentials = new System.Net.NetworkCredential(fromEmailAddress, fromEmailAddressPassword);

                MailAddress from = new MailAddress(fromEmailAddress, "ViewAlloc");

                if (currentEmailInfo.emailAddressTo.Contains(";"))//有多个收件者
                {
                    string[] strToAll = currentEmailInfo.emailAddressTo.Split(';');//先将多个邮件拆开

                    foreach (string strTo in strToAll)
                    {
                        MailAddress to = new MailAddress(strTo, "ViewAlloc");

                        MailMessage message = new MailMessage(from, to);
                        message.Subject = currentEmailInfo.subject;
                        message.Body = currentEmailInfo.messageBody;
                        message.IsBodyHtml = true;

                        client.Send(message);
                    }
                }
                else
                {
                    MailAddress to = new MailAddress(currentEmailInfo.emailAddressTo, "ViewAlloc");

                    MailMessage message = new MailMessage(from, to);
                    message.Subject = currentEmailInfo.subject;
                    message.Body = currentEmailInfo.messageBody;
                    message.IsBodyHtml = true;

                    client.Send(message);
                }
            }
            catch (Exception)
            {

            }
        }
        /// <summary>
        /// 获取枚举描述信息方法
        /// 获取失败或者没有描述则返回原字符串
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetEnumDescription(Enum value)
        {
            try
            {
                FieldInfo field = value.GetType().GetField(value.ToString());
                DescriptionAttribute[] attributes = (DescriptionAttribute[])field.GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attributes != null && attributes.Length > 0)
                {
                    return attributes[0].Description;
                }
                else
                {
                    return value.ToString();
                }
            }
            catch (System.Exception)
            {
                return string.Empty;
            }
        }
        /// <summary>
        /// 填充店铺信息
        /// </summary>
        /// <param name="dtRestaurant"></param>
        /// <returns></returns>
        public static List<VARestaurant> FillRestaurant(DataTable dtRestaurant, VAShopDetailFillType shopDetailFillType = VAShopDetailFillType.DEFAULT)
        {
            //http://192.168.1.18/UploadFiles/Images/
            string imagePath = ConfigurationManager.AppSettings["Server"] + "/" + ConfigurationManager.AppSettings["ImagePath"];
            List<VARestaurant> restaurantList = new List<VARestaurant>();
            for (int i = 0; i < dtRestaurant.Rows.Count; i++)
            {
                VARestaurant restaurant = new VARestaurant();
                switch (shopDetailFillType)
                {
                    case VAShopDetailFillType.CUSTOMERCOUPON:
                        {
                            restaurant.restaurantId = Common.ToInt32(dtRestaurant.Rows[i]["shopID"]);
                            restaurant.companyId = Common.ToInt32(dtRestaurant.Rows[i]["companyID"]);
                        } break;
                    case VAShopDetailFillType.DEFAULT:
                    default:
                        {
                            restaurant.restaurantId = Common.ToInt32(dtRestaurant.Rows[i]["shopID"]);
                            restaurant.name = Common.ToString(dtRestaurant.Rows[i]["shopName"]);
                            restaurant.description = Common.ToString(dtRestaurant.Rows[i]["shopDescription"]);
                            restaurant.longitude = Common.ToDouble11Digit(dtRestaurant.Rows[i]["longitude"]);
                            restaurant.latitude = Common.ToDouble11Digit(dtRestaurant.Rows[i]["latitude"]);
                            restaurant.address = Common.ToString(dtRestaurant.Rows[i]["shopAddress"]);
                            restaurant.phone = Common.ToString(dtRestaurant.Rows[i]["shopTelephone"]);
                            string shopLogo = Common.ToString(dtRestaurant.Rows[i]["shopLogo"]);
                            if (string.IsNullOrEmpty(shopLogo))
                            {
                                restaurant.thumbnailImageUrl = "";
                            }
                            else
                            {
                                restaurant.thumbnailImageUrl = imagePath + Common.ToString(dtRestaurant.Rows[i]["shopImagePath"]) + shopLogo;
                            }
                            restaurant.companyId = Common.ToInt32(dtRestaurant.Rows[i]["companyID"]);
                            if (Common.ToInt32(dtRestaurant.Rows[i]["prePayCashBackCount"]) > 0)
                            {
                                restaurant.supportPrePayCashBack = true;
                            }
                            if (Common.ToInt32(dtRestaurant.Rows[i]["prePayVIPCount"]) > 0)
                            {
                                restaurant.supportPrePayVIPEntrance = true;
                            }
                            if (Common.ToInt32(dtRestaurant.Rows[i]["prePaySendGiftCount"]) > 0)
                            {
                                restaurant.supportPrePayGift = true;
                            }
                            MenuManager menuMan = new MenuManager();
                            DataTable dtMenu = menuMan.SelectMenuByShop(restaurant.restaurantId);
                            restaurant.menuList = Common.FillMenuForApp(dtMenu);
                            //restaurant.prepayPolicies = QueryPrepayPrivilegeByShopId(restaurant.restaurantId, 0);
                            restaurant.prepayPolicies = null;
                            ShopManager shop = new ShopManager();
                            restaurant.openingTime = shop.GetOpenTime(Common.ToInt32(dtRestaurant.Rows[i]["shopID"]));
                            restaurant.shopMedal = Common.FillShopMedal(Common.ToInt32(dtRestaurant.Rows[i]["shopID"]));
                            restaurant.sundryInfo = Common.FillSundry(Common.ToInt32(dtRestaurant.Rows[i]["shopID"]));
                            ShopManager shopmanager = new ShopManager();
                            DataTable dtli = shopmanager.GetQueryListMFS(Common.ToInt32(dtRestaurant.Rows[i]["shopID"]));
                            if (dtli.Rows.Count > 0)
                            {
                                for (int n = 0; n < dtli.Rows.Count; n++)
                                {
                                    switch (Common.ToInt32(dtli.Rows[n]["descriptionType"]))
                                    {
                                        case (int)VAQueueFreeAndCouponBackAndDishGift.SHOP_COUPONBACK: restaurant.couponBackDesc = Common.ToString(dtli.Rows[n]["description"]); break;
                                        case (int)VAQueueFreeAndCouponBackAndDishGift.SHOP_QUEUERFEE: restaurant.queueFreeDesc = Common.ToString(dtli.Rows[n]["description"]); break;
                                        case (int)VAQueueFreeAndCouponBackAndDishGift.SHOP_DISHGIFT: restaurant.dishGiftDesc = Common.ToString(dtli.Rows[n]["description"]); break;
                                    }
                                }
                            }
                        } break;
                }
                //其他项暂时没填充
                restaurantList.Add(restaurant);
            }
            return restaurantList;
        }
        /// <summary>
        /// 填充杂项
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public static List<VASundryInfo> FillSundry(int shopId)
        {
            DataTable dtSundry = new ShopInfoCacheLogic().GetShopSundryInfo(shopId);
            List<VASundryInfo> sundryList = new List<VASundryInfo>();
            for (int i = 0; i < dtSundry.Rows.Count; i++)
            {
                VASundryInfo sundry = new VASundryInfo();
                sundry.sundryId = Common.ToInt32(dtSundry.Rows[i]["sundryId"]);
                sundry.sundryName = Common.ToString(dtSundry.Rows[i]["sundryName"]);
                sundry.price = Common.ToDouble(dtSundry.Rows[i]["price"]);
                sundry.sundryChargeMode = Common.ToInt32(dtSundry.Rows[i]["sundryChargeMode"]);
                sundry.sundryStandard = Common.ToString(dtSundry.Rows[i]["sundryStandard"]);
                sundry.backDiscountable = Common.ToBool(dtSundry.Rows[i]["backDiscountable"]);
                sundry.vipDiscountable = Common.ToBool(dtSundry.Rows[i]["vipDiscountable"]);
                sundry.required = Common.ToBool(dtSundry.Rows[i]["required"]);
                if (sundry.sundryChargeMode != 3)//如果不是按人次的话
                {
                    sundry.required = false;
                }
                sundry.quantity = 2;
                sundryList.Add(sundry);
            }
            return sundryList;
        }

        public static List<VAMedalInfo> FillShopMedal(int shopId)
        {
            ShopOperate SO = new ShopOperate();
            ShopInfo companyinfo = SO.QueryShop(shopId);
            string imagePath = ConfigurationManager.AppSettings["Server"] + "/" + ConfigurationManager.AppSettings["ImagePath"] + companyinfo.shopImagePath;
            ShopManager SManager = new ShopManager();
            DataTable dtMedal = SManager.GetMedal(shopId);
            List<VAMedalInfo> medallist = new List<VAMedalInfo>();
            for (int i = 0; i < dtMedal.Rows.Count; i++)
            {
                VAMedalInfo medal = new VAMedalInfo();
                medal.name = Common.ToString(dtMedal.Rows[i]["medalName"]);
                medal.imageURL = imagePath + Common.ToString(dtMedal.Rows[i]["medalURL"]);
                medal.medalDescription = Common.ToString(dtMedal.Rows[i]["medalDescription"]);
                medal.smallImageURL = imagePath + Common.ToString(dtMedal.Rows[i]["smallmedalURL"]);
                medallist.Add(medal);
            }
            return medallist;

        }
        /// <summary>
        /// 填充店铺公司列表
        /// </summary>
        /// <returns></returns>
        public static List<VABrand> FillBrand(DataTable dtBrand, int cityId, VAAppType appType, DataTable dtcustomerFavoriteCompany = null)
        {
            //http://192.168.1.18/UploadFiles/Images/
            string imagePath = ConfigurationManager.AppSettings["Server"] + "/" + ConfigurationManager.AppSettings["ImagePath"];
            List<VABrand> brandList = new List<VABrand>();
            for (int i = 0; i < dtBrand.Rows.Count; i++)
            {
                VABrand brand = new VABrand();
                brand.name = Common.ToString(dtBrand.Rows[i]["companyName"]);
                brand.description = Common.ToString(dtBrand.Rows[i]["companyDescription"]);
                string companyLogo = Common.ToString(dtBrand.Rows[i]["companyLogo"]);
                if (string.IsNullOrEmpty(companyLogo))
                {
                    brand.logoUrlString = "";
                }
                else
                {
                    brand.logoUrlString = imagePath + Common.ToString(dtBrand.Rows[i]["companyImagePath"]) + companyLogo;
                }
                brand.companyId = Common.ToInt32(dtBrand.Rows[i]["companyID"]);
                brand.acpp = Common.ToDouble(dtBrand.Rows[i]["acpp"]);
                brand.numberOfPreorders = Common.ToInt64(dtBrand.Rows[i]["preorderCount"]);
                brand.numberOfPrepays = Common.ToInt64(dtBrand.Rows[i]["prepayOrderCount"]);
                if (dtcustomerFavoriteCompany != null)
                {//判断该公司用户是否收藏
                    DataTable dtcustomerFavoriteCompanyCopy = dtcustomerFavoriteCompany.Copy();
                    DataView dvcustomerFavoriteCompany = dtcustomerFavoriteCompanyCopy.DefaultView;
                    dvcustomerFavoriteCompany.RowFilter = "companyID = '" + brand.companyId + "'";
                    if (dvcustomerFavoriteCompany.Count > 0)
                    {
                        brand.isFavorite = true;
                    }
                    else
                    {
                        brand.isFavorite = false;
                    }
                }
                else
                {
                    brand.isFavorite = false;
                }
                int defaultMenuId = Common.ToInt32(dtBrand.Rows[i]["defaultMenuId"]);
                MenuManager menuMan = new MenuManager();
                DataTable dtMenu = menuMan.SelectMenu(defaultMenuId);
                string defaultMenuURL = string.Empty;
                if (dtMenu.Rows.Count == 1)
                {//菜谱plist文件的格式为menuId_menuVersion.zip
                    defaultMenuURL = imagePath + Common.ToString(dtMenu.Rows[0]["menuImagePath"]) + Common.ToString(dtMenu.Rows[0]["MenuID"]) + "_" + Common.ToString(dtMenu.Rows[0]["MenuVersion"]) + ".zip";
                }
                brand.defaultMenuUrl = defaultMenuURL;
                ShopManager shopMan = new ShopManager();
                DataTable dtShop = shopMan.SelectShopByCompanyAndCity(brand.companyId, cityId, appType);
                brand.restaurantList = Common.FillRestaurant(dtShop);
                brand.companyMedal = Common.FillCompanyMedal(brand.companyId);
                if (Common.ToInt32(dtBrand.Rows[i]["prePayCashBackCount"]) > 0)
                {
                    brand.supportPrePayCashBack = true;
                }
                if (Common.ToInt32(dtBrand.Rows[i]["prePayVIPCount"]) > 0)
                {
                    brand.supportPrePayVIPEntrance = true;
                }
                if (Common.ToInt32(dtBrand.Rows[i]["prePaySendGiftCount"]) > 0)
                {
                    brand.supportPrePayGift = true;
                }
                //其他项暂时没填充
                brandList.Add(brand);
            }
            return brandList.OrderByDescending(i => i.isFavorite).ToList();
        }
        /// <summary>
        /// 填充公司medal
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public static List<VAMedalInfo> FillCompanyMedal(int companyId)
        {
            CompanyOperate CO = new CompanyOperate();
            CompanyInfo companyinfo = CO.QueryCompany(companyId);
            string imagePath = ConfigurationManager.AppSettings["Server"] + "/" + ConfigurationManager.AppSettings["ImagePath"] + companyinfo.companyImagePath;

            CompanyManager CManager = new CompanyManager();


            DataTable dtMedal = CManager.GetMedal(companyId);
            List<VAMedalInfo> medallist = new List<VAMedalInfo>();
            for (int i = 0; i < dtMedal.Rows.Count; i++)
            {
                VAMedalInfo medal = new VAMedalInfo();
                medal.name = Common.ToString(dtMedal.Rows[i]["medalName"]);
                medal.imageURL = imagePath + Common.ToString(dtMedal.Rows[i]["medalURL"]);
                medal.medalDescription = Common.ToString(dtMedal.Rows[i]["medalDescription"]);
                medal.smallImageURL = imagePath + Common.ToString(dtMedal.Rows[i]["smallmedalURL"]);
                medallist.Add(medal);
            }
            return medallist;

        }
        /// <summary>
        /// 填充公司Banner
        /// </summary>
        /// <param name="dtBrandBanner"></param>
        /// <param name="cityId"></param>
        /// <returns></returns>
        public static List<VABrandBanner> FillBrandBanner(DataTable dtBrandBanner, int cityId, VAAppType appType)
        {
            //http://192.168.1.18/UploadFiles/Images/
            string imagePath = ConfigurationManager.AppSettings["Server"] + "/" + ConfigurationManager.AppSettings["ImagePath"];
            List<VABrandBanner> brandList = new List<VABrandBanner>();
            for (int i = 0; i < dtBrandBanner.Rows.Count; i++)
            {
                VABrandBanner brand = new VABrandBanner();
                brand.name = Common.ToString(dtBrandBanner.Rows[i]["companyName"]);
                brand.description = Common.ToString(dtBrandBanner.Rows[i]["companyDescription"]);
                brand.logoUrlString = imagePath + Common.ToString(dtBrandBanner.Rows[i]["companyImagePath"]) + Common.ToString(dtBrandBanner.Rows[i]["companyLogo"]);
                brand.companyId = Common.ToInt32(dtBrandBanner.Rows[i]["companyID"]);
                brand.acpp = Common.ToDouble(dtBrandBanner.Rows[i]["acpp"]);
                brand.numberOfPreorders = Common.ToInt64(dtBrandBanner.Rows[i]["preorderCount"]);
                brand.numberOfPrepays = Common.ToInt64(dtBrandBanner.Rows[i]["prepayOrderCount"]);
                brand.bannerImageUrlString = imagePath + Common.ToString(dtBrandBanner.Rows[i]["imageURL"]);
                brand.bannerType = Common.ToInt32(dtBrandBanner.Rows[i]["advertisementAreaId"]);
                int defaultMenuId = Common.ToInt32(dtBrandBanner.Rows[i]["defaultMenuId"]);
                MenuManager menuMan = new MenuManager();
                DataTable dtMenu = menuMan.SelectMenu(defaultMenuId);
                string defaultMenuURL = string.Empty;
                if (dtMenu.Rows.Count == 1)
                {//菜谱plist文件的格式为menuId_menuVersion.zip
                    defaultMenuURL = imagePath + Common.ToString(dtMenu.Rows[0]["menuImagePath"])
                        + Common.ToString(dtMenu.Rows[0]["MenuID"])
                        + "_" + Common.ToString(dtMenu.Rows[0]["MenuVersion"]) + ".zip";
                }
                brand.defaultMenuUrl = defaultMenuURL;

                ShopManager shopMan = new ShopManager();
                DataTable dtShop = shopMan.SelectShopByCompanyAndCity(brand.companyId, cityId, appType);
                brand.restaurantList = Common.FillRestaurant(dtShop);
                CompanyManager listshop = new CompanyManager();
                //brand.companyId
                brand.shopId = listshop.GetShopIdbyCompanyId(Common.ToInt64(dtBrandBanner.Rows[i]["advertisementId"]));
                brand.bannerName = Common.ToString(dtBrandBanner.Rows[i]["name"]);
                brand.bannerDescript = Common.ToString(dtBrandBanner.Rows[i]["advertisementDescription"]);

                if (Common.ToInt32(dtBrandBanner.Rows[i]["prePayCashBackCount"]) > 0)
                {
                    brand.supportPrePayCashBack = true;
                }
                if (Common.ToInt32(dtBrandBanner.Rows[i]["prePayVIPCount"]) > 0)
                {
                    brand.supportPrePayVIPEntrance = true;
                }
                if (Common.ToInt32(dtBrandBanner.Rows[i]["prePaySendGiftCount"]) > 0)
                {
                    brand.supportPrePayGift = true;
                }

                //其他项暂时没填充

                brandList.Add(brand);
            }
            return brandList;
        }

        /// <summary>
        /// 填充Vip信息
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="customerId"></param>
        /// <param name="userCompletedOrderCount"></param>
        /// <param name="vipPolicies"></param>
        /// <param name="userPolicy"></param>
        public static void FillVipInfo(int companyId, long customerId, ref int userCompletedOrderCount, ref List<VAVipPolicy> vipPolicies, ref VAVipPolicy userPolicy)
        {
            VipManager vipMan = new VipManager();
            DataTable dtCompanyVip = vipMan.SelectCompanyVip(companyId);
            DataTable dtCustomerCompanyVip = vipMan.SelectCustomerCompanyVip(customerId, companyId);
            int lastNextRequirement = 0;
            for (int i = 0; i < dtCompanyVip.Rows.Count; i++)
            {
                VAVipPolicy vipPolicy = new VAVipPolicy();
                vipPolicy.policyId = Common.ToInt64(dtCompanyVip.Rows[i]["id"]);
                vipPolicy.name = Common.ToString(dtCompanyVip.Rows[i]["name"]);
                vipPolicy.discount = Common.ToDouble(dtCompanyVip.Rows[i]["discount"]);
                vipPolicy.nextRequirement = Common.ToInt32(dtCompanyVip.Rows[i]["nextRequirement"]);
                vipPolicy.requirement = lastNextRequirement;
                lastNextRequirement = vipPolicy.nextRequirement;
                if (dtCustomerCompanyVip.Rows.Count == 1)
                {
                    if (Common.ToInt64(dtCustomerCompanyVip.Rows[0]["companyVipId"]) == vipPolicy.policyId)
                    {
                        userPolicy = vipPolicy;
                        userCompletedOrderCount = Common.ToInt32(dtCustomerCompanyVip.Rows[0]["userCompletedOrderCount"]);
                    }
                }
                vipPolicies.Add(vipPolicy);
            }
            if (dtCustomerCompanyVip.Rows.Count != 1 && dtCompanyVip.Rows.Count > 0)
            {
                VAVipPolicy vipPolicy = new VAVipPolicy();
                vipPolicy.policyId = Common.ToInt64(dtCompanyVip.Rows[0]["id"]);
                vipPolicy.name = Common.ToString(dtCompanyVip.Rows[0]["name"]);
                vipPolicy.discount = Common.ToDouble(dtCompanyVip.Rows[0]["discount"]);
                vipPolicy.nextRequirement = Common.ToInt32(dtCompanyVip.Rows[0]["nextRequirement"]);
                vipPolicy.requirement = 0;
                userPolicy = vipPolicy;
            }
            if (vipPolicies.Count == 0)
            {
                vipPolicies = null;
            }
            if (userPolicy.policyId == 0)
            {
                userPolicy = null;
            }
        }
        ///// <summary>
        ///// 根据公司编号查询对应的预支付奖励，并设置预付的金额
        ///// </summary>
        ///// <param name="shopId"></param>
        ///// <param name="paymentAmount"></param>
        ///// <returns></returns>
        //public static List<VAOrderPrepayPolicy> QueryPrepayPrivilegeByShopId(int shopId, double paymentAmount)
        //{
        //    PreOrder19dianManager preOrder19dianMan = new PreOrder19dianManager();
        //    DataTable dtPrePayPrivilege = preOrder19dianMan.SelectPrePayPrivilegeByShop(shopId);
        //    DataView dvPrepayPrivilege = dtPrePayPrivilege.DefaultView;
        //    dvPrepayPrivilege.RowFilter = "isValid = 1";
        //    List<VAOrderPrepayPolicy> prepayPolicies = new List<VAOrderPrepayPolicy>();
        //    for (int i = 0; i < dvPrepayPrivilege.Count; i++)
        //    {
        //        VAOrderPrepayPolicy orderPrepayPolicy = new VAOrderPrepayPolicy();
        //        orderPrepayPolicy.policyId = Common.ToString(dvPrepayPrivilege[i]["id"]);
        //        orderPrepayPolicy.policyDescription = Common.ToString(dvPrepayPrivilege[i]["prePayPrivilegeStr"]);
        //        orderPrepayPolicy.paymentAmount = paymentAmount;
        //        orderPrepayPolicy.numberOfHoursAhead = Common.ToDouble(dvPrepayPrivilege[i]["numberOfHoursAhead"]);
        //        prepayPolicies.Add(orderPrepayPolicy);
        //    }
        //    return prepayPolicies;
        //}
        /// <summary>
        /// 填充手机端需要的菜谱信息
        /// </summary>
        /// <param name="dtMenu"></param>
        /// <returns></returns>
        public static List<VAMenuForApp> FillMenuForApp(DataTable dtMenu)
        {
            string imagePath = string.Empty;
            imagePath = WebConfig.OssDomain + WebConfig.ImagePath;
            List<VAMenuForApp> menuList = new List<VAMenuForApp>();
            for (int j = 0; j < dtMenu.Rows.Count; j++)
            {
                VAMenuForApp menuForApp = new VAMenuForApp();
                menuForApp.menuId = Common.ToInt32(dtMenu.Rows[j]["MenuID"]);
                menuForApp.menuName = Common.ToString(dtMenu.Rows[j]["MenuName"]);
                menuForApp.menuUrl = imagePath + Common.ToString(dtMenu.Rows[j]["menuImagePath"])
                                        + menuForApp.menuId + "_"
                                        + Common.ToString(dtMenu.Rows[j]["MenuVersion"])
                                        + ".zip";
                menuList.Add(menuForApp);
            }
            return menuList;
        }
        /// <summary>
        /// 给图片添加水印
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public static Bitmap AddWaterMark(Bitmap image)
        {
            Graphics g = Graphics.FromImage(image);
            #region 文字水印,目前没有使用
            //string text = waterMark;
            //int fontSize = waterMarkFontSize;
            //Font font = new Font("宋体", fontSize);
            //Brush brush = Brushes.DarkGray;
            //SizeF size = g.MeasureString(text, font);
            //g.DrawString(text, font, brush, image.Width - size.Width, image.Height - size.Height);
            #endregion

            Image waterImage = null;
            if (image.Width > 300)
            {
                waterImage = Image.FromFile(waterMarkPath);
            }
            else
            {
                waterImage = Image.FromFile(waterMarkSmallPath);
            }
            int x1 = (image.Width - waterImage.Width) / 2;
            int y1 = image.Height - waterImage.Height;
            PointF ulCorner = new PointF(x1, y1);
            PointF urCorner = new PointF(x1 + waterImage.Width, y1);
            PointF llCorner = new PointF(x1, image.Height);
            PointF[] destPara = { ulCorner, urCorner, llCorner };

            // Draw image to screen.
            g.DrawImage(waterImage, destPara);
            g.Dispose();
            return image;
        }
        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <returns></returns>
        //public static string GetVerificationCode()
        //{
        //    lock (getVerificationCodeLock)
        //    {
        //        CouponManager couponMan = new CouponManager();
        //        return couponMan.SelectAndDeleteVerificationCode();
        //    }
        //}

        #region config

        /// <summary>
        /// 图片水印字体大小
        /// </summary>
        private static int _waterMarkFontSize;
        public static int waterMarkFontSize
        {
            get
            {
                if (_waterMarkFontSize == 0)
                {
                    _waterMarkFontSize = int.Parse(ConfigurationManager.AppSettings["Font-Size"]);
                }
                return _waterMarkFontSize;
            }
        }
        /// <summary>
        /// 图片水印文字
        /// </summary>
        private static string _waterMark;
        public static string waterMark
        {
            get
            {
                if (string.IsNullOrEmpty(_waterMark))
                {
                    _waterMark = ConfigurationManager.AppSettings["WaterMark"];
                }
                return _waterMark;
            }
        }
        /// <summary>
        /// 水印大图路径
        /// </summary>
        private static string _waterMarkPath;
        public static string waterMarkPath
        {
            get
            {
                if (string.IsNullOrEmpty(_waterMarkPath))
                {
                    _waterMarkPath = ConfigurationManager.AppSettings["waterMarkPath"];
                }
                return _waterMarkPath;
            }
        }
        /// <summary>
        /// 水印小图路径
        /// </summary>
        private static string _waterMarkSmallPath;
        public static string waterMarkSmallPath
        {
            get
            {
                if (string.IsNullOrEmpty(_waterMarkSmallPath))
                {
                    _waterMarkSmallPath = ConfigurationManager.AppSettings["waterMarkSmallPath"];
                }
                return _waterMarkSmallPath;
            }
        }
        /// <summary>
        /// Vamsg页面调试标志
        /// </summary>
        private static string _debugFlag;
        public static string debugFlag
        {
            get
            {
                if (string.IsNullOrEmpty(_debugFlag))
                {
                    _debugFlag = ConfigurationManager.AppSettings["Debug"].Trim();
                }
                return _debugFlag;
            }
        }

        private static int _smsValidTime = -1;
        /// <summary>
        /// 短信有效期(秒)
        /// xiaoyu20140319
        /// </summary>
        public static int smsValidTime
        {
            get
            {
                if (_smsValidTime < 0)
                {
                    SystemConfigManager systemConfigMan = new SystemConfigManager();
                    DataTable dtSystem = systemConfigMan.SelectSystemConfig(string.Empty, string.Empty);
                    DataView dvSystem = dtSystem.DefaultView;
                    dvSystem.RowFilter = "configName = 'smsValidTime'";
                    if (dvSystem.Count == 1)
                    {
                        int configContent = Common.ToInt32(dvSystem[0]["configContent"]);
                        _smsValidTime = configContent > 0 ? configContent : 600;
                    }
                    else
                    {
                        _smsValidTime = 600;
                    }
                }
                return _smsValidTime;
            }
        }

        private static string _timeOutDebugFlag;
        /// <summary>
        /// 是否记录响应时间超过1秒的请求
        /// </summary>
        public static string timeOutDebugFlag
        {
            get
            {
                if (string.IsNullOrEmpty(_timeOutDebugFlag))
                {
                    _timeOutDebugFlag = ConfigurationManager.AppSettings["timeOutDebug"].Trim();
                }
                return _timeOutDebugFlag;
            }
        }
        #endregion

        /// <summary>
        /// 对字符串进行检查和替换其中的特殊字符
        /// </summary>
        /// <param name="strHtml"></param>
        /// <returns></returns>
        public static string HtmlToTxt(string strHtml)
        {
            string[] aryReg ={
                        @"<script[^>]*?>.*?</script>",
                        @"<(\/\s*)?!?((\w+:)?\w+)(\w+(\s*=?\s*(([""'])(\\[""'tbnr]|[^\7])*?\7|\w+)|.{0})|\s)*?(\/\s*)?>",
                        @"([\r\n])[\s]+",
                        @"&(quot|#34);",
                        @"&(amp|#38);",
                        @"&(lt|#60);",
                        @"&(gt|#62);", 
                        @"&(nbsp|#160);", 
                        @"&(iexcl|#161);",
                        @"&(cent|#162);",
                        @"&(pound|#163);",
                        @"&(copy|#169);",
                        @"&#(\d+);",
                        @"-->",
                        @"<!--.*\n"
                        };
            string newReg = aryReg[0];
            string strOutput = strHtml;
            for (int i = 0; i < aryReg.Length; i++)
            {
                Regex regex = new Regex(aryReg[i], RegexOptions.IgnoreCase);
                strOutput = regex.Replace(strOutput, string.Empty);
            }
            strOutput.Replace("<", "");
            strOutput.Replace(">", "");
            strOutput.Replace("\r\n", "");
            return strOutput;
        }

        /// <summary>
        /// 替换html中的特殊字符
        /// </summary>
        /// <param name="theString">需要进行替换的文本。</param>
        /// <returns>替换完的文本。</returns>
        public string HtmlEncode(string theString)
        {
            theString = theString.Replace(">", "&gt;");
            theString = theString.Replace("<", "&lt;");
            theString = theString.Replace(" ", "&nbsp;");
            theString = theString.Replace(" ", "&nbsp;");
            theString = theString.Replace("\"", "&quot;");
            theString = theString.Replace("\'", "'");
            theString = theString.Replace("\n", "<br/> ");
            return theString;
        }

        /// <summary>
        /// 恢复html中的特殊字符
        /// </summary>
        /// <param name="theString">需要恢复的文本。</param>
        /// <returns>恢复好的文本。</returns>
        public string HtmlDiscode(string theString)
        {
            theString = theString.Replace("&gt;", ">");
            theString = theString.Replace("&lt;", "<");
            theString = theString.Replace("&nbsp;", " ");
            theString = theString.Replace("&nbsp;", " ");
            theString = theString.Replace("&quot;", "\"");
            theString = theString.Replace("'", "\'");
            theString = theString.Replace("<br/> ", "\n");
            return theString;
        }

        /// <summary>
        /// CKEditor中处理特殊的Html字符
        /// </summary>
        /// <param name="theString"></param>
        /// <returns></returns>
        public static string HtmlDiscodeForCKEditor(string theString)
        {
            theString = theString.Replace("&#39;", "'");
            theString = theString.Replace("&quot;", "\"");
            //theString = theString.Replace("&gt;", ">");
            //theString = theString.Replace("&lt;", "<");
            //theString = theString.Replace("&amp;", "&");
            //theString = theString.Replace("&amp;", "&");
            theString = theString.Replace("&lsquo;", "‘");
            theString = theString.Replace("&rsquo;", "’");
            theString = theString.Replace("&ldquo;", "“");
            theString = theString.Replace("&rdquo;", "”");
            theString = theString.Replace("&nbsp;", " ");
            theString = theString.Replace("&hellip;", "…");
            theString = theString.Replace("&mdash;", "—");
            theString = theString.Replace("&middot;", "·");
            return theString;
        }
        /// <summary>
        /// 压缩文件
        /// </summary>
        /// <param name="FileToZip">要压缩的文件</param>
        /// <param name="ZipedFile">压缩之后的路径</param>
        /// <param name="Password">密码</param>
        /// <returns></returns>
        public static bool ToZipFile(string filesToZip, string zipedFile, string password)
        {
            string[] fileToZipList = filesToZip.Split(new string[1] { "*" }, StringSplitOptions.RemoveEmptyEntries);
            bool result = false;
            FileStream zipFile = null;
            ZipOutputStream zipStream = null;
            zipStream = new ZipOutputStream(File.Create(zipedFile));
            ZipEntry zipEntry = null;
            int count = 0;
            foreach (string abc in fileToZipList)
            {
                try
                {
                    if (File.Exists(abc))
                    {
                        zipFile = File.OpenRead(abc);
                        byte[] buffer = new byte[zipFile.Length];
                        zipFile.Read(buffer, 0, buffer.Length);
                        zipFile.Close();
                        if (!string.IsNullOrEmpty(password))
                        {
                            zipStream.Password = password;
                        }
                        zipEntry = new ZipEntry(Path.GetFileName(abc));
                        zipStream.PutNextEntry(zipEntry);
                        zipStream.SetLevel(6);
                        zipStream.Write(buffer, 0, buffer.Length);
                        count++;
                    }
                }
                catch
                {
                    result = false;
                    break;
                }
            }
            if (zipEntry != null)
            {
                zipEntry = null;
            }
            if (zipStream != null)
            {
                zipStream.Finish();
                zipStream.Close();
            }
            if (zipFile != null)
            {
                zipFile.Close();
                zipFile = null;
            }
            GC.Collect();
            GC.Collect(1);
            if (count > 0)
            {
                result = true;
            }
            return result;
        }
        /// <summary>
        /// 根据店铺ID 返回活动列表
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        //public static List<CouponsReceiveActivities> ReturnListActivities(int cityId, int shopId, long customerId, VAAppType appType)
        //{
        //    CustomEncourageManager CustomEncourageManager = new CustomEncourageManager();
        //    int companyId = 0;
        //    DataTable dtActivity = new DataTable();
        //    companyId = Common.ToInt32(Common.GetFieldValue("ShopInfo", "companyID", "shopID ='" + shopId + "'"));
        //    if (shopId != 0)
        //    {
        //        dtActivity = CustomEncourageManager.GetActivityDt(cityId, companyId, shopId);
        //    }
        //    else
        //    {
        //        dtActivity = CustomEncourageManager.GetActivityDt(cityId, companyId);
        //    }
        //    List<CouponsReceiveActivities> CouponsReceiveActivitieslist = new List<CouponsReceiveActivities>();

        //    CouponManager couponMan = new CouponManager();

        //    //id	couponsReceiveActivitiesName	companyId	shopId	couponId	activitiesValidStartTime	activitiesValidEndTime	couponsReceiveActivitiesDes	couponValidDayCount	status
        //    string imagePath2 = ConfigurationManager.AppSettings["Server"] + "/" + ConfigurationManager.AppSettings["ImagePath"];
        //    for (int i = 0; i < dtActivity.Rows.Count; i++)
        //    {
        //        CouponsReceiveActivities CouponsReceiveActivities = new CouponsReceiveActivities();
        //        CouponsReceiveActivities.activitiesId = Common.ToInt64(dtActivity.Rows[i]["id"]);
        //        CouponsReceiveActivities.couponsReceiveActivitiesName = Common.ToString(dtActivity.Rows[i]["couponsReceiveActivitiesName"]);
        //        CouponsReceiveActivities.companyId = Common.ToInt32(dtActivity.Rows[i]["companyId"]);

        //        CouponsReceiveActivities.couponId = Common.ToInt64(dtActivity.Rows[i]["couponId"]);
        //        CouponsReceiveActivities.activitiesValidStartTime = Common.ToSecondFrom1970(Common.ToDateTime(dtActivity.Rows[i]["activitiesValidStartTime"]));
        //        CouponsReceiveActivities.activitiesValidEndTime = Common.ToSecondFrom1970(Common.ToDateTime(dtActivity.Rows[i]["activitiesValidEndTime"]));
        //        CouponsReceiveActivities.couponValidDayCount = Common.ToDouble(dtActivity.Rows[i]["couponValidDayCount"]);
        //        DataTable dtcoupon = Common.GetDataTableFieldValue("CouponInfo", "couponType,discount,discountedAmount", "couponID ='" + CouponsReceiveActivities.couponId + "'");
        //        if (dtcoupon.Rows.Count > 0)
        //        {
        //            CouponsReceiveActivities.couponType = (VACouponType)Common.ToInt32(dtcoupon.Rows[0]["couponType"]);
        //            CouponsReceiveActivities.discount = Common.ToDouble(dtcoupon.Rows[0]["discount"]);
        //            CouponsReceiveActivities.discountedAmount = Common.ToDouble(dtcoupon.Rows[0]["discountedAmount"]);
        //        }
        //        DataTable dtCouponDish = couponMan.SelectdetailImageUrls(Common.ToInt64(dtActivity.Rows[i]["couponId"]));
        //        List<string> detailImageUrls = new List<string>();
        //        for (int j = 0; j < dtCouponDish.Rows.Count; j++)
        //        {
        //            string detailImageUrl = imagePath2 + Common.ToString(dtCouponDish.Rows[j]["couponImagePath"]);
        //            detailImageUrls.Add(detailImageUrl);
        //        }
        //        CouponsReceiveActivities.detailImageUrls = detailImageUrls;
        //        CouponsReceiveActivities.couponImage = imagePath2 + couponMan.SelectThumbnailImagePathForCoupon(Common.ToInt64(dtActivity.Rows[i]["couponId"]));
        //        CouponsReceiveActivities.couponDownloadPrice = Common.ToDouble(Common.GetFieldValue("CouponInfo", "couponDownloadPrice", "couponID ='" + Common.ToString(dtActivity.Rows[i]["couponId"]) + "'"));
        //        DataTable dtstatus = Common.GetDataTableFieldValue("CustomerConnCoupon", "customerConnCouponID", "couponID ='" + Common.ToString(dtActivity.Rows[i]["couponId"]) + "' and status ='1' and customerID='" + customerId + "' and EncourageID='" + CouponsReceiveActivities.activitiesId + "' and Encouragetype='" + (int)VAEncouragetype.FROM_COUPONSRECEIVEACTIVITIES + "'");
        //        int status = 1;//默认未使用
        //        if (dtstatus.Rows.Count > 0)
        //        {
        //            status = 2;
        //        }
        //        CouponsReceiveActivities.status = status;

        //        DataTable dtCouponShop = couponMan.SelectCouponConnShop(CouponsReceiveActivities.couponId, appType);
        //        CouponsReceiveActivities.applicableRestaurants = Common.FillRestaurant(dtCouponShop, VAShopDetailFillType.DEFAULT);
        //        CouponsReceiveActivities.companyName = Common.GetFieldValue("CompanyInfo", "companyName", "companyID ='" + CouponsReceiveActivities.companyId + "'");
        //        DataTable dtactivity = Common.GetDataTableFieldValue("CouponsReceiveActivities,CouponInfo", "couponValidStartTime,couponValidEndTime,timeType,couponValidDayCount", "id='" + CouponsReceiveActivities.activitiesId + "' and CouponsReceiveActivities.couponId=CouponInfo.couponID");
        //        if (dtactivity.Rows.Count > 0)
        //        {
        //            if (Common.ToInt32(dtactivity.Rows[0]["timeType"]) == (int)VACouponTimeType.ABSOLUTE_TIME)//绝对时间
        //            {
        //                CouponsReceiveActivities.usableStartDate = Common.ToSecondFrom1970(Common.ToDateTime(dtactivity.Rows[0]["couponValidStartTime"]));
        //                CouponsReceiveActivities.usableEndDate = Common.ToSecondFrom1970(Common.ToDateTime(dtactivity.Rows[0]["couponValidEndTime"]));
        //            }
        //            else
        //            {
        //                CouponsReceiveActivities.usableStartDate = Common.ToSecondFrom1970(System.DateTime.Now);
        //                CouponsReceiveActivities.usableEndDate = Common.ToSecondFrom1970(System.DateTime.Now.AddDays(Common.ToDouble(dtactivity.Rows[0]["couponValidDayCount"])));
        //            }
        //        }
        //        CouponsReceiveActivitieslist.Add(CouponsReceiveActivities);
        //    }
        //    return CouponsReceiveActivitieslist;

        //}

        /// <summary>
        /// 查询个人在该店铺的未使用的优惠卷
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <returns></returns>
        //public static List<VACouponStatic> QueryCouponStaticList(long CustomerID, int shopID, string orderInJson)
        //{

        //    List<PreOrderIn19dian> listOrderInfo = JsonOperate.JsonDeserialize<List<PreOrderIn19dian>>(orderInJson);
        //    List<PreShortOrderIn19dian> listShortOrderInfo = new List<PreShortOrderIn19dian>();
        //    var t = from p in listOrderInfo
        //            group p by p.dishPriceI18nId into g
        //            select new { g.Key, num = g.Sum(p => p.quantity) };
        //    foreach (var n in t)
        //    {
        //        PreShortOrderIn19dian shortOrder = new PreShortOrderIn19dian();
        //        shortOrder.dishPriceI18nId = n.Key;
        //        shortOrder.quantity = n.num;
        //        listShortOrderInfo.Add(shortOrder);
        //    }

        //    #region 拼接字符串
        //    // string alist = CommonPageOperate.SplicingListStr<PreOrderIn19dian>(listOrderInfo, "dishPriceI18nId");
        //    #endregion

        //    List<VACouponStatic> couponlist = new List<VACouponStatic>();
        //    CouponManager CM = new CouponManager();
        //    DateTime currentTime = System.DateTime.Now;
        //    //通用抵价卷
        //    DataTable dtcouponCommon = CM.CouponAllNOUseListByCommon(CustomerID, shopID, Common.ToInt32(VACouponType.DEDUCT_GENERAL_CAMPAIGN_TYPE), currentTime);
        //    //通用折扣卷
        //    DataTable dtcouponDiscount = CM.CouponAllNOUseListByCommon(CustomerID, shopID, Common.ToInt32(VACouponType.DISCOUNT_GENERAL_CAMPAIGN_TYPE), currentTime);

        //    //特定抵价卷
        //    DataTable dtcoupondish = CM.CouponMealNoUse(CustomerID, shopID, Common.ToInt32(VACouponType.DEDUCT_DISH_CAMPAIGN_TYPE), currentTime);
        //    DataTable dtcoupondishclone = DtReturnShowTable(dtcoupondish, listShortOrderInfo);

        //    //特定折扣卷
        //    DataTable dtcouponDishcount = CM.CouponMealNoUse(CustomerID, shopID, Common.ToInt32(VACouponType.DISCOUNT_DISH_CAMPAIGN_TYPE), currentTime);
        //    DataTable dtcouponDishcountclone = DtReturnShowTable(dtcouponDishcount, listShortOrderInfo);

        //    //套餐卷MEAL_SPECIAL_CAMPAIGN_TYPE

        //    DataTable dtcouponbymeal = CM.CouponMealNoUse(CustomerID, shopID, Common.ToInt32(VACouponType.MEAL_SPECIAL_CAMPAIGN_TYPE), currentTime);
        //    DataTable dtcouponbymealclone = DtReturnShowTable(dtcouponbymeal, listShortOrderInfo);

        //    dtcouponCommon.Merge(dtcoupondishclone);
        //    dtcouponCommon.Merge(dtcouponDiscount);
        //    dtcouponCommon.Merge(dtcouponbymealclone);
        //    dtcouponCommon.Merge(dtcouponDishcountclone);

        //    for (int n = 0; n < dtcouponCommon.Rows.Count; n++)
        //    {
        //        VACouponStatic couponli = new VACouponStatic();
        //        couponli.customerConnCouponID = Common.ToInt64(dtcouponCommon.Rows[n]["customerConnCouponID"]);
        //        couponli.CouponId = Common.ToInt64(dtcouponCommon.Rows[n]["couponID"]);
        //        couponli.CouponName = Common.ToString(dtcouponCommon.Rows[n]["couponName"]);
        //        couponli.CouponValue = 1;
        //        couponli.couponUseEndTime = Common.ToSecondFrom1970(Common.ToDateTime(dtcouponCommon.Rows[n]["couponValidEndTime"]));
        //        couponli.couponUseTimesBysame = Common.ToInt32(dtcouponCommon.Rows[n]["canUseNumberOnesOrder"]);
        //        couponli.ishistoryUse = false;
        //        couponlist.Add(couponli);
        //    }
        //    return couponlist;

        //}
        /// <summary>
        /// 返回能够显示的COUPON DT
        /// </summary>
        /// <param name="dtcoupon"></param>
        /// <param name="listOrderInfo"></param>
        /// <returns></returns>
        //public static DataTable DtReturnShowTable(DataTable dtcoupon, List<PreShortOrderIn19dian> listOrderInfo)
        //{
        //    CouponManager CM = new CouponManager();
        //    DataTable dtcouponbyclone = dtcoupon.Clone();
        //    foreach (DataRow dr in dtcoupon.Rows)
        //    {
        //        DataTable dtcoupondishbymeal = CM.CoupondishbyMeal(Common.ToInt32(dr["couponID"]));//当前优惠卷菜信息
        //        bool isuseok = JustIsshowOk(dtcoupondishbymeal, listOrderInfo);
        //        if (isuseok)
        //        {
        //            dtcouponbyclone.Rows.Add(dr.ItemArray);
        //        }

        //    }
        //    return dtcouponbyclone;

        //}
        /// <summary>
        /// 判断该优惠卷 在该用户点单中是否能显示
        /// </summary>
        /// <param name="dtcoupondishbymeal"></param>
        /// <param name="listOrderInfo"></param>
        /// <returns></returns>
        public static bool JustIsshowOk(DataTable dtcoupondishbymeal, List<PreShortOrderIn19dian> listOrderInfo)
        {
            for (int i = 0; i < dtcoupondishbymeal.Rows.Count; i++)
            {
                bool isordercanuse = IsOrderCanUse(Common.ToInt32(dtcoupondishbymeal.Rows[i]["dishPriceI18nID"]), Common.ToInt32(dtcoupondishbymeal.Rows[i]["dishQuantity"]), listOrderInfo);

                if (isordercanuse == false)
                {
                    return false;
                }
            }
            return true;

        }
        /// <summary>
        /// 该优惠卷所需某菜ID，数量在该用户菜单中是否满足
        /// </summary>
        /// <param name="dishPriceI18nId"></param>
        /// <param name="quantity"></param>
        /// <param name="listOrderInfo"></param>
        /// <returns></returns>
        public static bool IsOrderCanUse(int dishPriceI18nId, int quantity, List<PreShortOrderIn19dian> listOrderInfo)
        {
            for (int i = 0; i < listOrderInfo.Count; i++)
            {
                if (listOrderInfo[i].dishPriceI18nId == dishPriceI18nId && listOrderInfo[i].quantity >= quantity)
                {
                    return true;
                }
            }
            return false;
        }
        //测试全局变量
        //public static string Bar
        //{
        //    get
        //    {
        //        return HttpContext.Current.Application["Bar"] as string;
        //    }
        //    set
        //    {
        //        HttpContext.Current.Application["Bar"] = value;
        //    }
        //}
        /// <summary>
        /// 根据某个字段过滤掉重复记录
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="FieldName"></param>
        /// <returns></returns>
        public static DataTable SelectDistinctByField(DataTable dtSource, string FieldName)
        {
            DataTable returnDt = new DataTable();
            returnDt = dtSource.Copy();//将原DataTable复制一个新的   
            DataRow[] datarows = returnDt.Select("", FieldName);//将DataTable按指定的字段排序   
            object LastValue = null;
            for (int i = 0; i < datarows.Length; i++)
            {
                if ((LastValue == null) || (!(ColumnEqual(LastValue, datarows[i][FieldName]))))
                {
                    LastValue = datarows[i][FieldName];
                    continue;
                }
                datarows[i].Delete();
            }
            return returnDt;
        }
        private static bool ColumnEqual(object A, object B)
        {
            //   Compares   two   values   to   see   if   they   are   equal.   Also   compares   DBNULL.Value.   
            //   Note:   If   your   DataTable   contains   object   fields,   then   you   must   extend   this   
            //   function   to   handle   them   in   a   meaningful   way   if   you   intend   to   group   on   them.  
            if (A == DBNull.Value && B == DBNull.Value)   //     both   are   DBNull.Value   
                return true;
            if (A == DBNull.Value || B == DBNull.Value)   //     only   one   is   DBNull.Value   
                return false;
            return (A.Equals(B));     //   value   type   standard   comparison   
        }

        /// <summary>
        /// 判断是否这些优惠卷达到使用次数
        /// </summary>
        /// <param name="couponList"></param>
        /// <returns></returns>
        //public static bool QueryIsCouponCanUse(List<long> couponList)
        //{
        //    #region 拼接字符串
        //    //string alist = "(";
        //    //for (int i = 0; i < couponList.Count; i++)
        //    //{
        //    //    if (i != couponList.Count - 1)
        //    //    {
        //    //        alist += "'" + couponList[i] + "',";
        //    //    }
        //    //    else
        //    //    {
        //    //        alist += "'" + couponList[i] + "'";
        //    //    }

        //    //}
        //    //alist += ")";
        //    string alist = CommonPageOperate.SplicingListStr<long>(couponList, "");
        //    #endregion
        //    CouponManager CM = new CouponManager();
        //    DataTable CouponUsedt = CM.CouponCanUseDT(alist);

        //    for (int n = 0; n < CouponUsedt.Rows.Count; n++)
        //    {
        //        if (Common.ToInt32(CouponUsedt.Rows[n]["cnt"]) > Common.ToInt32(CouponUsedt.Rows[n]["canUseNumberOnesOrder"]))
        //        {
        //            return false;
        //        }
        //    }
        //    return true;
        //}

        //public static bool QueryIsCouponUsedbyAnybody(List<long> couponList)
        //{
        //    #region 拼接字符串
        //    string alist = CommonPageOperate.SplicingListStr<long>(couponList, "");
        //    #endregion
        //    CouponManager CM = new CouponManager();
        //    DataTable dtisused = new DataTable();
        //    dtisused = CM.GetCouponUsebyanybody(alist);

        //    if (dtisused.Rows.Count > 0)
        //    {
        //        return false;
        //    }
        //    else
        //    {
        //        return true;
        //    }

        //}

        /// <summary>
        /// 获取表中某字段的值
        /// </summary>
        /// <param name="tblName">表名</param>
        /// <param name="field">字段名</param>
        /// <param name="filter">过滤条件</param>
        /// <returns></returns>
        public static string GetFieldValue(string tblName, string field, string filter)
        {
            string sql = "select " + field + " from " + tblName;
            if (filter != "")
            {
                sql += " where " + filter;
            }
            try
            {
                return Common.ToString(CommonManager.GetFieldValue(sql));

            }
            catch
            {
                return "";
            }
        }
        public static DataTable GetDataTableFieldValue(string tblName, string field, string filter)
        {
            string sql = "select " + field + " from " + tblName;
            DataTable dt = new DataTable();
            if (filter != "")
            {
                sql += " where " + filter;
            }
            try
            {
                dt = CommonManager.GetDataTableFieldValue(sql);
                return dt;

            }
            catch
            {
                return dt;
            }

        }
        public static DataTable GetDataTableFieldValueOrderby(string tblName, string field, string filter, string orderby)
        {
            string sql = "select " + field + " from " + tblName;
            DataTable dt = new DataTable();
            if (filter != "")
            {
                sql += " where " + filter;
            }
            if (orderby != "")
            {
                sql += " order by " + orderby;
            }
            try
            {
                dt = CommonManager.GetDataTableFieldValue(sql);
                return dt;

            }
            catch
            {
                return dt;
            }

        }
        /// <summary>
        /// 这个方法用来查出最大公约数
        /// </summary>
        /// <param name="n1"></param>
        /// <param name="n2"></param>
        /// <returns></returns>
        public static float maxGongYueShu(int n1, int n2)
        {
            int temp = Math.Max(n1, n2);
            n2 = Math.Min(n1, n2);//n2中存放两个数中最小的
            n1 = temp;//n1中存放两个数中最大的
            while (n2 != 0)
            {
                n1 = n1 > n2 ? n1 : n2;//使n1中的数大于n2中的数
                int m = n1 % n2;
                n1 = n2;
                n2 = m;
            }
            return n1;
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="pg"></param>
        /// <returns></returns>
        public static DataTable DbPager(PaginationPager pg)
        {
            return CommonManager.DbPager(pg);
        }
        /// <summary>
        /// 更新字段
        /// </summary>
        /// <param name="tblName"></param>
        /// <param name="field"></param>
        /// <param name="fValue"></param>
        /// <param name="cFiled"></param>
        /// <param name="cValue"></param>
        /// <returns></returns>
        public static int UpdateStatus(string tblName, string field, string fValue, string cFiled, string cValue)
        {
            if (tblName.Length == 0 || field.Length == 0 || cFiled.Length == 0)
            {
                return -1;
            }
            string sql = string.Format("update {0} set {1}='{2}' where {3}='{4}'", tblName, cFiled, cValue, field, fValue);

            return CommonManager.UpdateStatus(sql);
        }
        /// <summary>
        /// 退款日志
        /// </summary>
        /// <param name="tblName"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        public static bool InsertRefundData(long customerId, double refundsum, long preOrder19dianId, string note)
        {
            return CommonManager.InsertRefundData(customerId, refundsum, preOrder19dianId, note);
        }
        /// <summary>
        /// 向推送数据库中加入记录
        /// </summary>
        /// <param name="notificationObj"></param>
        /// <returns></returns>
        public static void AddNotificationRecord(object notificationObj)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                //插入推送记录
                NotificationRecord notificationRecord = (NotificationRecord)notificationObj;
                NotificationRecordManager notificationRecordMan = new NotificationRecordManager();
                long flagInsertNotificationRecord = notificationRecordMan.InsertNotificationRecord(notificationRecord);
                if (flagInsertNotificationRecord > 0)
                {
                    scope.Complete();
                }
            }
        }
        /// <summary>
        /// 插入员工操作日志信息记录
        /// </summary>
        /// <param name="employeeOperateLogInfo"></param>
        /// <returns></returns>
        public static void InsertEmployeeOperateLog(object employeeOperateLogInfo)
        {
            CommonManager.InsertEmployeeOperateLogInfo((EmployeeOperateLogInfo)employeeOperateLogInfo);
        }
        /// <summary>
        /// 记录员工操作日志
        /// </summary>
        /// <param name="pageType">页面Type，从枚举VAEmployeeOperateLogOperatePageType中获取</param>
        /// <param name="operateType">操作Type，从枚举VAEmployeeOperateLogOperateType中获取</param>
        /// <param name="operateDes">页面操作描述，自己整合</param>
        public static void RecordEmployeeOperateLog(int pageType, int operateType, string operateDes)
        {
            try
            {
                EmployeeOperateLogInfo employeeOperateLogInfo = new EmployeeOperateLogInfo();
                employeeOperateLogInfo.employeeId = ((VAEmployeeLoginResponse)HttpContext.Current.Session["UserInfo"]).employeeID;
                employeeOperateLogInfo.employeeName = Common.GetFieldValue("EmployeeInfo", "EmployeeFirstName+EmployeeLastName employeeName", "EmployeeID='" + employeeOperateLogInfo.employeeId + "'");
                employeeOperateLogInfo.pageType = pageType;
                employeeOperateLogInfo.operateType = operateType;
                employeeOperateLogInfo.operateTime = DateTime.Now;
                employeeOperateLogInfo.operateDes = operateDes;
                //开启单独线程插入数据
                ParameterizedThreadStart threadstart = new ParameterizedThreadStart(InsertEmployeeOperateLog);
                Thread thread = new Thread(threadstart);
                thread.IsBackground = true;
                thread.Start(employeeOperateLogInfo);
            }
            catch
            {
            }
        }
        /// <summary>
        /// 收银宝记录员工操作日志
        /// </summary>
        /// <param name="pageType">页面Type，从枚举VAEmployeeOperateLogOperatePageType中获取</param>
        /// <param name="operateType">操作Type，从枚举VAEmployeeOperateLogOperateType中获取</param>
        /// <param name="operateDes">页面操作描述，自己整合</param>
        public static void RecordEmployeeOperateLogBySYB(int pageType, int operateType, string operateDes)
        {
            try
            {
                EmployeeOperateLogInfo employeeOperateLogInfo = new EmployeeOperateLogInfo();
                employeeOperateLogInfo.employeeId = ((VAEmployeeLoginResponse)HttpContext.Current.Session["MerchantsTreasureUserInfo"]).employeeID;
                employeeOperateLogInfo.employeeName = Common.GetFieldValue("EmployeeInfo", "EmployeeFirstName+EmployeeLastName employeeName", "EmployeeID='" + employeeOperateLogInfo.employeeId + "'");
                employeeOperateLogInfo.pageType = pageType;
                employeeOperateLogInfo.operateType = operateType;
                employeeOperateLogInfo.operateTime = DateTime.Now;
                employeeOperateLogInfo.operateDes = operateDes;
                //开启单独线程插入数据
                ParameterizedThreadStart threadstart = new ParameterizedThreadStart(InsertEmployeeOperateLog);
                Thread thread = new Thread(threadstart);
                thread.IsBackground = true;
                thread.Start(employeeOperateLogInfo);
            }
            catch
            {
            }
        }

        /// <summary>
        /// 是否为时间格式
        /// </summary>
        /// <param name="Object">要判断的对象</param>
        /// <param name="isTrue">返回是否转换成功</param>
        /// <returns>DateTime</returns>
        public static bool IsTime(object Object)
        {
            bool val = false;
            try
            {
                if (IsNull(Object)) return false;
                DateTime.Parse(Object.ToString());
                val = true;
            }
            catch
            {
                return false;
            }
            return val;
        }

        /// <summary>
        /// 对象是否为空
        /// 为空返回 false
        /// 不为空返回 true
        /// </summary>
        /// <param name="Object">要判断的对象</param>
        /// <returns>bool值</returns>
        public static bool IsNull(object Object) { return IsNull(Object, false); }
        /// <summary>
        /// 对象是否为空
        /// 为空返回 false
        /// 不为空返回 true
        /// </summary>
        /// <param name="Object">要判断的对象</param>
        /// <param name="IsRemoveSpace">是否去除空格</param>
        /// <returns>bool值</returns>
        public static bool IsNull(object Object, bool IsRemoveSpace)
        {
            if (Object == null) return true;
            string Objects = Object.ToString();
            if (Objects == "") return true;
            if (IsRemoveSpace)
            {
                if (Objects.Replace(" ", "") == "") return true;
                if (Objects.Replace("　", "") == "") return true;
            }
            return false;
        }
        /// <summary>
        /// 修改用户在平台的vip等级20140313
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="preOrderTotalQuantity"></param>
        /// <param name="currentPlatformVipGrade"></param>
        /// <param name="serverUxianPriceSum"></param>
        /// <param name="cumulativeAmount"></param>
        /// <param name="refundAll"></param>
        /// <returns></returns>
        public static bool ModifyUserPlatVip(long customerId, int preOrderTotalQuantity, int currentPlatformVipGrade
            , double serverUxianPriceSum, double cumulativeAmount, bool refundAll)
        {
            CustomerManager customerMan = new CustomerManager();
            int userVipGradeToBe = 0;
            ViewallocInfoManager viewallocMan = new ViewallocInfoManager();
            DataTable dtVAVIP = viewallocMan.SelectViewAllocPlatformVipInfo();
            DataView dvVAVIP = dtVAVIP.DefaultView;
            dvVAVIP.Sort = "consumptionLevel ASC";
            if (dtVAVIP.Rows.Count > 0)
            {
                if (Common.ToBool(dtVAVIP.Rows[0]["isMonetary"]))
                {
                    //表示平台是按照消费金额进行升级
                    for (int i = 0; i < dvVAVIP.Count; i++)
                    {
                        if (cumulativeAmount >= Common.ToDouble(dvVAVIP[i]["consumptionLevel"]))
                        {
                            userVipGradeToBe = Common.ToInt32(dvVAVIP[i]["id"]);
                            continue;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                else
                {
                    //表示平台是按照消费次数进行升级
                    for (int i = 0; i < dvVAVIP.Count; i++)
                    {
                        if (preOrderTotalQuantity + 1 >= Common.ToInt32(dvVAVIP[i]["consumptionLevel"]))
                        {
                            userVipGradeToBe = Common.ToInt32(dvVAVIP[i]["id"]);
                            continue;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
            else
            {
                //未找到相关VIP信息，理论上不存在该情况，无需处理
            }
            bool updateCustomerPartInfo = false;
            if (userVipGradeToBe > 0)
            {
                if (serverUxianPriceSum < 0)//退款操作
                {
                    if (refundAll)//全额退款
                    {
                        updateCustomerPartInfo = customerMan.UpdateCustomerPartInfo(serverUxianPriceSum, -1, userVipGradeToBe, customerId);
                    }
                    else
                    {
                        updateCustomerPartInfo = customerMan.UpdateCustomerPartInfo(serverUxianPriceSum, 0, userVipGradeToBe, customerId);
                    }
                }
                else//支付点单操作
                {
                    updateCustomerPartInfo = customerMan.UpdateCustomerPartInfo(serverUxianPriceSum, 1, userVipGradeToBe, customerId);
                }
            }
            else//消费金额不够升级，但是需要修改用户消费次数和消费金额，VIP等级更新为原来的平台等级
            {
                // updateCustomerPartInfo = true;
                updateCustomerPartInfo = customerMan.UpdateCustomerPartInfo(serverUxianPriceSum, 1, currentPlatformVipGrade, customerId);
            }
            return updateCustomerPartInfo;
        }
        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="mob">手机号码(多个用英文逗号,分隔,最多200个，超过将被忽略)</param>
        /// <param name="msg">短信内容(60-70个字，超过自己做下循环)</param>
        /// <returns></returns>
        public static bool SendMessageBySms(string mob, string msg)
        {
            string smsType = WebConfig.SmsType;
            bool result = false;
            switch (smsType)
            {
                case "Yimei":
                    {
                        string[] mobList = mob.Split(new string[1] { "," }, StringSplitOptions.RemoveEmptyEntries);
                        SMSYimei.SMSYimeiVoice smsYimeiVoice = new SMSYimei.SMSYimeiVoice();
                        DateTime currentTime = DateTime.Now;
                        long smsId = Common.ToInt64(Common.ToSecondFrom1970(currentTime).ToString() + Common.randomStrAndNum(5));
                        if (smsYimeiVoice.SendMessage("", mobList, msg, smsId) == VAResult.VA_OK)
                        {
                            result = true;
                        }
                        else
                        {
                            result = false;
                        }
                    }
                    break;
                case "Jianzhou":
                    {
                        SMS.SMSJianzhou jianzhouClient = new SMS.SMSJianzhou();
                        if (jianzhouClient.SendMessage(mob.Replace(",", ";"), msg) == VAResult.VA_OK)
                        {
                            result = true;
                        }
                        else
                        {
                            result = false;
                        }
                    }
                    break;
                case "Guodu":
                default:
                    {
                        string smsUid = WebConfig.SmsUid.Trim();
                        string smsPwd = WebConfig.SmsPwd.Trim();
                        if (SMSGuoDu.SMSGuoDu.SentMessage(smsUid, smsPwd, mob, msg) == VAResult.VA_OK)//国都短信
                        {
                            result = true;
                        }
                        else
                        {
                            result = false;
                        }
                    }
                    break;
            }
            return result;
        }
        ///<summary>
        ///直接删除指定目录下的所有文件及文件夹(保留目录)
        ///2013-7-19 wangcheng 创建于CompanyAdd.cs文件
        ///20140402 wangc 移动
        ///</summary>
        public static bool DeleteDir(string strPath)
        {
            try
            {
                strPath = @strPath.Trim().ToString();
                // 判断文件夹是否存在
                if (System.IO.Directory.Exists(strPath))
                { // 获得文件夹数组
                    string[] strDirs = System.IO.Directory.GetDirectories(strPath);
                    string[] strFiles = System.IO.Directory.GetFiles(strPath);
                    // 遍历所有子文件夹
                    foreach (string strFile in strFiles)
                    {
                        System.IO.File.Delete(strFile); // 删除文件夹
                    }
                    // 遍历所有文件
                    foreach (string strdir in strDirs)
                    {
                        System.IO.Directory.Delete(strdir, true);// 删除文件
                    }
                }
                return true; // 成功
            }
            catch (Exception Exp)
            {
                // 异常信息
                System.Diagnostics.Debug.Write(Exp.Message.ToString());
                return false;// 失败
            }
        }
        /// <summary>
        /// 发送语音短信
        /// 除签名外的内容必须是4-6位的数字
        /// </summary>
        /// <param name="mob"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static bool SendVoiceMessage(string[] mob, string msg)
        {
            SMSYimei.SMSYimeiVoice smsYimeiVoice = new SMSYimei.SMSYimeiVoice();
            DateTime currentTime = DateTime.Now;
            long smsId = Common.ToInt64(Common.ToSecondFrom1970(currentTime).ToString() + Common.randomStrAndNum(5));
            if (smsYimeiVoice.SendVoice("", mob, msg, smsId) == VAResult.VA_OK)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #region 悠先点菜版本判断
        /// <summary>
        /// 201408月份悠先点菜（该方法仅仅在当前版本调用）判断客户端当前版本是否是最新版本（true：是最新版本，false：是老版本）
        /// 备注：该方法仅仅用于判断201408月份上线悠先点菜版本，后期该版本以后版本如需做版本兼容不可调用该方法判断当前版本是否为最新版本，需重载当前方法并传入新的版本号
        /// </summary>
        /// <param name="apptype">悠先点菜客户端类型</param>
        /// <param name="currectBuild">当前版本号</param>
        /// <returns></returns>
        public static bool CheckLatestBuild_August(VAAppType apptype, string currectBuild)
        {
            string[] strs = new SystemConfigCacheLogic().GetAvailableRedEnvelopeBuildOfCache();
            if (apptype == VAAppType.ANDROID && string.Compare(currectBuild, strs[0]) < 0)
            {
                return false;
            }
            if (apptype == VAAppType.IPHONE && string.Compare(currectBuild, strs[1]) < 0)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 判断客户端当前版本是否是最新版本（true：是最新版本，false：是老版本）
        /// 该方法仅仅用于判断2014年11月初上线悠先点菜版本
        /// </summary>
        /// <param name="apptype">悠先点菜客户端类型</param>
        /// <param name="currectBuild">当前版本号</param>
        /// <returns></returns>
        public static bool CheckLatestBuild_November(VAAppType apptype, string currectBuild)
        {
            if (apptype == VAAppType.ANDROID && string.Compare(currectBuild, "6.4.9") < 0)
            {
                return false;
            }
            if (apptype == VAAppType.IPHONE && string.Compare(currectBuild, "2014.10.31") < 0)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 判断客户端当前版本是否是最新版本（true：是最新版本，false：是老版本）
        /// 该方法仅仅用于判断2014年12月10日上线悠先点菜版本
        /// </summary>
        /// <param name="apptype">悠先点菜客户端类型</param>
        /// <param name="currectBuild">当前版本号</param>
        /// <returns></returns>
        public static bool CheckLatestBuild_December(VAAppType apptype, string currectBuild)
        {
            if (apptype == VAAppType.ANDROID && string.Compare(currectBuild, "6.5.2") < 0)
            {
                return false;
            }
            if (apptype == VAAppType.IPHONE && string.Compare(currectBuild, "2014.12.10") < 0)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 判断客户端当前版本是否是最新版本（true：是最新版本，false：是老版本）
        /// 该方法仅仅用于判断2015年1月15日上线悠先点菜版本
        /// </summary>
        /// <param name="apptype">悠先点菜客户端类型</param>
        /// <param name="currectBuild">当前版本号</param>
        /// <returns></returns>
        public static bool CheckLatestBuild_January(VAAppType apptype, string currectBuild)
        {
            if (apptype == VAAppType.ANDROID && string.Compare(currectBuild, "6.6.0") < 0)
            {
                return false;
            }
            if (apptype == VAAppType.IPHONE && string.Compare(currectBuild, "2015.01.28") < 0)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 判断客户端当前版本是否是最新版本（true：是最新版本，false：是老版本）
        /// 该方法仅仅用于判断2015年2月2日上线悠先点菜版本
        /// </summary>
        /// <param name="apptype">悠先点菜客户端类型</param>
        /// <param name="currectBuild">当前版本号</param>
        /// <returns></returns>
        public static bool CheckLatestBuild_February(VAAppType apptype, string currectBuild)
        {
            if (apptype == VAAppType.ANDROID && string.Compare(currectBuild, "6.6.1") < 0)
            {
                return false;
            }
            if (apptype == VAAppType.IPHONE && string.Compare(currectBuild, "2015.02.11") < 0)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 判断客户端当前版本是否是最新版本（true：是最新版本，false：是老版本）
        /// 该方法仅仅用于判断2015年4月22日上线悠先点菜版本
        /// </summary>
        /// <param name="apptype">悠先点菜客户端类型</param>
        /// <param name="currectBuild">当前版本号</param>
        /// <returns></returns>
        public static bool CheckLatestBuild_201504(VAAppType apptype, string currectBuild)
        {
            if (apptype == VAAppType.ANDROID && string.Compare(currectBuild, "6.6.3") < 0)
            {
                return false;
            }
            if (apptype == VAAppType.IPHONE && string.Compare(currectBuild, "2015.04.21") < 0)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 判断客户端当前版本是否是最新版本（true：是最新版本，false：是老版本）
        /// 该方法仅仅用于判断2015年6月19日上线悠先点菜版本
        /// </summary>
        /// <param name="apptype">悠先点菜客户端类型</param>
        /// <param name="currectBuild">当前版本号</param>
        /// <returns></returns>
        public static bool CheckLatestBuild_201506(VAAppType apptype, string currectBuild)
        {
            if (apptype == VAAppType.ANDROID && string.Compare(currectBuild, "6.6.6") < 0)
            {
                return false;
            }
            if (apptype == VAAppType.IPHONE && string.Compare(currectBuild, "2015.06.15") < 0)
            {
                return false;
            }
            return true;
        }




        //------------------------------------------------------------------------------------------------

        /// <summary>
        /// 判断客户端当前版本是否是最新版本（true：是最新版本，false：是老版本）
        /// 该方法仅仅用于判断2015年7月1日上线悠先点菜版本
        /// </summary>
        /// <param name="apptype">悠先点菜客户端类型</param>
        /// <param name="currectBuild">当前版本号</param>
        /// <returns></returns>
        public static bool CheckLatestBuild_201507(VAAppType apptype, string currectBuild)
        {
            if (apptype == VAAppType.ANDROID && string.Compare(currectBuild, "6.6.8") < 0)
            {
                return false;
            }
            if (apptype == VAAppType.IPHONE && string.Compare(currectBuild, "2015.07.01") < 0)
            {
                return false;
            }
            return true;
        }       

        /// <summary>
        /// 客户端当前版本 与 启用红包过期版本 比较
        /// 判断客户端当前版本是否是最新版本（true：是最新版本，false：是老版本）
        /// 该方法仅仅用于判断2015年8月初上线悠先点菜版本
        /// </summary>
        /// <param name="apptype">悠先点菜客户端类型</param>
        /// <param name="currectBuild">当前版本号</param>
        /// <returns></returns>
        public static bool CheckLatestBuild_201508(VAAppType apptype, string currectBuild)
        {
            if (apptype == VAAppType.ANDROID && string.Compare(currectBuild, "6.6.9") < 0)
            {
                return false;
            }
            if (apptype == VAAppType.IPHONE && string.Compare(currectBuild, "2015.07.21") < 0)
            {
                return false;
            }
            return true;
        }

        //------------------------------------------------------------------------------------------------



        /// <summary>
        /// 客户端当前版本 与 启用红包过期版本 比较
        /// 高于等于设置版本，则启用，返回true，反之false
        /// </summary>
        /// <param name="apptype"></param>
        /// <param name="currectBuild"></param>
        /// <returns></returns>
        public static bool CheckExpireRedEnvelopeBuild_February(VAAppType apptype, string currectBuild)
        {
            string[] strs = new SystemConfigCacheLogic().GetExpireRedEnvelopeBuildOfCahce();
            if (apptype == VAAppType.ANDROID && string.Compare(currectBuild, strs[0]) < 0)
            {
                return false;
            }
            if (apptype == VAAppType.IPHONE && string.Compare(currectBuild, strs[1]) < 0)
            {
                return false;
            }
            return true;
        }
        #endregion

        #region 悠先服务版本判断
        public static bool UXServiceCheckLatestBuild_March(VAServiceType aerviceType, string currectBuild)
        {
            if (aerviceType == VAServiceType.ANDROID && string.Compare(currectBuild, "2.3.3") < 0)
            {
                return false;
            }
            if (aerviceType == VAServiceType.IPHONE && string.Compare(currectBuild, "2.3.4") < 0)
            {
                return false;
            }
            return true;
        }

        public static bool UXServiceCheckLatestBuild_201507(VAServiceType aerviceType, string currectBuild)
        {
            if (aerviceType == VAServiceType.ANDROID && string.Compare(currectBuild, "2.3.4") < 0)
            {
                return false;
            }
            if (aerviceType == VAServiceType.IPHONE && string.Compare(currectBuild, "2.3.5") < 0)
            {
                return false;
            }
            return true;
        }
        #endregion

        /// <summary>
        /// 获取指定周数的开始日期和结束日期，开始日期为周日
        /// </summary>
        /// <param name="year">年份</param>
        /// <param name="index">周数</param>
        /// <param name="first">当此方法返回时，则包含参数 year 和 index 指定的周的开始日期的 System.DateTime 值；如果失败，则为 System.DateTime.MinValue。</param>
        /// <param name="last">当此方法返回时，则包含参数 year 和 index 指定的周的结束日期的 System.DateTime 值；如果失败，则为 System.DateTime.MinValue。</param>
        /// <returns></returns>
        public static bool GetDaysOfWeeks(int year, int index, out DateTime first, out DateTime last)
        {
            first = DateTime.MinValue;
            last = DateTime.MinValue;
            if (year < 1700 || year > 9999 || index < 1 || index > 53)
            {
                return false;//"年份,周次超限"
            }
            DateTime startDay = new DateTime(year, 1, 1);  //该年第一天
            DateTime endDay = new DateTime(year + 1, 1, 1).AddMilliseconds(-1);
            int dayOfWeek = 0;
            if (Common.ToInt32(startDay.DayOfWeek.ToString("d")) > 0) { dayOfWeek = Common.ToInt32(startDay.DayOfWeek.ToString("d")); }
            if (dayOfWeek == 7) { dayOfWeek = 0; }
            if (index == 1)
            {
                first = startDay;
                if (dayOfWeek == 6)
                {
                    last = first;
                }
                else
                {
                    last = startDay.AddDays((6 - dayOfWeek));
                }
            }
            else
            {
                first = startDay.AddDays((7 - dayOfWeek) + (index - 2) * 7);
                last = first.AddDays(6);
                if (last > endDay) { last = endDay; }
            }
            if (first > endDay) { return false; }
            first = Common.ToDateTime(first.ToString("yyyy/MM/dd") + " 00:00:00");
            last = Common.ToDateTime(last.ToString("yyyy/MM/dd") + " 23:59:59");
            return true;
        }
        /// <summary>
        /// 获取当前时间的周次
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static int GetWeekOfYear(DateTime dt)
        {
            GregorianCalendar gc = new GregorianCalendar();
            int weekOfYear = gc.GetWeekOfYear(dt, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
            return weekOfYear;
        }

        /// <summary>
        /// 随机重新组成泛型结构（保留原泛型结构）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="inputList"></param>
        /// <returns></returns>
        public static List<T> GetRandomList<T>(List<T> inputList)
        {
            T[] copyArray = new T[inputList.Count];
            inputList.CopyTo(copyArray);
            List<T> copyList = new List<T>();
            copyList.AddRange(copyArray);
            List<T> outputList = new List<T>();
            Random rd = new Random(DateTime.Now.Millisecond);
            while (copyList.Count > 0)
            {
                int rdIndex = rd.Next(0, copyList.Count - 1);
                T remove = copyList[rdIndex];
                copyList.Remove(remove);
                outputList.Add(remove);
            }
            return outputList;
        }

        public static DataTable ListToDataTable<T>(List<T> list)
        {
            Type elementType = typeof(T);
            var t = new DataTable();
            elementType.GetProperties().ToList().ForEach(propInfo => t.Columns.Add(propInfo.Name, Nullable.GetUnderlyingType(propInfo.PropertyType) ?? propInfo.PropertyType));
            foreach (T item in list)
            {
                var row = t.NewRow();
                elementType.GetProperties().ToList().ForEach(propInfo => row[propInfo.Name] = propInfo.GetValue(item, null) ?? DBNull.Value);
                t.Rows.Add(row);
            }
            return t;
        }
        /// 根据公历获取农历日期
        ///</summary>
        ///<param name="datetime">公历日期</param>
        ///<returns>农历日期</returns>
        public static string GetChineseDateTime(DateTime datetime)
        {
            var clc = new System.Globalization.ChineseLunisolarCalendar();
            var year = clc.GetYear(datetime);
            var month = clc.GetMonth(datetime);
            var day = clc.GetDayOfMonth(datetime);
            var leapMonth = clc.GetLeapMonth(year);
            return string.Format("{0}{1} {2}年{3}{4}月{5}{6}"
                , "甲乙丙丁戊己庚辛壬癸"[(year - 4) % 10]
                , "子丑寅卯辰巳午未申酉戌亥"[(year - 4) % 12]
                , "鼠牛虎兔龙蛇马羊猴鸡狗猪"[(year - 4) % 12]
                , month == leapMonth ? "润" : ""
                , "无正二三四五六七八九十冬腊"[leapMonth > 0 && leapMonth <= month ? month - 1 : month]
                , "初十廿三"[day / 10]
                , "日一二三四五六七八九"[day % 10]
            );
        }

        public static string GetChineseDateTimeForMeal(DateTime datetime)
        {
            var clc = new System.Globalization.ChineseLunisolarCalendar();
            var year = clc.GetYear(datetime);
            var month = clc.GetMonth(datetime);
            var day = clc.GetDayOfMonth(datetime);
            var leapMonth = clc.GetLeapMonth(year);
            return string.Format("{0}{1}"
                , "初十廿三"[day / 10]
                , "十一二三四五六七八九"[day % 10]
            );
        }

        public static T GetObjectFromContent<T>() where T : class, new()
        {
            T value = new T();
            return value;
        }

        public static string lastElecChequeNo = null;
        public static int lastElecChequeNumber = 0;

        /// <summary>
        /// 银企直联用到的流水号
        /// </summary>
        public static string GetElecChequeNo
        {
            get
            {
                var elecChequeNo = DateTime.Now.ToString("yyMMddHHmmssfff");
                int number = 0;
                while (elecChequeNo == lastElecChequeNo && number++ < lastElecChequeNumber)
                {
                    number = lastElecChequeNumber;
                    if (number == 9)
                    {
                        Thread.Sleep(1);
                        elecChequeNo = DateTime.Now.ToString("yyMMddHHmmssfff");
                        number = 0;
                    }
                }
                lastElecChequeNo = elecChequeNo;
                lastElecChequeNumber = number;
                return elecChequeNo + number.ToString();
            }
        }

        /// <summary>
        /// 以顺序guid
        /// </summary>
        /// <returns></returns>
        public static Guid CreateCombGuid()
        {
            byte[] guidArray = Guid.NewGuid().ToByteArray();

            DateTime baseDate = new DateTime(1900, 1, 1);
            DateTime now = DateTime.Now;

            //Get the days and milliseconds which will be used to build the byte string 
            TimeSpan days = new TimeSpan(now.Ticks - baseDate.Ticks);
            TimeSpan msecs = now.TimeOfDay;

            //Convert to a byte array 
            //Note that SQL Server is accurate to 1/300th of a millisecond so we divide by 3.333333 
            byte[] daysArray = BitConverter.GetBytes(days.Days);
            byte[] msecsArray = BitConverter.GetBytes((long)(msecs.TotalMilliseconds / 3.333333));

            //Reverse the bytes to match SQL Servers ordering 
            Array.Reverse(daysArray);
            Array.Reverse(msecsArray);

            //Copy the bytes into the guid 
            Array.Copy(daysArray, daysArray.Length - 2, guidArray, guidArray.Length - 6, 2);
            Array.Copy(msecsArray, msecsArray.Length - 4, guidArray, guidArray.Length - 4, 4);

            return new Guid(guidArray);
        }

        /// <summary>
        /// 是否为时间格式
        /// </summary>
        /// <param name="Object">要判断的对象</param>
        /// <param name="isTrue">返回是否转换成功</param>
        /// <returns>DateTime</returns>
        public static bool IsPhoneNumber(string PhoneNumber)
        {
           return Regex.IsMatch(PhoneNumber,@"^1[358]\d{9}$");
        }
    }
}
