using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Autofac;
using Autofac.Integration.Web;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.WebPageDll;
using VAGastronomistMobileApp.WebPageDll.Services;
using CloudStorage;
using VAGastronomistMobileApp.WebPageDll.Services.Infrastructure;

public partial class Shared_FoodDiaryDefaultConfigDishAdd : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }


    protected void Button1_Click(object sender, EventArgs e)
    {
        //if (string.IsNullOrWhiteSpace(TextBox_DishName.Text))
        //{
        //    Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('菜品名称不能为空');</script>");
        //    return;;
        //}
        string filename;
        if (UploadImage(ImageName_File, ImageName_Img, out filename))
        {
            FoodDiaryDefaultConfigDish foodDiaryDefaultConfigDish = new FoodDiaryDefaultConfigDish()
            {
                DishName = TextBox_DishName.Text,
                ImageName = filename,
                Status = true
            };
            //var preOrder19DianService = cp.RequestLifetime.Resolve<IPreOrder19DianService>();
            var foodDiaryDefaultConfigDishService = ServiceFactory.Resolve<IFoodDiaryDefaultConfigDishService>();
            foodDiaryDefaultConfigDishService.Add(foodDiaryDefaultConfigDish);
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('上传图片失败');</script>");
        }
    }

    private bool UploadImage(FileUpload ImageName_File, Image ImageName_Img, out string filename)
    {
        string savaLogoPath = WebConfig.ImagePath + WebConfig.FoodDiaryImagePath;
        string queryLogoPath = WebConfig.CdnDomain + savaLogoPath;
        string fileName = Server.HtmlEncode(ImageName_File.FileName);
        string extension = System.IO.Path.GetExtension(fileName);
        string[] supportExtensions = new string[] { ".jpg", ".jpeg", ".png" };
        if (supportExtensions.Contains(extension))
        {
            System.Drawing.Bitmap originalBMP = new System.Drawing.Bitmap(ImageName_File.FileContent);//该上传图片文件
            if (originalBMP.Size.Width / originalBMP.Size.Height == 4 / 3)
            {
                string destfileName =
                    Convert.ToString((long)(new DateTime(1970, 1, 1) - DateTime.Now).TotalSeconds, 16) + extension;

                CloudStorageResult result = CloudStorageOperate.PutObject(savaLogoPath + destfileName, ImageName_File, destfileName);
                ImageName_Img.ImageUrl = queryLogoPath + destfileName;
                filename = destfileName;
                return result.code;
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('图片尺寸不正确');</script>");
            }

        }
        else
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('图片格式不正确');</script>");
        }
        filename = string.Empty;
        return false;
    }
}