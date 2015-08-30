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

public partial class ViewAllocWebSite_download : System.Web.UI.Page
{
    //装载最新动态的Html
    //public string history = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.rpHistory.DataSource = GetHistoryDataTableFromDB(5);// GetHistoryDataTable(5);
            this.rpHistory.DataBind();
        }
    }
    /// <summary>
    /// 获取前N条数据
    /// </summary>
    /// <param name="number">N</param>
    /// <returns></returns>
    private DataTable GetHistoryDataTableFromDB(int number)
    {
        OfficialWebConfigOperate webOperate = new OfficialWebConfigOperate();
        DataTable dt = webOperate.QueryOfficialWebConfig(VAOfficialWebType.UPDATE_HISTORY);
        DataTable dt1 = new DataTable();
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i]["content"] = dt.Rows[i]["content"].ToString().Replace("\r\n", "<br />");
            }
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
    private DataTable GetHistoryDataTable(int number)
    {
        DataTable dt = new DataTable();
        DataTable dt1 = new DataTable();
        DataRow row;

        dt.Columns.Add("number");
        dt.Columns.Add("date");
        dt.Columns.Add("version");
        dt.Columns.Add("title");
        dt.Columns.Add("content");

        dt.Columns["number"].DataType = typeof(int);

        string fileName = Server.MapPath("App_Data/updatehistory.xml");
        string xPath = "//updatehistory//history";

        XmlNodeList List = SysXmlHelper.GetXmlNodeListByXpath(fileName, xPath);

        if (List != null && List.Count > 0)
        {
            foreach (XmlNode node in List)
            {
                row = dt.NewRow();

                XmlNodeList children = node.ChildNodes;

                if (children != null)
                {
                    for (int i = 0; i < children.Count; i++)
                    {
                        if (i == 0)
                        {
                            row[i] = Convert.ToInt32(children[i].InnerXml);
                        }
                        else
                        {
                            //row[i] = children[i].InnerText;
                            row[i] = children[i].InnerXml;
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


    /// <summary>
    /// 将前N条更新历史转成Html
    /// </summary>
    /// <param name="number">条数</param>
    /// <returns>Html</returns>
    //private string GetTopNHistory(int number)
    //{
    //    StringBuilder strHtml = new StringBuilder();//返回给前端的Html

    //    DataTable dt = GetHistoryDataTable();

    //    strHtml.Append("<table border='0' cellspacing='5' width='60%'>");

    //    for (int i = 0; i < number && i < dt.Rows.Count; i++)
    //    {
    //        strHtml.Append("<tr><td>" + dt.Rows[i]["date"].ToString() + "</td><td>" + dt.Rows[i]["version"].ToString() + "</td><td>" + dt.Rows[i]["content"].ToString() + "</td></tr>");
    //    }
    //    strHtml.Append("</table>");

    //    return strHtml.ToString();
    //}

}