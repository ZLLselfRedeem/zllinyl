using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


using VAGastronomistMobileApp.WebPageDll;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.Model.QueryObject;
using VAGastronomistMobileApp.Model.Interface; 

public partial class ShopStatic_ShopInfoStatistics : System.Web.UI.Page
{
    private const string _DateTimeFormat = "yyyy-MM-dd";
    private const string _StaticFormat = "{0} - {1}";

    protected void Page_Load(object sender, EventArgs e)
    {
        this.DoInti();
    }
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        this.DoQuery();
    }
    protected void ButtonQuery_Click(object sender, EventArgs e)
    {
        this.AspNetPager1.CurrentPageIndex = 1;
        this.DoQuery();
    }

    private void DoInti()
    {
        if (!this.IsPostBack)
        {
            DateTime staticStart;
            if (DateTime.Today.DayOfWeek == DayOfWeek.Sunday)
            {
                staticStart = DateTime.Today.AddDays(-13);
            }
            else
            {
                staticStart = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek).AddDays(-6);
            }
            DateTime staticEnd = staticStart.AddDays(7); 
            //统计至2015年2月起始数据
            DateTime queryStart = new DateTime(2015, 1, 26);
            ListItem item = null;
            DateTime tmpStaticStart = staticStart;
            while (tmpStaticStart >= queryStart)
            {
                item =
                    new ListItem(string.Format(_StaticFormat, tmpStaticStart.ToString(_DateTimeFormat), tmpStaticStart.AddDays(6).ToString(_DateTimeFormat)));
                item.Value = tmpStaticStart.ToString(_DateTimeFormat);
                this.DropDownListStatic.Items.Add(item);
                tmpStaticStart = tmpStaticStart.AddDays(-7);
            }
            this.DropDownListStatic.SelectedValue = staticStart.ToString(_DateTimeFormat);

            this.DoQuery();
        }
    }

    private void DoQuery()
    {
        var staticsStart = DateTime.Parse(this.DropDownListStatic.SelectedValue);
        var queryObject = new ShopStaticsQueryObject() { StaticsStart = staticsStart, ReportType = 1 };
        long count = ShopStaticsOperate.GetCountByQuery(queryObject);
        if (count == 0)
        {
            this.DoSave(staticsStart, staticsStart.AddDays(7));
            this.DoQuery();
        }
        else
        {
            this.AspNetPager1.RecordCount = (int)count;
            var list = ShopStaticsOperate.GetListByQuery(this.AspNetPager1.PageSize, this.AspNetPager1.CurrentPageIndex, queryObject);
            this.GridViewShop.DataSource = list;
            this.GridViewShop.DataBind();
        }
    }

    private void DoSave(DateTime staticStart, DateTime staticEnd)
    {
        if (ShopStaticsOperate.GetCountByQuery(new ShopStaticsQueryObject() { StaticsStart = staticStart , ReportType = 1}) > 0)
        {
            return;
        }
        //统计截止时间之前注册的门店
        var list = ShopOperate.GetListByQuery(new ShopInfoQueryObject() { ShopRegisterTimeTo = staticEnd});

        var shopHandleLogList 
            = ShopHandleLogOperate.GetListByQuery(new ShopHandleLogQueryObject() { OperateTimeTo = staticEnd},ShopHandleLogOrderColumn.OperateTime);

        var stopPayLogList 
            = ShopStopPaymentLogOperate.GetListByQuery(new ShopStopPaymentLogQueryObject() { StopPaymentTimeFrom = staticStart, StopPaymentTimeTo = staticEnd, State =1 });
        if (stopPayLogList == null)
        {
            stopPayLogList = new List<IShopStopPaymentLog>();
        }
        var shopInfoStatisticsList = from p in list where p.cityID == 87 ||  p.cityID == 73
                                     group p by p.cityID into g 
                                     select new ShopStatics
                                     {
                                         CityId = g.Key,
                                         TotalCount = g.Count(),
                                         //最后一条数据状态为1(审核通过)则为在线
                                         OnlineCount =
                                            g.Count(h => shopHandleLogList.Exists( p=>p.ShopId == h.shopID) && shopHandleLogList.First(p=>p.ShopId == h.shopID).HandleStatus == 1),
                                         AddedCount = g.Count(h => h.shopRegisterTime.HasValue && h.shopRegisterTime >= staticStart && h.shopRegisterTime <= staticEnd),
                                         StopPaymentCount =
                                            g.Count(h => stopPayLogList.Exists(j => j.ShopId == h.shopID) && stopPayLogList.First(j => j.ShopId == h.shopID).State == 1),
                                         CreateTime = DateTime.Now,
                                         StaticsStart = staticStart,
                                         StaticsEnd = staticEnd,
                                         ReportType = 1
                                     };
        if (shopInfoStatisticsList != null && shopInfoStatisticsList.Count() > 0)
        {
            foreach (var entity in shopInfoStatisticsList)
            {
                ShopStaticsOperate.Add(entity);
            }
        }
    }
    protected void GridViewShop_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        var entity = e.Row.DataItem as ShopStatics;
        if (entity != null)
        {
           var LabelCity =  e.Row.FindControl("LabelCity") as Label;
           if (LabelCity != null)
           {
               var city = CityOperate.GetCityByCityId(entity.CityId);
               if (city != null)
               {
                   LabelCity.Text = city.cityName;
               }
           }
        }
    }
}