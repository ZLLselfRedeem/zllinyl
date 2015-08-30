using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.WebPageDll;
using Web.Control.DDL;

public partial class ShopManage_ShopAmountLogQuery : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            init_date.InnerHtml = "";
            new CityDropDownList().BindCity(ddlCity);
            txLogDate.Text = DateTime.Now.AddDays(-1).ToString("yyyy/MM/dd");
            if (!string.IsNullOrEmpty(Common.ToString(Request.QueryString["name"])))
            {
                string[] strRequest = Common.ToString(Request.QueryString["name"]).Split(',');//回传页面显示公司名称
                text.Value = strRequest[0];
            }
            else
            {
                text.Value = string.Empty;
            }

            Label_LargePageCount.Text = "10"; ;//分页选中10
        }
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        loadData(0, Common.ToInt32(Label_LargePageCount.Text));
    }

    //分页数目控制
    protected void Button_LargePageCount_Click(object sender, EventArgs e)
    {
        Button Button = (Button)sender;
        Label_LargePageCount.Text = "";
        switch (Button.CommandName)
        {
            case "button_10":
                Button_10.CssClass = "tabButtonBlueClick";
                Button_50.CssClass = "tabButtonBlueUnClick";
                Button_100.CssClass = "tabButtonBlueUnClick";
                Label_LargePageCount.Text = "10";
                break;
            case "button_50":
                Button_10.CssClass = "tabButtonBlueUnClick";
                Button_50.CssClass = "tabButtonBlueClick";
                Button_100.CssClass = "tabButtonBlueUnClick";
                Label_LargePageCount.Text = "50";
                break;
            case "button_100":
                Button_10.CssClass = "tabButtonBlueUnClick";
                Button_50.CssClass = "tabButtonBlueUnClick";
                Button_100.CssClass = "tabButtonBlueClick";
                Label_LargePageCount.Text = "100";
                break;
            default:
                break;
        }
        loadData(0, Common.ToInt32(Label_LargePageCount.Text));
    }

    /// <summary>
    /// 加载列表
    /// </summary>
    /// <param name="shopId"></param>
    private void loadData(int str, int end)
    {
        DateTime logDate = Common.ToDateTime(txLogDate.Text.Trim());
        ShopOperate so = new ShopOperate();
        int shopID=Common.ToInt32(Request.QueryString["id"]);
        if (string.IsNullOrEmpty(text.Value))
        {
            shopID = 0;
        }
        DataTable dt = so.QueryShopAmountLog(logDate,Common.ToInt32(ddlCity.SelectedValue),shopID);
        DataTable dtPage = new DataTable();
        AspNetPager1.PageSize = Common.ToInt32(Label_LargePageCount.Text);
        if (dt.Rows.Count > 0)
        {
            int tableCount = dt.Rows.Count;
            AspNetPager1.RecordCount = tableCount;
            dtPage = Common.GetPageDataTable(dt, str, end);
        }
        else
        {
            AspNetPager1.RecordCount = 0;
        }

        shopAmoutLogList.DataSource = null;
        shopAmoutLogList.DataSource = dtPage;
        shopAmoutLogList.DataBind();
    }
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        loadData(AspNetPager1.StartRecordIndex - 1, AspNetPager1.EndRecordIndex);
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        ShopOperate so = new ShopOperate();
        so.CreateShopAmountLog();
    }
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        CreateExcel(GetGrData());
    }

    private DataTable GetGrData()
    {
        DataTable dt = new DataTable();
        DateTime logDate = Common.ToDateTime(txLogDate.Text.Trim());
        ShopOperate so = new ShopOperate();
        int shopID = Common.ToInt32(Request.QueryString["id"]);
        if (string.IsNullOrEmpty(text.Value))
        {
            shopID = 0;
        }
        DataTable dtApply = so.QueryShopAmountLog(logDate, Common.ToInt32(ddlCity.SelectedValue), shopID);
        dt.Columns.Add("门店名", typeof(string));
        dt.Columns.Add("城市", typeof(string));
        dt.Columns.Add("商户余额", typeof(string));

        for (int i = 0; i < dtApply.Rows.Count; i++)
        {
            DataRow dr = dt.NewRow();
            dr[0] = dtApply.Rows[i]["shopName"].ToString();
            dr[1] = dtApply.Rows[i]["cityName"].ToString();
            dr[2] = Common.ToDouble(dtApply.Rows[i]["amount"].ToString()).ToString();
            dt.Rows.Add(dr);

        }
        return dt;
    }

    private void CreateExcel(DataTable dt)
    {
        if (dt.Rows.Count.Equals(0))
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('查无相关明细')</script>");
            return;
        }
        string excelName = HttpUtility.UrlEncode("商户余额日志_" + DateTime.Now.ToString("yyyy/mm/dd_hh:mm:ss"), System.Text.Encoding.UTF8).ToString();
        HttpResponse resp;
        resp = Page.Response;
        resp.Buffer = true;
        resp.ClearContent();
        resp.ClearHeaders();
        resp.Charset = "GB2312";
        resp.AppendHeader("Content-Disposition", "attachment;filename=" + excelName + ".xls");
        resp.ContentEncoding = System.Text.Encoding.Default;//设置输出流为简体中文   
        resp.ContentType = "application/ms-excel";//设置输出文件类型为excel文件。 
        string colHeaders = "", ls_item = "";
        DataRow[] myRow = dt.Select();
        int i = 0;
        int cl = dt.Columns.Count;
        for (i = 0; i < cl; i++)
        {
            if (i == (cl - 1))//最后一列，加n
            {
                colHeaders += dt.Columns[i].Caption.ToString().Trim() + "\n";
            }
            else
            {
                colHeaders += dt.Columns[i].Caption.ToString().Trim() + "\t";
            }
        }
        resp.Write(colHeaders);
        foreach (DataRow row in myRow)
        {
            for (i = 0; i < cl; i++)
            {
                if (i == (cl - 1))
                {
                    ls_item += "'" + row[i].ToString().Trim() + "\n";
                }
                else
                {
                    ls_item += row[i].ToString().Trim() + "\t";
                }
            }
            resp.Write(ls_item);
            ls_item = "";
        }
        resp.End();
    }
}