using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.WebPageDll;
using VAGastronomistMobileApp.Model;
using System.Data;
using System.Transactions;
using System.ComponentModel;

public partial class ShopManage_ShopSundryManage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetSundry_PageInfo(0, 10);//分页显示店铺杂货信息
            GetShopInfo();//绑定显示公司列表信息
        }
    }
    /// <summary>
    /// 隐藏绑定显示杂项的列表信息，显示添加杂项界面
    /// </summary>
    protected void Button_Add_Click(object sender, EventArgs e)
    {
        Panel_TablewareAndRiceList.Visible = false;
        Panel_TablewareAndRiceAdd.Visible = true;
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
        Label_shop.Text = DropDownList_shop.SelectedItem.Text;
    }

    /// <summary>
    /// (wangcheng)生成杂项数据
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button_Comfirm_Click(object sender, EventArgs e)
    {
        SundryOperate sundryOperate = new SundryOperate();
        SundryInfo sundryInfo = new SundryInfo();
        sundryInfo.shopId = Common.ToInt32(Request.QueryString["shopId"]);
        sundryInfo.sundryName = TextBox_SundryName.Text.Trim().ToString();
        sundryInfo.sundryChargeMode = Common.ToInt32(DropDownList_sundryStandard.SelectedValue);
        sundryInfo.sundryStandard = TextBox_sundryStandary.Text.Trim().ToString();
        sundryInfo.price = Common.ToDouble(TextBox_Price.Text.ToString());
        sundryInfo.status = (int)VASundry.OPENED;//默认状态开启
        sundryInfo.description = TextBox_MessageDescription.Text.ToString();
        if (RadioButton_required_false.Checked)//是否必选
            sundryInfo.required = false;
        else
            sundryInfo.required = true;
        if (RadioButton_vipDiscountable_true.Checked)//享受折扣
            sundryInfo.vipDiscountable = true;
        else
            sundryInfo.vipDiscountable = false;
        if (RadioButton_backDiscountable_false.Checked)//支持返送
            sundryInfo.backDiscountable = false;
        else
            sundryInfo.backDiscountable = true;
        //执行插入数据库方法
        long returnid = sundryOperate.InsertSundryInfo(sundryInfo);
        if (returnid > 0)
        {
            Common.RecordEmployeeOperateLog((int)VAEmployeeOperateLogOperatePageType.SHOP_SUNDRYMANAGE, (int)VAEmployeeOperateLogOperateType.ADD_OPERATE, " 门店编号："
                   + sundryInfo.shopId + "，杂项名称：" + sundryInfo.sundryName + "，收费模式：" + sundryInfo.sundryChargeMode + "，规格：" + sundryInfo.sundryStandard
                   + "，价格：" + sundryInfo.price + "，状态：" + sundryInfo.status + "，描述：" + sundryInfo.description + "，是否必选：" + sundryInfo.required
                   + "，是否享受折扣：" + sundryInfo.supportChangeQuantity + "，是否支持返送：" + sundryInfo.supportChangeQuantity);
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('店铺杂项添加成功！');</script>");
            Panel_TablewareAndRiceAdd.Visible = false;
            Panel_TablewareAndRiceList.Visible = true;
            GetSundry_PageInfo(0, 10);
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('添加失败，请重试！');</script>");
        }
    }
    /// <summary>
    /// 单元格操作
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GridView_ShopSundryManage_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;//获得点击的当前行
        long sundryId = Common.ToInt64(GridView_ShopSundryManage.DataKeys[index].Values["sundryId"].ToString());//获得点击的按钮
        SundryOperate sundryOperate = new SundryOperate();
        if (e.CommandName.ToString() == "SetIsValid")
        {
            using (TransactionScope scope = new TransactionScope())
            {
                //执行开启和关闭的操作
                int status = Common.ToInt32(GridView_ShopSundryManage.DataKeys[index].Values["status"].ToString());//获得点击行的状态
                bool updateStatus = sundryOperate.UpdateSundryStatus(sundryId, status);
                if (updateStatus == false)
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('开启失败！');</script>");
                }
                else
                {
                    if (((LinkButton)e.CommandSource).Text == "关闭")
                    {
                        ((LinkButton)e.CommandSource).Text = "开启";
                        scope.Complete();
                    }
                    else
                    {
                        ((LinkButton)e.CommandSource).Text = "关闭";
                        scope.Complete();
                    }
                }
            }
        }
        else if (e.CommandName == "Modification")
        {
            //修改当前选中的杂项,跳转到新页面
            Response.Redirect("UpdateShopSundry.aspx?sundryId=" + sundryId + "&shopId=" + Common.ToInt32(Request.QueryString["shopId"]));//传sundryId参数进去（杂项的唯一标识）
        }
    }
    /// <summary>
    /// 分页
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        GetSundry_PageInfo(AspNetPager1.StartRecordIndex - 1, AspNetPager1.EndRecordIndex);
    }

    //绑定数据
    protected void GetSundry_PageInfo(int str, int end)
    {
        SundryOperate sundryOperate = new SundryOperate();
        DataTable dt = sundryOperate.QuerySundryInfo(Common.ToInt32(Request.QueryString["shopId"]));
        if (dt.Rows.Count > 0)
        {
            //有数据，表示开启了或者曾经开启了杂项，隐藏开启按钮,显示添加按钮
            Button_ReadySundry.Attributes.Add("style", "display:none");
            Button_Add.Attributes.Add("style", "display:block");
            int sundry_Page = dt.Rows.Count;
            AspNetPager1.RecordCount = sundry_Page;
            DataTable dt_page = Common.GetPageDataTable(dt, str, end);//分页的DataTable
            GridView_ShopSundryManage.DataSource = dt_page;
            GridView_ShopSundryManage.DataBind();
            //设置默认的是否开启的显示状态
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (Common.ToInt32(dt.Rows[i]["status"]) == (int)VASundry.OPENED)//开启
                {
                    ((LinkButton)GridView_ShopSundryManage.Rows[i].FindControl("LinkButton1")).Text = "开启";
                }
                else if (Common.ToInt32(dt.Rows[i]["status"]) == (int)VASundry.CLOSED)//关闭
                {
                    ((LinkButton)GridView_ShopSundryManage.Rows[i].FindControl("LinkButton1")).Text = "关闭";
                }
            }
            Panel_TablewareAndRiceList.Visible = true;
        }
        else
        {
            Button_ReadySundry.Attributes.Add("style", "display:block");
            Button_Add.Attributes.Add("style", "display:none");
            Panel_TablewareAndRiceList.Visible = false;
        }
    }
    //开启杂项
    protected void Button_ReadySundry_Click(object sender, EventArgs e)
    {
        //显示事先设计好的杂项
        //显示添加按钮
        Button_Add.Attributes.Add("style", "display:block");
        Button_ReadySundry.Attributes.Add("style", "display:none");
        //将系统默认的杂项信息添加到当前的添加到当前对应的shopId中去
        SundryOperate sundryOperate = new SundryOperate();
        long returnid = sundryOperate.InsertDefaultSundryInfo(Common.ToInt32(Request.QueryString["shopId"]));
        if (returnid > 0)
        {
            GetSundry_PageInfo(0, 10); //插入成功，下一步显示出来
        }
    }
    protected void Button_Back_Click(object sender, EventArgs e)
    {
        Panel_TablewareAndRiceList.Visible = true;
        Panel_TablewareAndRiceAdd.Visible = false;
    }
    protected void DropDownList_sundryStandard_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownList_sundryStandard.SelectedValue == "2") message.Text = "（请输入0-1的小数）";
        else message.Text = "（请输入大于0的数字）";
    }
}