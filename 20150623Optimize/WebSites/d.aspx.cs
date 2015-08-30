using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.WebPageDll;
using VAGastronomistMobileApp.Model;

public partial class d : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string typeId = "", shopId = "";
        if (!string.IsNullOrEmpty(Request.QueryString["t"]))
        {
            typeId = Request.QueryString["t"];
        }
        if (!string.IsNullOrEmpty(Request.QueryString["s"]))
        {
            shopId = Request.QueryString["s"];
        }
        if (typeId != "" || shopId != "")
        {
            SaveQRCodePV(typeId, shopId);
        }
    }

    protected void SaveQRCodePV(string typeId, string shopId)
    {
        try
        {
            QRCodePVOperate _QRCodePV = new QRCodePVOperate();
            QRCodePageView qrCodePV = new QRCodePageView();
            qrCodePV.typeId = Common.ToInt32(typeId);
            qrCodePV.shopId = Common.ToInt32(shopId);
            qrCodePV.visitTime = DateTime.Now;

            _QRCodePV.InsertQRCodePV(qrCodePV);
        }
        catch (Exception)
        {
        }
    }
}