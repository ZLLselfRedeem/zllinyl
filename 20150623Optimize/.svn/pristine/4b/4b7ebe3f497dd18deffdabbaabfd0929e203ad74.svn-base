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
using System.IO;
using CloudStorage;

public partial class ViewAllocWebSite_CorpManage_classicCaseMaintain : System.Web.UI.Page
{
    OfficialWebConfigOperate webOperate = new OfficialWebConfigOperate();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["admin"] == null)
        {
            Response.Redirect("adminLogin.aspx");
        }
        if (img.ImageUrl == "")
        {
            this.img.Visible = false;
        }
        if (!IsPostBack)
        {
            ViewState["number"] = "";

            //说明是Manage页面点击修改按钮跳转而来，反之是新增
            if (Page.Request.QueryString["number"] != null && Page.Request.QueryString["number"].ToString().Length > 0)
            {
                ViewState["number"] = Page.Request.QueryString["number"].ToString();
                InitialEditData(Common.ToInt64(ViewState["number"].ToString()));
            }
            else//新增
            {
                this.txtOrder.Text = (GetMaxOrderFromDB() + 1).ToString();
            }
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            string strOrder = this.txtOrder.Text;
            string strTitle = this.txtTitle.Text.Trim();
            string filename = this.FileUpload2.PostedFile.FileName;

            if (filename != "")
            {
                filename = filename.Substring(filename.LastIndexOf("\\") + 1);

                //对上传文件的大小进行检测，限定文件最大不超过8M
                if (FileUpload2.PostedFile.ContentLength >= 8192000)
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "success", "<script>alert('文件大小超过8M，请重新选择！')</script>");
                    return;
                }

                string newName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + Path.GetExtension(FileUpload2.FileName);
                string objectKey = WebConfig.UploadFiles + WebConfig.WebImages + newName;

                CloudStorageResult result = CloudStorageOperate.PutObject(objectKey, FileUpload2, newName);
                //strContent = objectKey;

                if (result.code)
                {
                    OfficialWebConfig web = new OfficialWebConfig();
                    web.type = (int)VAOfficialWebType.CLASS_CASE;
                    web.title = strTitle;
                    web.content = objectKey;
                    web.sequence = Common.ToInt32(strOrder);
                    web.imageName = newName;
                    web.remark = "";

                    bool saveResult = SaveInfomationToDB(web);
                    if (saveResult)
                    {
                        this.img.ImageUrl = WebConfig.CdnDomain + objectKey;
                        this.img.Visible = true;
                        ClearValue();
                        //this.txtOrder.Text = (GetMaxOrder() + 1).ToString();
                        this.txtOrder.Text = (GetMaxOrderFromDB() + 1).ToString();
                        Page.ClientScript.RegisterStartupScript(GetType(), "success", "<script>alert('保存成功！');</script>");
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(GetType(), "success", "<script>alert('保存失败！')</script>");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "success", "<script>alert('" + ex.Message + "')</script>");
        }
    }

    private void InitialEditData(long id)
    {
        //DataTable dt = GetAllCase();
        //DataRow[] row = dt.Select("number = '" + number + "'");
        DataTable dt = webOperate.QueryOfficialWebConfig(id);
        if (dt.Rows.Count > 0)
        {
            this.txtTitle.Text = dt.Rows[0]["title"].ToString();
            this.txtOrder.Text = dt.Rows[0]["sequence"].ToString();
            this.img.ImageUrl = WebConfig.CdnDomain + WebConfig.UploadFiles + WebConfig.WebImages + dt.Rows[0]["imageName"].ToString();
            this.img.Visible = true;
        }
        else
        {
            ClearValue();
        }
    }

    private DataTable GetAllCase()
    {
        DataTable dt = new DataTable();
        DataRow row;

        dt.Columns.Add("number");
        dt.Columns.Add("title");
        dt.Columns.Add("order");
        dt.Columns.Add("content");

        string fileName = Server.MapPath("../App_Data/classiccase.xml");
        string xPath = "//classiccase//image";

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
                        row[i] = children[i].InnerXml;
                    }
                    dt.Rows.Add(row);
                }
            }
        }
        //dt.DefaultView.Sort = "type desc,date desc";
        //DataTable dt1 = dt.DefaultView.ToTable();

        return dt;
    }

    /// <summary>
    /// 将信息写入XML
    /// </summary>
    /// <param name="order">顺序号</param>
    /// <param name="title">商户名称</param>
    /// <param name="content">图片路径</param>
    /// <returns>执行结果True/False</returns>
    private bool SaveInfomation(string order, string title, string content)
    {
        string fileName = Server.MapPath("../App_Data/classiccase.xml");
        string xPath = "classiccase";
        string xPathChild = "//classiccase//image";
        string xPathNumber = "//classiccase//image//number";
        string oldContent = "";
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
                            node["title"].InnerText = title;
                            node["order"].InnerText = order;
                            if (!string.IsNullOrEmpty(content))
                            {
                                oldContent = node["content"].InnerXml;
                                node["content"].InnerXml = content;
                            }
                            break;
                        }
                    }
                }
                xmlDoc.Save(fileName); //保存到XML文档
                CloudStorageOperate.DeleteObject(oldContent);
            }
            else
            {
                //先创建“image”节点
                SysXmlHelper.CreateXmlNodeByXPath(fileName, xPath, "image", "", "", "");

                //再创建“image”节点的子节点
                int number = maxNumber + 1;//当前最大序号+1

                SysXmlHelper.CreateXmlNodeByXPath(fileName, xPathChild, "number", number.ToString());
                SysXmlHelper.CreateXmlNodeByXPath(fileName, xPathChild, "title", title);
                SysXmlHelper.CreateXmlNodeByXPath(fileName, xPathChild, "order", order);
                SysXmlHelper.CreateXmlNodeByXPath(fileName, xPathChild, "content", content);
            }
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    private void ClearValue()
    {
        this.txtTitle.Text = string.Empty;
        this.txtOrder.Text = string.Empty;
        //this.CKEditor1.Text = string.Empty;
        ViewState["number"] = null;
    }

    /// <summary>
    /// 获取当前最大序号
    /// </summary>
    /// <returns></returns>
    private int GetMaxOrder()
    {
        string order = string.Empty;
        string fileName = Server.MapPath("../App_Data/classiccase.xml");
        string xPathOrder = "//classiccase//image//order";

        DataTable dt = new DataTable();

        dt.Columns.Add("order");
        dt.Columns["order"].DataType = typeof(int);

        DataRow row;

        XmlNodeList List = SysXmlHelper.GetXmlNodeListByXpath(fileName, xPathOrder);

        if (List != null && List.Count > 0)
        {


            for (int i = 0; i < List.Count; i++)
            {
                row = dt.NewRow();
                row[0] = Convert.ToInt32(List.Item(i).InnerText);

                dt.Rows.Add(row);
            }

            dt.DefaultView.Sort = "order desc";
            DataTable dt1 = dt.DefaultView.ToTable();

            return Convert.ToInt32(dt1.Rows[0]["order"].ToString());
        }
        else
        {
            return 0;
        }
    }

    #region 2014-4-22

    public bool SaveInfomationToDB(OfficialWebConfig web)
    {
        int maxNumber = GetMaxOrderFromDB();//当前最大序号

        //先判断是新增还是修改
        if (ViewState["number"] != null && ViewState["number"].ToString() != "")//修改
        {
            web.id = Convert.ToInt32(ViewState["number"]);
            DataTable dtOld = webOperate.QueryOfficialWebConfig(web.id);
            string oldContent = dtOld.Rows[0]["content"].ToString();

            bool update = webOperate.UpdateOfficialWebConfig(web);
            if (update)
            {
                CloudStorageOperate.DeleteObject(oldContent);
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

    private int GetMaxOrderFromDB()
    {
        return webOperate.QueryMaxSquence(VAOfficialWebType.CLASS_CASE);
    }

    #endregion
}