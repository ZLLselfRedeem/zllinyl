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

public partial class Coupon_CouponAdd : System.Web.UI.Page
{
    #region 属性
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
    #endregion

    #region 方法
    private void DoSearch()
    {
        var list =
           ShopOperate.GetListByQuery(12, 1, new ShopInfoQueryObject() { ShopNameFuzzy = this.TextBoxShopName.Text, ShopStatus = 1, IsHandle = 1 });
        this.RadioButtonListShop.DataSource = list;
        if (list == null|| list.Count == 0)
        {
            CommonPageOperate.AlterMsg(this, "门店不存在,请输入正确的店名!");
            return;
        }
        this.RadioButtonListShop.DataBind();
        this.RadioButtonListShop.SelectedIndex = 0;
        
    }

    private void DoInit()
    {
        this.TextBoxStartDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
        this.TextBoxEndDate.Text = DateTime.Now.AddDays(10).ToString("yyyy-MM-dd");
        this.DoSearch();
    }
    

    #endregion

    #region 事件
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            this.DoInit();
        }
    }
    protected void ButtonSearch_Click(object sender, EventArgs e)
    {
        this.DoSearch();
    }
    protected void ButtonAdd_Click(object sender, EventArgs e)
    {
        if (Common.ToDouble(TextBoxMaxAmount.Text) / Common.ToDouble(TextBoxDeductibleAmount.Text) <= 0 || Common.ToDouble(TextBoxMaxAmount.Text) % Common.ToDouble(TextBoxDeductibleAmount.Text) != 0)
        {
            CommonPageOperate.AlterMsg(this, "最多减金额必须为抵用金额的整数倍！");
            return;
        }
        DateTime startDate =
            DateTime.Parse(Request.Form[this.TextBoxStartDate.ClientID]);
        DateTime endDate =
            DateTime.Parse(Request.Form[this.TextBoxEndDate.ClientID]).AddDays(0.99999);
        var shopOperate = new ShopOperate();
        var shop = shopOperate.QueryShop(int.Parse(this.RadioButtonListShop.SelectedValue));
        var shopCoordinate = shopOperate.QueryShopCoordinate(2, shop.shopID);
        //if (checkAllDay.Checked == false &&(txtBeginTime.Text.Trim()==string.Empty || txtEndTime.Text.Trim()==string.Empty))
        //{
        //    CommonPageOperate.AlterMsg(this, "请选择每日使用时间！");
        //    return;
        //}

        //TimeSpan? beginTime = null;
        //TimeSpan? endTime = null;
        //if (checkAllDay.Checked)
        //{
        //    beginTime = TimeSpan.Parse("00:00:00");
        //    endTime = TimeSpan.Parse("23:59:59");
        //}
        //else
        //{
        //    beginTime = TimeSpan.Parse(txtBeginTime.Text);
        //    endTime = TimeSpan.Parse(txtEndTime.Text);
        //}

        Coupon entity = new Coupon()
        {
            SendCount = 0,
            SheetNumber = int.Parse(this.TextBoxSheetNumber.Text),
            ShopId = int.Parse(this.RadioButtonListShop.SelectedValue),
            SortOrder = 0,
            EndDate = endDate,
            StartDate = startDate,
            State = -1,//待审核
            CouponName = this.TextBoxName.Text,
            DeductibleAmount = Common.ToDouble(this.TextBoxDeductibleAmount.Text),
            RequirementMoney = Common.ToDouble(this.TextBoxRequirementMoney.Text),
            ValidityPeriod = int.Parse(this.TextBoxValidityPeriod.Text),
            Remark = this.TextBoxRemark.Text,
            CreateTime = DateTime.Now,
            LastUpdatedBy = this.UserInfo.employeeID,
            CreatedBy = this.UserInfo.employeeID,
            LastUpdatedTime = DateTime.Now,
            IsGot = false,
            ShopName = shop.shopName,
            ShopAddress = shop.shopAddress,
            CouponType = Common.ToInt32(ddlCouponType.SelectedValue),
            IsDisplay = true,
            Image = shop.publicityPhotoPath ,
            Latitude = shopCoordinate.latitude,
            Longitude = shopCoordinate.longitude,
            CityId = shop.cityID,
            SubsidyAmount = Common.ToDouble(this.TextBoxSubsidyAmount.Text),
            MaxAmount=Common.ToDouble(TextBoxMaxAmount.Text)
            //,
            //BeginTime = beginTime,
            //EndTime=endTime,
            //IsGeneralHolidays=Common.ToBool(ddlIsGeneralHolidays.SelectedValue)
        };
        if (CouponOperate.Add(entity))
        {
            CommonPageOperate.AlterMsg(this, "添加成功！");
            Page.ClientScript.RegisterStartupScript(GetType(),
           "message", "<script language='javascript' defer>window.location.href = 'CouponManage.aspx';</script>");
        }
        else
        {
            CommonPageOperate.AlterMsg(this,"数据提交失败，请重试！");
        }
    }
    #endregion
    protected void ddlCouponType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Common.ToInt32(ddlCouponType.SelectedValue) == (int)CouponType.Package)
        {
            TextBoxSubsidyAmount.Text = "0.00";
            TextBoxSubsidyAmount.ReadOnly = true;
        }
        else
        {
            TextBoxSubsidyAmount.ReadOnly = false;
        }
    }
}