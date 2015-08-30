using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.WebPageDll;
using Web.Control.DDL;
using VAGastronomistMobileApp.SQLServerDAL;

public partial class OrderOptimization_ChannelManager : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["cityID"] != null)
            {
                string cityID = Request.QueryString["cityID"];
                new CityDropDownList().BindCity(ddlCity, cityID);
            }
            else
            {
                new CityDropDownList().BindCity(ddlCity, "87");
            }
            BindChannelList(Common.ToInt32(ddlCity.SelectedValue));
        }
    }

    /// <summary>
    /// 根据cityID获取增值页（频道）列表
    /// </summary>
    /// <param name="p"></param>
    private void BindChannelList(int cityID)
    {
        DataTable channels = new ChannelManager().SelectChannel(cityID);
        GridView_Channel.DataSource = channels;
        GridView_Channel.DataBind();
    }

    public string GetConfirm(string status, string channelName)
    {
        string returnMsg = null;
        if (status == "True")
        {
            returnMsg = string.Format("return confirm('确定关闭频道{0}吗？')", channelName);
        }
        else
            returnMsg = string.Format("return confirm('确定开启频道{0}吗？')", channelName);
        return returnMsg;
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        int cityID = Common.ToInt32(ddlCity.SelectedValue);
        BindChannelList(cityID);
    }

    protected void Add_Click(object sender, EventArgs e)
    {
        Response.Redirect("ChannelDetail.aspx?cityID=" + ddlCity.SelectedValue);
    }

    protected void GridView_Channel_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;//转换为按钮类型，获取其所在的行的索引
        int id = Common.ToInt32(GridView_Channel.DataKeys[index].Values["id"]);
        int firstTitleID = Common.ToInt32(GridView_Channel.DataKeys[index].Values["firstID"]);
        //int modelType = Common.ToInt32(GridView_Channel.DataKeys[index].Values["modelType"]);
        string status = Common.ToString(GridView_Channel.DataKeys[index].Values["status"]);
        switch (e.CommandName.ToString())
        {
            case "edit":
                Response.Redirect("ChannelDetail.aspx?id=" + id + "&cityID=" + ddlCity.SelectedValue + "&firstTitleID=" + firstTitleID);
                break;
            case "delete":
                ChannelManager manager = new ChannelManager();
                bool deleteReust = manager.RemoveChannel(id);
                if (deleteReust == true && manager.DeteteRelation(id))
                {

                    Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('删除成功！');</script>");
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('删除失败！');</script>");
                }
                BindChannelList(Convert.ToInt32(ddlCity.SelectedValue));
                break;
            case "open":
                int updateResult = 0;
                if (status == "False")
                {
                    updateResult = new ChannelManager().MerchantUpdate(id, 1);
                    if (updateResult >= 1)
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
                    updateResult = new ChannelManager().MerchantUpdate(id, 0);

                    if (updateResult >= 1)
                    {
                        Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('关闭成功！');</script>");
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('关闭失败！');</script>");
                    }
                }
                BindChannelList(Convert.ToInt32(ddlCity.SelectedValue));
                break;
            default:
                break;
        }
    }
    protected void GridView_Channel_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

    }
    protected void GridView_Channel_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void GridView_Channel_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
}