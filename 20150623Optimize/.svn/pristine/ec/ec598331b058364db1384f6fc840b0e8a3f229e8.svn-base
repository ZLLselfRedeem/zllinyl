using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.Model.Interface;
using VAGastronomistMobileApp.Model.QueryObject;
using VAGastronomistMobileApp.WebPageDll;
using Web.Control;

public partial class Coupon_CouponManage : System.Web.UI.Page
{
    /// <summary>
    /// 获取查询条件
    /// </summary>
    /// <returns></returns>
    private CouponQueryObject GetQueryCondition()
    {
        CouponQueryObject queryObject = new CouponQueryObject();
        queryObject.CouponNameFuzzy = this.TextBoxName.Text;
        queryObject.ShopNameFuzzy = this.TextBoxShopName.Text;
        if (!string.IsNullOrEmpty(this.TextBoxCreateTimeFrom.Text))
        {
            queryObject.CreateTimeFrom = DateTime.Parse(this.TextBoxCreateTimeFrom.Text);
        }
        if (!string.IsNullOrEmpty(this.TextBoxCreateTimeTo.Text))
        {
            queryObject.CreateTimeTo = DateTime.Parse(this.TextBoxCreateTimeTo.Text).AddDays(0.99999);
        }
        if (!string.IsNullOrEmpty(this.DropDownListState.SelectedValue))
        {
            queryObject.State = int.Parse(this.DropDownListState.SelectedValue);
        }

        if (!string.IsNullOrEmpty(this.DropDownListCouponType.SelectedValue))
        {
            queryObject.CouponType = int.Parse(this.DropDownListCouponType.SelectedValue);
        }
        return queryObject;
    }
    #region 方法
    private void DoSearch()
    {
        var queryObject = GetQueryCondition();
        this.AspNetPager1.RecordCount = (int)CouponOperate.GetCountByQuery(queryObject);
        var list = CouponOperate.GetListByQuery(AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex, queryObject);
        this.GridViewCoupon.DataSource = list;
        this.GridViewCoupon.DataBind();
    }

    private void DoInit()
    {
        if (!this.IsPostBack)
        {
            this.TextBoxCreateTimeFrom.Text = DateTime.Today.AddDays(-10).ToString("yyyy-MM-dd");
            this.TextBoxCreateTimeTo.Text = DateTime.Today.ToString("yyyy-MM-dd");
            this.DoSearch();
        }
    }
    #endregion

    #region 事件
    protected void Page_Load(object sender, EventArgs e)
    {
        DoInit();
    }
    protected void ButtonSearch_Click(object sender, EventArgs e)
    {
        this.DoSearch();
    }
    #endregion
    protected void GridViewCoupon_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        var entity = e.Row.DataItem as Coupon;
        if (entity != null)
        {
            EmployeeOperate employeeOperate = new EmployeeOperate();
            var employee = employeeOperate.QueryEmployee(entity.CreatedBy);
            var LabelCreatedBy = e.Row.FindControl("LabelCreatedBy") as Label;
            if (LabelCreatedBy != null && employee != null)
            {
                LabelCreatedBy.Text = employee.EmployeeFirstName;
            }

            if (entity.AuditEmployee.HasValue && entity.State == 1)
            {
                employee = Common.ToInt32(entity.AuditEmployee) == 0 ? null : employeeOperate.QueryEmployee(Common.ToInt32(entity.AuditEmployee));
                var LabelAuditEmployee = e.Row.FindControl("LabelAuditEmployee") as Label;
                if (LabelAuditEmployee != null && employee != null)
                {
                    LabelAuditEmployee.Text = employee.EmployeeFirstName;
                }
            }

            var LabelState = e.Row.FindControl("LabelState") as Label;
            if (LabelState != null)
            {
                var CheckBoxSelect = e.Row.FindControl("CheckBoxSelect") as CheckBox;
                CheckBoxSelect.Enabled = false;
                e.Row.FindControl("LinkButtonStop").Visible = false;
                if (entity.State == 0)
                {
                    LabelState.Text = "失效（停止）";
                    e.Row.FindControl("LinkButtonRecovery").Visible = true;
                }
                else if (entity.State == 1)
                {
                    if (entity.EndDate < DateTime.Now)
                    {
                        LabelState.Text = "已结束";
                    }
                    else if (entity.StartDate > DateTime.Now)
                    {
                        LabelState.Text = "未开始";
                    }
                    else
                    {
                        LabelState.Text = "进行中";
                        e.Row.FindControl("LinkButtonStop").Visible = true;
                    }
                }
                else if (entity.State == -1)
                {
                    LabelState.Text = "待审核";
                    CheckBoxSelect.Enabled = true;
                }
                else if (entity.State == -2)
                {
                    LabelState.Text = "未通过";
                }
                else
                {
                    LabelState.Text = "未知";
                }
            }
            var LabelCouponType = e.Row.FindControl("LabelCouponType") as Label;
            if (LabelCouponType != null)
            {
                if (entity.CouponType == (int)CouponType.GeneralVouchers)
                {
                    LabelCouponType.Text = Common.GetEnumDescription(CouponType.GeneralVouchers);
                }
                else if (entity.CouponType == (int)CouponType.OperatingData)
                {
                    LabelCouponType.Text = Common.GetEnumDescription(CouponType.OperatingData);
                }
                else if (entity.CouponType == (int)CouponType.Package)
                {
                    LabelCouponType.Text = Common.GetEnumDescription(CouponType.Package);
                    e.Row.Cells[10].Text = string.Empty;
                }
            }

            var LabelCity = e.Row.FindControl("LabelCity") as Label;
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

    protected void GridViewCoupon_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Recovery")
        {
            int couponId = int.Parse(e.CommandArgument.ToString());
            var entity = CouponOperate.GetEntityById(couponId);
            entity.State = 1;
            if (CouponOperate.Update(entity))
            {
                #region------------------------------------------------------
                // 抵扣券恢复使用时，需要添加日志记录和公告提醒
                ShopAwardVersionOperate operateVersion = new ShopAwardVersionOperate();
                int employeeId = ((VAEmployeeLoginResponse)Session["UserInfo"]).employeeID;
                operateVersion.InsertShopAwardVersionAndLog(employeeId, Common.ToInt32(entity.ShopId), "抵扣券" + entity.CouponName+"恢复使用", "老后台", Guid.Empty);
                #endregion
                CommonPageOperate.AlterMsg(this, entity.CouponName + "活动,恢复使用成功！");
            }
            this.DoSearch();
        }
        if (e.CommandName == "Stop")
        {
            int couponId = int.Parse(e.CommandArgument.ToString());
            var entity = CouponOperate.GetEntityById(couponId);
            entity.State = 0;
            if (CouponOperate.Update(entity))
            {
                CommonPageOperate.AlterMsg(this, entity.CouponName + "活动,停用成功！");
            }
            this.DoSearch();
        }
    }
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        this.DoSearch();
    }
    /// <summary>
    /// 拒绝弹窗
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ButtonRefuse_Click(object sender, EventArgs e)
    {
        TextBoxRefuseReason.Text = "";
        Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>ConfirmWindow('PanelRefuse');</script>");
    }

    private int Check()
    {
        int flagCount = 0;
        for (int i = 0; i < GridViewCoupon.Rows.Count; i++)
        {
            CheckBox cb = (CheckBox)GridViewCoupon.Rows[i].FindControl("CheckBoxSelect");
            if (cb.Checked == true)
            {
                flagCount++;
            }
        }
        return flagCount;
    }
    /// <summary>
    /// 批量提交拒绝理由
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ButtonOk_Click(object sender, EventArgs e)
    {
        if (Check() <= 0)
        {
            CommonPageOperate.AlterMsg(this, "请先选择项。");
            return;
        }
        if (String.IsNullOrWhiteSpace(TextBoxRefuseReason.Text))
        {
            CommonPageOperate.AlterMsg(this, "拒绝理由不能为空。");
            return;
        }
        int employeeId = ((VAEmployeeLoginResponse)Session["UserInfo"]).employeeID;
        using (TransactionScope ts = new TransactionScope())
        {
            int count = 0;
            for (int i = 0; i < GridViewCoupon.Rows.Count; i++)
            {
                CheckBox cb = (CheckBox)GridViewCoupon.Rows[i].FindControl("CheckBoxSelect");
                if (cb.Checked == true)
                {
                    int couponId = Common.ToInt32(GridViewCoupon.DataKeys[i].Values["CouponId"]);
                    var entity = CouponOperate.GetEntityById(couponId);
                    entity.State = -2;
                    entity.AuditEmployee = employeeId;
                    entity.AuditTime = DateTime.Now;
                    entity.RefuseReason = entity.RefuseReason = DateTime.Now.ToString() + "，拒绝通过，原因：" + TextBoxRefuseReason.Text + "。";
                    if (CouponOperate.Update(entity))
                    {
                        count++;
                    }
                }
            }
            if (count == Check())
            {
                this.DoSearch();
                ts.Complete();
            }
            else
            {
                CommonPageOperate.AlterMsg(this, "操作失败。");
            }
        }
    }
    /// <summary>
    /// 数据导出excel
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ButtonExportExcel_Click(object sender, EventArgs e)
    {
        var queryObject = GetQueryCondition();
        this.AspNetPager1.RecordCount = (int)CouponOperate.GetCountByQuery(queryObject);
        List<ICoupon> list = CouponOperate.GetListByQuery(queryObject);

        if (list.Any())
        {
            DataTable dt = CommonPageOperate.GetTableFromList(list);
            dt.Columns["CouponId"].ColumnName = "优惠券编号";
            dt.Columns["CouponName"].ColumnName = "名称";
            dt.Columns["StartDate"].ColumnName = "开始时间";
            dt.Columns["EndDate"].ColumnName = "结束时间";
            dt.Columns["ShopId"].ColumnName = "门店编号";
            dt.Columns["RequirementMoney"].ColumnName = "满足条件";
            dt.Columns["DeductibleAmount"].ColumnName = "抵扣金额";
            dt.Columns["CreatedBy"].ColumnName = "创建人";
            dt.Columns["CreateTime"].ColumnName = "创建时间";
            dt.Columns["Remark"].ColumnName = "创建备注";
            dt.Columns["ShopName"].ColumnName = "门店名称";
            dt.Columns["RefuseReason"].ColumnName = "拒绝理由";
            dt.Columns["ValidityPeriod"].ColumnName = "有效期";
            dt.Columns["SheetNumber"].ColumnName = "领取数量";
            dt.Columns["SendCount"].ColumnName = "发放数量";
            dt.Columns["State"].ColumnName = "状态";
            dt.Columns["AuditEmployee"].ColumnName = "审核人";
            dt.Columns["AuditTime"].ColumnName = "审核时间";
            dt.Columns.Remove("SortOrder");
            dt.Columns.Remove("LastUpdatedBy");
            dt.Columns.Remove("LastUpdatedTime");
            dt.Columns.Remove("cityID");
            dt.Columns.Remove("ShopAddress");
            dt.Columns.Remove("IsGot");
            dt.Columns.Remove("Longitude");
            dt.Columns.Remove("Latitude");
            dt.Columns.Remove("DeductibleProportion");
            dt.Columns.Remove("Image");
            dt.Columns.Remove("CouponType");
            dt.Columns.Remove("IsDisplay");
            dt.Columns.Remove("SubsidyAmount");
            dt.Columns.Remove("MaxAmount");
            dt.Columns.Remove("TicketType");
            dt.Columns.Remove("SubsidyType"); 

            EmployeeOperate employeeOperate = new EmployeeOperate();
            foreach (DataRow item in dt.Rows)
            {
                item["创建人"] = employeeOperate.QueryEmployee(Common.ToInt32(item["创建人"])).EmployeeFirstName;
                if (Common.ToInt32(item["审核人"]) > 0)
                {
                    item["审核人"] = employeeOperate.QueryEmployee(Common.ToInt32(item["审核人"])).EmployeeFirstName;
                }
                int status = Common.ToInt32(item["状态"]);
                if (status == 0)
                {
                    item["状态"] = "失效（停止）";
                }
                else if (status == 1)
                {
                    if (Common.ToDateTime(item["结束时间"]) < DateTime.Now)
                    {
                        item["状态"] = "已结束";
                    }
                    else if (Common.ToDateTime(item["开始时间"]) > DateTime.Now)
                    {
                        item["状态"] = "未开始";
                    }
                    else
                    {
                        item["状态"] = "进行中";
                    }
                }
                else if (status == -1)
                {
                    item["状态"] = "待审核";
                }
                else if (status == -2)
                {
                    item["状态"] = "未通过";
                }
                else
                {
                    item["状态"] = "未知";
                }
            }

            ExcelHelper.ExportExcel(dt, this, "Coupon_" + DateTime.Now);
        }
    }
    /// <summary>;
    /// 审批通过
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ButtonAgree_Click(object sender, EventArgs e)
    {
        if (Check() <= 0)
        {
            CommonPageOperate.AlterMsg(this, "请先选择项。");
            return;
        }
        int employeeId = ((VAEmployeeLoginResponse)Session["UserInfo"]).employeeID;
        using (TransactionScope ts = new TransactionScope())
        {
            int count = 0;
            for (int i = 0; i < GridViewCoupon.Rows.Count; i++)
            {
                CheckBox cb = (CheckBox)GridViewCoupon.Rows[i].FindControl("CheckBoxSelect");
                if (cb.Checked == true)
                {
                    int couponId = Common.ToInt32(GridViewCoupon.DataKeys[i].Values["CouponId"]);
                    var entity = CouponOperate.GetEntityById(couponId);
                    entity.State = 1;
                    entity.RefuseReason = DateTime.Now.ToString() + "，通过审核。";
                    entity.AuditEmployee = employeeId;
                    entity.AuditTime = DateTime.Now;
                    if (CouponOperate.Update(entity))
                    {
                        count++;
                    }

                    #region------------------------------------------------------
                    // 添加门店奖品变更记录日志
                    ShopAwardVersionOperate operateVersion = new ShopAwardVersionOperate();
                    operateVersion.InsertShopAwardVersionAndLog(employeeId, Common.ToInt32(entity.ShopId), "添加抵扣券"+couponId, "老后台", Guid.Empty);
                    #endregion
                }
            }
            if (count == Check())
            {
                this.DoSearch();
                ts.Complete();
            }
            else
            {
                CommonPageOperate.AlterMsg(this, "操作失败。");
            }
        }
    }
}