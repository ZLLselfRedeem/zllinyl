using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.WebPageDll;
using System.Data;
using VAGastronomistMobileApp.Model;
using System.ComponentModel;

public partial class ShopManage_UpdateShopSundry : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetShopInfo();
            ShowSundryInfo();
            if (Request.UrlReferrer != null)
                ViewState["UrlReferrer"] = Request.UrlReferrer.ToString();
            DropDownList_sundryStandard_SelectedIndexChanged(DropDownList_sundryStandard, null);
        }
    }
    /// <summary>
    /// 修改餐厅杂项信息
    /// </summary>
    protected void Button_Comfirm_Click(object sender, EventArgs e)
    {
        SundryInfo sundryInfo = new SundryInfo();
        sundryInfo.price = Common.ToDouble(TextBox_Price.Text.Trim());
        sundryInfo.sundryChargeMode = Common.ToInt32(DropDownList_sundryStandard.SelectedValue);
        sundryInfo.sundryName = TextBox_SundryName.Text.Trim();
        sundryInfo.sundryStandard = TextBox_sundryStandary.Text.Trim();
        sundryInfo.description = TextBox_MessageDescription.Text.Trim();
        if (RadioButton_required_Yes.Checked == true)//是否必选
            sundryInfo.required = true;
        else
            sundryInfo.required = false;
        if (RadioButton_vipDiscountable_true.Checked == true)//享受折扣
            sundryInfo.vipDiscountable = true;
        else
            sundryInfo.vipDiscountable = false;
        if (RadioButton_backDiscountable_true.Checked == true)//支持返送
            sundryInfo.backDiscountable = true;
        else
            sundryInfo.backDiscountable = false;
        sundryInfo.shopId = Common.ToInt32(DropDownList_shop.SelectedValue);
        sundryInfo.status = (int)VASundry.OPENED;//默认修改后设置为开启状态
        SundryOperate so = new SundryOperate();
        long newSundryId = Common.ToInt64(Request.QueryString["sundryId"]);
        long result = so.UpdateSundayInfo(sundryInfo, newSundryId);
        if (result > 0)
        {
            Common.RecordEmployeeOperateLog((int)VAEmployeeOperateLogOperatePageType.SHOP_SUNDRYMANAGE, (int)VAEmployeeOperateLogOperateType.UPDATE_OPERATE, " 门店编号："
                  + sundryInfo.shopId + "，杂项名称：" + sundryInfo.sundryName + "，收费模式：" + sundryInfo.sundryChargeMode + "，规格：" + sundryInfo.sundryStandard
                  + "，价格：" + sundryInfo.price + "，状态：" + sundryInfo.status + "，描述：" + sundryInfo.description + "，是否必选：" + sundryInfo.required
                  + "，享受折扣：" + sundryInfo.vipDiscountable + "，支持返送：" + sundryInfo.backDiscountable);
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('修改成功！');</script>");
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('修改失败，请重试！');</script>");
        }
    }
    /// <summary>
    /// 获取店铺信息
    /// </summary>
    protected void GetShopInfo()
    {
        ShopOperate shopOpe = new ShopOperate();
        ShopInfo shopInfo = shopOpe.QueryShop(Common.ToInt32(Request.QueryString["shopId"]));
        List<VAEmployeeShop> listShop = new List<VAEmployeeShop>();
        VAEmployeeShop shop = new VAEmployeeShop();
        shop.shopID = shopInfo.shopID;
        shop.shopName = shopInfo.shopName;
        listShop.Add(shop);
        DropDownList_shop.DataSource = listShop;
        DropDownList_shop.DataTextField = "shopName";
        DropDownList_shop.DataValueField = "shopId";
        DropDownList_shop.DataBind();
        DropDownList_shop.Text = Request.QueryString["shopId"];
        DropDownList_shop.Enabled = false;//可见不可操作
    }
    protected void ShowSundryInfo()
    {
        if (Common.ToInt32(Request.QueryString["sundryId"]) > 0)
        {
            SundryOperate sundryOperate = new SundryOperate();
            DataTable sundryInfoDT = new DataTable();
            sundryInfoDT = sundryOperate.QuerySundryInfoBySundryId(Common.ToInt32(Request.QueryString["sundryId"]));
            if (sundryInfoDT.Rows.Count == 1)
            {
                TextBox_Price.Text = Common.ToString(sundryInfoDT.Rows[0]["price"]);
                if (sundryInfoDT.Rows[0]["sundryChargeMode"].ToString() == "2")
                {
                    DropDownList_sundryStandard.Items.Add(new ListItem("按比例", "2"));
                    DropDownList_sundryStandard.SelectedValue = "2";
                    DropDownList_sundryStandard.Enabled = false;
                }
                else
                {
                    DropDownList_sundryStandard.SelectedValue = sundryInfoDT.Rows[0]["sundryChargeMode"].ToString();
                }
                TextBox_SundryName.Text = sundryInfoDT.Rows[0]["sundryName"].ToString();
                TextBox_sundryStandary.Text = sundryInfoDT.Rows[0]["sundryStandard"].ToString();
                TextBox_MessageDescription.Text = sundryInfoDT.Rows[0]["description"].ToString();
                if (Common.ToBool(sundryInfoDT.Rows[0]["required"].ToString()) == true)//是否必选
                    RadioButton_required_Yes.Checked = true;
                else
                    RadioButton_required_No.Checked = true;
                if (Common.ToBool(sundryInfoDT.Rows[0]["vipDiscountable"].ToString()) == true)//享受折扣
                    RadioButton_vipDiscountable_true.Checked = true;
                else
                    RadioButton_vipDiscountable_false.Checked = true;
                if (Common.ToBool(sundryInfoDT.Rows[0]["backDiscountable"].ToString()) == true)//支持返送
                    RadioButton_backDiscountable_true.Checked = true;
                else
                    RadioButton_backDiscountable_false.Checked = true;
            }
        }
    }
    //返回
    protected void Button_Back_Click(object sender, EventArgs e)
    {
        if (ViewState["UrlReferrer"] != null)
            Response.Redirect(ViewState["UrlReferrer"].ToString());
        else
            Response.Redirect("ShopSundryManage.aspx?shopId=" + Common.ToInt32(Request.QueryString["shopId"]) + "");
    }
    protected void DropDownList_sundryStandard_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownList_sundryStandard.SelectedValue == "2") message.Text = "（请输入0-1的小数）";
        else message.Text = "（请输入大于0的数字）";
    }
}