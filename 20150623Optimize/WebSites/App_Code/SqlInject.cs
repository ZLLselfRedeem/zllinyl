using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.OleDb;
using System.Text.RegularExpressions;

public class SqlInject : System.Web.UI.Page
{
    //过滤特征字符
    private const string StrKeyWord = @"select|insert|delete|from|count(|drop table|update|truncate|asc(|mid(|char(|xp_cmdshell|exec|master|net local group administrators|net user|or|and";
    private const string StrRegex = @"-|;|,|/|(|)|[|]|{|}|%|@|*|!";//去掉了-

    private HttpRequest request;
    public SqlInject(System.Web.HttpRequest _request)
    {
        this.request = _request;
    }
    /// <summary>
    /// 验证整个页面的输入是不是含有sql注入
    /// </summary>
    /// <returns></returns>
    public string CheckAllPageInject()
    {
        string  error = "";
        if (CheckRequestQuery() || CheckRequestForm())
        {
            error = "请不要输入非法字符！";
           
        }
        System.Web.HttpContext.Current.Response.Clear();
        return error;
    }
    /// <summary>
    /// 验证某个控件输入是不是含有sql注入
    /// </summary>
    /// <returns></returns>
    public string CheckTextBoxInject(string text)
    {
        string error = "";
        if (CheckKeyWord(text))
        {
            error = "请不要输入非法字符！";
        }
        System.Web.HttpContext.Current.Response.Clear();
        return error;
    }
    ///<summary>
    ///检查字符串中是否包含Sql注入关键字
    /// <param name="_key">被检查的字符串</param>
    /// <returns>如果包含注入true;否则返回false</returns>
    ///</summary>
    private static bool CheckKeyWord(string _key)
    {
        string[] pattenString = StrKeyWord.Split('|');
        string[] pattenRegex = StrRegex.Split('|');
        foreach (string sqlParam in pattenString)
        {
            if (_key.Contains(sqlParam + " ") || _key.Contains(" " + sqlParam) || _key.Contains(sqlParam))
            {
                return true;
            }
        }
        foreach (string sqlParam in pattenRegex)
        {
            if (_key.Contains(sqlParam))
            {
                return true;
            }
        }
        return false;
    }
    ///<summary>
    ///检查URL中是否包含Sql注入
    /// <param name="_request">当前HttpRequest对象</param>
    /// <returns>如果包含注入true;否则返回false</returns>
    ///</summary>
    public bool CheckRequestQuery()
    {
        if (request.QueryString.Count > 0)
        {
            foreach (string sqlParam in this.request.QueryString)
            {
                if (sqlParam == "__VIEWSTATE") continue;
                if (sqlParam == "__EVENTVALIDATION") continue;
                if (CheckKeyWord(request.QueryString[sqlParam].ToLower()))
                {
                    return true;
                }
            }
        }
        return false;
    }
    ///<summary>
    ///检查提交的表单中是否包含Sql注入
    /// <param name="_request">当前HttpRequest对象</param>
    /// <returns>如果包含注入true;否则返回false</returns>
    ///</summary>
    public bool CheckRequestForm()
    {
        if (request.Form.Count > 0)
        {
            foreach (string sqlParam in this.request.Form)
            {
                if (sqlParam == "__VIEWSTATE") continue;
                if (sqlParam == "__EVENTVALIDATION") continue;
                if (CheckKeyWord(request.Form[sqlParam]))
                {
                    return true;
                }
            }
        }
        return false;
    }
}
