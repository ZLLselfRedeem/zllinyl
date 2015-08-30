using System;
using System.IO;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.WebPageDll;

public partial class image : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int type = Common.ToInt32(Request.QueryString["type"]);
        int value = Common.ToInt32(Request.QueryString["value"]);
        int scale = Common.ToInt32(Request.QueryString["scale"]);
        if (type > 0 && value > 0)
        {
            switch(type)
            {
                case (int)VAImageType.PREORDER_SHARE_IMAGE:
                    {
                        string dishImagePath = GetDishImagePath(value, scale);
                        
                        if (string.IsNullOrEmpty(dishImagePath))
                        {
                            if (scale == 1)
                            {
                                //dishImagePath = Server.MapPath("~/UploadFiles/Images/imageNotFoundSmall.jpg");
                                dishImagePath = WebConfig.CdnDomain + WebConfig.ImagePath + "imageNotFoundSmall.jpg";
                            } 
                            else
                            {
                                //dishImagePath = Server.MapPath("~/UploadFiles/Images/imageNotFound.jpg");
                                dishImagePath = WebConfig.CdnDomain + WebConfig.ImagePath + "imageNotFound.jpg";
                            }
                        }
                        Bitmap image = new Bitmap(dishImagePath);

                        CreateImageOnPage(image, this.Context);
                    }
                    break;
                    default:
                    break;
            }
        }
    }
    /// <summary>
    /// 查询菜的图片路径
    /// </summary>
    /// <param name="dishPriceI18nId"></param>
    /// <returns></returns>
    private string GetDishImagePath(int dishPriceI18nId, int imageScale)
    {
        DishOperate dishOpe = new DishOperate();
        return dishOpe.QueryDishImagePath(dishPriceI18nId, imageScale);
    }
    #region 将创建好的图片输出到页面
    public void CreateImageOnPage(Bitmap image, HttpContext context)
    {
        MemoryStream ms = new System.IO.MemoryStream();
        image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
        context.Response.ClearContent();
        context.Response.ContentType = "image/Jpeg";
        context.Response.BinaryWrite(ms.GetBuffer());

        ms.Close();
        ms = null;
        image.Dispose();
        image = null;
    }
    #endregion
}