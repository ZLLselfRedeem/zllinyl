using LogDll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AppPages_weChat_callbackAddress : System.Web.UI.Page
{
    protected string message = null;
    protected string state = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        state = Session.SessionID;
        Session["weChatState"] = state;
    }
}