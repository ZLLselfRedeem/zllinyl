using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.WebPageDll;
using System.Data;

public partial class ShopManage_ShopVipDicount : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindShopVipList();
        }
    }
    private void BindShopVipList()
    {
        int shopId = Common.ToInt32(Request.QueryString["shopId"]);
        if (shopId > 0)
        {
            ShopOperate _ShopO = new ShopOperate();
            DataTable dtList = _ShopO.GetShopVipInfo(shopId);
            if (dtList != null && dtList.Rows.Count > 0)
            {
                this.GridView_ShopVipList.DataSource = dtList;
                this.GridView_ShopVipList.DataBind();
            }
            Panel_ShopVipList.Visible = true;
            Panel_ShopVipAdd.Visible = false;
        }
    }
    protected void Button_Save_Click(object sender, EventArgs e)
    {
        object[] result = new object[] { false, "保存失败" };
        ShopOperate _ShopO = new ShopOperate();
        try
        {
            int id = Common.ToInt32(this.HiddenField_Id.Value);//店铺VIP序列号
            int platformVipId = Common.ToInt32(DropDownList_VAVip.SelectedValue);
            int shopId = Common.ToInt32(Request.QueryString["shopId"]);//店铺ID
            string name = this.TextBox_ShopVipName.Text.Trim();//名称
            double discount = Math.Round(Common.ToDouble(this.TextBox_Discount.Text.Trim()) / 100, 2);//折扣
            if (discount == 0)
            {
                CommonPageOperate.AlterMsg(this, "折扣不能为0");
                return;
            }
            //先判断是新增还是修改
            if (!string.IsNullOrEmpty(this.HiddenField_Id.Value))//修改
            {
                result[0] = _ShopO.UpdateShopVipInfo(id, name, platformVipId, discount);
                result[1] = "修改成功！";
                BindShopVipList();
            }
            else//新增
            {
                result[0] = _ShopO.InsertShopVipInfo(platformVipId, name, shopId, discount);
                result[1] = "新增成功！";
                BindShopVipList();
            }
        }
        catch (Exception ex)
        {
            result[1] = ex.Message;
        }
        Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('" + result[1].ToString() + "');</script>");
    }

    protected void GridView_ShopVipList_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        ShopOperate _ShopO = new ShopOperate();
        int id = Common.ToInt32(GridView_ShopVipList.DataKeys[e.RowIndex].Values["id"].ToString());//店铺VIP折扣序列号
        object[] result = _ShopO.DeleteShopVipInfo(id);

        if ((int)result[0] > 0)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('删除成功！');</script>");
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('删除失败！');</script>");
        }
        BindShopVipList();
    }

    protected void GridView_ShopVipList_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindDropDownList();
        Button_Save.CommandArgument = "update";
        TextBox_ShopVipName.Text = GridView_ShopVipList.DataKeys[GridView_ShopVipList.SelectedIndex].Values["name"].ToString();
        double discount = Common.ToDouble(GridView_ShopVipList.DataKeys[GridView_ShopVipList.SelectedIndex].Values["discount"]);
        TextBox_Discount.Text = Common.ToString(discount * 100);
        DropDownList_VAVip.SelectedValue = GridView_ShopVipList.DataKeys[GridView_ShopVipList.SelectedIndex].Values["platformVipId"].ToString();
        HiddenField_Id.Value = GridView_ShopVipList.DataKeys[GridView_ShopVipList.SelectedIndex].Values["id"].ToString();
    }

    protected void Button_Add_Click(object sender, EventArgs e)
    {
        BindDropDownList();
        Button_Add.Visible = false;
    }
    protected void BindDropDownList()
    {
        Panel_ShopVipList.Visible = false;
        Panel_ShopVipAdd.Visible = true;
        VipOperate vipOper = new VipOperate();
        DataTable dt = vipOper.QueryViewAllocPlatformVipInfo();
        DropDownList_VAVip.DataSource = dt;
        DropDownList_VAVip.DataTextField = "name";
        DropDownList_VAVip.DataValueField = "id";
        DropDownList_VAVip.DataBind();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Panel_ShopVipAdd.Visible = false;
        Panel_ShopVipList.Visible = true;
        Button_Add.Visible = true;
    }
}