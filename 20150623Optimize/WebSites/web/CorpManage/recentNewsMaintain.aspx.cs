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

public partial class ViewAllocWebSite_CorpManage_recentNewsMaintain : System.Web.UI.Page
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
            //DropDownListDataBind(GetAllType());
            DropDownListDataBind(GetAllTypeNew());

            ViewState["number"] = "";

            //说明是Manage页面点击修改按钮跳转而来，反之是新增
            if (Page.Request.QueryString["number"] != null && Page.Request.QueryString["number"].ToString().Length > 0)
            {
                ViewState["number"] = Page.Request.QueryString["number"].ToString();
                //InitialEditData(ViewState["number"].ToString());
                InitialEditDataNew(Common.ToInt64(ViewState["number"]));
            }
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        OfficialWebConfig web = new OfficialWebConfig();
        web.type = (int)VAOfficialWebType.RECENT_NEWS;
        web.title = this.txtTitle.Text.Trim();
        web.date = this.txtDate.Text.Trim();
        web.content = this.txbContent.Text.Trim();
        web.remark = this.ddlType.SelectedValue.ToString();
        //string strType = this.ddlType.SelectedValue.ToString();
        //string strDate = this.txtDate.Text.Trim();
        //string strTitle = this.txtTitle.Text.Trim();
        //string strContent = this.txbContent.Text.Trim();
        //string strContent = this.CKEditor1.Text.Trim();
        //strContent =Common.HtmlDiscodeForCKEditor(strContent);
        //if (SaveInfomation(strType, strDate, strTitle, strContent))

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
    
    /// <summary>
    /// 绑定所有类别
    /// </summary>
    /// <param name="dt">从XML中读取的类别的DataTable</param>
    private void DropDownListDataBind(DataTable dt)
    {
        if (dt != null)
        {
            this.ddlType.Items.Clear();
            this.ddlType.Items.Add(new ListItem("==请选择==", ""));

            foreach (DataRow dr in dt.Rows)
            {
                this.ddlType.Items.Add(new ListItem(dr["title"].ToString(), dr["title"].ToString()));
            }
        }
    }

    private void ClearValue()
    {
        this.ddlType.SelectedValue = string.Empty;
        this.txtDate.Text = string.Empty;
        this.txtTitle.Text = string.Empty;
        this.txbContent.Text = string.Empty;
        //this.CKEditor1.Text = string.Empty;
        ViewState["number"] = null;
    }
    
    #region 2014-4-22
    /// <summary>
    /// 读取所有类别信息
    /// </summary>
    private DataTable GetAllTypeNew()
    {
        return webOperate.GetRecentNewsType();
    }
    private void InitialEditDataNew(long id)
    {
        DataTable dt = webOperate.QueryOfficialWebConfig(id);
        if (dt.Rows.Count > 0)
        {
            this.ddlType.SelectedValue = dt.Rows[0]["remark"].ToString();
            this.txtDate.Text = dt.Rows[0]["date"].ToString();
            this.txtTitle.Text = dt.Rows[0]["title"].ToString();
            this.txbContent.Text = dt.Rows[0]["content"].ToString();
            //this.CKEditor1.Text = dt.Rows[0]["content"].ToString();
        }
        else
        {
            ClearValue();
        }
    }
    private int GetMaxOrderFromDB()
    {
        return webOperate.QueryMaxSquence(VAOfficialWebType.RECENT_NEWS);
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
    #region XML Method
    private void InitialEditData(string number)
    {
        DataTable dt = GetAllRecentNews();
        DataRow[] row = dt.Select("number = '" + number + "'");

        this.ddlType.SelectedValue = row[0]["type"].ToString();
        this.txtDate.Text = row[0]["date"].ToString();
        this.txtTitle.Text = row[0]["title"].ToString();
        //this.CKEditor1.Text = row[0]["content"].ToString();
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
    private DataTable GetAllType()
    {
        DataTable dt = new DataTable();
        string fileName = Server.MapPath("../App_Data/recentnewstype.xml");
        string xPath = "//recentnewstype//type";

        XmlNodeList List = SysXmlHelper.GetXmlNodeListByXpath(fileName, xPath);


        if (List != null)
        {
            dt.Columns.Add("number");
            dt.Columns.Add("type");
            dt.Columns.Add("order");

            DataRow row;

            foreach (XmlNode node in List)
            {
                row = dt.NewRow();
                row[0] = node.Attributes[0].Value;
                row[1] = node.InnerText;
                row[2] = node.Attributes[1].Value;
                dt.Rows.Add(row);
            }

            dt.DefaultView.Sort = "order asc";
            dt = dt.DefaultView.ToTable();

        }
        return dt;
    }
    private bool SaveInfomation(string type, string date, string title, string content)
    {
        string fileName = Server.MapPath("../App_Data/recentnews.xml");
        string xPath = "recentnews";
        string xPathChild = "//recentnews//news";
        string xPathNumber = "//recentnews//news//number";

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
                            node["type"].InnerText = type;
                            node["date"].InnerText = date;
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
                //先创建“news”节点
                SysXmlHelper.CreateXmlNodeByXPath(fileName, xPath, "news", "", "", "");

                //再创建“news”节点的子节点
                int number = maxNumber + 1;//当前最大序号+1

                SysXmlHelper.CreateXmlNodeByXPath(fileName, xPathChild, "number", number.ToString());
                SysXmlHelper.CreateXmlNodeByXPath(fileName, xPathChild, "type", type);
                SysXmlHelper.CreateXmlNodeByXPath(fileName, xPathChild, "date", date);
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
    #endregion
}