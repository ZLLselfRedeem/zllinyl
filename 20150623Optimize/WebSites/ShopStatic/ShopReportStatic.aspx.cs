using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.WebPageDll;
using VAGastronomistMobileApp.Model;

public partial class ShopStatic_ShopReportStatic : System.Web.UI.Page
{
    #region 方法 
    private void DoInit()
    {
        if (!this.IsPostBack)
        {
            this.AspNetPager1.CurrentPageIndex = 1;
            DoQuery();
        }
    }

    private void DoQuery()
    {
        var queryObject = new ShopReportVQueryObject()
        {
            ShopNameFuzzy = this.TextBoxShopName.Text
        };
        if (!string.IsNullOrEmpty(this.DropDownReportType.SelectedValue))
        {
            queryObject.ReportValue = int.Parse(this.DropDownReportType.SelectedValue);
        }
        this.AspNetPager1.RecordCount = (int)ShopReportVOperate.GetCountByQuery(queryObject);
        var list = ShopReportVOperate.GetListByQuery(this.AspNetPager1.PageSize, this.AspNetPager1.CurrentPageIndex, queryObject);
        this.GridViewShopReport.DataSource = list;
        this.GridViewShopReport.DataBind();
    }
    #endregion

    #region 事件
    protected void Page_Load(object sender, EventArgs e)
    {
        DoInit();
    }
    protected void ButtonQuery_Click(object sender, EventArgs e)
    {
        this.AspNetPager1.CurrentPageIndex = 1;
        DoQuery();
    }
    protected void GridViewShopReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        var entity = e.Row.DataItem as ShopReportV;
        if (entity != null)
        {
          Label LabelManagerName =  e.Row.FindControl("LabelManagerName") as Label;
          Label LabelManagerPhoneNumber = e.Row.FindControl("LabelManagerPhoneNumber") as Label;
          if (LabelManagerName != null && LabelManagerPhoneNumber != null)
          {
              var shopOperate = new ShopOperate();
              var shopInfo = shopOperate.QueryShop(entity.ShopId);
              EmployeeOperate employeeOperate = new EmployeeOperate();
              if (shopInfo.accountManager.HasValue)
              {
                  var employee = employeeOperate.QueryEmployee(shopInfo.accountManager.Value);
                  if (employee != null)
                  {
                      LabelManagerName.Text = employee.EmployeeFirstName;
                      LabelManagerPhoneNumber.Text = employee.EmployeePhone;
                  }
              } 
              Label LabelCity = e.Row.FindControl("LabelCity") as Label;
              if (LabelCity != null)
              {
                  CityOperate cityOperate = new CityOperate();
                  var city = cityOperate.GetCityNameAndShopName(shopInfo.shopID);
                  LabelCity.Text = city.cityName;
              }
          }
          Label LabelReportInformation = e.Row.FindControl("LabelReportInformation") as Label;
          if (LabelReportInformation != null)
          {
              LabelReportInformation.Text = Common.GetEnumDescription((ShopReportEnum)entity.ReportValue);
          }
        }
    }
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        this.DoQuery();
    }
    #endregion
}