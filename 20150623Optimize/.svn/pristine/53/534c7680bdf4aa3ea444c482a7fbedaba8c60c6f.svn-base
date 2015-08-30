using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using System.Drawing.Imaging;
using VAGastronomistMobileApp.WebPageDll;

/// <summary>
///ImageHandler 的摘要说明
/// </summary>
public class ImageHandler : IHttpHandler
{
	public ImageHandler()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}
    #region IHttpHandler 成员
    /// <summary>
    /// 指示IHttpHandler 实例是否可再次使用
    /// </summary>
    public bool IsReusable
    {
        get { return true; }
    }
    /// <summary>
    /// 处理请求的方法
    /// </summary>
    /// <param name="context">它提供对用于为 HTTP 请求提供服务的内部服务器对象（如 Request、Response、Session 和 Server）的引用。</param>
    public void ProcessRequest(HttpContext context)
    {
        try
        {
            //获取请求的物理图片路径
            string imagePath = context.Request.PhysicalPath;
            Bitmap image = null;
            if (context.Cache[imagePath] == null)//如果当前缓存中没有指定的图片就将该图片添加水印并缓存
            {
                image = new Bitmap(imagePath);
                image = Common.AddWaterMark(image);
                //context.Cache[imagePath] = image;//取消图片缓存
            }
            else//否则就直接从混存中取出添加了水印的图片，节省时间
            {
                image = context.Cache[imagePath] as Bitmap;
            }
            image.Save(context.Response.OutputStream, ImageFormat.Jpeg);//将添加水印的图片输入到当前流中
            /*标明类型为jpg，如果不加，使用IE浏览不会有问题，用FireFox就会是乱码*/
            context.Response.ContentType = "image/jpeg";//制定输出流的类型
            context.Response.Flush();//向客户端发送当前所有缓冲的输出
            context.Response.End();
        }
        catch (Exception)
        {

        }
    }

    #endregion

    //给图片添加水印
    //private Bitmap AddWaterMark(Bitmap image)
    //{
    //    string text = System.Configuration.ConfigurationManager.AppSettings["WaterMark"].ToString();
    //    int fontSize = int.Parse(System.Configuration.ConfigurationManager.AppSettings["Font-Size"].ToString());
    //    Font font = new Font("宋体", fontSize);

    //    Brush brush = Brushes.DarkGray;
    //    Graphics g = Graphics.FromImage(image);
    //    SizeF size = g.MeasureString(text, font);
    //    g.DrawString(text, font, brush, image.Width - size.Width, image.Height - size.Height);
    //    g.Dispose();
    //    return image;
    //}
}