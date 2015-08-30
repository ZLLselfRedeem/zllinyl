using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

/// <summary>
/// 批量上传模块
/// </summary>
public partial class CompanyPages_DishMutiUpload : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        HttpPostedFile file = Request.Files["Filedata"];
        string folder = Request["folder"];

        Random rnd = new Random();
        var fileUploadFolder = Server.MapPath(".")+ System.Configuration.ConfigurationManager.AppSettings["ImagePath"] + "\\Temp\\";
        var fileName =  fileUploadFolder + System.Guid.NewGuid().ToString()
            + file.FileName.Substring(file.FileName.LastIndexOf(".")).ToLower();
        file.SaveAs(fileName);
    }
}