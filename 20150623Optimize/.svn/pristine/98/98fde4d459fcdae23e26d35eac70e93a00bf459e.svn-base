using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.WebPageDll;

public partial class RedEnvelope_CustomerRedEnvelopeDetail : System.Web.UI.Page
{
    private readonly RedEnvelopeDetailOperate redEnvelopeDetailOperate = new RedEnvelopeDetailOperate();
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    [WebMethod]
    public string GetWebRedEnvelopeDetail(int pageIndex, int pageSize, string cookie)
    {
        WebRedEnvelope queryData = redEnvelopeDetailOperate.GetWebRedEnvelopeDetail(pageIndex, pageSize, cookie, "");
        return JsonOperate.JsonSerializer(queryData);
    }
}