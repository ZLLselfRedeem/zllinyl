using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.WebPageDll;
using VAGastronomistMobileApp.Model;
using System.Configuration;
using ChineseCharacterToPinyin;

public partial class CompanyManage_CompanyAdd : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    //添加公司
    protected void Button_AddCompany_Click(object sender, EventArgs e)
    {
        if (Session["UserInfo"] != null)
        {
            // string imagePath = @"../" + ConfigurationManager.AppSettings["ImagePath"].ToString();
            CompanyOperate companyOperate = new CompanyOperate();
            CompanyInfo companyInfo = new CompanyInfo();
            companyInfo.companyAddress = TextBox_companyAddress.Text;
            companyInfo.companyLogo = "";
            companyInfo.companyName = TextBox_companyName.Text;
            companyInfo.companyStatus = 1;//公司状态
            companyInfo.companyTelePhone = TextBox_companyTelePhone.Text;
            companyInfo.contactPerson = TextBox_contactPerson.Text;
            companyInfo.contactPhone = TextBox_contactPhone.Text;
            companyInfo.companyDescription = TextBox_companyDescription.Text;
            companyInfo.ownedCompany = TextBox_ownedCompany.Text;//所属公司
            string companyImagePath = CharacterToPinyin.GetAllPYLetters(Common.ToClearSpecialCharString(companyInfo.companyName)) + DateTime.Now.ToString("yyyyMMddHHmmssfff") + "/";
            //Common.FolderCreate(Server.MapPath(imagePath + companyImagePath));//生成一个文件夹
            companyInfo.companyImagePath = companyImagePath;
            int newCompanyId = companyOperate.AddCompany(companyInfo);
            if (newCompanyId > 0)
            {
                //给这个用户对这个公司的权限
                // EmployeeConnShopOperate employeeShopOperate = new EmployeeConnShopOperate();
                // EmployeeConnShop employeeShop = new EmployeeConnShop();
                // employeeShop.shopID = 0;
                // employeeShop.companyID = newCompanyId;
                //  VAEmployeeLoginResponse vAEmployeeLoginResponse = ((VAEmployeeLoginResponse)Session["UserInfo"]);//根据session获取用户信息
                //  employeeShop.employeeID = vAEmployeeLoginResponse.employeeID;
                //  if (employeeShopOperate.AddEmployeeShop(employeeShop))
                // {
                Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('添加成功！');</script>");
                // }
            }
            else
            {
                // Common.DeleteDir(imagePath + companyImagePath);//删除生成的的文件夹
                Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('添加失败！');</script>");
            }
        }
    }
}