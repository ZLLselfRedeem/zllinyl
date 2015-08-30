<%@ WebHandler Language="C#" Class="UploadFile" %>

using System;
using System.Web;
using System.IO;
using System.Text;
using System.Net;
using VAGastronomistMobileApp.WebPageDll;
using VAGastronomistMobileApp.Model;
using System.Configuration;
public class UploadFile : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        context.Response.Charset = "utf-8";
        HttpPostedFile file = context.Request.Files["Filedata"];
        string uploadPath = context.Request["folder"];
        string[] getstring = { };
        if (context.Request["type"] != null)
        {
            getstring = context.Request["type"].Split('$');
        }
        string type = "";
        string menuid = "";
        string imgtype = "";
        if (getstring != null && getstring.Length != 0)
        {
            type = getstring[0];
            menuid = getstring[1];
            imgtype = getstring[2];
        }


        int FileLength = file.ContentLength;//取得数据的长度
        Byte[] FileByteArray = new byte[FileLength];//图象文件临时存储到Byte数组里
        Stream StreamObject = file.InputStream;//建立数据流对象
        StreamObject.Read(FileByteArray, 0, FileLength);

        //建立图片对象
        System.Drawing.Image MyImage = System.Drawing.Image.FromStream(StreamObject);
        //开始判断图片的大小
        bool isok = CheckImage(MyImage.Width, MyImage.Height);

        if (isok)
        {

            string imagePath = HttpContext.Current.Server.MapPath("~/" + ConfigurationManager.AppSettings["ImagePath"].ToString() + uploadPath);//如果上传到其他服务器，则需要在iis建立虚拟目录，指向图片服务器
            if (file != null)
            {
                if (!Directory.Exists(imagePath))
                {
                    Directory.CreateDirectory(imagePath);
                }
                string extension = System.IO.Path.GetExtension(file.FileName);
                string newImageName = "";
                if (type == null)
                {
                    newImageName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + extension;//默认此种方式生成
                }
                else
                {
                    newImageName = imgtype + "_" + menuid + "_" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + extension;//图片名称形如：0_17_201209200333123.jpg 1为小图。2为大图
                }

                #region 缩放
                int newweight = 600;//大图缩放比例
                int newheight = 450;
                if (imgtype == "1")
                {
                    newweight = 272;//小图缩放比例
                    newheight = 204;
                }
                //原始图片（获取原始图片创建对象，并使用流中嵌入的颜色管理信息）
                System.Drawing.Image initImage = System.Drawing.Image.FromStream(StreamObject, true);

                System.Drawing.Image newImage = new System.Drawing.Bitmap(newweight, newheight);
                System.Drawing.Graphics newG = System.Drawing.Graphics.FromImage(newImage);

                //设置质量
                newG.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                newG.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                //置背景色
                newG.Clear(System.Drawing.Color.White);
                //画图
                newG.DrawImage(initImage, new System.Drawing.Rectangle(0, 0, newImage.Width, newImage.Height), new System.Drawing.Rectangle(0, 0, initImage.Width, initImage.Height), System.Drawing.GraphicsUnit.Pixel);

                //Byte[] bytes = imageToByteArray(newImage);
                //Stream stream = new MemoryStream(bytes);
                #endregion

                newImage.Save(imagePath + newImageName, System.Drawing.Imaging.ImageFormat.Jpeg);
                //释放资源
                newG.Dispose();
                newImage.Dispose();
                initImage.Dispose();
                //file.SaveAs(imagePath + newImageName);
                //下面这句代码缺少的话，上传成功后上传队列的显示不会自动消失
                //context.Response.Write(newBigImageName);
                context.Response.Write(newImageName);
            }
            else
            {
                context.Response.Write("0");
            }
        }
        else
        {
            context.Response.Write("1");
        }

    }
    public byte[] imageToByteArray(System.Drawing.Image imageIn)
    {
        MemoryStream ms = new MemoryStream();
        imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
        return ms.ToArray();
    }
    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
    public bool CheckImage(int Width, int Height)
    {
        bool Result = false;
        float gongyueshu =Common.maxGongYueShu(Width, Height);
        if (Width / gongyueshu != 4 || Height / gongyueshu != 3)//图片比例不合格，直接不允许上传//必须满足4：3
        {
            Result = false;
        }
        else//图片比例合格
        {
            Result = true;
        }
        return Result;
    }
    /*/// <summary>
    /// ajax上传图片
    /// </summary>
    /// <param name="shopImagePath"></param>
    /// <param name="fileName"></param>
    /// <returns></returns>
    protected string UploadCouponThumnail(string imageUploadPath, string fileName)
    {
        //string imagePath = ConfigurationManager.AppSettings["Server"].ToString() + "/" + ConfigurationManager.AppSettings["ImagePath"].ToString() + imageUploadPath;
        string imagePath = "~/" + ConfigurationManager.AppSettings["ImagePath"].ToString() + imageUploadPath;//这里需要在远程图片服务器
        string imageName = string.Empty;
        
        
        //上传图片
        //string fileName = Server.HtmlEncode(FileUpload_CouponThumbnail.FileName);
        string extension = System.IO.Path.GetExtension(fileName);
        if (fileName != "")
        {
            if (Common.ValidateExtension(extension))
            {
                imageName = fileName.Replace(extension, "");
                string newImageName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + extension;//
                imageName = newImageName;
                Common.FolderCreate(HttpContext.Current.Server.MapPath(imagePath));//这里需要在远程图片服务器上生成一个文件夹
                string couponImagePath = imagePath + newImageName;
                //创建WebClient实例
                WebClient myWebClient = new WebClient();
                //设定window网络安全认证
                myWebClient.Credentials = CredentialCache.DefaultCredentials;
                //要上传的文件
                FileStream fs = new FileStream("C:\\Users\\t\\Pictures\\095428u879u88a78c20zad.jpg", FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fs);
                //使用UploadFile方法可以用下面的格式
                string aaaaaa = "http://localhost:54020/" + ConfigurationManager.AppSettings["ImagePath"].ToString() + imageUploadPath + newImageName;
                myWebClient.UploadFile(aaaaaa, "C:\\Users\\t\\Pictures\\095428u879u88a78c20zad.jpg");

                CouponImageAndDish couponImageAndDish = new CouponImageAndDish();
                couponImageAndDish.couponId = 0;
                couponImageAndDish.couponImageName = newImageName;
                couponImageAndDish.couponImagePath = imageUploadPath + newImageName;
                couponImageAndDish.couponImageScale = 1;
                couponImageAndDish.couponImageSequence = 0;
                couponImageAndDish.couponImageStatus = 1;
                couponImageAndDish.dishId = 0;
                couponImageAndDish.dishName = "";
                couponImageAndDish.dishPrice = 0;
                couponImageAndDish.dishPriceI18nID = 0;
                couponImageAndDish.scaleName = "";
                couponImageAndDish.dishDescDetail = "";
                couponImageAndDish.dishDescShort = "";

                return JsonOperate.JsonSerializer<CouponImageAndDish>(couponImageAndDish);
            }
            else
            {
                return "";
            }
        }
        else
        {
            return "";
        }
    }*/
}