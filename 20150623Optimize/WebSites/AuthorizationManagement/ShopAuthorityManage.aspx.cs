using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.WebPageDll.Services;
using VAGastronomistMobileApp.WebPageDll.Services.Infrastructure;

public partial class AuthorizationManagement_ShopAuthorityManage : System.Web.UI.Page
{


    int employeeID = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        var tmp_EmployeeID = Request.QueryString["EmployeeID"];

        if (!(int.TryParse(tmp_EmployeeID, out employeeID) && employeeID > 0))
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('无效的EmployeeID')</script>");
            return;
        }
        if (!Page.IsPostBack)
            Bind();
    }

    private void Bind()
    {
        try
        {
            IShopAuthorityService shopAuthorityService = ServiceFactory.Resolve<IShopAuthorityService>();

            var list = shopAuthorityService.GetWaiterRoleInfos(0, employeeID);
            GridView1.DataSource = list;
            GridView1.DataBind();
        }
        catch (ArgumentException exc)
        {

            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('" + exc.Message + "');window.location='EmployeeManage.aspx';</script>"); ;
        }

    }

    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        var roleId = (int)GridView1.DataKeys[GridView1.SelectedIndex].Values["roleId"];

        IViewAllocEmployeeAuthorityService viewAllocEmployeeAuthorityService = ServiceFactory.Resolve<IViewAllocEmployeeAuthorityService>();
        IList<ViewAllocEmployeeAuthority> vaea =
            viewAllocEmployeeAuthorityService.GetViewAllocEmployeeAuthorityByEmployeeAndShopAuthority(employeeID, roleId);
        if (vaea.Count > 0)
        {
            foreach (var v in vaea)
            {
                viewAllocEmployeeAuthorityService.Delete(v);
            }
        }
        else
        {
            ViewAllocEmployeeAuthority vv = new ViewAllocEmployeeAuthority()
            {
                EmployeeId = employeeID,
                ShopAuthorityId = roleId,
                Status = true
            };
            viewAllocEmployeeAuthorityService.Add(vv);
        }

        
        Bind();
    }
}