using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.UI.WebControls;

namespace Web.Control.Enum
{
    /// <summary>
    /// 枚举操作辅助类
    /// </summary>
    public class EnumHelper
    {
        /// <summary>
        /// 枚举结构Description和Value转换为DataTable结构
        /// </summary>
        /// <param name="enumType">枚举结构</param>
        /// <param name="TextColumnsName">枚举描述</param>
        /// <param name="ValueColumnName">枚举值</param>
        /// <returns></returns>
        public static DataTable EnumToDataTable(Type enumType, string TextColumnsName, string ValueColumnName)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(TextColumnsName, typeof(System.String));
            dt.Columns.Add(ValueColumnName, typeof(System.Int32));
            Type typeDescription = typeof(DescriptionAttribute);
            FieldInfo[] fields = enumType.GetFields();
            foreach (FieldInfo field in fields)
            {
                if (field.FieldType.IsEnum == true)
                {
                    DataRow dr = dt.NewRow();
                    dr[ValueColumnName] = (int)(int)enumType.InvokeMember(field.Name, BindingFlags.GetField, null, null, null);
                    object[] arr = field.GetCustomAttributes(typeDescription, true);
                    if (arr.Length > 0)
                    {
                        DescriptionAttribute descriptionAttribute = (DescriptionAttribute)arr[0];
                        dr[TextColumnsName] = descriptionAttribute.Description;
                    }
                    else
                    {
                        dr[TextColumnsName] = field.Name;
                    }
                    dt.Rows.Add(dr);
                }
            }
            return dt;
        }

        /// <summary>
        /// 枚举结构Description和Value转换为DataTable结构
        /// </summary>
        /// <param name="enumType">枚举结构</param>
        /// <returns></returns>
        public static DataTable EnumToDataTable(Type enumType)
        {
            string TextColumnsName = "text";
            string ValueColumnName = "value";
            return EnumToDataTable(enumType, TextColumnsName, ValueColumnName);
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
        /// 获取枚举结构的Name（Description）
        /// </summary>
        /// <typeparam name="T">枚举类型</typeparam>
        /// <param name="value">枚举值</param>
        /// <returns></returns>
        public static string GetEnumName<T>(int value) where T : new()
        {
            Type t = typeof(T);
            foreach (MemberInfo mInfo in t.GetMembers())
            {
                if (mInfo.Name == t.GetEnumName(value))
                {
                    foreach (Attribute attr in Attribute.GetCustomAttributes(mInfo))
                    {
                        if (attr.GetType() == typeof(DescriptionAttribute))
                        {
                            return ((DescriptionAttribute)attr).Description;
                        }
                    }
                }
            }
            return "";
        }
    }
}
