using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using VAGastronomistMobileApp.WebPageDll;
using VAGastronomistMobileApp.Model;
using System.Data;

public partial class PointsManage_index : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {       
    }

    /// <summary>
    /// 根据客户端返回的参数【c&t】查询服务员基本信息
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    public static string QueryEmployeeByCookie(string cookie)
    {
        EmployeePointOperate _Operate = new EmployeePointOperate();
        string employeeInfo = _Operate.ClientQueryEmployeeByCookie(cookie);
        return "{" + "\"employeeInfo\":[" + employeeInfo + "]}";
    }
}