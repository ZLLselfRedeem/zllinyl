using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.WebPageDll;
using VAGastronomistMobileApp.Model;

public partial class SystemConfig_SystemConfig : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetSystemConfigInfo();
        }
    }
    /// <summary>
    /// 获取系统配置信息
    /// </summary>
    protected void GetSystemConfigInfo()
    {
        SystemConfigOperate configOperate = new SystemConfigOperate();
        GridView_SystemConfig.DataSource = configOperate.QuerySystemConfig(txt_configName.Text.Trim(), txt_configDescription.Text.Trim());
        GridView_SystemConfig.DataBind();
    }
    protected void GridView_SystemConfig_SelectedIndexChanged(object sender, EventArgs e)
    {
        Button_AddSystemConfig.CommandArgument = "update";
        TextBox_ConfigName.Text = GridView_SystemConfig.DataKeys[GridView_SystemConfig.SelectedIndex]["configName"].ToString();
        TextBox_ConfigName.Enabled = false;
        TextBox_ConfigDescription.Text = GridView_SystemConfig.DataKeys[GridView_SystemConfig.SelectedIndex]["configDescription"].ToString();
        TextBox_ConfigContent.Text = GridView_SystemConfig.DataKeys[GridView_SystemConfig.SelectedIndex]["configContent"].ToString();
        Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>ConfirmWindow('Panel_SystemConfig');</script>");
    }
    /// <summary>
    /// 添加或者修改财务分类
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button_AddSystemConfig_Click(object sender, EventArgs e)
    {
        SystemConfigOperate configOperate = new SystemConfigOperate();
        SystemConfigInfo config = new SystemConfigInfo();
        config.configName = TextBox_ConfigName.Text;
        config.configDescription = TextBox_ConfigDescription.Text;
        config.configContent = TextBox_ConfigContent.Text;
        if (Button_AddSystemConfig.CommandArgument == "add")
        {
            configOperate.AddSystemConfig(config);
        }
        else if (Button_AddSystemConfig.CommandArgument == "update")
        {
            int Id = Common.ToInt32(GridView_SystemConfig.DataKeys[GridView_SystemConfig.SelectedIndex].Values["Id"].ToString());
            config.Id = Id;
            configOperate.ModifySystemConfig(config);
        }
        Response.Redirect("SystemConfig.aspx");
    }
    protected void Button_SystemConfigAdd_Click(object sender, EventArgs e)
    {
        Button_AddSystemConfig.CommandArgument = "add";
        TextBox_ConfigName.Enabled = true;
        Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>ConfirmWindow('Panel_SystemConfig');</script>");
    }
    protected void Button_CancelAddfinancialType_Click(object sender, EventArgs e)
    {
        Response.Redirect("SystemConfig.aspx");
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        SystemConfigOperate configOperate = new SystemConfigOperate();
        GridView_SystemConfig.DataSource = configOperate.QuerySystemConfig(txt_configName.Text.Trim(), txt_configDescription.Text.Trim());
        GridView_SystemConfig.DataBind();
    }
}