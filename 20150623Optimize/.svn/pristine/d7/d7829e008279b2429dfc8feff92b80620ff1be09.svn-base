using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.Model.HomeNew;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL.HomeNew;
using VAGastronomistMobileApp.SQLServerDAL;
using VAGastronomistMobileApp.WebPageDll;
using Web.Control.DDL;

public partial class OrderOptimization_ChannelDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //price.Attributes.Add("Onkeyup", "return price_Onkeyup(this)");
            if (Request.QueryString["id"] == null)
            {
                new CityDropDownList().BindCity(ddlCity, Common.ToString(Request.QueryString["cityID"]));
                BindTitle(firstTitleName, null, Common.ToInt32(Request.QueryString["cityID"]));
                BindModelType(modelType);
                modelType.SelectedValue = "1";
                firstTitleName.SelectedValue = "0";
                radListStatus.SelectedValue = "0";
                radListFlags.SelectedValue = "0";
            }
            else
            {
                int id = Common.ToInt32(Request.QueryString["id"]);
                Channel channel = new ChannelManager().Select(id);
                new CityDropDownList().BindCity(ddlCity, Common.ToString(channel.cityID));
                ddlCity.Enabled = false;
                BindTitle(firstTitleName, Common.ToString(channel.firstTitleID), channel.cityID);
                modelType.Enabled = false;
                pageName.Text = channel.channelName;
                price.Text = Common.ToString(channel.price);
                if (channel.status)
                {
                    radListStatus.SelectedValue = "1";
                }
                else
                {
                    radListStatus.SelectedValue = "0";
                }
                radListFlags.SelectedValue = Common.ToString(channel.sign);
                content.Text = channel.content;
                BindModelType(modelType);
                modelType.SelectedValue = Common.ToString(channel.modelType);
            }

        }
    }

    private void BindModelType(DropDownList modelType)
    {
        // var cityOperate = new CityOperate();
        modelType.DataTextField = "modelName";
        modelType.DataValueField = "modelId";
        modelType.DataBind();
        modelType.Items.Add(new ListItem("新品", "1"));
        modelType.Items.Add(new ListItem("特价", "2"));
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
        ddl_Title.Items.Add(new ListItem("无", "0"));
        if (titleID != null)
        {
            ddl_Title.SelectedValue = titleID;
        }
    }

    protected List<TitleViewModel> GetHandleTitle(int cityID, out int NonAdTitleID)
    {
        return TitleManager.SelectHandleTitle(cityID, out NonAdTitleID);
    }

    protected void ddlCity_SelectedIndexChanged(object sender, EventArgs e)
    {
        int cityID = Common.ToInt32(ddlCity.SelectedValue);
        BindTitle(firstTitleName, null, cityID);
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("ChannelManager.aspx?cityID=" + Request.QueryString["cityID"]);
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (CheckValue())
        {
            int result = 0;
            Channel channel = ChannelCreate();
            if (Request.QueryString["id"] == null)
            {
                result = new ChannelManager().Insert(channel);
                if (result != 0)
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('新建成功！');window.location.href='ChannelManager.aspx?cityID=" + ddlCity.SelectedValue + "'</script>");
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('新建失败！');</script>");
                }
            }
            else
            {
                channel.id = Common.ToInt32(Request.QueryString["id"]);
                result = new ChannelManager().Update(channel);
                if (result != 0)
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('修改成功！');window.location.href='ChannelManager.aspx?cityID=" + ddlCity.SelectedValue + "'</script>");
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('修改失败！');</script>");
                }
            }
        }
    }

    public bool CheckValue()
    {
        if (string.IsNullOrEmpty(pageName.Text.Trim()))
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('页面名称不能为空！');</script>");
            return false;
        }
        return true;
    }

    private Channel ChannelCreate()
    {
        Channel channel = new Channel()
        {
            channelName = pageName.Text,
            cityID = Common.ToInt32(ddlCity.SelectedValue),
            price = Common.ToDouble(price.Text),
            firstTitleID = Common.ToInt32(firstTitleName.SelectedValue),
            sign = Common.ToInt32(radListFlags.SelectedValue),
            content = content.Text,
            createTime = DateTime.Now,
            isDelete = false,
            modelType = Common.ToInt32(modelType.SelectedValue)
        };

        if (radListStatus.SelectedValue == "0")
        {
            channel.status = false;
        }
        else
        {
            channel.status = true;
        }

        return channel;
    }
}