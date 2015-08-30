using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.Model.QueryObject;
using VAGastronomistMobileApp.WebPageDll;
using Web.Control;

public partial class Coupon_CouponFinancialStatistics : System.Web.UI.Page
{
    public DataTable ExportDataFormat
    {
        get
        {
            DataTable dt = new DataTable();
            var column = new DataColumn("City");
            column.Caption = "城市";
            dt.Columns.Add(column);
            column = new DataColumn("ShopName");
            column.Caption = "门店名";
            dt.Columns.Add(column);
            column = new DataColumn("BankName");
            column.Caption = "开户行";
            dt.Columns.Add(column);
            column = new DataColumn("AccountName");
            column.Caption = "开户名";
            dt.Columns.Add(column);
            column = new DataColumn("Account");
            column.Caption = "帐号";
            dt.Columns.Add(column);
            column = new DataColumn("AccountManager");
            column.Caption = "客户经理";
            dt.Columns.Add(column);
            column = new DataColumn("Content");
            column.Caption = "活动内容";
            dt.Columns.Add(column);
            column = new DataColumn("GetCount");
            column.Caption = "领取数";
            dt.Columns.Add(column);
            column = new DataColumn("UseCount");
            column.Caption = "使用数";
            dt.Columns.Add(column);
            column = new DataColumn("SubsidyAmount");
            column.Caption = "单笔补贴";
            dt.Columns.Add(column);
            column = new DataColumn("TotalSubsidyAmount");
            column.Caption = "补贴总额";
            dt.Columns.Add(column);
            column = new DataColumn("ActiveTime");
            column.Caption = "活动时间";
            dt.Columns.Add(column);
            return dt;
        }
    }

    private void DoQuery()
    {
        var queryObject = new CouponQueryObject()
        {
            ShopNameFuzzy = this.TextBoxName.Text,
            CouponType = 1
        };
        if (string.IsNullOrEmpty(this.DropDownListCity.SelectedValue) == false)
        {
            queryObject.CityId = int.Parse(this.DropDownListCity.SelectedValue);
        }
        var couponCount = CouponOperate.GetCountByQuery(queryObject);
        this.AspNetPager1.RecordCount = (int)couponCount;
        var coupons = CouponOperate.GetListByQuery(this.AspNetPager1.PageSize, this.AspNetPager1.CurrentPageIndex, queryObject);
        this.GridViewCoupon.DataSource = coupons;
        this.GridViewCoupon.DataBind();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            this.TextBoxUseTimeFrom.Text = DateTime.Now.AddDays(-11).ToString("yyyy-MM-dd");
            this.TextBoxUseTimeTo.Text = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
            this.TextBoxUseTimeFrom.Attributes.Add("onfocus",
                  "WdatePicker({isShowClear:false,readOnly:true,dateFmt:'yyyy-MM-dd',maxDate:'" + DateTime.Now.AddDays(-1).ToShortDateString() + "'})");
            this.TextBoxUseTimeTo.Attributes.Add("onfocus",
                "WdatePicker({isShowClear:false,readOnly:true,dateFmt:'yyyy-MM-dd',maxDate:'" + DateTime.Now.AddDays(-1).ToShortDateString() + "'})");
            ButtonSearch_Click(null, null);
        }
    }
    protected void ButtonSearch_Click(object sender, EventArgs e)
    {
        DoQuery();

        //统计信息
        DateTime? staticsTimeForm = null;
        DateTime? staticsTimeTo = null;
        int? cityId = null;
        if (string.IsNullOrEmpty(this.TextBoxUseTimeFrom.Text) == false)
        {
            staticsTimeForm = DateTime.Parse(this.TextBoxUseTimeFrom.Text);
        }
        if (string.IsNullOrEmpty(this.TextBoxUseTimeTo.Text) == false)
        {
            staticsTimeTo = DateTime.Parse(this.TextBoxUseTimeTo.Text).AddDays(0.9999);
        }
        if (string.IsNullOrEmpty(this.DropDownListCity.SelectedValue) == false)
        {
            cityId = int.Parse(this.DropDownListCity.SelectedValue);
        }

        var couponGetQueryObject = new CouponGetDetailVQueryObject()
        {
            GetTimeFrom = staticsTimeForm,
            GetTimeTo = staticsTimeTo,
            ShopNameFuzzy = this.TextBoxName.Text,
            CityId = cityId
        };
        this.LabelGetCount.Text = CouponGetDetailVOperate.GetCountByQuery(couponGetQueryObject).ToString();
        couponGetQueryObject = new CouponGetDetailVQueryObject()
        {
            ReconciliationTimeFrom = staticsTimeForm,
            ReconciliationTimeTo = staticsTimeTo,
            ShopNameFuzzy = this.TextBoxName.Text,
            State = 2,
            CityId = cityId,
            IsApproved = 1
        };
        var couponGets = CouponGetDetailVOperate.GetListByQuery(couponGetQueryObject);
        this.LabelUseCount.Text = couponGets.Count().ToString();
        this.LabelShopCount.Text = couponGets.GroupBy(p => p.ShopId).Where(p => p.Key.HasValue).Count().ToString();
        var couponGroups = couponGets.GroupBy(p => p.CouponId);
        double subsidyAmount = 0;
        foreach (var group in couponGroups)
        {
            var coupon = CouponOperate.GetEntityById(group.Key);
            subsidyAmount = subsidyAmount + coupon.SubsidyAmount * group.Count();
        }
        this.LabelTotalSubsidyAmount.Text = subsidyAmount.ToString("C");
    }


    protected void GridViewCoupon_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        var entity = e.Row.DataItem as Coupon;
        if (entity == null || entity.ShopId.HasValue == false)
        {
            return;
        }
        var shopOperate = new ShopOperate();
        var companyAccountOprate = new CompanyAccountOprate();

        var shopInfo = shopOperate.QueryShop(entity.ShopId.Value);
        if (shopInfo == null)
        {
            return;
        }
        var LabelCity = e.Row.FindControl("LabelCity") as Label;
        if (LabelCity != null)
        {
            var city = CityOperate.GetCityByCityId(shopInfo.cityID);
            LabelCity.Text = city.cityName;
        }

        var LabelBankName = e.Row.FindControl("LabelBankName") as Label;
        var LabelAccountName = e.Row.FindControl("LabelAccountName") as Label;
        var LabelAccount = e.Row.FindControl("LabelAccount") as Label;
        if (LabelBankName != null && LabelAccountName != null && LabelAccount != null && shopInfo.bankAccount.HasValue)
        {
            var table = companyAccountOprate.QueryAccountById(shopInfo.bankAccount.Value);
            if (table != null && table.Rows.Count > 0)
            {
                LabelBankName.Text = table.Rows[0]["bankName"].ToString();
                LabelAccountName.Text = table.Rows[0]["accountName"].ToString();
                LabelAccount.Text = table.Rows[0]["accountNum"].ToString();
            }
        }

        var LabelAccountManager = e.Row.FindControl("LabelAccountManager") as Label;
        if (LabelAccountManager != null && shopInfo.accountManager.HasValue)
        {
            var employee = new EmployeeOperate().QueryEmployee(shopInfo.accountManager.Value);
            if (employee != null && employee.EmployeeFirstName != null)
            {
                LabelAccountManager.Text = employee.EmployeeFirstName;
            }
        }

        DateTime? staticsTimeForm = null;
        DateTime? staticsTimeTo = null;

        if (string.IsNullOrEmpty(this.TextBoxUseTimeFrom.Text) == false)
        {
            staticsTimeForm = DateTime.Parse(this.TextBoxUseTimeFrom.Text);
        }
        if (string.IsNullOrEmpty(this.TextBoxUseTimeTo.Text) == false)
        {
            staticsTimeTo = DateTime.Parse(this.TextBoxUseTimeTo.Text).AddDays(1).AddTicks(-1);
        }

        var LabelGetCount = e.Row.FindControl("LabelGetCount") as Label;
        if (LabelGetCount != null)
        {
            var couponGetQueryObject = new CouponGetDetailVQueryObject()
            {
                GetTimeFrom = staticsTimeForm,
                GetTimeTo = staticsTimeTo,
                CouponId = entity.CouponId
            };
            LabelGetCount.Text = CouponGetDetailVOperate.GetCountByQuery(couponGetQueryObject).ToString();
        }

        var LabelUseCount = e.Row.FindControl("LabelUseCount") as Label;
        var LabelTotalSubsidyAmount = e.Row.FindControl("LabelTotalSubsidyAmount") as Label;
        var useCouponGetQueryObject = new CouponGetDetailVQueryObject()
        {
            ConfirmTimeFrom = staticsTimeForm,
            ConfirmTimeTo = staticsTimeTo,
            CouponId = entity.CouponId,
            State = 2,
            IsApproved = 1
        };
        var coupons = CouponGetDetailVOperate.GetListByQuery(useCouponGetQueryObject);
        int overlyingCount = CoumputeCouponOverlyingCount(coupons);
        int useCount = 0;
        int overfyingCount = CoumputeCouponOverlyingCount(coupons);
        if (LabelUseCount != null && LabelTotalSubsidyAmount != null)
        {
            useCount = coupons.Count();
            LabelUseCount.Text = useCount.ToString();
            LabelTotalSubsidyAmount.Text = (entity.SubsidyAmount * overlyingCount).ToString("C");
        }
        var LabelSubsidyType = e.Row.FindControl("LabelSubsidyType") as Label;
        var LabelOverlyingCount = e.Row.FindControl("LabelOverlyingCount") as Label;
        if (LabelOverlyingCount != null)
        {
            LabelOverlyingCount.Text = overfyingCount.ToString();
        }
        if (LabelSubsidyType != null)
        {
            if (entity.SubsidyType == 1)
            {
                LabelSubsidyType.Text = "按订单补贴";
                LabelTotalSubsidyAmount.Text = (entity.SubsidyAmount * useCount).ToString("C");
            }
            else
            {
                LabelTotalSubsidyAmount.Text = (entity.SubsidyAmount * overfyingCount).ToString("C");
                LabelSubsidyType.Text = "按叠加次数补贴";
            }
        }
    }


    protected void ButtonExportExcel_Click(object sender, EventArgs e)
    {

        var queryObject = new CouponQueryObject()
        {
            ShopNameFuzzy = this.TextBoxName.Text,
            CouponType = 1
        };
        if (string.IsNullOrEmpty(this.DropDownListCity.SelectedValue) == false)
        {
            queryObject.CityId = int.Parse(this.DropDownListCity.SelectedValue);
        }

        var coupons = CouponOperate.GetListByQuery(queryObject);
        var shopOperate = new ShopOperate();
        var companyAccountOprate = new CompanyAccountOprate();
        var employeeOperate = new EmployeeOperate();
        var dt = this.ExportDataFormat.Clone();
        DataRow dr = null;
        foreach (var entity in coupons)
        {

            var shopInfo = shopOperate.QueryShop(entity.ShopId.Value);
            if (shopInfo == null || shopInfo.shopID == 0)
            {
                continue;
            }
            dr = dt.NewRow();
            dr["ShopName"] = shopInfo.shopName;
            dr["Content"] = string.Format("满{0}减{1}", entity.RequirementMoney, entity.DeductibleAmount);
            dr["SubsidyAmount"] = entity.SubsidyAmount.ToString();
            dr["ActiveTime"] = string.Format("{0:D}-{1:D}", entity.StartDate, entity.EndDate);
            var city = CityOperate.GetCityByCityId(shopInfo.cityID);
            dr["City"] = city.cityName;

            dr["BankName"] = string.Empty;
            dr["AccountName"] = string.Empty;
            dr["Account"] = string.Empty;
            if (shopInfo.bankAccount.HasValue)
            {
                var table = companyAccountOprate.QueryAccountById(shopInfo.bankAccount.Value);
                if (table != null && table.Rows.Count > 0)
                {
                    dr["BankName"] = table.Rows[0]["bankName"].ToString();
                    dr["AccountName"] =
                        table.Rows[0]["accountName"] == DBNull.Value ? string.Empty : table.Rows[0]["accountName"].ToString();
                    dr["Account"] = "'" + table.Rows[0]["accountNum"].ToString();
                }
            }
            dr["AccountManager"] = string.Empty;
            if (shopInfo.accountManager.HasValue)
            {
                var employee = employeeOperate.QueryEmployee(shopInfo.accountManager.Value);
                if (employee != null)
                {
                    dr["AccountManager"] = employee.EmployeeFirstName == null ? string.Empty : employee.EmployeeFirstName;
                }
            }
            DateTime? staticsTimeForm = null;
            DateTime? staticsTimeTo = null;

            if (string.IsNullOrEmpty(this.TextBoxUseTimeFrom.Text) == false)
            {
                staticsTimeForm = DateTime.Parse(this.TextBoxUseTimeFrom.Text);
            }
            if (string.IsNullOrEmpty(this.TextBoxUseTimeTo.Text) == false)
            {
                staticsTimeTo = DateTime.Parse(this.TextBoxUseTimeTo.Text).AddDays(0.99999);
            }


            var couponGetQueryObject = new CouponGetDetailVQueryObject()
            {
                GetTimeFrom = staticsTimeForm,
                GetTimeTo = staticsTimeTo,
                CouponId = entity.CouponId
            };
            dr["GetCount"] = CouponGetDetailVOperate.GetCountByQuery(couponGetQueryObject).ToString();
            couponGetQueryObject = new CouponGetDetailVQueryObject()
               {
                   ReconciliationTimeFrom = staticsTimeForm,
                   ReconciliationTimeTo = staticsTimeTo,
                   CouponId = entity.CouponId,
                   State = 2,
                   IsApproved = 1
               };
            long useCount = CouponGetDetailVOperate.GetCountByQuery(couponGetQueryObject);
            dr["UseCount"] = useCount.ToString();
            dr["TotalSubsidyAmount"] = (entity.SubsidyAmount * useCount).ToString();
            dt.Rows.Add(dr);

        }

        ExcelHelper.ExportExcel(dt, this, "Coupon_" + DateTime.Now);

    }

    /// <summary>
    /// 计算抵扣券叠加次数
    /// </summary>
    /// <param name="couponGetDetails"></param>
    /// <returns></returns>
    private static int CoumputeCouponOverlyingCount(List<VAGastronomistMobileApp.Model.Interface.ICouponGetDetailV> couponGetDetails)
    {
        int toatlOverlyingCount = 0;
        PreOrder19dianOperate preOrder19dianOperate = new PreOrder19dianOperate();
        foreach (var couponGetDetail in couponGetDetails)
        {
            if (couponGetDetail.PreOrder19DianId.HasValue && couponGetDetail.PreOrder19DianId > 0)
            {
                var preOrder19dian =
                    preOrder19dianOperate.GetPreOrder19dianById(couponGetDetail.PreOrder19DianId.Value);
                if (preOrder19dian != null)
                {
                    double refundMoneySum = 0;
                    if (preOrder19dian.refundMoneySum.HasValue)
                    {
                        refundMoneySum = preOrder19dian.refundMoneySum.Value;
                    }
                    int overlyingCount =
                        (int)((preOrder19dian.preOrderServerSum.Value + couponGetDetail.RealDeductibleAmount
                                - refundMoneySum) / couponGetDetail.RequirementMoney);
                    if (overlyingCount > (couponGetDetail.RealDeductibleAmount / couponGetDetail.DeductibleAmount))
                    {
                        overlyingCount = (int)(couponGetDetail.RealDeductibleAmount / couponGetDetail.DeductibleAmount);
                    }
                    toatlOverlyingCount = toatlOverlyingCount + overlyingCount;
                }
            }
        }
        return toatlOverlyingCount;
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        this.DoQuery();
    }
}
