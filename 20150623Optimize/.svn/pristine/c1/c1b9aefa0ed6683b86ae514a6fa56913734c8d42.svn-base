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

public partial class ViewAllocWebSite_service : System.Web.UI.Page
{
    public string strHtml = ""; 
    OfficialWebConfigOperate webOperate = new OfficialWebConfigOperate();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //strHtml = GetTopNImages(7);
            this.rpCooperate.DataSource = GetCooperateFromDB();
            this.rpCooperate.DataBind();

            this.rpCase.DataSource = GetCaseFromDB();
            this.rpCase.DataBind();
        }
    }

    private DataTable GetCooperateFromDB()
    {
        DataTable dt= webOperate.QueryOfficialWebConfig(VAOfficialWebType.COOPERATE);
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i]["content"] = WebConfig.CdnDomain + dt.Rows[i]["content"];
            }
        }
        return dt;
    }
    private DataTable GetCaseFromDB()
    {
        DataTable dt = webOperate.QueryOfficialWebConfig(VAOfficialWebType.CLASS_CASE);
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i]["content"] = WebConfig.CdnDomain + dt.Rows[i]["content"];
            }
        }
        return dt;
    }
    private DataTable GetCooperate()
    {
        string fileName = Server.MapPath("App_Data/cooperate.xml");
        string xPath = "//cooperate//image";

        DataTable dt = GetAllCaseImage("cooperate", fileName, xPath);
        return dt;
    }

    private DataTable GetCase()
    {
        string fileName = Server.MapPath("App_Data/classiccase.xml");
        string xPath = "//classiccase//image";

        DataTable dt = GetAllCaseImage("case", fileName, xPath);
        return dt;
    }

    private DataTable GetAllCaseImage(string type, string fileName, string xPath)
    {
        DataTable dt = new DataTable();
        DataRow row;

        dt.Columns.Add("number");
        dt.Columns.Add("title");
        dt.Columns.Add("order");
        dt.Columns.Add("content");

        dt.Columns["order"].DataType = typeof(int);

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
                        switch (i)
                        {
                            case 2:
                                row[i] = Convert.ToInt32(children[i].InnerText);
                                break;
                            //case 3:
                            //    //去掉图片中原本的style
                            //    row[i] = children[i].InnerXml.Remove(children[i].InnerXml.IndexOf("style"), (children[i].InnerXml.Length - children[i].InnerXml.IndexOf("style") - 2));
                            //    if (type == "case")
                            //    {
                            //        row[i] = row[i].ToString().Insert(row[i].ToString().Length - 2, "style=\"width: 267px; height: 200px;\" ");
                            //    }
                            //    else
                            //    {
                            //        row[i] = row[i].ToString().Insert(row[i].ToString().Length - 2, "style=\"width: 110px; height: 110px;\" ");
                            //    }
                            //    break;
                            case 3:
                                //row[i] = children[i].InnerXml.Remove(0, 3);//去掉一层../，获取相对此页的正确路径
                                row[i] = WebConfig.CdnDomain + children[i].InnerXml;
                                break;
                            default:
                                row[i] = children[i].InnerXml;
                                break;
                        }
                    }
                    dt.Rows.Add(row);
                }
            }
        }
        dt.DefaultView.Sort = "order desc";
        DataTable dt1 = dt.DefaultView.ToTable();

        return dt;
    }


    /// <summary>
    /// 将要显示的图片构造Html
    /// </summary>
    /// <param name="number">一行显示照片的个数</param>
    /// <returns>Html</returns>
    //private string GetTopNImages(int number)
    //{
    //    StringBuilder strHtml = new StringBuilder();//返回给前端的Html

    //    DataTable dt = GetCooperate();

    //    strHtml.Append("<table border='0' cellspacing='0' width='60%'><tr><td>");

    //    for (int i = 0; i < number && i < dt.Rows.Count; i++)
    //    {
    //        strHtml.Append("" + dt.Rows[i]["content"].ToString() + "");
    //    }

    //    strHtml.Append("</td></tr><tr><td><br /></td></tr><tr><td>");

    //    for (int i = number; i < dt.Rows.Count && i < number * 2; i++)
    //    {
    //        strHtml.Append("" + dt.Rows[i]["content"].ToString() + "");
    //    }
    //    strHtml.Append("</td></tr></table>");

    //    return strHtml.ToString();
    //}
}