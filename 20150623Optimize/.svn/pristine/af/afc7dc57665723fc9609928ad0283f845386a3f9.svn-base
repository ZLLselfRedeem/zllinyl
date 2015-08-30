using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.WebPageDll;
using System.IO;

public partial class POSLiteVersion : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int type = Convert.ToInt32(Request.Form["type"]);
        string msg = Request.Form["msg"];
        PreOrder19dianOperate preOrder19dianOpe = new PreOrder19dianOperate();
        VALatestPOSLiteInfo latestPOSLiteInfo = preOrder19dianOpe.QueryPOSLiteLatestInfo(type);
        string result = JsonOperate.JsonSerializer<VALatestPOSLiteInfo>(latestPOSLiteInfo);
        Response.Write(result);
        Response.End();
    }
}