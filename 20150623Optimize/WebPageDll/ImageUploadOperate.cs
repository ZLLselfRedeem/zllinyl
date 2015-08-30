using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Web;
using System.IO;
using System.Drawing;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.Model;
using System.Data;
using VAGastronomistMobileApp.SQLServerDAL;
using CloudStorage;


namespace VAGastronomistMobileApp.WebPageDll
{
    public class ImageUploadOperate
    {
        public static string validExtension = ConfigurationManager.AppSettings["extension"].ToString();
        /// <summary>
        /// 这个方法用来查出最大公约数
        /// </summary>
        /// <param name="n1"></param>
        /// <param name="n2"></param>
        /// <returns></returns>
        public float maxGongYueShu(int n1, int n2)
        {
            int temp = Math.Max(n1, n2);
            n2 = Math.Min(n1, n2);//n2中存放两个数中最小的
            n1 = temp;//n1中存放两个数中最大的
            while (n2 != 0)
            {
                n1 = n1 > n2 ? n1 : n2;//使n1中的数大于n2中的数
                int m = n1 % n2;
                n1 = n2;
                n2 = m;
            }
            return n1;
        }
        /// <summary>
        /// 检测一个文件后缀名，是不是有效的图片后缀名
        /// </summary>
        /// <param name="extension"></param>
        /// <returns></returns>
        public bool CheckFileIsImage(string extension)
        {
            //在配置文件里面配置的，允许的图片后缀名
            string[] validExtensions = validExtension.Split('|');
            bool isImage = false;
            for (int j = 0; j < validExtensions.Length; j++)
            {
                if (extension == validExtensions[j])
                {
                    isImage = true;
                }
            }
            return isImage;
        }
        /// <summary>
        /// 检测一个文件夹中的图片是不是合格（规格是不是4:3，图片width是不是大于600，height是不是大于450）
        /// </summary>
        /// <param name="imageFullName"></param>
        /// <returns></returns>
        public int DetectImages(string imageFullName, int imageWidth, int imageHeight)
        {
            // System.Drawing.Image image = BytToImg(imageMemoryStream);
            int imageResult = -3;
            string extension = "." + imageFullName.Substring(imageFullName.LastIndexOf(".") + 1, (imageFullName.Length - imageFullName.LastIndexOf(".") - 1));   //扩展名
            if (CheckFileIsImage(extension))
            {
                try
                {
                    string newFullImagePath = System.Web.HttpContext.Current.Server.MapPath(imageFullName);//要补全路径，否则报错
                    System.Drawing.Image image = System.Drawing.Image.FromFile(newFullImagePath);//获得Image对象
                    int width = image.Width;
                    int height = image.Height;
                    float gongyueshu = maxGongYueShu(width, height);
                    if (width / gongyueshu != 4 || height / gongyueshu != 3)//图片比例不合格，直接不允许上传//必须满足4：3
                    {
                        image.Dispose();
                        imageResult = 0;
                    }
                    else//图片比例合格
                    {
                        if (width == imageWidth && height == imageHeight) //图片分辨率合格//尺寸大小合格
                        {
                            image.Dispose();//这里必须先释放
                            imageResult = 1;//表示完全符合上传的标准
                        }
                        else
                        {
                            image.Dispose();
                            imageResult = -1;//尺寸不对，需要处理一下
                        }
                    }
                }
                catch (Exception)
                {
                    imageResult = -2;//表示上传出现问题，上传失败
                }
            }
            return imageResult;
        }
        /// <summary>
        /// 修改上传的图片尺寸生成缩略图
        /// </summary>
        /// <param name="originalImagePath">源图路径（物理路径）</param>
        /// <param name="thumbnailPath">缩略图路径（物理路径）</param>
        /// <param name="width">缩略图宽度</param>
        /// <param name="height">缩略图高度</param>
        /// <param name="mode">生成缩略图的方式</param> 
        public bool MakeThumbnail(string originalImagePath, string thumbnailPath, int width, int height, string mode, string type)
        {
            bool result = true;
            string newFullImagePath = System.Web.HttpContext.Current.Server.MapPath(originalImagePath);//要补全路径，否则报错
            System.Drawing.Image originalImage = System.Drawing.Image.FromFile(newFullImagePath);
            int towidth = width;
            int toheight = height;
            int x = 0;
            int y = 0;
            int ow = originalImage.Width;
            int oh = originalImage.Height;
            switch (mode)
            {
                case "HW"://指定高宽缩放（可能变形） 
                    break;
                case "W"://指定宽，高按比例 
                    toheight = originalImage.Height * width / originalImage.Width;
                    break;
                case "H"://指定高，宽按比例
                    towidth = originalImage.Width * height / originalImage.Height;
                    break;
                case "Cut"://指定高宽裁减（不变形） 
                    if ((double)originalImage.Width / (double)originalImage.Height > (double)towidth / (double)toheight)
                    {
                        oh = originalImage.Height;
                        ow = originalImage.Height * towidth / toheight;
                        y = 0;
                        x = (originalImage.Width - ow) / 2;
                    }
                    else
                    {
                        ow = originalImage.Width;
                        oh = originalImage.Width * height / towidth;
                        x = 0;
                        y = (originalImage.Height - oh) / 2;
                    }
                    break;
                case "DB"://等比缩放（不变形，如果高大按高，宽大按宽缩放） 
                    if ((double)originalImage.Width / (double)towidth < (double)originalImage.Height / (double)toheight)
                    {
                        toheight = height;
                        towidth = originalImage.Width * height / originalImage.Height;
                    }
                    else
                    {
                        towidth = width;
                        toheight = originalImage.Height * width / originalImage.Width;
                    }
                    break;
                default:
                    break;
            }
            //新建一个bmp图片
            System.Drawing.Image bitmap = new System.Drawing.Bitmap(towidth, toheight);
            //新建一个画板
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bitmap);
            //设置高质量插值法
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
            //设置高质量,低速度呈现平滑程度
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            //清空画布并以透明背景色填充
            g.Clear(System.Drawing.Color.Transparent);
            //在指定位置并且按指定大小绘制原图片的指定部分
            g.DrawImage(originalImage, new System.Drawing.Rectangle(0, 0, towidth, toheight),
            new System.Drawing.Rectangle(x, y, ow, oh),
            System.Drawing.GraphicsUnit.Pixel);
            try
            {
                string newImagePath = System.Web.HttpContext.Current.Server.MapPath(thumbnailPath);//要补全路径，否则报错

                if (type == "jpg")
                {
                    bitmap.Save(newImagePath, System.Drawing.Imaging.ImageFormat.Jpeg);//保存新图片
                }
                //else if (type == "bmp")
                //{
                //    bitmap.Save(newImagePath, System.Drawing.Imaging.ImageFormat.Bmp);
                //}
                //else if (type == "gif")
                //{
                //    bitmap.Save(newImagePath, System.Drawing.Imaging.ImageFormat.Gif);
                //}
                //else if (type == "png")
                //{
                //    bitmap.Save(newImagePath, System.Drawing.Imaging.ImageFormat.Png);
                //}
                else
                {
                    result = false;
                }
            }
            catch (System.Exception e)
            {
                result = false;
                throw e;
            }
            finally
            {
                originalImage.Dispose();
                bitmap.Dispose();
                g.Dispose();
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="original_image"></param>
        /// <returns></returns>
        public string GetthumbnailId(System.Drawing.Image original_image)
        {
            System.Drawing.Image thumbnail_image = null;
            System.Drawing.Bitmap final_image = null;
            System.Drawing.Graphics graphic = null;
            MemoryStream ms = null;
            try
            {
                int width = original_image.Width;
                int height = original_image.Height;
                int target_width = original_image.Width;
                int target_height = original_image.Height;
                int new_width, new_height;
                new_width = width;
                new_height = height;
                float ratio = 0.75f;
                float rationow = (float)height / (float)width;

                if (ratio > rationow)
                {
                    target_width = width;
                    target_height = (int)Math.Floor(ratio * (float)width);

                }
                else
                {
                    target_width = (int)Math.Floor((float)height / ratio);
                    target_height = height;

                }

                if (width >= Common.ToInt32(WebConfig.DishImageWidth) && height >= Common.ToInt32(WebConfig.DishImageHeight))
                {
                    final_image = new System.Drawing.Bitmap(target_width, target_height);
                    graphic = System.Drawing.Graphics.FromImage(final_image);
                    graphic.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.Black), new System.Drawing.Rectangle(0, 0, target_width, target_height));
                    int paste_x = (target_width - new_width) / 2;
                    int paste_y = (target_height - new_height) / 2;
                    graphic.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic; /* new way */
                    //graphic.DrawImage(thumbnail_image, paste_x, paste_y, new_width, new_height);
                    graphic.DrawImage(original_image, paste_x, paste_y, new_width, new_height);

                    // Store the thumbnail in the session (Note: this is bad, it will take a lot of memory, but this is just a demo)
                    ms = new MemoryStream();
                    final_image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);

                    //final_image.Save("E:\\44445.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);//保存新图片
                    // Store the data in my custom Thumbnail object
                    //string thumbnail_id = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                    string thumbnail_id = System.Guid.NewGuid().ToString();
                    Thumbnail thumb = new Thumbnail(thumbnail_id, ms.GetBuffer());

                    // Put it all in the Session (initialize the session if necessary)			
                    List<Thumbnail> thumbnails = HttpContext.Current.Session["file_info"] as List<Thumbnail>;

                    if (thumbnails == null)
                    {
                        thumbnails = new List<Thumbnail>();
                        HttpContext.Current.Session["file_info"] = thumbnails;
                    }
                    thumbnails.Add(thumb);
                    return thumbnail_id;
                }
                else
                {
                    //return "imageRatioError";
                    return "imageRatioError,最小尺寸应为" + WebConfig.DishImageWidth + "*" + WebConfig.DishImageHeight;
                }
            }
            catch
            {
                return "";
            }
            finally
            {
                // Clean up
                if (final_image != null) final_image.Dispose();
                if (graphic != null) graphic.Dispose();
                if (original_image != null) original_image.Dispose();
                if (thumbnail_image != null) thumbnail_image.Dispose();
                if (ms != null) ms.Close();
            }
        }

        /// <summary>
        /// 图片保存
        /// </summary>
        /// <param name="menuId">菜谱ID</param>
        /// <param name="id">sessionID</param>
        /// <param name="x">客户端截图坐标X</param>
        /// <param name="y">客户端截图坐标Y</param>
        /// <param name="w">客户端截图坐标宽度</param>
        /// <param name="h">客户端截图坐标长度</param>
        /// <param name="kehuduanwidth">客户端实际宽度</param>
        /// <param name="bigimageName">大图名称</param>
        /// <param name="smallimageName">小图名称</param>
        public static void ImageMake(string menuId, string id, int x, int y, int w, int h, int kehuduanwidth, out string bigimageName)//, out string smallimageName 2014-6-9 菜图取消小图
        {
            string path = "";
            List<Thumbnail> thumbnails = HttpContext.Current.Session["file_info"] as List<Thumbnail>;
            byte[] imgData = null;
            foreach (Thumbnail thumb in thumbnails)
            {
                if (thumb.ID == id)
                {
                    imgData = thumb.Data;
                }
            }
            System.Drawing.Image originalImage = ReturnPhoto(imgData);

            int ow = originalImage.Width;//session中取出来的原始图片
            int oh = originalImage.Height;
            int jiemianw = kehuduanwidth;//客户端界面实际宽度
            int px = (x * ow) / jiemianw;//原始图片实际坐标
            int py = (y * ow) / jiemianw;

            int pw = 0;
            int ph = 0;
            //前端截图后上传的图片宽高忽略了小数点，可能会导致截图小于最小尺寸，此处要处理一下
            if (Math.Ceiling(Common.ToDouble(jiemianw) * Common.ToDouble(WebConfig.DishImageWidth) / Common.ToDouble(ow)) == w + 1)
            {
                pw = Common.ToInt32(WebConfig.DishImageWidth);
                ph = Common.ToInt32(WebConfig.DishImageHeight);
            }
            else
            {
                pw = (w * ow) / jiemianw;
                ph = (h * ow) / jiemianw;
            }
            if (x != 0)//若截图，检查图片比例 2014-6-11
            {
                float gongYueShu = Common.maxGongYueShu(pw, ph);
                if (pw / gongYueShu != 4 || ph / gongYueShu != 3)
                {
                    //若裁剪，由于前端传过来的数据四舍五入，导致计算出的图片尺寸不是精确的4:3，以width为准重新计算height
                    ph = pw * 3 / 4;
                }
            }
            #region : wyh同志默认不截图传过来的是600*450,如果取到该值则不用缩放
            //if (w == 600)
            //{
            //    pw = 600;
            //}
            //if (h == 450)
            //{
            //    ph = 450;
            //}
            #endregion

            string imagePath = WebConfig.ImagePath;
            MenuManager man = new MenuManager();
            DataTable dt = man.QueryMenu(Common.ToInt32(menuId));
            if (dt.Rows.Count > 0)
            {
                path = imagePath + dt.Rows[0]["menuImagePath"].ToString();
            }
            string orgimageName = "2_" + menuId + "_" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".jpg";
            bigimageName = "0_" + menuId + "_" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".jpg";
            //smallimageName = "1_" + menuId + "_" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".jpg";
            string orgimageUrl = path + orgimageName;
            string bigimageUrl = path + bigimageName;
            //string smallimageUrl = path + smallimageName;
            System.Drawing.Image orgimage = MakeImage(pw, ph, px, py, originalImage, "caijian", orgimageUrl, orgimageName);

            System.Drawing.Image bigimage = MakeImage(pw, ph, 0, 0, orgimage, "suofang", bigimageUrl, bigimageName);
            //System.Drawing.Image bigimage = MakeImage(600, 450, 0, 0, orgimage, "suofang", bigimageUrl, bigimageName);
            //System.Drawing.Image smallimage = MakeImage(272, 204, 0, 0, bigimage, "suofang", smallimageUrl, smallimageName);
            orgimage.Dispose();
            bigimage.Dispose();
            //smallimage.Dispose();
            #region 删除session信息
            for (int i = thumbnails.Count - 1; i >= 0; i--)
            {
                if (thumbnails[i].ID == id)
                {
                    thumbnails.RemoveAt(i);
                }
            }
            #endregion
        }


        /// <summary>
        /// byte  转image
        /// </summary>
        /// <param name="streamByte"></param>
        /// <returns></returns>
        public static System.Drawing.Image ReturnPhoto(byte[] streamByte)
        {
            System.IO.MemoryStream ms = new System.IO.MemoryStream(streamByte);
            System.Drawing.Image img = System.Drawing.Image.FromStream(ms);
            return img;
        }
        /// <summary>
        /// 服务器保存图片
        /// </summary>
        /// <param name="w"></param>
        /// <param name="h"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="originalImage"></param>
        /// <param name="type"></param>
        /// <param name="Imageurl"></param>
        /// <returns></returns>
        public static System.Drawing.Image MakeImage(int w, int h, int x, int y, System.Drawing.Image originalImage, string type, string Imageurl, string imageName)
        {
            int towidth = w;
            int toheight = h;
            int ow = originalImage.Width;
            int oh = originalImage.Height;
            switch (type)
            {
                case "caijian":
                    towidth = originalImage.Width;
                    toheight = originalImage.Height;


                    break;
                case "suofang":
                    if ((double)originalImage.Width / (double)towidth < (double)originalImage.Height / (double)toheight)
                    {
                        toheight = h;
                        towidth = originalImage.Width * h / originalImage.Height;
                    }
                    else
                    {
                        towidth = w;
                        toheight = originalImage.Height * w / originalImage.Width;
                    }
                    break;
            }
            //新建一个bmp图片
            System.Drawing.Image bitmap = new System.Drawing.Bitmap(w, h);
            //新建一个画板
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bitmap);
            //设置高质量插值法
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
            //设置高质量,低速度呈现平滑程度
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            //清空画布并以透明背景色填充
            g.Clear(System.Drawing.Color.Transparent);
            //在指定位置并且按指定大小绘制原图片的指定部分
            g.DrawImage(originalImage, new System.Drawing.Rectangle(0, 0, towidth, toheight),
            new System.Drawing.Rectangle(x, y, ow, oh),
            System.Drawing.GraphicsUnit.Pixel);
            try
            {
                //string newImagePath = System.Web.HttpContext.Current.Server.MapPath(thumbnailPath);//要补全路径，否则报错
                if (type != "caijian")
                {
                    //bitmap.Save(Imageurl, System.Drawing.Imaging.ImageFormat.Jpeg);//保存新图片
                    HttpServerUtility server = System.Web.HttpContext.Current.Server;
                    bitmap.Save(server.MapPath(WebConfig.Temp + imageName), System.Drawing.Imaging.ImageFormat.Jpeg);//保存新图片

                    CloudStorageResult uploadResult = CloudStorageOperate.PutObject(Imageurl, server.MapPath(WebConfig.Temp + imageName));
                    if (uploadResult.code)
                    {
                        File.Delete(server.MapPath(WebConfig.Temp + imageName));
                    }
                }
                return bitmap;

            }
            catch (System.Exception)
            {
                return null;
            }
            finally
            {
                originalImage.Dispose();
                // bitmap.Dispose();
                g.Dispose();
            }

        }

        //批量传图
        public static DataTable GetBatchImageInfo(uint pageIndex)
        {
            ImageInfoManager imageMan = new ImageInfoManager();
            return imageMan.GetBatchImageInfo(pageIndex);
        }
        //获取图片数量
        public static int GetBatchImageCount()
        {
            ImageInfoManager imageMan = new ImageInfoManager();
            return imageMan.GetBatchImageCount();
        }


        ///// <summary>
        ///// 把文件转换成内存流
        ///// </summary>
        ///// <param name="fileName">完整的文件名</param>
        ///// <returns>返回已转好的内存流</returns>
        //public byte[] FileToStream(string fileName)
        //{
        //    // 打开文件
        //    FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
        //    // 读取文件的 byte[]
        //    byte[] bytes = new byte[fileStream.Length];
        //    fileStream.Read(bytes, 0, bytes.Length);
        //    fileStream.Close();
        //    // 把 byte[] 转换成 Stream
        //   // MemoryStream stream = new MemoryStream(bytes);
        //    return bytes;
        //}

        ///// <summary> 
        ///// 字节流转换成图片 
        ///// </summary> 
        ///// <param name="byt">要转换的字节流</param> 
        ///// <returns>转换得到的Image对象</returns> 
        //public  System.Drawing.Image BytToImg(byte[] byt)
        //{
        //    MemoryStream ms = new MemoryStream(byt);
        //    System.Drawing.Image img = System.Drawing.Image.FromStream(ms);
        //    return img;
        //}


    }
}
