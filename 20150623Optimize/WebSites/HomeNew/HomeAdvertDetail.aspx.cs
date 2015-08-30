using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.WebPageDll;
using Web.Control;
using System.Web.UI.HtmlControls;
using Web.Control.DDL;
using VAGastronomistMobileApp.Model.HomeNew;
using VAGastronomistMobileApp.SQLServerDAL.HomeNew;
using VAGastronomistMobileApp.SQLServerDAL;
using CloudStorage;

public partial class HomeNew_HomeAdvertDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitData();
        }
    }

    protected void InitData()
    {
        string advertID =Convert.ToString(Request.QueryString["advertID"]);
        if(string.IsNullOrEmpty(advertID))
        {
            new CityDropDownList().BindCity(ddlCity,"87");
            ddlCity.SelectedValue = Convert.ToString(Request.QueryString["cityID"]);
            BindTitle(ddlTitle, Convert.ToInt32(ddlCity.SelectedValue));
            ddlTitle.SelectedValue = Convert.ToString(Request.QueryString["firstTitleID"]);
            BindSubTitle(ddlSubTitle, Convert.ToInt32(Request.QueryString["firstTitleID"]));
            int secondTitleID = Common.ToInt32(Request.QueryString["secondTitleID"]);
            SubTitleManager subManager = new SubTitleManager();
            DataTable dtSub = subManager.SelectSubTitle(secondTitleID);
            if (dtSub != null && dtSub.Rows.Count > 0)
            {
                DataRow drSub = dtSub.Rows[0];
                string ruleType = Convert.ToString(drSub["type"]);
                ddlSubTitle.SelectedValue = secondTitleID + "," + ruleType;
            }
            //ddlSubTitle.SelectedValue = Convert.ToString(Request.QueryString["secondTitleID"]);
        }
        else
        {
            // 修改
            AdvertManager advertManager = new AdvertManager();
            DataTable dt=advertManager.SelectAdvert(Common.ToInt32(advertID));
            if(dt!=null && dt.Rows.Count>0)
            {
                DataRow dr = dt.Rows[0];
                new CityDropDownList().BindCity(ddlCity, "87");
                ddlCity.SelectedValue = Convert.ToString(dr["cityID"]);
                BindTitle(ddlTitle, Convert.ToInt32(dr["cityID"]));
                ddlTitle.SelectedValue = Convert.ToString(dr["firstTitleID"]);
                BindSubTitle(ddlSubTitle, Convert.ToInt32(dr["firstTitleID"]));
                int secondTitleID = Common.ToInt32(dr["secondTitleID"]);
                SubTitleManager subManager = new SubTitleManager();
                DataTable dtSub=subManager.SelectSubTitle(secondTitleID);
                if(dtSub!=null && dtSub.Rows.Count>0 )
                {
                    DataRow drSub = dtSub.Rows[0];
                    string ruleType = Convert.ToString(drSub["type"]);
                    ddlSubTitle.SelectedValue = secondTitleID + "," + ruleType;
                }
                
                text.Value= Convert.ToString(dr["title"]);
                hidShopID.Value = Convert.ToString(dr["shopID"]);
                txtSubTitle.Text = Convert.ToString(dr["subTitle"]);
                txtIndex.Text = Convert.ToString(dr["index"]);
                hidIndex.Value = txtIndex.Text;
                radListStatus.SelectedValue = Convert.ToString(dr["status"]);
                hidRadType.Value = Convert.ToString(dr["status"]);
                hidImageUrl.Value = Convert.ToString(dr["yuanImageUrl"]);
                if (!string.IsNullOrEmpty(hidImageUrl.Value.Trim()))
                {
                    this.Big_Img.ImageUrl = WebConfig.CdnDomain + WebConfig.ImagePath + hidImageUrl.Value;
                }
                //if(ddlTitle.SelectedItem.Text=="全部")
                //{
                //    Button_Upload.Enabled = false;
                //}
                ddlCity.Enabled = false;
            }
        }
    }

    private void Add()
    {
        AdvertShop objAdvertShop = BindAdvertShop();
        AdvertManager manager = new AdvertManager();
        manager.InsertAdvertShop(objAdvertShop);
    }

    private void Edit()
    {
        AdvertShop objAdvertShop = BindAdvertShop();
        AdvertManager manager = new AdvertManager();
        manager.UpdateAdvertShop(objAdvertShop);
    }

    private bool CheckValue()
    {
        if (ddlCity.SelectedValue == "0")
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('请选择城市！');</script>");
            return false;
        }
        if (ddlTitle.SelectedValue == "0" || ddlTitle.SelectedValue=="")
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('请选择一级标题！');</script>");
            return false;
        }
        if (string.IsNullOrEmpty(hidImageUrl.Value.Trim()))
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('请上传广告图片！');</script>");
            return false;
        }
        if (ddlSubTitle.SelectedValue == "0,0" || ddlSubTitle.SelectedValue=="")
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('请选择二级标题！');</script>");
            return false;
        }
        if (string.IsNullOrEmpty(txtIndex.Text.Trim()) && ddlSubTitle.SelectedValue.Contains(",1"))
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('推荐广告排序不能为空！');</script>");
            return false;
        }
        else 
        {
            if (ddlSubTitle.SelectedValue.Contains(",2"))
            {
                txtIndex.Text = "0";
            }
            else
            {
                int oldIndex = Common.ToInt32(hidIndex.Value);
                int newIndex = Common.ToInt32(txtIndex.Text);
                string secondTitleID = Convert.ToString(ddlSubTitle.SelectedValue);
                if (!string.IsNullOrEmpty(secondTitleID))
                {
                    secondTitleID = secondTitleID.Split(',')[0];
                }
                string oldRadType = hidRadType.Value;
                int cityID = Common.ToInt32(ddlCity.SelectedValue);
                if (ddlSubTitle.SelectedValue.Contains(",1") && radListStatus.SelectedValue == "1")
                {
                    if (oldIndex == newIndex && oldRadType == "1")
                    {
                    }
                    else
                    {
                        AdvertManager manager = new AdvertManager();
                        if (manager.CheckHasAdvertIndex(newIndex, secondTitleID, cityID))
                        {
                            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('推荐广告位已被占用！');</script>");
                            return false;
                        }
                    }
                }
            }
        }
        if (string.IsNullOrEmpty(txtSubTitle.Text.Trim()))
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('副标题不能为空！');</script>");
            return false;
        }
        if(string.IsNullOrEmpty(hidShopID.Value))
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('请选择一个存在的商户！');</script>");
            return false;
        }
        return true;
    }

    private AdvertShop BindAdvertShop()
    {
        AdvertShop objAdvertShop = new AdvertShop();
        objAdvertShop.cityID = Common.ToInt32(ddlCity.SelectedValue);
        objAdvertShop.firstTitleID = Common.ToInt32(ddlTitle.SelectedValue);
        objAdvertShop.secondTitleID = Common.ToInt32(ddlSubTitle.SelectedValue.Split(',')[0]);
        if (ddlSubTitle.SelectedValue.Split(',')[1] == "1")
        {
            objAdvertShop.index = Common.ToInt32(txtIndex.Text);
        }
        else
        {
            objAdvertShop.index = 0;
        }
        objAdvertShop.title = text.Value;
        objAdvertShop.shopID = Common.ToInt32(hidShopID.Value);
        objAdvertShop.subtitle = Convert.ToString(txtSubTitle.Text.Trim());
        objAdvertShop.status = Common.ToInt32(radListStatus.SelectedValue);
        objAdvertShop.createBy = Convert.ToString(((VAEmployeeLoginResponse)HttpContext.Current.Session["UserInfo"]).employeeID);
        objAdvertShop.createTime = DateTime.Now;
        objAdvertShop.id = Common.ToInt32(Request.QueryString["advertID"]);
        objAdvertShop.yuanImageUrl = hidImageUrl.Value.Trim();
        return objAdvertShop;
    }

    private void BindTitle(DropDownList ddl_Title, int cityID)
    {
        ddl_Title.Items.Clear();
        int NonAdTitleID;
        List<TitleViewModel> data = TitleManager.SelectHandleTitle(cityID, out NonAdTitleID);
        data = data.FindAll(t => t.titleID != NonAdTitleID).ToList();
        ddl_Title.DataSource = data;
        ddl_Title.DataTextField = "titleName";
        ddl_Title.DataValueField = "titleId";
        ddl_Title.DataBind();
        ddl_Title.Items.Insert(0, new ListItem("", "0"));
        //ddl_Title.SelectedValue = Common.ToString(NonAdTitleID);
    }

    private void BindSubTitle(DropDownList ddl_SubTitle, int firstTitleID)
    {
        ddl_SubTitle.Items.Clear();
        List<SubTitleModel> data = TitleManager.SelectHandleSubTitle(firstTitleID, 1);
        ddl_SubTitle.DataSource = data;
        ddl_SubTitle.DataTextField = "titleName";
        ddl_SubTitle.DataValueField = "titleId";
        ddl_SubTitle.DataBind();
        //ddl_SubTitle.Items.Insert(0, new ListItem("所有栏目", "0,0"));
        ddl_SubTitle.Items.Insert(0, new ListItem("", "0,0"));
        //ddl_SubTitle.SelectedValue = "0,0";
    }

    /// <summary>
    /// 城市下拉变更事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlCity_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindTitle(ddlTitle, Convert.ToInt32(ddlCity.SelectedValue));
        hidShopID.Value = "";
        text.Value = "";
        ddlTitle_SelectedIndexChanged(null, null);
    }
    /// <summary>
    /// 一级目录下拉变更事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlTitle_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindSubTitle(ddlSubTitle, Convert.ToInt32(ddlTitle.SelectedValue));
        if (ddlTitle.SelectedItem.Text == "全部")
        {
            Button_Upload.Enabled = false;
        }
    }
    

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        if (CheckValue())
        {
            string advertID = Convert.ToString(Request.QueryString["advertID"]);
            if (string.IsNullOrEmpty(advertID))
            {
                Add();
            }
            else
            {
                Edit();
            }
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('保存成功！');window.location.href='HomeAdvert.aspx?cityID=" + ddlCity.SelectedValue + "&firstTitleID=" + ddlTitle.SelectedValue + "&secondTitleID=" + ddlSubTitle.SelectedValue.Split(',')[0] + "'</script>");
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/HomeNew/HomeAdvert.aspx?cityID=" + ddlCity.SelectedValue + "&firstTitleID=" + ddlTitle.SelectedValue + "&secondTitleID=" + ddlSubTitle.SelectedValue.Split(',')[0] );
    }
    /// <summary>
    /// 上传广告图片
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button_Upload_Click(object sender, EventArgs e)
    {
        string fileName = Server.HtmlEncode(fileUpload.FileName);
        string extension = System.IO.Path.GetExtension(fileName);
        if (fileName != "")
        {
            if (Common.ValidateExtension(extension) && (extension.Replace(".", "").ToLower() == "gif" || extension.Replace(".", "").ToLower() == "png" || extension.Replace(".", "").ToLower() == "jpg" ))
            {
                if (fileUpload.PostedFile.ContentLength / 1024 < 1024)
                {
                    string virtualPath = string.Empty;
                    string imageName = string.Empty;
                    virtualPath = WebConfig.Advertisement + "common/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                    imageName = DateTime.Now.ToString("ddHHmmssfff") + extension;

                    string objectKey = virtualPath + imageName;//图片在数据库中的实际位置
                    hidImageUrl.Value = objectKey;
                    CloudStorageResult result = CloudStorageOperate.PutObject(WebConfig.ImagePath +objectKey, fileUpload, imageName);

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
                Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('请上传gif,png,jpg格式的图片！');</script>");
            }
        }
    }
    /// <summary>
    /// 预览
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnShowAdvertPic_Click(object sender, EventArgs e)
    {
        if (ddlCity.SelectedValue == "0")
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('请选择城市！');</script>");
            return;
        }
        if (ddlTitle.SelectedValue == "0")
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('请选择一级标题！');</script>");
            return;
        }
        if (ddlSubTitle.SelectedValue == "")
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('请选择二级标题！');</script>");
            return;
        }
        if (string.IsNullOrEmpty(hidImageUrl.Value.Trim()))
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('请上传广告图片！');</script>");
            return;
        }
        if (string.IsNullOrEmpty(txtIndex.Text.Trim()) && ddlSubTitle.SelectedValue.Contains(",1"))
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('推荐广告排序不能为空！');</script>");
            return;
        }
        Response.Redirect("HomeAdvertShow.aspx?cityID=" + ddlCity.SelectedValue + "&firstTitleID=" + ddlTitle.SelectedValue + "&secondTitleID=" + ddlSubTitle.SelectedValue.Split(',')[0] + "&advertIndex=" + txtIndex.Text.Trim() + "&advertPicUrl=" + hidImageUrl.Value + "&advertID=" + Convert.ToString(Request.QueryString["advertID"]));
    }
}