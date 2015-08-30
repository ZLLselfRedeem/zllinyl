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
using CloudStorage;
using System.Data;

public partial class OrderOptimization_ChannelDetail : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitChannelDish();
        }
    }

    private void InitChannelDish()
    {
        string shopName = Convert.ToString(Request.QueryString["shopName"]);
        string channelName = Common.ToString(Request.QueryString["channelName"]);
        liShowTitle.InnerText = shopName + "  增值栏目：" + channelName;
        //TextBox_MerchantName.Font.Size = 14;
        if (Request.QueryString["shopChannelDishID"] != null)
        {
            // 修改
            int shopChannelDishID = Common.ToInt32(Request.QueryString["shopChannelDishID"]);
            int shopID = Common.ToInt32(Request.QueryString["shopID"]);

            ShopChannelDish dish = ShopChannelDishOperate.Search(shopChannelDishID);
            DishChannelPrice channelPrice = ShopChannelDishOperate.SearchPriceAndDiscount(dish.dishPriceID, shopID);
            txtPrice.Text = Convert.ToString(channelPrice.price);
            hidPrice.Value = Convert.ToString(channelPrice.price);
            if (channelPrice.discount == 0)
            {
                txtDiscount.Text = "无折扣";
                hidDiscount.Value = Convert.ToString("无折扣");
            }
            else
            {
                hidDiscount.Value = Convert.ToString(channelPrice.discount);
                txtDiscount.Text = Convert.ToString(channelPrice.discount);
            }
            txtIndex.Text = Convert.ToString(dish.dishIndex);
            txtDishName.Value = dish.dishName;
            txtDishName.Disabled = true;
            txtDishContent.Text = dish.dishContent;
            hidImageUrl.Value = dish.dishImageUrl;
            
            hidDishID.Value = Convert.ToString(dish.dishID);
            hidDishPriceId.Value = Convert.ToString(dish.dishPriceID);
            if (!string.IsNullOrEmpty(hidImageUrl.Value.Trim()))
            {
                this.Big_Img.ImageUrl = WebConfig.CdnDomain + WebConfig.ImagePath + hidImageUrl.Value;
            }
        }
        hidShopID.Value = Request.QueryString["shopID"];

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        int id = Common.ToInt32(Request.QueryString["channelID"]);
        int channelIndex = Common.ToInt32(Request.QueryString["channelIndex"]);
        int shopID = Common.ToInt32(Request.QueryString["shopID"]);
        string shopName = Convert.ToString(Request.QueryString["shopName"]);
        string channelName = Common.ToString(Request.QueryString["channelName"]);
        Response.Redirect("ChannelConfig.aspx?id=" + id + "&channelIndex=" + channelIndex + "&shopID=" + shopID + "&shopName=" + shopName + "&channelName=" + channelName);
    }

    private ShopChannelDish ShopChannelDishCreate()
    {
        ShopChannelDish dish = new ShopChannelDish()
        {
            shopChannelID = Common.ToInt32(Request.QueryString["channelID"]),
            dishID = Common.ToInt32(hidDishID.Value),
            dishPriceID = Common.ToInt32(hidDishPriceId.Value),
            dishName = Common.ToString(txtDishName.Value.Split('-')[0]),
            dishIndex = Common.ToInt32(txtIndex.Text),
            dishContent = Common.ToString(txtDishContent.Text),
            dishImageUrl = Common.ToString(hidImageUrl.Value),
            createTime = DateTime.Now,
            isDelete = false,
            status = false
        };
        return dish;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (CheckValue())
        {
            ShopChannelDish dish = ShopChannelDishCreate();
            ShopChannelDishOperate operate = new ShopChannelDishOperate();
            bool result = true;
            int id = Common.ToInt32(Request.QueryString["channelID"]);
            int channelIndex = Common.ToInt32(Request.QueryString["channelIndex"]);
            int shopID = Common.ToInt32(Request.QueryString["shopID"]);
            string shopName = Convert.ToString(Request.QueryString["shopName"]);
            string channelName = Common.ToString(Request.QueryString["channelName"]);
            if (string.IsNullOrEmpty(Request.QueryString["shopChannelDishID"]))
            {
                // 新建
                dish.id = -1;
                result = operate.Insert(dish);
                if (result)
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('保存成功！');window.location.href='ChannelConfig.aspx?id=" + id + "&channelIndex=" + channelIndex +"&shopID="+shopID+"&shopName="+shopName+"&channelName="+channelName+"'</script>");
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('保存失败！');</script>");
                }
            }
            else
            {
                // 修改
                dish.id = Common.ToInt32(Request.QueryString["shopChannelDishID"]);
                result = operate.Insert(dish);
                operate.Delete(dish.id);
                if (result)
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('保存成功！');window.location.href='ChannelConfig.aspx?id=" + id + "&shopChannelDishID=" + dish.id + "&channelIndex=" + channelIndex + "&shopID=" + shopID +"&shopName="+shopName+"&channelName="+channelName+ "'</script>");
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('保存失败！');</script>");
                }
            }
        }
    }

    public bool CheckValue()
    {
        int index = Common.ToInt32(txtIndex.Text.Trim());
        int channelID = Common.ToInt32(Request.QueryString["channelID"]);
        // 编辑
        if (!string.IsNullOrEmpty(Request.QueryString["shopChannelDishID"]))
        {
            int shopDishIndex = Common.ToInt32(Request.QueryString["dishIndex"]);
            if (shopDishIndex != index && ShopChannelDishOperate.DishIndexIsClash(channelID, index))
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('序号被占用！');</script>");
                return false;
            }
        }
        else
        {
            //新增
            if (ShopChannelDishOperate.DishIndexIsClash(channelID, index))
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('序号被占用！');</script>");
                return false;
            }
        }

        return true;
    }

    /// <summary>
    /// 上传菜品 图片
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button_Upload_Click(object sender, EventArgs e)
    {
        string fileName = Server.HtmlEncode(fileUpload.FileName);
        string extension = System.IO.Path.GetExtension(fileName);
        int shopID = Common.ToInt32(hidShopID.Value);
        if (fileName != "")
        {
            if (Common.ValidateExtension(extension) && (extension.Replace(".", "").ToLower() == "png"))
            {
                if (fileUpload.PostedFile.ContentLength / 1024 < 1024)
                {
                    string virtualPath = string.Empty;
                    string imageName = string.Empty;

                    virtualPath = "ShopChannelDish/" + shopID + "/";
                    imageName = DateTime.Now.ToString("ddHHmmssfff") + extension;

                    string objectKey = virtualPath + imageName;//图片在数据库中的实际位置
                    hidImageUrl.Value = objectKey;
                    CloudStorageResult result = CloudStorageOperate.PutObject(WebConfig.ImagePath + objectKey, fileUpload, imageName);

                    if (result.code)
                    {
                        this.Big_Img.ImageUrl = WebConfig.CdnDomain + WebConfig.ImagePath + virtualPath + imageName;//阿里云服务器读取显示图片信息
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('上传失败');</script>");
                    }
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('图片必须小于1024KB');</script>");
                }
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('请上传png格式的图片！');</script>");
            }
        }
    }
}