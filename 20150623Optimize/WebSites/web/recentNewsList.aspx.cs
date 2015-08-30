using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Xml;
using System.Text;
using VAGastronomistMobileApp.WebPageDll;
using VAGastronomistMobileApp.Model;

public partial class ViewAllocWebSite_recentNewsList : System.Web.UI.Page
{
    public string strNews = "";
    OfficialWebConfigOperate webOperate = new OfficialWebConfigOperate();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            strNews = GetHtmlNews(3);
        }
    }
    private string GetHtmlNews(int perCount)
    {
        StringBuilder strHtml = new StringBuilder();
        //DataTable dt = GetNewsDataTable(perCount);
        DataTable dt = GetNewsDataTableFromDB(perCount); 
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            #region
            //if (i == 0)
            //{
            //    strHtml.Append("<div class='item'><dl><dt>" + dt.Rows[i]["type"].ToString() + "<a href='recentNewsWithType/" + ChangeTypeToNumber(dt.Rows[i]["type"].ToString()) + ".html'>更多<code>&gt;&gt;</code></a></dt>");//类别
            //    strHtml.Append("<dd class='txt'><span class='date'>" + dt.Rows[i]["date"].ToString() + "</span> <p><a href='recentNewsDetail/" + dt.Rows[i]["number"].ToString() + ".html'>" + dt.Rows[i]["title"].ToString() + "</a></p></dd>");
            //}
            //else
            //{
            //    //类别相同则继续输出
            //    if (dt.Rows[i]["type"].ToString() == dt.Rows[i - 1]["type"].ToString())
            //    {
            //        strHtml.Append("<dd class='txt'><span class='date'>" + dt.Rows[i]["date"].ToString() + "</span> <p><a href='recentNewsDetail/" + dt.Rows[i]["number"].ToString() + ".html'>" + dt.Rows[i]["title"].ToString() + "</a></p></dd>");
            //    }
            //    else//反之先输出新的类别
            //    {
            //        strHtml.Append("</dl></div>");
            //        strHtml.Append("<div class='item'><dl><dt>" + dt.Rows[i]["type"].ToString() + "<a href='recentNewsWithType/" + ChangeTypeToNumber(dt.Rows[i]["type"].ToString()) + ".html'>更多<code>&gt;&gt;</code></a></dt>");//类别
            //        strHtml.Append("<dd class='txt'><span class='date'>" + dt.Rows[i]["date"].ToString() + "</span> <p><a href='recentNewsDetail/" + dt.Rows[i]["number"].ToString() + ".html'>" + dt.Rows[i]["title"].ToString() + "</a></p></dd>");
            //    }
            //}
            #endregion
            if (i == 0)
            {
                strHtml.Append("<div class='item'><dl><dt>" + dt.Rows[i]["remark"].ToString() + "<a href='recentNewsWithType.aspx?type=" + ChangeTypeToNumber(dt.Rows[i]["remark"].ToString()) + "'>更多<code>&gt;&gt;</code></a></dt>");//类别
                strHtml.Append("<dd class='txt'><span class='date'>" + dt.Rows[i]["date"].ToString() + "</span> <p><a href='recentNewsDetail.aspx?number=" + dt.Rows[i]["id"].ToString() + "'>" + dt.Rows[i]["title"].ToString() + "</a></p></dd>");
            }
            else
            {
                //类别相同则继续输出
                if (dt.Rows[i]["remark"].ToString() == dt.Rows[i - 1]["remark"].ToString())
                {
                    strHtml.Append("<dd class='txt'><span class='date'>" + dt.Rows[i]["date"].ToString() + "</span> <p><a href='recentNewsDetail.aspx?number=" + dt.Rows[i]["id"].ToString() + "'>" + dt.Rows[i]["title"].ToString() + "</a></p></dd>");
                }
                else//反之先输出新的类别
                {
                    strHtml.Append("</dl></div>");
                    strHtml.Append("<div class='item'><dl><dt>" + dt.Rows[i]["remark"].ToString() + "<a href='recentNewsWithType.aspx?type=" + ChangeTypeToNumber(dt.Rows[i]["remark"].ToString()) + "'>更多<code>&gt;&gt;</code></a></dt>");//类别
                    strHtml.Append("<dd class='txt'><span class='date'>" + dt.Rows[i]["date"].ToString() + "</span> <p><a href='recentNewsDetail.aspx?number=" + dt.Rows[i]["id"].ToString() + "'>" + dt.Rows[i]["title"].ToString() + "</a></p></dd>");
                }
            }
        }
        strHtml.Append("</dl></div>");

        return strHtml.ToString();
    }

    private string ChangeTypeToNumber(string type)
    {
        DataTable dt = GetAllTypeFromDB();

        DataRow[] rows = dt.Select("title='" + type + "'");

        return rows[0]["id"].ToString();
    }
    //private string ChangeTypeToNumber(string type)
    //{
    //    DataTable dt = GetAllType();

    //    DataRow[] rows = dt.Select("type='" + type + "'");

    //    return rows[0]["number"].ToString();
    //}

    /// <summary>
    /// 读取所有类别信息
    /// </summary>
    private DataTable GetAllType()
    {
        DataTable dt = new DataTable();
        string fileName = Server.MapPath("App_Data/recentnewstype.xml");
        string xPath = "//recentnewstype//type";

        XmlNodeList List = SysXmlHelper.GetXmlNodeListByXpath(fileName, xPath);

        if (List != null)
        {
            dt.Columns.Add("number");
            dt.Columns.Add("type");
            dt.Columns.Add("order");

            dt.Columns["order"].DataType = typeof(int);

            DataRow row;

            foreach (XmlNode node in List)
            {
                row = dt.NewRow();
                row[0] = node.Attributes[0].Value;
                row[1] = node.InnerText;
                row[2] = Convert.ToInt32(node.Attributes[1].Value);
                dt.Rows.Add(row);
            }

            dt.DefaultView.Sort = "order asc";
            dt = dt.DefaultView.ToTable();
        }
        return dt;
    }

    private DataTable GetNewsDataTableFromDB(int perCount)
    {
        DataTable dt = new DataTable();
        DataTable dt1 = new DataTable();
        DataTable dt2 = new DataTable();
        dt = webOperate.QueryOfficialWebConfig(VAOfficialWebType.RECENT_NEWS);
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //dt.Rows[i]["sequence"] = Convert.ToInt32(GetOrderByType(dt.Rows[i]["type"].ToString()));
                dt.Rows[i]["sequence"] = Convert.ToInt32(GetOrderByTypeFromDB(dt.Rows[i]["remark"].ToString()));
            }
            dt.DefaultView.Sort = "sequence asc,type asc,date desc,id desc";
            dt1 = dt.DefaultView.ToTable();
            dt2 = dt1.DefaultView.ToTable(true, "type");
            dt.Rows.Clear();
            DataRow[] rows;

            for (int i = 0; i < dt2.Rows.Count; i++)
            {
                rows = dt1.Select("type='" + dt2.Rows[i]["type"].ToString() + "'");
                for (int j = 0; j < perCount && j < rows.Length; j++)
                {
                    dt.ImportRow(rows[j]);
                }
            }
        }
        return dt;
    }

    private string GetOrderByTypeFromDB(string type)
    {
        return webOperate.GetSequenceByType(type);
    }

    /// <summary>
    /// 读取所有类别信息
    /// </summary>
    private DataTable GetAllTypeFromDB()
    {
        return webOperate.QueryOfficialWebConfig(VAOfficialWebType.RECENT_NEWS_TYPE);
    }
    /// <summary>
    /// 先从XML中抓取所有动态并进行筛选
    /// </summary>
    /// <param name="pageCount">请求的页码</param>
    /// <param name="perCount">每页的动态条数</param>
    /// <returns></returns>
    private DataTable GetNewsDataTable(int perCount)
    {
        DataTable dt = new DataTable();
        DataTable dt1 = new DataTable();
        DataTable dt2 = new DataTable();
        DataRow row;

        dt.Columns.Add("number");
        dt.Columns.Add("type");
        dt.Columns.Add("date");
        dt.Columns.Add("title");
        dt.Columns.Add("content");
        dt.Columns.Add("order");

        dt.Columns["order"].DataType = typeof(int);
        dt.Columns["number"].DataType = typeof(int);

        string fileName = Server.MapPath("App_Data/recentnews.xml");
        string xPath = "//recentnews//news";

        XmlNodeList List = SysXmlHelper.GetXmlNodeListByXpath(fileName, xPath);

        if (List != null && List.Count > 0)
        {
            foreach (XmlNode node in List)
            {
                row = dt.NewRow();

                XmlNodeList children = node.ChildNodes;

                if (children != null)
                {
                    for (int i = 0; i <= children.Count; i++)
                    {
                        if (i == 0)
                        {
                            row[i] = Convert.ToInt32(children[i].InnerText);
                        }
                        else if (i == children.Count)
                        {
                            row[i] = 0;//先将所有order置0
                        }
                        else
                        {
                            row[i] = children[i].InnerText;
                        }
                        //if (i != children.Count)
                        //{
                        //    row[i] = children[i].InnerText;
                        //}
                        //else
                        //{
                        //    row[i] = 0;//先将所有order置0
                        //}
                    }

                    dt.Rows.Add(row);
                }
            }

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i]["order"] = Convert.ToInt32(GetOrderByType(dt.Rows[i]["type"].ToString()));
            }

            dt.DefaultView.Sort = "order asc,type asc,date desc,number desc";
            dt1 = dt.DefaultView.ToTable();
            dt2 = dt1.DefaultView.ToTable(true, "type");
            dt.Rows.Clear();
            DataRow[] rows;

            for (int i = 0; i < dt2.Rows.Count; i++)
            {
                rows = dt1.Select("type='" + dt2.Rows[i]["type"].ToString() + "'");
                for (int j = 0; j < perCount && j < rows.Length; j++)
                {
                    dt.ImportRow(rows[j]);
                }
            }
        }

        return dt;
    }

    private DataRow[] SelectTable(DataTable dtOriginal, string type)
    {
        //DataRow[] row = dtOriginal.Select("type=" + type);
        DataRow[] row = dtOriginal.Select("remark=" + type);

        return row;
    }

    private string GetOrderByType(string type)
    {
        string order = "";
        string fileName = Server.MapPath("App_Data/recentnewstype.xml");
        string xPath = "//recentnewstype//type";

        XmlNodeList List = SysXmlHelper.GetXmlNodeListByXpath(fileName, xPath);

        if (List != null && List.Count > 0)
        {
            foreach (XmlNode node in List)
            {
                if (node.InnerText == type)
                {
                    order = node.Attributes[1].Value;
                    break;
                }
            }
        }
        return order;
    }

    /// <summary>
    /// 获取分页页数
    /// </summary>
    /// <returns></returns>
    private DataTable GetPageCount(double perCount)
    {
        DataTable dt = new DataTable();

        dt.Columns.Add("pageCount");

        string fileName = Server.MapPath("App_Data/recentnews.xml");
        string xPath = "//recentnews//news";

        double sumCount = SysXmlHelper.GetXmlNodeCount(fileName, xPath);//最新动态总条数

        int pageCount = Convert.ToInt32(Math.Ceiling(sumCount / perCount));//向上取整,总页数,每页显示6条

        DataRow row;

        for (int i = 1; i < pageCount + 1; i++)
        {
            row = dt.NewRow();
            row[0] = i;

            dt.Rows.Add(row);
        }
        return dt;
    }

    /// <summary>
    /// 获取动态总条数
    /// </summary>
    /// <returns></returns>
    private int GetNewsCount()
    {
        string fileName = Server.MapPath("App_Data/recentnews.xml");
        string xPath = "//recentnews//news";

        int sumCount = SysXmlHelper.GetXmlNodeCount(fileName, xPath);//最新动态总条数

        return sumCount;
    }
}