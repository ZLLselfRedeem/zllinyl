using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.WebPageDll;
using System.IO;
using VAGastronomistMobileApp.DBUtility;
using System.Data;
using System.Text;

public partial class Left : System.Web.UI.Page
{
    public string menu;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetAuthorityByName();//根据用户名获取菜单权限
        }
    }

    /// <summary>
    /// 根据用户id查询用户角色。根据用户角色，查询用户权限。根据用户权限，画menu
    /// </summary>
    protected void GetAuthorityByName()
    {
        DataTable dt = null;
        if (Session["UserInfo"] != null)
        {
            string userName = ((VAEmployeeLoginResponse)Session["UserInfo"]).userName;
            if (userName == "viewalloc")
            {
                AuthorityOperate authorityOperate = new AuthorityOperate();
                dt = authorityOperate.QueryAuthority();
                GetAuthority(dt);
            }
            else
            {
                EmployeeOperate employeeOperate = new EmployeeOperate();
                dt = employeeOperate.QueryEmployeeAuthortiy(userName);
                GetAuthority(dt);
            }
        }
    }


    protected void GetAuthority(DataTable dt)
    {
        DataRow[] dr = dt.Select("AuthorityRank=0 and AuthorityType='page'");
        StringBuilder sb = new StringBuilder();

        for (int i = 0; i < dr.Length; i++)
        {
            sb.Append("<div>");
            sb.Append("<div class=\"cate1\"  id=\"cate_sort_" + i + "\" onclick=\"ShowThis(" + i + ");\" style=\"cursor: pointer;\">");
            sb.Append("<div class=\"cate2\">");
            //sb.Append("<span>");
            //sb.Append("<img alt=\"\" src=\"images/close.png\" id=\"sort_" + i + "_img_1\"></span> <span>");
            sb.Append("<img alt=\"\" src=\"images/close.png\" id=\"sort_" + i + "_img_1\">");
            sb.Append(dr[i]["AuthorityName"]);
            //sb.Append("</span>");
            sb.Append("</div>");
            sb.Append("<div class=\"cate3\">");
            //sb.Append("<span>");
            //sb.Append("<img alt=\"\" style=\"cursor: pointer;\" src=\"images/plus.gif\" id=\"sort_" + i + "_img_2\"></span>");
            sb.Append("<img alt=\"\" style=\"cursor: pointer;\" src=\"images/plus.gif\" id=\"sort_" + i + "_img_2\">");
            sb.Append("</div>");
            sb.Append("</div>");
            //sb.Append("<div style=\"display: none;\" class=\"sort\" id=\"sort_" + i + "\">");
            sb.Append("<div style=\"display: none;\" class=\"sort\" id=\"sort_" + i + "\">");
            sb.Append("<ul>");
            DataView dv = dt.DefaultView;
            dv.RowFilter = "AuthorityRank=" + dr[i]["AuthorityID"] + " and  AuthorityType='page'";
            for (int j = 0; j < dv.Count; j++)
            {
                sb.Append("<li>");
                //sb.Append("<img alt=\"\" style=\"vertical-align: middle\" src=\"images/arrow2.gif\">&nbsp;");
                sb.Append("<a target=\"mainFrame\" href=\"" + dv[j]["AuthorityURL"] + "\">" + dv[j]["AuthorityName"] + "</a>");
                sb.Append("</li>");
            }
            sb.Append("</ul>");
            sb.Append("</div>");
            sb.Append("</div>");
        }
        menu = sb.ToString();
    }


   

}