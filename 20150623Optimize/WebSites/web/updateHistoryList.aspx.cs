using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Data;
using VAGastronomistMobileApp.WebPageDll;
using VAGastronomistMobileApp.Model;

public partial class ViewAllocWebSite_updateHistoryList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //this.rpHistory.DataSource = GetHistoryDataTable();
            //this.rpHistory.DataBind();

            GetPage();
        }
    }

    private DataTable GetHistoryDataTableFromDB()
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
        }
        return dt1;
    }
    /// <summary>
    /// 先从XML中抓取所有动态,并按时间逆序排序
    /// </summary>
    /// <returns></returns>
    private DataTable GetHistoryDataTable()
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
                            row[i] = children[i].InnerXml; 
                        }
                    }
                    dt.Rows.Add(row);
                }
            }
            dt.DefaultView.Sort = "date desc,number desc";
            dt1 = dt.DefaultView.ToTable();

            //dt1 = dt1.AsEnumerable().Take(number).CopyToDataTable<DataRow>();//获取DataTable前N条
        }

        return dt1;
    }


    private void GetPage()
    {
        PagedDataSource pds = new PagedDataSource();
        //pds.DataSource = GetHistoryDataTable().DefaultView;
        pds.DataSource = GetHistoryDataTableFromDB().DefaultView;

        pds.AllowPaging = true;

        pds.PageSize = 10;

        int currentPage = Convert.ToInt32(Request["p"]);

        //设当前页
        pds.CurrentPageIndex = currentPage;

        //设几个超链接
        if (!pds.IsFirstPage)
        {
            lnkUp.NavigateUrl = Request.CurrentExecutionFilePath + "?p=" + (currentPage - 1);
        }

        if (!pds.IsLastPage)
        {
            lnkDown.NavigateUrl = Request.CurrentExecutionFilePath + "?p=" + (currentPage + 1);
        }

        lbl_info.Text = "第" + (currentPage + 1) + "页、共" + pds.PageCount + "页";

        rpHistory.DataSource = pds;
        rpHistory.DataBind();
    }
}