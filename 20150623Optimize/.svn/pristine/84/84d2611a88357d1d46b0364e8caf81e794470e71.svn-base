using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.WebPageDll;

namespace Web.Control.DDL
{
    public class CompanyDropDownList
    {
        public void BindCompany(DropDownList ddl, int cityId)
        {
            List<CompanyViewModel> employeeCompany = new EmployeeConnShopOperate().QueryEmployeeCompany(SessionHelper.GetCurrectSessionEmployeeId(), cityId);
            ddl.DataSource = employeeCompany;
            ddl.DataTextField = "CompanyName";
            ddl.DataValueField = "CompanyID";
            ddl.DataBind();
            ddl.Items.Add(new ListItem("所有公司", "0"));
            ddl.SelectedValue = "0";
        }
    }
}
