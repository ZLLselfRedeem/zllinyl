using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

using ThoughtWorks;
using ThoughtWorks.QRCode;
using ThoughtWorks.QRCode.Codec;
using ThoughtWorks.QRCode.Codec.Data;

using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.Model.QueryObject;
using VAGastronomistMobileApp.WebPageDll;
using System.Text;
using System.IO;
using System.Drawing.Imaging;


public partial class Coupon_SystemShareCouponDetail : System.Web.UI.Page
{
    public int QrCodeId
    {
        get
        {
            if (ViewState["QrCodeId"] == null)
            {
                return 0;
            }
            return int.Parse(ViewState["QrCodeId"].ToString());
        }
        set
        {
            ViewState["QrCodeId"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["CouponSendDetailId"]))
            {
                int couponSendDetailId = int.Parse(Request.QueryString["CouponSendDetailId"]);
                var couponSendDetail = CouponSendDetailOperate.GetEntityById(couponSendDetailId);
                if (couponSendDetail != null)
                {
                    var couponGetDetailVs = CouponGetDetailVOperate.GetListByQuery(new CouponGetDetailVQueryObject()
                    {
                        CouponSendDetailID = couponSendDetailId
                    });

                    this.LabelAmount.Text = couponGetDetailVs.Where(p => p.PreOrderSum.HasValue).Sum(p => p.PreOrderSum).Value.ToString("F2");
                    this.LabelGetCount.Text = couponGetDetailVs.Count().ToString();
                    this.LabelUsedCount.Text = couponGetDetailVs.Count(p => p.State == 2).ToString();
                    this.LabelCreateTime.Text = couponSendDetail.CreateTime.ToString("yyyy-MM-dd");

                    var employee = new EmployeeOperate().QueryEmployee(couponSendDetail.CreatedBy);
                    if (employee != null)
                    {
                        this.LabelCreatedBy.Text = employee.EmployeeFirstName;
                    }

                    var qrCode = QrCodeOperate.GetFirstByQuery(new QrCodeQueryObject()
                    {
                        LinkKey = couponSendDetailId,
                        Type = 1 
                    });
                    if (qrCode != null)
                    {
                        this.QrCodeId = qrCode.Id;
                        this.LabelName.Text = qrCode.Name;
                        this.LabelRemark.Text = qrCode.Remark; 
                        this.ImageQrCode.ImageUrl = "QrCode.aspx?QrCodeId=" + qrCode.Id;
                        this.HyperLinkLongLink.NavigateUrl = WebConfig.ServerDomain
                            + string.Format( 
                                @"AppPages/discountCoupons/v3.html?id={0}&shareType=2&cityId={1}",couponSendDetail.CouponSendDetailID,qrCode.CityId);
                        this.HyperLinkLongLink.Text = this.HyperLinkLongLink.NavigateUrl;

                        this.HyperLinkShortLink.NavigateUrl = WebConfig.ServerDomain + @"15/7.html?c=" + qrCode.Id;
                        this.HyperLinkShortLink.Text = this.HyperLinkShortLink.NavigateUrl;
                        var city = CityOperate.GetCityByCityId(qrCode.CityId);
                        if (city != null)
                        {
                            this.LabelCityName.Text = city.cityName;
                        }
                    }
                }
            }
        }
    }
    protected void ButtonDownload_Click(object sender, EventArgs e)
    {
        Bitmap bt;
        string enCodeString = string.Format(WebConfig.CdnDomain + "15/7.html?c={0}", this.QrCodeId);
        QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
        bt = qrCodeEncoder.Encode(enCodeString, Encoding.UTF8);
        using (MemoryStream stream = new MemoryStream())
        {
            bt.Save(stream, ImageFormat.Bmp);
            byte[] mydata = new byte[stream.Length];
            mydata = stream.ToArray();

            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.ContentType = "image/gif";
            Response.Charset = "UTF8";
            Response.AppendHeader("Content-Disposition", "attachment;filename=" + this.QrCodeId + ".bmp");
            Response.ContentEncoding = System.Text.Encoding.Default;//设置输出流为简体中文
            Response.OutputStream.Write(mydata, 0, mydata.Length);   
            Response.End();
        }  
    }
}