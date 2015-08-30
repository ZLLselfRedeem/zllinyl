using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL;
using VAGastronomistMobileApp.WebPageDll;

public partial class OrderOptimization_ShopChannelConfig : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["shopID"] != null)
            {
                ShopChannelBinding();
            }
        }
    }

    private void ShopChannelBinding()
    {
        TextBox_MerchantName.InnerText = Common.ToString(Request.QueryString["shopName"])+"增值页管理";
        //TextBox_MerchantName.Font.Size = 14;
        ShopChannelOperate operate = new ShopChannelOperate();
        int shopID = Common.ToInt32(Request.QueryString["shopID"]);
        GridView_ChannelConfig.DataSource = operate.SelectShopChannel(shopID, "back");
        GridView_ChannelConfig.DataBind();
    }

    public string GetConfirm(string status, string channelName)
    {
        string returnMsg = null;
        if (status == "True")
        {
            returnMsg = string.Format("return confirm('确定关闭频道{0}吗？')", channelName);
        }
        else
        {
            returnMsg = string.Format("return confirm('确定开启频道{0}吗？')", channelName);
        }
        return returnMsg;
    }
    protected void GridView_ChannelConfig_RowCommand(object sender, GridViewCommandEventArgs e)
     {
        int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;//转换为按钮类型，获取其所在的行的索引
        int shopID = Common.ToInt32(Request.QueryString["shopID"]);
        string shopName = Common.ToString(Request.QueryString["shopName"]);
        int shopChannelID = Common.ToInt32(GridView_ChannelConfig.DataKeys[index].Values["id"]);
        int channelIndex = Common.ToInt32(GridView_ChannelConfig.DataKeys[index].Values["channelIndex"]);
        string channelName = Common.ToString(GridView_ChannelConfig.DataKeys[index].Values["name"]);
        string status = Common.ToString(GridView_ChannelConfig.DataKeys[index].Values["status"]);
        switch (e.CommandName.ToString())
        {
            case "config":
                ShopChannelDishManager manager = new ShopChannelDishManager();
                int result = manager.NoPublicDelete(shopChannelID);
                Response.Redirect("ChannelConfig.aspx?id=" + shopChannelID + "&channelIndex=" + channelIndex + "&shopID=" + shopID + "&shopName=" + shopName + "&channelName=" + channelName);
                break;
            case "open":
                bool updateResult = false;
                if (status == "True")
                {
                    updateResult = new ShopChannelManager().UpdateStatus(shopChannelID, 0);
                    if (updateResult)
                    {
                        Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('关闭成功！');</script>");
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('关闭失败！');</script>");
                    }
                }
                else
                {
                    if (ShopChannelOperate.ChannelIndexIsClash(Common.ToInt32(Request.QueryString["shopID"]), channelIndex))
                    {
                        Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('开启失败，增值页排序冲突！');</script>");
                    }
                    else
                    {
                        ShopChannel channel = ShopChannelOperate.SelectChannelByID(shopChannelID);
                        if (channel.IsAuthorization)
                        {
                            updateResult = new ShopChannelManager().UpdateStatus(shopChannelID, 1);
                            if (updateResult)
                            {
                                Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('开启成功！');</script>");
                            }
                            else
                            {
                                Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('开启失败！');</script>");
                            }
                        }
                        else
                        {
                            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('该增值服务未授权无法开启，必须在后台网站先授权！');</script>");
                        }
                    }
                }
                ShopChannelBinding();
                break;
            default:
                break;
        }
    }

    protected void GridView_ChannelConfig_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void GridView_ChannelConfig_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void GridView_ChannelConfig_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

    }
}