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

public partial class ViewAllocWebSite_CorpManage_recentNewsTypeManage : System.Web.UI.Page
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
            GetOrderListNew("");
            GetAllTypeNew();
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            OfficialWebConfig web = new OfficialWebConfig();
            web.type = (int)VAOfficialWebType.RECENT_NEWS_TYPE;
            web.title = this.txtType.Text.Trim();
            web.sequence = Common.ToInt32(this.ddlOrder.SelectedValue);
            SaveTypeToDB(web);
            GetAllTypeNew();
            GetOrderListNew("");
            //CreateOrUpdateType(this.txtType.Text.Trim(), this.ddlOrder.SelectedValue);
            //GetAllType();
            //GetOrderList("");
            ViewState["number"] = null;
            this.txtType.Text = string.Empty;
            this.ddlOrder.SelectedValue = string.Empty;
        }
        catch (Exception)
        { }
    }

    protected void lnkbtnEdit_OnCommand(object sender, CommandEventArgs e)
    {
        long number = Common.ToInt64(e.CommandArgument.ToString());

        switch (e.CommandName)
        {
            case "modify":
                SetEditValueNew(number);
                break;
            case "del":
                //DeleteType(number);
                //GetAllType();
                DeleteTypeNew(number);
                GetAllTypeNew();
                break;
            default:
                break;
        }
    }

    #region 2014-4-22
    /// <summary>
    /// 新增/修改类别时，生成初始序号
    /// </summary>
    private void GetOrderListNew(string order)
    {
        DataTable dt = webOperate.QueryOfficialWebConfig(VAOfficialWebType.RECENT_NEWS_TYPE);
        int cnt = dt.Rows.Count;

        this.ddlOrder.Items.Clear();
        this.ddlOrder.Items.Add(new ListItem("==请选择==", ""));

        if (ViewState["number"] == null)//新增
        {
            for (int i = 1; i < cnt + 2; i++)
            {
                this.ddlOrder.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }
            this.ddlOrder.SelectedValue = (cnt + 1).ToString();
        }
        else//修改
        {
            for (int i = 1; i < cnt + 1; i++)
            {
                this.ddlOrder.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }
            this.ddlOrder.SelectedValue = order;
        }
    }
    /// <summary>
    /// 读取所有类别信息
    /// </summary>
    private void GetAllTypeNew()
    {
        DataTable dt = webOperate.QueryOfficialWebConfig(VAOfficialWebType.RECENT_NEWS_TYPE);
        if (dt.Rows.Count > 0)
        {
            DataView dv1 = dt.DefaultView;
            dv1.Sort = "sequence asc";
            DataTable dt1 = dv1.ToTable();
            this.gvType.DataSource = dt1;
            this.gvType.DataBind();
        }
        else
        {
            this.gvType.DataSource = null;
            this.gvType.DataBind();
        }
    }
    /// <summary>
    /// 新增/修改类别
    /// </summary>
    /// <param name="web"></param>
    private bool SaveTypeToDB(OfficialWebConfig web)
    {
        //判断是编辑还是新增
        if (ViewState["number"] != null && ViewState["number"].ToString() != "")//编辑
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
        else//新增
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
    /// <summary>
    /// 通过类别序号找到其名称
    /// </summary>
    /// <param name="number"></param>
    private void SetEditValueNew(long id)
    {
        ViewState["number"] = id;
        DataTable dt = webOperate.QueryOfficialWebConfig(id);
        if (dt.Rows.Count > 0)
        {
            this.txtType.Text = dt.Rows[0]["title"].ToString();
            GetOrderListNew(dt.Rows[0]["sequence"].ToString());
        }
    }
    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    private bool DeleteTypeNew(long id)
    {
        return webOperate.DeleteOfficialWebConfig(id);
    }
    #endregion

    #region Xml Methods
    private void DeleteType(string number)
    {
        string fileName = Server.MapPath("../App_Data/recentnewstype.xml");
        string xPath = "//recentnewstype//type";

        SysXmlHelper.DeleteXmlByAttribute(fileName, xPath, "number", number);
    }

    /// <summary>
    /// 通过类别序号找到类别节点具体的值
    /// </summary>
    /// <param name="number"></param>
    private void SetEditValue(string number)
    {
        string fileName = Server.MapPath("../App_Data/recentnewstype.xml");
        string xPath = "//recentnewstype//type";

        ViewState["number"] = number;

        XmlNode node = SysXmlHelper.GetXmlNodeByAttribute(fileName, xPath, "number", number);

        this.txtType.Text = node.InnerText;
        GetOrderList(node.Attributes[1].Value);
    }

    /// <summary>
    /// 读取所有类别信息
    /// </summary>
    private void GetAllType()
    {
        string fileName = Server.MapPath("../App_Data/recentnewstype.xml");
        string xPath = "//recentnewstype//type";

        XmlNodeList List = SysXmlHelper.GetXmlNodeListByXpath(fileName, xPath);

        if (List != null)
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("number");
            dt.Columns.Add("type");
            dt.Columns.Add("order");

            dt.Columns["order"].DataType = typeof(int);

            DataRow row;

            foreach (XmlNode node in List)
            {
                row = dt.NewRow();
                row[0] = node.Attributes[0].Value;
                row[1] = node.InnerText;
                row[2] = Convert.ToInt32(node.Attributes[1].Value);
                dt.Rows.Add(row);
            }

            dt.DefaultView.Sort = "order asc";
            dt = dt.DefaultView.ToTable();

            this.gvType.DataSource = dt;
            this.gvType.DataBind();
        }
    }

    /// <summary>
    /// 新增/修改类别时，生成初始序号
    /// </summary>
    private void GetOrderList(string order)
    {
        string fileName = Server.MapPath("../App_Data/recentnewstype.xml");
        string xPath = "//recentnewstype//type";

        int cnt = SysXmlHelper.GetXmlNodeCount(fileName, xPath);

        this.ddlOrder.Items.Clear();
        this.ddlOrder.Items.Add(new ListItem("==请选择==", ""));

        if (ViewState["number"] == null)//新增
        {
            for (int i = 1; i < cnt + 2; i++)
            {
                this.ddlOrder.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }
            this.ddlOrder.SelectedValue = (cnt + 1).ToString();
        }
        else//修改
        {
            for (int i = 1; i < cnt + 1; i++)
            {
                this.ddlOrder.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }
            this.ddlOrder.SelectedValue = order;
        }
    }

    /// <summary>
    /// 新增/修改类别
    /// </summary>
    /// <param name="typeName"></param>
    /// <param name="typeOrder"></param>
    private void CreateOrUpdateType(string typeName, string typeOrder)
    {
        string fileName = Server.MapPath("../App_Data/recentnewstype.xml");
        string xPath = "recentnewstype";
        string xPathType = "//recentnewstype//type";

        //判断是编辑还是新增
        if (ViewState["number"] != null && ViewState["number"].ToString() != "")//编辑
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(fileName);
            XmlNodeList List = xmlDoc.SelectNodes(xPathType);

            foreach (XmlNode node in List)
            {
                if (node.Attributes[0].Value == ViewState["number"].ToString())//在XML中找到匹配项
                {
                    node.InnerText = typeName;
                    node.Attributes[1].Value = typeOrder;
                    xmlDoc.Save(fileName);
                    break;
                }
            }
        }
        else//新增
        {
            //先创建“recentnewstype”节点
            SysXmlHelper.CreateXmlNodeByXPath(fileName, xPath, "type", typeName, "", "");

            //当前最大序号
            int maxNumber = SysXmlHelper.GetXmlNodeCount(fileName, xPathType);

            //再增加其属性
            SysXmlHelper.CreateOrUpdateXmlAttributeByXPath(fileName, xPathType, typeName, "number", maxNumber.ToString());
            SysXmlHelper.CreateOrUpdateXmlAttributeByXPath(fileName, xPathType, typeName, "order", typeOrder);
        }
    }
    #endregion
}