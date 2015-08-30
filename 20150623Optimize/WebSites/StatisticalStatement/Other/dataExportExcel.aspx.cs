using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using VAGastronomistMobileApp.WebPageDll;
using Web.Control;
using Web.Control.DDL;
using VAGastronomistMobileApp.Model.QueryObject;
using VAGastronomistMobileApp.Model;

public partial class StatisticalStatement_Other_dataExportExcel : System.Web.UI.Page
{
     

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        { 
            //new CityDropDownList().BindCity(ddlCity);
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button_operate_Click(object sender, EventArgs e)
    {
        if (TextBox_orderStartTime.Text.Trim() == "" || TextBox_orderEndTime.Text.Trim() == "")
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('时间条件输入有误');</script>");
            return;
        }

        DateTime? strartTime = null;
        DateTime? endTime = null; 
        byte? status = null;
        int? isHandle = null;
        PreOrder19dianOperate preOrder19dianOperate = new PreOrder19dianOperate();

        if(!string.IsNullOrEmpty(this.TextBox_orderStartTime.Text))
        {
          strartTime=  Common.ToDateTime(TextBox_orderStartTime.Text).Date;
        }
        if(!string.IsNullOrEmpty(this.TextBox_orderEndTime.Text))
        {
            endTime = Common.ToDateTime(TextBox_orderEndTime.Text).Date.AddDays(1);
        }
        if(!string.IsNullOrEmpty(this.RadioButtonListStatus.SelectedValue))
        {
            status = byte.Parse(this.RadioButtonListStatus.SelectedValue);
        }

        if (!string.IsNullOrEmpty(this.DropDownListShopState.SelectedValue))
        {
            isHandle = int.Parse(this.DropDownListShopState.SelectedValue);
        }

        double minAmount = Common.ToDouble(TextBox_paymentMin.Text.Trim());
        double maxAmount = 0;
        if (TextBox_paymentMax.Text.Trim() == "")
        {
            maxAmount = 10000000;
        }
        else
        {
            maxAmount = Common.ToDouble(TextBox_paymentMax.Text.Trim());
        }
        if (minAmount > maxAmount)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('支付金额条件输入有误');</script>");
            return;
        }
        //StatisticalStatementOperate oper = new StatisticalStatementOperate();
        //int flag = 0;
        //if (sum.Checked)//按照金额
        //{
        //    flag = 1;
        //}
        //else//按照点单数量
        //{
        //    flag = 2;
        //}
        //int flag1 = rbNone.Checked ? 1 : (rbConfrim.Checked ? 2 : 3);
        int? cityId = null;
        if (!string.IsNullOrEmpty(ddlCity.SelectedValue))
        {
            cityId = Common.ToInt32(ddlCity.SelectedValue);
        } 

        var queryObject = new Preorder19DianLineVQueryObject()
        {
            CityID = cityId,
            PayType = 2,
            CreateTimeFrom = strartTime,
            CreateTimeTo = endTime,
            Status = status
        };
        //支付宝支付
        var list = Preorder19DianLineVOperate.GetListByQueryWithColumns(queryObject,
                                Preorder19DianLineVOrderColumn.Amount, Preorder19DianLineVOrderColumn.RefundAmount,
                                Preorder19DianLineVOrderColumn.Preorder19DianId, Preorder19DianLineVOrderColumn.ShopID);

        queryObject = new Preorder19DianLineVQueryObject()
        {
            CityID = cityId,
            PayType = 3,
            CreateTimeFrom = strartTime,
            CreateTimeTo = endTime,
            Status = status
        };
         
        //微信支付
        var wechatList = Preorder19DianLineVOperate.GetListByQueryWithColumns(queryObject,
                                Preorder19DianLineVOrderColumn.Amount, Preorder19DianLineVOrderColumn.RefundAmount,
                                 Preorder19DianLineVOrderColumn.Preorder19DianId, 
                                Preorder19DianLineVOrderColumn.ShopID);

        if (wechatList != null && wechatList.Count > 0)
        {
            list.AddRange(wechatList);
        }

        queryObject = new Preorder19DianLineVQueryObject()
        {
            CityID = cityId,
            PayType = 1,
            CreateTimeFrom = strartTime,
            CreateTimeTo = endTime,
            Status = status
        };
        //粮票支付
        var balanceList = Preorder19DianLineVOperate.GetListByQueryWithColumns(queryObject,
                                Preorder19DianLineVOrderColumn.Amount, Preorder19DianLineVOrderColumn.RefundAmount, Preorder19DianLineVOrderColumn.Preorder19DianId,Preorder19DianLineVOrderColumn.ShopID);

        if (balanceList != null && balanceList.Count > 0)
        {
            list.AddRange(balanceList);
        }
      

        DataTable dt = new DataTable(); 

        DataColumn dataColumn = new DataColumn("CityName");
        dataColumn.Caption = "城市";
        dt.Columns.Add(dataColumn);
        dataColumn = new DataColumn("IsHandle");
        dataColumn.Caption = "是否上线";
        dt.Columns.Add(dataColumn);
        dataColumn = new DataColumn("ShopName");
        dataColumn.Caption = "店铺名称";
        dt.Columns.Add(dataColumn);
        dataColumn = new DataColumn("AccountManager");
        dataColumn.Caption = "客户经理";
        dt.Columns.Add(dataColumn);
        dataColumn = new DataColumn("AreaManager");
        dataColumn.Caption = "区域经理";
        dt.Columns.Add(dataColumn);
        dataColumn = new DataColumn("TotalPreorderSum");
        dataColumn.Caption = "交易累计";
        dt.Columns.Add(dataColumn);
        dataColumn = new DataColumn("TotalPreorderCount");
        dataColumn.Caption = "点单量";
        dt.Columns.Add(dataColumn);



        if (list != null && list.Count > 0)
        {
            var exportList =  from f in list
                             where f.Amount > f.RefundAmount
                             group f by f.ShopID into g
                             select g;
            DataRow dr = null;
            ShopOperate shopOperate = new ShopOperate();
            EmployeeOperate employeeOperate = new EmployeeOperate();
            CityOperate cityOperate = new CityOperate();
            foreach (var entity in exportList)
            {

                var shopPreorderList = from f in entity
                                       group f by f.Preorder19DianId into g
                                       select g;
                double totalPreorderSum  =
                    Common.ToDouble(
                    shopPreorderList.Sum(p => p.Sum(g => g.Amount - g.RefundAmount) - preOrder19dianOperate.SelectExtendPay(p.Key)));

                var preorderSumList =
                  (from p in shopPreorderList
                 select
                 p.Sum(g => g.Amount - g.RefundAmount) - preOrder19dianOperate.SelectExtendPay(p.Key)).AsParallel();
                preorderSumList = preorderSumList.Where(p => p < maxAmount && p > minAmount);
                if (preorderSumList.Count() == 0)
                {
                    continue;
                }
                dr = dt.NewRow();
                dr["TotalPreorderSum"] = preorderSumList.Sum();
                dr["TotalPreorderCount"] = preorderSumList.Count();

                var shop = shopOperate.QueryShop(entity.Key);
                if (shop != null)
                {
                    var city = cityOperate.GetCityNameAndShopName(shop.shopID);
                    dr["CityName"] = city.cityName;
                    if (isHandle == 1  )
                    {
                        if (shop.isHandle != 1)
                        {
                            continue;
                        }
                    } 
                    else if (isHandle == 2)
                    {
                        if (shop.isHandle == 1)
                        {
                            continue;
                        }
                    }

                    if (shop.isHandle == 1)
                    {
                        dr["IsHandle"] = "是";
                    }
                    else
                    {
                        dr["IsHandle"] = "否";
                    }
                    dr["ShopName"] = shop.shopName;

                    if (shop.AreaManager.HasValue)
                    {
                        var areaManager = employeeOperate.QueryEmployee(shop.AreaManager.Value);
                        if (areaManager != null)
                        {
                            dr["AreaManager"] = areaManager.EmployeeFirstName;
                        }
                        else
                        {
                            dr["AreaManager"] = "-";
                        }
                    }
                    else
                    {
                        dr["AreaManager"] = "-";
                    }

                    if (shop.accountManager.HasValue)
                    {
                        var accountManager = employeeOperate.QueryEmployee(shop.accountManager.Value);
                        if (accountManager != null)
                        {
                            dr["AccountManager"] = accountManager.EmployeeFirstName;
                        }
                        else
                        {
                            dr["AccountManager"] = "-";
                        }
                    }
                    else
                    {
                        dr["AccountManager"] = "-";
                    }
                }
                dt.Rows.Add(dr);
            } 
        }
        
        if (dt.Rows.Count > 0)
        {
            ExcelHelper.ExportExcel(dt, this, "OrderStatement_" + DateTime.Now.ToString("yyyy/mm/dd_hh:mm:ss"));
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('没有数据');</script>");
        }
    }
}