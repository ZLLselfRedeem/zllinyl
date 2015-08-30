using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.WebPageDll;

public partial class ServerLog_clientErrorDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            long errorId = Common.ToInt64(Request.QueryString["errorId"]);
            ClientErrorInfoOperate operate = new ClientErrorInfoOperate();
            errorMessage.Text = operate.QueryErrorMessage(errorId);
        }
    }
}