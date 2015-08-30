using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.WebPageDll;
using VAGastronomistMobileApp.Model; 
using System.Data.SqlClient;
using System.Data;
using Web.Control;


public partial class ShopStatic_ShopStatic : System.Web.UI.Page
{
    private const string _DateTimeFormat = "yyyy-MM-dd";
    private const string _StaticFormat = "{0} - {1}";

    private static DataTable ExportScheme
    {
        get
        {
            DataTable schemeTable = new DataTable();
            DataColumn  dataColumn = new DataColumn("ShopName");
            dataColumn.Caption = "商户名称";
            schemeTable.Columns.Add(dataColumn);
            dataColumn = new DataColumn("PreorderCount");
            dataColumn.Caption = "订单量";
            schemeTable.Columns.Add(dataColumn);
            dataColumn = new DataColumn("MaxPreorderSum");
            dataColumn.Caption = "最高客单价";
            schemeTable.Columns.Add(dataColumn);
            dataColumn = new DataColumn("AveragePreorderSum");
            dataColumn.Caption = "平均客单价";
            schemeTable.Columns.Add(dataColumn);
            dataColumn = new DataColumn("TotalPreorderSum");
            dataColumn.Caption = "流水";
            schemeTable.Columns.Add(dataColumn);
            dataColumn = new DataColumn("CompareRise");
            dataColumn.Caption = "流水环比上升";
            schemeTable.Columns.Add(dataColumn);
            dataColumn = new DataColumn("SaveTime");
            dataColumn.Caption = "节省时间(分钟)";
            schemeTable.Columns.Add(dataColumn);
            dataColumn = new DataColumn("OldCustomerCount");
            dataColumn.Caption = "老客户数";
            schemeTable.Columns.Add(dataColumn);
            dataColumn = new DataColumn("TotalCustomerCount");
            dataColumn.Caption = "所有客户数";
            schemeTable.Columns.Add(dataColumn);
            dataColumn = new DataColumn("OldCustomerPercent");
            dataColumn.Caption = "老客户比例";
            schemeTable.Columns.Add(dataColumn);
            dataColumn = new DataColumn("TotalEvaluationCount");
            dataColumn.Caption = "总评分个数";
            schemeTable.Columns.Add(dataColumn);
            dataColumn = new DataColumn("GoodEvaluationCount");
            dataColumn.Caption = "好评数";
            schemeTable.Columns.Add(dataColumn);
            dataColumn = new DataColumn("OldCustomerGoodEvaluationPercent");
            dataColumn.Caption = "老客户满意度";
            schemeTable.Columns.Add(dataColumn);
            dataColumn = new DataColumn("Ranking");
            dataColumn.Caption = "排名";
            schemeTable.Columns.Add(dataColumn);
            dataColumn = new DataColumn("RefoundSum");
            dataColumn.Caption = "累计退款金额";
            schemeTable.Columns.Add(dataColumn);
            dataColumn = new DataColumn("DeductibleAmountSum");
            dataColumn.Caption = "抵扣券使用合计";
            schemeTable.Columns.Add(dataColumn);
            dataColumn = new DataColumn("RedEnvelopeSum");
            dataColumn.Caption = "红包支付金额";
            schemeTable.Columns.Add(dataColumn);
            dataColumn = new DataColumn("AccountManager");
            dataColumn.Caption = "服务经理";
            schemeTable.Columns.Add(dataColumn);
            dataColumn = new DataColumn("AreaManager");
            dataColumn.Caption = "区域经理";
            schemeTable.Columns.Add(dataColumn);
            return schemeTable;
        }
    }

    #region 方法
    /// <summary>
    /// 
    /// </summary>
    /// <param name="staticStart"></param>
    /// <param name="isAddPreStatic">是否生成上一统计周期数据</param>
    private void DoSave(DateTime staticStart, bool isAddPreStatic, int cityId)
    { 
        //上一周统计数据
        var preShopStaticList =
            ShopStaticsReportOperate.GetListByQuery(new ShopStaticsReportQueryObject() { StaticsStart = staticStart.AddDays(-7), ReportType = 1,CityId = cityId });
        if (isAddPreStatic == true && (preShopStaticList == null || preShopStaticList.Count == 0))
        {
            DoSave(staticStart.AddDays(-7), false, cityId);
        }
        DateTime staticEnd = staticStart.AddDays(7);
        var preOrder19dianVList =
            PreOrder19dianVOperate.GetListByQuery(new PreOrder19dianVQueryObject() { PrePayTimeFrom = staticStart, PrePayTimeTo = staticEnd, IsPaid = 1, CityId = cityId });
        preOrder19dianVList = preOrder19dianVList.Where(p => p.PreOrderSum > 0).ToList();
        if (preOrder19dianVList != null && preOrder19dianVList.Count > 0)
        {
            RedEnvelopeConnPreOrderOperate redEnvelopeConnPreOrderOperate = new RedEnvelopeConnPreOrderOperate(); 
            var couponGetDetailList =
            CouponGetDetailOperate.GetListByQuery(new CouponGetDetailQueryObject() { State = 2, UseTimeFrom = staticStart, UseTimeTo = staticStart.AddDays(7) });
            var joinList = from preOrder19dianV in preOrder19dianVList
                           join couponGetDetail in couponGetDetailList
                           on preOrder19dianV.PreOrder19dianId equals couponGetDetail.PreOrder19DianId into temp
                           from t in temp.DefaultIfEmpty()
                           where preOrder19dianV.ShopId.HasValue
                           select new { preOrder19dianV, couponGetDetail = t};
            var list = (from e in joinList
                        group e by e.preOrder19dianV.ShopId into g
                        select new ShopStaticsReport
                        {
                            CityId = cityId,
                            ShopId = g.Key.Value,
                            TotalPreorderSum = g.Sum(p => p.preOrder19dianV.PrePaidSum).Value,
                            TotalCustomerCount = g.GroupBy(p => p.preOrder19dianV.CustomerId).Count(),
                            PreorderCount = g.Count(),
                            CompanyId = g.First().preOrder19dianV.CompanyId.Value,
                            ShopName = g.First().preOrder19dianV.ShopName,
                            ReportType = 1,
                            AveragePreorderSum = Math.Round(  g.Average(p => p.preOrder19dianV.PrePaidSum).Value,2),
                            GoodEvaluationCount = g.Count(p => p.preOrder19dianV.EvaluationLevel >= 0),
                            MaxPreorderSum = g.Max(p => p.preOrder19dianV.PrePaidSum).Value,
                            StaticsEnd = staticEnd,
                            StaticsStart = staticStart,
                            TotalEvaluationCount = g.Count(p => p.preOrder19dianV.IsEvaluation == 1),
                            CreateTime = DateTime.Now,
                            OldCustomerCount = 0,
                            OldCustomerGoodEvaluationCount = 0,
                            DeductibleAmountSum =  Math.Round(   g.Where(p=>p.couponGetDetail != null).Sum(p=>p.couponGetDetail.DeductibleAmount),2),
                            RefoundSum = Math.Round( g.Sum(p => p.preOrder19dianV.RefundMoneySum).Value,2) ,
                            RedEnvelopeSum = 
                                Math.Round(  g.Sum(p=>redEnvelopeConnPreOrderOperate.GetPayOrderConsumeRedEnvelopeAmount(p.preOrder19dianV.PreOrder19dianId)),2),
                            OldCustomerEvaluationCount = 0
                        }).AsParallel().ToList();
            //计算老客户
            var preOrder19dianVQueryObject = new PreOrder19dianVQueryObject();
            string ss = string.Empty;
            foreach (var entity in preOrder19dianVList.GroupBy(p => new { ShopId = p.ShopId, CustomerId = p.CustomerId }))
            {
                preOrder19dianVQueryObject.CustomerId = entity.Key.CustomerId.Value;
                preOrder19dianVQueryObject.ShopId = entity.Key.ShopId.Value;
                preOrder19dianVQueryObject.PrePayTimeTo = staticStart;
                if (PreOrder19dianVOperate.GetCountByQuery(preOrder19dianVQueryObject) > 0)
                {
                    var shopStaticsReport = list.FirstOrDefault(p => p.ShopId == entity.Key.ShopId);
                    if (shopStaticsReport != null)
                    {
                        shopStaticsReport.OldCustomerCount = shopStaticsReport.OldCustomerCount + 1;
                        shopStaticsReport.OldCustomerEvaluationCount = shopStaticsReport.OldCustomerEvaluationCount + entity.Count(p => p.IsEvaluation == 1);
                        shopStaticsReport.OldCustomerGoodEvaluationCount =
                            shopStaticsReport.OldCustomerGoodEvaluationCount + entity.Count(p => p.EvaluationLevel == 1 || p.EvaluationLevel == 0);
                    }
                }
            }
            //计算排名
            int order = 0;
            CompanyOperate companyOperate = new CompanyOperate();
            CompanyInfo companyInfo = null;
            ShopOperate shopOperate = new ShopOperate();
            ShopInfo shopInfo = null;
            foreach (var entity in list.OrderByDescending(p => p.TotalPreorderSum))
            {
                order++;
                entity.Ranking = order;
                companyInfo = companyOperate.QueryCompany(entity.CompanyId);
                if (companyInfo != null)
                {
                    entity.CompanyName = companyInfo.companyName;
                }
                shopInfo = shopOperate.QueryShop(entity.ShopId);
                if (shopInfo != null && shopInfo.accountManager.HasValue)
                {
                    entity.AccountManager = shopInfo.accountManager.Value;
                }
                entity.SaveTime = entity.PreorderCount * 15;
                var preShopStatic = preShopStaticList.FirstOrDefault(p => p.ShopId == entity.ShopId);
                if (preShopStatic == null || preShopStatic.TotalPreorderSum == 0)
                {
                    entity.CompareRise = null;
                }
                else
                {
                    entity.CompareRise = (entity.TotalPreorderSum - preShopStatic.TotalPreorderSum) / preShopStatic.TotalPreorderSum;
                }
            }
            if (ShopStaticsReportOperate.GetCountByQuery(new ShopStaticsReportQueryObject() { StaticsStart = staticStart, ReportType = 1, CityId = cityId }) == 0)
            {
                ShopStaticsReportOperate.AddList(list);
            }
        }
    }

    private void DoQuery()
    {

        var queryObject = new ShopStaticsReportQueryObject();
        queryObject.StaticsStart = DateTime.Parse(this.DropDownListStatic.SelectedValue);
        queryObject.ReportType = 1;
        queryObject.CityId = int.Parse(this.DropDownListCity.SelectedValue);
        queryObject.ShopNameFuzzy = this.TextBoxShopName.Text;
        queryObject.CompanyNameFuzzy = this.TextBoxCompanyName.Text;
        this.AspNetPager1.RecordCount = (int)ShopStaticsReportOperate.GetCountByQuery(queryObject);
        var list = ShopStaticsReportOperate.GetListByQuery(this.AspNetPager1.PageSize, this.AspNetPager1.CurrentPageIndex, queryObject,
                                                            ShopStaticsReportOrderColumn.Ranking, SortOrder.Ascending);
        this.GridViewShopReport.DataSource = list;
        this.GridViewShopReport.DataBind();
    }

    private DataTable DoExport()
    {
        var queryObject = new ShopStaticsReportQueryObject();
        queryObject.StaticsStart = DateTime.Parse(this.DropDownListStatic.SelectedValue);
        queryObject.ReportType = 1;
        queryObject.ShopNameFuzzy = this.TextBoxShopName.Text;
        queryObject.CompanyNameFuzzy = this.TextBoxCompanyName.Text;
        queryObject.CityId = int.Parse(this.DropDownListCity.SelectedValue);
        List<ShopStaticsReport> list = ShopStaticsReportOperate.GetListByQuery(queryObject, ShopStaticsReportOrderColumn.Ranking, SortOrder.Ascending);
        var table = ExportScheme.Clone();
        DataRow dataRow = null;
        ShopOperate shopOperate = new ShopOperate();
        ShopInfo shopInfo = null;
        EmployeeOperate employeeOperate = new EmployeeOperate();
        EmployeeInfo employeeInfo = null;
        var shopStaticsReportQueryObject = new ShopStaticsReportQueryObject();
        foreach (var entity in list)
        {
            dataRow = table.NewRow();
            dataRow["ShopName"] = entity.ShopName;
            dataRow["PreorderCount"] = entity.PreorderCount;
            dataRow["MaxPreorderSum"] = entity.MaxPreorderSum;
            dataRow["AveragePreorderSum"] = Math.Round(entity.AveragePreorderSum, 2);
            dataRow["TotalPreorderSum"] = entity.TotalPreorderSum;
            if (entity.CompareRise.HasValue)
            {
                dataRow["CompareRise"] = (entity.CompareRise.Value * 100).ToString("N2") + "%";
            }
            else
            {
                shopStaticsReportQueryObject.StaticsStart = entity.StaticsStart.AddDays(-7);
                shopStaticsReportQueryObject.ShopId = entity.ShopId;
                shopStaticsReportQueryObject.ReportType = entity.ReportType;
                var perShopStatics = ShopStaticsReportOperate.GetFirstByQuery(shopStaticsReportQueryObject);
                if (perShopStatics != null && perShopStatics.TotalPreorderSum > 0)
                {
                    double compareRise = Math.Round((entity.TotalPreorderSum - perShopStatics.TotalPreorderSum) / perShopStatics.TotalPreorderSum, 4);
                    entity.CompareRise = compareRise;
                    dataRow["CompareRise"] = (compareRise * 100).ToString("N2") + "%";
                    ShopStaticsReportOperate.Update(entity);
                }
                else
                {
                    dataRow["CompareRise"] = "-";
                }
            }
            dataRow["SaveTime"] = entity.SaveTime;
            dataRow["OldCustomerCount"] = entity.OldCustomerCount;
            dataRow["TotalCustomerCount"] = entity.TotalCustomerCount;
            dataRow["OldCustomerPercent"] =
                entity.TotalCustomerCount > 0 ?(entity.OldCustomerCount * 100 / entity.TotalCustomerCount).ToString("N2") + "%" : "-";
            dataRow["TotalEvaluationCount"] = entity.TotalEvaluationCount;
            dataRow["GoodEvaluationCount"] = entity.GoodEvaluationCount;
            dataRow["OldCustomerGoodEvaluationPercent"]
                = entity.OldCustomerEvaluationCount > 0 ? (entity.OldCustomerGoodEvaluationCount * 100 / entity.OldCustomerEvaluationCount).ToString("N2") + "%" : "-";
            dataRow["Ranking"] = entity.Ranking;
            shopInfo = shopOperate.QueryShop(entity.ShopId);
            if (shopInfo != null && shopInfo.accountManager.HasValue)
            {
                employeeInfo = employeeOperate.QueryEmployee(shopInfo.accountManager.Value);
                if (employeeInfo != null)
                {
                    dataRow["AccountManager"] = employeeInfo.EmployeeFirstName + "(" + employeeInfo.EmployeePhone + ")";
                }
                else
                {
                    dataRow["AccountManager"] = "-";
                }
            }
            else
            {
                dataRow["AccountManager"] = "-";
            } 

            if (shopInfo != null && shopInfo.AreaManager.HasValue)
            {
                var areaManager = employeeOperate.QueryEmployee(shopInfo.AreaManager.Value);
                if (employeeInfo != null)
                {
                    dataRow["AreaManager"] = areaManager.EmployeeFirstName + "(" + areaManager.EmployeePhone + ")";
                }
                else
                {
                    dataRow["AreaManager"] = "-";
                }
            }
            else
            {
                dataRow["AreaManager"] = "-";
            }

            dataRow["RefoundSum"] = entity.RefoundSum.HasValue ? Math.Round(entity.RefoundSum.Value, 2) : 0;
            dataRow["DeductibleAmountSum"] = entity.DeductibleAmountSum;
            dataRow["RedEnvelopeSum"] = entity.RedEnvelopeSum;
            table.Rows.Add(dataRow);
        }
        return table;
    }
    private void DoInit()
    {
        if (!this.IsPostBack)
        {
            DateTime staticStart;
            int cityId = int.Parse(this.DropDownListCity.SelectedValue);
            if (DateTime.Today.DayOfWeek == DayOfWeek.Sunday)
            {
                 staticStart = DateTime.Today.AddDays(-13);
            }
            else
            {
                staticStart = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek).AddDays(-6);
            }
            //如本周统计数据不存在,则需新增
            if (ShopStaticsReportOperate.GetCountByQuery(new ShopStaticsReportQueryObject() { StaticsStart = staticStart, ReportType = 1,CityId = cityId }) == 0)
            {
                DoSave(staticStart, true, cityId);
            }
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
    #endregion

    #region 事件
    protected void Page_Load(object sender, EventArgs e)
    {
        DoInit();
    }


    protected void ButtonQuery_Click(object sender, EventArgs e)
    { 
        DateTime staticStart = DateTime.Parse(this.DropDownListStatic.SelectedValue);
        int cityId = int.Parse(this.DropDownListCity.SelectedValue);
        //如本周统计数据不存在,则需新增
        if (ShopStaticsReportOperate.GetCountByQuery(new ShopStaticsReportQueryObject() { StaticsStart = staticStart, ReportType = 1, CityId = cityId }) == 0)
        {
            DoSave(staticStart, true, cityId);
        }
        //如上周统计数据不存在,则需新增
        if (ShopStaticsReportOperate.GetCountByQuery(new ShopStaticsReportQueryObject() { StaticsStart = staticStart.AddDays(-7), ReportType = 1, CityId = cityId }) == 0)
        {
            DoSave(staticStart.AddDays(-7), false, cityId);
        }
        this.AspNetPager1.CurrentPageIndex = 1;
        DoQuery();
    }

    protected void GridViewShopReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        var entity = e.Row.DataItem as ShopStaticsReport;
        if (entity != null)
        {
            Label LabelOldCustomerPercent = e.Row.FindControl("LabelOldCustomerPercent") as Label;
            if (LabelOldCustomerPercent != null && entity.TotalCustomerCount > 0)
            {
                LabelOldCustomerPercent.Text = (entity.OldCustomerCount * 100 / entity.TotalCustomerCount).ToString("N2") + "%";
            }
            Label LabelOldCustomerGoodEvaluationPercent = e.Row.FindControl("LabelOldCustomerGoodEvaluationPercent") as Label;
            if (LabelOldCustomerGoodEvaluationPercent != null)
            {
                if (entity.OldCustomerEvaluationCount > 0)
                {
                    LabelOldCustomerGoodEvaluationPercent.Text = (entity.OldCustomerGoodEvaluationCount * 100 / entity.OldCustomerEvaluationCount).ToString("N2") + "%";
                } 
            }
            Label LabelCompareRise = e.Row.FindControl("LabelCompareRise") as Label;
            if (LabelCompareRise != null)
            {
                if (entity.CompareRise.HasValue)
                {
                    LabelCompareRise.Text = (entity.CompareRise.Value*100).ToString("N2") + "%";
                }
                else
                {
                    var queryObject = new ShopStaticsReportQueryObject() { StaticsStart = entity.StaticsStart.AddDays(-7), ShopId = entity.ShopId, ReportType = entity.ReportType };
                    var perShopStatics = ShopStaticsReportOperate.GetFirstByQuery(queryObject);
                    if (perShopStatics != null && perShopStatics.TotalPreorderSum > 0)
                    {
                        double compareRise = Math.Round((entity.TotalPreorderSum - perShopStatics.TotalPreorderSum) / perShopStatics.TotalPreorderSum, 4);
                        entity.CompareRise = compareRise;
                        LabelCompareRise.Text = (compareRise * 100).ToString("N2") + "%";
                        ShopStaticsReportOperate.Update(entity);
                    }
                }
            }

            ShopOperate shopOperate = new ShopOperate();
            EmployeeOperate employeeOperate = new EmployeeOperate();
            var shop = shopOperate.QueryShop(entity.ShopId);
            Label LabelAccountManager = e.Row.FindControl("LabelAccountManager") as Label;
            if (LabelAccountManager != null)
            {
                if (shop != null && shop.accountManager.HasValue)
                {
                    var employee = employeeOperate.QueryEmployee(shop.accountManager.Value);
                    if (employee != null)
                    {
                        LabelAccountManager.Text = employee.EmployeeFirstName + "(" + employee.EmployeePhone + ")";
                    }
                    else
                    {
                        LabelAccountManager.Text = "-";
                    }
                }
            }

            Label LabelAreaManager = e.Row.FindControl("LabelAreaManager") as Label;
            if (LabelAreaManager != null)
            {
                if (shop != null && shop.AreaManager.HasValue)
                {
                    var areaManager = employeeOperate.QueryEmployee(shop.AreaManager.Value);
                    if (areaManager != null)
                    {
                        LabelAreaManager.Text = areaManager.EmployeeFirstName + "(" + areaManager.EmployeePhone + ")";
                    }
                    else
                    {
                        LabelAreaManager.Text = "-";
                    }
                }
            } 

        }
    }
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        this.DoQuery();
    } 
    protected void ButtonExport_Click(object sender, EventArgs e)
    {
        DateTime staticStart = DateTime.Parse(this.DropDownListStatic.SelectedValue);
        int cityId = int.Parse(this.DropDownListCity.SelectedValue);
        //如本周统计数据不存在,则需新增
        if (ShopStaticsReportOperate.GetCountByQuery(new ShopStaticsReportQueryObject() { StaticsStart = staticStart, ReportType = 1,CityId = cityId }) == 0)
        {
            DoSave(staticStart, true, cityId);
        }
        var exportTable = DoExport();
        ExcelHelper.ExportExcel(exportTable, this, "ShopSaleReport_" + this.DropDownListStatic.SelectedItem.Text);
    }



     #endregion
}