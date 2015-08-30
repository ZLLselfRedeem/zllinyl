using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.WebPageDll;
using VAGastronomistMobileApp.Model;
using System.Data;
using Web.Control;
using Web.Control.DDL;

/// <summary>
/// 下载页访问统计
/// 20140117 jinyanni
/// </summary>
public partial class StatisticalStatement_QRCodePageViewStatistics : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            new CityDropDownList().BindCity(DropDownList_City);
            Button_LargePageCount_Click(Button_10, null);//分页选中10
            GetQRCodeShopType();
            GetCompanyInfo();
            GetShopInfo();
            Button_day_Click(Button_1day, null);//默认选中今天
        }
    }

    /// <summary>
    /// 绑定所有二维码类型
    /// </summary>
    protected void GetQRCodeShopType()
    {
        QRCodeOperate _QRCode = new QRCodeOperate();
        DataTable dtQRCode = _QRCode.QueryQRCodeType();
        DropDownList_Type.DataSource = dtQRCode;
        DropDownList_Type.DataTextField = "name";
        DropDownList_Type.DataValueField = "id";
        DropDownList_Type.DataBind();
        DropDownList_Type.Items.Add(new ListItem("所有类别", "0"));
        DropDownList_Type.SelectedValue = "0";
    }

    /// <summary>
    /// 获取公司信息
    /// </summary>
    protected void GetCompanyInfo()
    {
        CompanyOperate operate = new CompanyOperate();
        int companyId = operate.GetCompanyId(Common.ToInt32(Request.QueryString["id"]));
        List<CompanyViewModel> employeeCompany = new EmployeeConnShopOperate().QueryEmployeeCompany(SessionHelper.GetCurrectSessionEmployeeId(), Common.ToInt32(DropDownList_City.SelectedValue));
        DropDownList_Company.DataSource = employeeCompany;
        DropDownList_Company.DataTextField = "CompanyName";
        DropDownList_Company.DataValueField = "CompanyID";
        DropDownList_Company.DataBind();
        DropDownList_Company.Items.Add(new ListItem("所有公司", "0"));
        DropDownList_Company.SelectedValue = companyId.ToString();//默认选择所有公司
    }

    /// <summary>
    /// 获取门店信息
    /// </summary>
    protected void GetShopInfo()
    {
        if (DropDownList_Company.Items.Count <= 0)
        {
            return;
        }
        DropDownList_Shop.Items.Clear();
        EmployeeConnShopOperate employeeConnShopOperate = new EmployeeConnShopOperate();
        List<VAEmployeeShop> employeeShop = new List<VAEmployeeShop>();
        VAEmployeeLoginResponse vAEmployeeLoginResponse = (VAEmployeeLoginResponse)Session["UserInfo"];
        int employeeID = vAEmployeeLoginResponse.employeeID;
        int companyID = Common.ToInt32(DropDownList_Company.SelectedValue);
        employeeShop = employeeConnShopOperate.QueryEmployeeShopByCompanyAndEmplyee(employeeID, companyID);
        DropDownList_Shop.DataSource = employeeShop;
        DropDownList_Shop.DataTextField = "shopName";
        DropDownList_Shop.DataValueField = "shopID";
        DropDownList_Shop.DataBind();
        DropDownList_Shop.Items.Add(new ListItem("所有门店", "0"));
        DropDownList_Shop.SelectedValue = "0";//默认选择所有门店
        BindPVData(0, Common.ToInt32(Label_LargePageCount.Text));
    }

    /// <summary>
    /// 选择类别
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void DropDownList_Type_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindPVData(0, Common.ToInt32(Label_LargePageCount.Text));
    }

    /// <summary>
    /// 选择公司
    /// </summary>
    protected void DropDownList_Company_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetShopInfo();//动态选择门店
        BindPVData(0, Common.ToInt32(Label_LargePageCount.Text));
    }

    /// <summary>
    /// 选择店铺
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void DropDownList_Shop_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindPVData(0, Common.ToInt32(Label_LargePageCount.Text));
    }

    /// <summary>
    /// 查询按钮
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button_QRCodePV_Click(object sender, EventArgs e)
    {
        BindPVData(0, Common.ToInt32(Label_LargePageCount.Text));
    }

    /// <summary>
    /// 按查询条件获取数据
    /// </summary>
    /// <param name="str"></param>
    /// <param name="end"></param>
    protected void BindPVData(int str, int end)
    {
        AspNetPager1.PageSize = Common.ToInt32(Label_LargePageCount.Text);
        QRCodePVOperate _QRCodePV = new QRCodePVOperate();
        DateTime timeFrom = Common.ToDateTime(TextBox_visitTimeStr.Text + " 00:00:00");
        DateTime timeTo = Common.ToDateTime(TextBox_visitTimeEnd.Text + " 23:59:59");

        int typeId = Common.ToInt32(DropDownList_Type.SelectedValue);
        int companyId = Common.ToInt32(DropDownList_Company.SelectedValue);//默认这个值为0
        int shopId = Common.ToInt32(DropDownList_Shop.SelectedValue);

        DataTable dt = _QRCodePV.QueryQRCodePV(typeId, companyId, shopId, timeFrom, timeTo);

        if (dt != null && dt.Rows.Count != 0)
        {
            Label_massage.Text = "";
            int tableCount = dt.Rows.Count;//总数
            Label_OrderCount.Text = tableCount.ToString();
            AspNetPager1.RecordCount = tableCount;
            DataTable dt_page = Common.GetPageDataTable(dt, str, end);
            GridView_QRCodePV.DataSource = dt_page;
        }
        else
        {
            Label_massage.Text = "暂无符合条件的数据";
            this.GridView_QRCodePV.DataSource = null;
        }
        this.GridView_QRCodePV.DataBind();
    }

    /// <summary>
    /// 分页操作
    /// </summary>
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        BindPVData(AspNetPager1.StartRecordIndex - 1, AspNetPager1.EndRecordIndex);
    }

    /// <summary>
    /// 选择统计日期，添加样式
    /// </summary>
    protected void Button_day_Click(object sender, EventArgs e)
    {
        TextBox_visitTimeStr.Text = "";
        TextBox_visitTimeEnd.Text = "";
        Button Button = (Button)sender;
        switch (Button.CommandName)
        {
            case "1"://
                Button_1day.CssClass = "tabButtonBlueClick";//今天
                Button_yesterday.CssClass = "tabButtonBlueUnClick";//昨天
                Button_7day.CssClass = "tabButtonBlueUnClick";//最近一周
                Button_14day.CssClass = "tabButtonBlueUnClick";//最近14天
                Button_30day.CssClass = "tabButtonBlueUnClick";//最近30天
                Button_self.CssClass = "tabButtonBlueUnClick";//自定义
                TextBox_visitTimeStr.Text = DateTime.Now.ToString("yyyy-MM-dd");//日期控件显示当前时间
                TextBox_visitTimeEnd.Text = DateTime.Now.ToString("yyyy-MM-dd");
                break;
            case "yesterday":
                Button_1day.CssClass = "tabButtonBlueUnClick";//今天
                Button_yesterday.CssClass = "tabButtonBlueClick";//昨天
                Button_7day.CssClass = "tabButtonBlueUnClick";//最近一周
                Button_14day.CssClass = "tabButtonBlueUnClick";//最近14天
                Button_30day.CssClass = "tabButtonBlueUnClick";//最近30天
                Button_self.CssClass = "tabButtonBlueUnClick";//自定义
                TextBox_visitTimeStr.Text = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
                TextBox_visitTimeEnd.Text = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
                break;
            case "7":
                Button_1day.CssClass = "tabButtonBlueUnClick";//今天
                Button_yesterday.CssClass = "tabButtonBlueUnClick";//昨天
                Button_7day.CssClass = "tabButtonBlueClick";//最近一周
                Button_14day.CssClass = "tabButtonBlueUnClick";//最近14天
                Button_30day.CssClass = "tabButtonBlueUnClick";//最近30天
                Button_self.CssClass = "tabButtonBlueUnClick";//自定义
                TextBox_visitTimeStr.Text = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
                TextBox_visitTimeEnd.Text = DateTime.Now.ToString("yyyy-MM-dd");
                break;
            case "14":
                Button_1day.CssClass = "tabButtonBlueUnClick";//今天
                Button_yesterday.CssClass = "tabButtonBlueUnClick";//昨天
                Button_7day.CssClass = "tabButtonBlueUnClick";//最近一周
                Button_14day.CssClass = "tabButtonBlueClick";//最近14天
                Button_30day.CssClass = "tabButtonBlueUnClick";//最近30天
                Button_self.CssClass = "tabButtonBlueUnClick";//自定义
                TextBox_visitTimeStr.Text = DateTime.Now.AddDays(-14).ToString("yyyy-MM-dd");
                TextBox_visitTimeEnd.Text = DateTime.Now.ToString("yyyy-MM-dd");
                break;
            case "30":
                Button_1day.CssClass = "tabButtonBlueUnClick";//今天
                Button_yesterday.CssClass = "tabButtonBlueUnClick";//昨天
                Button_7day.CssClass = "tabButtonBlueUnClick";//最近一周
                Button_14day.CssClass = "tabButtonBlueUnClick";//最近14天
                Button_30day.CssClass = "tabButtonBlueClick";//最近30天
                Button_self.CssClass = "tabButtonBlueUnClick";//自定义
                TextBox_visitTimeStr.Text = DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd");
                TextBox_visitTimeEnd.Text = DateTime.Now.ToString("yyyy-MM-dd");
                break;
            case "self":
                Button_1day.CssClass = "tabButtonBlueUnClick";//今天
                Button_yesterday.CssClass = "tabButtonBlueUnClick";//昨天
                Button_7day.CssClass = "tabButtonBlueUnClick";//最近一周
                Button_14day.CssClass = "tabButtonBlueUnClick";//最近14天
                Button_30day.CssClass = "tabButtonBlueUnClick";//最近30天
                Button_self.CssClass = "tabButtonBlueClick";//自定义
                break;
            default:
                break;
        }
        BindPVData(0, Common.ToInt32(Label_LargePageCount.Text));
    }

    /// <summary>
    /// 自定义选择时间日期
    /// </summary>
    protected void TextBox_visitTimeStr_TextChanged(object sender, EventArgs e)
    {
        BindPVData(0, Common.ToInt32(Label_LargePageCount.Text));
        Button_1day.CssClass = "tabButtonBlueUnClick";//今天
        Button_yesterday.CssClass = "tabButtonBlueUnClick";//昨天
        Button_7day.CssClass = "tabButtonBlueUnClick";//最近一周
        Button_14day.CssClass = "tabButtonBlueUnClick";//最近14天
        Button_30day.CssClass = "tabButtonBlueUnClick";//最近30天
        Button_self.CssClass = "tabButtonBlueClick";//自定义
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
        BindPVData(0, Common.ToInt32(Label_LargePageCount.Text));
    }
    protected void DropDownList_City_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetCompanyInfo();
        BindPVData(0, 10);
    }
}