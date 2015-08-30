using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.WebPageDll;
using Web.Control;
using Web.Control.DDL;

public partial class PreOrder19dianManage_mealOrderConfirm : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            new CityDropDownList().BindCity(ddlCity);
        }
    }
    private void BindGridView(int pageIndex, int pageSize)
    {
        var list = new MealOperate().SelectMealOrderReport();
        if (!list.Any())
        {
            grOrderList.DataSource = null;
            grOrderList.DataBind();
            return;
        }
        int orderStatus = 0;
        if (rbNotPayment.Checked == true)
        {
            orderStatus = (int)OrderStatus.未付款;
        }
        else if (rbNotConfrim.Checked == true)
        {
            orderStatus = (int)OrderStatus.待确认;
        }
        else if (rbYesConfrim.Checked == true)
        {
            orderStatus = (int)OrderStatus.已确认;
        }
        else if (rbYesRefund.Checked == true)
        {
            orderStatus = (int)OrderStatus.已退款;
        }
        else if (rbOvertimeNotPayment.Checked == true)
        {
            orderStatus = (int)OrderStatus.超时未付款;
        }
        list = list.Where(p => p.orderStatus == orderStatus).ToList();

        int cityId = Common.ToInt32(ddlCity.SelectedValue);
        if (cityId > 0)
        {
            list = list.Where(p => p.cityId == cityId).ToList();
        }

        if (!String.IsNullOrWhiteSpace(txtOrderStartTime.Text) && !String.IsNullOrWhiteSpace(txtOrderEndTime.Text))
        {
            DateTime orderStartTime = Common.ToDateTime(txtOrderStartTime.Text + " 00:00:00");
            DateTime orderEndTime = Common.ToDateTime(txtOrderEndTime.Text + " 23:59:59");
            list = list.Where(p => p.orderTime > orderStartTime && p.orderTime < orderEndTime).ToList();
        }

        if (!String.IsNullOrWhiteSpace(txtPayStartTime.Text) && !String.IsNullOrWhiteSpace(txtPayEndTime.Text))
        {
            DateTime payStartTime = Common.ToDateTime(txtPayStartTime.Text + " 00:00:00");
            DateTime payEndTime = Common.ToDateTime(txtPayEndTime.Text + " 23:59:59");
            list = list.Where(p => Common.ToDateTime(p.orderPayTime) > payStartTime && Common.ToDateTime(p.orderTime) < payEndTime).ToList();
        }

        if (!String.IsNullOrWhiteSpace(txtShopName.Text))
        {
            list = list.Where(p => p.shopName.Contains(txtShopName.Text)).ToList();
        }
        if (!String.IsNullOrWhiteSpace(txtServieManager.Text))
        {
            list = list.Where(p => p.employeeName.Contains(txtServieManager.Text)).ToList();
        }
        if (!String.IsNullOrWhiteSpace(txtPhone.Text))
        {
            list = list.Where(p => p.customerPhone.Contains(txtPhone.Text)).ToList();
        }

        if (list.Any())
        {
            AspNetPager1.RecordCount = list.Count;
            grOrderList.DataSource = list.Skip(pageIndex * pageSize).Take(pageSize);
        }
        else
        {
            AspNetPager1.RecordCount = 0;
        }
        grOrderList.DataBind();
    }

    /// <summary>
    /// 分页操作
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        BindGridView((AspNetPager1.StartRecordIndex - 1) / 10, 10);
    }
    /// <summary>
    /// 查询事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        BindGridView(0, 10);
    }
}