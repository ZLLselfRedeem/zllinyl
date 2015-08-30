using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.WebPageDll;
using System.Data;
using VAGastronomistMobileApp.Model;

public partial class WeChatPlatManage_RecommendRestaurant : System.Web.UI.Page
{
    private static DataTable dt_page;
    private static DataTable dt_recommand;
    //private Dictionary<string, object> dateDic = new Dictionary<string, object>();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //dateDic.Add(Guid.NewGuid().ToString(), "yoyo");
            initTable();
            Button_City_Click(Button_Hangzhou, e);

        }
    }

    private void initTable()
    {
        dt_recommand = new DataTable();
        dt_recommand.Columns.AddRange(new DataColumn[] { 
                new DataColumn("field",typeof(string)),
                new DataColumn("shopID",typeof(int)),
                new DataColumn("shopName",typeof(string)),
                new DataColumn("shopAddress",typeof(string)),
                new DataColumn("shopTelephone",typeof(string)),
                new DataColumn("recommandType",typeof(int)),
                new DataColumn("recommandTypeName",typeof(string))
            });
        //加载数据
        WechatRecommandShopOperator rso = new WechatRecommandShopOperator();
        DataTable dt = rso.GetRecommandShopInfo();
        foreach (DataRow dr in dt.Rows)
        {
            DataRow drNew = dt_recommand.NewRow();
            drNew["field"] = dr["cityName"];
            drNew["shopID"] = dr["shopID"];
            drNew["shopName"] = dr["shopName"];
            drNew["shopAddress"] = dr["shopAddress"];
            drNew["shopTelephone"] = dr["shopTelephone"];
            drNew["recommandType"] = dr["recommandType"];
            drNew["recommandTypeName"] = dr["recommandTypeName"];
            dt_recommand.Rows.Add(drNew);
        }
    }

    protected void Button_City_Click(object sender, EventArgs e)
    {
        Button Button = (Button)sender;
        switch (Button.CommandName)
        {
            case "Hangzhou":
                Button_Hangzhou.CssClass = "tabButtonBlueClick";
                Button_Beijing.CssClass = "tabButtonBlueUnClick";
                Button_Shanghai.CssClass = "tabButtonBlueUnClick";
                Button_Guangzhou.CssClass = "tabButtonBlueUnClick";
                Button_Shenzhen.CssClass = "tabButtonBlueUnClick";
                cityName.Value = "杭州市";
                break;
            case "Beijing":
                Button_Beijing.CssClass = "tabButtonBlueClick";
                Button_Hangzhou.CssClass = "tabButtonBlueUnClick";
                Button_Shanghai.CssClass = "tabButtonBlueUnClick";
                Button_Guangzhou.CssClass = "tabButtonBlueUnClick";
                Button_Shenzhen.CssClass = "tabButtonBlueUnClick";
                cityName.Value = "北京市";
                break;
            case "Shanghai":
                Button_Shanghai.CssClass = "tabButtonBlueClick";
                Button_Beijing.CssClass = "tabButtonBlueUnClick";
                Button_Hangzhou.CssClass = "tabButtonBlueUnClick";
                Button_Guangzhou.CssClass = "tabButtonBlueUnClick";
                Button_Shenzhen.CssClass = "tabButtonBlueUnClick";
                cityName.Value = "上海市";
                break;
            case "Guangzhou":
                Button_Guangzhou.CssClass = "tabButtonBlueClick";
                Button_Beijing.CssClass = "tabButtonBlueUnClick";
                Button_Shanghai.CssClass = "tabButtonBlueUnClick";
                Button_Hangzhou.CssClass = "tabButtonBlueUnClick";
                Button_Shenzhen.CssClass = "tabButtonBlueUnClick";
                cityName.Value = "广州市";
                break;
            case "Shenzhen":
                Button_Shenzhen.CssClass = "tabButtonBlueClick";
                Button_Guangzhou.CssClass = "tabButtonBlueUnClick";
                Button_Beijing.CssClass = "tabButtonBlueUnClick";
                Button_Shanghai.CssClass = "tabButtonBlueUnClick";
                Button_Hangzhou.CssClass = "tabButtonBlueUnClick";
                cityName.Value = "深圳市";
                break;
        }
        GetShopInfo(0, 5);
        GetRecommandInfo();
    }
    //根据输入餐厅名模糊匹配选定地区的餐厅--店铺
    protected void Button_QueryByResturantName_Click(object sender, EventArgs e)
    {
        GetShopInfo(0, 5);
    }

    private void GetShopInfo(int str, int end)
    {
        ShopOperate so = new ShopOperate();
        //DataView dv = new DataView();
        DataTable dt = new DataTable();
        dt = so.QueryShopByName(TextBox_ResturantName.Text, cityName.Value);
        AspNetPager1.RecordCount = dt.Rows.Count;
        dt_page = Common.GetPageDataTable(dt, str, end);
        GridView_ShopInfo.DataSource = dt_page;
        GridView_ShopInfo.DataBind();

    }

    private void GetRecommandInfo()
    {
        if (dt_recommand != null)
        {
            DataView dv = dt_recommand.DefaultView;
            dv.RowFilter = "field='" + cityName.Value + "'";
            GridView1.DataSource = dv;
            GridView1.DataBind();
        }
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        GetShopInfo(AspNetPager1.StartRecordIndex - 1, AspNetPager1.EndRecordIndex);
    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        for (int i = dt_page.Rows.Count - 1; i >= 0; i--)
        {
            if (dt_recommand.Rows[e.RowIndex]["shopID"].ToString() == dt_page.Rows[i]["shopID"].ToString())
            {
                //删除数据库中数据
                WechatRecommandShopOperator wop = new WechatRecommandShopOperator();
                wop.Delete(Common.ToInt32(dt_recommand.Rows[e.RowIndex]["shopID"].ToString()));

                dt_recommand.Rows.RemoveAt(e.RowIndex);
                break;
            }

        }
        GridView1.DataSource = dt_recommand;
        GridView1.DataBind();

        GetShopInfo(0, 5);
        GetRecommandInfo();

    }

    protected void DropDown_SetRecommand_SelectedIndexChanged(object sender, EventArgs e)
    {

        int index = ((GridViewRow)(((DropDownList)sender).Parent.Parent)).RowIndex;
        DropDownList dp = (DropDownList)sender;

        switch (dp.SelectedItem.Text.Substring(2))
        {
            case "主推荐餐厅":
                if (mRecommand >= 1)
                {
                    ScriptManager.RegisterStartupScript(this.UpdatePanel1, GetType(), "js", "alert('主推荐餐厅个数为1，已达上限');", true);
                    dp.SelectedIndex = 0;
                    return;
                } break;
            case "次推荐餐厅":
                if (sRecommand >= 2)
                {
                    ScriptManager.RegisterStartupScript(this.UpdatePanel1, GetType(), "js", "alert('次推荐餐厅个数为2，已达上限');", true);
                    dp.SelectedIndex = 0;
                    return;
                } break;
            case "特色餐厅":
                if (tRecommand >= 2)
                {
                    ScriptManager.RegisterStartupScript(this.UpdatePanel1, GetType(), "js", "alert('特色餐厅个数为2，已达上限');", true);
                    dp.SelectedIndex = 0;
                    return;
                } break;
        }
        if (dp.SelectedValue != "-1")
        {
            for (int i = dt_recommand.Rows.Count - 1; i >= 0; i--)
            {
                if (dt_recommand.Rows[i]["shopID"].ToString() == dt_page.Rows[index]["shopID"].ToString())
                {
                    dt_recommand.Rows.RemoveAt(i); break;
                }
            }
            //Label_massage.Text = index.ToString();
            DataRow dr = dt_recommand.NewRow();
            DataRow drSlect = dt_page.Rows[index];
            dr["field"] = cityName.Value;
            dr["shopID"] = drSlect["shopID"];
            dr["shopName"] = drSlect["shopName"];
            dr["shopAddress"] = drSlect["shopAddress"];
            dr["shopTelephone"] = drSlect["shopTelephone"];
            dr["recommandType"] = Common.ToInt32(dp.SelectedValue);
            dr["recommandTypeName"] = dp.SelectedItem.Text.Substring(2);
            dt_recommand.Rows.Add(dr);
        }
        else
        {
            for (int i = dt_recommand.Rows.Count - 1; i >= 0; i--)
            {
                if (dt_recommand.Rows[i]["shopID"].ToString() == dt_page.Rows[index]["shopID"].ToString())
                {
                    dt_recommand.Rows.RemoveAt(i); break;
                }
            }
        }

        GridView1.DataSource = dt_recommand;
        GridView1.DataBind();
    }

    protected void Button_Save_Click(object sender, EventArgs e)
    {
        WechatRecommandShopOperator recoShopOper = new WechatRecommandShopOperator();
        int cityID = recoShopOper.GetCityID(cityName.Value);
        foreach (DataRow dr in dt_recommand.Rows)
        {
            WechatRecommandShopInfo reco = new WechatRecommandShopInfo();
            reco.cityID = cityID;
            reco.shopID = Common.ToInt32(dr["shopID"]);
            reco.operatorID = ((VAEmployeeLoginResponse)Session["UserInfo"]).employeeID;
            reco.recommandType = (VARecommandType)Common.ToInt32(dr["recommandType"]);
            reco.operateDate = DateTime.Now;

            recoShopOper.Insert(reco);
        }

        //Label_massage.Text = dt_page.Rows[0][2].ToString();
        //Page.ClientScript.RegisterStartupScript(GetType(), "js", "<script>alert('成功!');</script>");
        ScriptManager.RegisterStartupScript(this.UpdatePanel1, GetType(), "js", "<script>alert('保存成功!');</script>", false);
    }

    protected void GridView_ShopInfo_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    protected void GridView_ShopInfo_DataBound(object sender, EventArgs e)
    {
        for (int i = 0; i < dt_page.Rows.Count; i++)
        {
            (GridView_ShopInfo.Rows[i].Cells[5].FindControl("DropDown_SetRecommand") as DropDownList).SelectedIndex = Common.ToInt32(dt_page.Rows[i]["recommandType"].ToString()) + 1;
        }
    }

    private static int mRecommand, sRecommand, tRecommand;
    protected void GridView1_DataBound(object sender, EventArgs e)
    {
        mRecommand = sRecommand = tRecommand = 0;
        foreach (DataRow dr in dt_recommand.Rows)
        {
            switch (dr["recommandTypeName"].ToString())
            {
                case "主推荐餐厅": mRecommand++; break;
                case "次推荐餐厅": sRecommand++; break;
                case "特色餐厅": tRecommand++; break;
            }
        }
    }

    //protected void Page_Unload(object sender, EventArgs e)
    //{
    //    ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "js", "alert('22323');", true);
    //}
}