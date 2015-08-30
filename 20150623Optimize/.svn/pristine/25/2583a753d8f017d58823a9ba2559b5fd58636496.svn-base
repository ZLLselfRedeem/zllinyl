using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL;
using VAGastronomistMobileApp.WebPageDll;

public partial class OrderOptimization_ChannelConfig : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["id"] != null)
            {

                if (Request.QueryString["shopID"] != null)
                {
                    int shopID = Common.ToInt32(Request.QueryString["shopID"]);
                }
                //if (Request.QueryString["shopChannelDishID"] != null)
                //{
                //    int shopChannelDishID = Common.ToInt32(Request.QueryString["shopChannelDishID"]);
                //    dishEditRecord.Add(shopChannelDishID);
                //}
                ChannelBind();
            }
        }
    }

    private void ChannelBind()
    {
        int channelID = Convert.ToInt32(Request.QueryString["id"]);
        string channelIndex = Convert.ToString(Request.QueryString["channelIndex"]);
        string shopName = Convert.ToString(Request.QueryString["shopName"]);
        string channelName = Common.ToString(Request.QueryString["channelName"]);
        TextBox_MerchantName.InnerText = shopName + "  增值栏目：" + channelName;
        //TextBox_MerchantName.Font.Size = 14;
        TextBox_Index.Text = channelIndex;
        DataTable dt = new ShopChannelDishOperate().SelectBack(channelID);
        GridView_ChannelConfig.DataSource = dt;
        GridView_ChannelConfig.DataBind();
    }

    protected void GridView_ChannelConfig_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;//转换为按钮类型，获取其所在的行的索引
        int shopChannelDishID = Common.ToInt32(GridView_ChannelConfig.DataKeys[index].Values["id"]);
        int shopChannelDishIndex = Common.ToInt32(GridView_ChannelConfig.DataKeys[index].Values["dishIndex"]);
        string shopName = Convert.ToString(Request.QueryString["shopName"]);
        string channelName = Common.ToString(Request.QueryString["channelName"]);
        switch (e.CommandName.ToString())
        {
            case "delete":
                ShopChannelDishOperate operate = new ShopChannelDishOperate();
                //dishEditRecord.Add(shopChannelDishID);
                ShopChannelDish dish = ShopChannelDishOperate.Search(shopChannelDishID);
                dish.isDelete = true;
                operate.Insert(dish);
                bool result = operate.Delete(shopChannelDishID);
                if (result)
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('删除成功！');</script>");
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('删除失败！');</script>");
                }
                ChannelBind();
                break;
            case "edit":
                Response.Redirect("ShopChannelDishDetail.aspx?shopChannelDishID=" + shopChannelDishID + "&dishIndex=" + shopChannelDishIndex + "&channelID=" + Common.ToInt32(Request.QueryString["id"]) + "&shopID=" + Convert.ToInt32(Request.QueryString["shopID"]) + "&channelIndex=" + TextBox_Index.Text + "&shopName=" + shopName + "&channelName=" + channelName);
                break;
            default:
                break;
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        int shopID = Common.ToInt32(Request.QueryString["shopID"]);
        string shopName = Common.ToString(Request.QueryString["shopName"]);
        Response.Redirect("ShopChannelConfig.aspx?shopID=" + shopID + "&shopName=" + shopName);
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        int channelID = Convert.ToInt32(Request.QueryString["id"]);
        int initIndex = Convert.ToInt32(Request.QueryString["channelIndex"]);
        int channelIndex = Convert.ToInt32(TextBox_Index.Text);
        int shopID = Common.ToInt32(Request.QueryString["shopID"]);
        string shopName = Common.ToString(Request.QueryString["shopName"]);
        if (initIndex != channelIndex && ShopChannelOperate.ChannelIndexIsClash(shopID, channelIndex))
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('发布失败，增值页排序冲突！');unbindunbeforunload();</script>");
        }
        else
        {
            bool flag = true;
            bool result = true;
            if (initIndex != channelIndex)
            {
                flag = ShopChannelOperate.UpdateIndex(channelID, channelIndex);
            }
            
            result = ShopChannelDishOperate.Public(channelID);
            
            if (result && flag)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('发布成功！');unbindunbeforunload();window.location.href='ShopChannelConfig.aspx?shopID=" + shopID + "&shopName=" + shopName + "'</script>");
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('发布失败！');unbindunbeforunload();</script>");
            }
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        ShopChannelDishOperate operate = new ShopChannelDishOperate();
        bool deleteResult = true;
        bool flag = false;
        for (int i = 0; i < GridView_ChannelConfig.Rows.Count; i++)
        {
            HtmlInputCheckBox selects = (HtmlInputCheckBox)GridView_ChannelConfig.Rows[i].FindControl("ckbSelect");

            if (selects.Checked)
            {
                flag = true;
                int shopChannelDishID = Convert.ToInt32(GridView_ChannelConfig.DataKeys[i].Values["id"]);
                //dishEditRecord.Add(shopChannelDishID);
                ShopChannelDish dish = ShopChannelDishOperate.Search(shopChannelDishID);
                dish.isDelete = true;
                operate.Insert(dish);
                if (!operate.Delete(shopChannelDishID))
                {
                    deleteResult = false;
                    break;
                }
            }
        }

        if (!flag)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('删除失败， 请选择要删除的项！');</script>");
        }
        else
        {
            if (deleteResult)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('删除成功！');</script>");
                ChannelBind();
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('删除失败！');</script>");
            }
        }
    }


    protected void btnAdd_Click(object sender, EventArgs e)
    {
        string shopName = Convert.ToString(Request.QueryString["shopName"]);
        string channelName = Common.ToString(Request.QueryString["channelName"]);
        Response.Redirect("ShopChannelDishDetail.aspx?channelID=" + Common.ToInt32(Request.QueryString["id"]) + "&shopID=" + Convert.ToInt32(Request.QueryString["shopID"]) + "&channelIndex=" + TextBox_Index.Text+"&shopName=" + shopName + "&channelName=" + channelName);
    }

    protected void GridView_ChannelConfig_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

    }

    protected void GridView_ChannelConfig_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

    protected void GridView_ChannelConfig_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

}