using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ThoughtWorks.QRCode.Codec;
using VAGastronomistMobileApp.Model;

public partial class Coupon_QrCode : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int qrCodeId = int.Parse(Request.QueryString["QrCodeId"]);
        Bitmap bt;
        string enCodeString = string.Format(WebConfig.ServerDomain + "15/7.html?c={0}", qrCodeId);
        QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
        bt = qrCodeEncoder.Encode(enCodeString, Encoding.UTF8);
        using (MemoryStream stream = new MemoryStream())
        {
            bt.Save(stream, ImageFormat.Bmp);  
            byte[] mydata = new byte[stream.Length];
            mydata = stream.ToArray();

            Response.Clear();
            Response.ContentType = "image/gif";
            Response.OutputStream.Write(mydata, 0, mydata.Length);
            Response.End();
        }  
    }
}