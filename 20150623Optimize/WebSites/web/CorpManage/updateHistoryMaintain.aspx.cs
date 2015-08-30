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

public partial class ViewAllocWebSite_CorpManage_updateHistoryMaintain : System.Web.UI.Page
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
            ViewState["number"] = "";

            //说明是Manage页面点击修改按钮跳转而来，反之是新增
            if (Page.Request.QueryString["number"] != null && Page.Request.QueryString["number"].ToString().Length > 0)
            {
                ViewState["number"] = Page.Request.QueryString["number"].ToString();
                InitialEditData(ViewState["number"].ToString());
            }
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            OfficialWebConfig web = new OfficialWebConfig();
            web.type = (int)VAOfficialWebType.UPDATE_HISTORY;
            web.title = this.txtTitle.Text.Trim();
            web.date = this.txtDate.Text.Trim();
            web.content = this.txbContent.Text.Trim();
            web.remark = this.txtVersion.Text.Trim();
            //string strDate = this.txtDate.Text.Trim();
            //string strVersion = this.txtVersion.Text.Trim();
            //string strTitle = this.txtTitle.Text.Trim();
            //string strContent = this.CKEditor1.Text.Trim();
            //strContent = Common.HtmlDiscodeForCKEditor(strContent);
            //if (SaveInfomation(strDate, strVersion, strTitle, strContent))

            if (SaveInfomationToDB(web))
            {
                ClearValue();
                Page.ClientScript.RegisterStartupScript(GetType(), "success", "<script>alert('保存成功！')</script>");
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "success", "<script>alert('保存失败！')</script>");
            }
        }
        catch (Exception)
        { }
    }

    private void InitialEditData(string number)
    {
        //DataTable dt = GetAllHistory();
        //DataRow[] row = dt.Select("number = '" + number + "'");
        DataTable dt = webOperate.QueryOfficialWebConfig(Common.ToInt64(number));
        if (dt.Rows.Count > 0)
        {
            this.txtDate.Text = dt.Rows[0]["date"].ToString();
            this.txtVersion.Text = dt.Rows[0]["remark"].ToString();
            this.txtTitle.Text = dt.Rows[0]["title"].ToString();
            this.txbContent.Text = dt.Rows[0]["content"].ToString();
            //this.CKEditor1.Text = row[0]["content"].ToString();
        }
        else
        {
            ClearValue();
        }
    }

    private void ClearValue()
    {
        this.txtDate.Text = string.Empty;
        this.txtVersion.Text = string.Empty;
        this.txtTitle.Text = string.Empty;
        this.txbContent.Text = string.Empty;
        //this.CKEditor1.Text = string.Empty;
        ViewState["number"] = null;
    }
    #region 2014-4-22 DB Methods
    private DataTable GetAllHistoryFormDB()
    {
        return webOperate.QueryOfficialWebConfig(VAOfficialWebType.UPDATE_HISTORY);
    }
    private int GetMaxOrderFromDB()
    {
        return webOperate.QueryMaxSquence(VAOfficialWebType.UPDATE_HISTORY);
    }
    private bool SaveInfomationToDB(OfficialWebConfig web)
    {
        try
        {
            //int maxNumber = GetMaxOrderFromDB();//当前最大序号
            //先判断是新增还是修改
            if (ViewState["number"] != null && ViewState["number"].ToString() != "")//修改
            {
                web.id = Convert.ToInt32(ViewState["number"]);
                bool update = webOperate.UpdateOfficialWebConfig(web);
                if (update)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                int insert = webOperate.InsertOfficialWebConfig(web);
                if (insert > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        catch (Exception)
        {
            return false;
        }
    }
    #endregion
    #region Xml Methods
    private DataTable GetAllHistory()
    {
        DataTable dt = new DataTable();
        DataRow row;

        dt.Columns.Add("number");
        dt.Columns.Add("date");
        dt.Columns.Add("version");
        dt.Columns.Add("title");
        dt.Columns.Add("content");

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
    private bool SaveInfomation(string date, string version, string title, string content)
    {
        string fileName = Server.MapPath("../App_Data/updatehistory.xml");
        string xPath = "updatehistory";
        string xPathChild = "//updatehistory//history";
        string xPathNumber = "//updatehistory//history//number";

        try
        {
            int maxNumber = SysXmlHelper.GetXmlNodeMaxNumber(fileName, xPathNumber);//当前最大序号

            //先判断是新增还是修改
            if (ViewState["number"] != null && ViewState["number"].ToString() != "")//修改
            {

                XmlDocument xmlDoc = new XmlDocument();

                xmlDoc.Load(fileName); //加载XML文档
                XmlNodeList xmlNodeList = xmlDoc.SelectNodes(xPathChild);

                if (xmlNodeList != null)
                {
                    //遍历xpath节点下的所有子节点
                    foreach (XmlNode node in xmlNodeList)
                    {
                        if (node["number"].InnerText.ToString() == ViewState["number"].ToString())//XML中找到匹配项
                        {
                            node["date"].InnerText = date;
                            node["version"].InnerText = version;
                            node["title"].InnerText = title;
                            //node["content"].InnerText = content;
                            node["content"].InnerXml = content;
                            break;
                        }
                    }
                }
                xmlDoc.Save(fileName); //保存到XML文档
            }
            else
            {
                //先创建“history”节点
                SysXmlHelper.CreateXmlNodeByXPath(fileName, xPath, "history", "", "", "");

                //再创建“history”节点的子节点
                int number = maxNumber + 1;//当前最大序号+1

                SysXmlHelper.CreateXmlNodeByXPath(fileName, xPathChild, "number", number.ToString());
                SysXmlHelper.CreateXmlNodeByXPath(fileName, xPathChild, "date", date);
                SysXmlHelper.CreateXmlNodeByXPath(fileName, xPathChild, "version", version);
                SysXmlHelper.CreateXmlNodeByXPath(fileName, xPathChild, "title", title);
                SysXmlHelper.CreateXmlNodeByXPath(fileName, xPathChild, "content", content);

            }
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
    #endregion endregion
}