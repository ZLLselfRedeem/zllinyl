using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.SQLServerDAL;
using VAGastronomistMobileApp.WebPageDll;

public partial class HomeNew_CityManage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindCityList();
        }
    }

    /// <summary>
    /// 绑定城市数据源
    /// </summary>
    protected void BindCityList()
    {
        CityManager manager = new CityManager();
        DataTable dtCity = manager.SelectCity("");
        GridView_City.DataSource = dtCity.DefaultView;
        GridView_City.DataBind();
    }

    protected void GridView_City_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;//转换为按钮类型，获取其所在的行的索引
        int cityID = Convert.ToInt32(GridView_City.DataKeys[index].Values["cityID"].ToString());
        string status = GridView_City.DataKeys[index].Values["status"].ToString();
        string isClientShow = GridView_City.DataKeys[index].Values["isClientShow"].ToString();
        string cityName = GridView_City.DataKeys[index].Values["cityName"].ToString();
        switch (e.CommandName.ToString())
        {
            case "cityUpdate":
                if (status == "2")
                {
                    if (cityID == 87)
                    {
                        Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('退出失败！杭州市不能退出入驻！');</script>");
                    }
                    else
                    {
                        bool result = CityExit(cityID);
                        if (result)
                        {
                            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('" + cityName + "已成功退出');</script>");
                        }
                        else
                        {
                            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('" + cityName + "退出失败');</script>");
                        }
                    }
                }
                else
                {
                    bool result = CityEnter(cityID);
                    Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('" + cityName + "已成功入驻');</script>");
                }
                break;
            case "clientUpdate":
                CityManager manager = new CityManager();
                if (isClientShow == "True")
                {
                    manager.SetClientStatus(0, cityID);
                    Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('" + cityName + "客户端已成功下线');</script>");
                }
                else
                {
                    manager.SetClientStatus(1, cityID);
                    Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('" + cityName + "客户端已成功上线');</script>");
                }
                break;
            default:
                break;
        }
        Button_QueryCity_Click(null, null);
    }

    /// <summary>
    /// 城市进行入驻
    /// </summary>
    /// <param name="cityID"></param>
    /// <param name="manager"></param>
    private bool CityEnter(int cityID)
    {
        bool result = false;
        using (TransactionScope ts = new TransactionScope())
        {
            CityManager manager = new CityManager();
            result = manager.SetCityStatus(2, cityID);     //设置城市入驻
            if (result)
            {
                if (!manager.CityOnceOpened(cityID))            //检验该城市是否是第一次进行入驻
                {
                    int createBy = ((VAEmployeeLoginResponse)HttpContext.Current.Session["UserInfo"]).employeeID;
                    result = manager.AddNomalTitle(cityID, createBy);    // 增加一级“全部”栏目和二级“全城”和“附近”栏目
                }
            }
            if (result)
            {
                ts.Complete();
            }
        }
        return result;
    }

    /// <summary>
    /// 城市退出入驻
    /// </summary>
    /// <param name="cityID"></param>
    /// <param name="manager"></param>
    /// <returns></returns>
    private bool CityExit(int cityID)
    {
        bool result1 = false;
        bool result2 = false;
        bool result3 = false;

        using (TransactionScope ts = new TransactionScope())
        {
            CityManager manager = new CityManager();
            result1 = manager.SetCityStatus(1, cityID);    // 城市退出
            result2 = manager.SetClientStatus(0, cityID);  // 城市客户端下线
            result3 = manager.DownlineAll(cityID);         //把该城市下的所有一级栏目和所有广告都下线
            ts.Complete();
        }

        if (result1 && result2 && result3)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    protected void GridView_City_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

    }
    protected void GridView_City_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void Button_QueryCity_Click(object sender, EventArgs e)
    {
        string cityName = TextBox_CityName.Text.Trim().ToString();
        DataTable dtCity = new DataTable();
        if (!string.IsNullOrEmpty(cityName))
        {
            dtCity = GetNewDataTable(cityName);
        }
        else
        {
            CityManager manager = new CityManager();
            dtCity = manager.SelectCity("");
        }
        GridView_City.DataSource = dtCity.DefaultView;
        GridView_City.DataBind();
    }

    protected DataTable GetNewDataTable(string condition)
    {
        DataTable dtCity = new CityManager().SelectCity("");
        DataTable newCity = new DataTable();
        newCity = dtCity.Clone();
        string likecityName = "cityName like '%" + condition + "%'";
        DataRow[] dr = dtCity.Select(likecityName);
        //dr.OrderByDescending()
        for (int i = 0; i < dr.Length; i++)
        {
            newCity.ImportRow((DataRow)dr[i]);
        }
        return newCity;
    }

    protected string UpdateConfirmMsg(int status, string cityName)
    {
        string reStr = null;
        if (cityName == "杭州市")
        {
            reStr = string.Empty;
        }
        else
        {
            if (status == 1)
            {
                reStr = string.Format("return confirm('确定{0}入驻吗？')", cityName);
            }
            else
            {
                reStr = string.Format("return confirm('城市退出，将会使该城市的所有栏目和广告在客户端下线，确定{0}退出吗？')", cityName);
            }
        }
        return reStr;
    }

    protected string ClientConfirmMsg(int status, string isClientShow, string cityName)
    {
        string reStr = null;
        if (status == 2)
        {
            if (isClientShow == "True")
            {
                reStr = string.Format("return confirm('确定{0}客户端下线吗？')", cityName);
            }
            else
            {
                reStr = string.Format("return confirm('确定{0}客户端上线吗？')", cityName);
            }
        }
        return reStr;
    }
}