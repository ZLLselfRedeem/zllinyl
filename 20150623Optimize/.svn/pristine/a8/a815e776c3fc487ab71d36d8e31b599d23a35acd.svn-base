using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Data;
using System.Collections;
using VAGastronomistMobileApp.WebPageDll;
using VAGastronomistMobileApp.Model;

public partial class ViewAllocWebSite_CorpManage_updateHistoryManage : System.Web.UI.Page
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
        this.gvUpdateHistory.DataSource = GetAllUpdateHistoryFromDB();
        this.gvUpdateHistory.DataBind();
    }
    protected void lnkbtnEdit_OnCommand(object sender, CommandEventArgs e)
    {
        string number = e.CommandArgument.ToString();

        switch (e.CommandName)
        {
            case "modify":
                Response.Redirect("updateHistoryMaintain.aspx?number=" + number + "");
                break;
            case "del":
                DeleteUpdateHistoryFromDB(Common.ToInt64(number));
                ReadInfomation();
                break;
            default:
                break;
        }
    }

    #region 2014-4-22 DB Methods
    private DataTable GetAllUpdateHistoryFromDB()
    {
        return webOperate.QueryOfficialWebConfig(VAOfficialWebType.UPDATE_HISTORY);
    }
    private bool DeleteUpdateHistoryFromDB(long id)
    {
        return webOperate.DeleteOfficialWebConfig(id);
    }
    #endregion

    #region Xml Methods
    private ArrayList GetUpdateHistoryByNo(string number)
    {
        ArrayList array = new ArrayList();

        string fileName = Server.MapPath("../App_Data/updatehistory.xml");
        string xPath = "//updatehistory//history";

        XmlNodeList List = SysXmlHelper.GetXmlNodeListByXpath(fileName, xPath);

        if (List != null)
        {
            foreach (XmlNode node in List)
            {
                XmlNodeList children = node.ChildNodes;

                if (children != null)
                {
                    if (children[0].InnerText == number)
                    {
                        for (int i = 0; i < children.Count; i++)
                        {
                            array.Add(children[i].InnerText);
                        }
                        break;
                    }
                }
            }
        }
        return array;
    }

    private DataTable GetAllUpdateHistory()
    {
        DataTable dt = new DataTable();
        DataRow row;

        dt.Columns.Add("number");
        dt.Columns.Add("date");
        dt.Columns.Add("version");
        dt.Columns.Add("title");

        string fileName = Server.MapPath("../App_Data/updatehistory.xml");
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
                    for (int i = 0; i < children.Count - 1; i++)
                    {
                        //row[i] = children[i].InnerText;
                        row[i] = children[i].InnerXml;
                    }
                    dt.Rows.Add(row);
                }
            }
        }
        return dt;
    }

    /// <summary>
    /// 根据唯一值序号，删除其父节点下所有节点
    /// </summary>
    /// <param name="number"></param>
    private void DeleteUpdateHistoryByNo(string number)
    {
        string fileName = Server.MapPath("../App_Data/updatehistory.xml");
        string xPath = "//updatehistory//history//number";

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