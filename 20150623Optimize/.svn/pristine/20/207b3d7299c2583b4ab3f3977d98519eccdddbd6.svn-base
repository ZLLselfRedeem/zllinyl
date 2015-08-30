using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.WebPageDll;
using System.Data;
using VAGastronomistMobileApp.Model;
using System.Transactions;
using Web.Control.DDL;
using System.Net;
using System.Net.Sockets;
using VA.CacheLogic;
using Web.Control;

public partial class FinanceManage_financialReconciliation : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            new CityDropDownList().BindCity(ddlCity);
            init_date.InnerHtml = "";
            //初始化分页
            Button_10.CssClass = "tabButtonBlueClick";
            Button_50.CssClass = "tabButtonBlueUnClick";
            Button_100.CssClass = "tabButtonBlueUnClick";
            Label_LargePageCount.Text = "10";

            ddlStatus.SelectedIndex = 1;
            ViewState["vsShopId"] = Common.ToInt32(Request.QueryString["id"]);
            if (!string.IsNullOrEmpty(Common.ToString(Request.QueryString["name"])))
            {
                string[] strRequest = Common.ToString(Request.QueryString["name"]).Split(',');//回传页面显示公司名称
                text.Value = strRequest[0];
              
            }
            else
            {
                text.Value = string.Empty;
            }

            txtOperateEndTime.Text = DateTime.Now.ToString("yyyy/MM/dd") + " 23:59:59";
            txtOperateBeginTime.Text = DateTime.Now.AddDays(-7).ToString("yyyy/MM/dd") + " 00:00:00";

        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (String.IsNullOrEmpty(txtOperateBeginTime.Text))
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('开始时间不能为空')</script>");
            return;
        }
        if (String.IsNullOrEmpty(txtOperateEndTime.Text))
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('结束时间不能为空')</script>");
            return;
        }
        if (Common.ToDateTime(txtOperateBeginTime.Text) > Common.ToDateTime(txtOperateEndTime.Text))
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('开始时间不能大于结束时间')</script>");
            return;
        }
        if ((Common.ToDateTime(txtOperateEndTime.Text)-Common.ToDateTime(txtOperateBeginTime.Text)).Days>31)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('查询区间不能大于一个月')</script>");
            return;
        }

        AspNetPager1.CurrentPageIndex = 1;

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
        AspNetPager1.CurrentPageIndex = 0;
        loadData(0, Common.ToInt32(Label_LargePageCount.Text));
    }

    /// <summary>
    /// 加载列表
    /// </summary>
    /// <param name="shopId"></param>
    private void loadData(int str, int end)
    {
        PreOrder19dianOperate po = new PreOrder19dianOperate();
        int shopID = 0;
        if (!String.IsNullOrEmpty(text.Value.Trim()))
        {
            shopID = Common.ToInt32(Request.QueryString["id"]);
        }
        DataTable dt = po.GetFinancial(shopID, txtOperateBeginTime.Text, txtOperateEndTime.Text, Common.ToInt32(ddlStatus.SelectedValue), Common.ToInt32(ddlCity.SelectedValue));
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
        string sortedField = "";
        if (ViewState["SortedField"] != null)
        {
            Dictionary<string, string> sorted = (Dictionary<string, string>)ViewState["SortedField"];
            foreach (KeyValuePair<string, string> kvp in sorted)
            {
                sortedField = kvp.Key + "  " + kvp.Value;
            }
            dtPage.DefaultView.Sort = sortedField;
        }
        gdList.DataSource = null;
        gdList.DataSource = dtPage;
        gdList.DataBind();
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        loadData(AspNetPager1.StartRecordIndex - 1, AspNetPager1.EndRecordIndex);
    }
   
    protected void gdList_Sorting(object sender, GridViewSortEventArgs e)
    {
        Dictionary<string, string> sorted = new Dictionary<string, string>();
        if (ViewState["SortedField"] == null)
        {
            sorted.Add(e.SortExpression, "ASC");
            ViewState["SortedField"] = sorted;
        }
        else
        {
            sorted = (Dictionary<string, string>)ViewState["SortedField"];
            if (sorted.ContainsKey(e.SortExpression))
            {
                if (sorted[e.SortExpression] == "ASC")
                {
                    sorted[e.SortExpression] = "DESC";
                }
                else
                {
                    sorted[e.SortExpression] = "ASC";
                }
            }
            else
            {
                sorted.Clear();
                sorted.Add(e.SortExpression, "ASC");
                ViewState["SortedField"] = sorted;
            }
        }
        loadData(0, Common.ToInt32(Label_LargePageCount.Text));
    }
   
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        CreateExcel(GetGrData());
    }

    private DataTable GetGrData()
    {
        DataTable dt = new DataTable();
        PreOrder19dianOperate po = new PreOrder19dianOperate();
        int shopID=0;
        if (!text.Value.Trim().Equals(string.Empty))
        {
            shopID = Common.ToInt32(Request.QueryString["id"]);
        }
        DataTable dtApply = po.GetFinancial(shopID, txtOperateBeginTime.Text, txtOperateEndTime.Text, Common.ToInt32(ddlStatus.SelectedValue), Common.ToInt32(ddlCity.SelectedValue));

        dt.Columns.Add("门店名", typeof(string));
        dt.Columns.Add("公司名", typeof(string));
        dt.Columns.Add("交易量", typeof(string));
        dt.Columns.Add("当前佣金比例", typeof(string));
        dt.Columns.Add("实际佣金比例", typeof(string));
        dt.Columns.Add("佣金金额", typeof(string));
        dt.Columns.Add("红包支付", typeof(string));
        dt.Columns.Add("粮票支付", typeof(string));
        dt.Columns.Add("支付宝支付", typeof(string));
        dt.Columns.Add("微信支付", typeof(string));
        dt.Columns.Add("纯红包贴补", typeof(string));

        for (int i = 0; i < dtApply.Rows.Count; i++)
        {
            DataRow dr = dt.NewRow();
            dr[0] = dtApply.Rows[i]["shopName"].ToString();
            dr[1] = dtApply.Rows[i]["companyName"].ToString();
            dr[2] = dtApply.Rows[i]["prePaidSum"].ToString();
            dr[3] = dtApply.Rows[i]["viewallocCommissionValue"].ToString();
            dr[4] = dtApply.Rows[i]["viewalloc"].ToString();
            dr[5] = dtApply.Rows[i]["viewallocCommission"].ToString();
            dr[6] = Common.ToDouble(dtApply.Rows[i]["redenvelope"]).ToString();
            dr[7] = Common.ToDouble(dtApply.Rows[i]["balance"]).ToString();
            dr[8] = Common.ToDouble(dtApply.Rows[i]["alipay"]).ToString();
            dr[9] = Common.ToDouble(dtApply.Rows[i]["wechat"]).ToString();
            dr[10] = Common.ToDouble(dtApply.Rows[i]["ExtendPay"]).ToString();
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
        string excelName = HttpUtility.UrlEncode("批量打款_" + DateTime.Now.ToString("yyyy/mm/dd_hh:mm:ss"), System.Text.Encoding.UTF8).ToString();
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
                if (i == (cl - 1))//最后一列，加n
                {
                    ls_item += "'" + row[i].ToString().Trim() + "\n";
                }
                else if (i > 1)
                {
                    ls_item += "'" + row[i].ToString().Trim() + "\t";
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