using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.WebPageDll;

public partial class Meal_MealScheduling : System.Web.UI.Page
{
    #region 属性
    public int MealID
    {
        get
        {
            if (ViewState["MealID"] != null)
            {
                return int.Parse(ViewState["MealID"].ToString());
            }
            return 0;
        }
        set
        {
            ViewState["MealID"] = value;
        }
    }
    public int MealScheduleID
    {
        get
        {
            if (ViewState["MealScheduleID"] != null)
            {
                return int.Parse(ViewState["MealScheduleID"].ToString());
            }
            return 0;
        }
        set
        {
            ViewState["MealScheduleID"] = value;
        }
    }
    public DateTime DinnerTime
    {
        get
        {
            DateTime date;

            if (DateTime.TryParse(Request.Form[this.TextBoxDinnerTime.ClientID], out date))
            {
                return date;
            }
            return DateTime.MaxValue;
        }
        set
        {
            this.TextBoxDinnerTime.Text = value.ToString("yyyy-MM-dd HH:mm");
        }
    }
    public DateTime ValidTo
    {
        get
        {
            DateTime date;

            if (DateTime.TryParse(Request.Form[this.TextBoxValidTo.ClientID], out date))
            {
                return date;
            }
            return DateTime.MaxValue;
        }
        set
        {
            this.TextBoxValidTo.Text = value.ToString("yyyy-MM-dd HH:mm");
        }
    }
    public int TotalCount
    {
        get
        {
            return int.Parse(this.TextBoxTotalCount.Text);
        }
        set
        {
            this.TextBoxTotalCount.Text = value.ToString();
        }
    }
    public int DinnerType
    {
        get
        {
            return int.Parse(this.DropDownListDinnerType.SelectedValue);
        }
        set
        {
            this.DropDownListDinnerType.SelectedValue = value.ToString();
        }
    }
    #endregion

    private void DoQuery()
    {
        var mealScheduleOperate = new MealScheduleOperate();
        var list = mealScheduleOperate.GetListByQuery(new MealScheduleQueryObject() { MealID = this.MealID });

        this.GridViewMeal.DataSource = list;
        this.GridViewMeal.DataBind();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["MealID"] == null)
            {
                return;
            }
            this.MealID = int.Parse(Request.QueryString["MealID"]);
            MealOperate mealOperate = new MealOperate();
            DataTable mealTable =  mealOperate.GetMealTableByQuery(new MealQueryObject() { MealID = this.MealID }, 1, 1);
            if (mealTable != null)
            {
                EmployeeOperate employeeOperate = new EmployeeOperate();
                var employee = employeeOperate.QueryEmployee(int.Parse(mealTable.Rows[0]["CreatedBy"].ToString()));
                if (employee != null)
                {
                    this.LabelCreator.Text = employee.EmployeeFirstName;
                }
                this.LabelMealName.Text = mealTable.Rows[0]["MealName"].ToString();
                this.LabelIsActive.Text = Common.GetEnumDescription((IsActive)int.Parse(mealTable.Rows[0]["IsActive"].ToString()));
                this.LabelCompany.Text = mealTable.Rows[0]["CompanyName"].ToString();
                this.LabelShop.Text = mealTable.Rows[0]["ShopName"].ToString();
            }
            DoQuery();

        }
    }

  
    protected void LinkButtonEdit_Click(object sender, EventArgs e)
    {

    }
    protected void GridViewMeal_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        var mealSchedule = e.Row.DataItem as MealSchedule;
        if (mealSchedule != null)
        {
            var LabelDinnerType = e.Row.FindControl("LabelDinnerType") as Label;
            if (LabelDinnerType != null)
            {
                LabelDinnerType.Text = mealSchedule.DinnerType.ToString();
            }
        }
    }
    protected void GridViewMeal_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "E")
        {
            this.MealScheduleID = int.Parse(e.CommandArgument.ToString());
            var mealScheduleOperate = new MealScheduleOperate();
            var mealSchedule = mealScheduleOperate.GetEntityByID(this.MealScheduleID);
            if (mealSchedule != null)
            {
                this.ValidTo = mealSchedule.ValidTo;
                this.DinnerTime = mealSchedule.DinnerTime;
                this.TotalCount = mealSchedule.TotalCount;
                this.DinnerType = (int)mealSchedule.DinnerType;
            }
            this.ButtonAdd.Visible = false;
            this.ButtonUpdate.Visible = true;
            this.Panel_Window.Visible = true;
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>ConfirmWindow('Panel_Window');</script>");
        }
    }
    protected void ButtonAdd_Click(object sender, EventArgs e)
    {
        MealScheduleOperate mealScheduleOperate = new MealScheduleOperate();
        var queryObject = new MealScheduleQueryObject()
        {
            DinnerTime = this.DinnerTime,
            DinnerType = this.DinnerType,
            MealID = this.MealID
        };
        if (mealScheduleOperate.GetCountByQuery(queryObject) > 0)
        {
            CommonPageOperate.AlterMsg(this, string.Format("{0} 【{1}】排期已存在，请选择其他时段！", this.DinnerTime, ((DinnerType)this.DinnerType).ToString()));
            return;
        }
        var entity = new MealSchedule()
        {
            SoldCount = 0,
            TotalCount = this.TotalCount,
            DinnerTime = this.DinnerTime,
            DinnerType = (VAGastronomistMobileApp.Model.DinnerType)this.DinnerType,
            MealID = this.MealID,
            ValidFrom = DateTime.Parse("1970-1-1"),
            ValidTo = this.ValidTo,
            IsActive = 1
        };
        if (mealScheduleOperate.AddEntity(entity))
        {
            this.Panel_Window.Visible = false;
            this.DoQuery();
        }

    }
    protected void ButtonUpdate_Click(object sender, EventArgs e)
    {
        MealScheduleOperate mealScheduleOperate = new MealScheduleOperate();
        var queryObject = new MealScheduleQueryObject()
        {
            DinnerTime = this.DinnerTime,
            DinnerType = this.DinnerType,
            MealID = this.MealID
        };
        var list = mealScheduleOperate.GetListByQuery(queryObject);
        if (list.Count > 0 && !list.Exists(p => p.MealScheduleID == this.MealScheduleID))
        {
            CommonPageOperate.AlterMsg(this, string.Format("{0} 【{1}】排期已存在，请选择其他时段！", this.DinnerTime, ((DinnerType)this.DinnerType).ToString()));
            return;
        }
        var entity = mealScheduleOperate.GetEntityByID(this.MealScheduleID);
        if (entity != null)
        { 
            entity.TotalCount = this.TotalCount;
            entity.DinnerTime = this.DinnerTime;
            entity.DinnerType = (VAGastronomistMobileApp.Model.DinnerType)this.DinnerType;
            entity.MealID = this.MealID;
            entity.ValidFrom = DateTime.Parse("1970-1-1");
            entity.ValidTo = this.ValidTo;
            entity.IsActive = 1;
            if (mealScheduleOperate.UpdateEntity(entity))
            {
                this.Panel_Window.Visible = false;
                this.DoQuery();
            }
        }
    }
    protected void ButtonCancel_Click(object sender, EventArgs e)
    {
        this.Panel_Window.Visible = false;
    }
    protected void LinkButtonAdd_Click(object sender, EventArgs e)
    {
        this.MealScheduleID = 0;

        this.ValidTo = DateTime.Now;
        this.DinnerTime = DateTime.Now;
        this.TotalCount = 0;
        this.DinnerType = 1;

        this.ButtonAdd.Visible = true;
        this.ButtonUpdate.Visible = false;
        this.Panel_Window.Visible = true;
        Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>ConfirmWindow('Panel_Window');</script>");
    }


}