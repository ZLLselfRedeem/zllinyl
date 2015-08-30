using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using VAGastronomistMobileApp.WebPageDll;
using System.Data;
using VAGastronomistMobileApp.Model;
using System.Transactions;
using Web.Control.DDL;
using System.Net;
using System.Net.Sockets;
using Web.Control;
using System.Reflection;
using System.EnterpriseServices;
using System.ServiceModel.Activities;

public partial class Package_PackageManager : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            new CityDropDownList().BindCity(ddlCity);
            Button_LargePageCount_Click(Button_10, null);//分页选中10
        }
    }
    
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        AspNetPager1.CurrentPageIndex = 1;
        LoadData(0, Common.ToInt32(Label_LargePageCount.Text), Common.ToInt32(ddlCity.SelectedValue), Common.ToInt32(ddlStatus.SelectedValue));
    }

    protected void btn_New_Click(object sender, EventArgs e)
    {
        Response.Redirect("PackageDetail.aspx");
    }

    private void LoadData(int str,int end,int cityId,int status)
    {
        PackageOperate po = new PackageOperate();
        DataTable dt = po.Packages(cityId, status);
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

        gdList.DataSource = dtPage;
        gdList.DataBind();

        for (int i = 0; i < gdList.Rows.Count; i++)
        {
            if (Common.ToInt32(gdList.DataKeys[i].Values["ValuationType"]) == (int)ValuationType.ByPerson)
            {
                gdList.Rows[i].Cells[3].Text = killZero(Convert.ToDecimal(gdList.Rows[i].Cells[3].Text).ToString()) + "/人次";
            }
            else
            {
                gdList.Rows[i].Cells[3].Text = killZero(Convert.ToDecimal(gdList.Rows[i].Cells[3].Text).ToString()) + "/次";
            }

            LinkButton lbtnChangeStatus = (LinkButton)gdList.Rows[i].FindControl("lbtnChangeStatus");

            if (Common.ToInt32(gdList.DataKeys[i].Values["Status"]) == (int)PackageStatus.Enable)
            {
                gdList.Rows[i].Cells[5].Text = "已启用";
                lbtnChangeStatus.Text = "停用";
                lbtnChangeStatus.Attributes.Add("onclick", "return confirm('是否确定停用当前模板？');");
            }
            else
            {
                gdList.Rows[i].Cells[5].Text = "已停用";
                lbtnChangeStatus.Text = "启用";
                lbtnChangeStatus.Attributes.Add("onclick", "return confirm('是否确定启用当前模板？');");
            }

            foreach (ShopLevel level in Enum.GetValues(typeof(ShopLevel)))
            {
                if ((int)level == Common.ToInt32(gdList.DataKeys[i].Values["LevelRequirements"]))
                {
                    gdList.Rows[i].Cells[4].Text = Common.GetEnumDescription(level);
                }
            } 

            LinkButton lbUpdate = (LinkButton)gdList.Rows[i].FindControl("lbtnUpdate");
            lbUpdate.PostBackUrl = "PackageDetail.aspx?ID=" + gdList.DataKeys[i].Values["ID"].ToString();
            
        }
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        LoadData(AspNetPager1.StartRecordIndex - 1, AspNetPager1.EndRecordIndex,Common.ToInt32(ddlCity.SelectedValue), Common.ToInt32(ddlStatus.SelectedValue));
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
        LoadData(0, Common.ToInt32(Label_LargePageCount.Text), Common.ToInt32(ddlCity.SelectedValue),Common.ToInt32(ddlStatus.SelectedValue));
    }
    protected void gdList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    DataRowView dr = e.Row.DataItem as DataRowView;
        //    if (dr != null)
        //    {
                
        //    }
        //}
    }
    protected void gdList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ChangeStatus")
        {
            int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;//转换为按钮类型，获取其所在的行的索引
            int ChangeID = Common.ToInt32(gdList.DataKeys[index].Values["ID"]);
            int Status = Common.ToInt32(!Common.ToBool(gdList.DataKeys[index].Values["Status"]));
            PackageOperate po = new PackageOperate();
            po.ChangeStatus(ChangeID, Status);

            AspNetPager1_PageChanged(sender, e);

            int g = 0;
            while (g == 0)
            {
                for (int i = 0; i < gdList.Rows.Count; i++)
                {
                    if (gdList.DataKeys[i].Values["ID"].ToString().Equals(ChangeID.ToString()))
                    {
                        g++;
                        break;
                    }
                }
                if (g == 0)
                {
                    AspNetPager1.CurrentPageIndex = AspNetPager1.CurrentPageIndex + 1;
                    if (AspNetPager1.CurrentPageIndex == AspNetPager1.PageCount)
                    {
                        break;
                    }
                    AspNetPager1_PageChanged(sender, e);
                }
            }
        }
    }

    private string killZero(string str)
    {
        if (str.IndexOf('.') != -1)
        {
            return str.TrimEnd('0').TrimEnd('.');
        }
        else
        {
            return str;
        }

    }
}