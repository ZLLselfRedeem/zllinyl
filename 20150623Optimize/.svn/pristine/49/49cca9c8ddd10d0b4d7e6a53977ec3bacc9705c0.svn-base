<%@ WebHandler Language="C#" Class="upload" %>

using System;
using System.Web;
using System.IO;
using System.Text;
using System.Net;
using VAGastronomistMobileApp.WebPageDll;
using VAGastronomistMobileApp.Model;
using System.Configuration;
using System.Web.SessionState;
using System.Collections.Generic;
public class upload : IHttpHandler, IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {

        context.Response.ContentType = "text/plain";
        context.Response.Charset = "utf-8";

        try
        {
            System.Drawing.Image original_image = null;
            // Get the data
            HttpPostedFile jpeg_image_upload = context.Request.Files["Filedata"];

            // Retrieve the uploaded image
            original_image = System.Drawing.Image.FromStream(jpeg_image_upload.InputStream);

            // Calculate the new width and height
            string thumbnail_id = "";
            ImageUploadOperate image = new ImageUploadOperate();
            thumbnail_id = image.GetthumbnailId(original_image);

            context.Response.StatusCode = 200;
            context.Response.Write(thumbnail_id);

        }
        catch
        {
            // If any kind of error occurs return a 500 Internal Server error
            context.Response.StatusCode = 500;
            context.Response.Write("An error occured");
            context.Response.End();
        }
        finally
        {

            context.Response.End();
        }
    }
    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}