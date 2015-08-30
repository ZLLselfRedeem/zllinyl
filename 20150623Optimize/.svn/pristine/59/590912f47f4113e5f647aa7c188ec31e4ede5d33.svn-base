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

public partial class ViewAllocWebSite_CorpManage_recentNewsManage : System.Web.UI.Page
{
    OfficialWebConfigOperate webOperate = new OfficialWebConfigOperate();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["admin"] == null)
        {
            Response.Redirect("adminLogin.aspx");
        }
        if (!IsPostBack)
        {
            ReadInfomation();
        }
    }

    private void ReadInfomation()
    {
        this.gvRecentNews.DataSource = GetAllRecentNewsFromDB();
        this.gvRecentNews.DataBind();
    }

    protected void lnkbtnEdit_OnCommand(object sender, CommandEventArgs e)
    {
        string number = e.CommandArgument.ToString();

        switch (e.CommandName)
        {
            case "modify":
                Response.Redirect("recentNewsMaintain.aspx?number=" + number + "");
                break;
            case "del":
                DeleteRecentNewsFromDB(Common.ToInt64(number));
                ReadInfomation();
                break;
            default:
                break;
        }
    }

    #region 2014-4-22 Xml Methods
    private DataTable GetAllRecentNewsFromDB()
    {
        DataTable dt = webOperate.QueryOfficialWebConfig(VAOfficialWebType.RECENT_NEWS);
        DataView dv1 = dt.DefaultView;
        dv1.Sort = "date desc";
        return dv1.ToTable();
    }
    /// <summary>
    /// 根据唯一值序号，删除其父节点下所有节点
    /// </summary>
    /// <param name="number"></param>
    private void DeleteRecentNewsFromDB(long id)
    {
        try
        {
            bool delete = webOperate.DeleteOfficialWebConfig(id);
            if (delete)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "success", "<script>alert('删除成功！')</script>");
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "success", "<script>alert('删除失败！')</script>"); 
            }
        }
        catch (Exception)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "success", "<script>alert('删除失败！')</script>");
        }
    }
    #endregion

    #region Xml Methods
    private DataTable GetAllRecentNewsWithType()
    {
        DataTable dt = new DataTable();
        DataRow row;

        dt.Columns.Add("number");
        dt.Columns.Add("type");
        dt.Columns.Add("date");
        dt.Columns.Add("title");

        string fileName = Server.MapPath("../App_Data/recentnews.xml");
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
                    for (int i = 0; i < children.Count - 1; i++)
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
    /// <summary>
    /// 根据唯一值序号，删除其父节点下所有节点
    /// </summary>
    /// <param name="number"></param>
    private void DeleteRecentNewsByNo(string number)
    {
        string fileName = Server.MapPath("../App_Data/recentnews.xml");
        string xPath = "//recentnews//news//number";

        try
        {
            SysXmlHelper.DeleteXmlNodeByXPathAndNode(fileName, xPath, number);

            Page.ClientScript.RegisterStartupScript(GetType(), "success", "<script>alert('删除成功！')</script>");
        }
        catch (Exception)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "success", "<script>alert('删除失败！')</script>");
        }
    }
    #endregion
}