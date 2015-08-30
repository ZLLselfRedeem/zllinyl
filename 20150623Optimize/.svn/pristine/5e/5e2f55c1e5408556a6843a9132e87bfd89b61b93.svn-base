using System;
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
using Web.Control.DDL;

public partial class Coupon_CouponCount : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            new CityDropDownList().BindCity(ddlCity);
        }
    }
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        if (txtOperateBeginTime.Text.Equals(string.Empty) || txtOperateEndTime.Text.Equals(string.Empty))
        {
            CommonPageOperate.AlterMsg(this, "时间区间请选择完整");
            return;
        }

        if (Common.ToDateTime(txtOperateEndTime.Text) < Common.ToDateTime(txtOperateBeginTime.Text))
        {
            CommonPageOperate.AlterMsg(this, "开始时间必须小于结束时间");
            return;
        }

        if ((Common.ToDateTime(txtOperateEndTime.Text)-Common.ToDateTime(txtOperateBeginTime.Text)).Days > 6)
        {
            CommonPageOperate.AlterMsg(this, "查询区间为7天");
            return;
        }

        CreateExcel(GetGrData());
    }

    private DataTable GetGrData()
    {
        CouponGetDetailOperate cgo = new CouponGetDetailOperate();
        DataTable dt = cgo.GetCouponCount(Common.ToInt32(ddlCity.SelectedValue), txtOperateBeginTime.Text, txtOperateEndTime.Text);

        return dt;
    }

    private void CreateExcel(DataTable dt)
    {

        if (dt.Rows.Count.Equals(0))
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('查无相关明细')</script>");
            return;
        }
        string excelName = HttpUtility.UrlEncode("券支付统计_" + DateTime.Now.ToString("yyyy/mm/dd_hh:mm:ss"), System.Text.Encoding.UTF8).ToString();
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
                else if (i == 2 || i == 5)
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