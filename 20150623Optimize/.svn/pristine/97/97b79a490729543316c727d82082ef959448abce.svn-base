using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.WebPageDll;
using VAGastronomistMobileApp.Model;


public partial class VAEncourageConfig_EncourageConfig : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetVAEncourageConfigInfo();
        }
    }
    /// <summary>
    /// 获取财务分类
    /// </summary>
    protected void GetVAEncourageConfigInfo()
    {
        VAEncourageConfigOperate configOperate = new VAEncourageConfigOperate();
        GridView_VAEncourageConfig.DataSource = configOperate.QueryVAEncourageConfig();
        GridView_VAEncourageConfig.DataBind();
    }
    protected void GridView_VAEncourageConfig_SelectedIndexChanged(object sender, EventArgs e)
    {
        Button_AddVAEncourageConfig.CommandArgument = "update";
        TextBox_ConfigName.Text = GridView_VAEncourageConfig.DataKeys[GridView_VAEncourageConfig.SelectedIndex]["configName"].ToString();
        TextBox_ConfigName.Enabled = false;
        TextBox_ConfigDescription.Text = GridView_VAEncourageConfig.DataKeys[GridView_VAEncourageConfig.SelectedIndex]["configDescription"].ToString();
        TextBox_ConfigContent.Text = GridView_VAEncourageConfig.DataKeys[GridView_VAEncourageConfig.SelectedIndex]["configContent"].ToString();
        Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>ConfirmWindow('Panel_VAEncourageConfig');</script>");
    }
    /// <summary>
    /// 添加或者修改财务分类
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button_AddVAEncourageConfig_Click(object sender, EventArgs e)
    {
        VAEncourageConfigOperate configOperate = new VAEncourageConfigOperate();
        VAEncourageConfig config = new VAEncourageConfig();
        config.configName = TextBox_ConfigName.Text;
        config.configDescription = TextBox_ConfigDescription.Text;
        config.configContent = TextBox_ConfigContent.Text;
        config.configMessage = TextBox_configMessage.Text;
        if (Button_AddVAEncourageConfig.CommandArgument == "add")
        {
            configOperate.AddVAEncourageConfig(config);
        }
        else if (Button_AddVAEncourageConfig.CommandArgument == "update")
        {
            int id = Common.ToInt32(GridView_VAEncourageConfig.DataKeys[GridView_VAEncourageConfig.SelectedIndex].Values["id"].ToString());
            config.id = id;
            configOperate.ModifyVAEncourageConfig(config);
        }
        Response.Redirect("EncourageConfig.aspx");
    }
    protected void Button_VAEncourageConfigAdd_Click(object sender, EventArgs e)
    {
        Button_AddVAEncourageConfig.CommandArgument = "add";
        TextBox_ConfigName.Enabled = true;
        Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>ConfirmWindow('Panel_VAEncourageConfig');</script>");
    }
    protected void Button_CancelAddfinancialType_Click(object sender, EventArgs e)
    {
        Response.Redirect("EncourageConfig.aspx");
    }
}