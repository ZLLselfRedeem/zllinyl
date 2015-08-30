using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.WebPageDll;

public partial class Coupon_AddSystemShareCoupon : System.Web.UI.Page
{
    public VAEmployeeLoginResponse UserInfo
    {
        get
        {
            if (Session["UserInfo"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            return Session["UserInfo"] as VAEmployeeLoginResponse;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            this.TextBoxRemark.Text = string.Empty;
        }
    }
    protected void ButtonAdd_Click(object sender, EventArgs e)
    {
        var couponSendDetail = new CouponSendDetail()
        {
            CreateTime = DateTime.Now,
            LastUpdateTime = DateTime.Now,
            PreOrder19DianId = 0,
            ShareType =2,
            SendCount = 0,
            TotalCount = 1000,
            ValidityEnd = DateTime.Now.AddYears(1),
            CreatedBy = UserInfo.employeeID,
            LastUpdatedBy = UserInfo.employeeID
        };
        if (CouponSendDetailOperate.Add(couponSendDetail))
        {
            var qrCode = new QrCode()
            {
                State = 1,
                CityId = int.Parse(this.DropDownListCity.SelectedValue),
                CreatedBy = UserInfo.employeeID,
                LastUpdatedBy = UserInfo.employeeID,
                CreateTime = DateTime.Now,
                LastUpdateTime = DateTime.Now,
                Type = 1,
                LinkKey = couponSendDetail.CouponSendDetailID,
                Name = this.TextBoxName.Text,
                Remark = this.TextBoxRemark.Text
            };
            if (QrCodeOperate.Add(qrCode))
            {
                CommonPageOperate.AlterMsg(this,"添加成功!");
                this.Response.Write(
                    string.Format("<script type='text/javascript' defer>window.location.href=SystemShareCouponDetail.aspx?id={0}</script>",qrCode.Id));
            }

        }
    }
}