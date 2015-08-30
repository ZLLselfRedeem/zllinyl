using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Xml;
using VAGastronomistMobileApp.WebPageDll;
using VAGastronomistMobileApp.Model;

public partial class ViewAllocWebSite_recentNewsWithType : System.Web.UI.Page
{
    public string strTypeNumber = string.Empty;
    OfficialWebConfigOperate webOperate = new OfficialWebConfigOperate();

    protected void Page_Load(object sender, EventArgs e)
    {
        //如果是由所有动态列表链接过来(recentNewsList.aspx)
        if (!string.IsNullOrEmpty(Page.Request.QueryString["type"]))
        {
            strTypeNumber = Page.Request.QueryString["type"].ToString();//实际存放的是对应的Number

            ViewState["strType"] = strTypeNumber;
        }
        //页面间跳转
        if (!string.IsNullOrEmpty(Page.Request.QueryString["rt"]))
        {
            ViewState["strType"] = Server.UrlDecode(Page.Request.QueryString["rt"].ToString());
        }
        this.lbType1.Text = ChangeNumberToType(Common.ToInt64(ViewState["strType"]));//真实Type    
        GetPage(lbType1.Text);
    }

    /// <summary>
    /// 先从XML中抓取所有动态并进行筛选出某一Type的
    /// </summary>
    /// <param name="pageCount">请求的页码</param>
    /// <param name="perCount">每页的动态条数</param>
    /// <returns></returns>
    private DataTable GetNewsDataTable(string type)
    {
        DataTable dt = new DataTable();
        DataTable dt1 = new DataTable();
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

            DataRow[] rows;
            rows = dt.Select("type='" + type + "'");

            dt1 = dt.Copy();
            dt1.Rows.Clear();

            for (int i = 0; i < rows.Length; i++)
            {
                dt1.ImportRow(rows[i]);
            }

            dt1.DefaultView.Sort = "date desc,number desc";
            dt.Rows.Clear();
            dt = dt1.DefaultView.ToTable();
        }

        return dt;
    }

    private DataTable GetNewsDataTableFromDB(string type)
    {
        DataTable dt = webOperate.QueryRencetNewWithType(type);
        DataTable dt1 = new DataTable();
        if (dt.Rows.Count > 0)
        {
            dt.DefaultView.Sort = "date desc,id desc";
            dt1 = dt.DefaultView.ToTable();
        }
        return dt1;
    }

    /// <summary>
    /// 读取所有类别信息
    /// </summary>
    private DataTable GetAllTypeFromDB()
    {
        return webOperate.QueryOfficialWebConfig(VAOfficialWebType.RECENT_NEWS_TYPE);
    }

    private string ChangeNumberToType(long number)
    {
        //DataTable dt = GetAllType();
        //DataRow[] rows = dt.Select("number='" + number + "'");
        DataTable dt = GetAllTypeFromDB();
        DataRow[] rows = dt.Select("id='" + number + "'");

        return rows[0]["title"].ToString();
    }

    private void GetPage(string type)
    {
        PagedDataSource pds = new PagedDataSource();

        //pds.DataSource = GetNewsDataTable(type).DefaultView;
        pds.DataSource = GetNewsDataTableFromDB(type).DefaultView;

        pds.AllowPaging = true;

        pds.PageSize = 10;

        int currentPage = Convert.ToInt32(Request["p"]);

        //设当前页
        pds.CurrentPageIndex = currentPage;

        string rt = Server.UrlEncode(ViewState["strType"].ToString());
        //设几个超链接
        if (!pds.IsFirstPage)
        {
            lnkUp.NavigateUrl = Request.CurrentExecutionFilePath + "?rt=" + rt + "&p=" + (currentPage - 1);
        }

        if (!pds.IsLastPage)
        {
            lnkDown.NavigateUrl = Request.CurrentExecutionFilePath + "?rt=" + rt + "&p=" + (currentPage + 1);
        }

        lbl_info.Text = "第" + (currentPage + 1) + "页、共" + pds.PageCount + "页";

        rpNews.DataSource = pds;
        rpNews.DataBind();
    }

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
}