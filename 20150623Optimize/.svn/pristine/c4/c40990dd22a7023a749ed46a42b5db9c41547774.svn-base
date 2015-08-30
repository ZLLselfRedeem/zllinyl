using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.WebPageDll;
using VAGastronomistMobileApp.Model;
using System.Data;

public partial class SystemConfig_ClientImageSizeConfig : System.Web.UI.Page
{
    ClientImageSizeOperate clientImageSizeOperate = new ClientImageSizeOperate();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindQueryDDL();
            BindList("", "", "", 0, 15);
        }
    }
    private void BindQueryDDL()
    {
        string imageTypeName = "";
        this.ddlImageTypeQuery.Items.Clear();
        this.ddlImageTypeQuery.Items.Add(new ListItem("==请选择==", "NA"));
        foreach (int imageType in Enum.GetValues(typeof(VAImageType)))
        {
            imageTypeName = Enum.GetName(typeof(VAImageType), imageType);
            this.ddlImageTypeQuery.Items.Add(new ListItem(imageTypeName, imageType.ToString()));
        }
        string appTypeName = "";
        this.ddlAppTypeQuery.Items.Clear();
        this.ddlAppTypeQuery.Items.Add(new ListItem("==请选择==", "NA"));
        foreach (int appType in Enum.GetValues(typeof(VAAppType)))
        {
            appTypeName = Enum.GetName(typeof(VAAppType), appType);
            this.ddlAppTypeQuery.Items.Add(new ListItem(appTypeName, appType.ToString()));
        }
    }
    private void BindList(string appType, string imageType, string screenWidth, int str, int end)
    {
        DataTable dtList = clientImageSizeOperate.QueryClientImageSize(0, Common.ToInt32(appType.Replace("NA", "0")), imageType.Replace("NA", ""), screenWidth);

        if (dtList.Rows.Count > 0)
        {
            int cnt = dtList.Rows.Count;
            AspNetPager1.RecordCount = cnt;
            DataTable dtPage = Common.GetPageDataTable(dtList, str, end);

            if (dtPage.Rows.Count > 0)
            {
                this.gdvList.DataSource = dtPage;
                this.gdvList.DataBind();

                this.lbCount.Text = gdvList.Rows.Count.ToString();
            }
            else
            {
                this.gdvList.DataSource = null;
                this.gdvList.DataBind();
                this.lbCount.Text = "0";
            }
        }
        else
        {
            this.gdvList.DataSource = null;
            this.gdvList.DataBind();
            this.lbCount.Text = "0";
        }
    }
    protected void lnkbtnEdit_OnCommand(object sender, CommandEventArgs e)
    {
        int Id = Common.ToInt32(e.CommandArgument);
        ViewState["id"] = Id;

        switch (e.CommandName)
        {
            case "modify":
                BindDetail(Id);
                this.divList.Attributes.Add("style", "display:none");
                this.divDetail.Attributes.Add("style", "display:''");
                break;
            case "del":
                bool result = clientImageSizeOperate.DeleteClientImageSize(Id);
                if (result)
                {
                    BindList(ddlAppTypeQuery.SelectedValue, ddlImageTypeQuery.SelectedValue, txbQueryScreenWidth.Text, 0, 15);
                    Page.ClientScript.RegisterStartupScript(GetType(), "success", "<script>alert('删除成功！')</script>");
                    Clear();
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "success", "<script>alert('删除失败！'')</script>");
                }
                break;
            default:
                break;
        }
    }
    //保存活动内容
    protected void btnSave_Click(object sender, EventArgs e)
    {
        bool update = false;
        int insert = 0;
        ClientImageSize clientImageSize = new ClientImageSize();
        clientImageSize.apptype = Convert.ToInt32(ddlAppType.SelectedValue);
        clientImageSize.screenWidth = txbScreenWidth.Text;
        clientImageSize.imageType = Common.ToInt32(ddlImageType.SelectedValue);
        clientImageSize.value = txbValue.Text;
        clientImageSize.status = 1;

        if (ViewState["id"] != null && ViewState["id"].ToString() != "")//修改
        {
            clientImageSize.id = Common.ToInt32(ViewState["id"]);
            update = clientImageSizeOperate.UpdateClientImageSize(clientImageSize);
        }
        else
        {
            clientImageSize.id = 0;
            insert = clientImageSizeOperate.InsertClientImageSize(clientImageSize);
        }
        if (insert > 0 || update)
        {
            this.divDetail.Attributes.Add("style", "display:none");
            this.divList.Attributes.Add("style", "display:''");
            if (ViewState["id"] != null && ViewState["id"].ToString() != "")//修改
            {
                BindList(ddlAppTypeQuery.SelectedValue, ddlImageTypeQuery.SelectedValue, txbQueryScreenWidth.Text, AspNetPager1.StartRecordIndex - 1, AspNetPager1.EndRecordIndex);
            }
            else
            {
                BindList(ddlAppTypeQuery.SelectedValue, ddlImageTypeQuery.SelectedValue, txbQueryScreenWidth.Text, 0, 15);
            }
            Clear();
            EnabledTextBox();
            Page.ClientScript.RegisterStartupScript(GetType(), "success", "<script>alert('保存成功！');</script>");
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "success", "<script>alert('保存失败！')</script>");
        }
    }
    //编辑页面取消按钮
    protected void btnCancle_Click(object sender, EventArgs e)
    {
        this.divDetail.Attributes.Add("style", "display:none");
        this.divList.Attributes.Add("style", "display:''");
        EnabledTextBox();
        Clear();
    }
    //新建
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        this.divList.Attributes.Add("style", "display:none");
        this.divDetail.Attributes.Add("style", "display:''");

        BindDDL();
    }
    private void BindDDL()
    {
        string imageTypeName = "";
        this.ddlImageType.Items.Clear();
        this.ddlImageType.Items.Add(new ListItem("==请选择==", "NA"));
        foreach (int imageType in Enum.GetValues(typeof(VAImageType)))
        {
            imageTypeName = Enum.GetName(typeof(VAImageType), imageType);
            this.ddlImageType.Items.Add(new ListItem(imageTypeName, imageType.ToString()));
        }

        string appTypeName = "";
        this.ddlAppType.Items.Clear();
        this.ddlAppType.Items.Add(new ListItem("==请选择==", "NA"));
        foreach (int appType in Enum.GetValues(typeof(VAAppType)))
        {
            appTypeName = Enum.GetName(typeof(VAAppType), appType);
            this.ddlAppType.Items.Add(new ListItem(appTypeName, appType.ToString()));
        }
    }
    private void Clear()
    {
        this.ddlAppType.SelectedValue = "NA";
        this.txbScreenWidth.Text = "";
        this.ddlImageType.SelectedValue = "NA";
        this.txbValue.Text = "";
        ViewState["id"] = null;
    }
    /// <summary>
    /// 分页操作
    /// </summary>
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        BindList(ddlAppTypeQuery.SelectedValue, ddlImageTypeQuery.SelectedValue, txbQueryScreenWidth.Text, AspNetPager1.StartRecordIndex - 1, AspNetPager1.EndRecordIndex);
    }
    private void BindDetail(int Id)
    {
        DataTable dt = clientImageSizeOperate.QueryClientImageSize(Id, 0, "", "");
        if (dt.Rows.Count > 0)
        {
            BindDDL();
            this.ddlAppType.SelectedValue = dt.Rows[0]["appType"].ToString();
            this.txbScreenWidth.Text = dt.Rows[0]["screenWidth"].ToString();
            this.ddlImageType.SelectedValue = dt.Rows[0]["imageType"].ToString();
            this.txbValue.Text = dt.Rows[0]["value"].ToString();
            DisabledTextBox();
        }
    }
    private void EnabledTextBox()
    {
        this.ddlAppType.Enabled = true;
        this.txbScreenWidth.Enabled = true;
        this.ddlImageType.Enabled = true;
    }
    private void DisabledTextBox()
    {
        this.ddlAppType.Enabled = false;
        this.txbScreenWidth.Enabled = false;
        this.ddlImageType.Enabled = false;
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        BindList(ddlAppTypeQuery.SelectedValue, ddlImageTypeQuery.SelectedValue, txbQueryScreenWidth.Text, 0, 15);
    }
    //设备类别切换
    protected void ddlAppTypeQuery_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindList(ddlAppTypeQuery.SelectedValue, ddlImageTypeQuery.SelectedValue, txbQueryScreenWidth.Text, 0, 15);
    }
    //图片类别切换
    protected void ddlImageTypeQuery_SelectedIndexChanged(object sender, EventArgs e)
    {

        BindList(ddlAppTypeQuery.SelectedValue, ddlImageTypeQuery.SelectedValue, txbQueryScreenWidth.Text, 0, 15);
    }
    protected void gdvList_DataBound(object sender, EventArgs e)
    {
        int appType = 0;
        int imageType = 0;
        for (int i = 0; i < gdvList.Rows.Count; i++)
        {
            appType = Common.ToInt32(gdvList.Rows[i].Cells[1].Text);
            gdvList.Rows[i].Cells[1].Text = Enum.GetName(typeof(VAAppType), appType);
            imageType = Common.ToInt32(gdvList.Rows[i].Cells[3].Text);
            gdvList.Rows[i].Cells[3].Text = Enum.GetName(typeof(VAImageType), imageType);
        }
    }
}