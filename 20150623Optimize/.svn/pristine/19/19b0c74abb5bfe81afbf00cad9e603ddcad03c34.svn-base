using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.Model.HomeNew;
using VAGastronomistMobileApp.WebPageDll;

namespace Web.Control.DDL
{
    public class CityDropDownList
    {
        public void BindCity(DropDownList ddl_city)
        {
            var cityOperate = new CityOperate();
            List<CityViewModel> data = cityOperate.GetHandleCity();
            ddl_city.DataSource = data;
            ddl_city.DataTextField = "cityName";
            ddl_city.DataValueField = "cityId";
            ddl_city.DataBind();
            ddl_city.Items.Insert(0, new ListItem("所有", "0"));
            //ddl_city.SelectedValue = "87";
        }

        public void BindCity(DropDownList ddl_city, string cityID)
        {
            var cityOperate = new CityOperate();
            List<CityViewModel> data = cityOperate.GetHandleCity();
            ddl_city.DataSource = data;
            ddl_city.DataTextField = "cityName";
            ddl_city.DataValueField = "cityId";
            ddl_city.DataBind();
            //ddl_city.Items.Insert(0, new ListItem("所有", "0"));
            ddl_city.SelectedValue = cityID;
        }
    }
}
