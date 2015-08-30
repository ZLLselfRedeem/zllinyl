using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.WebPageDll;
using Web.Control.Enum;
public partial class CompanyManage_CompanyCommissionAndFreeRefundHour : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["companyID"] != null)
            {
                int companyID = Common.ToInt32(Request.QueryString["companyID"]);
                BindVACommissionType();
                GetCompanyInfo(companyID);
                DropDownList_CommissionType_SelectedIndexChanged(null, null);
            }
        }
    }
    /// <summary>
    /// 绑定商家支付给友络的佣金类型
    /// </summary>
    protected void BindVACommissionType()
    {
        DropDownList_CommissionType.DataSource = EnumHelper.EnumToList(typeof(VACommissionType));
        DropDownList_CommissionType.DataTextField = "Text";
        DropDownList_CommissionType.DataValueField = "Value";
        DropDownList_CommissionType.DataBind();
    }
    /// <summary>
    /// 获取某公司信息
    /// </summary>
    /// <param name="companyID"></param>
    protected void GetCompanyInfo(int companyID)
    {
        CompanyInfo companyInfo = new CompanyInfo();
        CompanyOperate companyOperate = new CompanyOperate();
        companyInfo = companyOperate.QueryCompany(companyID);
        DropDownList_CommissionType.Text = Common.ToInt32(companyInfo.viewallocCommissionType).ToString();
        TextBox_FreeRefundHour.Text = companyInfo.freeRefundHour.ToString();
        TextBox_CommissionValue.Text = companyInfo.viewallocCommissionValue.ToString();
        Label_CompanyName.Text = companyInfo.companyName;
    }
    protected void Button_Set_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["companyID"] != null)
        {
            int companyID = Common.ToInt32(Request.QueryString["companyID"]);
            CompanyInfo companyInfo = new CompanyInfo();
            companyInfo.companyID = companyID;
            companyInfo.viewallocCommissionType = (VACommissionType)int.Parse(DropDownList_CommissionType.SelectedValue);
            if (int.Parse(DropDownList_CommissionType.SelectedValue) == 2)//表示选中的是比例
            {
                if (Common.ToDouble(TextBox_CommissionValue.Text) <= Common.ToDouble(0) || Common.ToDouble(TextBox_CommissionValue.Text) >= Common.ToDouble(1))
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('佣金值输入非法！');</script>");
                }
            }
            companyInfo.viewallocCommissionValue = Common.ToDouble(TextBox_CommissionValue.Text);
            companyInfo.freeRefundHour = Common.ToDouble(TextBox_FreeRefundHour.Text);
            CompanyOperate companyOperate = new CompanyOperate();
            if (companyOperate.ModifyCompanyCommissionAndRefundHour(companyInfo))
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('设置成功！');</script>");
                Common.RecordEmployeeOperateLog((int)VAEmployeeOperateLogOperatePageType.COMPANY_COMMISSIONANDFREEREFUNDHOUR, (int)VAEmployeeOperateLogOperateType.UPDATE_OPERATE, "公司名称："
                    + Label_CompanyName.Text.Trim() + "，佣金形式：" + DropDownList_CommissionType.SelectedItem.Text + "，佣金值：" + TextBox_CommissionValue.Text + "，无忧退款时间：" + TextBox_FreeRefundHour.Text);
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('设置失败！');</script>");
            }
        }
    }
    protected void DropDownList_CommissionType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownList_CommissionType.SelectedValue == ((int)VACommissionType.Normal_Value).ToString())
        {
            Label_Alert.Text = "请填入大于或等于0的数值";
        }
        else if (DropDownList_CommissionType.SelectedValue == ((int)VACommissionType.Proportion).ToString())
        {
            Label_Alert.Text = "请填入0-1之间的数值";
        }
    }
}