using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.DBUtility;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.Model.HomeNew;
using VAGastronomistMobileApp.SQLServerDAL.HomeNew;
using VAGastronomistMobileApp.WebPageDll;
using Web.Control.DDL;

public partial class HomeNew_HomeTitleAdd : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["cityID"] != null)
            {
                string cityID = Request.QueryString["cityID"];
                new CityDropDownList().BindCity(ddlCity, cityID);
                string titleID = Request.QueryString["titleID"];
                BindTitle(TextTitleName, titleID, Common.ToInt32(cityID));
                int firstTitleID = Convert.ToInt32(Request.QueryString["titleID"]);
                // int firstTitleStatus = GetTitleStatus(firstTitleID);
                TextTitleName.Text = TitleManager.QueryTitle(firstTitleID).TitleName;
                //radListStatus.SelectedValue = "0";
                //if (firstTitleStatus == 2)
                //{
                BindRule(ddlRule);
                //}
                //else
                //{
                //    BindNonAdRule(ddlRule);
                //}

                ddlRule.SelectedValue = "1";
            }
        }
    }

    public void BindTitle(DropDownList ddl_Title, string titleID, int cityID)
    {
        int NonAdTitleID;
        List<TitleViewModel> data = GetHandleTitle(cityID, out NonAdTitleID);
        data = data.FindAll(t => t.titleID != NonAdTitleID).ToList();
        ddl_Title.DataSource = data;
        ddl_Title.DataTextField = "titleName";
        ddl_Title.DataValueField = "titleId";
        ddl_Title.DataBind();
        if (Convert.ToInt32(titleID) != NonAdTitleID && titleID !="0")
        {
            ddl_Title.SelectedValue = titleID;
        }
    }
    protected List<TitleViewModel> GetHandleTitle(int cityID, out int NonAdTitleID)
    {
        return TitleManager.SelectHandleTitle(cityID, out NonAdTitleID);
    }


    private int GetTitleStatus(int firstTitleID)
    {
        Title title = TitleManager.QueryTitle(firstTitleID);
        return title.Type;
    }

    public void BindRule(DropDownList ddlRule)
    {
        var cityOperate = new CityOperate();
        //List<CityViewModel> data = cityOperate.GetHandleCity();
        //ddlRule.DataSource = data;
        ddlRule.DataTextField = "ruleName";
        ddlRule.DataValueField = "ruleId";
        ddlRule.DataBind();
        ddlRule.Items.Add(new ListItem("手动排序", "1"));
        ddlRule.Items.Add(new ListItem("由近到远", "2"));
    }

    public void BindNonAdRule(DropDownList ddlRule)
    {
        var cityOperate = new CityOperate();
        ddlRule.DataTextField = "ruleName";
        ddlRule.DataValueField = "ruleId";
        ddlRule.DataBind();
        ddlRule.Items.Add(new ListItem("智能排序", "1"));
        ddlRule.Items.Add(new ListItem("由近到远", "2"));
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("HomeSubTitle.aspx?cityID=" + Request.QueryString["cityID"] + "&titleID=" + Request.QueryString["titleID"]);
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        SubTitle subTitle = SubTitleGenerator();
        int result = 0;
        string cityID = ddlCity.SelectedValue;
        string titleID = TextTitleName.SelectedValue;
        if (string.IsNullOrEmpty(titleID))
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('新建二级栏目时，一级栏目不能为空！');</script>");
        }
        else
        {
            //string firstStatus = Common.ToString(TitleManager.SelectTitleByID(subTitle.FirstTitleID).Rows[0]["status"]);

            //if (firstStatus == "0" && subTitle.Status == 1)
            //{
            //    Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('请先设置一级栏目上线，才能设置二级栏目上线！');</script>");
            //}
            //else
            //{
            if (TitleManager.SubIndexClash(subTitle.TitleIndex, subTitle.FirstTitleID))
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('新建失败,二级栏目排序冲突！');</script>");
            }
            else
            {
                result = SubTitleManager.Insert(subTitle);
                if (result == 1)
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('新建成功！');window.location.href='HomeSubTitle.aspx?cityID=" + ddlCity.SelectedValue + "&titleID=" + TextTitleName.SelectedValue + "'</script>");
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('新建失败！');</script>");
                }
            }

            //}
        }
    }

    private SubTitle SubTitleGenerator()
    {
        SubTitle subTitle = new SubTitle()
        {
            FirstTitleID = Common.ToInt32(TextTitleName.SelectedValue),
            TitleName = TextSubTitleName.Text,
            TitleIndex = Common.ToInt32(TextTitleIndex.Text),
            RuleType = Common.ToInt32(ddlRule.SelectedValue),
            //Status = Common.ToInt32(radListStatus.SelectedValue),
            CreateTime = DateTime.Now,
            CreateBy = Common.ToInt32(((VAEmployeeLoginResponse)HttpContext.Current.Session["UserInfo"]).employeeID),
            IsDelete = false
        };
        return subTitle;
    }
    protected void ddlCity_SelectedIndexChanged(object sender, EventArgs e)
    {
        int cityID = Convert.ToInt32(ddlCity.SelectedValue);
        BindTitle(TextTitleName, "0", cityID);
    //{
    //    int NonAdTitleID;
    //    List<TitleViewModel> data = GetHandleTitle(cityID, out NonAdTitleID);
    //    data = data.FindAll(t => t.titleID != NonAdTitleID).ToList();
    //    ddl_Title.DataSource = data;
    //    ddl_Title.DataTextField = "titleName";
    //    ddl_Title.DataValueField = "titleId";
    //    ddl_Title.DataBind();
    //    if (Convert.ToInt32(titleID) != NonAdTitleID && titleID !="0")
    //    {
    //        ddl_Title.SelectedValue = titleID;
    //    }
    //}
    }
}