using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Web;
using VAGastronomistMobileApp.DBUtility;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL;
using System.Collections.Generic;
using CloudStorage;

namespace VAGastronomistMobileApp.WebPageDll.Syb
{
    public class SybMultiImageUploadOperate
    {
        private static object locker = new object();

        public static string Upload(int userId, int shopId, int memuId, string fileName, Stream stream)
        {
            #region
            string extension = Path.GetExtension(fileName);

            MenuManager menuManager = new MenuManager();
            MenuInfo menuInfo = menuManager.GetMenuInfoByMenuId(memuId);
            if (menuInfo == null || string.IsNullOrEmpty(menuInfo.menuImagePath))
            {
                //需要处理
                return string.Empty;
            }
            string path = WebConfig.ImagePath + menuInfo.menuImagePath;
            Random random = new Random();
            int index = random.Next(0, 10000);
            string romandFileName = string.Format("t_{3}_{0:yyyyMMddHHmmssfff}_{1:0000}{2}", DateTime.Now, index, extension, memuId);
            #endregion

            using (Image original_image = Image.FromStream(stream))
            {
                int width = original_image.Width;
                int height = original_image.Height;
                #region

                int target_width = 4;
                int target_height = 3;
                int new_width, new_height;
                int paste_x, paste_y;

                float target_ratio = (float)target_width / (float)target_height;
                float image_ratio = (float)width / (float)height;
                //if (width > 1128 && height > 846)
                //{
                //    target_width = 1128;
                //    target_height = 846;

                //    if (target_ratio > image_ratio)
                //    {
                //        new_height = target_height;
                //        new_width = (int)Math.Floor(image_ratio * (float)target_height);
                //    }
                //    else
                //    {
                //        new_height = (int)Math.Floor((float)target_width / image_ratio);
                //        new_width = target_width;
                //    }

                //    new_width = new_width > target_width ? target_width : new_width;
                //    new_height = new_height > target_height ? target_height : new_height;
                //    paste_x = (target_width - new_width) / 2;
                //    paste_y = (target_height - new_height) / 2;
                //}
                //else
                //{
                if (image_ratio > target_ratio)
                {
                    new_width = width;
                    new_height = (int)Math.Floor(width / target_ratio);
                }
                else
                {
                    new_height = height;
                    new_width = (int)Math.Floor(height * target_ratio);
                }
                target_width = new_width;
                target_height = new_height;
                paste_x = (new_width - width) / 2;
                paste_y = (new_height - height) / 2;
                new_width = width;
                new_height = height;
                //}

                #endregion

                ImageTask itask = new ImageTask
                {
                    Extension = extension,
                    Name = Path.GetFileNameWithoutExtension(fileName),
                    Path = path,
                    CreateTime = DateTime.Now,
                    UserId = userId,
                    FileName = romandFileName,
                    MenuId = memuId,
                    ShopId = shopId,
                    EqualProportion = target_ratio == image_ratio
                };

                #region
                using (Image final_image = new Bitmap(target_width, target_height))
                {
                    using (Graphics graphic = Graphics.FromImage(final_image))
                    {
                        graphic.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.Black), new System.Drawing.Rectangle(0, 0, target_width, target_height));

                        graphic.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

                        graphic.DrawImage(original_image, paste_x, paste_y, new_width, new_height);

                        using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
                        {
                            conn.Open();

                            using (SqlTransaction tran = conn.BeginTransaction())
                            {
                                ImageTaskManager iTaskManager = new ImageTaskManager(tran);
                                try
                                {
                                    itask.Id = iTaskManager.Insert(itask);

                                    //final_image.Save(imagePath, ImageFormat.Jpeg);

                                    HttpServerUtility server = System.Web.HttpContext.Current.Server;
                                    final_image.Save(server.MapPath(WebConfig.Temp + romandFileName), ImageFormat.Jpeg);

                                    CloudStorageResult uploadResult = CloudStorageOperate.PutObject(path + romandFileName, server.MapPath(WebConfig.Temp + romandFileName));
                                    if (uploadResult.code)
                                    {
                                        File.Delete(server.MapPath(WebConfig.Temp + romandFileName));
                                        tran.Commit();
                                    }
                                }
                                catch
                                {
                                    itask.Id = 0;
                                    tran.Rollback();
                                }
                            }
                        }
                    }
                }
                #endregion

                string json = JsonOperate.JsonSerializer<SybMultiImageUploadResponse>(new SybMultiImageUploadResponse
                {
                    EqualProportion = itask.EqualProportion,
                    Id = itask.Id,
                    Name = itask.Name,
                    Url = WebConfig.CdnDomain + path + romandFileName
                });

                return json;

            }
        }

        public static object[] Make(int menuId, int id, int x, int y, int w, int h, int clientWidth, out string bigFileName)//, out string smallFileName 2014-6-9 菜图取消小图
        {
            object[] objResult = new object[] { false, "" };
            bigFileName = string.Empty;
            //smallFileName = string.Empty;
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                conn.Open();

                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    ImageTaskManager iTaskManager = new ImageTaskManager(tran);
                    ImageTask itask = iTaskManager.GetImageTaskById(id);
                    if (itask != null)
                    {
                        CloudStorageObject cloudStorageObject = CloudStorageOperate.GetObject(itask.Path + itask.FileName);
                        if (cloudStorageObject == null)
                        {
                            objResult[1] = "云空间获取图片失败！";
                        }
                        else
                        {
                            Image original_image = Image.FromStream(cloudStorageObject.Content);
                            int ow = original_image.Width;//session中取出来的原始图片
                            int oh = original_image.Height;

                            if (ow < oh && Math.Floor(Common.ToDouble(ow * 3 / 4)) != oh)
                            {
                                if (ow < Common.ToInt32(WebConfig.DishImageWidth))
                                {
                                    objResult[1] = "保存失败，图片比例应为4：3，且最小尺寸为" + WebConfig.DishImageWidth + "*" + WebConfig.DishImageHeight;
                                }
                                else
                                {
                                    objResult[1] = "保存失败，图片比例应为4：3，请改为" + ow + "*" + Math.Floor(Common.ToDouble(ow * 3 / 4));
                                }
                                return objResult;
                            }
                            if (w == 0 || h == 0)
                            {
                                w = clientWidth;
                                h = clientWidth * 3 / 4;
                            }
                            int pw = 0;
                            int ph = 0;
                            //前端截图后上传的图片宽高忽略了小数点，可能会导致截图小于最小尺寸，此处要处理一下
                            if (Math.Ceiling(Common.ToDouble(clientWidth) * Common.ToDouble(WebConfig.DishImageWidth) / Common.ToDouble(ow)) == w + 1)
                            {
                                pw = Common.ToInt32(WebConfig.DishImageWidth);
                                ph = Common.ToInt32(WebConfig.DishImageHeight);
                            }
                            else
                            {
                                pw = (w * ow) / clientWidth;
                                ph = (h * ow) / clientWidth;
                            }

                            int px = (x * ow) / clientWidth;//原始图片实际坐标
                            int py = (y * ow) / clientWidth;

                            #region 检查图片比例和最小尺寸 2014-6-11
                            //检查图片最小尺寸
                            if (pw < Common.ToInt32(WebConfig.DishImageWidth) || ph < Common.ToInt32(WebConfig.DishImageHeight))
                            {
                                objResult[1] = "保存失败，图片最小尺寸应为" + WebConfig.DishImageWidth + "*" + WebConfig.DishImageHeight;
                                return objResult;
                            }
                            if (x != 0)
                            {
                                float gongYueShu = Common.maxGongYueShu(pw, ph);
                                if (pw / gongYueShu != 4 || ph / gongYueShu != 3)
                                {
                                    //若裁剪，由于前端传过来的数据四舍五入，导致计算出的图片尺寸不是精确的4:3，以width为准重新计算height
                                    ph = pw * 3 / 4;
                                }
                            }
                            #endregion

                            Random random = new Random();
                            bigFileName = string.Format("0_{0}_{1:yyyyMMddHHmmssfff}_{2:0000}{3}", itask.MenuId, DateTime.Now, random.Next(0, 10000), itask.Extension);
                            //smallFileName = string.Format("1_{0}_{1:yyyyMMddHHmmssfff}_{2:0000}{3}", itask.MenuId, DateTime.Now, random.Next(0, 10000), itask.Extension);

                            try
                            {
                                iTaskManager.DeleteById(itask.Id);

                                MakeImage(itask.Path + bigFileName, bigFileName, original_image, 0, 0, pw, ph, px, py, pw, ph);//不限制尺寸一定是600*450，最小是960*720就好
                                //MakeImage(itask.Path + bigFileName, bigFileName, original_image, 0, 0, 600, 450, px, py, pw, ph);
                                //MakeImage(itask.Path + smallFileName, smallFileName, original_image, 0, 0, 272, 204, px, py, pw, ph);
                                original_image.Dispose();
                                //File.Delete(imagePath + itask.FileName);
                                CloudStorageOperate.DeleteObject(itask.Path + itask.FileName);
                                tran.Commit();

                                objResult[0] = true;
                            }
                            catch
                            {
                                tran.Rollback();
                            }
                        }
                    }
                    else
                    {
                        objResult[1] = "图片错误！比例应为4:3，最小尺寸" + WebConfig.DishImageWidth + "*" + WebConfig.DishImageHeight + "，且大小不能超过3M";
                    }
                }
            }
            return objResult;
        }

        private static void MakeImage(string objectKey, string fileName, Image bitmap, int x1, int y1, int w1, int h1, int x2, int y2, int w2, int h2)
        {
            using (Image cutimgae = new Bitmap(w2, h2))
            {
                using (Graphics graphic = Graphics.FromImage(cutimgae))
                {
                    //设置高质量插值法
                    graphic.InterpolationMode = InterpolationMode.High;
                    //设置高质量,低速度呈现平滑程度
                    graphic.SmoothingMode = SmoothingMode.HighQuality;
                    //清空画布并以透明背景色填充
                    graphic.Clear(Color.Transparent);

                    graphic.DrawImage(bitmap, new Rectangle(0, 0, w2, h2), new Rectangle(x2, y2, w2, h2), GraphicsUnit.Pixel);

                    //cutimgae.Save(fileName, ImageFormat.Jpeg);

                }

                using (Image image = new Bitmap(w1, h1))
                {
                    using (Graphics graphic = Graphics.FromImage(image))
                    {
                        //设置高质量插值法
                        graphic.InterpolationMode = InterpolationMode.High;
                        //设置高质量,低速度呈现平滑程度
                        graphic.SmoothingMode = SmoothingMode.HighQuality;
                        //清空画布并以透明背景色填充
                        graphic.Clear(Color.Transparent);

                        graphic.DrawImage(cutimgae, new Rectangle(x1, y1, w1, h1));
                    }
                    //image.Save(fileName, ImageFormat.Jpeg);
                    HttpServerUtility server = System.Web.HttpContext.Current.Server;
                    image.Save(server.MapPath(WebConfig.Temp + fileName), ImageFormat.Jpeg);
                    CloudStorageOperate.PutObject(objectKey, server.MapPath(WebConfig.Temp + fileName));
                    File.Delete(server.MapPath(WebConfig.Temp + fileName));
                }
            }
        }

        public static int GetUntreatedImagetaskCount(int userId, int shopId)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                conn.Open();

                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    ImageTaskManager iTaskManager = new ImageTaskManager(tran);
                    int count = iTaskManager.GetCountByCompanyShop(userId, shopId);
                    return count;
                }
            }
        }

        public static string GetUntreatedImageTaskList(int userId, int shopId)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                conn.Open();

                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    ImageTaskManager iTaskManager = new ImageTaskManager(tran);
                    List<ImageTask> list = iTaskManager.GetListByCompanyShop(userId, shopId);
                    List<SybMultiImageUploadResponse> lstResponse = new List<SybMultiImageUploadResponse>();
                    foreach (ImageTask itask in list)
                    {
                        lstResponse.Add(new SybMultiImageUploadResponse
                        {
                            Id = itask.Id,
                            EqualProportion = itask.EqualProportion,
                            Name = itask.Name,
                            Url = WebConfig.CdnDomain + itask.Path + itask.FileName
                        });
                    }
                    return JsonOperate.JsonSerializer<List<SybMultiImageUploadResponse>>(lstResponse);
                }
            }
        }

        public static bool DeleteUntreatedImageTask(int userId, int shopId)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                conn.Open();

                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    ImageTaskManager iTaskManager = new ImageTaskManager(tran);
                    List<ImageTask> list = iTaskManager.GetListByCompanyShop(userId, shopId);

                    try
                    {
                        foreach (ImageTask itask in list)
                        {
                            iTaskManager.DeleteById(itask.Id);
                            CloudStorageOperate.DeleteObject(itask.Path + itask.FileName);
                        }

                        tran.Commit();
                        return true;
                    }
                    catch
                    {
                        tran.Rollback();
                        return false;
                    }

                }
            }
        }

        public static bool DeleteImageTask(int id)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                conn.Open();

                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    ImageTaskManager iTaskManager = new ImageTaskManager(tran);
                    ImageTask itask = iTaskManager.GetImageTaskById(id);
                    if (itask != null)
                    {
                        try
                        {
                            iTaskManager.DeleteById(itask.Id);
                            CloudStorageOperate.DeleteObject(itask.Path + itask.FileName);
                            tran.Commit();
                            return true;
                        }
                        catch
                        {
                            tran.Rollback();
                            return false;
                        }
                    }
                    return true;
                }
            }
        }

        public class SybMultiImageUploadResponse
        {
            public int Id { set; get; }
            public bool EqualProportion { set; get; }
            public string Url { set; get; }
            public string Name { set; get; }
        }
    }
}
