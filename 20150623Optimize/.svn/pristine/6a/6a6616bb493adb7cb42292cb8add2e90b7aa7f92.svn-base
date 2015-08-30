using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.Model;
using System.Data;
using VAGastronomistMobileApp.WebPageDll;

public partial class WeChatPlatManage_HotMenu : System.Web.UI.Page
{

    private static List<WechatHotMenuInfo> hotList = new List<WechatHotMenuInfo>(); //热菜列表
    private static DataTable dtStatis = new DataTable(); //统计数据

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //统计出各个地区的热菜
            initStatisticData();
        }
    }

    private void initStatisticData()
    {
        WechatHotMenuOperate hmo = new WechatHotMenuOperate();
        dtStatis = hmo.GetHotMenuStatisticInfo();
        //在dt里按城市分组..杭州 北京 上海 广州
        //dt.DefaultView.RowFilter = "cityName = '杭州'";
        DataView dvHz = dtStatis.DefaultView;
        dvHz.RowFilter = "cityName = '杭州市'";
        GridView_Hangzhou.DataSource = dvHz;
        GridView_Hangzhou.DataBind();

        DataView dvBj = dtStatis.DefaultView;
        dvBj.RowFilter = "cityName = '北京市'";
        GridView_Beijing.DataSource = dvBj;
        GridView_Beijing.DataBind();

        DataView dvSh = dtStatis.DefaultView;
        dvSh.RowFilter = "cityName = '上海市'";
        GridView_Shanghai.DataSource = dvSh;
        GridView_Shanghai.DataBind();

        DataView dvGz = dtStatis.DefaultView;
        dvGz.RowFilter = "cityName = '广州市'";
        GridView_Guangzhou.DataSource = dvGz;
        GridView_Guangzhou.DataBind();

        DataView dvSz = dtStatis.DefaultView;
        dvSz.RowFilter = "cityName = '深圳市'";
        GridView_ShenZhen.DataSource = dvSz;
        GridView_ShenZhen.DataBind();

    }

    protected void chkSet_CheckedChanged(object sender, EventArgs e)
    {
        GridView gv = (GridView)((sender as CheckBox).Parent.Parent.Parent.Parent);
        int index = ((GridViewRow)((sender as CheckBox).Parent.Parent)).RowIndex;
        for (int i = 0; i < gv.Rows.Count; i++)
        {
            if (i != index)
                (gv.Rows[i].Cells[6].FindControl("chkSet") as CheckBox).Checked = false;
        }

        //记录下选中的信息
        int dishID = Common.ToInt32(gv.Rows[index].Cells[0].Text);
        if ((sender as CheckBox).Checked)
        {
            WechatHotMenuInfo hotMenuInfo = new WechatHotMenuInfo();
            DataRow[] dr = dtStatis.Select("DishID = " + dishID);
            if (dr.Length > 0)
            {
                hotMenuInfo.DishID = dishID;
                hotMenuInfo.saleAmount = Common.ToInt32(dr[0].ItemArray[1]);
                hotMenuInfo.cityName = dr[0].ItemArray[2].ToString();
                hotMenuInfo.shopName = dr[0].ItemArray[3].ToString();
                hotMenuInfo.shopAddress = dr[0].ItemArray[4].ToString();
                hotMenuInfo.ImageFolder = dr[0].ItemArray[5].ToString();
                hotMenuInfo.DishName = dr[0].ItemArray[6].ToString();
                hotMenuInfo.DishPrice = float.Parse(dr[0].ItemArray[7].ToString());
                hotMenuInfo.ImageName = dr[0].ItemArray[8].ToString();
                hotMenuInfo.setDateTime = DateTime.Now.ToString("yyyyMMdd HH:mm:ss");
                hotMenuInfo.operaterID = ((VAEmployeeLoginResponse)Session["UserInfo"]).employeeID;

                hotList.Add(hotMenuInfo);
            }
        }
        else
        {
            for (int i = 0; i < hotList.Count; i++)
            {
                if (hotList[i].DishID == dishID)
                    hotList.RemoveAt(i);
            }
        }
    }

    protected void Button_Save_Click(object sender, EventArgs e)
    {
        bool bSuc = false;
        if (hotList.Count > 0)
        {
            WechatHotMenuOperate hmo = new WechatHotMenuOperate();
            foreach (WechatHotMenuInfo hotMenu in hotList)
            {
                if (hmo.InsertHotMenu(hotMenu) == 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "js", "alert('保存失败!');", true);
                    bSuc = true;
                }
            }
            if (!bSuc)
                ScriptManager.RegisterStartupScript(this, GetType(), "js", "alert('保存成功!');", true);
        }
    }

    protected void GridView_Hangzhou_DataBound(object sender, EventArgs e)
    {
        //获取设置的热菜dishid,显示默认设置 
        WechatHotMenuOperate hmo = new WechatHotMenuOperate();
        int dishID = hmo.GetDishIDByCityName("杭州市");
        for (int i = 0; i < GridView_Hangzhou.Rows.Count; i++)
        {
            if (Common.ToInt32(GridView_Hangzhou.Rows[i].Cells[0].Text) == dishID)
            {
                (GridView_Hangzhou.Rows[i].Cells[6].FindControl("chkSet") as CheckBox).Checked = true;
                break;
            }
        }

    }
    protected void GridView_Beijing_DataBound(object sender, EventArgs e)
    {
        //获取设置的热菜dishid,显示默认设置 
        WechatHotMenuOperate hmo = new WechatHotMenuOperate();
        int dishID = hmo.GetDishIDByCityName("北京市");
        for (int i = 0; i < GridView_Hangzhou.Rows.Count; i++)
        {
            if (Common.ToInt32(GridView_Hangzhou.Rows[i].Cells[0].Text) == dishID)
            {
                (GridView_Beijing.Rows[i].Cells[6].FindControl("chkSet") as CheckBox).Checked = true;
                break;
            }
        }
    }
    protected void GridView_Shanghai_DataBound(object sender, EventArgs e)
    {
        //获取设置的热菜dishid,显示默认设置 
        WechatHotMenuOperate hmo = new WechatHotMenuOperate();
        int dishID = hmo.GetDishIDByCityName("上海市");
        for (int i = 0; i < GridView_Hangzhou.Rows.Count; i++)
        {
            if (Common.ToInt32(GridView_Hangzhou.Rows[i].Cells[0].Text) == dishID)
            {
                (GridView_Beijing.Rows[i].Cells[6].FindControl("chkSet") as CheckBox).Checked = true;
                break;
            }
        }
    }
    protected void GridView_Guangzhou_DataBound(object sender, EventArgs e)
    {
        //获取设置的热菜dishid,显示默认设置 
        WechatHotMenuOperate hmo = new WechatHotMenuOperate();
        int dishID = hmo.GetDishIDByCityName("广州市");
        for (int i = 0; i < GridView_Hangzhou.Rows.Count; i++)
        {
            if (Common.ToInt32(GridView_Hangzhou.Rows[i].Cells[0].Text) == dishID)
            {
                (GridView_Guangzhou.Rows[i].Cells[6].FindControl("chkSet") as CheckBox).Checked = true;
                break;
            }
        }
    }
    protected void GridView_ShenZhen_DataBound(object sender, EventArgs e)
    {
        //获取设置的热菜dishid,显示默认设置 
        WechatHotMenuOperate hmo = new WechatHotMenuOperate();
        int dishID = hmo.GetDishIDByCityName("深圳市");
        for (int i = 0; i < GridView_Hangzhou.Rows.Count; i++)
        {
            if (Common.ToInt32(GridView_Hangzhou.Rows[i].Cells[0].Text) == dishID)
            {
                (GridView_ShenZhen.Rows[i].Cells[6].FindControl("chkSet") as CheckBox).Checked = true;
                break;
            }
        }
    }
}