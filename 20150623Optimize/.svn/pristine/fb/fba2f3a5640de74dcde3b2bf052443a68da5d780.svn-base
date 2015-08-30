using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.WebPageDll;

public partial class Award_ChangeHistory : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            txtShopName.Text = Convert.ToString(Request.QueryString["shopName"]);
            txtCompanyName.Text = Convert.ToString(Request.QueryString["companyName"]);
            BindGridViewChange(0, 10);
        }
    }


    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        BindGridViewChange(AspNetPager1.StartRecordIndex - 1, AspNetPager1.EndRecordIndex);
    }

    /// <summary>
    /// 绑定GRIDVIEW
    /// </summary>
    protected void BindGridViewChange(int beginIndex,int endIndex)
    {
        int shopID = VAGastronomistMobileApp.WebPageDll.Common.ToInt32(Request.QueryString["shopID"]);
        DataTable dt = new DataTable();
        ShopAwardVersionLogOperate operate = new ShopAwardVersionLogOperate();
        dt=operate.SelectAwardVersionLog(shopID);
        if (dt.Rows.Count > 0)
        {
            int tableCount = dt.Rows.Count;
            AspNetPager1.RecordCount = tableCount;
            DataTable dt_page = Common.GetPageDataTable(dt, beginIndex, endIndex);//分页的DataTable
            GridViewChange.DataSource = dt;
            GridViewChange.DataBind();
        }
    }
}