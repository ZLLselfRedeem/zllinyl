using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.WebPageDll;
using VAGastronomistMobileApp.Model;
using CloudStorage;
using System.Transactions;

public partial class RedEnvelope_ActivityShareConfig : System.Web.UI.Page
{
    ActivityShareOperate activityShareOperate = new ActivityShareOperate();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ddlType.SelectedValue = ((int)ActivityShareInfoType.Text).ToString();

            if (Request.QueryString["activityId"] != null && Request.QueryString["activityId"].Length > 0)
            {
                ViewState["activityId"] = Common.ToInt32(Request.QueryString["activityId"]); 
            }
            if (ViewState["activityId"] == null)
            {
                BindShareList(ActivityShareInfoType.Text, Common.ToInt32(ViewState["activityId"]));
                lkbRedirect.Visible = false;
            }
            else
            {
                BindShareList(ActivityShareInfoType.Text, Common.ToInt32(ViewState["activityId"]));
                lkbRedirect.Visible = true;
            }
        }
    }
    private void BindShareList(ActivityShareInfoType type, int activityId)
    {
        List<ActivityShareInfo> shareInfo = null;
        if (activityId == 0)
        {
            shareInfo = activityShareOperate.QueryActivityShareInfo(type);
        }
        else
        {
            shareInfo = activityShareOperate.QueryActivityShareInfo(type, activityId);
        }
        if (shareInfo.Count > 0)
        {
            gdvActivity.DataSource = shareInfo;
        }
        else
        {
            gdvActivity.DataSource = null;
        }
        gdvActivity.DataBind();
    }

    //新增文字
    protected void btnSave_Click(object sender, EventArgs e)
    {
        ActivityShareInfo share = new ActivityShareInfo();
        if (ViewState["activityId"] != null && Common.ToInt32(ViewState["activityId"]) != 0)
        {
            share.activityId = Common.ToInt32(ViewState["activityId"]);
        }
        else
        {
            share.activityId = 0;
        }
        share.type = ActivityShareInfoType.Text;
        share.remark = txbShareText.Text.Trim();
        share.status = true;
        if (!string.IsNullOrEmpty(txbShareText.Text.Trim()))
        {
            int i = activityShareOperate.InsertActivityShareInfo(share);
            if (i > 0)
            {
                ddlType.SelectedValue = ((int)ActivityShareInfoType.Text).ToString();
                BindShareList(ActivityShareInfoType.Text, Common.ToInt32(ViewState["activityId"]));
                CommonPageOperate.AlterMsg(this, "新增成功");
            }
            else
            {
                CommonPageOperate.AlterMsg(this, "新增失败");
            }
        }
    }

    //新增图片
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        ActivityShareInfo share = new ActivityShareInfo();
        string imageName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + System.IO.Path.GetExtension(fileUpload.FileName);
        if (ViewState["activityId"] != null && Common.ToInt32(ViewState["activityId"]) != 0)
        {
            share.activityId = Common.ToInt32(ViewState["activityId"]);
        }
        else
        {
            share.activityId = 0;
        }
        share.type = ActivityShareInfoType.Image;
        share.remark = WebConfig.ImagePath + "TreasureChest/" + imageName;
        share.status = true;

        object[] obj = UploadFile(fileUpload, imageName);

        if (Common.ToBool(obj[0]))
        {
            int i = activityShareOperate.InsertActivityShareInfo(share);
            if (i > 0)
            {
                ddlType.SelectedValue = ((int)ActivityShareInfoType.Image).ToString();
                BindShareList(ActivityShareInfoType.Image, Common.ToInt32(ViewState["activityId"]));
                CommonPageOperate.AlterMsg(this, "新增成功");
            }
            else
            {
                CloudStorageOperate.DeleteObject(WebConfig.ImagePath + "TreasureChest/" + imageName);
                CommonPageOperate.AlterMsg(this, "新增失败," + obj[1].ToString());
            }
        }
        else
        {
            CommonPageOperate.AlterMsg(this, "新增失败," + obj[1].ToString());
        }
    }

    private object[] UploadFile(FileUpload fileUpload, string imageName)
    {
        object[] objResult = new object[] { false, "" };
        string fileName = fileUpload.FileName;
        try
        {
            if (string.IsNullOrEmpty(fileName))
            {
                objResult[1] = "请先选择文件";
            }
            else
            {
                string extension = System.IO.Path.GetExtension(fileName);//获取扩展名

                System.Drawing.Bitmap originalBMP = new System.Drawing.Bitmap(fileUpload.FileContent);

                if (originalBMP.Width != 100 || originalBMP.Height != 100 || (extension != ".png" && extension != ".jpg"))
                {
                    objResult[1] = "图片尺寸:100*100，类型png或jpg";
                }
                else
                {
                    string objectKey = WebConfig.ImagePath + "TreasureChest/" + imageName;
                    CloudStorageResult result = CloudStorageOperate.PutObject(objectKey, fileUpload, imageName);

                    if (result.code)
                    {
                        if (result.code)
                        {
                            objResult[0] = true;
                        }
                        else
                        {
                            objResult[1] = "图片上传失败";
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            objResult[1] = ex.Message;
        }
        return objResult;
    }

    protected void lnkbtn_OnCommand(object sender, CommandEventArgs e)
    {
        int id = Common.ToInt32(e.CommandArgument);

        switch (e.CommandName)
        {
            case "del":
                if (ddlType.SelectedValue == ((int)ActivityShareInfoType.Image).ToString())
                {
                    ActivityShareInfo share = activityShareOperate.QueryActivityShareInfoById(id); ;
                    CloudStorageOperate.DeleteObject(share.remark);
                }
                bool delete = activityShareOperate.DeleteActivityShareInfo(id);
                if (delete)
                {
                    BindShareList((ActivityShareInfoType)Common.ToInt32(ddlType.SelectedValue), Common.ToInt32(ViewState["activityId"]));
                    CommonPageOperate.AlterMsg(this, "删除成功");
                }
                else
                {
                    CommonPageOperate.AlterMsg(this, "删除失败");
                }
                break;
            default:
                break;
        }
    }
    /// <summary>
    /// 分页操作
    /// </summary>
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        //BindActivityList("", AspNetPager1.CurrentPageIndex, pageSize);
    }
    protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindShareList((ActivityShareInfoType)Common.ToInt32(ddlType.SelectedValue), Common.ToInt32(ViewState["activityId"]));
    }
    protected void gdvActivity_DataBound(object sender, EventArgs e)
    {

        for (int i = 0; i < gdvActivity.Rows.Count; i++)
        {
            string type = gdvActivity.DataKeys[i].Values["type"].ToString();
            string path = gdvActivity.DataKeys[i].Values["remark"].ToString();
            Image img = (Image)gdvActivity.Rows[i].FindControl("imgDish");
            if (type == "Image")
            {
                img.ImageUrl = WebConfig.OssDomain + path;
            }
            else
            {
                img.Visible = false;
            }
        }
    }
    protected void btnActivityRule_Click(object sender, EventArgs e)
    {
        ActivityShareInfo share = new ActivityShareInfo();
        if (ViewState["activityId"] != null && Common.ToInt32(ViewState["activityId"]) != 0)
        {
            share.activityId = Common.ToInt32(ViewState["activityId"]);
        }
        else
        {
            share.activityId = 0;
        }
        share.type = ActivityShareInfoType.activityRule;
        share.remark = txbActivityRule.Text.Trim();
        share.status = true;
        if (!string.IsNullOrEmpty(txbActivityRule.Text.Trim()))
        {
            int i = activityShareOperate.InsertActivityShareInfo(share);
            if (i > 0)
            {
                ddlType.SelectedValue = ((int)ActivityShareInfoType.activityRule).ToString();
                BindShareList(ActivityShareInfoType.activityRule, Common.ToInt32(ViewState["activityId"]));
                CommonPageOperate.AlterMsg(this, "新增成功");
            }
            else
            {
                CommonPageOperate.AlterMsg(this, "新增失败");
            }
        }
    }
}