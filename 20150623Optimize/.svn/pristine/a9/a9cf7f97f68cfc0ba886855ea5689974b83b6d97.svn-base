using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using VAGastronomistMobileApp.DBUtility;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.WebPageDll;
public partial class WebUserControl_DishManageControl : System.Web.UI.UserControl
{
    //通过属性，改变样式
    public string Linkcss { get; set; }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Linkcss == "base")
        {
            LinkButton_base.CssClass = "DishInfoButtonOn";
            LinkButton_video.CssClass = "DishInfoButtonOff";
            LinkButton_price.CssClass = "DishInfoButtonOff";
        }
        else if (Linkcss == "video")
        {
            LinkButton_base.CssClass = "DishInfoButtonOff";
            LinkButton_video.CssClass = "DishInfoButtonOn";
            LinkButton_price.CssClass = "DishInfoButtonOff";
        }
        else if (Linkcss == "price")
        {
            LinkButton_base.CssClass = "DishInfoButtonOff";
            LinkButton_video.CssClass = "DishInfoButtonOff";
            LinkButton_price.CssClass = "DishInfoButtonOn";
        }
        //获取菜名
        if (Request.QueryString["DishID"] != null)
        {
            int DishID =Common.ToInt32(Request.QueryString["DishID"].ToString());
            DishOperate dishOperate = new VAGastronomistMobileApp.WebPageDll.DishOperate();
            VADish VADish = dishOperate.QueryDishInfo(DishID);
            Label1.Text = VADish.dishName;
        }
    }
    protected void LinkButton_base_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["DishID"] != null && Request.QueryString["langID"] != null && Request.QueryString["menuID"] != null && Request.QueryString["DishTypeID"] != null)
        {
            string DishID = Request.QueryString["DishID"].ToString();
            string langID = Request.QueryString["langID"].ToString();
            string menuID = Request.QueryString["menuID"].ToString();
            string DishTypeID = Request.QueryString["DishTypeID"].ToString();
            Response.Redirect("~/DishManage/DishUpdate.aspx?DishID=" + DishID + "&langID=" + langID + "&menuID=" + menuID + "&DishTypeID=" + DishTypeID);
        }
    }
    protected void LinkButton_video_Click(object sender, EventArgs e)
    {

        if (Request.QueryString["DishID"] != null && Request.QueryString["langID"] != null && Request.QueryString["menuID"] != null && Request.QueryString["DishTypeID"] != null)
        {
            string DishID = Request.QueryString["DishID"].ToString();
            string langID = Request.QueryString["langID"].ToString();
            string menuID = Request.QueryString["menuID"].ToString();
            string DishTypeID = Request.QueryString["DishTypeID"].ToString();
            Response.Redirect("~/DishManage/DishAdd2.aspx?DishID=" + DishID + "&langID=" + langID + "&menuID=" + menuID + "&DishTypeID=" + DishTypeID);
        }
    }
    protected void LinkButton_price_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["DishID"] != null && Request.QueryString["langID"] != null && Request.QueryString["menuID"] != null && Request.QueryString["DishTypeID"] != null)
        {
            string DishID = Request.QueryString["DishID"].ToString();
            string langID = Request.QueryString["langID"].ToString();
            string menuID = Request.QueryString["menuID"].ToString();
            string DishTypeID = Request.QueryString["DishTypeID"].ToString();
            Response.Redirect("~/DishManage/DishAddPrice.aspx?DishID=" + DishID + "&langID=" + langID + "&menuID=" + menuID + "&DishTypeID=" + DishTypeID);
        }

    }

    /// <summary>
    /// 返回列表
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void LinkButton_back_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["DishID"] != null && Request.QueryString["langID"] != null && Request.QueryString["menuID"] != null && Request.QueryString["DishTypeID"] != null)
        {
            string DishID = Request.QueryString["DishID"].ToString();
            string langID = Request.QueryString["langID"].ToString();
            string menuID = Request.QueryString["menuID"].ToString();
            string DishTypeID = Request.QueryString["DishTypeID"].ToString();
            Response.Redirect("~/DishManage/DishList.aspx?DishID=" + DishID + "&langID=" + langID + "&menuID=" + menuID + "&DishTypeID=" + DishTypeID);
        }
    }
}