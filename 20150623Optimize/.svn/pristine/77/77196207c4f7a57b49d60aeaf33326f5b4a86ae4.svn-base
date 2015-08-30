using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Data;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.WebPageDll;
using CloudStorage;

public partial class ViewAllocWebSite_CorpManage_cooperateManage : System.Web.UI.Page
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
        this.gvCooperate.DataSource = GetAllCooperateFromDB();// GetAllCooperate();
        this.gvCooperate.DataBind();
    }

    private DataTable GetAllCooperate()
    {
        DataTable dt = new DataTable();
        DataRow row;

        dt.Columns.Add("number");
        dt.Columns.Add("title");
        dt.Columns.Add("order");
        dt.Columns.Add("content");

        dt.Columns["order"].DataType = typeof(int);

        string fileName = Server.MapPath("../App_Data/cooperate.xml");
        string xPath = "//cooperate//image";

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
                            //order要排序
                            case 2:
                                row[i] = Convert.ToInt32(children[i].InnerText);
                                break;
                            //content要修改图片尺寸
                            case 3:
                                //    //去掉图片中原本的style
                                //    row[i] = children[i].InnerXml.Remove(children[i].InnerXml.IndexOf("style"), (children[i].InnerXml.Length - children[i].InnerXml.IndexOf("style") - 2));
                                //    //添加新的style
                                //    row[i] = row[i].ToString().Insert(row[i].ToString().Length - 2, "style=\"width: 60px; height: 60px;\" ");
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

    protected void lnkbtnEdit_OnCommand(object sender, CommandEventArgs e)
    {
        string number = e.CommandArgument.ToString();

        switch (e.CommandName)
        {
            case "modify":
                Response.Redirect("cooperateMaintain.aspx?number=" + number + "");
                break;
            case "del":
                //DeleteCooperateByNo(number);
                DeleteCooperateById(Common.ToInt64(number));
                ReadInfomation();
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// 根据唯一值序号，删除其父节点下所有节点
    /// </summary>
    /// <param name="number"></param>
    private void DeleteCooperateByNo(string number)
    {
        string fileName = Server.MapPath("../App_Data/cooperate.xml");
        string xPath = "//cooperate//image//number";

        try
        {
            ////先删除XML中图片信息
            //SysXmlHelper.DeleteXmlNodeByXPathAndNode(fileName, xPath, number);

            ////通过序号获取路径
            ////string imagePath = GetImagePathByNo(number);
            ////再删除图片
            ////File.Delete(Server.MapPath(imagePath));

            //Page.ClientScript.RegisterStartupScript(GetType(), "success", "<script>alert('删除成功！')</script>");

            //通过序号获取路径
            string imagePath = GetImagePathByNo(number);

            //先删除云空间中图片
            
            CloudStorageResult deleteResult = CloudStorageOperate.DeleteObject(imagePath);
            if (deleteResult.code)
            {
                //再删除XML中图片信息
                SysXmlHelper.DeleteXmlNodeByXPathAndNode(fileName, xPath, number);
                Page.ClientScript.RegisterStartupScript(GetType(), "success", "<script>alert('删除成功！')</script>");
            }
        }
        catch (Exception)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "success", "<script>alert('删除失败！')</script>");
        }
    }

    private string GetImagePathByNo(string number)
    {
        string path = "";
        string fileName = Server.MapPath("../App_Data/cooperate.xml");
        string xPath = "//cooperate//image//number";

        XmlNodeList List = SysXmlHelper.GetXmlNodeListByXpath(fileName, xPath);

        foreach (XmlNode node in List)
        {
            if (node.InnerText == number)
            {
                path = node.NextSibling.NextSibling.InnerText;
                break;
            }
        }
        return path;
    }
    #region 2014-4-22

    private DataTable GetAllCooperateFromDB()
    {
        DataTable dt = webOperate.QueryOfficialWebConfig(VAOfficialWebType.COOPERATE);
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i]["content"] = WebConfig.CdnDomain + dt.Rows[i]["content"];
            }
        }
        return dt; 
    }
    private string GetImagePathById(long id)
    {
        DataTable dt = webOperate.QueryOfficialWebConfig(id);
        if (dt.Rows.Count > 0)
        {
            return dt.Rows[0]["content"].ToString();
        }
        else
        {
            return "";
        }
    }
    private void DeleteCooperateById(long id)
    {
        try
        {
            //通过序号获取路径
            string imagePath = GetImagePathById(id);
            //先删除云空间中图片
            CloudStorageResult cloudStorageResult = CloudStorageOperate.DeleteObject(imagePath);
            if (cloudStorageResult.code)
            {
                //再删除DB中此条数据
                bool dbResult = webOperate.DeleteOfficialWebConfig(id);
                if (dbResult)
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "success", "<script>alert('删除成功！')</script>");
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "success", "<script>alert('图片删除成功，数据库信息更新失败！')</script>");
                }
            }
        }
        catch (Exception)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "success", "<script>alert('删除失败！')</script>");
        }
    }
    #endregion
}