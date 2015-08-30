using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.WebPageDll;

public partial class ViewAllocVip_viewallocVip : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ShopVipArticleOperate shopVipArticleOperate = new ShopVipArticleOperate();

        if (!string.IsNullOrEmpty(Request.QueryString["shopId"]))
        {
            int shopId = Common.ToInt32(Request.QueryString["shopId"]);

            div1.InnerHtml = shopVipArticleOperate.ClientGetArticle(shopId);
        }
    }
}