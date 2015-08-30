using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.Model.Interface;
using VAGastronomistMobileApp.Model.QueryObject;
using VAGastronomistMobileApp.WebPageDll;

public partial class Coupon_SystemCouponShareManage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            this.TextBoxCreateTimeFrom.Text = DateTime.Now.AddDays(-10).ToString("yyyy-MM-dd");
            this.TextBoxCreateTimeTo.Text = DateTime.Now.ToString("yyyy-MM-dd");
            this.DoQuery();
        }
    }
    protected void ButtonSearch_Click(object sender, EventArgs e)
    {
        DoQuery();
    }

    private void DoQuery()
    {
        var queryObject = GetQueryObject();
        this.AspNetPager1.RecordCount = (int)QrCodeOperate.GetCountByQuery(queryObject);
        var qrCodes = QrCodeOperate.GetListByQuery(this.AspNetPager1.PageSize, this.AspNetPager1.CurrentPageIndex, queryObject);
        this.GridViewQrCode.DataSource = qrCodes;
        this.GridViewQrCode.DataBind();
    }

    private QrCodeQueryObject GetQueryObject()
    {
        var queryObject = new QrCodeQueryObject()
        {
            Type = 1,
            NameFuzzy = this.TextBoxName.Text
        };

        if (!string.IsNullOrEmpty(this.DropDownListCity.SelectedValue))
        {
            queryObject.CityId = int.Parse(this.DropDownListCity.SelectedValue);
        }
        if (!string.IsNullOrEmpty(this.TextBoxCreateTimeFrom.Text))
        {
            queryObject.CreateTimeFrom = DateTime.Parse(this.TextBoxCreateTimeFrom.Text);
        }
        if (!string.IsNullOrEmpty(this.TextBoxCreateTimeTo.Text))
        {
            queryObject.CreateTimeTo = DateTime.Parse(this.TextBoxCreateTimeTo.Text).Date.AddDays(1);
        }
        return queryObject;
    } 

    protected void GridViewCoupon_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        var entity = e.Row.DataItem as QrCode;
        if (entity == null)
        {
            return;
        }
        var LabelCity = e.Row.FindControl("LabelCity") as Label;
        if (LabelCity != null)
        {
            LabelCity.Text = CityOperate.GetCityByCityId(entity.CityId).cityName;
        }

        var couponGetDetailVs = CouponGetDetailVOperate.GetListByQuery(new CouponGetDetailVQueryObject()
        {
            CouponSendDetailID = entity.LinkKey
        });
        var LabelAmount = e.Row.FindControl("LabelAmount") as Label;
        var LabelGetCount = e.Row.FindControl("LabelGetCount") as Label;
        var LabelUseCount = e.Row.FindControl("LabelUseCount") as Label;
        if (LabelAmount != null)
        {
            LabelAmount.Text = couponGetDetailVs.Where(p => p.PreOrderSum.HasValue).Sum(p => p.PreOrderSum).Value.ToString("C2");
        }
        if (LabelGetCount != null)
        {
            LabelGetCount.Text = couponGetDetailVs.Count().ToString();
        }
        if (LabelUseCount != null)
        {
            LabelUseCount.Text = couponGetDetailVs.Count(p => p.State == 2).ToString();
        }

        var LabelNewUserCount = e.Row.FindControl("LabelNewUserCount") as Label;
        if (LabelNewUserCount != null)
        {
            LabelNewUserCount.Text = CouponSendDetailOperate.GetRegisterUserCountViaCoupon((int)entity.LinkKey).ToString();
        }
    }
}