using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Xml;
using VAGastronomistMobileApp.WebPageDll;
using System.Configuration;
using VAGastronomistMobileApp.Model;

public partial class index : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.rpNews.DataSource = GetNewsWithoutTypeFromDB(8);//GetNewsWithoutType(8);
            this.rpNews.DataBind();

            GetCompany();
        }
    }

    private DataTable GetNewsWithoutTypeFromDB(int number)
    {
        OfficialWebConfigOperate webOperate = new OfficialWebConfigOperate();
        DataTable dt1 = new DataTable();
        DataTable dt = webOperate.QueryOfficialWebConfig(VAOfficialWebType.RECENT_NEWS);
        if (dt.Rows.Count > 0)
        {
            dt.DefaultView.Sort = "date desc,id desc";
            dt1 = dt.DefaultView.ToTable();
            dt1 = dt1.AsEnumerable().Take(number).CopyToDataTable<DataRow>();//获取DataTable前N条
        }
        return dt1;
    }

    /// <summary>
    /// 先从XML中抓取所有动态,并按时间逆序排序
    /// </summary>
    /// <returns></returns>
    private DataTable GetNewsWithoutType(int number)
    {
        DataTable dt = new DataTable();
        DataTable dt1 = new DataTable();
        DataRow row;

        dt.Columns.Add("number");
        dt.Columns.Add("type");
        dt.Columns.Add("date");
        dt.Columns.Add("title");

        dt.Columns["number"].DataType = typeof(int);

        string fileName = Server.MapPath("web/App_Data/recentnews.xml");
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
                    for (int i = 0; i < children.Count - 1; i++)
                    {
                        if (i == 0)
                        {
                            row[i] = Convert.ToInt32(children[i].InnerText);
                        }
                        else
                        {
                            row[i] = children[i].InnerText;
                        }
                    }
                    dt.Rows.Add(row);
                }
            }
            dt.DefaultView.Sort = "date desc,number desc";
            dt1 = dt.DefaultView.ToTable();
            dt1 = dt1.AsEnumerable().Take(number).CopyToDataTable<DataRow>();//获取DataTable前N条
        }

        return dt1;
    }



    private void GetCompany()
    {
        DataTable dt = new DataTable();

        //dt = GetCityCompanyLogoAndName(87, 8);//杭州
        dt = GetShopLogo(87, 8);
        if (dt != null && dt.Rows.Count > 0)
        {
            this.rpHangzhou.DataSource = dt;
            this.rpHangzhou.DataBind();
            dt.Clear();
        }

        //dt = GetCityCompanyLogoAndName(73, 8);//上海
        dt = GetShopLogo(73, 8);
        if (dt != null && dt.Rows.Count > 0)
        {
            this.rpShanghai.DataSource = dt;
            this.rpShanghai.DataBind();
            dt.Clear();
        }

        //dt = GetCityCompanyLogoAndName(1, 8);//北京
        dt = GetShopLogo(1, 8);
        if (dt != null && dt.Rows.Count > 0)
        {
            this.rpBeijing.DataSource = dt;
            this.rpBeijing.DataBind();
            dt.Clear();
        }

        //dt = GetCityCompanyLogoAndName(197, 8);//广州
        dt = GetShopLogo(197, 8);
        if (dt != null && dt.Rows.Count > 0)
        {
            this.rpGuangzhou.DataSource = dt;
            this.rpGuangzhou.DataBind();
            dt.Clear();
        }

        //dt = GetCityCompanyLogoAndName(199, 8);//深圳
        dt = GetShopLogo(199, 8);
        if (dt != null && dt.Rows.Count > 0)
        {
            this.rpShenzhen.DataSource = dt;
            this.rpShenzhen.DataBind();
            dt.Clear();
        }
    }

    /// <summary>
    /// viewalloc官网 获取指定城市最新上线公司的图片信息
    /// </summary>
    /// <param name="cityId">城市标号</param>
    /// <param name="logoImageCount">需要查询城市图片数量</param>
    /// <returns></returns>
    public DataTable GetCityCompanyLogoAndName(int cityId, int logoImageCount)
    {
        CompanyOperate companyOperate = new CompanyOperate();
        DataTable dTable = companyOperate.QueryCompanyLogoAndName(cityId, logoImageCount);
        if (dTable.Rows.Count > 0)
        {
            string serverPath = ConfigurationManager.AppSettings["Server"].ToString() + "/" + ConfigurationManager.AppSettings["ImagePath"].ToString();
            //string serverPath = "http://192.168.1.11" + "/" + ConfigurationManager.AppSettings["ImagePath"].ToString();//测试
            for (int i = 0; i < dTable.Rows.Count; i++)
            {
                dTable.Rows[i]["companyLogoFullPath"] = serverPath + dTable.Rows[i]["companyLogoFullPath"];//填补获取图片的全路径
            }
        }
        return dTable;
    }

    /// <summary>
    /// 获取指定城市指定个数的店铺logo
    /// Add at 2014-4-2
    /// </summary>
    /// <param name="cityId"></param>
    /// <param name="logoImageCount"></param>
    /// <returns></returns>
    public DataTable GetShopLogo(int cityId, int logoImageCount)
    {
        ShopOperate shopOperate = new ShopOperate();
        DataTable dt = shopOperate.QueryShopLogo(cityId, logoImageCount);
        if (dt != null && dt.Rows.Count > 0)
        {
            string serverPath = WebConfig.CdnDomain + WebConfig.ImagePath;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i]["shopLogo"] = serverPath + dt.Rows[i]["shopLogo"];//
            }
        }
        return dt;
    }

    /// <summary>
    /// 将前N条最新动态转成Html
    /// </summary>
    /// <param name="number">条数</param>
    /// <returns>Html</returns>
    //private string GetTopNFromNews(int number)
    //{
    //    StringBuilder strHtml = new StringBuilder();//返回给前端的Html

    //    DataTable dt = GetNewsWithoutType();

    //    strHtml.Append("<div><ul>");

    //    for (int i = 0; i < number && i < dt.Rows.Count; i++)
    //    {
    //        strHtml.Append("<li><span>" + dt.Rows[i]["date"].ToString() + "</span>&nbsp;&nbsp;&nbsp;&nbsp;<a href='recentNews.aspx?number=" + dt.Rows[i]["number"].ToString() + "'>" + dt.Rows[i]["title"].ToString() + "</a></li>");
    //    }
    //    strHtml.Append("</ul></div>");

    //    return strHtml.ToString();
    //}

}