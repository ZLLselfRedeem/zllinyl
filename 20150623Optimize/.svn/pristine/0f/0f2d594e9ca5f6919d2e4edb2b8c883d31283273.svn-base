using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.Model;
using System.Data;
using VAGastronomistMobileApp.WebPageDll;
using CloudStorage;
using Web.Control.Enum;

public partial class SystemConfig_installConfig : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindGridView();
        }
    }
    /// <summary>
    /// 切换操作模块
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void rbt_CheckedChanged(object sender, EventArgs e)
    {
        BindGridView();
    }
    /// <summary>
    /// App type
    /// </summary>
    void QueryType()
    {
        if (rbt_App.Checked)
        {
            ddl_type.DataSource = EnumHelper.EnumToList(typeof(VAAppType));
        }
        else
        {
            ddl_type.DataSource = EnumHelper.EnumToList(typeof(VAServiceType));
        }
        ddl_type.DataTextField = "Text";
        ddl_type.DataValueField = "Value";
        ddl_type.DataBind();
    }
    /// <summary>
    /// 绑定列表数据信息
    /// </summary>
    void BindGridView()
    {
        Panel_List.Visible = true;
        Panel_Add.Visible = false;
        InstallVersionOperate operate = new InstallVersionOperate();
        DataTable dtInstall = new DataTable();
        if (rbt_App.Checked)
        {
            dtInstall = operate.QueryAppLatestBuild();//
        }
        else
        {
            dtInstall = operate.QueryServiceLatestBuild();
        }
        GridView_List.DataSource = dtInstall;
        GridView_List.DataBind();
        for (int i = 0; i < GridView_List.Rows.Count; i++)
        {
            Label lb_type = GridView_List.Rows[i].FindControl("lb_type") as Label;
            if (rbt_App.Checked)
            {
                lb_type.Text = EnumHelper.EnumToList(typeof(VAAppType)).FirstOrDefault(x => x.Value == GridView_List.DataKeys[i].Values["type"].ToString()).Text;
            }
            else
            {
                lb_type.Text = EnumHelper.EnumToList(typeof(VAServiceType)).FirstOrDefault(x => x.Value == GridView_List.DataKeys[i].Values["type"].ToString()).Text;
            }
        }
    }
    /// <summary>
    /// 列表信息修改
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GridView_List_SelectedIndexChanged(object sender, EventArgs e)
    {
        Panel_Add.Visible = true;
        Panel_List.Visible = false;
        QueryType();
        txt_latestBuild.Text = GridView_List.DataKeys[GridView_List.SelectedIndex].Values["latestBuild"].ToString();
        txt_latestUpdateDescription.Text = GridView_List.DataKeys[GridView_List.SelectedIndex].Values["latestUpdateDescription"].ToString();
        txt_latestUpdateUrl.Text = GridView_List.DataKeys[GridView_List.SelectedIndex].Values["latestUpdateUrl"].ToString();
        txt_oldBuildSupport.Text = GridView_List.DataKeys[GridView_List.SelectedIndex].Values["oldBuildSupport"].ToString();
        ddl_type.SelectedValue = GridView_List.DataKeys[GridView_List.SelectedIndex].Values["type"].ToString();
        Button_Save.CommandName = GridView_List.DataKeys[GridView_List.SelectedIndex].Values["id"].ToString();
        if (GridView_List.DataKeys[GridView_List.SelectedIndex].Values["type"].ToString() == "1" || GridView_List.DataKeys[GridView_List.SelectedIndex].Values["type"].ToString() == "2")
        {
            table.Attributes.CssStyle.Add("display", "none");
        }
        else
        {
            table.Attributes.CssStyle.Add("display", "block");
        }
    }
    /// <summary>
    /// 保存修改版本信息
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button_Save_Click(object sender, EventArgs e)
    {
        if (String.IsNullOrEmpty(txt_latestBuild.Text.Trim()) || String.IsNullOrEmpty(txt_latestUpdateDescription.Text.Trim()) ||
            String.IsNullOrEmpty(txt_latestUpdateUrl.Text.Trim()) || String.IsNullOrEmpty(txt_oldBuildSupport.Text.Trim()))
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('填写信息有误');</script>");
            return;
        }
        //对应DB两个不同的Table，需要两种case
        int id = Common.ToInt32(Button_Save.CommandName);
        InstallVersionOperate operate = new InstallVersionOperate();
        bool flag = false;
        if (rbt_App.Checked)
        {
            AppBuildInfo app = new AppBuildInfo();
            app.id = id;
            app.latestBuild = txt_latestBuild.Text.Trim();
            app.latestUpdateDescription = txt_latestUpdateDescription.Text.Trim();
            app.latestUpdateUrl = txt_latestUpdateUrl.Text.Trim();
            app.oldBuildSupport = txt_oldBuildSupport.Text.Trim();
            app.type = Common.ToInt32(ddl_type.SelectedValue);
            app.updateTime = DateTime.Now;
            flag = operate.ModifyAppBuildInfo(app);
        }
        else
        {
            ServiceBuildInfo service = new ServiceBuildInfo();
            service.id = id;
            service.latestBuild = txt_latestBuild.Text.Trim();
            service.latestUpdateDescription = txt_latestUpdateDescription.Text.Trim();
            service.latestUpdateUrl = txt_latestUpdateUrl.Text.Trim();
            service.oldBuildSupport = txt_oldBuildSupport.Text.Trim();
            service.type = Common.ToInt32(ddl_type.SelectedValue);
            service.updateTime = DateTime.Now;
            flag = operate.ModifyServiceLatestBuild(service);
        }
        if (flag)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('修改成功');</script>");
            BindGridView();//重新绑定列表
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('修改失败');</script>");
        }
    }
    protected void Button_Back_Click(object sender, EventArgs e)
    {
        Panel_Add.Visible = false;
        Panel_List.Visible = true;
    }
    /// <summary>
    /// 上传程序包操作
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ButtonBig_Click(object sender, EventArgs e)
    {
        UploadImg(Big_File);
    }
    protected void UploadImg(FileUpload fileUpload)
    {
        string fileName = Server.HtmlEncode(fileUpload.FileName);//上传的文件
        if (!string.IsNullOrEmpty(fileName))
        {   
            string objectKey = WebConfig.UploadFiles + fileName;//图片在数据库中的实际位置
            CloudStorageResult result = CloudStorageOperate.PutObject(objectKey, fileUpload, fileName);
            if (result.code)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('上传成功');</script>");
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('上传失败，请重试');</script>");
            }
        }
    }
}