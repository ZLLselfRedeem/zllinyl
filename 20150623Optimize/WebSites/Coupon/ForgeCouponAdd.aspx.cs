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

public partial class Coupon_ForgeCouponAdd : System.Web.UI.Page
{

    public int? CouponId
    {
        get
        {
            if (ViewState["CouponId"] != null)
            {
                return int.Parse(ViewState["CouponId"].ToString());
            }
            return null;
        }
        set
        {
            if (value != null)
            {
                ViewState["CouponId"] = value.Value;
            }
        }
    }

    public string image
    {
        get
        {
            if (ViewState["image"] != null)
            {
                return ViewState["image"].ToString();
            }
            return string.Empty;
        }
        set
        {
            if (value != null)
            {
                ViewState["image"] = value;
            }
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

    private void DoInit()
    {
        if (!this.IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["CouponId"]))
            {
                int couponId = int.Parse(Request.QueryString["CouponId"]);
                var coupon = CouponOperate.GetEntityById(couponId);
                if (coupon != null)
                {
                    this.CouponId = couponId;
                    this.TextBoxName.Text = coupon.CouponName;
                    this.TextBoxEndDate.Text = coupon.EndDate.ToString("yyyy-MM-dd");
                    this.TextBoxStartDate.Text = coupon.StartDate.ToString("yyyy-MM-dd");
                    this.TextBoxSheetNumber.Text = coupon.SheetNumber.ToString();
                    this.TextBoxRemark.Text = coupon.Remark;
                    this.Big_Img.ImageUrl = WebConfig.CdnDomain + WebConfig.ImagePath + coupon.Image;
                    this.image = coupon.Image;
                    this.TextBoxShopAddress.Text = coupon.ShopAddress;
                    this.TextBoxRequirementMoney.Text = coupon.RequirementMoney.ToString();
                    this.TextBoxDeductibleAmount.Text = coupon.DeductibleAmount.ToString();
                    this.TextBoxShopName.Text = coupon.ShopName;
                    this.ButtonAdd.Text = "编辑";
                }
            }
            else
            {
                this.TextBoxEndDate.Text = DateTime.Now.AddMonths(1).ToString("yyyy-MM-dd");
                this.TextBoxStartDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }
        }
    } 

    protected void Page_Load(object sender, EventArgs e)
    {
        this.DoInit();
    }


    protected void ButtonAdd_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(this.image))
        {
            CommonPageOperate.AlterMsg(this, "门脸图不能为空,请上传!");
            return;
        }
        var baiduMapLocation = Common.GetBaiduMapCoordinate(this.TextBoxShopAddress.Text, this.ddlCity.SelectedItem.Text);
        if (baiduMapLocation == null)
        {
            CommonPageOperate.AlterMsg(this, "地址无法获取经纬度,请重新输入!");
            return;
        }
        if (this.CouponId.HasValue)
        {
            var entity = CouponOperate.GetEntityById(this.CouponId.Value);
            entity.SendCount = int.Parse(this.TextBoxSheetNumber.Text);
            entity.SheetNumber = int.Parse(this.TextBoxSheetNumber.Text);
            entity.CouponName = this.TextBoxName.Text;
            entity.DeductibleAmount = double.Parse(this.TextBoxDeductibleAmount.Text);
            entity.RequirementMoney = double.Parse(this.TextBoxRequirementMoney.Text);
            entity.Remark = this.TextBoxRemark.Text;
            entity.LastUpdatedBy = this.UserInfo.employeeID;
            entity.LastUpdatedTime = DateTime.Now;
            entity.Image = this.image;
            entity.StartDate = DateTime.Parse(Request.Form[this.TextBoxStartDate.ClientID]);
            entity.EndDate = DateTime.Parse(Request.Form[this.TextBoxEndDate.ClientID]).AddDays(1).AddTicks(-1);
            entity.Longitude = baiduMapLocation.lng;
            entity.Latitude = baiduMapLocation.lat;
            entity.CityId = int.Parse(this.ddlCity.SelectedValue);
            if (CouponOperate.Update(entity))
            {
                CommonPageOperate.AlterMsg(this, "修改成功！");
                Page.ClientScript.RegisterStartupScript(GetType(),
               "message", string.Format("<script language='javascript' defer>window.location.href = 'CouponDetail.aspx?CouponId={0}';</script>", entity.CouponId));
            }
            else
            {
                CommonPageOperate.AlterMsg(this, "数据提交失败，请重试！");
            }
        }
        else
        {
            DateTime startDate =
                DateTime.Parse(Request.Form[this.TextBoxStartDate.ClientID]);
            DateTime endDate =
                DateTime.Parse(Request.Form[this.TextBoxEndDate.ClientID]).AddDays(1).AddTicks(-1);
            Coupon entity = new Coupon()
            {
                SendCount = int.Parse(this.TextBoxSheetNumber.Text),
                SheetNumber = int.Parse(this.TextBoxSheetNumber.Text),
                ShopId = null,
                SortOrder = 0,
                EndDate = endDate,
                StartDate = startDate,
                State = 1,
                CouponName = this.TextBoxName.Text,
                DeductibleAmount = double.Parse(this.TextBoxDeductibleAmount.Text),
                RequirementMoney = double.Parse(this.TextBoxRequirementMoney.Text),
                ValidityPeriod = 0,
                Remark = this.TextBoxRemark.Text,
                CreateTime = DateTime.Now,
                LastUpdatedBy = this.UserInfo.employeeID,
                CreatedBy = this.UserInfo.employeeID,
                LastUpdatedTime = DateTime.Now,
                Image = this.image,
                IsGot = true,
                IsDisplay = true,
                Latitude = baiduMapLocation.lat,
                Longitude = baiduMapLocation.lng,
                ShopAddress = this.TextBoxShopAddress.Text,
                ShopName = this.TextBoxShopName.Text,
                CouponType = 2,
                CityId = int.Parse(this.ddlCity.SelectedValue)
            };
            if (CouponOperate.Add(entity))
            {
                CommonPageOperate.AlterMsg(this, "添加成功！");
                Page.ClientScript.RegisterStartupScript(GetType(),
               "message", string.Format("<script language='javascript' defer>window.location.href = 'CouponDetail.aspx?CouponId={0}';</script>", entity.CouponId));
            }
            else
            {
                CommonPageOperate.AlterMsg(this, "数据提交失败，请重试！");
            }
        }
    }
    protected void ButtonCancel_Click(object sender, EventArgs e)
    {

    }
    protected void ButtonUpload_Click(object sender, EventArgs e)
    {
        string fileName = Server.HtmlEncode(fileUpload.FileName);
        string extension = System.IO.Path.GetExtension(fileName);
        if (!string.IsNullOrEmpty(fileName))
        {
            if (Common.ValidateExtension(extension) &&
                (extension.Replace(".", "").ToLower() == "png" 
                || extension.Replace(".", "").ToLower() == "jpg" || extension.Replace(".", "").ToLower() == "jpeg"))
            {
                if (fileUpload.PostedFile.ContentLength / 1024 < WebConfig.BannerSpace)
                {
                    string virtualPath = string.Empty;
                    string imageName = string.Empty;

                    virtualPath =  "15/Coupon/";
                    imageName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + extension;

                    Bitmap originalBMP = new Bitmap(fileUpload.FileContent);//该上传图片文件
                    string objectKey = WebConfig.ImagePath + virtualPath + imageName;//图片在数据库中的实际位置
                    CloudStorageResult result = CloudStorageOperate.PutObject(objectKey, fileUpload, imageName);

                    if (result.code)
                    {
                        this.image = virtualPath + imageName;
                        this.Big_Img.ImageUrl = WebConfig.CdnDomain + WebConfig.ImagePath + virtualPath + imageName;//阿里云服务器读取显示图片信息
                    }
                    else
                    { 
                        Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('上传失败');</script>");
                    }
                }
            }
        }
    }
}