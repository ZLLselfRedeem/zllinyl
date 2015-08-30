using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI.WebControls;
using System.Data;
using VAGastronomistMobileApp.Model;
using System.ComponentModel;
using System.Web.UI;
using System.Reflection;
using System.Collections;

namespace VAGastronomistMobileApp.WebPageDll
{
    /// <summary>
    /// 2013-9-6 wangcheng
    /// asp.net页面操作公共方法（合法校验）
    /// </summary>
    public class CommonPageOperate
    {
        private static Regex RegPhone = new Regex("^(d+-)?(d{4}-?d{7}|d{3}-?d{8}|^d{7,8})(-d+)?");//phone正则表达式
        private static Regex RegNumber = new Regex("^[0-9]+$");
        private static Regex RegNumberSign = new Regex("^[+-]?[0-9]+$");
        private static Regex RegDecimal = new Regex("^[0-9]+[.]?[0-9]+$");
        private static Regex RegDecimalSign = new Regex("^[+-]?[0-9]+[.]?[0-9]+$");
        private static Regex RegEmail = new Regex("^[0-9a-z][a-z0-9._-]{1,}@[a-z0-9-]{1,}[a-z0-9].[a-z.]{1,}[a-z]$");//邮箱地址
        private static Regex RegCHZN = new Regex("[\u4e00-\u9fa5]");//中文字符

        #region 页面合法校验

        /// <summary>
        /// 是否电话号码字符串
        /// </summary>
        /// <param name="inputData">输入字符串</param>
        /// <returns></returns>
        public static bool IsPhoneNumber(string inputData)
        {
            Match m = RegPhone.Match(inputData);
            return m.Success;
        }
        /// <summary> 
        /// 是否数字字符串 
        /// </summary> 
        /// <param name="inputData">输入字符串</param> 
        /// <returns></returns> 
        public static bool IsNumber(string inputData)
        {
            Match m = RegNumber.Match(inputData);
            return m.Success;
        }
        /// <summary> 
        /// 是否数字字符串 可带正负号 
        /// </summary> 
        /// <param name="inputData">输入字符串</param> 
        /// <returns></returns> 
        public static bool IsNumberSign(string inputData)
        {
            Match m = RegNumberSign.Match(inputData);
            return m.Success;
        }
        /// <summary> 
        /// 是否是浮点数 
        /// </summary> 
        /// <param name="inputData">输入字符串</param> 
        /// <returns></returns> 
        public static bool IsDecimal(string inputData)
        {
            Match m = RegDecimal.Match(inputData);
            return m.Success;
        }
        /// <summary> 
        /// 是否是浮点数 可带正负号 
        /// </summary> 
        /// <param name="inputData">输入字符串</param> 
        /// <returns></returns> 
        public static bool IsDecimalSign(string inputData)
        {
            Match m = RegDecimalSign.Match(inputData);
            return m.Success;
        }
        /// <summary> 
        /// 检测是否有中文字符 
        /// </summary> 
        /// <param name="inputData">输入字符串</param> 
        /// <returns></returns> 
        public static bool IsHasCHZN(string inputData)
        {
            Match m = RegCHZN.Match(inputData);
            return m.Success;
        }
        /// <summary> 
        /// 是否是邮箱地址
        /// </summary> 
        /// <param name="inputData">输入字符串</param> 
        /// <returns></returns> 
        public static bool IsEmail(string inputData)
        {
            Match m = RegEmail.Match(inputData);
            return m.Success;
        }
        /// <summary> 
        /// 转换成HTML code 
        /// </summary> 
        /// <param name="str">string</param> 
        /// <returns>string</returns> 
        public static string TextEncode(string str)
        {
            str = str.Replace("&", "&amp;");
            str = str.Replace("'", "''");
            str = str.Replace("\"", "&quot;");
            str = str.Replace(" ", "&nbsp;");
            str = str.Replace("<", "&lt;");
            str = str.Replace(">", "&gt;");
            str = str.Replace("\n", "<br>");
            return str;
        }
        /// <summary> 
        ///解析HTML成普通文本 
        /// </summary> 
        /// <param name="str">string</param> 
        /// <returns>string</returns> 
        public static string TextDecode(string str)
        {
            str = str.Replace("<br>", "\n");
            str = str.Replace("&gt;", ">");
            str = str.Replace("&lt;", "<");
            str = str.Replace("&nbsp;", " ");
            str = str.Replace("&quot;", "\"");
            return str;
        }
        /// <summary>
        /// 清理字符串
        /// </summary>
        /// <param name="strText">字符串文本</param>
        /// <returns></returns>
        public static string StrTextClear(string strText)
        {
            if (strText == null)
            {
                return null;
            }
            if (strText == "")
            {
                return "";
            }
            strText = strText.Replace(",", "");//去除, 
            strText = strText.Replace("<", "");//去除< 
            strText = strText.Replace(">", "");//去除> 
            strText = strText.Replace("--", "");//去除-- 
            strText = strText.Replace("'", "");//去除' 
            strText = strText.Replace("\"", "");//去除" 
            strText = strText.Replace("=", "");//去除= 
            strText = strText.Replace("%", "");//去除% 
            strText = strText.Replace(" ", "");//去除空格 
            return strText;
        }

        #endregion

        /// <summary>
        /// DataTable 拼接字符串，sql语句中where条件
        /// </summary>
        /// <param name="dtTable">DataTable</param>
        /// <param name="column">字段</param>
        /// <param name="IsString">column是否为字符串类型</param>
        /// <returns></returns>
        public static string SplicingAlphabeticStr(DataTable dtTable, string column, bool IsString = false)
        {
            if (dtTable.Rows.Count > 0)
            {
                try
                {
                    string strSplicing = "(";
                    for (int i = 0; i < dtTable.Rows.Count; i++)
                    {
                        if (i != dtTable.Rows.Count - 1)
                        {
                            if (IsString)
                            {
                                strSplicing += "\'" + Common.ToString(dtTable.Rows[i][column]) + "\'" + ",";
                            }
                            else
                            {
                                strSplicing += Common.ToString(dtTable.Rows[i][column]) + ",";
                            }
                        }
                        else
                        {
                            if (IsString)
                            {
                                strSplicing += "\'" + Common.ToString(dtTable.Rows[i][column]) + "\'";
                            }
                            else
                            {
                                strSplicing += Common.ToString(dtTable.Rows[i][column]);
                            }
                        }
                    }
                    strSplicing += ")";
                    return strSplicing;
                }
                catch (Exception)
                {
                    return "";
                }
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// list泛型 拼接字符串，sql语句中where条件
        /// </summary>
        /// <param name="objList">list</param>
        /// <param name="strValue">字段</param>
        /// <returns></returns>
        public static string SplicingListStr<T>(List<T> objList, string strValue)
        {
            //T为int，long等时，strValue=“”；
            //T为自定义类（具有多属性）时，strValue=传递过来属性值字段（单词）；
            if (objList.Count > 0)
            {
                try
                {
                    string strSplicing = "(";
                    for (int i = 0; i < objList.Count; i++)
                    {
                        if (i != objList.Count - 1)
                        {
                            if (strValue != "")
                            {
                                strSplicing += objList[i].GetType().GetProperty(strValue).GetValue(objList[i], null) + ",";
                            }
                            else
                            {
                                strSplicing += Common.ToString(objList[i]) + ",";
                            }
                        }
                        else
                        {
                            if (strValue != "")
                            {
                                strSplicing += objList[i].GetType().GetProperty(strValue).GetValue(objList[i], null);
                            }
                            else
                            {
                                strSplicing += Common.ToString(objList[i]);
                            }
                        }
                    }
                    strSplicing += ")";
                    return strSplicing;
                }
                catch (Exception)
                {
                    return "";
                }
            }
            else
            {
                return "";
            }
        }
        /// <summary>
        ///  小时数目转为天数/小时数目
        /// </summary>
        /// <param name="hours">小时时间</param>
        /// <returns></returns>
        public static int[] HoursToDays(int hours)
        {
            int[] dayAndHour = new int[2];
            dayAndHour[0] = hours / 24;
            dayAndHour[1] = hours % 24;
            return dayAndHour;
        }
        /// <summary>
        /// 获取枚举中的描述信息（wangcheng 2013-7-16）
        /// </summary>
        public static List<string> GetEnumDescription<Ttype>(Ttype Enumtype)
        {
            List<string> listDes = new List<string>();
            System.Reflection.FieldInfo[] fieldinfo = Enumtype.GetType().GetFields();
            foreach (System.Reflection.FieldInfo item in fieldinfo)
            {
                Object[] obj = item.GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (obj != null && obj.Length != 0)
                {
                    DescriptionAttribute des = (DescriptionAttribute)obj[0];
                    listDes.Add(des.Description);
                }
            }
            return listDes;//返回描述数组
        }

        /// <summary>
        /// 友絡计算价格算法
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static double YouLuoRound(double value, int decimals)
        {
            if (value < 0)
            {
                return Math.Round(value + 5 / Math.Pow(10, decimals + 1), decimals, MidpointRounding.AwayFromZero);
            }
            else
            {
                return Math.Round(value, decimals, MidpointRounding.AwayFromZero);
            }
        }
        /// <summary>
        /// 计算两个日期的天数间隔
        /// </summary>
        /// <param name="DateTime1">开始时间</param>
        /// <param name="DateTime2">结束时间</param>
        /// <returns>时间间隔天数</returns>
        public static int GetDiffDay(DateTime DateTime1, DateTime DateTime2)
        {
            int dateDiff = 0;
            TimeSpan ts1 = new TimeSpan(DateTime1.Ticks);
            TimeSpan ts2 = new TimeSpan(DateTime2.Ticks);
            TimeSpan ts = ts1.Subtract(ts2).Duration();
            dateDiff = ts.Days;
            return dateDiff;
        }
        /// <summary>
        /// 服务器端弹出提示消息框
        /// </summary>
        /// <param name="page">当前页面对象，一般传this</param>
        /// <param name="msgContent">消息内容</param>
        public static void AlterMsg(Page page, string msgContent)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<script type='text/javascript' defer>alert('");
            sb.Append(msgContent);
            sb.Append("')</script>");
            ClientScriptManager csm = page.ClientScript;
            if (!csm.IsStartupScriptRegistered("msg"))
            {
                csm.RegisterStartupScript(page.GetType(), "msg", sb.ToString());
            }
        }
        /// <summary>
        /// 弹出消息后跳转
        /// </summary>
        /// <param name="page"></param>
        /// <param name="msgContent">消息内容</param>
        /// <param name="url">要跳转的链接</param>
        public static void AlterMsgAndRedirect(Page page, string msgContent,string url)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<script type='text/javascript' defer>alert('");
            sb.Append(msgContent);
            sb.AppendFormat( "');window.location.href={0}</script>",url);
            ClientScriptManager csm = page.ClientScript;
            if (!csm.IsStartupScriptRegistered("msg"))
            {
                csm.RegisterStartupScript(page.GetType(), "msg", sb.ToString());
            }
        }

        /// <summary>
        /// 枚举结构Description和Value转换为List<ListItem>结构
        /// </summary>
        /// <param name="enumType">枚举结构</param>
        /// <returns></returns>
        public static List<ListItem> EnumToList(Type enumType)
        {
            if (enumType.IsEnum == false)
            {
                return null;
            }
            List<ListItem> list = new List<ListItem>();
            Type typeDescription = typeof(DescriptionAttribute);
            System.Reflection.FieldInfo[] fields = enumType.GetFields();
            string strText = string.Empty;
            string strValue = string.Empty;
            foreach (FieldInfo field in fields)
            {
                if (field.IsSpecialName) continue;
                strValue = field.GetRawConstantValue().ToString();
                object[] arr = field.GetCustomAttributes(typeDescription, true);
                if (arr.Length > 0)
                {
                    strText = (arr[0] as DescriptionAttribute).Description;
                }
                else
                {
                    strText = field.Name;
                }
                list.Add(new ListItem(strText, strValue));
            }
            return list;
        }

        /// <summary>
        ///  
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static DataTable GetTableFromList(IList list)
        {
            if (list != null && list.Count > 0)
            {
                DataTable dt = new DataTable();
                var properties = list[0].GetType().GetProperties(BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public);
                foreach (var item in properties)
                {
                    dt.Columns.Add(item.Name);
                }
                DataRow dr = null;
                foreach (var item in list)
                {
                    dr = dt.NewRow();
                    foreach (var property in properties)
                    {
                        dr[property.Name] = property.GetValue(item, null);
                    }
                    dt.Rows.Add(dr);
                }
                return dt;
            }
            return null;
        }
    }
    /// <summary>
    /// list 分页 add by wangc
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PagingOperate<T> : List<T>
    {
        /// <summary>
        /// list 数据分页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页面尺寸</param>
        /// <param name="list">数据</param>
        /// <returns></returns>
        public static string GetPagingListData(int pageIndex, int pageSize, List<T> list)
        {
            PagingUtil pageList = new PagingUtil();
            List<T> newList = new List<T>();
            if (list == null || pageIndex < 0 || pageSize <= 0)//没有数据，或者传递数据参数有误，默认为空的数据
            {
                pageList.pageSize = pageSize;
                pageList.pageIndex = pageIndex;
                pageList.recordCount = 0;
                pageList.pageCount = 0;
                pageList.data = JsonOperate.JsonSerializer<List<T>>(newList);
                return JsonOperate.JsonSerializer<PagingUtil>(pageList);
            }
            if (list.Count <= 0)
            {
                pageList.pageSize = pageSize;
                pageList.pageIndex = pageIndex;
                pageList.recordCount = 0;
                pageList.pageCount = 0;
                pageList.data = JsonOperate.JsonSerializer<List<T>>(newList);
                return JsonOperate.JsonSerializer<PagingUtil>(pageList);
            }
            try
            {
                newList.AddRange(list.Skip((pageIndex - 1) * pageSize).Take(pageSize));
                pageList.pageSize = pageSize;
                pageList.pageIndex = pageIndex;
                pageList.recordCount = list.Count;
                pageList.pageCount = (int)Math.Ceiling((decimal)list.Count / pageSize);
                pageList.data = JsonOperate.JsonSerializer<List<T>>(newList);
            }
            catch
            {
                pageList.pageSize = pageSize;
                pageList.pageIndex = pageIndex;
                pageList.recordCount = 0;
                pageList.pageCount = 0;
                pageList.data = JsonOperate.JsonSerializer<List<T>>(newList);
            }
            return JsonOperate.JsonSerializer<PagingUtil>(pageList);
        }
    }
}
