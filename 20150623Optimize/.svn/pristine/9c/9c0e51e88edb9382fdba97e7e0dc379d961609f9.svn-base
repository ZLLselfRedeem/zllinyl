using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.WebPageDll;

namespace Web.Control.DDL
{
    public class ShopDropDownList
    {
        public void BindShop(DropDownList ddl, int companyId)
        {
            ddl.DataSource = new EmployeeConnShopOperate().QueryEmployeeShopByCompanyAndEmplyee(SessionHelper.GetCurrectSessionEmployeeId(), companyId); ;
            ddl.DataTextField = "shopName";
            ddl.DataValueField = "shopID";
            ddl.DataBind();
            ddl.Items.Add(new ListItem("所有门店", "0"));
            ddl.SelectedValue = "0";
        }
    }
}
