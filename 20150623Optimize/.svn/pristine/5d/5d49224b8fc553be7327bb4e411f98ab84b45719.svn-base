using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Xml;

public partial class ViewAllocWebSite_updateHistoryDetail : System.Web.UI.Page
{
    public string strContent = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Page.Request.QueryString["number"] != null && Page.Request.QueryString["number"].ToString().Length > 0)
            {
                string number = Page.Request.QueryString["number"].ToString();
                GetHistoryDetail(number);
            }
        }
    }

    private void GetHistoryDetail(string number)
    {
        DataTable dt = GetAllRecentNews();
        DataRow[] row = dt.Select("number = '" + number + "'");

        this.lbDate.Text = row[0]["date"].ToString();
        this.lbVersion.Text = row[0]["version"].ToString();
        this.lbTitle.Text = row[0]["title"].ToString();
        strContent = row[0]["content"].ToString();
    }

    private DataTable GetAllRecentNews()
    {
        DataTable dt = new DataTable();
        DataRow row;

        dt.Columns.Add("number");
        dt.Columns.Add("date");
        dt.Columns.Add("version");
        dt.Columns.Add("title");
        dt.Columns.Add("content");

        string fileName = Server.MapPath("App_Data/updatehistory.xml");
        string xPath = "//updatehistory//history";

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
        dt.DefaultView.Sort = "date desc";
        DataTable dt1 = dt.DefaultView.ToTable();

        return dt1;
    }
}