using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.WebPageDll;
using VAGastronomistMobileApp.Model;
using ChineseCharacterToPinyin;
using System.Configuration;

public partial class CompanyManage_CompanyUpdate : System.Web.UI.Page
{
    static string savePath = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["companyID"] != null)
            {
                int companyID = Common.ToInt32(Request.QueryString["companyID"]);
                GetCompanyInfo(companyID);
            }
        }
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
        TextBox_companyAddress.Text = companyInfo.companyAddress;
        TextBox_companyName.Text = companyInfo.companyName;
        TextBox_companyTelePhone.Text = companyInfo.companyTelePhone;
        TextBox_contactPerson.Text = companyInfo.contactPerson;
        TextBox_contactPhone.Text = companyInfo.contactPhone;
        TextBox_companyDescription.Text = companyInfo.companyDescription;
        TextBox_sinaWeibo.Text = companyInfo.sinaWeiboName;
        TextBox_qqWeibo.Text = companyInfo.qqWeiboName;

        TextBox_ownedCompany.Text = companyInfo.ownedCompany;
        TextBox_accp.Text = companyInfo.acpp.ToString();
        TextBox_companyDescription.Text = companyInfo.companyDescription.ToString();
        TextBox_wechatPublicName.Text = companyInfo.wechatPublicName;//wangcheng
    }
    /// <summary>
    /// 修改公司
    /// </summary>
    protected void ModifyCompany()
    {
        CompanyOperate companyOperate = new CompanyOperate();
        CompanyInfo companyInfo = new CompanyInfo();
        int companyID = Common.ToInt32(Request.QueryString["companyID"]);
        companyInfo.companyID = companyID;
        companyInfo.companyAddress = TextBox_companyAddress.Text;
        companyInfo.companyLogo = "";
        companyInfo.companyName = TextBox_companyName.Text;
        companyInfo.companyStatus = 1;
        companyInfo.companyTelePhone = TextBox_companyTelePhone.Text;
        companyInfo.contactPerson = TextBox_contactPerson.Text;
        companyInfo.contactPhone = TextBox_contactPhone.Text;
        companyInfo.companyDescription = TextBox_companyDescription.Text;
        companyInfo.sinaWeiboName = TextBox_sinaWeibo.Text;
        companyInfo.acpp = Common.ToDouble(TextBox_accp.Text);
        companyInfo.qqWeiboName = TextBox_qqWeibo.Text;
        companyInfo.wechatPublicName = TextBox_wechatPublicName.Text;//wangcheng
        companyInfo.ownedCompany = TextBox_ownedCompany.Text;
        bool result = companyOperate.ModifyCompany(companyInfo);
        if (result)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('修改成功！');</script>");
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('修改失败！');</script>");
        }
    }
    //修改公司
    protected void Button1_Click(object sender, EventArgs e)
    {
        ModifyCompany();
    }
}