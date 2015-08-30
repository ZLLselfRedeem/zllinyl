using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using VAGastronomistMobileApp.WebPageDll;
using System.Data;

public partial class AppPages_HotMenuInfo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string DishID = Request.QueryString["DishID"];
            //Response.Write(DishID);
            //return;
            WechatHotMenuOperate wmo = new WechatHotMenuOperate();
            DataTable dt = wmo.GetHopMenuInfo(Common.ToInt32(DishID));//参数 cityName,shopName,shopAddress,DishName,DishPrice,ImageFolder,ImageName,DishID,saleAmount

            //取得图片路径
            string imgServer = ConfigurationManager.AppSettings["Server"].ToString();
            string imgFolder = ConfigurationManager.AppSettings["ImagePath"].ToString();
            if (dt != null && dt.Rows.Count > 0)
            {
                if (dt.Rows[0][5].ToString() != "")
                    dishImg.ImageUrl = imgServer + "/" + imgFolder + dt.Rows[0][5].ToString() + dt.Rows[0][6].ToString();
                else
                    dishImg.ImageUrl = imgServer + "/" + imgFolder + "imageNotFoundSmall.jpg";

                lblHeader.Text = dt.Rows[0][0].ToString().Substring(0, 2) + "地区热菜";
                lbldishName.Text = "菜品名称:" + dt.Rows[0][3].ToString();
                lblDishPrice.Text = "菜品价格:" + dt.Rows[0][4].ToString() + "元";
                lblSaleAmount.Text = "上周销量:" + dt.Rows[0][8].ToString();
                lblShopName.Text = "可售门店:" + dt.Rows[0][1].ToString();
                lblShopAddr.Text = "地址:" + dt.Rows[0][2].ToString();
            }
        }
        catch (Exception ex)
        { Response.Write(ex.Message); }
    }
}