using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Autofac;
using Autofac.Integration.Web;
using VAGastronomistMobileApp.WebPageDll.Services;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.WebPageDll.Services.Infrastructure;

public partial class Shared_FoodDiaryDefaultConfigDishManage : System.Web.UI.Page
{
    private IFoodDiaryDefaultConfigDishService foodDiaryDefaultConfigDishService;
    public Shared_FoodDiaryDefaultConfigDishManage()
    {
        foodDiaryDefaultConfigDishService = ServiceFactory.Resolve<IFoodDiaryDefaultConfigDishService>();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Bind();
        }

    }

    private void Bind()
    {
        var list = foodDiaryDefaultConfigDishService.GetAllList().Select(p => new FoodDiaryDefaultConfigDish
        {
            DishName = p.DishName,
            Status = p.Status,
            ImageName = WebConfig.CdnDomain + WebConfig.ImagePath + WebConfig.FoodDiaryImagePath + p.ImageName,
            Id = p.Id
        }).ToList();

        DishView.DataSource = list;
        DishView.DataBind();
    }

    protected void DishView_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "delete":
                int id = Convert.ToInt32(e.CommandArgument);
                foodDiaryDefaultConfigDishService.Delete(id);
                Bind();
                break;
        }

    }

    protected void DishView_DataBound(object sender, EventArgs e)
    {
        int currentPage = datapager1.StartRowIndex / datapager1.PageSize + 1;
        int totalPages = datapager1.TotalRowCount / datapager1.PageSize;
    }

    protected void DishView_OnItemDeleting(object sender, ListViewDeleteEventArgs e)
    {
        
    }
}