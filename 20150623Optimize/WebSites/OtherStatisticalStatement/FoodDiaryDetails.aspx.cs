using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL.Persistence;
using VAGastronomistMobileApp.SQLServerDAL.Persistence.Infrastructure;
using VAGastronomistMobileApp.WebPageDll.Services.Infrastructure;

public partial class OtherStatisticalStatement_FoodDiaryDetails : System.Web.UI.Page
{
    private DateTime startDate;
    private DateTime endDate;
    private string mobilePhoneNumber;
    private bool isPaid;
    private const int PageSize = 20;
    protected void Page_Load(object sender, EventArgs e)
    {
        //start_date.Value
        
        if (!DateTime.TryParse(start_date.Value, out startDate))
        {
            startDate = DateTime.Now.Date;
        }
        if (!DateTime.TryParse(end_date.Value, out endDate))
        {
            endDate = DateTime.Now.AddDays(1).Date;
        }
        else
        {
            endDate = endDate.AddDays(1);
        }
        mobilePhoneNumber = txtMobilePhoneNumber.Text;
        isPaid = checkbox_isPaid.Checked;
        if (!Page.IsPostBack)
        {
            checkbox_isPaid.Checked = true;
            isPaid = true;
            start_date.Value = startDate.ToString("d");
            end_date.Value = endDate.AddDays(-1).ToString("d");
            Bind(1, PageSize, startDate, endDate, isPaid, mobilePhoneNumber);
        }
    }

    private void Bind(int pageIndex, int pageSize, DateTime startDate, DateTime endDate, bool isPaid, string mobilePhoneNumber)
    {
        AspNetPager1.PageSize = pageSize;

        IFoodDiaryRepository foodDiaryRepository = ServiceFactory.Resolve<IFoodDiaryRepository>();
        int count = 0;
        var foodDiaryDetailses = foodDiaryRepository.GetPageFoodDiaryDetailses(new VAGastronomistMobileApp.SQLServerDAL.Persistence.Infrastructure.Page(pageIndex, pageSize), mobilePhoneNumber, isPaid, startDate, endDate, out count);

        AspNetPager1.RecordCount = count;
        fooddiaryGv.DataSource = foodDiaryDetailses.ToList();

        fooddiaryGv.DataBind();
    }
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        Bind(AspNetPager1.CurrentPageIndex, PageSize, startDate, endDate, isPaid, mobilePhoneNumber);
    }

    protected void button1_OnClick(object sender, EventArgs e)
    {
        Bind(1, PageSize, startDate, endDate, isPaid, mobilePhoneNumber);
    }
}