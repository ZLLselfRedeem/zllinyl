using CloudStorage;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls; 
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.WebPageDll;

public partial class Meal_MealAdd : System.Web.UI.Page
{
    #region 属性
    public string ImageURL
    {
        get
        {
            return this.HiddenField_Image.Value;
        }
        set
        {
            this.HiddenField_Image.Value = value;
            if (!string.IsNullOrEmpty(value))
            {
                this.Big_Img.ImageUrl = WebConfig.CdnDomain + WebConfig.ImagePath + value;
            }
        }
    }
    public int? MealID
    {
        get
        {
            if(ViewState["MealID"]!=null)
            {
                return int.Parse(ViewState["MealID"].ToString());
            }
            return null;
        }
        set
        {
            ViewState["MealID"] = value;
        }
    }

    public double OriginalPrice
    {
        get
        {
            double originalPrice;
            if (double.TryParse(this.TextBoxOriginalPrice.Text, out originalPrice))
            {
                return Math.Round(originalPrice, 2);
            }
            return  0;
        }
        set
        {
            this.TextBoxOriginalPrice.Text = value.ToString();
        }
    }
    public double  Price
    {
        get
        {
            double price;
            if (double.TryParse(this.TextBoxPrice.Text, out price))
            {
                return Math.Round(price,2);
            }
            return 0;
        }
        set
        {
            this.TextBoxPrice.Text = value.ToString();
        }
    }
    public string Suggestion
    {
        get
        {
            return this.TextBoxSuggestion.Text;
        }
        set
        {
            this.TextBoxSuggestion.Text = value;
        }
    }
    public string Menu
    {
        get
        {
            return this.TextBoxMenu.Text;
        }
        set
        {
            this.TextBoxMenu.Text = value;
        }
    }
    public string MealName
    {
        get
        {
            return this.TextBoxMealName.Text;
        }
        set
        {
            this.TextBoxMealName.Text = value;
        }
    }
    public int IsActive
    {
        get
        {
            return int.Parse(this.RadioButtonListIsActive.SelectedValue);
        }
        set
        {
            this.RadioButtonListIsActive.SelectedValue = value.ToString();
        }
    }
    public int ShopID
    {
        get
        {
            return int.Parse(this.RadioButtonListShop.SelectedValue);
        }
        set
        {
            var operate = new ShopOperate();
            var shopInfo = operate.QueryShop(value);
            if (shopInfo != null)
            {
                this.LabelShop.Text = shopInfo.shopName;
                this.DropDownListCompanys.SelectedValue = shopInfo.companyID.ToString();
                DropDownListCompanys_SelectedIndexChanged(null, null);
                this.RadioButtonListShop.SelectedValue = value.ToString();
                this.LabelCompany.Text = this.DropDownListCompanys.SelectedItem.Text;
            }
        }
    }
    public int OrderNumber
    {
        get
        {
            return int.Parse(this.TextBoxOrderNumber.Text);
        }
        set
        {
            this.TextBoxOrderNumber.Text = value.ToString();
        }
    }
    public VAEmployeeLoginResponse UserInfo
    {
        get
        {
            if (Session["UserInfo"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            return Session["UserInfo"] as VAEmployeeLoginResponse;
        }
    }
    /// <summary>
    /// 是否是运营专员
    /// </summary>
    public bool IsOperatingSpecialist
    {
        get
        {
            if (ViewState["IsOperatingSpecialist"] != null)
            {
                return bool.Parse(ViewState["IsOperatingSpecialist"].ToString());
            }
            return false;
        }
        set
        {
            ViewState["IsOperatingSpecialist"] = value;
        }
    }
    #endregion 

    #region 方法
    public void DoInit()
    {
        if (!Page.IsPostBack)
        { 
            CompanyOperate companyOperate = new CompanyOperate();
            var companyTable = companyOperate.QueryCompany(); 
            this.DropDownListCompanys.DataSource = companyTable.DefaultView;
            this.DropDownListCompanys.DataBind();

            if (Request.QueryString["MealID"] != null)
            {
                this.MealID = int.Parse(Request.QueryString["MealID"]);
                MealOperate mealOperate = new MealOperate();
                Meal meal = mealOperate.GetEntityByID(this.MealID.Value);
                if (meal != null)
                {
                    this.MealName = meal.MealName;
                    this.Menu = meal.Menu;
                    this.OriginalPrice = meal.OriginalPrice;
                    this.Price = meal.Price;
                    this.IsActive = meal.IsActive;
                    this.ShopID = meal.ShopID;
                    this.MealName = meal.MealName;
                    this.ImageURL = meal.ImageURL;
                    this.Suggestion = meal.Suggestion;
                    this.OrderNumber = meal.OrderNumber;
                }
                this.DropDownListCompanys.Visible = false;
                this.RadioButtonListShop.Visible = false;
                this.LabelCompany.Visible = true;
                this.LabelShop.Visible = true;
                this.ButtonAdd.Text = "更新";
            }
            else
            {
                DropDownListCompanys_SelectedIndexChanged(null, null);
                this.DropDownListCompanys.Visible = true;
                this.RadioButtonListShop.Visible = true;
                this.LabelCompany.Visible = false;
                this.LabelShop.Visible = false;
            }
            var employeeOperate = new  EmployeeOperate();
            var employeeRole = employeeOperate.QueryEmployeeRole(this.UserInfo.employeeID);
            if (employeeRole.Count(p => p.roleName == "运营") > 0)
            {
                this.IsOperatingSpecialist = true;
                this.trIsActive.Visible = true;
                this.trImage.Visible = true;
            }
            else
            {
                this.IsOperatingSpecialist = false;
                this.trIsActive.Visible = false;
                this.trImage.Visible = false;
                this.IsActive = 0;
            }
        }
    }
    #endregion

    #region 事件
    protected void Page_Load(object sender, EventArgs e)
    {
        this.DoInit();
    }
    protected void Button_Upload_Click(object sender, EventArgs e)
    {
        string fileName = Server.HtmlEncode(fileUpload.FileName);
        string extension = System.IO.Path.GetExtension(fileName);
        if (fileName != "")
        {
            if (Common.ValidateExtension(extension) && (extension.Replace(".", "").ToLower() == "png" || extension.Replace(".", "").ToLower() == "jpg"))
            {
                if (fileUpload.PostedFile.ContentLength / 1024 < 100)
                {
                    string virtualPath = @"15/Meal/";
                    string imageName = string.Empty; 
                    imageName = "Meal_" + Guid.NewGuid().ToString("N") + extension;

                    #region 判断上传图片的信息
                    Bitmap originalBMP = new Bitmap(fileUpload.FileContent);//该上传图片文件
                    int width = originalBMP.Width;
                    int height = originalBMP.Height;
                    if (Math.Floor(Common.ToDouble(width * 4 / 5)) == height)
                    {
                        if (width >= 640)
                        {
                            string objectKey = WebConfig.ImagePath + virtualPath + imageName;//图片在数据库中的实际位置
                            CloudStorageResult result = CloudStorageOperate.PutObject(objectKey, fileUpload, imageName);

                            if (result.code)
                            {
                                this.ImageURL = virtualPath + imageName;
                            }
                            else
                            {
                                Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('上传失败');</script>");
                            }
                        }
                        else
                        {
                            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('图片最小尺寸应为640*512！');</script>");
                        }
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('图片比例应为5*4！');</script>");
                    }
                    #endregion
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('图片必须小于100KB');</script>");
                }
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('请上传png格式的图片！');</script>");
            }
        }
    }
    protected void ButtonAdd_Click(object sender, EventArgs e)
    {
        if (this.IsActive == 1 && string.IsNullOrEmpty(this.ImageURL))
        {
            CommonPageOperate.AlterMsg(this, "图片未上传！");
            return;
        }
        Meal meal = null;
        MealOperate mealOperate = new MealOperate();
        if (this.MealID.HasValue)
        {
            meal = mealOperate.GetEntityByID(this.MealID.Value);
        }
        if (meal == null)
        {
            meal = new Meal();
        }
        meal.MealName = this.MealName;
        meal.Menu = this.Menu;
        meal.IsActive = this.IsActive;
        meal.OrderNumber = this.OrderNumber;
        meal.OriginalPrice = this.OriginalPrice;
        meal.Price = this.Price;
        meal.Suggestion = this.Suggestion;
        meal.ImageURL = this.ImageURL;
        meal.ShopID = this.ShopID;
        meal.CreatedBy = this.UserInfo.employeeID;
        meal.CreationDate = DateTime.Now; 
        if (this.MealID.HasValue)
        {
            if (mealOperate.UpdateEntity(meal))
            {
                CommonPageOperate.AlterMsg(this, "套餐更新成功！");
                Page.ClientScript.RegisterStartupScript(GetType(),
               "message", "<script language='javascript' defer>window.location.href = 'MealManager.aspx';</script>");
            }
        }
        else
        {
            if (mealOperate.AddEntity(meal))
            {
                var shopSequenceOperate = new ShopSequenceOperate();
                shopSequenceOperate.AddShopSequence(this.ShopID);
                CommonPageOperate.AlterMsg(this, "套餐添加成功！"); 
                Page.ClientScript.RegisterStartupScript(GetType(),
               "message", "<script language='javascript' defer>window.location.href = 'MealManager.aspx';</script>");
            }
        }

    }
    protected void DropDownListCompanys_SelectedIndexChanged(object sender, EventArgs e)
    {
        int companyID = int.Parse(this.DropDownListCompanys.SelectedValue);
        var shopOperate = new ShopOperate();
        var shopTable = shopOperate.QueryShopInfoByCompanyId(companyID);
        this.RadioButtonListShop.DataSource = shopTable;
        this.RadioButtonListShop.DataBind();
        this.RadioButtonListShop.SelectedIndex = 0;
    }
    #endregion
   
} 