using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using VAGastronomistMobileApp.WebPageDll;
using VAGastronomistMobileApp.Model;
using System.Data;

public partial class AppPages_RecommendShopInfo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //显示某餐厅信息 
        try
        {
            string cityID = Request.QueryString["cityID"];
            string shopID = Request.QueryString["shopID"];
            //lblMsg.Text = cityID + "/" + shopID;

            WechatRecommandShopOperator wso = new WechatRecommandShopOperator();
            //b.cityName,a.cityID,a.shopID,c.shopName,c.shopAddress,c.shopLogo,c.shopImagePath,c.shopTelephone,a.recommandType,recommandTypeName
            DataTable dt = wso.GetRecommandShopInfo(Common.ToInt32(cityID), Common.ToInt32(shopID));
            DataTable dtImg = wso.GetRecommandShopInfo(Common.ToInt32(shopID));
            if (dt != null && dt.Rows.Count > 0)
            {
                //取得图片路径
                string imagePath = WebConfig.CdnDomain + WebConfig.ImagePath;

                if (dtImg != null && dtImg.Rows.Count > 0)
                    img1.ImageUrl = imagePath + dtImg.Rows[0][0].ToString() + WebConfig.ShopImg + dtImg.Rows[0][1].ToString();
                else
                {
                    if (!string.IsNullOrEmpty(dt.Rows[0][5].ToString()))
                    {
                        img1.ImageUrl = imagePath + dt.Rows[0][6].ToString() + dt.Rows[0][5].ToString();
                    }
                    else
                    {
                        img1.ImageUrl = imagePath + "imageNotFoundSmall.jpg";
                    }
                    img2.Visible = false;
                }

                if (dtImg != null && dtImg.Rows.Count > 1)
                {
                    img2.Visible = true;
                    img2.ImageUrl = imagePath + dtImg.Rows[1][0].ToString() + WebConfig.ShopImg + dtImg.Rows[1][1].ToString();
                }
                else
                    img2.Visible = false;
                
                rType.Text = dt.Rows[0][0].ToString() + "-" + dt.Rows[0][9].ToString();
                lblName.Text = "餐厅名称：" + dt.Rows[0][3].ToString();
                lblAddr.Text = "餐厅地址：" + dt.Rows[0][4].ToString();
                lblTel.Text = "电话:" + dt.Rows[0][7].ToString();
            }
        }
        catch (Exception ex)
        { Response.Write(ex.Message); }
    }
}