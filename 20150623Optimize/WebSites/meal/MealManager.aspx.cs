using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.WebPageDll;

public partial class Meal_MealManager : System.Web.UI.Page
{
    #region 属性
    public string MealName
    {
        get
        {
            return this.TextBoxMealName.Text;
        }
        set
        {
            this.TextBoxMealName.Text = value;
        }
    }

    public int? CityID
    {
        get
        {
            int cityID;
            if(int.TryParse(this.DropDownListCity.SelectedValue,out cityID))
            {
                return cityID;
            }
            return null;
        }
        set
        {
            this.DropDownListCity.SelectedValue = value.ToString();
        }
    }

    public int? CompanyID
    {
        get
        {
            int companyID;
            if (int.TryParse(this.DropDownListCompany.SelectedValue, out companyID))
            {
                return companyID;
            }
            return null;
        }
        set
        {
            this.DropDownListCompany.SelectedValue = value.ToString();
        }
    }
    public int? ShopID
    {
        get
        {
            int shopID;
            if (int.TryParse(this.DropDownListShop.SelectedValue, out shopID))
            {
                return shopID;
            }
            return null;
        }
        set
        {
            this.DropDownListShop.SelectedValue = value.ToString();
        }
    }
    public int? IsActive
    {
        get
        {
            int isActive;
            if (int.TryParse(this.DropDownListIsActive.SelectedValue, out isActive))
            {
                return isActive;
            }
            return null;
        }
        set
        {
            this.DropDownListIsActive.SelectedValue = value.ToString();
        }
    }

    public DateTime? CreationDateTo
    {
        get
        {
            DateTime creationDateTo;

            if (DateTime.TryParse(Request.Form[this.TextBoxCreatedTo.ClientID], out creationDateTo))
            {
                return creationDateTo.AddDays(0.999999); ;
            }
            return null;
        }
        set
        {
            if (value.HasValue)
            {
                this.TextBoxCreatedTo.Text = value.Value.ToString("yyyy-MM-dd");
            }
        }
    }
    public DateTime? CreationDateFrom
    {
        get
        {
            DateTime creationDateFrom;
            if (DateTime.TryParse(Request.Form[this.TextBoxCreatedFrom.ClientID], out creationDateFrom))
            {
                return creationDateFrom.Date;
            }
            return null;
        }
        set
        {
            if (value.HasValue)
            {
                this.TextBoxCreatedFrom.Text = value.Value.ToString("yyyy-MM-dd");
            }
        }
    }
    #endregion
    #region 方法
    private void DoQuery()
    {
        MealQueryObject queryObject = new MealQueryObject()
        {
            IsActive = this.IsActive,
            CityID = this.CityID,
            CreationDateTo = this.CreationDateTo,
            CreationDateFrom = this.CreationDateFrom,
            ShopID = this.ShopID,
            CompanyID = this.CompanyID,
            MealName = this.MealName
        };
        var operate = new MealOperate();
        int count = operate.GetMealTableCountByQuery(queryObject);
        this.AspNetPagerMeal.RecordCount = count;
        var mealTable = operate.GetMealTableByQuery(queryObject, this.AspNetPagerMeal.CurrentPageIndex, this.AspNetPagerMeal.PageSize);
        this.GridViewMeal.DataSource = mealTable;
        this.GridViewMeal.DataBind();

    }
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CompanyOperate companyOperate = new CompanyOperate(); 
            var companyTable = companyOperate.QueryCompany();
            this.DropDownListCompany.DataSource = companyTable;
            this.DropDownListCompany.DataBind();
            this.DropDownListCompany.Items.Insert(0, "--请选择--");
            this.DropDownListShop.Items.Insert(0, "--请选择--"); 
            this.AspNetPagerMeal.CurrentPageIndex = 1;
            this.DoQuery();
        }
    }
    protected void LinkButtonOperate_Click(object sender, EventArgs e)
    {

    } 
    protected void ButtonQuery_Click(object sender, EventArgs e)
    {
        this.AspNetPagerMeal.CurrentPageIndex = 1;
        this.DoQuery();
    }

    
    protected void GridViewMeal_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        var dataRow = e.Row.DataItem as DataRowView;
        if (dataRow != null)
        {
            int createdBy = int.Parse(dataRow["CreatedBy"].ToString());
            EmployeeOperate employeeOperate = new EmployeeOperate();
            var employee = employeeOperate.QueryEmployee(createdBy);
            var LabelCreator =e.Row.FindControl("LabelCreator") as Label;
            if (employee!=null && LabelCreator != null)
            {
                LabelCreator.Text = employee.EmployeeFirstName;
            }
            var LabelIsActive = e.Row.FindControl("LabelIsActive") as Label;
            IsActive active = (IsActive)int.Parse(dataRow["IsActive"].ToString());
            if (LabelIsActive != null)
            {
               LabelIsActive.Text = Common.GetEnumDescription(active);
            }
            var LabelShopState = e.Row.FindControl("LabelShopState") as Label;
            if (LabelShopState != null)
            {
                var shopOperate = new ShopOperate();
                var shopInfo = shopOperate.QueryShop(int.Parse(dataRow["ShopID"].ToString()));
                if (shopInfo != null)
                {
                    if (shopInfo.isHandle == 1)
                    {
                        LabelShopState.Text = "已上线";
                    }
                    else
                    {
                        LabelShopState.Text = "未上线";
                    }
                }
            }
        }
    }
    protected void AspNetPagerMeal_PageChanged(object sender, EventArgs e)
    {
        this.DoQuery();
    }
    protected void DropDownListCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.CompanyID.HasValue)
        {
            ShopOperate operate = new ShopOperate();
            var shopTable = operate.QueryCompanyShop(this.CompanyID.Value);
            if (shopTable != null)
            {
                this.DropDownListShop.DataSource = shopTable;
                this.DropDownListShop.DataBind();
            }
            else
            {
                this.DropDownListShop.Items.Clear();
            }
        }
        else
        { 
            this.DropDownListShop.Items.Clear();
        }
        this.DropDownListShop.Items.Insert(0,"--请选择--");
    }
}