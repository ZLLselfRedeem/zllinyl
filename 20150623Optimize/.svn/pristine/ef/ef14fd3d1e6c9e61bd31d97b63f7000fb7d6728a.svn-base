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

public partial class ViewAllocWebSite_recentNewsDetail : System.Web.UI.Page
{
    public string strContent = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Page.Request.QueryString["number"] != null && Page.Request.QueryString["number"].ToString().Length > 0)
            {
                string number = Page.Request.QueryString["number"].ToString();
                GetNewsDetail(number);
            }
        }
    }

    private void GetNewsDetail(string number)
    {
        //DataTable dt = GetAllRecentNews();
        //DataRow[] row = dt.Select("number = '" + number + "'");
        DataTable dt = GetAllRecentNewsFromDB(Common.ToInt64(number));

        if (dt.Rows.Count > 0)
        {
            this.lbType.Text = dt.Rows[0]["remark"].ToString();
            this.lbType1.Text = dt.Rows[0]["remark"].ToString();
            this.lbDate.Text = dt.Rows[0]["date"].ToString();
            this.lbTitle.Text = dt.Rows[0]["title"].ToString();
            strContent = dt.Rows[0]["content"].ToString().Replace("\r\n", "<br />");
        }
    }

    private DataTable GetAllRecentNews()
    {
        DataTable dt = new DataTable();
        DataRow row;

        dt.Columns.Add("number");
        dt.Columns.Add("type");
        dt.Columns.Add("date");
        dt.Columns.Add("title");
        dt.Columns.Add("content");

        string fileName = Server.MapPath("App_Data/recentnews.xml");
        string xPath = "//recentnews//news";

        XmlNodeList List = SysXmlHelper.GetXmlNodeListByXpath(fileName, xPath);

        if (List != null)
        {
            foreach (XmlNode node in List)
            {
                row = dt.NewRow();

                XmlNodeList children = node.ChildNodes;

                if (children != null)
                {
                    for (int i = 0; i < children.Count; i++)
                    {
                        //row[i] = children[i].InnerText;
                        row[i] = children[i].InnerXml;
                    }
                    dt.Rows.Add(row);
                }
            }
        }
        dt.DefaultView.Sort = "type desc,date desc";
        DataTable dt1 = dt.DefaultView.ToTable();

        return dt1;
    }

    private DataTable GetAllRecentNewsFromDB(long id)
    {
        OfficialWebConfigOperate webOperate = new OfficialWebConfigOperate();
        return webOperate.QueryOfficialWebConfig(id);
    }
}