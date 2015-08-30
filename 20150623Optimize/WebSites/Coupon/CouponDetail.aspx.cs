﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.WebPageDll;

public partial class Coupon_CouponDetail : System.Web.UI.Page
{
    public int CouponId
    {
        get
        {
            return int.Parse(ViewState["CouponId"].ToString());
        }
        set
        {
            ViewState["CouponId"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["CouponId"]))
            {
                int couponId = int.Parse(Request.QueryString["CouponId"]);
                this.CouponId  = couponId;
                var entity = CouponOperate.GetEntityById(couponId);
                if (entity != null)
                {
                    this.LabelName.Text = entity.CouponName;
                    this.LabelCreateTime.Text = entity.CreateTime.ToString();
                    this.LabelTotalCount.Text = entity.SheetNumber.ToString();
                    this.LabelState.Text = entity.State == 0 ? "失效" : "正常";
                    this.LabelShopName.Text = entity.ShopName;
                    this.LabelRemark.Text = entity.Remark;
                    CouponGetDetailQueryObject queryObject = new CouponGetDetailQueryObject();
                    queryObject.CouponId = couponId;
                    long sendCount = CouponGetDetailOperate.GetCountByQuery(queryObject);
                    this.LabelSendCount.Text = sendCount.ToString();
                    //已使用
                    queryObject.State = 2;
                    long usedCount = CouponGetDetailOperate.GetCountByQuery(queryObject);
                    this.LabelUsedCount.Text = usedCount.ToString();
                    //未使用
                    queryObject.State = 1;
                    this.LabelNotUsedCount.Text = (sendCount - usedCount).ToString();
                    var list = CouponGetDetailOperate.GetListByCouponId(couponId);
                    if (list != null)
                    {
                        double? amount = list.Where(p=>p.UseTime.HasValue).Sum(p => p.PreOrderSum);
                        if (amount.HasValue)
                        {
                            this.LabelAmount.Text = amount.Value.ToString("0.00");
                        }
                    }
                    LabelDesc.Text = String.Format("满{0}减{1}", entity.RequirementMoney, entity.DeductibleAmount);
                    if (entity.AuditEmployee.HasValue)
                    {
                        EmployeeOperate employeeOperate = new EmployeeOperate();
                        var employee = Common.ToInt32(entity.AuditEmployee) == 0 ? null : employeeOperate.QueryEmployee(Common.ToInt32(entity.AuditEmployee));
                        LabelEmployee.Text = employee == null ? "" : employee.EmployeeFirstName;
                    }
                    LabelRecord.Text = Common.ToString(entity.RefuseReason);
                    if (entity.CouponType == 2)
                    {
                        this.LabelCouponType.Text = "运营数据";
                        this.ButtonEdit.Visible = true;
                    }
                    else
                    {
                        this.LabelCouponType.Text = "活动数据";
                        this.ButtonEdit.Visible = false;
                    }
                    LabelMaxAmount.Text = Common.ToDouble(entity.MaxAmount).ToString();
                    //if (entity.IsGeneralHolidays == false)
                    //{
                    //    LabelIsGeneralHolidays.Text = "无限制";
                    //}
                    //else
                    //{
                    //    LabelIsGeneralHolidays.Text = "仅工作日可使用";
                    //}

                    //if (entity.BeginTime == TimeSpan.Parse("00:00:00") && entity.EndTime == TimeSpan.Parse("23:59:59"))
                    //{
                    //    LabelTime.Text = "可全天使用";
                    //}
                    //else
                    //{
                    //    LabelTime.Text = entity.BeginTime + "~" + entity.EndTime;
                    //}
                }
            }
        }
    } 

    protected void ButtonEdit_Click(object sender, EventArgs e)
    {
        Response.Redirect("ForgeCouponAdd.aspx?CouponId=" + this.CouponId);
    }
}