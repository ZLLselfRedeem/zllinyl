using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.SQLServerDAL;


public partial class ServerStatus : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int result = 0;
        System.Random random = new Random(~unchecked((int)DateTime.Now.Ticks));
        result = random.Next(1, 100);
        Response.Write(result);
        Response.End();
    }
}